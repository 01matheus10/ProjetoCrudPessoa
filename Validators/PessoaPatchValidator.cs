using FluentValidation;
using ProjetoCrudPessoa.DTOs;

namespace ProjetoCrudPessoa.Validators
{
    public class PessoaPatchValidator : AbstractValidator<PessoaPatchDto>
    {
        public PessoaPatchValidator()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3)
                .When(x => !string.IsNullOrWhiteSpace(x.Nome));

            RuleFor(x => x.CPF)
                .Matches(@"^\d{11}$")
                .WithMessage("CPF deve conter exatamente 11 dígitos.")
                .When(x => !string.IsNullOrWhiteSpace(x.CPF));

            RuleFor(x => x.Idade)
                .GreaterThan(0)
                .When(x => x.Idade.HasValue);

            RuleFor(x => x.DataNascimento)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Data de nascimento não pode ser futura.")
                .When(x => x.DataNascimento.HasValue);
        }
    }
}