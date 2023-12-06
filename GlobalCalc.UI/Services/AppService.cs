using System;
using System.IO;
using System.Text.Json;

using GlobalCalc.Models;
using GlobalCalc.Client;

namespace GlobalCalc.UI.Services;

internal class AppService : IDisposable
{
    #region Static
    
    public static readonly string DataPath = Path.Combine(Environment.CurrentDirectory, "data");

    static AppService()
    {
        if (!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);
    }

    #endregion

    private readonly ServicesManager _services;

    public FacadeData Data { get; private set; } = null!;

    public AppService(ServicesManager services)
    {
        _services = services;
        _services.Images.Initialize();
    }

    public string? Run()
    {
        FacadeData? fd = null;

        try
        {
            fd = _services.Api.GetData();
        }
        catch (Exception)
        {
            if ((fd = SetFacadeDataFromLocalFile()) == null)
                throw new Exception("Не удалось соединиться с сервером.");

            Data = fd;
            return "Не удалось соединиться с сервером. Данные могут быть неактуальны.";
        }

        Data = fd;
        return null;
    }

    public void Dispose()
    {
        string serializedFacadeData = JsonSerializer.Serialize(Data);
        File.WriteAllText("data.json", serializedFacadeData);
    }

    #region Private methods
    
    private FacadeData? SetFacadeDataFromLocalFile()
    {
        try
        {
            return File.Exists("data.json")
                ? JsonSerializer.Deserialize<FacadeData>(File.ReadAllText("data.json"))
                : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    #endregion
}