using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FranchiseManagerTest.Models
{
    public class FranchiseManagerContext : DbContext
    {
        public FranchiseManagerContext() : base("DefaultConnection")
        {

        }
        public DbSet<FranchiseSet> FranchiseSets { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feature> Features { get; set; }


    }
}