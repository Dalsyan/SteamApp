using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamDomain
{
    public class Vote
    {
        public int VoteId { get; set; }
        public double Score { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
