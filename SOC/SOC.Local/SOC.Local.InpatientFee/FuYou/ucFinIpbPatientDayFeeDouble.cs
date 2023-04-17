using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.FuYou
{
    public partial class ucFinIpbPatientDayFeeDouble : UserControl
    {
        public ucFinIpbPatientDayFeeDouble()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        /// <summary>
        /// 清单的总金额
        /// </summary>
        private decimal totCost = 0;

        /// <summary>
        /// 总页数
        /// </summary>
        private int pageCount = 0;

        /// <summary>
        /// 当前打印页数
        /// </summary>
        private int curPage = 1;

        /// <summary>
        /// 每个患者的记账条数
        /// </summary>
        private int itemCount = 0;

        /// <summary>
        /// 查询病人集合
        /// </summary>
        public string inpatientLine = string.Empty;

        /// <summary>
        /// 查询服务类
        /// </summary>
        Base.ReportService rs = new Base.ReportService();

        /// <summary>
        /// 每页打印行数
        /// </summary>
        int rowCount = 16;

        /// <summary>
        /// 统计日期
        /// </summary>
        DateTime dtStatDate;
        #endregion

        #region 属性
        /// <summary>
        /// 每页打印行数
        /// </summary>
        public int RowCount
        {
            set
            {
                this.rowCount = value;
            }
            get
            {
                return this.rowCount;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public int SetPrintData(DateTime dt)
        {
            pageCount = 0;
            this.inpatientLine = this.inpatientLine.TrimEnd(',');
            this.dtStatDate = dt;
            DateTime dtBeginTime = dt.Date;
            DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);
            ArrayList al = this.rs.QueryInpatientDayFee(inpatientLine, dtBeginTime, dtEndTime);

            if (al == null || al.Count == 0)
            {
                return 1;
            }

            #region 计算总页数
            Hashtable htInpatientID = new Hashtable();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList ft in al)
            {
                if (!htInpatientID.Contains(ft.Patient.PID.PatientNO))
                {
                    htInpatientID.Add(ft.Patient.PID.PatientNO, 1);
                }
                else
                {
                    int countValue = FS.FrameWork.Function.NConvert.ToInt32(htInpatientID[ft.Patient.PID.PatientNO]) + 1;
                    htInpatientID[ft.Patient.PID.PatientNO] = countValue;
                }
            }

            foreach (string keys in htInpatientID.Keys)
            {
                pageCount = pageCount + 1;
                int itemCount = FS.FrameWork.Function.NConvert.ToInt32(htInpatientID[keys]);

                int pageTotNum = decimal.ToInt32(decimal.Ceiling(new decimal(itemCount / 2.0))) / this.rowCount;
                if (decimal.ToInt32(decimal.Ceiling(new decimal(itemCount / 2.0))) != this.rowCount * pageTotNum)
                {
                    pageTotNum++;
                }
                pageCount = pageCount + pageTotNum - 1;
            }
            #endregion

            this.alPrintData = al;
            if (this.Print() == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 2此函数用来分页
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region 打印信息设置
            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("patientDayFee");
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsHaveGrid = true;
            p.SetPageSize(pSize);
            #endregion

            #region 分页打印
            int height = this.pfspread.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuSpread1_Sheet1.Rows[0].Height;

            NoSortHashTable hs = new NoSortHashTable();
            hsTotCost.Clear();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList ft in alPrintData)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = ft.Clone();
                if (hs.Contains(f.Patient.PID.PatientNO))
                {
                    ArrayList al = (ArrayList)hs[f.Patient.PID.PatientNO];
                    al.Add(f);

                    if (hsTotCost.Contains(f.Patient.PID.PatientNO))
                    {
                        totCost = FS.FrameWork.Function.NConvert.ToDecimal(hsTotCost[f.Patient.PID.PatientNO]);
                        totCost += f.FT.TotCost;
                        hsTotCost[f.Patient.PID.PatientNO] = totCost;
                    }
                    else
                    {
                        totCost = 0;
                        totCost = f.FT.TotCost;
                        hsTotCost.Add(f.Patient.PID.PatientNO, totCost);
                    }
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(f);
                    hs.Add(f.Patient.PID.PatientNO, al);

                    if (hsTotCost.Contains(f.Patient.PID.PatientNO))
                    {
                        totCost = FS.FrameWork.Function.NConvert.ToDecimal(hsTotCost[f.Patient.PID.PatientNO]);
                        totCost += f.FT.TotCost;
                        hsTotCost[f.Patient.PID.PatientNO] = totCost;
                    }
                    else
                    {
                        totCost = 0;
                        totCost = f.FT.TotCost;
                        hsTotCost.Add(f.Patient.PID.PatientNO, totCost);
                    }
                }
            }

            //分单据打印
            foreach (string str in hs.Keys)
            {
                ArrayList alPrintList = (ArrayList)hs[str];
                int pageTotNum = decimal.ToInt32(decimal.Ceiling(new decimal(alPrintList.Count / 2.0))) / this.rowCount;

                if (decimal.ToInt32(decimal.Ceiling(new decimal(alPrintList.Count / 2.0))) != this.rowCount * pageTotNum)
                {
                    pageTotNum++;
                }
                ArrayList alPrint = new ArrayList();
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in alPrintList)
                {
                    alPrint.Add(item);
                }
                this.itemCount = alPrint.Count;
                //分页打印
                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    ArrayList al = new ArrayList();

                    for (int index = pageNow * this.rowCount * 2; index < alPrint.Count && index < (pageNow + 1) * this.rowCount * 2; index++)
                    {
                        al.Add(alPrint[index]);
                    }

                    this.SetPrintData(al, pageNow + 1, pageTotNum);

                    this.pfspread.Height = this.pfspread.Height + (int)rowHeight * decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0)));
                    this.Height += (int)rowHeight * decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0)));

                    if (((FS.HISFC.Models.Base.Employee)this.rs.Operator).IsManager)
                    {
                        p.PrintPreview(5, 0, this.pMainPrint);
                    }
                    else
                    {
                        p.PrintPage(5, 0, this.pMainPrint);
                    }

                    this.pfspread.Height = height;
                    this.Height = ucHeight;
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 3打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="operCode">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            FS.HISFC.Models.Fee.Inpatient.FeeItemList info = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)al[0];

            #region label赋值
            this.lbTitle.Text = info.Memo + "患者一日清单";
            this.lbPatientNo.Text = "住院号：" + info.Patient.PID.PatientNO;
            this.lbInTimes.Text = "次数：" + info.Patient.InTimes.ToString();
            this.lbInDept.Text = "科别：" + info.Order.InDept.Name;
            this.lbBedNo.Text = "床号：" + info.Order.Patient.PVisit.PatientLocation.Bed.ID;
            this.lbInDate.Text = "入院日期：" + info.Order.Patient.PVisit.InTime.ToString();
            this.lbName.Text = "姓名：" + info.Patient.Name;
            this.lbSex.Text = "性别：" + info.Patient.Sex.Name;
            this.lbAge.Text = "年龄：" + this.rs.GetAge(info.Patient.Birthday);
            this.lbPactCode.Text = "结算种类：" + info.Patient.Pact.Name;
            this.lbStatDate.Text = "统计日期：" + new DateTime(this.dtStatDate.Year, this.dtStatDate.Month, this.dtStatDate.Day,
                00, 00, 01).ToString() + "至" + new DateTime(this.dtStatDate.Year, this.dtStatDate.Month, this.dtStatDate.Day,
                23, 59, 59).ToString();
            this.lbPrintDate.Text = "打印日期：" + this.rs.GetDateTimeFromSysDateTime().ToString();
            this.lbPrepayCost.Text = "预交款：" + info.FT.PrepayCost.ToString();

            //只有最后一页才显示合计金额
            if (inow != icount)
            {
                this.lbPrepayCost.Visible = false;
                this.lbTotCost.Visible = false;
                this.lbCount.Visible = false;
                this.lbMemo.Visible = false;
            }
            else
            {
                this.lbPrepayCost.Visible = true;
                this.lbTotCost.Visible = true;
                this.lbCount.Visible = true;
                this.lbMemo.Visible = true;
            }
            #endregion

            #region farpoint赋值
            this.neuSpread1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuSpread1_Sheet1.AddRows(i, 1);

                FS.HISFC.Models.Fee.Inpatient.FeeItemList ft = al[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (i < decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0))))
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = ft.Item.NationCode;//编码
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = ft.Item.Name;//名称
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = ft.Item.Specs;//规格
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = ft.Item.PriceUnit;//单位
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = ft.Item.Qty.ToString();//数量
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = ft.FT.TotCost.ToString();//金额
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i - decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0))), 7].Text = ft.Item.NationCode;//编码
                    this.neuSpread1_Sheet1.Cells[i - decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0))), 8].Text = ft.Item.Name;//名称
                    this.neuSpread1_Sheet1.Cells[i - decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0))), 9].Text = ft.Item.Specs;//规格
                    this.neuSpread1_Sheet1.Cells[i - decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0))), 10].Text = ft.Item.PriceUnit;//单位
                    this.neuSpread1_Sheet1.Cells[i - decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0))), 11].Text = ft.Item.Qty.ToString();//数量
                    this.neuSpread1_Sheet1.Cells[i - decimal.ToInt32(decimal.Ceiling(new decimal(al.Count / 2.0))), 12].Text = ft.FT.TotCost.ToString();//金额
                }
            }

            //总数据
            this.lbTotCost.Text = "本张清单费用小计：" + FS.FrameWork.Function.NConvert.ToDecimal(hsTotCost[info.Patient.PID.PatientNO]).ToString("F4");
            this.lbCount.Text = "本日记账共" + this.itemCount.ToString() + "条";
            #endregion

            this.resetTitleLocation();
        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation()
        {
            this.pTop.Controls.Remove(this.lbTitle);
            int with = 0;
            for (int col = 0; col < this.neuSpread1_Sheet1.ColumnCount; col++)
            {
                if (this.neuSpread1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuSpread1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.pTop.Width)
            {
                with = this.pTop.Width;
            }
            this.lbTitle.Location = new Point((with - this.lbTitle.Size.Width) / 2, this.lbTitle.Location.Y);
            this.pTop.Controls.Add(this.lbTitle);

        }
        #endregion
    }

    /// <summary>
    /// 自定义哈希表
    /// </summary>
    public class NoSortHashTable : Hashtable
    {
        private ArrayList list = new ArrayList();

        public override void Add(object key, object value)
        {
            base.Add(key, value);
            list.Add(key);
        }

        public override void Clear()
        {
            base.Clear();
            list.Clear();
        }

        public override void Remove(object key)
        {
            base.Remove(key);
            list.Remove(key);
        }

        public override ICollection Keys
        {
            get
            {
                return list;
            }
        }
    }
}
