using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace Neusoft.HISFC.Components.RADT.Classes
{
    /// <summary>
    /// [功能描述: 护士站公用函数]<br></br>
    /// [创 建 者: huangchw]<br></br>
    /// [创建时间: 2012-11-19]<br></br>
    /// </summary>
    class Function : Neusoft.FrameWork.Management.Database
    {
        #region 变量
        private static Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        private static Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        private static Neusoft.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();
        private static Neusoft.HISFC.BizProcess.Integrate.Order orderIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Order();
        private static Neusoft.HISFC.BizLogic.RADT.InPatient inpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        #endregion

        #region 出院和转科时对患者的各种判断复用

        #region 退费申请
        /// <summary>
        /// 退费申请判断
        /// </summary>
        public static string CheckReturnApply(string patientID)
        {
            ArrayList applys = feeIntegrate.QueryReturnApplys(patientID, false);
            if (applys == null)
            {
                return null ;
            }

            string itemInfo = null;
            if (applys.Count > 0) //存在退费申请
            {
                itemInfo = "";
                Hashtable hsCombItem = new Hashtable();
                foreach (Neusoft.HISFC.Models.Fee.ReturnApply returnApply in applys)
                {
                    if (!string.IsNullOrEmpty(returnApply.UndrugComb.ID))
                    {
                        if (!hsCombItem.ContainsKey(returnApply.Order.ID + returnApply.UndrugComb.ID))
                        {
                            itemInfo += returnApply.UndrugComb.Name + "--(" + manager.GetDepartment(returnApply.ExecOper.Dept.ID).Name + ")" + "\r\n";
                            hsCombItem.Add(returnApply.Order.ID + returnApply.UndrugComb.ID, null);
                        }
                    }
                    else
                    {
                        itemInfo += returnApply.Item.Name + "--(" + manager.GetDepartment(returnApply.ExecOper.Dept.ID).Name + ")" + "\r\n";
                    }
                }
            }
            return itemInfo;
        }
        #endregion

        #region 摆药查询
        /// <summary>
        /// 摆药查询
        /// </summary>
        public static string CheckDrugApplay(string patientID)
        {
            DataSet dsTemp = null;
            string msg = null;

            //查询所有摆药、退药申请
            int returnValue = inpatient.ExecQuery("Pharmacy.Item.QueryNoConfirmApplyOut.Select.1", ref dsTemp, patientID);
            if (dsTemp != null && dsTemp.Tables.Count > 0)
            {
                DataTable dtTemp = dsTemp.Tables[0];

                //摆药申请
                string drugApplay = "";
                //退药申请
                string quitDrugApplay = "";

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    // 存在未摆药药品
                    string strTemp = string.Empty;
                    Hashtable hsDrugDept = new Hashtable();
                    foreach (DataRow drRow in dtTemp.Rows)
                    {
                        strTemp = drRow["drug_dept_code"].ToString();
                        if (!hsDrugDept.ContainsKey(strTemp))
                        {
                            hsDrugDept.Add(strTemp, manager.GetDepartment(strTemp));
                        }
                    }

                    string strDrugCode = string.Empty;
                    string strDrugName = string.Empty;
                    Hashtable hsDrug = new Hashtable();
                    foreach (string strKey in hsDrugDept.Keys)  //循环，可能重置 msg
                    {
                        hsDrug.Clear();
                        msg = hsDrugDept[strKey].ToString() + "：\r\n";
                        foreach (DataRow drRow in dtTemp.Rows)
                        {
                            strTemp = drRow["drug_dept_code"].ToString();
                            strDrugCode = drRow["drug_code"].ToString();
                            strDrugName = drRow["trade_name"].ToString();
                            if (strTemp == strKey && !hsDrug.ContainsKey(strDrugCode))
                            {
                                msg = msg + "    " + strDrugName + "\r\n";
                                hsDrug.Add(strDrugCode, strDrugName);
                            }
                        }
                    }
                }
            }
            return msg;
        }
        #endregion

        #region 未确认项目判断
        /// <summary>
        /// 未确认项目判断
        /// </summary>
        public static string CheckUnConfirm(string patientID)
        {
            string itemInfo = null;
            ArrayList alExecOrder = orderIntegrate.QueryExecOrderByDept(patientID, "2", false, "all");
            
            if (alExecOrder != null && alExecOrder.Count > 0)
            {
                itemInfo = "项目：\n";
                foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
                {
                    itemInfo += execOrder.Order.Item.Name + "--(" + execOrder.Order.ExeDept.Name + ")" + "\n";
                }
            }
            return itemInfo;
        }
        #endregion

        #region 是否有未收费的非药品医嘱执行档判断
        /// <summary>
        /// 是否有未收费的非药品医嘱执行档判断
        /// </summary>
        public static string CheckExecOrderCharge(string patientID)
        {
            string returnStr = null;
            ArrayList alUnFeeedItem = orderIntegrate.QueryExecOrderIsCharg(patientID, "2", false);
            
            if (alUnFeeedItem != null && alUnFeeedItem.Count > 0)
            {
                string unfee = "";

                int countWarning = 0;
                foreach (Neusoft.HISFC.Models.Order.ExecOrder exec in alUnFeeedItem)
                {
                    if (exec.Order.OrderType.isCharge && unfee.IndexOf(exec.Order.Item.Name) < 0)
                    {
                        unfee += exec.Order.Item.Name + "\r\n";
                        countWarning += 1;
                    }
                }
                returnStr = unfee + "|||" + countWarning.ToString();
            }
            return returnStr;
        }
        #endregion


        #region 长嘱是否全停

        /// <summary>
        /// 长嘱是否全停
        /// </summary>
        /// <param name="inPateintNo"></param>
        /// <returns></returns>
        public bool CheckIsAllLongOrderStop(string inPateintNo)
        {
            string sql = @"select count(*) from met_ipm_order t
                            where t.inpatient_no='{0}'
                            and t.decmps_state='1'
                            and (t.mo_stat not in ('3','4','7')--停止、重整、预停止
                            or (t.mo_stat in ('3','7')  and t.dc_confirm_flag='0'))
                            AND  SUBTBL_FLAG = '0'";

            try
            {
                sql = string.Format(sql, inPateintNo);

                int rev = Neusoft.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
                if (rev > 0)
                {
                    this.Err = "长期医嘱没有全部停止,或护士没有审核已停止长嘱！";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }
        }

        #endregion

        #region 是否开立出院医嘱

        /// <summary>
        /// 是否已开立出院医嘱（项目包含“出院”!）
        /// </summary>
        /// <param name="inPateintNo"></param>
        /// <returns></returns>
        [Obsolete("作废，已经有方法了", true)]
        public bool CheckIsHaveOutOrder(string inPateintNo)
        {
            string sql = @"select count(*) from met_ipm_order
                            where inpatient_no='{0}'
                            and (class_code = 'MRH'
                            or item_name like '%出院%')
                            and decmps_state='0'";

            try
            {
                sql = string.Format(sql, inPateintNo);

                int rev = Neusoft.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
                if (rev <= 0)
                {
                    this.Err = "还没有开立出院医嘱，不允许出院！";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }
        }

        #endregion

        #region 是否有未审核医嘱

        /// <summary>
        /// 查询是否有未审核医嘱
        /// </summary>
        /// <param name="inPateintNo"></param>
        /// <returns></returns>
        public bool CheckIsAllOrderConfirm(string inPateintNo)
        {
            string sql = @"select count(*) from met_ipm_order t
                        where t.inpatient_no='{0}'
                        AND (t.mo_stat in('0','5','6') --新开、需审核、暂存
                        or (t.mo_stat in ('3','7') --停止、重整、预停止  重整医嘱不做审核处理
                        and t.dc_confirm_flag='0'))
                        AND t.SUBTBL_FLAG='0'";
            try
            {
                sql = string.Format(sql, inPateintNo);

                int rev = Neusoft.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

                if (rev > 0)
                {
                    this.Err = "存在未审核医嘱！";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取未审核医嘱的开立科室列表
        /// </summary>
        /// <param name="inPateintNo"></param>
        /// <returns></returns>
        public string GetUnConfirmOrders(string inPateintNo)
        {
            string sql = @"select distinct (select f.dept_name from com_department f where f.dept_code=t.list_dpcd)
                        from met_ipm_order t
                        where t.inpatient_no='{0}'
                        AND (t.mo_stat in('0','5','6') --新开、需审核、暂存
                        or (t.mo_stat in ('3','7') --停止、重整、预停止  重整医嘱不做审核处理
                        and t.dc_confirm_flag='0'))
                        AND t.SUBTBL_FLAG='0'";
            try
            {
                sql = string.Format(sql, inPateintNo);

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                string recipeDept = "";
                while (this.Reader.Read())
                {
                    if (string.IsNullOrEmpty(recipeDept))
                    {
                        recipeDept = this.Reader[0].ToString();
                    }
                    else
                    {
                        recipeDept += "、" + this.Reader[0].ToString();
                    }
                }
                return recipeDept;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        #endregion

        #endregion
    }
}