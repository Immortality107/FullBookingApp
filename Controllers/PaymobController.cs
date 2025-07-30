using Microsoft.AspNetCore.Mvc;

public class PaymentController : Controller
{
    private readonly PaymobService _paymobService;

    public PaymentController(PaymobService paymobService)
    {
        _paymobService = paymobService;
    }

    public async Task<IActionResult> StartPayment()
    {
        string token = await _paymobService.AuthenticateAsync();
        return Content($"Token: {token}");
    }

    public async Task<IActionResult> StartLocalPayment()
    {
        var token = await _paymobService.AuthenticateAsync();
        var orderId = await _paymobService.CreateOrderAsync(token, 15000); // for 150 EGP

        // ✅ Next Step: Generate payment key with this order ID
        return Content($"Order Created. Order ID: {orderId}");
    }
}
