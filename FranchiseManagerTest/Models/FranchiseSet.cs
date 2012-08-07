using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FranchiseManagerTest.Models
{
    public class FranchiseSet
    {        
        
        public int FranchiseSetID { get; set; }

        [Required(ErrorMessage = "Franchise Set name is required.")]
        public string FranchiseSetName { get; set; }

        [Required(ErrorMessage = "Main Address is required.")]
        public string MainAddress { get; set; }

        [Required(ErrorMessage = "Time Zone is required.")]
        public string TimeZone { get; set; }  
     
        public virtual ICollection<Franchise> Franchises { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Feature> Features { get; set; }

        //public FranchiseSet()
        //{
        //    Features = new HashSet<Feature>();
        //}

    }
}