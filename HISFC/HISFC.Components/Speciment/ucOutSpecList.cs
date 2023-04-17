using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucOutSpecList : UserControl
    {        
        private ApplyTableManage applyTableManage;
        private FS.HISFC.BizLogic.Speciment.SpecApplyOutManage appMgr = new FS.HISFC.BizLogic.Speciment.SpecApplyOutManage();
        private ApplyTable currentTable;
        private string curApplyNum;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private string strSql;
        private string pageSql;

        /// <summary>
        /// ����ID��
        /// </summary>
        private string applyID = string.Empty;
        public string ApplyID
        {
            get
            {
                return this.applyID;
            }
            set
            {
                this.applyID = value;
                this.txtApplyNum.Text = value;
            }
        }

        public string PageSQL
        {
            get
            {
                return pageSql;
            }
            set
            {
                pageSql = value;
            }
        }

        public ucOutSpecList(FS.HISFC.Models.Base.Employee login)
        {
            InitializeComponent();
            applyTableManage = new ApplyTableManage();
            currentTable = new ApplyTable();
            loginPerson = login;
            strSql = "";           
        }

        /// <summary>
        /// ArrayList �е� table�ϲ�
        /// </summary>
        /// <param name="arrTable"></param>
        /// <returns>�ϲ����DataTable</returns>
        public DataTable MergeTable(ArrayList arrTable)
        {
            //nspSpecList_Sheet1.BindDataColumn(3, "�걾��");
            //nspSpecList_Sheet1.BindDataColumn(4, "�걾����");
            //nspSpecList_Sheet1.BindDataColumn(5, "����");
            //nspSpecList_Sheet1.BindDataColumn(6, "������");
            //nspSpecList_Sheet1.BindDataColumn(7, "�ʹ����");
            //nspSpecList_Sheet1.BindDataColumn(8, "������");
            //nspSpecList_Sheet1.BindDataColumn(9, "���ʱ��");
            //nspSpecList_Sheet1.BindDataColumn(10, "�������");
            //nspSpecList_Sheet1.BindDataColumn(11, "�����");
            //nspSpecList_Sheet1.BindDataColumn(12, "�ɼ��׶�");
            //nspSpecList_Sheet1.BindDataColumn(13, "��Ĥ");
            //nspSpecList_Sheet1.BindDataColumn(14, "����֯");
            DataTable mergedTable = new DataTable();
            #region datatable �г�ʼ��
            DataColumn dt = new DataColumn();
            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "����";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "����";
            mergedTable.Columns.Add(dt);

            DataColumn dtSubSpecId = new DataColumn();
            dtSubSpecId.DataType = System.Type.GetType("System.Int32");
            dtSubSpecId.ColumnName = "�걾��";
            mergedTable.Columns.Add(dtSubSpecId);

            DataColumn dtSpecType = new DataColumn();
            dtSpecType.DataType = System.Type.GetType("System.String");
            dtSpecType.ColumnName = "�걾����";
            mergedTable.Columns.Add(dtSpecType);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "���ﲿλ";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "������";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "����";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "������";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�Ա�";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "����";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�ʹ����";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�ʹ�ҽ��";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�ʹ�����";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�����";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "������";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�������";
            mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "�����";
            //mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "������";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "��";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "��";
            mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "������";
            //mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "����";
            //mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "���";
            //mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "�ʹ�����";
            //mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "���Ʒ���";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "���Ʒ���";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "��Ժ���";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�������";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "��Ժ���";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "�ڿ�״̬";
            mergedTable.Columns.Add(dt);

            #endregion 
            foreach (DataTable dt1 in arrTable)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    DataRow newDr = mergedTable.NewRow();
                    newDr.ItemArray = dr.ItemArray;                    
                    mergedTable.Rows.Add(newDr);                    
                } 
            }
            return mergedTable;
        }

        public void DataBinding(DataTable dtResult)
        {
            try
            {
                for (int i = 0; i < dtResult.Rows.Count;i++ )
                {
                    DataRow newDr = dtResult.Rows[i];
                    #region ������
                    if (newDr["������"].ToString().Trim() != "")
                    {
                        char[] tumorPor = newDr["������"].ToString().ToCharArray();
                        newDr["������"] = "";
                        Constant.TumorPro TumorPro = Constant.TumorPro.ԭ����;
                        foreach (char t in tumorPor)
                        {
                            TumorPro = (Constant.TumorPro)(Convert.ToInt32(t.ToString()));
                            switch (TumorPro)
                            {
                                //�걾����1.ԭ���� 2.������ 3.ת�ư�
                                case Constant.TumorPro.ԭ����:
                                    newDr["������"] += "ԭ����";
                                    break;
                                case Constant.TumorPro.������:
                                    newDr["������"] += "������";
                                    break;
                                case Constant.TumorPro.ת�ư�:
                                    newDr["������"] += "ת�ư�";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    #region ������
                    if (newDr["������"].ToString().Trim() != "")
                    {
                        //1.���� 2.���� 3.���� 4�����硢�ж� 5.��˨ 6, ��(ѪҺ), 7 ����(ѪҺ)
                        char[] tumorType = newDr["������"].ToString().ToCharArray();
                        newDr["������"] = "";
                        foreach (char t in tumorType)
                        {
                            Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(t.ToString()));
                            switch (TumorType)
                            {
                                case Constant.TumorType.����:
                                    newDr["������"] += "����";
                                    break;
                                case Constant.TumorType.����:
                                    newDr["������"] += "����";
                                    break;
                                //case Constant.TumorType.����:
                                //    newDr["������"] += "��";
                                //    break;
                                case Constant.TumorType.����:
                                    newDr["������"] += "����";
                                    break;
                                case Constant.TumorType.��˨:
                                    newDr["������"] += "��˨";
                                    break;
                                case Constant.TumorType.����:
                                    newDr["������"] += "����";
                                    break;
                                case Constant.TumorType.�ܰͽ�:
                                    newDr["������"] += "�ܰͽ�";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    //if (newDr["���"].ToString().Trim() != "")
                    //{

                    //    switch (newDr["���"].ToString())
                    //    {
                    //        case "L":
                    //            newDr["���"] = "���";
                    //            break;
                    //        case "R":
                    //            newDr["���"] = "�Ҳ�";
                    //            break;
                    //        case "A":
                    //            newDr["���"] = "ȫ��";
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}

                    newDr["�ʹ�����"] = Convert.ToDateTime(newDr["�ʹ�����"].ToString()).ToString("yyyy-MM-dd");

                    if (newDr["�ڿ�״̬"].ToString().Trim() != "")
                    {

                        switch (newDr["�ڿ�״̬"].ToString())
                        {
                            case "1":
                                newDr["�ڿ�״̬"] = "δ���";
                                break;
                            case "3":
                                newDr["�ڿ�״̬"] = "�ѹ黹";
                                break;
                            case "2":
                                newDr["�ڿ�״̬"] = "δ�黹";
                                break;
                            default:
                                break;
                        }
                    }
                    newDr["���Ʒ���"] = newDr["���Ʒ���"] == null ? "" : newDr["���Ʒ���"].ToString();
                    newDr["���Ʒ���"] = newDr["���Ʒ���"] == null ? "" : newDr["���Ʒ���"].ToString();
                    newDr["��Ժ���"] = newDr["��Ժ���"] == null ? "" : newDr["��Ժ���"].ToString();
                    newDr["��Ժ���"] = newDr["��Ժ���"] == null ? "" : newDr["��Ժ���"].ToString();
                    newDr["�����"] = newDr["�����"] == null ? "" : newDr["�����"].ToString();
                    newDr["�������"] = newDr["�������"] == null ? "" : newDr["�������"].ToString();
                }            
            
                //nspSpecList_Sheet1.Rows.Count = 0;
                nspSpecList_Sheet1.AutoGenerateColumns = false;
                 
                nspSpecList_Sheet1.DataSource = null;
                nspSpecList_Sheet1.DataSource = dtResult;
                nspSpecList_Sheet1.ColumnCount = 29;
                nspSpecList_Sheet1.BindDataColumn(4, "����");
                nspSpecList_Sheet1.BindDataColumn(5, "����");
                nspSpecList_Sheet1.BindDataColumn(6, "�걾��");
                nspSpecList_Sheet1.BindDataColumn(7, "�걾����");
                nspSpecList_Sheet1.BindDataColumn(8, "���ﲿλ");
                nspSpecList_Sheet1.BindDataColumn(9, "������");
                nspSpecList_Sheet1.BindDataColumn(10, "����");
                nspSpecList_Sheet1.BindDataColumn(11, "������");
                nspSpecList_Sheet1.BindDataColumn(12, "�Ա�");
                nspSpecList_Sheet1.BindDataColumn(13, "����");
                nspSpecList_Sheet1.BindDataColumn(14, "�ʹ����");
                nspSpecList_Sheet1.BindDataColumn(15, "�ʹ�ҽ��");
                nspSpecList_Sheet1.BindDataColumn(16, "�ʹ�����");
                nspSpecList_Sheet1.BindDataColumn(17, "�����");

                nspSpecList_Sheet1.BindDataColumn(18, "������");
                nspSpecList_Sheet1.BindDataColumn(19, "�������");
                nspSpecList_Sheet1.BindDataColumn(20, "������");
                nspSpecList_Sheet1.BindDataColumn(21, "��");
                nspSpecList_Sheet1.BindDataColumn(22, "��");   
                nspSpecList_Sheet1.BindDataColumn(23, "���Ʒ���");
                nspSpecList_Sheet1.BindDataColumn(24, "���Ʒ���");
                nspSpecList_Sheet1.BindDataColumn(25, "��Ժ���");
                nspSpecList_Sheet1.BindDataColumn(26, "�������");
                nspSpecList_Sheet1.BindDataColumn(27, "��Ժ���");
                nspSpecList_Sheet1.BindDataColumn(28, "�ڿ�״̬");
                #region ����ʾ��Ŷ 
                nspSpecList_Sheet1.Columns[18].Visible = false;
                nspSpecList_Sheet1.Columns[19].Visible = false;
                nspSpecList_Sheet1.Columns[20].Visible = false;
                nspSpecList_Sheet1.Columns[21].Visible = false;
                nspSpecList_Sheet1.Columns[22].Visible = false;
                nspSpecList_Sheet1.Columns[23].Visible = false;
                nspSpecList_Sheet1.Columns[24].Visible = false;
                nspSpecList_Sheet1.Columns[25].Visible = false;
                nspSpecList_Sheet1.Columns[26].Visible = false;
                nspSpecList_Sheet1.Columns[27].Visible = false;
                nspSpecList_Sheet1.Columns[28].Visible = false;
                #endregion
                lblCount.Text = nspSpecList_Sheet1.Rows.Count.ToString();
                foreach (FarPoint.Win.Spread.Row cur in nspSpecList_Sheet1.Rows)
                {
                    int index = cur.Index;
                    nspSpecList_Sheet1.Cells[index, 0].Value = false;
                    if (nspSpecList_Sheet1.Cells[index, 22].Value.ToString().Contains("δ�黹"))
                    {
                        FarPoint.Win.Spread.Row r = cur;
                        r.BackColor = Color.Aquamarine;
                        nspSpecList_Sheet1.Cells[index, 0].Locked = true;
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// ��ѯ�������ĳ���걾�б�
        /// </summary>
        /// <param name="spec"></param>
        public void QueryImpedOutSpec(string spec)
        {
            string sql = " SELECT DISTINCT SPEC_SUBSPEC.SUBBARCODE ����,SPEC_DISEASETYPE.DISEASENAME ����,SPEC_SOURCE.SPEC_NO �걾��,  SPEC_TYPE.SPECIMENTNAME �걾����, \n" +
                       " SPEC_SOURCE_STORE.TUMORPOS ���ﲿλ,SPEC_SOURCE_STORE.TUMORTYPE ������,SPEC_PATIENT.NAME ����,SPEC_PATIENT.CARD_NO ������,SPEC_PATIENT.GENDER �Ա�, \n" +
                       " SPEC_PATIENT.BIRTHDAY ����,COM_DEPARTMENT.DEPT_NAME �ʹ����,COM_EMPLOYEE.EMPL_NAME �ʹ�ҽ��, SPEC_SOURCE.SENDDATE �ʹ�����,MAIN_DIANAME �����,  \n" +
                       " SPEC_SOURCE.TUMORPOR ������,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) �������,\n" +
                       " SPEC_BOX.BOXBARCODE ������,SPEC_SUBSPEC.BOXENDROW ��,SPEC_SUBSPEC.BOXENDCOL ��,\n" +
                       " SPEC_SOURCE.RADSCHEME ���Ʒ���, SPEC_SOURCE.MEDSCHEME ���Ʒ���, \n" +
                       " INHOS_DIANAME ��Ժ���, CLINIC_DIANAME �������, M_DIAGICDNAME ��Ժ���,SPEC_SUBSPEC.STATUS �ڿ�״̬ \n" +
                       " FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID \n" +
                       " INNER JOIN SPEC_APPLY_OUT ON  SPEC_APPLY_OUT.SUBSPECBARCODE = SPEC_SUBSPEC.SUBBARCODE \n" +
                       " INNER JOIN SPEC_BOX ON SPEC_APPLY_OUT.BOXID = SPEC_BOX.BOXID\n" +
                       " INNER JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID\n" +
                       " INNER JOIN SPEC_SOURCE ON  SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID\n" +
                       " LEFT JOIN SPEC_DISEASETYPE ON SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
                       " LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID\n" +
                       " LEFT JOIN	COM_DEPARTMENT ON COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO \n" +
                       " LEFT JOIN SPEC_BASE ON SPEC_BASE.SPECID = SPEC_SOURCE.SPECID \n" +
                       " LEFT JOIN SPEC_PATIENT ON SPEC_PATIENT.PATIENTID = SPEC_SOURCE.PATIENTID \n" +
                       " LEFT JOIN COM_EMPLOYEE ON COM_EMPLOYEE.EMPL_CODE = SPEC_SOURCE.SENDDOCID \n" +
                       " WHERE SPEC_SUBSPEC.SUBSPECID IN (" + spec + ")";           
            
            DataSet ds = new DataSet();
            applyTableManage.ExecQuery(sql, ref ds);
            if (ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                DataBinding(ds.Tables[0]);
            }             
        }

        /// <summary>
        /// ��ѯ������Ӧ������Ҫ��
        /// </summary>
        /// <param name="applyNum">�����ID</param>
        public void QuerySpecListByApplyNum(string applyNum)
        {
            curApplyNum = applyNum;
            try
            {
                List<string> sqlList = new List<string>();
                //��������ID��ѯ�����
                currentTable = applyTableManage.QueryApplyByID(applyNum);
                if (currentTable.ApplyId > 0)
                {
                    #region sql���
                    /*strSql = " SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID �걾��,  SPEC_TYPE.SPECIMENTNAME �걾����,COM_DEPARTMENT.DEPT_NAME �ʹ����,\n" +
                       " SPEC_DISEASETYPE.DISEASENAME ����,SPEC_SOURCE.TUMORPOR ������, SPEC_SOURCE_STORE.TUMORTYPE ������,\n" +
                       " SPEC_SOURCE_STORE.STORETIME ���ʱ��,\n" +
                       " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) �������,\n" +
                       " SPEC_BOX.BOXBARCODE ������,\n" +
                       " SPEC_SUBSPEC.BOXENDROW ��,SPEC_SUBSPEC.BOXENDCOL ��,\n" +
                       " SPEC_SOURCE_STORE.TUMORPOS ���ﲿλ, SPEC_SOURCE.RADSCHEME ���Ʒ���, SPEC_SOURCE.MEDSCHEME ���Ʒ���, \n" +
                       " MAIN_DIANAME �����, INHOS_DIANAME ��Ժ���, CLINIC_DIANAME �������, M_DIAGICDNAME ��Ժ���,SPEC_SUBSPEC.STATUS �ڿ�״̬ ,SPEC_SUBSPEC.SUBBARCODE ����\n" +
                       "  FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID \n" +
                       "  INNER JOIN SPEC_BOX ON SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID\n" +
                       "  INNER JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID\n" +
                       "  INNER JOIN SPEC_SOURCE ON  SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID\n" +
                       " LEFT JOIN SPEC_DISEASETYPE ON SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
                       " LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID\n" +
                       " LEFT JOIN	COM_DEPARTMENT ON COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO \n" +
                       " LEFT JOIN SPEC_BASE ON SPEC_BASE.SPECID = SPEC_SOURCE.SPECID \n" +
                       " WHERE SPEC_SUBSPEC.SPECID>0 and ( SPEC_SUBSPEC.INSTORE='S'AND (SPEC_SUBSPEC.STATUS='1' OR SPEC_SUBSPEC.STATUS='3' OR SPEC_SUBSPEC.STATUS='2'))";*/
                    #endregion
                    /*
                    string[] specType = currentTable.SpecType.Split(',');
                    string[] specIsCancer = currentTable.SpecIsCancer.Split(',');
                    string[] specDetAmount = currentTable.SpecDetAmout.Split(',');
                    string[] specDisType = currentTable.DiseaseType.Split(',');
                    ArrayList arrList = new ArrayList();
                    DataTable dtReslut = new DataTable();
                    //��������Ҫ���ѯ�����������ı걾
                    for (int i = 0; i < specType.Length; i++)
                    {
                        string curSql = strSql+ " AND SPEC_SOURCE_STORE.SPECTYPEID =" + specType[i] + "\n";
                        curSql += " AND SPEC_SOURCE_STORE.TUMORTYPE ='" + specIsCancer[i] + "'\n";
                        curSql += " AND SPEC_SOURCE.DISEASETYPEID=" + specDisType[i] + "\n ORDER BY SPEC_SUBSPEC.SUBSPECID";
                       
                        if (sqlList.Contains(curSql) && i != 0)
                            break;
                        sqlList.Add(curSql);
                        DataSet ds = new DataSet();
                        applyTableManage.ExecQuery(curSql, ref ds);
                        arrList.Add(ds.Tables[0]);                        
                    }
                    */
                    #region ��ȡsql��估Dataset
                    ArrayList arrList = new ArrayList();
                    DataTable dtReslut = new DataTable();
                    ArrayList appTab = this.appMgr.GetSubSpecOut(applyNum);
                    string specBar = string.Empty;
                    if ((appTab != null) && (appTab.Count > 0))
                    {
                        string alBar = string.Empty;
                        foreach (FS.HISFC.Models.Speciment.SpecOut spOut in appTab)
                        {
                            if (string.IsNullOrEmpty(alBar))
                            {
                                alBar = "'" + spOut.SubSpecBarCode + "'";
                            }
                            else
                            {
                                alBar = alBar + "," + "'" + spOut.SubSpecBarCode + "'";
                            }
                        }
                        if (!string.IsNullOrEmpty(alBar))
                        {
                            specBar = alBar;
                        }
                        string curSql = this.GetSql() + " where SPEC_SUBSPEC.SUBBARCODE in ({0})";
                        curSql = string.Format(curSql, specBar);
                    
                        DataSet ds = new DataSet();
                        applyTableManage.ExecQuery(curSql, ref ds);
                        arrList.Add(ds.Tables[0]);
                    }
                    #endregion

                    #region FarPoint ���ݰ�
                    dtReslut = MergeTable(arrList);
                    DataBinding(dtReslut);                    
                    #endregion
                }
            }
            catch 
            {

            }
        }

        /// <summary>
        /// ��ϸ�Ĳ�ѯ����
        /// </summary>
        /// <returns></returns>
        private string GetSql()
        {
            #region
            string sql = " SELECT DISTINCT SPEC_SUBSPEC.SUBBARCODE ����,SPEC_DISEASETYPE.DISEASENAME ����,SPEC_SOURCE.SPEC_NO �걾��,  SPEC_TYPE.SPECIMENTNAME �걾����, \n" +
                       " SPEC_SOURCE_STORE.TUMORPOS ���ﲿλ,SPEC_SOURCE_STORE.TUMORTYPE ������,SPEC_PATIENT.NAME ����,SPEC_PATIENT.CARD_NO ������,SPEC_PATIENT.GENDER �Ա�, \n" +
                       " SPEC_PATIENT.BIRTHDAY ����,COM_DEPARTMENT.DEPT_NAME �ʹ����,COM_EMPLOYEE.EMPL_NAME �ʹ�ҽ��, SPEC_SOURCE.SENDDATE �ʹ�����,MAIN_DIANAME �����,  \n" +
                       " SPEC_SOURCE.TUMORPOR ������,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) �������,\n" +
                       " SPEC_BOX.BOXBARCODE ������,SPEC_SUBSPEC.BOXENDROW ��,SPEC_SUBSPEC.BOXENDCOL ��,\n" +
                       " SPEC_SOURCE.RADSCHEME ���Ʒ���, SPEC_SOURCE.MEDSCHEME ���Ʒ���, \n" +
                       " INHOS_DIANAME ��Ժ���, CLINIC_DIANAME �������, M_DIAGICDNAME ��Ժ���,SPEC_SUBSPEC.STATUS �ڿ�״̬ \n" +
                       " FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID \n" +
                       " INNER JOIN SPEC_APPLY_OUT ON  SPEC_APPLY_OUT.SUBSPECBARCODE = SPEC_SUBSPEC.SUBBARCODE \n" +
                       " INNER JOIN SPEC_BOX ON SPEC_APPLY_OUT.BOXID = SPEC_BOX.BOXID\n" +
                       " INNER JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID\n" +
                       " INNER JOIN SPEC_SOURCE ON  SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID\n" +
                       " LEFT JOIN SPEC_DISEASETYPE ON SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID \n" +
                       " LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID\n" +
                       " LEFT JOIN	COM_DEPARTMENT ON COM_DEPARTMENT.DEPT_CODE = SPEC_SOURCE.DEPTNO \n" +
                       " LEFT JOIN SPEC_BASE ON SPEC_BASE.SPECID = SPEC_SOURCE.SPECID \n" +
                       " LEFT JOIN SPEC_PATIENT ON SPEC_PATIENT.PATIENTID = SPEC_SOURCE.PATIENTID \n" +
                       " LEFT JOIN COM_EMPLOYEE ON COM_EMPLOYEE.EMPL_CODE = SPEC_SOURCE.SENDDOCID \n";
            return sql;
            #endregion
        }

        /// <summary>
        /// ����������
        /// </summary>    
        /// <param name="outInfoList">Ԥ����ı걾�б�List</param>
        /// <param name="listSubSpecId"></param>
        /// <returns></returns>
        private int UpdateApplyTable(List<OutInfo> outInfoList)
        {
            string specList = "";
            string isReturnList = "";
           
            #region  �����ID�б�
            foreach (OutInfo info in outInfoList)
            {
                //����ı걾ID�б�    
                specList += info.SpecBarCode;
                specList += ",";
                //�걾�ɻ��б�,��걾IDһһ��Ӧ
                isReturnList += info.ReturnAble;
                isReturnList += ",";
                if (info.ReturnAble == "1")
                {
                    DateTime dtReturn = DateTime.Now;
                    dtReturn = dtReturn.AddDays(info.ReturnDays);
                    string sql = " UPDATE SPEC_SUBSPEC  " +
                                 " SET ISRETURNABLE=1,DATERETURNTIME= to_date('" + dtReturn.ToString() + "','yyyy-mm-dd hh24:mi:ss')" +
                                 " WHERE SUBSPECID =" + info.SpecId;
                    int result = applyTableManage.ExecNoQuery(sql);
                    if (result == -1)
                    {
                        return -1;
                    }
                }
            }
            #endregion

            specList += currentTable.SpecList;
            isReturnList += currentTable.IsImmdBackList;

            string updateTablesql = "";
            if (curApplyNum != null && curApplyNum != "")
            {
                updateTablesql = " UPDATE SPEC_APPLICATIONTABLE" +
                             " SET SPECLIST = '" + specList + "',ISIMMEDBACKLIST='" + isReturnList + "'" +
                             " WHERE APPLICATIONID=" + curApplyNum;
                return applyTableManage.ExecNoQuery(updateTablesql);
            }
            return 1;
        }
       
        /// <summary>
        /// �����������һ���ط�����ʾ�걾����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nspSpecList_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            //int rowIndex = nspSpecList_Sheet1.ActiveRowIndex;
            //if (rowIndex >= 0&&nspSpecList_Sheet1.Rows.Count>0)
            //{
            //    string applyNum = nspSpecList_Sheet1.Cells[rowIndex, 4].Value.ToString();
            //    string sql = "SELECT DISTINCT SPEC_SUBSPEC.SUBBARCODE ������,  SPEC_SUBSPEC.SPECCAP ����,SPEC_SOURCE_STORE.TUMORPOS ���ﲿλ,ORGNAME �걾��֯����," +
            //                 "SPEC_SOURCE_STORE.SIDEFROM ���, SPEC_SOURCE.SENDDATE,SPEC_SOURCE.RADSCHEME ���Ʒ���, SPEC_SOURCE.MEDSCHEME ���Ʒ���," +
            //                 "INHOS_DIANAME ��Ժ���, CLINIC_DIANAME �������,MAIN_DIANAME �����,M_DIAGICDNAME ��Ժ���" +
            //                 " FROM SPEC_SUBSPEC LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID,SPEC_SOURCE_STORE," +
            //                 " SPEC_SOURCE LEFT JOIN SPEC_BASE ON SPEC_BASE.SPECID = SPEC_SOURCE.SPECID, SPEC_DISEASETYPE," +
            //                 " SPEC_TYPE LEFT JOIN SPEC_ORGTYPE ON SPEC_ORGTYPE.ORGTYPEID = SPEC_TYPE.OrgTypeID" +
            //                 " WHERE SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID" +
            //                 " AND SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID" +
            //                 " AND SPEC_SUBSPEC.SPECID=SPEC_SOURCE.SPECID AND SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID" +
            //                 " AND SPEC_SUBSPEC.SUBSPECID=" + applyNum;
            //    DataSet ds = new DataSet();
            //    try
            //    {
            //        applyTableManage.ExecQuery(sql, ref ds);
            //        if (ds.Tables.Count >= 1)
            //        {
            //            DataTable dt = ds.Tables[0];
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                txtBarCode.Text = dr["������"].ToString();
            //                txtCap.Text = dr["����"].ToString();
            //                txtTumorPos.Text = dr["���ﲿλ"].ToString();
            //                txtOrgName.Text = dr["�걾��֯����"].ToString();
            //                switch (dr["���"].ToString())
            //                {
            //                    case "L":
            //                        txtSideFrom.Text = "���";
            //                        break;
            //                    case "R":
            //                        txtSideFrom.Text = "�Ҳ�";
            //                        break;
            //                    case "A":
            //                        txtSideFrom.Text = "ȫ��";
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                txtRadSch.Text = dr["���Ʒ���"] == null ? "" : dr["���Ʒ���"].ToString();
            //                txtMedSch.Text = dr["���Ʒ���"] == null ? "" : dr["���Ʒ���"].ToString();
            //                txtInDia.Text = dr["��Ժ���"] == null ? "" : dr["��Ժ���"].ToString();
            //                txtOutDia.Text = dr["��Ժ���"] == null ? "" : dr["��Ժ���"].ToString();
            //                txtMainDia.Text = dr["�����"] == null ? "" : dr["�����"].ToString();
            //                txtCliDia.Text = dr["�������"] == null ? "" : dr["�������"].ToString();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        string message = ex.Message;
            //    }
            //}
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    FarPoint.Win.Spread.Row r = nspSpecList_Sheet1.Rows[i];
                    if (r.Visible && !nspSpecList_Sheet1.Cells[i, 22].Value.ToString().Contains("δ�黹"))
                    {                        
                        nspSpecList_Sheet1.Cells[i, 0].Value = true;
                    }                    
                } 
            }
            else
            {
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    nspSpecList_Sheet1.Cells[i, 0].Value = (object)0;
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOut_Click(object sender, EventArgs e)
        {
            if (txtApplyNum.Text.Trim() == "" && curApplyNum=="")
            {
                MessageBox.Show("���������뵥��");
                return;
            }
            #region ����˵��
            //�¼�˵������ȡѡ�б걾��������
            //������Ϣ�Ļ�ȡ��
            //1. ��ѡ�еı걾����List��
            //2. �걾�Ƿ�ɻ�������ɻ������û��걾����
            //������Ϣ
            //1. ����ÿһ�걾�� Status�ֶΣ��ڿ��״̬�����걾��λ�ã�Instore״̬��Ϣ��Լ���ķ���ʱ��
            //2. ������������Ϣ���걾����ID�б�IsImmedBackList��SpecCountInDept��PerInAll��LeftAmout
            //3. ���³�����Ϣ��
            //4.���±� Spec_Source_Store SotreCount 
            #endregion
            List<SubSpec> outSpecNum = new List<SubSpec>();
            List<OutInfo> outInfoList = new List<OutInfo>();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            try
            {
                SubSpecManage tmp = new SubSpecManage();
                tmp.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                applyTableManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); 
               
                //��ѡ�еı걾����List��
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    if (nspSpecList_Sheet1.Cells[i, 0].Value != null && (nspSpecList_Sheet1.Cells[i, 0].Value.ToString().ToUpper() == "TRUE"||nspSpecList_Sheet1.Cells[i, 0].Value.ToString()=="1"))
                    {
                        OutInfo outInfo = new OutInfo();
                        string subSpecID = nspSpecList_Sheet1.Cells[i, 4].Value.ToString();
                        string subBarCode = nspSpecList_Sheet1.Cells[i, 23].Text.Trim();
                        
                        string boxBarCode = nspSpecList_Sheet1.Cells[i, 12].Value.ToString();
                        decimal count =0.0M;
                        if (nspSpecList_Sheet1.Cells[i, 3].Value != null)
                        {
                            count = Convert.ToDecimal(nspSpecList_Sheet1.Cells[i, 3].Value.ToString());
                            outInfo.Count = count;
                        }
                        outSpecNum.Add(tmp.GetSubSpecById(subSpecID, ""));
                        outInfo.SpecId = subSpecID;
                        outInfo.BoxBarCode = boxBarCode;
                        outInfo.SpecBarCode = subBarCode;
                        //�걾�ɻ��б�
                        if (nspSpecList_Sheet1.Cells[i, 1].Value != null && (nspSpecList_Sheet1.Cells[i, 1].Value.ToString().ToUpper() == "TRUE" || nspSpecList_Sheet1.Cells[i, 1].Value.ToString() == "1"))
                        {
                            outInfo.ReturnAble = "1";
                            outInfo.ReturnDays = nspSpecList_Sheet1.Cells[i, 2].Value == null ? 0 : Convert.ToInt32(nspSpecList_Sheet1.Cells[i, 2].Value.ToString());
                       }
                       if (nspSpecList_Sheet1.Cells[i, 1].Value != null && (nspSpecList_Sheet1.Cells[i, 1].Value.ToString() == "2"))
                       {
                           outInfo.ReturnAble = "2";
                       }
                       if (nspSpecList_Sheet1.Cells[i, 1].Value != null && (nspSpecList_Sheet1.Cells[i, 1].Value.ToString() == "0"))
                       {
                           outInfo.ReturnAble = "0";
                       }
                       if (nspSpecList_Sheet1.Cells[i, 1].Value == null)
                       {
                           outInfo.ReturnAble = "0";
                       }
                       outInfoList.Add(outInfo);
                    }
                }
                SpecOutOper specOut;
                if (txtApplyNum.Text.Trim() != "")
                {
                    try
                    {
                        curApplyNum = txtApplyNum.Text.Trim();
                        if (applyTableManage.QueryApplyByID(curApplyNum).ApplyId <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���뵥�Ų����ڣ�", " ����");
                            return;
                        }
                        //specOut = new SpecOutOper(currentTable, curApplyNum, loginPerson);
                        
                    }
                    catch
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����ű���Ϊ���֣�", " ����");
                        return;
                    }
                }
                specOut = new SpecOutOper(currentTable, loginPerson);
                if (UpdateApplyTable(outInfoList) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�������뵥ʧ�ܣ�", "�걾����");
                    return;
                }
                //Ԥ���⣬ֻ��������뵥 ������¿��
                //specOut.UpdateApplyTable(outInfoList, trans);
                //û��Ԥ����Ĵ���
                //specOut.SpecOut(outInfoList, trans);
                try
                {
                    specOut.PrintTitle = "�걾�����嵥";
                    specOut.PrintOutSpec(outSpecNum, FS.FrameWork.Management.PublicTrans.Trans);
                }
                catch
                {                   
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��ӡʧ��", "�걾����");
                    return;
                }
                QuerySpecListByApplyNum(curApplyNum);                
                //���°����
                if (pageSql != null && pageSql != "")
                {
                    DataSet ds = new DataSet();
                    applyTableManage.ExecQuery(PageSQL, ref ds);
                    if (ds.Tables.Count > 0)
                    {
                        DataBinding(ds.Tables[0]);
                    }
                    //PageSQL = "";
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("����ɹ�!", "�걾����");
            }
            catch (Exception ex)
            {
               string message = ex.Message;
               FS.FrameWork.Management.PublicTrans.RollBack();
               MessageBox.Show(ex.Message, "�걾����");
            }
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "��ѯ���������ѡ��Excel�ļ�����λ��";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "�걾";
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.nspSpecList.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            }
        }

        private void nspSpecList_AutoFilteredColumn(object sender, FarPoint.Win.Spread.AutoFilteredColumnEventArgs e)
        {
            string filterString = e.FilterString;
            if (filterString == "(All)")
            {
                for (int i = 0; i < nspSpecList_Sheet1.RowCount; i++)
                {
                    FarPoint.Win.Spread.Row r = nspSpecList_Sheet1.Rows[i];
                    r.Visible = true;
                }
                return;
            }
            for (int i = 0; i < nspSpecList_Sheet1.RowCount; i++)
            {
                FarPoint.Win.Spread.Row r = nspSpecList_Sheet1.Rows[i];
                string s1 = nspSpecList_Sheet1.Cells[i, e.Column].Value.ToString();
                if (s1 != filterString)
                {
                    r.Visible = false;
                }
                else
                    r.Visible = true;
            }
        }

        private void chkBack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBack.CheckState == CheckState.Checked)
            {
                chkBack.Text = "�黹";
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    if (!nspSpecList_Sheet1.Cells[i, 22].Value.ToString().Contains("δ�黹"))
                        nspSpecList_Sheet1.Cells[i, 1].Value = (object)1;
                }
            }
            if (chkBack.CheckState == CheckState.Unchecked)
            {
                chkBack.Text = "���黹";
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    nspSpecList_Sheet1.Cells[i, 1].Value = (object)0;
                }
            }

            if (chkBack.CheckState == CheckState.Indeterminate)
            {
                chkBack.Text = "��γ���";
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    if (!nspSpecList_Sheet1.Cells[i, 22].Value.ToString().Contains("δ�黹"))
                        nspSpecList_Sheet1.Cells[i, 1].Value = (object)2;
                }
            }
        }

        private void nudBorDay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int days = Convert.ToInt32(nudBorDay.Value.ToString());
                if (days > 0)
                {
                    for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                    {
                        string check = "";                       
                        string backStatus = "";
                        backStatus = nspSpecList_Sheet1.Cells[i, 1].Value == null ? "" : nspSpecList_Sheet1.Cells[i, 1].Value.ToString();
                        check = nspSpecList_Sheet1.Cells[i, 0].Value == null ? "" : nspSpecList_Sheet1.Cells[i, 0].Value.ToString();
                        if (backStatus == "1" && (check == "1" || check.ToUpper() == "TRUE"))
                            nspSpecList_Sheet1.Cells[i, 2].Text = days.ToString();
                    }
                }
            }
            catch
            { }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            decimal count = Convert.ToDecimal(nudCount.Value.ToString());
            if (count > 0.0M)
            {                
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    string check = "";
                    check = nspSpecList_Sheet1.Cells[i, 0].Value == null ? "" : nspSpecList_Sheet1.Cells[i, 0].Value.ToString();
                    if (check == "1" || check.ToUpper() == "TRUE")
                        nspSpecList_Sheet1.Cells[i, 3].Text = count.ToString();
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int rowIndex = nspSpecList_Sheet1.ActiveRowIndex;
            if (rowIndex >= 0)
            {
                string subSpecID = nspSpecList_Sheet1.Cells[rowIndex, 4].Value.ToString();
                SubSpecManage tmpManage = new SubSpecManage();
                SubSpec tmpSub = tmpManage.GetSubSpecById(subSpecID, "");
                OrgTypeManage orgManage = new OrgTypeManage();
                string orgName = orgManage.GetBySpecType(tmpSub.SpecTypeId.ToString()).OrgName;

                if (tmpSub != null && tmpSub.SubSpecId > 0)
                {
                    if(orgName.Contains("Ѫ"))
                    {
                        ucSpecimentSource ucBld = new ucSpecimentSource();
                        ucBld.SpecId = tmpSub.SpecId;
                        Size size = new Size();
                        size.Height = 800;
                        size.Width = 1280;
                        ucBld.Size = size;

                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucBld, FormBorderStyle.FixedSingle, FormWindowState.Normal);
                        //FS.FrameWork.WinForms.Classes.Function.PopForm popForm = new FS.NFC.Interface.Forms.BaseForm();
                 
                        
                    }
                    if (orgName.Contains("��֯"))
                    {
                        ucSpecSourceForOrg ucOrg = new ucSpecSourceForOrg();
                        ucOrg.SpecId = tmpSub.SpecId;                         
                       // FormBorderStyle formStyle = new FormBorderStyle();
                        Size size = new Size();
                        size.Height = 800;
                        size.Width = 1280;
                        ucOrg.Size = size;

                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucOrg, FormBorderStyle.FixedSingle, FormWindowState.Normal);
                        //FS.FrameWork.WinForms.Classes.Function.PopForm popForm = new FS.NFC.Interface.Forms.BaseForm();
                        
                    }
 
                }
            }
        }

        private class UseDeptSpecInfo
        {
            public string deptNo = "";//���ұ��
            public string specTypeId = "";//�걾����ID
            public string specTypeName = "";//�걾��������
            public string disTypeName = "";//������������
            public int specCountInDept = 0;//����ĳһ�걾���͵�����
            public int useCountInDept = 0;//��һ��������ȡ�õı걾����
            public decimal usePercent = 0.0M;//һ�������б걾���͵İٷֱ�
            public int disSpecCount = 0;//ͬһ�걾���ͣ�ͬһ����ʹ�õ�����
            public bool ChkInOneDept(string strDeptNo, string strSpecType)
            {
                if (strDeptNo != this.deptNo)
                {
                    return false;
                }
                if (strSpecType != this.specTypeId)
                {
                    return false; 
                }
                return true;
            }
        }

        /// <summary>
        /// ��дArralyList�е�Contains����
        /// </summary>
        private class CurrentArrayList : ArrayList
        {
            public override bool Contains(object item)
            {
                foreach (object o in this)
                {
                    UseDeptSpecInfo useUseInfo = o as UseDeptSpecInfo;
                    UseDeptSpecInfo curUseInfo = item as UseDeptSpecInfo;
                    if (useUseInfo.deptNo != curUseInfo.deptNo)
                    {
                        return false;
                    }
                    if (useUseInfo.specTypeId != curUseInfo.specTypeId)
                    {
                        return false;
                    }
                    return true;
                }
                return base.Contains(item);
            }
        }

        private void nspSpecList_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void txtApplyNum_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}
