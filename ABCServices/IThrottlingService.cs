using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ABCServices
{
    [ServiceContract]
    public interface IThrottlingService
    {
        [OperationContract(IsOneWay = true)]
        void DoWork();
    }
}
