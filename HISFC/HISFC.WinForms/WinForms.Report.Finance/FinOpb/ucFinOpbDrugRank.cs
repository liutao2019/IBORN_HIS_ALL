using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinOpb
{
    public partial class ucFinOpbDrugRank : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinOpbDrugRank()
        {
            InitializeComponent();
        }

        #region ����
        FS.HISFC.BizLogic.Manager.Department manger = new FS.HISFC.BizLogic.Manager.Department();
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            ArrayList list = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = null;
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "����";
            list.Add(obj);            
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "2";
            obj.Name = "סԺ";
            list.Add(obj);
            this.neuDept.AddItems(list);
            this.neuDept.Text = "����";
            this.endTime = new DateTime(manger.GetDateTimeFromSysDateTime().Year, manger.GetDateTimeFromSysDateTime().Month, manger.GetDateTimeFromSysDateTime().Day, 0, 0, 0);
            this.beginTime = this.endTime.AddDays(-1);
            base.OnLoad(e);
        }

        protected override int OnRetrieve(params object[] objects)
        {
            int num = 0;
            string type = "";
            string type1 = "";
            string type2 = "";
            this.MainDWLabrary = "Report\\finopb.pbd;Report\\finopb.pbl";
            int j = 0;
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            if (this.neuDept.Tag.ToString() == "1")
            {
                type = "1";
                type1 = "M1";
                type2 = "M2";
            }
            else if (this.neuDept.Tag.ToString() == "2")
            {
                type = "2";
                type1 = "Z1";
                type2 = "Z2";
            }
            if (this.neuTabControl1.SelectedTab.Text != "ҩƷ��Ʒ�ֲ�ѯ" && this.neuTabControl1.SelectedTab.Text != "ҽ����������" )
            {
                if (this.neuNum.Text == "")
                {
                    MessageBox.Show("��¼��ǰ����");
                    return 1;
                }
            }
            num = FS.FrameWork.Function.NConvert.ToInt32(this.neuNum.Text);
            if (this.neuTabControl1.SelectedTab.Text == "ҽ����������")
            {
                this.MainDWDataObject = "d_fin_opb_doct_rank";
                this.MainDWLabrary = "Report\\finopb.pbd;Report\\finopb.pbl";
                type = this.neuDept.Tag.ToString();
                return this.neuDataWindow2.Retrieve(this.beginTime, this.endTime, num, type);
            }
            if (this.neuTabControl1.SelectedTab.Text == "ҽʦҩƷ��������")
            {
                this.MainDWDataObject = "d_fin_opb_doct_rank";
                this.MainDWLabrary = "Report\\finopb.pbd;Report\\finopb.pbl";
                type = this.neuDept.Tag.ToString();
                return this.neuDataWindow6.Retrieve(this.beginTime, this.endTime, num, type);
            }
            if (this.neuTabControl1.SelectedTab.Text == "ҩƷ���Ļ�������")
            {
                //string[] dept = new string[] { };
                List<string> dept = new List<string>();
                this.MainDWDataObject = "d_fin_opb_cost_rank";
                num = FS.FrameWork.Function.NConvert.ToInt32(this.neuNum.Text);
                //ArrayList list = manger.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);
                //if (this.neuDept.Tag.ToString() == "1")
                //{
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        FS.HISFC.Models.Base.Department obj = list[i] as FS.HISFC.Models.Base.Department;
                //        if (obj.Name.Contains("����"))
                //        {
                //            dept.Add(obj.ID);
                //            j++;
                //        }
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        FS.HISFC.Models.Base.Department obj = list[i] as FS.HISFC.Models.Base.Department;
                //        if (obj.Name.Contains("����"))
                //        {
                //            dept.Add(obj.ID);
                //            j++;
                //        }
                //    }
                //}
                //string[] dt = dept.ToArray();
                //return this.neuDataWindow4.Retrieve(this.beginTime, this.endTime, type1, type2, dt,num);
                return this.neuDataWindow4.Retrieve(this.beginTime, this.endTime, type1, type2, num);
            }
            if (this.neuTabControl1.SelectedTab.Text == "ҩƷ�����������")
            {
                this.MainDWDataObject = "d_fin_opb_num_rank";
                
                num = FS.FrameWork.Function.NConvert.ToInt32(this.neuNum.Text);
                return this.neuDataWindow3.Retrieve(this.beginTime, this.endTime, num, type);
            }
            if (this.neuTabControl1.SelectedTab.Text == "ҩƷ����������")
            {
                this.MainDWDataObject = "d_fin_opb_fee_rank";
                return this.neuDataWindow1.Retrieve(this.beginTime, this.endTime, num, type);
            }
            if (this.neuTabControl1.SelectedTab.Text == "ҩƷ��Ʒ�ֲ�ѯ")
            {
                this.MainDWDataObject = "d_fin_opb_singledrug_rank";
                return this.neuDataWindow5.Retrieve(this.beginTime, this.endTime, FS.FrameWork.Management.Connection.Operator.Name);
            }
            return 1;
        }


        private string queryStr = "((spell like '%{0}%')or(spell1 like '%{1}%') or(spell2 like '%{2}%'))";
        private string queryStr1 = "((spell1 like '%{0}%') or(spell2 like '%{1}%'))";
        private string queryStr2 = "spell like '%{0}%'";
        private string queryStr3 = "((spell like '%{0}%')or(spell1 like '%{1}%')";

        private void neuDoct_TextChanged(object sender, EventArgs e)
        {
            //string querycode = "%" + this.neuDoct.Text.Trim() + "%";
            //string filter = "spell like '" + querycode + "'";
            string doct = this.neuDoct.Text.Trim().ToUpper().Replace(@"\", "");
            string pha = this.neuPha.Text.Trim().ToUpper().Replace(@"\", "");
            string company = this.neuCompany.Text.Trim().ToUpper().Replace(@"\", "");
            string tot = "tot desc";
            string num = "qty desc";
            if (this.neuTabControl1.SelectedTab.Text == "ҽʦҩƷ��������")
            {
                if (doct.Equals("")&&pha.Equals("")&&company.Equals(""))
                {
                    
                    this.neuDataWindow6.SetFilter("");
                    this.neuDataWindow6.Filter();
                    this.neuDataWindow6.SetSort(tot);
                    this.neuDataWindow6.Sort();
                    return;
                }

                string str = string.Format(queryStr2, doct);
                this.neuDataWindow6.SetFilter(str);
                this.neuDataWindow6.Filter();                 
            }
            else if (this.neuTabControl1.SelectedTab.Text == "ҩƷ���Ļ�������")
            {
                if (doct.Equals("") && pha.Equals("") && company.Equals(""))
                {
                    this.neuDataWindow4.SetFilter("");
                    this.neuDataWindow4.Filter();
                    this.neuDataWindow4.SetSort(tot);
                    this.neuDataWindow4.Sort();
                    return;
                }

                string str = string.Format(queryStr1, pha, company);
                this.neuDataWindow4.SetFilter(str);
                this.neuDataWindow4.Filter();
            }
            else if (this.neuTabControl1.SelectedTab.Text == "ҩƷ�����������")
            {
                if (doct.Equals("") && pha.Equals("") && company.Equals(""))
                {
                    this.neuDataWindow3.SetFilter("");
                    this.neuDataWindow3.Filter();
                    this.neuDataWindow3.SetSort(num);
                    this.neuDataWindow3.Sort();
                    return;
                }

                string str = string.Format(queryStr, doct,pha,company);
                this.neuDataWindow3.SetFilter(str);
                this.neuDataWindow3.Filter();
            }
            else if (this.neuTabControl1.SelectedTab.Text == "ҩƷ��Ʒ�ֲ�ѯ")
            {
                if (doct.Equals("") && pha.Equals("") && company.Equals(""))
                {
                    this.neuDataWindow5.SetFilter("");
                    this.neuDataWindow5.Filter();
                    this.neuDataWindow5.SetSort(tot);
                    this.neuDataWindow5.Sort();
                    return;
                }

                string str = string.Format(queryStr3, doct, pha);
                this.neuDataWindow5.SetFilter(str);
                this.neuDataWindow5.Filter();
            }
            else
            {
                if (doct.Equals("") && pha.Equals("") && company.Equals(""))
                {
                    this.neuDataWindow1.SetFilter("");
                    this.neuDataWindow1.Filter();
                    this.neuDataWindow1.SetSort(tot);
                    this.neuDataWindow1.Sort();
                    return;
                }

                string str = string.Format(queryStr, doct, pha, company);
                this.neuDataWindow1.SetFilter(str);
                this.neuDataWindow1.Filter();
            }
        }

        private void neuTabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text == "ҩƷ����������" || e.TabPage.Text == "ҩƷ�����������" || e.TabPage.Text == "ҩƷ���Ļ�������")
            {
                this.neuLabel4.Visible = true;
                this.neuLabel5.Visible = true;
                this.neuLabel6.Visible = true;
                this.neuLabel7.Visible = true;
                this.neuLabel8.Visible = true;
                this.neuNum.Visible = true;
                this.neuDoct.Visible = true;
                this.neuPha.Visible = true;
                this.neuCompany.Visible = true;
            }
            if (e.TabPage.Text == "ҽ����������")
            {
                this.neuLabel4.Visible = true;
                this.neuLabel5.Visible = true;
                this.neuLabel6.Visible = false;
                this.neuLabel7.Visible = false;
                this.neuLabel8.Visible = false;
                this.neuNum.Visible = true;
                this.neuDoct.Visible = false;
                this.neuPha.Visible = false;
                this.neuCompany.Visible = false;
            }
            if (e.TabPage.Text == "ҩƷ��Ʒ�ֲ�ѯ")
            {
                this.neuLabel4.Visible = false;
                this.neuLabel5.Visible = false;
                this.neuLabel6.Visible = true;
                this.neuLabel7.Visible = true;
                this.neuLabel8.Visible = false;
                this.neuNum.Visible = false;
                this.neuDoct.Visible = true;
                this.neuPha.Visible = true;
                this.neuCompany.Visible = false;
            }
            if (e.TabPage.Text == "ҽʦҩƷ��������")
            {
                this.neuLabel4.Visible = true;
                this.neuLabel5.Visible = true;
                this.neuLabel6.Visible = true;
                this.neuLabel7.Visible = false;
                this.neuLabel8.Visible = false;
                this.neuNum.Visible = true;
                this.neuDoct.Visible = true;
                this.neuPha.Visible = false;
                this.neuCompany.Visible = false;
            }
        }

        
    }
}
