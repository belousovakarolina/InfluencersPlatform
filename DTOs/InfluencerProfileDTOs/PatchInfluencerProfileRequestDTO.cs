﻿using InfluencersPlatformBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfluencersPlatformBackend.DTOs.InfluencerProfileDTOs
{
    public class PatchInfluencerProfileRequestDTO
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? IgFollowerCount { get; set; }
        public int? FbFollowerCount { get; set; }
        public int? TiktokFollowerCount { get; set; }
    }
}
