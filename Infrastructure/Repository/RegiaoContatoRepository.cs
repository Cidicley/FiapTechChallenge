using Core.Entity;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RegiaoContatoRepository : EFRepository<RegiaoContato>, IRegiaoContatoRepository
    {
        public RegiaoContatoRepository(ApplicationDbContext context) : base(context)
        {
        }        

        public async Task<RegiaoContato> RetornaIdPorDDD(int ddd)
        {
            //Lazy Loading
            var  contatoRegiao = await _context.RegiaoContato
                .FirstOrDefaultAsync(c => c.DDD.Equals(ddd))
                ?? throw new Exception("Esse DDD não existe");

            return contatoRegiao;
        }
    }
}
