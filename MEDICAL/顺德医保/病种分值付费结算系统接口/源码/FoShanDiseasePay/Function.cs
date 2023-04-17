using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;


namespace FoShanDiseasePay
{
    /// <summary>
    /// ********************************************************
    /// 功能描述：佛山市按病种分值付费结算接口
    /// 创建日期：2018-03-01
    /// 创 建 人：gumaozhu
    /// 修改日期：
    /// 修 改 人：
    /// 修改内容：
    /// ********************************************************
    /// </summary>
    public class Function
    {
        #region 上传-入参数封装JSON

        [DllImport("Audit4Hospital.dll", EntryPoint = "Audit4HospitalPortal", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern string Audit4HospitalPortal(string args);//初始化
        /// <summary>
        /// 主单信息上传
        /// </summary>
        public static string MainInfoJSON
        {
            get
            {
                string str = @"'wylsh':'{0}','sfdjh':'{1}','djjsrq':'{2}','ddjgbm':'{3}','ddjgmc':'{4}','fyfsjgbm':'{5}',
                            'fyfsjgmc':'{6}','zwjgmc':'{7}','gmsfhm':'{8}','cbrmc':'{9}','cbrxb':'{10}','cbrcsrq':'{11}',
                            'rylbbm':'{12}','jzfsbm':'{13}','sfydjy':'{14}','ryzdbm':'{15}','cyzdbm':'{16}','fzd1':'{17}',
                            'fzd2':'{18}','fzd3':'{19}','fzd4':'{20}','fzd5':'{21}','fzd6':'{22}','fzd7':'{23}','fzd8':'{24}',
                            'fzd9':'{25}','fzd10':'{26}','fzd11':'{27}','fzd12':'{28}','fzd13':'{29}','fzd14':'{30}','fzd15':'{31}',
                            'fzd16':'{32}','sftbmbdjbz':'{33}','mbtbdm':'{34}','ryrq':'{35}','cyrq':'{36}','jzrq':'{37}','sfhy':'{38}',
                            'sfbrq':'{39}','sg':'{40}','tz':'{41}','xy':'{42}','kfxt':'{43}','chxt':'{44}','tw':'{45}','xl':'{46}',
                            'sfzry':'{47}','ybzxdm':'{48}','ydrylb':'{49}','ydcbxzqydm':'{50}','dylx':'{51}','cblx':'{52}','zje':'{53}',
                            'ybnzje':'{54}','ybnzfje':'{55}','zfbl':'{56}','zhfbl':'{57}','xyf':'{58}','zchyf':'{59}','zcyf':'{60}',
                            'ybylfwf':'{61}','ybzlczf':'{62}','hlf':'{63}','blzdf':'{64}','syszdf':'{65}','yxxzdf':'{66}','lczdxmf':'{67}',
                            'sszlf':'{68}','mjf':'{69}','fsszlxmf':'{70}','lcwlzlf':'{71}','kff':'{72}','xf':'{73}','bdblzpf':'{74}',
                            'qdblzpf':'{75}','nxyzlzpf':'{76}','xbyzlzpf':'{77}','jcyclf':'{78}','zlyclf':'{79}','ssyclf':'{80}',
                            'mzzyh':'{81}','cjrbs':'{82}','yylb':'{83}','cjdjh':'{84}','jjlx':'{85}','ssdm':'{86}','ssqkfl':'{87}',
                            'cyzry':'{88}',detail: [{89}]";
                return str;
            }
        }

        /// <summary>
        /// 费用明细信息上传
        /// </summary>
        public static string FeeDetailJSON
        {
            get
            {
                string str = @"'sfdjh':'{0}','wylsh':'{1}','fwrq':'{2}','xmbm':'{3}','xmmc':'{4}','xmlb':'{5}',
                            'dj':'{6}','sl':'{7}','zj':'{8}','ysbm':'{9}','ysmc':'{10}','ksbm':'{11}',
                            'ksmc':'{12}','yf':'{13}','gytj':'{14}','yl':'{15}','pc':'{16}','yyts':'{17}',
                            'ybnje':'{18}','jylx':'{19}','ypgg':'{20}','cydybs':'{21}','zyh':'{22}',
                            'yzzdid':'{23}','yzmxid':'{24}','sbxh':'{25}','ypbwm':'{26}','yyxmbm':'{27}'";
                return str;
            }
        }

        /// <summary>
        /// 病案信息上传
        /// </summary>
        public static string InCaseJSON
        {
            get
            {
                string str = @"     'HospitalId':'{0}','AdmissionNo':'{1}','SciCardNo':'{2}','SciCardIdentified':'{3}',
		                            'OutBedNum':'{4}','AdmissionDate':'{5}','DischargeDate':'{6}','DoctorCode':'{7}',
		                            'DoctorName':'{8}','IsDrugAllergy':'{9}','AllergyDrugCode':'{10}','AllergyDrugName':'{11}',
		                            'IsPathologicalExamination':'{12}','PathologyCode':'{13}','IsHospitalInfected':'{14}',
		                            'HospitalInfectedCode':'{15}','BloodTypeS':'{16}','BloodTypeE':'{17}','LeaveHospitalType':'{18}',
		                            'ChiefComplaint':'{19}','MedicalHistory':'{20}','SurgeryHistory':'{21}','BloodTransHistory':'{22}',
		                            'Marriage':'{23}','Height':'{24}','Weight':'{25}','NewbornDate':'{26}','NewbornWeight':'{27}',
		                            'NewbornCurrentWeight':'{28}','BearPregnancy':'{29}','BearYie':'{30}','AdmissionDiseaseId':'{31}',
		                            'AdmissionDiseaseName':'{32}','DiagnosePosition1':'{33}','DischargeDiseaseId':'{34}',
		                            'DischargeDiseaseName':'{35}','DiagnosePosition2':'{36}','Tsblbs':'{37}','Dept_Name':'{38}',{39}
                            ";
                return str;
            }
        }

        /// <summary>
        /// 检查主单
        /// </summary>
        public static string ListCheckJSON
        {
            get
            {
                string str = @"'CheckId': '{0}','DepartmentCode': '{1}','DepartmentName': '{2}','ApplyProjectCode': '{3}',
                            'ApplyProjectName': '{4}','ApplyDoctor': '{5}','ApplyDoctorName': '{6}','ApplyDatetime': '{7}',
                            'ReportDatetime': '{8}','CheckPositionCode': '{9}','Abnormal': '{10}','CheckResult': '{11}'";
                return str;
            }
        }

        /// <summary>
        /// 手术记录主单
        /// </summary>
        public static string ListOperationJSON
        {
            get
            {
                string str = @"'OperationRecordNo': '{0}','OperationDoctorCode': '{1}','OperationDoctorName': '{2}','FirstOperdoctorcode': '{3}',
                            'FirstOperdoctorname': '{4}','SecondOperdoctorcode': '{5}','SecondOperdoctorname': '{6}','AnesthesiologistCode': '{7}',
                            'AnesthesiologistName': '{8}','OperationDate': '{9}','OperationFinishDate': '{10}','AnaesthesiaType': '{11}',
                            'IsComplication': '{12}','ComplicationCode': '{13}','ComplicationName': '{14}','OperationRecord': '{15}',
                            'RecordDoctorCode': '{16}','RecordDoctorName': '{17}','ListOperationDetail': [{18}]";
                return str;
            }
        }

        /// <summary>
        /// 手术记录明细
        /// </summary>
        public static string ListOperationDetailJSON
        {
            get
            {
                string str = @"'OperationRecordNo': '{0}','OperationNo': '{1}','OperationCode': '{2}','OperationName': '{3}','OperationLevel': '{4}',
                            'OperationIncisionClass': '{5}','OperationHealClass': '{6}','IsMajorIden': '{7}','IsIatrogenic': '{8}'";
                return str;
            }
        }

        /// <summary>
        /// 出院小结
        /// </summary>
        public static string LeaveHospitalJSON
        {
            get
            {
                string str = @"'DischargeOutcome': '{0}',
                               'HospitalizationSituation': '{1}',
                               'DtProcess': '{2}',
                               'LeaveHospitalStatus': '{3}',
                               'LeaveDoctorAdvice': '{4}'";

                return str;
            }
        }

        //--查询--

        /// <summary>
        /// 已上传主单信息查询
        /// </summary>
        public static string QueryMainInfoJSON
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
                                    <!-- 0 医院编号  ckz543  varchar2(8) 不可为空  -->
                                    <ckz543>{0}</ckz543>
                                    <!-- 1 经办人  aae011  varchar2(20) 不可为空 (操作员id) -->
                                    <aae011>{1}</aae011>
                                    <!-- 2 医院登陆的sessionid  sessionid  varchar2(100) 不可为空 (操作员id) -->
                                    <sessionid>{2}</sessionid>

	                                <!-- 3 身份证号码 -->
                                    <gmsfhm>{3}</gmsfhm>
                                    <!-- 4 住院号/门诊号 -->
                                    <mzzyh>{4}</mzzyh>
                                    <!-- 5 唯一流水号（主单号)单据主键，唯一标识医院编码+发票号+收费单据号-->
                                    <wylsh>{5}</wylsh>
                                    <!-- 6 收费单据号 即医保系统的结算顺序号，主单结算以后再上传-->
                                    <sfdjh>{6}</sfdjh>

                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 已上传费用明细信息查询
        /// </summary>
        public static string QueryFeeDetailJSON
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
                                    <!-- 0 医院编号  ckz543  varchar2(8) 不可为空  -->
                                    <ckz543>{0}</ckz543>
                                    <!-- 1 经办人  aae011  varchar2(20) 不可为空 (操作员id) -->
                                    <aae011>{1}</aae011>
                                    <!-- 2 医院登陆的sessionid  sessionid  varchar2(100) 不可为空 (操作员id) -->
                                    <sessionid>{2}</sessionid>

                                    <!-- 3 收费单据号 即医保系统的结算顺序号，主单结算以后再上传-->
                                    <sfdjh>{3}</sfdjh>

                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 已上传病案信息查询
        /// </summary>
        public static string QueryInCaseJSON
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
                                    <!-- 0 医院编号  ckz543  varchar2(8) 不可为空  -->
                                    <ckz543>{0}</ckz543>
                                    <!-- 1 经办人  aae011  varchar2(20) 不可为空 (操作员id) -->
                                    <aae011>{1}</aae011>
                                    <!-- 2 医院登陆的sessionid  sessionid  varchar2(100) 不可为空 (操作员id) -->
                                    <sessionid>{2}</sessionid>

                                    <!-- 3 收费单据号 即医保系统的结算顺序号，主单结算以后再上传-->
                                    <sfdjh>{3}</sfdjh>
                                    <!-- 4 门诊/住院号 -->
                                    <mzzyh>{4}</mzzyh>
                                    <!-- 5 身份证号码 -->
                                    <gmsfhm>{5}</gmsfhm>

                                </input>";
                return str;
            }
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="listNode"></param>
        /// <param name="root"></param>
        /// <param name="dicNodeValue"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int GetNodeValue(XmlDocument doc, List<string> listNode, string root, ref Dictionary<string, string> dicNodeValue, out string errorMsg)
        {
            errorMsg = string.Empty;
            //无元素值
            if (listNode.Count <= 0)
            {
                return 1;
            }
            int rev = -1;
            string nodeValue = string.Empty;
            foreach (string key in listNode)
            {
                //获取值
                rev = GetParamValue(doc, root + "/" + key, out nodeValue, out errorMsg);
                if (rev <= 0)
                {
                    errorMsg = "无节点【" + key + "】!请检查输入参数!";
                    //return rev;

                    continue;
                }

                //添加到Dictionary中
                if (!dicNodeValue.ContainsKey(key))
                {
                    dicNodeValue.Add(key, nodeValue);
                }
            }

            return rev;
        }

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="strNode"></param>
        /// <param name="Value"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int GetParamValue(XmlDocument doc, string strNode, out string Value, out string errorMsg)
        {
            Value = string.Empty;
            errorMsg = string.Empty;
            if (doc == null || string.IsNullOrEmpty(strNode))
            {
                errorMsg = "内部参数调用异常！";
                return -1;
            }

            XmlNode node = doc.SelectSingleNode(strNode);
            if (node == null)
            {
                errorMsg = "获取参数异常,不存在 【" + strNode + "】节点。";
                return -1;
            }

            if (node.HasChildNodes)
            {
                if (node.ChildNodes.Count > 1 || node.ChildNodes[0].HasChildNodes)
                {
                    Value = node.OuterXml.Trim();
                }
                else
                {
                    Value = node.InnerText.Trim();
                }
            }
            else
            {
                Value = node.InnerText.Trim();
            }

            return 1;
        }

