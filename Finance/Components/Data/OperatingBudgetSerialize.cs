using System.Text.Json;
using System.Text.Json.Serialization;

namespace Finance.Components.Data
{
    public class OperatingBudget
    {
        [JsonPropertyName("year")]
        public string? Year { get; set; }

        [JsonPropertyName("service")]
        public  string? Service { get; set; }

        [JsonPropertyName("sub_service")]
        public string? SubService { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("budget_year")]
        public  string? BudgetYear { get; set; }

        [JsonPropertyName("year_plus_one_projection")]
        public string? YearOneProjection { get; set; }

        [JsonPropertyName("year_plus_two_projection")]
        public string? YearTwoProjection { get; set; }
    }
}
