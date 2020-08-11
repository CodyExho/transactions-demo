using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Operations.Models.BindingModels
{
    public class TransactionBindingModel
    {
        [Required]
        public TransactionType Type { get; set; }
        [Range(0, 100)]
        [Required]
        public int Amount { get; set; }
    }
}