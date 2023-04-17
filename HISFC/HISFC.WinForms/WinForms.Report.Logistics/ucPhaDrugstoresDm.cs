using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Logistics.Pharmacy
{
    public partial class ucPhaDrugstoresDm : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaDrugstoresDm()
        {
            InitializeComponent();
        }
        FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.Pharmacy intePharmacy = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        private void ucPhaDrugstoresDm_Load(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���������У����Ժ򡭡�");
            Application.DoEvents();
            this.cmbDrugQua.AddItems(inteManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY));
            this.cmbDrugName.AddItems(inteManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P));
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        protected override int OnRetrieve(params object[] objects)
        {

            string indept = "1";
            string qualityID = "2";

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            if (cmbDrugName.SelectedItem == null)
            {
                MessageBox.Show("��ѡ��ҩ����");
                return -1;
                indept = cmbDrugName.SelectedItem.ID;
            }
            if (cmbDrugQua.SelectedItem == null)
            {
                MessageBox.Show("��ѡ��ҩƷ���ʣ�");
                return -1;
                qualityID = cmbDrugQua.SelectedItem.ID;
            }
            return base.OnRetrieve(this.dtpBeginDate.Value, this.dtpEndDate.Value, indept, qualityID);

        }




    }
}
