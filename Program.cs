using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using OllamaSharp;
namespace chatbot_ollama {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddChatClient(new OllamaApiClient(new Uri("http://localhost:11434"), "qwen3:1.7b"), ServiceLifetime.Singleton);
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(option => { option.IdleTimeout = TimeSpan.FromHours(1); });
            var app = builder.Build();
            if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");
            app.UseRouting();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Main}/{id?}").WithStaticAssets();
            app.UseSession();
            app.Run();
        }
    }
}
