using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;
using System.Data;

namespace FS.HISFC.Components.Speciment
{
    public class OutInfo
    {
        private string specId="";
        private decimal count = 0.0M;
        private string returnAble = "";
        private int returnDays = 0;
        private string barCode = "";
        private string specBarCdoe = "";

        #region 新增一些字段用于打印
        private string position = string.Empty;
        private string subCol = string.Empty;
        private string subRow = string.Empty;
        private string subDis = string.Empty;
        private string subType = string.Empty;
        private string subqly = string.Empty;

        /// <summary>
        /// 标本位置
        /// </summary>
        public string Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
        /// <summary>
        /// 标本列
        /// </summary>
        public string SubCol
        {
            get
            {
                return this.subCol;
            }
            set
            {
                this.subCol = value;
            }
        }
        /// <summary>
        /// 标本行
        /// </summary>
        public string SubRow
        {
            get
            {
                return this.subRow;
            }
            set
            {
                this.subRow = value;
            }
        }
        /// <summary>
        /// 病种
        /// </summary>
        public string SubDis
        {
            get
            {
                return this.subDis;
            }
            set
            {
                this.subDis = value;
            }
        }
        /// <summary>
        /// 标本类型
        /// </summary>
        public string SubType
        {
            get
            {
                return this.subType;
            }
            set
            {
                this.subType = value;
            }
        }
        /// <summary>
        /// 标本性质
        /// </summary>
        public string SubQly
        {
            get
            {
                return this.subqly;
            }
            set
            {
                this.subqly = value;
            }
        }
        #endregion

        /// <summary>
        /// 标本号  
        /// </summary>
        public string SpecId
        {
            get
            {
                return specId;
            }
            set
            {
                specId = value;
            }
        }

        /// <summary>
        /// 出库的份数
        /// </summary>
        public decimal Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        /// <summary>
        /// 是否可还
        /// </summary>
        public string ReturnAble
        {
            get
            {
                return returnAble;
            }
            set
            {
                returnAble = value;
            }
        }

        /// <summary>
        /// 最多借出几天
        /// </summary>
        public int ReturnDays
        {
            get
            {
                return returnDays;
            }
            set
            {
                returnDays = value;
            }
        }

        /// <summary>
        /// 标本的条码
        /// </summary>
        public string SpecBarCode
        {
            get
            {
                return specBarCdoe;
            }
            set
            {
                specBarCdoe = value;
            }
        }

        public string BoxBarCode
        {
            get
            {
                return barCode;
            }
            set
            {
                barCode = value;
            }
        }
    }

    public class SpecOutOper
    {
        private ApplyTable currentTable;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private string curApplyNum;
        private FarPoint.Win.Spread.SheetView sheetView = new FarPoint.Win.Spread.SheetView();
        private string printTitle = "";
        private bool isDirec = false;
        private ArrayList arrTmpOut = new ArrayList();

        private ApplyTableManage applyTableManage = new ApplyTableManage();
        private SubSpecManage subSpecManage = new SubSpecManage();
        private SpecOutManage specOutManage = new SpecOutManage();
        private SpecApplyOutManage applyOutManage = new SpecApplyOutManage();
        private SpecSourcePlanManage specPlanManage = new SpecSourcePlanManage();
        private SpecTypeManage specTypeManage = new SpecTypeManage();
        private SpecBoxManage specBoxManage = new SpecBoxManage();
        private ShelfManage shelfManage = new ShelfManage();

        private string oper = "Imp";
        /// <summary>
        /// 申请数据类型
        /// </summary>
        public string Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }

        /// <summary>
        /// 打印纸的标题
        /// </summary>
        public string PrintTitle
        {
            get
            {
                return printTitle;
            }
            set
            {
                printTitle = value;
            }
        }

        /// <summary>
        /// 是否直接出库
        /// </summary>
        public bool IsDirect
        {
            set
            {
                isDirec = value;
            }
            
        }

