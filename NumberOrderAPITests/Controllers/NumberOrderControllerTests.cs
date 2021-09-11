using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NumberOrderingAPI.Controllers;
using NumberOrderingAPI.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NumberOrderAPITests.Controllers
{
    [TestClass]
    public class NumberOrderControllerTests
    {
        private Mock<INumberOrderService> _mockNumberOrderService;
        private NumberOrderController _controller;

        [TestInitialize]
        public void InitializeTestData()
        {
            _mockNumberOrderService = new Mock<INumberOrderService>();
            _controller = new NumberOrderController(_mockNumberOrderService.Object);
        }

        [TestMethod]
        public void NumberOrderController_Should_Be_Initialized()
        {
            Assert.IsNotNull(_controller);
        }

        [TestMethod]
        public async Task StoreNumbersInOrder_Should_Return_400_StatusCode()
        {
            var response = await _controller.StoreNumbersInOrder(null) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task StoreNumbersInOrder_Should_Return_200_StatusCode()
        {
            var unOreredList = new List<int>() { 100, 20, 50, 10 };
            var orderedList = new List<int>() { 10, 20, 50, 100 };
            _mockNumberOrderService.Setup(x => x.SortUnOrderedList(It.IsAny<List<int>>())).Returns(orderedList);
            _mockNumberOrderService.Setup(x => x.StoreListIntoFile(It.IsAny<List<int>>())).Returns(Task.FromResult(true));
            var response = await _controller.StoreNumbersInOrder(unOreredList) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetLatestFileData_Should_Return_200()
        {
            var expectedResult = new List<string>() { "10", "20", "30"};
            _mockNumberOrderService.Setup(x => x.GetLatestFileData()).Returns(expectedResult);
            var response = _controller.GetLatestFileData();
            Assert.IsNotNull(response);
        }
    }
}
