using COMMPortLib;
using MessageBoxPlusLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BootLoaderLib
{
    public class BootLoader
	{
		#region 变量定义

		/// <summary>
		/// 使用的通信端口
		/// </summary>
		private COMMPort usedPort = null;

		/// <summary>
		/// 使用的窗体
		/// </summary>
		private Form usedForm = null;

		#endregion

		#region 构造函数

		/// <summary>
		/// 
		/// </summary>
		public BootLoader()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="useForm"></param>
		/// <param name="usePort"></param>
		/// <param name="cbb"></param>
		/// <param name="msg"></param>
		public BootLoader(Form useForm, COMMPort usePort, ComboBox cbb = null, RichTextBox msg = null)
		{
			//---窗体
			if (this.usedForm == null)
			{
				this.usedForm = new Form();
			}
			this.usedForm = useForm;

			//---端口
			if (this.usedPort == null)
			{
				this.usedPort = new COMMPort();
			}

			this.usedPort = usePort;
			this.usedPort.Init(cbb, msg);
		}

		#endregion

		#region 函数定义

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int OpenDevice(string name, RichTextBox msg = null)
		{
			if (this.usedPort != null)
			{
				return this.usedPort.OpenDevice(name, msg);
			}
			else
			{
				if (this.usedForm != null)
				{
					MessageBoxPlus.Show(this.usedForm, "通信端口初始化失败!\r\n", "错误提示");
				}
				else
				{
					MessageBox.Show("通信端口初始化失败!\r\n", "错误提示");
				}
			}
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int CloseDevice(string name, RichTextBox msg = null)
		{
			if (this.usedPort != null)
			{
				return this.usedPort.CloseDevice(name, msg);
			}
			else
			{
				if (this.usedForm != null)
				{
					MessageBoxPlus.Show(this.usedForm, "通信端口初始化失败!\r\n", "错误提示");
				}
				else
				{
					MessageBox.Show("通信端口初始化失败!\r\n", "错误提示");
				}
			}
			return 1;
		}

		#endregion

		#region 事件定义
		public virtual void DevicesChangedEvent(ref Message m, ComboBox cbb = null, RichTextBox msg = null)
		{
			if (this.usedPort != null)
			{
				this.usedPort.DevicesChangedEvent(ref m, cbb, msg);
			}
		}
		#endregion

	}
}
