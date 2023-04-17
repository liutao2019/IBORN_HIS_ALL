using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Operation;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Operation
{
    public partial class ucFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFee()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucFee_Load);
        }

        void ucFee_Load(object sender, EventArgs e)
        {
            if (!Environment.DesignMode)
                this.RefreshGroupList();
            //this.ucRegistrationTree1.ShowCanceled = false;
            //{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
            this.ucRegistrationTree1.ListType = this.ListType;
            this.ucFeeForm1.ListType = this.ListType;
            this.ucRegistrationTree1.Init();
            this.ucRegistrationTree1.ShowCanceled = false;

            Query(null, null);
        }

        #region �ֶ�
        FS.HISFC.BizProcess.Integrate.Manager groupManager = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #region {9FD18145-4049-4612-A8A7-A826E89426CD}
        /// <summary>
        /// ������������
        /// </summary>
        private ApplyType operateType = ApplyType.OperationAppllication;
        /// <summary>
        /// ��������
        /// </summary>
        private OperationAppllication operationAppllication = new OperationAppllication();
        /// <summary>
        /// �����Ǽ�
        /// </summary>
        private OperationRecord operationRecord = new OperationRecord();
        /// <summary>
        /// ����Ǽ�
        /// </summary>
        private AnaeRecord anaeRecord = new AnaeRecord();

        /// <summary>
        /// �Ƿ���Ҫ�������������������:true��Ҫ��false����Ҫ
        /// </summary>
        private bool isNeedApply = true;

        #endregion

        #endregion

        #region  ����
        [Category("�ؼ�����"), Description("���øÿؼ����ص���Ŀ��� ҩƷ:drug ��ҩƷ undrug ����: all")]
        public FS.HISFC.Components.Common.Controls.EnumShowItemType ������Ŀ���
        {
            get
            {
                return ucFeeForm1.������Ŀ���;
            }
            set
            {
                ucFeeForm1.������Ŀ��� = value;
            }
        }
        /// <summary>
        /// �ؼ�����
        /// </summary>
        [Category("�ؼ�����"), Description("��û������øÿؼ�����Ҫ����"), DefaultValue(1)]
        public FS.HISFC.Components.Common.Controls.ucInpatientCharge.FeeTypes �ؼ�����
        {
            get
            {
                return this.ucFeeForm1.�ؼ�����;
            }
            set
            {
                this.ucFeeForm1.�ؼ����� = value;
            }
        }

        /// <summary>
        /// �Ƿ�����շѻ��߻���0���۵���Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("��û��������Ƿ�����շѻ��߻���"), DefaultValue(false)]
        public bool IsChargeZero
        {
            get
            {
                return this.ucFeeForm1.IsChargeZero;
            }
            set
            {
                this.ucFeeForm1.IsChargeZero = value;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return this.ucFeeForm1.MessageType;
            }
            set
            {
                this.ucFeeForm1.MessageType = value;
            }
        }
        [Category("�ؼ�����"), Description("����Ϊ���Ƿ���ʾ��������")]
        public bool IsJudgeQty
        {
            get
            {
                return this.ucFeeForm1.IsJudgeQty;
            }
            set
            {
                this.ucFeeForm1.IsJudgeQty = value;
            }
        }
        [Category("�ؼ�����"), Description("ִ�п����Ƿ�Ĭ��Ϊ��½����")]
        public bool DefaultExeDeptIsDeptIn
        {
            get
            {
                return this.ucFeeForm1.DefaultExeDeptIsDeptIn;
            }
            set
            {
                this.ucFeeForm1.DefaultExeDeptIsDeptIn = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ������{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ������"), DefaultValue(false)]
        public bool IsShowFeeRate
        {
            get { return this.ucFeeForm1.IsShowFeeRate; }
            set
            {

                this.ucFeeForm1.IsShowFeeRate = value;
            }
        }
        //{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
        [Category("�ؼ�����"), Description("�ؼ����ͣ��������շ�")]
        public ucRegistrationTree.EnumListType ListType
        {
            get
            {
                return this.ucRegistrationTree1.ListType;
            }
            set
            {
                this.ucRegistrationTree1.ListType = value;
            }
        }

        /// <summary>
        /// �����շ��Ƿ�������ҽ�� ����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
        /// </summary>
        [Category("�ؼ�����"), Description("���� �����շ��Ƿ�������ҽ�� ")]
        public bool IsNeedUOOrder
        {
            get
            {
                return this.ucFeeForm1.IsNeedUOOrder;
            }
            set
            {
                this.ucFeeForm1.IsNeedUOOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ�ֻ��ʾ����ҽ�� ����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
        /// </summary>
        [Category("�ؼ�����"), Description("���� �Ƿ�ֻ��ʾ����ҽ�� ")]
        public bool IsOnlyUO
        {
            get
            {
                return this.ucFeeForm1.IsOnlyUO;
            }
            set
            {
                this.ucFeeForm1.IsOnlyUO = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ�������������������
        /// </summary>
        [Category("�ؼ�����"), Description("���� �Ƿ���������������������")]
        public bool IsNeedApply
        {
            get
            {
                return this.isNeedApply;
            }

            set
            {
                this.isNeedApply = value;
            }
        }

        [Category("�ؼ�����"), Description("���� Ĭ��ȡҩҩ���Ŀ��ұ���")]
        public string DefaultStorageDept
        {
            get
            {
                return this.ucFeeForm1.DefaultStorageDept;
            }
            set
            {
                this.ucFeeForm1.DefaultStorageDept = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ���ɿ����շ������б�
        /// </summary>
        /// <returns></returns>
        private int RefreshGroupList()
        {
            this.tvGroup.Nodes.Clear();

            TreeNode root = new TreeNode();
            root.Text = "ģ��";
            root.ImageIndex = 22;
            root.SelectedImageIndex = 22;
            tvGroup.Nodes.Add(root);

            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            //ArrayList groups = this.groupManager.GetValidGroupList(Environment.OperatorDeptID);
            ArrayList groups = this.groupManager.GetValidGroupListByRoot(Environment.OperatorDeptID);
            if (groups != null)
            {
                foreach (FS.HISFC.Models.Fee.ComGroup group in groups)
                {
                    //TreeNode Node = new TreeNode();
                    //Node.Text = group.Name;//ģ������
                    //Node.Tag = group.ID;//ģ�����
                    //Node.ImageIndex = 11;
                    //Node.SelectedImageIndex = 11;
                    //root.Nodes.Add(Node);
                    this.AddGroupsRecursion(root, group);
                }
            }
            root.Expand();

            return 0;
        }

        //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        private int AddGroupsRecursion(TreeNode parent, FS.HISFC.Models.Fee.ComGroup group)
        {

            ArrayList al = this.groupManager.GetGroupsByDeptParent("1", group.deptCode, group.ID);
            if (al.Count == 0)
            {
                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = group.Name;// +"[" + group.ID + "]";
                parent.Nodes.Add(newNode);

                return -1;
            }
            else
            {

                foreach (FS.HISFC.Models.Fee.ComGroup item in al)
                {
                    //if (item.ID == "aaa")
                    //{
                    //    MessageBox.Show("aaa");
                    //}
                    TreeNode newNode = new TreeNode();
                    newNode.Tag = group;
                    newNode.Text = group.Name;// +"[" + group.ID + "]";
                    parent.Nodes.Add(newNode);
                    return this.AddGroupsRecursion(newNode, item);
                }
            }


            return 1;
        }

        #endregion

        #region �¼�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "ɾ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            #region {FDD16528-9F0B-473a-869C-FFBCD66920C0}
            this.toolBarService.AddToolButton("�鿴��ϸ", "�鿴��������������ϸ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);

            #endregion
            this.toolBarService.AddToolButton("�ݴ�", "�ݴ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            return this.toolBarService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            DateTime BeginTime = Convert.ToDateTime(this.neuDateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime EndTime = Convert.ToDateTime(this.neuDateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59");
            //·־����2007-4-13 ȡ��С��ʼʱ���������ʱ��
            this.ucRegistrationTree1.RefreshList(BeginTime, EndTime);
            //this.ucRegistrationTree1.RefreshList(this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value);
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        ///  ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            #region {9FD18145-4049-4612-A8A7-A826E89426CD}
            int i = this.ucFeeForm1.Save();

            if (i > 0)
            {
                if (this.operateType == ApplyType.OperationAppllication)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int iReturn = Environment.OperationManager.UpdateOpsFee(this.operationAppllication.ID);

                    if (Environment.OperationManager.DoAnaeRecord(this.operationAppllication.ID, "4") < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����շѱ��ʧ�ܣ�" + Environment.OperationManager.Err);
                        return -1;
                    }

                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����շѱ��ʧ�ܣ�"+Environment.OperationManager.Err);
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                if (this.operateType == ApplyType.OperationRecord)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    Environment.RecordManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int iReturn = Environment.RecordManager.UpdateOpsFee(this.operationRecord.OperationAppllication.ID);

                    if (Environment.OperationManager.DoAnaeRecord(this.operationAppllication.ID, "4") < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����շѱ��ʧ�ܣ�" + Environment.OperationManager.Err);
                        return -1;
                    }

                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����շѱ��ʧ�ܣ�" + Environment.RecordManager.Err);
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                if (this.operateType == ApplyType.AnaeRecord)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    Environment.AnaeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int iReturn = Environment.AnaeManager.UpdateAnaeFee(this.anaeRecord.OperationApplication.ID);

                    if (Environment.OperationManager.DoAnaeRecord(this.operationAppllication.ID, "4") < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����շѱ��ʧ�ܣ�" + Environment.OperationManager.Err);
                        return -1;
                    }

                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����շѱ��ʧ�ܣ�" + Environment.AnaeManager.Err);
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }


            #endregion

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucFeeForm1.Print();
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.ucFeeForm1.Clear();
            }
            else if (e.ClickedItem.Text == "ɾ��")
            {
                this.ucFeeForm1.DelRow();
            }
            #region {FDD16528-9F0B-473a-869C-FFBCD66920C0}
            else if (e.ClickedItem.Text == "�鿴��ϸ")
            {
                if (this.operateType == ApplyType.OperationRecord)
                {
                    ucOpsQueryFee uc = new ucOpsQueryFee();
                    uc.InPatientNO = this.operationRecord.OperationAppllication.PatientInfo.ID;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����������Ϣ��ѯ";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                }
                if (this.operateType == ApplyType.OperationAppllication)
                {
                    ucOpsQueryFee uc = new ucOpsQueryFee();
                    uc.InPatientNO = this.operationAppllication.PatientInfo.ID;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����������Ϣ��ѯ";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                }
                if (this.operateType == ApplyType.AnaeRecord)
                {
                    ucOpsQueryFee uc = new ucOpsQueryFee();
                    uc.InPatientNO = this.anaeRecord.OperationApplication.PatientInfo.ID;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����������Ϣ��ѯ";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                }
            } 
            #endregion
            else if (e.ClickedItem.Text == "�ݴ�")
            {
                this.ucFeeForm1.TempSave();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }


        private void ucRegistrationTree1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag.GetType() == typeof(OperationAppllication))
            {
                PatientInfo patient = (e.Node.Tag as OperationAppllication).PatientInfo;
                this.ucQueryInpatientNo1.Text = patient.PID.PatientNO;
                this.ucFeeForm1.OperationAppllication = e.Node.Tag as OperationAppllication;

                #region {9FD18145-4049-4612-A8A7-A826E89426CD}
                this.operateType = ApplyType.OperationAppllication;
                this.operationAppllication = e.Node.Tag as OperationAppllication; 
                #endregion
            }
            else if (e.Node.Tag.GetType() == typeof(OperationRecord))
            {
                OperationAppllication application = (e.Node.Tag as OperationRecord).OperationAppllication;
                this.ucQueryInpatientNo1.Text = application.PatientInfo.PID.PatientNO;
                this.ucFeeForm1.OperationAppllication = application;

                #region {9FD18145-4049-4612-A8A7-A826E89426CD}
                this.operateType = ApplyType.OperationRecord;
                this.operationRecord = e.Node.Tag as OperationRecord; 
                #endregion
            }
            else if (e.Node.Tag.GetType() == typeof(AnaeRecord))
            {
                //OperationAppllication application = (e.Node.Tag as AnaeRecord).OperationAppllication;
                OperationAppllication application = (e.Node.Tag as AnaeRecord).OperationApplication;
                this.ucQueryInpatientNo1.Text = application.PatientInfo.PID.PatientNO;
                this.ucFeeForm1.OperationAppllication = application;

                #region {9FD18145-4049-4612-A8A7-A826E89426CD}
                this.operateType = ApplyType.AnaeRecord;
                this.anaeRecord = e.Node.Tag as AnaeRecord; 
                #endregion
            }

        }

        private void tvGroup_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FS.HISFC.Models.Fee.ComGroup comGroup = e.Node.Tag as FS.HISFC.Models.Fee.ComGroup;
            if (comGroup == null)
            {
                return;
            }
            this.ucFeeForm1.InsertGroup(comGroup.ID);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ucQueryInpatientNo1_myEvent()
        {
            if (ucQueryInpatientNo1.InpatientNo == "")
            {
                MessageBox.Show("û�иû�����Ϣ!", "��ʾ");
                ucQueryInpatientNo1.Focus();
                return;
            }
            try
            {
                //��������ʵ����
                if (this.IsNeedApply)
                {
                    FS.HISFC.Models.Operation.OperationAppllication apply = new FS.HISFC.Models.Operation.OperationAppllication();
                    //��ȡ�û���������Ϣ
                    //FS.HISFC.Management.Operator.Operator opMgr = new FS.HISFC.Management.Operator.Operator();
                    string operationNo = Environment.OperationManager.GetMaxByPatient(ucQueryInpatientNo1.InpatientNo);
                    if (operationNo == null || operationNo == "")
                    {
                        MessageBox.Show("�û���û�н�������!", "��ʾ");
                        ucQueryInpatientNo1.Focus();
                        return;
                    }
                    else
                    {
                        //����������Ż������ʵ��
                        apply = Environment.OperationManager.GetOpsApp(operationNo);
                        if (apply.ExecStatus == "4")
                        {
                            if (MessageBox.Show("�û���������" + (apply.OperationInfos[0] as OperationInfo).OperationItem.Name + "�����������շѼ�¼���Ƿ�����շ�?", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                               // return;
                            }
                            else
                            {
                                return;
                            }

                          //  ucQueryInpatientNo1.Focus();

 
                        }
                    }
                    if (apply == null) return;
                    //��ȡ������Ŀ��Ϣ
                    ucFeeForm1.OperationAppllication = apply;
                    this.operateType = ApplyType.OperationAppllication;
                    this.operationAppllication = apply;
                }
                else
                {

                    FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
                    FS.HISFC.Models.Operation.OperationAppllication apply = new OperationAppllication();
                    apply.PatientInfo = radtMgr.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
                    ucFeeForm1.OperationAppllication = apply;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡ����������Ϣʱ����!" + e.Message, "��ʾ");
                ucQueryInpatientNo1.Focus();
                return;
            }
        }

        #endregion
    }

    /// <summary>
    /// ������������
    /// {9FD18145-4049-4612-A8A7-A826E89426CD}
    /// </summary>
    public enum ApplyType
    {
        /// <summary>
        /// ������������
        /// </summary>
        OperationAppllication,
        /// <summary>
        /// �����Ǽ�����
        /// </summary>
        OperationRecord,
        /// <summary>
        /// ����Ǽ�����
        /// </summary>
        AnaeRecord
    }

}
