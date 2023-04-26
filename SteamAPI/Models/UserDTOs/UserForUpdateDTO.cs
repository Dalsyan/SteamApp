namespace SteamAPI.Models.UserDTOs
{
    public class UserForUpdateDTO
    {
        public string Nickname { get; set; } = string.Empty;
        public int CountryId { get; set; }
    }
}
