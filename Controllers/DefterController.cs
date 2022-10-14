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
            HomeViewModel model = new();
            model.TotalWordCount = await context.Words.LongCountAsync(); 
            model.TotalRecordCount = await context.Records.CountAsync();
            model.deletedRecordDate = deletedRecordDate;
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
            List<Definition> GetDefinitions(int x, CreateRecordViewModel model) // Altta Tanımları eklemeye yarayacak metot
            {
                var boluk = model.WordDefs[x].Split(";").ToList();
                var boluk_tur = model.WordTypes[x].Split(";").ToList();
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
                for (int i = 0; i < 4; i++)
                    kayit.Words.Add(new Word { Name = recordVM.WordNames[i], Definitions = GetDefinitions(i, recordVM) });
            }
            catch (Exception)
            {
                return RedirectToPage("/ErrorPage", new { errorMessage = "Lütfen 4 kelimeyi eksiksiz girin!", returnPage = HttpContext.Request.Path });
            }
            await context.Records.AddAsync(kayit);
            await context.SaveChangesAsync();
            return View();
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
        public async Task<IActionResult> DeleteAsync(int id, string returnAction)
        {
            Record? record = await context.Records.FindAsync(id);
            string deletedRecordDate = record.date.ToString();
            if (record != null)
            {
                context.Records.Remove(record);
                //await context.SaveChangesAsync();
            } 
            else
                return RedirectToPage("/ErrorPage", new { errorMessage = "Silmek istediğiniz kayıt mevcut değil!", returnPage = "/Defter/Homepage" });

            return RedirectToAction(returnAction, routeValues: returnAction);
        }
    }
}
