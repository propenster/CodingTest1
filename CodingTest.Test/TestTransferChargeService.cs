using CodingTest.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingTest.Test
{
    public class TestTransferChargeService
    {

        TransferChargeService _service;
        List<Fee> fees;
        public TestTransferChargeService()
        {
            string jSonFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\CodingTest", @"fees.config.json"));
            fees = getListFromJsonParsing(jSonFilePath);
            _service = new TransferChargeService(fees);
        }

        private List<Fee> getListFromJsonParsing(string filePath)
        {
            List<Fee> feesFromConfig = new List<Fee>();
            feesFromConfig = Utility.ParseJsonToObject<List<Fee>>(filePath);

            return feesFromConfig;
        }

        [Theory]
        [InlineData(4000)]
        [InlineData(3000)]
        [InlineData(2000)]
        [InlineData(1500)]
        [InlineData(1000)]
        [InlineData(999)]
        [InlineData(550)]
        public void Test_CalculateExpectedCharge_Returns_Right_Charge_For_Tier_1_Async_Version(double Amount)
        {
            //Arrange

            //Act
            double result = Task.Run(async () => await _service.CalculatedExpectedChargeAsync(Amount)).Result;

            //Assert
            Assert.Equal(10, result);
        }

        [Theory]
        [InlineData(4000)]
        [InlineData(3000)]
        [InlineData(2000)]
        [InlineData(1500)]
        [InlineData(1000)]
        [InlineData(999)]
        [InlineData(550)]
        public void Test_CalculateExpectedCharge_Returns_Right_Charge_For_Tier_1_Sync_Version(double Amount)
        {
            //Arrange

            //Act
            double result = _service.CalculatedExpectedCharge(Amount);

            //Assert
            Assert.Equal(10, result);
        }

        
        [Fact]
        public void Test_CalcluateExpectedChargeAsync_Returns_Right_Charge_For_Tier_1_Void()
        {
            double Amount = 4500;
            //Act
            double result = _service.CalculatedExpectedChargeAsync(Amount).Result;

            //Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void Test_CalcluateExpectedChargeSync_Returns_Right_Charge_For_Tier_2()
        {
            double Amount = 45000;
            //string ProjectRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\CodingTest", @"fees.config.json"));
            //Console.WriteLine($"Backtrack to Project folder {ProjectRootPath}");
            //Act
            double result = _service.CalculatedExpectedCharge(Amount);
            
            //Assert
            Assert.Equal(25, result);
        }

        [Fact]
        public async void Test_CalcluateExpectedChargeAsync_Returns_Right_Charge_For_Tier_3()
        {
            //Arrange
            double Amount = 150000;
            //Act
            double result = await _service.CalculatedExpectedChargeAsync(Amount);

            //Assert
            Assert.Equal(50, result);

        }

        [Fact]
        public async void Failure_Test_CalcluateExpectedChargeAsync_Returns_Right_Charge_For_Tier_1()
        {
            //Arrange
            double Amount = 5000;
            //Act
            double result = await _service.CalculatedExpectedChargeAsync(Amount);

            //Assert
            Assert.Equal(15, result);

        }

        [Fact]
        public async void Failure_Test_CalcluateExpectedChargeAsync_Returns_Right_Charge_For_Tier_2()
        {
            //Arrange
            double Amount = 15000;
            //Act
            double result = await _service.CalculatedExpectedChargeAsync(Amount);

            //Assert
            Assert.Equal(30, result);

        }

        [Fact]
        public async void Failure_Test_CalcluateExpectedChargeAsync_Returns_Right_Charge_For_Tier_3()
        {
            //Arrange
            double Amount = 1500000;
            //Act
            double result = await _service.CalculatedExpectedChargeAsync(Amount);

            //Assert
            Assert.Equal(60, result); //actual should be 50

        }

        [Fact]
        public async void Test_Amount_Less_Than_1_Throws_ArgumentOutOfRangeException_Async()
        {
            //Arrange
            double Amount = 0-1;
            //Act
            //double result = await _service.CalculatedExpectedChargeAsync(Amount);

            //Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.CalculatedExpectedChargeAsync(Amount));
        }
        [Fact]
        public void Test_Amount_Less_Than_1_Throws_ArgumentOutOfRangeException_Sync()
        {
            //Arrange
            double Amount = 0 - 1;
            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.CalculatedExpectedCharge(Amount));
        }

        [Fact]
        public async void Test_Amount_Greater_Than_999999999_Throws_ArgumentOutOfRangeException_Async()
        {
            //Arrange
            double Amount = 0 - 1;
            //Act
            //double result = await _service.CalculatedExpectedChargeAsync(Amount);

            //Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.CalculatedExpectedChargeAsync(Amount));
        }
        [Fact]
        public void Test_Amount_Greater_Than_999999999_Throws_ArgumentOutOfRangeException_Sync()
        {
            //Arrange
            double Amount = 0 - 1;
            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.CalculatedExpectedCharge(Amount));
        }

    }
}
