namespace SteamAPI.Models.UserDTOs
{
    public class UserForCreationDTO
    {
        public string Nickname { get; set; } = string.Empty;
        public int CountryId { get; set; }
    }
}
