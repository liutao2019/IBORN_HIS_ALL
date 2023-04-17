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
    /// סԺ��ҩ���ܵ���ӡ
    /// 
    /// <����˵��>
    ///     1��AlterApplyData ���ڽ�������ɢ��� Ŀǰ�Ѿ����θù��� 
    ///     2��GetStaticPlaceNO ���ڻ�ȡɢ��װ��λ�� GetStaticDosage ���ڻ�ȡҩƷ����
    ///         �ù���������
    /// </����˵��>
    /// </summary>
    public partial class ucDrugTotal : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucDrugTotal()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitFp();

                this.InitData();
            }
        }

        private static FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ��̬������
        /// </summary>
        private static System.Collections.Hashtable hsDept = new Hashtable();

        /// <summary>
        /// ɢ��װ��λ��
        /// </summary>
        private static System.Collections.Hashtable hsStaticPlaceNO = new Hashtable();

        /// <summary>
        /// ����
        /// </summary>
        private static System.Collections.Hashtable hsDosage = new Hashtable();

        /// <summary>
        /// ��װ��λ
        /// </summary>
        private static System.Collections.Hashtable hsPackUnit = new Hashtable();

        /// <summary>
        /// ���Ͱ�����
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper dosageHelper = new FS.FrameWork.Public.ObjectHelper();

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
        /// ��ȡɢ��װ��λ��
        /// </summary>
        private static string GetStaticPlaceNO(string deptCode,string drugCode)
        {
            return "";

            if (hsStaticPlaceNO.ContainsKey(deptCode + drugCode))
            {
                return hsStaticPlaceNO[deptCode + drugCode].ToString();
            }
            else            
            {
                FS.HISFC.Models.Pharmacy.Storage storage = itemManager.GetStockInfoByDrugCode(deptCode, drugCode);
                if (storage == null)
                {
                    return "";
                }

                hsStaticPlaceNO.Add(deptCode + drugCode, storage.Memo);
                //hsDosage.Add(drugCode, dosageHelper.GetName(storage.Item.DosageForm.ID));

                return storage.Memo;
            }            
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        private static string GetStaticDosage(string drugCode)
        {
            return "";

            if (hsDosage.ContainsKey(drugCode))
            {
                return hsDosage[drugCode].ToString();
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item item = itemManager.GetItem(drugCode);
                hsDosage.Add(drugCode, dosageHelper.GetName(item.DosageForm.ID));
                hsPackUnit.Add(drugCode, item.PackUnit);
                return item.DosageForm.ID;
            }
        }

        /// <summary>
        /// ��ȡ��װ��λ
        /// </summary>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        private static string GetStaticPackUnit(string drugCode)
        {
            if (hsPackUnit.ContainsKey(drugCode))
            {
                return hsPackUnit[drugCode].ToString();
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item item = itemManager.GetItem(drugCode);
                hsPackUnit.Add(drugCode, item.PackUnit);
                hsDosage.Add(drugCode, dosageHelper.GetName(item.DosageForm.ID));
                return item.PackUnit;
            }
        }

        /// <summary>
        /// Fp����
        /// </summary>
        private void InitFp()
        {
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

              this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 30;
            this.neuSpread1_Sheet1.Rows.Default.Height = 30;
            //FarPoint.Win.Spread.CellType.TextCellType texttype = new FarPoint.Win.Spread.CellType.TextCellType();//{8CA1AEE7-F038-4c32-BD3E-ECCC8DFE687B}
            //texttype.WordWrap = true;
            //texttype.Multiline = true;
            //this.neuSpread1_Sheet1.Columns.Get(1).CellType = texttype;
        }

        /// <summary>
        /// �������ݳ�ʼ��
        /// </summary>
        private void InitData()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alList = consManager.GetAllList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
            if (alList != null)            
            {
                dosageHelper = new FS.FrameWork.Public.ObjectHelper(alList);
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
        /// ҩƷ��ɢ���
        /// </summary>
        /// <param name="alOriginalData"></param>
        /// <param name="alData"></param>
        protected void AlterApplyData(ArrayList alOriginalData, ref ArrayList alData)
        {
            alData = alOriginalData;

            return;

            ArrayList alDetail = new ArrayList();

            FS.HISFC.Models.Pharmacy.ApplyOut temp = null;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alOriginalData)
            {
                if (info.Days == 0)
                {
                    info.Days = 1;
                }
                decimal allQty = info.Operation.ApplyQty * info.Days;                
                int min = 0;
                int pack = System.Math.DivRem((int)allQty, (int)info.Item.PackQty,out min);
                if (min == 0)       //��������װ ������װ����
                {
                    temp = info.Clone();
                    temp.Operation.ApplyQty = pack;
                    if (temp.Item.PackUnit == "")
                    {
                        temp.Item.PackUnit = GetStaticPackUnit(temp.Item.ID);
                    }
                    temp.Item.MinUnit = temp.Item.PackUnit;
                    temp.Item.User01 = "1";
                    alData.Add(temp.Clone());
                }
                else               //��λ�������� 
                {
                    if (pack == 0)  //���������һ����װ��λ ֱ�Ӱ�����С��λ����
                    {
                        temp = info.Clone();
                        //�˴������� ��ɢ��λ�Ļ�λ��
                        temp.PlaceNO = GetStaticPlaceNO(temp.StockDept.Name, temp.Item.ID);
                        alDetail.Add(temp.Clone());
                    }
                    else           //������װ�����Ƴ� �γ���/ɢ������¼
                    {
                        //����װ��
                        temp = info.Clone();
                        temp.Operation.ApplyQty = pack;
                        if (temp.Item.PackUnit == "")
                        {
                            temp.Item.PackUnit = GetStaticPackUnit(temp.Item.ID);
                        }
                        temp.Item.MinUnit = info.Item.PackUnit ;
                        temp.Item.User01 = "1";
                        alData.Add(temp.Clone());
                        //ɢ��װ��
                        temp = info.Clone();
                        temp.Operation.ApplyQty = min;
                        temp.Item.MinUnit = info.Item.MinUnit;
                        //�˴���������ɢ��λ�����
                        temp.PlaceNO = GetStaticPlaceNO(temp.StockDept.Name, temp.Item.ID);
                        alDetail.Add(temp.Clone());

                    }
                } 
            }

            alData.AddRange(alDetail);
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="alOriginalData"></param>
        /// <param name="drugBillClass"></param>
        public void ShowData(ArrayList alOriginalData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            ArrayList alData = new ArrayList();
            this.AlterApplyData(alOriginalData, ref alData);

            CompareApplyOut compare = new CompareApplyOut();
            alData.Sort(compare);

            #region ��̬���Ұ�����Ϣ��ȡ

            if (ucDrugTotal.hsDept.Count == 0)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                foreach (FS.HISFC.Models.Base.Department dept in alDept)
                {
                    ucDrugTotal.hsDept.Add(dept.ID, dept.Name);
                }
            }

            #endregion

            this.SuspendLayout();

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            this.lbPrintTime.Text = "��ӡʱ��:" + dataManager.GetDateTimeFromSysDateTime().ToString();

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

            int iRow = 0;
            int iCount = 0;
            decimal totCost = 0;//�ܽ��{65581D3C-D84E-4d4d-AF93-B58077F10DD5}
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                iCount++;
                if (iCount == 6)
                {
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    iRow++;
                    iCount = 0;
                }
                #region ���ݸ�ֵ

                if (ucDrugTotal.hsDept.ContainsKey(info.ApplyDept.ID))
                    this.lbTitl.Text = "                       " + ucDrugTotal.hsDept[info.ApplyDept.ID] + drugBillClass.Name + "�����ܣ�" + "      "   + this.ifBPrint;
                else
                    this.lbTitl.Text = "                       " + info.ApplyDept.Name + drugBillClass.Name + "�����ܣ�" + "     " + this.ifBPrint;

                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                info.Item.NameCollection.RegularName = itemManager.GetItem(info.Item.ID).NameCollection.RegularName;//{8CA1AEE7-F038-4c32-BD3E-ECCC8DFE687B}
                this.neuSpread1_Sheet1.Cells[iRow, 0].Text = info.PlaceNO;
                //���μ��͵���ʾ
                this.neuSpread1_Sheet1.Cells[iRow, 1].Text = info.Item.NameCollection.Name + "(" + info.Item.Name + ")" + "[ " + info.Item.Specs + " ]";
                char[] ca = this.neuSpread1_Sheet1.Cells[iRow, 1].Text.ToCharArray();//{8CA1AEE7-F038-4c32-BD3E-ECCC8DFE687B}
                int j = System.Text.Encoding.Default.GetByteCount(ca, 0, ca.Length);
                 if (j / 40 >= 1 && j % 40 == 0)//����һ�в������õ�����һ��
                {
                    this.neuSpread1_Sheet1.Rows[iRow].Height = (j / 40) * FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows.Default.Height);
                }
                else if (j / 40 >= 1 && j % 40 > 0)//����һ�У��������쵽��һ��
                {
                    this.neuSpread1_Sheet1.Rows[iRow].Height = ((j / 40) + 1) * FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows.Default.Height);
                }
                this.neuSpread1_Sheet1.Cells[iRow, 2].Text = info.Operation.ApplyQty.ToString();
                this.neuSpread1_Sheet1.Cells[iRow, 3].Text = info.Item.MinUnit;
                this.neuSpread1_Sheet1.Cells[iRow, 4].Text = info.Item.PriceCollection.RetailPrice.ToString();
                if (info.Item.User01 == "1")            //���������Ϊ��װ��λ
                {
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Value = (info.Operation.ApplyQty * info.Item.PriceCollection.RetailPrice);
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Value = (info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice);
                }
                totCost += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[iRow, 5].Value);
                iRow++;

                #endregion
            }

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            #region ����������

            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 6;
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "��ҩ��         ��ҩ��         ��׼��         �Ʊ��ˣ�" + (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Name + "      �ۼƣ�" +totCost.ToString();                     
                    //this.neuSpread1_Sheet1.Cells[iRow, 5].Formula = string.Format("SUM(E1:E{0})", iRow.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.ResumeLayout(true);

            #endregion
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            ////print.PrintPage(40, 10, this);
            //foreach (Control c in this.neuPanel1.Controls)
            //{
            //    c.Visible = true;
            //}

            ////print.SetPageSize(new System.Drawing.Printing.PaperSize("Letter", 780, 640));
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            //print.PrintPreview(50, 10, this.neuPanel1);

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

            print.PrintPreview(40, 10, this.neuPanel1);
        }

        public class CompareApplyOut : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = o1.PlaceNO.PadLeft(5, '0') + o1.Item.UserCode;          //��λ��+�Զ�����{D9BE63EB-D955-48e2-A3A9-8FDB77BB3998}
                string oY = o2.PlaceNO.PadLeft(5,'0')+o1.Item.UserCode;          //��λ��+�Զ�����
              
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
    }
}
