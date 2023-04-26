namespace SteamAPI.Models.UserDTOs
{
    public class UserBaseDTO
    {
        public int UserId { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public int EmailId { get; set; }
    }
}
