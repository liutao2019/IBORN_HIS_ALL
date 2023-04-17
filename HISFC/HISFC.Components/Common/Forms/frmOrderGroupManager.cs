using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Common.Forms
{
    /// <summary>
    /// ҽ�������޸ģ���ӣ�
    /// ������
    /// </summary>
    public partial class frmOrderGroupManager : Form
    {
        public frmOrderGroupManager()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }


        //public ArrayList alGroupDept = new ArrayList();
        //public ArrayList alGroupPer = new ArrayList();
        public ArrayList alGroupAll = new ArrayList();

        protected ArrayList myalItems;

        /// <summary>
        /// ��ʾ��Ŀ
        /// </summary>
        public ArrayList alItems
        {
            set
            {
                myalItems = value;
                SetGroupDetail(myalItems);
            }
        }

        private FS.HISFC.BizLogic.Manager.Group manager = new FS.HISFC.BizLogic.Manager.Group();


        FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

        /// <summary>
        /// �Ƿ���ʾȫԺ����
        /// </summary>
        public bool IsManager
        {
            set
            {
                //this.rdo3.Visible = value;
            }
        }

        /// <summary>
        /// �������ͣ�����C��סԺI��
        /// </summary>
        protected FS.HISFC.Models.Base.ServiceTypes inpatientType = FS.HISFC.Models.Base.ServiceTypes.I;

        /// <summary>
        /// �������ͣ�����C��סԺI��
        /// </summary>
        [DefaultValue(FS.HISFC.Models.Base.ServiceTypes.I)]
        public FS.HISFC.Models.Base.ServiceTypes InpatientType
        {
            get
            {
                return inpatientType;
            }
            set
            {
                inpatientType = value;
            }
        }

        private int SetGroupDetail(ArrayList alGroupDetail)
        {
            Dictionary<string, string> dicCombNo = new Dictionary<string, string>();

            FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

            foreach (FS.HISFC.Models.Order.Order ord in alGroupDetail)
            {
                if (!string.IsNullOrEmpty(ord.Combo.ID))
                {
                    if (dicCombNo.ContainsKey(ord.Combo.ID))
                    {
                        ord.Combo.ID = dicCombNo[ord.Combo.ID];
                    }
                    else
                    {
                        string strOldCombNo = ord.Combo.ID;
                        ord.Combo.ID = orderMgr.GetNewOrderComboID();

                        dicCombNo.Add(strOldCombNo, ord.Combo.ID);
                    }
                }
                else
                {
                    ord.Combo.ID = orderMgr.GetNewOrderComboID();
                }
            }

            Classes.Function.ShowOrder(this.fpSpread1_Sheet1, alGroupDetail, 1, this.inpatientType);
            FarPoint.Win.Spread.CellType.NumberCellType num = new FarPoint.Win.Spread.CellType.NumberCellType();
            num.MaximumValue = 10;
            num.DecimalPlaces = 0;
            this.fpSpread1_Sheet1.Columns[5].CellType = num;
            this.tvDoctorGroup1.Visible = false;
            this.mnuAll.Checked = false;

            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.tvDoctorGroup1.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvDoctorGroup1_SelectOrder);
            
            this.cmbGroupName.Text = "";

            
            this.Activated+=new EventHandler(frmOrderGroupManager_Activated);
            this.fpSpread1_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpSpread1_Sheet1_CellChanged);
            this.rdo3.CheckedChanged += new EventHandler(radioButton3_CheckedChanged);
            
            if (this.tvDoctorGroup1.Nodes.Count == 0)
            {
                this.tvDoctorGroup1.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                this.tvDoctorGroup1.ShowType = FS.HISFC.Components.Common.Controls.enuGroupShowType.All;
                this.tvDoctorGroup1.InpatientType = this.inpatientType;
                this.tvDoctorGroup1.Init();
                this.alGroupAll = this.tvDoctorGroup1.AllGroup;
            }

            //����Ȩ���ж��ܷ񱣴�Ϊ���ҡ�ȫԺ����
            string sysClass = "groupManager";
            string error = "";

            int ret = docAbility.CheckPopedom(docAbility.Operator.ID, "DEPT", sysClass, false, ref error);

            this.rdo2.Visible = ret > 0 ? true : false;

            ret = docAbility.CheckPopedom(docAbility.Operator.ID, "ALL", sysClass, false, ref error);

            this.rdo3.Visible = ret > 0 ? true : false;

            InitComboBox();
            base.OnLoad(e);
        }
       
        void tvDoctorGroup1_SelectOrder(ArrayList alOrders)
        {
            object o = this.tvDoctorGroup1.SelectedNode.Tag;
            if (o != null)
            {
                if (o.GetType() == typeof(FS.HISFC.Models.Base.Group))
                {
                    FS.HISFC.Models.Base.Group info = o as FS.HISFC.Models.Base.Group;
                    this.label1.Text = info.Name;
                    this.cmbGroupName.Items.Clear();
                    this.cmbGroupName.Items.Add(info);
                    this.cmbGroupName.SelectedIndex = 0;
                    //this.cmbGroupName.Text = info.Name;
                    myalItems = manager.GetAllItem(info);
                    Classes.Function.ShowOrder(this.fpSpread1_Sheet1, myalItems, 1,this.inpatientType);
                    
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            #region �����޸��ж�

            if (this.rdo2.Checked)
            {
                string sysClass = "groupManager";
                string error = "";

                FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

                int ret = docAbility.CheckPopedom(docAbility.Operator.ID, "DEPT", sysClass, false, ref error);

                if (ret <= 0)
                {
                    MessageBox.Show(error);
                    return;
                }
            }

            if (this.rdo3.Checked)
            {
                string sysClass = "groupManager";
                string error = "";

                FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

                int ret = docAbility.CheckPopedom(docAbility.Operator.ID, "ALL", sysClass, false, ref error);

                if (ret <= 0)
                {
                    MessageBox.Show(error);
                    return;
                }
            }

            #endregion

            bool IsAdd = false;
            FS.HISFC.Models.Base.Group CurrentGroup=null;

            #region �ж��Ƿ�Ϊ�޸�ԭ����
            FS.HISFC.Models.Base.Group group;
            FS.HISFC.Models.Base.Employee empl = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
            FS.HISFC.Models.Base.Group groupSelected = this.cmbGroupName.SelectedItem as FS.HISFC.Models.Base.Group;
            if (groupSelected == null || groupSelected.ID == "")
            {
                groupSelected = new FS.HISFC.Models.Base.Group();
            }
            if (this.rdo2.Checked)
            {
                for (int i = 0; i < this.alGroupAll.Count; i++)
                {
                    group = alGroupAll[i] as FS.HISFC.Models.Base.Group;
                    
                    if (groupSelected.ID == group.ID)
                    {
                        CurrentGroup = group;
                        IsAdd = true;
                        break;
                    }
                    if (this.cmbGroupName.Text == group.Name 
                        && group.Dept.ID == empl.Dept.ID 
                        && group.Kind == FS.HISFC.Models.Base.GroupKinds.Dept )
                    {
                        CurrentGroup = group;
                        IsAdd = true;
                        break;
                    }
                }
            }
            else
            {
                if (this.rdo1.Checked)
                {
                    for (int i = 0; i < this.alGroupAll.Count; i++)
                    {
                        group = alGroupAll[i] as FS.HISFC.Models.Base.Group;
                        
                        if (groupSelected.ID == group.ID)
                        {
                            CurrentGroup = group;
                            IsAdd = true;
                            break;
                        }
                        if (this.cmbGroupName.Text == group.Name && group.Doctor.ID == empl.ID && group.Kind == FS.HISFC.Models.Base.GroupKinds.Doctor)
                        {
                            CurrentGroup = group;
                            IsAdd = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < this.alGroupAll.Count; i++)
                    {
                        group = this.alGroupAll[i] as FS.HISFC.Models.Base.Group;
                        
                        if (groupSelected.ID == group.ID)
                        {
                            CurrentGroup = group;
                            IsAdd = true;
                            break;
                        }
                        if (this.cmbGroupName.Text == group.Name 
                            && group.Kind == FS.HISFC.Models.Base.GroupKinds.All)
                        {
                            CurrentGroup = group;
                            IsAdd = true;
                            break;
                        }
                    }
                }
            }
            #endregion

            if (CurrentGroup == null)//�µ�
            {
                CurrentGroup = new FS.HISFC.Models.Base.Group();
                CurrentGroup.ID = manager.GetNewGroupID();
                if (this.cmbGroupName.Text.Trim() == "")
                {
                    MessageBox.Show("��������������!");
                    CurrentGroup = null;
                    return;
                }
                if (FS.FrameWork.Public.String.ValidMaxLengh(this.cmbGroupName.Text.Trim(), 30) == false)
                {
                    MessageBox.Show("�������Ƴ���!", "��ʾ");
                    CurrentGroup = null;
                    return;
                }
                CurrentGroup.Name = this.cmbGroupName.Text;
                //CurrentGroup.UserType = FS.HISFC.Models.Base.ServiceTypes.I;//סԺ
                CurrentGroup.UserType = this.inpatientType;
                FS.HISFC.Models.Base.Employee ee = ((FS.HISFC.Models.Base.Employee)manager.Operator).Clone();
                CurrentGroup.Dept.ID = ee.Dept.ID;
                if (this.rdo2.Checked)			//����
                {
                    CurrentGroup.Kind = FS.HISFC.Models.Base.GroupKinds.Dept;
                    CurrentGroup.Doctor.ID = "";
                }
                else
                {
                    if (this.rdo1.Checked)		//����
                    {
                        CurrentGroup.Kind = FS.HISFC.Models.Base.GroupKinds.Doctor;
                        CurrentGroup.Doctor.ID = manager.Operator.ID;
                    }
                    else								//ȫԺ����
                    {
                        CurrentGroup.Kind = FS.HISFC.Models.Base.GroupKinds.All;
                    }
                }

                //���ױ�ע���������� {2E2FE2A6-3C9C-431e-908F-77B5B941E5F9} houwb
                if (string.IsNullOrEmpty(CurrentGroup.Memo))
                {
                    CurrentGroup.Memo = this.manager.GetMaxGroupSortID();
                }
            }

            if (IsAdd == true && this.chkAdd.Checked == false)
            {
                DialogResult r = MessageBox.Show("�Ƿ�Ҫ���ǵ�ԭ�������ף�\n*��׷�ӻᶪʧ��ԭ�������ݣ�", "������ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.No)
                    return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(manager.Connection);
            //t.BeginTransaction();
            manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (this.chkAdd.Checked == false)
            {
                if (manager.DeleteGroup(CurrentGroup) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��������ʧ��"  + manager.Err);
                    return;
                }
                if (manager.DeleteGroupOrder(CurrentGroup) < 0)//ɾ����ϸ
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��������ʧ��" + manager.Err);
                    return;
                }
            }

            try
            {
                CurrentGroup.ParentID = ((FS.HISFC.Models.Base.Group)groupSelected).ParentID;
            }
            catch
            {
                CurrentGroup.ParentID = "";
            }

            if (manager.UpdateGroup(CurrentGroup) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��������ʧ��" + manager.Err);
                return;
            }
            try
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                {
                    if (this.inpatientType == FS.HISFC.Models.Base.ServiceTypes.I)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = (this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order).Clone();

                        //�ж϶Կ���ʱ����޸��Ƿ���ȷ
                        order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, 14].Text);// {F53BD032-1D92-4447-8E20-6C38033AA607}
                        if (order.BeginTime == DateTime.MinValue)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(order.Item.Name + "ҽ����ʼʱ�����ô��� ����������");
                            return;
                        }
                        if (order.Item.SysClass.ID.ToString() == "UL" && order.Sample.Name != "")
                        {
                            order.CheckPartRecord = order.Sample.Name;
                        }
                        if (manager.UpdateGroupItem(CurrentGroup, order) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(manager.Err);
                            return;
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.Order.OutPatient.Order order = (this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.OutPatient.Order).Clone();
                        //�ж϶Կ���ʱ����޸��Ƿ���ȷ
                        order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, 14].Text);// {F53BD032-1D92-4447-8E20-6C38033AA607}
                        if (order.BeginTime == DateTime.MinValue)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(order.Item.Name + "ҽ����ʼʱ�����ô��� ����������");
                            return;
                        }
                        if (order.Item.SysClass.ID.ToString() == "UL" && order.Sample.Name != "")
                        {
                            order.CheckPartRecord = order.Sample.Name;
                        }
                        if (manager.UpdateGroupItem(CurrentGroup, order) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(manager.Err);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); 
                MessageBox.Show(ex.Message);
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ���");
            this.tvDoctorGroup1.RefrshGroup();
            this.Close();
        }

        private void InitComboBox()
        {
            FS.HISFC.Models.Base.Group group;
            this.cmbGroupName.Items.Clear();
            FS.HISFC.Models.Base.Employee empl = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
            if (this.rdo2.Checked)
            {
                for (int i = 0; i < this.alGroupAll.Count; i++)
                {
                    group = alGroupAll[i] as FS.HISFC.Models.Base.Group;
                    if (group == null) continue;
                    if ( group.Dept.ID == empl.Dept.ID && group.Kind == FS.HISFC.Models.Base.GroupKinds.Dept)
                    {
                        this.cmbGroupName.Items.Add(group);
                    }
                }
            }
            else
            {
                if (this.rdo1.Checked)
                {
                    for (int i = 0; i < this.alGroupAll.Count; i++)
                    {
                        group = alGroupAll[i] as FS.HISFC.Models.Base.Group;
                        if (group == null) continue;
                        if ( group.Doctor.ID == empl.ID && group.Kind == FS.HISFC.Models.Base.GroupKinds.Doctor)
                        {
                            this.cmbGroupName.Items.Add(group);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < this.alGroupAll.Count; i++)
                    {
                        group = this.alGroupAll[i] as FS.HISFC.Models.Base.Group;
                        if (group == null) continue;
                        if (group.Kind == FS.HISFC.Models.Base.GroupKinds.All)
                        {
                            this.cmbGroupName.Items.Add(group);
                        }
                        
                    }
                }
            }   
        }
       

        private void mnuExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

      
        private void frmOrderGroupManager_Activated(object sender, EventArgs e)
        {
            this.cmbGroupName.Focus();
        }

        bool dirty = false;
        private void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            if (e.Column == 5 && dirty == false)
            {
                FS.HISFC.Models.Order.Order order = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Order.Order;
                if (order == null) return;
                order.User03 = this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Value.ToString();
                if (order.Combo.ID != null && order.Combo.ID != "")
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        FS.HISFC.Models.Order.Order obj = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Order;
                        if (obj.Combo.ID == order.Combo.ID)
                        {
                            obj.User03 = order.User03;
                            dirty = true;
                            this.fpSpread1_Sheet1.Cells[i, e.Column].Value = order.User03;
                            dirty = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ѡ���������ͱ仯 ���������б�����Ŀ
        /// </summary>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cmbGroupName.Items.Count > 0)
                this.cmbGroupName.Items.Clear();
            InitComboBox();
        }
        

     
        /// <summary>
        /// ����
        /// </summary>
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        /// <summary>
        /// ȡ��
        /// </summary>
        private void button2_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAll_Click(object sender, EventArgs e)
        {
            this.tvDoctorGroup1.Visible = !this.mnuAll.Checked;
            this.mnuAll.Checked = !this.mnuAll.Checked;
            
        }

        private void rdo1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cmbGroupName.Items.Count > 0)
                this.cmbGroupName.Items.Clear();
            InitComboBox();

        }

        private void rdo2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cmbGroupName.Items.Count > 0)
                this.cmbGroupName.Items.Clear();
            InitComboBox();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}