using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//created by zhang_tl in 2013-01-22
namespace Neusoft.SOC.Local.EMR.ZDLY.Forms
{
    public partial class frmRecordLock : Form
    {
        public frmRecordLock()
        {
            InitializeComponent();
        }

        Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.PatientInfoLogic clspatient = new Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.PatientInfoLogic();
        DataTable dt = new DataTable();

        private void frmRecordLock_Load(object sender, EventArgs e)
        {
            InitData();
            this.WindowState = FormWindowState.Maximized;

        }

        #region 初始化数据
        private void InitData()
        {
              dt = clspatient.GetRecordLock("abcd");
              this.gcLock.DataSource = dt.DefaultView;
              this.gvLock.BestFitColumns();
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
                    strFilter = strFilter + " and  (PATIENT_NO  " + filter + " or NAME " + filter + ")";
                }
                this.dt.DefaultView.RowFilter = strFilter;
                this.gcLock.DataSource = this.dt.DefaultView;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 解锁操作
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string strAllId = "-1";
            int[]  intArrRowsId= this.gvLock.GetSelectedRows();
            for (int i = 0; i < intArrRowsId.Length; i++)
            {

                strAllId = strAllId + "," + this.gvLock.GetRowCellValue(intArrRowsId[i], "ID");
            }
            strAllId = "(" + strAllId.TrimEnd(',') + ")";
            if (clspatient.DoUnLock(strAllId) > -1)
            {
                XtraMessageBox.Show("病历解锁成功");
                this.gvLock.DeleteSelectedRows();
            }
            else
            {
                XtraMessageBox.Show("病历解锁失败");
            }

        }
        #endregion
    }
}
