using System;
using CreativeCollab.Migrations;
using CreativeCollab.Models;
using CreativeCollab.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CreativeCollab.Controllers
{
    public class NeighbourhoodController : Controller
    {
        private static readonly HttpClient Client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static NeighbourhoodController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44372/api/");
        }

        // GET: Neighbourhood/List
        public ActionResult List()
        {
            string url = "neighbourhooddata/listneighbourhoods";

            HttpResponseMessage response = Client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<NeighbourhoodDto> Neighbourhoods = response.Content.ReadAsAsync<IEnumerable<NeighbourhoodDto>>().Result;

            return View(Neighbourhoods);
        }

        // GET: Neighbourhood/Details/5
        public ActionResult Details(int id)
        {
            DetailsNeighbourhood ViewModel = new DetailsNeighbourhood();

            string url = "neighbourhooddata/findneighbourhood/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;

            NeighbourhoodDto SelectedNeighbourhood = response.Content.ReadAsAsync<NeighbourhoodDto>().Result;

            ViewModel.SelectedNeighbourhood = SelectedNeighbourhood;

            //showcase information about restaurants related to this neighbourhood
            // send request to gather information about restaurnts related to a particular neighbourhood id
            url = "restaurantdata/listrestaurantsforneighbourhood/" + id;
            response = Client.GetAsync(url).Result;
            IEnumerable<RestaurantDto> RelatedRestaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;

            ViewModel.RelatedRestaurants = RelatedRestaurants;

            return View(ViewModel);
        }

        //Error page
        public ActionResult Error()
        {
            return View();
        }

        // GET: Neighbourhood/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Neighbourhood/Create
        [HttpPost]
        public ActionResult Create(Neighbourhood neighbourhood)
        {
            string url = "neighbourhooddata/addneighbourhood";

            string jsonpayload = jss.Serialize(neighbourhood); // will convert into json string
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = Client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Neighbourhood/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "neighbourhooddata/findneighbourhood/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            NeighbourhoodDto SelectedNeighbourhood = response.Content.ReadAsAsync<NeighbourhoodDto>().Result;
            return View(SelectedNeighbourhood);
        }

        // POST: Neighbourhood/Update/5
        [HttpPost]
        public ActionResult Update(int id, Neighbourhood neighbourhood)
        {
            string url = "neighbourhooddata/updaterneighbourhood/" + id;
            string jsonpayload = jss.Serialize(neighbourhood);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = Client.PostAsync(url, content).Result;
            //Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Neighbourhood/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "neighbourhooddata/findneighbourhood/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            NeighbourhoodDto SelectedNeighbourhood = response.Content.ReadAsAsync<NeighbourhoodDto>().Result;
            return View(SelectedNeighbourhood);
        }

        // POST: Neighbourhood/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "neighbourhooddata/deleteneighbourhood/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = Client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
