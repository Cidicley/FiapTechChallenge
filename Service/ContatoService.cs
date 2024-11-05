using Core.Entity;
using Core.Input;
using Core.Input.Dto;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Service
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IRegiaoContatoService _regiaoService;

        public ContatoService(IContatoRepository contatoRepository, IRegiaoContatoService regiaoService)
        {
            _contatoRepository = contatoRepository;
            _regiaoService = regiaoService;
        }

        public async Task<IList<Contato>> ObterTodosAsync()
        {
            try
            {
                //Exemplo de Lazy Loading
                return await _contatoRepository.ObterTodosAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Contato> ObterPorIdAsync(int id)
        {
            try
            {
                return await _contatoRepository.ObterPorIdAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IList<ContatoDto>> ObterContatoPelaRegiaoAsync(string regiao)
        {
            try
            {
                regiao = regiao.ToLower();
                
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                regiao = textInfo.ToTitleCase(regiao);

                //Lazy Loading
                var todasRegioes = await _regiaoService.ObterTodosAsync();
                var resultadoRegiao = todasRegioes.Where(c => c.Regiao.Equals(regiao)).FirstOrDefault() ?? throw new Exception("Essa Região não existe");

                List<ContatoDto> contatoDto = new List<ContatoDto>();
                var contatos = await _contatoRepository.ObterTodosAsync();

                if (contatos != null)
                {
                    foreach (var contato in contatos.Where(c => c.RegiaoContato.Regiao.Equals(resultadoRegiao.Regiao)))
                    {
                        contatoDto.Add(new ContatoDto()
                        {
                            Id = contato.Id,
                            Nome = contato.Nome,
                            Telefone = contato.Telefone,
                            RegiaoContato = new RegiaoContatoDto()
                            {
                                DDD = contato.RegiaoContato.DDD,
                                Regiao = contato.RegiaoContato.Regiao,
                                Estado = contato.RegiaoContato.Estado
                            },
                            Email = contato.Email
                        });
                    }
                }

                return contatoDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IList<ContatoDto>> ObterTodosContatoAsync()
        {
            try
            {
                //Exemplo de Lazy Loading

                var contatosDto = new List<ContatoDto>();
                var contatos = await _contatoRepository.ObterTodosAsync();

                foreach (var contato in contatos)
                {
                    contatosDto.Add(new ContatoDto()
                    {
                        Id = contato.Id,
                        //DataCriacao = contato.DataCriacao,
                        Nome = contato.Nome,
                        Email = contato.Email,
                        Telefone = contato.Telefone,
                        RegiaoContato = new RegiaoContatoDto()
                        {
                            DDD = contato.RegiaoContato.DDD,
                            Estado = contato.RegiaoContato.Estado,
                            Regiao = contato.RegiaoContato.Regiao
                        }
                    });
                }

                return contatosDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task CadastraContato(ContatoInput input)
        {
            try
            {
                RegiaoContato regiaoContato = await _regiaoService.RetornaIdPorDDD(input.DDD);

                var contato = new Contato()
                {
                    Nome = input.Nome,
                    RegiaoContato = regiaoContato,
                    Email = input.Email,
                    Telefone = int.Parse(input.Telefone)
                };
                await this.CadastrarAsync(contato);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task CadastrarAsync(Contato entidade)
        {
            try
            {
                await _contatoRepository.CadastrarAsync(entidade);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task AlterarAsync(Contato entidade)
        {
            try
            {
                var contato = await this.ObterPorIdAsync(entidade.Id);
                contato.Nome = entidade.Nome;

                await _contatoRepository.AlterarAsync(contato);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task AlterarCadastraContatoAsync(ContatoUpdateInput input)
        {
            try
            {
                var contato = await _contatoRepository.ObterPorIdAsync(input.Id);
                contato.Nome = input.Nome;

                await _contatoRepository.AlterarAsync(contato);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }       
        public async Task DeletarAsync(int id)
        {
            try
            {
                await _contatoRepository.DeletarAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
