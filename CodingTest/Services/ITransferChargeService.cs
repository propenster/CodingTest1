using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Services
{
    public interface ITransferChargeService
    {
        Task<double> CalculatedExpectedChargeAsync(double TransferAmount);
        double CalculatedExpectedCharge(double TransferAmount);
        double CalculateSurcharge(double TransferAmount);


    }
}
