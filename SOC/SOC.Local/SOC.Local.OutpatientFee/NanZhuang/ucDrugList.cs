using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.FrameWork.Management;
using System.Collections;
using Neusoft.HISFC.BizLogic.Pharmacy;
using Neusoft.HISFC.Models.Pharmacy;
using Neusoft.HISFC.Models.HealthRecord.EnumServer;

namespace Neusoft.SOC.Local.OutpatientFee.NanZhuang
{
    public partial class ucDrugList : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee
    {
        public ucDrugList()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                Init();
            }
        }

        #region ����

        /// <summary>
        /// ��ҩ��
        /// </summary>
        decimal drugListTotalPrice = 0;

        /// <summary>
        /// �������
        /// </summary>
        private int rowMaxCount = 12;

        /// <summary>
        /// ҩ�������
        /// </summary>
        Neusoft.HISFC.BizLogic.Pharmacy.Item itemManager = new Neusoft.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        Neusoft.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new Neusoft.HISFC.BizLogic.Pharmacy.DrugStore();

        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        Neusoft.HISFC.BizLogic.Registration.Register registerMgr = new Neusoft.HISFC.BizLogic.Registration.Register();

        Neusoft.HISFC.Models.Registration.Register register = new Neusoft.HISFC.Models.Registration.Register();

        Neusoft.SOC.Local.OutpatientFee.NanZhuang.ucHerbalDrugList herbalDrugList = new ucHerbalDrugList();

        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        private bool isPrint = true;

        private string isReprint = "1";

        string sendWindows = "";

        private bool isMoreOnePage = false;

        /// <summary>
        /// ���մ����š�������ˮ������
        /// </summary>
        private FeeItemCompare itemCompare = new FeeItemCompare();

        #endregion

        private void AddAllData(System.Collections.ArrayList al, string diagnose, string hospitalName)
        {
            if (al.Count > 0)
            {
                //����
                int iRow = 1;
                int num = 0;
                DateTime recipeTime = new DateTime();
                al.Sort(itemCompare);

                ArrayList alPrintFeeItem_PCC = new ArrayList();
                ArrayList alPrintFeeItem_Inject = new ArrayList();
                ArrayList alPrintFeeItem_Other = new ArrayList();

                //��һ����Ŀ
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList preFeeItemObj = null;

                //��һ����Ŀ����ϱ��
                string firstCombFlag = "";

                //���һ����Ŀ����ϱ��
                string lastCombFlag = "";

                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj in al)
                {
                    recipeTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(feeItemObj.FeeOper.OperTime);
                    num++;
                    //��ҩƷ����
                    if (feeItemObj.Item.ItemType != Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }
                    //����ǲ�ҩ�߲�ҩ�ӿ�
                    if (feeItemObj.Item.SysClass.ID.ToString() == "PCC")
                    { 
                        alPrintFeeItem_PCC.Add(feeItemObj);
                        continue;
                    }
                    if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(feeItemObj.Order.Usage.ID))
                    {
                        if (alPrintFeeItem_Inject.Count <= 10)
                        {
                            alPrintFeeItem_Inject.Add(feeItemObj);
                        }

                        //if (alPrintFeeItem_Inject.Count == 10 || (num == al.Count && alPrintFeeItem_Inject.Count > 0))
                        //{
                        //    ////��һ����Ŀ����ϱ��
                        //    //if (num <= 10)
                        //    //{
                        //    //    firstCombFlag = "";
                        //    //}
                        //    //else
                        //    //{
                        //    //    if (preFeeItemObj.Order.Combo.ID == feeItemObj.Order.Combo.ID)
                        //    //    {
                        //    //        if(
                        //    //    }
                        //    //}

                        //    this.Print(alPrintFeeItem_Inject, diagnose, hospitalName);
                        //    alPrintFeeItem_Inject = new ArrayList();
                        //}
                    }
                    else
                    {
                        if (alPrintFeeItem_Other.Count <= 10)
                        {
                            alPrintFeeItem_Other.Add(feeItemObj);
                        }

                        //if (alPrintFeeItem_Other.Count == 10 || (num == al.Count && alPrintFeeItem_Other.Count > 0))
                        //{
                        //    this.Print(alPrintFeeItem_Other, diagnose, hospitalName);
                        //    alPrintFeeItem_Other = new ArrayList();
                        //}
                    }
                    preFeeItemObj = feeItemObj.Clone();
                }
                    if (num == al.Count)
                    {
                        if (alPrintFeeItem_Inject.Count > 0)
                        {
                            this.Clear();
                            this.Print(alPrintFeeItem_Inject, diagnose, hospitalName);
                            alPrintFeeItem_Inject = new ArrayList();
                        }
                        if (alPrintFeeItem_Other.Count > 0)
                        {
                            this.Clear();
                            this.Print(alPrintFeeItem_Other, diagnose, hospitalName);
                            alPrintFeeItem_Other = new ArrayList();
                        }
                        if (alPrintFeeItem_PCC.Count > 0)
                        {
                            this.Clear();
                            herbalDrugList.Print(alPrintFeeItem_PCC, diagnose, register, hospitalName, recipeTime, this.isReprint);
                            alPrintFeeItem_Other = new ArrayList();
                        }
                    }
            }
        }

        private void Print(ArrayList alFeeItem, string diagnose, string hospitalName)
        {
            if (alFeeItem == null || alFeeItem.Count == 0)
            {
                return;
            }

            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj = alFeeItem[0] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;

            // ����Lable��ֵ
            SetLableValue(feeItemObj, diagnose, hospitalName);
            //����
            int iRow = 1;
            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeItem)
            {
                SetDrugDeatail(iRow, feeItemList, true);
                iRow++;
            }

            //������ҩ�ۺͷ�ҩʱ��
            //this.AddLastRow();������ʾ��ҩ��
            drugListTotalPrice = 0;
            iRow = 1;

            this.Print();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            if (isPrint == true)
            {
                Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
                print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 450, 400));
                print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsDataAutoExtend = false;
                try
                {
                    //�ռ÷�Ժ4�Ŵ����Զ���ӡ������ͣ����ӡ����ͷ��ֽ��̫����̫�񶼿���������ͣ
                    Neusoft.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                catch { }
                if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(5, 5, this);
                }
                else
                {
                    print.PrintPage(5, 5, this);
                }
                this.Clear();
            }

            this.isPrint = true;
        }

        /// <summary>
        /// ���������뷽��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// ����ؼ�����
        /// </summary>
        private void Clear()
        {
            lbName.Text = "����:";
            lbCardNo.Text = "��������:";
            lbSex.Text = "�Ա�:";
            lbDiagnose.Text = "���:";
            lbAge.Text = "����:";
            lbDeptName.Text = "��������:";
            lbInvoice.Text = "��Ʊ��:";
            lbRecipeTime.Text = "������:";
            lbDoctor.Text = "ҽʦ:";

            this.sendWindows = "";
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 1)
                {
                    for (int i = 1; i < this.rowMaxCount - 1; i++)
                    {
                        this.neuSpread1_Sheet1.SetText(i, 0, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 1, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 2, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 3, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 4, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 5, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 6, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 7, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 8, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 9, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 10, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 11, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 12, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 13, string.Empty);
                    }
                    this.neuSpread1_Sheet1.SetText(this.rowMaxCount - 1, 0, string.Empty);
                }
            }
            catch
            {
                this.neuSpread1_Sheet1.RowCount = 0;
                this.neuSpread1_Sheet1.RowCount = this.rowMaxCount;
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.Clear();

            this.SetFormat();


            if (this.neuSpread1_Sheet1.Rows.Count > this.rowMaxCount)
            {
                this.neuSpread1_Sheet1.Rows.Remove(rowMaxCount, this.neuSpread1_Sheet1.Rows.Count - this.rowMaxCount);
            }

            else if (this.rowMaxCount > this.neuSpread1_Sheet1.Rows.Count)
            {
                this.neuSpread1_Sheet1.Rows.Add(rowMaxCount, this.rowMaxCount - this.neuSpread1_Sheet1.Rows.Count);
            }
            else
            {

            }

        }

        /// <summary>
        /// ����Lable��ֵ
        /// </summary>
        /// <param name="info"></param>
        private void SetLableValue(Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj, string diagnose, string hospitalName)
        {

            //this.neuPanName.Text = hospitalName + "ҩƷ�嵥";
            //����
            lbName.Text = "������" + register.Name;
            this.lbCardNo.Text = "�������ţ�" + feeItemObj.Patient.PID.CardNO;
            lbSex.Text = "�Ա�" + register.Sex.Name;
            lbAge.Text = "���䣺" + itemManager.GetAge(register.Birthday);
            this.lbDoctor.Text = "ҽʦ��" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(feeItemObj.RecipeOper.ID);
            //��������
            lbDeptName.Text = "�������ƣ�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(register.DoctorInfo.Templet.Dept.ID);
            //���
            this.lbDiagnose.Text = "��ϣ�" + diagnose.ToString();
            //��������
            lbRecipeTime.Text = "�������ڣ�" + Neusoft.FrameWork.Function.NConvert.ToDateTime(feeItemObj.FeeOper.OperTime).ToString("yyyy-MM-dd");

        }

        /// <summary>
        /// ����Ϣ��ӵ��б���
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="info"></param>
        private void SetDrugDeatail(int iRow, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj, bool isValid)
        {
            //�Զ�����
            this.neuSpread1_Sheet1.SetText(iRow, 0, SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(feeItemObj.Item.ID));
            //ҩƷ��
            this.neuSpread1_Sheet1.SetText(iRow, 1, feeItemObj.Item.Name);
            //���
            if (string.IsNullOrEmpty(feeItemObj.Item.SpecialFlag1))
            {
                feeItemObj.Item.SpecialFlag1 = "  ";
            }
            this.neuSpread1_Sheet1.SetText(iRow, 2, feeItemObj.Item.SpecialFlag1 + feeItemObj.Item.Specs);
            //����

            this.neuSpread1_Sheet1.SetText(iRow, 3, ((int)(feeItemObj.Item.Qty)).ToString("F4").TrimEnd('0').TrimEnd('.') + feeItemObj.Item.PriceUnit);

            //��ҩ��
            this.drugListTotalPrice += (feeItemObj.Item.Price / feeItemObj.Item.PackQty) * (feeItemObj.Item.Qty * feeItemObj.Days);

            //�÷�
            //this.neuSpread1_Sheet1.SetText(iRow, 7, SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
            try
            {
                this.neuSpread1_Sheet1.SetText(iRow, 7, SOC.HISFC.BizProcess.Cache.Common.GetUsage(feeItemObj.Order.Usage.ID).Name);
            }
            catch { }
            //ÿ������
            if (feeItemObj.Order.DoseOnce == 0)
            {
                this.neuSpread1_Sheet1.SetText(iRow, 8, "��ҽ��");
            }
            else
            {
                this.neuSpread1_Sheet1.SetText(iRow, 8, feeItemObj.Order.DoseOnce.ToString() + feeItemObj.Order.DoseUnit);
            }

            if (!string.IsNullOrEmpty(feeItemObj.Order.Frequency.ID))
            {
                //this.neuSpread1_Sheet1.SetText(iRow, 9, info.Frequency.ID.ToLower());
                //this.neuSpread1_Sheet1.SetText(iRow, 9, SOC.LocalDrugStore.Function.GetFrequenceName((Neusoft.HISFC.Models.Order.Frequency)(info.Frequency)));
                this.neuSpread1_Sheet1.SetText(iRow, 9, feeItemObj.Order.Frequency.ID);
            }
            try
            {
                this.neuSpread1_Sheet1.Cells[iRow, 11].Text = feeItemObj.Days.ToString();
            }
            catch { }
            if (feeItemObj.Order.Combo.ID.ToString().Length > 2)
            {
                this.neuSpread1_Sheet1.SetText(iRow, 13, feeItemObj.Order.Combo.ID.Substring(feeItemObj.Order.Combo.ID.Length - 3));
            }
            else
            {
                this.neuSpread1_Sheet1.SetText(iRow, 13, "��");
            }
            if (isMoreOnePage)
            {
                this.neuSpread1_Sheet1.Columns[12].Visible = false;
                this.neuSpread1_Sheet1.Columns[13].Visible = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[12].Visible = true;
                this.neuSpread1_Sheet1.Columns[13].Visible = false;
                Neusoft.HISFC.Components.Common.Classes.Function.DrawCombo(this.neuSpread1_Sheet1, 13, 12);
            }
        }

        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        private string GetDiag(string ClinicCode)
        {
            try
            {
                ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(ClinicCode, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                if (al == null)
                {
                    MessageBox.Show("��ȡ�����Ϣʧ�ܣ�" + diagManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }

                string strDiag = "";
                foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
                {
                    if (diag != null && diag.Memo != null && diag.Memo != "")
                    {
                        strDiag += diag.Memo + "��";
                    }
                    else
                    {
                        strDiag += diag.DiagInfo.ICD10.Name;
                    }
                }
                strDiag = strDiag.TrimEnd(new char[] { '��' });
                return strDiag;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ȡ�����Ϣʧ�ܣ�" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /// <summary>
        /// �������һ��
        /// </summary>
        private void AddLastRow()
        {
            this.neuSpread1_Sheet1.Models.Span.Add(this.rowMaxCount - 1, 0, 1, 12);
            try
            {
                this.neuSpread1_Sheet1.SetText(this.rowMaxCount - 1, 0, string.Format("��ҩ�ۣ���{0}               ��ӡʱ�䣺{1}    {2}",
                           new object[] { this.drugListTotalPrice.ToString("0.00"), itemManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"), this.sendWindows }));
            }
            catch { }
            FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, true);
            this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Border = lineBorder11;
            this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }

        /// <summary>
        /// FarPoint��ʽ
        /// </summary>
        private void SetFormat()
        {
            //FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder4 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder5 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder6 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder7 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder8 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder9 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder10 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);

            //FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            //t.WordWrap = true;

            //this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "����";
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).Border = lineBorder2;
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "ҩƷ����";
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).Border = lineBorder3;
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "  ���";
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).Border = lineBorder4;
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "����";
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).Border = lineBorder5;
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "��λ";
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).Border = lineBorder8;
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "�÷�";

            //this.neuSpread1_Sheet1.Cells.Get(0, 6).Border = lineBorder9;
            //this.neuSpread1_Sheet1.Cells.Get(0, 6).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "ÿ������";
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).Border = lineBorder10;
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "Ƶ��";

            //this.neuSpread1_Sheet1.Cells.Get(0, 8).Border = lineBorder6;
            //this.neuSpread1_Sheet1.Cells.Get(0, 8).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 8).Value = "����";
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).Border = lineBorder7;
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).Value = "���";
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).Border = lineBorder11;
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).Value = "����";
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).Border = lineBorder11;
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).Value = "��ע";
            //this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(0).Width = 55F;
            //this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(1).Width = 165F;
            //this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(2).Width = 100F;
            //this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(3).Width = 40F;
            //this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(4).Width = 35F;
            //this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(5).Width = 80F;
            //this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(6).Width = 75F;
            //this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(7).Width = 45F;
            //this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(8).Width = 55F;
            //this.neuSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(9).Width = 65F;
            //this.neuSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(10).Width = 0F;
            //this.neuSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(11).Width = 100F;
            //this.neuSpread1_Sheet1.Columns.Get(11).CellType = t;
            //this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            //this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
        }

        /// <summary>
        /// ��ӡ��ҩ�嵥
        /// </summary>
        /// <param name="alFeeItemLists">������ϸ</param>
        /// <param name="diagnose">���</param>
        /// <returns></returns>
        public int PrintDrugBill(ArrayList alFeeItemLists, string diagnose, string hospitalName)
        {
            if (alFeeItemLists == null || alFeeItemLists.Count == 0)
            {
                return -1;
            }
            this.Init();
            this.AddAllData(alFeeItemLists, diagnose, hospitalName);

            return 0;
        }

        #region IOutpatientAfterFee ��Ա

        public int AfterFee(ArrayList alFeeItem, string info)
        {
            string hospitalName= Neusoft.FrameWork.Management.Connection.Hospital.Name.ToString();
            this.isReprint = isReprint;
            if (this.isReprint=="0")
            {
                this.lbReprint.Visible = false;
            }
            else
            {
                this.lbReprint.Visible = true;
            }
            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj = (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList)alFeeItem[0];
            this.register = (Neusoft.HISFC.Models.Registration.Register)registerMgr.QueryPatient(feeItemObj.Patient.ID)[0];
            string diagnose=this.GetDiag(feeItemObj.Patient.ID);
            return this.PrintDrugBill(alFeeItem, diagnose, hospitalName);
        }

        #endregion
    }

    public class FeeItemCompare : IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            try
            {
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem1 = x as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem2 = y as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;

                if (Neusoft.FrameWork.Function.NConvert.ToInt32(feeItem1.RecipeNO) > Neusoft.FrameWork.Function.NConvert.ToInt32(feeItem2.RecipeNO))
                {
                    return -1;
                }
                else if (Neusoft.FrameWork.Function.NConvert.ToInt32(feeItem1.RecipeNO) < Neusoft.FrameWork.Function.NConvert.ToInt32(feeItem2.RecipeNO))
                {
                    return 1;
                }
                else
                {
                    if (feeItem1.SequenceNO > feeItem2.SequenceNO)
                    {
                        return -1;
                    }
                    else if (feeItem1.SequenceNO < feeItem2.SequenceNO)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}
