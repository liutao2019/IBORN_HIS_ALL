using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.Public.Ini;
using System.Xml.Linq;

namespace FS.SOC.HISFC.RADT.Components.Common
{
   public  class NeuInputLabel:FS.FrameWork.WinForms.Controls.NeuLabel,FS.SOC.HISFC.RADT.Interface.Common.IInputControl
    {
        #region IInputControl 成员

        public bool IsTextInput
        {
            get
            {
                return false;
            }
            set
            {
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

        public string InputMsg
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public bool IsValidInput()
        {
            return true;
        }

        public void ReadConfig(System.Xml.Linq.XElement doc)
        {
            if (doc.Element(this.Name) != null)
            {
                this.ForeColor = System.Drawing.Color.FromArgb(
                    FS.FrameWork.Function.NConvert.ToInt32(doc.Element(this.Name).Attribute("ForeColorRed").Value),
                    FS.FrameWork.Function.NConvert.ToInt32(doc.Element(this.Name).Attribute("ForeColorGreen").Value),
                    FS.FrameWork.Function.NConvert.ToInt32(doc.Element(this.Name).Attribute("ForeColorBlue").Value));
                this.Text = doc.Element(this.Name).Attribute("Text").Value;
            }
        }

        public void SaveConfig(System.Xml.Linq.XElement doc)
        {
            if (doc.Element(this.Name) != null)
            {
                doc.Element(this.Name).RemoveAll();
            }

            doc.Add(new XElement(this.Name, new XAttribute("ForeColorRed", this.ForeColor.R), new XAttribute("ForeColorGreen", this.ForeColor.G), new XAttribute("ForeColorBlue", this.ForeColor.B), new XAttribute("Text", this.Text)));
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
