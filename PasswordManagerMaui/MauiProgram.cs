using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Reflection;
using PasswordManagerMaui.Models;

namespace PasswordManagerMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
                     

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            using var stream = Assembly.GetExecutingAssembly()
  .GetManifestResourceStream("PasswordManagerMaui.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Configuration.AddConfiguration(config);

            //var connectionString = builder.Configuration.GetConnectionString("testdb");
            //builder.Services.AddSingleton<WeatherForecastService>(
            //    s => ActivatorUtilities.CreateInstance<WeatherForecastService>(s, connectionString));

            

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value) });

            return builder.Build();
        }
    }    
}