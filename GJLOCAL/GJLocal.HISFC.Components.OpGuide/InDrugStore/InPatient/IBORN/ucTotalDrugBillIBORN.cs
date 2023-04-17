using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN
{
    /// <summary>
    /// [��������: סԺҩ�����ܵ���ӡ���ػ�ʵ��]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-12]<br></br>
    /// ˵����
    /// 1����Ϊһ�����ӱ�����������Ҫ����
    /// 2������Ŀ����޸Ĳ���Ļ������Կ��Ǽ̳з�ʽ
    /// </summary>
    public partial class ucTotalDrugBillIBORN : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucTotalDrugBillIBORN()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
        }

        #region ����

        /// <summary>
        /// ÿҳ������������ǰ���LetterpageRowNum���и߸ı�Ӱ���ҳ
        /// {D5919440-5685-4fa8-B86B-4BD57D0901DE}
        /// </summary>
        int pageRowNum = 10;

        /// <summary>
        /// ���ҳ��
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// ��ӡ����Ч����,��ѡ��ҳ�뷶Χʱ��Ч
        /// </summary>
        int validRowNum = 0;

        /// <summary>
        /// ��ǰ��ӡҳ��ҳ��
        /// �����Զ������
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// ���δ�ӡ���ҳ��
        /// �����Զ������
        /// </summary>
        private int maxPageNO = 1;

        /// <summary>
        /// ��ӡ��
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
        
        #endregion

        #region ��ҩ����ͨ�÷���

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            this.nlbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.nlbRowCount.Text = "��¼����";
            this.nlbBillNO.Text = "��ҩ���ţ�";
            this.lblOrderDate.Text = "ҽ��ʱ�䣺";
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// Fp����
        /// </summary>
        //private void SetFormat()
        //{
        //    this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreTotDrugBill.xml");
        //}

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetSendType(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string sendType = string.Empty;
            switch (applyOut.SendType)
            {
                case 1:
                    sendType = "����";
                    break;
                case 2:
                    sendType = "��ʱ";
                    break;
                case 4:
                    sendType = "����";
                    break;
            }
            return sendType;
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="alData">��������applyout�������ǰ���ҩƷ��������˵�</param>
        /// <param name="drugBillClass"></param>
        private void ShowBillTotData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            #region farpoint����
           
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            textCellType4.Multiline = true;
            textCellType4.WordWrap = true;FarPoint.Win.LineBorder noneBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder noRightBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder noBottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, false);


            FarPoint.Win.LineBorder LeftTopBottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, true);
            FarPoint.Win.LineBorder LeftTopRightBottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
            
            
            #endregion
            

            this.SuspendLayout();
            string applyDeptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            string sendType = this.GetSendType(alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
            if (drugBillClass.ID == "L" || drugBillClass.ID == "T" || drugBillClass.ID == "P" || drugBillClass.ID == "TL" || drugBillClass.ID == "A")// {F417D766-19C0-4d3e-AB72-D774058B497E}
            {
                this.lblTitleName.Text = "���/����ҩ��ҩ��(����)";
            }
            else
            {
                this.lblTitleName.Text = drugBillClass.Name + "(����)";

                if (drugBillClass.ID == "S") { 
               IBORN.ucTotalAnestheticDrugBill ucTotalAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucTotalAnestheticDrugBill();
               ucTotalAnestheticDrugBill.Init();
               ucTotalAnestheticDrugBill.PrintData(alData, drugBillClass, stockDept);
                
                return ;
                }
            }

            this.nlbRowCount.Text = "��¼����" + alData.Count.ToString();
            this.nlbBillNO.Text = "��ҩ���ţ�" + drugBillClass.DrugBillNO;
            //this.nlbStockDept.Text = "��ҩ���ң�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);
            this.nlbNurseCell.Text = "������" + applyDeptName;
            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            decimal totCost = 0m;
            ArrayList allOrderDate = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                order = orderMgr.QueryOneOrder(info.OrderNO);
                if (info.UseTime == DateTime.MinValue)
                {
                    if (!allOrderDate.Contains(info.Operation.ApplyOper.OperTime))
                    {
                        allOrderDate.Add(info.Operation.ApplyOper.OperTime);
                    }
                }
                else
                {
                    if (!allOrderDate.Contains(info.UseTime))
                    {
                        allOrderDate.Add(info.UseTime);
                    }
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
                {
                    this.nlbReprint.Visible = true;
                }
                else
                {
                    this.nlbReprint.Visible = false;
                }
                info.PlaceNO = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID, info.Item.ID);
                info.User01 = FS.FrameWork.Function.NConvert.ToInt32(!FS.SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(info.StockDept.ID, info.Item.ID)).ToString();
            }

            //if (drugBillClass.ID == "T")
            //{
            //    alData.Sort(new CompareApplyOutSpecs());
            //}
            //else
            //{ 
            //  alData.Sort(new CompareApplyOut());
            //}
            alData.Sort(new CompareApplyOutSpecs());
            int iRow = 0;
            DateTime dtFirstPrintTime = DateTime.MinValue;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                string valueable = string.Empty;
                if(!FS.FrameWork.Function.NConvert.ToBoolean(info.User01))
                {
                    valueable = "��";
                }

                FS.SOC.HISFC.BizLogic.Pharmacy.InOut item = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                if (string.IsNullOrEmpty(info.PlaceNO))
                {
                    info.PlaceNO = item.GetPlaceNO(info.Clone().StockDept.ID, info.Clone().Item.ID).ToString();//���������ID,��ĿID
                }
                dtFirstPrintTime = info.Operation.ExamOper.OperTime;
                #region ���ݸ�ֵ
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                
                this.neuSpread1_Sheet1.Rows.Get(iRow).CellType = textCellType4;
                this.neuSpread1_Sheet1.Rows.Get(iRow).Font = new Font("����", 10f);

                this.neuSpread1.SetCellValue(0,iRow,"���",(iRow + 1).ToString());
                this.neuSpread1.SetCellValue(0, iRow, "�Զ�����", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                this.neuSpread1.SetCellValue(0, iRow, "����", valueable + info.Item.Name);
                this.neuSpread1.SetCellValue(0, iRow, "����˶�", "");
                this.neuSpread1.SetCellValue(0, iRow, "��ҩ�˶�", "");
                this.neuSpread1.SetCellValue(0, iRow, "���", "  " + info.Item.Specs);
                this.neuSpread1.SetCellValue(0, iRow, "��λ��", info.PlaceNO);
                FS.HISFC.Models.Pharmacy.Item itemInfo = new FS.HISFC.Models.Pharmacy.Item();
                itemInfo = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                //{E2613CC6-9F59-48b2-9299-D3814C6254A7}
                if (info.Memo == "")
                {
                    this.neuSpread1.SetCellValue(0, iRow, "��ע", itemInfo.Memo );
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, iRow, "��ע", itemInfo.Memo + "/ҽ����ע��" + info.Memo);
                }
                totCost += (info.Item.PriceCollection.RetailPrice * (info.Operation.ApplyQty / info.Item.PackQty));

                decimal applyQty = info.Operation.ApplyQty;
                string unit = info.Item.MinUnit;
                decimal price = info.Item.PriceCollection.RetailPrice;


                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
                if (string.IsNullOrEmpty(info.Item.PackUnit))
                {
                    if (info.Item.PackQty == 1)
                    {
                        info.Item.PackUnit = info.Item.MinUnit;
                    }
                    else
                    {
                        try 
                        {
                            info.Item.PackUnit = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).PackUnit;
                        }
                        catch { }
                    }
                }
                if (outPackQty == 0)
                {
                    applyQty = info.Operation.ApplyQty;
                    unit = info.Item.MinUnit;
                    price = info.Item.PriceCollection.RetailPrice / info.Item.PackQty;
                }
                else if (outMinQty == 0)
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit;
                }
                else
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit + outMinQty.ToString() + info.Item.MinUnit;
                }

                this.neuSpread1.SetCellValue(0, iRow, "����", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);
                this.neuSpread1.SetCellValue(0, iRow, "����", price.ToString("F4").TrimEnd('0').TrimEnd('.'));

                //{D5919440-5685-4fa8-B86B-4BD57D0901DE}
                if ((iRow + 1) % 10 == 0)
                {
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 1].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 2].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 3].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 4].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Border = LeftTopBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Border = LeftTopRightBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 9].Border = LeftTopRightBottomBorder;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 1].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 2].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 3].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 4].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Border = noBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 9].Border = LeftTopRightBottomBorder;
                }
                #endregion

                iRow++;
            }

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Border = LeftTopBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Border = LeftTopRightBottomBorder;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Border = LeftTopRightBottomBorder;

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            allOrderDate.Sort(new CompareOrderDate());
            DateTime startTime = new DateTime();
            DateTime enTime = new DateTime();
            if (allOrderDate.Count > 0)
            {
                startTime = DateTime.Parse(allOrderDate[0].ToString());

                enTime = DateTime.Parse(allOrderDate[allOrderDate.Count - 1].ToString());
            }
            this.lblOrderDate.Text = "ҽ��ʱ�䣺" + startTime.ToShortDateString() + " �� " + enTime.ToShortDateString();
            if (this.lblOrderDate.Text.Contains("0001-01-01"))
            {

            }
            
            #region ҳ�뼰������Ϣ�ȴ���

            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    int index = this.neuSpread1_Sheet1.Rows.Count;
                    //ҳ��
                    totPageNO = (int)Math.Ceiling((double)index / pageRowNum);
                    //�����һҳ��ʼ�������ҳ��
                    for (int pageNO = totPageNO; pageNO > 0; pageNO--)
                    {
                        //���һҳ
                        if (pageNO == totPageNO)
                        {
                            //this.neuSpread1_Sheet1.AddRows(index, 1);
                            ////��ӡ���ײ�����
                            //this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count; ;
                            ////string sumTot = string.Empty;
                            ////sumTot = totCost.ToString("F2");
                            
                            //this.neuSpread1_Sheet1.Cells[index, 0].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 0].Text = "���/����ҩʦ��                        �˶�/��ҩҩʦ��                        ȡҩ�ˣ�";
                            //this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("����", 10f);
                            //this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                           
                            //���ҳ�룬����ѡ��ҳ��ʱ��
                            //this.neuSpread1_Sheet1.Rows[index].Tag = pageNO;
                            continue;
                        }
                    }

                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //SetSheetStyle();

            //this.ResumeLayout(true);

            #endregion
        }

        /// <summary>
        /// ��ҩ����ʽ��ӡ
        /// ���������ʾ�ؼ�
        /// </summary>
        /// <param name="alData">applyout��������ʵ������</param>
        /// <param name="drugBillClass">��ҩ������</param>
        /// <param name="stockDept">ʵ������</param>
        private void ShowBillData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            //�������
            this.Clear();

            //����������ã���ϣ�����
            Hashtable hsTotData = new Hashtable();
            System.Collections.ArrayList alTotData = new ArrayList();

            alData.Sort(new CompareStringByPatientNO());
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                //������Ϣ
                if (hsTotData.Contains(applyOut.Item.ID))
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = hsTotData[applyOut.Item.ID] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    applyOutTot.Operation.ApplyQty += applyOut.Operation.ApplyQty;
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = applyOut.Clone();
                    hsTotData.Add(applyOutTot.Item.ID, applyOutTot);
                    alTotData.Add(applyOutTot);
                }
            }

            this.ShowBillTotData(alTotData, drugBillClass, stockDept);

            //���ñ���λ��
            this.ResetTitleLocation(false);
        }

        /// <summary>
        /// ��ȡֽ��
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            int height = 0;
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = null; //= pageSizeMgr.GetPageSize("InPatientDrugBillT", "ALL");

            //����Ӧֽ�ţ�ע���ӡ����������������ȷ��
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 870;

                    int curHeight = 0;

                    int addHeight = this.validRowNum * (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    int additionAddHeight = 120;

                    paperSize.Width = width;
                    paperSize.Height = (addHeight + curHeight + additionAddHeight);

                    //this.Height = paperSize.Height;
                    int ifMore = paperSize.Height % 550;
                    if (ifMore == 0)
                    {
                        height = paperSize.Height;
                    }
                    else
                    {
                        int pageNum = paperSize.Height / 550;
                        height = (pageNum + 1) * 550;
                    }
                    paperSize.Height = height;
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

        ///// <summary>
        ///// ��ӡ
        ///// </summary>
        //private void PrintPage()
        //{
        //    FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

        //    this.Dock = DockStyle.None;

        //    FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
        //    print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

        //    print.SetPageSize(paperSize);

        //    //����Ա����Ԥ��������鿴����
        //    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
        //    {
        //        print.PrintPreview(10, 10, this);
        //    }
        //    else
        //    {
        //        print.PrintPage(10, 10, this);
        //    }

        //    this.Dock = DockStyle.Fill;
        //}

        #endregion

        #region ���ܵ������ⷽ��

        /// <summary>
        /// ����FarPoint����ʽ
        /// </summary>
        //private void SetSheetStyle()
        //{

        //    FarPoint.Win.ComplexBorder bevelBorder1 = new FarPoint.Win.ComplexBorder(FarPoint.Win.ComplexBorderSide.Empty, FarPoint.Win.ComplexBorderSide.Empty, FarPoint.Win.ComplexBorderSide.Empty, new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.HairLine));
        //    FarPoint.Win.BevelBorder lineBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, false, false, true);
        //    FarPoint.Win.BevelBorder lineBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, false, false, true);

        //    for (int i = 0, j = this.neuSpread1_Sheet1.Rows.Count - 1; i < j; i++)
        //    {
        //        for (int k = 0; k < 8; k++)
        //        {

        //            this.neuSpread1_Sheet1.Cells.Get(i, k).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        //        }

        //        FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
        //        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 0).Border = lineBorder3;
        //        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        //    }
        //    for (int index = 0; index < this.neuSpread1_Sheet1.Rows.Count - 2; index++)
        //    {
        //        this.neuSpread1_Sheet1.Rows.Get(index).Border = bevelBorder1;
        //    }
        //}

        /// <summary>
        /// �������ñ���λ��
        /// </summary>
        private void ResetTitleLocation(bool isPrint)
        {
            this.nlbTitle.Dock = DockStyle.None;
            this.neuPanel1.Controls.Remove(this.nlbTitle);
            this.neuPanel1.Controls.Remove(this.lblTitleName);

            int with = 0;
            for (int colIndex = 0; colIndex < this.neuSpread1_Sheet1.ColumnCount; colIndex++)
            {
                if (this.neuSpread1_Sheet1.Columns[colIndex].Visible)
                {
                    with += (int)this.neuSpread1_Sheet1.Columns[colIndex].Width;
                }
            }
            if (!isPrint && with > this.neuPanel1.Width)
            {
                with = this.neuPanel1.Width;
            }
            this.nlbTitle.Location = new Point((with - this.nlbTitle.Size.Width) / 2, this.nlbTitle.Location.Y);

            this.lblTitleName.Location = new Point((with - this.lblTitleName.Size.Width) / 2, this.lblTitleName.Location.Y);

            this.neuPanel1.Controls.Add(this.nlbTitle);
            this.neuPanel1.Controls.Add(this.lblTitleName);

        }

        #endregion

        #region ���÷���
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            this.Clear();
            //this.SetFormat();
            //this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            //this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        //void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        //{
        //    this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreTotDrugBill.xml");
        //}

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
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            if (drugBillClass.ID != "S")
            {
                this.PrintPage();
            }
        }

        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        private class CompareApplyOut : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;

                //string oX = o1.PlaceNO;          //��λ��
                //if (oX == null)
                //{
                //    oX = "";
                //}
                //string oY = o2.PlaceNO;          //��λ��
                //if (oY == null)
                //{
                //    oY = "";
                //}
                string oX = o1.User01 + FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o1.Item.ID).NameCollection.RegularSpell.UserCode.ToString();
                if (oX == null)
                {
                    oX = "";
                }
                string oY = o2.User01 + FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o2.Item.ID).NameCollection.RegularSpell.UserCode.ToString();
                if (oY == null)
                {
                    oY = "";
                }

                return string.Compare(oX.ToString(), oY.ToString());
            }

        }

        /// <summary>
        /// ���������
        /// </summary>
        private class CompareApplyOutSpecs : IComparer
        {

            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                 FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;

                string oX = o1.PlaceNO;          //��λ��
                if (oX == null)
                {
                    oX = "";
                }
                string oY = o2.PlaceNO;          //��λ��
                if (oY == null)
                {
                    oY = "";
                }
                //decimal oX = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o1.Item.ID).BaseDose;

                //decimal oY = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o2.Item.ID).BaseDose;

                return oX.CompareTo(oY);
            }

            #endregion
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
            //if (this.totPageNO == 1)
            //{
            //    this.PrintPage();
            //}

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            FS.SOC.Windows.Forms.PrintPageSelectDialog printPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();
            printPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
            printPageSelectDialog.MaxPageNO = this.totPageNO;
            printPageSelectDialog.ShowDialog();

            //��ʼҳ��Ϊ0��˵���û�ȡ����ӡ
            if (printPageSelectDialog.FromPageNO == 0)
            {
                return;
            }

            //��ӡȫ��
            if ((printPageSelectDialog.FromPageNO == 1 && printPageSelectDialog.ToPageNO == this.totPageNO))
            {
                this.PrintPage();
                return;
            }

            //ѡ����ҳ
            int curPageNO = 1;
            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                if (this.neuSpread1_Sheet1.Rows[rowIndex].Tag != null)
                {
                    curPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows[rowIndex].Tag) + 1;
                }
                if (curPageNO >= printPageSelectDialog.FromPageNO && curPageNO <= printPageSelectDialog.ToPageNO)
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = false;
                    this.validRowNum--;
                }
            }

            this.PrintPage();

            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
            }

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            
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
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.����;
            }
        }

        #endregion

        #region ��ӡ����
        void PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            curPageNO = 1;
        }
        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //����ѡ���ӡ��Χ�������
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                this.curPageNO = 1;
                //this.maxPageNO = 1;
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            #region �������
            int mainTitleLocalX = this.DrawingMargins.Left + this.nlbTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.nlbTitle.Location.Y;
            graphics.DrawString(this.nlbTitle.Text, this.nlbTitle.Font, new SolidBrush(this.nlbTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.nlbNurseCell.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.nlbNurseCell.Location.Y;

            graphics.DrawString(this.nlbNurseCell.Text, this.nlbNurseCell.Font, new SolidBrush(this.nlbNurseCell.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbBillNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbBillNO.Location.Y;

            graphics.DrawString(this.nlbBillNO.Text, this.nlbBillNO.Font, new SolidBrush(this.nlbBillNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblTitleName.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblTitleName.Location.Y;

            graphics.DrawString(this.lblTitleName.Text, this.lblTitleName.Font, new SolidBrush(this.lblTitleName.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;

            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblOrderDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOrderDate.Location.Y;

            graphics.DrawString(this.lblOrderDate.Text, this.lblOrderDate.Font, new SolidBrush(this.lblOrderDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            if (this.nlbReprint.Visible)
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.nlbReprint.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.nlbReprint.Location.Y;
                graphics.DrawString(this.nlbReprint.Text, this.nlbReprint.Font, new SolidBrush(this.nlbReprint.ForeColor), additionTitleLocalX, additionTitleLocalY);
            }
            #endregion

            #region Farpoint����
            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            //{D5919440-5685-4fa8-B86B-4BD57D0901DE}
            //int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            int drawingHeight = (int)this.neuSpread1_Sheet1.Rows.Default.Height * pageRowNum + (int)this.neuSpread1_Sheet1.RowHeader.Rows.Default.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

            #endregion

            #region ҳ�����

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPageNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top  + this.nlbPageNo.Location.Y;

            graphics.DrawString("ҳ�룺" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.nlbPageNo.Font, new SolidBrush(this.nlbPageNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion


            //{D5919440-5685-4fa8-B86B-4BD57D0901DE}
            #region ҳβ����

            int spreadHeight = 0;
            if (this.neuSpread1_Sheet1.Rows.Count >= curPageNO * this.pageRowNum)
            {
                spreadHeight = (int)this.neuSpread1_Sheet1.Rows.Default.Height * pageRowNum + (int)this.neuSpread1_Sheet1.RowHeader.Rows.Default.Height;
            }
            else 
            {
                int temp = this.neuSpread1_Sheet1.Rows.Count % pageRowNum;
                spreadHeight = (int)this.neuSpread1_Sheet1.Rows.Default.Height * temp + (int)this.neuSpread1_Sheet1.RowHeader.Rows.Default.Height;
            }

            //{5B0E232F-CA85-4591-AE00-59E1C0A51B62}

            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel1.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuPanel1.Height + spreadHeight + this.neuLabel1.Location.Y;

            string neuPanel1text = "��ҽ�����/������ҩʦ��";
            if (this.lblTitleName.Text.Contains("��ҩ"))
            {
                neuPanel1text = "����ҩʦ��";
            }

            graphics.DrawString(neuPanel1text, new Font("����", 10f), new SolidBrush(this.neuLabel1.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel2.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuPanel1.Height + spreadHeight + this.neuLabel2.Location.Y;

            string neuPanel2text = "��ҽ���˶�/����ҩҩʦ��";
            if (this.lblTitleName.Text.Contains("��ҩ"))
            {
                neuPanel2text = "�˶�ҩʦ��";
            }

            graphics.DrawString(neuPanel2text, new Font("����", 10f), new SolidBrush(this.neuLabel2.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel3.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuPanel1.Height + spreadHeight + this.neuLabel3.Location.Y;
            graphics.DrawString("ȡҩ�ˣ�", new Font("����", 10f), new SolidBrush(this.neuLabel3.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel4.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuPanel1.Height + spreadHeight + this.neuLabel3.Location.Y;

            string neuPanel4text = "�����ˣ�";
            if (this.lblTitleName.Text.Contains("��ҩ"))
            {
                neuPanel4text = "�˻��ˣ�";
            }
            graphics.DrawString(neuPanel4text, new Font("����", 10f), new SolidBrush(this.neuLabel4.ForeColor), additionTitleLocalX, additionTitleLocalY);

            //
            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel5.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuPanel1.Height + spreadHeight + this.neuLabel5.Location.Y;
            graphics.DrawString("һʽ�������ٰ���ҩ�� �ں�����ʿ �ۻ�������", new Font("����", 7f), new SolidBrush(this.neuLabel5.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion 

            #region ��ҳ
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                //maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                //{DDFC27A3-159F-4558-96D7-55BB812E44E4}
                FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize paperSize1 = pageSizeMgr.GetPageSize("InPatientDrugBillT");
                paperSize = new System.Drawing.Printing.PaperSize("InPatientDrugBillT", paperSize1.Width, paperSize1.Height);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        private void myPrintView()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ������" + ex.Message);
            }
        }

        /// <summary>
        /// ��ӡҳ��ѡ��
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            //{D5919440-5685-4fa8-B86B-4BD57D0901DE}
            //int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            int drawingHeight = (int)this.neuSpread1_Sheet1.Rows.Default.Height * pageRowNum + (int)this.neuSpread1_Sheet1.RowHeader.Rows.Default.Height;

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = false;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuSpread1_Sheet1.RowHeader.Visible;
            this.neuSpread1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuSpread1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0);

            socPrintPageSelectDialog.MaxPageNO = this.maxPageNO;
            if (this.maxPageNO > 1)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            System.Drawing.Printing.PaperSize paperSize = null;
            this.SetPaperSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintView();
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.SetPaperSize(paperSize);
                    this.PrintDocument.Print();
                }
            }

        }
        #endregion

    }

    public class SortByValueAble : IComparer
    {

        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            if ((x is FS.HISFC.Models.Pharmacy.ApplyOut) && (y is FS.HISFC.Models.Pharmacy.ApplyOut))
            {
                return (x as FS.HISFC.Models.Pharmacy.ApplyOut).User01.CompareTo((y as FS.HISFC.Models.Pharmacy.ApplyOut).User01);
            }
            return 1;
        }

        #endregion
    }
}
