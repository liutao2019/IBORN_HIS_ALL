using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ִ�е���ӡ]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucExecBill : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucExecBill()
        {
            InitializeComponent();
        }

        #region ����
        private FS.HISFC.BizLogic.Order.ExecBill Bill = new FS.HISFC.BizLogic.Order.ExecBill();
        
        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        bool IsRePrint = false;

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        bool IsFirst = false; 

        /// <summary>
        /// ִ�е�ִ��ʱ��
        /// </summary>
        string Memo = "";

        /// <summary>
        /// ���û�ʿվ����ҽ��������,���Ÿ��� ����:CONS ����:DEPTXXX ҽ��:DEPTXXX ����:OTHER"
        /// </summary>
        string speOrderType = "";

        protected List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;

        /// <summary>
        /// ��ǰ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPrintTransFusion ip = null;

        /// <summary>
        /// �Ƿ���ʾ��������ӡ
        /// </summary>
        private bool isShowFirstDay = false;

        /// <summary>
        /// �Ƿ���ʾ��������ӡ
        /// </summary>
        [Category("��ӡ����"), Description("�Ƿ���ʾ��������ӡ")]
        public bool IsShowFirstDay
        {
            get
            {
                return isShowFirstDay;
            }
            set
            {
                isShowFirstDay = value;
            }
        }

        /// <summary>
        /// ֹͣ��ҽ���Ƿ��ӡ��ֹͣʱ��֮ǰ�ģ�
        /// </summary>
        private bool dcIsPrint = true;

        /// <summary>
        /// ֹͣ��ҽ���Ƿ��ӡ��ֹͣʱ��֮ǰ�ģ�
        /// </summary>
        [Category("��ӡ����"), Description("ֹͣ��ҽ���Ƿ��ӡ��ֹͣʱ��֮ǰ�ģ�")]
        public bool DCIsPrint
        {
            get
            {
                return dcIsPrint;
            }
            set
            {
                dcIsPrint = value;
            }
        }

        /// <summary>
        /// δ�շ��Ƿ��ӡ
        /// </summary>
        private bool noFeeIsPrint = true;

        /// <summary>
        /// δ�շ��Ƿ��ӡ
        /// </summary>
        [Category("��ӡ����"), Description("δ�շ��Ƿ��ӡ��Ĭ�ϴ�ӡ����ʾδ�շѣ���")]
        public bool NoFeeIsPrint
        {
            get
            {
                return noFeeIsPrint;
            }
            set
            {
                noFeeIsPrint = value;
            }
        }

        /// <summary>
        /// �˷��Ƿ��ӡ
        /// </summary>
        private bool quitFeeIsprint = true;

        /// <summary>
        /// �˷��Ƿ��ӡ
        /// </summary>
        [Category("��ӡ����"), Description("�˷��Ƿ��ӡ")]
        public bool QuitFeeIsPrint
        {
            get
            {
                return quitFeeIsprint;
            }
            set
            {
                quitFeeIsprint = value;
            }
        }

        /// <summary>
        /// ���û�ʿվ����ҽ��������,���Ÿ���
        /// </summary>
        [Category("�ؼ�����"), Description("���û�ʿվ����ҽ��������,���Ÿ��� ����:CONS ����:DEPTXXX ҽ��:DEPTXXX ����:OTHER")]
        public string SpeOrderType
        {
            set
            {
                this.speOrderType = value;
            }
            get
            {
                return this.speOrderType;
            }
        }

        /// <summary>
        /// Ĭ�ϵ�ִ�н���ʱ��
        /// </summary>
        private string endTime = "12:01:00";

        /// <summary>
        /// ��ѯ��ֹʱ��
        /// </summary>
        [Category("��ѯ����"), Description("Ĭ�ϵĲ�ѯ����ʱ�䣬�� 12:01:00")]
        public string EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        /// <summary>
        /// ��ʼʱ����ļ������
        /// </summary>
        private int beginDateSpanDay = 0;

        /// <summary>
        /// ��ʼʱ����ļ������
        /// </summary>
        [Category("��ѯ����"), Description("Ĭ�ϵĲ�ѯ��ʼʱ����ļ������")]
        public int BeginDateSpanDay
        {
            get
            {
                return beginDateSpanDay;
            }
            set
            {
                beginDateSpanDay = value;
            }
        }

        /// <summary>
        /// Ĭ�ϵĲ�ѯ����ʱ����ļ������
        /// </summary>
        private int endDateSpanDay = 1;

        /// <summary>
        /// Ĭ�ϵĲ�ѯ����ʱ����ļ������
        /// </summary>
        [Category("��ѯ����"), Description("Ĭ�ϵĲ�ѯ����ʱ����ļ������")]
        public int EndDateSpanDay
        {
            get
            {
                return endDateSpanDay;
            }
            set
            {
                endDateSpanDay = value;
            }
        }

        /// <summary>
        /// ��ѯ��ʼʱ��
        /// </summary>
        string beginTime = "12:01:00";

        /// <summary>
        /// ��ѯ��ʼʱ��
        /// </summary>
        [Category("��ѯ����"), Description("Ĭ�ϵĲ�ѯ����ʱ��,�磺12:01:00")]
        public string BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            Init();

        }


        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            ResetPanel();

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            this.dateTimePicker1.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 00, 00, 00);
            this.dateTimePicker2.Value = new DateTime(dtNow.AddDays(1).Year, dtNow.AddDays(1).Month, dtNow.AddDays(1).Day, 12, 01, 00);
            try
            {
                if (!string.IsNullOrEmpty(beginTime))
                {
                    DateTime dtBegin = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(beginDateSpanDay).ToString("yyyy.MM.dd") + " " + beginTime);
                    this.dateTimePicker1.Value = dtBegin;
                }
                if (!string.IsNullOrEmpty(endTime))
                {
                    DateTime dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(endDateSpanDay).ToString("yyyy.MM.dd") + " " + endTime);
                    this.dateTimePicker2.Value = dtEnd;
                }
            }
            catch
            { }
        }

        public int Retrieve()
        {
            // TODO:  ��� ucDrugCardPanel.Retrieve ʵ��
            if (this.tabControl1.TabPages.Count <= 0)
                return 0;
            FS.FrameWork.Models.NeuObject obj = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag);
            string BillNo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).ID;
            //this.IsRePrint = false;
            this.Memo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).User01;
            this.Query(BillNo);
            return 0;
        }

        private void Query(string billNo)
        {
            if (this.tabControl1.TabPages.Count <= 0)
            {
                MessageBox.Show("��ά���ӿ�");
                return;
            }
            if (this.myPatients == null || this.myPatients.Count == 0)
            {
                MessageBox.Show("û��ѡ������Ϣ��");
                return;
            }

            IsRePrint = this.chkRePrint.Checked;
            IsFirst = this.chkFirst.Checked;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯִ�е���Ϣ...");
            Application.DoEvents();

            if (this.tabControl1.SelectedTab.Controls[0].Controls.Count == 0)
            {
                //��ǰTabҳ���滹û����Һ��
                object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucExecBill), typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion));
                if (o == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("��ά��HISFC.Components.Order.Controls.ucExecBill����ӿ�FS.HISFC.BizProcess.Integrate.IPrintTransFusion��ʵ�����գ�");
                    return;
                }

                ip = o as FS.HISFC.BizProcess.Interface.IPrintTransFusion;

                ((Control)o).Tag = tabControl1.SelectedTab.Text;
                ((Control)o).Visible = true;
                ((Control)o).Dock = DockStyle.Fill;
                this.tabControl1.SelectedTab.Controls[0].Controls.Add((Control)o);
            }
            else
            {
                ip = this.tabControl1.SelectedTab.Controls[0].Controls[0] as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
            }

            if (ip == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("ά����ʵ�����߱�FS.HISFC.BizProcess.Integrate.IPrintTransFusion�ӿ�");
                return;
            }

            try
            {
                //����ע����userCode��
                this.myPatients[0].UserCode = this.Memo;

                string orderType = "ALL";

                if (this.rbtAll.Checked)
                    orderType = "ALL";

                if (this.rbtLong.Checked)
                    orderType = "1";

                if (this.rbtShort.Checked)
                    orderType = "0";

                ip.DCIsPrint = this.dcIsPrint;
                ip.NoFeeIsPrint = this.noFeeIsPrint;
                ip.QuitFeeIsPrint = this.quitFeeIsprint;
                ip.SetSpeOrderType(this.speOrderType);

                ip.Query(this.myPatients, billNo, this.dateTimePicker1.Value, this.dateTimePicker2.Value, this.IsRePrint, orderType, IsFirst);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        public void ResetPanel()
        {
            ArrayList alBill = new ArrayList();

            try
            {
                //���ִ�е�����
                alBill = Bill.QueryExecBill(CacheManager.LogEmpl.Nurse.ID);
            }
            catch { MessageBox.Show("���ִ�е��������"); }

            if (alBill == null)
            {
                MessageBox.Show("���ִ�е����ó���");
                return;
            }
            this.tabControl1.TabPages.Clear();

            for (int i = 0; i < alBill.Count; i++)
            {
                TabPage t = new TabPage();
                t.Text = ((FS.FrameWork.Models.NeuObject)alBill[i]).Name;
                
                t.Tag = alBill[i];
                Panel p = new Panel();
                p.AutoScroll = true;
                p.Dock = DockStyle.Fill;
                p.BackColor = Color.White;

                t.Controls.Add(p);

                this.tabControl1.TabPages.Add(t);
            }


        }

        /// <summary>
        /// ����ִ�е�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //frmSetExecBill f = new frmSetExecBill(FS.Common.Class.Main.var);
            //f.ShowDialog();
            this.ResetPanel();
        }

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.myPatients != null && this.myPatients.Count > 0 && this.tabControl1.TabPages.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾִ�е���Ϣ�����Ժ�........");
                Application.DoEvents();
                string BillNo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).ID;
                this.IsRePrint = false;

                this.Memo = ((FS.FrameWork.Models.NeuObject)this.tabControl1.SelectedTab.Tag).User01;
                this.Query(BillNo);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ����仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.Retrieve();
            }
            catch
            {
                MessageBox.Show("���ȵ��ѯ��ť���в�ѯ��");
            }
        }

        #region ��ִֹ�е��ĸ�ѡ���ں�ҽ����ѯ���ص������ʾ��ʧ�� 20100916

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService  OnInit(object sender, object neuObject, object param)
        {
            TreeView tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false) tv.CheckBoxes = true;
            this.ResetPanel();

            this.chkFirst.Visible = this.isShowFirstDay;

            return null;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        #endregion

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alValues)
            {
                myPatients.Add(p);
            }
            this.Retrieve();
            return 0;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
           // return this.Retrieve();
            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            if (ip != null)
                ip.Print();
            return 0;
        }

        /// <summary>
        /// ���ô�ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int SetPrint(object sender, object neuObject)
        {
            if (ip != null)
                ip.PrintSet();
            return 0;
        }

         #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get {
                 Type[]  type = new Type[1];
                 type[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion);
                return type;
            }
        }

        #endregion

    }
}
