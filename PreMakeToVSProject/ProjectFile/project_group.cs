using System.Collections.Generic;
using System.Linq;

namespace PreMakeToVSProject
{
	public class ProjectGroup
	{
		#region 变量定义

		/// <summary>
		/// 名称
		/// </summary>
		public string _name;

		/// <summary>
		/// 文件
		/// </summary>
		public List<ProjectFile> _file;

		/// <summary>
		/// 子群
		/// </summary>
		public List<ProjectGroup> _subGroup;
		
		/// <summary>
		/// 主群
		/// </summary>
		public ProjectGroup _masterGroup;

		/// <summary>
		/// 不包含
		/// </summary>
		public List<string> _exclude;

		#endregion

		#region 属性定义
		/// <summary>
		/// 全民
		/// </summary>
		public string m_FullName
		{
			get
			{
				if (this._masterGroup == null)
				{
					return this._name;
				}
				else
				{
					return this._masterGroup.m_FullName + "/" + this._name;
				}
				
			}
		}

		#endregion

		#region 构造函数

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="masterGroup"></param>
		public ProjectGroup(ProjectGroup masterGroup=null)
		{
			this._masterGroup = masterGroup;
			this._exclude	= new List<string>();
			this._file		= new List<ProjectFile>();
			this._subGroup	= new List<ProjectGroup>();
		}


		#endregion

		#region 函数定义
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="prjGroup"></param>
		/// <param name="exclude"></param>
		/// <param name="_return"></param>
		/// <returns></returns>
		public List<string> GetPath( List<ProjectGroup> prjGroup, string exclude, List<string> _return = null)
		{
			if (_return == null)
			{
				_return = new List<string>();
			}
			foreach (ProjectGroup temp in prjGroup)
			{
				if (temp._exclude.Contains(exclude))
				{
					continue;
				}
				_return.Add("[\"" + temp.m_FullName + "\"] = { \"" + string.Join("\" , \"", from file in temp._file where (!file._exclude.Contains(exclude)) select file._name) + "\" }");
				this.GetPath(temp._subGroup, exclude,_return);
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="prjGroup"></param>
		/// <param name="exclude"></param>
		/// <returns></returns>
		public List<string> GetFile(List<ProjectGroup> prjGroup,string exclude)
		{
			List<string> _return = new List<string>();
			foreach (var temp in prjGroup)
			{
				if (temp._exclude.Contains(exclude))
				{
					break;
				}
				_return.AddRange(from file in temp._file where (!file._exclude.Contains(exclude)) select file._name);
				_return.AddRange(this.GetFile(temp._subGroup, exclude));
			}
			return _return;
		}

		#endregion


	}

}
