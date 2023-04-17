using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Finance.FinOpb
{
    public partial class ucFinOpbStatFeeDoct : FS.HISFC.Components.Common.Report.ucCrossQueryBaseForFarPoint
    {
        public ucFinOpbStatFeeDoct()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 统计大类编码
        /// </summary>
        private string reportCode = "MZ01";//默认显示为门诊收费发票大类
        /// <summary>
        /// 统计大类名称
        /// </summary>
        private string reportName = string.Empty;
        /// <summary>
        /// 用于存储统计大类list
        /// </summary>
        private List<string> feeStatList = new List<string>();
        /// <summary>
        /// 用于存储拆分好的费用大类字符串
        /// </summary>
        private string feeStatStr = string.Empty;
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 医院名称
        /// </summary>
        private string hospitalName = string.Empty;
        #endregion

        #region 方法

        protected override int OnQuery(object sender, object neuObject)
        {
            string[] feeStatStr = this.feeStatList.ToArray();
            if (string.IsNullOrEmpty(reportCode))
            {
                MessageBox.Show("请选择统计大类！");
                return -1;
            }

            this.QuerySqlTypeValue = QuerySqlType.id;
            this.QuerySql = "WinForms.Report.Finance.FinOpb.ucFinOpbStatFeeDoct";
            this.DataCrossValues = "4";
            this.DataCrossRows = "0";
            this.DataCrossColumns = "2";
            this.DataBeginRowIndex = 5;

            QueryParams.Clear();
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpBeginTime.Value.ToString(), ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpEndTime.Value.ToString(), ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", reportCode, ""));
            string feeStr = string.Empty;
            if (this.feeStatList != null)
            {
                if (this.feeStatList.Count != 0)
                {
                    foreach (string str in feeStatList)
                    {
                        feeStr = feeStr+"'" + str + "',";
                    }
                    feeStr = feeStr.Substring(0, feeStr.Length - 1);
                }
            }
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", feeStr, ""));



            FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();

            employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;
            hospitalName = this.conManager.GetHospitalName();

            this.neuSpread1_Sheet1.SetText(3, 0, "统计日期：" + this.dtpBeginTime.Value.ToString() + "---" + this.dtpEndTime.Value.ToString());
            this.neuSpread1_Sheet1.Cells[3, 0].ColumnSpan = 10;
            this.neuSpread1_Sheet1.SetText(4, 0, "制表人：" + employee.Name);
            this.neuSpread1_Sheet1.Cells[4, 0].ColumnSpan = 6;
            this.neuSpread1_Sheet1.SetText(4, 6, "打印日期：" + System.DateTime.Now.ToString());
            this.neuSpread1_Sheet1.Cells.Get(4, 6).ColumnSpan = 6;
            this.neuSpread1_Sheet1.SetText(0, 0, hospitalName);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = 10;
            this.neuSpread1_Sheet1.SetText(1, 0, "门诊医生收入统计汇总表（按发票）");
            this.neuSpread1_Sheet1.Cells[1, 0].ColumnSpan = 10;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

           
            return base.OnQuery(sender, neuObject);
        }
        #endregion

        #region 事件
        /// <summary>
        /// 统计大类选择按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFeeSelect_Click(object sender, EventArgs e)
        {
            FS.Report.Finance.FinIpb.ucFeeStatSelect feeStatSelect = new FS.Report.Finance.FinIpb.ucFeeStatSelect();
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "项目选择";
            DialogResult r = FS.FrameWork.WinForms.Classes.Function.PopShowControl(feeStatSelect);
            if (r == DialogResult.Cancel)
            {
                return;
            }
            this.reportCode = string.Empty;
            this.feeStatList = new List<string>();
            if (!string.IsNullOrEmpty(feeStatSelect.ReportCodeStr))
            {
                this.reportCode = feeStatSelect.ReportCodeStr;
                this.lblMemo.Text = "您当前选择是统计类型是:[" + conManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.FEECODESTAT, feeStatSelect.ReportCodeStr.ToString()) + "]";
            }
            else
            {
                this.reportCode = "MZ01";
            }
            if (feeStatSelect.FeeStatList != null)
            {
                this.feeStatList = feeStatSelect.FeeStatList;
            }        

        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStatMger = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        private void ucFinOpbStatFeeDoct_Load(object sender, EventArgs e)
        {
            System.Collections.ArrayList feelist = new System.Collections.ArrayList();
          
            feelist = feeCodeStatMger.QueryFeeCodeStatByReportCode("MZ01");//默认是门诊自费发票
            System.Collections.Hashtable feeStatHash = new System.Collections.Hashtable();
            System.Collections.ArrayList arryFeeStat = new System.Collections.ArrayList();
            foreach (FS.HISFC.Models.Fee.FeeCodeStat feeStatObj in feelist)
            {
                if (!feeStatHash.ContainsKey(feeStatObj.StatCate.ID))//将统计大类编码作为哈希表主键
                {
                    feeStatHash.Add(feeStatObj.StatCate.ID, feeStatObj.StatCate.Name);
                    arryFeeStat.Add(feeStatObj);//将不重复的统计大类实体添加到ArrayList中
                }
            }
            foreach (FS.HISFC.Models.Fee.FeeCodeStat feeObj in arryFeeStat)
            {
                this.feeStatList.Add(feeObj.StatCate.ID);
            }

        }
        #endregion

        
    }
}
