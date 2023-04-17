using System;

namespace Neusoft.HISFC.Object.Equipment
{
    /// <summary>
    /// Equipment<br></br>
    /// [��������: �豸ʵ��]<br></br>
    /// [�� �� ��: ��ȫ]<br></br>
    /// [����ʱ��: 2007-09-20]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Equipment : Neusoft.NFC.Object.NeuObject
    {
        /// <summary>
        /// �豸��
        /// </summary>
        public Equipment()
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

        /// <summary>
        /// �豸����
        /// </summary>
        private Neusoft.NFC.Object.NeuObject kind = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// �ϼ�����(�����ϼ�����Ϊ0)
        /// </summary>
        private string preCode;

        /// <summary>
        /// ������
        /// </summary>
        private string nationCode;

        /// <summary>
        /// ƴ����
        /// </summary>
        private string spellCode;
        
        /// <summary>
        /// �����
        /// </summary>
        private string wbCode;

        /// <summary>
        /// �Զ�����
        /// </summary>
        private string userCode;

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
        private Neusoft.NFC.Object.NeuObject account = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ��Ӧ�ɱ�������Ŀ���
        /// </summary>
        private string statCode;

        /// <summary>
        /// ��ע
        /// </summary>
        private string mark;

        /// <summary>
        /// ����Ա����
        /// </summary>
        private Neusoft.NFC.Object.NeuObject oper = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operDate;

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

        /// <summary>
        /// �豸����
        /// </summary>
        public Neusoft.NFC.Object.NeuObject Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

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
        /// ƴ����
        /// </summary>
        public string SpellCode
        {
            get
            {
                return this.spellCode;
            }
            set
            {
                this.spellCode = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string WbCode
        {
            get
            {
                return this.wbCode;
            }
            set
            {
                this.wbCode = value;
            }
        }

        /// <summary>
        /// �Զ�����
        /// </summary>
        public string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                this.userCode = value;
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
        public Neusoft.NFC.Object.NeuObject Account
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
        /// ����Ա
        /// </summary>
        public Neusoft.NFC.Object.NeuObject Oper
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
        /// ����ʱ��
        /// </summary>
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new Equipment Clone()
        {
            Equipment equipment = base.Clone() as Equipment;

            equipment.Kind = this.Kind.Clone();
            equipment.Account = this.Account.Clone();
            equipment.Oper = this.Oper.Clone();

            return equipment;
        }

        #endregion

    }
}
