
using PaymentContracts.DTOs;
using System.Threading.Tasks;

    public interface ILog
    {
        Task<List<PaymentDTO>> GetAllRegisteredAccounts();
        Task<List<PaymentDTO>> AddAccount();
        Task<List<PaymentDTO>> DeleteAccount();
        Task<List<PaymentDTO>> UpdateAccount();


}

