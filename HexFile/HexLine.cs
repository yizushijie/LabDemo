using System;
using System.Text.RegularExpressions;

namespace HexFileLib
{
	/// <summary>
	/// Hex文件的行信息的解析
	/// </summary>

	#region hex文件类型

	public enum HexType : byte
	{
		DATA_RECORD = 0,                    //用来记录数据，HEX文件的大部分记录都是数据记录
		END_OF_FILE_RECORD = 1,                 //用来标识文件结束，放在文件的最后，标识HEX文件的结尾
		EXTEND_SEGMENT_ADDRESS_RECORD = 2,                  //用来标识扩展段地址的记录
		START_SEGMENT_ADDRESS_RECORD = 3,                   //开始段地址记录
		EXTEND_LINEAR_ADDRESS_RECORD = 4,                   //用来标识扩展线性地址的记录
		START_LINEAR_ADDRESS_RECORD = 5,                    //开始线性地址记录
	}

	#endregion hex文件类型

	/// <summary>
	/// Hex的数据行处理
	/// </summary>
	public partial class HexLine
	{
		#region 变量定义

		/// <summary>
		/// 当前数据是否可用;true--可用;false--不可用
		/// </summary>
		protected bool _available = false;

		/// <summary>
		/// 当前数据的长度
		/// </summary>
		protected byte _length = 0;

		/// <summary>
		/// 当前数据的起始地址
		/// </summary>
		protected long _addr = 0;

		/// <summary>
		/// 当前数据类型
		/// </summary>
		protected HexType _hexType = HexType.END_OF_FILE_RECORD;

		/// <summary>
		/// 当前数据信息
		/// </summary>
		protected byte[] _hexByte = null;

		/// <summary>
		/// 校验信息
		/// </summary>
		protected byte _crcVal = 0;

		/// <summary>
		/// 错误信息
		/// </summary>
		protected string _errMsg = "";

		#endregion 变量定义

		#region 属性定义

		/// <summary>
		/// 属性只读
		/// </summary>
		public bool m_Available
		{
			get
			{
				return this._available;
			}
		}

		/// <summary>
		/// 属性只读
		/// </summary>
		public byte m_Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>
		/// 属性只读
		/// </summary>
		public long m_Addr
		{
			get
			{
				return this._addr;
			}
		}

		/// <summary>
		/// 属性只读
		/// </summary>
		public HexType m_HexType
		{
			get
			{
				return this._hexType;
			}
		}

		/// <summary>
		/// 属性只读
		/// </summary>
		public byte[] m_HexByte
		{
			get
			{
				return this._hexByte;
			}
		}

		/// <summary>
		/// 属性只读
		/// </summary>
		public byte m_CRCVal
		{
			get
			{
				return this._crcVal;
			}
		}

		/// <summary>
		/// 属性只读
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
		public HexLine()
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="fileLine"></param>
		public HexLine(string fileLine)
		{
			this._available = this.GetHexLine(fileLine);
		}

		#endregion 构造函数

		#region 函数定义

