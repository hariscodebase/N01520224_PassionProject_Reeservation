using N01520224_PassionProject_Reeservation.Models;
using N01520224_PassionProject_Reeservation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace N01520224_PassionProject_Reeservation.Controllers
{
    public class ReservationController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ReservationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44365/api/");
        }

        // GET: Reservation/List
        public ActionResult List()
        {
            //Objective: retrieve the list of reservations
            string url = "ReservationData/ListReservations";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);
            IEnumerable<ReservationDto> reservations = response.Content.ReadAsAsync<IEnumerable<ReservationDto>>().Result;
            return View(reservations);
        }

        // GET: Animal/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateReservation ViewModel = new UpdateReservation();

            //the existing animal information
            string url = "ReservationData/FindReservation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ReservationDto SelectedReservation = response.Content.ReadAsAsync<ReservationDto>().Result;
            ViewModel.SelectedReservation = SelectedReservation;

            // all species to choose from when updating this animal
            //the existing animal information
            string guestUrl = "GuestData/ListGuests";
            response = client.GetAsync(guestUrl).Result;
            IEnumerable<Guest> guests = response.Content.ReadAsAsync<IEnumerable<Guest>>().Result;
            ViewModel.Guests = guests;

            string unitUrl = "UnitData/ListUnits";
            response = client.GetAsync(unitUrl).Result;
            IEnumerable<Unit> units = response.Content.ReadAsAsync<IEnumerable<Unit>>().Result;
            ViewModel.Units = units;

            return View(ViewModel);
        }

        // POST: Animal/Update/5
        [HttpPost]
        public ActionResult Update(int id, Reservation reservation)
        {

            string url = "ReservationData/UpdateReservation/" + id;
            string jsonpayload = jss.Serialize(reservation);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Reservation/New
        public ActionResult New()
        {
            //Objective: send the required data for dropdowns to create reservation
            DetailsNewReservation ViewModel = new DetailsNewReservation();
            string guestUrl = "GuestData/ListGuests";
            HttpResponseMessage response = client.GetAsync(guestUrl).Result;
            IEnumerable<Guest> guests = response.Content.ReadAsAsync<IEnumerable<Guest>>().Result;
            ViewModel.Guests = guests;

            string unitUrl = "UnitData/ListUnits";
            response = client.GetAsync(unitUrl).Result;
            IEnumerable<Unit> units = response.Content.ReadAsAsync<IEnumerable<Unit>>().Result;
            ViewModel.Units = units;
            return View(ViewModel);
        }

        // POST: Reservation/Create
        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(animal.AnimalName);
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/animaldata/addanimal 
            string url = "reservationdata/addreservation";


            string jsonpayload = jss.Serialize(reservation);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        

        // POST: Reservation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
