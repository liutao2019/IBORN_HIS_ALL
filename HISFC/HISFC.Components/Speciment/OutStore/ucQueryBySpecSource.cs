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
        //��֯���͹������
        private OrgTypeManage orgTypeManage;
        //�걾���͹������
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
            sql = " SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID �걾��,  SPEC_TYPE.SPECIMENTNAME �걾����,COM_DEPARTMENT.DEPT_NAME �ʹ����,\n" +
                              " SPEC_DISEASETYPE.DISEASENAME ����,SPEC_SOURCE.TUMORPOR ������, SPEC_SOURCE_STORE.TUMORTYPE ������,\n" +
                              " SPEC_SOURCE_STORE.STORETIME ���ʱ��,\n" +
                              " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) �������,\n" +
                              " SPEC_BOX.BOXBARCODE ������,\n" +
                              " SPEC_SUBSPEC.BOXENDROW ��,SPEC_SUBSPEC.BOXENDCOL ��,\n" +
                              " SPEC_SOURCE_STORE.TUMORPOS ���ﲿλ, SPEC_SOURCE.RADSCHEME ���Ʒ���, SPEC_SOURCE.MEDSCHEME ���Ʒ���, \n" +
                              " MAIN_DIANAME �����, INHOS_DIANAME ��Ժ���, CLINIC_DIANAME �������, M_DIAGICDNAME ��Ժ���,SPEC_SUBSPEC.STATUS �ڿ�״̬ ,SPEC_SUBSPEC.SUBBARCODE ����\n" +
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
        /// ���ò�ѯ����
        /// </summary>
        /// <param name="strQuery"></param>
        public void GetQueryCondition(ref string strQuery)
        {
            #region sql���
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

        #region ˽�к�������ʼ������
        /// <summary>
        /// ��֯���Ͱ�
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
        /// �󶨲�������
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
        /// ������֯����ID��ȡ�걾����
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
