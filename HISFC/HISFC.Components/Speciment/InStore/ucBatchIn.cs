using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;
using FS.HISFC.Models.Speciment;

namespace FS.HISFC.Components.Speciment.InStore
{

    public partial class ucBatchIn : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SpecTypeManage specTypeMange;
        private OrgTypeManage orgTypeManage;
        private DisTypeManage disTypeManage;
        private SubSpecManage subSpecMange;
        private SpecBoxManage specBoxManage;
        private IceBoxManage iceBoxMange;
        private BoxSpecManage boxSpecManage;
        private BarCodeManage barCodeManage;
        private SpecSourcePlanManage specPlanManage;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private List<string> subSpecList;
        private List<int> rowIndexList;
        private List<SubSpec> newSpecList;
        private SpecOutOper specOutOper;
        private string sql = "";
        public ucBatchIn()
        {
            InitializeComponent();
            specTypeMange = new SpecTypeManage();
            orgTypeManage = new OrgTypeManage();
            disTypeManage = new DisTypeManage();
            subSpecMange = new SubSpecManage();
            specBoxManage = new SpecBoxManage();
            iceBoxMange = new IceBoxManage();
            subSpecList = new List<string>();
            rowIndexList = new List<int>();
            boxSpecManage = new BoxSpecManage();
            barCodeManage = new BarCodeManage();
            specPlanManage = new SpecSourcePlanManage();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            newSpecList = new List<SubSpec>();
        }

        #region 私有函数 
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

                cmbOrgType1.ValueMember = "Key";
                cmbOrgType1.DisplayMember = "Value";
                cmbOrgType1.DataSource = bsTmp;
                cmbOrgType1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定病种类型
        /// </summary>
        private void DisTypeBinding()
        {
            Dictionary<int, string> dicDisType = new Dictionary<int, string>();
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
            cmbDisType.Text = "";
        }

        /// <summary>
        /// 根据组织类型ID获取标本类型
        /// </summary>
        /// <param name="orgTypeID"></param>
        private void SpecTypeBinding(string orgTypeID,string sender)
        {
            Dictionary<int, string> specTypeDic = new Dictionary<int, string>();    
            specTypeDic = specTypeMange.GetSpecTypeByOrgID(orgTypeID);
            if (specTypeDic.Count > 0)
            {
                //specTypeDic.Add(-1, "");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = specTypeDic;

                if (sender == "cmbOrgType1")
                {                   
                    cmbSpecType1.ValueMember = "Key";
                    cmbSpecType1.DisplayMember = "Value";
                    cmbSpecType1.DataSource = bsTmp;
                    cmbSpecType1.Text = "";
                } 
            }
        }

        /// <summary>
        /// 拼接sql语句
        /// </summary>
        private void GenerateSql()
        {
            sql = " select SPEC_SUBSPEC.SUBSPECID, SPEC_SUBSPEC.SUBBARCODE, SPEC_SUBSPEC.SPECID,SPEC_SUBSPEC.STORETIME,\n" +
                  " SPEC_TYPE.SPECIMENTNAME, SPEC_SUBSPEC.LASTRETURNTIME, SPEC_SUBSPEC.BOXID,SPEC_SUBSPEC.BOXENDCOL,\n"+
                  " SPEC_SUBSPEC.BOXENDROW,SPEC_BOX.BLOODORORGID,SPEC_BOX.BOXBARCODE, SPEC_DISEASETYPE.DISEASENAME,\n"+
                  " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) OUTCOUNT,\n"+
                  "  SPEC_BOX.DESCAPROW,SPEC_BOX.DESCAPCOL,SPEC_BOX.DESCAPSUBLAYER,SPEC_SUBSPEC.SPECTYPEID,SPEC_BOX.DISEASETYPEID\n" +
                  " from SPEC_SUBSPEC left join spec_box on SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID\n"+
                  " left join spec_type on SPEC_SUBSPEC.SPECTYPEID= Spec_type.SPECIMENTTYPEID\n"+
                  " left join spec_diseasetype on SPEC_BOX.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID where (SPEC_SUBSPEC.SUBSPECID>0)";
            #region 选项
            if (rbtAnd.Checked)
            {
                if (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "")
                {
                    sql += " and SPEC_DISEASETYPE.DISEASETYPEID = " + cmbDisType.SelectedValue.ToString();
                }
                if (cmbSpecType1.SelectedValue != null && cmbSpecType1.Text.Trim() != "")
                {
                    sql += " and Spec_type.SPECIMENTTYPEID = " + cmbSpecType1.SelectedValue.ToString();
                }
                if (chkIn.Checked)
                {
                    sql += " and SPEC_SUBSPEC.STATUS = '1'";
                }
                if (chkBacked.Checked)
                {
                    sql += " and SPEC_SUBSPEC.STATUS='3'"; 
                }
                if (chkOut.Checked)
                {
                    sql += " and SPEC_SUBSPEC.STATUS='2'";
                }
            }
            if (rbtOr.Checked)
            {
                if (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "")
                {
                    sql += " or SPEC_DISEASETYPE.DISEASETYPEID = " + cmbDisType.SelectedValue.ToString();
                }
                if (cmbSpecType1.SelectedValue != null && cmbSpecType1.Text.Trim() != "")
                {
                    sql += " or Spec_type.SPECIMENTTYPEID = " + cmbSpecType1.SelectedValue.ToString();
                }
                if (chkIn.Checked)
                {
                    sql += " or SPEC_SUBSPEC.STATUS = '1'";
                }
                if (chkBacked.Checked)
                {
                    sql += " or SPEC_SUBSPEC.STATUS='3'";
                }
                if (chkOut.Checked)
                {
                    sql += " or SPEC_SUBSPEC.STATUS='2'";
                }
            }
            #endregion
        }

