﻿using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.InfluencerProfileDTOs
{
    public class CreateInfluencerProfileRequestDTO
    {
        public int UserId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? IgFollowerCount { get; set; }
        public int? FbFollowerCount { get; set; }
        public int? TiktokFollowerCount { get; set; }
    }
}
