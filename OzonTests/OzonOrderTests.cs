using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Ozon.Tests
{
    [TestFixture()]
    public class OzonOrderTests
    {
        List<OzonOrder> Orders;

        [SetUp()]
        public void Init()
        {
            string json = File.ReadAllText("./Json/OrdersResponse.json");
            JObject jOrders = JObject.Parse(json);
            Orders = OzonOrder.CreateOrders(jOrders);
        }

        [Test()]
        public void CreateOrderTest()
        {
            
            Assert.That(Orders[0].OrderID, Is.EqualTo("74069191-0262-1"));
            Assert.That(Orders[19].OrderID, Is.EqualTo("0148944775-0011-1"));
            Assert.That(Orders.Count, Is.EqualTo(20));
        }

        [Test()]
        public void CompleteCurrentProductTest()
        {
            Assert.That(Orders[0].CurrentProduct.CountWait, Is.EqualTo(2));
            Assert.That(Orders[0].WaitProducts.Count, Is.EqualTo(1));
            Assert.That(Orders[0].CompleteProducts.Count, Is.EqualTo(0));
            Assert.That(Orders[0].IsFinished, Is.EqualTo(false));

            Orders[0].CompleteCurrentProduct();
            Assert.That(Orders[0].CurrentProduct.CountWait, Is.EqualTo(1));
            Assert.That(Orders[0].CurrentProduct.IsCompleted, Is.EqualTo(false));
            Assert.That(Orders[0].CompleteProducts.Count, Is.EqualTo(0));
            Assert.That(Orders[0].IsFinished, Is.EqualTo(false));

            Orders[0].CompleteCurrentProduct();

            Assert.That(Orders[0].WaitProducts.Count, Is.EqualTo(0));
            Assert.That(Orders[0].CompleteProducts.Count, Is.EqualTo(1));
            Assert.That(Orders[0].IsFinished, Is.EqualTo(true));

        }

    }
}