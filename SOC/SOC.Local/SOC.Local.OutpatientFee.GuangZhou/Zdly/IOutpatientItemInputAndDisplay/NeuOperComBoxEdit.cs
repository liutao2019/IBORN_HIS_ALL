using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.FrameWork.WinForms.Controls;
using System.Windows.Forms;
using Neusoft.FrameWork.Public;
using System.Collections;

namespace Neusoft.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
{
    public class NeuOperComBoxEdit:NeuComboBoxEdit
    {
        public NeuOperComBoxEdit()
        {
            IsMultiSelect = true;
            QueryItems();
            this.BringToFront();
        }
        Neusoft.HISFC.BizLogic.Manager.Person personMgr = new Neusoft.HISFC.BizLogic.Manager.Person();

        private string strSelectedValues = "";
        [Description("选择项的Value值(字符串)")]
        public string StrSelectedValues
        {
            get
            {
                if (IsMultiSelect)
                {
                    strSelectedValues = "";
                    foreach (KeyValuePair<object, string> m in SelectItems)
                    {
                        strSelectedValues = strSelectedValues + m.Key.ToString() + ",";
                    }
                    strSelectedValues = strSelectedValues.Trim(',');
                    return strSelectedValues;
                }
                return strSelectedValues;
            }
        }

        private void QueryItems()
        {
            ArrayList alPerson = personMgr.GetEmployee(Neusoft.HISFC.Models.Base.EnumEmployeeType.F);
            this.DataSource = alPerson;
            this.DisplayMember = "Name";
            this.ValueMember = "ID";

        }
    }
}
