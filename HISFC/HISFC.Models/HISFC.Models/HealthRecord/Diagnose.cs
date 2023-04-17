using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// ���������Ϣ��ID ����Ա���� Name ����Ա���� memo��ע
    /// </summary>
    [Serializable]
    public class Diagnose : FS.FrameWork.Models.NeuObject
    {
        public Diagnose()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�б���

        private string diagOutState;
        private string secondICD;
        private string synDromeID;
        private string clpa;
        private string dubDiagFlag;
        private string mainFlag;
        private string levelCode;
        private string periorCode;

        private DiagnoseBase myDiagInfo = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
        private FS.HISFC.Models.RADT.PVisit myPVisit = new FS.HISFC.Models.RADT.PVisit();
        private string operType; //�������ͣ��ж���ҽ��վ¼�뻹�ǲ�����¼��� ��
        private string operationFlag;
        private string is30Disease;
        private int infectNum; //Ժ�ڸ�Ⱦ����
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// ����Ա��
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
        /// ��Ⱦ����
        /// </summary>
        public int InfectNum
        {
            get
            {
                return infectNum;
            }
            set
            {
                infectNum = value;
            }
        }
        /// <summary>
        /// �Ƿ�ʦ30�ּ���
        /// </summary>
        public string Is30Disease
        {
            get
            {
                if (is30Disease == null)
                {
                    is30Disease = "";
                }
                return is30Disease;
            }
            set
            {
                is30Disease = value;
            }
        }
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public string OperationFlag
        {
            get
            {
                if (operationFlag == null)
                {
                    operationFlag = "";
                }
                return operationFlag;
            }
            set
            {
                operationFlag = value;
            }
        }
        /// <summary>
        /// �ּ�
        /// </summary>
        public string LevelCode
        {
            get
            {
                if (levelCode == null)
                {
                    levelCode = "";
                }
                return levelCode;
            }
            set { levelCode = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string PeriorCode
        {
            get
            {
                if (periorCode == null)
                {
                    periorCode = "";
                }
                return periorCode;
            }
            set { periorCode = value; }
        }
        /// <summary>
        /// ���߷�����
        /// </summary>
        public FS.HISFC.Models.RADT.PVisit Pvisit
        {
            get { return myPVisit; }
            set { myPVisit = value; }
        }

        /// <summary>
        /// �����������
        /// </summary>
        public string DiagOutState
        {
            get
            {
                if (diagOutState == null)
                {
                    diagOutState = "";
                }
                return diagOutState;
            }
            set
            {
                diagOutState = value;
            }
        }

        /// <summary>
        /// �ڶ�ICD����
        /// </summary>
        public string SecondICD
        {
            get
            {
                if (secondICD == null)
                {
                    secondICD = "";
                }
                return secondICD;
            }
            set
            {
                secondICD = value;
            }
        }

        /// <summary>
        /// �ϲ�֢����
        /// </summary>
        public string SynDromeID
        {
            get
            {
                if (synDromeID == null)
                {
                    synDromeID = "";
                }
                return synDromeID;
            }
            set
            {
                synDromeID = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string CLPA
        {
            get
            {
                return clpa;
            }
            set
            {
                clpa = value;
            }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public string DubDiagFlag
        {
            get
            {
                return dubDiagFlag;
            }
            set
            {
                dubDiagFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ������
        /// </summary>
        public string MainFlag
        {
            get
            {
                if (mainFlag == null)
                {
                    mainFlag = "";
                }
                return mainFlag;
            }
            set
            {
                mainFlag = value;
            }
        }


        /// <summary>
        /// �����Ϣ
        /// </summary>
        public DiagnoseBase DiagInfo
        {
            get
            {
                return myDiagInfo;
            }
            set
            {
                myDiagInfo = value;
            }
        }

        /// <summary>
        /// �������ͣ��ж���ҽ��վ¼�뻹�ǲ�����¼��� ��
        /// </summary>
        public string OperType
        {
            get
            {
                if (operType == null)
                {
                    operType = "";
                }
                return operType;
            }
            set
            {
                operType = value;
            }
        }
        #endregion

        #region ���ú���


        public new Diagnose Clone()
        {
            Diagnose DiagnoseClone = (Diagnose)base.Clone(); ;

            DiagnoseClone.myDiagInfo = this.myDiagInfo.Clone();
            DiagnoseClone.myPVisit = this.myPVisit.Clone();
            DiagnoseClone.operInfo = operInfo.Clone();
            return DiagnoseClone;
        }

        #endregion

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("��������OperInfo.OperTime����")]
        public DateTime OperDate
        {
            get
            {
                return System.DateTime.Now;
            }
            //set 
            //{
            //    operDate = value; 
            //}
        }
        #endregion
    }
}
