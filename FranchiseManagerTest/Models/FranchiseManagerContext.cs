using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FranchiseManagerTest.Models
{
    public class FranchiseManagerContext : DbContext
    {
        public DbSet<FranchiseSet> FranchiseSets { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

    }
}