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
        /// 申请ID号
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
        /// ArrayList 中的 table合并
        /// </summary>
        /// <param name="arrTable"></param>
        /// <returns>合并后的DataTable</returns>
        public DataTable MergeTable(ArrayList arrTable)
        {
            //nspSpecList_Sheet1.BindDataColumn(3, "标本号");
            //nspSpecList_Sheet1.BindDataColumn(4, "标本类型");
            //nspSpecList_Sheet1.BindDataColumn(5, "病种");
            //nspSpecList_Sheet1.BindDataColumn(6, "癌性质");
            //nspSpecList_Sheet1.BindDataColumn(7, "送存科室");
            //nspSpecList_Sheet1.BindDataColumn(8, "癌种类");
            //nspSpecList_Sheet1.BindDataColumn(9, "入库时间");
            //nspSpecList_Sheet1.BindDataColumn(10, "出库次数");
            //nspSpecList_Sheet1.BindDataColumn(11, "库存量");
            //nspSpecList_Sheet1.BindDataColumn(12, "采集阶段");
            //nspSpecList_Sheet1.BindDataColumn(13, "包膜");
            //nspSpecList_Sheet1.BindDataColumn(14, "碎组织");
            DataTable mergedTable = new DataTable();
            #region datatable 列初始化
            DataColumn dt = new DataColumn();
            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "条码";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "病种";
            mergedTable.Columns.Add(dt);

            DataColumn dtSubSpecId = new DataColumn();
            dtSubSpecId.DataType = System.Type.GetType("System.Int32");
            dtSubSpecId.ColumnName = "标本号";
            mergedTable.Columns.Add(dtSubSpecId);

            DataColumn dtSpecType = new DataColumn();
            dtSpecType.DataType = System.Type.GetType("System.String");
            dtSpecType.ColumnName = "标本类型";
            mergedTable.Columns.Add(dtSpecType);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "肿物部位";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "癌种类";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "姓名";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "病历号";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "性别";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "年龄";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "送存科室";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "送存医生";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "送存日期";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "主诊断";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "癌性质";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "出库次数";
            mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "库存量";
            //mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "盒条码";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "行";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "列";
            mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "条形码";
            //mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "容量";
            //mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "侧别";
            //mergedTable.Columns.Add(dt);

            //dt = new DataColumn();
            //dt.DataType = System.Type.GetType("System.String");
            //dt.ColumnName = "送存日期";
            //mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "放疗方案";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "化疗方案";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "入院诊断";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "门诊诊断";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "出院诊断";
            mergedTable.Columns.Add(dt);

            dt = new DataColumn();
            dt.DataType = System.Type.GetType("System.String");
            dt.ColumnName = "在库状态";
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
                    #region 癌性质
                    if (newDr["癌性质"].ToString().Trim() != "")
                    {
                        char[] tumorPor = newDr["癌性质"].ToString().ToCharArray();
                        newDr["癌性质"] = "";
                        Constant.TumorPro TumorPro = Constant.TumorPro.原发癌;
                        foreach (char t in tumorPor)
                        {
                            TumorPro = (Constant.TumorPro)(Convert.ToInt32(t.ToString()));
                            switch (TumorPro)
                            {
                                //标本属性1.原发癌 2.复发癌 3.转移癌
                                case Constant.TumorPro.原发癌:
                                    newDr["癌性质"] += "原发癌";
                                    break;
                                case Constant.TumorPro.复发癌:
                                    newDr["癌性质"] += "复发癌";
                                    break;
                                case Constant.TumorPro.转移癌:
                                    newDr["癌性质"] += "转移癌";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    #region 癌种类
                    if (newDr["癌种类"].ToString().Trim() != "")
                    {
                        //1.肿物 2.子瘤 3.癌旁 4，正茬、切端 5.癌栓 6, 癌(血液), 7 正常(血液)
                        char[] tumorType = newDr["癌种类"].ToString().ToCharArray();
                        newDr["癌种类"] = "";
                        foreach (char t in tumorType)
                        {
                            Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(t.ToString()));
                            switch (TumorType)
                            {
                                case Constant.TumorType.肿物:
                                    newDr["癌种类"] += "肿物";
                                    break;
                                case Constant.TumorType.子瘤:
                                    newDr["癌种类"] += "子瘤";
                                    break;
                                //case Constant.TumorType.正常:
                                //    newDr["癌种类"] += "癌";
                                //    break;
                                case Constant.TumorType.癌旁:
                                    newDr["癌种类"] += "癌旁";
                                    break;
                                case Constant.TumorType.癌栓:
                                    newDr["癌种类"] += "癌栓";
                                    break;
                                case Constant.TumorType.正常:
                                    newDr["癌种类"] += "正常";
                                    break;
                                case Constant.TumorType.淋巴结:
                                    newDr["癌种类"] += "淋巴结";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    //if (newDr["侧别"].ToString().Trim() != "")
                    //{

                    //    switch (newDr["侧别"].ToString())
                    //    {
                    //        case "L":
                    //            newDr["侧别"] = "左侧";
                    //            break;
                    //        case "R":
                    //            newDr["侧别"] = "右侧";
                    //            break;
                    //        case "A":
                    //            newDr["侧别"] = "全部";
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}

                    newDr["送存日期"] = Convert.ToDateTime(newDr["送存日期"].ToString()).ToString("yyyy-MM-dd");

                    if (newDr["在库状态"].ToString().Trim() != "")
                    {

                        switch (newDr["在库状态"].ToString())
                        {
                            case "1":
                                newDr["在库状态"] = "未借出";
                                break;
                            case "3":
                                newDr["在库状态"] = "已归还";
                                break;
                            case "2":
                                newDr["在库状态"] = "未归还";
                                break;
                            default:
                                break;
                        }
                    }
                    newDr["放疗方案"] = newDr["放疗方案"] == null ? "" : newDr["放疗方案"].ToString();
                    newDr["化疗方案"] = newDr["化疗方案"] == null ? "" : newDr["化疗方案"].ToString();
                    newDr["入院诊断"] = newDr["入院诊断"] == null ? "" : newDr["入院诊断"].ToString();
                    newDr["出院诊断"] = newDr["出院诊断"] == null ? "" : newDr["出院诊断"].ToString();
                    newDr["主诊断"] = newDr["主诊断"] == null ? "" : newDr["主诊断"].ToString();
                    newDr["门诊诊断"] = newDr["门诊诊断"] == null ? "" : newDr["门诊诊断"].ToString();
                }            
            
                //nspSpecList_Sheet1.Rows.Count = 0;
                nspSpecList_Sheet1.AutoGenerateColumns = false;
                 
                nspSpecList_Sheet1.DataSource = null;
                nspSpecList_Sheet1.DataSource = dtResult;
                nspSpecList_Sheet1.ColumnCount = 29;
                nspSpecList_Sheet1.BindDataColumn(4, "条码");
                nspSpecList_Sheet1.BindDataColumn(5, "病种");
                nspSpecList_Sheet1.BindDataColumn(6, "标本号");
                nspSpecList_Sheet1.BindDataColumn(7, "标本类型");
                nspSpecList_Sheet1.BindDataColumn(8, "肿物部位");
                nspSpecList_Sheet1.BindDataColumn(9, "癌种类");
                nspSpecList_Sheet1.BindDataColumn(10, "姓名");
                nspSpecList_Sheet1.BindDataColumn(11, "病历号");
                nspSpecList_Sheet1.BindDataColumn(12, "性别");
                nspSpecList_Sheet1.BindDataColumn(13, "年龄");
                nspSpecList_Sheet1.BindDataColumn(14, "送存科室");
                nspSpecList_Sheet1.BindDataColumn(15, "送存医生");
                nspSpecList_Sheet1.BindDataColumn(16, "送存日期");
                nspSpecList_Sheet1.BindDataColumn(17, "主诊断");

                nspSpecList_Sheet1.BindDataColumn(18, "癌性质");
                nspSpecList_Sheet1.BindDataColumn(19, "出库次数");
                nspSpecList_Sheet1.BindDataColumn(20, "盒条码");
                nspSpecList_Sheet1.BindDataColumn(21, "行");
                nspSpecList_Sheet1.BindDataColumn(22, "列");   
                nspSpecList_Sheet1.BindDataColumn(23, "放疗方案");
                nspSpecList_Sheet1.BindDataColumn(24, "化疗方案");
                nspSpecList_Sheet1.BindDataColumn(25, "入院诊断");
                nspSpecList_Sheet1.BindDataColumn(26, "门诊诊断");
                nspSpecList_Sheet1.BindDataColumn(27, "出院诊断");
                nspSpecList_Sheet1.BindDataColumn(28, "在库状态");
                #region 不显示列哦 
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
                    if (nspSpecList_Sheet1.Cells[index, 22].Value.ToString().Contains("未归还"))
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
        /// 查询已审批的出库标本列表
        /// </summary>
        /// <param name="spec"></param>
        public void QueryImpedOutSpec(string spec)
        {
            string sql = " SELECT DISTINCT SPEC_SUBSPEC.SUBBARCODE 条码,SPEC_DISEASETYPE.DISEASENAME 病种,SPEC_SOURCE.SPEC_NO 标本号,  SPEC_TYPE.SPECIMENTNAME 标本类型, \n" +
                       " SPEC_SOURCE_STORE.TUMORPOS 肿物部位,SPEC_SOURCE_STORE.TUMORTYPE 癌种类,SPEC_PATIENT.NAME 姓名,SPEC_PATIENT.CARD_NO 病历号,SPEC_PATIENT.GENDER 性别, \n" +
                       " SPEC_PATIENT.BIRTHDAY 年龄,COM_DEPARTMENT.DEPT_NAME 送存科室,COM_EMPLOYEE.EMPL_NAME 送存医生, SPEC_SOURCE.SENDDATE 送存日期,MAIN_DIANAME 主诊断,  \n" +
                       " SPEC_SOURCE.TUMORPOR 癌性质,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) 出库次数,\n" +
                       " SPEC_BOX.BOXBARCODE 盒条码,SPEC_SUBSPEC.BOXENDROW 行,SPEC_SUBSPEC.BOXENDCOL 列,\n" +
                       " SPEC_SOURCE.RADSCHEME 放疗方案, SPEC_SOURCE.MEDSCHEME 化疗方案, \n" +
                       " INHOS_DIANAME 入院诊断, CLINIC_DIANAME 门诊诊断, M_DIAGICDNAME 出院诊断,SPEC_SUBSPEC.STATUS 在库状态 \n" +
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
        /// 查询申请表对应的申请要求
        /// </summary>
        /// <param name="applyNum">申请表ID</param>
        public void QuerySpecListByApplyNum(string applyNum)
        {
            curApplyNum = applyNum;
            try
            {
                List<string> sqlList = new List<string>();
                //根据申请ID查询申请表
                currentTable = applyTableManage.QueryApplyByID(applyNum);
                if (currentTable.ApplyId > 0)
                {
                    #region sql语句
                    /*strSql = " SELECT DISTINCT SPEC_SUBSPEC.SUBSPECID 标本号,  SPEC_TYPE.SPECIMENTNAME 标本类型,COM_DEPARTMENT.DEPT_NAME 送存科室,\n" +
                       " SPEC_DISEASETYPE.DISEASENAME 病种,SPEC_SOURCE.TUMORPOR 癌性质, SPEC_SOURCE_STORE.TUMORTYPE 癌种类,\n" +
                       " SPEC_SOURCE_STORE.STORETIME 入库时间,\n" +
                       " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) 出库次数,\n" +
                       " SPEC_BOX.BOXBARCODE 盒条码,\n" +
                       " SPEC_SUBSPEC.BOXENDROW 行,SPEC_SUBSPEC.BOXENDCOL 列,\n" +
                       " SPEC_SOURCE_STORE.TUMORPOS 肿物部位, SPEC_SOURCE.RADSCHEME 放疗方案, SPEC_SOURCE.MEDSCHEME 化疗方案, \n" +
                       " MAIN_DIANAME 主诊断, INHOS_DIANAME 入院诊断, CLINIC_DIANAME 门诊诊断, M_DIAGICDNAME 出院诊断,SPEC_SUBSPEC.STATUS 在库状态 ,SPEC_SUBSPEC.SUBBARCODE 条码\n" +
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
                    //根据申请要求查询出符合条件的标本
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
                    #region 获取sql语句及Dataset
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

                    #region FarPoint 数据绑定
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
        /// 明细的查询条件
        /// </summary>
        /// <returns></returns>
        private string GetSql()
        {
            #region
            string sql = " SELECT DISTINCT SPEC_SUBSPEC.SUBBARCODE 条码,SPEC_DISEASETYPE.DISEASENAME 病种,SPEC_SOURCE.SPEC_NO 标本号,  SPEC_TYPE.SPECIMENTNAME 标本类型, \n" +
                       " SPEC_SOURCE_STORE.TUMORPOS 肿物部位,SPEC_SOURCE_STORE.TUMORTYPE 癌种类,SPEC_PATIENT.NAME 姓名,SPEC_PATIENT.CARD_NO 病历号,SPEC_PATIENT.GENDER 性别, \n" +
                       " SPEC_PATIENT.BIRTHDAY 年龄,COM_DEPARTMENT.DEPT_NAME 送存科室,COM_EMPLOYEE.EMPL_NAME 送存医生, SPEC_SOURCE.SENDDATE 送存日期,MAIN_DIANAME 主诊断,  \n" +
                       " SPEC_SOURCE.TUMORPOR 癌性质,(SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) 出库次数,\n" +
                       " SPEC_BOX.BOXBARCODE 盒条码,SPEC_SUBSPEC.BOXENDROW 行,SPEC_SUBSPEC.BOXENDCOL 列,\n" +
                       " SPEC_SOURCE.RADSCHEME 放疗方案, SPEC_SOURCE.MEDSCHEME 化疗方案, \n" +
                       " INHOS_DIANAME 入院诊断, CLINIC_DIANAME 门诊诊断, M_DIAGICDNAME 出院诊断,SPEC_SUBSPEC.STATUS 在库状态 \n" +
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
        /// 更新审批表
        /// </summary>    
        /// <param name="outInfoList">预出库的标本列表List</param>
        /// <param name="listSubSpecId"></param>
        /// <returns></returns>
        private int UpdateApplyTable(List<OutInfo> outInfoList)
        {
            string specList = "";
            string isReturnList = "";
           
            #region  出库的ID列表
            foreach (OutInfo info in outInfoList)
            {
                //出库的标本ID列表    
                specList += info.SpecBarCode;
                specList += ",";
                //标本可还列表,与标本ID一一对应
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
        /// 点击该行任意一个地方，显示标本详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nspSpecList_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            //int rowIndex = nspSpecList_Sheet1.ActiveRowIndex;
            //if (rowIndex >= 0&&nspSpecList_Sheet1.Rows.Count>0)
            //{
            //    string applyNum = nspSpecList_Sheet1.Cells[rowIndex, 4].Value.ToString();
            //    string sql = "SELECT DISTINCT SPEC_SUBSPEC.SUBBARCODE 条形码,  SPEC_SUBSPEC.SPECCAP 容量,SPEC_SOURCE_STORE.TUMORPOS 肿物部位,ORGNAME 标本组织类型," +
            //                 "SPEC_SOURCE_STORE.SIDEFROM 侧别, SPEC_SOURCE.SENDDATE,SPEC_SOURCE.RADSCHEME 放疗方案, SPEC_SOURCE.MEDSCHEME 化疗方案," +
            //                 "INHOS_DIANAME 入院诊断, CLINIC_DIANAME 门诊诊断,MAIN_DIANAME 主诊断,M_DIAGICDNAME 出院诊断" +
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
            //                txtBarCode.Text = dr["条形码"].ToString();
            //                txtCap.Text = dr["容量"].ToString();
            //                txtTumorPos.Text = dr["肿物部位"].ToString();
            //                txtOrgName.Text = dr["标本组织类型"].ToString();
            //                switch (dr["侧别"].ToString())
            //                {
            //                    case "L":
            //                        txtSideFrom.Text = "左侧";
            //                        break;
            //                    case "R":
            //                        txtSideFrom.Text = "右侧";
            //                        break;
            //                    case "A":
            //                        txtSideFrom.Text = "全部";
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                txtRadSch.Text = dr["放疗方案"] == null ? "" : dr["放疗方案"].ToString();
            //                txtMedSch.Text = dr["化疗方案"] == null ? "" : dr["化疗方案"].ToString();
            //                txtInDia.Text = dr["入院诊断"] == null ? "" : dr["入院诊断"].ToString();
            //                txtOutDia.Text = dr["出院诊断"] == null ? "" : dr["出院诊断"].ToString();
            //                txtMainDia.Text = dr["主诊断"] == null ? "" : dr["主诊断"].ToString();
            //                txtCliDia.Text = dr["门诊诊断"] == null ? "" : dr["门诊诊断"].ToString();
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
        /// 全选
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
                    if (r.Visible && !nspSpecList_Sheet1.Cells[i, 22].Value.ToString().Contains("未归还"))
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
        /// 出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOut_Click(object sender, EventArgs e)
        {
            if (txtApplyNum.Text.Trim() == "" && curApplyNum=="")
            {
                MessageBox.Show("请输入申请单号");
                return;
            }
            #region 出库说明
            //事件说明：获取选中标本，并出库
            //基本信息的获取：
            //1. 将选中的标本加入List中
            //2. 标本是否可还，如果可还，设置还标本日期
            //更新信息
            //1. 更新每一标本的 Status字段（在库的状态），标本的位置，Instore状态信息，约定的返还时间
            //2. 更新申请表的信息：标本出库ID列表，IsImmedBackList，SpecCountInDept，PerInAll，LeftAmout
            //3. 更新出库信息表
            //4.更新表 Spec_Source_Store SotreCount 
            #endregion
            List<SubSpec> outSpecNum = new List<SubSpec>();
            List<OutInfo> outInfoList = new List<OutInfo>();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            try
            {
                SubSpecManage tmp = new SubSpecManage();
                tmp.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                applyTableManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); 
               
                //将选中的标本加入List中
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
                        //标本可还列表
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
                            MessageBox.Show("申请单号不存在！", " 出库");
                            return;
                        }
                        //specOut = new SpecOutOper(currentTable, curApplyNum, loginPerson);
                        
                    }
                    catch
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("申请编号必须为数字！", " 出库");
                        return;
                    }
                }
                specOut = new SpecOutOper(currentTable, loginPerson);
                if (UpdateApplyTable(outInfoList) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新申请单失败！", "标本出库");
                    return;
                }
                //预出库，只需更新申请单 无需更新库存
                //specOut.UpdateApplyTable(outInfoList, trans);
                //没有预出库的代码
                //specOut.SpecOut(outInfoList, trans);
                try
                {
                    specOut.PrintTitle = "标本出库清单";
                    specOut.PrintOutSpec(outSpecNum, FS.FrameWork.Management.PublicTrans.Trans);
                }
                catch
                {                   
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("打印失败", "标本出库");
                    return;
                }
                QuerySpecListByApplyNum(curApplyNum);                
                //重新邦定数据
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
                MessageBox.Show("出库成功!", "标本出库");
            }
            catch (Exception ex)
            {
               string message = ex.Message;
               FS.FrameWork.Management.PublicTrans.RollBack();
               MessageBox.Show(ex.Message, "标本出库");
            }
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "查询结果导出，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "标本";
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
                chkBack.Text = "归还";
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    if (!nspSpecList_Sheet1.Cells[i, 22].Value.ToString().Contains("未归还"))
                        nspSpecList_Sheet1.Cells[i, 1].Value = (object)1;
                }
            }
            if (chkBack.CheckState == CheckState.Unchecked)
            {
                chkBack.Text = "不归还";
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    nspSpecList_Sheet1.Cells[i, 1].Value = (object)0;
                }
            }

            if (chkBack.CheckState == CheckState.Indeterminate)
            {
                chkBack.Text = "多次出库";
                for (int i = 0; i < nspSpecList_Sheet1.Rows.Count; i++)
                {
                    if (!nspSpecList_Sheet1.Cells[i, 22].Value.ToString().Contains("未归还"))
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
                    if(orgName.Contains("血"))
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
                    if (orgName.Contains("组织"))
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
            public string deptNo = "";//科室编号
            public string specTypeId = "";//标本类型ID
            public string specTypeName = "";//标本类型名称
            public string disTypeName = "";//病种类型名称
            public int specCountInDept = 0;//科室某一标本类型的总数
            public int useCountInDept = 0;//在一个科室中取用的标本数量
            public decimal usePercent = 0.0M;//一个科室中标本类型的百分比
            public int disSpecCount = 0;//同一标本类型，同一病种使用的数量
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
        /// 重写ArralyList中的Contains方法
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
