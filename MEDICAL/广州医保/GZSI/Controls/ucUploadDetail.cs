using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using FS.HISFC.Models.Fee;
using FS.HISFC.BizLogic.Fee;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Fee.Inpatient;

namespace GZSI.Controls
{
    public partial class ucUploadDetail : UserControl
    {
        public ucUploadDetail()
        {
            InitializeComponent();
        }
		
		#region 变量

		private ArrayList alDrugDetail; //要上传的药品明细
		private ArrayList alUndrugDetail;//要上传的非药品明细

		private System.Windows.Forms.ProgressBar pbDrug;
		private System.Windows.Forms.GroupBox gbDrug;
		private System.Windows.Forms.ProgressBar pbUndrug;
		private System.Windows.Forms.GroupBox gbUndrug;//医保上传业务层

		private PatientInfo pInfo;//医保患者个人基本信息


		#endregion

		#region 属性

		/// <summary>
		/// 要上传的药品明细
		/// </summary>
		public ArrayList DrugDetails
		{
			get
			{
				return alDrugDetail;
			}
			set
			{
				alDrugDetail = value;
			}
		}
		/// <summary>
		/// 要上传的非药品明细

		/// </summary>
		public ArrayList UndrugDetails
		{
			get
			{
				return alUndrugDetail;
			}
			set
			{
				alUndrugDetail = value;
			}
		}
		/// <summary>
		/// 医保患者个人基本信息

		/// </summary>
		public PatientInfo PInfo
		{
			get
			{
				return pInfo;
			}
			set
			{
				pInfo = value;
			}
		}

		#endregion

		#region 函数

