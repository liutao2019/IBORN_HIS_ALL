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
    public partial class ucDrugStoreNotUse : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        //#region ����

        //private FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        //string strFilterByDrug = "(ҩƷ���� like '%{0}%') or (drug_spellcode like '%{0}%')";
        //string strFilterByOper = "(������ like '%{0}%') or (oper_spellcode like '%{0}%')";
       

        //#endregion

        //public ucPhaAdjustPriceByList()
        //{
        //    InitializeComponent();
        //}

        //private void ucPhaAdjustPriceByList_Load(object sender, EventArgs e)
        //{
        //    this.dtpBeginTime.Value = this.dtpBeginTime.Value.Date;
        //    this.dtpEndTime.Value = this.dtpEndTime.Value.Date.AddDays(1).AddMilliseconds(-1);
        //}

        //#region ����

        //private void DrugFilter()
        //{
        //    string str = "";
        //    string drug = txtDrug.Text.Trim().Replace(@"\", "").Replace(@"'", "").ToUpper();

        //    DataView dv = this.dwMain.Dv;
        //    if (dv == null)
        //    {
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(drug))
        //    {
        //        //dwMain.SetFilter("");
        //        //dwMain.Filter();

        //        dv.RowFilter = "";
        //        return;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            str = string.Format(strFilterByDrug,drug);
        //            dv.RowFilter = str;
        //        }
        //        catch(Exception ex)
        //        {
        //            MessageBox.Show("��ʽ���ַ������� !");
        //            return;
        //        }
        //        //dwMain.SetFilter(str);
        //        //dwMain.Filter();
        //    }
        //    //dwMain.SetSort("���۵���");
        //    //dwMain.Sort();
        //}

        //private void OperFilter()
        //{
        //    string str = "";
        //    string oper = txtOper.Text.Trim().Replace(@"\", "").Replace(@"'", "").ToUpper();

        //    DataView dv = this.dwMain.Dv;
        //    if (dv == null)
        //    {
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(oper))
        //    {
        //        //dwMain.SetFilter("");
        //        //dwMain.Filter();
        //        dv.RowFilter = "";
        //        return;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            str = string.Format(strFilterByOper,oper);

        //            dv.RowFilter = str;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("��ʽ���ַ������� !");
        //            return;
        //        }
        //        //dwMain.SetFilter(str);
        //        //dwMain.Filter();
        //    }
        //    //dwMain.SetSort("���۵���");
        //    //dwMain.Sort();
        //}

       


        //protected override int OnRetrieve(params object[] objects)
        //{
        //    string adjustNo = "%"+ txtNo.Text.Trim()+"%";

        //    if(this.dtpBeginTime.Value > this.dtpEndTime.Value)
        //    {
        //        MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!");
        //        return -1;
        //    }
        //    return base.OnRetrieve(empl.Dept.ID, this.dtpBeginTime.Value,this.dtpEndTime.Value,adjustNo);
        //}

        //#endregion

        //#region �¼�

        //private void txtDrug_TextChanged(object sender, EventArgs e)
        //{
        //    this.DrugFilter();
        //}

        //private void txtOper_TextChanged(object sender, EventArgs e)
        //{
        //    this.OperFilter();
        //}

        //#endregion

        private string emplCode = string.Empty;
        private string emplName = string.Empty;
        ArrayList alDept = new ArrayList();

        protected override void OnLoad(EventArgs e)
        {
           
            ///<summary>
            /// ��ʼ������
            ///<summary>

         
            
            //FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            //FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            //obj.ID = "ALL";
            //obj.Name = "ȫ��";
            //alDept.Add(obj);

            //ArrayList dept = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.PI);
            //alDept.AddRange(dept);

            //alDept.AddRange(managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P));

            //this.neuDept.AddItems(alDept);
            //this.neuDept.SelectedIndex = 0;

            base.OnLoad(e);
        }




        protected override int OnRetrieve(params object[] objects)
        {
            

            if (this.dtpBeginTime.Value > this.dtpEndTime.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!");
                return -1;
            }

            string deptCode = "ALL";

           // deptCode = neuDept.SelectedItem.ID;



            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,"ALL");
        }
      

     
    }
}
