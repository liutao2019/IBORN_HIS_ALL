using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.Report.InpatientFee.InpatientChargePrint
{
    public partial class ucOrderExeQueryPrint : Neusoft.NFC.Interface.Controls.ucBaseControl,Neusoft.UFC.Order.Classes.IOrderExeQuery
    {
        public ucOrderExeQueryPrint()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ����ʵ��
        /// </summary>
        private Neusoft.HISFC.Object.RADT.PatientInfo myPatientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();

        /// <summary>
        /// ִ�е�
        /// </summary>
        private Neusoft.HISFC.Object.Order.ExecOrder myExeOrder = new Neusoft.HISFC.Object.Order.ExecOrder();

        
        private InpatientFee.Class.LocalInpatientManager inPatientManager = new Neusoft.Report.InpatientFee.Class.LocalInpatientManager ();

        Neusoft.HISFC.Object.Fee.Inpatient.FeeItemList myFeeItemList = new Neusoft.HISFC.Object.Fee.Inpatient.FeeItemList();

        #region IOrderExeQuery ��Ա

        public void Print()
        {
            Neusoft.NFC.Interface.Classes.Print p = new Neusoft.NFC.Interface.Classes.Print();
            Neusoft.HISFC.Object.Base.PageSize pz = new Neusoft.HISFC.Object.Base.PageSize("JZDDY", 460, 380);
            p.SetPageSize(pz);
            p.PrintPage(26, 0, this.neuPanel3);
        }

        public int SetValue(ArrayList alExeOrder)
        {
            if (alExeOrder == null || alExeOrder.Count == 0) return 0;
            this.nlbl_����.Text = "��������: " + this.myPatientInfo.Name;
            this.nlbl_סԺ��.Text = "סԺ��: " + this.myPatientInfo.PID.PatientNO;
            this.nlbl_����.Text = "����: " + this.myPatientInfo.PVisit.PatientLocation.NurseCell.Name;
            decimal decQty = 0;
            int rows = 0;
            string strȡҩҩ�� = ".";
            Hashtable htDrugCode = new Hashtable();
            this.neuSpread1_Sheet1.Rows.Count = 0;

            for (int i = 0; i < alExeOrder.Count; i++)
            {
                this.myExeOrder = alExeOrder[i] as Neusoft.HISFC.Object.Order.ExecOrder;
                if (!htDrugCode.ContainsKey(this.myExeOrder.Order.Item.ID))
                {
                    htDrugCode.Add(this.myExeOrder.Order.Item.ID, this.myExeOrder);
                }
                strȡҩҩ�� = this.myExeOrder.Order.Item.Memo;
            }
            foreach(DictionaryEntry oneItem in htDrugCode)
            {
                decQty = 0;
                for (int i = 0; i < alExeOrder.Count; i++)
                {
                    this.myExeOrder = alExeOrder[i] as Neusoft.HISFC.Object.Order.ExecOrder;
                    if (this.myExeOrder.Order.Item.ID == oneItem.Key.ToString())
                    {
                        decQty += this.myExeOrder.Order.Qty;
                    }
                }
                this.neuSpread1_Sheet1.AddRows(rows, 1);
                Neusoft.HISFC.Object.Order.ExecOrder oneExeOrderItemObj = oneItem.Value as Neusoft.HISFC.Object.Order.ExecOrder;
                this.neuSpread1_Sheet1.Cells[rows, 0].Text = oneExeOrderItemObj.Order.Item.Name;
                this.neuSpread1_Sheet1.Cells[rows, 1].Text = oneExeOrderItemObj.Order.Item.Specs;
                this.neuSpread1_Sheet1.Cells[rows, 2].Text = decQty.ToString();
                this.neuSpread1_Sheet1.Cells[rows, 3].Text = oneExeOrderItemObj.Order.Unit;
                rows++;
            }
            this.nlblҩ��.Text = "ȡҩҩ���� " + strȡҩҩ��;
            this.neuSpread1_Sheet1.AddRows(rows, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(rows, 0, 1, 4);
            this.neuSpread1_Sheet1.Cells[rows, 0].Text = "��ӡ�ˣ� " + inPatientManager.Operator.Name + " ��ӡʱ��:" + inPatientManager.GetSysDateTime("yyyy-mm-dd hh24:mi:ss");
            return 1;
        }

        public Neusoft.HISFC.Object.RADT.PatientInfo patientInfoObj
        {
            get
            {
                return this.myPatientInfo;
            }
            set
            {
                this.myPatientInfo = value;
            }
        }

        #endregion

        public int SetFeeValue(ArrayList alFeeItem)
        {
            if (alFeeItem == null || alFeeItem.Count == 0) return 0;
            this.nlbl_����.Text = "��������: " + this.myPatientInfo.Name;
            this.nlbl_סԺ��.Text = "סԺ��: " + this.myPatientInfo.PID.PatientNO;
            this.nlbl_����.Text = "����: " + this.myPatientInfo.PVisit.PatientLocation.NurseCell.Name;
            decimal decQty = 0;
            int rows = 0;
            string strȡҩҩ�� = ".";
            decimal str�ܽ�� = 0;
            Hashtable htDrugCode = new Hashtable();
            this.neuSpread1_Sheet1.Rows.Count = 0;

            for (int i = 0; i < alFeeItem.Count; i++)
            {               
                this.myFeeItemList = alFeeItem[i] as Neusoft.HISFC.Object.Fee.Inpatient.FeeItemList;

                if (!htDrugCode.ContainsKey(this.myFeeItemList.Item.ID))
                {
                    htDrugCode.Add(this.myFeeItemList.Item.ID, this.myFeeItemList);
                }
                strȡҩҩ�� = this.myFeeItemList.Item.Memo;
            }
            foreach (DictionaryEntry oneItem in htDrugCode)
            {
                decQty = 0;
                for (int i = 0; i < alFeeItem.Count; i++)
                {
                    this.myFeeItemList = alFeeItem[i] as Neusoft.HISFC.Object.Fee.Inpatient.FeeItemList;
                    if (this.myFeeItemList.Item.ID == oneItem.Key.ToString())
                    {
                        decQty += this.myFeeItemList.Item.Qty;
                    }
                }
                this.neuSpread1_Sheet1.AddRows(rows, 1);
                Neusoft.HISFC.Object.Fee.Inpatient.FeeItemList oneFeeItemObj = oneItem.Value as Neusoft.HISFC.Object.Fee.Inpatient.FeeItemList;
                this.neuSpread1_Sheet1.Cells[rows, 0].Text = oneFeeItemObj.Item.Name;
                this.neuSpread1_Sheet1.Cells[rows, 1].Text = oneFeeItemObj.Item.Specs;
                this.neuSpread1_Sheet1.Cells[rows, 2].Text = decQty.ToString();
                this.neuSpread1_Sheet1.Cells[rows, 3].Text = oneFeeItemObj.Item.PriceUnit;
                str�ܽ�� += oneFeeItemObj.Item.Price / oneFeeItemObj.Item.PackQty * oneFeeItemObj.Item.Qty;
                rows++;
            }
            this.nlblҩ��.Text = "ȡҩҩ���� " + strȡҩҩ��;
            this.lbl_�ܽ��.Text = "�ܽ� " + Neusoft.NFC.Public.String.FormatNumberReturnString(str�ܽ��, 2) + "Ԫ";  
            this.neuSpread1_Sheet1.AddRows(rows, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(rows, 0, 1, 4);
            this.neuSpread1_Sheet1.Cells[rows, 0].Text = "��ӡ�ˣ� " + inPatientManager.Operator.Name + " ��ӡʱ��:" + inPatientManager.GetSysDateTime("yyyy-mm-dd hh24:mi:ss");
            return 1;
        }
    }
}
