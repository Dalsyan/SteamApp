using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamDomain
{
    public class Score
    {
        [Key]
        [Range(1, 5)]
        public float ScoreV { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
