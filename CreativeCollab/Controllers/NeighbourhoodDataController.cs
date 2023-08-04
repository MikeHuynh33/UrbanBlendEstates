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

namespace CreativeCollab.Controllers
{
    public class NeighbourhoodDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NeighbourhoodData/ListNeighbourhoods
        [HttpGet]
        [ResponseType(typeof(NeighbourhoodDto))]
        public IEnumerable<NeighbourhoodDto> ListNeighbourhoods()
        {
            List<Neighbourhood> Neighbourhoods = db.Neighbourhoods.ToList();
            List<NeighbourhoodDto> NeighbourhoodDtos = new List<NeighbourhoodDto>();

            Neighbourhoods.ForEach(n => NeighbourhoodDtos.Add(new NeighbourhoodDto()
            {
                NeighbourhoodId = n.NeighbourhoodId,
                NeighbourhoodName = n.NeighbourhoodName

            }));

            return NeighbourhoodDtos;
        }

        // GET: api/NeighbourhoodData/FindNeighbourhood/5
        [ResponseType(typeof(NeighbourhoodDto))]
        [HttpGet]
        public IHttpActionResult FindNeighbourhood(int id)
        {
            Neighbourhood Neighbourhood = db.Neighbourhoods.Find(id);
            NeighbourhoodDto NeighbourhoodDto = new NeighbourhoodDto()
            {
                NeighbourhoodId = Neighbourhood.NeighbourhoodId,
                NeighbourhoodName = Neighbourhood.NeighbourhoodName
            };

            if (Neighbourhood == null)
            {
                return NotFound();
            }

            return Ok(NeighbourhoodDto);
        }

        // POST: api/NeighbourhoodData/UpdateNeighbourhood/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateNeighbourhood(int id, Neighbourhood neighbourhood)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != neighbourhood.NeighbourhoodId)
            {
                return BadRequest();
            }

            db.Entry(neighbourhood).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NeighbourhoodExists(id))
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

        // POST: api/NeighbourhoodData/AddNeighbourhood
        [ResponseType(typeof(Neighbourhood))]
        [HttpPost]
        public IHttpActionResult AddNeighbourhood(Neighbourhood neighbourhood)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Neighbourhoods.Add(neighbourhood);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = neighbourhood.NeighbourhoodId }, neighbourhood);
        }

        // POST: api/NeighbourhoodData/DeleteNeighbourhood/5
        [ResponseType(typeof(Neighbourhood))]
        [HttpPost]
        public IHttpActionResult DeleteNeighbourhood(int id)
        {
            Neighbourhood neighbourhood = db.Neighbourhoods.Find(id);
            if (neighbourhood == null)
            {
                return NotFound();
            }

            db.Neighbourhoods.Remove(neighbourhood);
            db.SaveChanges();

            return Ok(neighbourhood);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NeighbourhoodExists(int id)
        {
            return db.Neighbourhoods.Count(e => e.NeighbourhoodId == id) > 0;
        }
    }
}