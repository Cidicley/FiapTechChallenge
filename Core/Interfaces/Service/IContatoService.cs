using Core.Entity;
using Core.Input;
using Core.Input.Dto;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Service
{
    public interface IContatoService : IRepository<Contato>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CadastraContato(ContatoInput input);        
        /// <summary>
        /// Obtem os contatos pela região 
        /// </summary>
        /// <param name="regiao"></param>
        /// <returns></returns>
        Task<IList<ContatoDto>> ObterContatoPelaRegiaoAsync(string regiao);
        /// <summary>
        /// Obtem todos os contatos registrados
        /// </summary>
        /// <returns></returns>
        Task<IList<ContatoDto>> ObterTodosContatoAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AlterarCadastraContatoAsync(ContatoUpdateInput input);
    }
}
