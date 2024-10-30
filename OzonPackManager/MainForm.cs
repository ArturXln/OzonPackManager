using System;
using System.IO;
using System.Windows.Forms;

namespace OzonPackManager
{
    public partial class MainForm : Form
    {
        OzonApi ozonAPI = new OzonApi();
        public MainForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ozonAPI = new OzonApi("Auth.xml");
            await ozonAPI.GetOrders(dtShippingDate.Value);
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

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await ozonAPI.ProccessOrder(cbPrintLabel.Checked);
            RefresOrderInfo();
        }

    }
}
