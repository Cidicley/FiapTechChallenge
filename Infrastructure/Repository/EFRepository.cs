using Core.Entity.Base;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EFRepository<T> : IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AlterarAsync(T entidade)
        {
            _context.Set<T>().Update(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task CadastrarAsync(T entidade)
        {
            entidade.DataCriacao = DateTime.Now;
            _context.Set<T>().Add(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            _context.Set<T>().Remove(await this.ObterPorIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<T> ObterPorIdAsync(int id)
            => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IList<T>> ObterTodosAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }

}
