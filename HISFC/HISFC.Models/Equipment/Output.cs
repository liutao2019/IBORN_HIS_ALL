using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// [��������: ����ʵ��]
    /// [�� �� ��: Sunm]
    /// [����ʱ��: 2007-12-04]
    /// </summary>
    /// 
    [System.Serializable]
    public class Output : FS.FrameWork.Models.NeuObject
    {
        public Output()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        // ���ⵥ��ˮ��
        private string outBillNo;
        //���ⵥ���
        private int outSerialNo;
        //���ⵥ�ݺ�
        private string outListNo;
        //��Ƭ��ˮ��
        private string cardSequence;
        //��Ƭ���
        private string cardNo;
        //��������
        private string outType;
        //�������
        private string outClass;
        //����״̬
        private string outState;
        //�������
        private FS.FrameWork.Models.NeuObject outDept = new FS.FrameWork.Models.NeuObject();
        //�豸�ֵ���Ϣ
        private EquipBase equBase = new EquipBase();
        //�豸�ͺ�
        private string model;
        //��������
        private int outNum;
        //����۸�
        private decimal outPrice;
        //������
        private decimal outCost;
        //������
        private FS.FrameWork.Models.NeuObject getPerson = new FS.FrameWork.Models.NeuObject();
        //���ÿ���
        private FS.FrameWork.Models.NeuObject getDept = new FS.FrameWork.Models.NeuObject();
        //�豸��;
        private string equUse;
        //���뵥��ˮ��
        private string applyBillNo;
        //���뵥���
        private int applySerialNo;
        //�豸����
        private string barCode;
        //����Ա
        private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();
        //��������
        private DateTime outDate;
        //���յ�λ�������ã�
        private string inceptUnit;
        //��Ƭ��������
        private DateTime cardStartDate;
        //���κ�
        private string groupNo;

        #endregion

        #region ����
        /// <summary>
        /// ���ⵥ��ˮ��
        /// </summary>
        public string OutBillNo
        {
            get
            {
                return this.outBillNo;
            }
            set
            {
                this.outBillNo = value;
            }
        }
        /// <summary>
        /// ���ⵥ���
        /// </summary>
        public int OutSerialNo
        {
            get
            {
                return this.outSerialNo;
            }
            set
            {
                this.outSerialNo = value;
            }
        }
        /// <summary>
        /// ���ⵥ�ݺ�
        /// </summary>
        public string OutListNo
        {
            get
            {
                return this.outListNo;
            }
            set
            {
                this.outListNo = value;
            }
        }
        /// <summary>
        /// ��Ƭ��ˮ��
        /// </summary>
        public string CardSequence
        {
            get
            {
                return this.cardSequence;
            }
            set
            {
                this.cardSequence = value;
            }
        }
        /// <summary>
        /// ��Ƭ���
        /// </summary>
        public string CardNo
        {
            get
            {
                return this.cardNo;
            }
            set
            {
                this.cardNo = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string OutType
        {
            get
            {
                return this.outType;
            }
            set
            {
                this.outType = value;
            }
        }
        /// <summary>
        /// ������ࣨClass3MeaningCode��
        /// </summary>
        public string OutClass
        {
            get
            {
                return this.outClass;
            }
            set
            {
                this.outClass = value;
            }
        }
        /// <summary>
        /// ����״̬��0����1����2��׼��
        /// </summary>
        public string OutState
        {
            get
            {
                return this.outState;
            }
            set
            {
                this.outState = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OutDept
        {
            get
            {
                return this.outDept;
            }
            set
            {
                this.outDept = value;
            }
        }
        /// <summary>
        /// �豸�ֵ���Ϣ
        /// </summary>
        public EquipBase EquBaseInfo
        {
            get
            {
                return this.equBase;
            }
            set
            {
                this.equBase = value;
            }
        }
        /// <summary>
        /// �豸�ͺ�
        /// </summary>
        public string Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int OutNum
        {
            get
            {
                return this.outNum;
            }
            set
            {
                this.outNum = value;
            }
        }
        /// <summary>
        /// ����۸�
        /// </summary>
        public decimal OutPrice
        {
            get
            {
                return this.outPrice;
            }
            set
            {
                this.outPrice = value;
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
        /// ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject GetPerson
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
        /// ���ÿ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject GetDept
        {
            get
            {
                return this.getDept;
            }
            set
            {
                this.getDept = value;
            }
        }
        /// <summary>
        /// �豸��;
        /// </summary>
        public string EquUse
        {
            get
            {
                return this.equUse;
            }
            set
            {
                this.equUse = value;
            }
        }
        /// <summary>
        /// ���뵥��ˮ��
        /// </summary>
        public string ApplyBillNo
        {
            get
            {
                return this.applyBillNo;
            }
            set
            {
                this.applyBillNo = value;
            }
        }
        /// <summary>
        /// ���뵥���
        /// </summary>
        public int ApplySerialNo
        {
            get
            {
                return this.applySerialNo;
            }
            set
            {
                this.applySerialNo = value;
            }
        }
        /// <summary>
        /// �豸����
        /// </summary>
        public string BarCode
        {
            get
            {
                return this.barCode;
            }
            set
            {
                this.barCode = value;
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.FrameWork.Models.NeuObject Oper
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
        /// ��������
        /// </summary>
        public DateTime OutDate
        {
            get
            {
                return this.outDate;
            }
            set
            {
                this.outDate = value;
            }
        }

        /// <summary>
        /// ���յ�λ�������ã�
        /// </summary>
        public string InceptUnit
        {
            get
            {
                return inceptUnit;
            }
            set
            {
                inceptUnit = value;
            }
        }

        /// <summary>
        /// ��Ƭ��������
        /// </summary>
        public DateTime CardStartDate
        {
            get
            {
                return cardStartDate;
            }
            set
            {
                cardStartDate = value;
            }
        }

        /// <summary>
        /// ���κ�
        /// </summary>
        public string GroupNo
        {
            get
            {
                return groupNo;
            }
            set
            {
                groupNo = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Output Clone()
        {
            Output outPut = base.Clone() as Output;

            outPut.outDept = this.outDept.Clone();
            outPut.equBase = this.equBase.Clone();
            outPut.getPerson = this.getPerson.Clone();
            outPut.getDept = this.getDept.Clone();
            outPut.oper = this.oper.Clone();

            return outPut;
        }

        #endregion

    }
}
