using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HISFC.Components.Package.Controls;

namespace HISFC.Components.Package.Fee.Forms
{
    public partial class frmChoosePackageItem : Form
    {
        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();


        /// <summary>
        /// 当前选中套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentPackage = null;

        /// <summary>
        /// 当前选中子套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentChildPackage = null;

        /// <summary>
        /// 当前明细
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.PackageDetail currentDetail;

        /// <summary>
        /// 套餐总金额
        /// </summary>
        private decimal totFee = 0;

        public decimal TotFee
        {
            get { return this.totFee; }
            set
            {
                totFee = value;
            }
        }

        /// <summary>
        /// 当前的明细
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.PackageDetail CurrentDetail
        {
            get { return this.currentDetail; }
            set
            {
                this.currentDetail = (value == null) ? null : value.Clone(); ;
                this.ucPackageItemSelect1.CurrentDetail = value;
            }
        }

        private System.Collections.ArrayList currentDetailList = new System.Collections.ArrayList();

        /// <summary>
        /// 当前明细列表
        /// </summary>
        public System.Collections.ArrayList CurrentDetailList
        {
            get { return currentDetailList; }
            set { currentDetailList = value; }
        }

        public frmChoosePackageItem()
        {
            InitializeComponent();
            this.InitControls();
            this.BindEvents();
        }

        private void InitControls()
        {
            //初始化项目选择控件
            this.ucPackageItemSelect1.Init();
            this.ucPackageItemSelect1.SetNewDetail = setNewDetail;
            this.ucPackageItemSelect1.ModifyDetail = modifyDetail;
        }

