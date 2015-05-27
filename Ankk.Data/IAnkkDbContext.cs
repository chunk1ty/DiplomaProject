﻿namespace Ankk.Data
{
    using Ankk.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IAnkkDbContext
    {
        IDbSet<Subject> Subjects { get; set; }

        IDbSet<Score> Scores { get; set; }

        IDbSet<Contest> Contests { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}