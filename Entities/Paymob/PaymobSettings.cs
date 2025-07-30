using Microsoft.Extensions.Options;

public class PaymobSettings
{
    public string ApiKey { get; set; }
    public string CardIntegrationId { get; set; }

    public string WalletIntegrationId { get; set; }

    public string IframeId { get; set; }

}

