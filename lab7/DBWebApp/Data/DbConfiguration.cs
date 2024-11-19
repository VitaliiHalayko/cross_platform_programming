using DBWebApp.Data;

public class DbConfiguration
{
    public string DatabaseProvider { get; set; }  // MS-SQL, PostgreSQL, SQLite, In-Memory
    public string ConnectionString { get; set; }
}
