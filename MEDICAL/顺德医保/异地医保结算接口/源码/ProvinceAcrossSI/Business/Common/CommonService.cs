using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ProvinceAcrossSI.Business.Common
{
    public class CommonService
    {
        ProvinceAcrossSI.Funtion function = new Funtion();
        ProvinceAcrossSI.ProvinceAcrossSIDatabase SImgr = new ProvinceAcrossSIDatabase();

        #region 作废
        ///// <summary>
        ///// 数据字典下载
        ///// </summary>
        ///// <param name="type"></param>
        //public string DownLoadDictionary(string type, ref ArrayList alDictionary, ref string status, ref string msg)
        //{
        //    string ser = "医院联网结算";
        //    string cas = "数据字典下载";
        //    string parXML = "<?xml version=\"1.0\" encoding=\"GB18030\"?><XML><字典目录><字典项目>{0}</字典项目></字典目录></XML>";

        //    parXML = string.Format(parXML, type);

        //    string sig = function.GetSig(ser, function.DEP, cas, parXML);

        //    if ("-1".Equals(sig))
        //    {
        //        status = "-1";
        //        msg = "未能读取电子签名";
        //        return "";
        //    }

        //    string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

        //    function.GetResultStatusAndMessage(retXML, ref status, ref msg);

        //    if (status == null || string.IsNullOrEmpty(status))
        //    {
        //        return msg;
        //    }

        //    alDictionary = new ArrayList();

        //    function.GetReturnDictionary(retXML, type, ref alDictionary);

        //    return retXML;
        //}

        ///// <summary>
        ///// 检查合同是否正确(根据fin_com_sicompare_FoShan查找对照)      
        ///// </summary>
        ///// <param name="siType"></param>
        ///// <param name="personType"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public int CheckPactCode(FS.HISFC.Models.Registration.Register patientInfo, string siType, string personType, string type, out string retPactCode)
        //{
        //    //by han-zf 2014-09-28 因上线需要，该方法暂时处理为任何情况均检查通过
        //    retPactCode = patientInfo.Pact.ID;
        //    return 1;

        //    if ("C".Equals(type) || "I".Equals(type))
        //    {
        //        if (patientInfo.Pact.ID.Equals("19") || patientInfo.Pact.ID.Equals("20") || patientInfo.Pact.ID.Equals("21"))
        //        {
        //            //如果合同单位选择：体外震波碎石(门诊)、门诊放化疗(门诊)、它院检查(门诊)。将不做合同单位选择判断。                 
        //            //判断方式：通过本地对照fin_com_sicompare_zhuhai，根据中心返回人员类别、参保险种判断。
        //            retPactCode = patientInfo.Pact.ID;
        //            return 1;
        //        }

        //        retPactCode = this.SImgr.GetComparePactCode(siType, personType, type);
        //        if (retPactCode.Split(',')[0].Equals(patientInfo.Pact.ID))
        //        {
        //            return 1;
        //        }
        //        else
        //        {
        //            return -1;
        //        }
        //    }
        //    else
        //    {
        //        retPactCode = "";
        //        return -1;
        //    }
        //}
#endregion

        /// <summary>
        /// 获得异地医保参数
        /// 1、就医地统筹区编码，2、就医地分中心编码，3、医院编码
        /// </summary>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public int GetYdConstParm(ref ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
        {
            //TODO:后续需将这些数据放到字典表，以便维护
            //personInfo.HospitalizeAreaCode = "440605";
            //personInfo.HospitalizeCenterAreaCode = "";
            //personInfo.HospitalCode = "45607420044060411A5201";
            function.GetHosCodeAndRegionCode(ref personInfo);
            return 1;
        }

        #region 作废
        ///// <summary>
        ///// 门诊特殊结算检查【碎石、放化疗、它院检查】
        ///// </summary>
        ///// <param name="pact">合同单位信息</param>
        ///// <param name="bxlb">报销类别</param>
        ///// <returns>1特殊结算，其他普通结算</returns>
        //public int CheckSpecialOutBalance(FS.HISFC.Models.Base.PactInfo pact, ref string bxlb)
        //{
        //    if (pact.Name.Contains("碎石") || pact.Name.Contains("放化疗") || pact.Name.Contains("院检查"))
        //    {
        //        //体外震波碎石(门诊)、门诊放化疗(门诊)、它院检查(门诊)。结算XML 
        //        bxlb = string.Empty;
        //        if (pact.Name.Contains("碎石"))
        //        {
        //            bxlb = "100";
        //        }
        //        if (pact.Name.Contains("放化疗"))
        //        {
        //            bxlb = "200";
        //        }
        //        if (pact.Name.Contains("院检查"))
        //        {
        //            bxlb = "300";
        //        }
        //        return 1;
        //    }

        //    return -1;
        //}

        ///// <summary>
        ///// 门诊特殊结算检查【门诊生育结算】
        ///// </summary>
        ///// <param name="pact"></param>
        ///// <param name="siType">生育门诊结算医保类别</param>
        ///// <returns></returns>
        //public int CheckSYoutBalance(FS.HISFC.Models.Base.PactInfo pact, ref string siType)
        //{
        //    if (pact.Name.Contains("生育"))
        //    {
        //        siType = "6";
        //        return 1;
        //    }
        //    return -1;
        //}
        #endregion

        /// <summary>
        /// 获取常数字典
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        public ArrayList QueryTYPEFormComDictionary(string strType)
        {
            return this.SImgr.QueryTYPEFormComDictionary(strType);
        }
    }
}
