using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml;

namespace FS.HISFC.WinForms.WorkStation
{
    /// <summary>
    /// 护士医嘱管理
    /// </summary>
    public partial class frmNurseOrder : FS.FrameWork.WinForms.Forms.frmBaseForm
    {
        public frmNurseOrder()
        {
            InitializeComponent();
            base.tree = this.tvNurseCellPatientList1;
            base.initTree();
            this.tvNurseCellPatientList1.Refresh();

            alInpatientNO = GetChangedNO();

            if (!this.DesignMode)
            {
                timer1.Enabled = true;
                timer1.Interval = 500;
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Start();

                // 
                //{D0DECEC4-47EB-4039-B0D9-B2F20CEF8FBD}
                tbPackage = new System.Windows.Forms.ToolStripButton();
                tbPackage.ImageTransparentColor = System.Drawing.Color.Magenta;
                tbPackage.Name = "tbPackage";
                tbPackage.Text = "套餐查看";
                tbPackage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                tbPackage.ToolTipText = "套餐查看";
                tbPackage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T套餐);
                toolBar1.Items.Insert(toolBar1.Items.Count - 1, this.tbPackage);
                //--

                tbRefresh = new System.Windows.Forms.ToolStripButton();
                tbRefresh.TextImageRelation = TextImageRelation.ImageAboveText;
                tbRefresh.ToolTipText = "刷新患者";
                tbRefresh.Text = "刷新患者";
                tbRefresh.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S刷新);
                toolBar1.Items.Insert(toolBar1.Items.Count - 1, this.tbRefresh);
                this.tbRefresh.Visible = true;//先把它隐藏，需要时打开

                tbPrintLisBarcode = new System.Windows.Forms.ToolStripButton();
                tbPrintLisBarcode.TextImageRelation = TextImageRelation.ImageAboveText;
                tbPrintLisBarcode.ToolTipText = "检验条码";
                tbPrintLisBarcode.Text = "检验条码";
                tbPrintLisBarcode.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J健康档案);
                toolBar1.Items.Insert(toolBar1.Items.Count - 1, this.tbPrintLisBarcode);
                this.tbPrintLisBarcode.Visible = true;//先把它隐藏，需要时打开

