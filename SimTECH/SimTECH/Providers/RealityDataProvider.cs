using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SimTECH.Providers;

public class RealityDataProvider
{
    private readonly IWebHostEnvironment _env;
    private readonly string[] dataFiles;

    public RealityDataProvider(IWebHostEnvironment env)
    {
        _env = env;
        dataFiles = Directory.GetFiles(Path.Combine(_env.WebRootPath, "csv"));
    }


}
