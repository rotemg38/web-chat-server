using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IRateService
    {
        public Rate GetRate(int? id);
        public List<Rate> GetAll();
        public void Add(Rate rate);
        public void Update(Rate rate);
        public void Remove(int id);
        public List<Rate> Search(string query);
        public float GetAvr();
    }
}
