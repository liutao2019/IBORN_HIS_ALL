using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucNotCallBack : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucNotCallBack()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();

        private CallbackStatus cbStatus = CallbackStatus.δ����;

        FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack cbMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ���ò�ѯ����
        /// </summary>
        [Category("������ѯ״̬"), Description("���õ�ǰ��ѯ������������")]
        public CallbackStatus CbStatus
        {
            get { return cbStatus; }
            set { cbStatus = value; }
        }

        DateTime dtBeginUse = new DateTime(2011, 1, 1);
        /// <summary>
        /// ���չ��ܿ�ʼʹ��ʱ��
        /// </summary>
        [Category("���ÿ�ʼʹ��ʱ�俪��"), Description("��ʼʹ�õ�ʱ�䣬����δ���ղ�ѯ��ʼ����")]
        public DateTime DtBeginUse
        {
            get { return this.dtBeginUse; }
            set { this.dtBeginUse = value; }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public enum CallbackStatus
        {
            /// <summary>
            /// δ����
            /// </summary>
            δ���� = 0,
            /// <summary>
            /// �ѻ���
            /// </summary>
            �ѻ���
        };

        private void neuBtnQuery_Click(object sender, EventArgs e)
        {
            //modify 2011-3-16 ch ʱ���ʽ������֤��һ��������ڣ�
            this.OnQuery(this.neuDTBegin.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neuDTEnd.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
        }

        private void OnQuery(DateTime dtBegin, DateTime dtEnd)
        {
            if (ds != null)
            {
                ds.Clear();
            }

            string status = ((int)cbStatus).ToString();

            //sql = string.Format(sql, status, dtBegin, dtEnd, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            System.Collections.ArrayList al;
            string deptCode = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            FS.HISFC.Models.Base.Department dept = deptMgr.GetDeptmentById(deptCode);

            if (dept == null)
            {
                MessageBox.Show("���ݿ��ұ����ȡ����ʵ�����");
                return;
            }
            if (dept.DeptType.Name == "��ʿվ")
            {
                al = deptMgr.GetDeptFromNurseStation(dept);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("���Ҳ�����Ӧ�Ŀ��ҳ��� nullֵ");
                    return;
                }
                dept = (FS.HISFC.Models.Base.Department)al[0];
            }
            string sql = string.Empty;
            if (this.cbStatus == CallbackStatus.δ����)
            {
                if (dtBegin.Date < this.dtBeginUse)
                {
                    if (MessageBox.Show("��ѯ��ʼ����Ӧ�ô��ڵ���ϵͳģ��ʹ������" + this.dtBeginUse.ToShortDateString() + "�������ѯ�������Ϊδ����״̬���Ƿ������ѯ", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (this.cbMgr.Sql.GetSql("Case.Callback.QueryDataSet.UnCallBack", ref sql) < 0)
                {
                    return;
                }
            }
            else if (this.cbStatus == CallbackStatus.�ѻ���)
            {
                if (this.cbMgr.Sql.GetSql("Case.Callback.QueryDataSet.CallBack", ref sql) < 0)
                {
                    return;
                }
            }
            string docCode = string.Empty;
            if (this.checkBox1.Checked)
            {
                docCode = "ALL";
            }
            else
            {
                docCode = this.cbMgr.Operator.ID;
            }
            this.cbMgr.ExecQuery(string.Format(sql, status, dtBegin, dtEnd, dept.ID,docCode), ref ds);

            this.neuSpread1_Sheet1.DataSource = ds;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(this.neuPanel3.Left, this.neuPanel3.Top, this.neuPanel3);
            return base.OnPrint(sender, neuObject);
        }
    }
}
