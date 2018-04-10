using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.IO;

using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class NFC_Test : Form
    {
        NFCReader R1;

        Thread TestThread = null;

        string TestStatus = "start";

        string logfile = "NFC_log.txt";


        public delegate void UIDLabelInvoke(string uid);
        public delegate void StatusLabelInvoke(string uid);

        public NFC_Test()
        {
            InitializeComponent();

            R1 = new NFCReader(this);

            R1.Connect();
        }

        private void AutoRun()
        {
            R1.Thread_NFCReading();
        }


        private void closeThread()
        {
            if (TestThread != null)
            {
                if (TestThread.IsAlive)
                {
                    TestThread.Abort();
                }
            }
        }


        public void UpdateUIDThread(string info)
        {
            UIDLabelInvoke Mi = new UIDLabelInvoke(UpdateUID);

            this.BeginInvoke(Mi, new object[] { info });

        }

        public void UpdateUID(string info)
        {
            UID_Text.Text = "UID:" + info;


        }


        public void UpdateStatusThread(string info)
        {
            StatusLabelInvoke Mi = new StatusLabelInvoke(UpdateStatus);

            this.BeginInvoke(Mi, new object[] { info });

        }

        public void UpdateStatus(string info)
        {
            StatusLabel.Text = info;


        }

        private void Test_BTN_Click(object sender, EventArgs e)
        {
           
            closeThread();

            TestThread = new Thread(new ThreadStart(AutoRun));

            switch (TestStatus)
            {
                case "start":
                    {

                        Test_BTN.Text = "结束测试";
                        TestThread.Start();
                        TestStatus = "end";


                    }

                    break;

                case "end":
                    {

                        Test_BTN.Text = "开始测试";

                        closeThread();

                        TestStatus = "start";

                    }

                    break;



            }


          


           


        }

        private void NFC_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeThread();
        }


       




    }
}
