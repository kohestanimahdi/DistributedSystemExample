using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace DistributedSystems.Common.Utilities
{
    public class RedisMessagePassing
    {
        private readonly string connectionString;

        public RedisMessagePassing(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task SendMessage(object data, string channel= "LastMomentFlightRequest")
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);
            var pubsub = redis.GetSubscriber();
            await pubsub.PublishAsync(channel, JsonConvert.SerializeObject(data));
            redis.Close();
        }
    }
}
