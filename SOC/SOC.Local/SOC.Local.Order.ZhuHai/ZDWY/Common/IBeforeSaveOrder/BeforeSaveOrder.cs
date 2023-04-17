using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IBeforeSaveOrder
{
    class BeforeSaveOrder : FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder
    {
        #region IBeforeSaveOrder 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        private FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();


        /// <summary>
        /// 是否进行实时审核
        /// </summary>
        private bool isCheck = true;
        /// <summary>
        /// 控制参数业务层 --com_controlargument
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 常数列表管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 是否提醒实时审核
        /// </summary>
        private bool isTip = true;

        /// <summary>
        /// 所有需要实时审核的合同单位
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpSSHHPact = null;

        /// <summary>
        /// 住院
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int BeforeSaveOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            // {8C0695D9-AFA2-479a-B1D7-73337848788B}
            this.errInfo = string.Empty;
            isCheck = controlParam.GetControlParam<bool>("SSHH01");
            isTip = controlParam.GetControlParam<bool>("SSHH02");
            if (helpSSHHPact == null || helpSSHHPact.ArrayObject.Count <= 0)// {EFD97867-FCCC-4373-9055-B0FD7D01A0D7}
            {
                ArrayList al = conMgr.GetList("SHPACT");
                if (al != null && al.Count > 0)
                {
                    helpSSHHPact = new FS.FrameWork.Public.ObjectHelper(al);
                }
            }
            if (helpSSHHPact != null && helpSSHHPact.ArrayObject.Count > 0)
            {
                if (isCheck)
                {
                    FS.FrameWork.Models.NeuObject shItemObj = helpSSHHPact.GetObjectFromID(patientInfo.Pact.ID);

                    if (shItemObj != null && !string.IsNullOrEmpty(shItemObj.ID))
                    {
                        if (isTip)
                        {
                            if (MessageBox.Show("是否进行医保实时审核?", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int returnValue = FoShanOrderAudit.SiOrderAudit.AuditOrder(patientInfo, reciptDoct, reciptDept, alOrder, false, ref errInfo);
                                if (returnValue <= 0)
                                {
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            int returnValue = FoShanOrderAudit.SiOrderAudit.AuditOrder(patientInfo, reciptDoct, reciptDept, alOrder, false, ref errInfo);
                            if (returnValue <= 0)
                            {
                                return -1;
                            }
                        }
                    }


                }
            }

            #region 出院带药天数限制

            //自费限制14天
            //医保限制7天

            //2014-10-13 12:09:18 韩科找陈熙文确认，腹透中心自费患者不限制，医保患者限制30以内

            int limitDay = 14;

            bool isSIPatient = false;
            if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).PayKind.ID == "02")
            {
                isSIPatient = true;
            }

            string strZGInfo = "";
            bool isFT = false;
            if (patientInfo.PVisit.PatientLocation.Dept.Name.Contains("腹膜透析"))
            {
                isFT = true;
            }
            if (isFT)
            {
                limitDay = 999999;
                if (isSIPatient)
                {
                    limitDay = 30;
                }
            }
            else
            {
                if (isSIPatient)
                {
                    //医保出院带药限制：
                    //1、只能带第一诊断的治疗用药（不超过5种）；  这个人为判断吧....
                    //2、时间不能超过1个星期（治愈患者不能超过3天）
                    //3、不能带注射针剂。 

                    limitDay = 7;

                    string sql = @"select (select y.name
                                  from com_dictionary y
                                 where y.type = 'ZG'
                                   and y.code = i.zg) 转归
                          from fin_ipr_inmaininfo i
                         where i.inpatient_no = '{0}'";

                    sql = string.Format(sql, patientInfo.ID);

                    string strZG = dbMgr.ExecSqlReturnOne(sql);
                    if (strZG.Contains("好转"))
                    {
                        limitDay = 3;
                        strZGInfo = "治愈出院\r\n";
                    }
                }
            }

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (isSIPatient && !isFT)
                    {
                        if (SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID)
                            && order.OrderType.ID == "CD" //出院带药
                                )
                        {
                            MessageBox.Show("医保患者出院带药不允许带注射针剂！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return -1;
                        }
                    }

                    if (order.Item.SysClass.ID.ToString() != "PCC"
                        && order.OrderType.ID == "CD" //出院带药
                        && order.HerbalQty > limitDay)
                    {
                        errInfo += order.Item.Name + "\r\n";
                    }

                    #region 控制临嘱毒麻药品只能开立1天

                    if (GetItemQaulity(order) > 1
                        && order.HerbalQty > 1)
                    {
                        MessageBox.Show("毒麻药品【" + order.Item.Name + "】开立超过1天，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //return -1;
                    }

                    #endregion
                }
            }
            if (!string.IsNullOrEmpty(errInfo))
            {
                errInfo = "患者【" + patientInfo.Name + "】 合同单位【" + FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).Name + "】\r\n" + strZGInfo + errInfo + "出院带药天数不允许超过" + limitDay.ToString() + "天！\r\n\r\n如有疑问请联系医务科！";
                return -1;
            }
            #endregion

            #region 重复项目提示

            /* 1、长嘱存在护理级别再新开时，给出提示
             * 2、长嘱临嘱存在相同的有效医嘱时给出提示
             * */

            string UN_sql = @"select wm_concat( f.item_name)
                                from met_ipm_order f
                                where f.inpatient_no='{0}'
                                and f.decmps_state='1'
                                and f.class_code='UN'
                                and (f.mo_order!='{1}' or '{1}'='ALL')
                                and f.mo_stat not in('3','4','7')";

            string Item_sql = @"select 1
                                from met_ipm_order f
                                where f.inpatient_no='{0}'
                                and f.item_code='{1}'
                                and (f.mo_order!='{2}' or '{2}'='ALL')
                                and f.mo_stat not in('3','4','7')";

            bool isHaveUNOrder = false;

            string unOrder = "";

            string repeatItem = "";

            string moOrder = "";

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
            {
                moOrder = string.IsNullOrEmpty(order.ID) ? "ALL" : order.ID;
                //护理级别
                if (order.Item.SysClass.ID.ToString() == "UN")
                {
                    isHaveUNOrder = true;
                    unOrder = moOrder;
                }

                //按照项目提示的先屏蔽了 会影响效率
                //if (deptMgr.ExecSqlReturnOne(string.Format(Item_sql, patientInfo.ID, order.Item.ID, moOrder)) == "1")
                //{
                //    repeatItem += order.Item.Name + "\r\n";
                //}
            }

            if (isHaveUNOrder)
            {
                if (string.IsNullOrEmpty(unOrder))
                {
                    unOrder = "ALL";
                }

                string unItem = dbMgr.ExecSqlReturnOne(string.Format(UN_sql, patientInfo.ID, unOrder));
                if (!string.IsNullOrEmpty(unItem) && unItem != "-1")
                {
                    MessageBox.Show("已存在有效的护理级别医嘱：" + unItem, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //if (!string.IsNullOrEmpty(repeatItem))
            //{
            //    Classes.Function.ShowBalloonTip(3, "提示", "以下项目可能之前已经开立：\r\n\r\n" + repeatItem, ToolTipIcon.Info);
            //}

            #endregion

            #region 弹出知情同意书

            //先不使用，待上线后再说
            if (false)
            {
                // 根据患者住院流水号、病历输入模板ID判断患者是否存在该病历
                //方法名：GetIsWritenRecordByInputTemplateId(string HisInpatientNo, long inputTemplateID)
                //参数说明：住院流水号、病历输入模板ID

                //    根据患者、输入模板ID弹出知情同意书书写窗体
                //方法名：ShowInformedConsentRecordWriteFor(string HisInpatientNo, long inputTemplateID)
                // 参数说明：住院流水号、病历输入模板ID

                bool IsUseEmr = false;
                try
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    System.Xml.XmlNode node = null;
                    doc.Load(Application.StartupPath + "\\Setting\\EmrSetting.xml");
                    node = doc.SelectSingleNode("/设置/是否新版电子病历");

                    if (node == null)
                    {
                        IsUseEmr = false;
                    }
                    else
                    {
                        if (node.InnerXml.ToLower() == "true")
                        {
                            IsUseEmr = true;
                        }
                        else
                        {
                            IsUseEmr = false;
                        }
                    }
                }
                catch
                {
                    IsUseEmr = false;
                }

                if (IsUseEmr)
                {
                    //if (!FS.Emr.AppInterface.RecordInterface.AppRecordFacadeProxy.GetIsWritenRecordByInputTemplateId(patientInfo.ID, 33040))
                    //{
                    //    FS.Emr.AppInterface.RecordInterface.AppRecordFacadeProxy.ShowInformedConsentRecordWriteFor(patientInfo.ID, 33040);
                    //}
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 药品性质转换类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsDrugQuaulity = null;

        /// <summary>
        /// 药理作用帮助类（二级）
        /// </summary>
        private static Hashtable hsPhyFunction = null;

        /// <summary>
        /// 获取药品性质的分方类别
        /// 3、精二；2、毒麻精一；1、普通；0、非药品；-1 大输液
        /// </summary>
        /// <param name="feeItem"></param>
        /// <returns></returns>
        public static int GetItemQaulity(FS.HISFC.Models.Order.Order order)
        {
            //3、精二；2、毒麻精一；1、普通；0、非药品

            int quaulityType = 0;
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                if (hsDrugQuaulity == null)
                {
                    hsDrugQuaulity = new FS.FrameWork.Public.ObjectHelper();

                    FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

                    //取药品剂型
                    ArrayList alDrugQuaulity = managerIntegrate.GetConstantList("DRUGQUALITY");
                    if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                    {
                        hsDrugQuaulity.ArrayObject = alDrugQuaulity;
                    }
                }

                FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((order.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;

                if (quaulity != null && quaulity.ID.Length > 0)
                {
                    if (quaulity.Memo.Contains("精二")

                        || quaulity.UserCode.Contains("P2")//精二
                        )
                    {
                        quaulityType = 3;
                    }
                    else if (quaulity.Memo.Contains("毒")
                        || quaulity.Memo.Contains("麻")
                        || quaulity.Memo.Contains("精一")

                        || quaulity.UserCode.Contains("P1")//精一
                        || quaulity.UserCode.Contains("P")//精神类
                        || quaulity.UserCode.Contains("S")//毒药
                        )
                    {
                        quaulityType = 2;
                    }
                    else if (quaulity.Memo.Contains("大输液")
                        || quaulity.UserCode.Contains("T"))
                    {
                        quaulityType = -1;
                    }
                    else
                    {
                        quaulityType = 1;
                    }
                }
            }

            return quaulityType;
        }

        /// <summary>
        /// 限制项目
        /// </summary>
        ArrayList alLimitItem = null;

        FS.FrameWork.Public.ObjectHelper limitItemHelper = null;

        /// <summary>
        /// 允许超越30天用量限制的药品
        /// </summary>
        Dictionary<string, object> dicSpePHA = null;

        /// <summary>
        /// 门诊
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int BeforeSaveOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            // {8C0695D9-AFA2-479a-B1D7-73337848788B}
            this.errInfo = string.Empty;
            isCheck = controlParam.GetControlParam<bool>("SSHH01");
            isTip = controlParam.GetControlParam<bool>("SSHH02");

            if (helpSSHHPact == null || helpSSHHPact.ArrayObject.Count <= 0)// {EFD97867-FCCC-4373-9055-B0FD7D01A0D7}
            {
                ArrayList al = conMgr.GetList("SHPACT");
                if (al != null && al.Count > 0)
                {
                    helpSSHHPact = new FS.FrameWork.Public.ObjectHelper(al);
                }
            }
            if (isCheck)
            {
                FS.FrameWork.Models.NeuObject shItemObj = helpSSHHPact.GetObjectFromID(regObj.Pact.ID);

                if (shItemObj != null && !string.IsNullOrEmpty(shItemObj.ID))
                {
                    if (isTip)
                    {
                        //if (regObj.Pact.PayKind.ID == "02")
                        {
                            if (MessageBox.Show("是否进行医保实时审核?", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int returnValue = FoShanOrderAudit.SiOrderAudit.AuditOrder(regObj, reciptDoct, reciptDept, alOrder, true, ref errInfo);
                                if (returnValue <= 0)
                                {
                                    return returnValue;
                                }
                            }
                        }
                    }
                    else
                    {
                        //if (regObj.Pact.PayKind.ID == "02")
                        {
                            int returnValue = FoShanOrderAudit.SiOrderAudit.AuditOrder(regObj, reciptDoct, reciptDept, alOrder, true, ref errInfo);
                            if (returnValue <= 0)
                            {
                                return returnValue;
                            }
                        }
                    }
                }

            }


            //{43EBC808-7B71-4128-A685-4F31236DA6D4}
            return 1;


            #region 草药组合限制
            //1、必须组合
            //2、不允许项目重复

            //Hashtable itemList = new Hashtable();
            //string combo = string.Empty;
            //foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            //{
            //    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            //    {
            //        if (order.Item.SysClass.ID.ToString() == "PCC")
            //        {
            //            if (string.IsNullOrEmpty(combo))
            //            {
            //                combo = order.Combo.ID;
            //            }
            //            if (combo != order.Combo.ID)
            //            {
            //                errInfo = "草药必须组合！";
            //                return -1;
            //            }

            //            if (!itemList.ContainsKey(order.Item.ID))
            //            {
            //                itemList.Add(order.Item.ID, order.Item.ID);
            //            }
            //            else
            //            {
            //                errInfo = "[" + order.Item.Name + "]项目重复！";
            //                return -1;
            //            }
            //        }
            //    }
            //}

            #endregion

            string seeNo = "";
            if (reciptDept.Name.Contains("简易"))
            {
                #region 简易门诊限制开立不能超过7天用药。一次挂号开立不能超过5种药品（不含大输液）

                //简易门诊限制开立不能超过7天用药
                string stopInfo = "";

                int phaCount = 0;

                 //string itemStr = "''";

                seeNo = "";

                foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
                {
                    //itemStr += ",'" + order.Item.ID + "'";
                    seeNo = order.SeeNO;

                    if (GetItemQaulity(order) > 0)
                    {
                        phaCount += 1;
                    }

                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (order.Item.SysClass.ID.ToString() != "PCC")
                        {
                            if (order.HerbalQty > 7)
                            {
                                stopInfo += order.Item.Name + "\r\n";
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(stopInfo))
                {
                    errInfo = stopInfo + "用药天数不允许超过7天！\r\n\r\n如有疑问请联系医务科！";
                    return -1;
                }

                //一次挂号开立不能超过5种药品（不含大输液）
                if (phaCount > 5)
                {
                    errInfo = "一次挂号开立不能超过5种药品（不含大输液）！\r\n\r\n如有疑问请联系医务科！";
                    return -1;
                }
                else
                {
                    string sql = @"select count(*)
                                    from fin_opb_feedetail  f
                                    where f.clinic_code='{0}'
                                    and f.cancel_flag='1'
                                    and f.drug_flag='1'
                                    and f.class_code!='PCC'
                                    and not exists (select 1 from met_ord_recipedetail l
                                    where l.clinic_code=f.clinic_code
                                    and l.card_no=f.card_no
                                    and l.sequence_no=f.mo_order
                                    and l.see_no='{1}')";

                    sql = string.Format(sql, regObj.ID, seeNo);

                    string count = dbMgr.ExecSqlReturnOne(sql, "0");

                    int chargeCount = 0;

                    try
                    {
                        chargeCount = FS.FrameWork.Function.NConvert.ToInt32(count);
                        if (chargeCount == -1)
                        {
                            chargeCount = 0;
                        }
                    }
                    catch
                    {
                        chargeCount = 0;
                    }

                    if (chargeCount + phaCount > 5)
                    {
                        errInfo = "患者已经开立的药品种类数为" + chargeCount.ToString() + "种,本次只能开立不多于" + (5 - chargeCount).ToString() + "种的药品。\r\n实际开立药品种类为" + phaCount.ToString() + "种，请调整！\r\n（以上均不含大输液）\r\n\r\n如有疑问请联系医务科！";
                        return -1;
                    }
                }

                #endregion
            }
            else
            {
                #region 开立天数限制

                if (!regObj.PID.CardNO.StartsWith("9"))
                {

                    //急诊号 限药3天
                    if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(regObj.DoctorInfo.Templet.RegLevel.ID).IsEmergency)
                    {
                        foreach (FS.HISFC.Models.Order.Order order in alOrder)
                        {
                            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (order.Item.SysClass.ID.ToString() != "PCC"
                                    && order.HerbalQty > 3)
                                {
                                    errInfo += order.Item.Name + "\r\n";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(errInfo))
                        {
                            errInfo += "用药天数不允许超过3天！\r\n\r\n";

                            if (MessageBox.Show("患者【" + regObj.Name + "】是急诊患者，是否继续保存？\r\n\r\n" + errInfo + "\r\n\r\n如有疑问请联系医务科！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                errInfo = string.Empty;
                                return -1;
                            }

                            //errInfo += "用药天数不允许超过3天！\r\n\r\n如有疑问请联系医务科！";
                            //return -1;
                        }
                    }
                    else
                    {
                        //普通门诊限药7天，慢病限药30天
                        string stopInfo = "";
                        string warnInfo = "";
                        foreach (FS.HISFC.Models.Order.Order order in alOrder)
                        {
                            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (order.Item.SysClass.ID.ToString() != "PCC")
                                {
                                    if (order.HerbalQty > 30)
                                    {
                                        stopInfo += order.Item.Name + "\r\n";
                                    }
                                    else if (order.HerbalQty > 7)
                                    {
                                        warnInfo += order.Item.Name + "\r\n";
                                        //if (MessageBox.Show("患者【" + regObj.Name + "】是否是慢病患者？\r\n\r\n[" + order.Item.Name + "]用药天数超过7天,非慢病患者限制开立7天内药量！\r\n\r\n如有疑问请联系医务科！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                        //{
                                        //    errInfo = "[" + order.Item.Name + "]用药天数不允许超过7天！\r\n\r\n如有疑问请联系医务科！";
                                        //    return -1;
                                        //}
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(stopInfo))
                        {
                            errInfo = stopInfo + "用药天数不允许超过30天！\r\n\r\n";
                            return -1;
                        }

                        if (!string.IsNullOrEmpty(warnInfo))
                        {
                            if (MessageBox.Show("患者【" + regObj.Name + "】是否是慢病患者？\r\n\r\n" + warnInfo + "用药天数超过7天,非慢病患者限制开立7天内药量！\r\n\r\n如有疑问请联系医务科！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return -1;
                            }
                        }
                    }
                }
                #endregion
            }

            #region 保存时，当天重复项目提示

            #region 原有的当天重复项目的提示就取消了吧


//            DateTime dtNow = dbMgr.GetDateTimeFromSysDateTime();
//            DateTime dtBegin = dtNow.Date;
//            if (regObj.DoctorInfo.Templet.RegLevel.IsEmergency)
//            {
//                dtBegin = dtNow.AddHours(-24);
//            }

//            string query_SQL = @"select wm_concat(distinct f.item_name||decode(nvl(f.specs,'哈哈'),'哈哈','','['||f.specs||']'))
//                                from fin_opb_feedetail  f
//                                where f.card_no='{0}'
//                                and f.reg_date>to_date('{1}','yyyy-mm-dd hh24:mi:ss')
//                                and f.item_code in ({2})
//                                and f.cancel_flag='1'
//                                and not exists (select 1 from met_ord_recipedetail l
//                                where l.clinic_code=f.clinic_code
//                                and l.card_no=f.card_no
//                                and l.sequence_no=f.mo_order
//                                and l.see_no='{3}')";

//            string itemStr = "''";
//            seeNo = "";

//            Hashtable hsItem = new Hashtable();
//            string repeatItemName = string.Empty;
//            foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
//            {
//                itemStr += ",'" + outOrder.Item.ID + "'";
//                seeNo = outOrder.SeeNO;

//                if (outOrder.ID != "999"
//                    && GetItemQaulity(outOrder) > 0)
//                {
//                    if (!hsItem.Contains(outOrder.Item.ID))
//                    {
//                        hsItem.Add(outOrder.Item.ID, null);
//                    }
//                    else
//                    {
//                        repeatItemName += (string.IsNullOrEmpty(repeatItemName) ? "" : ",") + outOrder.Item.Name;
//                    }
//                }
//            }

//            query_SQL = string.Format(query_SQL, regObj.PID.CardNO, dtBegin.ToString(), itemStr, seeNo);

//            string rev = dbMgr.ExecSqlReturnOne(query_SQL, "-1");
//            if (rev != "-1"
//                && !string.IsNullOrEmpty(rev)
//                && !repeatItemName.Contains(rev))
//            {
//                repeatItemName += (string.IsNullOrEmpty(repeatItemName) ? "" : ",") + rev;
//            }

//            if (!string.IsNullOrEmpty(repeatItemName))
//            {
//                if (MessageBox.Show("以下项目今天已重复开立，是否继续保存？\r\n\r\n" + repeatItemName, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
//                {
//                    errInfo = string.Empty;
//                    return -1;
//                }
//            }

            #endregion

            Hashtable hsItem = new Hashtable();
            string repeatItemName = string.Empty;
            string strPhyFunction = string.Empty;
            foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
            {
                if (outOrder.ID != "999"
                    && outOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID);

                    if (hsPhyFunction == null||hsPhyFunction.Count == 0)
                    {
                        hsPhyFunction = new Hashtable();

                        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

                        //取药品剂型
                        ArrayList alPhyFunction = managerIntegrate.GetConstantList("PhyFunction2");
                        foreach (FS.HISFC.Models.Base.Const con in alPhyFunction)
                        {
                            if (con.IsValid
                                &&!hsPhyFunction.Contains(con.ID))
                            {
                                hsPhyFunction.Add(con.ID, con);
                            }
                        }
                    }

                    if (!hsPhyFunction.ContainsKey(phaItem.PhyFunction2.ID))
                    {
                        if (!hsItem.Contains(outOrder.Item.ID))
                        {
                            hsItem.Add(outOrder.Item.ID, null);
                        }
                        else
                        {
                            repeatItemName += "\r\n第" + outOrder.SubCombNO.ToString() + "组  " + outOrder.Item.Name;
                        }
                    }
                }
            }

            if (hsPhyFunction != null && hsPhyFunction.Count > 0)
            {
                foreach (DictionaryEntry de in hsPhyFunction)
                {
                    FS.HISFC.Models.Base.Const con = de.Value as FS.HISFC.Models.Base.Const;
                    strPhyFunction += con.Name + "、";
                }
            }

            if (!string.IsNullOrEmpty(repeatItemName))
            {
                MessageBox.Show("除药理作用为[" + strPhyFunction + "]的药品外，其他药品不允许重复开立！\r\n如有疑问请联系医务科、药学部！\r\n\r\n重复药品：" + repeatItemName, "禁止", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                errInfo = string.Empty;
                return -1;
            }

            #endregion

            #region 处方30天用量限制

            string strErrItem = string.Empty;

            if (dicSpePHA == null)
            {
                dicSpePHA = new Dictionary<string, object>();

                FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList alSpePHA = conMgr.GetAllList("NoLimitPHAForOrder");
                foreach (FS.HISFC.Models.Base.Const con in alSpePHA)
                {
                    if (con.IsValid)
                    {
                        if (!dicSpePHA.ContainsKey(con.ID))
                        {
                            dicSpePHA.Add(con.ID, null);
                        }
                    }
                }
            }

            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug
                    && order.Item.ID != "999")
                {
                    FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                    //2014-10-20 张美蓉提出，对于按照包装单位取整的，而且数量是1的，不判断30天限量
                    if (phaItem.PackUnit == order.Unit
                        && order.Qty == 1

                        && "1".Contains(phaItem.SplitType))
                    {
                    }
                    else
                    {
                        //廉价药外用药不做这个限制
                        if (phaItem.SpecialFlag1 != "1"&&phaItem.SpecialFlag2 != "1"
                            //0最小单位总量取整、1包装单位总量取整
                            && "1".Contains(phaItem.SplitType))
                        {
                            //频次*每次量*天数=总量
                            //天数=总量/(频次*每次量)
                            if (!dicSpePHA.ContainsKey(phaItem.UserCode))
                            {
                                decimal freqCount = 1;
                                freqCount = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(order.Frequency.ID);

                                int useDay = 30;
                                if (order.Unit == phaItem.PackUnit)
                                {
                                    useDay = (Int32)Math.Ceiling(order.Qty * phaItem.PackQty * phaItem.BaseDose / (order.DoseOnce * freqCount));
                                }
                                else
                                {
                                    useDay = (Int32)Math.Ceiling(order.Qty * phaItem.BaseDose / (order.DoseOnce * freqCount));
                                }

                                if (useDay > 30)
                                {
                                    if (string.IsNullOrEmpty(strErrItem))
                                    {
                                        strErrItem = "以下项目开立总量超过30天用量，请修改！\r\n";
                                    }

                                    strErrItem += "\r\n第" + order.SubCombNO.ToString() + "组 " + phaItem.Name + " 按照总量" + order.Qty.ToString() + order.Unit + "计算，允许开立的最大天数为" + GetLimitUseDay(order, phaItem, freqCount).ToString() + "天";
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(strErrItem))
            {
                strErrItem += "\r\n\r\n如有疑问请联系医务科、药学部！";

                errInfo = strErrItem;
                return -1; 
            }

            #endregion

            return 1;
        }

        private int GetLimitUseDay(FS.HISFC.Models.Order.OutPatient.Order order, FS.HISFC.Models.Pharmacy.Item phaItem, decimal freqCount)
        {
            int useDay = 30;

            for (int qty = (Int32)Math.Ceiling(order.Qty); qty > 0; qty--)
            {
                useDay = (Int32)Math.Floor(qty * phaItem.BaseDose * phaItem.PackQty / (order.DoseOnce * freqCount));
                if (useDay <= 30)
                {
                    return useDay;
                }
            }
            return 30;
        }

        #endregion
    }
}
