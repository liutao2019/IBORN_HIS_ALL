using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// Check<br></br>
    /// [��������: �豸�̵�ʵ����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-12-4]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class Check : FS.HISFC.Models.Base.Spell
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public Check()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion ���캯��

        #region ����

        #region ˽�б���

        /// <summary>
        /// �̵㵥���
        /// </summary>
        private int checkSerialCode;

        /// <summary>
        /// �̵㵥�ݺ�
        /// </summary>
        private string checkListCode;

        /// <summary>
        /// ��Ƭʵ��
        /// </summary>
        private Main checkMain = new Main();

        /// <summary>
        /// ��״̬
        /// </summary>
        private string currStateNew;

        /// <summary>
        /// ��״̬������
        /// </summary>
        private string class3MeaningCodeNew;

        /// <summary>
        /// ӯ��״̬0����1��ӯ2�̿�
        /// </summary>
        private string checkState;

        /// <summary>
        /// ӯ�����(ԭֵ)
        /// </summary>
        private decimal checkCost;

        /// <summary>
        /// �Ƿ��׼�̵�0δ��׼1��׼
        /// </summary>
        private bool isApprove;

        /// <summary>
        /// �̵���
        /// </summary>
        private NeuObject checkOper = new NeuObject();

        /// <summary>
        /// �̵����
        /// </summary>
        private NeuObject checkDept = new NeuObject();

        /// <summary>
        /// �̵�ʱ��
        /// </summary>
        private DateTime checkDate;

        /// <summary>
        /// ��׼��
        /// </summary>
        private NeuObject approveOper = new NeuObject();

        /// <summary>
        /// ��׼����
        /// </summary>
        private NeuObject aproveDept = new NeuObject();

        /// <summary>
        /// ��׼ʱ��
        /// </summary>
        private DateTime approveDate;

        /// <summary>
        /// ���ϱ���ˮ��
        /// </summary>
        private string rejectNo;

        /// <summary>
        /// ���̵㵥���̵��б�
        /// </summary>
        private List<Check> checkList = new List<Check>();

        #endregion ˽�б���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region ����

        /// <summary>
        /// �̵㵥���
        /// </summary>
        public int CheckSerialCode
        {
            get { return checkSerialCode; }
            set { checkSerialCode = value; }
        }

        /// <summary>
        /// �̵㵥�ݺ�
        /// </summary>
        public string CheckListCode
        {
            get { return checkListCode; }
            set { checkListCode = value; }
        }

        /// <summary>
        /// ��Ƭʵ��
        /// </summary>
        public Main CheckMain
        {
            get { return checkMain; }
            set { checkMain = value; }
        }

        /// <summary>
        /// ��״̬
        /// </summary>
        public string CurrStateNew
        {
            get { return currStateNew; }
            set { currStateNew = value; }
        }

        /// <summary>
        /// ��״̬������
        /// </summary>
        public string Class3MeaningCodeNew
        {
            get { return class3MeaningCodeNew; }
            set { class3MeaningCodeNew = value; }
        }

        /// <summary>
        /// ӯ��״̬0����1��ӯ2�̿�
        /// </summary>
        public string CheckState
        {
            get { return checkState; }
            set { checkState = value; }
        }

        /// <summary>
        /// ӯ�����(ԭֵ)
        /// </summary>
        public decimal CheckCost
        {
            get { return checkCost; }
            set { checkCost = value; }
        }

        /// <summary>
        /// �Ƿ��׼�̵�0δ��׼1��׼
        /// </summary>
        public bool IsApprove
        {
            get { return isApprove; }
            set { isApprove = value; }
        }

        /// <summary>
        /// �̵���
        /// </summary>
        public NeuObject CheckOper
        {
            get { return checkOper; }
            set { checkOper = value; }
        }

        /// <summary>
        /// �̵����
        /// </summary>
        public NeuObject CheckDept
        {
            get { return checkDept; }
            set { checkDept = value; }
        }

        /// <summary>
        /// �̵�ʱ��
        /// </summary>
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
        }

        /// <summary>
        /// ��׼��
        /// </summary>
        public NeuObject ApproveOper
        {
            get { return approveOper; }
            set { approveOper = value; }
        }

        /// <summary>
        /// ��׼����
        /// </summary>
        public NeuObject AproveDept
        {
            get { return aproveDept; }
            set { aproveDept = value; }
        }

        /// <summary>
        /// ��׼ʱ��
        /// </summary>
        public DateTime ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }

        /// <summary>
        /// ���ϱ���ˮ��
        /// </summary>
        public string RejectNo
        {
            get { return rejectNo; }
            set { rejectNo = value; }
        }

        /// <summary>
        /// �̵㵥���������
        /// </summary>
        /// <param name="serial">�̵㵥���</param>
        /// <returns></returns>
        public Check this[int serial]
        {
            get
            {
                if (serial > this.checkList.Count)
                {
                    return null;
                }
                return this.checkList[serial];
            }
            set
            {
                if (this.checkList.Count > serial)
                {
                    this.checkList[serial] = value;
                }
            }
        }

        /// <summary>
        /// �̵㵥����
        /// </summary>
        public int Count
        {
            get
            {
                return this.checkList.Count;
            }
        }

        #endregion ����

        #region ����

        #region ��Դ�ͷ�
        #endregion ��Դ�ͷ�

        #region ���
        /// <summary>
        /// ���һ���̵���Ϣ
        /// </summary>
        /// <param name="newCheck"></param>
        public void Add(FS.HISFC.Models.Equipment.Check newCheck)
        {
            for (int i = 0; i < this.checkList.Count; i++)
            {
                if (this.checkList[i].checkMain.ID == newCheck.checkMain.ID)
                {
                    return;
                }
            }
            this.checkList.Add(newCheck);
            this.setSerialCode();
        }
        #endregion

        #region �Ƴ�
        /// <summary>
        /// �Ƴ�һ���̵���Ϣ
        /// </summary>
        /// <param name="cardNo">��Ƭ���</param>
        public void Remove(string cardNo)
        {
            for (int i = 0; i < this.checkList.Count; i++)
            {
                if (this.checkList[i].checkMain.ID == cardNo)
                {
                    this.checkList.Remove(this.checkList[i]);
                    break;
                }
            }
            this.setSerialCode();
        }
        #endregion

        #region �����̵����
        /// <summary>
        /// �����̵����
        /// </summary>
        private void setSerialCode()
        {
            for (int i = 0; i < this.checkList.Count; i++)
            {
                this.checkList[i].checkSerialCode = i;
            }
        }
        #endregion

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new Check Clone()
        {
            Check check = base.Clone() as Check;

            check.CheckMain = this.checkMain.Clone();
            check.CheckOper = this.checkOper.Clone();
            check.CheckDept = this.checkDept.Clone();
            check.ApproveOper = this.approveOper.Clone();
            check.AproveDept = this.aproveDept.Clone();
            for (int i = 0; i < this.checkList.Count; i++)
            {
                check.checkList[i] = this.checkList[i].Clone();
            }

            return check;
        }

        #endregion ��¡

        #region ˽�з���
        #endregion ˽�з���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region �ӿ�ʵ��
        #endregion �ӿ�ʵ��
    }
}
