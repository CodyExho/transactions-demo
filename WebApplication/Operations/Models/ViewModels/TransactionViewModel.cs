using System;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Operations.Models.ViewModels
{
    public class TransactionViewModel
    {
        public string Id { get; set; }
        
        public TransactionType Type { get; set; }
        
        public int Amount { get; set; }
        
        public DateTimeOffset EffectiveTime { get; set; }        
    }
}