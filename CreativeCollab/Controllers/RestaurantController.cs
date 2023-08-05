using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using CreativeCollab.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Diagnostics;
using CreativeCollab.Models.ViewModels;

namespace CreativeCollab.Controllers
{
    public class RestaurantController : Controller
    {
        private static readonly HttpClient Client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static RestaurantController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44372/api/");
        }

        // GET: Restaurant/List
        public ActionResult List()
        {
            string url = "restaurantdata/listrestaurants";

            HttpResponseMessage response = Client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<RestaurantDto> Restaurants = response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>().Result;
            //Debug.WriteLine("number of restaurants received: ");
            //Debug.WriteLine(restaurants.Count());

            return View(Restaurants);
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our restaurant data api to retrieve restaurants


            DetailsRestaurant ViewModel = new DetailsRestaurant();

            string url = "restaurantdata/findrestaurant/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;

            RestaurantDto SelectedRestaurant = response.Content.ReadAsAsync<RestaurantDto>().Result;

            ViewModel.SelectedRestaurant = SelectedRestaurant;

            //GO OVER neighbourhood details and compare

            return View(ViewModel);
        }

        //Error page
        public ActionResult Error()
        {
            return View();
        }

        // GET: Restaurant/New
        //asks the user for info about the restaurant (form)
        public ActionResult New()
        {
            string url = "neighbourhooddata/listneighbourhoods";
            HttpResponseMessage response = Client.GetAsync(url).Result;
            IEnumerable<NeighbourhoodDto> NeighbourhoodOptions = response.Content.ReadAsAsync<IEnumerable<NeighbourhoodDto>>().Result;

            return View(NeighbourhoodOptions);
        }

        // POST: Restaurant/Create
        // responsible for creating the restaurant (with the info from New)
        [HttpPost]
        public ActionResult Create(Restaurant Restaurant)
        {
            //objective: add a new restaurant into our system using the API
            string url = "restaurantdata/addrestaurant";

            string jsonpayload = jss.Serialize(Restaurant); // will convert into json string
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

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateRestaurant ViewModel = new UpdateRestaurant();

            string url = "restaurantdata/findrestaurant/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            RestaurantDto SelectedRestaurant = response.Content.ReadAsAsync<RestaurantDto>().Result;

            return View(SelectedRestaurant);

        }

        // POST: Restaurant/Update/5
        [HttpPost]
        public ActionResult Update(int id, Restaurant Restaurant)
        {
            string url = "restaurantdata/updaterestaurant/" + id;
            string jsonpayload = jss.Serialize(Restaurant);
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

        // GET: Restaurant/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "restaurantdata/findrestaurant/" + id;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            RestaurantDto SelectedRestaurant = response.Content.ReadAsAsync<RestaurantDto>().Result;
            return View(SelectedRestaurant);
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "restaurantdata/deleterestaurant/" + id;
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
