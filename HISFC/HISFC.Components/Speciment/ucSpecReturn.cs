using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucSpecReturn : UserControl
    {
        private SpecTypeManage specTypeMange;
        private OrgTypeManage orgTypeManage;
        private DisTypeManage disTypeManage;
        private SubSpecManage subSpecMange;
        private SpecBoxManage specBoxManage;
        private IceBoxManage iceBoxMange;
        private BoxSpecManage boxSpecManage;
        private SpecBarCodeManage barCodeManage;
        private SpecSourcePlanManage specPlanManage;
        private System.Data.IDbTransaction trans;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private List<string> subSpecList;
        private List<int> rowIndexList;
        private List<SpecBox> fullBoxList;
        private List<SubSpec> oldSpecList;
        private List<SubSpec> newSpecList;
        private SpecOutOper specOutOper;
        private string sql = "";
        private string title;

        /// <summary>
        /// 新增加的需要打印的标本条码
        /// </summary>
        public List<string> barCodeList;
        public List<string> sequenceList;
        public List<string> disTypeList;
        public List<string> numList;
        public List<string> specTypeList;

        public string Sql
        {
            get
            {
                return sql;
            }
            set
            {
                sql = value;
            }
        }

        public ucSpecReturn()
        {
            InitializeComponent();
            specTypeMange = new SpecTypeManage();
            orgTypeManage = new OrgTypeManage();
            disTypeManage = new DisTypeManage();
            subSpecMange = new SubSpecManage();
            specBoxManage = new SpecBoxManage();
            iceBoxMange = new IceBoxManage();
            subSpecList = new List<string>();
            rowIndexList = new List<int>();
            boxSpecManage = new BoxSpecManage();
            barCodeManage = new SpecBarCodeManage();
            specPlanManage = new SpecSourcePlanManage();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            oldSpecList = new List<SubSpec>();
            newSpecList = new List<SubSpec>();
            title = "标本返回入库";
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            specTypeList = new List<string>();
            numList = new List<string>();
        }

        #region 私有函数
        /// <summary>
        /// 组织类型绑定
        /// </summary>
        private void OrgTypeBinding()
        {
            Dictionary<int, string> orgTypeDic = new Dictionary<int, string>();
            orgTypeDic = orgTypeManage.GetAllOrgType();
            if (orgTypeDic.Count > 0)
            {
                //orgTypeDic.Add(-1,"");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = orgTypeDic;
                cmbOrgType.ValueMember = "Key";
                cmbOrgType.DisplayMember = "Value";
                cmbOrgType.DataSource = bsTmp;
                cmbOrgType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 根据组织类型ID获取标本类型
        /// </summary>
        /// <param name="orgTypeID"></param>
        private void SpecTypeBinding(string orgTypeID)
        {
            Dictionary<int, string> specTypeDic = new Dictionary<int, string>();
            specTypeDic = specTypeMange.GetSpecTypeByOrgID(orgTypeID);
            if (specTypeDic.Count > 0)
            {
                //specTypeDic.Add(-1, "");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = specTypeDic;
                cmbSpecType.ValueMember = "Key";
                cmbSpecType.DisplayMember = "Value";
                cmbSpecType.DataSource = bsTmp;
                cmbSpecType.Text = "";
            }
        }

        /// <summary>
        /// 解析标本盒位置信息
        /// </summary>
        /// <param name="boxBarCode"></param>
        /// <returns></returns>
        private string ParseBoxCode(string boxBarCode, string col, string row, string subLayer)
        {
            string tmpBarCode = boxBarCode;
            string boxId = Convert.ToInt32(tmpBarCode.Substring(2, 3)).ToString();
            string otherInfo = tmpBarCode.Substring(5);
            string parseResult = "";
            parseResult = iceBoxMange.GetIceBoxById(boxId).IceBoxName + " ";
            parseResult += otherInfo;
            return parseResult;
        }

        /// <summary>
        /// 查询出的数据绑定到FarPoint上
        /// </summary>
        public void DataBinding()
        {         
            DataSet ds = new DataSet();
            subSpecMange.ExecQuery(sql, ref ds);
            if (ds.Tables.Count <= 0 || ds == null)
            {
                return;
            }
            DataTable dt = ds.Tables[0];
            DataTable newDt = new DataTable();

            #region datatable初始化
            DataColumn dcl = new DataColumn();
            dcl.ColumnName = "SUBSPECID";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "SUBBARCODE";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "SPECID";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "STORETIME";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "SPECIMENTNAME";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "LASTRETURNTIME";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "BOXID";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "BOXENDCOL";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "BOXENDROW";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "BLOODORORGID";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "BOXBARCODE";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "DISEASENAME";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "OUTCOUNT";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "DESCAPROW";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "DESCAPCOL";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "DESCAPSUBLAYER";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "SPECTYPEID";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "DISEASETYPEID";
            dcl.DataType = System.Type.GetType("System.String");
            newDt.Columns.Add(dcl);
            #endregion

            try
            {
                foreach (DataRow dr in dt.Rows)
                { 
                    DataRow newDr = newDt.NewRow();
                    newDr.ItemArray = dr.ItemArray;
                    newDr["LASTRETURNTIME"] = newDr["LASTRETURNTIME"] == null ? "" : Convert.ToDateTime(newDr["LASTRETURNTIME"].ToString()).ToString("yyyy-MM-dd");
                    newDr["BOXBARCODE"] = ParseBoxCode(dr["BOXBARCODE"].ToString(), dr["DESCAPCOL"].ToString(), dr["DESCAPROW"].ToString(), dr["DESCAPSUBLAYER"].ToString());
                    newDr["STORETIME"] = newDr["STORETIME"] == null ? "" : Convert.ToDateTime(newDr["STORETIME"].ToString()).ToString("yyyy-MM-dd");
                    newDt.Rows.Add(newDr);
                }

                neuSpread1_Sheet1.AutoGenerateColumns = false;
                neuSpread1_Sheet1.DataSource = null;
                neuSpread1_Sheet1.DataSource = newDt;
                neuSpread1_Sheet1.BindDataColumn(1, "SUBBARCODE");
                neuSpread1_Sheet1.BindDataColumn(2, "SUBSPECID");
                neuSpread1_Sheet1.BindDataColumn(3, "BLOODORORGID");
                neuSpread1_Sheet1.BindDataColumn(4, "SPECIMENTNAME");
                neuSpread1_Sheet1.BindDataColumn(5, "DISEASENAME");
                neuSpread1_Sheet1.BindDataColumn(6, "BOXBARCODE");
                neuSpread1_Sheet1.BindDataColumn(10, "OUTCOUNT");
                neuSpread1_Sheet1.BindDataColumn(11, "LASTRETURNTIME");
                neuSpread1_Sheet1.BindDataColumn(12, "STORETIME");
                neuSpread1_Sheet1.BindDataColumn(13, "SPECTYPEID");
                neuSpread1_Sheet1.BindDataColumn(14, "DISEASETYPEID");

                neuSpread1_Sheet1.Columns[3].Visible = false;
                neuSpread1_Sheet1.Columns[13].Visible = false;
                neuSpread1_Sheet1.Columns[14].Visible = false;
             
                neuSpread1_Sheet1.Visible = true;
                int activeIndex;
                if (neuSpread1_Sheet1.Rows.Count >= 1)
                {
                    neuSpread1_Sheet1.SetActiveCell(neuSpread1_Sheet1.Rows.Count - 1, 0);
                    activeIndex = neuSpread1_Sheet1.ActiveCell.Row.Index;
                }
               
                neuSpread1_Sheet1.Rows.Count++;
                //neuSpread1_Sheet1.Rows.Add(0, 0);
                //neuSpread1_Sheet1.Rows.Add(newDt.Rows.Count, 0);
            }
               
            catch
            { }
        }

        /// <summary>
        /// 根据输入的barCode 查询出 分装标本并绑定到相应的行上
        /// </summary>
        /// <param name="rowIndex">当前编辑行索引</param>
        /// <param name="barCode">标本条码</param>
        private void RowBinding(int rowIndex, string barCode)
        {
            SubSpec tmp = new SubSpec();
            tmp = subSpecMange.GetSubSpecById("", barCode);
            if (tmp == null)
            {
                MessageBox.Show("获取信息失败！");
                return;
            }
            if (tmp.Status == "1" || tmp.Status == "3")
            {
                MessageBox.Show("该标本已经在库中！");
                return;
            }

            string tmpSql = " select SPEC_SUBSPEC.SUBSPECID, SPEC_SUBSPEC.SUBBARCODE, SPEC_SUBSPEC.SPECID,SPEC_SUBSPEC.STORETIME,\n" +
                            " SPEC_TYPE.SPECIMENTNAME, SPEC_SUBSPEC.LASTRETURNTIME, SPEC_SUBSPEC.BOXID,SPEC_SUBSPEC.BOXENDCOL,\n" +
                            " SPEC_SUBSPEC.BOXENDROW,SPEC_BOX.BLOODORORGID,SPEC_BOX.BOXBARCODE, SPEC_DISEASETYPE.DISEASENAME,\n" +
                            " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) OUTCOUNT,\n" +
                            "  SPEC_BOX.DESCAPROW,SPEC_BOX.DESCAPCOL,SPEC_BOX.DESCAPSUBLAYER,SPEC_SUBSPEC.SPECTYPEID,SPEC_BOX.DISEASETYPEID\n" +
                            " from SPEC_SUBSPEC left join spec_box on SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID\n" +
                            " left join spec_type on SPEC_SUBSPEC.SPECTYPEID= Spec_type.SPECIMENTTYPEID\n" +
                            " left join spec_diseasetype on SPEC_BOX.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID where SPEC_SUBSPEC.SUBBARCODE = '" + barCode + "'";
            DataSet ds = new DataSet();
            subSpecMange.ExecQuery(tmpSql, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return;
            }
            DataTable dt = ds.Tables[0];
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            dr["BOXBARCODE"] = ParseBoxCode(dr["BOXBARCODE"].ToString(), dr["DESCAPCOL"].ToString(), dr["DESCAPROW"].ToString(), dr["DESCAPSUBLAYER"].ToString());

            //绑定数据
            neuSpread1_Sheet1.Cells[rowIndex, 1].Text = dr["SUBBARCODE"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 2].Text = dr["SUBSPECID"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 3].Text = dr["BLOODORORGID"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 4].Text = dr["SPECIMENTNAME"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 5].Text = dr["DISEASENAME"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 6].Text = dr["BOXBARCODE"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 10].Text = dr["OUTCOUNT"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 11].Text = Convert.ToDateTime(dr["LASTRETURNTIME"].ToString()).ToString("yyyy-MM-dd");
            neuSpread1_Sheet1.Cells[rowIndex, 12].Text = Convert.ToDateTime( dr["STORETIME"].ToString()).ToString("yyyy-MM-dd");
            neuSpread1_Sheet1.Cells[rowIndex, 13].Text = dr["SPECTYPEID"].ToString();
            neuSpread1_Sheet1.Cells[rowIndex, 14].Text = dr["DISEASETYPEID"].ToString();

            //在当前扫描行下添加新行
            neuSpread1_Sheet1.Rows.Add(rowIndex + 1, 1);
            neuSpread1_Sheet1.SetActiveCell(rowIndex + 1, 1);
        }

        /// <summary>
        /// 扫描遍历选中的标本
        /// </summary>
        private void ScanCheckedSpec()
        {
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (neuSpread1_Sheet1.Cells[i, 0].Value != null && (neuSpread1_Sheet1.Cells[i, 0].Value.ToString() == "1" || neuSpread1_Sheet1.Cells[i, 0].Value.ToString().ToLower() == "true"))
                {
                    subSpecList.Add(neuSpread1_Sheet1.Cells[i, 2].Text.Trim());
                    rowIndexList.Add(i);
                }
            }
        }

        /// <summary>
        /// 原位归还
        /// </summary>
        private int KeepLocate()
        {
            //入库操作
            SpecInOper specInOper = new SpecInOper();
            specInOper.LoginPerson = loginPerson;
            specInOper.Trans = trans;
            specInOper.SetTrans();

            if (subSpecList.Count <= 0)
            {
                ScanCheckedSpec();
            }
            oldSpecList.Clear();
            foreach (string specId in subSpecList)
            {
                SubSpec sub = subSpecMange.GetSubSpecById(specId, "");

                oldSpecList.Add(sub);

                if (sub.Status == "3" || sub.Status == "1")
                {
                    continue;
                }
                sub.Status = "3";
                sub.LastReturnTime = DateTime.Now;
                sub.InStore = "S";
                if (subSpecMange.UpdateSubSpec(sub) <= 0)
                {
                    return -1;
                }
                sub.Comment = "标本返回入库";
                specInOper.SubSpec = sub;
                if (specInOper.InOper() <= 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 移除原有标本
        /// </summary>
        private int RemoveLocate()
        {
            if (subSpecList.Count <= 0)
            {
                ScanCheckedSpec();
            }

            int index = 0;
            foreach (string specId in subSpecList)
            {
                int rowindex = rowIndexList[index];
                //如果标本类型没有变更
                if (neuSpread1_Sheet1.Cells[rowindex, 7].Text.Trim() == "")
                {
                    index++;
                    continue;
                }
                SubSpec sub = subSpecMange.GetSubSpecById(specId, "");
                if (sub.BoxId == 0)
                {
                    index++;
                    continue;
                }

                sub.Comment = "标本返回时，不保留原标本";
                //不保留原有标本就当作原先标本已出库
                specOutOper.IsDirect = true;
                List<OutInfo> outInfoList = new List<OutInfo>();
                OutInfo tmpOut = new OutInfo();
                tmpOut.ReturnAble = "0";
                tmpOut.Count = 1;
                tmpOut.SpecId = sub.SubSpecId.ToString();

                if (specOutOper.SpecOut(outInfoList) <= 0)
                {
                    return -1;
                }
                //SpecBox box = specBoxManage.GetBoxById(sub.BoxId.ToString());
                //string boxId = box.BoxId.ToString();

                //int occupyCount = box.OccupyCount - 1;
                //if (occupyCount <= 0)
                //    occupyCount = 0;
                ////更新标本盒的占用数量
                //if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), boxId) <= 0)
                //{
                //    return -1;
                //}
                //if (specBoxManage.UpdateOccupy(boxId, "0") <= 0)
                //{
                //    return -1;
                //}

                //SpecSourcePlan specPlan = new SpecSourcePlan();
                //specPlan = specPlanManage.GetPlanById(sub.StoreID.ToString(),"");
                //int storeCount = specPlan.StoreCount - 1;
                //storeCount = storeCount < 0 ? 0 : storeCount;
                //if (specPlanManage.UpdateSourcePlan("update spec_source_store set sotrecount =" + storeCount.ToString() + " where sotreid=" + sub.StoreID.ToString()) <= 0)
                //{
                //    return -1;
                //}

                //sub.Status = "4";
                //sub.BoxId = 0;
                //sub.BoxEndCol = 0;
                //sub.BoxEndRow = 0;
                //sub.BoxStartCol = 0;
                //sub.BoxStartRow = 0;
                //sub.IsReturnable = '0';
                //if (subSpecMange.UpdateSubSpec(sub) <= 0)
                //{
                //    return -1;
                //}
                neuSpread1_Sheet1.Cells[rowindex, 6].Text = "";

                index++;
            }
            return 1;
        }

        /// <summary>
        /// 给标本类型变更的标本查找新位置
        /// </summary>
        private int FindLocateForChangeSpec()
        {
            fullBoxList = new List<SpecBox>();
            if (subSpecList.Count <= 0)
            {
                ScanCheckedSpec();
            }

            int index = 0;
            oldSpecList.Clear();
            newSpecList.Clear();

            foreach (string specId in subSpecList)
            {
                int rowIndex = rowIndexList[index];
                string orgTypeId = neuSpread1_Sheet1.Cells[rowIndex, 3].Text.Trim();
                string specTypeName = neuSpread1_Sheet1.Cells[rowIndex, 7].Text.Trim();
                string disTypeName = neuSpread1_Sheet1.Cells[rowIndex, 5].Text.Trim();
                string disTypeId = neuSpread1_Sheet1.Cells[rowIndex, 14].Text.Trim();
                string specTypeId = specTypeMange.GetSpecIDByName(specTypeName).ToString();
                int count = neuSpread1_Sheet1.Cells[rowIndex,15].Value==null? 1: Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIndex,15].Value);
                
                SubSpec sub = subSpecMange.GetSubSpecById(specId, "");
                oldSpecList.Add(sub);
                if (Convert.ToInt32(specTypeId) <= 0 || Convert.ToInt32(disTypeId) <= 0)
                {
                    continue;
                }                           
                int planId = 0;
                SpecBarCode bar = barCodeManage.GetSpecBarCode(disTypeName, specTypeName);
                if (bar == null)
                {
                    MessageBox.Show("查找序列号失败!");
                    return -1; 
                }
                string sequence = bar.Sequence;
                if (barCodeManage.UpdateBarCode(disTypeName, specTypeName, (Convert.ToInt32(sequence) + 1).ToString()) <= 0)
                {
                    MessageBox.Show("更新序列号!");
                    return -1; 
                }

                string barCodePre = "";
                if (cmbOrgType.Text.Contains("血"))
                {
                    barCodePre = bar.DisAbrre + bar.Sequence.PadLeft(6, '0') + bar.SpecTypeAbrre;
                } 
                for (int i = 0; i < count; i++)
                {

                    //标本类型变更的标本
                    SubSpec updatedSub = new SubSpec();
                    ArrayList arrBox = specBoxManage.GetLastLocation(disTypeId, specTypeId);
                    if (arrBox == null || arrBox.Count <= 0)
                    {
                        MessageBox.Show("此标本类型, 没有可使用的标本盒!", title);
                        return -1;
                    }

                    SpecBox currentBox = new SpecBox();
                    int boxCount = 0;
                    foreach (SpecBox b in arrBox)
                    {
                        if (b.BoxId > 0)
                        {
                            currentBox = b;
                            break;
                        }
                        boxCount++;
                    }
                    if (boxCount == arrBox.Count)
                    {
                        MessageBox.Show("此标本类型, 没有可使用的标本盒!", title);
                        return -1;
                    }
                    //if (currentBox.BoxId <= 0)
                    //{
                    //    //FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("此标本类型, 没有可使用的标本盒!", title);
                    //    return -1;
                    //}
                    updatedSub.BoxId = currentBox.BoxId;
                    BoxSpec boxSpec = boxSpecManage.GetSpecByBoxId(currentBox.BoxId.ToString());
                    int maxRow = boxSpec.Row;
                    int maxCol = boxSpec.Col;
                    //查找标本位置
                    SubSpec lastSubSpec = subSpecMange.ScanSpecBox(currentBox.BoxId.ToString(), boxSpec);
                    int currentEndRow = lastSubSpec.BoxEndRow;
                    int currentEndCol = lastSubSpec.BoxEndCol;
                    if (currentEndCol < maxCol)
                    {
                        updatedSub.BoxStartCol = currentEndCol + 1;
                        updatedSub.BoxEndCol = currentEndCol + 1;
                        updatedSub.BoxEndRow = currentEndRow;
                        updatedSub.BoxStartRow = currentEndRow;
                    }
                    if (currentEndCol == maxCol && currentEndRow < maxRow)
                    {
                        updatedSub.BoxEndCol = 1;
                        updatedSub.BoxStartCol = 1;
                        updatedSub.BoxStartRow = currentEndRow + 1;
                        updatedSub.BoxEndRow = currentEndRow + 1;
                    }

                    string subSpecId = "";
                    subSpecMange.GetNextSequence(ref subSpecId);
                    updatedSub.SubSpecId = Convert.ToInt32(subSpecId);
                    updatedSub.IsCancer = sub.IsCancer;
                    updatedSub.SpecCap = 1.0M;
                    updatedSub.SpecCount = 1;
                    updatedSub.SpecId = sub.SpecId;
                    updatedSub.SpecTypeId = Convert.ToInt32(specTypeId);
                    updatedSub.Status = "1";
                    updatedSub.StoreTime = DateTime.Now;                    
                    barCodeList.Add(updatedSub.SubBarCode);
                    numList.Add((i + 1).ToString());
                    disTypeList.Add(disTypeName);
                    specTypeList.Add(specTypeName); 
                    sequenceList.Add(sequence);
                    if (chkGenBarCode.Checked)
                    {
                        updatedSub.SubBarCode = barCodePre + (i + 1).ToString();
                    }
                    else
                    {
                        updatedSub.SubBarCode = neuSpread1_Sheet1.Cells[rowIndex, 8].Text.Trim();
                    }

                        if (neuSpread1_Sheet1.Cells[rowIndex, 16].Text.Trim() != "")
                        {
                            updatedSub.SpecCap = Convert.ToDecimal(neuSpread1_Sheet1.Cells[rowIndex, 16].Text.Trim());
                        }
                    updatedSub.InStore = "S";
                    SpecSourcePlan specPlan;
                    SpecSourcePlan specNewPlan;                   
                    if (i == 0)
                    {
                        specNewPlan = new SpecSourcePlan();
                        specPlan = new SpecSourcePlan();
                        specPlan = specPlanManage.GetPlanById(sub.StoreID.ToString(),"");                       
                        specNewPlan = specPlan.Clone();
                        specNewPlan.PlanID = Convert.ToInt32(specPlanManage.GetNextSequence());
                        planId = specNewPlan.PlanID;
                        specNewPlan.SpecType.SpecTypeID = Convert.ToInt32(specTypeId);
                        specNewPlan.StoreCount = count;
                        specNewPlan.Count = count;
                        specNewPlan.StoreTime = DateTime.Now;
                        specNewPlan.SubSpecCodeList.Add(updatedSub.SubBarCode);
                        if (specPlanManage.InsertSourcePlan(specNewPlan) == -1)
                        {
                            return -1;
                        }
                    }
                    updatedSub.StoreID = planId;
                    int result = subSpecMange.InsertSubSpec(updatedSub);

                    if (result == -1)
                    {
                        MessageBox.Show("生成新标本失败！", title);
                        return -1;
                    }                   
                    newSpecList.Add(updatedSub);

                    string newLocation = ParseBoxCode(currentBox.BoxBarCode, currentBox.DesCapCol.ToString(), currentBox.DesCapRow.ToString(), currentBox.DesCapSubLayer.ToString());
                    newLocation += "  " + updatedSub.BoxEndRow.ToString() + " " + updatedSub.BoxEndCol.ToString() + " ";
                    neuSpread1_Sheet1.Cells[rowIndex, 9].Text = newLocation + " ";
                    neuSpread1_Sheet1.Cells[rowIndex, 8].Text = updatedSub.SubBarCode + " ";
                    //更新当前盒子的占用量
                    result = specBoxManage.UpdateOccupyCount((currentBox.OccupyCount + 1).ToString(), currentBox.BoxId.ToString());
                    if (result == -1)
                    {
                        MessageBox.Show("更新标本盒失败！", title);
                        return -1;
                    }
                    bool isFull = false;
                    if (updatedSub.BoxEndCol >= maxCol && updatedSub.BoxEndRow >= maxRow)
                    {
                        isFull = true; 
                    }
                    if (currentBox.OccupyCount == currentBox.Capacity)
                    {
                        isFull = true;
                    }
                    //如果当前盒子满了提示入库,并更新
                    if (isFull)
                    {
                        specBoxManage.UpdateOccupy(currentBox.BoxId.ToString(), "1");
                        fullBoxList.Add(currentBox);
                        DialogResult diagResult = MessageBox.Show("该标本盒已满，是否入库?", "标本盒入库", MessageBoxButtons.YesNo);
                        if (diagResult == DialogResult.Yes)
                        {
                            result = specBoxManage.UpdateSotreFlag("1", currentBox.BoxId.ToString());
                            if (result == -1)
                            {
                                MessageBox.Show("更新标本盒失败！", title);
                                return -1;
                            }
                        }
                    }
                }               
                index++;
            }
            return 1;
        }

        #endregion

        private void orgTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrgType.SelectedValue != null && cmbOrgType.Text.Trim() != "")
            {
                int orgId = Convert.ToInt32(cmbOrgType.SelectedValue.ToString());
                SpecTypeBinding(orgId.ToString());
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    FarPoint.Win.Spread.Row r = neuSpread1_Sheet1.Rows[i];
                    if (r.Visible && neuSpread1_Sheet1.Cells[i, 2].Value != null && neuSpread1_Sheet1.Cells[i, 2].ToString().Trim() != "")
                    {
                        neuSpread1_Sheet1.Cells[i, 0].Value = (object)1;
                        //rowIndexList.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    neuSpread1_Sheet1.Cells[i, 0].Value = (object)false;
                    //rowIndexList.Clear();
                }
            }
        }

        private void neuSpread1_AutoFilteredColumn(object sender, FarPoint.Win.Spread.AutoFilteredColumnEventArgs e)
        {
            string filterString = e.FilterString;
            if (filterString == "(All)")
            {
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    FarPoint.Win.Spread.Row r = neuSpread1_Sheet1.Rows[i];
                    r.Visible = true;
                }
                return;
            }
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                FarPoint.Win.Spread.Row r = neuSpread1_Sheet1.Rows[i];
                string s1 = neuSpread1_Sheet1.Cells[i, e.Column].Value.ToString();
                if (s1 != filterString)
                {
                    r.Visible = false;
                }
                else
                    r.Visible = true;
            }
        }

        private void cmbSpecType_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                string type = cmbSpecType.Text;
                if (cmbOrgType.SelectedValue == null||!chkChangeType.Checked)
                {
                    return;
                }
                if ((neuSpread1_Sheet1.Cells[i, 0].Value != null) && (neuSpread1_Sheet1.Cells[i, 0].Value.ToString() == "1" || neuSpread1_Sheet1.Cells[i, 0].Value.ToString().ToLower() == "true"))
                {
                    if (cmbOrgType.SelectedValue.ToString() == neuSpread1_Sheet1.Cells[i, 3].Text.Trim())
                    {

                        neuSpread1_Sheet1.Cells[i, 7].Text = type;
                    }
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            subSpecList.Clear();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            trans = FS.FrameWork.Management.PublicTrans.Trans;

            orgTypeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            disTypeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            subSpecMange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            iceBoxMange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            barCodeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specPlanManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specTypeMange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specOutOper.SetTrans(trans);

            try
            {
                //如果标本类型不变更，存放在原先位置
                if (!chkChangeType.Checked)
                {

                    if (KeepLocate() == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败!", title);
                        return;
                    }
                    specOutOper.PrintOutSpec(oldSpecList, trans);
                }
                //标本类型变更
                if (chkChangeType.Checked)
                {
                    if (this.FindLocateForChangeSpec() == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败!", title);
                        return;
                    }
                    //不保留原标本，清空原标本位置信息
                    if (!chkKeepOld.Checked)
                    {
                        if (RemoveLocate() == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("操作失败!", title);
                            return;
                        }
                    }
                    else
                    {
                        if (KeepLocate() == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("操作失败!", title);
                            return;
                        }
                    }
                    //打印新旧位置列表
                    specOutOper.PrintTitle = "原标本位置";
                    specOutOper.PrintOutSpec(oldSpecList, trans);
                    if (chkChangeType.Checked)
                    {
                        specOutOper.PrintTitle = "变更后标本位置";
                        specOutOper.PrintOutSpec(newSpecList, trans);
                    }

                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功！", title);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("操作失败!", title);
                return;
            }
            if (fullBoxList != null)
            {
                foreach (SpecBox box in fullBoxList)
                {
                    if (box.OccupyCount == box.Capacity)
                    {
                        MessageBox.Show("当前标本盒已满，请添加新的标本盒！", title);
                        //提示用户添加新的标本盒
                        FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                        if (box.DesCapType == 'B')
                            newSpecBox.CurLayerId = box.DesCapID;
                        else
                            newSpecBox.CurShelfId = box.DesCapID;
                        newSpecBox.DisTypeId = box.DiseaseType.DisTypeID;
                        newSpecBox.OrgOrBlood = box.OrgOrBlood;
                        newSpecBox.SpecTypeId = box.SpecTypeID;
                        newSpecBox.Show();
                    }
                }
            }
        }

        private void chkChangeType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChangeType.Checked)
            {
                cmbOrgType.Enabled = true;
                cmbSpecType.Enabled = true;
            }
            if (!chkChangeType.Checked)
            {
                cmbOrgType.Enabled = false;
                cmbSpecType.Enabled = false;
                chkGenBarCode.Checked = false;
                chkKeepOld.Checked = false;
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    neuSpread1_Sheet1.Cells[i, 7].Text = "";
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmbOrgType.Text.Contains("血"))
            {
                if (PrintLabel.Print2DBarCode(barCodeList, sequenceList, specTypeList, disTypeList, numList) == -1)
                {
                    MessageBox.Show("打印失败");
                    return;
                }
                barCodeList = new List<string>();
                sequenceList = new List<string>();
                disTypeList = new List<string>();
                numList = new List<string>();
            }
        }

        private void neuSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int rowIndex = neuSpread1_Sheet1.ActiveRow.Index;
            if (e.Column == 1)
            {
                string barCode = neuSpread1_Sheet1.Cells[rowIndex, 1].Value.ToString();
                if (barCode.Length >= 8)
                {
                    this.RowBinding(rowIndex, barCode);
                }
            }
            if (e.Column == 8)
            {
                string newBarCode = neuSpread1_Sheet1.Cells[rowIndex, 8].Value.ToString();
                if (newBarCode.Length == 10 || newBarCode.Length == 12)
                {
                    //跳转到下一行
                    neuSpread1_Sheet1.SetActiveCell(rowIndex + 1, 8);
                }
            }
        }

        private void neuSpread1_KeyUp(object sender, KeyEventArgs e)
        {
            FarPoint.Win.Spread.Cell cell = neuSpread1.ActiveSheet.ActiveCell;

            if (cell.Column.Index == 1 && e.KeyCode == Keys.Enter)
            {
                int rowIndex = neuSpread1.ActiveSheet.ActiveRow.Index;
                string barCode = neuSpread1_Sheet1.Cells[rowIndex, 1].Value.ToString();
                this.RowBinding(rowIndex, barCode);
                neuSpread1_Sheet1.Rows.Add(rowIndex + 1, 1);
                this.neuSpread1_Sheet1.SetActiveCell(rowIndex + 1, 1);
            }
        }

        private void ucSpecReturn_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            specOutOper = new SpecOutOper(loginPerson);          
            OrgTypeBinding();         
            cmbOrgType.Text = "";          
            cmbSpecType.Text = "";
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                neuSpread1_Sheet1.Cells[i, 15].Value = Convert.ToInt32(nudCount.Value);
                //rowIndexList.Clear();
            }
        }

        private void nudCapacity_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                string typeId = neuSpread1_Sheet1.Cells[i, 13].Text.Trim();
                if (typeId.Trim() == "")
                {
                    continue;
                }
                string orgName = orgTypeManage.GetBySpecType(typeId).OrgName;
                if (orgName.Contains("血"))
                {
                    neuSpread1_Sheet1.Cells[i, 16].Value = Convert.ToDecimal(nudCapacity.Value);
                }
                //rowIndexList.Clear();
            }
           
            
        }
    }
}
