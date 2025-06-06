using System.ComponentModel.DataAnnotations;

namespace ReflectionCorner.Models
{
    public class SearchCriteria
    {
        [StringLength(50)]
        public string? Username { get; set; }

        public ReviewType? ReviewType { get; set; }

        public int? CustomReviewTypeId { get; set; }

        [StringLength(100)]
        public string? Title { get; set; }

        public bool HasCriteria =>
            !string.IsNullOrWhiteSpace(Username) ||
            ReviewType.HasValue ||
            CustomReviewTypeId.HasValue ||
            !string.IsNullOrWhiteSpace(Title);
    }
}