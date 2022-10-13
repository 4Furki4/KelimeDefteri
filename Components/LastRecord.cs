using KelimeDefteri.Models;
using KelimeDefteri.ViewModels.Defter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KelimeDefteri.Components
{
    public class LastRecord : ViewComponent
    {
        private DefterDB context;

        public LastRecord(DefterDB context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            GunlukKayit kayit = await context.GunlukKayitlar
                .Include(gk => gk.Kelimeler).ThenInclude(k => k.Tanimlar).OrderBy(g=> g.Id)
                .LastOrDefaultAsync() ?? new GunlukKayit();

            RecordDetailViewModel recordDetailViewModel = new();
            recordDetailViewModel.date = kayit.date;
            recordDetailViewModel.Kelimeler = kayit.Kelimeler;
            recordDetailViewModel.Id = kayit.Id;
            return View(recordDetailViewModel);
        }
    }
}
