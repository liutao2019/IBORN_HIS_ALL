using System;

namespace Neusoft.HISFC.Object.Material
{
    /// <summary>
    /// [��������: ���ʳ�����Ϣ]
    /// [�� �� ��: ������]
    /// [����ʱ��: 2007-03]
    /// 
    /// ID ���ⵥ��ˮ��
    /// </summary>
    public class Output : Neusoft.NFC.Object.NeuObject
    {
        public Output()
        {
            this.storeBase.Class2Type = "0520";
        }


        #region ����

        /// <summary>
        /// �Զ��嵥�ݺ� Ĭ��������+��ˮ��
        /// </summary>
        private string outListNO;

        /// <summary>
        /// ������
        /// </summary>
        private decimal outCost;

        /// <summary>
        /// ���ӷ�
        /// </summary>
        private decimal otherFee;

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        private bool isPrivate;

        /// <summary>
        /// ��ȡ��  ֻ��¼��ȡ����Ա
        /// </summary>
        private Neusoft.NFC.Object.NeuObject drawOper = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ����������(סԺ��ˮ�Ż�������ˮ��)
        /// </summary>
        private Neusoft.NFC.Object.NeuObject getPerson = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime outTime;

        /// <summary>
        /// ����(��Ʊ)״̬  0  �з�Ʊ     1  �޷�Ʊ    2  ��Ʊ��¼
        /// </summary>
        private string billState = "1";

        /// <summary>
        /// �跽��Ŀ
        /// </summary>
        private string debit = "";

        /// <summary>
        /// �����ˮ��
        /// </summary>
        private string inNO = "";

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime produceTime;

        /// <summary>
        /// ��;
        /// </summary>
        private string use = "";

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Material.StoreBase storeBase = new StoreBase();

        /// <summary>
        /// ����������ˮ��
        /// </summary>
        private string outSequence = "";

        /// <summary>
        /// ���뵥��(liuxq add)
        /// </summary>
        private string applylistcode = "";

        /// <summary>
        /// ���뵥�����(liuxq add)
        /// </summary>
        private int applyserialno;

        private string recipeNO;

        private int sequenceNO;

        /// <summary>
        /// �����˿�����
        /// </summary>
        private decimal returnApplyNum;

        #endregion

        #region ����

        /// <summary>
        /// �Զ��嵥�ݺ� Ĭ��������+��ˮ��
        /// </summary>
        public string OutListNO
        {
            get
            {
                return this.outListNO;
            }
            set
            {
                this.outListNO = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
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
        /// ���ӷ�
        /// </summary>
        public decimal OtherFee
        {
            get
            {
                return this.otherFee;
            }
            set
            {
                this.otherFee = value;
            }
        }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsPrivate
        {
            get
            {
                return this.isPrivate;
            }
            set
            {
                this.isPrivate = value;
            }
        }

        /// <summary>
        /// ��ȡ��  ֻ��¼��ȡ����Ա
        /// </summary>
        public Neusoft.NFC.Object.NeuObject DrawOper
        {
            get
            {
                return this.drawOper;
            }
            set
            {
                this.drawOper = value;
            }
        }

        /// <summary>
        /// ����������(סԺ��ˮ�Ż�������ˮ��)
        /// </summary>
        public Neusoft.NFC.Object.NeuObject GetPerson
        {
            get
            {
                return this.getPerson;
            }
            set
            {
                this.getPerson = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime OutTime
        {
            get
            {
                return this.outTime;
            }
            set
            {
                this.outTime = value;
            }
        }

        /// <summary>
        /// ����(��Ʊ)״̬  0  �з�Ʊ     1  �޷�Ʊ    2  ��Ʊ��¼
        /// </summary>
        public string BillState
        {
            get
            {
                return this.billState;
            }
            set
            {
                this.billState = value;
            }
        }

        /// <summary>
        /// �跽��Ŀ
        /// </summary>
        public string Debit
        {
            get
            {
                return this.debit;
            }
            set
            {
                this.debit = value;
            }
        }

        /// <summary>
        /// ��ⵥ��ˮ��
        /// </summary>
        public string InNO
        {
            get
            {
                return this.inNO;
            }
            set
            {
                this.inNO = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime ProduceTime
        {
            get
            {
                return this.produceTime;
            }
            set
            {
                this.produceTime = value;
            }
        }

        /// <summary>
        /// ��;
        /// </summary>
        public string Use
        {
            get
            {
                return this.use;
            }
            set
            {
                this.use = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Material.StoreBase StoreBase
        {
            get
            {
                return this.storeBase;
            }
            set
            {
                this.storeBase = value;
            }
        }

        /// <summary>
        /// ����������ˮ��
        /// </summary>
        public string OutSequence
        {
            get
            {
                return this.outSequence;
            }
            set
            {
                this.outSequence = value;
            }
        }

        /// <summary>
        /// ���뵥��(liuxq)
        /// </summary>
        public string ApplyListCode
        {
            get
            {
                return this.applylistcode;
            }
            set
            {
                this.applylistcode = value;
            }
        }

        /// <summary>
        /// ���뵥�����(liuxq)
        /// </summary>
        public int ApplySerialNO
        {
            get
            {
                return this.applyserialno;
            }
            set
            {
                this.applyserialno = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string RecipeNO
        {
            get
            {
                return this.recipeNO;
            }
            set
            {
                this.recipeNO = value;
            }
        }

        /// <summary>
        /// ��������Ŀ���
        /// </summary>
        public int SequenceNO
        {
            get
            {
                return this.sequenceNO;
            }
            set
            {
                this.sequenceNO = value;
            }
        }

        /// <summary>
        /// �����˿�����
        /// </summary>
        public decimal ReturnApplyNum
        {
            get
            {
                return returnApplyNum;
            }
            set
            {
                returnApplyNum = value;
            }
        }

        #endregion

        #region ����

        public new Output Clone()
        {
            Output output = base.Clone() as Output;

            output.drawOper = this.drawOper.Clone();

            output.getPerson = this.getPerson.Clone();

            output.storeBase = this.storeBase.Clone();

            return output;
        }


        #endregion
    }
}
