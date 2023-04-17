using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
//using Neusoft.Emr.Patient.UI.Internal.Controls;
using Neusoft.SOC.Local.EMR.ZDLY.Class;
using Neusoft.HISFC.Models.SIInterface;

namespace Neusoft.SOC.Local.EMR.ZDLY.Forms
{
    public partial class frmPatientList : Form
    {
        public frmPatientList()
        {
            InitializeComponent();
        }
        CurrentOperator user=new CurrentOperator (); 
        Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.PatientInfoLogic clspatient = new Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.PatientInfoLogic();

        string strUrl = "";
        private void frmPatientList_Load(object sender, EventArgs e)
        {
         
            InitgvPatientList();
            InitLoginDept();
            InitOther();
            this.WindowState = FormWindowState.Maximized;
            InitWebUrl();

        }

        #region 
        private void InitWebUrl()
        {
            strUrl=Neusoft.SOC.Local.EMR.ZDLY.Class.clsReadXml.GetEmrWebUrl();
            strUrl = strUrl + "?hisInpatientID={0}&inpatientID={0}&Out=0";
        }
        #endregion

        private void InitOther()
        {
            this.dtebegindate.DateTime = System.DateTime.Today.AddDays(-7);
            this.dteendate.DateTime = System.DateTime.Today;
            this.lueDeptList.Text = user.CurrentUser.Dept.Name;
            if (user.CurrentUser.EmployeeType.ID.ToString()== "D")
            {
                chkduty.Visible = true;
            }
        }
     
        #region 初始化列表头
        private void InitgvPatientList()
        {
            try
            {
                if (!System.IO.File.Exists("EmrData.xml"))
                {
                    MessageBox.Show("文件：EmrData.xml 不存在");
                    return;
                }
                XElement root = XElement.Load(@"EmrData.xml");
                var query1 =
                   from item in root.Elements("frmInpatientList-columns").Elements("patientlist")
                   select item;
                this.gvPatientList.Columns.Clear();
                int i = 0;
                foreach (var item in query1)
                {
                    this.gvPatientList.Columns.Add();
                    this.gvPatientList.Columns[i].Name = item.Attribute("FieldName").Value;
                    this.gvPatientList.Columns[i].FieldName = this.gvPatientList.Columns[i].Name.ToUpper();
                    this.gvPatientList.Columns[i].Caption = item.Attribute("Text").Value;
                    this.gvPatientList.Columns[i].Visible = (item.Attribute("IsVisible").Value == "true") ? true : false;
                    i++;
                }
                InitgvPatientListData(user.CurrentUser.Dept.ID.ToString(), "I","begin","end");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 初始化病人信息
        DataTable dt = new DataTable();
        private void InitgvPatientListData(string strdept_code,string strInstate,string begindate,string enddate)
        {

            dt = clspatient.GetPatientInfo(new string[] { user.CurrentUser.CurrentGroup.Name, strdept_code,strInstate,begindate,enddate});
                    this.grcPatientList.DataSource = dt.DefaultView;
                    this.gvPatientList.BestFitColumns();

        }
        #endregion

        #region 过滤操作
        private void emrFindText1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strFilter = "1=1";
                if (this.emrFindText1.Text.Trim() != "" && this.emrFindText1.Text.Trim() != this.emrFindText1.HintStr)
                {
                    string filter = " like '%" + this.emrFindText1.Text.Trim() + "%'";
                    strFilter = strFilter + " and  (PATIENT_NO  " + filter + " or NAME " + filter + " or BED_NO" + filter+")";
                }
                if (this.chkduty.Checked)
                {
                   strFilter= strFilter + " and HOUSE_DOC_NAME='" + user.Operator.Name + "'";
                }
                this.dt.DefaultView.RowFilter = strFilter;
                this.grcPatientList.DataSource = this.dt.DefaultView;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region 显示行号
        private void gvPatientList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();

        }
        #endregion

        #region 科室改变事件
        private void lueDeptList_EditValueChanged(object sender, EventArgs e)
        {
            InitgvPatientListData(this.lueDeptList.EditValue.ToString(), (rgpInstate.SelectedIndex==1) ? "O" : "I",this.dtebegindate.DateTime.ToShortDateString(),this.dteendate.DateTime.ToShortDateString());

        }
        #endregion

        #region 科室控件赋值
        private void InitLoginDept()
        {
            this.lueDeptList.Properties.DisplayMember = "科室";
            this.lueDeptList.Properties.ValueMember = "编码";
            this.lueDeptList.Properties.DataSource = clspatient.GetLoginDept(user.Operator.ID.ToString());

        }
        #endregion

        #region 入院出院状态改变
        private void rgpInstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitgvPatientListData(this.lueDeptList.EditValue.ToString(), (rgpInstate.SelectedIndex == 1) ? "O" : "I",this.dtebegindate.DateTime.ToShortDateString(),this.dteendate.DateTime.ToShortDateString());
            if (this.rgpInstate.SelectedIndex ==1)
            {
                this.pnloutdate.Visible = true;
            }
            else
            {
                this.pnloutdate.Visible = false;
            }
        }
        #endregion

        #region 查询操作
        private void btnSearch_Click(object sender, EventArgs e)
        {
            InitgvPatientListData(this.lueDeptList.EditValue.ToString(), "O", this.dtebegindate.DateTime.ToShortDateString(), this.dteendate.DateTime.ToShortDateString());
        }
        #endregion

        #region 管床医生
        private void chkduty_CheckedChanged(object sender, EventArgs e)
        {
            string strFilter = "1=1";
            if (this.emrFindText1.Text.Trim() != "" && this.emrFindText1.Text.Trim() != this.emrFindText1.HintStr)
            {
                string filter = " like '%" + this.emrFindText1.Text.Trim() + "%'";
                strFilter = strFilter + " and  (PATIENT_NO  " + filter + " or NAME " + filter + " or BED_NO" + filter+")";
            }
            if (this.chkduty.Checked)
            {
                strFilter = strFilter + " and HOUSE_DOC_NAME='" + user.Operator.Name + "'";
            }
            this.dt.DefaultView.RowFilter = strFilter;
            this.grcPatientList.DataSource = this.dt.DefaultView;

        }
        #endregion 

        #region 双击跳进Url
        private void gvPatientList_DoubleClick(object sender, EventArgs e)
        {
            string strPatientId = "";
            string Url = "";
            if (this.gvPatientList.FocusedRowHandle > -1)
            {

                strPatientId = this.gvPatientList.GetFocusedRowCellValue("INPATIENT_NO").ToString();
                Url = string.Format(strUrl, strPatientId);
                System.Diagnostics.Process.Start(Url);
            }
        }
        #endregion

    }
}
