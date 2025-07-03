using ServiceContracts1.DTOs;
using System.Threading.Tasks;

    public interface IReview
    {
        Task<List<ReviewResponse>> GetReviews();
        Task<ReviewResponse> AddReview();
    }
