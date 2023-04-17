using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// ��ҩ����ӡ
    /// </summary>
    public partial class ucDrugBill : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        /// <summary>
        /// ��ҩ����ӡ
        /// </summary>
        public ucDrugBill()
        {
            InitializeComponent();
        }

        private ucDrugBillDetail ucDetail = new ucDrugBillDetail();

        private FS.HISFC.Models.Pharmacy.DrugBillClass tempDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();

        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        
        #region IDrugPrint ��Ա

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            this.SetTabPageShow(drugBillClass);

            switch (drugBillClass.PrintType.ID.ToString())
            {
                case "D":           //��ϸ��ӡ
                case "R":           //������
                    ArrayList alDetail = new ArrayList();
                    if (drugBillClass.DrugBillNO == null || drugBillClass.DrugBillNO == "")
                    {
                        alDetail = al;
                    }
                    else
                    {
                        alDetail = this.itemManager.QueryDrugBillDetail(drugBillClass.DrugBillNO);
                        if (alDetail == null || alDetail.Count == 0)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ݰ�ҩ���Ż�ȡ��ҩ����ϸ��Ϣ��������") + this.itemManager.Err);
                            return;
                        }

                        System.Collections.Hashtable hsOriginal = new Hashtable();
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in al)
                        {
                            if (hsOriginal.ContainsKey(temp.User01+temp.User02))
                            {
                            }
                            else
                            {
                                hsOriginal.Add(temp.User01 + temp.User02, temp.PatientNO);
                            }
                        }

                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut detail in alDetail)
                        {
                            if (hsOriginal.ContainsKey((detail.User02).Substring(4) + detail.User01))
                            {
                                detail.PatientNO = hsOriginal[(detail.User02).Substring(4) + detail.User01] as string;
                            }
                        }
 
                    }
                     
                    this.ucDrugBillDetail1.Clear();
                    this.ucDrugBillDetail1.IfBPrint = "�ѷ�";
                    this.ucDrugTotal1.IfBPrint = "�ѷ�";
                    this.ucDrugBillDetail1.ShowData(alDetail, drugBillClass);
                    break;
                case "T":           //���ܴ�ӡ
                    ArrayList alTotal = new ArrayList();
                    if (drugBillClass.DrugBillNO == null || drugBillClass.DrugBillNO == "")
                    {
                        alTotal = al;
                    }
                    else
                    {
                        alTotal = this.itemManager.QueryDrugBillTotal(drugBillClass.DrugBillNO);
                        if (alTotal == null || alTotal.Count == 0)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ݰ�ҩ���Ż�ȡ��ҩ��������Ϣ��������"));
                            return;
                        }
                    }

                    this.ucDrugTotal1.Clear();
                    this.ucDrugTotal1.IfBPrint = "�ѷ�";
                    this.ucDrugBillDetail1.IfBPrint = "�ѷ�";
                    this.ucDrugTotal1.ShowData(alTotal, drugBillClass);

                    ArrayList alDetailTemp = new ArrayList();
                    if (drugBillClass.DrugBillNO == null || drugBillClass.DrugBillNO == "")
                    {
                        alDetailTemp = al;
                    }
                    else
                    {
                        alDetailTemp = this.itemManager.QueryDrugBillDetail(drugBillClass.DrugBillNO);
                        if (alDetailTemp == null || alDetailTemp.Count == 0)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ݰ�ҩ���Ż�ȡ��ҩ����ϸ��Ϣ��������") + this.itemManager.Err);
                            return;
                        }
                    }

                    this.ucDrugBillDetail1.Clear();
                    this.ucDrugBillDetail1.ShowData(alDetailTemp, drugBillClass);
                    break;
                case "H":           //��ҩ��
                    ArrayList alHerbalDetail = new ArrayList();
                    if (drugBillClass.DrugBillNO == null || drugBillClass.DrugBillNO == "")
                    {
                        alHerbalDetail = al;
                    }
                    else
                    {
                        alHerbalDetail = this.itemManager.QueryDrugBillDetail(drugBillClass.DrugBillNO);
                        if (alHerbalDetail == null || alHerbalDetail.Count == 0)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ݰ�ҩ���Ż�ȡ��ҩ����ϸ��Ϣ��������"));
                            return;
                        }
                    }

                    this.ucDrugHerbal1.Clear();
                    this.ucDrugHerbal1.ShowData(alHerbalDetail, drugBillClass);
                    break;
            }

            this.tempDrugBill = drugBillClass;
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddAllData(System.Collections.ArrayList al)
        {
            if (this.tempDrugBill == null)
            {
                this.ucDrugBillDetail1.Clear();

                this.ucDrugTotal1.Clear();

                this.ucDrugHerbal1.Clear();

                return;
            }

            this.SetTabPageShow(this.tempDrugBill);

            switch (this.tempDrugBill.PrintType.ID.ToString())
            {
                case "D":           //��ϸ��ӡ
                case "R":           //������                                     
                    this.ucDrugBillDetail1.Clear();
                    this.ucDrugBillDetail1.ShowData(al, this.tempDrugBill);
                    break;
                case "T":           //���ܴ�ӡ
                   
                    this.ucDrugTotal1.Clear();
                    this.ucDrugTotal1.ShowData(al, this.tempDrugBill);

                    this.ucDrugBillDetail1.Clear();
                    this.ucDrugBillDetail1.ShowData(al, this.tempDrugBill);

                    break;
                case "H":           //��ҩ��                  

                    this.ucDrugHerbal1.Clear();
                    this.ucDrugHerbal1.ShowData(al, this.tempDrugBill);
                    break;
            }

            //this.tempDrugBill = drugBillClass;
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddCombo(System.Collections.ArrayList alCombo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        decimal FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.DrugTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        FS.HISFC.Models.RADT.PatientInfo FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.InpatientInfo
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

        decimal FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.LabelTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        FS.HISFC.Models.Registration.Register FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.OutpatientInfo
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

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.Preview()
        {
            switch (this.tempDrugBill.PrintType.ID.ToString())            
            {
                case "D":           //��ϸ��ӡ
                case "R":           //������
                    this.ucDrugBillDetail1.PrintPreview();
                    break;
                case "T":           //���ܴ�ӡ
                    this.ucDrugTotal1.PrintPreview();

                    this.ucDrugBillDetail1.PrintPreview();
                    break;
                case "H":           //��ҩ��
                    this.ucDrugHerbal1.PrintPreview();
                    break;
            }
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.Print()
        {
            switch (this.tempDrugBill.PrintType.ID.ToString())
            {
                case "D":           //��ϸ��ӡ
                case "R":           //������
                    this.ucDrugBillDetail1.PrintPreview();
                    break;
                case "T":           //���ܴ�ӡ
                    this.ucDrugTotal1.PrintPreview();

                    this.ucDrugBillDetail1.PrintPreview();
                    break;
                case "H":           //��ҩ��
                    this.ucDrugHerbal1.PrintPreview();
                    break;
            }
        }

        #endregion

        private void SetTabPageShow(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            //if (this.tempDrugBill.PrintType.ID.ToString() == drugBillClass.PrintType.ID.ToString())
            //{
            //    return;
            //}

            switch (drugBillClass.PrintType.ID.ToString())
            {
                case "D":
                case "R":
                    if (!this.neuTabControl1.TabPages.Contains(this.tabPage1))
                        this.neuTabControl1.TabPages.Add(this.tabPage1);
                    if (this.neuTabControl1.TabPages.Contains(this.tabPage2))
                        this.neuTabControl1.TabPages.Remove(this.tabPage2);
                    if (this.neuTabControl1.TabPages.Contains(this.tabPage3))
                        this.neuTabControl1.TabPages.Remove(this.tabPage3);
                    break;
                case "T":
                    if (this.neuTabControl1.TabPages.Contains(this.tabPage1))
                        this.neuTabControl1.TabPages.Remove(this.tabPage1);
                    if (!this.neuTabControl1.TabPages.Contains(this.tabPage2))
                        this.neuTabControl1.TabPages.Add(this.tabPage2);
                    if (this.neuTabControl1.TabPages.Contains(this.tabPage3))
                        this.neuTabControl1.TabPages.Remove(this.tabPage3);
                    break;
                case "H":
                    if (this.neuTabControl1.TabPages.Contains(this.tabPage1))
                        this.neuTabControl1.TabPages.Remove(this.tabPage1);
                    if (this.neuTabControl1.TabPages.Contains(this.tabPage2))
                        this.neuTabControl1.TabPages.Remove(this.tabPage2);
                    if (!this.neuTabControl1.TabPages.Contains(this.tabPage3))
                        this.neuTabControl1.TabPages.Add(this.tabPage3);
                    break;
            }
        }
    }
}
