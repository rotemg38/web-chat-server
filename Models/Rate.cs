using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Rate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RateNumber { get; set; }

        public string TextReview { get; set; }
    }
}
