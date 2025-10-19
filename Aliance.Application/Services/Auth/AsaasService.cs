using Aliance.Application.Interfaces.Auth;
using Aliance.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Aliance.Application.Integration.Asaas
{
    public class AsaasService : IAsaasService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AsaasService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Asaas:ApiKey"]
                ?? throw new ArgumentNullException("Asaas:ApiKey not found in configuration");

            var baseUrl = configuration["Asaas:BaseUrl"] ?? "https://api-sandbox.asaas.com/v3/";
            _httpClient.BaseAddress = new Uri(baseUrl);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("access_token", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "AlianceApp/1.0"); // ← obrigatório pelo Asaas
        }

        public async Task<string> CreateCustomerAsync(Church church, ApplicationUser user)
        {
            var payload = new
            {
                name = church.Name,
                email = church.Email,
                phone = church.Phone,
                mobilePhone = user.PhoneNumber,
                cpfCnpj = church.CNPJ, // opcional
                postalCode = "89227-000", // teste com CEP válido
                addressNumber = "100",
                address = church.Address,
                province = "Centro",
                city = 4205407
            };

            var response = await _httpClient.PostAsJsonAsync("customers", payload);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao criar cliente no Asaas: {(int)response.StatusCode} - {body}");

            var json = JsonDocument.Parse(body);
            return json.RootElement.GetProperty("id").GetString()!;
        }

        public async Task<string> CreateCheckoutAsync(string customerId, string plan, string paymentMethod)
        {
            decimal value = plan.ToLower() switch
            {
                "basico" => 29.90M,
                "essencial" => 59.90M,
                "premium" => 99.90M,
                _ => 29.90M
            };

            var items = new[]
            {
                new
                {
                    name = $"Plano {plan}",
                    description = $"Assinatura do plano {plan}",
                    quantity = 1,
                    value = value,
                }
            };

            var payload = new
            {
                billingTypes = new[] { paymentMethod.ToUpper() },
                chargeTypes = new[] { "RECURRENT" },
                minutesToExpire = 15,
                callback = new
                {
                    cancelUrl = "https://google.com/cancel",
                    expiredUrl = "https://google.com/expired",
                    successUrl = "https://aliance.app.br"
                },
                items = items,
                customer = customerId, 
                subscription = new
                {
                    cycle = "MONTHLY",
                    endDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd HH:mm:ss"),
                    nextDueDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd HH:mm:ss")
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("checkouts", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Adicione log para capturar erro detalhado
                throw new Exception($"Erro ao criar checkout no Asaas: {(int)response.StatusCode} - {body}");
            }

            var json = JsonDocument.Parse(body);

            return json.RootElement.GetProperty("link").GetString()!;
        }

        public async Task<(string subscriptionId, decimal value)> CreateSubscriptionAsync(string customerId, string plan, string paymentMethod)
        {
            decimal value = plan.ToLower() switch
            {
                "basico" => 29.90M,
                "essencial" => 59.90M,
                "premium" => 99.90M,
                _ => 29.90M
            };

            var payload = new
            {
                customer = customerId,
                billingType = paymentMethod.ToUpper(), // CREDIT_CARD ou BOLETO
                value = value,
                cycle = "MONTHLY",
                nextDueDate = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd"),
                description = $"Plano {plan}"
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("subscriptions", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao criar assinatura no Asaas: {(int)response.StatusCode} - {body}");

            var json = JsonDocument.Parse(body);
            var id = json.RootElement.GetProperty("id").GetString()!;
            return (id, value);
        }

        public async Task<string> DeleteCustomerAsync(string customerId)
        {
            // Realizando a requisição DELETE para o endpoint correto
            var response = await _httpClient.DeleteAsync($"customers/{customerId}");

            // Verificando a resposta da API
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Se a resposta não for bem-sucedida, lançar uma exceção com o erro
                throw new Exception($"Erro ao deletar o cliente no Asaas: {(int)response.StatusCode} - {body}");
            }

            // Se o cliente for deletado com sucesso, retornamos uma mensagem ou o id do cliente deletado
            return $"Cliente com ID {customerId} deletado com sucesso!";
        }

    }
}
