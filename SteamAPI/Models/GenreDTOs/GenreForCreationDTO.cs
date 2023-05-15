﻿using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Models.UserDTOs;

namespace SteamAPI.Models.GenreDTOs
{
    public class GenreForCreationDTO
    {
        public string GenreName { get; set; }

        public ICollection<GameForCreationDTO?> Games { get; set; } = new List<GameForCreationDTO?>();
    }
}