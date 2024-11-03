using Core.Entity;
using Core.Input.Dto;
using Core.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class ContatoRepository : EFRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ApplicationDbContext context) : base(context)
        {
        }

        //IList<ContatoDto> IContatoRepository.ObterContatoPelaRegiao(string regiao)
        //{
        //    //Lazy Loading
        //    var contatoRegiao = _context.RegiaoContato
        //        .FirstOrDefault(c => c.Regiao.Equals(regiao))
        //        ?? throw new Exception("Essa Região não existe");

        //    List<ContatoDto> contatoDto = new List<ContatoDto>();

        //    var contatos = _context.Contato.ToList().Where(c => c.RegiaoContato.Regiao.Equals(contatoRegiao.Regiao));

        //    foreach (var contato in contatos)
        //    {
        //        contatoDto.Add(new ContatoDto()
        //        {
        //            Nome = contato.Nome,
        //            Telefone = contato.Telefone,
        //            RegiaoContato = new RegiaoContatoDto()
        //            {
        //                DDD = contato.RegiaoContato.DDD,
        //                Regiao = contato.RegiaoContato.Regiao,
        //                Estado = contato.RegiaoContato.Estado
        //            },
        //            Email = contato.Email
        //        });
        //    }

        //    return contatoDto;
        //}
    }
}
