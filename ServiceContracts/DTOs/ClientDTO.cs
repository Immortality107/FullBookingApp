using Azure.Core;
using ServiceContracts.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    public class ClientDTO
    {
        [Required]
        [StringLength(25)]
        public required string ClientName { get; set; }

        [Required]
        [StringLength(20)]
        public required string Country { get; set; }

        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        [Required]
        [StringLength(15)]
        public required string Phone { get; set; }

        [Required]
        public required int Age { get; set; }

        [Required]
        public required string Gender { get; set; } //enGenderOptions

        public Client ToClient(ClientDTO clientDTO)
        {
            return new Client
            {
                ClientID=Guid.NewGuid(),
                ClientName= clientDTO.ClientName,
                Age = clientDTO.Age,
                Country=clientDTO.Country,
                Email=clientDTO.Email,
                Gender = clientDTO.Gender,
                Phone=clientDTO.Phone,
                Notes=""
            };
        }
    }
}
public static class ClientExtention
{
    public static ClientDTO  ToClientDTO(this Client client)
    {
        return new ClientDTO { Age = client.Age , ClientName=client.ClientName,
            Country= client.Country, Email= client.Email,
            Gender= client.Gender, Phone = client.Phone };
    }
}
