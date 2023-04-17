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
    public partial class ucTransferStore : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private IceBoxManage iceBoxManage;
        private IceBoxLayerManage layerManage;
        private ShelfManage shelfManage;
        private SpecBoxManage boxManage;
        private ArrayList arrBox;
        private BoxSpecManage boxSpecManage;
        private ShelfSpecManage shelfSpecManage;
        private ArrayList arrNewBoxLocate;
        private List<string> oldLocateList;
        private List<string> newLocateList;
        private List<Shelf> oldShelfList;
        private List<Shelf> newShelfList;
       
        private string shelfId = "";
        private string layerId = "";
        private string iceboxId = "";

        private System.Data.IDbTransaction trans;

        public ucTransferStore()
        {
            InitializeComponent();
            iceBoxManage = new IceBoxManage();
            layerManage = new IceBoxLayerManage();
            shelfManage = new ShelfManage();
            boxManage = new SpecBoxManage();
            arrBox = new ArrayList();
            boxSpecManage = new BoxSpecManage();
            shelfSpecManage = new ShelfSpecManage();
            arrNewBoxLocate = new ArrayList();
            oldLocateList = new List<string>();
            newLocateList = new List<string>();
            oldShelfList = new List<Shelf>();
            newShelfList = new List<Shelf>();           

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

        private void GetBoxList()
        {
            iceboxId = "";
            layerId = "";
            shelfId = "";
            string sql = "select distinct spec_box.*\n" +
                         " from spec_box inner join spec_shelf on spec_box.descapid = SPEC_SHELF.shelfid\n" +
                         " inner join spec_iceboxlayer on spec_shelf.iceboxlayerid = SPEC_ICEBOXLAYER.iceboxlayerid \n" +
                         " inner join spec_icebox on spec_iceboxlayer.iceboxid = spec_icebox.iceboxid\n" +
                         " where spec_box.boxid>0 ";
            if (cmbIceBox.SelectedValue != null && cmbIceBox.Text.Trim() != "")
            {
                iceboxId = cmbIceBox.SelectedValue.ToString();
                sql += " AND spec_icebox.iceboxid = " + iceboxId;
                
            }
            if (cmbLayer.SelectedValue != null && cmbLayer.Text.Trim() != "")
            {
                layerId = cmbLayer.SelectedValue.ToString();
                sql += " AND spec_iceboxlayer.iceboxlayerid = " + layerId;
            }
            if (cmbShelf.SelectedValue != null && cmbShelf.Text.Trim() != "")
            {
                shelfId = cmbShelf.SelectedValue.ToString();
                sql += " AND  SPEC_SHELF.shelfid = " + shelfId;
            }

            sql += " order by spec_box.boxid";
            arrBox = boxManage.GetBoxBySql(sql); 
        }

        /// <summary>
        /// �����걾�е�λ����Ϣ
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ParseBoxCode(SpecBox box, char type)
        {
            string tmpBarCode = box.BoxBarCode;
            string boxId = tmpBarCode.Substring(2, 3);
            string otherInfo = tmpBarCode.Substring(6);
            string parseResult = "";
            parseResult = iceBoxManage.GetIceBoxById(boxId).IceBoxName + " ";
            if (type == 'J')
            {
                parseResult += otherInfo ;
                //�㣬�У���               

            }
            if (type == 'B')
            {
                parseResult += tmpBarCode.Substring(3, 2) + "-";
                //�У��У���
                parseResult += box.DesCapRow.ToString() + "-";
                parseResult += box.DesCapCol.ToString() + "-";
                parseResult += box.DesCapSubLayer.ToString();
            }
            return parseResult;
        }

        private void GenerateBarCode(SpecBox box,string shelfBarCode, int shelfId)
        {
            box.DesCapType = 'J';
            string boxBarCode = shelfBarCode + "-";// +currentShelf.IceBoxLayer.Col.ToString().PadLeft(2, '0');
            boxBarCode += box.DesCapSubLayer.ToString();
            boxBarCode += box.DesCapRow.ToString();
            //boxBarCode += box.DesCapCol.ToString().PadLeft(2, '0');
            box.BoxBarCode = boxBarCode;
            box.DesCapID = shelfId;
            arrNewBoxLocate.Add(box);
            newLocateList.Add(ParseBoxCode(box, 'J'));
        }

        /// <summary>
        /// ��λ�걾��
        /// </summary>
        private void BoxLocate(SpecBox box, Shelf currentShelf)
        {
            try
            {
                BoxSpec boxSpec = boxSpecManage.GetSpecById(box.BoxSpec.BoxSpecID.ToString());
                box.Capacity = boxSpec.Row * boxSpec.Col;

                //currentShelf = shelfManage.GetShelf(disTypeid.ToString(), boxSpecId.ToString(), specTypeId.ToString(), box.DesCapID.ToString());
                //if (currentShelf == null || currentShelf.ShelfID <= 0)
                //{
                //    MessageBox.Show("û���ҵ�����ܴ�ű걾�У����������ã�", title);
                //    return 0;
                //}
                SpecBox lastSpecBox = new SpecBox();
                ShelfSpec shelfSpec = new ShelfSpec();
                //lastSpecBox = currentShelf.LastSpecBox;
                lastSpecBox = boxManage.ShelfGetLastCapBox(currentShelf.ShelfID.ToString());
                shelfSpec = shelfSpecManage.GetShelfByShelf(currentShelf.ShelfID.ToString());
                //��ȡ��ǰʹ�ü��ӵĹ���У��У���
                int shelfCol = shelfSpec.Col;
                int shelfRow = shelfSpec.Row;
                int shelfHeight = shelfSpec.Height;
                //��ȡ������������ڵ��У��У���
                int lastCol = lastSpecBox.DesCapCol;
                int lastRow = lastSpecBox.DesCapRow;
                int lastHeight = lastSpecBox.DesCapSubLayer;
                if (lastCol < shelfCol)
                {
                    box.DesCapCol = lastCol + 1;
                    box.DesCapRow = lastRow;
                    box.DesCapSubLayer = lastHeight;
                    //currentShelf.LastSpecBox = box;
                    return;
                }
                if (lastCol == shelfCol && lastRow < shelfRow)
                {
                    box.DesCapCol = 1;
                    box.DesCapRow = lastRow + 1;
                    box.DesCapSubLayer = lastHeight;
                    //currentShelf.LastSpecBox = box;
                    return;
                }
                if (lastCol == shelfCol && lastRow == shelfRow && lastHeight < shelfHeight)
                {
                    box.DesCapCol = 1;
                    box.DesCapRow = 1;
                    box.DesCapSubLayer = lastSpecBox.DesCapSubLayer + 1;
                    //currentShelf.LastSpecBox = box;
                    return;
                }               
            }
            catch
            { }
        }

        /// <summary>
        /// ��ʾλ����Ϣ
        /// </summary>
        private void CheckNewLoate()
        {
            try
            {
                DataTable dtNewLocate = new DataTable();

                DataColumn dcBoxId = new DataColumn();
                dcBoxId.DataType = System.Type.GetType("System.Int32");
                dcBoxId.ColumnName = "���к�";
                dtNewLocate.Columns.Add(dcBoxId);

                DataColumn dcOldBarCode = new DataColumn();
                dcOldBarCode.DataType = System.Type.GetType("System.String");
                dcOldBarCode.ColumnName = "������";
                dtNewLocate.Columns.Add(dcOldBarCode);

                DataColumn dcOldLocate = new DataColumn();
                dcOldLocate.DataType = System.Type.GetType("System.String");
                dcOldLocate.ColumnName = "��λ����Ϣ";
                dtNewLocate.Columns.Add(dcOldLocate);

                DataColumn dcNewBarCode = new DataColumn();
                dcNewBarCode.DataType = System.Type.GetType("System.String");
                dcNewBarCode.ColumnName = "������";
                dtNewLocate.Columns.Add(dcNewBarCode);

                DataColumn dcNewLoate = new DataColumn();
                dcNewLoate.DataType = System.Type.GetType("System.String");
                dcNewLoate.ColumnName = "��λ����Ϣ";
                dtNewLocate.Columns.Add(dcNewLoate);

                for (int i = 0; i < arrBox.Count; i++)
                {
                    DataRow dr = dtNewLocate.NewRow();
                    dr["���к�"] = (arrBox[i] as SpecBox).BoxId.ToString();
                    dr["������"] = (arrBox[i] as SpecBox).BoxBarCode;
                    dr["��λ����Ϣ"] = oldLocateList[i];
                    dr["������"] = (arrNewBoxLocate[i] as SpecBox).BoxBarCode;
                    dr["��λ����Ϣ"] = newLocateList[i];
                    dtNewLocate.Rows.Add(dr);
                }

                neuSpread1_Sheet1.DataSource = null;
                neuSpread1_Sheet1.DataSource = dtNewLocate;
            }
            catch
            { }
        }

        /// <summary>
        /// ���±걾����Ϣ���ڲ鿴��ť�¼���ʹ��
        /// </summary>
        /// <param name="box">��ǰ�걾��</param>
        /// <param name="curShelf">�걾�����ڼ���</param>
        private void FindNewShelf(SpecBox box, Shelf curShelf)
        {          
            SpecBox tmpBox = box.Clone();
            oldLocateList.Add(ParseBoxCode(box, 'J'));

            //�ɼ��Ӽ���
            Shelf oldS = new Shelf();
            oldS = shelfManage.GetShelfByShelfId(box.DesCapID.ToString(), "");
            oldShelfList.Add(oldS);

            //�¼��Ӽ���
            newShelfList.Add(curShelf);
            if (curShelf == null || curShelf.ShelfID <= 0)
            {
                tmpBox.BoxBarCode = "";
                arrNewBoxLocate.Add(tmpBox);
                newLocateList.Add("");
                return;
                //MessageBox.Show("û���ҵ�����ܴ�ű걾�У����������ã�", title);
                ////    return 0;
            }  
            //BoxLocate(tmpBox, ts);
            BoxLocate(tmpBox, curShelf);
            tmpBox.DesCapID = curShelf.ShelfID;
            GenerateBarCode(tmpBox, curShelf.SpecBarCode, curShelf.ShelfID);
            //ts.OccupyCapacity = ts.OccupyCapacity + 1;
            int result = boxManage.UpdateSpecBox(tmpBox);
            int occupyCount = curShelf.OccupyCount + 1;
            shelfManage.UpdateOccupyCount(occupyCount.ToString(), curShelf.ShelfID.ToString());
            //Shelf test = shelfManage.GetShelfByShelfId(curShelf.ShelfID.ToString(), "");
            if (occupyCount == curShelf.Capacity)
            {
                shelfManage.UpdateIsFull("1", curShelf.ShelfID.ToString());
            }            
        }        

        private void ucTransferStore_Load(object sender, EventArgs e)
        {
            BindingIceBox();
        }

        private void btnOk_Click(object sender, EventArgs e)
        { 
            GetBoxList();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            shelfSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (arrBox != null)
                {
                    arrNewBoxLocate = new ArrayList();
                    oldShelfList.Clear();
                    newShelfList.Clear();
                    oldLocateList.Clear();
                    newLocateList.Clear();

                    if (shelfId != "")
                    {
                        foreach (SpecBox box in arrBox)
                        {
                            Shelf curShelf = new Shelf();
                            curShelf = shelfManage.GetShelf(box.DiseaseType.DisTypeID.ToString(), box.BoxSpec.BoxSpecID.ToString(), box.SpecTypeID.ToString(), shelfId);
                            if (curShelf == null)
                            {
                                Shelf tmp = shelfManage.GetShelfByShelfId(box.DesCapID.ToString(),"");
                                if (tmp != null)
                                {
                                    curShelf = shelfManage.GetShelf(tmp.DisTypeId.ToString(), box.BoxSpec.BoxSpecID.ToString(), tmp.SpecTypeId.ToString(), shelfId);
                                }
                            }
                            FindNewShelf(box, curShelf);                             
                        }
                        CheckNewLoate();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    if (layerId != "")
                    {
                        foreach (SpecBox box in arrBox)
                        {
                            Shelf curShelf = new Shelf();
                            ArrayList arrTmp = shelfManage.GetShelfNotInOneLayer(box.DiseaseType.DisTypeID.ToString(), box.BoxSpec.BoxSpecID.ToString(), box.SpecTypeID.ToString(), layerId);
                            if (arrTmp.Count > 0)
                            {
                                curShelf = arrTmp[0] as Shelf;
                            }
                            FindNewShelf(box, curShelf);
                        }
                        CheckNewLoate();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    if (iceboxId != "")
                    {
                        foreach (SpecBox box in arrBox)
                        {
                            Shelf curShelf = new Shelf();
                            ArrayList arrTmp = shelfManage.GetShelfNotInOneIceBox(box.DiseaseType.DisTypeID.ToString(), box.BoxSpec.BoxSpecID.ToString(), box.SpecTypeID.ToString(), iceboxId);
                            if (arrTmp.Count > 0)
                            {
                                curShelf = arrTmp[0] as Shelf;
                            }
                            FindNewShelf(box, curShelf);
                        }
                        CheckNewLoate();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                }                
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); 
                MessageBox.Show("����ʧ�ܣ�", "�ƿ�");
                return;
            }
        }

        private void cmbIceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIceBox.SelectedValue != null && cmbIceBox.Text.Trim()!="")
            {
                BindingLayer(cmbIceBox.SelectedValue.ToString()); 
            }
        }

        private void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLayer.SelectedValue != null && cmbLayer.Text.Trim()!="")
            {
                BindingShelf(cmbLayer.SelectedValue.ToString());
            }
        }

        public override int Save(object sender, object neuObject)
        {
            if (arrBox.Count <= 0 || arrNewBoxLocate.Count <= 0)
            {
                MessageBox.Show("�걾���б�Ϊ�գ�", "�ƿ�");
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            shelfSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                foreach (SpecBox s in arrNewBoxLocate)
                {
                    if (s.BoxBarCode == "")
                    {
                        DialogResult diagResult = MessageBox.Show("���б걾��û���ҵ��µ�λ�ã�������", "��ʾ", MessageBoxButtons.YesNo);
                        if (diagResult == DialogResult.No)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return 0;
                        }
                    }
                }

                int index = 0;
                foreach (SpecBox s in arrNewBoxLocate)
                {
                    if (s.BoxBarCode == "")
                    {
                        index++;
                        continue;
                    }
                    else
                    {
                        if (boxManage.UpdateSpecBox(s) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ�", "�ƿ�");
                            return 0;
                        }

                        int shelfId = oldShelfList[index].ShelfID;
                        int oldOccupyCount = shelfManage.GetShelfByShelfId(shelfId.ToString(), "").OccupyCount - 1;

                        int newShelfId = newShelfList[index].ShelfID;
                        int newOccupyCount = shelfManage.GetShelfByShelfId(newShelfId.ToString(), "").OccupyCount + 1;

                        //���º������ھɼ��ӵ���Ϣ
                        if (shelfManage.UpdateOccupyCount(oldOccupyCount.ToString(), shelfId.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ�", "�ƿ�");                          
                            return 0;
                        }

                        ////���º��������¼��ӵ���Ϣ
                        if (shelfManage.UpdateOccupyCount(newOccupyCount.ToString(), newShelfId.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ�", "�ƿ�");
                            return 0;
                        }

                        if (shelfManage.UpdateIsFull("0", shelfId.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ�", "�ƿ�");                           
                            return 0;
                        }
                        index++;
                    }
                }
                //FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����ɹ���", "�ƿ�");
                return 0;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�", "�ƿ�");
            }
          
            return base.Save(sender, neuObject);
        }

    }
}
