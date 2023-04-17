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
    public partial class ucphaoutputlist : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucphaoutputlist()
        {
            InitializeComponent();
        }
        private string tra_name = string.Empty;
        private string tra_code = string.Empty;

        private string peo_name_code = string.Empty;
        private string peo_name_name = string.Empty;

        private string rec_no = string.Empty;

        private string dr_type_code = string.Empty;
        private string dr_type_name = string.Empty;

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            // ҩƷ����
            //if (string.IsNullOrEmpty(neuTextBox1.Text))
            //{
            //    tra_name = "ALL";
            //}
            //else
            //{
            //    tra_name = neuTextBox1.Text;
            //}

            ////��ҩ��
            //if(string.IsNullOrEmpty(neuTextBox2.Text))
            //{
            //    peo_name = "ALL";
            //}
            //else
            //{
            //    peo_name = neuTextBox2.Text;
            //}

            //����
            if (string.IsNullOrEmpty(neuTextBox3.Text))
            {
                rec_no = "ALL";
            }
            else
            {
                rec_no = neuTextBox3.Text;

            }

            //ҩƷ����
            //if (string.IsNullOrEmpty(neuComboBox1.Text))
            //{
            //    dr_type = "ALL";
            //}
            //else
            //{
            //    dr_type = neuComboBox1.Text;
            //}

            objects = new object[] { this.beginTime, this.endTime, tra_code, peo_name_code, rec_no, dr_type_code };

            base.OnRetrieve(objects);
            this.dwMain.CalculateGroups();
            return 1;
        }

        //private void neuCheckBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (neuCheckBox1.Checked)
        //    {
        //        neuTextBox1.Enabled = true;
        //    }
        //    else
        //    {
        //        neuTextBox1.Enabled = false;
        //    }
        //}

        private void neuCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (neuCheckBox3.Checked)
            {
                neuTextBox3.Enabled = true;
            }
            else
            {
                neuTextBox3.Enabled = false;
                neuTextBox3.Text = "";
                rec_no = "ALL";
            }
        }

        private void neuCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (neuCheckBox2.Checked)
            {
                neuComboBox2.Enabled = true;
            }
            else
            {
                neuComboBox2.Enabled = false;
                neuComboBox2.SelectedIndex = 0;
            }
        }

        private void neuCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (neuCheckBox4.Checked)
            {
                neuComboBox1.Enabled = true;
            }
            else
            {
                neuComboBox1.Enabled = false;
                neuComboBox1.SelectedIndex = 0;
            }
        }
        protected override void OnLoad()
        {
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            //��ҩ��
            System.Collections.ArrayList alPersonconstantList = manager.QueryEmployeeAll();
            FS.HISFC.Models.Base.Employee allpeo_name = new FS.HISFC.Models.Base.Employee();
            allpeo_name.ID = "ALL";
            allpeo_name.Name = "ȫ��";
            allpeo_name.SpellCode = "QB";

            neuComboBox2.Items.Add(allpeo_name);
            

            foreach (FS.HISFC.Models.Base.Employee var in alPersonconstantList)
            {
                neuComboBox2.Items.Add(var);
            }

            alPersonconstantList.Insert(0, allpeo_name);
            this.neuComboBox2.alItems.AddRange(alPersonconstantList);

            if (neuComboBox2.Items.Count > 0)
            {
                neuComboBox2.SelectedIndex = 0;
                peo_name_code = ((FS.HISFC.Models.Base.Employee)neuComboBox2.Items[0]).ID;
                peo_name_name = ((FS.HISFC.Models.Base.Employee)neuComboBox2.Items[0]).Name;
            }


            ArrayList list = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
            obj.ID = "ALL";
            obj.Name = "ȫ��";
            obj.SpellCode = "QB";
            obj.WBCode = "WU";
            list.Add(obj);
            this.neuComboBox1.Items.Add(obj);
            foreach (FS.HISFC.Models.Base.Const con in list)
            {
                neuComboBox1.Items.Add(con);
            }

            this.neuComboBox1.alItems.Add(obj);
            this.neuComboBox1.alItems.AddRange(list);

            if (neuComboBox1.Items.Count > 0)
            {
                neuComboBox1.SelectedIndex = 0;
                dr_type_code = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[0]).ID;
                dr_type_name = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[0]).Name;
            }

            #region  ҩƷ���Ƹ�ֵ
            //list = new ArrayList();
            FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> ListDrug = phaManager.QueryItemList();
            //Object b = new Object();
            for (int i = 0; i < ListDrug.Count; i++)
            {
                //b = ListDrug[i];
                //AllDrugList.Insert(0, b);
                neuComboBox3.Items.Add(ListDrug[i]);
            }
            
            FS.HISFC.Models.Pharmacy.Item obj2 = new FS.HISFC.Models.Pharmacy.Item();
            obj2.ID = "ALL";
            obj2.Name = "ȫ��";

            neuComboBox3.Items.Insert(0, obj2);
            neuComboBox3.alItems.Add(obj2);
            neuComboBox3.alItems.AddRange(ListDrug);

            if (neuComboBox3.Items.Count > 0)
            {
                neuComboBox3.SelectedIndex = 0;
                tra_name = ((FS.HISFC.Models.Pharmacy.Item)neuComboBox3.Items[0]).Name;
                tra_code = ((FS.HISFC.Models.Pharmacy.Item)neuComboBox3.Items[0]).ID;
            }
          
            
            #endregion 
        }


        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox1.SelectedIndex > -1)
            {
                dr_type_code = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                dr_type_name = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;

            }
        }





        private void neuComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox2.SelectedIndex > -1)
            {
                peo_name_code = ((FS.HISFC.Models.Base.Employee)neuComboBox2.Items[this.neuComboBox2.SelectedIndex]).ID;
                peo_name_name = ((FS.HISFC.Models.Base.Employee)neuComboBox2.Items[this.neuComboBox2.SelectedIndex]).Name;

            }
        }
        


        private void neuComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox3.SelectedIndex >= 0)
            {
                tra_name = ((FS.HISFC.Models.Pharmacy.Item)neuComboBox3.Items[neuComboBox3.SelectedIndex]).Name;
                tra_code = ((FS.HISFC.Models.Pharmacy.Item)neuComboBox3.Items[neuComboBox3.SelectedIndex]).ID;
            }
        }

        private void neuCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (neuCheckBox1.Checked)
            {
                neuComboBox3.Enabled = true;
            }
            else
            {
                neuComboBox3.Enabled = false;
                neuComboBox3.SelectedIndex = 0;
            }
        }
    }
}


        