using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class RateService
    {
        private static List<Rate> _ratings = new List<Rate>();

        public RateService()
        {
            //_ratings.Add(new Rate() { Id = 1, TextReview = "great app", Name = "user1", RateNumber = 5 });
        }

        public Rate GetRate(int? id)
        {
            Rate rate = _ratings.Find((rate) => { return rate.Id == id; });
            return rate;
        }
        public List<Rate> GetAll()
        {
            return _ratings;
        }

        public void Add(Rate rate)
        {
            _ratings.Add(rate);
        }
        public void Update(Rate rate)
        { // plaster :(
            Rate updatedRate = _ratings.Find((updatedRate) => { return updatedRate.Id == rate.Id; });
            _ratings.Remove(updatedRate);
            _ratings.Add(rate);
            
        }
        public void Remove(int id)
        {
            Rate remRate = _ratings.Find((remRate) => { return remRate.Id == id; });
            _ratings.Remove(remRate);
        }

        
        
    }
}
