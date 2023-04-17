using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// 病案借阅卡信息表实体 继承于 FS.FrameWork.Models.NeuObject
    /// ID 操作员编码 Name 操作员名称
    ///
    /// 
    /// </summary>
    [Serializable]
    public class ReadCard : FS.FrameWork.Models.NeuObject
    {
        public ReadCard()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //私有字段
        private string myCardID;
        private FS.HISFC.Models.Base.OperEnvironment myEmplInfo = new FS.HISFC.Models.Base.OperEnvironment();
        private FS.FrameWork.Models.NeuObject myDeptInfo = new FS.FrameWork.Models.NeuObject();
        private DateTime myOperDate;
        private string myValidFlag;
        private FS.FrameWork.Models.NeuObject myCancelOperInfo = new FS.FrameWork.Models.NeuObject();
        private DateTime myCancelDate;

        //EmployeeInfo
        /// <summary>
        /// 借阅证号
        /// </summary>
        public string CardID
        {
            get
            {
                if (myCardID == null)
                {
                    myCardID = "";
                }
                return myCardID;
            }
            set { myCardID = value; }
        }

        /// <summary>
        /// 员工信息 ID 编码 Name 姓名
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment EmployeeInfo
        {
            get
            {
                return myEmplInfo;
            }
            set { myEmplInfo = value; }
        }

        /// <summary>
        /// 科室信息 ID 编码 Name 科室名称
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeptInfo
        {
            get
            {
                return myDeptInfo;
            }
            set { myDeptInfo = value; }
        }



        /// <summary>
        /// 有效 1有效/2无效
        /// </summary>
        public string ValidFlag
        {
            get
            {
                if (myValidFlag == null)
                {
                    myValidFlag = "";
                }
                return myValidFlag;
            }
            set { myValidFlag = value; }
        }

        /// <summary>
        /// 作废人信息 ID 编码 Name 姓名
        /// </summary>
        public FS.FrameWork.Models.NeuObject CancelOperInfo
        {
            get
            {
                return myCancelOperInfo;
            }
            set { myCancelOperInfo = value; }
        }

        /// <summary>
        /// 作废时间
        /// </summary>
        public DateTime CancelDate
        {
            get
            {
                return myCancelDate;
            }
            set { myCancelDate = value; }
        }

        public new ReadCard Clone()
        {
            ReadCard ReadCardClone = base.MemberwiseClone() as ReadCard;

            ReadCardClone.myCancelOperInfo = this.myCancelOperInfo.Clone();
            ReadCardClone.myDeptInfo = this.myDeptInfo.Clone();
            ReadCardClone.myEmplInfo = this.myEmplInfo.Clone();

            return ReadCardClone;
        }

        #region
        /// <summary>
        /// 操作时间
        /// </summary>
        [Obsolete("废弃 用EmployeeInfo.OperTime代替", true)]
        public DateTime OperDate
        {
            get { return myOperDate; }
            set { myOperDate = value; }
        }
        /// <summary>
        /// 员工信息 ID 编码 Name 姓名
        /// </summary>
        [Obsolete("废弃 用EmployeeInfo代替", true)]
        public FS.HISFC.Models.Base.OperEnvironment EmplInfo
        {
            get
            {
                return myEmplInfo;
            }
            set { myEmplInfo = value; }
        }
        #endregion
    }
}
