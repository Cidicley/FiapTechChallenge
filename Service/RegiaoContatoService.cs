using Core.Entity;
using Core.Input.Dto;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Service
{
    public class RegiaoContatoService : IRegiaoContatoService
    {
        private readonly IRegiaoContatoRepository _regiaoContatoRepository;

        public RegiaoContatoService(IRegiaoContatoRepository regiaoContatoRepository)
        {
            _regiaoContatoRepository = regiaoContatoRepository;
        }

        public async Task CadastrarAsync(RegiaoContato entidade)
        {
            try
            {
                await _regiaoContatoRepository.CadastrarAsync(entidade);
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
                await _regiaoContatoRepository.DeletarAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task<RegiaoContato> ObterPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IList<RegiaoContato>> ObterTodosAsync()
        {
            return await _regiaoContatoRepository.ObterTodosAsync();
        }
        public async Task<RegiaoContato> RetornaIdPorDDD(int ddd)
        {
            try
            {
                return await _regiaoContatoRepository.RetornaIdPorDDD(ddd);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<RegiaoContatoDto>> ObterTodosDtoAsync()
        {
            try
            {
                var listaDto = new List<RegiaoContatoDto>();
                var listaRegiao = await this.ObterTodosAsync();

                foreach (var regiao in listaRegiao)
                {
                    listaDto.Add(new RegiaoContatoDto()
                    {
                        DDD = regiao.DDD,
                        Estado = regiao.Estado,
                        Regiao = regiao.Regiao
                    });
                };

                return listaDto.OrderBy(c => c.DDD).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task AlterarAsync(RegiaoContato entidade)
        {
            throw new NotImplementedException();
        }
    }
}
