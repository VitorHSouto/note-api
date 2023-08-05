namespace note_api.Domain.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<List<T>> ListAll();
        public Task<T?> GetById(Guid Id);
        public Task<T?> Save(T entity);
        public Task<T?> Update(T entity);
        public Task<bool> Delete(Guid id);
    }
}
