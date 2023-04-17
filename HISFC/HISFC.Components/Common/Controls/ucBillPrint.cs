using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucBillPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBillPrint()
        {
            InitializeComponent();
            this.Init();

        }
        /// <summary>
        /// 清单打印接口
        /// </summary>
        public  FS.HISFC.BizProcess.Interface.RADT.IBillPrint IBillPrint = null;


        /// <summary>
        /// 患者实体
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 患者信息管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 费用信息管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 显示的清单类型
        /// </summary>
        private int showIBill = 1;

        private void ucBillPrint_Load(object sender, EventArgs e)
        {
            Init();
        }
        /// <summary>
        /// 显示的清单类型
        /// </summary>
        [Category("控件设置"), Description("显示的清单类型,1:清单汇总、2：清单明细、3：一日清单、4：清单汇总（包括护士站）")]
        public int ShowIBill
        {
            set
            {
                this.showIBill = value;
            }
            get
            {
                return this.showIBill;
            }
        }

        [Category("控件设置"), Description("设置查询树的可见性")]
        public bool TreeLeftVisible
        {
            get
            {
                return this.pLeft.Visible;
            }
            set
            {
                this.pLeft.Visible = value;
            }
        }

        public void Init()
        {
            if (this.ShowIBill == 1)
            {
                this.panel4.Visible = false;
                this.panel2.Visible = false;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint), 1) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;

            }
            else if (this.ShowIBill == 2)
            {
                this.panel4.Visible = false;
                this.panel2.Visible = false;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint),2) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;
            }
            else if (this.ShowIBill == 3)
            {
                this.panel4.Visible = true;
                this.panel2.Visible = false;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint), 3) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;

            }
            else if (this.ShowIBill == 4)
            {
                this.panel4.Visible = false;
                this.panel2.Visible = true;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint), 4) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;

            }
            else if (this.showIBill == 5)//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加小孩费用清单汇总
            {
                this.panel4.Visible = false;
                this.panel2.Visible = false;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint), 1) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;
            }
            else if (this.showIBill == 6)////{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加小孩费用清单汇总
            {
                this.panel4.Visible = false;
                this.panel2.Visible = false;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint), 1) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;
            }
            else if (this.showIBill == 7)  //{34a15202-a3f9-4d3e-9bad-c7e6783b540c}
            {
                this.panel4.Visible = false;
                this.panel2.Visible = false;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint), 2) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;
            }
            else if (this.showIBill == 8)//费用清单汇总(折后价=医院折后-医保价)
            {
                this.panel4.Visible = false;
                this.panel2.Visible = false;
                IBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBillPrint), 8) as FS.HISFC.BizProcess.Interface.RADT.IBillPrint;
            }

            if (IBillPrint == null)
            {
                MessageBox.Show("初始化接口失败！FS.HISFC.BizProcess.Interface.RADT.IBillPrint");
                return;
            }
            this.dtBegTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(DateTime.Now.ToShortDateString());
            
            if (this.IBillPrint is Control)
            {
                this.gbBillInfo.Controls.Clear();
                //加载界面
                ((Control)this.IBillPrint).Dock = DockStyle.Fill;
                this.gbBillInfo.Controls.Add((Control)this.IBillPrint);
            }
            this.ucQueryInfo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInfo_myEvent);

            //this.gbQuery.Controls.Clear();
            //ucQueryInpatientNo ucQueryInpatientNo = new ucQueryInpatientNo();
            //ucQueryInpatientNo.Location = new Point(21, 20);
            //ucQueryInpatientNo.Dock = DockStyle.None;
            //this.gbQuery.Controls.Add(ucQueryInpatientNo);
            this.trvPatients.Nodes.Clear();

            //List<FS.HISFC.Models.RADT.PatientInfo> pat = new List<FS.HISFC.Models.RADT.PatientInfo>();
            //pat.Add(this.patientInfo);
            //this.AddPatientsToTree(pat);
            //this.gbTreeView.Controls.Clear();
        }


        /// <summary>
        /// 添加发票信息到树
        /// </summary>
        /// <param name="lstInvoice"></param>
        private void AddPatientsToTree(List<FS.HISFC.Models.RADT.PatientInfo> lstPatients)
        {
            this.trvPatients.Nodes.Clear();
            if (lstPatients == null || lstPatients.Count <= 0)
                return;

            TreeNode tn = null;
            tn = new TreeNode();
            tn.Name = "";
            tn.Text = "患者信息";
            tn.Tag = "患者信息";
            trvPatients.Nodes.Add(tn);
            foreach (FS.HISFC.Models.RADT.PatientInfo obj in lstPatients)
            {
                AddPatientsToTree(obj);
            }

            this.trvPatients.ExpandAll();
        }
        /// <summary>
        /// 添加到树
        /// {7A484F24-EFB0-414d-8C25-F89D51BC4846}
        /// </summary>
        /// <param name="invoice"></param>
        private void AddPatientsToTree(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
                return;

            //TreeNode[] tnArr = trvPatients.Nodes.Find(patientInfo.ID, true);

            TreeNode tn = null;
            TreeNode tnTemp = null;


            tn = new TreeNode();
            tn.Name = patientInfo.ID;
            tn.Text = patientInfo.Name;
            tn.Tag = patientInfo;
            trvPatients.Nodes.Add(tn);

            tn.Nodes.Add(tnTemp);

        }
        private void ILeftQueryCondition_OnFilterHandler(string filterStr)
        {
        }


        protected override int OnQuery(object sender, object neuObject)
        {
            if (string.IsNullOrEmpty(this.lblInvioceNo.Text))
            {
                this.ucQueryInfo_myEvent();
            }
            else
            {

                this.QueryByInvoiceNo();
            }
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (IBillPrint != null)
            {
                IBillPrint.PrintPreview();
            }
            return 1;
        }

        /// <summary>
        /// 导出{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            if (IBillPrint != null&&this.patientInfo!=null)
            {
                IBillPrint.Export(patientInfo);
            }
            return 1;
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        private void ucQueryInfo_myEvent()
        {
            string errText = "";
            if (this.QueryByPatientNO(this.ucQueryInfo.InpatientNo, ref errText) < 0)
            {
                IBillPrint.Clear();
                //MessageBox.Show(errText);
                this.ucQueryInfo.Focus();
            }
        }
        /// <summary>
        /// 住院号回车处理
        /// </summary>
        protected virtual int QueryByPatientNO(string inpatientNO, ref string errText)
        {
            try
            {
                IBillPrint.Clear();
                if (string.IsNullOrEmpty(inpatientNO.Trim()))
                {
                    throw new Exception("住院号错误，没有找到该患者");
                }

                this.patientInfo = this.radtMgr.QueryPatientInfoByInpatientNO(inpatientNO);
                this.patientInfo.User01 = string.Empty;
                this.ucQueryInfo.Text = this.patientInfo.PID.PatientNO;
                //if (this.patientInfo.PVisit.OutTime < new DateTime(1900, 1, 1))
                //{
                //    throw new Exception("该患者未做出院登记，请联系护士站！");
                //}
                string invoiceNo = string.Empty;
                string nurseCode = string.Empty;
                ArrayList alData = new ArrayList();
                DataTable dt = new DataTable();
                if (showIBill == 1)
                {
                    alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                    // 给要导出的控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
                    //dt = this.feeMgr.GetPatientDetailFeeDTInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                    //导出控件绑定数据源，根据医保需求更改编码{940D2882-F02B-488f-A8E3-07689B0D064D}
                    dt = this.feeMgr.GetPatientTotalFeeDTInfoByInPatientNOHospitalDrugNo(this.patientInfo.ID, invoiceNo);
                    IBillPrint.ShowData(dt, this.patientInfo);
                }
                else if (showIBill == 2)
                {
                    alData = this.feeMgr.GetPatientDetailFeeListInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                }
                else if (showIBill == 3)
                {
                    alData = this.feeMgr.GetPatientOneDayFeeListInfoByInPatientNO(this.patientInfo.ID, invoiceNo,this.dtBegTime.Value.ToShortDateString());
                }
                else if (showIBill == 4)
                {
                    nurseCode = "ALL";
                    this.cmbNurse.ClearItems();
                    alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNOAndNurseCode(this.patientInfo.ID, invoiceNo, nurseCode);

                       
                }
                else if (showIBill == 5)//只显示小孩的//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加小孩费用清单汇总
                {
                    alData = this.feeMgr.GetPatientChildTotalFeeListInfoByInPatientNO(this.patientInfo.ID);
                    dt = this.feeMgr.GetPatientChildDetailFeeDTInfoByInPatientNO(this.patientInfo.ID);
                    IBillPrint.ShowData(dt, this.patientInfo);
                }
                else if (showIBill == 6)//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加只有妈妈的费用清单汇总
                {
                    alData = this.feeMgr.GetPatientMontherDetailFeeListInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                    // 给要导出的控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
                    dt = this.feeMgr.GetPatientMontherDetailFeeDTInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                    IBillPrint.ShowData(dt, this.patientInfo);
                }

                else if (showIBill == 7) //{34a15202-a3f9-4d3e-9bad-c7e6783b540c}
                {
                    alData = this.feeMgr.GetBalanceFeeByInPatienNo(this.patientInfo.ID, invoiceNo);
                }

                else if (showIBill == 8) //
                {
                    alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNOAndCI(this.patientInfo.ID);
                }

                if (alData.Count <= 0 || alData == null)
                {
                    MessageBox.Show("没有患者费用信息");
                    return -1;
                }

                if (showIBill == 4)
                {
                    ArrayList alNurse = new ArrayList();
                    Hashtable hsNurse = new Hashtable();
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

                    con.ID = "ALL";
                    con.Name = "全部";
                    alNurse.Add(con);
                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in alData)
                    {
                        con = new FS.HISFC.Models.Base.Const();
                        con.ID = feeInfo.ConfirmOper.Dept.ID;
                        con.Name = feeInfo.ConfirmOper.Dept.Name;
                        if (!hsNurse.Contains(con.ID))
                        {
                            alNurse.Add(con);
                            hsNurse.Add(con.ID, con);
                        }
                    }
                    if (alNurse.Count > 0)
                    {
                        this.cmbNurse.AddItems(alNurse);
                        this.cmbNurse.Tag = "ALL";
                    }
                }
                
                if (this.panel4.Visible)// {407F4A63-CC38-4842-BFDA-995E1C3FC664}
                {
                    this.patientInfo.User02 = this.dtBegTime.Value.ToShortDateString();
                }
                IBillPrint.ShowBill(alData, this.patientInfo);

            }
            catch (Exception ex)
            {
                errText = ex.Message;
                return -1;
            }

            return 1;
        }
        protected void QueryByInvoiceNo()
        {
            if (string.IsNullOrEmpty(this.lblInvioceNo.Text))
            {
                MessageBox.Show("请输入发票号！");
                this.lblInvioceNo.Focus();
                return;
            }
            string invoiceNo = this.lblInvioceNo.Text.Trim();
            string nurseCode = string.Empty;
            string sql = @"select d.invoice_no,d.print_invoiceno,d.inpatient_no from fin_ipb_balancehead d
                                where (d.invoice_no like '%{0}' or d.print_invoiceno like '%{0}')
                                  and rownum = 1";
            sql = string.Format(sql, invoiceNo);

            DataSet dsResult = new DataSet();

            if (this.regMgr.ExecQuery(sql, ref dsResult) == -1)
            {
                return;
            }
            if (dsResult == null)
            {
                MessageBox.Show("找不到发票信息，请确认是否已结算！");
                return;
            }
            else
            {//{275EF519-39A8-4c40-AE81-288EE9DEE944} 
                if (dsResult.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("找不到发票信息，请确认是否已结算！");
                    return;
                }
            }
            string invoice_no = string.Empty;
            string print_invoiceno = string.Empty;
            string inpatient_no = string.Empty;

            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                invoice_no = dr["invoice_no"].ToString();
                print_invoiceno = dr["print_invoiceno"].ToString();
                inpatient_no = dr["inpatient_no"].ToString();
            }
          
            this.lblInvioceNo.Text = invoice_no;
            this.patientInfo = this.radtMgr.QueryPatientInfoByInpatientNO(inpatient_no);
            this.patientInfo.User01 = print_invoiceno;
            ArrayList alData = new ArrayList();
            if (showIBill == 1)
            {
                alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNO(this.patientInfo.ID, invoice_no);
                // 给要导出的控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
                DataTable dt = this.feeMgr.GetPatientDetailFeeDTInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                IBillPrint.ShowData(dt, this.patientInfo);
            }
            else if (showIBill == 2)
            {
                alData = this.feeMgr.GetPatientDetailFeeListInfoByInPatientNO(this.patientInfo.ID, invoice_no);
            }
            else if (showIBill == 3)
            {
                alData = this.feeMgr.GetPatientOneDayFeeListInfoByInPatientNO(this.patientInfo.ID, invoice_no, this.dtBegTime.Value.ToShortDateString());
            }
            else if (showIBill == 4)
            {

                nurseCode = "ALL";
                this.cmbNurse.ClearItems();
                alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNOAndNurseCode(this.patientInfo.ID, invoiceNo, nurseCode);
            }
            else if (showIBill == 5)
            {
                MessageBox.Show("小孩不能根据发票号查询！");
                return;
            }
            else if (showIBill == 6)
            {
                alData = this.feeMgr.GetPatientMontherDetailFeeListInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                // 给要导出的控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
                DataTable dt = this.feeMgr.GetPatientMontherDetailFeeDTInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                IBillPrint.ShowData(dt, this.patientInfo);
            }
            else if (showIBill == 7) //{34a15202-a3f9-4d3e-9bad-c7e6783b540c}
            {
                alData = this.feeMgr.GetBalanceFeeByInPatienNo(this.patientInfo.ID, invoiceNo);
            
            }
            if (alData.Count <= 0 || alData == null)
            {
                MessageBox.Show("没有患者费用信息");
                return;
            }

            if (showIBill == 4)
            {
                ArrayList alNurse = new ArrayList();
                Hashtable hsNurse = new Hashtable();
                FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

                con.ID = "ALL";
                con.Name = "全部";
                alNurse.Add(con);
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in alData)
                {
                    con = new FS.HISFC.Models.Base.Const();
                    con.ID = feeInfo.ConfirmOper.Dept.ID;
                    con.Name = feeInfo.ConfirmOper.Dept.Name;
                    if (!hsNurse.Contains(con.ID))
                    {
                        alNurse.Add(con);
                        hsNurse.Add(con.ID, con);
                    }
                }
                if (alNurse.Count > 0)
                {
                    this.cmbNurse.AddItems(alNurse);
                    this.cmbNurse.Tag = "ALL";
                }
            }

            if (this.panel4.Visible)// {407F4A63-CC38-4842-BFDA-995E1C3FC664}
            {
                this.patientInfo.User02 = this.dtBegTime.Value.ToShortDateString();
            }
            IBillPrint.ShowBill(alData, this.patientInfo);
            return;

        }

        /// <summary>
        /// 根据清单查询数据  //{C3F605B6-9281-4068-98BB-393ADE0DF06C}
        /// </summary>
        /// <returns></returns>
        public bool QueryByInvoiceNoIn()
        {
            if (string.IsNullOrEmpty(this.lblInvioceNo.Text))
            {
                MessageBox.Show("请输入发票号！");
                this.lblInvioceNo.Focus();
                return false;
            }
            string invoiceNo = this.lblInvioceNo.Text.Trim();
            string nurseCode = string.Empty;
            string sql = @"select d.invoice_no,d.print_invoiceno,d.inpatient_no from fin_ipb_balancehead d
                                where (d.invoice_no like '%{0}' or d.print_invoiceno like '%{0}')
                                  and rownum = 1";
            sql = string.Format(sql, invoiceNo);

            DataSet dsResult = new DataSet();

            if (this.regMgr.ExecQuery(sql, ref dsResult) == -1)
            {
                MessageBox.Show(regMgr.Err);
                return false;
            }
            if (dsResult == null)
            {
                MessageBox.Show("找不到发票信息，请确认是否已结算！");
                return false;
            }
            else
            {//{275EF519-39A8-4c40-AE81-288EE9DEE944} 
                if (dsResult.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("找不到发票信息，请确认是否已结算！");
                    return false;
                }
            }
            string invoice_no = string.Empty;
            string print_invoiceno = string.Empty;
            string inpatient_no = string.Empty;

            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                invoice_no = dr["invoice_no"].ToString();
                print_invoiceno = dr["print_invoiceno"].ToString();
                inpatient_no = dr["inpatient_no"].ToString();
            }

            this.lblInvioceNo.Text = invoice_no;
            this.patientInfo = this.radtMgr.QueryPatientInfoByInpatientNO(inpatient_no);
            this.patientInfo.User01 = print_invoiceno;
            ArrayList alData = new ArrayList();
            if (showIBill == 1)
            {
                alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNO(this.patientInfo.ID, invoice_no);
             
                // 给要导出的控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
                DataTable dt = this.feeMgr.GetPatientDetailFeeDTInfoByInPatientNO(this.patientInfo.ID, invoiceNo);
                IBillPrint.ShowData(dt, this.patientInfo);
            }
            //else if (showIBill == 2)
            //{
            //    alData = this.feeMgr.GetPatientDetailFeeListInfoByInPatientNO(this.patientInfo.ID, invoice_no);
            //}
            //else if (showIBill == 3)
            //{
            //    alData = this.feeMgr.GetPatientOneDayFeeListInfoByInPatientNO(this.patientInfo.ID, invoice_no, this.dtBegTime.Value.ToShortDateString());
            //}
            //else if (showIBill == 4)
            //{

            //    nurseCode = "ALL";
            //    this.cmbNurse.ClearItems();
            //    alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNOAndNurseCode(this.patientInfo.ID, invoiceNo, nurseCode);
            //}
            if (alData.Count <= 0 || alData == null)
            {
                MessageBox.Show("没有患者费用信息");
                return false;
            }

            //if (showIBill == 4)
            //{
            //    ArrayList alNurse = new ArrayList();
            //    Hashtable hsNurse = new Hashtable();
            //    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

            //    con.ID = "ALL";
            //    con.Name = "全部";
            //    alNurse.Add(con);
            //    foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in alData)
            //    {
            //        con = new FS.HISFC.Models.Base.Const();
            //        con.ID = feeInfo.ConfirmOper.Dept.ID;
            //        con.Name = feeInfo.ConfirmOper.Dept.Name;
            //        if (!hsNurse.Contains(con.ID))
            //        {
            //            alNurse.Add(con);
            //            hsNurse.Add(con.ID, con);
            //        }
            //    }
            //    if (alNurse.Count > 0)
            //    {
            //        this.cmbNurse.AddItems(alNurse);
            //        this.cmbNurse.Tag = "ALL";
            //    }
            //}

            if (this.panel4.Visible)// {407F4A63-CC38-4842-BFDA-995E1C3FC664}
            {
                this.patientInfo.User02 = this.dtBegTime.Value.ToShortDateString();
            }
            IBillPrint.ShowBill(alData, this.patientInfo);
            return true;

        }

        private void lblInvioceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryByInvoiceNo();
            }
        }

        private void trvPatients_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeCollection tns = this.trvPatients.Nodes;
            foreach (TreeNode tn in tns)
            {
                this.SetNodeColor(tn);
            }
            e.Node.BackColor = Color.LightGray;

            if (e.Node != null && e.Node.Tag != null)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = e.Node.Tag as FS.HISFC.Models.RADT.PatientInfo;

                if (patient != null)
                {
                }

            }
        }

        private void SetNodeColor(TreeNode tn)
        {
            tn.BackColor = Color.White;
            if (tn.Nodes.Count > 0)
            {
                foreach (TreeNode tnChild in tn.Nodes)
                {
                    SetNodeColor(tnChild);
                }
            }
        }

        private void trvPatients_Click(object sender, EventArgs e)
        {

        }

        private void cmbNurse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList alData = new ArrayList();
            string invoiceNo = this.lblInvioceNo.Text;
            string nurseCode = this.cmbNurse.Tag.ToString();
            IBillPrint.Clear();
            alData = this.feeMgr.GetPatientTotalFeeListInfoByInPatientNOAndNurseCode(this.patientInfo.ID, invoiceNo, nurseCode);

            if (alData.Count <= 0)
            {
                return;
            }
            if (this.panel4.Visible)// {407F4A63-CC38-4842-BFDA-995E1C3FC664}
            {
                this.patientInfo.User02 = this.dtBegTime.Value.ToShortDateString();
            }
            IBillPrint.ShowBill(alData, this.patientInfo);

        }
    }
}
