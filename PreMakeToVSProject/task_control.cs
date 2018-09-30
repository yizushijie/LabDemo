using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PreMakeToVSProject
{
	public class TaskControl
	{
        #region 变量定义

		/*
        string IncOverride =
           "   premake.override(premake.vstudio.vc2010, \"includePath\", function(base,cfg)\r\n" +
           "   local dirs = premake.vstudio.path(cfg, cfg.sysincludedirs)\r\n" +
           "   if #dirs > 0 then\r\n" +
           "   premake.vstudio.vc2010.element(\"IncludePath\", nil, \"%s\", table.concat(dirs, \";\"))\r\n" +
           "   end\r\n" +
           "end)";
		*/
        #endregion



        #region 函数定义

        /// <summary>
        /// </summary>
        /// <param name="srcVersion"></param>
        /// <returns></returns>
        public string GetPathAndProjectVersion(string srcVersion)
		{
			OpenFileDialog openFile = null;
			if (srcVersion=="IAR")
			{
				openFile= new OpenFileDialog { Filter = @"IAR Project (*.ewp)|*.ewp" };
			}
			else if (srcVersion == "Keil")
			{
				openFile = new OpenFileDialog { Filter = @"Keil Project (*.uvprojx)|*.uvprojx|(*.uvproj)|*.uvproj" };
			}
			else
			{
				return null;
			}
			if (openFile.ShowDialog() != DialogResult.OK)
			{
				return null;
			}
			else
			{
				return openFile.FileName;
			}
		}

		public string GetPathAndProjectVersion(string srcVersion,Label lbl)
		{
			OpenFileDialog openFile = null;
			if (srcVersion == "IAR")
			{
				//lbl.Visible = false;
				openFile = new OpenFileDialog { Filter = @"IAR Project (*.ewp)|*.ewp" };
			}
			else if (srcVersion == "Keil")
			{
				//lbl.Visible = true;
				openFile = new OpenFileDialog { Filter = @"Keil Project (*.uvprojx)|*.uvprojx|(*.uvproj)|*.uvproj" };
			}
			else
			{
				return null;
			}
			if (openFile.ShowDialog() != DialogResult.OK)
			{
				return null;
			}
			else
			{
				return openFile.FileName;
			}
		}


		/// <summary>
		/// 递归调用查找节点
		/// </summary>
		/// <param name="inXmlNode"></param>
		/// <param name="inTreeNode"></param>
		public void AddTreeNode(XmlNode inXmlNode, TreeNode inTreeNode)
		{
			XmlNode xNode;
			TreeNode tNode;
			XmlNodeList nodeList;
			int i;
			// Loop through the XML nodes until the leaf is reached.
			// Add the nodes to the TreeView during the looping process.
			if (inXmlNode.HasChildNodes)
			{
				nodeList = inXmlNode.ChildNodes;
				for (i = 0; i <= nodeList.Count - 1; i++)
				{
					xNode = inXmlNode.ChildNodes[i];
					inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
					tNode = inTreeNode.Nodes[i];
					AddTreeNode(xNode, tNode);
				}
			}
			else
			{
				inTreeNode.Text = (inXmlNode.OuterXml).Trim();
			}
		}

		public void AddTreeNode(XmlNode inXmlNode, TreeNode inTreeNode,string name)
		{
			XmlNode xNode;
			TreeNode tNode;
			XmlNodeList nodeList;
			int i;
			// Loop through the XML nodes until the leaf is reached.
			// Add the nodes to the TreeView during the looping process.
			if (inXmlNode.HasChildNodes)
			{
				nodeList = inXmlNode.ChildNodes;

				int j = 0;
				for (i = 0; i <= nodeList.Count - 1; i++)
				{
					xNode = inXmlNode.ChildNodes[i];
					if (xNode.Name==name)
					{
						inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
						tNode = inTreeNode.Nodes[j];
						j++;
						AddTreeNode(xNode, tNode);
					}
					else
					{
						continue;
					}
				}
			}
			else
			{
				inTreeNode.Text = (inXmlNode.OuterXml).Trim();
			}
		}

		/// <summary>
		/// 递归查询,找到返回该节点
		/// </summary>
		/// <param name="tNode"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public TreeNode FindTreeNode(TreeNode tNode, string name)
		{
			//接受返回的节点
			TreeNode _return = null;
			//循环查找
			foreach (TreeNode temp in tNode.Nodes)
			{
				//是否有子节点
				if (temp.Nodes.Count != 0)
				{
					//如果找到
					if ((_return = FindTreeNode(temp, name)) != null)
					{
						return _return;
					}
				}
				//如果找到
				if (string.Equals(temp.Text, name))
				{
					return temp;
				}
			}
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="projectName"></param>
		/// <param name="srcVersion"></param>
		/// <returns></returns>
		public bool ManageToVSProject(string projectName,string srcVersion)
		{
			if (srcVersion == "IAR")
			{
				return this.IARProjectToVSProject(projectName);
			}
			else if (srcVersion == "Keil")
			{
				return this.KeilProjectToVSProject(projectName);
			}
			else
			{
				return false;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="srcVersion"></param>
        /// <returns></returns>
        public bool ManageToVSProject(string projectName, ComboBox srcVersion)
        {
            if (projectName.Contains("uvprojx")|| projectName.Contains("uvproj"))
            {
                if (srcVersion.Text!="Keil")
                {
                    srcVersion.Text = "Keil";
                }
                return this.KeilProjectToVSProject(projectName);
            }
            else if (projectName.Contains("ewp"))
            {
                if (srcVersion.Text != "IAR")
                {
                    srcVersion.Text = "IAR";
                }
                return this.IARProjectToVSProject(projectName);
            }
            else
            {
                return false;
            }
          
        }


        public bool KeilProjectToVSProject(string projectName)
		{
			//---判断文件是否存在
			if (!File.Exists(projectName))
			{
				return false;
			}

			//---变量定义
			ProjectCfg prjcfg = new ProjectCfg();
			List<ProjectGroup> prjgroup = new List<ProjectGroup>();

			//---读取设备的文件---创建XmlReader对象的实例并将其返回给调用程序
			XmlReader xmlRead = XmlReader.Create(new StringReader(File.ReadAllText(projectName)));
			//---读取配置文件---不断读取直至找到指定的元素
			xmlRead.ReadToFollowing("TargetOption");
			do
			{
				//---读取子节点---而是用于创建 XML 元素周围的边界
				XmlReader subXmlRead = xmlRead.ReadSubtree();
				//---移动读取器至下一个匹配子孙元素的节点
				subXmlRead.ReadToDescendant("OutputName");
				//---从流中读取下一个节点
				subXmlRead.Read();
				prjcfg._name = subXmlRead.Value;

				//---获取设置
				subXmlRead.ReadToFollowing("Cads");
				//---
				subXmlRead.ReadToDescendant("Optim");
				//---从流中读取下一个节点
				subXmlRead.Read();
				//---判断数据值
				if (int.Parse(subXmlRead.Value) > 0)
				{
					prjcfg._cmsis = true;
				}
				else
				{
					prjcfg._cmsis = false;
				}
				//---读取下一个节点
				subXmlRead.ReadToFollowing("VariousControls");
				subXmlRead.ReadToFollowing("Define");
				prjcfg._define = this.GetKeilSubNodeValue(subXmlRead.ReadSubtree());

				//----Keil工程的生成中借用了IAR的一些文件，用于避免一些数据类型报错的问题
				//---IAR中增加的信息
				prjcfg._define.Add("_IAR_");
				prjcfg._define.Add("__ICCARM__");
                prjcfg._define.Add("__CC_ARM");
                prjcfg._define.Add("_Pragma(x)=");
				prjcfg._define.Add("__interrupt=");
				prjcfg._define.Add("__packed=");
				prjcfg._define.Add("__weak=");
                //prjcfg._define.Add("__attribute__((x))=");
                //prjcfg._define.Add("__STATIC_INLINE=");
				//---获取预包含的头文件
				//subXmlRead.ReadToFollowing("PreInclude");
				//prjcfg._preInclude = this.GetKeilSubNodeValue(subXmlRead,';');
				//---获取包含文件的路径
				subXmlRead.ReadToFollowing("IncludePath");
				prjcfg._includePath = this.GetKeilSubNodeValue(subXmlRead.ReadSubtree(), ';');

			}
			while (xmlRead.ReadToNextSibling("TargetOption"));
			xmlRead.Close();

			xmlRead = XmlReader.Create(new StringReader(File.ReadAllText(projectName)));
			//---读取配置文件---不断读取直至找到指定的元素
			//---获取组文件
			
			do
			{
				xmlRead.ReadToFollowing("Group");
				if (xmlRead.Depth == 4 && xmlRead.NodeType == XmlNodeType.Element)
				{
					ProjectGroup _return = GetKeilSubNodeGroup(xmlRead, "Group");
					if (_return != null)
					{
						prjgroup.Add(_return);
					}
				}
				
			}
			while (!xmlRead.EOF);
			xmlRead.Close();

			//---处理MDK文档
			foreach (var temp in prjgroup)
			{
				if (temp._name.Contains("MDK"))
				{
					temp._name= temp._name.Substring(temp._name.IndexOf("/MDK") + 1);
				}
				else
				{
					continue;
				}
			}

			//---获取C文件
			xmlRead = XmlReader.Create(new StringReader(File.ReadAllText(projectName)));
			do
			{
				xmlRead.ReadToFollowing("Files");
				if (xmlRead.Depth == 3 && xmlRead.NodeType == XmlNodeType.Element)
				{
					ProjectGroup _return = GetKeilSubNodeFile(xmlRead, "Files");
					if (_return != null)
					{
						prjgroup.Add(_return);
					}
				}
			}
			while (!xmlRead.EOF);
			xmlRead.Close();

            //---获取Keil的执行路径
			//---Keil5
            string keilPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetProcessesByName("UV4")[0].MainModule.FileName);
			if (keilPath==null)
			{
				//---keil4
				keilPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetProcessesByName("UV3")[0].MainModule.FileName);
			}
			if (keilPath==null)
			{
				//---keil2
				keilPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetProcessesByName("UV2")[0].MainModule.FileName);
			}
            string includedirs= string.Join("\", \"", prjcfg._includePath);
            if (keilPath!=null)
            {
                keilPath =keilPath.Replace("\\","/");
                keilPath =keilPath.Replace("UV4", "ARM/RV31");
                includedirs += "\", \"" + keilPath;
            }
			//string configurations = "Debug" + "\", \"" + "Release";
			//StreamWriter file = new StreamWriter(Path.GetDirectoryName(projectName) + "\\KeilToVS.lua");
			StreamWriter file = new StreamWriter(Path.GetDirectoryName(projectName) + "\\premake5.lua");
			//StreamWriter file = new StreamWriter(Directory.GetParent(Path.GetDirectoryName(projectName)).FullName + "\\premake5.lua");
			{
				//
				file.WriteLine("  solution \"" + Path.GetFileNameWithoutExtension(projectName) + "\"");
				file.WriteLine("  configurations { \"" + string.Join("\", \"", prjcfg._name) + "\" }");
				//file.WriteLine("  configurations { \"" + configurations + "\" }");
				file.WriteLine("  project\"" + Path.GetFileNameWithoutExtension(projectName)+ "\"");
				file.WriteLine("  kind \"ConsoleApp\"");
				file.WriteLine("  language \"C\"");
				file.WriteLine("  filter \"configurations:" + prjcfg._name + "\"");
				file.WriteLine("  sysincludedirs  {\"$(VC_IncludePath)\"}");
				file.WriteLine("  defines { \"" + string.Join("\", \"", prjcfg._define) + "\" }");
				file.WriteLine("  forceincludes { \"" + string.Join("\", \"", prjcfg._preInclude) + "\" }");
				file.WriteLine("  includedirs { \"" + includedirs + "\" }");
				List<string> srcFiles = new ProjectGroup().GetFile(prjgroup, prjcfg._name);
				file.WriteLine("  files { \"" + string.Join("\", \"", srcFiles) + "\"}");
				List<string> vGroups = new ProjectGroup().GetPath(prjgroup, prjcfg._name);
				file.WriteLine("  vpaths {" + string.Join(" , ", vGroups) + " }");
				//file.Write(IncOverride);
				file.Close();
			}

			return true;
		}

		/// <summary>
		/// IAR项目转VS工程
		/// </summary>
		/// <param name="projectName"></param>
		public bool  IARProjectToVSProject(string projectName)
        {
            //---判断文件是否存在
            if (!File.Exists(projectName))
            {
                return false;
            }

            //---变量定义
            ProjectCfg prjcfg = new ProjectCfg();
            List<ProjectGroup> prjgroup = new List<ProjectGroup>();

            //---读取设备的文件---创建XmlReader对象的实例并将其返回给调用程序
            XmlReader xmlRead = XmlReader.Create(new StringReader(File.ReadAllText(projectName)));
            //---读取配置文件---不断读取直至找到指定的元素
            xmlRead.ReadToFollowing("configuration");
            do
            {
                //---读取子节点---而是用于创建 XML 元素周围的边界
                XmlReader subXmlRead = xmlRead.ReadSubtree();
                //---移动读取器至下一个匹配子孙元素的节点
                subXmlRead.ReadToDescendant("name");
                //---从流中读取下一个节点
                subXmlRead.Read();
                prjcfg._name = subXmlRead.Value;

                //---获取设置
                subXmlRead.ReadToFollowing("settings");
                //---
                subXmlRead.ReadToDescendant("archiveVersion");
                //---从流中读取下一个节点
                subXmlRead.Read();
                //---判断数据值
                if (int.Parse(subXmlRead.Value)>0)
                {
                    prjcfg._cmsis = true;
                }
                else
                {
                    prjcfg._cmsis = false;
                }
                //---读取下一个节点
                subXmlRead.ReadToFollowing("settings");
                prjcfg._define = this.GetIarSubNodeValue(subXmlRead, "CCDefines");
                //---IAR中增加的信息
                prjcfg._define.Add("_IAR_");
                prjcfg._define.Add("__ICCARM__");
                prjcfg._define.Add("_Pragma(x)=");
                prjcfg._define.Add("__interrupt=");
                prjcfg._define.Add("__packed=");
                prjcfg._define.Add("__weak=");
                //prjcfg._define.Add("__attribute__((x))=");
                //prjcfg._define.Add("__STATIC_INLINE=");
                //---获取预包含的头文件
                prjcfg._preInclude = this.GetIarSubNodeValue(subXmlRead, "PreInclude");
                //---获取包含文件的路径
                prjcfg._includePath= this.GetIarSubNodeValue(subXmlRead, "CCIncludePath2");

            }
			while (xmlRead.ReadToNextSibling("configuration"));
			xmlRead.Close();

			xmlRead = XmlReader.Create(new StringReader(File.ReadAllText(projectName)));
            //---读取配置文件---不断读取直至找到指定的元素
            //---获取组文件
            xmlRead.ReadToFollowing("group");
            do
            {
                ProjectGroup _return = GetIarSubNodeGroup(xmlRead,"group");
                if (_return != null)
                {
                    prjgroup.Add(_return);
                }
            }
			while (xmlRead.ReadToNextSibling("group"));
			xmlRead.Close();

			//---获取C文件
			xmlRead = XmlReader.Create(new StringReader(File.ReadAllText(projectName)));
            do
            {
                xmlRead.ReadToFollowing("file");
                if (xmlRead.Depth == 1 && xmlRead.NodeType == XmlNodeType.Element)
                {
					ProjectGroup _return = GetIarSubNodeFile( xmlRead, "file");
                    if (_return != null)
                    {
                        prjgroup.Add(_return);
                    }
                }
            }
            while (!xmlRead.EOF);
            xmlRead.Close();
			//string configurations = "Debug" + "\", \"" + "Release";
			//StreamWriter file =new StreamWriter(Path.GetDirectoryName(projectName) + "\\IARToVS.lua");
			StreamWriter file = new StreamWriter(Path.GetDirectoryName(projectName) + "\\premake5.lua");
			//StreamWriter file = new StreamWriter(Directory.GetParent(Path.GetDirectoryName(projectName)).FullName + "\\premake5.lua");
			{
                //
                file.WriteLine("  solution \"" + Path.GetFileNameWithoutExtension(projectName) + "\"");
				file.WriteLine("  configurations { \"" + string.Join("\", \"", prjcfg._name) + "\" }");
				//file.WriteLine("  configurations { \"" + configurations + "\" }");
				file.WriteLine("  project\"" + Path.GetFileNameWithoutExtension(projectName) + "\"");
                file.WriteLine("  kind \"ConsoleApp\"");
                file.WriteLine("  language \"C\"");
                file.WriteLine("  filter \"configurations:" + prjcfg._name + "\"");
                file.WriteLine("  sysincludedirs  {\"$(VC_IncludePath)\"}");
                file.WriteLine("  defines { \"" + string.Join("\", \"", prjcfg._define) + "\" }");
                file.WriteLine("  forceincludes { \"" + string.Join("\", \"", prjcfg._preInclude) + "\" }");
                file.WriteLine("  includedirs { \"" + string.Join("\", \"", prjcfg._includePath) + "\" }");
                List<string> srcFiles =new ProjectGroup().GetFile(prjgroup, prjcfg._name);
                file.WriteLine("  files { \"" + string.Join("\", \"", srcFiles) + "\" }");
				List<string> vGroups =new ProjectGroup().GetPath(prjgroup, prjcfg._name);
				file.WriteLine("  vpaths {" + string.Join(" , ", vGroups) + " }");
                //file.Write(IncOverride);
                file.Close();
            }

            return true;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlRead"></param>
		/// <param name="val"></param>
		/// <returns></returns>
        List<string> GetIarSubNodeValue(XmlReader xmlRead,string val)
        {
            List<string> _return = new List<string>();
            //---读取到指定的值为止
            do
            {
                xmlRead.Read();
                if (xmlRead.EOF)
                {
                    return _return;
                }
            } while (xmlRead.Value!=val);
            xmlRead.Read();
            int depth = xmlRead.Depth;
            //---读取指定值包含的数据
            do
            {
                if (xmlRead.Read()&& (xmlRead.NodeType==XmlNodeType.Text)&&(xmlRead.HasValue))
                {
                    string temp = xmlRead.Value;
                    temp = temp.Replace("$PROJ_DIR$\\", "");
                    temp = temp.Replace("$PROJ_DIR$/", "");
                    temp = temp.Replace("\\", "/");
                    temp = temp.Replace("$PROJ_DIR$\\", "");
                    temp = temp.Replace("$PROJ_DIR$/", "");
                    temp = temp.Replace("\\", "/");
                    _return.Add(temp);
                }

            } while (xmlRead.Depth>=depth);
			//---移动到当前元素
            xmlRead.MoveToElement();
            return _return;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlRead"></param>
		/// <returns></returns>
		List<string> GetKeilSubNodeValue(XmlReader xmlRead,char split=',')
		{
			List<string> _return = new List<string>();
			//---读取到指定的值为止
			xmlRead.Read();
			//---读取指定值包含的数据
			do
			{
				if (xmlRead.Read() && (xmlRead.NodeType == XmlNodeType.Text) && (xmlRead.HasValue))
				{
					string temp = xmlRead.Value;
					temp = temp.Replace("$PROJ_DIR$\\", "");
					temp = temp.Replace("$PROJ_DIR$/", "");
					temp = temp.Replace("\\", "/");
					temp = temp.Replace("$PROJ_DIR$\\", "");
					temp = temp.Replace("$PROJ_DIR$/", "");
					temp = temp.Replace("\\", "/");
					_return.AddRange(temp.Split(split));
				}

			} while (!xmlRead.EOF);
			//---移动到当前元素
			xmlRead.MoveToElement();
			return _return;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlRead"></param>
		/// <param name="parent"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		ProjectGroup GetIarSubNodeGroup(XmlReader xmlRead, string str,ProjectGroup parent=null )
		{
			ProjectGroup _return = new ProjectGroup(parent);
			if (xmlRead.NodeType != XmlNodeType.Element || xmlRead.Name != str)
			{
				xmlRead.ReadToFollowing(str);
			}
			if (xmlRead.EOF)
			{
				return _return;
			}
			XmlReader subXmlRead = xmlRead.ReadSubtree();
			do
			{
				subXmlRead.Read();
			}
			while (subXmlRead.NodeType != XmlNodeType.Text);
			_return._name = subXmlRead.Value;
			do
			{
				if (subXmlRead.Read() && subXmlRead.NodeType == XmlNodeType.Element)
				{
					switch (subXmlRead.Name)
					{
						case "group":
							//---子分组信息---递归调用
							_return._subGroup.Add(this.GetIarSubNodeGroup( subXmlRead, str, _return));
							break;
						case "file":
							//---获取节点
							XmlReader subSubXmlRead = subXmlRead.ReadSubtree();
							do
							{
								subSubXmlRead.Read();
								switch (subSubXmlRead.Name)
								{
									case "name":
										if (subSubXmlRead.Read()&&(subSubXmlRead.NodeType == XmlNodeType.Text) && subSubXmlRead.HasValue && (subSubXmlRead.Depth == 2))
										{
											_return._file.Add(new ProjectFile());
											string temp = subSubXmlRead.Value;
											temp=temp.Replace("$PROJ_DIR$\\", "");
											temp=temp.Replace("$PROJ_DIR$/", "");
											temp=temp.Replace("\\", "/");
											_return._file.Last()._name = temp;//subsubReader.Value;
										}
										break;
									case "excluded":
										if (subSubXmlRead.NodeType == XmlNodeType.Element)
										{
											XmlReader subSubSubXmlRead = subSubXmlRead.ReadSubtree();
											do
											{
												subSubSubXmlRead.Read();
												if (subSubSubXmlRead.NodeType == XmlNodeType.Text)
												{
													_return._file.Last()._exclude.Add(subSubSubXmlRead.Value);
												}
											}
											while (!subSubSubXmlRead.EOF);
											subSubSubXmlRead.Close();
										}
										break;
                                    default:
                                        break;
								}
							}
							while (!subSubXmlRead.EOF);
							subSubXmlRead.Close();
                            break;
						case "excluded":
							do
							{
								if (subXmlRead.Read()&&(subXmlRead.NodeType == XmlNodeType.Text) && (subXmlRead.HasValue))
								{
									_return._exclude.Add(subXmlRead.Value);
								}
							}
							while (xmlRead.Name != "excluded");
							break;
                        default:
                            break;
					}
				}
			}
			while (!subXmlRead.EOF);
			subXmlRead.Close();
			return _return;
		}

		ProjectGroup GetKeilSubNodeGroup(XmlReader xmlRead, string str, ProjectGroup parent = null)
		{
			ProjectGroup _return = new ProjectGroup(parent);
			if (xmlRead.NodeType != XmlNodeType.Element || xmlRead.Name != str)
			{
				xmlRead.ReadToFollowing(str);
			}
			if (xmlRead.EOF)
			{
				return _return;
			}
			XmlReader subXmlRead = xmlRead.ReadSubtree();
			do
			{
				subXmlRead.Read();
			}
			while (subXmlRead.NodeType != XmlNodeType.Text);
			_return._name = subXmlRead.Value;
			do
			{
				if (subXmlRead.Read() && subXmlRead.NodeType == XmlNodeType.Element)
				{
					switch (subXmlRead.Name)
					{
						case "Group":
							//---子分组信息---递归调用
							_return._subGroup.Add(this.GetKeilSubNodeGroup(subXmlRead, str, _return));
							break;
						case "Files":
							//---获取节点
							XmlReader subSubXmlRead = subXmlRead.ReadSubtree();
							do
							{
								subSubXmlRead.Read();
								switch (subSubXmlRead.Name)
								{
									case "FilePath"://"FilePath":
										if (subSubXmlRead.Read() && (subSubXmlRead.NodeType == XmlNodeType.Text) && subSubXmlRead.HasValue && (subSubXmlRead.Depth == 3))
										{
											_return._file.Add(new ProjectFile());
											string temp = subSubXmlRead.Value;
											temp = temp.Replace("$PROJ_DIR$\\", "");
											temp = temp.Replace("$PROJ_DIR$/", "");
											temp = temp.Replace("\\", "/");
											_return._file.Last()._name = temp;//subsubReader.Value;
										}
										break;
									case "excluded":
										if (subSubXmlRead.NodeType == XmlNodeType.Element)
										{
											XmlReader subSubSubXmlRead = subSubXmlRead.ReadSubtree();
											do
											{
												subSubSubXmlRead.Read();
												if (subSubSubXmlRead.NodeType == XmlNodeType.Text)
												{
													_return._file.Last()._exclude.Add(subSubSubXmlRead.Value);
												}
											}
											while (!subSubSubXmlRead.EOF);
											subSubSubXmlRead.Close();
										}
										break;
									default:
										break;
								}
							}
							while (!subSubXmlRead.EOF);
							subSubXmlRead.Close();
							break;
						case "excluded":
							do
							{
								if (subXmlRead.Read() && (subXmlRead.NodeType == XmlNodeType.Text) && (subXmlRead.HasValue))
								{
									_return._exclude.Add(subXmlRead.Value);
								}
							}
							while (xmlRead.Name != "excluded");
							break;
						default:
							break;
					}
				}
			}
			while (!subXmlRead.EOF);
			subXmlRead.Close();
			return _return;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlRead"></param>
		/// <param name="NodeName"></param>
		/// <returns></returns>
		public ProjectGroup GetIarSubNodeFile(XmlReader xmlRead,string NodeName)
		{
			ProjectGroup _return = new ProjectGroup();
			//---是否位于结尾
			if (xmlRead.EOF)
			{
				return _return;
			}
			_return._name = string.Empty;
			//---读取包含的文件信息
			do
			{
				//---File后的节点深度是1
				if (xmlRead.Depth != 1)
				{
					continue;
				}
				XmlReader subXmlRead = xmlRead.ReadSubtree();
				if (subXmlRead.Read() && (subXmlRead.NodeType == XmlNodeType.Element))
				{
					//---读取当前节点以及子节点
					XmlReader subsubXmlRead = subXmlRead.ReadSubtree();
					do
					{
						subsubXmlRead.Read();
						switch (subsubXmlRead.Name)
						{
							case "name":
								if (subsubXmlRead.Read()&&(subsubXmlRead.NodeType == XmlNodeType.Text) && (subsubXmlRead.HasValue) && (subsubXmlRead.Depth == 2))
								{
									_return._file.Add(new ProjectFile());
									string temp = subsubXmlRead.Value;
									temp = temp.Replace("$PROJ_DIR$\\", "");
									temp = temp.Replace("$PROJ_DIR$/", "");
									temp = temp.Replace("\\", "/");
									_return._file.Last()._name = temp;
								}
								break;
							case "excluded":
								if (subsubXmlRead.NodeType == XmlNodeType.Element)
								{
									XmlReader subSubSubXmlRead = subsubXmlRead.ReadSubtree();
									do
									{
										if (subSubSubXmlRead.Read()&&(subSubSubXmlRead.NodeType == XmlNodeType.Text)&&(subSubSubXmlRead.HasValue))
										{
											_return._file.Last()._exclude.Add(subSubSubXmlRead.Value);
										}	
									}
									while (!subSubSubXmlRead.EOF);
									subSubSubXmlRead.Close();
								}
								break;
						}
					}
					while (!subsubXmlRead.EOF);
					subsubXmlRead.Close();
				}
			}
			while (xmlRead.ReadToNextSibling(NodeName));
			return _return;
		}

		public ProjectGroup GetKeilSubNodeFile(XmlReader xmlRead, string NodeName)
		{
			ProjectGroup _return = new ProjectGroup();
			//---是否位于结尾
			if (xmlRead.EOF)
			{
				return null;
			}
			_return._name = string.Empty;
			//---读取包含的文件信息
			do
			{
				//---Files后的节点深度是5
				if (xmlRead.Depth != 3)
				{
					continue;
				}
				XmlReader subXmlRead = xmlRead.ReadSubtree();
				if (subXmlRead.Read() && (subXmlRead.NodeType == XmlNodeType.Element))
				{
					//---读取当前节点以及子节点
					XmlReader subsubXmlRead = subXmlRead.ReadSubtree();
					do
					{
						subsubXmlRead.Read();
						switch (subsubXmlRead.Name)
						{
							case "FileName":
								subsubXmlRead.Read();
								if (/*subsubXmlRead.Read() && */(subsubXmlRead.NodeType == XmlNodeType.Text) && (subsubXmlRead.HasValue) && (subsubXmlRead.Depth == 3))
								{
									_return._file.Add(new ProjectFile());
									string temp = subsubXmlRead.Value;
									temp = temp.Replace("$PROJ_DIR$\\", "");
									temp = temp.Replace("$PROJ_DIR$/", "");
									temp = temp.Replace("\\", "/");
									_return._file.Last()._name = temp;
								}
								break;
							case "excluded":
								if (subsubXmlRead.NodeType == XmlNodeType.Element)
								{
									XmlReader subSubSubXmlRead = subsubXmlRead.ReadSubtree();
									do
									{
										if (subSubSubXmlRead.Read() && (subSubSubXmlRead.NodeType == XmlNodeType.Text) && (subSubSubXmlRead.HasValue))
										{
											_return._file.Last()._exclude.Add(subSubSubXmlRead.Value);
										}
									}
									while (!subSubSubXmlRead.EOF);
									subSubSubXmlRead.Close();
								}
								break;
						}
					}
					while (!subsubXmlRead.EOF);
					subsubXmlRead.Close();
				}
			}
			while (xmlRead.ReadToNextSibling(NodeName));
			return _return;
		}

		#endregion

	}
}
