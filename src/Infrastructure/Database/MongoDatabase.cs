using System;
using System.Threading;
using Infrastructure.Database.Connection;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Infrastructure.Database 
{
    public class MongoDatabase : IMongoConnect
    {
        public IMongoDatabase db {get;set;}
        public bool IsConnected {get;set;}
        public MongoDatabase(IConfiguration _configuration)
        {
            try
            {   
                var _ConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
                ConventionRegistry.Register("camelCase",_ConventionPack, t => true);
                var _MongoDBClient = new MongoClient(_configuration.GetSection("ConnectionString").
                                            GetSection("DefaultConnection").Value.ToString());
                db = _MongoDBClient.GetDatabase(_configuration["NomeBanco"]);
            }
            catch (Exception e) 
            {
                throw new Exception("Erro no Banco de Dados", e);
            }
            
        }
        
    }
}