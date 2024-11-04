using CoreScanner;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Zebra
{
    public class ZebraScanner
    {

        public CCoreScannerClass m_pCoreScanner;

        bool m_bSuccessOpen;//Is open success
        short m_nNumberOfTypes;
        readonly short[] m_arScannerTypes;
        readonly Scanner[] m_arScanners;
        int m_nTotalScanners;
        readonly XmlReader m_xml;
        List<string> claimlist = new List<string>();


        // Scanner types
        public const short SCANNER_TYPES_ALL = 1;
        public const short SCANNER_TYPES_SNAPI = 2;
        public const short SCANNER_TYPES_SSI = 3;
        public const short SCANNER_TYPES_RSM = 4;
        public const short SCANNER_TYPES_IMAGING = 5;
        public const short SCANNER_TYPES_IBMHID = 6;
        public const short SCANNER_TYPES_NIXMODB = 7;
        public const short SCANNER_TYPES_HIDKB = 8;
        public const short SCANNER_TYPES_IBMTT = 9;
        public const short SCALE_TYPES_IBM = 10;
        public const short SCALE_TYPES_SSI_BT = 11;

        // Total number of scanner types
        public short TOTAL_SCANNER_TYPES = 11;
        public bool[] m_arSelectedTypes;

        // available values for 'status' //
        const int STATUS_SUCCESS = 0;
        const int STATUS_FALSE = 1;
        const int STATUS_LOCKED = 10;

        //End Symbology Types

        const string APP_TITLE = "Scanner Multi-Interface Test Utility";
        const string STR_OPEN = "Start";
        const string STR_CLOSE = "Stop";
        const string STR_REFRESH = "Rediscover Scanners";
        const string STR_FIND = "Discover Scanners";
        const int NUM_SCANNER_EVENTS = 6;

        const int SUBSCRIBE_BARCODE = 1;
        const int SUBSCRIBE_IMAGE = 2;
        const int SUBSCRIBE_VIDEO = 4;
        const int SUBSCRIBE_RMD = 8;
        const int SUBSCRIBE_PNP = 16;
        const int SUBSCRIBE_OTHER = 32;

        //****** CORESCANNER PROTOCOL ******//
        const int GET_VERSION = 1000;
        const int REGISTER_FOR_EVENTS = 1001;
        const int CLAIM_DEVICE = 1500;
        const int GET_PAIRING_BARCODE = 1005;   // Get  Blue tooth scanner pairing bar code

        public const int RSM_ATTR_GETALL = 5000;
        public const int RSM_ATTR_GET = 5001;
        public const int RSM_ATTR_GETNEXT = 5002;
        public const int RSM_ATTR_SET = 5004;
        public const int RSM_ATTR_STORE = 5005;

        const int MAX_NUM_DEVICES = 255;/* Maximum number of scanners to be connected*/


        public ZebraScanner()
        {
            m_bSuccessOpen = false;
            m_nTotalScanners = 0;
            m_arScanners = new Scanner[MAX_NUM_DEVICES];
            for (int i = 0; i < MAX_NUM_DEVICES; i++)
            {
                Scanner scanr = new Scanner();
                m_arScanners.SetValue(scanr, i);
            }
            m_xml = new XmlReader();

            m_nNumberOfTypes = 0;
            m_arScannerTypes = new short[TOTAL_SCANNER_TYPES];
            m_arSelectedTypes = new bool[TOTAL_SCANNER_TYPES];

            try
            {
                m_pCoreScanner = new CoreScanner.CCoreScannerClass();
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                //m_pCoreScanner = new CoreScanner.CCoreScannerClass();
            }

        }

        public void LoadSAPIDevice()
        {
            FilterScannerList();
            MakeConnectCtrl();
            registerForEvents();
            ShowScanners();
            //             GetPairingBarcode();
        }

        private void GetPairingBarcode()
        {
            int protocol = 1;
            int defaultOption = 0;
            int size = 0;
            {


                int status = STATUS_FALSE;
                string outXml, inXml;

                inXml = "<inArgs>"
                             + " <cmdArgs>"
                                + "<arg-int>3</arg-int>"  //number of parameters
                                + "<arg-int>"
                                + protocol + ","// Protocol
                                + defaultOption + ","// Default Option
                                + size + ","// Image Size
                                + "</arg-int>"
                              + " </cmdArgs>"
                          + "</inArgs>";

                ExecCmd(GET_PAIRING_BARCODE, ref inXml, out outXml, out status);
                DisplayResult(status, "GET_PAIRING_BARCODE");
            }
        }


        private void FilterScannerList()
        {
            for (int index = 0; index < TOTAL_SCANNER_TYPES; index++)
            {
                m_arSelectedTypes[index] = false;
            }
            m_arSelectedTypes[SCANNER_TYPES_ALL - 1] = true;
        }

        private void MakeConnectCtrl()
        {
            Connect();
        }

        private void ShowScanners()
        {
            int opCode = CLAIM_DEVICE;
            string inXml = String.Empty;
            string outXml;
            int status = STATUS_FALSE;

            m_arScanners.Initialize();
            if (m_bSuccessOpen)
            {
                m_nTotalScanners = 0;
                short numOfScanners = 0;
                int nScannerCount = 0;
                string outXML = "";
                int[] scannerIdList = new int[MAX_NUM_DEVICES];
                try
                {
                    m_pCoreScanner.GetScanners(out numOfScanners, scannerIdList, out outXML, out status);
                    DisplayResult(status, "GET_SCANNERS");
                    if (STATUS_SUCCESS == status)
                    {
                        m_nTotalScanners = numOfScanners;
                        m_xml.ReadXmlString_GetScanners(outXML, m_arScanners, numOfScanners, out nScannerCount);
                        for (int index = 0; index < m_arScanners.Length; index++)
                        {
                            for (int i = 0; i < claimlist.Count; i++)
                            {
                                if (string.Compare(claimlist[i], m_arScanners[index].SERIALNO) == 0)
                                {
                                    Scanner objScanner = (Scanner)m_arScanners.GetValue(index);
                                    objScanner.CLAIMED = true;
                                }
                            }
                        }

                        //          FillScannerList();
                        //          UpdateOutXml(outXML);
                        for (int index = 0; index < m_nTotalScanners; index++)
                        {
                            Scanner objScanner = (Scanner)m_arScanners.GetValue(index);
                            string[] strItems = new string[] { "", "", "", "", "" };

                            inXml = "<inArgs><scannerID>" + objScanner.SCANNERID + "</scannerID></inArgs>";

                            for (int i = 0; i < claimlist.Count; i++)
                            {
                                if (string.Compare(claimlist[i], objScanner.SERIALNO) == 0)
                                {
                                    ExecCmd(opCode, ref inXml, out outXml, out status);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error GETSCANNERS - " + ex.Message, APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void registerForEvents()
        {
            if (IsMotoConnected())
            {
                int nEvents = 0;
                string strEvtIDs = GetRegUnregIDs(out nEvents);
                string inXml = "<inArgs>" +
                                    "<cmdArgs>" +
                                    "<arg-int>" + nEvents + "</arg-int>" +
                                    "<arg-int>" + strEvtIDs + "</arg-int>" +
                                    "</cmdArgs>" +
                                    "</inArgs>";

                int opCode = REGISTER_FOR_EVENTS;
                string outXml = "";
                int status = STATUS_FALSE;
                ExecCmd(opCode, ref inXml, out outXml, out status);
                DisplayResult(status, "REGISTER_FOR_EVENTS");
            }
        }

        /// <summary>
        /// Sends ExecCommand(Sync) or ExecCommandAsync
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="inXml"></param>
        /// <param name="outXml"></param>
        /// <param name="status"></param>
        private void ExecCmd(int opCode, ref string inXml, out string outXml, out int status)
        {
            outXml = "";
            status = STATUS_FALSE;
            if (m_bSuccessOpen)
            {
                try
                {
                    m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out status);
                }
                catch (Exception ex)
                {
                    DisplayResult(status, "EXEC_COMMAND");
                    UpdateResults("..." + ex.Message.ToString());
                }
            }
        }

        private void UpdateResults(string messageResult)
        {
        }

        private string GetRegUnregIDs(out int nEvents)
        {
            string strIDs = "";
            nEvents = NUM_SCANNER_EVENTS;
            strIDs = SUBSCRIBE_BARCODE.ToString();
            strIDs += "," + SUBSCRIBE_IMAGE.ToString();
            strIDs += "," + SUBSCRIBE_VIDEO.ToString();
            strIDs += "," + SUBSCRIBE_RMD.ToString();
            strIDs += "," + SUBSCRIBE_PNP.ToString();
            strIDs += "," + SUBSCRIBE_OTHER.ToString();
            return strIDs;
        }


        /// <summary>
        /// Is Open successful
        /// </summary>
        /// <returns></returns>
        private bool IsMotoConnected()
        {
            return m_bSuccessOpen;
        }

        private void Connect()
        {
            if (m_bSuccessOpen)
            {
                return;
            }
            int appHandle = 0;
            GetSelectedScannerTypes();
            int status = STATUS_FALSE;

            try
            {
                m_pCoreScanner.Open(appHandle, m_arScannerTypes, m_nNumberOfTypes, out status);
                DisplayResult(status, "OPEN");
                if (STATUS_SUCCESS == status)
                {
                    m_bSuccessOpen = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error OPEN - " + exp.Message, APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void GetSelectedScannerTypes()
        {
            m_nNumberOfTypes = 0;
            for (int index = 0, k = 0; index < TOTAL_SCANNER_TYPES; index++)
            {
                if (m_arSelectedTypes[index])
                {
                    m_nNumberOfTypes++;
                    switch (index + 1)
                    {
                        case SCANNER_TYPES_ALL:
                            m_arScannerTypes[k++] = SCANNER_TYPES_ALL;
                            return;

                        case SCANNER_TYPES_SNAPI:
                            m_arScannerTypes[k++] = SCANNER_TYPES_SNAPI;
                            break;

                        case SCANNER_TYPES_SSI:
                            m_arScannerTypes[k++] = SCANNER_TYPES_SSI;
                            break;

                        case SCANNER_TYPES_NIXMODB:
                            m_arScannerTypes[k++] = SCANNER_TYPES_NIXMODB;
                            break;

                        case SCANNER_TYPES_RSM:
                            m_arScannerTypes[k++] = SCANNER_TYPES_RSM;
                            break;

                        case SCANNER_TYPES_IMAGING:
                            m_arScannerTypes[k++] = SCANNER_TYPES_IMAGING;
                            break;

                        case SCANNER_TYPES_IBMHID:
                            m_arScannerTypes[k++] = SCANNER_TYPES_IBMHID;
                            break;

                        case SCANNER_TYPES_HIDKB:
                            m_arScannerTypes[k++] = SCANNER_TYPES_HIDKB;
                            break;

                        case SCALE_TYPES_SSI_BT:
                            m_arScannerTypes[k++] = SCALE_TYPES_SSI_BT;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void DisplayResult(int status, string strCmd)
        {
            switch (status)
            {
                case STATUS_SUCCESS:
                    UpdateResults(strCmd + " - Command success.");
                    break;
                case STATUS_LOCKED:
                    UpdateResults(strCmd + " - Command failed. Device is locked by another application.");
                    break;
                default:
                    UpdateResults(strCmd + " - Command failed. Error:" + status.ToString());
                    break;
            }
        }



    }
}
