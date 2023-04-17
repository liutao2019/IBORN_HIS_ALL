using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.HISFC.BizLogic.Privilege.Service;
using FS.HISFC.BizLogic.Privilege;
using FS.HISFC.BizLogic.Privilege;
//using FS.WinForms.Forms;



namespace FS.HISFC.Components.Privilege
{
    public partial class AddAuthorizeForm : FS.HISFC.Components.Privilege.InputBaseForm
    {
        private IList<FS.HISFC.BizLogic.Privilege.Model.Resource> currentResourceList = null;
        private Role parentRole = new Role();
        private Role currentRole = new Role();
        RoleResourceMapping controlRoleResource = null;
        private string pageUpdate = string.Empty;
        private string pageJudge = String.Empty;
        private string MenuPageJudge = "MenuRes";
        private string WebPageJudge = "WebRes";
        private string UserPageJudge = "UserRes";
        private string DictionaryPageJudge = "DictionaryRes";
        private string ReportPageJudge = "ReportRes";
        private string JudgeOperate = "UpdateRes";

        public AddAuthorizeForm(Role crole, Role prole, RoleResourceMapping roleResource, String pageType)
        {

            InitializeComponent();
            parentRole = prole;
            currentRole = crole;
            controlRoleResource = roleResource;
            pageJudge = pageType;
            Init();
        }

        public AddAuthorizeForm(Role crole, Role prole, RoleResourceMapping roleResource, String pageType, string update)
        {
            InitializeComponent();
            pageUpdate = update;
            parentRole = prole;
            currentRole = crole;
            controlRoleResource = roleResource;
            pageJudge = pageType;
            Init();
        }

        /// <summary>
        /// ��ʼ����Դ��Ȩ������
        /// </summary>
        private void Init()
        {
            if (pageJudge == MenuPageJudge || pageJudge == WebPageJudge)
            {
                typename.Text = FS.FrameWork.Management.Language.Msg( "�˵�����" );
            }
            if (pageJudge == DictionaryPageJudge)
            {
                typename.Text = FS.FrameWork.Management.Language.Msg( "��������" );
            }
            if (pageJudge == ReportPageJudge)
            {
                typename.Text = FS.FrameWork.Management.Language.Msg( "��������" );
            }

            if (pageJudge == WebPageJudge)
            {
                nLabel4.Text = "    URl:";
                tbControlDll.Visible = false;
                tbparameter.Visible = false;
                nLabel5.Visible = false;
                nLabel6.Visible = false;
            }

            if (this.pageJudge != this.ReportPageJudge)
            {
                this.tbReportItem.Visible = false;
                this.txtDeptSql.Visible = false;
                this.txtEmpSql.Visible = false;
                this.neuLabel1.Visible = false;
                this.neuLabel2.Visible = false;

                this.txtName2.Visible = false;
                this.txtName1.Visible = false;
                this.neuLabel3.Visible = false;
                this.neuLabel4.Visible = false;
            }
            else
            {
                this.tbReportItem.Visible = true;
                this.txtDeptSql.Visible = true;
                this.txtEmpSql.Visible = true;
                this.neuLabel1.Visible = true;
                this.neuLabel2.Visible = true;

                this.txtName2.Visible = true;
                this.txtName1.Visible = true;
                this.neuLabel3.Visible = true;
                this.neuLabel4.Visible = true;
            }

        }

        private void AddAuthorizeForm_Load(object sender, EventArgs e)
        {
            this.cmbType.SelectedValueChanged -= new System.EventHandler(this.cmbType_SelectedValueChanged);
            //this.cmbImage.SelectedIndexChanged -= new System.EventHandler(this.cmbImage_SelectedIndexChanged);
           // this.cbResource.SelectedIndexChanged -= new System.EventHandler(this.cbResource_SelectedIndexChanged);
            InitComboBox();

            if (pageUpdate == JudgeOperate)
            {
                tbTypeName.Text = controlRoleResource.Name;
                tbparameter.Text = controlRoleResource.Parameter;
                cmbImage.SelectedIndex = FrameWork.Function.NConvert.ToInt32(controlRoleResource.Icon);
                if (controlRoleResource.ValidState == "1")
                {
                    nrbRight.Checked = true;
                }
                else
                {
                    nrbWrong.Checked = true;
                }

                if (controlRoleResource.EmplSql != null)
                {
                    string[] sql1 = controlRoleResource.EmplSql.Split('|');

                    if (sql1.Length == 2)
                    {
                        this.txtName1.Text = sql1[0];
                        this.txtEmpSql.Text = sql1[1];
                    }

                }

                if (controlRoleResource.DeptSql != null)
                {
                    string[] sql2 = controlRoleResource.DeptSql.Split('|');

                    if (sql2.Length == 2)
                    {
                        this.txtName2.Text = sql2[0];
                        this.txtDeptSql.Text = sql2[1];
                    }
                }
            }

            this.cmbType.SelectedValueChanged += new System.EventHandler(this.cmbType_SelectedValueChanged);
           // this.cmbImage.SelectedIndexChanged += new System.EventHandler(this.cmbImage_SelectedIndexChanged);
           // this.cbResource.SelectedIndexChanged += new System.EventHandler(this.cbResource_SelectedIndexChanged);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbTypeName.Text.Trim()))
            {
                MessageBox.Show("��Դ���Ʋ���Ϊ�գ���");
                return;
            }

            PrivilegeService _proxy = Common.Util.CreateProxy();
            try
            {
                FrameWork.Management.PublicTrans.BeginTransaction();
                using (_proxy as IDisposable)
                {
                    if (pageUpdate == JudgeOperate)
                    {
                        _proxy.UpdateRoleResource(SetRoleResource());
                    }
                    else
                    {
                        _proxy.InsertRoleResource(SetRoleResource());
                    }
                }
                FrameWork.Management.PublicTrans.Commit();
                this.DialogResult = DialogResult.OK;
                base.Close();

            }
            catch (Exception ex)
            {
                FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show(ex.ToString());
                //new SystemErrorForm(ex).ShowDialog();
            }

        }

