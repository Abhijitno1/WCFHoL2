using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DebitTransactionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class DebitService : IDebitService
    {
        readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SampleDBConnectionString"].ConnectionString;

        [OperationBehavior(TransactionScopeRequired = true)]
        public bool PerformDebitTransaction(string accountId, double amount)
        {
            bool debitResult = false;
            try 
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("UPDATE Account SET Amount = Amount - {0} WHERE ID = {1}", amount, accountId);
                        debitResult = cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch
            {
                throw new FaultException(new FaultReason("Error occurred while debitting account"));
            }
            return debitResult;
        }

        public decimal GetAccountBalance(int accountId)
        {
            decimal? accountBal = null;
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("SELECT Amount FROM Account WHERE ID = {0}", accountId);
                    try
                    {
                        conn.Open();
                        accountBal = cmd.ExecuteScalar() as decimal?;
                    }
                    catch(Exception ex)
                    {
                        throw new FaultException(new FaultReason(ex.Message));
                    }
                }
            }
            if (accountBal.HasValue)
                return accountBal.Value;
            else
                throw new FaultException(new FaultReason("Unable to retrieve the amount"));
        }
    }
}
