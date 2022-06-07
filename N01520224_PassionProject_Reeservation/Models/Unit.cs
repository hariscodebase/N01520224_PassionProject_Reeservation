using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace N01520224_PassionProject_Reeservation.Models
{
    public class Unit
    {
        // unit details

        [Key]
        public int UnitId { get; set; }
        public int UnitNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}