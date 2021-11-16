using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ONNONLed5KSDKD;

namespace Led5KSDKDemoCSharp
{
    public partial class ProgramDlg : Form
    {
        public ProgramDlg()
        {
            InitializeComponent();
        }

        public Led5kProgram m_Program = new Led5kProgram();
        private void ProgramDlg_Load(object sender, EventArgs e)
        {
        }
        //取消
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //确定
        private void button3_Click(object sender, EventArgs e)
        {
            int num = listBox1.Items.Count;
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入文件名");
            }
            else
            {
                m_Program.AreaNum =Convert.ToByte(listBox1.Items.Count);
                m_Program.overwrite = true;
                m_Program.name = textBox1.Text;
                #region 过滤工作日
           
                if (checkBox3.Checked)
                {
                    m_Program.ProgramWeek = 1;
                }
                else
                {
                    int x1 = (checkBox4.Checked ? 1 : 0) * (1 << 1);
                    int x2 = (checkBox5.Checked ? 1 : 0) * (1 << 2);
                    int x3 = (checkBox6.Checked ? 1 : 0) * (1 << 3);
                    int x4 = (checkBox7.Checked ? 1 : 0) * (1 << 4);
                    int x5 = (checkBox8.Checked ? 1 : 0) * (1 << 5);
                    int x6 = (checkBox9.Checked ? 1 : 0) * (1 << 6);
                    int x7 = (checkBox10.Checked ? 1 : 0) * (1 << 7);

                    m_Program.ProgramWeek = Convert.ToByte(x1 + x2 + x3 + x4 + x5 + x6 + x7);
                }
                #endregion
                #region 播报时段
                if (checkBox2.Checked)
                {
                    m_Program.IsPlayOnTime = false;
                }
                else 
                {
                    m_Program.IsPlayOnTime = true;
                    m_Program.StartHour =(byte)dateTimePicker4.Value.Hour;
                    m_Program.StartMinute = (byte)dateTimePicker4.Value.Minute;
                    m_Program.StartSecond = (byte)dateTimePicker4.Value.Second;
                    m_Program.EndHour = (byte)dateTimePicker3.Value.Hour;
                    m_Program.EndMinute=(byte)dateTimePicker3.Value.Minute;
                    m_Program.EndSecond =(byte)dateTimePicker3.Value.Second;
                }
                #endregion
                #region 有效时间段
                if (checkBox1.Checked)
                {
                    m_Program.IsValidAlways = true;
                }
                else
                {
                    m_Program.IsValidAlways = false;
                    m_Program.StartYear = (ushort)dateTimePicker1.Value.Year;
                    m_Program.StartMonth = (byte)dateTimePicker1.Value.Month;
                    m_Program.StartDay = (byte)dateTimePicker1.Value.Day;
                    m_Program.EndYear = (ushort)dateTimePicker2.Value.Year;
                    m_Program.EndMonth = (byte)dateTimePicker2.Value.Month;
                    m_Program.EndDay = (byte)dateTimePicker2.Value.Day;


                }
                #endregion
                #region 播放方式
                if (radioButton1.Checked == true)
                {
                    m_Program.DisplayType = 0;
                    m_Program.PlayTimes = Convert.ToByte(textBox2.Text.Trim());
                }
                else
                {
                    m_Program.DisplayType = Convert.ToByte(textBox3.Text.Trim());
                    m_Program.PlayTimes = Convert.ToByte(textBox3.Text.Trim());
                }
                #endregion

            }
        }

        //一周循环
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                

            }
            else
            {
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
                checkBox8.Enabled = true;
                checkBox9.Enabled = true;
                checkBox10.Enabled = true;
            }

        }
        //有效事件范围
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;

            }
            else
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;

            }

        }
        //播放时段
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
            }
            else
            {
                dateTimePicker3.Enabled = true;
                dateTimePicker4.Enabled = true;
               

            }

        }
 
        //添加图文区
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            DialogResult ret = f3.ShowDialog();
            if (ret == DialogResult.OK)
            {
                Led5kstaticArea area = new Led5kstaticArea();

                area.header = Form3.bx_5k;
                area.text=Form3.AreaText;
                m_Program.m_arealist.Add(area);
                this.listBox1.Items.Add(f3.TextBoxText.Trim());
            }

            f3.DialogResult = DialogResult.Cancel;
            
        }
        #region 播放方式
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
            }
        }
        #endregion

        #region
        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        //移出图文区
        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.Items.Count;
            if (index != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                //m_Program.m_arealist.RemoveAt(listBox1.SelectedIndex);
                
            }
            
        }
    }
}
