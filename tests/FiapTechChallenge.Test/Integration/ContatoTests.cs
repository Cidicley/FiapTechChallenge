using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FiapTechChallenge.Test.Integration
{
    public class ContatoTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly EFRepository<Contato> _contatoRepository;

        public ContatoTests(IntegrationTestFixture fixture)
        {
            _context = fixture.Context;
            _contatoRepository = new EFRepository<Contato>(_context);
        }

        //[Fact]
//public async Task RetornaSucessoAoPesquisarUmEmailCadastradoParaOContatoCriado()
//{
//    // Verificando se o Contato foi cadastrado corretamente
//    var contato = await _context.Contato
//        .FirstOrDefaultAsync(c => c.Email == "testcontato@example.com");

//    Assert.NotNull(contato); // Verifica se o Contato foi realmente encontrado
//    Assert.Equal("Test Contato", contato.Nome); // Verifica o Nome
//    Assert.Equal("testcontato@example.com", contato.Email); // Verifica o E-mail
//}

        [Fact]
        public async Task CriarContatoComSucesso()
        {
            // Arranjo (setup)
            var contato = new Contato
            {
                Nome = "Contato Teste",
                Telefone = 987654321,
                Email = "test@novocontato.com",
                RegiaoContato = new RegiaoContato
                {
                    DDD = 13,
                    Estado = "SP",
                    Regiao = "SUDESTE",
                    Id = 6000
                }
            };

            // Ação (ação real)
            await _contatoRepository.CadastrarAsync(contato);

            // Asserts
            var contatoSalvo = await _context.Contato
                .FirstOrDefaultAsync(c => c.Email == contato.Email);

            Assert.NotNull(contatoSalvo);
            Assert.Equal("Contato Teste", contatoSalvo.Nome);
            Assert.Equal("test@novocontato.com", contatoSalvo.Email);
        }

        [Fact]
        public async Task RetornaErro_CriarContatoComErro_Validação()
        {
            // Arranjo (setup)
            var contato = new Contato
            {
                Nome = "",  // Nome vazio deve falhar na validação
                Telefone = 123456789,
                Email = "erro@example.com",
                RegiaoContato = new RegiaoContato
                {
                    DDD = 13,
                    Estado = "SP",
                    Regiao = "SUDESTE",
                    Id = 4000
                }
            };

            // Ação
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(contato, new ValidationContext(contato), validationResults, true);

            // Assert
            Assert.False(isValid);  // Esperamos que o objeto não seja válido devido ao nome vazio
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("Nome"));  // Verifica se há um erro relacionado ao campo Nome

        }

        [Fact]
        public async Task ConsultaContatoPorId()
        {
            var contato = new Contato
            {
                Id = 5,
                Nome = "Contato Teste",
                Telefone = 987654321,
                Email = "test@novocontato.com",
                RegiaoContato = new RegiaoContato
                {
                    DDD = 13,
                    Estado = "SP",
                    Regiao = "SUDESTE",
                    Id = 3000
                }
            };

            await _contatoRepository.CadastrarAsync(contato); // Salva no banco

            // Act: Consulta pelo DDD
            var contatoConsulta = await _contatoRepository.ObterPorIdAsync(contato.Id);

            // Assert: Verifica se o objeto retornado é o que foi inserido
            Assert.NotNull(contatoConsulta);
            Assert.Equal("Contato Teste", contatoConsulta.Nome);
            Assert.Equal(987654321, contatoConsulta.Telefone);
            Assert.Equal("test@novocontato.com", contatoConsulta.Email);
            Assert.Equal(13, contatoConsulta.RegiaoContato.DDD);
            Assert.Equal("SUDESTE", contatoConsulta.RegiaoContato.Regiao);
            Assert.Equal("SP", contatoConsulta.RegiaoContato.Estado);
        }

        [Fact]
        public async Task AtualizacaoContato()
        {
            // Arrange: Criação de um RegiaoContato válido
            var contato = new Contato
            {
                Id = 10,
                DataCriacao = DateTime.Now,
                Nome = "Contato Teste",
                Telefone = 987654321,
                Email = "test@novocontato.com",
                RegiaoContato = new RegiaoContato
                {
                    DDD = 11,
                    Regiao = "SUDESTE",
                    Estado = "SP",
                    DataCriacao = DateTime.Now,
                    Id = 2000
                }
            };

            await _contatoRepository.CadastrarAsync(contato); // Salva no banco

            // Act: Atualiza o RegiaoContato
            contato.RegiaoContato.DDD = 71;
            contato.RegiaoContato.Regiao = "NORDESTE";
            contato.RegiaoContato.Estado = "BA";
            await _contatoRepository.AlterarAsync(contato); // Método que você provavelmente tem no repositório

            // Assert: Verifica se o registro foi realmente atualizado
            var contatoAtualizado = await _contatoRepository.ObterPorIdAsync(10);

            Assert.NotNull(contatoAtualizado);
            Assert.Equal("NORDESTE", contato.RegiaoContato.Regiao);
            Assert.Equal("BA", contato.RegiaoContato.Estado);
            Assert.Equal(71, contato.RegiaoContato.DDD);
        }

        [Fact]
        public async Task ExcluirContato()
        {
            // Arrange: Criação de um Contato válido            
            int id = 15;
            var contato = new Contato
            {
                Id = id,
                DataCriacao = DateTime.Now,
                Nome = "Contato Teste",
                Telefone = 987654321,
                Email = "test@novocontato.com",
                RegiaoContato = new RegiaoContato
                {
                    DDD = 11,
                    Regiao = "SUDESTE",
                    Estado = "SP",
                    DataCriacao = DateTime.Now,
                    Id = 1000
                }
            };

            await _contatoRepository.CadastrarAsync(contato); // Salva no banco

            // Act: Exclui o RegiaoContato
            await _contatoRepository.DeletarAsync(id); // Método que você provavelmente tem no repositório

            // Assert: Verifica se o objeto foi excluído
            var contatoExcluido = await _contatoRepository
                .ObterPorIdAsync(id);

            Assert.Null(contatoExcluido); // Espera-se que não encontre mais o objeto
        }
    }
}
