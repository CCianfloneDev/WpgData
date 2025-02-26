using Finance.Components.Data;
using MudBlazor;

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

        /// <summary>
        /// Returns formatter bar chart data, total revenue and expendatures / 100000
        /// </summary>
        /// <returns>Chart data</returns>
        public (List<ChartSeries> Series, string[] XAxisLabels) GetBarChartData()
        {
            if (operatingBruhget == null)
            {
                return (new List<ChartSeries>(), Array.Empty<string>());
            }

            var services = operatingBruhget
                .GroupBy(e => e.Service)
                .Select(g => new
                {
                    Service = g.Key.Length > 15 ? g.Key[..15] + "..." : g.Key, 
                    OriginalService = g.Key,
                    TotalBudget = Math.Abs(g.Sum(e => double.TryParse(e.BudgetYear, out double result) ? result : 0))
                })
                .OrderByDescending(x => x.TotalBudget)
                .Take(5)
                .ToArray();

            var revenueSeries = new ChartSeries
            {
                Name = "Revenue",
                Data = services.Select(s => Math.Abs(operatingBruhget
                    .Where(e => e.Service == s.OriginalService && e.Category == "REVENUE")
                    .Sum(e => double.TryParse(e.BudgetYear, out double result) ? result : 0)) / 1000000)
                    .ToArray()
            };

            var expenditureSeries = new ChartSeries
            {
                Name = "Expenditure",
                Data = services.Select(s => operatingBruhget
                    .Where(e => e.Service == s.OriginalService && e.Category == "EXPENDITURES")
                    .Sum(e => double.TryParse(e.BudgetYear, out double result) ? result : 0) / 1000000)
                    .ToArray()
            };

            return (new List<ChartSeries> { revenueSeries, expenditureSeries }, services.Select(s => s.Service).ToArray());
        }

        public double[] GetBudgets()
        {
            if (operatingBruhget == null)
            {
                return [];
            }

            return operatingBruhget
                .Where(e => !string.IsNullOrEmpty(e.BudgetYear))
                .GroupBy(e => e.BudgetYear)
                .Select(g => (double)g.Count())
                .ToArray();
        }
    }
}
