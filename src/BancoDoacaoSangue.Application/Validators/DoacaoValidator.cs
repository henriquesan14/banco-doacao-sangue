using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
using FluentValidation;

namespace BancoDoacaoSangue.Application.Validators
{
    public class DoacaoValidator : AbstractValidator<CadastrarDoacaoCommand>
    {
        public DoacaoValidator()
        {
            RuleFor(d => d.QuantidadeMl)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
                .InclusiveBetween(420, 470).WithMessage("O campo {PropertyName} deve ser entre 420 e 470");
                
            RuleFor(d => d.DoadorId)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
