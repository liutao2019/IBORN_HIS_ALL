using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// 病案质量检测表实体。
    /// 继承FS.FrameWork.Models.NeuObject
    /// FS.FrameWork.Models.NeuObject.ID 操作员编码 FS.FrameWork.Models.NeuObject.Name 操作员姓名
    ///
    /// 
    /// </summary>
    [Serializable]
    public class QC : FS.FrameWork.Models.NeuObject
    {
        public QC()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        //私有字段
        private string myInpatientNO;
        private FS.FrameWork.Models.NeuObject myRuleInfo = new FS.FrameWork.Models.NeuObject();
        private decimal myMark;
        private string myDenyFlag;
        private DateTime myOperDate;
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();

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
        /// 住院流水号
        /// </summary>
        public string InpatientNO
        {
            get
            {
                return myInpatientNO;
            }
            set
            {
                myInpatientNO = value;
            }
        }

        /// <summary>
        /// 规则信息 ID 规则编码 Name 规则信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject RuleInfo
        {
            get
            {
                return myRuleInfo;
            }
            set
            {
                myRuleInfo = value;
            }
        }

        /// <summary>
        /// 得分
        /// </summary>
        public decimal Mark
        {
            get
            {
                return myMark;
            }
            set
            {
                myMark = value;
            }
        }

        /// <summary>
        /// 是否单项否定
        /// </summary>
        public string DenyFlag
        {
            get
            {
                return myDenyFlag;
            }
            set
            {
                myDenyFlag = value;
            }
        }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return myOperDate;
            }
            set
            {
                myOperDate = value;
            }
        }

        public new QC Clone()
        {
            QC QCCLone = base.MemberwiseClone() as QC;

            QCCLone.myRuleInfo = this.myRuleInfo.Clone();

            return QCCLone;
        }

    }
}
