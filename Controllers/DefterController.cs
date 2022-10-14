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

        public async Task<IActionResult> Home()
        {
            HomeViewModel model = new();
            model.TotalWordCount = await context.Words.LongCountAsync(); 
            model.TotalRecordCount = await context.Records.CountAsync();
            return View(model);
        }

        public async Task<IActionResult> AllRecord()
        {
            List<Record> gunlukKayitlar = await context.Records.Include(gk => gk.Words).ThenInclude(K=>K.Definitions).ToListAsync();
            return View(gunlukKayitlar);
        }

        public IActionResult AddRecord() // to get blank form for adding new record
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromForm] CreateRecordViewModel record)
        {
            List<Definition> TanımDon(int x, CreateRecordViewModel model) // Altta Tanımları eklemeye yarayacak metot
            {
                var boluk = model.Kelime_tanimlari[x].Split(";").ToList();
                var boluk_tur = model.Kelime_turleri[x].Split(";").ToList();
                List<Definition> list = new List<Definition>();
                for (int i = 0; i < boluk.Count; i++)
                {
                    list.Add(new Definition { definition = boluk[i], definitionType = boluk_tur[i] });
                }
                return list;
            }         

            Record kayit = new Record();
            kayit.date = record.Date;
            try
            {
                for (int i = 0; i < 4; i++)
                    kayit.Words.Add(new Word { Name = record.Kelime_isimleri[i], Definitions = TanımDon(i, record) });
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
            Record kayit = await context.Records
                .Include(gk => gk.Words).ThenInclude(k => k.Definitions)
                .FirstOrDefaultAsync(gk => gk.Id == id) ?? new Record();

            RecordDetailViewModel recordDetailViewModel = new();
            recordDetailViewModel.date = kayit.date;
            recordDetailViewModel.Kelimeler = kayit.Words;
            recordDetailViewModel.Id = kayit.Id;
            return View(recordDetailViewModel);
        }
    }
}
