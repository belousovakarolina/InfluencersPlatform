﻿using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.UserDTOs
{
    public class PatchUserRequestDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
