using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HexBoxLib
{
	public partial class HexBox : Control
	{
		#region 变量定义

		//---记录数据区域的数据结束的位置
		private int _endWidth = 0;

		//---记录数据区域的数据开始的位置
		private int _beginWidth = 0;

		//---记录数据区域每行的高度
		private int _beginHeight = 0;

		#endregion 变量定义

		#region 构造函数

		public HexBox()
		{
			#region	设置窗体的Style
			//---支持用户重绘窗体
			this.SetStyle(ControlStyles.UserPaint, true);
			//---在内存中先绘制界面
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			//---双缓冲，防止绘制时抖动
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.UpdateStyles();

			#endregion 构造函数

			#region 控件初始化
			//---获取控件的地址的宽度
			this.m_YAddrWidth = this.CalcSizeFontWidth("0x0000", this.m_Font) + this.m_YAddrBeginWidth;

			if (this.m_XAddrShowBits == XAddrShowBits.Bit16)
			{
				this.row_DisplayNum = 16;
			}
			else if (this.m_XAddrShowBits == XAddrShowBits.Bit32)
			{
				this.row_DisplayNum = 32;
			}
			else
			{
				this.row_DisplayNum = 16;
			}
			#endregion 控件初始化

			#region 光标

			//---设置鼠标位置属性
			this.m_MousePos = new POS();
			this.m_MousePos.iPos = -1;
			this.m_MousePos.iArea = -1;
			this.m_MousePos.bLeftPos = false;
			this.m_MousePos.bRightPos = false;

			#endregion 光标

			#region 初始化垂直滚动条
			//---初始化滑块
			this.m_VScrollBar = new VScrollBar();
			this.m_VScrollBar.Visible = false;
			this.m_VScrollBar.Enabled = false;
			this.m_VScrollBar.Width = this.m_ScrollWidth;
			this.m_VScrollBar.Minimum = 0;
			this.m_VScrollBar.Maximum = 0;
			//---滑块滑动事件处理
			m_VScrollBar.Scroll += new ScrollEventHandler(m_VScrollBar_Scroll);
			//---在当前控件中添加垂直滚动条
			this.Controls.Add(m_VScrollBar);

			#endregion 初始化垂直滚动条

			//强制设置输入法为英文输入模式
			this.ImeMode = ImeMode.Disable;

			//this.AddByteData(512);
		}

		#endregion

		#region 析够函数

		~HexBox()
		{
			if (this.m_IsCreateCaret)
			{
				HexBox.DestroyCaret();
			}
		}

		#endregion

		#region 窗体绘制函数

		/// <summary>
		/// 重新绘制窗体函数
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			//---设置窗体的背景色
			this.BackColor = this.m_BackgroundColor;
			//设置输入焦点
			this.Focus();

			//---绘制船体外部线条
			this.OnDrawOutLine(e);

			//---显示垂直滚动条
			if ((this._nowByte != null) && (this._nowByte.Length != 0))
			{
				this.VScrollBarDisplay();
			}

			//---绘制窗体标题栏
			if (this.m_ShowXAddr)
			{
				this.OnDrawXAddr(e);
			}

			//---绘制窗体地址栏
			if (this.m_ShowYAddr)
			{
				this.OnDrawYAddr(e);
			}

			//---绘制窗体数据栏
			this.OnDrawDataAddr(e);

			//---绘制数据地址选择框
			this.OnDrawYAddrSelected(e);

			//---绘制数据头选择框
			this.OnDrawXAddrSelected(e);
		}

		/// <summary>
		/// 绘制外部线条框
		/// </summary>
		/// <param name="e"></param>
		private void OnDrawOutLine(PaintEventArgs e)
		{
			//获取二位平面的X和Y坐标
			Point[] nowPoint = { new Point(0, 0),
								 new Point(this.Width, 0),
								 new Point(this.Width, this.Height),
								 new Point(0, this.Height),
								 new Point(0, 0)
								};
			//画笔
			Pen nowPen = new Pen(this.m_OutLineColor, this.m_OutLineWidth);

			//绘制线条
			e.Graphics.DrawLines(nowPen, nowPoint);
		}

		/// <summary>
		/// 绘制标题栏框
		/// </summary>
		/// <param name="e"></param>
		private void OnDrawXAddr(PaintEventArgs e)
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return;
			}
			else
			{
				//---获取标题栏的起始点
				Point nowPoint = new Point(this.m_OutLineWidth / 2, this.m_OutLineWidth / 2);

				//---计算标题栏的宽度
				int nowWidth = 0;
				//---判断垂直滚动条是否可见
				if (this.m_VScrollBar.Visible)
				{
					nowWidth = this.Width - this.m_OutLineWidth - this.m_ScrollWidth;
				}
				else
				{
					nowWidth = this.Width - this.m_OutLineWidth;
				}

				//---计算标题栏的高度
				int nowHeight = (this.m_XAddrHeight);

				//---获得字体的宽度
				int fontWidth = this.CalcSizeFontWidth();

				//---设置字体的起始位置
				int fontOffset = this.m_OutLineWidth / 2 + this.row_StaffWidth;

				//---判断是否显示地址栏
				if (this.m_ShowYAddr)
				{
					//---计算字体的大小
					SizeF sizefAdd;
					if (this.m_YAddrShowBits == YAddrShowBits.Bit2)
					{
						sizefAdd = CalcSizeFont("0x00", this.m_Font);
					}
					else if (this.m_YAddrShowBits == YAddrShowBits.Bit4)
					{
						sizefAdd = CalcSizeFont("0x0000", this.m_Font);
					}
					else if (this.m_YAddrShowBits == YAddrShowBits.Bit6)
					{
						sizefAdd = CalcSizeFont("0x000000", this.m_Font);
					}
					else
					{
						sizefAdd = CalcSizeFont("0x00000000", this.m_Font);
					}

					//---标题栏第一个字符的起始地址
					fontOffset += (int)sizefAdd.Width + this.m_YAddrBeginWidth;

					//---数据地址栏的宽度
					this.m_YAddrWidth = (int)sizefAdd.Width + this.m_YAddrBeginWidth;
				}
				//---获取坐标
				Point nowPointA = new Point();
				Point nowPointB = new Point();

				//---标题栏要填充的背景色
				Brush nowBrush = new SolidBrush(this.m_XAddrBackGroundColor);

				//---计算标题栏的操作区域
				Rectangle nowRectangle = new Rectangle(nowPoint.X, nowPoint.Y, nowWidth, nowHeight);

				//---绘制指定大小的矩形区域并填充指定的背景色
				e.Graphics.FillRectangle(nowBrush, nowRectangle);

				//---设置标题栏字体颜色
				nowBrush = new SolidBrush(this.m_XAddrFontColor);

				//---绘制标题栏
				for (int i = 0; i < this.row_DisplayNum; i++)
				{
					//---绘制数据
					string msg = i.ToString("X2");

					nowPointA.X = fontOffset;
					nowPointA.Y = nowHeight / 6;

					//---绘制字符串
					e.Graphics.DrawString(msg, this.m_Font, nowBrush, nowPointA);

					//---计算下一个数据的地址
					fontOffset += fontWidth + this.row_StaffWidth;
				}
				//---标题栏下划线的颜色
				Pen nowPen = new Pen(this.m_OutLineColor);

				//---获取第一个点的二维坐标
				nowPointA.X = this.column_StaffWidth + this.m_YAddrWidth - this.m_OutLineWidth / 2; //this.HexBoxOutLineWidth / 2; //---顶格显示
				nowPointA.Y = nowHeight - this.m_OutLineWidth / 2;

				//---获取的第二个点的二维坐标
				if (this.m_VScrollBar.Visible)
				{
					nowPointB.X = this.Width - this.m_ScrollWidth - this.m_OutLineWidth / 2;
				}
				else
				{
					nowPointB.X = this.Width - this.m_OutLineWidth / 2;
				}
				nowPointB.Y = nowPointA.Y;
				this._beginHeight = nowPointB.Y;
				//---绘制标题栏的下划线
				e.Graphics.DrawLine(nowPen, nowPointA, nowPointB);
			}
		}

		/// <summary>
		/// 绘制地址框
		/// </summary>
		/// <param name="e"></param>
		private void OnDrawYAddr(PaintEventArgs e)
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return;
			}
			else
			{
				//---获取起始点
				Point nowPoint = this.CalcYAddrBeginPosition();

				//---计算当前控件能够显示的最大行数
				int iMaxRowCount = this.CalcShowMaxRowNum();

				//---计算数据需要显示的行数
				int iTotalRowCount = this.CalcMaxRowNum();

				//---计算字体的高度
				int fonHeight = this.CalcSizeFontHeigth();

				//---计算实际起始写的位置
				int fontOffset = 0;

				//---计算字体的大小
				SizeF sizefAdd;
				if (this.m_YAddrShowBits == YAddrShowBits.Bit2)
				{
					sizefAdd = this.CalcSizeFont("0x00", this.m_Font);
				}
				else if (this.m_YAddrShowBits == YAddrShowBits.Bit4)
				{
					sizefAdd = this.CalcSizeFont("0x0000", this.m_Font);
				}
				else if (this.m_YAddrShowBits == YAddrShowBits.Bit6)
				{
					sizefAdd = this.CalcSizeFont("0x000000", this.m_Font);
				}
				else
				{
					sizefAdd = this.CalcSizeFont("0x00000000", this.m_Font);
				}

				//---数据宽度判断
				if (this.m_YAddrWidth < ((int)sizefAdd.Width + this.m_YAddrBeginWidth))
				{
					this.m_YAddrWidth = ((int)sizefAdd.Width + this.m_YAddrBeginWidth);
				}
				//---字体地址偏移
				fontOffset = this.m_YAddrWidth - (int)sizefAdd.Width;

				//---地址栏的背景色
				Brush backGroundBrush = new SolidBrush(this.m_YAddrBackGroundColor);

				//---矩形区域
				Rectangle nowRectangle;
				if (this.m_ShowXAddr)
				{
					nowRectangle = new Rectangle(nowPoint.X, nowPoint.Y, this.m_YAddrWidth, (this.Height - this.m_XAddrHeight - this.m_OutLineWidth));
				}
				else
				{
					nowRectangle = new Rectangle(nowPoint.X, nowPoint.Y, this.m_YAddrWidth, (this.Height - this.m_OutLineWidth));
				}
				//---填充背景色
				e.Graphics.FillRectangle(backGroundBrush, nowRectangle);

				//---设置地址栏字体色
				Brush fontbrush = new SolidBrush(this.m_YAddrFontColor);
				nowRectangle = new Rectangle();
				Point nowPointA = new Point();
				string strMsg = null;

				//---填充地址
				for (int ix = this.row_DisplayIndex; ix < this.row_DisplayIndex + iMaxRowCount; ix++)
				{
					//---数据长度超出
					if (iTotalRowCount <= ix)
					{
						break;
					}
					else
					{
						nowPointA.X = nowPoint.X + fontOffset;
						nowPointA.Y = nowPoint.Y + (ix - row_DisplayIndex) * (fonHeight + this.column_StaffWidth) + (fonHeight + this.column_StaffWidth) / (this.current_PositionOffset);

						if (this.m_YAddrShowBits == YAddrShowBits.Bit2)
						{
							strMsg = "0x" + (ix * this.row_DisplayNum).ToString("X2");
						}
						else if (this.m_YAddrShowBits == YAddrShowBits.Bit4)
						{
							strMsg = "0x" + (ix * this.row_DisplayNum).ToString("X4");
						}
						else if (this.m_YAddrShowBits == YAddrShowBits.Bit6)
						{
							strMsg = "0x" + (ix * this.row_DisplayNum).ToString("X6");
						}
						else
						{
							strMsg = "0x" + (ix * this.row_DisplayNum).ToString("X8");
						}
						//---绘制地址字符串
						e.Graphics.DrawString(strMsg, this.m_Font, fontbrush, nowPointA);
					}
				}

				//---绘制地址栏右侧的竖线
				Pen nowPen = new Pen(this.m_OutLineColor);
				//---获取第一个点的位置
				nowPoint.X = this.m_YAddrWidth + this.column_StaffWidth - this.m_OutLineWidth / 2;
				nowPoint.Y = this.m_XAddrHeight - this.m_OutLineWidth / 2;

				//---获取第二个点的位置
				nowPointA.X = nowPoint.X;
				nowPointA.Y = this.Height;

				this._endWidth = (int)nowPointA.X;
				//---绘制直线
				e.Graphics.DrawLine(nowPen, nowPoint, nowPointA);
			}
		}

		/// <summary>
		/// 绘制数据栏
		/// </summary>
		/// <param name="e"></param>
		private void OnDrawDataAddr(PaintEventArgs e)
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return;
			}
			else
			{
				//---获取起始地址点
				Point nowPointA = this.CalcDataAddrBeginPosition();

				//---计算字体的宽度
				int fontWidth = this.CalcSizeFontWidth();
				//---计算字体的高度
				int fontHeight = this.CalcSizeFontHeigth();

				//---计算当前控件能够显示的最大行数
				int iMaxRowCount = this.CalcShowMaxRowNum();

				//---计算数据需要显示的行数
				int iTotalRowCount = this.CalcMaxRowNum();

				//---数据栏的背景色
				Brush backGroundBrush = new SolidBrush(this.m_BackgroundColor);

				//---设置数据栏字体颜色
				Brush fontBrush = new SolidBrush(this.m_DataAddrFontColor);

				//---设置绘制数据区域的矩形图形
				Rectangle nowRectangle = new Rectangle();
				//---设置绘制区域
				Point nowPointB = new Point();

				string strMsg = null;
				int dataOffset = row_DisplayIndex * row_DisplayNum;
				int iHeight = nowPointA.Y + (fontHeight + column_StaffWidth) / (this.current_PositionOffset);

				for (int ix = this.row_DisplayIndex; ix < this.row_DisplayIndex + iMaxRowCount; ix++)
				{
					//---数据长度超出
					if (iTotalRowCount <= ix)
					{
						break;
					}
					else
					{
						nowPointB.X = nowPointA.X + this.row_StaffWidth;
						nowPointB.Y = iHeight;

						for (int iy = dataOffset; iy < dataOffset + row_DisplayNum; iy++)
						{
							if (iy >= this._nowByte.Length)
							{
								break;
							}

							//---刷新背景色
							nowRectangle.X = nowPointB.X;
							nowRectangle.Y = nowPointB.Y = iHeight;
							nowRectangle.Width = fontWidth;
							nowRectangle.Height = fontHeight;
							//---判断是否通过背景色的进行数据不同的标注
							if ((this._nowByte[iy] != this._lastByte[iy]) || (this._isShowDifference == true))
							{
								backGroundBrush = new SolidBrush(Color.Yellow);
								e.Graphics.FillRectangle(backGroundBrush, nowRectangle);
								backGroundBrush = new SolidBrush(Color.White);
								this._isNewByte = true;
							}
							else
							{
								e.Graphics.FillRectangle(backGroundBrush, nowRectangle);
							}

							//---显示字符串信息
							strMsg = this._nowByte[iy].ToString("X2");
							e.Graphics.DrawString(strMsg, this.m_Font, fontBrush, nowPointB);
							nowPointB.X += fontWidth + this.row_StaffWidth;
						}
						this._beginWidth = (int)nowPointB.X;
						//---数据偏移量
						dataOffset += this.row_DisplayNum;
						iHeight += fontHeight + this.column_StaffWidth;
					}
				}
			}
		}

		/// <summary>
		/// 绘制数据地址选择框
		/// </summary>
		/// <param name="e"></param>
		private void OnDrawYAddrSelected(PaintEventArgs e)
		{
			if (this.m_ShowYAddr && (this.row_SelectedIndex != -1) && (this.m_MousePos.iArea == 1))
			{
				//---判定起始点
				Point nowPointA = this.CalcYAddrBeginPosition();

				//---计算宽度
				int fontWidth = this.CalcSizeFontWidth();

				//计算字体的高度
				int fontHeight = this.CalcSizeFontHeigth();

				//---矩形区域
				Rectangle nowRectangle = new Rectangle();
				nowRectangle.X = nowPointA.X;
				nowRectangle.Y = nowPointA.Y + this.row_SelectedIndex * (fontHeight + this.column_StaffWidth);

				nowRectangle.Width = this.m_YAddrWidth + (this.m_OutLineWidth);

				nowRectangle.Height = fontHeight + this.column_StaffWidth;

				//---区域矩形背景填充
				Brush backGroundBrush = new SolidBrush(Color.FromArgb(60, Color.Black));
				e.Graphics.FillRectangle(backGroundBrush, nowRectangle);

				//---外边框线条
				e.Graphics.DrawRectangle(new Pen(Color.DarkGray, 1), nowRectangle);
			}
			else
			{
				return;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="e"></param>
		private void OnDrawXAddrSelected(PaintEventArgs e)
		{
			if (this.m_ShowXAddr && (this.row_SelectedIndex != -1))
			{
				//---数据选择的列号
				int currentColumn = this.CalcCurrentColumnIndex();

				//---判断列号是否合法
				if (currentColumn == -1)
				{
					return;
				}
				else
				{
					//---获取起始地址
					Point pointA;
					if (this.m_ShowYAddr)
					{
						pointA = new Point(this.m_YAddrWidth + this.column_StaffWidth - this.m_OutLineWidth / 2, this.m_OutLineWidth / 2);
					}
					else
					{
						pointA = new Point(this.m_OutLineWidth / 2, this.m_OutLineWidth / 2);
					}

					//---计算宽度
					int fontWidth = this.CalcSizeFontWidth();

					//---计算字体的高度
					int fontHeight = this.CalcSizeFontHeigth();

					Rectangle nowRectangle = new Rectangle();

					if (this.m_MousePos.bLeftPos)
					{
						nowRectangle.X = pointA.X + currentColumn * (fontWidth + this.row_StaffWidth) + (fontWidth - 3) / 2;
					}
					if (this.m_MousePos.bRightPos)
					{
						nowRectangle.X = pointA.X + currentColumn * (fontWidth + this.row_StaffWidth) + fontWidth - 2;
					}

					nowRectangle.Y = pointA.Y;

					nowRectangle.Width = (fontWidth + 1) / 2;

					nowRectangle.Height = this.m_XAddrHeight - this.m_OutLineWidth;

					//---区域矩形背景填充
					Brush backGroundBrush = new SolidBrush(Color.FromArgb(60, Color.Black));
					e.Graphics.FillRectangle(backGroundBrush, nowRectangle);

					//---外边框线条
					e.Graphics.DrawRectangle(new Pen(Color.DarkGray, 1), nowRectangle);
				}
			}
			else
			{
				return;
			}
		}

		#endregion

		#region 地址栏的函数

		/// <summary>
		/// 计算地址栏的起始位置
		/// </summary>
		/// <returns></returns>
		private Point CalcYAddrBeginPosition()
		{
			Point _return;

			if (this.m_ShowXAddr)
			{
				_return = new Point(this.m_OutLineWidth / 2, this.m_XAddrHeight - this.m_OutLineWidth / 2);
			}
			else
			{
				_return = new Point(this.m_OutLineWidth / 2, this.m_OutLineWidth / 2);
			}

			return _return;
		}

		#endregion

		#region 数据栏

		/// <summary>
		/// 计算当前控件能够显示的最大行数
		/// </summary>
		/// <returns></returns>
		private int CalcShowMaxRowNum()
		{
			//---获取字体的宽度及长度
			SizeF sizef = this.CalcSizeFont();

			//---字体的宽度和长度转换成整型
			int fontWidth = (int)(sizef.Width);
			int fontHeight = (int)(sizef.Height);

			//---计算控件最大可显示的行数
			int iHeight = 0;

			//---是否显示标题栏
			if (this.m_ShowXAddr)
			{
				iHeight = this.Height - this.m_OutLineWidth * 2 - this.m_XAddrHeight;
			}
			else
			{
				iHeight = this.Height - this.m_OutLineWidth * 2;
			}
			return (int)(iHeight / (fontHeight + this.column_StaffWidth));
		}

		/// <summary>
		/// 数据地址栏显示的总行数
		/// </summary>
		/// <returns></returns>
		private int CalcMaxRowNum()
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return -1;
			}
			//---计算实际的行数
			int dataRow = 0;
			dataRow = this._nowByte.Length / this.row_DisplayNum;

			//---不能整除则加一
			if ((this._nowByte.Length % this.row_DisplayNum) != 0)
			{
				dataRow += 1;
			}
			return dataRow;
		}

		/// <summary>
		/// 数据栏的起始位置
		/// </summary>
		/// <returns></returns>
		private Point CalcDataAddrBeginPosition()
		{
			Point pointA;
			int iWidth = 0;
			int iHeight = 0;

			//---判定是否显示标题栏
			if (this.m_ShowXAddr)
			{
				iHeight = this.m_XAddrHeight - this.m_OutLineWidth / 2;
			}
			else
			{
				iHeight = this.m_OutLineWidth / 2;
			}
			//---判定是否显示地址栏
			if (this.m_ShowYAddr)
			{
				iWidth = this.m_YAddrWidth + this.m_OutLineWidth / 2;
			}
			else
			{
				iWidth = this.m_OutLineWidth / 2;
			}

			pointA = new Point(iWidth, iHeight);

			return pointA;
		}

		/// <summary>
		/// 计算当前显示的列的列号
		/// </summary>
		/// <returns></returns>
		private int CalcCurrentColumnIndex()
		{
			int _return = 0;
			if (this.m_MousePos.iPos >= 0)
			{
				_return = this.m_MousePos.iPos % this.row_DisplayNum;
			}
			else
			{
				//_return = -1;
				_return = 0;
			}

			return _return;
		}

		/// <summary>
		/// 获取数据所在行的起始信息
		/// </summary>
		/// <returns></returns>
		private Point CalcRowBeginPosition()
		{
			Point pointA;
			int iOffset = 0;

			//---计算字体的高度
			int fontHeight = this.CalcSizeFontHeigth();

			if (this.m_ShowYAddr)
			{
				iOffset = this.m_OutLineWidth / 2 + this.m_YAddrWidth;
			}
			else
			{
				iOffset = this.m_OutLineWidth / 2;
			}

			int height = 0;
			if (this.m_ShowXAddr)
			{
				height = this.m_OutLineWidth / 2 + this.m_XAddrHeight + this.row_SelectedIndex * (fontHeight + this.column_StaffWidth);
			}
			else
			{
				height = this.m_OutLineWidth / 2 + this.row_SelectedIndex * (fontHeight + this.column_StaffWidth);
			}

			pointA = new Point(iOffset, height);

			return pointA;
		}

		#endregion

		#region 数据处理

		/// <summary>
		/// 获取指定位置的数据
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public byte GetDataByPosition(int pos)
		{
			if (pos > this._nowByte.Length)
			{
				return 0;
			}
			else
			{
				return this._nowByte[pos];
			}
		}

		/// <summary>
		/// 添加数据
		/// </summary>
		/// <param name="length"></param>
		public void AddByteData(long length)
		{
			//----数据赋初值为0xFF
			HexBox.memset(ref this._nowByte, length, 0xFF);
			HexBox.memset(ref this._lastByte, length, 0xFF);
			this._isShowDifference = false;
			this._isNewByte = false;

			//---判断地址
			if ((length < 0x100))
			{
				this.m_YAddrShowBits = YAddrShowBits.Bit2;
			}
			else if (length < 0x10000)
			{
				this.m_YAddrShowBits = YAddrShowBits.Bit4;
			}
			else if (length <= 0x1000000)
			{
				this.m_YAddrShowBits = YAddrShowBits.Bit6;
			}
			else
			{
				this.m_YAddrShowBits = YAddrShowBits.Bit8;
			}

			//---创建并显示光标
			this.OnCreateAndShowCaret();

			//---滑块指向开始
			this.m_VScrollBar.Value = 0;

			//---置位当前显示的行号位0
			this.row_DisplayIndex = 0;

			//---显示垂直滚动条
			this.VScrollBarDisplay();

			//---重新绘制窗体
			this.Invalidate();
		}

		/// <summary>
		/// 添加数据
		/// </summary>
		/// <param name="buffer"></param>
		public void AddByteData(byte[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			else
			{
				//---重新置位缓存区的大小
				Array.Resize<byte>(ref this._nowByte, buffer.Length);
				Array.Resize<byte>(ref this._lastByte, buffer.Length);

				//---数组拷贝
				Array.Copy(buffer, this._nowByte, buffer.Length);
				Array.Copy(buffer, this._lastByte, buffer.Length);

				//---创建并显示光标
				this.OnCreateAndShowCaret();

				//---滑块指向开始
				this.m_VScrollBar.Value = 0;

				//---置位当前显示的行号位0
				this.row_DisplayIndex = 0;

				//---显示垂直滚动条
				this.VScrollBarDisplay();

				//---重新绘制窗体
				this.Invalidate();
			}
		}

		/// <summary>
		/// 添加数据
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="isShowDifference"></param>
		public void AddByteData(byte[] buffer, bool isShowDifference)
		{
			if (buffer == null)
			{
				return;
			}
			else
			{
				if (this._nowByte.Length != buffer.Length)
				{
					//---重新置位缓存区的大小
					Array.Resize<byte>(ref this._nowByte, buffer.Length);
					Array.Resize<byte>(ref this._lastByte, buffer.Length);
				}
				//---数组拷贝
				Array.Copy(buffer, this._nowByte, buffer.Length);
				//---判断是否显示不同
				if (isShowDifference != true)
				{
					Array.Copy(buffer, this._lastByte, buffer.Length);
					this._isShowDifference = true;
				}

				//---创建并显示光标
				this.OnCreateAndShowCaret();

				//---滑块指向开始
				this.m_VScrollBar.Value = 0;

				//---置位当前显示的行号位0
				this.row_DisplayIndex = 0;

				//---显示垂直滚动条
				this.VScrollBarDisplay();

				//---重新绘制窗体
				this.Invalidate();
			}
		}

		/// <summary>
		/// 添加数据
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		public void AddByteData(byte[] buffer, int index, int length)
		{
			//--数据的合法性
			if ((buffer == null) || (length > buffer.Length))
			{
				return;
			}
			//---数据长度的合法性
			if ((index + length) > this._nowByte.Length)
			{
				return;
			}
			//---数据拷贝
			Array.Copy(buffer, 0, this._nowByte, index, length);
			Array.Copy(this._nowByte, this._lastByte, this._nowByte.Length);

			//---创建并显示光标
			this.OnCreateAndShowCaret();

			//---滑块指向开始
			this.m_VScrollBar.Value = 0;

			//---置位当前显示的行号位0
			this.row_DisplayIndex = 0;

			//---显示垂直滚动条
			this.VScrollBarDisplay();

			//重新绘制窗体
			this.Invalidate();
		}

		/// <summary>
		/// 得到字符串数据
		/// </summary>
		/// <returns></returns>
		public string[] GetStringData()
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return null;
			}
			else
			{
				List<string> _return = new List<string>();

				for (long i = 0; i < this._nowByte.Length; i++)
				{
					_return.Add(this._nowByte[i].ToString("X2"));
				}
				return _return.ToArray();
			}
		}

		/// <summary>
		/// 得到字节数据
		/// </summary>
		/// <returns></returns>
		public byte[] GetByteArray()
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return null;
			}
			else
			{
				return this._nowByte;
			}
		}

		#endregion

		#region 垂直滚动条

		/// <summary>
		/// 垂直滚动条处理函数
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		private void m_VScrollBar_Scroll(object obj, ScrollEventArgs e)
		{
			if (this.row_DisplayIndex != e.NewValue)
			{
				this.row_DisplayIndex = e.NewValue;

				//---判断光标是否隐藏
				if (!this.m_IsHideCaret)
				{
					//---隐藏光标
					this.OnHideCaret();
					this.m_IsHideCaret = true;
				}
				//---复位鼠标信息
				this.m_MousePos.iPos = -1;
				this.m_MousePos.iArea = -1;
				this.m_MousePos.bLeftPos = false;
				this.m_MousePos.bRightPos = false;
				//---绘制船体
				this.Invalidate();
			}
		}

		/// <summary>
		/// 垂直滚动条显示函数
		/// </summary>
		private void VScrollBarDisplay()
		{
			//---获取字体大小信息
			SizeF fontSizef = this.CalcSizeFont();

			int fontWidth = (int)fontSizef.Width;
			int fontHeight = (int)fontSizef.Height;

			//---计算当前控件能够显示的最大行数
			int iMaxRowCount = this.CalcShowMaxRowNum();

			//---计算数据需要显示的行数
			int iTotalRowCount = this.CalcMaxRowNum();

			if (iTotalRowCount > iMaxRowCount)
			{
				this.m_VScrollBar.Visible = true;
				this.m_VScrollBar.Enabled = true;
				//---设置控件位置
				this.m_VScrollBar.Location = new Point((this.Width - this.m_OutLineWidth - this.m_ScrollWidth), this.m_OutLineWidth);

				this.m_VScrollBar.Size = new Size(this.m_ScrollWidth, (this.Height - this.m_OutLineWidth * 2));

				//---设置控件的最大值
				this.m_VScrollBar.LargeChange = 1;
				this.m_VScrollBar.Maximum = iTotalRowCount - iMaxRowCount;
			}
			else
			{
				this.m_VScrollBar.Visible = false;
				this.m_VScrollBar.Enabled = false;
			}
		}

		#endregion

		#region 光标位置处理

		/// <summary>
		/// 重新设置并显示光标
		/// </summary>
		public void OnCreateAndShowCaret()
		{
			//---获取焦点
			this.Focus();

			this.m_IsCreateCaret = false;

			//---判断选中行是否合法
			if (this.row_SelectedIndex == -1)
			{
				return;
			}
			//---数据选择的列号
			int currentColumn = this.CalcCurrentColumnIndex();

			//---判断当前列号是否合法
			if (currentColumn == -1)
			{
				return;
			}

			//---计算宽度
			int fontWidth = this.CalcSizeFontWidth();

			//---计算字体的高度
			int fontHeight = this.CalcSizeFontHeigth();

			//---当前起始位置
			Point pointA = this.CalcDataAddrBeginPosition();

			if (this.m_MousePos.bLeftPos)
			{
				pointA.X = pointA.X + currentColumn * (fontWidth + this.row_StaffWidth) + (fontWidth) / 2;
			}
			if (this.m_MousePos.bRightPos)
			{
				pointA.X = pointA.X + currentColumn * (fontWidth + this.row_StaffWidth) + fontWidth;
			}
			pointA.Y += (this.row_SelectedIndex) * (fontHeight + this.column_StaffWidth);

			this.OnCreateCaret();

			//---设置光标
			HexBox.SetCaretPos(pointA.X, pointA.Y);
			//---显示光标
			HexBox.ShowCaret(this.Handle);
		}

		/// <summary>
		/// 隐藏光标,,并设置相关位置
		/// </summary>
		private void OnHideCaret()
		{
			if (!this.m_IsHideCaret)
			{
				HexBox.HideCaret(this.Handle);
				this.m_IsHideCaret = true;
			}
			this.m_MousePos.iPos = -1;
			this.m_MousePos.iArea = -1;
			this.m_MousePos.bLeftPos = false;
			this.m_MousePos.bRightPos = false;
		}

		/// <summary>
		/// 创建光标
		/// </summary>
		private void OnCreateCaret()
		{
			//如果没有创建Caret，则创建
			if (!this.m_IsCreateCaret)
			{
				this.m_IsCreateCaret = true;
				HexBox.CreateCaret(this.Handle, IntPtr.Zero, (int)(m_Font.Size - 0.5), m_Font.Height + this.column_StaffWidth);
			}
		}

		#endregion

		#region 消息处理

		/// <summary>
		/// 消息处理函数
		/// 该函数不处理WM_ERASEBKGRND消息
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x00000014)
			{
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		#region 按键处理------对输入的按键值处理

		/// <summary>
		/// 处理键盘输入键消息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (this.m_MousePos.iPos != -1 && this.m_MousePos.iArea != -1)
			{
				if (((keyData >= Keys.D0) && (keyData <= Keys.F)) || (keyData >= Keys.NumPad0 && keyData <= Keys.NumPad9))
				{
					//---设置焦点
					this.Focus();
					int c = 0;
					int b = 0;
					if (this.m_MousePos.iPos < this.m_NowByte.Length)
					{
						if (this.m_MousePos.bLeftPos)
						{
							c = (byte)(this.m_NowByte[m_MousePos.iPos] & 0x0F);
							b = this.GetKeyBoardInputValue((byte)keyData);
						}
						if (this.m_MousePos.bRightPos)
						{
							b = (byte)((this.m_NowByte[m_MousePos.iPos] & 0xF0) >> 4);
							c = this.GetKeyBoardInputValue((byte)keyData);
						}
						this.m_NowByte[m_MousePos.iPos] = (byte)((b << 4) | c);
						//光标向右移动
						this.OnCaretMove_Right();
					}
				}
				else
				{
					switch (keyData)
					{
						case Keys.Up:
							this.OnCaretMove_Up();
							break;

						case Keys.Down:
							this.OnCaretMove_Down();
							break;

						case Keys.Left:
							this.OnCaretMove_Left();
							break;

						case Keys.Right:
							this.OnCaretMove_Right();
							break;

						case Keys.Enter:
							string temp = keyData.ToString();
							break;
					}
				}
				return true;
				//不做基类的处理，不然在有多个控件的时候会发生问题
				//return base.ProcessCmdKey( ref msg, keyData );
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 获取键盘的输入值
		/// 16进制的数据和数字小键盘的数据
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		private byte GetKeyBoardInputValue(byte b)
		{
			if ('0' <= b && b <= '9')
			{
				return (byte)(b - '0');
			}
			else if ('A' <= b && b <= 'F')
			{
				return (byte)(b - 'A' + 10);
			}
			else if (97 <= b && b <= 105)
			{
				return (byte)(b - 97 + 1);
			}
			else
			{
				return 0;
			}
		}

		#endregion

		#region 鼠标的操作

		/// <summary>
		/// 鼠标按下的处理-----用于获取光标的位置------显示光标的信息
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return;
			}
			else
			{
				if (e.Button == MouseButtons.Left)
				{
					//---获取焦点
					this.Focus();
					//---记录鼠标的坐标
					Point mousePoint = new Point(e.X, e.Y);

					//---获取地址栏的起始位置
					Point pointA = this.CalcYAddrBeginPosition();

					//---计算字体的宽度
					int fontWidth = this.CalcSizeFontWidth();

					//---计算字体的高度
					int fontHeight = this.CalcSizeFontHeigth();

					//---计算当前控件能够显示的最大行数
					int iMaxRowCount = this.CalcShowMaxRowNum();

					//---计算数据需要显示的行数
					int iTotalRowCount = this.CalcMaxRowNum();

					//---判断鼠标左键是否是右侧的空白位置
					if ((mousePoint.X < this._beginHeight) || (mousePoint.X > (this._beginWidth - 10)))
					{
						return;
					}

					//---判定是否在当前行中
					Rectangle nowRectangle = new Rectangle();

					Region nowRegion;

					//---高度
					int iHeight = pointA.Y;

					#region 鼠标位于地址区域	-----获取当前选中的行号

					for (int ix = this.row_DisplayIndex; ix < this.row_DisplayIndex + iMaxRowCount; ix++)
					{
						if (iTotalRowCount <= ix)
						{
							//---表明没有选中行
							this.row_SelectedIndex = -1;

							break;
						}
						else
						{
							//---计算矩形区域的尺寸
							nowRectangle.X = pointA.X;
							nowRectangle.Y = iHeight;
							nowRectangle.Width = this.Width - this.m_OutLineWidth;
							nowRectangle.Height = fontHeight + this.column_StaffWidth;

							//---指示由矩形和由路径构成的图形形状的内部
							nowRegion = new Region(nowRectangle);

							//---判断是否找到光标的位置
							if (nowRegion.IsVisible(mousePoint))
							{
								//---表明选中了某一行
								this.row_SelectedIndex = ix - this.row_DisplayIndex;
								//---重新绘制控件
								this.Invalidate();
								break;
							}
							iHeight += fontHeight + this.column_StaffWidth;
						}
					}
					//---判断是否是本行被选中
					if (this.row_SelectedIndex == -1)
					{
						//---没有选中行
						return;
					}
					#endregion

					#region 鼠标位于数据区域-----用于创建光标

					Point poinaB;

					int dataOffset = 0;

					if (this.m_ShowYAddr)
					{
						dataOffset = this.m_OutLineWidth / 2 + this.m_YAddrWidth;
					}
					else
					{
						dataOffset = this.m_OutLineWidth / 2;
					}
					poinaB = new Point(dataOffset, pointA.Y);

					//---计算位置
					nowRectangle.X = poinaB.X;
					nowRectangle.Y = poinaB.Y + this.row_SelectedIndex * (fontHeight + this.column_StaffWidth);
					nowRectangle.Width = this.row_DisplayNum * (this.row_StaffWidth + fontWidth);

					nowRectangle.Height = fontHeight + this.column_StaffWidth;

					//---断鼠标左键是否是左侧的空白位置
					if (mousePoint.X < this._endWidth)
					{
						//---创建光标
						this.OnCreateCaret();
						//---设置光标
						HexBox.SetCaretPos(this._endWidth + 8, nowRectangle.Y);
						//---显示光标
						HexBox.ShowCaret(this.Handle);

						this.m_IsHideCaret = false;
						this.m_MousePos.iPos = (this.row_DisplayIndex + this.row_SelectedIndex) * this.row_DisplayNum + 0;
						this.m_MousePos.iArea = 1;
						this.m_MousePos.bLeftPos = true;
						this.m_MousePos.bRightPos = false;
						return;
					}

					nowRegion = new Region(nowRectangle);
					//鼠标在指定的区域内
					if (nowRegion.IsVisible(mousePoint))
					{
						//---创建光标
						this.OnCreateCaret();
						int iDataOffset = nowRectangle.X + this.row_StaffWidth;
						int iDataHeight = nowRectangle.Y;

						//---遍历查找光标的位置
						for (int iy = 0; iy < this.row_DisplayNum; iy++)
						{
							nowRectangle.X = iDataOffset;
							nowRectangle.Y = iDataHeight;
							nowRectangle.Width = fontWidth / 2;
							nowRectangle.Height = fontHeight + this.column_StaffWidth;
							nowRegion = new Region(nowRectangle);
							//---数据的左边
							if (nowRegion.IsVisible(mousePoint))
							{
								//---设置光标
								HexBox.SetCaretPos(nowRectangle.X, nowRectangle.Y);
								//---显示光标
								HexBox.ShowCaret(this.Handle);

								this.m_IsHideCaret = false;
								this.m_MousePos.iPos = (this.row_DisplayIndex + this.row_SelectedIndex) * this.row_DisplayNum + iy;
								this.m_MousePos.iArea = 1;
								this.m_MousePos.bLeftPos = true;
								this.m_MousePos.bRightPos = false;
								break;
							}
							nowRectangle.X += (fontWidth + 1) / 2;
							nowRectangle.Width = (fontWidth + 3) / 2 + this.row_StaffWidth;
							nowRegion = new Region(nowRectangle);
							//---数据的右边
							if (nowRegion.IsVisible(mousePoint))
							{
								//---设置光标
								HexBox.SetCaretPos(nowRectangle.X, nowRectangle.Y);
								//---显示光标
								HexBox.ShowCaret(this.Handle);

								this.m_IsHideCaret = false;
								this.m_MousePos.iPos = (this.row_DisplayIndex + this.row_SelectedIndex) * this.row_DisplayNum + iy;
								this.m_MousePos.iArea = 1;
								this.m_MousePos.bLeftPos = false;
								this.m_MousePos.bRightPos = true;

								break;
							}
							iDataOffset += fontWidth + this.row_StaffWidth;
						}
						//---如果点击区域超过最大值，则不显示
						if (this.m_MousePos.iPos != -1 && this.m_MousePos.iPos >= this._nowByte.Length)
						{
							this.OnHideCaret();
						}
						//---添加---注销
						nowRegion.Dispose();
					}
					else
					{
						this.OnHideCaret();
					}

					#endregion
				}
			}
			//处理基类鼠标按下的事件
			//base.OnMouseDown( e );
		}

		/// <summary>
		/// 滑轮滚动处理
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			//表明像下滚动
			if (e.Delta == -120)
			{
				if (this.row_DisplayIndex < this.m_VScrollBar.Maximum)
				{
					if (this.m_VScrollBar.Enabled)
					{
						this.row_DisplayIndex += 1;
						this.m_VScrollBar.Value = this.row_DisplayIndex;
						this.Invalidate();
					}
				}
			}
			//---表明向上滚动
			else if (e.Delta == 120)
			{
				if (this.row_DisplayIndex > 0)
				{
					row_DisplayIndex -= 1;
					this.m_VScrollBar.Value = this.row_DisplayIndex;
					this.Invalidate();
				}
				else
				{
					this.row_DisplayIndex = 0;
				}
			}
		}

		/// <summary>
		/// 鼠标按键弹起
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if ((this._nowByte == null) || (this._nowByte.Length == 0))
			{
				return;
			}
			//---获取焦点
			this.Focus();

			//处理基类的按键释放
			//base.OnMouseUp(e);
		}

		#endregion

		#region 光标移动

		/// <summary>
		///
		/// </summary>
		private void OnCaretMove_Right()
		{
			if ((this.m_MousePos.iPos != -1) && (this.m_MousePos.iArea != -1))
			{
				//---设置输入点
				this.Focus();
				//---光标在数据区域
				if (this.m_MousePos.iArea == 1)
				{
					//---计算字体的宽度
					int fonWidth = this.CalcSizeFontWidth();

					//---计算字体的高度
					int fontHeight = this.CalcSizeFontHeigth();

					//---获取当前的列信息
					int colPosition = this.CalcCurrentColumnIndex();

					//---获取数据所在行的起始信息---仅仅是起始信息
					Point pointA = this.CalcRowBeginPosition();

					//---用于计算光标的二维坐标
					Point pointB = new Point();

					//---表明在最后一个字节
					if (colPosition == (this.row_DisplayNum - 1))
					{
						//---表明在字节的右部
						if (this.m_MousePos.bRightPos)
						{
							//---计算控件最大可显示的行数
							int iMaxDataRow = this.CalcShowMaxRowNum();

							//---计算实际的行数
							int iDataRow = this.CalcMaxRowNum();

							//---判断选择数据所在的行号-----从1开始
							if (this.row_SelectedIndex < (iMaxDataRow - 1))
							{
								this.row_SelectedIndex++;

								//---为了避免光标在最后一个位置,依然能够移动位置
								if (this.row_SelectedIndex != iDataRow)
								{
									pointB.X = pointA.X + this.row_StaffWidth;
									pointB.Y = pointA.Y + fontHeight + this.column_StaffWidth - 2;
									this.m_MousePos.iPos += 1;

									//---设置光标的位置;同时置位光标的位置标志
									this.m_MousePos.bRightPos = false;
									this.m_MousePos.bLeftPos = true;
									HexBox.SetCaretPos(pointB.X, pointB.Y);
								}
								else
								{
									//---保持光标位置不变，选择的列号页不变
									this.row_SelectedIndex -= 1;

									//---最后一行数据的最后一个数据---位置信息保留
									this.m_MousePos.bRightPos = true;
									this.m_MousePos.bLeftPos = false;
								}

								//---重新绘制窗体
								this.Invalidate();
								return;
							}
							else if ((this.row_SelectedIndex == (iMaxDataRow - 1) && (iDataRow - this.row_DisplayIndex - iMaxDataRow) > 0))
							{
								this.row_DisplayIndex += 1;
								this.m_VScrollBar.Value = this.row_DisplayIndex;
								this.Invalidate();

								pointB.X = pointA.X + this.row_StaffWidth;
								pointB.Y = pointA.Y - 2;
								this.m_MousePos.iPos += 1;
								this.m_MousePos.bRightPos = false;
								this.m_MousePos.bLeftPos = true;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								return;
							}
							//---如果本窗体的最后一行且是最后一个数据，则如下处理
							else if ((this.row_SelectedIndex == (iMaxDataRow - 1) && (iDataRow - this.row_DisplayIndex - iMaxDataRow) == 0))
							{
								//SetCaretPos( pt.X, pt.Y );
								this.Invalidate();
								return;
							}
						}

						//---表明在字节的左部
						if (this.m_MousePos.bLeftPos)
						{
							//---用于显示光标位置
							pointB.X = pointA.X + colPosition * (fonWidth + this.row_StaffWidth) + this.row_StaffWidth + (fonWidth) / 2;
							pointB.Y = pointA.Y - 2;
							this.m_MousePos.bRightPos = true;
							this.m_MousePos.bLeftPos = false;
							HexBox.SetCaretPos(pointB.X, pointB.Y);
							this.Invalidate();
							return;
						}
					}
					else
					{
						//---表明在字节的右部
						if (this.m_MousePos.bRightPos)
						{
							//---判定是否在最后一个字节
							if ((this.m_MousePos.iPos + 1) >= (this.m_NowByte.Length))
							{
								return;
							}

							pointB.X = pointA.X + (colPosition + 1) * (fonWidth + this.row_StaffWidth) + this.row_StaffWidth;
							pointB.Y = pointA.Y - 2;
							this.m_MousePos.iPos += 1;
							this.m_MousePos.bRightPos = false;
							this.m_MousePos.bLeftPos = true;
							HexBox.SetCaretPos(pointB.X, pointB.Y);
							this.Invalidate();
							return;
						}

						//---表明在字节的左部
						if (this.m_MousePos.bLeftPos)
						{
							pointB.X = pointA.X + colPosition * (fonWidth + this.row_StaffWidth) + this.row_StaffWidth + (fonWidth) / 2;
							pointB.Y = pointA.Y - 2;
							this.m_MousePos.bRightPos = true;
							this.m_MousePos.bLeftPos = false;
							HexBox.SetCaretPos(pointB.X, pointB.Y);
							this.Invalidate();
							return;
						}
					}
				}
			}
		}

		/// <summary>
		/// 光标左移
		/// </summary>
		private void OnCaretMove_Left()
		{
			if (this.m_MousePos.iPos != -1 && this.m_MousePos.iArea != -1)
			{
				this.Focus();

				//---表明在Data区
				if (this.m_MousePos.iArea == 1)
				{
					//---获取字体的宽度及长度
					int fontWidth = this.CalcSizeFontWidth();
					int fontHeight = this.CalcSizeFontHeigth();

					//---查找在第几列
					int colPosition = this.CalcCurrentColumnIndex();

					//---获取所在行的启动信息
					Point pointA = this.CalcRowBeginPosition();

					//---表示在每行的起始位置
					Point pointB = new Point();
					if (colPosition == 0)
					{
						//---表明在第一个字节的右部
						if (this.m_MousePos.bRightPos)
						{
							pointB.X = pointA.X + this.row_StaffWidth;
							pointB.Y = pointA.Y - 2;
							this.m_MousePos.bRightPos = false;
							this.m_MousePos.bLeftPos = true;
							HexBox.SetCaretPos(pointB.X, pointB.Y);

							this.Invalidate();
							return;
						}
						//---表明在第一个字节的左部
						if (this.m_MousePos.bLeftPos)
						{
							if (this.row_SelectedIndex > 0)
							{
								this.row_SelectedIndex -= 1;

								pointB.X = pointA.X + row_DisplayNum * (fontWidth + this.row_StaffWidth) - (fontWidth) / 2;
								pointB.Y = pointA.Y - fontHeight - this.column_StaffWidth - 2;
								this.m_MousePos.iPos -= 1;
								this.m_MousePos.bRightPos = true;
								this.m_MousePos.bLeftPos = false;
								HexBox.SetCaretPos(pointB.X, pointB.Y);

								this.Invalidate();
								return;
							}
							else if (this.row_SelectedIndex == 0 && this.row_DisplayIndex > 0)
							{
								this.row_SelectedIndex = 0;
								this.row_DisplayIndex -= 1;

								this.m_VScrollBar.Value = this.row_DisplayIndex;
								this.Invalidate();

								pointB.X = pointA.X + row_DisplayNum * (fontWidth + this.row_StaffWidth) - (fontWidth) / 2;
								pointB.Y = pointA.Y;
								m_MousePos.iPos -= 1;
								this.m_MousePos.bRightPos = true;
								this.m_MousePos.bLeftPos = false;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.Invalidate();
								return;
							}
						}
					}
					//---表示不在每行的起始位置
					else
					{
						//---表明在字节的右部
						if (this.m_MousePos.bRightPos)
						{
							pointB.X = pointA.X + colPosition * (fontWidth + this.row_StaffWidth) + this.row_StaffWidth;
							pointB.Y = pointA.Y - 2;
							this.m_MousePos.bRightPos = false;
							this.m_MousePos.bLeftPos = true;
							HexBox.SetCaretPos(pointB.X, pointB.Y);
							this.Invalidate();
							return;
						}

						//---表明在字节的左部
						if (this.m_MousePos.bLeftPos)
						{
							pointB.X = pointA.X + (colPosition - 1) * (fontWidth + this.row_StaffWidth) + this.row_StaffWidth + fontWidth - (fontWidth) / 2;
							pointB.Y = pointA.Y - 2;
							this.m_MousePos.iPos -= 1;
							this.m_MousePos.bRightPos = true;
							this.m_MousePos.bLeftPos = false;
							HexBox.SetCaretPos(pointB.X, pointB.Y);
							this.Invalidate();
							return;
						}
					}
				}
			}
		}

		/// <summary>
		/// 光标上移
		/// </summary>
		private void OnCaretMove_Up()
		{
			if (m_MousePos.iPos != -1 && m_MousePos.iArea != -1)
			{
				//---表明在Data区
				if (m_MousePos.iArea == 1)
				{
					//---获取字体的宽度及长度
					int i_font_width = this.CalcSizeFontWidth();
					int i_font_height = this.CalcSizeFontHeigth();

					//---查找在第几列
					int iColumn = this.CalcCurrentColumnIndex();

					//---获取所在行的启动信息
					Point pointA = this.CalcRowBeginPosition();

					//---计算最大显示行数
					int iMaxDataRow = this.CalcShowMaxRowNum();

					//---计算实际数据可显示的行数
					int i_data_row = this.CalcMaxRowNum();

					//---进行判定
					Point pointB = new Point();
					if ((m_MousePos.iPos - row_DisplayNum) >= 0)
					{
						if (row_SelectedIndex > 0)
						{
							row_SelectedIndex -= 1;

							//---表明在第一个字节的右部
							if (m_MousePos.bRightPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth + (i_font_width) / 2;
								pointB.Y = pointA.Y - i_font_height - column_StaffWidth - 2;

								m_MousePos.iPos -= row_DisplayNum;
								m_MousePos.bRightPos = true;
								m_MousePos.bLeftPos = false;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.Invalidate();
								return;
							}
							//---表明在第一个字节的左部
							if (m_MousePos.bLeftPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth;
								pointB.Y = pointA.Y - i_font_height - column_StaffWidth - 2;

								m_MousePos.iPos -= row_DisplayNum;
								m_MousePos.bRightPos = false;
								m_MousePos.bLeftPos = true;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.Invalidate();
								return;
							}
						}
						else
						{
							row_DisplayIndex -= 1;

							//---表明在第一个字节的右部
							if (m_MousePos.bRightPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth + (i_font_width) / 2;

								pointB.Y = pointA.Y;

								m_MousePos.iPos -= row_DisplayNum;
								m_MousePos.bRightPos = true;
								m_MousePos.bLeftPos = false;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.m_VScrollBar.Value = row_DisplayIndex;
								this.Invalidate();
								return;
							}
							//---表明在第一个字节的左部
							if (m_MousePos.bLeftPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth;
								pointB.Y = pointA.Y;

								m_MousePos.iPos -= row_DisplayNum;
								m_MousePos.bRightPos = false;
								m_MousePos.bLeftPos = true;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.m_VScrollBar.Value = row_DisplayIndex;
								this.Invalidate();
								return;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 光标下移
		/// </summary>
		private void OnCaretMove_Down()
		{
			if (m_MousePos.iPos != -1 && m_MousePos.iArea != -1)
			{
				//---获取焦点
				this.Focus();

				//----表明在Data区
				if (m_MousePos.iArea == 1)
				{
					//---获取字体的宽度及长度
					int i_font_width = CalcSizeFontWidth();
					int i_font_height = CalcSizeFontHeigth();

					//---查找在第几列
					int iColumn = this.CalcCurrentColumnIndex();

					//---获取所在行的启动信息
					Point pointA = this.CalcRowBeginPosition();

					//---计算最大显示行数
					int iMaxDataRow = this.CalcShowMaxRowNum();

					//---计算实际数据可显示的行数
					int i_data_row = this.CalcMaxRowNum();

					//---进行判定
					Point pointB = new Point();
					if ((this.m_MousePos.iPos + row_DisplayNum) < (this.m_NowByte.Length))
					{
						if (row_SelectedIndex < iMaxDataRow - 1)
						{
							row_SelectedIndex += 1;

							//---表明在第一个字节的右部
							if (m_MousePos.bRightPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth + (i_font_width) / 2;
								pointB.Y = pointA.Y + i_font_height + column_StaffWidth - 2;

								m_MousePos.iPos += row_DisplayNum;
								m_MousePos.bRightPos = true;
								m_MousePos.bLeftPos = false;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.Invalidate();
								return;
							}
							//---表明在第一个字节的左部
							if (m_MousePos.bLeftPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth;
								pointB.Y = pointA.Y + i_font_height + column_StaffWidth - 2;

								m_MousePos.iPos += row_DisplayNum;
								m_MousePos.bRightPos = false;
								m_MousePos.bLeftPos = true;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.Invalidate();
								return;
							}
						}
						else
						{
							row_DisplayIndex += 1;

							//---表明在第一个字节的右部
							if (m_MousePos.bRightPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth + (i_font_width) / 2;
								pointB.Y = pointA.Y;

								m_MousePos.iPos += row_DisplayNum;
								m_MousePos.bRightPos = true;
								m_MousePos.bLeftPos = false;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.m_VScrollBar.Value = row_DisplayIndex;
								this.Invalidate();
								return;
							}
							//---表明在第一个字节的左部
							if (m_MousePos.bLeftPos)
							{
								pointB.X = pointA.X + iColumn * (i_font_width + row_StaffWidth) + row_StaffWidth;
								pointB.Y = pointA.Y;

								m_MousePos.iPos += row_DisplayNum;
								m_MousePos.bRightPos = false;
								m_MousePos.bLeftPos = true;
								HexBox.SetCaretPos(pointB.X, pointB.Y);
								this.m_VScrollBar.Value = row_DisplayIndex;
								this.Invalidate();
								return;
							}
						}
					}
				}
			}
		}

		#endregion

		#region 函数处理

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

		#endregion
	}
}