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
    public partial class ucStoDrugInputAcc : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucStoDrugInputAcc()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;    
       
        ArrayList alDept = new ArrayList();
        
        string deptId = "ȫԺ";
        string deptName = "ȫԺ";
         protected override void OnLoad(EventArgs e)
        {
            
           //ҩ������
            ArrayList list = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = "ȫԺ";
            obj.Name = "ȫԺ";
            alDept.Add(obj);

            list = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            alDept.AddRange(list);

            cmbDeptName.AddItems(alDept);
            cmbDeptName.SelectedIndex = 0;

            base.OnLoad(e);
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }
           
            deptId = cmbDeptName.SelectedItem.ID;
            deptName = cmbDeptName.SelectedItem.Name;
            int  RetrieveRow = base.OnRetrieve(this.beginTime, this.endTime, deptId, empl.Name);

           int rownum = dwMain.RowCount;

           for (int i = 1; i <= rownum; i++)
            {
                dwMain.SetItemString(i, "����Ա", empl.Name);
            }

                dwMain.Modify("dept_name.text = '" + deptName + "'");
            return 1;
           
        }

    }
}
