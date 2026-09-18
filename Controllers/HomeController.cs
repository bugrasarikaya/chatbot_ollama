using chatbot_ollama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using System.Diagnostics;
using System.Text.Json;

namespace chatbot_ollama.Controllers {
    public class HomeController(IChatClient chatclient) : Controller {
        private List<ChatMessage>? listChatMessage;
        private List<ChatResponseUpdate>? listChatResponseUpdate;
        private readonly IChatClient _chatClient = chatclient;
        public async Task<IActionResult> Main(MessageModel message) {
            if (!string.IsNullOrEmpty(message.Text)) {
                listChatMessage = HttpContext.Session.Get("ChatMessage") == null ? [] : JsonSerializer.Deserialize<List<ChatMessage>>(HttpContext.Session.Get("ChatMessage"));
                listChatMessage!.Add(new(ChatRole.User, message.Text));
                listChatResponseUpdate = [];
                await foreach (ChatResponseUpdate chatResponseUpdate in _chatClient.GetStreamingResponseAsync(listChatMessage)) listChatResponseUpdate.Add(chatResponseUpdate);
                listChatMessage.AddMessages(listChatResponseUpdate);
                HttpContext.Session.SetString("ChatMessage", JsonSerializer.Serialize(listChatMessage));
            }
            return await Task.Run(() => View(listChatMessage?.Last()));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
