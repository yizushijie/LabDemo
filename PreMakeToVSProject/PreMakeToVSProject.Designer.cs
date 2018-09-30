namespace PreMakeToVSProject
{
	partial class PreMakeToVSProject
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
			this.TextBox_SrcPath = new System.Windows.Forms.TextBox();
			this.button_OpenSrc = new System.Windows.Forms.Button();
			this.label_SrcName = new System.Windows.Forms.Label();
			this.label_SrcVersion = new System.Windows.Forms.Label();
			this.comboBox_SrcVersion = new System.Windows.Forms.ComboBox();
			this.label_VSVersion = new System.Windows.Forms.Label();
			this.comboBox_VSVersion = new System.Windows.Forms.ComboBox();
			this.button_Go = new System.Windows.Forms.Button();
			this.checkBox_CloseApplication = new System.Windows.Forms.CheckBox();
			this.checkBox_OpenVSProject = new System.Windows.Forms.CheckBox();
			this.label_KeilNote = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TextBox_SrcPath
			// 
			this.TextBox_SrcPath.Location = new System.Drawing.Point(79, 12);
			this.TextBox_SrcPath.Name = "TextBox_SrcPath";
			this.TextBox_SrcPath.Size = new System.Drawing.Size(296, 21);
			this.TextBox_SrcPath.TabIndex = 2;
			// 
			// button_OpenSrc
			// 
			this.button_OpenSrc.Location = new System.Drawing.Point(401, 10);
			this.button_OpenSrc.Name = "button_OpenSrc";
			this.button_OpenSrc.Size = new System.Drawing.Size(75, 23);
			this.button_OpenSrc.TabIndex = 3;
			this.button_OpenSrc.Text = "选择项目";
			this.button_OpenSrc.UseVisualStyleBackColor = true;
			// 
			// label_SrcName
			// 
			this.label_SrcName.AutoSize = true;
			this.label_SrcName.Location = new System.Drawing.Point(21, 15);
			this.label_SrcName.Name = "label_SrcName";
			this.label_SrcName.Size = new System.Drawing.Size(53, 12);
			this.label_SrcName.TabIndex = 4;
			this.label_SrcName.Text = "项目名称";
			// 
			// label_SrcVersion
			// 
			this.label_SrcVersion.AutoSize = true;
			this.label_SrcVersion.Location = new System.Drawing.Point(32, 53);
			this.label_SrcVersion.Name = "label_SrcVersion";
			this.label_SrcVersion.Size = new System.Drawing.Size(53, 12);
			this.label_SrcVersion.TabIndex = 5;
			this.label_SrcVersion.Text = "项目版本";
			// 
			// comboBox_SrcVersion
			// 
			this.comboBox_SrcVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_SrcVersion.FormattingEnabled = true;
			this.comboBox_SrcVersion.Items.AddRange(new object[] {
            "Keil",
            "IAR"});
			this.comboBox_SrcVersion.Location = new System.Drawing.Point(23, 78);
			this.comboBox_SrcVersion.Name = "comboBox_SrcVersion";
			this.comboBox_SrcVersion.Size = new System.Drawing.Size(82, 20);
			this.comboBox_SrcVersion.TabIndex = 6;
			// 
			// label_VSVersion
			// 
			this.label_VSVersion.AutoSize = true;
			this.label_VSVersion.Location = new System.Drawing.Point(177, 53);
			this.label_VSVersion.Name = "label_VSVersion";
			this.label_VSVersion.Size = new System.Drawing.Size(41, 12);
			this.label_VSVersion.TabIndex = 7;
			this.label_VSVersion.Text = "VS版本";
			// 
			// comboBox_VSVersion
			// 
			this.comboBox_VSVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_VSVersion.FormattingEnabled = true;
			this.comboBox_VSVersion.Items.AddRange(new object[] {
            "vs2017",
            "vs2015",
            "vs2013",
            "vs2012",
            "vs2010",
            "vs2008",
            "vs2005"});
			this.comboBox_VSVersion.Location = new System.Drawing.Point(160, 78);
			this.comboBox_VSVersion.Name = "comboBox_VSVersion";
			this.comboBox_VSVersion.Size = new System.Drawing.Size(107, 20);
			this.comboBox_VSVersion.TabIndex = 8;
			// 
			// button_Go
			// 
			this.button_Go.Location = new System.Drawing.Point(401, 75);
			this.button_Go.Name = "button_Go";
			this.button_Go.Size = new System.Drawing.Size(75, 23);
			this.button_Go.TabIndex = 9;
			this.button_Go.Text = "工程转换";
			this.button_Go.UseVisualStyleBackColor = true;
			// 
			// checkBox_CloseApplication
			// 
			this.checkBox_CloseApplication.AutoSize = true;
			this.checkBox_CloseApplication.Checked = true;
			this.checkBox_CloseApplication.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_CloseApplication.Location = new System.Drawing.Point(291, 53);
			this.checkBox_CloseApplication.Name = "checkBox_CloseApplication";
			this.checkBox_CloseApplication.Size = new System.Drawing.Size(96, 16);
			this.checkBox_CloseApplication.TabIndex = 10;
			this.checkBox_CloseApplication.Text = "自动关闭应用";
			this.checkBox_CloseApplication.UseVisualStyleBackColor = true;
			// 
			// checkBox_OpenVSProject
			// 
			this.checkBox_OpenVSProject.AutoSize = true;
			this.checkBox_OpenVSProject.Location = new System.Drawing.Point(291, 79);
			this.checkBox_OpenVSProject.Name = "checkBox_OpenVSProject";
			this.checkBox_OpenVSProject.Size = new System.Drawing.Size(84, 16);
			this.checkBox_OpenVSProject.TabIndex = 11;
			this.checkBox_OpenVSProject.Text = "打开VS工程";
			this.checkBox_OpenVSProject.UseVisualStyleBackColor = true;
			// 
			// label_KeilNote
			// 
			this.label_KeilNote.AutoSize = true;
			this.label_KeilNote.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label_KeilNote.ForeColor = System.Drawing.Color.Red;
			this.label_KeilNote.Location = new System.Drawing.Point(21, 116);
			this.label_KeilNote.Name = "label_KeilNote";
			this.label_KeilNote.Size = new System.Drawing.Size(427, 12);
			this.label_KeilNote.TabIndex = 12;
			this.label_KeilNote.Text = "注意：1.将Keil转换成VS工程，必须保证Keil已经打开，否则出错！！！";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(18, 135);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(386, 12);
			this.label1.TabIndex = 13;
			this.label1.Text = "      2.工程路径以及工程的命名中不能有空格，否则出错！！！";
			// 
			// PreMakeToVSProject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(519, 156);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label_KeilNote);
			this.Controls.Add(this.checkBox_OpenVSProject);
			this.Controls.Add(this.checkBox_CloseApplication);
			this.Controls.Add(this.button_Go);
			this.Controls.Add(this.comboBox_VSVersion);
			this.Controls.Add(this.label_VSVersion);
			this.Controls.Add(this.comboBox_SrcVersion);
			this.Controls.Add(this.label_SrcVersion);
			this.Controls.Add(this.label_SrcName);
			this.Controls.Add(this.button_OpenSrc);
			this.Controls.Add(this.TextBox_SrcPath);
			this.Name = "PreMakeToVSProject";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ToVSProject";
			this.Load += new System.EventHandler(this.PreMakeToVSProject_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBox_SrcPath;
		private System.Windows.Forms.Button button_OpenSrc;
		private System.Windows.Forms.Label label_SrcName;
		private System.Windows.Forms.Label label_SrcVersion;
		private System.Windows.Forms.ComboBox comboBox_SrcVersion;
		private System.Windows.Forms.Label label_VSVersion;
		private System.Windows.Forms.ComboBox comboBox_VSVersion;
		private System.Windows.Forms.Button button_Go;
		private System.Windows.Forms.CheckBox checkBox_CloseApplication;
		private System.Windows.Forms.CheckBox checkBox_OpenVSProject;
		private System.Windows.Forms.Label label_KeilNote;
		private System.Windows.Forms.Label label1;
	}
}

