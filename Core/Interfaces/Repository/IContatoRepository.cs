using Core.Entity;
using Core.Input.Dto;

namespace Core.Interfaces.Repository
{
    public interface IContatoRepository : IRepository<Contato>
    {
        ///// <summary>
        ///// Obtem os contatos pela região 
        ///// </summary>
        ///// <param name="regiao"></param>
        ///// <returns></returns>
        //IList<ContatoDto> ObterContatoPelaRegiao(string regiao);
    }
}
