using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Public;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.Public.Ini;
using System.Xml.Linq;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Common
{
    public partial class NeuCombox : FS.FrameWork.WinForms.Controls.NeuComboBox, FS.SOC.HISFC.RADT.Interface.Common.IInputControl
    {
        #region  私有变量       
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();     
        bool isFind = false;//根据名称找不到编码时清空数据
        #endregion

        #region 属性        
        [Description("根据名称找不到编码时清空数据")]
        public bool IsFind
        {
            get
            {
                return isFind;
            }
            set
            {
                isFind = value;
            }
        }       
        #endregion
        
        public NeuCombox()
        {
            
        }
        #region 公开函数
        #region 加载项目
        /// <summary>
        /// 加载项目
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public new int AddItems(ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            //如果不是必填，则默认为空
            //if (!this.isTagInput && !this.isTextInput)
            //{
            //    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject(".", "", "");
            //    list.Insert(0, obj);
            //}
            objHelper.ArrayObject = list;
            return base.AddItems(list);            
        }
        #endregion

        #region 清空数据
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        public void Reset()
        {
            this.Tag = null;
            this.Text = "";
        }
        #endregion 
        #endregion

        #region IInputControl 成员

        private bool isTextInput = false;
        public bool IsTextInput
        {
            get
            {
                return isTextInput;
            }
            set
            {
                isTextInput = value;
            }
        }

        private bool isTagInput = false;
        public bool IsTagInput
        {
            get
            {
                return isTagInput;
            }
            set
            {
                isTagInput = value;
            }
        }

        private string inputMsg = "";
        public string InputMsg
        {
            get
            {
                return inputMsg;
            }
            set
            {
                inputMsg = value;
            }
        }

        public bool IsValidInput()
        {
            if (isTextInput && string.IsNullOrEmpty(this.Text))
            {
                return false;
            }

            if (isTagInput && (this.Tag == null || string.IsNullOrEmpty(this.Tag.ToString())))
            {
                return false;
            }

            return true;
        }

        public void ReadConfig(System.Xml.Linq.XElement doc)
        {
            if (doc.Element(this.Name) != null)
            {
                this.InputMsg = doc.Element(this.Name).Attribute("InputMsg").Value;
                this.IsTagInput = FS.FrameWork.Function.NConvert.ToBoolean(doc.Element(this.Name).Attribute("IsTagInput").Value);
                this.IsTextInput = FS.FrameWork.Function.NConvert.ToBoolean(doc.Element(this.Name).Attribute("IsTextInput").Value);
                this.IsDefaultCHInput = FS.FrameWork.Function.NConvert.ToBoolean(doc.Element(this.Name).Attribute("IsDefaultCHInput").Value);
                this.Enabled = FS.FrameWork.Function.NConvert.ToBoolean(doc.Element(this.Name).Attribute("Enabled").Value);
                this.TabIndex = FS.FrameWork.Function.NConvert.ToInt32(doc.Element(this.Name).Attribute("TabIndex").Value); ;
            }

        }

        public void SaveConfig(System.Xml.Linq.XElement doc)
        {
            if (doc.Element(this.Name) != null)
            {
                doc.Element(this.Name).RemoveAll();
            }

            doc.Add(new XElement(this.Name, new XAttribute("IsTagInput", this.IsTagInput), new XAttribute("IsTextInput", this.IsTextInput), new XAttribute("InputMsg", this.InputMsg), new XAttribute("Enabled", this.Enabled), new XAttribute("IsDefaultCHInput", this.IsDefaultCHInput), new XAttribute("TabIndex", this.TabIndex)));
        }


        public bool IsDefaultCHInput
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        #endregion
    }
}
