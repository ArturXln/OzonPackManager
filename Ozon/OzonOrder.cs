using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Ozon
{
    public class OzonOrder
    {
        internal List<OzonProduct> Products = new List<OzonProduct>();
        public string OrderID { get; private set; }
        
        public OzonOrder(string orderID) { 
            OrderID = orderID;
        }

        public List<OzonProduct> CompleteProducts
        {
            get
            {
                return Products.Where(prod => prod.IsCompleted == true).ToList();
            }
        }
        public List<OzonProduct> WaitProducts
        {
            get
            {
                return Products.Where(prod => prod.IsCompleted == false).ToList();
            }
        }

        public OzonProduct CurrentProduct
        {
            get
            {
                if (WaitProducts.Count > 0) return WaitProducts[0];
                else return new OzonProduct();
            }
        }

        public bool IsFinished
        {
            get
            {
                return (WaitProducts.Count == 0 && CompleteProducts.Count > 0);
            }
        }


        /// <summary>
        /// Создаем экземпляр из JToken'a
        /// </summary>
        public static OzonOrder Create(JToken jOrder)
        {
            OzonOrder ozonOrder = new OzonOrder((string)jOrder["posting_number"]);
            foreach (var item in jOrder["products"])
            {
                ozonOrder.Products.Add(new OzonProduct(item));
            }
            return ozonOrder;
        }
        /// <summary>
        /// Обработать текущий товар в заказе
        /// </summary>
        public void CompleteCurrentProduct()
        {
            OzonProduct Product = Products.First(prod => prod.SKU == CurrentProduct.SKU);
            if ((Product.CountWait > 0)) Product.CountWait--;
        }

        public static List<OzonOrder> CreateOrders(JObject jOrders)
        {
            return jOrders["result"]["postings"].Select(ord => OzonOrder.Create(ord)).ToList();
        }
    }
}
