using Operations.Models.ViewModels;

namespace Operations
{
    public interface IBalanceOperations
    {
        BalanceViewModel GetBalance();
    }
}