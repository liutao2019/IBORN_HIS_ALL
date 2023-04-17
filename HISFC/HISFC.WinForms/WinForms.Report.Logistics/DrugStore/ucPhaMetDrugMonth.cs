using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Logistics.DrugStore
{
    public partial class ucPhaMetDrugMonth : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaMetDrugMonth()
        {
            InitializeComponent();
        }
        #region ����
      
        /// <summary>
        /// ���Ź�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// ���ڴ洢ҩ������б�
        /// </summary>
        private ArrayList deptList = new ArrayList();
        /// <summary>
        /// ���ڴ洢ҩ������б�
        /// </summary>
        private ArrayList deptListYK = new ArrayList();
        /// <summary>
        /// ���ڴ洢�½�ʱ����
        /// </summary>
        private ArrayList dayList = new ArrayList();
        

        #endregion

    

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string deptNameStr = "ȫԺ";
            string dayStr = "ALL";

            if (this.cmbDept.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbDept.SelectedItem.ID))
                {
                    deptNameStr = this.cmbDept.SelectedItem.ID;
                }
            }
            if (this.cmbDateList.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbDateList.SelectedItem.Name))
                {
                    dayStr = this.cmbDateList.SelectedItem.Name;
                }
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptNameStr, dayStr);            
           
        }
        /// <summary>
        /// ��������¼�
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.cmbDept.Items.Clear();
            this.cmbDateList.Items.Clear();
            deptList = new ArrayList();
            deptListYK = new ArrayList();
            dayList = new ArrayList();
            deptList = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);//ҩ��
            deptListYK = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.PI);//ҩ��          
  
            foreach(FS.HISFC.Models.Base.Department deptObj in deptListYK)
            {
                deptList.Add(deptObj);
            }
            this.cmbDept.AddItems(deptList);            
           
            dayList = this.QueryDateList();
            if (dayList != null)
            {
                this.cmbDateList.AddItems(dayList);
                
                
            }           

            

            this.isAcross = true;
            this.isSort = false;
        }

        /// <summary>
        /// ��ȡ�Ѿ��½�Ĳ���ʱ����
        /// </summary>
        /// <returns></returns>
        private ArrayList QueryDateList()
        {
            string strSql = string.Empty;

            strSql = string.Format("select distinct(to_char(t.oper_date,'yyyymmdd')) from pha_com_ms_drug t ");
           
            FS.HISFC.BizLogic.Manager.Report reportManager = new FS.HISFC.BizLogic.Manager.Report();
            if (reportManager.ExecQuery(strSql) == -1)
            {
                MessageBox.Show("û���ҵ�����!");

                return null;
            }
            ArrayList arrdayList = new ArrayList();
            string strday = string.Empty;
            FS.HISFC.Models.Base.Const constObj = new FS.HISFC.Models.Base.Const();
           
            try
            {
                while (reportManager.Reader.Read())
                {
                    constObj = new FS.HISFC.Models.Base.Const();
                    constObj.ID = reportManager.Reader[0].ToString();
                    constObj.Name = reportManager.Reader[0].ToString();

                    arrdayList.Add(constObj);

                }

                reportManager.Reader.Close();

                return arrdayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (!reportManager.Reader.IsClosed)
                {
                    reportManager.Reader.Close();
                }

                return null;
            }
        }

        
    }
}
