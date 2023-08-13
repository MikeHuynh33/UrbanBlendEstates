using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int PropertyId { get; set; }
        public int restaurant_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime TimeDate { get; set; }
    }

    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int PropertyId { get; set; }
        public int restaurant_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime TimeDate { get; set; }
    }
}