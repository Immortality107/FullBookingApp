
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContracts.DTOs
{
    public class ReviewAddRequest
    {
        [Required(ErrorMessage = "Please Add Your Review")]
        public required string ReviewMessage { get; set; }


        [Required(ErrorMessage = "Your BookingID Is Invalid, You Can Contact Us To Get Your Valid One")]
        public Guid BookingID { get; set; }


        public Review ToReview()
        {
            return new Review() {ReviewId=Guid.NewGuid(), BookingID = this.BookingID, ReviewMessage= this.ReviewMessage };
        }
    }
}
