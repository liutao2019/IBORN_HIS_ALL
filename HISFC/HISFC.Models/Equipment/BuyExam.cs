using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// [��������: �豸��������ʵ����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-11-26]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class BuyExam : FS.HISFC.Models.Base.Spell
    {
        #region ���캯��
	    /// <summary>
	    /// ���캯��
	    /// </summary>
        public BuyExam()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
	    #endregion ���캯��

	    #region ����

	    #region ˽�б���

        /// <summary>
        /// ǰ��������ˮ��(��Ϊ'NONE')
        /// </summary>
        private string lastExamNo;

        /// <summary>
        /// ��������ˮ��(��Ϊ'NONE')
        /// </summary>
        private string nextExamNo;

        /// <summary>
        /// ���뵥��ˮ��
        /// </summary>
        private string applyNo;

        /// <summary>
        /// ���뵥�ݺ�
        /// </summary>
        private string applyListCode;

        /// <summary>
        /// �豸����
        /// </summary>
        private NeuObject dept = new NeuObject();

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string itemName;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string itemCont;

        /// <summary>
        /// ����
        /// </summary>
        private int itemNum;

        /// <summary>
        /// Ԥ����
        /// </summary>
        private decimal itemCost;

        /// <summary>
        /// �շѱ�׼
        /// </summary>
        private string feeRule;

        /// <summary>
        /// �����ջؿ�ʼʱ��
        /// </summary>
        private DateTime fromDate;

        /// <summary>
        /// �����ջؽ���ʱ��
        /// </summary>
        private DateTime toDate;

        /// <summary>
        /// ������
        /// </summary>
        private NeuObject applyOper = new NeuObject();

        /// <summary>
        /// �������
        /// </summary>
        private NeuObject applyDept = new NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime applyDate;

        /// <summary>
        /// �ռ�ʱ��
        /// </summary>
        private DateTime getDate;

        /// <summary>
        /// ������
        /// </summary>
        private NeuObject examOper = new NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private NeuObject examDept = new NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime examDate;

        /// <summary>
        /// ����˵��
        /// </summary>
        private string examCont;

        /// <summary>
        /// �����Ƿ�ͨ��
        /// </summary>
        private bool isPass;

        /// <summary>
        /// �����Ƿ����
        /// </summary>
        private bool isEnd;

        /// <summary>
        /// �����׶�(��������Ժ������)
        /// </summary>
        private string examPhase;

        /// <summary>
        /// �ϼ���������
        /// </summary>
        private NeuObject nextDept = new NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private List<BuyExam> buyExam = new List<BuyExam>();

        /// <summary>
        /// ����״̬:"0"�ޱ仯,"1"����,"2"�޸�
        /// </summary>
        private string examState = "0";

        /// <summary>
        /// ����״̬
        /// </summary>
        private string applyState;

	    #endregion ˽�б���

	    #region ��������
	    #endregion ��������

	    #region ��������
	    #endregion ��������

	    #endregion ����

	    #region ����

        /// <summary>
        /// ǰ��������ˮ��(��Ϊ'NONE')
        /// </summary>
        public string LastExamNo
        {
            get { return lastExamNo; }
            set { lastExamNo = value; }
        }

        /// <summary>
        /// ��������ˮ��(��Ϊ'NONE')
        /// </summary>
        public string NextExamNo
        {
            get { return nextExamNo; }
            set { nextExamNo = value; }
        }

        /// <summary>
        /// ���뵥��ˮ��
        /// </summary>
        public string ApplyNo
        {
            get { return applyNo; }
            set { applyNo = value; }
        }

        /// <summary>
        /// ���뵥�ݺ�
        /// </summary>
        public string ApplyListCode
        {
            get { return applyListCode; }
            set { applyListCode = value; }
        }

        /// <summary>
        /// �豸����
        /// </summary>
        public NeuObject Dept
        {
            get { return dept; }
            set { dept = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemCont
        {
            get { return itemCont; }
            set { itemCont = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int ItemNum
        {
            get { return itemNum; }
            set { itemNum = value; }
        }

        /// <summary>
        /// Ԥ����
        /// </summary>
        public decimal ItemCost
        {
            get { return itemCost; }
            set { itemCost = value; }
        }

        /// <summary>
        /// �շѱ�׼
        /// </summary>
        public string FeeRule
        {
            get { return feeRule; }
            set { feeRule = value; }
        }

        /// <summary>
        /// �����ջؿ�ʼʱ��
        /// </summary>
        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }

        /// <summary>
        /// �����ջؽ���ʱ��
        /// </summary>
        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public NeuObject ApplyOper
        {
            get { return applyOper; }
            set { applyOper = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public NeuObject ApplyDept
        {
            get { return applyDept; }
            set { applyDept = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime ApplyDate
        {
            get { return applyDate; }
            set { applyDate = value; }
        }

        /// <summary>
        /// �ռ�ʱ��
        /// </summary>
        public DateTime GetDate
        {
            get { return getDate; }
            set { getDate = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public NeuObject ExamOper
        {
            get { return examOper; }
            set { examOper = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public NeuObject ExamDept
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
        /// �����Ƿ�ͨ��
        /// </summary>
        public bool IsPass
        {
            get { return isPass; }
            set { isPass = value; }
        }

        /// <summary>
        /// �����Ƿ����
        /// </summary>
        public bool IsEnd
        {
            get { return isEnd; }
            set { isEnd = value; }
        }

        /// <summary>
        /// �����׶�(��������Ժ������)
        /// </summary>
        public string ExamPhase
        {
            get { return examPhase; }
            set { examPhase = value; }
        }

        /// <summary>
        /// �ϼ���������
        /// </summary>
        public NeuObject NextDept
        {
            get { return nextDept; }
            set { nextDept = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BuyExam this[int pos]
        {
            get
            {
                return this.buyExam[pos] as BuyExam;
            }
            set
            {
                this.buyExam[pos] = value;
            }
        }

        public BuyExam this[string dept]
        {
            get
            {
                foreach (BuyExam tempExam in this.buyExam)
                {
                    if (tempExam.examDept.ID == dept)
                    {
                        return tempExam;
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < buyExam.Count; i++)
                {
                    if (buyExam[i].examDept.ID == dept)
                    {
                        buyExam[i] = value;
                    }
                }
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        public int Count
        {
            get
            {
                return this.buyExam.Count;
            }
        }

        /// <summary>
        /// ����״̬:"0"�ޱ仯,"1"����,"2"�޸�
        /// </summary>
        public string ExamState
        {
            get { return examState; }
            set { examState = value; }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public string ApplyState
        {
            get { return applyState; }
            set { applyState = value; }
        }

	    #endregion ����

	    #region ����

	    #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new BuyExam Clone()
        {
            BuyExam buyExam = base.Clone() as BuyExam;

            buyExam.Dept = this.dept.Clone();
            buyExam.ApplyOper = this.applyOper.Clone();
            buyExam.ApplyDept = this.applyDept.Clone();
            buyExam.ExamOper = this.examOper.Clone();
            buyExam.ExamDept = this.examDept.Clone();
            buyExam.NextDept = this.nextDept.Clone();

            return buyExam;
        }

	    #endregion ��¡

	    #region ˽�з���
	    #endregion ˽�з���

	    #region ��������
	    #endregion ��������

	    #region ��������

        /// <summary>
        /// ����һ��������Ϣ
        /// </summary>
        /// <param name="buyExam">����ʵ��</param>
        public void Add(BuyExam newBuyExam)
        {
            this.buyExam.Add(newBuyExam);
        }

	    #endregion ��������

	    #endregion ����

    }
}
