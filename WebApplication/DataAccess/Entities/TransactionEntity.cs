using System;
using Common.Enums;

namespace DataAccess.Entities
{
    public class TransactionEntity : AbstractEntity
    {
        public int Amount { get; set; }
        
        public TransactionType Type { get; set; }
        
        public DateTimeOffset EffectiveDate { get; set; } = DateTimeOffset.UtcNow;
    }
}