using Finance.Components.Data;

using Finance.Components.Pages;


namespace Finance.Components.Services
{
    /// <summary>
    /// Service for obtaining and interacting with the Bid Opportunity Data
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BidOpportunityService"/> class.
    /// </remarks>
    /// <param name="httpClient">An instance of <see cref="HttpClient"/> used to fetch data from the API.</param>
    public class BidOpportunityService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private List<BidOpportunity>? bidOpportunities;
        private const string Url = "https://data.winnipeg.ca/resource/rijt-92n4.json?$limit=50000";

        /// <summary>
        /// Asynchronously retrieves the list of bid opportunities from the API.
        /// If data has already been fetched, returns the cached list.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of <see cref="BidOpportunity"/>.</returns>
        public async Task<List<BidOpportunity>> GetBidOpportunitiesAsync()
        {
            if (bidOpportunities == null)
            {
                string jsonResponse = await _httpClient.GetStringAsync(Url);
                bidOpportunities = JsonHelper.DeserializeList<BidOpportunity>(jsonResponse);
            }
            return bidOpportunities;
        }

        /// <summary>
        /// Retrieves a dictionary of each year and the number of bid opportunities published in a certain year.
        /// </summary>
        /// <returns>A dictionary of each year and the number of bid opportunities published in a certain year</returns>
        public Dictionary<int, int> GetBidOpportunitiesPerYear()
        {
            if (bidOpportunities == null)
            {
                return [];
            }

            return bidOpportunities
                .Where(e => !string.IsNullOrEmpty(e.Year))  // Ensure Year is not null or empty
                .GroupBy(e => int.Parse(e.Year))  // Group by Year
                .OrderBy(g => g.Key)  // Sort by Year (converted to int)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Retrieves a List of top N contract officers and count of bid opportunities published/sanctioned by each officer in a descending manner
        /// </summary>
        /// <returns>A List of top N contract officers and count of bid opportunities published/sanctioned by each officer in a descending manner</returns>
        public List<(string? ContractOfficer, int Count)> GetTopContractOfficers(int topN)
        {
            if (bidOpportunities == null)
            {
                return [];
            }

            return bidOpportunities
                .Where(e => !string.IsNullOrEmpty(e.ContractOfficer)) // Ensure Contract officer for Bid is not empty
                .GroupBy(e => e.ContractOfficer) // Group By Contract officers
                .OrderByDescending(g => g.Count()) // Order all the contracts per officer by descending
                .Take(topN) // Extract the top n officers
                .Select(g => (g.Key, g.Count()))
                .ToList();
        }
    }
}
