using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data.Oracle
{
    [DataBase( FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class MedicineList:AbstractMedicineList
    {
        #region Select

        public override string SelectAllNewFee
        {
            get {

                return @"
                    select 
                                ext_code,
                                recipe_no,
                                sequence_no,
                                trans_type,
                                inpatient_no,
                                unit_price,
                                pack_qty,
                                qty,
                                tot_cost,
                                own_cost,
                                pay_cost,
                                pub_cost,
                                eco_cost,
                                deftot_cost,
                                senddrug_flag
                        from fin_ipb_medicinelist
                        where inpatient_no='{0}'
                        and balance_state='0'
                        and ext_flag2 like '%N'
                ";
            }
        }

        public override string SelectAll
        {
            get
            {
                return @"
                        
                        ";
            }
        }

        #endregion

        #region Insert

        public override string InsertOldFee
        {
            get {
                return @"
                                    INSERT INTO FIN_IPB_MEDICINELIST
                                    SELECT 
	                                    'Y'||lpad(seq_fin_drugrecipe.nextval,13,'0') RECIPE_NO,
	                                    SEQUENCE_NO,
	                                    '2' TRANS_TYPE,
	                                    INPATIENT_NO,
	                                    NAME,
	                                    PAYKIND_CODE,
	                                    PACT_CODE,
	                                    INHOS_DEPTCODE,
	                                    NURSE_CELL_CODE,
	                                    RECIPE_DEPTCODE,
	                                    EXECUTE_DEPTCODE,
	                                    MEDICINE_DEPTCODE,
	                                    RECIPE_DOCCODE,
	                                    DRUG_CODE,
	                                    FEE_CODE,
	                                    CENTER_CODE,
	                                    DRUG_NAME,
	                                    SPECS,
	                                    DRUG_TYPE,
	                                    DRUG_QUALITY,
	                                    HOME_MADE_FLAG,
	                                    UNIT_PRICE,
	                                    CURRENT_UNIT,
	                                    PACK_QTY,
	                                    -NOBACK_NUM,/* 数量=可退数量等于退费数量 t.qty*/        
	                                    DAYS,
	                                    -(UNIT_PRICE*NOBACK_NUM)/NVL(PACK_QTY,1) TOT_COST,
	                                    -(UNIT_PRICE*NOBACK_NUM)/NVL(PACK_QTY,1)  OWN_COST,
	                                    -PAY_COST,
	                                    -PUB_COST,
	                                    -ECO_COST,
	                                    UPDATE_SEQUENCENO,
	                                    SENDDRUG_SEQUENCE,
	                                    '2' SENDDRUG_FLAG,
	                                    BABY_FLAG,
	                                    JZQJ_FLAG,
	                                    BROUGHT_FLAG,
	                                    EXT_FLAG,
	                                    INVOICE_NO,
	                                    BALANCE_NO,
	                                    BALANCE_STATE,
	                                    0 NOBACK_NUM,
	                                    RECIPE_NO EXT_CODE,
	                                    EXT_OPERCODE,
	                                    EXT_DATE,
	                                    APPRNO,
	                                    CHARGE_OPERCODE,
	                                    CHARGE_DATE,
	                                    '{1}' FEE_OPERCODE,
	                                    sysdate FEE_DATE,
	                                    EXEC_OPERCODE,
	                                    EXEC_DATE,
	                                    SENDDRUG_OPERCODE,
	                                    SENDDRUG_DATE,
	                                    CHECK_OPERCODE,
	                                    CHECK_NO,
	                                    MO_ORDER,
	                                    MO_EXEC_SQN,
	                                    FEE_RATE,
	                                    FEEOPER_DEPTCODE,
	                                    UPLOAD_FLAG,
	                                    substr(lpad(EXT_FLAG2,3,'0'),1,2)||'N' EXT_FLAG2, /* t费用来源第三位=1*/
	                                    EXT_FLAG1,
	                                    EXT_FLAG3,
	                                    MEDICALTEAM_CODE,
	                                    OPERATIONNO,
	                                    TRANSACTION_SEQUENCE_NUMBER,
	                                    SI_TRANSACTION_DATETIME,
	                                    HIS_RECIPE_NO,
	                                    SI_RECIPE_NO,
	                                    HIS_CANCEL_RECIPE_NO,
	                                    SI_CANCEL_RECIPE_NO,
	                                    EXT_FLAG4,
	                                    EXT_FLAG5,
	                                    BALANCE_OPERCODE,
	                                    BALANCE_DATE,
	                                    DEFTOT_COST
                                    FROM FIN_IPB_MEDICINELIST
                                    WHERE INPATIENT_NO='{0}'
                                    AND BALANCE_STATE='0'
                                    AND NOBACK_NUM>0
                                    ";
            }
        }

        public override string InsertNewFee
        {
            get
            {
                return @"
                                    INSERT INTO FIN_IPB_MEDICINELIST
                                    SELECT 
	                                    'Y'||lpad(seq_fin_drugrecipe.nextval,13,'0') RECIPE_NO,
	                                    SEQUENCE_NO,
	                                    '1' TRANS_TYPE,
	                                    INPATIENT_NO,
	                                    NAME,
	                                    '{2}' PAYKIND_ID,
	                                    '{3}' PACT_CODE,
	                                    INHOS_DEPTCODE,
	                                    NURSE_CELL_CODE,
	                                    RECIPE_DEPTCODE,
	                                    EXECUTE_DEPTCODE,
	                                    MEDICINE_DEPTCODE,
	                                    RECIPE_DOCCODE,
	                                    FIN_IPB_MEDICINELIST.DRUG_CODE,
	                                    FIN_IPB_MEDICINELIST.FEE_CODE,
	                                    CENTER_CODE,
	                                    FIN_IPB_MEDICINELIST.DRUG_NAME,
	                                    FIN_IPB_MEDICINELIST.SPECS,
	                                    FIN_IPB_MEDICINELIST.DRUG_TYPE,
	                                    FIN_IPB_MEDICINELIST.DRUG_QUALITY,
	                                    HOME_MADE_FLAG,
	                                    DECODE('{4}','3',DECODE(NVL(PHA_COM_BASEINFO.RETAIL_PRICE2,0),0,PHA_COM_BASEINFO.RETAIL_PRICE,PHA_COM_BASEINFO.RETAIL_PRICE2),PHA_COM_BASEINFO.RETAIL_PRICE) UNIT_PRICE,/*当使用零差价(=3)时，使用药品购入价*/
	                                    FIN_IPB_MEDICINELIST.CURRENT_UNIT,
	                                    FIN_IPB_MEDICINELIST.PACK_QTY,
	                                    NOBACK_NUM,/* 数量=可退数量等于退费数量 t.qty*/        
	                                    DAYS,
	                                    (UNIT_PRICE*NOBACK_NUM)/NVL(FIN_IPB_MEDICINELIST.PACK_QTY,1) TOT_COST,
	                                    (UNIT_PRICE*NOBACK_NUM)/NVL(FIN_IPB_MEDICINELIST.PACK_QTY,1)  OWN_COST,
	                                    PAY_COST,
	                                    PUB_COST,
	                                    ECO_COST,
	                                    UPDATE_SEQUENCENO,
	                                    SENDDRUG_SEQUENCE,
	                                    SENDDRUG_FLAG,
	                                    BABY_FLAG,
	                                    JZQJ_FLAG,
	                                    BROUGHT_FLAG,
	                                    EXT_FLAG,
	                                    INVOICE_NO,
	                                    BALANCE_NO,
	                                    BALANCE_STATE,
	                                    NOBACK_NUM,
	                                    RECIPE_NO EXT_CODE,
	                                    EXT_OPERCODE,
	                                    EXT_DATE,
	                                    APPRNO,
	                                    CHARGE_OPERCODE,
	                                    CHARGE_DATE,
	                                    '{1}' FEE_OPERCODE,
	                                    sysdate FEE_DATE,
	                                    EXEC_OPERCODE,
	                                    EXEC_DATE,
	                                    SENDDRUG_OPERCODE,
	                                    SENDDRUG_DATE,
	                                    CHECK_OPERCODE,
	                                    CHECK_NO,
	                                    MO_ORDER,
	                                    MO_EXEC_SQN,
	                                    FEE_RATE,
	                                    FEEOPER_DEPTCODE,
	                                    UPLOAD_FLAG,
	                                    substr(lpad(EXT_FLAG2,3,'0'),1,2)||'N' EXT_FLAG2, /* t费用来源第三位=1*/
	                                    EXT_FLAG1,
	                                    EXT_FLAG3,
	                                    MEDICALTEAM_CODE,
	                                    OPERATIONNO,
	                                    TRANSACTION_SEQUENCE_NUMBER,
	                                    SI_TRANSACTION_DATETIME,
	                                    HIS_RECIPE_NO,
	                                    SI_RECIPE_NO,
	                                    HIS_CANCEL_RECIPE_NO,
	                                    SI_CANCEL_RECIPE_NO,
	                                    EXT_FLAG4,
	                                    EXT_FLAG5,
	                                    BALANCE_OPERCODE,
	                                    BALANCE_DATE,
	                                    DEFTOT_COST
                                    FROM FIN_IPB_MEDICINELIST,PHA_COM_BASEINFO
                                    WHERE INPATIENT_NO='{0}'
                                    AND FIN_IPB_MEDICINELIST.DRUG_CODE=PHA_COM_BASEINFO.DRUG_CODE
                                    AND BALANCE_STATE='0'
                                    AND NOBACK_NUM>0";
            }
        }

        public override string Insert
        {
            get { return ""; }
        }

        #endregion

        #region Update

        public override string UpdateSendFlagByInpatientNO
        {
            get
            {
                return @"
                                update FIN_IPB_MEDICINELIST
                                  set SENDDRUG_FLAG='2'
                                  where INPATIENT_NO='{0}'
                                  and BALANCE_STATE='0'
                                  and NOBACK_NUM=0
                                  and TRANS_TYPE='1'";
            }
        }

        public override string UpdateNoBackNumByInPatientNO
        {
            get
            {
                return @"UPDATE FIN_IPB_MEDICINELIST   
                   SET NOBACK_NUM= NOBACK_NUM - QTY  
                 WHERE   INPATIENT_NO='{0}'
                   AND NOBACK_NUM>=ABS(QTY)
                   and PACT_CODE='{1}'
                   AND BALANCE_STATE = '0'";
            }
        }

        public override string UpdateFTSource
        {
            get
            {
                return @"
                                update fin_ipb_medicinelist  set ext_flag2=substr(lpad(ext_flag2,3,'0'),1,2)||'1'  where inpatient_no='{0}' and balance_state='0'   and ext_flag2 like '%N'   ";
            }
        }

        public override string UpdateFTCost
        {
            get {
                return @" update fin_ipb_medicinelist set tot_cost={4},own_cost={5},pay_cost={6},pub_cost={7},eco_cost={8},deftot_cost={9} where ext_code='{0}' and recipe_no='{1}' and sequence_no={2} and trans_type='{3}'";
            }
        }

        #endregion
    }
}
