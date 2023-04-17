using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse
{
    public partial class ucDept : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucDept()
        {
            InitializeComponent();
        }

        private void ucDept_Load(object sender, EventArgs e)
        {
            this.InitTree();

            //�õ��Һſ���
            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            al = deptMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);

            if (al == null) al = new ArrayList();
            this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 0, al);

            foreach (FS.HISFC.Models.Base.Department dept in al)
            {
                htDept.Add(dept.ID, dept.Name);
            }
            this.fpEnter1.KeyEnter += new FS.HISFC.Components.Nurse.Base.FpEnter.keyDown(fpEnter1_KeyEnter);
            this.fpEnter1.SetItem += new FS.HISFC.Components.Nurse.Base.FpEnter.setItem(fpSpread1_SetItem);
        }

        #region ������

        private FS.HISFC.BizProcess.Integrate.Manager cDept = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Nurse.Dept nurseDept = new FS.HISFC.BizLogic.Nurse.Dept();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup current = null;

        private Hashtable hashFlag = new Hashtable();
        private Hashtable htDept = new Hashtable();
        private ArrayList alNurse = null;
        private ArrayList al = new ArrayList();

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("���", "", 0, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "", 0, true, false, null);
            return this.toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "���":
                    this.AddRow();
                    break;
                case "ɾ��":
                    this.DelRow();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ݿ��Ҵ����������
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetDeptNameByID(string ID)
        {
            IDictionaryEnumerator dict = this.htDept.GetEnumerator();
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == ID)
                    return dict.Value.ToString();
            }
            return ID;
        }

        private void InitTree()
        {
            this.tvPatientList1.Nodes.Clear();

            TreeNode root = new TreeNode("��ʿվ");
            this.tvPatientList1.Nodes.Add(root);

            //��Ļ�ʿվ�б�
            this.alNurse = cDept.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            if (alNurse != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in alNurse)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    node.Tag = obj;
                    root.Nodes.Add(node);
                }
            }
            root.Expand();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void AddRow()
        {
            try
            {
                if (this.fpEnter1_Sheet1.Tag == null)
                {
                    MessageBox.Show("����ѡ����!", "��ʾ");
                    return;
                }
                this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.RowCount, 1);
                int row = this.fpEnter1_Sheet1.RowCount - 1;
                //�Һſ���
                this.fpEnter1_Sheet1.SetValue(row, 0, "", false);
                //��ʾ˳��
                this.fpEnter1_Sheet1.SetValue(row, 1, "1", false);
                //����Ա
                this.fpEnter1_Sheet1.SetValue(row, 2, FS.FrameWork.Management.Connection.Operator.Name/*var.User.Name*/, false);
                this.fpEnter1_Sheet1.Cells[row, 2].Value = FS.FrameWork.Management.Connection.Operator.ID;/* var.User.ID;*/
                //����ʱ��
                this.fpEnter1_Sheet1.SetValue(row, 3, this.nurseDept.GetDateTimeFromSysDateTime().ToString(), false);
                this.fpEnter1.Focus();
                this.fpEnter1_Sheet1.SetActiveCell(row, 0, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }


        private void DelRow()
        {
            try
            {
                int row = this.fpEnter1_Sheet1.ActiveRowIndex;
                if (row < 0 || this.fpEnter1_Sheet1.RowCount == 0) return;

                if (MessageBox.Show("�Ƿ�Ҫɾ���ü�¼?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No) return;

                //�Ѿ��������Ŀ,�����ݿ���ɾ��
                if (this.fpEnter1_Sheet1.Rows[row].Tag != null)
                {

                }
                this.fpEnter1_Sheet1.Rows.Remove(row, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + this.nurseDept.Err);
            }
        }


        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <param name="person"></param>
        private void RefreshList(FS.FrameWork.Models.NeuObject nurse)
        {
            if (this.fpEnter1_Sheet1.RowCount > 0)
                this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.RowCount);

            try
            {
                //����������ά����Ϣ
                this.alNurse = this.nurseDept.GetDeptInfoByNurseNo(nurse.ID);

                this.neuTabControl1.TabPages[0].Text = nurse.Name;

                this.fpEnter1_Sheet1.Tag = nurse;

                if (alNurse != null)
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in alNurse)
                    {
                        this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.RowCount, 1);
                        int row = this.fpEnter1_Sheet1.RowCount - 1;
                        this.fpEnter1_Sheet1.Rows[row].Tag = obj;
                        //�Һſ��Ҵ���
                        this.fpEnter1_Sheet1.SetText(row, 0, this.GetDeptNameByID(obj.Name));

                        this.fpEnter1_Sheet1.Cells[row, 0].Tag = obj.Name;
                        //��ʾ˳��
                        this.fpEnter1_Sheet1.SetValue(row, 1, obj.User01, false);

                        //����Ա
                        this.fpEnter1_Sheet1.SetValue(row, 2, obj.User02, false);
                        //����ʱ��
                        this.fpEnter1_Sheet1.SetValue(row, 3, this.nurseDept.GetSysDateTime(), false);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + this.nurseDept.Err);
            }
        }

        private bool ValidData()
        {
            Hashtable hash = new Hashtable();

            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1.StopCellEditing();

                for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                {
                    //���Ҵ���
                    if (this.fpEnter1_Sheet1.GetTag(i, 0) == null)
                    {
                        MessageBox.Show("�Һſ��Ҵ���δѡ��!");
                        return false;
                    }
                    string DeptID = this.fpEnter1_Sheet1.GetTag(i, 0).ToString();

                    if (DeptID == "")
                    {
                        MessageBox.Show("�Һſ��Ҳ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else if (!FS.FrameWork.Public.String.ValidMaxLengh(DeptID, 4))
                    {
                        MessageBox.Show("�Һſ��Ҵ������");
                        return false;
                    }
                    if (hash.Contains(DeptID))
                    {
                        MessageBox.Show("�Һſ��Ҳ����ظ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else
                    {
                        hash.Add(DeptID, 0);
                    }

                    //��ʾ˳��
                    string SortId = this.fpEnter1_Sheet1.GetText(i, 1).ToString();
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(SortId, 4))
                    {
                        MessageBox.Show("˳��Ź���");
                        return false;
                    }
                    else if (SortId == "")
                    {
                        MessageBox.Show("˳��Ų���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                }

                return true;

            }
            else
            {
                return true;
            }
        }



        private void SaveData()
        {
            this.fpEnter1.StopCellEditing();

            if (this.fpEnter1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ����!", "��ʾ");
                return;
            }
            //��֤����
            if (!this.ValidData())
            {
                this.fpEnter1.Focus();
                return;
            }
            //FS.FrameWork.Management.Transaction tran = new FS.FrameWork.Management.Transaction(this.nurseDept.Connection);
            //��ʼ����
            try
            {
                //tran.BeginTransaction();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.nurseDept.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.nurseDept.DelDeptInfo((this.fpEnter1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.nurseDept.Err, "��ʾ");
                    return;
                }
                //ѭ������
                for (int i = 0; i < this.fpEnter1_Sheet1.RowCount; i++)
                {
                    FS.FrameWork.Models.NeuObject objDept = new FS.FrameWork.Models.NeuObject();
                    //��ʿվ
                    objDept.ID = (this.fpEnter1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID;
                    //�Һſ���
                    objDept.Name = this.fpEnter1_Sheet1.Cells[i, 0].Tag.ToString();
                    //��ʾ˳��
                    objDept.User01 = this.fpEnter1_Sheet1.Cells[i, 1].Text.ToString();
                    //����Ա					
                    objDept.User02 = this.nurseDept.Operator.ID;
                    //����ʱ��
                    objDept.User03 = this.nurseDept.GetSysDateTime();

                    if (this.nurseDept.InsertDeptInfo(objDept) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.nurseDept.Err, "��ʾ");
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
            MessageBox.Show("����ɹ�!", "��ʾ");
        }

        #endregion

        #region �¼�

        //private void fpSpread1_Enter(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F11)
        //    {
        //        this.AddRow();
        //    }
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        //�Һſ���
        //        if (this.fpEnter1_Sheet1.ActiveColumnIndex == 0)
        //        {
        //            current = this.fpEnter1.GetCurrentList(this.fpEnter1_Sheet1, 0);
        //            if (current == null) return ;

        //            FS.FrameWork.Models.NeuObject obj = current.GetSelectedItem();

        //            if (obj == null) return ;
        //            this.fpEnter1_Sheet1.SetText(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex,
        //                obj.Name);
        //            this.fpEnter1_Sheet1.SetTag(this.fpEnter1_Sheet1.ActiveRowIndex,
        //                this.fpEnter1_Sheet1.ActiveColumnIndex, obj.ID);

        //            this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 1, false);
        //        }
        //        //��ʾ˳��
        //        else if (this.fpEnter1_Sheet1.ActiveColumnIndex == 1)
        //        {
        //            if (this.fpEnter1_Sheet1.ActiveRowIndex == this.fpEnter1_Sheet1.RowCount - 1)
        //            {
        //                this.AddRow();
        //                this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 0, false);
        //            }
        //            else
        //            {
        //                this.fpEnter1_Sheet1.ActiveRowIndex++;
        //                this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 0, false);
        //            }
        //        }
        //    }
        //}

        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.F11)
            {
                this.AddRow();
            }
            if (key == Keys.Enter)
            {
                //�Һſ���
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == 0)
                {
                    current = this.fpEnter1.GetCurrentList(this.fpEnter1_Sheet1, 0);
                    if (current == null) return -1;

                    FS.FrameWork.Models.NeuObject obj = current.GetSelectedItem();

                    if (obj == null) return -1;
                    this.fpEnter1_Sheet1.SetText(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex,
                        obj.Name);
                    this.fpEnter1_Sheet1.SetTag(this.fpEnter1_Sheet1.ActiveRowIndex,
                        this.fpEnter1_Sheet1.ActiveColumnIndex, obj.ID);

                    this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 1, false);
                }
                //��ʾ˳��
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == 1)
                {
                    if (this.fpEnter1_Sheet1.ActiveRowIndex == this.fpEnter1_Sheet1.RowCount - 1)
                    {
                        this.AddRow();
                        this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 0, false);
                    }
                    else
                    {
                        this.fpEnter1_Sheet1.ActiveRowIndex++;
                        this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 0, false);
                    }
                }
            }
            return 0;
        }

        private int fpSpread1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            if (obj == null) return -1;

            this.fpEnter1_Sheet1.SetText(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex,
                obj.Name);
            this.fpEnter1_Sheet1.SetTag(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex,
                obj.ID);

            this.fpEnter1.Focus();
            this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex);
            return 0;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            int altKey = Keys.Alt.GetHashCode();

            if (keyData == Keys.Add || keyData == Keys.Oemplus)
            {
                this.AddRow();
                return true;
            }
            else if (keyData == Keys.Subtract || keyData == Keys.OemMinus)
            {
                this.DelRow();
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.S.GetHashCode())
            {
                this.SaveData();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void tvPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currentNode = this.tvPatientList1.SelectedNode;

            if (currentNode == null || currentNode.Parent == null)
            {
                if (this.fpEnter1_Sheet1.RowCount > 0)
                    this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.RowCount);

                this.fpEnter1_Sheet1.Tag = null;
            }
            else
            {
                FS.FrameWork.Models.NeuObject nurse = (FS.FrameWork.Models.NeuObject)currentNode.Tag;
                this.RefreshList(nurse);
            }
        }

        #endregion

    }
}
