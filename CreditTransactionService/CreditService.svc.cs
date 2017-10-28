using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CreditTransactionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CreditService : ICreditService
    {
        readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SampleDBConnectionString"].ConnectionString;

        [OperationBehavior(TransactionScopeRequired = true)]
        public bool PerformCreditTransaction(string accountId, double amount)
        {
            bool creditResult = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("UPDATE Account SET Amount = Amount + {0} WHERE ID = {1}", amount, accountId);
                        creditResult = cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch
            {
                throw new FaultException(new FaultReason("Error occurred while crediting account"));
            }
            return creditResult;
        }
    }
}
