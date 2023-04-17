using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data.Oracle
{
    [DataBase( FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class FeeRegular:AbstractFeeRegular
    {
        public override string SelectAll
        {
            get
            {
                return @"SELECT  ID,
	                                           ITEM_CODE,
                                               ITEM_NAME ,
                                               LIMIT_CONDITION ,
                                               FEE_TYPE ,
                                               DAYLIMIT ,
                                               LIMIT_CODE ,
                                               OPER_CODE ,
                                               OPER_DATE,
                                               OUTFEE_FLAG
                                        FROM FIN_IPB_FEERULE
                                        ORDER BY FEE_TYPE";
            }
        }

        public override string Insert
        {
            get {
                return @"
                                INSERT INTO FIN_IPB_FEERULE
                                (
                                  ID,
                                  ITEM_CODE ,
                                  ITEM_NAME ,
                                 limit_condition ,
                                  FEE_TYPE ,
                                  DAYLIMIT ,
                                  LIMIT_CODE ,
                                  OPER_CODE ,
                                  OPER_DATE,
                                  OUTFEE_FLAG
                                )
                                VALUES
                                (
                                 {0},
                                 '{1}' ,
                                 '{2}' ,
                                '{3}',
                                 '{4}' ,
                                  {5} ,
                                 '{6}' ,
                                 '{7}' ,
                                  to_date('{8}','yyyy-mm-dd hh24:mi:ss'),
                                  '{9}'
                                )";
            }
        }

        public override string Update
        {
            get
            {
                return @"UPDATE FIN_IPB_FEERULE
                                SET ITEM_CODE = '{1}',
                                    ITEM_NAME ='{2}' ,
                                   LIMIT_CONDITION = '{3}',
                                    FEE_TYPE = '{4}' ,
                                    DAYLIMIT = {5} ,
                                    LIMIT_CODE ='{6}' ,
                                    OPER_CODE = '{7}' ,
                                    OPER_DATE = to_date('{8}','yyyy-mm-dd hh24:mi:ss'),
                                    OUTFEE_FLAG='{9}'
                                WHERE ID = {0}";
            }
        }

        public override string Delete
        {
            get
            {
                return @" delete from fin_ipb_feerule where ID  ={0}";
            }
        }

        public override string AutoID
        {
            get
            {
                return @"SELECT SEQ_FIN_IPB_FEERULE.nextval FROM DUAL";
            }
        }

        public override string WhereByKey
        {
            get
            {
                return @" 
                                where ID={0}";
            }
        }

        public override string WhereByItemCode
        {
            get
            {
                return @"
                                WHERE ITEM_CODE = '{0}'";
            }
        }
    }
}
