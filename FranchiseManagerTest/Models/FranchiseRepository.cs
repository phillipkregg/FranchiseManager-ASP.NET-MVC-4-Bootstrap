using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FranchiseManagerTest.Models
{ 
    public class FranchiseRepository : IFranchiseRepository
    {
        FranchiseManagerContext context = new FranchiseManagerContext();

        public IQueryable<Franchise> All
        {
            get { return context.Franchises; }
        }

        public IQueryable<Franchise> AllIncluding(params Expression<Func<Franchise, object>>[] includeProperties)
        {
            IQueryable<Franchise> query = context.Franchises;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Franchise Find(int id)
        {
            return context.Franchises.Find(id);
        }

        public void InsertOrUpdate(Franchise franchise)
        {
            if (franchise.FranchiseID == default(int)) {
                // New entity
                context.Franchises.Add(franchise);
            } else {
                // Existing entity
                context.Entry(franchise).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var franchise = context.Franchises.Find(id);
            context.Franchises.Remove(franchise);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IFranchiseRepository : IDisposable
    {
        IQueryable<Franchise> All { get; }
        IQueryable<Franchise> AllIncluding(params Expression<Func<Franchise, object>>[] includeProperties);
        Franchise Find(int id);
        void InsertOrUpdate(Franchise franchise);
        void Delete(int id);
        void Save();
    }
}