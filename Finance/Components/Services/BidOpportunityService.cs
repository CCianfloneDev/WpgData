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

        public async Task<List<BidOpportunity>> GetBidOpportunitiesAsync()
        {
            if (bidOpportunities == null)
            {
                string jsonResponse = await _httpClient.GetStringAsync(Url);
                bidOpportunities = JsonHelper.DeserializeList<BidOpportunity>(jsonResponse);
            }
            return bidOpportunities;
        }

        public double[] GetBidStatusCounts()
        {
            if (bidOpportunities == null)
            {
                return [];
            }

            // Count occurrences of each bid status
            return bidOpportunities
                .Where(e => !string.IsNullOrEmpty(e.BidOpportunityNumber))
                .GroupBy(e => e.Year)
                .Select(g => (double)g.Count())
                .ToArray();
        }


    }
}
