using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Collections;

namespace GZSI.Management
{
    /// <summary>
    /// 医保接口业务
    /// </summary>
    public class SIConnect : SIDatabase
    {
        #region 属性

        private string siConnectFile = string.Empty;
        /// <summary>
        /// 医保连接配置文件
        /// </summary>
        public string SiConnectFile
        {
            get { return siConnectFile;}
        }

        #endregion 

        public SIConnect()
        {
            siConnectFile = profileName;
        }

        public SIConnect(string strFile) : base(strFile)
        {
            this.siConnectFile = strFile;
        }

        #region 登记

        /// <summary>
        /// 通过就诊号,获得患者基本信息
        /// </summary>
        /// <param name="regNo">就诊医疗号</param>
        /// <returns>null 没有找到或者数据库出错 obj 患者登记信息实体</returns>
        public FS.HISFC.Models.RADT.PatientInfo GetRegPersonInfo(string regNo)
        {
            string sql = "select * from his_zydj where jydjh = '" + regNo + "'";

            if (this.ExecQuery(sql) == -1)
                return null;

            FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
            try
            {
                while (Reader.Read())
                {
                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    obj.Sex.ID = Reader[5].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.PID.PatientNO = Reader[9].ToString();
                    obj.PVisit.MedicalType.ID = Reader[10].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.InDiagnose.Name = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[14].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[15].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());
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

            return obj;
        }

        /// <summary>
        /// 获得某天的医保登记患者信息
        /// </summary>
        /// <param name="regDate">登记日期</param>
        /// <returns>null错误 ArrayList 包含患者登记信息的实体数组</returns>
        public ArrayList GetRegPersonInfo(DateTime regDate)
        {
            DateTime beginDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 0, 0, 0);
            DateTime endDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 23, 59, 59);
            string sql = "select * from his_zydj where RYRQ >= '" + beginDate + "'" + " and RYRQ <= '" + endDate + "'  ORDER BY JYDJH DESC";

            if (this.ExecQuery(sql) == -1)
                return null;

            ArrayList al = new ArrayList();
            try
            {
                while (Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();

                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    obj.Sex.ID = Reader[5].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.PID.PatientNO = Reader[9].ToString();
                    obj.PVisit.MedicalType.ID = Reader[10].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.InDiagnose.Name = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[14].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[15].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());

                    al.Add(obj);
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

            return al;
        }

        /// <summary>
        /// 获得某天的医保登记患者信息
        /// </summary>
        /// <param name="regDate">登记日期</param>
        ///  <param name="flag">1住院 2门特</param>
        /// <returns>null错误 ArrayList 包含患者登记信息的实体数组</returns>
        public ArrayList GetRegPersonInfo(DateTime regDate, string flag)
        {
            DateTime beginDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 0, 0, 0);
            DateTime endDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 23, 59, 59);
            string sql = "select * from his_zydj where RYRQ >= '" + beginDate + "'" + " and RYRQ <= '" + endDate + "' and JZLB = '" + flag + "'  ORDER BY JYDJH DESC";

            if (this.ExecQuery(sql) == -1)
                return null;

            ArrayList al = new ArrayList();

            try
            {
                while (Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();

                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    obj.Sex.ID = Reader[5].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.PID.PatientNO = Reader[9].ToString();
                    obj.PVisit.MedicalType.ID = Reader[10].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.InDiagnose.Name = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[14].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[15].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());

                    al.Add(obj);
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

            return al;
        }

        /// <summary>
        ///  更新入院登记的读入标志
        /// </summary>
        /// <param name="regNo">医保患者就医流水号</param>
        /// <param name="readFlag">更新的标志 1 读入 0 未读入 2 错误</param>
        /// <param name="commit">是否直接提交?</param>
        /// <returns>-1 失败 0 成功</returns>
        public int UpdateRegReadFlag(string regNo, int readFlag, bool commit)
        {
            string sql = "update his_zydj set DRBZ = " + readFlag.ToString() + " where jydjh = '" + regNo + "'";

            int tempRows = 0;

            tempRows = this.ExecNoQuery(sql);

            if (tempRows <= 0)
            {
                if (commit)
                {
                    trans.Rollback();
                }
                return -1;
            }

            try
            {
                if (commit)
                {
                    trans.Commit();
                }

                return tempRows;
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                //trans.Rollback();
                return -1;
            }

        }

        #endregion

        #region 医嘱明细操作

        /// <summary>
        /// 删除共享区上传的明细(结算召回用)
        /// </summary>
        /// <param name="regNo">就医登记号</param>
        /// <returns></returns>
        public int DeleteItemList(string regNo)
        {
            string strSql = "delete from his_cfxm where JYDJH = " + "'" + regNo + "' ";

            return this.ExecNoQuery(strSql);
        }

        public int DeleteItemListEX(string regNo)
        {
            string strSql1 = "delete from HIS_CFXM_JKSYBZ where JYDJH = " + "'" + regNo + "' ";
            return this.ExecNoQuery(strSql1);
        }

