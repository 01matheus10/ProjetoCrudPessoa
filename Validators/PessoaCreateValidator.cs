using FluentValidation;
using ProjetoCrudPessoa.DTOs;

namespace ProjetoCrudPessoa.Validators
{
    public class PessoaCreateValidator : AbstractValidator<PessoaCreateDto>
    {
        public PessoaCreateValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.CPF)
                .NotEmpty()
                .Matches(@"^\d{11}$")
                .WithMessage("CPF deve conter exatamente 11 dígitos.");

            RuleFor(x => x.Idade)
                .GreaterThan(0);

            RuleFor(x => x.DataNascimento)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Data de nascimento não pode ser futura.");
        }
    }
}
