using SimTECH.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimTECH.Providers
{
    public class HumanBeingProvider
    {
        private readonly IWebHostEnvironment _env;

        public HumanBeingProvider(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string[] GetLanguageTypes()
        {
            var dataFiles = Directory.GetFiles(Path.Combine(_env.WebRootPath, "json"));
            var pathNameLength = dataFiles[0].Length;

            return dataFiles.Select(e => e.Substring(startIndex: pathNameLength - 10, 2)).ToArray();
        }

        public string[] GetNationalityCodes()
        {
            var dataFiles = Directory.GetFiles(Path.Combine(_env.WebRootPath, "json"));
            var pathNameLength = dataFiles[0].Length;

            return dataFiles.Select(e => e.Substring(startIndex: pathNameLength - 7, 2)).ToArray();
        }

        public HumanBeing[] GetHumanBeings(Country nationality)
        {
            var dataFiles = Directory.GetFiles(Path.Combine(_env.WebRootPath, "json"));
            var relevantFiles = dataFiles.Where(e => e.EndsWith(nationality.ToString() + ".json"));

            var humanBeans = new List<HumanBeing>();
            foreach (var relevancy in relevantFiles)
            {
                var dataString = File.ReadAllText(relevancy);
                var databank = JsonSerializer.Deserialize<PersonDatabank>(dataString);

                if (databank != null)
                    HumanBeanify(humanBeans, databank);
            }

            return humanBeans.ToArray();
        }

        private static void HumanBeanify(List<HumanBeing> humanBeans, PersonDatabank databank)
        {
            // TODO: Gender filter
            // TODO: Amount filter
            var combinedNames = databank.Male.Concat(databank.Female).ToArray();

            for (int i = 0; i < 5; i++)
            {
                var nameIndex = NumberHelper.RandomInt(combinedNames.Length - 1);
                var surIndex = NumberHelper.RandomInt(databank.Family.Length - 1);

                humanBeans.Add(new HumanBeing
                {
                    Name = $"{combinedNames[nameIndex]} {databank.Family[surIndex]}",
                    Gender = "o",
                    Nationality = Country.BR,
                    Dob = DateTime.Now,
                });
            }
        }
    }

    internal class PersonDatabank
    {
        [JsonPropertyName("male")]
        public string[] Male { get; set; }
        [JsonPropertyName("female")]
        public string[] Female { get; set; }
        [JsonPropertyName("family")]
        public string[] Family { get; set; }
    }

    public class HumanBeing
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public Country Nationality { get; set; }
        public DateTime Dob { get; set; }
    }
}
