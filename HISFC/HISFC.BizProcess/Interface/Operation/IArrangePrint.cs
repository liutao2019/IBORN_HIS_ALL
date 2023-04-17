using System;
using System.Collections.Generic;

namespace FS.HISFC.BizProcess.Interface.Operation
{
    public interface IArrangePrint : FS.FrameWork.WinForms.Forms.IReportPrinter 
    {
        DateTime Date { set; }
        
        string Title { set; }

        void AddAppliction(FS.HISFC.Models.Operation.OperationAppllication appliction);
        void Reset();

        EnumArrangeType ArrangeType
        {
            get;
            set;
        }


    }
    public enum EnumArrangeType
    {
        /// <summary>
        /// 手术安排
        /// </summary>
        Operation,
        /// <summary>
        /// 麻醉安排
        /// </summary>
        Anaesthesia
    }
}
