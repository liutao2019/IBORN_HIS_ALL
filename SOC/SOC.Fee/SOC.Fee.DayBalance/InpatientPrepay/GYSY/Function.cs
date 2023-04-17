using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Fee.DayBalance.InpatientPrepay.GYSY
{
    class Function
    {
        public static FS.FrameWork.Models.NeuObject PopPrepayRetobj(string Begin, string End, int Type)
        {
            SOC.Fee.DayBalance.InpatientPrepay.GYSY.ucPopPrePay uc = new  ucPopPrePay();
            uc.Begin = Begin;
            uc.End = End;
            uc.InputType = Type;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            return uc.Object;
        }
        /// <summary>
        /// 弹预交金控件
        /// </summary>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string PopPrepay(string Begin, string End, int Type)
        {
            SOC.Fee.DayBalance.InpatientPrepay.GYSY.ucPopPrePay uc = new  ucPopPrePay();
            uc.Begin = Begin;
            uc.End = End;
            uc.InputType = Type;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            return uc.NO;
        }
    }
}
