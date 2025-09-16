using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;

            // Load Stripe Secret Key from config (User Secrets / Environment Variables)
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [HttpPost("create-payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount, // Amount in cents
                Currency = request.Currency ?? "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
    }

    public class PaymentRequest
    {
        public long Amount { get; set; }
        public string? Currency { get; set; }
    }
}
