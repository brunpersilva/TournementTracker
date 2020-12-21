using TrackerLibrary.Entities;

namespace TrackerLibrary.Connections
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel model);
    }
}
