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

        /// <summary>
        /// Returns all reservations in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all reservations in the database, including their associated species.
        /// </returns>
        /// <example>
        /// GET: api/ReservationData/ListReservations
        /// </example>
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

        // GET: api/ReservationData/ListReservationByGuestId/2
        [ResponseType(typeof(Reservation))]
        [HttpGet]
        public IHttpActionResult ListReservationByGuestId(int id)
        {
            List<Reservation> Reservations = db.Reservations.Where(r => r.GuestId == id).ToList();
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

        // GET: api/ReservationData/ListReservationByUnitId/2
        [ResponseType(typeof(Reservation))]
        [HttpGet]
        public IHttpActionResult ListReservationByUnitId(int id)
        {
            List<Reservation> Reservations = db.Reservations.Where(r => r.UnitId == id).ToList();
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

        /// <summary>
        /// Returns all reservations in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An reservation in the system matching up to the reservation ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the reservation</param>
        /// <example>
        /// GET: api/ReservationData/FindReservation/5
        /// </example>

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



        /// <summary>
        /// Updates a particular reservation in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the reservation ID primary key</param>
        /// <param name="reservation">JSON FORM DATA of an reservation</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ReservationData/UpdateReservation/5
        /// FORM DATA: reservation JSON Object
        /// </example>
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

        /// <summary>
        /// Adds an reservation to the system
        /// </summary>
        /// <param name="reservation">JSON FORM DATA of an reservation</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: reservation ID, reservation Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ReservationData/AddReservation
        /// FORM DATA: reservation JSON Object
        /// </example>
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