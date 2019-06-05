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

        [Display(Name = "Address")]
        public string address { get; set; }

        [Display(Name = "Zip Code")]
        public string zipCode { get; set; }

        [Display(Name = "Weekly Pickup Day")]
        public string weeklyPickupDay { get; set; }

        [Display(Name = "Special One Time Pickup")]
        public string specialOneTimePickup { get; set; }

        [Display(Name = "Hold Pickup Start Day")]
        public string holdPickUpStart { get; set; }

        [Display(Name = "Hold Pickup End Day")]
        public string holdPickUpEnd { get; set; }

        [Display(Name = "Bill")]
        public string bill { get; set; }

        [Display(Name = "User Id")]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}