using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class RateService
    {
        private static List<Rate> _ratings;
        
        public RateService()
        {
            _ratings = new List<Rate>();
            _ratings.Add(new Rate() { Id = 1, TextReview = "great app", Name = "user1", RateNumber = 5 });
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
            //todo
        }
        public void Update(Rate rate)
        {
            //todo
        }
        public void Remove(int id)
        {
            //todo
        }

        
        
    }
}
