
using PaymentContracts.DTOs;
using System.Threading.Tasks;
namespace PaymentContracts
{
    public interface IReview
    {
        Task<List<ReviewResponse>> GetReviews();
        Task<ReviewResponse> AddReview(ReviewAddRequest? addRequest);
    }
}
