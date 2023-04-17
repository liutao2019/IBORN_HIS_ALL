using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using System.Collections;
using FS.HISFC.BizLogic.Privilege.Model;

namespace FS.HISFC.Components.Privilege
{
    public partial class DividePrivForm : FS.HISFC.Components.Privilege.PermissionBaseForm
    {
        public DividePrivForm()
        {
            InitializeComponent();
            FS.FrameWork.WinForms.Classes.Function.SetFarPointStyle(fpSpread1_Sheet1);
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
            this.fpSpread1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1_Sheet1.Columns[1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1_Sheet1.Columns[2].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1_Sheet1.DataAutoCellTypes = false;
            this.MainToolStrip.ItemClicked += new ToolStripItemClickedEventHandler(MainToolStrip_ItemClicked);
            Init();
        }
        protected DataSet ds = new DataSet();
        protected DataView dv = null;

        protected void Init()
        {
            InitToolBar();
            InitDataSet();
            RefreshUserList();
        }

        protected virtual void RefreshUserList()
        {
            FS.HISFC.Models.Base.Employee person = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            List<User> userList = new FS.HISFC.BizLogic.Privilege.UserLogic().Query();

            if (userList == null)
            {
                MessageBox.Show("读取语句出错。");
                return;
            }
            AddDataIntoTable(ds.Tables[0], userList); //填充数据
            dv = new DataView(ds.Tables[0]);   //初始化 DataView 
            this.fpSpread1_Sheet1.DataSource = dv;          // 绑定数据源
        }

        private bool AddDataIntoTable(System.Data.DataTable table, List<User> userList)
        {
            if (table == null)
            {
                return false;
            }
            try
            {
                table.Clear();

                foreach (User obj in userList)
                {
                    table.Rows.Add(new object[] { obj.Id, obj.Name, obj.Account });
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
            return true;

        }

        private void InitDataSet()
        {
            DataTable table = new DataTable("人员");

            DataColumn dataColumn1 = new DataColumn("编码");
            dataColumn1.DataType = typeof(System.String);
            table.Columns.Add(dataColumn1);

            DataColumn dataColumn2 = new DataColumn("人员名称");
            dataColumn2.DataType = typeof(System.String);
            table.Columns.Add(dataColumn2);

            DataColumn dataColumn3 = new DataColumn("登录名");
            dataColumn3.DataType = typeof(System.String);
            table.Columns.Add(dataColumn3);

            ds.Tables.Add(table);
        }

        private void InitToolBar()
        {
            MainToolStrip.Items.Clear();
            ToolBarService _toolBarService = new ToolBarService();
            _toolBarService.Clear();
            _toolBarService.AddToolButton("添加", "", FS.FrameWork.WinForms.Classes.EnumImageList.R人员, true, false, null);
            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("退出", "", FS.FrameWork.WinForms.Classes.EnumImageList.T退出, true, false, null);

            ArrayList toolButtons = _toolBarService.GetToolButtons();
            for (int i = 0; i < toolButtons.Count; i++)
            {
                this.MainToolStrip.Items.Add(toolButtons[i] as ToolStripItem);
            }
            for (int i = 0; i < MainToolStrip.Items.Count; i++)
            {
                this.MainToolStrip.Items[i].TextImageRelation = TextImageRelation.ImageAboveText;
            }
        }

        private void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "添加":
                   Add();
                    break;
                case "退出":
                    this.Close();
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string temp = " like  '" + this.textBox1.Text + "%' ";
                dv.RowFilter = "编码" + temp + " or " + "人员名称" + temp + " or " + "登录名" + temp;
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
                textBox1.Text = "";
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (e.Column != -1 && e.Row != -1)
            {
                User user = new FS.HISFC.BizLogic.Privilege.UserLogic().Get(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim());
                if (user == null)
                {
                    return;
                }

                AddUserForm frmUser = new AddUserForm(user, GetRole());
                frmUser.ShowDialog();
                RefreshUserList();
            }
        }

        private void Add()
        {
            AddUserForm frmUser = new AddUserForm(GetRole());
            frmUser.ShowDialog();
            RefreshUserList();
        }

        private Role GetRole()
        {
            Role role = new Role();
            role.ID = "roleadmin";
            role.Name = "系统管理";
            role.AppId = "NEU";
            return role;
        }

    }
}
