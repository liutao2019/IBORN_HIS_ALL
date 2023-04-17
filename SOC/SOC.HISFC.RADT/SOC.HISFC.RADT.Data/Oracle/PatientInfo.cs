using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.RADT.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class PatientInfo:AbstractPatientInfo
    {
        public override string SelectAll
        {
            get { return ""; }
        }

        public override string Insert
        {
            get { return ""; }
        }

        public override string UpdatePactInfo
        {
            get {
                return @"
                UPDATE FIN_IPR_INMAININFO 
                SET    
                        PACT_CODE = '{1}',--		合同代码
                        PACT_NAME ='{2}',--	合同单位名称
                        PAYKIND_CODE	='{3}',--		结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干       
                        MCARD_NO = '{4}',
                        FEE_INTERVAL = {5},
                        BED_LIMIT = {6},
                        BEDOVERDEAL='{7}',
                        DAY_LIMIT ={8},
                        LIMIT_TOT={9},
                        LIMIT_OVERTOP={10},
                        AIR_LIMIT={11}       
               WHERE  inpatient_no ='{0}'"
                ; }
        }
    }
}
