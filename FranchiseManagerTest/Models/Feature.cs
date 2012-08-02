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
        [ForeignKey("FranchiseSet")]
        public int FranchiseSetId { get; set; }
        public string FeatureName { get; set; }
        public bool IsChecked { get; set; }
        
        public virtual FranchiseSet FranchiseSet { get; set; }
    }
}