using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Net;

namespace SOC.Local.Assign.ShenZhen.BinHai.IAssignDisplay
{
    public partial class frmDefualtDisplay : Form, FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay
    {
        public frmDefualtDisplay()
        {
            InitializeComponent();
        }

        #region 变量
        private FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();
        private FS.SOC.HISFC.Assign.BizProcess.Call callMgr = new FS.SOC.HISFC.Assign.BizProcess.Call();
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
                    this.timer4.Interval = 2000;
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
        private float patientFontSize = 42;

        /// <summary>
        /// 候诊人数显示字体
        /// </summary>
        private float countFontSize = 30;

        /// <summary>
        /// 显示屏
        /// </summary>
        private ucQueueForDisplay uc = null;
        #endregion

        /// <summary>
        /// 队列
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            //获取当前控件数量

            DateTime currenttime = CommonController.CreateInstance().GetSystemTime();
            DateTime current = currenttime.Date;
            label2.Text = currenttime.ToString();
            string noonID = CommonController.CreateInstance().GetNoonID(currenttime);//午别
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            this.alQueue = queueMgr.QueryValidByNurseID(ps.Nurse.ID, current, noonID);
            int intTmp = this.alQueue.Count;
            if (intTmp <= 0)
            {
                this.pnlctrl1.Controls.Clear();
                //this.pnlctrl2.Controls.Clear();
                //this.pnlctrl3.Controls.Clear();
                //this.pnlctrl4.Controls.Clear();
                //设置出现以外情况的处理(没有维护队列)-------------------------------------??????????
            }
            //控件数量跟原来相比较
            if (intTmp != queueNum)
            {   //赋值一个新的控件/队列数量
                this.queueNum = intTmp;
            }

            #region 显示修改
            FS.SOC.HISFC.Assign.Models.Queue queue = null;

            if (alQueue.Count > 0)
            {
                queue = this.alQueue[0] as FS.SOC.HISFC.Assign.Models.Queue;
                uc.Queue = queue;
                this.pnlctrl1.Controls.Add(uc);
            }
            #endregion

            //开始叫号
            string errInfo = "";
            callMgr.CallQueue(ps.Nurse.ID, noonID, ref errInfo);
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            this.label1.Text = (new FS.HISFC.BizLogic.Manager.Constant()).GetHospitalName();
            FS.FrameWork.Management.ControlParam controlMgr = new FS.FrameWork.Management.ControlParam();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            string screenSize = controlMgr.QueryControlerInfo("900004");
            this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 1);

            string screenSizeX = controlMgr.QueryControlerInfo("900008");
            string screenSizeY = controlMgr.QueryControlerInfo("900009");

            this.Size = new Size(FS.FrameWork.Function.NConvert.ToInt32(screenSizeX), FS.FrameWork.Function.NConvert.ToInt32(screenSizeY));

            ps = (FS.HISFC.Models.Base.Employee)controlMgr.Operator;
            DateTime currenttime = CommonController.CreateInstance().GetSystemTime();
            DateTime current = currenttime.Date;
            string noonID = CommonController.CreateInstance().GetNoonID(currenttime);//午别
            this.alQueue = queueMgr.QueryValidByNurseID(ps.Nurse.ID, current, noonID);

            this.queueNum = this.alQueue.Count;
            label2.Text = currenttime.ToString();
            this.pnltop.Height = this.Height / 9;
            //this.pnlTleft.Width = this.Width / 2;
            //this.pnlBleft.Width = this.Width / 2;

            this.timer2.Enabled = true;//主界面没刷新，此处开启无用
            this.timer4.Enabled = true;

            //ArrayList alFzDept = (new FS.HISFC.BizProcess.Integrate.Manager()).QueryFZDept();
            //foreach(FS.HISFC.Models.Base.DepartmentStat objDept in alFzDept)
            //{
            //    if (objDept.PardepCode == ps.Nurse.ID)
            //    {
            //        deptId += objDept.DeptCode + "|";
            //    }
            //}

            //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //string pathName = Application.StartupPath + "/Setting/NurseSetting.xml";
            //if (!System.IO.File.Exists(pathName)) return;
            //doc.Load(pathName);
            //System.Xml.XmlNode xn = doc.SelectSingleNode("//是否隐藏姓名");
            //if (xn != null)
            //{
            //    isDiaplayName = FS.FrameWork.Function.NConvert.ToBoolean(xn.Attributes[0].Value);
            //}

            //System.Xml.XmlNode xnDoct = doc.SelectSingleNode("//医生姓名字体");
            //if (xnDoct != null)
            //{
            //    try
            //    {
            //        doctFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnDoct.Attributes[0].Value);
            //    }
            //    catch (Exception)
            //    {
            //        doctFontSize = 30;
            //    }

            //}
            //System.Xml.XmlNode xnPatient = doc.SelectSingleNode("//患者姓名字体");
            //if (xnPatient != null)
            //{
            //    try
            //    {
            //        patientFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnPatient.Attributes[0].Value);
            //    }
            //    catch (Exception)
            //    {
            //        patientFontSize = 30;
            //    }

            //}
            //System.Xml.XmlNode xnCount = doc.SelectSingleNode("//候诊人数字体");
            //if (xnCount != null)
            //{
            //    try
            //    {
            //        countFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnCount.Attributes[0].Value);
            //    }
            //    catch (Exception)
            //    {
            //        countFontSize = 30;
            //    }
            //}

            #region 显示修改
            uc = new ucQueueForDisplay(false);
            uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            uc.Dock = DockStyle.Fill;
            this.pnlctrl1.Controls.Clear();

