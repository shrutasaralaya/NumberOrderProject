using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberOrderingAPI.Services;
using System.Collections.Generic;

namespace NumberOrderAPITests.Services
{
    [TestClass]
    public class NumberOrderServiceTests
    {
        private NumberOrderService _service;

        [TestInitialize]
        public void BeforeEach()
        {
            _service = new NumberOrderService();
        }

        [TestMethod]
        public void TankConfigService_Shold_be_Initialized()
        {
            Assert.IsNotNull(_service);
        }

        [TestMethod]
        public void SortUnOrderedList_Should_Return_SortedList()
        {
            var unOrderedList = new List<int>() { 100, 20, 50, 10 };
            var expectedResult = new List<int>() { 10, 20, 50, 100 };

            var result = _service.SortUnOrderedList(unOrderedList);
            Assert.AreEqual(expectedResult, result);

        }

    }
}
