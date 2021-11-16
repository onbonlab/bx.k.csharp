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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }
        public static Led5kSDK.bx_5k_area_header bx_5k;
        public static string AreaText;
        //public static byte[] AreaText;
        public static int AreaDataListLen;
        public string P_Area_Name;
        public string TextBoxText
        {
            get { return P_Area_Name; }
            set { P_Area_Name = value; }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请填写图文区域名称");
            }
                TextBoxText = textBox1.Text;

                bx_5k.AreaX = (ushort)(Convert.ToUInt16(textBox2.Text) / 8);
                bx_5k.AreaY = Convert.ToUInt16(textBox3.Text);
                bx_5k.AreaWidth = (ushort)(Convert.ToUInt16(textBox4.Text) / 8);
                bx_5k.AreaHeight = Convert.ToUInt16(textBox5.Text);

                byte[] SingleLine_list = new byte[2];
                SingleLine_list[0] = 0x01;
                SingleLine_list[1] = 0x02;
                int sll = comboBox1.SelectedIndex;
                bx_5k.SingleLine = SingleLine_list[sll];

                byte[] NewLine_list = new byte[2];
                NewLine_list[0] = 0x01;
                NewLine_list[1] = 0x02;
                int nl = comboBox2.SelectedIndex;
                bx_5k.NewLine = NewLine_list[nl];

                bx_5k.Lines_sizes = Convert.ToByte(textBox6.Text);

                bx_5k.Reserved1 = 0;
                bx_5k.Reserved2 = 0;
                bx_5k.Reserved3 = 0;


                byte[] DisplayMode_list = new byte[6];
                DisplayMode_list[0] = 0x01;
                DisplayMode_list[1] = 0x02;
                DisplayMode_list[2] = 0x03;
                DisplayMode_list[3] = 0x04;
                DisplayMode_list[4] = 0x05;
                DisplayMode_list[5] = 0x06;
                int dml = comboBox3.SelectedIndex;
                bx_5k.DisplayMode = DisplayMode_list[dml];

                bx_5k.DynamicAreaLoc = 0xff;
                bx_5k.RunMode = 0;
                bx_5k.Timeout = 0;
                bx_5k.ExitMode = 0x00;

                bx_5k.Speed = (byte)comboBox4.SelectedIndex; ;

                bx_5k.StayTime = Convert.ToByte(textBox7.Text);

                AreaText =textBox8.Text.Trim();
                bx_5k.DataLen = textBox8.TextLength;
                AreaDataListLen = textBox8.TextLength;
                TextBoxText = this.textBox1.Text;
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
