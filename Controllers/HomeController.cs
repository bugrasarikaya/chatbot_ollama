using chatbot_ollama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using System.Diagnostics;

namespace chatbot_ollama.Controllers {
    public class HomeController(IChatClient chatclient) : Controller {
        private readonly IChatClient _chatClient = chatclient;
        public async Task<IActionResult> Index() {
            var response = await _chatClient.GetResponseAsync("What is .NET? Reply in 50 words max.");
            return await Task.Run(() => View(response));
        }
        public IActionResult Privacy() {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
