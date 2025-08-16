using System.Text.Json.Serialization;

public class PaymentIntentionResponse
{
    [JsonPropertyName("order_id")]
    public string Id { get; set; }

    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; }

    [JsonPropertyName("redirection_url")]
    public string RedirectionUrl { get; set; }
}