        /// <summary>
        /// 插入单条医嘱明细
        /// </summary>
        /// <param name="pInfo">住院患者基本信息,包括医保基本信息</param>
        /// <param name="obj">费用明细信息</param>
        /// <returns></returns>
        public int InsertItemList(FS.HISFC.Models.RADT.PatientInfo pInfo, FS.HISFC.Models.Fee.Inpatient.FeeItemList obj)
        {
            //if (string.IsNullOrEmpty(pInfo.SIMainInfo.HosNo))
            //{
            pInfo.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? pInfo.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            //}

            string sqlMaxNo = "select isnull(max(XMXH), 0) from his_cfxm where JYDJH = " + "'" + pInfo.SIMainInfo.RegNo + "'";
            int i = 1;

            if (this.ExecQuery(sqlMaxNo) == -1)
                return -1;

            while (Reader.Read())
            {
                i = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
            }

            Reader.Close();

            i++;

            decimal uploadprice = 0;
            if (obj.FTRate.OwnRate == 0)
            {
                obj.FTRate.OwnRate = 1;
            }
            //----------屏蔽 修改前
            // uploadprice = obj.Item.Price * obj.FTRate.OwnRate
            //if (uploadprice == 0 )
            //{
            //    uploadprice = obj.FT.TotCost / obj.Item.Qty;
            //}
            //-----------修改后

            decimal itemPrice = this.GetPrice(obj.Item);
            uploadprice = FS.FrameWork.Public.String.FormatNumber(itemPrice * obj.FTRate.OwnRate, 4);
            decimal totcost = FS.FrameWork.Public.String.FormatNumber(uploadprice * obj.Item.Qty, 2);
            if (uploadprice == 0)
            {
                uploadprice = FS.FrameWork.Public.String.FormatNumber(obj.FT.TotCost / obj.Item.Qty, 4);
            }
            //暂时添加判断,解决从化医保自己重新计算带来的四舍五入问题
            if (pInfo.Pact.ID == "J01")//从化医保
                if (FS.FrameWork.Public.String.FormatNumber(uploadprice * obj.Item.Qty, 2) != obj.FT.TotCost)
                {
                    // 数量=1 && 单价=金额
                    uploadprice = obj.FT.TotCost;
                    obj.Item.Qty = 1;
                    if (obj.FT.TotCost < 0)//单价不能为负数
                    {
                        uploadprice = obj.FT.TotCost * (-1);
                        obj.Item.Qty = -1;
                    }
                }
            //---------------------------
            if (obj.Item.Name.Length > 20)
            {
                obj.Item.Name = obj.Item.Name.Substring(1, 20);
            }

            //数据合法性判断主要针对数字型

            string sql = "insert into his_cfxm values('" + pInfo.SIMainInfo.RegNo + "','" +
                                                          pInfo.SIMainInfo.HosNo + "','" +
                                                          pInfo.IDCard + "','" +
                                                          pInfo.PID.PatientNO + "','" +
                                                          pInfo.PVisit.InTime.ToString() + "','" +
                // obj.FeeOper.OperTime.ToString() + "'," + //将费用时间改为记账时间
                                                          obj.ChargeOper.OperTime.ToString() + "'," +
                                                          i.ToString() + ",'" +
                                                          obj.Item.UserCode + "','" +
                                                          obj.Item.Name + "'," +
                                                          "0" + ",'" +
                                                          obj.Item.Specs + "','" +
                                                          "" + "'," +
                                                          (uploadprice).ToString() + "," +
                                                          obj.Item.Qty.ToString() + "," +
                                                          totcost.ToString() + ",'" +
                                                          "" + "','" + "" + "','" + "" + "'," + "0" + ",'" + "" + "')";
            if (this.ExecNoQuery(sql) == -1)
                return -1;

            //{67C4F998-C669-4509-A392-33B3156A2C42} 标志上传
            ////{FC86C316-F598-4724-B300-8320FAD72344} zhang-wx 2016-12-30 异地医保的才上传限制用药标志
            //if (pInfo.Pact.ID.Substring(0, 2).ToString() == "YD")
            //{
            if (!string.IsNullOrEmpty(obj.Order.Item.User01))   //obj.Order.OrdExtend.Extend3))
            {
                //int limitFlag = obj.Order.OrdExtend.Extend3 == "1" ? 1 : 0;
                int limitFlag = obj.Order.Item.User01 == "1" ? 1 : 0;

                string sql1 = "insert into HIS_CFXM_JKSYBZ values('" + pInfo.SIMainInfo.RegNo + "','" +
                                                                  pInfo.SIMainInfo.HosNo + "','" +
                                                                  pInfo.IDCard + "','" +
                                                                  pInfo.PID.PatientNO + "','" +
                                                                  i.ToString() + "','" +
                                                                  obj.Item.UserCode + "','" +
                                                                  obj.Item.Name + "'," +
                                                                  obj.Item.Qty.ToString() + "," +
                                                                  obj.FT.TotCost.ToString() + ",'" +
                                                                  limitFlag.ToString() + "','" +
                                                                  "" + "','" + "" + "','" +
                                                                  "" + "','" + "" + "')";
                if (this.ExecNoQuery(sql1) == -1)
                {
                    return -1;
                }
            }
            //}
            return 0;
        }

