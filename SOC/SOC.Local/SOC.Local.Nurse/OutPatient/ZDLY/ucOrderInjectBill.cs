using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Nurse.OutPatient.ZDLY
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
    /// </summary>
    public partial class ucOrderInjectBill : UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint
    {
        #region 变量

        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        private int iSet = 5;

        /// <summary>
        /// 当前显示组号
        /// </summary>
        private int showGroupNO = 0;

        /// <summary>
        /// 当前记录的组合号
        /// </summary>
        private string rememberComboNO = "";

        /// <summary>
        /// 合并第一列单元格的起始行号
        /// </summary>
        private int spanRowIndex = 1;

        /// <summary>
        /// 诊查费
        /// </summary>
        private decimal zhenChaFee = 0m;

        /// <summary>
        /// 医院名称
        /// </summary>
        string strHosName = string.Empty;
        /// <summary>
        /// 纸张
        /// </summary>
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        ///常数维护业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public ucOrderInjectBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印一个院注射单
        /// </summary>
        /// <param name="al"></param>
        /// <param name="current"></param>
        /// <param name="total"></param>
        private void PrintOnePage(ArrayList al, int current, int total)
        {
            GetHospLogo();
            //this.neuLabel2.Text = strHosName + "院注单";
            this.lblZhenChaFee.Visible = false;
            if (zhenChaFee > 0)
            {
                this.lblZhenChaFee.Visible = true;
                this.lblZhenChaFee.Text = string.Format("该患者已支付急诊监护费({0}元)", zhenChaFee);
            }
            #region 设置对齐和border
            //for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            //{
            //    if (i == 1)
            //    {
            //        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //    }
            //    else
            //    {
            //        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    }
            //    this.neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //}
            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
            FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
            #endregion
            spanRowIndex = 0;
            FS.HISFC.Models.Nurse.Inject info = null;          
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                //int tempRowCount = neuSpread1_Sheet1.RowCount;
                //this.neuSpread1_Sheet1.RemoveRows(0, neuSpread1_Sheet1.RowCount);
                //this.neuSpread1_Sheet1.Rows.Add(0, tempRowCount);
                this.neuSpread1_Sheet1.RowCount = 0;
            }
            for (int i = 0; i < al.Count; i++)
            {
                this.neuSpread1_Sheet1.Rows.Add(i, 1);
                info = (FS.HISFC.Models.Nurse.Inject)al[i];                
                //修改组号
                if (info.Item.Order.Combo.ID != rememberComboNO)
                {
                    rememberComboNO = info.Item.Order.Combo.ID;
                    //spanRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                    spanRowIndex = i;
                    showGroupNO++;
                    //this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    //剂量不能合并
                    //this.neuSpread1_Sheet1.Cells[spanRowIndex, 3].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 2].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 6].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    //this.neuSpread1_Sheet1.Cells[spanRowIndex, 7].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 10].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 11].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                }
                this.neuSpread1_Sheet1.Cells[i, 0].Text = showGroupNO.ToString();   //组合号
                this.neuSpread1_Sheet1.Cells[i, 1].Text = info.Item.Name;    //药品名称
                this.neuSpread1_Sheet1.Cells[i, 3].Text = info.Item.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.Order.DoseUnit;  //用量                 
                try
                {
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(info.Item.Order.Usage.ID)).Name;//用法
                }
                catch { }
                string hypoTest = "";    //皮试
                if (info != null)
                {
                    hypoTest = outOrderMgr.TransHypotest(info.Item.Order.HypoTest);
                }
                this.neuSpread1_Sheet1.Cells[i, 4].Text = hypoTest; //皮试
                this.neuSpread1_Sheet1.Cells[i, 5].Text = info.Item.Item.Specs; //规格
                this.neuSpread1_Sheet1.Cells[i, 6].Text = info.Item.Order.Frequency.ID; //次数
                if (info != null)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = info.Item.Days.ToString(); // 天数
                    this.neuSpread1_Sheet1.Cells[i, 9].Text = info.Item.Order.Memo;//滴速
                }
                this.neuSpread1_Sheet1.Cells[i, 8].Text = info.Item.Order.Qty.ToString() + info.Item.Order.DoseUnit;    //总量
                this.neuSpread1_Sheet1.Cells[i, 10].Text = " ";//开始时间
                this.neuSpread1_Sheet1.Cells[i, 11].Text = " ";//执行人

                this.neuSpread1_Sheet1.Rows[i].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            }
            #region    给lb赋值信息
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            //this.lbInvoiceNo.Text = "发票号：" + drugRecipe.InvoiceNO;
            this.lbCard.Text = info.Patient.PID.CardNO;  //病历号
            this.lbName.Text = info.Patient.Name;      //姓名
            this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lbAge.Text = this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
            if (info.Patient.Sex.ID.ToString() == "M")
            {
                this.lbSex.Text = "男";
            }
            else if (info.Patient.Sex.ID.ToString() == "F")
            {
                this.lbSex.Text = "女";
            }
            else
            {
                this.lbSex.Text = "";
            }
            this.lblDoct.Text = "医生：" + info.Item.Order.ReciptDoctor.Name;
            this.lblDept.Text = "科室：" + info.Item.Order.DoctorDept.Name;
            this.lbPage.Text = "第" + current.ToString() + "页" + "/" + "共" + total.ToString() + "页";
            #endregion            
            ////增加fp下面的内容
            //this.neuDoctName.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Item.RegDeptInfo.Name);//医生姓名 
            //this.neuChargeOper.Text = "收费员：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.FeeOper.ID); ;//收费员
            #region 设置界面用来配合打印

            System.Windows.Forms.Control c = this;
            c.Width = this.Width;
            c.Height = this.Height;

            #endregion
            //打印机
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //FS.Common.Class.Function.GetPageSize("Inject4", ref p);

            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OrderInjectBill");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OrderInjectBill", 900, 750);
                }
            }
            //pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientCureCard", 400, 300);
            p.SetPageSize(pageSize);
            p.PrintPage(40, 0, c);
            //PrintPage(regObj.User03);           
        }
        
        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // 单元格重绘
            Pen pp = new Pen(Color.Red);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1);
        }

        //获取医院LOGO和名称
        private void GetHospLogo()
        {            
            ComFun cf = new ComFun();
            string erro = "出错";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }

        #region IInjectPrint 成员

        public void Init(ArrayList alData)
        {
            try
            {
                //获取医院名称
                strHosName = this.constMgr.GetHospitalName();
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));
                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("打印出错!" + e.Message);
                return;
            }
        }

        #endregion
    }

    #region 排序类
    /// <summary>
    /// 按处方排序类
    /// </summary>
    public class CompareApplyOutByCombNO : IComparer
    {
        /// <summary>
        /// 排序方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.OutPatient.Order o1 = (x as FS.HISFC.Models.Order.OutPatient.Order).Clone();
            FS.HISFC.Models.Order.OutPatient.Order o2 = (y as FS.HISFC.Models.Order.OutPatient.Order).Clone();

            Int32 oX = o1.SortID;          //患者姓名
            Int32 oY = o2.SortID;          //患者姓名

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
                nComp = oX - oY;
            }

            return nComp;
        }

    }
    #endregion
}