        private void BindEvents()
        {
            this.dgbChildPackageDetail.CurrentCellChanged += new EventHandler(dgbChildPackageDetail_CurrentCellChanged);
            this.btnDeleteDetail.Click += new EventHandler(btnDeleteDetail_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.GetValues();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.MedicalPackage.PackageDetail tmp = this.dgbChildPackageDetail.CurrentRow.Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
            this.CurrentDetail = tmp.Clone();
            string msg = "确认删除【" + tmp.Item.Name + "】吗？";

            if (MessageBox.Show(msg, "删除提示", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.dgbChildPackageDetail.Rows.Remove(this.dgbChildPackageDetail.CurrentRow);
            }
        }


        private void setNewDetail(FS.HISFC.Models.Base.Item item)
        {
            this.CurrentDetail = this.ucPackageItemSelect1.CurrentDetail;
            try
            {
                this.CurrentDetail.ExecDept = new FS.FrameWork.Models.NeuObject();
                this.CurrentDetail.CreateOper = FS.FrameWork.Management.Connection.Operator.ID;
                this.CurrentDetail.ModifyOper = FS.FrameWork.Management.Connection.Operator.ID;
                //{A87AAC77-567E-4c94-8BCA-1BD9D1EF74E7}  添加套餐费用类别
                CurrentDetail.Item.MinFee.ID = item.MinFee.ID;
                this.addDataGridRow(CurrentDetail);
                this.dgbChildPackageDetail.CurrentCellChanged -= new EventHandler(dgbChildPackageDetail_CurrentCellChanged);
                this.dgbChildPackageDetail.CurrentCell = this.dgbChildPackageDetail.Rows[this.dgbChildPackageDetail.Rows.Count - 1].Cells[0];
                this.dgbChildPackageDetail.CurrentCellChanged += new EventHandler(dgbChildPackageDetail_CurrentCellChanged);

            }
            catch (Exception ex)
            {
                MessageBox.Show("转换出错!\r\n" + ex.Message);
            }
        }

        private void modifyDetail(FS.HISFC.Models.MedicalPackage.PackageDetail detail, EnumDetailFieldList field)
        {
            if (this.dgbChildPackageDetail.SelectedRows.Count < 1)
            {
                return;
            }

            FS.HISFC.Models.MedicalPackage.PackageDetail tmp = this.dgbChildPackageDetail.SelectedRows[0].Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;

            if (field == EnumDetailFieldList.Qty)
            {
                tmp.Item.Qty = detail.Item.Qty;
                this.dgbChildPackageDetail.SelectedRows[0].Cells[5].Value = detail.Item.Qty;
            }

            if (field == EnumDetailFieldList.Unit)
            {
                tmp.Unit = detail.Unit;
                tmp.UnitFlag = detail.UnitFlag;
                this.dgbChildPackageDetail.SelectedRows[0].Cells[6].Value = detail.Unit;
            }

            if (field == EnumDetailFieldList.Memo)
            {
                tmp.Memo = detail.Memo;
                this.dgbChildPackageDetail.SelectedRows[0].Cells[11].Value = detail.Memo;
            }

            this.CurrentDetail = tmp.Clone();

            this.dgbChildPackageDetail.SelectedRows[0].Tag = tmp;
        }

        private int deleteDetail()
        {
            if (this.dgbChildPackageDetail.CurrentRow == null)
            {
                MessageBox.Show("请选择要删除的明细项！");
                return -1;
            }

            FS.HISFC.Models.MedicalPackage.PackageDetail tmp = this.dgbChildPackageDetail.CurrentRow.Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
            this.CurrentDetail = tmp.Clone();
            string msg = "确认删除【" + tmp.Item.Name + "】吗？";

            if (MessageBox.Show(msg, "删除提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.dgbChildPackageDetail.Rows.Remove(this.dgbChildPackageDetail.CurrentRow);
                return 1;
            }
            return 1;
        }

        private void dgbChildPackageDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dgbChildPackageDetail.CurrentRow == null)
            {
                this.CurrentDetail = null;
                this.ucPackageItemSelect1.PresentDetail();
                return;
            }

            if (this.dgbChildPackageDetail.CurrentRow.Tag is FS.HISFC.Models.MedicalPackage.PackageDetail)
            {
                FS.HISFC.Models.MedicalPackage.PackageDetail cur = this.dgbChildPackageDetail.CurrentRow.Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
                this.CurrentDetail = cur.Clone();
                this.ucPackageItemSelect1.PresentDetail();
            }
        }

        /// <summary>
        /// 加载项目包明细
        /// </summary>
        public void SetPackageDetails(System.Collections.ArrayList detailList)
        {

            this.TotFee = 0;
            this.dgbChildPackageDetail.RowCount = 0;
            foreach (FS.HISFC.Models.MedicalPackage.PackageDetail detail in detailList)
            {
                decimal tmpQTY = detail.Item.Qty;

                if (detail.Item.SysClass.ID.ToString().Equals("P") ||
                   detail.Item.SysClass.ID.ToString().Equals("PCC") ||
                   detail.Item.SysClass.ID.ToString().Equals("PCZ"))
                {
                    detail.Item = itemIntegrate.GetItem(detail.Item.ID);
                }
                else
                {
                    detail.Item = itemMgr.GetUndrugByCode(detail.Item.ID);
                }

                detail.Item.Qty = tmpQTY;
                detail.Item.Price = Decimal.Parse(detail.Item.Price.ToString("F2"));

                this.addDataGridRow(detail);
            }
        }

        private void addDataGridRow(FS.HISFC.Models.MedicalPackage.PackageDetail detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(this.dgbChildPackageDetail);
            row.Cells[0].ValueType = Type.GetType("System.Int32");
            row.Cells[1].ValueType = Type.GetType("System.String");
            row.Cells[2].ValueType = Type.GetType("System.String");
            row.Cells[3].ValueType = Type.GetType("System.String");
            row.Cells[4].ValueType = Type.GetType("System.String");
            row.Cells[5].ValueType = Type.GetType("System.Int32");
            row.Cells[6].ValueType = Type.GetType("System.String");
            row.Cells[7].ValueType = Type.GetType("System.String");
            row.Cells[8].ValueType = Type.GetType("System.DateTime");
            row.Cells[9].ValueType = Type.GetType("System.String");
            row.Cells[10].ValueType = Type.GetType("System.DateTime");
            row.Cells[11].ValueType = Type.GetType("System.String");
            row.Cells[12].ValueType = Type.GetType("System.String");
            row.Cells[0].Value = this.dgbChildPackageDetail.Rows.Count + 1;
            row.Cells[1].Value = detail.Item.ID;
            row.Cells[2].Value = detail.Item.Name + "[" + detail.Item.Specs + "]";
            row.Cells[3].Value = detail.ExecDept.ID;
            if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                row.Cells[4].Value = detail.Item.Price.ToString() + "/" + (detail.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit;
                if (detail.UnitFlag.Equals("0"))
                {
                    this.TotFee += Math.Round((detail.Item.Price / detail.Item.PackQty) * detail.Item.Qty, 2);
                }
                else
                {
                    this.TotFee += detail.Item.Price * detail.Item.Qty;
                }
            }
            else
            {
                row.Cells[4].Value = detail.Item.Price.ToString() + "/" + detail.Unit;
                this.TotFee += detail.Item.Price * detail.Item.Qty;
            }
            row.Cells[5].Value = detail.Item.Qty;
            row.Cells[6].Value = detail.Unit;
            row.Cells[7].Value = detail.ModifyOper;
            row.Cells[8].Value = detail.ModifyTime;
            row.Cells[9].Value = detail.CreateOper;
            row.Cells[10].Value = detail.CreateTime;
            row.Cells[11].Value = detail.Memo;
            //{A87AAC77-567E-4c94-8BCA-1BD9D1EF74E7}  添加套餐费用类别
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject obj1 = constantMgr.GetConstant("MINFEE", detail.Item.MinFee.ID);
            row.Cells[12].Value = obj1.Name;
            row.Tag = detail;
            this.dgbChildPackageDetail.CurrentCellChanged -= new EventHandler(dgbChildPackageDetail_CurrentCellChanged);
            this.dgbChildPackageDetail.Rows.Add(row);
            this.dgbChildPackageDetail.CurrentCellChanged += new EventHandler(dgbChildPackageDetail_CurrentCellChanged);
        }

        public void GetValues()
        {
            this.TotFee = 0;
            this.currentDetailList.Clear();
            foreach (DataGridViewRow row in this.dgbChildPackageDetail.Rows)
            {
                FS.HISFC.Models.MedicalPackage.PackageDetail tmp = row.Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
                this.currentDetailList.Add(tmp);
                if (tmp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (tmp.UnitFlag.Equals("0"))
                    {
                        this.TotFee += Math.Round((tmp.Item.Price / tmp.Item.PackQty) * tmp.Item.Qty, 2);
                    }
                    else
                    {
                        this.TotFee += tmp.Item.Price * tmp.Item.Qty;
                    }
                }
                else
                {
                    this.TotFee += tmp.Item.Price * tmp.Item.Qty;
                }
            }
        }
    }
}
