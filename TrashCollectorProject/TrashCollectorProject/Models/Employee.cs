using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string address { get; set; }
        
        public string zipCode { get; set; }
        public string routeStartingPoint { get; set; }
        public string routeEndingPoint { get; set; }
    }
}