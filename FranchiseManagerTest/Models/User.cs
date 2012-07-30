using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FranchiseManagerTest.Models
{
    public class User
    {
        public int UserID { get; set; }
        [ForeignKey("FranchiseSet")]
        public int FranchiseSetId { get; set; }
        public string UserName { get; set; }
        public string UserNumber { get; set; }

        public virtual FranchiseSet FranchiseSet { get; set; }


    }
}