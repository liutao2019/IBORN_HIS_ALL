using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Finance.FinIpr
{
    public partial class ucFinIprOutpatientInfo :FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIprOutpatientInfo()
        {
            InitializeComponent();
        }

        string queryStr = string.Empty;
        string strDeptCode = "ALL";
        string strPatientID = string.Empty;
        ArrayList alDept = new ArrayList();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        #region ��ʼ��
        protected override void OnLoad(EventArgs e)
        {
            //����
            ArrayList list = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "ȫ��";
            alDept.Add(obj);

            list = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            alDept.AddRange(list);
            cmbDept.AddItems(alDept);
            cmbDept.SelectedIndex = 0;

            DateTime now = this.deptManager.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = new DateTime(now.Year, now.Month, now.Day, 00, 00, 01);
            this.dtpEndTime.Value = now;

           // base.OnLoad(e);
        }
        #endregion 

        #region ��ѯ
        protected override int OnRetrieve(params object[] objects)
        {

            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
          
        }
        #endregion 

        #region ����

        private void neuFilter_SelectedChanged(object sender, EventArgs e)
        {
            strDeptCode = cmbDept.SelectedItem.ID;
            strPatientID = ntbPatientID.Text.Trim().ToUpper().Replace(@"\", "");
            DataView dv = this.dwMain.Dv;
            if (dv == null)
            {
                return;
            }

            if (strDeptCode == "ALL")
            {
                queryStr = "סԺ�� like '%{1}'";

            }
            else
            {
                queryStr = "((dept_code = '{0}')) and ((סԺ�� like '%{1}') )";
            }

            //this.dwMain.SetFilter("");
            //this.dwMain.Filter();
            dv.RowFilter = "";

            string str = string.Format(this.queryStr, strDeptCode, strPatientID);
            //this.dwMain.SetFilter(str);
            //this.dwMain.Filter();
            try
            {
                dv.RowFilter = str;
            }
            catch
            {
                MessageBox.Show("�������������ַ�����������ȷ�Ĳ�ѯ��Ϣ��");
                return;
            }

        }


     

      

        #endregion 
    }
}