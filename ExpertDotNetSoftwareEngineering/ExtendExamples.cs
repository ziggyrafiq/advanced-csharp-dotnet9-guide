using Microsoft.Extensions.Configuration;

namespace ExpertDotNetSoftwareEngineering;

public static class ExtendExamples
{
    private static readonly IConfiguration _configuration;
    private static string _apiUrlAddress = _configuration?["ApiUrlAddress"] ?? string.Empty;



    public static string MatchComplexPattern(List<int> numbers)
    {
        return numbers switch
        {
        [0, .., > 5] => "Starts with 0 and ends with a number greater than 5",
        [1, 2, 3, .. var rest] when rest.Contains(4) => "Starts with 1, 2, 3 and contains a 4 in the remaining list",
            _ => "No match found"
        };
    }

    public struct ComplexNumber : ICalculable<ComplexNumber>
    {
        public double Real { get; }
        public double Imaginary { get; }

        public ComplexNumber(double real, double imaginary) => (Real, Imaginary) = (real, imaginary);

        public static ComplexNumber Add(ComplexNumber a, ComplexNumber b) =>
            new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);

        public static ComplexNumber Subtract(ComplexNumber a, ComplexNumber b) =>
            new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    public static async IAsyncEnumerable<string?> FetchDataAsync(string? apiUrlAddress = null)
    {
        if (string.IsNullOrEmpty(apiUrlAddress))
        {
            apiUrlAddress = _apiUrlAddress;
        }

        using var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync(apiUrlAddress, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();
        using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            yield return await reader.ReadLineAsync();
        }
    }

    public static async Task ProcessDataAsync(string? apiUrlAddress = null)
    {
        if (string.IsNullOrEmpty(apiUrlAddress))
        {
            apiUrlAddress = _apiUrlAddress;
        }

        await foreach (var line in FetchDataAsync(apiUrlAddress))
        {
            Console.WriteLine(line);
        }
    }



}
