using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FranchiseManagerTest.Models
{
    public class Feature
    {
        public int FeatureID { get; set; }
        
        public string FeatureName { get; set; }        
        
        public virtual ICollection<FranchiseSet> FranchiseSets { get; set; }

        
    }
}