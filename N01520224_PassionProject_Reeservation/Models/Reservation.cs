using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace N01520224_PassionProject_Reeservation.Models
{
    public class Reservation
    {
        //reservation details

        [Key]
        public int ReservationId { get; set; }
        public string ReservationNumber { get; set; }
        public string ReservationStatus { get; set; }


        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }

        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

    }

    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public string ReservationNumber { get; set; }
        public string ReservationStatus { get; set; }
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public int UnitId { get; set; }

        public int UnitNumber { get; set; }
    }
}