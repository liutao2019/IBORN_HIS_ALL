using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.LSYY.Inpatient
{
    /// <summary>
    /// [��������: סԺҩ����ҩ��ҩ�����ػ�]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2011-02]<br></br>
    /// ˵����
    /// </summary>
    public partial class ucHerbalDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucHerbalDrugBill()
        {
            InitializeComponent();
        }

        #region ��ö��
        enum ColSet
        {
            ColDrugNO1,
            ColTradeName1,
            ColQty1,
            ColUnit1,
            ColCost1,
            ColSpeUsage1,

            ColDrugNO2,
            ColTradeName2,
            ColQty2,
            ColUnit2,
            ColCost2,
            ColSpeUsage2,

            ColEnd
        }

        #endregion

        #region ����

        /// <summary>
        /// ÿҳ��ӡ����
        /// </summary>
        private int pageCount = 2;

        /// <summary>
        /// ���ҳ��
        /// </summary>
        int totPageNO = 0;
        #endregion

        #region ��ҩ����ͨ�÷���

        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            this.nlblBillNO.Text = "���ţ�";
            this.nlbStockDeptName.Text = "��ҩ���ң�";
            this.nlbFirstPrintTime.Text = "�״δ�ӡ��";
            this.nlbPrintTime.Text = "��ӡʱ�䣺";
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// Fp����
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            this.neuSpread1_Sheet1.Rows.Default.Height = 30;
            this.neuSpread1_Sheet1.Columns.Default.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Default.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.neuSpread1_Sheet1.Columns.Count = (int)ColSet.ColEnd;

            FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColDrugNO1].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColDrugNO1].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColTradeName1].Width = 110F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColTradeName1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[(int)ColSet.ColTradeName1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColQty1].Width = 90F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColUnit1].Width = 34F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColUnit1].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColCost1].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColCost1].CellType = n;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColCost1].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColSpeUsage1].Width = 130F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColDrugNO2].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColDrugNO2].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColTradeName2].Width = 110F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColTradeName2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[(int)ColSet.ColTradeName2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColQty2].Width = 90F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColUnit2].Width = 34F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColUnit2].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColCost2].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColCost2].CellType = n;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColCost2].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColSet.ColSpeUsage2].Width = 130F;

            this.neuSpread1_Sheet1.DefaultStyle.Font = new Font("����", 10.5F);
        }

        /// <summary>
        /// ��ʵ��û�����壬�ͻ��ܵ�ͳһ����
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        private void ShowBillData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowDetailData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            FarPoint.Win.LineBorder border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);


            this.SuspendLayout();

            #region ����

            this.nlbStockDeptName.Text = "��ҩ���ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);

            //����������
            alData.Sort(new CompareApplyOutByCombNO());

            //������Ϻ�
            string combNO = string.Empty;
            //�к�
            int iRow = 0;
            //�кţ�
            int iCol = 0;
            //��ҩ��
            decimal drugListTotalPrice = 0;
            #endregion

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();


            #region ��������

            //���ݺ�
            this.nlblBillNO.Text = "���ţ�" + drugBillClass.DrugBillNO;

            System.Collections.Hashtable hsCombo = new Hashtable();
            FS.HISFC.Models.Pharmacy.ApplyOut lastInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                //this.lbInsureDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);


                if (hsCombo.Contains(info.CombNO))
                {
                    int qty = (int)hsCombo[info.CombNO];
                    qty = qty + 1;
                    hsCombo[info.CombNO] = qty;
                }
                else
                {
                    hsCombo.Add(info.CombNO, 1);
                }


                //this.lbApplyDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyOper.Dept.ID);             


                string bedNO = info.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }

                if (combNO != info.CombNO)
                {
                    if (iCol == (int)ColSet.ColEnd / 2)
                    {
                        iCol = 0;
                        iRow++;
                    }
                    if (!string.IsNullOrEmpty(combNO))
                    {
                        AddNewRow(iRow, false);
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColDrugNO1].Text = "ҩζ����";
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColTradeName1].Text = hsCombo[lastInfo.CombNO].ToString();
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Text = "������";
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColCost2].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColCost2].Text = lastInfo.Days.ToString();
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Font = new Font("����", 11F, FontStyle.Bold);
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColCost2].Font = new Font("����", 11F, FontStyle.Bold);
                        iRow++;
                    }

                    AddNewRow(iRow, true);

                    this.neuSpread1_Sheet1.Cells[iRow, 0].Text =
                        string.Format("{3}    {0}��    ������{1}    סԺID��{2}    ����ҽ����{4}", bedNO, info.PatientName, info.PatientNO, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID), SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID));
                    if (iRow == 0)
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
                    }
                    iRow++;
                    this.AddLabelRow(iRow);
                    for (int colIndex = 0; colIndex < (int)ColSet.ColEnd; colIndex++)
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, colIndex].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                    }

                    iRow++;
                    iCol = 0;

                    combNO = info.CombNO;

                }
                if (info.Item.BaseDose == 0)
                {
                    info.Item.BaseDose = 1;
                }
                if (iCol == 0)
                {
                    this.AddNewRow(iRow, false);
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColDrugNO1].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID);
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColTradeName1].Text = info.Item.Name;
                    if (info.DoseOnce <= 0 || info.Item.MinUnit == info.Item.DoseUnit || Math.Round(info.Operation.ApplyQty, 0) == info.Operation.ApplyQty)
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty1].Text = info.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit;
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColUnit1].Text = info.Item.MinUnit;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty1].Text = (info.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColUnit1].Text = info.Item.DoseUnit;
                    }
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColCost1].Value = (info.Operation.ApplyQty * info.Days / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F2");
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColSpeUsage1].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID);
                    iCol = (int)ColSet.ColEnd / 2;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColDrugNO2].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID);
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColTradeName2].Text = info.Item.Name;
                    //this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Text = info.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit;
                    if (info.DoseOnce <= 0 || info.Item.MinUnit == info.Item.DoseUnit || Math.Round(info.Operation.ApplyQty, 0) == info.Operation.ApplyQty)
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Text = info.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit;
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColUnit2].Text = info.Item.MinUnit;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Text = (info.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColUnit2].Text = info.Item.DoseUnit;
                    }
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColCost2].Value = (info.Operation.ApplyQty * info.Days / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F2");
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID);
                    iCol = (int)ColSet.ColEnd;
                }

                //��ӡʱ�䣬�״δ�ӡҲ���ڱ�����ӡ�ģ�info.State�������0����drugBillClass.ApplyState�ڵ��øÿؼ�ǰ�����
                if (drugBillClass.ApplyState != "0")
                {
                    this.nlbFirstPrintTime.Text = "�״δ�ӡ��" + info.Operation.ExamOper.OperTime.ToString();
                    this.nlbPrintTime.Text = "��ӡʱ�䣺" + DateTime.Now;
                }
                else
                {
                    this.nlbPrintTime.Text = "��ӡʱ�䣺" + info.Operation.ExamOper.OperTime.ToString();
                    this.nlbFirstPrintTime.Text = "";
                }


                //����
                decimal drugPrice = info.Operation.ApplyQty * info.Days / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                //���
                //��ҩ��
                drugListTotalPrice += FS.FrameWork.Function.NConvert.ToDecimal(drugPrice.ToString("F2"));

                //�кŵ���5ʱ�����¿�ʼһ��
                if (iCol == (int)ColSet.ColEnd)
                {
                    iCol = 0;
                    iRow++;
                }
                lastInfo = info;
            }
            #endregion

            iRow = this.neuSpread1_Sheet1.RowCount;
            AddNewRow(iRow, false);
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColTradeName1].Text = "ҩζ����";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty1].Text = hsCombo[lastInfo.CombNO].ToString();
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Text = "������";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = lastInfo.Days.ToString();
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Font = new Font("����", 11F, FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColSpeUsage2].Font = new Font("����", 11F, FontStyle.Bold);

            ////���õײ�����
            iRow = this.neuSpread1_Sheet1.RowCount;
            this.AddNewRow(iRow, true);
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = string.Format("�䷽�ˣ�             �˷��ˣ�              ���ϼƣ�{0}            ��ע:{1}", drugListTotalPrice, applyOut.Memo);

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            this.pageCount = hsCombo.Count;
            this.ResumeLayout(true);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            this.Dock = DockStyle.None;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
            print.SetPageSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(15, 10, this);
            }
            else
            {
                print.PrintPage(15, 10, this);
            }

            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// ��ȡֽ��
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillH", dept);
            //����Ӧֽ��
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 800;

                    int curHeight = 0;

                    int addHeight = (this.neuSpread1.ActiveSheet.RowCount - 1) *
                        (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    int additionAddHeight = 180;

                    paperSize.Width = width;
                    paperSize.Height = (addHeight + curHeight + additionAddHeight);

                    this.Height = paperSize.Height;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("����ֽ�ų���>>" + ex.Message);
                }
            }
            if (!string.IsNullOrEmpty(paperSize.Printer) && paperSize.Printer.ToLower() == "default")
            {
                paperSize.Printer = "";
            }
            return paperSize;
        }
        #endregion

        #region ��ҩ��ҩ�������ⷽ��
        /// <summary>
        /// ����з���
        /// </summary>
        /// <param name="iRow">�ڼ���</param>
        /// <param name="isSpan">�Ƿ�ϲ���</param>
        private void AddNewRow(int iRow, bool isSpan)
        {
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            if (isSpan)
            {
                this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = (int)ColSet.ColEnd;
            }
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }

        /// <summary>
        /// ��Ӵ����б�����
        /// </summary>
        /// <param name="iRow"></param>
        private void AddLabelRow(int iRow)
        {
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColDrugNO1].Text = "����";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColTradeName1].Text = "����";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty1].Text = "ÿ����";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColSpeUsage1].Text = "�÷�";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColDrugNO2].Text = "����";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColTradeName2].Text = "����";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColQty2].Text = "ÿ����";
            this.neuSpread1_Sheet1.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = "�÷�";
            this.neuSpread1_Sheet1.Rows[iRow].Height = 20f;
            this.neuSpread1_Sheet1.Rows[iRow].Font = new Font("����", 9f);
        }

        #endregion

        #region ���÷���

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            this.Clear();
            this.SetFormat();
        }

        /// <summary>
        /// �ṩû�з�Χѡ��Ĵ�ӡ
        /// һ���ڰ�ҩ����ʱ����
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        public void PrintData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.PrintPage();
        }

        #endregion

        #region ������

        /// <summary>
        /// ������������
        /// </summary>
        public class CompareApplyOutByCombNO : IComparer
        {
            /// <summary>
            /// ���򷽷�
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = o1.CombNO;          //��������
                string oY = o2.CombNO;          //��������

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }
        #endregion

        #region IInpatientBill ��Ա������ʱ��

        /// <summary>
        /// �ṩ��ҩ��������ʾ�ķ���
        /// һ���ڰ�ҩ������ʱ����
        /// </summary>
        /// <param name="alData">��������applyout</param>
        /// <param name="drugBillClass">��ҩ������</param>
        /// <param name="stockDept">������</param>
        public void ShowData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// �ṩ����ѡ���ӡ��Χ�Ĵ�ӡ����
        /// </summary>
        public void Print()
        {
            this.PrintPage();
        }

        /// <summary>
        /// ����Dock���ԣ�����ʱ��
        /// </summary>
        public DockStyle WinDockStyle
        {
            get
            {
                return this.Dock;
            }
            set
            {
                this.Dock = value;
            }
        }

        /// <summary>
        /// �������ͣ�����ʱ��
        /// </summary>
        public SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.��ҩ;
            }
        }

        #endregion

    }

}