        /// <summary>
        /// 1-门诊、2-住院
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static string GetServiceName(string serviceType)
        {
            string serviceName = string.Empty;
            switch (serviceType)
            {
                case "1":
                    serviceName = "门诊";
                    break;
                case "2":
                    serviceName = "住院";
                    break;
            }

            return serviceName;
        }

        /// <summary>
        /// 性别转换：1男0女
        /// 
        /// 修改为：
        /// 性别1男2女9未知
        /// 
        /// </summary>
        /// <param name="sexId"></param>
        /// <returns></returns>
        public static string ConvertSexCode(string sexId)
        {
            string sexCode = string.Empty;
            switch (sexId)
            {
                case "F":
                    sexCode = "2";
                    break;
                case "M":
                    sexCode = "1";
                    break;
            }
            return sexCode;
        }

        #endregion

        #region 图片转为Base64编码的文本


        /// <summary>
        /// 图片转为base64编码的文本
        /// </summary>
        /// <param name="imageFilePath">图片文件</param>
        /// <param name="format">压缩格式</param>
        /// <returns></returns>
        public static string ToBase64String(string imageFilePath, ImageFormat format)
        {
            try
            {
                Bitmap bmp = new Bitmap(imageFilePath);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, format);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                ms.Dispose();
                bmp.Dispose();
                return Convert.ToBase64String(arr);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// base64编码的文本转为图片
        /// </summary>
        /// <param name="imageBase64">base64编码的文本</param>
        /// <returns></returns>
        public static Image ToImage(string imageBase64)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(imageBase64);
                //读入MemoryStream对象
                MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                memoryStream.Write(imageBytes, 0, imageBytes.Length);
                //转成图片
                Image image = Image.FromStream(memoryStream);
                return image;
            }
            catch
            {
                return null;
            }
        }


