using Finance.Components.Data;
using Finance.Components.Pages;

namespace Finance.Components.Services
{
    /// <summary>
    /// Service for retrieving and managing council member expense data.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="WardExpenseService"/> class.
    /// </remarks>
    /// <param name="httpClient">An instance of <see cref="HttpClient"/> used to fetch data from the API.</param>
    public class OperatingBudgetService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private List<OperatingBudget>? operatingBruhget;
        private const string Url = "https://data.winnipeg.ca/resource/xkie-s6ee.json";

        /// <summary>
        /// Asynchronously retrieves the list of council member expenses from the API.
        /// If data has already been fetched, returns the cached list.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of <see cref="WardExpense"/>.</returns>
        public async Task<List<OperatingBudget>> GetExpensesAsync()
        {
            if (operatingBruhget == null)
            {
                string jsonResponse = await _httpClient.GetStringAsync(Url);
                operatingBruhget = JsonHelper.DeserializeList<OperatingBudget>(jsonResponse);
            }
            return operatingBruhget;
        }
        public string[] GetBudgetServices()
        {
            if (operatingBruhget == null)
            {
                return [];
            }

            // Get a distinct list of council member names
            return operatingBruhget
             .Where(e => !string.IsNullOrEmpty(e.Service))
             .Select(e => e.Service + " - " + e.SubService)
             .Distinct()
             .ToArray();
        }
        public double[] GetBudgets()
        {
            if (operatingBruhget == null)
            {
                return [];
            }

            // Count occurrences of each unique CouncilMember
            return operatingBruhget
                .Where(e => !string.IsNullOrEmpty(e.BudgetYear))
                .GroupBy(e => e.BudgetYear)
                .Select(g => (double)g.Count())
                .ToArray();
        }

    }
}
