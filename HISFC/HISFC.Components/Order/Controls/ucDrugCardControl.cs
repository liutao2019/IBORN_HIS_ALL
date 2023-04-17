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
    /// [��������: ��Һ���ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='�볬'
    ///		�޸�ʱ��='2010-10-12'
    ///		�޸�Ŀ��='ʵ�ֲ����÷���ʾ��Һ��'
    ///		�޸�����='��isSeprateUse�����ж��Ƿ���÷�'
    ///  />
    /// </summary>
    public partial class ucDrugCardControl : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucDrugCardControl()
        {
            InitializeComponent();
        }

        #region ����
        FS.HISFC.BizLogic.Order.TransFusion manager = new FS.HISFC.BizLogic.Order.TransFusion();

        /// <summary>
        /// ��ǰ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPrintTransFusion ip = null;

        /// <summary>
        /// �Ƿ��ش�
        /// </summary>
        bool isRePrint = true;

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        bool isFirst = false;

        /// <summary>
        /// ���ڴ洢�÷�
        /// </summary>
        ArrayList useList = null;

        /// <summary>
        /// ѡ�еĻ����б�
        /// </summary>
        protected List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;

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
        /// ���û�ʿվ����ҽ��������,���Ÿ��� ����:CONS ����:DEPTXXX ҽ��:DEPTXXX ����:OTHER"
        /// </summary>
        private string speOrderType = "";

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
        /// �Ƿ���÷� Ĭ�Ϸ��÷�
        /// </summary>
        private bool isSeprateUse = true;

        /// <summary>
        /// �Ƿ���÷���ʾ��Һ��
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���÷���ʾ��Һ�� true �� ��false ����")]
        public bool IsSeprateUse
        {
            get
            {
                return this.isSeprateUse;
            }
            set
            {
                this.isSeprateUse = value;
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

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            if (tv != null && tv.CheckBoxes == false)
            {
                tv.CheckBoxes = true;
            }

            try
            {
                DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                DateTime dt1 = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

                DateTime dt2 = new DateTime(dtNow.AddDays(1).Year, dtNow.AddDays(1).Month, dtNow.AddDays(1).Day, 12, 00, 00);
                try
                {
                    if (!string.IsNullOrEmpty(beginTime))
                    {
                        dt1 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(beginDateSpanDay).ToString("yyyy.MM.dd") + " " + beginTime);
                    }
                    if (!string.IsNullOrEmpty(endTime))
                    {
                        dt2 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(endDateSpanDay).ToString("yyyy.MM.dd") + " " + endTime);
                    }
                }
                catch
                { }

                this.dateTimePicker1.Value = dt1;
                this.dateTimePicker2.Value = dt2;
            }
            catch { }
            ResetPanel();

            this.chkFirst.Visible = this.isShowFirstDay;

            return null;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void ResetPanel()
        {
            ArrayList al = manager.QueryTransFusion(CacheManager.LogEmpl.Nurse.ID);
            if (al == null)
            {
                MessageBox.Show("�����Һ�����ó���");
                return;
            }
            this.neuTabControl1.TabPages.Clear();
            //{D5058DCE-E168-4732-B5F7-1541A87C2D82}�����÷�����TABҳ
            if (this.isSeprateUse)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    TabPage tp = new TabPage(SOC.HISFC.BizProcess.Cache.Common.GetUsageName(al[i].ToString()));
                    tp.Tag = SOC.HISFC.BizProcess.Cache.Common.GetUsage(al[i].ToString());
                    Panel p = new Panel();
                    p.AutoScroll = true;
                    p.Dock = DockStyle.Fill;
                    p.BackColor = Color.White;
                    tp.Controls.Add(p);
                    this.neuTabControl1.TabPages.Add(tp);
                }
            }
            else//{D5058DCE-E168-4732-B5F7-1541A87C2D82}�����÷��Ļ�������TABҳ
            {
                TabPage tp = new TabPage("��Һ��");
                tp.Tag = null;
                Panel p = new Panel();
                p.AutoScroll = true;
                p.Dock = DockStyle.Fill;
                p.BackColor = Color.White;
                tp.Controls.Add(p);
                this.neuTabControl1.TabPages.Add(tp);
                this.useList = new ArrayList(al);
            }
        }
       
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            //{D5058DCE-E168-4732-B5F7-1541A87C2D82}
            if (this.isSeprateUse)
            {
                if (this.neuTabControl1.TabPages.Count <= 0) return 0;
                string CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
                this.Query(CardCode);
            }
            else
            {
                string useFlag = string.Empty;
                if (this.useList != null)
                {
                    for (int i = 0; i < this.useList.Count; i++)
                    {
                        useFlag += "'" + this.useList[i].ToString() + "'" + ",";
                    }
                }
                if (useFlag.Length > 0)
                {
                    useFlag = useFlag.Substring(0, useFlag.Length - 1);
                    this.Query(useFlag);
                }
            }
            return 0;
        }

        private void Query(string usageCode)
        {
            if (this.neuTabControl1.TabPages.Count <= 0 || this.myPatients == null)
            {
                return;
            }

            isRePrint = this.chkRePrint.Checked;
            isFirst = this.chkFirst.Checked;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��Һ����Ϣ...");
            Application.DoEvents();

            if (this.neuTabControl1.SelectedTab.Controls[0].Controls.Count == 0)
            {
                //��ǰTabҳ���滹û����Һ��
                object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucDrugCardControl), typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion));
                if (o == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("��ά��HISFC.Components.Order.Controls.ucDrugCardControl����ӿ�FS.HISFC.BizProcess.Interface.IPrintTransFusion��ʵ�����գ�");
                    return;
                }
                this.ip = o as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
                ((Control)o).Visible = true;
                ((Control)o).Dock = DockStyle.Fill;
                this.neuTabControl1.SelectedTab.Controls[0].Controls.Add((Control)o);
            }

            ip = this.neuTabControl1.SelectedTab.Controls[0].Controls[0] as FS.HISFC.BizProcess.Interface.IPrintTransFusion;

            if (ip == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("ά����ʵ�����߱�FS.HISFC.BizProcess.Integrate.IPrintTransFusion�ӿ�");
                return;
            }

            try
            {
                string orderType = "ALL";

                if (this.rbtAll.Checked)
                {
                    orderType = "ALL";
                }

                if (this.rbtLong.Checked)
                {
                    orderType = "1";
                }

                if (this.rbtShort.Checked)
                {
                    orderType = "0";
                }

                ip.DCIsPrint = this.dcIsPrint;
                ip.NoFeeIsPrint = this.noFeeIsPrint;
                ip.QuitFeeIsPrint = this.quitFeeIsprint;
                ip.SetSpeOrderType(this.speOrderType);

                ip.Query(this.myPatients, usageCode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, isRePrint, orderType, isFirst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == null) return;
            string CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
            this.Query(CardCode);
        }

        #region ��д
       

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alValues)
            {
                myPatients.Add(p);
            }

            return 0;
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && this.tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.GetPatientList() == -1)
            {
                return -1;
            }
            return this.Retrieve();
        }

        private int GetPatientList()
        {
            try
            {
                ArrayList al = this.GetSelectedTreeNodes();
                if (al != null)
                {
                    this.myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                        {
                            myPatients.Add((FS.HISFC.Models.RADT.PatientInfo)obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myPatients = new List<FS.HISFC.Models.RADT.PatientInfo>();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
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
        #endregion

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forms.frmDrugCardSet f = new HISFC.Components.Order.Forms.frmDrugCardSet();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.ResetPanel();
            }
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Retrieve();
        }
    }
}
