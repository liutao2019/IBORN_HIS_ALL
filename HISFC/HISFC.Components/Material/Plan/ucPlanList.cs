using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Material.Plan
{
    /// <summary>
    /// ���������ʱ���ѡ��
    /// </summary>
    public partial class ucPlanList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPlanList()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ��Ʒ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

        /// <summary>
        /// ������Ŀ��
        /// {9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operPrivDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ϲ���ƻ���Ϣ
        /// </summary>
        private ArrayList alterInPlan = null;

        /// <summary>
        /// ���ڽ��
        /// </summary>
        private DialogResult result = DialogResult.Cancel;

        /// <summary>
        /// ���ݼ���״̬
        /// </summary>
        private string state = "0";
        #endregion

        #region �� ��

        /// <summary>
        /// �Ƿ��������ʱ���ѡ��
        /// </summary>
        [Description("�Ƿ��������ʱ���ѡ��"), Category("����"), DefaultValue(false)]
        public bool IsShowTimeSelect
        {
            get
            {
                return this.neuPanel1.Visible;
            }
            set
            {
                this.neuPanel1.Visible = value;
            }
        }

        /// <summary>
        /// ��ǰ����Ȩ�޿���
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperPrivDept
        {
            get
            {
                return this.operPrivDept;
            }
            set
            {
                this.operPrivDept = value;
            }
        }


        /// <summary>
        /// ��ǰ��������״̬
        /// </summary>
        public string State
        {
            get
            {
                return this.state;

                if (this.cmbState.Text == "" || this.cmbState.Text.IndexOf("[") == -1)
                {
                    return "0";
                }
                else
                {
                    return this.cmbState.Text.Substring(this.cmbState.Text.IndexOf("[") + 1, 1);
                }
            }
            set
            {
                this.state = value;

                switch (value)
                {
                    case "0":
                        this.cmbState.Text = "�ƻ�[0]";
                        break;
                    case "1":
                        this.cmbState.Text = "�ɹ�[1]";
                        break;
                    case "2":
                        this.cmbState.Text = "���[2]";
                        break;
                    case "3":
                        this.cmbState.Text = "���[3]";
                        break;
                    case "4":
                        this.cmbState.Text = "����[4]";
                        break;
                }
            }
        }


        /// <summary>
        /// �ϲ���ƻ���Ϣ
        /// </summary>
        public ArrayList AlterInPlan
        {
            get
            {
                return alterInPlan;
            }
            set
            {
                alterInPlan = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }

        /// <summary>
        ///  ��ѯ��ʼʱ��
        /// </summary>
        protected DateTime BeginTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtBegin.Text);
            }
        }

        /// <summary>
        /// ��ѯ����ʱ��
        /// </summary>
        protected DateTime EndTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtEnd.Text);
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            DateTime sysTime = this.planManager.GetDateTimeFromSysDateTime();
            this.dtBegin.Value = sysTime.Date.AddDays(-7);
            this.dtEnd.Value = sysTime;

            this.neuSpread1_Sheet2.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet1.Columns[0].Locked = false;

            return 1;
        }

        /// <summary>
        /// �����б����
        /// </summary>
        /// <param name="deptNO"></param>
        /// <param name="state"></param>
        /// <param name="queryBeginTime"></param>
        /// <param name="queryEndTime"></param>
        /// <returns></returns>
        public int QueryList(string deptNO, string state)
        {
            ArrayList alList = this.planManager.QueryInPLanList(deptNO, state);
            if (alList == null)
            {
                MessageBox.Show("��ȡ���ƻ������б�������" + this.planManager.Err);
                return -1;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.FrameWork.Models.NeuObject objList in alList)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                this.neuSpread1_Sheet1.Cells[0, 0].Value = false;           //�Ƿ�ѡ�� Ĭ�ϲ�ѡ��
                this.neuSpread1_Sheet1.Cells[0, 1].Text = objList.ID;       //�ƻ�����
                this.neuSpread1_Sheet1.Cells[0, 2].Text = objList.Name;     //�ƻ���
            }
            return 1;
        }

        /// <summary>
        /// ������ϸ��ѯ
        /// </summary>
        /// <param name="deptNO">������</param>
        /// <param name="listNO">���ݺ�</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int QueryDetail(string deptNO, string listNO)
        {
            ArrayList alDetail = this.planManager.QueryInPlanDetail(deptNO, listNO);
            if (alDetail == null)
            {
                MessageBox.Show("��ȡ��ϸʧ��" + this.planManager.Err);
                return -1;
            }

            this.neuSpread1_Sheet2.Rows.Count = 0;

            foreach (FS.HISFC.Models.Material.InputPlan objDetail in alDetail)
            {
                //���»�ȡһ�����ʻ�����Ϣ
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                objDetail.StoreBase.Item = this.itemManager.GetMetItemByMetID(objDetail.StoreBase.Item.ID);

                this.neuSpread1_Sheet2.Rows.Add(0, 1);
                this.neuSpread1_Sheet2.Cells[0, 0].Text = objDetail.StoreBase.Item.Name;                                         //��Ʒ����
                this.neuSpread1_Sheet2.Cells[0, 1].Text = objDetail.StoreBase.Item.Specs;                                        //���
                this.neuSpread1_Sheet2.Cells[0, 2].Text = (objDetail.PlanNum / objDetail.StoreBase.Item.PackQty).ToString();     //�ƻ�����
                this.neuSpread1_Sheet2.Cells[0, 3].Text = objDetail.StoreBase.Item.PackUnit;                                     //��λ
                this.neuSpread1_Sheet2.Cells[0, 4].Text = (objDetail.StoreSum / objDetail.StoreBase.Item.PackQty).ToString();    //�����ҿ����
                this.neuSpread1_Sheet2.Cells[0, 5].Text = (objDetail.StoreTotsum / objDetail.StoreBase.Item.PackQty).ToString(); //ȫԺ�����
                this.neuSpread1_Sheet2.Cells[0, 6].Text = objDetail.Memo;														//��ע
            }

            return 1;
        }

        /// <summary>
        /// ���ݺϲ�
        /// </summary>
        /// <returns></returns>
        public int MergeInPlan()
        {
            int billNOCount = 0;
            string singlePlanNO = "";
            string[] strList = new string[this.neuSpread1_Sheet1.Rows.Count];
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value))
                {
                    strList[i] = this.neuSpread1_Sheet1.Cells[i, 1].Text;

                    singlePlanNO = strList[i];
                    billNOCount++;
                }
            }
            //δѡ��ƻ���
            if (billNOCount == 0)
            {
                MessageBox.Show("��ѡ��ƻ���");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //ֻѡ��һ������ ��������ʾ
            if (billNOCount == 1)
            {
                this.alterInPlan = this.planManager.MergeInPlan(this.operPrivDept.ID, singlePlanNO);
                if (this.alterInPlan == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���ݵ��ݺŻ�ȡ���ƻ���Ϣʧ��" + this.planManager.Err);
                    return -1;
                }
            }
            else  //ѡ���������� ���кϲ�
            {
                DialogResult rs = MessageBox.Show("��ѡ���˶������ ȷ�϶���ѡ��ĵ��ݺϲ�Ϊһ�żƻ�����? ��ע�� �˲������ɻָ�", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }

                this.alterInPlan = this.planManager.MergeInPlan(this.operPrivDept.ID, strList);

                string strBillNO = "";
                //{4FDD0EDB-7352-46ab-A1A4-319E512A8CBF}
                int planNO = 1;
                foreach (FS.HISFC.Models.Material.InputPlan info in this.alterInPlan)
                {
                    //if (this.planManager.CancelInPlan(info.ID) == -1){4FDD0EDB-7352-46ab-A1A4-319E512A8CBF}
                    if (this.planManager.CancelInPlan(info.StorageCode, info.Extend2) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.alterInPlan = new ArrayList();
                        MessageBox.Show("����ԭ�ƻ�����Ϣʧ��" + this.planManager.Err);
                        return -1;
                    }

                    #region ���ݺŴ���

                    if (strBillNO == "")
                    {
                        info.PlanListCode = this.planManager.GetPlanNO(info.StorageCode);
                        if (info.PlanListCode == null || info.StorageCode == "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.alterInPlan = new ArrayList();
                            MessageBox.Show("��ȡ�ɹ����ݺ�ʧ��" + this.planManager.Err);
                            return -1;
                        }

                        strBillNO = info.PlanListCode;
                        //{4FDD0EDB-7352-46ab-A1A4-319E512A8CBF}
                        info.PlanNo = planNO;
                        planNO++;
                    }
                    else
                    {
                        info.PlanListCode = strBillNO;
                        //{4FDD0EDB-7352-46ab-A1A4-319E512A8CBF}
                        info.PlanNo = planNO;
                        planNO++;
                    }

                    #endregion
                    //{4FDD0EDB-7352-46ab-A1A4-319E512A8CBF}
                    info.Extend2 = info.Extend2.Replace(@"','", "|");

                    if (this.planManager.InsertInputPlan(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.alterInPlan = new ArrayList();
                        MessageBox.Show("�������������ƻ���Ϣʧ��" + this.planManager.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        protected void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void ucPlanList_Load(object sender, System.EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.QueryList(this.operPrivDept.ID, this.State);
            }            
        }

        private void btnMerge_Click(object sender, System.EventArgs e)
        {
            if (this.MergeInPlan() == -1)
            {
                this.result = DialogResult.Cancel;
            }
            else
            {
                this.result = DialogResult.OK;

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.result = DialogResult.Cancel;

            this.Close();
        }

        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            this.QueryList(this.operPrivDept.ID, this.State);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
                return;

            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet2)
            {
                this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
                return;
            }

            string listNO = this.neuSpread1_Sheet1.Cells[e.Row, 1].Text;
            if (this.QueryDetail(this.operPrivDept.ID, listNO) == -1)
            {
                return;
            }

            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
        }


    }
}
