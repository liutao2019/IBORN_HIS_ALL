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
    public class BuyApply : FS.HISFC.Models.Base.Spell
    {
        #region ���캯��
	    /// <summary>
	    /// ���캯��
	    /// </summary>
        public BuyApply()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
	    #endregion ���캯��

	    #region ����

	    #region ˽�б���

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
        /// ����״̬0����1������2������3����
        /// </summary>
        private string applyState;

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
        /// ������
        /// </summary>
        private NeuObject wasteOper = new NeuObject();

        /// <summary>
        /// ���Ͽ���
        /// </summary>
        private NeuObject wasteDept = new NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime wasteDate;

        /// <summary>
        /// ����ԭ��
        /// </summary>
        private string wasteCause;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private BuyExam buyExams = new BuyExam();

        /// <summary>
        /// �ϼ���������
        /// </summary>
        private NeuObject nextDept = new NeuObject();

	    #endregion ˽�б���

	    #region ��������
	    #endregion ��������

	    #region ��������
	    #endregion ��������

	    #endregion ����

	    #region ����

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
        /// ����״̬0����1������2������3����
        /// </summary>
        public string ApplyState
        {
            get { return applyState; }
            set { applyState = value; }
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
        /// ������
        /// </summary>
        public NeuObject WasteOper
        {
            get { return wasteOper; }
            set { wasteOper = value; }
        }

        /// <summary>
        /// ���Ͽ���
        /// </summary>
        public NeuObject WasteDept
        {
            get { return wasteDept; }
            set { wasteDept = value; }
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
        /// ����ԭ��
        /// </summary>
        public string WasteCause
        {
            get { return wasteCause; }
            set { wasteCause = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public BuyExam BuyExams
        {
            get { return buyExams; }
            set { buyExams = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsEnd
        {
            set
            {
                for (int i = 0; i < buyExams.Count; i++)
                {
                    buyExams[i].IsEnd = value;
                    buyExams.ExamState = "2";
                }
            }
        }

        /// <summary>
        /// �ϼ���������
        /// </summary>
        public NeuObject NextDept
        {
            get { return nextDept; }
            set { nextDept = value; }
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
        public new BuyApply Clone()
        {
            BuyApply buyApply = base.Clone() as BuyApply;

            buyApply.Dept = this.dept.Clone();
            buyApply.ApplyOper = this.applyOper.Clone();
            buyApply.ApplyDept = this.applyDept.Clone();
            buyApply.WasteOper = this.wasteOper.Clone();
            buyApply.WasteDept = this.wasteDept.Clone();
            buyApply.BuyExams = this.buyExams.Clone();
            buyExams.NextDept = this.nextDept.Clone();

            return buyApply;
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
