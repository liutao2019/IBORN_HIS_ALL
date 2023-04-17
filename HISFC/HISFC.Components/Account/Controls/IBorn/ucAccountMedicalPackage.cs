using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    public partial class ucAccountMedicalPackage : ucBaseControl
    {


        /// <summary>
        /// 工具条
        /// </summary>
        protected ToolBarService _toolBarService = new ToolBarService();

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();


        /// <summary>
        /// 套餐修改对话框
        /// </summary>
        private FS.HISFC.Components.Account.Forms.frmMedicalPackage packageForm = new FS.HISFC.Components.Account.Forms.frmMedicalPackage();


        private FS.HISFC.Components.Account.Forms.frmMedicalPackageDetail detailForm = new FS.HISFC.Components.Account.Forms.frmMedicalPackageDetail();


        public ucAccountMedicalPackage()
        {
            InitializeComponent();

            Init();
        }


        private void Init()
        {

            BindEvents();
            InitPackage();

        }


        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            _toolBarService.AddToolButton("新建套餐", "新建套餐", (int)FS.FrameWork.WinForms.Classes.EnumImageList.R入库单, true, false, null);
            _toolBarService.AddToolButton("修改套餐", "修改套餐", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S申请单, true, false, null);

            _toolBarService.AddToolButton("修改项目包", "修改项目包", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);
            // _toolBarService.AddToolButton("退出修改", "退出修改", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手工录入取消, true, false, null);

            return _toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "新建套餐":
                    this.PackageOperation(false);
                    break;
                case "修改套餐":
                    this.PackageOperation(true);
                    break;
                case "修改项目包":
                    this.PackageEdit();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);

        }



        private void PackageEdit()
        {
            if (detailForm == null)
            {
                detailForm = new FS.HISFC.Components.Account.Forms.frmMedicalPackageDetail();
            }

            ItemMedical medial = this.dgbChildPackage.CurrentRow.Tag as ItemMedical;
            if (medial == null)
            {
                MessageBox.Show("请选择需要进行操作的套包！");
                return;
            }

            detailForm.CurrentPackage = medial;
            detailForm.ShowDialog();


        }


        /// <summary>
        /// 套餐操作
        /// </summary>
        /// <returns></returns>
        private void PackageOperation(bool isEdit)
        {

            if (packageForm == null)
            {
                packageForm = new FS.HISFC.Components.Account.Forms.frmMedicalPackage();
            }

            if (isEdit)
            {

                ItemMedical medial = this.dgbChildPackage.CurrentRow.Tag as ItemMedical;
                if (medial == null)
                {
                    MessageBox.Show("请选择需要进行操作的套包！");
                    return;
                }

                packageForm.CurrentPackage = medial;
                packageForm.EditMode = isEdit;
            }
            packageForm.ShowDialog();

            if (packageForm.SaveState)
            {
                InitPackage();

            }


        }


        private void BindEvents()
        {
            this.dgbChildPackage.CurrentCellChanged += new EventHandler(dgbChildPackage_CurrentCellChanged);

            if (detailForm == null)
            {
                detailForm = new FS.HISFC.Components.Account.Forms.frmMedicalPackageDetail();

            }

            detailForm.RefreshPackge += new FS.HISFC.Components.Account.Forms.RefreshPackgeDetailDelegate(detailForm_RefreshPackge);
        }


        private void detailForm_RefreshPackge(ItemMedical package)
        {
            SetChildPackage(package);

        }

        /// <summary>
        /// 项目包选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgbChildPackage_CurrentCellChanged(object sender, EventArgs e)
        {
            this.fpChildPackage_Sheet1.RowCount = 0;

            if (this.dgbChildPackage.CurrentRow == null)
            {
                return;
            }

            ItemMedical medial = this.dgbChildPackage.CurrentRow.Tag as ItemMedical;

            SetInfo(medial);
            this.SetChildPackage(medial);

        }


        private void SetInfo(ItemMedical medical)
        {
            if (medical != null)
            {
                this.lblName.Text = medical.PackageName;
                this.lblTotFee.Text = "￥" + medical.PackageCost.ToString();
                this.lblMemo.Text = medical.Memo;
                this.lblStatus.Text = medical.ValidState == "0" ? "作废" : "启用";
                this.lblCreateTime.Text = medical.CreateEnvironment.OperTime.ToString();
            }

        }



        private void SetChildPackage(ItemMedical package)
        {

            if (package == null)
            {
                return;
            }
            List<ItemMedicalDetail> packagedetailList = accountManager.QueryItemMedicalDetailById(package.PackageId);

            packagedetailList = packagedetailList.OrderBy(a => int.Parse(a.SequenceNo)).ToList();

            this.fpChildPackage_Sheet1.RowCount = 0;
            foreach (ItemMedicalDetail detail in packagedetailList)
            {
                this.fpChildPackage.StopCellEditing();
                this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
                //this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
                int currRow = this.fpChildPackage_Sheet1.RowCount - 1;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.No].Value = detail.SequenceNo;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemCode].Value = detail.ItemCode;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemName].Value = detail.ItemName;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemNum].Value = detail.ItemNum;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemCodeSoon].Value = detail.ItemSubcode;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemNameSoon].Value = detail.ItemSubname;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.Price].Value = detail.UnitPrice;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.CreateCode].Value = detail.CreateEnvironment.ID;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.CreateTime].Value = detail.CreateEnvironment.OperTime;
                this.fpChildPackage_Sheet1.Rows[currRow].Tag = detail;
            }



        }


        private void InitPackage()
        {

            try
            {
                this.dgbChildPackage.Rows.Clear();
                List<ItemMedical> packageList = this.accountManager.QueryAllItemMedical("ALL");

                foreach (ItemMedical itemMedical in packageList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dgbChildPackage);
                    row.Cells[0].ValueType = Type.GetType("System.String");
                    row.Cells[1].ValueType = Type.GetType("System.String");

                    row.Cells[0].Value = itemMedical.PackageId;
                    row.Cells[1].Value = itemMedical.PackageName;

                    row.Tag = itemMedical;

                    if (itemMedical.ValidState == "0")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }

                    this.dgbChildPackage.Rows.Add(row);
                }


            }
            catch { }

        }



        /// <summary>
        /// 列枚举
        /// </summary>
        private enum Columns
        {
            /// <summary>
            /// 序号
            /// </summary>
            No = 0,

            /// <summary>
            /// 编码
            /// </summary>
            ItemCode = 1,

            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 2,

            /// <summary>
            /// 数量
            /// </summary>
            ItemNum = 3,

            /// <summary>
            /// 子编码
            /// </summary>
            ItemCodeSoon = 4,

            /// <summary>
            /// 子名称
            /// </summary>
            ItemNameSoon = 5,

            /// <summary>
            /// 项目金额
            /// </summary>
            Price = 6,

            /// <summary>
            /// 备注
            /// </summary>
            Memo = 7,

            /// <summary>
            /// 创建人
            /// </summary>
            CreateCode = 8,

            /// <summary>
            /// 创建时间
            /// </summary>
            CreateTime = 9

        }
    }
}
