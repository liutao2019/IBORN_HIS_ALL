using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;
using FS.FrameWork.Models;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.Print.FuYou
{
    /// <summary>
    /// 东莞药品入库单据打印
    /// </summary>
    public partial class ucEmptyCheckBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucEmptyCheckBill()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// 打印相关属性值
        /// </summary>
        private Base.PrintBill printBill = new Base.PrintBill();

        private DateTime fOperDate = DateTime.Now;

        private string deptCode = "";

        Hashtable hsItem = new Hashtable();
        #endregion

        /// <summary>
        /// 1外部调用 
        /// </summary>
        /// <param name="alPrintData"></param>
        /// <param name="printBill"></param>
        /// <returns></returns>
        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (alPrintData != null && alPrintData.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在检索打印信息...请稍候");
                Application.DoEvents();

                FS.SOC.HISFC.BizLogic.Pharmacy.Check itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();

                List<FS.HISFC.Models.Pharmacy.Item> alItem = itemMgr.QueryItemList();
                if (alItem == null)
                {
                    Function.ShowMessage("获取药品基本信息发生错误，请与系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
                {
                    hsItem.Add(item.ID, item);
                }

                NeuObject obj = alPrintData[0] as NeuObject;
                this.deptCode = obj.ID;
                int days = NConvert.ToInt32(obj.Name);

                ArrayList alCheckDetail = itemMgr.CloseAll(deptCode, false, days);

                if (alCheckDetail==null||alCheckDetail.Count==0)
                {
                    MessageBox.Show("没有要打印的内容");
                    return 0;
                }

                foreach (FS.HISFC.Models.Pharmacy.Check checkItem in alCheckDetail)
                {
                    if (hsItem.Contains(checkItem.Item.ID))
                    {
                        checkItem.Item = hsItem[checkItem.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                    }
                    else
                    {
                        checkItem.Item = new FS.HISFC.Models.Pharmacy.Item();
                    }
                }

                this.alPrintData = alCheckDetail;

                Base.PrintBill.SortByCustomerCode(ref this.alPrintData);

                this.printBill = printBill;
                
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                return this.Print();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 2打印函数
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region 打印信息设置
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            this.lblCheckDate.Text = "日期：" + DateTime.Now.ToString("yyyy-MM-dd");
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();


            int pageTotNum = alPrintData.Count /( this.printBill.RowCount*2);
            if (alPrintData.Count != this.printBill.RowCount * pageTotNum * 2)
            {
                pageTotNum++;
            }

            int fromPage = 0;
            int toPage = 0;
            FS.SOC.Local.Pharmacy.Print.BeiJiao.frmSelectPages frmSelect = new FS.SOC.Local.Pharmacy.Print.BeiJiao.frmSelectPages();
            frmSelect.PageCount = pageTotNum;
            frmSelect.SetPages();
            DialogResult dRsult = frmSelect.ShowDialog();
            if (dRsult == DialogResult.OK)
            {
                fromPage = frmSelect.FromPage - 1;
                toPage = frmSelect.ToPage;
            }
            else
            {
                return 0;
            }

            //分页打印
            for (int pageNow = fromPage; pageNow < toPage; pageNow++)
            {
                ArrayList al = new ArrayList();

                for (int index = pageNow * this.printBill.RowCount*2; index < alPrintData.Count && index < (pageNow + 1) * this.printBill.RowCount*2; index++)
                {
                    al.Add(alPrintData[index]);
                }
                this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                this.neuPanel5.Height += (int)rowHeight * al.Count/2;
                this.Height += (int)rowHeight * al.Count/2;

                if (this.printBill.IsNeedPreview)
                {
                    p.PrintPreview(5, 0, this.neuPanel1);
                }
                else
                {
                    p.PrintPage(5, 0, this.neuPanel1);
                }

                this.neuPanel5.Height = height;
                this.Height = ucHeight;
            }
            // }
            #endregion

            return 1;
        }

        /// <summary>
        /// 3赋值
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            this.lblTitle.Text = "{0}盘点表";
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.Check info = (FS.HISFC.Models.Pharmacy.Check)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.deptCode));
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.deptCode));
                }
                this.lblTitle.Text = title;
            }

            this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
            this.lblPage.Text = "页:" + inow.ToString() + "/" + icount.ToString();

            #endregion

            #region farpoint赋值

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {

                FS.HISFC.Models.Pharmacy.Check check = al[i] as FS.HISFC.Models.Pharmacy.Check;
                //if (i<this.printBill.RowCount)
                //{
                    
                //this.neuFpEnter1_Sheet1.AddRows(i, 1);
                //this.neuFpEnter1_Sheet1.Cells[i, 0].Text = check.Item.UserCode;
                //this.neuFpEnter1_Sheet1.Cells[i, 1].Text = check.Item.Name;//药品名称
                //this.neuFpEnter1_Sheet1.Cells[i, 2].Text = check.Item.Specs;//规格		
                //this.neuFpEnter1_Sheet1.Cells[i, 4].Text = check.Item.PackUnit;//包装单位	
                //this.neuFpEnter1_Sheet1.Cells[i, 6].Text = check.Item.MinUnit;//最小单位
                //}
                //else
                //{
                //    this.neuFpEnter1_Sheet1.Cells[i - this.printBill.RowCount, 7].Text = check.Item.UserCode;
                //   this.neuFpEnter1_Sheet1.Cells[i - this.printBill.RowCount, 8].Text = check.Item.Name;//药品名称
                //    this.neuFpEnter1_Sheet1.Cells[i - this.printBill.RowCount, 9].Text = check.Item.Specs;//规格		
                //    this.neuFpEnter1_Sheet1.Cells[i - this.printBill.RowCount, 11].Text = check.Item.PackUnit;//包装单位	
                //    this.neuFpEnter1_Sheet1.Cells[i - this.printBill.RowCount, 13].Text = check.Item.MinUnit;//最小单位
                //}
                if (i % 2 == 0)
                {
                    this.neuFpEnter1_Sheet1.AddRows(i / 2, 1);
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 0].Text = check.Item.UserCode;
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 1].Text = check.Item.Name;//药品名称
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 2].Text = check.Item.Specs;//规格		
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 4].Text = check.Item.PackUnit;//包装单位	
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 6].Text = check.Item.MinUnit;//最小单位
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 7].Text = check.Item.UserCode;
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 8].Text = check.Item.Name;//药品名称
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 9].Text = check.Item.Specs;//规格		
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 11].Text = check.Item.PackUnit;//包装单位	
                    this.neuFpEnter1_Sheet1.Cells[i / 2, 13].Text = check.Item.MinUnit;//最小单位
                }
            }          

            #endregion

            this.resetTitleLocation();
        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation()
        {
            this.neuPanel4.Controls.Remove(this.lblTitle);
            int with = 0;
            for (int col = 0; col < this.neuFpEnter1_Sheet1.ColumnCount; col++)
            {
                if (this.neuFpEnter1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuFpEnter1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel4.Width)
            {
                with = this.neuPanel4.Width;
            }
            this.lblTitle.Location = new Point((with - this.lblTitle.Size.Width) / 2, this.lblTitle.Location.Y);
            this.neuPanel4.Controls.Add(this.lblTitle);

        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <returns></returns>
        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

    }
}
