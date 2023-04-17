﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.InpatientFee.ChaoYang
{
    public partial class ucFinIpbPatientDayFeeByStat : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFinIpbPatientDayFeeByStat()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();

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
                    TreeNode emplNode = new TreeNode();
                    emplNode.Tag = empl;
                    emplNode.Text = "【" + empl.PVisit.PatientLocation.Bed.ID.Substring(4) + "】" + empl.Name;
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
            string[] inpatient = inpatientLine.ToArray();

            DateTime dtBeginTime = neuDateTime.Value.Date;
            DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);
            string inpatientNo = "";
            int index = 0;
            for (index = 0; index < inpatient.Count() - 1; index++)
            {
                inpatientNo += inpatient[index] + "'" + "," + "'";
            }
            inpatientNo += inpatient[index];
            DataSet ds = new DataSet();
            int intReturn = this.GetInpatientDayFeeByStat(inpatientNo, dtBeginTime.ToString(), dtEndTime.ToString(), "ALL", ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show("查询错误");
                return;
            }

            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText,0, false, false, false, false);
            if (ds != null && ds.Tables.Count > 0)
            {
                Font font = new Font(this.neuSpread1.Font.FontFamily, 14F, System.Drawing.FontStyle.Bold);
                #region 循环每个患者往farpoint赋值
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    DataRow row = ds.Tables[0].Rows[k];
                    int lineIndex = 0;
                    int rowIndex = 0;
                    if (k % 2 == 1)
                    {
                        lineIndex = 7;
                    }

                    //增加标题。
                    if (k % 2 == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        rowIndex = this.neuSpread1_Sheet1.RowCount;
                    }
                    else
                    {
                        rowIndex = this.neuSpread1_Sheet1.RowCount - 12;
                    }
                    
                    this.neuSpread1_Sheet1.Models.Span.Add(rowIndex - 1, lineIndex, 1, 6);
                    this.neuSpread1_Sheet1.Cells[rowIndex - 1, lineIndex].Text = row[0].ToString() + "患者一日清单";
                    this.neuSpread1_Sheet1.Cells[rowIndex - 1, lineIndex].Font = font;//加粗,加大标题
                    this.neuSpread1_Sheet1.Cells[rowIndex - 1, lineIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;//居中
                    //增加患者基本信息
                    if (k % 2 == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 4);
                        rowIndex = this.neuSpread1_Sheet1.RowCount;
                    }
                    else
                    {
                        rowIndex = this.neuSpread1_Sheet1.RowCount - 8;
                    }


                    this.neuSpread1_Sheet1.Models.Span.Add(rowIndex - 4, lineIndex, 1, 6);
                    this.neuSpread1_Sheet1.Cells[rowIndex - 4, lineIndex].Text = "统计时间:" + dtBeginTime.ToString() + "-" + dtEndTime.ToString();
                    this.neuSpread1_Sheet1.Models.Span.Add(rowIndex - 3, lineIndex, 1, 6);
                    this.neuSpread1_Sheet1.Cells[rowIndex - 3, lineIndex].Text = "入院日期:" + row[10].ToString() + "  " + "病区:" + row[6].ToString() + "  " + "病床号:" + row[11].ToString();
                    this.neuSpread1_Sheet1.Models.Span.Add(rowIndex - 2, lineIndex, 1, 6);
                    this.neuSpread1_Sheet1.Cells[rowIndex - 2, lineIndex].Text = "合同单位:" + row[7].ToString() + "   " + "住院次数:" + row[17].ToString() + "    " + "住院号:" + row[1].ToString();
                    this.neuSpread1_Sheet1.Models.Span.Add(rowIndex - 1, lineIndex, 1, 6);
                    this.neuSpread1_Sheet1.Cells[rowIndex - 1, lineIndex].Text = "姓名:" + row[2].ToString() + "    " + "性别:" + row[9].ToString() + "    " + "年龄:" + row[8].ToString();
                    this.neuSpread1_Sheet1.Rows[rowIndex - 1].Border = lineBorder1;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 1,6].Border = lineBorder2;
                   
                    //显示患者费用列头
                    if (k % 2 == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        rowIndex = this.neuSpread1_Sheet1.RowCount;
                    }
                    else
                    {
                        rowIndex = this.neuSpread1_Sheet1.RowCount - 7;
                    }


                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 0, "类别", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 1, "费用", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 2, "类别", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 3, "费用", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 4, "类别", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 5, "费用", false);
                    //显示患者具体统计大类费用
                    if (k % 2 == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 4);
                        rowIndex = this.neuSpread1_Sheet1.RowCount;
                    }
                    else
                    {
                        rowIndex = this.neuSpread1_Sheet1.RowCount - 3;
                    }

                    this.neuSpread1_Sheet1.SetValue(rowIndex - 4, lineIndex + 0, "西药", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 4, lineIndex + 1, row[18].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 4, lineIndex + 2, "中成药", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 4, lineIndex + 3, row[19].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 4, lineIndex + 4, "中草药", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 4, lineIndex + 5, row[20].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 3, lineIndex + 0, "诊查费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 3, lineIndex + 1, row[21].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 3, lineIndex + 2, "化验费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 3, lineIndex + 3, row[22].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 3, lineIndex + 4, "检查费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 3, lineIndex + 5, row[23].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 2, lineIndex + 0, "手术费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 2, lineIndex + 1, row[24].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 2, lineIndex + 2, "治疗费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 2, lineIndex + 3, row[25].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 2, lineIndex + 4, "材料费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 2, lineIndex + 5, row[26].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 0, "床位费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 1, row[27].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 2, "护理费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 3, row[28].ToString(), false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 4, "其他费", false);
                    this.neuSpread1_Sheet1.SetValue(rowIndex - 1, lineIndex + 5, row[29].ToString(), false);

                    this.neuSpread1_Sheet1.Rows[rowIndex - 1].Border = lineBorder1;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 1, 6].Border = lineBorder2;
                    //增加患者费用信息
                    if (k % 2 == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 2);
                        rowIndex = this.neuSpread1_Sheet1.RowCount;
                    }
                    else
                    {
                        rowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                    }

                    this.neuSpread1_Sheet1.Models.Span.Add(rowIndex - 2, lineIndex, 1, 6);
                    this.neuSpread1_Sheet1.Cells[rowIndex - 2, lineIndex].Text = "结存余额:" + row[14].ToString() + " " + "预交款:" + row[12].ToString() + " " + "未结费用总额:" + row[3].ToString();
                    this.neuSpread1_Sheet1.Models.Span.Add(rowIndex - 1, lineIndex, 1, 6);
                    this.neuSpread1_Sheet1.Cells[rowIndex - 1, lineIndex].Text ="本张清单费用小计:" + row[30].ToString()+" "+ "打印日期:" + managerIntegrate.GetSysDateTime();
                    if (k % 2 == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    }
                }
                #endregion

                int rowCount = ds.Tables[0].Rows.Count;
                if (rowCount % 2 == 1)
                {
                    int rowIndex = this.neuSpread1_Sheet1.Rows.Count;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 4, 7].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 4, 8].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 4, 9].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 4, 10].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 4, 11].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 4, 12].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 9, 7].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 9, 8].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 9, 9].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 9, 10].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 9, 11].Border = lineBorder2;
                    this.neuSpread1_Sheet1.Cells[rowIndex - 9, 12].Border = lineBorder2;

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

            if (this.managerIntegrate.Sql.GetSql("Fee.InpatientFee.GetInpatientDayFeeByStat", ref sql) == -1)
            {
                this.managerIntegrate.Err = "获取患者一日费用出错!";
                return -1;
            }
            sql = string.Format(sql, inpatientNo, beginDate, endDate, dept);
            this.managerIntegrate.ExecQuery(sql, ref ds);
            return 1;
        }

        public void Print()
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
            ps.Top = 0;
            pi.PaperSize = ps1;
            pi.ShowRowHeaders = false;
            pi.ShowColumnHeaders = false;
            pi.Preview = false;

            this.neuSpread1_Sheet1.PrintInfo = pi;
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
