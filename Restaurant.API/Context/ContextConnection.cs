namespace Restaurant.API.Context;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

public static class ContextConnection {

    public static IMongoDatabase GetDatabase() {
        var configuration =  new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);    
        IConfiguration config = configuration.Build();
        var contextConfiguration = config.GetSection("Mongodb").Get<ContextConfiguration>();

        var mongoClient = new MongoClient(contextConfiguration?.ConnectionString);
        return mongoClient.GetDatabase(contextConfiguration?.DatabaseName);
        
    }
}