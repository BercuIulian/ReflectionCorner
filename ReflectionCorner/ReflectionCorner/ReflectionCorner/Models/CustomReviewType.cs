using System;
using System.ComponentModel.DataAnnotations;

namespace ReflectionCorner.Models
{
    public class CustomReviewType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public bool IsDefault { get; set; } = false; // True for Movie, TVShow, Episode

        public int? UserId { get; set; } // Null for default types, UserId for custom types

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
    }
}