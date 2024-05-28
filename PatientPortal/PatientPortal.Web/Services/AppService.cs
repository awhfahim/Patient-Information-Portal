using System.Net.Http.Headers;

namespace PatientPortal.Web.Services;

public class ApiService
{
    private readonly HttpClient _client = new();

    public async Task<string> GetFromApiAsync(string jwtToken, string apiUrl)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await _client.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return null; // Handle error appropriately in real scenarios
    }

    public async Task<string> PostToApiAsync(string jwtToken, string apiUrl, object requestData)
    {
        //string json = JsonConvert.SerializeObject(requestData);
       // var content = new StringContent(json, Encoding.UTF8, "application/json");

       // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        // HttpResponseMessage response = await _client.PostAsync(apiUrl, content);
        // if (response.IsSuccessStatusCode)
        // {
        //     return await response.Content.ReadAsStringAsync();
        // }

        return null; // Handle error appropriately in real scenarios
    }
}