using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucQueryBySpecSource : UserControl
    {
        private DisTypeManage disTypeManage;
        //组织类型管理对象
        private OrgTypeManage orgTypeManage;
        //标本类型管理对象
        private SpecTypeManage specTypemanage;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private string sql;

        public ucQueryBySpecSource(FS.HISFC.Models.Base.Employee login)
        {
            InitializeComponent();
            disTypeManage = new DisTypeManage();
            orgTypeManage = new OrgTypeManage();
            specTypemanage = new SpecTypeManage();
            loginPerson = login;
            SetSql();
        }

        private void SetSql()
        {
            sql = " SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID 标本号,  SPEC_TYPE.SPECIMENTNAME 标本类型,COM_DEPARTMENT.DEPT_NAME 送存科室,\n" +
                              " SPEC_DISEASETYPE.DISEASENAME 病种,SPEC_SOURCE.TUMORPOR 癌性质, SPEC_SOURCE_STORE.TUMORTYPE 癌种类,\n" +
                              " SPEC_SOURCE_STORE.STORETIME 入库时间,\n" +
                              " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) 出库次数,\n" +
                              " SPEC_BOX.BOXBARCODE 盒条码,\n" +
                              " SPEC_SUBSPEC.BOXENDROW 行,SPEC_SUBSPEC.BOXENDCOL 列,\n" +
                              " SPEC_SOURCE_STORE.TUMORPOS 肿物部位, SPEC_SOURCE.RADSCHEME 放疗方案, SPEC_SOURCE.MEDSCHEME 化疗方案, \n" +
                              " MAIN_DIANAME 主诊断, INHOS_DIANAME 入院诊断, CLINIC_DIANAME 门诊诊断, M_DIAGICDNAME 出院诊断,SPEC_SUBSPEC.STATUS 在库状态 ,SPEC_SUBSPEC.SUBBARCODE 条码\n" +
                              "  FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID \n" +
                              "  INNER JOIN SPEC_BOX ON SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID\n" +
                              "  INNER JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID\n" +
                              "  INNER JOIN SPEC_SOURCE ON  SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID\n" +
                              " LEFT JOIN SPEC_DISEASETYPE ON SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
                              " LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID\n" +
                              " LEFT JOIN	COM_DEPARTMENT ON COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO \n" +
                              " LEFT JOIN SPEC_BASE ON SPEC_BASE.SPECID = SPEC_SOURCE.SPECID \n" +
                              " WHERE ( SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3' OR SPEC_SUBSPEC.STATUS='2'))";        
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="strQuery"></param>
        public void GetQueryCondition(ref string strQuery)
        {
            #region sql语句
            strQuery = sql;
            if (rbtAnd.Checked)
            {
                strQuery += cmbDept.SelectedValue == null ? "" : " AND SPEC_SOURCE.DEPTNO =" + cmbDept.SelectedValue.ToString();
                strQuery += (cmbSpecType.SelectedValue != null && cmbSpecType.Text.Trim() != "") ? " AND SPEC_SOURCE_STORE.SPECTYPEID=" + cmbSpecType.SelectedValue.ToString() : "";
                strQuery += " AND SPEC_SOURCE_STORE.STORETIME >= to_date('" + dtpStartDate.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                strQuery += " AND SPEC_SOURCE_STORE.STORETIME <= to_date('" + dtpEndTime.Value.Date.AddDays(1.0) + "','yyyy-mm-dd hh24:mi:ss')";
                strQuery += txtBarCode.Text.Trim() == "" ? "" : " AND SPEC_SUBSPEC.SUBBARCODE = '" + txtBarCode.Text.Trim() + "'";
                strQuery += (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "") ? " AND SPEC_SOURCE.DISEASETYPEID = " + cmbDisType.SelectedValue.ToString() : "";
                strQuery += chkHIS.Checked ? " AND SPEC_SOURCE.ISHIS = '1'" : " AND SPEC_SOURCE.ISHIS = '0'";
            }
            if (rbtOr.Checked)
            {
                strQuery += cmbDept.SelectedValue == null ? "" : " OR SPEC_SOURCE.DEPTNO =" + cmbDept.SelectedValue.ToString();
                strQuery += (cmbSpecType.SelectedValue != null && cmbSpecType.Text.Trim() != "") ? " OR SPEC_SOURCE_STORE.SPECTYPEID=" + cmbSpecType.SelectedValue.ToString() : "";
                strQuery += " OR SPEC_SOURCE_STORE.STORETIME >= to_date('" + dtpStartDate.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                strQuery += " OR SPEC_SOURCE_STORE.STORETIME <= to_date('" + dtpEndTime.Value.Date.AddDays(1.0) + "','yyyy-mm-dd hh24:mi:ss')";
                strQuery += txtBarCode.Text.Trim() == "" ? "" : " OR SPEC_SUBSPEC.SUBBARCODE LIKE '%" + txtBarCode.Text.Trim() + "'%";
                strQuery += (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "") ? " OR SPEC_SOURCE.DISEASETYPEID = " + cmbDisType.SelectedValue.ToString() : "";
                strQuery += chkHIS.Checked ? " OR SPEC_SOURCE.ISHIS = '1'" : " OR SPEC_SOURCE.ISHIS = '0'";
            }
           #endregion
        }

        #region 私有函数：初始化界面
        /// <summary>
        /// 组织类型绑定
        /// </summary>
        private void OrgTypeBinding()
        {
            Dictionary<int, string> orgTypeDic = new Dictionary<int, string>();
            orgTypeDic = orgTypeManage.GetAllOrgType();
            if (orgTypeDic.Count > 0)
            {
                //orgTypeDic.Add(-1,"");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = orgTypeDic;               
                cmbOrgType.ValueMember = "Key";
                cmbOrgType.DisplayMember = "Value";
                cmbOrgType.DataSource = bsTmp;
                cmbOrgType.SelectedIndex = 0;
            }
        }
       
        /// <summary>
        /// 绑定病种类型
        /// </summary>
        private void DisTypeBinding()
        {
            Dictionary<int, string> dicDisType =new Dictionary<int,string>();
            dicDisType = disTypeManage.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                //dicDisType.Add(-1, "");
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
                cmbDisType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 根据组织类型ID获取标本类型
        /// </summary>
        /// <param name="orgTypeID"></param>
        private void SpecTypeBinding(string orgTypeID)
        {
            Dictionary<int, string> specTypeDic = new Dictionary<int, string>();
            cmbSpecType.DataSource = null;
            specTypeDic = specTypemanage.GetSpecTypeByOrgID(orgTypeID);
            if (specTypeDic.Count > 0)
            {
                //specTypeDic.Add(-1, "");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = specTypeDic;
                //cmbSpecName.DataSource = bsTmp;
                cmbSpecType.ValueMember = "Key";
                cmbSpecType.DisplayMember = "Value";
                cmbSpecType.DataSource = bsTmp;
                cmbSpecType.SelectedIndex = 0;
            }
        }

        #endregion

        private void ucQueryBySpecSource_Load(object sender, EventArgs e)
        {
            OrgTypeBinding();
            DisTypeBinding();

            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDepts = manager.GetMultiDept(loginPerson.ID);
            this.cmbDept.AddItems(alDepts);
            cmbDept.Text = "";
            dtpStartDate.Value = DateTime.Now.AddYears(-3);
            cmbOrgType.Text = "";
            cmbDisType.Text = "";
            cmbSpecType.Text = "";
        }

        private void cmbOrgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgTypeName = cmbOrgType.Text;
            int orgId = orgTypeManage.SelectOrgByName(orgTypeName);
            if (orgId > 0)
            {
                SpecTypeBinding(orgId.ToString());
            }            
        }
    }
}
