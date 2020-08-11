using System.Collections.Generic;
using Operations.Models;
using Operations.Models.BindingModels;
using Operations.Models.ViewModels;

namespace Operations
{
    public interface ITransationOperations
    {
        List<TransactionViewModel> Get();
        
        TransactionViewModel Get(string transactionId);

        TransactionViewModel Create(TransactionBindingModel bindingModel);
    }
}