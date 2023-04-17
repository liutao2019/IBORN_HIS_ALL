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

namespace FS.HISFC.Components.Speciment.TransferStore
{
    public partial class ucSpecMerge : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private IceBoxManage iceBoxManage;
        private IceBoxLayerManage layerManage;
        private ShelfManage shelfManage;
        private SpecBoxManage boxManage;      
        private BoxSpecManage boxSpecManage;
        private ShelfSpecManage shelfSpecManage;
        private SubSpecManage subSpecManage;
       
        //�ƶ���
        private List<SubSpec> specList;
        //�ƶ�ǰ
        private List<SubSpec> beforeRemoveList;
        //�ƶ�ǰÿһ���걾��λ��
        private Dictionary<string, string> dicBeforeMove;
        //�ƶ���
        private Dictionary<SpecBox, List<SubSpec>> dicAfterMove;

        private ArrayList arrBox;
        private Dictionary<string, List<SpecBox>> dicSameSpecBox;
        private Dictionary<SpecBox, List<SpecBox>> dicMergedBox;

        private string shelfId = "";
        private string layerId = "";
        
        //�ϲ��걾�еĹ��
        private int capacity = 0;
        
        public ucSpecMerge()
        {
            InitializeComponent();
            iceBoxManage = new IceBoxManage();
            layerManage = new IceBoxLayerManage();
            shelfManage = new ShelfManage();
            boxManage = new SpecBoxManage();
            arrBox = new ArrayList();
            boxSpecManage = new BoxSpecManage();
            shelfSpecManage = new ShelfSpecManage();
            subSpecManage = new SubSpecManage();
            dicSameSpecBox = new Dictionary<string, List<SpecBox>>();       
        }

        private void BindingIceBox()
        {
            ArrayList arrIcebox = new ArrayList();
            arrIcebox = iceBoxManage.GetAllIceBox();
            cmbIceBox.DisplayMember = "IceBoxName";
            cmbIceBox.ValueMember = "IceBoxId";
            cmbIceBox.DataSource = arrIcebox;
        }

        private void BindingLayer(string boxId)
        {
            ArrayList arrLayer = new ArrayList();
            arrLayer = layerManage.GetLayerInOneBox(boxId);
            if (arrLayer != null)
            {               
                cmbLayer.DisplayMember = "LayerNum";
                cmbLayer.ValueMember = "LayerId";
                cmbLayer.DataSource = arrLayer;
                cmbLayer.SelectedIndex = arrLayer.Count - 1;
                cmbLayer.Text = "";
            }
        }

        private void BindingShelf(string layerId)
        {
            ArrayList arrShelf = new ArrayList();
            arrShelf = shelfManage.GetShelfByLayerID(layerId);
            if (arrShelf != null)
            {
                arrShelf.Add(new Shelf());
                cmbShelf.DisplayMember = "SpecBarCode";
                cmbShelf.ValueMember = "ShelfID";
                cmbShelf.DataSource = arrShelf;
                cmbShelf.SelectedIndex = arrShelf.Count - 1;
            }
        }

