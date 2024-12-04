using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class DispositivoValidator : AbstractValidator<Dispositivo>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public DispositivoValidator()
        {
            RuleFor(c => c.Identificador).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
