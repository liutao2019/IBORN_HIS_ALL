using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Inpatient
{
    /// <summary>
    /// [��������: סԺҩ����Ժ��ҩ���������ػ�]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-12]<br></br>
    /// ˵����
    /// </summary>    
    public partial class ucNormalDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        public ucNormalDrugBill()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����������
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region ��ҩ����ͨ�÷���

        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            for (int index = 0; index < this.neuPanel1.Controls.Count; index++)
            {
                this.neuPanel1.Controls[index].Text = "";
            }
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// ��ʼ��Fp
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreNorDrugBill.xml");
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

            this.SuspendLayout();

            #region ��������
            //���ݺ�
            if (drugBillClass.ApplyState != "0")
            {
                this.nlblBillNO.Text = "���ţ�" + drugBillClass.DrugBillNO;// (drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO) + "   ����";
            }
            else
            {
                this.nlblBillNO.Text = "���ţ�" + drugBillClass.DrugBillNO;// (drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO);
            }
            this.nlblRowCount.Text = "��¼����" + alData.Count.ToString();

            //����
            int iRow = 0;

            //�ϼƽ��
            decimal drugListTotalPrice = 0;

            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut(); 

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                this.lbTitle.Text = "סԺ����(" + drugBillClass.Name + ")";
                if (patient == null || string.IsNullOrEmpty(patient.ID))
                {
                    patient = inpatientManager.GetPatientInfoByPatientNO(info.PatientNO);
                    //������
                    this.lbPationNO.Text = "סԺ��ˮ�ţ�" + info.PatientNO;
                    //��������
                    this.lbName.Text = "������" + patient.Name;
                    //����
                    this.nlblPatientDept.Text = "������ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);
                    this.nlblAge.Text = "���䣺" + inpatientManager.GetAge(patient.Birthday);
                    if (!string.IsNullOrEmpty(info.BedNO) && info.BedNO.Length > 4)
                    {
                        this.nlblBedNO.Text = "���ţ�" + info.BedNO.Substring(4);
                    }
                    this.nlblSex.Text = "�Ա�" + patient.Sex.Name;
                }


                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);

                //ҩƷ����
                FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {

                    this.neuSpread1.SetCellValue(0, iRow, "ҩƷ����", item.NameCollection.RegularName);
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, iRow, "ҩƷ����", item.Name);
                }
                //this.neuSpread1.SetCellValue(0, iRow, "ҩƷ����", info.Item.Name);
                this.neuSpread1.SetCellValue(0, iRow, "���", info.Item.Specs);

                //����
                decimal applyQty = info.Operation.ApplyQty;
                string unit = info.Item.MinUnit;
                decimal price = 0m;

                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty), (int)info.Item.PackQty, out outMinQty);
                if (outPackQty == 0)
                {
                    applyQty = info.Operation.ApplyQty;
                    unit = info.Item.MinUnit;
                    price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 4);
                }
                else if (outMinQty == 0)
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit;
                    price = info.Item.PriceCollection.RetailPrice;
                }
                else
                {
                    applyQty = info.Operation.ApplyQty;
                    unit = info.Item.MinUnit;
                    price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 4);
                }
                this.neuSpread1.SetCellValue(0, iRow, "����", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);               
                this.neuSpread1.SetCellValue(0, iRow, "��λ��", itemMgr.GetPlaceNO(info.StockDept.ID, info.Item.ID));
                this.neuSpread1.SetCellValue(0, iRow, "ʹ�÷���", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));

                //��ҩ����
                this.neuSpread1.SetCellValue(0, iRow, "��ҩ����", Common.Function.GetFrequenceName(info.Frequency));

                //ÿ������
                string doseOnce = Common.Function.GetOnceDose(info);
                this.neuSpread1.SetCellValue(0, iRow, "ÿ������", doseOnce);
                //���
                decimal drugTotalPrice = Math.Round(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 6);
                //��ҩ��
                drugListTotalPrice += drugTotalPrice;
                this.neuSpread1.SetCellValue(0, iRow, "���", drugTotalPrice.ToString("F4"));
                //����ҽ��
                this.neuSpread1.SetCellValue(0, iRow, "����ҽ��", SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID));
                //��ҩ˵��
                this.neuSpread1.SetCellValue(0, iRow, "��ҩ˵��", info.Memo);

                iRow++;
            }

            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 10;
            string operName = inpatientManager.Operator.Name;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new Font("����", 10f);
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = string.Format("��ҩ��            �˶ԣ�                       �ϼƽ�{0}  ��ӡʱ�䣺{1}", drugListTotalPrice.ToString(), inpatientManager.GetDateTimeFromSysDateTime().ToString());
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            #endregion

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
                print.PrintPreview(10, 10, this);
            }
            else
            {
                print.PrintPage(10, 10, this);
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
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillN", dept);
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
            this.SetFormat();
            this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreNorDrugBill.xml");
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
            if (alData == null || alData.Count == 0)
            {
                return;
            }
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.PrintPage();
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
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.��Ժ��ҩ����;
            }
        }

        #endregion

    }
}
