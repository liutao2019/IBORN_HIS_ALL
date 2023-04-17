using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GZSI.ApiModel;
using FS.FrameWork.Models;
using GZSI.ApiControls;

namespace GZSI.ApiClass
{
    /// <summary>
    /// 广州医保API业务类
    /// </summary>
    public class GzsiApiBizProcess : FS.FrameWork.Management.Database
    {
        #region 业务类

        /// <summary>
        /// API操作HIS类
        /// </summary>
        private ApiManager.LocalManager localMgr = new ApiManager.LocalManager();

        #endregion

        #region 变量

        /// <summary>
        /// 日志路径
        /// </summary>
        private string strLog = @"C:\log";

        /// <summary>
        /// 是否为调试模式
        /// </summary>
        private int flag = 1;

        /// <summary>
        /// 实例化句柄
        /// </summary>
        private IntPtr pInt;

        /// <summary>
        /// API返回结果值变量
        /// </summary>
        long result = -1;

        /// <summary>
        /// 用于获取API调用后结果集
        /// </summary>
        StringBuilder strValue = new StringBuilder(100);

        private string operdate = string.Empty;
        /// <summary>
        /// 操作之间
        /// </summary>
        public string OperDate
        {
            get
            {
                if (string.IsNullOrEmpty(operdate))
                {
                    operdate = GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
                return operdate;
            }
            set { operdate = value; }

        }

        private string fin_staff = string.Empty;
        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperId
        {
            get { return fin_staff; }
            set { fin_staff = value; }
        }

        private string fin_man = string.Empty;
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperName
        {
            get { return fin_man; }
            set { fin_man = value; }
        }

        #endregion

        #region 属性

        private string server = "http://192.168.34.11/HygeiaWebService/web/ProcessAll.asmx";
        /// <summary>
        /// 应用服务器地址
        /// </summary>
        public string Server
        {
            set { server = value; }
        }

        private int port = 7001;
        /// <summary>
        /// 端口号
        /// </summary>
        public short Port
        {
            set { port = value; }
        }

        private string servlet = "hygeia";
        /// <summary>
        /// 应用服务器入口Servlet的名称
        /// </summary>
        public string Servlet
        {
            set { servlet = value; }
        }

        private string hospital_id = "006181";
        /// <summary>
        /// 医院编码
        /// </summary>
        public string HospitalID
        {
            set { hospital_id = value; }
        }

        StringBuilder strErrMsg = new StringBuilder(200);

        private string errMsg = string.Empty;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get { return errMsg; }
            set { errMsg = value; }
        }

        private string login_id = "hexu";
        /// <summary>
        /// 登录ID
        /// </summary>
        public string LoginID
        {
            set { login_id = value; }
        }

        private string login_password = "hexu";
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword
        {
            set { login_password = value; }
        }

        private string center_id = "620013";
        /// <summary>
        /// 中心编码
        /// </summary>
        public string CenterID
        {
            set { center_id = value; }
        }

        #endregion

        #region API接口封装

        public GzsiApiBizProcess()
        {
            if (!System.IO.Directory.Exists(strLog))
            {
                System.IO.Directory.CreateDirectory(strLog);
            }
        }

        /// <summary>
        /// 实例化接口
        /// </summary>
        /// <returns></returns>
        private int NewInterface()
        {
            pInt = GzsiAPI.newinterface();
            if (pInt == null)
            {
                this.errMsg = "错误:创建接口失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <returns></returns>
        private int InitInterface()
        {
            //if (-1 == NewInterface())
            //{
            //    return -1;
            //}
            //result = GzsiAPI.init(pInt, server, port, servlet);
            //if (-1 == result)
            //{
            //    this.errMsg = "错误:初始化接口失败！";
            //    return -1;
            //}

            pInt = GzsiAPI.newinterfacewithinit(server, port, servlet);
            if (null == pInt)
            {
                this.errMsg = "错误:初始化接口失败！";
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 传入参数
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        private int PutParams(NoSortHashtable ht)
        {
            if (ht == null || ht.Count == 0)
            {
                this.errMsg = "错误：参数不存在";
                return -1;
            }
            try
            {
                errMsg = "";
                //传入参数
                foreach (string key in ht.Keys)
                {
                    string par_value = ht[key].ToString();

                    result = GzsiAPI.putcol(pInt, key, par_value);
                    if (result < 0)
                    {
                        result = GzsiAPI.getmessage(pInt, strErrMsg);
                        this.errMsg += "错误：插入参数，参数名：" + key + " 值：" + par_value + "  " + strErrMsg.ToString();
                        return -1;
                    }
                }

            }
            catch (Exception e)
            {
                this.errMsg = "错误：插入参数 " + e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 调用医保业务初始操作
        /// </summary>
        /// <param name="func_id">功能号</param>
        /// <returns></returns>
        public int CallSIProcessBegin(string func_id)
        {
            if (-1 == InitInterface())
            {
                return -1;
            }

            //登录到前置服务器
            NoSortHashtable ht = new NoSortHashtable();
            ht.Add("login_id", this.login_id);
            ht.Add("login_password", this.login_password);

            result = GzsiAPI.start(pInt, "0");
            if (-1 == result)
            {
                this.errMsg = "错误：接口调用(登录到前置服务器)开始函数失败！";
                return -1;
            }
            //设置调试模式
            result = GzsiAPI.setdebug(pInt, flag, strLog);

            if (-1 == PutParams(ht))
            {
                return -1;
            }
            result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, this.strErrMsg);
                this.errMsg = this.strErrMsg.ToString();
                return -1;
            }

            //其他功能
            result = GzsiAPI.start(pInt, func_id);
            if (-1 == result)
            {
                this.errMsg = "错误：接口调用(" + func_id + ")开始函数失败！";
                return -1;
            }
            //设置调试模式
            result = GzsiAPI.setdebug(pInt, flag, strLog);

            return 1;

        }

        /// <summary>
        /// 执行医保api调用
        /// </summary>
        /// <param name="func_id"></param>
        /// <returns></returns>
        public int CallSIProcess(string func_id)
        {
            result = GzsiAPI.start(pInt, func_id);
            if (-1 == result)
            {
                this.errMsg = "错误：接口调用(" + func_id + ")开始函数失败！";
                return -1;
            }
            //设置调试模式
            result = GzsiAPI.setdebug(pInt, flag, strLog);

            return 1;

        }

        /// <summary>
        /// 调用医保业务结束操作
        /// </summary>
        /// <returns></returns>
        public int CallSIProcessEnd()
        {
            //if (pInt !=null)
            //{
            //    if (-1 == GzsiAPI.destoryinterface(pInt))
            //    {
            //        long result = GzsiAPI.setdebug(pInt, flag, strLog); 
            //        this.errMsg = "错误：释放接口的实例失败！";
            //        return -1;
            //    }
            //}
            return 1;
        }

        #endregion

        #region 获取个人信息

        /// <summary>
        /// 获取普通诊个人信息
        /// </summary>
        /// <param name="idcard">查询信息</param>
        /// <param name="biz_type">业务类型：11：普通门诊 </param>
        /// <param name="flag">1:身份证 2：个人电脑号 3：姓名 4:医疗证号</param>
        /// <returns></returns>
        public ArrayList GetPtSiPersonInfo(string idcard, string biz_type, DateTime biz_date)
        {
            NoSortHashtable ht = new NoSortHashtable();
            string flag = "1";
            string[] serch = idcard.Split('|');
            if (serch.Length > 1)
            {
                flag = serch[0];
                idcard = serch[1];
            }
            if (flag == "1")
            {
                ht.Add("idcard", idcard);
            }
            else if (flag == "2")
            {
                //indi_id
                ht.Add("indi_id", idcard);
            }
            else if (flag == "3")
            {
                ht.Add("name", idcard);
            }
            else if (flag == "4")
            {
                ht.Add("insr_code", idcard);
            }
            ht.Add("hospital_id", hospital_id);
            ht.Add("biz_type", biz_type);
            ht.Add("biz_date", biz_date.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("treatment_type", "110");

            if (-1 == CallSIProcessBegin("BIZH131001")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString();
                return null;
            }
            //获取结果
            result = GzsiAPI.setresultset(pInt, "PersonInfo");
            if (-1 == result)
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);

            if (l_size > 1)
            {
                //多条记录
                try
                {
                    do
                    {
                        PersonInfo pInfo = new PersonInfo();
                        GzsiAPI.getbyname(pInt, "indi_id", strValue);
                        pInfo.Api_indi_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "idcard", strValue);
                        pInfo.Api_idcard = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "birthday", strValue);
                        pInfo.Api_birthday = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_id", strValue);
                        pInfo.Api_corp_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_name", strValue);
                        pInfo.Api_corp_name = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "name", strValue);
                        pInfo.Api_name = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "office_grade", strValue);
                        pInfo.Api_office_grade = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                        pInfo.Api_pers_identity = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "pers_status", strValue);
                        pInfo.Api_pers_status = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "post_code", strValue);
                        pInfo.Api_post_code = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "sex", strValue);
                        pInfo.Api_sex = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "telephone", strValue);
                        pInfo.Api_telephone = strValue.ToString(); ;


