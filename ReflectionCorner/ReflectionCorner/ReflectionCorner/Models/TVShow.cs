using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReflectionCorner.Models
{
    public class TVShow
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }

        public User User { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}