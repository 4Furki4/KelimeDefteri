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
            Record kayit = await context.Records
                .Include(gk => gk.Words).ThenInclude(k => k.Definitions).OrderBy(g=> g.Id)
                .LastOrDefaultAsync() ?? new Record();

            RecordDetailViewModel VM = new();
            VM.date = kayit.date;
            VM.Kelimeler = kayit.Words;
            VM.Id = kayit.Id;
            return View(VM);
        }
    }
}
