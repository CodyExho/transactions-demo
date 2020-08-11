using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DataAccess.Entities;
using Operations.Exceptions;
using Operations.Models.BindingModels;
using Operations.Models.ViewModels;

namespace Operations
{
    public class TransactionOperations : ITransationOperations
    {
        private readonly OperationsContext _operationsContext;
        public TransactionOperations(OperationsContext operationsContext)
        {
            _operationsContext = operationsContext;
        }
        
        public List<TransactionViewModel> Get()
        {
            List<TransactionViewModel> transactions;
            try
            {
                OperationsContext.Lock.AcquireReaderLock(1000);
                transactions = _operationsContext.UnitOfWork.GetRepository<TransactionEntity>().All?.Select(el =>
                    new TransactionViewModel
                    {
                        Id = el.Id,
                        Type = el.Type,
                        Amount = el.Amount,
                        EffectiveTime = el.EffectiveDate
                    }).ToList();
            }
            finally
            {
                OperationsContext.Lock.ReleaseReaderLock();
            }

            return transactions;
        }

        public TransactionViewModel Get(string transactionId)
        {
            TransactionEntity entity;
            try
            {
                OperationsContext.Lock.AcquireReaderLock(1000);
                entity = _operationsContext.UnitOfWork.GetRepository<TransactionEntity>().Find(transactionId);
            }
            finally
            {
                OperationsContext.Lock.ReleaseReaderLock();
            }

            if (entity == null)
            {
                throw new TransactionNotFoundException(transactionId);
            }
            return new TransactionViewModel
            {
                Id = entity.Id,
                Type = entity.Type,
                Amount = entity.Amount,
                EffectiveTime = entity.EffectiveDate
            };
        }

        public TransactionViewModel Create(TransactionBindingModel bindingModel)
        {
            TransactionEntity entity;
            try
            {
                OperationsContext.Lock.AcquireWriterLock(1000);
                var balance = _operationsContext.BalanceOperations.GetBalance();
                if (bindingModel.Type == TransactionType.Credit && balance.Balance - bindingModel.Amount < 0)
                {
                    throw new InvalidTransactionException();
                }

                entity = _operationsContext.UnitOfWork.GetRepository<TransactionEntity>().Insert(
                    new TransactionEntity
                    {
                        Type = bindingModel.Type,
                        Amount = bindingModel.Amount,
                    });
            }
            finally
            {
                OperationsContext.Lock.ReleaseWriterLock();
            }

            return new TransactionViewModel
            {
                Id = entity.Id,
                Type = entity.Type,
                Amount = entity.Amount,
                EffectiveTime = entity.EffectiveDate
            };
        }
    }
}