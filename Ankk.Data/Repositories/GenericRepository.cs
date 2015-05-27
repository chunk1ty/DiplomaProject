namespace Ankk.Data.Repositories
{
    using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IAnkkDbContext context;
        private IDbSet<T> set;

        public GenericRepository(IAnkkDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.set.AsQueryable<T>();
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions);
        }

        public void Add(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Detach(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Detached;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            return entry;
        }
    }
}
