using Core.Entity;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FiapTechChallenge.Test.Integration
{
    public class IntegrationTestFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }
        private EFRepository<Contato> _contatoRepository;

        public IntegrationTestFixture()
        {
            // Configura um banco em memória (para testes)
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase") // Aqui você pode substituir pelo UseSqlServer para o banco real
                .Options;

            Context = new ApplicationDbContext(options);

            // Criação da instância do repositório
            _contatoRepository = new EFRepository<Contato>(Context);

            // Inicializa com dados necessários
            //SeedDatabase().Wait(); // Como é assíncrono, utilizamos .Wait() para aguardar a conclusão
        }
   

        public void Dispose()
        {
            // Limpeza após os testes, caso necessário
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }


}
