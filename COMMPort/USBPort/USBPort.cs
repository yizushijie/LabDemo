using System;
using System.Windows.Forms;

namespace COMMPortLib
{
	public class USBCOMMPort : COMMPort
	{
		#region 属性定义

		/// <summary>
		///
		/// </summary>
		public override string m_COMMPortName
		{
			get
			{
				return base.m_COMMPortName;
			}
			set
			{
				base.m_COMMPortName = value;
			}
		}

		/// <summary>
		///
		/// </summary>
		public override int m_COMMPortIndex
		{
			get
			{
				return base.m_COMMPortIndex;
			}
			set
			{
				base.m_COMMPortIndex = value;
			}
		}

		/// <summary>
		/// 工作状态，忙---true；空闲---false
		/// </summary>
		public override bool m_COMMPortSTATE
		{
			get
			{
				return base.m_COMMPortSTATE;
			}
			set
			{
				base.m_COMMPortSTATE = value;
			}
		}

		/// <summary>
		/// 当前端口发送数据缓存区的大小
		/// </summary>
		public override int m_COMMPortSize
		{
			get
			{
				return base.m_COMMPortSize;
			}
			set
			{
				base.m_COMMPortSize = value;
			}
		}

		/// <summary>
		/// 接收CRC等级
		/// </summary>
		public override byte m_COMMPortReceCRC
		{
			get
			{
				return base.m_COMMPortReceCRC;
			}
			set
			{
				base.m_COMMPortReceCRC = value;
			}
		}

		/// <summary>
		/// 发送CRC等级
		/// </summary>
		public override byte m_COMMPortSendCRC
		{
			get
			{
				return base.m_COMMPortSendCRC;
			}
			set
			{
				base.m_COMMPortSendCRC = value;
			}
		}

		/// <summary>
		/// 接收数据缓存区
		/// </summary>
		public override COMMPortBytes m_COMMPortReceBytes
		{
			get
			{
				return base.m_COMMPortReceBytes;
			}
			set
			{
				base.m_COMMPortReceBytes = value;
			}
		}

		/// <summary>
		/// 发送数据缓存区
		/// </summary>
		public override COMMPortBytes m_COMMPortSendBytes
		{
			get
			{
				return base.m_COMMPortSendBytes;
			}
			set
			{
				base.m_COMMPortSendBytes = value;
			}
		}

		/// <summary>
		/// 耗时时间
		/// </summary>
		public override TimeSpan m_COMMPortUsedTime
		{
			get
			{
				return base.m_COMMPortUsedTime;
			}
			set
			{
				base.m_COMMPortUsedTime = value;
			}
		}

		/// <summary>
		/// 错误信息
		/// </summary>
		public override string m_COMMPortErrMsg
		{
			get
			{
				return base.m_COMMPortErrMsg;
			}
			set
			{
				base.m_COMMPortErrMsg = value;
			}
		}

		/// <summary>
		/// 使用的窗体
		/// </summary>
		public override Form m_COMMPortForm
		{
			get
			{
				return base.m_COMMPortForm;
			}
			set
			{
				base.m_COMMPortForm = value;
			}
		}

		/// <summary>
		/// 接收报头
		/// </summary>
		public override byte m_COMMPortReceID
		{
			get
			{
				return base.m_COMMPortReceID;
			}
			set
			{
				base.m_COMMPortReceID = value;
			}
		}

		/// <summary>
		/// 发送数据报头
		/// </summary>
		public override byte m_COMMPortSendID
		{
			get
			{
				return base.m_COMMPortSendID;
			}
			set
			{
				base.m_COMMPortSendID = value;
			}
		}

		#endregion 属性定义

		#region 构造函数

		/// <summary>
		///
		/// </summary>
		public USBCOMMPort() : base()
		{
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="usedForm"></param>
		public USBCOMMPort(Form usedForm)
		{
			if (this.m_COMMPortForm == null)
			{
				this.m_COMMPortForm = new Form();
			}

			this.m_COMMPortForm = usedForm;
		}

		#endregion 构造函数
	}
}