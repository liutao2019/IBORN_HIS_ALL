using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucImplent : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private ApplyTableManage applyTableManage;
        private string title;
        private ApplyTable currentTable;
        private string curApplyNum;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private DisTypeManage disTypeManage;
        private SpecTypeManage specTypeManage;
        private FS.HISFC.BizProcess.Integrate.Manager dpMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("生成申请ID号", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S申请单, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "生成申请ID号":
                    try
                    {
                        FS.HISFC.Components.Speciment.OutStore.frmNewApply frmApp = new FS.HISFC.Components.Speciment.OutStore.frmNewApply();
                        if (!((Form)frmApp).Visible)
                        {
                            this.Focus();
                            frmApp.StartPosition = FormStartPosition.CenterScreen;
                            frmApp.ShowDialog();
                        }
                        if (frmApp.RtResult == 1)
                        {
                            txtApplyNum.Text = frmApp.ApplyId;
                            this.Query();
                        }
                    }
                    catch
                    {
                    }
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public ucImplent()
        {
            InitializeComponent();
            applyTableManage = new ApplyTableManage();
            currentTable = new ApplyTable();
            disTypeManage = new DisTypeManage();
            specTypeManage = new SpecTypeManage();
            title = "审批录入";
            curApplyNum = "";
            ArrayList alDept = new ArrayList();
            alDept = this.dpMgr.GetDepartment();
            if ((alDept != null) && (alDept.Count > 0))
            {
                this.cmbApplyDept.AddItems(alDept);
            }
            ArrayList alEmpl = new ArrayList();
            alEmpl = this.dpMgr.QueryEmployeeAll();
            if ((alEmpl != null) && (alEmpl.Count > 0))
            {
                this.cmbEmployee.AddItems(alEmpl);
            }
        }



        /// <summary>
        /// 根据设置的条件查询数据
        /// </summary>
        private void Query()
        {
            try
            {
                if (txtApplyNum.Text.Trim() != "")
                {
                    int num = Convert.ToInt32(txtApplyNum.Text.Trim());
                }
            }
            catch
            {
                MessageBox.Show("申请编号须为数字类型，请重新填写！", title);
                return;
            }
            //ERROR [42601] [IBM][DB2/NT] SQL0104N  在 "ME　基金结束时间FROM" 后面找到异常标记 "SPEC_APPLICATIONTABLE"。预期标记可能包括："FROM"。  SQLSTATE=42601
            string strQuery = "SELECT APPLICATIONID 申请ID,APPLYUSERNAME 申请人姓名,DEPARTMENTNAME 部门名称,APPLYTIME 申请时间,SUBJECTNAME 项目名称,FUNDNAME 基金名称," +
                              " FUNDSTARTTIME 基金开始时间,FUNDENDTIME　基金结束时间,(CASE IMPRESLUT WHEN '2' THEN '拒绝' WHEN '1' THEN '同意'　WHEN '' THEN '审批中' END) 审批结果 ";
            strQuery += " FROM SPEC_APPLICATIONTABLE WHERE APPLICATIONID>0 ";
            strQuery += txtApplyNum.Text.Trim() == "" ? "" : " AND APPLICATIONID =" + txtApplyNum.Text.Trim();
            strQuery += this.cmbEmployee.Text.Trim() == "" ? "" : " AND APPLYUSERNAME LIKE '%" + cmbEmployee.Text.Trim() + "%'";
            strQuery += cmbApplyDept.Text.Trim() == "" ? "" : " AND DEPARTMENTNAME LIKE '%" + cmbApplyDept.Text + "%'";
            if (chkImped.Checked && !chxUnImped.Checked)
            {
                strQuery += " AND IMPPROCESS like 'O%'";
            }
            else if (!chkImped.Checked && chxUnImped.Checked)
            {
                strQuery += " AND IMPPROCESS='U'";
            }
            if (!cbxReject.Checked)
            {
                strQuery += " AND IMPRESLUT <> '2'";
            }
            DataSet ds = new DataSet();
            applyTableManage.ExecQuery(strQuery, ref ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            dgvResult.DataSource = dt;
            this.setFormat();
        }

        /// <summary>
        /// 根据申请编号查找申请要求，并显示在页面上
        /// </summary>
        /// <param name="curApplyNum">当前的申请编号</param>
        private void QueryDemand(string curApplyNum)
        {
            flpApplyDemand.Controls.Clear();
            string disType = "";
            int specAmount = 0;
            string specType = "";
            string specIsCancer = "";
            string specDetAmount = "";
            string specOutList = "";
            string specOutList1 = "";

            disType = currentTable.DiseaseType;
            specAmount = currentTable.SpecAmout;
            specType = currentTable.SpecType;
            specIsCancer = currentTable.SpecIsCancer;
            specDetAmount = currentTable.SpecDetAmout;
            specOutList = currentTable.SpecList;

            if (specOutList.EndsWith(","))
            {
                int lastIndex = specOutList.LastIndexOf(',');
                specOutList1 = "( " + specOutList.TrimEnd().Remove(lastIndex) + ")";
            }
            else
                specOutList1 = "( " + specOutList + ")";

            string[] disTypeList = disType.Split(',');
            string[] specTypeList = specType.Split(',');
            string[] specIsCancerList = specIsCancer.Split(',');
            string[] specDetAmountList = specDetAmount.Split(',');
            int index = 0;
            foreach (string det in disTypeList)
            {
                //查询出已出库的标本数量
                string sql = "SELECT DISTINCT COUNT(SPEC_SUBSPEC.SUBSPECID) OUTCOUNT" +
                             " FROM SPEC_SUBSPEC LEFT JOIN SPEC_SOURCE ON SPEC_SUBSPEC.SPECID = SPEC_SOURCE.SPECID" +
                             " LEFT JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID = SPEC_SOURCE_STORE.SOTREID" +
                             " WHERE SPEC_SOURCE.DISEASETYPEID = " + det +
                             " AND SPEC_SOURCE_STORE.SPECTYPEID = " + specTypeList[index] + " AND SPEC_SOURCE_STORE.TUMORTYPE = '" + specIsCancerList[index] + "' ";
                sql += specOutList.Replace(",", "") == "" ? " AND SPEC_SUBSPEC.SUBSPECID IN (0) " : " AND SPEC_SUBSPEC.SUBSPECID IN " + specOutList1;
                string outCount = "";
                DataSet ds = new DataSet();
                applyTableManage.ExecQuery(sql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    outCount = dt.Rows[0]["OUTCOUNT"].ToString();
                    ucApplyDemand ucTmpDemand = new ucApplyDemand();
                    string disTypeTmp = disTypeManage.SelectDisByID(det).DiseaseName;
                    string specTypeTmp = specTypeManage.GetSpecTypeById(specTypeList[index]).SpecTypeName;
                    string specCancerTmp = "";
                    #region 癌种类
                    Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(specIsCancerList[index]));
                    switch (TumorType)
                    {
                        case Constant.TumorType.肿物:
                            specCancerTmp = "肿物";
                            break;
                        case Constant.TumorType.子瘤:
                            specCancerTmp = "子瘤";
                            break;
                        //case Constant.TumorType:
                        //    specCancerTmp = "肿瘤";
                        //    break;
                        case Constant.TumorType.癌旁:
                            specCancerTmp = "癌旁";
                            break;
                        case Constant.TumorType.癌栓:
                            specCancerTmp = "癌栓";
                            break;
                        case Constant.TumorType.正常:
                            specCancerTmp = "正常";
                            break;
                        case Constant.TumorType.淋巴结:
                            specCancerTmp = "淋巴结";
                            break;
                        default:
                            break;
                    }
                    #endregion
                    string specDetTmp = specDetAmountList[index];
                    ucTmpDemand.SetDemand("", specTypeTmp, specDetTmp, disTypeTmp, specCancerTmp, outCount);
                    flpApplyDemand.Controls.Add(ucTmpDemand);
                    index++;
                }
            }
        }

        public override int Query(object sender, object neuObject)
        {
            Query();
            tabResult.SelectTab("tpApply");
            return base.Query(sender, neuObject);
        }

        private void setFormat()
        {
            for (int i = 0; i < dgvResult.ColumnCount; i++)
            {
                if (i >= 5)
                {
                    dgvResult.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
            for (int j = 0; j < dgvResult.RowCount; j++)
            {
                if (dgvResult.Rows[j].Cells[13].Value.ToString() == "拒绝")
                {
                    dgvResult.Rows[j].DefaultCellStyle.BackColor = Color.LightPink;
                }
                if (dgvResult.Rows[j].Cells[13].Value.ToString() == "审批中")
                {
                    dgvResult.Rows[j].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        /// <summary>
        /// 申请单结果列表中的按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvResult.SelectedCells.Count > 0)
            {
                string type = dgvResult.SelectedCells[0].GetType().ToString();
                string text = dgvResult.SelectedCells[0].Value.ToString();
                //增加了一列申请号在第4列了
                curApplyNum = dgvResult.CurrentRow.Cells[5].Value.ToString();
                currentTable = applyTableManage.QueryApplyByID(curApplyNum);
                //申请单批准
                List<string> specList = new List<string>();
                List<string> returnableList = new List<string>();
                if (currentTable.ImpResult == "1")
                {
                    string[] outSpecList = currentTable.SpecList.Split(',');
                    string[] backList = currentTable.IsImmdBackList.Split(',');
                    int count = backList.Length > outSpecList.Length ? outSpecList.Length : backList.Length;
                    for (int i = 0; i < count; i++)
                    {
                        if (outSpecList[i].Trim() != "")
                        {
                            specList.Add(outSpecList[i]);
                            //可还的标本
                            if (backList[i].Trim() == "1")
                            {
                                returnableList.Add(outSpecList[i]);
                            }
                        }
                    }
                }
                if (type.Equals("System.Windows.Forms.DataGridViewButtonCell") && text == "详情")
                {
                    frmApplyTable frmApply = new frmApplyTable();
                    frmApply.ApplyNum = Convert.ToInt32(curApplyNum);
                    frmApply.ApplyOrImp = "Imp";
                    Size size = new Size();
                    size.Height = 700;
                    size.Width = 1000;
                    frmApply.Size = size;
                    frmApply.ApplyOrImp = "";
                    frmApply.IsState = "Query";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(frmApply, FormBorderStyle.FixedSingle, FormWindowState.Normal);

                }
                if (type.Equals("System.Windows.Forms.DataGridViewButtonCell") && text == "审批")
                {
                    //frmApplyTable frmApply = new frmApplyTable();
                    //frmApply.ApplyNum = Convert.ToInt32(curApplyNum);
                    //frmApply.ApplyOrImp = "Imp";
                    //Size size = new Size();
                    //size.Height = 700;
                    //size.Width = 1000;
                    //frmApply.Size = size;
                    //frmApply.ApplyOrImp = "";
                    //frmApply.IsState = "Confirm";
                    //FS.FrameWork.WinForms.Classes.Function.PopShowControl(frmApply, FormBorderStyle.FixedSingle, FormWindowState.Normal);
                    frmImplentSchedule frmLent = new frmImplentSchedule();
                    frmLent.AppTab = currentTable;
                    frmLent.StartPosition = FormStartPosition.CenterScreen;
                    if (!((Form)frmLent).Visible)
                    {
                        this.Focus();
                        frmLent.ShowDialog();
                    }
                }
                if (type.Equals("System.Windows.Forms.DataGridViewButtonCell") && text == "进度情况")
                {
                    frmApplySchedule frmSdl = new frmApplySchedule();
                    //Size size = new Size();
                    //size.Height = 700;
                    //size.Width = 1000;
                    //frmSdl.Size = size;
                    frmSdl.ApplyId = curApplyNum;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(frmSdl, FormBorderStyle.FixedSingle, FormWindowState.Normal);

                }
                if (type.Equals("System.Windows.Forms.DataGridViewButtonCell") && text.Contains("查看"))
                {
                    tabResult.SelectTab("tpSpec");
                    tpSpec.Controls.Clear();
                    //currentTable = applyTableManage.QueryApplyByID(curApplyNum);
                    ucOutSpecList ucOutList = new ucOutSpecList(loginPerson);
                    ucOutList.Dock = DockStyle.Fill;
                    ucOutList.ApplyID = curApplyNum;
                    //如果审批结束，显示已经出库的标本
                    if (currentTable.ImpProcess == "O")
                    {
                        string spec = "";
                        int index = 0;
                        if ((specList != null) && (specList.Count > 0))
                        {
                            foreach (string s2 in specList)
                            {
                                spec += s2;
                                if (index < specList.Count)
                                {
                                    spec += ",";
                                }
                                index++;
                            }
                            if (spec.Trim() == "")
                            {
                                return;
                            }
                        }
                        else //可能没有存进申请表的情况查找申请记录
                        {
                            ucOutList.QuerySpecListByApplyNum(curApplyNum);
                        }
                        ucOutList.QueryImpedOutSpec(spec);
                    }
                    else
                    {
                        ucOutList.QuerySpecListByApplyNum(curApplyNum);
                    }
                    tpSpec.Controls.Add(ucOutList);
                    QueryDemand(curApplyNum);
                }
                if (type.Equals("System.Windows.Forms.DataGridViewButtonCell") && text.Contains("归还"))
                {
                    tabResult.SelectTab("tpReturn");
                    tpReturn.Controls.Clear();
                    //currentTable = applyTableManage.QueryApplyByID(curApplyNum);
                    //申请单批准
                    if (currentTable.ImpResult == "1")
                    {
                        string specReturnable = "";
                        int index = 0;
                        foreach (string s1 in returnableList)
                        {
                            specReturnable += "'" + s1 + "'";
                            if (index < returnableList.Count - 1)
                            {
                                specReturnable += ",";
                            }
                            index++;
                        }
                        ucSpecReturn ucReturn = new ucSpecReturn();
                        string sql = " select SPEC_SUBSPEC.SUBSPECID, SPEC_SUBSPEC.SUBBARCODE, SPEC_SUBSPEC.SPECID,SPEC_SUBSPEC.STORETIME,\n" +
                                     " SPEC_TYPE.SPECIMENTNAME, SPEC_SUBSPEC.LASTRETURNTIME, SPEC_SUBSPEC.BOXID,SPEC_SUBSPEC.BOXENDCOL,\n" +
                                     " SPEC_SUBSPEC.BOXENDROW,SPEC_BOX.BLOODORORGID,SPEC_BOX.BOXBARCODE, SPEC_DISEASETYPE.DISEASENAME,\n" +
                                     " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) OUTCOUNT,\n" +
                                     "  SPEC_BOX.DESCAPROW,SPEC_BOX.DESCAPCOL,SPEC_BOX.DESCAPSUBLAYER,SPEC_SUBSPEC.SPECTYPEID,SPEC_BOX.DISEASETYPEID\n" +
                                     " from SPEC_SUBSPEC left join spec_box on SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID\n" +
                                     " left join spec_type on SPEC_SUBSPEC.SPECTYPEID= Spec_type.SPECIMENTTYPEID\n" +
                                     " left join spec_diseasetype on SPEC_BOX.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID";
                        if (specReturnable != "")
                        {
                            sql += " where SPEC_SUBSPEC.SUBBARCODE in (" + specReturnable + ")";
                        }
                        if (specReturnable == "")
                        {
                            sql += " where SPEC_SUBSPEC.SUBBARCODE in ('')";
                        }
                        ucReturn.Sql = sql;
                        ucReturn.DataBinding();
                        ucReturn.Dock = DockStyle.Fill;
                        tpReturn.Controls.Add(ucReturn);
                    }
                }
            }
        }

        private void ucImplent_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
        }

        private void chkImped_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImped.Checked)
            {
                dclReturnSpec.Visible = true;
            }
            if (!chkImped.Checked)
            {
                dclReturnSpec.Visible = false;
            }
        }
    }
}
