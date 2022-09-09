using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace SampleSonar.Data.Extensions
{
    public class MySqlGenerator<T> : SqlGenerator<T> where T : class
    {
        public MySqlGenerator() : base(SqlProvider.MSSQL) { }
    }
}
