using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using TestI18nDemo.Data;
using TestI18nDemo.Extend;

namespace TestI18nDemo;

public static class MauiProgram
{
	public static MauiApp CreateMauiAppAsync()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		
		builder.Services.AddSingleton<WeatherForecastService>();

		builder.Services.AddMasaBlazor();
        builder.Services.AddMasaBlazor().AddI18nForMauiBlazor("i18n");
		
        return builder.Build();
	}
}
