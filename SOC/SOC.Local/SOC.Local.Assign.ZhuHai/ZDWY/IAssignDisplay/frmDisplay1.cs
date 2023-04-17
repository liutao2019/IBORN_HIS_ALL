using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;


namespace FS.SOC.Local.Assign.ZhuHai.ZDWY.IAssignDisplay
{
    /// <summary>
    ///  FS.FrameWork.WinForms.Controls.ucBaseControl 
    /// </summary>
    public partial class frmDisplay1 : Form, FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay
    {
        #region 变量

        private FS.HISFC.BizLogic.Nurse.Queue queMgr = new FS.HISFC.BizLogic.Nurse.Queue();
        private FS.HISFC.BizProcess.Integrate.Manager psMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Nurse.Assign assMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        private FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();
        //private FS.HISFC.Models.RADT.Person ps = new FS.HISFC.Models.RADT.Person();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration doctSchemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();

        private ArrayList alQueue = new ArrayList();
        private ArrayList alBook = new ArrayList();

        /// <summary>
        /// 总队列数
        /// </summary>
        private int queueNum = 0;

        /// <summary>
        /// 当前页码
        /// </summary>
        private int pageNum = 0;

        /// <summary>
        /// 设定分页时每页刷新的速度 毫秒单位 默认30秒
        /// </summary>
        public int PageRefreshTime
        {
            set
            {
                //如果传入的参数有误 则默认为30s刷新
                if (value <= 0)
                {
                    this.timer4.Interval = 10000;
                }
                else
                {
                    this.timer4.Interval = value;
                }
            }
        }

        /// <summary>
        /// 是否显示患者姓名
        /// </summary>
        private bool isDiaplayName = true;

        /// <summary>
        /// 医生姓名显示字体
        /// </summary>
        private float doctFontSize = 30;

        /// <summary>
        /// 患者姓名显示字体
        /// </summary>
        private float patientFontSize = 30;

        /// <summary>
        /// 候诊人数显示字体
        /// </summary>
        private float countFontSize = 30;


        /// <summary>
        /// 显示的患者数
        /// </summary>
        private int showCount = 3;


        /// <summary>
        /// 队列延长时间
        /// </summary>
        private double extendTime = -1;

        /// <summary>
        /// 播放广告的panel高度
        /// </summary>
        private int flashHeight = 70;


        /// <summary>
        /// 底部的广告
        /// </summary>
        private string flashTitle = "祝您健康！";


        /// <summary>
        /// 广告字体
        /// </summary>
        private float flashFontSize = 40;


        /// <summary>
        /// 叫号刷新时间
        /// </summary>
        int callRefreshTime = 1;

        /// <summary>
        /// 护士叫号接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Nurse.INurseAssign INurseAssign = null;

        #endregion

        public frmDisplay1()
        {
            InitializeComponent();
        }

        #region 事件


