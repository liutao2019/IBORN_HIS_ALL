using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.Account.Forms
{

    public delegate void RefreshPackgeDetailDelegate(ItemMedical Pacakge);
    public partial class frmMedicalPackageDetail : FS.FrameWork.WinForms.Forms.BaseForm
    {



        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 当前套餐
        /// </summary>
        private ItemMedical currentPackage = null;

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        public ItemMedical CurrentPackage
        {
            get { return this.currentPackage; }
            set
            {
                this.currentPackage = value;
            }
        }

        private decimal allMongy = 0.0m;

        List<ItemMedicalDetail> deleteDetails = new List<ItemMedicalDetail>();

        public event RefreshPackgeDetailDelegate RefreshPackge;

        public frmMedicalPackageDetail()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmMedicalPackageDetail_Load);
        }

        private void frmMedicalPackageDetail_Load(object sender, EventArgs e)
        {
            this.InitControls();
            this.SetPackageInfo();
        }



        private void InitControls()
        {
            if (CurrentPackage != null)
            {
                this.lblName.Text = CurrentPackage.PackageName;
                this.lblTotFee.Text = "￥" + CurrentPackage.PackageCost.ToString();
                this.lblMemo.Text = CurrentPackage.Memo;
                this.lblStatus.Text = CurrentPackage.ValidState == "0" ? "作废" : "启用";
                this.lblCreateTime.Text = CurrentPackage.CreateEnvironment.OperTime.ToString();
            }

        }

        private void SetPackageInfo()
        {
            if (this.CurrentPackage == null || string.IsNullOrEmpty(this.currentPackage.PackageId))
            {
                return;
            }

            List<ItemMedicalDetail> packagedetailList = accountManager.QueryItemMedicalDetailById(currentPackage.PackageId);

            packagedetailList = packagedetailList.OrderBy(a => int.Parse(a.SequenceNo)).ToList();

            this.fpChildPackage_Sheet1.RowCount = 0;
            foreach (ItemMedicalDetail detail in packagedetailList)
            {
                this.fpChildPackage.StopCellEditing();
                this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
                int currRow = this.fpChildPackage_Sheet1.RowCount - 1;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.No].Value = currRow + 1;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemCode].Value = detail.ItemCode;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemName].Value = detail.ItemName;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemNum].Value = detail.ItemNum;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemCodeSoon].Value = detail.ItemSubcode;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ItemNameSoon].Value = detail.ItemSubname;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.Price].Value = detail.UnitPrice;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.Memo].Value = detail.Memo;
                this.fpChildPackage_Sheet1.Rows[currRow].Tag = detail;
            }

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
            /// 验证
            /// </summary>
            Err = 8



        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewChildPackage_Click(object sender, EventArgs e)
        {
            this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
            int currRow = this.fpChildPackage_Sheet1.RowCount - 1;
            this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.No].Value = this.fpChildPackage_Sheet1.RowCount;
        }


        private List<ItemMedicalDetail> getDetails()
        {

            List<ItemMedicalDetail> list = new List<ItemMedicalDetail>();
            for (int i = 0; i < this.fpChildPackage_Sheet1.Rows.Count; i++)
            {

                ItemMedicalDetail detail = this.fpChildPackage_Sheet1.Rows[i].Tag as ItemMedicalDetail;
                if (detail == null || string.IsNullOrEmpty(detail.MedicalDetailId))
                {
                    if (this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemCode].Value == null || string.IsNullOrEmpty(this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemCode].Value.ToString()))
                    {
                        continue;
                    }
                    detail = new ItemMedicalDetail();
                    detail.PackageId = this.currentPackage.PackageId;
                    detail.CreateEnvironment.OperTime = DateTime.Now;
                    detail.CreateEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;

                }
                detail.SequenceNo = (i + 1).ToString();
                detail.ItemCode = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemCode].Value.ToString().Trim();
                detail.ItemName = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemName].Value.ToString();
                detail.ItemNum = int.Parse(this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemNum].Value.ToString());
                object itemSubcode = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemCodeSoon].Value;
                detail.ItemSubcode = itemSubcode == null ? "" : itemSubcode.ToString().Trim();
                object ItemSubname = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemNameSoon].Value;
                detail.ItemSubname = ItemSubname == null ? "" : ItemSubname.ToString();
                detail.UnitPrice = decimal.Parse(this.fpChildPackage_Sheet1.Cells[i, (int)Columns.Price].Value.ToString());
                detail.OperEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
                detail.OperEnvironment.OperTime = DateTime.Now;

                allMongy += detail.ItemNum * detail.UnitPrice;
                list.Add(detail);

            }

            return list;

        }

        private bool ValidDetails()
        {
            bool valid = true;
            for (int i = 0; i < this.fpChildPackage_Sheet1.Rows.Count; i++)
            {
                string msg = "";
                object ItemCode = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemCode].Value;
                object ItemName = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemName].Value;
                object ItemNum = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemNum].Value;
                object UnitPrice = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.Price].Value;
                object itemSubcode = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemCodeSoon].Value;
                object ItemSubname = this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ItemNameSoon].Value;


                if (ItemCode == null && ItemName == null && ItemNum == null && UnitPrice == null && itemSubcode == null && ItemSubname == null)
                {
                    this.fpChildPackage_Sheet1.Rows.Remove(i, 1);
                    continue;
                }

                if (ItemCode == null || string.IsNullOrEmpty(ItemCode.ToString()) || (ItemCode.ToString()).Length != 12)
                {
                    msg += "主项目编码不能为空或主项目编码有误！！";
                }
                if (ItemName == null || string.IsNullOrEmpty(ItemName.ToString()))
                {
                    msg += "主项目名称不能为空";
                }

                if (ItemNum == null || string.IsNullOrEmpty(ItemNum.ToString()))
                {
                    msg += "项目次数不能为空";
                }

                if (UnitPrice == null || string.IsNullOrEmpty(UnitPrice.ToString()))
                {
                    msg += "项目价格不能为空";
                }

                if (itemSubcode != null && !string.IsNullOrEmpty(itemSubcode.ToString()))
                {
                    if ((itemSubcode.ToString()).Length != 12)
                    {
                        msg += "子项目编码有误！！";
                    }
                }

                if (valid && !string.IsNullOrEmpty(msg))
                {
                    valid = false;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    this.fpChildPackage_Sheet1.Cells[i, (int)Columns.Err].Value = msg;
                    this.fpChildPackage_Sheet1.Cells[i, (int)Columns.Err].ForeColor = Color.Red;
                }


            }

            return valid;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidDetails())
            {
                return;
            }

            List<ItemMedicalDetail> detailList = this.getDetails();

            if (allMongy != this.currentPackage.PackageCost)
            {
                string msg = "套包明细金额和" + allMongy + "不等于【" + currentPackage.PackageName + "】套包金额(" + currentPackage.PackageCost + ")，确定继续保存操作吗？";

                if (MessageBox.Show(msg, "删除提示", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (ItemMedicalDetail detail in detailList)
            {
                if (string.IsNullOrEmpty(detail.MedicalDetailId))
                {
                    if (accountManager.InsertItemMedicalDetail(detail) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存套包信息失败：" + this.accountManager.Err);
                        return;
                    }
                }
                else
                {
                    if (accountManager.UpdateItemMedicalDetail(detail) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存套包信息失败：" + this.accountManager.Err);
                        return;
                    }
                }

            }


            foreach (ItemMedicalDetail item in deleteDetails)
            {
                if (!string.IsNullOrEmpty(item.MedicalDetailId))
                {
                    if (accountManager.DeleteItemMedicalDetail(item) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存套包信息失败：" + this.accountManager.Err);
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.Hide();
            this.RefreshPackge(this.currentPackage);
            this.Clear();






        }


        private void Clear()
        {
            CurrentPackage = new ItemMedical();
            deleteDetails = new List<ItemMedicalDetail>();
            allMongy = 0.0m;
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {

            ItemMedicalDetail medialdetail = this.fpChildPackage_Sheet1.ActiveRow.Tag as ItemMedicalDetail;
            if (medialdetail != null && !string.IsNullOrEmpty(medialdetail.MedicalDetailId))
            {
                string msg = "确认删除【" + medialdetail.ItemName + "】吗？";

                if (MessageBox.Show(msg, "删除提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(medialdetail.MedicalDetailId))
                    {
                        deleteDetails.Add(medialdetail);
                    }

                    this.fpChildPackage_Sheet1.Rows.Remove(this.fpChildPackage_Sheet1.ActiveRowIndex, 1);
                }
            }

        }


        /// <summary>
        /// 导入明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {

            if (this.fpChildPackage_Sheet1.RowCount != 0)
            {
                MessageBox.Show("只能新增新的套包是才能导入！");
                return;
            }

            System.Windows.Forms.OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "工作表格|*.xlxs;*.xl*;";
            if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fileDlg.FileName;
                DataSet ds = ExcelToDS(path);

                if (ds != null)
                {
                    this.fpChildPackage_Sheet1.RowCount = 0;
                    this.fpChildPackage_Sheet1.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("读取Excel发生错误！");
                }
            }

        }


        private DataSet ExcelToDS(string Path)
        {
            try
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
                strConn = string.Format(strConn, Path);
                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                System.Data.OleDb.OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                strExcel = "select * from [sheet1$]";
                myCommand = new System.Data.OleDb.OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds, "table1");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btnDownTemplate_Click(object sender, EventArgs e)
        {

            string TemplateAddress = "\\temp\\套包明细导入样式.xlsx";

            string fileAddress = System.Windows.Forms.Application.StartupPath + TemplateAddress;
            SaveFileDialog path = new SaveFileDialog();
            path.Filter = "Excel文件|*.xlsx";
            if (path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string pathName = path.FileName;
                try
                {
                    System.IO.File.Copy(fileAddress, pathName, true);
                    MessageBox.Show("下载成功！");
                }
                catch
                {
                }
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {

            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                dlg.FileName = "套包明细";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;

                    this.fpChildPackage.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    MessageBox.Show("导出成功", "温馨提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出数据发生错误>>" + ex.Message);
            }
           

        }



    }
}
