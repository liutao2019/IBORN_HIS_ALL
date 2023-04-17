using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel4208 : ResponseBase
    {
       public Output output { get; set; }
       public class Output
       {
           public string recordCounts { get; set; }

           public string pages { get; set; }

           public List<Data> data { get; set; }
       }

         public class Data {
             /// <summary>
             /// 	医药机构就诊ID	
             /// </summary>
             public string fixmedinsMdtrtId { get; set; }
             /// <summary>
             /// 	定点医药机构编号	
             /// </summary>
             public string fixmedinsCode { get; set; }
             /// <summary>
             /// 	定点医药机构名称	
             /// </summary>
             public string fixmedinsName { get; set; }
             /// <summary>
             /// 	人员证件类型	
             /// </summary>
             public string psnCertType { get; set; }
             /// <summary>
             /// 	证件号码	
             /// </summary>
             public string certno { get; set; }
             /// <summary>
             /// 	人员姓名	
             /// </summary>
             public string psnName { get; set; }
             /// <summary>
             /// 	性别	
             /// </summary>
             public string gend { get; set; }
             /// <summary>
             /// 	民族	
             /// </summary>
             public string naty { get; set; }
             /// <summary>
             /// 	出生日期	
             /// </summary>
             public string brdy { get; set; }
             /// <summary>
             /// 	年龄	
             /// </summary>
             public string age { get; set; }
             /// <summary>
             /// 	联系人姓名	
             /// </summary>
             public string conerName { get; set; }
             /// <summary>
             /// 	联系电话	
             /// </summary>
             public string tel { get; set; }
             /// <summary>
             /// 	联系地址	
             /// </summary>
             public string addr { get; set; }
             /// <summary>
             /// 	开始时间	
             /// </summary>
             public string begntime { get; set; }
             /// <summary>
             /// 	结束时间	
             /// </summary>
             public string endtime { get; set; }
             /// <summary>
             /// 	医疗类别	
             /// </summary>
             public string medType { get; set; }
             /// <summary>
             /// 	住院/门诊号	
             /// </summary>
             public string iptOtpNo { get; set; }
             /// <summary>
             /// 	病历号	
             /// </summary>
             public string medrcdno { get; set; }
             /// <summary>
             /// 	主诊医师代码	
             /// </summary>
             public string chfpdrCode { get; set; }
             /// <summary>
             /// 	入院诊断描述	
             /// </summary>
             public string admDiagDscr { get; set; }
             /// <summary>
             /// 	入院科室编码	
             /// </summary>
             public string admDeptCodg { get; set; }
             /// <summary>
             /// 	入院科室名称	
             /// </summary>
             public string admDeptName { get; set; }
             /// <summary>
             /// 	入院床位	
             /// </summary>
             public string admBed { get; set; }
             /// <summary>
             /// 	病区床位	
             /// </summary>
             public string wardareaBed { get; set; }
             /// <summary>
             /// 	转科室标志	
             /// </summary>
             public string trafDeptFlag { get; set; }
             /// <summary>
             /// 	出院主诊断代码	
             /// </summary>
             public string dscgMaindiagCode { get; set; }
             /// <summary>
             /// 	出院科室编码	
             /// </summary>
             public string dscgDeptCodg { get; set; }
             /// <summary>
             /// 	出院科室名称	
             /// </summary>
             public string dscgDeptName { get; set; }
             /// <summary>
             /// 	出院床位	
             /// </summary>
             public string dscgBed { get; set; }
             /// <summary>
             /// 	离院方式	
             /// </summary>
             public string dscgWay { get; set; }
             /// <summary>
             /// 	主要病情描 述	
             /// </summary>
             public string mainCondDscr { get; set; }
             /// <summary>
             /// 	病种编号	
             /// </summary>
             public string diseNo { get; set; }
             /// <summary>
             /// 	病种名称	
             /// </summary>
             public string diseName { get; set; }
             /// <summary>
             /// 	手术操作代码	
             /// </summary>
             public string oprnOprtCode { get; set; }
             /// <summary>
             /// 	手术操作名称	
             /// </summary>
             public string oprnOprtName { get; set; }
             /// <summary>
             /// 	门诊诊断信息	
             /// </summary>
             public string otpDiaglnfo { get; set; }
             /// <summary>
             /// 	在院状态	
             /// </summary>
             public string inhospStas { get; set; }
             /// <summary>
             /// 	死亡日期	
             /// </summary>
             public string dieDate { get; set; }
             /// <summary>
             /// 	住院天数	
             /// </summary>
             public string iptDays { get; set; }
             /// <summary>
             /// 	计划生育服务证号	
             /// </summary>
             public string fpscNo { get; set; }
             /// <summary>
             /// 	生育类别	
             /// </summary>
             public string matnType { get; set; }
             /// <summary>
             /// 	计划生育手术类别	
             /// </summary>
             public string birctrIType { get; set; }
             /// <summary>
             /// 	晚育标志	
             /// </summary>
             public string latechbFlag { get; set; }
             /// <summary>
             /// 	孕周数	
             /// </summary>
             public string gesoVal { get; set; }
             /// <summary>
             /// 	胎次	
             /// </summary>
             public string fetts { get; set; }
             /// <summary>
             /// 	胎儿数	
             /// </summary>
             public string fetusCnt { get; set; }
             /// <summary>
             /// 	早产标志	
             /// </summary>
             public string pretFlag { get; set; }
             /// <summary>
             /// 	妊娠时间	
             /// </summary>
             public string preyTime { get; set; }
             /// <summary>
             /// 	计划生育手术或生育日期	
             /// </summary>
             public string birctrIMatnDate { get; set; }
             /// <summary>
             /// 	伴有并发症标志	
             /// </summary>
             public string copFlag { get; set; }
             /// <summary>
             /// 	有效标志	
             /// </summary>
             public string valiFlag { get; set; }
             /// <summary>
             /// 	备注	
             /// </summary>
             public string memo { get; set; }
             /// <summary>
             /// 	经办人ID	
             /// </summary>
             public string opterId { get; set; }
             /// <summary>
             /// 	经办人姓名	
             /// </summary>
             public string opterName { get; set; }
             /// <summary>
             /// 	经办时间	
             /// </summary>
             public string optTime { get; set; }
             /// <summary>
             /// 	唯一记录号	
             /// </summary>
             public string rid { get; set; }
             /// <summary>
             /// 	更新时间	
             /// </summary>
             public string updtTime { get; set; }
             /// <summary>
             /// 	创建人	
             /// </summary>
             public string crterId { get; set; }
             /// <summary>
             /// 	创建人姓名	
             /// </summary>
             public string crterName { get; set; }
             /// <summary>
             /// 	创建时间	
             /// </summary>
             public string crteTime { get; set; }
             /// <summary>
             /// 	创建机构	
             /// </summary>
             public string crteOptinsNo { get; set; }
             /// <summary>
             /// 	经办机构	
             /// </summary>
             public string optinsNo { get; set; }
             /// <summary>
             /// 	统筹区编码	
             /// </summary>
             public string poolareaNo { get; set; }
             /// <summary>
             /// 	主诊医师姓名	
             /// </summary>
             public string chfpdrName { get; set; }
             /// <summary>
             /// 	住院主诊断名称	
             /// </summary>
             public string dscgMaindiagName { get; set; }
             /// <summary>
             /// 	医疗总费用	
             /// </summary>
             public string medfeeSumamt { get; set; }
             /// <summary>
             /// 	电子票据代码	
             /// </summary>
             public string elecBillCode { get; set; }
             /// <summary>
             /// 	电子票据号码	
             /// </summary>
             public string elecBillnoCode { get; set; }
             /// <summary>
             /// 	电子票据校验码	
             /// </summary>
             public string elecBillChkcode { get; set; }
             /// <summary>
             /// 	完成标志	
             /// </summary>
             public string cpltflag { get; set; }
             /// <summary>
             /// 	扩展字段	
             /// </summary>
             public string expContent { get; set; }


         }
    }
}
