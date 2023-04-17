using Neusoft.FrameWork.Models;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.Models.RADT;
using System;
using System.Collections.Generic;

namespace Neusoft.WinForms.Report.InpatientFee.Class
{
    public class DayReport : Neusoft.FrameWork.Models.NeuObject
    {
        #region "����"

        /// <summary>
        /// ͳ�����
        /// </summary>
        private string statNO = "";

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endDate = DateTime.MinValue;
        /// <summary>
        /// ����Ա������Ϣ
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();
        /// <summary>
        /// Ԥ������
        /// </summary>
        private decimal prepayCost = 0m;
        /// <summary>
        /// �跽֧Ʊ���
        /// </summary>
        private decimal debitCheckCost = 0m;

        /// <summary>
        /// �跽���п����
        /// </summary>
        private decimal debitBankCost = 0m;
        /// <summary>
        /// ����Ԥ������
        /// </summary>
        private decimal balancePrepayCost = 0m;
        /// <summary>
        /// ����֧Ʊ���
        /// </summary>
        private decimal lenderCheckCost = 0m;
        /// <summary>
        /// �������п����
        /// </summary>
        private decimal lenderBankCost = 0m;
        /// <summary>
        /// ���Ѽ��ʽ��
        /// </summary>
        private decimal busaryPubCost = 0m;
        /// <summary>
        /// ��ҽ���ʻ�֧�����
        /// </summary>
        private decimal cityMedicarePayCost = 0m;
        /// <summary>
        /// ��ҽ��ͳ��֧�����
        /// </summary>
        private decimal cityMedicarePubCost = 0m;
        /// <summary>
        /// ʡҽ���ʻ�֧�����
        /// </summary>
        private decimal provinceMedicarePayCost = 0m;
        /// <summary>
        /// ʡҽ��ͳ��֧�����
        /// </summary>
        private decimal provinceMedicarePubCost = 0m;
        /// <summary>
        /// ����ֽ��Ͻɽ�
        /// </summary>
        private decimal turnInCash = 0m;
        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        private int prepayInvCount = 0;
        /// <summary>
        /// ���㷢Ʊ��Ʊ����
        /// </summary>
        private int balanceInvCount = 0;
        /// <summary>
        /// ����Ԥ����Ʊ����
        /// </summary>
        private int prepayWasteInvCount = 0;
        /// <summary>
        /// ���Ͻ��㷢Ʊ����
        /// </summary>
        private int balanceWasteInvCount = 0;
        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        private string prepayInvZone = "";
        /// <summary>
        /// ���㷢Ʊ����
        /// </summary>
        private string balanceInvZone = "";
        /// <summary>
        /// Ԥ��������Ʊ��
        /// </summary>
        private string prepayWasteInvNO = "";
        /// <summary>
        /// ��������Ʊ��
        /// </summary>
        private string balanceWasteInvNO = "";
        /// <summary>
        /// �����ܽ��
        /// </summary>
        private decimal balanceCost = 0m;
        /// <summary>
        /// ���ֽ�
        /// </summary>
        private decimal debitCash = 0m;
        /// <summary>
        /// ���ֽ�
        /// </summary>
        private decimal lenderCash = 0m;
        /// <summary>
        /// �跽Ժ���ʻ�
        /// </summary>
        private decimal debitHos = 0m;
        /// <summary>
        /// ����Ժ���ʻ�
        /// </summary>
        private decimal lenderHos = 0m;
        /// <summary>
        /// ������Ԥ��
        /// </summary>
        private decimal debitOther = 0m;
        /// <summary>
        /// ������Ԥ��
        /// </summary>
        private decimal lenderOther = 0m;
        /// <summary>
        /// ҽ�Ƽ���
        /// </summary>
        private decimal derateCost = 0m;
        /// <summary>
        /// �д��
        /// </summary>
        private decimal cityMedicareOverCost = 0m;
        /// <summary>
        /// ʡ�����
        /// </summary>
        private decimal provinceMedicareOverCost = 0m;
        /// <summary>
        /// ʡ������Ա
        /// </summary>
        private decimal provinceMedicareOfficeCost = 0m;

        /// <summary>
        /// ��Ŀ��ϸ
        /// </summary>
        private List<Item> itemList = new List<Item>();
        #endregion

        #region "����"

        /// <summary>
        /// ͳ�����
        /// </summary>
        public string StatNO
        {
            get
            {
                return this.statNO;

            }
            set
            {
                statNO = value;
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
                beginDate = value;
            }
        }
        /// <summary>
        /// ����ʱ��
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
        /// ����Ա������Ϣ
        /// </summary>
        public OperEnvironment Oper
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
        /// Ԥ������
        /// </summary>
        public decimal PrepayCost
        {
            get
            {
                return this.prepayCost;
            }
            set
            {
                this.prepayCost = value;
            }
        }
        /// <summary>
        /// �跽֧Ʊ���
        /// </summary>
        public decimal DebitCheckCost
        {
            get
            {
                return this.debitCheckCost;
            }
            set
            {
                this.debitCheckCost = value;
            }
        }

