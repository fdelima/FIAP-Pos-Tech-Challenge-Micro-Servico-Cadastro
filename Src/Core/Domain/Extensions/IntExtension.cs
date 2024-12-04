namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions
{
    public static class IntExtension
    {
        public static bool IsZero(this int? number)
        {
            return number.HasValue && number.Value == 0;
        }
    }
}
