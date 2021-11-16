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
    public partial class SoundArea : Form
    {
        public SoundArea(int type)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            if (type == 5 || type == 9 || type == 10 || type == 11)
            {
                comboBox6.Enabled = true;
                comboBox7.Enabled = true;
                comboBox8.Enabled = true;
                comboBox9.Enabled = true;
            }
            else
            {
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
            }
        }
            
        //public uint i;
        public static byte[] AreaText;
        public static byte[] SoundAreaText;
        public byte SoundMode;
        public byte SoundPerson;
        public byte SoundVolume;
        public byte SoundSpeed ;
        public int SoundDataLen;
       
        public Led5kSDK.bx_5k_area_header bx_5k;
        private void button1_Click(object sender, EventArgs e)
        {
            bx_5k.AreaType = 0x06;
            bx_5k.AreaX = (ushort)(Convert.ToUInt16(textBox1.Text) / 8);
            bx_5k.AreaY = Convert.ToUInt16(textBox2.Text);
            bx_5k.AreaWidth = (ushort)(Convert.ToUInt16(textBox3.Text) / 8);
            bx_5k.AreaHeight = Convert.ToUInt16(textBox4.Text);

            bx_5k.Lines_sizes = Convert.ToByte(textBox5.Text);

            byte[] RunMode_list = new byte[3];
            RunMode_list[0] = 0;
            RunMode_list[1] = 1;
            RunMode_list[2] = 2;
            int rl = comboBox3.SelectedIndex;
            bx_5k.RunMode = RunMode_list[rl];
            //bx_5k.RunMode = Convert.ToByte(comboBox3.SelectedIndex+1);

            bx_5k.Timeout = Convert.ToInt16(textBox7.Text);

            byte[] SoundMode_list = new byte[3];
            SoundMode_list[0] = 0;
            SoundMode_list[1] = 1;
            SoundMode_list[2] = 2;
            int sml = comboBox6.SelectedIndex;
            SoundMode = SoundMode_list[sml];

            byte[] SoundPerson_list = new byte[6];
            SoundPerson_list[0] = 0;
            SoundPerson_list[1] = 1;
            SoundPerson_list[2] = 2;
            SoundPerson_list[3] = 3;
            SoundPerson_list[4] = 4;
            SoundPerson_list[5] = 5;
            int spl = comboBox7.SelectedIndex;
            SoundPerson = SoundPerson_list[spl];

            SoundVolume = (byte)comboBox8.SelectedIndex;

            SoundSpeed  = (byte)comboBox9.SelectedIndex;

            SoundAreaText = System.Text.Encoding.Default.GetBytes(textBox9.Text.Trim());
            SoundDataLen = SoundAreaText.Length;
            

            bx_5k.Reserved1 = 0;
            bx_5k.Reserved2 = 0;
            bx_5k.Reserved3 = 0;

            byte[] SingleLine_list = new byte[2];
            SingleLine_list[0] = 0x01;
            SingleLine_list[1] = 0x02;
            int sll = comboBox1.SelectedIndex;
            bx_5k.SingleLine = SingleLine_list[sll];
            //bx_5k.SingleLine = Convert.ToByte(comboBox1.SelectedIndex);

            byte []NewLine_list=new byte[2];
            NewLine_list[0] = 0x01;
            NewLine_list[1] = 0x02;
            int nl = comboBox2.SelectedIndex;
            bx_5k.NewLine = NewLine_list[nl];
            //bx_5k.NewLine = Convert.ToByte(comboBox2.SelectedIndex);


            byte[] DisplayMode_list=new byte[6];
            DisplayMode_list[0] = 0x01;
            DisplayMode_list[1] = 0x02;
            DisplayMode_list[2] = 0x03;
            DisplayMode_list[3] = 0x04;
            DisplayMode_list[4] = 0x05;
            DisplayMode_list[5] = 0x06;
            int dml = comboBox4.SelectedIndex;
            bx_5k.DisplayMode = DisplayMode_list[dml];
            //bx_5k.DisplayMode = Convert.ToByte(comboBox4.SelectedIndex);

            bx_5k.ExitMode = 0x00;


            bx_5k.Speed =(byte) comboBox5.SelectedIndex;
            //bx_5k.Speed=Convert.ToByte(comboBox5.SelectedIndex);

            bx_5k.StayTime = Convert.ToByte(textBox8.Text);
            
            AreaText = System.Text.Encoding.Default.GetBytes(textBox6.Text.Trim());
            bx_5k.DataLen = AreaText.Length;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