            //this.timer4_Tick(null, null);
            #endregion

        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
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
            ////如果队列数为0则不再处理。
            //if (this.queueNum == 0)
            //{
            //    return;
            //}
            ////如果当前页页码大于最后一页，则转到第一页
            ////if ((decimal)this.pageNum >= (decimal)this.queueNum / 4)
            ////{
            ////    this.pageNum = 0;
            ////}
            ////int index = this.pageNum * 4;
            ////把当前页面的数据显示到显示屏幕
            //FS.SOC.HISFC.Assign.Models.Queue queue = null;
            //ucQueueForDisplay uc = null;
            //#region 第一格显示
            ////if (index < this.alQueue.Count)
            ////{
            //queue = this.alQueue[0] as FS.SOC.HISFC.Assign.Models.Queue;
            ////}
            ////else
            ////{
            ////    queue = new FS.SOC.HISFC.Assign.Models.Queue();
            ////}

            //try
            //{
            //    uc = this.pnlctrl1.Controls[0] as ucQueueForDisplay;
            //    uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    uc.Queue = queue;
            //}
            //catch
            //{
            //}
            //if (uc == null)
            //{
            //    this.pnlctrl1.Controls.Clear();
            //    uc = new ucQueueForDisplay(isDiaplayName);
            //    uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    uc.Queue = queue;
            //    uc.Dock = DockStyle.Fill;
            //    this.pnlctrl1.Controls.Add(uc);
            //}
            //#endregion

            //#region 第二格显示
            //////if (index + 1 < this.alQueue.Count)
            //////{
            //////    queue = this.alQueue[index + 1] as FS.SOC.HISFC.Assign.Models.Queue;
            //////}
            //////else
            //////{
            //////    queue = new FS.SOC.HISFC.Assign.Models.Queue();
            //////}
            ////uc = null;
            //try
            //{
            //    //uc = this.pnlctrl2.Controls[0] as ucQueueForDisplay;
            //    //uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    //uc.Queue = queue;
            //}
            //catch
            //{
            //}
            ////if (uc == null)
            //{
            //    //this.pnlctrl2.Controls.Clear();
            //    //uc = new ucQueueForDisplay(isDiaplayName);
            //    //uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    //uc.Queue = queue;
            //    //uc.Dock = DockStyle.Fill;
            //    //this.pnlctrl2.Controls.Add(uc);
            //}
            //#endregion

            //#region 第三格显示
            ////if (index + 2 < this.alQueue.Count)
            ////{
            ////    queue = this.alQueue[index + 2] as FS.SOC.HISFC.Assign.Models.Queue;
            ////}
            ////else
            ////{
            ////    queue = new FS.SOC.HISFC.Assign.Models.Queue();
            ////}

            ////uc = null;
            //try
            //{
            //    //uc = this.pnlctrl3.Controls[0] as ucQueueForDisplay;
            //    //uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    //uc.Queue = queue;
            //}
            //catch
            //{
            //}
            ////if (uc == null)
            //{
            //    //this.pnlctrl3.Controls.Clear();
            //    //uc = new ucQueueForDisplay(isDiaplayName);
            //    //uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    //uc.Queue = queue;
            //    //uc.Dock = DockStyle.Fill;
            //    //this.pnlctrl3.Controls.Add(uc);
            //}
            //#endregion

            //#region 第四格显示
            ////if (index + 3 < this.alQueue.Count)
            ////{
            ////    queue = this.alQueue[index + 3] as FS.SOC.HISFC.Assign.Models.Queue;
            ////}
            ////else
            ////{
            ////    queue = new FS.SOC.HISFC.Assign.Models.Queue();
            ////}

            ////uc = null;
            //try
            //{
            //    //uc = this.pnlctrl4.Controls[0] as ucQueueForDisplay;
            //    //uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    //uc.Queue = queue;
            //}
            //catch
            //{
            //}
            ////if (uc == null)
            //{
            //    //this.pnlctrl4.Controls.Clear();
            //    //uc = new ucQueueForDisplay(isDiaplayName);
            //    //uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
            //    //uc.Queue = queue;
            //    //uc.Dock = DockStyle.Fill;
            //    //this.pnlctrl4.Controls.Add(uc);
            //}
            //#endregion


            //this.pageNum++;


        }

        #region IAssignDisplay 成员

        //frmDefualtDisplay frm = null;
        /// <summary>
        /// 关闭屏幕
        /// </summary>
        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Close()
        {
            if (this != null && !this.IsDisposed)
            {
                //frm.Close();
                //this.Close();
                this.Hide();
            }
        }

        /// <summary>
        /// 打开屏幕
        /// </summary>
        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Show()
        {
            //frm = new frmDefualtDisplay();
            string hosName = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(hosName);
            string ipv4 = String.Empty;
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }
            //string ip = System.Net.Dns.GetHostEntry(hosName).AddressList[0].ToString();
            FS.HISFC.Models.Base.Department deptTemp = (new FS.HISFC.BizLogic.Manager.Department()).GetDeptmentById(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (ipv4 != deptTemp.EnglishName)
            {
                MessageBox.Show("“" + deptTemp.Name + "”" + "IP地址<" + deptTemp.EnglishName + "> 与本地IP地址<" + ipv4 + ">不一致，不能打开，请联系管理员设置“" + deptTemp.Name + "”IP地址。");
                return;
            }

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
            }
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            if (this != null && !this.IsDisposed)
            {
                //frm.Close();
                //frm.Dispose();
                this.Close();
                this.Dispose();
            }
        }

        #endregion
    }
}
