using Bogus;
using Core.Entity;
using Core.Input.Dto;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Moq;
using Service;

namespace FiapTechChallenge.Test
{
    public class ContatoServiceMetodos
    {
        #region ObterContatoPelaRegiaoAsync

        [Fact(DisplayName = "Retorna lista de Contato Dto")]
        [Trait("Contato", "Validando o método ObterContatoPelaRegiaoAsync")]
        public async Task ObterContatoPelaRegiaoAsyncPelaInterfaceRetornaListaContatoDto()
        {
            //arrange

            var fakerRegiaoContatoDto = new Faker<RegiaoContatoDto>()
                .CustomInstantiator(f =>
                {
                    return new RegiaoContatoDto()
                    {
                        DDD = 38,
                        Estado = "Minas Gerais",
                        Regiao = "Sudeste"
                    };
                });
            var fakerContatoDto = new Faker<ContatoDto>()
                .CustomInstantiator(f => new ContatoDto()
                {
                    Id = f.Random.Int(1, 100),
                    RegiaoContato = fakerRegiaoContatoDto,
                    Email = "email@teste.com",
                    Nome = "Nome Teste",
                    Telefone = 123456
                });

            var contato = new ContatoDto()
            {
                RegiaoContato = new RegiaoContatoDto()
                {
                    DDD = 41,
                    Estado = "Paraná",
                    Regiao = "Sul"
                },
                Email = "emailContato@teste.com",
                Nome = "Nome do Contato",
                Telefone = 123456
            };

            var listaRetornoContatoDto = new List<ContatoDto>();


            var lista = fakerContatoDto.Generate(200);
            lista.Add(contato);

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.ObterContatoPelaRegiaoAsync("Sul")).ReturnsAsync(lista);

            //act
            var retorno = await mockService.Object.ObterContatoPelaRegiaoAsync("Sul");

            //assert
            Assert.NotNull(retorno);
        }

        [Fact(DisplayName = "Retorna lista de Contato Dto")]
        [Trait("Contato", "Validando o método ObterContatoPelaRegiaoAsync")]
        public async Task ObterContatoPelaRegiaoAsyncDeveEncontrarUmRegiao()
        {
            // Arrange
            var regiao = "Sul";
            RegiaoContato regiaoContato = new RegiaoContato()
            {
                DDD = 41,
                Estado = "Paraná",
                Regiao = regiao
            };

            var fakerRegiaoContato = new Faker<RegiaoContato>()
                .CustomInstantiator(f =>
                {
                    return new RegiaoContato()
                    {
                        Id = f.Random.Int(1, 100),
                        DDD = 38,
                        Estado = "Minas Gerais",
                        Regiao = "Sudeste",
                        DataCriacao = DateTime.UtcNow
                    };
                });

            var listaRegiao = fakerRegiaoContato.Generate(200);
            listaRegiao.Add(regiaoContato);

            var fakerContato = new Faker<Contato>()
                .CustomInstantiator(f =>
                {
                    return new Contato()
                    {
                        Id = f.Random.Int(1, 100),
                        Nome = "Nome fake",
                        Email = "email@fake.com",
                        Telefone = 112321,
                        DataCriacao = DateTime.Now,
                        RegiaoContato = new RegiaoContato()
                        {
                            Id = f.Random.Int(1, 100),
                            DDD = 38,
                            Estado = "Minas Gerais",
                            Regiao = "Sudeste",
                            DataCriacao = DateTime.UtcNow
                        }
                    };
                });

            var listaContato = fakerContato.Generate(200);

            var contato = new Contato()
            {
                Id = 1,
                Nome = "Nome sem alteração",
                DataCriacao = DateTime.Now,
                Email = "email@teste.com",
                Telefone = 123465,
                RegiaoContato = new RegiaoContato()
                {
                    DDD = 11,
                    Estado = "São Paulo",
                    Regiao = "Sudeste"
                }
            };
            var contatoSul = new Contato()
            {
                Id = 1,
                Nome = "Nome sem alteração",
                DataCriacao = DateTime.Now,
                Email = "email@teste.com",
                Telefone = 123465,
                RegiaoContato = regiaoContato
            };

            listaContato.Add(contatoSul);
            listaContato.Add(contato);


            var mockContatoRepository = new Mock<IContatoRepository>();
            mockContatoRepository.Setup(repo => repo.ObterPorIdAsync(contato.Id)).ReturnsAsync(contato);
            mockContatoRepository.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(listaContato);

            var mockRegiaoRepository = new Mock<IRegiaoContatoRepository>();
            mockRegiaoRepository.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(listaRegiao);

            var mockRegiaoService = new Mock<IRegiaoContatoService>();
            mockRegiaoService.Setup(service => service.ObterTodosAsync()).ReturnsAsync(listaRegiao);


            var service = new ContatoService(mockContatoRepository.Object, mockRegiaoService.Object);

            // Act
            var resultado = await service.ObterContatoPelaRegiaoAsync(regiao);

            // Assert            
            Assert.NotEmpty(resultado);
            Assert.Equal(1, resultado.Count);
            Assert.Equal(regiao, resultado[0].RegiaoContato.Regiao);
            Assert.Equal(contatoSul.Nome, resultado[0].Nome);
        }

