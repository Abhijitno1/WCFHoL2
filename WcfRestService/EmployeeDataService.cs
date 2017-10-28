using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfRestService
{
    [ServiceBehavior(InstanceContextMode= InstanceContextMode.Single)]
    public class EmployeeDataService: IEmployeeData
    {
        List<Employee> empList = new List<Employee>();
        int employeeCount = 0;

        public void SubmitEmployee(Employee employee)
        {
            employee.Id = (++employeeCount).ToString();
            empList.Add(employee);
        }

        public Employee GetEmployee(string Id)
        {
            return empList.FirstOrDefault(emp => emp.Id == Id);
        }

        public List<Employee> GetAllEmployees()
        {
            return GetEmployeesBySubmitter(null);
        }

        public List<Employee> GetEmployeesBySubmitter(string submitter)
        {
            if (string.IsNullOrEmpty(submitter))
                return empList;
            else
                return empList.Where(emp => emp.Submitter.ToLower().Equals(submitter.ToLower())).ToList();
        }

        public void RemoveEmployee(string Id)
        {
            empList.RemoveAll(emp => emp.Id.Equals(Id));
        }
    }
}
