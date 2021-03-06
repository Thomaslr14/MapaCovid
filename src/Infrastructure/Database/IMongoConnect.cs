using MongoDB.Driver;

namespace Infrastructure.Database
{
    public interface IMongoConnect
    {
        IMongoDatabase db{get;set;}
        bool IsConnected {get;set;}
    }
}