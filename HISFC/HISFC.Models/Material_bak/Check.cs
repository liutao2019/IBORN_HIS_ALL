using System;

namespace Neusoft.HISFC.Object.Material
{
    /// <summary>
    /// Check ��ժҪ˵����
    /// �̵�ʵ��̳п��ʵ��
    /// ID			�̵���ˮ��(CheckNo)
    /// GroupNo		����
    /// Dept.Id		���ұ���
    /// Itemʵ��	�̵�ҩƷ��Ϣ
    /// OperCode	����Ա
    /// OperDate	����ʱ��
    /// </summary>
    public class Check : Neusoft.NFC.Object.NeuObject
    {
        //private string myCheckCode;		//�̵㵥��
        //private decimal myFStoreNum;	//��������
        //private decimal myAdjustNum;	//�̵�����
        //private decimal myCStoreNum;	//�������
        //private decimal myMinNum;		//��С����
        //private decimal myPackNum;		//��װ����
        //private decimal myProfitLossNum;//ӯ������
        //private string myProfitStatic;	//ӯ����� 0 �̿�  1 ��ӯ 2 ��ӯ��
        //private string myQualityFlag;	//������� 0 ��	   1 ����
        ////private string isAdd;			//����ҩƷ���  0 ������  1 ����
        //private string myDisposeWay;	//����ʽ
        //private string myFOperCode;		//������
        //private DateTime myFOperDate;	//����ʱ��
        //private string myCOperCode;		//�����
        //private DateTime myCOperDate;	//���ʱ��
        //private string myCheckState;	//�̵�״̬:0����,1���,2ȡ��
        //private decimal myLastNum;//���½��
        //private decimal myInNum;//���������
        //private decimal myOutNum;//���³�����
        //private decimal myInMoney;//���������
        //private decimal myOutMoney;//���³�����
        //private decimal myFstoreMoney;//�������Ӧ�ڷ���������
        //private DateTime myFromDate;
        //private DateTime myToDate;

        public Check()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            //this.PrivType = "0506";	//�̵���Ȩ�ޱ���
        }

        #region ����

        private string myCheckCode;		//�̵㵥��

        private string myCheckName;     //�̵㵥����

        private decimal myFStoreNum;	//��������

        private decimal myAdjustNum;	//�̵�����

        private decimal myCStoreNum;	//�������

        private decimal myProfitLossNum;//ӯ������

        private string myProfitFlag;	//ӯ����� 0 �̿�  1 ��ӯ 2 ��ӯ��

        private string myCheckState;	//�̵�״̬:0����,1���,2ȡ��

        private decimal myCheckLossCost; //�̿����(���ۼ�)

        private decimal myCheckProfitCost; //��ӯ���(���ۼ�)

        private StoreHead myStoreHead = new StoreHead();  //��������Ϣ

        /// <summary>
        /// ���ʲ�����Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment fOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment cOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion ����

        #region ����

        /// <summary>
        /// �̵㵥��
        /// </summary>
        public string CheckCode
        {
            get { return myCheckCode; }
            set { myCheckCode = value; }
        }

        /// <summary>
        /// �̵㵥����
        /// </summary>
        public string CheckName
        {
            get { return myCheckName; }
            set { myCheckName = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal FStoreNum
        {
            get { return myFStoreNum; }
            set { myFStoreNum = value; }
        }

        /// <summary>
        /// �̵�����
        /// </summary>
        public decimal AdjustNum
        {
            get { return myAdjustNum; }
            set { myAdjustNum = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public decimal CStoreNum
        {
            get { return myCStoreNum; }
            set { myCStoreNum = value; }
        }

        /// <summary>
        /// ӯ������
        /// </summary>
        public decimal ProfitLossNum
        {
            get { return myProfitLossNum; }
            set { myProfitLossNum = value; }
        }

        /// <summary>
        /// ӯ����� 0 �̿�  1 ��ӯ 2 ��ӯ��
        /// </summary>
        public string ProfitFlag
        {
            get { return myProfitFlag; }
            set { myProfitFlag = value; }
        }

        /// <summary>
        /// �̵�״̬:0����,1���,2ȡ��
        /// </summary>
        public string CheckState
        {
            get { return myCheckState; }
            set { myCheckState = value; }
        }

        /// <summary>
        /// �̿����(���ۼ�)
        /// </summary>
        public decimal CheckLossCost
        {
            get { return myCheckLossCost; }
            set { myCheckLossCost = value; }
        }

        /// <summary>
        /// ��ӯ���(���ۼ�)
        /// </summary>
        public decimal CheckProfitCost
        {
            get { return myCheckProfitCost; }
            set { myCheckProfitCost = value; }
        }

        /// <summary>
        /// ���ʲ�����Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment FOper
        {
            get { return fOper; }
            set { fOper = value; }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment COper
        {
            get { return cOper; }
            set { cOper = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public StoreHead StoreHead
        {
            get { return myStoreHead; }
            set { myStoreHead = value; }
        }

        #endregion ����
    }
}
