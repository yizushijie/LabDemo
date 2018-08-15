using System;
using System.Collections.Generic;
using System.IO;

namespace HexFileLib
{
	/// <summary>
	/// Hex文件的处理
	/// </summary>
	public partial class HexFile : List<HexLine>
	{
		#region 变量定义

		/// <summary>
		/// 文件是否可用;true---文件解析完成;false---文件解析失败
		/// </summary>
		private bool _hexAvailable = false;

		/// <summary>
		/// hex文件信息图
		/// </summary>
		private byte[] _hexMap = null;

		/// <summary>
		/// 错误信息
		/// </summary>
		private string _errMsg = null;

		#endregion 变量定义

		#region 属性定义

		/// <summary>
		/// Hex文件的起始地址
		/// </summary>
		public long m_BeginAddr
		{
			get
			{
				long _return = 0;
				byte[] buffer = null;
				switch (base[0].m_HexType)
				{
					case HexType.DATA_RECORD:               //---文件信息首先填充0xFF，开始地址是0x00
						_return = base[0].m_Addr;
						if (_return != 0)
						{
							_return = 0;
						}
						break;

					case HexType.END_OF_FILE_RECORD:
						this._hexAvailable = false;
						break;

					case HexType.EXTEND_SEGMENT_ADDRESS_RECORD:
						buffer = new byte[2] { base[0].m_HexByte[1], base[0].m_HexByte[0] };
						_return = BitConverter.ToUInt16(buffer, 0);
						_return <<= 4;
						break;

					case HexType.START_SEGMENT_ADDRESS_RECORD:
						buffer = new byte[4] { base[0].m_HexByte[3], base[0].m_HexByte[2], base[0].m_HexByte[1], base[0].m_HexByte[0] };
						_return = BitConverter.ToUInt32(buffer, 0);
						this._hexAvailable = false;
						break;

					case HexType.EXTEND_LINEAR_ADDRESS_RECORD:
						buffer = new byte[2] { base[0].m_HexByte[1], base[0].m_HexByte[0] };
						_return = BitConverter.ToUInt16(buffer, 0);
						_return <<= 16;
						break;

					case HexType.START_LINEAR_ADDRESS_RECORD:
						buffer = new byte[4] { base[0].m_HexByte[3], base[0].m_HexByte[2], base[0].m_HexByte[1], base[0].m_HexByte[0] };
						_return = BitConverter.ToUInt32(buffer, 0);
						this._hexAvailable = false;
						break;

					default:
						this._hexAvailable = false;
						break;
				}

				return _return;
			}
		}

		/// <summary>
		/// 获取Hex文件的结束地址
		/// </summary>
		public long m_EndAddr
		{
			get
			{
				long _return = 0;
				byte[] buffer = null;
				for (int i = 0; i < base.Count; i++)
				{
					switch (base[i].m_HexType)
					{
						case HexType.DATA_RECORD:                            //0
							_return = (_return & 0xFFFF0000) + (long)(base[i].m_Addr + base[i].m_Length);
							break;

						case HexType.END_OF_FILE_RECORD:                     //1
							break;

						case HexType.EXTEND_SEGMENT_ADDRESS_RECORD:          //2
							buffer = new byte[2] { base[i].m_HexByte[1], base[i].m_HexByte[0] };
							_return = BitConverter.ToUInt16(buffer, 0);
							_return <<= 4;
							break;

						case HexType.START_SEGMENT_ADDRESS_RECORD:           //3
							buffer = new byte[4] { base[i].m_HexByte[3], base[i].m_HexByte[2], base[i].m_HexByte[1], base[i].m_HexByte[0] };
							//_return = BitConverter.ToUInt32(buffer, 0);
							break;

						case HexType.EXTEND_LINEAR_ADDRESS_RECORD:           //4
							buffer = new byte[2] { base[i].m_HexByte[1], base[i].m_HexByte[0] };
							_return = BitConverter.ToUInt16(buffer, 0);
							_return <<= 16;
							break;

						case HexType.START_LINEAR_ADDRESS_RECORD:            //5
							buffer = new byte[4] { base[i].m_HexByte[3], base[i].m_HexByte[2], base[i].m_HexByte[1], base[i].m_HexByte[0] };
							//_return = BitConverter.ToUInt32(buffer, 0);
							break;

						default:
							break;
					}
				}
				return _return;
			}
		}

