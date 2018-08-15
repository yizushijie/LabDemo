using System.Diagnostics;
using System.Reflection;

namespace ProcessApplicationLib
{
	public partial class ProcessApplication
	{
		#region 确保程序只运行一个实例

		/// <summary>
		/// 在进程中查找是否已经有实例在运行
		/// </summary>
		/// <returns></returns>
		public static Process RunApplication()
		{
			//---获取当前的进程
			Process current = Process.GetCurrentProcess();
			//---赛选当前进程的名称
			Process[] processes = Process.GetProcessesByName(current.ProcessName);
			//---遍历与当前进程名称相同的进程列表
			foreach (Process process in processes)
			{
				//---如果实例已经存在则忽略当前进程
				if (process.Id != current.Id)
				{
					//---保证要打开的进程同已经存在的进程来自同一文件路径
					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
					{
						//---返回已经存在的进程
						return process;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// 获取当前指定名称的应用实例
		/// </summary>
		/// <param name="processFileName"></param>
		/// <returns></returns>
		public static Process RunApplication(string processFileName)
		{
			//---获取当前指定名称的京城
			Process[] processes = Process.GetProcessesByName(processFileName);
			//---判断当前进程是否存在
			if (processes.Length > 0)
			{
				if (processFileName == "Calculator")
				{
					processes[0].Kill();
					return null;
				}
				return processes[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 已经有了就把它激活，并将其窗口放置最前端
		/// </summary>
		/// <param name="instance"></param>
		public static bool ActivateRunApplication(Process instance)
		{
			//---调用api函数，正常显示窗口
			ShowWindowAsync(instance.MainWindowHandle, 1);
			//---将窗口放置最前端
			SetForegroundWindow(instance.MainWindowHandle);
			return true;
		}

		/// <summary>
		/// 激活应用
		/// </summary>
		/// <param name="exe"></param>
		/// <param name="exeName"></param>
		public static bool ActivateRunApplication(string exe, string exeName)
		{
			ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
			info.FileName = exe;//"calc.exe"为计算器，"notepad.exe"为记事本
								//---启动进程
			Process proc = RunApplication(exeName);
			if (proc != null)
			{
				//---激活应用
				ActivateRunApplication(proc);
			}
			else
			{
				//---启动应用
				proc = Process.Start(info);
			}

			//---判断是否有进程启动
			if (proc == null)
			{
				return false;
			}

			return true;
		}

		#endregion 确保程序只运行一个实例
	}
}