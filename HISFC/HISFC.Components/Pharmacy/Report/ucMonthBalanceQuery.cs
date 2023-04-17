using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [功能描述: 药品月结报表查询打印 {796539DF-D61F-4ff6-B6A1-CF3124BD8F9D}]
    /// [创 建 者: Sunjh]
    /// [创建时间: 2010-09-26]   
    /// </summary>
    public partial class ucMonthBalanceQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthBalanceQuery()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        private System.Collections.Hashtable hsMonthDate = new System.Collections.Hashtable();

        #endregion

        #region 方法

        /// <summary>
        /// 根据二级权限初始化查寻科室
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        public void InitDept()
        {            
            this.cmbDept.Items.Clear();
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            ArrayList al = phaConsManager.QueryDeptConstantList();
            if (al == null)
            {
                MessageBox.Show(phaConsManager.Err);
                return;
            }
            this.cmbDept.AddItems(al);
        }

        /// <summary>
        /// 查询月结单
        /// </summary>
        public void Query()
        {
            ArrayList alCmb = new ArrayList();
            ArrayList al = itemManager.QueryMonthBalanceTotal(this.cmbDept.Tag.ToString(), this.dtStart.Value.ToString(), this.dtEnd.Value.ToString());
            this.hsMonthDate.Clear();
            for (int i = 0; i < al.Count; i++)
            {
                string[] str = al[i] as string[];
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.Name = str[1];
                alCmb.Add(obj);
                hsMonthDate.Add(str[1], str);
            }
            this.cmbMonth.AddItems(alCmb);
            neuLabel13.Text = "检索到 " + al.Count.ToString() + " 条月结记录";
        }

        /// <summary>
        /// 读取信息到表格
        /// </summary>
        /// <param name="hsStr"></param>
        private void SetTable(string hsStr)
        {
            string[] str = this.hsMonthDate[hsStr] as string[];

            
            this.neuLabel8.Text = "制表时间：" + DateTime.Now.ToString();
            this.neuLabel2.Text = "库存科室：" + this.cmbDept.Text;
            this.neuLabel3.Text = "月结时间：" + str[0] + " - " + str[1];
            this.neuLabel4.Text = Convert.ToDateTime(str[1]).Year.ToString() + "年" + Convert.ToDateTime(str[1]).Month.ToString() + "月";
            //上期转入
            this.fpMonth.Cells[0, 1].Text = string.Format("{0:0.00}",Convert.ToDecimal(str[3]));
            this.fpMonth.Cells[0, 2].Text = string.Format("{0:0.00}",Convert.ToDecimal(str[2]));
            this.fpMonth.Cells[0, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[2]) - Convert.ToDecimal(str[3]));
            this.fpMonth.Cells[0, 4].Text = "收";

            //入库金额
            this.fpMonth.Cells[1, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[5]));
            this.fpMonth.Cells[1, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[4]));
            this.fpMonth.Cells[1, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[4]) - Convert.ToDecimal(str[5]));
            this.fpMonth.Cells[1, 4].Text = "收";

            //调价盈亏
            this.fpMonth.Cells[2, 1].Text = string.Format("{0:0.00}", 0);/////////缺字段
            this.fpMonth.Cells[2, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[14]));
            this.fpMonth.Cells[2, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[14]));
            this.fpMonth.Cells[2, 4].Text = "收";

            //盘点盈亏
            this.fpMonth.Cells[3, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[13]));
            this.fpMonth.Cells[3, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[12]));
            this.fpMonth.Cells[3, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[12]) - Convert.ToDecimal(str[13]));
            this.fpMonth.Cells[3, 4].Text = "收";

            //出库金额
            this.fpMonth.Cells[4, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[7]));
            this.fpMonth.Cells[4, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[6]));
            this.fpMonth.Cells[4, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[6]) - Convert.ToDecimal(str[7]));
            this.fpMonth.Cells[4, 4].Text = "支";

            //特殊入库
            this.fpMonth.Cells[5, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[9]));
            this.fpMonth.Cells[5, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[8]));
            this.fpMonth.Cells[5, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[8]) - Convert.ToDecimal(str[9]));
            this.fpMonth.Cells[5, 4].Text = "收";

            //特殊出库
            this.fpMonth.Cells[6, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[11]));
            this.fpMonth.Cells[6, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[10]));
            this.fpMonth.Cells[6, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[10]) - Convert.ToDecimal(str[11]));
            this.fpMonth.Cells[6, 4].Text = "支";

            //本期合计
            this.fpMonth.Cells[7, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[3]) + Convert.ToDecimal(str[5]) +
                0 + Convert.ToDecimal(str[13]) - Convert.ToDecimal(str[7])
                - Convert.ToDecimal(str[9]) - Convert.ToDecimal(str[11]));
            this.fpMonth.Cells[7, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[2]) + Convert.ToDecimal(str[4]) +
                Convert.ToDecimal(str[14]) + Convert.ToDecimal(str[12]) - Convert.ToDecimal(str[6])
                - Convert.ToDecimal(str[8]) - Convert.ToDecimal(str[10]));
            this.fpMonth.Cells[7, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(this.fpMonth.Cells[7, 2].Text) - Convert.ToDecimal(this.fpMonth.Cells[7, 1].Text));
            this.fpMonth.Cells[7, 4].Text = "";

            //期末库存
            this.fpMonth.Cells[8, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[15]));
            this.fpMonth.Cells[8, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[16]));
            this.fpMonth.Cells[8, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(str[16]) - Convert.ToDecimal(str[15]));
            this.fpMonth.Cells[8, 4].Text = "";

            //帐户差额
            this.fpMonth.Cells[9, 1].Text = string.Format("{0:0.00}", Convert.ToDecimal(this.fpMonth.Cells[7, 1].Text) - Convert.ToDecimal(this.fpMonth.Cells[8, 1].Text));
            this.fpMonth.Cells[9, 2].Text = string.Format("{0:0.00}", Convert.ToDecimal(this.fpMonth.Cells[7, 2].Text) - Convert.ToDecimal(this.fpMonth.Cells[8, 2].Text));
            this.fpMonth.Cells[9, 3].Text = string.Format("{0:0.00}", Convert.ToDecimal(this.fpMonth.Cells[9, 2].Text) - Convert.ToDecimal(this.fpMonth.Cells[9, 1].Text));
            this.fpMonth.Cells[9, 4].Text = "";

        }
        #endregion

        #region 事件

        private void ucMonthBalanceQuery_Load(object sender, EventArgs e)
        {
            this.dtStart.Value = DateTime.Now.Date;
            this.dtEnd.Value = DateTime.Now.Date.AddDays(1);
            this.InitDept();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print printPanel = new FS.FrameWork.WinForms.Classes.Print();
            printPanel.PrintPreview(10, 10, this.neuPanel1);
            return base.OnPrint(sender, neuObject);
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetTable(this.cmbMonth.Text);
        }

        #endregion        

    }
}
