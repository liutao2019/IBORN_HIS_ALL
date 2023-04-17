using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IOrderChooseUL
{
    public class SelectItem : FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderChooseUL
    {
        private string err = string.Empty;

        FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IOrderChooseUL.ucSelectItem ucChooseItem = null;

        private void init()
        {
            if (ucChooseItem == null)
            {
                ucChooseItem = new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IOrderChooseUL.ucSelectItem();
            }
            ucChooseItem.Clear();
        }

        #region IOrderChooseUL 成员

        public string Err
        {
            get
            {
                return this.err;
            }
            set
            {
                this.err = value;
            }
        }

        public int GetChooseUL(ref ArrayList alOrders)
        {
            this.init();
            DialogResult dr =  FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucChooseItem);
            if (dr == DialogResult.OK)
            {

                if (ucChooseItem.Orders == null)
                {
                    this.err = "检验项目选择控件错误";
                    return -1;
                }
                else
                {
                    alOrders = ucChooseItem.Orders;
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        #endregion
    }
}