        private void GetAppSql()
        {
            sql = @"select SPEC_SUBSPEC.SUBSPECID, SPEC_SUBSPEC.SUBBARCODE, SPEC_SUBSPEC.SPECID,SPEC_SUBSPEC.STORETIME,
                  SPEC_TYPE.SPECIMENTNAME, SPEC_SUBSPEC.LASTRETURNTIME, SPEC_SUBSPEC.BOXID,SPEC_SUBSPEC.BOXENDCOL,
                  SPEC_SUBSPEC.BOXENDROW,SPEC_BOX.BLOODORORGID,SPEC_BOX.BOXBARCODE, SPEC_DISEASETYPE.DISEASENAME,
                  (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) OUTCOUNT,
                  SPEC_BOX.DESCAPROW,SPEC_BOX.DESCAPCOL,SPEC_BOX.DESCAPSUBLAYER,SPEC_SUBSPEC.SPECTYPEID,SPEC_BOX.DISEASETYPEID
                  from SPEC_SUBSPEC left join spec_box on SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID
                  left join spec_type on SPEC_SUBSPEC.SPECTYPEID= Spec_type.SPECIMENTTYPEID
                  left join spec_diseasetype on SPEC_BOX.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID
                  join SPEC_OUT on SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID where SPEC_OUT.RELATEID = {0}";
            string othStr = string.Empty;
            if ((this.cbNoBack.Checked) && (this.cbReturn.Checked) && (this.cbSomeTimes.Checked))
            {
                //全选不用做什么
            }
            else
            {
                if (this.cbNoBack.Checked)
                {
                    othStr += " SPEC_SUBSPEC.STATUS = '4' or";
                }
                if (this.cbReturn.Checked)
                {
                    othStr += " SPEC_SUBSPEC.STATUS = '2' or";
                }
                if (this.cbSomeTimes.Checked)
                {
                    othStr += " SPEC_SUBSPEC.STATUS = '3' or";
                }
                if (!string.IsNullOrEmpty(othStr))
                {
                    sql += " and" + othStr.TrimEnd('o','r'); 
                }
            }

        }
        #endregion

        private void orgTypeSelectedIndexChanged1(object sender, EventArgs e)
        {
            if (cmbOrgType1.SelectedValue != null && cmbOrgType1.Text.Trim() != "")
            {
                int orgId = Convert.ToInt32(cmbOrgType1.SelectedValue.ToString());
                SpecTypeBinding(orgId.ToString(), "cmbOrgType1");
            }
        }

        private void ucBatchIn_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            specOutOper = new SpecOutOper(loginPerson);
            DisTypeBinding();
            OrgTypeBinding();
            cmbDisType.Text = "";      
            cmbOrgType1.Text = "";
            cmbSpecType1.Text = "";
        }

        public override int Query(object sender, object neuObject)
        {
            if (cmbDisType.Text == "" && cmbOrgType1.Text == "" && cmbSpecType1.Text == "")
            {
                MessageBox.Show("请输入查询条件！");
                return -1;
            }
            splitContainer1.Panel2.Controls.Clear();
            ucSpecReturn ucSpecReturn = new ucSpecReturn();
            GenerateSql();
            ucSpecReturn.Sql = this.sql;
            ucSpecReturn.DataBinding();
            ucSpecReturn.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(ucSpecReturn);
            return base.Query(sender, neuObject);
        }

        private void tbApplyNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string appNum = string.Empty;
                appNum = tbApplyNum.Text.Trim();
                if (!string.IsNullOrEmpty(appNum))
                {
                    splitContainer1.Panel2.Controls.Clear();
                    ucSpecReturn ucSpecReturn = new ucSpecReturn();
                    GetAppSql();
                    ucSpecReturn.Sql = string.Format(this.sql,appNum);
                    ucSpecReturn.DataBinding();
                    ucSpecReturn.Dock = DockStyle.Fill;
                    splitContainer1.Panel2.Controls.Add(ucSpecReturn);
                }
                else
                {
                    MessageBox.Show("请输入申请单号！");
                    return;
                }
            }
        }      
    }
}
