using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FranchiseManagerTest.Models
{
    public class FranchiseSet
    {        
        [Key]
        public int FranchiseSetID { get; set; }
        public string FranchiseSetName { get; set; }
        public string MainAddress { get; set; }
        public string TimeZone { get; set; }       
        public virtual ICollection<Franchise> Franchises { get; set; }
    }
}