using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;

namespace TestProject.MockData
{
	/// <summary>
	/// Mock de dados das ações
	/// </summary>
	public class DispositivoMock
	{

		/// <summary>
		/// Mock de dados válidos
		/// </summary>
		public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
		{
			for (var index = 1; index <= quantidade; index++)
				yield return new object[]
				{
					$"Identificador {index}"
				};
		}

		/// <summary>
		/// Mock de dados inválidos
		/// </summary>
		public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
		{
			for (var index = 1; index <= quantidade; index++)
				yield return new object[]
				{
					null
				};
		}

		/// <summary>
		/// Mock de dados válidos
		/// </summary>
		public static IEnumerable<object[]> ObterDadosConsulta(int quantidade)
		{

			for (var index = 1; index <= quantidade; index++)
			{
				var param = new PagingQueryParam<Dispositivo>() { CurrentPage = 1, Take = 10 };
				yield return new object[]
				{
					param,
					param.SortProp(),
					new List<Dispositivo>{
						new Dispositivo {
							IdDispositivo = Guid.NewGuid(),
							Identificador = $"Identificador {index}"
						}
					}
				};
			}
		}

		public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
		{
			for (var index = 1; index <= quantidade; index++)
				yield return new object[]
				{
					Guid.NewGuid()
				};
		}

		public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
		{
			for (var index = 1; index <= quantidade; index++)
				yield return new object[]
				{
					Guid.Empty
				};
		}
	}
}
