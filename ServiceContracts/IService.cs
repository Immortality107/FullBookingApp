
using PaymentContracts.DTOs;
using System.Threading.Tasks;

    public interface IService
    {
        Task<List<Service>> GetServices();  
    }

