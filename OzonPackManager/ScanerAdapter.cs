
using System;
using System.Xml;
using Zebra;

namespace OzonPackManager
{
    internal class ScanerAdapter
    {
        ZebraScanner zebraScanner = new ZebraScanner();
        MainForm mainForm;

        /// <summary>
        /// BarcodeEvent received
        /// </summary>
        /// <param name="eventType">Type of event</param>
        /// <param name="scanData">Barcode string</param>
        void OnBarcodeEvent(short eventType, ref string scanData)
        {
            try
            {
                string tmpScanData = scanData;
                string fBarCode = ShowBarcodeLabel(tmpScanData);
                DataReceived(fBarCode);
            }
            catch (Exception ex)
            {
                scanData = ex.Message;
            }

        }

        private void DataReceived(string fBarCode)
        {
            mainForm.ScannerDataReceivedAsync(fBarCode);
        }

        private string ShowBarcodeLabel(string strXml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);

            string strData = String.Empty;
            string barcode = xmlDoc.DocumentElement.GetElementsByTagName("datalabel").Item(0).InnerText;
            string symbology = xmlDoc.DocumentElement.GetElementsByTagName("datatype").Item(0).InnerText;
            string[] numbers = barcode.Split(' ');

            foreach (string number in numbers)
            {
                if (String.IsNullOrEmpty(number))   break;
                strData += ((char)Convert.ToInt32(number, 16)).ToString();
            }
            return strData;
        }

        public void LoadScanner(MainForm mainForm)
        {
            zebraScanner.LoadSAPIDevice();
            if (zebraScanner.m_pCoreScanner == null) return;
            zebraScanner.m_pCoreScanner.BarcodeEvent += new CoreScanner._ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
            this.mainForm = mainForm;
        }

    }
}
