using System;

namespace FS.HISFC.DCP.Object
{
    /// <summary>
    /// InfectAddtionReport<br></br>
    /// [功能描述: InfectAddtionReport]<br></br>
    /// [创 建 者: zengft]<br></br>
    /// [创建时间: 2007-12-8]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class SexReport : FS.FrameWork.Models.NeuObject
    {
        #region 私有字段

        /*
        Item marriage = new Item();
		Item nation = new Item();
		Item education = new Item();
		Item houseHoldRegion = new Item();
		Address houseHoldAddress;
		string houseHoldName;
		string contactHistory ;
		Item sexHistory = new Item();
		Item possiableInfWay = new Item();
		Item labelSource = new Item();
		Item libResult = new Item();
		DateTime libDate = DateTime.MinValue;
		string libDeptName;
		ArrayList alChild = new ArrayList();
        AdditionOther shayanAdditon = new AdditionOther();
        */


        /// <summary>
        /// 患者住院号或病历号
        /// </summary>
        private string patientNo = "";

        /// <summary>
        /// 婚姻装况[0 未婚 1已婚 2同居 3离异或丧偶 4不详]
        /// </summary>
        private FS.FrameWork.Models.NeuObject marigerStatus = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 民族
        /// </summary>
        private FS.FrameWork.Models.NeuObject nation = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 文化程度
        /// </summary>
        private FS.FrameWork.Models.NeuObject education = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 户籍所在地
        /// </summary>
        private FS.FrameWork.Models.NeuObject houseHoldRegion = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 户籍地址
        /// </summary>
        private FS.FrameWork.Models.NeuObject houseHold = new FS.FrameWork.Models.NeuObject(); 

        private FS.FrameWork.Models.NeuObject contactHistory = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject sexHistory = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject possiableInfWay = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject labelSource = new FS.FrameWork.Models.NeuObject();
        
        private FS.FrameWork.Models.NeuObject libResult = new FS.FrameWork.Models.NeuObject();
        DateTime libDate = DateTime.MinValue;
        private FS.FrameWork.Models.NeuObject libDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 有效性
        /// </summary>
        private bool isValid = true;

        #endregion

        #region 共有字段

        /// <summary>
        /// 患者住院号或病历号
        /// </summary>
        public string PatientNo
        {
            get
            {
                return this.patientNo;
            }
            set
            {
                this.patientNo = value;
            }
        }

        /// <summary>
        /// 婚姻装况[0 未婚 1已婚 2同居 3离异或丧偶 4不详]
        /// </summary>
        public FS.FrameWork.Models.NeuObject MarigerStatus        {
            get
            {
                return this.marigerStatus;
            }
            set
            {
                this.marigerStatus = value;
            }
        }

        /// <summary>
        /// 民族
        /// </summary>
        public FS.FrameWork.Models.NeuObject Nation
        {
            get
            {
                return this.nation;
            }
            set
            {
                this.nation = value;
            }
        }
        /// <summary>
        /// 文化程度
        /// </summary>
        public FS.FrameWork.Models.NeuObject Education
        {
            get
            {
                return this.education;
            }
            set
            {
                this.education = value;
            }
        }

        /// <summary>
        /// 户籍所在地
        /// </summary>
        public FS.FrameWork.Models.NeuObject HouseHoldRegion
        {
            get
            {
                return this.houseHoldRegion;
            }
            set
            {
                this.houseHoldRegion = value;
            }
        }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public FS.FrameWork.Models.NeuObject HouseHold
        {
            get
            {
                return this.houseHold;
            }
            set
            {
                this.houseHold = value;
            }
        }

        public FS.FrameWork.Models.NeuObject ContactHistory
        {
            get
            {
                return this.contactHistory;
            }
            set
            {
                this.contactHistory = value;
            }
        }
        public FS.FrameWork.Models.NeuObject SexHistory
        {
            get
            {
                return this.sexHistory;
            }
            set
            {
                this.sexHistory = value;
            }
        }
        public FS.FrameWork.Models.NeuObject PossiableInfWay
        {
            get
            {
                return this.possiableInfWay;
            }
            set
            {
                this.possiableInfWay = value;
            }
        }
        public FS.FrameWork.Models.NeuObject LabelSource
        {
            get
            {
                return this.labelSource;
            }
            set
            {
                this.labelSource = value;
            }
        }

        public FS.FrameWork.Models.NeuObject LibResult
        {
            get
            {
                return this.libResult;
            }
            set
            {
                this.libResult = value;
            }
        }
        public DateTime LibDate
        {
            get
            {
                return this.libDate;
            }
            set
            {
                this.libDate = value;
            }
        }
        public FS.FrameWork.Models.NeuObject LibDept
        {
            get
            {
                return this.libDept;
            }
            set
            {
                this.libDept = value;
            }
        }

        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        public SexReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 克隆函数
        public SexReport Clone()
        {
            SexReport sexReport = new SexReport();
            sexReport.contactHistory.Clone();
            sexReport.education.Clone();
            sexReport.houseHoldRegion.Clone();
            sexReport.labelSource.Clone();
            sexReport.libDept.Clone();
            sexReport.libResult.Clone();
            sexReport.marigerStatus.Clone();
            sexReport.nation.Clone();
            sexReport.possiableInfWay.Clone();
            sexReport.sexHistory.Clone();
            
            return sexReport;
        }
        #endregion
    }
}
