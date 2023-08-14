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
        /// <summary>
        /// ListAgents will handle GET REQUEST to retrieve all Agents from EstateAgent Table.
        /// </summary>
        /// <returns>
        ///     {List of all Agents data from EstateAgent Table}
        /// </returns>
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
        /// <summary>
        /// FindAgent will handle GET REQUEST with ID params , the function will look up in EstateAgents to find sepecific ID of AGENTS
        /// </summary>
        /// <param name="id">1</param>
        /// <returns>
        ///     Specific Agents that we look for.
        /// </returns>
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
        /// <summary>
        /// FindAgentAssociateWithProperty will handle Get Request with id params, the function will find associated properties that under
        /// Agent's management by calling bridge table.
        /// </summary>
        /// <param name="id">2</param>
        /// <returns>{
        ///     The list of all properties that has link between 2 table 
        /// }
        /// </returns>
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
        /// <summary>
        /// UpdateAgent will handle Post request with JSON data from FORM and Id params. to update specific Agents (ID) and replace old data to new data.
        /// </summary>
        /// <param name="id">1</param>
        /// <param name="agent">{ List of Inputs data from FORM }</param>
        /// <returns></returns>
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
        /// <summary>
        /// AddNewAgent will handle POST REQ with JSON data , it will insert new agent based on REQ(DATA)
        /// </summary>
        /// <param name="agent">{ list of input from FORM }</param>
        /// <returns></returns>
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
        /// <summary>
        /// DeleteAgent will handle Post Req with ID params , it will delete agents with agentID matching params.
        /// </summary>
        /// <param name="id">"1"</param>
        /// <returns></returns>
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
        /// <summary>
        /// AgentExists will handle to genegrate hightest ID
        /// </summary>
        /// <param name="id">1</param>
        /// <returns>hightest ID</returns>
        private bool AgentExists(int id)
        {
            return db.EstateAgents.Count(e => e.EstateAgentId == id) > 0;
        }
    }
}