                //{C826392C-D6A8-4ba5-95C9-7591460BE490}
                tbEMR = new System.Windows.Forms.ToolStripButton();
                tbEMR.TextImageRelation = TextImageRelation.ImageAboveText;
                tbEMR.ToolTipText = "病历";
                tbEMR.Text = "病历";
                tbEMR.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B病历);
                toolBar1.Items.Insert(toolBar1.Items.Count - 1, this.tbEMR);
                this.tbEMR.Visible = true;


                //{C826392C-D6A8-4ba5-95C9-7591460BE490}{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
                tbMSG = new System.Windows.Forms.ToolStripButton();
                tbMSG.TextImageRelation = TextImageRelation.ImageAboveText;
                tbMSG.ToolTipText = "消息发送";
                tbMSG.Text = "消息发送";
                tbMSG.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X信息);
                toolBar1.Items.Insert(toolBar1.Items.Count - 1, this.tbMSG);
                this.tbMSG.Visible = true;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.tbExport.Visible = false;//不显示导出按忸

            InitButton();

            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            bool isShowLisBarPrint = controlMgr.GetControlParam<bool>("HNZY28", true, false);
            this.tbPrintLisBarcode.Visible = isShowLisBarPrint;

            toolBar1.ItemClicked += new ToolStripItemClickedEventHandler(toolBar1_ItemClicked);

            this.iClinicPathPatient();
        }

        #region 变量

        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 记录当前时间
        /// </summary>
        DateTime currentTime = DateTime.Now;

        /// <summary>
        /// 未处理医嘱的患者列表
        /// </summary>
        ArrayList alInpatientNO = null;
        /// <summary>
        /// 未处理加急医嘱的患者列表
        /// </summary>
        ArrayList alEmcInpatientNO = null;

        /// <summary>
        /// 查找患者控件
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucQueryInpatient ucQueryInpatient = new FS.HISFC.Components.Common.Controls.ucQueryInpatient();

        /// <summary>
        /// 查看套餐按钮
        /// </summary>
        /// // {D0DECEC4-47EB-4039-B0D9-B2F20CEF8FBD}
        private System.Windows.Forms.ToolStripButton tbPackage = null;


        /// <summary>
        /// 刷新按钮
        /// </summary>
        private System.Windows.Forms.ToolStripButton tbRefresh = null;

        /// <summary>
        /// 打印LIS条码按钮
        /// </summary>
        private System.Windows.Forms.ToolStripButton tbPrintLisBarcode = null;

        /// <summary>
        /// 电子病历打开接口
        /// {C826392C-D6A8-4ba5-95C9-7591460BE490}
        /// </summary>
        private System.Windows.Forms.ToolStripButton tbEMR = null;


        /// <summary>
        /// 消息发送{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
        /// 
        /// </summary>
        private System.Windows.Forms.ToolStripButton tbMSG = null;

        /// <summary>
        /// 本地查询 {EBAD1BF3-9750-40bb-924B-BBEDB6713610} houwb 2014-5-29
        /// </summary>
        Classes.Function funMgr = new FS.HISFC.WinForms.WorkStation.Classes.Function();

        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;
        FS.HISFC.Models.RADT.PatientInfo patient = null;

        #endregion

        /// <summary>
        /// 路径患者，患者列表字体加粗
        /// </summary>
        private void iClinicPathPatient()
        {
            if (this.iClinicPath == null)
            {
                iClinicPath = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IClinicPath))
               as FS.HISFC.BizProcess.Interface.Common.IClinicPath;
            }
            foreach (TreeNode nodes in this.tvNurseCellPatientList1.Nodes)
            {
                foreach (TreeNode childNode in nodes.Nodes)
                    try
                    {
                        patient = childNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                        if (iClinicPath.PatientIsSelectedPath(patient.ID))
                        {
                            //childNode.NodeFont = new Font("宋体 ", 9, FontStyle.Bold);
                            childNode.Text = "★" + childNode.Text;
                        }
                    }
                    catch { }
            }
        }


        /// <summary>
        /// 初始化按钮
        /// </summary>
        private void InitButton()
        {

        }

        #region 事件
        private void toolBar1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tbRefresh)
            {
                this.tvNurseCellPatientList1.Refresh();
                this.iClinicPathPatient();
                if (CurrentControl != null)
                    CurrentControl.Refresh();
            }
            else if (e.ClickedItem == this.tbPrintLisBarcode)
            {
                //以后不要把代码写死，这个属于接口内容的！！！ 待处理

                //打印检验条码
                //try
                //{
                FS.HISFC.Models.Base.Employee employee = (FS.HISFC.Models.Base.Employee)this.orderMgr.Operator;
                //string strStartArgs = employee.Nurse.ID + " " + employee.Nurse.Name + " " + employee.Name + " " + employee.ID;
                string strStartArgs = " " + employee.ID;
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    strStartArgs = " admin";
                }

                System.Diagnostics.ProcessStartInfo pInfo = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + GetLISProcessPath(), strStartArgs);

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = pInfo;
                pInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start();
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "\r\n正在启动条码打印功能，请稍后！", ToolTipIcon.Info);

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("启动LIS程序出错 " + ex.Message);
                //}
            }
            //{C826392C-D6A8-4ba5-95C9-7591460BE490}
            else if (e.ClickedItem == this.tbEMR)
            {
                string opercode = FS.FrameWork.Management.Connection.Operator.ID.Substring(2, 4);
                string name = patient.Name;
                string sex = patient.Sex.Name;
                string birthday = patient.Birthday.ToShortDateString();
                string age = patient.Age;
                string hisinside = patient.ID;
                string hisoutside = patient.PID.PatientNO;
                string patientno = patient.PID.PatientNO;
                string department = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.PatientLocation.Dept.Name;

                string s = " http://192.168.34.7/ORCService";
                s += " " + opercode + " emr";
                s += " " + "HOSPITAL " + hisinside;
                s += " Name:" + name;
                s += "|Sex:" + sex;
                s += "|Birthday:" + birthday;
                s += "|Age:" + age;
                s += "|HIS内部标识:" + hisinside;
                s += "|HIS外部标识:" + hisoutside;
                s += "|住院号:" + patientno;
                s += "|Department:" + department;

                string loadPath = GetEMRProcessPath();

                if(string.IsNullOrEmpty(loadPath))
                {
                    MessageBox.Show("请维护电子病历的启动路径！");
                    return;
                }
                System.Diagnostics.ProcessStartInfo pInfo = new System.Diagnostics.ProcessStartInfo(loadPath,s);

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = pInfo;
                pInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start();
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "\r\n正在启动电子病历功能，请稍后！", ToolTipIcon.Info);
            }
            // {D0DECEC4-47EB-4039-B0D9-B2F20CEF8FBD}
            #region 套餐查看

            else if (e.ClickedItem == this.tbPackage)
            {
                FS.HISFC.Models.RADT.PatientInfo selectedPatient = tvNurseCellPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                if (selectedPatient == null || string.IsNullOrEmpty(selectedPatient.PID.CardNO))
                {
                    MessageBox.Show("请先检索患者！");
                    return;
                }

                string cardno = selectedPatient.PID.CardNO;
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(selectedPatient.PID.CardNO);
                frmpackage.ShowDialog();
            }
            else if (e.ClickedItem == this.tbMSG)
            {
                FS.HISFC.Models.RADT.PatientInfo selectedPatient = tvNurseCellPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (selectedPatient == null || string.IsNullOrEmpty(selectedPatient.PID.CardNO))
                {
                    MessageBox.Show("请先检索患者！");//{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
                    return;
                }
                FS.HISFC.Components.Order.Forms.frmAddMsgForm msg = new FS.HISFC.Components.Order.Forms.frmAddMsgForm();
                msg.patient = selectedPatient;
                msg.Init();
                msg.ShowDialog();
            }
            #endregion
        }

        private string GetLISProcessPath()
        {
            string strProcessPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\LisInterface.xml"))
            {
                XmlDocument file = new XmlDocument();
                file.Load(Application.StartupPath + "\\LisInterface.xml");
                XmlNode node = file.SelectSingleNode("Config/StartPath");
                if (node != null)
                {
                    strProcessPath += "/" + node.InnerText;
                }

                node = file.SelectSingleNode("Config/Process");
                if (node != null)
                {
                    strProcessPath += "/" + node.InnerText;
                }
            }

            if (string.IsNullOrEmpty(strProcessPath))
            {
                strProcessPath = "/LisInterface/smtags.exe";
            }
            return strProcessPath;
        }

        //{C826392C-D6A8-4ba5-95C9-7591460BE490}
        private string GetEMRProcessPath()
        {
            string strProcessPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrInterface.xml"))
            {
                XmlDocument file = new XmlDocument();
                file.Load(Application.StartupPath + "\\EmrInterface.xml");
                XmlNode node = file.SelectSingleNode("Config/Path");
                if (node != null)
                {
                    strProcessPath = node.InnerText;
                }
            }
            return strProcessPath;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //{242423A1-3275-4364-8F86-16DD2DCD5736} 防止新增的非基础按钮在切换tab后消失 yangw 20100910
            if (this.tabControl1.SelectedTab.Controls.Count > 0)
            {
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];

                InitButton();
            }
        }
        #endregion

        #region 将MQ嵌入系统
        //医生站保存时能自动通知护士站。护士站医嘱界面能自动响应，类似QQ的头像晃动，并可以给出声音提示

        /// <summary>
        /// 定时刷新查询住院号列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentTime < DateTime.Now.AddSeconds(-5))
            {
                currentTime = DateTime.Now;
                alInpatientNO = GetChangedNO();
                alEmcInpatientNO = GetEmcChangedNO();// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
            }

            if (alInpatientNO != null && alInpatientNO.Count > 0)
            {
                //this.tvNurseCellPatientList1.RefreshMQ(alInpatientNO);
                this.tvNurseCellPatientList1.RefreshImage(alInpatientNO);
            }
            if (alEmcInpatientNO != null && alEmcInpatientNO.Count >= 0)// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
            {
                this.tvNurseCellPatientList1.EmcRefresh(alEmcInpatientNO);
            }
        }

        /// <summary>
        /// 获得医嘱有变化的住院号列表
        /// </summary>
        /// <returns></returns>
        private ArrayList GetChangedNO()
        {
            //优化刷新显示医嘱变动的SQL，提取出SQLID {EBAD1BF3-9750-40bb-924B-BBEDB6713610} houwb 2014-5-29
            string nurseCellCode = ((FS.HISFC.Models.Base.Employee)orderMgr.Operator).Nurse.ID;
            string deptCode = ((FS.HISFC.Models.Base.Employee)orderMgr.Operator).Dept.ID;
            if (string.IsNullOrEmpty(nurseCellCode))
            {
                nurseCellCode = deptCode;
            }
            ArrayList al = funMgr.QueryChangedOrder(nurseCellCode, deptCode);
            if (al == null)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "警告", "查询患者变动医嘱出现错误！\r\n" + funMgr.Err, ToolTipIcon.Warning);
                return null;
            }

            return al;
        }
        /// <summary>
        /// 获得加急医嘱有变化的住院号列表// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        /// <returns></returns>
        private ArrayList GetEmcChangedNO()
        {
            //优化刷新显示医嘱变动的SQL，提取出SQLID {EBAD1BF3-9750-40bb-924B-BBEDB6713610} houwb 2014-5-29
            string nurseCellCode = ((FS.HISFC.Models.Base.Employee)orderMgr.Operator).Nurse.ID;
            string deptCode = ((FS.HISFC.Models.Base.Employee)orderMgr.Operator).Dept.ID;
            if (string.IsNullOrEmpty(nurseCellCode))
            {
                nurseCellCode = deptCode;
            }
            ArrayList al = funMgr.QueryEmcChangedOrder(nurseCellCode, deptCode);
            if (al == null)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "警告", "查询患者变动医嘱出现错误！\r\n" + funMgr.Err, ToolTipIcon.Warning);
                return null;
            }

            return al;
        }
        #endregion

        /// <summary>
        /// 查询选择住院号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode parent = null;

            if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucQueryInpatient) == DialogResult.OK)
            {
                TreeNode node = new TreeNode();

                node.Tag = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient;
                node.Text = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Name;

                foreach (TreeNode temp in this.tvNurseCellPatientList1.Nodes)
                {
                    if (temp.Text.IndexOf(FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.PVisit.PatientLocation.NurseCell.Name) >= 0)
                    {
                        parent = temp;
                        break;
                    }
                }

                if (parent == null)
                {
                    parent = new TreeNode();
                    parent.Tag = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.PVisit.PatientLocation.NurseCell;
                    parent.Text = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.PVisit.PatientLocation.NurseCell.Name;
                    if (this.tvNurseCellPatientList1.Nodes.Count > 0)
                    {
                        parent.ImageIndex = this.tvNurseCellPatientList1.Nodes[0].ImageIndex;
                    }
                    this.tvNurseCellPatientList1.Nodes.Add(parent);
                }

                parent.Nodes.Add(node);
                parent.ExpandAll();
            }
        }
    }
}