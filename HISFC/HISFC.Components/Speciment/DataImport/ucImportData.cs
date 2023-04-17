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

namespace FS.HISFC.Components.Speciment.DataImport
{
    public partial class ucImportData : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SpecPatient patient = new SpecPatient();
        private PatientManage pMgr = new PatientManage();

        public ucImportData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 首先写入PatientId 写入 SpecPatient表
        /// </summary>
        /// <returns></returns>
        private void GetOldPatientData()
        {         


            string sql = " select * from TEMP_YAO where BINGZHONG = '{0}' and ext1 is null or ext1 = '' ";
            string update = "update TEMP_YAO set EXT1='{1}' where IDD = '{0}' and Bingzhong = '{2}'";
            sql = string.Format(sql, txtDis.Text.Trim());
            DataSet ds = new DataSet();
            pMgr.ExecQuery(sql, ref ds);
            int cun = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    string cardNo = "";
                    string id = dr["IDD"] == null ? "" : dr["IDD"].ToString();
                    int patientId = 0;
                    string card_No = "";
                    ArrayList arrSpec = new ArrayList();
                    if(dr["IN_PATIENTNO"] == null && dr["IN_PATIENTNO"].ToString().Trim()=="")
                    {

                    }
                    if (dr["IN_PATIENTNO"] != null && dr["IN_PATIENTNO"].ToString().Trim()!="" )
                    {
                        int len =  dr["IN_PATIENTNO"].ToString().Length;
                        cardNo = dr["IN_PATIENTNO"].ToString().Trim();
                        if (len > 10)
                        {
                            cardNo = cardNo.Substring(4);
                        }

                        cardNo = cardNo.PadLeft(10, '0');
                        arrSpec = pMgr.GetPatientInfo("select * from SPEC_PATIENT where CARD_NO = '" + cardNo + "'");
                        //如果标本库中存在该病人的信息
                        if (arrSpec != null && arrSpec.Count > 0)
                        {
                            patientId = (arrSpec[0] as SpecPatient).PatientID;
                        }
                        //如果不存在从His中获取
                        else
                        {
                            patient = new SpecPatient();
                            patient = pMgr.GetPatientInfoCardNo(cardNo);
                            patient.Comment = "历史数据导入";
                            string pId = "";

                            pMgr.GetNextSequence(ref pId);
                            patientId = Convert.ToInt32(pId);
                            patient.PatientID = patientId;
                            pMgr.InsertPatient(patient);
                        }

                    }
                    else
                    {
                        card_No = dr["CARD_NO"] == null ? "": dr["CARD_NO"].ToString();
                        arrSpec.Clear();
                        if (card_No != "")
                        {
                            arrSpec = pMgr.GetPatientInfo("select * from SPEC_PATIENT where CARD_NO = '" + card_No + "'");

                        }
                        if (arrSpec != null && arrSpec.Count > 0)
                        {
                            patientId = (arrSpec[0] as SpecPatient).PatientID;
                        }
                        
                        else
                        {
                            patient = new SpecPatient();
                            string pId = "";
                            pMgr.GetNextSequence(ref pId);
                            patientId = Convert.ToInt32(pId);
                            patient.PatientName = dr["NAME"].ToString();
                            patient.Gender = dr["SEX"].ToString().Trim() == "男" ? 'M' : 'F';
                            patient.CardNo = dr["CARD_NO"].ToString();
                            patient.PatientID = patientId;
                            patient.Comment = "NO,历史数据导入";
                            pMgr.InsertPatient(patient);
                        }
                    }
                    string tmpsql = string.Format(update, id, patientId.ToString(),txtDis.Text.Trim());
                    int result = pMgr.ExecNoQuery(tmpsql);
                }
                catch
                {
                    continue;
                }
            }
        }

        private void GetSpecSource()
        {
            
            string sql = " select * from SPEC_OLDDATA where SPECID like '" + txtDis.Text.Trim() + "%' and (Ext2 <> '1' or EXT2 is null) order by seq1";
            DataSet ds = new DataSet();
            //return;
            pMgr.ExecQuery(sql, ref ds);
            int count = ds.Tables[0].Rows.Count;
            SpecSourceManage sMgr = new SpecSourceManage();
            SpecSourcePlanManage spMgr = new SpecSourcePlanManage();
            SubSpecManage subMgr = new SubSpecManage();
            int curBoxCount = 0;
            int curSBoxId = 4577;//Convert.ToInt32(txtBoxId3.Text.Trim());
            int curPBoxId = 4576;// Convert.ToInt32(txtBoxId4.Text.Trim());
            int distypeid = 3;
            int toseq = 100;
            int nextSBoxId = 0;
            int nextPBoxid = 1;
            int curWBoxId = 1662;
            
            

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                spMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                sMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                subMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                pMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    string subSeq = dr["SEQ1"].ToString();
                    int curSeq = Convert.ToInt32(subSeq);
                    subSeq = txtAbree.Text.Trim() + subSeq.PadLeft(6, '0');//血清

                    if (curSeq == 2801)
                    {
 
                    }

                    if (curSeq < 2161)
                    {
                        //continue;
                    }
                    if (curSeq >= 3101)
                    {
                        //return;
                        //return;
                    }
                    //肠标本疑问
                    //if (curSeq >= 2161 && curSeq <= 3100)
                    //{
                    //    continue;
                    //}

                    if (curSeq == 601)
                    {

                    }

                    SpecSource s = new SpecSource();
                    string specId = "";
                    sMgr.GetNextSequence(ref specId);
                    s.OrgOrBoold = "B";
                    s.DiseaseType.DisTypeID = distypeid;
                    s.SpecId = Convert.ToInt32(specId);
                    s.IsHis = 'O';
                    s.IsInBase = '0';
                    s.SpecNo = curSeq.ToString().PadLeft(6, '0');
                    s.SendDoctor.ID = dr["DOCNAME"] == null ? "" : dr["DOCNAME"].ToString();
                    try
                    {
                        s.OperTime = dr["OPERTIME"] == null ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["OPERTIME"].ToString());

                    }
                    catch
                    {
                        s.OperTime = new DateTime(1900, 1, 1);
                    }
                    s.NonantiBldCount = 1;
                    s.SendTime = s.OperTime;
                    s.NonAntiBldCapcacity = 1;
                    try
                    {
                        s.AnticolBldCapcacity = Convert.ToDecimal(dr["EDTA"].ToString());
                    }
                    catch
                    {
                        s.AnticolBldCapcacity = 0;
                    }
                    try
                    {
                        s.NonAntiBldCapcacity = Convert.ToDecimal(dr["NOEDTA"].ToString());
                    }
                    catch
                    { }
                    s.OperEmp.Name = dr["OPERNAME"] == null ? "" : dr["OPERNAME"].ToString();
                    s.Commet = dr["COMMENT"] == null ? "" : dr["COMMENT"].ToString();
                    try
                    {
                        s.MatchFlag = dr["EXT1"] == null ? "" : dr["EXT1"].ToString().Substring(0, 1);
                    }
                    catch
                    {
                        s.MatchFlag = "0";
                    }
                    //注意查看送存科室 有没有存入
                    s.DeptNo = dr["INDEPT"] == null ? "" : dr["INDEPT"].ToString();
                    string subCode = dr["SPECID"].ToString();
                    string upOldDataSql = " UPDATE  SPEC_OLDDATA SET EXT2 = '1' where SPECID = '" + subCode + "' ";

                    if (sMgr.ExecNoQuery(upOldDataSql) <= 0)
                    {
                        sMgr.WriteErr();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                    }

                    #region 标本源的处理
                    string oS = "select EXT1 from TEMP_YAO where BINGZHONG = '{0}' AND IDD='{1}'";
                    string old = "select VAL FROM TEMP_YAO where BINGZHONG = '{0}' AND IDD='{1}'";
                    string toS = string.Format(oS, txtDis.Text.Trim(), dr["OLDID"].ToString());
                    string told = string.Format(old, txtDis.Text.Trim(), dr["OLDID"].ToString());

                    //取历史数据病人的标志
                    string val = sMgr.ExecSqlReturnOne(told);
                    string patientId = sMgr.ExecSqlReturnOne(toS);
                    string inPatSql = "select IN_PATIENTNO from TEMP_YAO where BINGZHONG = '{0}' AND IDD='{1}'";

                    string updatePatVal = " update SPEC_PATIENT SET ODC = '" + val + "' WHERE PATIENTID = " + patientId;
                    if (sMgr.ExecNoQuery(updatePatVal) <= 0)
                    {
                        sMgr.WriteErr();
                    }

                    s.Patient.PatientID = Convert.ToInt32(patientId);
                    string inPatSqlTmp = string.Format(inPatSql, txtDis.Text.Trim(), dr["OLDID"].ToString());
                    string inpatientNo = sMgr.ExecSqlReturnOne(inPatSqlTmp);
                    s.InPatientNo = inpatientNo;
                    if (sMgr.InsertSpecSource(s) <= 0)
                    {
                        sMgr.WriteErr();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                    }
                    #endregion

                    #region 分装标本存储量处理
                    SpecSourcePlan sp = new SpecSourcePlan();

                    try
                    {
                        sp.Capacity = dr["CAP"] == null ? 0 : Convert.ToDecimal(dr["CAP"].ToString());
                    }
                    catch { }
                    try
                    {
                        sp.Count = dr["CNT"] == null ? 0 : Convert.ToInt32(dr["CNT"].ToString());
                    }
                    catch { }
                    int outCnt = 0;
                    try
                    {
                        outCnt = dr["OUTCNT"] == null ? 0 : Convert.ToInt32(dr["OUTCNT"].ToString());
                    }
                    catch
                    { }
                    if (outCnt >= sp.Count)
                    {
                        sp.StoreCount = 0;
                    }
                    else
                    {
                        sp.StoreCount = sp.Count - outCnt;
                    }

                    sp.SpecType.SpecTypeID = 2;// Convert.ToInt32(dr["TYPE"].ToString());
                    sp.StoreTime = s.OperTime;
                    sp.SpecID = s.SpecId;
                    sp.Comment = "历史数据导入";
                    int planId = Convert.ToInt32(spMgr.GetNextSequence());
                    sp.PlanID = planId;
                    if (spMgr.InsertSourcePlan(sp) <= 0)
                    {
                        sMgr.WriteErr();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                    }

                    SpecSourcePlan sp1 = new SpecSourcePlan();
                    sp1.Capacity = dr["CAP1"] == null ? 0.20m : Convert.ToDecimal(dr["CAP1"].ToString());
                    try
                    {
                        sp1.Count = dr["CNT1"] == null ? 0 : Convert.ToInt32(dr["CNT1"].ToString());
                    }
                    catch
                    {
                        sp1.Count = 0;
                    }
                    int outCnt1 = 0;
                    try
                    {
                        outCnt1 = dr["OUTCNT1"] == null ? 0 : Convert.ToInt32(dr["OUTCNT1"].ToString());
                    }
                    catch
                    {
                    }
                    if (outCnt1 >= sp1.Count)
                    {
                        sp1.StoreCount = 0;
                    }
                    else
                    {
                        sp1.StoreCount = sp1.Count - outCnt1;
                    }
                    sp1.SpecType.SpecTypeID = 1;//Convert.ToInt32(dr["TYPE1"].ToString());
                    sp1.StoreTime = s.OperTime;
                    sp1.SpecID = s.SpecId;
                    sp1.Comment = "历史数据导入";
                    int planId1 = Convert.ToInt32(spMgr.GetNextSequence());
                    sp1.PlanID = planId1;
                    if (spMgr.InsertSourcePlan(sp1) <= 0)
                    {
                        sMgr.WriteErr();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                    }


                    SpecSourcePlan sp2 = new SpecSourcePlan();

                    try
                    {
                        sp2.Capacity = (dr["CAP2"] == null || dr["CAP2"].ToString().Trim() == "") ? 0.20m : Convert.ToDecimal(dr["CAP2"].ToString());

                    }
                    catch
                    {
                        sp2.Capacity = 0;
                    }
                    try
                    {
                        sp2.Count = (dr["CNT2"] == null || dr["CNT2"].ToString().Trim() == "") ? 0 : Convert.ToInt32(dr["CNT2"].ToString());

                    }
                    catch
                    {
                        sp2.Count = 0;
                    }
                    int outCnt2 = 0;
                    try
                    {
                        outCnt2 = (dr["OUTCNT2"] == null || dr["OUTCNT2"].ToString().Trim() == "") ? 0 : Convert.ToInt32(dr["OUTCNT2"].ToString());

                    }
                    catch
                    {

                    }
                    if (outCnt2 >= sp2.Count)
                    {
                        sp2.StoreCount = 0;
                    }
                    else
                    {
                        sp2.StoreCount = sp2.Count - outCnt2;
                    }
                    sp2.SpecType.SpecTypeID = 3;// Convert.ToInt32(dr["TYPE2"].ToString());
                    sp2.StoreTime = s.OperTime;
                    sp2.SpecID = s.SpecId;
                    sp2.Comment = "历史数据导入";
                    int planId2 = Convert.ToInt32(spMgr.GetNextSequence());
                    sp2.PlanID = planId2;
                    if (spMgr.InsertSourcePlan(sp2) <= 0)
                    {
                        sMgr.WriteErr();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                    }

                    #endregion




                    //导入数据库时，已经出库的标本，不分配位置，只记录信息
                    for (int i = sp1.StoreCount + 1; i <= sp1.Count; i++)
                    {
                        SubSpec sub = new SubSpec();
                        sub.SubBarCode = subSeq + "P" + i.ToString();
                        sub.BoxId = 0;
                        sub.InStore = "0";
                        sub.Comment = "历史数据导入，已出库";
                        sub.IsReturnable = '0';
                        sub.SpecCap = 0;
                        sub.SpecCount = 0;
                        sub.SpecId = s.SpecId;
                        sub.SpecTypeId = sp1.SpecType.SpecTypeID;
                        sub.Status = "4";
                        sub.StoreID = sp1.PlanID;
                        sub.StoreTime = sp1.StoreTime;
                        string seq = "";
                        subMgr.GetNextSequence(ref seq);
                        sub.SubSpecId = 400 + Convert.ToInt32(seq);
                        if (subMgr.InsertSubSpec(sub) <= 0)
                        {
                            sMgr.WriteErr();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                        }
                    }

                    //导入数据库时，已经出库的标本，不分配位置，只记录信息
                    for (int i = sp.StoreCount + 1; i <= sp.Count; i++)
                    {
                        SubSpec sub = new SubSpec();
                        sub.SubBarCode = subSeq + "S" + i.ToString();
                        sub.BoxId = 0;
                        sub.InStore = "0";
                        sub.Comment = "历史数据导入，已出库";
                        sub.IsReturnable = '0';
                        sub.SpecCap = 0;
                        sub.SpecCount = 0;
                        sub.SpecId = s.SpecId;
                        sub.SpecTypeId = sp.SpecType.SpecTypeID;
                        sub.Status = "4";
                        sub.StoreID = sp.PlanID;
                        sub.StoreTime = sp.StoreTime;
                        string seq = "";
                        subMgr.GetNextSequence(ref seq);
                        sub.SubSpecId = 400 + Convert.ToInt32(seq);
                        if (subMgr.InsertSubSpec(sub) <= 0)
                        {
                            sMgr.WriteErr();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                        }
                    }



                    for (int i = sp2.StoreCount + 1; i <= sp2.Count; i++)
                    {
                        SubSpec sub = new SubSpec();
                        sub.SubBarCode = subSeq + "W" + i.ToString();
                        sub.BoxId = 0;
                        sub.InStore = "0";
                        sub.Comment = "历史数据导入，已出库";
                        sub.IsReturnable = '0';
                        sub.SpecCap = 0;
                        sub.SpecCount = 0;
                        sub.SpecId = s.SpecId;
                        sub.SpecTypeId = sp2.SpecType.SpecTypeID;
                        sub.Status = "4";
                        sub.StoreID = sp2.PlanID;
                        sub.StoreTime = sp2.StoreTime;
                        string seq = "";
                        subMgr.GetNextSequence(ref seq);
                        sub.SubSpecId = 400 + Convert.ToInt32(seq);
                        if (subMgr.InsertSubSpec(sub) <= 0)
                        {
                            sMgr.WriteErr();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                        }
                    }

                    //肠 小于1600 的处理方式
                    #region
                    if (curSeq <= toseq)
                    {
                        //血清
                        for (int i = 1; i <= sp.StoreCount; i++)
                        {
                            SubSpec sub = new SubSpec();
                            string lastLbl = subSeq.Substring(subSeq.Length - 1);
                            if (lastLbl == "0")
                            {
                                sub.BoxEndCol = 10;
                                sub.BoxStartCol = 10;
                            }
                            else
                            {
                                sub.BoxEndCol = Convert.ToInt32(lastLbl);
                                sub.BoxStartCol = Convert.ToInt32(lastLbl);
                            }
                            sub.BoxStartRow = i;
                            sub.BoxEndRow = i;
                            sub.SubBarCode = subSeq + "S" + i.ToString();
                            int boxSeq1 = curSeq % 10 == 0 ? 0 : 1;
                            //curSBoxId = 4576;
                            int boxSeq = Convert.ToInt32(curSBoxId) + curSeq / 10 + boxSeq1 - 1;
                            sub.BoxId = boxSeq;
                            sub.InStore = "1";
                            sub.Comment = "历史数据导入";
                            sub.IsReturnable = '1';
                            sub.SpecCap = sp.Capacity;
                            sub.SpecCount = 1;
                            sub.SpecId = s.SpecId;
                            sub.SpecTypeId = sp.SpecType.SpecTypeID;
                            sub.Status = "1";
                            sub.StoreID = sp.PlanID;
                            sub.StoreTime = sp.StoreTime;
                            string seq = "";
                            subMgr.GetNextSequence(ref seq);
                            sub.SubSpecId = 400 + Convert.ToInt32(seq);
                            if (subMgr.InsertSubSpec(sub) <= 0)
                            {
                                sMgr.WriteErr();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                continue;
                            }
                        }

                        //血浆
                        for (int i = 1; i <= sp1.StoreCount; i++)
                        {
                            SubSpec sub = new SubSpec();
                            string lastLbl = subSeq.Substring(subSeq.Length - 1);
                            if (lastLbl == "0")
                            {
                                sub.BoxEndCol = 10;
                                sub.BoxStartCol = 10;
                            }
                            else
                            {
                                sub.BoxEndCol = Convert.ToInt32(lastLbl);
                                sub.BoxStartCol = Convert.ToInt32(lastLbl);
                            }
                            sub.BoxStartRow = i + 5;
                            sub.BoxEndRow = i + 5;
                            sub.SubBarCode = subSeq + "P" + i.ToString();
                            int boxSeq1 = curSeq % 10 == 0 ? 0 : 1;
                            int boxSeq = Convert.ToInt32(curSBoxId) + curSeq / 10 + boxSeq1 - 1;
                            sub.BoxId = boxSeq;
                            sub.InStore = "1";
                            sub.Comment = "历史数据导入";
                            sub.IsReturnable = '1';
                            sub.SpecCap = sp1.Capacity;
                            sub.SpecCount = 1;
                            sub.SpecId = s.SpecId;
                            sub.SpecTypeId = sp1.SpecType.SpecTypeID;
                            sub.Status = "1";
                            sub.StoreID = sp1.PlanID;
                            sub.StoreTime = sp1.StoreTime;
                            string seq = "";
                            subMgr.GetNextSequence(ref seq);
                            sub.SubSpecId = 400 + Convert.ToInt32(seq);
                            if (subMgr.InsertSubSpec(sub) <= 0)
                            {
                                sMgr.WriteErr();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                continue;
                            }
                        }

                    }
                    #endregion

                    #region 大于 2000处理方式
                    if (curSeq > toseq)
                    {


                        //continue;
                        //curBoxCount++;
                        //if (curBoxCount > 20)
                        //{
                        //    curPBoxId += 2;
                        //    curSBoxId += 2;
                        //    curBoxCount = 1;
                        //}

                        //curPBoxId = 4586-2;
                        //curSBoxId = 4587 -2;
                        if (curSeq % 20 == 1)
                        {
                            curPBoxId += 2;
                            curSBoxId += 2;
                        }
                        //isChange = false;

                        //血清
                        for (int i = 1; i <= sp.StoreCount; i++)
                        {                            
                            //curPBoxId = 1179;
                            //curSBoxId = 1180;
                            SubSpec sub = new SubSpec();
                            int col = 0;
                            int row = 0;
                            if (curSeq % 20 <= 10)
                            {
                                col = curSeq % 20;
                                row = i;
                            }
                            if (curSeq % 20 > 10)
                            {
                                col = curSeq % 20 - 10;
                                row = i + 5;
                            }
                            if (curSeq % 20 == 0)
                            {
                                col = curSeq % 20 + 10;
                                row = i + 5;
                            }

                            //int boxSeq1 = curSeq % 20 == 0 ? 0 : 1;
                            //int boxSeq = Convert.ToInt32(curSBoxId) + curSeq / 20 + boxSeq1 - 1;
                            //sub.BoxId = boxSeq;
                            sub.BoxId = curSBoxId;
                            sub.BoxEndCol = col;
                            sub.BoxEndRow = row;
                            sub.BoxStartCol = col;
                            sub.BoxStartRow = row;
                            sub.Comment = "历史数据导入";
                            sub.SubBarCode = subSeq + "S" + i.ToString();
                            sub.IsReturnable = '1';
                            sub.SpecCap = sp.Capacity;
                            sub.SpecCount = 1;
                            sub.SpecId = s.SpecId;
                            sub.SpecTypeId = sp.SpecType.SpecTypeID;
                            sub.Status = "1";
                            sub.StoreID = sp.PlanID;
                            sub.StoreTime = sp.StoreTime;
                            string seq = "";
                            subMgr.GetNextSequence(ref seq);
                            sub.SubSpecId = 400 + Convert.ToInt32(seq);
                            if (subMgr.InsertSubSpec(sub) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                continue;
                            }
                        }

                        //血浆
                        for (int i = 1; i <= sp1.StoreCount; i++)
                        {
                            SubSpec sub = new SubSpec();
                            int col = 0;
                            int row = 0;
                            if (curSeq % 20 <= 10)
                            {
                                col = curSeq % 20;
                                row = i;
                            }
                            if (curSeq % 20 > 10)
                            {
                                col = curSeq % 20 - 10;
                                row = i + 5;
                            }
                            if (curSeq % 20 == 0)
                            {
                                col = curSeq % 20 + 10;
                                row = i + 5;
                            }

                            //int boxSeq1 = curSeq % 20 == 0 ? 0 : 1;
                            //int boxSeq = Convert.ToInt32(curPBoxId) + curSeq / 20 + boxSeq1 - 1;
                            //sub.BoxId = boxSeq;
                            sub.BoxId = curPBoxId;
                            sub.BoxEndCol = col;
                            sub.BoxEndRow = row;
                            sub.BoxStartCol = col;
                            sub.BoxStartRow = row;
                            sub.Comment = "历史数据导入";
                            sub.SubBarCode = subSeq + "P" + i.ToString();
                            sub.IsReturnable = '1';
                            sub.SpecCap = sp1.Capacity;
                            sub.SpecCount = 1;
                            sub.SpecId = s.SpecId;
                            sub.SpecTypeId = sp1.SpecType.SpecTypeID;
                            sub.Status = "1";
                            sub.StoreID = sp1.PlanID;
                            sub.StoreTime = sp1.StoreTime;
                            string seq = "";
                            subMgr.GetNextSequence(ref seq);
                            sub.SubSpecId = 400 + Convert.ToInt32(seq);
                            if (subMgr.InsertSubSpec(sub) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                continue;
                            }
                        }

                    }

                    #endregion

                    //鼻咽白细胞处理方式   --934  以前都是血浆血清放一起 包括934
                    #region
                    /*string cellSql = " select * from SPEC_TEMPLOC where dis = '" + txtDis.Text.Trim() + "' and SEQ = '" + curSeq.ToString() + "'";
                    DataSet dsCur = new DataSet();
                    subMgr.ExecQuery(cellSql, ref dsCur);
                    if (dsCur == null || dsCur.Tables.Count <= 0)
                    {
                        continue;
                    }
                    foreach (DataRow dr1 in dsCur.Tables[0].Rows)
                    {
                        SubSpec subSpec = new SubSpec();
                        subSpec.StoreID = sp2.PlanID;
                        subSpec.BoxEndCol = Convert.ToInt32(dr1["BOXCOL"].ToString());
                        subSpec.BoxStartCol = Convert.ToInt32(dr1["BOXCOL"].ToString());
                        subSpec.BoxStartRow = Convert.ToInt32(dr1["BOXROW"].ToString());
                        subSpec.BoxEndRow = Convert.ToInt32(dr1["BOXROW"].ToString());
                        subSpec.BoxId = Convert.ToInt32(dr1["BOXID"].ToString());
                        subSpec.Comment = "历史数据导入";
                        subSpec.InStore = "1";
                        subSpec.IsReturnable = '1';
                        subSpec.SpecCap = sp2.Capacity;
                        subSpec.SpecCount = 1;
                        subSpec.SpecId = s.SpecId;
                        subSpec.SpecTypeId = sp2.SpecType.SpecTypeID;
                        subSpec.Status = "1";
                        subSpec.StoreTime = sp2.StoreTime;
                        subSpec.SubBarCode = subSeq + "W1";
                        string seq = "";
                        subMgr.GetNextSequence(ref seq);
                        subSpec.SubSpecId = 400 + Convert.ToInt32(seq);
                        if (subMgr.InsertSubSpec(subSpec) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                        }

                        string updateCell = " update SPEC_TEMPLOC set ext1 = '1' where dis = '" + txtDis.Text.Trim() + "' and SEQ = '" + curSeq.ToString() + "'";
                        if (subMgr.ExecNoQuery(updateCell) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                        }
                    }*/
                    #endregion

                    #region 细胞
                    //血清

                    for (int i = 1; i <= sp2.StoreCount; i++)
                    {
                        if (curSeq >= 601)
                        {
                            curWBoxId = 1752 - 6;
                        }
                        
                        SubSpec sub = new SubSpec();
                        int col = 0;
                        int row = 0;
                        if (curSeq % 100 == 0)
                        {
                            col = 10;
                            row = 10;
                        }
                        else
                        {
                            string posIndex = (curSeq % 100).ToString().PadLeft(2, '0');
                            if (curSeq % 10 == 0)
                            {
                                row = Convert.ToInt32(posIndex.Substring(0, 1));
                                col = 10;
                            }
                            else
                            {
                                row = Convert.ToInt32(posIndex.Substring(0, 1)) + 1;
                                col = Convert.ToInt32(posIndex.Substring(1, 1));

                            }
                        }

                        int boxSeq1 = curSeq % 100 == 0 ? 0 : 1;
                        int boxSeq = curWBoxId + curSeq / 100 + boxSeq1 - 1;

                        sub.BoxId = boxSeq;
                        sub.BoxEndCol = col;
                        sub.BoxEndRow = row;
                        sub.BoxStartCol = col;
                        sub.BoxStartRow = row;
                        sub.Comment = "历史数据导入";
                        sub.SubBarCode = subSeq + "W" + i.ToString();
                        sub.IsReturnable = '1';
                        sub.SpecCap = sp2.Capacity;
                        sub.SpecCount = 1;
                        sub.SpecId = s.SpecId;
                        sub.SpecTypeId = sp2.SpecType.SpecTypeID;
                        sub.Status = "1";
                        sub.StoreID = sp2.PlanID;
                        sub.StoreTime = sp2.StoreTime;
                        string seq = "";
                        subMgr.GetNextSequence(ref seq);
                        sub.SubSpecId = 400 + Convert.ToInt32(seq);
                        if (subMgr.InsertSubSpec(sub) <= 0)
                        {
                            sMgr.WriteErr();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                        }
                        if (curSeq % 100 == 1)
                        {
                            //curWBoxId += 1;
                            //curSBoxId += 2;
                        }
                        //肠

                    }
                    #endregion



                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch
                {
                    sMgr.WriteErr();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    continue;
                }
                
            }
        }


        public override int Print(object sender, object neuObject)
        {
            try
            {
                this.GetOldPatientData();
            }
            catch
            { }
            return base.Print(sender, neuObject);
        }
        public override int Save(object sender, object neuObject)
        {
            try
            {
                if (txtAbree.Text.Trim() == "")
                    return 0;
                this.GetSpecSource();
            }
            catch
            { }
            return base.Save(sender, neuObject);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select specid from spec_source where spec_no is null";
                SubSpecManage submgr = new SubSpecManage();
                DataSet ds = new DataSet();
                submgr.ExecQuery(sql, ref ds);

                string update = " update spec_source set spec_no = '{0}' where specid = {1}";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        string specid = dr[0].ToString();
                        string tmp = " select distinct subbarcode from spec_subspec where specid = " + specid;
                        int cnt = ds.Tables[0].Rows.Count;
                        string sub = submgr.ExecSqlReturnOne(tmp);
                        string u = "";
                        if (sub.Length == 9)
                        {
                            u = string.Format(update, sub.Substring(1, 6), specid);
                        }
                        else
                        {
                            u = string.Format(update, sub.Substring(0, 6), specid);
                        }
                        submgr.ExecNoQuery(u);
                    }
                    catch
                    { }
 
                }

                //ExlToDb2Manage exl = new ExlToDb2Manage();                
                //SubSpecManage subMgr = new SubSpecManage();
                //DataSet ds = new DataSet();
                //exl.ExlConnect("D:\\历史数据落下.xls", ref ds);
                //int i = 1;
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    SubSpec sub = new SubSpec();
                //    string subSeq = "G" + dr["SEQ"].ToString().PadLeft(6, '0');
                //    sub.StoreID = Convert.ToInt32(dr["SOTREID"].ToString());
                //    string type = subMgr.ExecSqlReturnOne("select spectypeid from spec_source_store where sotreid = " + sub.StoreID.ToString());


                //    if (type == "1")
                //    {
                //        subSeq += "P1";
                //    }
                //    if (type == "2")
                //    {
                //        subSeq += "S1";
                //    }
                //    if (type == "3")
                //    {
                //        subSeq += "W1";
                //    }

                //    sub.SpecTypeId = Convert.ToInt32(type);

                //    i++;
                //    sub.BoxId = 0;
                //    sub.InStore = "0";
                //    sub.Comment = "历史数据导入，已出库";
                //    sub.IsReturnable = '0';
                //    sub.SpecCap = 0;
                //    sub.SpecCount = 0;
                //    sub.SpecId = Convert.ToInt32(dr["SPECID1"].ToString());
                //    sub.SubBarCode = subSeq;
                //    sub.Status = "4";

                //    string seq = "";
                //    subMgr.GetNextSequence(ref seq);
                //    sub.SubSpecId = 400 + Convert.ToInt32(seq);
                //    if (subMgr.InsertSubSpec(sub) <= 0)
                //    {
                //        continue;
                //    }
                //}
            }
            catch
            { }
        }
    }
}
