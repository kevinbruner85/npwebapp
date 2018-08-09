using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        
        private IParkDAL _dal;
        // GET: Home
        public HomeController(IParkDAL dal)
        {
            _dal = dal;
        }
     
        public ActionResult Index()
        {
            var parks = _dal.GetAllParks();
            return View(parks);
        }

        public ActionResult Weather(Park park, string Scale)
        {
            DetailView detailView = new DetailView();
            detailView.park = _dal.ParkByCode(park.ParkCode);
            detailView.weather = _dal.GetPark5DayForecast(park.ParkCode);
            if(park.ParkCode == null)
            {
                park.ParkCode = Session["ParkCode"] as string;
            }
            else
            {
                Session["ParkCode"] = park.ParkCode;
            }
            if(Scale == null)
            {
                Scale = Session["Scale"] as string;
            }
            else
            {
                Session["Scale"] = Scale;
            }
            if (Scale == "true")
            {
                foreach (var item in detailView.weather)
                {
                    item.HighTemp = ((item.HighTemp - 32) * 5 / 9);
                    item.LowTemp = ((item.LowTemp - 32) * 5 / 9);
                }
            }
            return View("WeatherDetail", detailView);
        }

        public ActionResult Survey()
        {
            SurveyView survey = new SurveyView();
            survey.Parks = _dal.GetAllParks();
            return View("Surveys", survey);
        }

        [HttpPost]
        public ActionResult Survey(SurveyView survey)
        {
            
            ActionResult redirect;
            if (!ModelState.IsValid)
            {
                survey.Parks = _dal.GetAllParks();
                redirect = View("Surveys", survey);
            }
            else
            {
                _dal.SaveSurvey(survey);
                var favorites = _dal.GetFavorites();
                redirect = RedirectToAction("Favorites", favorites);
                
            }
           
            
            return redirect;

        }

        public ActionResult Favorites()
        {
            var favorites = _dal.GetFavorites();
            return View("Favorites", favorites);
        }

        public ActionResult SetScale(string Scale, string parkCode)
        {
            Session["Scale"] = Scale;
            Park park = new Park();
            park.ParkCode = null;
            return Weather(park, null);
           
        }

    }
}