        private RoleResourceMapping SetRoleResource()
        {
            RoleResourceMapping currentRoleResource = new RoleResourceMapping();
            if (cbResource.SelectedValue != null)
            {
                currentRoleResource.Resource.Id = cbResource.SelectedValue.ToString();
            }
            else
            {
                currentRoleResource.Resource.Id = null;
            }

            if (nrbRight.Checked == true)
            {
                currentRoleResource.ValidState = "1";
            }
            else
            {
                currentRoleResource.ValidState = "0";
            }

            if (controlRoleResource == null)
            {
                currentRoleResource.ParentId = "root";

                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    currentRoleResource.OrderNumber = _proxy.QueryRoleResource(pageJudge, "root").Count+1;
                }

            }
            else
            {
                if (pageUpdate == JudgeOperate)
                {
                    currentRoleResource.ParentId = controlRoleResource.ParentId;

                    decimal sortNum = 0;
                    if (decimal.TryParse(this.tbparameter.Text, out sortNum))
                    {
                        currentRoleResource.OrderNumber = sortNum;
                    }

                }
                else
                {
                    currentRoleResource.ParentId = controlRoleResource.Id;

                    PrivilegeService _proxy = Common.Util.CreateProxy();
                    using (_proxy as IDisposable)
                    {
                        //Ϊ���ڵ�Ĭ�ϵ�ǰ�Ľ�ɫ
                        if (controlRoleResource.Id == "root")
                        {
                            controlRoleResource.Role = currentRole;
                            controlRoleResource.Type = pageJudge;
                        }
                        currentRoleResource.OrderNumber = _proxy.QueryByTypeParentRole(controlRoleResource).Count + 1;
                    }

                }

            }

            if (pageUpdate == JudgeOperate)
            {
                currentRoleResource.Role.ID = controlRoleResource.Role.ID;
                currentRoleResource.Id = controlRoleResource.Id;

            }
            else
            {
                currentRoleResource.Role.ID = currentRole.ID;
                currentRoleResource.Id = new FrameWork.Management.DataBaseManger().GetSequence("PRIV.SEQ_ROLE_RESOURCE");
            }
            currentRoleResource.EmplSql = this.txtName1.Text.Trim()+"|"+this.txtEmpSql.Text;
            currentRoleResource.DeptSql = this.txtName2.Text.Trim()+"|"+this.txtDeptSql.Text;
            currentRoleResource.Parameter = tbparameter.Text.Trim();
            currentRoleResource.Type = pageJudge;
            currentRoleResource.Name = tbTypeName.Text.Trim();
            currentRoleResource.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
            currentRoleResource.OperDate = FrameWork.Function.NConvert.ToDateTime(new FrameWork.Management.DataBaseManger().GetSysDateTime());

            if (cmbImage.SelectedIndex != -1)
            {
                currentRoleResource.Icon = cmbImage.SelectedIndex.ToString();
            }
            //if (cmbImage.Text == string.Empty)
            //{
            //    currentRoleResource.Icon = null;
            //}
            return currentRoleResource;
        }


