using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ONNONLed5KSDKD;

namespace Led5KSDKDemoCSharp
{
    public partial class SpecialAppDynamic : Form
    {
        public SpecialAppDynamic()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }
        public static ushort AreaX;
        public static ushort AreaY;
        public static ushort AreaWidth;
        public static ushort AreaHigh;
        public static byte DataType;

        public static byte Pagetotal;

        public static byte RunState;
        public static ushort Timeout;
        public static byte SingleLine;
        public static byte Lines_sizes;
        public static byte NewLine;
        public static ushort StayTime;
        private void button1_Click(object sender, EventArgs e)
        {
            AreaX = Convert.ToUInt16(textBox1.Text);
            AreaY = Convert.ToUInt16(textBox2.Text);
            AreaWidth = Convert.ToUInt16(textBox3.Text);
            AreaHigh = Convert.ToUInt16(textBox4.Text);
            DataType = Convert.ToByte(comboBox3.SelectedIndex);


            RunState = Convert.ToByte(comboBox4.SelectedIndex);
            //Timeout = Convert.ToUInt16(textBox6.Text);
            Timeout = 2;
            byte[] SingleLine_list = new byte[2];
            SingleLine_list[0] = 0x01;
            SingleLine_list[1] = 0x02;
            int sll = comboBox1.SelectedIndex;
            SingleLine = SingleLine_list[sll];

            Lines_sizes = Convert.ToByte(textBox5.Text);

            byte[] NewLine_list = new byte[2];
            NewLine_list[0] = 0x01;
            NewLine_list[1] = 0x02;
            int nl = comboBox2.SelectedIndex;
            NewLine = NewLine_list[nl];

            StayTime = Convert.ToUInt16(textBox7.Text); 
        }

        private void SpecialAppDynamic_Load(object sender, EventArgs e)
        {

        }
    }
}
