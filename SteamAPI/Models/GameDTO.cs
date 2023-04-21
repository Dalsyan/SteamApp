﻿using SteamDomain;

namespace SteamAPI.Models
{
    public class GameDTO
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public List<User?> Users { get; set; } = new List<User?>();
    }
}
