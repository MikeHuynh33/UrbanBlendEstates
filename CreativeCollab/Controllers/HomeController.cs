using CreativeCollab.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CreativeCollab.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44372/api/");
        }
        [HttpGet]
        // GET: /Home
        [Route("/")]
        public ActionResult Index()
        {
            string url = "AgentData/ListAgents";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<EstateAgentDTO> agents = response.Content.ReadAsAsync<IEnumerable<EstateAgentDTO>>().Result;

            string sec_url = "";
            string searchProperty = Request.QueryString["searchProperty"];
            Debug.WriteLine(searchProperty);
            if (!string.IsNullOrEmpty(searchProperty))
            {
                sec_url = "PropertyData/ListOfProperties?searchProperty=" + searchProperty;
            }
            else
            {
                sec_url = "PropertyData/ListOfProperties";
            }
            HttpResponseMessage sec_response = client.GetAsync(sec_url).Result;
            IEnumerable<PropertyDetailDTO> properties = sec_response.Content.ReadAsAsync<IEnumerable<PropertyDetailDTO>>().Result;

            AgentsAndPropertiesViewModel viewModel = new AgentsAndPropertiesViewModel
            {
                Agents = agents,
                Properties = properties
            };
            return View(viewModel);
        }
        [HttpGet]
        [Route("/Home/PropertyDetail/{id}")]
        public ActionResult PropertyDetail(int id)
        {
            string url = "PropertyData/FindPropertyAssociateWithAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                PropertyDetailDTO foundproperty = response.Content.ReadAsAsync<PropertyDetailDTO>().Result;
                return View(foundproperty);
            }
            else
            {
                return View("error");
            }
            
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
    }
}