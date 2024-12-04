using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Messages;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public ProdutoValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Preco).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Descricao).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Categoria).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Categoria)
                .Must(x => (new List<string>(Enum.GetNames(typeof(enmProdutoCategoria)))).Count(e => e.Equals(x)) > 0)
                .WithMessage("Precisa ser alguma dessas categorias: " + string.Join(",", Enum.GetNames(typeof(enmProdutoCategoria))));
        }
    }
}
