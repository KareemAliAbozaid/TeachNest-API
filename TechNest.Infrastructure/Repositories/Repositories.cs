using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechNest.Domain.Interfaces;
using TechNest.Domain.Services;
using TechNest.Infrastructure.Data;

namespace TechNest.Infrastructure.Repositories
{
    public class Repositories<T> : IRepositories<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public Repositories(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate,params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.AsNoTracking().ToListAsync();
        }


        public Task<T?> GetByIdAsync(int id)
        {
            var entity = _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            return entity.AsTask();
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var entity = await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T? entity =await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
