using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    /// <summary>
    /// 新农保本地预结算
    /// </summary>
    class XnhLocalBalance : Neusoft.FrameWork.Management.Database, IJob
    {
        #region "变量"

        private string errMsg = "";


        //服务器时间
        public DateTime dtServerDateTime;

        //上次新农保本地结算时间
        public DateTime dtPreFixFeeDateTime;

        //显示的文本框,引用主窗口的文本框
        public Neusoft.FrameWork.WinForms.Controls.NeuRichTextBox rtbLogo = null;

        //医保本地预结算管理类
        SILocalManager mySILocalManager = new  SILocalManager();

        //住院费用管理类
        Neusoft.HISFC.BizLogic.Fee.InPatient inpatientManager = new Neusoft.HISFC.BizLogic.Fee.InPatient();
      
        #endregion

        public string ErrMsg
        {
            get
            {
                return errMsg;
            }
            set
            {
                errMsg = value;
            }
        }

        #region "函数"

        #region 本地预结算

        /// <summary>
        /// 计算费用项目的费用金额（包括 TOT_COST,PUB_COST,PAY_COST,OWN_COST）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        private int RecomputeFeeItemListInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {

            int returnValue = 0;

            Neusoft.HISFC.Models.SIInterface.Compare objCompare = new Neusoft.HISFC.Models.SIInterface.Compare();

            returnValue = mySILocalManager.GetCompareSingleItem(patient.Pact.ID, item.Item.ID, ref objCompare);

            if (returnValue == -1)
            {
                this.ErrMsg = mySILocalManager.Err;
                return returnValue;
            }
            if (returnValue == -2)
            {
                objCompare.CenterItem.Rate = 1;
            }

            item.FT.OwnCost = Neusoft.FrameWork.Public.String.FormatNumber(item.FT.TotCost * objCompare.CenterItem.Rate, 2);
            item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
            item.FT.PayCost = 0;
            item.FT.DerateCost = 0;//重算不考虑减免金额

            return 0;
        }
        /// <summary>
        /// 计算所有的费用，并更新费用明细（fin_ipb_medicinelist，fin_ipb_itemlist,fin_ipb_feeinfo、fin_ipr_inmaininfo）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public int ComputeFeeCostInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeItemLists)
        {
            for (int i = 0; i <= feeItemLists.Count - 1; ++i)
            {

                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = feeItemLists[i] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemOld = item.Clone();

                #region 重新计算费用

                //判断身份变更费用明细
                if (item.Patient.Pact.ID != patient.Pact.ID) continue;

                if (RecomputeFeeItemListInpatient(patient, ref item) < 0)
                {
                    return -1;
                }
                #endregion

                #region 更新费用明细

                //更新费用明细表（fin_ipb_itemlist,fin_ipb_medicinelist）
                if (item.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //更新药品费用明细表（fin_ipb_medicinelist）
                    if (mySILocalManager.UpdateMedItemList(item) < 0)
                    {
                        return -1;
                    }
                }
                else
                {
                    //更新非药品费用明细表（fin_ipb_itemlist）
                    if (mySILocalManager.UpdateItemList(item) < 0)
                    {
                        return -1;
                    }
                }
                #endregion

                #region 更新费用汇总
                //根据处方号、最小费用、执行科室 获取费用汇总信息
                Neusoft.HISFC.Models.Base.FT ft = mySILocalManager.QueryFeeInfo(itemOld.RecipeNO, itemOld.Item.MinFee.ID, itemOld.ExecOper.Dept.ID);
                if (ft == null)
                {
                    return -1;
                }

                //更新费用汇总
                //增加重新计算的费用
                ft.TotCost = ft.TotCost - itemOld.FT.TotCost + item.FT.TotCost;
                ft.OwnCost = ft.OwnCost - itemOld.FT.OwnCost + item.FT.OwnCost;
                ft.PubCost = ft.PubCost - itemOld.FT.PubCost + item.FT.PubCost;
                ft.PayCost = ft.PayCost - itemOld.FT.PayCost + item.FT.PayCost;


                //更新费用汇总操作
                if (-1 == mySILocalManager.UpdateFeeInfo(ft, itemOld.RecipeNO, itemOld.Item.MinFee.ID, itemOld.ExecOper.Dept.ID))
                {
                    return -1;

                }
                #endregion

                #region 更新住院主表
                //根据住院流水号，获取住院主表费用
                Neusoft.HISFC.Models.Base.FT ftMain = mySILocalManager.QueryInMainInfo(patient.ID);
                if (ftMain == null) return -1;


                //更新费用汇总
                //增加重新计算的费用
                ftMain.TotCost = ftMain.TotCost - itemOld.FT.TotCost + item.FT.TotCost;
                ftMain.OwnCost = ftMain.OwnCost - itemOld.FT.OwnCost + item.FT.OwnCost;
                ftMain.PubCost = ftMain.PubCost - itemOld.FT.PubCost + item.FT.PubCost;
                ftMain.PayCost = ftMain.PayCost - itemOld.FT.PayCost + item.FT.PayCost;


                //更新费用汇总操作
                if (-1 == mySILocalManager.UpdateInMainInfo(ftMain, patient.ID))
                {
                    return -1;
                }
                #endregion



            }
            return 1;
        }


        /// <summary>
        /// 本地预结算
        /// 预计农保预结算Job执行时间晚上 2:00
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isChangePact">身份变更 true 不是身份变更 false</param>
        /// <returns></returns>
        public int LocalComputeInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, bool isChangePact)
        {

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            mySILocalManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            inpatientManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            //查询农保预结算JOB 的上次执行时间
            DateTime dtLast = DateTime.MinValue;
            string lastTime = mySILocalManager.GetJobLastExecDate(patient.ID, patient.Pact.ID);
            if (string.IsNullOrEmpty(lastTime))
            {
                dtLast = patient.PVisit.InTime;
            }
            else
            {
                dtLast = Neusoft.FrameWork.Function.NConvert.ToDateTime(lastTime);
            }

            if (dtLast == DateTime.MinValue)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }


            DateTime dtNow = mySILocalManager.GetDateTimeFromSysDateTime();

            if (isChangePact)
            {
                //获取该患者所有在院未结费用，按每天费用处理

            }
            else
            {
                //非身份变更预结算
                if (patient.PVisit.InState.ID.ToString() == "C" || patient.PVisit.InState.ID.ToString() == "B")
                {
                    #region 出院预结算
                    DateTime dtFirst = dtLast.Date;
                    if (dtLast != patient.PVisit.InTime && dtLast.AddDays(1) > dtNow)
                    {
                        //出院当天费用的预结算
                        if (-1 == ExtcutePreBalance(patient, dtFirst))
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }
                    else
                    {

                        try
                        {
                            //出院前几天为跑农保预结算处理
                            System.TimeSpan interval = dtNow.Date - dtLast.Date;
                            if (interval.Days >= 1)
                            {
                                //多天未执行农保预结算，按每天来预结算费用                           
                                for (int i = 0; i <= interval.Days; i++)
                                {

                                    if (-1 == ExtcutePreBalance(patient, dtFirst))
                                    {
                                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                        return -1;
                                    }
                                    dtFirst = dtFirst.AddDays(1);

                                }


                            }

                        }
                        catch { }
                    }

                    #endregion //出院预结算

                    #region 更新新农合预结算时间
                    //更新新农合预结算时间
                    if (-1 == mySILocalManager.InsertOrUpdateLocalBalanceTime(patient, mySILocalManager.GetDateTimeFromSysDateTime()))
                    {
                        this.ErrMsg = mySILocalManager.Err;
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    #region 在院预结算
                    //在院预结算
                    if (dtLast!=patient.PVisit.InTime && dtLast.AddDays(1) > dtNow)
                    {
                        //已经预结算
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        return 1;
                    }
                    else
                    {
                        try
                        {
                            DateTime dtFirst = dtLast.Date;
                            System.TimeSpan interval = dtNow.Date - dtLast.Date;
                            if (interval.Days >= 1)
                            {
                                //多天未执行农保预结算，按每天来预结算费用

                                for (int i = 0; i < interval.Days; i++)
                                {

                                    if (-1 == ExtcutePreBalance(patient, dtFirst))
                                    {
                                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                        return -1;
                                    }
                                    dtFirst = dtFirst.AddDays(1);

                                }
                                #region 更新新农合预结算时间
                                //更新新农合预结算时间
                                DateTime dt = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 2, 0, 0);
                                if (-1 == mySILocalManager.InsertOrUpdateLocalBalanceTime(patient, dt))
                                {
                                    this.ErrMsg = mySILocalManager.Err;
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    return -1;
                                }
                                #endregion

                            }

                        }
                        catch { }

                    }

                    #endregion //end 在院预结算

                    
                }


            }


            Neusoft.FrameWork.Management.PublicTrans.Commit();
    //        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 按时间执行当日本地预结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private int ExtcutePreBalance(Neusoft.HISFC.Models.RADT.PatientInfo patient, DateTime date)
        {
            DateTime dtBegin = date.Date;
            DateTime dtEnd = new DateTime(dtBegin.Year, dtBegin.Month, dtBegin.Day, 23, 59, 59);

            try
            {
                //上传当日费用明细（当日出院，请手工上传）
                this.UploadFeeDetail(patient, date);
            }
            catch(Exception exp)
            {
                this.ErrMsg = "上传费用明细出错！ 住院号："+patient.PID.PatientNO+" 费用日期："+date.ToShortDateString()  +"   "+exp.Message;
            }

            ArrayList alFeeItemList = new ArrayList();
            alFeeItemList = inpatientManager.QueryMedicineListsForBalance(patient.ID, dtBegin, dtEnd);

            alFeeItemList.AddRange(inpatientManager.QueryItemListsForBalance(patient.ID, dtBegin, dtEnd));

            if (alFeeItemList != null && alFeeItemList.Count > 0)
            {
                return this.ComputeFeeCostInpatient(patient, ref alFeeItemList);
            }
           
            return 1;
        }


        #endregion

        #region 上传费用明细
        XNH.Models.PYNBMainInfo myNBpatient = new  HISTIMEJOB.XNH.Models.PYNBMainInfo();
        XNH.Management.SIConnect myConn = new HISTIMEJOB.XNH.Management.SIConnect();
        string pactcode = string.Empty;

        private int UploadFeeDetail(Neusoft.HISFC.Models.RADT.PatientInfo patient, DateTime dtDate)
        {
            if (myConn != null)
            {
                myConn.Close();
                myConn.Open();
            }

            Neusoft.FrameWork.Models.NeuObject Neuobj = new Neusoft.FrameWork.Models.NeuObject();
            string NBcardNo = string.Empty;
            XNH.Management.SILocalManager fyfunciton = new  HISTIMEJOB.XNH.Management.SILocalManager();

            string patientNO = string.Empty;
            patientNO = patient.PID.PatientNO;
            if (patientNO == "" || patientNO == null)
            {
                this.errMsg = "输入的住院号错误,请重新输入:"+patientNO;
                return -1;
            }
            else
            {

                Neuobj = fyfunciton.GetPatOutInfo(ref NBcardNo, patient.ID, ref myNBpatient);
                if (myNBpatient != null && !string.IsNullOrEmpty(myNBpatient.Memo3))
                {
                    pactcode = myNBpatient.Memo3;
                }
                myNBpatient = myConn.GetRegPersonInfo(NBcardNo);
                myNBpatient.Medi_sn = Neuobj.ID;
                myNBpatient.Medi_nn = patientNO;
                fyfunciton.GetPatOutInfo(patientNO, ref myNBpatient);		//为了获取出院日期					
                if (myNBpatient == null)
                {
                   this.errMsg="住院号:" + patientNO + "的患者不是农保患者或未做农保登记,请核准后再输入!";
                    return -1;
                }
                // 获得患者信息				
                // 获取就医登记号
                myConn.GetMediSnInfo(ref myNBpatient);
                //this.txtIDNo.Text = myNBpatient.Man_id;
                //this.txtName.Text = myNBpatient.Man_name;
                //this.txCard_no.Text = myNBpatient.Card_no;
                //this.txApplYear.Text = myNBpatient.Appl_year.ToString();
                //this.txtRegNo.Text = myNBpatient.Medi_no;
                // 已经结算信息
                //myConn.GetSendState(ref myNBpatient);
                int result = this.myConn.GetFeeItemCount(myNBpatient, dtDate);
                if (result > 0)
                {
                    this.errMsg = "已上传:"+patientNO;
                    return -1;
                }


                XNH.Management.SILocalManager pyfuction = new HISTIMEJOB.XNH.Management.SILocalManager();
                System.Data.DataSet dsItem = new System.Data.DataSet();
                pyfuction.QueryFeeDetail(patientNO, patient.Pact.ID, dtDate, ref dsItem);

                if (dsItem==null || dsItem.Tables.Count==0||dsItem.Tables[0].Rows.Count==0)
                {
                    return -1;
                }

                decimal sumAccount = 0;
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList obj;
                myConn.BeginTranscation();//开始事务
                // 一条一条上传
                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    System.Data.DataRow dr = dsItem.Tables[0].Rows[i];
                    obj = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();
                    obj.Item.User01 = dr["项目编码"].ToString();
                    obj.Item.User02 = dr["项目名称"].ToString();
                    obj.Item.ID = dr["农保编码"].ToString();
                    obj.Item.Name = dr["农保名称"].ToString();
                    obj.Item.PriceUnit = dr["单位"].ToString();
                    obj.Item.Qty = Neusoft.FrameWork.Function.NConvert.ToDecimal(dr["数量"].ToString());
                    obj.Item.MinFee.User01 = dr["金额"].ToString();
                    //有修改
                    obj.FTRate.PayRate = Neusoft.FrameWork.Function.NConvert.ToDecimal(dr["折扣比"].ToString());
                    obj.FTRate.PubRate = 1 - obj.FTRate.PayRate;


                    obj.Item.MinFee.User02 = dr["折扣比"].ToString();
                    sumAccount += Neusoft.FrameWork.Function.NConvert.ToDecimal(obj.Item.MinFee.User01);
                    if (myConn.InsertNBMFeeItemList(this.myNBpatient, dtDate, obj, "00") > 0)
                    {

                    }
                    else
                    {
                        myConn.RollBack();
                        this.errMsg="数据传送失败！"+patient.PID.PatientNO;
                        return -1;
                    }

                }

                //写控制表
                if (myConn.SeDataChangeInfoDraw(myNBpatient, "15", "00", dsItem.Tables[0].Rows.Count, sumAccount) > 0)
                {

                    myConn.Commit();
                }
                else
                {
                    myConn.RollBack();
                    this.errMsg = "写控制表失败！" + patient.PID.PatientNO;
                    return -1;
                }
                this.errMsg = "已上传成功！"+patient.PID.PatientNO;


            }
            return 1;
        }

        #endregion

        /// <summary>
        /// 本地预结算
        /// </summary>
        /// <returns>1 成功 －1 失败</returns>
        public int LocalBalanceStart()
        {
            try
            {
                //服务器时间
                dtServerDateTime = this.GetDateTimeFromSysDateTime();

                if (dtServerDateTime == DateTime.MinValue)
                {
                    return -1;
                }

                //获取所有未出院结算的患者（新农合）
                ArrayList alPatientInfo = mySILocalManager.QueryXnhPatientInfo("611");
                if (alPatientInfo == null||alPatientInfo.Count <=0)
                {
                    this.Err = "没找到新农合患者信息!";
                    WriteErr();
                    return -1;//如果找到患者信息
                }

                //循环患者，进行本地结算
                Neusoft.HISFC.Models.RADT.PatientInfo  patient = null;
                for (int i = 0; i < alPatientInfo.Count; i++)
                {
                    patient = (Neusoft.HISFC.Models.RADT.PatientInfo)alPatientInfo[i];

                    //验证数据合法性
                    if (string.IsNullOrEmpty(patient.ID) || string.IsNullOrEmpty(patient.Pact.ID ))
                    {
                        this.Err = patient.ID+"住院流水号或合同单位代码为空!";
                        WriteErr();
                        continue;
                    }

                    //本地预结算该患者
                    int iResult = this.LocalComputeInpatient(patient, false);
                    if (iResult == -1)
                    {
                        this.Err = patient.ID + " " + patient.Name + " 本地预结算该患者出错!";
                        WriteErr();
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "本地结算错误!" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 写错误日志


        /// </summary>
        public override void WriteErr()
        {
            this.myMessage = this.Err;
            base.WriteErr();
        }

        #endregion


        #region IJob 成员
        private string myMessage = ""; //传递消息


        public string Message
        {
            // TODO:  添加 calculateFee.Con setter 实现
            get
            {
                return this.myMessage;
            }
        }
        public System.Data.OracleClient.OracleConnection Con
        {
            set
            {
                // TODO:  添加 calculateFee.Con setter 实现
            }
        }

        public int Start()
        {
            // TODO:  添加 calculateFee.Start 实现
            return this.LocalBalanceStart();
        }

        #endregion

    }
}
