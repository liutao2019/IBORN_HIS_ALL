using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Finance.FinOpb
{
    public partial class ucFinOpbStatClinic : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinOpbStatClinic()
        {
            InitializeComponent();
        }
        int currentNo = 0;

        protected override void OnLoad(EventArgs e)
        {
            ArrayList list = new ArrayList();
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            list = con.GetAllList("PACTUNIT");
            this.neuPact.AddItems(list);
            base.OnLoad(e);
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.neuTabControl1.SelectedTab.Text == "δȡҩ����")
            {
                return this.dwNoDrug.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            }
            if (this.neuTabControl1.SelectedTab.Text == "δȡҩ�˷Ѵ���")
            {
                return this.dwNoFeeNoDrug.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            }
            if (this.neuTabControl1.SelectedTab.Text == "��ȡҩ����")
            {
                return this.dwFeeDrug.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            }
            if (this.neuTabControl1.SelectedTab.Text == "δ�˶Լ�鵥")
            {
                return dwDetail.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            }
            if (this.neuTabControl1.SelectedTab.Text == "�˷Ѽ�鵥")
            {
                return dwOutFee.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            }
            if (this.neuTabControl1.SelectedTab.Text == "�췽�˷�")
            {
                return dwRedOutFee.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            }
            return 1;
        }

        private void dwNoDrug_DoubleClick(object sender, EventArgs e)
        {
            string no = this.dwNoDrug.GetItemString(currentNo, "�շѵ���");
            string Inpatient = this.dwNoDrug.GetItemString(currentNo, "��������");
            string dept = this.dwNoDrug.GetItemString(currentNo, "��������");
            string doct = this.dwNoDrug.GetItemString(currentNo, "����ҽʦ");
            string date = this.dwNoDrug.GetItemString(currentNo, "�շ�ʱ��");
            string operfee = this.dwNoDrug.GetItemString(currentNo, "�շ���Ա");
            string ys = this.dwNoDrug.GetItemString(currentNo, "Ӧ�ս��");
            string ss = this.dwNoDrug.GetItemString(currentNo, "ʵ�ս��");            

            ucFinOpbStatClinicDetail detail = new ucFinOpbStatClinicDetail();
            detail.Init(no, Inpatient, dept, doct, operfee, date, ys, ss,"����δȡҩ������ϸ");
            detail.ShowDialog();
        }

        private void dwNoFeeNoDrug_DoubleClick(object sender, EventArgs e)
        {
            string no = this.dwNoFeeNoDrug.GetItemString(currentNo, "�շѵ���");
            string Inpatient = this.dwNoFeeNoDrug.GetItemString(currentNo, "��������");
            string dept = this.dwNoFeeNoDrug.GetItemString(currentNo, "��������");
            string doct = this.dwNoFeeNoDrug.GetItemString(currentNo, "����ҽʦ");
            string date = this.dwNoFeeNoDrug.GetItemString(currentNo, "�շ�ʱ��");
            string operfee = this.dwNoFeeNoDrug.GetItemString(currentNo, "�˷���Ա");
            string ys = this.dwNoFeeNoDrug.GetItemString(currentNo, "Ӧ�˽��");
            string ss = this.dwNoFeeNoDrug.GetItemString(currentNo, "ʵ�˽��");

            ucFinOpbStatClinicDetail detail = new ucFinOpbStatClinicDetail();
            detail.Init(no, Inpatient, dept, doct, operfee, date, ys, ss, "����δȡҩ�˷Ѵ���ͳ��");
            detail.ShowDialog();
        }
        private void dwRedOutFee_DoubleClick(object sender, EventArgs e)
        {
            string no = this.dwRedOutFee.GetItemString(currentNo, "�շѵ���");
            string Inpatient = this.dwRedOutFee.GetItemString(currentNo, "��������");
            string dept = this.dwRedOutFee.GetItemString(currentNo, "��������");
            string doct = this.dwRedOutFee.GetItemString(currentNo, "����ҽʦ");
            string date = this.dwRedOutFee.GetItemString(currentNo, "�շ�ʱ��");
            string operfee = this.dwRedOutFee.GetItemString(currentNo, "�˷���Ա");
            string fee = this.dwRedOutFee.GetItemString(currentNo, "ʵ�˽��");
            string ss = "";

            ucFinOpbStatClinicDetail detail = new ucFinOpbStatClinicDetail();
            detail.Init(no, Inpatient, dept, doct, operfee, date, fee, ss, "���ﴦ����ҩ������ϸ");
            detail.ShowDialog();
        }

        private void dwNoDrug_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            currentNo = e.RowNumber;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedTab.Text == "δȡҩ����")
            {
                dwNoDrug.Print();
            }
            if (this.neuTabControl1.SelectedTab.Text == "δȡҩ�˷Ѵ���")
            {
                dwNoFeeNoDrug.Print();
            }
            if (this.neuTabControl1.SelectedTab.Text == "��ȡҩ����")
            {
                dwFeeDrug.Print();
            }
            if (this.neuTabControl1.SelectedTab.Text == "δ�˶Լ�鵥")
            {
                dwDetail.Print();
            }
            if (this.neuTabControl1.SelectedTab.Text == "�˷Ѽ�鵥")
            {
                dwOutFee.Print();
            }
            if (this.neuTabControl1.SelectedTab.Text == "�췽�˷�")
            {
                dwRedOutFee.Print();
            }
            return 1;
        }

        string a = "(����ƴ���� like '%{0}%') or (��Ʊ��ˮ�� like '%{1}%') or (����ƴ���� like '%{2}%') or (ҽʦƴ���� like '%{3}%') or (�շ�Աƴ���� like '%{4}%') or(��ͬ��λ  like '%{5}%')";
        string b = "(����ƴ���� like '%{0}%') or (��Ʊ��ˮ�� like '%{1}%') or (����ƴ���� like '%{2}%') or (ҽʦƴ���� like '%{3}%') or (�շ�Աƴ���� like '%{4}%') or(��Ŀƴ���� like '%{5}%') or (��ͬ��λ like '%{6}%')";

        private void neuInpatient_TextChanged(object sender, EventArgs e)
        {
             string patient = this.neuInpatient.Text.Trim().ToUpper().Replace(@"\", "");
             string invoic = this.neuInvoic.Text.Trim().ToUpper().Replace(@"\", "");
             string dept = this.neuDept.Text.Trim().ToUpper().Replace(@"\", "");
             string doct = this.neuDoct.Text.Trim().ToUpper().Replace(@"\", "");
             string operfee = this.neuFeeOper.Text.Trim().ToUpper().Replace(@"\", "");
            string item=this.neuItem.Text.Trim().ToUpper().Replace(@"\", "");
            string pact = this.neuPact.Tag.ToString();

            if (this.neuTabControl1.SelectedTab.Text == "δȡҩ����")
            {
                if (patient.Equals("") && invoic.Equals("") && dept.Equals("") && doct.Equals("") && operfee.Equals("") && pact.Equals(""))
                {

                    this.dwNoDrug.SetFilter("");
                    this.dwNoDrug.Filter();
                    return;
                }

                string str = string.Format(a, patient, invoic, dept, doct, operfee, pact);
                this.dwNoDrug.SetFilter(str);
                this.dwNoDrug.Filter();       
            }
            if (this.neuTabControl1.SelectedTab.Text == "δȡҩ�˷Ѵ���")
            {
                if (patient.Equals("") && invoic.Equals("") && dept.Equals("") && doct.Equals("") && operfee.Equals("") && pact.Equals(""))
                {

                    this.dwNoFeeNoDrug.SetFilter("");
                    this.dwNoFeeNoDrug.Filter();
                    return;
                }

                string str = string.Format(a, patient, invoic, dept, doct, operfee, pact);
                this.dwNoFeeNoDrug.SetFilter(str);
                this.dwNoFeeNoDrug.Filter();  
            }
            if (this.neuTabControl1.SelectedTab.Text == "��ȡҩ����")
            {
                if (patient.Equals("") && invoic.Equals("") && dept.Equals("") && doct.Equals("") && operfee.Equals("") && pact.Equals(""))
                {

                    this.dwFeeDrug.SetFilter("");
                    this.dwFeeDrug.Filter();
                    return;
                }

                string str = string.Format(a, patient, invoic, dept, doct, operfee, pact);
                this.dwFeeDrug.SetFilter(str);
                this.dwFeeDrug.Filter();  
            }
            if (this.neuTabControl1.SelectedTab.Text == "δ�˶Լ�鵥")
            {
                if (patient.Equals("") && invoic.Equals("") && dept.Equals("") && doct.Equals("") && operfee.Equals("") && item.Equals("")&&pact.Equals(""))
                {

                    this.dwDetail.SetFilter("");
                    this.dwDetail.Filter();
                    return;
                }

                string str = string.Format(b, patient, invoic, dept, doct, operfee, item, pact);
                this.dwDetail.SetFilter(str);
                this.dwDetail.Filter();        
            }
            if (this.neuTabControl1.SelectedTab.Text == "�˷Ѽ�鵥")
            {
                if (patient.Equals("") && invoic.Equals("") && dept.Equals("") && doct.Equals("") && operfee.Equals("") && item.Equals("") && pact.Equals(""))
                {

                    this.dwOutFee.SetFilter("");
                    this.dwOutFee.Filter();
                    return;
                }

                string str = string.Format(b, patient, invoic, dept, doct, operfee, item, pact);
                this.dwOutFee.SetFilter(str);
                this.dwOutFee.Filter();       
            }
            if (this.neuTabControl1.SelectedTab.Text == "�췽�˷�")
            {
                if (patient.Equals("") && invoic.Equals("") && dept.Equals("") && doct.Equals("") && operfee.Equals("") && pact.Equals(""))
                {

                    this.dwRedOutFee.SetFilter("");
                    this.dwRedOutFee.Filter();
                    return;
                }

                string str = string.Format(a, patient, invoic, dept, doct, operfee, pact);
                this.dwRedOutFee.SetFilter(str);
                this.dwRedOutFee.Filter();
            }
        }
    }
}
