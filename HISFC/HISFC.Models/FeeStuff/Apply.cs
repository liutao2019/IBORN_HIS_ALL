using System;

namespace FS.HISFC.Models.FeeStuff
{
    /// <summary>
    /// [��������: ���ʹ���������]
    /// [�� �� ��: ��˹]
    /// [����ʱ��: 2007-03]
    /// 
    /// ID ������ˮ��
    /// </summary>
    [Serializable]
    public class Apply : FS.HISFC.Models.IMA.IMABase
    {
        public Apply()
        {

        }


        #region ����

        /// <summary>
        /// ���뵥��
        /// </summary>
        private string applyListNO;

        /// <summary>
        /// �������
        /// </summary>
        private int serialNO;

        /// <summary>
        /// ����ʵ��
        /// </summary>
        private FS.HISFC.Models.FeeStuff.MaterialItem item = new MaterialItem();

        /// <summary>
        /// ����۸�
        /// </summary>
        private decimal applyPrice;

        /// <summary>
        /// ������
        /// </summary>
        private decimal applyCost;

        /// <summary>
        /// ����۸�
        /// </summary>
        private decimal purchasePrice;

        /// <summary>
        /// ������
        /// </summary>
        private decimal purchaseCost;

        /// <summary>
        /// ������˾
        /// </summary>
        private FS.FrameWork.Models.NeuObject company = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// Ŀ�굥λ
        /// </summary>
        private FS.FrameWork.Models.NeuObject targeDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����ҿ��
        /// </summary>
        private decimal storeQty;

        /// <summary>
        /// ȫԺ���
        /// </summary>
        private decimal totStoreQty;

        /// <summary>
        /// ������
        /// </summary>
        private decimal outQty;
        /// <summary>
        /// ������{A81EC25B-20A7-4cd5-B284-67207FC91F1F}
        /// </summary>
        private decimal approveQty;
        /// <summary>
        /// ��׼����{A81EC25B-20A7-4cd5-B284-67207FC91F1F}
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment approveOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ������(liuxq ����������)
        /// </summary>
        private decimal outCost;

        /// <summary>
        /// ��Ч��״̬
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ���ⵥ��ˮ��
        /// </summary>
        private string outNo;

        /// <summary>
        /// ������
        /// </summary>
        private string stockNO;

        /// <summary>
        /// ��չ�ֶ�1{A81EC25B-20A7-4cd5-B284-67207FC91F1F}
        /// </summary>
        private string extend1;

        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        private string extend2;

        /// <summary>
        /// ��չ�ֶ�3
        /// </summary>
        private string extend3;

        /// <summary>
        /// ��չ�ֶ�4
        /// </summary>
        private string extend4;

        /// <summary>
        /// ��չ�ֶ�5
        /// </summary>
        private string extend5;

        #endregion

        #region ����
        /// <summary>
        /// ��׼����{A81EC25B-20A7-4cd5-B284-67207FC91F1F}
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment ApproveOper
        {
            get
            {
                return approveOper;
            }
            set 
            {
                approveOper = value;
            }
        }
        /// <summary>
        /// ��������{A81EC25B-20A7-4cd5-B284-67207FC91F1F}
        /// </summary>
        public decimal ApproveQty
        {
            get 
            { 
                return approveQty; 
            }
            set 
            { 
                approveQty = value; 
            }
        }
        /// <summary>
        /// ���뵥��
        /// </summary>
        public string ApplyListNO
        {
            get
            {
                return this.applyListNO;
            }
            set
            {
                this.applyListNO = value;
            }
        }


        /// <summary>
        /// �������
        /// </summary>
        public int SerialNO
        {
            get
            {
                return this.serialNO;
            }
            set
            {
                this.serialNO = value;
            }
        }


        /// <summary>
        /// ������Ŀ
        /// </summary>
        public FS.HISFC.Models.FeeStuff.MaterialItem Item
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
        /// ����۸�
        /// </summary>
        public decimal ApplyPrice
        {
            get
            {
                return this.applyPrice;
            }
            set
            {
                this.applyPrice = value;
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        public decimal ApplyCost
        {
            get
            {
                return this.applyCost;
            }
            set
            {
                this.applyCost = value;
            }
        }


        /// <summary>
        /// ����۸�
        /// </summary>
        public decimal PurchasePrice
        {
            get
            {
                return this.purchasePrice;
            }
            set
            {
                this.purchasePrice = value;
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        public decimal PurchaseCost
        {
            get
            {
                return this.purchaseCost;
            }
            set
            {
                this.purchaseCost = value;
            }
        }


        /// <summary>
        /// ������˾
        /// </summary>
        public FS.FrameWork.Models.NeuObject Company
        {
            get
            {
                return this.company;
            }
            set
            {
                this.company = value;
            }
        }


        /// <summary>
        /// Ŀ�겿��
        /// </summary>
        public FS.FrameWork.Models.NeuObject TargetDept
        {
            get
            {
                return this.targeDept;
            }
            set
            {
                this.targeDept = value;
            }
        }


        /// <summary>
        /// �����ҿ����
        /// </summary>
        public decimal StoreQty
        {
            get
            {
                return this.storeQty;
            }
            set
            {
                this.storeQty = value;
            }
        }


        /// <summary>
        /// ȫԺ�����
        /// </summary>
        public decimal TotStoreQty
        {
            get
            {
                return this.totStoreQty;
            }
            set
            {
                this.totStoreQty = value;
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        public decimal OutQty
        {
            get
            {
                return this.outQty;
            }
            set
            {
                this.outQty = value;
            }
        }


        public decimal OutCost
        {
            get
            {
                return this.outCost;
            }
            set
            {
                this.outCost = value;
            }
        }

        /// <summary>
        /// ��Ч��״̬
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
        /// ���ⵥ��ˮ��
        /// </summary>
        public string OutNo
        {
            get 
            { 
                return outNo; 
            }
            set 
            { 
                outNo = value; 
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string StockNO
        {
            get 
            { 
                return stockNO;
            }
            set 
            {
                stockNO = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�1{A81EC25B-20A7-4cd5-B284-67207FC91F1F}
        /// </summary>
        public string Extend1
        {
            get 
            { 
                return extend1;
            }
            set
            {
                extend1 = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        public string Extend2
        {
            get 
            {
                return extend2;
            }
            set
            {
                extend2 = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�3
        /// </summary>
        public string Extend3
        {
            get 
            { 
                return extend3;
            }
            set
            { 
                extend3 = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�4
        /// </summary>
        public string Extend4
        {
            get 
            { 
                return extend4;
            }
            set 
            {
                extend4 = value; 
            }
        }

        /// <summary>
        /// ��չ�ֶ�5
        /// </summary>
        public string Extend5
        {
            get 
            { 
                return extend5;
            }
            set
            {
                extend5 = value; 
            }
        }
        #endregion

        #region ����

        public new Apply Clone()
        {
            Apply apply = base.Clone() as Apply;

            apply.item = this.item.Clone();

            apply.company = this.company.Clone();

            apply.targeDept = this.targeDept.Clone();
            //{A81EC25B-20A7-4cd5-B284-67207FC91F1F}
            apply.approveOper = this.approveOper.Clone();

            return apply;
        }

        #endregion


    }
}
