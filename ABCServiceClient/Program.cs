using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCServiceConsoleClient.ThrottlingServiceReference;
using System.Threading;
using ABCServiceConsoleClient.CustomerContactServiceReference;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace ABCServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThrottlingTest();
            DeserializationTest();
        }

        static void ThrottlingTest()
        {
            var client = new ThrottlingServiceClient();
            for (var i = 0; i < 10; i++)
            {
                var thread = new Thread(client.DoWork);
                thread.Start();
            }
            Console.ReadLine();
            client.Close();
        }

        static void SerializationTest()
        {
            var client = new CustomerContactClient();
            Customer myCustomer = client.GetCustomerContactData("ABC", 1);
            Console.WriteLine("Enter file name to put Customer data into:");
            var fileName = Console.ReadLine();
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Customer));
                serializer.WriteObject(fs, myCustomer);
            }
        }

        static void DeserializationTest()
        {
            Customer myCustomer;
            Console.WriteLine("Enter file name to put Customer data into:");
            var fileName = Console.ReadLine();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Customer));
                myCustomer = serializer.ReadObject(fs) as Customer;
                Console.WriteLine("Retrieved Customer with Name: {0}, Joining Date: {1}", 
                    myCustomer.CustomerName, myCustomer.JoiningDate);
            }
        }
    }
}
