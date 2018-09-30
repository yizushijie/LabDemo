using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PreMakeToVSProject
{
	public partial class PreMakeToVSProject : Form
	{
		public PreMakeToVSProject()
		{
			InitializeComponent();
		}

		public PreMakeToVSProject(string path)
		{
			InitializeComponent();
			this.TextBox_SrcPath.Text = path;
		}

		#region 初始化

		/// <summary>
		/// 加载窗体
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreMakeToVSProject_Load(object sender, EventArgs e)
		{
			this.StartUpInit();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		private void StartUpInit()
		{
			this.comboBox_SrcVersion.SelectedIndex = 0;
			this.comboBox_VSVersion.SelectedIndex = 0;

			this.RegistrationEvent();
            //---通过加载文件的不同自适应当前文档
            if (this.TextBox_SrcPath.Text != string.Empty)
            {
                if (this.TextBox_SrcPath.Text.Contains("uvprojx") || this.TextBox_SrcPath.Text.Contains("uvproj"))
                {
                    if (this.comboBox_SrcVersion.Text != "Keil")
                    {
                        this.comboBox_SrcVersion.Text = "keil";
                    }
                }
                else if (this.TextBox_SrcPath.Text.Contains("ewp"))
                {
                    if (this.comboBox_SrcVersion.Text != "IAR")
                    {
                        this.comboBox_SrcVersion.Text = "IAR";
                    }
                }
            }
        }

		/// <summary>
		/// 事件注册
		/// </summary>
		private void RegistrationEvent()
		{
			this.button_OpenSrc.Click += new System.EventHandler(this.button_Click);
			this.button_Go.Click += new System.EventHandler(this.button_Click);
		}

		#endregion

		#region 事件处理

		/// <summary>
		/// 按键点击事件处理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Click(object sender, EventArgs e)
		{
			Button bt = (Button)sender;
			bt.Enabled = false;
			bool _return = true;
			switch (bt.Text)
			{
				case "选择项目":
					this.TextBox_SrcPath.Text = new TaskControl().GetPathAndProjectVersion(this.comboBox_SrcVersion.Text);
					if ((this.TextBox_SrcPath.Text==string.Empty)||(this.TextBox_SrcPath.Text==null))
					{
						_return = false;
					}
					break;
				case "工程转换":
                    _return = new TaskControl().ManageToVSProject(this.TextBox_SrcPath.Text,this.comboBox_SrcVersion.Text);
					if (_return)
					{
						_return = this.UsePreMakeToVsProject();
						if (_return)
						{
							if (this.checkBox_CloseApplication.Checked)
							{
								Application.Exit();
							}
						}
					}
                    break;
				default:
					break;
			}
			bt.Enabled = true;
		}

		#endregion

		#region 函数定义

		/// <summary>
		/// 使用PreMaKe创建VS项目
		/// </summary>
		/// <returns></returns>
		private bool UsePreMakeToVsProject()
		{

			string vsPath = null;
			if (this.comboBox_SrcVersion.Text == "IAR")
			{
                vsPath = Path.GetDirectoryName(this.TextBox_SrcPath.Text);
                //vsPath = Directory.GetParent(Path.GetDirectoryName(this.TextBox_SrcPath.Text)).FullName;
			}
			else if (this.comboBox_SrcVersion.Text == "Keil")
			{
				vsPath = Path.GetDirectoryName(this.TextBox_SrcPath.Text);
				//vsPath = Directory.GetParent(Path.GetDirectoryName(this.TextBox_SrcPath.Text)).FullName;
			}
			else
			{
				return false;
			}

			//---启动进程
			Process proc = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "premake5.exe",
					//Arguments = "--File=\"" + Path.GetDirectoryName(this.TextBox_SrcPath.Text) + "\\premake5.lua\" " + this.comboBox_VSVersion.Text,
					Arguments = "--File=\"" + vsPath + "\\premake5.lua\" " + this.comboBox_VSVersion.Text,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};
			//---启动应用
			proc.Start();
			string makeOut = proc.StandardOutput.ReadToEnd();
			//---创建VS工程
			if (proc.ExitCode == 0)
			{
				MessageBox.Show(makeOut, @"Make output");
				//---创建VS工程
				if (this.comboBox_VSVersion.Text.Contains("vs"))
				{
					if (this.checkBox_OpenVSProject.Checked)
					{
						DialogResult dialogResult = MessageBox.Show(@"Open Project ?", Text, MessageBoxButtons.YesNo);
						if (dialogResult == DialogResult.Yes)
						{
							ProcessStartInfo psi = new ProcessStartInfo(Path.ChangeExtension(this.TextBox_SrcPath.Text, "sln"));
							//ProcessStartInfo psi = new ProcessStartInfo(Path.ChangeExtension(vsPath, "sln"));
							//{
							//	UseShellExecute = true
							//};
							Process.Start(psi);
						}
					}
				}
			}
			else
			{
				MessageBox.Show(makeOut, @"Make output", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			return true;
		}

		#endregion
		


	}
}
