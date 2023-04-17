using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 制剂函数功能类]<br></br>
    /// [创 建 者: 飞斯]<br></br>
    /// [创建时间: 2007-09]<br></br>
    /// <说明>
    ///    
    /// </说明>
    /// </summary>
    public class Function
    {
        /// <summary>
        /// 获取所有公开属性(可读写)名称
        /// </summary>
        /// <param name="t">类型</param>
        /// <returns>成功返回属性字符串数组</returns>
        public static List<string> GetProperties(Type t)
        {
            List<string> propertyName = new List<string>();
            System.Reflection.PropertyInfo[] recordList = t.GetProperties();

            foreach (System.Reflection.PropertyInfo p in recordList)
            {
                if (p.CanRead && p.CanWrite)
                {
                    propertyName.Add(p.Name);
                }
            }

            return propertyName;
        }

        /// <summary>
        /// 获取远程配置文件
        /// </summary>
        /// <returns>成功返回远程配置文件信息 失败返回null</returns>
        public static System.Xml.XmlDocument GetConfig(string xmlName)
        {
            #region 获取配置文件路径

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(Application.StartupPath + "\\url.xml");

            System.Xml.XmlNode node = doc.SelectSingleNode("//dir");
            if (node == null)
            {
                MessageBox.Show(Language.Msg("url中找dir结点出错！"));
            }

            string serverPath = node.InnerText;
            string configPath = "//" + xmlName; //远程配置文件名 

            #endregion

            try
            {
                doc.Load(serverPath + configPath);
            }
            catch (System.Net.WebException)
            {

            }
            catch (System.IO.FileNotFoundException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("装载Config.xml失败！\n" + ex.Message));
            }

            return doc;
        }

        /// <summary>
        /// 获取工作流设置信息
        /// </summary>
        /// <param name="t">类型</param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.WorkFlow GetWorkFlowSetting(string groupName,string state)
        {
            System.Xml.XmlDocument doc = Function.GetConfig("WorkFlow.xml");
            if (doc == null)
            {
                return null;
            }

            System.Xml.XmlNode wfNode = doc.SelectSingleNode(string.Format("/WorkFlow/Group[@ID='{0}']/State[@ID='{1}']",groupName,state));
            if (wfNode != null)
            {
                FS.HISFC.Models.Base.WorkFlow wfObject = new FS.HISFC.Models.Base.WorkFlow();
                wfObject.State = wfNode.Attributes["ID"].Value;
                wfObject.NextState = wfNode.Attributes["Next"].Value;

                System.Xml.XmlNode competenceNode = wfNode.SelectSingleNode(string.Format("/WorkFlow/Group[@ID='{0}']/State[@ID='{1}']/Competence",groupName,state));
                wfObject.CompetenceList = new List<string>();                
                foreach (System.Xml.XmlNode node in competenceNode.ChildNodes)
                {
                    if (node.Name == "IsNeed")
                    {
                        wfObject.IsNeedCompetence = FS.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                    }
                    else if (node.Name == "Code")                        
                    {
                        foreach (System.Xml.XmlNode codeNode in node.ChildNodes)
                        {
                            if (codeNode.Name == "Value")
                            {
                                wfObject.CompetenceList.Add(codeNode.InnerText);
                            }
                        }
                    }
                }
                System.Xml.XmlNode paramNode = wfNode.SelectSingleNode(string.Format("/WorkFlow/Group[@ID='{0}']/State[@ID='{1}']/Params", groupName, state));
                wfObject.ParamList = new List<FS.FrameWork.Models.NeuObject>();
                foreach (System.Xml.XmlNode node in paramNode.ChildNodes)
                {
                    if (node.NodeType != System.Xml.XmlNodeType.Comment)
                    {
                        FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                        info.ID = node.Name;
                        info.Name = node.InnerText;
                        wfObject.ParamList.Add(info);
                    }
                }

                return wfObject;
            }

            return null;
        }

        /// <summary>
        /// 设置工作流设置信息
        /// </summary>
        /// <param name="t">类型</param>
        /// <returns></returns>
        public static int SetWorkFlowSetting<T>(T t, string groupName, string state)
        {
            FS.HISFC.Models.Base.WorkFlow wf = Function.GetWorkFlowSetting(groupName, state);
            if (wf == null)
            {
                return -1;
            }

            Type type = typeof(T);

            try
            {
                foreach (FS.FrameWork.Models.NeuObject property in wf.ParamList)
                {
                    if (property.Name == "")
                    {
                        continue;
                    }
                    System.Reflection.PropertyInfo p = type.GetProperty(property.ID);
                    if (p == null)
                    {
                        continue;
                    }

                    object pValue = null;
                    switch (p.PropertyType.FullName)
                    {
                        case "System.String":
                            pValue = property.Name;
                            break;
                        case "System.Boolenn":
                            pValue = FS.FrameWork.Function.NConvert.ToBoolean(property.Name);
                            break;
                        case "System.Int32":
                            pValue = FS.FrameWork.Function.NConvert.ToInt32(property.Name);
                            break;
                        case "System.Decimal":
                            pValue = FS.FrameWork.Function.NConvert.ToDecimal(property.Name);
                            break;
                        case "System.DateTime":
                            pValue = FS.FrameWork.Function.NConvert.ToDateTime(property.Name);
                            break;
                        case "System.Enum":
                            pValue = Enum.Parse(p.PropertyType, property.Name);
                            break;
                    }

                    p.SetValue(t, pValue, null);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return 1;
        }

        #region 工艺流程记录相关函数

        public static string NoneData = "NoneData";

        /// <summary>
        /// 设置工艺流程执行信息
        /// </summary>
        /// <param name="processList">工艺流程执行信息</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public static int SetProcessItem(List<FS.HISFC.Models.Preparation.Process> processList, System.Collections.Hashtable hsProcessControl)
        {
            foreach (FS.HISFC.Models.Preparation.Process info in processList)
            {
                if (hsProcessControl.ContainsKey(info.ProcessItem.ID))
                {
                    Control c = hsProcessControl[info.ProcessItem.ID] as Control;
                    switch (c.GetType().ToString())
                    {
                        case "FS.FrameWork.WinForms.Controls.NeuComboBox":
                            FS.FrameWork.WinForms.Controls.NeuComboBox neuCombo = c as FS.FrameWork.WinForms.Controls.NeuComboBox;
                            neuCombo.Tag = info.ResultStr;
                            break;
                        case "FS.FrameWork.WinForms.Controls.NeuTextBox":
                            c.Text = info.ResultStr;
                            break;
                        case "FS.FrameWork.WinForms.Controls.NeuDateTimePicker":
                            FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDate = c as FS.FrameWork.WinForms.Controls.NeuDateTimePicker;
                            neuDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(info.ResultStr);
                            break;
                        case "FS.FrameWork.WinForms.Controls.NeuNumericTextBox":
                            c.Text = info.ResultQty.ToString();
                            break;
                        case "FS.FrameWork.WinForms.Controls.ComboBox":
                            c.Text = info.ResultStr;
                            break;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 获取配置工艺流程执行信息
        /// </summary>
        /// <returns></returns>
        public static int GetProcessItemList(Control controlCollect, ref System.Collections.Hashtable hsProcess)
        {
            if (controlCollect.Controls.Count == 0 || controlCollect is FS.FrameWork.WinForms.Controls.NeuComboBox)
            {
                if (hsProcess.ContainsKey(controlCollect.Name))
                {
                    FS.HISFC.Models.Preparation.Process p = hsProcess[controlCollect.Name] as FS.HISFC.Models.Preparation.Process;
                    Function.GetProcessItem(controlCollect, ref p);
                }
            }
            else
            {
                foreach (Control c in controlCollect.Controls)
                {
                    Function.GetProcessItemList(c, ref hsProcess);
                }
            }
            return 1;
        }

        /// <summary>
        /// 根据控件信息返回类实体信息
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private static int GetProcessItem(Control control, ref FS.HISFC.Models.Preparation.Process p)
        {
            if (control is FS.FrameWork.WinForms.Controls.NeuComboBox)
            {
                FS.FrameWork.WinForms.Controls.NeuComboBox neuCombo = control as FS.FrameWork.WinForms.Controls.NeuComboBox;
                p.ResultStr = neuCombo.Tag.ToString();
            }
            else if (control is FS.FrameWork.WinForms.Controls.NeuComboBox)
            {
                p.ResultStr = control.Text;
            }
            else if (control is FS.FrameWork.WinForms.Controls.NeuNumericTextBox)
            {
                p.ResultQty = FS.FrameWork.Function.NConvert.ToDecimal(control.Text);
                p.ResultStr = control.Text;
            }
            else if (control is FS.FrameWork.WinForms.Controls.NeuTextBox)
            {
                p.ResultStr = control.Text;
            }
            else if (control is FS.FrameWork.WinForms.Controls.NeuDateTimePicker)
            {
                FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDate = control as FS.FrameWork.WinForms.Controls.NeuDateTimePicker;
                p.ResultStr = neuDate.Value.ToString();
            }
            

            return 1;
        }

        #endregion
    }
}