		/// <summary>
		/// 解析Hex文件数据行
		/// </summary>
		/// <param name="hexLine"></param>
		/// <returns></returns>
		public bool GetHexLine(string hexLine)
		{
			//---将数据记录行的首位空格去除并装换成大写模式
			hexLine = hexLine.Trim().ToUpper();
			//---判断字符信息是否合法
			if ((hexLine == string.Empty) || (hexLine == ""))
			{
				this._errMsg = "数据记录行的信息为空!";
				return false;
			}
			//---获取数据记录行的头
			if (hexLine[0] != ':')
			{
				this._errMsg = "数据记录行的头格式错误!";
				return false;
			}

			//---检查数据是否符合十六进制个数
			if (!HexLine.IsHexNumber(hexLine.Substring(1)))
			{
				this._errMsg = "数据记录的信息中含有不合法的信息!";
				return false;
			}
			//---获取数据的长度
			try
			{
				this._length = byte.Parse(hexLine.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
			}
			catch
			{
				this._errMsg = "数据记录行的长度错误!";
				return false;
			}
			this._crcVal = this._length;

			//---获取数据地址
			try
			{
				this._addr = UInt32.Parse(hexLine.Substring(3, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
			}
			catch
			{
				this._errMsg = "数据记录行的地址错误!";
				return false;
			}
			//---开始极端校验和
			this._crcVal += (byte)(this._addr >> 8);
			this._crcVal += (byte)(this._addr);

			//---获取数据类型
			try
			{
				this._hexType = (HexType)(byte.Parse(hexLine.Substring(7, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
			}
			catch
			{
				this._errMsg = "数据记录行的类型错误!";
				return false;
			}

			//---判断数据类型是否符合格式
			if (!Enum.IsDefined(typeof(HexType), this._hexType))
			{
				this._errMsg = "数据记录行的数据类型不能被识别!";
				return false;
			}
			this._crcVal += (byte)this._hexType;

			//---申请存储数据的空间
			this._hexByte = new byte[this._length];
			int i = 0;
			for (i = 0; i < this._length; i++)
			{
				try
				{
					this._hexByte[i] = byte.Parse(hexLine.Substring(9 + i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
					this._crcVal += this._hexByte[i];
				}
				catch
				{
					this._errMsg = "数据记录行的数据信息错误";
					return false;
				}
			}

			//---获取数据记录行的记录信息
			byte tempCRC = 0;
			try
			{
				tempCRC = byte.Parse(hexLine.Substring(9 + this._length * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
			}
			catch
			{
				this._errMsg = "数据记录行的校验信息错误";
				return false;
			}

			//---计算校验和
			this._crcVal = (byte)(0x100 - this._crcVal);
			//---校验和比对
			if (this._crcVal != tempCRC)
			{
				this._errMsg = "数据记录行的校验错误!";
				return false;
			}
			//---数据类型比较----判断数据记录行的信息是否正确
			switch (this._hexType)
			{
				case HexType.DATA_RECORD:
					if (this._length != this._hexByte.Length)
					{
						this._errMsg = "数据文件数据记录标识错误!";
						return false;
					}
					break;

				case HexType.END_OF_FILE_RECORD:
					if (this._length != 0)
					{
						this._errMsg = "数据文件结束标识错误!";
						return false;
					}
					break;

				case HexType.EXTEND_SEGMENT_ADDRESS_RECORD:
					if (this._length != 2)
					{
						this._errMsg = "数据文件扩展段地址标识错误!";
						return false;
					}
					break;

				case HexType.START_SEGMENT_ADDRESS_RECORD:
					if (this._length != 4)
					{
						this._errMsg = "数据文件段地址标识错误!";
						return false;
					}
					break;

				case HexType.EXTEND_LINEAR_ADDRESS_RECORD:
					if (this._length != 2)
					{
						this._errMsg = "数据文件扩展线性地址标识错误!";
						return false;
					}
					break;

				case HexType.START_LINEAR_ADDRESS_RECORD:
					if (this._length != 4)
					{
						this._errMsg = "数据文件开始线性地址标识错误!";
						return false;
					}
					break;

				default:
					this._errMsg = "不能识别的数据文件!";
					return false;
			}
			return true;
		}

		/// <summary>
		/// 判断给定的字符串是不是都是16进制数
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		public static bool IsHexNumber(string hexString)
		{
			if ((hexString == string.Empty) || (hexString == null))
			{
				return false;
			}
			hexString = hexString.Trim().ToUpper();
			if (hexString == "")
			{
				return false;
			}
			//---利用正则表达式
			return Regex.IsMatch(hexString, "^[0-9A-Fa-f]+$");
		}

		/// <summary>
		/// 将数据转换成HexLine
		/// </summary>
		/// <param name="addr"></param>
		/// <param name="type"></param>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static string ToHexLine(long addr, HexType type, byte[] buffer)
		{
			string _return = ":";
			byte crc = (byte)(buffer.Length);
			_return += crc.ToString("X2");
			crc += (byte)(addr >> 8);
			crc += (byte)(addr);
			_return += addr.ToString("X4");
			crc += (byte)(type);
			_return += ((byte)type).ToString("X2");
			for (int i = 0; i < buffer.Length; i++)
			{
				_return += buffer[i].ToString("X2");
				crc += buffer[i];
			}
			crc = (byte)(0x100 - crc);
			_return += crc.ToString("X2");
			_return += "\r\n";
			return _return;
		}

		/// <summary>
		/// 将数据转换成HexLine
		/// </summary>
		/// <param name="addr"></param>
		/// <param name="type"></param>
		/// <param name="buffer"></param>
		/// <param name="isHighFirst"></param>
		/// <returns></returns>
		public static string ToHexLine(long addr, HexType type, byte[] buffer, bool isHighFirst = false)
		{
			string _return = ":";
			byte crc = (byte)(buffer.Length);
			_return += crc.ToString("X2");
			crc += (byte)(addr >> 8);
			crc += (byte)(addr);
			_return += addr.ToString("X4");
			crc += (byte)(type);
			_return += ((byte)type).ToString("X2");
			for (int i = 0; i < buffer.Length; i += 2)
			{
				if (isHighFirst)
				{
					_return += string.Format("{0:X2}{1:X2}", buffer[i + 1], buffer[i]);
				}
				else
				{
					_return += string.Format("{0:X2}{1:X2}", buffer[i], buffer[i + 1]);
				}
				crc += buffer[i];
				crc += buffer[i + 1];
			}
			crc = (byte)(0x100 - crc);
			_return += crc.ToString("X2");
			_return += "\r\n";
			return _return;
		}

		/// <summary>
		/// 数据记录行
		/// </summary>
		/// <param name="addr"></param>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static string ToHexLineDataRecord(long addr, byte[] buffer)
		{
			return HexLine.ToHexLine(addr, HexType.DATA_RECORD, buffer);
		}

		/// <summary>
		/// 数据结束标识符
		/// </summary>
		/// <returns></returns>
		public static string ToHexLineEndOfFileRecord()
		{
			return (":00000001FF" + "\r\n");
		}

		/// <summary>
		/// 数据扩展段地址
		/// </summary>
		/// <param name="addr"></param>
		/// <returns></returns>
		public static string ToHexLineExternSegmentAddrRecord(long addr)
		{
			string _return = "";
			byte[] line = new byte[2] { (byte)((addr >> 8) & 0xFF), (byte)(addr & 0xFF) };
			_return = HexLine.ToHexLine(0, HexType.EXTEND_SEGMENT_ADDRESS_RECORD, line);
			return _return;
		}

		/// <summary>
		/// 数据扩展线性地址
		/// </summary>
		/// <param name="addr"></param>
		/// <returns></returns>
		public static string ToHexLineExternLineAddrRecord(long addr)
		{
			string _return = "";

			byte[] line = new byte[2] { (byte)((addr >> 8) & 0xFF), (byte)(addr & 0xFF) };

			_return = HexLine.ToHexLine(0, HexType.EXTEND_LINEAR_ADDRESS_RECORD, line);

			return _return;
		}

		#endregion 函数定义
	}
}