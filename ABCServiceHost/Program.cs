using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ABCServices;

namespace ABCServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ThrottlingService)))
            {
                host.Open();
                Console.WriteLine("{0} started at {1}", host.Description.ServiceType, DateTime.Now);
                Console.ReadLine();
            }
            using (var host = new ServiceHost(typeof(CustomerContactService)))
            {
                host.Open();
                Console.WriteLine("{0} started at {1}", host.Description.ServiceType, DateTime.Now);
                Console.ReadLine();
            }
        }
    }
}
