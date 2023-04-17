using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucCallBackNum : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCallBackNum()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack callbackMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack();

        DataSet ds = new DataSet();

        private void Init()
        {
            #region 加载科室列表
            ArrayList al = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject("ALL", "全部", "");
            al.Add(obj);
            this.neuComDept.AddItems(al);
            this.neuComDept.SelectedIndex = al.Count - 1;
            #endregion

            al.Clear();
            al.Add(new FS.FrameWork.Models.NeuObject("OUTDATE", "按照出院时间查询", ""));
            al.Add(new FS.FrameWork.Models.NeuObject("CALLDATE", "按照回收日期查询", ""));
            this.neuCmbType.AddItems(al);
            this.neuCmbType.SelectedIndex = 1;
            al.Clear();
            #region  初始化时查询
            this.OnQuery();
            #endregion

        }


        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void OnQuery()
        {
            ds.Clear();
            this.neuSpread1_Sheet1.RowCount = 0;
            if (this.neuCmbType.Tag.ToString() == "OUTDATE")
            {
                this.callbackMgr.GetIsCallbackNum(this.neuComDept.Tag.ToString(), this.neuDtBegin.Value, this.neuDtEnd.Value, ref ds);
            }
            else if (this.neuCmbType.Tag.ToString() == "CALLDATE")
            {
                this.callbackMgr.GetIsCallbackNumByCallDate(this.neuComDept.Tag.ToString(), this.neuDtBegin.Value, this.neuDtEnd.Value, ref ds);
            }
            this.neuSpread1_Sheet1.DataSource = ds;
        }


        /// <summary>
        /// 查询病历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnQuery_Click(object sender, EventArgs e)
        {
            this.OnQuery();
        }


        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnExit_Click(object sender, EventArgs e)
        {
            //((Form)this.Parent).Hide();
            Form f = null;
            if ((f = this.Parent as Form) != null)
            {
                f.Hide();
            }
            else
            {
                this.Parent.Hide();
            }
        }


        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnPrint_Click(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(this.neuPanel1.Left, this.neuPanel1.Top, this.neuPanel1);

        }


    }
}
