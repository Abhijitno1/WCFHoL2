using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.ServiceModel;

namespace TransactionServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            bool debitResult = false, creditResult = false;

            try
            {
                using (var ts = new TransactionScope())
                {
                    var drClient = new DebitServiceReference.DebitServiceClient();
                    debitResult = drClient.PerformDebitTransaction("1", 100);
                    var crClient = new CreditServiceReference.CreditServiceClient();
                    creditResult = crClient.PerformCreditTransaction("2", 100);

                    if (debitResult && creditResult)
                    {
                        // To commit the transaction if both debit and credit are true
                        ts.Complete();
                    }
                }
            }
            catch 
            {
                // the transaction scope will take care of rolling back the transaction
                //Values debited/Credited to Database will be reverted.
            }
        }
    }
}