        [Fact(DisplayName = "Não Retorna lista de Contato Dto")]
        [Trait("Contato", "Validando o método ObterContatoPelaRegiaoAsync")]
        public async Task ObterContatoPelaRegiaoAsyncNaoDeveEncontrarUmaRegiaoGeraExcecao()
        {
            // Arrange            
            var regiaoNaoCadastrada = "Centro-Oeste";

            var fakerRegiaoContato = new Faker<RegiaoContato>()
                .CustomInstantiator(f =>
                {
                    return new RegiaoContato()
                    {
                        Id = f.Random.Int(1, 100),
                        DDD = 38,
                        Estado = "Minas Gerais",
                        Regiao = "Sudeste",
                        DataCriacao = DateTime.UtcNow
                    };
                });

            var lista = fakerRegiaoContato.Generate(200);

            var mockContatoRepository = new Mock<IContatoRepository>();

            var mockRegiaoRepository = new Mock<IRegiaoContatoRepository>();
            mockRegiaoRepository.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(lista);

            var mockRegiaoService = new Mock<IRegiaoContatoService>();
            mockRegiaoService.Setup(service => service.ObterTodosAsync()).ReturnsAsync(lista);

            var service = new ContatoService(mockContatoRepository.Object, mockRegiaoService.Object);

            // Act            
            var exception = await Assert.ThrowsAsync<Exception>(() => service.ObterContatoPelaRegiaoAsync(regiaoNaoCadastrada));

            // Assert
            Assert.Equal("Essa Região não existe", exception.Message);
        }

        #endregion End ObterContatoPelaRegiaoAsync

        #region ObterTodosContatoAsyncRetornaListaContatoDto

        [Fact(DisplayName = "Retorna lista de Contato Dto")]
        [Trait("Contato", "Validando o método ObterTodosContatoAsync")]
        public async Task ObterTodosContatoAsyncRetornaListaContatoDto()
        {
            //arrange

            var fakerRegiaoContatoDto = new Faker<RegiaoContatoDto>()
                .CustomInstantiator(f =>
                {
                    return new RegiaoContatoDto()
                    {
                        DDD = 38,
                        Estado = "Minas Gerais",
                        Regiao = "Sudeste"
                    };
                });
            var fakerContatoDto = new Faker<ContatoDto>()
                .CustomInstantiator(f => new ContatoDto()
                {
                    Id = f.Random.Int(1, 100),
                    RegiaoContato = fakerRegiaoContatoDto,
                    Email = "email@teste.com",
                    Nome = "Nome Teste",
                    Telefone = 123456
                });

            var listaRetornoContatoDto = fakerContatoDto.Generate(200);

            var mockService = new Mock<IContatoService>();
            mockService.Setup(service => service.ObterTodosContatoAsync()).ReturnsAsync(listaRetornoContatoDto);

            //act
            var retorno = await mockService.Object.ObterTodosContatoAsync();

            //assert
            Assert.NotNull(retorno);
        }

        #endregion End ObterTodosContatoAsyncRetornaListaContatoDto

        #region AlterarCadastraContatoAsync

        [Fact(DisplayName = "Retorna lista de Contato Dto")]
        [Trait("Contato", "Validando o método AlterarCadastraContatoAsync")]
        public async Task AlterarCadastraContatoAsync()
        {
            // Arrange
            var nome = "Alterar o nome";
            var contato = new Contato()
            {
                Id = 1,
                Nome = "Nome sem alteração",
                DataCriacao = DateTime.Now,
                Email = "email@teste.com",
                Telefone = 123465,
                RegiaoContato = new RegiaoContato()
                {
                    DDD = 11,
                    Estado = "São Paulo",
                    Regiao = "Sudeste"
                }
            };

            var mockContatoRepository = new Mock<IContatoRepository>();
            mockContatoRepository.Setup(service => service.ObterPorIdAsync(contato.Id)).ReturnsAsync(contato);
            var mockRegiaoService = new Mock<IRegiaoContatoService>();
            var service = new ContatoService(mockContatoRepository.Object, mockRegiaoService.Object);

            contato.Nome = nome;

            // Act
            await service.AlterarAsync(contato);

            // Assert
            mockContatoRepository.Verify(repo => repo.AlterarAsync(contato), Times.Once);
            Assert.Equal(nome, contato.Nome);
        }

        #endregion End AlterarCadastraContatoAsync

        #region Auxiliar

        #endregion

    }
}

