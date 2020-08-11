using System;
using Common.Enums;
using DataAccess.Entities;
using Operations.Models.ViewModels;

namespace Operations
{
    public class BalanceOperations : IBalanceOperations
    {
        private readonly OperationsContext _operationsContext;
        
        public BalanceOperations(OperationsContext operationsContext)
        {
            _operationsContext = operationsContext;
        }
        
        public BalanceViewModel GetBalance()
        {
            int balance = 0;
            try
            {
                OperationsContext.Lock.AcquireReaderLock(1000);
                var transactions = _operationsContext.UnitOfWork.GetRepository<TransactionEntity>().All;

                if (transactions == null)
                {
                    return new BalanceViewModel
                    {
                        Balance = 0
                    };
                }
                foreach (var transaction in transactions)
                {
                    switch (transaction.Type)
                    {
                        case TransactionType.Debit:
                            balance += transaction.Amount;
                            break;
                        case TransactionType.Credit:
                            balance -= transaction.Amount;
                            break;
                    }
                }
            }
            finally
            {
                OperationsContext.Lock.ReleaseReaderLock();
            }
            
            return new BalanceViewModel
            {
                Balance = balance
            };
        }
    }
}