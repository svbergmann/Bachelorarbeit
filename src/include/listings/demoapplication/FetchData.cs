private static async Task<IEnumerable<CountryModel>> GetCountriesAsync(HttpClient client) {
    var response = await client.GetAsync("Countries/shadow");
    return response.IsSuccessStatusCode ?
        JsonConvert.DeserializeObject<List<CountryModel>>(response.Content.ReadAsStringAsync().Result)
        : null;
}

private static async Task<IEnumerable<AirportModel>> GetAirportsAsync(HttpClient client, string icao) {
    var response = await client.GetAsync($"Airports/shadow/icao?icaoCode={icao}");
    return response.IsSuccessStatusCode ?
        JsonConvert.DeserializeObject<List<AirportModel>>(response.Content.ReadAsStringAsync().Result)
        : null;
}

private static async Task<IEnumerable<SidStarApproachModel>> GetSidsStarsApproachesAsync(HttpClient client, string airportIdent, SidStarApproachEnum sidStarApproachEnum) {
    var response = await client.GetAsync($"SidsStarsApproaches/shadow/portIdent?portIdent={airportIdent}&sidStarApproachType={(int) sidStarApproachEnum}");
    return response.IsSuccessStatusCode ?
        JsonConvert.DeserializeObject<List<SidStarApproachModel>>(response.Content.ReadAsStringAsync().Result)
        : null;
}

private static async Task<IEnumerable<SidStarApproachModel>> GetSortedLegsAsync(HttpClient client,
    string custareaCode, string icaoCode, string ssaIdent, string transitionIdent) {
    var res = await client.GetAsync($"SidsStarsApproaches/shadow/sortedLegs?custareaCode={custareaCode}&icaoCode={icaoCode}&ssaIdent={ssaIdent}&transitionIdent={transitionIdent}");
    return res.IsSuccessStatusCode ?
        JsonConvert.DeserializeObject<List<SidStarApproachModel>>(res.Content.ReadAsStringAsync().Result)
        : null;
}

private static async Task<List<Waypoint>> GetWaypointsAsync(HttpClient client, string custareaCode, string icaoCode, string ident) {
    var res = await client.GetAsync($"Waypoints/full?custareaCode={custareaCode}&icaoCode={icaoCode}&ident={ident}");
    return res.IsSuccessStatusCode ?
        JsonConvert.DeserializeObject<List<Waypoint>>(res.Content.ReadAsStringAsync().Result)
        : null;
}