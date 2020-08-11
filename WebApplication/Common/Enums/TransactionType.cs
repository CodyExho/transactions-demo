using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionType
    {
        [EnumMember(Value = "debit")]
        Debit,
        [EnumMember(Value = "credit")]
        Credit
    }
}