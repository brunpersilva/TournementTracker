using System.Collections.Generic;
using TrackerLibrary.Models;

namespace TrackerLibrary.Connections
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel model);
        List<PersonModel> GetPerson_All();

    }
}
