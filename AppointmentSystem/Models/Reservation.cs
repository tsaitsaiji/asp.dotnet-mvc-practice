using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentSystem.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public string NAME { get; set;}
        public string PHONE { get; set; }
        public DateTime DATE { get; set; }
    }
}