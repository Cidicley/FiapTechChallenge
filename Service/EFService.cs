using Core.Entity.Base;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Infrastructure.Repository
{
    public class EFService<T> : IService<T> where T : EntityBase
    {   
        private readonly IRepository<T> _repository;

        public EFService(IRepository<T> repository)
        {   
            _repository = repository;
        }

        public async Task AlterarAsync(T entidade)
        {
            await _repository.AlterarAsync(entidade);
        }

        //public void Cadastrar(T entidade)
        //{
        //    _repository.Cadastrar(entidade);
        //}

        public void DeletarAsync(int id)
        {   
            _repository.DeletarAsync(id);
        }
        
        public async Task<T> ObterPorIdAsync(int id)
            => await _repository.ObterPorIdAsync(id);

        //public IList<T> ObterTodos()
        //    => _repository.ObterTodos();

        public async Task<IList<T>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }
    }

}
