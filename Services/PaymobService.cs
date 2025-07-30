using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Runtime;
using System.Text;
using System.Text.Json;
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

    public async Task<string> GeneratePaymentKeyAsync(string token, int orderId, int amountCents, string integrationId,string RedirectURL)
    {
        var paymentKeyRequest = new
        {
            auth_token = token,
            amount_cents = amountCents,
            expiration = 3600,
            order_id = orderId,
            billing_data = new
            {
                apartment = "NA",
                email = "test@example.com",
                floor = "NA",
                first_name = "Test",
                street = "NA",
                building = "NA",
                phone_number = "0123456789",
                shipping_method = "NA",
                postal_code = "NA",
                city = "Cairo",
                country = "EG",
                last_name = "User",
                state = "NA"
            },
            currency = "EGP",
            integration_id = int.Parse(integrationId),
            lock_order_when_paid = true,
            iframe_redirection_url = RedirectURL

        };
        var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", paymentKeyRequest);
        response.EnsureSuccessStatusCode();
       

        var result = await response.Content.ReadFromJsonAsync<PaymentKeyResponse>();
        return result.token;
    }
    public string GetIframeUrl(string paymentToken, string iframeId)
    {
        return $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentToken}";
    }

    //public async Task<string> WalletGeneratePaymentKeyAsync(string token, int orderId, int amountCents, string integrationId)
    //{
    //    var Request = new
    //    {
    //        auth_token = token,
    //        amount_cents = amountCents,
    //        expiration = 3600,
    //        order_id = orderId,
    //        billing_data = new
    //        {
    //            apartment = "NA",
    //            email = "test@example.com",
    //            floor = "NA",
    //            first_name = "Test",
    //            street = "NA",
    //            building = "NA",
    //            phone_number = "0123456789",
    //            shipping_method = "NA",
    //            postal_code = "NA",
    //            city = "Cairo",
    //            country = "EG",
    //            last_name = "User",
    //            state = "NA"
    //        },
    //        currency = "EGP",
    //        integration_id = int.Parse(integrationId)
    //    };
    //    var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", Request);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<PaymentKeyResponse>();
    //    return result.token;
    //}
    public async Task<string> InitiateWalletPaymentAsync(string paymentToken, string phoneNumber)
    {
        var request = new
        {
            source = new
            {
                identifier = phoneNumber,
                subtype = "WALLET"
            },
            payment_token = paymentToken
        };

        var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payments/pay", request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(json);
        string redirectUrl = result.redirect_url ?? result.data?.redirect_url ?? result.data?.iframe_redirection_url;

        return redirectUrl;
    }



    //public async Task<string> WalletPay(string PaymentKey) {

    //    var response = await _httpClient.PostAsJsonAsync(" https://accept.paymob.com/api/acceptance/payments/pay", 

    //        new WalletRequest() { Identifier= "", SubType="Wallet", Payment_Token=PaymentKey });

    //    var stToken = await response.Content.ReadFromJsonAsync<WalletResponse>();
    //        return stToken.Token;

    //}

    //public async Task<int> CreateOrder()
    //{
    //    var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", new { Token = AuthenticateAsync() });
    //    var OrderId = await response.Content.ReadFromJsonAsync<OrderResponse>();
    //    return OrderId.id;

    //}
}

