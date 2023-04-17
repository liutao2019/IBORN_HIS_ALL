using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.FrameWork.WinForms.Controls;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.BizLogic.Manager;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucIceBox : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //���ĸ���
        private int specCount;
        //����
        private int layerCount;
        //checkBox�ĸ���
        private int chkNum;
        //�����������
        private int specIndex;
        //checkBox������
        private int chkIndex;
        //TalbeLayoutPanel������
        private int tableCol;
        ////TalbeLayoutPanel������
        private int tableRow;
        //checkbox ����
        private NeuCheckBox[] chk;
        private ucIceBoxLayer[] ucLayer;
        //checkBox �Ƿ�ѡ��
        //private bool[] chkChecked;
        private IceBox tmpIcebox;
        //�����������
        private IceBox curSelectedIceBox;
        private IceBoxLayerManage layerManage;
        //����������
        private IceBoxManage iceBoxManage;
        //����ʵ���б�
        private ArrayList arrIcebox;
        private Dictionary<int, string> dicOrgType;
        private Dictionary<int, string> dicSpecType;
        private OrgTypeManage orgTypeManage;
        private SpecTypeManage specTypeManage;
        private ShelfManage shelfManage;
        private ShelfSpecManage shelfSpecManage;        
        private string title = "��������";
        private int LayerSetting = 0;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;      
        private CapLogManage capLogManage;
        private FS.HISFC.Models.Base.Employee loginPerson;

        public ucIceBox()
        {
            InitializeComponent();
            layerManage = new IceBoxLayerManage();
            iceBoxManage = new IceBoxManage();
            tmpIcebox = new IceBox();
            curSelectedIceBox = new IceBox();
            shelfManage = new ShelfManage();
            arrIcebox = new ArrayList();
            dicOrgType = new Dictionary<int, string>();
            dicSpecType = new Dictionary<int, string>();
            shelfSpecManage = new ShelfSpecManage();
            orgTypeManage = new OrgTypeManage();
            specTypeManage = new SpecTypeManage();
            capLogManage = new CapLogManage();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            tlpLayer.MouseWheel += this.tlpLayer_MouseWheel;
            loginPerson = new FS.HISFC.Models.Base.Employee();   
        }

        #region ��ʼ��ҳ�����ݰ�
        /// <summary>
        /// �󶨱걾����
        /// </summary>
        private void BindingSpecClass()
        {
            dicOrgType = orgTypeManage.GetAllOrgType();
            if (dicOrgType.Count > 0)
            {
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = dicOrgType;
                cmbOrgOrBlood.DisplayMember = "Value";
                cmbOrgOrBlood.ValueMember = "Key";
                cmbOrgOrBlood.DataSource = bsTmp;
            }
        }

        /// <summary>
        /// �󶨱���
        /// </summary>
        private void BindingIceBox(string typeId)
        {
            arrIcebox = iceBoxManage.GetIceBoxByType(typeId);
            if (arrIcebox != null)
            {
                if (arrIcebox.Count > 0)
                {
                    cmbIceBox.DataSource = arrIcebox;
                    cmbIceBox.DisplayMember = "IceBoxName";
                    cmbIceBox.ValueMember = "IceBoxId";
                    cmbIceBox.Text = "";
                }
            }
            //cmbIceBox.SelectedIndex = arrIcebox.Count - 1;
        }

        /// <summary>
        /// �󶨱걾����
        /// </summary>
        /// <param name="orgId"></param>
        private void BindingSpecType(string orgId)
        {
            dicSpecType.Clear();
            cmbSpecType.DataSource = null;
            dicSpecType = specTypeManage.GetSpecTypeByOrgID(orgId);
            if (dicSpecType.Count > 0)
            {
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = dicSpecType;
                cmbSpecType.DisplayMember = "Value";
                cmbSpecType.ValueMember = "Key";
                cmbSpecType.DataSource = bsTmp;
                cmbSpecType.SelectedIndex = 0;
            }
        }
        #endregion

        #region ��Ϣ��ȡ
        /// <summary>
        /// ���ݼ��ӹ��Id�õ����ӵ�����
        /// </summary>
        /// <param name="shelfSpecID">���ӵĹ��ID</param>
        /// <returns>���ӵ�����</returns>
        private int GetShelfSpec(string shelfSpecID)
        {
            ShelfSpec sp = new ShelfSpec();
            sp = shelfSpecManage.GetShelfByID(shelfSpecID);
            int capacity=0;
            if (sp.Col > 0 && sp.Row > 0 && sp.Height > 0)
            {
                capacity = sp.Col * sp.Height * sp.Row;
            }
            return capacity;
        }

        /// <summary>
        /// ��ҳ���ȡ�������Ϣ
        /// </summary>
        /// <returns></returns>
        private bool IceBoxInfo(System.Data.IDbTransaction trans)
        {
            tmpIcebox.LayerNum = Convert.ToInt32(nudLayerNum.Value);
            if (txtIceBoxType.Tag != null)
            {
                tmpIcebox.IceBoxTypeId = txtIceBoxType.Tag.ToString();
            }
            //Ĭ��Ϊ��ʽ�䶳��
            else
                tmpIcebox.IceBoxTypeId = "1";
            string iceboxName = txtName.Text.TrimEnd().TrimStart();// txtName.Text;            
            tmpIcebox.IceBoxName = iceboxName;
            iceBoxManage.SetTrans(trans);
            IceBox tmp = iceBoxManage.GetIceBoxByName(iceboxName);
            if (null != tmp && tmp.IceBoxName == iceboxName)
            {
                MessageBox.Show("�����������ظ������������룡", title);
                return false;
            }
            tmpIcebox.IsOccupy = Convert.ToInt16(0);
            tmpIcebox.Comment = txtComment.Text;
            if (cmbOrgOrBlood.SelectedValue != null)
            {
                tmpIcebox.OrgOrBlood = cmbOrgOrBlood.SelectedValue.ToString();
            }
            else
            {
                tmpIcebox.OrgOrBlood = "0";
            }
            if (cmbSpecType.SelectedValue != null)
            {
                tmpIcebox.SpecTypeId = cmbSpecType.SelectedValue.ToString();
            }
            else
            {
                tmpIcebox.SpecTypeId = "0";
            }
            tmpIcebox.UseStaus = "1";
            return true;
        }
        
        /// <summary>
        /// ���ñ����ı걾���ͺͱ걾����
        /// </summary>
        private void LayerSpecSetting()
        {
            if (cmbOrgOrBlood.SelectedValue == null)
            {
                return;
            }
            string orgID = cmbOrgOrBlood.SelectedValue.ToString();
            string specTypeID = "";
            if (curSelectedIceBox.SpecTypeId != ""&&curSelectedIceBox.SpecTypeId != null)
            {
                specTypeID = curSelectedIceBox.SpecTypeId.ToString();
            }
            if (cmbSpecType.SelectedValue != null)
            {
                specTypeID = cmbSpecType.SelectedValue.ToString();
            }
            foreach (ucIceBoxLayer ibl in ucLayer)
            {
                ibl.SetOrgClass(orgID);
                ibl.SetSpecType(specTypeID);
            }
            LayerSetting = 1;
        }
        #endregion

        #region ҳ�沼��
        /// <summary>
        /// TableLayout ����
        /// </summary>
        private void TableLayoutSet()
        {
            //����table Layout�еĲ���         
            specCount = Convert.ToInt32(nudLayerSpec.Value);
            layerCount = Convert.ToInt32(nudLayerNum.Value);
            chkNum = specCount * layerCount;
            specIndex = 0;
            chkIndex = 0;
            tableCol = 2;
            if (Convert.ToBoolean(specCount % 2))
            {
                tableRow = (specCount / 2 + 1);
            }
            else
            {
                tableRow = specCount / 2;
            }
            //tableLayerout������
            tlpLayer.ColumnCount = tableCol;
            tlpLayer.RowCount = tableRow;
            chk = new NeuCheckBox[chkNum];
            ucLayer = new ucIceBoxLayer[specCount];            

        }

        /// <summary>
        /// �����ż���Ĳ���
        /// </summary>
        private void DoulbeSet()
        {

            for (int j = 0; j < tlpLayer.RowCount; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    this.tlpLayer.Controls.Add(FlowAddControls(), i, j);
                    specIndex++;
                }
            }
            LayerSpecSetting();
        }

        /// <summary>
        /// ��TableLayout�м���ucIceBoxLayer �ؼ�
        /// </summary>
        /// <returns>ucIceBoxLayer:��Ҫ����ҳ���ϵĿؼ�</returns>
        private ucIceBoxLayer FlowAddControls()
        {
            ucIceBoxLayer layerTemp = new ucIceBoxLayer(txtIceBoxType.Tag.ToString());
            layerTemp.Controls.Find("flpChk", false);
            ControlCollection collection = layerTemp.Controls;
            foreach (Control c in collection)
            {
                if (c.Name == "flpChk")
                {
                    for (int k = 0; k < layerCount; k++)
                    {
                        NeuCheckBox chkTemp = new NeuCheckBox();
                        chkTemp.AutoSize = true;
                        chkTemp.Name = "chk" + chkIndex.ToString();
                        chkTemp.Text = "��" + (k + 1).ToString() + "��";
                        chkTemp.Width = 60;
                        chk[chkIndex] = chkTemp;
                        ((FlowLayoutPanel)c).Controls.Add(chk[chkIndex]);
                        chkIndex++;
                    }
                    break;
                }
            }
            layerTemp.Name = "layer" + specIndex.ToString();
            ucLayer[specIndex] = layerTemp; 
            return ucLayer[specIndex];

        }

        /// <summary>
        /// ����������Ĳ���
        /// </summary>
        private void SingleTableLayout()
        {
            for (int j = 0; j < tlpLayer.RowCount - 1; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    this.tlpLayer.Controls.Add(FlowAddControls(), i, j);
                    specIndex++;
                }
            }
            this.tlpLayer.Controls.Add(FlowAddControls(), 0, tlpLayer.RowCount - 1);
            LayerSpecSetting();

        }
        #endregion

        #region ����У��
        /// <summary>
        /// �����������У��
        /// </summary>
        /// <returns></returns>
        private bool SpecDataValidate()
        {
            if (txtIceBoxType.Text == "" || txtIceBoxType.Tag == null)
            {
                MessageBox.Show("�����ñ������ͣ�", title);
                return false;
            }
            if (Convert.ToInt32(nudLayerSpec.Value) > Convert.ToInt32(nudLayerNum.Value))
            {
                MessageBox.Show("����ܴ��ڲ���", title);
                return false;
            }
            string iceBoxName = txtName.Text.Trim();
            if (iceBoxName == "")
            {
                MessageBox.Show("�������ֲ���Ϊ��", title);
                return false;
            }
            foreach (IceBox i in arrIcebox)
            {
                if (iceBoxName == i.IceBoxName)
                {
                    MessageBox.Show("�������ֲ����ظ�", title);
                    return false;
                }
                break;
            }

            return true;
        }

        /// <summary>
        /// �������ݵ�У��
        /// </summary>
        /// <returns>false�����ݲ��ϸ�</returns>
        private bool IceBoxDataValidate()
        {
            if (txtIceBoxType.Tag == null || txtIceBoxType.Text == "")
            {
                MessageBox.Show("�����ñ������ͣ�", title);
                return false;
            }
            //ѡ�еĲ��б���һ�㱻ѡ����
            List<string> checkedLayerList = new List<string>();
            //ѭ���������ֲ�ͬ���ı����
            foreach (ucIceBoxLayer layer in ucLayer)
            {
                ControlCollection controls = layer.Controls;
                foreach (Control c in controls)
                {
                    //����FlowLayout�е�CheckBox���鿴�ĸ�CheckBox��ѡ��
                    if (c.Name.Contains("flpChk"))
                    {
                        ControlCollection flpControl = ((FlowLayoutPanel)c).Controls;
                        int checkedLayerIndex = 0;
                        foreach (Control flpc in flpControl)
                        {
                            NeuCheckBox tmpCheck = (NeuCheckBox)flpc;
                            string chkText = tmpCheck.Text;
                            if (tmpCheck.Checked)
                            {
                                //�鿴ͬһ���Ƿ�ѡ��2��
                                if (checkedLayerList.Contains(chkText))
                                {
                                    MessageBox.Show("ͬһ�㲻������2�����Ϲ��", "�������");
                                    return false;
                                }
                                else
                                {
                                    checkedLayerList.Add(chkText);
                                }
                            }
                            //checkedIndex++;
                            checkedLayerIndex++;
                        }
                    }
                }
            }
            if (checkedLayerList.Count < Convert.ToInt32(nudLayerNum.Value))
            {
                MessageBox.Show("������ÿһ�㶼�����˹������!", "���������");
                return false;
            }
            return true;
        }
        #endregion

        #region ������Ϣ����
        /// <summary>
        /// ����ÿһ�������
        /// </summary>
        /// <returns></returns>
        private bool IceBoxLayerSave(System.Data.IDbTransaction trans)
        {
            int checkedIndex = 0;
            shelfManage.SetTrans(trans);
            shelfSpecManage.SetTrans(trans);
            layerManage.SetTrans(trans);
            capLogManage.SetTrans(trans);

            try
            {
                foreach (ucIceBoxLayer layer in ucLayer)
                {                    
                    ControlCollection controls = layer.Controls;
                    foreach (Control c in controls)
                    {
                        if (c.Name.Contains("flpChk"))
                        {
                            ControlCollection flpControl = ((FlowLayoutPanel)c).Controls;
                            foreach (Control flpc in flpControl)
                            {
                                NeuCheckBox tmpCheck = (NeuCheckBox)flpc;
                                if (tmpCheck.Checked)
                                {
                                    IceBoxLayer layerTmp = new IceBoxLayer();                                   
                                    layer.LayerSaveObj(ref layerTmp); 
                                    layerTmp.IceBox = iceBoxManage.GetIceBoxByName(txtName.Text.TrimStart().TrimEnd());
                                    int layerNum = checkedIndex % layerCount + 1;
                                    layerTmp.LayerNum = Convert.ToInt16(layerNum);// as short;                                    
                                    string sequence = "";
                                    layerManage.GetNextSequence(ref sequence);
                                    layerTmp.LayerId = Convert.ToInt32(sequence);
                                    int r = layerManage.InsertIceBoxLayer(layerTmp);
                                    capLogManage.ModifyIceBoxLayer(new IceBoxLayer(), loginPerson.Name, "N", layerTmp, "�½������");
                                    //���������б���Ķ����
                                    //if (layerTmp.SaveType == 'J')
                                    //{
                                    //    int layerId = layerManage.GetLayerIDByIceBox(layerTmp.IceBox.IceBoxId.ToString(), layerNum.ToString());
                                    //    Shelf shelfTmp = new Shelf();
                                    //    for (int i = 1; i <= layerTmp.Col; i++)
                                    //    {
                                            
                                    //        string shelfsequence = "";
                                    //        shelfManage.GetNextSequence(ref shelfsequence);
                                    //        shelfTmp.ShelfID = Convert.ToInt32(shelfsequence);
                                    //        //�����ڱ������
                                    //        shelfTmp.IceBoxLayer.Row = 1;
                                    //        //�����ڱ������
                                    //        shelfTmp.IceBoxLayer.Col = i;
                                    //        //���ڱ����ĵڼ���
                                    //        shelfTmp.IceBoxLayer.Height = 1;
                                    //        shelfTmp.IceBoxLayer.LayerId = layerId;
                                    //        shelfTmp.SpecBarCode = layerTmp.IceBox.IceBoxId.ToString().PadLeft(3, '0') + layerTmp.LayerNum.ToString().Trim().PadLeft(2, '0') + i.ToString().Trim().PadLeft(2, '0');
                                    //        shelfTmp.ShelfSpec.ShelfSpecID = layerTmp.SpecID;
                                    //        shelfTmp.OccupyCount = 0;
                                    //        shelfTmp.Capacity = GetShelfSpec(shelfTmp.ShelfSpec.ShelfSpecID.ToString());
                                    //        shelfTmp.IsOccupy = '0';                                           
                                    //        int s = shelfManage.InsertShelf(shelfTmp);
                                    //    }
                                    //}
                                    //iceBoxTmp.Col
                                }
                                checkedIndex++;
                            }
                        }
                        break;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                //IceBoxLayerSave();
                string error = e.Message;
                return false;
            }

        }

        private int IceBoxSave(System.Data.IDbTransaction trans)
        {
            int i = 0;
            try
            {

                if (!IceBoxInfo(trans))
                    return i;
                if (!IceBoxDataValidate())
                {
                    return i;
                }

                iceBoxManage.SetTrans(trans);

                if (cmbIceBox.Text.Trim() == "")
                {
                    DialogResult resBut = MessageBox.Show("�˲��������һ���µı���!", title, MessageBoxButtons.YesNo);
                    if (resBut == DialogResult.No)
                        return 0;
                    i = iceBoxManage.InsertIceBox(tmpIcebox);
                    if (i == -1) return i;
                    if (!IceBoxLayerSave(trans)) return -1;

                }

                else
                {
                    
                        DialogResult resBut = MessageBox.Show("�˲�������ָ���ı�������!", title, MessageBoxButtons.YesNo);
                        if (resBut == DialogResult.No)
                            return 0;
                         
                    return iceBoxManage.ExecNoQuery(" update SPEC_ICEBOX set ICEBOXNAME = '" + txtName.Text + "' where ICEBOXID = " + cmbIceBox.SelectedValue.ToString());
                }

                return 1;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                return -1;
            }

        }
        #endregion

        #region ��ʾ������Ϣ
        private void GetIceBoxShow()
        {
            txtName.Text = cmbIceBox.Text;
            ArrayList arr = new ArrayList();
            //��ȡ�����в����Ϣ
            arr = layerManage.GetIceBoxLayers(cmbIceBox.SelectedValue.ToString());
            if (arr==null || arr.Count <= 0)
            {
                tlpLayer.Controls.Clear();
                return;
            }
            //arr = layerManage.GetIceBoxSpec("76");
            //��ȡ�����в�Ĺ����ϸ��Ϣ
            Dictionary<IceBoxLayer, List<int>> dicLayer = layerManage.GetLayerSpec(arr);            
            //��ȡ������Ϣ
            curSelectedIceBox = iceBoxManage.GetIceBoxByName(cmbIceBox.Text);
            cmbOrgOrBlood.SelectedValue = Convert.ToInt32(curSelectedIceBox.OrgOrBlood);
            cmbSpecType.SelectedValue = Convert.ToInt32(curSelectedIceBox.SpecTypeId);
            txtComment.Text = curSelectedIceBox == null ? "" : curSelectedIceBox.Comment;
            //foreach (Control c in gpBaseInfo.Controls)
            //{
            //    if (c.Name != "cmbIceBox" || c.Name != "txtIceBoxType")
            //        c.Enabled = false;
            //}
            //���ñ����ж��ٲ�
            nudLayerNum.Value = curSelectedIceBox.LayerNum;
            //���ñ����м��ֹ��
            nudLayerSpec.Value = dicLayer.Count;
            this.tlpLayer.Controls.Clear();
            //���㲼��
            TableLayoutSet();
            int specCount = Convert.ToInt32(nudLayerSpec.Value);
            if (specCount <= 0)
                return;
            if (specCount % 2 == 0)
            {
                DoulbeSet();
            }
            else
            {
                SingleTableLayout();
            }

            int ucIndex = 0;

            foreach (KeyValuePair<IceBoxLayer, List<int>> item in dicLayer)
            {
                ucIceBoxLayer tmpucLayer = new ucIceBoxLayer(txtIceBoxType.Tag.ToString());
                tmpucLayer = ucLayer[ucIndex];
                IceBoxLayer tmpLayer = new IceBoxLayer();
                tmpLayer = item.Key;
                tmpucLayer.SetValue(tmpLayer);
                ControlCollection cLayer = tmpucLayer.Controls;
                //foreach (Control c in cLayer)
                //{
                //    if (c.Name == "cmbDisType")
                //    {
                //        System.Windows.Forms.ComboBox cmb = (System.Windows.Forms.ComboBox)c;
                //        cmb.SelectedValue = tmpLayer.DiseaseType.DisTypeID;
                //        break;
                //    }
                //}
                #region �����ŵ��Ǽ���
                //if (tmpLayer.SaveType == 'J')
                //{
                //    foreach (Control c in cLayer)
                //    {
                //        if (c.Name == "rbtShelf")
                //        {
                //            RadioButton rb = c as RadioButton;
                //            rb.Checked = true;
                //        }
                //        if (c.Name == "cmbShelf")
                //        {
                //            System.Windows.Forms.ComboBox cmb = c as System.Windows.Forms.ComboBox;
                //            cmb.SelectedValue = tmpLayer.SpecID;
                //        }
                //        if (c.Name == "nudShelfCount")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Col;
                //        }
                //    }
                //}
                #endregion
                #region �����ŵ��Ǳ걾��
                //if (tmpLayer.SaveType == 'B')
                //{
                //    foreach (Control c in cLayer)
                //    {
                //        if (c.Name == "rbtSpec")
                //        {
                //            RadioButton rb = c as RadioButton;
                //            rb.Checked = true;
                //        }
                //        if (c.Name == "cmbSpec")
                //        {
                //            System.Windows.Forms.ComboBox cmb = c as System.Windows.Forms.ComboBox;
                //            cmb.SelectedValue = tmpLayer.SpecID;
                //        }
                //        if (c.Name == "nudRow")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Row;
                //        }
                //        if (c.Name == "nudCol")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Col;
                //        }
                //        if (c.Name == "nudHeight")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Height;
                //        }
                //    }
                //}
                #endregion
                List<int> tmpNum = item.Value;
                foreach (int i in tmpNum)
                {
                    foreach (Control c in cLayer)
                    {
                        if (c.Name.Contains("flp"))
                        {
                            FlowLayoutPanel flp = c as FlowLayoutPanel;
                            ControlCollection flpControls = flp.Controls;
                            foreach (Control fc in flpControls)
                            {
                                //NeuCheckBox neuChk = c as NeuCheckBox;
                                chk[ucIndex * layerCount + i - 1].Checked = true;
                            }
                            break;
                        }
                    }
                }
                ucIndex++;
            }

        }
        #endregion

        private void DisuseIceBox(int iceboxId)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                SpecBoxManage tmpManage = new SpecBoxManage();
                tmpManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                ArrayList arrLayer = layerManage.GetIceBoxLayers(iceboxId.ToString());
                ArrayList arrShelf;
                ArrayList arrBox;

                foreach (IceBoxLayer l in arrLayer)
                {
                    IceBoxLayer tmpl = l;
                    #region ���������Ǽ���
                    if (tmpl.SaveType == 'J')
                    {
                        arrShelf = new ArrayList();
                        arrShelf = shelfManage.GetShelfByLayerID(tmpl.LayerId.ToString());
                        foreach (Shelf s in arrShelf)
                        {
                            Shelf tmps = s;
                            arrBox = new ArrayList();
                            arrBox = tmpManage.GetBoxByCap(tmps.ShelfID.ToString(), 'J');
                            foreach (SpecBox b in arrBox)
                            {
                                //�ϳ������еı걾��
                                SpecBox tmpb = b;
                                tmpb.DesCapCol = 0;
                                tmpb.DesCapID = 0;
                                tmpb.DesCapRow = 0;
                                tmpb.DesCapSubLayer = 0;
                                tmpb.DesCapType = '0';
                                tmpb.IsOccupy = '0';
                                tmpb.OccupyCount = 0;
                                if (tmpManage.UpdateSpecBox(tmpb) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("����ʧ�ܣ�", title);
                                    return;
                                }
                                if (capLogManage.ModifyBoxLog(b, loginPerson.Name, "D", tmpb, "����ϳ�") <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("����ʧ�ܣ�", title);
                                    return;
                                }
                            }
                            tmps.IceBoxLayer.LayerId = 0;
                            tmps.IceBoxLayer.Col = 0;
                            tmps.IceBoxLayer.Row = 0;
                            tmps.IceBoxLayer.Height = 0;
                            tmps.IsOccupy = '0';
                            tmps.OccupyCount = 0;
                            if (shelfManage.UpdateShelf(tmps) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ʧ�ܣ�", title);
                                return;
                            }
                            if (capLogManage.ModifyShelf(s, loginPerson.Name, "D", tmps, "����ϳ�") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ʧ�ܣ�", title);
                                return;
                            }
                        }
                    }
                    #endregion
                    #region ���������Ǳ걾��
                    if (l.SaveType == 'B')
                    {
                        arrBox = new ArrayList();
                        arrBox = tmpManage.GetBoxByCap(l.LayerId.ToString(), 'B');
                        foreach (SpecBox b1 in arrBox)
                        {
                            //�ϳ������еı걾��
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
                            if (capLogManage.ModifyBoxLog(b1, loginPerson.Name, "D", tmpb1, "����ϳ�")<=0)
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
                    int result = capLogManage.ModifyIceBoxLayer(l, loginPerson.Name, "D", tmpl, "����ϳ�");
                    if (result<=0)
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            if (!SpecDataValidate()) return -1;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                int result = IceBoxSave(FS.FrameWork.Management.PublicTrans.Trans);
                switch (result)
                {
                    case -1:
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�", title);
                        return -1;                      
                    case 1:
                        MessageBox.Show("����ɹ�!", title);
                        break;
                    default:
                        return 0;
                        //break;

                }
               FS.FrameWork.Management.PublicTrans.Commit();
                //���°�IceBox
                if (txtIceBoxType.Tag != null && txtIceBoxType.Text.Trim() != "")
                {
                    BindingIceBox(txtIceBoxType.Tag.ToString());
                }
                //ѡ��Ϊ��ǰ��iceboxid
                cmbIceBox.SelectedValue = tmpIcebox.IceBoxId;
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ���Ϣ��" + ex.Message, title);
            }
            return base.Save(sender, neuObject);
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
            
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (Control c in gpBaseInfo.Controls)
            {
                c.Enabled = true;
            }
            switch (e.ClickedItem.Text.Trim())
            {
                case "����":
                    if (cmbIceBox.SelectedValue == null || cmbIceBox.SelectedValue.ToString() == "0")
                    {
                        MessageBox.Show("��ȡ����ʧ�ܣ�", title);
                        return;
                    }
                    int iceboxId = Convert.ToInt32(cmbIceBox.SelectedValue.ToString());
                    if (iceBoxManage.CheckIceBoxHaveSpecBox(iceboxId.ToString()) == 1)
                    {
                        MessageBox.Show("�����д��б걾�����ܷϳ�", title);
                        return;
                    }
                    DialogResult dialog = MessageBox.Show("�˲������ϳ����������д�ŵı걾�ܺͱ걾����Ϣ��������", title, MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                        return;
                    DisuseIceBox(iceboxId);
                    BindingIceBox(txtIceBoxType.Tag.ToString());
                    break;
                case "���":
                    tlpLayer.Controls.Clear();
                    txtIceBoxType.Text = "";
                    txtName.Text = "";
                    nudLayerNum.Value = 1;
                    nudLayerSpec.Value = 1;
                    txtComment.Text = "";
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
      
        /// <summary>
        /// ȷ����ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                if (!SpecDataValidate())
                {
                    return;
                }
                this.tlpLayer.Dock = DockStyle.Fill;
                this.tlpLayer.Controls.Clear();
                //TableRowColSet();
                TableLayoutSet();
                int specCount = Convert.ToInt32(nudLayerSpec.Value);
                if (specCount <= 0)
                    return;
                if (specCount % 2 == 0)
                {
                    //DoubleTableLayout();
                    DoulbeSet();

                }
                else
                {
                    SingleTableLayout();
                }
            }
            catch
            {
 
            }

        }

        /// <summary>
        /// ��� tlpLayer�����Ĺ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpLayer_Scroll(object sender, ScrollEventArgs e)
        {
            tlpLayer.PerformLayout();
            tlpLayer.Focus();
        }

        private void tlpLayer_MouseWheel(object sender, MouseEventArgs e)
        {
            tlpLayer.Focus();
        }

        /// <summary>
        /// ���������б�ѡ�б���Ĺ����ʾ��ҳ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbIceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIceBox.Text.Trim() != "")
            {
                GetIceBoxShow();
            }
            txtName.Text = "";           
        }

        private void cmbOrgOrBlood_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrgOrBlood.SelectedValue == null)
            {
                return;
            }
            string orgId = cmbOrgOrBlood.SelectedValue.ToString();
            BindingSpecType(orgId);
            if (LayerSetting == 1)
            {
                LayerSpecSetting();
            }
        }

        private void cmbSpecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LayerSetting == 1)
            {
                LayerSpecSetting();
            }
        }

        private void ucIceBox_Load(object sender, EventArgs e)
        {         
            BindingSpecClass();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //��ѯ���������б�
            ArrayList iceBoxTypeList = new ArrayList();
            iceBoxTypeList = con.GetList("ICEBOXTYPE");
            this.txtIceBoxType.AddItems(iceBoxTypeList);
        }

        private void txtIceBoxType_TextChanged(object sender, EventArgs e)
        {
            if (txtIceBoxType.Text.Trim() != "" && txtIceBoxType.Tag != null)
            {
                BindingIceBox(txtIceBoxType.Tag.ToString());
            }
        }
    }
}
