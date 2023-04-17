using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.MetCas
{
    public partial class ucMetCasStatDead : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucMetCasStatDead()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Department deptManger = new FS.HISFC.BizLogic.Manager.Department();
        System.Collections.ArrayList alDeptList = null;
        private string patientNo = string.Empty;
        private string patientName = string.Empty;
        private string deptId = string.Empty;
        private string deptName = string.Empty;

         

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            if (string.IsNullOrEmpty(tbpatientNo.Text))
            {
                patientNo = "%%";
            }
            else
            {
                patientNo = "%" + tbpatientNo.Text.Trim() + "%";
                //patientNo = patientNo.PadLeft(10, '0');
                //this.tbpatientNo.Text = patientNo;
            }

            if (string.IsNullOrEmpty(tbpatientName.Text))
            {
                patientName = "%%";
            }
            else
            {
                patientName = "%" + tbpatientName.Text.Trim() + "%";
            }

             return base.OnRetrieve(base.beginTime, base.endTime, patientNo, patientName, deptId);
          
        }

        private void ucMetCasStatDead_Load(object sender, EventArgs e)
        {


            FS.HISFC.Models.Base.Department objAll = new FS.HISFC.Models.Base.Department();

            objAll.ID = "ALL";
            objAll.Name = "ȫ��";

            alDeptList = deptManger.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            //foreach (FS.HISFC.Models.Base.Department dept in alDeptList)
            //{
            //    cboDeptCode.Items.Add(dept);
            //    //deptId = dept.ID;
            //    //deptName = dept.Name;
            //}
            ////�������  
            ////this.cboDeptCode.AddItems(alDeptList);

            //foreach (FS.HISFC.Models.Base.Department dept in alDeptList)
            //{                
            //    deptId = dept.ID;
            //    deptName = dept.Name;
            //}
            //�������  
            alDeptList.Add(objAll);
            this.cboDeptCode.AddItems(alDeptList);
            cboDeptCode.SelectedIndex = alDeptList.Count-1;
            DateTime currentDateTime = this.deptManger.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 00, 00, 00);
            this.dtpEndTime.Value = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 23, 59, 59);
           
        }

        private void cboDeptCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            deptId = ((FS.HISFC.Models.Base.Department)alDeptList[this.cboDeptCode.SelectedIndex]).ID;
            deptName = ((FS.HISFC.Models.Base.Department)alDeptList[this.cboDeptCode.SelectedIndex]).Name;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {

                this.dwMain.Print();
                return 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ����", "��ʾ");
                return -1;
            }

        }
    }
}
