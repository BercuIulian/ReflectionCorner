using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReflectionCorner.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string? Content { get; set; }

        public int? Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsPublic { get; set; } = true;

        [Required]
        public ReviewType Type { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ParentId")]
        public Review ParentReview { get; set; }
    }

    public enum ReviewType
    {
        None,
        Movie,
        TVShow,
        Episode
    }
}