using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.FuYou
{
    public partial class ucItemListBill : UserControl, FS.HISFC.BizProcess.Interface.Nurse.ITreatmentPrint
    {
        public ucItemListBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 缓存用法
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper usageHelper;
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();


        //每页显示的行数
        int maxRowNumPerPage = 7;

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.nlbPatientName.Text = "";
            this.nlbSex.Text = "性别：";
            this.nlbAge.Text = "年龄：";
            this.nlbTime.Text = "时间：";
            this.nlbDoctor.Text = "医生：";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// 设置列
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientFeeItemListBill.xml");
        }

        public int Init()
        {
            this.Clear();
            this.SetFormat();

            if (usageHelper == null)
            {
                usageHelper = new FS.FrameWork.Public.ObjectHelper();
                usageHelper.ArrayObject = constantMgr.GetAllList("MZZLDUSAGE");
            }
            if (usageHelper == null || usageHelper.ArrayObject == null)
            {
                MessageBox.Show("获取用法信息发生错误");
                return -1;
            }

            this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);

            return 1;
        }

        private void SetBill(ArrayList alFeeItem)
        {
            if (alFeeItem == null || alFeeItem.Count == 0)
            {
                return;
            }

            ArrayList alNeedPrint = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeItem)
            {
                FS.HISFC.Models.Base.Const constObject = usageHelper.GetObjectFromID(f.Order.Usage.ID) as FS.HISFC.Models.Base.Const;
                if (constObject == null || string.IsNullOrEmpty(constObject.ID))// || f.FTSource != "1")
                {
                    continue;
                }
                alNeedPrint.Add(f);
            }

            if (alNeedPrint.Count == 0)
            {
                return;
            }

            //alNeedPrint.Sort(new CompareItemList());

            this.neuSpread1_Sheet1.RowCount = alNeedPrint.Count;
            int rowIndex = 0;
            FS.HISFC.Models.Order.OutPatient.Order order = null;

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alNeedPrint)
            {
                order = null;
                if (f.Order.Frequency.ID == "" || f.Days.ToString() == "")
                {
                    FS.HISFC.BizLogic.Order.OutPatient.Order orderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                    order = orderMgr.QueryOneOrder(f.Order.ID);
                    if (order != null)
                    {
                        f.Order.Frequency.ID = order.Frequency.ID;
                        f.Days = order.HerbalQty;
                    }
                }
                //this.neuSpread1.SetCellValue(0, rowIndex, "组", f.Order.Combo.ID);
                this.neuSpread1.SetCellValue(0, rowIndex, "项目名称", f.Item.Name);
                this.neuSpread1.SetCellValue(0, rowIndex, "用法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(f.Order.Usage.ID));
                this.neuSpread1.SetCellValue(0, rowIndex, "次数", f.Order.Frequency.ID);
                //this.neuSpread1.SetCellValue(0, rowIndex, "金额", (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString("F4").TrimEnd('0').TrimEnd('.') + " 元");
                if (string.IsNullOrEmpty(f.Memo))
                {
                    FS.HISFC.BizLogic.Order.OutPatient.Order orderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                    order = orderMgr.QueryOneOrder(f.Order.ID);
                    if (order != null)
                    {
                        f.Memo = order.Memo;
                    }
                }

                //if (phaItem.IsAllergy)
                //{
                //    hyTest = this.orderManager.TransHypotest(order.HypoTest);
                //    if (!string.IsNullOrEmpty(hyTest))
                //    {
                //        buffer.Append(hyTest + " ");
                //    }
                //}

                this.neuSpread1.SetCellValue(0, rowIndex, "备注", f.Memo);

                if (f.Days > 0)
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "天数", f.Days.ToString("F4").TrimEnd('0').TrimEnd('.'));
                }
                this.neuSpread1.SetCellValue(0, rowIndex, "组合号", f.UndrugComb.ID);

                if (rowIndex == 0)
                {
                    if (string.IsNullOrEmpty(f.Patient.Name))
                    {
                        FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
                        FS.HISFC.Models.Registration.Register r = registerMgr.GetByClinic(f.Patient.ID);
                        if (r != null)
                        {
                            this.nlbPatientName.Text = r.Name;
                            this.nlbSex.Text = "性别：" + r.Sex.Name;
                            this.nlbAge.Text = "年龄：" + constantMgr.GetAge(r.Birthday);
                        }
                    }
                    else
                    {
                        this.nlbPatientName.Text = f.Patient.Name;
                        this.nlbSex.Text = "性别：" + f.Patient.Sex.Name;
                        this.nlbAge.Text = "年龄：" + constantMgr.GetAge(f.Patient.Birthday);
                    }
                    this.nlbTime.Text = "时间：" + DateTime.Now.ToString();

                    this.nlbDoctor.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(f.RecipeOper.ID);
                    this.nlbFeeOper.Text = "收费员：" + f.FeeOper.ID;
                }

                rowIndex++;
            }

        }

        public void PrintBill()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int maxPageNO = this.neuSpread1_Sheet1.RowCount / this.maxRowNumPerPage;
            if (maxPageNO * this.maxRowNumPerPage < this.neuSpread1_Sheet1.RowCount)
            {
                maxPageNO = maxPageNO + 1;
            }

            int fromPageNO = 1;
            int toPageNO = maxPageNO;

            if (this.nlbRePrint.Visible && maxPageNO > 1)
            {
                SOC.Windows.Forms.PrintPageSelectDialog printPageSelectDialog = new SOC.Windows.Forms.PrintPageSelectDialog();
                printPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                printPageSelectDialog.MaxPageNO = maxPageNO;
                printPageSelectDialog.ShowDialog();
                //开始页码为0，说明用户取消打印
                if (printPageSelectDialog.FromPageNO == 0)
                {
                    return;
                }
                fromPageNO = printPageSelectDialog.FromPageNO;
                toPageNO = printPageSelectDialog.ToPageNO;
            }


            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                this.nlbPageNo.Text = "页：" + (pageNO + 1).ToString() + "/" + maxPageNO.ToString();
                int rowIndex = 0;
                for (; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
                {
                    if (rowIndex < maxRowNumPerPage * (pageNO + 1) && rowIndex >= maxRowNumPerPage * pageNO)
                    {
                        this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[rowIndex].Visible = false;
                    }
                }
                if (pageNO + 1 < fromPageNO || pageNO + 1 > toPageNO)
                {
                    continue;
                }
                this.PrintPage();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();

            print.SetPageSize(paperSize);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 0, this);
            }
            else
            {
                print.PrintPage(5, 0, this);
            }
        }

        /// <summary>
        /// 获取纸张
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("MZZLD1");
            //自适应纸张
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = "OutPatientZLD";

                paperSize.Width = 800;
                paperSize.Height = 1105 / 3;

            }
            if (!string.IsNullOrEmpty(paperSize.Printer) && paperSize.Printer.ToLower() == "default")
            {
                paperSize.Printer = "";
            }
            return paperSize;
        }


        public int PrintData(ArrayList alFeeItem)
        {
            this.Init();
            this.SetBill(alFeeItem);
            this.nlbRePrint.Visible = false;
            this.PrintBill();

            return 1;
        }

        public int SetData(ArrayList alFeeItem)
        {
            this.Init();
            this.SetBill(alFeeItem);
            return 1;
        }


        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientFeeItemListBill.xml");
        }



        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareItemList : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList o1 = (x as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Clone();
                FS.HISFC.Models.Fee.Outpatient.FeeItemList o2 = (y as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Clone();

                string oX = "";
                string oY = "";


                oX = o1.UndrugComb.ID;
                oY = o2.UndrugComb.ID;

                return string.Compare(oX, oY);
            }
        }
    }
}
