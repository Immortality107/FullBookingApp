using System.Runtime.CompilerServices;
using Entities;
namespace PaymentContracts.DTOs
{
    public class PaymentDTO
    {
        public required string PaymentName { get; set; }
        public required string PaymentDetails { get; set; }

    }
    public static class PaymentMethodsExtensions
    {
        public static PaymentDTO ToPaymentDTO(this PaymentMethods payment)
        {
            return new PaymentDTO() { PaymentDetails= payment.PaymentDetails, PaymentName=payment.PaymentName };
        }
    }
}
