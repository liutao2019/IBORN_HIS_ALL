using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// ��ϸ��ӡ��ҩ��
    /// 
    /// <����˵��>
    ///     1������Function�о�̬������ɼ��͵ĸ��� �ù�������
    /// </����˵��>
    /// </summary>
    public partial class ucDrugBillDetail : UserControl
    {
        /// <summary>
        /// ��ϸ��ӡ��ҩ��
        /// </summary>
        public ucDrugBillDetail()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitFp();
            }
        }

        private static System.Collections.Hashtable hsDept = new Hashtable();

        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        //�Ƿ񲹴�
        private string ifBPrint;

        public string IfBPrint
        {
            get
            {
                return ifBPrint;
            }
            set
            {
                ifBPrint = value;
            }

        }

        /// <summary>
        /// ���Ի���
        /// </summary>
        private System.Collections.Hashtable hsPatint = new Hashtable();

        /// <summary>
        /// Fp����
        /// </summary>
        private void InitFp()
        {
            //this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
           // this.neuSpread1_Sheet1.Columns[4].Visible = false;

            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 30;

            this.setColums();            
        }

        /// <summary>
        /// ������
        /// </summary>
        private void setColums()
        {
            try
            {
                FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
                FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
                FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
                FarPoint.Win.Spread.CellType.TextCellType texttype = new FarPoint.Win.Spread.CellType.TextCellType();//{8CA1AEE7-F038-4c32-BD3E-ECCC8DFE687B}
               texttype.WordWrap=true;
                texttype.Multiline=true;
                //������
                this.neuSpread1_Sheet1.ColumnCount = (int)ColumnsSet.ColEnd;

                //������,���뷽ʽ
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColPatienName).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColTradeName).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColSpecs).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColUsage).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColFrequency).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColNum).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColUnit).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColExeTime).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.ColumnHeader.Columns.Get((int)ColumnsSet.ColPlace).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColPatienName).Label = "[����]����";

                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColTradeName).Label = "�� Ʒ �� ��";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColTradeName).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColTradeName).CellType = texttype;//{8CA1AEE7-F038-4c32-BD3E-ECCC8DFE687B}
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColSpecs).Label = "���";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColSpecs).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColUsage).Label = "�÷�";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColDoseOnce).Label = "ÿ����";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColBaseDose).Label = "����";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColFrequency).Label = "Ƶ��";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColDays).Label = "����";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColNum).Label = "����";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColUnit).Label = "��λ";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColExeTime).Label = "��ҩʱ��";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColRetailPrice).Label = "����";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColCost).Label = "���";
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColPlace).Label = "��λ��";

                //���������� ��� ����
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColPatienName).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColPatienName).Width = 106F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColTradeName).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColTradeName).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColTradeName).Width = 220F;

                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColSpecs).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColSpecs).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColSpecs).Width = 100F;

                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColUsage).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColUsage).Width = 49F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColDoseOnce).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColDoseOnce).Width = 48F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColBaseDose).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColBaseDose).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColFrequency).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColFrequency).Width = 36F;
                numberCellType3.DecimalPlaces = 0;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColDays).CellType = numberCellType3;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColDays).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColDays).Width = 34F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColNum).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColNum).Width = 51F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColNum).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColUnit).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColUnit).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColUnit).Width = 46F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColExeTime).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColExeTime).Width = 100F;
                numberCellType4.DecimalPlaces = 4;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColRetailPrice).CellType = numberCellType4;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColRetailPrice).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColRetailPrice).Width = 59F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColPlace).Width = 62F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColumnsSet.ColPlace).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //�пɼ���
                this.neuSpread1_Sheet1.Columns[(int)ColumnsSet.ColBaseDose].Visible = false;//��������
                this.neuSpread1_Sheet1.Columns[(int)ColumnsSet.ColDays].Visible = false;//����
                this.neuSpread1_Sheet1.Columns[(int)ColumnsSet.ColDoseOnce].Visible = false;//ÿ������
                //this.neuSpread1_Sheet1.Columns[(int)ColumnsSet.ColNum].Visible = false;//����
                this.neuSpread1_Sheet1.Columns[(int)ColumnsSet.ColRetailPrice].Visible = false;//���ۼ�
                this.neuSpread1_Sheet1.Columns[(int)ColumnsSet.ColCost].Visible = false;//���

                //��ͷ��������
                this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
                this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
          
            }
            catch (Exception ex)
            {
                MessageBox.Show("����������setColums()ʧ��>>" + ex.Message);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        public void ShowData(ArrayList alData,FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            
            if (ucDrugBillDetail.hsDept.Count == 0)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                foreach (FS.HISFC.Models.Base.Department dept in alDept)
                {
                    ucDrugBillDetail.hsDept.Add(dept.ID, dept.Name);
                }
            }

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            this.lbPrintTime.Text = "��ӡʱ��:" + dataManager.GetDateTimeFromSysDateTime().ToString();

            //this.lbPrintTime.Text = "��ӡʱ��:" + drugBillClass.Oper.OperTime.ToString();

            #region ��ͬһҽ������ҩʱ�������ʾ

            DateTime dt = dataManager.GetDateTimeFromSysDateTime();

            string orderId = "";//����ð�ҽ����ˮ������ 
            FS.HISFC.Models.Pharmacy.ApplyOut objLast = null;
            //�ϲ�
            for (int i = alData.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = (alData[i] as FS.HISFC.Models.Pharmacy.ApplyOut);                                
                    if (orderId == "")
                    {
                        orderId = obj.OrderNO;
                        objLast = obj;
                        objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.User03), dt);
                    }
                    else if (orderId == obj.OrderNO)//��һ��ҩ
                    {
                        objLast.User03 = objLast.User03 + " " + this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(obj.User03), dt);
                        //objLast.Operation.ApplyQty += obj.Operation.ApplyQty * obj.Days;//�������
                        alData.RemoveAt(i);
                    }
                    else
                    {
                        orderId = obj.OrderNO;
                        objLast = obj;
                        objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.User03), dt);
                    }
            }

            #endregion

            #region ����������

            //CompareApplyOut com = new CompareApplyOut();
            //alData.Sort(com);
            #endregion

            #region �����ߴ��źͻ�λ������{D9BE63EB-D955-48e2-A3A9-8FDB77BB3998}
            CompareBedPlaceNo comNew = new CompareBedPlaceNo();
            alData.Sort(comNew);
            #endregion

            this.SuspendLayout();          

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

            string privPatient = "";

            int iRow = 0;

            //���ߴ��� �������Ժ�ҩƷͬ�У���������Ƿ�Ҫ����һ����ʾҩƷ
            bool isNeedAddRow = true;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (ucDrugBillDetail.hsDept.ContainsKey(info.ApplyDept.ID))
                    this.lbTitl.Text = "                       " + ucDrugBillDetail.hsDept[info.ApplyDept.ID] + drugBillClass.Name + "����ϸ��" + "      " + this.ifBPrint;
                else
                    this.lbTitl.Text = "                       " + info.ApplyDept.Name + drugBillClass.Name + "����ϸ��" + "      " + this.ifBPrint;
                info.Item.NameCollection.RegularName = itemManager.GetItem(info.Item.ID).NameCollection.RegularName;//{8CA1AEE7-F038-4c32-BD3E-ECCC8DFE687B}
                isNeedAddRow = true;
                //{D515D71A-75B4-4c02-B2F7-569779A2A5A8}
                //string bedNO = info.User02;
                string bedNO = info.User02;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                //{D515D71A-75B4-4c02-B2F7-569779A2A5A8}
                //if (privPatient != "[" + bedNO + "]" + info.User01)
                if (privPatient != "[" + bedNO + "]" + info.User01)
                {
                    //���һ���µĻ�����Ϣ

                    #region ��һ������Ӧ�ü�һ��{827FF133-63BC-40e3-8704-6E732D5116B1}
                    if (iRow != 0)
                    {
                        iRow++;
                    }
                    #endregion

                    //{D515D71A-75B4-4c02-B2F7-569779A2A5A8}
                    //privPatient = "[" + bedNO + "]" + info.User01;
                    privPatient = "[" + bedNO + "]" + info.User01;
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);

                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColPatienName].Text = privPatient;

                    //���סԺ��
                    //if (drugBillClass.Name == "��ҩ��")
                    //    this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColPatienName].Text = privPatient + (info.PatientNO.ToString()).Substring(7);
                   
                    isNeedAddRow = false;
                }

                if (isNeedAddRow)
                {
                    iRow++;
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColPatienName].Text = "";
                }

                    // "[" + (info.User02).Substring(4) + "]" + info.User01;
                //this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColTradeName].Text = info.Item.Name + "��" + Function.DrugDosage.GetStaticDosage(info.Item.ID);
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColTradeName].Text = info.Item.NameCollection.RegularName + "(" + info.Item.Name + ")";
                char[] ca = this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColTradeName].Text.ToCharArray();//{8CA1AEE7-F038-4c32-BD3E-ECCC8DFE687B}������ʾ
                int j = System.Text.Encoding.Default.GetByteCount(ca, 0, ca.Length);
                if (j / 28 >= 1 && j % 28 == 0)//����һ�в������õ�����һ��
                {
                    this.neuSpread1_Sheet1.Rows[iRow].Height = (j / 28) * FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows.Default.Height);
                }
                else if (j / 28 >= 1 && j % 28 > 0)//����һ�У��������쵽��һ��
                {
                    this.neuSpread1_Sheet1.Rows[iRow].Height = ((j / 28) + 1) * FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows.Default.Height);
                }
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColSpecs].Text = info.Item.Specs;
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColUsage].Text = info.Usage.Name;
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColDoseOnce].Text = info.DoseOnce.ToString() + info.Item.DoseUnit.ToString();

                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColBaseDose].Text = info.Item.BaseDose.ToString();

                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColFrequency].Text = info.Frequency.ID;

                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColDays].Text = info.Days.ToString();
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColNum].Text = info.Operation.ApplyQty.ToString();
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColUnit].Text = info.Item.MinUnit;              
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColRetailPrice].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColCost].Value = (info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice);
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColPlace].Text = info.PlaceNO;

                if (FS.FrameWork.Public.String.ValidMaxLengh(info.User03, 16))
                {
                    this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColExeTime].Text = info.User03;
                }
                else
                {
                    string useTime = info.User03;
                    while (!FS.FrameWork.Public.String.ValidMaxLengh(useTime, 15))
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColExeTime].Text = useTime.Substring(0,12);
                        useTime = useTime.Substring(12);
                        iRow++;
                        this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                        this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnsSet.ColExeTime].Text = useTime;
                    }
                }
            }

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    int index = this.neuSpread1_Sheet1.Rows.Count;
                    this.neuSpread1_Sheet1.Rows.Add(index, 1);
                    this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 9;
                    this.neuSpread1_Sheet1.Cells[index, 11].Formula = string.Format("SUM(M1:M{0})", index.ToString());
                    decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[index, 11].Text);
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = "��ҩ��         ��ҩ��          ���ˣ�           �Ʊ��ˣ�" + (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Name + "      �ϼƣ�" + totCost.ToString("N");

                    this.neuSpread1_Sheet1.Cells[index, 11].Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.ResumeLayout(true);
        }

        /// <summary>
        /// ����ҩʱ��/��ǰʱ�� �����ʾ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        private string FormatDateTime(DateTime dt, DateTime sysdate)
        {
            try
            {
                if (sysdate.Date.AddDays(-1) == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date == dt.Date)
                {
                    return dt.Hour.ToString().PadLeft(2, '0');
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
                    return dt.Hour.ToString().PadLeft(2, '0');
                }
            }
            catch
            {
                return dt.Hour.ToString().PadLeft(2, '0');
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new System.Drawing.Printing.PaperSize("Letter", 780, 640));
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.PrintPreview(20, 10, this);

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.PaperName = "xxx" + (new Random()).Next(10000).ToString();//���������
            try
            {
                int width = 960;
                int curHeight = this.Height;
                int addHeight = (this.neuSpread1_Sheet1.RowCount - 1) * (int)this.neuSpread1_Sheet1.Rows[0].Height;

                int additionAddHeight = 3 * (int)this.neuSpread1_Sheet1.Rows[0].Height;

                paperSize.Width = width;
                paperSize.Height = (addHeight + curHeight + additionAddHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("���û��ܷ�ҩֽ�ų���>>" + ex.Message);
            }

            print.SetPageSize(paperSize);
            print.PrintPreview(15, 10, this);

        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        public void PrintPreview()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new System.Drawing.Printing.PaperSize("Letter", 780, 640));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPreview(20, 10, this);
        }

        public class CompareApplyOut : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = o1.User01;          //��������
                string oY = o2.User01;          //��������

                if (o1.User02.Length > 4)
                {
                    oX = o1.User02.Substring(4);
                }
                else
                {
                    oX = o1.User02;
                }
                if (o2.User02.Length > 4)
                {
                    oY = o2.User02.Substring(4);
                }
                else
                {
                    oY = o2.User02;
                }
                oX = oX.PadLeft(5, '0') + o1.User01;
                oY = oY.PadLeft(5, '0') + o2.User01;

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

        public class CompareBedPlaceNo : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = o1.User02 + o1.PlaceNO.PadLeft(5, '0')+o1.Item.UserCode;        //���ߴ���+��λ��+�Զ�����
                string oY = o2.User02 + o2.PlaceNO.PadLeft(5,'0')+o1.Item.UserCode;      //���ߴ���+��λ��+�Զ�����

                return string.Compare(oX, oY);
            }

        }

        enum ColumnsSet
        {
            ColPatienName,
            ColTradeName,
            ColSpecs,
            ColUsage,
            ColDoseOnce,
            ColBaseDose,
            ColFrequency,
            ColDays,
            ColNum,
            ColUnit,
            ColExeTime,
            ColRetailPrice,
            ColCost,
            ColPlace,
            ColEnd
        }
    }
}
