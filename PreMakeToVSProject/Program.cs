using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PreMakeToVSProject
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main( string[] arg )
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (arg.Count() == 1)
			{
				Application.Run(new PreMakeToVSProject(arg[0]));
			}
			else
			{
				Application.Run(new PreMakeToVSProject());
			}
			
		}
	}
}
