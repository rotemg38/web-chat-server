using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Rate
        public IActionResult Index()
        {
            float avrg = _context.GetAvr();
            ViewData["avrage"] = avrg;
            return View(_context.GetAll());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
