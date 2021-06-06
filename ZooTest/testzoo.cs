using System;
using Xunit;
using Moq;
using ZooApplication.Data_Abstraction;
using ZooApplication.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZooTest
{
    [TestClass]
    public class testzoo
    {
        public Mock<IPriceCalculation> mock = new Mock<IPriceCalculation>();

        [Fact]
        public async void CalculatePrice()
        {
            mock.Setup(p => p.CalculatePrice()).ReturnsAsync("");
            Zoo zoo = new Zoo(mock.Object);
            double result = await zoo.CalculatePrice();
            Assert.Equal("", double.Parse(result));

        }
    }
}
