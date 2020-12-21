using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Entities;

namespace TrackerLibrary.Connections
{
    class SqlConnector : IDataConnection
    {
        //TODO - Make the create prize method actually save to the database
        /// <summary>
        /// Saves a new prize to the SQL databse
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns>The prize information, including unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;
            return model;
        }

    }
}
