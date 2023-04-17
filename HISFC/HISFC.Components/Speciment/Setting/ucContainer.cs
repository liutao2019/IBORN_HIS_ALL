using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucContainer : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private IceBoxManage iceBoxManage;
        private IceBoxLayerManage layerManage;
        private ShelfManage shelfManage;
        private SpecBoxManage specBoxManage;
        private BoxSpecManage boxSpecManage;
        private SpecTypeManage specTypeManage;
        private SubSpecManage subSpecManage;
        private DisTypeManage disTypeManage;

        StringBuilder sb = new StringBuilder();

        private ArrayList arrIceBoxList;
        private ArrayList arrLayerList;
        private ArrayList arrBoxList;
        private ArrayList arrShelfList;

        private List<SubSpec> specList = new List<SubSpec>();
        private List<SpecBox> boxList = new List<SpecBox>();

        private Dictionary<Position, SpecBox> dicBoxPos;
        private Dictionary<Position, SubSpec> dicSubSpec;

        //���ڼ�¼�걾�л����ʱ��ԭ�ڵ�
        private TreeNode tnParent;       

        private int rowIndex = -1;
        private int colIndex = -1;

        //�걾λ�ں����е��У������������ں����еı걾����
        private int specRowIndex = -1;
        private int specColIndex = -1;
        //���ڴ�ӡ�ƶ�λ����Ϣ
        private FarPoint.Win.Spread.SheetView sheetView;
        //�Ƿ��Ѿ���ӡ�˵�����λ�ã������ӡ���˳�ʱ����ʾ�����û����ʾ�û���ӡ
        private bool isPrint = true;

        //��¼�걾���Ƿ��Ѿ��ƶ�����Ȼ���϶��걾�е����˵���ʱ�����2��
        private bool isDrag = false;

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;

        #region ����
        /// <summary>
        /// ��ǰ�걾��
        /// </summary>
        private SpecBox curBox = new SpecBox();
        public SpecBox CurBox
        {
            set
            {
                curBox = value;
            }
        }

        private Shelf curShelf = new Shelf();
        public Shelf CurShelf
        {
            set
            {
                curShelf = value;
            }
        }
        #endregion

        private enum SheetCols
        {
            ����,
            ԭ����,
            ԭλ��,
            ������,
            ��λ��,
        }

        public ucContainer()
        {
            InitializeComponent();           
            iceBoxManage = new IceBoxManage();
            layerManage = new IceBoxLayerManage();
            shelfManage = new ShelfManage();
            specBoxManage = new SpecBoxManage();
            boxSpecManage = new BoxSpecManage();
            disTypeManage = new DisTypeManage();
            specTypeManage = new SpecTypeManage();
            subSpecManage = new SubSpecManage();
            arrIceBoxList = new ArrayList();
            arrLayerList = new ArrayList();
            arrShelfList = new ArrayList();
            arrBoxList = new ArrayList();
            dicBoxPos = new Dictionary<Position, SpecBox>();
            dicSubSpec = new Dictionary<Position, SubSpec>();
            sheetView = new FarPoint.Win.Spread.SheetView();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        }

        /// <summary>
        /// �󶨱걾����
        /// </summary>
        /// <param name="orgId"></param>
        private void BindingSpecType()
        {
            ArrayList arrTmp = new ArrayList(); 
            cmbSpecType.DataSource = null;
            arrTmp = specTypeManage.GetAllSpecType();
            if (arrTmp.Count > 0)
            {
                cmbSpecType.DisplayMember = "SpecTypeName";
                cmbSpecType.ValueMember = "SpecTypeID";
                cmbSpecType.DataSource = arrTmp;
                cmbSpecType.Text = "";
            }
        }

        /// <summary>
        /// �󶨲�������
        /// </summary>
        private void BindingDisType()
        {
            //disTypeManage = new DisTypeManage();
            //Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            //if (dicDisType.Count > 0)
            //{
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = dicDisType;
            //    cmbDisType.DisplayMember = "Value";
            //    cmbDisType.ValueMember = "Key";
            //    cmbDisType.DataSource = bs;
            //}
            ArrayList alDisType = disTypeManage.GetAllValidDisType();
            if (alDisType != null)
            {
                if (alDisType.Count > 0)
                {
                    cmbDisType.AddItems(alDisType);
                }
            }
            cmbDisType.Text = "";
        }

        /// <summary>
        /// ��ȡ�걾�ܴ�ŵ�����
        /// </summary>
        /// <param name="shelfId"></param>
        /// <param name="disType"></param>
        /// <param name="specType"></param>
        /// <returns></returns>
        private string GetShelfDisAndType(string shelfId,string disType,string specType)
        {

            ArrayList arrSpecBox = specBoxManage.GetBoxByCap(shelfId, 'J');
            try
            {
                DiseaseType dis = new DiseaseType();;
                FS.HISFC.Models.Speciment.SpecType st = new FS.HISFC.Models.Speciment.SpecType();
                if (arrSpecBox != null && arrSpecBox.Count > 0)
                {
                    SpecBox box = arrSpecBox[0] as SpecBox;
                    dis = disTypeManage.GetDisTypeByBoxId(box.BoxId.ToString());
                    st = specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString());
                }
                else
                {
                    dis = disTypeManage.SelectDisByID(disType);
                    st = specTypeManage.GetSpecTypeById(specType);
                }
                return dis.DiseaseName + " " + st.SpecTypeName;                
            }
            catch
            { }
            return "";
        }

        /// <summary>
        /// �������Ͱ󶨱����
        /// </summary>
        /// <param name="type">S: ����ܣ�B �걾��</param>
        private void GetLayer(string type)
        {
            Size size = new Size(1028, 1000);
            this.ParentForm.Size = size;
            this.Size = size;
            if (type == "")
            { }
            IceBox tmpIceBox = new IceBox();
            IceBoxLayer tmpLayer = new IceBoxLayer();
            Shelf tmpShelf = new Shelf();

            #region ��ȡ������Ϣ
            if (type == "B")
            {
                string tmpBoxId = curBox.BoxId.ToString();
               //IceBoxLayer tmpLayer= layerManage 
                tmpIceBox = iceBoxManage.GetIceBoxBySpecBoxId(tmpBoxId);
                if (tmpIceBox == null)
                {
                    return;
                }
                tmpLayer = layerManage.GetLayerBySpecBox(tmpBoxId);
                if (tmpLayer == null)
                {
                    return;
                }
                tmpShelf = shelfManage.GetShelfByBoxId(tmpBoxId);
                if (tmpShelf == null)
                {
                    return;
                }
            }
            if (type == "J")
            {
                string tmpShelfId = curShelf.ShelfID.ToString();
                //IceBoxLayer tmpLayer= layerManage 
                tmpIceBox = iceBoxManage.GetIceBoxByShelf(tmpShelfId);
                if (tmpIceBox == null)
                {
                    return;
                }
                tmpLayer = layerManage.GetLayerByShelf(tmpShelfId);
                if (tmpLayer == null)
                {
                    return;
                }
                tmpShelf =curShelf;
                if (tmpShelf == null)
                {
                    return;
                }
            }
     #endregion
 
            TreeNode root = new TreeNode();
            root.Tag = tmpIceBox;
            root.Text = tmpIceBox.IceBoxName + " (��" + tmpIceBox.LayerNum.ToString() + "��)";
            this.tvIceBox.Nodes.Add(root);

            TreeNode layerRoot = new TreeNode();
            layerRoot.Tag = tmpLayer;
            layerRoot.Text = "�� " + tmpLayer.LayerNum + " �� (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
            root.Nodes.Add(layerRoot);
            TreeNode shelfRoot = new TreeNode();
            shelfRoot.Tag = tmpShelf;
            string shelfBarCode = tmpShelf.SpecBarCode;
            shelfRoot.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + tmpShelf.OccupyCount.ToString() + "/" + tmpShelf.Capacity.ToString() + ")";
            shelfRoot.Text += this.GetShelfDisAndType(tmpShelf.ShelfID.ToString(), tmpShelf.DisTypeId.ToString(), tmpShelf.SpecTypeId.ToString());
            layerRoot.Nodes.Add(shelfRoot);
            tvIceBox.SelectedNode = shelfRoot;

            if (type == "B")
            {
                dgvBox.SelectionMode = DataGridViewSelectionMode.CellSelect;
                for(int i =0;i<dgvBox.ColumnCount;i++)
                    for (int k = 0; k < dgvBox.RowCount; k++)
                    {
                        if (dgvBox[i, k].Selected)
                            dgvBox[i, k].Selected = false;
                    }
                int tmpSubLayer = curBox.DesCapSubLayer;
                int tmpRow = curBox.DesCapRow;
                dgvBox[tmpRow-1,dgvBox.RowCount - tmpSubLayer].Selected = true;
                DataGridViewCellEventArgs e = new DataGridViewCellEventArgs(tmpRow - 1, dgvBox.RowCount - tmpSubLayer);
                dgvBox_CellClick(null,e);
            }
            tvIceBox.ExpandAll();
        }

        private void GetLayerByShelf()
        {
            if (curShelf == null || curShelf.ShelfID <= 0)
            {
                return;
            }
            this.GetLayer("S");
        }

        private void GetLayerBySpecBox()
        {
            if (curBox == null || curBox.BoxId <= 0)
            {
                return;
            }
            this.GetLayer("B");
        }


        /// <summary>
        /// ��ȡ���б���
        /// </summary>
        private void GetAllIceBox()
        {
            arrIceBoxList.Clear();
           //arrIceBoxList = new ArrayList();
           arrIceBoxList = iceBoxManage.GetAllIceBox();           
        }

        /// <summary>
        /// ���ݱ���Id��ȡ�����
        /// </summary>
        /// <param name="iceBoxId"></param>
        private void GetLayerByIceBoxId(string iceBoxId)
        {
            arrLayerList.Clear();
            //arrLayerList = new ArrayList();
            arrLayerList = layerManage.GetIceBoxLayers(iceBoxId);
        }

        /// <summary>
        /// ���ݱ����ID��ȡ���м���
        /// </summary>
        /// <param name="layerId"></param>
        private void GetShelfByLayerId(string layerId)
        {
            arrShelfList.Clear();
            //arrShelfList = new ArrayList();
            arrShelfList = shelfManage.GetShelfByLayerID(layerId);
        }

        /// <summary>
        /// ���ݼ���ID��ȡ���б걾��
        /// </summary>
        /// <param name="shelfId"></param>
        private void GetBoxByShelfId(string shelfId)
        {
            arrBoxList.Clear();
            arrBoxList = specBoxManage.GetBoxByCap(shelfId,'J');
        }

        /// <summary>
        /// ���ݲ�Id��ȡ���б걾��
        /// </summary>
        /// <param name="layerId"></param>
        private void GetBoxByLayerId(string layerId)
        {
            arrLayerList.Clear();
            arrLayerList = specBoxManage.GetBoxByCap(layerId, 'B');
        }

        /// <summary>
        /// �ƶ��걾
        /// </summary>
        private void CopySubSpec()
        {
            specList.Clear();
            try
            { 
                for (int i = dgvSpec.Rows.Count - 1; i >= 0; i--)
                {
                    for (int j = dgvSpec.Columns.Count - 1; j >= 0; j--)
                    {
                        DataGridViewCell cell = dgvSpec[j, i];
                        if (cell.Selected)
                        {
                            SubSpec s = cell.Tag as SubSpec;
                            if (s == null)
                                continue;
                            specList.Add(cell.Tag as SubSpec);
                        }
                    }
                }
                specList.Sort(new SpecSort());
                
            }
            catch
            {
                specList.Clear();
            }
        }

        /// <summary>
        /// �ƶ��걾
        /// </summary>
        private void TransferSpec()
        {
            DialogResult r = MessageBox.Show("�ƶ��걾,�Ƿ����?","��ʾ", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                specList.Clear();
                return;
            }
            if (specList.Count <= 0)
                return;
            if (dgvBox.SelectedCells.Count > 1)
            {
                MessageBox.Show("�벻Ҫѡ�����걾��");
                return;
            }
            if(dgvBox.SelectedCells.Count == 0)
            {
                MessageBox.Show("��ѡ��Ŀ��걾��");
                return;
            }

            SpecBox box = dgvBox.SelectedCells[0].Tag as SpecBox;
            if (box == null)
            {
                MessageBox.Show("��ȡ�걾��ʧ��");
                return;
            }

            //�����ٻ�ȡһ��
            box = specBoxManage.GetBoxById(box.BoxId.ToString());

            if (box == null)
            {
                MessageBox.Show("��ȡ�걾��ʧ��");
                return;
            }          
           

            SubSpec sub = specList[0];
            if (sub.SpecTypeId != box.SpecTypeID)
            {
                MessageBox.Show("�걾���Ͳ����");
                return;
            }

            if (sub.BoxId != box.BoxId)
            {
                if (box.Capacity - box.OccupyCount < specList.Count)
                {
                    MessageBox.Show("��ǰ�걾�пռ䲻����");
                    return;
                }
            } 

            r = MessageBox.Show("�ƶ��걾,�Ƿ����?", "��ʾ", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                specList.Clear();
                return;
            }

            SpecInOper tmpIn = new SpecInOper();                       //BX005-C5-J1-11
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            tmpIn.Trans = FS.FrameWork.Management.PublicTrans.Trans;
            tmpIn.SetTrans();

            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (sub.BoxId == box.BoxId)
                {
                    boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    BoxSpec tmp = new BoxSpec();
                    tmp = boxSpecManage.GetSpecByBoxId(sub.BoxId.ToString());
                    subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int tmpRow;
                    int tmpCol;
                    #region �����ж�ǰ���Ƿ��п�λ�ã���ֹ����ʱû��ѡ��Ŀ��걾�о�ֱ���ڵ�ǰ�����ı걾���ƶ�lingk20110622
                    int vpNum = 0;
                    int rts = subSpecManage.ScanSpecBox(sub.BoxId.ToString(), out vpNum);
                    if (rts < 0) //��ѯ����
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ���ѯ�걾�п�λ����");
                        specList.Clear();
                        return;
                    }
                    else
                    {
                        if (specList.Count <= tmp.Capacity) //ȫѡ�����������
                        {
                        }
                        //����걾��λ��С��ѡ���ƶ��걾��andѡ���ƶ��걾��С���ܱ걾����������û��ȫѡ��
                        else if ((vpNum < specList.Count) || (specList.Count < tmp.Capacity))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ��걾�п�λ�����������Ҫ�ڱ��걾��������ȫѡ������ƶ��������걾����ѡ��Ŀ��걾�У�");
                            specList.Clear();
                            return;
                        }
                    }
                    #endregion
                    for (int i = 0; i < specList.Count; i++)
                    {
                        tmpRow = (i + 1) % tmp.Col == 0 ? (i + 1) / tmp.Col : (i + 1) / tmp.Col + 1;
                        tmpCol = (i + 1) % tmp.Col == 0 ? tmp.Col : (i + 1) % tmp.Col;
                        sub = specList[i];
                        sub.BoxEndRow = tmpRow;
                        sub.BoxEndCol = tmpCol;
                        sub.BoxStartCol = tmpCol;
                        sub.BoxStartRow = tmpRow;

                        #region �����ж�ǰ���Ƿ��п�λ�ã���ֹ����ʱû��ѡ��Ŀ��걾�о�ֱ���ڵ�ǰ�����ı걾���ƶ�lingk20110622
                        //SubSpec tmpSS = new SubSpec();
                        //tmpSS = subSpecManage.GetSubSpecByLocate(sub.BoxId, sub.BoxEndCol, sub.BoxEndRow, sub.BoxStartCol, sub.BoxStartRow);
                        //if (tmpSS != null) //ԭ��λ�ô��ڱ걾
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show("����ʧ�ܣ�");
                        //    specList.Clear();
                        //    return;
                        //}
                        //ò�����������update��ʹ��selectȡ���Ļ��洢���ѯ���ݶ��������⣨db2��with ur��֪�����з񣩣�ǰ���б걾��Ϣ�ƶ����ٴ�ѭ���ͻ���������
                        #endregion

                        if (subSpecManage.UpdateSubSpec(sub) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ�");
                            specList.Clear();
                            return;
                        }                         
                    }
                }
                else
                {
                    for (int i = 0; i < specList.Count; i++)
                    {
                        sub = specList[i];
                        SpecBox sourceBox = specBoxManage.GetBoxById(sub.BoxId.ToString());
                        SpecBox desBox = specBoxManage.GetBoxById(box.BoxId.ToString());
                        if (tmpIn.TransferSpec(sourceBox, desBox, sub) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ�");
                            specList.Clear();
                            return;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����ɹ�!");
                specList.Clear();
            }
            catch
            {
                MessageBox.Show("����ʧ�ܣ�");
                specList.Clear();
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
 
            
        }

        /// <summary>
        /// �ƶ��걾��
        /// </summary>
        private void CopyBox()
        {
            boxList.Clear();

            if (dgvBox.Tag == null)
            {
                return;
            }

            try
            {
                for (int i = dgvBox.Rows.Count - 1; i >= 0; i--)
                {
                    for (int j = dgvBox.Columns.Count - 1; j >= 0; j--)
                    {
                        DataGridViewCell cell = dgvBox[j, i];
                        if (cell.Selected)
                        {
                            SpecBox s = cell.Tag as SpecBox;
                            if (s == null)
                                continue;
                            boxList.Add(cell.Tag as SpecBox);
                        }
                    }
                }
                boxList.Sort(new BoxSort());

            }
            catch
            {
                boxList.Clear();
            }
        }

        /// <summary>
        /// �ƶ��걾��
        /// </summary>
        private void TransferBox()
        {
            DialogResult r = MessageBox.Show("�ƶ��걾��,�Ƿ����?", "��ʾ", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                boxList.Clear();
                return;
            }
            if (boxList.Count <= 0)
                return;

            if (tvIceBox.SelectedNode.Tag == null)
            {
                return;
            }
            Shelf shelf = new Shelf();
            try
            {
                if (dgvBox.SelectedCells.Count > 1)
                {
                    MessageBox.Show("�벻Ҫѡ���������!");
                    return;
                }
                shelf = dgvBox.SelectedCells[0].Tag as Shelf;
                if (shelf == null || shelf.ShelfID == 0)
                {
                    if (!tvIceBox.SelectedNode.Tag.GetType().ToString().Contains("Shelf"))
                    {
                        MessageBox.Show("��ѡ�񶳴��!");
                        return;
                    }

                    shelf = tvIceBox.SelectedNode.Tag as Shelf;
                }
            }
            catch
            { 
            }


            if (shelf == null || shelf.ShelfID == 0)
            {
                MessageBox.Show("��ѡ��Ŀ�궳��ܣ�");
                return;
            }
            
            //�����ٻ�ȡһ��
            shelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");

            if (shelf == null)
            {
                MessageBox.Show("��ȡ�����ʧ��");
                return;
            }


            SpecBox box = boxList[0];

            if (shelf.SpecTypeId != 9)
            {

                if (box.SpecTypeID != shelf.SpecTypeId)
                {
                    MessageBox.Show("�걾���Ͳ����");
                    return;
                }
            }

            int boxCount = specBoxManage.GetBoxByCap(shelf.ShelfID.ToString(), 'J').Count;

            if (box.DesCapID != shelf.ShelfID)
            {
                if (shelf.Capacity - boxCount < boxList.Count)
                {
                    MessageBox.Show("��ǰ���ӿռ䲻����");
                    return;
                }
            } 

            r = MessageBox.Show("�ƶ��걾��,�Ƿ����?", "��ʾ", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                boxList.Clear();
                return;
            }

            ShelfSpecManage shelfSpecMgr = new ShelfSpecManage();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            shelfSpecMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (box.DesCapID == shelf.ShelfID)
                {

                    boxList.Clear();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return;
                }
                else
                {
                    if (shelfManage.UpdateIsFull("0", box.DesCapID.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�");
                        return;
                    }

                    for (int i = 0; i < boxList.Count; i++)
                    {
                        box = new SpecBox();
                        box = boxList[i];
                        Shelf sourceShelf = shelfManage.GetShelfByShelfId(box.DesCapID.ToString(), "");
                        Shelf desShelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");

                        int sourceCount = specBoxManage.GetBoxByCap(box.DesCapID.ToString(), 'J').Count;
                        int desCount = specBoxManage.GetBoxByCap(shelf.ShelfID.ToString(), 'J').Count;

                        ShelfSpec tmpShelfSpec = new ShelfSpec();
                        tmpShelfSpec = shelfSpecMgr.GetShelfByShelf(desShelf.ShelfID.ToString());                        

                        //��ǰ�걾���Ƿ�λ
                        bool located = false; 

                        //ѭ������Ŀ�궳��ܣ������п�λ������
                        for (int rs = 1; rs <= tmpShelfSpec.Row; rs++)
                        {
                            if (located)
                            {
                                break;
                            }
                            for (int h = 1; h <= tmpShelfSpec.Height; h++)
                            {
                                if (specBoxManage.GetByPoint("1", rs.ToString(), h.ToString(), desShelf.ShelfID.ToString(), "J") == null)
                                {
                                    box.DesCapRow = rs;
                                    box.DesCapSubLayer = h;
                                    box.BoxBarCode = desShelf.SpecBarCode + "-" + rs.ToString() + h.ToString();
                                    box.DesCapID = desShelf.ShelfID;
                                    if (specBoxManage.UpdateSpecBox(box) <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("����ʧ�ܣ�");                                        
                                        return;
                                    }

                                    //�����¶����
                                    if (shelfManage.UpdateOccupyCount((desCount + 1).ToString(), shelf.ShelfID.ToString()) <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("����ʧ�ܣ�");                                        
                                        return;
                                    }

                                    if (desCount + 1 == desShelf.OccupyCount)
                                    {
                                        if (shelfManage.UpdateIsFull("1", desShelf.ShelfID.ToString()) <= 0)
                                        {
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("����ʧ�ܣ�");
                                            return;
                                        }
                                    }
                                    //���¾ɶ����
                                    if (shelfManage.UpdateOccupyCount((sourceCount - 1).ToString(), sourceShelf.ShelfID.ToString()) <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("����ʧ�ܣ�");
                                        return;
                                    }
                                    located = true;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����ɹ�!");

            }
            catch
            {
                MessageBox.Show("����ʧ�ܣ�");
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            finally
            {
                boxList.Clear();
            }
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private void InitTree()
        {             
            this.tvIceBox.Nodes.Clear();
            this.tvIceBox.AfterSelect += this.TreeClick;
            this.tvIceBox.MouseDown += this.lv_MouseDown;
            this.tvIceBox.DragDrop += this.lv_DragDrop;
            this.tvIceBox.DragEnter += this.lv_DragEnter;
            if (curBox != null && curBox.BoxId > 0)
            {
                this.GetLayerBySpecBox();
                return;
            }
            if (curShelf != null && curShelf.ShelfID > 0)
            {
                this.GetLayerByShelf();
                return;
            }
            GetAllIceBox();
            if (arrIceBoxList == null || arrIceBoxList.Count <= 0)
            {
                return;
            }
            this.tvIceBox.Font = new Font ("����", 12);
            //foreach (ColumnHeader ch in nlvSpecContainer.c)
            //{
               
            //}
            //nlvSpecContainer.Font = new Font ("����", 14);
            foreach (IceBox i in arrIceBoxList)
            {
                if (i.IceBoxTypeId == "2")
                {
                    continue;
                }
                TreeNode root = new TreeNode();
                root.Tag = i;
                root.Text = i.IceBoxName + " (��" + i.LayerNum.ToString() + "��)";
                this.tvIceBox.Nodes.Add(root);
                GetLayerByIceBoxId(i.IceBoxId.ToString());
                foreach (IceBoxLayer layer in arrLayerList)
                {
                    TreeNode layerRoot = new TreeNode();
                    layerRoot.Tag = layer;
                    layerRoot.Text = "�� " + layer.LayerNum + " �� (" + layer.OccupyCount.ToString() + "/" + layer.Capacity.ToString() + ")";
                    root.Nodes.Add(layerRoot);
                    if (layer.SaveType == 'J')
                    {
                        GetShelfByLayerId(layer.LayerId.ToString());
                        foreach (Shelf shelf in arrShelfList)
                        {
                            TreeNode shelfRoot = new TreeNode();
                            shelfRoot.Tag = shelf;
                            string shelfBarCode = shelf.SpecBarCode;
                            shelfRoot.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + shelf.OccupyCount.ToString() + "/" + shelf.Capacity.ToString() + ")";
                            shelfRoot.Text += this.GetShelfDisAndType(shelf.ShelfID.ToString(), shelf.DisTypeId.ToString(), shelf.SpecTypeId.ToString());
                            layerRoot.Nodes.Add(shelfRoot);                            
                        }
                    }
                    //if (layer.SaveType == 'B')
                    //{
                    //    for (int k = 1; k <= layer.Col; k++)
                    //    {
                    //        TreeNode boxRoot = new TreeNode();
                    //        boxRoot.Tag = k;
                    //        boxRoot.Text = "�� " + k.ToString() + " ��";
                    //        layerRoot.Nodes.Add(boxRoot);
                    //    }
                    //}
                }
            }
            tvIceBox.CollapseAll();
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            bool selAll = false;
            if (cmbDisType.Text.Trim() == "" && cmbSpecType.Text.Trim() == "")
            {
                chkTime.Checked = true;
                selAll = true;
            }
            tvIceBox.Nodes.Clear();
            int disTypeId = 0;
            if (cmbDisType.Text.Trim() != "")
            {
                disTypeId = Convert.ToInt32(cmbDisType.Tag.ToString());
            }

            int specTypeId = 0;
            if (cmbSpecType.Text.Trim() != "")
            {
                specTypeId = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
            }

            //�����б�
            arrIceBoxList = new ArrayList();
            //���б�
            arrLayerList = new ArrayList();
            //�����б�
            arrShelfList = new ArrayList();
            if (!chkTime.Checked)
            {
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetLayerByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
            }
            if (chkTime.Checked)
            {
                string start= dtpStart.Value.Date.ToString();
                string end = dtpEnd.Value.Date.AddDays(1.0).ToString();
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(),start,end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                if (selAll)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTime(start, end);
                    arrLayerList = layerManage.GetIceBoxByStoreTime(start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTime(start, end);
                }
            }

            #region ѭ�����������б� ���ص����β˵�
            foreach (IceBox tmpIceBox in arrIceBoxList)
            {
                TreeNode root = new TreeNode();
                root.Tag = tmpIceBox;
                root.Text = tmpIceBox.IceBoxName + " (��" + tmpIceBox.LayerNum.ToString() + "��)";
                this.tvIceBox.Nodes.Add(root);

                foreach (IceBoxLayer tmpLayer in arrLayerList)
                {
                    if (tmpLayer.IceBox.IceBoxId == tmpIceBox.IceBoxId)
                    {
                        TreeNode layerRoot = new TreeNode();
                        layerRoot.Tag = tmpLayer;
                        layerRoot.Text = "�� " + tmpLayer.LayerNum + " �� (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
                        root.Nodes.Add(layerRoot);
                        foreach (Shelf tmpShelf in arrShelfList)
                        {
                            if (tmpShelf.IceBoxLayer.LayerId == tmpLayer.LayerId)
                            {
                                TreeNode shelfRoot = new TreeNode();
                                shelfRoot.Tag = tmpShelf;
                                string shelfBarCode = tmpShelf.SpecBarCode;
                                shelfRoot.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + tmpShelf.OccupyCount.ToString() + "/" + tmpShelf.Capacity.ToString() + ")";
                                if (tmpShelf.OccupyCount < tmpShelf.Capacity)
                                {
                                    shelfRoot.ImageIndex = 2;
                                }
                                shelfRoot.Text += this.GetShelfDisAndType(tmpShelf.ShelfID.ToString(), tmpShelf.DisTypeId.ToString(), tmpShelf.SpecTypeId.ToString());
                                layerRoot.Nodes.Add(shelfRoot);
                            }
                        }
                    }
                }
            }
            tvIceBox.ExpandAll();

            selAll = false;            
            #endregion
        }

        private void SetContainer(IceBoxLayer l, Shelf s)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڻ�ȡ��Ϣ�����Ժ�...");
                Application.DoEvents();
                this.dgvBox.Rows.Clear();
                this.dgvBox.Columns.Clear();

                FS.HISFC.Models.Speciment.Container container = new FS.HISFC.Models.Speciment.Container();

                if (l != null)
                {
                    //container.Row = l.Row;
                    //container.Col = l.Col;
                    //container.Height = l.Height; 
                    // arrBoxList = specBoxManage.GetBoxByCapCol(l.LayerId.ToString(), tn.Tag.ToString());

                }
                if (s != null)
                {
                    arrBoxList = specBoxManage.GetBoxByCap(s.ShelfID.ToString(), 'J');
                    ShelfSpecManage tmpShelfSpec = new ShelfSpecManage();
                    ShelfSpec shelfSpec = tmpShelfSpec.GetShelfByShelf(s.ShelfID.ToString());
                    container.Height = shelfSpec.Height;
                    container.Col = shelfSpec.Col;
                    container.Row = shelfSpec.Row;
                    dgvBox.Tag = s;
                }

                #region �������ʱ

                //���ñ걾�б������Ĵ�С
                #region ���ñ걾�б������Ĵ�С
                if (container != null)
                {
                    if (arrBoxList.Count > 0)
                    {
                        grpSpecBoxInfo.Visible = true;
                        grpSpecInfo.Visible = true;
                        //nlvSpecContainer.Visible = true;
                    }
                    Size size = new Size();
                    if (container.Row > 2)
                    {
                        size = new Size(59 * container.Row + 30, 45 * container.Height + 59);
                        dgvBox.Size = size;
                    }
                }
                #endregion

                for (int r = 1; r <= container.Row; r++)
                {
                    DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                    //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                    imgDcl.HeaderText = "���:" + r.ToString();
                    imgDcl.Width = 180;
                    dgvBox.Columns.Add(imgDcl);
                }
                for (int h = container.Height; h >= 1; h--)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;
                    DataGridViewHeaderCell cell = dr.HeaderCell;
                    dr.HeaderCell.Value = "��" + h.ToString() + "��";
                    dgvBox.Rows.Add(dr);
                }
                foreach (SpecBox box in arrBoxList)
                {
                    Position p = new Position(box.DesCapRow, box.DesCapSubLayer);
                    dicBoxPos.Add(p, box);
                }
                //����dgvBox�е�ÿ��λ�ã����SpecBox��listview�ж�Ӧ�ĸߺ��У����ظñ걾����Ϣ

                for (int i = 0; i < dgvBox.Rows.Count; i++)
                {
                    for (int k = 0; k < dgvBox.Columns.Count; k++)
                    {

                        int index = 0;

                        foreach (Position pt in dicBoxPos.Keys)
                        {
                            if (pt.Row == k + 1 && dgvBox.RowCount - pt.Height + 1 == dgvBox.RowCount - i)
                            {
                                SpecBox box = new SpecBox();
                                //subSpecManage.GetSubSpecInOneBox();
                                box = dicBoxPos[pt];
                                string boxBarCode = box.BoxBarCode;
                                DataGridViewCell cell = dgvBox[k, dgvBox.RowCount - i - 1];
                                //d.DISEASENAME ||' '||
                                string tmpSpecTypeName = specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString()).SpecTypeName;
                                string tmpSql = @"select case length(d.specimentname) when 1 then d.specimentname when 2 then d.specimentname when 3 then d.specimentname
                                else substr(d.specimentname,3,2) end|| '('|| min(ss.SPEC_NO) || '-' || max(ss.SPEC_NO)||')'
                                from spec_box b join spec_type d on b.spectypeid = d.specimenttypeid
                                left join spec_subspec s on b.boxid = s.boxid
                                left join spec_source ss on ss.specid = s.specid
                                where b.boxid = {0} and b.DISEASETYPEID = ss.DISEASETYPEID
                                group by b.boxid, specimentname";

                                tmpSql = string.Format(tmpSql, box.BoxId.ToString());
                                string boxName = specBoxManage.ExecSqlReturnOne(tmpSql);
                                if (boxName == "-1")
                                {
                                    cell.Value = "";
                                }
                                else
                                {
                                    cell.Value = boxName;
                                }
                                cell.Tag = box;
                                string toolTipText = "λ�ã���" + box.DesCapRow.ToString() + " ��,��" + box.DesCapSubLayer.ToString() + " ��\n";
                                toolTipText += "�������ͣ�" + disTypeManage.GetDisTypeByBoxId(box.BoxId.ToString()).DiseaseName + "\n";
                                toolTipText += "�걾���ͣ�" + tmpSpecTypeName;
                                cell.ToolTipText = toolTipText;
                                //if (box.SpecialUse == "8")
                                //{
                                //    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                //    style.BackColor = Color.SkyBlue;
                                //    cell.Style = style;
                                //}
                                //if (box.SpecialUse == "1")
                                //{
                                //    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                //    style.BackColor = Color.Orange;
                                //    cell.Style = style;
                                //}
                                if (box.SpecialUse != "")
                                {
                                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    style.BackColor = Color.SkyBlue;
                                    cell.Style = style;
                                }
                                break;
                            }
                            index++;
                        }
                        if (index >= dicBoxPos.Keys.Count)
                        {
                            DataGridViewCell cell = dgvBox[k, dgvBox.RowCount - i - 1];
                            cell.Value = "��";
                            cell.Tag = null;
                            string toolTipText = "��λ��û�ű걾��";
                            cell.ToolTipText = toolTipText;
                        }
                    }
                }
                #endregion
            }
            catch
            { }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            }

        /// <summary>
        /// �Ƴ����޸Ķ����
        /// </summary>
        /// <param name="oper">�Ƴ���D���޸�: M</param>
        private void OperShelf(string oper)
        {
            if (grpSpecBoxInfo.Tag == null)
            {
                return;
            }
            string curTag = grpSpecBoxInfo.Tag.ToString();
            //����Ǳ���
            if (curTag == "I")
            {
                if (dgvBox.SelectedCells.Count > 1)
                {
                    MessageBox.Show("��ѡ��һ����Ҫ�Ƴ��Ķ����");
                    return;
                }
                try
                {
                    DataGridViewCell cell = dgvBox.SelectedCells[0];
                    Shelf tmpShelf = cell.Tag as Shelf;
                    OperShelf tmp = new OperShelf();
                    //�޸�
                    if (oper == "M")
                    {
                        tmp.ModifyShelf(ref tmpShelf);
                        cell.Tag = tmpShelf;
                    }
                    //�Ƴ�
                    if (oper == "D")
                    {
                        tmp.RemoveShelf(ref tmpShelf);
                        cell.Tag = tmpShelf;
                    }
                    
                }
                catch
                { }
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("��Ӷ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B��ҩ̨���, true, false, null);
            this.toolBarService.AddToolButton("��ӱ걾��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B�������, true, false, null);
            this.toolBarService.AddToolButton("��ӡ�걾������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("�޸ı걾��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            this.toolBarService.AddToolButton("�ƶ��걾", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zת��, true, false, null);
            this.toolBarService.AddToolButton("ȷ���걾λ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zת��ȷ��, true, false, null);
            this.toolBarService.AddToolButton("�Ƴ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
            this.toolBarService.AddToolButton("�޸Ķ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            this.toolBarService.AddToolButton("�ƶ��걾��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T��ת, true, false, null);
            this.toolBarService.AddToolButton("ȷ���걾��λ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T��ת, true, false, null);
            
            this.toolBarService.AddToolButton("ˢ���б�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {              
                case "��Ӷ����":
                    //ÿһ����Ӷ����ȷ��ȡ�õ������µļ�¼   
                    if (dgvBox.Tag == null)
                        return;
                    int layer = dgvBox.SelectedCells[0].RowIndex + 1;
                    if (layer <= 0)
                        return;
                    IceBox tmpIceBox = new IceBox();
                    IceBoxLayer tmpLayer = new IceBoxLayer();
                    try
                    {
                        tmpIceBox = dgvBox.Tag as IceBox;
                    }
                    catch
                    {
                        MessageBox.Show("��Ϣ����");
                        return;
                    }
                    tmpLayer = layerManage.GetLayerIDByIceBox(tmpIceBox.IceBoxId.ToString(), layer.ToString());
                    if (tmpLayer == null||tmpLayer.LayerId <= 0)
                    {
                        MessageBox.Show("��ȡ������Ϣʧ�ܣ�");
                        return;
                    }
                    if (tmpLayer.SaveType == 'B')
                    {
                        return;
                    }
                    if (tmpLayer.OccupyCount == tmpLayer.Capacity)
                    {
                        MessageBox.Show("�����������");
                        return;
                    }
                    //dgvBox.CurrentCell.ColumnIndex+1Ϊ��ǰ�У�ϵͳ��0�������ϰ��˼ά1�������������lingk0928
                    if (dgvBox.CurrentCell.ColumnIndex >= tmpLayer.Col)
                    {
                        MessageBox.Show("�ñ����ֻ��" + tmpLayer.Col.ToString() + "�����������ڵ�" + (dgvBox.CurrentCell.ColumnIndex + 1).ToString() + "�ռ�������Ӷ���ܣ�");
                        return;
                    }
                    frmSpecShelf frmCurShelf = new frmSpecShelf();
                    frmCurShelf.LayerId = tmpLayer.LayerId;
                    frmCurShelf.ShowDialog();
                    break;
                case "��ӱ걾��":

                    #region ��ӱ걾��
                    Shelf s = new Shelf();
                    frmSpecBox frmCurBox = new frmSpecBox();

                    if (dgvBox.Tag.GetType().ToString().Contains("Shelf"))// is Shelf)
                    {
                        s = dgvBox.Tag as Shelf;
                        int sublayer = dgvBox.RowCount - dgvBox.SelectedCells[0].RowIndex ;
                        int col = 1;
                        int row = dgvBox.SelectedCells[0].ColumnIndex + 1;

                        SpecBox box = specBoxManage.GetByPoint(col.ToString(), row.ToString(), sublayer.ToString(), s.ShelfID.ToString(), "J");
                        if (box != null && box.BoxId > 0)
                        {
                            MessageBox.Show("��λ�ô��ڱ걾�У�");
                            return;
                        }
                        frmCurBox.InShelfCol = col;
                        frmCurBox.InShelfRow = row;
                        frmCurBox.InShelfHeight = sublayer;
                    }
                    else
                    {
                        try
                        {
                            s = dgvBox.SelectedCells[0].Tag as Shelf;
                        }
                        catch
                        {
                            return;
                        }
                    }
                     
                    if (s == null)
                    {
                        return;
                    }
                    string shelfId = s.ShelfID.ToString();
                    if (shelfId == null || shelfId == "")
                    {
                        MessageBox.Show("��ѡ��Ŀ��ܣ�");
                        return;
                    }
                     if (s.OccupyCount == s.Capacity)
                    {
                        MessageBox.Show("�����������");
                        return;
                    }
                    frmCurBox.CurShelfId = Convert.ToInt32(shelfId);
                    frmCurBox.ShowDialog();
                    tvIceBox.Refresh();
                    break;
                    #endregion

                case "�޸ı걾��":
                    try
                    {
                        SpecBox tmpBox = new SpecBox();
                        tmpBox = dgvBox.SelectedCells[0].Tag as SpecBox;
                        if (subSpecManage.GetSubSpecInOneBox(tmpBox.BoxId.ToString()).Count > 0)
                        {
                            MessageBox.Show("�걾���ڴ��б걾�����޸ģ�");
                            return;
                        }                       
                        frmSpecBox frm = new frmSpecBox();
                        frm.CurBox = tmpBox;
                        frm.CurShelfId = tmpBox.DesCapID;
                        frm.Oper = "M";
                        frm.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("��ȡ�걾��ʧ��");
                    }
                    break;
           
                case "ȷ���걾λ��":
                    this.TransferSpec();
                    break;
                case "�ƶ��걾":
                    this.CopySubSpec();
                    break;
                case "�Ƴ������":
                    this.OperShelf("D");
                    break;
                case "�޸Ķ����":
                    this.OperShelf("M");
                    break;
                case "�ƶ��걾��":
                    CopyBox();                   
                    break;
                case "ȷ���걾��λ��":
                    TransferBox();
                    break;
                case "ˢ���б�":
                    InitTree();
                    break;
                default:
                    break;
            }
            //this.Query();
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ����Click�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeClick(object sender, EventArgs e)
        {
            try
            {
                grpSpecBoxInfo.Visible = false;
                grpSpecInfo.Visible = false;
                //nlvSpecContainer.Visible = false;
                grpSpecBoxInfo.Tag = "";
                dgvBox.Rows.Clear();
                //dgvSpec.Columns.Clear();
                dgvBox.Columns.Clear();
                //dgvSpec.Rows.Clear();
                dicBoxPos.Clear();
                dicSubSpec.Clear();

                dgvBox.Tag = null;

                dgvBox.RowHeadersWidth = 100;
                dgvSpec.RowHeadersWidth = 50;

                TreeNode tn = tvIceBox.SelectedNode;
                string type = tn.Tag.GetType().ToString();

                #region ���������ʱ
                if (type.Contains("IceBox"))
                {
                    IceBox tmpIceBox = new IceBox();
                    if (type.Contains("Layer"))
                    {
                        IceBoxLayer ly = new IceBoxLayer();
                        ly = tn.Tag as IceBoxLayer;
                        ly = layerManage.GetLayerById(ly.LayerId.ToString());
                        tn.Text = "�� " + ly.LayerNum + " �� (" + ly.OccupyCount.ToString() + "/" + ly.Capacity.ToString() + ")";
                        tmpIceBox = tn.Parent.Tag as IceBox;
                        grpSpecBoxInfo.Text = tn.Parent.Text;
                    }
                    else
                    {
                        tmpIceBox = tn.Tag as IceBox;
                        tmpIceBox = iceBoxManage.GetIceBoxById(tmpIceBox.IceBoxId.ToString());
                        grpSpecBoxInfo.Text = tn.Text;
                    }
                    grpSpecBoxInfo.Visible = true;
                    grpSpecBoxInfo.Tag = "I";
                    dgvBox.Tag = tmpIceBox;

                    ArrayList arrLayer = layerManage.GetLayerInOneBox(tmpIceBox.IceBoxId.ToString());
                    IceBoxLayer tmpLayer = arrLayer[0] as IceBoxLayer;
                    dgvBox.ShowCellToolTips = true;                    
                    for (int r = 1; r <= tmpLayer.Col; r++)
                    {
                        DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                        //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                        imgDcl.HeaderText = "����:" + r.ToString();
                        imgDcl.Width = 150;
                        dgvBox.Columns.Add(imgDcl);
                    }
                    for (int h = 1; h<= tmpIceBox.LayerNum; h++)
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.Height = 40;
                        DataGridViewHeaderCell cell = dr.HeaderCell;
                        dr.HeaderCell.Value = "��" + h.ToString() + "��";
                        dgvBox.Rows.Add(dr);
                    }                   

                    foreach (IceBoxLayer l in arrLayer)
                    {
                        ArrayList arrS = shelfManage.GetShelfByLayerID(l.LayerId.ToString());
                        if ((arrS != null) && (arrS.Count > 0))
                        {
                            foreach (Shelf s in arrS)
                            {
                                int row = l.LayerNum;
                                int col = s.IceBoxLayer.Col;
                                DataGridViewCell cell = dgvBox[col - 1, row - 1];
                                cell.Tag = s;

                                string disSetting = this.GetShelfDisAndType(s.ShelfID.ToString(), s.DisTypeId.ToString(), s.SpecTypeId.ToString());
                                string tipText = "�������: " + disSetting;
                                tipText += "\n����: " + s.Capacity.ToString();
                                tipText += "\nռ������: " + s.OccupyCount.ToString();
                                cell.Value = disSetting;
                                cell.ToolTipText = tipText;
                                if (s.IsOccupy == '1' || s.OccupyCount == s.Capacity)
                                {
                                    //DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    //style.BackColor = Color.SkyBlue;
                                    //cell.Style = style;
                                    cell.Value = disSetting + "(����)";
                                }
                                if (s.OccupyCount == 0)
                                {
                                    //DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    //style.BackColor = Color.Orange;
                                    //cell.Style = style;
                                    cell.Value = disSetting + " (��)";
                                }
                            }
                            for (int ki = 0; ki < dgvBox.ColumnCount; ki++)
                            {
                                if (l.Col <= ki)
                                {
                                    int lNum = l.LayerNum;
                                    DataGridViewCell cell = dgvBox[ki, lNum - 1];
                                    cell.Value = "��λ��";
                                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    style.BackColor = Color.LightGray;
                                    cell.Style = style;
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0; k < dgvBox.ColumnCount; k++)
                            {
                                if (l.Col <= k)
                                {
                                    int lNum = l.LayerNum;
                                    DataGridViewCell cell = dgvBox[k, lNum - 1];
                                    cell.Value = "��λ��";
                                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    style.BackColor = Color.LightGray;
                                    cell.Style = style;
                                }
                            }
                        }
                    }
                    return;
                }
                #endregion 

                #region �������ʱ
                //FS.HISFC.Object.Speciment.Container container = new FS.HISFC.Object.Speciment.Container();
                
                #region  ����ֱ�Ӵ�ű걾�У�������
                //if (tn != null && tn.Text.Contains("��"))
                //{
                //    if (tn.Parent == null)
                //        return;
                //    IceBoxLayer currentLayer = tn.Parent.Tag as IceBoxLayer;
                //    if (currentLayer.SaveType == 'B')
                //    {
                //        arrBoxList = specBoxManage.GetBoxByCapCol(currentLayer.LayerId.ToString(), tn.Tag.ToString());
                //    }
                //    if (arrBoxList.Count > 0)
                //    {
                //        //SpecBox s = arrBoxList[0] as FS.HISFC.Object.Speciment.SpecBox;    
                //        container.Row = currentLayer.Row;
                //        container.Col = currentLayer.Col;
                //        container.Height = currentLayer.Height;
                //        grpSpecBoxInfo.Visible = true;
                //        grpSpecInfo.Visible = true;                         
                //    } 
                //}
                #endregion

                if (tn != null && type.Contains("Shelf"))
                {
                    grpSpecBoxInfo.Tag = "S";
                    Shelf currentShelf = tn.Tag as Shelf;
                    currentShelf = shelfManage.GetShelfByShelfId(currentShelf.ShelfID.ToString(), "");
                    string shelfBarCode = currentShelf.SpecBarCode;
                    tn.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + currentShelf.OccupyCount.ToString() + "/" + currentShelf.Capacity.ToString() + ")";
                    tn.Text += this.GetShelfDisAndType(currentShelf.ShelfID.ToString(), currentShelf.DisTypeId.ToString(), currentShelf.SpecTypeId.ToString());

                    tnParent = tn;
                    //arrBoxList = specBoxManage.GetBoxByCap(currentShelf.ShelfID.ToString(), 'J');
                    //ShelfSpecManage tmpShelfSpec = new ShelfSpecManage();
                    //ShelfSpec shelfSpec = tmpShelfSpec.GetShelfByShelf(currentShelf.ShelfID.ToString());
                    //container.Height = shelfSpec.Height;
                    //container.Col = shelfSpec.Col;
                    //container.Row = shelfSpec.Row;
                    grpSpecBoxInfo.Text = ParseLocation.ParseShelf(currentShelf.SpecBarCode) +"  �걾��λ����Ϣ";
                    this.SetContainer(null, currentShelf);
                }

                #region ��δ�����ȡ����
                //���ñ걾�б������Ĵ�С
                //#region ���ñ걾�б������Ĵ�С
                //if (container != null)
                //{
                //    if (arrBoxList.Count > 0)
                //    {
                //        grpSpecBoxInfo.Visible = true;
                //        grpSpecInfo.Visible = true;
                //        //nlvSpecContainer.Visible = true;
                //    }

                //    Size size = new Size();
                //    if (container.Row > 2)
                //    {
                //        size = new Size(59 * container.Row + 30, 45 * container.Height + 59);
                //        dgvBox.Size = size;
                //    }
                //}
                //#endregion

                //for (int r = 1; r <= container.Row; r++)
                //{
                //    DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                //    //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                //    imgDcl.HeaderText = "���:" + r.ToString();
                //    imgDcl.Width = 150;
                //    dgvBox.Columns.Add(imgDcl);
                //}
                //for (int h = container.Height; h >= 1; h--)
                //{                    
                //    DataGridViewRow dr = new DataGridViewRow();                     
                //    dr.Height = 40;                    
                //    DataGridViewHeaderCell cell = dr.HeaderCell;                    
                //    dr.HeaderCell.Value = "��" + h.ToString()+"��" ;
                //    dgvBox.Rows.Add(dr);                    
                //}
                //foreach (SpecBox box in arrBoxList)
                //{
                //    Position p = new Position(box.DesCapRow, box.DesCapSubLayer);
                //    dicBoxPos.Add(p, box);
                //}
                ////����dgvBox�е�ÿ��λ�ã����SpecBox��listview�ж�Ӧ�ĸߺ��У����ظñ걾����Ϣ
                
                //for (int i = 0; i < dgvBox.Rows.Count; i++)
                //{
                //    for (int k = 0; k < dgvBox.Columns.Count; k++)
                //    {
                        
                //        int index = 0;

                //        foreach (Position pt in dicBoxPos.Keys)
                //        {
                //            if (pt.Row == k + 1 && dgvBox.RowCount - pt.Height + 1== dgvBox.RowCount - i)
                //            {
                //                SpecBox box = new SpecBox();
                //                box = dicBoxPos[pt];
                //                string boxBarCode = box.BoxBarCode;
                //                DataGridViewCell cell = dgvBox[k,dgvBox.RowCount- i - 1];
                //                cell.Value = box.BoxBarCode;
                //                cell.Tag = box;
                //                string toolTipText = "λ�ã���" + box.DesCapRow.ToString() + " ��,��" + box.DesCapSubLayer.ToString() + " ��\n";
                //                toolTipText += "�������ͣ�" + disTypeManage.GetDisTypeByBoxId(box.BoxId.ToString()).DiseaseName + "\n";
                //                toolTipText += "�걾���ͣ�" + specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString()).SpecTypeName;
                //                cell.ToolTipText = toolTipText;
                //                if (box.SpecialUse == "8")
                //                {
                //                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                //                    style.BackColor = Color.SkyBlue;
                //                    cell.Style = style;
                //                }
                //                if (box.SpecialUse == "1")
                //                {
                //                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                //                    style.BackColor = Color.Orange;
                //                    cell.Style = style;
                //                }
                //                break;
                //            }
                //            index++;
                //        }
                //        if (index >= dicBoxPos.Keys.Count)
                //        {
                //            DataGridViewCell cell = dgvBox[k, dgvBox.RowCount - i -1];
                //            cell.Value = "��";
                //            cell.Tag = null;
                //            string toolTipText = "��λ��û�ű걾��";
                //            cell.ToolTipText = toolTipText;
                //        }
                //    }
                //}
                #endregion
            }
            catch
            {

            }
                #endregion
        }

        /// <summary>
        /// ��¼���ӻ��ߺ����ƶ���ԭʼλ�ü���λ��
        /// </summary>
        private void SetSheetView()
        {
            sheetView.AutoGenerateColumns = false;
            sheetView.DataAutoSizeColumns = false;
            sheetView.Rows.Count = 0;
            sheetView.Columns.Count = 6;            
            sheetView.ColumnHeader.Rows.Count = 2;
            sheetView.ColumnHeader.Cells[1,Convert.ToInt32(SheetCols.����)].Text = SheetCols.����.ToString();
            sheetView.Columns[Convert.ToInt32(SheetCols.����)].Label = SheetCols.����.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.ԭ����)].Text = SheetCols.ԭ����.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.ԭλ��)].Text = SheetCols.ԭλ��.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.������)].Text = SheetCols.������.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.��λ��)].Text = SheetCols.��λ��.ToString();

            for (int i = 0; i < sheetView.Columns.Count; i++)
            {
                sheetView.Columns[i].Width = 145;
                sheetView.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                sheetView.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
        }

        /// <summary>
        /// �������ݵ�SheetView��
        /// </summary>
        /// <param name="type">����</param>
        /// <param name="oldBarCode">������</param>
        /// <param name="oldLoc">��λ��</param>
        /// <param name="newBarCode">������</param>
        /// <param name="newLoc">��λ��</param>
        private void AddToSheet(string type, string oldBarCode, string oldLoc, string newBarCode, string newLoc)
        {
            int rowIndex = sheetView.Rows.Count;
            sheetView.Rows.Count = rowIndex + 1;
            int i = Convert.ToInt32(SheetCols.��λ��);
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.����)].Text = type;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.ԭ����)].Text = oldBarCode;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.ԭλ��)].Text = oldLoc;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.������)].Text = newBarCode;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.��λ��)].Text = newLoc;
            this.isPrint = false;
        }

        /// <summary>
        /// �����ƶ����������ض�λ
        /// </summary>
        private bool ShelfLocate(IceBoxLayer iceBoxLayer, Shelf shelf, IceBoxLayer oldLayer)
        {
            string type = "����";
            string oldBarCode = shelf.SpecBarCode;
            string oldLoc = ParseLocation.ParseShelf(oldBarCode);
            string newBarCode = "";
            string newLoc = "";

            iceBoxLayer = layerManage.GetLayerById(iceBoxLayer.LayerId.ToString());
            if (oldLayer == null || oldLayer.LayerId <= 0)
            {
                return false;
            }
            if (iceBoxLayer == null || iceBoxLayer.LayerId <= 0)
            {
                return false;
            }
            if (iceBoxLayer.LayerId == oldLayer.LayerId)
            {
                return false;
            }
            if ((iceBoxLayer.OccupyCount == iceBoxLayer.Capacity) || iceBoxLayer.IsOccupy == (short)1)
            {
                MessageBox.Show("�ñ��������");
                return false;
            }
            if (iceBoxLayer.SpecID != shelf.ShelfSpec.ShelfSpecID)
            {
                MessageBox.Show("���ӹ�����");
                return false;
            }
            //if (specTypeId != 9 && disTypeId != 16)
            if (iceBoxLayer.SpecTypeID != 9)
            {
                if (iceBoxLayer.SpecTypeID != shelf.SpecTypeId)
                {
                    MessageBox.Show("�걾���Ͳ������");
                    return false;
                }
            }
            if (iceBoxLayer.DiseaseType.DisTypeID != 16)
            {
                if (iceBoxLayer.DiseaseType.DisTypeID != shelf.DisTypeId)
                {
                    MessageBox.Show("�걾���ֲ������");
                    return false;
                }
            }

            iceBoxLayer = layerManage.GetLayerById(iceBoxLayer.LayerId.ToString());
            oldLayer = layerManage.GetLayerById(oldLayer.LayerId.ToString());
            ArrayList arrShelf = new ArrayList();
            arrShelf = shelfManage.GetShelfByLayerID(iceBoxLayer.LayerId.ToString());
            shelf.IceBoxLayer.Row = 1;
            shelf.IceBoxLayer.Height = 1;
            shelf.IceBoxLayer.LayerId = iceBoxLayer.LayerId;
            if (arrShelf.Count == 0)
            {
                shelf.IceBoxLayer.Col = 1;
            }
            Shelf lastShelf = shelfManage.ScanLayer(arrShelf);
            shelf.IceBoxLayer.Col = lastShelf.IceBoxLayer.Col + 1;           
            shelf.SpecBarCode = "BX" + iceBoxLayer.IceBox.IceBoxId.ToString().PadLeft(3, '0');
            shelf.SpecBarCode += "-C" + iceBoxLayer.LayerNum.ToString().PadLeft(1, '0');
            shelf.SpecBarCode += "-J" + shelf.IceBoxLayer.Col.ToString().PadLeft(1, '0');
            newBarCode = shelf.SpecBarCode;
            newLoc = ParseLocation.ParseShelf(newBarCode);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (shelfManage.UpdateShelf(shelf) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ��");
                    return false;
                }
                int occupyCount = 0;
                occupyCount = iceBoxLayer.OccupyCount + 1;
                if (layerManage.UpdateOccupyCount(occupyCount.ToString(),iceBoxLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ��");
                    return false;
                }
                occupyCount = oldLayer.OccupyCount -1;
                occupyCount = occupyCount < 0 ? 0 : occupyCount;
                if (layerManage.UpdateOccupyCount(occupyCount.ToString(), oldLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ��");
                    return false;
                }
                if (iceBoxLayer.OccupyCount == iceBoxLayer.Capacity)
                {
                    if (layerManage.UpdateOccupyCount("1", iceBoxLayer.LayerId.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ��");
                        return false;
                    }
                }
                if(layerManage.UpdateOccupy("0",oldLayer.LayerId.ToString())<=0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ��");
                    return false;
                }
                ArrayList arrBox = specBoxManage.GetBoxByCap(shelf.ShelfID.ToString(), 'J');
                if (arrBox != null && arrBox.Count > 0)
                {
                    foreach (SpecBox b in arrBox)
                    {
                        string barCode = b.BoxBarCode.Replace(b.BoxBarCode.Substring(0, 11), shelf.SpecBarCode);
                        b.BoxBarCode = barCode;
                        if (specBoxManage.UpdateSpecBox(b) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ��");
                            return false;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.AddToSheet(type, oldBarCode, oldLoc, newBarCode, newLoc);
                MessageBox.Show("�����ɹ�");
                return true;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return false;
            }           
        }
 
         /// <summary>
        /// ��λ�걾��
        /// </summary>
        private bool BoxLocate(Shelf shelf , SpecBox box)
        {
            string type = "����";
            string oldBarCode = box.BoxBarCode;
            string oldLoc = ParseLocation.ParseSpecBox(oldBarCode);
            string newBarcode = "";
            string newLoc = "";
            shelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");
            if (shelf.Capacity == shelf.OccupyCount)
            {
                MessageBox.Show("��������!");
                return false;
            }
            ShelfSpec shelfSpec = new ShelfSpec();
            ShelfSpecManage shelfSpecManage = new ShelfSpecManage();
            shelfSpec = shelfSpecManage.GetShelfByID(shelf.ShelfSpec.ShelfSpecID.ToString());
            Shelf oldShelf = new Shelf();
            oldShelf = shelfManage.GetShelfByShelfId(box.DesCapID.ToString(),"");
            shelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");
            if (shelfSpec == null || oldShelf==null || oldShelf.ShelfID <= 0)
            {
                MessageBox.Show("��ȡ��Ϣʧ��!");
                return false;
            }            
            if (shelfSpec.BoxSpec.BoxSpecID != box.BoxSpec.BoxSpecID)
            {
                MessageBox.Show("�걾�й��һ��!");
                return false;
            }
            if (shelf.SpecTypeId != 9)
            {
                if (box.SpecTypeID != shelf.SpecTypeId)
                {
                    MessageBox.Show("�걾���Ͳ������");
                    return false;
                }
            }
            if (shelf.DisTypeId != 16)
            {
                if (box.DiseaseType.DisTypeID != shelf.DisTypeId)
                {
                    MessageBox.Show("�걾���ֲ������");
                    return false;
                }
            }          
             SpecBox lastSpecBox = new SpecBox(); 
             lastSpecBox = specBoxManage.ShelfGetLastCapBox(shelf.ShelfID.ToString());                           
               // currentShelf = shelfManage
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
                }
                if (lastCol == shelfCol && lastHeight < shelfHeight)
                {
                    box.DesCapCol = 1;
                    box.DesCapSubLayer = lastHeight + 1;
                    box.DesCapRow = lastRow;                 
                }
                if (lastCol == shelfCol && lastHeight == shelfHeight && lastRow < shelfRow)
                {
                    box.DesCapCol = 1;
                    box.DesCapSubLayer = 1;
                    box.DesCapRow = lastSpecBox.DesCapRow + 1;                  
                }          
                string boxBarCode = shelf.SpecBarCode;// +currentShelf.IceBoxLayer.Col.ToString().PadLeft(2, '0');
                boxBarCode += "-" + box.DesCapRow.ToString().PadLeft(1, '0');
                boxBarCode += box.DesCapSubLayer.ToString().PadLeft(1, '0');
                box.BoxBarCode = boxBarCode;
                newBarcode = boxBarCode;
                newLoc = ParseLocation.ParseSpecBox(newBarcode);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                bool isFull = false;

                int occupyCount = oldShelf.OccupyCount - 1;
                if (shelfManage.UpdateOccupyCount(occupyCount.ToString(), oldShelf.ShelfID.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return false;
                }
                if (oldShelf.IsOccupy == '1')
                {
                    if (shelfManage.UpdateIsFull("0", oldShelf.ShelfID.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�");
                        return false;
                    }
                }
                occupyCount = shelf.OccupyCount + 1;
                if (shelfManage.UpdateOccupyCount(occupyCount.ToString(), shelf.ShelfID.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return false;
                }
                box.DesCapID = shelf.ShelfID;
                if (specBoxManage.UpdateSpecBox(box) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return false;
                }
                if (occupyCount == shelf.Capacity)
                {
                    isFull = true;
                    if (shelfManage.UpdateIsFull("1", shelf.ShelfID.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�");
                        return false;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit(); 
                this.AddToSheet(type, oldBarCode, oldLoc, newBarcode, newLoc);
                MessageBox.Show("�����ɹ�");
                if (isFull)
                {
                    DialogResult diaRe = MessageBox.Show("�ö��������������µĶ����!","��Ӽ���",MessageBoxButtons.YesNo);
                    if (diaRe == DialogResult.No)
                    {
                        return true;
                    }
                    frmSpecShelf frmShelf = new frmSpecShelf();
                    frmShelf.LayerId = shelf.IceBoxLayer.LayerId;
                    frmShelf.ShowDialog();
                }
                return true;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�");
                return false;
            }       

        }

        /// <summary>
        /// ��ȡ�걾�Ĳ���������ɫ�ͱ걾����
        /// </summary>
        /// <param name="boxId">�걾��ID</param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        /// <param name="disColor"></param>
        /// <param name="specTypeName"></param>
        private void GetSubSpecDet(string boxId, string endRow, string endCol,  ref string specTypeName)
        {
            string[] parms = new string[] { boxId, endRow, endCol };
            string sql = " select DISTINCT SPEC_DISEASETYPE.DISEASECOLOR,SPEC_TYPE.SPECIMENTNAME \n" +
                       " from SPEC_SUBSPEC LEFT JOIN SPEC_SOURCE ON SPEC_SUBSPEC.SPECID = SPEC_SOURCE.SPECID \n" +
                       " left join SPEC_DISEASETYPE on SPEC_SOURCE.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID\n" +
                       " LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SUBSPEC.SPECTYPEID\n" +
                       " where boxid={0} AND SPEC_SUBSPEC.BOXENDROW={1} AND SPEC_SUBSPEC.BOXENDCOL={2}";
            string Sql = string.Format(sql, parms);
            DataSet ds = new DataSet();
            subSpecManage.ExecQuery(Sql, ref ds);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                //disColor = dt.Rows[0]["DISEASECOLOR"].ToString();
                specTypeName = dt.Rows[0]["SPECIMENTNAME"].ToString();
            }

        }

        private void ucContainer_Load(object sender, EventArgs e)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ر�����Ϣ�����Ժ�...");
                Application.DoEvents();
                InitTree();
                tvIceBox.SelectedImageIndex = 1;
                tvIceBox.ImageIndex = 0;
                grpSpecInfo.Visible = false;
                grpSpecBoxInfo.Visible = false;
                if (curShelf != null && curShelf.ShelfID > 0)
                {
                    grpSpecInfo.Visible = true;
                    grpSpecBoxInfo.Visible = true;
                }
                if (curBox != null && curBox.BoxId > 0)
                {
                    grpSpecInfo.Visible = true;
                    grpSpecBoxInfo.Visible = true;
                }

                this.tvIceBox.AllowDrop = true;
                SetSheetView();
                this.BindingDisType();
                this.BindingSpecType();
            }
            catch
            { }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            //nlvSpecContainer.Visible = false;
        }

        private void dgvBox_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void lv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lv_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))            
            {
                Point p = this.tvIceBox.PointToClient(new Point(e.X, e.Y));
                TreeViewHitTestInfo index = tvIceBox.HitTest(p);

                if (index == null || index.Node == null || index.Node.Tag == null)
                {
                    return;
                }
                if (!index.Node.Tag.GetType().ToString().Contains("IceBoxLayer"))
                {
                    return;
                }
                if (index.Node != null)
                {
                    TreeNode tnLayer = index.Node;
                    tvIceBox.SelectedNode = tnLayer;
                    TreeNode tnTmp = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (tnTmp == null || tnTmp.Tag == null)
                    {
                        return;
                    }
                    if (!tnTmp.Tag.GetType().ToString().Contains("Shelf"))
                    {
                        return;
                    }
                    bool succeed = this.ShelfLocate(tnLayer.Tag as IceBoxLayer, tnTmp.Tag as Shelf, tnParent.Tag as IceBoxLayer);
                    if (succeed)
                    {
                        tnParent.Nodes.Remove(tnTmp);
                        IceBoxLayer tmpLayer = layerManage.GetLayerById((tnParent.Tag as IceBoxLayer).LayerId.ToString());
                        if (tmpLayer != null)
                        {
                            tnParent.Text = "�� " + tmpLayer.LayerNum + " �� (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
                        }
                        tnLayer.Nodes.Add(tnTmp);

                        tmpLayer = new IceBoxLayer();
                        tmpLayer = layerManage.GetLayerById((tnLayer.Tag as IceBoxLayer).LayerId.ToString());
                        if (tmpLayer != null)
                        {
                            tnLayer.Text = "�� " + tmpLayer.LayerNum + " �� (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
                        }
                        Shelf s = (tnTmp.Tag as Shelf);
                        if (s != null)
                        {
                            tnTmp.Text = s.SpecBarCode.Substring(9, 2) + " (" + s.OccupyCount.ToString() + "/" + s.Capacity.ToString() + ")"; 

                        }//InitTree();
                    }
                    tnParent = new TreeNode();
                    //DataGridViewCell drv = (DataGridViewCell)e.Data.GetData(typeof(DataGridViewCell));
                    //index.Node.Text = drv.Value.ToString();
                }
            }
            if (e.Data.GetDataPresent(typeof(SpecBox)))
            {
                if (isDrag)
                {
                    return;
                }
                Point p = tvIceBox.PointToClient(new Point(e.X, e.Y));
                TreeViewHitTestInfo index = tvIceBox.HitTest(p);
                if (index.Node == null || index.Node.Tag == null || !index.Node.Tag.GetType().ToString().Contains("Shelf"))
                {
                    return;
                }

                this.tvIceBox.SelectedNode = index.Node;
                //TreeNode tnd = (TreeNode)e.Data.GetData(typeof(TreeNode));
                SpecBox box = (SpecBox)e.Data.GetData(typeof(SpecBox));
                if (box == null)
                {
                    return;
                }
                
                if (BoxLocate(index.Node.Tag as Shelf, box))
                {
                    dgvBox.Rows[rowIndex].Cells[colIndex].Tag = null;
                    dgvBox.Rows[rowIndex].Cells[colIndex].Value = "��";
                    Shelf s = shelfManage.GetShelfByShelfId((index.Node.Tag as Shelf).ShelfID.ToString(),"");
                    if (s != null)
                    {
                        index.Node.Text = s.SpecBarCode.Substring(9, 2) + " (" + s.OccupyCount.ToString() + "/" + s.Capacity.ToString() + ")"; 
                    }
                    s = new Shelf();
                    s = shelfManage.GetShelfByShelfId((tnParent.Tag as Shelf).ShelfID.ToString(),"");
                    if (s != null)
                    {
                        tnParent.Text = s.SpecBarCode.Substring(9, 2) + " (" + s.OccupyCount.ToString() + "/" + s.Capacity.ToString() + ")"; 
                    }
                    
                    rowIndex = -1;
                    colIndex = -1;
                    //TreeClick(null, null);
                   // InitTree();                   
                }
                tnParent = new TreeNode();
                isDrag = true;
            }
        }

        private void lv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = tvIceBox.SelectedNode;
                if (tn == null || tn.Tag == null)
                {
                    return;
                }
                if (!tn.Tag.GetType().ToString().Contains("Shelf"))
                {
                    return;
                }
                if (tn != null)
                {
                    tnParent = new TreeNode();
                    tnParent = tn.Parent;
                    tvIceBox.DoDragDrop(tn, DragDropEffects.Move);
                }
            }
        }

        private void dgvBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string tag = grpSpecBoxInfo.Tag.ToString();
                if (tag == null || tag == "I")
                    return;
            }
            catch
            { }
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = this.dgvBox.HitTest(e.X, e.Y);
                if (info.RowIndex != -1 && info.ColumnIndex != -1)
                {
                    SpecBox box = this.dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag as SpecBox;
                    if (box != null)
                    {
                        isDrag = false;
                        rowIndex = info.RowIndex;
                        colIndex = info.ColumnIndex;
                        //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = null;
                        this.DoDragDrop(box, DragDropEffects.Move);
                    }
                }
            }
        }

        private void dgvSpec_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = this.dgvSpec.HitTest(e.X, e.Y);
                if (info.RowIndex != -1 && info.ColumnIndex != -1)
                {
                    SubSpec sub = this.dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag as SubSpec;
                    if (sub != null)
                    {
                        specRowIndex = info.RowIndex;
                        specColIndex = info.ColumnIndex;
                        //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = null;
                        this.DoDragDrop(sub, DragDropEffects.Move);
                    }
                }
            }
        }

        private void dgvBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string tag = grpSpecBoxInfo.Tag.ToString();
                if (tag == null || tag == "I")
                    return;
            }
            catch
            { }
            string type = "����";
            string oldBarCode = "";
            string oldLoc = "";
            string newBarCode = "";
            string newLoc = "";

            Point p = this.dgvBox.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo info = this.dgvBox.HitTest(p.X, p.Y);

            #region �ƶ��걾
            if (e.Data.GetDataPresent(typeof(SubSpec)))
            {
                SpecBox desBox = dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag as SpecBox;
                desBox = specBoxManage.GetBoxById(desBox.BoxId.ToString());
                if (desBox == null)
                {
                    MessageBox.Show("��ȡĿ�ı걾��ʧ��!");
                    return;
                }
                SubSpec sub = (SubSpec)e.Data.GetData(typeof(SubSpec));
                if (sub == null)
                {
                    return;
                }

                if (desBox.SpecTypeID != sub.SpecTypeId)
                {
                    MessageBox.Show("�걾���Ͳ����!");
                    return;
                }

                if (desBox.OccupyCount == desBox.Capacity)
                {
                    MessageBox.Show("�걾������!");
                    return;
                }

                SpecBox sourceBox = specBoxManage.GetBoxById(sub.BoxId.ToString());
                if (sourceBox == null)
                {
                    MessageBox.Show("��ȡԭ�걾��ʧ��!");
                    return;
                }

                if (desBox.BoxId == sourceBox.BoxId)
                {
                    return;
                }

                SpecInOper tmpIn = new SpecInOper();                       //BX005-C5-J1-11

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                tmpIn.Trans = FS.FrameWork.Management.PublicTrans.Trans;
                tmpIn.SetTrans();

                try
                {

                    if (tmpIn.TransferSpec(sourceBox, desBox, sub) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�");
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }
                DataGridViewCellEventArgs te = new DataGridViewCellEventArgs(info.ColumnIndex, info.RowIndex);
                dgvBox.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgvBox[info.ColumnIndex, info.RowIndex].Selected = true;                
                this.dgvBox_CellDoubleClick(sender, te);
                return;
            }
            #endregion

            if (info.RowIndex != -1 && info.ColumnIndex != -1 && rowIndex >= 0 && colIndex >= 0)
            {


                if (dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag != null)
                {
                   return;
                }
                #region �ƶ��걾��
                if (e.Data.GetDataPresent(typeof(SpecBox)))
                {
                    SpecBox box = (SpecBox)e.Data.GetData(typeof(SpecBox));
                    if (box == null)
                    {
                        return;
                    }
                    if ((info.ColumnIndex + 1 == box.DesCapRow) && (dgvBox.RowCount - info.RowIndex == box.DesCapSubLayer))
                    {
                        return;
                    }

                    try
                    {
                        //BX005-C5-J1-11
                        oldBarCode = box.BoxBarCode;
                        oldLoc = ParseLocation.ParseSpecBox(oldBarCode);
                        string boxBarCode = box.BoxBarCode.Substring(0, 12);
                        box.DesCapRow = info.ColumnIndex + 1;
                        box.DesCapSubLayer = dgvBox.RowCount - info.RowIndex;
                        boxBarCode += box.DesCapRow.ToString().PadLeft(1, '0');
                        boxBarCode += box.DesCapSubLayer.ToString().PadLeft(1, '0');
                        box.BoxBarCode = boxBarCode;
                        newBarCode = boxBarCode;
                        newLoc = ParseLocation.ParseSpecBox(newBarCode);

                        if (specBoxManage.UpdateSpecBox(box) <= 0)
                        {
                            MessageBox.Show("����ʧ�ܣ�");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("����ʧ�ܣ�");
                        return;
                    }
                    dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = dgvBox.Rows[rowIndex].Cells[colIndex].Value; ;
                    dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag = box;
                    dgvBox.Rows[rowIndex].Cells[colIndex].Tag = null;
                    dgvBox.Rows[rowIndex].Cells[colIndex].Value = "��";
                    rowIndex = -1;
                    colIndex = -1;
                    this.AddToSheet(type, oldBarCode, oldLoc, newBarCode, newLoc);
                }
                #endregion
                //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = value;
            }
        }

        private void dgvSpec_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.dgvSpec.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo info = this.dgvSpec.HitTest(p.X, p.Y);
            if (info.RowIndex != -1 && info.ColumnIndex != -1 && specRowIndex >= 0 && specColIndex >= 0)
            {
                if (dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag != null)
                {
                    return;
                }
                SubSpec spec = (SubSpec)e.Data.GetData(typeof(SubSpec));
                if (spec == null)
                {
                    return;
                }
                if ((info.ColumnIndex + 1 == spec.BoxEndRow) && (info.RowIndex + 1 == spec.BoxEndCol))
                {
                    return;
                }

                try
                {
                    //BX005-C5-J1-11
                    
                    string row = (info.RowIndex + 1).ToString();
                    string col = (info.ColumnIndex + 1).ToString();
                 
                    spec.BoxEndCol = info.ColumnIndex + 1;
                    spec.BoxEndRow = info.RowIndex + 1;
                    spec.BoxStartCol = info.ColumnIndex + 1;
                    spec.BoxStartRow = info.RowIndex + 1;                     

                    if (subSpecManage.UpdateSubSpec(spec) <= 0)
                    {
                        MessageBox.Show("����ʧ�ܣ�");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }
                dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = spec.SubBarCode;
                dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag = spec;
                dgvSpec.Rows[specRowIndex].Cells[specColIndex].Tag = null;
                dgvSpec.Rows[specRowIndex].Cells[specColIndex].Value = "��";
                specRowIndex = -1;
                specColIndex = -1;                
                //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = value;
            }
        }

        private void dgvBox_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                string tag = grpSpecBoxInfo.Tag.ToString();
                if (tag == null || tag == "I")
                    return;
            }
            catch
            { }
            e.Effect = DragDropEffects.Move;
        }

        private void dgvSpec_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return base.Query(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            try
            {
                ucLocPrint print = new ucLocPrint();
                print.SheetTitle = "�¾�λ�ö���";
                print.SheetView = this.sheetView;
                print.SetSheet();
                print.Print();
                isPrint = true;
                this.sheetView.Rows.Count = 0;
            }
            catch
            {
                isPrint = false;
            }
            return base.Print(sender, neuObject);
        }

        public override int Exit(object sender, object neuObject)
        {
            if (!isPrint)
            {
                Print(null, null);
                isPrint = true;
                sheetView.Rows.Count = 0;
            }

            return base.Exit(sender, neuObject);
        }

        private void dgvBox_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSpec.Rows.Clear();
            dgvSpec.Columns.Clear();
            dicSubSpec.Clear();

            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }

            DataGridViewCell cell = dgvBox[e.ColumnIndex, e.RowIndex];

            if (cell.Value == "��λ��")
            {
                MessageBox.Show("��λ�����û���������ý�ȥ���ˣ�");
                return;
            }

            if (grpSpecBoxInfo.Tag == null)
            {
                return;
            }
            string curTag = grpSpecBoxInfo.Tag.ToString();
            //����Ǳ���
            if (curTag == "I")
            {
                try
                {
                    Shelf tmpShelf = cell.Tag as Shelf;
                    this.SetContainer(null, tmpShelf);
                    grpSpecBoxInfo.Text = ParseLocation.ParseShelf(tmpShelf.SpecBarCode) + " �걾��λ����Ϣ";
                    grpSpecBoxInfo.Tag = "";                    
                }
                catch
                { }
                return;
            }

            #region
            try
            {
                if (cell == null)
                {
                    return;
                }


                if (cell.Tag == null)
                {
                    return;
                }
                if (cell.Tag != null)
                {
                    SpecBox currentBox = cell.Tag as SpecBox;
                    ArrayList arrSpec = subSpecManage.GetSubSpecInOneBox(currentBox.BoxId.ToString());
                    BoxSpec spec = boxSpecManage.GetSpecByBoxId(currentBox.BoxId.ToString());
                    grpSpecInfo.Text = "����: " + ParseLocation.ParseSpecBox(currentBox.BoxBarCode) + " �б걾λ������";
                    Size size = new Size();
                    if (spec != null)
                    {
                        if (spec.Row > 2)
                        {
                            size.Width = 60 + spec.Col * 60;
                            grpSpecInfo.Width = (size.Width + 60);
                            size.Height = 30 + spec.Row * 30;
                            dgvSpec.Size = size;
                        }
                        for (int c = 1; c <= spec.Col; c++)
                        {
                            //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();
                            DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                            imgDcl.HeaderText = c.ToString();
                            imgDcl.Width = 90;
                            dgvSpec.Columns.Add(imgDcl);
                        }
                        for (int r = 1; r <= spec.Row; r++)
                        {
                            DataGridViewRow dr = new DataGridViewRow();
                            dr.Height = 30;
                            DataGridViewHeaderCell headerCell = dr.HeaderCell;

                            dr.HeaderCell.Value = Convert.ToChar(64 + r).ToString();
                            dgvSpec.Rows.Add(dr);
                        }
                    }
                    if (arrSpec == null)
                    {
                        return;
                    }
                    foreach (SubSpec s in arrSpec)
                    {
                        Position p = new Position(s.BoxEndRow.ToString(), s.BoxEndCol.ToString());
                        dicSubSpec.Add(p, s);
                    }
                    //����DataGridView�е�ÿ��λ�ã����SpecBox��DataGridView�ж�Ӧ�ĸߺ��У����ظñ걾����Ϣ
                    #region
                    for (int i = 0; i < dgvSpec.Rows.Count; i++)
                    {
                        for (int k = 0; k < dgvSpec.Columns.Count; k++)
                        {
                            int row = i + 1;
                            int col = k + 1;
                            int index = 0;
                            foreach (Position pt in dicSubSpec.Keys)
                            {
                                if (row == pt.Row && col == pt.Col)
                                {
                                    SubSpec sub = new SubSpec();
                                    sub = dicSubSpec[pt];
                                    DataGridViewCell cell1 = dgvSpec[k, i];
                                    if (sub.SubBarCode == "" && sub.StoreID == 0)
                                    {
                                        cell1.Value = "��";
                                        cell1.Tag = null;
                                        string toolTipText1 = "��λ��û�ű걾";
                                        cell1.ToolTipText = toolTipText1;
                                        break;
                                    }
                                    cell1.Value = sub.SubBarCode;
                                    //cell1.Value = Image.FromFile("Spec.bmp");
                                    string color = "";
                                    string specTypeName = "";
                                    //this.GetSubSpecDet(currentBox.BoxId.ToString(), sub.BoxEndRow.ToString(), sub.BoxEndCol.ToString(), ref specTypeName);
                                    cell1.Tag = sub;
                                    string toolTipText = "λ�ã���" + sub.BoxEndRow.ToString() + " ��,��" + sub.BoxEndCol.ToString() + " ��\n";
                                    toolTipText += "�걾���ͣ�" + specTypeName + "\n";
                                    cell1.ToolTipText = toolTipText;
                                    break;
                                }
                                index++;
                            }
                            if (index >= dicSubSpec.Keys.Count)
                            {
                                DataGridViewCell cell2 = dgvSpec[k, i];
                                cell2.Value = "��";
                                //cell2.Value = Image.FromFile("EmptySmall.bmp");
                                cell2.Tag = null;
                                string toolTipText = "��λ��û�ű걾";
                                cell2.ToolTipText = toolTipText;
                            }
                        }
                    }
                    #endregion

                }
                dgvSpec.Visible = true;
            }
            catch
            {

            }
            #endregion

        }

        private void dgvSpec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SubSpec tmp = dgvSpec.SelectedCells[0].Tag as SubSpec;                 

                 OrgTypeManage orgManage = new OrgTypeManage();
                string orgName = orgManage.GetBySpecType(tmp.SpecTypeId.ToString()).OrgName;

                  if(orgName.Contains("Ѫ"))
                    {
                        ucSpecimentSource ucBld = new ucSpecimentSource();
                        ucBld.SpecId = tmp.SpecId;
                        Size size = new Size();
                        size.Height = 800;
                        size.Width = 1280;
                        ucBld.Size = size;

                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucBld, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                        //FS.NFC.Interface.Classes.Function.PopForm popForm = new FS.NFC.Interface.Forms.BaseForm();
                 
                        
                    }
                    if (orgName.Contains("��֯"))
                    {
                        ucSpecSourceForOrg ucOrg = new ucSpecSourceForOrg();
                        ucOrg.SpecId = tmp.SpecId;                         
                       // FormBorderStyle formStyle = new FormBorderStyle();
                        Size size = new Size();
                        size.Height = 800;
                        size.Width = 1280;
                        ucOrg.Size = size;
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucOrg, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                        //FS.NFC.Interface.Classes.Function.PopForm popForm = new FS.NFC.Interface.Forms.BaseForm();
                        
                    }
 
            }
            catch
            {
                return;
            }

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                InitTree();
            }
        }

        /// <summary>
        /// ��ȡ�걾��ѯ����
        /// </summary>
        public void GetQuery()
        {
            int disTypeId = 0;
            if (cmbDisType.Text.Trim() != "")
            {
                disTypeId = Convert.ToInt32(cmbDisType.Tag.ToString());
            }

            int specTypeId = 0;
            if (cmbSpecType.Text.Trim() != "")
            {
                specTypeId = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
            }

            //�����б�
            arrIceBoxList = new ArrayList();
            //���б�
            arrLayerList = new ArrayList();
            //�����б�
            arrShelfList = new ArrayList();
            if (!chkTime.Checked)
            {
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetLayerByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
            }
            if (chkTime.Checked)
            {
                string start = dtpStart.Value.Date.ToString();
                string end = dtpEnd.Value.Date.AddDays(1.0).ToString();
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                if (string.IsNullOrEmpty(cmbDisType.Text.Trim().ToString()) && string.IsNullOrEmpty(cmbSpecType.Text.Trim().ToString()))
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTime(start, end);
                    arrLayerList = layerManage.GetIceBoxByStoreTime(start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTime(start, end);
                }
            }   
        }
    }

    sealed class Position
    {
        public int Row = 0;
        public int Height = 0;
        public int Col = 0;
        public Position(int r, int h)
        {
            Row = r;
            Height = h;
        }
        public Position(string r, string c)
        {
            Row = Convert.ToInt32(r);
            Col = Convert.ToInt32(c);
        }

    }

    sealed class SpecSort : IComparer<SubSpec>
    {
        int IComparer<SubSpec>.Compare(SubSpec s1, SubSpec s2)
        {
            return s1.SubSpecId.CompareTo(s2.SubSpecId); 
        }
    }

    sealed class BoxSort : IComparer<SpecBox>
    {
        int IComparer<SpecBox>.Compare(SpecBox b1, SpecBox b2)
        {
            return b1.BoxBarCode.CompareTo(b2.BoxBarCode);
        }
    }
}
