using System.Text.Json;
using System.Text.Json.Serialization;

namespace Finance.Components.Data
{
    public class WardExpense
    {
        [JsonPropertyName("ward_id")]
        public  string? WardId { get; set; }

        [JsonPropertyName("ward_office")]
        public  string? WardOffice { get; set; }

        [JsonPropertyName("council_member")]
        public  string? CouncilMember { get; set; }

        [JsonPropertyName("journal_date")]
        public DateTime JournalDate { get; set; }

        [JsonPropertyName("vendor")]
        public  string? Vendor { get; set; }

        [JsonPropertyName("expense_type")]
        public string? ExpenseType { get; set; }

        [JsonPropertyName("description")]
        public  string? Description { get; set; }

        [JsonPropertyName("account")]
        public  string? Account { get; set; }

        [JsonPropertyName("amount")]
        public  string? Amount { get; set; }

        public double AmountNumeric => double.TryParse(Amount, out var value) ? value : 0;
    }
}
