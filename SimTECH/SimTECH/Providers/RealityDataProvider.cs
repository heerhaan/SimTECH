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

    public async Task<List<RealCircuit>> GetExistingCircuits()
    {
        var dataPath = dataFiles.FirstOrDefault(e => e.Contains("circuits"));

        if (dataPath == null)
            return [];

        var allLines = await File.ReadAllLinesAsync(dataPath);

        return allLines
            .Skip(1)
            .Select(RealCircuit.FromCsv)
            .ToList();
    }

    public async Task<List<RealConstructor>> GetExistingConstructors()
    {
        var dataPath = dataFiles.FirstOrDefault(e => e.Contains("constructors"));

        if (dataPath == null)
            return [];

        var allLines = await File.ReadAllLinesAsync(dataPath);

        return allLines
            .Skip(1)
            .Select(RealConstructor.FromCsv)
            .ToList();
    }

    public async Task<List<RealDriver>> GetExistingDrivers()
    {
        var dataPath = dataFiles.FirstOrDefault(e => e.Contains("drivers"));

        if (dataPath == null)
            return [];

        var allLines = await File.ReadAllLinesAsync(dataPath);

        return allLines
            .Skip(1)
            .Select(RealDriver.FromCsv)
            .ToList();
    }

    public async Task<List<RealStatus>> GetExistingStatii()
    {
        var dataPath = dataFiles.FirstOrDefault(e => e.Contains("status"));

        if (dataPath == null)
            return [];

        var allLines = await File.ReadAllLinesAsync(dataPath);

        return allLines
            .Skip(1)
            .Select(RealStatus.FromCsv)
            .ToList();
    }
}

public class RealCircuit
{
    public int Id { get; set; }

    public string Ref { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public string Country { get; set; }

    public static RealCircuit FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');

        return new RealCircuit
        {
            Id = int.Parse(values[0]),
            Ref = values[1],
            Name = values[2],
            Location = values[3],
            Country = values[4],
        };
    }
}

public class RealConstructor
{
    public int Id { get; set; }

    public string Ref { get; set; }

    public string Name { get; set; }

    public static RealConstructor FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');

        return new RealConstructor
        {
            Id = int.Parse(values[0]),
            Ref = values[1],
            Name = values[2],
        };
    }
}

public class RealDriver
{
    public int Id { get; set; }

    public string Ref { get; set; }

    public int Number { get; set; }

    public string Code { get; set; }

    public string Forename { get; set; }

    public string Surname { get; set; }

    public DateTime Dob { get; set; }

    public string Nationality { get; set; }

    public static RealDriver FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');

        var realDriver = new RealDriver
        {
            Id = int.Parse(values[0]),
            Ref = values[1],
            Code = values[3],
            Forename = values[4],
            Surname = values[5],
            Nationality = values[7],
        };

        if (int.TryParse(values[2], out int parsedNum))
            realDriver.Number = parsedNum;

        if (DateTime.TryParse(values[6], out DateTime parsedDate))
            realDriver.Dob = parsedDate;

        return realDriver;
    }
}

public class RealStatus
{
    public int Id { get; set; }

    public string Status { get; set; }

    public static RealStatus FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');

        return new RealStatus
        {
            Id = int.Parse(values[0]),
            Status = values[1],
        };
    }
}
