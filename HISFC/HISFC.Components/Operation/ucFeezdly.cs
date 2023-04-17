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
    public partial class ucFeezdly : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFeezdly()
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
            ucFeeForm1.InitDoctList();
            this.ucRegistrationTree1.Init();
            this.ucRegistrationTree1.ShowCanceled = false;

            Query(null, null);
        }

        #region 字段
        FS.HISFC.BizProcess.Integrate.Manager groupManager = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #region {9FD18145-4049-4612-A8A7-A826E89426CD}
        /// <summary>
        /// 批费申请类型
        /// </summary>
        private ApplyType operateType = ApplyType.OperationAppllication;
        /// <summary>
        /// 手术申请
        /// </summary>
        private OperationAppllication operationAppllication = new OperationAppllication();
        /// <summary>
        /// 手术登记
        /// </summary>
        private OperationRecord operationRecord = new OperationRecord();
        /// <summary>
        /// 麻醉登记
        /// </summary>
        private AnaeRecord anaeRecord = new AnaeRecord();

        /// <summary>
        /// 是否需要手术申请才能手术批费:true需要；false不需要
        /// </summary>
        private bool isNeedApply = true;

        #endregion

        #endregion

        #region  属性
        [Category("控件设置"), Description("设置该控件加载的项目类别 药品:drug 非药品 undrug 所有: all")]
        public FS.HISFC.Components.Common.Controls.EnumShowItemType 加载项目类别
        {
            get
            {
                return ucFeeForm1.加载项目类别;
            }
            set
            {
                ucFeeForm1.加载项目类别 = value;
            }
        }
        /// <summary>
        /// 控件功能
        /// </summary>
        [Category("控件设置"), Description("获得或者设置该控件的主要功能"), DefaultValue(1)]
        public FS.HISFC.Components.Common.Controls.ucInpatientCharge.FeeTypes 控件功能
        {
            get
            {
                return this.ucFeeForm1.控件功能;
            }
            set
            {
                this.ucFeeForm1.控件功能 = value;
            }
        }

        /// <summary>
        /// 不加载的药品性质// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        [Category("控件设置"), Description("不加载的药品性质，用“,”区分开")]
        public string NoAddDrugQuality
        {
            get
            {
                return this.ucFeeForm1.NoAddDrugQuality;
            }
            set
            {
                this.ucFeeForm1.NoAddDrugQuality = value;
            }
        }
        /// <summary>
        /// 是否可以收费或者划价0单价的项目
        /// </summary>
        [Category("控件设置"), Description("获得或者设置是否可以收费或者划价"), DefaultValue(false)]
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

        [Category("控件设置"), Description("是否判断欠费,Y：判断欠费，不允许继续收费,M：判断欠费，提示是否继续收费,N：不判断欠费")]
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
        [Category("控件设置"), Description("数量为零是否提示和允许保存")]
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
        [Category("控件设置"), Description("执行科室是否默认为登陆科室")]
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
        /// 是否显示比例项{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        [Category("控件设置"), Description("是否显示比例项"), DefaultValue(false)]
        public bool IsShowFeeRate
        {
            get { return this.ucFeeForm1.IsShowFeeRate; }
            set
            {

                this.ucFeeForm1.IsShowFeeRate = value;
            }
        }
        //{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
        [Category("控件设置"), Description("控件类型：麻醉还是收费")]
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

        [Category("控件设置"), Description("设置：手术收费时是否自动更新相关操作人员信息")]
        public bool IsUpdateOpsOper
        {
            get
            {
                return this.ucFeeForm1.IsUpdateOpsOper;
            }
            set
            {
                this.ucFeeForm1.IsUpdateOpsOper = value;
            }
        }

        /// <summary>
        /// 手术收费是否必须关联医嘱 增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
        /// </summary>
        [Category("控件设置"), Description("设置 手术收费是否必须关联医嘱 ")]
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
        /// 是否只显示手术医嘱 增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
        /// </summary>
        [Category("控件设置"), Description("设置 是否只显示手术医嘱 ")]
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
        /// 是否需要手术申请才能手术批费
        /// </summary>
        [Category("控件设置"), Description("设置 是否必须有手术申请才能批费")]
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

        [Category("控件设置"), Description("设置 默认取药药房的科室编码")]
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

        #region 方法
        /// <summary>
        /// 生成科室收费组套列表
        /// </summary>
        /// <returns></returns>
        private int RefreshGroupList()
        {
            this.tvGroup.Nodes.Clear();

            TreeNode root = new TreeNode();
            root.Text = "模板";
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
                    //Node.Text = group.Name;//模板名称
                    //Node.Tag = group.ID;//模板编码
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
                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = group.Name;// +"[" + group.ID + "]";
                parent.Nodes.Add(newNode);
                foreach (FS.HISFC.Models.Fee.ComGroup item in al)
                {
                    //if (item.ID == "aaa")
                    //{
                    //    MessageBox.Show("aaa");
                    //}
                    //TreeNode newNode = new TreeNode();
                    //newNode.Tag = group;
                    //newNode.Text = group.Name;// +"[" + group.ID + "]";
                    //parent.Nodes.Add(newNode);
                    //return this.AddGroupsRecursion(newNode, item);
                    this.AddGroupsRecursion(newNode, item);
                }
            }


            return 1;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            this.toolBarService.AddToolButton("删除", "删除", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            #region {FDD16528-9F0B-473a-869C-FFBCD66920C0}
            this.toolBarService.AddToolButton("查看明细", "查看患者手术批费明细", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询历史, true, false, null);

            #endregion
            this.toolBarService.AddToolButton("暂存", "暂存", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);

            this.toolBarService.AddToolButton("保存.", "保存.", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历本费, true, false, null);

            this.toolBarService.AddToolButton("确认", "确认", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
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
            //路志鹏　2007-4-13 取最小开始时间和最大结束时间
            this.ucRegistrationTree1.RefreshList(BeginTime, EndTime);
            //this.ucRegistrationTree1.RefreshList(this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value);
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        ///  保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            #region {9FD18145-4049-4612-A8A7-A826E89426CD}

            PatientInfo patient = null;
            //PatientInfo patient = Environment.RadtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            if (!string.IsNullOrEmpty(this.operationAppllication.PatientInfo.ID))
            {
                patient = Environment.RadtManager.GetPatientInfomation(this.operationAppllication.PatientInfo.ID);
            }
            else
            {
                patient = Environment.RadtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            }

            int i = this.ucFeeForm1.Save();

            if (i > 0)
            {
                if (this.ListType == ucRegistrationTree.EnumListType.Operation)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int iReturn = -1;
                    if (this.operationAppllication != null &&!string.IsNullOrEmpty(this.operationAppllication.ID))
                    {
                         iReturn = Environment.OperationManager.UpdateOpsFee(this.operationAppllication.ID);
                         if (Environment.OperationManager.DoAnaeRecord(this.operationAppllication.ID, "4") < 1)
                         {
                             FS.FrameWork.Management.PublicTrans.RollBack();
                             MessageBox.Show("更新收费标记失败！" + Environment.OperationManager.Err);
                             return -1;
                         }

                         if (iReturn < 0)
                         {
                             FS.FrameWork.Management.PublicTrans.RollBack();
                             MessageBox.Show("更新收费标记失败！" + Environment.OperationManager.Err);
                             return -1;
                         }
                    }
                    else if (this.ucFeeForm1.OperationAppllication != null && !string.IsNullOrEmpty(this.ucFeeForm1.OperationAppllication.ID))
                    {
                        iReturn = Environment.OperationManager.UpdateOpsFee(this.ucFeeForm1.OperationAppllication.ID);
                        if (iReturn < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新收费标记失败！" + Environment.OperationManager.Err);
                            return -1;
                        }
                    }

                    
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                if (this.ListType == ucRegistrationTree.EnumListType.Anaesthesia)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    Environment.RecordManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int iReturn = Environment.RecordManager.UpdateAnaeFee(this.ucFeeForm1.OperationAppllication.ID);

                    //if (Environment.OperationManager.DoAnaeRecord(this.operationAppllication.ID, "4") < 1)
                    //{
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("更新收费标记失败！" + Environment.OperationManager.Err);
                    //    return -1;
                    //}

                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新收费标记失败！" + Environment.RecordManager.Err);
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                //if (this.operateType == ApplyType.AnaeRecord)
                //{
                //    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //    Environment.AnaeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //    int iReturn = Environment.AnaeManager.UpdateAnaeFee(this.anaeRecord.OperationApplication.ID);

                //    if (Environment.OperationManager.DoAnaeRecord(this.operationAppllication.ID, "4") < 1)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("更新收费标记失败！" + Environment.OperationManager.Err);
                //        return -1;
                //    }

                //    if (iReturn < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("更新收费标记失败！" + Environment.AnaeManager.Err);
                //        return -1;
                //    }
                //    FS.FrameWork.Management.PublicTrans.Commit();
                //}
            }

            ArrayList  al = Environment.OperationManager.GetOpsAppListforzdly(patient, "2",DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            string alertstr = "";
            int counti = 0;

            foreach (OperationAppllication temp in al)
            {
                if (temp.IsValid && temp.ExecStatus != "4")
                {
                    counti = counti + 1;    
                }
            }

            if (counti > 0)
            {
                MessageBox.Show(patient.Name + "还有" + counti.ToString() + "个手术申请未批费。将限制出院。");
            }

            this.ucRegistrationTree1.RefreshList(Convert.ToDateTime(this.neuDateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00"), Convert.ToDateTime(this.neuDateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59"));
          
            #endregion

            this.ucQueryInpatientNo1.Select();
            this.ucQueryInpatientNo1.Focus();

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 打印
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
        /// 工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "清屏")
            {
                this.ucFeeForm1.Clear();
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.ucFeeForm1.DelRow();
            }
            #region {FDD16528-9F0B-473a-869C-FFBCD66920C0}
            else if (e.ClickedItem.Text == "查看明细")
            {
                if (this.operateType == ApplyType.OperationRecord)
                {
                    ucOpsQueryFee uc = new ucOpsQueryFee();
                    uc.InPatientNO = this.operationRecord.OperationAppllication.PatientInfo.ID;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "手术费用信息查询";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                }
                if (this.operateType == ApplyType.OperationAppllication)
                {
                    ucOpsQueryFee uc = new ucOpsQueryFee();
                    uc.InPatientNO = this.operationAppllication.PatientInfo.ID;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "手术费用信息查询";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                }
                if (this.operateType == ApplyType.AnaeRecord)
                {
                    ucOpsQueryFee uc = new ucOpsQueryFee();
                    uc.InPatientNO = this.anaeRecord.OperationApplication.PatientInfo.ID;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "手术费用信息查询";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                }
            } 
            #endregion
            else if (e.ClickedItem.Text == "暂存")
            {
                this.ucFeeForm1.TempSave();
            }
            else if (e.ClickedItem.Text == "保存.")
            {
                this.ucFeeForm1.TempSave();
            }
            else if (e.ClickedItem.Text == "确认")
            {
                this.OnSave(null, null);
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
                operationAppllication = application;

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
                operationAppllication = application;

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
            
            this.ucFeeForm1.Clear();
            this.operationAppllication = null;
            if (ucQueryInpatientNo1.InpatientNo == "")
            {
                MessageBox.Show("没有该患者信息!", "提示");
                ucQueryInpatientNo1.Focus();
                return;
            }
            try
            {
                //手术申请实体类
                if (this.IsNeedApply)
                {
                    FS.HISFC.Models.Operation.OperationAppllication apply = new FS.HISFC.Models.Operation.OperationAppllication();
                    //获取该患者手术信息
                    //FS.HISFC.Management.Operator.Operator opMgr = new FS.HISFC.Management.Operator.Operator();

                    this.ucFeeForm1.Clear();

                    PatientInfo patient = Environment.RadtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
                    //{757094AC-55CD-428c-8C81-BB1F3F76172D}

                    if (patient == null)
                    {
                        MessageBox.Show("住院号有错！");
                        return;
                    }


                    if (patient.PVisit.InState.ID.ToString() != "N" && patient.PVisit.InState.ID.ToString() != "O")
                    {
                       // this.ucApplicationForm1.PatientInfo = patient;
                    }
                    else
                    {
                        MessageBox.Show("患者不是在院状态！");
                        return;

                    }

                    System.Collections.ArrayList al;
                    // al = Environment.OperationManager.GetOpsAppList(dept.ID, begin, end, "1");

                    DateTime end = this.neuDateTimePicker2.Value.AddDays(1);

                    al = Environment.OperationManager.GetOpsAppListforzdly(patient, "2", end.ToString());
                    if (al == null || al.Count <= 0)
                    {
                        MessageBox.Show("该患者没有进行手术!", "提示");
                        ucQueryInpatientNo1.Focus();
                        return;
                    }


              

                    //string operationNo = Environment.OperationManager.GetMaxByPatient(ucQueryInpatientNo1.InpatientNo);
                    if (al.Count == 1)
                    {
                        //根据手术序号获得手术实体
                        apply = al[0] as OperationAppllication; //Environment.OperationManager.GetOpsApp(operationNo);
                        if (apply.ExecStatus == "4")
                        {
                            if (MessageBox.Show("该患者手术【" + (apply.OperationInfos[0] as OperationInfo).OperationItem.Name + "】申请已有收费记录，是否继续收费?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    else
                    {
                        
                        ArrayList tal = new ArrayList();
                        foreach(OperationAppllication temp in al)
                        {
                            //if (temp.ExecStatus == "4" || !temp.IsValid)
                            if(!temp.IsValid)
                                continue;
                            FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
                           
                            c.ID= Environment.GetDept(temp.PatientInfo.PVisit.PatientLocation.NurseCell.ID).Name;
                            c.Name=temp.PatientInfo.Name;
                            c.Memo=(temp.OperationInfos[0] as OperationInfo).OperationItem.Name;
                            c.SpellCode = temp.PatientInfo.ID;
                            c.WBCode = temp.ID;
                            c.UserCode = temp.ID;
                            tal.Add(c);
                        }
                        FS.HISFC.Models.Base.Const tobj = null;

                        if (tal.Count == 0)
                        {
                            MessageBox.Show("该患者已经没有挂单的手术申请!");
                            return;
                        }
                        else
                        {
                            FrameWork.WinForms.Forms.frmEasyChoose fc = new FS.FrameWork.WinForms.Forms.frmEasyChoose(tal);
                            fc.ShowDialog();

                             tobj = fc.Object as FS.HISFC.Models.Base.Const;
                            if (tobj == null || string.IsNullOrEmpty(tobj.WBCode))
                            {
                                MessageBox.Show("请选择一条手术申请记录!");
                                return;
                            }
                        }

                        apply = Environment.OperationManager.GetOpsApp(tobj.WBCode);
 
                    }

                    if (apply == null)
                    {
                        MessageBox.Show("查找手术申请出错。");
                        return;
                    }
                    //读取手术项目信息
                    this.operateType = ApplyType.OperationAppllication;
                    this.operationAppllication = apply;
                    ucFeeForm1.OperationAppllication = apply;
                   
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
                MessageBox.Show("获取患者手术信息时出错!" + e.Message, "提示");
                ucQueryInpatientNo1.Focus();
                return;
            }
        }

        #endregion
    }

    
}
