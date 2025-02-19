using System.Data;
using System.Text;
using System.Transactions;
using Dapper;
using GAP.Core.Database.Repository;
using Npgsql;
using Serilog;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace GAP.Core.Database
{
    public interface ISqlWrapper
    {
        IDbConnection Connection { get; }
        TransactionScope GetTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(IDbConnection connection, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(IDbConnection connection, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<int> ExecuteAsync(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }

    public class SqlWrapper : ISqlWrapper
    {
        private readonly ILogger _log;

        public SqlWrapper(DbConnectionSettings settings)
        {
            _log = Log.ForContext<GapRepository>().ForContext("Data Access", GetType().ToString());
            _connectionString = settings.Database;
        }

        private readonly string _connectionString;

        public IDbConnection Connection =>
            Transaction.Current == null
                ? new NpgsqlConnection(_connectionString)
                : new NpgsqlConnection(_connectionString + ";Enlist=true");


        public TransactionScope GetTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return new TransactionScope(TransactionScopeOption.RequiresNew,
                new TransactionOptions
                {
                    IsolationLevel = isolationLevel,
                    Timeout = TimeSpan.FromMinutes(60)
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }
        
        private static void OpenConnectionAndLog(IDbConnection cnn)
        {
            if (cnn.State != ConnectionState.Open)
            {
                cnn.Open();
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(connection);

            return await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(IDbConnection connection, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(connection);

            return await connection.QueryAsync(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(IDbConnection connection, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(connection);

            return await connection.QueryAsync(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(cnn);

            return await cnn.QueryAsync(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }


        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(cnn);

            return await cnn.QueryAsync(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(cnn);

            return await cnn.QueryAsync(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(cnn);

            return await cnn.QueryAsync(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }


        public async Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(connection);

            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(connection);

            return await connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(connection);

            return await connection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<int> ExecuteAsync(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenConnectionAndLog(connection);
            
            return await connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
