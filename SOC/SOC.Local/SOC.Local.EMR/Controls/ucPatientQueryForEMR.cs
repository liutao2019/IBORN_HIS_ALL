using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace FS.SOC.Local.EMR.Controls
{
    public partial class ucPatientQueryForEMR : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        FS.SOC.Local.EMR.EMRService.EMRManage EMRManage = new FS.SOC.Local.EMR.EMRService.EMRManage();
        /// <summary>
        /// 公共管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        DataSet dsPatient = new DataSet();

        public ucPatientQueryForEMR()
        {
            InitializeComponent();
        }

        //查询
        protected override int OnQuery(object sender, object neuObject)
        {
            string strSql = @"SELECT 
       VIEW_INP.PATIENT_NO            AS 住院号,       
       VIEW_INP.INPATIENT_NO          AS 住院流水号,
       VIEW_INP.BED_NO                AS 床号,
       VIEW_INP.CARD_NO               AS 门诊号,      
       VIEW_INP.NAME                  AS 姓名,
       decode(VIEW_INP.SEX_CODE,'M','男','F','女','其他')              AS 性别,
       VIEW_INP.IN_DATE               AS 入院日期,      
       VIEW_INP.DEPT_NAME             AS 住院科室,
       VIEW_INP.PACT_NAME             AS 合同单位,      
       VIEW_INP.NURSE_CELL_NAME       AS 护理单元,
       --VIEW_INP.IN_AVENUE             AS INAVENUE,
       --VIEW_INP.IN_SOURCE             AS INSOURCE,
       --VIEW_INP.FREE_COST             AS FREECOST,
       decode(VIEW_INP.IN_STATE,'I','在院','O','已出院')   AS 住院状态,
       VIEW_INP.OUT_DATE              AS 出院日期,
       VIEW_INP.IDENNO                AS 身份证号,
       VIEW_INP.BIRTHDAY              AS 生日,                          
       decode(VIEW_INP.MARI,'W','丧偶','A','分居','R','再婚','D','失婚','M','已婚','S','未婚') AS 婚姻状况,
       VIEW_INP.COUN_CODE             AS 国籍,
       VIEW_INP.DIST                  AS 籍贯,
       VIEW_INP.HOME                  AS 家庭住址,
       VIEW_INP.HOME_TEL              AS 家庭电话,
       VIEW_INP.HOME_ZIP              AS 邮政,
       VIEW_INP.NATION_CODE           AS 民族,
       VIEW_INP.LINKMAN_NAME          AS 联系人,
       VIEW_INP.LINKMAN_TEL           AS 联系人电话,
       VIEW_INP.LINKMAN_ADD           AS 联系人住址,
       VIEW_INP.RELA_CODE             AS 关系        
FROM   VHIS_INPATIENTINFO VIEW_INP,PT_INPATIENT_CURE EMR_INPA
WHERE  
       VIEW_INP.ID = EMR_INPA.ID
       AND VIEW_INP.IN_STATE = '{0}' 
       and (VIEW_INP.DEPT_CODE='{1}' or 'ALL'='{1}')
";

            //string strWhereI = @"and VIEW_INP.IN_DATE>=to_date('{2}','yyyy-MM-dd hh24:mi:ss')
            //and VIEW_INP.IN_DATE<=to_date('{3}','yyyy-MM-dd hh24:mi:ss')";
            string strWhereO = @"and VIEW_INP.OUT_DATE>=to_date('{2}','yyyy-MM-dd hh24:mi:ss')
            and VIEW_INP.OUT_DATE<=to_date('{3}','yyyy-MM-dd hh24:mi:ss')";
            if (rbtnIPatient.Checked)
            {
                //strSql = strSql + strWhereI;
                //strSql = string.Format(strSql, "I", cmbDept.Tag.ToString(), dtpTimeStart.Text.ToString(), dtpTimeEnd.Text.ToString());
                strSql = string.Format(strSql, "I", cmbDept.Tag.ToString());
            }
            else
            {
                strSql = strSql + strWhereO;
                strSql = string.Format(strSql, "O", cmbDept.Tag.ToString(), dtpTimeStart.Text.ToString(), dtpTimeEnd.Text.ToString());
            }
            EMRManage.QueryPatientByTime(strSql, ref dsPatient);
            dgvPatient.DataSource = dsPatient.Tables[0].DefaultView;
            return base.OnQuery(sender, neuObject);
        }

        //患者选项更改
        private void rbtnOPatient_Click(object sender, EventArgs e)
        {
            if (rbtnIPatient.Checked)
            {
                lbQueryCon.Text = "入院日期：";
                lbQueryCon.Enabled = false;
                neuLabel2.Enabled = false;
                dtpTimeStart.Enabled = false;
                dtpTimeEnd.Enabled = false;
            }
            else if (this.rbtnOPatient.Checked)
            {
                lbQueryCon.Text = "出院日期：";
                lbQueryCon.Enabled = true;
                neuLabel2.Enabled = true;
                dtpTimeStart.Enabled = true;
                dtpTimeEnd.Enabled = true;
            }
        }

        //双击指向web
        private void dgvPatient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex<0)
            {
                return;
            }
            string InpatientID = dgvPatient.Rows[e.RowIndex].Cells["住院流水号"].Value.ToString();         
            string xmlpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "WebServerConfig.xml";          
            string errInfo;
            string url = "http://" + EMRManage.ReadXML(xmlpath, "ServerIP", "10.80.212.52", out errInfo)
                             + ":"+EMRManage.ReadXML(xmlpath, "ServerPort", ":80/WebEmr", out errInfo)
                             //+ "/"+EMRManage.ReadXML(xmlpath, "ServerUrl", "/Default.aspx", out errInfo)
                             + "/Default.aspx"
                             + "?inpatientID=" + InpatientID + "&recordID=&Out=0";            
            Process.Start(url);
        }

        private void ucPatientQueryForEMR_Load(object sender, EventArgs e)
        {
            //初始化科室下拉选框
            cmbDept.DataSource = null;
            ArrayList deptList = this.managerIntegrate.QueryDeptmentsInHos(true);
            if (deptList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("初始化科室列表出错!");
            }
            FS.HISFC.Models.Base.Department departmentAll = new FS.HISFC.Models.Base.Department();
            departmentAll.ID = "ALL";
            departmentAll.Memo = "全部";
            departmentAll.Name = "全部";
            deptList.Insert(0, departmentAll);
            this.cmbDept.AddItems(deptList);
            cmbDept.SelectedIndex = 0;

        }

        #region 三条件联合筛选
        private void txtPatient_TextChanged(object sender, EventArgs e)
        {
            this.txtFilter();
        }

        private void txtNameFilter_TextChanged(object sender, EventArgs e)
        {
            this.txtFilter();
        }

        private void txtPactFilter_TextChanged(object sender, EventArgs e)
        {
            this.txtFilter();
        }

        private void txtFilter()
        {
            string filterStr = "住院号 like '%" + this.txtPatient.Text.Trim() + "%'"
                   + " and  姓名 like '%" + this.txtNameFilter.Text.Trim() + "%'"
                   + " and 合同单位 like '%" + this.txtPactFilter.Text.Trim() + "%'";
            DataView rowfilter = new DataView(dsPatient.Tables[0]);
            rowfilter.RowFilter = filterStr;
            rowfilter.RowStateFilter = DataViewRowState.OriginalRows;
            dgvPatient.DataSource = rowfilter;
        }
        #endregion

        private void dgvPatient_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvPatient.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        
    }
}
