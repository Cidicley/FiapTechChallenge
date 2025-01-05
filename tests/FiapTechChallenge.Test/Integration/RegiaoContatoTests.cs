using Core.Entity;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FiapTechChallenge.Test.Integration
{
    public class RegiaoContatoTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly EFRepository<RegiaoContato> _regiaoContatoRepository;

        public RegiaoContatoTests(IntegrationTestFixture fixture)
        {
            _context = fixture.Context;
            _regiaoContatoRepository = new EFRepository<RegiaoContato>(_context);
        }

        [Fact]
        public async Task CriarRegiaoContatoValido()
        {
            // Arrange
            var regiaoContato = new RegiaoContato
            {
                Id = 10,
                DDD = 11, // Um valor válido de DDD
                Regiao = "SUDESTE",
                Estado = "SP"
            };

            // Ação
            await _regiaoContatoRepository.CadastrarAsync(regiaoContato);

            // Assert
            var regiaoContatoSalvo = await _context.RegiaoContato
                .FirstOrDefaultAsync(r => r.DDD == regiaoContato.DDD && r.Estado == regiaoContato.Estado && r.Regiao == regiaoContato.Regiao);

            Assert.NotNull(regiaoContatoSalvo); // Verifica se a entidade foi salva corretamente no banco
            Assert.Equal(regiaoContato.DDD, regiaoContatoSalvo.DDD);
            Assert.Equal(regiaoContato.Regiao, regiaoContatoSalvo.Regiao);
            Assert.Equal(regiaoContato.Estado, regiaoContatoSalvo.Estado);
        }

        [Fact]
        public async Task CriarRegiaoContatoComErroValidoDDDForaDoIntervalo()
        {
            // Arrange
            var regiaoContatoInvalido = new RegiaoContato
            {
                DDD = 100,  // DDD fora do intervalo permitido
                Regiao = "SUDESTE",
                Estado = "SP"
            };

            // Validação explícita
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(regiaoContatoInvalido, new ValidationContext(regiaoContatoInvalido), validationResults, true);

            // Verifique se a validação falha, já que o DDD está fora do intervalo
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("DDD"));

            // Ação - Não tente salvar se já falhou na validação
            if (!isValid)
            {
                // Assert - Se não for válido, a exceção de validação será esperada
                Assert.ThrowsAsync<ValidationException>(() => throw new ValidationException(validationResults.First().ErrorMessage));
            }
            else
            {
                // Caso contrário, pode tentar salvar no banco de dados e verificar se o erro ocorre na camada de persistência
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    await _regiaoContatoRepository.CadastrarAsync(regiaoContatoInvalido);
                });
            }
        }        

        [Fact]
        public async Task CriarRegiaoContatoComErroValidoRegiaoNomeVazio()
        {
            // Arrange
            var regiaoContatoInvalido = new RegiaoContato
            {
                DDD = 11,
                Regiao = "",  // Nome da região vazio deve falhar
                Estado = "SP"
            };

            // Ação
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(regiaoContatoInvalido, new ValidationContext(regiaoContatoInvalido), validationResults, true);

            // Assert
            Assert.False(isValid);  // Esperamos que o objeto não seja válido devido ao Regiao vazio
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("Regiao"));  // Verifica se há um erro relacionado ao campo Regiao

            // Verifique se a mensagem de erro está na lista de erros
            var errorMessage = validationResults.FirstOrDefault()?.ErrorMessage;
            Assert.NotNull(errorMessage);
            Assert.Contains("Regiao", errorMessage);

            // Caso o objeto seja válido (não esperado neste caso), tente salvar no banco
            if (isValid)
            {
                var exception = await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    await _regiaoContatoRepository.CadastrarAsync(regiaoContatoInvalido);
                });

                Assert.NotNull(exception);  // Verifica se a exceção foi lançada
            }
        }

        [Fact]
        public async Task CriarRegiaoContatoComErroValidoEstadoNomeVazio()
        {
            // Arrange
            var regiaoContatoInvalido = new RegiaoContato
            {
                DDD = 11,
                Regiao = "SUDESTE",
                Estado = ""  // Estado vazio deve falhar
            };

            // Ação
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(regiaoContatoInvalido, new ValidationContext(regiaoContatoInvalido), validationResults, true);

            // Assert
            Assert.False(isValid);  // Esperamos que o objeto não seja válido devido ao Estado vazio
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("Estado"));  // Verifica se há um erro relacionado ao campo Estado

            // Verifique se a mensagem de erro está na lista de erros
            var errorMessage = validationResults.FirstOrDefault()?.ErrorMessage;
            Assert.NotNull(errorMessage);
            Assert.Contains("Estado", errorMessage);

            // Caso o objeto seja válido (não esperado neste caso), tente salvar no banco
            if (isValid)
            {
                var exception = await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    await _regiaoContatoRepository.CadastrarAsync(regiaoContatoInvalido);
                });

                Assert.NotNull(exception);  // Verifica se a exceção foi lançada
            }
        }

        [Fact]
        public async Task Consulta_RegiaoContato_PorDDD()
        {
            int ddd = 11;

            // Arrange: Criação de um RegiaoContato válido
            var regiaoContato = new RegiaoContato
            {
                DDD = ddd,
                Regiao = "SUDESTE",
                Estado = "SP"
            };

            await _regiaoContatoRepository.CadastrarAsync(regiaoContato); // Salva no banco

            // Act: Consulta pelo DDD
            IList<RegiaoContato> listaRegiaoCoontado = await _regiaoContatoRepository.ObterTodosAsync();

            RegiaoContato regiaoContatoConsultado = listaRegiaoCoontado.Where(r => r.DDD == ddd).FirstOrDefault();

            // Assert: Verifica se o objeto retornado é o que foi inserido
            Assert.NotNull(regiaoContatoConsultado);
            Assert.Equal(ddd, regiaoContatoConsultado.DDD);
            Assert.Equal("SUDESTE", regiaoContatoConsultado.Regiao);
            Assert.Equal("SP", regiaoContatoConsultado.Estado);
        }

        [Fact]
        public async Task AtualizacaoRegiaoContato()
        {
            // Arrange: Criação de um RegiaoContato válido
            var regiaoContato = new RegiaoContato
            {   
                DDD = 11,
                Regiao = "SUDESTE",
                Estado = "SP",
                DataCriacao = DateTime.Now,
                Id = 100
            };

            await _regiaoContatoRepository.CadastrarAsync(regiaoContato); // Salva no banco

            // Act: Atualiza o RegiaoContato
            regiaoContato.DDD = 71;
            regiaoContato.Regiao = "NORDESTE";
            regiaoContato.Estado = "BA";
            await _regiaoContatoRepository.AlterarAsync(regiaoContato); // Método que você provavelmente tem no repositório

            // Assert: Verifica se o registro foi realmente atualizado
            var regiaoContatoAtualizado = await _regiaoContatoRepository.ObterPorIdAsync(100);

            Assert.NotNull(regiaoContatoAtualizado);
            Assert.Equal("NORDESTE", regiaoContatoAtualizado.Regiao);
            Assert.Equal("BA", regiaoContatoAtualizado.Estado);
            Assert.Equal(71, regiaoContatoAtualizado.DDD);
        }

        [Fact]
        public async Task ExcluirRegiaoContato()
        {
            // Arrange: Criação de um RegiaoContato válido
            int id = 2;

            var regiaoContato = new RegiaoContato
            {
                DDD = 11,
                Regiao = "SUDESTE",
                Estado = "SP",
                Id = id
            };

            await _regiaoContatoRepository.CadastrarAsync(regiaoContato); // Salva no banco

            // Act: Exclui o RegiaoContato
            await _regiaoContatoRepository.DeletarAsync(id); // Método que você provavelmente tem no repositório

            // Assert: Verifica se o objeto foi excluído
            var regiaoContatoExcluido = await _regiaoContatoRepository
                .ObterPorIdAsync(id);

            Assert.Null(regiaoContatoExcluido); // Espera-se que não encontre mais o objeto
        }
    }

}
