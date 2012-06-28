﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FranchiseManagerTest.Models
{
    public class Franchise
    {
        [Key]
        public int FranchiseID { get; set; }
        [ForeignKey("FranchiseSet")]
        public int FranchiseSetId { get; set; }
        public string FranchiseName { get; set; }
        public string FranchiseNumber { get; set; }        
        
        public virtual FranchiseSet FranchiseSet { get; set; }

    }
}
