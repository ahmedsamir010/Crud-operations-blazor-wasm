using Tables;

namespace Crud_operations_using_blazor.Services.Interfaces
{
    public interface IEmployeeDataService
    {
        Task CreateEmployee(Employee employee);


        Task<Employee> GetEmployeeById(int employeeId);


        Task<IEnumerable<Employee>> GetAllEmployees();


        Task UpdateEmployee(Employee updatedEmployee);


        Task<bool> DeleteEmployee(int employeeId);
    }
}
