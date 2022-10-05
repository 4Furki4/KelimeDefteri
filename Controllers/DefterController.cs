using KelimeDefteri.Models;
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

        public IActionResult AllRecord()
        {
            List<GunlukKayit> gunlukKayitlar = context.GunlukKayitlar.Include(gk => gk.Kelimeler).ThenInclude(K=>K.Tanimlar).ToList();
            return View(gunlukKayitlar);
        }

        public IActionResult AddRecord() // to get blank form for adding new record
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecord([FromForm] GunlukKayit gunlukKayit)
        {
            return View();
        }
    }
}
