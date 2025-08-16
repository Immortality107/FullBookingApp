using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Appointments
    {
        [Key]
     public required Guid AppointmentId {  get; set; }

        [Required]
     public required Guid BookingID {get; set; }

        [Required]
     public required DateTime StartTime { get; set; }  
        
        [Required]
     public required  DateTime BookedAt { get; set; }

        [Required]
        public required Guid PaymentId { get; set; }

        public required Booking? booking { get; set; }

     

    }
}
