namespace RFASKFreqCurrentScanForm
{
    partial class RFASKFreqCurrentScanForm
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
            this.tabControl_FuncMenu = new System.Windows.Forms.TabControl();
            this.tabPage_Func = new System.Windows.Forms.TabPage();
            this.tabPage_Chart = new System.Windows.Forms.TabPage();
            this.panel_PortName = new System.Windows.Forms.Panel();
            this.button_CloseDevice = new System.Windows.Forms.Button();
            this.button_OpenDevice = new System.Windows.Forms.Button();
            this.label_PortName = new System.Windows.Forms.Label();
            this.comboBox_PortName = new System.Windows.Forms.ComboBox();
            this.toolStrip_BottomMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel_SysTickName = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator__SysTickName = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_SysTick = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator__SysTick = new System.Windows.Forms.ToolStripSeparator();
            this.timer_SysTick = new System.Windows.Forms.Timer(this.components);
            this.richTextBox_msg = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip_richTextBox_msg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl_FuncMenu.SuspendLayout();
            this.tabPage_Func.SuspendLayout();
            this.panel_PortName.SuspendLayout();
            this.toolStrip_BottomMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_FuncMenu
            // 
            this.tabControl_FuncMenu.AllowDrop = true;
            this.tabControl_FuncMenu.Controls.Add(this.tabPage_Func);
            this.tabControl_FuncMenu.Controls.Add(this.tabPage_Chart);
            this.tabControl_FuncMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_FuncMenu.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl_FuncMenu.ItemSize = new System.Drawing.Size(48, 18);
            this.tabControl_FuncMenu.Location = new System.Drawing.Point(0, 0);
            this.tabControl_FuncMenu.Name = "tabControl_FuncMenu";
            this.tabControl_FuncMenu.Padding = new System.Drawing.Point(16, 3);
            this.tabControl_FuncMenu.SelectedIndex = 0;
            this.tabControl_FuncMenu.Size = new System.Drawing.Size(980, 677);
            this.tabControl_FuncMenu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_FuncMenu.TabIndex = 0;
            // 
            // tabPage_Func
            // 
            this.tabPage_Func.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Func.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage_Func.Controls.Add(this.richTextBox_msg);
            this.tabPage_Func.Controls.Add(this.panel_PortName);
            this.tabPage_Func.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage_Func.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Func.Name = "tabPage_Func";
            this.tabPage_Func.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Func.Size = new System.Drawing.Size(972, 651);
            this.tabPage_Func.TabIndex = 0;
            this.tabPage_Func.Text = "功能";
            // 
            // tabPage_Chart
            // 
            this.tabPage_Chart.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Chart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage_Chart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage_Chart.ForeColor = System.Drawing.Color.LightGray;
            this.tabPage_Chart.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Chart.Name = "tabPage_Chart";
            this.tabPage_Chart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Chart.Size = new System.Drawing.Size(972, 651);
            this.tabPage_Chart.TabIndex = 1;
            this.tabPage_Chart.Text = "曲线";
            // 
            // panel_PortName
            // 
            this.panel_PortName.Controls.Add(this.button_CloseDevice);
            this.panel_PortName.Controls.Add(this.button_OpenDevice);
            this.panel_PortName.Controls.Add(this.label_PortName);
            this.panel_PortName.Controls.Add(this.comboBox_PortName);
            this.panel_PortName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel_PortName.Location = new System.Drawing.Point(1, 1);
            this.panel_PortName.Name = "panel_PortName";
            this.panel_PortName.Size = new System.Drawing.Size(252, 38);
            this.panel_PortName.TabIndex = 1;
            // 
            // button_CloseDevice
            // 
            this.button_CloseDevice.Location = new System.Drawing.Point(192, 8);
            this.button_CloseDevice.Name = "button_CloseDevice";
            this.button_CloseDevice.Size = new System.Drawing.Size(48, 23);
            this.button_CloseDevice.TabIndex = 3;
            this.button_CloseDevice.Text = "关闭";
            this.button_CloseDevice.UseVisualStyleBackColor = true;
            // 
            // button_OpenDevice
            // 
            this.button_OpenDevice.Location = new System.Drawing.Point(138, 8);
            this.button_OpenDevice.Name = "button_OpenDevice";
            this.button_OpenDevice.Size = new System.Drawing.Size(48, 23);
            this.button_OpenDevice.TabIndex = 2;
            this.button_OpenDevice.Text = "打开";
            this.button_OpenDevice.UseVisualStyleBackColor = true;
            // 
            // label_PortName
            // 
            this.label_PortName.AutoSize = true;
            this.label_PortName.Location = new System.Drawing.Point(3, 13);
            this.label_PortName.Name = "label_PortName";
            this.label_PortName.Size = new System.Drawing.Size(47, 12);
            this.label_PortName.TabIndex = 1;
            this.label_PortName.Text = "端口号:";
            // 
            // comboBox_PortName
            // 
            this.comboBox_PortName.FormattingEnabled = true;
            this.comboBox_PortName.Location = new System.Drawing.Point(56, 10);
            this.comboBox_PortName.Name = "comboBox_PortName";
            this.comboBox_PortName.Size = new System.Drawing.Size(74, 20);
            this.comboBox_PortName.TabIndex = 0;
            // 
            // toolStrip_BottomMenu
            // 
            this.toolStrip_BottomMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip_BottomMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel_SysTickName,
            this.toolStripSeparator__SysTickName,
            this.toolStripLabel_SysTick,
            this.toolStripSeparator__SysTick});
            this.toolStrip_BottomMenu.Location = new System.Drawing.Point(0, 652);
            this.toolStrip_BottomMenu.Name = "toolStrip_BottomMenu";
            this.toolStrip_BottomMenu.Size = new System.Drawing.Size(980, 25);
            this.toolStrip_BottomMenu.TabIndex = 5;
            this.toolStrip_BottomMenu.Text = "toolStrip1";
            // 
            // toolStripLabel_SysTickName
            // 
            this.toolStripLabel_SysTickName.Name = "toolStripLabel_SysTickName";
            this.toolStripLabel_SysTickName.Size = new System.Drawing.Size(56, 783);
            this.toolStripLabel_SysTickName.Text = "当前时间";
            // 
            // toolStripSeparator__SysTickName
            // 
            this.toolStripSeparator__SysTickName.Name = "toolStripSeparator__SysTickName";
            this.toolStripSeparator__SysTickName.Size = new System.Drawing.Size(6, 786);
            // 
            // toolStripLabel_SysTick
            // 
            this.toolStripLabel_SysTick.Name = "toolStripLabel_SysTick";
            this.toolStripLabel_SysTick.Size = new System.Drawing.Size(119, 783);
            this.toolStripLabel_SysTick.Text = "2018/8/30 00:00:00";
            // 
            // toolStripSeparator__SysTick
            // 
            this.toolStripSeparator__SysTick.Name = "toolStripSeparator__SysTick";
            this.toolStripSeparator__SysTick.Size = new System.Drawing.Size(6, 786);
            // 
            // timer_SysTick
            // 
            this.timer_SysTick.Enabled = true;
            this.timer_SysTick.Interval = 1000;
            // 
            // richTextBox_msg
            // 
            this.richTextBox_msg.ContextMenuStrip = this.contextMenuStrip_richTextBox_msg;
            this.richTextBox_msg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox_msg.Location = new System.Drawing.Point(3, 470);
            this.richTextBox_msg.Name = "richTextBox_msg";
            this.richTextBox_msg.Size = new System.Drawing.Size(962, 174);
            this.richTextBox_msg.TabIndex = 2;
            this.richTextBox_msg.Text = "";
            // 
            // contextMenuStrip_richTextBox_msg
            // 
            this.contextMenuStrip_richTextBox_msg.Name = "contextMenuStrip_richTextBox_msg";
            this.contextMenuStrip_richTextBox_msg.Size = new System.Drawing.Size(61, 4);
            // 
            // RFASKFreqCurrentScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(980, 677);
            this.Controls.Add(this.toolStrip_BottomMenu);
            this.Controls.Add(this.tabControl_FuncMenu);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "RFASKFreqCurrentScanForm";
            this.Text = "ASK频率电流扫描";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RFASKFreqCurrentScanForm_FormClosing);
            this.Load += new System.EventHandler(this.RFASKFreqCurrentScanForm_Load);
            this.tabControl_FuncMenu.ResumeLayout(false);
            this.tabPage_Func.ResumeLayout(false);
            this.panel_PortName.ResumeLayout(false);
            this.panel_PortName.PerformLayout();
            this.toolStrip_BottomMenu.ResumeLayout(false);
            this.toolStrip_BottomMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_FuncMenu;
        private System.Windows.Forms.TabPage tabPage_Func;
        private System.Windows.Forms.TabPage tabPage_Chart;
        private System.Windows.Forms.Panel panel_PortName;
        private System.Windows.Forms.Button button_CloseDevice;
        private System.Windows.Forms.Button button_OpenDevice;
        private System.Windows.Forms.Label label_PortName;
        private System.Windows.Forms.ComboBox comboBox_PortName;
        private System.Windows.Forms.ToolStrip toolStrip_BottomMenu;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_SysTickName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator__SysTickName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_SysTick;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator__SysTick;
        private System.Windows.Forms.Timer timer_SysTick;
        private System.Windows.Forms.RichTextBox richTextBox_msg;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_richTextBox_msg;
    }
}

