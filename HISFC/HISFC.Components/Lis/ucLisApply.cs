using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Order;
using FS.HISFC.Models.RADT;

namespace UFC.Lis
{
    /// <summary>
    /// [功能描述: 检验单主界面]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2008-03]<br></br>
    /// </summary>
    public partial class ucLisApply : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucLisApply()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 业务管理类
        /// </summary>
        private FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 患者信息列表
        /// </summary>
        protected ArrayList myPatients = null;

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        #endregion

        #region 属性

        /// <summary>
        /// 是否补打
        /// </summary>
        protected bool RePrint
        {
            get
            {
                return this.ckPrePrint.Checked;
            }
        }

        /// <summary>
        /// 起始时间
        /// </summary>
        protected DateTime BeginDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpBegin.Text);
            }
        }

        /// <summary>
        /// 终止时间
        /// </summary>
        protected DateTime EndDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpEnd.Text);
            }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        public ArrayList Patients
        {
            get
            {
                return this.myPatients;
            }
            set
            {
                this.myPatients = value;
            }
        }

        #endregion        

        #region 检验单打印接口变量、属性

        /// <summary>
        /// 界面显示控件
        /// </summary>
        protected FS.FrameWork.WinForms.Controls.ucBaseControl displayControl = new ucLisApplyControl();

        /// <summary>
        /// 单据打印控件
        /// </summary>
        protected FS.FrameWork.WinForms.Controls.ucBaseControl printControl = new ucPrintLisApply();

        /// <summary>
        /// Lis数据处理接口
        /// </summary>
        protected ILisDB ILisDBInstance = null;
        #endregion
       
        /// <summary>
        /// 查询
        /// </summary>
        protected virtual void Query()
        {
            if (this.ILisDBInstance != null)
            {
                if (this.ILisDBInstance.ConnectLisOnQuery() == -1)
                {
                    MessageBox.Show("连接Lis数据库失败");
                }
            }

            ArrayList al = new ArrayList();
            if (myPatients == null)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询检验单信息...");
            Application.DoEvents();

            for (int i = 0; i < this.myPatients.Count; i++)
            {
                ArrayList alOrder = alOrder = orderManager.QueryOrderLisApplyBill(((FS.FrameWork.Models.NeuObject)this.myPatients[i]).ID, this.BeginDate, this.EndDate, this.RePrint);
                if (alOrder == null)
                {
                    MessageBox.Show(orderManager.Err);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //患者诊断信息 赋值
                FS.HISFC.Models.RADT.PatientInfo p = ((FS.HISFC.Models.RADT.PatientInfo)myPatients[i]).Clone();	
                string diagnose = this.GetDiagnose(p);

                string strDocName = "";
                string strDiff = "";
                string strItems = "";
                string strID = "";//为了更新打印的索引

                FS.HISFC.Models.Order.ExecOrder exeTempOrder;		//临时变量 存储执行档信息
                //p.User01 主键ID p.User02 样本  p.User03 项目
                //p.PVisit.User01 执行科室  p.PVisit.User02 医生  p.PVisit.User01 送检日期 p.PVisit.Memo 数量
                for (int j = 0; j < alOrder.Count; j++)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(j);
                    Application.DoEvents();

                    exeTempOrder = alOrder[j] as FS.HISFC.Models.Order.ExecOrder;

                    if (strDiff != exeTempOrder.Order.Combo.ID + exeTempOrder.DateUse.ToString("YYYY-MM-DD HH:mm") + exeTempOrder.Order.Sample.Name)
                    {
                        #region 不同组别项目
                        //添加上次的项目
                        if (strItems != "")
                        {
                            p.User01 = strID;		        //上次索引
                            p.User03 = strItems;

                            p.PVisit.User02 = strDocName;

                            al.Add(p.Clone());				//上次的检验单
                        }

                        strDiff = exeTempOrder.Order.Combo.ID + exeTempOrder.DateUse.ToString("YYYY-MM-DD HH:mm") + exeTempOrder.Order.Sample.Name;
                        //患者诊断信息赋值
                        p = ((FS.HISFC.Models.RADT.PatientInfo)myPatients[i]).Clone();						//设置下一个
                        p.Diagnoses = new ArrayList(new string[1] { diagnose });

                        strItems = exeTempOrder.Order.Item.Name;
                        strDocName = exeTempOrder.Order.ReciptDoctor.Name;

                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.IsEmergency == true)
                        {
                            p.ExtendFlag1 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.IsEmergency.ToString();//加急标志
                        }
                        if (this.RePrint)
                        {
                            p.ExtendFlag2 = "True";//补打标志
                        }

                        p.User01 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).ID;
                        p.User02 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Sample.Name;         //样本类型
                        p.PVisit.User01 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ExeDept.Name; //执行科室
                        p.PVisit.User03 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToLongDateString();
                       
                        p.PVisit.Memo = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Qty.ToString(); //数量

                        strID = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).ID;

                        #endregion
                    }
                    else//相同的
                    {
                        strItems = strItems + "+" + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Item.Name;

                        p.PVisit.Memo = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Qty.ToString();//数量                          

                        strID = strID + "," + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).ID;
                    }
                }
                if (strItems != "")
                {
                    p.User01 = strID;
                    p.User03 = strItems;

                    p.PVisit.User02 = strDocName;

                    al.Add(p.Clone());//上次的检验单
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.AddControlData(al);
        }

        /// <summary>
        /// 数据打印
        /// </summary>
        /// <returns></returns>
        protected virtual int Print()
        {
            #region 管理变量

            ArrayList al = new ArrayList(); //数值数组
            FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();//管理类

            #endregion

            foreach (Control c in this.panelContainer.Controls)
            {
                #region 事务设置

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(orderManager.Connection);
                //t.BeginTransaction();
                //orderManager.SetTrans(t.Trans);
                //itemManager.SetTrans(t.Trans);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                #endregion

                if (((ucLisApplyControl)c).IsSelected)//选择打印的检验单
                {
                    #region 打印状态更新

                    al.Add(((ucLisApplyControl)c).ControlValue);

                    //添加控制打印条码	//置打印标记	//保存检验条码
                    FS.HISFC.Models.RADT.PatientInfo p = ((ucLisApplyControl)c).ControlValue as FS.HISFC.Models.RADT.PatientInfo;

                    List<FS.HISFC.Models.Order.ExecOrder> execList = null;
                    if (this.ILisDBInstance != null)
                    {
                        execList = new List<ExecOrder>();
                    }

                    try
                    {
                        string[] strExeOrderID = p.User01.Split(',');
                        for (int m = 0; m < strExeOrderID.Length; m++)
                        {
                            #region 如果ILisDB接口已实现 则获取医嘱执行档信息

                            if (this.ILisDBInstance != null)
                            {
                                FS.HISFC.Models.Order.ExecOrder exeOrder = orderManager.QueryExecOrderByExecOrderID(strExeOrderID[m], "2");
                                if (exeOrder == null)
                                {
                                    //t.RollBack();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("获得执行档信息出错!" + orderManager.Err);
                                    return -1;
                                }

                                execList.Add(exeOrder);
                            }

                            #endregion

                            #region 更新本地医嘱信息
                            if (orderManager.UpdateExecOrderLisBarCode(strExeOrderID[m], "") == -1)
                            {
                                //t.RollBack();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("无法更新条码！" + orderManager.Err);
                                return -1;
                            }
                            if (orderManager.UpdateExecOrderLisPrint(strExeOrderID[m]) == -1)//更新巡回卡打印标记
                            {
                                //t.RollBack();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("无法更新打印标记！" + orderManager.Err);
                                return -1;
                            }
                            #endregion
                        }

                        #region 如果ILisDB接口已实现 则传输Lis数据

                        if (this.ILisDBInstance != null)
                        {
                            string err = "";
                            if (this.ILisDBInstance.TransDataToLisDB(p, execList,ref err) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(p.Name + " 患者的Lis数据传输失败!  " + err);
                                return -1;
                            }
                        }

                        #endregion 
                    }
                    catch (Exception ex)
                    {
                        //t.RollBack();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("无法打印条码！" + ex.Message);
                        return -1;
                    }

                    #endregion
                }

                //t.Commit();
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            #region 打印检验申请单

            Panel panel = new Panel();
            panel.BackColor = Color.White;

            if (al.Count > 0) //打印
            {
                ArrayList alNew = new ArrayList();
                foreach (FS.HISFC.Models.RADT.PatientInfo pa in al)//查询数量，打印多张检验申请单
                {
                    string strLisID = "";
                    for (int i = 0; i < FS.FrameWork.Function.NConvert.ToInt32(pa.PVisit.Memo); i++)
                    {
                        if (strLisID == "")
                        {
                            strLisID = pa.User01;
                        }

                        pa.User01 = strLisID + "-" + (i + 1).ToString();

                        alNew.Add(pa.Clone());
                    }
                }

                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alNew, this.printControl, panel, new System.Drawing.Size(800, 353), 1);

                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                try
                {
                    Control c = panel;

                    p.SetPageSize(new System.Drawing.Printing.PaperSize("", 800, 1000));
                    //FS.UFC.Common.Classes.Function.GetPageSize("jyd", ref p);

                    p.IsPrintBackImage = false;
                    p.PrintPreview(8, 1, c);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.Query();

                return 0;
            }
            else //没选择不打印
            {
                return -1;
            }

            #endregion
        }

        /// <summary>
        /// 获取诊断
        /// </summary>
        protected string GetDiagnose(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
            {
                //从医生站录入的信息中查询
                //{014680EC-6381-408b-98FB-A549DAA49B82}
                //ArrayList diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                ArrayList diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.A);
                if (diagList != null && diagList.Count > 0)
                {
                    FS.HISFC.Models.HealthRecord.Diagnose obj = diagList[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                    return obj.DiagInfo.ICD10.Name;
                }
            }

            return "";
        }

        /// <summary>
        /// 数据赋值
        /// </summary>
        /// <param name="alValues">打印数据</param>
        protected void AddControlData(ArrayList alValues)
        {
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, this.displayControl, this.panelContainer, new System.Drawing.Size(800, 1200), 1);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;

            return null;
        }

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.Patients = alValues;

            return 1;
        }

        protected override int OnQuery(object sender, object NeuObject)
        {
            this.Query();

            return 1;
        }

        protected override int OnPrint(object sender, object NeuObject)
        {
            return this.Print();
        }

        private void ucLisApply_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.ILisDBInstance != null)
                {
                    if (this.ILisDBInstance.ConnectLisOnLoad() == -1)
                    {
                        MessageBox.Show("连接Lis数据库失败");
                    }
                }

                this.ILisDBInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(UFC.Lis.ILisDB)) as UFC.Lis.ILisDB;       

                DateTime dt = this.orderManager.GetDateTimeFromSysDateTime();
                DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                DateTime dt2 = new DateTime(dt.AddDays(1).Year, dt.AddDays(1).Month, dt.AddDays(1).Day, 12, 00, 00);
                this.dtpBegin.Value = dt1;
                this.dtpEnd.Value = dt2;
            }
            catch { }

            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(managerIntegrate.GetDepartment());
        }

        private void ckCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.panelContainer.Controls.Count <= 1)
                return;
            if (this.ckCheckAll.Checked == true)
            {
                for (int i = 0; i < this.panelContainer.Controls.Count; i++)
                    ((ucLisApplyControl)(this.panelContainer.Controls[i])).IsSelected = true;
            }
            else
            {
                for (int i = 0; i < this.panelContainer.Controls.Count; i++)
                    ((ucLisApplyControl)(this.panelContainer.Controls[i])).IsSelected = false;
            }
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(UFC.Lis.ILisDB);

                return printType;
            }
        }

        #endregion
    }
}
