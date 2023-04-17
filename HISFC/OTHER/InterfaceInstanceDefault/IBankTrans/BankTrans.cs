using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceInstanceDefault.IBankTrans
{
    public class BankTrans : FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans
    {
        #region IBankTrans 成员

        public bool Do()
        {
            #region BankRequest
            //传给银行信用卡系统的交易参数bankrequest

            //内容
            // 格式
            // 长度
            // 描述

            //POS机号
            // ASCII
            // 15
            // 不足右补空格

            //POS员工号
            // ASCII
            // 15
            // 不足右补空格

            //交易类型标志
            // ASCII
            // 1
            // 为'C'，表示是正数（消费交易）；

            //为'D'，表示是负数（取消交易或退货交易）；

            //为’E’表示其它交易(签到、重打印、结算交易等等)

            //金额
            // ASCII
            // 12
            // 信用卡消费金额，char(12)，没有小数点"."，精确到分，最后两位为小数位，不足前补0。
            string bankRequest4 = string.Empty;
            bankRequest4 = FS.FrameWork.Public.String.FormatNumberReturnString(FS.FrameWork.Function.NConvert.ToDecimal(inputListInfo[1]), 2).Replace(".", "");
            bankRequest4 = bankRequest4.PadLeft(12, '0');
            //备用
            // ASCII
            // 6
            // “000000”，备用 
            #endregion
            string bankResponse = string.Empty;
            try
            {
                YinLianDLL.PostForm postForm = new YinLianDLL.PostForm();
                bankResponse = postForm.Post("", "", "C", bankRequest4, "000000");
            }
            catch (Exception ex)
            {
                outputListInfo.Add(ex.Message.ToString());
                return false;
            }

            #region BankResponse
            //银行信用卡系统传回的交易回应BankResponse

            //内容
            // 格式
            // 长度
            // 描   述

            //响应码
            // ASCII
            // 2
            // 00 表示成功，其它表示失败
            bool bankResponse1 = false;
            if (bankResponse.Length >= 2 && bankResponse.Substring(0, 2) == "00")
            {
                bankResponse1 = true;
            }
            else
            {
                outputListInfo.Add("错误码:" + bankResponse.Substring(0, 2));
            }
            //金额
            // ASCII
            // 12
            // 信用卡刷卡成功金额，char(12)，没有小数点"."，精确到分，最后两位为小数位，不足前补0。
            decimal bankResponse2 = 0m;
            if (bankResponse1 == true && bankResponse.Length >= 12 + 2)
            {
                bankResponse2 = FS.FrameWork.Function.NConvert.ToDecimal(bankResponse.Substring(2, 10))
                    + FS.FrameWork.Function.NConvert.ToDecimal("0." + bankResponse.Substring(12, 2));
            }
            //0:银行 1：账号 2：pos号 3：金额
            //卡号
            // ASCII
            // 19
            // 不足右补空格

            string bankResponse3 = string.Empty;
            if (bankResponse1 == true && bankResponse.Length >= 12 + 2 + 19)
            {
                bankResponse3 = bankResponse.Substring(12 + 2, 19).TrimStart();

            }
            //备用
            // ASCII
            // 19
            // 备用
            string bankResponse4 = string.Empty;
            //卡类型标志
            // ASCII
            // 4
            // 银行卡卡类
            string bankResponse5 = string.Empty;
            //卡名
            // ASCII
            // 8
            // 银行卡名称
            string bankResponse6 = string.Empty;
            //LRC
            // ASCII
            // 3
            // 三位数字，应该和请求一致
            string bankResponse7 = string.Empty;
            //商户号
            // ASCII
            // 15
            string bankResponse8 = string.Empty;

            //终端号
            // ASCII
            // 8
            string bankResponse9 = string.Empty;

            //批次号
            // ASCII
            // 6
            string bankResponse10 = string.Empty;
            //0:银行 1：账号 2：pos号 3：金额
            //流水号
            // ASCII
            // 6
            string bankResponse11 = string.Empty;
            if (bankResponse1 == true && bankResponse.Length >= 12 + 2 + 19 + 9 + 4 + 8 + 3 + 5 + 8 + 6 + 6)
            {
                bankResponse11 = bankResponse.Substring(12 + 2 + 19 + 9 + 4 + 8 + 3 + 5 + 8 + 6, 6);

            }

            //系统参考号
            // ASCII
            // 12


            //日期
            // ASCII
            // 4
            // MMDD

            //时间
            // ASCII
            // 6
            // HHMMSS 
            #endregion

            if (bankResponse1 == false)
            {

            }
            else
            {
                //0:银行 1：账号 2：pos号 3：金额
                //银行
                outputListInfo.Add("");
                //账户
                outputListInfo.Add(bankResponse3);
                //pos号
                outputListInfo.Add(bankResponse11);
                //金额
                outputListInfo.Add(bankResponse2);

            }
            return bankResponse1;
        }
        private List<object> inputListInfo = new List<object>();

        public List<object> InputListInfo
        {
            get { return inputListInfo; }
            set { inputListInfo = value; }
        }
        private List<object> outputListInfo = new List<object>();

        public List<object> OutputListInfo
        {
            get { return outputListInfo; }
            set { outputListInfo = value; }
        }
       

        #endregion
    }
}
