namespace Catalog.Configuration
{
    public class MongoDBConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectiongString 
        { 
            get 
            {
                return $"mongodb://{User}:{Password}@{Host}:{Port}";
            } 
        }
    }
}