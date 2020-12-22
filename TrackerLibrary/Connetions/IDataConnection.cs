using TrackerLibrary.Models;

namespace TrackerLibrary.Connections
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel model);
    }
}
