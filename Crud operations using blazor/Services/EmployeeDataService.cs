using Crud_operations_using_blazor.Services.Interfaces;
using System.Net.Http.Json;
using Tables;

namespace Crud_operations_using_blazor.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUri = "https://localhost:7268"; // Replace with your API base URL

        public EmployeeDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEmployee(Employee employee)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{baseUri}/Employee", employee);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error creating employee: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create employee.", ex);
            }
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var response = await _httpClient.DeleteAsync($"{baseUri}/Employee/{employeeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>($"{baseUri}/Employee");
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"{baseUri}/Employee/{employeeId}");
        }

        public async Task UpdateEmployee(Employee updatedEmployee)
        {
            await _httpClient.PutAsJsonAsync($"{baseUri}/Employee/{updatedEmployee.Id}", updatedEmployee);
        }





    }

}
