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
    public partial class ucStoDrugYh : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucStoDrugYh()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        ArrayList alDept = new ArrayList();
        string deptId = "ALL";
        string deptName = "ȫԺ";
         protected override void OnLoad(EventArgs e)
        {
            
           //ҩ������
            ArrayList list = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = "ALL";
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
            dwMain.Modify("dept_name.text = '" + deptName + "'");
            return base.OnRetrieve(deptId);
        }
    }
}
