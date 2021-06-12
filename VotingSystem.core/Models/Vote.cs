using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.core.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CounterId { get; set; }
    }
}
