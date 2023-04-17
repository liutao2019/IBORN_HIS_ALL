using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class frmNorTmpLayerSetting : Form
    {
        private IceBoxLayer layer;
        private string boxName;
        private BoxSpecManage boxSpecManage = new BoxSpecManage();
        public delegate void SetLayer(ref IceBoxLayer l);
        public event SetLayer OnSetLayer;

        public IceBoxLayer Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
            }
        }


        public string BoxName
        {
            set
            {
                boxName = value;
            }
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
        /// ���ӹ���
        /// </summary>
        private void BoxSpecBinding()
        {
            Dictionary<int, string> dicSpec = boxSpecManage.GetAllBoxSpec();
            if (dicSpec.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicSpec;
                cmbBoxSpec.DisplayMember = "Value";
                cmbBoxSpec.ValueMember = "Key";
                cmbBoxSpec.DataSource = bs;
                cmbBoxSpec.SelectedIndex = 0;
                // cmbBoxSpec.SelectedValue;
            }
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

        /// <summary>
        /// ����У��
        /// </summary>
        /// <returns></returns>
        private bool DataValidate()
        {
            try
            {
                if (cmbDisType.SelectedValue == null || cmbDisType.Text.Trim() == "")
                {
                    MessageBox.Show("���ֱ�����д");
                    return false;
                }
                if ((cmbSpecType.SelectedValue == null) || cmbSpecType.Text.Trim() == "")
                {
                    MessageBox.Show("�걾���ͱ�����д");
                    return false;
                }
                layer.DiseaseType.DisTypeID = Convert.ToInt32(cmbDisType.SelectedValue.ToString());
                layer.SpecTypeID = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
                layer.SpecID = Convert.ToInt32(cmbBoxSpec.SelectedValue.ToString());

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                try
                {
                    SpecBoxManage boxMgr = new SpecBoxManage();
                    IceBoxLayerManage layerMgr = new IceBoxLayerManage();
                    boxMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    layerMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    ArrayList arrBox = new ArrayList();
                    arrBox =  boxMgr.GetBoxByCap(layer.LayerId.ToString(), 'B');
                    if(arrBox==null || arrBox.Count==0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return true;
                    }

                    SpecBox tmpBox = arrBox[0] as SpecBox;

                    if (tmpBox.OccupyCount > 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���б걾 ,���ܸ���");
                        return false;
                    }

                    if (layerMgr.UpdateLayer(layer) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���´洢��ʧ�ܣ�");
                        return false;
                    }

                    tmpBox.SpecTypeID = layer.SpecTypeID;
                    tmpBox.BoxSpec.BoxSpecID = layer.SpecID;
                    tmpBox.DiseaseType.DisTypeID = layer.DiseaseType.DisTypeID;

                    if (boxMgr.UpdateSpecBox(tmpBox) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���±걾�У�");
                        return false;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return true;
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void InitData()
        {
            this.txtName.Text = boxName;
            this.txtHeight.Text = layer.LayerNum.ToString();
            cmbDisType.SelectedValue = layer.DiseaseType.DisTypeID;
            cmbSpecType.SelectedValue = layer.SpecTypeID;
            cmbBoxSpec.SelectedValue = layer.SpecID;
            this.Text = boxName + "  " + "��" + layer.LayerNum.ToString() + "��";
        }

        public frmNorTmpLayerSetting()
        {
            InitializeComponent();
            layer = new IceBoxLayer();
            boxName = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!DataValidate())
            {
                return;
            }
            this.OnSetLayer(ref layer);
            this.Close();
        }

        private void frmNorTmpLayerSetting_Load(object sender, EventArgs e)
        {
            this.DisTypeBinding();
            this.SpecTypeBinding();
            this.BoxSpecBinding();
            InitData();
        }


    }
}