using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OADAL
{
    public class OADataDeal
    {

        public void InsertFormList(List<FormInfo> list)
        {
            string delsql = "delete oaforminfo";
            OracleHelper.ExecuteNonQuery(delsql);
            string sql = @"insert into oaforminfo
  (id, formname, typename, useversionid, versionid, vcount, datacount, startdate, enddate)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";
            foreach (FormInfo model in list)
            {
                if (model.versionDefinition != null && model.versionDefinition.Count > 0)
                {
                    foreach (versionInfo item in model.versionDefinition)
                    {
                        string sql1 = string.Format(sql, model.definitionVersionsId, model.formName, model.typeName, model.versionDefinition[model.versionDefinition.Count - 1].ID, item.ID, item.versions, item.count.ToString(), item.createdTime, item.endTime);
                        int i = OracleHelper.ExecuteNonQuery(sql1);
                    }
                }
                else
                {
                    string sql1 = string.Format(sql, model.definitionVersionsId, model.formName, model.typeName, model.definitionVersionsId, model.definitionVersionsId, 1, model.count.ToString(), "", "");
                    int i = OracleHelper.ExecuteNonQuery(sql1);
                    // sql = string.Format(sql, model.definitionVersionsId, model.formName, model.typeName, model.versionDefinition[model.versions - 1].ID, item.ID, item.versions, item.count, item.createdTime, item.endTime);
                }
                //
            }
        }
        /// <summary>
        /// 费用报销申请
        /// </summary>
        /// <param name="list"></param>
        public void InsertExpenseFee(List<Dictionary<string, List<string>>> list)
        {
            LogHelper.Write("进入插入数据阶段 要插入数据" + list.Count);
            int j = 0;
            foreach (Dictionary<string, List<string>> dic in list)
            {
                string sqlFind = "select * from oa_fee_expense where id='{0}' ";
                sqlFind = string.Format(sqlFind, dic["Id"][0]);
                DataTable dt = OracleHelper.ExecuteDataTable(sqlFind);
                if (dt != null && dt.Rows.Count > 0) continue;
                string no = dic["Id"][0];
                if (dic.Keys.Contains("总支付方式"))
                {
                    if (dic["总支付方式"][0] == "现金")
                    {
                        if (dic.Keys.Contains("总付款银行"))//
                            dic["总付款银行"][0] = "库存现金";
                        else
                        {
                            if (dic.Keys.Contains("总转账银行"))
                                dic.Add("总付款银行", dic["总转账银行"]);
                        }
                    }
                }
                else
                {
                    if (dic.Keys.Contains("总付款方式") && dic["总付款方式"][0] == "现金")
                    {
                        dic["总付款方式"][0] = "库存现金";
                        dic.Add("总支付方式", dic["总付款方式"]);
                    }
                    else if (dic.Keys.Contains("总单项选择"))
                    {
                        if (dic["总单项选择"][0] == "现金")
                        {
                            dic["总单项选择"][0] = "库存现金";
                        }
                        dic.Add("总支付方式", dic["单项选择"]);
                    }
                    else if (dic.Keys.Contains("总付款方式"))
                    {
                        if (dic["总付款方式"][0] == "现金")
                        {
                            dic["总付款方式"][0] = "库存现金";
                        }
                        dic.Add("总支付方式", dic["总付款方式"]);
                    }
                }
                if (!dic.Keys.Contains("总付款金额"))
                {
                    dic.Add("总付款金额", dic["总转账金额"]);
                }
                if (!dic.Keys.Contains("总付款银行"))
                {
                    dic.Add("总付款银行", dic["总转账银行"]);
                }
                string sql = @"insert into oa_fee_expense (ID, APPLYPERSONNAME, POSITIONNAME, EXPENSEDATE, FEEDESCRIBE, FEEUSEDEPT, BILLNUM, APPLYFEETOTLE, LENDFEE, EXPENSEFEE, CHECKFEE, CREATEDATE, HOSPITALID, FEETYPE, MEMO,CLOSEPERSON,CLOSETIME,instanceTitle,feetypeID,cwfeetotal,cwpaymant,cwbankname,seq)
values ('{0}', '{1}', '{2}', to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'), '{4}', '{5}', {6}, {7}, {8}, {9}, {10}, to_date('{11}', 'yyyy-mm-dd hh24:mi:ss'), '{12}', '{13}', '{14}','{15}', to_date('{16}', 'yyyy-mm-dd hh24:mi:ss'),'{17}','{18}','{19}','{20}','{21}',{22})";
                //申请，职位，钱使用日期，费用描述，费用承担部门，票据数量。申请报销总金额，已借支金额，剩余报销金额，财务核定金额，申请日期，医院，费用类型 备注
                try
                {
                    sql = string.Format(sql, no, dic["personName"][0], dic["职位"][0], dic["总日期"][0], dic["总费用描述"][0], dic["总费用承担部门"][0],
                        dic["总附单据数"][0], dic["总申报金额（元）："][0], dic["总已借金额（元）："][0], dic["总剩余报销金额（元）："][0], dic["总财务核定金额（元）"][0] == "" ? "0" : dic["总财务核定金额（元）"][0], dic["createTime"][0], dic["HospitalID"][0], dic["FormType"][0], "", dic["closePerson"][0], dic["closeTime"][0], dic["instanceTitle"][0], dic["FormTypeID"][0], dic["总付款金额"][0], dic["总支付方式"][0], dic["总付款银行"][0], j + 1);
                    int i = 0;

                    i = OracleHelper.ExecuteNonQuery(sql);
                    j++;
                    LogHelper.WriteInsertSuceess(sql);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteInsertFail("异常sql:" + sql + "\r\n" + ex.Message);
                }
            }

            LogHelper.Write("插入费用报销完成,插入数据" + j);
        }


        /// <summary>
        /// 费用借支申请
        /// </summary>
        /// <param name="list"></param>
        //        public void InsertLoanFee(List<Dictionary<string, List<string>>> list)
        //        {
        //            foreach (Dictionary<string, List<string>> dic in list)
        //            {
        //                string no = string.Empty;
        //                string sql = @"insert into oa_fee_loan (ID, APPLYPERSONNAME, POSITIONNAME, FEEUSEDEPT, APPLYFEETOTLE, USEDIRECTIONS, BANKNAME, BANKACCOUNT, USEDATE, MEMO, BACKDATE, PAYMODE, FEETYPE, HOSPITALID, CREATEDATE)
        //values ('{0}', '{1}', '{2', '{3}', {4}, '{5}', '{6}', '{7}',  to_date('{8}', 'yyyy-mm-dd hh24:mi:ss'), '{9}', to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'), '{11}', '{12}', '{13}', to_date('{14}', 'yyyy-mm-dd hh24:mi:ss'));";
        //                //编码 0，申请 1，职位 2，费用使用部门 3，借支总金额4，借支理由 5，开户银行 6，银行账号 7，使用日期 8，备注 9，还款日期 10 支付方式 11，费用类型 12，医院 13，流程填写日期 14
        //                sql = string.Format(sql, no, dic["personName"][0], dic["职位"][0], dic["所属部门"][0], dic["申请金额（元）"][0], dic["借款用途（元）"][0], dic["借款人开户银行"][0],
        //                    dic["借款人银行卡号"][0], dic["使用日期"][0], dic["备注"][0], dic["归还日期"][0], "", dic["FormType"][0], dic["HospitalID"][0], dic["createTime"][0]);

        //            }


        //        }

        /// <summary>
        /// 费用借支申请
        /// </summary>
        /// <param name="list"></param>
        public void InsertLoanFee(List<Dictionary<string, List<string>>> list)
        {
            LogHelper.Write("进入插入数据阶段 要插入数据" + list.Count);
            int j = 0;

            foreach (Dictionary<string, List<string>> dic in list)
            {
                string no = dic["Id"][0];
                if (dic.Keys.Contains("总支付方式"))
                {
                    if (dic["总支付方式"][0] == "现金")
                    {
                        dic["总付款银行"][0] = "库存现金";
                    }
                }
                else
                {
                    if (dic["总单项选择"][0] == "现金")
                    {
                        dic["总单项选择"][0] = "库存现金";
                    }
                    dic.Add("总支付方式", dic["单项选择"]);
                }
                if (!dic.Keys.Contains("总付款金额"))
                {
                    dic.Add("总付款金额", dic["总转账金额"]);
                }
                if (!dic.Keys.Contains("总付款银行"))
                {
                    dic.Add("总付款银行", dic["总转账银行"]);
                }
                string sql = @"insert into oa_fee_loan (ID, APPLYPERSONNAME, POSITIONNAME, FEEUSEDEPT, APPLYFEETOTLE, Feedescribe, BANKNAME, BANKACCOUNT, USEDATE, MEMO, BACKDATE, PAYMODE, FEETYPE, HOSPITALID, CREATEDATE,CLOSEPERSON,CLOSETIME,instanceTitle,feetypeID,cwfeetotal,cwpaymant,cwbankname,seq)
values ('{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}',  to_date('{8}', 'yyyy-mm-dd hh24:mi:ss'), '{9}', to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'), '{11}', '{12}', '{13}', to_date('{14}', 'yyyy-mm-dd hh24:mi:ss'),'{15}', to_date('{16}', 'yyyy-mm-dd hh24:mi:ss'),'{17}','{18}','{19}','{20}','{21}',{22})";
                //编码 0，申请 1，职位 2，费用使用部门 3，借支总金额4，借支理由 5，开户银行 6，银行账号 7，使用日期 8，备注 9，还款日期 10 支付方式 11，费用类型 12，医院 13，流程填写日期 14
                sql = string.Format(sql, no, dic["personName"][0], "", dic["所属部门"][0], dic["申请金额（元）"][0], dic["借款用途"][0], dic["借款人开户银行"][0],
                    dic["借款人银行卡号"][0], dic["使用日期"][0], dic["备注"][0], dic["归还日期"][0], "", dic["FormType"][0], dic["HospitalID"][0], dic["createTime"][0], dic["closePerson"][0], dic["closeTime"][0], dic["instanceTitle"][0], dic["FormTypeID"][0], dic["总付款金额"][0], dic["总支付方式"][0], dic["总付款银行"][0], j + 1);
                string sqlFind = "select * from oa_fee_loan where id='{0}' ";
                sqlFind = string.Format(sqlFind, dic["Id"][0]);
                DataTable dt = OracleHelper.ExecuteDataTable(sqlFind);
                if (dt != null && dt.Rows.Count > 0) continue;
                try
                {
                    int i = OracleHelper.ExecuteNonQuery(sql);
                    j++;
                    LogHelper.WriteInsertSuceess(sql);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteInsertFail("异常sql:" + sql + "\r\n" + ex.Message);
                }

            }
            LogHelper.Write("插入借支申请完成,插入数据" + j);

        }


        /// <summary>
        /// 费用付款申请
        /// </summary>
        /// <param name="list"></param>
        public void InsertPaymentFee(List<Dictionary<string, List<string>>> list)
        {
            LogHelper.Write("进入插入数据阶段 要插入数据" + list.Count);
            int j = 0;
            foreach (Dictionary<string, List<string>> dic in list)
            {
                string sqlFind = "select * from oa_fee_payment where id='{0}' ";
                sqlFind = string.Format(sqlFind, dic["Id"][0]);
                DataTable dt = OracleHelper.ExecuteDataTable(sqlFind);
                if (dt != null && dt.Rows.Count > 0) continue;
                string no = dic["Id"][0];
                if (dic.Keys.Contains("总支付方式"))
                {
                    if (dic["总支付方式"][0] == "现金")
                    {
                        dic["总支付方式"][0] = "库存现金";
                    }
                }
                else
                {
                    if (dic.Keys.Contains("总单项选择"))
                    {
                        if (dic["总单项选择"][0] == "现金")
                        {
                            dic["总单项选择"][0] = "库存现金";
                        }
                        dic.Add("总支付方式", dic["单项选择"]);
                    }
                    else if (dic.Keys.Contains("支付方式"))
                    {
                        dic.Add("总支付方式", dic["支付方式"]);
                    }
                    else
                    {
                        dic.Add("总支付方式", null);
                    }
                }
                if (!dic.Keys.Contains("总付款金额"))
                {
                    if (dic.Keys.Contains("总转账金额"))
                    {
                        dic.Add("总付款金额", dic["总转账金额"]);
                    }
                    else if (dic.Keys.Contains("金额"))
                    {
                        dic.Add("总付款金额", dic["金额"]);
                    }
                    else
                    {
                        dic.Add("总付款金额", null);
                    }
                }
                if (!dic.Keys.Contains("总付款银行"))
                {
                    if (dic.Keys.Contains("总转账银行"))
                    {
                        dic.Add("总付款银行", dic["总转账银行"]);
                    }
                    else if (dic.Keys.Contains("付款单位开户行"))
                    {
                        dic.Add("总付款银行", dic["付款单位开户行"]);
                    }
                    else
                    {
                        dic.Add("总付款银行", null);
                    }
                }
                if (!dic.Keys.Contains("所属部门"))
                {
                    dic.Add("所属部门", null);
                }
                if (!dic.Keys.Contains("申请金额（元）"))
                {
                    if (dic.Keys.Contains("金额"))
                    {
                        dic.Add("申请金额（元）", dic["金额"]);
                    }
                    else
                    {
                        dic.Add("申请金额（元）", null);
                    }
                }
                if (!dic.Keys.Contains("借款用途"))
                {
                    if (dic.Keys.Contains("请款理由"))
                    {
                        dic.Add("借款用途", dic["请款理由"]);
                    }
                    else
                    {
                        dic.Add("借款用途", null);
                    }
                }
                if (!dic.Keys.Contains("借款人开户银行"))
                {
                    if (dic.Keys.Contains("收款单位开户行"))
                    {
                        dic.Add("借款人开户银行", dic["收款单位开户行"]);
                    }
                    else
                    {
                        dic.Add("借款人开户银行", null);
                    }
                }
                if (!dic.Keys.Contains("借款人银行卡号"))
                {
                    if (dic.Keys.Contains("收款单位银行账号"))
                    {
                        dic.Add("借款人银行卡号", dic["收款单位银行账号"]);
                    }
                    else
                    {
                        dic.Add("借款人银行卡号", null);
                    }
                }
                if (!dic.Keys.Contains("使用日期"))
                {
                    if (dic.Keys.Contains("资金到账期限"))
                    {
                        dic.Add("使用日期", dic["资金到账期限"]);
                    }
                    else
                    {
                        dic.Add("使用日期", null);
                    }
                }
                if (!dic.Keys.Contains("备注"))
                {
                    if (dic.Keys.Contains("请款理由"))
                    {
                        dic.Add("备注", dic["请款理由"]);
                    }
                    else
                    {
                        dic.Add("备注", null);
                    }
                }
                if (!dic.Keys.Contains("归还日期"))
                {
                    dic.Add("归还日期", null);
                }
                if (!dic.Keys.Contains("经办部门"))
                {
                    if (dic.Keys.Contains("所属部门"))
                    {
                        dic.Add("经办部门", dic["所属部门"]);
                    }
                    else
                    {
                        dic.Add("经办部门", null);
                    }
                }
                string sql = @"insert into oa_fee_payment (ID, APPLYPERSONNAME, POSITIONNAME, FEEDESCRIBE, FEEUSEDEPT, APPLYFEETOTLE, LENDFEE, TSFEE, CREATEDATE, HOSPITALID, FEETYPE, BANKNAME, BANKACCOUNT,
MEMO, PAYMODE, INVOICEOPTION,
COLLECTIONCOMPANY, COMPANYNAME, PROJECTTYPE,CLOSEPERSON,CLOSETIME,instanceTitle,feetypeID,cwfeetotal,cwpaymant,cwbankname,seq)
values ('{0}', '{1}', '{2}', '{3}', '{4}', {5},{6}, {7}, to_date('{8}', 'yyyy-mm-dd hh24:mi:ss'), '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}','{19}', to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),'{21}','{22}',{23},'{24}','{25}',{26})";
                //编码 0，申请人 1，职位 2，费用描述 3，费用申请部门 4，合同总金额 5，已经付款金额 6，本次付款金额 7，表单申请日期 8，医院 9，费用类型 10，开户银行 11，银行账户 12 备注 13，支付方式 14，发票选项 15   FormType
                //收款公司16 公司 17 工程类型 18
                string projectType = "";
                if (dic.Keys.Contains("工程类别"))
                {
                    projectType = dic["工程类别"][0];
                }
                string companyName = "";
                if (dic.Keys.Contains("公司名称"))
                    companyName = dic["公司名称"][0];

                string hospitalId = dic["HospitalID"][0];
                if (dic["FormTypeID"][0] == "5")
                {
                    switch (companyName)
                    {
                        case "广州爱博恩医疗集团有限公司":
                            hospitalId = "IBORN";
                            break;
                        case "广州爱博恩医疗门诊部有限公司":
                            hospitalId = "BELLAIRE";
                            break;
                        case "佛山顺德爱博恩妇产医院有限公司":
                            hospitalId = "SDIBORN";
                            break;
                        case "佛山顺德爱博恩医疗门诊有限公司":
                            hospitalId = "SDIBORNCLINIC";
                            break;
                        case "广州爱博恩母婴护理有限公司":
                            hospitalId = "IBORNBABY";
                            break;
                        default :
                            break;
                    }
                }

                if (dic["请款理由"][0].IndexOf("'") > 0)
                {
                    dic["请款理由"][0] = dic["请款理由"][0].Replace("'", "''");
                }
                if (dic["总支付方式"].Count == 0)
                {
                    dic["总支付方式"].Add("");
                }

                if (!dic.Keys.Contains("合同金额"))
                {
                    if (dic.Keys.Contains("金额"))
                    {
                        dic.Add("合同金额", dic["金额"]);
                    }
                    else
                    {
                        dic.Add("合同金额", null);
                    }
                }
                if (!dic.Keys.Contains("已付款金额"))
                {
                    if (dic.Keys.Contains("金额"))
                    {
                        dic.Add("已付款金额", dic["金额"]);
                    }
                    else
                    {
                        dic.Add("已付款金额", null);
                    }
                }
                if (!dic.Keys.Contains("本次付款金额"))
                {
                    if (dic.Keys.Contains("金额"))
                    {
                        dic.Add("本次付款金额", dic["金额"]);
                    }
                    else
                    {
                        dic.Add("本次付款金额", null);
                    }
                }
                if (!dic.Keys.Contains("发票选项"))
                {
                    if (dic.Keys.Contains("备注"))
                    {
                        dic.Add("发票选项", dic["备注"]);
                    }
                    else
                    {
                        dic.Add("发票选项", null);
                    }
                }
                try
                {
                    sql = string.Format(sql, no, dic["personName"][0], "", dic["请款理由"][0], dic["经办部门"] == null ? "" : dic["经办部门"][0], dic["合同金额"][0], dic["已付款金额"][0], dic["本次付款金额"][0], dic["createTime"][0], hospitalId, dic["FormType"][0], dic["收款单位银行账号"][0],
                        dic["收款单位银行账号"][0], "", dic["支付方式"][0], dic["发票选项"][0], dic["收款单位名称"][0], companyName, projectType, dic["closePerson"][0], dic["closeTime"][0], dic["instanceTitle"][0], dic["FormTypeID"][0], dic["总付款金额"][0], dic["总支付方式"][0], dic["总付款银行"][0], j + 1);

                    int i = OracleHelper.ExecuteNonQuery(sql);
                    j++;
                    LogHelper.WriteInsertSuceess(sql);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteInsertFail("异常sql:" + sql + "\r\n" + ex.Message);
                }

            }

            //LogHelper.Write("插入付款申请完成,插入数据" + j);
        }

    }
}
