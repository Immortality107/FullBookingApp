
using PaymentContracts.DTOs;
using System.Threading.Tasks;

    public interface ILogin
    {
        Task<List<LoginDTO>> GetAllRegisteredAccounts();
        Task<Guid> AddAccount(LoginDTO loginDTO);
        Task<bool> DeleteAccount(LoginDTO loginDTO);
        Task<bool> UpdateAccount(LoginDTO loginDTO);
        Task<bool> AccountFound(LoginDTO loginDTO);


}

