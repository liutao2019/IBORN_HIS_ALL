using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.EPR
{
    /// <summary>
    /// ���Ӳ����ؼ�
    /// wolf 2007-7-23
    /// </summary>
    public partial class ucEMRControl : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer    {
        public ucEMRControl()
        {
            try
            {
                InitializeComponent();
                if (DesignMode) return;
                this.panelToolbar.Visible = false;
                this.panelModual.Visible = false;



                this.ucDataFileLoader1.InterfaceFileName = "\\interface.xml";
                   //TemplateDesignerHost.Function.SystemPath + "\\interface.xml";
                this.ucDataFileLoader1.PageChanged += new EventHandler(ucDataFileLoader1_PageChanged);
                this.ucDataFileLoader1.AfterSaved += new EventHandler(ucDataFileLoader1_AfterSaved);
                this.ucDataFileLoader1.BeforOpen += new EventHandler(ucDataFileLoader1_BeforOpen);
            }
            catch { }


        }

       

        #region ����

        protected FS.FrameWork.Models.NeuObject objGroup = new FS.FrameWork.Models.NeuObject();

        #endregion

        #region ����
        private bool isShowModual = false;
        private Color templateColor = Color.LightBlue;

        /// <summary>
        /// ģ����ɫ
        /// </summary>
        public Color TemplateColor
        {
            get { return templateColor; }
            set {
                templateColor = value;
                this.navigateBar1.NavigateBarColorTable.CaptionEnd = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾģ��
        /// </summary>
        public  bool IsShowModual
        {
            get { return isShowModual; }
            set {
                if (ucDataFileLoader1.ucTemplateSelect.Files == null)
                    return;
                isShowModual = value;
                if (this.panelModual.Controls.Count<=0)
                {
                    this.panelModual.RelatedControl = this.ucDataFileLoader1.ucTemplateSelect;
                    this.navigateBar1.SelectedButton = this.panelModual;
                }
                this.panelModual.Visible = value;
            }
        }

        private bool isShowToolFunction = false;

        /// <summary>
        /// �Ƿ���ʾ���߰�ť��
        /// </summary>
        public bool IsShowToolFunction
        {
            get { return isShowToolFunction; }
            set { isShowToolFunction = value;
            this.panelToolbar.Visible = isShowToolFunction;
            this.label1.Visible = !isShowToolFunction;

            }
        }

    

        private int currentType = 0;
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public int Type
        {
            get
            {
                return currentType;
            }
            set
            {
                currentType = value;
              
            }
        }
        private bool isShowInterface = false;

        public bool IsShowInterface
        {
            get { return isShowInterface; }
            set { isShowInterface = value; }
        }

        private string pageName = "EMR";

        public string PageName
        {
            get { return pageName; }
            set { pageName = value; }
        }

        private SQLType sqlType = SQLType.Inpatient;

        private bool isShowSendMessage = true;
        /// <summary>
        /// �Ƿ���ʾ���ʹ��󱨸�˵�
        /// </summary>
        [Description("�Ƿ���ʾ���ʹ��󱨸�˵�")]
        public bool IsShowSendMessage
        {
            get
            {
                return this.isShowSendMessage;
            }
            set
            {
                this.isShowSendMessage = value;
                this.mnuSendMessage.Visible = value;
            }
        }

        #endregion

        #region �ڲ�����
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //�¼�����
            this.ucDataFileLoader1.PageSelectedChanged += new EventHandler(ucDataFileLoader1_PageSelectedChanged);
            this.ucDataFileLoader1.ControlEnter += new EventHandler(ucDataFileLoader1_ControlEnter);
            this.navigateBar1.DisplayedButtonCount = 0;
            this.panelUserText.RelatedControl = this.ucUserText1;
            this.panelInfo.RelatedControl = this.ucUserCommonText1;
            this.btnEncap.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.F����);
            SearchPatient.SearchControl.Location = new Point(299, 112);//new Point((this.panelEMR.Width - SearchPatient.SearchControl.Width) / 2, (this.panelEMR.Height - SearchPatient.SearchControl.Height) / 2);
            this.panelEMR.Controls.Add(SearchPatient.SearchControl);
            if(this.curPatient == null)
                SearchPatient.SearchControl.Visible = true;
            NameChange();
            this.FindForm().FormClosing += new FormClosingEventHandler(ucEMRControl_FormClosing);
            SearchPatient.SearchControl.BringToFront();

        }
        /// <summary>
        /// ��������Ĳ�����ǰ����ϱ�־ 
        /// </summary>
        private void NameChange()
        {
            ArrayList lis = this.ucDataFileLoader1.Files;
            string InPatientNo = this.ucDataFileLoader1.index1;
            if (lis == null) return;
            //��Ҫ�޸ģ�Ӧ�ð�����סԺ��ˮ�Ź��ˣ�ȫ����Ӱ��Ч�ʣ����ݿ�Ҫ��������
            ArrayList Msglis = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryEmrId(InPatientNo); 

            if (Msglis == null) return;

            for (int i = 1; i < lis.Count; i++)
            {
                string emrid = ((FS.FrameWork.Models.NeuObject)this.ucDataFileLoader1.Files[i]).ID;

                foreach (FS.HISFC.Models.Base.Message message in Msglis)
                {
                    if (emrid == message.Emr.ID)
                    {
                        ucDataFileLoader1.ChangePageImage(emrid,true);
                        //c_NameChangedEvent(emrid, "[������]" + message.Emr.Name);
                    }
                }

            }
        }
        void ucEMRControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            //���ڹر�ʱ��
            if (AllowClosed() == false)
            {
                e.Cancel = true;
            }
            
        }

      
        void ucDataFileLoader1_PageSelectedChanged(object sender, EventArgs e)
        {
            try
            {	//��������
                this.ucUserText1.SetControl((System.ComponentModel.IContainer)sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            FS.FrameWork.WinForms.Forms.ToolBarService toolbar = new FS.FrameWork.WinForms.Forms.ToolBarService();
            toolbar.AddToolButton("ģ��", "��ʾģ��", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolbar.AddToolButton("����", "ѡ����", FS.FrameWork.WinForms.Classes.EnumImageList.G�˿�, true, false, null);
            toolbar.AddToolButton("����", "��ʾ����", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolbar.AddToolButton("��ʷ", "��ʾ��ʷ��¼", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            toolbar.AddToolButton("����", "��������¼", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            toolbar.AddToolButton("����", "��������", FS.FrameWork.WinForms.Classes.EnumImageList.R������, true, false, null);
            toolbar.AddToolButton("����", "������", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡԤ��, true, false, null);

            return toolbar;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ģ��")
            {
                this.IsShowModual = !this.IsShowModual;
            }
            else if (e.ClickedItem.Text == "����")
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                p.ID = "ZY010008802223";
                p.Name = "aaa";
                this.OnSetValue(p, null);
            }
            else if (e.ClickedItem.Text == "����")
            {
                this.navigateBar1.SelectedButton = this.panelUserText;
                
            }
            else if (e.ClickedItem.Text == "��ʷ")
            {
                this.ucDataFileLoader1.CurrentLoader.RefreshLogo();
            }
            else if (e.ClickedItem.Text == "����")
            {
                this.ManagerFile();
            }
            else if (e.ClickedItem.Text == "����")
            {
                this.SetFont();
            }
            else if (e.ClickedItem.Text == "����")
            {
                this.ContinuePrint();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }


        FS.HISFC.Components.EPR.Interface.ISearchPatient mySearchPatient = null;
        FS.HISFC.Components.EPR.Interface.ISearchPatient SearchPatient
        {
            get
            {
                if (mySearchPatient == null)
                {
                    mySearchPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.EPR.ucEMRControl), typeof(FS.HISFC.Components.EPR.Interface.ISearchPatient)) as FS.HISFC.Components.EPR.Interface.ISearchPatient;
                    if (mySearchPatient == null)
                    {
                        FS.HISFC.Components.EPR.Interface.ucSearchPatient uc = new FS.HISFC.Components.EPR.Interface.ucSearchPatient();
                        mySearchPatient = uc as FS.HISFC.Components.EPR.Interface.ISearchPatient;
                    }
                }
                return mySearchPatient;
            }
        }

        /// <summary>
        /// �����л�
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {

            if (neuObject == null) return -1;

            SearchPatient.SearchControl.Visible = false;

            if (curPatient!=null && curPatient.ID == ((FS.FrameWork.Models.NeuObject)neuObject).ID)
            {
                return 0;
            }

            //�ж��Ƿ�����л�
            if (AllowClosed() == false)
                return -1;

            

            string[] param = null;
           
            //סԺ����
            if (neuObject.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
                param = new string[]{ FS.FrameWork.Management.Connection.Operator.ID, ((FS.HISFC.Models.RADT.PatientInfo)neuObject).ID };
                this.ucDataFileLoader1.ISql = Common.Classes.Function.ISql;
                this.ucDataFileLoader1.InitSql("", param);
                SetUserText("rybl", "סԺ����");
                setPatientInfo((FS.HISFC.Models.RADT.PatientInfo)neuObject);

            }//���ﻼ��
            else if (neuObject.GetType() == typeof(FS.HISFC.Models.Registration.Register))
            {
                 param = new string[] { FS.FrameWork.Management.Connection.Operator.ID, ((FS.HISFC.Models.Registration.Register)neuObject).ID };
                this.ucDataFileLoader1.ISql = Common.Classes.Function.ISqlOutPatient;
                this.ucDataFileLoader1.InitSql("", param);
                SetUserText("mzbl", "���ﲡ��");

            }
            else
            {
                param = new string[] { FS.FrameWork.Management.Connection.Operator.ID, ((FS.FrameWork.Models.NeuObject)neuObject).ID };
                this.ucDataFileLoader1.ISql = Common.Classes.Function.ISqlOther;
                this.ucDataFileLoader1.InitSql("", param);
                SetUserText("other", "��������");
            }
      

            curPatient = neuObject as FS.FrameWork.Models.NeuObject;

            string id = param[1];
            this.ucDataFileLoader1.Init(this.currentType.ToString(), id );
            this.ucDataFileLoader1.index1 = id ;
            this.ucDataFileLoader1.index2 = ((FS.FrameWork.Models.NeuObject)neuObject).Name;

            this.ucDataFileLoader1.IsShowInterface = this.isShowInterface;
            
            this.ucDataFileLoader1.RefreshForm();

            this.ucUserCommonText1.SetPatient(((FS.FrameWork.Models.NeuObject)neuObject).ID, ucDataFileLoader1.DataStoreName, this.ucDataFileLoader1.ISql);

            if (this.IsShowModual == false )
                this.IsShowModual = true;
            return 0;
        }

        private void SetUserText(string groupID,string groupName)
        {
            if (objGroup.ID != groupID)
            {
                objGroup.ID = groupID;
                objGroup.Name = groupName;
                ucUserText1.GroupInfo = objGroup;
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Cursor = Cursors.WaitCursor;
            if (this.ucDataFileLoader1.Save() == 0)
            {
                this.ucUserCommonText1.RefreshList();
            }
            this.Cursor = Cursors.Default;
            return 0;
        }

        protected FS.FrameWork.Models.NeuObject curPatient = null;
        
        #endregion

        #region ��������
        /// <summary>
        ///��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ManagerFile()
        {
            if (this.curPatient == null) return;
            ucEMRManager c = new ucEMRManager(this.currentType.ToString());
            c.NameChangedEvent += new NameChangedHandler(c_NameChangedEvent);
            c.DeleteEvent += new DeleteHandler(c_DeleteEvent);
            c.InpatientNo = curPatient.ID;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(c);
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newName"></param>
        private void c_NameChangedEvent(int index, string newName)
        {
            this.ucDataFileLoader1.ChangePageName(index, newName);
        }
        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="index"></param>
        private void c_DeleteEvent(int index)
        {
            this.ucDataFileLoader1.DeletePage(index);
        }
        #endregion

        #region ˽�к���

        private void setPatientInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.lblPatient.Text = string.Format("������{0}   ���䣺{1}   �Ա�{2}   ��ͬ��λ��{3}   סԺ�ţ�{4}", patient.Name, patient.Age, patient.Sex.Name, patient.Pact.Name, patient.PID.PatientNO);
        }

        private Control currentControl = null;

        void ucDataFileLoader1_ControlEnter(object sender, EventArgs e)
        {
            this.currentControl = sender as Control;
            FS.FrameWork.EPRControl.IUserControlable ic = this.currentControl as FS.FrameWork.EPRControl.IUserControlable;
            try
            {
                if (ic != null)
                    currentControl = ic.FocusedControl;
            }
            catch { }
            
        }

        protected void SetFont()
        {
            if (currentControl == null)
            {
                MessageBox.Show("��ѡ�����֣�");
                return;
            }
            FontDialog font = new FontDialog();
            bool bRichtTextBox = false;
            if (currentControl.GetType().IsSubclassOf(typeof(RichTextBox))) //�����ı�
            {
                bRichtTextBox = true;
            }
            if (bRichtTextBox)
            {
                font.Font = ((RichTextBox)currentControl).SelectionFont;
            }
            else
            {
                font.Font = currentControl.Font;
            }
            if (font.ShowDialog(this) == DialogResult.OK)
            {
                if (bRichtTextBox)
                {
                    ((RichTextBox)currentControl).SelectionFont = font.Font;
                }
                else
                {
                    currentControl.Font = font.Font;
                }
            }
        }

        protected void SetFont1()
        {
            if (currentControl == null)
            {
                MessageBox.Show("��ѡ�����֣�");
                return;
            }
            frmSetFont form = new frmSetFont();
            
            if (currentControl.GetType().IsSubclassOf(typeof(RichTextBox))) //�����ı�
            {
            }
            else
            {
                return ;
            }
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                //Modified by zhengxun at 2008-2-28
                //for Super and sub Character
                string str = ((RichTextBox)currentControl).SelectedRtf;
                str = str.Replace(@"\nosupersub", "");
                str = str.Replace(@"\super", "");
                str = str.Replace(@"\sub", "");
                
                switch (form.Type)
                {
                    case 0:
                        //((RichTextBox)currentControl).SelectionCharOffset = 4;
                        //((RichTextBox)currentControl).SelectionFont = new Font(((RichTextBox)currentControl).SelectionFont.FontFamily.Name, 7, ((RichTextBox)currentControl).SelectionFont.Style); ;
                        str = str.Replace(@"\lang2052\f0", @"\lang2052\super\f0");
                        break;
                    case 1:
                        //((RichTextBox)currentControl).SelectionCharOffset = 0;
                        //((RichTextBox)currentControl).SelectionFont = new Font(((RichTextBox)currentControl).SelectionFont.FontFamily.Name, 9, ((RichTextBox)currentControl).SelectionFont.Style); ;
                        break;
                    case 2:
                        //((RichTextBox)currentControl).SelectionCharOffset = -4;
                        //((RichTextBox)currentControl).SelectionFont = new Font(((RichTextBox)currentControl).SelectionFont.FontFamily.Name, 7, ((RichTextBox)currentControl).SelectionFont.Style); ;
                        str = str.Replace(@"\lang2052\f0", @"\lang2052\sub\f0");
                        break;
                }
                ((RichTextBox)currentControl).SelectedRtf = str;
            }
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            myPrint();
            return 0;
        }

        private void myPrint()
        {
            print = new FS.FrameWork.WinForms.Classes.Print();
            this.printpage(ref print);
        }

        
        FS.FrameWork.WinForms.Classes.Print print = null;

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void printpage(ref FS.FrameWork.WinForms.Classes.Print print)
        {
            if (this.ucDataFileLoader1.CurrentLoader == null) return;
            if (this.ucDataFileLoader1.CurrentLoader.dt == null) return;
            if (this.ucDataFileLoader1.CurrentLoader.dt.ID == "") return;

            ((FS.FrameWork.EPRControl.emrPanel)this.ucDataFileLoader1.CurrntPanel).AutoScrollPosition = new Point(0, 0);


            FS.HISFC.Models.Base.PageSize page = Common.Classes.Function.GetPageSize(pageName);

            if (page != null)
            {
                print.SetPageSize(page);
                if (page.Memo.Trim().Length == 1)
                    print.ControlBorder = (FS.FrameWork.WinForms.Classes.enuControlBorder)FS.FrameWork.Function.NConvert.ToInt32(page.Memo);
                else
                    print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
            }
            else
            {
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;//default
            }

            bool autoExtend = ((FS.FrameWork.EPRControl.emrPanel)this.ucDataFileLoader1.CurrntPanel).�Զ���ҳ;

            print.IsDataAutoExtend = !autoExtend;
            print.IsHaveGrid = autoExtend;
            print.IsPrintInputBox = false;
            FS.FrameWork.WinForms.Classes.PrintControlCompare p=new FS.FrameWork.WinForms.Classes.PrintControlCompare();
            p.SetEPRControl();
            print.SetControlCompare(p);
            print.IsPrintBackImage = false;
            
            //���ÿؼ���ӡ״̬
            print.PrintPreview(this.ucDataFileLoader1.CurrntPanel);

        }


        FS.HISFC.Components.EPR.Interface.IContinuePrint myContinuePrint = null;
        FS.HISFC.Components.EPR.Interface.IContinuePrint continuePrint
        {
            get
            {
                if (myContinuePrint == null)
                {
                    myContinuePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.EPR.ucEMRControl), typeof(FS.HISFC.Components.EPR.Interface.IContinuePrint)) as FS.HISFC.Components.EPR.Interface.IContinuePrint;
                    if (myContinuePrint == null)
                    {
                        FS.HISFC.Components.EPR.Interface.ContinuePrint uc = new FS.HISFC.Components.EPR.Interface.ContinuePrint();
                        myContinuePrint = uc as FS.HISFC.Components.EPR.Interface.IContinuePrint;
                    }
                }
                return myContinuePrint;
            }
        }
       
        /// <summary>
        /// ����
        /// </summary>
        public void ContinuePrint()
        {

            //���̼�¼����ӡ
            if (continuePrint.IsCanContinuePrint(this.ucDataFileLoader1.CurrntPanel))
            {

                continuePrint.Print(this.ucDataFileLoader1.CurrntPanel);
            }
            else
            {
                MessageBox.Show("��ҳ���ṩ�����ܣ�");
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.IsShowToolFunction = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.groupBox1.Visible = false;
        }

        private void btnEncap_Click(object sender, EventArgs e)
        {
            if (this.ucDataFileLoader1.CurrntPanel == null)
            {
                MessageBox.Show("��ѡ����ҳ��");
                return;
            }
            this.groupBox2.Visible = !this.groupBox2.Visible;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.groupBox2.Visible = false;
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInStore_Click(object sender, EventArgs e)
        {
            if (this.curPatient == null) return;
            //TemplateDesignerHost.Function.SealEMR(this.curPatient as FS.HISFC.Models.RADT.PatientInfo);
            this.groupBox2.Visible = false;
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutStore_Click(object sender, EventArgs e)
        {
            if (this.curPatient == null) return;
            //TemplateDesignerHost.Function.UnSealEMR(this.curPatient as FS.HISFC.Models.RADT.PatientInfo);
            this.groupBox2.Visible = false;
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[1];
                t[0] = typeof(FS.HISFC.Components.EPR.Interface.ISearchPatient);
                t[1] = typeof(FS.HISFC.Components.EPR.Interface.IContinuePrint);
                return t;
            }
        }


        #endregion

        #region ���ܰ�ť
        private void label1_Click(object sender, EventArgs e)
        {
            this.IsShowToolFunction = true;

        }

       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.ucDataFileLoader1.RefreshPage();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //ȥ�������ʶ

            if (this.ucDataFileLoader1.CurrentLoader != null)
            {
                if (ucDataFileLoader1.IsHaveImage(this.ucDataFileLoader1.CurrentLoader.dt.ID))
                {
                    FS.HISFC.Models.File.DataFileInfo df = null;
                    string emrid = "";
                    string emrname = "";

                    try
                    {
                        df = this.ucDataFileLoader1.CurrentLoader.dt;

                        emrid = df.ID;

                        //emrname = df.Name.Remove(0, 5);

                    }
                    catch { }

                    int returnvalue = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.UpdateMessage(2, emrid);

                    if (returnvalue == -1) return;

                    this.ucDataFileLoader1.ChangePageImage(emrid,false);

                    //c_NameChangedEvent(emrid, emrname);
                }
            }
            if (this.ucDataFileLoader1.Save() == 0)
            {
                this.ucUserCommonText1.RefreshList();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.ucDataFileLoader1.CurrentLoader == null)
            {
                MessageBox.Show("��ѡ����ҳ��");
                return;
            }
            ucMapGroup ucMap = new ucMapGroup();
            ucMap.Dock = DockStyle.Fill;
            ucMap.SetLoader(this.ucDataFileLoader1, (FS.HISFC.Models.RADT.PatientInfo)this.curPatient);
            ucMap.Visible = true;
            frmMap form = new frmMap();
            form.Controls.Add(ucMap);
            form.ShowDialog();
        }

        private void btnContinuePrint_Click(object sender, EventArgs e)
        {
            this.ContinuePrint();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (this.ucDataFileLoader1.CurrentLoader == null)
            {
                MessageBox.Show("��ѡ����ҳ��");
                return;
            }
            this.SetFont();
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            this.ManagerFile();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (this.ucDataFileLoader1.CurrentLoader == null)
            {
                MessageBox.Show("��ѡ����ҳ��");
                return;
            }
            this.ucDataFileLoader1.CurrentLoader.RefreshLogo();
        }

        private void btnFont1_Click(object sender, EventArgs e)
        {
            if (this.ucDataFileLoader1.CurrentLoader == null)
            {
                MessageBox.Show("��ѡ����ҳ��");
                return;
            }
            this.SetFont1();
        }
        #endregion

        #region ����
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ucDataFileLoader1.CurrntPanel == null)
            {
                MessageBox.Show("��ѡ����ҳ��");
                return;
            }
            this.groupBox1.Visible = !this.groupBox1.Visible;
        }

        /// <summary>
        /// ���ޱ߿�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBorder_CheckedChanged(object sender, EventArgs e)
        {
            SetBorderStyle(!this.chkBorder.Checked);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ucDataFileLoader1.CurrntPanel.BackgroundImage = null;
        }

        private void SetBorderStyle(bool bHave)
        {
            foreach (Component c in ((FS.FrameWork.EPRControl.emrPanel)this.ucDataFileLoader1.CurrntPanel).Components)
            {
                if (c.GetType().IsSubclassOf(typeof(TextBoxBase)))
                {
                    if (bHave)
                        ((TextBoxBase)c).BorderStyle = BorderStyle.None;
                    else
                        ((TextBoxBase)c).BorderStyle = BorderStyle.FixedSingle;
                }
                if (c.GetType().IsSubclassOf(typeof(ComboBox)) || c.GetType() == typeof(ComboBox))
                {
                    if (bHave)
                        ((ComboBox)c).FlatStyle = FlatStyle.Popup;
                    else
                        ((ComboBox)c).FlatStyle = FlatStyle.System;
                }
            }
        }
        
        private void button12_Click(object sender, EventArgs e)
        {
            this.ucDataFileLoader1.CurrntPanel.BackgroundImage = ((Button)sender).Image;
            foreach (Control c in this.ucDataFileLoader1.CurrntPanel.Controls)
            {
                if (c != null)
                {
                    try
                    {
                        c.BackColor = Color.Transparent;
                    }
                    catch { }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.bmp|*bmp|*.jpg|*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.ucDataFileLoader1.CurrntPanel.BackgroundImage = Image.FromFile(dialog.FileName);
            }
        }

        private void ucDataFileLoader1_Click(object sender, EventArgs e)
        {
            this.groupBox1.Visible = false;
        }

        #endregion

        #region ��
        private void ucDataFileLoader1_PageChanged(object sender,EventArgs e)
        {
           
        }
        Lock myLock = new Lock();
        private void ucDataFileLoader1_BeforOpen(object sender, EventArgs e)
        {
            if(this.curPatient.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                myLock.BeforOpen(this.ucDataFileLoader1, (FS.HISFC.Models.RADT.PatientInfo)this.curPatient, (FS.HISFC.Models.File.DataFileInfo)sender);
        }
        private void ucDataFileLoader1_AfterSaved(object sender ,EventArgs e)
        {
           
        }

        public bool AllowClosed()
        {
            //�ж��Ƿ���û�б���Ĳ���������ʾ���Ƿ񱣴�
            
            if (this.ucDataFileLoader1.ReadOnly == true) return true;
            if (ucDataFileLoader1.CurrentLoader == null || this.ucDataFileLoader1.CurrentLoader.dt == null) return true;
            if (this.ucDataFileLoader1.IsHaveSaved() == false) return false;
            if (this.curPatient.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                myLock.UnLock(this.ucDataFileLoader1, (FS.HISFC.Models.RADT.PatientInfo)this.curPatient);
            return true;
            
        }
        #endregion

     

        #region �����ӿ�
        private void mnuSendMessage_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.File.DataFileInfo dt = null;
            string eprid ="";
            string eprName ="";
            FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();
            try
            {
                dt = this.ucDataFileLoader1.CurrentLoader.dt;
                eprid = dt.ID;
                eprName = dt.Name;
            }catch{}
            FS.HISFC.Components.EPR.Controls.ucSendMessage sendMessage = new FS.HISFC.Components.EPR.Controls.ucSendMessage(this.curPatient, eprid, eprName, oper);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(sendMessage);

        }

        private void mnuViewWriteRule_Click(object sender, EventArgs e)
        {
            ucCaseWriteRule uc = new ucCaseWriteRule();
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }

        #endregion


    }
    public enum SQLType
    {
        Outpatient,
        Inpatient,
        Other
    }
}
