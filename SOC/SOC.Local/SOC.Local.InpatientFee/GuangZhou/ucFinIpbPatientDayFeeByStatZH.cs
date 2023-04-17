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
    public partial class ucFinIpbPatientDayFeeByStatZH : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFinIpbPatientDayFeeByStatZH()
        {
            InitializeComponent();
            //this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);
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
                AlSort sort=new AlSort();
                emplList.Sort(sort);

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
                    emplNode.Text = "【" + empl.PVisit.PatientLocation.Bed.ID.Substring(4) + "】" + empl.Name;
                    parentTreeNode.Nodes.Add(emplNode);
                }

                //parentTreeNode.ExpandAll();   //不需要全部展开
                parentTreeNode.Checked = false;
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载全院患者列表，请稍等......");
                Application.DoEvents();


                //全院患者列表
                //在院患者
                ArrayList emplList = managerIntegrate.QueryPatient(FS.HISFC.Models.Base.EnumInState.I);
                AlSort sort = new AlSort();
                emplList.Sort(sort);

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
                        patient.Text = empl.Name + "【" + empl.PVisit.PatientLocation.Bed.ID.Substring(4) + "-" + empl.PID.PatientNO.ToString() + "】";

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
                        patient.Text = empl.Name + "【" + empl.PVisit.PatientLocation.Bed.ID.Substring(4) + "-" + empl.PID.PatientNO.ToString() + "】";
                        patient.Checked = false;
                        dept.Nodes.Add(patient);
                        deptDic.Add(empl.PVisit.PatientLocation.Dept.ID, dept);

                        dept.Checked = false;
                        parentTreeNode.Nodes.Add(dept);
                    }
                    index++;
                }
                //parentTreeNode.ExpandAll();  //不需要全部展开
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

            if (neuDateTime.Value.Date >= managerIntegrate.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show("不能查询当前日期或未来日期费用，抱歉！");
                return;
            }
            DateTime dtBeginTime = neuDateTime.Value.Date;
            DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);
            DataSet ds = new DataSet();
            DataSet dsFee= new DataSet();
 
            foreach (string inpatientNo in inpatientLine)
            {
                int intReturn = this.GetInpatientDayFeeByStat(inpatientNo, dtBeginTime.ToString(), dtEndTime.ToString(), "ALL", ref ds);
                int intReturnFee = this.GetInpatientDayFeeDetailByStat(inpatientNo, dtBeginTime.ToString(), dtEndTime.ToString(), ref dsFee);

                if (intReturn == -1 || intReturnFee == -1)
                {
                    MessageBox.Show("查询错误");
                    return;
                }
                int intRowCount = ds.Tables[0].Rows.Count;

                DataRow objDr = null;

                FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);

                if (ds != null && ds.Tables.Count > 0 && dsFee != null && dsFee.Tables[0].Rows.Count > 0)
                {
                    Font font = new Font(this.neuSpread1.Font.FontFamily, 14F, System.Drawing.FontStyle.Bold);
                    decimal dayCost = 0;  //当天合计

                    #region 循环每个患者往farpoint赋值

                    int pageCount = 1;
                    for (int rowCount = 0; rowCount < dsFee.Tables[0].Rows.Count; )
                    {
                        objDr = ds.Tables[0].Rows[0];
                        //增加标题。
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 7);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = objDr[0].ToString() + "患者一日清单(汇总)";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = font;//加粗,加大标题
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;//居中

                        //增加患者基本信息
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 2;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "住院号:" + objDr[1].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                        //床号未去掉科室号
                        string bedNO=objDr[11].ToString();
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "床号:" + bedNO;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].ColumnSpan = 3;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "病区:" + objDr[6].ToString();

                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 2;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "姓名:" + objDr[2].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = objDr[7].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].ColumnSpan = 2;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "日期:" + dtBeginTime.ToShortDateString();

                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;

                        //显示患者费用列头
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "代码", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, "名称", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, "规格", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 5, "单位", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, "单价", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 4, "数量", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 6, "金额", false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 7, "标注", false);

                        
                        DataRow objDrFeeDetail = null;
                        int maxRowCount = 0;
                        if (dsFee.Tables[0].Rows.Count - 60 * pageCount > 0)
                        {
                            maxRowCount = 60;
                        }
                        else
                        {
                            maxRowCount = dsFee.Tables[0].Rows.Count - 60 * (pageCount - 1);
                        }
                        for (int i = 0; i < maxRowCount; i++)
                        {
                            objDrFeeDetail = dsFee.Tables[0].Rows[rowCount];
                            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, objDrFeeDetail[1], false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, objDrFeeDetail[2], false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, objDrFeeDetail[3], false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 5, objDrFeeDetail[6], false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, objDrFeeDetail[4], false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 4, objDrFeeDetail[5], false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 6, objDrFeeDetail[7], false);
                            //用于显示补给预收的列
                            string memo = this.GetMemo(objDrFeeDetail[9].ToString(), objDrFeeDetail[10].ToString());
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 7, memo, false);
                            
                            dayCost += FS.FrameWork.Function.NConvert.ToDecimal(objDrFeeDetail[7].ToString());
                            rowCount = rowCount + 1;
                        }

                        //当天金额
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 6;
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "当天合计：" + dayCost.ToString("F2");


                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 2);


                    
                        if (rowCount == dsFee.Tables[0].Rows.Count)
                        {
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 0].ColumnSpan = 7;
                            if (objDr[7].ToString() == "现金")
                            {
                                this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0,
                                                "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString() + "    "
                                               + "已结算金额：" + FS.FrameWork.Function.NConvert.ToDecimal(objDr[20]).ToString()+"   "
                                               + "余额：" + (Convert.ToDecimal(objDr[12]) - Convert.ToDecimal(objDr[3]) + Convert.ToDecimal(objDr[20])), false);
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0, "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString());
                            }
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 7;
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "此清单仅供参考，实际发生费用以出院结算单为准。");
                            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        }
                        //this.neuSpread1.PrintSheet(0);
                        pageCount = pageCount + 1;

                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// 返回算出的东西
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetMemo(string date,string type)
        {
            //负交易 显示退费
            if (type == "2")
            {
                if (date != string.Empty)
                {
                    return "退" + date.Substring(5, 2) + "." + date.Substring(8, 2);
                }
            }
            else//正交易 计算补收还是预计
            {
                try
                {
                    if (date != "0001-01-01 00:00:00" && date != string.Empty)
                    {
                        DateTime printDate = neuDateTime.Value.Date;
                        DateTime feeDate = Convert.ToDateTime(date);
                        if (printDate.Date < feeDate.Date)
                        {
                            return "预计" + date.Substring(5, 2) + "." + date.Substring(8, 2);
                        }
                        else if (printDate.Date > feeDate.Date)
                        {
                            return "补收" + date.Substring(5, 2) + "." + date.Substring(8, 2);
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch { }
            }
            return date+type;
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

        public int GetInpatientDayFeeDetailByStat(string inpatientNo, string beginDate, string endDate, ref DataSet ds)
        {
            string sql = "";

            if (this.managerIntegrate.Sql.GetSql("Fee.InpatientFee.GetInpatientDayFeeDetailByStatZH", ref sql) == -1)
            {
                this.managerIntegrate.Err = "获取患者一日费用明细出错!";
                return -1;
            }
            #region sql 作废
//            sql = @"select (select fc.fee_stat_name
//          from fin_com_feecodestat fc
//         where fc.report_code = 'ZY01'
//           and fc.fee_code = 费用名称) 费用名称,
//        项目编码,
//        项目名称,
//                规格,
//        单价,
//        数量,
//        单位,
//        金额,
//        
//        inpatient_no,
//        执行时间,
//        正负交易
//  from
//(
//select *
//  from
//(
//select m.fee_code 费用名称,
//       (select p.custom_code
//          from pha_com_baseinfo p
//         where p.drug_code = m.drug_code) 项目编码,
//       m.drug_name||decode(m.specs,'','','['||m.specs||']') 项目名称,
//             m.specs 规格,
//         ROUND(M.UNIT_PRICE/M.PACK_QTY,2) 单价,
//        sum(m.qty)  数量,
//       m.current_unit 单位,
//       round(sum(m.tot_cost), 2) 金额,
//    (select e.use_time
//                          from met_ipm_execdrug e
//                         where e.exec_sqn = m.mo_exec_sqn) as 执行时间,
//       m.inpatient_no,
//       m.trans_type as 正负交易
//  from fin_ipb_medicinelist m, fin_ipr_inmaininfo r
// where m.inpatient_no = r.inpatient_no
//   and m.fee_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
//   and m.fee_date < to_date('{2}','yyyy-mm-dd hh24:mi:ss')
//     and m.inpatient_no='{0}'
//     and m.inpatient_no='{0}'
// group by m.fee_code, m.drug_code, m.drug_name, m.specs, m.unit_price, m.pack_qty, r.paykind_code, r.pact_code, m.inpatient_no, m.current_unit,m.mo_exec_sqn,m.trans_type
//having sum(m.tot_cost) <> 0
//
// union all
// 
//select  i.fee_code 费用名称,
//        (select bb.input_code
//          from fin_com_undruginfo bb
//         where bb.item_code = i.item_code) 项目编码,
//       i.item_name 项目名称,
//             '1项' 规格,
//       i.unit_price 单价,
//       sum(i.qty) 数量,
//       i.current_unit 单位,
//       sum(i.tot_cost) 金额,
//        (select e.use_time from met_ipm_execundrug e where e.exec_sqn=i.mo_exec_sqn) as 执行时间,
//       i.inpatient_no,
//         i.trans_type as 正负交易
//  from fin_ipb_itemlist i, fin_ipr_inmaininfo r
// where i.inpatient_no = r.inpatient_no
//   and i.fee_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
//   and i.fee_date < to_date('{2}','yyyy-mm-dd hh24:mi:ss')
//     and i.inpatient_no='{0}'
//     and r.inpatient_no='{0}'
//group by r.paykind_code, i.inpatient_no, i.unit_price,i.item_code,i.item_name,i.fee_code,i.current_unit,i.mo_exec_sqn, i.trans_type
//)
//)
// order by 费用名称, 项目编码";
            #endregion
            sql = string.Format(sql, inpatientNo, beginDate, endDate);
            this.managerIntegrate.ExecQuery(sql, ref ds);
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
            if (!string.IsNullOrEmpty(this.ucQueryInpatientNo1.TextBox.Text.ToString()) && !string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo.ToString()))
            {
                FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize ps = pageSizeMgr.GetPageSize("dayFee");
                if (ps == null)
                {
                    MessageBox.Show("请在信息科维护处，维护名叫“dayFee”的纸张，一般设置为高400，宽450");
                    return;
                }
                FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
                System.Drawing.Printing.PaperSize ps1 = new System.Drawing.Printing.PaperSize();

                ps1.PaperName = "dayFee";
                ps1.Width = ps.Width;
                ps1.Height = ps.Height;
                //  ps1.Height = 1100;
                ps.Top = 0;
                pi.PaperSize = ps1;
                pi.ShowRowHeaders = false;
                pi.ShowColumnHeaders = false;
                pi.Preview = false;

                this.printSheet();
            }

            else
            {
                //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize ps = pageSizeMgr.GetPageSize("dayFee");
                if (ps == null)
                {
                    MessageBox.Show("请在信息科维护处，维护名叫“dayFee”的纸张，一般设置为高400，宽450");
                    return;
                }
                FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
                System.Drawing.Printing.PaperSize ps1 = new System.Drawing.Printing.PaperSize();

                ps1.PaperName = "dayFee";
                ps1.Width = ps.Width;
                ps1.Height = ps.Height;
                //  ps1.Height = 1100;
                ps.Top = 0;
                pi.PaperSize = ps1;
                pi.ShowRowHeaders = false;
                pi.ShowColumnHeaders = false;
                pi.Preview = false;


                if (this.neuSpread1_Sheet1.RowCount > 0)
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                List<string> inpatientLine = new List<string>();
                GetPatients(this.neuTvLeft.Nodes[0], inpatientLine);
                if (inpatientLine.Count <= 0
                    && (!string.IsNullOrEmpty(this.ucQueryInpatientNo1.TextBox.Text.ToString()) && !string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo.ToString()))
                    )
                {
                    MessageBox.Show("请选择患者");
                    return;
                }

                if (neuDateTime.Value.Date >= managerIntegrate.GetDateTimeFromSysDateTime().Date)
                {
                    MessageBox.Show("不能查询当前日期或未来日期费用，抱歉！");
                    return;
                }
                DateTime dtBeginTime = neuDateTime.Value.Date;
                DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);

                foreach (string inpatientNo in inpatientLine)
                {
                    this.Print(inpatientNo, dtBeginTime, dtEndTime);
                }
            }
            //this.neuSpread1_Sheet1.PrintInfo = pi;
            //this.neuSpread1_Sheet1.PrintInfo.Margin.Top = 0;
            //this.neuSpread1_Sheet1.PrintInfo.Margin.Bottom = 30;
            //this.neuSpread1_Sheet1.PrintInfo.ShowBorder = false;
            //this.neuSpread1.PrintSheet(0);

            //p.PrintPage(0, 0, this.neuPlFp);
        }

        public void Print(string inpatientNo, DateTime dtBeginTime, DateTime dtEndTime)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            DataSet ds = null;
            DataSet dsFee = null;
            int intReturn = this.GetInpatientDayFeeByStat(inpatientNo, dtBeginTime.ToString(), dtEndTime.ToString(), "ALL", ref ds);
            int intReturnFee = this.GetInpatientDayFeeDetailByStat(inpatientNo, dtBeginTime.ToString(), dtEndTime.ToString(), ref dsFee);

            if (intReturn == -1 || intReturnFee == -1)
            {
                MessageBox.Show("查询错误");
                return;
            }
            int intRowCount = ds.Tables[0].Rows.Count;

            DataRow objDr = null;

            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);

            if (ds != null && ds.Tables.Count > 0 && dsFee != null && dsFee.Tables[0].Rows.Count > 0)
            {
                Font font = new Font(this.neuSpread1.Font.FontFamily, 14F, System.Drawing.FontStyle.Bold);
                decimal dayCost = 0;

                #region 循环每个患者往farpoint赋值

                int pageCount = 1;
                for (int rowCount = 0; rowCount < dsFee.Tables[0].Rows.Count; )
                {
                    if (this.neuSpread1_Sheet1.RowCount > 0)
                        this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                    objDr = ds.Tables[0].Rows[0];
                    //增加标题。
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 7);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = objDr[0].ToString() + "患者一日清单(汇总)";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = font;//加粗,加大标题
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;//居中

                    //增加患者基本信息
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "住院号:" + objDr[1].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "床号:" +  objDr[11].ToString().Substring(4);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].ColumnSpan = 3;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "病区:" + objDr[6].ToString();

                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "姓名:" + objDr[2].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = objDr[7].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "日期:" + dtBeginTime.ToShortDateString();

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;

                    //显示患者费用列头
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "代码", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, "名称", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, "规格", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 5, "单位", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, "单价", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 4, "数量", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 6, "金额", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 7, "标注", false);

                    
                    DataRow objDrFeeDetail = null;
                    int maxRowCount = 0;
                    if (dsFee.Tables[0].Rows.Count - 60 * pageCount > 0)
                    {
                        maxRowCount = 60;
                    }
                    else
                    {
                        maxRowCount = dsFee.Tables[0].Rows.Count - 60 * (pageCount - 1);
                    }
                    for (int i = 0; i < maxRowCount; i++)
                    {
                        objDrFeeDetail = dsFee.Tables[0].Rows[rowCount];
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, objDrFeeDetail[1], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, objDrFeeDetail[2], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, objDrFeeDetail[3], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 5, objDrFeeDetail[6], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, objDrFeeDetail[4], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 4, objDrFeeDetail[5], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 6, objDrFeeDetail[7], false);
                        //用于显示补给预收的列
                        string memo = this.GetMemo(objDrFeeDetail[9].ToString(), objDrFeeDetail[10].ToString());
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 7, memo, false);

                        dayCost += FS.FrameWork.Function.NConvert.ToDecimal(objDrFeeDetail[7].ToString());
                        rowCount = rowCount + 1;
                    }

                    //当天金额
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 6;
                    //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "当天合计：" + dayCost.ToString("F2");


                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 2);


                #endregion

                    //this.neuSpread1.PrintSheet(0);
                    if (rowCount == dsFee.Tables[0].Rows.Count)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 0].ColumnSpan = 7;
                        if (objDr[7].ToString() == "现金")
                        {
                            //this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0, "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString() + "    " + "余额：" + (Convert.ToDecimal(objDr[12]) - Convert.ToDecimal(objDr[3])), false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0,
                                                "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString() + "    "
                                               + "已结算金额：" + FS.FrameWork.Function.NConvert.ToDecimal(objDr[20]).ToString() + "   "
                                               + "余额：" + (Convert.ToDecimal(objDr[12]) - Convert.ToDecimal(objDr[3]) + Convert.ToDecimal(objDr[20])), false);
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0, "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString());
                        } 
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 7;
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "此清单仅供参考，实际发生费用以出院结算单为准。");
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    }
                    pageCount = pageCount + 1;
                    this.printSheet();
                }
                
            }

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

        private void printSheet()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("", 827, 1169);
            p.SetPageSize(ps);

            p.PrintPage(0, 0, this.neuPlFp);


        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            if (ucQueryInpatientNo1.InpatientNo==null)
            {
                MessageBox.Show("没有查询到相关患者信息");
                return;
            }

            if (neuDateTime.Value.Date >= managerIntegrate.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show("不能查询当前日期或未来日期费用，抱歉！");
                return;
            }
            DateTime dtBeginTime = neuDateTime.Value.Date;
            DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);
            DataSet ds = new DataSet();
            DataSet dsFee = new DataSet();

            int intReturn = this.GetInpatientDayFeeByStat(ucQueryInpatientNo1.InpatientNo.ToString(), dtBeginTime.ToString(), dtEndTime.ToString(), "ALL", ref ds);
            int intReturnFee = this.GetInpatientDayFeeDetailByStat(ucQueryInpatientNo1.InpatientNo.ToString(), dtBeginTime.ToString(), dtEndTime.ToString(), ref dsFee);

            if (intReturn == -1 || intReturnFee == -1)
            {
                MessageBox.Show("查询错误");
                return;
            }
            //int intRowCount = ds.Tables[0].Rows.Count;

            DataRow objDr = null;

            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);

            if (ds != null && ds.Tables.Count > 0 && dsFee != null && dsFee.Tables[0].Rows.Count > 0)
            {
                Font font = new Font(this.neuSpread1.Font.FontFamily, 14F, System.Drawing.FontStyle.Bold);
                decimal dayCost = 0;  //当天合计

                #region 循环每个患者往farpoint赋值

                int pageCount = 1;
                for (int rowCount = 0; rowCount < dsFee.Tables[0].Rows.Count; )
                {
                    objDr = ds.Tables[0].Rows[0];
                    //增加标题。
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 7);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = objDr[0].ToString() + "患者一日清单(汇总)";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = font;//加粗,加大标题
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;//居中

                    //增加患者基本信息
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "住院号:" + objDr[1].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                    //床号未去掉科室号
                    string bedNO = objDr[11].ToString();
                    if (bedNO.Length > 4)
                    {
                        bedNO = bedNO.Substring(4);
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "床号:" + bedNO;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].ColumnSpan = 3;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "病区:" + objDr[6].ToString();

                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "姓名:" + objDr[2].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = objDr[7].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].ColumnSpan = 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "日期:" + dtBeginTime.ToShortDateString();

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;

                    //显示患者费用列头
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "代码", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, "名称", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, "规格", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 5, "单位", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, "单价", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 4, "数量", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 6, "金额", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 7, "标注", false);


                    DataRow objDrFeeDetail = null;
                    int maxRowCount = 0;
                    if (dsFee.Tables[0].Rows.Count - 60 * pageCount > 0)
                    {
                        maxRowCount = 60;
                    }
                    else
                    {
                        maxRowCount = dsFee.Tables[0].Rows.Count - 60 * (pageCount - 1);
                    }
                    for (int i = 0; i < maxRowCount; i++)
                    {
                        objDrFeeDetail = dsFee.Tables[0].Rows[rowCount];
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, objDrFeeDetail[1], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, objDrFeeDetail[2], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, objDrFeeDetail[3], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 5, objDrFeeDetail[6], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, objDrFeeDetail[4], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 4, objDrFeeDetail[5], false);
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 6, objDrFeeDetail[7], false);
                        //用于显示补给预收的列
                        string memo = this.GetMemo(objDrFeeDetail[9].ToString(), objDrFeeDetail[10].ToString());
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 7, memo, false);

                        dayCost += FS.FrameWork.Function.NConvert.ToDecimal(objDrFeeDetail[7].ToString());
                        rowCount = rowCount + 1;
                    }

                    //当天金额
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 6;
                    //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "当天合计：" + dayCost.ToString("F2");


                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 2);



                    if (rowCount == dsFee.Tables[0].Rows.Count)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 0].ColumnSpan = 7;
                        if (objDr[7].ToString() == "现金")
                        {
                            //this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0, "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString() + "    " + "余额：" + (Convert.ToDecimal(objDr[12]) - Convert.ToDecimal(objDr[3])), false);
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0,
                                                "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString() + "    "
                                               + "已结算金额：" + FS.FrameWork.Function.NConvert.ToDecimal(objDr[20]).ToString() + "   "
                                               + "余额：" + (Convert.ToDecimal(objDr[12]) - Convert.ToDecimal(objDr[3]) + Convert.ToDecimal(objDr[20])), false);
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 2, 0, "当天合计：" + dayCost.ToString("F2") + "    " + "预交金：" + objDr[12].ToString() + "    " + "累计费用：" + objDr[3].ToString());
                        }
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 7;
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "此清单仅供参考，实际发生费用以出院结算单为准。");
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    }
                    //this.neuSpread1.PrintSheet(0);
                    pageCount = pageCount + 1;

                }

                #endregion
            }
        }

    }

    public class AlSort : System.Collections.IComparer
    {
        #region IComparer 成员

        /// <summary>
        /// 比较方号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfox = x as FS.HISFC.Models.RADT.PatientInfo;
                FS.HISFC.Models.RADT.PatientInfo patientInfoy = y as FS.HISFC.Models.RADT.PatientInfo;
                if (Convert.ToInt32(patientInfox.PVisit.PatientLocation.Bed.ID) > Convert.ToInt32(patientInfoy.PVisit.PatientLocation.Bed.ID))
                {
                    return 1;
                }
                else if (Convert.ToInt32(patientInfox.PVisit.PatientLocation.Bed.ID) == Convert.ToInt32(patientInfoy.PVisit.PatientLocation.Bed.ID))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        #endregion
    }
}
