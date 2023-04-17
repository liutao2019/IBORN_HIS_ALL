using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// ItemList<br></br>
    /// [��������: ����շ���Ŀ��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class ItemList : FS.FrameWork.Models.NeuObject
    {
        #region ˽�б��� 
        /// <summary>
        /// ��λ��ʶ 1 ҩƷ 2 ��ҩƷ 3��ϸ 4����
        /// </summary>
        private string unitFlag = string.Empty;

        /// <summary>
        /// ������� 1  ������� 2 ������� 
        /// </summary>
        private string chkFlag = string.Empty; 

        /// <summary>
        /// ������
        /// </summary>
        private string clinicNO = string.Empty; 

        /// <summary>
        /// ���￨��
        /// </summary>
        private string cardNO = string.Empty; 

        /// <summary>
        /// �Żݱ���
        /// </summary>
        private decimal ecoRate ;

        /// <summary>
        /// �Żݽ��
        /// </summary>
        private decimal realCost;

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        private string comNO = string.Empty; 
 
        /// <summary>
        /// ���к�
        /// </summary>
        private string sequenceNO = string.Empty;  

        /// <summary>
        /// ȷ����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment conformOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        /// <summary>
        /// �շ�ԱԱ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment feeOperInfo = new FS.HISFC.Models.Base.OperEnvironment(); 

        /// <summary>
        /// ִ�п���
        /// </summary>
        private FS.FrameWork.Models.NeuObject execDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ŀ
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// �Ƿ��Ѿ��ն�ȷ��
        /// </summary>
        private string isConfirm = string.Empty;
       
        /// <summary>
        /// ��������
        /// </summary>
        private decimal noBackQty;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject recipeDoc = new FS.FrameWork.Models.NeuObject(); 
 
        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject recipeDept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��Ϻ�
        /// </summary>
        private string combo = string.Empty;
        /// <summary>
        /// �շѱ�־
        /// </summary>
        private string feeFlag = string.Empty;
        private string recipeSequence = string.Empty; //���һ�η�Ʊ��Ϻ�
        private string accountFlag = string.Empty;
        #endregion

        #region ����
        /// <summary>
        /// ���˻���־ 0 û�п��˻� 1 �Ѿ����˻�
        /// </summary>
        public string AccountFlag
        {
            get
            {
                return accountFlag;
            }
            set
            {
                accountFlag = value;
            }
        }
        /// <summary>
        /// ���һ�η�Ʊ��Ϻ�
        /// </summary>
        public string RecipeSequence
        {
            get
            {
                return recipeSequence;
            }
            set
            {
                recipeSequence = value;
            }
        }
        /// <summary>
        /// �շѲ���Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment FeeOperInfo
        {
            get
            {
                return feeOperInfo;
            }
            set
            {
                feeOperInfo = value;
            }
        }
        /// <summary>
        /// �շѱ�־  0 δ�շѣ�1�����շѣ�2����
        /// </summary>
        public string FeeFlag
        {
            get
            {
                return feeFlag;
            }
            set
            {
                feeFlag = value;
            }
        }
        /// <summary>
        /// ��Ϻ�
        /// </summary>
        public string Combo
        {
            get
            {
                return combo;
            }
            set
            {
                combo = value;
            }
        }
        /// <summary>
        /// ʵ�ս��
        /// </summary>
        public decimal RealCost
        {
            get
            {
                return realCost;
            }
            set
            {
                realCost = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject RecipeDept
        {
            get
            { 
                return recipeDept;
            }
            set
            {
                recipeDept = value;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject RecipeDoc  
        {
            get
            { 
                return recipeDoc;
            }
            set
            {
                recipeDoc = value;
            }
        }

        /// <summary>
        /// ������� 1������� 2 �������
        /// </summary>
        public string ChkFlag
        {
            get
            {
                return chkFlag;
            }
            set
            {
                chkFlag = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal NoBackQty
        {
            get
            {
                return noBackQty;
            }
            set
            {
                noBackQty = value;
            }
        }

        /// <summary>
        /// ȷ�ϱ�־
        /// </summary>
        public string IsConfirm
        {
            get
            {
                return isConfirm;
            }
            set
            {
                isConfirm = value;
            }
        }

        /// <summary>
        /// ���к�
        /// </summary>
        public string SequenceNO
        {
            get
            {
                return sequenceNO;
            }
            set
            {
                sequenceNO = value;
            }
        }

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        public string ComNO
        {
            get
            {
                return comNO;
            }
            set
            {
                comNO = value;
            }
        }

        /// <summary>
        /// ��Ŀ
        /// </summary>
        public FS.HISFC.Models.Fee.Item.Undrug Item
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
        /// ִ�п���
        /// </summary>
        public FS.FrameWork.Models.NeuObject ExecDept
        {
            get
            { 
                return execDept;
            }
            set
            {
                execDept = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }

        /// <summary>
        /// �Żݱ���
        /// </summary>
        public decimal EcoRate
        {
            get
            {
                return ecoRate;
            }
            set
            {
                ecoRate = value;
            }
        }

        /// <summary>
        /// ȷ����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment ConformOper
        {
            get
            { 
                return conformOper;
            }
            set
            {
                conformOper = value;
            }
        }

        /// <summary>
        /// ���￨��
        /// </summary>
        public string CardNO
        {
            get
            {
                return cardNO;
            }
            set
            {
                cardNO = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string ClinicNO
        {
            get
            {
                return clinicNO;
            }
            set
            {
                clinicNO = value;
            }
        }

        /// <summary>
        /// ��λ��ʶ 0ҩƷ/1 ��ҩƷ/2����/3������Ŀ
        /// </summary>
        public string UnitFlag
        {
            get
            {
                return unitFlag;
            }
            set
            {
                unitFlag = value;
            }
        }
        #endregion 

        #region ��������
        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("���� �� ClinicNO ����", true)]
        public string ClinicNo
        {
            get
            {
                return clinicNO;
            }
            set
            {
                clinicNO = value;
            }
        }

        /// <summary>
        /// ���￨��
        /// </summary>
        [Obsolete("���� �� CardNO ����", true)]
        public string CardNo
        {
            get
            {
                return cardNO;
            }
            set
            {
                cardNO = value;
            }
        }

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        [Obsolete("���� �� ComNO ����", true)]
        public string ComNo
        {
            get
            {
                return comNO;
            }
            set
            {
                comNO = value;
            }
        }

        /// <summary>
        /// ���к�
        /// </summary>
        [Obsolete("���� �� SequenceNO ����", true)]
        public string SequenceNo
        {
            get
            {
                return sequenceNO;
            }
            set
            {
                sequenceNO = value;
            }
        }

        /// <summary>
        /// ȷ������
        /// </summary>
        [Obsolete("���� �� ConformOper.OperTime ����", true)]
        public System.DateTime ConformDate
        {
            //get
            //{
            //    //return conformTime;
            //}
            set
            {
                //conformTime = value;
            }
        }

        /// <summary>
        /// ȷ�ϱ�־
        /// </summary>
        [Obsolete("���� �� ISConfirm ����", true)]
        public string ConfirmFlag 
        {
            get
            {
                return isConfirm;
            }
            set
            {
                isConfirm = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("���� �� NoBackQty ����", true)]
        public decimal NoBackNum
        {
            get
            {
                return noBackQty;
            }
            set
            {
                noBackQty = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("���� �� OperInfo ��Ĳ���ʱ����� ����", true)]
        public System.DateTime OperDate
        {
            get
            {
                return operDate;
            }
            set
            {
                operDate = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        [Obsolete("���� �� OperInfo ����", true)]
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            { 
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }

        /// <summary>
        /// ��չ��־
        /// </summary>
        //[Obsolete("����", true)]
        public string ExtChar1
        {
            get
            {
                return extChar1;
            }
            set
            {
                extChar1 = value;
            }
        }

        /// <summary>
        /// �Ƽ۵�λ
        /// </summary>
        [Obsolete("���� ,��item.PriceUnit", true)]
        public string ExtChar
        {
            get
            {
                return extChar;
            }
            set
            {
                extChar = value;
            }
        }

        /// <summary>
        /// �Ż�ǰ�۸�
        /// </summary>
        [Obsolete("���� ��Item �е� Price ����", true)]
        public decimal ExtNumber1
        {
            get
            {
                return extNumber1;
            }
            set
            {
                extNumber1 = value;
            }
        }

        /// <summary>
        /// �Żݺ�۸�
        /// </summary>
        [Obsolete("���� ��RealCost����  ", true)]
        public decimal ExtNumber
        {
            get
            {
                return extNumber;
            }
            set
            {
                extNumber = value;
            }
        }

        /// <summary>
        /// ��չ��־
        /// </summary>
        [Obsolete("���� �� Combo����", true)]
        public string ExtFlag1
        {
            get
            {
                return extFlag1;
            }
            set
            {
                extFlag1 = value;
            }
        }

        /// <summary>
        /// ��չ��־
        /// </summary>
        //[Obsolete("����", true)]
        public string ExtFlag
        {
            get
            {
                return extFlag;
            }
            set
            {
                extFlag = value;
            }
        }
        #endregion 

        #region ��������

        [Obsolete("���� ��OperInfo �ڵĲ���ʱ�����", true)]
        private System.DateTime operDate;

        //[Obsolete("����", true)]
        private string extFlag; 
   
        [Obsolete("����", true)]                    
        private decimal extNumber;

        [Obsolete("����", true)]                      
        private string extChar; 

        //[Obsolete("����", true)]                  
        private string extFlag1;

        [Obsolete("����", true)]                        
        private decimal extNumber1;

        //[Obsolete("����", true)]                        
        private string extChar1;

        #endregion 

        #region ��¡����
        public new ItemList Clone()
        {
            ItemList obj = base.Clone() as ItemList;
            obj.item = this.item.Clone();
            obj.execDept = this.ExecDept.Clone();//(FS.HISFC.Models.Fee.Invoice)Invoice.Clone();
            obj.operInfo = this.OperInfo.Clone();
            obj.conformOper = this.ConformOper.Clone();
            return obj;
        }
        #endregion 
    }

}
