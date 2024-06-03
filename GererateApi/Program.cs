using System.Net;
using NSwag.CodeGeneration.CSharp;
using NSwag;

namespace RestApi.Generator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var wclient = new WebClient();
            await GenerateBackend(wclient);
        }

        private static async Task GenerateBackend(WebClient wclient)
        {
            var document = await OpenApiDocument.FromJsonAsync(
                wclient.DownloadString(
                    "https://localhost:7070/swagger/v1/swagger.json"));

            wclient.Dispose();

            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = "SkiApi",
                CSharpGeneratorSettings =
                {
                    Namespace = "SkiSelect",
                    DateTimeType = typeof(DateTime).FullName
                }
            };

            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
            await File.WriteAllTextAsync("../../../../Stratsys.SkiSelect/Api/SkiApi.cs", code);
        }
    }
}