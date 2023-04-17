using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    /// <summary>
    /// [功能描述: 静态盘点]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-08]<br></br>
    /// </summary>
    public partial class ucDynamicCheck : ucBaseCheck, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDynamicCheck()
        {
            InitializeComponent();
        }

        #region 属性
        private int curDays = 90;

        /// <summary>
        /// CurDays天内无入出库业务的库存为0的不封账
        /// </summary
        [Description("CurDays天内无入出库业务的库存为0的不封账"), Category("设置"), Browsable(true)]
        public int Days
        {
            get { return curDays; }
            set { curDays = value; }
        }

        #endregion

        #region 权限处理

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();
            int param = Function.ChoosePriveDept("0305", ref priveDept);
            if (param == 0 || param == -1 || priveDept == null || string.IsNullOrEmpty(priveDept.ID))
            {
                return -1;
            }

            this.nlbPriveDept.Text = "您选择的科室是【" + priveDept.Name + "】";

            this.curStockDept = priveDept;

            return 1;
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
            return this.SetPriveDept();
        }

        #endregion

        #region 封账

        protected int FStorage()
        {
            if (!this.CheckFStorePrive())
            {
                Function.ShowMessage("您没有权限！", MessageBoxIcon.Information);
                return 0;
            }

            DataTable dtAddMofity = this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity != null && dtAddMofity.Rows.Count > 0)
            {
                Function.ShowMessage("数据更改后应用封账会丢失更改内容，请注意保存!", MessageBoxIcon.Information);
                return 0;
            }

            object interfaceImplement = InterfaceManager.GetExtendBizImplement();

            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (interfaceImplement == null)
            {
                return DefaultFStorage();
            }

            ArrayList alDetail = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)interfaceImplement).SetCheckDetail(this.curStockDept.ID);
            if (alDetail == null)
            {
                return DefaultFStorage();
            }

            this.curIDataDetail.FpSpread.DataSource = null;
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.hsCheck.Clear();

            foreach (FS.HISFC.Models.Pharmacy.Check check in alDetail)
            {
                this.curCheckBillNO = check.CheckNO;

                if (this.AddObjectToDataTable(check) == -1)
                {
                    continue;
                }
            }
            this.curIDataDetail.FpSpread.DataSource = this.dtDetail.DefaultView;
            this.SetTotInfoAndColor();
            this.dtDetail.AcceptChanges();

            this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);

            this.ShowList();

            Function.ShowMessage("封账完成!", MessageBoxIcon.Information);
            return 1;
        }

        protected int DefaultFStorage()
        {
            string checkManagerState = "-1";
            if (hsCheck != null && hsCheck.Count > 0)
            {
                foreach (FS.HISFC.Models.Pharmacy.Check check in hsCheck.Values)
                {
                    if (check.BatchNO.ToUpper() == "ALL")
                    {
                        checkManagerState = "1";
                    }
                    else
                    {
                        checkManagerState = "0";
                    }
                    break;
                }
            }
            using (frmSetFStory frmSetFStory = new frmSetFStory(this.curStockDept.ID, this.curCheckBillNO, checkManagerState, this.settingFileName))
            {
                //如果需要frmSetFStory返回数组，则frmSetFStory不能关闭后调用其属性变量
                //目前采用封账就自动保存到数据库的方式，一是问了快捷，保证封账形成的数据和保存的数据时间统一
                frmSetFStory.StartPosition = FormStartPosition.CenterScreen;
                if (frmSetFStory.ShowDialog(this) == DialogResult.Cancel)
                {
                    return 0;
                }
            }

            this.curIDataDetail.FpSpread.DataSource = this.dtDetail.DefaultView;
            this.SetTotInfoAndColor();
            this.dtDetail.AcceptChanges();

            this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);

            this.ShowList();


            return 1;
        }

        private int FStoreOnlyRefreshStorageInfo()
        {
            if (!this.CheckFStorePrive())
            {
                Function.ShowMessage("您没有权限！", MessageBoxIcon.Information);
                return 0;
            }

            DataTable dtAddMofity = this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity != null && dtAddMofity.Rows.Count > 0)
            {
                Function.ShowMessage("数据更改后应用封账会丢失更改内容，请注意保存!", MessageBoxIcon.Information);
                return 0;
            }

            if (string.IsNullOrEmpty(this.curCheckBillNO))
            {
                Function.ShowMessage("请选择盘点单!", MessageBoxIcon.Information);
                return 0;
            }

            DialogResult dr = MessageBox.Show("封账开始前，请通知库房所有人员停止出入库、调价等其他业务操作\n\n确认封账吗?", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍后...");
            Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int param = this.itemMgr.UpdateCheckStaticFStroeInfo(this.curStockDept.ID, this.curCheckBillNO);
            if (param == 1)
            {
                param = this.itemMgr.RefreshCheckBillStorageInfo(this.curStockDept.ID, this.curCheckBillNO);
                if (param == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("数据已经发生变化，请刷新!", MessageBoxIcon.Information);
                    return param;
                }
                else if(param <0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("按原单重新封账发生错误，请向系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return param;
                }
            }
            else if (param == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("数据已经发生变化，请刷新!", MessageBoxIcon.Information);
                return param;
            }
            else if(param < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("按原单重新封账发生错误，请向系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            Function.ShowMessage("封账成功！", MessageBoxIcon.Information);

            this.ShowList();

            return 1;
        }
        #endregion

        #region 工具栏
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("库存表", "切换到库存表，可以单个药品封账", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            toolBarService.AddToolButton("盘点单", "切换到盘点单列表", FS.FrameWork.WinForms.Classes.EnumImageList.S申请单, true, false, null);
            toolBarService.AddToolButton("批量封账", "批量封账", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            toolBarService.AddToolButton("原单重封", "保证品种、顺序、盘点方式不变的前提下重新封账库存信息", FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);

            return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "库存表")
            {               
                if (this.IsShowChooseList)
                {
                    this.ucTreeViewChooseList.TreeView.Visible = false;
                    this.FreshDataChooseList();
                }
            }
            else if (e.ClickedItem.Text == "盘点单")
            {
                if (this.IsShowChooseList)
                {
                    this.ucTreeViewChooseList.TreeView.Visible = true;
                }
            }
            else if (e.ClickedItem.Text == "批量封账")
            {
                this.FStorage();
            }
            else if (e.ClickedItem.Text == "原单重封")
            {
                this.FStoreOnlyRefreshStorageInfo();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion
    }
}
