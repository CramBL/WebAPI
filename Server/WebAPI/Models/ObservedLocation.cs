using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ObservedLocation
    {
        [Key]
        public long ObservedLocationId {get; set;}
        public string LocationName{get;set;}
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
