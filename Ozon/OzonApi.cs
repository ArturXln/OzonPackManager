using BarcodeStandard;
using Newtonsoft.Json.Linq;
using Ozon;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OzonPackManager
{
    public class OzonApi
    {
        readonly string APIKey;
        readonly string CliendID;

        List<OzonOrder> Orders = new List<OzonOrder>();

        public OzonOrder CurrentOrder
        {
            get
            {
                if (Orders.Count > 0) return Orders[0];
                else return new OzonOrder(string.Empty);
            }
        }

        public int OrdersCount { get { return Orders.Count; } }
        

        public OzonApi()
        {
        }
        /// <summary>
        /// Авторизация через XML
        /// </summary>
        public OzonApi(string xmlPath)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPath);
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    if (xnode.Name == "ClientID") CliendID = xnode.InnerText;
                    if (xnode.Name == "ApiKey") APIKey = xnode.InnerText;
                }
            }
        }
        /// <summary>
        /// ОБработать сборку товара
        /// </summary>
        public async Task ProccessOrder(bool isPrintLabel)
        {
            CurrentOrder.CompleteCurrentProduct();
            if (!CurrentOrder.IsFinished) return;
            //собираем заказ в Озон
            string posting_nunmber = await CompleteOrder(CurrentOrder);

            if (isPrintLabel)
            {
                await Task.Delay(3000); // ждем пока Озон приготовит этикетку
                PrintPackageLabel(await GetPackageLabel(posting_nunmber));
            }
            Orders.Remove(CurrentOrder);
        }

        /// <summary>
        /// Получить список заказов для комплектации на указанную дату
        /// </summary>
        public async Task GetOrders(DateTime dateShipping)
        {
            JObject jOrders = await GetOrdersJObject(dateShipping);
            Orders = OzonOrder.CreateOrders(jOrders);
            await LoadProductInfo(Orders);
        }

        /// <summary>
        /// Запрос дополнительных параметров для коллекции заказов
        /// </summary>
        private async Task LoadProductInfo(List<OzonOrder> orders)
        {
            foreach (var order in orders)
                foreach (var product in order.Products)
                    await GetProductInfo(product);
        }

        /// <summary>
        /// Получить JObject с заказами на комплектацию
        /// </summary>
        public async Task<JObject> GetOrdersJObject(DateTime dateShipping)
        {
            string requestUrl = "https://api-seller.ozon.ru/v3/posting/fbs/unfulfilled/list";
            JObject OrdersRequest = UncompleteOrdersObjecRequest();
            OrdersRequest["filter"]["cutoff_from"] = dateShipping.Date.ToLocalTime();
            OrdersRequest["filter"]["cutoff_to"] = dateShipping.Date.ToLocalTime().AddDays(1);
            return await JsonRequestAsync(requestUrl, OrdersRequest.ToString());
        }
        /// <summary>
        /// Запрос дополнительных параметров для товара
        /// </summary>
        public async Task GetProductInfo(OzonProduct ozonItem)
        {
            string requestUrl = "https://api-seller.ozon.ru/v2/product/info";
            JObject jItemDescr = await JsonRequestAsync(requestUrl, ProductInfoObjectRequest(ozonItem.SKU).ToString());
            ozonItem.FillDescription(jItemDescr);
        }
        /// <summary>
        /// Собрать заказ в Озон
        /// </summary>
        public async Task<string> CompleteOrder(OzonOrder ozonOrder)
        {
            string requestUrl = "https://api-seller.ozon.ru/v4/posting/fbs/ship";
            JObject jSaveReuslt = await JsonRequestAsync(requestUrl, CompleteOrderObjestRequest(ozonOrder).ToString());
            return (string)jSaveReuslt["result"][0];
        }
        /// <summary>
        /// Получить этикетку для собраного заказа
        /// </summary>
        public async Task<byte[]> GetPackageLabel(string postingNumber)
        {
            string requestUrl = "https://api-seller.ozon.ru/v2/posting/fbs/package-label";
            return await FileRequestAsync(requestUrl, PostingNumberObjectRequest(postingNumber).ToString());
        }

        public void PrintPackageLabel(byte[] pdfContent)
        {
            var doc = new PdfDocument();
            try
            {
                PrintDialog pd = new PrintDialog();
                doc.LoadFromBytes(pdfContent);
                doc.PrintSettings.PrinterName = pd.PrinterSettings.PrinterName;
                doc.PrintSettings.PaperSize = new PaperSize("Custom", 295, 472);
                doc.Print();
            }
            catch 
            {
            }
            finally { doc.Dispose(); }
        }

        /// <summary>
        /// Отправить json и вернуть JObject
        /// </summary>
        private async Task<JObject> JsonRequestAsync(string url, string json)
        {
            HttpResponseMessage response = await RequestAsync(url, json);
            string responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }
        /// <summary>
        /// Отправить json и получить ответ в виде байтового массива
        /// </summary>
        private async Task<byte[]> FileRequestAsync(string url, string json)
        {
            HttpResponseMessage response = await RequestAsync(url, json);
            return await response.Content.ReadAsByteArrayAsync();
        }
        /// <summary>
        /// Запрос с авторизацией Озон
        /// </summary>
        private async Task<HttpResponseMessage> RequestAsync(string url, string json)
        {
            HttpClient httpClient = new HttpClient();
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // авторизация
            byteContent.Headers.Add("Client-Id", CliendID);
            byteContent.Headers.Add("Api-Key", APIKey);
            return (await httpClient.PostAsync(url, byteContent));
        }


        /// <summary>
        /// Json объект для запроса заказов на сборку
        /// </summary>
        private JObject UncompleteOrdersObjecRequest()
        {
            return new JObject
            {
                {
                    "filter", new JObject
                    {
                        { "cutoff_from", DateTime.Today },
                        { "cutoff_to", DateTime.Today.AddDays(1) },
                        { "status",  "awaiting_packaging"},
                    }
                },
                {"limit", 100},
                {"offset", 0},
                {
                    "with",  new JObject
                    {
                        {"barcodes", true},
                        {"translit", true}
                    }
                }
            };
        }

        private JObject ProductInfoObjectRequest(string sku)
        {
            return new JObject
            {
                { "sku", sku }
            };
        }

        private JObject PostingNumberObjectRequest(string orderID)
        {
            return new JObject
            {
                {"posting_number", new JArray {orderID}}
            };
        }

        private JObject CompleteOrderObjestRequest(OzonOrder ozonOrder)
        {
            return JObject.FromObject(new
            {
                packages = new List<object>
                {
                    new
                    {
                        products =
                            from p in ozonOrder.CompleteProducts
                            select new
                            {
                                product_id = p.SKU,
                                quantity = p.Quantity
                            }
                    }
                },
                posting_number = ozonOrder.OrderID
            });
        }
        /// <summary>
        /// Генерация баркода
        /// </summary>
        public Image GetBarcode()
        {
            if(CurrentOrder.CurrentProduct.BarCode == string.Empty) return null;
            Barcode barcode = new Barcode();
            return Image.FromStream(barcode.Encode(BarcodeStandard.Type.Code39, CurrentOrder.CurrentProduct.BarCode.ToString()).Encode().AsStream());
        }

    }
}


