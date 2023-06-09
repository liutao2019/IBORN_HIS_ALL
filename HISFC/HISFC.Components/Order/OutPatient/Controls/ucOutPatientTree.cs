using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using FS.HISFC.Models.Nurse;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 门诊医生站患者列表树
    /// </summary>
    public partial class ucOutPatientTree : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOutPatientTree()
        {
            InitializeComponent();

            //添加事件
            this.Disposed += new EventHandler(ucOutPatientTree_Disposed);

            //this.Font = new Font(this.Font.FontFamily, 12);
            this.neuTreeView1.Font = new Font(neuTreeView1.Font.FontFamily, 12);
            this.neuTreeView2.Font = new Font(neuTreeView2.Font.FontFamily, 12);
        }

        #region 变量

        /// <summary>
        /// 用来标识是否选中的是患者节点
        /// </summary>
        private const string More = "..";

        /// <summary>
        /// 是否显示24小时内的患者
        /// </summary>
        bool isShow24HoursPatient = false;

        /// <summary>
        /// 是否右键显示复制整张处方功能
        /// </summary>
        bool isShowCopyRecipe = false;

        /// <summary>
        /// 是否右键显示删除整张处方功能
        /// </summary>
        bool isShowDeleteRecipe = false;

        /// <summary>
        /// 是否已诊状态
        /// </summary>
        private bool bAlreadyState = false;

        /// <summary>
        /// 是否启用分诊系统
        /// </summary>
        private bool isUseNurseArray;

        /// <summary>
        /// 采用分诊流程时医生站是否只加载进诊患者，否则查询所有患者
        /// {5F0AD2D0-16B4-4697-8A0F-41F5B8923BFE}
        /// </summary>
        private bool isOnlyShowAssignedPatient = false;

        /// <summary>
        /// 是否启用一个患者多重身份选择
        /// </summary>
        private bool isUserOnePersonMorePact = false;

        /// <summary>
        /// 是否启用根据时间获取已诊患者
        /// </summary>
        private bool isSelectSeePatientByDate = false;
        /// <summary>
        /// 诊室选择窗口
        /// </summary>
        private Forms.frmSelectRoom froom = null;

        /// <summary>
        /// 健康业务层
        /// </summary>
        //private FS.HISFC.BizLogic.HealthCard.HealthCardManager healthCardManager = new FS.HISFC.BizLogic.HealthCard.HealthCardManager();

        /// <summary>
        /// 健康卡实体
        /// </summary>
        //private FS.HISFC.BizLogic.HealthCard.HealthCard healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();

        /// <summary>
        /// 分诊科室
        /// </summary>
        private ArrayList alFZDept = new ArrayList();

        /// <summary>
        /// 查找卡号加载到医生未诊列表接口
        /// </summary>
        private SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPateintTree IOutPateintTree = null;

        /// <summary>
        /// 当前登陆人员
        /// </summary>
        private FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        /// <summary>
        /// 当前诊台
        /// </summary>
        private FS.FrameWork.Models.NeuObject currentRoom = null;

        /// <summary>
        /// 华南本地挂号管理
        /// </summary>
        private FS.SOC.HISFC.BizProcess.RegisterIntegrate.Register socRegMgr = new FS.SOC.HISFC.BizProcess.RegisterIntegrate.Register();

        /// <summary>
        /// 华南分诊业务管理
        /// </summary>
        private FS.SOC.HISFC.BizProcess.NurseIntegrate.Assign assignIntegrate = new FS.SOC.HISFC.BizProcess.NurseIntegrate.Assign();

        /// <summary>
        /// 门诊分诊接收叫号信息
        /// </summary>
        private FS.SOC.HISFC.CallQueue.Interface.INurseAssign INurseAssign = null;

        /// <summary>
        /// 分诊业务
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Queue myQueue = new FS.HISFC.BizLogic.Nurse.Queue();

        /// <summary>
        /// 分诊科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper alFZDeptHelper = new FS.FrameWork.Public.ObjectHelper();

        #region 增加叫号按钮

        /// <summary>
        /// 午别
        /// </summary>
        private ArrayList alNoon = new ArrayList();

        /// <summary>
        /// 午别
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper noonHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper markHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 队列延长时间
        /// </summary>
        private double extendTime = -1;

        /// <summary>
        /// 上次修改队列延长的操作时间
        /// </summary>
        private DateTime dtLastOper = DateTime.MinValue;

        #endregion

        #region 定义传出事件

        /// <summary>
        /// 实现双击效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="isDoubleClick">是否真的是双击树节点效果</param>
        public delegate void TreeDoubleClickHandler(object sender, ClickEventArgs e, bool isDoubleClick);

        public delegate void CopyRecipeByPatientTreeHandler(FS.HISFC.Models.Registration.Register selectRegister, int copyNum);

        public delegate void DeleteRecipeByPatientTreeHandler(FS.HISFC.Models.Registration.Register selectRegister);

        //患者树右键复制处方事件
        public event CopyRecipeByPatientTreeHandler CopyRecipeByPatientTree;

        //患者树右键删除处方事件
        public event DeleteRecipeByPatientTreeHandler DeleteRecipeByPatientTree;


        /// <summary>
        /// 双击树事件
        /// </summary>
        public event TreeDoubleClickHandler TreeDoubleClick;

        #endregion

        /// <summary>
        /// 门诊医生患者树历史处方查询方式（0：clinicno方式 。>0表示按照cardno方式查询，以数字代表几天以内）
        /// </summary>
        private int iSeeHistoryMode = 0;

        /// <summary>
        /// 是否门诊账户流程
        /// </summary>
        private bool isAccountMode = false;

        /// <summary>
        /// 是否管理员
        /// </summary>
        private bool isManager =CacheManager.LogEmpl.IsManager;

        /// <summary>
        /// 是否门诊账户流程
        /// </summary>
        public bool IsAccountMode
        {
            get
            {
                return isAccountMode;
            }
            set
            {
                isAccountMode = value;
            }
        }


        /// <summary>
        /// 是否已诊状态
        /// </summary>
        public bool BAlreadyState
        {
            get { return this.bAlreadyState; }
            set { this.bAlreadyState = value; }
        }



        /// <summary>
        /// 是否用颜色区分是否收费标记
        /// </summary>
        bool isShowIsFeeColor = false;

        /// <summary>
        /// 是否启用新的分诊流程
        /// </summary>
        bool isNewAssign = false;

        //{390EA9BE-1A9C-43da-B26B-08533FC00415}
        /// <summary>
        /// 标识哪个院区
        /// </summary>
        int hospitalRegion = 1;

        /// <summary>
        /// 标识哪个诊室
        /// </summary>
        string roomName = "";

        /// <summary>
        /// 是否医生站自动更新分诊队列
        /// </summary>
        bool isAutoDoctQueue = false;

        TreeNode patientNode = null;
        int image = 0;
        string accountType = "";
        string feeType = "";
        string before = "";

        //ArrayList al = null;
        FS.HISFC.Models.Registration.Register r = null;

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register myPatientInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 患者基本信息
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.myPatientInfo;
            }
            set
            {
                this.myPatientInfo = value;
            }
        }

        /// <summary>
        /// 当前诊台
        /// </summary>
        public FS.FrameWork.Models.NeuObject CurrRoom
        {
            get
            {
                return this.currentRoom;
            }
            set
            {
                this.currentRoom = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int InitControl()
        {
            if (DesignMode)
            {
                return 1;
            }
            Classes.LogManager.Write("【开始初始化门诊医生树列表】");

            this.ucQuerySeeNoByCardNo1.UseType = FS.HISFC.Components.Common.Controls.enumUseType.Doct;

            #region 控制参数

            //是否显示24小时患者
            this.isShow24HoursPatient = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ99", false, "1"));
            //是否右键显示复制整张处方功能
            this.isShowCopyRecipe = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ91", false, "0"));
            //是否右键显示删除整张处方功能
            this.isShowDeleteRecipe = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ88", false, "0"));

            isUseNurseArray = Classes.Function.IsUseNurseArray();

            this.iSeeHistoryMode = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("200301", false, "0"));


            isShowIsFeeColor = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ34", true, "0"));

            this.isOnlyShowAssignedPatient = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("200320", false, "1"));

            isUserOnePersonMorePact = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ10", false, "0"));

            isNewAssign = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ02", false, "0"));

            isAutoDoctQueue = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ09", false, "0"));

            //患者树点击已诊是否根据日期获取患者0否1是
            this.isSelectSeePatientByDate = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ52", true, "0"));

            this.ucQuerySeeNoByCardNo1.IsUserOnePersonMorePact = isUserOnePersonMorePact;
            if (this.isUserOnePersonMorePact)
            {
                this.neuPanel1.Size = new Size(200, 58);
            }
            else
            {
                this.neuPanel1.Size = new Size(200, 37);
            }

            IOutPateintTree = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree), typeof(SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPateintTree)) as SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPateintTree;
            
            INurseAssign = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.SOC.HISFC.CallQueue.Interface.INurseAssign)) as FS.SOC.HISFC.CallQueue.Interface.INurseAssign;

            #endregion

            this.neuTreeView1.AfterSelect += new TreeViewEventHandler(neuTreeView1_AfterSelect);
            this.neuTreeView1.DoubleClick += new EventHandler(neuTreeView1_DoubleClick);
            this.neuTreeView2.AfterSelect += new TreeViewEventHandler(neuTreeView1_AfterSelect);
            this.neuTreeView2.DoubleClick += new EventHandler(neuTreeView1_DoubleClick);
            this.neuTreeView1.MouseUp += new MouseEventHandler(neuTreeView1_MouseUp);
            this.neuTreeView2.MouseUp += new MouseEventHandler(neuTreeView1_MouseUp);
            this.neuTreeView2.Visible = false;

            this.ucQuerySeeNoByCardNo1.myEvents += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQuerySeeNoByCardNo1_myEvents);

            //填充卡类型
            ArrayList arrMarkType = CacheManager.GetConList("MarkType");
            markHelper.ArrayObject = arrMarkType;

            if (isUseNurseArray)
            {
                //获取分诊科室
                this.alFZDept = CacheManager.InterMgr.QueryFZDept();
                this.alFZDeptHelper.ArrayObject = alFZDept;

                //增加取消功能 2011-1-3 houwb{DA7F7F3C-C9A6-4bcf-9181-93A6238B13F7}
                if (this.SelectRoom() == -1)
                {
                    return -1;
                }

                this.LoadMenuSet();
            }

            #region 接口初始化

            if (this.INurseAssign != null)
            {
                string errInfo =string.Empty ;
                int rInt = this.INurseAssign.Init(FS.FrameWork.Management.Connection.Operator, null, null, null, null, null, ref errInfo);
            }

            #endregion

            Classes.LogManager.Write("【结束初始化门诊医生树列表】");

            return 1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加单个患者到未诊列表
        /// </summary>
        /// <param name="obj"></param>
        private void AddPatientToTree(FS.HISFC.Models.Registration.Register obj)
        {
            TreeNode patientNode = new TreeNode();

            if (obj.Sex.ID.ToString() == "F")//女
            {
                if (obj.IsBaby)
                {
                    image = 10;
                }
                else
                {
                    image = 6;
                }
            }
            else //男
            {
                if (obj.IsBaby)
                {
                    image = 8;
                }
                else
                {
                    image = 4;
                }
            }
            patientNode.ImageIndex = image;
            patientNode.SelectedImageIndex = image + 1;

            //bool isExpert = false;
            //FS.HISFC.Models.Registration.RegLevel reglv = Classes.Function.RegLevelHelper.GetObjectFromID(obj.DoctorInfo.Templet.RegLevel.ID) as FS.HISFC.Models.Registration.RegLevel;
            //if (reglv != null)
            //{
            //    isExpert = reglv.IsExpert;
            //}

            if (!obj.IsSee
                && (obj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre
                || obj.DoctorInfo.SeeDate.Date > obj.InputOper.OperTime.Date))
            {
                before = "预";
            }


            //if (obj.DoctorInfo.Templet.Doct.ID != "" && isExpert)
            //{
            //    patientNode.Text =  obj.Name + "【" + before + obj.OrderNO.ToString() + "】" +"*专*" + More;
            //}
            //else
            //{
            //2012-10-09 14:51:10 这个时间之前的全院序号有问题
            if (obj.InputOper.OperTime < new DateTime(2012, 10, 09, 14, 51, 10))
            {
                patientNode.Text = accountType + feeType + obj.Name + "【" + before + "*】" + "*" + obj.DoctorInfo.Templet.RegLevel.Name + "*" + More;
            }
            else
            {
                //if(FS.SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(obj.
                patientNode.Text = accountType + feeType + obj.Name + "【" + before + obj.OrderNO.ToString() + "(" + obj.DoctorInfo.SeeNO.ToString() + ")】" + "*" + obj.DoctorInfo.Templet.RegLevel.Name + "*" + More;
            }
            //}
            //obj.DoctorInfo.SeeNO = -1;

            patientNode.Tag = obj;

            this.neuTreeView1.Nodes[0].Nodes.Add(patientNode);
            this.neuTreeView1.ExpandAll();
            this.neuTreeView1.SelectedNode = patientNode;
        }

        /// <summary>
        /// 选择诊室
        /// </summary>
        private int SelectRoom()
        {
            Classes.LogManager.Write("【开始选择诊室】");
            try
            {
                if (!isUseNurseArray)
                {
                    return 1;
                }


                FS.FrameWork.Models.NeuObject obj = this.alFZDeptHelper.GetObjectFromID(this.employee.Dept.ID);
                if (obj == null)//不分诊科室
                {
                    return 1;
                }
                else
                {
                    //修改为根据科室查询分诊信息里面的诊室诊台信息
                    DateTime dtNow = CacheManager.SchemaMgr.GetDateTimeFromSysDateTime();
                    ArrayList alSchema = CacheManager.SchemaMgr.QueryByDeptAll(dtNow, CacheManager.LogEmpl.Dept.ID);
                    if (alSchema == null)
                    {
                        MessageBox.Show("查询诊室排班信息出错！\r\n" + CacheManager.SchemaMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return -1;
                    }

                    //{993C4984-D7C6-462c-A554-6BA7251E3D4B}
                    //只加载当前医生的诊室，如果只有一个诊室，则不用选择
                    ArrayList alRoom = new ArrayList();
                    foreach (FS.HISFC.Models.Registration.Schema schema in alSchema)
                    {
                        if (schema.Templet.Noon.ID == SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(dtNow).ID && FrameWork.Management.Connection.Operator.ID == schema.Templet.Doct.ID)
                        {
                            FS.FrameWork.Models.NeuObject room = new FS.FrameWork.Models.NeuObject();
                            room.ID = schema.Templet.Room.ID;
                            room.Name = schema.Templet.Room.Name;
                            if (!RoomContainsList(alRoom, room))
                                alRoom.Add(room);
                        }
                    }

                    if (alRoom.Count == 0)
                    {
                        if (MessageBox.Show("未找到排班对应的诊室信息，是否是临时出诊？\r\n\r\n临时出诊无法使用分诊叫号流程！","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                            currentRoom = seatObj;
                            return 1;
                        }
                        else
                        {
                            ArrayList alNurse = CacheManager.InterMgr.QueryNurseStationByDept(this.employee.Dept);
                            if (alNurse == null || alNurse.Count <= 0)
                            {
                                MessageBox.Show("获得所属诊区出错！\r\n请联系管理员检查科室关系对照是否正确！\r\n" + CacheManager.InterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return -1;
                            }
                            ArrayList al = new ArrayList();

                            foreach (FS.FrameWork.Models.NeuObject nurseObj in alNurse)
                            {
                                al.AddRange(CacheManager.InterMgr.QueryRoomByDeptID(nurseObj.ID));
                            }

                            //al = CacheManager.InterMgr.QueryRoomByDeptID((alNurse[0] as FS.FrameWork.Models.NeuObject).ID);
                            if (al == null || al.Count == 0)
                            {
                                MessageBox.Show("获取分诊台与科室对照关系出错！\r\n请检查科室关系对照是否正确！\r\n" + CacheManager.InterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return -1;
                            }

                            alRoom.AddRange(al);
                        }
                    }
                    else if (alRoom.Count >= 0)
                    {
                        if (alRoom.Count == 1)
                        {
                            ArrayList alSeat = CacheManager.InterMgr.QuerySeatByRoomNo((alRoom[0] as FS.FrameWork.Models.NeuObject).ID);
                            if (alSeat == null || alSeat.Count == 0)
                            {
                                if (MessageBox.Show("当前诊室未维护诊室，是否是临时出诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //this.currentRoom = new FS.FrameWork.Models.NeuObject();
                                    FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                                    currentRoom = seatObj;
                                    return 1;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else if (alSeat.Count == 1)
                            {
                                this.currentRoom = alSeat[0] as FS.HISFC.Models.Nurse.Seat;
                            }
                            else
                            {
                                if (froom == null)
                                {
                                    froom = new Forms.frmSelectRoom(alRoom);
                                }
                                froom.pValue = this.isUseNurseArray;
                                froom.alFZDepts = this.alFZDept;
                                froom.OKEvent += new FS.FrameWork.WinForms.Forms.OKHandler(froom_OKEvent);
                                DialogResult r = froom.ShowDialog();
                                if (r == DialogResult.OK)
                                {
                                    if (currentRoom == null)
                                    {
                                        if (MessageBox.Show("未选择诊室，是否是临时出诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                                            currentRoom = seatObj;
                                            return 1;
                                        }
                                        else
                                        {
                                            return -1;
                                        }
                                    }
                                    else
                                    {
                                        return 1;
                                    }
                                }
                                else if (r == DialogResult.Cancel)
                                {
                                    FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                                    currentRoom = seatObj;
                                    return 1;
                                }
                                else
                                {
                                    if (MessageBox.Show("未选择诊室，是否是临时出诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        //this.currentRoom = new FS.FrameWork.Models.NeuObject();
                                        FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                                        currentRoom = seatObj;
                                        return 1;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (froom == null)
                            {
                                froom = new Forms.frmSelectRoom(alRoom);
                            }
                            froom.pValue = this.isUseNurseArray;
                            froom.alFZDepts = this.alFZDept;
                            froom.OKEvent += new FS.FrameWork.WinForms.Forms.OKHandler(froom_OKEvent);
                            DialogResult r = froom.ShowDialog();
                            if (r == DialogResult.OK)
                            {
                                if (currentRoom == null)
                                {
                                    if (MessageBox.Show("未选择诊室，是否是临时出诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                                        currentRoom = seatObj;
                                        return 1;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else
                                {
                                    return 1;
                                }
                            }
                            else if (r == DialogResult.Cancel)
                            {
                                FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                                currentRoom = seatObj;
                                return 1;
                            }
                            else
                            {
                                if (MessageBox.Show("未选择诊室，是否是临时出诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //this.currentRoom = new FS.FrameWork.Models.NeuObject();
                                    FS.HISFC.Models.Nurse.Seat seatObj = new Seat();
                                    currentRoom = seatObj;
                                    return 1;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.currentRoom = new FS.FrameWork.Models.NeuObject();
                    }

                    Classes.LogManager.Write("【结束选择诊室】");

                    return 1;
                }
            }
            catch
            {
                MessageBox.Show("获得科室所属护理站出错", "提示");
                return -1;
            }
        }


        private bool RoomContainsList(ArrayList list, FS.FrameWork.Models.NeuObject room)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in list)
            {
                if (room.ID==obj.ID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 诊室窗口选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void froom_OKEvent(object sender, FS.FrameWork.Models.NeuObject e)
        {
            try
            {
                currentRoom = e;
            }
            catch { }
        }

        /// <summary>
        /// 补挂号
        /// </summary>
        public void AddNewReg()
        {
            this.ucQuerySeeNoByCardNo1.AddNewReg();

        }

        #region 患者列表

        /// <summary>
        /// 延迟分诊标记
        /// </summary>
        string assignDelayFlag = "";

        /// <summary>
        /// 是否自动刷新患者信息
        /// </summary>
        private bool isAutoFresh = false;

        /// <summary>
        /// 刷新待诊患者列表
        /// 保证诊台、诊室不维护时窗口可以关闭
        /// </summary>
        public int RefreshNoSeePatient()
        {
            ArrayList al = null;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在刷新待诊患者信息..", 0, false);
            Application.DoEvents();

            //查询所有当天，本科室 的待诊及已诊患者信息
            //
            DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            DateTime dtEnd = dtNow;

            #region　广医四院新的门诊分诊系统
            if (this.isNewAssign)
            {
                //分为待诊患者和科室患者 两个节点

                TreeNode nodePersonal = new TreeNode("待诊患者【" + employee.Name + "】");//待诊患者根
                nodePersonal.ImageIndex = 3;
                nodePersonal.SelectedImageIndex = 2;

                TreeNode nodeDept = new TreeNode("科室患者【" + employee.Dept.Name + "】");//待诊患者根
                nodeDept.ImageIndex = 3;
                nodeDept.SelectedImageIndex = 2;

                //分诊状态
                string assignFlag = "";

                if (isOnlyShowAssignedPatient)
                {
                    assignFlag = "'" + ((Int32)EnuTriageStatus.In).ToString() + "','" + ((Int32)EnuTriageStatus.Delay).ToString() + "'";
                }
                else
                {
                    assignFlag = "'" + ((Int32)EnuTriageStatus.Triage).ToString() + "','" + ((Int32)EnuTriageStatus.In).ToString() + "','" + ((Int32)EnuTriageStatus.Delay).ToString() + "'";
                }

                //科室未指定医师的患者列表
                ArrayList alDept = assignIntegrate.QuerySimpleAssignPatientByDeptID(dtNow.Date, dtNow.AddDays(1), assignFlag, this.employee.Dept.ID);
                if (alDept == null)
                {
                    MessageBox.Show("查询科室待诊患者出错！\r\n" + assignIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                //被指定为诊疗医师的患者列表
                ArrayList alPersonal = assignIntegrate.QuerySimpleAssignPatientByDoctID(dtNow.Date, dtNow.AddDays(1), assignFlag, this.employee.ID, this.employee.Dept.ID);
                if (alPersonal == null)
                {
                    MessageBox.Show("查询个人待诊患者出错！\r\n" + assignIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                object obj = null;

                if (alPersonal != null)
                {
                    //循环把未指定医师的患者加到“待诊患者”节点
                    nodePersonal.Text = nodePersonal.Text + "(" + alPersonal.Count + "人)";
                    for (int i = 0; i < alPersonal.Count; i++)
                    {
                        obj = alPersonal[i];
                        if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Assign))
                        {
                            AddPatientToRoot(nodePersonal, ((FS.HISFC.Models.Nurse.Assign)obj).Register);
                        }
                        else
                        {
                            AddPatientToRoot(nodePersonal, (FS.HISFC.Models.Registration.Register)obj);
                        }
                    }
                }

                if (alDept != null)
                {
                    nodeDept.Text = nodeDept.Text + "(" + alDept.Count + "人)";

                    //循环把未指定医师的患者加到“科室患者”节点
                    for (int i = 0; i < alDept.Count; i++)
                    {
                        obj = alDept[i];
                        if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Assign))
                        {
                            AddPatientToRoot(nodeDept, ((FS.HISFC.Models.Nurse.Assign)obj).Register);
                        }
                        else
                        {
                            AddPatientToRoot(nodeDept, (FS.HISFC.Models.Registration.Register)obj);
                        }
                    }
                }

                this.neuTreeView1.Nodes.Clear();
                this.neuTreeView1.Nodes.Add(nodePersonal);
                neuTreeView1.Nodes.Add(nodeDept);
            }
            #endregion

            #region 核心流程
            else
            {
                TreeNode nodePersonal = new TreeNode("待诊患者【" + employee.Name + "】");//待诊患者根
                nodePersonal.ImageIndex = 3;
                nodePersonal.SelectedImageIndex = 2;

                TreeNode nodeDept = new TreeNode("科室患者【" + employee.Dept.Name + "】");//待诊患者根
                nodeDept.ImageIndex = 3;
                nodeDept.SelectedImageIndex = 2;

                //分诊状态
                string assignFlag = "";
                assignDelayFlag = "";

                #region 分诊模式

                if (isUseNurseArray && this.alFZDeptHelper.GetObjectFromID(this.employee.Dept.ID) != null)
                {
                    if (this.currentRoom != null)
                    {
                        #region 加载个人分诊患者

                        //只查询进诊患者 {5F0AD2D0-16B4-4697-8A0F-41F5B8923BFE}
                        if (isOnlyShowAssignedPatient)
                        {
                            assignFlag = "'" + ((Int32)EnuTriageStatus.In).ToString() + "','" + ((Int32)EnuTriageStatus.Delay).ToString() + "'";
                            al = this.assignIntegrate.QuerySimpleAssignPatientByState(dtNow.Date, dtNow.AddDays(1), (this.currentRoom as FS.HISFC.Models.Nurse.Seat).PRoom.ID, assignFlag, this.employee.ID, employee.Dept.ID);
                        }
                        else
                        {
                            //查询分诊、进诊（正在看诊）、已诊、延迟看诊患者
                            //排除 已诊患者3
                            //al = CacheManager.InterMgr.QueryAssignPatientByState(dt.Date, dt.AddDays(1), this.currentRoom.ID, "'1','2',''", employee.ID);
                            assignFlag = "'" + ((Int32)EnuTriageStatus.Triage).ToString() + "','" + ((Int32)EnuTriageStatus.In).ToString() + "','" + ((Int32)EnuTriageStatus.Delay).ToString() + "'";
                            al = assignIntegrate.QuerySimpleAssignPatientByState(dtNow.Date, dtNow.AddDays(1), (this.currentRoom as FS.HISFC.Models.Nurse.Seat).PRoom.ID, assignFlag, employee.ID, employee.Dept.ID);
                        }

                        //查询以前患者的就诊信息
                        if (al == null)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("获得患者列表出错！" + assignIntegrate.Err);
                            return -1;
                        }

                        nodePersonal.Text = nodePersonal.Text + "(" + al.Count + "人)";

                        #region 延迟叫号患者放到最后显示

                        ArrayList alNomalAssign = new ArrayList();
                        ArrayList alDelayAssign = new ArrayList();

                        if (isUseNurseArray && this.alFZDeptHelper.GetObjectFromID(this.employee.Dept.ID) != null)
                        {
                            foreach (Assign assign in al)
                            {
                                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
                                {
                                    alDelayAssign.Add(assign);
                                }
                                else
                                {
                                    alNomalAssign.Add(assign);
                                }
                            }
                        }

                        #endregion

                        object obj = null;
                        for (int i = 0; i < alNomalAssign.Count; i++)
                        {
                            obj = alNomalAssign[i];
                            if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Assign))
                            {
                                AddPatientToRoot(nodePersonal, ((FS.HISFC.Models.Nurse.Assign)obj).Register);
                            }
                            else
                            {
                                AddPatientToRoot(nodePersonal, (FS.HISFC.Models.Registration.Register)obj);
                            }
                        }


                        assignDelayFlag = "[延]";
                        for (int i = 0; i < alDelayAssign.Count; i++)
                        {
                            obj = alDelayAssign[i];
                            if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Assign))
                            {
                                AddPatientToRoot(nodePersonal, ((FS.HISFC.Models.Nurse.Assign)obj).Register);
                            }
                            else
                            {
                                AddPatientToRoot(nodePersonal, (FS.HISFC.Models.Registration.Register)obj);
                            }
                        }
                        assignDelayFlag = "";

                        #endregion

                        #region 加载科室患者

                        //科室未指定医师的患者列表
                        ArrayList alDept = assignIntegrate.QuerySimpleAssignPatientByDeptID(dtNow.Date, dtNow.AddDays(1), assignFlag, this.employee.Dept.ID);

                        if (alDept != null)
                        {
                            nodeDept.Text = "科室患者【" + employee.Dept.Name + "】" + "(" + alDept.Count + "人)";

                            //循环把未指定医师的患者加到“科室患者”节点
                            for (int i = 0; i < alDept.Count; i++)
                            {
                                obj = alDept[i];
                                if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Assign))
                                {
                                    AddPatientToRoot(nodeDept, ((FS.HISFC.Models.Nurse.Assign)obj).Register);
                                }
                                else
                                {
                                    AddPatientToRoot(nodeDept, (FS.HISFC.Models.Registration.Register)obj);
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("获得诊室和诊台出错,请维护诊室和诊台！", "提示");
                        return -1;
                    }
                }
                #endregion

                #region 非分诊模式
                else
                {
                    if (this.currentRoom == null || string.IsNullOrEmpty(this.currentRoom.ID))
                    {
                        //al = CacheManager.RegInterMgr.QueryByDept(this.employee.Dept.ID, dt.Date, dt.AddDays(1), false);
                        al = socRegMgr.QuerySimpleRegByDept(employee.Dept.ID, dtNow.Date, dtNow.AddDays(1), "0", "1");

                        if (al == null)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("获得患者列表出错！" + socRegMgr.Err);
                            return -1;
                        }
                        else
                        {
                            //{8FE4C905-279D-48c7-9D1B-D0742556A102}
                            al.Sort(new regCompare());
                        }
                    }
                    else
                    {
                        assignFlag = "'" + ((Int32)EnuTriageStatus.Triage).ToString() + "','" + ((Int32)EnuTriageStatus.In).ToString() + "','" + ((Int32)EnuTriageStatus.Delay).ToString() + "'";
                        //al = CacheManager.InterMgr.QueryPatient(this.employee.Dept.ID, this.currentRoom.ID);
                        al = assignIntegrate.QuerySimpleAssignByAssignFlag(employee.Dept.ID, currentRoom.ID, assignFlag);

                        if (al == null)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("获得患者列表出错！" + CacheManager.InterMgr.Err);
                            return -1;
                        }
                    }

                    object obj = null;
                    for (int i = 0; i < al.Count; i++)
                    {
                        obj = al[i];
                        if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Assign))
                        {
                            if (((FS.HISFC.Models.Nurse.Assign)obj).Queue.Doctor.ID == employee.ID)
                            {
                                AddPatientToRoot(nodeDept, ((FS.HISFC.Models.Nurse.Assign)obj).Register);
                            }
                            else if (string.IsNullOrEmpty(((FS.HISFC.Models.Nurse.Assign)obj).Queue.Doctor.ID))
                            {
                                AddPatientToRoot(nodeDept, ((FS.HISFC.Models.Nurse.Assign)obj).Register);
                            }
                        }
                        else
                        {
                            if (((FS.HISFC.Models.Registration.Register)obj).DoctorInfo.Templet.Doct.ID == employee.ID)
                            {
                                AddPatientToRoot(nodePersonal, (FS.HISFC.Models.Registration.Register)obj);
                            }
                            else if (string.IsNullOrEmpty(((FS.HISFC.Models.Registration.Register)obj).DoctorInfo.Templet.Doct.ID))
                            {
                                AddPatientToRoot(nodeDept, (FS.HISFC.Models.Registration.Register)obj);
                            }
                        }
                    }
                }
                #endregion

                this.neuTreeView1.Nodes.Clear();
                this.neuTreeView1.Nodes.Add(nodePersonal);
                this.neuTreeView1.Nodes.Add(nodeDept);
            }
            #endregion

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //加载留观患者
            GetEnEmergencyPatient(neuTreeView1);
            this.neuTreeView1.ExpandAll();

            return 1;
        }

        /// <summary>
        /// 刷新已诊患者列表
        ///<param name="interval">根据时间段刷新已诊患者</param>
        /// </summary>
        public void RefreshSeePatient(params DateTime[] interval)
        {
            string sTemp = "";

            try
            {
                sTemp = this.employee.Dept.Name;
            }
            catch { }
            if (sTemp == "") sTemp = "已诊患者";
            else sTemp = "已诊患者【" + sTemp + "】";

            TreeNode nodeRoot = new TreeNode(sTemp);//已诊患者根
            nodeRoot.ImageIndex = 3;
            nodeRoot.SelectedImageIndex = 2;


            ArrayList al = null;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在更新已诊患者信息..", 0, false);
            Application.DoEvents();

            //查询所有当天，已诊患者信息
            DateTime dt = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            
            //按时间段查询已诊患者列表
            if (interval != null && interval.Length == 2)
            {
                al = CacheManager.RegInterMgr.QueryBySeeDocAndSeeDate(this.employee.ID, interval[0], interval[1], true);
            }
            else if (isShow24HoursPatient)//此处修改为查询最近24小时看诊的所有患者
            {
                al = CacheManager.RegInterMgr.QueryBySeeDocAndSeeDate(this.employee.ID, dt.AddDays(-1), dt.AddDays(2), true);
            }
            else
            {
                //凌晨时间段显示最近12小时内已诊患者
                if (dt.Hour >= 0 && dt.Hour <= 6)
                {
                    al = CacheManager.RegInterMgr.QueryBySeeDocAndSeeDate(this.employee.ID, dt.AddHours(-12), dt.AddDays(2), true);
                }
                else
                {
                    al = CacheManager.RegInterMgr.QueryBySeeDocAndSeeDate(this.employee.ID, dt.Date, dt.AddDays(2), true);
                }
            }

            //查询以前患者的就诊信息
            if (al == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.RegInterMgr.Err);
                return;
            }

            try
            {
                FS.HISFC.Models.Registration.Register obj = null;
                nodeRoot.Text = nodeRoot.Text + "(" + al.Count + "人)";
                for (int i = 0; i < al.Count; i++)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i + 1, al.Count);
                    Application.DoEvents();

                    obj = al[i] as FS.HISFC.Models.Registration.Register;

                    if (obj.IsSee)//已经看诊
                    {
                        AddPatientToRoot(nodeRoot, obj);
                    }
                    else//待看诊
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.neuTreeView2.Nodes.Clear();
            this.neuTreeView2.Nodes.Add(nodeRoot);

            //加载留观患者
            GetEnEmergencyPatient(neuTreeView2);
            this.neuTreeView2.ExpandAll();
        }

        /// <summary>
        /// 加载患者到树列表 
        /// </summary>
        /// <param name="rootNode"></param>
        private void AddPatientToRoot(TreeNode rootNode, FS.HISFC.Models.Registration.Register obj)
        {
            ///原来是显示所有挂到科室的患者,现在改为挂到该科室所有普通患者和挂到该医生的专家号,对于挂到其他
            ///医生的专家号不显示

            if (obj.DoctorInfo.Templet.Doct.ID != null && obj.DoctorInfo.Templet.Doct.ID != ""/*不是普通号*/
                && (obj.DoctorInfo.Templet.Doct.Name.IndexOf("教授") < 0
                && obj.DoctorInfo.Templet.Doct.ID != this.employee.ID)
                && !obj.IsSee && !isUseNurseArray)//挂号医生不是当前医生,返回
            {
                return;
            }

            //{7DFACF9E-AC03-4477-86C8-E75042BBBAD5}
            //只加载当前院区的患者（已诊和待诊都是）
            HISFC.BizLogic.Manager.Department deptManager = new HISFC.BizLogic.Manager.Department();
            HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();
            HISFC.Models.Registration.Register regobjtmp = registerManager.GetByClinic(obj.ID);

            if (registerManager == null)
            {
                return;
            }

            FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(regobjtmp.DoctorInfo.Templet.Dept.ID);
            FS.HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department deptempl = empl.Dept as FS.HISFC.Models.Base.Department;

            if (!dept.HospitalID.Equals(deptempl.HospitalID))
            {
                return;
            }


            patientNode = new TreeNode();

            if (obj.Sex.ID.ToString() == "F")//女
            {
                if (obj.IsBaby)
                {
                    image = 7;
                }
                else
                {
                    image = 5;
                }
            }
            else //男
            {
                if (obj.IsBaby)
                {
                    image = 6;
                }
                else
                {
                    image = 4;
                }
            }
            patientNode.ImageIndex = image;
            patientNode.SelectedImageIndex = image;


            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            if (obj.IsAccount)
            {
                //accountType = "账户挂号";
            }
            if (obj.IsFee == false)
            {
                //feeType = "未收费";
            }

            if (isShowIsFeeColor)
            {
                //已收费显示蓝色
                if (obj.IsSee == true)
                {
                    try
                    {
                        string strSQL = @"select count(*)
                                    from fin_opb_invoiceinfo m
                                    where m.clinic_code='{0}'";
                        strSQL = string.Format(strSQL, obj.ID);
                        string rev = CacheManager.OutOrderMgr.ExecSqlReturnOne(strSQL, "0");
                        if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                        {
                            patientNode.ForeColor = Color.Blue;
                        }
                    }
                    catch
                    { }
                    //List<FS.HISFC.Models.Fee.Outpatient.Balance> lstInvoice = null;

                    //DateTime dt = CacheManager.OrderMgr.GetDateTimeFromSysDateTime();
                    //int iTemp = outpatientManager.QueryInvoiceInfoByCardNo(obj.PID.CardNO, dt.AddDays(-1), dt, out lstInvoice);

                    //if (iTemp > 0)
                    //{
                    //    if (lstInvoice != null && lstInvoice.Count > 0)
                    //    {
                    //        patientNode.ForeColor = Color.Blue;
                    //    }
                    //}
                }
            }
            //清空下，不然效果你就想象不到了
            before = string.Empty;

            if (!obj.IsSee
                && (obj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre
                || obj.DoctorInfo.SeeDate.Date > obj.InputOper.OperTime.Date))
            {
                before = "预";
            }

            string display = "";


            //FS.HISFC.Models.Registration.RegLevel reglv = Classes.Function.RegLevelHelper.GetObjectFromID(obj.DoctorInfo.Templet.RegLevel.ID) as FS.HISFC.Models.Registration.RegLevel;


            //if (obj.DoctorInfo.Templet.Doct.ID != "" &&!object.Equals(reglv,null) && reglv.IsExpert)
            //{
            //    display = accountType + feeType + "*专*" + obj.Name + "【" + before + obj.DoctorInfo.SeeNO + "】" + More;
            //}
            //else
            //{
            //    display = accountType + feeType + obj.Name + "【" + before + obj.DoctorInfo.SeeNO + "】" + More;
            //}

            //2012-10-09 14:51:10 这个时间之前的全院序号有问题
            if (obj.InputOper.OperTime < new DateTime(2012, 10, 09, 14, 51, 10))
            {
                //{32C0913E-CB24-4cd5-986B-B3F9A9130998}
                //display = accountType + feeType + obj.Name + "【" + before + "*】" + "*" + obj.DoctorInfo.Templet.RegLevel.Name + "*" + More;
                display = obj.Name + "【" + obj.DoctorInfo.Templet.RegLevel.Name + "】";
            }
            else
            {
                //{32C0913E-CB24-4cd5-986B-B3F9A9130998}
                //display = accountType + feeType + obj.Name + "【" + before + obj.OrderNO.ToString() + "(" + obj.DoctorInfo.SeeNO.ToString() + ")】" + "*" + obj.DoctorInfo.Templet.RegLevel.Name + "*" + More;
                //display = obj.Name + "【" + obj.DoctorInfo.Templet.RegLevel.Name + "】";

                display = "{0}{1}{2}{3}";

                string sequenceNO = obj.SequenceNO < 0 ? "【过号】" : (obj.SequenceNO == 0 ? "【现场】" : "【" + obj.SequenceNO.ToString() + "】");
                string name = obj.Name;
                string firstSee = obj.FirstSeeFlag == "1" ? "【优先】" : "";
                string regLevel = "【" + obj.DoctorInfo.Templet.RegLevel.Name + "】";

                display = string.Format(display, firstSee, sequenceNO, name, regLevel);
            }

            patientNode.Text = assignDelayFlag + display;
            //obj.DoctorInfo.SeeNO = -1;

            patientNode.Tag = obj;

            //处理VIP患者颜色标记
            //门诊医生患者列表用绿色特殊标示优先患者。由护士领优先患者到医生站，门诊医生定位患者看诊，系统不做特殊处理
            if (IsVIP(obj.ID))
            {
                patientNode.ForeColor = Color.Blue;
            }

            //{6a58c5be-7255-430d-8422-89dba74ef4f5}
            if (obj.DoctorInfo.Templet.RegLevel.Name.Contains("急诊"))  
            {
                patientNode.ForeColor = Color.Red;
            }

            rootNode.Nodes.Add(patientNode);
        }

        private bool IsVIP(string patientID)
        {
            string sql = @"select patient_type from fin_opr_register where clinic_code='{0}'";

            sql = string.Format(sql, patientID);

            string strPatientType = CacheManager.ConManager.ExecSqlReturnOne(sql, "1");

            //PersonType 1 普通患者
            //PersonType 2 VIP患者
            if (strPatientType == "2")
            {
                return true;
            }

            //FS.FrameWork.Models.NeuObject con = CacheManager.ConManager.GetConstant("PersonType", strPatientType);
            //if (con != null
            //    && con.Name.Contains("VIP"))
            //{
            //    return true;
            //}

            return false;
        }

        /// <summary>
        ///  返回以前患者的就诊信息 
        /// </summary>
        /// <param name="patientNode"></param>
        private void GetOldSeeInfo(TreeNode patientNode)
        {
            string sTemp = "【{0}】【{1}】";

            DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            ArrayList al = null;
            if (this.iSeeHistoryMode == 0)
            {
                al = CacheManager.OutOrderMgr.QuerySeeNoListByCardNo(((FS.HISFC.Models.Registration.Register)patientNode.Tag).ID, ((FS.HISFC.Models.Registration.Register)patientNode.Tag).PID.CardNO);
            }
            else
            {
                ArrayList alOldTmp = CacheManager.OutOrderMgr.QuerySeeNoListByCardNo(((FS.HISFC.Models.Registration.Register)patientNode.Tag).PID.CardNO);

                if (alOldTmp == null)
                {
                    MessageBox.Show(CacheManager.OutOrderMgr.Err);
                    return;
                }

                al = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject obj in alOldTmp)
                {
                    if (FS.FrameWork.Function.NConvert.ToDateTime(obj.Memo) > dtNow.Date.AddDays(-this.iSeeHistoryMode))
                    {
                        al.Add(obj);
                    }
                }
            }

            if (patientNode.Nodes.Count > 0)
            {
                patientNode.Nodes.Clear();
            }
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                TreeNode node = new TreeNode();

                try
                {
                    node.Text = string.Format(sTemp, obj.Memo.Substring(0, obj.Memo.IndexOf(" ")), obj.User02);
                }
                catch
                {
                    node.Text = string.Format(sTemp, obj.Memo, obj.User02);
                }
                node.ImageIndex = 8;
                node.SelectedImageIndex = 9;
                r = ((FS.HISFC.Models.Registration.Register)patientNode.Tag).Clone() as FS.HISFC.Models.Registration.Register;

                r.DoctorInfo.SeeNO = int.Parse(obj.ID);
                node.Tag = r;
                patientNode.Nodes.Add(node);
                patientNode.ExpandAll();
            }
        }

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                FS.HISFC.Models.Registration.Register reg = new FS.HISFC.Models.Registration.Register();

                if (e.Node.Text.IndexOf(More) > 0)
                {
                    e.Node.Text = e.Node.Text.Replace(More, "");
                    this.GetOldSeeInfo(e.Node);
                }

                if (((TreeView)sender).SelectedNode.Tag != null)
                {
                    reg = (FS.HISFC.Models.Registration.Register)((TreeView)sender).SelectedNode.Tag;
                }
                else
                {
                    //reg = null;
                }

                if (e.Node.Level != 2)
                {
                    reg.DoctorInfo.SeeNO = -1;
                }

                this.myPatientInfo = reg;
                DoTreeDoubleClick(false);
            }
            catch { }
        }

        /// <summary>
        /// 刷新已诊患者单个节点
        /// 如果当前选择的不是已诊患者，则不刷新
        /// </summary>
        /// <returns></returns>
        public int FreshSeePatientNode()
        {
            if (neuTreeView2.Visible)
            {
                try
                {
                    FS.HISFC.Models.Registration.Register reg = new FS.HISFC.Models.Registration.Register();

                    if (neuTreeView2.SelectedNode.Tag != null)
                    {
                        reg = (FS.HISFC.Models.Registration.Register)neuTreeView2.SelectedNode.Tag;
                    }
                    else
                    {
                        //reg = null;
                    }
                    if (neuTreeView2.SelectedNode.Level == 1)
                    {
                        this.GetOldSeeInfo(neuTreeView2.SelectedNode);
                    }
                    else if (neuTreeView2.SelectedNode.Level == 2)
                    {
                        TreeNode node = neuTreeView2.SelectedNode.Parent;
                        if (node != null)
                        {
                            this.GetOldSeeInfo(node);
                        }
                    }
                }
                catch { }
            }
            return 1;
        }

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void neuTreeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                FS.HISFC.Models.Registration.Register reg = (FS.HISFC.Models.Registration.Register)((TreeView)sender).SelectedNode.Tag;
                if (((TreeView)sender).SelectedNode.Parent.Parent == null && reg != null)
                {
                    reg.DoctorInfo.SeeNO = -1;
                    this.GetOldSeeInfo(((TreeView)sender).SelectedNode);
                    myPatientInfo = reg;
                    if (((TreeView)sender).SelectedNode.Level != 2)
                    {
                        myPatientInfo.DoctorInfo.SeeNO = -1;
                        ((TreeView)sender).SelectedNode.Tag = myPatientInfo;
                    }
                    DoTreeDoubleClick(true);
                }
                this.myPatientInfo = reg;

            }
            catch { }
        }

        /// <summary>
        /// 设置新开立模式
        /// </summary>
        [Obsolete("作废", true)]
        public void SetNewOrder()
        {
            if (neuTreeView1.Visible)
            {
                TreeNode node = neuTreeView1.SelectedNode;
                neuTreeView1.SelectedNode = neuTreeView1.Nodes[0];
                if (node != null)
                {
                    if (node.Level == 1)
                    {
                        neuTreeView1.SelectedNode = node;
                    }
                    else if (node.Level == 2)
                    {
                        neuTreeView1.SelectedNode = node.Parent;
                    }
                }
            }
            if (neuTreeView2.Visible)
            {
                TreeNode node = neuTreeView2.SelectedNode;
                neuTreeView2.SelectedNode = neuTreeView2.Nodes[0];

                if (node != null)
                {
                    if (node.Level == 1)
                    {
                        neuTreeView2.SelectedNode = node;
                    }
                    else if (node.Level == 2)
                    {
                        neuTreeView2.SelectedNode = node.Parent;
                    }
                }
            }
        }

        /// <summary>
        /// 切换待诊、已诊列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            PatientStateConvert();
        }

        /// <summary>
        /// 患者状态 待诊/已诊转换
        /// </summary>
        private void PatientStateConvert()
        {
            bAlreadyState = !bAlreadyState;
            this.neuTreeView1.Visible = !bAlreadyState;
            this.neuTreeView2.Visible = bAlreadyState;
            if (bAlreadyState)//已诊
            {
                this.linkLabel1.Text = "待诊";
                if (this.isSelectSeePatientByDate)
                {
                    FS.FrameWork.WinForms.Forms.frmChooseDate chooseForm = new FS.FrameWork.WinForms.Forms.frmChooseDate();
                    chooseForm.ShowDialog();
                    this.RefreshSeePatientByDate(new DateTime[] { chooseForm.DateBegin, chooseForm.DateEnd });
                }
                else
                {
                    this.RefreshSeePatient();
                }
            }
            else//待诊
            {
                this.linkLabel1.Text = "已诊";
                this.RefreshNoSeePatient();
            }
        }


        /// <summary>
        /// 查询已诊患者按时间
        /// </summary>
        public void RefreshSeePatientByDate(params DateTime[] interval)
        {
            if (!bAlreadyState)
            {
                bAlreadyState = true;
            }
            this.neuTreeView1.Visible = !bAlreadyState;
            this.neuTreeView2.Visible = bAlreadyState;
            this.linkLabel1.Text = "待诊";
            this.RefreshSeePatient(interval);
        }


        /// <summary>
        /// 加载留观患者
        /// </summary>
        private void GetEnEmergencyPatient(FS.FrameWork.WinForms.Controls.NeuTreeView tree)
        {
            TreeNode nodeRoot = new TreeNode("留观患者");//留观患者根
            nodeRoot.ImageIndex = 3;
            nodeRoot.SelectedImageIndex = 2;
            nodeRoot.Tag = "Observance";
            ArrayList alPatient = CacheManager.RegInterMgr.PatientQueryByNurseCell(this.employee.Dept.ID);
            if (alPatient == null)
            {
                MessageBox.Show("加载留观患者信息失败！");
                return;
            }
            if (alPatient.Count == 0)
            {
                return;
            }

            foreach (FS.HISFC.Models.Registration.Register r in alPatient)
            {
                AddPatientToRoot(nodeRoot, r);
            }
            tree.Nodes.Add(nodeRoot);
        }

        #endregion

        /// <summary>
        /// 医生叫号进诊
        /// </summary>
        /// <param name="reg"></param>
        public void DiagIn(FS.HISFC.Models.Registration.Register reg)
        {
            FS.HISFC.BizProcess.Interface.IDiagInDisplay o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree), typeof(FS.HISFC.BizProcess.Interface.IDiagInDisplay)) as FS.HISFC.BizProcess.Interface.IDiagInDisplay;
            if (o == null)
            {
                MessageBox.Show("接口未实现");
            }
            else
            {
                o.RegInfo = reg;
                o.ObjRoom = currentRoom;

                o.DiagInDisplay();
            }
        }

        #region 增加叫号按钮

        /// <summary>
        /// 获取队列延时设置
        /// </summary>
        private void LoadMenuSet()
        {
            try
            {
                if (!System.IO.File.Exists(Application.StartupPath + "/Setting/ExtendQueue.xml"))
                {
                    Classes.Function.CreateXML(Application.StartupPath + "/Setting/ExtendQueue.xml", "-1", this.dtLastOper.ToString());
                }
                //是否延长队列时间 叫号的本地设置
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + "/Setting/ExtendQueue.xml");
                XmlNode node = doc.SelectSingleNode("//延长队列");

                if (node != null)
                {
                    this.extendTime = double.Parse(node.Attributes["ExtendTime"].Value);
                }

                node = doc.SelectSingleNode("//保存时间");
                if (node != null)
                {
                    this.dtLastOper = FS.FrameWork.Function.NConvert.ToDateTime(node.Attributes["LastOperTime"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string GetNoon(DateTime current)
        {
            if (this.alNoon == null) return "";
            /*
             * 理解错误：以为午别应该是包含一天全部时间上午：06~12,下午:12~18其余为晚上,
             * 实际午别为医生出诊时间段,上午可能为08~11:30，下午为14~17:30
             * 所以如果挂号员如果不在这个时间段挂号,就有可能提示午别未维护
             * 所以改为根据传人时间所在的午别例如：9：30在06~12之间，那么就判断是否有午别在
             * 06~12之间，全包含就说明9:30是那个午别代码
             */
            //			foreach(FS.HISFC.Models.Registration.Noon obj in alNoon)
            //			{
            //				if(int.Parse(current.ToString("HHmmss"))>=int.Parse(obj.BeginTime.ToString("HHmmss"))&&
            //					int.Parse(current.ToString("HHmmss"))<int.Parse(obj.EndTime.ToString("HHmmss")))
            //				{
            //					return obj.ID;
            //				}
            //			}


            if (this.alNoon == null || this.alNoon.Count == 0)
            {
                this.alNoon = CacheManager.RegInterMgr.QueryNoon();
                if (alNoon == null)
                {
                    MessageBox.Show("获取午别信息时出错!" + CacheManager.RegInterMgr.Err, "提示");
                    return "";
                }
                this.noonHelper.ArrayObject = this.alNoon;
            }

            int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            int begin = 0, end = 0;

            for (int i = 0; i < 3; i++)
            {
                if (zones[i, 0] <= time && zones[i, 1] > time)
                {
                    begin = zones[i, 0];
                    end = zones[i, 1];
                    break;
                }
            }

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (int.Parse(obj.StartTime.ToString("HHmmss")) >= begin &&
                    int.Parse(obj.EndTime.ToString("HHmmss")) <= end)
                {
                    return obj.ID;
                }
            }

            return "";
        }

        /// <summary>
        /// 取消进诊
        /// </summary>
        /// <returns></returns>
        public int CancelTriage()
        {
            //启用分诊程序
            if (this.isUseNurseArray && this.myPatientInfo != null)
            {
                FS.HISFC.Models.Nurse.Assign assign = null;
                assign = CacheManager.InterMgr.QueryAssignByClinicID(myPatientInfo.ID);

                //未找到分诊信息，直接退出
                if (assign == null)
                {
                    return 1;
                }
                else if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out)
                {
                    return 1;
                }
                //选中的是已进诊的，退出时更新为分诊状态
                else if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    int rtn = CacheManager.AssignMgr.CancelIn(assign.Register.ID, assign.Queue.Console.ID);
                    if (rtn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(CacheManager.InterMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    try
                    {
                        try
                        {
                            //判断是否不存在着路径配置文件
                            string strXMLPath = ".\\Setting\\OutDoctScreen.xml";
                            if (!System.IO.File.Exists(strXMLPath))
                            {
                                return 1;
                            }

                            //判断节点是否存在
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            doc.Load(strXMLPath);

                            System.Xml.XmlNode node = doc.SelectSingleNode("Config/Enable");
                            if (node == null)
                            {
                                return 1;
                            }

                            if (!string.IsNullOrEmpty(node.InnerText))
                            {
                                bool tmp = FS.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                                if (!tmp)
                                {
                                    return 1;
                                }
                            }
                            else
                            {
                                return 1;
                            }

                            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                            System.Xml.XmlNode node1 = doc.SelectSingleNode("Config/Region");
                            if (node1 != null)
                            {
                                if (!string.IsNullOrEmpty(node1.InnerText))
                                {
                                    try
                                    {
                                        this.hospitalRegion = FS.FrameWork.Function.NConvert.ToInt32(node1.InnerText);
                                    }
                                    catch
                                    {
                                        this.hospitalRegion = 1;
                                    }
                                }
                            }

                            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                            System.Xml.XmlNode node2 = doc.SelectSingleNode("Config/Room");
                            if (node2 != null)
                            {
                                if (!string.IsNullOrEmpty(node2.InnerText))
                                {
                                    try
                                    {
                                        this.roomName = node2.InnerText.ToString();
                                    }
                                    catch
                                    {
                                        this.roomName = "";
                                    }
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            this.hospitalRegion = 1;
                            this.roomName = "";
                        }

                        //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                        if (this.hospitalRegion == 1)
                        {
                            //if (FS.HISFC.Components.Order.Classes.Function.Queue_Show(myPatientInfo.ID, myPatientInfo.Name, assign.SeeNO) < 0)
                            //{

                            //}
                            string resultCode = "";
                            string resultDesc = "";
                            if (FS.HISFC.Components.Order.Classes.WSHelper.CallOut(myPatientInfo.ID, out resultCode, out resultDesc) > 0)
                            {
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                    }

                    this.RefreshNoSeePatient();
                    return 1;
                }
                return 1;
            }
            return 1;
        }

        #region 移除门诊医生队列
        /// <summary>
        /// 医生退出时移除该医生在门诊队列中的位置(用于新分诊流程)
        /// </summary>
        private void RemoveDoctQueue()
        {
            if (this.CurrRoom == null)
            {
                MessageBox.Show("当前诊台为空，将无法叫号和接受护士分诊患者。\n请通知信息科将门诊科室结构维护正确。");
                return;
            }

            FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

            queue.SRoom.ID = this.CurrRoom.ID;
            queue.Doctor.ID = this.employee.ID;
            queue.Noon.ID = this.GetNoon(this.myQueue.GetDateTimeFromSysDateTime());

            this.myQueue.DeleteDoctQueue(queue);
        }
        #endregion


        /// <summary>
        /// 叫号进诊
        /// </summary>
        /// <param name="regObj"></param>
        public void AutoTriage()
        {

            if (this.isNewAssign)
            {
                FS.HISFC.Models.Nurse.Assign assignPatient = null;

                assignPatient = CacheManager.InterMgr.QueryAssignByClinicID(myPatientInfo.ID);

                string docName = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(assignPatient.Queue.Doctor.ID);

                if (assignPatient.TriageStatus == EnuTriageStatus.In && !string.Equals(assignPatient.Queue.Doctor.ID, employee.ID))
                {
                    MessageBox.Show("患者已被【" + docName + "】叫过号，请选择另一个患者叫号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                try
                {
                    if (assignIntegrate.UpdateRegistrationAssign(myPatientInfo.ID, this.employee.ID, CacheManager.AssignMgr.GetDateTimeFromSysDateTime()) == -1)
                    {
                        return;
                    }
                    object[] args = {myPatientInfo.ID, this.currentRoom.ID,this.currentRoom.Name, this.employee.ID,
                                            CacheManager.AssignMgr.GetDateTimeFromSysDateTime() , (int)FS.HISFC.Models.Nurse.EnuTriageStatus.In};
                    if (CacheManager.AssignMgr.UpdateAssignAfterCall(args) == -1)
                    {

                    }

                    MessageBox.Show("患者【" + myPatientInfo.Name + "】叫号成功。");
                    return;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新分诊表信息失败。" + ex.Message);
                    return;
                }
            }




            //启用分诊程序
            if (this.isUseNurseArray)
            {
                FS.HISFC.Models.Nurse.Assign assign = null;
                if (this.currentRoom != null)
                {
                    //根据当前时间判断队列是否有效
                    DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                    DateTime dtBegin = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.ToShortDateString() + " 00:00:00");
                    DateTime dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.ToShortDateString() + " 23:59:59");
                    string noonID = "";
                    if (this.dtLastOper.Date == dtNow.Date && this.extendTime > 0)
                    {
                        noonID = this.GetNoon(dtNow.AddMinutes(-this.extendTime));
                    }
                    else
                    {
                        noonID = this.GetNoon(dtNow);
                    }
                    string queueID = CacheManager.InterMgr.QueryQueueID(this.currentRoom.ID, noonID, dtNow.ToString());
                    if (queueID == null)
                    {
                        MessageBox.Show(CacheManager.InterMgr.Err);
                        return;
                    }
                    else if (queueID == "")
                    {
                        if (this.dtLastOper.Date != dtNow.Date || this.extendTime <= 0)
                        {
                            //取下个午别
                            noonID = (FS.FrameWork.Function.NConvert.ToInt32(noonID) + 1).ToString();
                            extendTime = this.GetExtenQueueTime(this.currentRoom.ID, noonID, dtNow, out queueID);
                            if (extendTime == -1)//不延长队列时间或者分诊到紧邻队列
                            {
                                if (queueID == "")//不继续看诊
                                {
                                    return;
                                }
                            }
                            else//延长当前队列时间
                            {
                                noonID = this.GetNoon(dtNow.AddMinutes(-extendTime));
                                queueID = CacheManager.InterMgr.QueryQueueID(this.currentRoom.ID, noonID, dtNow.ToString());
                            }
                        }
                    }

                    if (this.isOnlyShowAssignedPatient || this.myPatientInfo == null)
                    {
                        assign = CacheManager.InterMgr.QueryWait(queueID, dtBegin, dtEnd);

                        if (assign == null)
                        {
                            MessageBox.Show("未找到分诊患者，请联系护士分诊!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        //队列有效-开始赋值  诊台诊室不变 进诊时间取当前时间
                        assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;//进诊
                    }
                    else
                    {
                        //选中的是已分诊未进诊的，则进诊 {37B12182-D44B-4ecb-9BA6-A6ECD310C7C0}
                        if (this.myPatientInfo != null)
                        {
                            assign = CacheManager.InterMgr.QueryAssignByClinicID(myPatientInfo.ID);
                            if (assign != null && assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out)
                            {
                                return;
                            }
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    try
                    {
                        //如果患者没有进诊信息，直接退出
                        if (assign == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return;
                        }

                        int rtn = CacheManager.InterMgr.Update(assign.Register.ID, assign.Queue.SRoom, assign.Queue.Console, dtNow);
                        if (rtn == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(CacheManager.InterMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (rtn == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            //MessageBox.Show("该患者分诊状态已经改变,请重新刷新屏幕!", "提示");
                            return;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                        //this.RefreshTreeView();

                        
                    }
                    catch (Exception error)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //{0BEB97B8-373D-45d0-A186-12502DD0AADE}
                    try
                    {
                        try
                        {
                            //判断是否不存在着路径配置文件
                            string strXMLPath = ".\\Setting\\OutDoctScreen.xml";
                            if (!System.IO.File.Exists(strXMLPath))
                            {
                                return;
                            }

                            //判断节点是否存在
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            doc.Load(strXMLPath);

                            System.Xml.XmlNode node = doc.SelectSingleNode("Config/Enable");
                            if (node == null)
                            {
                                return;
                            }

                            if (!string.IsNullOrEmpty(node.InnerText))
                            {
                                bool tmp = FS.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                                if (!tmp)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }

                            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                            System.Xml.XmlNode node1 = doc.SelectSingleNode("Config/Region");
                            if (node1 != null)
                            {
                                if (!string.IsNullOrEmpty(node1.InnerText))
                                {
                                    try
                                    {
                                        this.hospitalRegion = FS.FrameWork.Function.NConvert.ToInt32(node1.InnerText);
                                    }
                                    catch
                                    {
                                        this.hospitalRegion = 1;
                                    }
                                }
                            }

                            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                            System.Xml.XmlNode node2 = doc.SelectSingleNode("Config/Room");
                            if (node2 != null)
                            {
                                if (!string.IsNullOrEmpty(node2.InnerText))
                                {
                                    try
                                    {
                                        this.roomName = node2.InnerText.ToString();
                                    }
                                    catch
                                    {
                                        this.roomName = "";
                                    }
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            this.hospitalRegion = 1;
                            this.roomName = "";
                        }

                        //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                        if (this.hospitalRegion == 1)
                        {
                            //if (FS.HISFC.Components.Order.Classes.Function.Queue_Show(myPatientInfo.ID, myPatientInfo.Name, assign.SeeNO) < 0)
                            //{

                            //}
                            string resultCode = "";
                            string resultDesc = "";
                            if (FS.HISFC.Components.Order.Classes.WSHelper.CallIn(myPatientInfo.ID, out resultCode, out resultDesc) > 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + myPatientInfo.Name + "】进诊！", System.Windows.Forms.ToolTipIcon.Info);
                            }


                            //Hashtable ht = new Hashtable();

                            //ht.Add("DoctorID", FS.FrameWork.Management.Connection.Operator.ID.ToString());
                            //ht.Add("DoctorName", ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name);
                            //ht.Add("ClinicName", roomName);
                            //ht.Add("PatientID", myPatientInfo.ID);
                            //ht.Add("PatientName", myPatientInfo.Name);
                            //ht.Add("IPaddress", roomName);
                            //ht.Add("Jiaohao", "开立");
                            //bool success = FS.HISFC.Components.Order.Classes.Function.PostWebServiceByJson("http://192.168.34.14:81/WebServiceTerminalCall.asmx", "Call", ht, false);
                            //if (success)
                            //{
                            //    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + myPatientInfo.Name + "】进诊！", System.Windows.Forms.ToolTipIcon.Info);
                            //}

                        }
                        else if (this.hospitalRegion == 2)
                        {
                            Hashtable ht = new Hashtable();

                            ht.Add("DoctorID", FS.FrameWork.Management.Connection.Operator.ID.ToString());
                            ht.Add("DoctorName", ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name);
                            ht.Add("ClinicName", roomName);
                            ht.Add("PatientID", myPatientInfo.ID);
                            ht.Add("PatientName", myPatientInfo.Name);
                            ht.Add("IPaddress", roomName);
                            ht.Add("Jiaohao", "开立");
                            bool success = FS.HISFC.Components.Order.Classes.Function.PostWebServiceByJson("http://10.20.20.251:81/WebServiceTerminalCall.asmx", "Call", ht, false);
                            if (success)
                            {
                                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + myPatientInfo.Name + "】进诊！", System.Windows.Forms.ToolTipIcon.Info);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 算法描述：
        /// （紧邻队列：上午-下午，下午-晚上）
        /// 1.当天还存在紧邻队列，则提示是否分诊到紧邻队列
        /// 2.当天不存在紧邻队列，则提示是否延长分诊时间，延长的队列时间存放在本地
        /// 获取队列延长时间
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="Noon"></param>
        /// <param name="dt"></param>
        /// <param name="queueID"></param>
        private double GetExtenQueueTime(string roomID, string noonID, DateTime dt, out  string queueID)
        {
            double extendTime = -1;
            //查询队列ID
            queueID = CacheManager.InterMgr.QueryQueueID(roomID, noonID, dt.ToString());
            if (queueID == null)
            {
                MessageBox.Show(CacheManager.InterMgr.Err);
                return -1;
            }
            else if (queueID == "")//未找到满足条件的队列，提示是否延长队列时间
            {
                //DialogResult dr = MessageBox.Show("队列时间已结束，是否延长队列时间？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.No)
                //{
                //    return -1;
                //}
                //ucExtendQueueTime ucExtendTime = new ucExtendQueueTime();
                //ucExtendTime.ExtendTime = this.extendTime;
                //FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "延长队列";
                //FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucExtendTime);
                //if (ucExtendTime.Dr == DialogResult.Cancel)
                //{
                //    return -1;
                //}
                //extendTime = ucExtendTime.ExtendTime;

                extendTime = 1;
            }
            else//找到满足条件的队列
            {
                string noonName = this.noonHelper.GetName(noonID);
                //DialogResult dr = MessageBox.Show("队列时间已过，是否将患者分入" + noonName + "队列？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.No)//若不愿意将患者分入下个队列，提示是否延长队列时间
                //{
                //    dr = MessageBox.Show("是否延长队列时间？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dr == DialogResult.No)
                //    {
                //        queueID = "";
                //        return -1;
                //    }
                //    ucExtendQueueTime ucExtendTime = new ucExtendQueueTime();
                //    ucExtendTime.ExtendTime = this.extendTime;
                //    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "延长队列";
                //    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucExtendTime);
                //    if (ucExtendTime.Dr == DialogResult.Cancel)
                //    {
                //        queueID = "";
                //        return -1;
                //    }
                //    extendTime = ucExtendTime.ExtendTime;
                //}

                extendTime = 1;
            }

            return extendTime;
        }

        #region 排队叫号

        /// <summary>
        /// 选中待诊列表中根节点。
        /// </summary>
        private void SelectNoSeePatientRootTreeNode()
        {
            if (this.neuTreeView1.Nodes.Count > 0)
            {
                this.neuTreeView1.SelectedNode = this.neuTreeView1.Nodes[0];
            }
        }
        
        /// <summary>
        /// 返回指定挂号Id的树节点对象。
        /// </summary>
        /// <param name="clinicCode">挂号流水号</param>
        /// <param name="isSaw">是否已诊列表</param>
        /// <returns></returns>
        /// {4316de20-69b3-40e2-80ac-7033f60cd1ed}
        private TreeNode FindPatientTreeNode(string clinicCode, bool isSaw)
        {
            if (string.IsNullOrEmpty(clinicCode))
            {
                return null;
            }

            TreeView treeViewControl = (isSaw ? this.neuTreeView2 : this.neuTreeView1);
            if (treeViewControl == null)
            {
                return null;
            }

            return this.FindRegisterTreeNode(clinicCode, treeViewControl.Nodes);
        }

        /// <summary>
        /// 查询指定的门诊患者的节点。
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="nodeCollection"></param>
        /// <returns></returns>
        /// {4316de20-69b3-40e2-80ac-7033f60cd1ed}
        private TreeNode FindRegisterTreeNode(string clinicCode, TreeNodeCollection nodeCollection)
        {
            if (nodeCollection == null || string.IsNullOrEmpty(clinicCode))
            {
                return null;
            }

            foreach (TreeNode node in nodeCollection)
            {
                if (node.Tag != null)
                {
                    FS.HISFC.Models.Registration.Register register = node.Tag as FS.HISFC.Models.Registration.Register;
                    if (register != null && register.ID == clinicCode)
                    {
                        return node;
                    }
                }

                TreeNode findNode = this.FindRegisterTreeNode(clinicCode, node.Nodes);
                if (findNode != null)
                {
                    return findNode;
                }
            }

            return null;
        }

        /// <summary>
        /// 叫号
        /// </summary>
        public void Call()
        {
            #region 采用新的叫号流程

            if (INurseAssign != null)
            {
                FS.HISFC.Models.Registration.Register callReg = null;
                FS.HISFC.Models.Nurse.Assign assignPatient = null;

                if (!string.IsNullOrEmpty(myPatientInfo.ID)
                    && !myPatientInfo.IsSee)
                {
                    assignPatient = CacheManager.InterMgr.QueryAssignByClinicID(myPatientInfo.ID);

                    // {1af52e02-7c99-44c0-8800-812c68f59014} 判定取消分诊的情况，提示用户刷新数据。
                    if (assignPatient == null)
                    {
                        MessageBox.Show("患者可能已被取消分诊，请刷新数据重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }

                    string docName = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(assignPatient.Queue.Doctor.ID);
                    if (assignPatient.TriageStatus == EnuTriageStatus.In 
                        && !string.Equals(assignPatient.Queue.Doctor.ID, employee.ID) 
                        && !string.IsNullOrEmpty(docName))
                    {
                        MessageBox.Show("患者已被其他医生叫过号，请选择另一个患者叫号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        this.SelectNoSeePatientRootTreeNode();
                        return;
                    }
                }

                if (this.neuTreeView1.Visible == true 
                    && this.neuTreeView1.SelectedNode != null)
                {
                    callReg = this.neuTreeView1.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
                }
                if (this.neuTreeView2.Visible == true 
                    && this.neuTreeView2.SelectedNode != null)
                {
                    callReg = this.neuTreeView2.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
                    if (callReg != null)
                    {
                        if (MessageBox.Show("当前选择为已诊患者，是否继续叫号？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                //如果叫号对象为空，则选择待诊列表第一个进行叫号
                if (callReg == null && this.neuTreeView1.Visible == true)
                {
                    if (this.neuTreeView1.Nodes.Count > 0)
                    {
                        if (this.neuTreeView1.Nodes[0].Nodes.Count > 0)
                        {
                            callReg = this.neuTreeView1.Nodes[0].Nodes[0].Tag as FS.HISFC.Models.Registration.Register;
                        }
                    }
                }

                if (this.currentRoom == null)
                {
                    this.currentRoom = new FS.HISFC.Models.Nurse.Seat();
                }

                string roomID = (this.currentRoom as FS.HISFC.Models.Nurse.Seat).PRoom.ID;
                string sql = @"select m.dept_code from met_nuo_room m where m.room_id='{0}'";
                sql = string.Format(sql, roomID);
                string nurseID = CacheManager.SchemaMgr.ExecSqlReturnOne(sql);
                FS.FrameWork.Models.NeuObject nurseObj = new FS.FrameWork.Models.NeuObject();
                nurseObj.ID = nurseID;
                
                DateTime dtNow = CacheManager.ConManager.GetDateTimeFromSysDateTime();
                string noonID = GetNoon(dtNow);//午别

                int assignCallResult = -1;
                string strErr = "";
                if (callReg != null)
                {
                    callReg = CacheManager.RegMgr.GetByClinic(callReg.ID);

                    //选中患者叫号
                    assignCallResult = INurseAssign.Insert(callReg, 
                        (this.myQueue.Operator as FS.HISFC.Models.Base.Employee).Dept,
                        nurseObj, 
                        (this.currentRoom as FS.HISFC.Models.Nurse.Seat).PRoom, 
                        currentRoom, 
                        (this.noonHelper.GetObjectFromID(noonID) as FS.HISFC.Models.Base.Noon), 
                        ref strErr);
                }
                else
                {
                    //自动叫号
                    assignCallResult = INurseAssign.Insert(callReg, 
                        (this.myQueue.Operator as FS.HISFC.Models.Base.Employee).Dept,
                        nurseObj, 
                        (this.currentRoom as FS.HISFC.Models.Nurse.Seat).PRoom, 
                        currentRoom, 
                        (this.noonHelper.GetObjectFromID(noonID) as FS.HISFC.Models.Base.Noon), 
                        ref strErr);
                }

                if (assignCallResult == -1)
                {
                    MessageBox.Show(strErr, "提示");
                    return;
                }
                else
                {
                    // 如果自动叫号成功，再次刷新患者列表。
                    this.RefreshNoSeePatient();
                    if (callReg != null)
                    {
                        // 选中叫号的患者节点。 {4316de20-69b3-40e2-80ac-7033f60cd1ed}
                        var findNode = this.FindPatientTreeNode(callReg.ID, false);
                        if (findNode != null)
                        {
                            this.neuTreeView1.SelectedNode = findNode;
                        }
                    }
                }
            }

            return;

            #endregion

            #region 旧的作废备份


            if (this.myPatientInfo == null || string.IsNullOrEmpty(myPatientInfo.ID))
            {
                MessageBox.Show("当前没有选中患者！");
                return;
            }

            #region 新分诊流程叫号之后更新分诊表信息
            if (this.isNewAssign)
            {
                FS.HISFC.Models.Nurse.Assign assignPatient = null;

                assignPatient = CacheManager.InterMgr.QueryAssignByClinicID(myPatientInfo.ID);

                string docName = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(assignPatient.Queue.Doctor.ID);

                if (assignPatient.TriageStatus == EnuTriageStatus.In 
                    && !string.Equals(assignPatient.Queue.Doctor.ID, employee.ID))
                {
                    MessageBox.Show("患者已被【" + docName + "】叫过号，请选择另一个患者叫号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                try
                {

                    //更新挂号表中的分诊标记
                    if (assignIntegrate.UpdateRegistrationAssign(myPatientInfo.ID, this.employee.ID, CacheManager.AssignMgr.GetDateTimeFromSysDateTime()) == -1)
                    {
                        MessageBox.Show("更新挂号表中的分诊标记失败");
                        return;
                    }

                    //更新分诊表中的分诊标记
                    //if (CacheManager.AssignMgr.UpdateAssignFlag((int)FS.HISFC.Models.Nurse.EnuTriageStatus.Out, this.employee.Dept, this.employee.ID, CacheManager.AssignMgr.GetDateTimeFromSysDateTime().ToShortDateString(), (int)FS.HISFC.Models.Nurse.EnuTriageStatus.In) == -1) 
                    //{
                    //    MessageBox.Show("更新分诊表中的分诊标记失败");
                    //    return;
                    //}

                    if (this.currentRoom == null)
                    {
                        MessageBox.Show("无法获取当前诊台。\n请通知信息科将科室结构维护正确。", "叫号失败");
                        return;
                    }

                    //更新分诊状态
                    object[] args = {myPatientInfo.ID, this.currentRoom.ID,(this.currentRoom as FS.HISFC.Models.Nurse.Seat).PRoom.Name, this.employee.ID,
                                    CacheManager.AssignMgr.GetDateTimeFromSysDateTime() , (int)FS.HISFC.Models.Nurse.EnuTriageStatus.In};
                    if (CacheManager.AssignMgr.UpdateAssignAfterCall(args) == -1)
                    {
                        return;
                    }

                    MessageBox.Show("患者【" + myPatientInfo.Name + "】叫号成功。");
                    return;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新分诊表信息失败。" + ex.Message);
                    return;
                }
            }
            #endregion

            else
            {
                if (this.currentRoom == null || string.IsNullOrEmpty(currentRoom.ID))
                {
                    MessageBox.Show("当前没有诊台信息！");
                    return;
                }

                #region 根据当前时间判断队列是否有效

                FS.HISFC.Models.Nurse.Assign assign = null;

                DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                DateTime dtBegin = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.ToShortDateString() + " 00:00:00");
                DateTime dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.ToShortDateString() + " 23:59:59");

                string noonID = "";
                if (this.dtLastOper.Date == dtNow.Date && this.extendTime > 0)
                {
                    noonID = this.GetNoon(dtNow.AddMinutes(-this.extendTime));
                }
                else
                {
                    noonID = this.GetNoon(dtNow);
                }

                string queueID = CacheManager.InterMgr.QueryQueueID(this.currentRoom.ID, noonID, dtNow.ToString());
                if (queueID == null)
                {
                    MessageBox.Show(CacheManager.InterMgr.Err);
                    return;
                }
                else if (queueID == "")
                {
                    if (this.dtLastOper.Date != dtNow.Date || this.extendTime <= 0)
                    {
                        //取下个午别
                        noonID = (FS.FrameWork.Function.NConvert.ToInt32(noonID) + 1).ToString();
                        //extendTime = this.GetExtenQueueTime(this.currentRoom.ID, noonID, dtNow, out queueID);
                        //if (extendTime == -1)//不延长队列时间或者分诊到紧邻队列
                        //{
                        //    if (queueID == "")//不继续看诊
                        //    {
                        //        return;
                        //    }
                        //}
                        //else//延长当前队列时间
                        //{
                        //    noonID = this.GetNoon(dtNow.AddMinutes(-extendTime));
                        //    queueID = CacheManager.InterMgr.QueryQueueID(this.currentRoom.ID, noonID, dtNow.ToString());
                        //}
                    }
                }

                if (this.isOnlyShowAssignedPatient || this.myPatientInfo == null)
                {
                    assign = CacheManager.InterMgr.QueryWait(queueID, dtBegin, dtEnd);

                    if (assign == null)
                    {
                        MessageBox.Show("未找到分诊患者，请联系护士分诊!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    //队列有效-开始赋值  诊台诊室不变 进诊时间取当前时间
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;//进诊
                }
                else
                {
                    //选中的是已分诊未进诊的，则进诊 {37B12182-D44B-4ecb-9BA6-A6ECD310C7C0}
                    if (this.myPatientInfo != null)
                    {
                        assign = CacheManager.InterMgr.QueryAssignByClinicID(myPatientInfo.ID);
                        if (assign == null)
                        {
                            MessageBox.Show("未找到分诊患者，请联系护士分诊!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }

                        /*
                        if (assign != null && assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out )
                        {
                            return;
                        }
                        */

                        ArrayList alQueue = myQueue.QueryByQueueID(assign.Queue.ID);
                        if (alQueue != null && alQueue.Count > 0)
                        {
                            FS.HISFC.Models.Nurse.Queue queueTmp = alQueue[0] as FS.HISFC.Models.Nurse.Queue;

                            dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

                            if (this.dtLastOper.Date == dtNow.Date && this.extendTime > 0)
                            {
                                noonID = this.GetNoon(dtNow.AddMinutes(-this.extendTime));
                            }
                            else
                            {
                                noonID = this.GetNoon(dtNow);
                            }

                            queueID = CacheManager.InterMgr.QueryQueueID(this.currentRoom.ID, noonID, dtNow.ToString());
                            if (queueID == null)
                            {
                                MessageBox.Show(CacheManager.InterMgr.Err);
                                return;
                            }
                            else if (queueID != queueTmp.ID.ToString())
                            {
                                noonID = (FS.FrameWork.Function.NConvert.ToInt32(noonID) + 1).ToString();
                                //extendTime = this.GetExtenQueueTime(this.currentRoom.ID, noonID, dtNow, out queueID);
                                //if (extendTime == -1)//不延长队列时间或者分诊到紧邻队列
                                //{
                                //    if (queueID == "")//不继续看诊
                                //    {
                                //        return;
                                //    }
                                //}
                                //else//延长当前队列时间
                                //{
                                //    noonID = this.GetNoon(dtNow.AddMinutes(-extendTime));
                                //    queueID = CacheManager.InterMgr.QueryQueueID(this.currentRoom.ID, noonID, dtNow.ToString());
                                //}
                            }
                        }
                    }
                }
                #endregion

                #region 插入叫号队列

                ArrayList alNurse = CacheManager.InterMgr.QueryNurseStationByDept(this.employee.Dept, "14");
                if (alNurse == null || alNurse.Count <= 0)
                {
                    MessageBox.Show(CacheManager.InterMgr.Err, "提示");
                    return;
                }

                FS.FrameWork.Models.NeuObject nurseObj = alNurse[0] as FS.FrameWork.Models.NeuObject;
                string strErr = string.Empty;

                if (INurseAssign != null)
                {
                    int rtn = INurseAssign.Insert(assign.Register, CacheManager.LogEmpl.Dept,
                          nurseObj, assign.Queue.SRoom, assign.Queue.Console, (this.noonHelper.GetObjectFromID(noonID) as FS.HISFC.Models.Base.Noon), ref strErr);
                    if (rtn < 0)
                    {
                        MessageBox.Show(strErr, "提示");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("叫号成功，已经将该患者添加到叫号队列•");
                        return;
                    }

                }
                #endregion
            }

            #endregion
        }

        /// <summary>
        /// 过号 延迟叫号
        /// </summary>
        public void DelayCall()
        {
            if (myPatientInfo.IsSee)
            {
                MessageBox.Show("已经看诊，不用需要延迟叫号！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int rev = CacheManager.AssignMgr.UpdateAssignFlag(myPatientInfo.ID, CacheManager.AssignMgr.Operator.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Delay);

            if (rev <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (rev < 0)
                {
                    MessageBox.Show("延迟叫号失败！\r\n" + CacheManager.AssignMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("延迟叫号失败！\r\n原因：该患者不是分诊患者！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            #region 接口延迟叫号

            if (this.INurseAssign != null)
            {
                string errInfo = string.Empty;
                int rInt = this.INurseAssign.DelayCall(myPatientInfo, null, null, null, null, null, ref errInfo);
            }

            #endregion

            this.RefreshNoSeePatient();
        }

        #endregion

        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 右键查看患者基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTreeView1_MouseUp(object sender, MouseEventArgs e)
        {
            FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            contextMenu1.Items.Clear();
            if (e.Button == MouseButtons.Right)
            {
                FS.HISFC.Models.Registration.Register mnuSelectRegister = null;
                if (this.neuTreeView1.Visible == true && this.neuTreeView1.SelectedNode != null)
                {
                    mnuSelectRegister = this.neuTreeView1.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
                }
                if (this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
                {
                    mnuSelectRegister = this.neuTreeView2.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
                }
                if (mnuSelectRegister != null)
                {
                    FS.HISFC.Models.Registration.Register regTmep = CacheManager.RegInterMgr.GetByClinic(mnuSelectRegister.ID);
                    if (regTmep != null && !string.IsNullOrEmpty(regTmep.ID))
                    {
                        #region //挂号信息中的SeeNO 和处方表中的SeeNO不一样 Add by liuww 2012-09-26
                        regTmep.DoctorInfo.SeeNO = mnuSelectRegister.DoctorInfo.SeeNO;
                        #endregion

                        mnuSelectRegister = regTmep;

                        if (this.neuTreeView1.Visible == true && this.neuTreeView1.SelectedNode != null)
                        {
                            this.neuTreeView1.SelectedNode.Tag = regTmep;
                        }
                        else if (this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
                        {
                            this.neuTreeView2.SelectedNode.Tag = regTmep;
                        }
                    }

                    ToolStripMenuItem mnuPatientInfo = new ToolStripMenuItem();//院注次数
                    mnuPatientInfo.Click += new EventHandler(mnuPatientInfo_Click);

                    mnuPatientInfo.Text = "查看患者信息";
                    contextMenu1.Items.Add(mnuPatientInfo);

                    //bool hasLocalPlatformInfo = false;
                    //List<FS.HISFC.Models.Account.AccountCard> accountList = accountManager.GetMarkList(mnuSelectRegister.PID.CardNO);
                    //foreach (FS.HISFC.Models.Account.AccountCard accountCard in accountList)
                    //{
                    //    if (accountCard.IsValid && markHelper.GetName(accountCard.MarkType.ID).Contains("健康"))
                    //    {
                    //        hasLocalPlatformInfo = true;
                    //    }
                    //}

                    ToolStripMenuItem mnuLocalPlatformInfo = new ToolStripMenuItem();
                    mnuLocalPlatformInfo.Click += new EventHandler(mnuLocalPlatformInfo_Click);
                    mnuLocalPlatformInfo.Text = "查看区域平台信息";
                    //if (!hasLocalPlatformInfo)
                    //{
                    //    mnuLocalPlatformInfo.Enabled = false;
                    //}
                    contextMenu1.Items.Add(mnuLocalPlatformInfo);
                    //判断已诊且允许复制处方且是处方才显示
                    if (isShowCopyRecipe && this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
                    {
                        //判断是否是处方，即是否有子节点
                        if (this.neuTreeView2.SelectedNode.Nodes.Count == 0)
                        {
                            ToolStripMenuItem mnuCopyRecipe = new ToolStripMenuItem();//复制当前看诊信息
                            mnuCopyRecipe.Click += new EventHandler(mnuCopyRecipe_Click);

                            mnuCopyRecipe.Text = "复制当前看诊信息";
                            contextMenu1.Items.Add(mnuCopyRecipe);
                        }
                    }
                    //判断已诊且允许删除处方且是处方才显示
                    if (isShowDeleteRecipe && this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
                    {
                        //判断是否是处方，即是否有子节点
                        if (this.neuTreeView2.SelectedNode.Nodes.Count == 0)
                        {
                            ToolStripMenuItem mnuDeteleRecipe = new ToolStripMenuItem();//删除当前看诊信息
                            mnuDeteleRecipe.Click += new EventHandler(mnuDeteleRecipe_Click);

                            mnuDeteleRecipe.Text = "删除当前看诊信息";
                            contextMenu1.Items.Add(mnuDeteleRecipe);
                        }
                    }

                }
                if (this.neuTreeView1.Visible == true)
                {
                    contextMenu1.Show(this.neuTreeView1, e.X, e.Y);
                }
                if (this.neuTreeView2.Visible == true)
                {
                    contextMenu1.Show(this.neuTreeView2, e.X, e.Y);
                }
            }

        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPatientInfo_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Registration.Register mnuSelectRegister = null;
            if (this.neuTreeView1.Visible == true && this.neuTreeView1.SelectedNode != null)
            {
                mnuSelectRegister = this.neuTreeView1.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
            }
            if (this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
            {
                mnuSelectRegister = this.neuTreeView2.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
            }

            if (mnuSelectRegister == null)
            {
                return;
            }
            else
            {
                FS.HISFC.Components.Common.Controls.ucPatientPropertyForClinic ucPatientpro = new FS.HISFC.Components.Common.Controls.ucPatientPropertyForClinic();
                ucPatientpro.PatientInfo = mnuSelectRegister;
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "患者基本信息";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPatientpro);
            }
        }

        /// <summary>
        /// 复制当前看诊信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyRecipe_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Registration.Register selectRegister = null;
            if (this.neuTreeView1.Visible == true && this.neuTreeView1.SelectedNode != null)
            {
                selectRegister = this.neuTreeView1.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
            }
            if (this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
            {
                selectRegister = this.neuTreeView2.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
            }

            if (selectRegister == null)
            {
                return;
            }
            else
            {
                Forms.FrmCopyRecipeNum fromCopyRecipeNum = new Forms.FrmCopyRecipeNum();
                fromCopyRecipeNum.ShowDialog();
                string copyNum = fromCopyRecipeNum.CopyNum;
                if (copyNum == null || copyNum == "0" || copyNum == "")
                {
                    return;
                }
                int icopyNum = FS.FrameWork.Function.NConvert.ToInt32(copyNum);
                if (CopyRecipeByPatientTree != null)
                {
                    CopyRecipeByPatientTree(selectRegister, icopyNum);
                }
                this.RefreshSeePatient();
            }
            return;
        }

        /// <summary>
        /// 删除当前看诊信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDeteleRecipe_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Registration.Register selectRegister = null;

            if (this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
            {
                selectRegister = this.neuTreeView2.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
            }

            if (selectRegister == null)
            {
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("是否确认删除这张处方，删除后将不能恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    if (DeleteRecipeByPatientTree != null)
                    {
                        DeleteRecipeByPatientTree(selectRegister);
                    }
                    this.RefreshSeePatient();
                }
            }
            return;
        }


        protected void DoTreeDoubleClick(bool isDoubleClick)
        {
            if (this.TreeDoubleClick != null)
            {
                this.TreeDoubleClick(this, new ClickEventArgs(myPatientInfo), isDoubleClick);
            }
        }

        public void QueryPatientInfo(string MCardNo)// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
        {
            this.ucQuerySeeNoByCardNo1.MCardNo = MCardNo;
        }

        /// <summary>
        /// 刷卡查询
        /// </summary>
        private void ucQuerySeeNoByCardNo1_myEvents()
        {
            if (this.ucQuerySeeNoByCardNo1.Register == null)
            {
                MessageBox.Show("不能查询到患者在有效时间内的有效信息！");
                return;
            }

            //if (IOutPateintTree != null)
            //{
            //    if (IOutPateintTree.BeforeAddToTree(ucQuerySeeNoByCardNo1.Register) <= 0)
            //    {
            //        if (!string.IsNullOrEmpty(IOutPateintTree.Err))
            //        {
            //            MessageBox.Show(IOutPateintTree.Err);
            //        }
            //        return;
            //    }
            //}

            FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();
            regObj = this.ucQuerySeeNoByCardNo1.Register;
            FS.HISFC.Models.Registration.Register regtmp = null;

            //已看诊，看诊医生一致
            //对于允许一次挂号多次看诊的，加载到未看诊列表 houwb
            if (regObj.IsSee && regObj.SeeDoct.ID == this.myQueue.Operator.ID)
            {
                if (this.linkLabel1.Text == "已诊")
                {
                    PatientStateConvert();
                }

                //是否在已诊列表找到此患者
                bool isFind = false;

                foreach (TreeNode node in this.neuTreeView2.Nodes[0].Nodes)
                {
                    if (node.Tag != null)
                    {
                        regtmp = node.Tag as FS.HISFC.Models.Registration.Register;

                        //判断是否是补挂号患者，补挂号患者只判断病历号相同
                        if (regtmp.IsFee)
                        {
                            if (regObj.ID == regtmp.ID)
                            {
                                this.neuTreeView2.SelectedNode = node;
                                isFind = true;
                                return;
                            }
                        }
                        //补挂号患者
                        else
                        {
                            if (regObj.PID.CardNO == regtmp.PID.CardNO)
                            {
                                this.neuTreeView2.SelectedNode = node;
                                isFind = true;
                                //MessageBox.Show("该患者已经在列表中！");
                                return;
                            }
                        }
                    }
                }

                //保证患者挂号有效期内，即使不在已诊列表也能加载患者看诊
                if (!isFind)
                {
                    if (this.linkLabel1.Text == "待诊")
                    {
                        PatientStateConvert();
                    }
                }
            }
            else
            {
                if (this.linkLabel1.Text == "待诊")
                {
                    PatientStateConvert();
                }

                foreach (TreeNode node in this.neuTreeView1.Nodes[0].Nodes)
                {
                    if (node.Tag != null)
                    {
                        regtmp = node.Tag as FS.HISFC.Models.Registration.Register;

                        //判断是否是补挂号患者，补挂号患者只判断病历号相同
                        if (regtmp.IsFee)
                        {
                            if (regObj.ID == regtmp.ID)
                            {
                                ///MessageBox.Show("该患者已经在列表中！");
                                this.neuTreeView1.SelectedNode = node;
                                return;
                            }
                        }
                        //补挂号患者
                        else
                        {
                            if (regObj.PID.CardNO == regtmp.PID.CardNO)
                            {
                                this.neuTreeView1.SelectedNode = node;
                                this.neuTreeView1.SelectedNode.Tag = regObj;
                                if (neuTreeView1.SelectedNode.Level != 2)
                                {
                                    regObj.DoctorInfo.SeeNO = -1;
                                    neuTreeView1.SelectedNode.Tag = regObj;
                                }
                                DoTreeDoubleClick(false);
                                return;
                            }
                        }
                    }
                }
                //留观判断
                if (this.neuTreeView1.Nodes.Count > 1)
                {
                    foreach (TreeNode node in this.neuTreeView1.Nodes[1].Nodes)
                    {
                        if (node.Tag != null)
                        {
                            regtmp = node.Tag as FS.HISFC.Models.Registration.Register;

                            if (regObj.PID.CardNO == regtmp.PID.CardNO)
                            {
                                this.neuTreeView1.SelectedNode = node;
                                return;
                            }
                        }
                    }
                }
            }

            if (this.isAccountMode)
            {
                #region 判断账户余额
                decimal vacancy = 0;
                int rev = CacheManager.AccountMgr.GetVacancy(regObj.PID.CardNO, ref vacancy);
                if (rev == -1)
                {
                    MessageBox.Show("获取账户余额出错：" + CacheManager.AccountMgr.Err);
                    return;
                }
                //没有账户或账户停用
                else if (rev == 0)
                {
                    if (MessageBox.Show("该患者不存在账户，是否继续看诊？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    if (vacancy < 4 && regObj.Memo != "不补收")
                    {
                        if (MessageBox.Show("当前账户余额为" + vacancy.ToString() + "元，不足4元，是否继续看诊？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                #endregion
            }

            AddPatientToTree(regObj);
        }

        /// <summary>
        /// 显示区域平台信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuLocalPlatformInfo_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Registration.Register mnuSelectRegister = null;
            if (this.neuTreeView1.Visible == true && this.neuTreeView1.SelectedNode != null)
            {
                mnuSelectRegister = this.neuTreeView1.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
            }
            if (this.neuTreeView2.Visible == true && this.neuTreeView2.SelectedNode != null)
            {
                mnuSelectRegister = this.neuTreeView2.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
            }

            if (mnuSelectRegister == null)
            {
                return;
            }
            else
            {
                try
                {
                    //string markNO = string.Empty;
                    //List<FS.HISFC.Models.Account.AccountCard> accountList = accountManager.GetMarkList(mnuSelectRegister.PID.CardNO);
                    //foreach (FS.HISFC.Models.Account.AccountCard accountCard in accountList)
                    //{
                    //    if (accountCard.IsValid && markHelper.GetName(accountCard.MarkType.ID).Contains("健康"))
                    //    {
                    //        markNO = accountCard.MarkNO;
                    //    }
                    //}

                    //healthCard.DepartmentCode = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                    //healthCard.CardNumber = markNO;

                    //healthCardManager.PopHealthRecord(mnuSelectRegister);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucOutPatientTree_Disposed(object sender, EventArgs e)
        {
            #region 接口结束

            if (this.INurseAssign != null)
            {
                string errInfo = string.Empty;
                int rInt = this.INurseAssign.Close(null, null, null, null, null, null, ref errInfo);
            }

            #endregion
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[1];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.IDiagInDisplay);

                return t;
            }
        }

        #endregion
    }

    //{8FE4C905-279D-48c7-9D1B-D0742556A102}
    public class regCompare : IComparer
    {
        int System.Collections.IComparer.Compare(object x, object y)
        {
            FS.HISFC.Models.Registration.Register regX = (FS.HISFC.Models.Registration.Register)x;
            FS.HISFC.Models.Registration.Register regY = (FS.HISFC.Models.Registration.Register)y;

            //先根据优先标记
            //再排除过号和-1
            //剩下按照排队号排列

            string regXFirstSeeFlag = string.IsNullOrEmpty(regX.FirstSeeFlag) ? "0" : regX.FirstSeeFlag;
            string regYFirstSeeFlag = string.IsNullOrEmpty(regY.FirstSeeFlag) ? "0" : regY.FirstSeeFlag;

            //排序按照 AssignStatus为1,2的记录 ， 无AssignStatus的记录，AssignStatus为4 的记录
            //因此此处把AssignStatus为空的记录设置为4，方便排序，此处不会修改状态，只是排序
            string regXAssignStatus = string.IsNullOrEmpty(regX.AssignStatus) ? "3" : regX.AssignStatus;
            string regYAssignStatus = string.IsNullOrEmpty(regY.AssignStatus) ? "3" : regY.AssignStatus;

            //排序号小于0时排在最后，因此赋值一个很大的数
            int regXSequence = regX.SequenceNO < 0 ? 10000 : regX.SequenceNO;
            int regYSequence = regY.SequenceNO < 0 ? 10000 : regY.SequenceNO;

            if (regXFirstSeeFlag == regYFirstSeeFlag)
            {
                if (regXAssignStatus == regYAssignStatus)
                {
                    if (regXSequence == regYSequence)
                    {
                        if (regX.InputOper.OperTime < regY.InputOper.OperTime)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        if (regXSequence < regYSequence)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
                else
                {
                    return regXAssignStatus.CompareTo(regYAssignStatus);
                }
            }
            else
            {
                if (regXFirstSeeFlag == "1")
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

        }
    }

    /// <summary>
    /// 事件传出参数
    /// </summary>
    public class ClickEventArgs : EventArgs
    {
        private FS.HISFC.Models.Registration.Register reg;
        public FS.HISFC.Models.Registration.Register Message
        {
            get
            {
                return reg;
            }
            set
            {
                reg = value;
            }
        }
        public ClickEventArgs(FS.HISFC.Models.Registration.Register obj)
        {
            Message = obj;
        }

    }

}