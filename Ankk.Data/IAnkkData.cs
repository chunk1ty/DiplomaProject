namespace Ankk.Data
{
    using Ankk.Data.Repositories;
    using Ankk.Models;

    public interface IAnkkData
    {
        IGenericRepository<Subject> Subjects { get; }

        IGenericRepository<Score> Scores { get; }

        IGenericRepository<Contest> Contests { get; }

        void SaveChanges();
    }
}
