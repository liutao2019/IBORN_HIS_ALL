using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord.Visit
{
    /// <summary>
    /// VisitSearches <br></br>
    /// [��������: ��ü�������ʵ��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-09-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class VisitSearches : Case.CaseInfo
    {
        #region ����

        /// <summary>
        /// ���뻷��(����ҽ��,����ʱ��)
        /// </summary>
        private HISFC.Models.Base.OperEnvironment doctorOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ҽ����ʦ,��ʦ�ĵ绰�����ֶ�User01��
        /// </summary>
        private FS.FrameWork.Models.NeuObject teacher = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �������ݣ����֡����ޡ�������Ŀ��
        /// </summary>
        private string searchesContent = "";

        /// <summary>
        /// ԤԼ����ʱ��
        /// </summary>
        private DateTime bookingTime = DateTime.MinValue;
        
        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        private bool isCharge = false;

        /// <summary>
        /// �շѽ��
        /// </summary>
        private decimal chargeCost = 0m;

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject illType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private decimal years = 0m;

        /// <summary>
        /// ��Ŀ
        /// </summary>
        private FS.FrameWork.Models.NeuObject items = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private string copy = "";

        /// <summary>
        /// ����
        /// </summary>
        private string append = "";

        /// <summary>
        /// ����״̬
        /// </summary>
        private EnumSearchesState searchesState = new EnumSearchesState();

        /// <summary>
        /// ��˻���
        /// </summary>
        private HISFC.Models.Base.OperEnvironment auditingOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// ��������
        /// </summary>
        private HISFC.Models.Base.OperEnvironment searchesOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��Ϣ�Ƴ��������,����浽User01��
        /// </summary>
        private HISFC.Models.Base.OperEnvironment notionOper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ���뻷��(����ҽ��,����ʱ��)
        /// </summary>
        public HISFC.Models.Base.OperEnvironment DoctorOper
        {
            get 
            {
                return this.doctorOper; 
            }
            set
            { 
                this.doctorOper = value; 
            }
        }

        /// <summary>
        /// ҽ����ʦ,��ʦ�ĵ绰�����ֶ�User01��
        /// </summary>
        public FS.FrameWork.Models.NeuObject Teacher
        {
            get 
            {
                return this.teacher; 
            }
            set
            {
                this.teacher = value; 
            }
        }

        /// <summary>
        /// �������ݣ����֡����ޡ�������Ŀ��
        /// </summary>
        public string SearchesContent
        {
            get 
            {
                return this.searchesContent; 
            }
            set
            {
                this.searchesContent = value; 
            }
        }

        /// <summary>
        /// ԤԼ����ʱ��
        /// </summary>
        public DateTime BookingTime
        {
            get 
            {
                return this.bookingTime; 
            }
            set
            {
                this.bookingTime = value; 
            }
        }

        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        public bool IsCharge
        {
            get 
            { 
                return this.isCharge; 
            }
            set 
            {
                this.isCharge = value; 
            }
        }

        /// <summary>
        /// �շѽ��
        /// </summary>
        public decimal ChargeCost
        {
            get 
            { 
                return this.chargeCost; 
            }
            set 
            { 
                this.chargeCost = value; 
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject IllType
        {
            get 
            { 
                return this.illType; 
            }
            set 
            { 
                this.illType = value; 
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        public decimal Years
        {
            get 
            { 
                return this.years; 
            }
            set 
            { 
                this.years = value; 
            }
        }


        /// <summary>
        /// ��Ŀ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Items
        {
            get 
            { 
                return this.items; 
            }
            set 
            { 
                this.items = value; 
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Copy
        {
            get 
            { 
                return this.copy; 
            }
            set 
            { 
                this.copy = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Append
        {
            get 
            { 
                return this.append; 
            }
            set 
            { 
                this.append = value;
            }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public EnumSearchesState SearchesState
        {
            get 
            { 
                return this.searchesState; 
            }
            set 
            {
                this.searchesState = value; 
            }
        }

        /// <summary>
        /// ��˻���
        /// </summary>
        public HISFC.Models.Base.OperEnvironment AuditingOper
        {
            get 
            { 
                return this.auditingOper; 
            }
            set 
            { 
                this.auditingOper = value; 
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public HISFC.Models.Base.OperEnvironment SearchesOper
        {
            get 
            { 
                return this.searchesOper; 
            }
            set 
            { 
                this.searchesOper = value; 
            }
        }

        /// <summary>
        /// ��Ϣ�Ƴ��������,����浽User01��
        /// </summary>
        public HISFC.Models.Base.OperEnvironment NotionOper
        {
            get 
            { 
                return this.notionOper; 
            }
            set 
            { 
                this.notionOper = value; 
            }
        }

        #endregion

        #region ����

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>��ü�������ʵ��</returns>
        public new VisitSearches Clone()
        {
            VisitSearches visitSearches = base.Clone() as VisitSearches;

            visitSearches.doctorOper = this.DoctorOper.Clone();
            visitSearches.teacher = this.Teacher.Clone();
            visitSearches.auditingOper = this.AuditingOper.Clone();
            visitSearches.searchesOper = this.SearchesOper.Clone();
            visitSearches.notionOper = this.NotionOper.Clone();

            return visitSearches;
        }

        #endregion

        #endregion
    }
}
