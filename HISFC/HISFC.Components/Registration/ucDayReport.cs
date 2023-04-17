using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UFC.Registration
{
    /// <summary>
    /// �ս�
    /// </summary>
    public partial class ucDayReport : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucDayReport()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucDayReport_Load);            
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
        }


        #region ����
        /// <summary>
        /// �ս������
        /// </summary>
        Neusoft.HISFC.Management.Registration.DayReport dayReport = new Neusoft.HISFC.Management.Registration.DayReport();
        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        Neusoft.HISFC.Management.Registration.Register regMgr = new Neusoft.HISFC.Management.Registration.Register();        
        /// <summary>
        /// ˮ������
        /// </summary>
      //  Report.crDayReport crDayReport = new UFC.Registration.Report.crDayReport();
        /// <summary>
        /// �ս�ʵ��
        /// </summary>
        Neusoft.HISFC.Object.Registration.DayReport objDayReport;
        /// <summary>
        /// ����Դ
        /// </summary>
        DataSet source = new Report.dsDayReport();
        DataSet dsRegInfo = new DataSet();
        private ArrayList al;
        private Boolean RepeatFlag = false;
        #endregion

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDayReport_Load(object sender, EventArgs e)
        {
            this.InitTree();
                        
            this.ShowReport();

            //Ĭ��ѡ��ǰ����Ա
            foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
            {
                if (node.Tag.ToString() == regMgr.Operator.ID)
                {
                    this.treeView1.SelectedNode = node;
                    break;
                }
            }
        }

        /// <summary>
        /// ���ɹҺ�Ա�б�
        /// </summary>
        private void InitTree()
        {
            this.treeView1.Nodes.Clear();

            TreeNode root = new TreeNode("�Һ�Ա", 22, 22);
            this.treeView1.Nodes.Add(root);
            
            //Neusoft.HISFC.Management.Registration.Permission perMgr = new Neusoft.HISFC.Management.Registration.Permission();
            ////��ò����ҺŴ��ڵ���Ա
            //this.al = perMgr.Query("UFC.Registration.ucRegister");
            //if (al == null)
            //{
            //    MessageBox.Show("��ȡ�Һ�Ա��Ϣʱ����!" + perMgr.Err, "��ʾ");
            //    return;
            //}
            
            
            //foreach (Neusoft.NFC.Object.NeuObject obj in al)
            //{
            //    TreeNode node = new TreeNode(obj.Name, 34, 35);
            //    node.Tag = obj.ID;
            //    root.Nodes.Add(node);
            //}
            TreeNode node = new TreeNode(this.dayReport.Operator.Name, 34, 35);
            node.Tag = this.dayReport.Operator.ID;
            root.Nodes.Add(node);

            root.Expand();
            this.SetQueryDateTime();
            
            this.nDTPBeginDate.Enabled = false;

            
            
        }

        /// <summary>
        /// ��ѯ�ս���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node.Parent != null)//���Ǹ��ڵ�
            {
                Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڼ�������Ա�ս���Ϣ,���Ժ�!");
                Application.DoEvents();

                this.Query(e.Node.Tag.ToString(), e.Node.Text);

                Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            this.source.Tables[0].Rows.Clear();
            this.objDayReport = null;
            this.ShowReport();
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        private void ShowReport()
        {
            //try
            //{
            //    this.crDayReport.SetDataSource(this.source);
            //    this.crystalReportViewer1.ReportSource = this.crDayReport;
            //    this.crystalReportViewer1.RefreshReport();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);                
            //}
        }
        /// <summary>
        /// �ս��ѯ
        /// </summary>
        private void Query(string OperID, string OperName)
        {
            //��ʼʱ�䡢����ʱ��
            //���һ���ս�Ҳû��,Ĭ����ʼʱ��Ϊ2000-01-01
            //DateTime beginDate = DateTime.Parse("2000-01-01");

            //string rtn = this.dayReport.GetBeginDate(OperID);
            //if (rtn == "-1") return;

            //if (rtn != "") beginDate = DateTime.Parse(rtn);

            //DateTime endDate = this.dayReport.GetDateTimeFromSysDateTime();
            this.RepeatFlag = false;
            DateTime beginDate = this.nDTPBeginDate.Value;
            DateTime endDate = this.nDTPEndDate.Value;
            //�����Һ���ϸ
            this.dsRegInfo = this.GetRegDetail(OperID, beginDate, endDate);
            if (dsRegInfo == null) return;
            this.nDTPBeginDate.Value = beginDate;
            this.nDTPEndDate.Value = endDate;
            //if (dsRegInfo.Tables.Count == 0 || dsRegInfo.Tables[0].Rows.Count == 0)
            //{
            //    MessageBox.Show("�ò���Ա�޹Һ���Ϣ!", "��ʾ");
            //    return;
            //}

            

            #region �����ս���Ϣ
            this.SetReportDetail(beginDate, endDate, OperID, OperName);
            this.SetReport();
            this.SetCR();
            #endregion

            this.ShowReport();
            this.ucRegDayBalanceReport1.InitUC();
            this.ucRegDayBalanceReport1.setFP(this.objDayReport);
        }
        private void SetQueryDateTime()
        {
            //��ʼʱ�䡢����ʱ��
            //���һ���ս�Ҳû��,Ĭ����ʼʱ��Ϊ2000-01-01
            DateTime beginDate = DateTime.Parse("2000-01-01");

            string rtn = this.dayReport.GetBeginDate(this.dayReport.Operator.ID);
            if (rtn == "-1") return;

            if (rtn != "") beginDate = DateTime.Parse(rtn);

            DateTime endDate = this.dayReport.GetDateTimeFromSysDateTime();
            this.nDTPBeginDate.Value = beginDate;
            this.nDTPEndDate.Value = endDate;

        }

        /// <summary>
        /// ��ȡ�Һ���ϸ
        /// </summary>
        /// <param name="OperId"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private DataSet GetRegDetail(string OperId, DateTime begin, DateTime end)
        {
            //string sql = "";

            //if (this.regMgr.Sql.GetSql("Registration.Register.Query.11", ref sql) == -1) return null;

            //try
            //{
            //    sql = string.Format(sql, begin.ToString(), end.ToString(), OperId);
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("��ȡ����Ա�Һ���ϸ����!" + e.Message, "��ʾ");
            //    return null;
            //}

            DataSet ds = new DataSet();

            //if (this.regMgr.Sql.ExecQuery(sql, ref ds) == -1) return null;
            this.dayReport.QueryRegisterDetails(OperId, begin, end, ref ds);
            return ds;

        }

        /// <summary>
        /// �����ս���ϸʵ��
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="operID"></param>
        /// <param name="operName"></param>
        private void SetReportDetail(DateTime begin, DateTime end, string operID, string operName)
        {

            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            this.objDayReport = new Neusoft.HISFC.Object.Registration.DayReport();
            this.objDayReport.BeginDate = begin;
            this.objDayReport.EndDate = end;
            this.objDayReport.Oper.ID = operID;
            this.objDayReport.Oper.Name = operName;
            this.objDayReport.Oper.OperTime = current;

            Neusoft.HISFC.Object.Registration.DayDetail detail = new Neusoft.HISFC.Object.Registration.DayDetail();
            detail.EndRecipeNo = "-1";

            //�����ձ�ʵ��,ԭ����������������״̬һ�µĺ�Ϊһ���ս���ϸ
            for (int i = 0; i < this.dsRegInfo.Tables[0].Rows.Count; i++)
            {
                DataRow row = this.dsRegInfo.Tables[0].Rows[i];


                ///
                ///�Һ�״̬Ϊ�˺š��ս��˲��������ˡ�δ�սᣬ�������Ϊ�����˸ò���Ա��,�˺���Ч
                ///
                if (row[8].ToString() == "0" && operID != row[9].ToString())
                {
                    row[8] = "1";//��Ϊ��Ч
                }


                if (long.Parse(row[0].ToString()) - 1 != long.Parse(detail.EndRecipeNo) ||//����������
                    int.Parse(row[8].ToString()) != (int)detail.Status) //״̬�ı�					
                {
                    //�����µ��ս���ϸ
                    if (i != 0)//��һ��������
                    { this.objDayReport.Details.Add(detail); }

                    //���������µ���ϸ
                    detail = new Neusoft.HISFC.Object.Registration.DayDetail();
                    detail.BeginRecipeNo = row[0].ToString();//��ʼ��	
                    detail.EndRecipeNo = detail.BeginRecipeNo;//Convert.ToString(long.Parse(row[0].ToString()) -1) ;
                    detail.Status = (Neusoft.HISFC.Object.Base.EnumRegisterStatus)int.Parse(row[8].ToString());
                }

                detail.EndRecipeNo = row[0].ToString();
                detail.Count++;
                detail.RegFee += decimal.Parse(row[1].ToString());
                detail.ChkFee += decimal.Parse(row[2].ToString());
                detail.DigFee += decimal.Parse(row[3].ToString());
                detail.OthFee += decimal.Parse(row[4].ToString());
                detail.OwnCost += decimal.Parse(row[5].ToString());
                detail.PayCost += decimal.Parse(row[6].ToString());
                detail.PubCost += decimal.Parse(row[7].ToString());

                if (i == this.dsRegInfo.Tables[0].Rows.Count - 1)
                    this.objDayReport.Details.Add(detail);//���һ��Ҳ����������ϸ
            }
        }
        /// <summary>
        /// �����ս�ʵ��
        /// </summary>
        private void SetReport()
        {
            for(int i = 0; i < this.objDayReport.Details.Count; i++)
            {
                Neusoft.HISFC.Object.Registration.DayDetail detail = this.objDayReport.Details[i];
                detail.OrderNO = i.ToString();

                this.objDayReport.SumCount += detail.Count;
                if (detail.Status == Neusoft.HISFC.Object.Base.EnumRegisterStatus.Cancel) continue;

                this.objDayReport.SumRegFee += detail.RegFee;
                this.objDayReport.SumChkFee += detail.ChkFee;
                this.objDayReport.SumDigFee += detail.DigFee;
                this.objDayReport.SumOthFee += detail.OthFee;
                this.objDayReport.SumOwnCost += detail.OwnCost;
                this.objDayReport.SumPayCost += detail.PayCost;
                this.objDayReport.SumPubCost += detail.PubCost;
            }
        }
        /// <summary>
        /// ����ˮ������
        /// </summary>
        private void SetCR()
        {
            DataSet ds = new Report.dsDayReport();

            string RMBCasch = Neusoft.NFC.Function.NConvert.ToCapital(this.objDayReport.SumOwnCost);

            foreach (Neusoft.HISFC.Object.Registration.DayDetail detail in this.objDayReport.Details)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = detail.BeginRecipeNo;
                dr[1] = detail.EndRecipeNo;
                dr[2] = detail.Count;


                //�˺�

                if (detail.Status == Neusoft.HISFC.Object.Base.EnumRegisterStatus.Back)
                {
                    dr[3] = -detail.RegFee;
                    dr[4] = -(detail.DigFee + detail.ChkFee);
                    dr[6] = -detail.OthFee;
                    dr[7] = -detail.OwnCost;
                }
                else
                {
                    dr[3] = detail.RegFee;
                    dr[4] = detail.DigFee + detail.ChkFee;
                    dr[6] = detail.OthFee;
                    dr[7] = detail.OwnCost;
                }

                dr[8] = this.getStatus(detail.Status);
                dr[9] = this.objDayReport.Oper.ID;
                dr[10] = this.treeView1.SelectedNode.Text;
                dr[11] = this.objDayReport.BeginDate;
                dr[12] = this.objDayReport.EndDate;
                dr[13] = this.objDayReport.Oper.OperTime;
                dr[15] = this.objDayReport.SumOwnCost;
                dr[16] = this.objDayReport.SumRegFee;
                dr[17] = this.objDayReport.SumDigFee + this.objDayReport.SumChkFee;
                dr[5] = this.objDayReport.SumOthFee;
                dr[18] = RMBCasch;

                ds.Tables[0].Rows.Add(dr);
            }

            //��״̬��������
            DataView dv = new DataView(ds.Tables[0]);
            dv.Sort = "״̬ DESC";

            source.Tables[0].Rows.Clear();
            foreach (DataRowView dvRow in dv)
            {
                DataRow dr = this.source.Tables[0].NewRow();
                #region ��ֵ
                dr[0] = dvRow[0];
                dr[1] = dvRow[1];
                dr[2] = dvRow[2];
                dr[3] = dvRow[3];
                dr[4] = dvRow[4];
                dr[5] = dvRow[5];
                dr[6] = dvRow[6];
                dr[7] = dvRow[7];
                dr[8] = dvRow[8];
                dr[9] = dvRow[9];
                dr[10] = dvRow[10];
                dr[11] = dvRow[11];
                dr[12] = dvRow[12];
                dr[13] = dvRow[13];
                dr[14] = dvRow[14];
                dr[15] = dvRow[15];
                dr[16] = dvRow[16];
                dr[17] = dvRow[17];
                dr[18] = dvRow[18];
                #endregion
                this.source.Tables[0].Rows.Add(dr);
            }

        }
        /// <summary>
        /// ��ȡ����״̬
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string getStatus(Neusoft.HISFC.Object.Base.EnumRegisterStatus status)
        {
            if (status == Neusoft.HISFC.Object.Base.EnumRegisterStatus.Valid)
            { return "����"; }
            else if (status == Neusoft.HISFC.Object.Base.EnumRegisterStatus.Back)
            { return "�˺�"; }
            else if (status == Neusoft.HISFC.Object.Base.EnumRegisterStatus.Cancel)
            { return "����"; }
            else
            { return "����"; }
        }
        /// <summary>
        /// ��ȡҽԺ����
        /// </summary>
        /// <returns></returns>
        private string getHosName()
        {
            return "";
        }
        /// <summary>
        /// ��ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Q.GetHashCode())
            {
                this.Query();

                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                this.Save();

                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.R.GetHashCode())
            {
                this.Reprint();

                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// �ս�
        /// </summary>
        private void Save()
        {
            if (this.treeView1.SelectedNode == null) return;

            //�����¼���һ�飬��ֹʱ���������׳���!
            this.treeView1_AfterSelect(new object(), new TreeViewEventArgs(this.treeView1.SelectedNode, TreeViewAction.Unknown));

            if (this.objDayReport == null)
            {
                MessageBox.Show("��ѡ��Һ�Ա,��������!", "��ʾ");
                return;
            }
            if (this.objDayReport.ID != "")
            {
                MessageBox.Show("���ս���Ϣ�Ѿ�����,�����ٴα���!", "��ʾ");
                return;
            }
            if (this.objDayReport.Details.Count == 0)
            {
                MessageBox.Show("���ս���Ϣ,���豣��!", "��ʾ");
                return;
            }
            if (this.objDayReport.Oper.ID != regMgr.Operator.ID)
            {
                MessageBox.Show("�������ս᲻�Ǳ��˵ķ�����Ϣ!", "��ʾ");
                return;
            }

            Neusoft.NFC.Management.Transaction SQLCA = new Neusoft.NFC.Management.Transaction(regMgr.con);
            SQLCA.BeginTransaction();

            try
            {
                this.regMgr.SetTrans(SQLCA.Trans);
                this.dayReport.SetTrans(SQLCA.Trans);

                string seq = this.regMgr.GetSequence("Registration.DayReport.GetSequence");
                this.objDayReport.ID = seq;

                foreach (Neusoft.HISFC.Object.Registration.DayDetail detail in this.objDayReport.Details)
                {
                    detail.ID = seq;
                }
                if (this.dayReport.Insert(this.objDayReport) == -1)
                {
                    SQLCA.RollBack();
                    MessageBox.Show(this.dayReport.Err, "��ʾ");
                    return;
                }

                int rtn = this.regMgr.Update(this.objDayReport.BeginDate, this.objDayReport.EndDate,
                    this.objDayReport.Oper.ID,seq);
                if (rtn == -1)
                {
                    SQLCA.RollBack();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return;
                }

                if (rtn == 0)
                {
                    SQLCA.RollBack();
                    MessageBox.Show("�ս���Ϣ״̬�Ѿ����,�������ս�!", "��ʾ");
                    return;
                }

                SQLCA.Commit();
            }
            catch (Exception e)
            {
                SQLCA.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            MessageBox.Show("�ս�ɹ�!", "��ʾ");

            //this.crystalReportViewer1.PrintReport();
            this.PrintPanel(this.panel3);
            this.Clear();
            this.SetQueryDateTime();
            this.Query(this.dayReport.Operator.ID, this.dayReport.Operator.Name);
            
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            this.ucRegDayBalanceReport1.InitUC();
            if (this.treeView1.SelectedNode.Parent == null)
            {
                MessageBox.Show("��ѡ�����Ա!", "��ʾ");
                return;
            }

            frmQueryDayReport f = new frmQueryDayReport();
            f.OperID = this.treeView1.SelectedNode.Tag.ToString();
            f.Query();
            DialogResult r = f.ShowDialog();

            if (r == DialogResult.OK)
            {
                this.objDayReport = f.SelectedDayReport;
                if (this.objDayReport != null && this.objDayReport.ID != "")
                {
                    ArrayList aldetails = this.dayReport.Query(this.objDayReport.ID);
                    foreach (Neusoft.HISFC.Object.Registration.DayDetail obj in aldetails)
                    {
                        this.objDayReport.Details.Add(obj);
                    }

                    this.SetCR();
                    this.ShowReport();
                    this.ucRegDayBalanceReport1.setFP(this.objDayReport);
                }

            }
            f.Dispose();
            this.RepeatFlag = true;
        }
        /// <summary>
        /// ����
        /// </summary>
        private void Reprint()
        {
            //this.crystalReportViewer1.PrintReport();
            if (this.RepeatFlag == true)
            {
                this.PrintPanel(this.panel3);
            }
            else 
            {
                MessageBox.Show("ֻ���ս�����ʹ�ò�����");
                return;
            }
        }

        private Neusoft.NFC.Interface.Forms.ToolBarService toolBarService = new Neusoft.NFC.Interface.Forms.ToolBarService();

        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����(&R)", "", (int)Neusoft.NFC.Interface.Classes.EnumImageList.A��ѯ, true, false, null);
            this.toolBarService.AddToolButton("�ս�(&S)", "", (int)Neusoft.NFC.Interface.Classes.EnumImageList.A����, true, false, null);
            this.toolBarService.AddToolButton("��ӡ(&P)", "", (int)Neusoft.NFC.Interface.Classes.EnumImageList.A��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ѯ(&Q)", "", (int)Neusoft.NFC.Interface.Classes.EnumImageList.A�˿�, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����(&R)":
                    this.Query();

                    break;
                case "�ս�(&S)":
                    this.Save();

                    break;
                case "��ӡ(&P)":
                    this.Reprint();

                    break;
                case "��ѯ(&Q)":
                    this.Query(this.dayReport.Operator.ID,this.dayReport.Operator.Name);
                    break;

            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        /// <summary>
        ///��ӡ����
        /// </summary>
        /// <param name="argPanel"></param>
        public void PrintPanel(System.Windows.Forms.Panel argPanel)
        {
            Neusoft.NFC.Interface.Classes.Print print = new Neusoft.NFC.Interface.Classes.Print();
            print.PrintPage(0,0,this.panel3);
        }
    }
}
