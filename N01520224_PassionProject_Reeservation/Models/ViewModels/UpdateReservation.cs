using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N01520224_PassionProject_Reeservation.Models.ViewModels
{
    public class UpdateReservation
    {
        public ReservationDto SelectedReservation { get; set; }
        public IEnumerable<Guest> Guests { get; set; }
        public IEnumerable<Unit> Units { get; set; }
    }
}