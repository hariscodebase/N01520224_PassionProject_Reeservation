using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace N01520224_PassionProject_Reeservation.Models
{
    public class Guest
    {
        //guest details
        [Key]
        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }

    }

}