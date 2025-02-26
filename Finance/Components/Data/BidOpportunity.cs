using System.Text.Json.Serialization;

namespace Finance.Components.Data
{
    public class BidOpportunity
    {
        [JsonPropertyName("bid_opportunity_number")]
        public string? BidOpportunityNumber { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("year")]
        public string? Year { get; set; }

        [JsonPropertyName("contract_officer")]
        public string? ContractOfficer { get; set; }

        [JsonPropertyName("contract_officer_email")]
        public string? ContractOfficerEmail { get; set; }

        [JsonPropertyName("contract_administrator")]
        public string? ContractAdministrator { get; set; }

        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("submission_deadline")]
        public DateTime SubmissionDeadline { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("transaction_commodity")]
        public string? TransactionCommodity { get; set; }

        [JsonPropertyName("documents_url")]
        public UrlObject? DocumentsUrl { get; set; }

        [JsonPropertyName("merx_url")]
        public UrlObject? MerxUrl { get; set; }
    }

    public class UrlObject
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
