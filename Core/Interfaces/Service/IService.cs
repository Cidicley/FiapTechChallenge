using Core.Entity.Base;

namespace Core.Interfaces.Service
{
    public interface IService<T> where T : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<T>> ObterTodosAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> ObterPorIdAsync(int id);        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        Task AlterarAsync(T entidade);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeletarAsync(int id);
    }
}
