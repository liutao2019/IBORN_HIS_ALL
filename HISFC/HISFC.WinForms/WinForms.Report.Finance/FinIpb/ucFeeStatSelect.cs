using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinIpb
{
    /// <summary>
    /// [功能描述:统计类型及大类选择]
    /// 
    /// </summary>
    public partial class ucFeeStatSelect : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucFeeStatSelect()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 用于存储统计类型
        /// </summary>
        private ArrayList reportTypeList = new ArrayList();
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant contManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 统计大类业务类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStatMger = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        /// <summary>
        /// 统计大类实体层
        /// </summary>
        private FS.HISFC.Models.Fee.FeeCodeStat feeCodeStatObj = new FS.HISFC.Models.Fee.FeeCodeStat();
        /// <summary>
        /// 用于存储选择的统计大类数组
        /// </summary>
        private List<string> feeStatList = new List<string>();
        /// <summary>
        /// 用于存储选择的统计类型
        /// </summary>
        private string reportCodeStr = string.Empty;



        #endregion



        #region 属性
        /// <summary>
        /// 用于存储选择的统计大类数组
        /// </summary>
        public List<string> FeeStatList
        {
            get
            {
                return feeStatList;
            }
            set
            {
                feeStatList = value;
            }
        }

        /// <summary>
        /// 用于存储选择的统计类型
        /// </summary>
        public string ReportCodeStr
        {
            get
            {
                return reportCodeStr;
            }
            set
            {
                reportCodeStr = value;
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 初始化方法
        /// </summary>
        private void Init()
        {
            this.cmbReportType.Items.Clear();

            reportTypeList = new ArrayList();
            reportTypeList = contManager.GetAllList(FS.HISFC.Models.Base.EnumConstant.FEECODESTAT);//获取统计类型序列

            this.cmbReportType.AddItems(reportTypeList);
            this.cmbReportType.SelectedIndex = 0;

            this.setFP(this.cmbReportType.SelectedItem.ID);
        }
        /// <summary>
        /// 设置显示fp内容
        /// </summary>
        /// <param name="reportCode">统计类型编码</param>
        private void setFP(string reportCode)
        {
            ArrayList feeStateList = new ArrayList();
            feeStateList = feeCodeStatMger.QueryFeeCodeStatByReportCode(reportCode);

            ArrayList arryFeeStat = new ArrayList();
            Hashtable feeStatHash = new Hashtable();
            foreach (FS.HISFC.Models.Fee.FeeCodeStat feeStatObj in feeStateList)
            {
                if (!feeStatHash.ContainsKey(feeStatObj.StatCate.ID))//将统计大类编码作为哈希表主键
                {
                    feeStatHash.Add(feeStatObj.StatCate.ID, feeStatObj.StatCate.Name);
                    arryFeeStat.Add(feeStatObj);//将不重复的统计大类实体添加到ArrayList中
                }
            }


            this.neuSpread1_Sheet1.RowCount = 0;
            int index = 0;

            foreach (FS.HISFC.Models.Fee.FeeCodeStat feeStatObj in arryFeeStat)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Cells[index, 1].Text = feeStatObj.StatCate.ID;
                this.neuSpread1_Sheet1.Cells[index, 2].Text = feeStatObj.StatCate.Name;

                index++;
            }

        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFeeStatSelect_Load_1(object sender, EventArgs e)
        {
            this.Init();

        }
        /// <summary>
        /// 统计类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbReportType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.cmbReportType.SelectedItem != null)
            {
                this.setFP(this.cmbReportType.SelectedItem.ID);
            }
        }
        /// <summary>
        /// 全选按钮选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuCheckBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.neuCheckBox1.Checked)
            {
                if (this.neuSpread1_Sheet1.RowCount == 0)
                {
                    MessageBox.Show("对不起，没有选择的对象！");

                    return;
                }
                else
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = false;
                }

            }
        }
        /// <summary>
        /// 反选按钮选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkNotSelect_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.chkNotSelect.Checked)
            {
                if (this.neuSpread1_Sheet1.RowCount == 0)
                {
                    MessageBox.Show("对不起，没有选择的对象！");

                    return;
                }
                else
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, 0].Text == "True")
                        {
                            this.neuSpread1_Sheet1.Cells[i, 0].Value = false;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                        }
 
                    }
                }
            }

        }
        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            this.FeeStatList = new List<string>();
            if (this.neuSpread1_Sheet1.RowCount != 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Value != null && this.neuSpread1_Sheet1.Cells[i, 0].Text == "True")
                    {
                        this.FeeStatList.Add(this.neuSpread1_Sheet1.Cells[i, 1].Text);//将统计大类的编码添加到list中
                    }
                }
            }
            this.ReportCodeStr = this.cmbReportType.SelectedItem.ID;

            if (!string.IsNullOrEmpty(this.reportCodeStr) && this.feeCodeStatMger != null)
            {
                this.FindForm().DialogResult = DialogResult.OK;

                this.Hide();
            }
            else
            {
                MessageBox.Show("请选择统计大类");

                return;
            }

        }

        #endregion

       

       

    }
}