using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using WcfRestService;

namespace WcfRestServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WCF REST Service Client Application");
            WebChannelFactory<IEmployeeData> factory = new WebChannelFactory<IEmployeeData>(new Uri("http://localhost:8765/WcfRestService"));
            var client = factory.CreateChannel();
            var response = string.Empty;
            Console.WriteLine("Enter a command (x to exit): ");
            response = Console.ReadLine();
            while (response != "x")
            {
                switch (response)
                {
                    case "submit":
                        var eval = new Employee();
                        Console.WriteLine("Enter your name:");
                        eval.Submitter = Console.ReadLine();
                        Console.WriteLine("Enter your comments:"); 
                        eval.Comments = Console.ReadLine();
                        client.SubmitEmployee(eval);
                        Console.WriteLine("Employee information submitted");
                        break;

                    case "get":
                        Console.WriteLine("Enter submitter name: ");
                        var submitter = Console.ReadLine();
                        var employees = client.GetEmployeesBySubmitter(submitter);
                        employees.ForEach(emp =>
                            Console.WriteLine("{0} said {1} (Id {2})", emp.Submitter, emp.Comments, emp.Id)
                        );
                        Console.WriteLine();
                        break;
                    case "remove":
                        Console.WriteLine("Enter id of employee to remove: ");
                        var id = Console.ReadLine();
                        client.RemoveEmployee(id);
                        Console.WriteLine("Employee successfully removed");
                        break;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
                Console.WriteLine("Enter a command (x to exit): ");
                response = Console.ReadLine();
            } 
            
        }
    }
}
