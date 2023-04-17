using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// �������Ŀ���Ϣ��ʵ�� �̳��� FS.FrameWork.Models.NeuObject
    /// ID ����Ա���� Name ����Ա����
    ///
    /// 
    /// </summary>
    [Serializable]
    public class ReadCard : FS.FrameWork.Models.NeuObject
    {
        public ReadCard()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        //˽���ֶ�
        private string myCardID;
        private FS.HISFC.Models.Base.OperEnvironment myEmplInfo = new FS.HISFC.Models.Base.OperEnvironment();
        private FS.FrameWork.Models.NeuObject myDeptInfo = new FS.FrameWork.Models.NeuObject();
        private DateTime myOperDate;
        private string myValidFlag;
        private FS.FrameWork.Models.NeuObject myCancelOperInfo = new FS.FrameWork.Models.NeuObject();
        private DateTime myCancelDate;

        //EmployeeInfo
        /// <summary>
        /// ����֤��
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
        /// Ա����Ϣ ID ���� Name ����
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
        /// ������Ϣ ID ���� Name ��������
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
        /// ��Ч 1��Ч/2��Ч
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
        /// ��������Ϣ ID ���� Name ����
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
        /// ����ʱ��
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
        /// ����ʱ��
        /// </summary>
        [Obsolete("���� ��EmployeeInfo.OperTime����", true)]
        public DateTime OperDate
        {
            get { return myOperDate; }
            set { myOperDate = value; }
        }
        /// <summary>
        /// Ա����Ϣ ID ���� Name ����
        /// </summary>
        [Obsolete("���� ��EmployeeInfo����", true)]
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
