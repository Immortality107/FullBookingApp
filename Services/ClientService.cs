
using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTOs;

namespace Services
{
    public class ClientService : IClient
    {
        private ReviewDbContext _db;
        public ClientService(ReviewDbContext db) {
         _db = db;
        }
        public Task<bool> AddClient(ClientDTO clientDTO)
        {
            Client client = clientDTO.ToClient(clientDTO);
            _db.clients.Add(client);
            _db.SaveChanges();
            return Task.FromResult(true);
        }

        public async Task<List<ClientDTO>> GetAllClients()
        {
            List<Client> AllClients = _db.clients.ToList();
            List<ClientDTO> clientsDto = AllClients.Select( c => c.ToClientDTO()).ToList()  ;
            return  clientsDto;
        }
    }
}
