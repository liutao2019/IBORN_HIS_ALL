using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using GZSI.Controls;
using FS.HISFC.Models.Base;

namespace GZSI.Controls
{
    /// <summary>
    /// 住院医保明细上传
    /// </summary>
    public partial class frmUploadDetail : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmUploadDetail()
        {
            InitializeComponent();
        }

		ucUploadDetail ucUploadDetail1 = new ucUploadDetail();

        ArrayList alCenterCompare = new ArrayList(); //= this.myConn.GetSICompareList();
        ArrayList alYDCenterCompore = new ArrayList(); //= this.myConn.GetYDSICompareList(); {FC86C316-F598-4724-B300-8320FAD72344} zhang-wx 2016-12-30 增加了异地医保的对照信息查询

        Management.SIConnect myConn;
        Hashtable hsCompare = new Hashtable();

        Hashtable hsYDComPare = new Hashtable();  //{FC86C316-F598-4724-B300-8320FAD72344} zhang-wx 2016-12-30 增加了异地医保的对照信息查询

        Management.SILocalManager myInterface = new Management.SILocalManager();
        FS.HISFC.BizLogic.RADT.InPatient myInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        Management.SILocalManager mySiPatient = new   global::GZSI.Management.SILocalManager();
        Management.InpatientFee myFee = new Management.InpatientFee();
        FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();


        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        bool isSplitFee = false;//存在高收费业务


        FS.HISFC.Models.RADT.PatientInfo pInfo = new FS.HISFC.Models.RADT.PatientInfo();
		DataTable dsDetail = null;

