//using System;

//namespace FS.HISFC.Models.Fee
//{
//    /// <summary>
//    /// DerateFee ��ժҪ˵����
//    /// ���������
//    /// memo ����ԭ��
//    /// </summary>
//    public class DerateFee:FS.FrameWork.Models.NeuObject,FS.HISFC.Models.Base.IValid
//    {
//        public DerateFee()
//        {
//            //
//            // TODO: �ڴ˴���ӹ��캯���߼�
//            //
//        }
//        /// <summary>
//        /// ������
//        /// </summary>
//        public decimal DerateCost;
//        /// <summary>
//        /// ��������
//        /// </summary>
//        public FS.FrameWork.Models.NeuObject DerateType=new FS.FrameWork.Models.NeuObject();
//        /// <summary>
//        /// ��С���ô���
//        /// </summary>
//        public string FeeCode;
//        /// <summary>
//        /// ��Ŀ����
//        /// </summary>
//        public string ItemCode;
//        /// <summary>
//        /// ��׼��
//        /// </summary>
//        public FS.FrameWork.Models.NeuObject ConfirmOperator=new FS.FrameWork.Models.NeuObject();
//        #region IInvalid ��Ա
//        /// <summary>
//        /// ��Ч��� false 0 ��Ч
//        ///			 true 1 ��Ч
//        /// </summary>
//        public bool IsValid
//        {
//            get
//            {
//                // TODO:  ��� DerateFee.IsInValid getter ʵ��
//                return bIsInValid;
//            }
//            set
//            {
//                // TODO:  ��� DerateFee.IsInValid setter ʵ��
//                bIsInValid=value;
//            }
//        }

//        #endregion
//        /// <summary>
//        /// ��Ч��� false 0 ��Ч
//        ///			 true 1 ��Ч
//        /// </summary>
//        protected bool bIsInValid = false;
//    }
//}

