using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinIpb
{
    /// <summary>
    /// 【功能说明：】
    /// 【创建人：】
    /// 【创建时间：】
    /// 【修改记录】
    ///   2009-8-24 xuc 增加可以选择多科室多患者查询功能 {EA2A7657-6A55-4582-8052-DC7F8A5A4795}
    /// 
    /// 
    /// </summary>
    public partial class ucFinIpbPatientDayFeeGZ : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbPatientDayFeeGZ()
        {
            InitializeComponent();
        }


        #region 变量
        /// <summary>
        /// 业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
        sql sqlMgr = new sql();

        /// <summary>
        /// 是否显示全院患者
        /// </summary>
        bool isShowAllInDeptPatient = false;
        /// <summary>
        /// 是否显示全院患者
        /// </summary>
        public bool IsShowAllInDeptPatient
        {
            get
            {
                return isShowAllInDeptPatient;
            }
            set
            {
                isShowAllInDeptPatient = value;
            }
        }

        #endregion


        private Size printSize = new Size(850, 1169);
        [Category("控件设置"), Description("设置打印纸张的高度")]
        public int PrintHeight
        {
            get
            {
                return printSize.Height;
            }
            set
            {
                printSize.Height = value;
            }

        }
        [Category("控件设置"), Description("设置打印纸张的宽度")]
        public int PrintWidth
        {
            get
            {
                return printSize.Width;
            }
            set
            {
                printSize.Width = value;
            }

        }



        /// <summary>
        ///初始化树
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            base.OnDrawTree();

            this.tvLeft.Nodes.Clear();

            //左侧多选
            this.tvLeft.CheckBoxes = true;

            if (isShowAllInDeptPatient == false)
            {
                //在院患者
                FS.HISFC.Models.RADT.InStateEnumService inState = new FS.HISFC.Models.RADT.InStateEnumService();
                inState.ID = FS.HISFC.Models.Base.EnumInState.I.ToString();

                //ArrayList emplList = managerIntegrate.QueryPatientBasic(base.employee.Dept.ID, inState);

                ArrayList emplList = managerIntegrate.QueryPatientBasicByNurseCell(base.employee.Nurse.ID, inState);

                TreeNode parentTreeNode = new TreeNode("本区患者");
                parentTreeNode.Checked = false;
                parentTreeNode.Tag = "ROOT";
                tvLeft.Nodes.Add(parentTreeNode);
                foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
                {
                    if (string.IsNullOrEmpty(empl.PVisit.PatientLocation.Bed.ID))
                    {
                        continue;
                    }
                    TreeNode emplNode = new TreeNode();
                    emplNode.Tag = empl;
                    emplNode.Text ="【"+empl.PVisit.PatientLocation.Bed.ID.Substring(4)+"】"+ empl.Name;
                    parentTreeNode.Nodes.Add(emplNode);
                }

                parentTreeNode.ExpandAll();
                parentTreeNode.Checked = false;
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载全院患者列表，请稍等......");
                Application.DoEvents();


                //全院患者列表
                //在院患者
                ArrayList emplList = managerIntegrate.QueryPatient(FS.HISFC.Models.Base.EnumInState.I);

                //构建树列表
                Dictionary<string, TreeNode> deptDic = new Dictionary<string, TreeNode>();

                TreeNode parentTreeNode = new TreeNode("全院患者");
                
                parentTreeNode.Tag = "ROOT";
                tvLeft.Nodes.Add(parentTreeNode);
                int index = 0;
                foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
                {
                    if (deptDic.ContainsKey(empl.PVisit.PatientLocation.Dept.ID))
                    {
                        TreeNode patient = new TreeNode();
                        patient.Tag = empl;
                        patient.Text = empl.Name + "【" + empl.PID.PatientNO.ToString() + "】";

                        patient.Checked = false;
                        deptDic[empl.PVisit.PatientLocation.Dept.ID].Nodes.Add(patient);
                    }
                    else
                    {
                        TreeNode dept = new TreeNode();
                        dept.ForeColor = Color.Blue;
                        dept.Tag = empl.PVisit.PatientLocation.Dept;
                        dept.Text = empl.PVisit.PatientLocation.Dept.Name + "【" + empl.PVisit.PatientLocation.Dept.ID.ToString() + "】";
                        
                        TreeNode patient = new TreeNode();
                        patient.Tag = empl;
                        patient.Text = empl.Name + "【" + empl.PID.PatientNO.ToString()+"】";
                        patient.Checked = false;
                        dept.Nodes.Add(patient);
                        deptDic.Add(empl.PVisit.PatientLocation.Dept.ID, dept);

                        dept.Checked = false;
                        parentTreeNode.Nodes.Add(dept);
                    }
                    index++;
                }
                parentTreeNode.ExpandAll();
                parentTreeNode.Checked = false;


                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            

            

            this.tvLeft.AfterSelect -= new TreeViewEventHandler(tvLeft_AfterSelect);
            this.tvLeft.AfterSelect += new TreeViewEventHandler(tvLeft_AfterSelect);
            this.tvLeft.AfterCheck -= new TreeViewEventHandler(tvLeft_AfterCheck);
            this.tvLeft.AfterCheck += new TreeViewEventHandler(tvLeft_AfterCheck);

            return 1;
        }
        /// <summary>
        /// 选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                e.Node.Checked = !e.Node.Checked;
            }
        }
        /// <summary>
        /// 勾选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvLeft_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                bool isCheck = e.Node.Checked;
                this.SelectPatient(e.Node, isCheck);
            }
        }



        /// <summary>
        /// 勾选患者
        /// </summary>
        /// <param name="treeNode"></param>
        private void SelectPatient(TreeNode treeNode, bool isCheck)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = isCheck;
                SelectPatient(node, isCheck);
            }
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            //if (base.GetQueryTime() == -1)
            //{
            //    return -1;
            //}

            List<string> inpatientLine = new List<string>();
            GetPatients(this.tvLeft.Nodes[0], inpatientLine);
            if (inpatientLine.Count <= 0)
            {
                MessageBox.Show("请选择患者");
                return -1;
            }
            string inpatient = string.Empty;
            int intI = 0;
            foreach (string item in inpatientLine)
            {

                if (intI == 0)
                {
                    inpatient = item;
                    intI = 1;
                }
                else
                {
                    inpatient += "','" + item;
                }
            }

            //string[] inpatient = inpatientLine.ToArray();

            // {B8CDFC3D-BE39-4a34-A546-DE9EB91676A5}
            //return base.OnRetrieve(inpatient, this.beginTime, this.endTime, "ALL");

            // {B8CDFC3D-BE39-4a34-A546-DE9EB91676A5}

            if (dtpBeginTime.Value.Date >= managerIntegrate.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show("不能查询当前日期或未来日期费用，抱歉！");
                return -1;
            }
            DateTime dtBeginTime = dtpBeginTime.Value.Date;
            DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);

            dwMain.Modify("t_title.text= '" + this.managerIntegrate.Hospital.Name + "患者一日清单（明细）'");
            dwMain.Modify("t_27.text='费用日期：" +dtBeginTime.ToString("yyyy-MM-dd") + "'");
            return base.OnRetrieve(inpatient, "ALL", dtBeginTime, dtEndTime);
            return 1;
        }

        /// <summary>
        /// 递归获取选择的患者
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="inpatientLine"></param>
        void GetPatients(TreeNode nodes, List<string> inpatientLine)
        {
            DateTime dtEnd = managerIntegrate.GetDateTimeFromSysDateTime();
            foreach (TreeNode node in nodes.Nodes)
            {
                if (node.Checked && node.Tag is FS.HISFC.Models.RADT.PatientInfo)
                {
                    FS.HISFC.Models.RADT.PatientInfo patient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                    //判断该患者是否本地预约结算
                    FS.FrameWork.Models.NeuObject obj = sqlMgr.GetPaykindId(patient.ID);
                    patient.Pact.PayKind.ID = obj.User01;
                    patient.Pact.ID = obj.User02;
                    patient.PVisit.InState.ID = obj.User03;
                    patient.FT.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(obj.Memo);
                    //医保患者
                    if (patient.Pact.PayKind.ID == "02")
                    {

                        FS.HISFC.Models.Base.FT ft = this.feeMgr.QueryPatientSumFee(patient.ID, patient.PVisit.InTime.ToString(), dtEnd.ToString());
                            if (ft != null)
                            {
                                patient.PVisit.MedicalType.ID = sqlMgr.GetSiEmplType(patient.ID);
                                FS.HISFC.BizProcess.Integrate.Fee feeProcess = new FS.HISFC.BizProcess.Integrate.Fee();
                                feeProcess.ComputePatientSumFee(patient, ref ft);

                                decimal totCost=ft.PubCost+ft.PayCost;
                                ft.PrepayCost = patient.FT.PrepayCost;
                                decimal freecost = (totCost > ft.DefTotCost) ? (ft.PrepayCost - ft.OwnCost - ft.PayCost - ft.DefTotCost) : (ft.PrepayCost - ft.OwnCost - ft.PayCost);
                                sqlMgr.UpdateInMainInfoFreeCost(patient.ID, freecost);
                            }

                    }
                    inpatientLine.Add(patient.ID);
                }
                GetPatients(node, inpatientLine);
            }
        }



        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //if (this.dwMain != null)
            //{
            //    this.dwMain.PrintProperties.MarginBottom = 20;
            //    this.dwMain.Print(true, true);
            //}

            try
            {
                if (printSize.IsEmpty)
                {
                    /// <summary>
                    /// 打印纸张设置类
                    /// </summary>
                    FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
                    FS.HISFC.Models.Base.PageSize ps = psManager.GetPageSize("letter");
                    if (ps == null)
                    {
                        this.dwMain.Print();
                    }
                    else
                    {

                        dwMain.PrintProperties.PrinterName = ps.Printer;
                        dwMain.Modify(string.Format("DataWindow.Print.PrinterName='{0}'", ps.Printer));
                        dwMain.Modify("DataWindow.Print.Paper.Size=256");
                        dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Length={0}", ps.Height));
                        dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Width={0}", ps.Width));
                        this.dwMain.Height = ps.Height;
                        this.dwMain.Print(true, true);
                    }


                }
                else
                {
                    dwMain.Modify("DataWindow.Print.Paper.Size=256");
                    //此处设置letter纸为：216*279 像素为850*1100
                    //dwMain.Modify("DataWindow.Print.CustomPage.Length=1100");
                    //dwMain.Modify("DataWindow.Print.CustomPage.Width=425");
                    //此处设置letter纸为：216*279
                    dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Length={0}", printSize.Height));
                    dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Width={0}", printSize.Width));
                    this.dwMain.Print(true, true);
                }
                // this.dwMain.Print(true, true);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出错", "提示");
                return -1;
            }
        }

        private void ucFinIpbPatientDayFee_Load(object sender, EventArgs e)
        {
            //默认打印昨日清单
            FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
            this.dtpBeginTime.Value = orderMgr.GetDateTimeFromSysDateTime().Date.AddDays(-1);
        }
    }
}

