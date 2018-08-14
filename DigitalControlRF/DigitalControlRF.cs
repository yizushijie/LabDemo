﻿using COMMPortLib;
using MessageBoxPlusLib;
using RichTextBoxPlusLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalControlRFLib
{
	public enum HMC472_CMD_MENU : byte
	{
		CMD_HMC472_READ_433MS		= 1,		//---读取433M小信号源的输出功率
		CMD_HMC472_WRITE_433MS		= 2,		//---修改433M小信号源的输出功率
		CMD_HMC472_READ_315M		= 3,		//---读取315M信号源的输出功率
		CMD_HMC472_WRITE_315M		= 4,		//---修改315M信号源的输出功率
		CMD_HMC472_READ_207M		= 5,		//---读取207M信号源的输出功率
		CMD_HMC472_WRITE_207M		= 6,		//---修改207M信号源的输出功率
		CMD_HMC472_READ_433MB		= 7,		//---读取433M大信号源的输出功率
		CMD_HMC472_WRITE_433MB		= 8,		//---修改433M大信号源的输出功率

		CMD_HMC472_READ_RF433MS	    = 9,		//---读取433M小信号源的衰减功率
		CMD_HMC472_WRITE_RF433MS	= 10,		//---修改433M小信号源的衰减功率
		CMD_HMC472_READ_RF315M		= 11,		//---读取315M信号源的衰减功率
		CMD_HMC472_WRITE_RF315M	    = 12,		//---修改315M信号源的衰减功率
		CMD_HMC472_READ_RF207M		= 13,		//---读取207M信号源的衰减功率
		CMD_HMC472_WRITE_RF207M	    = 14,		//---修改207M信号源的衰减功率
		CMD_HMC472_READ_RF433MB	    = 15,		//---读取433M大信号源的衰减功率
		CMD_HMC472_WRITE_RF433MB	= 16,		//---修改433M大信号源的衰减功率


		CMD_HMC472_READ_RFA		    = 17,		//---读取433M小信号源的输出功率
		CMD_HMC472_WRITE_RFA		= 18,		//---修改433M小信号源的输出功率
		CMD_HMC472_READ_RFB		    = 19,		//---读取315M信号源的输出功率
		CMD_HMC472_WRITE_RFB		= 20,		//---修改315M信号源的输出功率
		CMD_HMC472_READ_RFC		    = 21,		//---读取207M信号源的输出功率
		CMD_HMC472_WRITE_RFC		= 22,		//---修改207M信号源的输出功率
		CMD_HMC472_READ_RFD		    = 23,		//---读取433M大信号源的输出功率
		CMD_HMC472_WRITE_RFD		= 24,		//---修改433M大信号源的输出功率

		CMD_HMC472_READ_RFE		    = 25,		//---读取433M小信号源的衰减功率
		CMD_HMC472_WRITE_RFE		= 26,		//---修改433M小信号源的衰减功率
		CMD_HMC472_READ_RFF		    = 27,		//---读取315M信号源的衰减功率
		CMD_HMC472_WRITE_RFF		= 28,		//---修改315M信号源的衰减功率
		CMD_HMC472_READ_RFG		    = 29,		//---读取207M信号源的衰减功率
		CMD_HMC472_WRITE_RFG		= 30,		//---修改207M信号源的衰减功率
		CMD_HMC472_READ_RFH		    = 31,		//---读取433M大信号源的衰减功率
		CMD_HMC472_WRITE_RFH		= 32,       //---修改433M大信号源的衰减功率

		CMD_HMC472_POWER_RF         = 33, //---HMC472的通道功率的输出值

	};

	public enum GEN_SIGNAL : byte
	{
		GEN_SIGNAL_NONE  = 0,
		GEN_SIGNAL_433MS = 1,
		GEN_SIGNAL_315M  = 2,
		GEN_SIGNAL_207M  = 3,
		GEN_SIGNAL_433MB = 4,
	}

	public enum RF_CHANNEL : byte
	{
		RF_NONE = 0,
		RF_A    = 1,
		RF_B    = 2,
		RF_C    = 3,
		RF_D    = 4,
		RF_E    = 5,
		RF_F    = 6,
		RF_G    = 7,
		RF_H    = 8,
	}

	public class DigitalControlRF
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

		/// <summary>
		/// HMC472通信使用的主命令
		/// </summary>
		private const byte CMD_HMC472 = 0xA0;

		#endregion

		#region 属性定义

		/// <summary>
		/// 
		/// </summary>
		public COMMPort m_UsedPort
		{
			get
			{
				return this.usedPort;

			}
		}

		#endregion

		#region 构造函数

		/// <summary>
		/// 
		/// </summary>
		public DigitalControlRF()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="usedPort"></param>
		public DigitalControlRF(COMMPort usePort)
		{
			if (this.usedPort==null)
			{
				this.usedPort = new COMMPort();
			}

			this.usedPort = usePort;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="usedPort"></param>
		/// <param name="cbb"></param>
		/// <param name="msg"></param>
		public DigitalControlRF(COMMPort usePort, ComboBox cbb = null, RichTextBox msg = null)
		{
			if (this.usedPort == null)
			{
				this.usedPort = new COMMPort();
			}

			this.usedPort = usePort;
			this.usedPort.Init(cbb, msg);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="useForm"></param>
		/// <param name="usePort"></param>
		/// <param name="cbb"></param>
		/// <param name="msg"></param>
		public DigitalControlRF(Form useForm,COMMPort usePort, ComboBox cbb = null, RichTextBox msg = null)
		{
			if (this.usedForm==null)
			{
				this.usedForm = new Form();
			}

			this.usedForm = useForm;
			if (this.usedPort == null)
			{
				this.usedPort = new COMMPort();
			}

			this.usedPort = usePort;
			this.usedPort.Init(cbb, msg);
		}

		#endregion

		#region 函数定义

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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFPower433MS(NumericUpDown nud,RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort!=null)&&(this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] {0x55, 0x02, CMD_HMC472,(byte)HMC472_CMD_MENU.CMD_HMC472_READ_433MS };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return))&&(res!=null)&&(res[0]==0x5A)&&(res[1]==(byte)(res.Length-2))&&(res[2+this.usedPort.m_DeviceIDIndex]== CMD_HMC472)&&(res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_433MS) &&(res[4 + this.usedPort.m_DeviceIDIndex] ==0x00))
				{
					int dBm = res[5+this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal =dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源初始功率，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源初始功率，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFPower433MS(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x04, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_433MS,0x00,0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int) dBmVal;
				cmd[4] = (byte) (dBm >> 8);
				cmd[5] = (byte) (dBm);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_433MS) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源初始功率，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源初始功率，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFPower315M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_315M };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_315M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源初始功率，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源初始功率，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFPower315M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x04, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_315M, 0x00, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm >> 8);
				cmd[5] = (byte)(dBm);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_315M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源初始功率，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源初始功率，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFPower207M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_207M };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_207M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源初始功率，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源初始功率，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFPower207M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x04, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_207M, 0x00, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm >> 8);
				cmd[5] = (byte)(dBm);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_207M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源初始功率，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源初始功率，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFPower433MB(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_433MB };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_433MB) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源初始功率，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源初始功率，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFPower433MB(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x04, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_433MB, 0x00, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm >> 8);
				cmd[5] = (byte)(dBm);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_433MB) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源初始功率，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源初始功率，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRF433MS(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF433MS };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF433MS) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRF433MS(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF433MS, 0x00};
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm/5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF433MS) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M小信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRF315M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF315M };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF315M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRF315M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF315M, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF315M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "315M信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRF207M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF207M };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF207M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRF207M(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF207M, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF207M) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "207M信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRF433MB(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF433MB };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RF433MB) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRF433MB(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF433MB, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RF433MB) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "433M大信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFA(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFA };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFA) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFA信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFA信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFA(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFA, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFA) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFA信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFA信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFB(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFB };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFB) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFB信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFB信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFB(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFB, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFB) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFB信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFB信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFC(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFC };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFC) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFC信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFC信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFC(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFC, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFC) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFC信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFC信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFD(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFD };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFD) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFD信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFD信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFD(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFD, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFD) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFD信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFD信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFE(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFE };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFE) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFE信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFE信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFE(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFE, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFE) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFE信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFE信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFF(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFF };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFF) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFF信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFF信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFF(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFF, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFF) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFF信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFF信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFG(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFG };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFG) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFG信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFG信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFG(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFG, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFG) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFG信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFG信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int ReadRFH(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x02, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFH };
				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_READ_RFH) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFH信号源功率衰减值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFH信号源功率衰减值，读取失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public virtual int WriteRFH(NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x03, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFH, 0x00 };
				decimal dBmVal = nud.Value;
				dBmVal = dBmVal * (decimal)(-10.0);
				int dBm = (int)dBmVal;
				cmd[4] = (byte)(dBm / 5);
				byte[] res = null;
				//---数据传输
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_WRITE_RFH) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFH信号源功率衰减值，写入成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "通道RFH信号源功率衰减值，写入失败!\r\n", Color.Red, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="genSignal"></param>
		/// <param name="rfChannel"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public int ReadPower(ComboBox genSignal, ComboBox rfChannel, NumericUpDown nud, RichTextBox msg)
		{
			int _return = 1;
			if ((this.usedPort != null) && (this.usedPort.IsAttached()))
			{
				byte[] cmd = new byte[] { 0x55, 0x04, CMD_HMC472, (byte)HMC472_CMD_MENU.CMD_HMC472_POWER_RF, (byte)GEN_SIGNAL.GEN_SIGNAL_NONE,(byte)RF_CHANNEL.RF_NONE };
				switch (genSignal.Text)
				{
					case "433M小信号源":
						cmd[4] = (byte)GEN_SIGNAL.GEN_SIGNAL_433MS;
						break;
					case "315M信号源":
						cmd[4] = (byte)GEN_SIGNAL.GEN_SIGNAL_315M;
						break;
					case "207M信号源":
						cmd[4] = (byte)GEN_SIGNAL.GEN_SIGNAL_207M;
						break;
					case "433M大信号源":
						cmd[4] = (byte)GEN_SIGNAL.GEN_SIGNAL_433MB;
						break;
					default:
						cmd[4] = (byte) GEN_SIGNAL.GEN_SIGNAL_NONE;
						break;
				}
				switch (rfChannel.Text)
				{
					case "RF通道A":
						cmd[5] = (byte)RF_CHANNEL.RF_A;
						break;
					case "RF通道B":
						cmd[5] = (byte)RF_CHANNEL.RF_B;
						break;
					case "RF通道C":
						cmd[5] = (byte)RF_CHANNEL.RF_C;
						break;
					case "RF通道D":
						cmd[5] = (byte)RF_CHANNEL.RF_D;
						break;
					case "RF通道E":
						cmd[5] = (byte)RF_CHANNEL.RF_E;
						break;
					case "RF通道F":
						cmd[5] = (byte)RF_CHANNEL.RF_F;
						break;
					case "RF通道G":
						cmd[5] = (byte)RF_CHANNEL.RF_G;
						break;
					case "RF通道H":
						cmd[5] = (byte)RF_CHANNEL.RF_H;
						break;
					default:
						cmd[5] = (byte)RF_CHANNEL.RF_NONE;
						break;
				}

				byte[] res = null;
				_return = this.usedPort.SendCmdAndReadResponse(cmd, ref res, 200);
				if ((this.usedPort.IsReadValidData(_return)) && (res != null) && (res[0] == 0x5A) && (res[1] == (byte)(res.Length - 2)) && (res[2 + this.usedPort.m_DeviceIDIndex] == CMD_HMC472) && (res[3 + this.usedPort.m_DeviceIDIndex] == (byte)HMC472_CMD_MENU.CMD_HMC472_POWER_RF) && (res[4 + this.usedPort.m_DeviceIDIndex] == 0x00))
				{
					int dBm = res[5 + this.usedPort.m_DeviceIDIndex];
					dBm = (dBm << 8) + res[6 + this.usedPort.m_DeviceIDIndex];
					decimal dBmVal = dBm;
					dBmVal = dBmVal * (decimal)0.10;
					nud.Value = -dBmVal;
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "信号源输出功率值，读取成功!\r\n", Color.Black, false);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, "信号源输出功率值，读取失败!\r\n", Color.Red, false);
				}

			}
			return _return;
		}

		#endregion

		#region 事件函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="m"></param>
		/// <param name="cbb"></param>
		/// <param name="msg"></param>
		public virtual void DevicesChangedEvent(ref Message m, ComboBox cbb = null, RichTextBox msg = null)
		{
			if (this.usedPort!=null)
			{
				this.usedPort.DevicesChangedEvent(ref m, cbb, msg);
			}
		}


		#endregion


	}
}
