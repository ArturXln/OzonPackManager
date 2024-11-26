using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OzonPackManager
{
    public partial class MainForm : Form
    {
        OzonApi ozonAPI = new OzonApi();
        ScanerAdapter scanerAdapter = new ScanerAdapter();

        public MainForm()
        {
            InitializeComponent();
        }

        public async void ScannerDataReceivedAsync(string data)
        {
            await ozonAPI.PrccessScannerDataAsync(data, cbPrintLabel.Checked);
            RefresOrderInfo();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await ozonAPI.ProccessOrderAsync(cbPrintLabel.Checked);
            RefresOrderInfo();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ozonAPI = new OzonApi("Auth.xml");
            await ozonAPI.GetOrdersAsync(dtShippingDate.Value);
            RefresOrderInfo();
        }

        private void RefresOrderInfo()
        {
            if (ozonAPI.OrdersCount == 0)
            {
                EmptyForm();
                return;
            }
            FillControls();
        }

        private void FillControls()
        {
            lbOrderID.Text = ozonAPI.CurrentOrder.OrderID;
            lCountProducts.Text = ozonAPI.CurrentOrder.CurrentProduct.CountWait.ToString();
            lbSKU.Text = ozonAPI.CurrentOrder.CurrentProduct.BarCode.ToString();
            lbProductName.Text = ozonAPI.CurrentOrder.CurrentProduct.Name.ToString();
            picBarcode.BackgroundImage = ozonAPI.GetBarcode();
            picProduct.ImageLocation = ozonAPI.CurrentOrder.CurrentProduct.ImageUrl;
            lStock.Text = ozonAPI.CurrentOrder.CurrentProduct.Stock.ToString();
            lOrdersCount.Text = ozonAPI.OrdersCount.ToString();
            lCountProducts.ForeColor = ozonAPI.CurrentOrder.CurrentProduct.CountWait  > 1 ? Color.Red: Color.Black;
        }

        private void EmptyForm()
        {
            lbOrderID.Text = "Список пуст";
            lCountProducts.Text = "0";
            lbSKU.Text = "";
            lbProductName.Text = "-";
            picBarcode.BackgroundImage = null;
            picProduct.ImageLocation = "";
            lStock.Text = "0";
            lOrdersCount.Text = "0";
        }



        private void bGetScanner_Click(object sender, EventArgs e)
        {
            scanerAdapter.LoadScanner(this);
        }
    }
}