        public string ApplyNum
        {
            set
            {
                curApplyNum = value;
            }
            get
            {
                return this.curApplyNum;
            }
        }

        public SpecOutOper(ApplyTable applyTable,FS.HISFC.Models.Base.Employee person)
        {
            currentTable = applyTable;
            loginPerson = person;    
        }
 

        /// <summary>
        /// UcBatchIn中使用该构造函数
        /// </summary>
        /// <param name="person"></param>
        /// <param name="trans"></param>
        public SpecOutOper(FS.HISFC.Models.Base.Employee person)
        {
             loginPerson = person; 
        }

        public SpecOutOper()
        {
 
        }

        /// <summary>
        /// UcBatchIn  ucBloodBarCode中使用该函数
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(System.Data.IDbTransaction trans)
        {
            specOutManage.SetTrans(trans);
            specBoxManage.SetTrans(trans);
            applyTableManage.SetTrans(trans);// = new ApplyTableManage();
            subSpecManage.SetTrans(trans);// = new SubSpecManage();
            specPlanManage.SetTrans(trans);// = new SpecSourcePlanManage();
            specTypeManage.SetTrans(trans);// = new SpecTypeManage();
            applyOutManage.SetTrans(trans);// = new SpecApplyOutManage();
            shelfManage.SetTrans(trans);
        }

        /// <summary>
        /// 更新审批表
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="listReturn">可还List</param>
        /// <param name="listSubSpecId"></param>
        /// <returns></returns>
        private int UpdateApplyTable(ArrayList arr, List<OutInfo> outInfoList)
        {
            ArrayList arrTmp = new ArrayList();
            arrTmp = arr;
            string specCountInDept = "{";//取用标本科室的所存标本的总量
            string perInAll = "{";//占总量的比例
            string leftAmout = "{";//剩余标本数量
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
            }
            #endregion

            #region 使用情况
            if (arrTmp.Count <= 0)
            {
                specCountInDept += "}";
                perInAll += "}";
                leftAmout += "}";
            }
            else
            {
                foreach (UseDeptSpecInfo u in arrTmp)
                {
                    specCountInDept += (u.deptNo + ",");
                    specCountInDept += (u.specTypeId + ",");
                    specCountInDept += u.specCountInDept;
                    specCountInDept += "}";
                    perInAll += (u.deptNo + ",");
                    perInAll += (u.specTypeId + ",");
                    perInAll += u.usePercent.ToString();
                    perInAll += "}";
                    leftAmout += (u.deptNo + ",");
                    leftAmout += (u.specTypeId + ",");
                    leftAmout += (u.specCountInDept - u.useCountInDept);
                    leftAmout += "}";
                    specCountInDept += ",";
                    perInAll += ",";
                    leftAmout += ",";
                }
            }
            #endregion

