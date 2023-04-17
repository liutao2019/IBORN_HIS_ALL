using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ�����������]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public class Function
    {
        /// <summary>
        /// ��ȡ���й�������(�ɶ�д)����
        /// </summary>
        /// <param name="t">����</param>
        /// <returns>�ɹ����������ַ�������</returns>
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
        /// ��ȡԶ�������ļ�
        /// </summary>
        /// <returns>�ɹ�����Զ�������ļ���Ϣ ʧ�ܷ���null</returns>
        public static System.Xml.XmlDocument GetConfig(string xmlName)
        {
            #region ��ȡ�����ļ�·��

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(Application.StartupPath + "\\url.xml");

            System.Xml.XmlNode node = doc.SelectSingleNode("//dir");
            if (node == null)
            {
                MessageBox.Show(Language.Msg("url����dir������"));
            }

            string serverPath = node.InnerText;
            string configPath = "//" + xmlName; //Զ�������ļ��� 

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
                MessageBox.Show(Language.Msg("װ��Config.xmlʧ�ܣ�\n" + ex.Message));
            }

            return doc;
        }

        /// <summary>
        /// ��ȡ������������Ϣ
        /// </summary>
        /// <param name="t">����</param>
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
        /// ���ù�����������Ϣ
        /// </summary>
        /// <param name="t">����</param>
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

        #region �������̼�¼��غ���

        public static string NoneData = "NoneData";

        /// <summary>
        /// ���ù�������ִ����Ϣ
        /// </summary>
        /// <param name="processList">��������ִ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
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
        /// ��ȡ���ù�������ִ����Ϣ
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
        /// ���ݿؼ���Ϣ������ʵ����Ϣ
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
