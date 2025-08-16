using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;
public class PaymobService
{
    private readonly HttpClient _httpClient;
    private readonly PaymobSettings _settings;


    public PaymobService(HttpClient httpClient, IOptions<PaymobSettings> options)
    {
        _httpClient = httpClient;
        //_apiKey = options.Value.ApiKey;
        _settings= options.Value;
    }

    public async Task<string> AuthenticateAsync()
    {
        var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", new
        {
            api_key = _settings.ApiKey
        });

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        return result.Token;
    }

    public async Task<int> CreateOrderAsync(string token, int amountCents)
    {
        var orderRequest = new
        {
            auth_token = token,
            delivery_needed = false,
            amount_cents = amountCents,
            currency = "EGP",
            items = new[]
            {
                new {
                    name = "Support Session",
                    amount_cents = amountCents,
                    quantity = 1
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", orderRequest);

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<OrderResponse>();

        return result.id; // You'll need this ID to generate a payment key in Step 3
    }
    public async Task<PaymentKeyResponse> GeneratePaymentTokenAsync(string authToken, int amountCents, int orderId, int integrationId)
    {
        var client = new HttpClient();
        var billingData = new
        {
            apartment = "NA",
            email = "user@example.com",
            floor = "NA",
            first_name = "Test",
            street = "NA",
            building = "NA",
            phone_number = "+201000000000",
            shipping_method = "PKG",
            postal_code = "NA",
            city = "Cairo",
            country = "EG",
            last_name = "User",
            state = "Cairo"
        };

        var response = await client.PostAsJsonAsync("https://accept.paymobsolutions.com/api/acceptance/payment_keys", new
        {
            auth_token = authToken,
            amount_cents = amountCents,
            expiration = 3600,
            order_id = orderId,
            billing_data = billingData,
            currency = "EGP",
            integration_id = integrationId
        });

        var result = await response.Content.ReadFromJsonAsync<PaymentKeyResponse>();
        return result;
    }

    public async Task<string> CreatePaymentIntentionAsync(string ServiceName, int amountCents)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _settings.SecretKey);
                                                                                               
        var response = await client.PostAsJsonAsync("https://accept.paymob.com/v1/intention/", new
        {
            amount = amountCents,
            currency = "EGP",
            payment_methods = new[] { int.Parse( _settings.CardIntegrationId) , int.Parse(_settings.WalletIntegrationId) },
            items = new[]
            {
            new
            {
                name = ServiceName,
                amount = amountCents
                //description = ServiceName,
                //quantity = 1
            }
        },
            billing_data = new
            {
                apartment = "dummy",
                first_name = "ala",
                last_name = "zain",
                street = "dummy",
                building = "dummy",
                phone_number = "+201000000000",
                city = "dummy",
                country = "EG",
                email = "ali@gmail.com",
                floor = "1",
                state = "dummy"
            },
            extras = new
            {
                ee = 22
            },
            //special_reference = OrderRef,
            expiration = 3600,
            notification_url = "https://webhook.site/your-id-here",
            redirection_url = "https://localhost:7118/AfterPaymentProcess"
        });
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Paymob error: {response.StatusCode} - {error}");
        }
        var PaymentResponse = await response.Content.ReadFromJsonAsync<PaymentIntentionResponse>();
        return PaymentResponse.ClientSecret;
    }

    public string GetUnifiedUrl(string ClientSecret)
    {
        return $" https://accept.paymob.com/unifiedcheckout/?publicKey={_settings.PublicKey}&clientSecret={ClientSecret}";
    }
}

