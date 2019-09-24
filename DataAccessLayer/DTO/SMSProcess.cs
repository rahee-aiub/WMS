using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataAccessLayer.DTO
{
    class SMSProcess
    { 
        public static void SMS(string Port, string MobileNo, string textMsg)
        {
            SerialPort SP = new SerialPort();

            string input = Port;
            string ProtNm = input.Substring(0, 4);

            SP.PortName = ProtNm;

            SP.Open();
            Thread.Sleep(1000);

            string ph_no;
            ph_no = Char.ConvertFromUtf32(34) + MobileNo + Char.ConvertFromUtf32(34);

            SP.Write("AT+CMGF=1" + Char.ConvertFromUtf32(13));
            Thread.Sleep(1000);

            SP.Write("AT+CMGS=" + ph_no + Char.ConvertFromUtf32(13));
            Thread.Sleep(1000);

            SP.Write(textMsg + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
            Thread.Sleep(1000);

            SP.Write("AT+CMGS=\"" + MobileNo + "\"\r\n");

            Thread.Sleep(1000);

            SP.Write(textMsg + "\x1A");

            Thread.Sleep(1000);

            SP.Close();
        }
              
    }
}
