
namespace IdentityAndSecurityMicroservice.Application
{
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            Secret = default!;
            ServiceSettings = default!;
            MongoDbSettings = default!;
        }
        public string Secret { get; set; }
        public ServiceSettings ServiceSettings { get; set; }
        public MongoDbSettings MongoDbSettings { get; set; }
    }

    public class ServiceSettings
    {
        public ServiceSettings()
            => ServiceName = default!;
        
        public string ServiceName { get; set; }
    }

    public class MongoDbSettings
    {
        public MongoDbSettings()
        {
            Host = default!;
            Port = default!;
        }

        public string Host { get; set; }
        public string Port { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}