        private void SetFeeDetail(ArrayList alUndrug, ArrayList alDrug)
        {
            this.dsDetail.Rows.Clear();
            decimal Cost = 0m;
            if (alUndrug == null || alDrug == null) return;
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList list in alDrug)
            {
                //if (list.NoBackQty <= 0)
                //{
                //    continue;
                //}

                Cost += list.FT.TotCost;
                FarPoint.Win.Spread.CellType.CheckBoxCellType checkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                fpSpread1_Sheet1.Columns[0].CellType = checkType;
                this.dsDetail.Columns[0].DataType = typeof(bool);
                this.dsDetail.Rows.Add(new object[]{true, 
                                                       this.pInfo.PID.ID,
													   this.pInfo.SIMainInfo.RegNo,
                                                       list.Item.UserCode,
                                                       list.Item.ID,
													   list.Item.Name,
													   list.Item.Specs,
													   list.Item.Price,
													   list.Item.Qty,
													   list.FT.TotCost,
                                                       list.ChargeOper.OperTime,
                                                       list.User03,
                    list.Item.SpellCode,
                    list.Item.WBCode});

                    this.fpSpread1_Sheet1.Rows[this.dsDetail.Rows.Count - 1].Tag = list;
                    if (list.User03 == "未对照")
                    {
                        this.fpSpread1_Sheet1.Rows[this.dsDetail.Rows.Count - 1].ForeColor = Color.Red;
                    }
                    else 
                    {
                        this.fpSpread1_Sheet1.Rows[this.dsDetail.Rows.Count - 1].ForeColor = Color.Black;
                    }
                }
            
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList list in alUndrug)
            {

                //if (list.NoBackQty <= 0)
                //{
                //    continue;
                //}

                Cost += list.FT.TotCost;
                this.dsDetail.Rows.Add(new object[]{true, 
                                                       this.pInfo.PID.ID,
													   this.pInfo.SIMainInfo.RegNo,
                                                       list.Item.UserCode,
                                                       list.Item.ID,
													   list.Item.Name,
													   list.Item.Specs,
													   list.Item.Price,
													   list.Item.Qty,  //没有找到对应的属性
													   list.FT.TotCost,
													   list.ChargeOper.OperTime,
                                                       list.User03,
                    list.Item.SpellCode,
                    list.Item.WBCode});
                this.fpSpread1_Sheet1.Rows[this.dsDetail.Rows.Count - 1].Tag = list;
                if (list.User03 == "未对照")
                {
                    this.fpSpread1_Sheet1.Rows[this.dsDetail.Rows.Count - 1].ForeColor = Color.Red;
                  
                }
                 else 
                    {
                        this.fpSpread1_Sheet1.Rows[this.dsDetail.Rows.Count - 1].ForeColor = Color.Black;
                    }
            }

            //排序，为对照的排在最前面
            this.dsDetail.DefaultView.Sort = " 是否对照 desc";

            this.txtCost.Text = Cost.ToString("0.00");
        }

		private void InitDataSet()
		{
			Type str = typeof(string);
			Type dec = typeof(decimal);
			Type Int = typeof(System.Int32);
            Type booltype = typeof(bool);
			dsDetail = new DataTable();

            this.dsDetail.Columns.AddRange(new DataColumn[]{
                                                               new DataColumn("是否上传",booltype),
															   new DataColumn("住院号",str),
															   new DataColumn("医保登记号",str),
                                                               new DataColumn("自定义码",str),
                                                               new DataColumn("项目代码",str),
															   new DataColumn("项目名称",str),
															   new DataColumn("规格",str),
															   new DataColumn("项目价格",dec),
															   new DataColumn("数量",dec),
															   new DataColumn("金额",dec),
															   new DataColumn("费用日期",str),
                                                               new DataColumn("是否对照"),
                                                               new DataColumn("拼音码",str),
                                                               new DataColumn("五笔码",str)

														   });

            this.fpSpread1_Sheet1.DataAutoHeadings = false;
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;

            this.fpSpread1_Sheet1.DataSource = this.dsDetail.DefaultView;
		}

		private void QureyFee()
		{
			if(this.pInfo == null || this.pInfo.ID == "")
			{
				return;
			}
			//更新不需要上传的费用明细最小费用标志。

			//FS.NFC.Management.Transaction t = new FS.NFC.Management.Transaction(FS.NFC.Management.Connection.Instance);
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();//常数 
            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            conMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
			ArrayList alMinFee = conMgr.GetList("NOUPMINFEE");
			if(alMinFee==null||alMinFee.Count==0)
			{
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("获取常数出错，请维护不需要上传的项目","提示");
				return ;
			}
			string strMinFee="";
			
			foreach(FS.HISFC.Models.Base.Const con in alMinFee)
			{
				strMinFee = strMinFee +",'"+con.ID+"'";
			}
			strMinFee = strMinFee.Remove(0,1);//去除第一个逗号
			if(myInterface.UpdateFlagForNotUpload(tbPatientNo.InpatientNo,strMinFee,"3")<0)
			{
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("更新上传标志出错"+myInterface.Err,"提示");
				return ;
			}
			//				if(myInterface.UpdateItemsNotUpload(true,tbPatientNo.InpatientNo) < 0)
			//				{
			//					t.RollBack();
			//					MessageBox.Show("更新不上传的药品标志出错"+myInterface.Err,"提示");
			//					return ;
			//				}
			//				if(myInterface.UpdateItemsNotUpload(false,tbPatientNo.InpatientNo) < 0)
			//				{
			//					t.RollBack();
			//					MessageBox.Show("更新不上传的非药品标志出错"+myInterface.Err,"提示");
			//					return ;
			//				}
            

            //怡康高收费不上传项目更新 
            if (isSplitFee)
            {
                if (myInterface.UpdateFlagForNotUploadForSplitFeeFlag(tbPatientNo.InpatientNo, "1", "3") < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新上传标志出错" + myInterface.Err, "提示");
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.QueryFeeList(this.dtBegin.Value, this.dtEnd.Value);
			this.SetFeeDetail(this.ucUploadDetail1.UndrugDetails,this.ucUploadDetail1.DrugDetails);

            //string filter = "是否对照" + like + "'未对照'";
            //this.dvDetail.RowFilter = filter;
		}

        private void QueryFeeList(DateTime beginDate, DateTime endDate)
        {
            ArrayList drugList = new ArrayList();
            drugList = myFee.QueryMedItemLists(tbPatientNo.InpatientNo, beginDate, endDate, "No");
            if (drugList == null)
            {
                MessageBox.Show("获得药品信息出错!");
                return;
            }

            FS.HISFC.BizLogic.Pharmacy.Item phaItemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.BizLogic.Fee.Item feeItemMgr = new FS.HISFC.BizLogic.Fee.Item();
            //{FC86C316-F598-4724-B300-8320FAD72344} zhang-wx 2016-12-30 增加了异地医保的判断，增加了异地医保项目对照
            if (pInfo.Pact.ID.Length > 2
                && pInfo.Pact.ID.Substring(0, 2).ToString() == "YD") //判断是否异地医保，是的话，走异地医保对照
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in drugList)
                {
                    //将药品编码转换成自定义码
                    item.Item.UserCode = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(item.Item.ID);

                    if (item.Item.UserCode == null || item.Item.UserCode == "" || !this.hsYDComPare.Contains(item.Item.UserCode))
                    {
                        item.User03 = "未对照";          //不确定是否能上传的项目
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in drugList)
                {
                    //将药品编码转换成自定义码
                    item.Item.UserCode = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(item.Item.ID);

                    if (item.Item.UserCode == null || item.Item.UserCode == "" || !this.hsCompare.Contains(item.Item.UserCode))
                    {
                        //item.User03 = "未对照";          //不确定是否能上传的项目
                    }
                }
            }

            tbDrugCount.Text = drugList.Count.ToString();
            ArrayList undrugList = new ArrayList();
            undrugList = myFee.QueryFeeItemLists(tbPatientNo.InpatientNo, beginDate, endDate, "No");
            if (undrugList == null)
            {
                MessageBox.Show("获得非药品信息出错!");
                return;
            }

            //ArrayList itemNotSureList = myFee.QueryFeeItemLists(tbPatientNo.InpatientNo, beginDate, endDate, "NOTSURE");
            //if (itemNotSureList != null && itemNotSureList.Count > 0)
            //{
            //    foreach (FS.HISFC.Object.Fee.Inpatient.FeeItemList item in itemNotSureList)
            //    {
            //        item.User03 = "未对照";           //不确定是否能上传的项目

            //    }
            //}
            // undrugList.AddRange(itemNotSureList);

            //{FC86C316-F598-4724-B300-8320FAD72344} zhang-wx 2016-12-30 增加了异地医保的判断，增加了异地医保项目对照
            if (pInfo.Pact.ID.Length > 2
                && pInfo.Pact.ID.Substring(0, 2).ToString() == "YD") //判断是否异地医保，是的话，走异地医保对照
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in undrugList)
                {

                    //非药品编码转化成自定义码
                    f.Item.UserCode = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItemUserCode(f.Item.ID);

                    if (f.Item.UserCode == null || f.Item.UserCode == "" || !this.hsYDComPare.Contains(f.Item.UserCode))
                    {
                        //f.User03 = "未对照";          //不确定是否能上传的项目

                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in undrugList)
                {

                    //非药品编码转化成自定义码
                    f.Item.UserCode = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItemUserCode(f.Item.ID);

                    if (f.Item.UserCode == null || f.Item.UserCode == "" || !this.hsCompare.Contains(f.Item.UserCode))
                    {
                        //f.User03 = "未对照";          //不确定是否能上传的项目
                    }
                }
            }
            tbUndrugCount.Text = undrugList.Count.ToString();

            this.ucUploadDetail1.PInfo = pInfo;
            //drugList = this.DealFeeItems(drugList);
            //if (drugList.Count >= 0)
            //{
            this.ucUploadDetail1.DrugDetails = drugList;
            //}
            //undrugList = this.DealFeeItems(undrugList);
            //if (undrugList.Count >= 0)
            //{
            this.ucUploadDetail1.UndrugDetails = undrugList;
            //}
        }
        private System.Collections.ArrayList DealFeeItems(System.Collections.ArrayList feeDetails)
        {
            //先写死用特诊价
            ArrayList alFeeItems = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fItem in feeDetails)
            {
                decimal price = 0;
                if (fItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item drugItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(fItem.Item.ID);
                    if (drugItem != null
                        && drugItem.SpecialPrice > 0)
                    {
                        price = drugItem.SpecialPrice;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrugItem = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(fItem.Item.ID);
                    if (undrugItem != null
                        && undrugItem.SpecialPrice > 0)
                    {
                        price = undrugItem.SpecialPrice;
                    }
                    else
                    {
                        continue;
                    }
                }

                FS.HISFC.Models.Fee.Inpatient.FeeItemList fNew = fItem.Clone();
                fNew.Item.Price = price;

                alFeeItems.Add(fNew);
            }
            return alFeeItems;
        }

        /// <summary>
        /// 更新本地上传标记
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private int UpdateNotUpload(DateTime beginDate, DateTime endDate)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.mySiPatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (feeItem.User03 == "未对照") //不上传项目,更新项目上传标志为3
                {
                    int state = this.mySiPatient.UpdateUploadFlag(tbPatientNo.InpatientNo, beginDate, endDate, "3", feeItem);
                    if (state == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

		private void btnOk_Click(object sender, System.EventArgs e)
		{
            //暂时屏蔽

            //btnOk.Enabled = false;
            //try
            //{
            //    FS.UFC.Common.Forms.frmPopTextBox frmPop = new FS.UFC.Common.Forms.frmPopTextBox();
            //    frmPop.IsShowContinue = false;
            //    frmPop.Text = "请输入医保就医登记号：";
            //    frmPop.ShowDialog();
            //    if(!frmPop.IsCancel&&frmPop.ReturnText!= "")
            //    {
            //        this.ucUploadDetail1.PInfo.SIMainInfo.RegNo = frmPop.ReturnText;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;
            //}
            //Cursor = Cursors.WaitCursor;
            if (this.ucUploadDetail1.CheckValid() == -1)
            {
                return;
            }
			this.ucUploadDetail1.Visible=true;
			this.ucUploadDetail1.BringToFront();
			this.ucUploadDetail1.Show();
            Application.DoEvents();

            if (this.UpdateNotUpload(this.dtBegin.Value, this.dtEnd.Value) == -1)
            {
                MessageBox.Show("不上传项目的标志的更新失败");
                return;
            }
            this.QueryFeeList(this.dtBegin.Value,this.dtEnd.Value);
            if (this.ucUploadDetail1.HasFeeDateAfterOutDate())
            {
                this.ucUploadDetail1.SetFeeDateBeforeOutDate();
            }

            if (this.ucUploadDetail1.Upload(this.dtBegin.Value, this.dtEnd.Value) < 0)
            {
                return;
            }
			this.ucUploadDetail1.Hide();
			this.ucUploadDetail1.Visible=false;
			this.ucUploadDetail1.SendToBack();
			Cursor = Cursors.Arrow;
			btnOk.Enabled = true;
            MessageBox.Show("上传费用成功!");

            this.dtBegin.Value = this.pInfo.PVisit.InTime.AddDays(-1);
            this.QueryFeeList(this.dtBegin.Value, this.dtEnd.Value);
            this.SetFeeDetail(this.ucUploadDetail1.UndrugDetails, this.ucUploadDetail1.DrugDetails);
            if (this.ucUploadDetail1.UndrugDetails.Count > 0 || this.ucUploadDetail1.DrugDetails.Count > 0)
            {
                MessageBox.Show("截止" + this.dtEnd.Value.ToString() + "该患者还有项目未上传,请检查是否漏传!");
            }
            //{7E7DB90E-5C84-458d-A9D5-111A11551A3D}FangW,2016-06-30上传成功之后，清理掉数据。
            this.ucUploadDetail1.DrugDetails.Clear();
            this.ucUploadDetail1.UndrugDetails.Clear();
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmUploadDetail_Load(object sender, System.EventArgs e)
		{
            //是否存在高收费业务
            this.isSplitFee= this.controlParamIntegrate.GetControlParam<bool>("I00020", true, false);

			InitDataSet();
			this.tbPatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(tbPatientNo_myEvent);
			
			this.Controls.Add(this.ucUploadDetail1);
			this.ucUploadDetail1.Size = new Size(550,136);
			this.ucUploadDetail1.Location = new Point(this.Width/2-200,this.Height/2 -50);
			this.ucUploadDetail1.Visible = false;
            //this.btnDelete.Visible = true;
            this.btnCancel.Text = "取消上传";
			//this.ucUploadDetail1.BringToFront();
            this.dtBegin.Value = myFee.GetDateTimeFromSysDateTime();
			//this.dtBegin.Value = myFee.GetDateTimeFromSysDateTime();

            try
            {
                myConn = new  global::GZSI.Management.SIConnect();
            }
            catch
            {
                MessageBox.Show("连接到医保出错!");
            }
            this.alCenterCompare =  this.myConn.GetSICompareList();
            if (alCenterCompare != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in this.alCenterCompare)
                {
                    if (!this.hsCompare.Contains(obj.User01))
                    {
                        this.hsCompare.Add(obj.User01, obj);
                    }
                }
            }

            #region {FC86C316-F598-4724-B300-8320FAD72344} zhang-wx 2016-12-30 增加了异地医保的对照信息查询
            this.alYDCenterCompore = this.myConn.GetYDSICompareList();
            //{3E8F8F4E-4FC9-44d6-AC9E-127FE30FB210} zhang-wx 2017-08-07 医保系统还没有做异地医保的情况下，不报错，主要是获取异地医保的对照信息
            if (this.alYDCenterCompore != null && this.alYDCenterCompore.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in this.alYDCenterCompore)
                {
                    if (!this.hsYDComPare.Contains(obj.User01))
                    {
                        this.hsYDComPare.Add(obj.User01, obj);
                    }
                }
            }
            //先不管异地医保信息
            //else
            //{
            //    MessageBox.Show("读取不到异地医保对照！");
            //}
            #endregion

            this.WindowState = FormWindowState.Maximized;
            this.tbPatientNo.Focus();
            this.tbPatientNo.Select();
		}

        bool isContinue = false;
		private void tbPatientNo_myEvent()
		{
       //     this.tbQurey.Enabled = true;
			if(tbPatientNo.InpatientNo == "" || tbPatientNo.InpatientNo == null)
			{
				MessageBox.Show("您输入的住院号错误,请重新输入!");
				return;			
			}
			else
			{
				
				pInfo = myInpatient.QueryPatientInfoByInpatientNO(tbPatientNo.InpatientNo);

                this.tbPatientNo.Text = pInfo.PID.ID;

				FS.HISFC.Models.RADT.PatientInfo siInfo = mySiPatient.GetSIPersonInfo(tbPatientNo.InpatientNo);
				if(siInfo == null)
				{
					MessageBox.Show("住院号:" + pInfo.ID + "的患者" + pInfo.Name + "不是医保患者,请核准后再输入!");
					return ;
				}
                FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                ArrayList applys = feeIntegrate.QueryReturnApplys(pInfo.ID, false);
                if (applys == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(feeIntegrate.Err));
                    return;
                }
                if (applys.Count > 0) //存在退费申请提示是否需要做院登记
                {
                    string itemInfo = "项目:\r\n";
                    foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in applys)
                    {
                        itemInfo += returnApply.Item.Name + "--(" + FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetDepartmentName(returnApply.ExecOper.Dept.ID) + ")" + "\r\n";
                    }

                    MessageBox.Show("还有未确认的退费申请，请先确认退费申请再进行中途结算？" + itemInfo, "警告"
                             , MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    return;
                }

                bool isSpecailPact = myInterface.IsPactDealByInpatient(pInfo.Pact.ID);
                pInfo.SIMainInfo = siInfo.SIMainInfo;

				this.tbName.Text = pInfo.Name;
                this.lblPatientInfo.Text = "姓名：" + pInfo.Name + " 就医登记号：" + pInfo.SIMainInfo.RegNo + " 性别：" + this.pInfo.Sex.Name + " 身份证号：" + pInfo.IDCard;
                this.lbldiag.Text = pInfo.MainDiagnose;

                DateTime inDate = pInfo.PVisit.InTime;

                //获取上次结算日期
                ArrayList alBalance = inpatientFeeManager.QueryBalancesByInpatientNO(pInfo.ID, "I");
                if (alBalance != null)
                {
                    foreach (FS.HISFC.Models.Fee.Inpatient.Balance balancedHead in alBalance)
                    {
                        if (balancedHead.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid &&
                            inDate < balancedHead.EndTime)
                        {
                            inDate = balancedHead.EndTime.AddSeconds(1);
                        }
                    }
                }

                this.dtBegin.Value = inDate;
				this.dtEnd.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

                DateTime dtMyEnd = new DateTime();
                if (pInfo.PVisit.OutTime <= new DateTime(1900, 1, 1, 1, 1, 1))
                {
                    dtMyEnd = myInterface.GetDateTimeFromSysDateTime().Date;
                }
                else
                {
                    dtMyEnd = pInfo.PVisit.OutTime.Date;
                }
                TimeSpan ts = dtMyEnd - pInfo.PVisit.InTime.Date;
                int days = ts.Days;
                if (pInfo.PID.ID.IndexOf('L') > -1)//临时号
                {
                    this.button1.Visible = true;
                    isContinue = true;
                }       
                else
                {
                    if (days > 90)
                    {
                        this.btn90day.Visible = true;
                        isContinue = true;
                    }
                    else if (days > 30)
                    {
                        this.button1.Visible = true;
                        isContinue = true;
                    }
                }
               // this.btn90day.Visible = true; //调试用例

				this.btnOk.Enabled = true;
                //if(pInfo.SIMainInfo.EmplType == "2" && pInfo.PVisit.InTime < new DateTime(2007,2,1,0,0,0))
                //{
                //    MessageBox.Show("广州医保离休患者2月1日前入院要进行中途结算。 ");
                //}
                if ((pInfo.PVisit.InState.ID.ToString() != "I"))
                {
                    #region 跨月业务提示
                    //if (pInfo.PVisit.InTime.Month != pInfo.PVisit.OutTime.Month ||
                    //    pInfo.PVisit.InTime.Year != pInfo.PVisit.OutTime.Year)
                    //{
                    //    MessageBox.Show("此病人有跨月业务，请先在医保系统查询！");
                    //}
                    #endregion
                    #region 病人备注信息提示
                    //如果患者有备注信息，弹出提示框
                    if (!string.IsNullOrEmpty(pInfo.Memo))
                    {
                        if (DialogResult.No == MessageBox.Show(pInfo.Memo + System.Environment.NewLine + "是否继续数据上传？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                        {
                            return;
                        }

                    }
                    #endregion
                }
                else if(!isContinue)
                {
                    MessageBox.Show("此病人在院,还没有出院登记");
                }

                #region
                #endregion

            }
		}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            Management.SIConnect myConn = null;
            try
            {
                myConn = new global::GZSI.Management.SIConnect();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("连接数据库失败!" + myConn.Err + ex.Message);
            }

            FS.HISFC.Models.RADT.PatientInfo siInfo = mySiPatient.GetSIPersonInfo(tbPatientNo.InpatientNo);
            if (siInfo == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("获得患者医保信息出错!");
                return;
            }

            int iReturn = myConn.DeleteItemList(siInfo.SIMainInfo.RegNo);
            int result = myConn.DeleteItemListEX(siInfo.SIMainInfo.RegNo);
            if (iReturn == -1 || result == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("删除共享区明细失败!" + myConn.Err);
                return;
            }

            iReturn = myInterface.DeleteShareData(siInfo);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("删除本地医保费用明细失败!" + myInterface.Err);
                return;
            }

            myConn.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("删除共享区明细成功!");
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iReturn = myInterface.UpdateAllDetailFlag(tbPatientNo.InpatientNo, "0");
            int result = 0;
			if(iReturn < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("更新失败!" + myInterface.Err);
				return;
			}
			if(iReturn == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("没有可更新的记录!");
				return;
			}

            Management.SIConnect myConn = null;
            try
            {
                myConn = new  global::GZSI.Management.SIConnect();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("连接数据库失败!" + myConn.Err + ex.Message);
            }

            FS.HISFC.Models.RADT.PatientInfo siInfo = mySiPatient.GetSIPersonInfo(tbPatientNo.InpatientNo);
            if (siInfo == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("获得患者医保信息出错!");
                return;
            }

            iReturn = myConn.DeleteItemList(siInfo.SIMainInfo.RegNo);
            result = myConn.DeleteItemListEX(siInfo.SIMainInfo.RegNo);
            if (iReturn == -1 || result == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("取消上传失败!" + myConn.Err);
                return;
            }

            iReturn = myInterface.DeleteShareData(siInfo);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                myConn.RollBack();
                MessageBox.Show("删除本地医保费用明细失败!" + myInterface.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            myConn.Commit();
            MessageBox.Show("取消上传成功!");

		}
        /// <summary>
        /// 重新关联医保就医登记号、更新患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlter_Click(object sender, System.EventArgs e)
        {
            if (this.pInfo.ID == string.Empty || this.pInfo.ID == null)
            {
                MessageBox.Show("请输入患者信息！");
                return;
            }
            else if (this.pInfo.Pact.PayKind.ID != "02")
            {
                MessageBox.Show("该患者非医保患者！");
                return;
            }

            FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            long returnYB = 0;
            medcareProxy.SetPactCode(this.pInfo.Pact.ID);
            //医保待遇接口连接
            returnYB = medcareProxy.Connect();
            if (returnYB == -1)
            {
                MessageBox.Show("待遇化接口出错！" + medcareProxy.ErrMsg);
                return;
            }
            //获取医保患者信息
            this.pInfo.Insurance.Memo = "重选患者";
            returnYB = medcareProxy.GetRegInfoInpatient(this.pInfo);
            if (returnYB < 0)
            {
                MessageBox.Show("初始化待遇接口出错！" + medcareProxy.ErrMsg);
                return;
            }
            medcareProxy.SetPactCode(this.pInfo.Pact.ID);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            medcareProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnYB = medcareProxy.Connect();
            if (returnYB == -1)
            {
                MessageBox.Show("待遇化接口出错！" + medcareProxy.ErrMsg);
                return;
            }

            //更新住院主表信息
            returnYB = this.SetSIPatientToLocal(this.pInfo);
            if (returnYB < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareProxy.Rollback();
                MessageBox.Show("更新医保信息失败！" + medcareProxy.ErrMsg);
                return;
            }
            //更新医保主表信息
            returnYB = medcareProxy.UploadRegInfoInpatient(this.pInfo);
            if (returnYB < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareProxy.Rollback();
                MessageBox.Show("更新医保信息失败！" + medcareProxy.ErrMsg);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            medcareProxy.Commit();
            MessageBox.Show("更新医保患者信息成功！");
        }

        /// <summary>
        /// 设置患者信息

        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private int SetSIPatientToLocal(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            if (FS.FrameWork.Management.PublicTrans.Trans != null)
            {
                inPatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }
            FS.HISFC.Models.RADT.PatientInfo p = inPatientMgr.QueryPatientInfoByInpatientNO( patientInfo.ID );
            if (p == null || p.ID == string.Empty)
            {
                return -1;
            }
            p.Name = patientInfo.Name;
            p.SIMainInfo = patientInfo.SIMainInfo;
            p.IDCard = patientInfo.IDCard;
            p.CompanyName = patientInfo.CompanyName;
            p.Birthday = patientInfo.Birthday;
            p.Sex = patientInfo.Sex;


            if (inPatientMgr.UpdatePatient( p ) == -1)
            {
                return -1;
            }
            return 1;
        }

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == this.tbQurey)
			{
				this.QureyFee();
			}
			else if(e.Button==this.tbUpload)
			{
				this.btnOk_Click(new object(),new System.EventArgs());
				return;
			}
			else if(e.Button==this.tbCancel)
			{
				this.btnCancel_Click(new object(),new System.EventArgs());
				return;
			}
			else if(e.Button==this.tbDelete)
			{
				this.btnDelete_Click(new object(),new System.EventArgs());
				return;
			}
            else if (e.Button == this.tbAlter)
            {
                this.btnAlter_Click( new object(), new EventArgs() );
                return;
            }
            else if (e.Button == this.tbBalance)
            {
                this.ucUploadDetail1.SIBalance();
                return;
            }
            else if (e.Button == this.tbQuit)
            {
                this.Close();
                return;
            }
		}

		private void btnChangedate_Click(object sender, System.EventArgs e)
		{
			//把费用日期大于出院日期的费用更改到出院日期

			if(this.ucUploadDetail1.PInfo == null)return;
			if(this.ucUploadDetail1.PInfo.PVisit.OutTime == DateTime.MinValue)
			{
				MessageBox.Show("患者没有出院日期！");
				return;
			}
			if(this.ucUploadDetail1.PInfo.PVisit.OutTime < this.ucUploadDetail1.PInfo.PVisit.InTime)
			{
				MessageBox.Show("患者mou有出院日期！");
				return;
			}

            this.ucUploadDetail1.SetFeeDateBeforeOutDate();

            //FS.NFC.Management.Transaction t = new FS.NFC.Management.Transaction(FS.NFC.Management.Connection.Instance);
            //t.BeginTransaction();
            //FS.HISFC.Management.Fee.Interface myInterface = new FS.HISFC.Management.Fee.Interface();
            //myInterface.SetTrans(t.Trans);
            //int iReturn = myInterface.UpdateSIFeeDate(tbPatientNo.InpatientNo, this.ucUploadDetail1.PInfo.PVisit.OutTime);
            //if (iReturn < 0)
            //{
            //    t.RollBack();
            //    MessageBox.Show("更新失败!" + myInterface.Err);
            //    return;
            //}
            //t.Commit();
            //MessageBox.Show("更新成功!");
		}

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="input"></param>
        private void FilterItem( string input)
        {
            string filterString = "";
            input = input.Trim().ToUpper();
            filterString = "拼音码" + " like '%" + input + "%'" + "or" + " 五笔码" + " like '%" + input + "%'" + "or" + " 自定义码" + " like '" + input + "%'" + "or" + " 项目名称" + " like '" + input + "%'";
            this.dsDetail.DefaultView.RowFilter = filterString;      
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            FilterItem(this.txtFilter.Text);
        }

        private void btn90day_Click(object sender, EventArgs e)
        {
            ///得包含当天
           DateTime dtTemp = this.dtBegin.Value.Date.AddDays(89).Date;
           this.dtEnd.Value = new DateTime(dtTemp.Year, dtTemp.Month, dtTemp.Day, 23, 59, 59);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dtTemp = this.dtBegin.Value.Date.AddDays(30).Date;
            this.dtEnd.Value = new DateTime(dtTemp.Year, dtTemp.Month, dtTemp.Day, 23, 59, 59);
        }
	}
}