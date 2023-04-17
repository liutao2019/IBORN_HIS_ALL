using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Logistics.Pharmacy
{
    public partial class ucPhaDeptDrugInputDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        private string DeptCode = string.Empty;
        private string name;
        private DateTime dt;
        public ucPhaDeptDrugInputDetail()
        {
            InitializeComponent();
        }

        #region 管理类


        FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        //FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        System.Collections.ArrayList drugDeptList = new System.Collections.ArrayList();
        FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();

        #endregion

        #region 字段
        /// <summary>
        ///过滤字符表达式

        //private string querycode = string.Empty;
        /// <summary>
        /// 过滤字符串
        /// </summary>

        private string queryStr = "(TRADE_NAME like '{0}%') or (pha_com_input_spell_code like '{0}%')";
        
        #endregion

        /// <summary>
        /// 过滤文本框事件

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            //this.querycode = ((FS.FrameWork.WinForms.Controls.NeuTextBox)sender).Text.Trim();
            string str = ((FS.FrameWork.WinForms.Controls.NeuTextBox)sender).Text.Trim().Replace(@"\", "").Replace(@"'", "").ToUpper();
         
            try
            {
                DataView dv = this.dwMain.Dv;
                if (dv == null)
                {
                    return;
                }

                if (str.Equals(""))
                {
                    dv.RowFilter = "";
                    return;
                }
                else
                {
                    str = string.Format(this.queryStr, str);
                    dv.RowFilter = str;

                }
                
                //if (!this.querycode.Equals(""))
                //{
                //    this.dwMain.SetFilter("(pha_com_input_trade_name LIKE '" + querycode + "%') or (pha_com_input_spell_code LIKE '" + querycode.ToUpper() + "%')");
                //}
                //else
                //{
                //    this.dwMain.SetFilter("");

                //    dv.RowFilter = "";
                //    return;
                //}
                //this.dwMain.Filter();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "提示");
            }
        }

        protected override void OnLoad()
        {
            this.Init();
            drugDeptList = manager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);

            foreach (FS.FrameWork.Models.NeuObject con in drugDeptList)
            {
                this.deptComboBox1.Items.Add(con);
            }
            if (deptComboBox1.Items.Count >= 0)
            {
                deptComboBox1.SelectedIndex = 0;
                DeptCode = ((FS.FrameWork.Models.NeuObject)deptComboBox1.Items[0]).ID;
            }
            base.OnLoad();
            dt = System.DateTime.Now;
			this.name = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
            //FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();
            //ArrayList alCon = interManager.GetDepartment();
            //FS.FrameWork.Models.NeuObject neuO = new FS.FrameWork.Models.NeuObject();
            //alCon.Insert(0, neuO);

            //this.cmbQuery.AddItems(alCon);
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.dtpBeginTime.Value>this.dtpEndTime.Value)
            {
                MessageBox.Show("结束时间不能小于开始时间");
            }
            
            dwMain.Retrieve(DeptCode, this.dtpBeginTime.Value, this.dtpEndTime.Value,this.name);
            return base.OnRetrieve(DeptCode, this.dtpBeginTime.Value, this.dtpEndTime.Value,this.name);
        }

        //private void cmbQuery_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //DeptCode = this.cmbQuery.Tag.ToString();
        //    DeptCode = ((FS.FrameWork.Models.NeuObject)this.deptComboBox1.Items[this.deptComboBox1.SelectedIndex]).ID;
        //}

        private void deptComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.deptComboBox1.SelectedIndex >= 0)
            {
                DeptCode = ((FS.FrameWork.Models.NeuObject)this.deptComboBox1.Items[this.deptComboBox1.SelectedIndex]).ID;
            }
        }
    }
}
