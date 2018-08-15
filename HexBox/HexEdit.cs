using System.Drawing;
using System.Windows.Forms;

namespace HexBoxLib
{
	/// <summary>
	/// 计算字体的高度和宽度
	/// </summary>
	public partial class HexBox : Control
	{
		#region 变量定义

		#region 在更新数据的时候是否需要对比现在的数据和已经存在的数据，已经存在的数据进行颜色的标注

		/// <summary>
		/// 是否在更新的数据时候进行数据的不同的标注
		/// </summary>
		private bool _isShowDifference = false;

		#endregion 在更新数据的时候是否需要对比现在的数据和已经存在的数据，已经存在的数据进行颜色的标注

		#region X轴地址

		/// <summary>
		/// 地址信息是否显示
		/// </summary>
		private bool _showXAddr = true;

		/// <summary>
		/// 地址的数据高度
		/// </summary>
		private int _xAddrHeight = 24;

		/// <summary>
		/// 地址的背景色
		/// </summary>
		private Color _xAddrBackGroundColor = Color.FromArgb(240, 240, 240);

		/// <summary>
		/// 字体大小
		/// </summary>
		private Color _xAddrFontColor = Color.Black;

		/// <summary>
		/// 数据显示的bit位数
		/// </summary>
		public enum XAddrShowBits
		{
			Bit16 = 16,
			Bit32 = 32,
		}

		/// <summary>
		/// 显示数据的个数
		/// </summary>
		private XAddrShowBits _xAddrShowBits = XAddrShowBits.Bit16;

		#endregion X轴地址

		#region Y轴地址

		/// <summary>
		/// 是都显示地址
		/// </summary>
		private bool _showYAddr = true;

		/// <summary>
		/// 地址起始的地址
		/// </summary>
		private int _yAddrBeginWidth = 32;

		/// <summary>
		/// 地址的宽度
		/// </summary>
		private int _yAddrWidth = 100;

		/// <summary>
		/// 地址的背景色
		/// </summary>
		private Color _yAddrBackGroundColor = Color.FromArgb(240, 240, 240);

		/// <summary>
		/// 地址的字体颜色
		/// </summary>
		private Color _yAddrFontColor = Color.Black;

		/// <summary>
		/// 地址显示的位数
		/// </summary>
		public enum YAddrShowBits
		{
			Bit2 = 2,
			Bit4 = 4,
			Bit6 = 6,
			Bit8 = 8,
		}

		/// <summary>
		/// 显示数据的位数
		/// </summary>
		private YAddrShowBits _yAddrShowBits = YAddrShowBits.Bit4;

		#endregion Y轴地址

		#region 数据地址

		/// <summary>
		/// 数据格式
		/// </summary>
		public enum EncodingType
		{
			ANSI = 1,
			Unicond = 2,
		}

		/// <summary>
		/// 数据的格式
		/// </summary>
		private EncodingType _dataAddrType = EncodingType.ANSI;

		/// <summary>
		/// 数据的字体的颜色
		/// </summary>
		private Color _dataAddrFontColor = Color.Black;

		#endregion 数据地址

		#region 字体

		/// <summary>
		/// 字体
		/// </summary>
		private Font _font = new Font("黑体", 12);

		/// <summary>
		/// 字体背景色
		/// </summary>
		private Color _backGroundColor = Color.White;

		#endregion 字体

		#region 外部线条

		/// <summary>
		/// 外部线条颜色
		/// </summary>
		private Color _outLineColor = Color.DarkGray;

		/// <summary>
		/// 外部线条的宽度
		/// </summary>
		private int _outLineWidth = 2;

		#endregion 外部线条

		#region 缓存区定义

		/// <summary>
		/// 当前数据
		/// </summary>
		private byte[] _nowByte = null;

		/// <summary>
		/// 最后的数据
		/// </summary>
		private byte[] _lastByte = null;

		/// <summary>
		/// 是否有新数据
		/// </summary>
		private bool _isNewByte = false;

		#endregion 缓存区定义

		#region 垂直滚动条

		/// <summary>
		/// 垂直滚动条
		/// </summary>
		private VScrollBar m_VScrollBar;

		/// <summary>
		/// 垂直滚动条的下拉控件的宽度
		/// </summary>
		private int m_ScrollWidth = 16;

		#endregion 垂直滚动条

		#region 字符

		/// <summary>
		/// 每行显示的数据个数
		/// </summary>
		private int row_DisplayNum = 16;

		/// <summary>
		/// 每行中字符间的间隔
		/// </summary>
		private int row_StaffWidth = 10;

		/// <summary>
		/// 每列中字符间的间隔
		/// </summary>
		private int column_StaffWidth = 4;

		/// <summary>
		/// 当前显示的起始行号
		/// </summary>
		private int row_DisplayIndex = 0;