        private void InitComboBox()
        {

            InitCmbType();
            InitCmbImage();

            if (currentRole.ParentId == "roleadmin" || currentRole.ID == "roleadmin")
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    currentResourceList = _proxy.QueryResourcesByType(pageJudge);
                }

            }
            else
            {
                currentResourceList = new List<FS.HISFC.BizLogic.Privilege.Model.Resource>();
                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    IList<RoleResourceMapping> newRoleResourcelist = _proxy.QueryByTypeRoleId(pageJudge, currentRole.ParentId);
                    foreach (RoleResourceMapping newRoleResource in newRoleResourcelist)
                    {
                        if (newRoleResource.Resource != null && newRoleResource.Resource.Id != null)
                        {
                            bool Judge = true;
                            if (currentResourceList.Count == 0)
                            {
                                currentResourceList.Add(newRoleResource.Resource);
                            }
                            else
                            {
                                foreach (FS.HISFC.BizLogic.Privilege.Model.Resource newRes in currentResourceList)
                                {
                                    if (newRoleResource.Resource.Id == newRes.Id)
                                    {
                                        Judge = false;
                                        break;
                                    }
                                }

                                if (Judge)
                                {
                                    currentResourceList.Add(newRoleResource.Resource);
                                }
                            }
                        }
                    }
                }

            }
            if (currentResourceList.Count != 0)
            {
                FiltrateList();
            }
            cbResource.DataSource = currentResourceList;
            cbResource.ValueMember = "Id";
            cbResource.SelectedIndex = -1;

            if (pageUpdate == JudgeOperate)
            {
                if (controlRoleResource.Resource.Id != null)
                {
                    cbResource.SelectedValue = controlRoleResource.Resource.Id;
                }
            }

        }
       

        private void FiltrateList()
        {
            for (int i = 0; i < currentResourceList.Count; i++)
            {
                if (currentResourceList[i].ParentId == "ROOT")
                {
                    currentResourceList.RemoveAt(i);
                    FiltrateList();
                }
            }
        }

        /// <summary>
        /// ��ʼ��ͼ��������
        /// </summary>
        private void InitCmbImage()
        {
            string[] icons = null;
            int max = Enum.GetValues(typeof(FS.FrameWork.WinForms.Classes.EnumImageList)).Length;
            icons = new string[max];

            int index = 0;
            for (index = 0; index < icons.Length; index++)
            {
                icons[index] = Enum.GetValues(typeof(FS.FrameWork.WinForms.Classes.EnumImageList)).GetValue(index).ToString();

                icons[index] = FS.FrameWork.Management.Language.Msg( icons[index] );
            }

            cmbImage.Items.AddRange(icons);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitCmbType()
        {
            List<Resource> resourceTypeList = new RoleResourceProcess().QueryByParentId("ROOT");
            if (resourceTypeList == null)
            {
                return;
            }

            cmbType.DataSource = resourceTypeList;
            cmbType.ValueMember = "ID";
            cmbType.SelectedIndex = -1;
        }

        private void cbResource_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbResource.Text == string.Empty)
            {
                tbControl.Text = string.Empty;
                tbControlDll.Text = string.Empty;
                return;
            }

            foreach (FS.HISFC.BizLogic.Privilege.Model.Resource resource in currentResourceList)
            {
                if (cbResource.SelectedValue != null)
                {
                    if (cbResource.SelectedValue.ToString() == resource.Id)
                    {
                        tbControl.Text = resource.WinName;
                        tbControlDll.Text = resource.DllName;
                        tbTypeName.Text = resource.Name;
                    }
                }
            }

        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbImage.SelectedIndex != -1)
            {
                pbImage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage((FS.FrameWork.WinForms.Classes.EnumImageList)(FS.FrameWork.Function.NConvert.ToInt32(cmbImage.SelectedIndex.ToString())));
            }
        }

        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            currentResourceList.Clear();
            this.cbResource.SelectedIndexChanged -= new System.EventHandler(this.cbResource_SelectedIndexChanged);
            if (cmbType.SelectedValue == null) return;
            if (currentRole.ParentId == "roleadmin" || currentRole.ID == "roleadmin")
            {

                currentResourceList = new ResourceProcess().QueryByTypeParentId(pageJudge,cmbType.SelectedValue.ToString());
            }
            else
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    IList<RoleResourceMapping> newRoleResourcelist = _proxy.QueryByTypeRoleId(pageJudge, currentRole.ParentId);
                    foreach (RoleResourceMapping newRoleResource in newRoleResourcelist)
                    {
                        if (newRoleResource.Resource != null && newRoleResource.Resource.Id != null)
                        {
                            bool Judge = true;
                            if (currentResourceList.Count == 0)
                            {
                                currentResourceList.Add(newRoleResource.Resource);
                            }
                            else
                            {
                                foreach (FS.HISFC.BizLogic.Privilege.Model.Resource newRes in currentResourceList)
                                {
                                    if (newRoleResource.Resource.Id == newRes.Id || newRes.ParentId != cmbType.SelectedValue.ToString())
                                    {
                                        Judge = false;
                                        break;
                                    }
                                }

                                if (Judge)
                                {
                                    currentResourceList.Add(newRoleResource.Resource);
                                }
                            }
                        }
                    }
                }

            }

            cbResource.DataSource = currentResourceList;
            cbResource.ValueMember = "Id";
            cbResource.SelectedIndex = -1;
            this.cbResource.SelectedIndexChanged += new System.EventHandler(this.cbResource_SelectedIndexChanged);
            tbTypeName.Text = string.Empty;
            tbControl.Text = string.Empty;
            tbControlDll.Text = string.Empty;
        }

        private void tbReportItem_Click(object sender, EventArgs e)
        {
            if (this.pageJudge != this.ReportPageJudge)
                return;

            if (this.controlRoleResource.Id.Length <= 0)
            {
                MessageBox.Show("��ǰ��Դ��δ���棡");
                return;
            }

            ucItemQueryManager ucMain = new ucItemQueryManager();

            ucMain.QueryType.ID = this.controlRoleResource.Id;
            ucMain.QueryType.Name = this.controlRoleResource.Name;

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucMain);
        }

        

    }
}