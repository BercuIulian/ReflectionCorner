using Microsoft.EntityFrameworkCore;
using ReflectionCorner.Data;
using ReflectionCorner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .AsNoTracking()
                .Where(r => r.IsPublic || r.UserId == currentUserId) // Only public reviews or user's own
                .Where(r => r.User.IsPublic || r.UserId == currentUserId); // Only public users or current user

            // Filter by review title
            if (!string.IsNullOrWhiteSpace(criteria.ReviewTitle))
            {
                query = query.Where(r => r.Title.ToLower().Contains(criteria.ReviewTitle.ToLower()));
            }

            // Filter by username
            if (!string.IsNullOrWhiteSpace(criteria.Username))
            {
                query = query.Where(r => r.User.Username.ToLower().Contains(criteria.Username.ToLower()));
            }

            // Filter by review type
            if (!string.IsNullOrWhiteSpace(criteria.ReviewType))
            {
                var reviewTypeFilter = criteria.ReviewType.ToLower();

                query = query.Where(r =>
                    // Check enum types
                    (r.Type == ReviewType.Movie && "movie".Contains(reviewTypeFilter)) ||
                    (r.Type == ReviewType.TVShow && ("tvshow".Contains(reviewTypeFilter) || "tv show".Contains(reviewTypeFilter))) ||
                    (r.Type == ReviewType.Episode && "episode".Contains(reviewTypeFilter)) ||
                    // Check custom types
                    (r.Type == ReviewType.Custom && r.CustomReviewType != null &&
                     r.CustomReviewType.Name.ToLower().Contains(reviewTypeFilter))
                );
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
                    Username = r.User.Username,
                    ReviewType = r.Type,
                    ReviewTypeName = r.Type == ReviewType.Custom && r.CustomReviewType != null
                        ? r.CustomReviewType.Name
                        : r.Type.ToString()
                })
                .ToListAsync();

            return results;
        }
    }
}