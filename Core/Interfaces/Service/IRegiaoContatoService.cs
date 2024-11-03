using Core.Entity;
using Core.Input.Dto;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Service
{
    public interface IRegiaoContatoService : IRepository<RegiaoContato>
    {
        /// <summary>
        /// Retorna região por DDD
        /// </summary>
        /// <param name="ddd"></param>
        /// <returns></returns>
        Task<RegiaoContato> RetornaIdPorDDD(int ddd);
        /// <summary>
        /// Busca uma lista de região contato
        /// </summary>
        /// <returns></returns>
        Task<List<RegiaoContatoDto>> ObterTodosDtoAsync();
    }
}
