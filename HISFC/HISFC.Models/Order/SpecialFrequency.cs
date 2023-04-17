using System;

namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.SpecialFrequency<br></br>
	/// [��������: ����Ƶ��ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class SpecialFrequency : FS.FrameWork.Models.NeuObject
    {
        public SpecialFrequency()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        ///  //ҽ����ˮ��
        /// </summary>
        private string orderID;

        /// <summary>
        /// //ҽ����Ϻ�
        /// </summary>
        private FS.HISFC.Models.Order.Combo combo;

        /// <summary>
        ///  //Ƶ�ε�
        /// </summary>
        private string point;

        /// <summary>
        /// //  Ƶ�ε�����
        /// </summary>
        private string dose;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Base.OperEnvironment oper;//��������

        #endregion

        #region ����
        /// <summary>
        /// ҽ����
        /// </summary>
        public string OrderID
        {
            get
            {
                if (orderID == null)
                {
                    orderID = string.Empty;
                }
                return this.orderID;
            }
            set
            {
                this.orderID = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public Combo Combo
        {
            set
            {
                if (combo == null)
                {
                    combo = new Combo();
                }
                this.combo = value;
            }
            get
            {
                return this.combo;
            }
        }

        /// <summary>
        /// ʱ���
        /// </summary>
        public string Point
        {
            get
            {
                if (point == null)
                {
                    point = string.Empty;
                }
                return this.point;
            }
            set
            {
                this.point = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Dose
        {
            get
            {
                if (dose == null)
                {
                    dose = string.Empty;
                }
                return this.dose;
            }
            set
            {
                this.dose = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public Base.OperEnvironment Oper
        {
            get
            {
                if (oper == null)
                {
                    oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        #endregion

        #region ���ϵ�

        [Obsolete("��OrderID����", true)]
        public string moOrder; //ҽ����ˮ��
        [Obsolete("��Combo.ID����", true)]
        public string combNo;  //ҽ����Ϻ�
        [Obsolete("��ID����", true)]
        public string drqFreqtype; //Ƶ������
        [Obsolete("��Name����", true)]
        public string drefreqName; //Ƶ������
        [Obsolete("��Point����", true)]
        public string drqPoint; //Ƶ�ε�
        [Obsolete("��Dose����", true)]
        public string dosePoint; //  Ƶ�ε�����
        [Obsolete("��Oper.ID����", true)]
        public string OperID; // ����Ա
        [Obsolete("��Oper.OperTime����", true)]
        public System.DateTime operDate; //����ʱ��

        #endregion

        #region ����

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new SpecialFrequency Clone()
        {
            SpecialFrequency obj = base.Clone() as SpecialFrequency;
            obj.combo = this.Combo.Clone();
            obj.oper = this.Oper.Clone();
            return obj;
        }

        #endregion

        #endregion

    }
}
