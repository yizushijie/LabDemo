using System.Drawing;
using System.Windows.Forms;

namespace HexBoxLib
{
	/// <summary>
	/// 计算字体的高度和宽度
	/// </summary>
	public partial class HexBox : Control
	{
		/// <summary>
		/// 计算算字体的大小
		/// </summary>
		/// <returns></returns>
		private SizeF CalcSizeFont()
		{
			Graphics g = this.CreateGraphics();
			SizeF sizeF = g.MeasureString("00", this.m_Font);
			g.Dispose();
			return sizeF;
		}

		/// <summary>
		/// 计算字体的大小
		/// </summary>
		/// <param name="strText"></param>
		/// <returns></returns>
		private SizeF CalcSizeFont(string strText)
		{
			Graphics g = this.CreateGraphics();
			SizeF sizeF = g.MeasureString(strText, this.m_Font);
			g.Dispose();
			return sizeF;
		}

		/// <summary>
		/// 计算字体的大小
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="font"></param>
		/// <returns></returns>
		private SizeF CalcSizeFont(string strText, Font font)
		{
			Graphics g = this.CreateGraphics();
			SizeF sizeF = g.MeasureString(strText, font);
			g.Dispose();
			return sizeF;
		}

		/// <summary>
		/// 计算字体的宽度
		/// </summary>
		/// <returns></returns>
		private int CalcSizeFontWidth()
		{
			SizeF size = CalcSizeFont("00", this.m_Font);
			return (int)(size.Width - 1.5);
		}

		/// <summary>
		/// 计算字体的宽度
		/// </summary>
		/// <param name="strText"></param>
		/// <returns></returns>
		private int CalcSizeFontWidth(string strText)
		{
			SizeF size = CalcSizeFont(strText, this.m_Font);
			return (int)(size.Width - 1.5);
		}

		/// <summary>
		/// 计算字体的宽度
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="font"></param>
		/// <returns></returns>
		private int CalcSizeFontWidth(string strText, Font font)
		{
			SizeF size = CalcSizeFont(strText, font);
			return (int)(size.Width);
		}

		/// <summary>
		/// 计算字体的高度
		/// </summary>
		/// <returns></returns>
		private int CalcSizeFontHeigth()
		{
			SizeF size = CalcSizeFont("00", this.m_Font);

			return (int)(size.Height);
		}

		/// <summary>
		/// 计算字体的高度
		/// </summary>
		/// <param name="strText"></param>
		/// <returns></returns>
		private int CalcSizeFontHeigth(string strText)
		{
			SizeF size = CalcSizeFont(strText, this.m_Font);

			return (int)(size.Height);
		}

		/// <summary>
		/// 计算字体的高度
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="font"></param>
		/// <returns></returns>
		private int CalcSizeFontHeigth(string strText, Font font)
		{
			SizeF size = CalcSizeFont(strText, font);
			return (int)(size.Height);
		}
	}
}