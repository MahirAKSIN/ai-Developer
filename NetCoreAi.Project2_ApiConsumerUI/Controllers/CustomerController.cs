using Microsoft.AspNetCore.Mvc;
using NetCoreAi.Project2_ApiConsumerUI.Dtos;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Text.Json;

public class CustomerController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomerController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    [HttpGet]
    public async Task<IActionResult> CustomerList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7169/api/Customers");

        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonData);
            var values = new List<ResultCustomerDto>();

            foreach (var item in document.RootElement.EnumerateArray())
            {
                item.TryGetProperty("customerId", out var idProp);
                item.TryGetProperty("customerName", out var nameProp);
                item.TryGetProperty("customerSurName", out var surNameProp);
                item.TryGetProperty("customerBalance", out var balanceProp);

                values.Add(new ResultCustomerDto
                {
                    CustomerId = idProp.ValueKind == JsonValueKind.Number && idProp.TryGetInt32(out var id) ? id : 0,
                    CustomerName = nameProp.GetString() ?? string.Empty,
                    CustomerSurName = surNameProp.GetString() ?? string.Empty,
                    CustomerBalance = ReadBalance(balanceProp)
                });
            }

            return View(values);
        }

        return View(new List<ResultCustomerDto>());
    }
    private static decimal ReadBalance(JsonElement balanceProp)
    {
        if (balanceProp.ValueKind == JsonValueKind.Number && balanceProp.TryGetDecimal(out var numericValue))
            return numericValue;

        if (balanceProp.ValueKind == JsonValueKind.String)
        {
            var rawText = balanceProp.GetString();
            if (decimal.TryParse(rawText, NumberStyles.Any, CultureInfo.InvariantCulture, out var stringValue))
                return stringValue;
        }

        return 0m;
    }
    [HttpGet]
    public IActionResult CreateCustomer()
    {
        return View(new CreateCustomerDto());
    }
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
    {
        if (!ModelState.IsValid)
        {
            return View(createCustomerDto);
        }

        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createCustomerDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7169/api/Customers", stringContent);

        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("CustomerList");
        }

        var errorBody = await responseMessage.Content.ReadAsStringAsync();
        ModelState.AddModelError(string.Empty, $"Kayit basarisiz. API: {(int)responseMessage.StatusCode} {responseMessage.ReasonPhrase} {errorBody}");
        return View(createCustomerDto);
    }
    [HttpGet]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var client = _httpClientFactory.CreateClient();

        var responseMessage = await client.DeleteAsync($"https://localhost:7169/api/Customers/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("CustomerList");

        }
        var errorBody = await responseMessage.Content.ReadAsStringAsync();
        TempData["DeleteError"] = $"Silme basarisiz. API: {(int)responseMessage.StatusCode} {responseMessage.ReasonPhrase} {errorBody}";
        return RedirectToAction("CustomerList");


    }
    [HttpGet]
    public async Task<IActionResult> UpdateCustomer(int id)
    {

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"https://localhost:7169/api/Customers/{id}");

        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<GetByIdCustomerDto>(jsonData);
            if (customer == null)
            {
                return View(new UpdateCustomerDto());
            }

            var updateDto = new UpdateCustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                CustomerSurName = customer.CustomerSurName,
                CustomerBalance = customer.CustomerBalance
            };

            return View(updateDto);
        }
        return View(new UpdateCustomerDto());
    }
    [HttpPost]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
    {

        if (!ModelState.IsValid)
        {
            return View(updateCustomerDto);
        }

        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateCustomerDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PutAsync("https://localhost:7169/api/Customers", stringContent);

        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("CustomerList");
        }

        var errorBody = await responseMessage.Content.ReadAsStringAsync();
        ModelState.AddModelError(string.Empty, $"Guncelleme basarisiz. API: {(int)responseMessage.StatusCode} {responseMessage.ReasonPhrase} {errorBody}");
        return View(updateCustomerDto);
    }

}
