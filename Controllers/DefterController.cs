﻿using KelimeDefteri.Models;
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
            List<Tanim> TanımDon(int x, CreateRecordViewModel model)
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
            
            for (int i = 0; i < 4; i++) 
            {
                kayit.Kelimeler.Add(new Kelime { Name = record.Kelime_isimleri[i], Tanimlar =  TanımDon(i, record)});
            }

            await context.GunlukKayitlar.AddAsync(kayit);
            await context.SaveChangesAsync();
            return View();
        }
    }
}
