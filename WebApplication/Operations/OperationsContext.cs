using System.Threading;
using DataAccess;
using Microsoft.Extensions.Caching.Memory;

namespace Operations
{
    public class OperationsContext
    {
        internal readonly IUnitOfWork UnitOfWork;
        private ITransationOperations _transactionOperations;
        private IBalanceOperations _balanceOperations;
        internal static ReaderWriterLock Lock = new ReaderWriterLock();

        
        public OperationsContext(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ITransationOperations TransationOperations =>
            _transactionOperations ??= new TransactionOperations(this);

        public IBalanceOperations BalanceOperations =>
            _balanceOperations ??= new BalanceOperations(this);
    }
}