        /// <summary>
        /// ��ѯ���������ı걾��
        /// </summary>
        /// <param name="layerId">�����Id</param>
        private DataTable GetBox()
        {            
            #region ��������д���2�����ϵı걾�й�� ���ܺϲ�
            if (cmbLayer.SelectedValue == null || cmbLayer.Text.Trim() == "")
            {
                if (cmbIceBox.SelectedValue != null && cmbIceBox.Text.Trim() != "" && cmbShelf.Text.Trim()=="" && cmbLayer.Text.Trim()=="")
                {
                    Dictionary<string, string> dicTmpSpec = new Dictionary<string, string>();
                    ArrayList arrLayer = layerManage.GetIceBoxLayers(cmbIceBox.SelectedValue.ToString());
                    foreach (IceBoxLayer layer in arrLayer)
                    {

                        layerId = layer.LayerId.ToString();
                        string boxLayerSql = " select distinct SPEC_BOX_SPEC.BOXSPECID \n" +
                                  " from spec_box_spec right join SPEC_SHELF_SPEC on SPEC_BOX_SPEC.BOXSPECID = SPEC_SHELF_SPEC.BOXSPECID\n" +
                                  " right join SPEC_ICEBOXLAYER on SPEC_ICEBOXLAYER.SPECID=SPEC_SHELF_SPEC.ID\n" +
                                  " where SPEC_ICEBOXLAYER.ICEBOXLAYERID=" + layer.LayerId.ToString();
                        string boxSpec = boxSpecManage.ExecSqlReturnOne(boxLayerSql);
                        if (!dicTmpSpec.ContainsKey(boxSpec))
                        {
                            dicTmpSpec.Add(boxSpec, "");
                        }
                    }
                    if (dicTmpSpec.Count > 1)
                    {
                        MessageBox.Show("�����д���2�����ϵı걾�й�񣬲��ܺϲ�", "�걾�кϲ�");
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            #endregion

            if (cmbLayer.SelectedValue != null && cmbLayer.Text.Trim() != "")
            {
                layerId = cmbLayer.SelectedValue.ToString();
            }

            string boxSql = " select distinct spec_box_spec.specrow * SPEC_BOX_SPEC.SPECCOL capacity\n" +
                                  " from spec_box_spec right join SPEC_SHELF_SPEC on SPEC_BOX_SPEC.BOXSPECID = SPEC_SHELF_SPEC.BOXSPECID\n" +
                                  " right join SPEC_ICEBOXLAYER on SPEC_ICEBOXLAYER.SPECID=SPEC_SHELF_SPEC.ID\n" +
                                  " where SPEC_ICEBOXLAYER.ICEBOXLAYERID=" + layerId;
            string boxCapacity = boxSpecManage.ExecSqlReturnOne(boxSql);

            try
            {
                capacity = Convert.ToInt32(boxCapacity);
                int occupyCapacity = Convert.ToInt32((100.0M - nudEmptyRate.Value) / 100.0M * Convert.ToDecimal(capacity));
                string sql = "select distinct spec_box.*\n" +
                             " from spec_box inner join spec_shelf on spec_box.descapid = SPEC_SHELF.shelfid\n" +
                             " inner join spec_iceboxlayer on spec_shelf.iceboxlayerid = SPEC_ICEBOXLAYER.iceboxlayerid \n" +
                             " inner join spec_icebox on spec_iceboxlayer.iceboxid = spec_icebox.iceboxid\n" +
                             " where spec_box.boxid>0 and SPEC_Box.OCCUPYCOUNT>0 and SPEC_BOX.OCCUPYCOUNT <= " + occupyCapacity.ToString();

                string sql2 = " select distinct Spec_diseasetype.DISEASENAME, SPECIMENTNAME, spec_box.boxid,spec_box.capacity,\n" +
                              " SPEC_BOX.OCCUPYCOUNT, SPEC_BOX.BOXBARCODE,(select distinct storeTime from spec_subspec  right join spec_box on SPEC_BOX.BOXID = SPEC_SUBSPEC.BOXID and storetime is not null fetch first 1 rows only) StoreTime\n" +
                              " from spec_box inner join spec_shelf on spec_box.descapid = SPEC_SHELF.shelfid \n" +
                              " inner join spec_iceboxlayer on spec_shelf.iceboxlayerid = SPEC_ICEBOXLAYER.iceboxlayerid \n" +
                              " inner join spec_icebox on spec_iceboxlayer.iceboxid = spec_icebox.iceboxid \n" +
                              " left join SPEC_TYPE on SPEC_BOX.SPECTYPEID = SPEC_TYPE.SPECIMENTTYPEID\n" +
                              " left join spec_diseasetype on SPEC_BOX.DISEASETYPEID = SPEC_diseasetype.DISEASETYPEID\n" +
                              " where spec_box.BOXID>0 and SPEC_Box.OCCUPYCOUNT>0 and SPEC_BOX.OCCUPYCOUNT <= " + occupyCapacity.ToString();

                if (cmbIceBox.Text.Trim() != "" && cmbIceBox.SelectedValue != null)
                {
                    string boxId = cmbIceBox.SelectedValue.ToString();
                    sql += " AND spec_icebox.iceboxid = " + boxId;
                    sql2 += " AND  spec_icebox.iceboxid = " + boxId;
                }

                if (cmbLayer.SelectedValue != null && cmbLayer.Text.Trim() != "")
                {
                    layerId = cmbLayer.SelectedValue.ToString();
                    sql += " AND spec_iceboxlayer.iceboxlayerid = " + layerId;
                    sql2 += " AND spec_iceboxlayer.iceboxlayerid = " + layerId;
                }
                if (cmbShelf.SelectedValue != null && cmbShelf.Text.Trim() != "")
                {
                    shelfId = cmbShelf.SelectedValue.ToString();
                    sql += " AND  SPEC_SHELF.shelfid = " + shelfId;
                    sql2 += " AND  SPEC_SHELF.shelfid = " + shelfId;
                }

                sql += " order by spec_box.OCCUPYCOUNT desc";
                sql2 += " order by spec_box.OCCUPYCOUNT, spec_diseasetype.DISEASENAME, SPECIMENTNAME desc";
                arrBox = boxManage.GetBoxBySql(sql);
                
                DataSet ds = new DataSet();
                boxManage.ExecQuery(sql2, ref ds);
                if (ds == null || ds.Tables.Count <= 0)
                {
                    return null;
                }
                return ds.Tables[0];
            }
           catch
           {
               return null;
           }
           
        }

        /// <summary>
        /// ������Ժϲ��ı걾��
        /// </summary>
        private void MergeBox()
        {
            dicSameSpecBox = new Dictionary<string, List<SpecBox>>();
            dicMergedBox = new Dictionary<SpecBox, List<SpecBox>>();
            if (arrBox == null || arrBox.Count <= 0)
            {
                return;
            }
            //����ͳ��ͬһ����ͬһ���͵ı걾��
            foreach (SpecBox box in arrBox)
            {
                //����ͬһ���ͬһ���� ͬһ���͵ı걾�н��з���
                string key = box.SpecTypeID.ToString() + "," + box.DiseaseType.DisTypeID.ToString() + ",";
                string year = subSpecManage.GetLastSpecInBox(box.BoxId.ToString()).StoreTime.Year.ToString();
                key += year;
                if (dicSameSpecBox.ContainsKey(key))
                {
                    dicSameSpecBox[key].Add(box);
                }
                else
                {
                    List<SpecBox> tmpBox = new List<SpecBox>();
                    tmpBox.Add(box);
                    dicSameSpecBox.Add(key, tmpBox);
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void CalMerge()
        {
            if (dicSameSpecBox == null || dicSameSpecBox.Count <= 0)
            {
                return;
            }
            #region ������Ժϲ��ı걾���б�           
            foreach (KeyValuePair<string,List<SpecBox>> tmpDic in dicSameSpecBox)
            {
                if (tmpDic.Value.Count <= 1)
                {
                    continue;
                }
                //canMergedBox ���Ժϲ���һ��ı걾���б�Key��Ŀ����ӣ�Value Դ����
                Dictionary<SpecBox, List<SpecBox>> canMergedBox = new Dictionary<SpecBox, List<SpecBox>>();               
                
                int boxCount = 0;
                int subSpecCount = 0;
                #region ��������걾�и���
                foreach (SpecBox b in tmpDic.Value)
                {
                    subSpecCount += b.OccupyCount;
                }
                if (subSpecCount % capacity == 0)
                {
                    boxCount = subSpecCount / capacity;
                }
                else
                {
                    boxCount = subSpecCount / capacity + 1;
                }
                #endregion
                //listMerged Ŀ��걾���б�
                List<SpecBox> listMerged = new List<SpecBox>();
                //����Ŀ������б���ǰboxCount���б�ĺ���
                for (int i = 0; i < boxCount; i++)
                {
                    canMergedBox.Add(tmpDic.Value[0], new List<SpecBox>());
                    listMerged.Add(tmpDic.Value[0]);
                    tmpDic.Value.Remove(tmpDic.Value[0]);
                }

                //��һ�μ��㣬���б걾�ƶ�
                #region ����ϲ��ı걾���б�
                try
                {
                    for (int i = 0; i < listMerged.Count; i++)
                    {
                        int desBoxCount = listMerged[i].OccupyCount;
                        //��תList,���ٵ����������
                        tmpDic.Value.Reverse();
                        bool reversed = false;

                        //��ѡװ�걾����Զ�ı걾��
                        for (int j = tmpDic.Value.Count - 1; j >= 0; j--)
                        {                            
                            int tmpCount = tmpDic.Value[j].OccupyCount;
                            desBoxCount += tmpCount;

                            if (desBoxCount <= capacity)
                            {
                                canMergedBox[listMerged[i]].Add(tmpDic.Value[j]);
                                tmpDic.Value.Remove(tmpDic.Value[j]);
                            }
                            if (desBoxCount > capacity && !reversed)
                            {
                                //��ȥ�ռ��ϵĲ���
                                desBoxCount -= tmpCount;
                                //��תList �Ӷൽ�����У���ѡװ�걾������ٵı걾��
                                tmpDic.Value.Reverse();
                                j = tmpDic.Value.Count;
                                reversed = true;
                            }
                            if (desBoxCount > capacity && reversed)
                            {
                                break;
                            }
                        }
                        //if (i == 3)
                        //{
                        //    string s = "s";
                        //}
                    }
                }
                catch
                { }
                #endregion

                #region ʣ�µı걾������ܺϲ���һ�� ��ϲ������򲻺ϲ�
                if (tmpDic.Value.Count > 1)
                {
                    int occupyCunt = 0;
                    foreach (SpecBox box in tmpDic.Value)
                    {
                        occupyCunt += box.OccupyCount;
                    }
                    if (occupyCunt <= capacity)
                    {
                        SpecBox tmpBox = tmpDic.Value[0];
                        canMergedBox.Add(tmpBox, new List<SpecBox>());
                        tmpDic.Value.Remove(tmpBox);

                        foreach (SpecBox b in tmpDic.Value)
                        {
                            canMergedBox[tmpBox].Add(b);
                        }
                    }
                    else
                    {
                        foreach (SpecBox b in tmpDic.Value)
                        {
                            canMergedBox.Add(b, new List<SpecBox>());
                        }
                    }
                }
                #endregion
                foreach (KeyValuePair<SpecBox, List<SpecBox>> sl in canMergedBox)
                {
                    dicMergedBox.Add(sl.Key, sl.Value);
                }
            }
            #endregion
        }

        /// <summary>
        /// ������Ҫ��λ�ı걾����ʼλ�ã���ֹλ��
        /// </summary>
        private void CalSubSpecMerge()
        {
            if (dicMergedBox == null || dicMergedBox.Count <= 0)
            {
                return;
            }
            dicAfterMove = new Dictionary<SpecBox, List<SubSpec>>();
            dicBeforeMove = new Dictionary<string, string>();
            foreach (KeyValuePair<SpecBox, List<SpecBox>> merged in dicMergedBox)
            {
                if (merged.Value.Count == 0)
                {
                    continue;
                }
                SpecBox desBox = merged.Key;
                dicAfterMove.Add(desBox, new List<SubSpec>());

                string desBoxId = desBox.BoxId.ToString();
                BoxSpec desBoxSpec = boxSpecManage.GetSpecByBoxId(desBoxId);
                int maxCol = desBoxSpec.Col;
                int maxRow = desBoxSpec.Row;
                specList = new List<SubSpec>();
                beforeRemoveList = new List<SubSpec>();
                ArrayList arrSpecList = new ArrayList();
                foreach (SpecBox sourceBox in merged.Value)
                {                    
                    arrSpecList = subSpecManage.GetSubSpecInOneBox(sourceBox.BoxId.ToString());
                    foreach (SubSpec spec in arrSpecList)
                    {
                        merged.Key.OccupyCount++;
                        dicBeforeMove.Add(spec.SubSpecId.ToString(), sourceBox.BoxBarCode +"-" + spec.BoxEndRow.ToString() + "-" + spec.BoxEndCol.ToString());
                        SubSpec beforeSpec = spec.Clone();
                        beforeRemoveList.Add(beforeSpec);
                        //���ұ걾λ��     
                        SubSpec lastSubSpec = subSpecManage.ScanSpecBox(desBoxId, desBoxSpec);
                                        
                        int currentEndRow = lastSubSpec.BoxEndRow;
                        int currentEndCol = lastSubSpec.BoxEndCol;
                        
                        if (currentEndCol < maxCol)
                        {
                            spec.BoxStartCol = currentEndCol + 1;
                            spec.BoxEndCol = currentEndCol + 1;
                            spec.BoxEndRow = currentEndRow;
                            spec.BoxStartRow = currentEndRow;
                        }
                        if (currentEndCol == maxCol && currentEndRow < maxRow)
                        {
                            spec.BoxEndCol = 1;
                            spec.BoxStartCol = 1;
                            spec.BoxStartRow = currentEndRow + 1;
                            spec.BoxEndRow = currentEndRow + 1;
                        }
                        spec.BoxId = Convert.ToInt32(desBoxId);
                        //���±걾λ����Ϣ
                        subSpecManage.UpdateSubSpec(spec);
                        specList.Add(spec);
                        dicAfterMove[desBox].Add(spec);
                    }
                }                
            }
            
        }

        /// <summary>
        /// ת����DataTable
        /// </summary>
        /// <returns>ת�����DataTable</returns>
        private DataTable BindingDataTable()
        {
            DataTable dt = new DataTable();

            DataColumn dcl = new DataColumn();
            dcl.ColumnName = "�걾�����к�";
            dcl.DataType= System.Type.GetType("System.String");
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "�걾��������";
            dcl.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "�걾���к�";
            dcl.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "�걾������";
            dcl.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "�ƶ�����";
            dcl.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "�ƶ�����";
            dcl.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.ColumnName = "�걾ԭλ��";
            dcl.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcl);

            if (dicAfterMove == null || dicAfterMove.Count <= 0)
            {
                return dt;
            }
            foreach (KeyValuePair<SpecBox, List<SubSpec>> tmp in dicAfterMove)
            {
                DataRow dr = dt.NewRow();
                dr["�걾�����к�"] = tmp.Key.BoxId.ToString();
                dr["�걾��������"] = tmp.Key.BoxBarCode;
                dt.Rows.Add(dr);
                foreach (SubSpec sub in tmp.Value)
                {
                    DataRow drSub = dt.NewRow();
                    string subSpecId = sub.SubSpecId.ToString();
                    drSub["�걾���к�"] = subSpecId;
                    drSub["�걾������"] = sub.SubBarCode;
                    drSub["�ƶ�����"] = sub.BoxEndRow.ToString();
                    drSub["�ƶ�����"] = sub.BoxEndCol.ToString();
                    drSub["�걾ԭλ��"] = dicBeforeMove[subSpecId];
                    dt.Rows.Add(drSub);
                }
            }
            
            return dt;              
        }

        /// <summary>
        /// �󶨵�FarPoint
        /// </summary>
        /// <param name="dt">�󶨵�����</param>
        private void BindingToFarPoint(DataTable dt)
        {
            neuSpread1_Sheet1.DataSource = null;
            foreach (FarPoint.Win.Spread.Column cl in neuSpread1_Sheet1.Columns)
            {
                cl.Width = 100;
            }
            neuSpread1_Sheet1.Columns[6].Width = 150;
            neuSpread1_Sheet1.AutoGenerateColumns = true;
            neuSpread1_Sheet1.DataSource = dt;   
        }

        /// <summary>
        /// �󶨱걾��
        /// </summary>
        /// <param name="dt"></param>
        private void BindingToFaPoint2(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataTable newDt = new DataTable();

            DataColumn dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "Id";
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "Locate";
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "StoreTime";
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "Blank";
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "DisType";
            newDt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "SpecType";
            newDt.Columns.Add(dcl);

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow newRow = newDt.NewRow();
                    newRow["Id"] = dr["BOXID"];
                    string barCode = dr["BOXBARCODE"].ToString();
                    newRow["Locate"] = iceBoxManage.GetIceBoxById(barCode.Substring(2, 3)).IceBoxName;
                    newRow["Locate"] += barCode.Substring(5);//
                    newRow["StoreTime"] = Convert.ToDateTime(dr["StoreTime"].ToString()).Year.ToString();
                    int blank = capacity - Convert.ToInt32(dr["OCCUPYCOUNT"].ToString());
                    newRow["Blank"] =((Convert.ToDecimal(blank)) / (Convert.ToDecimal(dr["capacity"])) * 100).ToString();
                    newRow["DisType"] = dr["DISEASENAME"].ToString();
                    newRow["SpecType"] = dr["SPECIMENTNAME"].ToString();
                    newDt.Rows.Add(newRow);
                }
            }
            catch
            { }
            neuSpread2_Sheet1.RowCount = 0;
            neuSpread2_Sheet1.DataSource = newDt;
            neuSpread2_Sheet1.AutoGenerateColumns = false;
             
            neuSpread2_Sheet1.ColumnCount = 7;
            neuSpread2_Sheet1.BindDataColumn(1, "Id");
            neuSpread2_Sheet1.BindDataColumn(2, "Locate");
            neuSpread2_Sheet1.BindDataColumn(3, "StoreTime");
            neuSpread2_Sheet1.BindDataColumn(4, "Blank");
            neuSpread2_Sheet1.BindDataColumn(5, "DisType");
            neuSpread2_Sheet1.BindDataColumn(6, "SpecType");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            shelfId = "";
            layerId = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            shelfSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ArrayList arrCheckedBox = new ArrayList();
            for (int i = 0; i < neuSpread2_Sheet1.Rows.Count; i++)
            {
                string value = neuSpread2_Sheet1.Cells[i, 0].Value == null ? null : neuSpread2_Sheet1.Cells[i, 0].Value.ToString();
                if (value != null && (value == "1" || value.ToLower() == "true"))
                {
                    string boxId = neuSpread2_Sheet1.Cells[i, 1].Text.Trim();                    
                    SpecBox tmpBox = boxManage.GetBoxById(boxId);
                    if (tmpBox != null && tmpBox.BoxId > 0)
                    {
                        arrCheckedBox.Add(tmpBox);
                    }
                }
            }
            if (arrCheckedBox != null && arrCheckedBox.Count > 1)
            {
                arrBox = arrCheckedBox;
            }
            if (arrBox == null || arrBox.Count <= 1)
            {
                GetBox();
                if (arrBox == null || arrBox.Count <= 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }
            MergeBox();
            CalMerge();
            CalSubSpecMerge();
            DataTable dt = BindingDataTable();
            BindingToFarPoint(dt);
            FS.FrameWork.Management.PublicTrans.RollBack();
        }

        private void ucSpecMerge_Load(object sender, EventArgs e)
        {
            BindingIceBox();            
        }

        private void cmbIceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIceBox.SelectedValue != null && cmbIceBox.Text.Trim() != "")
            {
                BindingLayer(cmbIceBox.SelectedValue.ToString());
            }
        }

        private void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLayer.SelectedValue != null && cmbLayer.Text.Trim() != "")
            {
                BindingShelf(cmbLayer.SelectedValue.ToString());
            }
        }

