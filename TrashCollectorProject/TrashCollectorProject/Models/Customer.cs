using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }


        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        public string address { get; set; }

        [Display(Name = "Zip Code")]
        public string zipCode { get; set; }

        [Display(Name = "Weekly Pickup Day")]
        public string weeklyPickupDay { get; set; }

        [Display(Name = "Special One Time Pickup")]
        public string specialOneTimePickup { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}