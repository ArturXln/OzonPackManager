using Newtonsoft.Json.Linq;

namespace Ozon
{
    public class OzonProduct
    {
        public string OfferId { get; private set; }
        public string ProductID { get; private set; }
        public string BarCode { get; private set; } = string.Empty;
        public string SKU { get; private set; }
        public string ImageUrl { get; private set; }
        public int Stock { get; private set; }
        public bool IsCompleted { get { return CountWait == 0; }}


        public string Name { get; set; }
        public int CountWait { get; set; }
        public int Quantity { get; }

        public OzonProduct(JToken jitem)
        {
            OfferId = (string)jitem["offer_id"];
            Name = (string)jitem["name"];
            CountWait = (int)jitem["quantity"];
            Quantity = (int)jitem["quantity"];
            SKU = (string)jitem["sku"];
        }

        public OzonProduct()
        {
        }

        public void FillDescription(JObject jProduct)
        {
            ImageUrl = (string)jProduct["result"]["primary_image"];
            Stock = (int)jProduct["result"]["stocks"]["present"];
            ProductID = (string)jProduct["result"]["id"];
            BarCode = (string)jProduct["result"]["barcode"];
        }
    }
}
