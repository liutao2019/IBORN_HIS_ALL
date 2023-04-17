using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.Case
{
    public partial class ucCaseStoreQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCaseStoreQuery()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe caseStoreMgr = new FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe();

        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        FS.HISFC.Models.HealthRecord.Case.CaseStore caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();

        FS.HISFC.BizLogic.HealthRecord.CaseCard cardMgr = new FS.HISFC.BizLogic.HealthRecord.CaseCard();
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

        private void txtCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.fpSpread_Sheet.RowCount = 0;
                this.fpSpread_Sheet2.RowCount = 0;
                this.fpSpread_Sheet3.RowCount = 0;
                this.Query();
                this.txtCaseNO.SelectAll();
            }
        }

        private void Query()
        {
            string caseNo = "";
            caseNo = this.txtCaseNO.Text.Trim();
            //add by chengym 东莞特殊处理
            if (caseNo.IndexOf('A') >= 0 || caseNo.IndexOf('B') >= 0 || caseNo.IndexOf('C') >= 0 || caseNo.IndexOf('D') >= 0 || caseNo.IndexOf('E') >= 0)
            {
                caseNo = caseNo.Replace('A', '0');
                caseNo = caseNo.Replace('B', '0');
                caseNo = caseNo.Replace('C', '0');
                caseNo = caseNo.Replace('D', '0');
                caseNo = caseNo.Replace('E', '0');
                caseNo = caseNo.TrimStart('0').PadLeft(6, '0');
            }
            //end
           
            caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
            //caseStore = this.caseStoreMgr.QueryCaseStore(CaseId.PadLeft(10, '0'), CaseInTimes);
            DataSet ds = new DataSet();

            this.caseStoreMgr.QueryCaseStoreByPatientNo(caseNo.PadLeft(10, '0'), ref ds);//获取库存信息  
  
            if (ds != null)
            {
                this.fpSpread_Sheet.DataSource = ds;
            }
            else
            {
                MessageBox.Show("该患者未做库房入库登记！", "提示");
                this.txtCaseNO.Text = "";
                this.txtCaseNO.Focus();
                return;
            }
            this.fpSpread_Sheet.Columns[3].BackColor = System.Drawing.Color.PeachPuff;
            this.QueryLendState(caseNo.PadLeft(10,'0'),"");//借阅信息查询
            this.QueryCallBack(caseNo,"");//回收信息查询
            this.txtCaseNO.Text = "";
            this.txtCaseNO.Focus();
        }
        /// <summary>
        /// 借阅信息查询
        /// </summary>
        /// <param name="caseNo"></param>
        ///<param name="type">根据姓名获取则不清空 </param>
        private void QueryLendState(string caseNo,string type)
        {
            ArrayList al = new ArrayList();
            if (type != "姓名")
            {
                al = this.cardMgr.QueryLendInfoByCaseNO(caseNo);
                if (al == null)
                {
                    return;
                }

                this.fpSpread_Sheet3.RowCount = 0;
            }
            else
            {
                al = this.cardMgr.QueryLendInfoByName(caseNo);
                if (al == null)
                {
                    return;
                }
            }
            foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
            {
                this.fpSpread_Sheet3.Rows.Add(this.fpSpread_Sheet3.RowCount, 1);
                int row = this.fpSpread_Sheet3.RowCount - 1;
                this.fpSpread_Sheet3.Cells[row, 0].Text = info.CaseBase.PatientInfo.ID;
                this.fpSpread_Sheet3.Cells[row, 1].Text = info.CaseBase.PatientInfo.Name;
                this.fpSpread_Sheet3.Cells[row, 2].Text = info.CaseBase.PatientInfo.PVisit.OutTime.Date.ToString();
                this.fpSpread_Sheet3.Cells[row, 3].Text = info.EmployeeInfo.Name;
                this.fpSpread_Sheet3.Cells[row, 4].Text = info.EmployeeDept.Name;
                this.fpSpread_Sheet3.Cells[row, 5].Text = info.LendDate.ToString();
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Models.NeuObject obj = con.GetConstant("CASE_LEND_TYPE", info.LendKind.ToString());
                if (obj != null && obj.ID != "")
                {
                    this.fpSpread_Sheet3.Cells[row, 6].Text = obj.Name;
                }
                else
                {
                    this.fpSpread_Sheet3.Cells[row, 6].Text = "";
                }
                TimeSpan d = con.GetDateTimeFromSysDateTime().Date - info.LendDate.Date;
                if (info.LendStus == "2")
                {
                    d = info.ReturnDate.Date - info.LendDate.Date;
                }
                this.fpSpread_Sheet3.Cells[row, 7].Text = d.Days.ToString();
                if (info.LendStus == "2")
                {
                    this.fpSpread_Sheet3.Cells[row, 8].Text = "已还";
                    this.fpSpread_Sheet3.Rows[row].BackColor = System.Drawing.Color.PeachPuff;
                }
                else
                {
                    this.fpSpread_Sheet3.Cells[row, 8].Text = "未还";
                }
            }
        }
        /// <summary>
        /// 回收信息查询
        /// </summary>
        /// <param name="caseNo"></param>
        ///<param name="type">根据姓名获取则不清空</param>
        private void QueryCallBack(string caseNo,string type)
        {
            if (type != "姓名")//直接传入中文姓名
            {
                caseNo = caseNo.TrimStart('0').PadLeft(10, '0');
            }
            else
            {
                caseNo=caseNo.Replace("%","");
            }
            FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack callBackMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack();
            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> cb = callBackMgr.QueryCaseHistorycallBackInfoByInpatientNO(caseNo);

            if (cb == null || cb.Count == 0)
            {
                //MessageBox.Show("未查找到住院号为" + caseNo + "的患者的信息");
                return;
            }
            if (type != "姓名")
            {
                this.fpSpread_Sheet2.RowCount = 0;
            }
            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack info in cb)
            {
                this.fpSpread_Sheet2.Rows.Add(this.fpSpread_Sheet2.RowCount, 1);
                int row = this.fpSpread_Sheet2.RowCount - 1;
                this.fpSpread_Sheet2.Cells[row, 0].Text = info.Patient.PID.PatientNO;//住院号
                this.fpSpread_Sheet2.Cells[row, 1].Text = info.Patient.Name;//姓名
                this.fpSpread_Sheet2.Cells[row, 2].Text = info.Patient.PVisit.OutTime.ToShortDateString();//出院日期
                if (info.IsCallback.ToString() == "0")//回收状态
                {
                    this.fpSpread_Sheet2.Cells[row, 3].Text = "未回收";
                }
                else
                {
                    this.fpSpread_Sheet2.Cells[row, 3].Text = "已回收";                    
                }
                this.fpSpread_Sheet2.Cells[row, 4].Text = info.Patient.PVisit.PatientLocation.Dept.Name;//所属科室名称
                this.fpSpread_Sheet2.Cells[row, 5].Text = info.Patient.PVisit.AdmittingDoctor.Name;//医生名称
                this.fpSpread_Sheet2.Cells[row, 6].Text = info.CallbackOper.Name;//回收人姓名
                this.fpSpread_Sheet2.Cells[row, 7].Text = info.CallbackOper.OperTime.ToShortDateString();//回收日期
            }
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            this.fpSpread_Sheet.RowCount = 0;
            if (this.txtCaseNO.Text != "")
            {
                this.Query();
            }
            else if (this.txtName.Text != "")
            {
                this.QueryByName();
            }
            else
            {
                this.txtCaseNO.Text = "";
                this.txtCaseNO.Focus();
                return;
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.fpSpread_Sheet.RowCount = 0;
                this.fpSpread_Sheet2.RowCount = 0;
                this.fpSpread_Sheet3.RowCount = 0;
                this.QueryByName();
            }
        }

        private void QueryByName()
        {
            string caseNo = "";
            caseNo = this.txtName.Text.Trim();
            caseNo = "%" + caseNo + "%";
            caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
  
            DataSet ds = new DataSet();

            this.caseStoreMgr.QueryCaseStoreByPatientName(caseNo, ref ds);

            if (ds != null)
            {
                this.fpSpread_Sheet.DataSource = ds;
            }
            else
            {
                MessageBox.Show("该患者未做库房入库登记！", "提示");
                this.txtName.Focus();
                return;
            }
             string[] str ={ "病案号"};

            DataView dv = new DataView(ds.Tables[0]);
            DataTable dt = dv.ToTable(true, str);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow drow in dt.Rows)
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = drow["病案号"].ToString();
                    this.QueryLendState(obj.ID.PadLeft(10, '0'), "姓名");
                    this.QueryCallBack(obj.ID, "姓名");
                }
            }
            else
            {
                this.QueryLendState(caseNo, "姓名");

                this.QueryCallBack(caseNo, "姓名");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void ucCaseStoreQuery_Load(object sender, EventArgs e)
        {
            this.txtCaseNO.Text = "";
            this.txtCaseNO.Focus();
        }
    }
}
