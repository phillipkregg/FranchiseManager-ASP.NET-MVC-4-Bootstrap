using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FranchiseManagerTest.Models
{ 
    public class FranchiseSetRepository : IFranchiseSetRepository
    {
        FranchiseManagerContext context = new FranchiseManagerContext();

        public IQueryable<FranchiseSet> All
        {
            get { return context.FranchiseSets; }
        }

        public IQueryable<FranchiseSet> AllIncluding(params Expression<Func<FranchiseSet, object>>[] includeProperties)
        {
            IQueryable<FranchiseSet> query = context.FranchiseSets;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public FranchiseSet Find(int id)
        {
            return context.FranchiseSets.Find(id);
        }

        public void InsertOrUpdate(FranchiseSet franchiseset)
        {
            if (franchiseset.FranchiseSetID == default(int)) {
                // New entity
                context.FranchiseSets.Add(franchiseset);
            } else {
                // Existing entity
                context.Entry(franchiseset).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var franchiseset = context.FranchiseSets.Find(id);
            context.FranchiseSets.Remove(franchiseset);
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

    public interface IFranchiseSetRepository : IDisposable
    {
        IQueryable<FranchiseSet> All { get; }
        IQueryable<FranchiseSet> AllIncluding(params Expression<Func<FranchiseSet, object>>[] includeProperties);
        FranchiseSet Find(int id);
        void InsertOrUpdate(FranchiseSet franchiseset);
        void Delete(int id);
        void Save();
    }
}