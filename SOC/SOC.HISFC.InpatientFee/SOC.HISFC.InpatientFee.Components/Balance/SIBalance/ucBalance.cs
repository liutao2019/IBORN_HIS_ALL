using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance.SIBalance
{
    public partial class ucBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 患者信息管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 住院费用管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeMgr = new FS.HISFC.BizLogic.Fee.InPatient();


        /// <summary>
        /// {5A1EFA76-6758-40ae-9870-E1BAEAA4BA72}
        /// 住院费用业务类
        /// </summary>
        private FS.SOC.HISFC.InpatientFee.BizProcess.Balance balanceProcess = new FS.SOC.HISFC.InpatientFee.BizProcess.Balance();

        /// <summary>
        /// 医保代理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy 
            = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 当前结算患者
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo currentPatientInfo;

        /// <summary>
        /// 当前时间
        /// </summary>
        string currentTime;

        /// <summary>
        /// 住院药品费用
        /// </summary>
        private ArrayList MedicineList = new ArrayList();

        /// <summary>
        /// 住院非药品费用
        /// </summary>
        private ArrayList UndrugList = new ArrayList();

        /// <summary>
        /// 全部费用
        /// </summary>
        private ArrayList FeeList = new ArrayList();

        /// <summary>
        /// 构造
        /// </summary>
        public ucBalance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucUpLoadFeeDetail_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.fpUploadList_Sheet1.DataAutoSizeColumns = false;
                this.fpPatientList_Sheet1.DataAutoSizeColumns = false;
                this.fpUnuploadList_Sheet1.DataAutoSizeColumns = false;
                this.QueryNeedUpLoadFeePatients();
            }

            this.fpUploadList_Sheet1.RowCount = 0;
            this.fpUnuploadList_Sheet1.RowCount = 0;
            this.ucInPatientInfo1.QueryPatientInfoByPatientNO += QueryPatientInfo;
            this.txtUploadFeeDetailFilter.TextChanged += txtUploadFeeDetailFilter_TextChanged;
        }

        /// <summary>
        /// 工具条
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("医保结算", "广州医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("重新登记", "重新登记医保", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T跳转, true, false, null);
            toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 工具栏按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "重新登记":
                    if (this.ReuploadRegInfo() > 0)
                    {
                        this.Query(null, null);
                    }
                    break;
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
        /// 重新登记患者
        /// </summary>
        /// <returns></returns>
        private int ReuploadRegInfo()
        {
            if (this.currentPatientInfo == null)
            {
                MessageBox.Show("未选择患者");
                return -1;
            }

            if (MessageBox.Show("确定重新进行医保登记吗？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.medcareInterfaceProxy.BeginTranscation();
            long returnValue = medcareInterfaceProxy.SetPactCode(this.currentPatientInfo.Pact.ID);
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

            //取消住院登记
            returnValue = medcareInterfaceProxy.CancelRegInfoInpatient(this.currentPatientInfo);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("取消住院登记失败：" + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //医保结算
            returnValue = medcareInterfaceProxy.UploadRegInfoInpatient(this.currentPatientInfo);
            if (returnValue < 0)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("重新住院登记失败：" + medcareInterfaceProxy.ErrMsg);
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

            MessageBox.Show("重新登记成功！");
            return 1;
        }

        /// <summary>
        /// 医保结算
        /// </summary>
        /// <returns></returns>
        private int OutBalanceSI()
        {
            if (this.currentPatientInfo == null)
            {
                MessageBox.Show("未选择患者");
                return -1;
            }

            string invoiceNO = this.GetInvoiceNO();
            if(string.IsNullOrEmpty(invoiceNO))
            {
                MessageBox.Show("发票号为空");
                return -1;
            }

            ArrayList balanceFeeList = this.GetBalanceFeeList();
            if(balanceFeeList.Count == 0)
            {
                MessageBox.Show("结算列表为空");
                return -1;
            }

            string errText = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.medcareInterfaceProxy.BeginTranscation();
            long returnValue = medcareInterfaceProxy.SetPactCode(this.currentPatientInfo.Pact.ID);
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
            returnValue = medcareInterfaceProxy.PreBalanceInpatient(this.currentPatientInfo, ref balanceFeeList);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("医保预结算失败：" + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //医保结算
            returnValue = medcareInterfaceProxy.BalanceInpatient(this.currentPatientInfo, ref balanceFeeList);
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
            this.currentPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.ucInPatientInfo1.Clear();
            this.fpUploadList_Sheet1.RowCount = 0;
            this.fpUnuploadList_Sheet1.RowCount = 0;
            this.MedicineList.Clear();
            this.UndrugList.Clear();
            this.FeeList.Clear();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            QueryNeedUpLoadFeePatients();
            return 1;
        }

        /// <summary>
        /// 查询患者医保信息
        /// </summary>
        /// <param name="patientNO"></param>
        private void QueryPatientInfo(string patientNO)
        {
            if (string.IsNullOrEmpty(patientNO))
            {
                return;
            }
            DataTable dtPatient = inpatientMgr.QuerySIPatient(patientNO);
            if (dtPatient == null || dtPatient.Rows.Count == 0)
            {
                MessageBox.Show("没有找到相关住院结算信息，请检查该住院号是否有效");
                return;
            }
            else if (dtPatient.Rows.Count == 1)
            {
                string inPatientNO = dtPatient.Rows[0][0].ToString();
                SetShowValue(inPatientNO);
            }
            else
            {
                ucBaseChoose ucChoose = new ucBaseChoose(dtPatient);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ucChoose);
                if (ucChoose.IsOK)
                {
                    string inPatientNO = ucChoose.SelectedID;
                    SetShowValue(inPatientNO);
                }
            }
        }

        /// <summary>
        /// 查询需要上传的患者列表
        /// </summary>
        private void QueryNeedUpLoadFeePatients()
        {
            this.Clear();
            //{5A1EFA76-6758-40ae-9870-E1BAEAA4BA72}
            //string sql = @" select t.inpatient_no,
            //                      t.pact_code,
            //                      t.patient_no,
            //                      t.name,
            //                      t.dept_name dept,
            //                      t.out_date
            //                 from fin_ipr_inmaininfo t
            //                 left join fin_ipr_siinmaininfo t2
            //                   on t.inpatient_no = t2.inpatient_no
            //                where t.in_state = 'O'
            //                  and t2.balance_state = '0'
            //                  and t.paykind_code = '02'
            //                  and t.dept_code != '1010'
            //                  and t.pact_code = '4'
            //                  and t2.valid_flag = '1'";

            //inpatientMgr.ExecQuery(sql, ref dsRes);
            //this.fpPatientList.DataSource = dsRes;

            DataSet dsRes = new DataSet();
            if (balanceProcess.QueryNeedUpLoadFeePatients(ref dsRes) >= 0)
            {
                this.fpPatientList.DataSource = dsRes;
            }
            else
            {
                MessageBox.Show("查询待医保结算列表失败" + balanceProcess.Err, "错误");
            }
        }

        /// <summary>
        /// 获取发票
        /// </summary>
        /// <returns></returns>
        private string GetInvoiceNO()
        {
            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> sortedFeeList = FeeList.Cast<FS.HISFC.Models.Fee.Inpatient.FeeItemList>().OrderByDescending(m => m.BalanceOper.OperTime).ToList();
            //返回最后一张发票作为医保结算发票
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = sortedFeeList[0];

            int index = 1;
            while (string.IsNullOrEmpty(feeItem.Invoice.ID) && index < sortedFeeList.Count)
            {
                feeItem = sortedFeeList[index];
                index++;
            }

            return feeItem.Invoice.ID;
        }

        /// <summary>
        /// 项目过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUploadFeeDetailFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = this.txtUploadFeeDetailFilter.Text;

            foreach (FarPoint.Win.Spread.Row row in this.fpUploadList_Sheet1.Rows)
            {
                if (!this.fpUploadList_Sheet1.Cells[row.Index, 5].Text.Contains(filter))
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
        /// 双击选择待患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPatientList_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string inpatientNO = this.fpPatientList_Sheet1.Cells[e.Row, 0].Text;
            SetShowValue(inpatientNO);
        }

        /// <summary>
        /// 显示待结算信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="pactCode"></param>
        private void SetShowValue(string inpatientNO)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载项目...");
            Application.DoEvents();
            try
            {
                currentPatientInfo = inpatientMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
                ucInPatientInfo1.SetPatientInfo(currentPatientInfo);

                if (MedicineList == null)
                {
                    this.MedicineList = new ArrayList();
                }

                if (this.UndrugList == null)
                {
                    this.UndrugList = new ArrayList();
                }

                this.MedicineList.Clear();
                this.UndrugList.Clear();
                this.FeeList.Clear();
                this.fpUploadList_Sheet1.RowCount = 0;
                this.fpUnuploadList_Sheet1.RowCount = 0;
                this.currentTime = this.inpatientFeeMgr.GetSysDate();

                //获取未结算的药品信息和非药品信息
                this.MedicineList = this.inpatientFeeMgr.QueryMedicineListsForYIBAOBalanceWithoutZero(currentPatientInfo.ID); // {6286BF60-9749-41FE-BA9E-0BBFE62E7810}
                this.UndrugList = this.inpatientFeeMgr.QueryItemListsForYIBAOBalanceWithoutZero(currentPatientInfo.ID);// {6286BF60-9749-41FE-BA9E-0BBFE62E7810}

                FeeList.AddRange(this.MedicineList.Cast<FS.HISFC.Models.Fee.Inpatient.FeeItemList>().Where(m=>!m.IsBaby).ToArray());
                FeeList.AddRange(this.UndrugList.Cast<FS.HISFC.Models.Fee.Inpatient.FeeItemList>().Where(m => !m.IsBaby).ToArray());

                decimal totCost = 0.0m;
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in FeeList.Cast<FS.HISFC.Models.Fee.Inpatient.FeeItemList>().OrderBy(m => m.Item.Name))
                {
                    if (item.IsBaby)
                    {
                        continue;
                    }

                    if (item.User03 == "0")
                    {
                        this.insertFPItem(this.fpUploadList_Sheet1, item);
                        totCost += item.SIft.TotCost;
                    }
                    else
                    {
                        this.insertFPItem(this.fpUnuploadList_Sheet1, item);
                    }

                }


                label4.Text = "总金额：" + totCost.ToString(); ;

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("加载项目失败" + ex.Message, "提示");
            }
        }

        /// <summary>
        /// 获取项目价格
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetPrice(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal price)
        {
            //若没有维护医保价，则直接取普通价
            if (f.Item.SpecialPrice == 0 && f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
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

            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && f.Item.Qty > 300)//中草药
            {
                price = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost * 10 / f.Item.Qty), 4);
            }
            else
            {
                price = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost / f.Item.Qty), 4);
            }

            return true;
        }

        /// <summary>
        /// 获取项目数量
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetCount(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal qty)
        {
            //数量大于300的，基本上就是中药了
            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && f.Item.Qty > 300)
            {
                qty = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.Item.Qty / 10), 4);
            }
            else
            {
                qty = f.Item.Qty;
            }

            return true;
        }

        /// <summary>
        /// 添加项目到相应的列表
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="feeItem"></param>
        /// <returns></returns>
        private int insertFPItem(FarPoint.Win.Spread.SheetView sheetView, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem)
        {
            decimal price = 0.0m;
            this.GetPrice(feeItem, ref price);

            //价格为0的非药品项目不上传
            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug && price == 0)
            {
                return 1;
            }

            int index = sheetView.RowCount;
            sheetView.Rows.Add(sheetView.RowCount, 1);
            sheetView.Cells[index, 0].Text = feeItem.Patient.ID;  //住院流水号
            sheetView.Cells[index, 1].Text = feeItem.Invoice.ID;  //结算发票号
            sheetView.Cells[index, 2].Text = feeItem.FeeOper.OperTime.ToString(); //费用日期
            sheetView.Cells[index, 3].Text = feeItem.RecipeNO + feeItem.SequenceNO.ToString().PadLeft(3,'0'); //项目序号

            sheetView.Cells[index, 4].Text = feeItem.Item.ID;    //医院项目编码
            sheetView.Cells[index, 5].Text = feeItem.Item.Name;  //医院项目名称
            sheetView.Cells[index, 6].Text = "";  //医保分类代码


            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item pharmacyItem = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                sheetView.Cells[index, 7].Text = pharmacyItem.Specs; //规格
                sheetView.Cells[index, 8].Text = pharmacyItem.DosageForm.ID;  //剂型
            }

            sheetView.Cells[index, 9].Text = price.ToString(); //单价
            sheetView.Cells[index, 10].Text = feeItem.Item.Qty.ToString(); //数量
            sheetView.Cells[index, 11].Text = feeItem.SIft.TotCost.ToString(); //金额
            sheetView.Cells[index, 12].Text = currentTime; //操作时间
            sheetView.Cells[index, 13].Text = "";   //备注2
            sheetView.Cells[index, 14].Text = "";   //备注3
            sheetView.Cells[index, 15].Text = "";   //读入标记
            sheetView.Cells[index, 16].Text = "";   //药品来源
            sheetView.Cells[index, 17].Text = feeItem.Item.MinFee.ID; //费用类别编码
            sheetView.Cells[index, 18].Text = feeItem.User03; //上传标记

            sheetView.Rows[index].Tag = feeItem;

            return 1;
        }

        /// <summary>
        /// 上传费用明细列表双击事件对应方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUploadList_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.currentPatientInfo == null)
            {
                return;
            }

            if (this.fpUploadList_Sheet1.RowCount == 0)
            {
                return;
            }

            int rowIndex = e.Row;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = this.fpUploadList_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

            int ret = 0;
            string sqlStr = "";

            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                sqlStr = @"update fin_ipb_medicinelist t
                               set t.upload_flag = '2'
                             where t.inpatient_no = '{0}'
                               and t.invoice_no = '{1}'
                               and t.drug_code = '{2}'
                               and t.recipe_no = '{3}'
                              ";

                sqlStr = string.Format(sqlStr, currentPatientInfo.ID, feeItem.Invoice.ID, feeItem.Item.ID,feeItem.RecipeNO);// {C5F51270-B9BC-45DF-BA64-40ED9DD2F9F1}
                ret = inpatientMgr.ExecNoQuery(sqlStr);
            }
            else
            {
                sqlStr = @"update fin_ipb_itemlist t
                              set t.upload_flag = '2'
                            where t.inpatient_no = '{0}'
                              and t.invoice_no = '{1}'
                              and t.item_code = '{2}'
                              and t.recipe_no = '{3}'";
                sqlStr = string.Format(sqlStr, currentPatientInfo.ID, feeItem.Invoice.ID, feeItem.Item.ID,feeItem.RecipeNO);// {C5F51270-B9BC-45DF-BA64-40ED9DD2F9F1}
                ret = inpatientMgr.ExecNoQuery(sqlStr);
            }

            if (ret > 0)
            {
                SetShowValue(currentPatientInfo.ID);
                if (!string.IsNullOrEmpty(this.txtUploadFeeDetailFilter.Text.Trim()))
                {
                    this.txtUploadFeeDetailFilter_TextChanged(null, null);
                }
            }
        }

        /// <summary>
        /// 不上传项目列表双击事件对应方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUnuploadList_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.currentPatientInfo == null)
            {
                return;
            }

            if (this.fpUnuploadList_Sheet1.RowCount == 0)
            {
                return;
            }

            int rowIndex = e.Row;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = this.fpUnuploadList_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

            int ret = 0;
            string sqlStr = "";

            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                sqlStr = @"update fin_ipb_medicinelist t
                              set t.upload_flag = '0'
                            where t.inpatient_no = '{0}'
                              and t.invoice_no = '{1}'
                              and t.drug_code = '{2}'
                              and t.recipe_no = '{3}'";

                sqlStr = string.Format(sqlStr, this.currentPatientInfo.ID, feeItem.Invoice.ID, feeItem.Item.ID, feeItem.RecipeNO);// {C5F51270-B9BC-45DF-BA64-40ED9DD2F9F1}
                ret = inpatientMgr.ExecNoQuery(sqlStr);
            }
            else
            {
                sqlStr = @"update fin_ipb_itemlist t
                              set t.upload_flag = '0'
                            where t.inpatient_no = '{0}'
                              and t.invoice_no = '{1}'
                              and t.item_code = '{2}'
                              and t.recipe_no = '{3}'";
                sqlStr = string.Format(sqlStr, this.currentPatientInfo.ID, feeItem.Invoice.ID, feeItem.Item.ID, feeItem.RecipeNO);// {C5F51270-B9BC-45DF-BA64-40ED9DD2F9F1}
                ret = inpatientMgr.ExecNoQuery(sqlStr);
            }

            if (ret > 0)
            {
                SetShowValue(this.currentPatientInfo.ID);
                if (!string.IsNullOrEmpty(this.txtUploadFeeDetailFilter.Text.Trim()))
                {
                    this.txtUploadFeeDetailFilter_TextChanged(null, null);
                }
            }
        }

        /// <summary>
        /// 获取当前需要进行结算的费用
        /// </summary>
        /// <returns></returns>
        private ArrayList GetBalanceFeeList()
        {
            ArrayList feeArr = new ArrayList();

            foreach (FarPoint.Win.Spread.Row row in this.fpUploadList_Sheet1.Rows)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList item = row.Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                feeArr.Add(item);
            }

            return feeArr;
        }

    }
}
