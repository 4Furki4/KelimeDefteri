using FluentValidation;
using KelimeDefteri.ViewModels.Defter;

namespace KelimeDefteri.Models.Validations
{
    public class RecordValidator : AbstractValidator<CreateRecordViewModel>
    {
        public RecordValidator()
        {
            //RuleFor(rVM => rVM.WordNames).ForEach(item => item.NotEmpty().NotEqual(String.Empty).MinimumLength(2));
            //RuleFor(rVM => rVM.WordDefs).ForEach(item => item.NotEmpty().NotEqual(String.Empty).MinimumLength(2));
            //RuleFor(rVM => rVM.Date).NotNull().GreaterThan(DateTime.Now.Date);
        }
    }
}
