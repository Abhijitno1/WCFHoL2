using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ABCServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CustomerContactService : ICustomerContact
    {
        private string AppKey { get { return "ABC"; } }

        CustomerContactResponse ICustomerContact.GetCustomerContactData(CustomerContactRequest request)
        {
            if (request.LicenceKey != AppKey)
                throw new FaultException<string>("Invalid Key");
            CustomerContactResponse response = new CustomerContactResponse();
            response.Contact = new Customer
            {
                CustomerName = "Shivani",
                Email = "shivani@diwani.net",
                PhoneNumber = "1234567890",
                JoiningDate = DateTime.Today
            };
            return response;
        }
    }
}
