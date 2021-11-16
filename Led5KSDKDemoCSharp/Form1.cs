using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Xml;
using ONNONLed5KSDKD;
using System.Threading;


namespace Led5KSDKDemoCSharp
{
    public partial class Form1 : Form
    {
        uint m_dwCurHand;
        public int type;
        public int sum ( int type)
        {
            return type;
        }
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 1;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 1;
            comboBox7.SelectedIndex = 1;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            string[] names = SerialPort.GetPortNames();
            //textBox12.Text = names[0];
            ONNONLed5KSDKD.Led5kSDK.InitSdk(2, 2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public static byte[] mmmm = new byte[16]; 
        //打开广播
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] broad_ip = System.Text.Encoding.ASCII.GetBytes(textBox1.Text.Trim());

            uint broad_port = Convert.ToUInt32(textBox2.Text.Trim());
            byte[] card_type_list = new byte[14];
            card_type_list[0] = 0xFE;
            card_type_list[1] = 0x51;
            card_type_list[2] = 0x58;
            card_type_list[3] = 0x54;
            card_type_list[4] = 0x53;
            card_type_list[5] = 0x5c;
            card_type_list[6] = 0x61;
            card_type_list[7] = 0x62;
            card_type_list[8] = 0x63;
            card_type_list[9] = 0x64;
            card_type_list[10] = 0x65;
            card_type_list[11] = 0x66;
            card_type_list[12] = 0x67;
            card_type_list[13] = 0x68;
            int t = comboBox1.SelectedIndex;
            type = t;
            byte Option = 0;
            byte[] bar={0};
            uint hand = ONNONLed5KSDKD.Led5kSDK.CreateBroadCast(broad_ip, broad_port, (Led5kSDK.bx_5k_card_type)card_type_list[t], bar, Option,0);
            m_dwCurHand = hand;
            if (hand == 0)
            {
                MessageBox.Show("连接控制器失败");
            }
            else
            {
                MessageBox.Show("连接控制器成功");
            }
        }
        //打开广播--设置
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                uint nTime = Convert.ToUInt32(textBox3.Text);
                ONNONLed5KSDKD.Led5kSDK.SetTimeout(m_dwCurHand, nTime);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入网络的超时时间");
            }
        }
        //创建固定IP通讯模式
        private void button4_Click(object sender, EventArgs e)
        {
            byte[] led_ip = System.Text.Encoding.ASCII.GetBytes(textBox6.Text);

            uint led_port = Convert.ToUInt32(textBox5.Text);

            byte[] card_type_list = new byte[14];
            card_type_list[0] = 0x51;
            card_type_list[1] = 0x58;
            card_type_list[2] = 0x54;
            card_type_list[3] = 0x53;
            card_type_list[4] = 0x5c;
            card_type_list[5] = 0xFE;
            card_type_list[6] = 0x61;
            card_type_list[7] = 0x62;
            card_type_list[8] = 0x63;
            card_type_list[9] = 0x64;
            card_type_list[10] = 0x65;
            card_type_list[11] = 0x66;
            card_type_list[12] = 0x67;
            card_type_list[13] = 0x68;
            int t = comboBox2.SelectedIndex;
            type = t;
            uint hand = ONNONLed5KSDKD.Led5kSDK.CreateClient(led_ip, led_port, (Led5kSDK.bx_5k_card_type)card_type_list[t],1,0, null);
            m_dwCurHand = hand;
            if (hand == 0)
            {
                MessageBox.Show("连接控制器失败");
            }
            else
            {
                MessageBox.Show("连接控制器成功");
            }
        }
        //创建固定IP通讯模式--设置
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                uint nTime = Convert.ToUInt32(textBox4.Text);
                ONNONLed5KSDKD.Led5kSDK.SetTimeout(m_dwCurHand, nTime);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入网络的超时时间");
            }
        }
        //断开连接
        private void button10_Click(object sender, EventArgs e)
        {
            ONNONLed5KSDKD.Led5kSDK.Destroy(m_dwCurHand);
            MessageBox.Show("ok");
        }

        //创建TCP Modbus通讯
        private void button6_Click(object sender, EventArgs e)
        {
            byte[] led_ip = System.Text.Encoding.ASCII.GetBytes(textBox9.Text);
            byte[] card_type_list = new byte[12];
            card_type_list[0] = 0xFE;
            card_type_list[1] = 0x51;
            card_type_list[2] = 0x58;
            card_type_list[3] = 0x54;
            card_type_list[4] = 0x53;
            card_type_list[5] = 0x5c;
            card_type_list[6] = 0x61;
            card_type_list[7] = 0x62;
            card_type_list[8] = 0x63;
            card_type_list[9] = 0x64;
            card_type_list[10] = 0x65;
            card_type_list[11] = 0x66;
            int t = comboBox3.SelectedIndex;
            type = t;
            uint hand = ONNONLed5KSDKD.Led5kSDK.CreateTcpModbus(led_ip, (Led5kSDK.bx_5k_card_type)card_type_list[t], null);
            m_dwCurHand = hand;
            if (hand == 0)
            {
                MessageBox.Show("连接控制器失败");
            }
            else
            {
                MessageBox.Show("连接控制器成功");
            }
        }
        //创建TCP Modbus通讯--设置
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                uint nTime = Convert.ToUInt32(textBox7.Text);
                ONNONLed5KSDKD.Led5kSDK.SetTimeout(m_dwCurHand, nTime);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入网络的超时时间");
            }
        }
        //485总线模式
        private void button8_Click(object sender, EventArgs e)
        {
            ushort ScreenID = Convert.ToUInt16(textBox11.Text);

            byte com = Convert.ToByte(textBox12.Text);
            byte[] card_type_list = new byte[12];
            card_type_list[0] = 0xFE;
            card_type_list[1] = 0x51;
            card_type_list[2] = 0x58;
            card_type_list[3] = 0x54;
            card_type_list[4] = 0x53;
            card_type_list[5] = 0x5c;
            card_type_list[6] = 0x61;
            card_type_list[7] = 0x62;
            card_type_list[8] = 0x63;
            card_type_list[9] = 0x64;
            card_type_list[10] = 0x65;
            card_type_list[11] = 0x66;
            int t = comboBox4.SelectedIndex;
            type = t;

            uint[] baudrate_list = new uint[2];
            baudrate_list[0] = 9600;
            baudrate_list[1] = 57600;
            int bl = comboBox7.SelectedIndex;

            uint hand = ONNONLed5KSDKD.Led5kSDK.CreateComClient(com, baudrate_list[bl], (Led5kSDK.bx_5k_card_type)card_type_list[t],1, ScreenID);

            m_dwCurHand = hand;
            if (hand == 0)
            {
                MessageBox.Show("连接控制器失败");
            }
            else
            {
                MessageBox.Show("连接控制器成功");
            }
        }
        //485总线模式--设置
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                uint nTime = Convert.ToUInt32(textBox10.Text);
                ONNONLed5KSDKD.Led5kSDK.SetTimeout(m_dwCurHand, nTime);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入网络的超时时间");
            }
        }
        //串口Modbus模式
        private void button9_Click(object sender, EventArgs e)
        {
            byte com = Convert.ToByte(textBox14.Text);

            uint[] baudrate_list = new uint[13];
            baudrate_list[0] = 300;
            baudrate_list[1] = 600;
            baudrate_list[2] = 1200;
            baudrate_list[3] = 2400;
            baudrate_list[4] = 4800;
            baudrate_list[5] = 9600;
            baudrate_list[6] = 14400;
            baudrate_list[7] = 19200; ;
            baudrate_list[8] = 38400;
            baudrate_list[9] = 56000;
            baudrate_list[10] = 57600;
            baudrate_list[11] = 115200;
            baudrate_list[12] = 128000;
            int bl = comboBox8.SelectedIndex;

            byte[] DataBits_list = new byte[5];
            DataBits_list[0] = 4;
            DataBits_list[1] = 5;
            DataBits_list[2] = 6;
            DataBits_list[3] = 7;
            DataBits_list[4] = 8;
            int dl = comboBox10.SelectedIndex;

            byte[] StopBits_list = new byte[3];
            StopBits_list[0] = 0;
            StopBits_list[1] = 1;
            StopBits_list[2] = 2;
            int sl = comboBox11.SelectedIndex;

            ushort ScreenID = Convert.ToUInt16(textBox8.Text);
            byte[] card_type_list = new byte[12];
            card_type_list[0] = 0xFE;
            card_type_list[1] = 0x51;
            card_type_list[2] = 0x58;
            card_type_list[3] = 0x54;
            card_type_list[4] = 0x53;
            card_type_list[5] = 0x5c;
            card_type_list[6] = 0x61;
            card_type_list[7] = 0x62;
            card_type_list[8] = 0x63;
            card_type_list[9] = 0x64;
            card_type_list[10] = 0x65;
            card_type_list[11] = 0x66;
            int t = comboBox5.SelectedIndex;
            type = t;

            byte[] Parity_list = new byte[5];
            Parity_list[0] = 0;
            Parity_list[1] = 1;
            Parity_list[2] = 2;
            Parity_list[3] = 3;
            Parity_list[4] = 4;
            int pl = comboBox9.SelectedIndex;

            uint hand = ONNONLed5KSDKD.Led5kSDK.CreateComModbus(com, baudrate_list[bl], (Led5kSDK.serial_parity)Parity_list[pl], (Led5kSDK.serial_databits)DataBits_list[dl],
              (Led5kSDK.serial_stopbits)StopBits_list[sl], (Led5kSDK.bx_5k_card_type)card_type_list[t], ScreenID);
            m_dwCurHand = hand;
            if (hand == 0)
            {
                MessageBox.Show("连接控制器失败");
            }
            else
            {
                MessageBox.Show("连接控制器成功");
            }
        }

        //发送节目
        private void button11_Click(object sender, EventArgs e)
        {
            ProgramDlg pd = new ProgramDlg();
            DialogResult ret = pd.ShowDialog();
            if (ret == DialogResult.OK)
            {
                textBox13.Text = pd.m_Program.name;
                int err = pd.m_Program.SendProgram(m_dwCurHand);
                if (err != 0)
                {
                    MessageBox.Show("发送节目失败");
                }
            }

        }

        //动态区域更新函数
        public void UpdateArea(uint i)
        {
            if (i == 0 || i == 1)
            {
                SoundArea a = new SoundArea(type);
                DialogResult ret = a.ShowDialog();

                if (ret == DialogResult.OK)
                { 
                    a.bx_5k.DynamicAreaLoc = (byte)i;
                    int x;
                    if(a.SoundMode == 0)
                    {
                        x = ONNONLed5KSDKD.Led5kSDK.SCREEN_SendDynamicArea(m_dwCurHand, a.bx_5k, (ushort)a.bx_5k.DataLen, SoundArea.AreaText);
                    }
                    else
                    {
                        x = ONNONLed5KSDKD.Led5kSDK.SCREEN_SendSoundDynamicArea(m_dwCurHand, a.bx_5k, (ushort)a.bx_5k.DataLen, SoundArea.AreaText, a.SoundMode, a.SoundPerson, a.SoundVolume, a.SoundSpeed, a.SoundDataLen, SoundArea.SoundAreaText);
                    }              
                    
                    if (x == 0)
                    {
                        MessageBox.Show("动态区域更新成功");
                    }
                    else
                    {
                        MessageBox.Show("动态区域更新失败");
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                Area a = new Area();
                DialogResult ret = a.ShowDialog();

                if (ret == DialogResult.OK)
                {
                    Area.bx_5k.DynamicAreaLoc = (byte)i;
                    int x = ONNONLed5KSDKD.Led5kSDK.SCREEN_SendDynamicArea(m_dwCurHand, Area.bx_5k, (ushort)Area.bx_5k.DataLen, Area.AreaText);

                    if (x == 0)
                    {
                        MessageBox.Show("动态区域更新成功");
                    }
                    else
                    {
                        MessageBox.Show("动态区域更新失败");
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        // 更新动态区1
        private void button17_Click(object sender, EventArgs e)
        {
            UpdateArea(0);
        }
        //更新动态区域2
        private void button18_Click(object sender, EventArgs e)
        {
            UpdateArea(1);
        }
        //更新动态区域3
        private void button19_Click(object sender, EventArgs e)
        {
            UpdateArea(2);
        }
        //更新动态区域4
        private void button20_Click(object sender, EventArgs e)
        {
            UpdateArea(3);
        }
        //更新动态区域5
        private void button21_Click(object sender, EventArgs e)
        {
            UpdateArea(4);
        }
        //删除动态区域1
        private void button39_Click(object sender, EventArgs e)
        {
            int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_DelDynamicArea(m_dwCurHand, 255);
            if (err == 0)
            {
                MessageBox.Show("删除动态区成功" + err);
            }
            else
            { MessageBox.Show("删除动态区失败" + err); }
        }
        //删除动态区域2
        private void button40_Click(object sender, EventArgs e)
        {
            int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_DelDynamicArea(m_dwCurHand, 1);
            if (err != 0)
            {
                MessageBox.Show("删除动态区失败");
            }
        }
        //删除动态区域3
        private void button38_Click(object sender, EventArgs e)
        {
            int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_DelDynamicArea(m_dwCurHand, 255);
            if (err != 0)
            {
                MessageBox.Show("删除动态区失败");
            }
        }
        //删除动态区域4
        private void button37_Click(object sender, EventArgs e)
        {
            int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_DelDynamicArea(m_dwCurHand, 3);
            if (err != 0)
            {
                MessageBox.Show("删除动态区失败");
            }
        }
        //删除动态区域5
        private void button36_Click(object sender, EventArgs e)
        {
            int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_DelDynamicArea(m_dwCurHand, 4);
            if (err != 0)
            {
                MessageBox.Show("删除动态区失败");
            }
        }
        //读取屏号
        private void button33_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand != 0)
            {
                ushort screenid = 0;
                int err = ONNONLed5KSDKD.Led5kSDK.CON_ReadScreenID(m_dwCurHand, ref screenid);
                if (err == 0)
                {
                    textBox19.Text = screenid.ToString();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //ping
        private void button24_Click(object sender, EventArgs e)
        {
            int x = ONNONLed5KSDKD.Led5kSDK.CON_PING(m_dwCurHand);
            if (x == 0)
            {
                MessageBox.Show("ping成功");
            }
            else
            {
                MessageBox.Show("ping失败" + x);
            }
        }

        //设定定时开机
        private void button29_Click(object sender, EventArgs e)
        {
            byte[] buff = new byte[12];
            int pos = 0;

            if (checkBox1.Checked)
            {
                buff[0] = Led5kProgram.byte2bcd((byte)dateTimePicker1.Value.Hour);
                buff[1] = Led5kProgram.byte2bcd((byte)dateTimePicker1.Value.Minute);
                buff[2] = Led5kProgram.byte2bcd((byte)dateTimePicker2.Value.Hour);
                buff[3] = Led5kProgram.byte2bcd((byte)dateTimePicker2.Value.Minute);
                pos += 4;
            }

            if (m_dwCurHand != 0)
            {
                byte[] pTimer = new byte[13];
                for (int i = 0; i < 13; i++)
                {
                    pTimer[i] = 0;
                }
                pTimer[0] = Convert.ToByte(pos / 4);
                for (int i = 0; i < pos; i++)
                {
                    pTimer[i + 1] = buff[i];
                }
                int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_TimeTurnOnOff(m_dwCurHand, pTimer, pos / 4);
                if (err != 0)
                {
                    MessageBox.Show("设定定时开关机失败");
                }
            }
        }
        //取消定时开关
        private void button30_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand != 0)
            {
                int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_CancelTimeOnOff(m_dwCurHand);
                if (err != 0)
                {
                    MessageBox.Show("取消定时失败");
                }
            }
        }
        //开机
        private void button31_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand != 0)
            {
                int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_ForceOnOff(m_dwCurHand, 1);
                if (err != 0)
                {
                    MessageBox.Show("开机失败");
                }
            }
        }
        //关机
        private void button32_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand != 0)
            {
                int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_ForceOnOff(m_dwCurHand, 2);
                if (err != 0)
                {
                    MessageBox.Show("关机失败");
                }
            }
        }
        //删除所有节目
        private void button15_Click(object sender, EventArgs e)
        {
            int err = ONNONLed5KSDKD.Led5kSDK.OFS_DeleteFile(m_dwCurHand, 0, null);
            if (err != 0)
            {
                MessageBox.Show("删除所有节目失败");
            }

        }
        // 格式化
        private void button16_Click(object sender, EventArgs e)
        {

            int err = ONNONLed5KSDKD.Led5kSDK.OFS_Formatting(m_dwCurHand);
            if (err != 0)
            {
                MessageBox.Show("格式化失败");
            }
        }
        // 删除节目
        private void button14_Click(object sender, EventArgs e)
        {
            byte[] name = new byte[5];
            name = System.Text.Encoding.Default.GetBytes(textBox13.Text);
            int err = ONNONLed5KSDKD.Led5kSDK.OFS_DeleteFile(m_dwCurHand, 1, name);
            if (err != 0)
            {
                MessageBox.Show("删除节目失败");
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }
        //解锁节目
        private void button13_Click(object sender, EventArgs e)
        {
            byte[] name = new byte[] { 0 };
            name = System.Text.Encoding.Default.GetBytes(textBox13.Text);
            int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_LockProgram(m_dwCurHand, 0, 0, name);
            if (err != 0)
            {
                MessageBox.Show("解锁节目失败");
            }
        }
        //锁定节目
        private void button12_Click(object sender, EventArgs e)
        {
            byte[] name = new byte[] { 0 };
            name = System.Text.Encoding.Default.GetBytes(textBox13.Text);
            int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_LockProgram(m_dwCurHand, 1, 0, name);
            if (err != 0)
            {
                MessageBox.Show("锁定节目失败");
            }
        }
        //校时
        private void button26_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand != 0)
            {
                int err = ONNONLed5KSDKD.Led5kSDK.CON_SytemClockCorrect(m_dwCurHand);
                if (err != 0)
                {
                    MessageBox.Show("校时失败");
                }
            }
        }
        //强制调亮
        private void button27_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand != 0)
            {
                string str;
                str = textBox18.Text;
                byte brightness = Convert.ToByte(str);
                int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_SetBrightness(m_dwCurHand, 1, brightness, null);
                if (err != 0)
                {
                    MessageBox.Show("设置亮度失败");
                }
            }
        }
        // 定时调亮
        private void button28_Click(object sender, EventArgs e)
        {

            if (m_dwCurHand != 0)
            {
                string str;
                str = textBox18.Text; ;
                byte[] brightness = new byte[] { 0 };
                brightness = System.BitConverter.GetBytes(10);
                int err = ONNONLed5KSDKD.Led5kSDK.SCREEN_SetBrightness(m_dwCurHand, 2, 0, brightness);
                if (err != 0)
                {
                    MessageBox.Show("设置亮度失败");
                }
            }
        }
        // 复位
        private void button25_Click(object sender, EventArgs e)
        {

            if (m_dwCurHand != 0)
            {
                int err = ONNONLed5KSDKD.Led5kSDK.CON_Reset(m_dwCurHand);
                if (err != 0)
                {
                    MessageBox.Show("复位失败" + err);
                }
            }
        }
        //设置屏号
        private void button34_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand != 0)
            {
                string str;
                str = textBox19.Text;
                ushort screenid = Convert.ToUInt16(str);
                int err = ONNONLed5KSDKD.Led5kSDK.CON_SetScreenID(m_dwCurHand, screenid);
                if (err != 0)
                {
                    MessageBox.Show("设置屏号失败");
                }
            }
        }
        // 查询固件
        private void button22_Click(object sender, EventArgs e)
        {
            byte[] name = new byte[5];
            for (int i = 0; i < name.Length; i++)
            { name[i] = 0; };
            byte[] version = new byte[9];
            for (int i = 0; i < version.Length; i++)
            { version[i] = 0; };
            byte[] datetime = new byte[20];
            for (int i = 0; i < datetime.Length; i++)
            { datetime[i] = 0; };
            int err = ONNONLed5KSDKD.Led5kSDK.CON_CheckCurrentFirmware(m_dwCurHand, name, version, datetime);
            if (err == 0)
            {
                textBox15.Text = System.Text.Encoding.Default.GetString(version);
                textBox16.Text = System.Text.Encoding.Default.GetString(datetime);
                textBox17.Text = System.Text.Encoding.Default.GetString(name);
            }
        }
        // 激活固件
        private void button23_Click(object sender, EventArgs e)
        {
            if (m_dwCurHand == 0)
            {
                string str;
                str = textBox17.Text;
                byte[] buff = new byte[5];
                buff = System.Text.Encoding.Default.GetBytes("F001");
                int err = ONNONLed5KSDKD.Led5kSDK.CON_FirmwareActivate(m_dwCurHand, buff);
                if (err != 0)
                {
                    MessageBox.Show("激活失败");
                }
            }
        }
        //查询控制器状态
        private void button35_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            byte[] pStatus = new byte[1024];
            for (int i = 0; i < pStatus.Length; i++)
            {
                pStatus[i] = 0;
            }
            ushort len = 0;
            int err = ONNONLed5KSDKD.Led5kSDK.CON_ControllerStatus(m_dwCurHand, pStatus, ref len);
            if (err == 0)
            {
                byte onoff = pStatus[0];
                comboBox6.SelectedIndex = onoff - 1;
                byte brightness = pStatus[1];
                byte[] date = new byte[20];

                ushort year = Convert.ToUInt16(pStatus[3] / 16 * 10 + pStatus[3] % 16 + pStatus[2] / 16 * 1000 + pStatus[2] % 16 * 100);
                byte month = Convert.ToByte(pStatus[4] / 16 * 10 + pStatus[4] % 16);
                byte day = Convert.ToByte(pStatus[5] / 16 * 10 + pStatus[5] % 16);
                byte hour = Convert.ToByte(pStatus[7] / 16 * 10 + pStatus[7] % 16);
                byte minute = Convert.ToByte(pStatus[8] / 16 * 10 + pStatus[8] % 16);
                byte sec = Convert.ToByte(pStatus[6] / 16 * 10 + pStatus[6] % 16);
                byte week = Convert.ToByte(pStatus[9] / 16 * 10 + pStatus[9] % 16);
                string str;
                str = brightness.ToString();
                textBox24.Text = str;
                //str = Convert.ToString(year) + "-" + Convert.ToString(month) + "-" + Convert.ToString(day) + " " + Convert.ToString(hour) +
                //    ":" + Convert.ToString(minute) + ":" + Convert.ToString(sec);
                textBox23.Text = string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, minute, sec);

                byte programnum = pStatus[10];
                textBox20.Text = string.Format("{0:d}", programnum);
                byte[] name = new byte[5];
                for (int i = 0; i < name.Length; i++)
                {
                    name[i] = 0;
                }
                for (int j = 0; j < 5; j++)
                {
                    name[j] = pStatus[j + 11]; ;
                }
                byte[] cur_program = new byte[5];
                name.CopyTo(cur_program, 0);
                textBox21.Text = System.Text.Encoding.Default.GetString(cur_program);

                //-------------
                byte SpecialDynaArea = pStatus[16];
                int z = pStatus[16];
                comboBox12.SelectedIndex = z;

                byte PageNum = pStatus[18];
                textBox26.Text = PageNum.ToString();

                byte DynaAreaNum = pStatus[17];
                textBox22.Text = DynaAreaNum.ToString();

                for (int i = listBox1.Items.Count; i > 0; i--)
                {
                    listBox1.Items.Remove(i - 1);
                }
                for (int i = 0; i < DynaAreaNum; i++)
                {
                    str = pStatus[18 + i].ToString();
                    listBox1.Items.Add(str);
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------
        private void SetSpecialAppDynamic()
        {
            SpecialAppDynamic sad = new SpecialAppDynamic();
            DialogResult ret = sad.ShowDialog();
            if (ret == DialogResult.OK)
            {
                int err = ONNONLed5KSDKD.Led5kSDK.BX5MK_SetSpecialAppDynamic(m_dwCurHand, SpecialAppDynamic.AreaX,
                    SpecialAppDynamic.AreaY, SpecialAppDynamic.AreaWidth, SpecialAppDynamic.AreaHigh,
                    SpecialAppDynamic.DataType, 0, SpecialAppDynamic.RunState, SpecialAppDynamic.Timeout,
                    SpecialAppDynamic.SingleLine, SpecialAppDynamic.Lines_sizes, SpecialAppDynamic.NewLine,
                    SpecialAppDynamic.StayTime);
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
        public byte PageNum;
        public ushort PageDataLen;
        public byte[] PageData;

        public byte BlockFlag;
        public ushort BlockAddr;
        public byte[] BlockData;
        public ushort BlockDataLen;
        //发送特殊动态区
        private void button41_Click(object sender, EventArgs e)
        {
            Display ds = new Display();
            Bitmap bit = ds.CreateContent("shanghaiyangbang", true, StringAlignment.Center);
            List<byte> tobyte = ds.ToBytes(bit, Display.ColorType.SINGLE, Display.MatrixType.RG);
            foreach (var v in tobyte)
            {
                string b = v.ToString();
                string h = Convert.ToString(v, 16);
                textBox2.Text += h + " ";
            }
            MessageBox.Show("shujushi" + textBox2.Text);
            SetSpecialAppDynamic();
            SpecialAppDynamic f4 = new SpecialAppDynamic();
            f4.ShowDialog();
            PageDataLen = 80;
            PageNum = 1;
            PageData = tobyte.ToArray();

      
            //PageDataLen = f4.PageDataLen;
            //PageNum = f4.PageNum;
            //PageData = f4.PageData;
            if (SpecialAppDynamic.DataType == 0)
            {
                //发送分页数据
                int err = ONNONLed5KSDKD.Led5kSDK.BX5MK_SendPageData(m_dwCurHand, PageNum, PageDataLen, PageData);
                if (err != 0)
                {
                    MessageBox.Show("发送分页数据失败");
                }
            }
            else
            {
                //发送点阵信息
                int err = ONNONLed5KSDKD.Led5kSDK.BX5MK_SendLatticeMessage(m_dwCurHand, BlockFlag, BlockAddr, BlockData, BlockDataLen);
                if (err != 0)
                {
                    MessageBox.Show("发送点阵信息失败");
                }
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            byte[] fontStatus = new byte[1024];
            for (int i = 0; i < fontStatus.Length; i++)
            {
                fontStatus[i] = 0;
            }
            ushort len = 0;
            int err = ONNONLed5KSDKD.Led5kSDK.CON_CheckCurrentFont(m_dwCurHand, fontStatus, ref len);
            if (err == 0)
            {
                string FontNum = "字库数量:" + fontStatus[0].ToString();
                listBox3.Items.Add(FontNum);
                for (int j = 0; j < fontStatus[0]; j++)
                {
                    string str;
                    if (fontStatus[j * 3 + 1] == 1)
                    {
                        str = "中文字库";
                    }
                    else { str = "英文字库"; }
                    int w = fontStatus[j * 3 + 2];
                    int h = fontStatus[j * 3 + 3];
                    string ss = "字库" + j + ":" + str + ",大小:" + w + "*" + h;
                    listBox3.Items.Add(ss);
                }
            }
            else { MessageBox.Show("查询失败"+err); }
        }

        ONNONLed5KSDKD.Led5kSDK.CloseFunc call;
        //处理消息 被委托的方法
        //int num;
        private void CloseFuncMethod(int total, int sendlen)
        {
            double number = (double)sendlen / total;
            progressBar1.Value = (int)(number * 100);
            //progressBar1.Value = num;
        }
        private void button43_Click(object sender, EventArgs e)
        {
            call = new ONNONLed5KSDKD.Led5kSDK.CloseFunc(CloseFuncMethod);
            XmlDocument xmlDoc;
            xmlDoc = new XmlDocument();
            xmlDoc.Load("Font.xml"); //加载xml文件
            XmlNode xn = xmlDoc.SelectSingleNode("Font_file");

            XmlNodeList xnl = xn.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement)xnf;
                //Console.WriteLine(xe.GetAttribute("genre"));//显示属性值
                //Console.WriteLine(xe.GetAttribute("ISBN"));

                XmlNodeList xnf1 = xe.ChildNodes;
                string Name = xnf.ChildNodes[0].InnerText;
                byte[] FileName = System.Text.Encoding.ASCII.GetBytes(Name);
                string tmpFilePath = xnf.ChildNodes[1].InnerText;
                FileStream readStream = new FileStream(tmpFilePath, FileMode.Open);
                byte[] data = new byte[readStream.Length];
                int LibData_len = (int)readStream.Length;
                while (true)
                {
                    int length = readStream.Read(data, 0, data.Length);
                    if (length == 0)
                    {
                        Console.WriteLine("读取结束");
                        break;
                    }
                }
                readStream.Close(); 
                int Width = int.Parse(xnf.ChildNodes[2].InnerText);
                byte FontWidth = (byte)Width;
                int Height = int.Parse(xnf.ChildNodes[3].InnerText);
                byte FontHeight = (byte)Height;
                int Encode = int.Parse(xnf.ChildNodes[4].InnerText);
                byte FontEncode = (byte)Encode;
                int err = ONNONLed5KSDKD.Led5kSDK.OFS_SendFontData(m_dwCurHand, 1, FileName, FontWidth, FontHeight,
                data, LibData_len, FontEncode, call);
                if (err == 0) 
                {
                    string list="更新字库" + Name + "成功";
                    listBox2.Items.Add(list);
                } 
            }
        }

        
        public static class N_call
        {
            public static Led5kSDK.CallBackCon callBack;// = new Led5kSDK.CallBackCon(CallBackMethod);
            public static Led5kSDK.CallBackLedClose callBackLedClose;// = new Led5kSDK.CallBackLedClose(CallBackMethod);
        }
        private void button45_Click(object sender, EventArgs e)
        {
            comboBox14.Items.Clear();
            N_call.callBack = new Led5kSDK.CallBackCon(CallBackMethod);
            N_call.callBackLedClose = new Led5kSDK.CallBackLedClose(CallBackMethod);
            uint port = uint.Parse(textBox27.Text);
            if (ONNONLed5KSDKD.Led5kSDK.StartGprsServer(port, N_call.callBack, N_call.callBackLedClose)) 
            {
                button45.Enabled = false;
                button46.Enabled = true;
                timer2.Enabled = true;
            }
        }
        public List<ItemObject> CallBac = new List<ItemObject>();
        public List<ItemObject> Del_CallBac = new List<ItemObject>();
        private void CallBackMethod(uint dwHand, string pid)
        {
            ItemObject n_call = new ItemObject(pid, dwHand);
            CallBac.Add(n_call); 
        }
        private void CallBackMethod(uint dwHand, string pid, int err_code)
        {
            foreach (ItemObject a in CallBac)
            {
                if (pid.Equals(a.Text)) { Del_CallBac.Add(a); }
            }
            foreach (ItemObject a in Del_CallBac)
            {
                if (pid.Equals(a.Text)) { CallBac.Remove(a); }
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            ONNONLed5KSDKD.Led5kSDK.CloseGprsServer();
            comboBox14.Items.Clear();
            CallBac.Clear();
            button45.Enabled = true;
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_dwCurHand = CallBac[comboBox14.SelectedIndex].Value;
        }

        private void button47_Click(object sender, EventArgs e)
        {
            byte[] CustomerStatus = new byte[16];
            for (int i = 0; i < CustomerStatus.Length; i++)
            {
                CustomerStatus[i] = 0;
            }
            ushort len = 0;
            int err = ONNONLed5KSDKD.Led5kSDK.CON_CheckCurrentCustomer(m_dwCurHand, CustomerStatus, ref len);
            if (err != 0)
            {
                MessageBox.Show("获取客户信息失败" + err);
            }
            else
            {
                textBox29.Text = System.Text.Encoding.Unicode.GetString(CustomerStatus);
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            byte[] ScreenStatus = new byte[1024];
            for (int i = 0; i < ScreenStatus.Length; i++)
            {
                ScreenStatus[i] = 0;
            }
            ushort len = 0;
            int err = ONNONLed5KSDKD.Led5kSDK.CON_ReadScreen(m_dwCurHand, ScreenStatus, ref len);
        }
        //设置客户信息
        private void button49_Click(object sender, EventArgs e)
        {
            byte[] ScreenStatus = new byte[16];
            byte[] ssr = System.Text.Encoding.Unicode.GetBytes(textBox28.Text);
            for (int i = 0; i < ScreenStatus.Length; i++)
            {
                if (i < ssr.Length)
                {
                    ScreenStatus[i] = ssr[i];
                }
            }
            int err = ONNONLed5KSDKD.Led5kSDK.OFS_SetFontInformation(m_dwCurHand, 1, ScreenStatus);
            if (err != 0)
            {
                MessageBox.Show("设置客户信息失败" + err);
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            byte[] pFileName=System.Text.Encoding.Default.GetBytes("C001");
            int err = ONNONLed5KSDKD.Led5kSDK.OFS_SendScreenData(m_dwCurHand, 1, pFileName, 1, 2,
            64, 32, 3, 0, 0, 0, 0, 0, 0, 5,0,1);
            if (err != 0)
            {
                MessageBox.Show("设置屏参信息失败" + err);
            }
        }

        private void FirmWare_Click(object sender, EventArgs e)
        {
            call = new ONNONLed5KSDKD.Led5kSDK.CloseFunc(CloseFuncMethod);
            FileStream readStream = new FileStream(textBox30.Text, FileMode.Open);
            byte[] FirmWaredata = new byte[readStream.Length];
            int FirmWaredata_len = (int)readStream.Length;
            while (true)
            {
                int length = readStream.Read(FirmWaredata, 0, FirmWaredata.Length);
                if (length == 0)
                {
                    Console.WriteLine("读取结束");
                    break;
                }
            }
            readStream.Close(); 
            byte[] pFileName = System.Text.Encoding.Default.GetBytes("F001");
            int err = ONNONLed5KSDKD.Led5kSDK.OFS_SendFirmWareData(m_dwCurHand, 1, pFileName, FirmWaredata, FirmWaredata_len,call);
            
            if (err != 0)
            {
                MessageBox.Show("发送失败"+err);
            }
            else
            {
                err = ONNONLed5KSDKD.Led5kSDK.CON_FirmwareActivate(m_dwCurHand, pFileName);
                if (err != 0)
                {
                    MessageBox.Show("激活失败" + err);
                }
            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox30.Text=openFileDialog1.FileName;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2.Interval == 100) 
            {
                for (int i = 0; i < CallBac.Count; i++)
                {
                    comboBox14.Items.Add(CallBac[i].Text);
                }
            }
            if (comboBox14.Items.Count > 0)
            {
                comboBox14.SelectedIndex = 0;
                timer2.Enabled = false;
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {

                int err = ONNONLed5KSDKD.Led5kSDK.BX5MK_SetSpecialAppDynamic(m_dwCurHand,
                    0,//AreaX 
                    0,//AreaY 
                    4,//AreaW 
                    32,//AreaH 
                    1,//DataType 
                    0,//Pagetotal 
                    0,//RunState 
                    1,//Timeout 
                    0,//SingleLine 
                    0, //Lines_sizes
                    0,//NewLine
                    0);//StayTime

            Display ds = new Display();
            
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.bmp)|*.bmp";
            string text="";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                text = fileDialog.FileName;
            }

            Bitmap _Bitmap = (Bitmap)Image.FromFile(text);
            List<byte> tobyte = ds.ToBytes(_Bitmap, Display.ColorType.DOUBLE, Display.MatrixType.RGGR);
            //foreach (var v in tobyte)
            //{
            //    string b = v.ToString();
            //    string h = Convert.ToString(v, 16);
            //    textBox2.Text += h + " ";
            //}
            //MessageBox.Show("shujushi" + textBox2.Text);
            //SetSpecialAppDynamic();
            //SpecialAppDynamic f4 = new SpecialAppDynamic();
            //f4.ShowDialog();
            //PageDataLen = 80;
            //PageNum = 1;
            //PageData = tobyte.ToArray();


            //PageDataLen = f4.PageDataLen;
            //PageNum = f4.PageNum;
            //PageData = f4.PageData;

            BlockData = tobyte.ToArray();
            BlockDataLen = (ushort)BlockData.Length;
            if (SpecialAppDynamic.DataType == 1)
            {
                //发送分页数据
                 err = ONNONLed5KSDKD.Led5kSDK.BX5MK_SendPageData(m_dwCurHand, PageNum, PageDataLen, PageData);
                if (err != 0)
                {
                    MessageBox.Show("发送分页数据失败");
                }
            }
            else
            {
                //发送点阵信息
                 err = ONNONLed5KSDKD.Led5kSDK.BX5MK_SendLatticeMessage(m_dwCurHand, 1, 0, BlockData, BlockDataLen);
                if (err != 0)
                {
                    MessageBox.Show("发送点阵信息失败");
                }
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {

        }

        public Led5kSDK.bx_5k_sound sound;
        public static byte[] AreaText;
        private void button53_Click(object sender, EventArgs e)
        {
            AreaText = System.Text.Encoding.Default.GetBytes(textBox31.Text.Trim());
            sound.StoreFlag = 1;
            sound.SoundPerson = 1;
            sound.SoundVolum = 10;
            sound.SoundSpeed = 5;
            sound.SoundDataMode = 1;
            sound.SoundReplayTimes = 1;
            sound.SoundReplayDelay = 255;
            sound.SoundReservedParaLen = 0;
            sound.SoundDataLen = AreaText.Length;
            int x = ONNONLed5KSDKD.Led5kSDK.SCREEN_SendSound(m_dwCurHand, sound, AreaText.Length, AreaText);
        }

        private void button54_Click(object sender, EventArgs e)
        {

        }

        private void button55_Click(object sender, EventArgs e)
        {
            byte cmd_group = (byte)Convert.ToInt32(textBox32.Text, 16);//Convert.ToByte(textBox32.Text);
            byte cmd = (byte)Convert.ToInt32(textBox33.Text, 16);
            byte[] cmd_data = new byte[0];
            if (textBox34.Text != "")
            {
                String[] numbers = textBox34.Text.Split(' ');
                cmd_data = new byte[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    String hex = numbers[i];
                    cmd_data[i] = (byte)Convert.ToInt32(hex, 16);
                }
            }
            else { }
            ushort data_len= (ushort)cmd_data.Length;
            byte[] recv_data=new byte[1024];
            for (int i = 0; i < recv_data.Length; i++)
            {
                recv_data[i] = 0;
            }
            short p_recv_len=0;
            int x = ONNONLed5KSDKD.Led5kSDK.SendAndRecvBuff(m_dwCurHand, cmd_group, cmd, cmd_data, data_len, recv_data,ref p_recv_len);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            byte cmd_group = (byte)Convert.ToInt32(textBox32.Text, 16);//Convert.ToByte(textBox32.Text);
            byte cmd = (byte)Convert.ToInt32(textBox33.Text, 16);
            byte[] cmd_data = new byte [0];
            if (textBox34.Text != "")
            {
                String[] numbers = textBox34.Text.Split(' ');
                cmd_data = new byte[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    String hex = numbers[i];
                    cmd_data[i] = (byte)Convert.ToInt32(hex, 16);
                }
            }
            else { }
            ushort data_len = (ushort)cmd_data.Length;
            int ack = ONNONLed5KSDKD.Led5kSDK.SendBuff(m_dwCurHand, cmd_group, cmd, cmd_data, data_len);
        }

        public Led5kDynamics m_Dynamic = new Led5kDynamics();
        private void button57_Click(object sender, EventArgs e)
        {
            Area f3 = new Area();
            DialogResult ret = f3.ShowDialog();
            if (ret == DialogResult.OK)
            {
                LedstaticArea area = new LedstaticArea();

                area.header = Area.bx_5k;
                area.text = Area.AreaTxt;
                m_Dynamic.m_arealist.Add(area);
                //this.listBox1.Items.Add(f3.TextBoxText.Trim());
            }

            f3.DialogResult = DialogResult.Cancel;
        }

        private void button58_Click(object sender, EventArgs e)
        {
                int err = m_Dynamic.SendAreas(m_dwCurHand);
                if (err != 0)
                {
                    MessageBox.Show("发送失败");
                }
        }

        private void NetworkSearch_Click(object sender, EventArgs e)
        {
            textBox35.Text = "";
            byte[] recv_buff = new byte[1024];
            for (int i = 0; i < recv_buff.Length; i++)
            {
                recv_buff[i] = 0;
            }
            ushort[] recv_len = new ushort[256];
            int err = ONNONLed5KSDKD.Led5kSDK.BX5MK_WebSearch(m_dwCurHand, recv_buff, recv_len);
            if (err == 0)
            {
                //int n = 0;
                //for (int m = 0; m < recv_len; m++)
                //{
                //    textBox35.Text += recv_buff[m].ToString("X2") + " ";
                //    if (m >= 53 && m <= 68) { mmmm[n] = recv_buff[m]; n++; }
                //}
                //textBox36.Text = System.Text.Encoding.ASCII.GetString(by);
            }
        }

        private void SetIP_Click(object sender, EventArgs e)
        {
            byte[] IP = System.Text.Encoding.ASCII.GetBytes(textBox6.Text);
            byte[] SubnetMask = System.Text.Encoding.Default.GetBytes("255.255.255.255");
            byte[] Gateway = System.Text.Encoding.Default.GetBytes("192.168.89.1");
            byte[] ServerIP = System.Text.Encoding.Default.GetBytes("");
            byte[] ServerAccessPassword = System.Text.Encoding.Default.GetBytes("");
            byte[] NetID = System.Text.Encoding.Default.GetBytes("BX-NET000001");
            int err = Led5kSDK.BX5MK_SetIPAddress(m_dwCurHand, 0x02, IP, SubnetMask, Gateway, (ushort)5005,
            0, ServerIP, (ushort)5007, ServerAccessPassword, (ushort)20, NetID);
            if (err == 0) { MessageBox.Show("OK"); } else { MessageBox.Show("falid"); }
        }

        private void btnOnlie_Click(object sender, EventArgs e)
        {

        }

        private void button59_Click(object sender, EventArgs e)
        {
            byte[] FileName = System.Text.Encoding.ASCII.GetBytes("T000");
            FileStream readStream = new FileStream("HP.bcm", FileMode.Open);
            byte[] data = new byte[readStream.Length];
            int LibData_len = (int)readStream.Length;
            while (true)
            {
                int length = readStream.Read(data, 0, data.Length);
                if (length == 0)
                {
                    Console.WriteLine("读取结束");
                    break;
                }
            }
            readStream.Close();
            int err = ONNONLed5KSDKD.Led5kSDK.OFS_SendFontData(m_dwCurHand, 1, FileName, 16, 16,
            data, LibData_len, 1, call);
            if (err == 0)
            {
                string list = "更新字库" + Name + "成功";
                listBox2.Items.Add(list);
            } 
        }
        //------------------------------------------------------------------------------------------------
    }
}
