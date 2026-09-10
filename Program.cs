using Microsoft.Extensions.AI;
using OllamaSharp;
namespace chatbot_ollama {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddChatClient(new OllamaApiClient(new Uri("http://localhost:11434"), "qwen3:1.7b"), ServiceLifetime.Scoped);
            var app = builder.Build();
            if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");
            app.UseRouting();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }
    }
}
