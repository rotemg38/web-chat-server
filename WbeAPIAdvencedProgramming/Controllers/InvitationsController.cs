using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

namespace WbeAPIAdvencedProgramming.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly ChatsService _context;

        public InvitationsController(ChatsService context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("invitations")]
        public ActionResult Invitations([FromBody] Chat chatInfo) 
        {
           if (_context.AddConction(chatInfo.Participants.Item1, chatInfo.Participants.Item2) == -1)
            {
                return NotFound();
            }
           return View(chatInfo); ///????
        }

        // GET: InvitaionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: InvitaionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvitaionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvitaionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvitaionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvitaionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvitaionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvitaionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
