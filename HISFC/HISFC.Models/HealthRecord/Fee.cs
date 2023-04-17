using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// Fee 的摘要说明。ID 操作员编码 Name 操作员姓名
    /// </summary>
    [Serializable]
    public class Fee : FS.FrameWork.Models.NeuObject
    {
        public Fee()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 私有变量

        private string inpatientNO;
        private FS.FrameWork.Models.NeuObject myDeptInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject myMainOutICD = new FS.FrameWork.Models.NeuObject();
        private decimal totCost;
        private DateTime outDate;
        private DateTime operDate;
        private FS.FrameWork.Models.NeuObject myFeeInfo = new FS.FrameWork.Models.NeuObject();
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region 属性

        /// <summary>
        /// 住院流水号
        /// </summary>
        public string InpatientNO
        {
            get
            {
                return inpatientNO;
            }
            set
            {

                inpatientNO = value;
            }
        }

        /// <summary>
        /// 科室信息(是否要考虑患者转科情况) ID 科室编码 Name 科室名称
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeptInfo
        {
            get
            {
                return myDeptInfo;
            }
            set
            {
                myDeptInfo = value;
            }
        }
        /// <summary>
        /// 出院主诊断信息 ID 主诊断信息 Name 诊断名称
        /// </summary>
        public FS.FrameWork.Models.NeuObject MainOutICD
        {
            get
            {
                return myMainOutICD;
            }
            set
            {
                myMainOutICD = value;
            }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotCost
        {
            get
            {
                return totCost;
            }
            set
            {
                totCost = value;
            }
        }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime OutDate
        {
            get
            {
                return outDate;
            }
            set
            {
                outDate = value;
            }
        }
        /// <summary>
        /// 操作员类
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }
        /// <summary>
        /// 费用信息 ID 费用大类代码 Name 费用大类名称
        /// </summary>
        public FS.FrameWork.Models.NeuObject FeeInfo
        {
            get
            {
                return myFeeInfo;
            }
            set
            {
                myFeeInfo = value;
            }
        }

        #endregion

        #region 公用函数


        /// <summary>
        /// 克隆本体
        /// </summary>
        /// <returns>Case.Fee</returns>
        public new Fee Clone()
        {
            Fee FeeClone = base.MemberwiseClone() as Fee;

            FeeClone.FeeInfo = this.FeeInfo.Clone();
            FeeClone.DeptInfo = this.DeptInfo.Clone();
            FeeClone.MainOutICD = this.MainOutICD.Clone();
            FeeClone.operInfo = operInfo.Clone();

            return FeeClone;
        }

        #endregion

        #region 废弃
        /// <summary>
        /// 操作日期
        /// </summary>
        [Obsolete("废弃 用 OperInfo.OperTime 代替", true)]
        public DateTime OperDate
        {
            get
            {
                return operDate;
            }
            set
            {
                operDate = value;
            }
        }
        #endregion
    }
}
