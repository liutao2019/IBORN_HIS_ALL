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
    /// [��������: סԺҩ����ϸ����ӡ���ػ�ʵ��]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-12]<br></br>
    /// ˵����
    /// 1����Ϊһ�����ӱ�����������Ҫ����
    /// 2������Ŀ����޸Ĳ���Ļ������Կ��Ǽ̳з�ʽ
    /// </summary>
    public partial class ucDetailDrugBillIBORN : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// ��ϸ��ӡ��ҩ��
        /// </summary>
        public ucDetailDrugBillIBORN()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        #region ����

        /// <summary>
        /// ÿҳ������������ǰ���LetterpageRowNum���и߸ı�Ӱ���ҳ
        /// </summary>
        int pageRowNum = 14;

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

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();
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

            int lblTitleNameLocalX = this.DrawingMargins.Left + this.lblTitleName.Location.X;
            int lblTitleNameTitleLoaclY = this.DrawingMargins.Top + this.lblTitleName.Location.Y;
            graphics.DrawString(this.lblTitleName.Text, this.lblTitleName.Font, new SolidBrush(this.lblTitleName.ForeColor), lblTitleNameLocalX, lblTitleNameTitleLoaclY);

            if (this.nlbReprint.Visible)
            {
                int nlbReprintLocalX = this.DrawingMargins.Left + this.nlbReprint.Location.X;
                int nlbReprintLoaclY = this.DrawingMargins.Top + this.nlbReprint.Location.Y;
                graphics.DrawString(this.nlbReprint.Text, this.nlbReprint.Font, new SolidBrush(this.nlbReprint.ForeColor), nlbReprintLocalX, nlbReprintLoaclY);
            }

            int additionTitleLocalX = this.DrawingMargins.Left + this.nlbNurseCell.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.nlbNurseCell.Location.Y;

            graphics.DrawString(this.nlbNurseCell.Text, this.nlbNurseCell.Font, new SolidBrush(this.nlbNurseCell.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbBillNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbBillNO.Location.Y;

            graphics.DrawString(this.nlbBillNO.Text, this.nlbBillNO.Font, new SolidBrush(this.nlbBillNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblOrderDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOrderDate.Location.Y;

            graphics.DrawString(this.lblOrderDate.Text, this.lblOrderDate.Font, new SolidBrush(this.lblOrderDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;

            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint����
            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 1100 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

            #endregion

            #region ҳ�����

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPageNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPageNo.Location.Y;

            graphics.DrawString("ҳ�룺" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.nlbPageNo.Font, new SolidBrush(this.nlbPageNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

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

        /// <summary>
        /// ������
        /// </summary>
        //private void SetFormat()
        //{
        //    this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");

        //}

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
            //��Ԫ������

            #region farpoint����
            FarPoint.Win.LineBorder noneBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder noRightBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder noBottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, false);

            #endregion


            string applyDeptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            string sendType = this.GetSendType(alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut);

            if (drugBillClass.ID == "R")
            {
                this.lblTitleName.Text = "��ҩ��(��ϸ)";
            }
            else if (drugBillClass.ID == "O")
            {
                this.lblTitleName.Text = "��Ժ��ҩ(��ϸ)";
            }
            else if (drugBillClass.ID == "L" || drugBillClass.ID == "T" || drugBillClass.ID == "P" || drugBillClass.ID == "TL" || drugBillClass.ID == "A")// {F417D766-19C0-4d3e-AB72-D774058B497E}
            {
                this.lblTitleName.Text = "���/����ҩ��ҩ��(��ϸ)";
            }
            else
            {
                this.lblTitleName.Text = drugBillClass.Name + "(��ϸ)";//+ "(" + sendType + ")";
            }

            bool isPO = false;
            if (drugBillClass.Name.Contains("�ڷ�"))
            {
                isPO = true;
            }
            if (isPO)
            {
                this.neuSpread1_Sheet1.Columns.Get(8).Label = "��ҩƵ��";

            }
            else
            {
                //this.neuSpread1_Sheet1.Columns.Get(5).Label = "����(Ԫ)";
            }
            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width) / 2, this.nlbTitle.Location.Y);
            this.lblTitleName.Location = new Point((this.Width - this.lblTitleName.Width) / 2, this.lblTitleName.Location.Y);

            this.nlbBillNO.Text = "��ҩ���ţ�" + drugBillClass.DrugBillNO;
            this.nlbNurseCell.Text = "������" + applyDeptName;

            //#region ��ͬһҽ������ҩʱ�������ʾ

            if (drugBillClass.ID != "O" && drugBillClass.ID != "P")
            {
                try
                {
                    alData.Sort(new CompareApplyOutByOrderNO());
                }
                catch { }
            }


            #region ����������


            #endregion

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();

            string orderId = "";//����ð�ҽ����ˮ������ 

            #region ��������

            int count = 0;

            Hashtable hsApplyInfo = new Hashtable();

            ArrayList difPatient = new ArrayList();

            ArrayList allOrderDate = new ArrayList();

            ArrayList patientData = new ArrayList();
            //�����߷���������Ϣ
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {
                if (hsApplyInfo.Contains(applyInfo.PatientNO))
                {
                    ArrayList alPatientData = hsApplyInfo[applyInfo.PatientNO] as ArrayList;
                    alPatientData.Add(applyInfo);
                }
                else
                {
                    ArrayList alPatientData = new ArrayList();
                    alPatientData.Add(applyInfo);
                    hsApplyInfo.Add(applyInfo.PatientNO, alPatientData);
                    difPatient.Add(applyInfo.PatientNO);
                }

                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                order = orderMgr.QueryOneOrder(applyInfo.OrderNO);
                if (!allOrderDate.Contains(applyInfo.UseTime))
                {
                    allOrderDate.Add(applyInfo.UseTime);
                }
            }

            //����
            int iRow = 0;
            difPatient.Sort(new CompareStringByPatientNO());
            foreach (string patintid in difPatient)
            {
                patientData = hsApplyInfo[patintid] as ArrayList;

                System.Collections.Hashtable hsTotData = new Hashtable();

                ArrayList newAllData = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in patientData)
                {
                    //{DDFC27A3-159F-4558-96D7-55BB812E44E4}
                    if (hsTotData.Contains(applyOut.OrderNO + applyOut.UseTime.ToShortDateString()))
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = hsTotData[applyOut.OrderNO + applyOut.UseTime.ToShortDateString()] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        applyOutTot.Operation.ApplyQty += applyOut.Operation.ApplyQty;
                        applyOutTot.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(applyOut.UseTime), dt) + "," + applyOutTot.User03;
                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = applyOut.Clone();
                        applyOutTot.User01 = FS.FrameWork.Function.NConvert.ToInt32(!FS.SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(applyOutTot.StockDept.ID, applyOutTot.Item.ID)).ToString();
                        applyOutTot.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(applyOut.UseTime), dt);
                        hsTotData.Add(applyOut.OrderNO + applyOut.UseTime.ToShortDateString(), applyOutTot);
                        newAllData.Add(applyOutTot);
                    }
                }


                this.SuspendLayout();

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

                //��������
                string privPatientName = "";
                string samePatient = "";

                string patientInfo = "";

                if (drugBillClass.ID != "O" && drugBillClass.ID != "P")
                {
                    try
                    {
                        CompareApplyOutByOrderNO com1 = new CompareApplyOutByOrderNO();
                        newAllData.Sort(com1);
                    }
                    catch { };
                }
                DateTime dtFirstPrintTime = DateTime.MinValue;
                int id = 0;

                count += newAllData.Count;

                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in newAllData)
                {
                    FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                    item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                    if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
                    {
                        this.nlbReprint.Visible = true;
                    }
                    else
                    {
                        this.nlbReprint.Visible = false;
                    }
                    dtFirstPrintTime = info.Operation.ExamOper.OperTime;
                    string bedNO = info.BedNO;
                    if (bedNO.Length > 4)
                    {
                        bedNO = bedNO.Substring(4);
                    }


                    if (drugBillClass.ID == "R")
                    {

                        if (p != null && p.PVisit.InState.ID.ToString() == "2")
                        {
                            bedNO = "*" + bedNO;
                        }
                    }
                    //���߲�ͬʱ��Ҫ����һ�л�����Ϣ
                    if (samePatient != info.PatientNO)
                    {
                        string age = "";
                        try
                        {
                            age = inpatientManager.GetAge(inpatientManager.GetPatientInfoByPatientNO(info.PatientNO).Birthday);
                        }
                        catch { }
                        p = inpatientManager.GetPatientInfoByPatientNO(info.PatientNO);
                        privPatientName = info.PatientName;
                        id = 0;
                        samePatient = info.PatientNO;
                        patientInfo = string.Format("���ţ�{0}  ������{1}  סԺ�ţ�{2}  ���䣺{3}", bedNO, FS.SOC.Public.String.PadRight(privPatientName, 8, ' '), p.PID.PatientNO.TrimStart('0'), age);
                        //��������ڱ�ҳ�ĵ�һ�У���Ҫ������Ϣ
                        if (iRow % this.pageRowNum != 0)
                        {
                            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noBottomBorder;
                            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
                            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                            iRow++;
                        }
                    }

                    //ÿҳ�ĵ�һ�ж���Ҫ������Ϣ
                    if (iRow % this.pageRowNum == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(iRow, 1);

                        this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noBottomBorder;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        iRow++;

                    }
                    //ҩƷ��Ϣ
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    string valueable = string.Empty;
                    string morethandoseonce = string.Empty;
                    if (info.OrderType.ID == "CZ")
                    {
                        valueable = "+";
                    }
                    else
                    {
                        valueable = "";
                    }
                    if (!FS.FrameWork.Function.NConvert.ToBoolean(info.User01))
                    {
                        valueable += "��";
                    }

                    int times = info.User03.Split(',').Length;

                    if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).OnceDose > info.DoseOnce)
                    {
                        //morethandoseonce = "��";
                    }

                    FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                    if (string.IsNullOrEmpty(info.PlaceNO))
                    {
                        info.PlaceNO = itemMgr.GetPlaceNO(info.Clone().StockDept.ID, info.Clone().Item.ID).ToString();//���������ID,��ĿID

                        // {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                    }
                    id += 1;
                    this.neuSpread1.SetCellValue(0, iRow, "���", id.ToString());
                    //this.neuSpread1_Sheet1.Cells.Get(iRow, 0).Border = noneBorder;
                    this.neuSpread1.SetCellValue(0, iRow, "��λ��", info.PlaceNO);
                    //{83A75FE2-89DE-4b11-9BCB-6092837FE537}
                    //����ҽ����ϸ��ҩ��������ÿ�μ���
                    this.neuSpread1.SetCellValue(0, iRow, "ÿ����", info.DoseOnce + info.Item.DoseUnit);
                    this.neuSpread1.SetCellValue(0, iRow, "����", valueable + info.Item.Name);
                    this.neuSpread1.SetCellValue(0, iRow, "���", info.Item.Specs);
                    this.neuSpread1.SetCellValue(0, iRow, "����˶�", "");
                    this.neuSpread1.SetCellValue(0, iRow, "��ҩ�˶�", "");

                    string memo = "";
                    if (!string.IsNullOrEmpty(item.Memo))
                    {
                        memo += "[" + item.Memo + "]";
                    }
                    memo += string.IsNullOrEmpty(info.Memo) ? "" : "��" + info.Memo + "��";
                    this.neuSpread1.SetCellValue(0, iRow, "��ע", memo);
                    //����

                    //this.neuSpread1_Sheet1.Cells[iRow, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
                    //this.neuSpread1_Sheet1.Cells[iRow, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;


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

                    if (info.OrderType.ID == "CZ" && FS.SOC.HISFC.BizProcess.Cache.Order.GetFrequency(info.Frequency.ID).Times.Length != times)
                    {
                        //unit = unit + "��";
                    }
                    this.neuSpread1.SetCellValue(0, iRow, "������", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + " " + unit);


                    if (isPO)
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "��ҩƵ��", info.Frequency.ID + "(" + info.User03 + ")");


                        if (info.Frequency.Name == "���")
                        {
                            this.neuSpread1.SetCellValue(0, iRow, "��ҩƵ��", "��" + info.Frequency.ID.ToLower() + "(" + info.User03 + ")");
                        }

                    }
                    else
                    {
                        //this.neuSpread1.SetCellValue(0, iRow, "����(Ԫ)", price.ToString("F4").TrimEnd('0').TrimEnd('.'));

                        this.neuSpread1.SetCellValue(0, iRow, "��ҩƵ��", info.Frequency.ID + "(" + info.User03 + ")");


                        if (info.Frequency.Name == "���")
                        {
                            this.neuSpread1.SetCellValue(0, iRow, "��ҩƵ��", "��" + info.Frequency.ID.ToLower() + "(" + info.User03 + ")");
                        }

                    }

                    //for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
                    //{
                    //    this.neuSpread1_Sheet1.Cells.Get(iRow, i).Border = noneBorder;
                    //}

                    this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 1].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 2].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 3].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 4].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Border = noBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Border = noBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 9].Border = noBottomBorder;
                    iRow++;
                }


            }

            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            allOrderDate.Sort(new CompareOrderDate());
            DateTime startTime = new DateTime();
            DateTime enTime = new DateTime();
            if (allOrderDate.Count > 0)
            {
                startTime = DateTime.Parse(allOrderDate[0].ToString());

                enTime = DateTime.Parse(allOrderDate[allOrderDate.Count - 1].ToString());
            }
            this.lblOrderDate.Text = "ҽ��ʱ�䣺" + startTime.ToShortDateString() + " �� " + enTime.ToShortDateString();

            this.nlbRowCount.Text = "��¼����" + count.ToString();

            this.neuSpread1_Sheet1.Columns.Get(0).SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Ascending;
            #region ���õײ�����
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    int index = this.neuSpread1_Sheet1.Rows.Count;
                    totPageNO = (int)Math.Ceiling((double)index / pageRowNum);
                    for (int page = totPageNO; page > 0; page--)
                    {
                        if (page == totPageNO)
                        {

                            //this.neuSpread1_Sheet1.AddRows(index, 1);
                            //this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                            //this.neuSpread1_Sheet1.Cells[index, 0].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 0].Text = "һʽ�������ٰ���ҩ�� �ں�����ʿ �ۻ�������";
                            //this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("����", 7f);
                            //this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;



                            this.neuSpread1_Sheet1.AddRows(index, 1);
                            //��ӡ���ײ�����


                            //{5B0E232F-CA85-4591-AE00-59E1C0A51B62}
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                            this.neuSpread1_Sheet1.Cells[index, 0].Border = topBorder;
                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "                                                              һʽ�������ٰ���ҩ�� �ں�����ʿ �ۻ������� ��ҽ�����/������ҩʦ��           ��ҽ���˶�/����ҩҩʦ��           ȡҩ�ˣ�               �����ˣ�";
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("����", 10f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                            this.neuSpread1_Sheet1.Rows[index].Tag = page;
                            continue;
                        }

                        //���ҳ�룬����ѡ��ҳ��ʱ��
                        this.neuSpread1_Sheet1.Rows[index].Tag = page;
                    }
                }
                this.neuSpread1_Sheet1.Rows.Get(0).Border = topBorder;

            #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
            //this.ResumeLayout(true);
        }

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

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize paperSize1 = pageSizeMgr.GetPageSize("InPatientDrugBillD");
                paperSize = new System.Drawing.Printing.PaperSize("InPatientDrugBillD", paperSize1.Width, paperSize1.Height);
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
            int drawingHeight = 1100 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;

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

        #region ��ϸ�������ⷽ��

        /// <summary>
        /// ��ȡƵ�δ����ÿ�����
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            return 1000;

            //��ׯ������
            if (string.IsNullOrEmpty(frequencyID))
            {
                return 1000;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//ÿ��һ��
            {
                return 1;
            }
            else if (id == "bid")//ÿ������
            {
                return 2;
            }
            else if (id == "tid")//ÿ������
            {
                return 3;
            }
            else if (id == "hs")//˯ǰ
            {
                return 1;
            }
            else if (id == "qn")//ÿ��һ��
            {
                return 1;
            }
            else if (id == "qid")//ÿ���Ĵ�
            {
                return 4;
            }
            else if (id == "pcd")//��ͺ�
            {
                return 1;
            }
            else if (id == "pcl")//��ͺ�
            {
                return 1;
            }
            else if (id == "pcm")//��ͺ�
            {
                return 1;
            }
            else if (id == "prn")//��Ҫʱ����
            {
                return 1;
            }
            else if (id == "��ҽ��")
            {
                return 1;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                return 100;
            }
        }

        /// <summary>
        /// ����ҩʱ��/��ǰʱ�� �����ʾ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        private string FormatDateTime(DateTime dt, DateTime sysdate)
        {
            int hours = dt.Hour;// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
            string hour = "";
            if (hours >= 0 && hours < 12)
            {
                hour = hours + "a";
            }
            else if (hours == 12)
            {
                hour = hours + "p";
            }
            else
            {
                hour = (hours - 12) + "p";
            }
            if (false)
            {
                try
                {
                    if (sysdate.Date.AddDays(-1) == dt.Date)
                    {
                        return "��" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else if (sysdate.Date == dt.Date)
                    {
                        return "��" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else if (sysdate.Date.AddDays(1) == dt.Date)
                    {
                        return "��" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else if (sysdate.Date.AddDays(2) == dt.Date)
                    {
                        return "��" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        if (dt.Month == sysdate.Month)
                        {
                            return dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');

                        }
                    }
                }
                catch
                {
                    return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
                }
            }
            return hour;
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
        //    this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");
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
            this.PrintPage();
        }

        #endregion

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        private class CompareApplyOutByPatient : IComparer
        {
            /// <summary>
            /// ���򷽷�
            /// </summary>
            //public int Compare(object x, object y)
            //{
            //    FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
            //    FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

            //    string oX = "";          //��������
            //    string oY = "";          //��������


            //    oX = o1.BedNO + o1.PatientName + this.GetFrequencySortNO(o1.Frequency) + o1.UseTime.ToString();
            //    oY = o2.BedNO + o2.PatientName + this.GetFrequencySortNO(o2.Frequency) + o2.UseTime.ToString(); 

            //    return string.Compare(oX, oY);
            //}
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //��������
                string oY = "";          //��������

                //FZC ADD ��������ǰ��
                #region

                string oo1 = string.Empty;
                string oo2 = string.Empty;
                if (o1.OrderType.ID == "CZ")
                {
                    oo1 = "2";
                }
                else
                {
                    oo1 = "1";
                }

                if (o2.OrderType.ID == "LZ")
                {
                    oo2 = "1";
                }
                else
                {
                    oo2 = "2";
                }

                #endregion

                oX = o1.BedNO + o1.PatientName + oo1 + o1.User01 + this.GetFrequencySortNO(o1.Frequency) + this.GetOrderNo(o1) + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + oo2 + o2.User01 + this.GetFrequencySortNO(o2.Frequency) + this.GetOrderNo(o2) + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }

            private string GetOrderNo(FS.HISFC.Models.Pharmacy.ApplyOut app)
            {
                string id = app.Item.ID.ToString();
                return id;
            }
            private string GetFrequencySortNO(FS.HISFC.Models.Order.Frequency f)
            {
                string id = f.ID.ToLower();
                string sortNO = "";
                if (id == "qd")
                {
                    sortNO = "1";
                }
                else if (id == "bid")
                {
                    sortNO = "2";
                }
                else if (id == "tid")
                {
                    sortNO = "3";
                }
                else
                {
                    sortNO = "4";
                }
                if (f.Name == "���")
                {
                    sortNO = "9999" + sortNO;
                }
                else
                {
                    sortNO = "0000" + sortNO;
                }
                return sortNO;
            }

        }

        /// <summary>
        /// ������
        /// </summary>
        private class CompareApplyOutByOrderNO : IComparer
        {
            /// <summary>
            /// ���򷽷�
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = new FS.HISFC.Models.Pharmacy.ApplyOut();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = new FS.HISFC.Models.Pharmacy.ApplyOut();
                if ((x is FS.HISFC.Models.Pharmacy.ApplyOut) && (y is FS.HISFC.Models.Pharmacy.ApplyOut))
                {
                    o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                    o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                }
                string oX = "1";          //��������
                string oY = "1";          //��������

                //FZC ADD ��������ǰ��
                #region

                string oo1 = string.Empty;
                string oo2 = string.Empty;
                if (o1.OrderType != null && o1.OrderType.ID == "CZ")
                {
                    oo1 = "2";
                }
                else
                {
                    oo1 = "1";
                }

                if (o1.OrderType != null && o2.OrderType.ID == "LZ")
                {
                    oo2 = "1";
                }
                else
                {
                    oo2 = "2";
                }

                #endregion

                oX = o1.BedNO + o1.PatientName + oo1 + o1.OrderNO + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + oo2 + o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }
        }

        private class CompareApplyOutByValuableOrderNO : IComparer
        {
            /// <summary>
            /// ���򷽷�
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //��������
                string oY = "";          //��������


                oX = o1.User01 + o1.OrderNO + o1.UseTime.ToString();
                oY = o2.User01 + o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
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
            this.Clear();
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// �ṩ����ѡ���ӡ��Χ�Ĵ�ӡ����
        /// </summary>
        public void Print()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPage();

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
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.��ϸ;
            }
        }

        #endregion
    }
    /// <summary>
    /// ������
    /// </summary>
    public class CompareOrderDate : IComparer
    {
        /// <summary>
        /// ���򷽷�
        /// </summary>
        public int Compare(object x, object y)
        {
            DateTime o1 = DateTime.Parse(x.ToString());
            DateTime o2 = DateTime.Parse(y.ToString());

            DateTime oX = new DateTime();          //��������
            DateTime oY = new DateTime();          //��������


            oX = o1;
            oY = o2;

            return DateTime.Compare(oX, oY);
        }
    }
}
