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
        /// ��ӡֽ�ı���
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
        /// UcBatchIn  ucBloodBarCode��ʹ�øú���
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
        /// ����������
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="listReturn">�ɻ�List</param>
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
        /// ���¿��
        /// </summary>
        /// <param name="subSpecId">����걾ID</param>
        /// <returns></returns>
        public int UpdateSpecStore(string subSpecId)
        {
            //ȡ���걾��Ӧ��SotreID
            string sql = "SELECT STOREID FROM SPEC_SUBSPEC WHERE SUBSPECID= " + subSpecId;
            DataSet ds = new DataSet();
            this.subSpecManage.ExecQuery(sql, ref ds);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string storeId = dt.Rows[0]["STOREID"].ToString();
                //����StoreID���� StoreCount ������StoreCount
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
        /// ���浱ǰ����ı걾��Ϣ
        /// </summary>
        /// <param name="subSpecId">��װ�걾Id</param>
        /// <param name="specTypeId">�걾����ID</param>
        /// <param name="count">����</param>
        /// <param name="trans">����</param>
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
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int SpecOut(List<OutInfo> outInfoList)
        {
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
                   //���±걾�е�ռ������
                    if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), boxId) < 0)
                    {
                        return -1;
                    }
                    if (specBoxManage.UpdateOccupy(boxId, "0") < 0)
                    {
                        return -1;
                    }
                }
                //����Spec_Source_Store
                if (spec.ReturnAble != "2")
                {
                    if (this.UpdateSpecStore(spec.SpecId) < 0)
                        return -1;
                }
            }
 
            result = UpdateApplyTable();
            //��ӡ�����б�
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
        /// ��ʼ��SheetView
        /// </summary>
        private void SetSheetView()
        {
            sheetView.AutoGenerateColumns = false;
            sheetView.DataAutoSizeColumns = false;
            sheetView.Rows.Count = 0;
            sheetView.Columns.Count = 6;
            sheetView.ColumnHeader.Rows.Count = 2;
 
            sheetView.ColumnHeader.Cells[1, 0].Text = "�걾��";
            sheetView.ColumnHeader.Cells[1, 1].Text = "�걾��";
            sheetView.ColumnHeader.Cells[1, 2].Text = "������";
            sheetView.ColumnHeader.Cells[1, 3].Text = "��";
            sheetView.ColumnHeader.Cells[1, 4].Text = "��";

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
            //��ӡ�����б�
            PrintOutSpec(this.specList);
            return 1;
            //1.��������������
        }
    }
}
