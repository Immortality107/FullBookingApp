

public class PaymentKeyRequest
{
    public string auth_token { get; set; }
    public string amount_cents { get; set; }
    public int expiration { get; set; } = 3600;
    public int order_id { get; set; }
    public BillingData billing_data { get; set; }
    public string currency { get; set; } = "EGP";
    public int integration_id { get; set; }
}

public class BillingData
{
    public string apartment { get; set; }
    public string email { get; set; }
    public string floor { get; set; }
    public string first_name { get; set; }
    public string street { get; set; }
    public string building { get; set; }
    public string phone_number { get; set; }
    public string shipping_method { get; set; }
    public string postal_code { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string last_name { get; set; }
    public string state { get; set; }
}

