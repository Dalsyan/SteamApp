﻿using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GenreDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.GameDTOs
{
    public class VoteForUpdateDTO
    {
        public string Title { get; set; }
        public int CompanyId { get; set; }

        public ICollection<GenreBaseDTO?> Genres { get; set; } = new List<GenreBaseDTO?>();
    }
}
