namespace TestDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.timer_Tick = new System.Windows.Forms.Timer(this.components);
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button_Open = new System.Windows.Forms.Button();
			this.button_Close = new System.Windows.Forms.Button();
			this.button_Clear = new System.Windows.Forms.Button();
			this.button_T1 = new System.Windows.Forms.Button();
			this.button_T2 = new System.Windows.Forms.Button();
			this.button_Log = new System.Windows.Forms.Button();
			this.button_Exit = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.button_Log2 = new System.Windows.Forms.Button();
			this.button_Log3 = new System.Windows.Forms.Button();
			this.button_Log4 = new System.Windows.Forms.Button();
			this.button_Log5 = new System.Windows.Forms.Button();
			this.button_Log6 = new System.Windows.Forms.Button();
			this.button_CheckSum = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// timer_Tick
			// 
			this.timer_Tick.Enabled = true;
			this.timer_Tick.Interval = 1000;
			this.timer_Tick.Tick += new System.EventHandler(this.timer_Tick_Tick);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(44, 44);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(139, 20);
			this.comboBox1.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(44, 70);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(571, 385);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// button_Open
			// 
			this.button_Open.Location = new System.Drawing.Point(200, 41);
			this.button_Open.Name = "button_Open";
			this.button_Open.Size = new System.Drawing.Size(75, 23);
			this.button_Open.TabIndex = 2;
			this.button_Open.Text = "打开";
			this.button_Open.UseVisualStyleBackColor = true;
			this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
			// 
			// button_Close
			// 
			this.button_Close.Location = new System.Drawing.Point(281, 41);
			this.button_Close.Name = "button_Close";
			this.button_Close.Size = new System.Drawing.Size(75, 23);
			this.button_Close.TabIndex = 3;
			this.button_Close.Text = "关闭";
			this.button_Close.UseVisualStyleBackColor = true;
			this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
			// 
			// button_Clear
			// 
			this.button_Clear.Location = new System.Drawing.Point(631, 432);
			this.button_Clear.Name = "button_Clear";
			this.button_Clear.Size = new System.Drawing.Size(75, 23);
			this.button_Clear.TabIndex = 4;
			this.button_Clear.Text = "清除";
			this.button_Clear.UseVisualStyleBackColor = true;
			this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
			// 
			// button_T1
			// 
			this.button_T1.Location = new System.Drawing.Point(631, 374);
			this.button_T1.Name = "button_T1";
			this.button_T1.Size = new System.Drawing.Size(75, 23);
			this.button_T1.TabIndex = 5;
			this.button_T1.Text = "字节长度";
			this.button_T1.UseVisualStyleBackColor = true;
			this.button_T1.Click += new System.EventHandler(this.button_T1_Click);
			// 
			// button_T2
			// 
			this.button_T2.Location = new System.Drawing.Point(631, 403);
			this.button_T2.Name = "button_T2";
			this.button_T2.Size = new System.Drawing.Size(75, 23);
			this.button_T2.TabIndex = 6;
			this.button_T2.Text = "字长度";
			this.button_T2.UseVisualStyleBackColor = true;
			this.button_T2.Click += new System.EventHandler(this.button_T2_Click);
			// 
			// button_Log
			// 
			this.button_Log.Location = new System.Drawing.Point(712, 287);
			this.button_Log.Name = "button_Log";
			this.button_Log.Size = new System.Drawing.Size(168, 23);
			this.button_Log.TabIndex = 7;
			this.button_Log.Text = "固定文件名测试";
			this.button_Log.UseVisualStyleBackColor = true;
			this.button_Log.Click += new System.EventHandler(this.button_Log_Click);
			// 
			// button_Exit
			// 
			this.button_Exit.Location = new System.Drawing.Point(631, 345);
			this.button_Exit.Name = "button_Exit";
			this.button_Exit.Size = new System.Drawing.Size(75, 23);
			this.button_Exit.TabIndex = 8;
			this.button_Exit.Text = "退出测试";
			this.button_Exit.UseVisualStyleBackColor = true;
			this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown1.Location = new System.Drawing.Point(631, 70);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
			this.numericUpDown1.TabIndex = 9;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown1.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			// 
			// button_Log2
			// 
			this.button_Log2.Location = new System.Drawing.Point(712, 316);
			this.button_Log2.Name = "button_Log2";
			this.button_Log2.Size = new System.Drawing.Size(168, 23);
			this.button_Log2.TabIndex = 10;
			this.button_Log2.Text = "动态文件名测试";
			this.button_Log2.UseVisualStyleBackColor = true;
			this.button_Log2.Click += new System.EventHandler(this.button_Log2_Click);
			// 
			// button_Log3
			// 
			this.button_Log3.Location = new System.Drawing.Point(712, 345);
			this.button_Log3.Name = "button_Log3";
			this.button_Log3.Size = new System.Drawing.Size(168, 23);
			this.button_Log3.TabIndex = 11;
			this.button_Log3.Text = "固定文件夹和文件名测试";
			this.button_Log3.UseVisualStyleBackColor = true;
			this.button_Log3.Click += new System.EventHandler(this.button_Log3_Click);
			// 
			// button_Log4
			// 
			this.button_Log4.Location = new System.Drawing.Point(712, 374);
			this.button_Log4.Name = "button_Log4";
			this.button_Log4.Size = new System.Drawing.Size(168, 23);
			this.button_Log4.TabIndex = 12;
			this.button_Log4.Text = "固定文件夹动态文件名测试";
			this.button_Log4.UseVisualStyleBackColor = true;
			this.button_Log4.Click += new System.EventHandler(this.button_Log4_Click);
			// 
			// button_Log5
			// 
			this.button_Log5.Location = new System.Drawing.Point(712, 403);
			this.button_Log5.Name = "button_Log5";
			this.button_Log5.Size = new System.Drawing.Size(168, 23);
			this.button_Log5.TabIndex = 13;
			this.button_Log5.Text = "动态文件夹固定文件名测试";
			this.button_Log5.UseVisualStyleBackColor = true;
			this.button_Log5.Click += new System.EventHandler(this.button_Log5_Click);
			// 
			// button_Log6
			// 
			this.button_Log6.Location = new System.Drawing.Point(712, 432);
			this.button_Log6.Name = "button_Log6";
			this.button_Log6.Size = new System.Drawing.Size(168, 23);
			this.button_Log6.TabIndex = 14;
			this.button_Log6.Text = "动态文件夹和文件名测试";
			this.button_Log6.UseVisualStyleBackColor = true;
			this.button_Log6.Click += new System.EventHandler(this.button_Log6_Click);
			// 
			// button_CheckSum
			// 
			this.button_CheckSum.Location = new System.Drawing.Point(621, 237);
			this.button_CheckSum.Name = "button_CheckSum";
			this.button_CheckSum.Size = new System.Drawing.Size(75, 23);
			this.button_CheckSum.TabIndex = 15;
			this.button_CheckSum.Text = "CheckSum";
			this.button_CheckSum.UseVisualStyleBackColor = true;
			this.button_CheckSum.Click += new System.EventHandler(this.button_CheckSum_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 563);
			this.Controls.Add(this.button_CheckSum);
			this.Controls.Add(this.button_Log6);
			this.Controls.Add(this.button_Log5);
			this.Controls.Add(this.button_Log4);
			this.Controls.Add(this.button_Log3);
			this.Controls.Add(this.button_Log2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.button_Exit);
			this.Controls.Add(this.button_Log);
			this.Controls.Add(this.button_T2);
			this.Controls.Add(this.button_T1);
			this.Controls.Add(this.button_Clear);
			this.Controls.Add(this.button_Close);
			this.Controls.Add(this.button_Open);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.comboBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_Tick;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button_Open;
		private System.Windows.Forms.Button button_Close;
		private System.Windows.Forms.Button button_Clear;
		private System.Windows.Forms.Button button_T1;
		private System.Windows.Forms.Button button_T2;
		private System.Windows.Forms.Button button_Log;
		private System.Windows.Forms.Button button_Exit;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Button button_Log2;
		private System.Windows.Forms.Button button_Log3;
		private System.Windows.Forms.Button button_Log4;
		private System.Windows.Forms.Button button_Log5;
		private System.Windows.Forms.Button button_Log6;
		private System.Windows.Forms.Button button_CheckSum;
	}
}

