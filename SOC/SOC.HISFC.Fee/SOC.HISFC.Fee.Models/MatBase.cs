using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.Fee.Models
{
    /// <summary>
    /// MatBase<br></br>
    /// <Font color='#FF1111'>[��������: �����ֵ�ʵ����]</Font><br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2009-11-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    [Serializable]
    public class MatBase : FS.HISFC.Models.Base.Item
    {
        #region ���캯��

        public MatBase()
        {
            base.ItemType = FS.HISFC.Models.Base.EnumItemType.MatItem;
            base.IsValid = true;
            base.PackQty = 1;
        }

        #endregion ���캯��

        #region ����

        #region ˽�б���

        ///// <summary>
        ///// ���ʷ���
        ///// </summary>
        //private MatKind kind = new MatKind();

        /// <summary>
        /// �ֿ�
        /// </summary>
        private FS.FrameWork.Models.NeuObject storage = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ұ���--����
        /// </summary>
        //private string gbCode = "";

        /// <summary>
        /// ����
        /// </summary>
        private FS.HISFC.Models.Base.Spell otherName = new FS.HISFC.Models.Base.Spell();

        ///// <summary>
        ///// ��Ч��Χ
        ///// </summary>
        //private BizLogic.EnumEffectArea effectArea = FS.HISFC.BizLogic.Material.BizLogic.EnumEffectArea.������;

        /// <summary>
        /// ��Ч����-����Ч��ΧΪָ������ʱ��Ч
        /// </summary>
        private string effectDept = "";

        /// <summary>
        /// ���-������
        /// </summary>
        //private string specs = "";

        /// <summary>
        /// ��С��λ
        /// </summary>
        private string minUnit = "";

        /// <summary>
        /// ��������-���װ
        /// </summary>
        private decimal inPrice = 0;

        /// <summary>
        /// ���ۼ۸�
        /// </summary>
        private decimal salePrice = 0;

        /// <summary>
        /// ���װ��λ
        /// </summary>
        private string packUnit = "";

        /// <summary>
        /// ���װ����-������
        /// </summary>
        //private decimal packQty = 1;

        /// <summary>
        /// ���װ�۸�
        /// </summary>
        private decimal packPrice = 0;


        /// <summary>
        /// ��С���ô���
        /// </summary>
        private string feeCode = "";

        /// <summary>
        /// �����շѱ�־
        /// </summary>
        private bool isFee = false;

        /// <summary>
        /// �Ƿ���Ч-����
        /// </summary>
        //private bool isValid = true;

        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        private bool isSpecial = false;

        /// <summary>
        /// �Ƿ��Ǹ�ֵ�Ĳ�
        /// </summary>
        private bool isHighValue = false;

        ///// <summary>
        ///// ��������
        ///// </summary>
        //private MatCompany factory = new MatCompany();

        ///// <summary>
        ///// ������˾
        ///// </summary>
        //private MatCompany company = new MatCompany();

        /// <summary>
        /// ��Դ
        /// </summary>
        private string inSource = "";

        /// <summary>
        /// ��;
        /// </summary>
        private string usage = "";

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string approveInfo = "";

        /// <summary>
        /// ������
        /// </summary>
        private string mader = "";

        /// <summary>
        /// ע���
        /// </summary>
        private string registerCode = "";

        /// <summary>
        /// �������
        /// </summary>
        private string specialType = "";

        /// <summary>
        /// ע��ʱ��
        /// </summary>
        private DateTime registerDate;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime overDate;

        /// <summary>
        /// �Ƿ���-��Ӧ����(1��0��)
        /// </summary>
        private bool isPack = false;

        /// <summary>
        /// �����Ƿ������
        /// </summary>
        private bool isExamine = false;

        /// <summary>
        /// �Ƿ�Ϊһ���ԺĲı�־
        /// </summary>
        private bool isNoRecycle = false;

        /// <summary>
        /// �Ƿ����ι���
        /// </summary>
        /// {5E811F39-FCA7-4bbf-B2E0-62AD5D499D35}
        private bool isNeedBatchNo = false;

        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        ///// <summary>
        ///// ��Ӧ������������֤��Ϣ�б�
        ///// </summary>
        //private List<MatBaseReg> baseRegList = new List<MatBaseReg>();

        ///// <summary>
        ///// ����
        ///// </summary>
        //private List<MatFile> files = new List<MatFile>();

        /// <summary>
        /// �Ƿ��¼ƻ����
        /// </summary>
        /// {7A8734DD-78FB-40ec-817E-8964CE065D90}
        private bool isPlan = false;

        private bool isB = false;

        private string extend1;
        

        #endregion ˽�б���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region ����
        /// <summary>
        /// �Ƿ��¼ƻ����
        /// </summary>
        /// {7A8734DD-78FB-40ec-817E-8964CE065D90}
        public bool IsPlan
        {
            get { return isPlan; }
            set { isPlan = value; }
        }

        ///// <summary>
        ///// ���ʷ���
        ///// </summary>
        //public MatKind Kind
        //{
        //    get
        //    {
        //        return kind;
        //    }
        //    set
        //    {
        //        kind = value;
        //    }
        //}

        /// <summary>
        /// �ֿ�
        /// </summary>
        public FS.FrameWork.Models.NeuObject Storage
        {
            get
            {
                return storage;
            }
            set
            {
                storage = value;
            }
        }

        /// <summary>
        /// ���ұ���--����
        /// </summary>
        //public string GbCode
        //{
        //    get
        //    {
        //        return gbCode;
        //    }
        //    set
        //    {
        //        gbCode = value;
        //    }
        //}

        /// <summary>
        /// ����
        /// </summary>
        public FS.HISFC.Models.Base.Spell OtherName
        {
            get
            {
                return otherName;
            }
            set
            {
                otherName = value;
            }
        }

        ///// <summary>
        ///// ��Ч��Χ
        ///// </summary>
        //public BizLogic.EnumEffectArea EffectArea
        //{
        //    get
        //    {
        //        return effectArea;
        //    }
        //    set
        //    {
        //        effectArea = value;
        //    }
        //}

        /// <summary>
        /// ��Ч����-����Ч��ΧΪָ������ʱ��Ч
        /// </summary>
        public string EffectDept
        {
            get
            {
                return effectDept;
            }
            set
            {
                effectDept = value;
            }
        }

        /// <summary>
        /// ��С��λ
        /// </summary>
        public string MinUnit
        {
            get
            {
                return minUnit;
            }
            set
            {
                minUnit = value;
            }
        }

        /// <summary>
        /// ��������-���װ
        /// </summary>
        public decimal InPrice
        {
            get
            {
                return inPrice;
            }
            set
            {
                inPrice = value;
            }
        }

        /// <summary>
        /// ���ۼ۸�
        /// </summary>
        public decimal SalePrice
        {
            get
            {
                return salePrice;
            }
            set
            {
                salePrice = value;
            }
        }

        /// <summary>
        /// ���װ��λ
        /// </summary>
        public string PackUnit
        {
            get
            {
                return packUnit;
            }
            set
            {
                packUnit = value;
            }
        }

        /// <summary>
        /// ���װ�۸�
        /// </summary>
        public decimal PackPrice
        {
            get
            {
                return packPrice;
            }
            set
            {
                packPrice = value;
            }
        }

        ///// <summary>
        ///// �Ӽ۷�ʽ
        ///// </summary>
        //public BizLogic.EnumAddRateType AddRateType
        //{
        //    get
        //    {
        //        return addRateType;
        //    }
        //    set
        //    {
        //        addRateType = value;
        //    }
        //}

        /// <summary>
        /// ��С���ô���
        /// </summary>
        public string FeeCode
        {
            get
            {
                return feeCode;
            }
            set
            {
                feeCode = value;
            }
        }

        /// <summary>
        /// �����շѱ�־
        /// </summary>
        public bool IsFee
        {
            get
            {
                return isFee;
            }
            set
            {
                isFee = value;
            }
        }

        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        public bool IsSpecial
        {
            get
            {
                return isSpecial;
            }
            set
            {
                isSpecial = value;
            }
        }

        /// <summary>
        /// �Ƿ��Ǹ�ֵ�Ĳ�
        /// </summary>
        public bool IsHighValue
        {
            get
            {
                return isHighValue;
            }
            set
            {
                isHighValue = value;
            }
        }

        ///// <summary>
        ///// ��������
        ///// </summary>
        //public MatCompany Factory
        //{
        //    get
        //    {
        //        return factory;
        //    }
        //    set
        //    {
        //        factory = value;
        //    }
        //}

        ///// <summary>
        ///// ������˾
        ///// </summary>
        //public MatCompany Company
        //{
        //    get
        //    {
        //        return company;
        //    }
        //    set
        //    {
        //        company = value;
        //    }
        //}

        /// <summary>
        /// ��Դ
        /// </summary>
        public string InSource
        {
            get
            {
                return inSource;
            }
            set
            {
                inSource = value;
            }
        }

        /// <summary>
        /// ��;
        /// </summary>
        public string Usage
        {
            get
            {
                return usage;
            }
            set
            {
                usage = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ApproveInfo
        {
            get
            {
                return approveInfo;
            }
            set
            {
                approveInfo = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string Mader
        {
            get
            {
                return mader;
            }
            set
            {
                mader = value;
            }
        }

        /// <summary>
        /// ע���
        /// </summary>
        public string RegisterCode
        {
            get
            {
                return registerCode;
            }
            set
            {
                registerCode = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string SpecialType
        {
            get
            {
                return specialType;
            }
            set
            {
                specialType = value;
            }
        }

        /// <summary>
        /// ע��ʱ��
        /// </summary>
        public DateTime RegisterDate
        {
            get
            {
                return registerDate;
            }
            set
            {
                registerDate = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OverDate
        {
            get
            {
                return overDate;
            }
            set
            {
                overDate = value;
            }
        }

        /// <summary>
        /// �Ƿ���-��Ӧ����(1��0��)
        /// </summary>
        public bool IsPack
        {
            get
            {
                return isPack;
            }
            set
            {
                isPack = value;
            }
        }

        /// <summary>
        /// �����Ƿ������
        /// </summary>
        public bool IsExamine
        {
            get
            {
                return isExamine;
            }
            set
            {
                isExamine = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ϊһ���ԺĲı�־
        /// </summary>
        public bool IsNoRecycle
        {
            get
            {
                return isNoRecycle;
            }
            set
            {
                isNoRecycle = value;
            }
        }

        /// <summary>
        /// �Ƿ����κŹ���
        /// </summary>
        /// {5E811F39-FCA7-4bbf-B2E0-62AD5D499D35}
        public bool IsNeedBatchNo
        {
            get
            {
                return isNeedBatchNo;
            }
            set
            {
                isNeedBatchNo = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

 
      
        public string Extend1
        {
            set
            {
                this.extend1 = value;
            }
            get
            {
                return this.extend1;
            }
        }

        public bool IsB
        {
            set
            {
                this.isB = value;
            }
            get
            {
                return this.isB;
            }
        }

        /// <summary>
        /// ��Ʒע���
        /// </summary>
        private string registerNo = "";
        /// <summary>
        /// ��Ʒע���
        /// </summary>
        public string RegisterNo
        {
            set
            {
                this.registerNo = value;
            }
            get
            {
                return this.registerNo;
            }
        }
        #endregion ����

        #region ����

        #region ��Դ�ͷ�
        #endregion ��Դ�ͷ�

        #region ��¡

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>���ؿ�¡���ʵ��</returns>
        public new MatBase Clone()
        {
            MatBase matBase = base.Clone() as MatBase;

            matBase.storage = this.storage.Clone();
            matBase.otherName = this.otherName.Clone();
         

            return matBase;
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
