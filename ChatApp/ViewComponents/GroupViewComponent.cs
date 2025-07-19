using ChatApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.ViewComponents
{
    public class GroupViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public GroupViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var chats = _context.ChatUsers
                .Include(cu => cu.Chat)
                .Where(c => c.UserId == userId)
                .Select(cu => cu.Chat)
                .ToList();
            return View(chats);
        }
    }
}
