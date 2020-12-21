using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Entities
{
    /// <summary>
    /// Represents what the prize is for a given place.
    /// </summary>
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier of the prizes
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The numeric identifier for the prize (2 for second place, etc)
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// The friendly name for the place (second place, third plance, etc)
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// The fixed amount this place earns, zero if not used
        /// </summary>
        public decimal PrizeAmount { get; set; }
        /// <summary>
        /// The number that represents the percentage of the overall take or zero if not used.
        /// The percentage is a fraction of 1 (0.5 = 50%)
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }
        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int placeNumberVlue = 0;
            int.TryParse(placeNumber, out placeNumberVlue);
            PlaceNumber = placeNumberVlue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0.0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;

        }
    }
}
