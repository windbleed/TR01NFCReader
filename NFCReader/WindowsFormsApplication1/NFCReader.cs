using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Drawing;


using System.IO;


using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    class NFCReader
    {

        NFC_Test MessageForm;

        string logfile="NFC_LOG.txt";

        [DllImport("kernel32.dll")]
        static extern void Sleep(int dwMilliseconds);


        //=========================== System Function =============================
        [DllImport("hfrdapi.dll")]
        static extern int Sys_GetDeviceNum(UInt16 vid, UInt16 pid, ref UInt32 pNum);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_GetHidSerialNumberStr(UInt32 deviceIndex,
                                                    UInt16 vid,
                                                    UInt16 pid,
                                                    [Out]StringBuilder deviceString,
                                                    UInt32 deviceStringLength);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_Open(ref IntPtr device,
                                   UInt32 index,
                                   UInt16 vid,
                                   UInt16 pid);

        [DllImport("hfrdapi.dll")]
        static extern bool Sys_IsOpen(IntPtr device);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_Close(ref IntPtr device);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_SetLight(IntPtr device, byte color);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_SetBuzzer(IntPtr device, byte msec);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_SetAntenna(IntPtr device, byte mode);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_InitType(IntPtr device, byte type);


        //=========================== M1 Card Function =============================
        [DllImport("hfrdapi.dll")]
        static extern int TyA_Request(IntPtr device, byte mode, ref UInt16 pTagType);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_Anticollision(IntPtr device,
                                            byte bcnt,
                                            byte[] pSnr,
                                            ref byte pLen);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_Select(IntPtr device,
                                     byte[] pSnr,
                                     byte snrLen,
                                     ref byte pSak);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_Halt(IntPtr device);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Authentication2(IntPtr device,
                                                 byte mode,
                                                 byte block,
                                                 byte[] pKey);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Read(IntPtr device,
                                      byte block,
                                      byte[] pData,
                                      ref byte pLen);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Write(IntPtr device, byte block, byte[] pData);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_InitValue(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_ReadValue(IntPtr device, byte block, ref Int32 pValue);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Decrement(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Increment(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Restore(IntPtr device, byte block);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Transfer(IntPtr device, byte block);
       

        string DeviceName = "HF12302ACB";

        IntPtr g_hDevice = (IntPtr)(-1); //g_hDevice must init as -1



         public NFCReader(NFC_Test F1)
        {
            MessageForm = F1;
     
        }


        public static String byteHEX(Byte ib)
        {
            String _str = String.Empty;
            try
            {
                char[] Digit = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A',
			    'B', 'C', 'D', 'E', 'F' };
                char[] ob = new char[2];
                ob[0] = Digit[(ib >> 4) & 0X0F];
                ob[1] = Digit[ib & 0X0F];
                _str = new String(ob);
            }
            catch (Exception)
            {
                new Exception("Fail");
            }
            return _str;

        }



        public string RequestCard()
        {
           // return "Fail";


             int status;
            byte mode = 0x52;
            ushort TagType = 0;
            byte bcnt = 0;
            byte[] dataBuffer = new byte[256];
            byte len = 255;
            byte sak = 0;
            String m_cardNo = String.Empty;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
               // MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "None connection";
            }

            for (int i = 0; i < 2; i++)
            {
                status = TyA_Request(g_hDevice, mode, ref TagType);//搜寻所有的卡
                if (status != 0)
                    continue;

                status = TyA_Anticollision(g_hDevice, bcnt, dataBuffer, ref len);//返回卡的序列号
                if (status != 0)
                    continue;

                status = TyA_Select(g_hDevice, dataBuffer, len, ref sak);//锁定一张ISO14443-3 TYPE_A 卡
                if (status != 0)
                    continue;

                

                for (int q = 0; q < len; q++)
                {
                    m_cardNo += byteHEX(dataBuffer[q]);
                }

               

                break;
            }

            return m_cardNo;
        }

        void LED_RED()
        {
            Sys_SetLight(g_hDevice, 1);

        }

        void LED_GREEN()
        {
            Sys_SetLight(g_hDevice, 2);


        }

        void LED_OFF()
        {
            Sys_SetLight(g_hDevice, 0);
        }

        public void BEEP(byte count) //1count=10ms;
        {
            Sys_SetBuzzer(g_hDevice, count);
        }


        public void Connect()
        {

            int status;
            string strError;

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice))
            {
                //If the reader is already open , close it firstly
                status = Sys_Close(ref g_hDevice);
                if (0 != status)
                {
                    strError = "Sys_Close failed !";
                   // MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Connect
            status = Sys_Open(ref g_hDevice, 0, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                //MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //============= Init the reader before operating the card ============
            //Close antenna of the reader
            status = Sys_SetAntenna(g_hDevice, 0);
            if (0 != status)
            {
                strError = "Sys_SetAntenna failed !";
                //MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating 

            //Set the reader's working mode
            status = Sys_InitType(g_hDevice, (byte)'A');
            if (0 != status)
            {
                strError = "Sys_InitType failed !";
                //MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_InitType operating


            //Open antenna of the reader
            status = Sys_SetAntenna(g_hDevice, 1);
            if (0 != status)
            {
                strError = "Sys_SetAntenna failed !";
                //MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating

            //============================ Success Tips ==========================
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice, 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
               // MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }






        }


        void Open()
        {

        }

        void Close()
        {

        }

        //define thread for nfc reading

       public void Thread_NFCReading()
       {
           string UID = "0";
           string OLDUID = "0";

           while (true)
           {


              // UID = this.RequestCard();

               OLDUID = UID;

               UID = this.RequestCard();

               if (UID == String.Empty)
               {

                   LED_RED();

                   MessageForm.UpdateUIDThread(UID);
                   MessageForm.UpdateStatusThread("读卡无效！");

                   BEEP(5);
                   Sleep(1000);

                  

               }
               else if(OLDUID==UID)
                   continue;

               else
               {


                   BEEP(20);
                   LED_GREEN();
                   //echo message on Form

                   MessageForm.UpdateUIDThread(UID);

                   MessageForm.UpdateStatusThread("读卡成功！");
                   SaveToLog(UID);
               }


           }

       }


       public void SaveToLog(string uid)
       {
           // throw new System.NotImplementedException();

           FileStream aFile = new FileStream(logfile, FileMode.Append);
           StreamWriter sw = new StreamWriter(aFile);

           sw.WriteLine("");

           sw.Write(uid);
           sw.Write("   ");

           sw.Write(System.DateTime.Now.ToString());
           sw.Close();
       }



    }
}
