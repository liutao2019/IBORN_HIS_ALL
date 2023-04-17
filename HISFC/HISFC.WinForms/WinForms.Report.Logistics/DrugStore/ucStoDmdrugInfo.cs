using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Logistics.DrugStore
{
    public partial class ucStoDmdrugInfo : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucStoDmdrugInfo()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        ArrayList alDept = new ArrayList();
        ArrayList alDrugQuality = new ArrayList();
        string deptId = "ALL";
        string drugQua = "ALL";

        #region ��ʼ��
        protected override void OnLoad(EventArgs e)
        {
            
           //ҩ������
            ArrayList list = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = "ALL";
            obj.Name = "ȫ��";
            alDept.Add(obj);

            list = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            alDept.AddRange(list);

            cmbDeptName.AddItems(alDept);
            cmbDeptName.SelectedIndex = 0;

            //ҩƷ����
            obj = new FS.FrameWork.Models.NeuObject();
            list = new ArrayList();

            obj.ID = "ALL";
            obj.Name = "ȫ��";
            alDrugQuality.Add(obj);

            list = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            alDrugQuality.AddRange(list);
            cmbDrugQua.AddItems(alDrugQuality);
            cmbDrugQua.SelectedIndex = 0;

            base.OnLoad(e);
        }
        #endregion 

        #region ��ѯ
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            deptId = cmbDeptName.SelectedItem.ID;
            drugQua = cmbDrugQua.SelectedItem.ID;
            return base.OnRetrieve(deptId,this.beginTime,this.endTime,drugQua);
           
        }
        #endregion 
    }
}