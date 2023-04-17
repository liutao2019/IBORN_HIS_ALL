using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using FS.FrameWork.Management;
using System.Collections;
//using FS.HISFC.BizLogic.Pharmacy;
//using FS.HISFC.Models.Pharmacy;
//using FS.HISFC.Models.HealthRecord.EnumServer;

namespace FS.SOC.Local.DrugStore.Example.Outpatient
{
    public partial class ucDrugList : UserControl
    {
        /// <summary>
        /// [��������: ����ҩ����ӡ���ػ�ʵ��]<br></br>
        /// [�� �� ��: cube]<br></br>
        /// [����ʱ��: 2011-1]<br></br>
        /// ˵����
        /// 1����ǿ�ķ�ҳ����ҳ����ʾ����
        /// 2��ֱ�۵�FarPoint�����ã����Ը��������Ƹ�Cell��ֵ
        /// </summary>>
        public ucDrugList()
        {
            InitializeComponent();
        }

        private void PrintAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string hospitalName)
        {
            
            decimal totCost = 0;
            int rowIndex = 0;
            this.Clear();
            
            SetLableValue(drugRecipe, hospitalName);
            this.neuSpread1_Sheet1.RowCount = al.Count + 1;
            
            foreach ( FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                //�Զ�����
                this.neuSpread1.SetCellValue(0,rowIndex, "����", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                //ҩƷ��
                this.neuSpread1.SetCellValue(0, rowIndex, "ҩƷ����", info.Item.Name);
               
                this.neuSpread1.SetCellValue(0,rowIndex, "���", info.Item.SpecialFlag1 + info.Item.Specs);
                //����
                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
                if (outPackQty == 0)
                {
                    this.neuSpread1.SetCellValue(0,rowIndex, "����", ((int)(info.Operation.ApplyQty * info.Days)).ToString("0.00"));
                    //��λ
                    this.neuSpread1.SetCellValue(0,rowIndex, "��λ", info.Item.MinUnit);
                }
                else if (outMinQty == 0)
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "����", outPackQty.ToString("0.00"));
                    //��λ
                    this.neuSpread1.SetCellValue(0, rowIndex, "��λ", info.Item.PackUnit);
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "����", ((int)(info.Operation.ApplyQty * info.Days)).ToString("0.00"));
                    //��λ
                    this.neuSpread1.SetCellValue(0, rowIndex, "��λ", info.Item.MinUnit);
                }

                //����
                this.neuSpread1.SetCellValue(0, rowIndex, "����", ((decimal)(info.Item.PriceCollection.RetailPrice / info.Item.PackQty)).ToString("0.00"));
                //���
                this.neuSpread1.SetCellValue(0, rowIndex, "���", ((decimal)((info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * (info.Operation.ApplyQty * info.Days))).ToString("0.00"));
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((info.Item.PriceCollection.RetailPrice * (info.Operation.ApplyQty * info.Days / info.Item.PackQty)).ToString("F2"));
                
                //�÷�
                this.neuSpread1.SetCellValue(0, rowIndex, "�÷�", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
                //ÿ������
                if (info.DoseOnce == 0)
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "ÿ������", "��ҽ��");
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "ÿ������", info.DoseOnce.ToString() + info.Item.DoseUnit);
                }

                if (!string.IsNullOrEmpty(info.Frequency.ID))
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "Ƶ��", info.Frequency.ID.ToLower());
                }
                this.neuSpread1.SetCellValue(0, rowIndex, "��ע", "��");

                rowIndex++;
            }

            this.SetTotInfo(totCost, drugRecipe.SendTerminal.Name);

            try
            {
                //��ͣ����ӡ����ͷ��ֽ��̫����̫�񶼿���������ͣ���ָ���ӡ
                FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
            }
            catch { }

            //this.PaperName = "OutPatientDrugBill";
            //this.PageHeight = 1100/3;
            //this.PageWith = 860;
            //this.IsPrintPageBottom = true;
            //this.DrawingMargins = new System.Drawing.Printing.Margins(0, 0, 0, 10);

            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    this.PrintView();
            //}
            //else
            //{
            //    this.PrintPage();
            //}
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
            lbRecipe.Text = "������:";
            lbDoctor.Text = "ҽʦ:";

            this.neuSpread1_Sheet1.RowCount = 0;
        }      

        /// <summary>
        /// ����Lable��ֵ
        /// </summary>
        /// <param name="info"></param>
        private void SetLableValue(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string hospitalName)
        {

            this.neuPanName.Text = hospitalName + "ҩƷ�嵥";
            //����
            lbName.Text = "������" + drugRecipe.PatientName;
            //������
            this.lbCardNo.Text = "�������ţ�" + drugRecipe.CardNO;
            //��Ʊ��
            lbInvoice.Text = "��Ʊ�ţ�" + drugRecipe.InvoiceNO;
            //�Ա�
            lbSex.Text = "�Ա�" + drugRecipe.Sex.Name;
            //����
            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
            lbAge.Text = "���䣺" + db.GetAge(drugRecipe.Age);
            //ҽʦ
            this.lbDoctor.Text = "ҽʦ��" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            //������
            this.lbRecipe.Text = "�����ţ�" + drugRecipe.RecipeNO;
            //��������
            lbDeptName.Text = "�������ƣ�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
          
            //��������
            lbRecipeDate.Text = "�������ڣ�" + FS.FrameWork.Function.NConvert.ToDateTime(drugRecipe.FeeOper.OperTime).ToString("yyyy-MM-dd");
           
        }    

        /// <summary>
        /// �������һ�л�����Ϣ
        /// </summary>
        private void SetTotInfo(decimal totCost, string sendWindows)
        {
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount-1, 0, 1, this.neuSpread1_Sheet1.ColumnCount);
            try
            {
                this.neuSpread1_Sheet1.SetText(this.neuSpread1_Sheet1.RowCount - 1, 0, string.Format("��ҩ�ۣ���{0}                  {1}",
                           new object[] { totCost.ToString("0.00"), sendWindows }));
            }
            catch { }
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }       

        /// <summary>
        /// ��ӡ��ҩ�嵥
        /// </summary>
        /// <param name="alData">��������ʵ��</param>
        /// <param name="diagnose">���</param>
        /// <param name="drugRecipe">����������Ϣ</param>
        /// <param name="drugTerminal">�ն���Ϣ</param>
        /// <returns></returns>
        public int PrintDrugBill(ArrayList alData, string diagnose, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal,string hospitalName)
        {
            if (alData == null || drugRecipe == null)
            {
                return -1;
            }
            
            //this.npbBarCode.Image = this.CreateBarCode(drugRecipe.RecipeNO);
            this.PrintAllData(alData, drugRecipe, hospitalName);

            return 0;
        }

       
    }
}