        #endregion

        #region 病种分值付费升级改造 病案信息

        /// <summary>
        /// 主体住院
        /// </summary>
        public static string BaseMainJsonZY
        {
            get
            {
                string str = @"'JBXX': {0},
                            'ZYZLXX': {1},
                            'YYSFXX': {2},
                            'CYXJ':{3},
                            'JZMX':{4}";
                return str;
            }
        }

        /// <summary>
        /// 主体门诊
        /// </summary>
        public static string BaseMainJsonMZ
        {
            get
            {
                string str = @"'JBXX': {0},
                            'MZMTBZLXX': {1},
                            'YYSFXX': {2},
                            'JZMX':{3}";
                return str;
            }
        }


        /// <summary>
        /// 病案实体JBXX
        /// </summary>
        public static string JBXXJson
        {
            get
            {
                string str = @"'QDLSH': '{0}',
                            'DDYLJGMC': '{1}',
                            'DDYLJGDM': '{2}',
                            'YBJSDJ': '{3}',
                            'YBBH': '{4}',
                            'BAH': '{5}',
                            'SBSJ': '{6}',
                            'XM': '{7}',
                            'XB': '{8}',
                            'CSRQ': '{9}',
                            'NL_S': '{10}',
                            'NL_BZZS': '{11}',
                            'GJ': '{12}',
                            'MZ': '{13}',
                            'HZZJLB': '{14}',
                            'HZZJHM': '{15}',
                            'ZY': '{16}',
                            'XZZ': '{17}',
                            'GZDWMC': '{18}',
                            'GZDWDZ': '{19}',
                            'DWDH': '{20}',
                            'GZDWYB': '{21}',
                            'LXRXM': '{22}',
                            'LXRYHZGX': '{23}',
                            'LXRDZ': '{24}',
                            'LXRDH': '{25}',
                            'YBLX': '{26}',
                            'TSRYLX': '{27}',
                            'CBD': '{28}',
                            'XSERYLX': '{29}',
                            'XSECSTZ': '{30}',
                            'XSERYTZ': '{31}'";
                return str;
            }
        }
        /// <summary>
        /// 诊断实体MZMTBZLXX
        /// </summary>
        public static string MZMTBZLXXJson
        {
            get
            {
                string str = @"'ZDKB': '{0}',
                            'JZRQ': '{1}',
                            'MTZD': [{2}],
                            'MTSS': [{3}]";
                return str;
            }
        }

