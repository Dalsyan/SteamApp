using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Server
    {
        [Key]
        public int ServerId { get; set; }
        [Required]
        public string ServerName { get; set; }

        [ForeignKey("Game")]
        public int? GameId { get; set; }
        public Game? Game { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}

