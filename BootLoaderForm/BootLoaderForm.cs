using BootLoaderLib;
using COMMPortLib;
using MessageBoxPlusLib;
using RichTextBoxPlusLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BootLoaderForm
{
	public partial class BootLoaderForm : Form
	{

		#region 变量定义

		//===使用的类函数
		private BootLoader usedBootLoader = null;

		//---文件打开---固件文件
		private OpenFileDialog openFirmwareFile = new OpenFileDialog();
		
		#endregion

		#region 系统消息处理

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (this.usedBootLoader != null)
			{
				this.usedBootLoader.DevicesChangedEvent(ref m, this.comboBox_portName, this.richTextBox_msg);

				//---始终让文本框获取焦点 
				//this.richTextBox_msg.Focus();
				this.Focus();
			}
		}

		#endregion

		#region 启动初始化
		public BootLoaderForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体加载事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BootLoaderForm_Load(object sender, EventArgs e)
		{
			this.BootLoaderForm_Init();
		}

		/// <summary>
		/// 船体关闭事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BootLoaderForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.MdiFormClosing)
			{
				return;
			}
			else if (e.CloseReason == CloseReason.UserClosing)
			{
				if (DialogResult.OK == MessageBoxPlus.Show(this, "你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
				{
					//---为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
					this.FormClosing -= new FormClosingEventHandler(this.BootLoaderForm_FormClosing);
					//---确认关闭事件
					e.Cancel = false;
					//退出当前应用---如果是MDI窗体,会导致整个应用退出
					//Application.Exit();
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
		/// 保持窗体始终位于中间
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BootLoaderForm_LocationChanged(object sender, EventArgs e)
		{
			//this.StartPosition = FormStartPosition.CenterScreen;
		}

		#endregion

		#region 初始化

		/// <summary>
		/// 初始化
		/// </summary>
		private void BootLoaderForm_Init()
		{

			this.panel_port.Enabled = false;

			//---限定窗体的最小值
			this.MinimumSize = this.Size;
			//---限定窗体的最大值
			this.MaximumSize = this.Size;
			//---初始化类
			if (this.usedBootLoader == null)
			{
				this.usedBootLoader = new BootLoader(this, new SerialCOMMPort(this), this.comboBox_portName, this.richTextBox_msg);
			}

			//---事件注册
			this.RegistrationEvent();

			this.button_closePort.Enabled = false;
			this.panel_update.Enabled = false;

			this.panel_port.Enabled = true;

		}


		/// <summary>
		/// 事件注册
		/// </summary>
		private void RegistrationEvent()
		{
			//---加载contextMenuTrip的子项---消息显示的清除按钮
			ToolStripItem tsItem;
			tsItem = this.AddContextMenu("清除", this.contextMenuStrip_richTextBox_msg.Items, new EventHandler(this.button_Click));

			//---注册时间控件
			this.timer_SysTick.Tick += new System.EventHandler(this.timer_Tick);

			//---注册按钮事件
			this.button_openFirmware.Click   += new EventHandler(this.button_Click);
			this.button_updateFirmware.Click += new EventHandler(this.button_Click);
			
			this.button_openPort.Click  += new EventHandler(this.button_Click);
			this.button_closePort.Click += new EventHandler(this.button_Click);

		}

		/// <summary>
		/// 
		/// </summary>
		private void UnRegistrationEvent()
		{
		}

		#endregion

		#region 控件事件

		//========================================================
		#region 按钮事件

		/// <summary>
		/// 按钮点击事件处理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Click(object sender, EventArgs e)
		{
			string str = sender.ToString();
			if (str=="清除")
			{
				this.richTextBox_msg.Clear();
			}
			else
			{
				if (this.usedBootLoader==null)
				{
					MessageBoxPlus.Show(this, "应用发生配置错误，请重新启动！", "错误提示!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				Button btn = (Button)sender;
				btn.Enabled = false;
				DialogResult dgTemp;
				int _return = 0;
				//---检查那个按钮使用
				switch (btn.Name)
				{
					case "button_openPort":
						_return = this.usedBootLoader.OpenDevice(this.comboBox_portName.Text, this.richTextBox_msg);
						if (_return==0)
						{
							this.button_closePort.Enabled = true;
							this.panel_update.Enabled = true;
							this.comboBox_portName.Enabled = false;
							return;
						}
						break;
					case "button_closePort":
						_return = this.usedBootLoader.CloseDevice(this.comboBox_portName.Text, this.richTextBox_msg);
						if (_return == 0)
						{
							this.button_openPort.Enabled = true;
							this.panel_update.Enabled = false;
							this.comboBox_portName.Enabled = true;
							return;
						}
						break;
					case "button_openFirmware":
						//---重新初始化打开文件
						openFirmwareFile = new OpenFileDialog();
						//---过滤文件
						openFirmwareFile.AddExtension = true;
						openFirmwareFile.DefaultExt = "hex";
						openFirmwareFile.Filter = " hex files(*.hex)|*.hex";
						openFirmwareFile.FilterIndex = 0;

						//---获取文件打开的状态
						dgTemp = openFirmwareFile.ShowDialog();

						//---判断是否选择固件
						if (dgTemp == DialogResult.OK)
						{
							this.textBox_firmware.Text = openFirmwareFile.FileName;
							RichTextBoxPlus.AppendTextInfoTopWithDataTime(this.richTextBox_msg, "固件打开成功! \r\n", Color.Black, false);
						}
						else if (dgTemp == DialogResult.Cancel)
						{
							this.textBox_firmware.Text = string.Empty;
							RichTextBoxPlus.AppendTextInfoTopWithDataTime(this.richTextBox_msg, "取消固件打开! \r\n", Color.Black, false);
						}
						else
						{
							this.textBox_firmware.Text = string.Empty;
							RichTextBoxPlus.AppendTextInfoTopWithDataTime(this.richTextBox_msg, "固件打开失败！\r\n", Color.Red, false);
						}
						break;
					case "button_updateFirmware":
						if ((this.textBox_firmware.Text==null)||(this.textBox_firmware.Text==string.Empty)||(this.openFirmwareFile.SafeFileNames.Length==0))
						{
							//---弹出警告框
							MessageBoxPlus.Show(this, "固件文件错误，请重新选择！", "错误提示!", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							//---弹出警告框
							if(DialogResult.OK ==MessageBoxPlus.Show(this, "？？？是否确定更新固件!", "更新提示!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
							{
								RichTextBoxPlus.AppendTextInfoTopWithDataTime(this.richTextBox_msg, "取消更新固件！\r\n", Color.Red, false);
							}
							else
							{
								RichTextBoxPlus.AppendTextInfoTopWithDataTime(this.richTextBox_msg, "准备更新固件！\r\n", Color.Red, false);
							}
						}
						break;
					default:
						break;
				}
				btn.Enabled = true;
			}
		}




		#endregion

		//========================================================

		#region 定时器事件
		private void timer_Tick(object sender, EventArgs e)
		{
			this.toolStripLabel_SysTick.Text = System.DateTime.Now.ToString();
		}
		#endregion

		#endregion

		#region 函数定义

		/// <summary>  
		/// 添加子菜单  
		/// </summary>
		/// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
		/// <param name="cms">要添加到的子菜单集合</param>
		/// <param name="callback">点击时触发的事件</param>
		/// <returns>生成的子菜单，如果为分隔条则返回null</returns>
		private ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
		{
			if (text == "-")
			{
				ToolStripSeparator tsp = new ToolStripSeparator();
				cms.Add(tsp);
				return null;
			}
			else if (!string.IsNullOrEmpty(text))
			{
				ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
				if (callback != null)
				{
					tsmi.Click += callback;
				}
				cms.Add(tsmi);
				return tsmi;
			}
			return null;
		}

		#endregion

	}
}
