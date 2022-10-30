using FluentValidation;
using KelimeDefteri.ViewModels.Defter;

namespace KelimeDefteri.Models.Validations
{
    public class RecordValidator : AbstractValidator<CreateRecordViewModel>
    {
        public RecordValidator()
        {
            RuleFor(rVM => rVM.WordName1).NotEmpty().WithMessage("1. Kelime boş olmamalı!").NotNull().MinimumLength(2).WithMessage("1. Kelime - Kelime uzunluğu en az 2 karakter olmalıdır! \n");
            RuleFor(rVM => rVM.WordName2).NotEmpty().NotNull().MinimumLength(2).WithMessage("2. Kelime - Kelime uzunluğu en az 2 karakter olmalıdır! \n");
            RuleFor(rVM => rVM.WordName3).NotEmpty().NotNull().MinimumLength(2).WithMessage("3. Kelime - Kelime uzunluğu en az 2 karakter olmalıdır! \n");
            RuleFor(rVM => rVM.WordName3).NotEmpty().NotNull().MinimumLength(2).WithMessage("4. Kelime - Kelime uzunluğu en az 2 karakter olmalıdır! \n");
        }
    }
}
