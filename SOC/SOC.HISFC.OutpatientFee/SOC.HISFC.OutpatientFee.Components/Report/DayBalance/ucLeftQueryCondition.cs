using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.DayBalance
{
    public partial class ucLeftQueryCondition : FS.FrameWork.WinForms.Controls.ucBaseControl, ILeftQueryCondition
    {

        public ucLeftQueryCondition()
        {
            InitializeComponent();
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.tvDeptList1.DoubleClick+=new EventHandler(tvDeptList1_DoubleClick); 
        }

        void tvDeptList1_DoubleClick(object sender, EventArgs e)
        {
            if (this.tvDeptList1.SelectedNode == null)
            {
                return;
            }

            if (this.OnFilterHandler != null)
            {
                this.OnFilterHandler(this.tvDeptList1.SelectedNode.Name);
            }
        }

        #region ITopQueryCondition 成员

        public int Init()
        {
            DateTime dtNow = CommonController.CreateInstance().GetSystemTime();
            this.dtBeginTime.Value = dtNow;
            return this.query();
        }

        public void AddControls(List<QueryControl> list)
        {
           
        }

        public object GetValue(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                
            }

            return "";
        }

        public object GetText(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {

            }

            return "";
        }


        public object GetValues(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {

            }

            return "";
        }

        public object GetTexts(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {

            }

            return "";
        }

        public event ICommonReportController.FilterHandler OnFilterHandler;

        #endregion


        private int query()
        {
            this.tvDeptList1.Nodes.Clear();
            TreeNode parentNode = new TreeNode();
            parentNode.Text = this.dtBeginTime.Value.ToString("yyyy年MM月");

            string error = string.Empty;
            List<FS.FrameWork.Models.NeuObject> list = Function.GetDayReportBizProcess().GetClinicFeeBalanceListByMonth(this.dtBeginTime.Value);
            if (list == null)
            {
                CommonController.CreateInstance().MessageBox("获取日结信息失败，原因：" + Function.GetDayReportBizProcess().Err, MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.FrameWork.Models.NeuObject obj in list)
            {
                TreeNode childNode = new TreeNode();

                string checkFlag = "";//审核标识
                if ("1".Equals(obj.Memo))
                {
                    checkFlag = "[已审核]";
                }

                childNode.Text = obj.Name + checkFlag;
                childNode.Name = obj.ID;
                childNode.ImageIndex = 2;
                childNode.SelectedImageIndex = 3;
                parentNode.Nodes.Add(childNode);
            }

            this.tvDeptList1.Nodes.Add(parentNode);
            this.tvDeptList1.ExpandAll();
            return 1;
        }

        void btnQuery_Click(object sender, EventArgs e)
        {
            this.query();
        }
    }
}
