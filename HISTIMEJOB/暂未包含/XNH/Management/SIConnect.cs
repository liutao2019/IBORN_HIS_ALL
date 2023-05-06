using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Collections;


namespace HISTIMEJOB.XNH.Management
{
    /// <summary>
    /// 番禺新农合医保接口业务
    /// </summary>
    public class SIConnect : SIDatabase
    {

        #region 获取农保参合人信息

        #region 获取三大目录
        /// <summary>
        /// 获取三大目录
        /// </summary>
        /// <param name=null>无</param>	
        public ArrayList GetItemRade()
        {
            string sql = "select * from item_rade where item_code is not null order by date_sn ";
            Neusoft.FrameWork.Models.NeuObject obj = null;
            ArrayList al = new ArrayList();

            if (this.ExecQuery(sql) == -1)
                return null;

            try
            {
                while (Reader.Read())
                {
                    obj = new Neusoft.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader["item_code"].ToString() + "";
                    obj.Name = this.Reader["item_name"].ToString() + "";
                    obj.User01 = this.Reader["item_flag"].ToString() + "";
                    obj.User02 = this.Reader["item_rade"].ToString() + "";
                    obj.User03 = this.Reader["unit_type"].ToString() + "";
                    al.Add(obj);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return al;
            }

            return al;
        }
        #endregion

        #region  根据农保卡号获取患者信息
        /// <summary>
        /// 根据农保卡号获取患者信息
        /// </summary>
        /// <param name="cardNo">农保卡号</param>	
        public Models.PYNBMainInfo GetRegPersonInfo(string cardNo)
        {
            string sql = "select * from policy where card_no = '" + cardNo + "'and appl_year = '" + System.DateTime.Now.Year.ToString() + "'";

            if (this.ExecQuery(sql) == -1)
                return null;
            Models.PYNBMainInfo PYNBMainInfo = new Models.PYNBMainInfo();

            try
            {
                while (Reader.Read())
                {
                    PYNBMainInfo.Acct_name = Reader["acct_name"].ToString() + "";
                    PYNBMainInfo.Appl_year = Neusoft.FrameWork.Function.NConvert.ToInt32(Reader["Appl_year"].ToString());
                    PYNBMainInfo.Bank_acct = Reader["Bank_acct"].ToString() + "";
                    PYNBMainInfo.Bank_fg = Reader["Bank_fg"].ToString() + "";
                    PYNBMainInfo.Bank_name = Reader["Bank_name"].ToString() + "";
                    PYNBMainInfo.Bank_type = Reader["Bank_type"].ToString() + "";
                    PYNBMainInfo.Card_no = Reader["Card_no"].ToString() + "";
                    PYNBMainInfo.End_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["End_date"].ToString());
                    PYNBMainInfo.Hu_no = Reader["Hu_no"].ToString() + "";
                    PYNBMainInfo.Man_age = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["Man_age"].ToString());
                    PYNBMainInfo.Man_birth = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["Man_birth"].ToString());
                    PYNBMainInfo.Man_id = Reader["Man_id"].ToString() + "";
                    PYNBMainInfo.Man_name = Reader["Man_name"].ToString().Trim() + "";
                    PYNBMainInfo.Man_no = Reader["Man_no"].ToString() + "";
                    PYNBMainInfo.Man_sex = Reader["Man_sex"].ToString() + "";
                    PYNBMainInfo.Man_type = Reader["Man_type"].ToString() + "";
                    PYNBMainInfo.Medi_mark = Reader["Medi_mark"].ToString() + "";
                    PYNBMainInfo.Medi_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["Medi_sum"].ToString());
                    PYNBMainInfo.N_hosp_code = Reader["N_hosp_code"].ToString() + "";
                    PYNBMainInfo.Pay_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["Pay_sum"].ToString());
                    PYNBMainInfo.Poli_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["Poli_date"].ToString());
                    PYNBMainInfo.Poli_no = Reader["Poli_no"].ToString() + "";
                    PYNBMainInfo.Poli_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["Poli_sum"].ToString());
                    PYNBMainInfo.Remarks = Reader["Remarks"].ToString() + "";
                    PYNBMainInfo.Stuff_no = Reader["Stuff_no"].ToString() + "";
                    PYNBMainInfo.Sys_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["Sys_date"].ToString());
                    PYNBMainInfo.Work_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["Work_date"].ToString());
                    PYNBMainInfo.Year_insur_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["Year_insur_sum"].ToString());
                    PYNBMainInfo.Yu_insur_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["Yu_insur_sum"].ToString());
                    PYNBMainInfo.Last_ph = 1;

                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            return PYNBMainInfo;
        }

        #endregion

        #region 根据住院流水号判断是否做了登记
        /// <summary>
        /// 根据住院流水号判断是否做了登记
        /// </summary>
        /// <param name="patientNo">住院流水号</param>	
        public Models.PYNBMainInfo GetRegPatientInfo(string patientNo)
        {
            string sql = @"SELECT * FROM medical_host_"+hosCode +@" WHERE state_flag = '00'   AND medi_sn = '" + patientNo + "'and appl_year = '" +
                           System.DateTime.Now.Year.ToString() + "'";

            if (this.ExecQuery(sql) == -1)
                return null;
            Models.PYNBMainInfo PYNBMainInfo = new Models.PYNBMainInfo();
            try
            {
                if (Reader.Read())
                {
                    PYNBMainInfo.Appl_year = Neusoft.FrameWork.Function.NConvert.ToInt32(Reader["Appl_year"].ToString());
                    PYNBMainInfo.Card_no = Reader["Card_no"].ToString() + "";
                    PYNBMainInfo.Medi_no = Reader["Medi_no"].ToString();
                    PYNBMainInfo.Man_name = Reader["Man_name"].ToString() + "";
                    PYNBMainInfo.Last_ph = Neusoft.FrameWork.Function.NConvert.ToInt32(Reader["last_ph"].ToString());
                    PYNBMainInfo.Stock_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["stock_date"].ToString());
                    PYNBMainInfo.Medi_nn = Reader["Medi_nn"].ToString();
                    PYNBMainInfo.Medi_sn = Reader["Medi_sn"].ToString();
                }
                Reader.Close();
            }
            catch
            {
                Reader.Close();
                return PYNBMainInfo;
            }

            return PYNBMainInfo;
        }
        #endregion

        #region 修改上传状态
        /// <summary>
        /// 修改上传状态
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        public int UpdateSendState(ref Models.PYNBMainInfo PYNBMainInfo, string sendState)
        {
            string sql = @"update medical_host_" + hosCode + @" set stuff_name = '" + sendState + "'WHERE state_flag = '00' and medi_no = '" + PYNBMainInfo.Medi_no + "'";

            if (this.ExecNoQuery(sql) == -1)
                return -1;
            return 1;

        }
        #endregion

        #region 获取上传状态
        /// <summary>
        /// 获取上传状态
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public string GetSendState(ref Models.PYNBMainInfo PYNBMainInfo)
        {
            string sendState = string.Empty;
            string sql = @"SELECT stuff_name FROM medical_host_" + hosCode + @" WHERE state_flag = '00' and medi_no = '" + PYNBMainInfo.Medi_no + "'";
            if (this.ExecQuery(sql) == -1)
                return null;

            try
            {
                if (Reader.Read())
                {
                    sendState = Reader["stuff_name"].ToString();
                    PYNBMainInfo.Stuff_name = sendState;
                }
                Reader.Close();
            }
            catch
            {
                Reader.Close();
                return sendState;
            }

            return sendState;
        }
        #endregion

        #region 获取就医登记号
        /// <summary>
        /// 获取就医登记号
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public string GetMediSnInfo(ref Models.PYNBMainInfo PYNBMainInfo)
        {
            string mediSn = string.Empty;
            string sql = @"SELECT medi_no,stock_date FROM medical_host_" + hosCode + @" WHERE state_flag = '00'   AND medi_sn = '" +
                           PYNBMainInfo.Medi_sn + "'and appl_year = '" +
                System.DateTime.Now.Year.ToString() + "' and card_no = '" + PYNBMainInfo.Card_no + "' and last_ph =" + PYNBMainInfo.Last_ph;

            if (this.ExecQuery(sql) == -1)
                return null;

            try
            {
                if (Reader.Read())
                {
                    PYNBMainInfo.Medi_no = Reader["medi_no"].ToString();
                    PYNBMainInfo.Stock_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["stock_date"].ToString());
                    mediSn = PYNBMainInfo.Medi_no;
                }
                Reader.Close();
            }
            catch
            {
                Reader.Close();
                return mediSn;
            }

            return mediSn;
        }
        #endregion

        #region 注释掉的函数
        //		/// <summary>
        //		/// 写数据控制表
        //		/// </summary>
        //		/// <param name="PYNBMainInfo">写数据控制表</param>	
        //		public int SeDataChangeInfo( Models.PYNBMainInfo PYNBMainInfo,string data_type,string flag_state,int data_count)
        //		{			
        //			string s_stock_date = string.Empty ;
        //			string e_stock_date = string.Empty;
        //			string end_date = string.Empty;	
        //			string srecType = string.Empty;
        //			end_date = GetDateToday();
        //
        //			s_stock_date = PYNBMainInfo.Stock_date.ToString ("MM-dd-yyyy");
        //				
        //			if (s_stock_date == "01-01-0001")
        //			{
        //				s_stock_date = "";
        //			}
        //
        //			s_stock_date = s_stock_date.Replace ("-","/");			       
        //			
        //			string strSql = "insert into data_ch_0076 (appl_year, hosp_code, medi_nn, medi_no, man_name, ph, data_count, data_sum, data_type, s_stock_date, e_stock_date, sys_date, state_flag, rec_data_count, rec_data_sum, rec_state, fail_reason, rec_date) ";
        //			strSql = strSql + " values({0},'0076','{1}',{2},'{3}',{4},{5},{6},'{7}','{8}','{9}',GETDATE(),'{10}','{11}','{12}','0','{13}','{14}')";
        //			strSql = String.Format(strSql,PYNBMainInfo.Appl_year ,PYNBMainInfo.Medi_nn ,PYNBMainInfo.Medi_no,PYNBMainInfo.Man_name.Trim (),
        //				   PYNBMainInfo.Last_ph  , data_count,0,data_type,end_date,end_date,flag_state,0,0,"","");
        //			if ( this.ExecNoQuery(strSql) > 0 )
        //			{
        //				// 读取执行信息
        //				System.Threading.Thread.Sleep(1000); //停留1秒，看结果
        //                strSql = @"select rec_state from data_ch_0076 where medi_no = '"+PYNBMainInfo.Medi_no+"' and flag_state = '00' and data_type = '"+data_type+"'";
        //				if(this.ExecQuery(srecType) == -1)
        //				{
        //					return -1;
        //				}			
        //
        //				if (Reader.Read())
        //				{
        //					srecType = this.Reader[0].ToString(); 
        //					if (srecType == "E")
        //						return -1;
        //				}
        //			}
        //			else
        //			{
        //				return -1;
        //			}
        //
        //			return 1;
        //
        //		}
        //
        #endregion

        #region 写数据控制表
        /// <summary>
        /// 写数据控制表
        /// </summary>
        /// <param name="PYNBMainInfo">写数据控制表</param>	
        public int SeDataChangeInfo(Models.PYNBMainInfo PYNBMainInfo, string data_type, string flag_state, int data_count, ref string fail_reason)
        {
            string s_stock_date = string.Empty;
            string e_stock_date = string.Empty;
            string end_date = string.Empty;
            string srecType = string.Empty;
            //如果出院日期不为空，则以出院日期为准，否则取当前日期
            end_date = PYNBMainInfo.Out_date.ToString("MM-dd-yyyy");
            if (end_date == "01-01-0001")
            {
                end_date = GetDateToday();
            }
            end_date = GetDateToday();
            end_date = end_date.Replace("-", "/");

            s_stock_date = PYNBMainInfo.Stock_date.ToString("MM-dd-yyyy");

            if (s_stock_date == "01-01-0001")
            {
                s_stock_date = "";
            }

            s_stock_date = s_stock_date.Replace("-", "/");

            string strSql = "insert into data_ch_" + hosCode + @" (appl_year, hosp_code, medi_nn, medi_no, man_name, ph, data_count, data_sum, data_type, s_stock_date, e_stock_date, sys_date, state_flag, rec_data_count, rec_data_sum, rec_state, fail_reason, rec_date) ";
            strSql = strSql + " values({0},'0076','{1}',{2},'{3}',{4},{5},{6},'{7}','{8}','{9}',GETDATE(),'{10}','{11}','{12}','0','{13}','{14}')";
            if (data_type == "12") //出院登记的时候控制表需要写金额
            {
                strSql = String.Format(strSql, PYNBMainInfo.Appl_year, PYNBMainInfo.Medi_nn, PYNBMainInfo.Medi_no, PYNBMainInfo.Man_name.Trim(),
                    PYNBMainInfo.Last_ph, data_count, Neusoft.FrameWork.Function.NConvert.ToDecimal(PYNBMainInfo.User01), data_type, s_stock_date, end_date, flag_state, 0, 0, "", "");
            }
            else
            {
                strSql = String.Format(strSql, PYNBMainInfo.Appl_year, PYNBMainInfo.Medi_nn, PYNBMainInfo.Medi_no, PYNBMainInfo.Man_name.Trim(),
                    PYNBMainInfo.Last_ph, data_count, 0, data_type, s_stock_date, end_date, flag_state, 0, 0, "", "");
            }
            return this.ExecNoQuery(strSql);


        }
        #endregion

        #region  读取执行信息
        /// <summary>
        /// 读取执行信息
        /// </summary>
        /// <param name="medi_no">住院登记号</param>
        /// <param name="data_type">信息类型</param>
        /// <param name="failreason">农保那边返回的信息</param>
        /// <returns></returns>
        public string getrecState(string medi_no, string data_type, ref string failreason)
        {
            string fail_reason = string.Empty;
            string srecType = string.Empty;
            string strSql = string.Empty;
            System.Threading.Thread.Sleep(1000); //停留1秒，看结果
            strSql = @"select rec_state,fail_reason from data_ch_" + hosCode + @" where medi_no = '" + medi_no + "' and state_flag = '00' and data_type = '" + data_type + "'";
            try
            {
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                if (Reader.Read())
                {
                    srecType = this.Reader[0].ToString();
                    failreason = this.Reader[1].ToString();
                    if (srecType == "E")
                    {
                        return srecType;
                    }
                }
            }
            finally
            {
                Reader.Close();
            }
           
            return null;
        }
        #endregion

        #region 写数据控制表
        /// <summary>
        /// 写数据控制表
        /// </summary>
        /// <param name="PYNBMainInfo">写数据控制表</param>	
        public int SeDataChangeInfoDraw(Models.PYNBMainInfo PYNBMainInfo, string data_type, string flag_state, int data_count, decimal sum_account)
        {
            string s_stock_date = string.Empty;
            string e_stock_date = string.Empty;
            string end_date = string.Empty;
            //如果出院日期不为空，则以出院日期为准，否则取当前日期
            end_date = PYNBMainInfo.Out_date.ToString("MM-dd-yyyy");
            if (end_date == "01-01-0001")
            {
                end_date = GetDateToday();
            }
            end_date = end_date.Replace("-", "/");

            s_stock_date = PYNBMainInfo.Stock_date.ToString("MM-dd-yyyy");

            if (s_stock_date == "01-01-0001")
            {
                s_stock_date = "";
            }

            s_stock_date = s_stock_date.Replace("-", "/");

            string strSql = "insert into data_ch_" + hosCode + @" (appl_year, hosp_code, medi_nn, medi_no, man_name, ph, data_count, data_sum, data_type, s_stock_date, e_stock_date, sys_date, state_flag, rec_data_count, rec_data_sum, rec_state, fail_reason, rec_date) ";
            strSql = strSql + " values('{0}','0076','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',GETDATE(),'{10}','{11}','{12}','0','{13}','{14}')";
            strSql = String.Format(strSql, PYNBMainInfo.Appl_year, PYNBMainInfo.Medi_nn, Neusoft.FrameWork.Function.NConvert.ToDecimal(PYNBMainInfo.Medi_no), PYNBMainInfo.Man_name.Trim(),
                PYNBMainInfo.Last_ph, data_count, sum_account, data_type, s_stock_date, end_date, flag_state, 0, 0, "", "");
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region  农保病人登记
        /// <summary>
        /// 农保病人登记
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public int SetRegPersonInfo(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            string stockdate = string.Empty; //入院日期
            string polidate = string.Empty;
            string enddate = string.Empty;

            stockdate = PYNBMainInfo.Stock_date.ToString("MM-dd-yyyy");
            polidate = PYNBMainInfo.Poli_date.ToString("MM-dd-yyyy");
            enddate = PYNBMainInfo.End_date.ToString("MM-dd-yyyy");
            if (stockdate == "01-01-0001")
            {
                stockdate = "";
            }
            stockdate = stockdate.Replace("-", "/");
            polidate = polidate.Replace("-", "/");
            enddate = enddate.Replace("-", "/");

            sql = @"INSERT INTO medical_host_" + hosCode + @"
			(appl_year,
			stock_date,
			card_no,
			man_name,
			poli_date,
			end_date,			
			hosp_code,
			zykb,
			jyzd,
			turn_flag,
			turn_hosp,
			stuff_no,
			stuff_name,
			sys_date,
			state_flag,
			rec_state,
			medi_sn,
			last_ph,
            medi_nn)
			VALUES
			({0},
			'{1}',
			'{2}',
			'{3}',
			'{4}',
			'{5}',			
			'0076',
			'{6}',
			'{7}',
			'{8}',
			'{9}',
			'{10}',
			'{11}',
			getdate(),
			'00',
			'0',
			'{12}',
			{13},
            '{14}')";

            sql = string.Format(sql, PYNBMainInfo.Appl_year, stockdate, PYNBMainInfo.Card_no, PYNBMainInfo.Man_name, polidate, enddate, PYNBMainInfo.Zykb, PYNBMainInfo.Jyzd, PYNBMainInfo.Turn_flag, PYNBMainInfo.Turn_hosp, PYNBMainInfo.Stuff_no, PYNBMainInfo.Stuff_name, PYNBMainInfo.Medi_sn, PYNBMainInfo.Last_ph, PYNBMainInfo.Medi_nn);
            return this.ExecNoQuery(sql);
        }
        #endregion

        #region 取消登记
        /// <summary>
        /// 取消登记
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public int CancelRegPerson(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            sql = @" update medical_host_" + hosCode + @" set state_flag = '11',rec_state='0' where state_flag = '00' and medi_no ='" + PYNBMainInfo.Medi_no + "'";
            return this.ExecNoQuery(sql);
        }
        #endregion

        #region  查询是否做了入院登记
        /// <summary>
        /// 查询是否做了入院登记
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public int GetIfInRegister(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            string stock_date = string.Empty;
            string out_state = string.Empty;
            int getRowcount = 0;
            sql = @"select count(*) from medical_host_" + hosCode + @" where  state_flag = '00' and card_no = '" + PYNBMainInfo.Card_no +
                "' and medi_sn = '" + PYNBMainInfo.Medi_sn + "'";
            if (this.ExecQuery(sql) == -1)
                return 0;
            try
            {
                if (Reader.Read())
                {
                    getRowcount = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString() + "");
                }
                Reader.Close();
            }
            catch { }
            return getRowcount;
        }
        #endregion

        #region 根据住院号查询是否已经取消结算或者汇总上传(因获取数据不对，此方法作废不用)
        /// <summary>
        /// 根据住院号查询是否已经取消结算或者汇总上传(因获取数据不对，此方法作废不用)
        /// 返回布尔值：true未取消或者出错，false已取消
        /// </summary>
        /// <param name="medi_nn">住院号</param>
        /// <param name="typedata">查找类型</param>
        /// <param name="ErrorMes">错误信息</param>
        /// <returns>返回布尔值：false未取消或者出错，true已取消</returns>
        public bool JudSettlementByMedinn(string medi_nn, string typedata, ref string ErrorMes)
        {
            string sql = "select appl_year,hosp_code,medi_nn,medi_no,man_name,ph,data_count,data_sum,data_type,s_stock_date,e_stock_date,sys_date,state_flag,rec_state,fail_reason from data_ch_" + hosCode + @" where medi_nn='{0}' and data_type='{1}' and hosp_code='0076' order by sys_date desc";
            sql = string.Format(sql, medi_nn, typedata);
            if (this.ExecQuery(sql) == -1)
            {
                ErrorMes = "查询出错";
                return false;
            }
            try
            {
                if (Reader.Read())		//读第一条数据，也就是读取data_ch控制表中最后一次病者的插入数据
                {
                    if (Reader["state_flag"].ToString() == "11")			//判断最后一条数据是否有效，有效的话说明未取消
                    {
                        return true;
                    }
                    else
                    {
                        if (typedata == "21")
                        {
                            ErrorMes = "没有取消费用结算，请先取消";
                        }
                        else if (typedata == "13")
                        {
                            ErrorMes = "没有取消费用汇总上传，请先取消";
                        }
                        else
                        {
                            ErrorMes = "没有取消上一步应该取消的";
                        }
                        return false;
                    }
                }
                else
                {
                    ErrorMes = "没找到相关信息";
                    return false;
                }
                Reader.Close();
            }
            catch
            {
                ErrorMes = "读取判断出错";
                return false;
            }
        }
        #endregion

        #endregion

        #region 医嘱明细操作

        #region  插入单条医嘱明细
        /// <summary>
        /// 插入单条医嘱明细
        /// </summary>
        /// <param name="pInfo">住院患者基本信息,包括医保基本信息</param>
        /// <param name="obj">费用明细信息</param>
        /// <returns></returns>
        public int InsertNBItemList(Models.PYNBMainInfo PYNBMainInfo, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList obj, string state_flag)
        {
            #region old
            string sDtFee = string.Empty;

            sDtFee = obj.FeeOper.OperTime.ToString();
            string sql = "insert into yu_meditem_" + hosCode + @"(hosp_code, medi_nn, medi_no, ph, man_name, card_no, item_code, item_name, item_flag, hos_item_code, hos_item_name, num, unit_type, stock_sum, item_rate, medi_sum, stock_date, state_flag, rec_state, sys_date) ";
            sql += "values('" + hosCode + @"',PYNBMainInfo.Medi_nn,PYNBMainInfo.Medi_no,PYNBMainInfo.Last_ph,PYNBMainInfo.Man_name,PYNBMainInfo.Card_no,obj.Item.User01,obj.Item.User02,obj.Item.User03, obj.Item.ID ,obj.Item.Name ,obj.Item.Amount.ToString() ,obj.Item.SysClass,obj.Item.MinFee.User01,obj.Rate.ToString(),obj.Item.MinFee.User02,sDtFee,state_flag,'0',getdate())";
            if (this.ExecNoQuery(sql) == -1)
                return -1;

            #endregion
            return 0;
        }
        #endregion

        #region 对冲上传的明细
        /// <summary>
        /// 对冲上传的明细
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <param name="obj"></param>
        /// <param name="state_flag"></param>
        /// <returns></returns>
        public int UpdateNBItemList(Models.PYNBMainInfo PYNBMainInfo, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList obj, string state_flag)
        {

            string sDtFee = string.Empty;

            sDtFee = obj.FeeOper.OperTime.ToString();
            string sql = "update  yu_meditem_" + hosCode + " set state_flag = '10' where medi_nn = PYNBMainInfo.Medi_nn and ph = PYNBMainInfo.Last_ph and stock_date = sDtFee and state_flag = '00' and card_no = PYNBMainInfo.Card_no ";

            if (this.ExecNoQuery(sql) == -1)
                return -1;

            return 0;
        }
        #endregion

        #region 循环插入医嘱明细
        /// <summary>
        /// 循环插入医嘱明细
        /// </summary>
        /// <param name="pInfo">患者基本信息,包括医保信息</param>
        /// <param name="itemList">患者费用明细实体数组</param>
        /// <returns></returns>
        public int InsertNBItemList(Models.PYNBMainInfo PYNBMainInfo, ArrayList itemList)
        {

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList obj in itemList)
            {
                if (this.InsertNBItemList(PYNBMainInfo, obj, "00") == -1)
                    return -1;
            }
            return 0;
        }
        #endregion

        #region 取消明细上传
        /// <summary>
        /// 取消明细上传
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public int UpdateNBItemList(Models.PYNBMainInfo PYNBMainInfo, ArrayList itemList)
        {
            this.UpdateNBItemList(PYNBMainInfo, itemList);
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList obj in itemList)
            {
                if (this.InsertNBItemList(PYNBMainInfo, obj, "11") == -1)
                    return -1;
            }
            return 0;
        }
        #endregion

        #region 费用汇总传送
        /// <summary>
        /// 费用汇总传送
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public int InsertNBMItemList(Models.PYNBMainInfo PYNBMainInfo, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList obj, string state_flag)
        {
            string sDtFee = string.Empty;
            string sDtFee2 = string.Empty;

            //如果出院日期不为空，则以出院日期为准，否则取当前日期
            sDtFee = PYNBMainInfo.Out_date.ToString("MM-dd-yyyy");
            if (sDtFee == "01-01-0001")
            {
                sDtFee = GetDateToday();
            }
            sDtFee = sDtFee.Replace("-", "/");

            sDtFee2 = PYNBMainInfo.Stock_date.ToString("MM-dd-yyyy");
            if (sDtFee2 == "01-01-0001")
            {
                sDtFee2 = "";
            }
            sDtFee2 = sDtFee2.Replace("-", "/");

            string sql = "insert into meditem_" + hosCode + @"( use_year, hosp_code, medi_nn, medi_no, ph, man_name, card_no, item_code, item_name, item_flag, hos_item_code, hos_item_name, num, unit_type, stock_sum, item_rate, medi_sum, state_flag, s_stock_date, e_stock_date, rec_state, sys_date) ";
            sql = sql + " values({0},'{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','0',getdate())";
            sql = string.Format(sql, PYNBMainInfo.Appl_year, "0076", PYNBMainInfo.Medi_nn, PYNBMainInfo.Medi_no, PYNBMainInfo.Last_ph, PYNBMainInfo.Man_name,
                PYNBMainInfo.Card_no, obj.Item.ID, obj.Item.Name, obj.Item.User03, obj.Item.User01, obj.Item.User02.Length > 20 ? obj.Item.User02.Substring(0, 20) : obj.Item.User02, obj.Item.Qty.ToString(),
                                    obj.Item.PriceUnit, obj.Item.MinFee.User01, obj.FTRate.PubRate.ToString(), obj.Item.MinFee.User02, state_flag, sDtFee2, sDtFee);
            try
            {
                if (this.ExecNoQuery(sql) == -1)
                    return -1;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return 1;
        }
        #endregion

        #region 取消(修改)费用汇总传送
        /// <summary>
        /// 取消(修改)费用汇总传送
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public int CancelNBMItemList(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            sql = "insert into meditem_" + hosCode + @" select use_year, hosp_code, medi_nn, medi_no, ph, man_name, card_no, item_code, item_name, item_flag, hos_item_code, hos_item_name,                   num, unit_type, stock_sum, item_rate, medi_sum, '11', s_stock_date, e_stock_date, rec_state, sys_date from meditem_0076";
            sql += " where state_flag = '00' and medi_nn = '" + PYNBMainInfo.Medi_nn + "' and medi_no = '" + PYNBMainInfo.Medi_no +
                 "' and card_no = '" + PYNBMainInfo.Card_no + "' and ph = " + PYNBMainInfo.Last_ph;

            if (this.ExecNoQuery(sql) == -1)
                return -1;

            sql = @"update meditem_" + hosCode + @" set state_flag = '11' ,rec_state='0' where state_flag = '00' and medi_nn = '" + PYNBMainInfo.Medi_nn + "' and medi_no = '" + PYNBMainInfo.Medi_no +
                 "' and card_no = '" + PYNBMainInfo.Card_no + "' and ph = " + PYNBMainInfo.Last_ph;

            if (this.ExecNoQuery(sql) == -1)
                return -1;

            return 1;

        }
        #endregion


        #region 费用明细上传
        public int InsertNBMFeeItemList(Models.PYNBMainInfo PYNBMainInfo,DateTime dtFee, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList obj, string state_flag)
        {
            string sDtFee = string.Empty;
            string sDtFee2 = string.Empty;

            //如果出院日期不为空，则以出院日期为准，否则取当前日期

            sDtFee = dtFee.ToString("yyyy-MM-dd");

            sDtFee2 = PYNBMainInfo.Stock_date.ToString("MM-dd-yyyy");
            if (sDtFee2 == "01-01-0001")
            {
                sDtFee2 = "";
            }
            sDtFee2 = sDtFee2.Replace("-", "/");

            string sql = "insert into yu_meditem_" + hosCode + @"(  hosp_code, medi_nn, medi_no, ph, man_name, card_no, item_code, item_name,item_flag, hos_item_code, hos_item_name, num, unit_type, stock_sum, item_rate, medi_sum, state_flag, stock_date, rec_state, sys_date) ";
            sql = sql + " values('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','0','{18}')";
            sql = string.Format(sql,  "0076", PYNBMainInfo.Medi_nn, PYNBMainInfo.Medi_no, PYNBMainInfo.Last_ph, PYNBMainInfo.Man_name,
                PYNBMainInfo.Card_no, obj.Item.ID, obj.Item.Name, obj.Item.User03, obj.Item.User01, obj.Item.User02.Length > 20 ? obj.Item.User02.Substring(0, 20) : obj.Item.User02, obj.Item.Qty.ToString(),
                                    obj.Item.PriceUnit, obj.Item.MinFee.User01, obj.FTRate.PubRate.ToString(), obj.Item.MinFee.User02, state_flag, sDtFee2, sDtFee);
            try
            {
                if (this.ExecNoQuery(sql) == -1)
                {
                    this.Err = "插入费用明细出错！";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err += ex.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 取消（修改）费用明细上传
        public int CancelNBMFeeItemList(Models.PYNBMainInfo PYNBMainInfo,DateTime dtFee)
        {
            string strDtFee = dtFee.ToString("yyyyMMdd");
            string sql = string.Empty;
            sql = "insert into yu_meditem_" + hosCode + @"(hosp_code, medi_nn, medi_no, ph, man_name, card_no, item_code, 
item_name, item_flag, hos_item_code, hos_item_name, num, unit_type,
 stock_sum, item_rate, medi_sum, state_flag, stock_date, rec_state, sys_date) select  hosp_code, medi_nn, medi_no, ph, man_name, card_no, item_code, item_name, item_flag, hos_item_code, hos_item_name, num, unit_type, stock_sum, item_rate, medi_sum, '11', stock_date, rec_state, sys_date from yu_meditem_" + hosCode;
            sql += "  where state_flag = '00' and medi_nn = '" + PYNBMainInfo.Medi_nn + "' and medi_no = '" + PYNBMainInfo.Medi_no +
                 "' and card_no = '" + PYNBMainInfo.Card_no + "' and ph = " + PYNBMainInfo.Last_ph + " and substring(Convert(char(10),sys_date,112),1,8)='"+strDtFee+"' ";

            if (this.ExecNoQuery(sql) == -1)
                return -1;

            sql = @"update yu_meditem_" + hosCode + @" set state_flag = '11' ,rec_state='0' where state_flag = '00' and medi_nn = '" + PYNBMainInfo.Medi_nn + "' and medi_no = '" + PYNBMainInfo.Medi_no +
                 "' and card_no = '" + PYNBMainInfo.Card_no + "' and ph = " + PYNBMainInfo.Last_ph + " and substring(Convert(char(10),sys_date,112),1,8)='" + strDtFee + "' ";

            if (this.ExecNoQuery(sql) == -1)
                return -1;

            return 1;

        }
        #endregion

        #region 是否上传过当日费用明细
        public int GetFeeItemCount(Models.PYNBMainInfo PYNBMainInfo, DateTime dtFee)
        {
            int result = 0;
            string strDtFee = dtFee.ToString("yyyyMMdd");
            string sql = @"SELECT COUNT(1) FROM yu_meditem_" + hosCode + @" a WHERE a.medi_nn='{0}' AND a.medi_no='{1}' AND a.card_no='{2}' AND a.ph='{3}' AND  substring(Convert(char(10),a.sys_date,112),1,8)='{4}' and  a.state_flag='00'";
            try
            {
                sql = string.Format(sql, PYNBMainInfo.Medi_nn, PYNBMainInfo.Medi_no, PYNBMainInfo.Card_no, PYNBMainInfo.Last_ph, strDtFee);
                if (this.ExecQuery(sql) == -1)
                    return -1;
                    if (Reader.Read())
                    {
                        result = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString() + "");
                    }
                    Reader.Close();
            }
            catch (Exception exp)
            {

                this.Err = "获取当日有效费用明细出错！"+exp.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 是否上传过医院实付登记（有效）
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <param name="dtFee"></param>
        /// <returns></returns>
        public int GetInvoiceCount(Models.PYNBMainInfo PYNBMainInfo,string invoiceNo)
        {
            int result = 0;
            string sql = @"SELECT COUNT(1) FROM hosp_pay_" + hosCode + @" a WHERE a.medi_nn='{0}' AND a.medi_no='{1}' AND a.pay_no='{2}' AND a.ph='{3}' and a.rec_state='1' and  a.state_flag='00'";
            try
            {
                sql = string.Format(sql, PYNBMainInfo.Medi_nn, PYNBMainInfo.Medi_no, invoiceNo, PYNBMainInfo.Last_ph);
                if (this.ExecQuery(sql) == -1)
                    return -1;
                if (Reader.Read())
                {
                    result = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString() + "");
                }
                Reader.Close();
            }
            catch (Exception exp)
            {

                this.Err = "获取当日有效费用明细出错！" + exp.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return result;
        }
        #endregion

        #region 出院登记
        /// <summary>
        /// 出院登记
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public int outRegister(Models.PYNBMainInfo PYNBMainInfo, string outstate)
        {
            string sql = string.Empty;
            string stock_date = string.Empty;
            string sDtFee = string.Empty;
            string sOperName = PYNBMainInfo.Stuff_name;
            string sDtOutFee = PYNBMainInfo.Out_date.ToString("MM-dd-yyyy");		//出院日期
            if (sDtOutFee == "01-01-0001")
            {
                sDtOutFee = GetDateToday();
            }
            sDtOutFee = sDtOutFee.Replace("-", "/");

            sDtFee = PYNBMainInfo.Stock_date.ToString("MM-dd-yyyy");				//入院日期
            if (sDtFee == "01-01-0001")
            {
                sDtFee = "";
            }
            sDtFee = sDtFee.Replace("-", "/");

            sql = @"
							INSERT INTO out_host_" + hosCode + @"
							(stock_date,
							card_no,
							man_name,
							medi_no,
							hosp_code,
							ph,
							medi_nn,
							zykb,
							zycw_no,
							turn_flag,
							turn_hosp,
							out_date,
							stock_sum,
							cyzd,
							out_state,
							stuff_no,
							stuff_name,
							sys_date,
							state_flag,
							rec_state)
							values
							('{0}',
							'{1}',
							'{2}',
							'{3}',
							'{4}',
							'{5}',
							'{6}',
							'{7}',
							'{8}',
							'{9}',
							'{10}',							
							'{11}',
							'{12}',
							'{13}',
							'{14}',
							'{15}'	,
							'{16}',
							getdate(),
							'00',
							'0')
							";

            sql = String.Format(sql, sDtFee, PYNBMainInfo.Card_no, PYNBMainInfo.Man_name, PYNBMainInfo.Medi_no, "0076", PYNBMainInfo.Last_ph.ToString(), PYNBMainInfo.Medi_nn,
                PYNBMainInfo.Zykb, "", PYNBMainInfo.Turn_flag, PYNBMainInfo.Turn_hosp, sDtOutFee, Neusoft.FrameWork.Function.NConvert.ToDecimal(PYNBMainInfo.User01),
                PYNBMainInfo.OutDiagnosis, outstate, PYNBMainInfo.Stuff_no, PYNBMainInfo.Stuff_name);
            if (this.ExecNoQuery(sql) == -1)
                return -1;
            return 1;
        }

        #endregion

        #region 查询是否做了出院登记
        /// <summary>
        /// 查询是否做了出院登记
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public int GetIfOutRegister(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            string stock_date = string.Empty;
            string out_state = string.Empty;
            int getRowcount = 0;

            sql = "select count(*) from out_host_" + hosCode + @" where  state_flag = '00' and card_no = '" + PYNBMainInfo.Card_no +
                "' and medi_no = '" + PYNBMainInfo.Medi_no + "' and ph = " + PYNBMainInfo.Last_ph + " and medi_nn = '" + PYNBMainInfo.Medi_nn + "'";

            if (this.ExecQuery(sql) == -1)
                return 0;

            try
            {
                if (Reader.Read())
                {
                    getRowcount = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString() + "");
                }
                Reader.Close();
            }
            catch { }


            return getRowcount;
        }

        #endregion

        #region 取消出院登记
        /// <summary>
        /// 取消出院登记
        /// </summary>
        /// <returns></returns>
        public int CancelNBOutSet(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            string stock_date = string.Empty;
            string out_state = string.Empty;

            sql = "update out_host_" + hosCode + @" set  state_flag = '11' where card_no = '" + PYNBMainInfo.Card_no +
                "' and medi_no = '" + PYNBMainInfo.Medi_no + "' and ph = " + PYNBMainInfo.Last_ph + " and medi_nn = '" + PYNBMainInfo.Medi_nn + "'";
            if (this.ExecNoQuery(sql) == -1)
                return -1;

            return 1;
        }
        #endregion

        #endregion

        #region 结算信息

        #region 获取理赔给付结算信息
        /// <summary>
        /// 获取理赔给付结算信息
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public Models.PYNBMainInfo.Drawwork GetNBDrawworkInfo2(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            sql = @"select * from drawwork_" + hosCode + @" where state_flag ='00' and  medi_no = '" + PYNBMainInfo.Medi_no + "' and ph = " + PYNBMainInfo.Last_ph;
            if (this.ExecQuery(sql) == -1)
                return null;
            Models.PYNBMainInfo.Drawwork drawwork = new Models.PYNBMainInfo.Drawwork();
            try
            {
                while (Reader.Read())
                {
                    drawwork.dbranch_no = this.Reader["branch_no"].ToString() + "";
                    drawwork.dcountry_no = this.Reader["country_no"].ToString() + "";
                    drawwork.ddraw_id = this.Reader["draw_id"].ToString() + "";
                    drawwork.ddraw_name = this.Reader["draw_name"].ToString() + "";
                    drawwork.de_stock_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader["e_stock_date"].ToString());
                    drawwork.dhosp_code = this.Reader["hosp_code"].ToString() + "";
                    drawwork.dinsur_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["insur_sum"].ToString());
                    drawwork.dmedi_nn = this.Reader["medi_nn"].ToString() + "";
                    drawwork.dmedi_no = this.Reader["medi_no"].ToString();
                    drawwork.dmedi_type = this.Reader["medi_type"].ToString() + "";
                    drawwork.dmy_pay_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["my_pay_sum"].ToString());
                    drawwork.dop_stuff = this.Reader["op_stuff"].ToString() + "";
                    drawwork.dPH = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader["PH"].ToString());
                    drawwork.drec_state = this.Reader["rec_state"].ToString() + "";
                    drawwork.ds_stock_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader["s_stock_date"].ToString());
                    drawwork.dstart_pay_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["start_pay_sum"].ToString());
                    drawwork.dstate_flag = this.Reader["state_flag"].ToString() + "";
                    drawwork.dsum_medi_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["sum_medi_sum"].ToString());
                    drawwork.dunit_name = this.Reader["unit_name"].ToString() + "";
                    drawwork.dwork_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader["work_date"].ToString());
                    drawwork.dye_insur_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["ye_insur_sum"].ToString());

                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return drawwork;
            }

            return drawwork;
        }

        #endregion

        #region 理赔给付结算信息
        /// <summary>
        /// 理赔给付结算信息
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public Models.PYNBMainInfo.Drawwork GetNBBalanceInfo(Models.PYNBMainInfo PYNBMainInfo)
        {
            Models.PYNBMainInfo.Drawwork drawwork = new Models.PYNBMainInfo.Drawwork();
            string sql = @"select * from drawwork_" + hosCode + @"  where state_flag ='00' and medi_no = '" + PYNBMainInfo.Medi_no +
                        "' and ph =" + PYNBMainInfo.Last_ph;

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询农保患者结算信息失败!";
                return drawwork;
            }

            if (!Reader.HasRows)
            {

                this.ErrCode = "-1";
                this.Err = "没有患者结算信息";
                return drawwork;
            }

            if (Reader.Read())
            {
                
                drawwork.ddraw_name = this.Reader["draw_name"].ToString() + "";
                drawwork.ddraw_id = this.Reader["draw_id"].ToString() + ""; //-领款人身份证号码
                drawwork.dsum_medi_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["sum_medi_sum"].ToString()); //总费用 
                drawwork.dstart_pay_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["start_pay_sum"].ToString());
                drawwork.dinsur_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["insur_sum"].ToString());
                drawwork.dmy_pay_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["my_pay_sum"].ToString());
                drawwork.dye_insur_sum = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["ye_insur_sum"].ToString());

                drawwork.ds_stock_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader["s_stock_date"]);
                drawwork.de_stock_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader["e_stock_date"]);
                drawwork.dstate_flag = this.Reader["state_flag"].ToString();
              //  drawwork.drec_state = this.Reader["rec_state"].ToString();
                drawwork.dop_stuff = this.Reader["op_stuff"].ToString();

            }

            Reader.Close();

            return drawwork;
        }

        #endregion

        #region  获取患者结算信息
        /// <summary>
        /// 获取患者结算信息
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public ArrayList GetNBmedilistData(Models.PYNBMainInfo PYNBMainInfo)
        {
            ArrayList al = new ArrayList();
            Models.PYNBMainInfo.Medilist_data medilistdata = null;
            string sql = @"select * from medilist_data  where state_flag ='00' and medi_no = '" + PYNBMainInfo.Medi_no +
                "' and ph =" + PYNBMainInfo.Last_ph + " order by no";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询农保补偿结算过程信息失败!";
                return al;
            }

            if (!Reader.HasRows)
            {

                this.ErrCode = "-1";
                this.Err = "没有患者结算信息";
                return al;
            }

            while (Reader.Read())
            {
                medilistdata = new Models.PYNBMainInfo.Medilist_data();
                medilistdata.mno = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader["no"].ToString());
                medilistdata.msum1 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["sum1"].ToString());
                medilistdata.msum2 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["sum2"].ToString());
                medilistdata.mrate = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["rate"].ToString());
                medilistdata.mm_sum1 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["m_sum1"].ToString());
                medilistdata.mm_sum2 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader["m_sum2"].ToString());
                al.Add(medilistdata);
            }

            Reader.Close();

            return al;
        }

        #endregion

        #region 取消结算
        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <returns></returns>
        public int CancelBalance(Models.PYNBMainInfo PYNBMainInfo)
        {
            string sql = string.Empty;
            sql = @"insert into drawwork_" + hosCode + @" select  branch_no, country_no, medi_no, ph, medi_type, work_date, hosp_code, medi_nn, unit_name, 
                    draw_name, draw_id, sum_medi_sum, start_pay_sum, s_stock_date, e_stock_date, my_pay_sum, ye_insur_sum, insur_sum, op_stuff, 
                     '11', rec_state, other_name, other_sum, other_name1, other_sum1 from drawwork 
                     where  state_flag ='00' and medi_no = '" + PYNBMainInfo.Medi_no + "' and ph =" + PYNBMainInfo.Last_ph;
            this.ExecNoQuery(sql);
            //修改原记录
            sql = @"update drawwork_" + hosCode + @" set state_flag= '10' where  state_flag ='00' and medi_no = '" + PYNBMainInfo.Medi_no +
                    "' and ph =" + PYNBMainInfo.Last_ph;
            this.ExecNoQuery(sql);

            // 对冲结算过程表
            sql = @"insert into medilist_data select appl_year, branch_no, country_no, medi_no, ph, no, sum1, sum2, rate, m_sum1, m_sum2, 
                    '11', rec_state from medilist_data  where state_flag ='00' and medi_no = '" + PYNBMainInfo.Medi_no +
                   "' and ph =" + PYNBMainInfo.Last_ph;
            this.ExecNoQuery(sql);
            //修改原记录
            sql = @"update medilist_data set state_flag= '10' where state_flag ='00' and medi_no = '" + PYNBMainInfo.Medi_no +
                  "' and ph =" + PYNBMainInfo.Last_ph;
            this.ExecNoQuery(sql);
            return 1;
        }
        #endregion

        #endregion

        #region 医院实付款登记

        #region 医院结算款插入
        /// <summary>
        /// 医院结算款插入
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <param name="obj"></param>
        /// <param name="state_flag"></param>
        /// <returns></returns>
        public int InsertHosp_pay(Models.PYNBMainInfo PYNBMainInfo, Neusoft.HISFC.Models.Fee.Inpatient.Balance obj, string state_flag)
        {
            string sql = @"insert into  hosp_pay_" + hosCode + @"
					    (hosp_code,
					    medi_nn,
						medi_no,
						man_name,
                        ph,
						pay_no,
						h_pay_date,
						h_pay_stuff,
						h_pay_stuff_code,
						sum_medi_sum,
						h_pay_sum,
						state_flag,
						rec_state,
						lb_pay_flag) values
						('{0}',
						'{1}',
						'{2}',
						'{3}',
						'{4}',
						'{5}',			
						'{6}',
						'{7}',
						'{8}',
						'{9}',
						'{10}',
						'{11}',
						'{12}',
						'{13}')";
            sql = string.Format(sql,
                        hosCode,
                        PYNBMainInfo.Medi_nn,
                        Neusoft.FrameWork.Function.NConvert.ToDecimal(PYNBMainInfo.Medi_no),
                        obj.Invoice.Name,
                        PYNBMainInfo.Last_ph,
                        obj.Invoice.ID,
                        obj.BalanceOper.OperTime,
                        obj.BalanceOper.Name,
                        obj.BalanceOper.ID,
                        obj.FT.TotCost,
                        obj.FT.PubCost,//obj.Fee.TotCost-obj.Fee.Pub_Cost,
                        "00",
                        "0",
                        "0");
            if (this.ExecNoQuery(sql) == -1)
                return -1;

            return 1;
        }

        #endregion

        #region 取消实付款登记
        /// <summary>
        /// 取消实付款登记
        /// </summary>
        /// <param name="PYNBMainInfo"></param>
        /// <param name="obj"></param>
        /// <param name="state_flag"></param>
        /// <returns></returns>
        public int UpdateHosp_pay(Models.PYNBMainInfo PYNBMainInfo, Neusoft.HISFC.Models.Fee.Inpatient.Balance obj, string state_flag)
        {
            string sql = @"update hosp_pay_" + hosCode + @" set state_flag = '11' , rec_state='0'  where   pay_no ='" + obj.Invoice.ID
                         + "'  and  state_flag = '00' and medi_no = '" + PYNBMainInfo.Medi_no + "' and lb_pay_flag <> '1' ";

            if (this.ExecNoQuery(sql) == -1)
                return -1;
            return 1;
        }

        #endregion

        #endregion

        private string GetDateToday()
        {
            string sql = @"Select CONVERT(varchar(100), GETDATE(),101)";
            string toDay = string.Empty;
            if (this.ExecQuery(sql) == -1)
            {
                return "0000/00/00";
            }

            if (Reader.Read())
            {
                toDay = this.Reader[0].ToString();
            }

            Reader.Close();

            return toDay;
        }


        #region 获取农合的医院信息
        /// <summary>
        /// 获取农合的医院信息
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public int GetNBHospitalList(ref ArrayList al)
        {
            // 查询农合那边的医院信息
            string strSql = "select h.hosp_code,h.hosp_name,h.hosp_address from hospital_data h";
            Neusoft.FrameWork.Models.NeuObject obj;
            try
            {
                this.ExecQuery(strSql);
                obj = new Neusoft.FrameWork.Models.NeuObject();
                while (Reader.Read())
                {
                    obj = new Neusoft.FrameWork.Models.NeuObject();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.Memo = Reader[2].ToString();
                    al.Add(obj);
                }
                Reader.Close();
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        #endregion

    }
}
