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
using CreativeCollab.Models;
using System.Diagnostics;

namespace CreativeCollab.Controllers
{
    public class RestaurantDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RestaurantData/ListRestaurants
        [HttpGet]
        [ResponseType(typeof(RestaurantDto))]
        public IHttpActionResult ListRestaurants()
        {
            List<Restaurant> Restaurants = db.Restaurants.ToList();
            List<RestaurantDto> RestaurantDtos = new List<RestaurantDto>();

            Restaurants.ForEach(r => RestaurantDtos.Add(new RestaurantDto()
            {
                RestaurantId = r.RestaurantId,
                RestaurantName = r.RestaurantName,
                Description = r.Description,
                Address = r.Address,
                RestaurantLink = r.RestaurantLink,
                NeighbourhoodId = r.NeighbourhoodId,
                NeighbourhoodName = r.Neighbourhood.NeighbourhoodName
            }));

            return Ok(RestaurantDtos);
        }

        //Gathers information about all restaurants related to a particular Neighbourhood
        //GET: api/RestaurantData/ListRestaurantsForNeighbourhood/3
        [HttpGet]
        [ResponseType(typeof(RestaurantDto))]
        public IHttpActionResult ListRestaurantsForNeighbourhood(int id)
        {
            List<Restaurant> Restaurants = db.Restaurants.Where(r => r.NeighbourhoodId == id).ToList();
            List<RestaurantDto> RestaurantDtos = new List<RestaurantDto>();

            Restaurants.ForEach(r => RestaurantDtos.Add(new RestaurantDto()
            {
                RestaurantId = r.RestaurantId,
                RestaurantName = r.RestaurantName,
            }));
            return Ok(RestaurantDtos);
        }

        // GET: api/RestaurantData/FindRestaurant/5
        [ResponseType(typeof(RestaurantDto))]
        [HttpGet]
        public IHttpActionResult FindRestaurant(int id)
        {
            Restaurant Restaurant = db.Restaurants.Find(id);
            RestaurantDto RestaurantDto = new RestaurantDto()
            {
                RestaurantId = Restaurant.RestaurantId,
                RestaurantName = Restaurant.RestaurantName,
                Description = Restaurant.Description,
                Address = Restaurant.Address,
                RestaurantLink = Restaurant.RestaurantLink,
                NeighbourhoodId = Restaurant.NeighbourhoodId,
                NeighbourhoodName = Restaurant.Neighbourhood.NeighbourhoodName
            };

            if (Restaurant == null)
            {
                return NotFound();
            }

            return Ok(RestaurantDto);
        }

        // POST: api/RestaurantData/UpdateRestaurant/5
        // post when you want ned data manipulation language, and get when you just need to read
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRestaurant(int id, Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.RestaurantId)
            {
                return BadRequest();
            }

            db.Entry(restaurant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/RestaurantData/AddRestaurant
        [ResponseType(typeof(Restaurant))]
        [HttpPost]
        public IHttpActionResult AddRestaurant(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = restaurant.RestaurantId }, restaurant);
        }

        // POST: api/RestaurantData/DeleteRestaurant/5
        [ResponseType(typeof(Restaurant))]
        [HttpPost]
        public IHttpActionResult DeleteRestaurant(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(restaurant);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestaurantExists(int id)
        {
            return db.Restaurants.Count(e => e.RestaurantId == id) > 0;
        }
    }
}