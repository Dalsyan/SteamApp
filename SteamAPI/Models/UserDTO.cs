﻿namespace SteamAPI.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int CountryId { get; set; }
    }
}
