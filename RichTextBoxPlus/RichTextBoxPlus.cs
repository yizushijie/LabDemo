using System;
using System.Drawing;
using System.Windows.Forms;

namespace RichTextBoxPlusLib
{
	/// <summary>
	/// 控件RichTextBox能够显示不用颜色的字体
	/// </summary>
	public static class RichTextBoxPlus
	{
		/// <summary>
		/// 不同颜色显示数据
		/// </summary>
		/// <param name="rtb">控件</param>
		/// <param name="text">显示信息</param>
		/// <param name="texColor">颜色</param>
		/// <param name="addNewLine ">是否换行</param>
		public static void AppendTextColorFull(this RichTextBox rtb, string text, Color textColor, bool addNewLine = true)
		{
			if (addNewLine)
			{
				text += Environment.NewLine;
			}
			rtb.SelectionStart = rtb.TextLength;
			rtb.SelectionLength = 0;
			rtb.SelectionColor = textColor;
			//在当前窗体中追加文本
			rtb.AppendText(text);
			rtb.SelectionColor = rtb.ForeColor;
		}

		/// <summary>
		/// 显示指定颜色
		/// </summary>
		/// <param name="rtBox"></param>
		/// <param name="text"></param>
		/// <param name="textColor"></param>
		/// <param name="addNewLine"></param>
		public static void AppendTextInfo(this RichTextBox rtBox, string text, Color textColor, bool addNewLine = true)
		{
			AppendTextColorFull(rtBox, text, textColor, addNewLine);
			rtBox.SelectionStart = rtBox.Text.Length;
			rtBox.ScrollToCaret();
			rtBox.Focus();
		}

		/// <summary>
		/// 显示信息,在最上方显示
		/// </summary>
		/// <param name="rtBox"></param>
		/// <param name="text"></param>
		/// <param name="textColor"></param>
		/// <param name="addNewLine"></param>
		public static void AppendTextInfoTop(this RichTextBox rtBox, string text, Color textColor, bool addNewLine = true)
		{
			string temp = rtBox.Text;
			rtBox.Clear();
			AppendTextColorFull(rtBox, text, textColor, addNewLine);
			rtBox.AppendText(temp);
			//指向顶部
			rtBox.SelectionStart = 0;
			rtBox.ScrollToCaret();
			rtBox.Focus();
		}

		/// <summary>
		/// 显示信息，带有日期时间
		/// </summary>
		/// <param name="rtBox"></param>
		/// <param name="text"></param>
		/// <param name="textColor"></param>
		/// <param name="addNewLine"></param>
		public static void AppendTextInfoWithDateTime(this RichTextBox rtBox, string text, Color textColor, bool addNewLine = true)
		{
			string str = string.Format("{0} {1} {2}", System.DateTime.Now.ToString(), "：", text);
			AppendTextColorFull(rtBox, str, textColor, addNewLine);
			//直线底部
			rtBox.SelectionStart = rtBox.Text.Length;
			rtBox.ScrollToCaret();
			rtBox.Focus();
		}

		/// <summary>
		/// 显示信息，不含日期时间
		/// </summary>
		/// <param name="rtBox"></param>
		/// <param name="text"></param>
		/// <param name="textColor"></param>
		/// <param name="addNewLine"></param>
		public static void AppendTextInfoWithoutDateTime(this RichTextBox rtBox, string text, Color textColor, bool addNewLine = true)
		{
			AppendTextColorFull(rtBox, text, textColor, addNewLine);
			//直线底部
			rtBox.SelectionStart = rtBox.Text.Length;
			rtBox.ScrollToCaret();
			rtBox.Focus();
		}

		/// <summary>
		/// 信息显示在顶部,并且含有日期时间
		/// </summary>
		/// <param name="rtBox"></param>
		/// <param name="text"></param>
		/// <param name="textColor"></param>
		/// <param name="addNewLine"></param>
		public static void AppendTextInfoTopWithDataTime(this RichTextBox rtBox, string text, Color textColor, bool addNewLine = true)
		{
			string temp = rtBox.Text;
			rtBox.Clear();
			string str = string.Format("{0} {1} {2}", System.DateTime.Now.ToString(), "：", text);
			AppendTextColorFull(rtBox, str, textColor, addNewLine);
			rtBox.AppendText(temp);
			//指向顶部
			rtBox.SelectionStart = 0;
			rtBox.ScrollToCaret();
			rtBox.Focus();
		}

		/// <summary>
		/// 信息显示在顶部,不含有日期时间
		/// </summary>
		/// <param name="rtBox"></param>
		/// <param name="text"></param>
		/// <param name="textColor"></param>
		/// <param name="addNewLine"></param>
		public static void AppendTextInfoTopWithoutDateTime(this RichTextBox rtBox, string text, Color textColor, bool addNewLine = true)
		{
			string temp = rtBox.Text;
			rtBox.Clear();
			AppendTextColorFull(rtBox, text, textColor, addNewLine);
			rtBox.AppendText(temp);
			//指向顶部
			rtBox.SelectionStart = 0;
			rtBox.ScrollToCaret();
			rtBox.Focus();
		}
	}
}