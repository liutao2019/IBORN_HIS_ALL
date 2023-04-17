using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace FoShanSiDetailUpload
{
    /// <summary>
    /// ********************************************************
    /// 功能描述：佛山市定点医疗机构就诊明细的上传接口
    /// 创建日期：2016-05-01
    /// 创 建 人：gumaozhu
    /// 修改日期：
    /// 修 改 人：
    /// 修改内容：
    /// ********************************************************
    /// </summary>
    public class Function
    {
        #region 变量和属性

        /// <summary>
        /// 医院代码  如：A-09
        /// </summary>
        private static string hospitalCode;

        /// <summary>
        /// 医院代码
        /// </summary>
        public static string HospitalCode
        {
            get
            {
                if (string.IsNullOrEmpty(Function.hospitalCode))
                {
                    Function.GetSetting();
                }
                return Function.hospitalCode;
            }
        }


        /// <summary>
        /// 账户
        /// </summary>
        private static string userID;

        /// <summary>
        /// 账户
        /// </summary>
        public static string UserID
        {
            get
            {
                if (string.IsNullOrEmpty(Function.userID))
                {
                    Function.GetSetting();
                }
                return Function.userID;
            }
        }


        /// <summary>
        /// 密码
        /// </summary>
        private static string passWord;

        /// <summary>
        /// 密码
        /// </summary>
        public static string PassWord
        {
            get
            {
                if (string.IsNullOrEmpty(Function.passWord))
                {
                    Function.GetSetting();
                }
                return Function.passWord;
            }
        }

        /// <summary>
        /// 代理路径
        /// </summary>
        private static string webServiceAddress = string.Empty;

        /// <summary>
        /// 代理路径
        /// </summary>
        public static string WebServiceAddress
        {
            get
            {
                if (string.IsNullOrEmpty(Function.webServiceAddress))
                {
                    Function.GetSetting();
                }
                return Function.webServiceAddress;
            }
        }
        /// <summary>
        /// 登录交易号[100]
        /// </summary>
        /// <returns></returns>
        public static string LoginTransNO
        {
            get
            {
                return "100";
            }
        }

        /// <summary>
        /// 医院口令修改交易号[101]
        /// </summary>
        public static string ChangePwTransNO
        {
            get
            {
                return "101";
            }
        }

        /// <summary>
        /// 主单信息上传[JJJG01]
        /// </summary>
        public static string MainInfoTransNO
        {
            get
            {
                return "JJJG01";
            }
        }

        /// <summary>
        /// 费用明细信息上传[JJJG02]
        /// </summary>
        public static string FeeDetailTransNO
        {
            get
            {
                return "JJJG02";
            }
        }

        /// <summary>
        /// 住院医嘱上传[JJJG03]
        /// </summary>
        public static string InOrderTransNO
        {
            get
            {
                return "JJJG03";
            }
        }

        /// <summary>
        /// 住院检治执行上传[JJJG04]
        /// </summary>
        public static string InExecUndrugTransNO
        {
            get
            {
                return "JJJG04";
            }
        }

        /// <summary>
        /// 住院药品执行上传[JJJG05]
        /// </summary>
        public static string InExecDrugTransNO
        {
            get
            {
                return "JJJG05";
            }
        }

        /// <summary>
        /// 门诊费用明细上传[JJJG06]
        /// </summary>
        public static string OutFeeDetailTransNO
        {
            get
            {
                return "JJJG06";
            }
        }

        /// <summary>
        /// 病案信息上传[JJJG07]
        /// </summary>
        public static string InCaseTransNO
        {
            get
            {
                return "JJJG07";
            }
        }    /// <summary>
        /// 配置文件
        /// </summary>
        private static string profileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\Setting\DetailUploadSIConfig.xml";


        /// <summary>
        /// 读取社保配置文件
        /// </summary>
        private static void GetSetting()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(profileName);

            XmlNode node = doc.SelectSingleNode(@"/SIConfig/hospitalCode");
            Function.hospitalCode = node.InnerText.Trim();

            node = doc.SelectSingleNode(@"/SIConfig/webServiceAddress");
            Function.webServiceAddress = node.InnerText.Trim();


            node = doc.SelectSingleNode(@"/SIConfig/userID");
            Function.userID = node.InnerText.Trim();

            node = doc.SelectSingleNode(@"/SIConfig/passWord");
            Function.passWord = node.InnerText.Trim();
        }
        #endregion

        #region 零星报销-入参数封装XML

        /// <summary>
        /// 医院登录[100]
        /// </summary>
        public static string LoginXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
	                                <!-- 0 医院编码  ckz543  varchar2(8) 不可为空 -->
	                                <ckz543>{0}</ckz543>
	                                <!-- 1 用户id  operid  varchar2(20) 不可为空 -->
	                                <operid>{1}</operid>
	                                <!-- 2 口令  password  varchar2(32) 不可为空 -->
	                                <password>{2}</password>
	                                <!-- 3 经办人类型  opertype  varchar2(3) (1普通经办人 2门诊收费员) 不可为空 -->
	                                <opertype>{3}</opertype>
	                                <!-- 4 经办人类型  clienttype  varchar2(20) (预留，传入“1”) 不可为空 -->
	                                <clienttype>{4}</clienttype>
                                </input>";

                return str;
            }
        }

        /// <summary>
        /// 医院口令修改[101]
        /// </summary>
        public static string ChangePwXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
                                    <!-- 0 医院编码  ckz543  varchar2(8) 不可为空 -->
                                    <ckz543>{0}</ckz543>
                                    <!-- 1 用户id  operid  varchar2(20) 不可为空 -->
                                    <operid>{1}</operid>
                                    <!-- 2 原口令  oldpwd  varchar2(32) 不可为空 -->
                                    <oldpwd>{2}</oldpwd>
                                    <!-- 3 新口令  newpwd  varchar2(32) 不可为空 -->
                                    <newpwd>{3}</newpwd>
                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 通用头信息
        /// </summary>
        public static string CommonHeadXML
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
	                                <!-- 3 明细 -->
                                    <dataset>{3}</dataset>
                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 主单信息上传[JJJG01]
        /// </summary>
        public static string MainInfoXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 唯一流水号  wylsh  varchar2(200) 不可为空 (单据主键，唯一标识：患者类别-发票电脑号-交易类型) -->
                                 <wylsh>{0}</wylsh>
                                 <!-- 1 收费单据号  sfdjh  varchar2(200) 不可为空 (如无可为空) -->
                                 <sfdjh>{1}</sfdjh>
                                 <!-- 2 单据结算日期  djjsrq  date 不可为空 (YYYYMMDD) -->
                                 <djjsrq>{2}</djjsrq>
                                 <!-- 3 定点机构编码  ddjgbm  varchar2(20) 不可为空 -->
                                 <ddjgbm>{3}</ddjgbm>
                                 <!-- 4 定点机构名称  ddjgmc  varchar2(100) 不可为空 -->
                                 <ddjgmc>{4}</ddjgmc>
                                 <!-- 5 费用发生机构编码  fyfsjgbm  varchar2(20) (如无可为空)-->
                                 <fyfsjgbm>{5}</fyfsjgbm>
                                 <!-- 6 费用发生机构名称  fyfsjgmc  varchar2(100) (如无可为空) -->
                                 <fyfsjgmc>{6}</fyfsjgmc>
                                 <!-- 7 转外机构名称  zwjgmc  varchar2(20) (如无可为空) -->
                                 <zwjgmc>{7}</zwjgmc>
                                 <!-- 8 身份证号码  gmsfhm  varchar2(20) 不可为空 -->
                                 <gmsfhm>{8}</gmsfhm>
                                 <!-- 9 参保人名称  cbrmc  varchar2(20) 不可为空 -->
                                 <cbrmc>{9}</cbrmc>
                                 <!-- 10 参保人性别  cbrxb  varchar2(1) 不可为空 (1男0女) -->
                                 <cbrxb>{10}</cbrxb>
                                 <!-- 11 参保人出生日期  cbrcsrq  date (YYYYMMDD) -->
                                 <cbrcsrq>{11}</cbrcsrq>
                                 <!-- 12 人员类别编码  rylbbm  varchar2(10) 可为空 (参考人员类别字典表) -->
                                 <rylbbm>{12}</rylbbm>
                                 <!-- 13 就诊方式编码  jzfsbm  varchar2(10) 不可为空 (参考就医方式字典表) -->
                                 <jzfsbm>{13}</jzfsbm>
                                 <!-- 14 是否异地就医  sfydjy  varchar2(1) 可为空 (标识是否为异地就医，1是0否)-->
                                 <sfydjy>{14}</sfydjy>
                                 <!-- 15 入院诊断编码  ryzdbm  varchar2(20) 不可为空 (门诊单据为主要诊断编码) -->
                                 <ryzdbm>{15}</ryzdbm>
                                 <!-- 16 出院诊断编码  cyzdbm  varchar2(20) 不可为空 (门诊单据为次要诊断编码)-->
                                 <cyzdbm>{16}</cyzdbm>
                                 <!-- 17 第一副诊断编码  fzd1  varchar2(20) 可为空 -->
                                 <fzd1>{17}</fzd1>
                                 <!-- 18 第二副诊断编码  fzd2  varchar2(20) 可为空-->
                                 <fzd2>{18}</fzd2>
                                 <!-- 19 第三副诊断编码  fzd3  varchar2(20) 可为空 -->
                                 <fzd3>{19}</fzd3>
                                 <!-- 20 第四副诊断编码  fzd4 varchar2(20) 可为空 -->
                                 <fzd4>{20}</fzd4>
                                 <!-- 21 第五副诊断编码  fzd5 varchar2(20) 可为空 -->
                                 <fzd5>{21}</fzd5>
                                 <!-- 22 第六副诊断编码  fzd6 varchar2(20) 可为空 -->
                                 <fzd6>{22}</fzd6>
                                 <!-- 23 第七副诊断编码  fzd7 varchar2(20) 可为空 -->
                                 <fzd7>{23}</fzd7>
                                 <!-- 24 第八副诊断编码  fzd8 varchar2(20) 可为空 -->
                                 <fzd8>{24}</fzd8>
                                 <!-- 25 第九副诊断编码  fzd9 varchar2(20) 可为空 -->
                                 <fzd9>{25}</fzd9>
                                 <!-- 26 第十副诊断编码  fzd10 varchar2(20) 可为空 -->
                                 <fzd10>{26}</fzd10>
                                 <!-- 27 第十一副诊断编码  fzd11 varchar2(20) 可为空 -->
                                 <fzd11>{27}</fzd11>
                                 <!-- 28 第十二副诊断编码  fzd12 varchar2(20) 可为空 -->
                                 <fzd12>{28}</fzd12>
                                 <!-- 29 第十三副诊断编码  fzd13 varchar2(20) 可为空 -->
                                 <fzd13>{29}</fzd13>
                                 <!-- 30 第十四副诊断编码  fzd14 varchar2(20) 可为空 -->
                                 <fzd14>{30}</fzd14>
                                 <!-- 31 第十五副诊断编码  fzd15 varchar2(20) 可为空 -->
                                 <fzd15>{31}</fzd15>
                                 <!-- 32 第十六副诊断编码  fzd16 varchar2(20) 可为空 -->
                                 <fzd16>{32}</fzd16>
                                 <!-- 33 是否特病慢病单据标志  sftbmbdjbz varchar2(1) 可为空 (标识是否为特病慢病单据，1是0否，1是0否) -->
                                 <sftbmbdjbz>{33}</sftbmbdjbz>
                                 <!-- 34 慢病特病代码  mbtbdm varchar2(20) 可为空 (如无可为空) -->
                                 <mbtbdm>{34}</mbtbdm>
                                 <!-- 35 入院日期  ryrq date 不可为空 (YYYYMMDD) -->
                                 <ryrq>{35}</ryrq>
                                 <!-- 36 出院日期  cyrq date 不可为空 (YYYYMMDD) -->
                                 <cyrq>{36}</cyrq>
                                 <!-- 37 就诊日期  jzrq date 不可为空 (YYYYMMDD) -->
                                 <jzrq>{37}</jzrq>
                                 <!-- 38 是否怀孕  sfhy varchar2(1) 不可为空 (标识参保人是否怀孕，1是0否) -->
                                 <sfhy>{38}</sfhy>
                                 <!-- 39 是否哺乳期  sfbrq varchar2(1) 不可为空 (标识是否处于哺乳期，1是0否) -->
                                 <sfbrq>{39}</sfbrq>
                                 <!-- 40 身高  sg number(12,2) 可为空-->
                                 <sg>{40}</sg>
                                 <!-- 41 体重  tz number(12,2) 可为空-->
                                 <tz>{41}</tz>
                                 <!-- 42 血压  xy varchar2(20) 可为空 (收缩压/舒张压)-->
                                 <xy>{42}</xy>
                                 <!-- 43 空腹血糖  kfxt number(12,2) 可为空 -->
                                 <kfxt>{43}</kfxt>
                                 <!-- 44 餐后血糖  chxt number(12,2) 可为空 -->
                                 <chxt>{44}</chxt>
                                 <!-- 45 体温  tw varchar2(20) 可为空 (数量单位为“摄氏度”)-->
                                 <tw>{45}</tw>
                                 <!-- 46 心率  xl varchar2(20) 可为空 (数量单位为“次分钟”)-->
                                 <xl>{46}</xl>
                                 <!-- 47 是否转入院  sfzry varchar2(1) 不可为空 (标识是否为转入院，1是0否)-->
                                 <sfzry>{47}</sfzry>
                                 <!-- 48 医保中心代码  ybzxdm varchar2(20) 不可为空 (建议为各医院所在市辖区编号)-->
                                 <ybzxdm>{48}</ybzxdm>
                                 <!-- 49 异地人员类别  ydrylb varchar2(20) 可为空 -->
                                 <ydrylb>{49}</ydrylb>
                                 <!-- 50 异地参保地行政区域代码  ydcbxzqydm varchar2(20) 可为空 -->
                                 <ydcbxzqydm>{50}</ydcbxzqydm>
                                 <!-- 51 待遇类型  dylx varchar2(20) 不可为空 (如无可为空)-->
                                 <dylx>{51}</dylx>
                                 <!-- 52 参保类型  cblx varchar2(20) 不可为空 (参考参保类型字典表)-->
                                 <cblx>{52}</cblx>
                                 <!-- 53 总金额  zje number(12,2) 不可为空-->
                                 <zje>{53}</zje>
                                 <!-- 54 医保内总金额  ybnzje number(12,2) 不可为空 (医保基金支付金额，包含统筹基金、专项基金等。)-->
                                 <ybnzje>{54}</ybnzje>
                                 <!-- 55 医保内自付金额  ybnzfje number(12,2) 可为空 -->
                                 <ybnzfje>{55}</ybnzfje>
                                 <!-- 56 自费金额  zfbl number(12,2) 可为空 -->
                                 <zfbl>{56}</zfbl>
                                 <!-- 57 支付比例  zhfbl number(12,2) 不可为空 (医保基金支付金额占总费用比例)-->
                                 <zhfbl>{57}</zhfbl>
                                 <!-- 58 西药费  xyf number(12,2) 可为空 (提取自病案首页)-->
                                 <xyf>{58}</xyf>
                                 <!-- 59 中成药费  zchyf number(12,2) 可为空 (提取自病案首页)-->
                                 <zchyf>{59}</zchyf>
                                 <!-- 60 中草药费  zcyf number(12,2) 可为空 (提取自病案首页)-->
                                 <zcyf>{60}</zcyf>
                                 <!-- 61 一般医疗服务费  ybylfwf number(12,2) 可为空 (提取自病案首页)-->
                                 <ybylfwf>{61}</ybylfwf>
                                 <!-- 62 一般治疗操作费  ybzlczf number(12,2) 可为空 (提取自病案首页)-->
                                 <ybzlczf>{62}</ybzlczf>
                                 <!-- 63 护理费  hlf number(12,2) 可为空 (提取自病案首页)-->
                                 <hlf>{63}</hlf>
                                 <!-- 64 病理诊断费  blzdf number(12,2) 可为空 (提取自病案首页)-->
                                 <blzdf>{64}</blzdf>
                                 <!-- 65 实验室诊断费  syszdf number(12,2) 可为空 (提取自病案首页)-->
                                 <syszdf>{65}</syszdf>
                                 <!-- 66 影像学诊断费  yxxzdf number(12,2) 可为空 (提取自病案首页)-->
                                 <yxxzdf>{66}</yxxzdf>
                                 <!-- 67 临床诊断项目费  lczdxmf number(12,2) 可为空 (提取自病案首页)-->
                                 <lczdxmf>{67}</lczdxmf>
                                 <!-- 68 手术治疗费  sszlf number(12,2) 可为空 (提取自病案首页)-->
                                 <sszlf>{68}</sszlf>
                                 <!-- 69 麻醉费  mjf number(12,2) 可为空 (提取自病案首页)-->
                                 <mjf>{69}</mjf>
                                 <!-- 70 非手术治疗项目费  fsszlxmf number(12,2) 可为空 (提取自病案首页)-->
                                 <fsszlxmf>{70}</fsszlxmf>
                                 <!-- 71 临床物理治疗费  lcwlzlf number(12,2) 可为空 (提取自病案首页)-->
                                 <lcwlzlf>{71}</lcwlzlf>
                                 <!-- 72 康复费  kff number(12,2) 可为空 (提取自病案首页)-->
                                 <kff>{72}</kff>
                                 <!-- 73 血费  xf number(12,2) 可为空 (提取自病案首页)-->
                                 <xf>{73}</xf>
                                 <!-- 74 白蛋白类制品费  bdblzpf number(12,2) 可为空 (提取自病案首页)-->
                                 <bdblzpf>{74}</bdblzpf>
                                 <!-- 75 球蛋白类制品费  qdblzpf number(12,2) 可为空 (提取自病案首页)-->
                                 <qdblzpf>{75}</qdblzpf>
                                 <!-- 76 凝血因子类制品费  nxyzlzpf number(12,2) 可为空 (提取自病案首页)-->
                                 <nxyzlzpf>{76}</nxyzlzpf>
                                 <!-- 77 细胞因子类制品费  xbyzlzpf number(12,2) 可为空 (提取自病案首页)-->
                                 <xbyzlzpf>{77}</xbyzlzpf>
                                 <!-- 78 检查用一次性医用材料费  jcyclf number(12,2) 可为空 (提取自病案首页)-->
                                 <jcyclf>{78}</jcyclf>
                                 <!-- 79 治疗用一次性医用材料费  zlyclf number(12,2) 可为空 (提取自病案首页)-->
                                 <zlyclf>{79}</zlyclf>
                                 <!-- 80 手术用一次性医用材料费  ssyclf number(12,2) 可为空 (提取自病案首页)-->
                                 <ssyclf>{80}</ssyclf>
                                 <!-- 81 门诊/住院号  mzzyh varchar2(50) 不可为空 -->
                                 <mzzyh>{81}</mzzyh>
                                 <!-- 82 残疾人标识  cjrbs varchar2(1) 可为空 (标识是否为残疾人，1是0否) -->
                                 <cjrbs>{82}</cjrbs>
                                 <!-- 83 医院类别  yylb varchar2(1) 不可为空 (参考医院类别字典表) -->
                                 <yylb>{83}</yylb>
                                 <!-- 84 冲减单据号  cjdjh varchar2(200) 不可为空 (该条单据去冲减单据的单据号) -->
                                 <cjdjh>{84}</cjdjh>
                                 <!-- 85 交易类型  jjlx varchar2(1) 不可为空 (是否为冲减单据，0为正常单据，1为被冲减单据，2为冲减单据) -->
                                 <jjlx>{85}</jjlx>
                                 <!-- 86 手术代码  ssdm varchar2(100) 可为空 (ICD-9-CM编码) -->
                                 <ssdm>{86}</ssdm>
                                 <!-- 87 手术切口分类  ssqkfl varchar2(10) 不可为空 (参考切口愈合等级字典表) -->
                                 <ssqkfl>{87}</ssqkfl>
                                 <!-- 88 出院31天再入院计划  cyzry varchar2(1) 可为空 (1：无；2：有) -->
                                 <cyzry>{88}</cyzry>
                            </row>";
                return str;
            }
        }

        /// <summary>
        /// 费用明细信息上传[JJJG02]
        /// </summary>
        public static string FeeDetailXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 主单编码  zdbm  varchar2(200) 不可为空 (所属主单编码，该字段与主单信息唯一流水号对应) -->
                                 <zdbm>{0}</zdbm>
                                 <!-- 1 费用明细唯一流水号  wylsh  varchar2(200) 不可为空 (明细主键，唯一标识字段) -->
                                 <wylsh>{1}</wylsh>
                                 <!-- 2 服务日期  fwrq  date 不可为空 (开处方日期 YYYYMMDD) -->
                                 <fwrq>{2}</fwrq>
                                 <!-- 3 项目编码  xmbm  varchar2(200) 不可为空 (三大目录产品唯一编码) -->
                                 <xmbm>{3}</xmbm>
                                 <!-- 4 项目名称  xmmc  varchar2(200) 不可为空 -->
                                 <xmmc>{4}</xmmc>
                                 <!-- 5 项目类别  xmlb  varchar2(200) 不可为空 (1为“药品”，2为“诊疗/服务设施”)-->
                                 <xmlb>{5}</xmlb>
                                 <!-- 6 单价  dj  number(12,2) 不可为空 (以医保要求为准) -->
                                 <dj>{6}</dj>
                                 <!-- 7 数量  sl  number(12,2) 不可为空 -->
                                 <sl>{7}</sl>
                                 <!-- 8 总价  zj  number(12,2) 不可为空 -->
                                 <zj>{8}</zj>
                                 <!-- 9 医生编码  ysbm  varchar2(200) 不可为空 (医师库支持) -->
                                 <ysbm>{9}</ysbm>
                                 <!-- 10 医生名称  ysmc  varchar2(200) 不可为空 (医师库支持) -->
                                 <ysmc>{10}</ysmc>
                                 <!-- 11 科室编码  ksbm  varchar2(200) 不可为空 (科室库支持) -->
                                 <ksbm>{11}</ksbm>
                                 <!-- 12 科室名称  ksmc  varchar2(200) 不可为空 (科室库支持) -->
                                 <ksmc>{12}</ksmc>
                                 <!-- 13 用法  yf  varchar2(200) 不可为空  -->
                                 <yf>{13}</yf>
                                 <!-- 14 给药途径  gytj  varchar2(200) 可为空 (参考字典信息表中表7)-->
                                 <gytj>{14}</gytj>
                                 <!-- 15 用量  yl  number(12,2) 不可为空  -->
                                 <yl>{15}</yl>
                                 <!-- 16 频次  pc  varchar2(200) 不可为空 (参考字典信息表中表6)-->
                                 <pc>{16}</pc>
                                 <!-- 17 用药天数  yyts  number(12,2) 不可为空 -->
                                 <yyts>{17}</yyts>
                                 <!-- 18 医保内金额  ybnje  number(12,2) 可为空 (医保基金支付金额，包含统筹基金、专项基金等)-->
                                 <ybnje>{18}</ybnje>
                                 <!-- 19 交易类型  jylx  varchar2(1) 不可为空 (标识是否为冲减单据，1是0否) -->
                                 <jylx>{19}</jylx>
                                 <!-- 20 药品规格  ypgg varchar2(50) 不可为空 (制剂规格：发药规格) -->
                                 <ypgg>{20}</ypgg>
                                 <!-- 21 出院带药标识  cydybs varchar2(1) 不可为空 (标识是否为出院带药，1是0否) -->
                                 <cydybs>{21}</cydybs>
                                 <!-- 22 门诊/住院号  zyh varchar2(50) 不可为空 (就诊记录号)-->
                                 <zyh>{22}</zyh>
                                 <!-- 23 医嘱ID  yzzdid varchar2(50) 不可为空 (医嘱主键，唯一标识字段)-->
                                 <yzzdid>{23}</yzzdid>
                                 <!-- 24 医嘱明细ID  yzmxid varchar2(50) (医嘱明细主键，唯一标识字段) -->
                                 <yzmxid>{24}</yzmxid>
                            </row>";
                return str;
            }
        }

        /// <summary>
        /// 住院医嘱上传[JJJG03]
        /// </summary>
        public static string InOrderXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 住院号  zyh  varchar2(50) 不可为空 (就诊记录号) -->
                                 <zyh>{0}</zyh>
                                 <!-- 1 医嘱ID  yzid  varchar2(50) 不可为空 (明细主键，唯一标识字段) -->
                                 <yzid>{1}</yzid>
                                 <!-- 2 医嘱明细ID  yzmxid  varchar2(50) 不可为空 (医嘱明细主键，唯一标识字段) -->
                                 <yzmxid>{2}</yzmxid>
                                 <!-- 3 病人科室  brks  varchar2(20) 可为空 (科室库支持) -->
                                 <brks>{3}</brks>
                                 <!-- 4 药品/项目ID  ypxmid  varchar2(100) 可为空 -->
                                 <ypxmid>{4}</ypxmid>
                                 <!-- 5 首次数量  scsl  number(12,2) 可为空 -->
                                 <scsl>{5}</scsl>
                                 <!-- 6 末次数量  mcsl  number(12,2) 可为空  -->
                                 <mcsl>{6}</mcsl>
                                 <!-- 7 本次用量  bcyl  number(12,2) 可为空 -->
                                 <bcyl>{7}</bcyl>
                                 <!-- 8 本次数量  bcsl  number(12,2) 可为空 -->
                                 <bcsl>{8}</bcsl>
                                 <!-- 9 单位  dw  varchar2(50) 可为空  -->
                                 <dw>{9}</dw>
                                 <!-- 10 开始时间  kssj  date 可为空 (YYYYMMDD) -->
                                 <kssj>{10}</kssj>
                                 <!-- 11 结束时间  jssj  date 可为空 (YYYYMMDD) -->
                                 <jssj>{11}</jssj>
                                 <!-- 12 执行科室  zxks  varchar2(20) 可为空 (科室库支持) -->
                                 <zxks>{12}</zxks>
                                 <!-- 13 开医嘱人  kyzr  varchar2(20) 可为空 (医师库支持)  -->
                                 <kyzr>{13}</kyzr>
                                 <!-- 14 停医嘱人  tyzr  varchar2(20) 可为空 (医师库支持)-->
                                 <tyzr>{14}</tyzr>
                                 <!-- 15 医生编码  ysbm  varchar2(20) 可为空 (医师库支持)  -->
                                 <ysbm>{15}</ysbm>
                                 <!-- 16 医嘱类型  yzlx  varchar2(1) 可为空 (1-长嘱 2-临嘱 3-其他)-->
                                 <yzlx>{16}</yzlx>
                                 <!-- 17 金额  je  number(12,2) 可为空 -->
                                 <je>{17}</je>
                                 <!-- 18 是否主医嘱  sfzyz  varchar2(1) 可为空 (标识是否为主医嘱，1是0否)-->
                                 <sfzyz>{18}</sfzyz>
                                 <!-- 19 是否收费  sfsf  varchar2(1) 可为空 (标识是否收费，1是0否) -->
                                 <sfsf>{19}</sfsf>
                                 <!-- 20 是否取消  sfqx varchar2(1) 可为空 (标识是否取消，1是0否) -->
                                 <sfqx>{20}</sfqx>
                                 <!-- 21 临嘱天数  lzts number(12,2) 可为空 -->
                                 <lzts>{21}</lzts>
                                 <!-- 22 发药方式  fyfs varchar2(50) 可为空 (1.病房2.病区3.中心药房)-->
                                 <fyfs>{22}</fyfs>
                                 <!-- 23 是否新医嘱  sfxyz varchar2(1) 可为空 (标识是否为新医嘱，1是0否)-->
                                 <sfxyz>{23}</sfxyz>
                                 <!-- 24 执行时间  zxsj date 可为空 (YYYYMMDD) -->
                                 <zxsj>{24}</zxsj>
                            </row>";
                return str;
            }
        }

        /// <summary>
        /// 住院检治执行上传[JJJG04]
        /// </summary>
        public static string InExecUndrugXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 住院号  zyh  varchar2(50) 不可为空 (就诊记录号) -->
                                 <zyh>{0}</zyh>
                                 <!-- 1 医嘱ID  yzid  varchar2(50) 不可为空 (明细主键，唯一标识字段) -->
                                 <yzid>{1}</yzid>
                                 <!-- 2 医嘱明细ID  yzmxid  varchar2(50) 不可为空 (医嘱明细主键，唯一标识字段) -->
                                 <yzmxid>{2}</yzmxid>
                                 <!-- 3 患者ID  hzid  varchar2(50) 可为空 (参保人库支持) -->
                                 <hzid>{3}</hzid>
                                 <!-- 4 项目ID  xmid  varchar2(100) 可为空 (医院内部ID)-->
                                 <xmid>{4}</xmid>
                                 <!-- 5 项目名称  xmmc  varchar2(100) 可为空(医院内部名称) -->
                                 <xmmc>{5}</xmmc>
                                 <!-- 6 数量  sl  number(12,2) 可为空  -->
                                 <sl>{6}</sl>
                                 <!-- 7 单价  dj  number(12,2) 可为空 -->
                                 <dj>{7}</dj>
                                 <!-- 8 合计  hj  number(12,2) 可为空 -->
                                 <hj>{8}</hj>
                                 <!-- 9 病人科室  brks  varchar2(20) 可为空 (科室库支持)  -->
                                 <brks>{9}</brks>
                                 <!-- 10 执行科室  zxks  varchar2(20) 可为空 (科室库支持)-->
                                 <zxks>{10}</zxks>
                                 <!-- 11 核算科室  hsks  varchar2(20) 可为空 (科室库支持) -->
                                 <hsks>{11}</hsks>
                                 <!-- 12 医嘱类型  yzlx  varchar2(1) 可为空 (1-长嘱 2-临嘱 3-其他) -->
                                 <yzlx>{12}</yzlx>
                                 <!-- 13 费用日期  fyrq  date 可为空 (YYYYMMDD)  -->
                                 <fyrq>{13}</fyrq>
                                 <!-- 14 记帐日期  jzrq  date 可为空 (YYYYMMDD)-->
                                 <jzrq>{14}</jzrq>
                                 <!-- 15 执行时间  zxsj  date 可为空 (YYYYMMDD)  -->
                                 <zxsj>{15}</zxsj>
                                 <!-- 16 是否收费  sfsf  varchar2(1) 可为空 (标识是否收费，1是0否)-->
                                 <sfsf>{16}</sfsf>
                                 <!-- 17 医生编码  ysbm  varchar2(20) 可为空 (医师库支持) -->
                                 <ysbm>{17}</ysbm>
                                 <!-- 18 执行人  zxr  varchar2(20) 可为空 (医师库支持)-->
                                 <zxr>{18}</zxr>
                                 <!-- 19 是否现金结算方式  sfxjjs  varchar2(1) 可为空 (标识是否为现金结算，1是0否) -->
                                 <sfxjjs>{19}</sfxjjs>
                            </row>";
                return str;
            }
        }

        /// <summary>
        /// 住院药品执行上传[JJJG05]
        /// </summary>
        public static string InExecDrugXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 住院号  zyh  varchar2(50) 不可为空 (就诊记录号) -->
                                 <zyh>{0}</zyh>
                                 <!-- 1 医嘱ID  yzid  varchar2(50) 不可为空 (明细主键，唯一标识字段) -->
                                 <yzid>{1}</yzid>
                                 <!-- 2 医嘱明细ID  yzmxid  varchar2(50) 不可为空 (医嘱明细主键，唯一标识字段) -->
                                 <yzmxid>{2}</yzmxid>
                                 <!-- 3 患者ID  hzid  varchar2(50) 可为空 (参保人库支持) -->
                                 <hzid>{3}</hzid>
                                 <!-- 4 项目ID  xmid  varchar2(100) 可为空 (医院内部ID)-->
                                 <xmid>{4}</xmid>
                                 <!-- 5 项目名称  xmmc  varchar2(100) 可为空(医院内部名称) -->
                                 <xmmc>{5}</xmmc>
                                 <!-- 6 数量  sl  number(12,2) 可为空  -->
                                 <sl>{6}</sl>
                                 <!-- 7 单位  dw  varchar2(20) 可为空 -->
                                 <dw>{7}</dw>
                                 <!-- 8 单价  dj  number(12,2) 可为空 -->
                                 <dj>{8}</dj>
                                 <!-- 9 合计  hj  number(12,2) 可为空  -->
                                 <hj>{9}</hj>
                                 <!-- 10 打折  dz  number(12,2) 可为空-->
                                 <dz>{10}</dz>
                                 <!-- 11 申请ID  sqid  varchar2(20) 可为空 (医师库支持) -->
                                 <sqid>{11}</sqid>
                                 <!-- 12 病人科室  brks  varchar2(20) 可为空 (科室库支持) -->
                                 <brks>{12}</brks>
                                 <!-- 13 执行科室  zxks  varchar2(20) 可为空 (科室库支持)  -->
                                 <zxks>{13}</zxks>
                                 <!-- 14 费用日期  fyriq  date 可为空 (YYYYMMDD)-->
                                 <fyriq>{14}</fyriq>
                                 <!-- 15 记帐日期  jzrq  date 可为空 (YYYYMMDD)  -->
                                 <jzrq>{15}</jzrq>
                                 <!-- 16 发药日期  fyrq  date 可为空 (YYYYMMDD) -->
                                 <fyrq>{16}</fyrq>
                                 <!-- 17 是否收费  sfsf  varchar2(1) 可为空 (标识是否收费，1是0否) -->
                                 <sfsf>{17}</sfsf>
                                 <!-- 18 是否执行  sfzx  varchar2(1) 可为空 (标识是否执行，1是0否)-->
                                 <sfzx>{18}</sfzx>
                                 <!-- 19 发药时间  fysj  date 可为空 (YYYYMMDD) -->
                                 <fysj>{19}</fysj>
                                 <!-- 20 是否发药  sffy  varchar2(1) 可为空 (标识是否发药，1是0否) -->
                                 <sffy>{20}</sffy>
                                 <!-- 21 医嘱类型  yzlx  varchar2(1) 可为空 (1-长嘱 2-临嘱 3-其他) -->
                                 <yzlx>{21}</yzlx>
                                 <!-- 22 发药类型  fylx  varchar2(1) 可为空 (1-长嘱 2-临嘱 3-其他) -->
                                 <fylx>{22}</fylx>
                                 <!-- 23 发药方式  fyfs  varchar2(1) 可为空 (1.病房2.病区3.中心药房) -->
                                 <fyfs>{23}</fyfs>
                                 <!-- 24 医生ID  ysid  varchar2(20) 可为空 (医师库支持) -->
                                 <ysid>{24}</ysid>
                                 <!-- 25 发药人  fyr  varchar2(20) 可为空 (医师库支持) -->
                                 <fyr>{25}</fyr>
                                 <!-- 26 发药单据号  fydjh  varchar2(100) 可为空  -->
                                 <fydjh>{26}</fydjh>
                                 <!-- 27 退药单据号  dydjh  varchar2(100) 可为空  -->
                                 <dydjh>{27}</dydjh>
                                 <!-- 28 药品最小拆分数量  ypzxcfsl  number(12,2) 可为空  -->
                                 <ypzxcfsl>{28}</ypzxcfsl>
                                 <!-- 29 是否现金结算  sfxjjs  varchar2(20) 可为空 (标识是否为现金结算，1是0否)  -->
                                 <sfxjjs>{29}</sfxjjs>
                            </row>";
                return str;
            }
        }

        /// <summary>
        /// 门诊费用明细上传[JJJG06]
        /// </summary>
        public static string OutFeeDetailXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 费用明细唯一流水号  fymxwylsh  varchar2(200) 不可为空 (明细主键，唯一标识字段) -->
                                 <fymxwylsh>{0}</fymxwylsh>
                                 <!-- 1 门诊号  mzh  varchar2(50) 不可为空 (就诊记录号) -->
                                 <mzh>{1}</mzh>
                                 <!-- 2 项目ID  xmid  varchar2(100) 不可为空 (医院内部ID) -->
                                 <xmid>{2}</xmid>
                                 <!-- 3 项目名称  xmmc  varchar2(100) 不可为空 (医院内部名称) -->
                                 <xmmc>{3}</xmmc>
                                 <!-- 4 规格  gg  varchar2(50) 可为空 -->
                                 <gg>{4}</gg>
                                 <!-- 5 单价  dj  number(12,2) 不可为空 -->
                                 <dj>{5}</dj>
                                 <!-- 6 单位  dw  varchar2(50) 可为空  -->
                                 <dw>{6}</dw>
                                 <!-- 7 数量  sl  number(12,2) 可为空 -->
                                 <sl>{7}</sl>
                                 <!-- 8 金额  je  number(12,2) 不可为空 -->
                                 <je>{8}</je>
                                 <!-- 9 是否收费  sfsf  varchar2(1) 不可为空 (标识是否收费，1是0否)  -->
                                 <sfsf>{9}</sfsf>
                                 <!-- 10 类别  lb  varchar2(50) 可为空-->
                                 <lb>{10}</lb>
                            </row>";
                return str;
            }
        }

        /// <summary>
        /// 病案信息上传[JJJG07]
        /// </summary>
        public static string InCaseXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 定点机构编码  ddjgbm  varchar2(20) 可为空 (定点结构库支持) -->
                                 <ddjgbm>{0}</ddjgbm>
                                 <!-- 1 身份证号码  gmsfhm  varchar2(20) 可为空 (参保人库支持) -->
                                 <gmsfhm>{1}</gmsfhm>
                                 <!-- 2 门诊/住院号  mzzyh  varchar2(50) 可为空 (YYYYMMDD) -->
                                 <mzzyh>{2}</mzzyh>
                                 <!-- 3 残疾人标识  cjrbs  varchar2(1) 可为空 (标识是否为残疾人，1是0否) -->
                                 <cjrbs>{3}</cjrbs>
                                 <!-- 4 支付比例  zfbl  number(10,2) 可为空 (医保基金支付金额占总费用比例) -->
                                 <zfbl>{4}</zfbl>
                                 <!-- 5 医院类别  yylb  varchar2(10) 可为空-->
                                 <yylb>{5}</yylb>
                                 <!-- 6 交易类型  jylx  varchar2(1) 可为空 (是否为冲减单据，1是，0否) -->
                                 <jylx>{6}</jylx>
                                 <!-- 7 冲减单据号  cjdjh  varchar2(200) 可为空  -->
                                 <cjdjh>{7}</cjdjh>
                                 <!-- 8 人员保障卡号  rybzh  varchar2(50) 可为空 -->
                                 <rybzh>{8}</rybzh>
                                 <!-- 9 就诊次数  jzcs  varchar2(10) 可为空 -->
                                 <jzcs>{9}</jzcs>
                                 <!-- 10 医生编码  ysbm  varchar2(50) 可为空 (医生ID) -->
                                 <ysbm>{10}</ysbm>
                                 <!-- 11 医生级别  ysjb  varchar2(50) 可为空 (住院医师/主治医师/副主任医师/主任医师) -->
                                 <ysjb>{11}</ysjb>
                                 <!-- 12 是否肝功能不全  sfggnbq  varchar2(1) 可为空 (标识参保人是否肝功能不全，1是0否) -->
                                 <sfggnbq>{12}</sfggnbq>
                                 <!-- 13 是否肾功能不全  sfsgnbq  varchar2(1) 可为空 (标识参保人是否肾功能不全，1是0否) -->
                                 <sfsgnbq>{13}</sfsgnbq>
                                 <!-- 14 过敏史  gms  varchar2(10) 可为空 -->
                                 <gms>{14}</gms>
                                 <!-- 15 过敏原  gmy  varchar2(100) 可为空  -->
                                 <gmy>{15}</gmy>
                                 <!-- 16 是否病原学检查  sfbyxjc  varchar2(1) 可为空 (标识是否为病原学检查，1是0否)-->
                                 <sfbyxjc>{16}</sfbyxjc>
                                 <!-- 17 是否微生物检验  sfwswjy  varchar2(1) 可为空 (标识是否为微生物检查，1是0否)-->
                                 <sfwswjy>{17}</sfwswjy>
                                 <!-- 18 检查时间  jcsj  date 可为空 (病原学或微生物检查时间)-->
                                 <jcsj>{18}</jcsj>
                                 <!-- 19 处方编码  cfbm  varchar2(100) 可为空 (区分单张处方) -->
                                 <cfbm>{19}</cfbm>
                                 <!-- 20 手术医师编码  ssysbm varchar2(20) 可为空 (医师库支持) -->
                                 <ssysbm>{20}</ssysbm>
                                 <!-- 21 手术医师姓名  ssysxm varchar2(20) 可为空 (医师库支持) -->
                                 <ssysxm>{21}</ssysxm>
                                 <!-- 22 手术医师I助编码  ssysizbm varchar2(20) 可为空 (医师库支持) -->
                                 <ssysizbm>{22}</ssysizbm>
                                 <!-- 23 手术医师I助姓名  ssysizxm varchar2(20) 可为空 (医师库支持) -->
                                 <ssysizxm>{23}</ssysizxm>
                                 <!-- 24 手术医师II助编码  ssysiizbm varchar2(20) 可为空 (医师库支持) -->
                                 <ssysiizbm>{24}</ssysiizbm>
                                 <!-- 25 手术医师II助姓名  ssysiizxm varchar2(20) 可为空 (医师库支持) -->
                                 <ssysiizxm>{25}</ssysiizxm>
                                 <!-- 26 麻醉师编码  mzsbm varchar2(20) 可为空 (医师库支持) -->
                                 <mzsbm>{26}</mzsbm>
                                 <!-- 27 麻醉师姓名  mzsxm varchar2(20) 可为空 (医师库支持) -->
                                 <mzsxm>{27}</mzsxm>
                                 <!-- 28 手术执行时间  sszxsj date 可为空 (YYYYMMDD)  -->
                                 <sszxsj>{28}</sszxsj>
                                 <!-- 29 麻醉方式  mzfs varchar2(10) 可为空 (参考麻醉方式字典表) -->
                                 <mzfs>{29}</mzfs>
                                 <!-- 30 手术代码  ssdm varchar2(100) 可为空 (ICD编码) -->
                                 <ssdm>{30}</ssdm>
                                 <!-- 31 手术切口分类  ssqkfl varchar2(10) 可为空 (参考切口愈合等级字典表) -->
                                 <ssqkfl>{31}</ssqkfl>
                                 <!-- 32 手术愈合分级  ssyhfj varchar2(10) 可为空 (甲/乙/丙级) -->
                                 <ssyhfj>{32}</ssyhfj>
                                 <!-- 33 手术序号  ssxh number(20) 可为空 (指本条记录的是第几个手术) -->
                                 <ssxh>{33}</ssxh>
                                 <!-- 34 主次标志  zcbz varchar2(1) 可为空 (1：主手术；2：非主手术。) -->
                                 <zcbz>{34}</zcbz>
                                 <!-- 35 医源性手术  yyxss varchar2(1) 可为空 (标识是否为医院原因导致该手术。1：是；2：否) -->
                                 <yyxss>{35}</yyxss>
                                 <!-- 36 手术并发症  ssbfz varchar2(100) 可为空 (ICD-10编码) -->
                                 <ssbfz>{36}</ssbfz>
                                 <!-- 37 院内感染  yngr varchar2(1) 可为空 (标识是否为院内感染，1是，0否) -->
                                 <yngr>{37}</yngr>
                                 <!-- 38 院内感染诊断编码  yngrzdbm varchar2(100) 可为空 (ICD-10编码) -->
                                 <yngrzdbm>{38}</yngrzdbm>
                                 <!-- 39 血型  xx varchar2(2) 可为空 (参考血型字典表) -->
                                 <xx>{39}</xx>
                                 <!-- 40 主诉  zs varchar2(500) 可为空 (患者对本次疾病的感受)-->
                                 <zs>{40}</zs>
                                 <!-- 41 症状描述  zzms varchar2(500) 可为空-->
                                 <zzms>{41}</zzms>
                                 <!-- 42 离院方式  lyfs varchar2(1) 可为空 (参考离院方式字典表)-->
                                 <lyfs>{42}</lyfs>
                                 <!-- 43 转入医院编码  zryybm varchar2(20) 可为空 (即转院时接收医院编码，定点机构库支持)-->
                                 <zryybm>{43}</zryybm>
                                 <!-- 44 转入医院名称  zryymc varchar2(100) 可为空 (即转院时接收医院名称，定点机构库支持)-->
                                 <zryymc>{44}</zryymc>
                            </row>";
                return str;
            }
        }

        #endregion

        #region 零星报销-出参数解析XML

        /// <summary>
        /// 公共出参实体
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="resultXML"></param>
        /// <returns></returns>
        public static Model.ResultHead GetResultHead(string transNO, string resultXML)
        {
            #region 参数封装形式

            //<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
            //    <result>
            //        <code>返回值 1:执行成功；否则:失败</code>
            //        <message>返回信息</message>
            //        <output>
            //            <SESSIONID>会话ID</SESSIONID>
            //        </output>
            //    </result>

            #endregion

            Model.ResultHead resultHead = new FoShanSiDetailUpload.Model.ResultHead();
            if (!string.IsNullOrEmpty(resultXML))
            {
                //节点-值
                Dictionary<string, string> dicNodeValue = new Dictionary<string, string>();
                //节点
                List<string> listNode = new List<string>();
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(resultXML);

                    //返回值 1:执行成功；否则:失败
                    listNode.Add("code");
                    listNode.Add("message");
                    listNode.Add("output");

                    string errorMsg = string.Empty; //错误信息
                    int rev = GetNodeValue(doc, listNode, "result", ref dicNodeValue, out errorMsg);
                    if (rev > 0)
                    {
                        #region 成功

                        //公共出参
                        if (dicNodeValue.ContainsKey("code"))
                        {
                            resultHead.Code = dicNodeValue["code"];
                        }
                        if (dicNodeValue.ContainsKey("message"))
                        {
                            resultHead.Message = dicNodeValue["message"];
                        }

                        //个性化output
                        if (dicNodeValue.ContainsKey("output"))
                        {
                            string outputXML = dicNodeValue["output"];

                            if (!string.IsNullOrEmpty(outputXML))
                            {
                                doc = new XmlDocument();
                                doc.LoadXml(outputXML);

                                if (transNO == "100")
                                {
                                    #region 医院登录[100]

                                    resultHead.SessionId = GetLoginOut(doc, ref errorMsg);

                                    #endregion
                                }
                                else if (transNO == "101")
                                {
                                    #region 医院口令修改[101]

                                    #endregion
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region 失败

                        resultHead.Code = "-1";
                        resultHead.Message = errorMsg;

                        #endregion
                    }

                }
                catch (Exception ex)
                {
                    resultHead.Code = "-1";
                    resultHead.Message = ex.Message;
                }

            }

            return resultHead;
        }

        /// <summary>
        /// 医院登录的Output
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private static string GetLoginOut(XmlDocument doc, ref string errorMsg)
        {
            int rev = -1;
            string nodeValue = string.Empty;
            //获取值
            rev = GetParamValue(doc, "output/SESSIONID", out nodeValue, out errorMsg);
            if (rev <= 0)
            {
                errorMsg = "无节点【output/SESSIONID】!请检查返回参数!";
                return "";
            }
            return nodeValue;
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
                    return rev;
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
        /// </summary>
        /// <param name="sexId"></param>
        /// <returns></returns>
        public static string ConvertSexCode(string sexId)
        {
            string sexCode = string.Empty;
            switch (sexId)
            {
                case "F":
                    sexCode = "0";
                    break;
                case "M":
                    sexCode = "1";
                    break;
            }
            return sexCode;
        }

        #endregion
    }
}
