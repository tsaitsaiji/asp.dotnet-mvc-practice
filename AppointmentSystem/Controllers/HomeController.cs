using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentSystem.Models;

namespace AppointmentSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBmanager dbmanager = new DBmanager();
            List<Reservation> reservations = dbmanager.GetReservations();
            ViewBag.reservations = reservations;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreatedReservation()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult CreatedReservation(Reservation reservation)
        {
            DBmanager dbmanager = new DBmanager();
            try
            {
                dbmanager.NewReservation(reservation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditReservation(int id)
        {
            DBmanager dBmanager = new DBmanager();
            Reservation reservation = dBmanager.GetReservationById(id);
            return View(reservation);
        }
        [HttpPost]
        public ActionResult EditReservation(Reservation reservation)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.UpdateReservation(reservation);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteReservation(int id)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.DeleteReservationByID(id);
            return RedirectToAction("Index");

        }




    }
}