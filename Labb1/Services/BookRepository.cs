using Labb1.Data;
using Labb1.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1.Services
{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookDbContext _context;
        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<Book> GetById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Book> Create(Book entity)
        {
            if (entity != null)
            {
                await _context.Books.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }
        public async Task<Book> Update(Book entity)
        {
            if (entity != null)
            {
                var bookToUpdate = await _context.Books.FirstOrDefaultAsync(b => b.Id == entity.Id);
                if (bookToUpdate != null)
                {
                    bookToUpdate = entity;
                    await _context.SaveChangesAsync();
                    return bookToUpdate;
                }
                return null;
            }
            return null;
        }
        public async Task<Book> Delete(int id)
        {
            var bookToDelete = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if ( bookToDelete != null )
            {
                _context.Books.Remove(bookToDelete);
                await _context.SaveChangesAsync();
                return bookToDelete;
            }
            return null;
        }
    }
}
