using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notifications.Sample.Web.Models;
using Microsoft.AspNetCore.SignalR;
using Notifications.Sample.Web.Hubs;

namespace Notifications.Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<NotificiationHub> _hubContext;

        public HomeController(IHubContext<NotificiationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("do-somethig")]
        public IActionResult DoSomething()
        {
            Task.Run(Notify);

            return Ok();
        }

        private async Task Notify()
        {
            await Task.Delay(60000);

            await _hubContext.Clients.All.InvokeAsync("showNotifications", "Well Done");
        }
    }
}