        /// <summary>
        /// �跽���п����
        /// </summary>
        public decimal DebitBankCost
        {
            get
            {
                return this.debitBankCost;
            }
            set
            {
                this.debitBankCost = value;
            }
        }

        /// <summary>
        /// ����Ԥ������
        /// </summary>
        public decimal BalancePrepayCost
        {
            get
            {
                return this.balancePrepayCost;
            }
            set
            {
                this.balancePrepayCost = value;
            }
        }
        /// <summary>
        /// ����֧Ʊ���
        /// </summary>
        public decimal LenderCheckCost
        {
            get
            {
                return this.lenderCheckCost;
            }
            set
            {
                this.lenderCheckCost = value;
            }
        }
        /// <summary>
        /// �������п����
        /// </summary>
        public decimal LenderBankCost
        {
            get
            {
                return this.lenderBankCost;
            }
            set
            {
                this.lenderBankCost = value;
            }
        }
        /// <summary>
        /// ���Ѽ��ʽ��
        /// </summary>
        public decimal BursaryPubCost
        {
            get
            {
                return this.busaryPubCost;
            }
            set
            {
                this.busaryPubCost = value;
            }
        }
        /// <summary>
        /// ��ҽ���ʻ�֧�����
        /// </summary>
        public decimal CityMedicarePayCost
        {
            get
            {
                return this.cityMedicarePayCost;
            }
            set
            {
                this.cityMedicarePayCost = value;
            }
        }
        /// <summary>
        /// ��ҽ��ͳ��֧�����
        /// </summary>
        public decimal CityMedicarePubCost
        {
            get
            {
                return this.cityMedicarePubCost;
            }
            set
            {
                this.cityMedicarePubCost = value;
            }
        }

        /// <summary>
        /// ʡҽ���ʻ�֧�����
        /// </summary>
        public decimal ProvinceMedicarePayCost
        {
            get
            {
                return this.provinceMedicarePayCost;
            }
            set
            {
                this.provinceMedicarePayCost = value;
            }
        }
        /// <summary>
        /// ʡҽ��ͳ��֧�����
        /// </summary>
        public decimal ProvinceMedicarePubCost
        {
            get
            {
                return this.provinceMedicarePubCost;
            }
            set
            {
                this.provinceMedicarePubCost = value;
            }
        }

        /// <summary>
        /// ����ֽ��Ͻɽ�
        /// </summary>
        public decimal TurnInCash
        {
            get
            {
                return this.turnInCash;
            }
            set
            {
                this.turnInCash = value;
            }
        }

        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        public int PrepayInvCount
        {
            get
            {
                return this.prepayInvCount;
            }
            set
            {
                this.prepayInvCount = value;
            }
        }
        /// <summary>
        /// ���㷢Ʊ��Ʊ����
        /// </summary>
        public int BalanceInvCount
        {
            get
            {
                return this.balanceInvCount;
            }
            set
            {
                this.balanceInvCount = value;
            }
        }
        /// <summary>
        /// ����Ԥ����Ʊ����
        /// </summary>
        public int PrepayWasteInvCount
        {
            get
            {
                return this.prepayWasteInvCount;
            }
            set
            {
                this.prepayWasteInvCount = value;
            }
        }
        /// <summary>
        /// ���Ͻ��㷢Ʊ����
        /// </summary>
        public int BalanceWasteInvCount
        {
            get
            {
                return this.balanceWasteInvCount;
            }
            set
            {
                this.balanceWasteInvCount = value;
            }
        }


        /// <summary>
        /// Ԥ����Ʊ����
        /// </summary>
        public string PrepayInvZone
        {
            get
            {
                return this.prepayInvZone;
            }
            set
            {
                this.prepayInvZone = value;
            }
        }
        /// <summary>
        /// ���㷢Ʊ����
        /// </summary>
        public string BalanceInvZone
        {
            get
            {
                return this.balanceInvZone;
            }
            set
            {
                this.balanceInvZone = value;
            }
        }

