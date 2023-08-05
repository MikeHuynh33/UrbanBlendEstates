using CreativeCollab.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CreativeCollab.Controllers
{
    public class AgentDataController : ApiController
    {
        // Connect to local db
        private ApplicationDbContext db = new ApplicationDbContext();
        // Get all Property from database for client to view them.
        [HttpGet]
        [ResponseType(typeof(EstateAgentDTO))]
        public IHttpActionResult ListAgents()
        {
            List<EstateAgent> estateAgents = db.EstateAgents.ToList();
            List<EstateAgentDTO> estateAgentDTO = new List<EstateAgentDTO>();

            estateAgents.ForEach(agent =>
                estateAgentDTO.Add(new EstateAgentDTO()
                {
                    EstateAgentId = agent.EstateAgentId,
                    Name = agent.Name,
                    Email = agent.Email,
                    Phone = agent.Phone,
                    Role = agent.Role
                })); ;
            return Ok(estateAgentDTO);
        }

        [ResponseType(typeof(EstateAgentDTO))]
        [HttpGet]
        public IHttpActionResult FindAgent(int? id)
        {
            EstateAgent foundagent = db.EstateAgents.Find(id);

            EstateAgentDTO agentDTO = new EstateAgentDTO()
            {
                EstateAgentId = foundagent.EstateAgentId,
                Name = foundagent.Name,
                Email = foundagent.Email,
                Phone = foundagent.Phone,
                Role = foundagent.Role
            };
            if (foundagent == null)
            {
                return NotFound();

            }
            return Ok(agentDTO);
        }

        [ResponseType(typeof(EstateAgentDTO))]
        [HttpGet]
        public IHttpActionResult FindAgentAssociateWithProperty(int? id)
        {
            EstateAgent foundagent = db.EstateAgents.Include(a => a.Properties).SingleOrDefault(p => p.EstateAgentId == id); ;
            var properties = foundagent.Properties.ToList();
            EstateAgentDTO agentDTO = new EstateAgentDTO()
            {
                EstateAgentId = foundagent.EstateAgentId,
                Name = foundagent.Name,
                Email = foundagent.Email,
                Phone = foundagent.Phone,
                Role = foundagent.Role,
                Properties = properties.Select(property => new PropertyDetailDTO
                {
                    PropertyID = property.PropertyID,
                    PropertyType = property.PropertyType,
                    PropertyAddress = property.PropertyAddress,
                    PropertySize = property.PropertySize,
                    NumberOfBedrooms = property.NumberOfBedrooms,
                    NumberOfBathrooms = property.NumberOfBathrooms,
                    Amenities = property.Amenities,
                    PropertyPrice = property.PropertyPrice,
                    PropertyDescription = property.PropertyDescription,
                    PropertyStatus = property.PropertyStatus,
                    ListingDate = property.ListingDate,
                }).ToList()
            };
            if (foundagent == null)
            {
                return NotFound();

            }
            return Ok(agentDTO);
        }

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAgent(int id, EstateAgent agent)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agent.EstateAgentId)
            {

                return BadRequest();
            }

            db.Entry(agent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
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

        [HttpPost]
        [ResponseType(typeof(EstateAgent))]
        public IHttpActionResult AddNewAgent(EstateAgent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.EstateAgents.Add(agent);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = agent.EstateAgentId }, agent);
        }

        [HttpPost]
        [ResponseType(typeof(EstateAgent))]
        public IHttpActionResult DeleteAgent(int id)
        {
            EstateAgent agent = db.EstateAgents.Find(id);
            if (agent == null) { return NotFound(); }
            db.EstateAgents.Remove(agent);
            db.SaveChanges();
            return Ok();
        }

        private bool AgentExists(int id)
        {
            return db.EstateAgents.Count(e => e.EstateAgentId == id) > 0;
        }
    }
}
