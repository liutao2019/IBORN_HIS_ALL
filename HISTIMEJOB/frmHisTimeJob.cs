using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using FS.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    public partial class frmHisTimeJob : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmHisTimeJob()
        {
            InitializeComponent();
        }

        #region "变量"

        /// <summary>
        /// job业务管理类

        /// </summary>
        FS.HISFC.BizLogic.Manager.Job managerJob;

        private System.IO.TextWriter output;

        /// <summary>
        /// 系统时间结构体,用于API方法更新本地时间.
        /// </summary>
        public struct SystemTime
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        #endregion

        #region "属性"


        #endregion

        #region "函数"

        /// <summary>
        /// 初始化

        /// </summary>
        /// <returns>1成功 －1失败</returns>
        private int init()
        {
            this.timerJob.Enabled = true;

            FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();

            empl.ID = "000000";
            empl.Name = "系统";
            FS.FrameWork.Management.Connection.Operator = empl;

            FS.FrameWork.Models.NeuObject hospital = new FS.FrameWork.Models.NeuObject();

            hospital.ID = "CORE_HIS50";


            FS.FrameWork.Management.Connection.Hospital = hospital;

            return 1;
        }

        /// <summary>
        /// 读取配置文件中的设置,将需要执行的和不需要执行的任务全部显示在窗口中
        /// </summary>
        private void LoadSetting()
        {
            DateTime dtNow = managerJob.GetDateTimeFromSysDateTime();

            //取job列表
            ArrayList al = this.managerJob.GetJobList("0");
            if (al == null)
            {
                this.fpJob_Sheet1.RowCount = 0;
                WriteLogo("取job列表时出错:" + this.managerJob.Err + dtNow.ToString());
                return;
            }

            this.fpJob_Sheet1.RowCount = al.Count;
            int row = 0;
            foreach (FS.HISFC.Models.Base.Job job in al)
            {
                this.fpJob_Sheet1.Cells[row, 0].Text = job.ID;			//代码
                this.fpJob_Sheet1.Cells[row, 1].Text = job.Name;		//名称
                this.fpJob_Sheet1.Cells[row, 2].Value = job.State.ID + "_" + job.State.Name;//状态

                this.fpJob_Sheet1.Cells[row, 3].Value = job.NextTime;	//下次执行时间
                this.fpJob_Sheet1.Cells[row, 4].Value = job.LastTime;	//上次执行时间
                this.fpJob_Sheet1.Cells[row, 5].Value = job.IntervalDays;//间隔
                this.fpJob_Sheet1.Cells[row, 6].Value = job.Type;		//类型
                this.fpJob_Sheet1.Cells[row, 7].Value = job.Memo;		//备注
                this.fpJob_Sheet1.Rows[row].Tag = job;					//job实体付给当前行

                row = row + 1;
            }
            return;
        }

        /// <summary>
        /// 设置本机时间
        /// </summary>
        /// <param name="lpSystemTime"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]

        public static extern int SetLocalTime(ref SystemTime lpSystemTime);

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="text">日志内容</param>
        private void WriteLogo(string text)
        {
            this.rtbLogo.AppendText("\n" + text);
            try
            {
                output = System.IO.File.AppendText(Application.StartupPath + "\\job.log");
                output.WriteLine("\n" + text);
                output.Close();
            }
            catch { }

        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private void SaveSetting()
        {
            //取需要保存的记录行数
            int rowCount = this.fpJob_Sheet1.RowCount;
            if (rowCount <= 0) return;

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.managerJob.Connection);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.Models.Base.Job job = new FS.HISFC.Models.Base.Job();
            this.managerJob.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //t.BeginTransaction();
            //this.managerJob.SetTrans(t.Trans);

            for (int i = 0; i < rowCount; i++)
            {
                job.ID = this.fpJob_Sheet1.Cells[i, 0].Text;				//0编码
                job.Name = this.fpJob_Sheet1.Cells[i, 1].Text;			//1名称
                job.State.ID = this.fpJob_Sheet1.Cells[i, 2].Text.Substring(0, 1);		//2状态

                job.NextTime = (DateTime)this.fpJob_Sheet1.Cells[i, 3].Value;		//3下次执行时间
                job.LastTime = (DateTime)this.fpJob_Sheet1.Cells[i, 4].Value;		//4上次执行时间
                job.IntervalDays = FS.FrameWork.Function.NConvert.ToInt32(this.fpJob_Sheet1.Cells[i, 5].Text);	//5间隔
                job.Type = this.fpJob_Sheet1.Cells[i, 6].Text.Substring(0, 1);		//6类型
                job.Memo = this.fpJob_Sheet1.Cells[i, 7].Text;			//7备注

                //如果有数据,则保存

                if (job.ID != "")
                {
                    //状态不能为空

                    if (job.State.ID.ToString() == "")
                    {
                        //t.RollBack();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("第" + (i + 1).ToString() + "行的状态不能为空!", "提示");
                        return;
                    }
                    if (this.managerJob.SetJob(job) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.managerJob.Err, "提示");
                        return;
                    }
                }
            }

            //提交数据库

            FS.FrameWork.Management.PublicTrans.Commit();

            DateTime dtNow = managerJob.GetDateTimeFromSysDateTime();
            this.WriteLogo("保存设置成功！" + dtNow.ToString());

            //刷新job列表
            this.LoadSetting();

        }

        /// <summary>
        /// 固定费用收取
        /// </summary>
        /// <param name="jobType">job类型</param>
        private int Job_FixFee()
        {
            try
            {
                //定义固定费用计算类并执行
                FixedFee fixedFee = new FixedFee();
                fixedFee.rtbLogo = this.rtbLogo;
                if (fixedFee.Start() == -1) return -1;

                return 1;
            }
            catch (Exception ex)
            {
                this.WriteLogo(ex.Message + ex.Source);
                return -1;
            }
        }

        /// <summary>
        /// OA数据抽取
        /// </summary>
        /// <param name="jobType">job类型</param>
        private int OA_FEE_LOAD()
        {
            try
            {
                //OA数据抽取
                OADeal.OAService oaService = new OADeal.OAService();
                //fixedFee.rtbLogo = this.rtbLogo;

                //由于道一云筛选条件，只能从开始单据提交时间开始查询，不能查询最终审核时间，所以每日遍历半个月的历史数据
                //由于时间跨度超过3天，查询可能超时，所以每次查询一天数据
                for (int i = 0; i <= 45; i++)
                {
                    string curdate = DateTime.Now.AddDays((-1) * i).Date.ToString("yyyy-MM-dd");
                    WriteLogo("当前统计日期：" + curdate);
                    oaService.GetOADataAndInsert(i,0);
                }
                //oaService.GetOADataAndInsert(8);

                //同样的东西获取第二次，但是审批流程的版本号往前一个
                OADeal.OAService oaServiceOld = new OADeal.OAService();
                //fixedFee.rtbLogo = this.rtbLogo;

                //由于道一云筛选条件，只能从开始单据提交时间开始查询，不能查询最终审核时间，所以每日遍历半个月的历史数据
                //由于时间跨度超过3天，查询可能超时，所以每次查询一天数据
                for (int i = 0; i <= 45; i++)
                {
                    string curdate = DateTime.Now.AddDays((-1) * i).Date.ToString("yyyy-MM-dd");
                    WriteLogo("前一个版本，当前统计日期：" + curdate);
                    oaServiceOld.GetOADataAndInsert(i, 1);
                }

            }
            catch (Exception ex)
            {
                this.WriteLogo(ex.Message + ex.Source);
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// 用友每日同步供应商
        /// </summary>
        /// <param name="jobType">job类型</param>
        private int SYN_YY_COMPANY()
        {
            try
            {
                OADAL.JobProcess p = new OADAL.JobProcess();
                p.SynchronizeYongYouCompany();
            }
            catch (Exception ex)
            {
                this.WriteLogo(ex.Message + ex.Source);
                return -1;
            }
            return 1;

        }
        
        /// <summary>
        /// 用友每日同步科目
        /// </summary>
        /// <param name="jobType">job类型</param>
        private int SYN_YY_SUBJECT()
        {
            /***
             * 关于院区
             * his系统内部存储的三个分别是IBORN,BELLAIRE,SDIBORN
             * 用友要以编码对应账套，目前001账套对应广州，002对应广州综合门诊，003对应顺德
             * 
             **/
            try
            {
                OADAL.JobProcess p = new OADAL.JobProcess();
                p.SynchronizeYongYouSubject();
            }
            catch (Exception ex)
            {
                this.WriteLogo(ex.Message + ex.Source);
                return -1;
            }
            return 1;

        }

        private bool JobStart(string dllName, string className)
        {
            try
            {
                if (string.IsNullOrEmpty(dllName) || string.IsNullOrEmpty(className))
                {
                    return false;
                }

                object obj = System.Reflection.Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\" + dllName).CreateInstance(className);
                if (obj is IJob)
                {
                    if (((IJob)obj).Start() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                this.WriteLogo(e.Message + e.Source);
                return false;
            }
        }

        ///// <summary>
        ///// 新农保本地预结算
        ///// </summary>
        ///// <returns></returns>
        //private int Job_XnbLocalBalance()
        //{
        //    try
        //    {
        //        XnhLocalBalance xnhLocal = new XnhLocalBalance();
        //        xnhLocal.rtbLogo = this.rtbLogo;
        //        if (xnhLocal.Start()==-1)
        //        {
        //            return -1;
        //        }
        //        return 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 新农保本地预结算
        ///// </summary>
        ///// <returns></returns>
        //private int Job_GZSILocalBalance()
        //{
        //    try
        //    {
        //        GZSILocalBalance xnhLocal = new GZSILocalBalance();
        //        xnhLocal.rtbLogo = this.rtbLogo;
        //        if (xnhLocal.Start() == -1)
        //        {
        //            return -1;
        //        }
        //        return 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 住院附材收取
        ///// </summary>
        ///// <param name="jobType">job类型</param>
        //private int Job_SubFee(DateTime feedate)
        //{
        //    try
        //    {
        //        //定义固定费用计算类并执行
        //        SubJob fixedFee = new SubJob();
        //        fixedFee.rtbLogo = this.rtbLogo;
        //        //当天收取前一天的附材费用
        //        fixedFee.FeeDate = feedate.AddDays(-1);
        //        if (fixedFee.Start() == -1)
        //        {
        //            this.WriteLogo(fixedFee.Message);
        //            return -1;
        //        }
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 医保费用上传
        ///// </summary>
        ///// <param name="jobType">job类型</param>
        //private int Job_FIN_SI(string arg)
        //{
        //    try
        //    {
        //        //定义固定费用计算类并执行
        //        FinSi finSi = new FinSi();
        //        finSi.rtbLogo = this.rtbLogo;
        //        finSi.JobArg = arg;
        //        if (finSi.Start() == -1) return -1;

        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 医保费用上传
        ///// </summary>
        ///// <returns></returns>
        //private int Job_SiUpLoad()
        //{
        //    int iRes = 1;
        //    try
        //    {
        //        SIPactUpdateLoad siUpLoad = new SIPactUpdateLoad();
        //        iRes = siUpLoad.Start();
        //    }
        //    catch (Exception objEx)
        //    {
        //        this.WriteLogo(objEx.Message + objEx.Source);
        //        iRes = -1;
        //    }

        //    return iRes;
        //}

        //private int Job_RuleFee(DateTime feeTime)
        //{
        //    try
        //    {
        //        FixRuleFee fixRuleFee = new FixRuleFee();
        //        DateTime tt = managerJob.GetDateTimeFromSysDateTime();
        //        DateTime t2 = tt;
        //        if (feeTime.Date >= tt.Date)
        //        {
        //            feeTime = new DateTime(t2.Year, t2.Month, t2.Day, 0, 0, 1);
        //        }

        //        fixRuleFee.FeeTime = feeTime;
        //        if (fixRuleFee.Start() == -1) return -1;
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        #region 暂时屏蔽  住院Lis试管费用收取
        ///// <summary>
        ///// 住院Lis试管费用收取
        ///// </summary>
        ///// <returns></returns>
        //private int Job_LisTubeFee()
        //{
        //    try
        //    {
        //        //定义固定费用计算类并执行
        //        LisTubeFee lisTubeFee = new LisTubeFee();
        //        lisTubeFee.rtbLogo = this.rtbLogo;
        //        if (lisTubeFee.Start() == -1)
        //        {
        //            this.WriteLogo(lisTubeFee.Message);
        //            return -1;
        //        }
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        #endregion

        #endregion

        #region "事件"

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 最小化图标显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveSetting();
        }

        /// <summary>
        /// 窗体初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHisTimeJob_Load(object sender, EventArgs e)
        {
            //初始化
            if (this.init() == -1)
                return;

            //不能在定义的时候实例化,因为那时还没有连接数据库
            this.managerJob = new FS.HISFC.BizLogic.Manager.Job();

            //读取配置,显示全部任务列表
            this.LoadSetting();

            //同步系统时间
            #region 同步系统时间
            try
            {
                FS.FrameWork.Management.DataBaseManger data = new FS.FrameWork.Management.DataBaseManger();
                System.DateTime ServerTime = data.GetDateTimeFromSysDateTime();
                SystemTime systNew = new SystemTime();
                systNew.wDay = (short)ServerTime.Day;
                systNew.wMonth = (short)ServerTime.Month;
                systNew.wYear = (short)ServerTime.Year;
                systNew.wHour = (short)ServerTime.Hour;
                systNew.wMinute = (short)ServerTime.Minute;
                systNew.wSecond = (short)(ServerTime.Second);
                SetLocalTime(ref systNew);
            }
            catch { }
            #endregion
        }

        /// <summary>
        /// 窗体Closed事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHisTimeJob_FormClosed(object sender, FormClosedEventArgs e)
        {
            //释放资源
            this.trayIcon.Dispose();
        }

        /// <summary>
        /// 窗体Closing事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHisTimeJob_FormClosing(object sender, FormClosingEventArgs e)
        {
            //窗口隐藏
            this.Hide();
            //不执行关闭
            //原来的这个会导致电脑无法关机
            //e.Cancel = true;
            //修改为其他情况不允许关闭，电脑关机时可以
            e.Cancel = e.CloseReason != System.Windows.Forms.CloseReason.WindowsShutDown;
        }

        private void timerJob_Tick(object sender, EventArgs e)
        {
            //if (System.IO.File.Exists("DebugSql.log"))
            //{
            //    System.IO.FileInfo fi = new System.IO.FileInfo("DebugSql.log");
            //    if (fi.CreationTime > System.dtNow.AddMonths(-1))
            //    {
            //        System.IO.File.Delete("DebugSql.log");
            //        System.IO.File.CreateText("DebugSql.log");
            //    }
            //}

            DateTime dtNow = managerJob.GetDateTimeFromSysDateTime();

            if (dtNow.Hour == 12)
            {
                if (System.IO.File.Exists(Application.StartupPath + @"\" + @"DebugSql.log"))
                {
                    System.IO.File.Delete(Application.StartupPath + @"\" + @"DebugSql.log");
                }
            }

            //显示系统当前时间
            this.lblCurrDateTime.Text = dtNow.ToString();

            //如果存在数据,则执行
            if (this.fpJob_Sheet1.RowCount == 0) return;

            //取当前时间
            DateTime currentTime = dtNow;
            string jobState = ""; //用来保存job原来的状态


            //对每条job记录进行判断是否需要执行
            FS.HISFC.Models.Base.Job job;
            for (int i = 0; i < this.fpJob_Sheet1.RowCount; i++)
            {
                //取列表中job实体
                job = this.fpJob_Sheet1.Rows[i].Tag as FS.HISFC.Models.Base.Job;
                if (job == null)
                {
                    WriteLogo("无法转换为job实体" + dtNow.ToString());
                    return;
                }

                //如果job的下次执行时间大于当前时间,或者job的状态是不需要执行N,或者正在执行中S,则不进行处理.
                if (job.State.ID.ToString() == "N" || job.State.ID.ToString() == "S")// || job.NextTime > currentTime
                {
                    continue;
                }

                //以下为job的执行处理: 添加job即在下面写代码
                //保存原状态
                jobState = job.State.ID.ToString();
                //修改job状态,表示正在执行.
                job.State.ID = "S";
                this.fpJob_Sheet1.Cells[i, 2].Text = job.State.ID + "_" + job.State.Name;

                //取数据库中的当前时间，用于判断数据库是否有效连接，如果当前已经失去连接则重新连接数据库。
                try
                {
                    this.managerJob.GetDateTimeFromSysDateTime();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                //执行前先判断数据库的状态
               FS.HISFC.Models.Base.Job currentJob= this.managerJob.GetJob(job.ID);

                //表示其他机器正在执行
               if (currentJob.State.ID.Equals("S"))
               {
                   continue;
               }
               else//更新为正在运行
               {
                   if (this.managerJob.SetJob(job) != 1)
                   {
                       WriteLogo("修改job信息时出错:" + this.managerJob.Err + dtNow.ToString("[hh:mm:ss]"));
                       return;
                   }
               }

                WriteLogo("[" + dtNow.ToString() + "]... ...开始：" + job.Name);
                Application.DoEvents();
                int parm = 1;
                if (this.JobStart(job.Implement.ID, job.Implement.Name) == false)
                {
                    switch (job.ID)
                    {
                        //case "ORDER_OrderExec":
                        //    //医嘱分解(使用主线程)
                        //    parm = this.Job_OrderExec();
                        //    break;
                        //case "FIN_AddDayLimit":
                        //    //增加公费患者日限额
                        //    parm = this.Job_AddDayLimit();
                        //    break;
                        ////修改集中发药标记(用主线程)
                        //case "PHA_SendDrugFlag":
                        //    #region 使用新的线程
                        //    //tsSendDrugFlag = new ThreadStart(SendDrugFlag);
                        //    //tdSendDrugFlag = new Thread(tsSendDrugFlag);
                        //    //this.tdSendDrugFlag.Start();
                        //    #endregion
                        //    //使用主线程

                        //    parm = this.Job_SendDrugFlag();
                        //    break;
                            
                        case "FIN_FixFee":
                            //固定费用收取
                            parm = this.Job_FixFee();
                            break;  
                        case "OA_FEE_LOAD":
                            //固定费用收取
                            parm = this.OA_FEE_LOAD();
                            break;
                        case "SYN_YY_COMPANY":
                            //固定费用收取
                            parm = this.SYN_YY_COMPANY();
                            break;
                        case "SYN_YY_SUBJECT":
                            //固定费用收取
                            parm = this.SYN_YY_SUBJECT();
                            break;
                        //case "XNH_LocalBalance":
                        //    //新农合本地预结算
                        //    parm = this.Job_XnbLocalBalance();
                        //    break;
                        //case "GZSI_LocalBalance":
                        //    //广州医保本地预结算
                        //    parm = this.Job_GZSILocalBalance();
                        //    break;

                        //case "FIN_RuleFee":
                        //    //按规则收费
                        //    parm = Job_RuleFee(job.NextTime);
                        //    break;
                        ////住院附材收取
                        //case "Sub_Fee":
                        //    //附材收费
                        //    parm = this.Job_SubFee(job.NextTime);
                        //    break;

                        //case "SI_UPLOAD":
                        //    if (!string.IsNullOrEmpty(job.Memo))
                        //    {
                        //        FS.FrameWork.Models.NeuObject objHos = new FS.FrameWork.Models.NeuObject();
                        //        objHos.ID = job.Memo;
                        //        FS.FrameWork.Management.Connection.Hospital = objHos;
                        //    }

                        //    parm = this.Job_SiUpLoad();
                        //    break;
                        //case "LisTube_Fee":
                        //    //住院Lis试管收费
                        //    parm = this.Job_LisTubeFee();
                        //    break;
                        //case "COM_DataStat":
                        //    //使用主线程

                        //    parm = this.Job_DataStat();
                        //    break;
                        //case "Board_Fee":
                        //    //使用主线程--Add By Maokb
                        //    parm = this.Job_BoardFee();
                        //    break;
                        //case "Fin_AdjustOverTop":
                        //    parm = this.Job_AdjustOverTop();
                        //    break;
                        //case "ADJUSTBEDFEE":
                        //    parm = this.AdjustGFBedFee();//收取固定费用时调整
                        //    break;
                        //case "FIN_SIUploadFee":
                        //    {
                        //        parm = this.Job_FIN_SI(job.Memo);
                        //        break;
                        //    }
                        //case "Fin_SiDayBalance":
                        //    //Add by Maokb
                        //    parm = this.Job_SiDayBalance();
                        //    break;

                    }
                }

                //如果程序出错,不更新
                if (parm == -1) return;

                //job执行后的处理
                //恢复job状态

                job.State.ID = jobState;

                //根据job的状态和间隔,修改job的上次执行时间和下次执行时间.
                if (job.IntervalDays <= 0) job.IntervalDays = 1;	//默认间隔为1


                //本次执行时间是上次执行时间
                //job.LastTime = currentTime;	//修改本次执行时间

                //修改下次执行时间
                switch (job.State.ID.ToString())
                {
                    case "D":
                        if (job.NextTime.Date == currentTime.Date)
                        {
                            job.LastTime = job.NextTime;
                            job.NextTime = job.LastTime.AddDays(1);
                        }
                        else
                        {
                            job.LastTime = FS.FrameWork.Function.NConvert.ToDateTime(currentTime.AddDays(-1).ToShortDateString() + " " + job.NextTime.ToLongTimeString());
                            job.NextTime = job.LastTime.AddDays(1);
                        }
                        break;
                    case "M":
                        job.NextTime = currentTime.AddMonths(job.IntervalDays);
                        break;
                    case "Y":
                        job.NextTime = currentTime.AddYears(job.IntervalDays);
                        break;
                }

                //直接修改job信息,不需要提交

                if (this.managerJob.SetJob(job) != 1)
                {
                    WriteLogo("修改job信息时出错:" + this.managerJob.Err + dtNow.ToString("[hh:mm:ss]"));
                    return;
                }

                this.fpJob_Sheet1.Cells[i, 2].Text = job.State.ID + "_" + job.State.Name;	//Job状态

                this.fpJob_Sheet1.Cells[i, 3].Value = job.NextTime;	//下次执行时间
                this.fpJob_Sheet1.Cells[i, 4].Value = job.LastTime;	//上次执行时间

                WriteLogo("[" + dtNow.ToString() + "]... ...结束：" + job.Name);
            }
        }

        #endregion

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Job_SiUpLoad();
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            Job_FixFee();
        }

           /// <summary>
        /// 增加公费患者药品日限额
        /// </summary>
        /// <param name="jobType">job类型</param>
        //private int Job_AddDayLimit()
        //{
        //    try
        //    {
        //        //定义费用管理类,并执行	
        //        FS.HISFC.BizLogic.Fee.Item job = new FS.HISFC.BizLogic.Fee.Item();
        //        //公费日限额处理
        //        if (job.ExecProcAddDayLimit() == -1)
        //        {
        //            WriteLogo(job.Err + dtNow.ToString());
        //            return -1;
        //        }

        //        //非主线程的方法在此处调用,更新job列表中的状态和执行时间
        //        //this.UpdateRow("FIN_AddDayLimit");
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        /// <summary>
        /// 超标药调整
        /// </summary>
        /// <returns></returns>
        //private int Job_AdjustOverTop()
        //{
        //    try
        //    {
        //        //日限额调整类
        //        AdjustOverTop job = new AdjustOverTop();
        //        //日限额调整
        //        if (job.Start() == -1)
        //        {
        //            this.WriteLogo(job.Message);
        //            return -1;
        //        }
        //        if (job.Message != "")
        //        {
        //            this.WriteLogo(job.Message);
        //        }
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteLogo(ex.Message + ex.Source);
        //        return -1;
        //    }
        //}

        /// <summary>
        /// 调整公费患者床位限额
        /// </summary>
        /// <returns></returns>
        //private int AdjustGFBedFee()
        //{
        //    try
        //    {
        //        //获取公费患者列表
        //        //只获取在院患者
        //        //获取公费患者列表
        //        FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        //        FS.HISFC.BizLogic.RADT.InPatient patientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        //        ArrayList alPatient = null;
        //        //alPatient = this.queryPatientByPakind("03");
        //        string begin = DateTime.MinValue.ToString();
        //        string end = patientMgr.GetSysDateTime();
        //        alPatient = patientMgr.PatientQuery(begin, end, "I", "03");
        //        if (alPatient == null)
        //        {
        //            this.WriteLogo(patientMgr.Err);
        //            return -1;
        //        }
        //        if (alPatient == null)
        //        {
        //            this.WriteLogo(patientMgr.Err);
        //            return -1;
        //        }
        //        foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in alPatient)
        //        {
        //            //按照人逐个提交
        //            FS.FrameWork.Management.PublicTrans.BeginTransaction();
        //            feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
        //            if (feeMgr.AdjustOverLimitBed(pInfo, false) == -1)
        //            {
        //                FS.FrameWork.Management.PublicTrans.RollBack();
        //                this.WriteLogo(feeMgr.Err);
        //                continue;
        //            }
        //            FS.FrameWork.Management.PublicTrans.Commit();
        //        }
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (FS.FrameWork.Management.PublicTrans.Trans != null)
        //        {
        //            FS.FrameWork.Management.PublicTrans.RollBack();
        //        }
        //        this.WriteLogo(ex.Message);
        //        return -1;
        //    }
        //}

    }
}