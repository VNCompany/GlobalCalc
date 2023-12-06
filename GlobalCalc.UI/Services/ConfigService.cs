using System;
using System.IO;
using System.Text.Json;

namespace GlobalCalc.UI.Services;

internal class ConfigService
{
#if STAGE      
    private const string configurationFile = "config.stage.json";
#elif DEBUG
    private const string configurationFile = "config.debug.json";
#elif RELEASE
    private const string configurationFile = "config.json";
#endif

    #region Properties

    public string Host { get; set; } = "http://127.0.0.1:5000/api";

    public bool UseImages { get; set; } = true;

    #endregion


    #region Methods

    public static ConfigService Load()
    {
        try
        {
            if (File.Exists(configurationFile))
                return JsonSerializer.Deserialize<ConfigService>(
                    File.ReadAllText(configurationFile)
                    , new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }
        catch (Exception e)
        {
            System.Diagnostics.Trace.WriteLine(e.Message);
        }

        // If load from file failed:
        return new ConfigService();
    }

    #endregion
}