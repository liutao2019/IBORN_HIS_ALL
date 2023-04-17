using System;

namespace FS.HISFC.Models.Nurse
{
	/// <summary>
	/// Queue<br></br>
	/// [��������: �������ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
    ///		�޸���='��ΰ��'
	///		�޸�ʱ��='2007-02-07'
	///		�޸�Ŀ��='����ֶ�,ԭ���Ĳ���'
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Queue:FS.FrameWork.Models.NeuObject
    {
        #region ˽�г�Ա

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime queueTime = DateTime.MinValue;


        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        private int order = 0;


        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid = false;

        /// <summary>
        /// �����к�������
        /// </summary>
        private int waitingCount = 0;

        /// <summary>
        /// ר�Ҷ��б�־
        /// </summary>
        private string expertFlag = "";

        /// <summary>
        /// ��̨��Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject console = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject assignDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �������
        /// </summary>
        private FS.FrameWork.Models.NeuObject noon = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject room = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ҽ�� ID, Name, Memo
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        /// <summary>
        /// ����ҽ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject Doctor
        {
            get
            {
                return this.doctor;
            }
            set
            {
                this.doctor = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime QueueDate
        {
            get
            {
                return this.queueTime;
            }
            set
            {
                this.queueTime = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public FS.FrameWork.Models.NeuObject Noon
        {
            get
            {
                return this.noon;
            }
            set
            {
                this.noon = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject SRoom
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = value;
            }
        }
        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        public int Order
        {
            get
            {
                return this.order;
            }
            set
            {
                this.order = value;
            }
        }
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }


        /// <summary>
        /// ��̨��Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Console
        {
            get
            {
                return this.console;
            }
            set
            {
                this.console = value;
            }
        }
        /// <summary>
        /// ר�Ҷ��б�־
        /// </summary>
        public string ExpertFlag
        {
            get
            {
                return this.expertFlag;
            }
            set
            {
                this.expertFlag = value;
            }
        }
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject AssignDept
        {
            get
            {
                return this.assignDept;
            }
            set
            {
                this.assignDept = value;
            }
        }
        /// <summary>
        /// �����к�������
        /// </summary>
        public int WaitingCount
        {
            get
            {
                return this.waitingCount;
            }
            set
            {
                this.waitingCount = value;
            }
        }


        #endregion

        #region ����

        /// <summary>
        /// ����Ա����
        /// </summary>
        [Obsolete("ʹ��Oper.ID", true)]
        public string OperID
        {
            get
            {
                return this.operID;
            }
            set
            {
                this.operID = value;
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("ʹ��Oper.OperTime", true)]
        public DateTime OperDate
        {
            get
            {
                return this.operDate;
            }
            set
            {
                this.operDate = value;
            }
        }

        /// <summary>
        /// ����Ա����
        /// </summary>
        private string operID = "";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operDate = DateTime.MinValue;
        #endregion

        #region ����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new Queue Clone()
        {
            Queue queue = base.Clone() as Queue;
            queue.console = this.console.Clone();
            queue.assignDept = this.assignDept.Clone();
            queue.noon = this.noon.Clone();
            queue.dept = this.dept.Clone();
            queue.room = this.room.Clone();
            queue.doctor = this.doctor.Clone();
            queue.oper = this.oper.Clone();

            return queue;
        }
        #endregion
    }
}