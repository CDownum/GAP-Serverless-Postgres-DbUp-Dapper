using System.Transactions;
using AutoMapper;
using Serilog;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace GAP.Core.Database.Repository
{
    public interface IGapRepository
    {
        ISqlWrapper SqlWrapper { get; }
        TransactionScope GetTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }

    public partial class GapRepository : IGapRepository
    {
        private readonly ILogger _log;
        private readonly IMapper _mapper;

        public GapRepository(ISqlWrapper wrapper, IMapper mapper)
        {
            _log = Log.ForContext<GapRepository>().ForContext("Data Access", GetType().ToString());
            SqlWrapper = wrapper;
            _mapper = mapper;
        }

        public ISqlWrapper SqlWrapper { get; }

        public TransactionScope GetTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return SqlWrapper.GetTransactionScope(isolationLevel);
        }
    }
}