namespace IPGeoGuard.lib.storage;
/*
# install .NET Client for Redis
# https://github.com/redis/NRedisStack

dotnet add package NRedisStack

# c
https://aws.amazon.com/memorydb/

*/
internal class RedisStorage : AFileStorage
{    
    // SET HERE YOU AWS Memory_DB ENDPOINT
    private const string AWS_MEMORY_DB_ENDPOINT = "clustercfg.lambda-db.8edggq.memorydb.sa-east-1.amazonaws.com:6379";

    // AFTER CONFIGURING AWS MEMORY_DB UNCOMMENT THIS LINE AND REMOVE THE LOCAL INTERFACE IDatabase BELOW
    //StackExchange.Redis.IDatabase redisDatabase;
    interface IDatabase
    {
        string StringGet(string key);
        void StringSet(string key, string value);
    }
    IDatabase redisDatabase;
    public RedisStorage(string serviceName) : base(serviceName)
    {
        //var redis = StackExchange.Redis.ConnectionMultiplexer.Connect(AWS_MEMORY_DB_ENDPOINT);
        //redisDatabase = redis.GetDatabase();
        throw new NotImplementedException("MUST CREATE AWS MemoryDB https://aws.amazon.com/memorydb/");
    }

    internal override string? Read()
    {
        return
            redisDatabase.StringGet(GetFileName());
    }

    internal override void Persist(string json)
    {
        redisDatabase.StringSet(GetFileName(),json);
    }
}