using System.Diagnostics;
using ChatApplication.Core.Models;
using ChatApplication.Core.Models.Enums;
using ChatApplication.Infrastructure;
using ChatApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            await _context.Chats.AddAsync(new Chat
            {
                Name = name,
                ChatType = ChatType.ROOM
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }


    }
}
