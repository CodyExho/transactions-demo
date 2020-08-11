using System;

namespace Operations.Exceptions
{
    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException() : base("Balance cannot be lower 0")
        {
            
        }
    }
}