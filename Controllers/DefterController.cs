using KelimeDefteri.Models;
using KelimeDefteri.ViewModels.Defter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KelimeDefteri.Controllers
{
    public class DefterController : Controller
    {
        private DefterDB context;

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

        public async Task<IActionResult> AllRecord()
        {
            List<Record> records = await context.Records.Include(gk => gk.Words).ThenInclude(K=>K.Definitions).ToListAsync();
            return View(records);
        }

        public IActionResult AddRecord() // to get blank form for adding new record
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromForm] CreateRecordViewModel recordVM)
        {
            List<Definition> GetDefinitions(int x, CreateRecordViewModel model) // Get definitions for each word
            {
                var boluk = model.WordDefs[x].Split(";").ToList(); // definitions are entered with semicolon between each def. That should be changed! 
                var boluk_tur = model.WordTypes[x].Split(";").ToList(); //Types have same logic with definitions BC their count must be equal
                List<Definition> list = new List<Definition>();
                for (int i = 0; i < boluk.Count; i++)
                {
                    list.Add(new Definition { definition = boluk[i], definitionType = boluk_tur[i] });
                }
                return list;
            }         

            Record kayit = new Record();
            kayit.date = recordVM.Date;
            try
            {
                for (int i = 0; i < 4; i++) // If there is less than 4 words entered, exception thrown 
                    kayit.Words.Add(new Word { Name = recordVM.WordNames[i], Definitions = GetDefinitions(i, recordVM) });
            }
            catch (Exception)
            {
                return RedirectToPage("/ErrorPage", new { errorMessage = "Lütfen 4 kelimeyi eksiksiz girin!", returnPage = HttpContext.Request.Path });
            }
            await context.Records.AddAsync(kayit);
            await context.SaveChangesAsync();
            // Redirecting to added record's detail page
            return RedirectToAction(nameof(RecordDetail), new {id = kayit.Id});
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
            Record? record = await context.Records.FindAsync(id);
            string deletedRecordDate = record.date.ToShortDateString();
            if (record != null)
            {
                context.Records.Remove(record);
                await context.SaveChangesAsync();
            } 
            else
                // redirect to error page with return view is HomePage
                return RedirectToPage("/ErrorPage", new { errorMessage = "Silmek istediğiniz kayıt mevcut değil!", returnPage = "/Defter/Homepage" }); 

            return RedirectToAction(returnAction, new {deletedRecordDate = deletedRecordDate});
        }
    }
}
