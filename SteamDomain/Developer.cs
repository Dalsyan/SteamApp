using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Developer
    {
        [Key]
        public int DevId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Game?> Games { get; set; } = new List<Game?>();
    }
}