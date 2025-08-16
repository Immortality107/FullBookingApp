
using PaymentContracts.DTOs;
using System.Threading.Tasks;

    public interface IBook
    {
        Task<List<PaymentDTO>> GetAllAppointments();
        Task<List<PaymentDTO>> AddAppointment();
        Task<List<PaymentDTO>> DeleteAppointment();
        Task<List<PaymentDTO>> UpdateAppointment();


}

