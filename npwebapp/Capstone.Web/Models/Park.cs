using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Park
    {
        public string ParkCode { get; set; }

        public string ParkName { get; set; }

        public string State { get; set; }
        
        public int Acreage { get; set; }

        public int ElevationInFeet { get; set; }

        public double MilesOfTrail { get; set; }

        public int NumberOfCampSites { get; set; }

        public string Climate { get; set; }

        public int YearFounded { get; set; }

        public int AnnualVisitorCount { get; set; }

        public string InspirationalQuote { get; set; }

        public string QuoteSource { get; set; }

        public string ParkDescription { get; set; }

        public int EntryFee { get; set; }

        public int NumberOfAnimalSpecies { get; set; }

    }
}