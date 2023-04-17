using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.QiaoTou
{
    public partial class ucQueryBaseForDataWindowBySQL : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucQueryBaseForDataWindowBySQL()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 费用报表业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();

        /// <summary>
        /// DataWindow所要用到的SQL语句
        /// </summary>
        private string sqlID = string.Empty;

        /// <summary>
        /// DataWindow所要用到的SQL语句
        /// </summary>
        [Category("查询设置"),Description("DataWindow使用的SQL语句")]
        public string SQLID
        {
            get
            {
                return this.sqlID;
            }
            set
            {
                this.sqlID = value;
            }
        }

        /// <summary>
        /// 操作员工号和姓名
        /// </summary>
        private string operNameObject = "t_oper";

        /// <summary>
        /// 操作员工号和姓名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        [Category("报表设置"), Description("显示操作员的控件名称")]
        public string OperNameObject
        {
            set
            {
                this.operNameObject = value;
            }
            get
            {
                return this.operNameObject;
            }
        }

        /// <summary>
        /// 设置操作人
        /// </summary>
        private void SetOper()
        {
            if (this.dwMain != null)
            {
                if (!string.IsNullOrEmpty(this.OperNameObject))
                {
                    try
                    {
                        this.dwMain.Modify(this.operNameObject + ".Text = " + "'打印人:" + this.feeMgr.Operator.ID + "(" + this.feeMgr.Operator.Name + ")'");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 显示操作时间的控件名称
        /// </summary>
        string operDateObjectName = "t_date";

        [Category("报表格式设置"), Description("显示操作时间的控件名称")]
        public string OperDateObjectName
        {
            get
            {
                return this.operDateObjectName;
            }
            set
            {
                this.operDateObjectName = value;
            }
        }

        /// <summary>
        ///  显示操作时间
        /// </summary>
        protected virtual void SetOperDate()
        {
            if (this.dwMain != null)
            {
                if (this.operDateObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.operDateObjectName + ".Text = " + "'打印时间：" + this.feeMgr.GetDateTimeFromSysDateTime().ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 显示查询时间段的控件名称
        /// </summary>
        string queryDateObjectName = "t_query";

        [Category("报表格式设置"), Description("显示查询时间段的控件名称")]
        public string QueryDateObjectName
        {
            get
            {
                return this.queryDateObjectName;
            }
            set
            {
                this.queryDateObjectName = value;
            }
        }

        /// <summary>
        ///  显示统计时间段
        /// </summary>
        protected virtual void SetOperQueryDate(DateTime beginTime, DateTime endTime)
        {
            if (this.dwMain != null)
            {
                if (this.queryDateObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.queryDateObjectName + ".Text = " + "'统计时间：" + beginTime.ToShortDateString() + " 至 " + endTime.ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {

            Cursor = Cursors.WaitCursor;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据,请等待....");

            Application.DoEvents();

            if (this.isRetrieveArgsOnlyTime)
            {
                this.RetrieveMainOnlyByTime();
            }
            else
            {
                this.OnRetrieve();
            }

            this.SetOper();
            this.SetOperDate();
            this.SetOperQueryDate(this.beginTime, this.endTime);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Cursor = Cursors.Arrow;

            return 1;
        }

        /// <summary>
        /// 只是时间事件查询
        /// </summary>
        /// <returns></returns>
        protected override int RetrieveMainOnlyByTime()
        {
            if (this.dwMain == null)
            {
                return -1;
            }

            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            if (!string.IsNullOrEmpty(this.SQLID))
            {
                string sqlStr = string.Empty;
                DataSet dsResult = new DataSet();
                if (this.feeMgr.Sql.GetSql(this.SQLID, ref sqlStr) < 0)
                {
                    MessageBox.Show("找不到SQL语句:" + this.SQLID + " " + this.feeMgr.Err);
                    return -1;
                }

                try
                {
                    sqlStr = string.Format(sqlStr, this.beginTime.ToString(), this.endTime.ToString());
                    if (this.feeMgr.ExecQuery(sqlStr, ref dsResult) < 0)
                    {
                        MessageBox.Show("执行SQL语句失败!" + this.feeMgr.Err);
                        return -1;
                    }

                    if (dsResult == null)
                    {
                        MessageBox.Show("执行SQL语句失败!");
                        return -1;
                    }

                    DataTable dt = dsResult.Tables[0];

                    if (this.dwMain.Retrieve(dt) < 0)
                    {
                        MessageBox.Show(this.dwMain.Error);
                        return -1;
                    }

                    return 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }

            return this.dwMain.Retrieve(beginTime, endTime);
        }


        private void ucQueryBaseForDataWindowBySQL_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            DateTime dtNow = this.feeMgr.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = new DateTime(dtNow.Year, dtNow.Month, 1, 0, 0, 0);
            //this.dtpBeginTime.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
            this.dtpEndTime.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);
        }

        #endregion

        

    }
}
