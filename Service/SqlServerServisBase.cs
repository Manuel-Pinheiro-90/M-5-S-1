using System.Data.Common;
using System.Data.SqlClient;

namespace M_5_S_1.Service
{
    public class SqlServerServiceBase
    {
        private readonly DbConnection _connection;

        public SqlServerServiceBase(DbConnection connection)
        {
            _connection = connection;
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, (SqlConnection)_connection);
        }

        public void AddParameter(DbCommand command, string parameterName, object value)
        {
            if (command is SqlCommand sqlCmd)
            {
                sqlCmd.Parameters.AddWithValue(parameterName, value);
            }
        }
    }
}
