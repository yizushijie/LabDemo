namespace LabDemoForm
{
	partial class LabMdiForm
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
			this.menuStrip_LabMdiMenuBar = new System.Windows.Forms.MenuStrip();
			this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Func = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_GenRF = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Tool = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Calc = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_TXT = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Wind = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip_LabMdiMenuBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip_LabMdiMenuBar
			// 
			this.menuStrip_LabMdiMenuBar.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
			this.menuStrip_LabMdiMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File,
            this.ToolStripMenuItem_Edit,
            this.ToolStripMenuItem_Func,
            this.ToolStripMenuItem_Tool,
            this.ToolStripMenuItem_Wind,
            this.ToolStripMenuItem_Help});
			this.menuStrip_LabMdiMenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.menuStrip_LabMdiMenuBar.Location = new System.Drawing.Point(0, 0);
			this.menuStrip_LabMdiMenuBar.Name = "menuStrip_LabMdiMenuBar";
			this.menuStrip_LabMdiMenuBar.Size = new System.Drawing.Size(915, 28);
			this.menuStrip_LabMdiMenuBar.TabIndex = 1;
			this.menuStrip_LabMdiMenuBar.Text = "menuStrip1";
			// 
			// ToolStripMenuItem_File
			// 
			this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Exit});
			this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
			this.ToolStripMenuItem_File.Size = new System.Drawing.Size(66, 24);
			this.ToolStripMenuItem_File.Text = "文件(&F)";
			// 
			// ToolStripMenuItem_Exit
			// 
			this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
			this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(125, 24);
			this.ToolStripMenuItem_Exit.Text = "退出(&X)";
			// 
			// ToolStripMenuItem_Edit
			// 
			this.ToolStripMenuItem_Edit.Name = "ToolStripMenuItem_Edit";
			this.ToolStripMenuItem_Edit.Size = new System.Drawing.Size(67, 24);
			this.ToolStripMenuItem_Edit.Text = "编辑(&E)";
			// 
			// ToolStripMenuItem_Func
			// 
			this.ToolStripMenuItem_Func.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_GenRF});
			this.ToolStripMenuItem_Func.Name = "ToolStripMenuItem_Func";
			this.ToolStripMenuItem_Func.Size = new System.Drawing.Size(66, 24);
			this.ToolStripMenuItem_Func.Text = "功能(&F)";
			// 
			// ToolStripMenuItem_GenRF
			// 
			this.ToolStripMenuItem_GenRF.Name = "ToolStripMenuItem_GenRF";
			this.ToolStripMenuItem_GenRF.Size = new System.Drawing.Size(167, 24);
			this.ToolStripMenuItem_GenRF.Text = "数控信号源(&R)";
			// 
			// ToolStripMenuItem_Tool
			// 
			this.ToolStripMenuItem_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Calc,
            this.ToolStripMenuItem_TXT});
			this.ToolStripMenuItem_Tool.Name = "ToolStripMenuItem_Tool";
			this.ToolStripMenuItem_Tool.Size = new System.Drawing.Size(67, 24);
			this.ToolStripMenuItem_Tool.Text = "工具(&T)";
			// 
			// ToolStripMenuItem_Calc
			// 
			this.ToolStripMenuItem_Calc.Name = "ToolStripMenuItem_Calc";
			this.ToolStripMenuItem_Calc.Size = new System.Drawing.Size(139, 24);
			this.ToolStripMenuItem_Calc.Text = "计算器(&C)";
			// 
			// ToolStripMenuItem_TXT
			// 
			this.ToolStripMenuItem_TXT.Name = "ToolStripMenuItem_TXT";
			this.ToolStripMenuItem_TXT.Size = new System.Drawing.Size(139, 24);
			this.ToolStripMenuItem_TXT.Text = "记事本(&T)";
			// 
			// ToolStripMenuItem_Wind
			// 
			this.ToolStripMenuItem_Wind.Name = "ToolStripMenuItem_Wind";
			this.ToolStripMenuItem_Wind.Size = new System.Drawing.Size(73, 24);
			this.ToolStripMenuItem_Wind.Text = "窗口(&W)";
			// 
			// ToolStripMenuItem_Help
			// 
			this.ToolStripMenuItem_Help.Name = "ToolStripMenuItem_Help";
			this.ToolStripMenuItem_Help.Size = new System.Drawing.Size(70, 24);
			this.ToolStripMenuItem_Help.Text = "帮助(&H)";
			// 
			// LabMdiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(915, 601);
			this.Controls.Add(this.menuStrip_LabMdiMenuBar);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip_LabMdiMenuBar;
			this.Name = "LabMdiForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "主窗体";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabMdiForm_FormClosing);
			this.Load += new System.EventHandler(this.LabMdiForm_Load);
			this.LocationChanged += new System.EventHandler(this.LabMdiForm_LocationChanged);
			this.menuStrip_LabMdiMenuBar.ResumeLayout(false);
			this.menuStrip_LabMdiMenuBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip_LabMdiMenuBar;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Edit;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Func;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Tool;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Wind;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Help;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Calc;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_TXT;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_GenRF;
	}
}

