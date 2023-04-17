using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucSpecIdImport : UserControl
    {
        private string strQuery;
        public ucSpecIdImport()
        {
            InitializeComponent();
            strQuery = "";
           //GetSql();
        }

        private void GetSql()
        {
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
        }
     //       strQuery = "SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID 标本号,  SPEC_TYPE.SPECIMENTNAME 标本类型,COM_DEPARTMENT.DEPT_NAME 送存科室,\n" +
     //                       "SPEC_DISEASETYPE.DISEASENAME 病种,SPEC_SOURCE.TUMORPOR 癌性质, SPEC_SOURCE_STORE.TUMORTYPE 癌种类,\n" +
     //                       "SPEC_SOURCE_STORE.STORETIME 入库时间,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) 出库次数,\n" +
     //                       " SPEC_BOX.BOXBARCODE 盒条码,\n" +
     //                       "　SPEC_SUBSPEC.BOXENDROW 行,SPEC_SUBSPEC.BOXENDCOL 列\n" +
     //                       " FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID,SPEC_SOURCE_STORE,SPEC_SOURCE, SPEC_DISEASETYPE,SPEC_TYPE,COM_DEPARTMENT, SPEC_BOX\n" +
     //                       " WHERE SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID AND　SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
     //                       " AND SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID \n" +
     //                       " AND SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID AND SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID \n" +
     //                       " AND COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO AND SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3')";//读取该申请表的申请要求
     //}        

       /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="strQuery"></param>
        public string GetQueryCondition()
        {
            return strQuery;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dlgResult = openFileDialog.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                DataSet ds = new DataSet();
                ExlToDb2Manage exlManage = new ExlToDb2Manage();
                if (exlManage.ExlConnect(openFileDialog.FileName, ref ds) == -1)
                {
                    MessageBox.Show("连接Excel出错！", "标本查询");
                    return;
                }
                DataTable dt = new DataTable();
                dt = ds.Tables[0];              
                if (dt.Rows.Count == 0)
                {
                    return; 
                }
                GetSql();
                int index = 0;
                strQuery += " AND SPEC_SOURCE.SPECID IN (";
                foreach (DataRow dr in dt.Rows)
                {
                    string specId = dr[0].ToString();
                    if(index == 0)
                    {
                        strQuery += specId;
                    }
                    if (index == dt.Rows.Count - 1)
                    {
                        strQuery += (" , " + specId + ")");
                    }
                    if(index > 0 && index < dt.Rows.Count - 1)
                    {
                        strQuery += (" , " + specId);
                    }
                    index++;
                }
            }
        }
    }
}
