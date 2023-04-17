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
    public partial class ucRecipeLabel : FS.FrameWork.WinForms.Controls.ucBaseControl,
        FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        public ucRecipeLabel()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
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
            if (this.frequencyHelper == null)
            {
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetAll("Root");
                if (alFrequency != null)
                    this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alFrequency);
            }
            //��ȡ�����÷�
            if (this.usageHelper == null)
            {
                FS.HISFC.BizLogic.Manager.Constant c = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList alUsage = c.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
                if (alUsage == null)
                {
                    MessageBox.Show("��ȡ�÷��б����!");
                    return;
                }
                ArrayList tempAl = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject info in alUsage)
                {
                    tempAl.Add(info);
                }

                this.usageHelper = new FS.FrameWork.Public.ObjectHelper(tempAl);
            }
        }

        private void GetRecipeLabelItem(string drugDept, string drugCode, ref FS.HISFC.Models.Pharmacy.Item item)
        {
            FS.FrameWork.Management.DataBaseManger dataBaseMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSql = @"select t.trade_name,t.regular_name,t.formal_name,t.other_name,
       t.english_regular,t.english_other,t.english_name,t.caution,t.store_condition,t.base_dose,s.place_code,
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
                    item.NameCollection.FormalName = dataBaseMgr.Reader[2].ToString();
                    item.NameCollection.OtherName = dataBaseMgr.Reader[3].ToString();
                    item.NameCollection.EnglishRegularName = dataBaseMgr.Reader[4].ToString();
                    item.NameCollection.EnglishOtherName = dataBaseMgr.Reader[5].ToString();
                    item.NameCollection.EnglishName = dataBaseMgr.Reader[6].ToString();
                    item.Product.Caution = dataBaseMgr.Reader[7].ToString();
                    item.Product.StoreCondition = dataBaseMgr.Reader[8].ToString();
                    item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(dataBaseMgr.Reader[9].ToString());
                    item.User01 = dataBaseMgr.Reader[10].ToString();
                    item.NameCollection.UserCode = dataBaseMgr.Reader[11].ToString();
                }
            }
        }
       
        /// <summary>
        /// �����ʾ 
        /// </summary>
        protected void Clear()
        {
            this.lbBarCode.Text = "";
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
                {
                    this.neuSpread1_Sheet1.Cells[i, j].Text = "";
                }
            }

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 3, 0].Text = "ע�����";
            //�ָı�ǩ ����ά����ȫ ��ʱ����ʾ�洢
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 4, 0].Text = "";
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowHosName, 0].Text = "";//"����ҽѧԺ��һ����ҽԺ";
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 0].ColumnSpan = 1;
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 2].ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 3, 0].ColumnSpan = 1;

        }


        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        protected void SetPatiAndSendInfo(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal labelNum)
        {
            //��������
            this.lbBarCode.Text = "*" + applyOut.RecipeNO + "*";
            //���û�����Ϣ����ҩ��Ϣ
            if (this.patientInfo != null)
            {
                //����
                this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo, 0].Text = this.patientInfo.Name;
                //���÷�ҩ��Ϣ			
                this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo, 1].Text = applyOut.Operation.ApplyOper.OperTime.ToString();
                //��λ��
                this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo, 2].Text = applyOut.Item.User01;
                //��ҩ��ǩ��ҳ��/��ǰҳ��
                this.neuSpread1_Sheet1.Cells[(int)RowSet.RowSendInfo, 3].Text = labelNum + "/" + this.drugTotNum;
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
            this.Clear();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo, 0].Text = this.patientInfo.Name;
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo, 1].Text = "�Ա�:" + this.patientInfo.Sex.Name + "   ����:" + dataManager.GetAge(this.patientInfo.Birthday);
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo, 2].Text = "�� " + this.patientInfo.User02 + "��";
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo + 1, 0].Text = "������:  " + this.patientInfo.PID.CardNO;

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo + 2, 0].Text = "ҽ������: " + this.patientInfo.DoctorInfo.Templet.Doct.Name;
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo + 3, 0].ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo + 3, 0].Text = "�շ����ڣ� " + this.patientInfo.User01;

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 3, 0].Text = "";
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 4, 0].Text = "";

            //��������
            this.lbBarCode.Text = "*" + this.patientInfo.User03 + "*";
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
                this.Print();
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
        /// ���δ�����ӡ��ҳ��
        /// </summary>
        protected decimal labelTotNum;

        /// <summary>
        /// ���δ�����ӡ��ҳ��
        /// </summary>
        public decimal LabelTotNum
        {
            set
            {
                this.labelTotNum = value;
                this.labelNum = 1;
            }
        }
        /// <summary>
        /// ��ӡ����ҩƷ
        /// </summary>
        /// <param name="applyOut"></param>
        public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.Clear();
            FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
            this.GetRecipeLabelItem(applyOut.StockDept.ID, applyOut.Item.ID, ref item);
            //���û�����Ϣ��ʾ����ҩ��Ϣ
            this.SetPatiAndSendInfo(applyOut);
            //���ô����ڷ�ҩҩƷ��Ϣ
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin, 0].ColumnSpan = 3;
            //�˸�ҩ��־
            if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Extend)
                this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin, 0].Text = "[��] " + applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";	//����
            else
                this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin, 0].Text = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";	//����

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin, 3].Text = applyOut.Operation.ApplyQty.ToString() + applyOut.Item.MinUnit;	//����������λ
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1, 0].Text = applyOut.Item.NameCollection.RegularName;	//����
            if (applyOut.ExecNO.ToString() != "" && applyOut.ExecNO.ToString() != "0")
                this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1, 2].Text = "Ժע" + applyOut.ExecNO.ToString() + "��";

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 0].Text = this.usageHelper.GetName(applyOut.Usage.ID);						//�÷�

            //���δ���
            //			this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,1].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;	//ÿ����
            //			this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,2].Text = "  " + this.frequencyHelper.GetName(applyOut.Frequency.ID);		//Ƶ��

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 1].Text = "  " + this.frequencyHelper.GetName(applyOut.Frequency.ID);		//Ƶ��
            //��ע���ÿ���� ��ʾ������λ
            //string str = applyOut.Item.NameCollection.UserCode;
            //if (str != null && str.Length > 3 && (str.Substring(0, 3) == "002" || str.Substring(0, 3) == "003" || str.Substring(0, 3) == "004"))
            //    this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 2].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;	//ÿ����
            //else
            //    this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 2].Text = "ÿ��" + this.DoseToMin(applyOut.DoseOnce, applyOut.Item.BaseDose).ToString() + applyOut.Item.MinUnit;	//ÿ����

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 2].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;	//ÿ����

            //�÷�����'����'������ҩƷ����ӡÿ����
            if (this.usageHelper.GetObjectFromID(applyOut.Usage.ID) != null)
            {
                if (this.usageHelper.GetName(applyOut.Usage.ID).IndexOf("����") != -1)
                {
                    this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2, 2].Text = "";
                }
            }

            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 3, 1].Text = applyOut.Item.Product.Caution;								//ע������
            //Ĭ�ϲ���ʾ�洢
            //			if (applyOut.Item.StoreCondition == "")
            //				applyOut.Item.StoreCondition = "���´洢";
            //			this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 4,1].Text = applyOut.Item.StoreCondition;						//�洢����
            this.neuSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 4, 1].Text = "";

            this.labelNum = this.labelNum + 1;
        }

        /// <summary>
        /// ��ӡһ��ҩƷ
        /// </summary>
        /// <param name="alCombo"></param>
        public void AddCombo(ArrayList alCombo)
        {
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

                FS.HISFC.Components.Common.Classes.Function.GetPageSize("RecipeLabel", ref p);
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            }
            System.Windows.Forms.Control c = this;
            c.Width = this.Width;
            c.Height = this.Height;
            //			p.PrintPreview(12,1,c);
            p.PrintPage(12, 1, c);

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
                this.Print();
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
    }
}
