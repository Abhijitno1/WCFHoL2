using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfRestService
{
    [DataContract(Namespace = "http://combdi.com/employee")]
    public class Employee
    {
        [DataMember]
        public string Id;
        [DataMember]
        public string Submitter;
        [DataMember]
        public string Comments;
    }

    [ServiceContract]
    public interface IEmployeeData
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "employees")]
        void SubmitEmployee(Employee employee);
        [OperationContract]
        [WebGet(UriTemplate="employee/{Id}")]
        Employee GetEmployee(string Id);
        [OperationContract]
        [WebGet(UriTemplate="employees")]
        List<Employee> GetAllEmployees();
        [OperationContract]
        [WebInvoke(Method="GET", UriTemplate="employees/{submitter}")]
        List<Employee> GetEmployeesBySubmitter(string submitter);
        [OperationContract]
        [WebInvoke(Method="DELETE", UriTemplate="employee/{Id}")]
        void RemoveEmployee(string Id);
    }
}
