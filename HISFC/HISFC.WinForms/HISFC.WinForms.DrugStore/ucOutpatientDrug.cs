using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.WinForms.DrugStore
{
    /// <summary>
    /// Bed<br></br>
    /// [��������: �����䷢ҩ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <�޸ļ�¼
    ///	    Ϊ�˲��Կؼ�ʹ�� ϣ������ͨ�����ര�ڵ����������������洰��Tag Ŀǰ��������..
    ///  />
    /// </summary>
    public partial class ucOutpatientDrug : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutpatientDrug()
        {
            InitializeComponent();
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

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            FS.FrameWork.Management.DataBaseManger dataBaseManager = new FS.FrameWork.Management.DataBaseManger();

            if (this.isOtherDrugDept)
            {
                FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();
                System.Collections.ArrayList al = integrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
                FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
                    return;
                else
                    this.OperDept = info;
            }
            else
            {
                this.OperDept = ((FS.HISFC.Models.Base.Employee)dataBaseManager.Operator).Dept;
            }

            this.OperInfo = dataBaseManager.Operator;
            this.ApproveDept = ((FS.HISFC.Models.Base.Employee)dataBaseManager.Operator).Dept;

            if (this.InitTerminal() == -1)
                return;

            this.InitControlParm();
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
                return -1;
            return 1;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void InitControlParm()
        {
            this.ucClinicTree1.OperDept = this.OperDept;
            this.ucClinicTree1.OperInfo = this.OperInfo;
            this.ucClinicTree1.ApproveDept = this.OperDept;
            this.ucClinicTree1.SetFunMode(FS.HISFC.Components.DrugStore.OutpatientFun.Drug);
            this.ucClinicTree1.SetTerminal(this.Terminal);

            this.ucClinicDrug1.OperDept = this.OperDept;
            this.ucClinicDrug1.OperInfo = this.OperInfo;
            this.ucClinicDrug1.ApproveDept = this.OperDept;
            this.ucClinicDrug1.SetFunMode(FS.HISFC.Components.DrugStore.OutpatientFun.Drug);
            this.ucClinicDrug1.SetTerminal(this.Terminal);

            this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            //this.statusBar1.Panels[1].Text = "���ڱ���...";

            base.OnStatusBarInfo(null, "���ڱ���...");

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
            try
            {
                this.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            base.OnLoad(e);
        }

        private void ucClinicTree1_MyTreeSelectEvent(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            this.ucClinicDrug1.ShowData(drugRecipe);
        }

        private void ucClinicDrug1_EndSave(object sender, EventArgs e)
        {
            this.ucClinicTree1.ChangeNodeLocation();
        }

        //private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    if (e.ClickedItem == this.tsbExit)          //�˳�
        //    {
        //        if (this.EnableExit())
        //            this.Close();
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbSave)          //����
        //    {
        //        this.Save();
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbRefresh)       //ˢ��
        //    {
        //        this.ucClinicTree1.ShowList();
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbQuery)         //��ѯ
        //    {
        //        this.ucClinicTree1.FindNode();
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbPrint)         //��ӡ
        //    {
        //        this.ucClinicTree1.Print();
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbPause)         //��ͣ��ӡ
        //    {
        //        FS.FrameWork.WinForms.Classes.Print.PausePrintJob(0);
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbRefreshWay)    //�ֹ�ˢ�� / �Զ�ˢ��
        //    {
        //        if (this.tsbRefreshWay.Checked)         //�ֹ�ˢ��״̬
        //        {
        //            this.ucClinicTree1.EndProcessRefresh();
        //            this.tsbRefreshWay.Text = "�Զ�ˢ��";
        //        }
        //        else
        //        {
        //            this.ucClinicTree1.BeginProcessRefresh(1000);
        //            this.tsbRefreshWay.Text = "�ֶ�ˢ��";
        //        }
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbRecipe)        //����
        //    {
        //        this.ucClinicDrug1.IsPrintRecipe = this.tsbRecipe.Checked;
        //        return;
        //    }
        //    if (e.ClickedItem == this.tsbDrugList)      //��ҩ�嵥
        //    {
        //        this.ucClinicDrug1.IsPrintListing = this.tsbDrugList.Checked;
        //        return;

        //    }
        //}

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ˢ��", "ˢ���б���ʾ", 0, true, false, null);
            this.toolBarService.AddToolButton("��ͣ��ӡ", "��ͣ��ǰ��ӡ��", 0, true, false, null);
            this.toolBarService.AddToolButton("�ֹ�ˢ��", "�˳�Ӧ�ó������δ��ҩ����ʱ�������˳�", 0, true, false, null);
            this.toolBarService.AddToolButton("����", "��ҩͬʱ��ӡ����", 0, true, false, null);
            this.toolBarService.AddToolButton("��ҩ�嵥", "��ҩͬʱ��ӡ��ҩ�嵥", 0, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ˢ��":
                    this.ucClinicTree1.ShowList();
                    break;
                case "��ͣ��ӡ":
                    FS.FrameWork.WinForms.Classes.Print.PausePrintJob(0);
                    break;
                case "����":
                    //this.ucClinicDrug1.IsPrintRecipe = this.tsbRecipe.Checked;
                    break;
                case "��ҩ�嵥":
                    //this.ucClinicDrug1.IsPrintListing = this.tsbDrugList.Checked;
                    break;
                case "�ֹ�ˢ��":
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ucClinicTree1.FindNode();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucClinicTree1.Print();
            return 1;
        }

        private void ucClinicTree1_ProcessMessageEvent(object sender, string msg)
        {
            //this.statusBar1.Panels[1].Text = msg;

            base.OnStatusBarInfo(null, msg);
        }
    }
}
