using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public class SpecOutMgr
    { 
        private ApplyTableManage applyTableManage=new ApplyTableManage();
        private SubSpecManage subSpecManage = new SubSpecManage();
        private SpecOutManage specOutManage = new SpecOutManage();
        private SpecSourcePlanManage specPlanManage = new SpecSourcePlanManage();
        private IceBoxManage iceBoxManage = new IceBoxManage();
        private SpecBoxManage specBoxManage = new SpecBoxManage();

        private FS.HISFC.Models.Base.Employee loginPerson=new FS.HISFC.Models.Base.Employee();
        private FarPoint.Win.Spread.SheetView sheetView = new FarPoint.Win.Spread.SheetView();
        private ApplyTable currentTable = new ApplyTable();
        private string printTitle = "";
        private string curApplyNum = "";
        private List<SubSpec> specList = new List<SubSpec>();

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

        public FS.HISFC.Models.Base.Employee LoginPerson
        {
            set
            {
                loginPerson = value; 
            }
        }

        public ApplyTable ApplyTable
        {
            set 
            {
                currentTable = value;
                curApplyNum = currentTable.ApplyId.ToString();
            } 
        }                 
 
        /// <summary>
        /// UcBatchIn  ucBloodBarCode中使用该函数
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(System.Data.IDbTransaction trans)
        {
            subSpecManage.SetTrans(trans);
            specPlanManage.SetTrans(trans);
            applyTableManage.SetTrans(trans);
            specOutManage.SetTrans(trans);
            specBoxManage.SetTrans(trans);
            iceBoxManage.SetTrans(trans);
        }

        /// <summary>
        /// 更新审批表
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="listReturn">可还List</param>
        /// <param name="listSubSpecId"></param>
        /// <returns></returns>
        private int UpdateApplyTable()
        {            
            string sql = "";
            if (curApplyNum != null && curApplyNum != "")
            {
                //sql = " UPDATE SPEC_APPLICATIONTABLE" +
                //             " SET SPECLIST = '" + specList + "',ISIMMEDBACKLIST='" + isReturnList + "'," +
                //             " SPECCOUNTINDEPT='" + specCountInDept + "', PERINALL='" + perInAll + "',LEFTAMOUT = '" + leftAmout + "'" +
                //             " WHERE APPLICATIONID=" + curApplyNum;
                //return applyTableManage.ExecNoQuery(sql);
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
                    return this.specPlanManage.ExecNoQuery(sql);
                     
                }
            }
            return -1;
        }

        /// <summary>
        /// 保存当前出库的标本信息
        /// </summary>
        /// <param name="subSpecId">分装标本Id</param>
        /// <param name="specTypeId">标本类型ID</param>
        /// <param name="count">容量</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public int SaveSpecOutInfo(SubSpec subSpec, decimal count)
        {
            SpecOut specOut = new SpecOut();
            string squence = "";
            specOutManage.GetNextSequence(ref squence);
            specOut.OutId = Convert.ToInt32(squence);
            specOut.OutDate = DateTime.Now;
            specOut.OperId = loginPerson.ID;
            specOut.OperName = loginPerson.Name;
            specOut.Count = count;             
            //specOut.SpecTypeId = specTypeId;
            specOut.SubSpecId = subSpec.SubSpecId;
            specOut.BoxCol = subSpec.BoxEndCol;
            specOut.BoxRow = subSpec.BoxEndRow;
            specOut.BoxId = subSpec.BoxId;
            specOut.Comment = subSpec.Comment;
            specOut.SubSpecBarCode = subSpec.SubBarCode;
            
            specOut.Unit = "";
            if (curApplyNum==null || curApplyNum != "")
            {
                specOut.RelateId = Convert.ToInt32(curApplyNum);
            }
            return specOutManage.InsertSubSpecOut(specOut);
             
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int SpecOut(List<OutInfo> outInfoList)
        {
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
            int result = -1;             
            foreach (OutInfo spec in outInfoList)
            {
                SubSpec tmpSpec = subSpecManage.GetSubSpecById(spec.SpecId, "");
                if (SaveSpecOutInfo(tmpSpec, spec.Count) <= 0)
                    return -1;
                 this.specList.Add(tmpSpec);
                if (spec.ReturnAble == "1")
                {                    
                    string sql = " UPDATE SPEC_SUBSPEC  " +
                                 " SET STATUS = '2', ISRETURNABLE=1" +
                                 " WHERE SUBSPECID =" + spec.SpecId;
                    if (subSpecManage.UpdateSubSpec(sql) < 0)
                    {
                        return -1;
                    }
                }
                if (spec.ReturnAble == "2")
                {
                    DateTime dtReturn = DateTime.Now;                    
                    string sql = " UPDATE SPEC_SUBSPEC"+
                                 " SET LASTRETURNTIME = to_date('"+ dtReturn.ToString() +"','yyyy-mm-dd hh24:mi:ss'), STATUS = '3', ISRETURNABLE =  1"+
                                 " WHERE SUBSPECID=" + spec.SpecId;
                    if (subSpecManage.UpdateSubSpec(sql) < 0)
                    {
                        return -1;
                    }
                }
                if (spec.ReturnAble == "0") 
                {
                    string sql = "UPDATE SPEC_SUBSPEC SET STATUS = '4', ISRETURNABLE=0, BOXID=0,BOXSTARTROW =0,BOXSTARTCOL=0,BOXENDROW=0,BOXENDCOL=0" +
                                 " WHERE SUBSPECID=" + spec.SpecId;
                    result = subSpecManage.UpdateSubSpec(sql);
                    string boxId = specBoxManage.GetBoxByBarCode(spec.BoxBarCode).BoxId.ToString();
                    int occupyCount = specBoxManage.GetBoxById(boxId).OccupyCount - 1;
                    if (occupyCount <= 0)
                        occupyCount = 0;
                   //更新标本盒的占用数量
                    if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), boxId) < 0)
                    {
                        return -1;
                    }
                    if (specBoxManage.UpdateOccupy(boxId, "0") < 0)
                    {
                        return -1;
                    }
                }
                //更新Spec_Source_Store
                if (spec.ReturnAble != "2")
                {
                    if (this.UpdateSpecStore(spec.SpecId) < 0)
                        return -1;
                }
            }
 
            result = UpdateApplyTable();
            //打印出库列表
            PrintOutSpec(this.specList);
            return result;

        }

        public void PrintOutSpec(List<SubSpec> outSpec)
        {
            #region 
            this.SetSheetView();
            sheetView.RowCount = 0;
            #endregion  

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

        public int InsertApplyOut(SubSpec sub)
        {
            return 1;

        }

        public int IndirectSpecOut(List<OutInfo> outInfoList)
        {       
 
            foreach (OutInfo spec in outInfoList)
            {
                SubSpec tmpSpec = subSpecManage.GetSubSpecById(spec.SpecId, ""); 
                if(InsertApplyOut(tmpSpec) <= 0)
                {
                    return -1;
                }
                this.specList.Add(tmpSpec);
            }
            //打印出库列表
            PrintOutSpec(this.specList);
            return 1;
            //1.插入出库申请表中
        }
    }
}
