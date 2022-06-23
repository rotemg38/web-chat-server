using System;
using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public class RateService : IRateService
    {
        private ServerDbContext _context;

        public RateService()
        {
            _context = new ServerDbContext();
        }

        public Rate GetRate(int? id)
        {
            return _context.getRate(id);
        }
        public List<Rate> GetAll()
        {
            return _context.getAllRate();
        }

        public void Add(Rate rate)
        {
            _context.insertRate(rate);
        }
        public void Update(Rate rate)
        { 
            _context.updateRate(rate);
        }
        public void Remove(int id)
        {
            Rate rate = _context.getRate(id);
            _context.removeRate(rate);
        }

        public float GetAvr()
        {
            List<Rate> ratings = GetAll();
            float avrg = 0, numOfRats = 0;
            foreach (Rate rate in ratings)
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

            List<Rate> ratings = GetAll();

            List<Rate> result =
            ratings.FindAll((rate) => { return rate.Feedback.Contains(query) == true; });

            return result;
        }
    }
}
