using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy.Base
{
    [Serializable]
    public class PlanBase : FS.FrameWork.Models.NeuObject
    {
        public PlanBase() 
		{

		}

        #region ����

        /// <summary>
        /// ���ݺ�
        /// </summary>
        private System.String myBillNo;

        /// <summary>
        /// ����״̬
        /// </summary>
        private System.String myState;

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject myDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private Item myItem = new Item();

        /// <summary>
        /// �����ҿ��
        /// </summary>
        private System.Decimal myStoreQty;

        /// <summary>
        /// ȫԺ���
        /// </summary>
        private System.Decimal myStoreTotQty;

        /// <summary>
        /// ȫԺ��������
        /// </summary>
        private System.Decimal myOutputQty;

        /// <summary>
        /// �ƻ�����
        /// </summary>
        private System.Decimal myPlanQty;

        /// <summary>
        /// �ƻ������
        /// </summary>
        private System.Decimal myStockPrice;

        /// <summary>
        /// �ƻ���
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment planOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �ɹ���Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment stockOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �ɹ�����
        /// </summary>
        private decimal sortNO;

        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        private string extend;

        #endregion

        /// <summary>
        /// ���ݺ� �ƻ���/�ɹ���
        /// </summary>
        public System.String BillNO
        {
            get
            {
                return this.myBillNo;
            }
            set
            {
                this.myBillNo = value;
            }
        }

        /// <summary>
        /// ����״̬ 0�ƻ�����1�ɹ���  2 �ɹ���� 3 ����� 4 ���ϼƻ���
        /// </summary>
        public System.String State
        {
            get
            {
                return this.myState;
            }
            set
            {
                this.myState = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.myDept;
            }
            set
            {
                this.myDept = value;
            }
        }

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            get
            {
                return this.myItem;
            }
            set
            {
                this.myItem = value;
            }
        }

        /// <summary>
        /// �����ҿ������
        /// </summary>
        public System.Decimal StoreQty
        {
            get
            {
                return this.myStoreQty;
            }
            set
            {
                this.myStoreQty = value;
            }
        }

        /// <summary>
        /// ȫԺ����ܺ�
        /// </summary>
        public System.Decimal StoreTotQty
        {
            get
            {
                return this.myStoreTotQty;
            }
            set
            {
                this.myStoreTotQty = value;
            }
        }

        /// <summary>
        /// ȫԺ��������
        /// </summary>
        public System.Decimal OutputQty
        {
            get
            {
                return this.myOutputQty;
            }
            set
            {
                this.myOutputQty = value;
            }
        }

        /// <summary>
        /// �ƻ������
        /// </summary>
        public System.Decimal PlanQty
        {
            get
            {
                return this.myPlanQty;
            }
            set
            {
                this.myPlanQty = value;
            }
        }

        /// <summary>
        /// �ƻ���Ա��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment PlanOper
        {
            get
            {
                return this.planOper;
            }
            set
            {
                this.planOper = value;
            }
        }

        /// <summary>
        /// �ɹ���Ա��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment StockOper
        {
            get
            {
                return this.stockOper;
            }
            set
            {
                this.stockOper = value;
            }
        }

        /// <summary>
        /// ������Ϣ
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
        /// ˳��� Ԥ��ʵ���ֶ� ��ʱû��ʹ�� ˳��ſ���ͨ����ˮ������
        /// </summary>
        public decimal SortNO
        {
            get
            {
                return this.sortNO;
            }
            set            
            {
                this.sortNO = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        public string Extend
        {
            get
            {
                return this.extend;
            }
            set
            {
                this.extend = value;
            }
        }

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ص�ǰʵ���Ŀ�¡ʵ��</returns>
        public new PlanBase Clone()
        {
            PlanBase planBase = base.Clone() as PlanBase;

            planBase.Dept = this.Dept.Clone();
            planBase.Item = this.Item.Clone();

            planBase.PlanOper = this.PlanOper.Clone();
            planBase.StockOper = this.StockOper.Clone();
            planBase.Oper = this.Oper.Clone();

            return planBase;
        }


        #endregion
    }
}
