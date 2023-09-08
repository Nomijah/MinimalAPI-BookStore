namespace Labb1.Services
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetByTitle(string title);
        public Task<IEnumerable<T>> GetByAuthor(string author);
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(int id);
    }
}
