using System;
using System.Collections.Generic;
using System.IO;

using GlobalCalc.Client;
using GlobalCalc.Models;

namespace GlobalCalc.UI.Test;

internal class ApiClientTest : IApiClient
{
    public FacadeData GetData() => Models.Test.TestModels.FacadeData;

    public List<RemoteImageFile> GetImages() => new(0);

    public Stream GetImage(string file)
    {
        throw new NotImplementedException();
    }
}