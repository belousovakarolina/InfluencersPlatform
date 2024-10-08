﻿namespace InfluencersPlatformBackend.Models
{
    public class InfluencerProfile
    {
        public  int Id { get; set; }
        public  int UserId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? IgFollowerCount { get; set; }
        public int? FbFollowerCount { get; set; }
        public int? TiktokFollowerCount { get; set; }
        public InfluencerProfile(int id, int userId) { }
        
    }
}
