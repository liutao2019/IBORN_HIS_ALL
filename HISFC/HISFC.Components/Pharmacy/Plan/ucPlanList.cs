using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy.Plan
{
    /// <summary>
    /// [��������: ҩƷ�ƻ�����ѡ��(���ƻ��ϲ�)]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucPlanList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPlanList()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operPrivDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ϲ���ƻ���Ϣ
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.InPlan> alterInPlan = null;

        /// <summary>
        /// ���ڽ��
        /// </summary>
        private DialogResult result = DialogResult.Cancel;
        #endregion

        #region �� ��

        /// <summary>
        /// �Ƿ��������ʱ���ѡ��
        /// </summary>
        [Description("�Ƿ��������ʱ���ѡ��"),Category("����"),DefaultValue(false)]
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
                switch (value)
                {
                    case "0":
                        this.cmbState.Text = "�ƻ�[0]";
                        break;
                    case "1":
                        this.cmbState.Text = "�ɹ�[1]";
                        break;
                    case"2":
                        this.cmbState.Text = "���[2]";
                        break;
                    case"3":
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
        public List<FS.HISFC.Models.Pharmacy.InPlan> AlterInPlan
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
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            }
        }

        /// <summary>
        /// ��ѯ����ʱ��
        /// </summary>
        protected DateTime EndTime
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();
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
            ArrayList alList = this.itemManager.QueryInPLanList(deptNO, state);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("��ȡ���ƻ������б�������" + this.itemManager.Err));
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
            List<FS.HISFC.Models.Pharmacy.InPlan> alDetail = this.itemManager.QueryInPlanDetail(deptNO, listNO);
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ϸʧ��" + this.itemManager.Err));
                return -1;
            }

            this.neuSpread1_Sheet2.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.InPlan objDetail in alDetail)
            {
                this.neuSpread1_Sheet2.Rows.Add(0, 1);
                this.neuSpread1_Sheet2.Cells[0, 0].Text = objDetail.Item.Name;                                          //ҩƷ����
                this.neuSpread1_Sheet2.Cells[0, 1].Text = objDetail.Item.Specs;                                         //���
                this.neuSpread1_Sheet2.Cells[0, 2].Text = (objDetail.PlanQty / objDetail.Item.PackQty).ToString();      //�ƻ�����
                this.neuSpread1_Sheet2.Cells[0, 3].Text = objDetail.Item.PackUnit;                                      //��λ
                this.neuSpread1_Sheet2.Cells[0, 4].Text = (objDetail.StoreQty / objDetail.Item.PackQty).ToString();     //�����ҿ����
                this.neuSpread1_Sheet2.Cells[0, 5].Text = (objDetail.StoreTotQty / objDetail.Item.PackQty).ToString();     //ȫԺ�����
                this.neuSpread1_Sheet2.Cells[0, 6].Text = objDetail.Memo;                                               //��ע
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
            for(int i = 0;i < this.neuSpread1_Sheet1.Rows.Count;i++)
            {
                if (NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i,0].Value))
                {
                    strList[i] = this.neuSpread1_Sheet1.Cells[i, 1].Text;

                    singlePlanNO = strList[i];
                    billNOCount++;
                }
            }
            //δѡ��ƻ���
            if (billNOCount == 0)
            {
                MessageBox.Show(Language.Msg("��ѡ��ƻ���"));
                return -1;
            }

            //ֻѡ��һ������ ��������ʾ
            if (billNOCount == 1)
            {
                this.alterInPlan = this.itemManager.MergeInPlan(this.operPrivDept.ID, singlePlanNO);
                if (this.alterInPlan == null)
                {
                    MessageBox.Show(Language.Msg("���ݵ��ݺŻ�ȡ���ƻ���Ϣʧ��") + this.itemManager.Err);
                    return -1;
                }
            }
            else  //ѡ���������� ���кϲ�
            {
                DialogResult rs = MessageBox.Show(Language.Msg("��ѡ���˶������ ȷ�϶���ѡ��ĵ��ݺϲ�Ϊһ�żƻ�����? ��ע�� �˲������ɻָ�"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    return -1;
                }

                this.alterInPlan = this.itemManager.MergeInPlan(this.operPrivDept.ID, strList);

                string strBillNO = "";

                foreach (FS.HISFC.Models.Pharmacy.InPlan info in this.alterInPlan)
                {
                    #region ���ݺŴ���

                    if (strBillNO == "")
                    {
                        info.BillNO = this.itemManager.GetPlanBillNO(info.Dept.ID);
                        if (info.BillNO == null || info.BillNO == "")
                        {
                            this.alterInPlan = new List<FS.HISFC.Models.Pharmacy.InPlan>();
                            MessageBox.Show(Language.Msg("��ȡ�ɹ����ݺ�ʧ��" + this.itemManager.Err));
                            return -1;
                        }

                        strBillNO = info.BillNO;
                    }
                    else
                    {
                        info.BillNO = strBillNO;
                    }

                    #endregion

                    if (this.itemManager.InsertInPlan(info) == -1)
                    {
                        this.alterInPlan = new List<FS.HISFC.Models.Pharmacy.InPlan>();
                        MessageBox.Show(Language.Msg("�������������ƻ���Ϣʧ��" + this.itemManager.Err));
                        return -1;
                    }

                    if (info.ReplacePlanNO.IndexOf("|") == -1)
                    {
                        if (this.itemManager.CancelInPlan(info.ID, info.ReplacePlanNO) == -1)
                        {
                            this.alterInPlan = new List<FS.HISFC.Models.Pharmacy.InPlan>();
                            MessageBox.Show(Language.Msg("����ԭ�ƻ�����Ϣʧ��" + this.itemManager.Err));
                            return -1;
                        }
                    }
                    else
                    {
                        if (this.itemManager.CancelInPlan(info.ID, info.ReplacePlanNO.Split('|')) == -1)
                        {
                            this.alterInPlan = new List<FS.HISFC.Models.Pharmacy.InPlan>();
                            MessageBox.Show(Language.Msg("����ԭ�ƻ�����Ϣʧ��" + this.itemManager.Err));
                            return -1;
                        }
                    }
                }
            }

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

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.QueryList(this.operPrivDept.ID, this.State);
            }

            base.OnLoad(e);
        }

        private void btnMerge_Click(object sender, EventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.result = DialogResult.Cancel;

            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
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

            string listNO = this.neuSpread1_Sheet1.Cells[e.Row,1].Text;
            if (this.QueryDetail(this.operPrivDept.ID, listNO) == -1)
            {
                return;
            }

            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
        }
    }
}
