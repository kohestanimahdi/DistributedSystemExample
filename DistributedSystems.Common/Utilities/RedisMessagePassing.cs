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
        public async Task SendMessage(object data)
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);
            var pubsub = redis.GetSubscriber();
            await pubsub.PublishAsync("SendSMSMessage", JsonConvert.SerializeObject(data));
            redis.Close();
        }
    }
}
