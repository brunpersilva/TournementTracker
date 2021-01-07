using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {

        public int Id { get; set; }
        /// <summary>
        /// Represents one Team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        /// Team Identifier
        /// </summary>
        public int TeamCompetingId { get; set; }
        /// <summary>
        /// Represents the score for this particular team
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// represents the matchup that this came from as winner
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }

        /// <summary>
        ///  Represent the matchup this team came from
        /// </summary>
        public int ParentMatchupId { get; set; }



    }
}
