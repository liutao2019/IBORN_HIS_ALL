using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.MET.MetCas
{
    public partial class ucMetCasHos :FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetCasHos()
        {
            InitializeComponent();
        }
        
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        private string bblx = string.Empty;
        string name0, name1, name2, name3, name4, name5, name6, name7, name8,
             name9, name10, name11, name12, name13,name14,name15,name16,name17,name18,name19;
 

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }      
   
            bblx = "ҽ Ժ �� �� ���£� �� �� ��һ��";
            TimeSpan times=base.endTime - base.beginTime;
            int days=times.Days;
            if (days > 365)
            {
                bblx = "ҽԺ�������꣩����";
            }
            else if (days > 182)
            {
                bblx = "ҽԺ���������꣩����";
            }
            else if (days > 90)
            {
                bblx = "ҽԺ��������������";
            }
            int RetrieveRow = base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            //int RetrieveRow =this.dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            this.dwList.Modify("t_bblx.text = '" + bblx + "'");
            this.dwList.Modify("t_zbr.text = '" + oper.Name + "'");
            this.dwList.Modify("t_date.text = '" + this.dtpBeginTime.Value.ToString("yyyy��MM��dd��") + " -- " + this.dtpEndTime.Value.ToString("yyyy��MM��dd��") + "'");

             name1 = dwMain.GetItemString(1, "compute_21");
             name2 = dwMain.GetItemString(1, "compute_20");
             name3 = dwMain.GetItemString(1, "compute_19");
             name4 = dwMain.GetItemString(1, "compute_4");
             name5 = dwMain.GetItemString(1, "compute_5");
             name6 = dwMain.GetItemString(1, "compute_6");
             name7 = dwMain.GetItemString(1, "compute_7");
             name8 = dwMain.GetItemString(1, "compute_8");
             name9 = dwMain.GetItemString(1, "compute_9");
             name10 = dwMain.GetItemString(1, "compute_10");
             name11 = dwMain.GetItemString(1, "compute_11");
             name12 = dwMain.GetItemString(1, "compute_12");     
             name13 = dwMain.GetItemString(1, "compute_18");
             name14 = dwMain.GetItemString(1, "compute_22");
             name15 = dwMain.GetItemString(1, "compute_13");
             name16 = dwMain.GetItemString(1, "compute_14");
             name17 = dwMain.GetItemString(1, "compute_15");
             name18 = dwMain.GetItemString(1, "compute_16");
             name19 = dwMain.GetItemString(1, "compute_17");
            dwList.Reset();
            dwList.InsertRow(0);
            dwList.SetItemString(1, "name_0", "   �ϼ�(����Ժ)");
            /*
            if (name1 != "" || name1==null)
                 dwList.SetItemString(1, "name_1", name1);  
            else
                 dwList.SetItemString(1, "name_1", "");  
            if (name2 != "" || name2== null)
                 dwList.SetItemString(1, "name_2", name2);
            else
                 dwList.SetItemString(1, "name_2", "");  
            if (name3 != "" || name3 == null)
                 dwList.SetItemString(1, "name_3", name3);
             else
                 dwList.SetItemString(1, "name_3", "");  
            if (name4 != null)
                 dwList.SetItemString(1, "name_4", name4);
             else
                 dwList.SetItemString(1, "name_4", "");
             if (name5 != null)
                 dwList.SetItemString(1, "name_5", name5);
             else
                 dwList.SetItemString(1, "name_5", "");
             if (name6 != null)
                 dwList.SetItemString(1, "name_6", name6);
             else
                 dwList.SetItemString(1, "name_6", "");
             if (name7 != null)
                 dwList.SetItemString(1, "name_7", name7);
             else
                 dwList.SetItemString(1, "name_7", "");
             if (name8 != null)
                 dwList.SetItemString(1, "name_8", name8);
             else
                 dwList.SetItemString(1, "name_8", "");
             if (name9 != null)
                 dwList.SetItemString(1, "name_9", name9);
             else
                 dwList.SetItemString(1, "name_9", "");
             if (name10 != null)
                 dwList.SetItemString(1, "name_10", name10);
             else
                 dwList.SetItemString(1, "name_10", "");
             if (name11 != null)
                 dwList.SetItemString(1, "name_11", name11);
             else
                 dwList.SetItemString(1, "name_11", "");
             if (name12 != null)
                 dwList.SetItemString(1, "name_12", name12);
             else
                 dwList.SetItemString(1, "name_12", "");
             if (name13 != null)
                 dwList.SetItemString(1, "name_13", name13);
             else
                 dwList.SetItemString(1, "name_13", ""); 
             if (name14 != null)
                 dwList.SetItemString(1, "name_14", name14);
             else
                 dwList.SetItemString(1, "name_14", "");
             if (name15 != null)
                 dwList.SetItemString(1, "name_15", name15);
             else
                 dwList.SetItemString(1, "name_15", "");
             if (name16 != null)
                 dwList.SetItemString(1, "name_16", name16);
             else
                 dwList.SetItemString(1, "name_16", ""); 
             if (name17 != null)
                 dwList.SetItemString(1, "name_17", name17);
             else
                 dwList.SetItemString(1, "name_17", "");
             if (name18 != null)
                 dwList.SetItemString(1, "name_18", name18);
             else
                 dwList.SetItemString(1, "name_18", "");
             if (name19 != null)
                 dwList.SetItemString(1, "name_19", name19);
             else
                 dwList.SetItemString(1, "name_19", "");
             */
            //�ڶ���
             dwList.InsertRow(0);

             name1 = dwMain.GetItemString(1, "compute_41");
             name2 = dwMain.GetItemString(1, "compute_42");
             name3 = dwMain.GetItemString(1, "compute_24");
             name4 = dwMain.GetItemString(1, "compute_25");
             name5 = dwMain.GetItemString(1, "compute_26");
             name6 = dwMain.GetItemString(1, "compute_27");
             name7 = dwMain.GetItemString(1, "compute_28");
             name8 = dwMain.GetItemString(1, "compute_29");
             name9 = dwMain.GetItemString(1, "compute_30");
             name10 = dwMain.GetItemString(1, "compute_31");
             name11 = dwMain.GetItemString(1, "compute_32");
             name12 = dwMain.GetItemString(1, "compute_33");
             name13 = dwMain.GetItemString(1, "compute_34");
             name14 = dwMain.GetItemString(1, "compute_35");
             name15 = dwMain.GetItemString(1, "compute_36");
             name16 = dwMain.GetItemString(1, "compute_37");
             name17 = dwMain.GetItemString(1, "compute_38");
             name18 = dwMain.GetItemString(1, "compute_39");
             name19 = dwMain.GetItemString(1, "compute_40");
            
             dwList.SetItemString(2, "name_0", "     ��  ��");
             if (name1 != "" || name1 == null)
                 dwList.SetItemString(2, "name_1", name1);
             else
                 dwList.SetItemString(2, "name_1", "");
             if (name2 != "" || name2 == null)
                 dwList.SetItemString(2, "name_2", name2);
             else
                 dwList.SetItemString(2, "name_2", "");
             if (name3 != "" || name3 == null)
                 dwList.SetItemString(2, "name_3", name3);
             else
                 dwList.SetItemString(2, "name_3", "");
             if (name4 != null)
                 dwList.SetItemString(2, "name_4", name4);
             else
                 dwList.SetItemString(2, "name_4", "");
             if (name5 != null)
                 dwList.SetItemString(2, "name_5", name5);
             else
                 dwList.SetItemString(2, "name_5", "");
             if (name6 != null)
                 dwList.SetItemString(2, "name_6", name6);
             else
                 dwList.SetItemString(2, "name_6", "");
             if (name7 != null)
                 dwList.SetItemString(2, "name_7", name7);
             else
                 dwList.SetItemString(2, "name_7", "");
             if (name8 != null)
                 dwList.SetItemString(2, "name_8", name8);
             else
                 dwList.SetItemString(2, "name_8", "");
             if (name9 != null)
                 dwList.SetItemString(2, "name_9", name9);
             else
                 dwList.SetItemString(2, "name_9", "");
             if (name10 != null)
                 dwList.SetItemString(2, "name_10", name10);
             else
                 dwList.SetItemString(2, "name_10", "");
             if (name11 != null)
                 dwList.SetItemString(2, "name_11", name11);
             else
                 dwList.SetItemString(2, "name_11", "");
             if (name12 != null)
                 dwList.SetItemString(2, "name_12", name12);
             else
                 dwList.SetItemString(2, "name_12", "");
             if (name13 != null)
                 dwList.SetItemString(2, "name_13", name13);
             else
                 dwList.SetItemString(2, "name_13", "");
             if (name14 != null)
                 dwList.SetItemString(2, "name_14", name14);
             else
                 dwList.SetItemString(2, "name_14", "");
             if (name15 != null)
                 dwList.SetItemString(2, "name_15", name15);
             else
                 dwList.SetItemString(2, "name_15", "");
             if (name16 != null)
                 dwList.SetItemString(2, "name_16", name16);
             else
                 dwList.SetItemString(2, "name_16", "");
             if (name17 != null)
                 dwList.SetItemString(2, "name_17", name17);
             else
                 dwList.SetItemString(2, "name_17", "");
             if (name18 != null)
                 dwList.SetItemString(2, "name_18", name18);
             else
                 dwList.SetItemString(2, "name_18", "");
             if (name19 != null)
                 dwList.SetItemString(2, "name_19", name19);
             else
                 dwList.SetItemString(2, "name_19", "");
           
             int curuntRow = 1;
             for (int i = 3; i <=dwMain.RowCount+2; i++)
             {
                 name0 = dwMain.GetItemString(curuntRow, "compute_23");
                 name1 = dwMain.GetItemString(curuntRow, "compute_3");
                 name2 = dwMain.GetItemString(curuntRow, "compute_2");
                 name3 = dwMain.GetItemString(curuntRow, "�����˴���");
                 name4 = dwMain.GetItemString(curuntRow, "�����˴���");
                 name5 = dwMain.GetItemString(curuntRow, "���������˴�");
                 name6 = dwMain.GetItemString(curuntRow, "�۲�����");
                 name7 = dwMain.GetItemString(curuntRow, "�۲�����");
                 name8 = dwMain.GetItemString(curuntRow, "��콡�����");
                 name9 = dwMain.GetItemString(curuntRow, "���µ�ʵ������");
                 name10 = dwMain.GetItemString(curuntRow, "��Ժ����");
                 name11 = dwMain.GetItemString(curuntRow, "ת������");
                 name12 = dwMain.GetItemString(curuntRow, "ת������");
                 name13 = dwMain.GetItemString(curuntRow, "��Ժ����");
                 name14 = dwMain.GetItemString(curuntRow, "compute_1");
                 name15 = dwMain.GetItemString(curuntRow, "��������");
                 name16 = dwMain.GetItemString(curuntRow, "��ת����");
                 name17 = dwMain.GetItemString(curuntRow, "δ������");
                 name18 = dwMain.GetItemString(curuntRow, "��������");
                 name19 = dwMain.GetItemString(curuntRow, "�µ�ʵ������");
                 dwList.InsertRow(0);

                 if (name0 != null)
                     dwList.SetItemString(i, "name_0", name0);
                 else
                     dwList.SetItemString(i, "name_0", "");
                 if (name1 != "" || name1 == null)
                     dwList.SetItemString(i, "name_1", name1);
                 else
                     dwList.SetItemString(i, "name_1", "");
                 if (name2 != "" || name2 == null)
                     dwList.SetItemString(i, "name_2", name2);
                 else
                     dwList.SetItemString(i, "name_2", "");
                 if (name3 != "" || name3 == null)
                     dwList.SetItemString(i, "name_3", name3);
                 else
                     dwList.SetItemString(i, "name_3", "");
                 if (name4 != null)
                     dwList.SetItemString(i, "name_4", name4);
                 else
                     dwList.SetItemString(i, "name_4", "");
                 if (name5 != null)
                     dwList.SetItemString(i, "name_5", name5);
                 else
                     dwList.SetItemString(i, "name_5", "");
                 if (name6 != null)
                     dwList.SetItemString(i, "name_6", name6);
                 else
                     dwList.SetItemString(i, "name_6", "");
                 if (name7 != null)
                     dwList.SetItemString(i, "name_7", name7);
                 else
                     dwList.SetItemString(i, "name_7", "");
                 if (name8 != null)
                     dwList.SetItemString(i, "name_8", name8);
                 else
                     dwList.SetItemString(i, "name_8", "");
                 if (name9 != null)
                     dwList.SetItemString(i, "name_9", name9);
                 else
                     dwList.SetItemString(i, "name_9", "");
                 if (name10 != null)
                     dwList.SetItemString(i, "name_10", name10);
                 else
                     dwList.SetItemString(i, "name_10", "");
                 if (name11 != null)
                     dwList.SetItemString(i, "name_11", name11);
                 else
                     dwList.SetItemString(i, "name_11", "");
                 if (name12 != null)
                     dwList.SetItemString(i, "name_12", name12);
                 else
                     dwList.SetItemString(i, "name_12", "");
                 if (name13 != null)
                     dwList.SetItemString(i, "name_13", name13);
                 else
                     dwList.SetItemString(i, "name_13", "");
                 if (name14 != null)
                     dwList.SetItemString(i, "name_14", name14);
                 else
                     dwList.SetItemString(i, "name_14", "");
                 if (name15 != null)
                     dwList.SetItemString(i, "name_15", name15);
                 else
                     dwList.SetItemString(i, "name_15", "");
                 if (name16 != null)
                     dwList.SetItemString(i, "name_16", name16);
                 else
                     dwList.SetItemString(i, "name_16", "");
                 if (name17 != null)
                     dwList.SetItemString(i, "name_17", name17);
                 else
                     dwList.SetItemString(i, "name_17", "");
                 if (name18 != null)
                     dwList.SetItemString(i, "name_18", name18);
                 else
                     dwList.SetItemString(i, "name_18", "");
                 if (name19 != null)
                     dwList.SetItemString(i, "name_19", name19);
                 else
                     dwList.SetItemString(i, "name_19", "");
                
                 curuntRow += 1;
             }

                 return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (bblx.Equals("ҽ Ժ �� �� ���£� �� �� ��һ��"))
            {
                this.dwList.Print(true,true);
                return 1;
                //return base.OnPrint(sender, neuObject);
            }
            else
            {
                MessageBox.Show("ֻ�ܴ�ӡ�±���");
                return -1;
            }
            
        }



        private void ucMetCasHos_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

            DateTime nowTime = inpatientManager.GetDateTimeFromSysDateTime();
            DateTime lastTime = new DateTime(nowTime.Year, nowTime.Month, 26, 00, 00, 00);
            this.dtpBeginTime.Value = lastTime.AddMonths(-1);
            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, 25, 00, 00, 00);
        }

    }
}
