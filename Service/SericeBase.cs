using System.Data.Common;

namespace M_5_S_1.Service
{
    public abstract class ServiceBase
    {
        public abstract DbConnection GetConnection();
        public abstract DbCommand GetCommand(DbConnection connection, string commandText);
        public abstract void AddParameter(DbCommand command, string parameterName, object value);
    }
}
