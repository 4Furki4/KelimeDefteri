using FluentValidation;
using KelimeDefteri.Models;
using KelimeDefteri.Controllers.Businesses;
using KelimeDefteri.Models.Validations;
using KelimeDefteri.ViewModels;
using KelimeDefteri.ViewModels.Defter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace KelimeDefteri.Controllers
{
    public class DefterController : Controller
    {

        public Business business { get; set; }
        private DefterDB context;
        public int pageSize = 4;

        public DefterController(DefterDB context, Business business)
        {
            this.context = context;
            this.business = business;
        }

        public async Task<IActionResult> Homepage(string? deletedRecordDate)
        {
            HomeViewModel model = new() // Fill homepage's model
            {
                TotalWordCount = await context.Words.LongCountAsync(),
                TotalRecordCount = await context.Records.CountAsync(),
                deletedRecordDate = deletedRecordDate // be aware of it can be null which needed in view
            };
            
            return View(model);
        }

        public async Task<IActionResult> AllRecord(int recordPage = 1)
        {
            List<AllRecordViewModel> model = new();
            List<Record> records = await business.getAllRecords();
            foreach (Record record in records)
            {
                model.Add(new AllRecordViewModel { Words = record.Words, date = record.date, Id = record.Id});
            }
            return View(new RecordListViewModel
            {
                Records = model.OrderBy(m => m.Id).Skip((recordPage - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo { CurrentPage = recordPage, ItemsPerPage = pageSize, TotalItems = await context.Records.CountAsync() }
            });
        }

        public IActionResult AddRecord() // to get blank form for adding new record
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromForm] NewRecordViewModel newInputs, [FromForm] CreateRecordViewModel VM)
        {


            //Implement fluent validation rules
            try
            {
                RecordValidator validations = new();
                await validations.ValidateAndThrowAsync(VM);
            }
            catch (ValidationException ex)
            {
                string errorMessages = "";
                foreach (var error in ex.Errors)
                {
                    errorMessages += error.ErrorMessage; //Get custom messages set in RecordValidator class
                }
                return RedirectToPage("/ErrorPage", new { errorMessage = errorMessages, returnPage = HttpContext.Request.Path });
            }
           
            Record record = new Record() { date = VM.Date };
            
            for (int i = 1; i < 5; i++) // I am working on 4 words, so I can loop them 4 times and can use the variables to fetch the properties defined ViewModels.
            {
                Word word = business.PrepareAWord(VM, i);

                List<string>? newDefs = business.PrepareExtraDefinitions(newInputs, i);      // get extra inputs for definitions as list of string
                List<string>? newTypes = business.PrepareExtraTypes(newInputs, i);           // get extra inputs for types as list of string


                if (newDefs.Any() && (newDefs.Contains(null) || newTypes.Contains(null))) // if user send blank extra definition inputs, redirect them to error page and show error message to them.
                    return RedirectToPage("/ErrorPage", new { errorMessage = "Lütfen ek tanımlar eklerken ikisini de girin!", returnPage = HttpContext.Request.Path });

                word = business.AddExtraDefinitions(word,newDefs, newTypes);
                
                record.Words.Add(word); //Add each created word to record object
            }

            context.Records.Add(record);                    // then add the record to db. 
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(RecordDetail), new { id = record.Id }); // redirect the user given correct inputs to the new record's detail page.
        }

        [HttpGet]
        public async Task<IActionResult> RecordDetail(long id = 1)
        {
            Record? kayit = await context.Records
                .Include(gk => gk.Words).ThenInclude(k => k.Definitions)
                .FirstOrDefaultAsync(gk => gk.Id == id);
            if (kayit is null)
            {
                return RedirectToAction("Homepage");
            }
            RecordDetailViewModel recordDetailViewModel = new();
            recordDetailViewModel.date = kayit.date;
            recordDetailViewModel.Words = kayit.Words;
            recordDetailViewModel.Id = kayit.Id;
            return View(recordDetailViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, string returnAction)
        {
            string deletedRecordDate;
            try // Check if there is a record with given id
            {
                Record? record = await context.Records.FindAsync(id);
                deletedRecordDate= record.date.ToShortDateString();
                if (record != null)
                {
                    context.Records.Remove(record);
                    await context.SaveChangesAsync();
                }
            } // If there is no, then redirect to error page with error message and returnPage
            catch (Exception ex)
            {
                if (ex.Message == "Object reference not set to an instance of an object.")
                {
                    return RedirectToPage("/ErrorPage", new { errorMessage = "Silmek istediğiniz kayıt mevcut değil!", returnPage = "/Defter/Homepage" });
                }
                else
                {
                    return RedirectToPage("/ErrorPage", new { errorMessage = ex.Message, returnPage = "/Defter/Homepage" });
                }
                
            }
            // If record is successfully deleted, then redirect to given action with deletedRecordDate
            return RedirectToAction(returnAction, new {deletedRecordDate = deletedRecordDate});
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var word = await context.Words.FindAsync(id);
            UpdateVM wordVM = new() 
            { 
                WordId = word.Id,
                Name = word.Name, 
                Definition = word.Definitions.ToList()[0].definition, 
                Type = word.Definitions.ToList()[0].definitionType 
            };

            if (word.Definitions.Count() > 1)
            {
                foreach (var defs in word.Definitions)
                {
                    if (defs.definition != wordVM.Definition)
                    {
                        wordVM.restOfDefs.Add(defs.definition);
                        wordVM.restOfTypes.Add(defs.definitionType);
                    }
                }
            }
            return View(wordVM);
        }
        public async Task<IActionResult> Update(long id, UpdateVM updateVM)
        {
            List<Definition> updatedDefs = new List<Definition>();
            updatedDefs.Add(new() { definition = updateVM.Definition, definitionType = updateVM.Type });
            if (updateVM.restOfDefs.Contains(null) || updateVM.restOfTypes.Contains(null))
            {
                return RedirectToPage("/ErrorPage", new { errorMessage = "Lütfen ek tanımlar eklerken ikisini de girin!", returnPage = HttpContext.Request.Path });
            }
            if (updateVM.restOfDefs.Any())
            {
                for (int i = 0; i < updateVM.restOfDefs.Count(); i++)
                {
                    updatedDefs.Add(
                        new() 
                        { 
                            definition = updateVM.restOfDefs[i],
                            definitionType = updateVM.restOfTypes[i]
                        });
                }
            }

            Word word = await context.Words.FirstAsync(w => w.Id == id);

            word.Name = updateVM.Name;

            word.Definitions = updatedDefs;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(RecordDetail), new { id = word.RecordId });
        }


        public async Task<IActionResult> SearchRecord(string searchInput)
        {
            var word = await context.Words.FirstOrDefaultAsync(w => w.Name == searchInput);
            if (word != null)
                return RedirectToAction(nameof(RecordDetail), new { id = word.RecordId });
            var record = await context.Records.FirstOrDefaultAsync(rec => rec.date.Date == DateTime.Parse(searchInput));
            if (record != null)
                return RedirectToAction(nameof(RecordDetail), new { id = record.Id });

            return RedirectToAction(nameof(Homepage));
        }
        
    }
}
