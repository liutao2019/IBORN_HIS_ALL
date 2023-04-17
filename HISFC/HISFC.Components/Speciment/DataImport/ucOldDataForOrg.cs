using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.DataImport
{
    public partial class ucOldDataForOrg : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        SpecSourcePlanManage planMgr = new SpecSourcePlanManage();
        SubSpecManage subMgr = new SubSpecManage();
        FS.HISFC.BizLogic.Speciment.SpecSourceManage spMgr = new FS.HISFC.BizLogic.Speciment.SpecSourceManage();

        private int rowCount = 0;

        public ucOldDataForOrg()
        {
            InitializeComponent();
        }

        public int GenerateInfo(string specTypeName, int specType, int count, string from, DataRow spDr, ref SpecSourcePlan spDofT, SpecSource spSource, string type)
        {
            #region T : D

            try
            {
                spDofT.Count = count;
            }
            catch
            {
                spDofT.Count = 0;
            }

            if (spDofT.Count == 0)
            {
                return 0;
            }

            if (spDofT.Count > 0)
            {
                spDofT.BaoMoEntire = from;
                spDofT.TumorPos = from;
                spDofT.Count = count;
                spDofT.IsHis = '1';
                spDofT.SideFrom = "";
                spDofT.SpecID = spSource.SpecId;
                spDofT.SpecType.SpecTypeID = specType;
                spDofT.StoreTime = spSource.SendTime;

                switch (type)
                {
                    case "T":     
                        spDofT.TumorType = "1";
                        break;
                    case "P":
                        spDofT.TumorType = "3";
                        break;
                    case "L":
                        spDofT.TumorType = "8";
                        break;
                    case "N":
                        spDofT.TumorType = "4";
                        break;
                    case "S":
                        spDofT.TumorType = "2";
                        break;
                    case "E":
                        spDofT.TumorType = "5";
                        break;
                    default:
                        spDofT.TumorType = "1";
                        break;
                }

                spDofT.TumorPor = "1";
                spDofT.Unit = "支";
                string comment = "初始送存信息: DNA  T: ";
                comment += spDr["SENTT"] == null ? "" : spDr["SENTT"].ToString();

                comment += "   子瘤:";
                comment += spDr["SENTS"] == null ? "" : spDr["SENTS"].ToString();

                comment += "   P:";
                comment += spDr["SENTP"] == null ? "" : spDr["SENTP"].ToString();
                comment += "   N:";
                comment += spDr["SENTN"] == null ? "" : spDr["SENTN"].ToString();

                comment += "   癌栓:";
                comment += spDr["SENTE"] == null ? "" : spDr["SENTE"].ToString();

                comment += "   淋巴结:";
                comment += spDr["SENTL"] == null ? "" : spDr["SENTL"].ToString();
                spDofT.Comment = comment;

                string spDofTPlanId = planMgr.GetNextSequence();

                spDofT.PlanID = Convert.ToInt32(spDofTPlanId);

                if (Convert.ToInt32(spSource.SpecNo) == 78)
                {
                    string ss = "";
                }

                //如果是石蜡块

                if (specType == 7)
                {
                    #region 石蜡块
                    string subSql = @"select *
                        from SPEC_OLDSPECFORORG od
                        where od.SPECTYPEID = '4' and od.SPECPOR = '{0}' and od.GETGROM = '{1}' and od.SPECNO = '{2}'
                        and DISTYPEID='{3}' fetch first 1 rows only";

                    string spDofTSql = string.Format(subSql, type, spDofT.TumorPos, Convert.ToInt32(spSource.SpecNo), spSource.DiseaseType.DisTypeID);

                    DataSet dsspDofT = new DataSet();

                    spMgr.ExecQuery(spDofTSql, ref dsspDofT);

                    string disAbbTmp = string.Empty;
                    string disGetTmp = string.Empty;

                    if (dsspDofT != null && dsspDofT.Tables[0].Rows.Count > 0)
                    {
                        DataRow drDofT = dsspDofT.Tables[0].Rows[0];
                        if (drDofT != null)
                        {
                            disAbbTmp = drDofT["DISABB"].ToString();
                            disGetTmp = drDofT["GETGROMABB"].ToString();
                        }
                    }
                    else
                    {
                        subSql = @"select * from SPEC_ORGLISTIMP oi
                        where oi.DISTYPEID='{0}'";
                        spDofTSql = string.Format(subSql, spSource.DiseaseType.DisTypeID);

                        spMgr.ExecQuery(spDofTSql, ref dsspDofT);
                        if (dsspDofT != null && dsspDofT.Tables[0].Rows.Count > 0)
                        {
                            DataRow drDoffT = dsspDofT.Tables[0].Rows[0];
                            if (drDoffT != null)
                            {
                                disAbbTmp = drDoffT["DISABB"].ToString();
                                disGetTmp = drDoffT["GETGROMABB"].ToString();
                            }
                        }
                    }
                    
                    if (Convert.ToInt32(spSource.SpecNo) == 78)
                    {
                        string ss = "";
                    }
                    for (int i = 0; i < spDofT.Count; i++)
                    {
                        SubSpec sub = new SubSpec();

                        sub.BoxEndCol = 1;
                        sub.BoxEndRow = 1;
                        sub.BoxId = 8874;
                        sub.BoxStartCol = 1;
                        sub.BoxStartRow = 1;
                        sub.Status = "1";

                        sub.IsReturnable = '1';

                        sub.SpecCount = 1;
                        sub.SpecId = spSource.SpecId;
                        sub.SpecTypeId = specType;

                        sub.StoreID = spDofT.PlanID;
                        sub.StoreTime = spDofT.StoreTime;
                        //sub.SubBarCode = spSource.SpecNo.PadLeft(6, '0')
                        //    + "B4"
                        //    + "BD"
                        //    + type + specTypeName + (i+1).ToString();
                        sub.SubBarCode = spSource.SpecNo.PadLeft(6, '0')
                            + disAbbTmp
                            + disGetTmp
                            + type + specTypeName + i.ToString();
                        string subDofT = "";
                        subMgr.GetNextSequence(ref subDofT);
                        sub.SubSpecId = Convert.ToInt32(subDofT) + 398;

                        if (subMgr.InsertSubSpec(sub) <= 0)
                            return -1;

                        string tmpSubUpdate = "update SPEC_SUBSPEC set HISTORYOUT = '{0}' where SUBSPECID = {1}";

                        string historyOut = "";// spDr["DNAOUTTIMES"] == null ? "" : spDr["DNAOUTTIMES"].ToString();

                        switch (specType)
                        {
                            case 4:
                                historyOut = spDr["DNAOUTTIMES"] == null ? "" : spDr["DNAOUTTIMES"].ToString();
                                break;
                            case 5:
                                historyOut = spDr["RNAOUTTIMES"] == null ? "" : spDr["DNAOUTTIMES"].ToString();
                                break;
                            case 7:
                                historyOut = spDr["POUTTIMES"] == null ? "" : spDr["POUTTIMES"].ToString();
                                historyOut += " * ";
                                historyOut += spDr["BPOUTTIME"] == null ? "" : spDr["BPOUTTIME"].ToString();
                                break;
                            default:
                                break;
                        }

                        tmpSubUpdate = string.Format(tmpSubUpdate, historyOut, sub.SubSpecId.ToString());

                        if (subMgr.ExecNoQuery(tmpSubUpdate) < 0)
                            return -1;
                    }

                    spDofT.StoreCount = spDofT.Count;
                    if (planMgr.InsertSourcePlan(spDofT) <= 0)
                    {
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    #region DNA\RNA
                    /*string subSql = @"select *
                        from SPEC_OLDLOCFORORG od
                        where od.SPECTYPEID = '{0}' and od.SPECPOR = '{1}' and od.GETGROM = '{2}' and od.SPECNO = '{3}'
                        and DISTYPEID='{4}'";*/
                    /*string subSql = @"select *
                        from SPEC_OLDSPECFORORG od
                        where od.SPECTYPEID = '{0}' and od.SPECPOR = '{1}' and od.GETGROM = '{2}' and od.SPECNO = '{3}'
                        and DISTYPEID='{4}'";*/

//                    string subSql = @"select *
//                        from SPEC_OLDSPECFORORG_UNIMP od
//                        where od.SPECTYPEID = '{0}' and od.SPECPOR = '{1}' and od.GETGROM = '{2}' and od.SPECNO = '{3}'
//                        and DISTYPEID='{4}'";

//                    string spDofTSql = string.Format(subSql, specType, type, spDofT.TumorPos, Convert.ToInt32(spSource.SpecNo),spSource.DiseaseType.DisTypeID);

//                    DataSet dsspDofT = new DataSet();

//                    spMgr.ExecQuery(spDofTSql, ref dsspDofT);

//                    int i = 1;

//                    foreach (DataRow drDofT in dsspDofT.Tables[0].Rows)
//                    {
//                        SubSpec sub = new SubSpec();
//                        string isOut = drDofT["ISOUT"] == null ? "" : drDofT["ISOUT"].ToString();

//                        if (isOut == "1")
//                        {
//                            sub.BoxEndCol = 0;
//                            sub.BoxEndRow = 0;
//                            sub.BoxId = 0;
//                            sub.BoxStartCol = 0;
//                            sub.BoxStartRow = 0;
//                            sub.Status = "4";
//                        }

//                        else
//                        {
//                            sub.BoxEndCol = Convert.ToInt32(drDofT["COL"]);
//                            sub.BoxEndRow = Convert.ToInt32(drDofT["ROW"]);
//                            sub.BoxId = Convert.ToInt32(drDofT["BOXID"]);
//                            sub.BoxStartCol = Convert.ToInt32(drDofT["COL"]);
//                            sub.BoxStartRow = Convert.ToInt32(drDofT["ROW"]);
//                            sub.Status = "1";
//                        }
//                        sub.IsReturnable = '1';

//                        sub.SpecCount = 1;
//                        sub.SpecId = spSource.SpecId;
//                        sub.SpecTypeId = specType;

//                        sub.StoreID = spDofT.PlanID;
//                        sub.StoreTime = spDofT.StoreTime;
//                        sub.SubBarCode = drDofT["SPECNO"].ToString().PadLeft(6, '0')
//                            + drDofT["DISABB"].ToString()
//                            + drDofT["GETGROMABB"].ToString()
//                            + type + specTypeName + i.ToString();

//                        string isExists = "select SUBBARCODE,SUBSPECID from SPEC_SUBSPEC where SUBBARCODE = '{0}'";
//                        isExists = string.Format(isExists, sub.SubBarCode);
//                        DataSet dsExists = new DataSet();
//                        spMgr.ExecQuery(isExists, ref dsExists);
//                        if (dsExists != null)
//                        {
//                            if (dsExists.Tables[0].Rows.Count > 0)
//                            {
//                                DataRow drExists = dsExists.Tables[0].Rows[0];
//                                if (drExists["SUBBARCODE"] != null)
//                                {
//                                    if (!string.IsNullOrEmpty(drExists["SUBBARCODE"].ToString()))
//                                    {
//                                        continue;
//                                    }
//                                }
//                            }
//                        }

//                        string subDofT = "";
//                        subMgr.GetNextSequence(ref subDofT);
//                        sub.SubSpecId = Convert.ToInt32(subDofT) + 398;
//                        sub.BoxId = Convert.ToInt32(drDofT["BOXID"]);
//                        if (subMgr.InsertSubSpec(sub) <= 0)
//                            return -1;

//                        string tmpSubUpdate = "update SPEC_SUBSPEC set HISTORYOUT = '{0}' where SUBSPECID = {1}";

//                        string historyOut = "";// spDr["DNAOUTTIMES"] == null ? "" : spDr["DNAOUTTIMES"].ToString();

//                        switch (specType)
//                        {
//                            case 4:
//                                historyOut = spDr["DNAOUTTIMES"] == null ? "" : spDr["DNAOUTTIMES"].ToString();
//                                break;
//                            case 5:
//                                historyOut = spDr["RNAOUTTIMES"] == null ? "" : spDr["DNAOUTTIMES"].ToString();
//                                break;
//                            case 7:
//                                historyOut = spDr["POUTTIMES"] == null ? "" : spDr["POUTTIMES"].ToString();
//                                historyOut += " * ";
//                                historyOut += spDr["BPOUTTIME"] == null ? "" : spDr["BPOUTTIME"].ToString();
//                                break;
//                            default:
//                                break;
//                        }

//                        tmpSubUpdate = string.Format(tmpSubUpdate, historyOut, sub.SubSpecId.ToString());

//                        if (subMgr.ExecNoQuery(tmpSubUpdate) < 0)
//                            return -1;
//                        i++;
//                    }

//                    spDofT.StoreCount = --i;
//                    if (planMgr.InsertSourcePlan(spDofT) <= 0)
//                    {
//                        return -1;
//                    }
                    #endregion
                }
                string tmpStoreUpdate = " update SPEC_SOURCE_STORE set COMMENT1 = '{0}' where SOTREID = {1}";

                string outComment = "";
                
                switch(specType)
                {
                    case 4:
                        outComment = spDr["DNAOUT"] == null ? "" : spDr["DNAOUT"].ToString();
                        break;
                    case 5:
                        outComment = spDr["RNAOUT"] == null ? "" : spDr["DNAOUT"].ToString();
                        break;
                    case 7:
                        outComment = spDr["OUTSKL"] == null ? "" : spDr["OUTSKL"].ToString();
                        break;
                    default :
                        break;
                }

                tmpStoreUpdate = string.Format(tmpStoreUpdate, outComment, spDofT.PlanID.ToString());
                if (planMgr.ExecNoQuery(tmpStoreUpdate) < 0)
                    return -1;

                rowCount += 1;

            }

            return 1;
            #endregion
        }

        
        private void GetSource(string disType, string specNo, int disTypeId)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            subMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            spMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            planMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                DataSet ds = new DataSet();
                string sql = @" select * from SPEC_OLDORGDATA o where o.SPECNO like '"+disType+"%' and o.SEQ = " + specNo;


                spMgr.ExecQuery(sql, ref ds);

                /* string locSql = @"
                         select distinct lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID, count(*) cnt
                         from SPEC_OLDLOCFORORG lc
                         where lc.SPECNO ='{0}' and DISTYPEID='{1}'
                         group by SPECNO, SPECPOR, GETGROM, lc.SPECTYPEID";*/
                string locSql = @"select distinct lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID, count(*) cnt
                                 from SPEC_OLDSPECFORORG lc
                                 where lc.SPECNO ='{0}' and DISTYPEID='{1}'
                                 group by SPECNO, SPECPOR, GETGROM, lc.SPECTYPEID";


                /*string locSql = @"select distinct lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID, count(*) cnt
                                 from SPEC_OLDSPECFORORG_UNIMP lc
                                 where lc.SPECNO ='{0}' and DISTYPEID='{1}'
                                 group by SPECNO, SPECPOR, GETGROM, lc.SPECTYPEID";*/

                DataSet dsPlan = new DataSet();
                locSql = string.Format(locSql, specNo, disTypeId);

                spMgr.ExecQuery(locSql, ref dsPlan);
                dsPlan.Tables[0].PrimaryKey = new DataColumn[] { dsPlan.Tables[0].Columns[0], dsPlan.Tables[0].Columns[1], dsPlan.Tables[0].Columns[2], dsPlan.Tables[0].Columns[3] };

                DataRow spDr = ds.Tables[0].Rows[0];
                //DataRow locDr = dsPlan.Tables[0].Rows[0];

                SpecSource spSource = new SpecSource();
                
                spSource.OrgOrBoold = "O";
                spSource.IsHis = '0';
                spSource.DiseaseType.DisTypeID = disTypeId;
                spSource.SendDoctor.ID = spDr["SENDDOC"] == null ? "" : spDr["SENDDOC"].ToString();
                spSource.OperTime = spDr["SENDDATE"] == null ? DateTime.MinValue : Convert.ToDateTime(spDr["SENDDATE"]);
                spSource.OperEmp.Name = spDr["OPER"] == null ? "" : spDr["OPER"].ToString();
                spSource.SendTime = spDr["SENDDATE"] == null ? DateTime.MinValue : Convert.ToDateTime(spDr["SENDDATE"]);
                spSource.InPatientNo = spDr["INPATIENT_NO"] == null ? "" : spDr["INPATIENT_NO"].ToString();
                spSource.DeptNo = spDr["DEPT"] == null ? "" : spDr["DEPT"].ToString();

                if (spDr["EXT3"] == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("无关联患者!");
                    return;
                }
                spSource.Patient.PatientID = Convert.ToInt32(spDr["EXT3"]);
                string spComment = "诊断:";
                spComment += spDr["DIAGNOSE"] == null ? "" : spDr["DIAGNOSE"].ToString();

                spComment += " 送存信息: RNA:";

                spComment += spDr["RNA"] == null ? "" : spDr["RNA"].ToString();

                spComment += " DNA:";

                spComment += spDr["DNA"] == null ? "" : spDr["DNA"].ToString();

                spComment += " 石蜡块:";

                spComment += spDr["SLK"] == null ? "" : spDr["SLK"].ToString();

                spComment += " 初始备注: ";

                spComment += spDr["COMMENT"] == null ? "" : spDr["COMMENT"].ToString();

                spSource.Commet = spComment;

                if (specNo == "77")
                {
 
                }

                spSource.IsInBase = '0';
                //spSource.MatchFlag,
                spSource.SpecNo = specNo.PadLeft(6, '0');


                #region 增加判断，是否存在标本源 暂时考虑3字段：病人ID、病种、标本号，有不增加只取标本ID，没有插入
                string sqlTmpl = "select SPECID from spec_source where PATIENTID = {0} and SPEC_NO = '{1}' and DISEASETYPEID = {2}";
                sqlTmpl = string.Format(sqlTmpl,spSource.Patient.PatientID, spSource.SpecNo, disTypeId);

                DataSet dsSource = new DataSet();
                spMgr.ExecQuery(sqlTmpl, ref dsSource);
                //是否插入标本源信息
                bool ifInsert = true;

                if (dsSource != null)
                {
                    if (dsSource.Tables[0].Rows.Count > 0)
                    {
                        DataRow drSource = dsSource.Tables[0].Rows[0];
                        if ((drSource != null) && (drSource["SPECID"] != null))
                        {
                            if (!string.IsNullOrEmpty(drSource["SPECID"].ToString()))
                            {
                                spSource.SpecId = Convert.ToInt32(drSource["SPECID"].ToString());
                                ifInsert = false;
                            }
                        }
                    }
                }
                #endregion

                if (ifInsert)
                {
                    //这样不浪费序列号
                    string spId = "";
                    spMgr.GetNextSequence(ref spId);
                    spSource.SpecId = Convert.ToInt32(spId);

                    if (spMgr.InsertSpecSource(spSource) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                }
                List<string> fromList = new List<string>();

                foreach (DataRow tmpDr in dsPlan.Tables[0].Rows)
                {
                    if (fromList.Contains(tmpDr["GETGROM"].ToString()))
                    {
                        continue;
                    }
                    fromList.Add(tmpDr["GETGROM"].ToString());
                }
 
                SpecSourcePlan spDofT = new SpecSourcePlan();
                SpecSourcePlan spRofT = new SpecSourcePlan();
                SpecSourcePlan spPofT = new SpecSourcePlan();

                SpecSourcePlan spDofP = new SpecSourcePlan();
                SpecSourcePlan spRofP = new SpecSourcePlan();
                SpecSourcePlan spPofP = new SpecSourcePlan();

                SpecSourcePlan spDofL = new SpecSourcePlan();
                SpecSourcePlan spRofL = new SpecSourcePlan();
                SpecSourcePlan spPofL = new SpecSourcePlan();

                SpecSourcePlan spDofN = new SpecSourcePlan();
                SpecSourcePlan spRofN = new SpecSourcePlan();
                SpecSourcePlan spPofN = new SpecSourcePlan();

                SpecSourcePlan spDofS = new SpecSourcePlan();
                SpecSourcePlan spRofS = new SpecSourcePlan();
                SpecSourcePlan spPofS = new SpecSourcePlan();


                SpecSourcePlan spDofE = new SpecSourcePlan();
                SpecSourcePlan spRofE = new SpecSourcePlan();
                SpecSourcePlan spPofE = new SpecSourcePlan();
                #region T : D

                int posCount = fromList.Count;

                int index = 1;

                foreach (string tmpFrom in fromList)
                {
                    int currentCnt = 0;

                    #region 肿物
                    // lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID
                    DataRow drspDofT = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "T", tmpFrom, "4" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["DTIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspDofT["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if (this.GenerateInfo("D", 4, currentCnt, tmpFrom, spDr, ref spDofT, spSource, "T") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    spRofT = new SpecSourcePlan();
                    DataRow drspRofT = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "T", tmpFrom, "5" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["RTIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspRofT["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if (this.GenerateInfo("R", 5, currentCnt, tmpFrom, spDr, ref spRofT, spSource, "T") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }


                    spPofT = new SpecSourcePlan();

                    DataRow drspPofT = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "T", tmpFrom, "7" });
                    if (index == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["PTIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        currentCnt = Convert.ToInt32(drspPofT["CNT"]);
                    //    }
                    //    catch
                    //    { currentCnt = 0; }
                    //}
                    if (this.GenerateInfo("P", 7, currentCnt, tmpFrom, spDr, ref spPofT, spSource, "T") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    #endregion

                    #region 癌旁
                    spDofP = new SpecSourcePlan();

                    // lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID
                    DataRow drspDofP = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "P", tmpFrom, "4" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["DPIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspDofP["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if (this.GenerateInfo("D", 4, currentCnt, tmpFrom, spDr, ref spDofP, spSource, "P") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    spRofP = new SpecSourcePlan();

                    DataRow drspRofP = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "P", tmpFrom, "5" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["RPIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspRofP["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if(this.GenerateInfo("R", 5, currentCnt, tmpFrom, spDr, ref spRofP, spSource, "P")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }


                    spPofP = new SpecSourcePlan();

                    DataRow drspPofP = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "P", tmpFrom, "7" });
                    if (index == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["PPIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        currentCnt = Convert.ToInt32(drspPofP["CNT"]);
                    //    }
                    //    catch
                    //    { currentCnt = 0; }
                    //}
                    if(this.GenerateInfo("P", 7, currentCnt, tmpFrom, spDr, ref spPofP, spSource, "P")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    #endregion

                    #region 淋巴结
                    spDofL = new SpecSourcePlan();

                    // lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID
                    DataRow drspDofL = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "L", tmpFrom, "4" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["DLIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspDofL["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if (this.GenerateInfo("D", 4, currentCnt, tmpFrom, spDr, ref spDofL, spSource, "L") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    spRofL = new SpecSourcePlan();

                    DataRow drspRofL = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "L", tmpFrom, "5" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["RLIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspRofL["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if(this.GenerateInfo("R", 5, currentCnt, tmpFrom, spDr, ref spRofL, spSource, "L")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }


                    spPofL = new SpecSourcePlan();

                    DataRow drspPofL = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "L", tmpFrom, "7" });
                    if (index == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["PLIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        currentCnt = Convert.ToInt32(drspPofL["CNT"]);
                    //    }
                    //    catch
                    //    { currentCnt = 0; }
                    //}
                   if( this.GenerateInfo("P", 7, currentCnt, tmpFrom, spDr, ref spPofL, spSource, "L")==-1)
                   {
                       FS.FrameWork.Management.PublicTrans.RollBack();
                       return;
                   }
                    #endregion

                    #region 正常
                    spDofN = new SpecSourcePlan();

                    // lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID
                    DataRow drspDofN = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "N", tmpFrom, "4" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["DNIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspDofN["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if(this.GenerateInfo("D", 4, currentCnt, tmpFrom, spDr, ref spDofN, spSource, "N")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    spRofN = new SpecSourcePlan();

                    DataRow drspRofN = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "N", tmpFrom, "5" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["RNIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspRofN["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if(this.GenerateInfo("R", 5, currentCnt, tmpFrom, spDr, ref spRofN, spSource, "N")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }


                    spPofN = new SpecSourcePlan();

                    DataRow drspPofN = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "N", tmpFrom, "7" });
                    if (index == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["PNIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        currentCnt = Convert.ToInt32(drspPofN["CNT"]);
                    //    }
                    //    catch
                    //    { currentCnt = 0; }
                    //}
                    if(this.GenerateInfo("P", 7, currentCnt, tmpFrom, spDr, ref spPofN, spSource, "N")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    #endregion

                    #region 子瘤
                    spDofS = new SpecSourcePlan();

                    // lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID
                    DataRow drspDofS = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "S", tmpFrom, "4" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["DSIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspDofS["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if(this.GenerateInfo("D", 4, currentCnt, tmpFrom, spDr, ref spDofS, spSource, "S")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    spRofS = new SpecSourcePlan();

                    DataRow drspRofS = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "S", tmpFrom, "5" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["RSIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspRofS["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if(this.GenerateInfo("R", 5, currentCnt, tmpFrom, spDr, ref spRofS, spSource, "S")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }


                    spPofS = new SpecSourcePlan();

                    DataRow drspPofS = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "S", tmpFrom, "7" });
                    if (index == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["PSIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        currentCnt = Convert.ToInt32(drspPofS["CNT"]);
                    //    }
                    //    catch
                    //    { currentCnt = 0; }
                    //}
                    if(this.GenerateInfo("P", 7, currentCnt, tmpFrom, spDr, ref spPofS, spSource, "S")==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    #endregion

                    #region 癌栓
                    spDofE = new SpecSourcePlan();

                    // lc.SPECNO,lc.SPECPOR, lc.GETGROM, lc.SPECTYPEID
                    DataRow drspDofE = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "E", tmpFrom, "4" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["DEIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspDofE["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if (this.GenerateInfo("D", 4, currentCnt, tmpFrom, spDr, ref spDofE, spSource, "E") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    spRofE = new SpecSourcePlan();

                    DataRow drspRofE = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "E", tmpFrom, "5" });
                    if (posCount == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["REIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(drspRofE["CNT"]);
                        }
                        catch
                        { currentCnt = 0; }
                    }
                    if (this.GenerateInfo("R", 5, currentCnt, tmpFrom, spDr, ref spRofE, spSource, "E") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }


                    spPofE = new SpecSourcePlan();

                    DataRow drspPofE = dsPlan.Tables[0].Rows.Find(new string[] { specNo, "E", tmpFrom, "7" });
                    if (index == 1)
                    {
                        try
                        {
                            currentCnt = Convert.ToInt32(spDr["PEIN"]);
                        }
                        catch
                        {
                            currentCnt = 0;
                        }
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        currentCnt = Convert.ToInt32(drspPofE["CNT"]);
                    //    }
                    //    catch
                    //    { currentCnt = 0; }
                    //}
                    if (this.GenerateInfo("P", 7, currentCnt, tmpFrom, spDr, ref spPofE, spSource, "E") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    #endregion

                    index++;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                
                #endregion
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }          
        }

        private void GetNoImportedData()
        {
            //妇E0 35     泌D0 26     肠C3   25    NPC 鼻 A1 21
            //肝C2 18     内Z0 8      乳B4   17    内激 J0  120
            //食B2 24     头A0 23     胃C1   20
            //string disType = "妇";
            //int disTypeid = 35;
            
            string alList = @"select a.DISID,a.DISNAME from SPEC_ORGLISTIMP a";

            DataSet dsList = new DataSet();
            planMgr.ExecQuery(alList, ref dsList);

            foreach (DataRow dRow in dsList.Tables[0].Rows)
            {
                string disType = dRow["DISNAME"].ToString();
                int disTypeid = Convert.ToInt32(dRow["DISID"].ToString());

                string tmpSql = string.Empty;

                if (disType == "内")
                {
                    tmpSql = @"select 
                                seq
                                from SPEC_OLDORGDATA d where d.SPECNO like '" + disType + "%' and d.SPECNO not like '内激%' order by seq";
                }
                else
                {
                    tmpSql = @"select 
                                seq
                                from SPEC_OLDORGDATA d where d.SPECNO like '" + disType + "%' order by seq";
                }
                DataSet ds = new DataSet();

                planMgr.ExecQuery(tmpSql, ref ds);

                //下边这段个人认为不应该以取材脏器作为参数传入，这样漏数据可能性大，应该传入病种代码如肠25 
                //lingk20110831
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.GetSource(disType, dr[0].ToString(), disTypeid);
                }
            }
            /*
            string disType = "肺";
            int disTypeid = 19;

            string tmpSql = @"select 
                                seq
                                from SPEC_OLDORGDATA d where d.SPECNO like '" + disType + "%' order by seq";

            DataSet ds = new DataSet();

            planMgr.ExecQuery(tmpSql, ref ds);

            //下边这段个人认为不应该以取材脏器作为参数传入，这样漏数据可能性大，应该传入病种代码如肠25 
            //lingk20110831
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[0].ToString() == "447" || dr[0].ToString() == "456")
                {
                    this.GetSource(disType, dr[0].ToString(), disTypeid);
                    //this.GetSource(disCode, dr[0].ToString(), disTypeid);
                }
            }
            */
            MessageBox.Show("补导入记录数：" + rowCount.ToString() + "条！");
        }

        public override int Save(object sender, object neuObject)
        {
            this.GetNoImportedData();
            return base.Save(sender, neuObject);
        }
    }
}
