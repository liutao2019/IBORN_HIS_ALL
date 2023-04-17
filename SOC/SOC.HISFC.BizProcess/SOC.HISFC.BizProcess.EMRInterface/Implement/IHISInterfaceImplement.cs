using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.EMRInterface.Implement
{
    /// <summary>
    ///  EMR用于HIS启动的接口实现
    /// </summary>
    public class IHISInterfaceImplement : Interface.IHISInterface
    {
        #region IHISInterface 成员

        public int EMRInit()
        {
            #region Dawn嵌入
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode node = null;
            doc.Load(Application.StartupPath + "\\Setting\\EmrSetting.xml");
            node = doc.SelectSingleNode("/设置/是否Dawn20版电子病历");
            bool UseDawnFlag = false;

            if (node == null)
            {
                UseDawnFlag = false;
            }
            else
            {
                if (node.InnerXml.ToLower() == "true")
                {
                    UseDawnFlag = true;
                }
                else
                {
                    UseDawnFlag = false;
                }
            }

            if (UseDawnFlag)
            {
                //FS.Dawn.AppInterface.DawnAppInterface.DawnInitialize();
            }
            else
            {
                //FS.Emr.AppInterface.EmrAppUtils.AppInitialize();
            }
            #endregion
            return 1;
        }

        public int EMRLogIn(string employeeId, string deptId)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode node = null;
            doc.Load(Application.StartupPath + "\\Setting\\EmrSetting.xml");
            node = doc.SelectSingleNode("/设置/是否Dawn20版电子病历");
            bool UseDawnFlag = false;

            if (node == null)
            {
                UseDawnFlag = false;
            }
            else
            {
                if (node.InnerXml.ToLower() == "true")
                {
                    UseDawnFlag = true;
                }
                else
                {
                    UseDawnFlag = false;
                }
            }

            if (UseDawnFlag)
            {
                //FS.Dawn.AppInterface.DawnAppInterface.UserLogin(employeeId, "", employeeId, deptId, "", "00", true);
            }
            else
            {
                //FS.Emr.AppInterface.EmrAppUtils.UserLogout();
                //FS.Emr.AppInterface.EmrAppUtils.UserLogin(employeeId, deptId);
            }
            return 1;
        }

        public int EMRLogOut(string employeeId, string deptId)
        {
            #region Dawn嵌入
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode node = null;
            doc.Load(Application.StartupPath + "\\Setting\\EmrSetting.xml");
            node = doc.SelectSingleNode("/设置/是否Dawn20版电子病历");
            bool UseDawnFlag = false;

            if (node == null)
            {
                UseDawnFlag = false;
            }
            else
            {
                if (node.InnerXml.ToLower() == "true")
                {
                    UseDawnFlag = true;
                }
                else
                {
                    UseDawnFlag = false;
                }
            }

            if (UseDawnFlag)
            {
                //FS.Dawn.AppInterface.DawnAppInterface.UserLogout();
            }
            else
            {
                //FS.Emr.AppInterface.EmrAppUtils.UserLogout();
            }
            #endregion
            return 1;
        }

        public int EMRClose()
        {
            //FS.Emr.AppInterface.EmrAppUtils.AppFinalize();
            return 1;
        }

        #endregion
    }
}
