﻿using InfluencersPlatformBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace InfluencersPlatformBackend.DTOs.CompanyProfileDTOs
{
    public class CreateCompanyProfileRequestDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }
        public double? YearlyIncome { get; set; }
        public CreateCompanyProfileRequestDTO() { }
    }
}
