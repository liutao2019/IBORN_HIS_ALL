using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// סԺ�ձ�ʵ��
    /// ID   ���ұ���
    /// Name ��������
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
        /// סԺ�ձ�ʵ��
        /// ID   ���ұ���
        /// Name ��������
        /// </summary>
        public InpatientDayReport()
        {
            // TODO: �ڴ˴����ӹ��캯���߼�
        }


        /// <summary>
        /// ͳ������
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
        /// ת�����
        /// </summary>
       

        /// <summary>
        /// ��ʿվ
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
        /// �����ڲ�����
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
        /// �Ӵ���
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
        /// �մ���
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
        /// �ڳ�������
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
        /// ������Ժ��
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
        /// ������Ժ��
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
        /// ������ת����
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
        /// ������ת����(�ڲ�ת��,��ɽһ����)
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
        /// �л���Ժ����
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
        /// �����Ժ��
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
        /// ת����������
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
        /// ת����������(�ڲ�ת��,��ɽһ����)
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
        /// ��Ժ����
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
        /// ��ĩ������
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
        /// 24Сʱ��������
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
        /// 24Сʱ��������
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
        /// ��λʹ����
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
        /// ����1����
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
        /// ����2����
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
        /// ת�Ʋ�������Ӧ����ؿ��ң�ת�������Ӧ��ԭ���ң�ת��������Ӧ��Ŀ�����
        /// </summary>
        public string RelationDeptID
        {
            get { return this.relationDeptID; }
            set { this.relationDeptID = value; }
        }

        /// <summary>
        /// ת��������Ӧ����ز�����ת�������Ӧ��ԭ������ת��������Ӧ��Ŀ�겡��
        /// </summary>
        public string RelationNurseCellID
        {
            get { return this.relationNurseCellID; }
            set { this.relationNurseCellID = value; }
        }

        /// <summary>
        /// ������Ժ
        /// </summary>
        public System.Int32 OutCure
        {
            get { return this.myOutCure; }
            set { this.myOutCure = value; }
        }

        /// <summary>
        /// δ����Ժ
        /// </summary>
        public System.Int32 OutUnCure
        {
            get { return this.myOutUnCure; }
            set { this.myOutUnCure = value; }
        }

        /// <summary>
        /// ��ת��Ժ
        /// </summary>
        public System.Int32 OutBetter
        {
            get { return this.myOutBetter; }
            set { this.myOutBetter = value; }
        }

        /// <summary>
        /// ������Ժ
        /// </summary>
        public System.Int32 OutDeath
        {
            get { return this.myOutDeath; }
            set { this.myOutDeath = value; }
        }

        /// <summary>
        /// ������Ժ
        /// </summary>
        public System.Int32 OutOther
        {
            get { return this.myOutOther; }
            set { this.myOutOther = value; }
        }

        #region
        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("���� �� OperInfo����", true)]
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
        /// ��������
        /// </summary>
        [Obsolete("���� �� OperInfo.OperTime����", true)]
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