using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.Public.Ini;
using System.Xml.Linq;

namespace FS.SOC.HISFC.RADT.Components.Common
{
    public class NeuInputDateTime : System.Windows.Forms.MaskedTextBox, FS.SOC.HISFC.RADT.Interface.Common.IInputControl
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

        public bool IsTagInput
        {
            get
            {
                return false;
            }
            set
            {
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
            if (isTextInput)
            {
                if (FS.FrameWork.Function.NConvert.ToDateTime(this.Text) <= DateTime.MinValue)
                {
                    return false;
                }
            }

            return true;
        }

        public void ReadConfig(System.Xml.Linq.XElement doc)
        {
            if (doc.Element(this.Name) != null)
            {
                this.Mask = doc.Element(this.Name).Attribute("Mask").Value;
                this.isTextInput = FS.FrameWork.Function.NConvert.ToBoolean(doc.Element(this.Name).Attribute("IsTextInput").Value);
                this.inputMsg = doc.Element(this.Name).Attribute("InputMsg").Value;
                this.TabIndex = FS.FrameWork.Function.NConvert.ToInt32(doc.Element(this.Name).Attribute("TabIndex").Value);
            }
        }

        public void SaveConfig(System.Xml.Linq.XElement doc)
        {
            if (doc.Element(this.Name) != null)
            {
                doc.Element(this.Name).RemoveAll();
            }


            doc.Add(new XElement(this.Name, new XAttribute("Mask", this.Mask), new XAttribute("IsTextInput", this.IsTextInput),new XAttribute("InputMsg", this.InputMsg),new XAttribute("TabIndex",this.TabIndex)));
        }

        #endregion

        #region IInputControl 成员

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
