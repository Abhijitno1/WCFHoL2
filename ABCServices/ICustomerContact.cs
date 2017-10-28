using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Security;

namespace ABCServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICustomerContact
    {
        [OperationContract]
        [FaultContract(typeof(string))]
        CustomerContactResponse GetCustomerContactData(CustomerContactRequest composite);

    }

    [MessageContract(IsWrapped = true)]
    public class CustomerContactRequest
    {
        [MessageHeader]
        public string LicenceKey;
        [MessageBodyMember(ProtectionLevel = ProtectionLevel.None)]
        public int CustomerId;
    }

    [MessageContract(IsWrapped = true)]
    public class CustomerContactResponse
    {
        [MessageBodyMember(Name="MyContact", Namespace="batatawada.com")]
        public Customer Contact;
    }

    [DataContract]
    public class Customer
    {
        [DataMember]
        public string CustomerName;
        [DataMember]
        public string PhoneNumber;
        [DataMember]
        public string Email;
        [DataMember]
        public DateTime JoiningDate;
    }

}
