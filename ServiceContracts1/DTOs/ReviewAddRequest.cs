
using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts1.DTOs
{
    public class ReviewAddRequest
    {
        [Required(ErrorMessage = "Please Add Your Review")]
        public required string ReviewMessage { get; set; }


        [Required(ErrorMessage = "Your BookingID Is Invalid, You Can Contact Us To Get Your Valid One")]
        public Guid BookingID { get; set; }


        public Review ToReview()
        {
            return new Review() { BookingID = this.BookingID, ReviewMessage= this.ReviewMessage };
        }
    }
}
