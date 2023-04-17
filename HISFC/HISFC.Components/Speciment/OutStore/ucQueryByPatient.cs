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
                       " LEFT JOIN SPEC_PATIENT ON SPEC_SOURCE.PATIENTID = SPEC_PATIENT.PATIENTID \n"+
                       " WHERE ( SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3' OR SPEC_SUBSPEC.STATUS='2'))";
            //strQuery = "SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID 标本号,  SPEC_TYPE.SPECIMENTNAME 标本类型,COM_DEPARTMENT.DEPT_NAME 送存科室,\n" +
            //                "SPEC_DISEASETYPE.DISEASENAME 病种,SPEC_SOURCE.TUMORPOR 癌性质, SPEC_SOURCE_STORE.TUMORTYPE 癌种类,\n" +
            //                "SPEC_SOURCE_STORE.STORETIME 入库时间,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) 出库次数,\n" +
            //                " SPEC_BOX.BOXBARCODE 盒条码,\n" +
            //                " SPEC_SUBSPEC.BOXENDROW 行,SPEC_SUBSPEC.BOXENDCOL 列\n" +
            //                " FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID,SPEC_SOURCE_STORE,\n" +
            //                " SPEC_SOURCE LEFT JOIN SPEC_PATIENT ON SPEC_SOURCE.PATIENTID = SPEC_PATIENT.PATIENTID, SPEC_DISEASETYPE,SPEC_TYPE,COM_DEPARTMENT,SPEC_BOX\n" +
            //                " WHERE SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID AND　SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
            //                " AND SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID \n" +
            //                " AND SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID AND SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID \n" +
            //                " AND COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO AND SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3')\n";//读取该申请表的申请要求                            

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
            //查询国家列表
            ArrayList countryList = new ArrayList();
            countryList = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.txtNationality.AddItems(countryList);

            //查询民族列表
            ArrayList Nationallist = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.txtNation.AddItems(Nationallist);

            //血型列表
            ArrayList BloodTypeList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);// baseDml.GetBloodType();
            this.txtBloodType.AddItems(BloodTypeList);

            //婚姻列表
            ArrayList MaritalStatusList = con.GetList("MaritalStatus");
            this.txtMaritalStatus.AddItems(MaritalStatusList);
        }
    }
}
