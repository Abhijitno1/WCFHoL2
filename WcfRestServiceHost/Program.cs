using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using WcfRestService;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WcfRestServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //HostWCFRESTService();
            JSONSerializationDemo();
        }

        static void HostWCFRESTService()
        {
            WebServiceHost ghost = new WebServiceHost(typeof(EmployeeDataService));
            try
            {
                ghost.Open();
                Console.WriteLine("Host is up and running. Press enter to shut down");
                Console.ReadLine();
                ghost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ghost.Abort();
            }
        }

        static void JSONSerializationDemo()
        {
            Employee eval = new Employee
            {
                Id = "420",
                Submitter = "James Bhand",
                Comments = "Agent 420"
            };

            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Employee));
            serializer.WriteObject(stream, eval);

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            Console.WriteLine("JSON serialized Employee object");
            Console.WriteLine(reader.ReadToEnd());

            Console.ReadLine();
        }
    }
}
