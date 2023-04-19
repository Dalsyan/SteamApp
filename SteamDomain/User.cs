namespace SteamDomain
{
    public class User
    {
        public User()
        {
            Games = new List<Game>();
        }
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public Account EmailId { get; set; }
        public int CountryId { get; set; }
        public List<Game> Games { get; set; }
        
    }
}
