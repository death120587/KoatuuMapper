namespace Common.Model
{
    public class MapperConfig
    {
        public DatabaseConfig Database { get; set; }
        public SqlLiteConfig Sqllite { get; set; }
    }

    public class DatabaseConfig
    { 
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
    }

    public class SqlLiteConfig
    { 
        public string Path { get; set; }
        public bool RecreateDb { get; set; }
    }
}
