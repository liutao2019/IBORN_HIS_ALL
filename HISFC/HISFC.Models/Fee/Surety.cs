using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Fee
{
    [System.Serializable]
    public class Surety : FS.FrameWork.Models.NeuObject
    {
        #region  ����
        /// <summary>
        /// ������
        /// </summary>
        private NeuObject suretyPerson = new NeuObject();
        /// <summary>
        /// ������
        /// </summary>
        private NeuObject applyPerson = new NeuObject();
        /// <summary>
        /// �������
        /// </summary>
        private decimal suretyCost = 0m;
        /// <summary>
        /// ��ע
        /// </summary>
        private string mark;
        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        /// ��������
        /// </summary>
        private SuretyTypeEnumService suretyType = new SuretyTypeEnumService();

        /// <summary>
        /// �������{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        private int happenNO = 0;

        /// <summary>
        /// ������ {0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        private Base.Bank bank = new FS.HISFC.Models.Base.Bank();

        /// <summary>
        /// ״̬{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        private string state = string.Empty;


       
        /// <summary>
        /// ֧����ʽ{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        private NeuObject payType = new NeuObject();

        /// <summary>
        /// ��Ʊ��{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        private string invoiceNO = string.Empty;

        /// <summary>
        /// �ɷ�Ʊ��{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        private string oldInvoiceNO = string.Empty;

        

       

        #endregion

        #region ����
        /// <summary>
        /// ������
        /// </summary>
        public NeuObject SuretyPerson
        {
            get
            {
                return suretyPerson;
            }
            set
            {
                suretyPerson = value;
            }

        }
        /// <summary>
        /// ������
        /// </summary>
        public NeuObject ApplyPerson
        {
            get
            {
                return applyPerson;
            }
            set
            {
                applyPerson = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public decimal SuretyCost
        {
            get
            {
                return suretyCost;
            }
            set
            {
                suretyCost = value;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
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
        /// <summary>
        /// ��������
        /// </summary>
        public SuretyTypeEnumService SuretyType
        {
            get
            {
                return suretyType;
            }
            set
            {
                suretyType = value;
            }
        }

        /// <summary>
        /// �������{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        public int HappenNO
        {
            get { return happenNO; }
            set { happenNO = value; }
        }
        /// <summary>
        /// ������{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        public Base.Bank Bank
        {
            get { return bank; }
            set { bank = value; }
        }
        /// <summary>
        /// ״̬{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        // <summary>
        /// ֧����ʽ{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        public NeuObject PayType
        {
            get { return payType; }
            set { payType = value; }
        }

        /// <summary>
        /// ��Ʊ��{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        public string InvoiceNO
        {
            get { return invoiceNO; }
            set { invoiceNO = value; }
        }
        /// <summary>
        /// �ɷ�Ʊ��{0374EA05-782E-4609-9CDC-03236AB97906}
        /// </summary>
        public string OldInvoiceNO
        {
            get { return oldInvoiceNO; }
            set { oldInvoiceNO = value; }
        }
        
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Surety Clone()
        {
            Surety obj = base.Clone() as Surety;
            obj.SuretyType = this.SuretyType.Clone() as SuretyTypeEnumService;
            obj.SuretyPerson = this.SuretyPerson.Clone();
            obj.Oper = this.Oper.Clone();
            obj.ApplyPerson = this.ApplyPerson.Clone();

            // ������{0374EA05-782E-4609-9CDC-03236AB97906}
        
            obj.Bank = this.Bank.Clone();
            obj.PayType = this.PayType.Clone();
            return obj;
        }
        #endregion
    }
}
