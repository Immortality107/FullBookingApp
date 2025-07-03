using BookingApp.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts1.DTOs
{
    public class ReviewResponse
    {
        public Guid ReviewID { get; set; }
        public required string ReviewMessage { get; set; }

        public Guid BookingID { get; set; }

        public Review ToReview()
        {
            return new Review() { ReviewMessage = ReviewMessage, BookingID = BookingID, ReviewId=ReviewID };
        }

        public override string ToString()
        {
            return $"ReviewID:{ReviewID} + ReviewMessage:{ReviewMessage} + BookingID:{BookingID}";
        }

    }

    public static class ReviewExtenstions
    {
        public static ReviewResponse ToReviewResponse(this Review review)
        {
            return new ReviewResponse() { ReviewID = review.ReviewId, ReviewMessage= review.ReviewMessage, BookingID=review.BookingID };
        }
    }
}