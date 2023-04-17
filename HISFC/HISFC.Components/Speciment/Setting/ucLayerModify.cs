using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using FS.HISFC.Models.Speciment;
using FS.FrameWork.WinForms.Controls;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucLayerModify : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private IceBoxLayerManage layerManage;
        //����������
        private IceBoxManage iceBoxManage;    
        private OrgTypeManage orgTypeManage;
        private SpecTypeManage specTypeManage;
        private ShelfManage shelfManage;
        private ShelfSpecManage shelfSpecManage;
        private SubSpecManage subSpecManage;
        private CapLogManage capLogManage;
        private SpecBoxManage specBoxManage;
        private DisTypeManage disTypeManage;

        //����ʵ���б�
        private ArrayList arrIcebox;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private string currentLayerSaveType;
        private string currentLayerId;
        // �޸�ǰ�Ĳ�����
        private IceBoxLayer currentLayer;
        //�޸ĺ�Ĳ�����
        private IceBoxLayer newIceBoxLayer;
        private ucIceBoxLayer ucLayer;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private string title;

        public ucLayerModify()
        {
            InitializeComponent();
            layerManage = new IceBoxLayerManage();
            iceBoxManage = new IceBoxManage();       
            shelfManage = new ShelfManage();
            arrIcebox = new ArrayList();
            shelfSpecManage = new ShelfSpecManage();
            orgTypeManage = new OrgTypeManage();
            specTypeManage = new SpecTypeManage();
            subSpecManage = new SubSpecManage();
            capLogManage = new CapLogManage();
            specBoxManage = new SpecBoxManage();
            currentLayerSaveType = "";
            currentLayerId = "";
            currentLayer = new IceBoxLayer();
            newIceBoxLayer = new IceBoxLayer();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            title = "����������޸�";
        }

        #region ��ʼ��ҳ�����ݰ�
        /// <summary>
        /// �󶨱걾����
        /// </summary>
        private void BindingSpecClass()
        {
            Dictionary<int, string> dicOrgType = new Dictionary<int, string>();
            dicOrgType.Add(-1, "");
            dicOrgType = orgTypeManage.GetAllOrgType();
            if (dicOrgType.Count > 0)
            {
                dicOrgType.Add(0, "");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = dicOrgType;
                cmbOrgOrBlood.DisplayMember = "Value";
                cmbOrgOrBlood.ValueMember = "Key";
                cmbOrgOrBlood.DataSource = bsTmp;
                cmbOrgOrBlood.SelectedValue = 0;
            }
        }

        /// <summary>
        /// �󶨱���
        /// </summary>
        private void BindingIceBox()
        {
            arrIcebox = iceBoxManage.GetAllIceBox();
            if (arrIcebox == null || arrIcebox.Count <= 0)
            {
                return;
            }
            arrIcebox.Add(new IceBox());
            cmbIceBox.DataSource = arrIcebox;
            cmbIceBox.DisplayMember = "IceBoxName";
            cmbIceBox.ValueMember = "IceBoxId";
            cmbIceBox.SelectedIndex = arrIcebox.Count - 1;
        }

        /// <summary>
        /// �󶨱걾����
        /// </summary>
        /// <param name="orgId"></param>
        private void BindingSpecType(string orgId)
        {
            Dictionary<int, string> dicSpecType = new Dictionary<int, string>();
            dicSpecType.Clear();
            dicSpecType.Add(-1, "");
            cmbSpecType.DataSource = null;
            dicSpecType = specTypeManage.GetSpecTypeByOrgID(orgId);
            if (dicSpecType.Count > 0)
            {
                dicSpecType.Add(0, "");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = dicSpecType;
                cmbSpecType.DisplayMember = "Value";
                cmbSpecType.ValueMember = "Key";
                cmbSpecType.DataSource = bsTmp;
                cmbSpecType.SelectedValue = 0;
            }
        }

        /// <summary>
        /// �󶨲�������
        /// </summary>
        private void BindingDisType()
        {
            disTypeManage = new DisTypeManage();
            Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
            }
            cmbDisType.Text = "";
        }

        #endregion

        private void Query()
        {
            neuSpread1_Sheet1.Rows.Count = 0;// = null;
            string preSql = " SELECT DISTINCT l.ICEBOXLAYERID ���к�1, ib.ICEBOXNAME ���ڱ�������," +
                         " (CASE ib.ICEBOXTYPEID WHEN '1' THEN '��ʽ�䶳��' WHEN '2' THEN '���¹�' WHEN '3' THEN 'Һ����' END) ��������," +
                         " l.LAYERNUM λ�ڵڼ���,\n" +
                         " t.SPECIMENTNAME �걾����,d.DISEASENAME ����," +
                         " (CASE l.SAVETYPE WHEN 'B' THEN '�걾' WHEN 'J' THEN '�����' END) ��������,\n" +
                         " l.OCCUPYCOUNT ռ������,l.CAPACITY ����,\n" +
                         " round((CAST(l.OCCUPYCOUNT AS DECIMAL(5,1))/l.CAPACITY),4)*100 ռ����\n" +
                         " FROM SPEC_ICEBOX ib RIGHT JOIN SPEC_ICEBOXLAYER l ON ib.ICEBOXID = l.ICEBOXID \n" +
                         " LEFT JOIN SPEC_DISEASETYPE d ON l.DISTYPEID = d.DISEASETYPEID \n" +
                         " LEFT JOIN SPEC_TYPE t ON l.SPECTYPEID = t.SpecimentTypeID \n" +
                         "left join SPEC_SHELF s on l.ICEBOXLAYERID = s.ICEBOXLAYERID left join SPEC_BOX b on s.SHELFID = b.DESCAPID\n"+
                         " WHERE l.SAVETYPE ='J' ";
            string layerSql = preSql;
            //layerSql += " WHERE l.ICEBOXLAYERID>0";
            //string otherInfo = "union " + preSql + "\n join SPEC_SHELF s on SPEC_ICEBOX.ICEBOXID = s.ICEBOXLAYERID join SPEC_BOX b on s.SHELFID = b.DESCAPID";
            // otherInfo += " WHERE l.ICEBOXLAYERID>0";

            //string boxType = "";
            //string boxName = "";
            //string layerNum = "";
            //string orgOrBld = "";
            //string specType = "";
            //string disType = "";

            if (txtIceBoxType.Tag != null && txtIceBoxType.Text != "")
            {
                layerSql += " AND ib.ICEBOXTYPEID = '" + txtIceBoxType.Tag.ToString() + "'"; 
            }
            if (cmbIceBox.Text != "")
            {
                layerSql += " AND ib.ICEBOXNAME LIKE '%" + cmbIceBox.Text + "%'";
            }
            if (nudLayerNum.Value > 0)
            {
                layerSql += " AND l.LAYERNUM =" + nudLayerNum.Value.ToString();
            }
            if(cmbOrgOrBlood.SelectedValue!=null&&cmbOrgOrBlood.Text!="")
            {
                layerSql += " AND b.BLOODORORGID =" + cmbOrgOrBlood.SelectedValue.ToString();
            }
            if (cmbSpecType.SelectedValue != null && cmbSpecType.Text != "")
            {
                layerSql += " AND b.SPECTYPEID =" + cmbSpecType.SelectedValue.ToString();
            }
            if (cmbDisType.SelectedValue != null && cmbDisType.Text != "")
            {
                layerSql += " AND b.DISEASETYPEID = " + cmbDisType.SelectedValue.ToString();
            }
            layerSql += " ORDER BY ib.ICEBOXNAME, LAYERNUM";//l.ICEBOXLAYERID
            DataSet ds = new DataSet();
            layerManage.ExecQuery(layerSql, ref ds);
            ///��ѯ������·��� �ı걾����걾�в�������
            QuerySubConCollection(ds);
        }

        private void QuerySubConCollection(DataSet ds)
        {
            try
            {
                if (ds == null || ds.Tables.Count <= 0)
                {
                    return;
                }
                DataTable dtLayer = ds.Tables[0];

                if (dtLayer == null || dtLayer.Rows.Count <= 0)
                {
                    return;
                }
                dtLayer.TableName = "Layer";

                string queryShelfSql =
                       "  SELECT DISTINCT SPEC_SHELF.SHELFID ���к�2,SPEC_SHELF.SHELFBARCODE ������," +
                        " SPEC_SHELF.SHELFCOL ��," +
                        " SPEC_TYPE.SPECIMENTNAME �걾����,SPEC_DISEASETYPE.DISEASENAME ����," +
                        " SPEC_SHELF.OCCUPYCOUNT ռ������,SPEC_SHELF.CAPACITY ����," +
                        " round((CAST(SPEC_SHELF.OCCUPYCOUNT AS DECIMAL(5,2))/SPEC_SHELF.CAPACITY),4)*100 ռ����," +
                        " SPEC_SHELF.ICEBOXLAYERID " +
                        " FROM SPEC_SHELF LEFT JOIN SPEC_ICEBOXLAYER ON SPEC_SHELF.ICEBOXLAYERID =SPEC_ICEBOXLAYER.ICEBOXLAYERID" +
                        " LEFT JOIN SPEC_DISEASETYPE ON SPEC_SHELF.DISTYPEID = SPEC_DISEASETYPE.DISEASETYPEID " +
                        " LEFT JOIN SPEC_TYPE ON SPEC_SHELF.SPECTYPEID = SPEC_TYPE.SpecimentTypeID ";

                string queryBoxSql = "SELECT DISTINCT SPEC_BOX.BOXBARCODE ����, SPEC_BOX.DESCAPROW �������, SPEC_BOX.DESCAPSUBLAYER ���ڸ߶�," +
                                     "TRIM(CHAR(SPEC_BOX_SPEC.SPECROW))||'*'||TRIM(CHAR(SPEC_BOX_SPEC.SPECCOL)) ���, " +
                                     "SPEC_TYPE.SPECIMENTNAME �걾����,SPEC_DISEASETYPE.DISEASENAME ����," +
                                     "round((CAST(SPEC_BOX.OCCUPYCOUNT AS DECIMAL(5,2))/SPEC_BOX.CAPACITY),4)*100 ռ����," +
                                     "SPEC_BOX.DESCAPID, SPEC_BOX.SPECUSE" +
                                     " FROM SPEC_BOX " +
                                     " LEFT JOIN SPEC_BOX_SPEC ON SPEC_BOX.BOXSPECID=SPEC_BOX_SPEC.BOXSPECID" +
                                     " LEFT JOIN SPEC_DISEASETYPE ON SPEC_BOX.DISEASETYPEID=SPEC_DISEASETYPE.DISEASETYPEID" +
                                     " LEFT JOIN SPEC_TYPE ON SPEC_BOX.SPECTYPEID=SPEC_TYPE.SPECIMENTTYPEID";

                DataTable dtShelf = ds.Tables.Add("Shelf");
                DataTable dtSpecBox = ds.Tables.Add("Box");

                dtShelf.Columns.AddRange(new DataColumn[] 
                                    { 
                                        new DataColumn("���к�2", typeof(decimal)), 
                                        new DataColumn("������", typeof(string)),
                                        new DataColumn("��", typeof(decimal)),
                                        new DataColumn("�걾����", typeof(string)),
                                        new DataColumn("����", typeof(string)),
                                        new DataColumn("ռ������", typeof(decimal)),
                                        new DataColumn("����", typeof(decimal)),
                                        new DataColumn("ռ����", typeof(decimal)),
                                        new DataColumn("ICEBOXLAYERID", typeof(decimal))
                                    });
                dtSpecBox.Columns.AddRange(new DataColumn[] 
                                    { 
                                        new DataColumn("����",typeof(string)),
                                        new DataColumn("�������", typeof(decimal)), 
                                        new DataColumn("���ڸ߶�", typeof(decimal)),
                                        new DataColumn("���", typeof(string)),
                                        new DataColumn("�걾����", typeof(string)),
                                        new DataColumn("����", typeof(string)),
                                        new DataColumn("ռ����", typeof(decimal)),
                                        new DataColumn("DESCAPID", typeof(decimal)),
                                        new DataColumn("SPECUSE", typeof(string))
                                    });
                foreach (DataRow dr in dtLayer.Rows)
                {
                    string layerId = dr["���к�1"].ToString();
                    string tmpShelfSql = " where SPEC_SHELF.ICEBOXLAYERID = " + layerId + " order by SPEC_SHELF.SHELFCOL";
                    string sqlShelf = queryShelfSql + tmpShelfSql;                     
                    DataSet tmpDs = new DataSet();
                    shelfManage.ExecQuery(sqlShelf, ref tmpDs);
                    if (tmpDs == null || tmpDs.Tables.Count <= 0)
                    {
                        continue;
                    }
                    if (tmpDs.Tables[0].Rows.Count <= 0)
                    {
                        continue;
                    }
                    dtShelf.Merge(tmpDs.Tables[0]);
                    foreach (DataRow row in tmpDs.Tables[0].Rows)
                    {
                        string shelfId = row["���к�2"].ToString();
                        string tmpBoxSql = " where SPEC_BOX.DESCAPTYPE='J' and SPEC_BOX.DESCAPID =" + shelfId + " order by DESCAPROW,DESCAPSUBLAYER";
                        string boxSql = queryBoxSql + tmpBoxSql;
                         
                        DataSet tmpBoxDs = new DataSet();
                        specBoxManage.ExecQuery(boxSql, ref tmpBoxDs);
                        if (tmpBoxDs == null || tmpBoxDs.Tables.Count <= 0)
                        {
                            continue;
                        }
                        if (tmpBoxDs.Tables[0].Rows.Count <= 0)
                        {
                            continue;
                        }
                        dtSpecBox.Merge(tmpBoxDs.Tables[0]);
                    }
                }
                if (ds.Tables.Count > 0)
                {
                    ds.Relations.Add("Shelf", ds.Tables[0].Columns["���к�1"], dtShelf.Columns["ICEBOXLAYERID"]);
                    ds.Relations.Add("Box", dtShelf.Columns["���к�2"], dtSpecBox.Columns["DESCAPID"]);
                    neuSpread1_Sheet1.AutoGenerateColumns = true;
                    neuSpread1_Sheet1.DataSource = ds;
                    neuSpread1_Sheet1.DataMember = "Layer";
                    for (int i = 0; i < neuSpread1_Sheet1.Columns.Count; i++)
                    {
                        neuSpread1_Sheet1.Columns[i].Width = 80;
                       
                    }
                    
                    FarPoint.Win.Spread.SheetView shelf;

                    for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        shelf = neuSpread1_Sheet1.GetChildView(i, 0);                      
                        shelf.Columns[8].Visible = false;
                        shelf.Columns[0].Visible = false;
                        for (int k = 0; k < shelf.Columns.Count; k++)
                        {
                            shelf.Columns[k].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                            shelf.Columns[k].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        }
                        for (int t = 0; t < shelf.Rows.Count; t++)
                        {
                            FarPoint.Win.Spread.SheetView box = shelf.GetChildView(t, 0);
                            box.DataAutoSizeColumns = false;
                            box.Columns[7].Visible = false;
                            box.Columns[8].Visible = false;
                            box.Columns[0].Width = 100;                            
                            box.Columns[1].Width = 60;
                            box.Columns[2].Width = 60;
                            box.Columns[3].Width = 60;
                            for (int r = 0; r < box.Columns.Count; r++)
                            {
                                box.Columns[r].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                box.Columns[r].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                            }
                            for (int r = 0; r < box.RowCount; r++)
                            {
                                string specialUse = box.Cells[r,8].Text.Trim();
                                if (specialUse == "8")
                                {
                                    box.Rows[r].BackColor = Color.SkyBlue;
                                    shelf.Rows[t].BackColor = Color.SkyBlue;
                                }
                            }
                        }
                    }
                    
                    neuSpread1_Sheet1.Columns[0].Visible = false;
                }
            }
            catch
            {
 
            }
        }
        /// <summary>
        /// �����µļ���
        /// </summary>
        /// <param name="startCol"></param>
        /// <param name="count"></param>
        /// <param name="lastShelfInLayer"></param>
        /// <returns></returns>
        private int InsertShelf(int startCol, int count, Shelf lastShelfInLayer)
        {
             int result = 0;
            try
            {
                for (int i = startCol; i < count + startCol; i++)
                {
                    Shelf tmp = new Shelf();
                    string sequence = "";
                    shelfManage.GetNextSequence(ref sequence);
                    tmp.ShelfID = Convert.ToInt32(sequence);
                    //�����ڱ������
                    tmp.IceBoxLayer.Row = 1;
                    //�����ڱ������
                    tmp.IceBoxLayer.Col = i;
                    //���ڱ����ĵڼ���
                    tmp.IceBoxLayer.Height = 1;
                    tmp.IceBoxLayer.LayerId = newIceBoxLayer.LayerId;
                    if (lastShelfInLayer != null)
                    {
                        tmp.ShelfSpec.ShelfSpecID = lastShelfInLayer.ShelfSpec.ShelfSpecID;
                        string barCode = lastShelfInLayer.SpecBarCode.Substring(0, 5);
                        barCode += i.ToString().PadLeft(2, '0');
                        tmp.SpecBarCode = barCode;
                        tmp.OccupyCount = 0;
                        tmp.Capacity = lastShelfInLayer.Capacity;
                    }
                    if (lastShelfInLayer == null)
                    {
                        tmp.SpecBarCode = newIceBoxLayer.IceBox.IceBoxId.ToString().PadLeft(3, '0') + newIceBoxLayer.LayerNum.ToString().Trim().PadLeft(2, '0') + i.ToString().Trim().PadLeft(2, '0');
                        tmp.ShelfSpec.ShelfSpecID = currentLayer.SpecID;
                        tmp.OccupyCount = 0;
                        ShelfSpec sp = new ShelfSpec();
                        sp = shelfSpecManage.GetShelfByID(tmp.ShelfSpec.ShelfSpecID.ToString());
                        int capacity = 0;
                        if (sp.Col > 0 && sp.Row > 0 && sp.Height > 0)
                        {
                            capacity = sp.Col * sp.Height * sp.Row;
                        }
                        tmp.Capacity = capacity;
                        tmp.IsOccupy = '0';
                    }
                    tmp.IsOccupy = '0';
                    result = shelfManage.InsertShelf(tmp);                    
                }
            }
            catch
            {
                return -1;
            }
            return result;
        }

        /// <summary>
        /// ���¼����к��ӵ���Ϣ
        /// </summary>
        /// <param name="arrShelfInLayer"></param>
        /// <returns></returns>
        private int UpdateSpecBoxInShelf(ArrayList arrShelfInLayer)
        {
            int result = 0;
            try
            {
                foreach (Shelf s in arrShelfInLayer)
                {
                    Shelf tmpShelf = new Shelf();
                    tmpShelf = s;
                    //������־��Ϣ
                    capLogManage.DisuseShelf(tmpShelf, loginPerson.Name, "M","����ܷ���");
                    tmpShelf.IceBoxLayer.Col = 0;
                    tmpShelf.IceBoxLayer.Height = 0;
                    tmpShelf.IceBoxLayer.Row = 0;
                    tmpShelf.IceBoxLayer.LayerId = 0;
                    shelfManage.UpdateShelf(tmpShelf);

                    ArrayList arrSpecBox = new ArrayList();
                    arrIcebox = specBoxManage.GetBoxByCap(s.ShelfID.ToString(), 'J');
                    //���¼����б걾�е���Ϣ
                    foreach (SpecBox box in arrSpecBox)
                    {
                        SpecBox tmp = box;
                        //д����־
                        capLogManage.DisuseSpecBox(tmp, loginPerson.Name, "M");
                        tmp.DesCapCol = 0;
                        tmp.DesCapID = 0;
                        tmp.DesCapRow = 0;
                        tmp.DesCapSubLayer = 0;
                        result = specBoxManage.UpdateSpecBox(tmp);
                    }
                }
            }
            catch
            {
 
            }
            return result;
        }

        /// <summary>
        /// ��������־��Ϣ
        /// </summary>
        /// <param name="arrShelfInLayer"></param>
        /// <returns></returns>
        private int UpdateShelfInfo(ArrayList arrShelfInLayer)
        {
            int result = 0;
            for (int i = 0; i < arrShelfInLayer.Count; i++)
            {
                Shelf slf = new Shelf();
                slf = arrShelfInLayer[i] as Shelf;
                slf.DisTypeId = newIceBoxLayer.DiseaseType.DisTypeID;
                slf.SpecTypeId = newIceBoxLayer.SpecTypeID;
                result = shelfManage.UpdateShelf(slf);
                result = capLogManage.ModifyShelf(arrShelfInLayer[i] as Shelf, loginPerson.Name,"M", slf, "�޸Ķ���ܵı���������Ϣ");
                ArrayList arrSpecBox = new ArrayList();
                arrSpecBox = specBoxManage.GetBoxByCap(slf.ShelfID.ToString(), 'J');
                //���¼����б걾�е���Ϣ
                foreach (SpecBox box in arrSpecBox)
                {
                    SpecBox tmp = box;
                    tmp.DiseaseType.DisTypeID = slf.DisTypeId;
                    tmp.SpecTypeID = slf.SpecTypeId;
                    result = specBoxManage.UpdateSpecBox(tmp);
                    result = capLogManage.ModifyBoxLog(box, loginPerson.Name, "M", tmp, "�޸ı걾�еı���������Ϣ");
                }
            }
            return result;
        }

        /// <summary>
        /// ��ȡ��ǰѡ�е�ShelfId
        /// </summary>
        /// <returns>ShelfId</returns>
        private string SelectedShelfId()
        {
            FarPoint.Win.Spread.SheetView shelf;
            shelf = neuSpread1.Sheets[0].FindChildView(neuSpread1_Sheet1.ActiveRowIndex, 0);
            string shelfId = "";
            if (shelf != null)
            {
                shelfId = shelf.Cells[shelf.ActiveRowIndex, 0].Value.ToString();
                
                //ss1 = ss.GetChildView(ss.ActiveRowIndex, 0);
                //if (ss1 != null)
                //{
                //    string test = ss1.Cells[ss1.ActiveRowIndex, 0].Text + " - " + ss1.Cells[ss1.ActiveRowIndex, 1].Text + " - " + ss1.Cells[ss1.ActiveRowIndex, 2].Text;
                //}
            }
            return shelfId;
        }

        private string SelectedBoxId()
        {
            FarPoint.Win.Spread.SheetView shelf;
            shelf = neuSpread1.Sheets[0].FindChildView(neuSpread1_Sheet1.ActiveRowIndex, 0);
            string boxBarCode = "";
            if (shelf != null)
            {
                FarPoint.Win.Spread.SheetView box;
                box = shelf.GetChildView(shelf.ActiveRowIndex, 0);                
                boxBarCode = box.Cells[box.ActiveRowIndex, 0].Value.ToString();

                //ss1 = shelf.GetChildView(shelf.ActiveRowIndex, 0);
                //if (ss1 != null)
                //{
                //    string test = ss1.Cells[ss1.ActiveRowIndex, 0].Text + " - " + ss1.Cells[ss1.ActiveRowIndex, 1].Text + " - " + ss1.Cells[ss1.ActiveRowIndex, 2].Text;
                //}
            }
            return boxBarCode;
        }

        private void ScanBox()
        {
            try
            {
                FS.HISFC.Models.Speciment.SpecBox tmp = specBoxManage.GetBoxByBarCode(SelectedBoxId());
                if (tmp != null && tmp.BoxBarCode != "" && tmp.OccupyCount > 0)
                {
                    ucSpecDetInBox ucBox = new ucSpecDetInBox();
                    ucBox.BoxId = tmp.BoxId;
                    BoxSpecManage bsM = new BoxSpecManage();
                    FS.HISFC.Models.Speciment.BoxSpec bs = bsM.GetSpecByBoxId(tmp.BoxId.ToString());

                    //Size size = new Size();
                    //size.Height = (bs.Row + 1) * 25 + 90;
                    //size.Width = (bs.Col) * 75 + 50;
                    //ucBox.Size = size;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucBox, FormBorderStyle.FixedSingle, FormWindowState.Normal);
                }    
                ucContainer tmpUc = new ucContainer();
                tmpUc.CurBox = tmp;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(tmpUc, FormBorderStyle.FixedSingle, FormWindowState.Normal);
             }
            catch
            { }
            
        }

        private void ucLayerModify_Load(object sender, EventArgs e)
        {
            BindingSpecClass();
            BindingIceBox();
            BindingDisType();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            cmbOrgOrBlood.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSpecType.DropDownStyle = ComboBoxStyle.DropDown;
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //��ѯ���������б�
            ArrayList iceBoxTypeList = new ArrayList();
            iceBoxTypeList = con.GetList("ICEBOXTYPE");
            this.txtIceBoxType.AddItems(iceBoxTypeList);
        }

        public override int Query(object sender, object neuObject)
        {
            Query();
            return base.Query(sender, neuObject);
        }

        private void cmbOrgOrBlood_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgId = cmbOrgOrBlood.SelectedValue.ToString();
            if (orgId != "-1" && orgId != null)
            {
                BindingSpecType(orgId);
            }
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int rowIndex =neuSpread1_Sheet1.ActiveRowIndex;
            if (rowIndex >= 0 && neuSpread1_Sheet1.Rows.Count > 0)
            {
                currentLayerId = neuSpread1_Sheet1.Cells[rowIndex, 0].Value.ToString();
                if (iceBoxManage.GetIceBoxByLayerID(currentLayerId) == null)
                    return;
                currentLayerSaveType = neuSpread1_Sheet1.Cells[rowIndex, 6].Value.ToString() == "�걾" ? "B" : "J";
                currentLayer = layerManage.GetLayerById(currentLayerId);
                string iceType = neuSpread1_Sheet1.Cells[rowIndex, 2].Value.ToString();
                string iceTypeId = "";
                switch (iceType)
                {
                        //��ʽ�䶳��' WHEN '2' THEN '���¹�' WHEN '3' THEN '
                    case "��ʽ�䶳��":
                        iceTypeId="1";
                        break;
                    case "���¹�":
                        iceTypeId="2";
                        break;
                    case "Һ����":
                        iceTypeId="3";
                        break;
                    default:
                        break;
                }
                panelLayer.Controls.Clear();
                ucLayer = new ucIceBoxLayer(iceTypeId);
                newIceBoxLayer = layerManage.GetLayerById(currentLayerId); 
                panelLayer.Controls.Add(ucLayer);
                ucLayer.SetValue(currentLayer);
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("�޸�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            this.toolBarService.AddToolButton("��Ӷ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("��ӱ걾��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.QȨ�����, true, false, null);
            this.toolBarService.AddToolButton("��ӡ�걾������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("�鿴���ڱ걾", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L���, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ucLayer == null)
                return;
            if (newIceBoxLayer.IceBox.IceBoxId <= 0)
            {
                panelLayer.Controls.Clear();
                return;
            }
            int tmpLayerId = newIceBoxLayer.LayerId;
            switch (e.ClickedItem.Text.Trim())
            {
                case "�޸�":
                    ArrayList arrSubSpec = subSpecManage.GetSubSpecInLayerOrShelf(currentLayerId, currentLayerSaveType);
                    if (arrSubSpec.Count > 0&&arrSubSpec!=null)
                    {
                        MessageBox.Show("�б걾��ţ������޸ģ�", "����������޸�");
                        return;
                    }
                    foreach (Control c in ucLayer.Controls)
                    {
                        if (c.Name == "grpIcebox")
                        {
                            GroupBox grp = c as GroupBox;
                            grp.Enabled = true;
                            foreach (Control g in grp.Controls)
                            {
                                g.Enabled = true;
                                if(g.GetType().ToString().Contains("GroupBox"))
                                {
                                    GroupBox tmp = g as GroupBox;
                                    foreach(Control tmpc in g.Controls)
                                    {
                                        tmpc.Enabled=true;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "��Ӷ����":
                    //ÿһ����Ӷ����ȷ��ȡ�õ������µļ�¼                    
                    newIceBoxLayer = layerManage.GetLayerById(tmpLayerId.ToString());
                    if (newIceBoxLayer.SaveType == 'B')
                    {
                        return;
                    }
                    if (newIceBoxLayer.OccupyCount == newIceBoxLayer.Capacity)
                    {
                        MessageBox.Show("�����������", title);
                        return;
                    }
                    frmSpecShelf frmCurShelf = new frmSpecShelf();
                    frmCurShelf.LayerId = tmpLayerId;
                    frmCurShelf.Show();
                    break;
                case "��ӱ걾��":
                    string shelfId = SelectedShelfId();
                    if (shelfId == null || shelfId == "")
                    {
                        MessageBox.Show("��ѡ��Ŀ��ܣ�", title);
                        return;
                    }
                    Shelf curShelf = shelfManage.GetShelfByShelfId(shelfId, "");
                    if (curShelf.OccupyCount == curShelf.Capacity)
                    {
                        MessageBox.Show("�����������", title);
                        return;
                    }
                    frmSpecBox frmCurBox = new frmSpecBox();
                    frmCurBox.CurShelfId = Convert.ToInt32(shelfId);
                    frmCurBox.Show();
                    break;
                case "��ӡ�걾������":
                    string boxBarCode = SelectedBoxId();
                    SpecBox printBox = specBoxManage.GetBoxByBarCode(boxBarCode);
                    if (printBox == null || printBox.BoxId <= 0)
                    {
                        MessageBox.Show("��ȡ�걾��ʧ��", title);
                        return;
                    }
                    frmBoxPrint frmPrint = new frmBoxPrint();
                    frmPrint.GetBarCode(printBox);
                    frmPrint.Show();
                    break;
                case "�鿴���ڱ걾":
                    ScanBox();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Save(object sender, object neuObject)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();          
            try
            {
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                ucLayer.LayerSaveObj(ref newIceBoxLayer);
                newIceBoxLayer.OccupyCount = 0;
                newIceBoxLayer.IsOccupy = Convert.ToInt16(0);
                layerManage.UpdateLayer(newIceBoxLayer);
                capLogManage.ModifyIceBoxLayer(currentLayer, loginPerson.Name, "M", newIceBoxLayer, "�޸ı���������");

                #region �޸�ǰ���Ǽ���ʱ
                if (currentLayer.SaveType == 'J'&& newIceBoxLayer.SaveType == 'J')
                {
                    Shelf tmpShelf = new Shelf();
                    ArrayList arrShelfInLayer = new ArrayList();
                    arrShelfInLayer = shelfManage.GetShelfByLayerID(currentLayer.LayerId.ToString());
                    if (arrShelfInLayer.Count > 0)
                    {
                        tmpShelf = arrShelfInLayer[0] as Shelf;
                        if (tmpShelf.ShelfSpec.ShelfSpecID != newIceBoxLayer.SpecID)
                        {
                            //���¼����к��ӵ���Ϣ
                            UpdateSpecBoxInShelf(arrShelfInLayer);
                        }
                        if (tmpShelf.ShelfSpec.ShelfSpecID == newIceBoxLayer.SpecID)
                        {
                            DialogResult dialog = MessageBox.Show("�����´˲��е����ж���ܼ��걾�д�ŵı걾���ͼ�������Ϣ��", title, MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.No)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                return 0;
                            }
                            else
                            {
                                UpdateShelfInfo(arrShelfInLayer);
                            }
                        }
                    }
                    //�ڲ�����Ӽ��ӣ�����ͬһ���м��ӵĸ���
                    #region
                    //int maxCol = 1;
                    //if (tmpShelf.ShelfSpec.ShelfSpecID == newIceBoxLayer.SpecID)
                    //{
                        //Shelf lastShelfInLayer = new Shelf();
                        //foreach (Shelf s in arrShelfInLayer)
                        //{
                        //    if (s.IceBoxLayer.Col > maxCol)
                        //    {
                        //        maxCol = s.IceBoxLayer.Col;
                        //        lastShelfInLayer = s;
                        //    }
                        //}
                        //���¼����к��ӵ���Ϣ
                       // UpdateSpecBoxInShelf(arrShelfInLayer);
                        //Ҫ�������ݿ��м��ӵĸ���
                        //int shelfCount = newIceBoxLayer.Col - arrShelfInLayer.Count;
                        //InsertShelf(maxCol + 1, shelfCount,lastShelfInLayer);
                    // }
                    
                    //if (newIceBoxLayer.SpecID != tmpShelf.ShelfSpec.ShelfSpecID)
                    //{
                    //    foreach (Shelf s in arrShelfInLayer)
                    //    {
                    //        Shelf tmp = new Shelf();
                    //        tmp = s;
                    //        //������־��Ϣ
                    //        capLogManage.DisuseShelf(tmp, loginPerson.Name, "M");
                    //        tmp.IceBoxLayer.Col = 0;
                    //        tmp.IceBoxLayer.Height = 0;
                    //        tmp.IceBoxLayer.Row = 0;
                    //        tmp.IceBoxLayer.LayerId = 0;
                    //        shelfManage.UpdateShelf(tmp);
                    //    }
                        //InsertShelf(1, newIceBoxLayer.Col, null);
                    //}
                    #endregion
                }
        
                #endregion

                #region �޸�ǰ���ŵĶ��Ǳ걾
                if (newIceBoxLayer.SaveType == 'B' && currentLayer.SaveType == 'B')
                {
                    ArrayList arrSpecBox = new ArrayList();
                    arrIcebox = specBoxManage.GetBoxByCap(currentLayer.LayerId.ToString(), 'B');
                    if (arrIcebox.Count > 0)
                    {
                        //���һ��,һ���򲻴���
                        if (newIceBoxLayer.SpecID != currentLayer.SpecID)
                        {
                            foreach (SpecBox s in arrIcebox)
                            {
                                capLogManage.DisuseSpecBox(s, loginPerson.Name, "M");
                            }
                            return 0;
                        }
                        if (newIceBoxLayer.SpecID == currentLayer.SpecID)
                        {
                            DialogResult dialog = MessageBox.Show("�����´˲��е����ж���ܼ��걾�д�ŵı걾���ͼ�������Ϣ��", title, MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.No)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                return 0;
                            }
                            else
                            {
                                foreach (SpecBox s in arrIcebox)
                                {
                                    SpecBox tmpBox = s;
                                    tmpBox.SpecTypeID = newIceBoxLayer.SpecTypeID;
                                    tmpBox.DiseaseType = newIceBoxLayer.DiseaseType;
                                    tmpBox.OrgOrBlood = Convert.ToInt32(newIceBoxLayer.BloodOrOrgId);
                                    specBoxManage.UpdateSpecBox(tmpBox);
                                    capLogManage.ModifyBoxLog(s, loginPerson.Name, "M", tmpBox, "�޸ı걾�б���������Ϣ");
                                }
                            }
                        }
                    }
                }
                #endregion

                #region �޸�ǰ��һ��ʱ
                if (currentLayer.SaveType == 'J' && newIceBoxLayer.SaveType == 'B')
                {
                    ArrayList arrShelfInLayer = new ArrayList();
                    arrShelfInLayer = shelfManage.GetShelfByLayerID(currentLayer.LayerId.ToString());                    
                    UpdateSpecBoxInShelf(arrShelfInLayer);
                }

                if (currentLayer.SaveType == 'B' && newIceBoxLayer.SaveType == 'J')
                {
                    ArrayList arrSpecBox = new ArrayList();
                    arrIcebox = specBoxManage.GetBoxByCap(currentLayer.LayerId.ToString(), 'B');
                    foreach (SpecBox s in arrIcebox)
                    {
                        capLogManage.DisuseSpecBox(s, loginPerson.Name, "M");
                    }
                    //InsertShelf(1, newIceBoxLayer.Col, null);
                }
                #endregion
                this.Query(null, null);
                FS.FrameWork.Management.PublicTrans.Commit();                
                MessageBox.Show("�޸ĳɹ���", title);              
                //FS.FrameWork.Management.PublicTrans.RollBack();
            }
        

            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�޸�ʧ�ܣ�", title);      
            }
            return base.Save(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "��ѯ���������ѡ��Excel�ļ�����λ��";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "����";
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                //this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveBothCustomRowAndColumnHeaders);
                 
                 this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);                
                
            }
            return base.Export(sender, neuObject);
        }
    }
}
