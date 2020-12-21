using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Entities
{
    public class MatchupModel
    {
        public List<MatchupEntryModel> Entries { get; set; }
        public TeamModel Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}
