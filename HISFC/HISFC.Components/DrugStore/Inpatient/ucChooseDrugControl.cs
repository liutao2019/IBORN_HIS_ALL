using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    public partial class ucChooseDrugControl : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucChooseDrugControl()
        {
            InitializeComponent();

            this.neuSpread1.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
        }

        public delegate void SelectControlDelegate(FS.HISFC.Models.Pharmacy.DrugControl drugControl);

        public event SelectControlDelegate SelectControlEvent;

        /// <summary>
        /// 当前选择的摆药台
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugControl drugControl = new FS.HISFC.Models.Pharmacy.DrugControl();

        /// <summary>
        /// 当前选择的科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject selectOperDept = null;

        /// <summary>
        /// 是否显示科室列表
        /// </summary>
        [Description("是否显示科室列表"),Category("设置"),DefaultValue(false)]
        public bool IsShowDept
        {
            get
            {
                return this.panelTree.Visible;
            }
            set
            {
                this.panelTree.Visible = value;
            }
        }

        /// <summary>
        /// 当前选择的科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject SelectOperDept
        {
            get
            {
                return this.selectOperDept;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void InitDeptList()
        {
            try
            {
                FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
                this.ShowControlList(((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept.ID);

                this.tvDeptTree1.IsShowPI = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化配药台列表发生错误" + ex.Message);
            }
        }

        /// <summary>
        /// 显示本科室全部摆药台列表
        /// </summary>
        public virtual void ShowControlList(string deptCode)
        {
            //清除当前显示的摆药台
            this.neuSpread1_Sheet1.Rows.Count = 0;

            //判断科室编码是否存在
            if (deptCode == "")
            {
                MessageBox.Show(Language.Msg("无效的摆药科室！没有可以选择的摆药台"));
                return;
            }

            //定义药房管理类
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

            //取本科室全部摆药台列表
            ArrayList al = drugStoreManager.QueryDrugControlList(deptCode);
            if (al == null)
            {
                MessageBox.Show(drugStoreManager.Err);
                return;
            }
            if (al.Count == 0)
            {
                MessageBox.Show(Language.Msg("您所在的科室没有设置摆药台，请先设置本科室的摆药台。"));
                return;
            }
           
            this.neuSpread1_Sheet1.Rows.Add(0, al.Count);
            FS.HISFC.Models.Pharmacy.DrugControl drugControl;
            for (int i = 0; i < al.Count; i++)
            {
                drugControl = al[i] as FS.HISFC.Models.Pharmacy.DrugControl;

                FarPoint.Win.Spread.CellType.ButtonCellType btnType = new FarPoint.Win.Spread.CellType.ButtonCellType();
                btnType.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(225)), ((System.Byte)(243)));
                btnType.Text = drugControl.Name;
                btnType.TextDown = drugControl.Name;
                this.neuSpread1_Sheet1.Cells[i, 0].CellType = btnType;

                this.neuSpread1_Sheet1.Cells[i, 1].Text = drugControl.SendType == 0 ? "全部" : (drugControl.SendType == 1 ? "集中" : "临时");
                this.neuSpread1_Sheet1.Cells[i, 2].Text = drugControl.ShowLevel == 0 ? "显示科室汇总" : (drugControl.ShowLevel == 1 ? "显示科室明细" : "显示患者明细");
                this.neuSpread1_Sheet1.Rows[i].Tag = drugControl;
            }

            if (al.Count == 1)
            {
                this.drugControl = al[0] as FS.HISFC.Models.Pharmacy.DrugControl;

                if (this.SelectControlEvent != null)
                {
                    this.SelectControlEvent(this.drugControl);
                }
                return;
            }
        }

         private void tvDeptTree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.ShowControlList((e.Node.Tag as FS.HISFC.Models.Base.Department).ID);

                this.selectOperDept = e.Node.Tag as FS.HISFC.Models.Base.Department;
            }
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                this.drugControl = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Pharmacy.DrugControl;

                if (this.SelectControlEvent != null)
                {
                    this.SelectControlEvent(this.drugControl);
                }
            }
        }

    }
}
