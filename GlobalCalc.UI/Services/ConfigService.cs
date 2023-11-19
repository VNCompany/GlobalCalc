using System;
using System.IO;
using System.Text.Json;

namespace GlobalCalc.UI.Services;

internal class ConfigService : IDisposable
{
    public static ConfigService Load(string file = "config.json")
    {
        try
        {
            if (File.Exists(file))
                return JsonSerializer.Deserialize<ConfigService>(file)!;
        }
        catch (Exception e)
        {
            System.Diagnostics.Trace.WriteLine(e.Message);
        }
        
        // If load from file failed:
        return new ConfigService();
    }
    
    
    public void Save(string file = "config.json")
    {
        File.WriteAllText(file, JsonSerializer.Serialize(this));
    }


    public void Dispose()
    {
        Save();
    }


    public string Host { get; set; } = "http://127.0.0.1:5000/api";

    public bool UseImages { get; set; } = true;
}