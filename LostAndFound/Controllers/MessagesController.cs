using LostAndFound.Hubs;
using LostAndFound.Models;
using LostAndFound.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LostAndFound.Controllers
{
    public class MessagesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ChatService messageService;
        private readonly IHubContext<ChatHub> hubContext;
        public MessagesController(
            UserManager<User> _userManager,
            ChatService _messageService,
            IHubContext<ChatHub> _hubContext)
        {
            userManager = _userManager;
            messageService = _messageService;
            hubContext = _hubContext;
        }
        public async Task<IActionResult> Index(int? itemId, int? receiverId)
        {
            var currentUser = await userManager.GetUserAsync(User);

            var vm = messageService.GetMessagesPage(
                currentUser.Id,
                itemId,
                receiverId);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsReturned(int itemId)
        {
            bool result = messageService.MarkItemAsReturned(itemId);

            if (!result)
                return Json(new { success = false });

            var users = messageService.GetChatUsers(itemId);

            if (users != null)
            {
                await hubContext.Clients.All.SendAsync(
       "ChatClosed",
       itemId);
            }

            return Json(new
            {
                success = true
            });
        }
    }
    }
