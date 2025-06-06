using System;

namespace ReflectionCorner.Models
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string ReviewTypeName { get; set; }
        public ReviewType ReviewType { get; set; }
        public string? TVShowTitle { get; set; } // For episodes
    }
}