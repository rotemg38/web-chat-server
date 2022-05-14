using System;
using System.ComponentModel.DataAnnotations;

namespace Models 
{
    public class Rate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(1, 5)]
        [Display(Name = "Rate Number")]
        public int RateNumber { get; set; }

        public string Feedback { get; set; }

        [Display(Name= "Created Time")]
        public DateTime WhenCreated { get; set; }

        public Rate()
        {
            WhenCreated = DateTime.Now;
        }
    }
}
