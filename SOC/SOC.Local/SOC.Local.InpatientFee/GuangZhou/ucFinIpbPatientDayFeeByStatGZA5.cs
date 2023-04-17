using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class ucFinIpbPatientDayFeeByStatGZA5 : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFinIpbPatientDayFeeByStatGZA5()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient  feeMgr = new  FS.HISFC.BizLogic.Fee.InPatient();

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


        private void Init()
        {
            this.OnDrawTree();
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.ColumnHeader.Visible = false;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;

            this.neuDateTime.Value = managerIntegrate.GetDateTimeFromSysDateTime().Date.AddDays(-1);
        }

        /// <summary>
        ///初始化树
        /// </summary>
        /// <returns></returns>
        protected int OnDrawTree()
        {
            if (this.neuTvLeft == null)
            {
                return -1;
            }

            this.neuTvLeft.Nodes.Clear();

            //左侧多选
            this.neuTvLeft.CheckBoxes = true;

            if (isShowAllInDeptPatient == false)
            {
                //在院患者
                FS.HISFC.Models.RADT.InStateEnumService inState = new FS.HISFC.Models.RADT.InStateEnumService();
                inState.ID = FS.HISFC.Models.Base.EnumInState.I.ToString();

                FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
                employee = managerIntegrate.Operator as FS.HISFC.Models.Base.Employee;
                ArrayList emplList = managerIntegrate.QueryPatientBasicByNurseCell(employee.Nurse.ID, inState);

                TreeNode parentTreeNode = new TreeNode("本区患者");
                parentTreeNode.Checked = false;
                parentTreeNode.Tag = "ROOT";
                neuTvLeft.Nodes.Add(parentTreeNode);
                foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
                {
                    if (string.IsNullOrEmpty(empl.PVisit.PatientLocation.Bed.ID))
                    {
                        continue;
                    }
                    TreeNode emplNode = new TreeNode();
                    emplNode.Tag = empl;
                    emplNode.Text = "【"+empl.PVisit.PatientLocation.Bed.ID.Substring(4)+"】"+empl.Name;
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
                neuTvLeft.Nodes.Add(parentTreeNode);
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
                        patient.Text = empl.Name + "【" + empl.PID.PatientNO.ToString() + "】";
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




            this.neuTvLeft.AfterSelect -= new TreeViewEventHandler(neuTvLeft_AfterSelect);
            this.neuTvLeft.AfterSelect += new TreeViewEventHandler(neuTvLeft_AfterSelect);
            this.neuTvLeft.AfterCheck -= new TreeViewEventHandler(neuTvLeft_AfterCheck);
            this.neuTvLeft.AfterCheck += new TreeViewEventHandler(neuTvLeft_AfterCheck);

            return 1;
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTvLeft_AfterSelect(object sender, TreeViewEventArgs e)
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
        private void neuTvLeft_AfterCheck(object sender, TreeViewEventArgs e)
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
        public void Query()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            List<string> inpatientLine = new List<string>();
            GetPatients(this.neuTvLeft.Nodes[0], inpatientLine);
            if (inpatientLine.Count <= 0)
            {
                MessageBox.Show("请选择患者");
                return;
            }
            //string[] inpatient = inpatientLine.ToArray();

            if (neuDateTime.Value.Date >= managerIntegrate.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show("不能查询当前日期或未来日期费用，抱歉！");
                return;
            }
            DateTime dtBeginTime = neuDateTime.Value.Date;
            DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);
            //string inpatientNo = "";
            DataSet ds;
            //int index = 0;
            //for (index = 0; index < inpatient.Count()-1; index++)
            //{
            //    inpatientNo += inpatient[index]+"'"+","+"'";
            //}
            //inpatientNo += inpatient[index];
            foreach (string inpatientNo in inpatientLine)
            {

                ds = new DataSet();
                int intReturn = this.GetInpatientDayFeeByStat(inpatientNo, dtBeginTime.ToString(), dtEndTime.ToString(), "ALL", ref ds);
                if (intReturn == -1)
                {
                    MessageBox.Show("查询错误");
                    return;
                }
                int intRowCount = ds.Tables[0].Rows.Count;
                Hashtable objHash = new Hashtable();
                List<DataRow> objListRow = null;
                DataRow objDr = null;
                for (int intI = 0; intI < intRowCount; intI++)
                {
                    objDr = ds.Tables[0].Rows[intI];
                    if (!objHash.ContainsKey(objDr[1].ToString()))
                    {
                        objListRow = new List<DataRow>();
                        objListRow.Add(objDr);
                        objHash.Add(objDr[1].ToString(), objListRow);
                    }
                    else
                    {
                        objListRow = objHash[objDr[1].ToString()] as List<DataRow>;
                        objListRow.Add(objDr);
                    }
                }
                FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);
                if (ds != null && ds.Tables.Count > 0)
                {
                    Font font = new Font(this.neuSpread1.Font.FontFamily, 14F, System.Drawing.FontStyle.Bold);
                    #region 循环每个患者往farpoint赋值
                    foreach (DictionaryEntry item in objHash)
                    {
                        objListRow = (List<DataRow>)item.Value;
                        objDr = objListRow[0];
                        //增加标题。
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 4);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = objDr[0].ToString() + "患者一日清单(汇总)";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = font;//加粗,加大标题
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;//居中
                        //增加患者基本信息
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "病床号:" + objDr[11].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "病区:" + objDr[6].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "住院号:" + objDr[1].ToString();

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "姓名:" + objDr[2].ToString();
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合同单位:" + objDr[7].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "日期:" + dtBeginTime.ToShortDateString();
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;

                        //显示患者费用列头
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "类别", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, "费用", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, "类别", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, "费用", false);
                        int intCount = objListRow.Count;
                        int intM = 0;
                        int intJ = 0;
                        DataRow objdr1 = null;
                        decimal dcmTot_Cost = 0;
                        for (intM = 0; intM < intCount; intM++)
                        {
                            objdr1 = objListRow[intM];
                            intJ = intM % 2;
                            if (intJ == 0)
                            {
                                //显示患者具体统计大类费用
                                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                            }
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, intJ * 2].Text = objdr1[18].ToString();
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, intJ * 2 + 1].Text = objdr1[19].ToString();
                            dcmTot_Cost += objDr[19] == null ? 0 : decimal.Parse(objdr1[19].ToString());

                            if (intM == intCount - 1)
                            {
                                this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;
                            }
                        }
                        //增加患者费用信息
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 3);

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 3, 0].Text = "今天金额：";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 3, 2].Text = "自付比例：";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 0].Text = "已结金额：";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 2].Text = "预交款：";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "未结总费用：";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "结余金额：";

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 3, 1].Text = dcmTot_Cost.ToString();//今天金额
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 3, 3].Text = objDr[22].ToString();//自付比例
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 1].Text = objDr[20].ToString();//已结金额
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 3].Text = objDr[12].ToString();//预交款
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = objDr[3].ToString();//未结总费用
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = objDr[14].ToString();//结余金额

                        FS.HISFC.Models.RADT.PatientInfo obj=this.managerIntegrate.QueryPatientInfoByInpatientNO(inpatientNo);
                        if (obj.Pact.PayKind.ID == "02")
                        {
                            FS.HISFC.Models.Base.FT ft = this.feeMgr.QueryPatientSumFee(inpatientNo, obj.PVisit.InTime.ToString(), dtEndTime.ToString());
                            if (ft != null)
                            {
                                obj.PVisit.MedicalType.ID = this.GetSiEmplType(inpatientNo);
                                FS.HISFC.BizProcess.Integrate.Fee feeProcess = new FS.HISFC.BizProcess.Integrate.Fee();
                                feeProcess.ComputePatientSumFee(obj, ref ft);
                                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 2);
                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 0].Text = "起付线金额：";
                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 2].Text = "按比例自付金额：";
                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "自费金额：";
                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 1].Text = ft.DefTotCost.ToString();
                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 3].Text = ft.PayCost.ToString();
                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = ft.OwnCost.ToString();

                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 5, 3].Text = (ft.FTRate.PayRate * 100).ToString() + "%";
                                ft.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(objDr[12].ToString());
                                //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 5].Text = (ft.PrepayCost - ft.OwnCost - ft.PayCost).ToString();
                                decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(objDr[3].ToString());
                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 3, 3].Text = (totCost > ft.DefTotCost) ? (ft.PrepayCost - ft.OwnCost - ft.PayCost - ft.DefTotCost).ToString() : (ft.PrepayCost - ft.OwnCost - ft.PayCost).ToString();
                            }
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 2);
                        }
                       

                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;

                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 4);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "备注：此清单结算费用仅供参考，最终以医保结算为准。";

                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 4);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "打印时间：" + DateTime.Now.ToString();

                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 9);

                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 递归获取选择的患者
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="inpatientLine"></param>
        void GetPatients(TreeNode nodes, List<string> inpatientLine)
        {
            foreach (TreeNode node in nodes.Nodes)
            {
                if (node.Checked && node.Tag is FS.HISFC.Models.RADT.PatientInfo)
                {
                    FS.HISFC.Models.RADT.PatientInfo patient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                    inpatientLine.Add(patient.ID);
                }
                GetPatients(node, inpatientLine);
            }
        }

        public int GetInpatientDayFeeByStat(string inpatientNo, string beginDate, string endDate, string dept, ref DataSet ds)
        {
            string sql = "";

            if (this.managerIntegrate.Sql.GetSql("Fee.InpatientFee.GetInpatientDayFeeByStatGZ", ref sql) == -1)
            {
                this.managerIntegrate.Err = "获取患者一日费用出错!";
                return -1;
            }
            sql = string.Format(sql, inpatientNo,beginDate,endDate,dept);
            this.managerIntegrate.ExecQuery(sql,ref ds);
            return 1;
        }

        private string GetSiEmplType(string inpatientNo)
        {
            string sql = "select si.empl_type from fin_ipr_siinmaininfo si where si.inpatient_no='{0}' and si.valid_flag='1'";
            string str="1";
            try
            {
             sql = string.Format(sql, inpatientNo);
             str = this.managerIntegrate.ExecSqlReturnOne(sql,"1");
             return str;
            }
            catch (Exception e)
            {

                return "1";
            }
        }

        public void Print()
        {
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize ps = pageSizeMgr.GetPageSize("dayFeeA5");
            if (ps==null)
            {
                MessageBox.Show("请在信息科维护处，维护名叫“dayFeeA5”的纸张，一般设置为高827，宽583");
                return;
            }
            FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
            System.Drawing.Printing.PaperSize ps1 = new System.Drawing.Printing.PaperSize();

            ps1.PaperName = "dayFeeA5";
            ps1.Width = ps.Width;
            ps1.Height = ps.Height;
          //  ps1.Height = 1100;
            ps.Top = 0;
            pi.PaperSize = ps1;
            pi.ShowRowHeaders = false;
            pi.ShowColumnHeaders = false;
            pi.Preview = true;
            

            this.neuSpread1_Sheet1.PrintInfo = pi;
            this.neuSpread1_Sheet1.PrintInfo.Margin.Top = 10;
            this.neuSpread1_Sheet1.PrintInfo.Margin.Left = 10;
            this.neuSpread1_Sheet1.PrintInfo.Margin.Bottom = 30;
            this.neuSpread1_Sheet1.PrintInfo.ShowBorder = false;
            this.neuSpread1.PrintSheet(0);
            //p.PrintPage(0, 0, this.neuPlFp);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

    }


}