		/// <summary>
		/// 获取Hex文件的长度
		/// </summary>
		public long m_HexLength
		{
			get
			{
				if (this._hexAvailable)
				{
					return (this.m_EndAddr - this.m_BeginAddr);
				}
				else
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// 属性只读---文件信息是否可以利用
		/// </summary>
		public bool m_HexAvailable
		{
			get
			{
				return this._hexAvailable;
			}
		}

		/// <summary>
		/// 属性只读---读取到的信息
		/// </summary>
		public byte[] m_HexMap
		{
			get
			{
				return this._hexMap;
			}
		}

		/// <summary>
		/// 获取错误信息
		/// </summary>
		public string m_ErrMsg
		{
			get
			{
				return this._errMsg;
			}
		}

		#endregion 属性定义

		#region 构造函数

		/// <summary>
		/// 构造函数
		/// </summary>
		public HexFile() : base()
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="fileName"></param>
		public HexFile(string fileName) : base()
		{
			if ((fileName != null) && (fileName != string.Empty))
			{
				this._hexAvailable = this.GetHexFile(fileName);
			}
			else
			{
				this._hexAvailable = false;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="length"></param>
		public HexFile(string fileName, long length) : base()
		{
			if ((fileName != null) && (fileName != string.Empty))
			{
				this._hexAvailable = GetHexFile(fileName, length);
			}
			else
			{
				this._hexAvailable = false;
			}
		}

		#endregion 构造函数

		#region 函数定义

		/// <summary>
		/// 获取HEX文件的信息
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public bool GetHexFile(string filePath)
		{
			//---检查文件是否存在
			if (!File.Exists(filePath))
			{
				this._errMsg = "给定的Hex文件不存在!\r\n";
				return false;
			}

			try
			{
				using (StreamReader std = new StreamReader(filePath))
				{
					long i = 0;
					while (std.Peek() >= 0)
					{
						i++;
						//---读取的数据
						string readline = std.ReadLine();
						//---每行数据创建一个对象
						HexLine readHexLine = new HexLine(readline);
						//---判断数据的读取是否有效
						if (readHexLine.m_Available)
						{
							base.Add(readHexLine);
						}
						else
						{
							this._errMsg = "第" + i.ToString() + "行" + "数据解析错误!" + readHexLine.m_ErrMsg + "\r\n";
							return false;
						}
					}
				}
			}
			catch (Exception)
			{
				this._errMsg = "读取数据发生错误!\r\n";
				return false; ;
			}
			return true;
		}

		/// <summary>
		/// 获取解析后的数据
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public byte[] GetHexFile(byte val = 0xFF)
		{
			//---确保最高是偶数
			long length = this.m_HexLength;
			if ((length & 0x01) != 0)
			{
				length += 1;
			}
			//---创建缓存区
			byte[] _return = new byte[length];
			if (_return == null)
			{
				return null;
			}
			//---用指定的数据填充缓存区
			HexFile.memset(ref _return, length, val);
			//---初始化数据的地址
			long baseAddr = this.m_BeginAddr;
			//---将解析后的数据填充到数据缓存区
			int i = 0;
			for (i = 0; i < base.Count; i++)
			{
				byte[] buffer = null;
				//---数据的解析
				switch (base[i].m_HexType)
				{
					case HexType.DATA_RECORD:
						//---拷贝数据
						Array.Copy(base[i].m_HexByte, 0, _return, (baseAddr + base[i].m_Addr), base[i].m_Length);
						break;

					case HexType.END_OF_FILE_RECORD:
						break;

					case HexType.EXTEND_SEGMENT_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[2] { base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt16(buffer, 0);
						baseAddr <<= 4;
						break;

					case HexType.START_SEGMENT_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[4] { base[i].m_HexByte[3], base[i].m_HexByte[2], base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt32(buffer, 0);
						baseAddr <<= 4;
						break;

					case HexType.EXTEND_LINEAR_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[2] { base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt16(buffer, 0);
						baseAddr <<= 16;
						break;

					case HexType.START_LINEAR_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[4] { base[i].m_HexByte[3], base[i].m_HexByte[2], base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt32(buffer, 0);
						break;

					default:
						return null;
				}
			}
			return _return;
		}

		/// <summary>
		/// 获取解析后的数据
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public byte[] GetHexFile(long length)
		{
			//---创建缓存区
			byte[] _return = new byte[length];
			if (_return == null)
			{
				return null;
			}
			//---用指定的数据填充缓存区
			HexFile.memset(ref _return, length, 0xFF);
			//---初始化数据的地址
			long baseAddr = this.m_BeginAddr;
			//---将解析后的数据填充到数据缓存区
			int i = 0;
			for (i = 0; i < base.Count; i++)
			{
				byte[] buffer = null;
				//---数据类型的解析
				switch (base[i].m_HexType)
				{
					case HexType.DATA_RECORD:
						//---拷贝数据
						Array.Copy(base[i].m_HexByte, 0, _return, (baseAddr + base[i].m_Addr), base[i].m_Length);
						break;

					case HexType.END_OF_FILE_RECORD:
						break;

					case HexType.EXTEND_SEGMENT_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[2] { base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt16(buffer, 0);
						baseAddr <<= 4;
						break;

					case HexType.START_SEGMENT_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[4] { base[i].m_HexByte[3], base[i].m_HexByte[2], base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt32(buffer, 0);
						baseAddr <<= 4;
						break;

					case HexType.EXTEND_LINEAR_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[2] { base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt16(buffer, 0);
						baseAddr <<= 16;
						break;

					case HexType.START_LINEAR_ADDRESS_RECORD:
						//---获取数据的地址
						buffer = new byte[4] { base[i].m_HexByte[3], base[i].m_HexByte[2], base[i].m_HexByte[1], base[i].m_HexByte[0] };
						baseAddr = BitConverter.ToUInt32(buffer, 0);
						break;

					default:
						return null;
				}
			}
			return _return;
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public bool GetHexFile(string filePath, long length)
		{
			//---依据文件路径获取Hex数据
			if (this.GetHexFile(filePath))
			{
				if (this._hexMap == null)
				{
					this._hexMap = new byte[] { };
				}
				//---获取Hex解析之后的数据
				this._hexMap = this.GetHexFile(length);
				if ((this._hexMap != null) && (this._hexMap.Length != 0))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 字数据
		/// </summary>
		/// <param name="val"></param>
		/// <param name="isHighFirst"></param>
		/// <returns></returns>
		public int[] GetHexFile(bool isHighFirst = false)
		{
			//---获取当前数据
			byte[] bufferMap = GetHexFile(0xFF);
			//---创建缓存区
			int[] _return = new int[bufferMap.Length / 2];
			//---填充数组为指定的数据
			HexFile.memset(ref _return, _return.Length, 0xFFFF);
			//---将数据转换成Int数组
			int i = 0;
			for (i = 0; i < _return.Length; i++)
			{
				if (isHighFirst)
				{
					//---高字节在前
					_return[i] = bufferMap[2 * i + 1];
					_return[i] = (_return[i] << 8) + bufferMap[2 * i];
				}
				else
				{
					//---低字节在前
					_return[i] = bufferMap[2 * i];
					_return[i] = (_return[i] << 8) + bufferMap[2 * i + 1];
				}
			}

			return _return;
		}

		/// <summary>
		/// 将给定的数据保存为HEX数据行
		/// </summary>
		/// <param name="addr"></param>
		/// <param name="val"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string[] ToHexFileLine(long addr, byte[] val, int num)
		{
			List<string> _return = new List<string>();
			//判断每行的数据是否过大或者不能被16整除
			if ((num > 0xFF) || ((num % 16) != 0))
			{
				num = 16;
			}
			//---进行分行处理
			long lineCount = (val.Length / num);
			//---求整数处理
			if ((val.Length % num) != 0)
			{
				lineCount += 1;
			}
			//---数据保存处理
			for (long i = 0; i < lineCount; i++)
			{
				string temp = "";
				//---计算当前数据行的数据
				int byteCount = (int)(((val.Length - i * num) > num) ? num : (val.Length - i * num));
				//---当前行数据
				byte[] lineVal = new byte[byteCount];
				//---拷贝数据
				Array.Copy(val, i * num, lineVal, 0, lineVal.Length);
				//---计算地址
				long baseAddr = addr + i * num;
				//---数据空间64K检查
				if (((baseAddr % 65536) == 0) && (baseAddr != 0))
				{
					//---对65536求整处理
					long externLineAddr = (baseAddr / 65536);
					temp = HexLine.ToHexLineExternLineAddrRecord(externLineAddr);
					_return.Add(temp);
					temp = string.Empty;
				}
				//---本次数组的内容为0xFF，则跳过
				if (HexFile.equal(lineVal))
				{
					continue;
				}
				else
				{
					//---保存数据记录文件
					temp = HexLine.ToHexLineDataRecord((baseAddr & 0xFFFF), lineVal);
					_return.Add(temp);
				}
			}
			return _return.ToArray();
		}

		/// <summary>
		/// 保存为HEX文件
		/// </summary>
		/// <param name="addr"></param>
		/// <param name="val"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string ToSaveHexFile(long addr, byte[] val, int num)
		{
			string _return = "";
			//---获取数据行
			string[] temp = HexFile.ToHexFileLine(addr, val, num);
			if (temp == null)
			{
				return null;
			}
			//---填充数据行
			int i = 0;
			for (i = 0; i < temp.Length; i++)
			{
				_return += temp[i];
			}
			//---保存为数据行
			_return += HexLine.ToHexLineEndOfFileRecord();
			return _return;
		}

		/// <summary>
		/// 保存为HEX文件
		/// </summary>
		/// <param name="val"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string ToSaveHexFile(byte[] val, int num)
		{
			return HexFile.ToSaveHexFile(0, val, num);
		}

		/// <summary>
		/// 保存为HEX文件
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string ToSaveHexFile(byte[] val)
		{
			return HexFile.ToSaveHexFile(0, val, 16);
		}

		/// <summary>
		/// 将HexFile文件中的数据保存为16进制的字节数组比如00保存为0x00
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <returns></returns>
		public string HexFileToHexByteArray(string filePath)
		{
			string _return = "";
			//---获取当前文件数据
			this._hexAvailable = this.GetHexFile(filePath);
			if (this._hexAvailable != true)
			{
				return null;
			}
			//---获取当前的数组
			byte[] tempBuffer = this.GetHexFile(0xFF);
			if (tempBuffer == null)
			{
				return null;
			}
			long i = 0;
			for (i = 0; i < tempBuffer.Length; i++)
			{
				_return += "0x" + tempBuffer[i].ToString("X2") + ", ";
				//---每行16个数据,加入换行符
				if ((i % 16) == 0)
				{
					_return += "\r\n";
				}
			}
			return _return;
		}

		/// <summary>
		/// 将HexFile文件中的数据保存为16进制的字节数组比如00保存为0x00
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public string HexFileToHexWordArray(string filePath)
		{
			string _return = "";
			//---获取当前文件数据
			this._hexAvailable = this.GetHexFile(filePath);
			if (this._hexAvailable != true)
			{
				return null;
			}
			//---获取当前的数组
			int[] tempBuffer = this.GetHexFile(false);
			if (tempBuffer == null)
			{
				return null;
			}
			long i = 0;
			for (i = 0; i < tempBuffer.Length; i++)
			{
				_return += "0x" + tempBuffer[i].ToString("X4") + ", ";
				//---每行16个数据,加入换行符
				if ((i % 16) == 0)
				{
					_return += "\r\n";
				}
			}
			return _return;
		}

		/// <summary>
		/// 数据填充
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="length"></param>
		/// <param name="val"></param>
		public static void memset(ref byte[] buffer, long length, byte val = 0xFF)
		{
			if ((buffer == null) || (buffer.Length != length))
			{
				buffer = new byte[length];
			}
			for (int i = 0; i < length; i++)
			{
				buffer[i] = val;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="length"></param>
		/// <param name="val"></param>
		public static void memset(ref int[] buffer, long length, int val = 0xFFFF)
		{
			if ((buffer == null) || (buffer.Length != length))
			{
				buffer = new int[length];
			}
			for (int i = 0; i < length; i++)
			{
				buffer[i] = val;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="val"></param>
		/// <returns></returns>
		public static bool equal(byte[] buffer, byte val = 0xFF)
		{
			if ((buffer == null))
			{
				return false;
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				if (buffer[i] == val)
				{
					continue;
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		#endregion 函数定义
	}
}