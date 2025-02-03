using System;
using System.ComponentModel.DataAnnotations;

namespace ReflectionCorner.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }

        public User User { get; set; }
    }
}