        /// <summary>
        /// 病案实体ZYZLXX
        /// </summary>
        public static string ZYZLXXJson
        {
            get
            {
                string str = @"'ZYYLLX': '{0}',
                                'RYTJ': '{1}',
                                'ZLLB': '{2}',
                                'RYSJ': '{3}',
                                'RYKB': '{4}',
                                'ZKKB': '{5}',
                                'CYSJ': '{6}',
                                'CYKB': '{7}',
                                'SJZY': '{8}',
                                'MJZZD1': '{9}',
                                'MJZXYJBDM': '{10}',
                                'MJZZD2': '{11}',
                                'MJZZYJBDM': '{12}',
                                'XYZYZD': '{13}',
                                'XYZYZDJBDM': '{14}',
                                'XYZYZDRYBQ': '{15}',
                                {16}
                                'ZB': '{17}',
                                'ZBJBDM': '{18}',
                                'ZBRYBQ': '{19}',
                                {20}
                                'ZDDMJS': '{21}',
                                {22}
                                'SSJCZDMJS': '{23}',
                                'HXJSYSJ': '{24}',
                                'RYQLNSSHZHMSJ': '{25}',
                                'RYHLNSSHZHMSJ': '{26}',
                                'ZZJHBFLX': '{27}',
                                'JZZJHSSJ': '{28}',
                                'CZZJHSSJ': '{29}',
                                'HJ': '{30}',
                                'SXPZ': '{31}',
                                'SXL': '{32}',
                                'SXJLDW': '{33}',
                                'TJHLTS': '{34}',
                                'YJHLTS': '{35}',
                                'EJHLTS': '{36}',
                                'SJHLTS': '{37}',
                                'LYFS': '{38}',
                                'NJSJGMC': '{39}',
                                'NJSJGDM': '{40}',
                                'SFYCY31TNZZYJH': '{41}',
                                'ZZYJHMD': '{42}',
                                'ZZYSXM': '{43}',
                                'ZZYSDM': '{44}',
                                'SciCardNo': '{45}',
                                'OutBedNum': '{46}',
                                'DoctorCode': '{47}',
                                'DoctorName': '{48}',
                                'IsDrugAllergy': '{49}',
                                'AllergyDrugCode': '{50}',
                                'AllergyDrugName': '{51}',
                                'IsPathologicalExamination': '{52}',
                                'PathologyCode': '{53}',
                                'IsHospitalInfected': '{54}',
                                'HospitalInfectedCode': '{55}',
                                'BloodTypeS': '{56}',
                                'BloodTypeE': '{57}',
                                'ChiefComplaint': '{58}',
                                'MedicalHistory': '{59}',
                                'SurgeryHistory': '{60}',
                                'BloodTransHistory': '{61}',
                                'Marriage': '{62}',
                                'Height': '{63}',
                                'Weight': '{64}',
                                'BearPregnancy': '{65}',
                                'BearYie': '{66}',
                                'DiagnosePosition1': '{67}',
                                'DiagnosePosition2': '{68}',
                                'Tsblbs': '{69}'";
                return str;
            }
        }

