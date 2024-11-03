using Core.Entity;

namespace Core.Interfaces.Repository
{
    public interface IRegiaoContatoRepository : IRepository<RegiaoContato>
    {        
        /// <summary>
        /// Retorna a região por DDD
        /// </summary>
        /// <param name="ddd"></param>
        /// <returns></returns>
        Task<RegiaoContato> RetornaIdPorDDD(int ddd);
    }
}