        public override int Save(object sender, object neuObject) 
        {
            if (specList != null & specList.Count > 0)
            {
                
                FS.HISFC.Models.Base.Employee loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                try
                {
                    SpecInOper specInOper = new SpecInOper();
                    specInOper.LoginPerson = loginPerson;
                    SpecOutOper specOutOper = new SpecOutOper(loginPerson);
                    specInOper.Trans = FS.FrameWork.Management.PublicTrans.Trans;
                    specInOper.SetTrans();
                    specOutOper.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    boxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    int index = 0;
                    foreach (SubSpec spec in specList)
                    {
                        spec.Comment = "�걾�ϲ����";
                        specInOper.SubSpec = spec;
                        if (subSpecManage.UpdateSubSpec(spec) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���±걾λ��ʧ�ܣ�", "�걾�кϲ�");
                            return -1;
                        }
                        if (specInOper.InOperInit() <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("������ʧ�ܣ�", "�걾�кϲ�");
                            return -1;
                        }

                        specOutOper.IsDirect = true;
                        List<OutInfo> outInfoList = new List<OutInfo>();
                        OutInfo tmpOut = new OutInfo();
                        tmpOut.ReturnAble = "0";
                        tmpOut.Count = 1;
                        tmpOut.SpecId = beforeRemoveList[index].SubSpecId.ToString();

                        if (specOutOper.SpecOut(outInfoList) <= 0)
                        {
                            return -1;
                        }

                        //if (specOutOper.SaveSpecOutInfo(beforeRemoveList[index], 1.0M) <= 0)
                        //{
                        //    MessageBox.Show("�������ʧ�ܣ�", "�걾�кϲ�");
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    return -1;
                        //}
                        index++;
                    }

                    foreach (SpecBox box in dicMergedBox.Keys)
                    {
                        int count = box.OccupyCount;
                        if (boxManage.UpdateOccupyCount(count.ToString(), box.BoxId.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���±걾��ʧ�ܣ�", "�걾�кϲ�");
                            return -1;
                        }
                        if (box.OccupyCount == box.Capacity)
                        {
                            if (boxManage.UpdateOccupy(box.BoxId.ToString(), "1") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���±걾��ʧ�ܣ�", "�걾�кϲ�");
                                return -1;
                            }
                        }
                    }

                    foreach (List<SpecBox> boxList in dicMergedBox.Values)
                    {
                        foreach (SpecBox box in boxList)
                        {
                            box.OccupyCount = 0;
                            box.IsOccupy = '0';
                            if (boxManage.UpdateSpecBox(box) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���±걾��ʧ�ܣ�", "�걾�кϲ�");
                                return -1;
                            }
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("�����ɹ���", "�걾�кϲ�");
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�", "�걾�кϲ�");
                    return -1;
                }
            }
            return base.Save(sender, neuObject);
        }

        public void Print()
        {
            FS.FrameWork.WinForms.Controls.NeuSpread neuSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();            
            FarPoint.Win.Spread.SheetView tmpSheetView = new FarPoint.Win.Spread.SheetView();
                
            neuSpread.Name = "neuSpread";
            neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] { tmpSheetView });   

            tmpSheetView.Reset();
            tmpSheetView.SheetName = "tmpSheetView";
            tmpSheetView.ColumnCount = 2;
            tmpSheetView.RowCount = 2;
            tmpSheetView.ColumnHeader.Cells.Get(0, 0).Value = "�걾��";      
            tmpSheetView.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            tmpSheetView.Columns.Get(0).Label = "�걾��";
            tmpSheetView.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            tmpSheetView.Columns.Get(0).Width = 1010F;
         
            tmpSheetView.RowHeader.Columns.Get(0).Width = 27F;
            try
            {
                int index = 0;
                foreach (KeyValuePair<SpecBox, List<SpecBox>> keyValue in dicMergedBox)
                {
                    string barCode = keyValue.Key.BoxBarCode;
                    string desBoxLoc = "λ�ã� " + iceBoxManage.GetIceBoxById(barCode.Substring(0, 3)).IceBoxName;
                    string barBoxCode = "���룺 " + keyValue.Key.BoxBarCode;

                    desBoxLoc += barCode.Substring(3, 4) + "-" + barCode.Substring(7, 2) + "-" + barCode.Substring(9, 2);
                    desBoxLoc += "  {";
                    barBoxCode += "      {";
                    //tmpSheetView.Cells[index, 0].Text = desBoxLoc;
                    //tmpSheetView.Rows.Add(index, 1);                    
                    string desBoxLocSub = "";
                    int i = 0;
                    foreach (SpecBox box in keyValue.Value)
                    {
                        string barCodeSub = box.BoxBarCode;
                        desBoxLocSub += iceBoxManage.GetIceBoxById(barCodeSub.Substring(0, 3)).IceBoxName;
                        desBoxLocSub += barCodeSub.Substring(3, 4) + "-" + barCodeSub.Substring(7, 2) + "-" + barCodeSub.Substring(9, 2);
                        barBoxCode += box.BoxBarCode;
                        if (i < keyValue.Value.Count - 1)
                        {
                            barBoxCode += ",";
                            desBoxLocSub += ",";
                        }
                        i++;
                    }
                    tmpSheetView.RowCount++;
                    index++;
                    desBoxLoc += desBoxLocSub + "}";
                    tmpSheetView.Cells[index, 0].Text = desBoxLoc;
                    tmpSheetView.RowCount++;
                    index++;
                    tmpSheetView.Cells[index, 0].Text = barBoxCode + "}";

                    tmpSheetView.RowCount++;
                    index++;
                    //tmpSheetView.Rows.Add(index, 1);
                }
                int count = tmpSheetView.Rows.Count;
            }
            catch
            { }
            
            try
            {
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
                p.PrintPreview(neuSpread);
               
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            Print();
            return base.OnPrint(sender, neuObject);
        }

        public override int Query(object sender, object neuObject)
        {
            try
            {
                DataTable dt = GetBox();
                if (dt == null || dt.Rows.Count <= 0)
                {                   
                    return -1;
                }
                BindingToFaPoint2(dt);
            }
            catch
            {
                return -1;
            }            
            return base.Query(sender, neuObject);
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < neuSpread2_Sheet1.Rows.Count; i++)
                {
                    //string boxId = neuSpread2_Sheet1.Cells[i, 1].Text;
                    neuSpread2_Sheet1.Cells[i, 0].Value = (object)1;
                }
            }
            else
            {
                for (int i = 0; i < neuSpread2_Sheet1.Rows.Count; i++)
                {
                    //string boxId = neuSpread2_Sheet1.Cells[i, 1].Text;
                    neuSpread2_Sheet1.Cells[i, 0].Value = (object)0;
                }
            }
        }
    }
}