		/// <summary>
		/// 当前选中的行号
		/// </summary>
		private int row_SelectedIndex = -1;

		/// <summary>
		/// 数据行矩形框的位置
		/// </summary>
		private int current_PositionOffset = 7;

		#endregion 字符

		#region 光标信息

		/// <summary>
		/// 鼠标位置
		/// </summary>
		private POS m_MousePos;

		private struct POS
		{
			public int iPos;
			public int iArea;
			public bool bLeftPos;
			public bool bRightPos;
		}

		/// <summary>
		/// 是否创建了Caret
		/// </summary>
		private bool m_IsCreateCaret = false;

		/// <summary>
		/// 是否隐藏了Caret
		/// </summary>
		private bool m_IsHideCaret = false;

		#endregion 光标信息

		#endregion 变量定义

		#region 属性定义

		#region 数据更新是的颜色区分

		public bool m_isShowDifference
		{
			get
			{
				return this._isShowDifference;
			}
			set
			{
				this._isShowDifference = value;
			}
		}

		#endregion 数据更新是的颜色区分

		#region X轴地址

		public bool m_ShowXAddr
		{
			get
			{
				return this._showXAddr;
			}
			set
			{
				this._showXAddr = value;
			}
		}

		public int m_XAddrHeight
		{
			get
			{
				return this._xAddrHeight;
			}
			set
			{
				this._xAddrHeight = value;
			}
		}

		public Color m_XAddrBackGroundColor
		{
			get
			{
				return this._xAddrBackGroundColor;
			}
			set
			{
				this._xAddrBackGroundColor = value;
			}
		}

		public Color m_XAddrFontColor
		{
			get
			{
				return this._xAddrFontColor;
			}
			set
			{
				this._xAddrFontColor = value;
			}
		}

		public XAddrShowBits m_XAddrShowBits
		{
			get
			{
				return this._xAddrShowBits;
			}
			set
			{
				this._xAddrShowBits = value;
			}
		}

		#endregion X轴地址

		#region Y轴地址

		public bool m_ShowYAddr
		{
			get
			{
				return this._showYAddr;
			}
			set
			{
				this._showYAddr = value;
			}
		}

		public int m_YAddrBeginWidth
		{
			get
			{
				return this._yAddrBeginWidth;
			}
			set
			{
				this._yAddrBeginWidth = value;
			}
		}

		public int m_YAddrWidth
		{
			get
			{
				return (this._yAddrWidth);
			}
			set
			{
				this._yAddrWidth = (value);
			}
		}

		public Color m_YAddrBackGroundColor
		{
			get
			{
				return this._yAddrBackGroundColor;
			}
			set
			{
				this._yAddrBackGroundColor = value;
			}
		}

		public Color m_YAddrFontColor
		{
			get
			{
				return this._yAddrFontColor;
			}
			set
			{
				this._yAddrFontColor = value;
			}
		}

		public YAddrShowBits m_YAddrShowBits
		{
			get
			{
				return this._yAddrShowBits;
			}
			set
			{
				this._yAddrShowBits = value;
			}
		}

		#endregion Y轴地址

		#region 数据地址

		public EncodingType m_DataAddrType
		{
			get
			{
				return this._dataAddrType;
			}
			set
			{
				this._dataAddrType = value;
			}
		}

		public Color m_DataAddrFontColor
		{
			get
			{
				return this._dataAddrFontColor;
			}
			set
			{
				this._dataAddrFontColor = value;
			}
		}

		#endregion 数据地址

		#region 字体

		public Font m_Font
		{
			get
			{
				return this._font;
			}
			set
			{
				this._font = value;
			}
		}

		public Color m_BackgroundColor
		{
			get
			{
				return this._backGroundColor;
			}
			set
			{
				this._backGroundColor = value;
			}
		}

		#endregion 字体

		#region 外部线条

		public Color m_OutLineColor
		{
			get
			{
				return this._outLineColor;
			}
			set
			{
				this._outLineColor = value;
			}
		}

		public int m_OutLineWidth
		{
			get
			{
				return this._outLineWidth;
			}
			set
			{
				this._outLineWidth = value;
			}
		}

		#endregion 外部线条

		#region 缓存区

		/// <summary>
		/// 属性只读
		/// </summary>
		public byte[] m_NowByte
		{
			get
			{
				return this._nowByte;
			}
			//set
			//{
			//    this._bufferNow = value;
			//}
		}

		/// <summary>
		/// 属性只读
		/// </summary>
		public byte[] m_LastByte
		{
			get
			{
				return this._lastByte;
			}
			//set
			//{
			//    this._bufferLast = value;
			//}
		}

		/// <summary>
		/// 属性读写
		/// </summary>
		public bool m_IsNewByte
		{
			get
			{
				return this._isNewByte;
			}
			set
			{
				this._isNewByte = value;
			}
		}

		#endregion 缓存区

		#endregion 属性定义
	}
}