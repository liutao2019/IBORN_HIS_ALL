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
    public partial class ucPhaMetDrugInput : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaMetDrugInput()
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
       
        

        #endregion

    

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string deptNameStr = "ALL";
            string deptName = string.Empty;

            if (this.cmbDept.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbDept.SelectedItem.ID))
                {
                    deptNameStr = this.cmbDept.SelectedItem.ID;
                    deptName = this.cmbDept.SelectedItem.Name;
                }
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptNameStr, this.employee.Name, deptName);            
           
        }
        /// <summary>
        /// ��������¼�
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.cmbDept.Items.Clear();
           
            deptList = new ArrayList();            
            deptList = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.PI);//ҩ��                    
            this.cmbDept.AddItems(deptList);  

            this.isAcross = true;
            this.isSort = false;
        }

        

        
    }
}
