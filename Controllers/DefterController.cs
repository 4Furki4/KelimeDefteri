﻿using FluentValidation;
using KelimeDefteri.Models;
using KelimeDefteri.Models.Validations;
using KelimeDefteri.ViewModels;
using KelimeDefteri.ViewModels.Defter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Primitives;

namespace KelimeDefteri.Controllers
{
    public class DefterController : Controller
    {
        private DefterDB context;
        public int pageSize = 4;

        public DefterController(DefterDB context)
        {
            this.context = context;
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
            List<Record> records = await context.Records.Include(gk => gk.Words).ThenInclude(K=>K.Definitions).ToListAsync();
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
            //foreach (string key in Request.Form.Keys)
            //{

            //}

            //foreach (string key in Request.Form.Keys)
            //{
            //    Request.Form.TryGetValue(key, out StringValues value);
            //}
            
            
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
                    errorMessages += error.ErrorMessage;
                }
                return RedirectToPage("/ErrorPage", new { errorMessage = errorMessages, returnPage = HttpContext.Request.Path });
            }
            


            
            // get the first word and set it to a new word entity instance
            // get the first word's definitions and types and set them to created word entities nav props.
            // if any exists, get other def and types.


            
            
            
            
            
            //RecordValidator validations = new RecordValidator();
            //var errors = validations.Validate(recordVM);
            //if (!errors.IsValid)
            //{
            //    return RedirectToPage("/ErrorPage", new { errorMessage = "Lütfen 4 kelimeyi eksiksiz girin!", returnPage = HttpContext.Request.Path });
            //}

            //List<Definition> GetDefinitions(int x, CreateRecordViewModel model) // Get definitions for each word
            //{
            //    var boluk = model.WordDefs[x].Split(";").ToList(); // definitions are entered with semicolon between each def. That should be changed! 
            //    var boluk_tur = model.WordTypes[x].Split(";").ToList(); //Types have same logic with definitions BC their count must be equal
            //    List<Definition> list = new List<Definition>();
            //    for (int i = 0; i < boluk.Count; i++)
            //    {
            //        list.Add(new Definition { definition = boluk[i], definitionType = boluk_tur[i] });
            //    }
            //    return list;
            //}         

            //Record kayit = new Record();
            //kayit.date = recordVM.Date;
            //try
            //{
            //    for (int i = 0; i < 4; i++) // If there is less than 4 words entered, exception thrown 
            //        kayit.Words.Add(new Word { Name = recordVM.WordNames[i], Definitions = GetDefinitions(i, recordVM) });
            //}
            //catch (Exception)
            //{
            //    return RedirectToPage("/ErrorPage", new { errorMessage = "Lütfen 4 kelimeyi eksiksiz girin!", returnPage = HttpContext.Request.Path });
            //}
            //await context.Records.AddAsync(kayit);
            //await context.SaveChangesAsync();
            //// Redirecting to added record's detail page
            //return RedirectToAction(nameof(RecordDetail), new {id = kayit.Id});

            return RedirectToAction(nameof(Homepage));
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
        public async Task<IActionResult> Update(int id)
        {
            WordUpdateViewModel wordVM = new();

            try
            {
                Word word = await context.Words.Include(w=>w.Definitions).FirstAsync(w=>w.Id == id);
                wordVM.Name = word.Name;
                wordVM.Definitions = word.Definitions.ToList();
            }
            catch (Exception)
            {
                return RedirectToPage("/ErrorPage", new { errorMessage = "Güncellemek istediğiniz kayıt mevcut değil", returnPage = "/Defter/Homepage" });
            }

            return View(wordVM);
        }
        
    }
}
