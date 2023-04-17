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
    /// [��������: ��ƿ���ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///  />
    /// </summary>
    public partial class ucCircuitControl : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucCircuitControl()
        {
            InitializeComponent();
        }

        #region ����
        FS.HISFC.BizLogic.Order.TransFusion manager = new FS.HISFC.BizLogic.Order.TransFusion();
        FS.HISFC.BizProcess.Interface.IPrintTransFusion IPrintTransFusion = null;//��ǰ�ӿ�

        bool bPrint = true;

        /// <summary>
        /// �Ƿ���÷� Ĭ�Ϸ��÷�
        /// </summary>
        private bool isSeprateUse = true;

        #endregion

        #region ����

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
        /// �Ƿ����ӡ�������ӡ�Ļ���ʱ����ֻ��ѡ��һ�죩
        /// </summary>
        private bool isShowByDay = false;

        /// <summary>
        /// �Ƿ����ӡ�������ӡ�Ļ���ʱ����ֻ��ѡ��һ�죩
        /// </summary>
        [Category("��ѯ����"), Description("�Ƿ����ӡ�������ӡ�Ļ���ʱ����ֻ��ѡ��һ�죩")]
        public bool IsShowByDay
        {
            get
            {
                return isShowByDay;
            }
            set
            {
                isShowByDay = value;
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

        protected List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;

        /// <summary>
        /// �Ƿ���÷���ʾ
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
                if (value == null)
                    return;
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

        #region ����

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            TreeView tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false)
            {
                tv.CheckBoxes = true;
            }
            DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

            DateTime dt1 = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            DateTime dt2 = new DateTime(dtNow.AddDays(1).Year, dtNow.AddDays(1).Month, dtNow.AddDays(1).Day, 12, 00, 00);
            if (!string.IsNullOrEmpty(beginTime))
            {
                dt1 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(beginDateSpanDay).ToString("yyyy.MM.dd") + " " + beginTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                dt2 = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(endDateSpanDay).ToString("yyyy.MM.dd") + " " + endTime);
            }

            this.dateTimePicker1.Value = dt1;
            this.dateTimePicker2.Value = dt2;
            if (isShowByDay)
            {
                //lblDateAnd.Visible = false;
                //dateTimePicker2.Visible = false;

                //dateTimePicker2.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
                //dateTimePicker1.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
                //dateTimePicker1.CustomFormat = "yyyy��MM��dd��";
            }
            else
            {
                lblDateAnd.Visible = true;
                dateTimePicker2.Visible = true;
            }
            ResetPanel();

            this.cbxFirstOrder.Visible = this.isShowFirstDay;

            return null;
        }


        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        public void ResetPanel()
        {
            ArrayList alUsage = null;
            FS.FrameWork.Public.ObjectHelper helper = null;
            try
            {
                //ϵͳ�÷�
                alUsage = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE);
                helper = new FS.FrameWork.Public.ObjectHelper(alUsage);
            }
            catch
            {
                MessageBox.Show("����÷�����");
                return;
            }

            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            ArrayList al = manager.QueryTransFusion(empl.Nurse.ID);
            if (al == null)
            {
                MessageBox.Show("�����Һ�����ó���");
                return;
            }
            else if (al.Count == 0)
            {
                return;
            }

            this.neuTabControl1.TabPages.Clear();

            //�÷�����
            if (this.isSeprateUse)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    TabPage tp = new TabPage(helper.GetName(al[i].ToString()));
                    tp.Tag = helper.GetObjectFromID(al[i].ToString());
                    Panel p = new Panel();
                    p.AutoScroll = true;
                    p.Dock = DockStyle.Fill;
                    p.BackColor = Color.White;
                    tp.Controls.Add(p);
                    this.neuTabControl1.TabPages.Add(tp);
                }
            }
            else
            {
                TabPage tp = new TabPage("ȫ����Һ��");
                string useCode = "";
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                int i = 0;
                for (i = 0; i < al.Count - 1; i++)
                {
                    obj = helper.GetObjectFromID(al[i].ToString());
                    if (obj != null)
                    {
                        useCode += obj.ID + "','";
                    }
                }
                obj = helper.GetObjectFromID(al[i].ToString());
                useCode += obj.ID;

                tp.Tag = useCode;
                Panel p = new Panel();
                p.AutoScroll = true;
                p.Dock = DockStyle.Fill;
                p.BackColor = Color.White;
                tp.Controls.Add(p);
                this.neuTabControl1.TabPages.Add(tp);
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            // TODO:  ��� ucDrugCardPanel.Retrieve ʵ��
            if (this.neuTabControl1.TabPages.Count <= 0)
                return 0;

            string CardCode = "";
            if (this.isSeprateUse)
            {
                CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
            }
            else
            {
                CardCode = this.neuTabControl1.SelectedTab.Tag.ToString();
            }

            this.Query(CardCode);

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
            if (IPrintTransFusion != null)
                IPrintTransFusion.Print();
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
            if (IPrintTransFusion != null)
                IPrintTransFusion.PrintSet();
            return 0;
        }

        private void Query(string usageCode)
        {

            if (this.neuTabControl1.TabPages.Count <= 0 || this.myPatients == null || this.myPatients.Count == 0)
            {
                return;
            }

            bPrint = this.chkRePrint.Checked;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��Һ����Ϣ...");
            if (this.neuTabControl1.SelectedTab.Controls[0].Controls.Count == 0)
            {
                //��ǰTabҳ���滹û����Һ��
                object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucCircuitControl), typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion));
                if (o == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("��ά��HISFC.Components.Order.Controls.ucDrugCardControl����ӿ�FS.HISFC.BizProcess.Integrate.IPrintTransFusion��ʵ�����գ�");
                    return;
                }
                IPrintTransFusion = o as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
                ((Control)o).Visible = true;
                ((Control)o).Dock = DockStyle.Fill;
                this.neuTabControl1.SelectedTab.Controls[0].Controls.Add((Control)o);

            }
            else
            {
                IPrintTransFusion = this.neuTabControl1.SelectedTab.Controls[0].Controls[0] as FS.HISFC.BizProcess.Interface.IPrintTransFusion;
            }
            if (IPrintTransFusion == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("ά����ʵ�����߱�FS.HISFC.BizProcess.Integrate.IPrintTransFusion�ӿ�");
                return;
            }

            try
            {
                //user01Ϊ�Ƿ������� 1Ϊ����ҽ����0��
                //user02Ϊ��ҽ�����ͣ�allΪȫ����1Ϊ������0Ϊ����
                bool isFirst = this.cbxFirstOrder.Checked;
                string orderType = string.Empty;
                if (this.rbtShort.Checked)
                {
                    orderType = "0";
                }
                else if (this.rbtLong.Checked)
                {
                    orderType = "1";
                }
                else
                {
                    orderType = "ALL";
                }

                IPrintTransFusion.DCIsPrint = this.dcIsPrint;
                IPrintTransFusion.NoFeeIsPrint = this.noFeeIsPrint;
                IPrintTransFusion.QuitFeeIsPrint = this.quitFeeIsprint;
                IPrintTransFusion.SetSpeOrderType(this.speOrderType);

                IPrintTransFusion.Query(this.myPatients, usageCode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, bPrint, orderType, isFirst);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucCircuitControl.Query" + ex.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

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

        #endregion

        #region �¼�

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == null) return;
            string CardCode = ((FS.FrameWork.Models.NeuObject)this.neuTabControl1.SelectedTab.Tag).ID;
            this.Query(CardCode);
        }

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forms.frmDrugCardSet f = new HISFC.Components.Order.Forms.frmDrugCardSet();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.ResetPanel();
            }
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintTransFusion);
                return type;
            }
        }

        #endregion
    }
}
