using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// 住院日报实体
    /// ID   科室编码
    /// Name 科室名称
    /// </summary>
    [Serializable]
    public class InpatientDayReport : FS.FrameWork.Models.NeuObject
    {
        private System.DateTime myDateStat;
        private FS.FrameWork.Models.NeuObject nurseStation = new FS.FrameWork.Models.NeuObject();
        private System.Int32 myBedStand;
        private System.Int32 myBedAdd;
        private System.Int32 myBedFree;
        private System.Int32 myBeginningNum;
        private System.Int32 myInNormal;
        private System.Int32 myInEmergency;
        private System.Int32 myInTransfer;
        private System.Int32 myInTransferInner;
        private System.Int32 myInReturn;
        private System.Int32 myOutNormal;
        private System.Int32 myOutTransfer;
        private System.Int32 myOutTransferInner;
        private System.Int32 myOutWithdrawal;
        private System.Int32 myEndNum;
        private System.Int32 myDeadIn24;
        private System.Int32 myDeadOut24;
        private System.Decimal myBedRate;
        private System.Int32 myOther1Num;
        private System.Int32 myOther2Num;
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();

        private string relationDeptID;
        private string relationNurseCellID;
        private System.Int32 myOutCure;
        private System.Int32 myOutUnCure;
        private System.Int32 myOutBetter;
        private System.Int32 myOutDeath;
        private System.Int32 myOutOther;
     
        /// <summary>
        /// 住院日报实体
        /// ID   科室编码
        /// Name 科室名称
        /// </summary>
        public InpatientDayReport()
        {
            // TODO: 在此处添加构造函数逻辑
        }


        /// <summary>
        /// 统计日期
        /// </summary>
        public System.DateTime DateStat
        {
            get
            {
                return this.myDateStat;
            }
            set
            {
                this.myDateStat = value;
            }
        }
        /// <summary>
        /// 转归代码
        /// </summary>
       

        /// <summary>
        /// 护士站
        /// </summary>
        public FS.FrameWork.Models.NeuObject NurseStation
        {
            get
            {
                return this.nurseStation;
            }
            set
            {
                this.nurseStation = value;
            }
        }


        /// <summary>
        /// 编制内病床数
        /// </summary>
        public System.Int32 BedStand
        {
            get
            {
                return this.myBedStand;
            }
            set
            {
                this.myBedStand = value;
            }
        }


        /// <summary>
        /// 加床数
        /// </summary>
        public System.Int32 BedAdd
        {
            get
            {
                return this.myBedAdd;
            }
            set
            {
                this.myBedAdd = value;
            }
        }


        /// <summary>
        /// 空床数
        /// </summary>
        public System.Int32 BedFree
        {
            get
            {
                return this.myBedFree;
            }
            set
            {
                this.myBedFree = value;
            }
        }


        /// <summary>
        /// 期初病人数
        /// </summary>
        public System.Int32 BeginningNum
        {
            get
            {
                return this.myBeginningNum;
            }
            set
            {
                this.myBeginningNum = value;
            }
        }


        /// <summary>
        /// 常规入院数
        /// </summary>
        public System.Int32 InNormal
        {
            get
            {
                return this.myInNormal;
            }
            set
            {
                this.myInNormal = value;
            }
        }


        /// <summary>
        /// 急诊入院数
        /// </summary>
        public System.Int32 InEmergency
        {
            get
            {
                return this.myInEmergency;
            }
            set
            {
                this.myInEmergency = value;
            }
        }


        /// <summary>
        /// 其他科转入数
        /// </summary>
        public System.Int32 InTransfer
        {
            get
            {
                return this.myInTransfer;
            }
            set
            {
                this.myInTransfer = value;
            }
        }


        /// <summary>
        /// 其他科转入数(内部转入,中山一需求)
        /// </summary>
        public System.Int32 InTransferInner
        {
            get
            {
                return this.myInTransferInner;
            }
            set
            {
                this.myInTransferInner = value;
            }
        }


        /// <summary>
        /// 招回入院人数
        /// </summary>
        public System.Int32 InReturn
        {
            get
            {
                return this.myInReturn;
            }
            set
            {
                this.myInReturn = value;
            }
        }


        /// <summary>
        /// 常规出院数
        /// </summary>
        public System.Int32 OutNormal
        {
            get
            {
                return this.myOutNormal;
            }
            set
            {
                this.myOutNormal = value;
            }
        }


        /// <summary>
        /// 转出其他科数
        /// </summary>
        public System.Int32 OutTransfer
        {
            get
            {
                return this.myOutTransfer;
            }
            set
            {
                this.myOutTransfer = value;
            }
        }


        /// <summary>
        /// 转出其他科数(内部转出,中山一需求)
        /// </summary>
        public System.Int32 OutTransferInner
        {
            get
            {
                return this.myOutTransferInner;
            }
            set
            {
                this.myOutTransferInner = value;
            }
        }


        /// <summary>
        /// 退院人数
        /// </summary>
        public System.Int32 OutWithdrawal
        {
            get
            {
                return this.myOutWithdrawal;
            }
            set
            {
                this.myOutWithdrawal = value;
            }
        }


        /// <summary>
        /// 期末病人数
        /// </summary>
        public System.Int32 EndNum
        {
            get
            {
                return this.myEndNum;
            }
            set
            {
                this.myEndNum = value;
            }
        }


        /// <summary>
        /// 24小时内死亡数
        /// </summary>
        public System.Int32 DeadIn24
        {
            get
            {
                return this.myDeadIn24;
            }
            set
            {
                this.myDeadIn24 = value;
            }
        }


        /// <summary>
        /// 24小时外死亡数
        /// </summary>
        public System.Int32 DeadOut24
        {
            get
            {
                return this.myDeadOut24;
            }
            set
            {
                this.myDeadOut24 = value;
            }
        }


        /// <summary>
        /// 床位使用率
        /// </summary>
        public System.Decimal BedRate
        {
            get
            {
                return this.myBedRate;
            }
            set
            {
                this.myBedRate = value;
            }
        }


        /// <summary>
        /// 其他1数量
        /// </summary>
        public System.Int32 Other1Num
        {
            get
            {
                return this.myOther1Num;
            }
            set
            {
                this.myOther1Num = value;
            }
        }


        /// <summary>
        /// 其他2数量
        /// </summary>
        public System.Int32 Other2Num
        {
            get
            {
                return this.myOther2Num;
            }
            set
            {
                this.myOther2Num = value;
            }
        }

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
        /// 转科操作所对应的相关科室：转入操作对应的原科室，转出操作对应的目标科室
        /// </summary>
        public string RelationDeptID
        {
            get { return this.relationDeptID; }
            set { this.relationDeptID = value; }
        }

        /// <summary>
        /// 转区操作对应的相关病区：转入操作对应的原病区，转出操作对应的目标病区
        /// </summary>
        public string RelationNurseCellID
        {
            get { return this.relationNurseCellID; }
            set { this.relationNurseCellID = value; }
        }

        /// <summary>
        /// 治愈出院
        /// </summary>
        public System.Int32 OutCure
        {
            get { return this.myOutCure; }
            set { this.myOutCure = value; }
        }

        /// <summary>
        /// 未愈出院
        /// </summary>
        public System.Int32 OutUnCure
        {
            get { return this.myOutUnCure; }
            set { this.myOutUnCure = value; }
        }

        /// <summary>
        /// 好转出院
        /// </summary>
        public System.Int32 OutBetter
        {
            get { return this.myOutBetter; }
            set { this.myOutBetter = value; }
        }

        /// <summary>
        /// 死亡出院
        /// </summary>
        public System.Int32 OutDeath
        {
            get { return this.myOutDeath; }
            set { this.myOutDeath = value; }
        }

        /// <summary>
        /// 其他出院
        /// </summary>
        public System.Int32 OutOther
        {
            get { return this.myOutOther; }
            set { this.myOutOther = value; }
        }

        #region
        /// <summary>
        /// 操作人
        /// </summary>
        [Obsolete("废弃 用 OperInfo代替", true)]
        public System.String OperCode
        {
            get
            {
                return this.myOperCode;
            }
            set
            {
                this.myOperCode = value;
            }
        }


        /// <summary>
        /// 整理日期
        /// </summary>
        [Obsolete("废弃 用 OperInfo.OperTime代替", true)]
        public System.DateTime OperDate
        {
            get
            {
                return this.myOperDate;
            }
            set
            {
                this.myOperDate = value;
            }
        }

        private System.String myOperCode;
        private System.DateTime myOperDate;
        #endregion

    }
}