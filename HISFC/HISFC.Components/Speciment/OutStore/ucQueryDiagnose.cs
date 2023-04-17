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
        /// ICD������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase icdMgr;     
    
        public ucQueryDiagnose()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ���ò�ѯ����
        /// </summary>
        /// <param name="strQuery"></param>
        public void GetQueryCondition(ref string strQuery)
        {
            #region sql���
            strQuery = " SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID �걾��,  SPEC_TYPE.SPECIMENTNAME �걾����,COM_DEPARTMENT.DEPT_NAME �ʹ����,\n" +
                       " SPEC_DISEASETYPE.DISEASENAME ����,SPEC_SOURCE.TUMORPOR ������, SPEC_SOURCE_STORE.TUMORTYPE ������,\n" +
                       " SPEC_SOURCE_STORE.STORETIME ���ʱ��,\n" +
                       " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) �������,\n" +
                       " SPEC_BOX.BOXBARCODE ������,\n" +
                       " SPEC_SUBSPEC.BOXENDROW ��,SPEC_SUBSPEC.BOXENDCOL ��,\n" +
                       " SPEC_SOURCE_STORE.TUMORPOS ���ﲿλ, SPEC_SOURCE.RADSCHEME ���Ʒ���, SPEC_SOURCE.MEDSCHEME ���Ʒ���, \n" +
                       " MAIN_DIANAME �����, INHOS_DIANAME ��Ժ���, CLINIC_DIANAME �������, M_DIAGICDNAME ��Ժ���,SPEC_SUBSPEC.STATUS �ڿ�״̬,SPEC_SUBSPEC.SUBBARCODE ���� \n" +
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
