using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class ExecDept:AbstractExecDept
    {
        public override string SelectAll
        {
            get {
                return @"SELECT 
	                                DEFUALT_CODE,
	                                COMPARE_CODE,
	                                COMPARE_NAME,
	                                ORIGINAL_CODE,
	                                ORIGINAL_CUSTOM_CODE,
	                                ORIGINAL_NAME,
	                                TARGET_CODE,
	                                TARGET_CUSTOM_CODE,
	                                TARGET_NAME,
	                                TARGET_SPELL_CODE,
	                                TARGET_WB_CODE,
	                                TARGET_EXT_CODE,
	                                TARGET_EXT2_CODE,
	                                TARGET_EXT3_CODE,
	                                TARGET2_CODE,
	                                TARGET2_CUSTOM_CODE,
	                                TARGET2_NAME,
	                                TARGET2_SPELL_CODE,
	                                TARGET2_WB_CODE,
	                                TARGET2_EXT_CODE,
	                                TARGET2_EXT2_CODE,
	                                TARGET2_EXT3_CODE
                                FROM FIN_COM_DEFAULTEXECDEPT"
                ; }
        }

        public override string Insert
        {
            get {
                return @"INSERT INTO FIN_COM_DEFAULTEXECDEPT
	                                (
	                                DEFUALT_CODE,
	                                COMPARE_CODE,
	                                COMPARE_NAME,
	                                ORIGINAL_CODE,
	                                ORIGINAL_CUSTOM_CODE,
	                                ORIGINAL_NAME,
	                                TARGET_CODE,
	                                TARGET_CUSTOM_CODE,
	                                TARGET_NAME,
	                                TARGET_SPELL_CODE,
	                                TARGET_WB_CODE,
	                                TARGET_EXT_CODE,
	                                TARGET_EXT2_CODE,
	                                TARGET_EXT3_CODE,
	                                TARGET2_CODE,
	                                TARGET2_CUSTOM_CODE,
	                                TARGET2_NAME,
	                                TARGET2_SPELL_CODE,
	                                TARGET2_WB_CODE,
	                                TARGET2_EXT_CODE,
	                                TARGET2_EXT2_CODE,
	                                TARGET2_EXT3_CODE
	                                )
                                VALUES 
	                                (
	                                '{0}',
	                                '{1}',
	                                '{2}',
	                                '{3}',
	                                '{4}',
	                                '{5}',
	                                '{6}',
	                                '{7}',
	                                '{8}',
	                                '{9}',
	                                '{10}',
	                                '{11}',
	                                '{12}',
	                                '{13}',
	                                '{14}',
	                                '{15}',
	                                '{16}',
	                                '{17}',
	                                '{18}',
	                                '{19}',
	                                '{20}',
	                                '{21}'
	                                )"
                ; }
        }

        public override string Delete
        {
            get
            {
                return @"DELETE FROM FIN_COM_DEFAULTEXECDEPT WHERE DEFUALT_CODE='{0}'";
            }
        }

        public override string AutoID
        {
            get
            {
                return @"SELECT SEQ_FIN_COM_DEFAULTEXECDEPT.NEXTVAL FROM DUAL";
            }
        }

        public override string Update
        {
            get
            {
                return @"UPDATE FIN_COM_DEFAULTEXECDEPT
                                    SET 
	                                    COMPARE_CODE = '{1}',
	                                    COMPARE_NAME = '{2}',
	                                    ORIGINAL_CODE = '{3}',
	                                    ORIGINAL_CUSTOM_CODE = '{4}',
	                                    ORIGINAL_NAME = '{5}',
	                                    TARGET_CODE = '{6}',
	                                    TARGET_CUSTOM_CODE = '{7}',
	                                    TARGET_NAME = '{8}',
	                                    TARGET_SPELL_CODE = '{9}',
	                                    TARGET_WB_CODE = '{10}',
	                                    TARGET_EXT_CODE = '{11}',
	                                    TARGET_EXT2_CODE = '{12}',
	                                    TARGET_EXT3_CODE = '{13}',
	                                    TARGET2_CODE = '{14}',
	                                    TARGET2_CUSTOM_CODE = '{15}',
	                                    TARGET2_NAME = '{16}',
	                                    TARGET2_SPELL_CODE = '{17}',
	                                    TARGET2_WB_CODE = '{18}',
	                                    TARGET2_EXT_CODE = '{19}',
	                                    TARGET2_EXT2_CODE = '{20}',
	                                    TARGET2_EXT3_CODE = '{21}'
                                    WHERE DEFUALT_CODE = '{0}'";
            }
        }

        public override string WhereByCompareID
        {
            get
            {
                return @" WHERE COMPARE_CODE='{0}'
                                   ORDER BY ORIGINAL_CODE,TARGET_CODE,TARGET2_CODE";
            }
        }

        public override string WhereByCompareIDAndOriginalID
        {
            get {
                return @" WHERE COMPARE_CODE='{0}' and ORIGINAL_CODE='{1}'
                                   ORDER BY ORIGINAL_CODE,TARGET_CODE,TARGET2_CODE";
            }
        }

        public override string WhereByCompareIDAndOrigianlIDAndTargetID
        {
            get
            {
                return @" WHERE COMPARE_CODE='{0}' and ORIGINAL_CODE='{1}' and TARGET_CODE='{2}'
                                   ORDER BY ORIGINAL_CODE,TARGET_CODE,TARGET2_CODE";
            }
        }
    }
}
