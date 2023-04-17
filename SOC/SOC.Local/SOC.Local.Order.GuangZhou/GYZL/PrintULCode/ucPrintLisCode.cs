using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Order.GuangZhou.Gyzl.PrintULCode
{
    /// <summary>
    /// 门诊打印LIS条码界面
    /// </summary>
    public partial class ucPrintLisCode : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrintLisCode()
        {
            InitializeComponent();
        }

        #region 变量

        #region 业务层变量

        /// <summary>
        /// 门诊费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 药品管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 参数控制业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 医嘱函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 常数业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 住院条码打印业务层
        /// </summary>
        private SOC.Local.Order.GuangZhou.Gyzl.PrintULCode.Unitl lisManager = new SOC.Local.Order.GuangZhou.Gyzl.PrintULCode.Unitl();

        #endregion

        /// <summary>
        /// 已选择患者
        /// </summary>
        private ArrayList alPatientInfo = null;

        /// <summary>
        /// 存放科室信息
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 配置文件
        /// </summary>
        private string printULXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\printULCode.xml";

        FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();

        FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();

        Unitl com = new Unitl();

        #endregion

        #region 属性

        #endregion

        #region 初始化

        /// <summary>
        /// 设置格式
        /// </summary>
        private void setFP()
        {
            txtType.ReadOnly = true;
            numType.DecimalPlaces = 0;

            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.姓名].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.处方号].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.项目].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.条码号].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.是否接收].CellType = txtType;

            if (System.IO.File.Exists(printULXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, printULXml);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, printULXml);
            }

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            this.setTime();
            this.setFP();
            this.clear();
            this.showCheckBox();
        }

        #endregion

        #region 公共

        /// <summary>
        /// 设置颜色(高亮度显示最后一条clinic医嘱)
        /// </summary>
        /// <returns></returns>
        private int ShowColor()
        {
            //取得最大clinic_code
            int maxClinic = 0;
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return -1;
            }
            FS.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (item.ID == maxClinic.ToString())
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(i, 0, false);
                }
            }
            return 0;
        }

        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="patient"></param>
        private void SetPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {

        }

        private string GetAgeByBirthday(DateTime birthday)
        {
            TimeSpan ts = DateTime.Now - birthday;
            int year = ts.Days / 365;
            return year + "岁";
        }

        #endregion

        #region  LIS

        /// <summary>
        /// 打印Lis条码
        /// </summary>
        private void printLisBarCode()
        {
            ArrayList cardNoList = new ArrayList();
            foreach (FarPoint.Win.Spread.Row row in this.neuSpread1_Sheet1.Rows)
            {
                string cardNo = ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)row.Tag).Patient.PID.CardNO;
                if (!cardNoList.Contains(cardNo))
                {
                    cardNoList.Add(cardNo);
                }
            }

            foreach (string cardNo in cardNoList)
            {
                try
                {
                    lisManager.PrintLisBarCode(cardNo, this.dtpBeginTime.Value, this.dtpEndTime.Value);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// 作废条码
        /// </summary>
        private void cancelLisBarCode()
        {
            ArrayList nameList = new ArrayList();
            ArrayList cardNoList = new ArrayList();
            foreach (FarPoint.Win.Spread.Row row in this.neuSpread1_Sheet1.Rows)
            {
                string name = ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)row.Tag).Memo;
                string cardNo = ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)row.Tag).Patient.PID.CardNO;
                if (!nameList.Contains(name))
                {
                    nameList.Add(name);
                }
                if (!cardNoList.Contains(cardNo))
                {
                    cardNoList.Add(cardNo);
                }
            }
            DialogResult dr = MessageBox.Show("提示", "确认作废" + this.ArrayToOneString(nameList) + "全部条码", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                //未完成,根据病人姓名提示是否作废
                foreach (string cardNo in cardNoList)
                {
                    try
                    {
                        lisManager.CancelLisBarCode(cardNo, this.dtpBeginTime.Value, this.dtpEndTime.Value, constantManager.Operator.ID);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        private string ArrayToOneString(ArrayList list)
        {
            StringBuilder result = new StringBuilder();
            foreach (string temp in list)
            {
                result.Append(temp + ",");
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        #endregion

        #region 方法

        private void setTime()
        {
            DateTime now = this.constantManager.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            this.dtpEndTime.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        }

        /// <summary>
        /// 显示树节点复选框
        /// </summary>
        private void showCheckBox()
        {
            if (this.tv != null)
            {
                this.tv.CheckBoxes = true;
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void clear()
        {
            neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 添加项目明细
        /// </summary>
        /// <param name="details"></param>
        private void addDetail(ArrayList details)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            int curRow = 0;
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in details)
            {
                curRow = this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Rows[curRow].Locked = true;
                this.neuSpread1_Sheet1.Rows[curRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Rows[curRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = feeItemList.Memo;
                this.neuSpread1_Sheet1.Cells[curRow, 1].Text = feeItemList.RecipeNO;
                this.neuSpread1_Sheet1.Cells[curRow, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[curRow, 2].Text = feeItemList.Item.Name;
                this.neuSpread1_Sheet1.Cells[curRow, 3].Text = feeItemList.Order.ApplyNo;
                this.neuSpread1_Sheet1.Cells[curRow, 4].Text = feeItemList.NoBackQty == 0 ? "是" : "否";
                this.neuSpread1_Sheet1.Cells[curRow, 4].Font = new Font("宋体", 9, FontStyle.Bold);
                this.neuSpread1_Sheet1.Rows[curRow].Tag = feeItemList;
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 菜单加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.tv = sender as TreeView;
            this.init();
            return null;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="alValues"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.alPatientInfo = alValues;
            //查询当前病区患者需要打印检验条码的信息
            ArrayList al = lisManager.QueryPatientWithULItemByNurseCellCodeAndDate(((FS.HISFC.Models.Base.Employee)constantManager.Operator).Dept.ID, dtpBeginTime.Value, dtpEndTime.Value);
            //查询
            ArrayList cardList = new ArrayList();
            foreach (FS.HISFC.Models.RADT.Patient patient in this.alPatientInfo)
            {
                cardList.Add(patient.PID.CardNO);
            }
            ArrayList result = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in al)
            {
                if (cardList.Contains(feeItemList.Patient.PID.CardNO))
                {
                    result.Add(feeItemList);
                }
            }
            this.addDetail(result);
            return 0;
        }

        /// <summary>
        /// 作废条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cancelLisBarCode();
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printLisBarCode();
        }

        /// <summary>
        /// 保存显示设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, printULXml);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }
        #endregion

        /// <summary>
        /// 列设置枚举
        /// </summary>
        enum EnumColSet
        {
            姓名,
            处方号,
            项目,
            条码号,
            是否接收
        }

    }

}
