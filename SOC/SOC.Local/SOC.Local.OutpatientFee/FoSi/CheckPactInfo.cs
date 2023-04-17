using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Xml;

namespace FS.SOC.Local.OutpatientFee.FoSi
{
    /// <summary>
    /// 校验患者合同单位信息
    /// </summary>
    public class CheckPactInfo : FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo
    {
        #region ICheckPactInfo 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string err;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }

        /// <summary>
        /// 佛四webswever服务接口
        /// </summary>
        FS.SOC.Local.OutpatientFee.fosiweb.FosiWeb web = new FS.SOC.Local.OutpatientFee.fosiweb.FosiWeb();
        /// <summary>
        /// 校验的合同单位
        /// </summary>
        string CheckEmployeePactList = "";

        /// <summary>
        /// 校验合同单位的有效性
        /// </summary>
        /// <returns></returns>
        public int CheckIsValid()
        {
            // 传入串 <Request><IdenNO>430381199004019194</IdenNO><PactCode>1</PactCode><PactName>普通</PactName></Request>
            // 返回串 <Response><ResultCode>{0}</ResultCode><ErrorMsg>{1}</ErrorMsg>{2}</Response>

            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            CheckEmployeePactList = controlMgr.GetControlParam<string>("HNMZ95", true, "");

            string requestXml = @"<Request><IdenNO>{0}</IdenNO><PactCode>{1}</PactCode><PactName>{2}</PactName></Request>";
            string strRequest = "";
            string returnedXml = "";
            string resultCode = "";
            string errorMsg = "";

            if (CheckEmployeePactList == null || CheckEmployeePactList == "")
            {
                err = "请维护需要webserver判断身份的合同单位！";
                return -1;
            }
            string[] strArr = CheckEmployeePactList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (patientInfo == null)
            {
                err = "患者信息为空！";
                return -1;
            }  

            if (patientInfo.Pact.ID == null || patientInfo.Pact.ID == "")
            {
                err = "患者合同单位为空！不能选择医保身份，请到收费处补充资料";
                return -1;
            }
            int iLen = strArr.Length;
            string strTemp = "";
            for (int idx = 0; idx < iLen; idx++)
            {
                #region 如果患者合同单位需要判断，则走下面的方法，否则继续判断，如果合同单位都不符合的话则返回1
                if (patientInfo.Pact.ID == strArr[idx])
                {
                    if (patientInfo.IDCard == null || patientInfo.IDCard == "")
                    {
                        err = "患者身份证号码为空！不能选择医保身份，请到收费处补充资料";
                        return -1;
                    }

                    strRequest = string.Format(requestXml, patientInfo.IDCard, patientInfo.Pact.ID, patientInfo.Pact.Name);
                    returnedXml=web.JudgeCanMedicare(strRequest);

                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(returnedXml);
                        XmlNode node = doc.SelectSingleNode("Response/ResultCode");
                        if (node == null)
                        {
                            this.err="获取参数异常,不存在ResultCode节点。";
                            return -1; 
                        }
                        resultCode = node.InnerText.Trim();
                        if (string.IsNullOrEmpty(resultCode))
                        {
                            this.err = "获取参数异常,ResultCode为空。";
                            return -1; 
                        }

                        node = doc.SelectSingleNode("Response/ErrorMsg");
                        if (node == null)
                        {
                            this.err = "获取参数异常,不存在ErrorMsg节点。";
                            return -1; 
                        }
                        errorMsg = node.InnerText.Trim();
                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            this.err = "获取参数异常,ErrorMsg为空。";
                            return -1; 
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        this.err = "获取参数异常：" + ex.ToString();
                        return -1; 
                    }
                    if (resultCode == "-1" || resultCode == "-2")
                    {
                        this.err = errorMsg;
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                    
                }
                #endregion
                else
                {
                    continue;
                }
                
            }

            return 1;
        }

        #endregion
    }
}
