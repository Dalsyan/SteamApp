﻿using SteamAPI.Models.AccountDTOs;
using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamDomain;

namespace SteamAPI.Models.CountryDTOs
{
    public class CountryBaseDTO
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}