                        pInfo.Api_treatment_type = "110";
                        alPersonInfo.Add(pInfo);


                    } while (GzsiAPI.nextrow(pInt) > 0);
                }
                catch (Exception ex)
                {
                    this.errMsg = "错误：获取多行个人信息失败！" + ex.Message;
                    return null;
                }


            }
            else if (l_size == 1)
            {

                //一行数据
                PersonInfo pInfo = new PersonInfo();
                GzsiAPI.getbyname(pInt, "indi_id", strValue);
                pInfo.Api_indi_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "idcard", strValue);
                pInfo.Api_idcard = strValue.ToString();
                GzsiAPI.getbyname(pInt, "birthday", strValue);
                pInfo.Api_birthday = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_id", strValue);
                pInfo.Api_corp_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_name", strValue);
                pInfo.Api_corp_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "last_balance", strValue);
                pInfo.Api_last_balance = strValue.ToString();

                GzsiAPI.getbyname(pInt, "name", strValue);
                pInfo.Api_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "office_grade", strValue);
                pInfo.Api_office_grade = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                pInfo.Api_pers_identity = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_status", strValue);
                pInfo.Api_pers_status = strValue.ToString();
                GzsiAPI.getbyname(pInt, "post_code", strValue);
                pInfo.Api_post_code = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sex", strValue);
                pInfo.Api_sex = strValue.ToString();
                GzsiAPI.getbyname(pInt, "telephone", strValue);
                pInfo.Api_telephone = strValue.ToString();

                //个人帐户冻结信息
                if (-1 == GzsiAPI.setresultset(pInt, "freezeinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "fund_id", strValue);
                pInfo.Api_fund_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "fund_name", strValue);
                pInfo.Api_fund_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "indi_freeze_status", strValue);
                pInfo.Api_indi_freeze_status = strValue.ToString();

                //门诊选点信息
                if (GzsiAPI.setresultset(pInt, "clinicapplyinfo") >= 0)
                {
                    GzsiAPI.getbyname(pInt, "serial_apply", strValue);
                    pInfo.Api_serial_apply = strValue.ToString();
                }
                if (string.IsNullOrEmpty(pInfo.Api_serial_apply))
                {
                    pInfo.Api_serial_apply = "0";
                }
                pInfo.Api_treatment_type = "110";
                alPersonInfo.Add(pInfo);
            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        /// 获取公医诊个人信息
        /// </summary>
        /// <param name="idcard">查询信息</param>
        /// <param name="biz_type">业务类型：11：普通门诊   61：公医门诊 </param>
        /// <param name="flag">1:身份证 2：个人电脑号 3：姓名 4:医疗证号</param>
        /// <returns></returns>
        public ArrayList GetGySiPersonInfo(string idcard, string biz_type, DateTime biz_date)
        {
            NoSortHashtable ht = new NoSortHashtable();
            int flag = 1;
            string[] serch = idcard.Split('|');
            if (serch.Length > 1)
            {
                flag = int.Parse(serch[0]);
                idcard = serch[1];
            }
            if (flag == 1)
            {
                ht.Add("idcard", idcard);
            }
            else if (flag == 2)
            {
                //indi_id
                ht.Add("indi_id", idcard);
            }
            else if (flag == 3)
            {
                ht.Add("name", idcard);
            }
            else if (flag == 4)
            {
                ht.Add("insr_code", idcard);
            }
            ht.Add("hospital_id", hospital_id);
            ht.Add("biz_type", biz_type);
            ht.Add("biz_date", biz_date.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("treatment_type", "610");



            if (-1 == CallSIProcessBegin("BIZH131001")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString();
                return null;
            }
            //获取结果
            result = GzsiAPI.setresultset(pInt, "PersonInfo");
            if (-1 == result)
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);

            if (l_size > 1)
            {
                //多条记录
                try
                {
                    do
                    {
                        PersonInfo pInfo = new PersonInfo();
                        GzsiAPI.getbyname(pInt, "indi_id", strValue);
                        pInfo.Api_indi_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "idcard", strValue);
                        pInfo.Api_idcard = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "birthday", strValue);
                        pInfo.Api_birthday = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_id", strValue);
                        pInfo.Api_corp_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_name", strValue);
                        pInfo.Api_corp_name = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "name", strValue);
                        pInfo.Api_name = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "office_grade", strValue);
                        pInfo.Api_office_grade = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                        pInfo.Api_pers_identity = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "pers_status", strValue);
                        pInfo.Api_pers_status = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "post_code", strValue);
                        pInfo.Api_post_code = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "sex", strValue);
                        pInfo.Api_sex = strValue.ToString(); ;
                        GzsiAPI.getbyname(pInt, "telephone", strValue);
                        pInfo.Api_telephone = strValue.ToString(); ;
                        pInfo.Api_treatment_type = "610";
                        alPersonInfo.Add(pInfo);


                    } while (GzsiAPI.nextrow(pInt) > 0);
                }
                catch (Exception ex)
                {
                    this.errMsg = "错误：获取多行个人信息失败！" + ex.Message;
                    return null;
                }


            }
            else if (l_size == 1)
            {

                //一行数据
                PersonInfo pInfo = new PersonInfo();
                GzsiAPI.getbyname(pInt, "indi_id", strValue);
                pInfo.Api_indi_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "idcard", strValue);
                pInfo.Api_idcard = strValue.ToString();
                GzsiAPI.getbyname(pInt, "birthday", strValue);
                pInfo.Api_birthday = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_id", strValue);
                pInfo.Api_corp_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_name", strValue);
                pInfo.Api_corp_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "last_balance", strValue);
                pInfo.Api_last_balance = strValue.ToString();

                GzsiAPI.getbyname(pInt, "name", strValue);
                pInfo.Api_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "office_grade", strValue);
                pInfo.Api_office_grade = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                pInfo.Api_pers_identity = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_status", strValue);
                pInfo.Api_pers_status = strValue.ToString();
                GzsiAPI.getbyname(pInt, "post_code", strValue);
                pInfo.Api_post_code = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sex", strValue);
                pInfo.Api_sex = strValue.ToString();
                GzsiAPI.getbyname(pInt, "telephone", strValue);
                pInfo.Api_telephone = strValue.ToString();

                //个人帐户冻结信息
                if (-1 == GzsiAPI.setresultset(pInt, "freezeinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "fund_id", strValue);
                pInfo.Api_fund_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "fund_name", strValue);
                pInfo.Api_fund_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "indi_freeze_status", strValue);
                pInfo.Api_indi_freeze_status = strValue.ToString();

                //门诊选点信息
                if (GzsiAPI.setresultset(pInt, "clinicapplyinfo") >= 0)
                {
                    GzsiAPI.getbyname(pInt, "serial_apply", strValue);
                    pInfo.Api_serial_apply = strValue.ToString();
                }
                if (string.IsNullOrEmpty(pInfo.Api_serial_apply))
                {
                    pInfo.Api_serial_apply = "0";
                }

                pInfo.Api_treatment_type = "610";
                alPersonInfo.Add(pInfo);
            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        /// 获取门慢门诊个人信息
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="biz_type"></param>
        /// <returns></returns>
        public ArrayList GetMmSiPersonInfo(string idcard, string biz_type, DateTime biz_date)
        {
            NoSortHashtable ht = new NoSortHashtable();
            int flag = 1;
            string[] serch = idcard.Split('|');
            if (serch.Length > 1)
            {
                flag = int.Parse(serch[0]);
                idcard = serch[1];
            }
            if (flag == 1)
            {
                ht.Add("idcard", idcard);
            }
            else if (flag == 2)
            {
                //indi_id
                ht.Add("indi_id", idcard);
            }
            else if (flag == 3)
            {
                ht.Add("name", idcard);
            }
            else if (flag == 4)
            {
                ht.Add("Insr_code", idcard);
            }
            ht.Add("biz_type", biz_type);
            ht.Add("hospital_id", hospital_id);
            //  ht.Add("treatment_type", treatment_type);
            ht.Add("reg_date", biz_date.ToString("yyyy-MM-dd HH:mm:ss"));//业务登记日期


            if (-1 == CallSIProcessBegin("BIZH131211")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString();
                return null;
            }
            //获取结果
            if (-1 == GzsiAPI.setresultset(pInt, "PersonInfo"))
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);

            if (l_size > 1)
            {
                //多条记录
                try
                {
                    do
                    {
                        PersonInfo pInfo = new PersonInfo();

                        GzsiAPI.getbyname(pInt, "indi_id", strValue);
                        pInfo.Api_indi_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "idcard", strValue);
                        pInfo.Api_idcard = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "birthday", strValue);
                        pInfo.Api_birthday = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_id", strValue);
                        pInfo.Api_corp_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_name", strValue);
                        pInfo.Api_corp_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "name", strValue);
                        pInfo.Api_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "office_grade", strValue);
                        pInfo.Api_office_grade = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                        pInfo.Api_pers_identity = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_status", strValue);
                        pInfo.Api_pers_status = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "post_code", strValue);
                        pInfo.Api_post_code = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "sex", strValue);
                        pInfo.Api_sex = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "telephone", strValue);
                        pInfo.Api_telephone = strValue.ToString();

                        alPersonInfo.Add(pInfo);


                    } while (GzsiAPI.nextrow(pInt) > 0);
                }
                catch (Exception ex)
                {
                    this.errMsg = "错误：获取多行个人信息失败！" + ex.Message;
                    return null;
                }


            }
            else if (l_size == 1)
            {
                //一行数据

                //门慢申请信息
                if (-1 == GzsiAPI.setresultset(pInt, "spinfo"))
                    return null;
                ArrayList alTreamType = new ArrayList();
                string treatment_type = "";
                do
                {
                    FS.FrameWork.Models.NeuObject neuTreamObj = new NeuObject();
                    GzsiAPI.getbyname(pInt, "treatment_type", strValue);
                    neuTreamObj.ID = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "treatment_name", strValue);
                    neuTreamObj.Name = strValue.ToString();

                    alTreamType.Add(neuTreamObj);

                } while (GzsiAPI.nextrow(pInt) > 0);

                if (alTreamType.Count == 1)
                {
                    treatment_type = ((NeuObject)alTreamType[0]).ID;

                }
                else
                {
                    ucGetTreamType uctreamType = new ucGetTreamType();
                    uctreamType.AlTreamType = alTreamType;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uctreamType);
                    if (uctreamType.TreamTypeObj != null && !string.IsNullOrEmpty(uctreamType.TreamTypeObj.ID))
                    {
                        treatment_type = uctreamType.TreamTypeObj.ID;
                    }
                    else
                    {
                        this.errMsg = "没有选择医保API的待遇类型，谢谢!";
                        return null;
                    }

                }
                //{5C911E87-3E6F-49cc-965D-1389E5BECF4F} zhang-wx 2016-04-26 患者有两个门慢信息时，要重新取数据
                if (flag == 2)
                {
                    idcard = "2|" + idcard;
                }
                alPersonInfo = this.GetMmSiPersonInfo(idcard, biz_type, treatment_type);




            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        /// 获取门慢门诊个人信息
        /// </summary>
        /// <param name="idcard">公民身份证号</param>
        /// <param name="treatment_type">待遇类型：13开头：门慢  </param>
        ///  <param name="biz_date">业务登记日期(格式yyyy-mm-dd hh:mm:ss)</param>
        /// <returns></returns>
        public ArrayList GetMmSiPersonInfo(string idcard, string biz_type, string treatment_type)
        {
            NoSortHashtable ht = new NoSortHashtable();
            int flag = 1;
            string[] serch = idcard.Split('|');
            if (serch.Length > 1)
            {
                flag = int.Parse(serch[0]);
                idcard = serch[1];
            }
            if (flag == 1)
            {
                ht.Add("idcard", idcard);
            }
            else if (flag == 2)
            {
                //indi_id
                ht.Add("indi_id", idcard);
            }
            else if (flag == 3)
            {
                ht.Add("name", idcard);
            }
            else if (flag == 4)
            {
                ht.Add("Insr_code", idcard);
            }
            ht.Add("biz_type", biz_type);
            ht.Add("hospital_id", hospital_id);
            ht.Add("treatment_type", treatment_type);
            ht.Add("reg_date", this.OperDate);//业务登记日期


            if (-1 == CallSIProcess("BIZH131211")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString();
                return null;
            }
            //获取结果
            if (-1 == GzsiAPI.setresultset(pInt, "PersonInfo"))
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);

            if (l_size > 1)
            {
                //多条记录
                try
                {
                    do
                    {
                        PersonInfo pInfo = new PersonInfo();

                        GzsiAPI.getbyname(pInt, "indi_id", strValue);
                        pInfo.Api_indi_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "idcard", strValue);
                        pInfo.Api_idcard = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "birthday", strValue);
                        pInfo.Api_birthday = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_id", strValue);
                        pInfo.Api_corp_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_name", strValue);
                        pInfo.Api_corp_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "name", strValue);
                        pInfo.Api_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "office_grade", strValue);
                        pInfo.Api_office_grade = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                        pInfo.Api_pers_identity = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_status", strValue);
                        pInfo.Api_pers_status = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "post_code", strValue);
                        pInfo.Api_post_code = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "sex", strValue);
                        pInfo.Api_sex = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "telephone", strValue);
                        pInfo.Api_telephone = strValue.ToString();

                        pInfo.Api_treatment_type = treatment_type;
                        alPersonInfo.Add(pInfo);




                    } while (GzsiAPI.nextrow(pInt) > 0);
                }
                catch (Exception ex)
                {
                    this.errMsg = "错误：获取多行个人信息失败！" + ex.Message;
                    return null;
                }

                string Api_indi_id = "";
                ucPersonInfo ucPerson = new ucPersonInfo();
                ucPerson.AlPersonInfo = alPersonInfo;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPerson);
                if (ucPerson.Person != null && !string.IsNullOrEmpty(ucPerson.Person.Api_indi_id))
                {
                    Api_indi_id = ucPerson.Person.Api_indi_id;

                }
                else
                {
                    this.errMsg = "没有选择人员信息，谢谢!";
                    return null;
                }
                Api_indi_id = "2|" + Api_indi_id;
                return GetMmSiPersonInfo(Api_indi_id, biz_type, treatment_type);

            }
            else if (l_size == 1)
            {
                //一行数据
                PersonInfo pInfo = new PersonInfo();
                GzsiAPI.getbyname(pInt, "indi_id", strValue);
                pInfo.Api_indi_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "idcard", strValue);
                pInfo.Api_idcard = strValue.ToString();
                GzsiAPI.getbyname(pInt, "birthday", strValue);
                pInfo.Api_birthday = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_id", strValue);
                pInfo.Api_corp_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_name", strValue);
                pInfo.Api_corp_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "name", strValue);
                pInfo.Api_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "office_grade", strValue);
                pInfo.Api_office_grade = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                pInfo.Api_pers_identity = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_status", strValue);
                pInfo.Api_pers_status = strValue.ToString();
                GzsiAPI.getbyname(pInt, "post_code", strValue);
                pInfo.Api_post_code = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sex", strValue);
                pInfo.Api_sex = strValue.ToString();
                GzsiAPI.getbyname(pInt, "telephone", strValue);
                pInfo.Api_telephone = strValue.ToString();

                //个人帐户冻结信息
                if (-1 == GzsiAPI.setresultset(pInt, "freezeinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "fund_id", strValue);
                pInfo.Api_fund_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "fund_name", strValue);
                pInfo.Api_fund_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "indi_freeze_status", strValue);
                pInfo.Api_indi_freeze_status = strValue.ToString();

                //门慢申请信息
                if (-1 == GzsiAPI.setresultset(pInt, "spinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "serial_apply", strValue);
                pInfo.Api_serial_apply = strValue.ToString();
                GzsiAPI.getbyname(pInt, "treatment_type", strValue);
                pInfo.Api_treatment_type = strValue.ToString();
                GzsiAPI.getbyname(pInt, "treatment_name", strValue);
                pInfo.Api_treatment_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "biz_type", strValue);
                pInfo.Api_biz_type = strValue.ToString();
                GzsiAPI.getbyname(pInt, "icd", strValue);
                pInfo.Api_icd = strValue.ToString();
                GzsiAPI.getbyname(pInt, "disease", strValue);
                pInfo.Api_disease = strValue.ToString();
                GzsiAPI.getbyname(pInt, "admit_effect", strValue);
                pInfo.Api_admit_effect = strValue.ToString();
                GzsiAPI.getbyname(pInt, "admit_date", strValue);
                pInfo.Api_admit_date = strValue.ToString();

                pInfo.Api_treatment_type = treatment_type;
                alPersonInfo.Add(pInfo);
            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        /// 获取生育门诊个人信息
        /// </summary>
        /// <param name="idcard">公民身份证号</param>
        /// <param name="treatment_type">待遇类型：511：生育门诊  </param>
        ///  <param name="biz_date">业务登记日期(格式yyyy-mm-dd hh:mm:ss)</param>
        /// <returns></returns>
        public ArrayList GetSySiPersonInfo(string idcard, string biz_type, DateTime biz_date)
        {
            string treatment_type = "";
            ucGetTreamType uctreamType = new ucGetTreamType();
            ArrayList alTreamType = localMgr.GetApiTreatmenttype(biz_type);
            uctreamType.AlTreamType = alTreamType;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uctreamType);
            if (uctreamType.TreamTypeObj != null && !string.IsNullOrEmpty(uctreamType.TreamTypeObj.ID))
            {
                treatment_type = uctreamType.TreamTypeObj.ID;
            }
            else
            {
                this.errMsg = "没有选择医保API的待遇类型，谢谢!";
                return null;
            }

            NoSortHashtable ht = new NoSortHashtable();
            int flag = 1;
            string[] serch = idcard.Split('|');
            if (serch.Length > 1)
            {
                flag = int.Parse(serch[0]);
                idcard = serch[1];
            }
            if (flag == 1)
            {
                ht.Add("idcard", idcard);
            }
            else if (flag == 2)
            {
                //indi_id
                ht.Add("indi_id", idcard);
            }
            else if (flag == 3)
            {
                ht.Add("name", idcard);
            }
            else if (flag == 4)
            {
                ht.Add("Insr_code", idcard);
            }
            ht.Add("hospital_id", hospital_id);
            ht.Add("biz_type", biz_type);
            ht.Add("treatment_type", treatment_type);
            ht.Add("biz_date", biz_date.ToString("yyyy-MM-dd HH:mm:ss"));//业务登记日期

            if (-1 == CallSIProcessBegin("BIZH131001")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                return null;
            }
            //获取结果
            if (-1 == GzsiAPI.setresultset(pInt, "PersonInfo"))
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);

            if (l_size > 1)
            {
                //多条记录
                try
                {
                    do
                    {
                        PersonInfo pInfo = new PersonInfo();
                        GzsiAPI.getbyname(pInt, "indi_id", strValue);
                        pInfo.Api_indi_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "idcard", strValue);
                        pInfo.Api_idcard = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "birthday", strValue);
                        pInfo.Api_birthday = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_id", strValue);
                        pInfo.Api_corp_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_name", strValue);
                        pInfo.Api_corp_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "name", strValue);
                        pInfo.Api_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "office_grade", strValue);
                        pInfo.Api_office_grade = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                        pInfo.Api_pers_identity = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_status", strValue);
                        pInfo.Api_pers_status = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "post_code", strValue);
                        pInfo.Api_post_code = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "sex", strValue);
                        pInfo.Api_sex = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "telephone", strValue);
                        pInfo.Api_telephone = strValue.ToString();

                        pInfo.Api_treatment_type = treatment_type;
                        alPersonInfo.Add(pInfo);

                    } while (GzsiAPI.nextrow(pInt) > 0);
                }
                catch (Exception ex)
                {
                    this.errMsg = "错误：获取多行个人信息失败！" + ex.Message;
                    return null;
                }

                string Api_indi_id = "";
                ucPersonInfo ucPerson = new ucPersonInfo();
                ucPerson.AlPersonInfo = alPersonInfo;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPerson);
                if (ucPerson.Person != null && !string.IsNullOrEmpty(ucPerson.Person.Api_indi_id))
                {
                    Api_indi_id = ucPerson.Person.Api_indi_id;

                }
                else
                {
                    this.errMsg = "没有选择人员信息，谢谢!";
                    return null;
                }
                return GetSySiPersonInfo(Api_indi_id, biz_type, treatment_type);

            }
            else if (l_size == 1)
            {

                //一行数据
                PersonInfo pInfo = new PersonInfo();
                GzsiAPI.getbyname(pInt, "indi_id", strValue);
                pInfo.Api_indi_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "idcard", strValue);
                pInfo.Api_idcard = strValue.ToString();
                GzsiAPI.getbyname(pInt, "birthday", strValue);
                pInfo.Api_birthday = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_id", strValue);
                pInfo.Api_corp_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_name", strValue);
                pInfo.Api_corp_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "name", strValue);
                pInfo.Api_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "office_grade", strValue);
                pInfo.Api_office_grade = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                pInfo.Api_pers_identity = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_status", strValue);
                pInfo.Api_pers_status = strValue.ToString();
                GzsiAPI.getbyname(pInt, "post_code", strValue);
                pInfo.Api_post_code = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sex", strValue);
                pInfo.Api_sex = strValue.ToString();
                GzsiAPI.getbyname(pInt, "telephone", strValue);
                pInfo.Api_telephone = strValue.ToString();

                //个人帐户冻结信息
                if (-1 == GzsiAPI.setresultset(pInt, "freezeinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "fund_id", strValue);
                pInfo.Api_fund_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "fund_name", strValue);
                pInfo.Api_fund_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "indi_freeze_status", strValue);
                pInfo.Api_indi_freeze_status = strValue.ToString();

                //生育认定信息
                if (-1 == GzsiAPI.setresultset(pInt, "injuryorbirthinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "serial_mn", strValue);
                pInfo.Api_serial_mn = strValue.ToString();


                pInfo.Api_treatment_type = treatment_type;
                alPersonInfo.Add(pInfo);
            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        /// 获取生育门诊个人信息
        /// </summary>
        /// <param name="indi_id">个人电脑号</param>
        /// <param name="biz_type">业务类型：51：生育门诊</param>
        /// <param name="treatment_type">待遇类型：511：生育门诊</param>
        /// <returns></returns>
        public ArrayList GetSySiPersonInfo(string indi_id, string biz_type, string treatment_type)
        {
            NoSortHashtable ht = new NoSortHashtable();

            ht.Add("indi_id", indi_id);
            ht.Add("hospital_id", hospital_id);
            ht.Add("biz_type", biz_type);
            ht.Add("treatment_type", treatment_type);
            ht.Add("biz_date", this.OperDate);//业务登记日期

            if (-1 == CallSIProcess("BIZH131001")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                return null;
            }
            //获取结果
            if (-1 == GzsiAPI.setresultset(pInt, "PersonInfo"))
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);
            if (l_size > 0)
            {
                //一行数据
                PersonInfo pInfo = new PersonInfo();
                GzsiAPI.getbyname(pInt, "indi_id", strValue);
                pInfo.Api_indi_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "idcard", strValue);
                pInfo.Api_idcard = strValue.ToString();
                GzsiAPI.getbyname(pInt, "birthday", strValue);
                pInfo.Api_birthday = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_id", strValue);
                pInfo.Api_corp_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_name", strValue);
                pInfo.Api_corp_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "name", strValue);
                pInfo.Api_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "office_grade", strValue);
                pInfo.Api_office_grade = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                pInfo.Api_pers_identity = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_status", strValue);
                pInfo.Api_pers_status = strValue.ToString();
                GzsiAPI.getbyname(pInt, "post_code", strValue);
                pInfo.Api_post_code = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sex", strValue);
                pInfo.Api_sex = strValue.ToString();
                GzsiAPI.getbyname(pInt, "telephone", strValue);
                pInfo.Api_telephone = strValue.ToString();

                //个人帐户冻结信息
                if (-1 == GzsiAPI.setresultset(pInt, "freezeinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "fund_id", strValue);
                pInfo.Api_fund_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "fund_name", strValue);
                pInfo.Api_fund_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "indi_freeze_status", strValue);
                pInfo.Api_indi_freeze_status = strValue.ToString();

                //生育认定信息
                if (-1 == GzsiAPI.setresultset(pInt, "injuryorbirthinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "serial_mn", strValue);
                pInfo.Api_serial_mn = strValue.ToString();

                pInfo.Api_treatment_type = treatment_type;
                alPersonInfo.Add(pInfo);
            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        /// 获取工伤门诊个人信息
        /// </summary>
        /// <param name="idcard">公民身份证号</param>
        /// <param name="biz_type">业务类型：41：工伤门诊  </param>
        /// <returns></returns>
        public ArrayList GetGsSiPersonInfo(string idcard, string biz_type, DateTime biz_date)
        {

            //string treatment_type = "";
            //ucGetTreamType uctreamType = new ucGetTreamType();
            //ArrayList alTreamType = localMgr.GetApiTreatmenttype(biz_type);
            //uctreamType.AlTreamType = alTreamType;
            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(uctreamType);
            //if (uctreamType.TreamTypeObj != null && !string.IsNullOrEmpty(uctreamType.TreamTypeObj.ID))
            //{
            //    treatment_type = uctreamType.TreamTypeObj.ID;
            //}
            //else
            //{
            //    this.errMsg = "没有选择医保API的待遇类型，谢谢!";
            //    return null;
            //}

            NoSortHashtable ht = new NoSortHashtable();
            string flag = "1";
            string[] serch = idcard.Split('|');
            if (serch.Length > 1)
            {
                flag = serch[0];
                idcard = serch[1];
            }
            if (flag == "1")
            {
                ht.Add("idcard", idcard);
            }
            else if (flag == "2")
            {
                //indi_id
                ht.Add("indi_id", idcard);
            }
            else if (flag == "3")
            {
                ht.Add("name", idcard);
            }
            else if (flag == "4")
            {
                ht.Add("Insr_code", idcard);
            }

            ht.Add("hospital_id", hospital_id);
            ht.Add("biz_type", biz_type);
            ht.Add("biz_date", biz_date.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("treatment_type", "410");

            if (-1 == CallSIProcessBegin("BIZH131001")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString();
                return null;
            }
            //获取结果
            result = GzsiAPI.setresultset(pInt, "PersonInfo");
            if (-1 == result)
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);

            if (l_size > 1)
            {
                //多条记录
                try
                {
                    do
                    {
                        PersonInfo pInfo = new PersonInfo();
                        GzsiAPI.getbyname(pInt, "indi_id", strValue);
                        pInfo.Api_indi_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "idcard", strValue);
                        pInfo.Api_idcard = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "birthday", strValue);
                        pInfo.Api_birthday = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_id", strValue);
                        pInfo.Api_corp_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "corp_name", strValue);
                        pInfo.Api_corp_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "name", strValue);
                        pInfo.Api_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "office_grade", strValue);
                        pInfo.Api_office_grade = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                        pInfo.Api_pers_identity = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "pers_status", strValue);
                        pInfo.Api_pers_status = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "post_code", strValue);
                        pInfo.Api_post_code = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "sex", strValue);
                        pInfo.Api_sex = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "telephone", strValue);
                        pInfo.Api_telephone = strValue.ToString();

                        pInfo.Api_treatment_type = "410";
                        alPersonInfo.Add(pInfo);


                    } while (GzsiAPI.nextrow(pInt) > 0);
                }
                catch (Exception ex)
                {
                    this.errMsg = "错误：获取多行个人信息失败！" + ex.Message;
                    return null;
                }
                //xhl 2017-04-28  
                //string Api_indi_id = "";
                //ucPersonInfo ucPerson = new ucPersonInfo();
                //ucPerson.AlPersonInfo = alPersonInfo;
                //FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPerson);
                //if (ucPerson.Person != null && !string.IsNullOrEmpty(ucPerson.Person.Api_indi_id))
                //{
                //    Api_indi_id = ucPerson.Person.Api_indi_id;

                //}
                //else
                //{
                //    this.errMsg = "没有选择人员信息，谢谢!";
                //    return null;
                //}
                //return GetGsSiPersonInfo(Api_indi_id, biz_type, treatment_type);


            }
            else if (l_size == 1)
            {
                //一行数据
                PersonInfo pInfo = new PersonInfo();
                GzsiAPI.getbyname(pInt, "indi_id", strValue);
                pInfo.Api_indi_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "idcard", strValue);
                pInfo.Api_idcard = strValue.ToString();
                GzsiAPI.getbyname(pInt, "birthday", strValue);
                pInfo.Api_birthday = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_id", strValue);
                pInfo.Api_corp_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_name", strValue);
                pInfo.Api_corp_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "last_balance", strValue);
                pInfo.Api_last_balance = strValue.ToString();

                GzsiAPI.getbyname(pInt, "name", strValue);
                pInfo.Api_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "office_grade", strValue);
                pInfo.Api_office_grade = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                pInfo.Api_pers_identity = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_status", strValue);
                pInfo.Api_pers_status = strValue.ToString();
                GzsiAPI.getbyname(pInt, "post_code", strValue);
                pInfo.Api_post_code = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sex", strValue);
                pInfo.Api_sex = strValue.ToString();
                GzsiAPI.getbyname(pInt, "telephone", strValue);
                pInfo.Api_telephone = strValue.ToString();
                GzsiAPI.getbyname(pInt, "last_balance", strValue);
                pInfo.Api_last_balance = strValue.ToString();

                //个人帐户冻结信息
                if (-1 == GzsiAPI.setresultset(pInt, "freezeinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "fund_id", strValue);
                pInfo.Api_fund_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "fund_name", strValue);
                pInfo.Api_fund_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "indi_freeze_status", strValue);
                pInfo.Api_indi_freeze_status = strValue.ToString();

                //工伤认定信息
                if (-1 == GzsiAPI.setresultset(pInt, "injuryorbirthinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "serial_wi", strValue);
                pInfo.Api_serial_wi = strValue.ToString();
                GzsiAPI.getbyname(pInt, "begin_date", strValue);
                pInfo.Api_begin_date = strValue.ToString();
                GzsiAPI.getbyname(pInt, "end_date", strValue);
                pInfo.Api_end_date = strValue.ToString();

                pInfo.Api_treatment_type = "410";
                alPersonInfo.Add(pInfo);
            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        ///  获取工伤门诊个人信息
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="biz_type"></param>
        /// <param name="treatment_type"></param>
        /// <returns></returns>
        public ArrayList GetGsSiPersonInfo(string idcard, string biz_type, string treatment_type)
        {

            NoSortHashtable ht = new NoSortHashtable();
            int flag = 1;
            string[] serch = idcard.Split('|');
            if (serch.Length > 1)
            {
                flag = int.Parse(serch[0]);
                idcard = serch[1];
            }
            if (flag == 1)
            {
                ht.Add("idcard", idcard);
            }
            else if (flag == 2)
            {
                //indi_id
                ht.Add("indi_id", idcard);
            }
            else if (flag == 3)
            {
                ht.Add("name", idcard);
            }
            else if (flag == 4)
            {
                ht.Add("Insr_code", idcard);
            }
            ht.Add("idcard", idcard);
            ht.Add("hospital_id", hospital_id);
            ht.Add("biz_type", biz_type);
            ht.Add("treatment_type", treatment_type);
            ht.Add("biz_date", this.OperDate);//业务登记日期


            if (-1 == CallSIProcess("BIZH131001")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                return null;
            }
            //获取结果
            if (-1 == GzsiAPI.setresultset(pInt, "PersonInfo"))
                return null;

            ArrayList alPersonInfo = new ArrayList();
            long l_size = GzsiAPI.getrowcount(pInt);

            if (l_size > 1)
            {
                //多条记录
                //暂不处理
            }
            else if (l_size == 1)
            {
                //一行数据
                PersonInfo pInfo = new PersonInfo();
                GzsiAPI.getbyname(pInt, "indi_id", strValue);
                pInfo.Api_indi_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "idcard", strValue);
                pInfo.Api_idcard = strValue.ToString();
                GzsiAPI.getbyname(pInt, "birthday", strValue);
                pInfo.Api_birthday = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_id", strValue);
                pInfo.Api_corp_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "corp_name", strValue);
                pInfo.Api_corp_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "name", strValue);
                pInfo.Api_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "office_grade", strValue);
                pInfo.Api_office_grade = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_identity", strValue);
                pInfo.Api_pers_identity = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pers_status", strValue);
                pInfo.Api_pers_status = strValue.ToString();
                GzsiAPI.getbyname(pInt, "post_code", strValue);
                pInfo.Api_post_code = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sex", strValue);
                pInfo.Api_sex = strValue.ToString();
                GzsiAPI.getbyname(pInt, "telephone", strValue);
                pInfo.Api_telephone = strValue.ToString();

                //个人帐户冻结信息
                if (-1 == GzsiAPI.setresultset(pInt, "freezeinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "fund_id", strValue);
                pInfo.Api_fund_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "fund_name", strValue);
                pInfo.Api_fund_name = strValue.ToString();
                GzsiAPI.getbyname(pInt, "indi_freeze_status", strValue);
                pInfo.Api_indi_freeze_status = strValue.ToString();

                //工伤认定信息
                if (-1 == GzsiAPI.setresultset(pInt, "injuryorbirthinfo"))
                    return null;
                GzsiAPI.getbyname(pInt, "serial_wi", strValue);
                pInfo.Api_serial_wi = strValue.ToString();
                GzsiAPI.getbyname(pInt, "begin_date", strValue);
                pInfo.Api_begin_date = strValue.ToString();
                GzsiAPI.getbyname(pInt, "end_date", strValue);
                pInfo.Api_end_date = strValue.ToString();

                pInfo.Api_treatment_type = treatment_type;
                alPersonInfo.Add(pInfo);
            }
            else
            {
                errMsg = "错误：获取普通门诊个人信息失败！";
                return null;
            }
            CallSIProcessEnd();
            return alPersonInfo;

        }

        /// <summary>
        ///获取医保病人信息
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="biz_type"></param>
        /// <param name="biz_date"></param>
        /// <returns></returns>
        public ArrayList GetSiPersonInfo(string idcard, string biz_type, DateTime biz_date)
        {
            switch (biz_type)
            {
                case "11":
                    return GetPtSiPersonInfo(idcard, biz_type, biz_date);
                case "61":
                    return GetGySiPersonInfo(idcard, biz_type, biz_date);
                case "13":
                    return GetMmSiPersonInfo(idcard, biz_type, biz_date);
                case "41":
                    return GetGsSiPersonInfo(idcard, biz_type, biz_date); //工伤病人信息xhl 2017-04-29   //{249DC040-7C55-4a7e-BAB8-6FF56FF985E5}
                case "51":
                    return GetSySiPersonInfo(idcard, biz_type, biz_date);

                default:
                    return null;
            }

        }

        #endregion // end 获取个人信息

        #region 门诊挂号登记

        /// <summary>
        /// 门诊挂号登记
        /// </summary>
        /// <param name="treatment_type">待遇类型 [110：普通门诊][...] 等等 </param>
        /// <param name="pInfo"></param>
        /// <param name="regObj"></param>
        /// <returns></returns>
        public int SiRegister(string treatment_type, PersonInfo pInfo, ref FS.HISFC.Models.Registration.Register regObj)
        {
            string biz_type = treatment_type.Substring(0, 2);
            NoSortHashtable ht = new NoSortHashtable();

            switch (biz_type)
            {
                case "11": //普通门诊
                    #region
                    ht.Add("save_flag", "1");
                    ht.Add("oper_flag", "1");
                    ht.Add("center_id", center_id);
                    ht.Add("hospital_id", hospital_id);
                    ht.Add("indi_id", pInfo.Api_indi_id);
                    ht.Add("patient_flag", "1");
                    ht.Add("diagnose_flag", "1");
                    ht.Add("reg_flag", "1");
                    ht.Add("treatment_type", treatment_type);//需赋值

                    ht.Add("doctor_name", "");
                    ht.Add("doctor_no", "");
                    ht.Add("diagnose_type", "");
                    ht.Add("fin_info", "0");
                    ht.Add("sex", pInfo.Api_sex == "男" ? "1" : "2");
                    ht.Add("diagnose", regObj.ClinicDiagnose);

                    ht.Add("ic_flag", "0");
                    ht.Add("fee_batch", "");
                    ht.Add("diagnose_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));//需赋值
                    ht.Add("reg_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    ht.Add("reg_staff", string.IsNullOrEmpty(regObj.InputOper.ID) ? "5188" : regObj.InputOper.ID);//需赋值
                    ht.Add("serial_apply", "");//pInfo.Api_serial_apply);
                    ht.Add("reg_man", string.IsNullOrEmpty(regObj.InputOper.Name) ? "收费员" : regObj.InputOper.Name);//需赋值
                    ht.Add("in_dept", "011700");//regObj.DoctorInfo.Templet.Dept.ID);
                    ht.Add("in_dept_name", "内科");//regObj.DoctorInfo.Templet.Dept.Name);
                    ht.Add("in_area", "011700");//"011700");
                    ht.Add("in_area_name", "内科");//"内科");
                    ht.Add("in_disease", regObj.ClinicDiagnose);//疾病编码，待定
                    ht.Add("patient_id", "1");//挂号，需赋值
                    ht.Add("recipe_no", "1");//string.IsNullOrEmpty(regObj.RecipeNO)?"1":regObj.RecipeNO);
                    ht.Add("last_balance", string.IsNullOrEmpty(pInfo.Api_last_balance) ? "0" : pInfo.Api_last_balance);
                    ht.Add("note", "");
                    ht.Add("hos_biz_serial", regObj.ID);//挂号流水号clinic_no

                    #endregion
                    break;
                case "13"://门慢
                    #region
                    ht.Add("save_flag", "1");
                    ht.Add("oper_flag", "1");
                    ht.Add("center_id", center_id);
                    ht.Add("hospital_id", hospital_id);
                    ht.Add("reg_staff", /*regObj.InputOper.ID*/"5188");//需赋值
                    ht.Add("reg_man", /*string.IsNullOrEmpty(regObj.InputOper.Name) ? "收费员" : regObj.InputOper.Name*/"杨莹");//需赋值
                    ht.Add("indi_id", pInfo.Api_indi_id);
                    ht.Add("biz_type", biz_type);
                    ht.Add("treatment_type", treatment_type);//需赋值
                    ht.Add("reg_flag", "1");
                    ht.Add("reg_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    ht.Add("diagnose_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));//需赋值
                    ht.Add("note", "");
                    ht.Add("in_dept", "011700" /*regObj.DoctorInfo.Templet.Dept.ID */);
                    ht.Add("in_dept_name", "内科一区" /*regObj.DoctorInfo.Templet.Dept.Name*/);
                    ht.Add("in_area", "011700");
                    ht.Add("in_area_name", "内科");
                    ht.Add("ic_flag", "0");
                    ht.Add("fee_batch", "");
                    ht.Add("serial_apply", pInfo.Api_serial_apply);
                    ht.Add("doctor_name", "");
                    ht.Add("doctor_no", "");
                    ht.Add("recipe_no", /*regObj.RecipeNO*/"1");
                    ht.Add("diagnose_type", "");
                    ht.Add("sex", pInfo.Api_sex == "男" ? "1" : "2");
                    ht.Add("diagnose", pInfo.Api_icd);
                    ht.Add("in_disease", pInfo.Api_icd);
                    ht.Add("patient_id", "1");//挂号，需赋值
                    ht.Add("last_balance", string.IsNullOrEmpty(pInfo.Api_last_balance) ? "0" : pInfo.Api_last_balance);

                    #endregion
                    break;
                case "41": //工伤门诊
                    break;
                case "51": //生育门诊
                    #region
                    ht.Add("save_flag", "1");
                    ht.Add("oper_flag", "1");
                    ht.Add("center_id", center_id);
                    ht.Add("hospital_id", hospital_id);

                    ht.Add("serial_apply", pInfo.Api_serial_apply);
                    ht.Add("patient_flag", "0");
                    ht.Add("reg_staff", regObj.InputOper.ID);//需赋值
                    ht.Add("reg_man", string.IsNullOrEmpty(regObj.InputOper.Name) ? "收费员" : regObj.InputOper.Name);//需赋值
                    ht.Add("indi_id", pInfo.Api_indi_id);
                    ht.Add("biz_type", biz_type);
                    ht.Add("treatment_type", treatment_type);//需赋值
                    ht.Add("reg_flag", "1");
                    ht.Add("reg_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    ht.Add("diagnose_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));//需赋值

                    ht.Add("note", "");
                    ht.Add("in_dept", regObj.DoctorInfo.Templet.Dept.ID);
                    ht.Add("in_dept_name", regObj.DoctorInfo.Templet.Dept.Name);
                    ht.Add("in_area", "1");
                    ht.Add("in_area_name", "住院病区");
                    ht.Add("ic_flag", "0");
                    ht.Add("fee_batch", "");

                    ht.Add("doctor_name", "");
                    ht.Add("doctor_no", "");
                    ht.Add("recipe_no", /*regObj.RecipeNO*/"1");
                    ht.Add("diagnose_type", "");
                    ht.Add("fin_info", "0");
                    ht.Add("injuryorbirth_serial", pInfo.Api_serial_wi);

                    ht.Add("sex", pInfo.Api_sex == "男" ? "1" : "2");
                    ht.Add("diagnose", pInfo.Api_icd);
                    ht.Add("in_disease", pInfo.Api_icd);
                    ht.Add("patient_id", "1");//挂号，需赋值
                    ht.Add("last_balance", string.IsNullOrEmpty(pInfo.Api_last_balance) ? "0" : pInfo.Api_last_balance);
                    #endregion
                    break;
                case "61": //公医门诊
                    #region
                    ht.Add("save_flag", "1");
                    ht.Add("oper_flag", "1");
                    ht.Add("center_id", center_id);
                    ht.Add("hospital_id", hospital_id);
                    ht.Add("serial_apply", pInfo.Api_serial_apply);

                    ht.Add("patient_flag", "0");
                    ht.Add("diagnose_flag", "0");
                    ht.Add("reg_staff", regObj.InputOper.ID);//需赋值
                    ht.Add("reg_man", string.IsNullOrEmpty(regObj.InputOper.Name) ? "收费员" : regObj.InputOper.Name);//需赋值
                    ht.Add("indi_id", pInfo.Api_indi_id);
                    ht.Add("biz_type", biz_type);
                    ht.Add("treatment_type", treatment_type);//需赋值
                    ht.Add("reg_flag", "1");
                    ht.Add("reg_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    ht.Add("diagnose_date", regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));//需赋值

                    ht.Add("note", "");
                    ht.Add("in_dept", regObj.DoctorInfo.Templet.Dept.ID);
                    ht.Add("in_dept_name", regObj.DoctorInfo.Templet.Dept.Name);
                    ht.Add("in_area", "1");
                    ht.Add("in_area_name", "住院病区");
                    ht.Add("ic_flag", "0");
                    ht.Add("fee_batch", "");
                    ht.Add("serial_apply", pInfo.Api_serial_apply);
                    ht.Add("doctor_name", "");
                    ht.Add("doctor_no", "");

                    ht.Add("recipe_no", /*regObj.RecipeNO*/"1");

                    if (regObj.DoctorInfo.Templet.RegLevel.IsEmergency)
                    {
                        ht.Add("diagnose_type", "3");
                    }
                    else
                    {
                        ht.Add("diagnose_type", "");
                    }
                    ht.Add("injuryorbirth_serial", "");
                    ht.Add("sex", pInfo.Api_sex == "男" ? "1" : "2");
                    ht.Add("diagnose", pInfo.Api_icd);
                    ht.Add("in_disease", pInfo.Api_icd);

                    ht.Add("patient_id", "1");//挂号，需赋值
                    ht.Add("last_balance", string.IsNullOrEmpty(pInfo.Api_last_balance) ? "0" : pInfo.Api_last_balance);

                    #endregion
                    break;
            }

            if (-1 == CallSIProcessBegin("BIZH131104"))
            {
                return -1;
            }
            if (-1 == PutParams(ht))
            {
                return -1;
            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString(); ;
                return -1;
            }
            if (-1 == GzsiAPI.setresultset(pInt, "bizinfo"))
            {
                this.errMsg = "错误：获取门诊登记信息失败！";
                return -1;
            }

            if (GzsiAPI.getbyname(pInt, "serial_no", strValue) < 0)
                return -1;
            regObj.SIMainInfo.RegNo = strValue.ToString();
            regObj.SIMainInfo.ApplyType.ID = treatment_type;//保存待遇类型

            CallSIProcessEnd();
            return 1;
        }

        /// <summary>
        /// 门诊挂号登记并收费
        /// </summary>
        /// <param name="treatment_type">待遇类型 [110：普通门诊][...] 等等 </param>
        /// <param name="pInfo"></param>
        /// <param name="regObj"></param>
        /// <param name="saveFlag">0:试算 1：收费</param>
        /// <returns></returns>
        public int SiRegisterAndClinicFee(string biz_type, PersonInfo pInfo, ref FS.HISFC.Models.Registration.Register regObj, ArrayList alFeeDetails, ref PayInfo payInfo, string saveFlag)
        {
            string treatment_type = "";
            switch (biz_type)
            {
                case "11":
                    treatment_type = "110";
                    break;
                case "61":
                    treatment_type = "610";
                    break;
                case "13":
                    treatment_type = pInfo.Api_treatment_type;
                    regObj.ClinicDiagnose = pInfo.Api_icd;
                    break;
                case "41"://工伤医保 2017-04-29 xhl 
                    treatment_type = "410";
                    break;
                case "51":
                    if (string.IsNullOrEmpty(pInfo.Api_treatment_type))
                    {
                        ucGetTreamType uctreamType = new ucGetTreamType();
                        ArrayList alTreamType = localMgr.GetApiTreatmenttype(biz_type);
                        uctreamType.AlTreamType = alTreamType;
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(uctreamType);
                        if (uctreamType.TreamTypeObj != null && !string.IsNullOrEmpty(uctreamType.TreamTypeObj.ID))
                        {
                            treatment_type = uctreamType.TreamTypeObj.ID;
                        }
                        else
                        {
                            this.errMsg = "没有选择医保API的待遇类型，谢谢!";
                            return -1;
                        }
                    }
                    else
                    {
                        treatment_type = pInfo.Api_treatment_type;
                    }
                    break;

                default:
                    this.errMsg = "该业务类型，尚在改造中......";
                    return -1;
            }


            NoSortHashtable ht = new NoSortHashtable();
            //if (biz_type != "51") //不是生育医保
            //{
            //}
            ht.Add("oper_flag", "1");
            ht.Add("center_id", center_id);
            ht.Add("hospital_id", hospital_id);
            ht.Add("indi_id", pInfo.Api_indi_id);
            ht.Add("treatment_type", treatment_type);//需赋值
            ht.Add("ic_flag", "0");
            ht.Add("fee_batch", "1");
            ht.Add("diagnose_date", this.OperDate);//需赋值
            ht.Add("reg_date", OperDate);
            ht.Add("reg_staff", string.IsNullOrEmpty(regObj.InputOper.ID) ? "9999" : regObj.InputOper.ID);//需赋值
            ht.Add("reg_man", string.IsNullOrEmpty(regObj.InputOper.Name) ? "收费员" : regObj.InputOper.Name);//需赋值
            ht.Add("in_area", "");
            ht.Add("in_area_name", "");
            ht.Add("in_dept", regObj.DoctorInfo.Templet.Dept.ID);
            ht.Add("in_dept_name", regObj.DoctorInfo.Templet.Dept.Name);
            ht.Add("in_disease", regObj.ClinicDiagnose);//疾病编码，待定
            //ht.Add("diagnose", regObj.ClinicDiagnose);
            ht.Add("patient_id", "0000");//挂号，需赋值
            if (biz_type != "51")
            {
                ht.Add("recipe_no", "1");//string.IsNullOrEmpty(regObj.RecipeNO)?"1":regObj.RecipeNO);
            }

            ht.Add("last_balance", string.IsNullOrEmpty(pInfo.Api_last_balance) ? "0" : pInfo.Api_last_balance);
            ht.Add("save_flag", saveFlag);
            ht.Add("note", "");
            ht.Add("hos_biz_serial", string.IsNullOrEmpty(regObj.ID) ? "1" : regObj.ID);//挂号流水号clinic_no
            ht.Add("join_flag", "");//his接入

            if (biz_type == "61")//公医
            {
                ht.Add("diagnose_type", "");
                //ht.Add("diagnose_type", "");
                FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.Models.Base.Department deptinfo = dept.GetDeptmentById(regObj.DoctorInfo.Templet.Dept.ID);

                if (true) // (deptinfo.BranchID.ToString() != "02")//江南西是专科医保，不需要定点，也不需要走上传这个特殊模式
                {
                    if (regObj.DoctorInfo.Templet.RegLevel.IsEmergency)
                    {

                        ht.Add("diagnose_type", "3");
                    }
                    else
                    {
                        ht.Add("diagnose_type", "");
                    }
                }
                else
                {
                    ht.Add("diagnose_type", "");
                }
            }

            //工伤登记
            if (!string.IsNullOrEmpty(pInfo.Api_serial_wi))
            {
                ht.Add("injuryorbirth_serial", pInfo.Api_serial_wi);
            }
            //生育凭证
            if (!string.IsNullOrEmpty(pInfo.Api_serial_mn))
            {
                ht.Add("injuryorbirth_serial", pInfo.Api_serial_mn);
            }

            //普通门诊、门慢登记
            if (!string.IsNullOrEmpty(pInfo.Api_serial_apply))
            {
                ht.Add("serial_apply", pInfo.Api_serial_apply);
            }


            if (-1 == CallSIProcess("BIZH131104"))
            {
                return -1;
            }
            if (-1 == PutParams(ht))
            {
                return -1;
            }
            //费用信息
            if (alFeeDetails.Count > 0)
            {
                if (-1 == GzsiAPI.setresultset(pInt, "feeinfo"))
                {
                    this.errMsg = "医保API错误：设置费用信息参数失败！";
                    return -1;
                }
                long i = 1;
                string svalue = "";
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeDetails)
                {

                    svalue = GetItemType(feeitem);
                    if (GzsiAPI.putcol(pInt, "medi_item_type", svalue) < 0)
                    {
                        GzsiAPI.getmessage(pInt, strErrMsg);
                        this.errMsg = strErrMsg.ToString();
                        return -1;
                    }
                    svalue = feeitem.Item.UserCode;
                    if (GzsiAPI.putcol(pInt, "his_item_code", svalue) < 0)
                    {
                        GzsiAPI.getmessage(pInt, strErrMsg);
                        this.errMsg = strErrMsg.ToString();
                        return -1;
                    }
                    svalue = feeitem.Item.Name.Length > 24 ? feeitem.Item.Name.Substring(0, 24) : feeitem.Item.Name;
                    GzsiAPI.putcol(pInt, "his_item_name", svalue);
                    svalue = "";
                    GzsiAPI.putcol(pInt, "model", svalue);
                    GzsiAPI.putcol(pInt, "factory", svalue);
                    svalue = feeitem.Item.Specs;
                    GzsiAPI.putcol(pInt, "standard", svalue);
                    svalue = this.OperDate;
                    GzsiAPI.putcol(pInt, "fee_date", svalue);
                    GzsiAPI.putcol(pInt, "input_date", svalue);
                    svalue = feeitem.Item.PriceUnit;
                    GzsiAPI.putcol(pInt, "unit", svalue);
                    svalue = FS.FrameWork.Public.String.FormatNumber(feeitem.SIPrice / feeitem.Item.PackQty, 4).ToString();
                    GzsiAPI.putcol(pInt, "price", svalue);
                    svalue = feeitem.Item.Qty.ToString();
                    GzsiAPI.putcol(pInt, "dosage", svalue);
                    svalue = (feeitem.SIft.OwnCost + feeitem.SIft.PubCost + feeitem.SIft.PayCost).ToString();
                    GzsiAPI.putcol(pInt, "money", svalue);
                    svalue = string.IsNullOrEmpty(feeitem.RecipeNO) ? "1" : feeitem.RecipeNO;
                    GzsiAPI.putcol(pInt, "recipe_no", svalue);
                    svalue = feeitem.Order.Doctor.ID;
                    GzsiAPI.putcol(pInt, "doctor_no", svalue);
                    svalue = feeitem.Order.Doctor.Name;
                    GzsiAPI.putcol(pInt, "doctor_name", svalue);
                    svalue = regObj.ID + i.ToString();
                    GzsiAPI.putcol(pInt, "hos_serial", svalue);//医院费用的唯一标识(挂号流水号+序列号)
                    svalue = localMgr.GetLimitByCard_No(feeitem.RecipeNO, feeitem.SequenceNO.ToString());
                    GzsiAPI.putcol(pInt, "range_flag", svalue);//限制用药标识

                    i++;

                    this.result = GzsiAPI.nextrow(pInt);
                    if (this.result < 0)
                    {
                        this.errMsg = "HIS错误：费用明细传入出错！";
                        return -1;
                    }
                }

            }

            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = "医保API错误：" + strErrMsg.ToString();
                return -1;
            }
            if (-1 == GzsiAPI.setresultset(pInt, "bizinfo"))
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = "医保API错误：获取门诊登记信息失败！" + strErrMsg;
                return -1;
            }

            //string reg_no = "";
            //if (GzsiAPI.getbyname(pInt, "serial_no", ref reg_no) < 0)
            //return -1;
            //  regObj.SIMainInfo.RegNo = reg_no;
            regObj.SIMainInfo.ApplyType.ID = treatment_type;//保存待遇类型
            regObj.SIMainInfo.FeeTimes = 1;//费用批次
            //regObj.SIMainInfo.User01 = pInfo.Api_serial_wi;//工伤就医凭证号
            regObj.SIMainInfo.User02= pInfo.Api_serial_wi;//工伤就医凭证号
            //regObj.SIMainInfo.User03 = pInfo.Api_serial_wi;//工伤就医凭证号
            //获取返回结果
            if (-1 == GzsiAPI.setresultset(pInt, "payinfo"))
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = "医保API错误：获取门诊收费返回支付信息失败！" + strErrMsg;
                return -1;
            }

            #region 未走预结算流程
            //
            //if (saveFlag == "0")
            //{
            //}
            //else
            //{
            //    GzsiAPI.getbyname(pInt, "zyzje", strValue);
            //    payInfo.zyzje = strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "sbzfje", strValue);
            //    payInfo.sbzfje = strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "serial_no", strValue);
            //    payInfo.serial_no = strValue.ToString();
            //}
            #endregion 

            do
            {
                GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                payInfo.hospital_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "serial_no", strValue);
                payInfo.serial_no = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zyzje", strValue);
                payInfo.zyzje = strValue.ToString();

                GzsiAPI.getbyname(pInt, "sbzfje", strValue);
                payInfo.sbzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zhzfje", strValue);
                payInfo.zhzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "bfxmzfje", strValue);
                payInfo.bfxmzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "qfje", strValue);
                payInfo.qfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje1", strValue);
                payInfo.grzfje1 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje2", strValue);
                payInfo.grzfje2 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje3", strValue);
                payInfo.grzfje3 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cxzfje", strValue);
                payInfo.cxzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "yyfdje", strValue);
                payInfo.yyfdje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_com", strValue);
                payInfo.cash_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_com", strValue);
                payInfo.acct_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_own", strValue);
                payInfo.cash_pay_own = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_own", strValue);
                payInfo.acct_pay_own = strValue.ToString();
            } while (GzsiAPI.nextrow(pInt) > 0);

            regObj.SIMainInfo.RegNo = payInfo.serial_no;

            CallSIProcessEnd();
            return 1;
        }

        #endregion //end 门诊挂号登记

        #region 门诊收费

        /// <summary>
        /// 获取门诊挂号业务信息
        /// </summary>
        /// <param name="serial_no"></param>
        /// <param name="treatment_type"></param>
        /// <param name="bizInfo"></param>
        /// <returns></returns>
        public int GetSiRegisterInfo(string serial_no, string treatment_type, ref BizInfo bizInfo)
        {
            try
            {
                NoSortHashtable ht = new NoSortHashtable();
                ht.Add("oper_flag", "2");//操作标志
                ht.Add("serial_no", serial_no);//就医登记号
                ht.Add("hospital_id", this.hospital_id);//医疗机构编码
                ht.Add("treatment_type", treatment_type);//待遇类型

                if (-1 == CallSIProcessBegin("BIZH131102")) return -1;

                if (-1 == PutParams(ht))
                {
                    return -1;
                }

                long result = GzsiAPI.run(pInt);
                if (result < 0)
                {
                    GzsiAPI.getmessage(pInt, strErrMsg);
                    this.errMsg = strErrMsg.ToString();
                    return -1;
                }

                //就医登记号
                bizInfo.serial_no = serial_no;
                //获取门诊信息结果集
                if (-1 == GzsiAPI.setresultset(pInt, "bizinfo"))
                {
                    this.errMsg = "医保API错误：获取门诊业务信息失败！";
                    return -1;
                }

                do
                {
                    GzsiAPI.getbyname(pInt, "indi_id", strValue);
                    bizInfo.personInfo.Api_indi_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                    bizInfo.hospital_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "biz_type", strValue);
                    bizInfo.biz_type = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "center_id", strValue);
                    bizInfo.center_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "name", strValue);
                    bizInfo.personInfo.Api_name = strValue.ToString();

                    GzsiAPI.getbyname(pInt, "sex", strValue);
                    bizInfo.personInfo.Api_sex = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "idcard", strValue);
                    bizInfo.personInfo.Api_idcard = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "ic_no", strValue);
                    bizInfo.ic_no = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "birthday", strValue);
                    bizInfo.personInfo.Api_birthday = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "telephone", strValue);
                    bizInfo.personInfo.Api_telephone = strValue.ToString();

                    GzsiAPI.getbyname(pInt, "corp_id", strValue);
                    bizInfo.personInfo.Api_corp_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "corp_name", strValue);
                    bizInfo.personInfo.Api_corp_name = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "treatment_type", strValue);
                    bizInfo.treatment_type = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "reg_date", strValue);
                    bizInfo.reg_date = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "reg_staff", strValue);
                    bizInfo.reg_staff = strValue.ToString();

                    GzsiAPI.getbyname(pInt, "reg_man", strValue);
                    bizInfo.reg_man = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "reg_flag", strValue);
                    bizInfo.reg_flag = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "begin_date", strValue);
                    bizInfo.begin_date = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "reg_info", strValue);
                    bizInfo.reg_info = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "in_dept", strValue);
                    bizInfo.in_dept = strValue.ToString();

                    GzsiAPI.getbyname(pInt, "in_dept_name", strValue);
                    bizInfo.in_dept_name = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "in_area", strValue);
                    bizInfo.in_area = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "in_area_name", strValue);
                    bizInfo.in_area_name = strValue.ToString();

                    GzsiAPI.getbyname(pInt, "in_bed", strValue);
                    bizInfo.in_bed = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "patient_id", strValue);
                    bizInfo.patient_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "in_disease", strValue);
                    bizInfo.in_disease = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "disease", strValue);
                    bizInfo.disease = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "ic_flag", strValue);
                    bizInfo.ic_flag = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "remark", strValue);
                    bizInfo.remark = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "total_fee", strValue);
                    bizInfo.total_fee = FS.FrameWork.Function.NConvert.ToDecimal(strValue.ToString());

                    //门慢
                    GzsiAPI.getbyname(pInt, "serial_apply", strValue);
                    bizInfo.serial_apply = strValue.ToString();

                } while (GzsiAPI.nextrow(pInt) > 0);

                if (GzsiAPI.setresultset(pInt, "personinfo") > 0)
                {
                    GzsiAPI.getbyname(pInt, "last_balance", strValue);
                    bizInfo.personInfo.Api_last_balance = strValue.ToString();
                }

                if (GzsiAPI.setresultset(pInt, "injuryorbirthinfo") > 0)
                {
                    GzsiAPI.getbyname(pInt, "serial_wi", strValue);//工伤凭证号
                    bizInfo.injury_borth_sn = strValue.ToString();
                    if (string.IsNullOrEmpty(bizInfo.injury_borth_sn))
                    {
                        GzsiAPI.getbyname(pInt, "serial_mn", strValue);//生育就医凭证号
                        bizInfo.injury_borth_sn = strValue.ToString();
                    }
                }

                CallSIProcessEnd();
                return 1;
            }
            catch (Exception ex)
            {
                this.errMsg = "HIS错误：获取门诊挂号业务信息失败！" + ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// 校验计算并保存费用信息(门诊收费)
        /// </summary>
        /// <param name="bInfo"></param>
        /// <param name="alFeeDetails"></param>
        /// <param name="payInfo"></param>
        /// <param name="saveflag">0:试算 1：收费</param>
        /// <returns></returns>
        public int SiClinicFee(BizInfo bInfo, ArrayList alFeeDetails, ref PayInfo payInfo, string saveflag)
        {
            NoSortHashtable ht = new NoSortHashtable();

            ht.Add("oper_flag", "2");
            ht.Add("center_id", center_id);//中心编码
            ht.Add("hospital_id", bInfo.hospital_id);
            ht.Add("serial_no", bInfo.serial_no);
            ht.Add("indi_id", bInfo.personInfo.Api_indi_id);
            ht.Add("treatment_type", bInfo.treatment_type);
            ht.Add("in_disease", bInfo.in_disease);
            ht.Add("reg_staff", bInfo.reg_staff);
            ht.Add("reg_man", string.IsNullOrEmpty(bInfo.reg_man) ? "收费员" : bInfo.reg_man);
            ht.Add("last_balance", bInfo.personInfo.Api_last_balance);

            //试算、收费标记
            ht.Add("save_flag", saveflag);
            ht.Add("reg_date", bInfo.reg_date);
            ht.Add("in_dept", bInfo.in_dept);
            ht.Add("in_dept_name", bInfo.in_dept_name);

            if (!string.IsNullOrEmpty(bInfo.injury_borth_sn))
            {
                ht.Add("injuryorbirth_serial", bInfo.injury_borth_sn);
            }
            else
            {
                ht.Add("serial_apply", string.IsNullOrEmpty(bInfo.personInfo.Api_serial_apply) ? "0" : bInfo.personInfo.Api_serial_apply);
                ht.Add("apply_hospital", bInfo.hospital_id);
                ht.Add("apply_content", bInfo.treatment_type);
            }

            if (-1 == CallSIProcess("BIZH131104")) return -1;

            if (-1 == PutParams(ht))
            {
                return -1;
            }

            if (alFeeDetails.Count > 0)
            {
                if (-1 == GzsiAPI.setresultset(pInt, "feeinfo"))
                {
                    this.errMsg = "医保API错误：设置费用信息参数失败！";
                    return -1;
                }

                int i = 1;
                GzsiAPI.firstrow(pInt);
                DateTime dtNow = this.GetDateTimeFromSysDateTime();
                string svalue = ""; //一定要定义变量
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeDetails)
                {
                    svalue = GetItemType(feeitem);
                    if (GzsiAPI.putcol(pInt, "medi_item_type", svalue) < 0)
                    {
                        GzsiAPI.getmessage(pInt, strErrMsg);
                        this.errMsg = strErrMsg.ToString();
                        return -1;
                    }
                    svalue = feeitem.Item.UserCode;
                    if (GzsiAPI.putcol(pInt, "his_item_code", svalue) < 0)
                    {
                        GzsiAPI.getmessage(pInt, strErrMsg);
                        this.errMsg = strErrMsg.ToString();
                        return -1;
                    }
                    svalue = feeitem.Item.Name.Length > 24 ? feeitem.Item.Name.Substring(0, 24) : feeitem.Item.Name;
                    GzsiAPI.putcol(pInt, "his_item_name", svalue);
                    svalue = "";
                    GzsiAPI.putcol(pInt, "model", svalue);
                    GzsiAPI.putcol(pInt, "factory", svalue);
                    svalue = feeitem.Item.Specs;
                    GzsiAPI.putcol(pInt, "standard", svalue);
                    svalue = dtNow.ToString();
                    GzsiAPI.putcol(pInt, "fee_date", svalue);
                    GzsiAPI.putcol(pInt, "input_date", svalue);
                    svalue = feeitem.Item.PriceUnit;
                    GzsiAPI.putcol(pInt, "unit", svalue);
                    svalue = FS.FrameWork.Public.String.FormatNumber(feeitem.Item.Price / feeitem.Item.PackQty, 4).ToString();
                    GzsiAPI.putcol(pInt, "price", svalue);
                    svalue = feeitem.Item.Qty.ToString();
                    GzsiAPI.putcol(pInt, "dosage", svalue);
                    svalue = (feeitem.FT.OwnCost + feeitem.FT.PubCost + feeitem.FT.PayCost).ToString();
                    GzsiAPI.putcol(pInt, "money", svalue);
                    svalue = string.IsNullOrEmpty(feeitem.RecipeNO) ? "1" : feeitem.RecipeNO;
                    GzsiAPI.putcol(pInt, "recipe_no", svalue);
                    svalue = feeitem.Order.Doctor.ID;
                    GzsiAPI.putcol(pInt, "doctor_no", svalue);
                    svalue = feeitem.Order.Doctor.Name;
                    GzsiAPI.putcol(pInt, "doctor_name", svalue);
                    svalue = bInfo.patient_id + i.ToString();
                    GzsiAPI.putcol(pInt, "hos_serial", svalue);//医院费用的唯一标识(挂号流水号+序列号)
                    
                    i++;

                    this.result = GzsiAPI.nextrow(pInt);
                    if (this.result < 0)
                    {
                        this.errMsg = "HIS错误：费用明细传入出错！";
                        return -1;
                    }
                }

            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString();
                return -1;
            }

            //获取返回结果
            if (-1 == GzsiAPI.setresultset(pInt, "payinfo"))
            {
                this.errMsg = "医保API错误：获取门诊收费返回支付信息失败！";
                return -1;
            }
            do
            {
                GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                payInfo.hospital_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "serial_no", strValue);
                payInfo.serial_no = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zyzje", strValue);
                payInfo.zyzje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sbzfje", strValue);
                payInfo.sbzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zhzfje", strValue);
                payInfo.zhzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "bfxmzfje", strValue);
                payInfo.bfxmzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "qfje", strValue);
                payInfo.qfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje1", strValue);
                payInfo.grzfje1 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje2", strValue);
                payInfo.grzfje2 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje3", strValue);
                payInfo.grzfje3 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cxzfje", strValue);
                payInfo.cxzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "yyfdje", strValue);
                payInfo.yyfdje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_com", strValue);
                payInfo.cash_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_com", strValue);
                payInfo.acct_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_own", strValue);
                payInfo.cash_pay_own = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_own", strValue);
                payInfo.acct_pay_own = strValue.ToString();
            } while (GzsiAPI.nextrow(pInt) > 0);

            CallSIProcessEnd();
            return 1;

        }

        /// <summary>
        /// 校验计算并保存费用信息(门慢收费)
        /// </summary>
        /// <param name="bInfo"></param>
        /// <param name="alFeeDetails"></param>
        /// <param name="payInfo"></param>
        /// <returns></returns>
        public int SiTsClinicFee(BizInfo bInfo, ArrayList alFeeDetails, ref PayInfo payInfo, string saveFlag)
        {
            NoSortHashtable ht = new NoSortHashtable();
            ht.Add("oper_flag", "2");
            ht.Add("hospital_id", bInfo.hospital_id);
            ht.Add("serial_no", bInfo.serial_no);
            ht.Add("indi_id", bInfo.personInfo.Api_indi_id);
            ht.Add("treatment_type", bInfo.treatment_type);
            ht.Add("in_disease", bInfo.in_disease);
            ht.Add("last_balance", bInfo.personInfo.Api_last_balance);
            ht.Add("diagnose_date", bInfo.diagnose_date);
            ht.Add("reg_date", bInfo.reg_date);

            ht.Add("reg_staff", bInfo.reg_staff);
            ht.Add("reg_man", bInfo.reg_man);
            ht.Add("save_flag", saveFlag);

            if (-1 == CallSIProcessBegin("BIZH131104")) return -1;

            if (-1 == PutParams(ht))
            {
                return -1;
            }

            if (alFeeDetails.Count > 0)
            {
                if (-1 == GzsiAPI.setresultset(pInt, "feeinfo"))
                {
                    this.errMsg = "HIS错误：设置费用信息参数失败！";
                    return -1;
                }
                int i = 1;
                string svalue = "";
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeDetails)
                {
                    svalue = GetItemType(feeitem);
                    if (GzsiAPI.putcol(pInt, "medi_item_type", svalue) < 0)
                    {
                        GzsiAPI.getmessage(pInt, strErrMsg);
                        this.errMsg = strErrMsg.ToString();
                        return -1;
                    }
                    svalue = feeitem.Item.UserCode;
                    if (GzsiAPI.putcol(pInt, "his_item_code", svalue) < 0)
                    {
                        GzsiAPI.getmessage(pInt, strErrMsg);
                        this.errMsg = strErrMsg.ToString();
                        return -1;
                    }
                    svalue = feeitem.Item.Name.Length > 24 ? feeitem.Item.Name.Substring(0, 24) : feeitem.Item.Name;
                    GzsiAPI.putcol(pInt, "his_item_name", svalue);
                    svalue = "";
                    GzsiAPI.putcol(pInt, "model", svalue);
                    GzsiAPI.putcol(pInt, "factory", svalue);
                    svalue = feeitem.Item.Specs;
                    GzsiAPI.putcol(pInt, "standard", svalue);
                    svalue = this.OperDate;
                    GzsiAPI.putcol(pInt, "fee_date", svalue);
                    GzsiAPI.putcol(pInt, "input_date", svalue);
                    svalue = feeitem.Item.PriceUnit;
                    GzsiAPI.putcol(pInt, "unit", svalue);
                    svalue = FS.FrameWork.Public.String.FormatNumber(feeitem.Item.Price / feeitem.Item.PackQty, 4).ToString();
                    GzsiAPI.putcol(pInt, "price", svalue);
                    svalue = feeitem.Item.Qty.ToString();
                    GzsiAPI.putcol(pInt, "dosage", svalue);
                    svalue = (feeitem.FT.OwnCost + feeitem.FT.PubCost + feeitem.FT.PayCost).ToString();
                    GzsiAPI.putcol(pInt, "money", svalue);
                    svalue = string.IsNullOrEmpty(feeitem.RecipeNO) ? "1" : feeitem.RecipeNO;
                    GzsiAPI.putcol(pInt, "recipe_no", svalue);
                    svalue = feeitem.Order.Doctor.ID;
                    GzsiAPI.putcol(pInt, "doctor_no", svalue);
                    svalue = feeitem.Order.Doctor.Name;
                    GzsiAPI.putcol(pInt, "doctor_name", svalue);
                    svalue = bInfo.patient_id + i.ToString();
                    GzsiAPI.putcol(pInt, "hos_serial", svalue);//医院费用的唯一标识(挂号流水号+序列号)

                    this.result = GzsiAPI.nextrow(pInt);
                    svalue = feeitem.Order.ExtendFlag2;
                    
                    if (this.result < 0)
                    {
                        this.errMsg = "HIS错误：费用明细传入出错！";
                        return -1;
                    }
                    i++;
                }

            }
            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg); ;
                return -1;
            }

            //获取返回结果
            if (-1 == GzsiAPI.setresultset(pInt, "payinfo"))
            {
                this.errMsg = "医保API错误：获取门诊收费返回支付信息失败！";
                return -1;
            }
            do
            {
                GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                payInfo.hospital_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "serial_no", strValue);
                payInfo.serial_no = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zyzje", strValue);
                payInfo.zyzje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sbzfje", strValue);
                payInfo.sbzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zhzfje", strValue);
                payInfo.zhzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "bfxmzfje", strValue);
                payInfo.bfxmzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "qfje", strValue);
                payInfo.qfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje1", strValue);
                payInfo.grzfje1 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje2", strValue);
                payInfo.grzfje2 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje3", strValue);
                payInfo.grzfje3 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cxzfje", strValue);
                payInfo.cxzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "yyfdje", strValue);
                payInfo.yyfdje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_com", strValue);
                payInfo.cash_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_com", strValue);
                payInfo.acct_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_own", strValue);
                payInfo.cash_pay_own = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_own", strValue);
                payInfo.acct_pay_own = strValue.ToString();
            } while (GzsiAPI.nextrow(pInt) > 0);

            CallSIProcessEnd();
            return 1;

        }

        #endregion //end 门诊收费

        #region 门诊退费

        /// <summary>
        /// 获取费用信息
        /// </summary>
        /// <param name="regObj">挂号登记实体</param>
        /// <param name="treatment_type">待遇类型</param>
        /// <param name="fee_batch">费用批次 0：所有批次的费用信息</param>
        /// <param name="rowCount">费用批次数</param>
        /// <param name="BizInfo">业务信息</param>
        /// <returns>rowCount=1  FeeInfo组 rowCount >1 FeeBatchInfo组 </returns>
        public ArrayList GetSiClinicFeeInfo(FS.HISFC.Models.Registration.Register regObj, string treatment_type, string fee_batch, ref long rowCount, ref BizInfo bizInfo)
        {
            NoSortHashtable ht = new NoSortHashtable();
            ht.Add("Oper_flag", "3");//退费
            ht.Add("Serial_no", regObj.SIMainInfo.RegNo);
            ht.Add("hospital_id", this.hospital_id);
            ht.Add("treatment_type", treatment_type);
            ht.Add("fee_batch", fee_batch);

            if (-1 == CallSIProcessBegin("BIZH131102")) return null;

            if (-1 == PutParams(ht))
            {
                return null;
            }

            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = strErrMsg.ToString();
                return null;
            }
            //获取返回结果
            ArrayList alFeeInfo = new ArrayList();

            if (GzsiAPI.setresultset(pInt, "feebatchinfo") < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg);
                this.errMsg = "设置费用批次失败！" + strErrMsg.ToString();
                return null;
            }
            rowCount = GzsiAPI.getrowcount(pInt);

            if (rowCount == 1)
            {
                //只有一个费用批次
                //业务信息
                if (GzsiAPI.setresultset(pInt, "bizinfo") < 0)
                {
                    GzsiAPI.getmessage(pInt, strErrMsg); ;
                    return null;
                }
                else
                {
                    GzsiAPI.getbyname(pInt, "serial_no", strValue);
                    bizInfo.serial_no = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "indi_id", strValue);
                    bizInfo.personInfo.Api_indi_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "treatment_type", strValue);
                    bizInfo.treatment_type = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "fee_batch", strValue);
                    bizInfo.fee_batch = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "reg_staff", strValue);
                    bizInfo.reg_staff = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "reg_man", strValue);
                    bizInfo.reg_man = strValue.ToString();
                    if (bizInfo.treatment_type == "511")
                    {
                        GzsiAPI.getbyname(pInt, "injury_borth_sn", strValue);
                        bizInfo.personInfo.Api_serial_mn = strValue.ToString();
                    }

                    if (bizInfo.treatment_type == "410") //工伤 xhl 2017-04-29   //{249DC040-7C55-4a7e-BAB8-6FF56FF985E5}
                    {
                        GzsiAPI.getbyname(pInt, "injury_borth_sn", strValue);
                        bizInfo.personInfo.Api_serial_wi = strValue.ToString();
                    }
                    ////工伤认定信息第二种获取方法 xhl 2017-04-29   //{249DC040-7C55-4a7e-BAB8-6FF56FF985E5}
                    //if (-1 == GzsiAPI.setresultset(pInt, "injuryorbirthinfo"))
                    //    return null;
                    //GzsiAPI.getbyname(pInt, "serial_wi", strValue);
                    //bizInfo.personInfo.Api_serial_wi = strValue.ToString();
                }

                //费用信息
                if (GzsiAPI.setresultset(pInt, "feeinfo") < 0)
                {
                    GzsiAPI.getmessage(pInt, strErrMsg); ;
                    return null;
                }
                else
                {
                    do
                    {
                        FeeInfo feeInfo = new FeeInfo();
                        GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                        feeInfo.hospital_id = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "serial_no", strValue);
                        feeInfo.serial_no = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "fee_batch", strValue);
                        feeInfo.fee_batch = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "serial_fee", strValue);
                        feeInfo.serial_fee = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "Stat_type", strValue);
                        feeInfo.Stat_type = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "medi_item_type", strValue);
                        feeInfo.medi_item_type = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "Item_code", strValue);
                        feeInfo.Item_code = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "Item_name", strValue);
                        feeInfo.Item_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "his_item_code", strValue);
                        feeInfo.his_item_code = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "his_item_name", strValue);
                        feeInfo.his_item_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "fee_date", strValue);
                        feeInfo.fee_date = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "model", strValue);
                        feeInfo.model = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "factory", strValue);
                        feeInfo.factory = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "standard", strValue);
                        feeInfo.standard = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "Unit", strValue);
                        feeInfo.Unit = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "Price", strValue);
                        feeInfo.Price = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "dosage", strValue);
                        feeInfo.dosage = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "Money", strValue);
                        feeInfo.Money = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "reduce_money", strValue);
                        feeInfo.reduce_money = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "usage_flag", strValue);
                        feeInfo.usage_flag = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "opp_serial_fee", strValue);
                        feeInfo.opp_serial_fee = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "recipe_no", strValue);
                        feeInfo.recipe_no = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "doctor_no", strValue);
                        feeInfo.doctor_no = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "doctor_name", strValue);
                        feeInfo.doctor_name = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "input_date", strValue);
                        feeInfo.input_date = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "input_staff", strValue);
                        feeInfo.input_staff = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "input_name", strValue);
                        feeInfo.input_name = strValue.ToString();

                        GzsiAPI.getbyname(pInt, "item_code", strValue);
                        feeInfo.Item_code = strValue.ToString();
                        GzsiAPI.getbyname(pInt, "item_name", strValue);
                        feeInfo.Item_name = strValue.ToString();



                        alFeeInfo.Add(feeInfo);

                    } while (GzsiAPI.nextrow(pInt) > 0);

                }


            }
            else
            {
                //存在多个费用批次
                rowCount = GzsiAPI.getrowcount(pInt);
                do
                {
                    FeeBatchInfo fbInfo = new FeeBatchInfo();
                    GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                    fbInfo.hospital_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "serial_no", strValue);
                    fbInfo.serial_no = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "Indi_id", strValue);
                    fbInfo.Indi_id = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "Name", strValue);
                    fbInfo.Name = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "fee_batch", strValue);
                    fbInfo.fee_batch = strValue.ToString();
                    GzsiAPI.getbyname(pInt, "sum_fee", strValue);
                    fbInfo.sum_fee = strValue.ToString();

                    alFeeInfo.Add(fbInfo);

                } while (GzsiAPI.nextrow(pInt) > 0);
            }

            return alFeeInfo;

        }

        /// <summary>
        /// 门诊退费 工伤
        /// </summary>
        /// <param name="bInfo"></param>
        /// <param name="alFeeInfo"></param>
        /// <returns></returns>
        public int CancleSiClinicFeeGS(BizInfo bInfo, ArrayList alFeeInfo, ref PayInfo payInfo)
        {
            NoSortHashtable ht = new NoSortHashtable();
            ht.Add("save_flag", "1");
            ht.Add("oper_flag", "3");
            ht.Add("center_id", center_id);//中心编码
            ht.Add("hospital_id", hospital_id);
            ht.Add("serial_apply", bInfo.serial_apply);

            ht.Add("indi_id", bInfo.personInfo.Api_indi_id);
            ht.Add("biz_type", bInfo.treatment_type.Substring(0, 2));
            ht.Add("treatment_type", bInfo.treatment_type);

            ht.Add("serial_no", bInfo.serial_no);
            ht.Add("end_flag", "0");
            ht.Add("reg_staff", string.IsNullOrEmpty(bInfo.reg_staff) ? "9999" : bInfo.reg_staff);
            ht.Add("reg_man", bInfo.reg_man);
            ht.Add("fee_batch", bInfo.fee_batch);
            ht.Add("last_balance", string.IsNullOrEmpty(bInfo.personInfo.Api_last_balance) ? "0" : bInfo.personInfo.Api_last_balance);
            ht.Add("injuryorbirth_serial", bInfo.personInfo.Api_serial_wi);//工伤凭证号

            //////////////////////////////////////////////////////////

            if (-1 == CallSIProcessBegin("BIZH131104")) return -1;

            if (-1 == PutParams(ht))
            {
                return -1;
            }

            if (-1 == GzsiAPI.setresultset(pInt, "feeinfo"))
            {
                this.errMsg = "医保API错误：设置退费费用信息参数失败！";
                return -1;
            }
            if (alFeeInfo.Count <= 0)
            {
                this.errMsg = "医保API错误：没有退费费用信息，请核实！";
                return -1;
            }
            GzsiAPI.firstrow(pInt);
            string str = "";
            string input_money = "";
            str = string.IsNullOrEmpty(bInfo.serial_apply) ? "0" : bInfo.serial_apply;
            foreach (FeeInfo f in alFeeInfo)
            {
                GzsiAPI.putcol(pInt, "serial_apply", str);
                GzsiAPI.putcol(pInt, "serial_fee", f.serial_fee);
                GzsiAPI.putcol(pInt, "serial_no", bInfo.serial_no);
                GzsiAPI.putcol(pInt, "serial_fee", f.serial_fee);

                GzsiAPI.putcol(pInt, "recipe_no", f.recipe_no);
                GzsiAPI.putcol(pInt, "doctor_no", f.doctor_no);
                GzsiAPI.putcol(pInt, "doctor_name", f.doctor_name);
                GzsiAPI.putcol(pInt, "input_date", this.OperDate);
                GzsiAPI.putcol(pInt, "input_man", string.IsNullOrEmpty(bInfo.reg_man) ? "收费员" : bInfo.reg_man);
                GzsiAPI.putcol(pInt, "input_staff", string.IsNullOrEmpty(bInfo.reg_staff) ? "9999" : bInfo.reg_staff);
                GzsiAPI.putcol(pInt, "fee_date", f.fee_date);
                GzsiAPI.putcol(pInt, "calc_flag", "1");
                GzsiAPI.putcol(pInt, "item_code", f.Item_code);
                GzsiAPI.putcol(pInt, "item_name", f.Item_name);

                GzsiAPI.putcol(pInt, "medi_item_type", f.medi_item_type);
                GzsiAPI.putcol(pInt, "his_item_code", f.his_item_code);
                GzsiAPI.putcol(pInt, "his_item_name", f.his_item_name);
                GzsiAPI.putcol(pInt, "model", f.model);
                GzsiAPI.putcol(pInt, "factory", f.factory);
                GzsiAPI.putcol(pInt, "standard", f.standard);
                GzsiAPI.putcol(pInt, "fee_date", f.fee_date);
                GzsiAPI.putcol(pInt, "unit", f.Unit);
                GzsiAPI.putcol(pInt, "price", f.Price);
                GzsiAPI.putcol(pInt, "dosage", "0");
                GzsiAPI.putcol(pInt, "money", "0");
                //GzsiAPI.putcol(pInt, "opp_serial_fee", f.opp_serial_fee);
                GzsiAPI.putcol(pInt, "opp_serial_fee", f.serial_fee);//工伤
                

                GzsiAPI.putcol(pInt, "note", "");
                GzsiAPI.putcol(pInt, "old_money", f.Money);
                GzsiAPI.putcol(pInt, "reduce_money", "0");
                GzsiAPI.putcol(pInt, "stat_type", f.Stat_type);
                GzsiAPI.putcol(pInt, "fee_batch", bInfo.fee_batch);
                input_money = "-" + f.Money;
                GzsiAPI.putcol(pInt, "input_money", input_money);

                GzsiAPI.putcol(pInt, "cancel_flag", "1");


                GzsiAPI.putcol(pInt, "hos_serial", f.hos_serial);

                int ll_result = GzsiAPI.nextrow(pInt);
                if (ll_result < 0)
                {
                    this.errMsg = "医保API错误：费用信息传入失败";
                    return -1;
                }

            }

            if (GzsiAPI.run(pInt) < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg); ;
                errMsg = "医保API错误：门诊退费失败！" + strErrMsg.ToString();
                return -1;
            }

            #region 屏蔽，不需要返回
            //获取返回结果
            if (-1 == GzsiAPI.setresultset(pInt, "payinfo"))
            {
                this.errMsg = "错误：获取门诊收费返回支付信息失败！";
                return -1;
            }
            do
            {
                GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                payInfo.hospital_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "serial_no", strValue);
                payInfo.serial_no = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zyzje", strValue);
                payInfo.zyzje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sbzfje", strValue);
                payInfo.sbzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zhzfje", strValue);
                payInfo.zhzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "bfxmzfje", strValue);
                payInfo.bfxmzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "qfje", strValue);
                payInfo.qfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje1", strValue);
                payInfo.grzfje1 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje2", strValue);
                payInfo.grzfje2 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje3", strValue);
                payInfo.grzfje3 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cxzfje", strValue);
                payInfo.cxzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "yyfdje", strValue);
                payInfo.yyfdje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_com", strValue);
                payInfo.cash_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_com", strValue);
                payInfo.acct_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_own", strValue);
                payInfo.cash_pay_own = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_own", strValue);
                payInfo.acct_pay_own = strValue.ToString();
            } while (GzsiAPI.nextrow(pInt) > 0);

            #endregion
            CallSIProcessEnd();
            return 1;

        }

        /// <summary>
        /// 门诊退费 普通、公医
        /// </summary>
        /// <param name="bInfo"></param>
        /// <param name="alFeeInfo"></param>
        /// <returns></returns>
        public int CancleSiClinicFee(BizInfo bInfo, ArrayList alFeeInfo, ref PayInfo payInfo)
        {
            NoSortHashtable ht = new NoSortHashtable();
            ht.Add("save_flag", "1");
            ht.Add("oper_flag", "3");
            ht.Add("center_id", center_id);//中心编码
            ht.Add("hospital_id", hospital_id);
            ht.Add("serial_apply", bInfo.serial_apply);

            ht.Add("indi_id", bInfo.personInfo.Api_indi_id);
            ht.Add("biz_type", bInfo.treatment_type.Substring(0, 2));
            ht.Add("treatment_type", bInfo.treatment_type);

            ht.Add("serial_no", bInfo.serial_no);
            ht.Add("end_flag", "0");
            ht.Add("reg_staff", string.IsNullOrEmpty(bInfo.reg_staff) ? "9999" : bInfo.reg_staff);
            ht.Add("reg_man", bInfo.reg_man);
            ht.Add("fee_batch", bInfo.fee_batch);
            ht.Add("last_balance", string.IsNullOrEmpty(bInfo.personInfo.Api_last_balance) ? "0" : bInfo.personInfo.Api_last_balance);


            //////////////////////////////////////////////////////////

            if (-1 == CallSIProcessBegin("BIZH131104")) return -1;

            if (-1 == PutParams(ht))
            {
                return -1;
            }

            if (-1 == GzsiAPI.setresultset(pInt, "feeinfo"))
            {
                this.errMsg = "医保API错误：设置退费费用信息参数失败！";
                return -1;
            }
            if (alFeeInfo.Count <= 0)
            {
                this.errMsg = "医保API错误：没有退费费用信息，请核实！";
                return -1;
            }
            GzsiAPI.firstrow(pInt);
            string str = "";
            string input_money = "";
            str = string.IsNullOrEmpty(bInfo.serial_apply) ? "0" : bInfo.serial_apply;
            foreach (FeeInfo f in alFeeInfo)
            {
                GzsiAPI.putcol(pInt, "serial_apply", str);
                GzsiAPI.putcol(pInt, "serial_fee", f.serial_fee);
                GzsiAPI.putcol(pInt, "serial_no", bInfo.serial_no);
                GzsiAPI.putcol(pInt, "serial_fee", f.serial_fee);

                GzsiAPI.putcol(pInt, "recipe_no", f.recipe_no);
                GzsiAPI.putcol(pInt, "doctor_no", f.doctor_no);
                GzsiAPI.putcol(pInt, "doctor_name", f.doctor_name);
                GzsiAPI.putcol(pInt, "input_date", this.OperDate);
                GzsiAPI.putcol(pInt, "input_man", string.IsNullOrEmpty(bInfo.reg_man) ? "收费员" : bInfo.reg_man);
                GzsiAPI.putcol(pInt, "input_staff", string.IsNullOrEmpty(bInfo.reg_staff) ? "9999" : bInfo.reg_staff);
                GzsiAPI.putcol(pInt, "fee_date", f.fee_date);
                GzsiAPI.putcol(pInt, "calc_flag", "1");
                GzsiAPI.putcol(pInt, "item_code", f.Item_code);
                GzsiAPI.putcol(pInt, "item_name", f.Item_name);

                GzsiAPI.putcol(pInt, "medi_item_type", f.medi_item_type);
                GzsiAPI.putcol(pInt, "his_item_code", f.his_item_code);
                GzsiAPI.putcol(pInt, "his_item_name", f.his_item_name);
                GzsiAPI.putcol(pInt, "model", f.model);
                GzsiAPI.putcol(pInt, "factory", f.factory);
                GzsiAPI.putcol(pInt, "standard", f.standard);
                GzsiAPI.putcol(pInt, "fee_date", f.fee_date);
                GzsiAPI.putcol(pInt, "unit", f.Unit);
                GzsiAPI.putcol(pInt, "price", f.Price);
                GzsiAPI.putcol(pInt, "dosage", "0");
                GzsiAPI.putcol(pInt, "money", "0");
                GzsiAPI.putcol(pInt, "opp_serial_fee", f.opp_serial_fee);

                GzsiAPI.putcol(pInt, "note", "");
                GzsiAPI.putcol(pInt, "old_money", f.Money);
                GzsiAPI.putcol(pInt, "reduce_money", "0");
                GzsiAPI.putcol(pInt, "stat_type", f.Stat_type);
                GzsiAPI.putcol(pInt, "fee_batch", bInfo.fee_batch);
                input_money = "-" + f.Money;
                GzsiAPI.putcol(pInt, "input_money", input_money);

                GzsiAPI.putcol(pInt, "cancel_flag", "1");


                GzsiAPI.putcol(pInt, "hos_serial", f.hos_serial);
                GzsiAPI.putcol(pInt, "range_flag", "0");
                

                int ll_result = GzsiAPI.nextrow(pInt);
                if (ll_result < 0)
                {
                    this.errMsg = "医保API错误：费用信息传入失败";
                    return -1;
                }

            }

            if (GzsiAPI.run(pInt) < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg); ;
                errMsg = "医保API错误：门诊退费失败！" + strErrMsg.ToString();
                return -1;
            }

            #region 屏蔽，不需要返回
            //获取返回结果
            if (-1 == GzsiAPI.setresultset(pInt, "payinfo"))
            {
                this.errMsg = "错误：获取门诊收费返回支付信息失败！";
                return -1;
            }
            do
            {
                GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                payInfo.hospital_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "serial_no", strValue);
                payInfo.serial_no = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zyzje", strValue);
                payInfo.zyzje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sbzfje", strValue);
                payInfo.sbzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zhzfje", strValue);
                payInfo.zhzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "bfxmzfje", strValue);
                payInfo.bfxmzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "qfje", strValue);
                payInfo.qfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje1", strValue);
                payInfo.grzfje1 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje2", strValue);
                payInfo.grzfje2 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje3", strValue);
                payInfo.grzfje3 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cxzfje", strValue);
                payInfo.cxzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "yyfdje", strValue);
                payInfo.yyfdje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_com", strValue);
                payInfo.cash_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_com", strValue);
                payInfo.acct_pay_com = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cash_pay_own", strValue);
                payInfo.cash_pay_own = strValue.ToString();
                GzsiAPI.getbyname(pInt, "acct_pay_own", strValue);
                payInfo.acct_pay_own = strValue.ToString();
            } while (GzsiAPI.nextrow(pInt) > 0);

            #endregion
            CallSIProcessEnd();
            return 1;

        }

        /// <summary>
        /// 门诊退费(无中心编码) 工伤、生育、门慢
        /// </summary>
        /// <param name="bInfo"></param>
        /// <param name="alFeeInfo"></param>
        /// <returns></returns>
        public int CancleTsSiClinicFee(BizInfo bInfo, ArrayList alFeeInfo, ref PayInfo payInfo)
        {
            NoSortHashtable ht = new NoSortHashtable(); 
            ht.Add("oper_flag", "3");
            ht.Add("center_id", this.center_id);//中心编码
            ht.Add("hospital_id", this.hospital_id);
            ht.Add("serial_no", bInfo.serial_no);
            ht.Add("indi_id", bInfo.personInfo.Api_indi_id);
            ht.Add("biz_type", bInfo.treatment_type.Substring(0, 2));
            ht.Add("treatment_type", bInfo.treatment_type);
            ht.Add("fee_batch", bInfo.fee_batch);
            ht.Add("reg_staff", string.IsNullOrEmpty(bInfo.reg_staff) ? "9999" : bInfo.reg_staff);
            ht.Add("reg_man", string.IsNullOrEmpty(bInfo.reg_man) ? "收费员" : bInfo.reg_man);
            ht.Add("save_flag", "1");
            ht.Add("join_flag", "HIS");
            ht.Add("end_flag", "0");
            ht.Add("last_balance", string.IsNullOrEmpty(bInfo.personInfo.Api_last_balance) ? "0" : bInfo.personInfo.Api_last_balance);
            ht.Add("injuryorbirth_serial", bInfo.personInfo.Api_serial_mn);

            ///////////////////////////////////////////////////////////
            if (-1 == CallSIProcessBegin("BIZH131104")) return -1;

            if (-1 == PutParams(ht))
            {
                return -1;
            }

            if (-1 == GzsiAPI.setresultset(pInt, "feeinfo"))
            {
                this.errMsg = "错误：设置退费费用信息参数失败！";
                return -1;
            }
            if (alFeeInfo.Count <= 0)
            {
                this.errMsg = "错误：没有退费费用信息，请核实！";
                return -1;
            }
            int i = 1;
            string str = "";
            string input_money = "";
            str = string.IsNullOrEmpty(bInfo.serial_apply) ? "0" : bInfo.serial_apply;
            foreach (FeeInfo f in alFeeInfo)
            {
                GzsiAPI.putcol(pInt, "serial_apply", str);
                GzsiAPI.putcol(pInt, "serial_fee", f.serial_fee);
                GzsiAPI.putcol(pInt, "serial_no", bInfo.serial_no);
                GzsiAPI.putcol(pInt, "serial_fee", f.serial_fee);

                GzsiAPI.putcol(pInt, "recipe_no", f.recipe_no);
                GzsiAPI.putcol(pInt, "doctor_no", f.doctor_no);
                GzsiAPI.putcol(pInt, "doctor_name", f.doctor_name);
                GzsiAPI.putcol(pInt, "input_date", this.OperDate);
                GzsiAPI.putcol(pInt, "input_man", string.IsNullOrEmpty(bInfo.reg_man) ? "收费员" : bInfo.reg_man);
                GzsiAPI.putcol(pInt, "input_staff", string.IsNullOrEmpty(bInfo.reg_staff) ? "9999" : bInfo.reg_staff);
                GzsiAPI.putcol(pInt, "fee_date", f.fee_date);
                GzsiAPI.putcol(pInt, "calc_flag", "1");
                GzsiAPI.putcol(pInt, "item_code", f.Item_code);
                GzsiAPI.putcol(pInt, "item_name", f.Item_name);

                GzsiAPI.putcol(pInt, "medi_item_type", f.medi_item_type);
                GzsiAPI.putcol(pInt, "his_item_code", f.his_item_code);
                GzsiAPI.putcol(pInt, "his_item_name", f.his_item_name);
                GzsiAPI.putcol(pInt, "model", f.model);
                GzsiAPI.putcol(pInt, "factory", f.factory);
                GzsiAPI.putcol(pInt, "standard", f.standard);
                GzsiAPI.putcol(pInt, "fee_date", f.fee_date);
                GzsiAPI.putcol(pInt, "unit", f.Unit);
                GzsiAPI.putcol(pInt, "price", f.Price);
                GzsiAPI.putcol(pInt, "dosage", "0");
                GzsiAPI.putcol(pInt, "money", "0");
                GzsiAPI.putcol(pInt, "opp_serial_fee", "0");

                GzsiAPI.putcol(pInt, "note", "");
                GzsiAPI.putcol(pInt, "old_money", f.Money);
                GzsiAPI.putcol(pInt, "reduce_money", "0");
                GzsiAPI.putcol(pInt, "stat_type", f.Stat_type);
                GzsiAPI.putcol(pInt, "fee_batch", bInfo.fee_batch);
                input_money = "-" + f.Money;
                GzsiAPI.putcol(pInt, "input_money", input_money);

                GzsiAPI.putcol(pInt, "cancel_flag", "1");


                GzsiAPI.putcol(pInt, "hos_serial", f.hos_serial);

                int ll_result = GzsiAPI.nextrow(pInt);
                if (ll_result < 0)
                {
                    this.errMsg = "医保API错误：费用信息传入失败";
                    return -1;
                }

            }
            if (GzsiAPI.run(pInt) < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg); ;
                errMsg = "错误：门诊退费失败！" + errMsg;
                return -1;
            }

            #region 屏蔽 测试用
            //获取返回结果
            //if (-1 == GzsiAPI.setresultset(pInt, "payinfo"))
            //{
            //    this.errMsg = "错误：获取门诊收费返回支付信息失败！";
            //    return -1;
            //}
            //do
            //{
            //    GzsiAPI.getbyname(pInt, "hospital_id", strValue);
            //    payInfo.hospital_id=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "serial_no", strValue);
            //    payInfo.serial_no=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "zyzje",strValue );
            //    payInfo.zyzje=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "sbzfje", strValue);
            //    payInfo.sbzfje=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "zhzfje",strValue );
            //    payInfo.zhzfje=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "bfxmzfje", strValue);
            //    payInfo.bfxmzfje=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "qfje",strValue );
            //    payInfo.qfje=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "grzfje1",strValue );
            //    payInfo.grzfje1=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "grzfje2", strValue);
            //    payInfo.grzfje2=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "grzfje3", strValue);
            //    payInfo.grzfje3=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "cxzfje",strValue );
            //    payInfo.cxzfje=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "yyfdje", strValue);
            //    payInfo.yyfdje=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "cash_pay_com",strValue );
            //    payInfo.cash_pay_com=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "acct_pay_com", strValue);
            //    payInfo.acct_pay_com=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "cash_pay_own", strValue);
            //    payInfo.cash_pay_own=strValue.ToString();
            //    GzsiAPI.getbyname(pInt, "acct_pay_own",strValue );
            //    payInfo.acct_pay_own=strValue.ToString();
            //} while (GzsiAPI.nextrow(pInt) > 0);

            #endregion
            CallSIProcessEnd();
            return 1;

        }
        #endregion //end 门诊退费

        #region 取消门诊业务

        /// <summary>
        /// 取消门诊业务
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        public int CancleSiRegister(FS.HISFC.Models.Registration.Register regObj)
        {
            NoSortHashtable ht = new NoSortHashtable();
            ht.Add("hospital_id", hospital_id);
            ht.Add("serial_no", regObj.SIMainInfo.RegNo);
            ht.Add("fin_staff", string.IsNullOrEmpty(regObj.InputOper.ID) ? "9999" : regObj.InputOper.ID);
            if (string.IsNullOrEmpty(regObj.InputOper.Name))
            {
                regObj.InputOper.Name = "收费员";
            }
            ht.Add("fin_man", regObj.InputOper.Name);


            if (-1 == CallSIProcessBegin("BIZH131105"))
            {
                return -1;
            }

            if (-1 == PutParams(ht))
            {
                return -1;
            }

            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg); ;
                return -1;
            }

            CallSIProcessEnd();
            return 1;

        }

        #endregion

        #region 私用方法

        private string GetItemType(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem)
        {
            if (feeItem.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                if (feeItem.Item.MinFee.ID == "001")
                {
                    return "1";
                }
                else if (feeItem.Item.MinFee.ID == "002")
                {
                    return "2";
                }
                else
                {
                    return "3";
                }

            }
            else
            {
                return "0";
            }
        }

        #endregion //end 私用方法

        #region 公用方法

        /// <summary>
        /// 提取计算结果
        /// </summary>
        /// <param name="serial_no"></param>
        /// <param name="payInfo"></param>
        /// <returns></returns>
        public int GetBalanceResult(string serial_no, ref PayInfo payInfo)
        {
            NoSortHashtable ht = new NoSortHashtable();
            ht.Add("hospital_id", this.hospital_id);
            ht.Add("serial_no", serial_no);
            ht.Add("pay_no", "0");
            if (-1 == CallSIProcessBegin("BIZH000106"))
            {
                return -1;
            }

            if (-1 == PutParams(ht))
            {
                return -1;
            }

            long result = GzsiAPI.run(pInt);
            if (result < 0)
            {
                GzsiAPI.getmessage(pInt, strErrMsg); ;
                return -1;
            }
            if (-1 == GzsiAPI.setresultset(pInt, "payinfo"))
            {
                this.errMsg = "错误：获取门诊收费返回支付信息失败！";
                return -1;
            }
            do
            {
                GzsiAPI.getbyname(pInt, "hospital_id", strValue);
                payInfo.hospital_id = strValue.ToString();
                GzsiAPI.getbyname(pInt, "serial_no", strValue);
                payInfo.serial_no = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pay_no", strValue);
                payInfo.pay_no = strValue.ToString();
                GzsiAPI.getbyname(pInt, "pay_date", strValue);
                payInfo.pay_date = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zyzje", strValue);
                payInfo.zyzje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "sbzfje", strValue);
                payInfo.sbzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "zhzfje", strValue);
                payInfo.zhzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "bfxmzfje", strValue);
                payInfo.bfxmzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "qfje", strValue);
                payInfo.qfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje1", strValue);
                payInfo.grzfje1 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje2", strValue);
                payInfo.grzfje2 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "grzfje3", strValue);
                payInfo.grzfje3 = strValue.ToString();
                GzsiAPI.getbyname(pInt, "cxzfje", strValue);
                payInfo.cxzfje = strValue.ToString();
                GzsiAPI.getbyname(pInt, "yyfdje", strValue);
                payInfo.yyfdje = strValue.ToString();
            } while (GzsiAPI.nextrow(pInt) > 0);

            CallSIProcessEnd();
            return 1;
        }

        /// <summary>
        /// 获取门诊结算单
        /// </summary>
        /// <param name="serial_no"></param>
        /// <returns></returns>
        public int GetOutPatientBalanceBill(string serial_no)
        {
            return 1;
        }

        /// <summary>
        /// 获取门慢结算单
        /// </summary>
        /// <param name="serial_no"></param>
        /// <returns></returns>
        public int GetOutPatientMmBalanceBill(string serial_no)
        {
            return 1;
        }

        #endregion
    }

}
