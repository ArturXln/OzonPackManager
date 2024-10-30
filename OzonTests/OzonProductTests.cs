using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Ozon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ozon.Tests
{
    [TestFixture()]
    public class OzonProductTests
    {
        [Test()]
        public void OzonProductTest()
        {
            string json = File.ReadAllText("./Json/ProductInfoResponse.json");
            JToken jProduct = JToken.Parse(json);
            OzonProduct product = new OzonProduct(jProduct);
            Assert.That(product.OfferId, Is.EqualTo("54830C5000"));
            Assert.That(product.Name, Is.EqualTo("Тестовый товар"));
            Assert.That(product.CountWait, Is.EqualTo(2));
            Assert.That(product.SKU, Is.EqualTo("1671872166"));
        }

        [Test()]
        public void FillDescriptionTest()
        {
            OzonProduct product = new OzonProduct();
            string json = File.ReadAllText("./Json/ProductDescriptionRequest.json");
            JObject jProduct = JObject.Parse(json);
            product.FillDescription(jProduct);

            Assert.That(product.ImageUrl, Is.EqualTo("https://cdn1.ozone.ru/s3/multimedia-1-t/7112214029.jpg"));
            Assert.That(product.Stock, Is.EqualTo(4));
            Assert.That(product.ProductID, Is.EqualTo("1160842684"));
            Assert.That(product.BarCode, Is.EqualTo("54830C5000"));
        }
    }
}