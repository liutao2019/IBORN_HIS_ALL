using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Public;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.Public.Ini;
using System.Xml.Linq;

namespace FS.SOC.HISFC.RADT.Components.Common
{
    public class NeuInputTextBox :FS.FrameWork.WinForms.Controls.NeuTextBox, FS.SOC.HISFC.RADT.Interface.Common.IInputControl
    {

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

        private bool isDefaultCHInput = false;
        public bool IsDefaultCHInput
        {
            get
            {
                return isDefaultCHInput;
            }
            set
            {
                isDefaultCHInput = value;
            }
        }

        #endregion
    }
}