        /// <summary>
        /// 病案费用实体YYSFXX
        /// </summary>
        public static string YYSFXXJson
        {
            get
            {
                string str = @"'YWLSH': '{0}',
                                'PJDM': '{1}',
                                'PJHM': '{2}',
                                'JSRQ': '{3}',
                                'JEHJ_JE': '{4}',
                                'JEHJ_JL': '{5}',
                                'JEHJ_YL': '{6}',
                                'JEHJ_ZF': '{7}',
                                'JEHJ_QT': '{8}',
                                'CWF_JE': '{9}',
                                'CWF_JL': '{10}',
                                'CWF_YL': '{11}',
                                'CWF_ZF': '{12}',
                                'CWF_QT': '{13}',
                                'ZCF_JE': '{14}',
                                'ZCF_JL': '{15}',
                                'ZCF_YL': '{16}',
                                'ZCF_ZF': '{17}',
                                'ZCF_QT': '{18}',
                                'JCF_JE': '{19}',
                                'JCF_JL': '{20}',
                                'JCF_YL': '{21}',
                                'JCF_ZF': '{22}',
                                'JCF_QT': '{23}',
                                'HYF_JE': '{24}',
                                'HYF_JL': '{25}',
                                'HYF_YL': '{26}',
                                'HYF_ZF': '{27}',
                                'HYF_QT': '{28}',
                                'ZLF_JE': '{29}',
                                'ZLF_JL': '{30}',
                                'ZLF_YL': '{31}',
                                'ZLF_ZF': '{32}',
                                'ZLF_QT': '{33}',
                                'SSF_JE': '{34}',
                                'SSF_JL': '{35}',
                                'SSF_YL': '{36}',
                                'SSF_ZF': '{37}',
                                'SSF_QT': '{38}',
                                'HLF_JE': '{39}',
                                'HLF_JL': '{40}',
                                'HLF_YL': '{41}',
                                'HLF_ZF': '{42}',
                                'HLF_QT': '{43}',
                                'WSCLF_JE': '{44}',
                                'WSCLF_JL': '{45}',
                                'WSCLF_YL': '{46}',
                                'WSCLF_ZF': '{47}',
                                'WSCLF_QT': '{48}',
                                'XYF_JE': '{49}',
                                'XYF_JL': '{50}',
                                'XYF_YL': '{51}',
                                'XYF_ZF': '{52}',
                                'XYF_QT': '{53}',
                                'ZYYPF_JE': '{54}',
                                'ZYYPF_JL': '{55}',
                                'ZYYPF_YL': '{56}',
                                'ZYYPF_ZF': '{57}',
                                'ZYYPF_QT': '{58}',
                                'ZCYF_JE': '{59}',
                                'ZCYF_JL': '{60}',
                                'ZCYF_YL': '{61}',
                                'ZCYF_ZF': '{62}',
                                'ZCYF_QT': '{63}',
                                'YBZLF_JE': '{64}',
                                'YBZLF_JL': '{65}',
                                'YBZLF_YL': '{66}',
                                'YBZLF_ZF': '{67}',
                                'YBZLF_QT': '{68}',
                                'GHF_JE': '{69}',
                                'GHF_JL': '{70}',
                                'GHF_YL': '{71}',
                                'GHF_ZF': '{72}',
                                'GHF_QT': '{73}',
                                'QTF_JE': '{74}',
                                'QTF_JL': '{75}',
                                'QTF_YL': '{76}',
                                'QTF_ZF': '{77}',
                                'QTF_QT': '{78}',
                                'YBTCJJZF': '{79}',
                                'QTZF': '{80}',
                                'DBBXZF': '{81}',
                                'YLJZZF': '{82}',
                                'GWYYLBZ': '{83}',
                                'DEBC': '{84}',
                                'QYBC': '{85}',
                                'GRZFU': '{86}',
                                'GRZFEI': '{87}',
                                'GRZHZF': '{88}',
                                'GRXJZF': '{89}',
                                'YBZFFS': '{90}',
                                'YLJGTBBM': '{91}',
                                'YLJGTBR': '{92}',
                                'YBJG': '{93}',
                                'YBJGJBR': '{94}'";
                return str;
            }
        }

        #endregion
    }
}
