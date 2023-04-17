using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.WinForms.DrugStore
{
    public partial class frmChooseInpatientControl : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmChooseInpatientControl()
        {
            InitializeComponent();

            this.ProgressRun(true);
        }

        /// <summary>
        /// 药柜处理
        /// </summary>
        private void SetArkDept(ref FS.FrameWork.Models.NeuObject operDept)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.DeptConstant deptCons = phaConsManager.QueryDeptConstant(operDept.ID);
            if (deptCons == null)
            {
                MessageBox.Show(Language.Msg("根据科室编码获取科室常数信息发生错误") + phaConsManager.Err);
                return;
            }

            if (deptCons.IsArk)         //对药柜管理科室进行以下处理
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                ArrayList al = managerIntegrate.LoadPhaParentByChildren(operDept.ID);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show(Language.Msg("获取科室结构信息发生错误") + managerIntegrate.Err);
                    return;
                }

                FS.HISFC.Models.Base.DepartmentStat deptStat = al[0] as FS.HISFC.Models.Base.DepartmentStat;
                if (deptStat.PardepCode.Substring(0, 1) == "S")     //上级节点为分类编码 不进行处理
                {
                    return;
                }
                else
                {
                    operDept.ID = deptStat.PardepCode;
                    operDept.Name = deptStat.PardepName;
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //退出
            if (keyData == Keys.F10)
            {
                this.Close();
            }
            //显示摆药单
            if (keyData == Keys.F12)
            {
                this.ShowDrugApprove();
            }
            return base.ProcessDialogKey(keyData);
        }

        private DrugStore.frmInpatientDrug frmDrugManager = null;

        private frmDrugBillApprove frmApprove = null;

        /// <summary>
        /// 摆药管理
        /// </summary>
        private void ShowDrugManager(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            frmDrugManager  = new frmInpatientDrug();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            if (drugControl.Dept == null || drugControl.Dept.ID == "")
            {
                drugControl.Dept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept.Clone();
            }

            frmDrugManager.SetDrugControl(drugControl);

            frmDrugManager.Show();
        }

        /// <summary>
        /// 摆药单补打 摆药核准显示
        /// </summary>
        private void ShowDrugApprove()
        {
            frmApprove = new frmDrugBillApprove();

            if (this.ucChooseDrugControl1.SelectOperDept != null)
            {
                frmApprove.OperDept = this.ucChooseDrugControl1.SelectOperDept;
            }

            frmApprove.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

            FS.FrameWork.Models.NeuObject operDept = ((FS.HISFC.Models.Base.Employee)drugStoreManager.Operator).Dept.Clone();

            this.SetArkDept(ref operDept);

            if (this.Tag.ToString() != "1")
            {
                //不显示科室列表
                this.ucChooseDrugControl1.IsShowDept = false;
                this.ucChooseDrugControl1.ShowControlList(operDept.ID);
            }
            else
            {
                this.ucChooseDrugControl1.IsShowDept = true;
                this.ucChooseDrugControl1.InitDeptList();
            }
        }

        private void ucChooseDrugControl1_SelectControlEvent(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            this.ShowDrugManager(drugControl);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tsbExit)
                this.Close();
            if (e.ClickedItem == this.tsbBill)
                this.ShowDrugApprove();
        }

        protected override void OnClosed(EventArgs e)
        {
            //主窗口关闭时 关闭所有打开的附属窗口
            if (this.frmApprove != null)
            {
                this.frmApprove.Close();
            }
            if (this.frmDrugManager != null)
            {
                this.frmDrugManager.Close();
            }
            base.OnClosed(e);
        }

    }
}