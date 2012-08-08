using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FranchiseManagerTest.Models
{
    public class FranchiseManagerContext : DbContext
    {
        // This constructor specifies the connection string name - needed for AppHarbor deploy
        public FranchiseManagerContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<FranchiseSet> FranchiseSets { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feature> Features { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feature>()
                .HasMany(f => f.FranchiseSets).WithMany(f => f.Features)
                .Map(t => t.MapLeftKey("FeatureID")
                    .MapRightKey("FranchiseSetID")
                    .ToTable("FranchiseSetFeatures"));
        }


    }
}