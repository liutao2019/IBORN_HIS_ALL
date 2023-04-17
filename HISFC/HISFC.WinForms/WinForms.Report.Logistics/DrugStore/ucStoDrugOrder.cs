using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Logistics.DrugStore
{
    public partial class ucStoDrugOrder : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucStoDrugOrder()
        {
            InitializeComponent();
        }

        ArrayList alDrugQuality = new ArrayList();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        DeptZone deptZone1 = DeptZone.ALL;

        #region ö��
        public enum DeptZone
        {
            MZ = 0,
            ZY = 1,
            ALL = 2,
        }
        #endregion

        #region ����
        [Category("��������"),Description("��ѯ��Χ��ALL��ȫԺ��MZ�����ZY��סԺ") ]
        public DeptZone DeptZone1
        {
            get 
            {
                return deptZone1;
            }
            set
            {
                deptZone1 = value;
            }
        }
        #endregion 

        #region ��ʼ��
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //ҩƷ����
            
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "ȫ��";
            alDrugQuality.Add(obj);

            ArrayList list = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            alDrugQuality.AddRange(list);

            cmbDrugq.AddItems(alDrugQuality);
            cmbDrugq.SelectedIndex = 0;

            //����
            cmbDept.ClearItems();

            if (deptZone1 == DeptZone.ALL)
            {
                cmbDept.Items.Add("ȫԺ");
                cmbDept.Items.Add("����");
                cmbDept.Items.Add("סԺ");

            }
            if (deptZone1 == DeptZone.MZ)
            {
                cmbDept.Items.Add("����");
                cmbDept.Enabled = false;
            }
            if (deptZone1 == DeptZone.ZY)
            {
                cmbDept.Items.Add("סԺ");
                cmbDept.Enabled = false;
            }

            cmbDept.SelectedIndex = 0;

            //��ѯ���
            cmbType.Items.Add("������ѯ");
            cmbType.Items.Add("��������ѯ");
            cmbType.SelectedIndex = 0;
        }
        #endregion 


        #region ��ѯ
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            string strDrugq;
            string strDept = "ȫԺ";
            List<string> alType = new List<string>();

            //ѡ���ѯ���
            if (cmbType.Items[cmbType.SelectedIndex].ToString() == "������ѯ")
            {
                this.mainDWDataObject = "d_sto_drugorder";
                dwMain.DataWindowObject = "d_sto_drugorder";
            }
            if (cmbType.Items[cmbType.SelectedIndex].ToString() == "��������ѯ")
            {
                this.mainDWDataObject = "d_sto_drugnumorder";
                dwMain.DataWindowObject = "d_sto_drugnumorder";
            }

            //ȡ��ѯ���ͣ�ȫԺ������orסԺ
            if (!string.IsNullOrEmpty(cmbDept.Items[cmbDept.SelectedIndex].ToString()))
            {
                strDept = cmbDept.Items[cmbDept.SelectedIndex].ToString();
            }


            if (strDept == "ȫԺ")
            {
                //alType[0] = "M1";
                //alType[1] = "M2";
                //alType[2] = "Z1";
                //alType[3] = "Z2";
                alType.Add("M1");
                alType.Add("M2");
                alType.Add("Z1");
                alType.Add("Z2");


            }
            if (strDept == "����")
            {
                alType.Add("M1");
                alType.Add("M2");
            }
            if (strDept == "סԺ")
            {
                alType.Add("Z1");
                alType.Add("Z2");

            }

            //ȡҩƷ����
            strDrugq = cmbDrugq.SelectedItem.ID;

            // ���סԺor ȫԺ
            string[] strValue = new string[alType.Count];
            for (int i = 0; i < alType.Count; i++)
            {
                strValue[i] = alType[i];
            }



            return base.OnRetrieve(this.beginTime, this.endTime, strDrugq, strValue);
        }

        #endregion
        


        #region ����

        private void ntbDrugOrder_SelectedChanged(object sender,EventArgs e)
        {
           int drugOrder ;

           try
           {
               drugOrder =  int.Parse(ntbDrugOrder.Text);
           }
           catch (Exception e1)
           {
               //if ( drugOrder == 0)
               //{
               //    dwMain.SetFilter("" );
               //    dwMain.Filter();
               //    dwMain.SetSort("����");
               //    dwMain.Sort();
               //    return;
               //}
               //else
               //{
                   MessageBox.Show("����������");
                   return;
               //}
           }

           dwMain.SetFilter("���� <=" + drugOrder);
           dwMain.Filter();

           dwMain.SetSort("����");
           dwMain.Sort();
        }

        #endregion 

       


    }
}