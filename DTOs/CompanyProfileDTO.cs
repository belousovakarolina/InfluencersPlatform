﻿using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs
{
    public class CompanyProfileDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? YearlyIncome { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public CompanyProfileDTO() { }
    }
}
