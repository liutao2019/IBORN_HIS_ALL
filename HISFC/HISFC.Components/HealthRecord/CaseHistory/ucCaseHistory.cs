using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    /// <summary>
    /// ��������ϵͳ
    /// </summary>
    public partial class ucCaseHistory : FS.FrameWork.WinForms.Controls.ucBaseControl//, FS.NFC.Interface.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucCaseHistory()
        {
            InitializeComponent();
            this.neuComboBox1.SelectedIndex = 0;
            this.neuFpEnter1_Sheet1.RowCount = this.neuFpEnter2_Sheet1.RowCount = 0;
            this.neuTabControl1.SelectedIndex = 1;
            this.neuFpEnter1_Sheet1.Columns[1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuFpEnter2_Sheet1.Columns[1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.neuFpEnter1_Sheet1.GrayAreaBackColor = Color.White;
            this.neuFpEnter2_Sheet1.GrayAreaBackColor = Color.White;

            this.neuTextBoxIsNotCBPatientNO.KeyDown += new KeyEventHandler(neuTextBoxIsNotCBPatientNO_KeyDown);
            this.neuTextBoxPatientNO.KeyDown += new KeyEventHandler(neuTextBoxPatientNO_KeyDown);

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            alDept = deptMgr.GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType.I);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "ȫ��";
            obj.Memo = "";
            alDept.Add(obj);
            if (alDept != null)
            {
                this.neuCmbDept.AddItems(alDept);
                this.neuCmbDept1.AddItems(alDept);
            }
            this.neuCmbDept.SelectedIndex = alDept.Count - 1;
            this.neuCmbDept1.SelectedIndex = alDept.Count - 1;

            this.neuCmbModifyStyle.SelectedIndex = 0; //�����������ڸ������� �ǰ����������Ļ��ǰ������ڸ���  Ĭ�ϰ�����������
            bool b = this.neuCmbModifyStyle.SelectedIndex == 0 ? (this.neuDtModifier.Visible =
                      !(this.ucAdvanceDays1.Visible = false)) : (this.neuDtModifier.Visible = !(this.ucAdvanceDays1.Visible = true));
            

        }

        #region ˽�б���&��������

        ArrayList alDept = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// ���ݿؼ��Ĵ���
        /// </summary>
        MyForm form = new MyForm();
        /// <summary>
        /// �����ʿؼ�
        /// </summary>
        ucCaseCallbackPercent caseCallbackPercent = new ucCaseCallbackPercent();
        /// <summary>
        /// ��ʱ������ϸ��Ϣ�ؼ�
        /// </summary>
        ucTimeoutCase timeoutCase = new ucTimeoutCase();
        /// <summary>
        /// ����������ѯ�ؼ�
        /// </summary>
        ucCallBackNum callbackNum = new ucCallBackNum();

        //TreeView tv = null;
        /// <summary>
        /// ��������ʵ��
        /// </summary>
        FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack = new FS.HISFC.Models.HealthRecord.CaseHistory.CallBack();

        /// <summary>
        /// �������չ�����
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack callBackMgr = null;

        private bool ISCaseCallBackLimits = true;//����Ȩ�� ͨ����������Ȩ����Ա������CaseCallBackLimits���� ��ʱ�Ȱ������ַ�ʽ���� 2011-6-27 chengym
        /// <summary>
        /// ��������
        /// </summary>
        Form listForm = null;

        ListView lv = null;

        string specifyDept ="";
        /// <summary>
        /// ������������Ŀ������� ,����
        /// </summary>
        [Category("������տ���"), Description("һ�����Ϊ5&7����� �������Ϊ�������")]
        public string SpecifyDept
        {
            get { return specifyDept; }
            set { specifyDept = value; }
        }

        bool isNeedAuthority = false;
        /// <summary>
        /// ���÷���Ȩ�޿���  trueΪ�� ����CaseCallBackLimits�����з���Ȩ
        /// </summary>
        [Category("���÷���Ȩ�޿���"), Description("trueΪ����falseΪ�� ������Ҫά������CaseCallBackLimits����")]
        public bool IsNeedAuthority
        {
            get { return this.isNeedAuthority; }
            set { this.isNeedAuthority = value; }
        }
        DateTime dtBeginUse = new DateTime(2011,1,1);
        /// <summary>
        /// ���չ��ܿ�ʼʹ��ʱ��
        /// </summary>
        [Category("���ÿ�ʼʹ��ʱ�俪��"), Description("��ʼʹ�õ�ʱ�䣬����δ���ղ�ѯ��ʼ����")]
        public DateTime DtBeginUse
        {
            get { return this.dtBeginUse; }
            set { this.dtBeginUse = value; }
        }

        int firTimeOut = 5;
        /// <summary>
        ///��С��ʱ����
        /// </summary>
        [Category("��С��ʱ����"), Description("һ�����Ϊ5�����")]
        public int FirTimeOut
        {
            get { return firTimeOut; }
            set { firTimeOut = value; }
        }

        int deaTimeOut = 7;
        /// <summary>
        ///��С��ʱ����
        /// </summary>
        [Category("������Ժ��С��ʱ����"), Description("һ�����Ϊ7�����")]
        public int DeaTimeOut
        {
            get { return deaTimeOut; }
            set { deaTimeOut = value; }
        }

        int secTimeOut = 8;
        /// <summary>
        ///���ʱ����
        /// </summary>
        [Category("���ʱ����"), Description("һ�����Ϊ8�����")]
        public int SecTimeOut
        {
            get { return secTimeOut; }
            set { secTimeOut = value; }
        }
        /// <summary>
        /// ����״̬ö��
        /// </summary>
        public enum CaseType
        {
            /// <summary>
            /// δ����
            /// </summary>
            UnCallBack=0,
            /// <summary>
            /// �ѻ���
            /// </summary>
            CallBack =1
        }
        #endregion 

        #region ����� ��ʱδ��
        //protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        //{
        //    this.tv = sender as TreeView;
        //    return base.OnInit(sender, neuObject, param);
        //}
        #endregion
        /// <summary>
        /// load��ʼ������
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            callBackMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack(specifyDept,firTimeOut,deaTimeOut,secTimeOut);
            if (this.isNeedAuthority)
            {
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Models.NeuObject obj = con.GetConstant("CaseCallBackLimits", con.Operator.ID);
                if (obj.ID == "")
                {
                    MessageBox.Show("���޷��䡰�������ա�Ȩ�ޣ�", "��ʾ-�������� �뼯��ƽ̨���� ������CaseCallBackLimits����¼");
                    this.neuTabControl1.Enabled = false;
                    this.ISCaseCallBackLimits = false;
                    return;
                }
            }
            base.OnLoad(e);
        }

        #region ��������������
        /// <summary>
        /// ���ݲ�������ʵ����Ϣ ��ʼ��fp
        /// </summary>
        /// <param name="cb"></param>
        private void InitFp(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb, FarPoint.Win.Spread.SheetView sheetView)
        {
            if (cb == null || cb.Patient.ID == "")
            {
                return;
            }

            sheetView.RowCount++;
            sheetView.Cells[sheetView.RowCount - 1, 0].Text = cb.Patient.ID;
            sheetView.Cells[sheetView.RowCount - 1, 1].Text = cb.Patient.PID.PatientNO.TrimStart('0');
            sheetView.Cells[sheetView.RowCount - 1, 2].Text = cb.Patient.Name;
            sheetView.Cells[sheetView.RowCount - 1, 3].Text = cb.Patient.PVisit.PatientLocation.Dept.ID;
            sheetView.Cells[sheetView.RowCount - 1, 4].Text = cb.Patient.PVisit.PatientLocation.Bed.ID;
            sheetView.Cells[sheetView.RowCount - 1, 5].Text = cb.Patient.PVisit.PatientLocation.Dept.Name;
            sheetView.Cells[sheetView.RowCount - 1, 6].Text = cb.Patient.PVisit.AdmittingDoctor.ID;
            sheetView.Cells[sheetView.RowCount - 1, 7].Text = cb.Patient.PVisit.AdmittingDoctor.Name;
            sheetView.Cells[sheetView.RowCount - 1, 8].Text = cb.Patient.PVisit.OutTime.ToShortDateString();
            sheetView.Cells[sheetView.RowCount - 1, 9].Text = cb.IsCallback == "0" ? "δ����" : "�ѻ���";
            sheetView.Cells[sheetView.RowCount - 1, 10].Text = cb.CallbackOper.ID;
            sheetView.Cells[sheetView.RowCount - 1, 11].Text = cb.CallbackOper.Name;
            if (cb.IsCallback == "1")
            {
                sheetView.Cells[sheetView.RowCount - 1, 12].Text = cb.CallbackOper.OperTime.ToShortDateString();
            }
            else
            {
                sheetView.Cells[sheetView.RowCount - 1, 12].Text = "";
            }

            sheetView.Rows[sheetView.RowCount - 1].Tag = cb;
            #region ����Ĭ����ʾΪ���һ��
            sheetView.ActiveRowIndex = sheetView.RowCount - 1;

            if (sheetView == this.neuFpEnter1_Sheet1)
            {
                this.neuFpEnter1.ShowRow(0, this.neuFpEnter1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            else
            {
                this.neuFpEnter2.ShowRow(0, this.neuFpEnter2_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            #endregion
        }


        /// <summary>
        /// �жϵ�ǰѡ���Ǹ�fp 
        /// </summary>
        /// <param name="selectTabText"></param>
        /// <returns></returns>
        private FarPoint.Win.Spread.SheetView JudgeSelectedFp(string selectTabText)
        {
            FarPoint.Win.Spread.SheetView sheetView = null;// new FarPoint.Win.Spread.SheetView();
            FarPoint.Win.Spread.SheetView[] sv = new FarPoint.Win.Spread.SheetView[2]
                                                {
                                                    this.neuFpEnter1_Sheet1, this.neuFpEnter2_Sheet1
                                                };
            sheetView = (selectTabText == "δ���ղ���") ? sv[0] : sv[1];
            // sheetView.RemoveRows(0, sheetView.RowCount);
            //fp��������ֵΪ0
            //sheetView.RowCount = 0;

            sheetView.Columns[3].Visible = false;
            sheetView.Columns[0].Visible = false;
            //sheetView.Columns[4].Visible = false;
            sheetView.Columns[6].Visible = false;
            sheetView.Columns[10].Visible = false;

            return sheetView;
        }

        /// <summary>
        /// �ۺϲ�ѯ�Ѿ����ջ���δ����֮����
        /// </summary>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="isOrNotCallBack">�Ƿ����</param>
        private void QueryIsOrNotCallBack(DateTime begin, DateTime end, string isOrNotCallBack)
        {
            FarPoint.Win.Spread.SheetView sheetView = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);
            //�ж��ǻ��տƿ��� ����δ���յĿ���
            string deptCode = isOrNotCallBack == "0" ? this.neuCmbDept.SelectedItem.ID.ToString() : this.neuCmbDept1.SelectedItem.ID.ToString();
            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> listCallBack = callBackMgr.QueryCaseHistoryCallBackInfo(isOrNotCallBack, begin, end, deptCode);
            if (listCallBack == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb in listCallBack)
            {
                //if (!this.ht.Contains(cb.Patient.ID))
                //{
                //    this.ht.Add(cb.Patient.ID, cb.Patient.Name);
                //}
                //else
                //{
                //    MessageBox.Show("סԺ��" + this.callBack.Patient.PID.PatientNO + "���ڽ�������ʾ"); 
                //}
                this.InitFp(cb, sheetView);
            }

        }

        /// <summary>
        /// ������ѯδ���ղ���ʱʹ��
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void QueryIsOrNotCallBack(DateTime begin, DateTime end)
        {
            FarPoint.Win.Spread.SheetView sheetView = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);

            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> listCallBack = callBackMgr.QueryCaseHistoryCallBackInfo(begin, end, this.neuCmbDept1.SelectedItem.ID.ToString());
            if (listCallBack == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb in listCallBack)
            {
                this.InitFp(cb, sheetView);
            }

        }

        /// <summary>
        /// ����סԺ�Ų�ѯ�������߲���������Ϣ
        /// </summary>
        private void QueryIsOrNotCallBackByPatientNO(string patientNO,CaseType caseType)
        {
            FarPoint.Win.Spread.SheetView sv = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);
            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> cb = null;
            if (caseType == CaseType.CallBack)
            {
                cb = callBackMgr.QueryCaseHistorycallBackInfoByPatientNO(patientNO.TrimStart('0').PadLeft(10,'0'),"1");
            }
            else
            {
                cb = callBackMgr.QueryCaseHistorycallBackInfoByPatientNO(patientNO.TrimStart('0').PadLeft(10, '0'), "0");
            }
            
            if (cb == null || cb.Count == 0)
            {
                if (caseType == CaseType.UnCallBack)
                {
                    MessageBox.Show("δ���ҵ�סԺ��Ϊ��" + patientNO + "���Ļ��ߵ���Ϣ");
                }
                else
                {
                    MessageBox.Show("סԺ��Ϊ��" + patientNO + "���Ļ��߲���δ����");
                }
                return;
            }
            //���߶��סԺ �����Ի���ѡ�� ������ʱû�п����ܷ��ڽ�������ʾ����ͬ����סԺ��ˮ��
            else if (cb.Count > 1)
            {
                this.callBack = null;
                this.SelectListBox(cb);
                if (this.callBack == null)
                {
                    MessageBox.Show("û�в��ҵ�������Ϣ");
                    return;
                }

                this.InitFp(this.callBack, sv);
                return;
            }
            this.callBack = cb[0];
            //if (!this.ht.Contains(this.callBack.Patient.ID))
            //{
            //    this.ht.Add(this.callBack.Patient.ID, this.callBack.Name);
            //}
            //else
            //{
            //    MessageBox.Show("סԺ��Ϊ" + this.callBack.Patient.PID.PatientNO + "�Ĳ������ڽ�������ʾ");
            //    return;
            //}
            //���Ʋ����ѻ��ղ���������δ���ղ�������ʾ ��֮���������ѻ�������ʾ
            if (this.callBack.IsCallback == "0")
            {
                if (this.neuTabControl1.SelectedTab.Text == "�ѻ��ղ���")
                {
                    MessageBox.Show("סԺ��Ϊ" +this.callBack.Patient.PID.PatientNO +"�Ĳ�����δ����");
                    return;
                }
            }
            else
            {
                if(this.neuTabControl1.SelectedTab.Text == "δ���ղ���")
                {
                    MessageBox.Show("סԺ��Ϊ" + this.callBack.Patient.PID.PatientNO +"�Ĳ����Ѿ�����");
                    return;
                }
            }

            this.InitFp(cb[0], sv);
        }


        /// <summary>
        /// ����ѯ��һ�����߶��סԺʱ ����С���幩�û�ѡ��
        /// </summary>
        /// <param name="cb"></param>
        private void SelectListBox(List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> cb)
        {
            listForm = new Form();
            //listBox = new ListBox();
            lv = new ListView();
            lv.Dock = DockStyle.Fill;
            lv.GridLines = true; //�Ƿ��б����ʾ
            lv.MultiSelect = false;
            lv.HeaderStyle = ColumnHeaderStyle.Nonclickable; //�б����Ƿ����Ӧ�¼�
            lv.View = View.Details; //����ʾ��ʽ
            lv.Columns.Add("סԺ��", 80);
            lv.Columns.Add("����", 90);
            lv.Columns.Add("��Ժ����", 120);
            listForm.Size = new Size(300, 200);
            listForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack in cb)
            {
                //ÿһ��Ϊһ��CallBackʵ��
                ListViewItem lvItem = new ListViewItem();
                lvItem.SubItems.Clear();
                lvItem.SubItems[0].Text =callBack.Patient.PID.PatientNO;
                lvItem.SubItems[0].ForeColor = Color.Blue;
                lvItem.SubItems.Add(callBack.Name);
                lvItem.SubItems.Add(callBack.Patient.PVisit.OutTime.ToString("yyyy��MM��dd��"));
                lvItem.Tag = callBack;
                lv.Items.Add(lvItem);
            }
            //listBox.Visible = true;
            //listBox.DoubleClick += new EventHandler(listBox_DoubleClick);  //������δʵ��
            //listBox.Show();
            lv.DoubleClick += new EventHandler(lv_DoubleClick);
            lv.KeyDown += new KeyEventHandler(lv_KeyDown);
            listForm.Controls.Add(lv);
            listForm.TopMost = true;
            //listForm.Show();
            listForm.ShowDialog();
            if (this.neuTabControl1.SelectedTab.Text == "δ���ղ���")
            {
                listForm.Location = this.neuTextBoxIsNotCBPatientNO.PointToScreen(new Point(this.neuTextBoxIsNotCBPatientNO.Width / 2 +
                            this.neuTextBoxIsNotCBPatientNO.Left, this.neuTextBoxIsNotCBPatientNO.Height + this.neuTextBoxIsNotCBPatientNO.Top));
            }
            else
            {
                listForm.Location = this.neuTextBoxPatientNO.PointToScreen(new Point(this.neuTextBoxPatientNO.Width / 2 +
                            this.neuTextBoxPatientNO.Left, this.neuTextBoxPatientNO.Height + this.neuTextBoxPatientNO.Top));
            }
            //Ĭ��ѡ�е�һ��

            
        }

        /// <summary>
        /// ˫�������Ļ��ߵ�������
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void lv_DoubleClick(object o, EventArgs e)
        {
            this.callBack = this.lv.FocusedItem.Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
            this.listForm.Hide();
            this.listForm.Dispose();
        }


        private void lv_KeyDown(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.callBack = this.lv.FocusedItem.Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
                this.listForm.Hide();
                this.listForm.Dispose();
                Application.DoEvents();
            }
        }
        /// <summary>
        /// ���ղ������� ������ʾ�Ļ���ȫ������
        /// </summary>
        /// <param name="cb"></param>
        private int CallBackCaseHistory(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb)
        {
            //if (this.callBackMgr.UpdateCaseHistoryCallBackInfo(cb) < 0)
            //{
            //    MessageBox.Show("סԺ��ˮ��Ϊ" + cb.Patient.ID + "���ߵĲ�������ʧ��");
            //    return -1;
            //}
            if (this.callBackMgr.InsertCaseHistoryCallBackInfo(cb) < 0)
            {
                return -1;
            }
            return 0;
            //MessageBox.Show("�������ճɹ�");
        }

        /// <summary>
        /// ˢ�²�ѯ���Ĳ�����Ϣ ������ѯʱˢ�½���
        /// </summary>
        private void RefreshCaseHistory()
        {
            this.QueryIsOrNotCallBack(this.neudtIsNotFrom.Value, this.neudtIsNotTo.Value,
                this.neuTabControl1.SelectedTab.Text == "δ���ղ���" ? "0" : "1");
        }

        /// <summary>
        /// shuxin  dange huanzhe de shihou   ������ѯ��ʱ��ˢ�� ��Ϊ������ѯֻҪ����
        /// ����Ļ��ͻ��ȫ�ֵ�callBack��ֵ ���� �������callBackΪ�յĻ� �ͷ�����
        /// ����ˢ�½���
        /// </summary>
        private void RefreshCaseHistorySingle(string patientNO)
        {
            FarPoint.Win.Spread.SheetView sv = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);
            //FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack = callBackMgr.QueryCaseHistorycallBackInfoByInpatientNO(patientNO);

            if (this.callBack == null)
            {
                //MessageBox.Show("δ���ҵ�סԺ��ˮ��Ϊ" + this.neuTextBoxIsNotCBPatientNO.Text + "�Ļ��ߵ���Ϣ");
                return;
            }

            this.InitFp(this.callBack, sv);
        }

        /// <summary>
        /// ��ʾ��ѯ����ؼ�
        /// </summary>
        /// <param name="c">����ؼ�</param>
        private int ShowControl(Control c)
        {
            //add 2011-6-27 by chengym  Ȩ��֮�Ƶ��޸�
            if (ISCaseCallBackLimits == false)
            {
                MessageBox.Show("���޷��䡰�������ա�Ȩ�ޣ�", "��ʾ-�������� ��ɾ�� ������CaseCallBackLimits����¼");
                return -1;
            }
            // end 
            try
            {
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                if (form.Controls.Count > 0)
                {
                    form.Controls.Clear();
                }
                if (!form.Controls.Contains(c))
                {
                    form.Controls.Add(c);
                }
                form.Size = new Size(c.Width +10, c.Height + 50);
                //form.TopMost = true;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.LocationChanged += new EventHandler(form_LocationChanged);
                form.DragDrop +=new DragEventHandler(form_DragDrop);
                //form.Opacity = 0;
                form.Show();
                //for (double i = 0; i <= 1; i += 0.001)
                //{
                //    form.Opacity = i;
                //    Application.DoEvents();
                //}
                //form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("�ؼ����ش���:" + c.Name + ex.Message.ToString());
                return -1;
            }

            return 0;
        }


        /// <summary>
        /// ���������ʲ�ѯ
        /// </summary>
        private void OnQueryCaseCallbackPercent()
        {

            this.ShowControl(caseCallbackPercent);
        }

        private void OnQueryTimeoutCaseInfo()
        {
            this.ShowControl(timeoutCase);
        }

        private void OnQueryCallbackNum()
        {
            this.ShowControl(callbackNum);
        }

        private void OnCallDateModifier()
        {
            //this.ShowControl(ucModifier);
        }

        private void OnCallDateModifierBatch()
        {
            //this.ShowControl(ucModifierBatch);
        }
        #endregion

        #region �¼�

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("������", "����������",FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            toolBarService.AddToolButton("��ʱ������ϸ", "��ʱ���յĲ�������ϸ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            toolBarService.AddToolButton("����������ѯ", "���տ��Һͳ�Ժʱ���ѯ��������", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            toolBarService.AddToolButton("���Ļ���ʱ��", "���Ļ���ʱ��",FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("�������Ļ���ʱ��", "�������Ļ���ʱ��", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("�鲡��״̬", "�鲡��״̬", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);//add by chengym 2011-6-24

            //return base.OnInit(sender, neuObject, param);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "������":
                    this.OnQueryCaseCallbackPercent();
                    break;
                case "��ʱ������ϸ":
                    this.OnQueryTimeoutCaseInfo();
                    break;
                case "����������ѯ":
                    this.OnQueryCallbackNum();
                    break;
                case "���Ļ���ʱ��":
                    this.OnCallDateModifier();
                    break;
                case "�������Ļ���ʱ��":
                    this.OnCallDateModifierBatch();
                    break;
                case "�鲡��״̬":
                    this.QueryCaseStoreAndCallBackState();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void form_LocationChanged(object sender, EventArgs e)
        {
            //form.Opacity = 0.5;
        }

        private void form_DragDrop(object sender, DragEventArgs e)
        {
            //form.Opacity = 1;
        }

        /// <summary>
        /// δ���ղ�����ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuIsNotQuery_Click(object sender, EventArgs e)
        {
            //this.ht.Clear();
            this.neuFpEnter1_Sheet1.RowCount = 0;
            if (this.neudtIsNotFrom.Value.Date < this.dtBeginUse)
            {
                if (MessageBox.Show("��ѯ��ʼ����Ӧ�ô��ڵ���ϵͳģ��ʹ������" + this.dtBeginUse.ToShortDateString() + "�������ѯ�������Ϊδ����״̬���Ƿ������ѯ", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            this.QueryIsOrNotCallBack(this.neudtIsNotFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neudtIsNotTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59), "0");
            this.neuLblNoCallback.Text = "δ����������" + this.neuFpEnter1_Sheet1.RowCount;
        }

        /// <summary>
        /// ���ղ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnCallBack_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ȷʵ���ս�������ʾ�����в�����ѡ���ǡ�������������ʾ�Ĳ���.", "����", 
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }
            callBackMgr.ArraySpecifyDept = this.specifyDept;

            DateTime dtTemp = this.neuDtCallBackDate.Value;
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            callBackMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
            {
                callBack = this.neuFpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
                callBack.IsCallback = "1";//����״̬ 1�ѻ��� 0 δ���� 
                callBack.CallbackOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;//������
                callBack.CallbackOper.OperTime = dtTemp; //��������
                callBack.IsDocument = "0";//�鵵״̬
                callBack.DocumentOper.ID = "";//�鵵��
                callBack.DocumentOper.OperTime = System.DateTime.MinValue;//�鵵����
                if (this.CallBackCaseHistory(callBack) < 0)
                {
                    if (this.callBackMgr.UpdateCaseHistoryCallBackInfo(callBack) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("סԺ��ˮ��Ϊ" + callBack.Patient.ID + "���ߵĲ�������ʧ��");
                        return;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�������ճɹ�");
            this.neuFpEnter1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// �Ѿ����ղ�����ѯ ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnQuery_Click(object sender, EventArgs e)
        {
            this.neuFpEnter2_Sheet1.RowCount = 0;
            //��Ժ����
            if (this.neuComboBox1.SelectedIndex == 0)
            {
                this.QueryIsOrNotCallBack(this.neuDtIsFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neudtIsTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59), "1");
            }
            else if (this.neuComboBox1.SelectedIndex == 1)
            {
                this.QueryIsOrNotCallBack(this.neuDtIsFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neudtIsTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
            }
            else
            {
                MessageBox.Show("��δѡ���κ�һ�ֲ�ѯ������");
            }
            this.neuLblIsCallback.Text = "�ѻ���������" + this.neuFpEnter2_Sheet1.RowCount;
            
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ȷʵ�������ս�������ʾ�����в�����ѡ���ǡ������в�����Ϊδ����״̬.", "��������", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }

            if (this.neuFpEnter2_Sheet1.RowCount <= 0)
            {
                return;
            }

            string inpatientNO;// = this.neuFpEnter2_Sheet1.Cells[this.neuFpEnter2_Sheet1.ActiveRowIndex, 0].Text.ToString();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            callBackMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuFpEnter2_Sheet1.RowCount; i++)
            {
                inpatientNO = this.neuFpEnter2_Sheet1.Cells[i, 0].Text.ToString();
                if (this.callBackMgr.CancelCaseHistoryCallBackInfo(inpatientNO) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("סԺ��ˮ��Ϊ" + inpatientNO + "���ߵĲ������ճ���ʧ��");
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�������ճ����ɹ�");
            //this.ht.Clear();
            this.neuFpEnter2_Sheet1.RowCount = 0;
            //this.RefreshCaseHistory();
        }

        /// <summary>
        /// ����סԺ�ŵ�����ѯ �ѻ��ղ�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnSingleQuery_Click(object sender, EventArgs e)
        {
            this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxPatientNO.Text,CaseType.CallBack);
            //this.RefreshCaseHistorySingle(this.neuTextBoxPatientNO.Text);
        }

        /// <summary>
        /// �س� ��ѯ����δ���ղ�����Ϣ 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBoxIsNotCBPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxIsNotCBPatientNO.Text,CaseType.UnCallBack);
                //this.RefreshCaseHistorySingle(this.neuTextBoxIsNotCBPatientNO.Text);
                this.neuTextBoxIsNotCBPatientNO.Text = "";
            }

        }
        /// <summary>
        /// �س� ��ѯ�����ѻ��ղ�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBoxPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxPatientNO.Text,CaseType.CallBack);
                //this.RefreshCaseHistorySingle(this.neuTextBoxIsNotCBPatientNO.Text);
                this.neuTextBoxIsNotCBPatientNO.Text = "";
            }
        }

        /// <summary>
        /// �������˲���������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void neuTextBoxPatientNO_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxPatientNO.Text);
        //       // this.RefreshCaseHistorySingle(this.neuTextBoxPatientNO.Text);
        //    }
        //}

        /// <summary>
        /// ��ӡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedTab.Text == "δ���ղ���")
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.PrintPage(this.neuPanel2.Left, this.neuPanel2.Top, this.neuPanel2);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.PrintPage(this.neuPanel3.Left, this.neuPanel3.Top, this.neuPanel3);
            }
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// ���������ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("��ȷʵ�����������ʾ��������", "����", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }

            this.neuFpEnter1_Sheet1.RowCount = 0;
            //this.ht.Clear();

        }

        private void neuBtnClearN_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("��ȷʵ�����������ʾ��������", "����", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }
            this.neuFpEnter2_Sheet1.RowCount = 0;
            //this.ht.Clear();
        }

        private void neuFpEnter1_CellDoubleClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true || e.RowHeader == true)
            {
                return;
            }

            int rowNum = e.Row + 1;
            DialogResult dr = MessageBox.Show("�Ƿ�ɾ����" + rowNum + "�е�����", "ɾ��", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }
            //this.ht.Remove(this.neuFpEnter1_Sheet1.Cells[e.Row, 0].Text.ToString().Trim());
            this.neuFpEnter1_Sheet1.RemoveRows(e.Row, 1);
            
            //this.neuFpEnter1_Sheet1.RowCount--;
        }

        private void neuFpEnter2_CellDoubleClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true || e.RowHeader == true)
            {
                return;
            }

            int rowNum = e.Row + 1;
            DialogResult dr = MessageBox.Show("�Ƿ������յ�" + rowNum + "�е�����", "��������", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }
            string patientNO = this.neuFpEnter2_Sheet1.Cells[e.Row, 0].Text.ToString().Trim();
            if (!string.IsNullOrEmpty(patientNO))
            {
                if (this.callBackMgr.CancelCaseHistoryCallBackInfo(patientNO) < 0)
                {
                    MessageBox.Show("��������ʧ�ܣ�");
                    return;
                }
            }
            else
            {
                return;
            }
            //this.ht.Remove(this.neuFpEnter2_Sheet1.Cells[e.Row, 0].Text.ToString().Trim());
            this.neuFpEnter2_Sheet1.RemoveRows(e.Row, 1);

            //this.neuFpEnter2_Sheet1.RowCount--;
        }
        #endregion

        /// <summary>
        /// ���Ĳ�������ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnModifier_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ȷʵ���Ľ�������ʾ�����в����Ļ���ʱ����", "���Ļ���ʱ��",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }

            if (this.neuFpEnter2_Sheet1.RowCount <= 0)
            {
                return;
            }


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            callBackMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuFpEnter2_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack = this.neuFpEnter2_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
                
                if (this.neuDtModifier.Visible)
                {
                    callBack.CallbackOper.OperTime = this.neuDtModifier.Value;
                }
                else if (this.ucAdvanceDays1.Visible)
                {
                    callBack.CallbackOper.OperTime = callBack.CallbackOper.OperTime.AddDays(-this.ucAdvanceDays1.AdvanceDays);
                }
                if (callBack.IsCallback == "0")
                {
                    callBack.IsCallback = "1";
                }
                if (this.callBackMgr.UpdateCaseHistoryCallBackInfo(callBack) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("סԺ��Ϊ" + callBack.Patient.PID.PatientNO + "���ߵĲ�������ʱ�����ʧ��");
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("��������ʱ����³ɹ�");
            this.neuFpEnter2_Sheet1.RowCount = 0;
        }

        private void neuCmbModifyStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.neuCmbModifyStyle.SelectedIndex == 0)
            //{
            //    this.ucAdvanceDays1.Visible = false;
            //    this.neuDtModifier.Visible = true;
            //}
            //else if(this.neuCmbModifyStyle.SelectedIndex == 1)
            //{
            //    this.ucAdvanceDays1.Visible = true;
            //}

            bool b = this.neuCmbModifyStyle.SelectedIndex == 0 ? (this.neuDtModifier.Visible = 
                !(this.ucAdvanceDays1.Visible = false)) : (this.neuDtModifier.Visible = !(this.ucAdvanceDays1.Visible = true));
        }

        private void QueryCaseStoreAndCallBackState()
        {
            //FS.HISFC.Components.HealthRecord.Case.frmCaseStoreQuery frm = new FS.HISFC.Components.HealthRecord.Case.frmCaseStoreQuery();
            //frm.ShowDialog();
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Title = "������Excel";
            saveFileDlg.CheckFileExists = false;
            saveFileDlg.CheckPathExists = true;
            saveFileDlg.DefaultExt = "*.xls";
            saveFileDlg.Filter = "(*.xls)|*.xls";

            DialogResult dr = saveFileDlg.ShowDialog();
            if (dr == DialogResult.Cancel || string.IsNullOrEmpty(saveFileDlg.FileName))
            {
                return;
            }

            neuFpEnter1.SaveExcel(saveFileDlg.FileName,FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
        }
    }


    /// <summary>
    /// ֮�������� Ŀ��������OnClosing���� ʹ֮������������
    /// </summary>
    class MyForm : Form
    {
        public MyForm()
        {
            this.Opacity = 1;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //e.Cancel = true;
            this.Hide();
            e.Cancel = true;
            base.OnClosing(e);
        }

    }

}
