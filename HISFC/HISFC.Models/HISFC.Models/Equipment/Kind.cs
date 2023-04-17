using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// Equipment<br></br>
    /// [��������: ��Ŀ����ʵ��]<br></br>
    /// [�� �� ��: ��ȫ]<br></br>
    /// [����ʱ��: 2007-09-20]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class Kind : FS.HISFC.Models.Base.Spell
    {
        /// <summary>
        /// �豸��
        /// </summary>
        public Kind()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ����

        /// <summary>
        /// ���൱ǰ���
        /// </summary>
        private string kindLevel;

        ///// <summary>
        ///// ��Ŀ������Ϣ
        ///// </summary>
        //private FS.FrameWork.Models.NeuObject kindInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ϼ�����(�����ϼ�����Ϊ0)
        /// </summary>
        private string preCode;

        /// <summary>
        /// �豸����
        /// </summary>
        private NeuObject dept = new NeuObject();

        /// <summary>
        /// ������
        /// </summary>
        private string nationCode;

        /// <summary>
        /// �Ƿ���Ҫ�Ǽ�1��0��
        /// </summary>
        private string regFlag;

        /// <summary>
        /// �Ƿ��۾�1��0��
        /// </summary>
        private string deFlag;

        /// <summary>
        /// �Ƿ�ĩ��1��0��
        /// </summary>
        private string leafFlag;

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private string validFlag;

        /// <summary>
        /// ˳���
        /// </summary>
        private int orderNO;

        /// <summary>
        /// �����Ŀ
        /// </summary>
        private FS.FrameWork.Models.NeuObject account = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ӧ�ɱ�������Ŀ���
        /// </summary>
        private string statCode;

        /// <summary>
        /// ��ע
        /// </summary>
        private string mark;

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ���൱ǰ���
        /// </summary>
        public string KindLevel
        {
            get
            {
                return this.kindLevel;
            }
            set
            {
                this.kindLevel = value;
            }
        }

        ///// <summary>
        ///// ��Ŀ������Ϣ
        ///// </summary>
        //public FS.FrameWork.Models.NeuObject KindInfo
        //{
        //    get
        //    {
        //        return this.kindInfo;
        //    }
        //    set
        //    {
        //        this.kindInfo = value;
        //    }
        //}

        /// <summary>
        /// �ϼ�����(�����ϼ�����Ϊ0)
        /// </summary>
        public string PreCode
        {
            get
            {
                return this.preCode;
            }
            set
            {
                this.preCode = value;
            }
        }

        /// <summary>
        /// �豸����
        /// </summary>
        public NeuObject Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string NationCode
        {
            get
            {
                return this.nationCode;
            }
            set
            {
                this.nationCode = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ�Ǽ�1��0��
        /// </summary>
        public string RegFlag
        {
            get
            {
                return this.regFlag;
            }
            set
            {
                this.regFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ��۾�1��0��
        /// </summary>
        public string DeFlag
        {
            get
            {
                return this.deFlag;
            }
            set
            {
                this.deFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ�ĩ��1��0��
        /// </summary>
        public string LeafFlag
        {
            get
            {
                return this.leafFlag;
            }
            set
            {
                this.leafFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        public string ValidFlag
        {
            get
            {
                return this.validFlag;
            }
            set
            {
                this.validFlag = value;
            }
        }

        /// <summary>
        /// ˳���
        /// </summary>
        public int OrderNO
        {
            get
            {
                return this.orderNO;
            }
            set
            {
                this.orderNO = value;
            }
        }

        /// <summary>
        /// �����Ŀ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Account
        {
            get
            {
                return this.account;
            }
            set
            {
                this.account = value;
            }
        }

        /// <summary>
        /// ��Ӧ�ɱ�������Ŀ���
        /// </summary>
        public string StatCode
        {
            get
            {
                return this.statCode;
            }
            set
            {
                this.statCode = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Mark
        {
            get
            {
                return this.mark;
            }
            set
            {
                this.mark = value;
            }
        }

        /// <summary>
        /// ����������Ϣ
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new Kind Clone()
        {
            Kind kind = base.Clone() as Kind;

            //kind.KindInfo = this.KindInfo.Clone();
            kind.Account = this.Account.Clone();
            kind.Oper = this.Oper.Clone();

            return kind;
        }

        #endregion

    }
}
