using System.ComponentModel.DataAnnotations;

namespace SteamDomain
{
    public class Developer
    {
        public Developer() {
            Games = new List<Game>();
        }
        [Key]
        public int DevId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public int CompanyId { get; set; }
        public List<Game> Games { get; set; }
    }
}