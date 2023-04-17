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
    /// [��������: סԺҩ����ҩ��ҩ�����ػ�]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2011-02]<br></br>
    /// ˵����
    /// </summary>
    public partial class ucHerbalDrugBillIBORNOLD : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucHerbalDrugBillIBORNOLD()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ÿҳ��ӡ����
        /// </summary>
        private int pageCount = 12;

        /// <summary>
        /// ���ҳ��
        /// </summary>
        int totPageNO = 0;

        private ArrayList allDataTmp = new ArrayList();

        private FS.FrameWork.Models.NeuObject stockDeptTmp = new FS.FrameWork.Models.NeuObject();

        private FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClassTmp = new FS.HISFC.Models.Pharmacy.DrugBillClass();


        /// <summary>
        /// סԺ���߹�����
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
        #endregion

        #region ��ҩ����ͨ�÷���

        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            this.nlblBillNO.Text = "��ҩ���ţ�";
            //this.nlbStockDeptName.Text = string.Empty;
            this.neuSpread1_Sheet1.Rows.Count = 0;
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
            this.allDataTmp = alData.Clone() as ArrayList;
            this.drugBillClassTmp = drugBillClass.Clone();
            this.stockDeptTmp = stockDept.Clone();
            this.ShowDetailData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.SuspendLayout();

            #region ����

            if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            {
                this.nlbReprint.Visible = true;
            }
            else
            {
                this.nlbReprint.Visible = false;
            }
            this.lbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.lblTitleName.Text = "��ҩ��Ƭ��ҩ��(��ϸ)";
            string applyDeptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).ApplyDept.ID);

            this.nlbNurseCellName.Text = applyDeptName + "           ";
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();

            textCellType2.Multiline = true;
            textCellType2.WordWrap = true;

            //this.nlbStockDeptName.Text = "��ҩ���ң�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).StockDept.ID);

            this.nlbOperName.Text = "����Ա��" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).Operation.ExamOper.ID);

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

            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, true, false);
            
            #region ��������

            //���ݺ�
            this.nlblBillNO.Text = "��ҩ���ţ�" + drugBillClass.DrugBillNO;

            System.Collections.Hashtable hsCombo = new Hashtable();

            FS.HISFC.Models.Pharmacy.ApplyOut lastInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

            ArrayList allPatient = new ArrayList();

            Dictionary<string, List<FS.HISFC.Models.Pharmacy.ApplyOut>> printInfo = new Dictionary<string, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
            
            int index = 0;

            decimal totCost = 0m;

            decimal curPatientTotCost = 0m;

            DateTime dtDrugedDate = new DateTime();

            string show = "";

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (printInfo.ContainsKey(info.PatientNO))
                {
                    printInfo[info.PatientNO].Add(info);
                }
                else
                {
                     List<FS.HISFC.Models.Pharmacy.ApplyOut> list = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                     list.Add(info);
                     printInfo.Add(info.PatientNO, list);          
                }
            }
            totCost = 0m;

            ArrayList allOrderDate = new ArrayList();
            decimal herbalQty = 1;

            foreach (string patientNO in printInfo.Keys)
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.GetPatientInfoByPatientNO(patientNO);
                string bedNO = patientInfo.PVisit.PatientLocation.Bed.ID;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                string info1 = string.Format("���ţ�{0}  ������{1}  סԺ�ţ�{2}  ���䣺{3}", bedNO, patientInfo.Name, patientInfo.PID.PatientNO, inPatientMgr.GetAge(patientInfo.Birthday));
                
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = info1;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                List<FS.HISFC.Models.Pharmacy.ApplyOut> list = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                foreach(FS.HISFC.Models.Pharmacy.ApplyOut info in printInfo[patientNO]) 
                {
                    list.Add(info);
                }
                curPatientTotCost = 0m;
                int rowCount = (int)Math.Ceiling(list.Count / (double)3);
                index = 0;
                for (int i = 0; i < rowCount; i++)
                {
                    if (list.Count >= i * 3 + 1)
                    {
                        index++; 
                        FS.HISFC.Models.Pharmacy.ApplyOut info = list[i * 3 + 0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        info.PlaceNO = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID, info.Item.ID);

                        FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                        order = orderMgr.QueryOneOrder(info.OrderNO);
                        if (!allOrderDate.Contains(info.UseTime))
                        {
                            allOrderDate.Add(info.UseTime);
                        }
                        herbalQty = info.Days;
                        show = "      �÷���" + info.Usage.Name + "��" + info.Frequency.Name;
                        string memo = string.IsNullOrEmpty(info.Memo) ? "" : "��" + info.Memo + "��";
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = index.ToString() + ".";

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text = info.Item.Name + memo;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].CellType = textCellType2;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].Text = info.DoseOnce + info.Item.DoseUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = info.Operation.ExamQty + info.Item.MinUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = info.PlaceNO;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Border = topBorder;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                        totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                    }
                    if (list.Count >= i * 3 + 2)
                    {
                        index++;
                        FS.HISFC.Models.Pharmacy.ApplyOut info = list[i * 3 + 1] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        info.PlaceNO = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID, info.Item.ID);

                        FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                        order = orderMgr.QueryOneOrder(info.OrderNO);
                        if (!allOrderDate.Contains(info.UseTime))
                        {
                            allOrderDate.Add(info.UseTime);
                        }
                        string memo = string.IsNullOrEmpty(info.Memo) ? "" : "��" + info.Memo + "��";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Text = index.ToString() + ".";

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = info.Item.Name + memo;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].CellType = textCellType2;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = info.DoseOnce + info.Item.DoseUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = info.Operation.ExamQty + info.Item.MinUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = info.PlaceNO;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Border = topBorder;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        
                        curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                        totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;

                    }
                    if (list.Count >= i * 3 + 3)
                    {
                        index++; 
                        FS.HISFC.Models.Pharmacy.ApplyOut info = list[i * 3 + 2] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        info.PlaceNO = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID, info.Item.ID);

                        FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                        order = orderMgr.QueryOneOrder(info.OrderNO);
                        if (!allOrderDate.Contains(info.UseTime))
                        {
                            allOrderDate.Add(info.UseTime);
                        }
                        string memo = string.IsNullOrEmpty(info.Memo) ? "" : "��" + info.Memo + "��";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 10].Text = index.ToString() + ".";

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 11].Text = info.Item.Name + memo;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 11].CellType = textCellType2;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 12].Text = info.DoseOnce + info.Item.DoseUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 13].Text = info.Operation.ExamQty + info.Item.MinUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 14].Text = info.PlaceNO;

                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 14].Border = topBorder;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                        totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                    }

                }
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = "�ϼƽ�" + curPatientTotCost.ToString("F2") + show + "      ��" + herbalQty + "��";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("����", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
               

            }
            #region ����
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            //{
            //    days = info.Days.ToString();
            //    if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            //    {
            //        this.nlbReprint.Visible = false;
            //        if (!this.lbTitle.Text.Contains("����"))
            //        {
            //            this.lbTitle.Text = this.nlbReprint.Text + this.lbTitle.Text;
            //        }
            //    }
            //    else
            //    {
            //        this.nlbReprint.Visible = false;
            //    }
            //    //this.lbInsureDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);
            //    if (!allPatient.Contains(info.PatientNO))
            //    {
            //        dtDrugedDate = info.Operation.ExamOper.OperTime;

            //        FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.GetPatientInfoByPatientNO(info.PatientNO);

            //        if (allPatient.Count != 0)
            //        {
            //            #region ���Ӻϼ�
	
            //            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = "�ϼƽ�" + curPatientTotCost.ToString("F2");

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            //            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count - 3;

            //            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = "�� " + info.Operation.ExamQty + "��";

            //            #endregion
            //        }

            //        curPatientTotCost = 0m;

            //        #region ������Ϣ
            //        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = 3;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "  " + patientInfo.Name + "     " + inPatientMgr.GetAge(patientInfo.Birthday);

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            //        allPatient.Add(info.PatientNO);
            //        index++;
            //        #endregion
                  
            //    }
            //    FS.SOC.HISFC.BizLogic.Pharmacy.InOut item = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            //    if (string.IsNullOrEmpty(info.PlaceNO))
            //    {
            //        info.PlaceNO = item.GetPlaceNO(info.Clone().StockDept.ID, info.Clone().Item.ID).ToString();//���������ID,��ĿID
            //    }
            //    #region ��ֵ��ʾ
            //    if (index % 2 == 1)
            //    {
            //        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = index.ToString() + ".";

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text = info.Item.Name;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].Text = info.DoseOnce + info.Item.DoseUnit;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = info.Operation.ExamQty + info.Item.MinUnit;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = info.PlaceNO;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Border = topBorder;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                    
            //    }
            //    else
            //    {
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Text = index.ToString() + ".";

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = info.Item.Name;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = info.DoseOnce + info.Item.DoseUnit;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = info.Operation.ExamQty + info.Item.MinUnit;
                    
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = info.PlaceNO;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            //    }

            //    curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
            //    totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
            //    index++;
            //    #endregion

                
               
            //}
            //#endregion

            //#region ���Ӻϼ�

            //this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = 3;
            
            // this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = "�ϼƽ�" + curPatientTotCost.ToString("F2");

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;


            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count - 3;


            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = "�� " + days + " ��";
            //#endregion
        
            #endregion
           
            this.nlbTotCost.Text = totCost.ToString("F2");
            allOrderDate.Sort(new CompareOrderDate());
            DateTime startTime = new DateTime();
            DateTime enTime = new DateTime();
            if (allOrderDate.Count > 0)
            {
                startTime = DateTime.Parse(allOrderDate[0].ToString());

                enTime = DateTime.Parse(allOrderDate[allOrderDate.Count - 1].ToString());
            }
            this.lblOrderDate.Text = "ҽ��ʱ�䣺" + startTime.ToShortDateString() + " �� " + enTime.ToShortDateString();
            
            this.nlbPrintDate.Text = "��ӡʱ�䣺" + DateTime.Now.ToString();

            //this.nlbDrugDate.Text = "��ҩʱ�䣺";//+dtDrugedDate.ToString();nlbDrugDate

            //this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            //FS.SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 7, 2);

            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();

            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            #endregion
            this.ResumeLayout(true);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void PrintPage()
        {
            this.Dock = DockStyle.None;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = new FS.HISFC.Models.Base.PageSize(string.Empty, 860, 1100);
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
                    int width = 850;

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


        #region ���÷���

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            this.Clear();
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
            this.ShowData(allDataTmp, drugBillClassTmp, stockDeptTmp);
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
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.��ҩ;
            }
        }

        #endregion

    }

}
