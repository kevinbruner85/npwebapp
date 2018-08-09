using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkDAL
    {
        private string _connectionString = "";
        private const string SQL_GetAllParks = "select * from park;";
        private const string SQL_GetAllWeather = "select * from weather;";
        private const string SQL_GetAllSurvey = "select * from survey_result;";
        private const string SQL_ParkByCode = "select * from park where parkCode = @parkCode;";
        private const string SQL_GetWeatherByCode = "select * from weather where parkCode = @parkCode;";
        private const string SQL_GetFavorites = "select COUNT(survey_result.parkCode) as parkCount, park.parkCode, Park.parkName, Park.parkDescription from survey_result JOIN park on park.parkCode = survey_result.parkCode group by Park.parkDescription, park.parkName, park.parkCode order by COUNT(survey_result.parkCode) desc;";
        private const string SQL_ParkCodeByPark = "select * from park where park.parkName = @parkName;";
        public ParkSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> result = new List<Park>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_GetAllParks;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park park = new Park();
                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Acreage = Convert.ToInt32(reader["acreage"]);
                    park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                    park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                    park.NumberOfCampSites = Convert.ToInt32(reader["numberOfCampsites"]);
                    park.Climate = Convert.ToString(reader["climate"]);
                    park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                    park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                    park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                    park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    result.Add(park);
                }
            }
            return result;

        }

        public List<Weather> GetAllWeather()
        {
            List<Weather> result = new List<Weather>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_GetAllWeather;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Weather weather = new Weather();
                    weather.ParkCode = Convert.ToString(reader["parkCode"]);
                    weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                    weather.LowTemp = Convert.ToInt32(reader["low"]);
                    weather.HighTemp = Convert.ToInt32(reader["high"]);
                    weather.Forecast = Convert.ToString(reader["forecast"]);
                    result.Add(weather);

                }
            }
            return result;

        }

        public List<Survey> GetAllSurvey()
        {
            List<Survey> result = new List<Survey>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_GetAllSurvey;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Survey survey = new Survey();
                    survey.ActivityLevel = Convert.ToString(reader["activityLevel"]);
                    survey.SurveyID = Convert.ToInt32(reader["surveyId"]);
                    survey.State = Convert.ToString(reader["state"]);
                    survey.EmailAddress = Convert.ToString(reader["emailAddress"]);
                    survey.ParkCode = Convert.ToString(reader["parkCode"]);
                    result.Add(survey);

                }
            }
            return result;

        }


        //Return new survey PK id C_R
        public bool SaveSurvey(SurveyView newSurvey)
        {
            bool wasSuccessful = true;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sqlSaveSurvey = "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES (@ParkCode, @Email, @State, @ActivityLevel)";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlSaveSurvey;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@ActivityLevel", newSurvey.ActivityLevel);
                //cmd.Parameters.AddWithValue("@SurveyID", newSurvey.SurveyID);
                cmd.Parameters.AddWithValue("@State", newSurvey.State);
                cmd.Parameters.AddWithValue("@Email", newSurvey.Email);
                cmd.Parameters.AddWithValue("@ParkCode", newSurvey.ParkCode);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    wasSuccessful = false;
                    throw new Exception("Error: Review could not be made.");
                }
            }
            return wasSuccessful;
        }


        //create method for reading in park object
        //if (reader.readd)-- create park object else, throw exception
        public Park ParkByCode(string parkCode)
        {
            Park park = new Park();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_ParkByCode;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@parkCode", parkCode);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Acreage = Convert.ToInt32(reader["acreage"]);
                    park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                    park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                    park.NumberOfCampSites = Convert.ToInt32(reader["numberOfCampsites"]);
                    park.Climate = Convert.ToString(reader["climate"]);
                    park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                    park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                    park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                    park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    
                }
            }
            return park;

        }


        //exception handling validate i receive 5 days worth of forecasts from database
        public List<Weather> GetPark5DayForecast(string parkCode)
        {
            List<Weather> result = new List<Weather>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_GetWeatherByCode;
                cmd.Parameters.AddWithValue("@parkCode", parkCode);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Weather weather = new Weather();
                    weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                    weather.Forecast = Convert.ToString(reader["forecast"]);
                    weather.ParkCode = Convert.ToString(reader["parkCode"]);
                    weather.LowTemp = Convert.ToInt32(reader["low"]);
                    weather.HighTemp = Convert.ToInt32(reader["high"]);
                    result.Add(weather);
                }
                return result;
            }
        }

        public List<Favorites> GetFavorites()
        {
            List<Favorites> result = new List<Favorites>();

            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_GetFavorites;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Favorites favorite = new Favorites();
                    favorite.ParkCode = Convert.ToString(reader["parkCode"]);
                    favorite.ParkCount = Convert.ToInt32(reader["parkCount"]);
                    favorite.ParkName = Convert.ToString(reader["parkName"]);
                    favorite.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    result.Add(favorite);
                }
                return result;
            }
        }
        

    }
}