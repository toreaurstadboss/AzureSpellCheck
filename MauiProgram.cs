using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AzureSpellCheckDemo;

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

        var azureKey = Environment.GetEnvironmentVariable("AZURE_BING_SEARCH_SERVICE_KEY");

        if (string.IsNullOrWhiteSpace(azureKey))
        {
            throw new ArgumentNullException(nameof(azureKey), "Missing system environment variable: AZURE_BING_SEARCH_SERVICE_KEY");
        }

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
