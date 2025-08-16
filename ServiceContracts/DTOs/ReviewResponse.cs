using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace PaymentContracts.DTOs
{
    public class ReviewResponse
    {
        public Guid ReviewID { get; set; }
        public required string ReviewMessage { get; set; }

        public Guid BookingID { get; set; }

        public Booking? booking {  get; set; }

        public Guid ClientID { get; set; }
        public Booking? Client { get; set; }

        public Review ToReview()
        {
            return new Review() { ReviewMessage = ReviewMessage, BookingID = BookingID, ReviewId=ReviewID,booking= this.booking };
        }

        public override string ToString()
        {
            return $"ReviewID:{ReviewID} + ReviewMessage:{ReviewMessage} + BookingID:{BookingID} ";
        }

    }

    public static class ReviewExtenstions
    {
        public static ReviewResponse ToReviewResponse(this Review review)
        {
            return new ReviewResponse() { ReviewID = review.ReviewId, ReviewMessage= review.ReviewMessage, BookingID=review.BookingID, booking = review.booking != null ? review.booking : null };
        }
    }
}

