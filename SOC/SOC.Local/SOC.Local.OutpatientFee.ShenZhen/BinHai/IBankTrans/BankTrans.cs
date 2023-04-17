using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IBankTrans
{
    public class BankTrans : FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans
    {
        #region IBankTrans 成员

        public bool Do()
        {
            this.outputListInfo.Clear();
            this.outputListInfo.Add("");
            this.outputListInfo.Add("");
            this.outputListInfo.Add("");
            if (this.inputListInfo.Count > 1)
            {
                this.outputListInfo.Add(this.inputListInfo[1]);
            }
            else
            {
                this.outputListInfo.Add(0);
            }

            return true;
        }

        private List<object> inputListInfo = new List<object>();

        public List<object> InputListInfo
        {
            get
            {
                return inputListInfo;
            }
            set
            {
                inputListInfo = value;
            }
        }

        private List<object> outputListInfo = new List<object>();

        public List<object> OutputListInfo
        {
            get
            {
                return outputListInfo;
            }
            set
            {
                outputListInfo = value;
            }
        }

        #endregion
    }
}
