
using PaymentContracts.DTOs;
using System.Threading.Tasks;

    public interface IPay
    {
        Task<List<PaymentDTO>> GetAllPayments();
        Task<List<PaymentDTO>> GetInternationalPayments();
        Task<List<PaymentDTO>> GetLocalPayments();
        Task<string> ExtractPriceAsync(string Price);
}

