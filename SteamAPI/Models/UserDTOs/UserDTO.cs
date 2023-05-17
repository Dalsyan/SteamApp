﻿using SteamAPI.Models.AccountDTOs;
using SteamAPI.Models.GameDTOs;
using SteamDomain;

namespace SteamAPI.Models.UserDTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public bool HasPremium { get; set; }

        public AccountBaseDTO Account { get; set; }
        public ICollection<VoteBaseDTO?> Games { get; set; } = new List<VoteBaseDTO?>();
    }
}
