using Finance.Components.Data;

namespace Finance.Components.Services
{
    /// <summary>
    /// Service for retrieving and managing council member expense data.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="WardExpenseService"/> class.
    /// </remarks>
    /// <param name="httpClient">An instance of <see cref="HttpClient"/> used to fetch data from the API.</param>
    public class WardExpenseService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private List<WardExpense>? councilMemberExpenses;
        private const string Url = "https://data.winnipeg.ca/resource/mgde-4fua.json?$limit=50000";

        /// <summary>
        /// Asynchronously retrieves the list of council member expenses from the API.
        /// If data has already been fetched, returns the cached list.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of <see cref="WardExpense"/>.</returns>
        public async Task<List<WardExpense>> GetExpensesAsync()
        {
            if (councilMemberExpenses == null)
            {
                string jsonResponse = await _httpClient.GetStringAsync(Url);
                councilMemberExpenses = JsonHelper.DeserializeList<WardExpense>(jsonResponse);
            }
            return councilMemberExpenses;
        }

        /// <summary>
        /// Retrieves an array of counts representing the number of expense records for each unique council member.
        /// </summary>
        /// <returns>An array of doubles representing the count of expenses per council member.</returns>
        public double[] GetCouncilMemberCounts()
        {
            if (councilMemberExpenses == null)
            {
                return [];
            }

            // Count occurrences of each unique CouncilMember
            return councilMemberExpenses
                .Where(e => !string.IsNullOrEmpty(e.CouncilMember))
                .GroupBy(e => e.CouncilMember)
                .Select(g => (double)g.Count())
                .ToArray();
        }

        /// <summary>
        /// Retrieves a distinct list of council member names, formatted with their ward office.
        /// </summary>
        /// <returns>An array of strings containing council member names and their ward offices.</returns>
        public string[] GetCouncilMemberNames()
        {
            if (councilMemberExpenses == null)
            {
                return [];
            }

            // Get a distinct list of council member names
            return councilMemberExpenses
                .Where(e => !string.IsNullOrEmpty(e.CouncilMember))
                .Select(e => e.CouncilMember + " - " + e.WardOffice)
                .Distinct()
                .ToArray();
        }
    }
}
