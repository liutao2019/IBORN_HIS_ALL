using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class ucNorTmpContainer : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private IceBoxLayerManage layerManage;

        //�洢��������
        private IceBoxManage iceBoxManage;
        private BoxSpecManage specManage;
        private SpecBoxManage specBoxManage;
        private SubSpecManage subSpecManage;
        private CapLogManage capLogManage;

        private BoxSpec boxSpec;
        private IceBox icebox;

        //�洢��ʵ���б�
        private ArrayList arrIcebox;
        private Dictionary<Position, SubSpec> dicSubSpec;
        //��Ҫ�޸ĵĲ��б�
        private List<IceBoxLayer> layerList;

        private FS.HISFC.Models.Base.Employee loginPerson;
        private string title;
        private string iceBoxName;
        //A����ӣ�M���޸�
        private string oper;

        private int rowIndex = -1;
        private int colIndex = -1;

        //�걾λ�ں����е��У������������ں����еı걾����
        private int specRowIndex = -1;
        private int specColIndex = -1;

        public ucNorTmpContainer()
        {
            InitializeComponent();
            layerManage = new IceBoxLayerManage();
            iceBoxManage = new IceBoxManage();
            capLogManage = new CapLogManage();
            specBoxManage = new SpecBoxManage();
            subSpecManage = new SubSpecManage();

            boxSpec = new BoxSpec();
            icebox = new IceBox();
            layerList = new List<IceBoxLayer>();

            dicSubSpec = new Dictionary<Position, SubSpec>();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            arrIcebox = new ArrayList();

            loginPerson = new Employee();
            title = "�洢������";
            iceBoxName = "";
            oper = "";
        }

        private void EnableCon(bool e)
        {
            nupLayerNum.Enabled = e;
            cmbSpec.Enabled = e;
        }

        private void ClearCon()
        {
            cmbIceBox.Text = "";
            txtComment.Text = "";
            txtName.Text = "";
            cmbSpec.Text = "";
            nupLayerNum.Value = 1;
            cmbSpecType.Text = "";
            cmbDisType.Text = "";
        }

        /// <summary>
        /// �걾�й���
        /// </summary>
        private void BoxBinding()
        {
            specManage = new BoxSpecManage();
            Dictionary<int, string> dicBoxSpec = new Dictionary<int, string>();
            dicBoxSpec = specManage.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = dicBoxSpec;
            this.cmbSpec.DisplayMember = "Value";
            this.cmbSpec.ValueMember = "Key";
            this.cmbSpec.DataSource = bsTemp;
        }

        /// <summary>
        /// �걾���Ͱ�
        /// </summary>
        private void SpecTypeBinding()
        {            
            SpecTypeManage tmpMgr = new SpecTypeManage();
            ArrayList arrTmp = new ArrayList();
            arrTmp = tmpMgr.GetSpecByOrgName("��֯");//.GetAllBoxSpec();
            cmbSpecType.DataSource = arrTmp;
            this.cmbSpecType.ValueMember = "SpecTypeID";
            this.cmbSpecType.DisplayMember = "SpecTypeName";
            this.cmbSpecType.DataSource = arrTmp;
        }

        /// <summary>
        /// �������Ͱ�
        /// </summary>
        private void DisTypeBinding()
        {
            DisTypeManage tmpMgr = new DisTypeManage();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic = tmpMgr.GetOrgDisType();//.GetSpecByOrgName("��֯");//.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = dic;
            this.cmbDisType.DisplayMember = "Value";
            this.cmbDisType.ValueMember = "Key";
            this.cmbDisType.DataSource = bsTemp;
        }

        private void IceBoxBinding()
        {
            ArrayList arr = iceBoxManage.GetIceBoxByType("2");
            if (arr == null)
            {
                return;
            }
            cmbIceBox.DataSource = arr;
            this.cmbIceBox.ValueMember = "IceBoxId";
            this.cmbIceBox.DisplayMember = "IceBoxName";
            this.cmbIceBox.DataSource = arr;
        }

        /// <summary>
        /// ���ϴ洢��
        /// </summary>
        /// <param name="iceboxId"></param>
        private void DisuseIceBox(int iceboxId)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                SpecBoxManage tmpManage = new SpecBoxManage();
                tmpManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                ArrayList arrLayer = layerManage.GetIceBoxLayers(iceboxId.ToString());
                ArrayList arrBox;

                foreach (IceBoxLayer l in arrLayer)
                {
                    IceBoxLayer tmpl = l;

                    #region ���������Ǳ걾��
                    if (l.SaveType == 'B')
                    {
                        arrBox = new ArrayList();
                        arrBox = tmpManage.GetBoxByCap(l.LayerId.ToString(), 'B');
                        foreach (SpecBox b1 in arrBox)
                        {
                            //�ϳ��洢���еı걾��
                            SpecBox tmpb1 = b1;
                            tmpb1.DesCapCol = 0;
                            tmpb1.DesCapID = 0;
                            tmpb1.DesCapRow = 0;
                            tmpb1.DesCapSubLayer = 0;
                            tmpb1.DesCapType = '0';
                            tmpb1.IsOccupy = '0';
                            tmpb1.OccupyCount = 0;
                            if (tmpManage.UpdateSpecBox(tmpb1) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ʧ�ܣ�", title);
                                return;
                            }
                            if (capLogManage.ModifyBoxLog(b1, loginPerson.Name, "D", tmpb1, "�洢��ϳ�") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ʧ�ܣ�", title);
                                return;
                            }
                        }
                    }
                    #endregion

                    tmpl.LayerNum = 0;
                    tmpl.OccupyCount = 0;
                    tmpl.IceBox.IceBoxId = 0;
                    tmpl.SaveType = '0';
                    tmpl.SpecID = 0;
                    if (layerManage.UpdateLayer(tmpl) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�", title);
                        return;
                    }
                    int result = capLogManage.ModifyIceBoxLayer(l, loginPerson.Name, "D", tmpl, "�洢��ϳ�");
                    if (result <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�", title);
                        return;
                    }
                }

                IceBox tmpib = iceBoxManage.GetIceBoxById(iceboxId.ToString());
                tmpib.UseStaus = "0";
                tmpib.Comment = txtComment.Text;
                if (iceBoxManage.UpdateIceBox(tmpib) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�", title);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����ɹ���", title);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�", title);
            }
        }        

        /// <summary>
        /// ����
        /// </summary>
        private void SettingLayer(IceBoxLayer l)
        {
            try
            {
                SpecTypeManage tmpMgr = new SpecTypeManage();
                DisTypeManage disTmpMgr = new DisTypeManage();
                string specType = tmpMgr.GetSpecTypeById(l.SpecTypeID.ToString()).SpecTypeName;
                string disType = disTmpMgr.SelectDisByID(l.DiseaseType.DisTypeID.ToString()).DiseaseName; ;
                BoxSpec tmpSpec = specManage.GetSpecById(l.SpecID.ToString());
                string spec = tmpSpec.Row.ToString() + "��" + tmpSpec.Col.ToString();
                if (specType == null)
                {
                    MessageBox.Show("��ȡ�걾����ʧ�ܣ�");
                    return;
                }

                if (disType == null)
                {
                    MessageBox.Show("��ȡ����ʧ�ܣ�");
                    return;
                }
                grpLayer.Text = icebox.IceBoxName + "  ��" + nupLayerNum.Value.ToString() + "��";
                dgv[0, l.LayerNum].Tag = l;
                dgv[0, l.LayerNum].Value = "��" + l.LayerNum.ToString() + "��  (" + disType + "," + specType + ") " + spec;

            }
            catch
            {
                MessageBox.Show("��ȡ��Ϣʧ�ܣ�");
                return;
            }

           
        }

        /// <summary>
        /// ����У��
        /// </summary>
        /// <returns></returns>
        private bool DataValidate()
        {
            iceBoxName = txtName.Text.TrimEnd().TrimStart();
            if (cmbSpec.SelectedValue == null || cmbSpec.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ����",title);
                return false;
            }
            if ((txtName.Text==""))// == null) || cmbSpecType.Text.Trim() != ""())
            {
                MessageBox.Show("����д����", title);
                return false;
            }
            IceBox tmp = iceBoxManage.GetIceBoxByName(iceBoxName);
            if (null != tmp && tmp.IceBoxName == iceBoxName)
            {
                MessageBox.Show("�����������ظ������������룡",title);
                return false;
            }
            icebox.IceBoxName = iceBoxName;
            return true;
        }

        /// <summary>
        /// �󶨴洢��
        /// </summary>
        private void BindingIceBox()
        {
            arrIcebox = iceBoxManage.GetIceBoxByType("2");
            arrIcebox.Add(new IceBox());
            cmbIceBox.DataSource = arrIcebox;
            cmbIceBox.DisplayMember = "IceBoxName";
            cmbIceBox.ValueMember = "IceBoxId";
            IceBoxBinding();
            //cmbIceBox.SelectedIndex = arrIcebox.Count - 1;
        }

        private void SettinLayerDet(ref IceBoxLayer l)
        {
            layerList.Add(l);
            this.SettingLayer(l);                                   
        }

        /// <summary>
        /// �ȱ���洢�񣬺󱣴�ÿһ�㣬��󱣴����
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            #region ����洢��
            icebox.IceBoxName = iceBoxName;
            icebox.Comment = txtComment.Text;
            
            if (oper == "M")
            {
                if (iceBoxManage.UpdateIceBox(icebox) <= 0)
                {
                    MessageBox.Show("���´洢��ʧ��", title);
                    return -1;
                }                
            }
            if (oper == "A")
            {
                icebox.IceBoxTypeId = "2";
                icebox.UseStaus = "1";
                icebox.SpecTypeId = "0";
                icebox.OrgOrBlood = "0";
                
                if (iceBoxManage.InsertIceBox(icebox) <= 0)
                {
                    MessageBox.Show("��Ӵ洢��ʧ��", title);
                    return -1;
                }
            }
            #endregion

            for (int i = 1; i < dgv.Rows.Count; i++)
            {                
                IceBoxLayer tmp = dgv.Rows[i].Cells[0].Tag as IceBoxLayer;
                if (tmp == null)
                {
                    return -1;
                }

                if (oper == "M")
                {
                    try
                    {
                        if (subSpecManage.GetSubSpecInLayerOrShelf(tmp.LayerId.ToString(), "B").Count > 0)
                        {
                            MessageBox.Show("�� " + i.ToString()+" ���б걾��� �����ܸ��ģ�", title);
                            continue;
                        }
                    }
                    catch
                    { }
                    SpecBox box = specBoxManage.GetBoxByCap(tmp.LayerId.ToString(), 'B')[0] as SpecBox;
                    if (box == null)
                    {
                        return -1;
                    }
                    if (layerManage.UpdateLayer(tmp) <= 0)
                    {
                        MessageBox.Show("���´洢��ʧ��", title);
                        return -1;
                    }

                    //���´洢��ÿһ���еĺ�����Ϣ
                    box.BoxBarCode = "BX" + tmp.IceBox.IceBoxId.ToString().PadLeft(3,'0') + "-C" + tmp.LayerNum.ToString();
                    //box.DesCapID = tmp.LayerId;
                    box.DiseaseType.DisTypeID = tmp.DiseaseType.DisTypeID;
                    box.SpecTypeID = tmp.SpecTypeID;
                    box.BoxSpec.BoxSpecID = tmp.SpecID;
                    if (specBoxManage.UpdateSpecBox(box) <= 0)
                    {
                        MessageBox.Show("���´洢��ʧ��", title);
                        return -1;
                    }
                }
                if (oper == "A")
                {
                    string seqL="";
                    layerManage.GetNextSequence(ref seqL);
                    tmp.LayerId = Convert.ToInt32(seqL);
                    tmp.IceBox = iceBoxManage.GetIceBoxByName(iceBoxName);
                    ;
                    if (layerManage.InsertIceBoxLayer(tmp) <= 0)
                    {
                        MessageBox.Show("��Ӵ洢��ʧ��", title);
                        return -1;
                    }
                    capLogManage.ModifyIceBoxLayer(new IceBoxLayer(), loginPerson.Name, "N", tmp, "�½������");
                    SpecBox newBox = new SpecBox();
                    newBox.OrgOrBlood = 2;
                    newBox.BoxBarCode = "BX" + tmp.IceBox.IceBoxId.ToString().PadLeft(3, '0') + "-C" + tmp.LayerNum.ToString();
                    newBox.DesCapID = tmp.LayerId;
                    newBox.DiseaseType.DisTypeID = tmp.DiseaseType.DisTypeID;
                    newBox.SpecTypeID = tmp.SpecTypeID;
                    string seq = "";
                    specBoxManage.GetNextSequence(ref seq);
                    newBox.BoxId = Convert.ToInt32(seq);
                    newBox.BoxSpec = boxSpec;
                    newBox.Capacity = boxSpec.Row * boxSpec.Col;
                    newBox.IsOccupy = '0';
                    newBox.InIceBox = '0';
                    newBox.ColNum = 1;
                    newBox.DesCapCol = 1;
                    newBox.DesCapRow = 1;
                    newBox.DesCapSubLayer = 1;
                    newBox.DesCapType = 'B';
                    newBox.OccupyCount = 0;
                    if (specBoxManage.InsertSpecBox(newBox) <= 0)
                    {
                        MessageBox.Show("��Ӵ洢��ʧ��", title);
                        return -1;
                    }
                    capLogManage.ModifyBoxLog(new SpecBox(), loginPerson.Name, "N", newBox, "�½��걾��");
                    //newBox.Capacity = 
                }
            }
            return 1;
        }

        public override int Save(object sender, object neuObject)
        {
            if (oper == "")
            {
                return 0;
            }

            IceBoxLayer tmp = dgv.Rows[0].Cells[0].Tag as IceBoxLayer;
            if (tmp != null)
            {
                MessageBox.Show("��һ�в��ܴ��");
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (Save() == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ��", title);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                oper = "";
                MessageBox.Show("�����ɹ�", title);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ��");
            } 
            return base.Save(sender, neuObject);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                frmNorTmpLayerSetting frm = new frmNorTmpLayerSetting();
                IceBoxLayer layer = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as IceBoxLayer;
                if (layer == null)
                {
                    MessageBox.Show("��ȡ��Ϣʧ�ܣ�");
                    return;
                }
                frm.BoxName = iceBoxName;
                frm.Layer = layer;
                frm.Show();
                frm.OnSetLayer += new frmNorTmpLayerSetting.SetLayer(SettinLayerDet);
            }
            catch
            { }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                dgv.Rows.Clear();
                this.SpecTypeBinding();
                this.BoxBinding();
                this.DisTypeBinding();
                this.SpecTypeBinding();
                this.BindingIceBox();
                ClearCon();
                EnableCon(false);

                DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                imgDcl.HeaderText = "�߶�";
                imgDcl.Width = 210;
                dgv.Columns.Add(imgDcl);
                dgv.Rows.Clear();
            }
            catch
            { }
            base.OnLoad(e);
        }
        /// <summary>
        /// �޸İ�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolBarService.AddToolButton("���", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("�޸�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "����":
                    if (cmbIceBox.SelectedValue == null || cmbIceBox.SelectedValue.ToString() == "0")
                    {
                        MessageBox.Show("��ȡ�洢��ʧ�ܣ�", title);
                        return;
                    }
                    int iceboxId = Convert.ToInt32(cmbIceBox.SelectedValue.ToString());
                    if (iceBoxManage.CheckIceBoxHaveSpecBox(iceboxId.ToString()) == 1)
                    {
                        MessageBox.Show("�洢���д��б걾�����ܷϳ�", title);
                        return;
                    }
                    DialogResult dialog = MessageBox.Show("�˲������ϳ��洢�������д�ŵı걾�ܺͱ걾����Ϣ��������", title, MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                        return;
                    DisuseIceBox(iceboxId);
                    BindingIceBox();
                    break;
                case "���":
                    oper = "A";
                    this.EnableCon(true);
                    this.ClearCon();
                    break;
                case "�޸�":
                    this.EnableCon(true);
                    oper = "M";
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            { 
                if (!DataValidate())
                {
                    return;
                }
                dgv.Rows.Clear();
                dgvSpec.Rows.Clear();
                icebox = new IceBox();
                icebox.LayerNum = Convert.ToInt32(nupLayerNum.Value.ToString());
                IceBoxLayer layer = new IceBoxLayer();
                layer.BloodOrOrgId = "2";
                layer.Capacity = 1;
                layer.IsOccupy = 1;
                layer.Col = 1;
                if (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "")
                {
                    layer.DiseaseType.DisTypeID = Convert.ToInt32(cmbDisType.SelectedValue.ToString());
                }
                layer.OccupyCount = 1;
                layer.Row = 1;
                layer.SaveType = 'B';
                if (cmbSpecType.SelectedValue != null && cmbSpecType.Text.Trim() != "")
                {
                    layer.SpecTypeID = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());

                }
                boxSpec = specManage.GetSpecById(cmbSpec.SelectedValue.ToString());
                layer.SpecID = boxSpec.BoxSpecID;

                for (int i = 0; i < icebox.LayerNum + 1; i++)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;
                    dgv.Rows.Add(dr);
                }
                // DataGridViewRow dr1 = new DataGridViewRow();
                //    dr1.Height = 40;
                //    dgv.Rows.Add(dr1); 
                for (int i = 0; i < icebox.LayerNum; i++)
                {
                    IceBoxLayer l = new IceBoxLayer();
                    l = layer.Clone();
                    l.LayerNum = (short)(i + 1);
                    this.SettingLayer(l);
                }
                dgv.Rows[0].Height = 40;
                dgv[0, 0].Tag = null;
                dgv[0, 0].Value = "������λ";
                dgv.Tag = icebox;
            }
            catch
            { }

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvSpec = new DataGridView();
            //dgvSpec.RowCount = 0; 
            //dgvSpec.ColumnCount = 0;
            dgvSpec.Rows.Clear();
            dgvSpec.Columns.Clear();
            dicSubSpec.Clear();
            IceBoxLayer layer = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as IceBoxLayer;
            if (layer == null || layer.LayerId<=0)
            {
                return;
            }
            SpecBox currentBox = new SpecBox();
            try
            {
                currentBox = specBoxManage.GetBoxByCap(layer.LayerId.ToString(), 'B')[0] as SpecBox;
            }
            catch
            {
                return;
            }
            dgvSpec.Tag = currentBox;
            if (currentBox == null || currentBox.BoxId <= 0)
            {
                return;
            }             

            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }

            #region
            try
            {
                    ArrayList arrSpec = subSpecManage.GetSubSpecInOneBox(currentBox.BoxId.ToString());
                    BoxSpec spec = specManage.GetSpecByBoxId(currentBox.BoxId.ToString());
                    Size size = new Size();
                    if (spec != null)
                    {
                        if (spec.Row > 2)
                        {
                            size.Width = 60 + spec.Col * 60;
                            size.Height = 30 + spec.Row * 30;
                            dgvSpec.Size = size;
                        }
                        for (int c = 1; c <= spec.Col; c++)
                        {
                            //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();
                            DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                            imgDcl.HeaderText = c.ToString();
                            imgDcl.Width = 75;
                            dgvSpec.Columns.Add(imgDcl);
                        }
                        for (int r = 1; r <= spec.Row; r++)
                        {
                            DataGridViewRow dr = new DataGridViewRow();
                            dr.Height = 30;
                            DataGridViewHeaderCell headerCell = dr.HeaderCell;

                            dr.HeaderCell.Value =r.ToString();
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
                                    //string color = "";
                                    string specTypeName = "";
                                    //this.GetSubSpecDet(currentBox.BoxId.ToString(), sub.BoxEndRow.ToString(), sub.BoxEndCol.ToString(), ref color, ref specTypeName);
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
            catch
            {

            }
            #endregion
        }

        private void cmbIceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbIceBox.Text.Trim() == "")
                {
                    return;
                }
                icebox = iceBoxManage.GetIceBoxById(cmbIceBox.SelectedValue.ToString());
                if (icebox == null || icebox.IceBoxId <= 0)
                {
                    return;
                }
                ArrayList arrLayer = layerManage.GetLayerInOneBox(icebox.IceBoxId.ToString());
                if (arrLayer == null || arrLayer.Count <= 0)
                {
                    return;
                }
                nupLayerNum.Value = icebox.LayerNum;
                iceBoxName = icebox.IceBoxName;
                txtComment.Text = icebox.Comment;
                txtName.Text = iceBoxName;

                dgv.Rows.Clear();
                //DataGridViewRow dr1 = new DataGridViewRow();
                //dr1.Height = 40;
                //dgv.Rows.Add(dr1);
                for (int i = 0; i < arrLayer.Count + 1; i++)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;
                    dgv.Rows.Add(dr);
                }
                foreach (IceBoxLayer l in arrLayer)
                {

                    //DataGridViewRow dr = new DataGridViewRow();
                    //dr.Height = 40;
                    //dgv.Rows.Add(dr); 
                    cmbSpec.SelectedValue = l.SpecID;
                    this.SettingLayer(l);
                }

                dgv.Rows[0].Height = 40;
                dgv[0, 0].Tag = null;
                dgv[0, 0].Value = "������λ";
            }
            catch
            { }
        }

        private void dgv_DragDrop(object sender, DragEventArgs e)
        { 
            Point p = this.dgv.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo info = this.dgv.HitTest(p.X, p.Y);
            if (info.RowIndex != -1 && info.ColumnIndex != -1 && rowIndex >= 0 && colIndex >= 0)
            {
                if (dgv.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag != null)
                {
                    return;
                }
                IceBoxLayer l = (IceBoxLayer)e.Data.GetData(typeof(IceBoxLayer));
                if (l == null || l.LayerId <= 0)
                {
                    return;
                }
                if (info.RowIndex == l.LayerNum)
                {
                    return;
                }

                try
                {
                    l.LayerNum = (short)(info.RowIndex);
                    string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                    int lI = cellValue.IndexOf("��");
                    string newCellValue = cellValue.Substring(lI);
                    string pre = "��" + l.LayerNum.ToString();

                    dgv.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = pre + newCellValue;
                    dgv.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag = l;
                    dgv.Rows[rowIndex].Cells[colIndex].Tag = null;
                    dgv.Rows[rowIndex].Cells[colIndex].Value = "��";
                    colIndex = -1;
                    rowIndex = -1;
                }
                catch
                {
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }
                
            }
        }

        private void dgv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = this.dgv.HitTest(e.X, e.Y);
                if (info.RowIndex != -1 && info.ColumnIndex != -1)
                {
                    IceBoxLayer l = this.dgv[info.ColumnIndex,info.RowIndex].Tag as IceBoxLayer;
                    if (l != null)
                    {
                        rowIndex = info.RowIndex;
                        colIndex = info.ColumnIndex;
                        this.DoDragDrop(l, DragDropEffects.Move);
                    }
                }
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
            }
        }

        private void dgvSpec_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
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
    }
}
