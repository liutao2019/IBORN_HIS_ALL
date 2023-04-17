using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Equipment
{
    /// <summary>
    /// Equipment<br></br>
    /// [��������: �豸��Ŀʵ��]<br></br>
    /// [�� �� ��: ��ȫ]<br></br>
    /// [����ʱ��: 2007-09-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class EquipAccount : Neusoft.NFC.Object.NeuObject
    {
        /// <summary>
        /// �豸��Ŀ��
        /// </summary>
        public EquipAccount()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ����

        /// <summary>
        /// �豸��Ŀ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject item = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string kindCode;

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
        /// ���
        /// </summary>
        private string specs;

        /// <summary>
        /// ��λ
        /// </summary>
        private string unit;

        /// <summary>
        /// �����
        /// </summary>
        private float price;

        /// <summary>
        /// ״̬
        /// </summary>
        private string state;

        /// <summary>
        /// �۾ɷ�ʽ
        /// </summary>
        private string deType;

        /// <summary>
        /// �۾�����(��)
        /// </summary>
        private int deTeam;

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        private string measureFlag;

        /// <summary>
        /// �������
        /// </summary>
        private float upLimit;

        /// <summary>
        /// �������
        /// </summary>
        private float downLimit;

        /// <summary>
        /// �̱�
        /// </summary>
        private string brand;

        /// <summary>
        /// �ⷿ����
        /// </summary>
        private string storageCode;

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
        /// �豸��Ŀ
        /// </summary>
        public Neusoft.NFC.Object.NeuObject Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string KindCode
        {
            get
            {
                return this.kindCode;
            }
            set
            {
                this.kindCode = value;
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
        /// ���
        /// </summary>
        public string Specs
        {
            get
            {
                return this.specs;
            }
            set
            {
                this.specs = value;
            }
        }

        /// <summary>
        /// ��λ
        /// </summary>
        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public float Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        /// <summary>
        /// �۾ɷ�ʽ
        /// </summary>
        public string DeType
        {
            get
            {
                return this.deType;
            }
            set
            {
                this.deType = value; 
            }
        }

        /// <summary>
        /// �۾�����(��)
        /// </summary>
        public int DeTeam
        {
            get
            {
                return this.deTeam;
            }
            set
            {
                this.deTeam = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        public string MeasureFlag
        {
            get
            {
                return this.measureFlag;
            }
            set
            {
                this.measureFlag = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public float UpLimit
        {
            get
            {
                return this.upLimit;
            }
            set
            {
                this.upLimit = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public float DownLimit
        {
            get
            {
                return this.downLimit;
            }
            set
            {
                this.downLimit = value;
            }
        }

        /// <summary>
        /// �̱�
        /// </summary>
        public string Brand
        {
            get
            {
                return this.brand;
            }
            set
            {
                this.brand = value;
            }
        }

        /// <summary>
        /// �ⷿ����
        /// </summary>
        public string StorageCode
        {
            get
            {
                return this.storageCode;
            }
            set
            {
                this.storageCode = value;
            }
        }

        /// <summary>
        /// ����Ա����
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
        public new EquipAccount Clone()
        {
            EquipAccount equipAccount = base.Clone() as EquipAccount;

            equipAccount.Item = this.Item.Clone();
            equipAccount.Oper = this.Oper.Clone();

            return equipAccount;
        }

        #endregion
    }
}
