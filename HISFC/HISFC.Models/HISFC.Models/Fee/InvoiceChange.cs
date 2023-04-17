using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Fee
{
    /// <summary>
    /// Invoice<br></br>
    /// [��������: ��Ʊ�����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-05-14]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
     [System.Serializable]
    public class InvoiceChange : NeuObject
    {

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        private int happenNO;

        /// <summary>
        /// ��Ʊ��ȡ��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment getOper = new OperEnvironment();

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        /// 
         
        //private InvoiceTypeEnumService enumInvoiceType = new InvoiceTypeEnumService();
        private NeuObject invoiceType = new NeuObject ();
        
        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new OperEnvironment();

        /// <summary>
        /// ��Ʊ��ʼ��
        /// </summary>
        private string beginNO;

        /// <summary>
        /// ��Ʊ��ֹ��
        /// </summary>
        private string endNO;

        /// <summary>
        /// ��ǰʹ�ú�
        /// </summary>
        private string usedNO;

        /// <summary>
        /// �������
        /// </summary>
        private string shiftType;

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        public int HappenNO
        {
            get 
            {
                return this.happenNO;
            }
            set 
            {
                this.happenNO = value;
            }
        }


        /// <summary>
        /// ��Ʊ��ȡ��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment GetOper
        {
            get 
            {
                return this.getOper;
            }
            set
            {
                this.getOper = value;
            }
        }

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        /// 
        
        //public InvoiceTypeEnumService InvoiceType
        //{
        //    get
        //    {
        //        return this.enumInvoiceType;
        //    }
        //    set
        //    {
        //        this.enumInvoiceType = value;
        //    }
        //}

        public NeuObject InvoiceType
        {

            get
            {
                return this.invoiceType;
            }
            set
            {
                this.invoiceType = value;
            }
        }
        
        /// <summary>
        /// ����Ա
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
        /// ��Ʊ��ʼ��
        /// </summary>
        public string BeginNO
        {
            get
            {
                return this.beginNO;
            }
            set
            {
                this.beginNO = value.PadLeft(12, '0');
            }

        }

        /// <summary>
        /// ��Ʊ��ֹ��
        /// </summary>
        public string EndNO
        {
            get
            {
                return this.endNO;
            }
            set
            {
                this.endNO = value.PadLeft(12, '0');
            }

        }

        /// <summary>
        /// ��ǰʹ�ú�
        /// </summary>
        public string UsedNO
        {
            get
            {
                return this.usedNO;
            }
            set
            {
                this.usedNO = value.PadLeft(12, '0');
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string ShiftType
        {
            get 
            {
                return this.shiftType;
            }
            set 
            {
                this.shiftType = value;
            }
        }

        #endregion
                
        #region ����

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>��ǰ�����ʵ������</returns>
        public new InvoiceChange Clone()
        {
            InvoiceChange invoiceChange = base.Clone() as InvoiceChange;
            invoiceChange.GetOper = this.GetOper.Clone();
            invoiceChange.Oper = this.Oper.Clone();
            invoiceChange.InvoiceType = this.InvoiceType.Clone();
            return invoiceChange;
        }

        #endregion

        #endregion
    }
}
