using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucQueryDiagnose : UserControl
    {
        /// <summary>
        /// ICD管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase icdMgr;     
    
        public ucQueryDiagnose()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="strQuery"></param>
        public void GetQueryCondition(ref string strQuery)
        {
            #region sql语句
            strQuery = " SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID 标本号,  SPEC_TYPE.SPECIMENTNAME 标本类型,COM_DEPARTMENT.DEPT_NAME 送存科室,\n" +
                       " SPEC_DISEASETYPE.DISEASENAME 病种,SPEC_SOURCE.TUMORPOR 癌性质, SPEC_SOURCE_STORE.TUMORTYPE 癌种类,\n" +
                       " SPEC_SOURCE_STORE.STORETIME 入库时间,\n" +
                       " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) 出库次数,\n" +
                       " SPEC_BOX.BOXBARCODE 盒条码,\n" +
                       " SPEC_SUBSPEC.BOXENDROW 行,SPEC_SUBSPEC.BOXENDCOL 列,\n" +
                       " SPEC_SOURCE_STORE.TUMORPOS 肿物部位, SPEC_SOURCE.RADSCHEME 放疗方案, SPEC_SOURCE.MEDSCHEME 化疗方案, \n" +
                       " MAIN_DIANAME 主诊断, INHOS_DIANAME 入院诊断, CLINIC_DIANAME 门诊诊断, M_DIAGICDNAME 出院诊断,SPEC_SUBSPEC.STATUS 在库状态,SPEC_SUBSPEC.SUBBARCODE 条码 \n" +
                       "  FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID \n" +
                       "  INNER JOIN SPEC_BOX ON SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID\n" +
                       "  INNER JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID\n" +
                       "  INNER JOIN SPEC_SOURCE ON  SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID\n" +
                       " LEFT JOIN SPEC_DISEASETYPE ON SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
                       " LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID\n" +
                       " LEFT JOIN	COM_DEPARTMENT ON COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO \n" +
                       " LEFT JOIN SPEC_BASE ON SPEC_BASE.SPECID = SPEC_SOURCE.SPECID \n" +
                       " WHERE ( SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3' OR SPEC_SUBSPEC.STATUS='2'))";
            if (cmbDia.Tag != null)
            {
                string icd = cmbDia.Text.ToString();
                strQuery += " AND (SPEC_BASE.M_DIAGICDNAME like '%" + icd + "%' Or SPEC_BASE.INHOS_DIANAME like '%" + icd + "%' Or SPEC_BASE.CLINIC_DIANAME like '%" + icd + "%' or SPEC_BASE.MAIN_DIANAME like '%" + icd;
                strQuery += "%' Or SPEC_BASE.MAIN_DIANAME1 like '% " + icd + "%' Or SPEC_BASE.MAIN_DIANAME2 like '% " + icd;
                strQuery += "%' Or SPEC_BASE.MOD_NAME like '%" + icd + "%')";
            }
             #endregion
        }
        private void ucQueryDiagnose_Load(object sender, EventArgs e)
        {
            ArrayList arrICD = new ArrayList();
            icdMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
            arrICD = icdMgr.ICDQuery(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            this.cmbDia.AddItems(arrICD);  
        }
    }
}
