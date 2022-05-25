using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;


namespace WebServerApp.Controllers
{
    public class RateController : Controller
    {
        private readonly IRateService _context;

        public RateController(IRateService context)
        {
            _context = context;
        }

        public IActionResult Signout()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5067/");
                var responseTask = client.GetAsync("api/setup/disConnectUser");
                responseTask.Wait();
            }
            return Redirect("http://localhost:3000/");
        }

        // GET: Rate
        public IActionResult Index()
        {
            float avrg = _context.GetAvr();
            ViewData["avrage"] = avrg;
            return View(_context.GetAll());
        }

       
        public IActionResult Search(string query)
        {
            float avrg = _context.GetAvr();
            ViewData["avrage"] = avrg;
            List<Rate> rates = _context.Search(query);
            return Json(JsonSerializer.Serialize(rates));
        }


        // GET: Rate/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = _context.GetRate(id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // GET: Rate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,RateNumber,Feedback")] Rate rate)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(rate);
                return RedirectToAction(nameof(Index));
            }
            return View(rate);
        }

        // GET: Rate/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = _context.GetRate(id);
            if (rate == null)
            {
                return NotFound();
            }
            return View(rate);
        }

        // POST: Rate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,RateNumber,Feedback")] Rate rate)
        {
            if (id != rate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RateExists(rate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rate);
        }

        // GET: Rate/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = _context.GetRate(id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // POST: Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RateExists(int id)
        {
            return _context.GetRate(id) != null;
        }
    }
}
