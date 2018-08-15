using System;
using System.Runtime.InteropServices;

namespace ProcessApplicationLib
{
	public partial class ProcessApplication
	{
		#region API

		/// <summary>
		///
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="cmdShow"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);

		/// <summary>
		///
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(System.IntPtr hWnd);

		/// <summary>
		///
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="fAltTab"></param>
		[DllImport("user32.dll")]
		public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

		/// <summary>
		///
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="nCmdShow"></param>
		/// <returns></returns>
		[DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
		private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

		#endregion API
	}
}