        /// <summary>
        /// 验证是否可以进行费用上传
        /// </summary>
        /// <returns></returns>
        public int CheckValid()
        {
            if (pInfo == null)
            {
                MessageBox.Show("没有获得患者基本信息,请赋值!");
                return -1;
            }
            if (string.IsNullOrEmpty(pInfo.SIMainInfo.RegNo))
            {
                MessageBox.Show("没有获取到就医登记号，请点击 重选医保患者 按钮，更新对应的挂号信息后，再输入住院号进行上传!");
                return -1;
            }
            if (alDrugDetail == null)
            {
                MessageBox.Show("药品明细为空,请赋值!");
                return -1;
            }
            if (alUndrugDetail == null)
            {
                MessageBox.Show("非药品明细为空,请赋值!");
                return -1;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in this.alDrugDetail)
            {
                if (item.User03 == "未对照")
                {
                    DialogResult r = MessageBox.Show("上传的费用中包含未对照的项目,未对照项目作自费处理,\n 是否继续上传", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.No)
                    {
                        return -1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in this.UndrugDetails)
            {
                if (item.User03 == "未对照")
                {
                    DialogResult r = MessageBox.Show("上传的费用中包含未对照的项目,未对照项目作自费处理,\n 是否继续上传", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.No)
                    {
                        return -1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 医保患者结算
        /// </summary>
        /// <returns></returns>
        public int SIBalance()
        {
            Medicare Medicare = new Medicare();

            #region 医保接口结算

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            Medicare.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            long returnMedcareValue = Medicare.Connect();
            if (returnMedcareValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Medicare.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口初始化失败") + Medicare.ErrMsg);
                return -1;
            }
            //获取医保挂号信息
            int returnValue = Medicare.GetRegInfoInpatient(pInfo);
            if (returnValue != 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获得待遇患者基本信息失败!") + Medicare.ErrCode);

                Medicare.Disconnect();
            }

            ArrayList alFeeInfo = new ArrayList();
            returnMedcareValue = Medicare.PreBalanceInpatient(this.pInfo, ref alFeeInfo);
            if (returnMedcareValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Medicare.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口门诊结算失败") + Medicare.ErrMsg);
                return -1;
            }

            //爱博恩医院医保报销金额处理为医保账户金额
            #region 医保账户

            Management.SILocalManager myInterface = new Management.SILocalManager();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            decimal amount = pInfo.SIMainInfo.PubCost + pInfo.SIMainInfo.PayCost;
            if (myInterface.SaveAccount(pInfo.PID.CardNO, amount) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Medicare.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("创建医保账户信息失败！") + myInterface.Err);
                return -1;
            }
            #endregion

            #region 本地存储医保收费明细

            //DateTime dtNow = myInterface.GetDateTimeFromSysDateTime();
            ////这里的费用明细从医保前置机再查下吧

            //int iReturn = myInterface.InsertShareData(pInfo, feeDetails, dtNow);
            //if (iReturn == -1)
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("插入本地医保费用明细信息失败！") + myInterface.Err);
            //    return -1;
            //}

            #endregion

            #endregion

            Medicare.Commit();
            Medicare.Disconnect();
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("医保结算成功");
            return 1;
        }

        /// <summary>
        /// 上传费用到前置机
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int Upload(DateTime beginTime, DateTime endTime)
        {
            Medicare Medicare = new  Medicare();
            if (Medicare.Connect() == -1)
            {
                return Err("连接医保服务器失败!,请重新配置连接 "+Medicare.ErrMsg);
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            Medicare.BeginTranscation();
            Medicare.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (Medicare.MyUploadFeeDetailsInpatient(this.pInfo,beginTime,endTime,ref this.alDrugDetail) == -1)
            {
                Medicare.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                return Err(Medicare.ErrMsg);
            }
            if (Medicare.MyUploadFeeDetailsInpatient(this.pInfo, beginTime, endTime, ref this.alUndrugDetail) == -1)
            {
                Medicare.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                return Err(Medicare.ErrMsg);
            }
            Medicare.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
       
        private int Err(string errMsg)
        {
            MessageBox.Show(errMsg);
            return -1;
        }

        public void SetFeeDateBeforeOutDate()
        {
            if (this.PInfo.PVisit.OutTime == DateTime.MinValue)
            {
                MessageBox.Show("患者没有出院日期！");
                return;
            }
            if (this.PInfo.PVisit.OutTime < this.PInfo.PVisit.InTime)
            {
                MessageBox.Show("患者mou有出院日期！");
                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in this.DrugDetails)
            {
                if (item.ChargeOper.OperTime > pInfo.PVisit.OutTime)
                {
                    item.ChargeOper.OperTime = pInfo.PVisit.OutTime;
                }
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in this.UndrugDetails)
            {
                if (item.ChargeOper.OperTime > pInfo.PVisit.OutTime)
                {
                    item.ChargeOper.OperTime = pInfo.PVisit.OutTime;
                }
            }

        }

        /// <summary>
        /// 费用日期校验
        /// </summary>
        /// <returns></returns>
        public bool HasFeeDateAfterOutDate()
        {
            if (this.PInfo.PVisit.OutTime == DateTime.MinValue)
            {
                MessageBox.Show("患者没有出院日期！");
                return false;
            }
            if (this.PInfo.PVisit.OutTime < this.PInfo.PVisit.InTime)
            {
                MessageBox.Show("患者mou有出院日期！");
                return false;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in this.DrugDetails)
            {
                if (item.ChargeOper.OperTime > pInfo.PVisit.OutTime)
                {
                    return true;
                }
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in this.UndrugDetails)
            {
                if (item.ChargeOper.OperTime > pInfo.PVisit.OutTime)
                {
                    return true;
                }
            }
            return false;
        }

        #region 屏蔽
        //public int Upload()
        //{
        //    if(pInfo == null)
        //    {
        //        MessageBox.Show("没有获得患者基本信息,请赋值!");
        //        return -1;
        //    }
        //    if(alDrugDetail == null)
        //    {
        //        MessageBox.Show("药品明细为空,请赋值!");
        //        return -1;
        //    }
        //    if(alUndrugDetail == null)
        //    {
        //        MessageBox.Show("非药品明细为空,请赋值!");
        //        return -1;
        //    }
        //    try
        //    {
        //         conn = new SIConnect();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show("连接医保服务器失败!,请重新配置连接" + ex.Message);
        //        return -1;
        //    }

        //    Interface myInterface = new Interface();
        //    FS.NFC.Management.Transaction t = new FS.NFC.Management.Transaction(FS.NFC.Management.Connection.Instance);

        //    FS.HISFC.Management.Pharmacy.Item iMgr = new FS.HISFC.Management.Pharmacy.Item();//药品
        //    FS.HISFC.Management.Fee.Item itemMgr = new FS.HISFC.Management.Fee.Item();//非药品


        //    t.BeginTransaction();
        //    myInterface.SetTrans(t.Trans);
        //    iMgr.SetTrans(t.Trans);
        //    itemMgr.SetTrans(t.Trans);		


        //    int i = 1; //药品上传进度
        //    int j = 1; //非药品上传进度

        //    int countDrug = alDrugDetail.Count;
        //    int countUndrug = alUndrugDetail.Count;
        //    int iReturn = 0;

        //    if(countDrug > 0)
        //    {
        //        gbDrug.Text = "正在上传药品明细...";
        //        //药品明细上传				
        //        foreach(FeeItemList obj in alDrugDetail)
        //        {
        //            //更新药品明细表,已上传标志									
        //            iReturn = myInterface.UpdateUploadedDetailFlag(obj.RecipeNO, obj.SendSequence, "1");
        //            if(iReturn == 0)//并发,或者条件不具备
        //            {
        //                continue;
        //            }
        //            else if(iReturn == -1)//错误
        //            {						
        //                t.RollBack();
        //                conn.RollBack();
        //                MessageBox.Show("更新上传标志出错!" + myInterface.Err);
        //                return -1;
        //            }
        //            //将药品编码转换成自定义码
        //            FS.HISFC.Models.Pharmacy.Item drugItem;
        //            try
        //            {
        //                obj.Item = (FS.HISFC.Models.Pharmacy.Item)obj.Item;
        //                drugItem = iMgr.GetItem(obj.Item.ID);
        //                obj.Item.ID = drugItem.UserCode;
        //            }
        //            catch(Exception ex)
        //            {
        //                t.RollBack();
        //                if(iMgr.Err !=null||iMgr.Err !="")
        //                {
        //                    MessageBox.Show("药品编码转换自定义码出错"+iMgr.Err,"提示");
        //                    return -1;
        //                }
        //                else
        //                {
        //                    MessageBox.Show("药品编码转换自定义码出错"+ex.Message,"提示");
        //                    return -1;
        //                }
        //            }

        //            //上传医保服务器

        //            iReturn = conn.InsertItemList(pInfo, obj);
        //            if(iReturn == -1)
        //            {
        //                t.RollBack();
        //                conn.RollBack();
        //                MessageBox.Show("医保上传明细出错!" + conn.Err);
        //                return -1;
        //            }
        //            this.pbDrug.Value = i*100 / countDrug ;
        //            Application.DoEvents();
        //            i++;
        //        }
        //        gbDrug.Text = "药品明细上传完毕.";

        //    }
        //    if(countUndrug > 0)
        //    {
        //        gbUndrug.Text = "正在上传非药品明细...";
        //        //非药品明细上传

        //        foreach(FeeItemList obj in alUndrugDetail)
        //        {
        //            //更新非药品明细表,已上传标志

        //            iReturn = myInterface.UpdateUploadedDetailFlag(obj.RecipeNO, obj.SendSequence, "2");
        //            if(iReturn == 0)//并发,或者条件不具备
        //            {
        //                continue;
        //            }
        //            else if(iReturn == -1)//错误
        //            {
        //                t.RollBack();
        //                conn.RollBack();
        //                MessageBox.Show("更新上传标志出错!" + myInterface.Err);
        //                return -1;
        //            }
        //            //非药品编码转化成自定义码
        //            FS.HISFC.Models.Fee.Item.Undrug item;
        //            try
        //            {
        //                item = (itemMgr.Query(obj.Item.ID, "1"))[0] as FS.HISFC.Models.Fee.Item.Undrug;
        //                obj.Item.ID = item.UserCode;
        //            }
        //            catch(Exception ex)
        //            {
        //                t.RollBack();
        //                if(itemMgr.Err !=null||itemMgr.Err !="")
        //                {
        //                    MessageBox.Show("药品编码转换自定义码出错"+itemMgr.Err,"提示");
        //                    return -1;
        //                }
        //                else
        //                {
        //                    MessageBox.Show("药品编码转换自定义码出错"+ex.Message,"提示");
        //                    return -1;
        //                }
        //            }
        //            //上传医保服务器

        //            iReturn = conn.InsertItemList(pInfo, obj);
        //            if(iReturn == -1)
        //            {
        //                t.RollBack();
        //                conn.RollBack();
        //                MessageBox.Show("医保上传明细出错!" + conn.Err);
        //                return -1;
        //            }
        //            this.pbUndrug.Value = j *100 / countUndrug;
        //            Application.DoEvents();
        //            j++;
        //        }
        //        gbUndrug.Text = "非药品明细上传完毕.";
        //    }

        //    conn.Commit();
        //    t.Commit();
        //    return 0;

        //}
        #endregion
		#endregion

	}
}
