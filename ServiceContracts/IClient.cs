
using PaymentContracts.DTOs;
using ServiceContracts.DTOs;
using System.Threading.Tasks;

    public interface IClient
    {
        Task<List<ClientDTO>> GetAllClients();
        Task<bool> AddClient(ClientDTO clientDTO);
        //Task<bool> DeleteClient(LoginDTO loginDTO);
        //Task<bool> UpdateAccount(LoginDTO loginDTO);


}

