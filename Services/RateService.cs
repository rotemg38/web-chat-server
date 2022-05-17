using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    //todo: in the future we ill add Repository project that will access to DB.
    public class RateService : IRateService
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
        { 
            Rate updatedRate = _ratings.Find((updatedRate) => { return updatedRate.Id == rate.Id; });
            updatedRate.Name = rate.Name;
            updatedRate.Feedback = rate.Feedback;
        }
        public void Remove(int id)
        {
            Rate remRate = _ratings.Find((remRate) => { return remRate.Id == id; });
            _ratings.Remove(remRate);
        }

        public float GetAvr()
        {
            float avrg = 0, numOfRats = 0;
            foreach (var rate in _ratings)
            {
                avrg += rate.RateNumber;
                numOfRats++;
            }
            if (numOfRats == 0)
            {
                return 0;
            }
            return avrg / numOfRats;
        }

        public List<Rate> Search(string query)
        {
            if(query == null)
            {
                return GetAll();
            }
            List<Rate> result =
            _ratings.FindAll((rate) => { return rate.Feedback.Contains(query) == true; });

            return result;
        }
    }
}