        /// <summary>
        /// Ԥ��������Ʊ��
        /// </summary>
        public string PrepayWasteInvNO
        {
            get
            {
                return this.prepayWasteInvNO;
            }
            set
            {
                this.prepayWasteInvNO = value;
            }
        }
        /// <summary>
        /// ��������Ʊ��
        /// </summary>
        public string BalanceWasteInvNO
        {
            get
            {
                return this.balanceWasteInvNO;
            }
            set
            {
                this.balanceWasteInvNO = value;
            }
        }
        /// <summary>
        /// �����ܽ��
        /// </summary>
        public decimal BalanceCost
        {
            get
            {
                return this.balanceCost;
            }
            set
            {
                this.balanceCost = value;
            }
        }
        /// <summary>
        /// ��Ŀ��ϸ
        /// </summary>
        public List<Item> ItemList
        {
            get
            {
                return itemList;
            }
            set
            { 
                itemList=value;
            }
        }
        /// <summary>
        /// ���ֽ�
        /// </summary>
        public decimal DebitCash
        {
            get
            {
                return debitCash;
            }
            set
            {
                debitCash = value;
            }
        }
        /// <summary>
        /// ���ֽ�
        /// </summary>
        public decimal LenderCash
        {
            get
            {
                return lenderCash;
            }
            set
            {
                lenderCash = value;
            }
        }
        /// <summary>
        /// �跽Ժ���ʻ�
        /// </summary>
        public decimal DebitHos
        {
            get
            {
                return debitHos;
            }
            set
            {
                debitHos = value;
            }
        }
        /// <summary>
        /// ����Ժ���ʻ�
        /// </summary>
        public decimal LenderHos
        {
            get
            {
                return lenderHos;
            }
            set
            {
                lenderHos = value;
            }
        }
        /// <summary>
        /// ������Ԥ��
        /// </summary>
        public decimal DebitOther
        {
            get
            {
                return debitOther;
            }
            set
            {
                debitOther = value;
            }
        }
        /// <summary>
        /// ������Ԥ��
        /// </summary>
        public decimal LenderOther
        {
            get
            {
                return lenderOther;
            }
            set
            {
                lenderOther = value;
            }
        }
        /// <summary>
        /// ҽ�Ƽ���
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
        /// �б����
        /// </summary>
        public decimal CityMedicareOverCost
        {
            get
            {
                return cityMedicareOverCost;
            }
            set
            {
                cityMedicareOverCost = value;
            }
        }
        /// <summary>
        /// ʡ�����
        /// </summary>
        public decimal ProvinceMedicareOverCost
        {
            get
            {
                return provinceMedicareOverCost;
            }
            set
            {
                provinceMedicareOverCost = value;
            }
        }
        /// <summary>
        /// ʡ������Ա
        /// </summary>
        public decimal ProvinceMedicareOfficeCost
        {
            get
            {
                return provinceMedicareOfficeCost;
            }
            set
            {
                provinceMedicareOfficeCost = value;
            }
        }
        #endregion

        #region "����"
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DayReport Clone()
        {
            Class.DayReport dayReport = base.Clone() as DayReport;
            dayReport.Oper = this.Oper.Clone();
            foreach(Class.Item item in this.ItemList)
            {
                dayReport.ItemList.Add(item);
            }
            return dayReport;
        }

        #endregion
    }

    public class Item :Neusoft.FrameWork.Models.NeuObject
    {
        #region ����
        /// <summary>
        /// ͳ�ƴ���
        /// </summary>
        private string stateCode = string.Empty;
        /// <summary>
        /// ���ý��
        /// </summary>
        private decimal totCost = 0m;
        /// <summary>
        /// �Էѽ��
        /// </summary>
        private decimal ownCost = 0m;
        /// <summary>
        /// �Ը����
        /// </summary>
        private decimal payCost = 0m;
        /// <summary>
        /// ���ѽ��
        /// </summary>
        private decimal pubCost = 0m;
        /// <summary>
        /// ��ע
        /// </summary>
        private string mark = string.Empty;
        #endregion

        #region  ����
        /// <summary>
        /// ͳ�ƴ���
        /// </summary>
        public string StateCode
        {
            get
            {
                return stateCode;
            }
            set
            {
                stateCode = value;
            }
        }
        /// <summary>
        /// ���ý��
        /// </summary>
        public decimal TotCost
        {
            get
            {
                return totCost;
            }
            set
            {
                totCost = value;
            }
        }
        /// <summary>
        /// �Էѽ��
        /// </summary>
        public decimal OwnCost
        {
            get
            {
                return ownCost;
            }
            set
            {
                ownCost = value;
            }
        }
        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal PayCost
        {
            get
            {
                return payCost;
            }
            set
            {
                payCost = value;
            }
        }
        /// <summary>
        /// ���ѽ��
        /// </summary>
        public decimal PubCost
        {
            get
            {
                return pubCost;
            }
            set
            {
                pubCost = value;
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
        #endregion

        #region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Item Clone()
        {
            Class.Item obj = base.Clone() as Item;
            return obj;
        }
        #endregion
    }
}
