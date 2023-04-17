using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.SpecType
{
    public partial class frmOrgType : Form
    {
        //组织类型管理对象
        private OrgTypeManage orgTypeManage;       
        private OrgType orgType;
        private Dictionary<int, string> orgTypeDic;
        private string title = "标本组织类型设置";
        public frmOrgType()
        {
            orgTypeManage = new OrgTypeManage();
            orgType = new OrgType();
            InitializeComponent();
        }

        /// <summary>
        /// 从页面读取组织类型信息
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        private string SetOrgType(string oper)
        {
            string orgTypeName = txtName.Text.TrimEnd().TrimStart();// cmbOrgType.SelectedText;             
            if (orgTypeName == "")
            {
                MessageBox.Show("名称不能为空！", title);
                return "";
            }
            if (orgTypeName != "")
            {
                switch (oper)
                {
                    case("Insert"):
                        if (orgTypeDic.ContainsValue(orgTypeName))
                        {
                            MessageBox.Show("名称重复，请重新设置！", title);
                            return "";
                        }
                        orgType.OrgName = orgTypeName;
                        orgType.IsShowOnApp = Convert.ToInt16(1);
                        return "Insert";
                    
                    case("Update"):
                        if (orgTypeDic.ContainsValue(orgTypeName) && cmbOrgType.Text != orgTypeName)
                        {
                            MessageBox.Show("名称重复，请重新设置！", title);
                            return "";
                        }
                        orgType.OrgTypeID = Convert.ToInt32(cmbOrgType.SelectedValue);
                        orgType.OrgName = orgTypeName;                        
                        return "Update";                     
                    default:
                        return "";                            
                }
            }
            return "";

        }
        /// <summary>
        /// 组织类型绑定
        /// </summary>
        private void OrgTypeBinding()
        {            
            orgTypeDic = orgTypeManage.GetAllOrgType();
            if (orgTypeDic.Count > 0)
            {
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = orgTypeDic;              
                cmbOrgType.ValueMember = "Key";
                cmbOrgType.DisplayMember = "Value";
                cmbOrgType.DataSource = bsTmp;                
                cmbOrgType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 保存标本类型或标本组织类型
        /// </summary>
        private int SaveType(string oper)
        {
            string orgOper = SetOrgType(oper);
            switch (orgOper)
            {
                case (""):
                    return 0;
                case ("Update"):
                    return orgTypeManage.UpdateOrgType(orgType.OrgName, orgType.OrgTypeID);
                    
                case ("Insert"):
                   return orgTypeManage.InsertOrgType(orgType);
                default:                    
                    return 0;
            }            

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string btnText = btnSave.Text;          
            if (btnText == "保存")
            {
                int save = SaveType("Insert");
                if (save > 0)
                {                  
                    MessageBox.Show("保存成功！", title);
                    OrgTypeBinding();
                }
                if (save == -1)
                {
                    MessageBox.Show("保存失败！", title);
                }
                btnSave.Text = "添加";
            }
            else
            {
                this.txtName.Text = "";
                this.btnSave.Text = "保存";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int save = SaveType("Update");
            if (save >= 1)
            {
                MessageBox.Show("更新成功！", title);
                OrgTypeBinding();
            }
            if (save == -1)
            {
                MessageBox.Show("更新失败！", title);
            }
        }

        private void cmbOrgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtName.Text = cmbOrgType.Text ;
        }

        private void frmOrgType_Load(object sender, EventArgs e)
        {
            OrgTypeBinding();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //Id = 3 不能删除（硬性规定）
            if (cmbOrgType.SelectedValue == null || cmbOrgType.Text.Trim() == "" || cmbOrgType.SelectedValue.ToString() == "3")
            {
                return; 
            }
            string sql = @"select t.ORGTYPEID
            from SPEC_SUBSPEC ss join SPEC_TYPE t on ss.SPECTYPEID = t.SPECIMENTTYPEID 
            where t.ORGTYPEID = {0}";

            sql = string.Format(sql, cmbOrgType.SelectedValue);
            string result = orgTypeManage.ExecSqlReturnOne(sql);

            if (result != "-1")
            {
                MessageBox.Show("正在使用，不能删除");
                return;
            }

            DialogResult dr = MessageBox.Show("是否删除: " + cmbOrgType.Text, "删除", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            string delSql = "delete from SPEC_ORGTYPE o where o.ORGTYPEID = {0}";
            string delTypeSql = "delete from SPEC_TYPE t where t.ORGTYPEID = {0}";

            delSql = string.Format(delSql, cmbOrgType.SelectedValue);
            delTypeSql = string.Format(delTypeSql, cmbOrgType.SelectedValue);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            orgTypeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (orgTypeManage.ExecNoQuery(delSql) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除失败," + orgTypeManage.Err);
                    return;
                }
                if (orgTypeManage.ExecNoQuery(delTypeSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除失败," + orgTypeManage.Err);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("删除成功!");
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除失败," + orgTypeManage.Err);
            }
            OrgTypeBinding();
        }
    }
}