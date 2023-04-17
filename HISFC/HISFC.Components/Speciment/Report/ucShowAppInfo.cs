using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucShowAppInfo : UserControl
    {
        public ucShowAppInfo()
        {
            InitializeComponent();
        }
        #region 变量属性
        private ArrayList alApplyTables = new ArrayList();
        /// <summary>
        /// 申请数组
        /// </summary>
        public ArrayList AlApplyTables
        {
            get
            {
                return this.alApplyTables;
            }
            set
            {
                this.alApplyTables = value;
            }
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        private int rtRs = -1;
        public int RtRs
        {
            get
            {
                return this.rtRs;
            }
            set
            {
                this.rtRs = value;
            }
        }

        /// <summary>
        /// 申请ID
        /// </summary>
        private string appID = string.Empty;
        public string AppID
        {
            get
            {
                return this.appID;
            }
            set
            {
                this.appID = value;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 当选择单条患者信息
        /// </summary>
        public delegate void GetAppRow(string appId);

        /// <summary>
        /// 当选择单条患者信息后触发
        /// </summary>
        public event GetAppRow SelectedAppRow;

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.RtRs = -1;
            this.FindForm().Close();
        }
        

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = e.Row;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                if (this.neuSpread1_Sheet1.Rows[row].Tag != null)
                {
                    this.RtRs = 1;
                    string id = this.neuSpread1_Sheet1.Cells[row, 0].Text.Trim();
                    this.AppID = id;
                    this.FindForm().Close();
                }
            }
        }
        #endregion

        #region 方法
        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        /// <summary>
        /// 界面数据初始化
        /// </summary>
        private void Init()
        {
            if ((AlApplyTables != null) && (AlApplyTables.Count > 0))
            {
                int index = 0;
                this.neuSpread1_Sheet1.RowCount = AlApplyTables.Count;
                foreach (FS.HISFC.Models.Speciment.ApplyTable appTab in this.alApplyTables)
                {
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = appTab.ApplyId.ToString();
                    this.neuSpread1_Sheet1.Cells[index, 1].Text = appTab.ApplyUserName;
                    this.neuSpread1_Sheet1.Cells[index, 2].Text = appTab.DeptName;
                    this.neuSpread1_Sheet1.Cells[index, 3].Text = appTab.ApplyTime.ToString();
                    string state = string.Empty;
                    if (appTab.ImpProcess.ToString() == "U")
                    {
                        state = "审批中";
                    }
                    else if (appTab.ImpProcess.ToString() == "O")
                    {
                        state = "审批结束";
                    }
                    else
                    {
                        state = "作废";
                    }
                    this.neuSpread1_Sheet1.Cells[index, 4].Text = state;
                    this.neuSpread1_Sheet1.Cells[index, 5].Text = appTab.ImpResult;
                    this.neuSpread1_Sheet1.Rows[index].Tag = appTab;
                    index++;
                }
            }
        }

        /// <summary>
        /// 选择当前行
        /// </summary>
        /// <param name="curRow"></param>
        private void SelectApplyRow(string id)
        {
            this.SelectedAppRow(id);
        }
        #endregion

        
    }
}
