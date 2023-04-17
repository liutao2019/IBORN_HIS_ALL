using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// [功能描述: 门诊分诊组件]<br></br>
    /// [创 建 者: 徐伟哲]<br></br>
    /// [创建时间: 不详]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucArray : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ucArray()
        {
            InitializeComponent();
        }

        #region 定义域
        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// 分诊管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Assign assginMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        /// <summary>
        /// 诊台管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Seat seatMgr = new FS.HISFC.BizLogic.Nurse.Seat();
        /// <summary>
        /// 队列管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();
        //FS.HISFC.BizLogic.Registration.RegLevel regLevelManager = new FS.HISFC.BizLogic.Registration.RegLevel();
        FS.HISFC.Models.Registration.RegLevel regLevel = new FS.HISFC.Models.Registration.RegLevel();
        
        /// <summary>
        /// treeview2的诊台信息
        /// </summary>
        private ArrayList alSeats = new ArrayList();
        private ArrayList alCollections = null;
        private TreeNode SelectedRegPatient = null;

        /// <summary>
        /// 是否允许自动刷新分诊患者
        /// </summary>
        bool isFresh = true;

        /// <summary>
        /// 是否允许自动刷新分诊患者
        /// </summary>
        [Description("是否允许自动刷新分诊患者,对于只显示外屏的不用刷新"), Category("分诊设置")]
        public bool IsFresh
        {
            get
            {
                return isFresh;
            }
            set
            {
                isFresh = value;
            }
        }



        /// <summary>
        /// 自动分诊深度标志
        /// </summary>
        string TrigeWhereFlag = "0";
        /// <summary>
        /// 显示屏是否在显示
        /// </summary>
        bool IsShowScreen = false;
        /// <summary>
        /// 是否有科室病区对应关系{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// </summary>
        bool isNurseDept = true;       
        /// <summary>
        /// 显示屏
        /// </summary>
        //Nurse.frmDisplay f = null;

        /// <summary>
        /// 是否按照科室
        /// </summary>
        bool IsByDept = true;

        #region 账户增加
        /// <summary>
        /// 账户挂号接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister iNurseRegiser = null;

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 账户是否终端扣费
        /// </summary>
        bool isAccountTermianFee = false;

        /// <summary>
        /// 参数控制业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion

        /// <summary>
        /// 待诊患者刷新频率，默认不刷新
        /// </summary>
        private string frequence = "None";

        /// <summary>
        /// 显示屏
        /// </summary>
        Nurse.frmDisplay f = null;
        /// <summary>
        /// 是否点“叉”关闭
        /// </summary>
        bool IsCancel = true;

        private bool isAutoTriage;

        /// <summary>
        /// 是否允许自动分诊
        /// </summary>
        public bool IsAutoTriage
        {
            get
            {
                return this.isAutoTriage;
            }
            set
            {
                this.isAutoTriage = value;
                this.neuGroupBox1.Visible = value;
            }
        }
        /// <summary>
        /// 是否有科室病区对应关系{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// </summary>
        public bool IsNurseDept
        {
            get { return isNurseDept; }
            set { isNurseDept = value; }
        }
        #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
        /// <summary>
        /// 医生列表
        /// </summary>
        FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        private QueDisplayType queType = QueDisplayType.Large;

        /// <summary>
        /// 队列列表显示模式
        /// </summary>
        [Category("控件设置"), Description("队列列表显示模式")]
        public QueDisplayType QueDisplayType
        {
            get
            {
                return this.queType;
            }
            set
            {
                this.queType = value;
            }
        }

        private FormDisplayMode frmDspType = FormDisplayMode.Horizontal;

        /// <summary>
        /// 队列列表显示模式
        /// </summary>
        [Category("控件设置"), Description("界面显示模式")]
        public FormDisplayMode FormDisplayMode
        {
            get
            {
                return this.frmDspType;
            }
            set
            {
                this.frmDspType = value;
            }
        }

        #endregion

        #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}

        /// <summary>
        /// 专家是否自动分诊
        /// </summary>
        bool isTrigeExpert = false;

        #endregion

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("分诊", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null);
            this.toolBarService.AddToolButton("取消分诊", "",(int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("刷新", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            this.toolBarService.AddToolButton("进诊", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X下一个, true, false, null);
            this.toolBarService.AddToolButton("取消进诊", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S上一个, true, false, null);
            this.toolBarService.AddToolButton("暂停", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null);
            this.toolBarService.AddToolButton("屏显（开）", "", 0, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "分诊":
                    this.Triage();
                    break;
                case "取消分诊":
                    this.CancelTriage();
                    break;
                case "刷新":
                    //this.RefreshForm();
                    this.RefreshRegPatient();
                    #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
                    this.RefreshQueue();
                    #endregion
                    this.RefreshRoom();
                    break;
                case "进诊":
                    this.In();
                    break;
                case "取消进诊":
                    this.CancelIn();
                    break;
                case "暂停":
                    break;
                case "屏显（开）":
                    this.Screen();
                    break;
                #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                case "屏显（关）":
                    this.Screen();
                    break;
                #endregion
                default:
                    break;
            }
        }

        private void RefreshForm()
        {
            this.AutoExpert();
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();
        }

        private void Triage()
        {
            if (this.lvQueue.Items.Count <= 0)
            {
                MessageBox.Show("没有维护队列!", "提示");
                return;
            }

            TreeNode node = this.neuTreeView1.SelectedNode;

            if (node == null || node.Parent == null) return;

            FS.HISFC.Models.Registration.Register reginfo =
                (FS.HISFC.Models.Registration.Register)node.Tag;
            FS.HISFC.Models.Registration.RegLevel myRegLevel = new FS.HISFC.Models.Registration.RegLevel();
            myRegLevel = this.regMgr.QueryRegLevelByCode(reginfo.DoctorInfo.Templet.RegLevel.ID);
            reginfo.DoctorInfo.Templet.RegLevel.IsExpert = myRegLevel.IsExpert;
            //防止并发
            if (this.JudgeTrige(reginfo.ID))
            {
                this.neuTreeView1.SelectedNode.Remove();
                MessageBox.Show("该患者已经分诊!", "提示");
                return;
            }
            //防止已经退号
            if (this.Judgeback(reginfo.ID))
            {
                this.neuTreeView1.SelectedNode.Remove();
                MessageBox.Show("该患者已经退号!");
                return;
            }

            //ucTriage f = new ucTriage(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            
            ucTriage f = new ucTriage(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            f.Register = (FS.HISFC.Models.Registration.Register)node.Tag;

            f.OK += new ucTriage.MyDelegate(f_OK);
            f.Cancel+=new EventHandler(f_Cancel);
           
            FS.FrameWork.WinForms.Classes.Function.ShowControl(f);
        }

        /// <summary>
        /// 分诊确定后，刷新界面
        /// </summary>
        /// <param name="assign"></param>
        private void f_OK(FS.HISFC.Models.Nurse.Assign assign)
        {
            TreeNode node = this.neuTreeView1.SelectedNode;

            //{AAA35EEB-7FCD-4fa4-8CAA-9321E2AE3050}
            //增加判断node不为空，否则报错。
            if (node != null)
            {
                this.neuTreeView1.Nodes[0].Nodes.Remove(node);
            }
            //{AAA35EEB-7FCD-4fa4-8CAA-9321E2AE3050}
            this.RefreshAssign(assign);
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();

        }

        private void f_Cancel(Object sender, EventArgs e)
        {
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();
        }


        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="select"></param>
        private void RefreshAssign(FS.HISFC.Models.Nurse.Assign select)
        {
            if (this.lvQueue.SelectedItems == null ||
                this.lvQueue.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvQueue.SelectedItems[0];

            if (selected.Tag.GetType() ==
                typeof(FS.HISFC.Models.Nurse.Queue))
            {
                if ((selected.Tag as FS.HISFC.Models.Nurse.Queue).ID == select.Queue.ID)
                    this.AddAssign(select);
            }
            else
            {
                if ((selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID == select.Queue.ID)
                    this.AddAssign(select);
            }
        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        private void CancelTriage()
        {
            if (this.lvPatient.SelectedItems == null ||
                this.lvPatient.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvPatient.SelectedItems[0];

            string error = "";

            if (MessageBox.Show("是否要取消该分诊信息?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            if (Function.CancelTriage( (selected.Tag as FS.HISFC.Models.Nurse.Assign), ref error)
                == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(error, "提示");
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.lvPatient.Items.Remove(selected);
            this.RefreshRegPatient();
        }
        private void CancelTriageUnsee()
        { 
            DateTime endDate = this.assginMgr.GetDateTimeFromSysDateTime();
            DateTime beginDate =endDate.Date;
            string noonID = Nurse.Function.GetNoon(endDate);
            ArrayList al = new ArrayList();
           al =  this.assginMgr.QueryUnInSee( beginDate.ToString(),endDate.ToString(),((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,noonID);
           if (al != null && al.Count > 0)
           {
               string error = "";
               foreach (FS.HISFC.Models.Nurse.Assign assign in al)
               {
                   FS.FrameWork.Management.PublicTrans.BeginTransaction();

                   //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                   //t.BeginTransaction();

                   if (Function.CancelTriage(assign, ref error) == -1)
                   {
                       FS.FrameWork.Management.PublicTrans.RollBack();
                       MessageBox.Show(error, "提示");
                       return;
                   }

                   FS.FrameWork.Management.PublicTrans.Commit();

                   this.RefreshRegPatient();
 
               }
           }
        }

        /// <summary>
        /// 菜单设置
        /// </summary>
        private void LoadMenuSet()
        {
            try
            {
                //XmlDocument doc = new XmlDocument();
                //doc.Load(Application.StartupPath + "/Setting/feeSetting.xml");
                //XmlNode node = doc.SelectSingleNode("//分诊刷新频率");

                //if (node != null)
                //    frequence = node.Attributes["currentSet"].Value;

                //start  是否严格按照科室自动分诊的本地设置,默认是 -----
                XmlDocument doc2 = new XmlDocument();
                doc2.Load(Application.StartupPath + "/Setting/NurseSetting.xml");
                XmlNode node2 = doc2.SelectSingleNode("//是否按科室分诊");

                if (node2 != null && node2.Attributes["isByDept"].Value.ToString() == "false")
                {
                    this.IsByDept = false;
                }
                else
                {
                    this.IsByDept = true;
                }

                #region {4B7F932C-1C2A-4b65-B6F4-C2C1EFA19B12}
                //if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                //{
                //    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                //    System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/Setting/arrangeSetting.xml", System.IO.FileMode.Open);

                //    RefreshFrequence refres = (RefreshFrequence)xs.Deserialize(fs);
                //this.neuGroupBox1.Visible = refres.IsAutoTriage;
                node2 = doc2.SelectSingleNode("//是否自动分诊");
                if (node2 != null && node2.Attributes["isAutoTriage"].Value.ToString() == "false")
                {
                    this.neuGroupBox1.Visible = false;
                }
                else
                {
                    this.neuGroupBox1.Visible = true;
                }

                node2 = doc2.SelectSingleNode("//自动分诊刷新");
                string refreshTime = "";
                if (node2 != null)
                {
                    refreshTime = node2.Attributes["RefreshTime"].Value.ToString();
                }

                if (this.neuGroupBox1.Visible)
                {
                    switch (refreshTime.Trim())
                    {
                        case "auto":
                            this.SetRadioFalse();
                            this.rbAuto.Checked = true;
                            break;
                        case "no":
                            this.SetRadioFalse();
                            this.rbNo.Checked = true;
                            break;
                        case "10":
                            this.SetRadioFalse();
                            this.rb10.Checked = true;
                            break;
                        case "30":
                            this.SetRadioFalse();
                            this.rb30.Checked = true;
                            break;
                        case "60":
                            this.SetRadioFalse();
                            this.rb60.Checked = true;
                            break;
                        case "300":
                            this.SetRadioFalse();
                            this.rbOther.Checked = true;
                            break;
                        default:
                            this.SetRadioFalse();
                            this.rbNo.Checked = true;
                            break;
                    }
                } 
                #endregion

                if (!isFresh)
                {
                    this.SetRadioFalse();
                    this.rbNo.Checked = true;
                }

                //this.SetFreq(frequence);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void Screen()
        {
            string screenSize = this.controlMgr.QueryControlerInfo("900004");
            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            ToolStripButton tsb = this.toolBarService.GetToolButton("屏显（开）");
            if (tsb == null)
            {
                ToolStripButton tsbtmp = this.toolBarService.GetToolButton("屏显（关）");

                if (tsbtmp == null)
                {
                    return;
                }
                else
                {
                    tsbtmp.Text = "屏显（开）";
                }
            }
            else
            {
                tsb.Text = "屏显（关）";
            }
            
            //开启这个窗口
            if (!IsShowScreen)
            {
                f = new frmDisplay();
                f.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 0);
                f.Show();
                IsShowScreen = true;
            }
            else
            {
                if (f != null)
                {
                    f.Close();
                }
                IsShowScreen = false;
            }
            #endregion
        }

        /// <summary>
        /// 进诊
        /// </summary>
        private void In()
        {
            if (this.lvPatient.SelectedItems == null || this.lvPatient.SelectedItems.Count == 0)
                return;

            ListViewItem selected = this.lvPatient.SelectedItems[0];

            FS.HISFC.Models.Nurse.Assign assigninfo = (FS.HISFC.Models.Nurse.Assign)selected.Tag;
            //防止已经退号
            if (this.Judgeback(assigninfo.Register.ID))
            {
                MessageBox.Show("该患者已经退号!请取消其分诊信息");
                return;
            }

            //ucInRoom f = new ucInRoom(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            ucInRoom f = new ucInRoom(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            f.Assign = assigninfo;
            f.OK += new ucInRoom.MyDelegate(In_OK);
            FS.FrameWork.WinForms.Classes.Function.ShowControl(f);
            //			f.OK +=new Nurse.frmInRoom.MyDelegate(In_OK);
            //if (f.DialogResult == DialogResult.OK)
            //{
            //    this.In_OK(assigninfo);
            //}

            //f.Dispose();
        }
        /// <summary>
        /// 处理进诊后的场面
        /// </summary>
        /// <param name="assign"></param>
        private void In_OK(FS.HISFC.Models.Nurse.Assign assign)
        {
            //在分诊列表中删除该分诊信息
            foreach (ListViewItem item in this.lvPatient.Items)
            {
                if ((item.Tag as FS.HISFC.Models.Nurse.Assign).Register.ID == assign.Register.ID)
                {
                    this.lvPatient.Items.Remove(item);
                    break;
                }
            }
            //在进诊列表中添加患者
            this.AddPatient(assign);
        }

        /// <summary>
        /// 取消进诊
        /// </summary>
        private void CancelIn()
        {
            TreeNode node = this.neuTreeView2.SelectedNode;

            if (node == null || node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Assign))
                return;

            if (MessageBox.Show("是否要取消该进诊信息?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            //恢复候诊状态

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            try
            {
                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Nurse.Assign info = node.Tag as FS.HISFC.Models.Nurse.Assign;
                int rtn = this.assginMgr.CancelIn(info.Register.ID, info.Queue.Console.ID);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.assginMgr.Err, "提示");
                    return;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("该分诊信息状态已经改变,请刷新屏幕!", "提示");
                    return;
                }


                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return;
            }
            //删除进诊患者
            this.neuTreeView2.Nodes.Remove(node);
            //恢复为待诊患者
            if (this.lvQueue.SelectedItems == null ||
                this.lvQueue.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvQueue.SelectedItems[0];

            if (selected.Tag.GetType() ==
                typeof(FS.HISFC.Models.Nurse.Queue))
            {
                if ((selected.Tag as FS.HISFC.Models.Nurse.Queue).ID ==
                    (node.Tag as FS.HISFC.Models.Nurse.Assign).Queue.ID)
                    this.AddAssign((FS.HISFC.Models.Nurse.Assign)node.Tag);
            }
            else
            {
                if ((selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID ==
                    (node.Tag as FS.HISFC.Models.Nurse.Assign).Queue.ID)
                    this.AddAssign((FS.HISFC.Models.Nurse.Assign)node.Tag);
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化列表
        /// </summary>
        private void Init()
        {
            this.RefreshQueue();

            this.RefreshRoom();

            #region {4B7F932C-1C2A-4b65-B6F4-C2C1EFA19B12}
            //this.LoadMenuSet();
            #endregion
            this.neuTreeView1.ImageList = this.neuTreeView1.deptImageList;
            this.neuTreeView2.ImageList = this.neuTreeView2.groupImageList;
            this.isNurseDept = controlIntegrate.GetControlParam<bool>("900010", true, false);//是否存在科室病区对应关系{F044FCF3-6736-4aaa-AA04-4088BB194C20}
            if (this.frequence != "Auto") this.RefreshRegPatient();
            if (this.frequence == "Auto")
            {
                this.Auto();
            }
            isAccountTermianFee = controlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, true, false);
            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            //获取分诊深度
            TrigeWhereFlag = controlIntegrate.GetControlParam<string>("900001", true, "0");
            
            isTrigeExpert = controlIntegrate.GetControlParam<bool>("900003", true, false);

            #endregion
        }
        /// <summary>
        /// 生成挂号患者列表
        /// </summary>
        /// <returns></returns>
        private int RefreshRegPatient()
        {
            this.CancelTriageUnsee();
            DateTime current = this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime().Date;
            string myNurseDept = "";

            //myNurseDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;  by niuxinyuan

            myNurseDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            //过滤科室
            this.neuTreeView1.Nodes.Clear();
            TreeNode root = new TreeNode("待分诊患者");
            this.neuTreeView1.Nodes.Add(root);
            if (this.isNurseDept)//{F044FCF3-6736-4aaa-AA04-4088BB194C20}是否存在科室病区对应关系
            {
                this.alCollections = this.regMgr.QueryNoTriagebyDept(current, myNurseDept);
            }
            else
            {
                this.alCollections = this.regMgr.QueryNoTriagebyNurse(current, myNurseDept);
            }
            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Registration.Register obj in this.alCollections)
                {
                    //如果已经分诊就跳过
                    bool blTriage = this.JudgeTrige(obj.ID);
                    if (blTriage == true)
                    {
                        continue;
                    }
                    TreeNode node = new TreeNode();
                    //对齐科室----------------------------------------------------------------------
                    int length = obj.Name.Trim().Length;
                    string strname = "";
                    if (length <= 7)
                    {
                        strname = obj.Name.PadRight(7 - length, ' ');
                    }
                    else
                    {
                        strname = obj.Name;
                    }
                    //ensd---------------------------------------------------------------------------
                    if (obj.DoctorInfo.Templet.Doct.ID != null && obj.DoctorInfo.Templet.Doct.ID != "")
                    {
                        node.Text = strname + " [" + obj.DoctorInfo.Templet.RegLevel.Name + "]" + " [" + obj.DoctorInfo.Templet.Doct.Name + "]" + obj.DoctorInfo.Templet.Doct.Name + "-" + obj.DoctorInfo.SeeDate.ToString();
                    }
                    else
                    {
                        node.Text = strname + " [" + obj.DoctorInfo.Templet.RegLevel.Name + "]" + obj.DoctorInfo.Templet.Dept.Name + "-" + obj.DoctorInfo.SeeDate.ToString();
                    }
                    //node.ImageIndex = 2;
                    //node.SelectedImageIndex = 2;
                    node.Tag = obj;

                    root.Nodes.Add(node);
                }
            }

            root.Expand();

            return 0;
        }
        /// <summary>
        /// 生成诊台列表和诊台下待诊患者
        /// </summary>
        /// <returns></returns>
        private int RefreshRoom()
        {
            this.neuTreeView2.Nodes.Clear();
            this.alSeats = new ArrayList();

            TreeNode root = new TreeNode("诊室列表");
            this.neuTreeView2.Nodes.Add(root);

            FS.HISFC.BizLogic.Nurse.Room roomMgr =
                new FS.HISFC.BizLogic.Nurse.Room();

            //this.alCollections = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            this.alCollections = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Room obj in this.alCollections)
                {
                    if (obj.IsValid == "0") continue;

                    TreeNode node = new TreeNode();

                    node.Text = obj.Name;
                    #region {E1FBDE66-8059-496f-B631-F5815788EB47}
                    //node.ImageIndex = 41;
                    //node.SelectedImageIndex = 40;
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 3; 
                    #endregion
                    node.Tag = obj;

                    root.Nodes.Add(node);

                    ArrayList alConsole = this.seatMgr.Query(obj.ID);
                    if (alConsole != null)
                    {
                        foreach (FS.HISFC.Models.Nurse.Seat seat in alConsole)
                        {
                            if (seat.PRoom.IsValid == "0") continue;

                            TreeNode node2 = new TreeNode();

                            node2.Text = seat.Name + "[" + this.GetDoct(seat.ID) + "]";//------------------------------
                            #region  {E1FBDE66-8059-496f-B631-F5815788EB47}
                            //node2.ImageIndex = 18;
                            //node2.SelectedImageIndex = 18;
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 3; 
                            #endregion
                            node2.Tag = seat;
                            node.Nodes.Add(node2);
                            //添加到诊台数组，用于自动分诊时候用。
                            this.alSeats.Add(seat);
                        }
                    }//end foreach
                }//end if
                this.AddPatientByConsole();

                #region 设置该诊室的人数(留做备用)
                //				foreach(TreeNode node1 in this.treeView2.Nodes[0].Nodes)
                //				{
                //					foreach(TreeNode node2 in node1.Nodes)
                //					{
                //						int count = node2.Nodes.Count;
                //						node2.Text = node2.Text + "(" + count.ToString() + ")";
                //					}
                //				}
                #endregion
            }

            root.ExpandAll();
            return 0;
        }
        /// <summary>
        /// 根据诊台在界面队列中寻找医生名称
        /// </summary>
        /// <param name="seatID"></param>
        /// <returns></returns>
        private string GetDoct(string seatID)
        {
            string strDoct = "";
            for (int i = 0; i < this.lvQueue.Items.Count; i++)
            {
                FS.HISFC.Models.Nurse.Queue info = this.lvQueue.Items[i].Tag
                                                            as FS.HISFC.Models.Nurse.Queue;
                //找到了所属队列
                if (info.Console.ID == seatID)
                {
                    //所属队列维护了医生
                    if (info.Doctor.ID == null || info.Doctor.ID == "")
                    {
                        continue;
                    }
                    //FS.HISFC.BizLogic.Manager.Person psMgr = new FS.HISFC.BizLogic.Manager.Person();
                    //FS.HISFC.Models.RADT.Person ps = new FS.HISFC.Models.RADT.Person();
                    FS.HISFC.BizProcess.Integrate.Manager psMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();
                    ps = psMgr.GetEmployeeInfo(info.Doctor.ID);
                    if (ps.Name != null || ps.Name != "")
                    {
                        strDoct = ps.Name;
                        break;
                    }
                }
            }
            return strDoct;
        }
        /// <summary>
        /// 生成待诊患者
        /// </summary>
        private void AddPatientByConsole()
        {
            //this.alCollections = this.assginMgr.Query(
            //    ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,
            //    this.assginMgr.GetDateTimeFromSysDateTime().Date,
            //    FS.HISFC.Models.Nurse.EnuTriageStatus.In
            //    );
            this.alCollections = this.assginMgr.QueryUnionRegister(
                //((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,
                ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,
                this.assginMgr.GetDateTimeFromSysDateTime().Date,
                FS.HISFC.Models.Nurse.EnuTriageStatus.In
                );

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Assign obj in this.alCollections)
                {
                    //根据clinic_code 获得患者是否退号
                    this.AddPatient(obj);
                }
            }
        }
        /// <summary>
        /// 添加到树中
        /// </summary>
        /// <param name="assign"></param>
        private void AddPatient(FS.HISFC.Models.Nurse.Assign assign)
        {
            foreach (TreeNode node2 in this.neuTreeView2.Nodes[0].Nodes)
            {
                foreach (TreeNode node in node2.Nodes)
                {
                    if (assign.Queue.Console.ID == (node.Tag as FS.HISFC.Models.Nurse.Seat).ID)
                    {
                        TreeNode child = new TreeNode();
                        if (assign.Register.DoctorInfo.Templet.Doct.ID != null && assign.Register.DoctorInfo.Templet.Doct.ID != "")
                        {
                            child.Text = assign.SeeNO.ToString() + "[" + assign.Register.Name + "]"+"[" + assign.Register.DoctorInfo.Templet.RegLevel.Name + "]" + "[" + assign.Register.DoctorInfo.Templet.Doct.Name + "]" ;
                        }
                        else
                        {
                            child.Text =  assign.SeeNO.ToString() + "[" + assign.Register.Name + "]"+"[" + assign.Register.DoctorInfo.Templet.RegLevel.Name + "]" ;
                        }
                        #region  {E1FBDE66-8059-496f-B631-F5815788EB47}
                        //child.ImageIndex = 36;
                        //child.SelectedImageIndex = 36;
                        child.ImageIndex = 4;
                        child.SelectedImageIndex = 3; 
                        #endregion
                        child.Tag = assign;

                        node.Nodes.Add(child);
                        break;
                    }

                }
            }
        }
        /// <summary>
        /// 查询分诊台分诊队列信息
        /// </summary>
        private void RefreshQueue()
        {
            this.lvQueue.Items.Clear();
            DateTime current = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            string noonID = Nurse.Function.GetNoon(current);//午别
            if (noonID == "")
            {
                MessageBox.Show("当前时间内没有对应的午别!", "提示");
                return;
            }
            this.RefreshQueue(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, current, noonID);

        }
        /// <summary>
        /// 刷新本诊台的分诊队列
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        private int RefreshQueue(string nurseID, DateTime queueDate, string noonID)
        {
            FS.HISFC.BizLogic.Nurse.Queue queueMgr =
                new FS.HISFC.BizLogic.Nurse.Queue();

            //this.alCollections = queueMgr.Query(nurseID, queueDate, noonID);
            this.alCollections = queueMgr.Query(nurseID, queueDate.Date/*.ToShortDateString()*/,noonID);

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Queue obj in this.alCollections)
                {
                    if (!obj.IsValid) continue;

                    ListViewItem item = new ListViewItem();

                    #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
                    item.SubItems.Clear();

                    item.SubItems.Add(obj.AssignDept.Name);
                    item.SubItems.Add(this.doctHelper.GetName(obj.Doctor.ID));
                    item.SubItems.Add(obj.Doctor.ID);
                    item.SubItems.Add(obj.SRoom.Name);
                    item.SubItems.Add(obj.WaitingCount.ToString());
                    #endregion

                    item.Text = obj.Name;
                    if (obj.Memo != null && obj.Memo != "")
                        item.Text = item.Text + "\n" + "[" + obj.AssignDept.Name + "]";
                    //专家和非专家的图标设置
                    if (obj.ExpertFlag == "1")
                    {
                        item.ImageIndex = 1;
                    }
                    else
                    {
                        item.ImageIndex = 0;
                    }

                    item.Tag = obj;

                    this.lvQueue.Items.Add(item);
                }
            }
            return 0;
        }

        #endregion

        #region 拖放

        private void neuTreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //拖动项
            if (e.Item == null) return;
            if (e.Button == MouseButtons.Left)
            {
                if ((e.Item as TreeNode).Tag == null || (e.Item as TreeNode).Tag.ToString() == "") return;

                this.SelectedRegPatient = (TreeNode)e.Item;
                DragDropEffects dropEffect = this.neuTreeView1.DoDragDrop((e.Item as TreeNode).Tag, DragDropEffects.All | DragDropEffects.Move);
            }
        }

        private void neuTreeView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
            {
                this.CancelTriage();
            }
        }

        private void neuTreeView1_DragEnter(object sender, DragEventArgs e)
        {
            this.neuTreeView1.Focus();

            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lvQueue_DragDrop(object sender, DragEventArgs e)
        {
            //分诊
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                //分诊
                ListViewItem item;

                Point p = this.lvQueue.PointToClient(Cursor.Position);

                item = this.lvQueue.GetItemAt(p.X, p.Y);
                if (item == null) return;

                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

                #region 实体赋值
                assign.Register = (FS.HISFC.Models.Registration.Register)
                    e.Data.GetData(typeof(FS.HISFC.Models.Registration.Register));

                FS.HISFC.Models.Nurse.Queue info = (FS.HISFC.Models.Nurse.Queue)item.Tag;
                assign.Queue = info;
                #region 可能情况提示
                //该患者已经退号
                if (this.Judgeback(assign.Register.ID))
                {
                    MessageBox.Show("该患者已经退号!请刷新屏幕!");
                    return;
                }
                //该患者已经分诊
                if (this.JudgeTrige(assign.Register.ID))
                {
                    MessageBox.Show("该患者已经分诊!请刷新屏幕!", "提示");
                    return;
                }

                //挂号科室与队列科室不符～
                if (assign.Queue.AssignDept.ID != assign.Register.DoctorInfo.Templet.Dept.ID
                    && assign.Queue.AssignDept.ID != "" && assign.Queue.AssignDept.ID != null)
                {
                    if (MessageBox.Show("患者挂号科室[" + assign.Register.DoctorInfo.Templet.Dept.Name + "]与队列的看诊科室["
                        + assign.Queue.AssignDept.Name + "]不符!" + "\n" + "是否继续？", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                //普通号进了专家～
                if ((assign.Register.DoctorInfo.Templet.Doct.ID == "" || assign.Register.DoctorInfo.Templet.Doct.ID == null)
                    && assign.Queue.ExpertFlag == "1")
                {
                    if (MessageBox.Show("普通挂号进诊专家队列" + "是否继续？", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                //专家号进了普通～
                if (assign.Register.DoctorInfo.Templet.Doct.ID != "" && assign.Register.DoctorInfo.Templet.Doct.ID != null
                    && assign.Queue.ExpertFlag == "0")
                {
                    if (MessageBox.Show("专家挂号进诊普通队列" + "是否继续？", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                //专家号进了普通～
                if (assign.Register.DoctorInfo.Templet.Doct.ID != "" && assign.Register.DoctorInfo.Templet.Doct.ID != null
                    && assign.Queue.Doctor.ID != assign.Register.DoctorInfo.Templet.Doct.ID)
                {
                    if (MessageBox.Show("选择医生与挂号医生不一致" + "是否继续？", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                #endregion
                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                //assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;// var.User.Nurse.ID;
                assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime(); //this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime();
                assign.Oper.OperTime = assign.TirageTime;
                assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID; //var.User.ID;
                assign.Queue.Dept = info.AssignDept;

                #endregion

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

                string error = "";

                if (Function.Triage(assign, "1", ref error) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(error, "提示");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                if (this.SelectedRegPatient != null)
                    this.SelectedRegPatient.Remove();

                //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID/*var.User.Nurse.ID*/, 
                //                  this.assginMgr.GetDateTimeFromSysDateTime().Date /*this.regMgr.GetDateTimeFromSysDateTime().Date*/,
                //                  (item.Tag as FS.HISFC.Models.Nurse.Queue).ID);
                this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID/*var.User.Nurse.ID*/,
                                 this.assginMgr.GetDateTimeFromSysDateTime().Date /*this.regMgr.GetDateTimeFromSysDateTime().Date*/,
                                 (item.Tag as FS.HISFC.Models.Nurse.Queue).ID);

            }
        }

        private void lvQueue_DragEnter(object sender, DragEventArgs e)
        {
            this.lvQueue.Focus();

            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lvQueue_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                ListViewItem item;

                Point p = this.lvQueue.PointToClient(Cursor.Position);

                item = this.lvQueue.GetItemAt(p.X, p.Y);

                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        private void lvPatient_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //拖动项
            if (e.Item == null) return;
            if (e.Button == MouseButtons.Left)
            {
                DragDropEffects dropEffect = lvPatient.DoDragDrop((e.Item as ListViewItem).Tag, DragDropEffects.All | DragDropEffects.Move);
            }
        }

        private void neuTreeView2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
            {
                Point p = this.neuTreeView2.PointToClient(Cursor.Position);

                TreeNode node = this.neuTreeView2.GetNodeAt(p.X, p.Y);

                if (node == null) return;

                this.neuTreeView2.SelectedNode = node;
            }
        }

        private void neuTreeView2_DragEnter(object sender, DragEventArgs e)
        {
            this.neuTreeView2.Focus();

            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void neuTreeView2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
            {
                //进诊台
                Point p = this.neuTreeView2.PointToClient(Cursor.Position);

                TreeNode node = this.neuTreeView2.GetNodeAt(p.X, p.Y);

                if (node == null || node.Tag == null) return;
                //没有放在诊台(或者患者)上面，返回。
                if (node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Seat)
                    && node.Parent.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Seat)) return;


                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                assign = (FS.HISFC.Models.Nurse.Assign)e.Data.GetData(typeof(FS.HISFC.Models.Nurse.Assign));

                if (this.Judgeback(assign.Register.ID))
                {
                    MessageBox.Show("该患者已经退号!请取消其分诊信息!");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

                try
                {
                    this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    int rtn = 1;
                    if (node.Tag.GetType() == typeof(FS.HISFC.Models.Nurse.Seat))
                    {
                        rtn = assginMgr.Update(assign.Register.ID, (FS.FrameWork.Models.NeuObject)node.Parent.Tag,
                            (FS.FrameWork.Models.NeuObject)node.Tag, assginMgr.GetDateTimeFromSysDateTime());
                    }
                    else
                    {
                        rtn = assginMgr.Update(assign.Register.ID, (FS.FrameWork.Models.NeuObject)node.Parent.Parent.Tag,
                            (FS.FrameWork.Models.NeuObject)node.Parent.Tag, assginMgr.GetDateTimeFromSysDateTime());
                    }

                    if (rtn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(assginMgr.Err, "提示");
                        return;
                    }
                    if (rtn == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("该患者分诊状态已经改变,请重新刷新屏幕!", "提示");
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception err)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(err.Message, "提示");
                    return;
                }
                //刷新屏幕
                //在分诊列表中删除该分诊信息
                foreach (ListViewItem item in this.lvPatient.Items)
                {
                    if ((item.Tag as FS.HISFC.Models.Nurse.Assign).Register.ID == assign.Register.ID)
                    {
                        this.lvPatient.Items.Remove(item);
                        break;
                    }
                }
                //在进诊列表中添加患者
                if (node.Tag.GetType() == typeof(FS.HISFC.Models.Nurse.Seat))
                {
                    assign.Queue.Console = (FS.FrameWork.Models.NeuObject)node.Tag;
                }
                else
                {
                    assign.Queue.Console = (FS.FrameWork.Models.NeuObject)node.Parent.Tag;
                }
                this.AddPatient(assign);
            }
        }

        #endregion

        #region 公共

        /// <summary>
        /// 判断是否已经分诊
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        private bool JudgeTrige(string ClinicCode)
        {
            //从FIN_OPB_REGISTER中,根据ClinicCode取出数据,如果assign_flag是Triage,则返回true,否则返回false
            bool bl = false;
            bl = this.regMgr.QueryIsTriage(ClinicCode);
            return bl;
        }

        /// <summary>
        /// 判断是否退号
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        private bool Judgeback(string ClinicCode)
        {
            //从FIN_OPB_REGISTER中,根据ClinicCode取出数据,如果valid_flag = 1 返回false,否则返回true
            bool bl = false;
            bl = this.regMgr.QueryIsCancel(ClinicCode);
            return bl;
        }

        /// <summary>
        /// 根据护理站，时间，科室 获取最少WaitingCount的队列
        /// </summary>
        /// <param name="al"></param>
        /// <param name="regDept"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Nurse.Queue GetMinQueue(ArrayList al, string regDept)
        {
            int minCount = int.MaxValue;
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime().Date; //this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;

            FS.HISFC.Models.Nurse.Queue minInfo = new FS.HISFC.Models.Nurse.Queue();

            #region 找最小人数的队列
            //找出所有最小人数的队列for your 

            ArrayList alMin = new ArrayList();
            foreach (FS.HISFC.Models.Nurse.Queue info in al)
            {
                if (info.ExpertFlag == "1") continue;

                #region 找多科室看看
                //				if(regDept != "ALL")
                //				{
                //					//如果在医生允许的多科室数组中也找不到...continue
                //					ArrayList alDept = new ArrayList();
                //					FS.HISFC.BizLogic.Manager.DepartmentStatManager deptMgr = 
                //						new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                //					alDept = deptMgr.GetMultiDept(info.Doctor.ID);
                //					bool IsHas = false;
                //
                //					foreach(FS.HISFC.Models.Base.DepartmentStat deptinfo in alDept)
                //					{
                //						if(deptinfo.ID == regDept )
                //						{
                //							IsHas = true;
                //							break;
                //						}
                //					}
                //					if(!IsHas) continue;
                //				}
                #endregion

                if (this.IsByDept)
                {
                    if (regDept != info.AssignDept.ID) continue;
                }
                if (info.WaitingCount < minCount)
                {
                    minCount = info.WaitingCount;
                    minInfo = info;
                }

            }
            //在所有最小人数的队列中
            #endregion

            //没有找到队列
            if (minCount == int.MaxValue)
            {
                return null;
            }

            //最小队列人数 +1 
            foreach (FS.HISFC.Models.Nurse.Queue info in al)
            {
                if (info == minInfo)
                {
                    //此队列人数 +1 
                    info.WaitingCount = info.WaitingCount + 1;
                }
            }

            return minInfo;
        }

        #endregion

        #region 单独的专家自动分诊
        /// <summary>
        /// 专家自动分诊(使用之前需要刷新一下患者和队列)------作为单独的模块使用，不能跟其他自动分诊重合
        /// </summary>
        private void AutoExpert()
        {
            #region	专家是否自动分诊{DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            //string TrigeExpertFlag = this.controlMgr.QueryControlerInfo("900003");
            //if (TrigeExpertFlag == null || TrigeExpertFlag == "")
            //{
            //    TrigeExpertFlag = "0";
            //}
            if (!isTrigeExpert)
            {
                return;
            }
            #endregion

            #region 获取基本信息
            //获取时间
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime().Date; //this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);

            //需要分诊的患者信息
            //this.alCollections = this.regMgr.QueryNoTriagebyDept(current, FS.FrameWork.Management.Connection.Operator.ID);
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (this.alCollections == null) return;

            //分诊的队列
            ArrayList al = new ArrayList();
            al = queueMgr.Query(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, current, noonID);
            if (al == null || al.Count <= 0) return;
            #endregion

            #region 自动分诊到
            foreach (FS.HISFC.Models.Registration.Register reg in this.alCollections)
            {
                if (reg.DoctorInfo.Templet.Doct.ID != null || reg.DoctorInfo.Templet.Doct.ID != "")
                {
                    //实体赋值
                    bool IsFound = false;
                    FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                    for (int i = 0; i < this.lvQueue.Items.Count; i++)
                    {
                        assign.Queue = (FS.HISFC.Models.Nurse.Queue)this.lvQueue.Items[i].Tag;
                        if (assign.Queue.Doctor.ID == reg.DoctorInfo.Templet.Doct.ID
                            && assign.Queue.SRoom.ID != null && assign.Queue.SRoom.ID != ""
                            && assign.Queue.Console.ID != null && assign.Queue.Console.ID != "")
                        {
                            assign.Register = reg;
                            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                            if (TrigeWhereFlag == "0")
                            {
                                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                            }
                            else
                            {
                                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                            }
                            #endregion
                            //assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;
                            assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                            assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime();
                            assign.Oper.OperTime = assign.TirageTime;
                            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                            assign.Queue.Dept = assign.Register.DoctorInfo.Templet.Dept;
                            assign.InTime = this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime();

                            IsFound = true;
                            break;
                        }
                    }

                    //操作数据库-------有个bug就是无法插入in_date
                    if (IsFound)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                        //SQLCA.BeginTransaction();
                        string error = "";

                        if (Function.Triage( assign, "2", ref error) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(error, "提示");
                            return;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                    }//end if

                }
            }//----------end foreach

            #endregion

        }

        #endregion

        #region 自动分诊 先对列,后诊台------------可用性不大,暂时不用
        #region 说明
        /*--------------------------------------------------------------------
		 * 1.要使用此自动分诊,则必须对每一个队列维护好诊室诊台,否则,有些不能分诊,后果没有测试
		 * 2.对专家的处理直接分诊到队列,简单易行
		 * 3.自动分诊到队列:来患者后,找出非专家队列中候诊人数最少的队列插入.
		 * 4.队列自动分诊到诊台:诊台人数小于自设常数,则放入患者.
		 * 5.插队时,停止自动分诊;插队结束后,恢复自动分诊
		 *	 敲入病历号回车时,停止自动分诊,弹出界面关闭或者MessageBoX的确定后恢复
		 * 6.注意相应队列,诊台患者数量的变化
		 * -------------------------------------------------------------------*/
        #endregion



        /// <summary>
        /// 自动分诊主函数
        /// </summary>
        private void AutoRefresh()
        {
            #region 先把界面上的基本准备准备好!
            //防止回车找不到
            this.neuTreeView1.Nodes.Clear();
            TreeNode root = new TreeNode("待分诊患者", 40, 40);
            this.neuTreeView1.Nodes.Add(root);

            this.RefreshRoom();
            this.RefreshQueue();

            if (this.lvQueue.Items.Count <= 0)
            {
                this.RefreshRegPatient();
                return;
            }

            #region 获取信息
            //获取分诊深度
            TrigeWhereFlag = this.controlMgr.QueryControlerInfo("900001");
            if (TrigeWhereFlag == null || TrigeWhereFlag == "")
            {
                TrigeWhereFlag = "0";
            }

            //是否已经全部分诊
            bool IsAll = true;

            //获取时间
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);

            //需要分诊的患者信息
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, FS.FrameWork.Management.Connection.Operator.ID);
            if (this.alCollections == null) return;

            //分诊的队列
            ArrayList al = new ArrayList();
            al = queueMgr.Query(FS.FrameWork.Management.Connection.Operator.ID, current, noonID);
            if (al == null || al.Count <= 0) return;
            #endregion
            #endregion

            #region 自动分诊到队列
            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);

            foreach (FS.HISFC.Models.Registration.Register reg in this.alCollections)
            {
                //防止并发
                if (this.JudgeTrige(reg.ID)) continue;

                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

                #region 1.对于非专家挂号,找出当前科室人数最少的队列插入
                if (reg.DoctorInfo.Templet.Doct.ID == null || reg.DoctorInfo.Templet.Doct.ID == "")
                {
                    //获取最少人数的队列
                    FS.HISFC.Models.Nurse.Queue info = new FS.HISFC.Models.Nurse.Queue();
                    info = this.GetMinQueue(al, reg.DoctorInfo.Templet.Doct.ID);
                    if (info == null)
                    {
                        IsAll = false;
                        continue;
                    }
                    //实体转化
                    assign = this.TransEntity(reg, info);

                    //数据库操作
                    string error = "";
                    //SQLCA.BeginTransaction();

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    if (Function.Triage(assign, "1", ref error) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error, "提示");
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    //刷新此队列信息
                    this.RefreshAssign(assign);
                }
                #endregion

                #region 2.对于专家挂号,找到专家队列分诊,否则不进行操作
                else
                {
                    //实体赋值
                    bool IsFound = false;
                    for (int i = 0; i < this.lvQueue.Items.Count; i++)
                    {
                        assign.Queue = (FS.HISFC.Models.Nurse.Queue)this.lvQueue.Items[i].Tag;
                        if (assign.Queue.ExpertFlag == "1" && assign.Queue.Doctor.ID == reg.DoctorInfo.Templet.Doct.ID)
                        {
                            assign = this.TransEntity(reg, assign.Queue);
                            IsFound = true;
                            break;
                        }
                    }

                    //操作数据库
                    if (IsFound)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        string error = "";

                        if (Function.Triage(assign, "1", ref error) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(error, "提示");
                            return;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {
                        IsAll = false;
                    }

                    this.RefreshAssign(assign);
                }
                #endregion
            }
            #endregion

            #region 自动分诊到诊台
            if (TrigeWhereFlag == "1")
            {
                this.AutoConsole();
            }
            #endregion

            #region 重新刷新一下界面
            //没有全部进行分诊
            if (!IsAll)
            {
                this.RefreshRegPatient();
            }
            this.neuTextBox1.Focus();
            this.RefreshRoom();
            #endregion
        }
        /// <summary>
        /// 实体转化( 挂号 + 队列 --> 分诊 )
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Nurse.Assign TransEntity(FS.HISFC.Models.Registration.Register reg,
            FS.HISFC.Models.Nurse.Queue queue)
        {
            FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

            assign.Register = reg;
            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            //assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;
            assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            assign.Oper.OperTime = assign.TirageTime;
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            assign.Queue.Dept = assign.Register.DoctorInfo.Templet.Doct;
            assign.InTime = this.assginMgr.GetDateTimeFromSysDateTime();

            assign.Queue = queue;

            return assign;
        }

        /// <summary>
        /// 自动分诊到诊台
        /// </summary>
        private void AutoConsole()
        {
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);

            //找出今天队列中的待诊信息
            ArrayList alAssign = new ArrayList();
            alAssign = this.assginMgr.Query(FS.FrameWork.Management.Connection.Operator.ID, System.DateTime.Now.Date,
                FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
            if (alAssign == null || alAssign.Count <= 0) return;

            //对队列中的待诊信息     1.如果维护了诊台，则自动分诊。
            //						 2.没有维护诊台的，不再进行操作。
            foreach (FS.HISFC.Models.Nurse.Assign info in alAssign)
            {
                if (info.Queue.Console.ID == null || info.Queue.Console.ID == "") continue;

                //分诊条件：1.诊台在treeview2中找到   2.诊台中的count为0   
                //			3.分诊根据挂号顺序，并没有根据序列号。（即:手动改变了顺序号不起作用）
                bool IsFoundConsole = false;
                foreach (FS.HISFC.Models.Nurse.Seat console in this.alSeats)
                {
                    //根据诊台找到对应队列,然后取得它现在的人数
                    if (console.ID == info.Queue.Console.ID)
                    {
                        int n = this.assginMgr.QueryConsoleNum(console.ID, current,
                            current.AddDays(1), FS.HISFC.Models.Nurse.EnuTriageStatus.In);
                        if (n >= 0 && n <
                            FS.FrameWork.Function.NConvert.ToInt32(this.TrigeWhereFlag))
                        {
                            this.alSeats.Remove(console);
                            IsFoundConsole = true;
                            break;
                        }
                    }
                }

                if (!IsFoundConsole) continue;

                //SQLCA.BeginTransaction();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.assginMgr.Update(info.Register.ID, info.Queue.SRoom, info.Queue.Console,
                    this.assginMgr.GetDateTimeFromSysDateTime()) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("自动分诊从队列到诊台出错!", "提示");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }
        #endregion

        #region 直接分诊到诊台

        /*自动分诊到诊台-----------------------------------------------------------------
		 * 1.数据准备  
		 *		(1)列出要分诊的患者. (2)列出每个诊台的信息
		 * 2.条件选择
		 *		(1)科室要是匹配的(所挂的科室要是该队列医生允许登陆的科室)
		 *		(2)对于所有允许的,找出当前人数最少的队列.
		 * 3.进行分诊
		 *		(1)同时人数变化,以免下次再次查询数据库
		 * 
		 * ----------------------------------------------------------------------------*/

        /// <summary>
        /// 自动分诊到诊台的函数
        /// </summary>
        private void Auto()
        {
            #region 先把界面上的基本准备准备好!

            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            ////定义一下分诊深度
            //TrigeWhereFlag = this.controlMgr.QueryControlerInfo("900001");
            //if (TrigeWhereFlag == null || TrigeWhereFlag == "")
            //{
            //    TrigeWhereFlag = "0";
            //} 
            #endregion

            //获取时间
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);

            //界面准备,防止回车找不到
            this.neuTreeView1.Nodes.Clear();
            TreeNode root = new TreeNode("待分诊患者");
            this.neuTreeView1.Nodes.Add(root);
            this.RefreshRoom();
            this.RefreshQueue();
            if (this.lvQueue.Items.Count <= 0)
            {
                this.RefreshRegPatient();
                return;
            }

            //获取患者信息
            //this.alCollections = this.regMgr.QueryNoTriagebyDept(current, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (this.alCollections == null || this.alCollections.Count <= 0) return;

            //获取分诊队列
            ArrayList alQueue = new ArrayList();
            //alQueue = queueMgr.Query(
            //    ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,
            //    current, 
            //    noonID);
            alQueue = queueMgr.Query(
                ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,
                current,
                noonID);

            if (alQueue == null || alQueue.Count <= 0) return;
            foreach (FS.HISFC.Models.Nurse.Queue queue in alQueue)
            {
                //借用这个字段
                queue.WaitingCount = this.assginMgr.QueryConsoleNum(queue.Console.ID, current,
                            current.AddDays(1), FS.HISFC.Models.Nurse.EnuTriageStatus.In);
            }

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance/*var.con*/);
            #endregion

            #region 专家
            foreach (FS.HISFC.Models.Registration.Register reginfo in this.alCollections)
            {
                FS.HISFC.Models.Registration.RegLevel regl = new FS.HISFC.Models.Registration.RegLevel();
                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                //排除非专家
                regl = this.regMgr.QueryRegLevelByCode(reginfo.DoctorInfo.Templet.RegLevel.ID);
                //if (reginfo.DoctorInfo.Templet.Doct.ID == null || reginfo.DoctorInfo.Templet.Doct.ID == "")
                //{
                //    continue;
                //}
                if (regl.IsExpert == false)
                {
                    continue;
                }

                //查找对应的队列
                bool IsFound = false;
               
                for (int i = 0; i < this.lvQueue.Items.Count; i++)
                {
                    assign.Queue = (FS.HISFC.Models.Nurse.Queue)this.lvQueue.Items[i].Tag;
                   
                    if (assign.Queue.Doctor.ID == reginfo.DoctorInfo.Templet.Doct.ID
                         && assign.Queue.AssignDept.ID == reginfo.DoctorInfo.Templet.Dept.ID)
                            //&& assign.Queue.Dept.ID == reginfo.DoctorInfo.Templet.Dept.ID)//assign.Queue.ExpertFlag == "1" && 
                    {
                        assign = this.TransEntity(reginfo, assign.Queue);
                        #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                        //根据分诊深度设置
                        if (this.TrigeWhereFlag == "0")
                        {
                            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                        }
                        else
                        {
                            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                        }
                        #endregion
                        IsFound = true;
                        break;
                    }
                }

                //找到队列
                if (IsFound)
                {
                    //SQLCA.BeginTransaction();

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    string error = "";

                    if (Function.Triage( assign, "1", ref error) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error, "提示");
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                //没有找到的不做操作
            }
            #endregion

            #region 非专家
            foreach (FS.HISFC.Models.Registration.Register reginfo in this.alCollections)
            {
                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                FS.HISFC.Models.Registration.RegLevel regl = new FS.HISFC.Models.Registration.RegLevel();
                //取挂号级别信息
                regl = this.regMgr.QueryRegLevelByCode(reginfo.DoctorInfo.Templet.RegLevel.ID);

                //排除专家
                //if (reginfo.DoctorInfo.Templet.Doct.ID != null && reginfo.DoctorInfo.Templet.Doct.ID != "")
                //{
                //    continue;
                //}
                if (regl.IsExpert == true)
                {
                    continue;
                }

                //该挂号科室的最小人数队列
                FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();
                queue = this.GetMinQueue(alQueue, reginfo.DoctorInfo.Templet.Dept.ID);
                if (queue == null)
                {
                    continue;
                }

                //实体转化
                assign = this.TransEntity(reginfo, queue);
                #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                if (this.TrigeWhereFlag == "0")
                {
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                }
                else
                {
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                }
                #endregion

                //数据库操作
                string error = "";
                //SQLCA.BeginTransaction();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (Function.Triage(assign, "1", ref error) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(error, "提示");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion

            #region 刷新界面
            this.RefreshRegPatient();
            this.RefreshRoom();
            #endregion

        }

        #endregion

        #region 叫号

        /// <summary>
        /// 语音叫号
        ///当前护士站对应诊室挨个叫号{63F980B1-1F3E-4a55-A7E3-34B1640772DD}
        /// </summary>
        private void CalledPatient()
        {
            foreach (ListViewItem viewItem in lvQueue.Items)
            {
                if (viewItem.Tag.GetType() == typeof(FS.HISFC.Models.Nurse.Queue))
                {
                    string queueID = (viewItem.Tag as FS.HISFC.Models.Nurse.Queue).ID;
                    //查询最近分诊的患者
                    FS.HISFC.Models.Nurse.Assign assignObj = this.assginMgr.QueryLastAssignPatient(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, this.assginMgr.GetDateTimeFromSysDateTime().Date, queueID, "2");
                    if (assignObj != null)
                    {
                        //this.controlMgr.Speak("请" + assignObj.Register.Name + "到" + assignObj.Queue.Console.Name + "就诊");
                        //this.controlMgr.Speak("请" + assignObj.Register.Name + "到" + assignObj.Queue.Console.Name + "就诊");
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        if (this.assginMgr.UpdateByCalled(queueID, assignObj.Register.ID, this.assginMgr.GetDateTimeFromSysDateTime(), "") == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新分诊状态失败：" + this.assginMgr.Err);
                            return;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
            }
        }

        #endregion

        #region 事件处理程序

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.frequence != "Auto")
            {
                this.RefreshRegPatient();
                this.RefreshRoom();
                this.AutoExpert();
                //{63F980B1-1F3E-4a55-A7E3-34B1640772DD}
                this.CalledPatient();
            }
            else
            {
                this.Auto();
            }
        }

        private void lvQueue_Click(object sender, EventArgs e)
        {
            if (this.lvQueue.SelectedItems == null ||
                this.lvQueue.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvQueue.SelectedItems[0];

            if (selected.Tag.GetType() ==
                typeof(FS.HISFC.Models.Nurse.Queue))
            {
                //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID/*var.User.Nurse.ID*/, 
                //                  this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                //                  (selected.Tag as FS.HISFC.Models.Nurse.Queue).ID);
                this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID/*var.User.Nurse.ID*/,
                                 this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                                 (selected.Tag as FS.HISFC.Models.Nurse.Queue).ID);
            }
            else
            {
                //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,/*var.User.Nurse.ID*/
                //                  this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                //                  (selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID);
                this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,/*var.User.Nurse.ID*/
                                 this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                                 (selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID);
            }
        }

        private void neuTreeView2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //获取分诊信息
                FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
                try
                {
                    info = (FS.HISFC.Models.Nurse.Assign)this.neuTreeView2.GetNodeAt(e.X, e.Y).Tag;
                    if (info != null)
                    {
                        this.TIMEToolStripMenuItem.Text =
                            "病历号:" + info.Register.PID.CardNO + "    " + "\n" +
                            "挂号科室:" + info.Register.DoctorInfo.Templet.Dept.Name + "    " + "\n" +
                            "性别:" + info.Register.Sex.Name + "    " + "\n" +
                            //							"年龄:" + age.ToString() + "---" + "\n" +
                            "挂号时间:" + info.Register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void neuLabelTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;

            string cardNo = this.neuTextBox1.Text.ToString().Trim().PadLeft(10, '0');

            //1.查找表MET_NUO_ASSIGNRECORD
            if (this.QueryAssignRecord(cardNo) == 0) return;

            //2.查找挂号表FIN_OPB_REGISTER
            this.QueryReg(cardNo);
            /*---------------------------------------------------------------------------------*
             * 对当天,同一患者挂了某一科室两个号的处理-->弹出一个列表界面以供选择.             *
             *		1.取患者今天的有效分诊信息. 置tag = 1									   *
             *		  取患者今天的有效挂号信息(分诊标志为0的). 置tag = 2                       *
             *		2.如果行数 = 1 ,直接弹出相应界面										   *
             *		3.双击,判断. tag=1 则弹出进诊界面 2 则弹出分诊界面                         *
             *---------------------------------------------------------------------------------*/
        }

        /// <summary>
        /// 查询某队列下待诊患者
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="queueID"></param>
        private void RetrieveWait(string nurseID, DateTime today, string queueID)
        {
            this.lvPatient.Items.Clear();


            this.alCollections = this.assginMgr.Query(nurseID, today.Date, queueID,
                FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Assign assgin in this.alCollections)
                {
                    this.AddAssign(assgin);
                }
            }
        }

        /// <summary>
        /// 添加分诊信息
        /// </summary>
        /// <param name="assign"></param>
        private void AddAssign(FS.HISFC.Models.Nurse.Assign assign)
        {
            ListViewItem item = new ListViewItem();
            item.Text = assign.Register.PID.CardNO;
            item.Tag = assign;
            item.SubItems.Add(assign.Register.Name);
            item.SubItems.Add(assign.Register.Sex.Name);
            #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
            if (string.IsNullOrEmpty(assign.Register.Age))
            {
                FS.HISFC.Models.Registration.Register regObj = this.regMgr.GetByClinic(assign.Register.ID);
                item.SubItems.Add(this.assginMgr.GetAge(regObj.Birthday));
            }
            else
            {
                item.SubItems.Add(assign.Register.Age);
            }
            #endregion
            item.SubItems.Add(assign.SeeNO.ToString());
            item.SubItems.Add(assign.TirageTime.TimeOfDay.ToString());

            this.lvPatient.Items.Add(item);
        }

        /// <summary>
        /// 患者队列前移一位
        /// </summary>
        private void UpMove()
        {
            if (this.lvPatient.SelectedItems == null ||
                this.lvPatient.SelectedItems.Count == 0) return;

            //获取选中项，和上一项的信息
            ListViewItem selected = this.lvPatient.SelectedItems[0];
            int currentIndex = this.lvPatient.SelectedIndices[0];
            FS.HISFC.Models.Nurse.Assign info = (FS.HISFC.Models.Nurse.Assign)selected.Tag;


            //操作数据库，交换实际看诊序号
            if (currentIndex > 0)
            {
                int priorIndex = currentIndex - 1;
                ListViewItem priorItem = this.lvPatient.Items[priorIndex];
                FS.HISFC.Models.Nurse.Assign priorinfo = (FS.HISFC.Models.Nurse.Assign)priorItem.Tag;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    if (this.assginMgr.Update(info.Register.ID, info.SeeNO.ToString(), priorinfo.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调整看诊顺序失败", "提示");
                        return;
                    }

                    if (this.assginMgr.Update(priorinfo.Register.ID, priorinfo.SeeNO.ToString(), info.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调整看诊顺序失败", "提示");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("调整看诊顺序失败" + ex.Message, "提示");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,/*var.User.Nurse.ID, */
            //                  this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date*/ 
            //                  info.Queue.ID);
            this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,/*var.User.Nurse.ID, */
                            this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date*/
                            info.Queue.ID);
            this.lvPatient.Focus();
            for (int i = 0; i < this.lvPatient.Items.Count; i++)
            {
                FS.HISFC.Models.Nurse.Assign tempinfo = (FS.HISFC.Models.Nurse.Assign)this.lvPatient.Items[i].Tag;
                if (tempinfo.Register.ID == info.Register.ID)
                {
                    this.lvPatient.Items[i].Selected = true;
                    break;
                }
            }
        }

        /// <summary>
        /// 患者队列后移一位
        /// </summary>
        private void DownMove()
        {
            if (this.lvPatient.SelectedItems == null ||
                this.lvPatient.SelectedItems.Count == 0) return;
            //获取选中项，和上一项的信息
            ListViewItem selected = this.lvPatient.SelectedItems[0];
            int currentIndex = this.lvPatient.SelectedIndices[0];
            FS.HISFC.Models.Nurse.Assign info = (FS.HISFC.Models.Nurse.Assign)selected.Tag;

            //操作数据库，交换实际看诊序号
            if (currentIndex < this.lvPatient.Items.Count - 1)
            {
                int nextIndex = currentIndex + 1;
                ListViewItem priorItem = this.lvPatient.Items[nextIndex];
                FS.HISFC.Models.Nurse.Assign nextinfo = (FS.HISFC.Models.Nurse.Assign)priorItem.Tag;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    if (this.assginMgr.Update(info.Register.ID, info.SeeNO.ToString(), nextinfo.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调整看诊顺序失败", "提示");
                        return;
                    }
                    if (this.assginMgr.Update(nextinfo.Register.ID, nextinfo.SeeNO.ToString(), info.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调整看诊顺序失败", "提示");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("调整看诊顺序失败" + ex.Message, "提示");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,/*var.User.Nurse.ID, */
            //                  this.assginMgr.GetDateTimeFromSysDateTime().Date,/* this.regMgr.GetDateTimeFromSysDateTime().Date,*/
            //                  info.Queue.ID);
            this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,/*var.User.Nurse.ID, */
                              this.assginMgr.GetDateTimeFromSysDateTime().Date,/* this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                              info.Queue.ID);
            this.lvPatient.Focus();
            for (int i = 0; i < this.lvPatient.Items.Count; i++)
            {
                FS.HISFC.Models.Nurse.Assign tempinfo = (FS.HISFC.Models.Nurse.Assign)this.lvPatient.Items[i].Tag;
                if (tempinfo.Register.ID == info.Register.ID)
                {
                    this.lvPatient.Items[i].Selected = true;
                    break;
                }
            }
        }

        private void UPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpMove();
        }

        private void DOWNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DownMove();
        }

        private void TIMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取分诊信息
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            try
            {
                info = (FS.HISFC.Models.Nurse.Assign)this.neuTreeView2.SelectedNode.Tag;
                if (info != null)
                {
                    this.TIMEToolStripMenuItem.Text =
                        "病历号:" + info.Register.PID.CardNO + "\n" +
                        "挂号科室:" + info.Register.DoctorInfo.Templet.Dept.Name + "\n" +
                        "性别:" + info.Register.Sex.Name + "\n" +
                        "年龄:" + info.Register.Age.ToString() + "\n" +
                        "挂号时间:" + info.Register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");
                }
            }
            catch 
            {
                //MessageBox.Show("请选择一名患者", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 判断挂号信息的有效性
        /// </summary>
        /// <param name="reginfo"></param>
        /// <returns></returns>
        private int ValidRegInfo(FS.HISFC.Models.Registration.Register reginfo)
        {
            //挂号表没有找到该病历号的患者
            if (reginfo == null || reginfo.PID.CardNO == null || reginfo.PID.CardNO == "")
            {
                MessageBox.Show("没有该患者信息!", "提示");
                this.neuTextBox1.Focus();
                this.neuTextBox1.SelectAll();
                return -1;
            }
            //			//该患者今天没有挂号信息
            //			if(reginfo.RegDate.ToString("yyyy-MM-dd") != this.assginMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd"))
            //			{
            //				MessageBox.Show("该患者今天没有挂号!","提示");
            //				this.textBox1.Focus();
            //				this.textBox1.SelectAll();
            //				return -1 ;
            //			}
            //该患者今天已经退号
            //			if(this.Judgeback(reginfo.ID))
            //			{
            //				MessageBox.Show("该患者最后一次挂号信息已经退号!\n如果确定患者还有有效挂号信息,请刷新屏幕!并在屏幕中进行分诊!","提示");
            //				return -1 ;
            //			}
            //防止并发
            if (this.JudgeTrige(reginfo.ID))
            {
                MessageBox.Show("该患者已经分诊!", "提示");//\n如果确定患者还有有效挂号信息,请刷新屏幕!并在屏幕中进行分诊!
                return -1;
            }
            //该患者不属于当前护理站，继续则 当作换科处理
            if (this.assginMgr.QueryNurseByDept(reginfo.DoctorInfo.Templet.Dept.ID) != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID/* var.User.Nurse.ID*/)
            {
                if (MessageBox.Show("患者挂号科室[" + reginfo.DoctorInfo.Templet.Dept.Name + "]不属于您的护理站!" + "\n" + "是否继续？", "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return -1;
            }
            return 0;
        }
        /// <summary>
        /// 根据病历号在分诊表中查找分诊信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns>找到返回 0  未找到返回 -1</returns>
        private int QueryAssignRecord(string cardNo)
        {
            DateTime dt = this.assginMgr.GetDateTimeFromSysDateTime().Date;
            FS.HISFC.Models.Nurse.Assign assign = this.assginMgr.Query(dt, cardNo);
            //add by Niuxy  判断是否为空    
            if (assign == null)
            {
                return -1;
            }
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = assign.Register.DoctorInfo.Templet.Dept.ID;

            ArrayList altemp = this.controlMgr.QueryNurseStationByDept(obj,"14");

            if (altemp.Count != 0)
            {   
                //by niuxinyan
                //if ((altemp[0] as FS.FrameWork.Models.NeuObject).ID == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
                //{
                //    MessageBox.Show("没有改患者信息：\n1 没有挂号或不是本护理站患者\n2 该患者已经被分诊\n3 该患者已经出诊");
                //    return -2;
                //}
                if ((altemp[0] as FS.FrameWork.Models.NeuObject).ID == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                {
                    MessageBox.Show("没有该患者信息：\n1 没有挂号或不是本护理站患者\n2 该患者已经被分诊\n3 该患者已经出诊");
                    return -2;
                }
            }
            

            //if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
            {
                //对已经分诊的信息,入诊室
                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                {
                    ucInRoom f = new ucInRoom(FS.FrameWork.Management.Connection.Operator.ID);
                    f.Assign = assign;
                    f.OK +=new ucInRoom.MyDelegate(In_OK);
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(f);

                    /*
                    frmInRoom f = new frmInRoom(var.User.Nurse.ID, var);
                    f.Assign = assign;

                    f.OK += new Nurse.frmInRoom.MyDelegate(In_OK);
                    f.ShowDialog();

                    f.Dispose();
                    */
                    return 0;
                }
                return -1;
            }

            return -1;

        }
        private void uc_OnSelect(FS.HISFC.Models.Registration.Register res)
        {
            this.IsCancel = false;
        }
        /// <summary>
        /// 挂号表中查找
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        private int QueryReg(string cardNo)
        {
            //获取有效期内的所有信息
            int validDays = FS.FrameWork.Function.NConvert.ToInt32(this.controlMgr.QueryControlerInfo("MZ0014"));
            DateTime dtvalid = this.assginMgr.GetDateTimeFromSysDateTime().Date.AddDays(1 - validDays);// this.regMgr.GetDateTimeFromSysDateTime().Date.AddDays(1 - validDays);
            //ArrayList al = this.regMgr.Query(cardNo, dtvalid);
            ArrayList al = new ArrayList();
            al = this.regMgr.QueryUnionNurse(cardNo, dtvalid);
            FS.HISFC.Models.Registration.Register reginfo = new FS.HISFC.Models.Registration.Register();


            //大于一条挂号信息
            if (al.Count > 1)
            {
                Nurse.ucPopPatient uc = new ucPopPatient();
                uc.OnSelect+=new ucPopPatient.SelectHander(uc_OnSelect);
                uc.alAll = al;
                uc.Init();
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (IsCancel == true)
                {
                    IsCancel = false;
                    return 2;
                }

                reginfo = uc.retInfo;
            }
            else if (al.Count == 1)//只有一条挂号信息
            {
                reginfo = (FS.HISFC.Models.Registration.Register)al[0];
            }
            //没有找到
            else if (al.Count <= 0)
            {
                return -1;
            }

            if (reginfo == null && reginfo.PID.CardNO == null)
            {
                return -1;
            }
            //			//判断条件
            //			if(this.ValidRegInfo(reginfo) == -1 ) return -1;

            //防止自动分诊的时候把树给刷新没有了.....
            if (frequence == "Auto" && this.neuTreeView1.Nodes.Count <= 0)
            {
                this.neuTreeView1.Nodes.Clear();
                TreeNode root = new TreeNode("待分诊患者", 40, 40);
                this.neuTreeView1.Nodes.Add(root);
            }
            //遍历树查找

            bool IsFound = false;
            foreach (TreeNode nodeCompare in this.neuTreeView1.Nodes[0].Nodes)
            {
                FS.HISFC.Models.Registration.Register obj =
                    (FS.HISFC.Models.Registration.Register)nodeCompare.Tag;
                if (obj.ID ==reginfo.ID && obj.PID.CardNO == reginfo.PID.CardNO && obj != null && obj.PID.CardNO != null)
                {
                    IsFound = true;
                    this.neuTreeView1.SelectedNode = nodeCompare;
                    this.Triage();
                    break;
                }
            }

            //未找到
            if (!IsFound)
            {
                TreeNode node = new TreeNode();

                if (reginfo.DoctorInfo.Templet.Doct.ID != null && reginfo.DoctorInfo.Templet.Doct.ID != "")
                {
                    node.Text = reginfo.Name + " [" + reginfo.DoctorInfo.Templet.RegLevel.Name + "]" + " [" + reginfo.DoctorInfo.Templet.Doct.Name + "]" + reginfo.DoctorInfo.Templet.Doct.Name + "-" + reginfo.DoctorInfo.SeeDate.ToString();
                }
                else
                {
                    node.Text = reginfo.Name + " [" + reginfo.DoctorInfo.Templet.RegLevel.Name + "]" + reginfo.DoctorInfo.Templet.Dept.Name + "-" + reginfo.DoctorInfo.SeeDate.ToString();
                }

                //node.Text = reginfo.Name + "  [" + reginfo.PID.CardNO + "] " + reginfo.DoctorInfo.Templet.Dept.Name;
                node.ImageIndex = 36;
                node.SelectedImageIndex = 35;
                node.Tag = reginfo;

                this.neuTreeView1.Nodes[0].Nodes.Add(node);
                this.neuTreeView1.SelectedNode = node;
                this.Triage();
            }
            return 0;
        }

        private void ucArray_Load(object sender, EventArgs e)
        {
            #region {4B7F932C-1C2A-4b65-B6F4-C2C1EFA19B12}
            #region 屏蔽
            // [2007/03/13] 读配置文件,如果存在(患者就诊信息查询)
            //if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
            //{
            //    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
            //    System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/Setting/arrangeSetting.xml", System.IO.FileMode.Open);

            //    RefreshFrequence refres = (RefreshFrequence)xs.Deserialize(fs);
            //    this.neuGroupBox1.Visible = refres.IsAutoTriage;

            //    switch (refres.RefreshTime.Trim())
            //    {
            //        case "auto":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbAuto.Checked = true;
            //            break;
            //        case "no":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbNo.Checked = true;
            //            break;
            //        case "10":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rb10.Checked = true;
            //            break;
            //        case "30":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rb30.Checked = true;
            //            break;
            //        case "60":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rb60.Checked = true;
            //            break;
            //        case "300":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbOther.Checked = true;
            //            break;
            //        default:
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbNo.Checked = true;
            //            break;
            //    }
            //}
            //else
            //{
            //    this.SetRadioFalse();
            //    this.rbNo.Checked = true;
            //} 
            #endregion
            #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
            this.doctHelper.ArrayObject = controlMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            this.Init();
            #endregion
            this.LoadMenuSet();
            #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
            this.CreateHeaders();
            

            if (this.queType == QueDisplayType.Detail)
            {
                this.lvQueue.View = View.Details;
            }
            else
            {
                this.lvQueue.View = View.LargeIcon;
            }

            if (this.frmDspType == FormDisplayMode.Vertical)
            {
                this.pnlQueue.Dock = DockStyle.Left;

                this.pnlQueue.Width = 300;

                this.neuSplitter3.Dock = DockStyle.Left;
            }
            else
            {
                this.pnlQueue.Dock = DockStyle.Top;

                this.pnlQueue.Height = 200;

                this.neuSplitter3.Dock = DockStyle.Top;
            }

            #endregion
            #endregion
            InitNurseRegister();

            // [2007/03/13] 结束
        }

        /// <summary>
        /// 所有RadioButton的
        /// </summary>
        private void SetRadioFalse()
        {
            // 不刷新
            this.rbNo.Checked = false;
            // 10秒
            this.rb10.Checked = false;
            // 30秒
            this.rb30.Checked = false;
            // 一分钟
            this.rb60.Checked = false;
            // 五分钟
            this.rbOther.Checked = false;
            // 自动
            this.rbAuto.Checked = false;
        }

        #endregion

        private void rbAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbAuto.Checked)
            {
                this.frequence = "Auto";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "auto";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 20000;
                this.timer1.Enabled = true;
            }
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbNo.Checked)
            {
                this.frequence = "None";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "no";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Enabled = false;
            }
        }

        private void rb10_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb10.Checked)
            {
                this.frequence = "10";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "10";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 10000;
                this.timer1.Enabled = true;
            }
        }

        private void rb30_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb30.Checked)
            {
                this.frequence = "30";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "30";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 30000;
                this.timer1.Enabled = true;
            }
        }

        private void rb60_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb60.Checked)
            {
                this.frequence = "60";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "60";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 60000;
                this.timer1.Enabled = true;
            }
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOther.Checked)
            {
                this.frequence = "300";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "300";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 300000;
                this.timer1.Enabled = true;
            }
        }

        private void neuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            IsCancel = true;

            string txtStr = this.neuTextBox1.Text.Trim();

            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            string cardNo = string.Empty;
            if (isAccountTermianFee && feeMgr.ValidMarkNO(txtStr, ref accountCard) > 0)
            {
                GetAccountRegister(accountCard);
            }
            else
            {

                cardNo = txtStr.PadLeft(10, '0');
                //2.查找挂号表FIN_OPB_REGISTER
                int returnValue = this.QueryReg(cardNo);
                if (returnValue == 2) return;
                if (returnValue == -1)
                {
                    MessageBox.Show("没有该患者的挂号信息");
                    return;
                }
            }
            /*---------------------------------------------------------------------------------*
             * 对当天,同一患者挂了某一科室两个号的处理-->弹出一个列表界面以供选择.             *
             *		1.取患者今天的有效分诊信息. 置tag = 1									   *
             *		  取患者今天的有效挂号信息(分诊标志为0的). 置tag = 2                       *
             *		2.如果行数 = 1 ,直接弹出相应界面										   *
             *		3.双击,判断. tag=1 则弹出进诊界面 2 则弹出分诊界面                         *
             *---------------------------------------------------------------------------------*/
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            #region {284437CB-1E5B-4fec-93AB-81924370D443}
            //if (keyData == Keys.F1)
            //{
            //    this.Triage();
            //}
            //else if (keyData == Keys.F2)
            //{
            //    this.In();
            //}
            //else if (keyData == Keys.F3)
            //{
            //    //this.CancelIn();
            //}
            //else if (keyData == Keys.F5)
            //{
            //    this.RefreshRegPatient();
            //    this.RefreshRoom();
            //}
            if (keyData == Keys.F11)
            {
                this.DownMove();
            }
            else if (keyData == Keys.F12)
            {
                this.UpMove();
            }
            else if (keyData.GetHashCode() == Keys.Add.GetHashCode())
            {
                this.DownMove();
            }
            else if (keyData.GetHashCode() == Keys.Subtract.GetHashCode())
            {
                this.UpMove();
            }
            #endregion

            return base.ProcessDialogKey(keyData);
        }

        private void txtCard2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;


            DateTime dt = this.assginMgr.GetDateTimeFromSysDateTime().Date;
            string cardNo = this.txtCard2.Text.ToString().Trim().PadLeft(10, '0');
            FS.HISFC.Models.Nurse.Assign assign = this.assginMgr.Query(dt, cardNo);
            bool bl = false;

            //if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID /*var.User.Nurse.ID*/)
              if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID /*var.User.Nurse.ID*/)
            {
                //查询已经进诊的信息
                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                {
                    bl = true;

                    string temp = "患者名称:" + assign.Register.Name + "\n"
                        + "-----------------------------" + "\n"
                        + "诊室:" + assign.Queue.SRoom.Name + "\n"
                        + "诊台:" + assign.Queue.Console.Name + "\n"
                        + "看诊医生:" + this.GetDoct(assign.Queue.Console.ID) + "\n"
                        + "挂号时间:" + assign.Register.DoctorInfo.SeeDate.ToString();
                    MessageBox.Show(temp, assign.Register.PID.CardNO);
                }
            }
            if (!bl)
            {
                MessageBox.Show("没有有效进诊信息!");
            }
        }

        #region {51AB60B7-8610-40c4-B5BC-CD978E788892}

        /// <summary>
        /// 创建ListView的列
        /// </summary>
        private void CreateHeaders()
        {
            //
            ColumnHeader colHead;
            colHead = new ColumnHeader();
            colHead.Text = "队列名称";
            colHead.Width = 150;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "科室";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "医生";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "医生编号";
            colHead.Width = 80;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "诊室";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "候诊人数";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);

        }

        private void lvQueue_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lvQueue.SelectedItems == null || this.lvQueue.SelectedItems.Count == 0)
                {
                    this.lvQueue.ContextMenuStrip = this.mnuSet;
                    this.MenuSetControl(true);
                }
                else
                {
                    this.lvQueue.ContextMenuStrip = null;
                }
            }
        }

        /// <summary>
        /// 控制设置菜单List的显示方式
        /// </summary>
        /// <param name="flag"></param>
        private void MenuSetControl(bool flag)
        {
            this.mnuI_Large.Checked = false;
            this.mnuI_Detail.Checked = false;

            if (this.lvQueue.Visible)
            {
                switch (this.lvQueue.View)
                {
                    case View.LargeIcon:
                        this.mnuI_Large.Checked = flag;
                        break;
                    case View.Details:
                        this.mnuI_Detail.Checked = flag;
                        break;
                    default:
                        this.mnuI_Large.Checked = flag;
                        break;
                }
            }
        }

        private void mnuI_Large_Click(object sender, EventArgs e)
        {
            this.lvQueue.Visible = true;

            this.lvQueue.View = View.LargeIcon;
            this.MenuSetControl(true);
        }

        private void mnuI_Detail_Click(object sender, EventArgs e)
        {
            this.lvQueue.Visible = true;

            this.lvQueue.View = View.Details;
            this.MenuSetControl(true);
        }


        #endregion

        #region 账户增加

        private int InitNurseRegister()
        {
            object obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister));
            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            //if (obj == null)
            //{
            //    MessageBox.Show("请维护接口FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister的实例对照!");
            //    return -1;
            //}
            if (obj != null)
            {
                this.iNurseRegiser = obj as FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister;
                iNurseRegiser.OnGetRegister += new FS.HISFC.BizProcess.Interface.Registration.GetRegisterHander(iNurseRegiser_OnGetRegister);
            }
            #endregion
            return 1;
        }

        void iNurseRegiser_OnGetRegister(ref FS.HISFC.Models.Registration.Register reg)
        {
            TreeNode node = new TreeNode();
            node.ImageIndex = 36;
            node.SelectedImageIndex = 35;
            node.Text = reg.Name;
            node.Tag = reg;
            this.neuTreeView1.Nodes[0].Nodes.Add(node);
            this.neuTreeView1.SelectedNode = node;
        }

        private int GetAccountRegister(FS.HISFC.Models.Account.AccountCard accountCard)
        {

            decimal vacancy = 0m;
            //通过病历号判断账户状态是否开启
            int returnValue = this.feeMgr.GetAccountVacancy(accountCard.Patient.PID.CardNO, ref vacancy);

            if (returnValue <= 0)
            {
                MessageBox.Show(feeMgr.Err);
                return -1;
            }

            if (!feeMgr.CheckAccountPassWord(accountCard.Patient))
            {
                return -1;
            }
            iNurseRegiser.Patient = accountCard.Patient;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(iNurseRegiser as Control);
            return 1;
        }
        #endregion

        #region IInterfaceContainer 成员

        /// <summary>
        /// 接口容器
        /// </summary>
        public Type[] InterfaceTypes
        {
            get 
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister);
                return types;
            }
        }

        #endregion

        #region addby xuewj 2010-11-4 {FCEC42B4-DF78-45c2-8D1A-EDAB94AA56DD} 分诊时修改患者基本信息
        private void neuTreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            // 加入右键菜单  by zlw 2006-5-1
            System.Windows.Forms.ContextMenu cmPatientPro = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem miPatientPro = new System.Windows.Forms.MenuItem();

            cmPatientPro.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { miPatientPro });

            miPatientPro.Text = "修改挂号信息";
            this.neuTreeView1.ContextMenu = cmPatientPro;

            miPatientPro.Click += new System.EventHandler(this.miPatientPro_Click);
        }

        private void miPatientPro_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.Models.Registration.Register findPatient = null;
            System.Windows.Forms.TreeNode node = this.neuTreeView1.SelectedNode;
            if (node == null) return;
            findPatient = node.Tag as FS.HISFC.Models.Registration.Register;
            if (findPatient == null)
            {
                return;
            }
            else
            {
                if (this.regMgr.QueryIsCancel(findPatient.ID) == true)
                {
                    MessageBox.Show("挂号信息已作废，请刷新界面!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.regMgr.QueryIsTriage(findPatient.ID) == true)
                {
                    MessageBox.Show("该患者已分诊，请刷新界面!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ucUpdateRegInfo ucUpdate = new ucUpdateRegInfo();
                ucUpdate.PatientInfo = findPatient.Clone();
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucUpdate);
                this.RefreshRegPatient();
            }
        } 
        #endregion

    }
    #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
    /// <summary>
    /// 队列显示模式
    /// </summary>
    public enum QueDisplayType
    {
        /// <summary>
        /// 大图标
        /// </summary>
        Large,
        /// <summary>
        /// 明细列表
        /// </summary>
        Detail
    }

    /// <summary>
    /// 队列和分诊列表的显示模式
    /// </summary>
    public enum FormDisplayMode
    {
        /// <summary>
        /// 横向
        /// </summary>
        Horizontal,
        /// <summary>
        /// 竖向
        /// </summary>
        Vertical
    }
    #endregion
}