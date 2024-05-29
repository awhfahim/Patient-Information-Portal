using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PatientPortal.Web.Models.Patients;

namespace PatientPortal.Web.Controllers;
public class PatientController(HttpClient httpClient) : Controller
{
    [HttpGet]
    public async Task<IActionResult> CreatePatient()
    {
        var model = new PatientCreateModel();
        
        var diseaseInfoModels = await model.FetchDiseaseInfo(httpClient).ConfigureAwait(false);
        var ncdModels = await model.FetchNcds(httpClient).ConfigureAwait(false);
        var allergieModels = await model.FetchAllergies(httpClient).ConfigureAwait(false);
        
        ViewBag.DiseaseInfo = new SelectList(diseaseInfoModels, "Id", "Name");
        ViewBag.NcdModels = ncdModels;
        ViewBag.AllergieModel = allergieModels;
        
        return View(model);
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePatient(PatientCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
    
        try
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:7236/api/Patients", data);

            if (response.IsSuccessStatusCode)
            {
                //var responseBody = await response.Content.ReadAsStringAsync();
                return View();
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseBody);
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            return View("Error");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response =  httpClient
            .GetFromJsonAsAsyncEnumerable<PatientModel>("https://localhost:7236/api/Patients", new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true, 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    
                });
        return View(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await httpClient.GetAsync($"https://localhost:7236/api/Patients/{id}");
        var responseBody = await response.Content.ReadAsStringAsync();
        var patient = JsonConvert.DeserializeObject<PatientModel>(responseBody);
        
        return View(patient);
    }
}

