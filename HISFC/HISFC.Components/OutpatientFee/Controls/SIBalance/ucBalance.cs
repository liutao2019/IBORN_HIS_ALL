using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FS.HISFC.Components.OutpatientFee.Controls.SIBalance
{
    public partial class ucBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 医保代理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy
            = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 门诊业务管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Outpatient outpatientMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 药品业务层
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemPharmacyManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 非药品业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 合同单位管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        // {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        //
        FS.HISFC.BizLogic.Order.OutPatient.Order OutOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();


        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register register;

        /// <summary>
        /// 费用明细
        /// </summary>
        private System.Collections.ArrayList feeItemLists;


        /// <summary>
        /// 上传费用明细
        /// </summary>
        private System.Collections.ArrayList uploadfeeItemLists = new ArrayList();


        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string clinicCode;

        /// <summary>
        /// 结算发票号
        /// </summary>
        private string invoiceNO;

        /// <summary>
        /// 当前时间
        /// </summary>
        private string currentTime;

        // {7CCDE870-4563-4b88-9504-15FED80B95CC}
        private int queryDays = 150;
        /// <summary>
        /// 往回查询的挂号天数
        /// </summary>
        public int QueryDays
        {
            get { return queryDays; }
            set { queryDays = value; }
        }

        private string pact_code_SI = "4";// {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
        /// <summary>
        /// 医保合同单位
        /// </summary>
        public string Pact_code_SI
        {
            get { return pact_code_SI; }
            set { pact_code_SI = value; }
        }

        /// <summary>
        /// 普通患者医保补结算
        /// </summary>
        public ucBalance()
        {
            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
            this.ucOutPatientInfo1.GetRegisterByCardNO += queryRegister;
            this.fpSpread2.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread2_CellDoubleClick);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            this.fpSpread3.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread3_CellDoubleClick);
            this.tbQuery.TextChanged += new EventHandler(tbQuery_TextChanged);
            this.currentTime = this.registerMgr.GetSysDate();

            this.pact_code_SI = ctlParamManage.GetControlParam<string>("YB0001");// {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
        }

        /// <summary>
        /// 菜单栏
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 菜单栏初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("医保结算", "门诊医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q确认收费, true, false, null);
            toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 菜单栏按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "医保结算":
                    if (this.OutBalanceSI() > 0)
                    {
                        this.Query(null, null);
                    }
                    break;
                case "清屏":
                    this.Clear();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 查询待上传患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.queryNeedUpLoadFeePatients();
            return 1;
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucUpLoadFeeDetail_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
                this.fpSpread2_Sheet1.DataAutoSizeColumns = false;
                queryNeedUpLoadFeePatients();
            }
        }

        /// <summary>
        /// 获取需要上传的列表
        /// </summary>
        private void queryNeedUpLoadFeePatients()
        {
            this.Clear();
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            // {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
            string sql = @"select t.clinic_code 门诊流水号,
                                  t2.invoice_no 结算发票号,
                                  t2.pact_code 合同单位,
                                  t.name 姓名,
                                  fun_get_dept_name(t.see_dpcd) 看诊科室,
                                  t.see_date 看诊时间,
                                  fun_get_employee_name(t.see_docd) 看诊医生
                             from fin_opr_register t,fin_opb_invoiceinfo t2
                            where t.clinic_code = t2.clinic_code
                              and t.valid_flag = '1'
                              and t.reg_date > trunc(sysdate) - {0}
                              and t2.cancel_flag = '1'
                              --and t2.pact_code = '9'
                              and not exists (select 1 
                                                from fin_ipr_siinmaininfo a 
                                               where a.inpatient_no = t.clinic_code
                                                 and a.invoice_no = t2.invoice_no
                                                 and a.valid_flag = '1')
                             order by t.see_date desc";

            sql = string.Format(sql, this.QueryDays.ToString());
            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            this.fpSpread2.DataSource = dsRes;
        }

        /// <summary>
        /// 患者过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbQuery_TextChanged(object sender, EventArgs e)
        {
            string filter = this.tbQuery.Text;

            foreach (FarPoint.Win.Spread.Row row in this.fpSpread2_Sheet1.Rows)
            {
                if (!this.fpSpread2_Sheet1.Cells[row.Index, 1].Text.Contains(filter) && !this.fpSpread2_Sheet1.Cells[row.Index, 3].Text.Contains(filter))
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }

                if (string.IsNullOrEmpty(filter))
                {
                    row.Visible = true;
                }
            }
        }

        /// <summary>
        /// 查询挂号信息
        /// </summary>
        /// <param name="patientNO"></param>
        private void queryRegister(string cardNO)
        {
            this.Clear();
            cardNO = cardNO.PadLeft(10, '0');
            //todo:需优化，代码整合，跟框架一样把sql统一到com_sql表进行管理
            // {7FEDE87A-5579-4EF0-A2EF-B5663EF45A01}
            string sql = @"select t.clinic_code 门诊流水号,
                                  t2.invoice_no 结算发票号,
                                  t2.pact_code 合同单位,
                                  t.name 姓名,
                                  fun_get_dept_name(t.see_dpcd) 看诊科室,
                                  t.see_date 看诊时间,
                                  fun_get_employee_name(t.see_docd) 看诊医生
                             from fin_opr_register t,fin_opb_invoiceinfo t2
                            where t.clinic_code = t2.clinic_code
                              and t.valid_flag = '1'
                              and t.reg_date > trunc(sysdate) - {1}
                              and t2.cancel_flag = '1'
                              --and t2.pact_code = '9' // {6206BCF6-595E-42D3-A212-88E7AF056394}
                              and t.card_no = '{0}'
                              and not exists (select 1 
                                                from fin_ipr_siinmaininfo a 
                                               where a.inpatient_no = t.clinic_code
                                                 and a.invoice_no = t2.invoice_no
                                                 and a.valid_flag = '1')
                             order by t.see_date desc";

            sql = string.Format(sql, cardNO, this.QueryDays.ToString());
            DataSet dsRes = new DataSet();
            registerMgr.ExecQuery(sql, ref dsRes);
            if (dsRes == null || dsRes.Tables[0] == null || dsRes.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("没有找到相关门诊结算信息，请检查该卡号是否有效或该卡号是否结算过");
                return;
            }
            ucBaseChoose ucChoose = new ucBaseChoose(dsRes.Tables[0]);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            FS.FrameWork.WinForms.Classes.Function.ShowControl(ucChoose);
            if (ucChoose.IsOK)
            {
                this.clinicCode = ucChoose.SelectedID;
                this.invoiceNO = ucChoose.SelectedNO;
                this.SetShowValue(this.clinicCode, this.invoiceNO);
            }
        }

        /// <summary>
        /// 双击选择发票记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Clear();

            this.clinicCode = this.fpSpread2_Sheet1.Cells[e.Row, 0].Text;
            this.invoiceNO = this.fpSpread2_Sheet1.Cells[e.Row, 1].Text;
            SetShowValue(this.clinicCode, this.invoiceNO);
        }

        /// <summary>
        /// 双击明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rowIndex = e.Row;

            if (this.fpSpread1_Sheet1.Rows[rowIndex].ForeColor == Color.Red)
            {
                MessageBox.Show("该项已选择！");
                return ;
            }

            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.fpSpread1_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

            if (feeItemList != null)
            {
                int index = this.sheetView1.RowCount;
                this.sheetView1.Rows.Add(this.sheetView1.RowCount, 1);

                decimal price = 0.0m;
                this.GetPrice(feeItemList, ref price);
                this.sheetView1.Cells[index, 0].Text = feeItemList.Patient.ID;  //门诊流水号
                this.sheetView1.Cells[index, 1].Text = feeItemList.Invoice.ID;  //结算发票号
                this.sheetView1.Cells[index, 2].Text = feeItemList.FeeOper.OperTime.ToString(); //费用日期
                this.sheetView1.Cells[index, 3].Text = feeItemList.RecipeNO + feeItemList.SequenceNO.ToString().PadLeft(3, '0'); //项目序号

                this.sheetView1.Cells[index, 4].Text = feeItemList.Item.ID;    //医院项目编码
                this.sheetView1.Cells[index, 5].Text = feeItemList.Item.Name;  //医院项目名称
                this.sheetView1.Cells[index, 6].Text = "";  //医保分类代码


                if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item pharmacyItem = feeItemList.Item as FS.HISFC.Models.Pharmacy.Item;
                    this.sheetView1.Cells[index, 7].Text = pharmacyItem.Specs; //规格
                    this.sheetView1.Cells[index, 8].Text = pharmacyItem.DosageForm.ID;  //剂型
                }

                this.sheetView1.Cells[index, 9].Text = price.ToString(); //单价
                this.sheetView1.Cells[index, 10].Text = feeItemList.Item.Qty.ToString(); //数量
                this.sheetView1.Cells[index, 11].Text = feeItemList.SIft.TotCost.ToString(); //金额
                this.sheetView1.Cells[index, 12].Text = currentTime; //操作时间
                this.sheetView1.Cells[index, 13].Text = "";   //备注2
                this.sheetView1.Cells[index, 14].Text = "";   //备注3
                this.sheetView1.Cells[index, 15].Text = "";   //读入标记
                this.sheetView1.Cells[index, 16].Text = "";   //药品来源
                this.sheetView1.Cells[index, 17].Text = feeItemList.Item.MinFee.ID; //费用类别编码
                this.sheetView1.Cells[index, 18].Text = feeItemList.UndrugComb == null ? "" : feeItemList.UndrugComb.Name; //复合项目名称

                this.fpSpread1_Sheet1.Rows[rowIndex].ForeColor = Color.Red;


                uploadfeeItemLists.Add(feeItemList);

                feeItemList.Hospital_name = rowIndex.ToString();   //先借用

                this.sheetView1.Rows[index].Tag = feeItemList;
            }

            

        }

        /// <summary>
        /// 双击移除
        /// </summary>
        private void fpSpread3_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rowIndex = e.Row;

            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.sheetView1.Rows[rowIndex].Tag as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

            uploadfeeItemLists.Remove(feeItemList);

            int inx = int.Parse(feeItemList.Hospital_name);
            this.fpSpread1_Sheet1.Rows[inx].ForeColor = System.Drawing.SystemColors.InfoText; 

            this.sheetView1.Rows.Remove(rowIndex, 1);

        }

        /// <summary>
        /// 显示费用
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="pactCode"></param>
        private int SetShowValue(string clinicNO, string invoiceNO)
        {
            register = registerMgr.GetByClinic(clinicNO);
            register.SIMainInfo.InvoiceNo = invoiceNO;

            ucOutPatientInfo1.SetPatientInfo(register);
            this.feeItemLists = this.outpatientMgr.QueryFeeItemListsByInvoiceNO(invoiceNO);

            System.Collections.Generic.Dictionary<string, decimal> alExce = OutOrderMgr.GetExceededItem(register.PID.CardNO);

            this.currentTime = this.registerMgr.GetSysDate();

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeItemLists)
            {
                decimal price = 0.0m;
                this.GetPrice(feeItemList, ref price);

                int index = this.fpSpread1_Sheet1.RowCount;
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                this.fpSpread1_Sheet1.Cells[index, 0].Text = feeItemList.Patient.ID;  //门诊流水号
                this.fpSpread1_Sheet1.Cells[index, 1].Text = feeItemList.Invoice.ID;  //结算发票号
                this.fpSpread1_Sheet1.Cells[index, 2].Text = feeItemList.FeeOper.OperTime.ToString(); //费用日期
                this.fpSpread1_Sheet1.Cells[index, 3].Text = feeItemList.RecipeNO + feeItemList.SequenceNO.ToString().PadLeft(3, '0'); //项目序号

                this.fpSpread1_Sheet1.Cells[index, 4].Text = feeItemList.Item.ID;    //医院项目编码
                this.fpSpread1_Sheet1.Cells[index, 5].Text = feeItemList.Item.Name;  //医院项目名称
                this.fpSpread1_Sheet1.Cells[index, 6].Text = "";  //医保分类代码


                if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item pharmacyItem = feeItemList.Item as FS.HISFC.Models.Pharmacy.Item;
                    this.fpSpread1_Sheet1.Cells[index, 7].Text = pharmacyItem.Specs; //规格
                    this.fpSpread1_Sheet1.Cells[index, 8].Text = pharmacyItem.DosageForm.ID;  //剂型
                }

                this.fpSpread1_Sheet1.Cells[index, 9].Text = price.ToString(); //单价
                this.fpSpread1_Sheet1.Cells[index, 10].Text = feeItemList.Item.Qty.ToString(); //数量
                this.fpSpread1_Sheet1.Cells[index, 11].Text = feeItemList.SIft.TotCost.ToString(); //金额
                this.fpSpread1_Sheet1.Cells[index, 12].Text = currentTime; //操作时间
                this.fpSpread1_Sheet1.Cells[index, 13].Text = "";   //备注2
                this.fpSpread1_Sheet1.Cells[index, 14].Text = "";   //备注3
                this.fpSpread1_Sheet1.Cells[index, 15].Text = "";   //读入标记
                this.fpSpread1_Sheet1.Cells[index, 16].Text = "";   //药品来源
                this.fpSpread1_Sheet1.Cells[index, 17].Text = feeItemList.Item.MinFee.ID; //费用类别编码
                this.fpSpread1_Sheet1.Cells[index, 18].Text = feeItemList.UndrugComb == null ? "" : feeItemList.UndrugComb.Name; //复合项目名称

                if (feeItemList.UndrugComb != null && !string.IsNullOrEmpty(feeItemList.UndrugComb.ID))
                {
                    if (alExce != null && alExce.ContainsKey(feeItemList.UndrugComb.ID))
                    {
                        decimal num = alExce[feeItemList.UndrugComb.ID];
                        if (num > 0)
                        {
                            this.fpSpread1_Sheet1.Cells[index, 19].Text = "注意复合项目【" + feeItemList.UndrugComb.Name + "】剩余" + num + "次可上传医保";
                        }

                    }

                }
                else
                {
                    if (alExce != null && alExce.ContainsKey(feeItemList.Item.ID))
                    {
                        decimal num = alExce[feeItemList.Item.ID];
                        if (num > 0)
                        {
                            alExce[feeItemList.Item.ID] = alExce[feeItemList.Item.ID] - feeItemList.Item.Qty;
                            this.fpSpread1_Sheet1.Cells[index, 19].Text = "该项可上传医保";
                        }

                    }

                }

                this.fpSpread1_Sheet1.Rows[index].Tag = feeItemList;
            }

            return 1;
        }

        /// <summary>
        /// 医保结算
        /// </summary>
        /// <returns></returns>
        private int OutBalanceSI()
        {
            if (this.register == null || string.IsNullOrEmpty(this.register.ID))
            {
                MessageBox.Show("未选择患者！");
                return -1;
            }

            if (string.IsNullOrEmpty(this.invoiceNO))
            {
                MessageBox.Show("发票号为空");
                return -1;
            }

            if (this.uploadfeeItemLists.Count == 0)
            {
                MessageBox.Show("结算费用为空");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.medcareInterfaceProxy.BeginTranscation();
            this.register.Pact = this.pactUnitMgr.GetPactUnitInfoByPactCode(Pact_code_SI);
            long returnValue = medcareInterfaceProxy.SetPactCode(this.register.Pact.ID);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医疗待遇接口初始化失败：" + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //连接待遇接口
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                MessageBox.Show("医疗待遇接口连接失败：" + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //医保预结算
            returnValue = medcareInterfaceProxy.PreBalanceOutpatient(this.register, ref this.uploadfeeItemLists);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医保预结算失败：" + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //医保结算
            returnValue = medcareInterfaceProxy.BalanceOutpatient(this.register, ref this.uploadfeeItemLists);
            if (returnValue < 0)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医保结算失败：" + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //断开医保连接
            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医保断开连接失败：" + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            this.medcareInterfaceProxy.Commit();
            this.medcareInterfaceProxy.Disconnect();
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("结算成功");
            return 1;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.register = new FS.HISFC.Models.Registration.Register();
            this.feeItemLists = new ArrayList();
            this.clinicCode = string.Empty;
            this.invoiceNO = string.Empty;
            this.ucOutPatientInfo1.Clear();
            this.fpSpread1_Sheet1.RowCount = 0;
            this.sheetView1.RowCount = 0;
            this.uploadfeeItemLists = new ArrayList();
        }

        /// <summary>
        /// 获取项目价格
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetPrice(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal price)
        {
            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = itemPharmacyManager.GetItem(f.Item.ID);
                if (phaItem == null)
                {
                    MessageBox.Show("获得药品信息出错!");
                    return false;
                }
                f.Item.SpecialPrice = phaItem.SpecialPrice;
                f.Item.PackQty = phaItem.PackQty;
                f.Item.SysClass.ID = phaItem.SysClass.ID;
            }
            else if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Base.Item baseItem = new FS.HISFC.Models.Base.Item();
                baseItem = itemManager.GetItemByUndrugCode(f.Item.ID);
                if (baseItem == null)
                {
                    MessageBox.Show("获得非药品信息出错!");
                    return false;
                }
                f.Item.SpecialPrice = baseItem.SpecialPrice;
                f.Item.PackQty = baseItem.PackQty;
                f.Item.SysClass.ID = baseItem.SysClass.ID;
            }

            //若没有维护医保价，则直接取普通价
            if (f.Item.SpecialPrice == 0)
            {
                f.Item.SpecialPrice = f.Item.Price;
            }

            //处理包装单位，部分项目没有包装单位
            if (f.Item.PackQty == 0)
            {
                f.Item.PackQty = 1;
            }

            if (f is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
            {
                //参考门诊的价格计算规则
                //门诊价格计算规则代码路径：
                //FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.ITruncFee.ITruncFeeImplement
                f.SIft.TotCost = FS.FrameWork.Public.String.TruncateNumber(f.Item.SpecialPrice * f.Item.Qty / f.Item.PackQty, 2);
                f.SIft.RebateCost = FS.FrameWork.Public.String.TruncateNumber(f.FT.RebateCost * f.Item.Qty / f.Item.PackQty, 2);
                f.SIft.OwnCost = f.SIft.TotCost;
            }
            else if (f is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
            {
                //参考门诊的价格计算规则
                //住院价格计算规则代码路径：
                //FS.HISFC.BizProcess.Integrate.Fee   ConvertOrderToFeeItemList函数
                f.SIft.TotCost = FS.FrameWork.Public.String.FormatNumber((f.Item.SpecialPrice / f.Item.PackQty), 2) * f.Item.Qty;
                f.SIft.TotCost = FS.FrameWork.Public.String.FormatNumber(f.SIft.TotCost, 2);
                f.SIft.RebateCost = FS.FrameWork.Public.String.FormatNumber((f.SIft.RebateCost / f.Item.PackQty), 2) * f.Item.Qty;
                f.SIft.RebateCost = FS.FrameWork.Public.String.FormatNumber(f.SIft.RebateCost, 2);
                f.SIft.OwnCost = f.SIft.TotCost;
            }
            else
            {
                return false;
            }

            price = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost / f.Item.Qty), 4);

            return true;
        }

        /// <summary>
        /// 获取项目数量
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetCount(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal qty)
        {
            qty = f.Item.Qty;

            return true;
        }

    }
}
