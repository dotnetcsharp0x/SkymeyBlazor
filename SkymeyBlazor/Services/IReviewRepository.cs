using SkymeyBlazor.Models;

namespace SkymeyBlazor.Services
{
    public interface IReviewRepository
    {
        Task<IEnumerable<BookReview>> GetReviewsAsync();
        Task<IEnumerable<BookReview>> GetReviewSummariesAsync();
    }
}
