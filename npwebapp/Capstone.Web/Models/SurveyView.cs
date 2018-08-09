using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class SurveyView
    {
        //public Survey survey { get; set; }
        public string ParkCode { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string State { get; set; }
        [Required(ErrorMessage = "Please select an activity level.")]
        public string ActivityLevel { get; set; }
        public List<Park> Parks { get; set; }
    }
}