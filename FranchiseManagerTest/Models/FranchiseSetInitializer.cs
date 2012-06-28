using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using FranchiseManagerTest.Models;

namespace FranchiseManagerTest
{
    class SetInitializer : DropCreateDatabaseIfModelChanges<FranchiseManagerContext>
    {


        protected override void Seed(FranchiseManagerContext context)
        {
            var sets = new List<FranchiseSet> {  
  
                 new FranchiseSet { 
                             FranchiseSetName = "My First Franchise Set",   
                             MainAddress = "321 Main St.",
                             TimeZone = "Central"},
                             

                 new FranchiseSet { 
                             FranchiseSetName = "My Second Franchise Set",   
                             MainAddress = "1500 West Maple Ave.",
                             TimeZone = "Eastern"},
                             
  
                 new FranchiseSet { 
                             FranchiseSetName = "Yet a Third Franchise Set",   
                             MainAddress = "Suite 100, Rutherford Blvd.",
                             TimeZone = "Pacific"}

               
             };

            sets.ForEach(d => context.FranchiseSets.Add(d));



            var franchises = new List<Franchise>
            {
                new Franchise { 
                    FranchiseName = "My Cool Franchise",      
                    FranchiseNumber = "123456789", 
                    //SetID = 1

                    },

                 new Franchise { 
                    FranchiseName = "Another Awesome Franchise",      
                    FranchiseNumber = "987654321", 
                    //SetID = 1
                    },

                new Franchise { 
                    FranchiseName = "And Another Franchise",      
                    FranchiseNumber = "456324897", 
                    //SetID = 1
                    },

                new Franchise { 
                    FranchiseName = "More Franchisiness",      
                    FranchiseNumber = "789654321", 
                    //SetID = 2
                    },

                new Franchise { 
                    FranchiseName = "Another one",      
                    FranchiseNumber = "786543211", 
                    //SetID = 2
                    }
               
            };
            franchises.ForEach(s => context.Franchises.Add(s));
            


        }


    }
}
