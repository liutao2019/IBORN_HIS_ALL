using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Inpatient
{
    /// <summary>
    /// [��������: סԺҩ����Ժ��ҩ���������ػ�]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-12]<br></br>
    /// ˵����
    /// </summary>    
    public partial class ucInpatientRecipe : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        public ucInpatientRecipe()
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
            //for (int index = 0; index < this.neuPanel1.Controls.Count; index++)
            //{
            //    this.neuPanel1.Controls[index].Text = "";
            //}
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
            //this.Clear();
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
                this.nlblBillNO.Text = "������ţ�" + drugBillClass.DrugBillNO;// (drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO) + "   ����";
            }
            else
            {
                this.nlblBillNO.Text = "������ţ�" + drugBillClass.DrugBillNO;// (drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO);
            }
            this.nlblRowCount.Text = "ҩƷƷ������" + alData.Count.ToString();

            //����
            int iRow = 0;

            //�ϼƽ��
            decimal drugListTotalPrice = 0;

            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            FS.SOC.Local.DrugStore.GuangZhou.GYZL.Outpatient.PrintInterfaceImplement diagMgr = new FS.SOC.Local.DrugStore.GuangZhou.GYZL.Outpatient.PrintInterfaceImplement();

            int count = 0;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                count++;
                
                if (patient == null || string.IsNullOrEmpty(patient.ID))
                {
                    patient = inpatientManager.QueryPatientInfoByInpatientNO(info.PatientNO);
                    //������
                    this.lbPationNO.Text = "סԺ��ˮ�ţ�" + patient.PID.PatientNO;
                    //��������
                    this.lbName.Text = "������" + patient.Name;
                    //����
                    this.nlblPatientDept.Text = "������ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(string.IsNullOrEmpty(drugBillClass.ApplyDept.ID) ? info.ApplyDept.ID : drugBillClass.ApplyDept.ID);
                    this.nlblAge.Text = "���䣺" + inpatientManager.GetAge(patient.Birthday);
                    if (!string.IsNullOrEmpty(info.BedNO) && info.BedNO.Length > 4)
                    {
                        this.nlblBedNO.Text = "���ţ�" + info.BedNO.Substring(4);
                    }
                    this.nlblSex.Text = "�Ա�" + patient.Sex.Name;

                    string diagName = FS.SOC.Local.DrugStore.GuangZhou.Common.Function.GetInpatientDiagnose(patient.ID);
                    if (string.IsNullOrEmpty(diagName))
                    {
                        diagName = diagMgr.GetDiagnose(patient.ID);
                    }

                    this.lblDiagnose.Text = "�ٴ���ϣ�" + diagName;
                    this.lblFeeDate.Text = "�������ڣ�"+info.Operation.ApplyOper.OperTime.ToShortDateString();
                    this.lblAddress.Text = "�绰/סַ��" +patient.PhoneHome+" / "+ patient.AddressHome;
                    this.lblDoctorName.Text = "����ҽ����" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID) + "(" + info.RecipeInfo.ID + ")";
                    this.lblPactUnit.Text = "��ͬ��λ��" + patient.Pact.Name;
                    this.lblPrintDate.Text = "��ӡʱ�䣺" + inpatientManager.GetDateTimeFromSysDateTime().ToString();
                    this.lblDoctorName1.Text = "ҽ    ʦ:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID) + "(" + info.RecipeInfo.ID + ")";
                }



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


                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                

                this.neuSpread1.SetCellValue(0, iRow, "���",count.ToString()+"��");
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
                this.neuSpread1.SetCellValue(0, iRow, "���", info.Item.Specs + "   �� " + applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);
iRow++;
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);



                this.neuSpread1.SetCellValue(0, iRow, "���", "");

                this.neuSpread1.SetCellValue(0, iRow, "ҩƷ����", "Sig�� " + SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID) + "   " + Common.Function.GetOnceDose(info) + "    " + Common.Function.GetFrequenceName(info.Frequency));
               
                this.neuSpread1.SetCellValue(0, iRow, "���","");// Common.Function.GetFrequenceName(info.Frequency));

                iRow++;
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1_Sheet1.Rows[iRow].Height = 11;

                //���
                decimal drugTotalPrice = Math.Round(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2);
                //��ҩ��
                drugListTotalPrice += drugTotalPrice;
                

                iRow++;
            }

            this.lblCost.Text = "ҩƷ��"+drugListTotalPrice.ToString();
          
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            #endregion

            this.ResumeLayout(true);
        }


        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {

            //���û��ϴ�ӡ�����ӡ
            FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();


            FS.HISFC.Models.Base.PageSize pageSize = this.GetPaperSize();
           
              

            //��ӡ�߾ദ����ά�����ϱ߾���Ϊ�±߾࣬��������ҳβ��ӡ�Ŀհף���֤������ȫ��ӡ
            print.DrawingMargins = new System.Drawing.Printing.Margins(20, 0, 0, 50);

            //ֽ�Ŵ���
            print.PaperName = pageSize.Name;
            print.PaperHeight = pageSize.Height;
            print.PaperWidth = pageSize.Width;

            //��ӡ������
            print.PrinterName = pageSize.Printer;

            //ҳ����ʾ
            this.lblPageNO.Tag = "ҳ�룺{0}/{1}";
            print.PageNOControl = this.lblPageNO;

            //ҳü�ؼ�����ʾÿҳ����ӡ
            print.HeaderControls.Add(this.neuPanel1);
            //ҳ�ſؼ�����ʾÿҳ����ӡ
            print.FooterControls.Add(this.neuPanel3);
            print.IsAutoMoveFooter = false;
            print.IsAutoMoveFooters = false;

            //����ʾҳ��ѡ��
            print.IsShowPageNOChooseDialog = false;
            this.neuLabel10.BringToFront();
            //this.SetUI();
            
            //����Աʹ��Ԥ������
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPageView(this);
            }
            else
            {
                print.PrintPage(this);
            }

        }

        /// <summary>
        /// ��ȡֽ��
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            return new FS.HISFC.Models.Base.PageSize("InPatientDrugBillN", 850, 1100);

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
            //this.Clear();
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
            this.Clear();
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
