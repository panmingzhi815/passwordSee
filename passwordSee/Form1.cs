using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace passwordSee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string t1 = textBox1.Text;
            string t2 = textBox2.Text;
            string t2CRC = byteToHexStr(CRC.CRC16(strToToHexByte(t1.Replace(" ", "") + t2.Replace(" ", ""))));
            t2 = t2 + t2CRC;
            string t3 = textBox3.Text;
            if (t1.Equals("") || t2.Equals("") || t3.Equals("")) {
                return;
            }

            byte[] t1Bytes = strToToHexByte(t1);
            byte[] t2Bytes = strToToHexByte(t2);
            byte[] t3Bytes = strToToHexByte(t3);

            byte[] result = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                int total = t1Bytes[i] + t2Bytes[i] + t3Bytes[i];
                if(total > 255){
                    total = total % 256;
                }
               
                result[i] = Convert.ToByte(total);
            }

            Console.WriteLine("t1:" + t1);
            Console.WriteLine("t2:" + t2);
            Console.WriteLine("t3:" + t3);

            string tsum = byteToHexStr(result);
            maskedTextBox1.Text = tsum.ToUpper();
        }

        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            while (hexString.Length < 12) {
                hexString = hexString + "0";
            }
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

    }
}
