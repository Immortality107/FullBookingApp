using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PaymentContracts.DTOs;
namespace Services
{

    public class PayService : IPay
    {
        private ReviewDbContext _db {  get; set; }
        public PayService(ReviewDbContext dbContext) {
        _db = dbContext;
        }
        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            List<PaymentDTO> paymentDTOs = _db.payment.Select(p => p.ToPaymentDTO()).ToList();
            return  paymentDTOs;
        }

        public async Task <string> ExtractPriceAsync(string PriceWithCurrency)
        {
            string digitsOnly = new string(PriceWithCurrency.Where(char.IsDigit).ToArray());
            return digitsOnly;
        }
        public async Task<List<PaymentDTO>> GetInternationalPayments()
        {
            List<PaymentMethods> payments= _db.payment.Where(p => p.Location == "International").ToList();
            List<PaymentDTO> InternationalPayments = payments.Select(p => p.ToPaymentDTO()).ToList();
            return InternationalPayments;
        }

        public async Task<List<PaymentDTO>> GetLocalPayments()
        {
            List<PaymentMethods> payments = _db.payment.Where(p => p.Location == "Egypt").ToList();
            List<PaymentDTO> LocalPayments = payments.Select(p => p.ToPaymentDTO()).ToList();
            return LocalPayments;
        }
    }
}
