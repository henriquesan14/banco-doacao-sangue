using BancoDoacaoSangue.Application.Commands.AtualizaDoador;
using FluentValidation;

namespace BancoDoacaoSangue.Application.Validators
{
    public class AtualizaDoadorValidator : AbstractValidator<AtualizaDoadorCommand>
    {
        public AtualizaDoadorValidator()
        {
            RuleFor(d => d.NomeCompleto)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(200).WithMessage("O campo {PropertyName} não pode ter mais de 200 caracteres");
            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .EmailAddress().WithMessage("O campo {PropertyName} não é um email válido")
                .MaximumLength(100).WithMessage("O campo {PropertyName} não pode ter mais de 100 caracteres");
            RuleFor(d => d.DataNascimento)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(d => d.Genero)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(50).WithMessage("O campo {PropertyName} não pode ter mais de 50 caracteres");
            RuleFor(d => d.Peso)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(d => d.TipoSanguineo)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(10).WithMessage("O campo {PropertyName} não pode ter mais de 10 caracteres");
            RuleFor(d => d.FatorRh)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(10).WithMessage("O campo {PropertyName} não pode ter mais de 10 caracteres");
            RuleFor(d => d.Cep)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(8).WithMessage("O campo {PropertyName} não pode ter mais de 8 caracteres");
        }
    }
}
