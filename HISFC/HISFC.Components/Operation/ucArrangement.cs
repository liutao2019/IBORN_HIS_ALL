using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;
using FS.FrameWork.Management;
namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: �������ſؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucArrangement : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucArrangement()
        {
            InitializeComponent();
        }

        #region �ֶ�
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        // {CB2F6DC4-F9C6-4756-A118-CEDB907C39EC}
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch�Ƿ�ϲ�����������������
        /// <summary>
        /// �Ƿ�ϲ�����������������
        /// </summary>
        private bool isJoinUc = false;
        /// <summary>
        /// ����ʵ��
        /// </summary>
        private List<OperationAppllication> apply = null;
        /// <summary>
        /// ���뵥�Ƿ���Ҫ�������Σ�����Ȩҽ�������
        /// </summary>
        private bool isNeedApprove = false;
        #endregion

        #region ����
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
        [Category("�ؼ�����"), Description("�Ƿ�ϲ�����������������")]    
        public bool IsJoinUc
        {
            get
            {
                return isJoinUc;
            }
            set
            {
                isJoinUc = value;
            }
        }

        private string anaesDocDept=string.Empty;
        /// <summary>
        /// ָ������ҽ����������
        /// </summary>
        [Category("�ؼ�����"), Description("����ҽ�����ڿ��ұ���")]
        public string AnaesDocDept
        {
            get
            {
                return this.anaesDocDept;
            }
            set
            {
                this.anaesDocDept = value;
            }
        }

        private FS.FrameWork.Public.ObjectHelper OperatoinOrderHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #region ����
        

        /// <summary>
        /// ˢ�����������б�
        /// </summary>
        /// <returns></returns>
        public int RefreshApplys()
        {

            this.ucArrangementSpread1.Reset();
            this.labApplyTime.Visible = false;
            //��ʼʱ��
            string beginTime = this.neuDateTimePicker1.Value.Date.ToString();
            //����ʱ��
            string endTime = this.neuDateTimePicker1.Value.Date.AddDays(1).ToString();
            //FS.HISFC.Components.Interface.Classes.Function.ShowWaitForm("������������,���Ժ�...");
            Application.DoEvents();
            ArrayList alApplys;
            try
            {
                this.ucArrangementSpread1.Reset();
                alApplys = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, beginTime, endTime,isNeedApprove);
                if (alApplys != null)
                {
                    CompareByDeptAndSort compareByDeptAndSort = new CompareByDeptAndSort();
                    compareByDeptAndSort.OperatoinOrderHelper = this.OperatoinOrderHelper;
                    alApplys.Sort(compareByDeptAndSort);
                    foreach (OperationAppllication apply in alApplys)
                    {
                        this.ucArrangementSpread1.AddOperationApplication(apply);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��������������Ϣ����!" + e.Message, "��ʾ");
                return -1;
            }

            this.ucArrangementSpread1.SetFilter();

            //FS.HISFC.Components.Interface.Classes.Function.HideWaitForm();
            //if (fpSpread1_Sheet1.RowCount > 0)
            //{
            //FarPoint.Win.Spread.LeaveCellEventArgs e = new FarPoint.Win.Spread.LeaveCellEventArgs
            //    (new FarPoint.Win.Spread.SpreadView(fpSpread1), 0, 0, 0, (int)Cols.opDate);
            //fpSpread1_LeaveCell(fpSpread1, e);
            //    fpSpread1.Focus();
            //    fpSpread1_Sheet1.SetActiveCell(0, (int)Cols.opDate, true);
            //}
            return 0;
        }

        public int RefreshAlreadyOperations()
        {
            this.ucAlreadyOperationSpread1.Reset();
            this.labApplyTime.Visible = false;
            //��ʼʱ��
            string beginTime = this.neuDateTimePicker1.Value.Date.ToString();
            //����ʱ��
            string endTime = this.neuDateTimePicker1.Value.Date.AddDays(1).ToString();
            Application.DoEvents();
            ArrayList alAlreadys;
            try
            {
                this.ucAlreadyOperationSpread1.Reset();
                alAlreadys = Environment.OperationManager.GetAlreadyOpsList(Environment.OperatorDeptID, beginTime, endTime);
                if (alAlreadys != null)
                {
                    CompareByDeptAndSort compareByDeptAndSort = new CompareByDeptAndSort();
                    compareByDeptAndSort.OperatoinOrderHelper = this.OperatoinOrderHelper;
                    alAlreadys.Sort(compareByDeptAndSort);
                    foreach (OperationAppllication already in alAlreadys)
                    {
                        this.ucAlreadyOperationSpread1.AddOperationApplication(already);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("���������δ�շ�������Ϣ����"+e.Message,"��ʾ");
                return -1;
            }
            this.ucAlreadyOperationSpread1.SetFilter();
            return 0;
        }

        /// <summary>
        ///�Ǽ���������δ�շѵĻ���
        /// </summary>
        /// <returns></returns>
        public int AlreadyOperation()
        {
            //��ʼʱ��
            string beginTime = this.neuDateTimePicker1.Value.Date.ToString();
            //����ʱ��
            string endTime = this.neuDateTimePicker1.Value.Date.AddDays(1).ToString();

            ArrayList alAlreadys;
            try
            {
                alAlreadys = Environment.OperationManager.GetAlreadyOpsAppList(Environment.OperatorDeptID, beginTime, endTime, isNeedApprove);
                if (alAlreadys != null)
                {
                    //foreach (OperationAppllication apply in alAlreadys)
                    //{
                        this.ucArrangementSpread1.ChangeState();
                    //}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��������������Ϣ����!" + e.Message, "��ʾ");
                return -1;
            }
            return 0;
        }

        /// <summary>
        ///ȡ���Ǽ���������δ�շѵĻ���
        /// </summary>
        /// <returns></returns>
        public int CancelAlreadyOperation()
        {
            //��ʼʱ��
            string beginTime = this.neuDateTimePicker1.Value.Date.ToString();
            //����ʱ��
            string endTime = this.neuDateTimePicker1.Value.Date.AddDays(1).ToString();

            ArrayList alCancelAlreadys;
            try
            {
                alCancelAlreadys = Environment.OperationManager.GetAlreadyOpsList(Environment.OperatorDeptID, beginTime, endTime);
                if (alCancelAlreadys != null)
                {
                    foreach (OperationAppllication cancel in alCancelAlreadys)
                    {
                        this.ucAlreadyOperationSpread1.CancelState();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��������������Ϣ����!" + e.Message, "��ʾ");
                return -1;
            }
            return 0;
        }
        

    
        #endregion

        #region �¼�

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            this.toolBarService.AddToolButton("ȫ��", "ȫ��", FS.FrameWork.WinForms.Classes.EnumImageList.F�ֽ�, true, true, null);
            this.toolBarService.AddToolButton("�Ѱ���", "�Ѱ���", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ��Һ��, true, false, null);
            this.toolBarService.AddToolButton("δ����", "δ����", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡִ�е�, true, false, null);
            this.toolBarService.AddToolButton("����", "����������", FS.FrameWork.WinForms.Classes.EnumImageList.Zת��, true, false, null);
            this.toolBarService.AddToolButton("�����Ǽ�", "�����Ǽ�", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ�嵥, true, false, null);
            this.toolBarService.AddToolButton("ȫѡ", "ȫѡ", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȫ��ѡ", "ȫ��ѡ", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��ѡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ���Ǽ�", "ȡ���Ǽ�", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("��������", "����", FS.FrameWork.WinForms.Classes.EnumImageList.Z����, true, false, null);
            this.toolBarService.AddToolButton("�������", "�������", FS.FrameWork.WinForms.Classes.EnumImageList.W��ɵ�, true, false, null);
            return this.toolBarService;
        }

        public void AllSelect()
        {
            if (this.tabInformation.SelectedTab == this.tabOpertion)
            {
                
                this.ucArrangementSpread1.AllSelect();
            }
            else if (this.tabInformation.SelectedTab == this.tabAnaesthesia)
            {
                 
                this.ucAnaesthesiaSpread1.AllSelect();
            }
            
        }

        public void AllNotSelect()
        {
            if (this.tabInformation.SelectedTab == this.tabOpertion)
            {

                this.ucArrangementSpread1.AllNotSelect();
            }
            else if (this.tabInformation.SelectedTab == this.tabAnaesthesia)
            {

                this.ucAnaesthesiaSpread1.AllNotSelect();
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {          
            //this.ucArrangementSpread1.Date = this.neuDateTimePicker1.Value;
            //this.ucArrangementSpread1.Print();
            //return base.OnPrint(sender, neuObject);
            if (this.tabInformation.SelectedTab == this.tabOpertion)
            {
                this.ucArrangementSpread1.Date=this.neuDateTimePicker1.Value;
                this.ucArrangementSpread1.Print();
            }
            else if (this.tabInformation.SelectedTab == this.tabAnaesthesia)
            {
                this.ucAnaesthesiaSpread1.Date=this.neuDateTimePicker1.Value;
                this.ucAnaesthesiaSpread1.Print();
            }
            return base.OnPrint(sender, neuObject);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            //���뵥�Ƿ���Ҫ��˲��� true ���ȡ����˺��Ѱ������� false���ȡ���� ��� ��������2012-3-12 chengym
            FS.HISFC.BizProcess.Integrate.Manager ctlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            try
            {
                //�Ƿ���Ҫ���
                string strNeedApprove = ctlMgr.QueryControlerInfo("opappr");
                if (strNeedApprove == "1")
                {
                    this.isNeedApprove = true;
                }
            }
            catch
            {

            }

            if (this.tabInformation.SelectedTab == this.tabOpertion)
            {
                this.RefreshApplys();
            }
            else if (this.tabInformation.SelectedTab == this.tabAlreadyOperation)
            {
                this.RefreshAlreadyOperations();
            }
            
            //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
            if (this.isJoinUc)
            {
                //����ϲ�ͬʱ������������Ϣ
                this.ViewAnaesthesia();
            }
            return base.OnQuery(sender, neuObject);
        }
        /// <summary>
        /// ����������Ϣ
        /// //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
        /// </summary>
        /// <returns></returns>
        private int ViewAnaesthesia()
        {
            this.ucAnaesthesiaSpread1.Reset();
            Operation.ucAnaesthesia uc = new ucAnaesthesia();
            uc.IsJoinUc = this.isJoinUc;
            uc.BeginTime = this.neuDateTimePicker1.Value.Date;
            uc.EndTime = this.neuDateTimePicker1.Value.Date.AddDays(1);
            uc.RefreshApplys();
            this.apply = uc.Apply;
            if (this.apply.Count > 0)
            {
                foreach (OperationAppllication apply in this.apply)
                {
                    this.ucAnaesthesiaSpread1.AddOperationApplication(apply);
                }
            }
            return 1;
        }
        //{2D9FB02D-2AE2-42ac-AD5A-E2B12DC841CE}��ʼ�������б�����feng.ch
        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        /// <returns></returns>
        private int InitData()
        {
            try
            {
                ArrayList alRet = null;
                FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
                alRet = manager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                this.cmbDept.AddItems(alRet);
                alRet = new ArrayList();
                alRet = Environment.TableManager.GetRoomsByDept(Environment.OperatorDeptID);
                this.cmbRoom.AddItems(alRet);
                ArrayList alOperatoinOrder = Environment.IntegrateManager.GetConstantList("OperatoinOrder");
                OperatoinOrderHelper.ArrayObject = alOperatoinOrder;
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        protected override int OnSave(object sender, object neuObject)
        {
            //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
            if (!this.isJoinUc)
            {
                this.ucArrangementSpread1.Save();
            }
            else
            {
                #region �ϲ�����������������
                if (this.tabInformation.SelectedTab == this.tabOpertion &&   //��������
                        this.ucArrangementSpread1.Save() == 0)
                {
                   // DialogResult rs= MessageBox.Show(Language.Msg("�Ƿ�������?"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                   // if (rs == DialogResult.OK)
                   // {
                    //    this.tabInformation.SelectedTab = this.tabAnaesthesia; �������������ҳ�棬�������ź����������Ƿֿ���
                        //����ϲ���ͬʱ������������Ϣ
                        this.ViewAnaesthesia();
                        this.RefreshApplys();
                        return base.OnSave(sender, neuObject);
                   // }
                }
                if (this.tabInformation.SelectedTab == this.tabAnaesthesia &&   //������
                   this.ucAnaesthesiaSpread1.Save() == 0)
                {
                    // this.tabInformation.SelectedTab = this.tabOpertion;  �����������ŵ�ҳ�棬�������ź����������Ƿֿ���
                    this.ViewAnaesthesia();
                    this.RefreshApplys();
                    return base.OnSave(sender, neuObject);
                }
                #endregion
            }
            
            return base.OnSave(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "�Ѱ���")
            {
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Checked;
                this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("�����Ǽ�").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.Already;
            }
            else if (e.ClickedItem.Text == "δ����")
            {
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Checked;
                this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("�����Ǽ�").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.NotYet;
            }
            else if (e.ClickedItem.Text == "ȫ��")
            {
                this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Checked;
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("�����Ǽ�").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.All;
            }
            else if (e.ClickedItem.Text == "�����Ǽ�")
            {
                //this.toolBarService.GetToolButton("�����Ǽ�").CheckState = System.Windows.Forms.CheckState.Checked;
                //this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Unchecked;
                //this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Unchecked;
                //this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Unchecked;

                //this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.Operation;
                if (this.AlreadyOperation() < 0) return;
                this.RefreshApplys();
                this.RefreshAlreadyOperations();
                this.ViewAnaesthesia();
            }
            else if (e.ClickedItem.Text == "ȡ���Ǽ�")
            {
                //this.toolBarService.GetToolButton("�����Ǽ�").CheckState = System.Windows.Forms.CheckState.Checked;
                //this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Unchecked;
                //this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Unchecked;
                //this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Unchecked;

                //this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.Operation;
                if (this.CancelAlreadyOperation() < 0) return;
                this.RefreshApplys();
                this.RefreshAlreadyOperations();
                this.ViewAnaesthesia();
            }
            else if (e.ClickedItem.Text == "����")
            {
                if (this.ucArrangementSpread1.ChangeDept() < 0) return;
                this.RefreshApplys();
            }
            else if (e.ClickedItem.Text == "ȫѡ")
            {
                this.AllSelect();
            }
            else if (e.ClickedItem.Text == "ȫ��ѡ")
            {
                this.AllNotSelect();
            }
            else if (e.ClickedItem.Text == "��������")
            {
                if (this.ucArrangementSpread1.CancelOperation() == 1)
                {
                    this.RefreshApplys();
                }
                
            }
            //{2310330B-FDB2-42bf-9838-D52FA88CE529}
            else if (e.ClickedItem.Text == "�������")
            {
                if (this.ucArrangementSpread1.FinishOperation() == 1)
                {
                    this.RefreshApplys();
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        private void ucArrangementSpread1_applictionSelected(object sender, OperationAppllication e)
        {

            if (e != null)
            {


                if (e.PatientSouce == "2")
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = radtIntegrate.GetPatientInfomation(e.PatientInfo.ID);

                    if (patientInfo == null)
                    {
                        MessageBox.Show(radtIntegrate.Err);
                        this.ucArrangementInfo1.OperationApplication = new OperationAppllication();

                        return;
                    }
                    //{B23F2C3A-D774-48a7-BA82-D68F842E6C89}feng.ch���Ž��濴����Ӧ����������ʱ�����ں˶�
                    this.labApplyTime.Visible = true;
                    this.labApplyTime.Text = "����������ʱ��:"+e.ApplyDate.ToString();

                    //if ((FS.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == FS.HISFC.Models.Base.EnumInState.N
                    //    || (FS.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == FS.HISFC.Models.Base.EnumInState.O)
                    if (patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("�û����Ѿ���Ժ!", 111);
                        //this.ucArrangementInfo1.OperationApplication = new OperationAppllication();
                        return;
                    }

                }

                this.ucArrangementInfo1.OperationApplication = e;
            }
        }

        //ѡ�������δ�շ�ѡ� add by zhy
        private void ucAlreadyOperationSpread1_applictionSelected(object sender, OperationAppllication e)
        {

            if (e != null)
            {


                if (e.PatientSouce == "2")
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = radtIntegrate.GetPatientInfomation(e.PatientInfo.ID);

                    if (patientInfo == null)
                    {
                        MessageBox.Show(radtIntegrate.Err);
                        this.ucArrangementInfo1.OperationApplication = new OperationAppllication();

                        return;
                    }
                    //{B23F2C3A-D774-48a7-BA82-D68F842E6C89}feng.ch���Ž��濴����Ӧ����������ʱ�����ں˶�
                    this.labApplyTime.Visible = true;
                    this.labApplyTime.Text = "����������ʱ��:" + e.ApplyDate.ToString();

                    //if ((FS.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == FS.HISFC.Models.Base.EnumInState.N
                    //    || (FS.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == FS.HISFC.Models.Base.EnumInState.O)
                    if (patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("�û����Ѿ���Ժ!", 111);
                        //this.ucArrangementInfo1.OperationApplication = new OperationAppllication();
                        return;
                    }

                }

                this.ucArrangementInfo1.OperationApplication = e;
            }
        }

        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
        private void ucArrangement_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.neuDateTimePicker1.Value = System.DateTime.Now.Date.AddDays(1);
            this.tabInformation.SelectedTab = this.tabOpertion;
            if (!this.isJoinUc)
            {
                //�Ƴ�TABҳ
                this.tabInformation.Controls.Remove(this.tabAnaesthesia);
            }
            //{2D9FB02D-2AE2-42ac-AD5A-E2B12DC841CE}
            if (this.InitData() == -1)
            {
                MessageBox.Show(Language.Msg("�����б��ʼ��ʧ��!"));
                return;
            }
        }
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch������FPѡ����ʾ���߶�Ӧ��Ϣ
        private void ucAnaesthesiaSpread1_applictionSelected(object sender, OperationAppllication e)
        {
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
            if (e != null)
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                if (e.PatientSouce == "2")
                {
                    patientInfo = radtIntegrate.GetPatientInfomation(e.PatientInfo.ID);

                    if (patientInfo == null)
                    {
                        MessageBox.Show(radtIntegrate.Err);
                        this.ucArrangementInfo1.OperationApplication = new OperationAppllication();
                        return;
                    }
                    if (patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("�û����Ѿ���Ժ!", 111);
                        return;
                    }
                }
                this.ucArrangementInfo1.OperationApplication = e;
            }
        }
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch����TABҳ�л���ʱ��İ�ť��ʾ
        private void tabInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabInformation.SelectedIndex == 1)
            {
                this.ucAnaesthesiaSpread1.AnaesDocDept = this.anaesDocDept;//add by chengym 2012-7-11 ָ������ҽ���������ұ���
                
                this.toolBarService.GetToolButton("�Ѱ���").Visible = false;
                this.toolBarService.GetToolButton("δ����").Visible = false;
                this.toolBarService.GetToolButton("����").Visible = false;
                this.toolBarService.GetToolButton("ȫ��").Visible = false;
            }
            else
            {
                this.toolBarService.GetToolButton("�Ѱ���").Visible = true;
                this.toolBarService.GetToolButton("δ����").Visible = true;
                this.toolBarService.GetToolButton("����").Visible = true;
                this.toolBarService.GetToolButton("ȫ��").Visible = true;
            }

        }

        #region ѡ���ӡģʽ
        FS.FrameWork.Models.NeuObject obj = null;
        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            obj = new FS.FrameWork.Models.NeuObject();
            if (rb1.Checked)
            {
                obj.ID = rb1.Name;
                obj.Memo = "";
                this.cmbRoom.Enabled = false;
                this.cmbDept.Enabled = false;
                if (this.tabInformation.SelectedTab == this.tabOpertion)
                {
                    this.ucArrangementSpread1.SetPrintMode(obj);
                }
                else if (this.tabInformation.SelectedTab == this.tabAnaesthesia)
                {
                    this.ucAnaesthesiaSpread1.SetPrintMode(obj);
                }
            }
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            obj = new FS.FrameWork.Models.NeuObject();
            if (rb2.Checked)
            {
                obj.ID = rb2.Name;
                obj.Memo = "";
                this.cmbRoom.Enabled = false;
                this.cmbDept.Enabled = false;
                //this.ucArrangementSpread1.SetPrintMode(obj);
                if (this.tabInformation.SelectedTab == this.tabOpertion)
                {
                    this.ucArrangementSpread1.SetPrintMode(obj);
                }
                else if (this.tabInformation.SelectedTab == this.tabAnaesthesia)
                {
                    this.ucAnaesthesiaSpread1.SetPrintMode(obj);
                }
            }
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3.Checked)
            {
                this.cmbRoom.Enabled = true;
                this.cmbDept.Enabled = false;
            }
        }

        private void rb4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb4.Checked)
            {
                this.cmbRoom.Enabled = false;
                this.cmbDept.Enabled = true;
            }
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = rb3.Name;
            obj.Memo = this.cmbRoom.Text;
            //this.ucArrangementSpread1.SetPrintMode(obj);
            if (this.tabInformation.SelectedTab == this.tabOpertion)
            {
                this.ucArrangementSpread1.SetPrintMode(obj);
            }
            else if (this.tabInformation.SelectedTab == this.tabAnaesthesia)
            {
                this.ucAnaesthesiaSpread1.SetPrintMode(obj);
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = rb4.Name;
            obj.Memo = this.cmbDept.Text;
            //this.ucArrangementSpread1.SetPrintMode(obj);
            if (this.tabInformation.SelectedTab == this.tabOpertion)
            {
                this.ucArrangementSpread1.SetPrintMode(obj);
            }
            else if (this.tabInformation.SelectedTab == this.tabAnaesthesia)
            {
                this.ucAnaesthesiaSpread1.SetPrintMode(obj);
            }
        }
        
        #endregion

    }

    public class CompareByDeptAndSort : IComparer
    {
        public FS.FrameWork.Public.ObjectHelper OperatoinOrderHelper = new FS.FrameWork.Public.ObjectHelper();
        #region IComparer ��Ա
        public int Compare(object x, object y)
        {
            if ((x is FS.HISFC.Models.Operation.OperationAppllication) && (y is FS.HISFC.Models.Operation.OperationAppllication))
            {
                string ox = string.Empty;
                string oy = string.Empty;
                if (!string.IsNullOrEmpty((x as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID) && !string.IsNullOrEmpty((x as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID) && !string.IsNullOrEmpty((x as FS.HISFC.Models.Operation.OperationAppllication).RoomID) && !string.IsNullOrEmpty((y as FS.HISFC.Models.Operation.OperationAppllication).RoomID))
                {
                    decimal oxRoom = 0;
                    decimal oyRoom = 0;
                    try
                    {
                        oxRoom = FS.FrameWork.Function.NConvert.ToDecimal(Environment.TableManager.GetRoomByID((x as FS.HISFC.Models.Operation.OperationAppllication).RoomID).InputCode);
                        oyRoom = FS.FrameWork.Function.NConvert.ToDecimal(Environment.TableManager.GetRoomByID((y as FS.HISFC.Models.Operation.OperationAppllication).RoomID).InputCode);
                    }
                    catch{}
                    ox =
                        //(x as FS.HISFC.Models.Operation.OperationAppllication).PatientInfo.PVisit.PatientLocation.NurseCell.ID +
                      oxRoom.ToString() + (x as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.Name;

                    oy =
                        //(y as FS.HISFC.Models.Operation.OperationAppllication).PatientInfo.PVisit.PatientLocation.NurseCell.ID +
                       oyRoom.ToString() +(y as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.Name;
                }
                else
                {
                    ox = (x as FS.HISFC.Models.Operation.OperationAppllication).PatientInfo.PVisit.PatientLocation.NurseCell.ID + OperatoinOrderHelper.GetObjectFromName((x as FS.HISFC.Models.Operation.OperationAppllication).BloodUnit).ID;
                    oy = (y as FS.HISFC.Models.Operation.OperationAppllication).PatientInfo.PVisit.PatientLocation.NurseCell.ID + OperatoinOrderHelper.GetObjectFromName((y as FS.HISFC.Models.Operation.OperationAppllication).BloodUnit).ID;
                }
                return ox.CompareTo(oy);
            }
            return 1;
        }

        #endregion
    }

}
