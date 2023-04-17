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

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucShelfModify : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ҵ��������
        /// </summary>
        private IceBoxLayerManage layerManage;        
        private IceBoxManage iceBoxManage;
        private ShelfManage shelfManage;
        private SubSpecManage subSpecManage;
        private CapLogManage capLogManage;
        private SpecBoxManage specBoxManage;

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private Shelf curShelf;

        private string title;
        private string querySql;
        private DataTable dtResult;

        public ucShelfModify()
        {
            InitializeComponent();

            layerManage = new IceBoxLayerManage();
            iceBoxManage = new IceBoxManage();
            subSpecManage = new SubSpecManage();
            capLogManage=new CapLogManage();
            specBoxManage=new SpecBoxManage();
            shelfManage = new ShelfManage();
            
            loginPerson = new FS.HISFC.Models.Base.Employee();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            curShelf = new Shelf();
            title = "������޸�";
            dtResult = new DataTable();

        }

        /// <summary>
        /// �󶨱걾����
        /// </summary>
        private void BindingSpecClass()
        {
            Dictionary<int, string> dicOrgType = new Dictionary<int, string>();
            OrgTypeManage orgTypeManage = new OrgTypeManage();
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
        /// �󶨱걾����
        /// </summary>
        /// <param name="orgId"></param>
        private void BindingSpecType(string orgId)
        {
            Dictionary<int, string> dicSpecType = new Dictionary<int, string>();
            SpecTypeManage specTypeManage = new SpecTypeManage();
            dicSpecType.Clear();
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
        /// �������͵İ�
        /// </summary>
        private void DisTypeBinding()
        {
            DisTypeManage disTypeManage = new DisTypeManage();
            Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            dicDisType.Add(-1, "");
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDiseaseType.DisplayMember = "Value";
                cmbDiseaseType.ValueMember = "Key";
                cmbDiseaseType.DataSource = bs;
                cmbDiseaseType.SelectedValue = -1;
            }
        }

        private void QuerySubConCollection(DataSet ds)
        {
            try
            {
                if (ds == null || ds.Tables.Count <= 0)
                {
                    return;
                }
                DataTable dtShelf = ds.Tables[0];

                if (dtShelf == null || dtShelf.Rows.Count <= 0)
                {
                    return;
                }
                dtShelf.TableName = "Shelf";
           
                string queryBoxSql = "SELECT DISTINCT SPEC_BOX.BOXBARCODE ����, SPEC_BOX.DESCAPROW �������, SPEC_BOX.DESCAPSUBLAYER ���ڸ߶�," +
                                     "TRIM(CHAR(SPEC_BOX_SPEC.SPECROW))||'*'||TRIM(CHAR(SPEC_BOX_SPEC.SPECCOL)) ���, " +
                                     "SPEC_TYPE.SPECIMENTNAME �걾����,SPEC_DISEASETYPE.DISEASENAME ����," +
                                     "round((CAST(SPEC_BOX.OCCUPYCOUNT AS DECIMAL(5,2))/SPEC_BOX.CAPACITY),4)*100 ռ����," +
                                     "SPEC_BOX.DESCAPID" +
                                     " FROM SPEC_BOX " +
                                     " LEFT JOIN SPEC_BOX_SPEC ON SPEC_BOX.BOXSPECID=SPEC_BOX_SPEC.BOXSPECID" +
                                     " LEFT JOIN SPEC_DISEASETYPE ON SPEC_BOX.DISEASETYPEID=SPEC_DISEASETYPE.DISEASETYPEID" +
                                     " LEFT JOIN SPEC_TYPE ON SPEC_BOX.SPECTYPEID=SPEC_TYPE.SPECIMENTTYPEID";

                DataTable dtSpecBox = ds.Tables.Add("Box");

                dtSpecBox.Columns.AddRange(new DataColumn[] 
                                    { 
                                        new DataColumn("����",typeof(string)),
                                        new DataColumn("�������", typeof(decimal)), 
                                        new DataColumn("���ڸ߶�", typeof(decimal)),
                                        new DataColumn("���", typeof(string)),
                                        new DataColumn("�걾����", typeof(string)),
                                        new DataColumn("����", typeof(string)),
                                        new DataColumn("ռ����", typeof(decimal)),
                                        new DataColumn("DESCAPID", typeof(decimal))
                                    });
                    foreach (DataRow row in dtShelf.Rows)
                    {
                        string shelfId = row["���к�"].ToString();
                        string tmpBoxSql = " where  SPEC_BOX.DESCAPTYPE='J' and SPEC_BOX.DESCAPID =" + shelfId + " order by DESCAPROW,DESCAPSUBLAYER";
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

                    if (ds.Tables.Count > 0)
                    {
                        ds.Relations.Add("Box", dtShelf.Columns["���к�"], dtSpecBox.Columns["DESCAPID"]);
                        neuSpread1_Sheet1.AutoGenerateColumns = true;
                        neuSpread1_Sheet1.DataSource = ds;
                        neuSpread1_Sheet1.DataMember = "Shelf";
                        for (int i = 0; i < neuSpread1_Sheet1.Columns.Count; i++)
                        {
                            neuSpread1_Sheet1.Columns[i].Width = 120;
                        }

                        for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                        {
                            neuSpread1_Sheet1.Rows[i].Height = 30;
                            FarPoint.Win.Spread.SheetView box = neuSpread1_Sheet1.GetChildView(i, 0);
                            box.DataAutoSizeColumns = false;
                            box.Columns[7].Visible = false;
                            
                            for (int r = 0; r < box.Columns.Count; r++)
                            {
                                box.Columns[r].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                box.Columns[r].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                box.Columns[r].Width = 120;
                            }
                            box.Columns[0].Width = 150; 
                            for (int k = 0; k < box.Rows.Count; k++)
                            {
                                box.Rows[k].Height = 30;
                            }
                        }

                        neuSpread1_Sheet1.Columns[0].Visible = false;
                        neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].AllowAutoSort = true;
                        neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].AllowAutoFilter = true;
                    }
            }
            catch
            {

            }
        }

        private void Query()
        {
            neuSpread1_Sheet1.RowCount = 0;
            querySql = " SELECT DISTINCT SPEC_SHELF.SHELFID ���к�,SPEC_SHELF.SHELFBARCODE ������, SPEC_ICEBOX.ICEBOXNAME ���ڱ�������," +
                     " CASE SPEC_ICEBOX.ICEBOXTYPEID WHEN '1' THEN '��ʽ�䶳��' WHEN '2' THEN '���¹�' WHEN '3' THEN 'Һ����' END ��������, " +
                     " SPEC_ICEBOXLAYER.LAYERNUM λ�ڵڼ���," +
                     " SPEC_SHELF.SHELFCOL ��," +
                     " SPEC_TYPE.SPECIMENTNAME �걾����,SPEC_DISEASETYPE.DISEASENAME ����," +
                     " SPEC_SHELF.OCCUPYCOUNT ռ������,SPEC_SHELF.CAPACITY ����," +
                     " round((CAST(SPEC_SHELF.OCCUPYCOUNT AS DECIMAL(5,2))/SPEC_SHELF.CAPACITY),4)*100 \"(ռ����%)\"" +
                     " FROM SPEC_SHELF LEFT JOIN SPEC_ICEBOXLAYER ON SPEC_SHELF.ICEBOXLAYERID = SPEC_ICEBOXLAYER.ICEBOXLAYERID" +
                     " LEFT JOIN SPEC_ICEBOX  ON SPEC_ICEBOX.ICEBOXID = SPEC_ICEBOXLAYER.ICEBOXID " +
                     " LEFT JOIN SPEC_DISEASETYPE ON SPEC_SHELF.DISTYPEID = SPEC_DISEASETYPE.DISEASETYPEID " +
                     " LEFT JOIN SPEC_TYPE ON SPEC_SHELF.SPECTYPEID = SPEC_TYPE.SpecimentTypeID ";
            if (txtBarCode.Text.Trim() == "")
            {
                querySql +=
                    " left join SPEC_BOX b on SPEC_SHELF.SHELFID = b.DESCAPID" +
                    " WHERE b.DESCAPTYPE='J' ";
                if (txtIceBoxType.Tag != null && txtIceBoxType.Text != "")
                {
                    querySql += " AND SPEC_ICEBOX.ICEBOXTYPEID = '" + txtIceBoxType.Tag.ToString() + "'";
                }
                if (cmbOrgOrBlood.SelectedValue != null && cmbOrgOrBlood.Text != "")
                {
                    querySql += " AND b.BLOODORORGID =" + cmbOrgOrBlood.SelectedValue.ToString();
                }
                if (cmbSpecType.SelectedValue != null && cmbSpecType.Text != "")
                {
                    querySql += " AND b.SPECTYPEID =" + cmbSpecType.SelectedValue.ToString();
                }
                if (cmbDiseaseType.SelectedValue != null && cmbDiseaseType.Text != "")
                {
                    querySql += " AND b.DISEASETYPEID=" + cmbDiseaseType.SelectedValue.ToString();
                }
                querySql += " ORDER BY ���ڱ�������, LAYERNUM ,SHELFCOL";

            }
            else
            {
                querySql += " where SPEC_SHELF.SHELFBARCODE LIKE '%" + txtBarCode.Text.Trim() + "%'";
            } 
          
            DataSet ds = new DataSet();
            shelfManage.ExecQuery(querySql, ref ds);
            
            if (ds.Tables.Count > 0)
            {
                dtResult = new DataTable();
                dtResult = ds.Tables[0];
                QuerySubConCollection(ds);
            }
        }

        /// <summary>
        /// �Ƴ������
        /// </summary>
        private void RemoveShelf()
        {
            OperShelf tmp = new OperShelf();
            tmp.RemoveShelf(ref curShelf);
            this.Query();
            #region ����OperShelf����
            //IceBoxLayerManage tmpLayerManage = new IceBoxLayerManage();
            //FS.NFC.Management.Transaction trans = new FS.NFC.Management.Transaction(FS.NFC.Management.Connection.Instance);
            //trans.BeginTransaction();
            //try
            //{
            //    capLogManage.SetTrans(trans.Trans);
            //    shelfManage.SetTrans(trans.Trans);
            //    tmpLayerManage.SetTrans(trans.Trans);
            //    specBoxManage.SetTrans(trans.Trans);

            //    Shelf tmpShelf = new Shelf();
            //    tmpShelf = curShelf.Clone();
            //    //д����־
            //    if (capLogManage.DisuseShelf(tmpShelf, loginPerson.Name, "D", "�Ƴ������") == -1)
            //    {
            //        trans.RollBack();
            //        MessageBox.Show("����ʧ�ܣ�", title);
            //        return;
            //    }
            //    tmpShelf.Comment = "�Ƴ�";
            //    tmpShelf.IceBoxLayer.Row = 0;
            //    tmpShelf.IceBoxLayer.Col = 0;
            //    tmpShelf.IceBoxLayer.LayerId = 0;
            //    tmpShelf.IceBoxLayer.Height = 0;
            //    tmpShelf.IsOccupy = '0';
            //    tmpShelf.OccupyCount = 0;
            //    //���¼���
            //    if (shelfManage.UpdateShelf(tmpShelf) == -1)
            //    {
            //        trans.RollBack();
            //        MessageBox.Show("����ʧ�ܣ�", title);
            //        return;
            //    }
            //    IceBoxLayer tmpLayer = tmpLayerManage.GetLayerById(curShelf.IceBoxLayer.LayerId.ToString());
            //    if (tmpLayer == null)
            //    {
            //        trans.RollBack();
            //        MessageBox.Show("����ʧ�ܣ�", title);
            //        return;
            //    }
            //    int occupyCount = tmpLayer.OccupyCount - 1;
            //    if (occupyCount <= 0)
            //        occupyCount = 0;
            //    if (tmpLayerManage.UpdateOccupy("0", tmpLayer.LayerId.ToString()) <= 0)
            //    {
            //        trans.RollBack();
            //        MessageBox.Show("����ʧ�ܣ�", title);
            //        return;
            //    }
            //    if (tmpLayerManage.UpdateOccupyCount(occupyCount.ToString(), tmpLayer.LayerId.ToString()) <= 0)
            //    {
            //        trans.RollBack();
            //        MessageBox.Show("����ʧ�ܣ�", title);
            //        return;
            //    }
            //    ArrayList arrBox = new ArrayList();
            //    arrBox = specBoxManage.GetBoxByCap(curShelf.ShelfID.ToString(), 'J');
            //    foreach (SpecBox sb in arrBox)
            //    {
            //        SpecBox tmp = new SpecBox();
            //        tmp = sb;
            //        tmp.OccupyCount = 0;
            //        tmp.DesCapCol = 0;
            //        tmp.DesCapID = 0;
            //        tmp.DesCapRow = 0;
            //        tmp.DesCapSubLayer = 0;
            //        tmp.DesCapType = '0';
            //        tmp.IsOccupy = '0';
            //        tmp.OccupyCount = 0;
                    
            //        if (specBoxManage.UpdateSpecBox(tmp) == -1)
            //        {
            //            trans.RollBack();
            //            MessageBox.Show("����ʧ��!", title);
            //            return;
            //        }
            //        capLogManage.ModifyBoxLog(sb, loginPerson.Name, "M", tmp, "�Ƴ��걾��");
            //    }
            //    trans.Commit();
            //    MessageBox.Show("�����ɹ�!", title);
            //    Query();
            //}
            //catch
            //{
            //    trans.RollBack();
            //    MessageBox.Show("����ʧ�ܣ�", title);
            //}
            #endregion
        }

        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return base.Query(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("�޸�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            this.toolBarService.AddToolButton("��ӱ걾��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("�Ƴ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (curShelf == null)
                return;
            //ArrayList arrSubSpec;
            if (curShelf.IceBoxLayer.LayerId <= 0)
                return;
            switch (e.ClickedItem.Text.Trim())
            {
                case "�޸�":
                    OperShelf s = new OperShelf();
                    s.ModifyShelf(ref curShelf);
                    //arrSubSpec = new ArrayList();
                    //arrSubSpec = subSpecManage.GetSubSpecInLayerOrShelf(curShelf.ShelfID.ToString(),"IJ");
                    //if (arrSubSpec.Count > 0 && arrSubSpec != null)
                    //{
                    //    MessageBox.Show("�б걾��ţ������޸ģ�", title);
                    //    return;
                    //}
                    //IceBoxLayerManage iceboxMangeTmp = new IceBoxLayerManage();
                    //IceBoxLayer layer = new IceBoxLayer();
                    //layer = iceboxMangeTmp.GetLayerById(curShelf.IceBoxLayer.LayerId.ToString());

                    //int specTypeId = layer.SpecTypeID;
                    //int disTypeId = layer.DiseaseType.DisTypeID;
                    //if (specTypeId != 9 && disTypeId != 16)
                    //{
                    //    MessageBox.Show("�����޸ģ�", title);
                    //    return;
                    //}                    
                    //frmSpecShelf frmModifyShelf = new frmSpecShelf();
                    //frmModifyShelf.ShelfFromModify = curShelf;
                    //frmModifyShelf.Show();
                    break;
                case "��ӱ걾��":
                    if (curShelf.OccupyCount == curShelf.Capacity)
                    {
                        MessageBox.Show("�����������", title);
                        return;
                    }
                    frmSpecBox frmspecBox = new frmSpecBox();
                    frmspecBox.CurShelfId = curShelf.ShelfID;
                    frmspecBox.Show();
                    break;

                case "�Ƴ������":                   
                   
                    RemoveShelf();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void cmbOrgOrBlood_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgId = cmbOrgOrBlood.SelectedValue.ToString();
            if (orgId != "-1" && orgId != null)
            {
                BindingSpecType(orgId);
            }
        }

        private void ucShelfModify_Load(object sender, EventArgs e)
        {
            BindingSpecClass();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            cmbOrgOrBlood.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSpecType.DropDownStyle = ComboBoxStyle.DropDown;
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //��ѯ���������б�
            ArrayList iceBoxTypeList = new ArrayList();
            iceBoxTypeList = con.GetList("ICEBOXTYPE");
            this.txtIceBoxType.AddItems(iceBoxTypeList);
            DisTypeBinding();
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int rowIndex = neuSpread1_Sheet1.ActiveRowIndex;
            int curShelfId;
            if (rowIndex >= 0 && neuSpread1_Sheet1.Rows.Count > 0)
            {
                curShelfId = Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIndex, 0].Value.ToString());
                curShelf = shelfManage.GetShelfByShelfId(curShelfId.ToString(), "");         
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            this.Query();
        }
 
    }
}
