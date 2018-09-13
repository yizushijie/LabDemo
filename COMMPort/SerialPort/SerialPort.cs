
using GenFuncLib;
using MessageBoxPlusLib;
using RichTextBoxPlusLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace COMMPortLib
{
	public class SerialCOMMPort : COMMPort
	{
		#region 变量定义

		/// <summary>
		/// 通信波特率
		/// </summary>
		private int baudRate = 100000;

		/// <summary>
		/// 数据位
		/// </summary>
		private int dataBits = 8;

		/// <summary>
		/// 奇偶校验
		/// </summary>
		private string parity = "NONE";

		/// <summary>
		/// 停止位
		/// </summary>
		private StopBits stopBits = StopBits.One;

		

		#endregion 变量定义

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
		/// 
		/// </summary>
		public override bool m_COMMEnableDataReceivedEvent
		{
			get
			{
				return base.m_COMMEnableDataReceivedEvent;
			}
			set
			{
				base.m_COMMEnableDataReceivedEvent = value;
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

		/// <summary>
		///
		/// </summary>
		public override COMMDevice[] m_COMMPortDevices
		{
			get
			{
				return base.m_COMMPortDevices;
			}
			set
			{
				base.m_COMMPortDevices = value;
			}
		}

		/// <summary>
		///
		/// </summary>
		public override int m_DeviceID
		{
			get
			{
				return base.m_DeviceID;
			}
			set
			{
				base.m_DeviceID = value;
			}
		}

		/// <summary>
		///
		/// </summary>
		public override int m_DeviceIDIndex
		{
			get
			{
				return base.m_DeviceIDIndex;
			}

			set
			{
				base.m_DeviceIDIndex = value;
			}
		}

        /// <summary>
        /// 还用的串口
        /// </summary>
        public override SerialPort m_COMMSerialPort
        {
            get
            {
                return base.m_COMMSerialPort;
            }
            set
            {
                base.m_COMMSerialPort = value;
            }
        }

        #endregion 属性定义

        #region 构造函数

        /// <summary>
        ///
        /// </summary>
        public SerialCOMMPort() : base()
		{
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="usedForm"></param>
		public SerialCOMMPort(Form usedForm)
		{
			if (this.m_COMMPortForm == null)
			{
				this.m_COMMPortForm = new Form();
			}

			this.m_COMMPortForm = usedForm;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="usedForm"></param>
		/// <param name="deviceID"></param>
		public SerialCOMMPort(Form usedForm, int deviceID)
		{
			if (this.m_COMMPortForm == null)
			{
				this.m_COMMPortForm = new Form();
			}

			this.m_COMMPortForm = usedForm;
			//---设置多设备通信的设备ID地址
			this.m_DeviceID = deviceID;

            //---命令索引修改
			if (this.m_DeviceID > 0)
			{
				this.m_DeviceIDIndex = 1;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="usedForm"></param>
		public SerialCOMMPort(Form usedForm, int baudRate, int size)
		{
			if (this.m_COMMPortForm == null)
			{
				this.m_COMMPortForm = new Form();
			}
			this.m_COMMPortForm = usedForm;

            //---通信波特率
            if (this.baudRate!=baudRate)
            {
                this.baudRate = baudRate;
            }

            //---缓存区的大小
            if (this.m_COMMPortSize != size)
            {
                this.m_COMMPortSize = size;
            }
		}

		/// <summary>
		/// 析构函数
		/// </summary>
		~SerialCOMMPort()
		{
			this.m_COMMSerialPort = null;
		}

		#endregion 构造函数

		#region 函数定义

		/// <summary>
		/// 获取校验位信息
		/// </summary>
		/// <param name="parity"></param>
		/// <returns></returns>
		private Parity SerialCOMMPortGetParityBits(string parityBit)
		{
			//---获取校验位
			Parity _return = new Parity();
			//---奇校验
			if (parityBit.StartsWith("奇") || parityBit.StartsWith("Odd") || parityBit.StartsWith("ODD") || parityBit.StartsWith("odd") || parityBit.StartsWith("oDD"))
			{
				_return = Parity.Odd;
			}
			//---偶校验
			else if (parityBit.StartsWith("偶") || parityBit.StartsWith("Even") || parityBit.StartsWith("EVEN") || parityBit.StartsWith("even") || parityBit.StartsWith("eVEN"))
			{
				_return = Parity.Even;
			}
			//---无校验位
			else
			{
				_return = Parity.None;
			}
			return _return;
		}

		/// <summary>
		/// 获取当前计算器存在的通信设备
		/// </summary>
		/// <returns></returns>
		private string[] SerialCOMMPortGetDeviceNames()
		{
			return SerialPort.GetPortNames();
		}

		/// <summary>
		/// 更新当前任务的设备信息
		/// </summary>
		/// <returns></returns>
		private COMMDevice[] SerialCOMMPortGetDevices()
		{
			COMMDevice[] _return;
			string[] deviceNames = this.SerialCOMMPortGetDeviceNames();
			//---判断当前设备是否存在通信设备
			if (deviceNames == null)
			{
				return null;
			}

			//===获取端口的序号
			int[] deviceIndexs = new int[deviceNames.Length];
			int index = 0;
			int errDeviceNameCount = 0;
			//---遍历端口信息
			for (index = 0; index < deviceNames.Length; index++)
			{
				//---判断端口名称是否合法
				if (deviceNames[index].StartsWith("COM") || deviceNames[index].StartsWith("com"))
				{
					//---去除字符串中非数字字符
					string str = Regex.Replace(deviceNames[index], @"[^\d]*", "");
					//---将字符串转换成数字
					deviceIndexs[index - errDeviceNameCount] = (int.Parse(str));
				}
				else
				{
					errDeviceNameCount++;
				}
			}
			//---重置缓存区的大小
			Array.Resize<int>(ref deviceIndexs, (index - errDeviceNameCount));
			//---数组从小到大排序
			int[] deviceIndexsTemp = deviceIndexs.OrderBy(x => x).ToArray();
			//---从大到小排序数组
			//int[] deviceIndexsTemp = deviceIndexs.OrderByDescending(x => x).ToArray();
			if ((deviceIndexsTemp == null) || (deviceIndexsTemp.Length == 0))
			{
				return null;
			}
			//---保存有效端口的信息
			deviceNames = new string[deviceIndexsTemp.Length];
			_return = new COMMDevice[deviceIndexsTemp.Length];
			//---更新端口信息
			for (index = 0; index < deviceIndexsTemp.Length; index++)
			{
				_return[index].name = "COM" + Convert.ToString(deviceIndexsTemp[index]);
				_return[index].index = deviceIndexsTemp[index];
				_return[index].state = true;
			}
			return _return;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="commDevice"></param>
		/// <returns></returns>
		private string[] SerialCOMMPortUpDateDeviceNames(COMMDevice[] commDevice)
		{
			if ((commDevice == null) || (commDevice.Length == 0))
			{
				return null;
			}
			string[] _return = new string[commDevice.Length];
			//---获取端口名称
			for (int i = 0; i < commDevice.Length; i++)
			{
				_return[i] = commDevice[i].name;
			}

			return _return;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="commDevice"></param>
		/// <returns></returns>
		private int[] SerialCOMMPortUpDateDeviceIndexs(COMMDevice[] commDevice)
		{
			if ((commDevice != null) || (commDevice.Length == 0))
			{
				return null;
			}
			int[] _return = new int[commDevice.Length];
			//---获取端口名称
			for (int i = 0; i < commDevice.Length; i++)
			{
				_return[i] = commDevice[i].index;
			}

			return _return;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cbb"></param>
		/// <returns></returns>
		private bool SerialCOMMPortUpdateDevices(ComboBox cbb = null, RichTextBox rtbMsg = null)
		{
			bool _return = true;

			COMMDevice[] deviceNames = this.SerialCOMMPortGetDevices();
			//---判断设备是否存在
			if ((deviceNames == null) || (deviceNames.Length == 0))
			{
				if (cbb.Items.Count != 0)
				{
					this.m_COMMPortErrMsg = string.Format("{0} {1}", System.DateTime.Now.ToString(), "：") +
											" 端口：" + cbb.Text + "移除\r\n";
					//---清理当前设备的集合列表
					cbb.Items.Clear();
					cbb.SelectedIndex = -1;
					cbb.Text = string.Empty;
				}
				_return = false;
			}
			else
			{
				this.m_COMMPortErrMsg = string.Empty;
				int i = 0;
				//---获取设备的名称
				string[] newDeviceNames = this.SerialCOMMPortUpDateDeviceNames(deviceNames);
				string[] oddDeviceNames = this.SerialCOMMPortUpDateDeviceNames(m_COMMPortDevices);
				//---判断保存的设备数据
				if ((this.m_COMMPortDevices == null) || (this.m_COMMPortDevices.Length == 0))
				{
					this.m_COMMPortDevices = new COMMDevice[deviceNames.Length];
					//---数据拷贝到缓存区
					Array.Copy(deviceNames, this.m_COMMPortDevices, deviceNames.Length);
					//---端口拷贝到控件
					if (cbb != null)
					{
						//---清理当前设备的集合列表
						cbb.Items.Clear();
						//---重新添加设备集合列表
						cbb.Items.AddRange(this.SerialCOMMPortUpDateDeviceNames(deviceNames));
						//---选择默认设备序号
						cbb.SelectedIndex = 0;
						//---设备处于不工作状态
						this.m_COMMPortSTATE = false;
					}
					for (i = 0; i < this.m_COMMPortDevices.Length; i++)
					{
						this.m_COMMPortErrMsg += string.Format("{0} {1}", System.DateTime.Now.ToString(), "：") + " 端口：" + this.m_COMMPortDevices[i].name + "插入\r\n";
					}
				}
				else
				{
					//---判断两个字符串是否相等
					if (Enumerable.SequenceEqual(this.SerialCOMMPortUpDateDeviceNames(this.m_COMMPortDevices),
						this.SerialCOMMPortUpDateDeviceNames(deviceNames)))
					{
						if (cbb != null)
						{
							//---判断当前设备列表是否显示为空
							if ((cbb.Text != string.Empty) && (cbb.Text != null) && (cbb.Text != ""))
							{
								//cbb.SelectedIndex = 0;
								//---获取当前设备在设备集合中位置
								int index = Array.IndexOf(oddDeviceNames, cbb.Text);
								if (index >= 0)
								{
									cbb.SelectedIndex = index;
								}
								else
								{
									cbb.SelectedIndex = 0;
									//---设备处于不工作状态
									this.m_COMMPortSTATE = false;
								}
							}
						}
					}
					else
					{
						//---设备集合发生变化
						if (oddDeviceNames.Length == newDeviceNames.Length)
						{
							//---设备集合的大小相等
							for (i = 0; i < oddDeviceNames.Length; i++)
							{
								//---判断是否有设备插入
								if (!oddDeviceNames.Contains(newDeviceNames[i]))
								{
									this.m_COMMPortErrMsg +=
										string.Format("{0} {1}", System.DateTime.Now.ToString(), "：") + " 端口：" +
										newDeviceNames[i] + "插入\r\n";
								}

								//---判断是否有设备移除
								if (!newDeviceNames.Contains(oddDeviceNames[i]))
								{
									this.m_COMMPortErrMsg +=
										string.Format("{0} {1}", System.DateTime.Now.ToString(), "：") + " 端口：" +
										oddDeviceNames[i] + "移除\r\n";
								}
							}
						}
						else if (oddDeviceNames.Length > newDeviceNames.Length)
						{
							//---历史设备集合的个数大于当前设备的个数;则有设备移除，需要更新设备集合
							for (i = 0; i < oddDeviceNames.Length; i++)
							{
								//---判断是否有设备移除
								if (!newDeviceNames.Contains(oddDeviceNames[i]))
								{
									this.m_COMMPortErrMsg +=
										string.Format("{0} {1}", System.DateTime.Now.ToString(), "：") + " 端口：" +
										oddDeviceNames[i] + "移除\r\n";
								}
							}

							//---重置缓存区的大小
							Array.Resize<string>(ref oddDeviceNames, newDeviceNames.Length);
						}
						else
						{
							//---历史设备集合的个数小于当前设备的个数;则有设备插入，需要更新设备集合
							for (i = 0; i < newDeviceNames.Length; i++)
							{
								//---判断是否有设备移除
								if (!oddDeviceNames.Contains(newDeviceNames[i]))
								{
									this.m_COMMPortErrMsg +=
										string.Format("{0} {1}", System.DateTime.Now.ToString(), "：") + " 端口：" +
										newDeviceNames[i] + "插入\r\n";
								}
							}

							//---重置缓存区的大小
							Array.Resize<string>(ref oddDeviceNames, newDeviceNames.Length);
						}

						//---更新当前设备集合
						Array.Copy(newDeviceNames, oddDeviceNames, newDeviceNames.Length);
						//---申请缓存区
						this.m_COMMPortDevices = new COMMDevice[deviceNames.Length];
						//---数据拷贝到缓存区
						Array.Copy(deviceNames, this.m_COMMPortDevices, deviceNames.Length);
						//---判断当前端口的信息
						if (cbb != null)
						{
							//---清理当前设备的集合列表
							cbb.Items.Clear();
							//---重新添加设备集合列表
							cbb.Items.AddRange(oddDeviceNames);
							//---判断默认信息是否有效
							if ((cbb.Text == string.Empty) || (cbb.Text == null) || (cbb.Text == ""))
							{
								cbb.SelectedIndex = 0;
								//---设备处于不工作状态
								this.m_COMMPortSTATE = false;
							}
							else
							{
								if (!oddDeviceNames.Contains(cbb.Text))
								{
									cbb.SelectedIndex = 0;
									//---设备处于不工作状态
									this.m_COMMPortSTATE = false;
								}
								else
								{
									//---获取当前设备在设备集合中位置
									int index = Array.IndexOf(oddDeviceNames, cbb.Text);
									if (index >= 0)
									{
										cbb.SelectedIndex = index;
									}
									else
									{
										cbb.SelectedIndex = 0;
										//---设备处于不工作状态
										this.m_COMMPortSTATE = false;
									}
								}
							}
						}
					}
				}
			}
			//---显示信息
			if ((rtbMsg != null) && (this.m_COMMPortErrMsg != null))
			{
				rtbMsg.Clear();
				if (rtbMsg.InvokeRequired)
				{
					rtbMsg.Invoke(new Action<string>((msg) =>
					{
						RichTextBoxPlus.AppendTextInfoTopWithoutDateTime(rtbMsg, this.m_COMMPortErrMsg, Color.Black, false);
					}), this.m_COMMPortErrMsg);
				}
				else
				{
					RichTextBoxPlus.AppendTextInfoTopWithoutDateTime(rtbMsg, this.m_COMMPortErrMsg, Color.Black, false);
				}
			}
			return _return;
		}

		/// <summary>
		/// 接收数据长度最大为255
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public int ReadResponse_8BitsTask(ref byte[] cmd, int time = 200)
		{
			int _return = 1;
			//---获取开始时间标签
			DateTime nowTime = DateTime.Now;
			//---接收缓存区
			this.m_COMMPortReceBytes = new COMMPortBytes();
			//---接收数据步序
			byte taskStep = 0;
			//---时间计数器
			DateTime startTime = DateTime.Now;
			//---数据存储的临时变量
			int temp = 0;
			//---通信端口为接收状态,不响应其他操作(阻塞操作)
			bool isWork = true;
			//---清空错误消息
			this.m_COMMPortErrMsg = string.Empty;
			//---数据的读取---阻塞读取
			while (isWork)
			{
				switch (taskStep)
				{
					case 0:         //---获取数据报头
						if (this.m_COMMSerialPort.BytesToRead > 0)
						{
							temp = this.m_COMMSerialPort.ReadByte();
							//---读取报头
							if ((byte)temp == this.m_COMMPortReceID)
							{
								//---保存数据
								this.m_COMMPortReceBytes.dateBytes.Add((byte)temp);
								//---进入下一任务
								taskStep = 1;
								//---重置时间标签
								startTime = DateTime.Now;
							}
						}
						break;

					case 1:         //---获取数据长度
						if (this.m_COMMSerialPort.BytesToRead > 0)
						{
							//---读取接收到的数据
							temp = this.m_COMMSerialPort.ReadByte();
							//---数据长度的合法性验证
							if ((temp > 0) && (temp < (this.m_COMMPortSize - 1)))
							{
								//---数据长度合法，接收数据长度
								this.m_COMMPortReceBytes.dateBytes.Add((byte)temp);
								//---进入下一任务
								taskStep = 2;
							}
							else
							{
								//---数据长度不合法，重新接收数据
								this.m_COMMPortReceBytes.dateBytes.Clear();
								taskStep = 0;
								//---重置时间标签
								startTime = DateTime.Now;
							}
						}
						break;

					case 2:         //---获取数据
						if (this.m_COMMSerialPort.BytesToRead > 0)
						{
							//---读取接收到的数据
							temp = this.m_COMMSerialPort.ReadByte();
							this.m_COMMPortReceBytes.dateBytes.Add((byte)temp);
							//---重置时间标签
							startTime = DateTime.Now;
						}
						break;

					case 3:
					default:
						isWork = false;
						this.m_COMMPortErrMsg += "接收数据的过程中发生错误!\r\n";
						_return = 2;
						break;
				}
				//---计算时间
				TimeSpan endTime = DateTime.Now - startTime;
				//---判断是否发生超时错误
				if (endTime.TotalMilliseconds > time)
				{
					//---退出while循环
					isWork = false;
					_return = 3;
					this.m_COMMPortErrMsg += "数据接收发生超时错误!\r\n";
					break;
				}

				//---判断接收到的数据
				if ((taskStep == 2) &&(this.m_COMMPortReceBytes!=null) &&(this.m_COMMPortReceBytes.dateBytes.Count>2) &&((this.m_COMMPortReceBytes.dateBytes.Count - 2) == this.m_COMMPortReceBytes.dateBytes[1]))
				{
					//---退出当前while循环
					isWork = false;
					_return = 0;
					break;
				}
				Application.DoEvents();
			}
			//---判断接收的数据
			if ((_return == 0) && (taskStep == 2) && (isWork == false) && (this.m_COMMPortReceBytes.dateBytes.Count > 2))
			{
				cmd = new byte[this.m_COMMPortReceBytes.dateBytes.Count];
				this.m_COMMPortReceBytes.dateBytes.CopyTo(cmd);
				//---修正数据信息
				if (this.m_COMMPortReceCRC == (byte)USE_CRC.CRC_CHECKSUM)
				{
					//---修正数据的实际长度
					cmd[1] -= 1;
					//---计算校验和
					byte checkSum = GenFunc.GenFuncCRCCheckSum(cmd, (cmd.Length - 1));
					//---判断校验和信息
					if (checkSum != cmd[cmd.Length - 1])
					{
						this.m_COMMPortErrMsg += "数据校验和发生错误!\r\n";
						_return = 4;
					}
					//---接收到的数据
					this.m_COMMPortReceBytes.crc = (byte)USE_CRC.CRC_CHECKSUM;
					this.m_COMMPortReceBytes.crcVal = cmd[cmd.Length - 1];
				}
				else if (this.m_COMMPortReceCRC == (byte)USE_CRC.CRC_CRC8)
				{
					//---修正数据的实际长度
					cmd[1] -= 1;
					//---计算校验和
					byte crc8 = GenFunc.GenFuncCRC8Table(cmd, (cmd.Length - 1));
					//---判断校验和信息
					if (crc8 != cmd[cmd.Length - 1])
					{
						this.m_COMMPortErrMsg += "数据CRC8校验发生错误!\r\n";
						_return = 5;
					}
					//---接收到的数据
					this.m_COMMPortReceBytes.crc = (byte)USE_CRC.CRC_CRC8;
					this.m_COMMPortReceBytes.crcVal = cmd[cmd.Length - 1];
				}
				else if (this.m_COMMPortReceCRC == (byte)USE_CRC.CRC_CRC16)
				{
					//---修正数据的实际长度
					cmd[1] -= 2;
					//---计算校验和
					UInt16 crc16 = GenFunc.GenFuncCRC16Table(cmd, (cmd.Length - 2));
					//---crc16的值
					UInt16 crc16Val = cmd[cmd.Length - 2];
					crc16Val = (UInt16)((crc16 << 8) + cmd[cmd.Length - 1]);
					//---判断校验和信息
					if (crc16 != crc16Val)
					{
						this.m_COMMPortErrMsg += "数据CRC16校验发生错误!\r\n";
						_return = 6;
					}
					//---接收到的数据
					this.m_COMMPortReceBytes.crc = (byte)USE_CRC.CRC_CRC16;
					this.m_COMMPortReceBytes.crcVal = crc16Val;
				}
				else if (this.m_COMMPortReceCRC == (byte)USE_CRC.CRC_CRC32)
				{
					//---修正数据的实际大小
					cmd[1] -= 4;
					UInt32 crc32Val = cmd[cmd.Length - 4];
					crc32Val = (crc32Val << 8) + cmd[cmd.Length - 3];
					crc32Val = (crc32Val << 8) + cmd[cmd.Length - 2];
					crc32Val = (crc32Val << 8) + cmd[cmd.Length - 1];
					//---获取CRC的校验和
					UInt32 crc32 = GenFunc.GenFuncCRC32Table(cmd, (cmd.Length - 4));
					//---判断CRC32的校验信息
					if (crc32 != crc32Val)
					{
						this.m_COMMPortErrMsg += "数据CRC32校验发生错误!\r\n";
						_return = 7;
					}
					//---接收到的数据
					this.m_COMMPortReceBytes.crc = (byte)USE_CRC.CRC_CRC32;
					this.m_COMMPortReceBytes.crcVal = crc32Val;
				}

				if (_return == 0)
				{
					int length = cmd[1] + 2;
					int i = 0;
					this.m_COMMPortReceBytes.dateBytes = new List<byte>();
					for (i = 0; i < length; i++)
					{
						this.m_COMMPortReceBytes.dateBytes.Add(cmd[i]);
					}
					cmd = new byte[this.m_COMMPortReceBytes.dateBytes.Count];
					this.m_COMMPortReceBytes.dateBytes.CopyTo(cmd);
				}
			}
			//---清空接收缓存区
			this.m_COMMSerialPort.DiscardInBuffer();
			//---清空发送缓存区
			this.m_COMMSerialPort.DiscardOutBuffer();
			//---计算本次读取的耗时时间
			this.m_COMMPortUsedTime = DateTime.Now - nowTime;
			return _return;
		}

		/// <summary>
		/// 接收数据长度最大为65535
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public int ReadResponse_16BitsTask(ref byte[] cmd, int time = 200)
		{
			return 1;
		}

		#endregion 函数定义

		#region 重载函数

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public override int Init()
		{
			return 1;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cbb"></param>
		/// <returns></returns>
		public override int Init(ComboBox cbb = null)
		{
			return 1;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cbb"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public override int Init(ComboBox cbb = null, RichTextBox msg = null)
		{
			this.SerialCOMMPortUpdateDevices(cbb, msg);
			return 1;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cmd"></param>
		/// <returns></returns>
		public override int SendCmd(byte[] cmd)
		{
			int _return = 1;
			int length = 0;
			int i = 0;
			if ((this.m_COMMSerialPort != null) && (this.m_COMMSerialPort.IsOpen))
			{
				//---检查缓存区中的数据是否发送完成并等待发送完成
				while (this.m_COMMSerialPort.BytesToWrite > 0)
				{
					//---窗体函数的响应
					Application.DoEvents();
				}
				//----判断是否需要增加设备ID信息
				if ((this.m_DeviceID > 0) && (this.m_DeviceID != 0x00))
				{
					byte[] tempCMD = new byte[cmd.Length];
					Array.Copy(cmd, tempCMD, cmd.Length);
					cmd = new byte[tempCMD.Length + 1];
					if (cmd.Length > 250)
					{
						length = 3;
					}
					else
					{
						length = 2;
					}
					Array.Copy(tempCMD, length, cmd, (length + 1), (tempCMD.Length - length));
					cmd[length] = (byte)this.m_DeviceID;
					for (i = 0; i < length; i++)
					{
						cmd[i] = tempCMD[i];
					}
				}
				//---修正数据长度
				length = cmd.Length;
				//---判断要发送数据的长度
				if (length > 250)
				{
					length -= 3;
					cmd[1] = (byte)(length >> 8);
					cmd[2] = (byte)length;
				}
				else
				{
					length -= 2;
					cmd[1] = (byte)length;
				}
				//---存储发送的数据
				this.m_COMMPortSendBytes = new COMMPortBytes();
				//---CRC校验等级
				if (this.m_COMMPortSendCRC == (byte)USE_CRC.CRC_CHECKSUM)
				{
					//---获取CRC的校验和信息
					byte checkSum = GenFunc.GenFuncCRCCheckSum(cmd, cmd.Length);
					//---重置缓存区的大小
					Array.Resize<byte>(ref cmd, (cmd.Length + 1));
					//---自动添加校验和信息
					cmd[cmd.Length - 1] = checkSum;
					//---发送数据的信息
					this.m_COMMPortSendBytes.crc = (byte)USE_CRC.CRC_CHECKSUM;
					this.m_COMMPortSendBytes.crcVal = checkSum;
				}
				else if (this.m_COMMPortSendCRC == (byte)USE_CRC.CRC_CRC8)
				{
					byte crc8 = GenFunc.GenFuncCRC8Table(cmd, cmd.Length);
					//---重置缓存区的大小
					Array.Resize<byte>(ref cmd, (cmd.Length + 1));
					//---自动添加校验和信息
					cmd[cmd.Length - 1] = crc8;
					//---发送数据的信息
					this.m_COMMPortSendBytes.crc = (byte)USE_CRC.CRC_CRC8;
					this.m_COMMPortSendBytes.crcVal = crc8;
				}
				else if (this.m_COMMPortSendCRC == (byte)USE_CRC.CRC_CRC16)
				{
					int crc16 = GenFunc.GenFuncCRC16Table(cmd, cmd.Length);
					//---重置缓存区的大小
					Array.Resize<byte>(ref cmd, (cmd.Length + 2));
					//---自动添加校验和信息
					cmd[cmd.Length - 2] = (byte)(crc16 >> 8);
					cmd[cmd.Length - 1] = (byte)(crc16);
					//---发送数据的信息
					this.m_COMMPortSendBytes.crc = (byte)USE_CRC.CRC_CRC16;
					this.m_COMMPortSendBytes.crcVal = (UInt32)crc16;
				}
				else if (this.m_COMMPortSendCRC == (byte)USE_CRC.CRC_CRC32)
				{
					UInt32 crc32 = GenFunc.GenFuncCRC32Table(cmd, cmd.Length);
					//---重置缓存区的大小
					Array.Resize<byte>(ref cmd, (cmd.Length + 4));
					//---自动添加校验和信息
					cmd[cmd.Length - 4] = (byte)(crc32 >> 24);
					cmd[cmd.Length - 3] = (byte)(crc32 >> 16);
					cmd[cmd.Length - 2] = (byte)(crc32 >> 8);
					cmd[cmd.Length - 1] = (byte)(crc32);
					//---发送数据的信息
					this.m_COMMPortSendBytes.crc = (byte)USE_CRC.CRC_CRC32;
					this.m_COMMPortSendBytes.crcVal = crc32;
				}
				//---重新修正数据长度
				length = cmd.Length;
				//---判断要发送数据的长度
				if (length > 250)
				{
					length -= 3;
					cmd[1] = (byte)(length >> 8);
					cmd[2] = (byte)length;
				}
				else
				{
					length -= 2;
					cmd[1] = (byte)length;
				}

				i = 0;
				for (i = 0; i < cmd.Length; i++)
				{
					this.m_COMMPortSendBytes.dateBytes.Add(cmd[i]);
				}
				_return = 0;
				//---发送指定长度的数据
				this.m_COMMSerialPort.Write(cmd, 0, cmd.Length);
			}
			else
			{
				if (this.m_COMMPortForm != null)
				{
					MessageBoxPlus.Show(this.m_COMMPortForm, "端口初始化失败!!!", "错误提示");
				}
				else
				{
					MessageBox.Show("端口初始化失败!!!", "错误提示");
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public override int ReadResponse(int time = 200)
		{
			byte[] temp = null;
			return this.ReadResponse(ref temp, time);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public override int ReadResponse(ref byte[] cmd, int time = 200)
		{
			int _return = 1;
			if ((this.m_COMMSerialPort != null) && (this.m_COMMSerialPort.IsOpen))
			{
				if (this.m_COMMPortSize > 250)
				{
					_return = this.ReadResponse_16BitsTask(ref cmd, time);
				}
				else
				{
					_return = this.ReadResponse_8BitsTask(ref cmd, time);
				}
			}
			else
			{
				if (this.m_COMMPortForm != null)
				{
					MessageBoxPlus.Show(this.m_COMMPortForm, "端口初始化失败!!!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show("端口初始化失败!!!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public override int ReadResponse(ref List<byte> cmd, int time = 200)
		{
			byte[] temp = null;
			int _return = this.ReadResponse(ref temp, time);
			cmd = new List<byte>();
			if ((_return==0)&&(temp!=null)&&(temp.Length>2))
			{
				cmd.AddRange(temp);
			}
			return _return;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="res"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public override int SendCmdAndReadResponse(byte[] cmd, ref byte[] res, int time = 200)
		{
			int _return = this.SendCmd(cmd);
			if (_return == 0)
			{
				_return = this.ReadResponse(ref res, time);
			}
			return _return;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public override int OpenDevice(string name)
		{
			return this.OpenDevice(name, null);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="name"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public override int OpenDevice(string name, RichTextBox msg = null)
		{
			int _return = 1;
			if (((name != string.Empty) && (name != null) && (name != "") && (name.StartsWith("COM"))))
			{
				this.m_COMMPortErrMsg = string.Empty;
				//---判断串口类是否有效
				if (this.m_COMMSerialPort == null)
				{
					this.m_COMMSerialPort = new SerialPort();
				}
				//---判断当前端口是否可用
				if (this.m_COMMSerialPort.IsOpen)
				{
					//---响应窗体事件
					Application.DoEvents();
					try
					{
						//---判断是否注册了接收事件
						if (this.m_COMMEnableDataReceivedEvent)
						{
							this.UnRegisterEventHandler();
						}
						//---关闭端口
						this.m_COMMSerialPort.Close();
					}
					catch
					{
						//---端口关闭出现错误
						this.m_COMMPortErrMsg += "端口：" + this.m_COMMSerialPort.PortName + "被其他应用占用，初始化失败!\r\n";
						_return = 2;
						goto GoToExit;
					}
				}
				//---获取端口名称
				if (this.m_COMMSerialPort.PortName != name)
				{
					this.m_COMMSerialPort.PortName = name;
					this.m_COMMPortName = name;
				}
				//---获取端口序号
				this.m_COMMPortIndex = Convert.ToInt16(this.m_COMMPortName.Replace("COM", ""), 10);
				//---设置波特率
				if (this.m_COMMSerialPort.BaudRate != this.baudRate)
				{
					this.m_COMMSerialPort.BaudRate = this.baudRate;
				}
				//---设置数据位
				if (this.m_COMMSerialPort.DataBits != this.dataBits)
				{
					this.m_COMMSerialPort.DataBits = this.dataBits;
				}
				//---设置停止位
				if (this.m_COMMSerialPort.StopBits != (StopBits)this.stopBits)
				{
					this.m_COMMSerialPort.StopBits = (StopBits)this.stopBits;
				}
				//---设置校验位
				Parity tmpParityBits = this.SerialCOMMPortGetParityBits(this.parity);
				if (this.m_COMMSerialPort.Parity != tmpParityBits)
				{
					this.m_COMMSerialPort.Parity = tmpParityBits;
				}
				//---打开端口
				try
				{
					//---打开端口
					this.m_COMMSerialPort.Open();
					//---判断端口打开是否成功
					if (this.m_COMMSerialPort.IsOpen)
					{
						this.m_COMMPortErrMsg += "端口：" + this.m_COMMSerialPort.PortName + "打开成功!\r\n";
						//---清空接收缓存区
						this.m_COMMSerialPort.DiscardInBuffer();
						//---清空发送缓存区
						this.m_COMMSerialPort.DiscardOutBuffer();
						_return = 0;
					}
					else
					{
						this.m_COMMPortErrMsg += "端口：" + this.m_COMMSerialPort.PortName + "打开失败!\r\n";
						_return = 3;
						goto GoToExit;
					}
				}
				catch
				{
					this.m_COMMPortErrMsg += "端口：" + this.m_COMMSerialPort.PortName + "端口被其他应用占用，打开失败!\r\n";
					_return = 4;
					goto GoToExit;
				}
				GoToExit:
				if (msg != null)
				{
					Color msgColor = Color.Black;
					if (_return != 0)
					{
						msgColor = Color.Red;
					}
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, this.m_COMMPortErrMsg, msgColor, false);
				}
				//---消息插件弹出
				if (_return != 0)
				{
					if (this.m_COMMPortForm != null)
					{
						MessageBoxPlus.Show(this.m_COMMPortForm, this.m_COMMPortErrMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						MessageBox.Show(this.m_COMMPortErrMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else
			{
				if (this.m_COMMPortForm != null)
				{
					MessageBoxPlus.Show(this.m_COMMPortForm, "端口名称不合法!\r\n", "错误提示",MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show("端口名称不合法!\r\n", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return _return;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override int OpenDevice(int index)
		{
			if ((index > 0) && (index < 0x100))
			{
				string name = "COM" + Convert.ToString(index);
				return this.OpenDevice(name);
			}
			return 1;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="index"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public override int OpenDevice(int index, RichTextBox msg = null)
		{
			if ((index > 0) && (index < 0x100))
			{
				string name = "COM" + Convert.ToString(index);
				return this.OpenDevice(name, msg);
			}
			return 1;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public override int CloseDevice(string name)
		{
			return this.CloseDevice(name, null);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="name"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public override int CloseDevice(string name, RichTextBox msg = null)
		{
			int _return = 1;
			this.m_COMMPortErrMsg = string.Empty;
			if (((name != string.Empty) && (name != null) && (name != "") && (name.StartsWith("COM"))))
			{
				if ((this.m_COMMSerialPort != null) && (this.m_COMMSerialPort.PortName == name) && (this.m_COMMSerialPort.IsOpen))
				{
					//---响应窗体事件
					Application.DoEvents();
					try
					{
						//---关闭的时候，如果挂载了事件，首先注销事件
						if (this.m_COMMEnableDataReceivedEvent)
						{
							this.UnRegisterEventHandler();
						}
						this.m_COMMSerialPort.Close();
						_return = 0;
					}
					catch
					{
						this.m_COMMPortErrMsg += "端口：" + name + "被其他应用占用，关闭失败!\r\n";
						_return = 2;
					}
				}
				else
				{
					this.m_COMMPortErrMsg += "端口：" + name + "初始化异常!\r\n";
					_return = 3;
				}
				//---
				if (_return == 0)
				{
					this.m_COMMPortErrMsg += "端口：" + name + "关闭成功!\r\n";
				}
				//---消息显示
				if (msg != null)
				{
					Color msgColor = Color.Black;
					if (_return != 0)
					{
						msgColor = Color.Red;
					}
					RichTextBoxPlus.AppendTextInfoTopWithDataTime(msg, this.m_COMMPortErrMsg, msgColor, false);
				}
			}
			else
			{
				if (this.m_COMMPortForm != null)
				{
					MessageBoxPlus.Show(this.m_COMMPortForm, "端口名称不合法!\r\n", "错误提示");
				}
				else
				{
					MessageBox.Show("端口名称不合法!\r\n", "错误提示");
				}
			}
			return _return;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override int CloseDevice(int index)
		{
			if ((index > 0) && (index < 0x100))
			{
				string name = "COM" + Convert.ToString(index);
				return this.CloseDevice(name, null);
			}
			return 1;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="index"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public override int CloseDevice(int index, RichTextBox msg = null)
		{
			if ((index > 0) && (index < 0x100))
			{
				string name = "COM" + Convert.ToString(index);
				return this.CloseDevice(name, msg);
			}
			return 1;
		}

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public override bool IsAttached()
		{
			if (this.m_COMMSerialPort != null)
			{
				return this.m_COMMSerialPort.IsOpen;
			}
			return false;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public override bool IsAttached(string name)
		{
			if (this.m_COMMSerialPort != null)
			{
				if (this.m_COMMSerialPort.PortName == name)
				{
					return this.m_COMMSerialPort.IsOpen;
				}
			}
			return false;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override bool IsAttached(int index)
		{
			if ((index > 0) && (index < 0x100))
			{
				string name = "COM" + Convert.ToString(index);
				return this.IsAttached(name);
			}
			return false;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="isOk"></param>
		/// <returns></returns>
		public override bool IsReadValidData(int isOk)
		{
			if ((isOk != 0) || (this.m_COMMPortReceBytes.dateBytes == null) || (this.m_COMMPortReceBytes.dateBytes.Count < 2))
			{
				return false;
			}

			if (this.m_DeviceID != 0x00)
			{
				int deviceID = 0;
				//---判断要发送数据的长度
				if (this.m_COMMPortReceBytes.dateBytes.Count > 250)
				{
					deviceID = this.m_COMMPortSendBytes.dateBytes[3];
				}
				else
				{
					deviceID = this.m_COMMPortSendBytes.dateBytes[2];
				}

				if (this.m_DeviceID != deviceID)
				{
					return false;
				}
			}

			return true;
		}

		#endregion 重载函数

		#region 事件定义

		/// <summary>
		/// 定义委托事件，用于刷新设备的UI信息
		/// </summary>
		/// <param name="cmd"></param>
		private delegate void AsynUpdateRichTextBox(byte[] cmd);

		/// <summary>
		/// 注册事件
		/// </summary>
		/// <param name="msg"></param>
		public override void RegisterEventHandler(RichTextBox msg = null)
		{
			if (this.m_COMMSerialPort != null)
			{
				if (this.m_COMMEnableDataReceivedEvent!=true)
				{
					//---注册接收事件处理函数
					this.m_COMMSerialPort.DataReceived += DataReceivedEvent;
					this.m_COMMEnableDataReceivedEvent = true;
				}
			}
		}

		/// <summary>
		/// 注销事件
		/// </summary>
		public override void UnRegisterEventHandler()
		{
			if (this.m_COMMSerialPort != null)
			{
				if (this.m_COMMEnableDataReceivedEvent)
				{
					//---注销接收事件处理函数
					this.m_COMMSerialPort.DataReceived -= DataReceivedEvent;
					this.m_COMMEnableDataReceivedEvent = false;
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="m"></param>
		/// <param name="cbb"></param>
		/// <param name="msg"></param>
		public override void DevicesChangedEvent(ref Message m, ComboBox cbb = null, RichTextBox msg = null)
		{
			if (m.Msg == CWndProMsgConst.WM_DEVICECHANGE)                   // 系统硬件改变发出的系统消息
			{
				switch (m.WParam.ToInt32())
				{
					case CWndProMsgConst.WM_DEVICECHANGE:
						break;
					//---设备插入或者更新，并且可以使用
					case CWndProMsgConst.DBT_DEVICEARRIVAL:
					//---设备卸载或者拔出
					case CWndProMsgConst.DBT_DEVICEREMOVECOMPLETE:        
						this.Init(cbb, msg);
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// 数据接收事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void DataReceivedEvent(object sender, EventArgs e)
		{
			//---判断发生的事件类型
			if (e.ToString().Contains("SerialDataReceivedEventArgs"))
			{
				if ((this.m_COMMSerialPort != null) && (this.m_COMMSerialPort.IsOpen) && (this.m_COMMSerialPort.BytesToRead > 0))
				{
					if (this.ReadResponse(200)==0)
					{

					}
				}
			}
		}

		#endregion 事件定义
	}
}