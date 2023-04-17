using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// InPlan<br></br>
    /// [��������: �豸�ɹ��ƻ�ʵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-1-7]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class InPlan : FS.FrameWork.Models.NeuObject
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public InPlan()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion ���캯��

        #region ����

        #region ˽�б���

        /// <summary>
        /// �ɹ��ƻ����
        /// </summary>
        private int serialCode;

        /// <summary>
        /// �ɹ��ƻ����ݺ�
        /// </summary>
        private string listCode;

        /// <summary>
        /// ���ʵ��
        /// </summary>
       // private HISFC.Object.Equipment.Input inputInfo = new Input();

        private FS.HISFC.Models.Equipment.Input inputInfo = new Input();

        /// <summary>
        /// �ɹ��ƻ�����
        /// </summary>
        private FS.FrameWork.Models.NeuObject planDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ƻ�������0�ֹ��ƻ�1������2����3ʱ��4������
        /// </summary>
        private string planType;

        /// <summary>
        /// �ƻ���״̬0�ɹ��ƻ���1���2�����3����
        /// </summary>
        private string planState;

        /// <summary>
        /// �ƻ����ҿ����
        /// </summary>
        private int planDeptStor;

        /// <summary>
        /// ȫԺ�����
        /// </summary>
        private int allDeptStor;

        /// <summary>
        /// ��ͬ��ˮ��
        /// </summary>
        private string pactNo;

        /// <summary>
        /// ��ͬ���
        /// </summary>
        private string pactCode;

        /// <summary>
        /// �ƻ�����
        /// </summary>
        private int planNum;

        /// <summary>
        /// �ƻ��۸�
        /// </summary>
        private decimal planPrice;

        /// <summary>
        /// �ƻ����
        /// </summary>
        private decimal planCost;

        /// <summary>
        /// �ƻ�����ʱ��
        /// </summary>
        private DateTime getDate;

        /// <summary>
        /// �ƻ��ɹ�ʱ��
        /// </summary>
        private DateTime buyDate;

        /// <summary>
        /// �ƻ��ɹ���Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject buyOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ƻ�¼����
        /// </summary>
        private FS.FrameWork.Models.NeuObject planOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ƻ�¼��ʱ��
        /// </summary>
        private DateTime planDate;

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject examOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private int examNum;

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject examDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime examDate;

        /// <summary>
        /// ����˵��
        /// </summary>
        private string examCont;

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject wasteOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime wasteDate;

        /// <summary>
        /// ����˵��
        /// </summary>
        private string wasteCont;

        #endregion ˽�б���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region ����

        /// <summary>
        /// �ɹ��ƻ����
        /// </summary>
        public int SerialCode
        {
            get { return serialCode; }
            set { serialCode = value; }
        }

        /// <summary>
        /// �ɹ��ƻ����ݺ�
        /// </summary>
        public string ListCode
        {
            get { return listCode; }
            set { listCode = value; }
        }

        /// <summary>
        /// ���ʵ��
        /// </summary>
        public FS.HISFC.Models.Equipment.Input InputInfo
        {
            get { return inputInfo; }
            set { inputInfo = value; }
        }

        /// <summary>
        /// �ɹ��ƻ�����
        /// </summary>
        public FS.FrameWork.Models.NeuObject PlanDept
        {
            get { return planDept; }
            set { planDept = value; }
        }

        /// <summary>
        /// �ƻ�������0�ֹ��ƻ�1������2����3ʱ��4������
        /// </summary>
        public string PlanType
        {
            get { return planType; }
            set { planType = value; }
        }

        /// <summary>
        /// �ƻ���״̬0�ɹ��ƻ���1���2�����3����
        /// </summary>
        public string PlanState
        {
            get { return planState; }
            set { planState = value; }
        }

        /// <summary>
        /// �ƻ����ҿ����
        /// </summary>
        public int PlanDeptStor
        {
            get { return planDeptStor; }
            set { planDeptStor = value; }
        }

        /// <summary>
        /// ȫԺ�����
        /// </summary>
        public int AllDeptStor
        {
            get { return allDeptStor; }
            set { allDeptStor = value; }
        }

        /// <summary>
        /// ��ͬ��ˮ��
        /// </summary>
        public string PactNo
        {
            get { return pactNo; }
            set { pactNo = value; }
        }

        /// <summary>
        /// ��ͬ���
        /// </summary>
        public string PactCode
        {
            get { return pactCode; }
            set { pactCode = value; }
        }

        /// <summary>
        /// �ƻ�����
        /// </summary>
        public int PlanNum
        {
            get { return planNum; }
            set { planNum = value; }
        }

        /// <summary>
        /// �ƻ��۸�
        /// </summary>
        public decimal PlanPrice
        {
            get { return planPrice; }
            set { planPrice = value; }
        }

        /// <summary>
        /// �ƻ����
        /// </summary>
        public decimal PlanCost
        {
            get { return planCost; }
            set { planCost = value; }
        }

        /// <summary>
        /// �ƻ�����ʱ��
        /// </summary>
        public DateTime GetDate
        {
            get { return getDate; }
            set { getDate = value; }
        }

        /// <summary>
        /// �ƻ��ɹ�ʱ��
        /// </summary>
        public DateTime BuyDate
        {
            get { return buyDate; }
            set { buyDate = value; }
        }

        /// <summary>
        /// �ƻ��ɹ���Ա
        /// </summary>
        public FS.FrameWork.Models.NeuObject BuyOper
        {
            get { return buyOper; }
            set { buyOper = value; }
        }

        /// <summary>
        /// �ƻ�¼����
        /// </summary>
        public FS.FrameWork.Models.NeuObject PlanOper
        {
            get { return planOper; }
            set { planOper = value; }
        }

        /// <summary>
        /// �ƻ�¼��ʱ��
        /// </summary>
        public DateTime PlanDate
        {
            get { return planDate; }
            set { planDate = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ExamOper
        {
            get { return examOper; }
            set { examOper = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int ExamNum
        {
            get { return examNum; }
            set { examNum = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ExamDept
        {
            get { return examDept; }
            set { examDept = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime ExamDate
        {
            get { return examDate; }
            set { examDate = value; }
        }

        /// <summary>
        /// ����˵��
        /// </summary>
        public string ExamCont
        {
            get { return examCont; }
            set { examCont = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject WasteOper
        {
            get { return wasteOper; }
            set { wasteOper = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime WasteDate
        {
            get { return wasteDate; }
            set { wasteDate = value; }
        }

        /// <summary>
        /// ����˵��
        /// </summary>
        public string WasteCont
        {
            get { return wasteCont; }
            set { wasteCont = value; }
        }

        #endregion ����

        #region ����

        #region ��Դ�ͷ�
        #endregion ��Դ�ͷ�

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new InPlan Clone()
        {
            InPlan inPlan = base.Clone() as InPlan;

            inPlan.WasteOper = this.WasteOper.Clone();
            inPlan.ExamDept = this.ExamDept.Clone();
            inPlan.ExamOper = this.ExamOper.Clone();
            inPlan.PlanOper = this.PlanOper.Clone();
            inPlan.BuyOper = this.BuyOper.Clone();
            inPlan.PlanDept = this.PlanDept.Clone();
            inPlan.InputInfo = this.InputInfo.Clone();

            return inPlan;
        }

        #endregion ��¡

        #region ˽�з���
        #endregion ˽�з���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region �¼�
        #endregion �¼�

        #region �ӿ�ʵ��
        #endregion �ӿ�ʵ��

    }
}
