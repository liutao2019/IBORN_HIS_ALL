using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.WinForms.Report.Material
{
    /// <summary>
    /// [功能描述: 物资出库单打印]
    /// [创 建 者: 王维]
    /// [创建时间: 2008-3-19]
    /// </summary>
    public partial class ucMatOutputBill : UserControl, Neusoft.HISFC.BizProcess.Interface.Material.IBillPrint
    {
        #region 构造方法

        public ucMatOutputBill()
        {
            InitializeComponent();
        }

        #endregion

        #region 字段

        /// <summary>
        /// 一张单据最大行数
        /// </summary>
        private int maxRowNo;

        /// <summary>
        /// 科室管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Department deptManager = new Neusoft.HISFC.BizLogic.Manager.Department();

        #endregion

        #region 属性

        /// <summary>
        /// 打印行数
        /// </summary>
        public int MaxRowNo
        {
            get
            {
                return this.maxRowNo;
            }
            set
            {
                this.maxRowNo = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 打印主函数
        /// </summary>
        /// <param name="alPrint">待打印实体</param>
        /// <param name="inow">所打印页数</param>
        /// <param name="icount">总页数</param>
        /// <param name="operCode">操作员编码</param>
        /// <param name="kind">打印类型</param>
        /// <returns></returns>
        public void print(List<Neusoft.HISFC.Models.FeeStuff.Output> alPrint, int inow, int icount, string operCode, string kind)
        {
            this.OutputPrint(alPrint, inow, icount);
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="alPrint"></param>
        /// <param name="inow"></param>
        /// <param name="icount"></param>
        /// <returns></returns>
        public void OutputPrint(List<Neusoft.HISFC.Models.FeeStuff.Output> alPrint, int inow, int icount)
        {
            if (alPrint.Count <= 0)
            {
                MessageBox.Show("无打印数据!");
                return;
            }

            #region LABLE 赋值

            Neusoft.HISFC.Models.Material.Output output = alPrint[0] as Neusoft.HISFC.Models.Material.Output;
            this.lbTitle.Text = this.deptManager.GetDeptmentById(output.StoreBase.StockDept.ID) + "物资出库单";
            this.lbDept.Text += this.deptManager.GetDeptmentById(output.StoreBase.TargetDept.ID);
            this.lbTime.Text = output.StoreBase.Operation.ExamOper.OperTime.ToString("yyyy.MM.dd");
            this.lbPagNum.Text = "第" + inow.ToString() + "页/共" + icount.ToString() + "页";

            #endregion

            #region FarPoint赋值

            this.sheetView1.RowCount = 0;
            decimal sum5 = 0;
            decimal sum6 = 0;
            for (int i = 0; i < alPrint.Count; i++)
            {
                this.sheetView1.AddRows(i, 1);
                Neusoft.HISFC.Models.Material.Output info = alPrint[i] as Neusoft.HISFC.Models.Material.Output;
                //{D306B133-1E9E-4f75-9458-EB8B0C820425} 使用info而不是output
                this.sheetView1.Cells[i, 0].Text = (i + 1).ToString();
                this.sheetView1.Cells[i, 1].Text = info.StoreBase.Item.Name;
                this.sheetView1.Cells[i, 2].Text = info.StoreBase.Item.Specs;
                this.sheetView1.Cells[i, 3].Text = info.StoreBase.Quantity.ToString();
                this.sheetView1.Cells[i, 4].Text = info.StoreBase.Item.PriceUnit;
                this.sheetView1.Cells[i, 5].Text = info.StoreBase.PriceCollection.RetailPrice.ToString();
                this.sheetView1.Cells[i, 6].Text = (info.StoreBase.PriceCollection.RetailPrice * info.StoreBase.Quantity).ToString();
                this.sheetView1.Cells[i, 7].Text = info.StoreBase.BatchNO.ToString();
                this.sheetView1.Cells[i, 8].Text = info.StoreBase.ValidTime.ToString("yyyy.MM.dd");
                this.sheetView1.Cells[i, 9].Text = info.OutListNO;
                sum5 += info.StoreBase.PriceCollection.RetailPrice;
                sum6 += info.StoreBase.PriceCollection.RetailPrice * info.StoreBase.Quantity;
                //-------------------------------------------------
            }
            this.sheetView1.RowCount = alPrint.Count + 1;
            this.sheetView1.Cells[alPrint.Count, 0].Text = "合计";
            this.sheetView1.Cells[alPrint.Count, 5].Text = sum5.ToString();
            this.sheetView1.Cells[alPrint.Count, 6].Text = sum6.ToString();
            this.fpSpread1.Height = (int)this.sheetView1.RowHeader.Rows[0].Height +
                (int)(this.sheetView1.Rows[0].Height * (alPrint.Count + 1)) + 10;

            #endregion

            Neusoft.FrameWork.WinForms.Classes.Print pri = null;
            try
            {
                pri = new Neusoft.FrameWork.WinForms.Classes.Print();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("初始化打印机失败" + ex.Message);
            }

            pri.PrintPage(12, 2, this.neuPanel1);
        }
        #endregion

        #region 实现IBillPrint成员

        public int Print()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Prieview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.Check> alCheck)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.Input> alInData)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.InputPlan> alPlan)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.Output> alOutData)
        {
            this.maxRowNo = 12;
            List<Neusoft.HISFC.Models.Material.Output> alprint = new List<Neusoft.HISFC.Models.Material.Output>();
            int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(System.Math.Ceiling(Convert.ToDouble(alOutData.Count) / this.MaxRowNo));

            for (int i = 0; i < icount; i++)
            {
                if (i != icount - 1)
                {
                    alprint = alOutData.GetRange(i, this.MaxRowNo);
                }
                else
                {
                    int num = alOutData.Count % this.MaxRowNo;
                    if (num == 0)
                    {
                        num = this.MaxRowNo;
                    }
                    alprint = alOutData.GetRange(i, num);
                }
                this.print(alprint, i + 1, icount, "", "");
            }
            return 1;
        }

        #endregion
    }
}
