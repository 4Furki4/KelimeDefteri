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

        public async Task<IActionResult> AllRecord()
        {
            List<GunlukKayit> gunlukKayitlar = await context.GunlukKayitlar.Include(gk => gk.Kelimeler).ThenInclude(K=>K.Tanimlar).ToListAsync();
            return View(gunlukKayitlar);
        }

        public IActionResult AddRecord() // to get blank form for adding new record
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromForm] CreateRecordViewModel record)
        {
            List<Tanim> TanımDon(int x, CreateRecordViewModel model) // Altta Tanımları eklemeye yarayacak metot
            {
                var boluk = model.Kelime_tanimlari[x].Split(";").ToList();
                var boluk_tur = model.Kelime_turleri[x].Split(";").ToList();
                List<Tanim> list = new List<Tanim>();
                for (int i = 0; i < boluk.Count; i++)
                {
                    list.Add(new Tanim { Aciklama = boluk[i], AciklamaTuru = boluk_tur[i] });
                }
                return list;
            }         

            GunlukKayit kayit = new GunlukKayit();
            kayit.date = record.Date;
            try
            {
                for (int i = 0; i < 4; i++)
                    kayit.Kelimeler.Add(new Kelime { Name = record.Kelime_isimleri[i], Tanimlar = TanımDon(i, record) });
            }
            catch (Exception)
            {
                return RedirectToPage("/ErrorPage", new { errorMessage = "Lütfen 4 kelimeyi eksiksiz girin!", returnPage = HttpContext.Request.Path });
            }
            await context.GunlukKayitlar.AddAsync(kayit);
            await context.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RecordDetail(long id = 1)
        {
            GunlukKayit? kayit = await context.GunlukKayitlar
                .Include(gk => gk.Kelimeler).ThenInclude(k => k.Tanimlar)
                .FirstOrDefaultAsync(gk => gk.Id == id);

            RecordDetailViewModel recDetailVM = new();

            recDetailVM.Date = kayit.date;

            foreach (Kelime kelime in kayit.Kelimeler)
            {
                recDetailVM.KelimeAdlari.Add(kelime.Name);

                foreach (Tanim tanim in kelime.Tanimlar)
                {
                    recDetailVM.Tanimlari.Add(tanim.Aciklama);
                    recDetailVM.TanimTurleri.Add(tanim.AciklamaTuru);
                }
            }
            return View(recDetailVM);
        }
    }
}
