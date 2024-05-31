using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientPortal.Web.Models;
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
    
        try
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:7236/api/Patients", model);

            if (response.IsSuccessStatusCode)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Patient created successfully",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            else
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "An error occurred while creating the patient",
                    Type = ResponseTypes.Danger
                });

                return View("Error", new ErrorViewModel(){RequestId = "An error occurred while creating the patient"});
            }
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel(){RequestId = "An error occurred while creating the patient"});
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
    public async Task<IActionResult> Profile(Guid id)
    {
        var response = await httpClient.GetFromJsonAsync<PatientModel>($"https://localhost:7236/api/Patients/{id}");
        return View(response);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await httpClient.DeleteAsync($"https://localhost:7236/api/Patients/{id}");
        TempData.Put("ResponseMessage", new ResponseModel
        {
            Message = "Patient deleted successfully",
            Type = ResponseTypes.Success
        });
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        TempData.Put("ResponseMessage", new ResponseModel
        {
            Message = "An error occurred while deleting the patient",
            Type = ResponseTypes.Danger
        });
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await httpClient.GetFromJsonAsync<PatientUpdateModel>($"https://localhost:7236/api/Patients/{id}");
        return View(response);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PatientUpdateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var response = await httpClient.PutAsJsonAsync($"https://localhost:7236/api/Patients", model);
        var content = await response.Content.ReadAsStringAsync();
        return RedirectToAction("Profile", new { id = model.Id });
    }
}

