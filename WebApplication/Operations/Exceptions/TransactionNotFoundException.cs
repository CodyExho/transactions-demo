using System;

namespace Operations.Exceptions
{
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(string id) : base($"Transaction with id {id} not found")
        {}
    }
}