using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Fee
{
    /// <summary>
    /// DerateFee ��ժҪ˵����{BD300517-D927-43c0-A1D3-8FB99BD10298}
    /// ���������
    /// memo ����ԭ��
    /// </summary>
    [Serializable]
    public class DerateFee : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        #region ����

        public DerateFee()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// סԺ��
        /// </summary>
        private string inpatientNO;

        /// <summary>
        /// ��С���ý��
        /// </summary>
        private decimal feeTotCost = 0m;

        /// <summary>
        /// ������
        /// </summary>
        private decimal derateCost = 0m;
   
        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject derateType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ô������
        /// </summary>
        private string feeStatCate = string.Empty;

        /// <summary>
        /// ���ô�������
        /// </summary>
        private string feeStatName = string.Empty;

        /// <summary>
        /// ��С���ô���
        /// </summary>
        private string feeCode = string.Empty;

        /// <summary>
        /// ��С��������
        /// </summary>
        private string feeName = string.Empty;


        /// ��Ŀ����
        /// </summary>
        private string itemCode = string.Empty;

        /// ��Ŀ����
        /// </summary>
        private string itemName = string.Empty;

        /// <summary>
        /// ��׼��
        /// </summary>
        private FS.FrameWork.Models.NeuObject confirmOperator = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ч��� false 0 ��Ч
        ///			 true 1 ��Ч
        /// </summary>
        private bool bIsInValid = false;

        /// <summary>
        /// �����������
        /// </summary>
        private OperEnvironment derateOper = new OperEnvironment();


        /// <summary>
        /// ���ϼ����������
        /// </summary>
        private OperEnvironment cancelDerateOper = new OperEnvironment();


        /// <summary>
        /// �������
        /// </summary>
        private int happenNO = 0 ;

        /// <summary>
        /// �������� 0 �ܶ� 1 ��С���� 2 ��Ŀ���� 3
        /// </summary>
        private FS.FrameWork.Models.NeuObject derateKind = new NeuObject();

        /// <summary>
        /// ������
        /// </summary>
        private string recipeNO = string.Empty;

        /// <summary>
        /// �����ڲ���ˮ��
        /// </summary>
        private int sequenceNO = 0;

        /// <summary>
        /// ����ԭ��
        /// </summary>
        private string derateCause = string.Empty;

        /// <summary>
        /// ����״̬
        /// </summary>
        private string balanceState = string.Empty;

        /// <summary>
        /// �������
        /// </summary>
        private int balanceNO ;

        /// <summary>
        /// �վݺ�
        /// </summary>
        private string invoiceNO;

        #endregion

        #region ����

        /// <summary>
        /// סԺ��
        /// </summary>
        public string InpatientNO
        {
            get
            { 
                return inpatientNO; 
            }
            set
            { 
                inpatientNO = value;
            }
        }

        /// <summary>
        /// ��С���ý��
        /// </summary>
        public decimal FeeTotCost
        {
            get 
            { 
                return feeTotCost;
            }
            set
            { 
                feeTotCost = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public decimal DerateCost
        {
            get
            { 
                return derateCost;
            }
            set 
            {
                derateCost = value; 
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject DerateType
        {
            get 
            { 
                return derateType;
            }
            set 
            { 
                derateType = value;
            }
        }

        /// <summary>
        /// ���ô������
        /// </summary>
        public string FeeStatCate
        {
            get 
            { 
                return feeStatCate; 
            }
            set 
            { 
                feeStatCate = value; 
            }
        }
        
        /// <summary>
        /// ͳ�ƴ�������
        /// </summary>
        public string FeeStatName
        {
            get
            {
                return feeStatName;
            }
            set
            {
                feeStatName = value;
            }
        }

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
        /// ��С��������
        /// </summary>
        public string FeeName
        {
            get { return feeName; }
            set { feeName = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set 
            { 
                itemCode = value; 
            }
        }

        /// <summary>
        /// ��׼��
        /// </summary>
        public FS.FrameWork.Models.NeuObject ConfirmOperator
        {
            get 
            { 
                return confirmOperator; 
            }
            set 
            { 
                confirmOperator = value; 
            }
        }

        /// <summary>
        /// ��Ч��� false 0 ��Ч
        ///			 true 1 ��Ч
        /// </summary>
        public bool IsValid
        {
            get
            {
                // TODO:  ��� DerateFee.IsInValid getter ʵ��
                return bIsInValid;
            }
            set
            {
                // TODO:  ��� DerateFee.IsInValid setter ʵ��
                bIsInValid = value;
            }
        }

        /// <summary>
        /// �����������
        /// </summary>
        public OperEnvironment DerateOper
        {
            get 
            { 
                return derateOper; 
            }
            set 
            {
                derateOper = value;
            }
        }


        /// <summary>
        /// ���ϼ����������
        /// </summary>
        public OperEnvironment CancelDerateOper
        {
            get { return cancelDerateOper; }
            set { cancelDerateOper = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int HappenNO
        {
            set
            {
                this.happenNO = value;
            }
            get
            {
                return this.happenNO;
            }
        }

        /// <summary>
        /// �������� 0 �ܶ� 1 ��С���� 2 ��Ŀ����
        /// </summary>
        public NeuObject DerateKind
        {
            set
            {
                this.derateKind = value;
            }
            get
            {
                return this.derateKind;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string RecipeNO
        {
            set
            {
                this.recipeNO = value ;
            }
            get
            {
                return this.recipeNO;
            }
        }

        /// <summary>
        /// �����ڲ���ˮ��
        /// </summary>
        public int SequenceNO
        {
            set
            {
                this.sequenceNO = value;
            }
            get
            {
                return this.sequenceNO;
            }
        }

        /// <summary>
        /// ����ԭ��
        /// </summary>
        public string DerateCause
        {
            set
            {
                this.derateCause = value;
            }
            get
            {
                return this.derateCause;
            }
        }

         /// <summary>
        /// ����״̬
        /// </summary>
        public string BalanceState
        {
            set
            {
                this.balanceState = value;
            }
            get
            {
                return this.balanceState;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int BalanceNO
        {
            set
            {
                this.balanceNO = value;
            }
            get
            {
                return this.balanceNO;
            }
        }

        /// <summary>
        /// �վݺ�
        /// </summary>
        public string InvoiceNO
        {
            set
            {
                this.invoiceNO = value;
            }
            get
            {
                return this.invoiceNO;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemName
        {
            set
            {
                this.itemName = value;
            }
            get
            {
                return this.itemName;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DerateFee Clone()
        {
            DerateFee derateFee = base.Clone() as DerateFee;
            
            derateFee.DerateOper = this.DerateOper.Clone();

            derateFee.cancelDerateOper = this.cancelDerateOper.Clone();
            derateFee.DerateKind = this.DerateKind.Clone();

            return derateFee;
        }

        #endregion
    }
}
