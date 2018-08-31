namespace BootLoaderForm
{
	partial class BootLoaderForm
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
			this.panel_port = new System.Windows.Forms.Panel();
			this.groupBox_port = new System.Windows.Forms.GroupBox();
			this.button_closePort = new System.Windows.Forms.Button();
			this.button_openPort = new System.Windows.Forms.Button();
			this.comboBox_portName = new System.Windows.Forms.ComboBox();
			this.label_portName = new System.Windows.Forms.Label();
			this.panel_SysTick = new System.Windows.Forms.Panel();
			this.richTextBox_msg = new System.Windows.Forms.RichTextBox();
			this.panel_update = new System.Windows.Forms.Panel();
			this.groupBox_update = new System.Windows.Forms.GroupBox();
			this.button_updateFirmware = new System.Windows.Forms.Button();
			this.progressBar_update = new System.Windows.Forms.ProgressBar();
			this.label_updateProgress = new System.Windows.Forms.Label();
			this.button_openFirmware = new System.Windows.Forms.Button();
			this.textBox_firmware = new System.Windows.Forms.TextBox();
			this.label_firmwareName = new System.Windows.Forms.Label();
			this.contextMenuStrip_richTextBox_msg = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.panel_msg = new System.Windows.Forms.Panel();
			this.toolStrip_BottomMenu = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel_SysTick = new System.Windows.Forms.ToolStripLabel();
			this.timer_SysTick = new System.Windows.Forms.Timer(this.components);
			this.toolStripLabel_SysTickName = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator__SysTickName = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator__SysTick = new System.Windows.Forms.ToolStripSeparator();
			this.panel_port.SuspendLayout();
			this.groupBox_port.SuspendLayout();
			this.panel_SysTick.SuspendLayout();
			this.panel_update.SuspendLayout();
			this.groupBox_update.SuspendLayout();
			this.panel_msg.SuspendLayout();
			this.toolStrip_BottomMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel_port
			// 
			this.panel_port.Controls.Add(this.groupBox_port);
			this.panel_port.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel_port.Location = new System.Drawing.Point(0, 0);
			this.panel_port.Margin = new System.Windows.Forms.Padding(3, 100, 3, 3);
			this.panel_port.Name = "panel_port";
			this.panel_port.Padding = new System.Windows.Forms.Padding(6, 6, 0, 2);
			this.panel_port.Size = new System.Drawing.Size(586, 86);
			this.panel_port.TabIndex = 0;
			// 
			// groupBox_port
			// 
			this.groupBox_port.Controls.Add(this.button_closePort);
			this.groupBox_port.Controls.Add(this.button_openPort);
			this.groupBox_port.Controls.Add(this.comboBox_portName);
			this.groupBox_port.Controls.Add(this.label_portName);
			this.groupBox_port.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox_port.Location = new System.Drawing.Point(6, 6);
			this.groupBox_port.Name = "groupBox_port";
			this.groupBox_port.Size = new System.Drawing.Size(137, 78);
			this.groupBox_port.TabIndex = 0;
			this.groupBox_port.TabStop = false;
			this.groupBox_port.Text = "通信配置";
			// 
			// button_closePort
			// 
			this.button_closePort.Location = new System.Drawing.Point(78, 49);
			this.button_closePort.Name = "button_closePort";
			this.button_closePort.Size = new System.Drawing.Size(56, 23);
			this.button_closePort.TabIndex = 3;
			this.button_closePort.Text = "关闭";
			this.button_closePort.UseVisualStyleBackColor = true;
			// 
			// button_openPort
			// 
			this.button_openPort.Location = new System.Drawing.Point(8, 49);
			this.button_openPort.Name = "button_openPort";
			this.button_openPort.Size = new System.Drawing.Size(56, 23);
			this.button_openPort.TabIndex = 2;
			this.button_openPort.Text = "打开";
			this.button_openPort.UseVisualStyleBackColor = true;
			// 
			// comboBox_portName
			// 
			this.comboBox_portName.FormattingEnabled = true;
			this.comboBox_portName.Location = new System.Drawing.Point(66, 23);
			this.comboBox_portName.Name = "comboBox_portName";
			this.comboBox_portName.Size = new System.Drawing.Size(68, 20);
			this.comboBox_portName.TabIndex = 1;
			// 
			// label_portName
			// 
			this.label_portName.AutoSize = true;
			this.label_portName.Location = new System.Drawing.Point(6, 26);
			this.label_portName.Name = "label_portName";
			this.label_portName.Size = new System.Drawing.Size(65, 12);
			this.label_portName.TabIndex = 0;
			this.label_portName.Text = "通信端口：";
			// 
			// panel_SysTick
			// 
			this.panel_SysTick.Controls.Add(this.toolStrip_BottomMenu);
			this.panel_SysTick.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel_SysTick.Location = new System.Drawing.Point(0, 478);
			this.panel_SysTick.Name = "panel_SysTick";
			this.panel_SysTick.Padding = new System.Windows.Forms.Padding(3);
			this.panel_SysTick.Size = new System.Drawing.Size(586, 30);
			this.panel_SysTick.TabIndex = 1;
			// 
			// richTextBox_msg
			// 
			this.richTextBox_msg.ContextMenuStrip = this.contextMenuStrip_richTextBox_msg;
			this.richTextBox_msg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox_msg.Location = new System.Drawing.Point(3, 3);
			this.richTextBox_msg.Name = "richTextBox_msg";
			this.richTextBox_msg.Size = new System.Drawing.Size(580, 149);
			this.richTextBox_msg.TabIndex = 0;
			this.richTextBox_msg.Text = "";
			// 
			// panel_update
			// 
			this.panel_update.Controls.Add(this.groupBox_update);
			this.panel_update.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel_update.Location = new System.Drawing.Point(0, 86);
			this.panel_update.Margin = new System.Windows.Forms.Padding(3, 100, 3, 3);
			this.panel_update.Name = "panel_update";
			this.panel_update.Padding = new System.Windows.Forms.Padding(6, 6, 6, 2);
			this.panel_update.Size = new System.Drawing.Size(586, 92);
			this.panel_update.TabIndex = 2;
			// 
			// groupBox_update
			// 
			this.groupBox_update.Controls.Add(this.button_updateFirmware);
			this.groupBox_update.Controls.Add(this.progressBar_update);
			this.groupBox_update.Controls.Add(this.label_updateProgress);
			this.groupBox_update.Controls.Add(this.button_openFirmware);
			this.groupBox_update.Controls.Add(this.textBox_firmware);
			this.groupBox_update.Controls.Add(this.label_firmwareName);
			this.groupBox_update.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox_update.Location = new System.Drawing.Point(6, 6);
			this.groupBox_update.Name = "groupBox_update";
			this.groupBox_update.Size = new System.Drawing.Size(574, 84);
			this.groupBox_update.TabIndex = 0;
			this.groupBox_update.TabStop = false;
			this.groupBox_update.Text = "更新配置";
			// 
			// button_updateFirmware
			// 
			this.button_updateFirmware.Location = new System.Drawing.Point(493, 53);
			this.button_updateFirmware.Name = "button_updateFirmware";
			this.button_updateFirmware.Size = new System.Drawing.Size(75, 23);
			this.button_updateFirmware.TabIndex = 5;
			this.button_updateFirmware.Text = "更新固件";
			this.button_updateFirmware.UseVisualStyleBackColor = true;
			// 
			// progressBar_update
			// 
			this.progressBar_update.Location = new System.Drawing.Point(66, 53);
			this.progressBar_update.Name = "progressBar_update";
			this.progressBar_update.Size = new System.Drawing.Size(421, 23);
			this.progressBar_update.TabIndex = 4;
			// 
			// label_updateProgress
			// 
			this.label_updateProgress.AutoSize = true;
			this.label_updateProgress.Location = new System.Drawing.Point(6, 58);
			this.label_updateProgress.Name = "label_updateProgress";
			this.label_updateProgress.Size = new System.Drawing.Size(65, 12);
			this.label_updateProgress.TabIndex = 3;
			this.label_updateProgress.Text = "更新进度：";
			// 
			// button_openFirmware
			// 
			this.button_openFirmware.Location = new System.Drawing.Point(493, 18);
			this.button_openFirmware.Name = "button_openFirmware";
			this.button_openFirmware.Size = new System.Drawing.Size(75, 23);
			this.button_openFirmware.TabIndex = 2;
			this.button_openFirmware.Text = "选择固件";
			this.button_openFirmware.UseVisualStyleBackColor = true;
			// 
			// textBox_firmware
			// 
			this.textBox_firmware.Location = new System.Drawing.Point(66, 20);
			this.textBox_firmware.Name = "textBox_firmware";
			this.textBox_firmware.Size = new System.Drawing.Size(421, 21);
			this.textBox_firmware.TabIndex = 1;
			// 
			// label_firmwareName
			// 
			this.label_firmwareName.AutoSize = true;
			this.label_firmwareName.Location = new System.Drawing.Point(6, 26);
			this.label_firmwareName.Name = "label_firmwareName";
			this.label_firmwareName.Size = new System.Drawing.Size(65, 12);
			this.label_firmwareName.TabIndex = 0;
			this.label_firmwareName.Text = "固件名称：";
			// 
			// contextMenuStrip_richTextBox_msg
			// 
			this.contextMenuStrip_richTextBox_msg.Name = "contextMenuStrip_richTextBox_msg";
			this.contextMenuStrip_richTextBox_msg.Size = new System.Drawing.Size(61, 4);
			// 
			// panel_msg
			// 
			this.panel_msg.Controls.Add(this.richTextBox_msg);
			this.panel_msg.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel_msg.Location = new System.Drawing.Point(0, 323);
			this.panel_msg.Name = "panel_msg";
			this.panel_msg.Padding = new System.Windows.Forms.Padding(3);
			this.panel_msg.Size = new System.Drawing.Size(586, 155);
			this.panel_msg.TabIndex = 3;
			// 
			// toolStrip_BottomMenu
			// 
			this.toolStrip_BottomMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip_BottomMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel_SysTickName,
            this.toolStripSeparator__SysTickName,
            this.toolStripLabel_SysTick,
            this.toolStripSeparator__SysTick});
			this.toolStrip_BottomMenu.Location = new System.Drawing.Point(3, 3);
			this.toolStrip_BottomMenu.Name = "toolStrip_BottomMenu";
			this.toolStrip_BottomMenu.Size = new System.Drawing.Size(580, 24);
			this.toolStrip_BottomMenu.TabIndex = 4;
			this.toolStrip_BottomMenu.Text = "toolStrip1";
			// 
			// toolStripLabel_SysTick
			// 
			this.toolStripLabel_SysTick.Name = "toolStripLabel_SysTick";
			this.toolStripLabel_SysTick.Size = new System.Drawing.Size(119, 21);
			this.toolStripLabel_SysTick.Text = "2018/8/30 00:00:00";
			// 
			// timer_SysTick
			// 
			this.timer_SysTick.Enabled = true;
			this.timer_SysTick.Interval = 1000;
			// 
			// toolStripLabel_SysTickName
			// 
			this.toolStripLabel_SysTickName.Name = "toolStripLabel_SysTickName";
			this.toolStripLabel_SysTickName.Size = new System.Drawing.Size(56, 21);
			this.toolStripLabel_SysTickName.Text = "当前时间";
			// 
			// toolStripSeparator__SysTickName
			// 
			this.toolStripSeparator__SysTickName.Name = "toolStripSeparator__SysTickName";
			this.toolStripSeparator__SysTickName.Size = new System.Drawing.Size(6, 24);
			// 
			// toolStripSeparator__SysTick
			// 
			this.toolStripSeparator__SysTick.Name = "toolStripSeparator__SysTick";
			this.toolStripSeparator__SysTick.Size = new System.Drawing.Size(6, 24);
			// 
			// BootLoaderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(586, 508);
			this.Controls.Add(this.panel_msg);
			this.Controls.Add(this.panel_update);
			this.Controls.Add(this.panel_SysTick);
			this.Controls.Add(this.panel_port);
			this.Name = "BootLoaderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "固件更新";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BootLoaderForm_FormClosing);
			this.Load += new System.EventHandler(this.BootLoaderForm_Load);
			this.LocationChanged += new System.EventHandler(this.BootLoaderForm_LocationChanged);
			this.panel_port.ResumeLayout(false);
			this.groupBox_port.ResumeLayout(false);
			this.groupBox_port.PerformLayout();
			this.panel_SysTick.ResumeLayout(false);
			this.panel_SysTick.PerformLayout();
			this.panel_update.ResumeLayout(false);
			this.groupBox_update.ResumeLayout(false);
			this.groupBox_update.PerformLayout();
			this.panel_msg.ResumeLayout(false);
			this.toolStrip_BottomMenu.ResumeLayout(false);
			this.toolStrip_BottomMenu.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel_port;
		private System.Windows.Forms.Panel panel_SysTick;
		private System.Windows.Forms.RichTextBox richTextBox_msg;
		private System.Windows.Forms.GroupBox groupBox_port;
		private System.Windows.Forms.Button button_closePort;
		private System.Windows.Forms.Button button_openPort;
		private System.Windows.Forms.ComboBox comboBox_portName;
		private System.Windows.Forms.Label label_portName;
		private System.Windows.Forms.Panel panel_update;
		private System.Windows.Forms.GroupBox groupBox_update;
		private System.Windows.Forms.Button button_openFirmware;
		private System.Windows.Forms.TextBox textBox_firmware;
		private System.Windows.Forms.Label label_firmwareName;
		private System.Windows.Forms.Button button_updateFirmware;
		private System.Windows.Forms.ProgressBar progressBar_update;
		private System.Windows.Forms.Label label_updateProgress;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_richTextBox_msg;
		private System.Windows.Forms.Panel panel_msg;
		private System.Windows.Forms.ToolStrip toolStrip_BottomMenu;
		private System.Windows.Forms.ToolStripLabel toolStripLabel_SysTick;
		private System.Windows.Forms.Timer timer_SysTick;
		private System.Windows.Forms.ToolStripLabel toolStripLabel_SysTickName;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator__SysTickName;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator__SysTick;
	}
}

