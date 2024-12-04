using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public ClienteValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Email).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Cpf).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
