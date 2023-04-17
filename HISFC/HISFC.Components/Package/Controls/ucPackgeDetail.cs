using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISFC.Components.Package.Controls
{
    /// <summary>
    /// 获取明细的委托函数
    /// </summary>
    /// <param name="PacakgeID"></param>
    public delegate ArrayList GetPackgeDetailDelegate(string PacakgeID); 

    public partial class ucPackgeDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        #region 业务层

        /// <summary>
        /// 数据库管理类
        /// </summary>
        FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// 套餐维护业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();

        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        #endregion

        #region 属性

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
        /// 被修改过的明细
        /// </summary>
        private Hashtable modifyDetails = new Hashtable();

        /// <summary>
        /// 被删除的明细
        /// </summary>
        public ArrayList deleteDetails = new ArrayList();


        /// <summary>
        /// 是否在编辑模式
        /// </summary>
        private bool editMode = false;

        /// <summary>
        /// 当前的明细
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.PackageDetail CurrentDetail
        {
            get { return this.currentDetail; }
            set
            {
                this.currentDetail = (value == null) ? null : value.Clone(); ;
                this.ucPackageInfoEdit1.CurrentDetail = value;
            }
        }

        #endregion

        #region 委托

        /// <summary>
        /// 删除明细
        /// </summary>
        public DeleteDelegate DeleteDetail;

        /// <summary>
        /// 获取明细数据的委托函数
        /// </summary>
        public GetPackgeDetailDelegate GetPackgeDetail;

        #endregion 

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
                this.SetTotFee();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public ucPackgeDetail()
        {
            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            SetControlsVisible();
            BindEvents();
        }

        private void BindEvents()
        {
            this.dgbPackageDetail.CurrentCellChanged += new EventHandler(dgbPackageDetail_CurrentCellChanged);
            this.dgbChildPackage.CurrentCellChanged += new EventHandler(dgbChildPackage_CurrentCellChanged);
            this.btnNewChildPackage.Click += new EventHandler(btnNewChildPackage_Click);
            this.btnDeleteDetail.Click += new EventHandler(btnDeleteDetail_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.ucPackageInfoEdit1.ModifyDetail += modifyDetail;
            this.ucPackageInfoEdit1.SetNewDetail += setNewDetail;
        }

        private void UnBindEvents()
        {
            this.ucPackageInfoEdit1.SetNewDetail -= setNewDetail;
            this.ucPackageInfoEdit1.ModifyDetail -= modifyDetail;
            this.btnSave.Click -= new EventHandler(btnSave_Click);
            this.btnDeleteDetail.Click -= new EventHandler(btnDeleteDetail_Click);
            this.btnNewChildPackage.Click -= new EventHandler(btnNewChildPackage_Click);
            this.dgbChildPackage.CurrentCellChanged -= new EventHandler(dgbChildPackage_CurrentCellChanged);
            this.dgbPackageDetail.CurrentCellChanged -= new EventHandler(dgbPackageDetail_CurrentCellChanged);
        }

        /// <summary>
        /// 设置编辑状态
        /// </summary>
        /// <param name="isEdit">是否编辑状态</param>
        public void SetEditMode(bool isEdit)
        {
            this.editMode = isEdit;
            this.SetControlsVisible();
        }

        /// <summary>
        /// 设置控件可见模式
        /// </summary>
        private void SetControlsVisible()
        {
            this.plTop.Visible = !editMode;
            this.plBottom.Visible = editMode;
            this.ucPackageInfoEdit1.Visible = editMode;
        }

        public void Clear()
        {
            modifyDetails.Clear();
            deleteDetails.Clear();
            this.dgbChildPackage.Rows.Clear();
            this.dgbPackageDetail.Rows.Clear();
        }

        public void ClearDetail()
        {
            modifyDetails.Clear();
            deleteDetails.Clear();
            this.dgbPackageDetail.Rows.Clear();
        }

        public void SetPackageInfo(FS.HISFC.Models.MedicalPackage.Package package)
        {
            this.currentPackage = package;
            this.ucPackageInfoEdit1.CurrentPackage = package;
            this.ucPackageInfoEdit1.CurrentChildPackage = null;
            this.ucPackageInfo1.CurrentPackage = package;

            if (this.currentPackage == null || string.IsNullOrEmpty(this.currentPackage.Name))
            {
                this.lblTitle.Text = "套餐详情";
            }
            else
            {
                this.lblTitle.Text = "套餐详情-" + this.currentPackage.Name;
            }

            this.SetChildPackageList();
        }

        #region 列表选择时间

        /// <summary>
        /// 项目包选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgbChildPackage_CurrentCellChanged(object sender, EventArgs e)
        {
            this.dgbPackageDetail.Rows.Clear();

            if (this.dgbChildPackage.CurrentRow == null)
            {
                this.currentChildPackage = null;
                return;
            }

            this.currentChildPackage = this.dgbChildPackage.CurrentRow.Tag as FS.HISFC.Models.MedicalPackage.Package;
            this.ucPackageInfoEdit1.CurrentChildPackage = this.currentChildPackage;

            this.SetPackageDetails();
        }

        /// <summary>
        /// 项目明细选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgbPackageDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dgbPackageDetail.CurrentRow == null)
            {
                this.CurrentDetail = null;
                this.ucPackageInfoEdit1.PresentDetail();
                return;
            }

            if (this.dgbPackageDetail.CurrentRow.Tag is FS.HISFC.Models.MedicalPackage.PackageDetail)
            {
                FS.HISFC.Models.MedicalPackage.PackageDetail cur = this.dgbPackageDetail.CurrentRow.Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
                this.CurrentDetail = cur.Clone();
                this.ucPackageInfoEdit1.PresentDetail();
            }
        }

        #endregion

        #region 加载数据

        /// <summary>
        /// 加载子套餐列表
        /// </summary>
        private void SetChildPackageList()
        {
            int lastIndex = -1;
            string lastChildPackageID = string.Empty;
            int count = 0;

            if (this.currentChildPackage != null)
            {
                lastChildPackageID = this.currentChildPackage.ID;
            }

            this.Clear();

            if (this.currentPackage == null || string.IsNullOrEmpty(this.currentPackage.ID))
            {
                return;
            }

            this.UnBindEvents();

            try
            {
                ArrayList childPackageList = this.packageProcess.QueryPackageByParentCode(this.currentPackage.ID);
                
                foreach (FS.HISFC.Models.MedicalPackage.Package childPackage in childPackageList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dgbChildPackage);
                    row.Cells[0].ValueType = Type.GetType("System.Int32");
                    row.Cells[1].ValueType = Type.GetType("System.Boolean");
                    row.Cells[2].ValueType = Type.GetType("System.String");

                    row.Cells[0].Value = this.dgbChildPackage.Rows.Count + 1;
                    row.Cells[1].Value = childPackage.MainFlag == "1";
                    row.Cells[2].Value = childPackage.Name;
                    row.Tag = childPackage;

                    if (!childPackage.IsValid)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }

                    this.dgbChildPackage.Rows.Add(row);


                    if (!string.IsNullOrEmpty(lastChildPackageID) && lastChildPackageID == childPackage.ID)
                    {
                        lastIndex = count;
                    }

                    count++;
                }

                if (lastIndex >= 0)
                {
                    this.dgbChildPackage.CurrentCell = this.dgbChildPackage.Rows[lastIndex].Cells[0];
                }

                this.dgbChildPackage_CurrentCellChanged(null, new EventArgs());
            }
            catch { }

            this.BindEvents();

        }

        /// <summary>
        /// 加载项目包明细
        /// </summary>
        private void SetPackageDetails()
        {
            if (this.currentChildPackage == null || string.IsNullOrEmpty(this.currentChildPackage.ID))
            {
                return;
            }

            ArrayList detailList = this.packageProcess.GetPackageItemByPackageID(this.currentChildPackage.ID);

            this.TotFee = 0;

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

        /// <summary>
        /// 添加一行项目明细数据
        /// </summary>
        /// <param name="detail"></param>
        private void addDataGridRow(FS.HISFC.Models.MedicalPackage.PackageDetail detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(this.dgbPackageDetail);
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
            row.Cells[0].Value = this.dgbPackageDetail.Rows.Count+1;
            row.Cells[1].Value = detail.Item.ID;
            row.Cells[2].Value = detail.Item.Name + "[" + detail.Item.Specs + "]";
            row.Cells[3].Value = detail.ExecDept.ID;
            if(detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
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

            //{9111494E-B467-4c51-B83A-93E07886A41E}停用的项目，标记为红色
            if (detail.Item.ValidState != "1")
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
            }

            this.dgbPackageDetail.CurrentCellChanged -= new EventHandler(dgbPackageDetail_CurrentCellChanged);
            this.dgbPackageDetail.Rows.Add(row);
            this.dgbPackageDetail.CurrentCellChanged += new EventHandler(dgbPackageDetail_CurrentCellChanged);
        }

        #endregion

        /// <summary>
        /// 获取新增或修改的套餐信息
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public bool GetChildPackageInfo(ref FS.HISFC.Models.MedicalPackage.Package package, ref string ErrMsg)
        {
            package = ucPackageInfoEdit1.GetChildPackageInfo(ref ErrMsg);

            if (package == null)
            {
                return false;
            }

            return true;
        }

        private void setNewDetail(FS.HISFC.Models.Base.Item item)
        {
            this.CurrentDetail = this.ucPackageInfoEdit1.CurrentDetail;
            try
            {
                this.CurrentDetail.ExecDept = new FS.FrameWork.Models.NeuObject();
                this.CurrentDetail.CreateOper = FS.FrameWork.Management.Connection.Operator.ID;
                this.CurrentDetail.CreateTime = dbManager.GetDateTimeFromSysDateTime();
                this.CurrentDetail.ModifyOper = FS.FrameWork.Management.Connection.Operator.ID;
                this.CurrentDetail.ModifyTime = dbManager.GetDateTimeFromSysDateTime();
                //{A87AAC77-567E-4c94-8BCA-1BD9D1EF74E7}  添加套餐费用类别
                CurrentDetail.Item.MinFee.ID = item.MinFee.ID;
                this.addDataGridRow(CurrentDetail);
                this.dgbPackageDetail.CurrentCellChanged -= new EventHandler(dgbPackageDetail_CurrentCellChanged);
                this.dgbPackageDetail.CurrentCell = this.dgbPackageDetail.Rows[this.dgbPackageDetail.Rows.Count - 1].Cells[0];
                this.dgbPackageDetail.CurrentCellChanged += new EventHandler(dgbPackageDetail_CurrentCellChanged);

            }
            catch (Exception ex)
            {
                MessageBox.Show("转换出错!\r\n" + ex.Message);
            }
        }

        private void modifyDetail(FS.HISFC.Models.MedicalPackage.PackageDetail detail, EnumDetailFieldList field)
        {
            if (this.dgbPackageDetail.SelectedRows.Count < 1)
            {
                return;
            }

            FS.HISFC.Models.MedicalPackage.PackageDetail tmp = this.dgbPackageDetail.SelectedRows[0].Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;

            if (field == EnumDetailFieldList.Qty)
            {
                tmp.Item.Qty = detail.Item.Qty;
                this.dgbPackageDetail.SelectedRows[0].Cells[5].Value = detail.Item.Qty;
                getTotFee();
            }

            if (field == EnumDetailFieldList.Unit)
            {
                tmp.Unit = detail.Unit;
                tmp.UnitFlag = detail.UnitFlag;
                this.dgbPackageDetail.SelectedRows[0].Cells[6].Value = detail.Unit;
                getTotFee();
            }

            if (field == EnumDetailFieldList.Memo)
            {
                tmp.Memo = detail.Memo;
                this.dgbPackageDetail.SelectedRows[0].Cells[11].Value = detail.Memo;
            }

            this.CurrentDetail = tmp.Clone();

            this.dgbPackageDetail.SelectedRows[0].Tag = tmp;

            ///如果是老记录，则需要更新
            if (!string.IsNullOrEmpty(this.CurrentDetail.SequenceNO))
            {
                if (!this.modifyDetails.ContainsKey(tmp.SequenceNO))
                {
                    this.modifyDetails.Add(tmp.SequenceNO, "");
                }
            }
        }

        public ArrayList GetModifyDetails()
        {
            ArrayList modifyDetailList = new ArrayList();
            for (int i = 0; i < this.dgbPackageDetail.Rows.Count; i++)
            {
                FS.HISFC.Models.MedicalPackage.PackageDetail detail = this.dgbPackageDetail.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
                if (detail != null)
                {
                    if (string.IsNullOrEmpty(detail.SequenceNO))
                    {
                        modifyDetailList.Add(detail);
                    }
                    else
                    {
                        if (this.modifyDetails.ContainsKey(detail.SequenceNO))
                        {
                            modifyDetailList.Add(detail);
                        }
                    }
                }
            }
            return modifyDetailList;
        }

        private int deleteDetail()
        {
            if(this.dgbPackageDetail.CurrentRow == null)
            {
                MessageBox.Show("请选择要删除的明细项！");
                return -1;
            }

            FS.HISFC.Models.MedicalPackage.PackageDetail tmp = this.dgbPackageDetail.CurrentRow.Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
            this.CurrentDetail = tmp.Clone();
            string msg = "确认删除【" + tmp.Item.Name + "】吗？";

            if (MessageBox.Show(msg, "删除提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(tmp.SequenceNO))
                {
                    deleteDetails.Add(tmp);
                }

                this.dgbPackageDetail.Rows.Remove(this.dgbPackageDetail.CurrentRow);
                getTotFee();
                return 1;
            }

            return 1;
        }

        private void getTotFee()
        {
            this.TotFee = 0;

            foreach (DataGridViewRow row in this.dgbPackageDetail.Rows)
            {
                FS.HISFC.Models.MedicalPackage.PackageDetail tmp = row.Tag as FS.HISFC.Models.MedicalPackage.PackageDetail;
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

        private void SetTotFee()
        {
            this.ucPackageInfoEdit1.SetTotFee(this.TotFee);
        }

        private int Save()
        {
            ArrayList detailList = this.GetModifyDetails();
            FS.HISFC.Models.MedicalPackage.Package childpackage = new FS.HISFC.Models.MedicalPackage.Package();
            string ErrMsg = string.Empty;
            if (!this.GetChildPackageInfo(ref childpackage, ref ErrMsg))
            {
                MessageBox.Show("获取项目包信息失败：" + ErrMsg);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (string.IsNullOrEmpty(childpackage.ID))
            {
                if (this.packageProcess.AddPackage(childpackage) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存项目包信息失败：" + this.packageProcess.Err);
                    return -1;
                }
            }
            else
            {
                if (this.packageProcess.UpdatePackage(childpackage) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新项目包信息失败：" + this.packageProcess.Err);
                    return -1;
                }
            }


            foreach (FS.HISFC.Models.MedicalPackage.PackageDetail detail in detailList)
            {
                if (string.IsNullOrEmpty(detail.SequenceNO))
                {
                    if (this.packageProcess.AddPackageDetail(childpackage, detail) < 0)
                    {
                        detail.SequenceNO = string.Empty;
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存项目包明细信息失败：" + this.packageProcess.Err);
                        return -1;
                    }
                }
                else
                {
                    if (this.packageProcess.UpdatePackageDetail(detail) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新项目包明细信息失败：" + this.packageProcess.Err);
                        return -1;
                    }
                }
            }

            foreach (FS.HISFC.Models.MedicalPackage.PackageDetail detail in this.deleteDetails)
            {
                if (this.packageProcess.DeletePackageDetail(this.currentChildPackage, detail) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除项目包明细信息失败：" + this.packageProcess.Err);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 新建项目包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewChildPackage_Click(object sender, EventArgs e)
        {
            this.ClearDetail();
            this.currentChildPackage = null;
            this.ucPackageInfoEdit1.CurrentChildPackage = null;
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            this.deleteDetail();
        }

        /// <summary>
        /// 保存项目包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Save() > 0)
            {
                MessageBox.Show("保存成功！");
                this.SetChildPackageList();
            }
        }
    }
}
