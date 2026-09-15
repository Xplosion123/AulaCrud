using MySql.Data.MySqlClient;

namespace AulaCrud.Data
{
    public class DataBase
    {
        private readonly string connectionString = "server=localhost;port=3305;database=Oficina_db;user=root;password=12345678;";
        public MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
