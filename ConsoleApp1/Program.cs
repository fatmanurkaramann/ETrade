using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5001/"); // API adresini ve port numarasını buraya girin

                // Kullanıcıdan ürün özelliklerini alın
                Console.WriteLine("Ürün Adı: ");
                string name = Console.ReadLine();

                Console.WriteLine("Ürün Fiyatı: ");
                double price;
                while (!double.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Geçersiz fiyat. Tekrar girin: ");
                }

                Console.WriteLine("Ürün Kategorisi: ");
                string category = Console.ReadLine();

                // Verileri oluşturun
                var productData = new
                {
                    Name = name,
                    Price = price,
                    Category = category
                };

                // Serialize edilmiş JSON verisini oluşturun
                var jsonData = System.Text.Json.JsonSerializer.Serialize(productData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // İstek başlıklarını ayarlayın (opsiyonel)
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // POST isteğini gönderin
                var response = await httpClient.PostAsync("Products", content);

                // İstek yanıtını işleyin
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Ürün başarıyla oluşturuldu.");
                }
                else
                {
                    Console.WriteLine($"Ürün oluşturma başarısız oldu. Status Code: {response.StatusCode}");
                }
            }
        }
    }
}
