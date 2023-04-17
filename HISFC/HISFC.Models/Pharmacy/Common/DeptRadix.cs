using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy.Common
{
    /// <summary>
    /// [��������: ����ҩƷ��������]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    [Serializable]
    public class DeptRadix : FS.FrameWork.Models.NeuObject
    {
        public DeptRadix()
        {
        }

        #region �����

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject drugDept = new FS.FrameWork.Models.NeuObject();          

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();

        /// <summary>
        /// ҩƷ����
        /// </summary>
        private decimal radixQty;
      
        /// <summary>
        /// ����ҩƷӯ������
        /// </summary>
        private decimal surplusQty;
      
        /// <summary>
        /// ����ҩƷ������
        /// </summary>
        private decimal expendQty;
    
        /// <summary>
        /// ����ҩƷ������
        /// </summary>
        private decimal supplyQty;

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginDate = System.DateTime.MinValue;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime endDate = System.DateTime.MaxValue;

        /// <summary>
        /// �������� Nurse ����վ��Terminal �նˡ�State ҩ���ն�
        /// </summary>
        private string deptType;

        #endregion

        #region ����

        /// <summary>
        /// ������ 
        /// </summary>
        public FS.FrameWork.Models.NeuObject StockDept
        {
            get
            {
                return this.drugDept;
            }
            set
            {
                this.drugDept = value;
            }
        }

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;

                base.ID = value.ID;
                base.Name = value.Name;
            }
        }

        /// <summary>
        /// ������Ա��Ϣ
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

        /// <summary>
        /// ҩƷ����
        /// </summary>
        public decimal RadixQty
        {
            get
            {
                return radixQty;
            }
            set
            {
                radixQty = value;
            }
        }

        /// <summary>
        /// ����ҩƷӯ������
        /// </summary>
        public decimal SurplusQty
        {
            get
            {
                return surplusQty;
            }
            set
            {
                surplusQty = value;
            }
        }

        /// <summary>
        /// ����ҩƷ������
        /// </summary>
        public decimal ExpendQty
        {
            get
            {
                return expendQty;
            }
            set
            {
                expendQty = value;
            }
        }

        /// <summary>
        /// ����ҩƷ������
        /// </summary>
        public decimal SupplyQty
        {
            get
            {
                return supplyQty;
            }
            set
            {
                supplyQty = value;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return this.beginDate;
            }
            set
            {
                this.beginDate = value;
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }

        /// <summary>
        /// �������� Nurse ����վ��Terminal �նˡ�State ҩ���ն�
        /// </summary>
        public string DeptType
        {
            get
            {
                return this.deptType;
            }
            set
            {
                this.deptType = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DeptRadix Clone()
        {
            DeptRadix deptRadix = base.Clone() as DeptRadix;

            deptRadix.drugDept = this.drugDept.Clone();

            deptRadix.dept = this.dept.Clone();

            deptRadix.item = this.item.Clone();

            deptRadix.oper = this.oper.Clone();

            return deptRadix;
        }

        #endregion
    }
}
