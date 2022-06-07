using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using N01520224_PassionProject_Reeservation.Models;

namespace N01520224_PassionProject_Reeservation.Controllers
{
    public class GuestDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ReservationData/ListGuests
        [HttpGet]
        [ResponseType(typeof(Guest))]
        public IHttpActionResult ListGuests()
        {
            IEnumerable<Guest> Guests = db.Guests.ToList();
            return Ok(Guests);
        }

        // POST: api/GuestData
        [ResponseType(typeof(Guest))]
        [HttpPost]
        public IHttpActionResult AddGuest(Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guests.Add(guest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = guest.GuestId }, guest);
        }

        // GET: api/GuestData
        public IQueryable<Guest> GetGuests()
        {
            return db.Guests;
        }

        // GET: api/GuestData/5
        [ResponseType(typeof(Guest))]
        public IHttpActionResult GetGuest(int id)
        {
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        // PUT: api/GuestData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuest(int id, Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guest.GuestId)
            {
                return BadRequest();
            }

            db.Entry(guest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
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

        // POST: api/GuestData
        [ResponseType(typeof(Guest))]
        public IHttpActionResult PostGuest(Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guests.Add(guest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = guest.GuestId }, guest);
        }

        // DELETE: api/GuestData/5
        [ResponseType(typeof(Guest))]
        public IHttpActionResult DeleteGuest(int id)
        {
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return NotFound();
            }

            db.Guests.Remove(guest);
            db.SaveChanges();

            return Ok(guest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestExists(int id)
        {
            return db.Guests.Count(e => e.GuestId == id) > 0;
        }
    }
}