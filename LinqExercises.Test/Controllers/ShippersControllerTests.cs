using LinqExercises.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LinqExercises.Test.Controllers
{
    [TestClass]
    public class ShippersControllerTests
    {
        private ShippersController _shippersController;

        [TestInitialize]
        public void Initialize()
        {
            // ARRANGE
            _shippersController = new ShippersController();
        }

        [TestMethod]
        public void GetFreightReportTest()
        {
            // ACT
            dynamic contentResult = _shippersController.GetFreightReport();

            var list = ((IEnumerable<dynamic>)contentResult.Content).ToList();

            // ASSERT
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, list.Count);
        }
    }
}
