
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public T? Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T Create(T entity)
        {
            if  (entity is null)
            {
                throw new ArgumentNullException("Dados inválidos");
            }

            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Dados inválidos");
            }

            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
