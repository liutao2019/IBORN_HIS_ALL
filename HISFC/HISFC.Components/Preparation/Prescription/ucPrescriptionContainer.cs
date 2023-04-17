using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation.Prescription
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ����ô���ά��(��������)]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-05]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class ucPrescriptionContainer : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrescriptionContainer()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();     
     
        /// <summary>
        /// ������Ʒ�б���Ϣ
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> prescriptionList;

        /// <summary>
        /// ��ǰ��ά���õ����ƴ���
        /// </summary>
        private System.Collections.Hashtable hsPrescription = new Hashtable();

        /// <summary>
        /// ��ǰ��ʾ�����ƴ����ĳ�Ʒ����
        /// </summary>
        private string nowProductPrescription = "";
        #endregion

        #region �ӿ�ʵ������

        /// <summary>
        /// ��Ʒ����ӿ�
        /// </summary>
        IPrescriptionProduct productInstance = null;

        /// <summary>
        /// ��Ʒ����ӿ�
        /// </summary>
        public IPrescriptionProduct ProductInstance
        {
            get
            {
                return this.productInstance;
            }
            set
            {
                this.productInstance = value;
            }
        }

        /// <summary>
        /// ԭ���ϴ�������ӿ�
        /// </summary>
        IPrescriptionMaterial materialInstance = null;

        /// <summary>
        /// ԭ���ϴ�������ӿ�
        /// </summary>
        public IPrescriptionMaterial MaterialInstance
        {
            get
            {
                return this.materialInstance;
            }
            set
            {
                this.materialInstance = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("���ӳ�Ʒ", "�������Ƽ���Ʒ", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("������ϸ", "�����Ƽ���Ʒԭ����ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ����Ʒ", "ɾ���Ƽ���Ʒ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("ɾ������", "ɾ���Ƽ�������ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "���ӳ�Ʒ")
            {
                this.productInstance.AddProduct();
            }
            if (e.ClickedItem.Text == "������ϸ")
            {
                this.materialInstance.AddMaterial();
            }
            if (e.ClickedItem.Text == "ɾ����Ʒ")
            {
                this.productInstance.DeleteProduct();

                this.Clear();

                this.QueryProductList();
            }
            if (e.ClickedItem.Text == "ɾ������")
            {
                this.materialInstance.DeleteMaterial();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.SavePrescription() == 1)
            {
                MessageBox.Show("����ɹ�");
            }

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryProductList();

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        /// <summary>
        /// ��ȡ��Ʒ���ƴ�����Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual FS.HISFC.Models.Base.EnumItemType GetItemType()
        {
            return FS.HISFC.Models.Base.EnumItemType.Drug;
        }

        /// <summary>
        /// ��ȡ��Ʒ���ƴ�����Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual int QueryProductList()
        {
            this.prescriptionList = this.preparationManager.QueryPrescriptionList(this.GetItemType());
            if (this.prescriptionList == null)
            {
                MessageBox.Show(Language.Msg("δ��ȷ��ȡ��Ʒ���ƴ�����Ϣ \n" + this.preparationManager.Err));
                return -1;
            }
            this.productInstance.Clear();

            foreach (FS.FrameWork.Models.NeuObject info in this.prescriptionList)
            {
                if (this.productInstance.ShowProduct(info) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }
    
        /// <summary>
        /// �������ƴ�����Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SavePrescription()
        {          
            List<FS.HISFC.Models.Preparation.PrescriptionBase> prescriptionList = this.materialInstance.GetMaterial();
            if (prescriptionList == null)
            {
                MessageBox.Show("��ȡ���δ����洦����ϸ��Ϣʧ��");
                return -1;
            }

            foreach (FS.HISFC.Models.Preparation.PrescriptionBase info in prescriptionList)
            {
                info.ItemType = this.GetItemType();
            }

            if (this.preparationManager.SavePrescription(prescriptionList) == -1)
            {
                MessageBox.Show("�����Ƽ���Ʒ���ô�����Ϣ��������" + this.preparationManager.Err);

                return -1;
            }
           
            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            this.productInstance.Clear();

            this.materialInstance.Clear();

            return 1;
        }

        #region ���ýӿ����

        /// <summary>
        /// ��ȡ�ӿ�ʵ��
        /// </summary>
        /// <returns></returns>
        protected virtual int GetInterface()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ػ������� ���Ժ�...");
            Application.DoEvents();

            ucProduct ucP = new ucProduct();
            this.productInstance = ucP;
            ucP.Init();

            ucMaterial ucM = new ucMaterial();
            this.materialInstance = ucM;
            ucM.Init();

            this.splitContainer2.Panel1.Controls.Add(ucP);
            ucP.Dock = DockStyle.Fill;

            this.splitContainer2.Panel2.Controls.Add(ucM);
            ucM.Dock = DockStyle.Fill;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        #endregion

        private void ucPrescription_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                if (this.GetInterface() == -1)
                {
                    return;
                }

                this.productInstance.ShowPrescriptionEvent += new EventHandler(productInstance_ShowPrescriptionEvent);

                this.QueryProductList();
            }
        }

        public void productInstance_ShowPrescriptionEvent(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject operProduct = sender as FS.FrameWork.Models.NeuObject;

            this.materialInstance.ShowMaterial(operProduct);
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] interfaceTypes = new Type[] { typeof(HISFC.Components.Preparation.IPrescription) };

                return interfaceTypes;
            }
        }

        #endregion
    }
}
