using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }

        [Display(Name = "Zip Code")]

        public string zipCode { get; set; }

        [Display(Name = "Route start")]
        public string routeStartingPoint { get; set; }

        [Display(Name = "Route end")]
        public string routeEndingPoint { get; set; }

        [Display(Name = "Email Address")]

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}