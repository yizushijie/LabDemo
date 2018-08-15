using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HexBoxLib
{
	public partial class HexBox : Control
	{
		/// <summary>
		/// 光标的坐标
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct CaretPoint
		{
			public int X;
			public int Y;

			public CaretPoint(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}
		}

		/// <summary>
		/// 获取光标的位置
		/// </summary>
		/// <param name="lpPoint"></param>
		/// <returns></returns>

		[DllImport("user32.dll")]
		public static extern bool GetCaretPos(out CaretPoint lpPoint);

		/// <summary>
		/// 创建光标
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="hBitmap"></param>
		/// <param name="nWidth"></param>
		/// <param name="nHeight"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);

		/// <summary>
		/// 显示光标
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool ShowCaret(IntPtr hWnd);

		/// <summary>
		/// 隐藏光标
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern bool HideCaret(IntPtr hWnd);

		/// <summary>
		/// 设置光标的位置
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern bool SetCaretPos(int x, int y);

		/// <summary>
		/// 释放光标
		/// </summary>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool DestroyCaret();

		/// <summary>
		/// 获取光标闪烁的间隔
		/// </summary>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern uint GetCaretBlinkTime();

		/// <summary>
		/// 设置光标闪烁的间隔
		/// </summary>
		/// <param name="uMSeconds"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool SetCaretBlinkTime(uint uMSeconds);
	}
}