using System;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[��������: �Ű�ģ��ʵ��]</br>
    /// <br>[�� �� ��: ��С��]</br>
    /// <br>[����ʱ��: 2007-2-1]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class SchemaTemplet:FS.FrameWork.Models.NeuObject,FS.HISFC.Models.Base.IValid
    {
        public SchemaTemplet()
        {                
        }

        #region ����  
        
        /// <summary>
        /// �Һż���
        /// </summary>
        private RegLevel regLevel = new RegLevel();
        /// <summary>
        /// ����
        /// </summary>
        private DayOfWeek week = DayOfWeek.Monday;
        /// <summary>
        /// �Ű�����
        /// </summary>
        private FS.HISFC.Models.Base.EnumSchemaType schemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
        /// <summary>
        /// �������
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ����ҽ��,ģ������Ϊ����ʱ,Ĭ��doctΪ'None'
        /// </summary>
        private FS.FrameWork.Models.NeuObject doct = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ����ҽ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// �������
        /// </summary>
        private FS.FrameWork.Models.NeuObject noon = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime begin = DateTime.MinValue;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime end = DateTime.MinValue;
        /// <summary>
        /// ���˹Һ��޶�
        /// </summary>
        private decimal regQuota = 0m;
        /// <summary>
        /// �绰�Һ��޶�
        /// </summary>
        private decimal telQuota = 0m;
        /// <summary>
        /// ����Һ��޶�
        /// </summary>
        private decimal speQuota = 0m;
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid = false;
        /// <summary>
        /// �Ƿ�Ӻ�
        /// </summary>
        private bool isAppend = false;        
        /// <summary>
        /// ͣ��ԭ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject stopReason = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        /// <summary>
        /// ֹͣ��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment stop = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����

        /// <summary>
        /// �Һż���
        /// </summary>
        public RegLevel RegLevel
        {
            get { return this.regLevel; }
            set { this.regLevel = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public DayOfWeek Week
        {
            get { return week; }
            set { week = value; }
        }

        /// <summary>
        /// �Ű�����
        /// </summary>
        public FS.HISFC.Models.Base.EnumSchemaType EnumSchemaType
        {
            get { return schemaType; }
            set { schemaType = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get { return dept; }
            set { dept = value; }
        }

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Doct
        {
            get { return doct; }
            set { doct = value; }
        }

        /// <summary>
        /// ҽ�����
        /// </summary>
        public FS.FrameWork.Models.NeuObject DoctType
        {
            get { return this.doctType; }
            set { this.doctType = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject Noon
        {
            get { return noon; }
            set { noon = value; }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime Begin
        {
            get { return begin; }
            set { begin = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        /// <summary>
        /// �ֳ��Һ��޶�
        /// </summary>
        public decimal RegQuota
        {
            get { return regQuota; }
            set { regQuota = value; }
        }

        /// <summary>
        /// �绰�Һ��޶�
        /// </summary>
        public decimal TelQuota
        {
            get { return telQuota; }
            set { telQuota = value; }
        }

        /// <summary>
        /// ����Һ��޶�
        /// </summary>
        public decimal SpeQuota
        {
            get { return speQuota; }
            set { speQuota = value; }
        }

        /// <summary>
        /// �Ƿ�Ӻ�
        /// </summary>
        public bool IsAppend
        {
            get { return isAppend; }
            set { isAppend = value; }
        }

        /// <summary>
        /// ͣ��ԭ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject StopReason
        {
            get { return this.stopReason; }
            set { this.stopReason = value; }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        /// <summary>
        /// ֹͣ��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Stop
        {
            get { return this.stop; }
            set { this.stop = value; }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new SchemaTemplet Clone()
        {
            SchemaTemplet obj = base.Clone() as SchemaTemplet;

            obj.regLevel = this.regLevel.Clone();
            obj.Dept = this.Dept.Clone();
            obj.Doct = this.Doct.Clone();
            obj.DoctType = this.DoctType.Clone();
            obj.Noon = this.noon.Clone();
            obj.StopReason = this.StopReason.Clone();
            obj.Oper = this.oper.Clone();
            obj.Stop = this.stop.Clone();
                        
            return obj;
        }
        #endregion

        #region IValid ��Ա

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

        #endregion
    }    
}
