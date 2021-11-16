namespace Led5KSDKDemoCSharp
{
    partial class AddScreencs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numScreenPart = new System.Windows.Forms.NumericUpDown();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.textGate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textSubNetMask = new System.Windows.Forms.TextBox();
            this.textIP = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioDHCP = new System.Windows.Forms.RadioButton();
            this.btnSetIP = new System.Windows.Forms.Button();
            this.dataScreen = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numScreenPart)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Gateway";
            // 
            // numScreenPart
            // 
            this.numScreenPart.Location = new System.Drawing.Point(392, 84);
            this.numScreenPart.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numScreenPart.Minimum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numScreenPart.Name = "numScreenPart";
            this.numScreenPart.Size = new System.Drawing.Size(120, 21);
            this.numScreenPart.TabIndex = 10;
            this.numScreenPart.Value = new decimal(new int[] {
            5005,
            0,
            0,
            0});
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(746, 365);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // textGate
            // 
            this.textGate.Location = new System.Drawing.Point(267, 84);
            this.textGate.Name = "textGate";
            this.textGate.Size = new System.Drawing.Size(100, 21);
            this.textGate.TabIndex = 7;
            this.textGate.Text = "192.168.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Subnet mask";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(848, 365);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // textSubNetMask
            // 
            this.textSubNetMask.Location = new System.Drawing.Point(148, 84);
            this.textSubNetMask.Name = "textSubNetMask";
            this.textSubNetMask.Size = new System.Drawing.Size(100, 21);
            this.textSubNetMask.TabIndex = 5;
            this.textSubNetMask.Text = "255.255.255";
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(31, 84);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(100, 21);
            this.textIP.TabIndex = 3;
            this.textIP.Text = "192.168.0.1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numScreenPart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textGate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textSubNetMask);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textIP);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioDHCP);
            this.groupBox1.Controls.Add(this.btnSetIP);
            this.groupBox1.Location = new System.Drawing.Point(3, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(943, 111);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(31, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "Client";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioDHCP
            // 
            this.radioDHCP.AutoSize = true;
            this.radioDHCP.Checked = true;
            this.radioDHCP.Location = new System.Drawing.Point(31, 21);
            this.radioDHCP.Name = "radioDHCP";
            this.radioDHCP.Size = new System.Drawing.Size(47, 16);
            this.radioDHCP.TabIndex = 1;
            this.radioDHCP.TabStop = true;
            this.radioDHCP.Text = "DHCP";
            this.radioDHCP.UseVisualStyleBackColor = true;
            // 
            // btnSetIP
            // 
            this.btnSetIP.Enabled = false;
            this.btnSetIP.Location = new System.Drawing.Point(845, 81);
            this.btnSetIP.Name = "btnSetIP";
            this.btnSetIP.Size = new System.Drawing.Size(75, 23);
            this.btnSetIP.TabIndex = 0;
            this.btnSetIP.Text = "Setup";
            this.btnSetIP.UseVisualStyleBackColor = true;
            // 
            // dataScreen
            // 
            this.dataScreen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataScreen.Location = new System.Drawing.Point(3, 1);
            this.dataScreen.MultiSelect = false;
            this.dataScreen.Name = "dataScreen";
            this.dataScreen.ReadOnly = true;
            this.dataScreen.RowTemplate.Height = 23;
            this.dataScreen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataScreen.Size = new System.Drawing.Size(943, 241);
            this.dataScreen.TabIndex = 5;
            // 
            // AddScreencs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 393);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataScreen);
            this.Name = "AddScreencs";
            this.Text = "AddScreencs";
            ((System.ComponentModel.ISupportInitialize)(this.numScreenPart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numScreenPart;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox textGate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textSubNetMask;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioDHCP;
        private System.Windows.Forms.Button btnSetIP;
        public System.Windows.Forms.DataGridView dataScreen;
    }
}