using CreativeCollab.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class AdminController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44372/api/");
        }

        [HttpGet]
        // GET: /Admin
        [Route("/Admin")]
        public ActionResult Index()
        {
           
            string url = "AgentData/ListAgents";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<EstateAgentDTO> agents = response.Content.ReadAsAsync<IEnumerable<EstateAgentDTO>>().Result;

            string sec_url = "PropertyData/ListOfProperties";
            HttpResponseMessage sec_response = client.GetAsync(sec_url).Result;
            IEnumerable<PropertyDetailDTO> properties = sec_response.Content.ReadAsAsync<IEnumerable<PropertyDetailDTO>>().Result;

            string third_url = "restaurantdata/listrestaurants";
            HttpResponseMessage third_response = client.GetAsync(third_url).Result;
            IEnumerable<RestaurantDto> restaurants = third_response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            string fourth_url = "BookingData/ListOfBooking";
            HttpResponseMessage fourth_response = client.GetAsync(fourth_url).Result;
            IEnumerable<BookingDTO> booking = fourth_response.Content.ReadAsAsync<IEnumerable<BookingDTO>>().Result;
            AgentsAndPropertiesViewModel viewModel = new AgentsAndPropertiesViewModel //added restaurants but don't want to rename
            {
                Agents = agents,
                Properties = properties,
                Restaurants = restaurants,
                Bookings =booking
            };
            return View(viewModel);
        }

        //GET /AddNewProperty Create Form View.  
        [HttpGet]
        [Route("/Admin/AddNewProperty")]
        public ActionResult AddNewProperty()
        {
            string url = "neighbourhooddata/listneighbourhoods";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<NeighbourhoodDto> NeighbourhoodOptions = response.Content.ReadAsAsync<IEnumerable<NeighbourhoodDto>>().Result;

            return View(NeighbourhoodOptions);

        }


        //GET /Admin/Property/Id
        [HttpGet]
        [Route("/Admin/PropertyDetail/{id}")]
        public ActionResult PropertyDetail(int id)
        {

            string url = "PropertyData/FindPropertyAssociateWithAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyDetailDTO foundproperty = response.Content.ReadAsAsync<PropertyDetailDTO>().Result;

            string sub_url = "AgentData/ListAgents";
            HttpResponseMessage sec_response = client.GetAsync(sub_url).Result;
            IEnumerable<EstateAgentDTO> agents = sec_response.Content.ReadAsAsync<IEnumerable<EstateAgentDTO>>().Result;

            PropertyDetailAndAllAgentsViewModel PropertyAndAgent = new PropertyDetailAndAllAgentsViewModel
            {
                Properties = foundproperty,
                AllAgents = agents
            };
            return View(PropertyAndAgent);
        }

        //POST: Admin Adding new Property
        [HttpPost]
        [Route("/Admin/NewProperty")]
        public ActionResult NewProperty(PropertyDetail property)
        {
            Debug.WriteLine("the json payload is :");

            string url = "PropertyData/AddNewProperty";
            List<string> imageFileNames = new List<string>();
            try
            {
                List<HttpPostedFileBase> ImageFiles = new List<HttpPostedFileBase>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    ImageFiles.Add(file);
                }

                foreach (HttpPostedFileBase file in ImageFiles)
                {
                    // Save the image file to location
                    string filename = Path.GetFileName(file.FileName);
                    string path = Server.MapPath("~/Uploads/") + filename;
                    file.SaveAs(path);
                    imageFileNames.Add(filename);
                    Debug.WriteLine("File path: " + path);
                    Debug.WriteLine(file.ContentLength > 0);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred during file saving: " + ex.Message);

                return RedirectToAction("Error");
            }


            // Assign the image file names to the property object
            property.ImageFileNames = string.Join(",", imageFileNames);


            string jsonpayload = jss.Serialize(property);
            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("../Admin");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //Get :  Edit property
        [Route("Admin/PropertyEdit/{id}")]
        [HttpGet]
        public ActionResult PropertyEdit(int id)
        {
            string url = "PropertyData/FindProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyDetailDTO SelectedProperty = response.Content.ReadAsAsync<PropertyDetailDTO>().Result;
            return View(SelectedProperty);
        }

        //Post : Update Property after submit.
        [HttpPost]
        [Route("Admin/PropertyUpdate/{id}")]
        public ActionResult PropertyUpdate(int id, PropertyDetail property, List<string> ExistingImageFileNames)
        {
            List<string> imageFileNames = new List<string>();
            Debug.WriteLine(Request.Files.Count);
            if (Request.Files.Count > 1)
            {
                try
                {
                    List<HttpPostedFileBase> ImageFiles = new List<HttpPostedFileBase>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        ImageFiles.Add(file);
                    }
                    if (ImageFiles.Count > 0)
                    {
                        foreach (HttpPostedFileBase file in ImageFiles)
                        {
                            // Save the image file to location
                            string filename = Path.GetFileName(file.FileName);
                            string path = Server.MapPath("~/Uploads/") + filename;
                            file.SaveAs(path);
                            imageFileNames.Add(filename);
                            Debug.WriteLine("File path: " + path);
                            Debug.WriteLine(file.ContentLength > 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("An error occurred during file saving: " + ex.Message);

                    return RedirectToAction("Error");
                }
            }

            // Assign the image file names to the property object.
            if (imageFileNames != null && imageFileNames.Any())
            {
                property.ImageFileNames = string.Join(",", imageFileNames);
            }
            else
            {
                property.ImageFileNames = string.Join(",", ExistingImageFileNames);
            }


            string url = "PropertyData/UpdateProperty/" + id;
            string jsonpayload = jss.Serialize(property);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("Admin/DeleteConfirm/{id}")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "PropertyData/FindProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyDetailDTO SelectedProperty = response.Content.ReadAsAsync<PropertyDetailDTO>().Result;
            return View(SelectedProperty);
        }

        [HttpPost]
        [Route("Admin/DeleteProperty/{id}")]
        public ActionResult DeleteProperty(int id)
        {
            string url = "PropertyData/DeleteProperty/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //POST /Admin/PropertyUpdateAgents Updating New List of Agents.
        [HttpPost]
        [Route("/Admin/PropertyUpdateAgents")]
        public ActionResult PropertyUpdateAgents(int propertyID, string[] agentSelected)
        {
            var payload = new
            {
                PropertyID = propertyID,
                AgentSelected = agentSelected
            };
            string url = "PropertyData/PropertyUpdateAgents";
            string jsonpayload = jss.Serialize(payload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("/PropertyDetail/" + propertyID);
            }
            else
            {
                return RedirectToAction("Error");
            }

        }


        //GET /AddNewAgent Create Form View.  
        [HttpGet]
        [Route("/Admin/AddNewAgent")]
        public ActionResult AddNewAgent()
        {
            return View();
        }

        //GET /Admin/Property/Id
        [HttpGet]
        [Route("/Admin/AgentDetail/{id}")]
        public ActionResult AgentDetail(int id)
        {
            string url = "AgentData/FindAgentAssociateWithProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ;
            EstateAgentDTO foundagent = response.Content.ReadAsAsync<EstateAgentDTO>().Result;

            return View(foundagent);
        }

        //POST: Admin Adding new Agent 
        [HttpPost]
        [Route("/Admin/NewAgent")]
        public ActionResult NewAgent(EstateAgent property)
        {
            Debug.WriteLine("the json payload is :");

            string url = "AgentData/AddNewAgent";
            string jsonpayload = jss.Serialize(property);
            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("../Admin");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //Get :  Edit property
        [Route("Admin/AgentEdit/{id}")]
        [HttpGet]
        public ActionResult AgentEdit(int id)
        {
            string url = "AgentData/FindAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EstateAgentDTO SelectedAgent = response.Content.ReadAsAsync<EstateAgentDTO>().Result;
            return View(SelectedAgent);
        }

        //Post : Update Property after submit.
        [HttpPost]
        [Route("Admin/AgentUpdate/{id}")]
        public ActionResult AgentUpdate(int id, EstateAgent agent)
        {
            string url = "AgentData/UpdateAgent/" + id;
            string jsonpayload = jss.Serialize(agent);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("Admin/DeleteAgentConfirm/{id}")]
        public ActionResult DeleteAgentConfirm(int id)
        {
            string url = "AgentData/FindAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EstateAgentDTO SelectedAgent = response.Content.ReadAsAsync<EstateAgentDTO>().Result;
            return View(SelectedAgent);
        }

        [HttpPost]
        [Route("Admin/DeleteAgent/{id}")]
        public ActionResult DeleteAgent(int id)
        {
            string url = "AgentData/DeleteAgent/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}