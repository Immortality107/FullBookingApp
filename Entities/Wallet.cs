using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class WalletResponse
{
    public WalletRedirectData data { get; set; }
}

public class WalletRedirectData
{
    [JsonPropertyName("redirect_url")]
    public string redirect_url { get; set; }
}

