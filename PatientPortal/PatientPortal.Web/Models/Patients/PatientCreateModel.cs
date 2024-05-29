using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PatientPortal.Web.Models.Patients;

public class PatientCreateModel
{
    [Required] public string Name { get; set; }
    [Required] public uint Age { get; set; }
    [Required] public string Gender { get; set; }
    [Required] public string BloodGroup { get; set; }
    [Required] public int EpilepsyStatus { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] public Guid DiseaseInfoId { get; set; }
    public List<string> NcdDetails { get; set; } = [];
    public List<string> AllergiesDetails { get; set; } = [];
    
    public async Task<List<DiseaseInfoModel>> FetchDiseaseInfo(HttpClient client)
    {
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetTokenAsync());
        var response = await client.GetAsync("https://localhost:7236/api/diseases").ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var diseaseInfo = JsonSerializer.Deserialize<List<DiseaseInfoModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return diseaseInfo;
    }
    
    public async Task<List<NcdModel>> FetchNcds(HttpClient client)
    {
        var response = await client.GetAsync("https://localhost:7236/api/ncds").ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var ncds = JsonSerializer.Deserialize<List<NcdModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return ncds;
    }

    public async Task<List<AllergieModel>> FetchAllergies(HttpClient client)
    {
        var response = await client.GetAsync("https://localhost:7236/api/allergies").ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var allergies = JsonSerializer.Deserialize<List<AllergieModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return allergies;
    }
}