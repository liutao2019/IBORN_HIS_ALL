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
    /// [功能描述: 门诊配发药]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2006-11]<br></br>
    /// <说明
    ///	    1 需要通过窗口Tag传入窗口功能  所以这个枚举FS.HISFC.Components.DrugStore.OutpatientWinFun用处不大
    ///     2 配药终端调剂初始化 通过 数据库 Job来执行 
    ///     3 为了控制同时登陆一个配药终端，使用Memo字段进行排它控制。Memo为‘1’ 说明正在使用中
    ///  />
    /// <修改记录>
    ///    1.修改配药窗口退出后不能还原窗口登录状态的问题 by Sunjh 2010-9-13 {52F2E611-215F-46b1-9594-4688D12CEFE1} 
    /// </修改记录>
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
            //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("更新配药终端为可用标记失败"));
            //            return;
            //        }
            //    }
            //}

            //修改配药窗口退出后不能还原窗口登录状态的问题 by Sunjh 2010-9-13 {52F2E611-215F-46b1-9594-4688D12CEFE1}
            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
            {
                FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
                this.Terminal = drugStoreManager.GetDrugTerminalById(this.Terminal.ID);
                this.Terminal.Memo = "";
                if (drugStoreManager.UpdateDrugTerminal(this.Terminal) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("更新配药终端为可用标记失败"));
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

        #region 域变量

        /// <summary>
        /// 操作的门诊终端类型
        /// </summary>
        private FS.HISFC.Components.DrugStore.OutpatientFun funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;

        /// <summary>
        /// 操作科室 本次登陆选择的科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject OperDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 操作人员
        /// </summary>
        private FS.FrameWork.Models.NeuObject OperInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 核准科室 扣库科室 
        /// </summary>
        private FS.FrameWork.Models.NeuObject ApproveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 门诊终端
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugTerminal Terminal = new FS.HISFC.Models.Pharmacy.DrugTerminal();

        /// <summary>
        /// 窗口功能
        /// </summary>
        private FS.HISFC.Components.DrugStore.OutpatientWinFun winFun = FS.HISFC.Components.DrugStore.OutpatientWinFun.配药;

        /// <summary>
        /// 是否其他药房配/发药
        /// </summary>
        private bool isOtherDrugDept = false;

        /// <summary>
        /// 当前窗口是否加载失败
        /// </summary>
        private bool isCancel = false;

        /// <summary>
        /// 时间间隔
        /// </summary>
        private System.Windows.Forms.Timer t = null;

        /// <summary>
        /// 时间间隔
        /// </summary>
        private int iTimerTick = 0;

        /// <summary>
        /// 信息提示
        /// </summary>
        private string msg = "";

        #endregion

        #region  属性

        /// <summary>
        /// 窗口功能
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
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.配药:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.发药:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.直接发药:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.DirectSend;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.还药:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Back;
                        this.isOtherDrugDept = false;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.其他药房配药:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.isOtherDrugDept = true;
                        break;
                    case FS.HISFC.Components.DrugStore.OutpatientWinFun.其他药房发药:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                        this.isOtherDrugDept = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 提示信息
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

        #region 初始化

        /// <summary>
        /// 初始化
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
        /// 终端初始化  暂时写死使用配药台
        /// </summary>
        protected int InitTerminal()
        {
            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
                this.Terminal = FS.HISFC.Components.DrugStore.Function.TerminalSelect(this.OperDept.ID, FS.HISFC.Models.Pharmacy.EnumTerminalType.配药台, true);
            else
                this.Terminal = FS.HISFC.Components.DrugStore.Function.TerminalSelect(this.OperDept.ID, FS.HISFC.Models.Pharmacy.EnumTerminalType.发药窗口, true);

            if (this.Terminal == null)
            {
                return -1;
            }

            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
            {
                if (this.Terminal.Memo == "1")
                {
                    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("该终端已在其他电脑登陆，不能再次使用，您确认登陆此配药终端吗？\n 注意：如果强行登陆后容易造成配药清单打印混乱！"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("更新配药终端为已在用标记失败"));
                    this.isCancel = true;
                    this.Terminal.Memo = "";
                    return -1;
                }
            }

            this.statusBar1.Panels[3].Text = this.statusBar1.Panels[3].Text + " - " + this.OperDept.Name + this.Terminal.Name + "[" + this.Terminal.ID + "]";

            return 1;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected void InitControlParm()
        {
            this.ucClinicTree1.OperDept = this.OperDept;
            this.ucClinicTree1.OperInfo = this.OperInfo;
            this.ucClinicTree1.ApproveDept = this.ApproveDept;     //核准科室 实际扣库科室
            this.ucClinicTree1.SetFunMode(this.funMode);
            this.ucClinicTree1.SetTerminal(this.Terminal);

            this.ucClinicTree1.IsFindToAdd = true;

            this.ucClinicDrug1.IsShowDrugSendInfo = false;      //不显示配/发药信息

            this.ucClinicDrug1.OperDept = this.OperDept;
            this.ucClinicDrug1.OperInfo = this.OperInfo;
            this.ucClinicDrug1.ApproveDept = this.ApproveDept;     //核准科室 实际扣库科室
            this.ucClinicDrug1.SetFunMode(this.funMode);
            this.ucClinicDrug1.SetTerminal(this.Terminal);
        }

        #endregion

        /// <summary>
        /// 信息显示
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
        /// 保存
        /// </summary>
        public void Save()
        {
            this.statusBar1.Panels[1].Text = "正在保存...";

            this.ucClinicTree1.IsBusySave = true;

            this.ucClinicDrug1.Save();

            this.ucClinicTree1.IsBusySave = false;
        }

        /// <summary>
        /// 退出判断 是否允许关闭窗口
        /// </summary>
        /// <returns></returns>
        public bool EnableExit()
        {
            if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Drug)
            {
                if (this.ucClinicTree1.SpareNode())
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("尚有未配药确认的处方 请对所有处方完成配药 进行配药确认后再关闭窗口"));
                    return false;
                }
            }
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
           
            base.OnLoad(e);

            //不写这句话窗口无法最大化
            this.WindowState = FormWindowState.Maximized;           

            try
            {               
                //控制参数管理类
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
                    //默认不进行提示
                    //MessageBox.Show("未配置处方单打印的实现，将无法进行处方单据打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.isCancel = true;

                    //#region 反射读取标签格式     

                    //try
                    //{
                    //    #region 标签/清单打印 接口实现

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
                    //    MessageBox.Show(Language.Msg("标签命名空间无效\n" + ex.Message));
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
                    //默认不进行提示
                    //MessageBox.Show("未配置处方单打印的实现，将无法进行处方单据打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.isCancel = true;

                    //#region 门诊处方打印接口实现

                    //object[] o1 = new object[] { };
                    ////门诊处方打印接口类实现
                    //string recipeValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Clinic_Print_Recipe, true, "Report.Order.ucRecipePrint");

                    ////处方
                    //System.Runtime.Remoting.ObjectHandle objHandel1 = System.Activator.CreateInstance("Report", recipeValue, false, System.Reflection.BindingFlags.CreateInstance, null, o1, null, null, null);
                    //object oLabel1 = objHandel1.Unwrap();

                    //FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug.RecipePrint = oLabel1 as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;

                    //if (FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug.RecipePrint == null)
                    //{
                    //    this.isCancel = true;
                    //}

                    //#endregion
                }

                //控件初始化 需先调用完本窗口初始化信息后再调用控件初始化
                this.ucClinicTree1.Init();
                this.ucClinicDrug1.Init();

                //列表初始化刷新
                this.ucClinicTree1.RefreshOperList(true);
                //大屏幕显示接口启动 发药窗口使用
                if (this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.Send || this.funMode == FS.HISFC.Components.DrugStore.OutpatientFun.DirectSend)
                {
                    if (this.ucClinicTree1.IsShowFeeData)
                    {
                        this.ucClinicTree1.BeginLEDRefresh(true);
                    }
                }

                //间隔2秒后启动自动刷新程序 如果立刻启动 那么大量单据待打印时会造成打开窗口长时间延迟
                //类似死机(用打印命令方式打印标签时会出现)
                if (!this.tsbRefreshWay.Checked)
                {
                    this.ucClinicTree1.BeginProcessRefresh(2000);
                }

                this.ucClinicTree1.SetFocus();
            
                //屏蔽暂停按钮
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
            if (e.ClickedItem == this.tsbExit)          //退出
            {
                if (this.EnableExit())
                {
                    this.Close();
                }
                return;
            }        

            if (e.ClickedItem == this.tsbSave)          //保存
            {
                this.Save();
                return;
            }
            if (e.ClickedItem == this.tsbRefresh)       //刷新
            {
                this.ucClinicDrug1.Clear();

                this.ucClinicTree1.ShowList();
                return;
            }
            if (e.ClickedItem == this.tsbQuery)         //查询
            {
                this.ucClinicTree1.FindNode();
                return;
            }
            if (e.ClickedItem == this.tsbPrint)         //打印
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
            if (e.ClickedItem == this.tsbPause)         //暂停打印
            {
                FS.FrameWork.WinForms.Classes.Print.PausePrintJob(0);
                return;
            }
            if (e.ClickedItem == this.tsbRefreshWay)    //手工刷新 / 自动刷新
            {
                this.tsbRefreshWay.Checked = !this.tsbRefreshWay.Checked;

                if (this.tsbRefreshWay.Checked)         //手工刷新状态
                {
                    this.ucClinicTree1.IsAutoPrint = false;
                    this.tsbRefreshWay.Text = FS.FrameWork.Management.Language.Msg( "自动打印" );

                    this.statusBar1.Panels[1].Text = FS.FrameWork.Management.Language.Msg( "手工打印状态 停止自动打印" );
                }
                else
                {
                    this.ucClinicTree1.IsAutoPrint = true;
                    this.tsbRefreshWay.Text = FS.FrameWork.Management.Language.Msg( "手动打印" );
                    this.statusBar1.Panels[1].Text = "自动打印状态..." + (this.ucClinicTree1.IsBusySave ? "正在保存" : "正在刷新");
                   
                }

                //if (this.tsbRefreshWay.Checked)         //手工刷新状态
                //{
                //    this.ucClinicTree1.EndProcessRefresh();
                //    this.tsbRefreshWay.Text = "自动刷新";

                //    this.statusBar1.Panels[1].Text = "手工刷新状态 停止自动刷新";
                //}
                //else
                //{
                //    this.ucClinicTree1.BeginProcessRefresh(1000);
                //    this.tsbRefreshWay.Text = "手工刷新";
                //}

                return;
            }
            if (e.ClickedItem == this.tsbRecipe)        //处方
            {
                this.tsbRecipe.Checked = !this.tsbRecipe.Checked;

                this.ucClinicDrug1.IsPrintRecipe = this.tsbRecipe.Checked;
                return;
            }
            if (e.ClickedItem == this.tsbDrugList)      //发药清单
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
            //如果不想员工号回车保存 可以屏蔽不处理此事件
            this.Save();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Divide)                         //退出
            {
                this.toolStrip1_ItemClicked(null,new ToolStripItemClickedEventArgs(this.tsbExit));

                return true;
            }
            if (keyData == Keys.E )     //保存
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
        /// 关闭时进行资源清理  {1367F373-862B-4ff5-A14A-F0DB46092776}
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            try
            {
                //停止当前线程
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


        #region IPreArrange 成员

        bool isPreArrange = false;

        public int PreArrange()
        {
            this.isPreArrange = true;

            #region 根据窗口参数 设置窗口功能

            if (this.Tag != null)
            {
                switch (this.Tag.ToString().ToUpper())
                {
                    case "DRUG":        //配药
                    case "ODRUG":       //其他药房配药
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.Text = "门诊配药";
                        break;
                    case "SEND":        //发药
                    case "OSEND":       //其他药房发药
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                        this.Text = "门诊发药";
                        break;
                    case "BACK":        //还药
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Back;
                        this.Text = "门诊还药";
                        break;
                    case "DSEND":       //直接发药 (不经过配药)
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.DirectSend;
                        this.Text = "门诊直接发药";
                        break;
                     //屏蔽其他药房配、发药功能 实际验证用处不大
                    //case "ODRUG":       //其他药房配药
                    //    this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                    //    this.isOtherDrugDept = true;
                    //    this.Text = "其他药房配药";
                    //    break;
                    //case "OSEND":       //其他药房发药
                    //    this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Send;
                    //    this.isOtherDrugDept = true;
                    //    this.Text = "其他药房发药";
                    //    break;
                    default:
                        this.funMode = FS.HISFC.Components.DrugStore.OutpatientFun.Drug;
                        this.Text = "门诊配药";
                        break;
                }
            }

            #endregion

            //本窗口基本信息获取 各控件信息赋值
            if (this.Init() == -1)
            {
                this.isCancel = true;
                return -1;
            }

            return 1;
        }

        #endregion

        #region IInterfaceContainer 成员

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