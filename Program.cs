using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsumerAdviceApi
{

    public class AdviceResponse
    {
        [JsonPropertyName("slip")]
        public Slip? Slip { get; set; }
    }


    public class Slip
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("advice")]
        public string? Advice { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://api.adviceslip.com/advice";

            Console.WriteLine("Iniciando requisição para obter dados de um conselho:");
            Console.WriteLine(url);
            Console.WriteLine();

            using HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                AdviceResponse? adviceData = JsonSerializer.Deserialize<AdviceResponse>(responseBody);

                if (adviceData?.Slip != null)
                {
                    Console.WriteLine("Conselho de Hoje:");
                    Console.WriteLine(adviceData.Slip.Advice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar a API: {ex.Message}");
            }
        }
    }
}