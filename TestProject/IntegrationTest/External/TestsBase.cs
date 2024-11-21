using Microsoft.EntityFrameworkCore;
using TestProject.IntegrationTest.Infra;

namespace TestProject.IntegrationTest.External
{
    public class TestsBase : IDisposable
    {

        //internal readonly FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.Context _context;
        protected readonly DbContextOptions<FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context> _options;
        internal readonly SqlServerTestFixture _sqlserverTest;

        public TestsBase()
        {
            // Do "global" initialization here; Called before every test method.
            _sqlserverTest = new SqlServerTestFixture();
            //_context = sqlserverTest.GetDbContext();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
            //_context.Dispose();
            _sqlserverTest.Dispose();
        }
    }
}
