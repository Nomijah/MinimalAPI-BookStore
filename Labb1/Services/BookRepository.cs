using AutoMapper;
using Labb1.Data;
using Labb1.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1.Services
{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookDbContext _context;
        private IMapper _mapper;
        public BookRepository(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Book> Update(Book entity)
        {
            
            var bookToUpdate = await _context.Books.FindAsync(entity.Id);
            _mapper.Map<Book, Book>(entity, bookToUpdate);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Book> Delete(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();
            return bookToDelete;
        }
    }
}