            //specList = currentTable.SpecList;
            //isReturnList = currentTable.IsImmdBackList;
            //specCountInDept = currentTable.SpecCountInDpet;
            //perInAll = currentTable.Percent;
            //leftAmout = currentTable.LeftAmount;
            string sql = "";
            if (curApplyNum != null && curApplyNum != "")
            {
                sql = " UPDATE SPEC_APPLICATIONTABLE" +
                             " SET SPECLIST = '" + specList + "',ISIMMEDBACKLIST='" + isReturnList + "'," +
                             " SPECCOUNTINDEPT='" + specCountInDept + "', PERINALL='" + perInAll + "',LEFTAMOUT = '" + leftAmout + "'" +
                             " WHERE APPLICATIONID=" + curApplyNum;
                return applyTableManage.ExecNoQuery(sql);
            }
            return 1;
            
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="subSpecId">出库标本ID</param>
        /// <returns></returns>
        public int UpdateSpecStore(string subSpecId)
        {
            //取出标本对应的SotreID
            string sql = "SELECT STOREID FROM SPEC_SUBSPEC WHERE SUBSPECID= " + subSpecId;
            DataSet ds = new DataSet();
            this.subSpecManage.ExecQuery(sql, ref ds);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string storeId = dt.Rows[0]["STOREID"].ToString();
                //根据StoreID查找 StoreCount 并更新StoreCount
                sql = "SELECT SOTRECOUNT FROM SPEC_SOURCE_STORE WHERE SOTREID= " + storeId;
                ds = new DataSet();
                this.specPlanManage.ExecQuery(sql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    int storeCount = Convert.ToInt32(dt.Rows[0]["SOTRECOUNT"].ToString());
                    storeCount--;
                    if (storeCount < 0)
                        storeCount = 0;
                    sql = "UPDATE SPEC_SOURCE_STORE SET SOTRECOUNT=" + storeCount.ToString() + " WHERE SOTREID=" + storeId;
                    int result = this.specPlanManage.ExecNoQuery(sql);
                    return result;
                }
            }
            return 0;
        }

        /// <summary>
        /// 保存当前出库的标本信息
        /// </summary>
        /// <param name="subSpecId">分装标本Id</param>
        /// <param name="specTypeId">标本类型ID</param>
        /// <param name="count">容量</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public int SaveSpecOutInfo(SpecOut specOut)
        {
            string squence = "";
            specOutManage.GetNextSequence(ref squence);
            specOut.OutId = Convert.ToInt32(squence);
            specOut.OutDate = specBoxManage.GetDateTimeFromSysDateTime();// DateTime.Now;
            specOut.OperId = loginPerson.ID;
            specOut.OperName = loginPerson.Name;
            //specOut.Count = count;           
           
            //specOut.SubSpecId = subSpec.SubSpecId;
            //specOut.BoxCol = subSpec.BoxEndCol;
            //specOut.BoxRow = subSpec.BoxEndRow;
            //specOut.BoxId = subSpec.BoxId;
            //specOut.Comment = subSpec.Comment;
            //specOut.SubSpecBarCode = subSpec.SubBarCode;
            //specOut.Unit = "";
            //specOut.SpecId = subSpec.SpecId;
            //if (curApplyNum==null || curApplyNum != "")
            //{
            //    specOut.RelateId = Convert.ToInt32(curApplyNum);
            //}
            int result = specOutManage.InsertSubSpecOut(specOut);
            return result;
        }

        /// <summary>
        /// 保存当前申请出库标本
        /// </summary>
        /// <param name="subSpec"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int SaveApplyOutInfo(SubSpec subSpec, decimal count, string isReturnAble)
        {
            SpecOut specOut = new SpecOut();
            string squence = "";
            specOutManage.GetNextSequence(ref squence);
            specOut.OutId = Convert.ToInt32(squence);
            specOut.OutDate = specOutManage.GetDateTimeFromSysDateTime();
            specOut.OperId = loginPerson.ID;
            specOut.OperName = loginPerson.Name;
            specOut.Count = count;
            specOut.SpecTypeId = subSpec.SpecTypeId;
            specOut.SubSpecId = subSpec.SubSpecId;
            specOut.BoxCol = subSpec.BoxEndCol;
            specOut.BoxRow = subSpec.BoxEndRow;
            specOut.BoxId = subSpec.BoxId;
            specOut.Comment = subSpec.Comment;
            specOut.SubSpecBarCode = subSpec.SubBarCode;
            specOut.Unit = "";
            specOut.Oper = this.Oper;
            specOut.IsRetuanAble = isReturnAble;
            specOut.SpecId = subSpec.SpecId;
            if (curApplyNum != null || curApplyNum != "")
            {
                specOut.RelateId = Convert.ToInt32(curApplyNum);
            }

            arrTmpOut.Add(specOut);
            return applyOutManage.InsertSubSpecApplyOut(specOut);
            //return result;
        }

        /// <summary>
        /// 确认出库
        /// </summary>
        /// <param name="arrOut"></param>
        /// <returns></returns>
        public int ConfirmSpecOut(ArrayList arrOut)
        {
            string sql = "";
            foreach (SpecOut c in arrOut)
            {
                if (this.SaveSpecOutInfo(c.Clone()) <= 0)
                {
                    return -1;
                }
                c.IsOut = "1";
                if (applyOutManage.UpdateSpecOut("update SPEC_APPLY_OUT set ISOUT = '1' where APPLYID =" + c.OutId.ToString()) <= 0)
                {
                    return -1;
                }
                switch (c.IsRetuanAble)
                {
                    case "1":
                        sql = " UPDATE SPEC_SUBSPEC  " +
                                     " SET STATUS = '2', ISRETURNABLE=1" +
                                     " WHERE SUBSPECID =" + c.SubSpecId;
                        if (subSpecManage.UpdateSubSpec(sql) <= 0)
                        {
                            return -1;
                        }
                        break;
                    case "2":
                        sql = " UPDATE SPEC_SUBSPEC" +
                                         " SET LASTRETURNTIME = to_date('" + subSpecManage.GetDateTimeFromSysDateTime().ToString() + "','yyyy-mm-dd hh24:mi:ss'), STATUS = '3', ISRETURNABLE =  1" +
                                         " WHERE SUBSPECID=" + c.SubSpecId;
                        if (subSpecManage.UpdateSubSpec(sql) <= 0)
                        {
                            return -1;
                        }
                        break;
                    case "0":
                        //sql = " UPDATE SPEC_SUBSPEC  " +
                        //             " SET STATUS = '2', ISRETURNABLE = 1" +
                        //             " WHERE SUBSPECID =" + c.SubSpecId;
                        //if (subSpecManage.UpdateSubSpec(sql) <= 0)
                        //{
                        //    return -1;
                        //}
                        sql = "UPDATE SPEC_SUBSPEC SET STATUS = '4', ISRETURNABLE=0, BOXID=0,BOXSTARTROW =0,BOXSTARTCOL=0,BOXENDROW=0,BOXENDCOL=0" +
                                     " WHERE SUBSPECID=" + c.SubSpecId;
                        if (subSpecManage.UpdateSubSpec(sql) <= 0)
                        {
                            return -1;
                        }

                        SpecBox tmpBox = specBoxManage.GetBoxById(c.BoxId.ToString());

                        int occupyCount = tmpBox.OccupyCount - 1;
                        if (occupyCount < 0)
                        {
                            occupyCount = 0;
                            if (tmpBox.DesCapType == 'J')
                            {
                                Shelf tmpShelf = shelfManage.GetShelfByBoxId(tmpBox.BoxId.ToString());
                                tmpShelf.OccupyCount = tmpShelf.OccupyCount - 1;

                                if (shelfManage.UpdateOccupyCount(tmpShelf.OccupyCount.ToString(), tmpShelf.ShelfID.ToString()) <= 0)
                                {
                                    return -1;
                                }
                            }
                            tmpBox.SpecTypeID = 0;
                            tmpBox.DisTypeId = 0;
                            tmpBox.DesCapID = 0; 
                            tmpBox.DesCapCol = 0;
                            tmpBox.DesCapSubLayer = 0;
                            tmpBox.DesCapRow = 0;
                            if (specBoxManage.UpdateSpecBox(tmpBox) <= 0)
                            {
                                return -1;
                            }

                        }
                        //更新标本盒的占用数量
                        if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), tmpBox.BoxId.ToString()) <= 0)
                        {
                            return -1;
                        }
                        if (tmpBox.DesCapType == 'B')
                        {
                            specBoxManage.UpdateOccupy(tmpBox.BoxId.ToString(), "0");
                        }
                         
                        break;
                }                 
               
                //更新Spec_Source_Store
                if (c.IsRetuanAble != "2")
                {
                    if (this.UpdateSpecStore(c.SubSpecId.ToString()) <= 0)
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int SpecOut(List<OutInfo> outInfoList)
        {
            arrTmpOut = new ArrayList();
            //,FS.NFC.Management.Transaction trans
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
 
            //subSpecManage.SetTrans(trans.Trans);
            //specPlanManage.SetTrans(trans.Trans);
            //applyTableManage.SetTrans(trans.Trans);
            //specOutManage.SetTrans(trans.Trans);
            //specBoxManage.SetTrans(trans.Trans);
            foreach (OutInfo spec in outInfoList)
            {
                SubSpec tmpSpec = subSpecManage.GetSubSpecById("",spec.SpecBarCode);
                if (this.SaveApplyOutInfo(tmpSpec, spec.Count,spec.ReturnAble) <= 0)
                {
                    return -1;
                }
                #region 作废
                //if (spec.ReturnAble == "1")
                //{
                //    tmpSpec.Status = "2";
                //    tmpSpec.IsReturnable = '1';
                    
                //    //string sql = " UPDATE SPEC_SUBSPEC  " +
                //    //             " SET STATUS = '2', ISRETURNABLE=1" +
                //    //             " WHERE SUBSPECID =" + spec.SpecId;
                //    if (subSpecManage.UpdateSubSpec(tmpSpec) <= 0)
                //    {
                //        return -1;
                //    }
                //}
                //if (spec.ReturnAble == "2")
                //{
                //    //DateTime dtReturn = DateTime.Now;                    
                //    //string sql = " UPDATE SPEC_SUBSPEC"+
                //    //             " SET LASTRETURNTIME = TIMESTAMP('"+ dtReturn.ToString() +"'), STATUS = '3', ISRETURNABLE =  1"+
                //    //             " WHERE SUBSPECID=" + spec.SpecId;
                //    //result = subSpecManage.UpdateSubSpec(sql);

                //    tmpSpec.Status = "3";
                //    tmpSpec.IsReturnable = '1';
                //    tmpSpec.LastReturnTime = subSpecManage.GetDateTimeFromSysDateTime();

                //    //string sql = " UPDATE SPEC_SUBSPEC  " +
                //    //             " SET STATUS = '2', ISRETURNABLE=1" +
                //    //             " WHERE SUBSPECID =" + spec.SpecId;
                //    if (subSpecManage.UpdateSubSpec(tmpSpec) <= 0)
                //    {
                //        return -1;
                //    }
                //}
                //if (spec.ReturnAble == "0") 
                //{

                //    tmpSpec.Status = "4";
                //    tmpSpec.IsReturnable = '0';
                //    tmpSpec.LastReturnTime = subSpecManage.GetDateTimeFromSysDateTime();

                //    //string sql = " UPDATE SPEC_SUBSPEC  " +
                //    //             " SET STATUS = '2', ISRETURNABLE=1" +
                //    //             " WHERE SUBSPECID =" + spec.SpecId;
                //    if (subSpecManage.UpdateSubSpec(tmpSpec) <= 0)
                //    {
                //        return -1;
                //    }

                //    string sql = "UPDATE SPEC_SUBSPEC SET STATUS = '4', ISRETURNABLE=0, BOXID=0,BOXSTARTROW =0,BOXSTARTCOL=0,BOXENDROW=0,BOXENDCOL=0" +
                //                 " WHERE SUBSPECID=" + spec.SpecId;
                //    result = subSpecManage.UpdateSubSpec(sql);
                //    string boxId = specBoxManage.GetBoxByBarCode(spec.BoxBarCode).BoxId.ToString();
                //    int occupyCount = specBoxManage.GetBoxById(boxId).OccupyCount - 1;
                //    if (occupyCount <= 0)
                //        occupyCount = 0;
                //   //更新标本盒的占用数量
                //    result = specBoxManage.UpdateOccupyCount(occupyCount.ToString(), boxId);
                //    result = specBoxManage.UpdateOccupy(boxId, "0");
                //}
                ////更新Spec_Source_Store
                //if (spec.ReturnAble != "2")
                //{
                //    result = this.UpdateSpecStore(spec.SpecId);
                //}
                #endregion

                #region 统计同一科室同一标本类型的使用数量
                //string sqlDeptCount = "SELECT DISTINCT SPEC_SOURCE.DEPTNO,SPEC_SOURCE_STORE.SPECTYPEID,SPEC_TYPE.SPECIMENTNAME,SPEC_DISEASETYPE.DISEASENAME" +
                //             " FROM SPEC_SUBSPEC,SPEC_SOURCE LEFT JOIN SPEC_DISEASETYPE ON SPEC_DISEASETYPE.DISEASETYPEID=SPEC_SOURCE.DISEASETYPEID," +
                //             " SPEC_SOURCE_STORE LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SOURCE_STORE.SPECTYPEID" +
                //             " WHERE SPEC_SUBSPEC.SPECID = SPEC_SOURCE.SPECID AND SPEC_SUBSPEC.STOREID=SPEC_SOURCE_STORE.SOTREID" +
                //             " AND SPEC_SUBSPEC.SUBSPECID=" + spec.SpecId;
                //UseDeptSpecInfo useInfo = new UseDeptSpecInfo();
                //DataSet ds = new DataSet();
                //applyTableManage.ExecQuery(sqlDeptCount, ref ds);
                //DataTable dt = ds.Tables[0];
                //string deptNo = dt.Rows[0]["DEPTNO"].ToString();
                //string specTypeId = dt.Rows[0]["SPECTYPEID"].ToString();
                //string specTypeName = dt.Rows[0]["SPECIMENTNAME"].ToString();
                //string disTypeName = dt.Rows[0]["DISEASENAME"].ToString();
                //useInfo.deptNo = deptNo;
                //useInfo.specTypeId = specTypeId;
                //useInfo.disTypeName = disTypeName;
                //useInfo.specTypeName = specTypeName;
                //if (!arrInfo.Contains(useInfo))
                //{
                //    useInfo.useCountInDept = 1;
                //    arrInfo.Add(useInfo);
                //}
                //else
                //{
                //    //如果包含同一科室同一标本类型，更改使用数量
                //    for (int i = 0; i < arrInfo.Count; i++)
                //    {

                //        if (((UseDeptSpecInfo)arrInfo[i]).ChkInOneDept(deptNo, specTypeId))
                //        {
                //            ((UseDeptSpecInfo)arrInfo[i]).useCountInDept++;
                //        }
                //    }
                //}
                #endregion
            }

            #region 统计出库的同一病种 同一标本类型的数量
            //统计出库的同一病种 同一标本类型的数量
            //List<UseDeptSpecInfo> listUseInfo = new List<UseDeptSpecInfo>();
            //for (int i = 0; i < arrInfo.Count; i++)
            //{
            //    UseDeptSpecInfo tmp = arrInfo[i] as UseDeptSpecInfo;
            //    if (i == 0)
            //    {
            //        tmp.disSpecCount = tmp.useCountInDept;
            //        listUseInfo.Add(tmp);
            //    }
            //    else
            //    {
            //        for (int k = 0; k < listUseInfo.Count; k++)
            //        {
            //            if (tmp.disTypeName == listUseInfo[k].disTypeName && tmp.specTypeId == listUseInfo[k].specTypeId)
            //            {
            //                listUseInfo[k].disSpecCount += tmp.useCountInDept;
            //            }
            //            else
            //            {
            //                tmp.disSpecCount = tmp.useCountInDept;
            //                listUseInfo.Add(tmp);
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 统计同一科室同一标本类型的总数量
            //for (int i = 0; i < arrInfo.Count; i++)
            //{
            //    string sql = "SELECT DISTINCT SUM(SPEC_SOURCE_STORE.SOTRECOUNT) STORECOUNT" +
            //                 " FROM SPEC_SOURCE, SPEC_SOURCE_STORE" +
            //                 " WHERE SPEC_SOURCE_STORE.SPECID = SPEC_SOURCE.SPECID AND " +
            //                 " SPEC_SOURCE.DEPTNO = '" + ((UseDeptSpecInfo)arrInfo[i]).deptNo + "'" +
            //                 " AND SPEC_SOURCE_STORE.SPECTYPEID=" + ((UseDeptSpecInfo)arrInfo[i]).specTypeId;
            //    DataSet ds = new DataSet();
            //    applyTableManage.ExecQuery(sql, ref ds);
            //    DataTable dt = ds.Tables[0];
            //    int storeount = Convert.ToInt32(dt.Rows[0]["STORECOUNT"].ToString());
            //    int useCount = ((UseDeptSpecInfo)arrInfo[i]).useCountInDept;
            //    ((UseDeptSpecInfo)arrInfo[i]).specCountInDept = storeount + useCount;
            //    decimal usePercent = Convert.ToDecimal(((UseDeptSpecInfo)arrInfo[i]).useCountInDept) / Convert.ToDecimal(storeount + useCount);
            //     ((UseDeptSpecInfo)arrInfo[i]).usePercent = usePercent.ToString("0.00"); 
            //}
            #endregion
            //更新申请表
            //result = UpdateApplyTable(arrInfo, outInfoList);

            if (isDirec)
            {
                if (this.ConfirmSpecOut(arrTmpOut) <= 0)
                {
                    return -1;
                }
            }

            return 1;

        }

        /// <summary>
        /// 根据申请ID获取出库申请信息
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        public ArrayList GetSubSpecOut(string applyId)
        {
            return applyOutManage.GetSubSpecOut(applyId);
        }

        public ApplyTable QueryApplyByID(string applyId)
        {
            return applyTableManage.QueryApplyByID(applyId);
        }

        public int UpdateSpecOut(string sql)
        {
            return applyOutManage.UpdateSpecOut(sql);
        }

        /// <summary>
        /// 根据标本条码获取标本信息
        /// </summary>
        /// <param name="specBarCode">标本条码</param>
        /// <returns>1成功 -1失败</returns>
        public int GetSubSpecById(string specBarCode, ref SubSpec tmpSpec)
        {
            try
            {
                tmpSpec = subSpecManage.GetSubSpecById("", specBarCode);
                if (tmpSpec == null)
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public void PrintOutSpec(List<SubSpec> outSpec, System.Data.IDbTransaction trans)
        {
            #region 
            this.SetSheetView();
            sheetView.RowCount = 0;
            #endregion
            IceBoxManage iceBoxManage = new IceBoxManage();
            if (trans != null)
            {
                iceBoxManage.SetTrans(trans);
                specBoxManage.SetTrans(trans);
                ParseLocation.BoxManage = iceBoxManage;
            }

            Dictionary<string, List<SubSpec>> dicSpecList = new Dictionary<string, List<SubSpec>>();
            int index = 0;
            #region
            foreach (SubSpec s in outSpec)
            {
                if (s.BoxId > 0)
                {
                    string boxBarCode = specBoxManage.GetBoxById(s.BoxId.ToString()).BoxBarCode;
                    string boxLocation = ParseLocation.ParseSpecBox(boxBarCode);
                  
                    List<SubSpec> tmpList = new List<SubSpec>();
                    if (index == 0)
                    {
                        tmpList.Add(s);
                        dicSpecList.Add(boxLocation, tmpList);
                        index++;
                        continue;
                    }
                    if (dicSpecList.ContainsKey(boxLocation))
                    {
                        dicSpecList[boxLocation].Add(s);
                        index++;
                        continue;
                    }
                    if (!dicSpecList.ContainsKey(boxLocation))
                    {
                        tmpList.Add(s);
                        dicSpecList.Add(boxLocation, tmpList);
                    }
                }
                index++;
            }
            #endregion            

           
            for (int i = 0; i < 1; i++)
            {
                foreach (KeyValuePair<string, List<SubSpec>> dic in dicSpecList)
                {
                    //gridView.Rows.Add(new object[] { dic.Key });
                    int tmpIndex = 0;
                    foreach (SubSpec spec in dic.Value)
                    {
                        int rowIndex = sheetView.Rows.Count;
                        ++sheetView.Rows.Count;

                        try
                        {
                            string seq = spec.SubBarCode.Length <= 9 ? spec.SubBarCode.Substring(1, 6) : spec.SubBarCode.Substring(0, 6);
                            sheetView.Cells[rowIndex, 1].Text = seq;
                        }
                        catch
                        { }
                        sheetView.Cells[rowIndex, 2].Text = spec.SubBarCode.ToString();
                        sheetView.Cells[rowIndex, 3].Text = spec.BoxEndRow.ToString();
                        sheetView.Cells[rowIndex, 4].Text = spec.BoxEndCol.ToString();
                        if (tmpIndex == 0)
                        {
                            sheetView.Cells[rowIndex, 0].Text = dic.Key;
                            //gridView.Rows.Add(new object[] { dic.Key, (object)spec.SubSpecId.ToString(), (object)spec.SubBarCode, (object)spec.BoxEndRow.ToString(), (object)spec.BoxEndCol.ToString() });
                            //tmpIndex++;
                            
                        }
                        else
                        {
                            sheetView.Cells[rowIndex, 0].Text = "";
                            //gridView.Rows.Add(new object[] { "", (object)spec.SubSpecId.ToString(), (object)spec.SubBarCode, (object)spec.BoxEndRow.ToString(), (object)spec.BoxEndCol.ToString() });
                            
                        }
                        tmpIndex++;
                    }
                }
            }
            ucLocPrint ucLoc = new ucLocPrint();
            ucLoc.SheetTitle = PrintTitle;
            ucLoc.SheetView = this.sheetView;
            ucLoc.SetSheet();
            ucLoc.Print();            
        }

        /// <summary>
        /// 初始化SheetView
        /// </summary>
        private void SetSheetView()
        {
            sheetView.AutoGenerateColumns = false;
            sheetView.DataAutoSizeColumns = false;
            sheetView.Rows.Count = 0;
            sheetView.Columns.Count = 6;
            sheetView.ColumnHeader.Rows.Count = 2;
 
            sheetView.ColumnHeader.Cells[1, 0].Text = "标本盒";
            sheetView.ColumnHeader.Cells[1, 1].Text = "标本号";
            sheetView.ColumnHeader.Cells[1, 2].Text = "条形码";
            sheetView.ColumnHeader.Cells[1, 3].Text = "行";
            sheetView.ColumnHeader.Cells[1, 4].Text = "列";

            sheetView.Columns[0].Width = 300;
            sheetView.Columns[1].Width = 90;
            sheetView.Columns[2].Width = 100;
            sheetView.Columns[3].Width = 40;
            sheetView.Columns[4].Width = 40;

            for (int i = 0; i < sheetView.Columns.Count; i++)
            {  
                sheetView.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                sheetView.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
        }
 
        internal class UseDeptSpecInfo
        {
            public string deptNo = "";//科室编号
            public string specTypeId = "";//标本类型ID
            public string specTypeName = "";//标本类型名称
            public string disTypeName = "";//病种类型名称
            public int specCountInDept = 0;//科室某一标本类型的总数
            public int useCountInDept = 0;//在一个科室中取用的标本数量
            public string usePercent = "";//一个科室中标本类型的百分比
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
        internal class CurrentArrayList : ArrayList
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
    }
}
