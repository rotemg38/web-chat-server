using System;
using System.ComponentModel.DataAnnotations;

namespace Models 
{
    public class Rate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(1, 5)]
        public int RateNumber { get; set; }

        public string Feedback { get; set; }

        //public string date { get; set; }
    }
}
