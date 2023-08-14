using CreativeCollab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CreativeCollab.Migrations;

namespace CreativeCollab.Controllers
{
    public class BookingDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        
        /// <summary>
        /// ListofBooking will handle GET Req , this function will retreive all booking data form booking table
        /// </summary>
        /// <returns>{ all booking in bookingtable }</returns>
        [HttpGet]
        [ResponseType(typeof(BookingDTO))]
        public IHttpActionResult ListOfBooking()
        {
            List<Booking> BookingDetails = db.Booking.ToList();
            List<BookingDTO> BookingDTO = new List<BookingDTO>();
            BookingDetails.ForEach(booking =>
               BookingDTO.Add(new BookingDTO()
               {
                    BookingId = booking.BookingId,
                    PropertyId = booking.PropertyId,
                    restaurant_id = booking.restaurant_id,
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    Phone =booking.Phone,
                    TimeDate = booking.TimeDate
        })); ;
            return Ok(BookingDTO);
        }

        /// <summary>
        /// CreateBooking will handle Post Request , it will create new booking appointment after client filed form , insert it into bookingtable
        /// </summary>
        /// <param name="newBooking">{List of Input from form}</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(BookingDTO))]
        public IHttpActionResult CreateBooking(Booking newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Booking.Add(newBooking);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = newBooking.BookingId }, newBooking);
        }
    }
}
