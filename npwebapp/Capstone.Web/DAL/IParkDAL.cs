using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IParkDAL
    {
        List<Park> GetAllParks();
        List<Weather> GetAllWeather();
        List<Survey> GetAllSurvey();
        bool SaveSurvey(SurveyView newSurvey);
        List<Weather> GetPark5DayForecast(string parkCode);
        Park ParkByCode(string parkCode);
        List<Favorites> GetFavorites();
        //string ParkCodeByPark(string parkName);

    }
}