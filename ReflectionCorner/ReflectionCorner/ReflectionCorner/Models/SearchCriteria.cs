using System.ComponentModel.DataAnnotations;

namespace ReflectionCorner.Models
{
    public class SearchCriteria
    {
        [StringLength(100)]
        public string? ReviewTitle { get; set; }

        [StringLength(50)]
        public string? ReviewType { get; set; }

        [StringLength(50)]
        public string? Username { get; set; }

        public bool HasCriteria => !string.IsNullOrWhiteSpace(ReviewTitle) ||
                                  !string.IsNullOrWhiteSpace(ReviewType) ||
                                  !string.IsNullOrWhiteSpace(Username);
    }
}