using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// CasOperationDetail ��ժҪ˵����
    /// </summary>
    [Serializable]
    public class OperationDetail
    {
        public OperationDetail()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ˽�б���
        //˽���ֶ�
        private string myInpatientNO;
        private string myHappenNO;
        private DateTime myOperationDate;
        private FS.FrameWork.Models.NeuObject myOperationInfo = new FS.FrameWork.Models.NeuObject();
        private string myOperationKind;
        private string myMarcKind;
        private string myNickKind;
        private string myCicaKind;
        private FS.FrameWork.Models.NeuObject myFirDoctInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject mySecDoctInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject myThrDoctInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject myNarcDoctInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject myFourDoctInfo = new FS.FrameWork.Models.NeuObject();
        private string myOpbOpa;
        private int myBeforeOperDays;
        private string myStatFlag;
        private DateTime myInDate;
        private DateTime myOutDate;
        private DateTime myDeatDate;
        private FS.FrameWork.Models.NeuObject myOperationDeptInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject myOutDeptInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject myOutICDInfo = new FS.FrameWork.Models.NeuObject();
        private string mySYNDFlag;
        private DateTime myOperDate;
        private string operType;
        private string operationNO;
        #endregion

        #region ����

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string InpatientNO
        {
            get { return myInpatientNO; }
            set { myInpatientNO = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string HappenNO
        {
            get { return myHappenNO; }
            set { myHappenNO = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime OperationDate
        {
            get { return myOperationDate; }
            set { myOperationDate = value; }
        }

        /// <summary>
        /// ������ϢID �������� Name��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperationInfo
        {
            get { return myOperationInfo; }
            set { myOperationInfo = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string OperationKind
        {
            get { return myOperationKind; }
            set { myOperationKind = value; }
        }

        /// <summary>
        /// ����ʽ
        /// </summary>
        public string MarcKind
        {
            get { return myMarcKind; }
            set { myMarcKind = value; }
        }

        /// <summary>
        /// �п�����
        /// </summary>
        public string NickKind
        {
            get { return myNickKind; }
            set { myNickKind = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string CicaKind
        {
            get { return myCicaKind; }
            set { myCicaKind = value; }
        }

        /// <summary>
        /// ����ҽʦ��Ϣ1 ID ҽ������ Name ҽ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject FirDoctInfo
        {
            get { return myFirDoctInfo; }
            set { myFirDoctInfo = value; }
        }

        /// <summary>
        /// ����ҽʦ��Ϣ2 ID ҽ������ Name ҽ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject FourDoctInfo
        {
            get { return myFourDoctInfo; }
            set { myFourDoctInfo = value; }
        }

        /// <summary>
        /// I��ҽʦ��Ϣ ID ҽ������ Name ҽ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject SecDoctInfo
        {
            get { return mySecDoctInfo; }
            set { mySecDoctInfo = value; }
        }

        /// <summary>
        /// II��ҽʦ��Ϣ ID ҽ������ Name ҽ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ThrDoctInfo
        {
            get { return myThrDoctInfo; }
            set { myThrDoctInfo = value; }
        }

        /// <summary>
        /// ����ҽʦ��Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject NarcDoctInfo
        {
            get { return myNarcDoctInfo; }
            set { myNarcDoctInfo = value; }
        }

        /// <summary>
        /// ��ǰ_�����
        /// </summary>
        public string OpbOpa
        {
            get { return myOpbOpa; }
            set { myOpbOpa = value; }
        }

        /// <summary>
        /// ��ǰסԺ����
        /// </summary>
        public int BeforeOperDays
        {
            get { return myBeforeOperDays; }
            set { myBeforeOperDays = value; }
        }

        /// <summary>
        /// ͳ�Ʊ�־
        /// </summary>
        public string StatFlag
        {
            get { return myStatFlag; }
            set { myStatFlag = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public DateTime InDate
        {
            get { return myInDate; }
            set { myInDate = value; }
        }

        /// <summary>
        /// ��Ժ����
        /// </summary>
        public DateTime OutDate
        {
            get { return myOutDate; }
            set { myOutDate = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime DeatDate
        {
            get { return myDeatDate; }
            set { myDeatDate = value; }
        }

        /// <summary>
        /// ����������ϢID ���ұ��� Name ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperationDeptInfo
        {
            get { return myOperationDeptInfo; }
            set { myOperationDeptInfo = value; }
        }

        /// <summary>
        /// ��Ժ������Ϣ ID ���ұ��� Name ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OutDeptInfo
        {
            get { return myOutDeptInfo; }
            set { myOutDeptInfo = value; }
        }

        /// <summary>
        /// ��Ժ�����ICD��Ϣ ID��Ϣ���� Name �������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OutICDInfo
        {
            get { return myOutICDInfo; }
            set { myOutICDInfo = value; }
        }

        /// <summary>
        /// �Ƿ�ϲ�֢
        /// </summary>
        public string SYNDFlag
        {
            get { return mySYNDFlag; }
            set { mySYNDFlag = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperDate
        {
            get { return myOperDate; }
            set { myOperDate = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string OperType
        {
            get
            {
                return operType;
            }
            set
            {
                operType = value;
            }
        }

        /// <summary>
        /// �������к�
        /// </summary>
        public string OperationNO
        {
            get
            {
                return operationNO;
            }
            set
            {
                operationNO = value;
            }
        }
        #endregion

        #region ���ú���


        public new OperationDetail Clone()
        {
            OperationDetail OperationDetailClone = base.MemberwiseClone() as OperationDetail;

            OperationDetailClone.myFirDoctInfo = this.myFirDoctInfo;
            OperationDetailClone.myNarcDoctInfo = this.myNarcDoctInfo;
            OperationDetailClone.myOperationDeptInfo = this.myOperationDeptInfo;
            OperationDetailClone.myOperationInfo = this.myOperationInfo;
            OperationDetailClone.myOutDeptInfo = this.myOutDeptInfo;
            OperationDetailClone.myOutICDInfo = this.myOutICDInfo;
            OperationDetailClone.mySecDoctInfo = this.mySecDoctInfo;
            OperationDetailClone.myThrDoctInfo = this.myThrDoctInfo;

            return OperationDetailClone;

        }

        #endregion
    }
}
