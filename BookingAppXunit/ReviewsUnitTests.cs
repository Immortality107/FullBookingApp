using Entities;
using Microsoft.EntityFrameworkCore;
using PaymentContracts;
using PaymentContracts.DTOs;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.InMemory;
namespace BookingAppXunit
{
    public class ReviewsUnitTests
    {
        ReviewDbContext dbContext { get; set; }
        //private readonly IReview _Reviews;
        //public ReviewsUnitTests()
        //{
        //    _Reviews = new ReviewPayments(new ReviewsDbContext(new DbContextOptionsBuilder<ReviewsDbContext>().Options));

        //}
        private readonly IReview _Reviews;

        public ReviewsUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReviewDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER2;Initial Catalog=ReviewsDbContext;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;");

            dbContext = new ReviewDbContext(optionsBuilder.Options);
            _Reviews = new ReviewServices(dbContext);
        }
            [Fact]
        #region "Add Review"
        public async void AddReview_NullValue()
        {
            //Arrange
            ReviewAddRequest review = null;
            //Act
            //Assert             

          await  Assert.ThrowsAsync<ArgumentNullException>(  async () => {  await
            _Reviews.AddReview(review);
        });
        }

        [Fact]
        public async void AddReview_BookingIDNotFound()
        { //arrange
            ReviewAddRequest review = new ReviewAddRequest() { BookingID = Guid.NewGuid(), ReviewMessage="Review" };

            //Act //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _Reviews.AddReview(review);
            });
        }
        [Fact]

        public async void AddReview_BookingMessageEmpty()
        { //arrange
           
            ReviewAddRequest review = new ReviewAddRequest() { BookingID = Guid.NewGuid(), ReviewMessage=" " };

            //Act
            if (string.IsNullOrEmpty(review.ReviewMessage))
            {
                //assert
               await Assert.ThrowsAsync<ArgumentNullException>(() => _Reviews.AddReview(review));
            }

        }
        [Fact]
        public async void AddReview_ValidValue()
        {
            //arrange
      
            ReviewAddRequest review = new ReviewAddRequest() { BookingID =Guid.Parse( "d2e84756-7743-4265-a40f-bac354fe0f31"), ReviewMessage="Review" };

            //Act
            ReviewResponse response = await _Reviews.AddReview(review);
            //Assert
            Assert.True(response.ReviewID!= null);
        }
        #endregion

        #region "GetReviews"
        //[Fact]

        //public async void GetReviews_EmptyList()
        //{
        //    //arrange
        //    dbContext.Reviews.RemoveRange(dbContext.Reviews);
        //    dbContext.SaveChanges();
        //    List <ReviewResponse> responses = await _Reviews.GetReviews();
        //    //Act
        //    //Assert
        //    Assert.Empty(responses);
        //}
        [Fact]
        public async Task GetReviews_EmptyList()
        {
            // Arrange - use a fresh in-memory database
            var options = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh DB
                .Options;

            var dbContext = new ReviewDbContext(options);
            var reviewService = new ReviewServices(dbContext); // inject whatever your real service needs

            // Act
            List<ReviewResponse> reviews = await reviewService.GetReviews();
            // Assert
            Assert.Empty(reviews);
        }
        [Fact]
        public async void GetReviews_ProperList()
        {
            //arrange
            List<ReviewAddRequest> reviewAddRequests = new List<ReviewAddRequest>()
           {
               new ReviewAddRequest(){BookingID=Guid.Parse("d2e84756-7743-4265-a40f-bac354fe0f31"), ReviewMessage="ReviewTest1"},
               new ReviewAddRequest(){BookingID=Guid.Parse("d2e84756-7743-4265-a40f-bac354fe0f31"), ReviewMessage="ReviewTest2"},
           };

            List<ReviewResponse> ActualResponses = new List<ReviewResponse>();
            //Act
            foreach (ReviewAddRequest r in reviewAddRequests)
            {
                ActualResponses.Add(await _Reviews.AddReview(r));
            }
            List <ReviewResponse> ResponsesFromGet = await _Reviews.GetReviews();
            //Assert
            foreach (var expected in ActualResponses)
            {
                Assert.Contains(ActualResponses, r => r.ReviewID == expected.ReviewID &&
                                                      r.BookingID == expected.BookingID);
            }

        }
        #endregion
    }
}