using DigitalControlRFForm;
using MessageBoxPlusLib;
using ProcessApplicationLib;
using System;
using System.Windows.Forms;

namespace LabDemoForm
{
	public partial class LabMdiForm : Form
	{
		#region 构造函数

		/// <summary>
		/// 构造函数
		/// </summary>
		public LabMdiForm()
		{
			InitializeComponent();
		}

		#endregion 构造函数

		#region 析构函数

		/// <summary>
		/// 析构函数
		/// </summary>
		~LabMdiForm()
		{
			//---退出当前窗体
			this.Dispose();
		}

		#endregion 析构函数

		#region 窗体初始化函数

		/// <summary>
		/// 窗体加载函数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LabMdiForm_Load(object sender, EventArgs e)
		{
			this.LabMdiForm_Init();
		}

		/// <summary>
		/// 窗体关闭事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LabMdiForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//---判断是Mdi窗体
			if (e.CloseReason == CloseReason.MdiFormClosing)
			{
				if (DialogResult.OK == MessageBoxPlus.Show(this, "你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
				{
					//----为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
					this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.LabMdiForm_FormClosing);
					//---确认关闭事件
					e.Cancel = false;
					//---退出当前窗体
					this.Dispose();
				}
				else
				{
					//---取消关闭事件
					e.Cancel = true;
				}
			}
			//---判断是用户关闭
			else if (e.CloseReason == CloseReason.UserClosing)
			{
				if (DialogResult.OK == MessageBoxPlus.Show(this, "你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
				{
					//----为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
					//this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.LabMdiForm_FormClosing);
					//---确认关闭事件
					e.Cancel = false;
					//---退出当前窗体
					this.Dispose();
				}
				else
				{
					//---取消关闭事件
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 窗体位置发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LabMdiForm_LocationChanged(object sender, EventArgs e)
		{
			this.StartPosition = FormStartPosition.CenterScreen;
		}

		#endregion 窗体初始化函数

		#region 初始化函数

		/// <summary>
		/// 初始化函数
		/// </summary>
		private void LabMdiForm_Init()
		{
			this.RegistrationEvent();
		}

		#endregion 初始化函数

		#region 事件函数

		/// <summary>
		/// 注册事件
		/// </summary>
		private void RegistrationEvent()
		{
			this.ToolStripMenuItem_Exit.Click += new EventHandler(this.ToolStripMenuItem_Click);
			this.ToolStripMenuItem_Calc.Click += new EventHandler(this.ToolStripMenuItem_Click);
			this.ToolStripMenuItem_TXT.Click += new EventHandler(this.ToolStripMenuItem_Click);
			this.ToolStripMenuItem_GenRF.Click += new EventHandler(this.ToolStripMenuItem_Click);
		}

		/// <summary>
		/// 卸载事件
		/// </summary>
		private void UnRegistrationEvent()
		{
		}

		/// <summary>
		/// 事件函数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
			tsm.Enabled = false;
			switch (tsm.Name)
			{
				//---退出操作
				case "ToolStripMenuItem_Exit":
					//---消息显示
					if (DialogResult.OK == MessageBoxPlus.Show(this, "你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
					{
						//---退出当前窗体
						this.Dispose();
					}
					break;
				//---调用计算器
				case "ToolStripMenuItem_Calc":
					ProcessApplication.ActivateRunApplication("calc.exe", "Calculator");
					break;
				//---调用文本编辑器
				case "ToolStripMenuItem_TXT":
					if (ProcessApplication.ActivateRunApplication("notepad++.exe", "Notepad++") == false)
					{
						ProcessApplication.ActivateRunApplication("notepad.exe", "Notepad");
					}
					break;
				//---调用数字信号源船体
				case "ToolStripMenuItem_GenRF":
					this.DigitalControlRFMenuForm_Init();
					break;

				default:
					break;
			}
			tsm.Enabled = true;
		}

		#endregion 事件函数

		#region 函数定义

		/// <summary>
		/// 数控信号源窗体的初始化
		/// </summary>
		private void DigitalControlRFMenuForm_Init()
		{
			//---遍历船体索引
			int i = 0;
			//---新窗体
			Form newForm = null;
			//---遍历窗体
			for (i = 0; i < this.MdiChildren.Length; i++)
			{
				if (this.MdiChildren[i] is DigitalControlRFMenuForm)
				{
					if (this.MdiChildren[i].WindowState == FormWindowState.Minimized)
					{
						this.MdiChildren[i].WindowState = FormWindowState.Normal;
					}
					this.MdiChildren[i].Activate();
					return;
				}
			}
			newForm = new DigitalControlRFMenuForm();
			//---判断窗体高度是否合适
			if (this.Height <= newForm.Height)
			{
				this.Height = newForm.Height + 100;
			}
			//---判断窗体宽度是否合适
			if (this.Width <= newForm.Width)
			{
				this.Width = newForm.Width + 100;
			}
			//---设置父窗体的最小值
			this.MinimumSize = this.Size;
			//---设置MDI的父窗体
			newForm.MdiParent = this;
			//---船体居中显示
			newForm.StartPosition = FormStartPosition.CenterScreen;
			//---显示船体
			newForm.Show();
			//---捕捉焦点
			//newForm.Focus();
		}

		#endregion 函数定义
	}
}