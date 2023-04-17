using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel4207 : ResponseBase
    {
         public Output output { get; set; }
         public class Output
         {
             public string recordCounts { get; set; }

             public string pages { get; set; }
             
             public List<Data> data { get; set; }
         }

         public class Data
         {

             /// <summary>
             /// 	医药机构就诊ID	
             /// </summary>
             public string fixmedinsMdtrtId { get; set; }
             /// <summary>
             /// 	记账流水号	
             /// </summary>
             public string bkkpSn { get; set; }
             /// <summary>
             /// 	费用发生时间	
             /// </summary>
             public string feeOcurTime { get; set; }
             /// <summary>
             /// 	定点医药机构编号	
             /// </summary>
             public string fixmedinsCode { get; set; }
             /// <summary>
             /// 	定点医药机构名称	
             /// </summary>
             public string fixmedinsName { get; set; }
             /// <summary>
             /// 	数量	
             /// </summary>
             public string cnt { get; set; }
             /// <summary>
             /// 	单价	
             /// </summary>
             public string pric { get; set; }
             /// <summary>
             /// 	明细项目费用总额	
             /// </summary>
             public string detltemFeeSumamt { get; set; }
             /// <summary>
             /// 	医疗目录编码	
             /// </summary>
             public string medListCodg { get; set; }
             /// <summary>
             /// 	医药机构目录编码	
             /// </summary>
             public string medinsListCodg { get; set; }
             /// <summary>
             /// 	医药机构目录名称	
             /// </summary>
             public string medinsListName { get; set; }
             /// <summary>
             /// 	医疗收费项目类别	
             /// </summary>
             public string medChrgitmType { get; set; }
             /// <summary>
             /// 	商品名	
             /// </summary>
             public string prodname { get; set; }
             /// <summary>
             /// 	开单科室编码	
             /// </summary>
             public string bilgDeptCodg { get; set; }
             /// <summary>
             /// 	开单科室名称	
             /// </summary>
             public string bilgDeptName { get; set; }
             /// <summary>
             /// 	开单医生编码	
             /// </summary>
             public string bilgDrCode { get; set; }
             /// <summary>
             /// 	开单医师姓名	
             /// </summary>
             public string bilgDrName { get; set; }
             /// <summary>
             /// 	受单科室编码	
             /// </summary>
             public string acordDeptCodg { get; set; }
             /// <summary>
             /// 	受单科室名称	
             /// </summary>
             public string acordDeptName { get; set; }
             /// <summary>
             /// 	受单医生编码	
             /// </summary>
             public string acordDrCode { get; set; }
             /// <summary>
             /// 	受单医生姓名	
             /// </summary>
             public string acordDrName { get; set; }
             /// <summary>
             /// 	中药使用方式	
             /// </summary>
             public string tcmdrugUsedWay { get; set; }
             /// <summary>
             /// 	外检标志	
             /// </summary>
             public string etipFlag { get; set; }
             /// <summary>
             /// 	外检医院编码	
             /// </summary>
             public string etipHospCode { get; set; }
             /// <summary>
             /// 	出院带药标志	
             /// </summary>
             public string dscgTkdrugFlag { get; set; }
             /// <summary>
             /// 	单次剂量描述	
             /// </summary>
             public string sinDosDscr { get; set; }
             /// <summary>
             /// 	使用频次描述	
             /// </summary>
             public string usedFrquDscr { get; set; }
             /// <summary>
             /// 	周期天数	
             /// </summary>
             public string prdDays { get; set; }
             /// <summary>
             /// 	用药途径描述	
             /// </summary>
             public string medcWayDscr { get; set; }
             /// <summary>
             /// 	备注	
             /// </summary>
             public string memo { get; set; }
             /// <summary>
             /// 	全自费金额	
             /// </summary>
             public string fulamtOwnpayAmt { get; set; }
             /// <summary>
             /// 	超限价自费金额	
             /// </summary>
             public string overlmtSelfpay { get; set; }
             /// <summary>
             /// 	先行自付金额	
             /// </summary>
             public string preselfpayAmt { get; set; }
             /// <summary>
             /// 	符合政策范围金额	
             /// </summary>
             public string inscpAmt { get; set; }
             /// <summary>
             /// 	有效标志	
             /// </summary>
             public string valiFlag { get; set; }
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
             /// 	经办机构	
             /// </summary>
             public string optinsNo { get; set; }
             /// <summary>
             /// 	统筹区编码	
             /// </summary>
             public string poolareaNo { get; set; }
             /// <summary>
             /// 	审核通过标识	
             /// </summary>
             public string chkPassFlag { get; set; }
             /// <summary>
             /// 	处方号	
             /// </summary>
             public string rxno { get; set; }

         }

    }
}
