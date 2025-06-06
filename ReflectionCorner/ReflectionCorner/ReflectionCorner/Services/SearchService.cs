using Microsoft.EntityFrameworkCore;
using ReflectionCorner.Data;
using ReflectionCorner.Models;

namespace ReflectionCorner.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _context;

        public SearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SearchResult>> SearchReviewsAsync(SearchCriteria criteria, int currentUserId)
        {
            if (!criteria.HasCriteria)
                return new List<SearchResult>();

            var query = _context.Reviews
                .Include(r => r.User)
                .Include(r => r.CustomReviewType)
                .Include(r => r.ParentReview)
                .AsNoTracking()
                .Where(r => r.IsPublic || r.UserId == currentUserId); // Only public reviews or user's own

            // Filter by username
            if (!string.IsNullOrWhiteSpace(criteria.Username))
            {
                query = query.Where(r => r.User.Username.ToLower().Contains(criteria.Username.ToLower()));
            }

            // Filter by review type
            if (criteria.ReviewType.HasValue)
            {
                query = query.Where(r => r.Type == criteria.ReviewType.Value);
            }

            // Filter by custom review type
            if (criteria.CustomReviewTypeId.HasValue)
            {
                query = query.Where(r => r.CustomReviewTypeId == criteria.CustomReviewTypeId.Value);
            }

            // Filter by title
            if (!string.IsNullOrWhiteSpace(criteria.Title))
            {
                query = query.Where(r => r.Title.ToLower().Contains(criteria.Title.ToLower()));
            }

            var results = await query
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new SearchResult
                {
                    Id = r.Id,
                    Title = r.Title,
                    Content = r.Content,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt,
                    IsPublic = r.IsPublic,
                    Username = r.User.Username,
                    Type = r.Type,
                    TypeDisplayName = r.Type == ReviewType.Custom && r.CustomReviewType != null
                        ? r.CustomReviewType.Name
                        : r.Type.ToString(),
                    TVShowTitle = r.Type == ReviewType.Episode && r.ParentReview != null
                        ? r.ParentReview.Title
                        : null
                })
                .Take(100) // Limit results for performance
                .ToListAsync();

            return results;
        }

        public async Task<List<CustomReviewType>> GetAvailableCustomTypesAsync(int userId)
        {
            return await _context.CustomReviewTypes
                .Where(crt => crt.UserId == userId)
                .OrderBy(crt => crt.Name)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}