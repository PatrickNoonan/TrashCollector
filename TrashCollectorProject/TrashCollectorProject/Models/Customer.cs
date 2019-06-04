using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string address { get; set; }
        public string zipCode { get; set; }
        public string weeklyPickupDay { get; set; }
        public string specialOneTimePickup { get; set; }
    }
}