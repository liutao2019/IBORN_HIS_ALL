using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// <�޸ļ�¼>
    ///    1.��ͬ��λ���Ϊ�������� by Sunjh 2010-9-14 {8FE2CA47-D536-4dde-B892-44276F89593B} 
    ///    2.���������޸ģ�ԭ�����������ף� by Sunjh 2010-9-14 {40F7B017-7364-427f-87BD-05285958B364}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucRecipeLabelFY : FS.FrameWork.WinForms.Controls.ucBaseControl,
        FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint, FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint
    {
        public ucRecipeLabelFY()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
        }

        private bool isPrint;

        protected bool Isprint
        {
            get
            {
                return this.isPrint;
            }
            set
            {
                this.isPrint = value;
            }
        }

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper frequencyHelper = null;

        /// <summary>
        /// �÷�������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper usageHelper = null;

        /// <summary>
        /// ��ӡ
        /// </summary>
        FS.FrameWork.WinForms.Classes.Print p = null;

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void Init()
        {
            //�������Ƶ����Ϣ 
            //if (this.frequencyHelper == null)
            //{
            //    FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
            //    ArrayList alFrequency = frequencyManagement.GetAll("Root");
            //    if (alFrequency != null)
            //        this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alFrequency);
            //}
            ////��ȡ�����÷�
            //if (this.usageHelper == null)
            //{
            //    FS.HISFC.BizLogic.Manager.Constant c = new FS.HISFC.BizLogic.Manager.Constant();
            //    ArrayList alUsage = c.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            //    if (alUsage == null)
            //    {
            //        MessageBox.Show("��ȡ�÷��б����!");
            //        return;
            //    }
            //    ArrayList tempAl = new ArrayList();
            //    foreach (FS.FrameWork.Models.NeuObject info in alUsage)
            //    {
            //        tempAl.Add(info);
            //    }

            //    this.usageHelper = new FS.FrameWork.Public.ObjectHelper(tempAl);
            //}
        }

        /// <summary>
        /// ����ҩƷ������Ϣ
        /// </summary>
        private void GetRecipeLabelItem(string drugDept, string drugCode, ref FS.HISFC.Models.Pharmacy.Item item)
        {
            FS.FrameWork.Management.DataBaseManger dataBaseMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSql = @"select t.trade_name,t.regular_name,t.formal_name,t.other_name,
       t.english_regular,t.english_other,t.english_name,t.caution,t.store_condition,t.specs,s.place_code,
	   t.custom_code
from   pha_com_baseinfo t,pha_com_stockinfo s
where  t.drug_code = s.drug_code
and    s.drug_dept_code = '{0}'
and    s.drug_code = '{1}'";
            strSql = string.Format(strSql, drugDept, drugCode);
            if (dataBaseMgr.ExecQuery(strSql) != -1)
            {
                if (dataBaseMgr.Reader.Read())
                {
                    item.Name = dataBaseMgr.Reader[0].ToString();
                    item.NameCollection.RegularName = dataBaseMgr.Reader[1].ToString();
                    //item.NameCollection.FormalName = dataBaseMgr.Reader[2].ToString();
                    //item.NameCollection.OtherName = dataBaseMgr.Reader[3].ToString();
                    //item.NameCollection.EnglishRegularName = dataBaseMgr.Reader[4].ToString();
                    //item.NameCollection.EnglishOtherName = dataBaseMgr.Reader[5].ToString();
                    //item.NameCollection.EnglishName = dataBaseMgr.Reader[6].ToString();
                    //item.Product.Caution = dataBaseMgr.Reader[7].ToString();
                    //item.Product.StoreCondition = dataBaseMgr.Reader[8].ToString();
                    item.Specs = dataBaseMgr.Reader[9].ToString();
                    item.User01 = dataBaseMgr.Reader[10].ToString();
                    //item.NameCollection.UserCode = dataBaseMgr.Reader[11].ToString();
                }
            }
        }
       
        /// <summary>
        /// �����ʾ 
        /// </summary>
        protected void Clear()
        {
            this.lblAge.Text = "";
            this.lblDeptName.Text = "";
            this.lblDignoseName.Text = "";
            this.lblDoct.Text = "";
            this.lblDrug.Text = "";
            this.lblDrugDate.Text = "";
            this.lblInvoiceNo.Text = "";
            this.lblPactName.Text = "";
            this.lblPatientId.Text = "";
            this.lblPatientName.Text = "";
            this.lblSendWindow.Text = "";
            this.lblSex.Text = "";
            this.lblTotCost.Text = "";
            this.TotCost =0;
            this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.Rows.Count);
        }


        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        protected void SetPatiAndSendInfo(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal labelNum)
        {
            //��������
           // this.lbBarCode.Text = "*" + applyOut.RecipeNO + "*";
            //���û�����Ϣ����ҩ��Ϣ
            if (this.patientInfo != null)
            {
                //����
                this.lblPatientName.Text = this.patientInfo.Name;
                this.lblPatientId.Text = this.patientInfo.ID;
                this.lblInvoiceNo.Text = this.patientInfo.InvoiceNO;//��Ʊ��
                this.lblPactName.Text = this.patientInfo.Pact.Name;
                this.lblSex.Text = this.patientInfo.Sex.Name;
                this.lblAge.Text = this.patientInfo.Age;
                this.lblDeptName.Text = this.patientInfo.DoctorInfo.Templet.Dept.Name;
                this.lblDignoseName.Text = this.patientInfo.ClinicDiagnose;
                this.lblDrug.Text = "��ҩ��";
                this.lblDoct.Text = this.patientInfo.DoctorInfo.Templet.Doct.Name;
                this.lblSendWindow.Text = applyOut.SendWindow;
         
            }
        }

        /// <summary>
        /// ���û�����ϢB
        /// </summary>
        protected void SetPatiAndSendInfo(FS.HISFC.Models.Fee.Outpatient.FeeItemList iteminfo)
        {
            //��������
            // this.lbBarCode.Text = "*" + applyOut.RecipeNO + "*";
            //���û�����Ϣ����ҩ��Ϣ
            if (this.patientInfo != null)
            {
                //����
                this.lblPatientName.Text = this.patientInfo.Name;
                this.lblPatientId.Text = this.patientInfo.ID;
                this.lblInvoiceNo.Text = this.patientInfo.InvoiceNO;//��Ʊ��
                this.lblPactName.Text = this.patientInfo.Pact.Name;
                this.lblSex.Text = this.patientInfo.Sex.Name;
                this.lblAge.Text = this.patientInfo.Age;
                this.lblDeptName.Text = this.patientInfo.DoctorInfo.Templet.Dept.Name;
                this.lblDignoseName.Text = this.patientInfo.ClinicDiagnose;
                this.lblDrug.Text = "";
                this.lblDoct.Text = this.patientInfo.DoctorInfo.Templet.Doct.Name;
                this.lblSendWindow.Text = "��ҩ����";

            }
        }
        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        protected void SetPatiAndSendInfo(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.SetPatiAndSendInfo(applyOut, this.labelNum);
        }

        /// <summary>
        /// ת��������λ����С��λ��ʾ
        /// </summary>
        /// <param name="doseOnce">ÿ�μ���</param>
        /// <param name="baseDose">��������</param>
        /// <returns>������Ӧ�ı�ʾ�ַ��� �Դ���1�İ�С����ʾ С��1�İ�������ʽ��ʾ</returns>
        protected string DoseToMin(decimal doseOnce, decimal baseDose)
        {
            if (baseDose == 0)
                baseDose = 1;
            decimal result = doseOnce / baseDose;
            if (result >= 1)
                return System.Math.Round(result, 2).ToString();
            else  //���㹫Լ�� ��ʾΪ������ʽ
            {
                result = this.MaxCD(doseOnce, baseDose);
                return (doseOnce / result).ToString() + "/" + (baseDose / result).ToString();
            }
        }

        public decimal MaxCD(decimal i, decimal j)
        {
            decimal a, b, temp;
            if (i > j)
            {
                a = i;
                b = j;
            }
            else
            {
                b = i;
                a = j;
            }
            temp = a % b;
            while (temp != 0)
            {
                a = b;
                b = temp;
                temp = a % b;
            }
            return b;
        }


        /// <summary>
        /// ����ҳ���ݸ�ֵ
        /// </summary>
        /// <param name="applyOut">��ҩ��������</param>
        public void SetPatiTotData()
        {
            //this.Clear();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();
            this.lblDrugDate.Text =  this.patientInfo.User01;

        }


        #region IRecipeLabel ��Ա
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register patientInfo = null;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                // TODO:  ��� ucComboRecipeLabel.PatientInfo getter ʵ��
                return this.patientInfo;
            }
            set
            {
                // TODO:  ��� ucComboRecipeLabel.PatientInfo setter ʵ��
                this.patientInfo = value;

                this.SetPatiTotData();
                //this.Print();
            }
        }


        protected decimal drugTotNum;
        /// <summary>
        /// һ�δ�ӡҩƷ������ҳ��
        /// </summary>
        public decimal DrugTotNum
        {
            set
            {
                this.drugTotNum = value;
                this.labelNum = 1;
            }
        }
        /// <summary>
        /// ���δ�����ӡҳ��
        /// </summary>
        protected decimal labelNum;
        /// <summary>
        /// ���δ������
        /// </summary>
        protected decimal TotCost;

        /// <summary>
        /// ���δ�����ӡ��ҳ��
        /// </summary>
        public decimal LabelTotNum
        {
            set
            {
                //this.labelTotNum = value;
                this.labelNum = 1;
            }
        }
        /// <summary>
        /// ��ӡ����ҩƷ
        /// </summary>
        /// <param name="applyOut"></param>
        public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            //this.Clear();
            FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
            this.GetRecipeLabelItem(applyOut.StockDept.ID, applyOut.Item.ID, ref item);
            //���û�����Ϣ��ʾ����ҩ��Ϣ
            this.SetPatiAndSendInfo(applyOut);
            //�˸�ҩ��־
            this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.Rows.Count, 2);
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].Font = new System.Drawing.Font("����", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 2].Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 0].Text = item.NameCollection.RegularName;
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Text = "     [" + item.Name + "]";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 1].Text = item.Specs;
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Text = applyOut.Usage.Name.ToString();
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 2].Text = applyOut.Operation.ApplyQty.ToString() + applyOut.Item.MinUnit;
            if (applyOut.PlaceNO != null)
            {
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 3].Text = applyOut.PlaceNO.ToString();
            }
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 4].Text = applyOut.Item.Price.ToString();
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 5].Text = "80%";
            TotCost += applyOut.Operation.ApplyQty * applyOut.Item.Price;
            this.lblTotCost.Text = TotCost.ToString();
        }

        /// <summary>
        /// ��ӡһ��ҩƷ
        /// </summary>
        /// <param name="alCombo"></param>
        public void AddCombo(ArrayList alCombo)
        {
            this.fpSpread1_Sheet1.Rows.Count = 0;
            for (int i = 0; i < alCombo.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alCombo[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (i == 0)
                {
                    this.SetPatiAndSendInfo(applyOut);
                }
                this.AddSingle(applyOut);
                if (i < alCombo.Count - 1)
                    this.Print();
            }
        }



        public void AddBackFeeItem(int iIndex, FS.HISFC.Models.Fee.Outpatient.FeeItemList infoItem)
        {
            //this.Clear();
            FS.HISFC.BizLogic.Pharmacy.Item con = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.Models.Pharmacy.Item item =con.GetItem(infoItem.Item.ID);
            this.GetRecipeLabelItem(infoItem.FeeOper.Dept.ID, infoItem.Item.ID, ref item);
            //���û�����Ϣ��ʾ����ҩ��Ϣ
            this.SetPatiAndSendInfo(infoItem);
            if (infoItem.Item.Qty < 0)
            {
                this.neuLabel1.Text = "�����и��ױ���Ժ��ҩ�嵥";
            }
            //�˸�ҩ��־
            this.fpSpread1_Sheet1.AddRows(iIndex * 2, 2);
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].Font = new System.Drawing.Font("����", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 0].Text = item.NameCollection.RegularName;
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Text = "     [" + item.Name+"]";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 1].Text = item.Specs;
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Text = " ";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 2].Text = infoItem.Item.Qty + infoItem.Item.PriceUnit;

            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 3].Text = " ";

            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 4].Text = infoItem.Item.Price.ToString();
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 2, 5].Text = "80%";
            TotCost += infoItem.Item.Qty * infoItem.Item.Price;
            this.lblTotCost.Text = TotCost.ToString();
            
        }

        /// <summary>
        /// ��ӡȫ��ҩƷ ��ҩ�嵥
        /// </summary>
        /// <param name="al"></param>
        public void AddAllData(ArrayList al)
        {
            // TODO:  ��� ucComboRecipeLabel.AddAllData ʵ��
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        public void Print()
        {

            // TODO:  ��� ucComboRecipeLabel.Print ʵ��
            if (p == null)
            {
                p = new FS.FrameWork.WinForms.Classes.Print();

                //FS.HISFC.Components.Common.Classes.Function.GetPageSize("RecipeLabel", ref p);//�������ã���Ϣ��ģ��

                FS.HISFC.Components.Common.Classes.Function.GetPageSize("MZPY", ref p);

                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            }
            System.Windows.Forms.Control c = this;
     
            p.PrintPage(5, 1, c);
           // p.PrintPreview(5, 1, c);

            this.Clear();
        }

        #endregion


        private enum RowSet
        {
            /// <summary>
            /// ��ҩ������ʼ���� 1
            /// </summary>
            RowDrugBegin = 1,
            /// <summary>
            /// ע������ 4
            /// </summary>
            RowCaution = 4,
            /// <summary>
            /// Ƶ�� 5
            /// </summary>
            RowFreUse = 5,
            /// <summary>
            /// ������Ϣ 0
            /// </summary>
            RowPatiInfo = 0,
            /// <summary>
            /// ��ҩ��Ϣ 0
            /// </summary>
            RowSendInfo = 0,
            /// <summary>
            /// ҽԺ����
            /// </summary>
            RowHosName = 6
        }


        #region IDrugPrint ��Ա

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public FS.HISFC.Models.Registration.Register OutpatientInfo
        {
            get
            {
                // TODO:  ��� ucComboRecipeLabel.PatientInfo getter ʵ��
                return this.patientInfo;
            }
            set
            {
                // TODO:  ��� ucComboRecipeLabel.PatientInfo setter ʵ��
                this.patientInfo = value;

                this.SetPatiTotData();              
            }
        }

        public void Preview()
        {
            if (p == null)
            {
                p = new FS.FrameWork.WinForms.Classes.Print();

                FS.HISFC.Components.Common.Classes.Function.GetPageSize("RecipeLabel", ref p);
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            }
            System.Windows.Forms.Control c = this;
            c.Width = this.Width;
            c.Height = this.Height;
            //			p.PrintPreview(12,1,c);
            p.PrintPreview(12, 1, c);

            this.Clear();
        }

        #endregion
        #region IBackFeeRecipePrint ��Ա

        public int SetData(ArrayList alBackFee)
        {
            //this.Clear();
            this.isPrint = true;
            int i = 0;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alBackFee)
            {
                this.AddBackFeeItem(i, f);
                i++;
            }

            return 1;
        }

        #endregion

        #region IBackFeeRecipePrint ��Ա

        public FS.HISFC.Models.Registration.Register Patient
        {
            set {

                if (value != null)
                {
                    this.patientInfo = value;
                }
            }
        }

        #endregion
    }
}