        /// <summary>
        /// 查询队列
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            //获取当前控件数量

            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = GetNoon(currenttime);//午别
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);
            int intTmp = this.alQueue.Count;
            if (intTmp <= 0)
            {
                this.pnlctrl1.Controls.Clear();
                this.pnlctrl2.Controls.Clear();
                this.pnlctrl3.Controls.Clear();
                this.pnlctrl4.Controls.Clear();
                this.pnlctrl5.Controls.Clear();


                //设置出现以外情况的处理(没有维护队列)-------------------------------------??????????
            }
            //控件数量跟原来相比较
            if (intTmp != queueNum)
            {   //赋值一个新的控件/队列数量
                this.queueNum = intTmp;
            }
        }


        /// <summary>
        /// 针对于一个护理站下有多于4个分诊队列时显示屏分页的情况 
        /// 新增全局变量：page当前显示页数  currentPageQueue当前页的分诊队列列表 
        /// 每次刷新按page从alQueue中取出当前页码应该显示的四个分诊队列，赋值到currentPageQueue列表中，然后显示到屏幕上
        /// 每次刷新 page+1 
        /// ygch {E7AD911A-5EFD-4999-8849-52BA9D61FAD7}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer4_Tick(object sender, EventArgs e)
        {
            //如果队列数为0则不再处理。
            if (this.queueNum == 0)
            {
                return;
            }
            //如果当前页页码大于最后一页，则转到第一页
            if ((decimal)this.pageNum >= (decimal)this.queueNum / 5)
            {
                this.pageNum = 0;
            }
            int index = this.pageNum * 5;
            //把当前页面的数据显示到显示屏幕
            FS.HISFC.Models.Nurse.Queue queue = null;
            ucQueForDisplay uc = null;
            #region 第一格显示
            if (index < this.alQueue.Count)
            {
                queue = this.alQueue[index] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            try
            {
                uc = this.pnlctrl1.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl1.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl1.Controls.Add(uc);
            }
            #endregion

            #region 第二格显示
            if (index + 1 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 1] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }
            uc = null;
            try
            {
                uc = this.pnlctrl2.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl2.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl2.Controls.Add(uc);
            }
            #endregion

            #region 第三格显示
            if (index + 2 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 2] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            uc = null;
            try
            {
                uc = this.pnlctrl3.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl3.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl3.Controls.Add(uc);
            }
            #endregion

            #region 第四格显示
            if (index + 3 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 3] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            uc = null;
            try
            {
                uc = this.pnlctrl4.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl4.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl4.Controls.Add(uc);
            }
            #endregion

            #region 第五格显示
            if (index + 4 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 4] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            uc = null;
            try
            {
                uc = this.pnlctrl5.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl5.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl5.Controls.Add(uc);
            }
            #endregion






            this.pageNum++;


        }


        /// <summary>
        /// 叫号定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCall_Tick(object sender, EventArgs e)
        {
            this.CalledPatient();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.FindForm().WindowState = FormWindowState.Maximized;
            base.OnLoad(e);
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {

            this.LoadMenuSet();


            FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            string screenSize = controlMgr.QueryControlerInfo("900004");
            this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 1);

            string screenSizeX = controlMgr.QueryControlerInfo("900008");
            string screenSizeY = controlMgr.QueryControlerInfo("900009");

            this.Size = new Size(FS.FrameWork.Function.NConvert.ToInt32(screenSizeX), FS.FrameWork.Function.NConvert.ToInt32(screenSizeY));

            ps = (FS.HISFC.Models.Base.Employee)this.queMgr.Operator;
            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = GetNoon(currenttime);//午别
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);

            this.queueNum = this.alQueue.Count;

            this.pnltop.Height = (this.Height - flashHeight);

            this.pnlTleft.Width = this.Width / 5;
            this.pnlTMiddle.Width = this.Width / 5;
            this.pnlTRight.Width = this.Width / 5;
            this.pnlTMiddle3.Width = this.Width / 5;
            this.pnlTMiddle2.Width = this.Width / 5;

            this.lblTitle.Text = this.flashTitle;
            this.pnlFlash.Height = this.flashHeight;
            this.lblTitle.Font = new System.Drawing.Font("宋体", flashFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            this.lblDelay.Font = new System.Drawing.Font("宋体", flashFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWait.Font = new System.Drawing.Font("宋体", flashFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.lblWait.Location = new Point(this.lblDelay.Location.X, this.lblDelay.Location.Y + this.lblDelay.Height + 3);
            //this.lblTitle.Location = new Point(this.lblDelay.Location.X, this.lblWait.Location.Y + this.lblWait.Height + 3);

            this.timer2.Enabled = true;//主界面没刷新，此处开启无用
            this.timer4.Enabled = true;
            this.timerCall.Enabled = true;
            this.timerCall.Interval = callRefreshTime * 1000;


            this.timer4_Tick(null, null);

        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {

            this.FindForm().WindowState = FormWindowState.Normal;
            this.FindForm().Close();
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.RButton | Keys.ShiftKey | Keys.Alt))
            {
                this.timer2.Enabled = false;//主界面没刷新，此处开启无用
                this.timer4.Enabled = false;
                this.timerCall.Enabled = false;
                this.FindForm().Dispose();
                this.FindForm().Close();

            }
            return base.ProcessDialogKey(keyData);
        }


        #endregion

        #region 函数



        /// <summary>
        /// 新建XML
        /// </summary>
        /// <returns></returns>
        public static int CreateXML(string fileName, string extendTime, string opertime)
        {
            string path;
            try
            {
                path = fileName.Substring(0, fileName.LastIndexOf(@"\"));
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch { }

            FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            root = myXml.CreateRootElement(doc, "Setting", "1.0");

            XmlElement e = myXml.AddXmlNode(doc, root, "延长队列", "");
            myXml.AddNodeAttibute(e, "ExtendTime", extendTime);

            e = myXml.AddXmlNode(doc, root, "保存时间", "");
            myXml.AddNodeAttibute(e, "LastOperTime", opertime);

            try
            {
                StreamWriter sr = new StreamWriter(fileName, false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);
                sr.Close();
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("无法保存！" + ex.Message); }

            return 1;
        }

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            FS.HISFC.BizProcess.Integrate.Registration.Registration schemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            ArrayList alNoon = schemaMgr.Query();
            if (alNoon == null) return "";
            /*
             * 实际午别为医生出诊时间段,上午可能为08~11:30，下午为14~17:30
             * 所以挂号员如果不在这个时间段挂号,就有可能提示午别未维护
             * 所以改为根据传人时间所在的午别例如：9：30在06~12之间，那么就判断是否有午别在
             * 06~12之间，全包含就说明9:30是那个午别代码
             */

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

            foreach (FS.HISFC.Models.Registration.Noon obj in alNoon)
            {
                if (int.Parse(obj.BeginTime.ToString("HHmmss")) >= begin &&
                    int.Parse(obj.EndTime.ToString("HHmmss")) <= end)
                {
                    return obj.ID;
                }
            }

            return "";
        }

        /// <summary>
        /// 补齐名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string PadName(string name)
        {
            //把名字补齐(原来6.5.4)
            int n = name.Length;
            string strname = "";
            if (n == 2)
            {
                strname = name.PadRight(6, ' ');
            }
            else if (n == 3)
            {
                strname = name.PadRight(5, ' ');
            }
            else if (n == 4)
            {
                strname = name.PadRight(4, ' ');
            }
            return strname;
        }


        /// <summary>
        /// 定义刷新叫号
        /// </summary>
        private void CalledPatient()
        {


            DateTime current = this.assMgr.GetDateTimeFromSysDateTime();


            #region 延迟患者
            ArrayList delayList = new ArrayList();
            ArrayList waitList = new ArrayList();
            //delayList = assMgr.Query(ps.Dept.ID, current.Date, "ALL", FS.HISFC.Models.Nurse.EnuTriageStatus.Delay);

            waitList = assMgr.Query(ps.Dept.ID, current.Date, "ALL", FS.HISFC.Models.Nurse.EnuTriageStatus.In);

            string delay = "过号患者：";
            for (int i = 0; i < delayList.Count; i++)
            {
                delay += (delayList[i] as FS.HISFC.Models.Nurse.Assign).Register.Name + " ";
            }
            lblDelay.Text = delay;

            string wait = string.Empty;

            if (waitList.Count > 0)
            {
                waitList.Sort(new AssignInDateCompare());
                wait = "";
            }
            else
            {
                wait = flashTitle;
            }

            for (int i = 0; i < 6; i++)
            {
                if (i < waitList.Count)
                {
                    if (i % 2 == 0 && i != 0)
                    {
                        wait += "\n";
                    }
                    string temps = "请" + (waitList[i] as FS.HISFC.Models.Nurse.Assign).Register.Name + "到" + (waitList[i] as FS.HISFC.Models.Nurse.Assign).Queue.SRoom.Name + "就诊";

                    wait += temps.PadRight(15, ' ');
                }
            }

            lblWait.Text = wait;

            #endregion

            /*
            if (lblWait.Location.X < 0 && (Math.Abs(lblWait.Location.X) - lblWait.Width / 2) > 0)//当label1右边缘与其容器的工作区左边缘之间的距离小于0时
            {
                lblWait.Location = new Point(lblWait.Width / 2, lblWait.Location.Y);//设置label1左边缘与其容器的工作区左边缘之间的距离为该窗体的宽度
            }
            else
            {
                lblWait.Left -= 10;//设置label1左边缘与其容器的工作区左边缘之间的距离
            }


            if (lblTitle.Location.X < 0 && (Math.Abs(lblTitle.Location.X) - lblTitle.Width / 2) > 0)//当label1右边缘与其容器的工作区左边缘之间的距离小于0时
            {
                lblTitle.Location = new Point(lblTitle.Width / 2, lblTitle.Location.Y);//设置label1左边缘与其容器的工作区左边缘之间的距离为该窗体的宽度
            }
            else
            {
                lblTitle.Left -= 10;//设置label1左边缘与其容器的工作区左边缘之间的距离
            }
             */



            try
            {
                string noonID = GetNoon(current);
                if (this.extendTime > 0)
                {
                    noonID = GetNoon(current.AddMinutes(-this.extendTime));
                }
                string nurseID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                FS.HISFC.BizProcess.Interface.Nurse.INurseAssign INurseAssign = null;
                INurseAssign = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.INurseAssign))
                    as FS.HISFC.BizProcess.Interface.Nurse.INurseAssign;
                if (INurseAssign != null)
                {
                    INurseAssign.Call(nurseID, noonID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 获取大屏幕参数
        /// </summary>
        private void LoadMenuSet()
        {
            try
            {

                #region 叫号设置

                if (!System.IO.File.Exists(Application.StartupPath + "/Setting/ExtendQueue.xml"))
                {
                    CreateXML(Application.StartupPath + "/Setting/ExtendQueue.xml", "60", this.assMgr.Operator.ID);
                }
                //是否延长队列时间 叫号的本地设置
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + "/Setting/ExtendQueue.xml");
                XmlNode node = doc.SelectSingleNode("//延长队列");
                if (node != null)
                {
                    this.extendTime = double.Parse(node.Attributes["ExtendTime"].Value);
                }
                node = doc.SelectSingleNode("//叫号刷新时间");
                if (node != null)
                {
                    this.callRefreshTime = int.Parse(node.Attributes["CallRefreshTime"].Value);
                }

                #endregion

                #region 显示设置

                string pathName = Application.StartupPath + "/Setting/NurseSetting.xml";
                if (!System.IO.File.Exists(pathName)) return;
                doc.Load(pathName);
                System.Xml.XmlNode xn = doc.SelectSingleNode("//是否隐藏姓名");
                if (xn != null)
                {
                    isDiaplayName = FS.FrameWork.Function.NConvert.ToBoolean(xn.Attributes[0].Value);
                }

                System.Xml.XmlNode xnDoct = doc.SelectSingleNode("//医生姓名字体");
                if (xnDoct != null)
                {
                    try
                    {
                        doctFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnDoct.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        doctFontSize = 30;
                    }

                }
                System.Xml.XmlNode xnPatient = doc.SelectSingleNode("//患者姓名字体");
                if (xnPatient != null)
                {
                    try
                    {
                        patientFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnPatient.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        patientFontSize = 30;
                    }

                }
                System.Xml.XmlNode xnCount = doc.SelectSingleNode("//候诊人数字体");
                if (xnCount != null)
                {
                    try
                    {
                        countFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnCount.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        countFontSize = 30;
                    }
                }

                System.Xml.XmlNode xnFlash = doc.SelectSingleNode("//广告设置");
                if (xnFlash != null)
                {
                    try
                    {
                        if (xnFlash.Attributes["Height"] != null)
                        {
                            flashHeight = int.Parse(xnFlash.Attributes["Height"].Value);
                        }

                        if (xnFlash.Attributes["Title"] != null)
                        {

                            flashTitle = xnFlash.Attributes["Title"].Value;
                        }

                        if (xnFlash.Attributes["Font"] != null)
                        {
                            flashFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnFlash.Attributes["Font"].Value);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                    }
                }

                System.Xml.XmlNode xnShow = doc.SelectSingleNode("//候诊显示人数");
                if (xnShow != null)
                {
                    try
                    {
                        if (xnShow.Attributes["Count"] != null)
                        {
                            showCount = int.Parse(xnShow.Attributes["Count"].Value);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                    }
                }


                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }


        public class AssignInDateCompare : IComparer
        {
            #region IComparer 成员

            public int Compare(object x, object y)
            {
                try
                {
                    FS.HISFC.Models.Nurse.Assign assignX = x as FS.HISFC.Models.Nurse.Assign;
                    FS.HISFC.Models.Nurse.Assign assignY = y as FS.HISFC.Models.Nurse.Assign;


                    if (assignX == null)
                    {
                        return (assignY != null) ? 1 : 0;
                    }
                    else if (assignY == null)
                    {
                        return -1;
                    }


                    if (assignX.InTime < assignY.InTime)
                    {
                        return 1;
                    }
                    else if (assignX.InTime > assignY.InTime)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                }

                catch
                {
                    return 0;
                }

                return 0;
            }

            #endregion
        }

        #endregion

        #region IAssignDisplay 成员

        /// <summary>
        /// 关闭屏幕
        /// </summary>
        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Close()
        {
            if (this != null && !this.IsDisposed)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// 打开屏幕
        /// </summary>
        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Show()
        {
            if (Screen.AllScreens.Length > 1)
            {
                this.Show();
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            else
            {
                this.Show();
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            if (this != null && !this.IsDisposed)
            {
                this.Close();
                this.Dispose();
            }
        }

        #endregion
    }
}