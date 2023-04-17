using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// DeptItem <br></br>
    /// [��������: ���ҳ�����Ŀ]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class DeptItem :  Spell, IValid
    {
        /// <summary>
        /// ��������
        /// </summary>
        public DeptItem()
        {
        }

        #region ˽�г�Ա

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private FS.HISFC.Models.Base.Item itemProperty = new Item();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.Department deptCode = new Department();


        /// <summary>
        /// ��λ��ʶ(1.��ϸ   2.����)
        /// </summary>
        private string unitFlag;

        /// <summary>
        /// ԤԼ��
        /// </summary>
        private string bookLocate;

        /// <summary>
        /// ԤԼ�̶�ʱ��
        /// </summary>
        private string bookTime;

        /// <summary>
        /// ִ�еص�
        /// </summary>
        private string executeLocate;

        /// <summary>
        /// ȡ����ʱ��
        /// </summary>
        private string reportDate;

        /// <summary>
        /// �д�/�޴�  ��0 �У�1�ޣ�
        /// </summary>
        private string hurtFlag;

        /// <summary>
        /// �Ƿ����ԤԼ�� 0�ǣ�1��
        /// </summary>
        private string selfBookFlag;

        /// <summary>
        /// ֪��ͬ����  ��0��Ҫ��1����Ҫ��
        /// </summary>
        private string reasonableFlag;

        /// <summary>
        /// ����רҵ
        /// </summary>
        private string speciality;

        /// <summary>
        /// �ٴ�����
        /// </summary>
        private string clinicMeaning;

        /// <summary>
        /// �걾
        /// </summary>
        private string sampleKind;

        /// <summary>
        /// ��������
        /// </summary>
        private string sampleWay;

        /// <summary>
        /// �걾��λ
        /// </summary>
        private string sampleUnit;

        /// <summary>
        /// �걾��
        /// </summary>
        private decimal sampleQty;

        /// <summary>
        /// ����
        /// </summary>
        private string sampleContainer;

        /// <summary>
        /// ����ֵ��Χ
        /// </summary>
        private string scope;

        /// <summary>
        /// �Ƿ���Ҫͳ��, 0-��Ҫ, 1-����Ҫ
        /// </summary>
        private string ynStat;
        
        /// <summary>
        /// �Ƿ��Զ�ԤԼ, 0-��Ҫ, 1-����Ҫ
        /// </summary>
        private string ynAutoBook;

        /// <summary>
        /// һ����Ŀִ������ʱ��
        /// </summary>
        private string itemTime;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operEnv = new OperEnvironment();

        /// <summary>
        /// ��Ŀ�Ƿ���Ч
        /// </summary>
        private bool isValid;

    	/// <summary>
    	/// �����Զ�����Ŀ����
    	/// </summary>
		private string customName = "";

        #endregion

        #region ����

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperEnv
        {
            get
            {
                return this.operEnv;
            }
            set
            {
                this.operEnv = value;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public FS.HISFC.Models.Base.Item ItemProperty
        {
            get
            {
                return this.itemProperty;
            }
            set
            {
                this.itemProperty = value;
            }
        }

        /// <summary>
        /// ������Ϣ,����Ҫ�ÿ��Ҵ���
        /// </summary>
        [Obsolete("�Ѿ���ʱ������ΪDept", true)]
        public FS.HISFC.Models.Base.Department DeptCode
        {
            get
            {
                return this.deptCode;
            }
            set
            {
                this.deptCode = value;
            }
        }

    	/// <summary>
		/// ������Ϣ
    	/// </summary>
    	public FS.HISFC.Models.Base.Department Dept
    	{
    		get
    		{
				return this.deptCode;
    		}
    		set
    		{
				this.deptCode = value;
    		}
    	}
    	
        /// <summary>
        /// ��λ��ʶ(1.��ϸ   2.����)
        /// </summary>
        public string UnitFlag
        {
            get
            {
                return this.unitFlag;
            }
            set
            {
                this.unitFlag = value;
            }
        }

        /// <summary>
        /// ԤԼ��
        /// </summary>
        public string BookLocate
        {
            get
            {
                return this.bookLocate;
            }
            set
            {
                this.bookLocate = value;
            }
        }

        /// <summary>
        /// ԤԼ�̶�ʱ��
        /// </summary>
        public string BookTime
        {
            get
            {
                return this.bookTime;
            }
            set
            {
                this.bookTime = value;
            }
        }

        /// <summary>
        /// ִ�еص�
        /// </summary>
        public string ExecuteLocate
        {
            get
            {
                return this.executeLocate;
            }
            set
            {
                this.executeLocate = value;
            }
        }

        /// <summary>
        /// ȡ����ʱ��
        /// </summary>
        public string ReportDate
        {
            get
            {
                return this.reportDate;
            }
            set
            {
                this.reportDate = value;
            }
        }

        /// <summary>
        /// �д�/�޴�(0��,  1��)
        /// </summary>
        public string HurtFlag
        {
            get
            {
                return this.hurtFlag;
            }
            set
            {
                this.hurtFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ����ԤԼ(0��,  1��)
        /// </summary>
        public string SelfBookFlag
        {
            get
            {
                return this.selfBookFlag;
            }
            set
            {
                this.selfBookFlag = value;
            }
        }

        /// <summary>
        /// ֪��ͬ����(0��Ҫ, 1����Ҫ)
        /// </summary>
        public string ReasonableFlag
        {
            get
            {
                return this.reasonableFlag;
            }
            set
            {
                this.reasonableFlag = value;
            }
        }

        /// <summary>
        /// ����רҵ
        /// </summary>
        public string Speciality
        {
            get
            {
                return this.speciality;
            }
            set
            {
                this.speciality = value;
            }
        }

        /// <summary>
        /// �ٴ�����
        /// </summary>
        public string ClinicMeaning
        {
            get
            {
                return this.clinicMeaning;
            }
            set
            {
                this.clinicMeaning = value;
            }
        }

        /// <summary>
        /// �걾
        /// </summary>
        public string SampleKind
        {
            get
            {
                return this.sampleKind;
            }
            set
            {
                this.sampleKind = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string SampleWay
        {
            get
            {
                return this.sampleWay;
            }
            set
            {
                this.sampleWay = value;
            }
        }

        /// <summary>
        /// �걾��λ
        /// </summary>
        public string SampleUnit
        {
            get
            {
                return this.sampleUnit;
            }
            set
            {
                this.sampleUnit = value;
            }
        }

        /// <summary>
        /// �걾��
        /// </summary>
        public decimal SampleQty
        {
            get
            {
                return this.sampleQty;
            }
            set
            {
                this.sampleQty = value;
            }
        }

        /// <summary>
        /// �걾����
        /// </summary>
        public string SampleContainer
        {
            get
            {
                return this.sampleContainer;
            }
            set
            {
                this.sampleContainer = value;
            }
        }

        /// <summary>
        /// ����ֵ��Χ
        /// </summary>
        public string Scope
        {
            get
            {
                return this.scope;
            }
            set
            {
                this.scope = value;
            }
        }
        
        /// <summary>
        /// һ����Ŀִ������ʱ��
        /// </summary>
        public string ItemTime
        {
            get
            {
                return this.itemTime;
            }
            set
            {
                this.itemTime = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ�ԤԼ, 0-��Ҫ, 1-����Ҫ
        /// </summary>
        public string IsAutoBook
        {
            get
            {
                return this.ynAutoBook;
            }
            set
            {
                this.ynAutoBook = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫͳ��, 0-��Ҫ, 1-����Ҫ
        /// </summary>
        public string IsStat
        {
            get
            {
                return this.ynStat;
            }
            set
            {
                this.ynStat = value;
            }
        }
        
    	/// <summary>
		/// �����Զ�����Ŀ����
    	/// </summary>
    	public string CustomName
    	{
    		get
    		{
				return this.customName;
    		}
    		set
    		{
				this.customName = value;
    		}
    	}

        #endregion

        #region ��¡����

        public new DeptItem Clone()
        {
            DeptItem di = base.Clone() as DeptItem;
            di.itemProperty = this.itemProperty.Clone();
            return di;
        }

        #endregion

        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                return this.isValid;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                this.isValid = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}