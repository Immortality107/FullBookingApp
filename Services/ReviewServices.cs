using System.Threading.Tasks;
using PaymentContracts;
using PaymentContracts.DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
public class ReviewServices : IReview
{
    private readonly ReviewDbContext _db;

    public ReviewServices(ReviewDbContext reviewsDbContext)
    {
        _db=reviewsDbContext;
        //_db.Reviews.Include(r => r.booking.client.ClientName).ToList();

       List< Review?> reviews =  _db.Reviews.ToList();
        foreach (Review r in reviews)
        {
            r.booking = _db.Bookings.FirstOrDefault(b => b.BookingId ==r.BookingID);

        }

    }
 

    public async Task<ReviewResponse> AddReview(ReviewAddRequest? addRequest)
    {
        if (addRequest==null) throw new ArgumentNullException(nameof(addRequest));
        else if (string.IsNullOrEmpty(addRequest.ReviewMessage)) throw new ArgumentNullException("Review Message Can Not Be Empty");
        List<Review> Reviews = _db.Reviews.ToList();
        Booking? BookingIDToCheck
            = _db.Bookings.FirstOrDefault( b => 
            b.BookingId == addRequest.BookingID );
        if (BookingIDToCheck == null)
        {
            throw new KeyNotFoundException($"No booking found with ID: {addRequest.BookingID}");
        }
       
            Review reviewToAdd = addRequest.ToReview();
         await  _db.Reviews.AddAsync(reviewToAdd);
         await  _db.SaveChangesAsync();
        
          return reviewToAdd.ToReviewResponse();
    }
    public async Task<List<ReviewResponse>> GetReviews()
    {
        //return await _db.Reviews
        //    .Include(r => r.booking) 
        //    .Select(review => review.ToReviewResponse())
        //    .ToListAsync();
        return await _db.Reviews
       .Include(r => r.booking)
           .ThenInclude(b => b.client)
       .Include(r => r.booking)
           .ThenInclude(b => b.payment)
       .Include(r => r.booking)
           .ThenInclude(b => b.Service)
       .Select(review => review.ToReviewResponse())
       .ToListAsync();
    }

}

