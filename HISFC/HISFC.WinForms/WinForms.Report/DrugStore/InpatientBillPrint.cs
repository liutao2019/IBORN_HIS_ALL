using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// ��ҩ����ӡ����
    /// </summary>
    public class InpatientBillPrint : FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {

        private FS.HISFC.Models.Pharmacy.DrugBillClass tempDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();

        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        private ucDrugBillDetail ucDrugBillDetail1 = null;

        private ucDrugTotal ucDrugTotal1 = null;

        private ucDrugHerbal ucDrugHerbal1 = null;

        #region IDrugPrint ��Ա

        public void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="al"></param>
        /// <param name="drugBillClass"></param>
        public void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            switch (drugBillClass.PrintType.ID.ToString())
            {
                case "D":           //��ϸ��ӡ
                case "R":           //������
                case "O":           //��չ��ӡ Ĭ��ȡ������ϸ��
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
                    }

                    System.Collections.Hashtable hash = new Hashtable();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in al)
                    {
                        if (hash.ContainsKey(temp.User01 + temp.User02))
                        {
                        }
                        else
                        {
                            hash.Add(temp.User01 + temp.User02, temp.PatientNO);
                        }
                    }

                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut detail in alDetail)
                    {
                        //{D515D71A-75B4-4c02-B2F7-569779A2A5A8}
                        //if(hash.ContainsKey((detail.User02).Substring(4) + detail.User01))
                        //{
                        //    detail.PatientNO = hash[(detail.User02).Substring(4) + detail.User01] as string;
                        //}
                        if (hash.ContainsKey(detail.User01 + detail.User02))
                        {
                            detail.PatientNO = hash[detail.User01  + detail.User02] as string;
                        }
                    }


                    if (this.ucDrugBillDetail1 == null)
                    {
                        this.ucDrugBillDetail1 = new ucDrugBillDetail();
                    }
                    this.ucDrugBillDetail1.Clear();
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

                    if (this.ucDrugTotal1 == null)
                    {
                        this.ucDrugTotal1 = new ucDrugTotal();
                    }
                    this.ucDrugTotal1.Clear();
                    this.ucDrugTotal1.ShowData(alTotal, drugBillClass);
                    if (drugBillClass.User01 != "NurseType")//{31607136-EF3D-46af-A2F9-EE96F6F9209C}
                    {
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

                        if (this.ucDrugBillDetail1 == null)
                        {
                            this.ucDrugBillDetail1 = new ucDrugBillDetail();
                        }
                        this.ucDrugBillDetail1.Clear();
                        this.ucDrugBillDetail1.ShowData(alDetailTemp, drugBillClass);
                    }
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

                    if (this.ucDrugHerbal1 == null)
                    {
                        this.ucDrugHerbal1 = new ucDrugHerbal();
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

            switch (this.tempDrugBill.PrintType.ID.ToString())
            {
                case "D":           //��ϸ��ӡ
                case "R":           //������    
                case "O":           //��չ��ӡ Ĭ��ȡ������ϸ��

                    if (this.ucDrugBillDetail1 == null)
                    {
                        this.ucDrugBillDetail1 = new ucDrugBillDetail();
                    }
                    this.ucDrugBillDetail1.Clear();
                    this.ucDrugBillDetail1.ShowData(al, this.tempDrugBill);
                    break;
                case "T":           //���ܴ�ӡ

                    if (this.ucDrugTotal1 == null)
                    {
                        this.ucDrugTotal1 = new ucDrugTotal();
                    }
                    this.ucDrugTotal1.Clear();
                    this.ucDrugTotal1.ShowData(al, this.tempDrugBill);

                    if (this.ucDrugBillDetail1 == null)
                    {
                        this.ucDrugBillDetail1 = new ucDrugBillDetail();
                    }
                    this.ucDrugBillDetail1.Clear();
                    this.ucDrugBillDetail1.ShowData(al, this.tempDrugBill);

                    break;
                case "H":           //��ҩ��                  

                    if (this.ucDrugHerbal1 == null)
                    {
                        this.ucDrugHerbal1 = new ucDrugHerbal();
                    }
                    this.ucDrugHerbal1.Clear();
                    this.ucDrugHerbal1.ShowData(al, this.tempDrugBill);
                    break;
            }
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
                case "O":           //��չ��ӡ Ĭ��ȡ������ϸ��
                    this.ucDrugBillDetail1.PrintPreview();
                    break;
                case "T":           //���ܴ�ӡ
                    this.ucDrugTotal1.PrintPreview();

                    //this.ucDrugBillDetail1.PrintPreview();//���ܴ�ӡ����ӡ��ϸ{7B180C79-21E1-4967-A430-0C581FE2C38F}
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
                case "O":           //��չ��ӡ Ĭ��ȡ������ϸ��
                    this.ucDrugBillDetail1.PrintPreview();
                    break;
                case "T":           //���ܴ�ӡ
                    this.ucDrugTotal1.PrintPreview();

                    //this.ucDrugBillDetail1.PrintPreview();//���ܴ�ӡ����ӡ��ϸ{7B180C79-21E1-4967-A430-0C581FE2C38F}
                    break;
                case "H":           //��ҩ��
                    this.ucDrugHerbal1.PrintPreview();
                    break;
            }
        }


        #endregion
    }
}
