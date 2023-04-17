using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucFinIpbInvoice : Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIpbInvoice()
        {
            InitializeComponent();
        }


        protected override void OnLoad()
        {

            //FS.HISFC.Models.Base.Employee allPerson = new FS.HISFC.Models.Base.Employee();
            //System.Collections.ArrayList alPersonconstantList = null;
            ////�������
            //FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            //if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager)
            //{
            //    alPersonconstantList = manager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            //    allPerson.ID = "ALL";
            //    allPerson.Name = "ȫ��";
            //    allPerson.SpellCode = "QB";
            //    //cboPersonCode.Items.Insert(0, allPerson);
            //    alPersonconstantList.Insert(0, allPerson);
            //}
            //else
            //{
            //    alPersonconstantList = new ArrayList();
            //    allPerson = new FS.HISFC.Models.Base.Employee();
            //    allPerson.ID = FS.FrameWork.Management.Connection.Operator.ID;
            //    allPerson.Name = FS.FrameWork.Management.Connection.Operator.Name;
            //    alPersonconstantList.Insert(0, allPerson);
            //}
            //this.cboPersonCode.AddItems(alPersonconstantList);
            //cboPersonCode.SelectedIndex = 0;


            
            System.Collections.ArrayList alCancelFlagConstantList  = new ArrayList();

            //ȫ��
            FS.HISFC.Models.Base.Const allCancelFlag0 = new FS.HISFC.Models.Base.Const();
            allCancelFlag0.ID = "ALL";
            allCancelFlag0.Name = "ȫ��";
            allCancelFlag0.SpellCode = "ALL";
            alCancelFlagConstantList.Add(allCancelFlag0);
            //��Ч
            FS.HISFC.Models.Base.Const allCancelFlag1 = new FS.HISFC.Models.Base.Const();
            allCancelFlag1.ID = "1";
            allCancelFlag1.Name = "��Ч";
            allCancelFlag1.SpellCode = "YX";
            alCancelFlagConstantList.Add(allCancelFlag1);
            //ȫ����Ʊ(�˷�,�ش�,ע��)
            FS.HISFC.Models.Base.Const allCancelFlag2 = new FS.HISFC.Models.Base.Const();
            allCancelFlag2.ID = "0";
            allCancelFlag2.Name = "�˷�";
            allCancelFlag2.SpellCode = "TF";
            alCancelFlagConstantList.Add(allCancelFlag2);

            this.cboCancelFlag.AddItems(alCancelFlagConstantList);
            cboCancelFlag.SelectedIndex = 0;
        }

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList emplList = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);

            TreeNode parentTreeNode = new TreeNode("���в���Ա");
            tvLeft.Nodes.Add(parentTreeNode);
            if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager)
            {
                foreach (FS.HISFC.Models.Base.Employee empl in emplList)
                {
                    TreeNode emplNode = new TreeNode();
                    emplNode.Tag = empl.ID;
                    emplNode.Text = empl.Name;
                    parentTreeNode.Nodes.Add(emplNode);
                }
            }
            else
            {
                TreeNode emplNode = new TreeNode();
                emplNode.Tag = FS.FrameWork.Management.Connection.Operator.ID;
                emplNode.Text = FS.FrameWork.Management.Connection.Operator.Name;
                parentTreeNode.Nodes.Add(emplNode);
            }
            parentTreeNode.ExpandAll();

            return base.OnDrawTree();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            TreeNode selectNode = tvLeft.SelectedNode;
            string emplCode;
            switch (selectNode.Level)
            {
                case 0:
                    emplCode = "ALL";
                    break;
                default:
                    emplCode = selectNode.Tag.ToString();
                    break;
            }

            this.dwMain.RowFocusChanged -= this.dwMain_RowFocusChanged;
            base.OnRetrieve(base.beginTime, base.endTime,  string.IsNullOrEmpty(this.tbInvoiceNo.Text)?"ALL":this.tbInvoiceNo.Text.PadLeft(12, '0'), emplCode, this.cboCancelFlag.SelectedItem.ID, string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo) ? "ALL" : this.ucQueryInpatientNo1.InpatientNo);
            this.dwMain.RowFocusChanged += this.dwMain_RowFocusChanged;
            if (dwMain.RowCount > 0)
            {
                RetrieveDetail(1);

            }
            else
            {
                dwDetail.Reset();
            }
            return 1;
        }


        private void dwMain_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            int currRow = e.RowNumber;
            if (currRow == 0)
            {
                return;
            }
            this.RetrieveDetail(currRow);
           
        }
        private void RetrieveDetail(int currRow)
        {
            try
            {

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����ϸ�����Ժ�...");
                string sInvoiceNo;
                double dbBalanceNo;
                string sInpatientNo;
                string trans_type;
                sInvoiceNo = dwMain.GetItemString(currRow, "fin_ipb_balancehead_invoice_no");
                dbBalanceNo = dwMain.GetItemDouble(currRow, "fin_ipb_balancehead_balance_no");
                sInpatientNo = dwMain.GetItemString(currRow, "fin_ipb_balancehead_inpatient_no");
                trans_type = dwMain.GetItemString(currRow, "fin_ipb_balancehead_trans_type");

                dwDetail.Retrieve(sInvoiceNo, dbBalanceNo, sInpatientNo, trans_type);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            }
        }
    }
}

