using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.InPatient
{
    /// <summary>
    /// [��������: סԺҩ����ҩ��ҩ�����ػ�]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2011-02]<br></br>
    /// ˵����
    /// </summary>
    public partial class ucHerbalDrugBillPrintByPatient : UserControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucHerbalDrugBillPrintByPatient()
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

        /// <summary>
        /// סԺ���߹�����
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        #endregion

        #region ��ҩ����ͨ�÷���

        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            this.nlblBillNO.Text = "���ţ�";
            this.nlbStockDeptName.Text = string.Empty;
            this.neuSpread1_Sheet1.Rows.Count = 0;
            
        }

        /// <summary>
        /// ��ʵ��û�����壬�ͻ��ܵ�ͳһ����
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        private void ShowBillData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept,int totPage)
        {
            this.Clear();
            this.ShowDetailData(alData, drugBillClass, stockDept,totPage);
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept,int totPage)
        {
            this.SuspendLayout();

            #region ����

            if (alData.Count%pageCount == 0)
            {
                totPage = (int)(alData.Count / pageCount);
            }
            else
            {
                totPage = (int)(alData.Count / pageCount) + 1;
            }

            //�������
            string applyDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).ApplyDept.ID);

            this.nlbNurseCellName.Text = applyDeptName + "           ";

            this.nlbStockDeptName.Text = "��ҩ���ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).StockDept.ID);

            this.nlbOperName.Text = "����Ա��" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).Operation.ExamOper.ID);

            if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            {
                this.nlbReprint.Visible = false;
                if (!this.lbTitle.Text.Contains("����"))
                {
                    this.lbTitle.Text = this.nlbReprint.Text + this.lbTitle.Text;
                }
            }
            else
            {
                this.nlbReprint.Visible = false;
            }

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
        

            System.Collections.Hashtable hsCombo = new Hashtable();

            FS.HISFC.Models.Pharmacy.ApplyOut lastInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

            ArrayList allPatient = new ArrayList();

            int index = 0;

            decimal curPatientTotCost = 0m;

            DateTime dtDrugedDate = new DateTime();

            int pageNo = 1;


            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                //this.lbInsureDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);
                if (!allPatient.Contains(info.PatientNO))
                {
                    dtDrugedDate = info.Operation.ExamOper.OperTime;

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.GetPatientInfoByPatientNO(info.PatientNO);

                    if (allPatient.Count != 0)
                    {
                        this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

                        this.nlbPrintDate.Text = "��ӡʱ�䣺" + DateTime.Now.ToString();

                        this.nlbDrugDate.Text = "��ҩʱ�䣺" + dtDrugedDate.ToString();

                        this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

                        SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

                        this.nlbPageNO.Text = "��" + pageNo + "ҳ��" + "��" + totPage + "ҳ";
                        this.SetPanelBottomVisible((pageNo == totPage));
                        this.PrintPage();
                        pageNo++;
                    }

                    this.nlblBillNO.Text = "���ţ�" + drugBillClass.DrugBillNO;

                    curPatientTotCost = 0m;

                    #region ������Ϣ
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = 3;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "  " + patientInfo.Name + "     " + inPatientMgr.GetAge(patientInfo.Birthday);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                    allPatient.Add(info.PatientNO);
                    index++;
                    if (index == 12)
                    {
                        this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

                        this.nlbPrintDate.Text = "��ӡʱ�䣺" + DateTime.Now.ToString();

                        this.nlbDrugDate.Text = "��ҩʱ�䣺" + dtDrugedDate.ToString();

                        this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

                        SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

                        this.nlbPageNO.Text = "��" + pageNo + "ҳ��" + "��" + totPage + "ҳ";

                        this.SetPanelBottomVisible((pageNo == totPage));

                        this.PrintPage();
                        this.neuSpread1_Sheet1.Rows.Count = 0;
                        pageNo++;
                    }
                    #endregion
                  
                }

                #region ��ֵ��ʾ

                if (index == 13)
                {
                    this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

                    this.nlbPrintDate.Text = "��ӡʱ�䣺" + DateTime.Now.ToString();

                    this.nlbDrugDate.Text = "��ҩʱ�䣺" + dtDrugedDate.ToString();

                    this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

                    SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

                    this.nlbPageNO.Text = "��" + pageNo + "ҳ��" + "��" + totPage + "ҳ";
                    this.SetPanelBottomVisible((pageNo == totPage));

                    this.PrintPage();
                    this.neuSpread1_Sheet1.Rows.Count = 0;
                    pageNo++;
                }

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = index.ToString();

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text = info.Item.Name;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = info.Item.Specs;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = info.DoseOnce + info.Item.DoseUnit;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Text = info.Frequency.ID;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = info.Operation.ApplyQty + info.Item.MinUnit;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = (info.Item.PriceCollection.RetailPrice / info.Item.PackQty).ToString("F2");

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = ((info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty).ToString("F2");

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = info.CombNO;

                curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ApplyQty;

                this.nlbDays.Text = "������" + info.Days;

                index++;
                #endregion
               
            }
            #endregion


          

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

            this.nlbPrintDate.Text = "��ӡʱ�䣺" + DateTime.Now.ToString();

            this.nlbDrugDate.Text = "��ҩʱ�䣺" + dtDrugedDate.ToString();

            this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

            this.nlbPageNO.Text = "��" + pageNo + "ҳ��" + "��" + totPage + "ҳ";
            this.SetPanelBottomVisible((pageNo == totPage));
            this.PrintPage();
            this.neuSpread1_Sheet1.Rows.Count = 0;

            this.ResumeLayout(true);
        }

        private void SetPanelBottomVisible(bool  isVilible)
        {
            foreach (Control c in this.panel3.Controls)
            {
                if (c is Label)
                {
                    c.Visible = isVilible;
                }
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            this.Dock = DockStyle.None;

            this.SuspendLayout();
            this.panel2.Dock = DockStyle.None;
            this.panel3.Dock = DockStyle.None;
            this.panel2.Height = (int)(this.neuSpread1.Sheets[0].RowHeader.Rows[0].Height + this.neuSpread1_Sheet1.Rows.Count * this.neuSpread1_Sheet1.Rows.Default.Height + 5);
            this.panel3.Location = new Point(0, this.panel1.Height + this.panel2.Height);
            this.ResumeLayout();

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
            print.SetPageSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }

            this.panel2.Height = 373;

            this.Dock = DockStyle.Fill;
        }


        /// <summary>
        /// ��ȡֽ��
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillH", "ALL");
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
            #region ����ҳ��

            int totPage = 0;
            ArrayList allPatient = new ArrayList();
            Hashtable hsPatientData = new Hashtable();
            int index = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (!allPatient.Contains(info.PatientNO))
                {
                    index = 1;
                    totPage++;
                    ArrayList allPatientData = new ArrayList();
                    allPatient.Add(info.PatientNO);
                    allPatientData.Add(info);
                    hsPatientData.Add(info.PatientNO, allPatientData);
                }
                else
                {
                    ArrayList allPatientData = hsPatientData[info.PatientNO] as ArrayList;
                    allPatientData.Add(info);
                }
                index++;

                if (index == 12)
                {
                    index = 1;
                    totPage++;
                }
            }
            #endregion
           
            this.ShowBillData(alData, drugBillClass, stockDept,totPage);
          
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

                string oX = o1.OrderNO +  o1.CombNO;          //��������
                string oY = o2.OrderNO +  o2.CombNO;          //��������

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
        public void ShowData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept,int totPage)
        {
            this.ShowBillData(alData, drugBillClass, stockDept,totPage);
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
