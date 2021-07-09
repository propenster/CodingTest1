using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Services
{
    public class TransferChargeService : ITransferChargeService
    {
        private readonly List<Fee> fees;

        public TransferChargeService(List<Fee> fees)
        {
            this.fees = fees;
        }
        /// <summary>
        /// This is the asynchronous version of CalculatedExpectedCharge
        /// 
        /// </summary>
        /// <param name="TransferAmount">The Amount of money to be transfered between 1 and 999999999</param>
        /// <returns>A Task of type double as result</returns>
        public async Task<double> CalculatedExpectedChargeAsync(double TransferAmount)
        {
            if (TransferAmount < 1) throw new ArgumentOutOfRangeException(nameof(TransferAmount), "Transfer Amount must be 1 or more.");
            if (TransferAmount > 999999999) throw new ArgumentOutOfRangeException(nameof(TransferAmount), "Transfer Amount cannot be greater than 999999999.");


            double charge = 0;
            List<Fee> feeList = new List<Fee>();
            feeList = this.fees; //await Utility.ParseJsonToObjectAsync<List<Fee>>(jsonFilePath);  

            foreach(var feeConfig in feeList)
            {
                if (feeConfig.MinAmount <= TransferAmount && TransferAmount <= feeConfig.MaxAmount) charge = feeConfig.FeeAmount;
            }

            return charge;
        }
        /// <summary>
        /// This is the synchronous version of CalculatedExpectedCharge
        /// </summary>
        /// <param name="TransferAmount">The Amount of money to be transfered between 1 and 999999999</param>
        /// <returns>Returns a calculated charge of type double from the configuration</returns>
        public double CalculatedExpectedCharge(double TransferAmount)
        {
            if (TransferAmount < 1) throw new ArgumentOutOfRangeException(nameof(TransferAmount), "Transfer Amount must be 1 or more.");
            if (TransferAmount > 999999999) throw new ArgumentOutOfRangeException(nameof(TransferAmount), "Transfer Amount cannot be greater than 999999999.");


            double charge = 0;
            List<Fee> feeList = new List<Fee>();
            feeList = this.fees;


            foreach (var feeConfig in feeList)
            {
                if (feeConfig.MinAmount <= TransferAmount && TransferAmount <= feeConfig.MaxAmount)
                {
                    charge = feeConfig.FeeAmount;
                    break; //There is no need continuing the loop when we've already found the boundary that we are looking for.
                }
            }

            return charge;
        }

        public double CalculateSurcharge(double TransferAmount)
        {
            if (TransferAmount < 1) throw new ArgumentOutOfRangeException(nameof(TransferAmount), "Transfer Amount must be 1 or more.");

            double ChargeOnAmount = CalculatedExpectedCharge(TransferAmount);

            double AdvisedTransferAmount = TransferAmount - ChargeOnAmount;

            return AdvisedTransferAmount;
        }
    }
}
