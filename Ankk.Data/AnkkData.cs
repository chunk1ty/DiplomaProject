namespace Ankk.Data
{
    using Ankk.Data.Repositories;
    using Ankk.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnkkData : IAnkkData
    {
        private IAnkkDbContext context;
        private IDictionary<Type, object> repositories;

        public AnkkData(IAnkkDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public AnkkData()
            : this(new AnkkDbContext())
        {

        }

        public IGenericRepository<Subject> Subjects
        {
            get { return this.GetRepository<Subject>(); }
        }

        public IGenericRepository<Score> Scores
        {
            get { return this.GetRepository<Score>(); }
        }

        public IGenericRepository<Contest> Contests
        {
            get { return this.GetRepository<Contest>(); }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
