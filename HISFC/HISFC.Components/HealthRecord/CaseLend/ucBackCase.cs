using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucBackCase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 归还
        /// </summary>
        public ucBackCase()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();   //部门业务类
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();//常数业务类
        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();//人员业务类
        FS.HISFC.BizProcess.Integrate.RADT radtMana = new FS.HISFC.BizProcess.Integrate.RADT();//住院患者业务类
        FS.HISFC.BizLogic.HealthRecord.CaseCard cardMgr = new FS.HISFC.BizLogic.HealthRecord.CaseCard();//借阅业务类

        FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe caseStoreMgr = new FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe();

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private bool isUserCaseStore = false;

        /// <summary>
        /// 是否使用库房管理
        /// </summary>
        [Category("是否使用库房管理"), Description("是借出默认出库，归还默认入库")]
        public bool IsUserCaseStore
        {
            get { return this.isUserCaseStore; }
            set { this.isUserCaseStore = value; }
        }

        /// <summary>
        /// 是否电子病历借阅 医生使用的申请功能 2011-8-10 by chengym
        /// </summary>
        private bool isElectronCase = false;

        /// <summary>
        /// 是否电子病历借阅属性
        /// </summary>
        [Category("是否电子病历借阅申请"), Description("处理电子病历借阅申请")]
        public bool IsElectronCase
        {
            get { return this.isElectronCase; }
            set { this.isElectronCase = value; }
        }
        /// <summary>
        /// 电子病历借阅类型 2011-8-10 by chengym
        /// </summary>
        private FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType lendCaseType = FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.HealthRecordVerify;

        /// <summary>
        /// 电子病历借阅类型属性
        /// </summary>
        [Category("电子病历借阅类型"), Description("电子病历借阅类型")]
        public FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType LendCaseType
        {
            get { return this.lendCaseType; }
            set { this.lendCaseType = value; }
        }


        private int maxLendDays = 30;

        /// <summary>
        /// 电子病历借阅
        /// </summary>
        [Category("最大借阅天数"), Description("电子病历的借阅天数，超过最大天数，自动归还不允许查询")]
        public int MaxLendDays
        {
            get { return this.maxLendDays; }
            set { this.maxLendDays = value; }
        }

        /// <summary>
        ///  加载按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            try
            {
                toolBarService.AddToolButton("清空", "清空", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
                toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误信息");
            }
            return toolBarService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.init();
            this.cmbOper.Tag = this.conMgr.Operator.ID;//经手人
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.Return)
            {
                label4.Text = "归还日期";
            }
            else
            {
                label2.Visible = false;
                label4.Text = "审核日期";
                this.caseDetail.Columns.Get(1).Label = "审核";
            }
            this.GetNeedBack(this.lendCaseType);
            base.OnLoad(e);
        }
        /// <summary>
        ///  初始化下拉列表
        /// </summary>
        private void init()
        {
            //科室
            ArrayList deptAl = this.deptMgr.GetDeptmentAll();
            this.cmbDept.AddItems(deptAl);
            //人员
            ArrayList personAl = this.personMgr.GetUserEmployeeAll();
            this.cmbPerson.AddItems(personAl);
            this.cmbOper.AddItems(personAl);
            //借阅用途
            ArrayList LendTypeAl = this.conMgr.GetAllList("CASE_LEND_TYPE");
            this.cmbLendType.AppendItems(LendTypeAl);
        }
        /// <summary>
        ///  查询当前借阅类型的所有数据
        /// <param name="Type">借阅类型</param>
        /// </summary>
        private void GetNeedBack(FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType Type)
        {
            List<FS.HISFC.Models.HealthRecord.Lend> needBackAl = this.cardMgr.QueryNeedBack(Type);
            if (needBackAl == null || needBackAl.Count == 0)
            {
                this.fpSpread1_Sheet1.RowCount = 0;
                return;
            }
            this.fpSpread1_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.HealthRecord.Lend info in needBackAl)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.Cells[row, 0].Text = info.CardNO;
                this.fpSpread1_Sheet1.Cells[row, 1].Text = info.EmployeeInfo.Name;
                this.fpSpread1_Sheet1.Cells[row, 2].Text = info.EmployeeDept.Name;
                this.fpSpread1_Sheet1.Cells[row, 3].Text = info.LendDate.ToShortDateString();
                this.fpSpread1_Sheet1.Rows[row].Tag = info;
            }
        }
        /// <summary>
        /// 根据住院号查询
        /// </summary>
        private void QueryByPatientNo()
        {
            //晕 借出是按照住院号加住院次数的，归还居然 忘记了自动保存的功能需要具体到住院次数了
            string where = string.Empty;
            string paitenNO = string.Empty;//住院号
            int inTimes = 1;//住院次数

            string caseNo = this.txtPatientNo.Text.Trim();

            if (caseNo.IndexOf('A') >= 0 || caseNo.IndexOf('B') >= 0 || caseNo.IndexOf('C') >= 0 || caseNo.IndexOf('D') >= 0 || caseNo.IndexOf('E') >= 0)
            {
                caseNo = caseNo.Replace('A', '0');
                caseNo = caseNo.Replace('B', '0');
                caseNo = caseNo.Replace('C', '0');
                caseNo = caseNo.Replace('D', '0');
                caseNo = caseNo.Replace('E', '0');
                caseNo = caseNo.TrimStart('0').PadLeft(10, '0');
            }
            //end
            caseNo = caseNo.Replace('—', '-');
            if (caseNo.IndexOf('-') > 0)
            {
                string[] CaseNO = caseNo.Split('-');
                caseNo = CaseNO[0].ToString().PadLeft(10, '0');

                paitenNO = caseNo;
                inTimes = FS.FrameWork.Function.NConvert.ToInt32(CaseNO[1].ToString().Trim());
            }
            else
            {
                paitenNO = caseNo.PadLeft(10, '0');//不带-次数情况
            }

            if (this.isElectronCase == false)
            {
                where = "  WHERE PATIENT_NO='{0}' AND in_times={1}  AND LEN_STUS='{2}' ORDER BY EMPL_CARDNO";

                where = string.Format(where, paitenNO, inTimes.ToString(), (int)this.lendCaseType);

                ArrayList al = this.cardMgr.QueryLendInfoSetWhere(where);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("找不到借阅信息", "提示");
                    this.txtPatientNo.Text = "";
                    this.txtPatientNo.Focus();
                    return;
                }
                //add by chengym 2011-6-28需要默认保存
                this.AutoSave(al);

                //end add
                foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
                {
                    string whereSQL = "  WHERE PATIENT_NO='{0}'  AND in_times={1}  ORDER BY EMPL_CARDNO";
                    whereSQL = string.Format(whereSQL, info.CaseBase.CaseNO, info.CaseBase.PatientInfo.InTimes.ToString());
                    ArrayList altemp = this.cardMgr.QueryLendInfoSetWhere(whereSQL);
                    foreach (FS.HISFC.Models.HealthRecord.Lend Lendinfo in altemp)
                    {
                        for (int row = 0; row < this.caseDetail.RowCount; row++)
                        {
                            FS.HISFC.Models.HealthRecord.Lend lendInfo = this.caseDetail.Rows[row].Tag as FS.HISFC.Models.HealthRecord.Lend;
                            if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.Return && lendInfo.LendStus == "1")
                            {
                                this.caseDetail.Rows[row].Remove();
                            }
                            else if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.MedicalMattersVerify && lendInfo.LendStus == "3")
                            {
                                this.caseDetail.Rows[row].Remove();
                            }
                            else if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.HealthRecordVerify && lendInfo.LendStus == "4")
                            {
                                this.caseDetail.Rows[row].Remove();
                            }
                        }
                        this.SetCaseDetail(Lendinfo);
                    }
                }
            }
            else
            {
                where = "  WHERE PATIENT_NO='{0}'   AND LEN_STUS='{1}' ORDER BY EMPL_CARDNO";

                where = string.Format(where, paitenNO, (int)this.lendCaseType);

                ArrayList al = this.cardMgr.QueryLendInfoSetWhere(where);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("找不到借阅信息", "提示");
                    this.txtPatientNo.Text = "";
                    this.txtPatientNo.Focus();
                    return;
                }

                this.caseDetail.RowCount = 0;
                foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
                {
                    this.SetCaseDetail(info);
                }
            }
            this.GetNeedBack(this.lendCaseType);
            this.txtPatientNo.Text = "";
            this.txtPatientNo.Focus();
        }
        /// <summary>
        /// 根据姓名查询
        /// </summary>
        private void QueryByName()
        {
            string where = "  WHERE name='{0}'  AND LEN_STUS='{1}' ORDER BY EMPL_CARDNO";
            string pName = this.txtPatientNo.Text.Trim();

            where = string.Format(where, pName,(int)this.lendCaseType);
            this.Clear();
            ArrayList al = this.cardMgr.QueryLendInfoSetWhere(where);
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("找不到借阅信息", "提示");
                return;
            }
            this.caseDetail.RowCount = 0;
            foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
            {
                this.SetCaseDetail(info);
            }
        }
        /// <summary>
        /// 住院号＋次数查询出来自动保存
        /// </summary>
        /// <param name="al"></param>
        private void AutoSave(ArrayList al)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.cardMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.caseStoreMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.HealthRecord.Lend lendCase in al)
            {
               
                if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.Return)
                {
                    lendCase.LendStus = "2";//病历状态 1借出/2返还
                    lendCase.ReturnOperInfo.ID = this.cmbOper.Tag.ToString();   //归还操作员代号
                    lendCase.ReturnDate = this.dtBackDate.Value;   //实际归还日期
                }
                else if(this.lendCaseType==FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.MedicalMattersVerify)
                {
                    lendCase.LendStus = "4";
                    lendCase.OperInfo.ID = this.cmbOper.Tag.ToString();   //操作员代号
                    lendCase.OperInfo.OperTime = this.dtBackDate.Value;   //
                }
                else if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.HealthRecordVerify)
                {
                    lendCase.LendStus = "1";
                    lendCase.OperInfo.ID = this.cmbOper.Tag.ToString();   //操作员代号
                    lendCase.OperInfo.OperTime = this.dtBackDate.Value;   //
                }


                if (this.cardMgr.UpdateLendInfo(lendCase) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("返还保存失败！", "提示");
                    return;
                }
                #region 借阅操作同时 做库房的出库操作--如果两个步骤是分开的可以屏蔽下面代码 2011-6-23 chengym

                if (this.isUserCaseStore && this.isElectronCase==false)
                {
                    FS.HISFC.Models.HealthRecord.Case.CaseStore info = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                    info.PatientInfo.PID.PatientNO = lendCase.CaseBase.CaseNO;//住院号
                    info.PatientInfo.Name = lendCase.CaseBase.PatientInfo.Name;
                    info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(lendCase.CaseBase.PatientInfo.PID.CardNO);//次数
                    FS.HISFC.Components.HealthRecord.Case.functionStore fun = new FS.HISFC.Components.HealthRecord.Case.functionStore();
                    info.Store.ID = fun.GetCaseStore(lendCase.CaseBase.CaseNO.TrimStart('0'));
                    info.Cabinet.ID = fun.GetCabinet(lendCase.CaseBase.CaseNO.TrimStart('0'));
                    info.Grid.ID = "";
                    info.CaseState = "4";
                    info.IsVaild = true;
                    info.CaseMemo = "";
                    info.OperEnv.ID = lendCase.ID;
                    info.OperEnv.OperTime = lendCase.ReturnDate;
                    info.Extend1 = "";
                    info.Extend2 = "";
                    info.Extend3 = "";
                    info.Extend4 = "";
                    if (this.caseStoreMgr.InsertCaseStore(info) < 0)
                    {
                        if (this.caseStoreMgr.UpdateCaseStore(info) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存病案库房信息失败！", "提示");
                            return;
                        }
                    }
                    string CaseNum = this.caseStoreMgr.GetSequence("HealthReacord.Case.CaseStore.Seq");
                    if (this.caseStoreMgr.InsertCaseStoreDetail(info, CaseNum) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存病案库房明细信息失败！", "提示");
                        return;
                    }
                }
                #endregion
              
            }
            FS.FrameWork.Management.PublicTrans.Commit();

        }
        /// <summary>
        /// 根据科室查询
        /// </summary>
        private void QueryByDept()
        {
            string where = "   WHERE DEPT_CODE='{0}'  AND LEN_STUS='{1}'  ORDER BY EMPL_CARDNO";
            if (this.cmbDept.Tag != null)
            {
                where = string.Format(where, this.cmbDept.Tag.ToString(),(int)this.lendCaseType);
            }
            this.Clear();
            ArrayList al = this.cardMgr.QueryLendInfoSetWhere(where);
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("找不到借阅信息", "提示");
                return;
            }
            this.caseDetail.RowCount = 0;
            foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
            {
                this.SetCaseDetail(info);
            }
        }
        /// <summary>
        /// 根据借阅人查询　
        /// </summary>
        private void QueryByPerson(FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType Type)
        {
            string where = "  WHERE EMPL_CODE='{0}'  AND LEN_STUS='{1}' ORDER BY EMPL_CARDNO";
            if (this.cmbPerson.Tag != null)
            {
                where = string.Format(where, this.cmbPerson.Tag.ToString(), (int)Type);
            }
            this.Clear();
            ArrayList al = this.cardMgr.QueryLendInfoSetWhere(where);
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("找不到借阅信息", "提示");
                return;
            }
            this.caseDetail.RowCount = 0;
            foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
            {
                this.SetCaseDetail(info);
            }
        }
        /// <summary>
        /// 根据借阅号－－自动生成序列
        /// </summary>
        /// <param name="CaseNO"></param>
        private void QueryCaseLendByCaseNO(string CaseNO)
        {
            string WHERE = " where  EMPL_CODE='{0}' AND LEN_STUS ='{1}'";
            
            WHERE = string.Format(WHERE, CaseNO,(int)lendCaseType);
            ArrayList al = this.cardMgr.QueryLendInfoSetWhere(WHERE);
            this.Clear();
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("找不到借阅信息", "提示");
                return;
            }
            foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
            {
                this.SetCaseDetail(info);
            }
        }
        /// <summary>
        /// 界面赋值
        /// </summary>
        /// <param name="info"></param>
        private void SetCaseDetail(FS.HISFC.Models.HealthRecord.Lend info)
        {
            //this.cmbPerson.Tag = info.EmployeeInfo.ID;
            //this.cmbDept.Tag = info.EmployeeDept.ID;
            //this.cmbLendType.Tag = info.LendKind;

            this.caseDetail.Rows.Add(this.caseDetail.RowCount, 1);
            int row = this.caseDetail.RowCount - 1;
            this.caseDetail.Cells[row, 0].Text = info.CardNO;
            if (this.lendCaseType==FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.Return && info.LendStus == "2")//病历状态 1借出/2返还
            {
                this.caseDetail.Cells[row, 0].BackColor = System.Drawing.Color.Red;
                this.caseDetail.Cells[row, 1].Text = "true";
            }
            else if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.MedicalMattersVerify && info.LendStus == "4")
            {
                this.caseDetail.Cells[row, 0].BackColor = System.Drawing.Color.Red;
                this.caseDetail.Cells[row, 1].Text = "true";
            }
            else if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.HealthRecordVerify && info.LendStus == "1")
            {
                this.caseDetail.Cells[row, 0].BackColor = System.Drawing.Color.Red;
                this.caseDetail.Cells[row, 1].Text = "true";
            }
            else
            {
                this.caseDetail.Cells[row, 1].Text = "false";
            }
            this.caseDetail.Cells[row, 2].Text = info.CaseBase.CaseNO;//病案号
            this.caseDetail.Cells[row, 3].Text = info.CaseBase.PatientInfo.InTimes.ToString();//住院次数
            this.caseDetail.Cells[row, 4].Text = info.CaseBase.PatientInfo.Name;//患者姓名
            this.caseDetail.Cells[row, 5].Text = info.CaseBase.PatientInfo.PVisit.OutTime.ToShortDateString();//出院日期
            this.caseDetail.Columns[5].Visible = false;
            this.caseDetail.Cells[row, 6].Text = info.LendDate.ToShortDateString();
            this.caseDetail.Cells[row, 7].Text = info.EmployeeInfo.Name;
            this.caseDetail.Cells[row, 8].Text = info.EmployeeDept.Name;
            this.caseDetail.Cells[row, 9].Text = this.conMgr.GetConstant("CASE_LEND_TYPE", info.LendKind).Name;
            if (info.CaseBase.PatientInfo.Sex.ID.ToString() == "M")
            {
                this.caseDetail.Cells[row, 10].Text = "男";
            }
            else
            {
                this.caseDetail.Cells[row, 10].Text = "女";
            }
            this.caseDetail.Cells[row, 11].Text = info.CaseBase.PatientInfo.Birthday.ToShortDateString();
            this.caseDetail.Columns[12].Visible = false;
            this.caseDetail.Columns[13].Visible = false;
            this.caseDetail.Columns[14].Visible = false;
            this.caseDetail.Columns[15].Visible = false;

            this.caseDetail.Cells[row, 16].Text = info.LendNum;
            this.caseDetail.Rows[row].Tag = info;
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        private void QueryCondition()
        {
            if (this.cmbPerson.Tag != null && this.cmbPerson.Tag.ToString() != "")
            {
                this.QueryByPerson(this.lendCaseType);
            }
            else if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
            {
                this.QueryByDept();
            }
            else if (this.txtPatientNo.Text.Trim() != "")
            {
                if (linkLabel1.Text == "病案号")
                {
                    this.QueryByPatientNo();
                }
                else if (linkLabel1.Text == "姓名")
                {
                    this.QueryByName();
                }
            }
            else if (this.txtNo.Text.Trim() != "")
            {
                this.QueryCaseLendByCaseNO(this.txtNo.Text.Trim());
            }
        }

        private void txtPatientNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (linkLabel1.Text == "病案号")
                {
                    this.QueryByPatientNo();
                }
                else if (linkLabel1.Text == "姓名")
                {
                    this.QueryByName();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text == "病案号")
            {
                linkLabel1.Text = "姓名";
            }
            else if (linkLabel1.Text == "姓名")
            {
                linkLabel1.Text = "病案号";
            }
        }

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryCaseLendByCaseNO(this.txtNo.Text.Trim());
            }
        }

        private void cmbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPerson.Tag != null && cmbPerson.Tag.ToString()!="")
            {
                this.QueryByPerson(this.lendCaseType);
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDept.Tag != null && cmbDept.Tag.ToString()!="")
            {
                this.QueryByDept();
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.QueryCaseLendByCaseNO(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim());
        }
        /// <summary>
        /// 多条件查询　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {

            this.GetNeedBack(this.lendCaseType);
           
            this.QueryCondition();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 保存获取实体数据前 判断必填项是否空缺
        /// </summary>
        /// <returns></returns>
        private bool Vaild()
        {
            if (this.cmbPerson.Tag == null && this.cmbPerson.Tag.ToString() == "")
            {
                MessageBox.Show("请选择借阅人");
                this.cmbPerson.Focus();
                return false;
            }
            if (this.cmbDept.Tag == null && this.cmbDept.Tag.ToString() == "")
            {
                MessageBox.Show("请选择借阅人所在科室");
                this.cmbDept.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (this.caseDetail.RowCount < 0)
            {
                return;
            }
            if (this.Vaild() == false)
            {
                return;
            }

            ArrayList al = new ArrayList();
            //ArrayList alEmr = new ArrayList();
            for (int i = 0; i < this.caseDetail.RowCount; i++)
            {
                if (this.caseDetail.Cells[i, 1].Text == "False")
                {
                    continue;
                }
                FS.HISFC.Models.HealthRecord.Lend lendCaseInfo = this.caseDetail.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Lend;
                //FS.HISFC.Models.HealthRecord.Lend lendEmrCommit = new FS.HISFC.Models.HealthRecord.Lend();
                if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.Return)
                {
                    lendCaseInfo.LendStus = "2";//病历状态 1借出/2返还
                    lendCaseInfo.ReturnOperInfo.ID = this.cmbOper.Tag.ToString();   //归还操作员代号
                    lendCaseInfo.ReturnDate = this.dtBackDate.Value;   //实际归还日期

                    //lendEmrCommit.ID = lendCaseInfo.CaseBase.PatientInfo.ID;
                    //lendEmrCommit.LendStus = "3";
                    //lendEmrCommit.LendDate = this.dtBackDate.Value;
                    //lendEmrCommit.PrerDate = this.dtBackDate.Value.AddDays(maxLendDays);
                }
                else if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.MedicalMattersVerify)
                {
                    lendCaseInfo.LendStus = "4";
                    lendCaseInfo.OperInfo.ID = this.cmbOper.Tag.ToString();   //操作员代号
                    lendCaseInfo.OperInfo.OperTime = this.dtBackDate.Value;   //
                }
                else if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.HealthRecordVerify)
                {
                    lendCaseInfo.LendStus = "1";
                    lendCaseInfo.OperInfo.ID = this.cmbOper.Tag.ToString();   //操作员代号
                    lendCaseInfo.OperInfo.OperTime = this.dtBackDate.Value;   //

                    //lendEmrCommit.ID = lendCaseInfo.CaseBase.PatientInfo.ID;
                    //lendEmrCommit.LendStus = "4";
                    //lendEmrCommit.LendDate = this.dtBackDate.Value;
                    //lendEmrCommit.PrerDate = this.dtBackDate.Value.AddDays(maxLendDays);

                }

                al.Add(lendCaseInfo);
                //alEmr.Add(lendEmrCommit);

            }
            if (al.Count < 1)
            {
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.cardMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.caseStoreMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.HealthRecord.Lend lendCase in al)
            {
                if (this.cardMgr.UpdateLendInfo(lendCase) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("返还保存失败！", "提示");
                    return;
                }
                #region 借阅操作同时 做库房的出库操作--如果两个步骤是分开的可以屏蔽下面代码 2011-6-23 chengym
                if (this.isUserCaseStore && this.isElectronCase==false)
                {
                    FS.HISFC.Models.HealthRecord.Case.CaseStore info = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                    info.PatientInfo.PID.PatientNO = lendCase.CaseBase.CaseNO;//住院号
                    info.PatientInfo.Name = lendCase.CaseBase.PatientInfo.Name;
                    info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(lendCase.CaseBase.PatientInfo.PID.CardNO);//次数
                    FS.HISFC.Components.HealthRecord.Case.functionStore fun = new FS.HISFC.Components.HealthRecord.Case.functionStore();
                    info.Store.ID = fun.GetCaseStore(lendCase.CaseBase.CaseNO.TrimStart('0'));
                    info.Cabinet.ID = fun.GetCabinet(lendCase.CaseBase.CaseNO.TrimStart('0'));
                    info.Grid.ID = "";
                    info.CaseState = "4";
                    info.IsVaild = true;
                    info.CaseMemo = "";
                    info.OperEnv.ID = lendCase.ID;
                    info.OperEnv.OperTime = lendCase.ReturnDate;
                    info.Extend1 = "";
                    info.Extend2 = "";
                    info.Extend3 = "";
                    info.Extend4 = "";
                    if (this.caseStoreMgr.InsertCaseStore(info) < 0)
                    {
                        if (this.caseStoreMgr.UpdateCaseStore(info) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存病案库房信息失败！", "提示");
                            return;
                        }
                    }
                    string CaseNum = this.caseStoreMgr.GetSequence("HealthReacord.Case.CaseStore.Seq");
                    if (this.caseStoreMgr.InsertCaseStoreDetail(info, CaseNum) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存病案库房明细信息失败！", "提示");
                        return;
                    }
                }
                #endregion
            }

            if (this.isElectronCase)
            {
                //借阅不处理电子病历提交表状态；考虑针对医生借阅，电子病历有可能面对多个医生的情况，与电子病历直接获取借阅信息来处理2012-6-5chengym
                //foreach (FS.HISFC.Models.HealthRecord.Lend lendEmr in alEmr)
                //{
                //    if (this.cardMgr.UpdateEmrQcCommit(lendEmr) < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("电子病历借阅保存失败！", "提示");
                //        return;
                //    }
                //}
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            if (this.lendCaseType == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.Return)
            {
                MessageBox.Show("返还保存成功！", "提示");
            }
            else
            {
                MessageBox.Show("审核成功！", "提示");
            }
            this.Clear();
        }
        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            txtNo.Text = "";
            this.txtPatientNo.Text = "";
            this.cmbPerson.Tag = "";
            this.cmbDept.Tag = "";
            this.cmbLendType.Tag = "";
            this.dtBackDate.Value = this.cardMgr.GetDateTimeFromSysDateTime();
            this.cmbOper.Tag = this.cardMgr.Operator.ID;
            this.caseDetail.RowCount = 0;
            this.GetNeedBack(this.lendCaseType);
            
        }
        
        /// <summary>
        /// 工具栏单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清空":
                    this.Clear();
                    break;
                case "删除":
                    if (this.caseDetail.Cells[this.caseDetail.ActiveRowIndex, 0].Text != "")
                    {
                        this.caseDetail.ActiveRow.Remove();
                    }
                    break;                    
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
    }
}