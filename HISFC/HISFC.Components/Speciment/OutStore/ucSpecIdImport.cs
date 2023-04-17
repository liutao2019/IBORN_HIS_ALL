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
        }
     //       strQuery = "SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID �걾��,  SPEC_TYPE.SPECIMENTNAME �걾����,COM_DEPARTMENT.DEPT_NAME �ʹ����,\n" +
     //                       "SPEC_DISEASETYPE.DISEASENAME ����,SPEC_SOURCE.TUMORPOR ������, SPEC_SOURCE_STORE.TUMORTYPE ������,\n" +
     //                       "SPEC_SOURCE_STORE.STORETIME ���ʱ��,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) �������,\n" +
     //                       " SPEC_BOX.BOXBARCODE ������,\n" +
     //                       "��SPEC_SUBSPEC.BOXENDROW ��,SPEC_SUBSPEC.BOXENDCOL ��\n" +
     //                       " FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID,SPEC_SOURCE_STORE,SPEC_SOURCE, SPEC_DISEASETYPE,SPEC_TYPE,COM_DEPARTMENT, SPEC_BOX\n" +
     //                       " WHERE SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID AND��SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
     //                       " AND SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID \n" +
     //                       " AND SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID AND SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID \n" +
     //                       " AND COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO AND SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3')";//��ȡ������������Ҫ��
     //}        

       /// <summary>
        /// ���ò�ѯ����
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
                    MessageBox.Show("����Excel����", "�걾��ѯ");
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
