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
    
    public FacadeData Data { get; }
    
    public string? Status { get; }

    public AppService(ServicesManager services)
    {
        services.Images.Initialize();
        
        FacadeData? fd;
        if ((fd = services.Api.GetData()) == null)
        {
            if ((fd = SetFacadeDataFromLocalFile()) == null)
                throw new Exception("Не удалось соединиться с сервером.");

            Status = "Не удалось соединиться с сервером. Данные могут быть неактуальны.";
        }

        Data = fd;
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