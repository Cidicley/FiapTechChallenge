using Core.Entity.Base;

namespace Core.Interfaces.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Obter todos os dados
        /// </summary>
        /// <returns></returns>
        //IList<T> ObterTodos();
        Task<IList<T>> ObterTodosAsync();
        /// <summary>
        /// Obter os dados por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> ObterPorIdAsync(int id);
        /// <summary>
        /// Cadastrar a entidade
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        Task CadastrarAsync(T entidade);
        /// <summary>
        /// Alterar a entidade
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        Task AlterarAsync(T entidade);
        /// <summary>
        /// Deletar a entidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletarAsync(int id);
    }
}
