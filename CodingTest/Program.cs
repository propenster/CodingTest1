using CodingTest.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CodingTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\", @"fees.config.json"));
            List<Fee> feeList = new List<Fee>();
            feeList = getFeeConfigFromJsonParsing(jsonFilePath);
            TransferChargeService service = new TransferChargeService(feeList);

            /**
             * Task 1 - Read Charge on Amount From Configuration File
             * 
             */
            #region Task 1 SOLUTION
            double CalculatedCharge = 0;
            Console.Write("Please enter an amount to be transfered: ");
            double TransferAmount = Convert.ToDouble(Console.ReadLine());

            //Sync Version
            CalculatedCharge = service.CalculatedExpectedCharge(TransferAmount);
            //Async Version -This I found faster...
            CalculatedCharge = Task.Run(async () => await service.CalculatedExpectedChargeAsync(TransferAmount)).Result;

            Console.WriteLine($"The Charge on {TransferAmount} is NGN {CalculatedCharge}");
            #endregion

            #region Task 2 SOLUTION
            //Task 2
            Console.WriteLine("\n\nTask 2 - To Compute and Display Surcharge info");
            Console.WriteLine("Please enter an Amount to be transfered:...");
            double SurchargeTransAmount = Convert.ToDouble(Console.ReadLine());
            double ChargeOnAmount = service.CalculatedExpectedCharge(SurchargeTransAmount);
            double AdvisedTransferAmount = service.CalculateSurcharge(SurchargeTransAmount);
            Console.Write($"\nAmount\tTransfer Amount\tCharge\tDebit(Transfer Amt + Charge)" +
                $"\n--------------------------------------------------" +
                $"\n{SurchargeTransAmount}\t{AdvisedTransferAmount}\t\t{ChargeOnAmount}\t{AdvisedTransferAmount + ChargeOnAmount}");

#endregion

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();

            

        }

        private static List<Fee> getFeeConfigFromJsonParsing(string jsonFilePath)
        {
            List<Fee> feesFromConfig = new List<Fee>();
            feesFromConfig = Utility.ParseJsonToObject<List<Fee>>(jsonFilePath);

            return feesFromConfig;
        }
    }
}