        /// <summary>
        /// 循环插入医嘱明细
        /// </summary>
        /// <param name="pInfo">患者基本信息,包括医保信息</param>
        /// <param name="itemList">患者费用明细实体数组</param>
        /// <returns></returns>
        public int InsertItemList(FS.HISFC.Models.RADT.PatientInfo pInfo, ArrayList itemList)
        {
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in itemList)
            {
                if (this.InsertItemList(pInfo, obj) == -1)
                    return -1;
            }
            return 0;
        }

        /// <summary>
        ///  根据读入标志 查询已传递的项目列表
        /// </summary>
        /// <param name="regNo">患者就医登记号</param>
        /// <param name="flag">0 未读入 1 读入 2 错误</param>
        /// <returns>Fee.Inpatient.FeeItemList实体集合</returns>
        public ArrayList GetUnValidItemList(string regNo, int flag)
        {
            string sql = "select * from his_cfxm where JYDJH = '" + regNo + "' and DRBZ = " + flag.ToString();

            if (this.ExecQuery(sql) == -1)
                return null;

            while (Reader.Read())
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList obj = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();


            }
            Reader.Close();

            return null;
        }

        #endregion

        #region 结算信息

        /// <summary>
        /// 得到医保患者的结算信息
        /// 医保结算的总金额 pInfo.SIMainInfo.TotCost
        /// 医保结算的帐户金额 pInfo.SIMainInfo.PayCost
        /// 医保结算的统筹金额 pInfo.SIMainInfo.PubCost
        /// 医保结算的自费金额 pInfo.SIMainInfo.OwnCost
        /// 其中 totcost = payCost + pubCost + ownCost;
        /// </summary>
        /// <param name="pInfo">患者基本信息,包括医保患者结算表的基本信息</param>
        /// <returns> -1 失败 0 没有结算信息 1 成功获取</returns>
        public int GetBalanceInfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            string sql = "select * from HIS_FYJS where JYDJH = '" + pInfo.SIMainInfo.RegNo + "'";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保患者结算信息失败!";
                return -1;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有患者结算信息";
                return 0;
            }

            while (Reader.Read())
            {
                pInfo.SIMainInfo.RegNo = Reader[0].ToString();
                pInfo.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                pInfo.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                pInfo.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
                pInfo.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
                pInfo.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());
                pInfo.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[10].ToString());
                pInfo.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                pInfo.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                pInfo.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                pInfo.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
                pInfo.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15].ToString());
                pInfo.SIMainInfo.Memo = Reader[16].ToString();
                pInfo.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());
                pInfo.SIMainInfo.User01 = Reader[18].ToString();
                pInfo.SIMainInfo.User02 = Reader[19].ToString();
                pInfo.SIMainInfo.User03 = Reader[20].ToString();
                pInfo.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[21].ToString());
            }

            Reader.Close();

            return 1;
        }

        /// <summary>
        /// 更新结算信息的读入标志
        /// </summary>
        /// <param name="regNo">患者就医登记号</param>
        /// <param name="readFlag"> 0 未读入 1 已读入 2 作废</param>
        /// <returns></returns>
        public int UpdateBalaceReadFlag(string regNo, int readFlag)
        {
            string sql = "update HIS_FYJS set DRBZ = " + readFlag.ToString() + " WHERE JYDJH = '" + regNo + "'";

            if (this.ExecNoQuery(sql) <= 0)
            {
                this.Err = "更新失败!";
                return -1;
            }

            return 0;
        }

        #endregion

        #region 对照信息

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 获得异地医保药品项目列表
        /// </summary>
        /// <returns></returns>
        public ArrayList GetYDSIDrugList()
        {
            string sql = "select MEDI_CODE,MEDI_ITEM_TYPE,MEDI_NAME,MODEL,CODE_PY,STAT_TYPE,MT_FLAG,STAPLE_FLAG, isnull(SELF_SCALE,0),RANGE "
                         + "from view_medi_out" +
                         " where VALID_FLAG = '1'  and MEDI_CODE in (select item_code from dbo.VIEW_MATCH_out) ";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有药品信息";
                return null;
            }

            ArrayList al = new ArrayList();

            FS.HISFC.Models.SIInterface.Item item = null;

            string sysClass = "";
            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    if (Reader[1].ToString() == "1")
                    {
                        sysClass = "X";
                    }
                    else
                    {
                        sysClass = "Z";
                    }
                    item.SysClass = sysClass;
                    item.Name = Reader[2].ToString();
                    item.DoseCode = Reader[3].ToString();
                    item.SpellCode = (Reader[4].ToString()).Length > 9 ? (Reader[4].ToString()).Substring(0, 10) : Reader[4].ToString();
                    item.FeeCode = Reader[5].ToString();
                    item.ItemType = Reader[6].ToString();
                    item.ItemGrade = Reader[7].ToString();
                    if (item.ItemGrade == "9")
                    {
                        item.ItemGrade = "3";
                    }
                    item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());

                    al.Add(item);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 获得异地医保非药品项目列表
        /// </summary>
        /// <returns></returns>
        public ArrayList GetYDSIUndrugList()
        {
            string sql = "select item_code ," + "'F" + "'" + " as sys_class, item_name,STAT_TYPE as fee_code, " +
                         "MT_FLAG as ITEM_TYPE, SELF_SCALE from view_item_out";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保非药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有非药品信息";
                return null;
            }

            ArrayList al = new ArrayList();

            FS.HISFC.Models.SIInterface.Item item = null;
            //			FS.HISFC.Models.Base.SpellCode sp = null;
            //			FS.HISFC.Management.Manager.Spell spell = new FS.HISFC.Management.Manager.Spell();
            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    item.SysClass = Reader[1].ToString();
                    item.Name = Reader[2].ToString();
                    //sp = (FS.HISFC.Models.Base.SpellCode)spell.Get(item.Name);
                    //if(sp != null)
                    //{
                    //	item.SpellCode = sp.SpellCode.Length > 9 ? sp.SpellCode.Substring(0,10) : sp.SpellCode;
                    //}
                    item.FeeCode = Reader[3].ToString();
                    item.ItemType = Reader[4].ToString();
                    item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    if (item.Rate == 0)
                    {
                        item.ItemGrade = "1";
                    }
                    else if (item.Rate == 1)
                    {
                        item.ItemGrade = "3";
                    }
                    else
                    {
                        item.ItemGrade = "2";
                    }

                    al.Add(item);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 获得医保药品项目列表
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSIDrugList()
        {
            string sql = "select MEDI_CODE,MEDI_ITEM_TYPE,MEDI_NAME,MODEL,CODE_PY,STAT_TYPE,MT_FLAG,STAPLE_FLAG,isnull(SELF_SCALE,0) "
                         + "from view_medi" +
                         " where VALID_FLAG = '1'";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有药品信息";
                return null;
            }

            ArrayList al = new ArrayList();

            FS.HISFC.Models.SIInterface.Item item = null;

            string sysClass = "";
            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    if (Reader[1].ToString() == "1")
                    {
                        sysClass = "X";
                    }
                    else
                    {
                        sysClass = "Z";
                    }
                    item.SysClass = sysClass;
                    item.Name = Reader[2].ToString();
                    item.DoseCode = Reader[3].ToString();
                    item.SpellCode = (Reader[4].ToString()).Length > 9 ? (Reader[4].ToString()).Substring(0, 10) : Reader[4].ToString();
                    item.FeeCode = Reader[5].ToString();
                    item.ItemType = Reader[6].ToString();
                    item.ItemGrade = Reader[7].ToString();
                    if (item.ItemGrade == "9")
                    {
                        item.ItemGrade = "3";
                    }
                    item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());

                    al.Add(item);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 获得异地医保药品项目
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.SIInterface.Item GetYDSIDrugItem(string medi_code)
        {
            string sql = @"select MEDI_CODE,MEDI_ITEM_TYPE,MEDI_NAME,MODEL,CODE_PY,STAT_TYPE,MT_FLAG,STAPLE_FLAG,isnull(SELF_SCALE,0) 
                         from view_medi_out where VALID_FLAG = '1' and medi_code='{0}' ";

            sql = string.Format(sql, medi_code);
            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有药品信息";
                return null;
            }

            FS.HISFC.Models.SIInterface.Item item = null;

            string sysClass = "";
            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    if (Reader[1].ToString() == "1")
                    {
                        sysClass = "X";
                    }
                    else
                    {
                        sysClass = "Z";
                    }
                    item.SysClass = sysClass;
                    item.Name = Reader[2].ToString();
                    item.DoseCode = Reader[3].ToString();
                    item.SpellCode = (Reader[4].ToString()).Length > 9 ? (Reader[4].ToString()).Substring(0, 10) : Reader[4].ToString();
                    item.FeeCode = Reader[5].ToString();
                    item.ItemType = Reader[6].ToString();
                    item.ItemGrade = Reader[7].ToString();
                    if (item.ItemGrade == "9")
                    {
                        item.ItemGrade = "3";
                    }
                    item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());

                }

                Reader.Close();

                return item;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 获得医保药品项目
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.SIInterface.Item GetSIDrugItem(string medi_code)
        {
            string sql = @"select MEDI_CODE,MEDI_ITEM_TYPE,MEDI_NAME,MODEL,CODE_PY,STAT_TYPE,MT_FLAG,STAPLE_FLAG,isnull(SELF_SCALE,0) 
                         from view_medi where VALID_FLAG = '1' and medi_code='{0}' ";

            sql = string.Format(sql, medi_code);
            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有药品信息";
                return null;
            }

            FS.HISFC.Models.SIInterface.Item item = null;

            string sysClass = "";
            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    if (Reader[1].ToString() == "1")
                    {
                        sysClass = "X";
                    }
                    else
                    {
                        sysClass = "Z";
                    }
                    item.SysClass = sysClass;
                    item.Name = Reader[2].ToString();
                    item.DoseCode = Reader[3].ToString();
                    item.SpellCode = (Reader[4].ToString()).Length > 9 ? (Reader[4].ToString()).Substring(0, 10) : Reader[4].ToString();
                    item.FeeCode = Reader[5].ToString();
                    item.ItemType = Reader[6].ToString();
                    item.ItemGrade = Reader[7].ToString();
                    if (item.ItemGrade == "9")
                    {
                        item.ItemGrade = "3";
                    }
                    item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());

                }

                Reader.Close();

                return item;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 获得医保非药品项目列表
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSIUndrugList()
        {
            string sql = "select item_code ," + "'F" + "'" + " as sys_class, item_name,STAT_TYPE as fee_code, " +
                         "MT_FLAG as ITEM_TYPE, SELF_SCALE from view_item";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保非药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有非药品信息";
                return null;
            }

            ArrayList al = new ArrayList();

            FS.HISFC.Models.SIInterface.Item item = null;
            //			FS.HISFC.Models.Base.SpellCode sp = null;
            //			FS.HISFC.Management.Manager.Spell spell = new FS.HISFC.Management.Manager.Spell();
            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    item.SysClass = Reader[1].ToString();
                    item.Name = Reader[2].ToString();
                    //					sp = (FS.HISFC.Models.Base.SpellCode)spell.Get(item.Name);
                    //					if(sp != null)
                    //					{
                    //						item.SpellCode = sp.SpellCode.Length > 9 ? sp.SpellCode.Substring(0,10) : sp.SpellCode;
                    //					}
                    item.FeeCode = Reader[3].ToString();
                    item.ItemType = Reader[4].ToString();
                    item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    if (item.Rate == 0)
                    {
                        item.ItemGrade = "1";
                    }
                    else if (item.Rate == 1)
                    {
                        item.ItemGrade = "3";
                    }
                    else
                    {
                        item.ItemGrade = "2";
                    }

                    al.Add(item);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 获得医保非药品项目
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.SIInterface.Item GetSIUndrugItem(string itemcode)
        {
            string sql = "select item_code ," + "'F" + "'" + " as sys_class, item_name,STAT_TYPE as fee_code, " +
                         "MT_FLAG as ITEM_TYPE, SELF_SCALE from view_item where item_code='{0}'";
            sql = string.Format(sql, itemcode);
            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保非药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有非药品信息";
                return null;
            }

            FS.HISFC.Models.SIInterface.Item item = null;

            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    item.SysClass = Reader[1].ToString();
                    item.Name = Reader[2].ToString();
                    //sp = (FS.HISFC.Models.Base.SpellCode)spell.Get(item.Name);
                    //if(sp != null)
                    //{
                    //   item.SpellCode = sp.SpellCode.Length > 9 ? sp.SpellCode.Substring(0,10) : sp.SpellCode;
                    //}
                    item.FeeCode = Reader[3].ToString();
                    item.ItemType = Reader[4].ToString();

                    //{7DA9F678-61A3-40bd-83A2-081CCC83FDE9} tang.ll 2015-7-23 中间表比例为空的项目，默认为自费项目
                    //item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    if (string.IsNullOrEmpty(Reader[5].ToString()))
                    {
                        item.Rate = 1;
                    }
                    else
                    {
                        item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    }
                    if (item.Rate == 0)
                    {
                        item.ItemGrade = "1";
                    }
                    else if (item.Rate == 1)
                    {
                        item.ItemGrade = "3";
                    }
                    else
                    {
                        item.ItemGrade = "2";
                    }

                }

                Reader.Close();

                return item;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 获得异地医保非药品项目
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.SIInterface.Item GetYDSIUndrugItem(string itemcode)
        {
            string sql = "select item_code ," + "'F" + "'" + " as sys_class, item_name,STAT_TYPE as fee_code, " +
                         "MT_FLAG as ITEM_TYPE, SELF_SCALE from view_item_out where item_code='{0}'";
            sql = string.Format(sql, itemcode);
            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保非药品目录失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有非药品信息";
                return null;
            }

            FS.HISFC.Models.SIInterface.Item item = null;

            try
            {
                while (Reader.Read())
                {
                    item = new FS.HISFC.Models.SIInterface.Item();

                    item.ID = Reader[0].ToString();
                    item.SysClass = Reader[1].ToString();
                    item.Name = Reader[2].ToString();
                    //sp = (FS.HISFC.Models.Base.SpellCode)spell.Get(item.Name);
                    //if(sp != null)
                    //{
                    //	 item.SpellCode = sp.SpellCode.Length > 9 ? sp.SpellCode.Substring(0,10) : sp.SpellCode;
                    //}
                    item.FeeCode = Reader[3].ToString();
                    item.ItemType = Reader[4].ToString();

                    //{7DA9F678-61A3-40bd-83A2-081CCC83FDE9} tang.ll 2015-7-23 中间表比例为空的项目，默认为自费项目
                    //item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    if (string.IsNullOrEmpty(Reader[5].ToString()))
                    {
                        item.Rate = 1;
                    }
                    else
                    {
                        item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    }
                    if (item.Rate == 0)
                    {
                        item.ItemGrade = "1";
                    }
                    else if (item.Rate == 1)
                    {
                        item.ItemGrade = "3";
                    }
                    else
                    {
                        item.ItemGrade = "2";
                    }

                }

                Reader.Close();

                return item;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 由医保服务器获取医保已对照信息
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSICompareList()
        {
            string sql = "select item_code,item_name,match_type,hosp_code,hosp_name from view_match order by match_type";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保已对照信息失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "无已对照信息";
                return null;
            }

            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject info;
            try
            {
                while (Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();
                    info.Name = this.Reader[1].ToString();
                    info.Memo = this.Reader[2].ToString();
                    info.User01 = this.Reader[3].ToString();
                    info.User02 = this.Reader[4].ToString();
                    al.Add(info);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 由医保服务器获取医保已对照信息(异地医保)
        /// {FC86C316-F598-4724-B300-8320FAD72344} zhang-wx 2016-12-30 增加了异地医保的对照信息查询
        /// </summary>
        /// <returns></returns>
        public ArrayList GetYDSICompareList()
        {
            string sql = "select item_code,item_name,match_type,hosp_code,hosp_name from view_match_out order by match_type";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保已对照信息失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "无已对照信息";
                return null;
            }

            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject info;
            try
            {
                while (Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();
                    info.Name = this.Reader[1].ToString();
                    info.Memo = this.Reader[2].ToString();
                    info.User01 = this.Reader[3].ToString();
                    info.User02 = this.Reader[4].ToString();
                    al.Add(info);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 根据自定义码获取异地医保服务器的已对照信息
        /// </summary>
        /// <returns></returns>
        public Hashtable GetYDSICompareHashTable()
        {
            string sql = "select item_code,item_name,match_type,hosp_code,hosp_name from view_match_out order by match_type";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保已对照信息失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "无已对照信息";
                return null;
            }

            Hashtable ht = new Hashtable();
            FS.FrameWork.Models.NeuObject info;
            try
            {
                while (Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString().Trim();
                    info.Name = this.Reader[1].ToString();
                    info.Memo = this.Reader[2].ToString();
                    info.User01 = this.Reader[3].ToString();
                    info.User02 = this.Reader[4].ToString();
                    if (!ht.Contains(info.User01.Trim()))
                    {
                        ht.Add(info.User01.Trim(), info);
                    }
                }

                Reader.Close();

                return ht;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 根据自定义码获取医保服务器的已对照信息
        /// </summary>
        /// <returns></returns>
        public Hashtable GetSICompareHashTable()
        {
            string sql = "select item_code,item_name,match_type,hosp_code,hosp_name from view_match order by match_type";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保已对照信息失败!";
                return null;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "无已对照信息";
                return null;
            }

            Hashtable ht = new Hashtable();
            FS.FrameWork.Models.NeuObject info;
            try
            {
                while (Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString().Trim();
                    info.Name = this.Reader[1].ToString();
                    info.Memo = this.Reader[2].ToString();
                    info.User01 = this.Reader[3].ToString();
                    info.User02 = this.Reader[4].ToString();
                    if (!ht.Contains(info.User01.Trim()))
                    {
                        ht.Add(info.User01.Trim(), info);
                    }
                }

                Reader.Close();

                return ht;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 获得非药品行数，判断是否需要更新用
        /// </summary>
        /// <returns></returns>
        public int GetSIUndrugCounts()
        {
            string sql = "select count(*) from view_item where VALID_FLAG = '1'";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保非药品目录失败!";
                return -1;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有非药品信息";
                return -1;
            }

            int count = 0;
            try
            {
                while (Reader.Read())
                {
                    count = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                }

                Reader.Close();

                return count;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return -1;
            }
        }

        /// <summary>
        /// 获得药品行数，判断是否需要更新用
        /// </summary>
        /// <returns></returns>
        public int GetSIDrugCounts()
        {
            string sql = "select count(*) from view_medi where VALID_FLAG = '1'";

            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保药品目录失败!";
                return -1;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有药品信息";
                return -1;
            }

            int count = 0;
            try
            {
                while (Reader.Read())
                {
                    count = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                }

                Reader.Close();

                return count;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return -1;
            }
        }

        #endregion

        #region 医保数据库操作(门诊)

        #region 明细操作
        /// <summary>
        /// 删除共享区上传的明细(结算召回用)
        /// </summary>
        /// <param name="regNo">就医登记号</param>
        /// <returns>-1 失败 0 没有找到行 1 成功!</returns>
        public int DeleteItemListClinic(string regNo)
        {
            string strSql = "delete from HIS_MZXM where JYDJH = " + "'" + regNo + "'";

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除共享区上传的明细限制性用药标记(结算召回用)
        /// </summary>
        /// <param name="regNo">就医登记号</param>
        /// <returns>-1 失败 0 没有找到行 1 成功!</returns>
        public int DeleteItemListIndicationsClinic(string regNo)
        {
            string strSql = "delete from HIS_CFXM_JKSYBZ where JYDJH = " + "'" + regNo + "'";

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 是否需要上传的数据
        /// </summary>
        /// <param name="fItem"></param>
        /// <returns></returns>
        public bool IsUpLoad(FS.HISFC.Models.Base.Item item)
        {
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                if (undrug.SpecialPrice <= 0)
                {
                    return false;
                }
            }
            else if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                //if (phaItem.SpecialPrice <= 0)
                //{
                //    return false;
                //}
            }
            return true;
        }

        /// <summary>
        /// 插入多条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareData(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, DateTime operDate)
        {
            int iReturn = 0;
            int uploadRows = 0;
            try
            {
                int seq = 1;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    #region 处理只有对照过的才上传

                    if (!IsUpLoad(f.Item))
                    {
                        continue;
                    }

                    #endregion
                    iReturn = InsertShareData(r, f, seq, operDate);
                    if (iReturn <= 0)
                    {
                        this.Err += "插入明细失败!";
                        return -1;
                    }

                    //处理限制用药信息
                    if (!string.IsNullOrEmpty(f.Item.User03))
                    {
                        iReturn = this.InsertIndicationsShareData(r, f, operDate);
                        if (iReturn <= 0)
                        {
                            this.Err += "插入医保限制性用药明细失败!";
                            return -1;
                        }
                    }
                    seq += 1;
                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return uploadRows;
        }

        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, int seq,
            DateTime operDate)
        {
            //if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            //{
            r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            //}

            string strSQL = "INSERT INTO HIS_MZXM (JYDJH, YYBH, GMSFHM, ZYH, RYRQ, FYRQ, XMXH, XMBH, XMMC, " +
                "FLDM, YPGG, YPJX, JG, MCYL, JE, BZ1, BZ2, BZ3, DRBZ, YPLY)" +
                "VALUES('{0}','{1}','{2}','{3}',CONVERT(datetime,'{4}'),CONVERT(datetime,'{5}'), " +
                "{6},'{7}','{8}',{9},'{10}','{11}',{12},{13},'{14}','{15}','{16}','{17}',{18},'{19}')";
            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }
            try
            {
                decimal price = this.GetPrice(f.Item);
                decimal totCost = f.Item.Qty * price / f.Item.PackQty;

                if (!string.IsNullOrEmpty(f.Order.ID))
                {
                    seq = FS.FrameWork.Function.NConvert.ToInt32(f.Order.ID);
                }
                if (seq == 0)
                {
                    seq = FS.FrameWork.Function.NConvert.ToInt32(f.Order.SequenceNO);
                }

                strSQL = string.Format(strSQL,
                    r.SIMainInfo.RegNo,
                    r.SIMainInfo.HosNo,
                    r.IDCard,
                    r.PID.CardNO,
                    r.DoctorInfo.SeeDate.ToString(),
                    operDate.ToString(),
                    seq.ToString(),
                    f.Item.UserCode,
                    name,
                    "000",
                    f.Item.Specs,
                    r.MainDiagnose, //诊断
                    FS.FrameWork.Public.String.FormatNumber(price / f.Item.PackQty, 4),
                    f.Item.Qty,                 //总量Amount
                    totCost,//(f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString(),
                    "", "", "", "0", ""
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入单条适应症信息
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertIndicationsShareData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f,
            DateTime operDate)
        {
            decimal price = this.GetPrice(f.Item);
            string sql1 = "insert into HIS_CFXM_JKSYBZ values('" + r.SIMainInfo.RegNo + "','" +
                                                              r.SIMainInfo.HosNo + "','" +
                                                              r.IDCard + "','" +
                                                                "','" +
                                                              f.Order.ID.ToString() + "','" +
                                                              f.Item.UserCode + "','" +
                                                              f.Item.Name + "'," +
                                                              f.Item.Qty.ToString() + "," +
                                                              f.FT.TotCost.ToString() + ",'" +
                                                              f.Item.User03.ToString() + "','" +
                                                              "" + "','" + "" + "','" +
                                                              "" + "','" + "" + "')";
            if (this.ExecNoQuery(sql1) == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 插入多条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareDataInpatient(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, DateTime operDate)
        {

            int iReturn = 0;
            int uploadRows = 0;
            try
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    #region 处理只有对照过的才上传

                    if (!IsUpLoad(f.Item))
                    {
                        continue;
                    }

                    #endregion

                    iReturn = InsertShareDataInpatient(r, f, operDate);
                    if (iReturn <= 0)
                    {
                        this.Err += "插入明细失败!";
                        return -1;
                    }
                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return uploadRows;
        }

        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareDataInpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f,
            DateTime operDate)
        {
            //if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            //{
            r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            //}
            //r.SIMainInfo.HosNo = GZSI.HosCode;
            string sqlMaxNo = "select isnull(max(XMXH), 0) from his_cfxm where JYDJH = " + "'" + r.SIMainInfo.RegNo + "'";
            int i = 1;

            if (this.ExecQuery(sqlMaxNo) == -1)
            {
                this.Err = "获得项目序号出错!";
                return -1;
            }
            while (Reader.Read())
            {
                i = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
            }

            Reader.Close();

            i++;

            //string sqlHaveInsert = "select count(*) from his_cfxm where JYDJH = " + "'" + r.SIMainInfo.RegNo + "' and XMBH= " + "'" + f.Item.UserCode + "'";
            //if (this.ExecQuery(sqlHaveInsert) == -1)
            //{
            //    this.Err = "获得插入行数出错!";
            //    return -1;
            //}
            //while (Reader.Read())
            //{
            //    int row = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
            //    if (row > 0)
            //    {
            //        Reader.Close();
            //        return 1;//已经存在了
            //    }
            //}
            //Reader.Close();

            string strSQL = "INSERT INTO his_cfxm (JYDJH, YYBH, GMSFHM, ZYH, RYRQ, FYRQ, XMXH, XMBH, XMMC, " +
                "FLDM, YPGG, YPJX, JG, MCYL, JE, BZ1, BZ2, BZ3, DRBZ, YPLY)" +
                "VALUES('{0}','{1}','{2}','{3}',CONVERT(datetime,'{4}'),CONVERT(datetime,'{5}'), " +
                "{6},'{7}','{8}',{9},'{10}','{11}',{12},{13},'{14}','{15}','{16}','{17}',{18},'{19}')";
            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }

            try
            {
                decimal itemPrice = this.GetPrice(f.Item);
                strSQL = string.Format(strSQL, r.SIMainInfo.RegNo,
                                    r.SIMainInfo.HosNo,
                                    r.IDCard,
                                    r.PID.CardNO,
                                    r.DoctorInfo.SeeDate.ToString(),
                                    operDate.ToString(),
                                    i.ToString(),
                                    f.Item.UserCode,
                                    name,
                                    "000",
                                    f.Item.Specs,
                                    r.MainDiagnose,
                                    FS.FrameWork.Public.String.FormatNumber(itemPrice / f.Item.PackQty, 4),
                                    f.Item.Qty,
                                    (f.FT.TotCost).ToString(),
                                    "", "", "", "0", "");
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region 挂号信息操作

        /// <summary>
        /// 获得指定日期的所有患者信息
        /// </summary>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public ArrayList GetRegInfo(DateTime regDate)
        {
            string sql = "select JYDJH, YYBH, GMSFHM, XM, DWMC, XB, CSRQ, RYLB, " +
                "GWYJB, ZYH, JZLB, RYRQ, RYZD, RYZDGJDM, BQDM, CWDH,TZDXSPH, BZ1, BZ2, BZ3, DRBZ " +
                "from HIS_MZDJ where RYRQ >= '" + regDate.Date + "'" + " and RYRQ < '" + regDate.Date.AddDays(1) + "' ORDER BY JYDJH DESC";

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            ArrayList alTemp = new ArrayList();
            FS.HISFC.Models.Registration.Register obj = null;

            try
            {
                while (Reader.Read())
                {
                    obj = new FS.HISFC.Models.Registration.Register();

                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.SIMainInfo.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    if (Reader[5].ToString() == "1")
                    {
                        obj.Sex.ID = "M";
                    }
                    else if (Reader[5].ToString() == "2")
                    {
                        obj.Sex.ID = "F";
                    }
                    else
                    {
                        obj.Sex.ID = "U";
                    }
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());

                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.SIMainInfo.User02 = Reader[9].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[10].ToString();

                    //obj.RegDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.ClinicDiagNose = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());

                    alTemp.Add(obj);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            return alTemp;
        }

        /// <summary>
        /// 获得指定日期的所有患者信息
        /// </summary>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public ArrayList GetInPatientRegInfo(DateTime regDate)
        {
            string sql = "select JYDJH, YYBH, GMSFHM, XM, DWMC, XB, CSRQ, RYLB, " +
                "GWYJB, ZYH, JZLB, RYRQ, RYZD, RYZDGJDM, BQDM, CWDH,TZDXSPH, BZ1, BZ2, BZ3, DRBZ " +
                "from HIS_ZYDJ where RYRQ >= '" + regDate.Date + "'" + " and RYRQ < '" + regDate.Date.AddDays(1) + "'";

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            ArrayList alTemp = new ArrayList();
            FS.HISFC.Models.Registration.Register obj = null;

            try
            {
                while (Reader.Read())
                {
                    obj = new FS.HISFC.Models.Registration.Register();

                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.SIMainInfo.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    if (Reader[5].ToString() == "1")
                    {
                        obj.Sex.ID = "M";
                    }
                    else if (Reader[5].ToString() == "2")
                    {
                        obj.Sex.ID = "F";
                    }
                    else
                    {
                        obj.Sex.ID = "U";
                    }
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());

                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.SIMainInfo.User02 = Reader[9].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[10].ToString();

                    //obj.RegDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.ClinicDiagNose = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());

                    alTemp.Add(obj);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据身份证件号码取得最新就医登记号
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        public string GetNewRegNo(string idNo)
        {
            string sql = "select JYDJH from his_mzdj where gmsfhm = '" + idNo + "'" + " order by bz1 desc";

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            string tmpNewRegNo = "";

            try
            {
                while (Reader.Read())
                {
                    tmpNewRegNo = Reader[0].ToString();
                    break;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            return tmpNewRegNo;
        }

        /// <summary>
        /// 根据身份证件号码取得最新就医登记号
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        public string GetNewRegNoInpatient(string idNo)
        {
            string sql = "select JYDJH from his_zydj where gmsfhm = '" + idNo + "'" + " order by bz1 desc";

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            string tmpNewRegNo = "";

            try
            {
                while (Reader.Read())
                {
                    tmpNewRegNo = Reader[0].ToString();
                    break;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            return tmpNewRegNo;
        }

        /// <summary>
        /// 根据身份证号查询患者挂号信息
        /// </summary>
        /// <param name="idNo">身份证号</param>
        /// <returns>null 失败 ArrayList.Count = 0 没有查询到数据 ArrayList.Count >= 1成功</returns>
        public ArrayList GetRegInfo(string idNo)
        {
            string sql = "select JYDJH, YYBH, GMSFHM, XM, DWMC, XB, CSRQ, RYLB, " +
                "GWYJB, ZYH, JZLB, RYRQ, RYZD, RYZDGJDM, BQDM, CWDH,TZDXSPH, BZ1, BZ2, BZ3, DRBZ " +
                "from HIS_MZDJ where GMSFHM = '" + idNo + "'";

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            ArrayList alTemp = new ArrayList();
            FS.HISFC.Models.Registration.Register obj = null;

            try
            {
                while (Reader.Read())
                {

                    obj = new FS.HISFC.Models.Registration.Register();

                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.SIMainInfo.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    if (Reader[5].ToString() == "1")
                    {
                        obj.Sex.ID = "M";
                    }
                    else if (Reader[5].ToString() == "2")
                    {
                        obj.Sex.ID = "F";
                    }
                    else
                    {
                        obj.Sex.ID = "U";
                    }
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());

                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.SIMainInfo.User02 = Reader[9].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[10].ToString();

                    //obj.RegDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.ClinicDiagNose = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());

                    alTemp.Add(obj);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据就医登记号获得患者挂号信息
        /// </summary>
        /// <param name="regNo">就医登记号</param>
        /// <returns>null 失败 obj成功</returns>
        public int GetRegInfo(string regNo, ref FS.HISFC.Models.Registration.Register obj)
        {
            string sql = "select JYDJH, YYBH, GMSFHM, XM, DWMC, XB, CSRQ, RYLB, " +
                         "GWYJB, ZYH, JZLB, RYRQ, RYZD, RYZDGJDM, BQDM, CWDH,TZDXSPH, BZ1, BZ2, BZ3, DRBZ " +
                         "from HIS_MZDJ where jydjh = '" + regNo + "'";

            if (this.ExecQuery(sql) == -1)
            {
                return -1;
            }
            int iReturn = 0;

            try
            {
                while (Reader.Read())
                {
                    iReturn++;
                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.SIMainInfo.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    if (Reader[5].ToString() == "1")
                    {
                        obj.Sex.ID = "M";
                    }
                    else if (Reader[5].ToString() == "2")
                    {
                        obj.Sex.ID = "F";
                    }
                    else
                    {
                        obj.Sex.ID = "U";
                    }
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());

                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.SIMainInfo.User02 = Reader[9].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[10].ToString();

                    //obj.RegDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.ClinicDiagNose = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }

            return iReturn;
        }

        /// <summary>
        /// 根据就医登记号获得患者挂号信息
        /// </summary>
        /// <param name="regNo">就医登记号</param>
        /// <returns>null 失败 obj成功</returns>
        public int GetRegInfoFromInpatient(string regNo, ref FS.HISFC.Models.Registration.Register obj)
        {
            string sql = "select JYDJH, YYBH, GMSFHM, XM, DWMC, XB, CSRQ, RYLB, " +
                "GWYJB, ZYH, JZLB, RYRQ, RYZD, RYZDGJDM, BQDM, CWDH,TZDXSPH, BZ1, BZ2, BZ3, DRBZ " +
                "from HIS_ZYDJ where jydjh = '" + regNo + "'";

            if (this.ExecQuery(sql) == -1)
            {
                return -1;
            }
            int iReturn = 0;

            try
            {
                while (Reader.Read())
                {
                    iReturn++;
                    obj.SIMainInfo.RegNo = Reader[0].ToString();
                    obj.SIMainInfo.HosNo = Reader[1].ToString();
                    obj.SIMainInfo.ID = Reader[0].ToString();
                    obj.IDCard = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.SIMainInfo.Name = Reader[3].ToString();
                    obj.CompanyName = Reader[4].ToString();
                    if (Reader[5].ToString() == "1")
                    {
                        obj.Sex.ID = "M";
                    }
                    else if (Reader[5].ToString() == "2")
                    {
                        obj.Sex.ID = "F";
                    }
                    else
                    {
                        obj.Sex.ID = "U";
                    }
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());

                    obj.SIMainInfo.EmplType = Reader[7].ToString();
                    obj.SIMainInfo.User01 = Reader[8].ToString();
                    obj.SIMainInfo.User02 = Reader[9].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[10].ToString();

                    //obj.RegDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.ClinicDiagNose = Reader[12].ToString();
                    obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
                    obj.User01 = Reader[17].ToString();
                    obj.User02 = Reader[18].ToString();
                    obj.User03 = Reader[19].ToString();
                    obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }

            return iReturn;
        }

        #endregion

        #region 结算信息操作

        /// <summary>
        /// 获得结算信息
        /// </summary>
        /// <param name="r">挂号实体</param>
        /// <returns>-1 失败 0 没有信息 1 成功</returns>
        public int GetBalanceInfo(FS.HISFC.Models.Registration.Register r)
        {
            string strSQL = "SELECT JYDJH, FYPC, YYBH, GMSFHM, ZYH, RYRQ, JSRQ, ZYZJE, SBZFJE, " +
                "ZHZFJE, BFXMZFJE, QFJE, GRZFJE1, GRZFJE2, GRZFJE3, CXZFJE, YYFDJE, ZFYY, BZ1, BZ2, BZ3, DRBZ FROM HIS_MZJS WHERE JYDJH = '" + r.SIMainInfo.RegNo + "'" +
                " and fypc = (select max(fypc) from his_mzjs where jydjh = '" + r.SIMainInfo.RegNo + "' )";

            if (this.ExecQuery(strSQL) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保患者结算信息失败!";
                return -1;
            }

            if (!Reader.HasRows)
            {
                this.Reader.Close();
                this.ErrCode = "-1";
                this.Err = "没有患者结算信息";
                return 0;
            }

            while (Reader.Read())
            {
                r.SIMainInfo.RegNo = Reader[0].ToString();
                r.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                r.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
                r.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
                r.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());
                r.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[10].ToString());
                r.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                r.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                r.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                r.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
                r.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15].ToString());
                r.SIMainInfo.Memo = Reader[16].ToString();
                r.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());
                r.SIMainInfo.User01 = Reader[18].ToString();
                r.SIMainInfo.User02 = Reader[19].ToString();
                r.SIMainInfo.User03 = Reader[20].ToString();
                r.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[21].ToString());
            }

            Reader.Close();

            return 1;
        }

        /// <summary>
        /// 获得结算信息
        /// </summary>
        /// <param name="r">挂号实体</param>
        /// <returns>-1 失败 0 没有信息 1 成功</returns>
        public int GetBalanceInfoInpatient(FS.HISFC.Models.Registration.Register r)
        {
            string strSQL = "SELECT JYDJH, FYPC, YYBH, GMSFHM, ZYH, RYRQ, JSRQ, ZYZJE, SBZFJE, " +
                "ZHZFJE, BFXMZFJE, QFJE, GRZFJE1, GRZFJE2, GRZFJE3, CXZFJE, YYFDJE, ZFYY, BZ1, BZ2, BZ3, DRBZ FROM HIS_FYJS WHERE JYDJH = '" + r.SIMainInfo.RegNo + "'" +
                " and fypc = (select max(fypc) from HIS_FYJS where jydjh = '" + r.SIMainInfo.RegNo + "' )";

            if (this.ExecQuery(strSQL) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保患者结算信息失败!";
                return -1;
            }

            if (!Reader.HasRows)
            {

                this.ErrCode = "-1";
                this.Err = "没有患者结算信息";
                Reader.Close();
                return 0;
            }

            while (Reader.Read())
            {
                r.SIMainInfo.RegNo = Reader[0].ToString();
                r.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                r.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
                r.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
                r.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString()); //r.SIMainInfo.TotCost - r.SIMainInfo.PubCost;//
                r.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[10].ToString());
                r.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                r.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                r.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                r.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
                r.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15].ToString());
                r.SIMainInfo.Memo = Reader[16].ToString();
                r.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());
                r.SIMainInfo.User01 = Reader[18].ToString();
                r.SIMainInfo.User02 = Reader[19].ToString();
                r.SIMainInfo.User03 = Reader[20].ToString();
                r.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[21].ToString());
            }

            Reader.Close();

            return 1;
        }

        /// <summary>
        /// 获得结算信息
        /// </summary>
        /// <param name="r">挂号实体</param>
        /// <returns>-1 失败 0 没有信息 1 成功</returns>
        public int GetBalanceInfoInpatient(FS.HISFC.Models.RADT.PatientInfo r)
        {
            string strSQL = "SELECT JYDJH, FYPC, YYBH, GMSFHM, ZYH, RYRQ, JSRQ, ZYZJE, SBZFJE, " +
                "ZHZFJE, BFXMZFJE, QFJE, GRZFJE1, GRZFJE2, GRZFJE3, CXZFJE, YYFDJE, ZFYY, BZ1, BZ2, BZ3, DRBZ FROM HIS_FYJS WHERE JYDJH = '" + r.SIMainInfo.RegNo + "'" +
                " and fypc = (select max(fypc) from HIS_FYJS where jydjh = '" + r.SIMainInfo.RegNo + "' )";

            if (this.ExecQuery(strSQL) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保患者结算信息失败!";
                return -1;
            }

            if (!Reader.HasRows)
            {

                this.ErrCode = "-1";
                this.Err = "没有患者结算信息";
                Reader.Close();
                return 0;
            }

            while (Reader.Read())
            {
                r.SIMainInfo.RegNo = Reader[0].ToString();
                r.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                r.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
                r.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
                r.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString()); //r.SIMainInfo.TotCost - r.SIMainInfo.PubCost;//
                r.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[10].ToString());
                r.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                // r.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                r.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());//存自付金额
                r.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                r.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
                r.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15].ToString());
                r.SIMainInfo.Memo = Reader[16].ToString();
                r.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());
                r.SIMainInfo.User01 = Reader[18].ToString();
                r.SIMainInfo.User02 = Reader[19].ToString();
                r.SIMainInfo.User03 = Reader[20].ToString();
                r.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[21].ToString());
            }

            Reader.Close();

            return 1;
        }

        #endregion

        #endregion

        #region 处理价格

        /// <summary>
        /// 获得上传的价格
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private decimal GetPrice(FS.HISFC.Models.Base.Item item)
        {
            decimal price = item.Price;
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                //if (undrug.SpecialPrice > 0)
                //{
                    price = undrug.SpecialPrice;
                //}
            }
            else if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                if (phaItem.SpecialPrice > 0)
                {
                    price = phaItem.SpecialPrice;
                }
            }
            if (price == 0)
            {
                price = item.Price;
            }
            return price;
        }

        #endregion
    }
}
