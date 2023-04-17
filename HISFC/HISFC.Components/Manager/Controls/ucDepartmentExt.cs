using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager.Controls
{
    public partial class ucDepartmentExt : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDepartmentExt()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.Manager.DepartmentStatManager myDeptManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
        private FS.FrameWork.Management.ExtendParam myDeptExt = new FS.FrameWork.Management.ExtendParam();
        FS.HISFC.Models.Base.ExtendInfo myExtendInfo = null;
        private DataTable dtDepartmentExt = new DataTable("����ά��");
        private Hashtable htDepartment = new Hashtable();
        ArrayList al = new ArrayList();
        ArrayList alDepartExt = new ArrayList();
        

        private void ucDepartmentExt_Load(object sender, EventArgs e)
        {
            this.InitDateTable();
            this.InitTreeView();
            this.InitFp();
            this.GetDepartmentExt();
        }

        private void InitDateTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                this.dtDepartmentExt.Columns.AddRange(new DataColumn[]{
														   new DataColumn("���ұ���", strType),	//0
														   new DataColumn("��������", strType),	 //1
														   new DataColumn("���Ա���", strType),//2
														   new DataColumn("��������", strType),//3
														   new DataColumn("�ַ�����", strType),//4
														   new DataColumn("��λ��", intType),//5
														   new DataColumn("��������", dtType),//6
														   new DataColumn("��ע",     strType), //7
														   new DataColumn("������",   strType),//8
														   new DataColumn("����ʱ��", dtType),//9
														   new DataColumn("��������", strType)});//10
                //������Դ
                this.fpSpread1.DataSource = this.dtDepartmentExt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitFp()
        {
            this.fpSpread1.Sheets[0].Columns[0].Visible = false;
            this.fpSpread1.Sheets[0].Columns[2].Visible = false;
            this.fpSpread1_Sheet1.Columns[4].Visible = false;
            this.fpSpread1_Sheet1.Columns[6].Visible = false;
            this.fpSpread1_Sheet1.Columns[10].Visible = false;
            this.fpSpread1_Sheet1.Columns[1].Locked = true;
            this.fpSpread1_Sheet1.Columns[3].Locked = true;
            this.fpSpread1_Sheet1.Columns[8].Locked = true;
        }

        private void InitTreeView()
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Tag = "ALL";
            treeNode.Text = "סԺ����";
            this.tvdept.Nodes.Add(treeNode);
            this.al = this.myDeptManager.LoadDepartmentStatAndByNodeKind("72", "1");
            for (int i = 0; i < al.Count; i++)
            {
                TreeNode onenode = new TreeNode();
                onenode.Tag = ((FS.HISFC.Models.Base.DepartmentStat)al[i]).DeptCode;
                onenode.Text = ((FS.HISFC.Models.Base.DepartmentStat)al[i]).DeptName;
                this.tvdept.Nodes[0].Nodes.Add(onenode);
            }
            this.tvdept.ExpandAll();
        }

        private void GetDepartmentExt()
        {
            this.alDepartExt = this.myDeptExt.GetComExtInfoList(FS.HISFC.Models.Base.EnumExtendClass.DEPT,"CASE_BED_STAND");
            FS.HISFC.Models.Base.ExtendInfo departmentExt = new FS.HISFC.Models.Base.ExtendInfo();
            for (int i = 0; i < this.alDepartExt.Count; i++)
            {
                departmentExt = this.alDepartExt[i] as FS.HISFC.Models.Base.ExtendInfo;

                this.htDepartment.Add(departmentExt.Item.ID, departmentExt.Item.ID);
                DataRow row = this.dtDepartmentExt.NewRow();
                row["���ұ���"] = departmentExt.Item.ID;
                row["��������"] = departmentExt.PropertyName;
                row["���Ա���"] = "CASE_BED_STAND";
                row["��������"] = "��λ��";
                row["��λ��"] = departmentExt.NumberProperty;
                row["������"] = departmentExt.OperEnvironment.ID;
                row["����ʱ��"] = FS.FrameWork.Function.NConvert.ToDateTime( departmentExt.OperEnvironment.Memo);
                this.dtDepartmentExt.Rows.Add(row);
            }
        }

        private void tvdept_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((TreeView)sender).SelectedNode.Level == 1)
            {
                if (htDepartment.ContainsKey(this.tvdept.SelectedNode.Tag))
                {
                }
                else
                {
                    this.htDepartment.Add(this.tvdept.SelectedNode.Tag, this.tvdept.SelectedNode.Tag);
                    DataRow row = this.dtDepartmentExt.NewRow();
                    row["���ұ���"] = this.tvdept.SelectedNode.Tag;
                    row["��������"] = this.tvdept.SelectedNode.Text;
                    row["���Ա���"] = "CASE_BED_STAND";
                    row["��������"] = "��λ��";
                    row["��λ��"] = 0;
                    row["������"] = this.myDeptManager.Operator.ID;
                    row["����ʱ��"] = this.myDeptManager.GetDateTimeFromSysDateTime();
                    this.dtDepartmentExt.Rows.Add(row);
                }
            }
            else
            {
                for (int i = 0; i < al.Count; i++)
                {
                    if (!htDepartment.ContainsKey(((FS.HISFC.Models.Base.DepartmentStat)al[i]).DeptCode))
                    {
                        this.htDepartment.Add(((FS.HISFC.Models.Base.DepartmentStat)al[i]).DeptCode, ((FS.HISFC.Models.Base.DepartmentStat)al[i]).DeptCode);
                        DataRow row = this.dtDepartmentExt.NewRow();
                        row["���ұ���"] = ((FS.HISFC.Models.Base.DepartmentStat)al[i]).DeptCode;
                        row["��������"] = ((FS.HISFC.Models.Base.DepartmentStat)al[i]).DeptName;
                        row["���Ա���"] = "CASE_BED_STAND";
                        row["��������"] = "��λ��";
                        row["��λ��"] = 0;
                        row["������"] = this.myDeptManager.Operator.ID;
                        row["����ʱ��"] = this.myDeptManager.GetDateTimeFromSysDateTime();
                        this.dtDepartmentExt.Rows.Add(row);
                    }
                }
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.myDeptExt.Connection);
            //trans.BeginTransaction();
            this.myDeptExt.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.SetDepartmentExt(i);
                if (this.myDeptExt.SetComExtInfo(this.myExtendInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show("����ʧ��!");
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�!");
            return base.OnSave(sender, neuObject);
        }
        private void SetDepartmentExt(int a)
        {
            this.myExtendInfo = new FS.HISFC.Models.Base.ExtendInfo();
            this.myExtendInfo.ExtendClass = FS.HISFC.Models.Base.EnumExtendClass.DEPT;
            this.myExtendInfo.Item.ID = this.fpSpread1_Sheet1.Cells[a, 0].Text;
            this.myExtendInfo.PropertyCode = "CASE_BED_STAND";
            this.myExtendInfo.PropertyName = this.fpSpread1_Sheet1.Cells[a, 1].Text;
            this.myExtendInfo.NumberProperty = FS.FrameWork.Function.NConvert.ToInt32( this.fpSpread1_Sheet1.Cells[a, 5].Value);
            this.myExtendInfo.Memo = this.fpSpread1_Sheet1.Cells[a, 7].Text;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPreview(0, 0, this.neuPanel1);
            return base.OnPrint(sender, neuObject);
        }
    }
}
