using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using N01520224_PassionProject_Reeservation.Models;

namespace N01520224_PassionProject_Reeservation.Controllers
{
    public class ReservationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ReservationData/ListReservations
        [HttpGet]
        [ResponseType(typeof(ReservationDto))]
        public IHttpActionResult ListReservations()
        {
            List<Reservation> Reservations = db.Reservations.ToList();
            List<ReservationDto> ReservationDtos = new List<ReservationDto>();

            Reservations.ForEach(res => ReservationDtos.Add(new ReservationDto()
            {
                ReservationId = res.ReservationId,
                ReservationNumber = res.ReservationNumber,
                ReservationStatus = res.ReservationStatus,
                GuestName = res.Guest.FirstName + " " + res.Guest.LastName,
                UnitNumber = res.Unit.UnitNumber
            }));

            return Ok(ReservationDtos);
        }

        // GET: api/ReservationData/GetReservation/5
        [ResponseType(typeof(Reservation))]
        [HttpGet]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            ReservationDto reservationDto = new ReservationDto()
            {
                ReservationId = reservation.ReservationId,
                ReservationNumber = reservation.ReservationNumber,
                ReservationStatus = reservation.ReservationStatus,
                GuestName = reservation.Guest.FirstName + " " + reservation.Guest.LastName,
                UnitNumber = reservation.Unit.UnitNumber
            };
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservationDto);
        }

        // GET: api/ReservationData/FindReservation/5

        [ResponseType(typeof(ReservationDto))]
        [HttpGet]
        public IHttpActionResult FindReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            ReservationDto reservationDto = new ReservationDto()
            {
                ReservationId = reservation.ReservationId,
                ReservationNumber = reservation.ReservationNumber,
                ReservationStatus = reservation.ReservationStatus,
                GuestId = reservation.GuestId,
                GuestName = reservation.Guest.FirstName + " " + reservation.Guest.LastName,
                UnitId = reservation.UnitId,
                UnitNumber = reservation.Unit.UnitNumber
            };
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservationDto);
        }


        // GET: api/ReservationData/UpdateReservation/5

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.ReservationId)
            {

                return BadRequest();
            }

            db.Entry(reservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ReservationData/AddReservation
        [ResponseType(typeof(Reservation))]
        [HttpPost]
        public IHttpActionResult AddReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            int lastResId = db.Reservations.ToList().Last().ReservationId + 1;

            string newResNum = "R000" + lastResId; 

            reservation.ReservationNumber = newResNum;
            db.Reservations.Add(reservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservation.ReservationId }, reservation);
        }

        // DELETE: api/ReservationData/5
        [ResponseType(typeof(Reservation))]
        [HttpPost]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.ReservationId == id) > 0;
        }
    }
}