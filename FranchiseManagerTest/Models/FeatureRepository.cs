using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FranchiseManagerTest.Models
{ 
    public class FeatureRepository : IFeatureRepository
    {
        FranchiseManagerContext context = new FranchiseManagerContext();

        public IQueryable<Feature> All
        {
            get { return context.Features; }
        }

        public IQueryable<Feature> AllIncluding(params Expression<Func<Feature, object>>[] includeProperties)
        {
            IQueryable<Feature> query = context.Features;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Feature Find(int id)
        {
            return context.Features.Find(id);
        }

        public void InsertOrUpdate(Feature feature)
        {
            if (feature.FeatureID == default(int)) {
                // New entity
                context.Features.Add(feature);
            } else {
                // Existing entity
                context.Entry(feature).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var feature = context.Features.Find(id);
            context.Features.Remove(feature);
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

    public interface IFeatureRepository : IDisposable
    {
        IQueryable<Feature> All { get; }
        IQueryable<Feature> AllIncluding(params Expression<Func<Feature, object>>[] includeProperties);
        Feature Find(int id);
        void InsertOrUpdate(Feature feature);
        void Delete(int id);
        void Save();
    }
}