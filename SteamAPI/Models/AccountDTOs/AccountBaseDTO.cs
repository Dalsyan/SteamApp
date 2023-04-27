namespace SteamAPI.Models.AccountDTOs
{
    public class AccountBaseDTO
    {
        public int EmailId { get; set; }
        public string Email { get; set; }
        // public string Password { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
