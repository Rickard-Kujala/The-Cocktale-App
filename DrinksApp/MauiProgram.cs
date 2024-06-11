using DrinksApp.Clients;
using DrinksApp.Data;
using DrinksApp.Mappers;
using DrinksApp.Services;
using DrinksApp.View;
using DrinksApp.ViewModel;
using Microsoft.Extensions.Logging;
using Refit;

namespace DrinksApp
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddScoped<IDrinkService, DrinkService>();
            builder.Services.AddRefitClient<IDrinksAppClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.thecocktaildb.com/"));

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<DrinksViewModel>();
            builder.Services.AddSingleton<DrinkDetailsViewModel>();
            builder.Services.AddSingleton<DetailsPage>();
            builder.Services.AddSingleton<LibraryPage>();
            builder.Services.AddSingleton<LibraryViewModel>();
            builder.Services.AddSingleton<DrinksDatabase>();
            builder.Services.AddScoped<IDrinkMapper, DrinkMapper>();

            return builder.Build();
        }
    }
}
