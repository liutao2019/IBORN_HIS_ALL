using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucQueryByPatient : UserControl
    {
        public ucQueryByPatient()
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
                       " LEFT JOIN SPEC_PATIENT ON SPEC_SOURCE.PATIENTID = SPEC_PATIENT.PATIENTID \n"+
                       " WHERE ( SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3' OR SPEC_SUBSPEC.STATUS='2'))";
            //strQuery = "SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID �걾��,  SPEC_TYPE.SPECIMENTNAME �걾����,COM_DEPARTMENT.DEPT_NAME �ʹ����,\n" +
            //                "SPEC_DISEASETYPE.DISEASENAME ����,SPEC_SOURCE.TUMORPOR ������, SPEC_SOURCE_STORE.TUMORTYPE ������,\n" +
            //                "SPEC_SOURCE_STORE.STORETIME ���ʱ��,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) �������,\n" +
            //                " SPEC_BOX.BOXBARCODE ������,\n" +
            //                " SPEC_SUBSPEC.BOXENDROW ��,SPEC_SUBSPEC.BOXENDCOL ��\n" +
            //                " FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID,SPEC_SOURCE_STORE,\n" +
            //                " SPEC_SOURCE LEFT JOIN SPEC_PATIENT ON SPEC_SOURCE.PATIENTID = SPEC_PATIENT.PATIENTID, SPEC_DISEASETYPE,SPEC_TYPE,COM_DEPARTMENT,SPEC_BOX\n" +
            //                " WHERE SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID AND��SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
            //                " AND SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID \n" +
            //                " AND SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID AND SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID \n" +
            //                " AND COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO AND SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3')\n";//��ȡ������������Ҫ��                            

            if (rbtAnd.Checked)
            {
                strQuery += txtPatName.Text.Trim() == "" ? "" : " AND SPEC_PATIENT.NAME LIKE '%" + txtPatName.Text.Trim() + "%'";
                strQuery += txtIDNum.Text.Trim() == "" ? "" : " AND IDCARDNO LIKE '%" + txtIDNum.Text.Trim() + "%'";
                strQuery += chkMale.Checked ? " AND GENDER = 'M'" : "";
                strQuery += chkFemale.Checked ? " AND GENDER='F'" : "";
                //strQuery += " AND BIRTHDAY >= " + dtpBirth.Value.Date.ToString();
                //strQuery += " AND BIRTHDAY <= " + dtpBirth.Value.Date.AddDays(1.0).ToString();
                strQuery += txtHome.Text.Trim() == "" ? "" : " AND HOME LIKE '%" + txtHome.Text.Trim() + "'%";
                strQuery += txtMaritalStatus.Tag == null ? "" : " AND ISMARR =" + txtMaritalStatus.Tag.ToString();
                strQuery += txtNation.Tag == null ? "" : " AND NATION =" + txtNation.Tag.ToString();
                strQuery += txtNationality.Tag == null ? "" : " AND NATIONALITY =" + txtNationality.Tag.ToString();
                strQuery += txtBloodType.Tag == null ? "" : " AND BLOOD_CODE =" + txtBloodType.Tag.ToString();
            }
            if (rbtOr.Checked)
            {
                strQuery += txtPatName.Text.Trim() == "" ? "" : " OR SPEC_PATIENT.NAME LIKE '%" + txtPatName.Text.Trim() + "%'";
                strQuery += txtIDNum.Text.Trim() == "" ? "" : " OR IDCARDNO LIKE '%" + txtIDNum.Text.Trim() + "%'";
                strQuery += chkMale.Checked ? " OR GENDER = 'M'" : "";
                strQuery += chkFemale.Checked ? " OR GENDER='F'" : "";
                //strQuery += " AND BIRTHDAY >= " + dtpBirth.Value.Date.ToString();
                //strQuery += " AND BIRTHDAY <= " + dtpBirth.Value.Date.AddDays(1.0).ToString();
                strQuery += txtHome.Text.Trim() == "" ? "" : " OR HOME LIKE '%" + txtHome.Text.Trim() + "'%";
                strQuery += txtMaritalStatus.Tag == null ? "" : " OR ISMARR =" + txtMaritalStatus.Tag.ToString();
                strQuery += txtNation.Tag == null ? "" : " OR NATION =" + txtNation.Tag.ToString();
                strQuery += txtNationality.Tag == null ? "" : " OR NATIONALITY =" + txtNationality.Tag.ToString();
                strQuery += txtBloodType.Tag == null ? "" : " OR BLOOD_CODE =" + txtBloodType.Tag.ToString();
            }
           #endregion
        }

        private void ucQueryByPatient_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //��ѯ�����б�
            ArrayList countryList = new ArrayList();
            countryList = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.txtNationality.AddItems(countryList);

            //��ѯ�����б�
            ArrayList Nationallist = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.txtNation.AddItems(Nationallist);

            //Ѫ���б�
            ArrayList BloodTypeList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);// baseDml.GetBloodType();
            this.txtBloodType.AddItems(BloodTypeList);

            //�����б�
            ArrayList MaritalStatusList = con.GetList("MaritalStatus");
            this.txtMaritalStatus.AddItems(MaritalStatusList);
        }
    }
}
