using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreMakeToVSProject
{
	public class ProjectCfg
	{
		#region 变量定义

		/// <summary>
		/// 名称
		/// </summary>
		public string _name;

		/// <summary>
		/// 宏定义
		/// </summary>
		/// 
		public List<string> _define;
		/// <summary>
		/// 包含路劲
		/// </summary>
		public List<string> _includePath;

		/// <summary>
		/// 预包含路劲
		/// </summary>
		public List<string> _preInclude;
		// ReSharper disable once NotAccessedField.Local
		public bool _cmsis=false;

		#endregion

		#region 构造函数

		public ProjectCfg()
		{
			this._name = string.Empty;
			this._define = new List<string>();
			this._includePath = new List<string>();
			this._preInclude = new List<string>();
			this._cmsis = false;
		}

		#endregion


	}
}
