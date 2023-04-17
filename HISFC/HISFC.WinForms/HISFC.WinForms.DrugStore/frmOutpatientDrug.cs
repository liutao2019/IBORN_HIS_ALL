using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.WinForms.DrugStore
{
    /// <summary>
    /// Bed<br></br>
    /// [��������: �����䷢ҩ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <˵��
    ///	    1 ��Ҫͨ������Tag���봰�ڹ���  �������ö��FS.HISFC.Components.DrugStore.OutpatientWinFun�ô�����
    ///     2 ��ҩ�ն˵�����ʼ�� ͨ�� ���ݿ� Job��ִ�� 
    ///     3 Ϊ�˿���ͬʱ��½һ����ҩ�նˣ�ʹ��Memo�ֶν����������ơ�MemoΪ��1�� ˵������ʹ����
    ///  />
    /// <�޸ļ�¼>
    ///    1.�޸���ҩ�����˳����ܻ�ԭ���ڵ�¼״̬������ by Sunjh 2010-9-13 {52F2E611-215F-46b1-9594-4688D12CEFE1} 
    /// </�޸ļ�¼>
    /// </summary>
    public partial class frmOutpatientDrug : FS.FrameWork.WinForms.Forms.BaseStatusBar, FS.FrameWork.WinForms.Classes.IPreArrange, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public frmOutpatientDrug()
        {
            InitializeComponent();

            this.ucClinicTree1.OperChangedEvent += new FS.HISFC.Components.DrugStore.Outpatient.ucClinicTree.MyOperChangedHandler(ucClinicTree1_OperChangedEvent);

            this.ucClinicTree1.SaveRecipeEvent += new EventHandler(ucClinicTree1_SaveRecipeEvent);

            this.ucClinicDrug1.MessageEvent += new EventHandler(ucClinicDrug1_MessageEvent);

            this.FormClosed += new FormClosedEventHandler(frmOutpatientDrug_FormClosed);

            this.ProgressRun(true);
     
        }

        private void frmOutpatientDrug_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!this.isCancel)
            //{
            //    if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
            //    {
            //        FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();                    
            //        this.Terminal = drugStoreManager.GetDrugTerminalById(this.Terminal.ID);
            //        this.Terminal.Memo = "";
            //        if (drugStoreManager.UpdateDrugTerminal(this.Terminal) == -1)
            //        {
            //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ҩ�ն�Ϊ���ñ��ʧ��"));
            //            return;
            //        }
            //    }
            //}

            //�޸���ҩ�����˳����ܻ�ԭ���ڵ�¼״̬������ by Sunjh 2010-9-13 {52F2E611-215F-46b1-9594-4688D12CEFE1}
            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
            {
                FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
                this.Terminal = drugStoreManager.GetDrugTerminalById(this.Terminal.ID);
                this.Terminal.Memo = "";
                if (drugStoreManager.UpdateDrugTerminal(this.Terminal) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ҩ�ն�Ϊ���ñ��ʧ��"));
                    return;
                }
            }
        }

        private void ucClinicDrug1_MessageEvent(object sender, EventArgs e)
        {
            this.Msg = sender.ToString();

            this.ShowMsg();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            int iRes;
            System.Math.DivRem(this.iTimerTick, 2, out iRes);
            if (iRes == 0)
            {
                this.tsMsg.Text = "";
            }
            else
            {
                this.tsMsg.Text = this.msg;
            }

            this.iTimerTick++;
            if (this.iTimerTick == 11)
            {
                t.Stop();
                this.iTimerTick = 0;
                this.Msg = "";
            }
        }     

        #region �����

        /// <summary>
        /// �����������ն�����
        /// </summary>
        private FS.HISFC.Components.DrugStore.OutpatientFun funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;

        /// <summary>
        /// �������� ���ε�½ѡ��Ŀ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject OperDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject OperInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��׼���� �ۿ���� 
        /// </summary>
        private FS.FrameWork.Models.NeuObject ApproveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����ն�
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugTerminal Terminal = new FS.HISFC.Models.Pharmacy.DrugTerminal();

        /// <summary>
        /// ���ڹ���
        /// </summary>
        private FS.HISFC.Components.DrugStore.OutpatientWinFun winFun = FS.HISFC.Components.DrugStore.OutpatientWinFun.��ҩ;

        /// <summary>
        /// �Ƿ�����ҩ����/��ҩ
        /// </summary>
        private bool isOtherDrugDept = false;

        /// <summary>
        /// ��ǰ�����Ƿ����ʧ��
        /// </summary>
        private bool isCancel = false;

        /// <summary>
        /// ʱ����
        /// </summary>
        private System.Windows.Forms.Timer t = null;

        /// <summary>
        /// ʱ����
        /// </summary>
        private int iTimerTick = 0;

        /// <summary>
        /// ��Ϣ��ʾ
        /// </summary>
        private string msg = "";

        #endregion

        #region  ����

        /// <summary>
        /// ���ڹ���
        /// </summary>
        public FS.HISFC.Components.DrugStore.OutpatientWinFun WinFun
        {
            get
            {
                return this.winFun;
            }
            set
            {
                this.winFun = value;

                switch (value)
                {
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.��ҩ:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.��ҩ:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.ֱ�ӷ�ҩ:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.DirectSend;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.��ҩ:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Back;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.����ҩ����ҩ:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.isOtherDrugDept = true;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.����ҩ����ҩ:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                        this.isOtherDrugDept = true;
                        break;
                }
            }
        }

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public string Msg
        {
            set
            {
                this.msg = value;

                this.tsMsg.Text = value;
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public int Init()
        {
            FS.FrameWork.Management.DataBaseManger dataBaseManager = new FS.FrameWork.Management.DataBaseManger();

            this.OperDept = ((FS.HISFC.Models.Base.Employee)dataBaseManager.Operator).Dept;

            if (this.isOtherDrugDept)
            {
                FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();
                System.Collections.ArrayList al = integrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
                foreach (FS.HISFC.Models.Base.Department tempDept in al)
                {
                    if (tempDept.ID == this.OperDept.ID)
                    {
                        al.Remove(tempDept);
                        break;
                    }
                }
                FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
                {
                    return -1;
                }
                else
                {
                    this.OperDept = info;
                }
            }
          
            this.OperInfo = dataBaseManager.Operator;
            this.ApproveDept = ((FS.HISFC.Models.Base.Employee)dataBaseManager.Operator).Dept;

            if (this.InitTerminal() == -1)
            {
                return -1;
            }

            this.InitControlParm();

            return 1;
        }

        /// <summary>
        /// �ն˳�ʼ��  ��ʱд��ʹ����ҩ̨
        /// </summary>
        protected int InitTerminal()
        {
            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
                this.Terminal = FS.HISFC.Components.DrugStore.Function.TerminalSelect(this.OperDept.ID, FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ̨, true);
            else
                this.Terminal = FS.HISFC.Components.DrugStore.Function.TerminalSelect(this.OperDept.ID, FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ����, true);

            if (this.Terminal == null)
            {
                return -1;
            }

            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
            {
                if (this.Terminal.Memo == "1")
                {
                    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ն������������Ե�½�������ٴ�ʹ�ã���ȷ�ϵ�½����ҩ�ն���\n ע�⣺���ǿ�е�½�����������ҩ�嵥��ӡ���ң�"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (rs == DialogResult.No)
                    {
                        this.isCancel = true;
                        this.Terminal.Memo = "";
                        return -1;
                    }
                }
                FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
                this.Terminal.Memo = "1";
                if (drugStoreManager.UpdateDrugTerminal(this.Terminal) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ҩ�ն�Ϊ�����ñ��ʧ��"));
                    this.isCancel = true;
                    this.Terminal.Memo = "";
                    return -1;
                }
            }

            this.statusBar1.Panels[3].Text = this.statusBar1.Panels[3].Text + " - " + this.OperDept.Name + this.Terminal.Name + "[" + this.Terminal.ID + "]";

            return 1;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void InitControlParm()
        {
            this.ucClinicTree1.OperDept = this.OperDept;
            this.ucClinicTree1.OperInfo = this.OperInfo;
            this.ucClinicTree1.ApproveDept = this.ApproveDept;     //��׼���� ʵ�ʿۿ����
            this.ucClinicTree1.SetFunMode(this.funMode);
            this.ucClinicTree1.SetTerminal(this.Terminal);

            this.ucClinicTree1.IsFindToAdd = true;

            this.ucClinicDrug1.IsShowDrugSendInfo = false;      //����ʾ��/��ҩ��Ϣ

            this.ucClinicDrug1.OperDept = this.OperDept;
            this.ucClinicDrug1.OperInfo = this.OperInfo;
            this.ucClinicDrug1.ApproveDept = this.ApproveDept;     //��׼���� ʵ�ʿۿ����
            this.ucClinicDrug1.SetFunMode(this.funMode);
            this.ucClinicDrug1.SetTerminal(this.Terminal);
        }

        #endregion

        /// <summary>
        /// ��Ϣ��ʾ
        /// </summary>
        /// <param name="msg"></param>
        public void ShowMsg()
        {
            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();      
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            this.statusBar1.Panels[1].Text = "���ڱ���...";

            this.ucClinicTree1.IsBusySave = true;

            this.ucClinicDrug1.Save();

            this.ucClinicTree1.IsBusySave = false;
        }

        /// <summary>
        /// �˳��ж� �Ƿ�����رմ���
        /// </summary>
        /// <returns></returns>
        public bool EnableExit()
        {
            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
            {
                if (this.ucClinicTree1.SpareNode())
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����δ��ҩȷ�ϵĴ��� ������д��������ҩ ������ҩȷ�Ϻ��ٹرմ���"));
                    return false;
                }
            }
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
           
            base.OnLoad(e);

            //��д��仰�����޷����
            this.WindowState = FormWindowState.Maximized;           

            try
            {               
                //���Ʋ���������
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                                             
                object factoryInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory)) as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;
                if (factoryInstance != null)
                {
                    FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory factory = factoryInstance as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;

                    FS.HISFC.Components.DrugStore.Function.IDrugPrint = factory.GetInstance(this.Terminal);

                    if (FS.HISFC.Components.DrugStore.Function.IDrugPrint == null)
                    {
                        this.isCancel = true;
                    }
                }
                else
                {
                    //Ĭ�ϲ�������ʾ
                    //MessageBox.Show("δ���ô�������ӡ��ʵ�֣����޷����д������ݴ�ӡ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.isCancel = true;

                    //#region �����ȡ��ǩ��ʽ     

                    //try
                    //{
                    //    #region ��ǩ/�嵥��ӡ �ӿ�ʵ��

                    //    object[] o = new object[] { };
                    //    string factoryValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Clinic_Print_Label, true, "FS.Report.DrugStore.OutpatientBillPrint");

                    //    System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", factoryValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                    //    object oLabel = objHandel.Unwrap();

                    //    FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory factory = oLabel as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;
                    //    if (factory != null)
                    //    {
                    //        FS.HISFC.Components.DrugStore.Function.IDrugPrint = factory.GetInstance(this.Terminal);
                    //    }

                    //    if (FS.HISFC.Components.DrugStore.Function.IDrugPrint == null)
                    //    {
                    //        this.isCancel = true;
                    //    }

                    //    #endregion
                       
                    //}
                    //catch (System.TypeLoadException ex)
                    //{
                    //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //    MessageBox.Show(Language.Msg("��ǩ�����ռ���Ч\n" + ex.Message));
                    //    this.isCancel = true;
                    //    return;
                    //}

                    //#endregion
                }

                object interfacePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;
                if (interfacePrint != null)
                {
                    FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug.RecipePrint = interfacePrint as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;

                    if (FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug.RecipePrint == null)
                    {
                        this.isCancel = true;
                    }
                }
                else
                {
                    //Ĭ�ϲ�������ʾ
                    //MessageBox.Show("δ���ô�������ӡ��ʵ�֣����޷����д������ݴ�ӡ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.isCancel = true;

                    //#region ���ﴦ����ӡ�ӿ�ʵ��

                    //object[] o1 = new object[] { };
                    ////���ﴦ����ӡ�ӿ���ʵ��
                    //string recipeValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Clinic_Print_Recipe, true, "Report.Order.ucRecipePrint");

                    ////����
                    //System.Runtime.Remoting.ObjectHandle objHandel1 = System.Activator.CreateInstance("Report", recipeValue, false, System.Reflection.BindingFlags.CreateInstance, null, o1, null, null, null);
                    //object oLabel1 = objHandel1.Unwrap();

                    //FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug.RecipePrint = oLabel1 as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;

                    //if (FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug.RecipePrint == null)
                    //{
                    //    this.isCancel = true;
                    //}

                    //#endregion
                }

                //�ؼ���ʼ�� ���ȵ����걾���ڳ�ʼ����Ϣ���ٵ��ÿؼ���ʼ��
                this.ucClinicTree1.Init();
                this.ucClinicDrug1.Init();

                //�б��ʼ��ˢ��
                this.ucClinicTree1.RefreshOperList(true);
                //����Ļ��ʾ�ӿ����� ��ҩ����ʹ��
                if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Send || this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.DirectSend)
                {
                    if (this.ucClinicTree1.IsShowFeeData)
                    {
                        this.ucClinicTree1.BeginLEDRefresh(true);
                    }
                }

                //���2��������Զ�ˢ�³��� ����������� ��ô�������ݴ���ӡʱ����ɴ򿪴��ڳ�ʱ���ӳ�
                //��������(�ô�ӡ���ʽ��ӡ��ǩʱ�����)
                if (!this.tsbRefreshWay.Checked)
                {
                    this.ucClinicTree1.BeginProcessRefresh(2000);
                }

                this.ucClinicTree1.SetFocus();
            
                //������ͣ��ť
                this.tsbPause.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.isCancel = true;
            }            
        }

        private void ucClinicTree1_MyTreeSelectEvent(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            this.ucClinicDrug1.ShowData(drugRecipe);
        }

        private void ucClinicDrug1_EndSave(object sender, EventArgs e)
        {
            this.ucClinicTree1.ChangeNodeLocation();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {            
            if (e.ClickedItem == this.tsbExit)          //�˳�
            {
                if (this.EnableExit())
                {
                    this.Close();
                }
                return;
            }        

            if (e.ClickedItem == this.tsbSave)          //����
            {
                this.Save();
                return;
            }
            if (e.ClickedItem == this.tsbRefresh)       //ˢ��
            {
                this.ucClinicDrug1.Clear();

                this.ucClinicTree1.ShowList();
                return;
            }
            if (e.ClickedItem == this.tsbQuery)         //��ѯ
            {
                this.ucClinicTree1.FindNode();
                return;
            }
            if (e.ClickedItem == this.tsbPrint)         //��ӡ
            {
                if (this.tsbRecipe.Checked || this.tsbDrugList.Checked)
                {
                    this.ucClinicDrug1.Print();
                }
                else
                {
                    this.ucClinicTree1.Print();
                }

                return;
            }
            if (e.ClickedItem == this.tsbPause)         //��ͣ��ӡ
            {
                FS.FrameWork.WinForms.Classes.Print.PausePrintJob(0);
                return;
            }
            if (e.ClickedItem == this.tsbRefreshWay)    //�ֹ�ˢ�� / �Զ�ˢ��
            {
                this.tsbRefreshWay.Checked = !this.tsbRefreshWay.Checked;

                if (this.tsbRefreshWay.Checked)         //�ֹ�ˢ��״̬
                {
                    this.ucClinicTree1.IsAutoPrint = false;
                    this.tsbRefreshWay.Text = FS.FrameWork.Management.Language.Msg( "�Զ���ӡ" );

                    this.statusBar1.Panels[1].Text = FS.FrameWork.Management.Language.Msg( "�ֹ���ӡ״̬ ֹͣ�Զ���ӡ" );
                }
                else
                {
                    this.ucClinicTree1.IsAutoPrint = true;
                    this.tsbRefreshWay.Text = FS.FrameWork.Management.Language.Msg( "�ֶ���ӡ" );
                    this.statusBar1.Panels[1].Text = "�Զ���ӡ״̬..." + (this.ucClinicTree1.IsBusySave ? "���ڱ���" : "����ˢ��");
                   
                }

                //if (this.tsbRefreshWay.Checked)         //�ֹ�ˢ��״̬
                //{
                //    this.ucClinicTree1.EndProcessRefresh();
                //    this.tsbRefreshWay.Text = "�Զ�ˢ��";

                //    this.statusBar1.Panels[1].Text = "�ֹ�ˢ��״̬ ֹͣ�Զ�ˢ��";
                //}
                //else
                //{
                //    this.ucClinicTree1.BeginProcessRefresh(1000);
                //    this.tsbRefreshWay.Text = "�ֹ�ˢ��";
                //}

                return;
            }
            if (e.ClickedItem == this.tsbRecipe)        //����
            {
                this.tsbRecipe.Checked = !this.tsbRecipe.Checked;

                this.ucClinicDrug1.IsPrintRecipe = this.tsbRecipe.Checked;
                return;
            }
            if (e.ClickedItem == this.tsbDrugList)      //��ҩ�嵥
            {
                this.tsbDrugList.Checked = !this.tsbDrugList.Checked;

                this.ucClinicDrug1.IsPrintListing = this.tsbDrugList.Checked;
                return;

            }
        }

        private void ucClinicTree1_ProcessMessageEvent(object sender, string msg)
        {
            try
            {
                if (this.statusBar1.Controls.Count > 0)
                {
                    this.statusBar1.Controls[1].Text = msg;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ucClinicTree1_OperChangedEvent(FS.FrameWork.Models.NeuObject oper)
        {
            this.ucClinicDrug1.OperInfo = oper;
        }

        private void ucClinicTree1_SaveRecipeEvent(object sender, EventArgs e)
        {
            //�������Ա���Żس����� �������β�������¼�
            this.Save();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Divide)                         //�˳�
            {
                this.toolStrip1_ItemClicked(null,new ToolStripItemClickedEventArgs(this.tsbExit));

                return true;
            }
            if (keyData == Keys.E )     //����
            {
                this.toolStrip1_ItemClicked(null,new ToolStripItemClickedEventArgs(this.tsbSave));

                return true;
            }
            if (keyData == Keys.P)
            {
                this.toolStrip1_ItemClicked(null,new ToolStripItemClickedEventArgs(this.tsbPrint));

                return true;
            }
            if (keyData == Keys.Add)
            {
                this.toolStrip1_ItemClicked(null,new ToolStripItemClickedEventArgs(this.tsbRefresh));

                return true;
            }
            if (keyData == Keys.Subtract)
            {
                this.toolStrip1_ItemClicked(null,new ToolStripItemClickedEventArgs(this.tsbPause));

                return true;                
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// �ر�ʱ������Դ����  {1367F373-862B-4ff5-A14A-F0DB46092776}
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            try
            {
                //ֹͣ��ǰ�߳�
                if (this.ucClinicTree1 != null)
                {
                    this.ucClinicTree1.EndProcessRefresh();
                }

            }
            catch
            {
            }

            base.OnClosed(e);
        }


        #region IPreArrange ��Ա

        bool isPreArrange = false;

        public int PreArrange()
        {
            this.isPreArrange = true;

            #region ���ݴ��ڲ��� ���ô��ڹ���

            if (this.Tag != null)
            {
                switch (this.Tag.ToString().ToUpper())
                {
                    case "DRUG":        //��ҩ
                    case "ODRUG":       //����ҩ����ҩ
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.Text = "������ҩ";
                        break;
                    case "SEND":        //��ҩ
                    case "OSEND":       //����ҩ����ҩ
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                        this.Text = "���﷢ҩ";
                        break;
                    case "BACK":        //��ҩ
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Back;
                        this.Text = "���ﻹҩ";
                        break;
                    case "DSEND":       //ֱ�ӷ�ҩ (��������ҩ)
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.DirectSend;
                        this.Text = "����ֱ�ӷ�ҩ";
                        break;
                     //��������ҩ���䡢��ҩ���� ʵ����֤�ô�����
                    //case "ODRUG":       //����ҩ����ҩ
                    //    this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                    //    this.isOtherDrugDept = true;
                    //    this.Text = "����ҩ����ҩ";
                    //    break;
                    //case "OSEND":       //����ҩ����ҩ
                    //    this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                    //    this.isOtherDrugDept = true;
                    //    this.Text = "����ҩ����ҩ";
                    //    break;
                    default:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.Text = "������ҩ";
                        break;
                }
            }

            #endregion

            //�����ڻ�����Ϣ��ȡ ���ؼ���Ϣ��ֵ
            if (this.Init() == -1)
            {
                this.isCancel = true;
                return -1;
            }

            return 1;
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[2];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory);
                printType[1] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint);

                return printType;
            }
        }

        #endregion
    }
}