using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Order.OutPatient
{
     [Serializable]
    public class DiagExtend : FS.FrameWork.Models.NeuObject
    {
        public DiagExtend()
        {

        }
        #region 变量

        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string clinicCode;

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string ClinicCode
        {
            get { return clinicCode; }
            set { clinicCode = value; }
        }

        /// <summary>
        /// 门诊号
        /// </summary>
        private string cardNo;

        /// <summary>
        /// 门诊号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
         /// <summary>
         /// 门诊诊断证明书序号
         /// </summary>
        private string proveNo;
        /// <summary>
        /// 门诊诊断证明书序号
        /// </summary>
         public string ProveNo
        {
            get { return proveNo; }
            set { proveNo = value; }
        }

         /// <summary>
         /// 门诊病假条序号
         /// </summary>
         private string leaveNo;
         /// <summary>
         /// 门诊诊断证明书序号
         /// </summary>
         public string LeaveNo
         {
             get { return leaveNo; }
             set { leaveNo = value; }
         }

         /// <summary>
         /// 门诊病假天数
         /// </summary>
         private string leaveDays;
         /// <summary>
         /// 门诊病假天数
         /// </summary>
         public string LeaveDays
         {
             get { return leaveDays; }
             set { leaveDays = value; }
         }

         /// <summary>
         /// 门诊病假开始时间
         /// </summary>
         private string leaveStart;
         /// <summary>
         /// 门诊病假开始时间
         /// </summary>
         public string LeaveStart
         {
             get { return leaveStart; }
             set { leaveStart = value; }
         }

         /// <summary>
         /// 门诊病假结束时间
         /// </summary>
         private string leaveEnd;
         /// <summary>
         /// 门诊病假结束时间
         /// </summary>
         public string LeaveEnd
         {
             get { return leaveEnd; }
             set { leaveEnd = value; }
         }

         /// <summary>
         /// 有效状态
         /// </summary>
         private string validFlag;
         /// <summary>
         /// 有效状态
         /// </summary>
         public string ValidFlag
         {
             get { return validFlag; }
             set { validFlag = value; }
         }

         /// <summary>
         /// 门诊诊断证明打印时间
         /// </summary>
         private string provePrintDate;
         /// <summary>
         /// 门诊诊断证明打印时间
         /// </summary>
         public string ProvePrintDate
         {
             get { return provePrintDate; }
             set { provePrintDate = value; }
         }

         /// <summary>
         /// 门诊病假建议书打印时间
         /// </summary>
         private string leavePrintDate;
         /// <summary>
         /// 门诊病假建议书打印时间
         /// </summary>
         public string LeavePrintDate
         {
             get { return leavePrintDate; }
             set { leavePrintDate = value; }
         }


         /// <summary>
         /// 主诉
         /// </summary>
         private string caseMain;
         /// <summary>
         /// 主诉
         /// </summary>
         public string CaseMain
         {
             get { return caseMain; }
             set { caseMain = value; }
         }

         /// <summary>
         /// 现病史
         /// </summary>
         private string caseNow;
         /// <summary>
         /// 现病史
         /// </summary>
         public string CaseNow
         {
             get { return caseNow; }
             set { caseNow = value; }
         }

         /// <summary>
         /// 治疗意见
         /// </summary>
         private string opinions;
         /// <summary>
         /// 治疗意见
         /// </summary>
         public string Opinions
         {
             get { return opinions; }
             set { opinions = value; }
         }

         /// <summary>
         /// 请假类型
         /// </summary>
         private string leaveType;
         /// <summary>
         /// 请假类型
         /// </summary>
         public string LeaveType
         {
             get { return leaveType; }
             set { leaveType = value; }
         }
        #endregion


         public new DiagExtend Clone()
         {
             DiagExtend diagExtend = base.Clone() as DiagExtend;
             return diagExtend;
         }
    }
}
