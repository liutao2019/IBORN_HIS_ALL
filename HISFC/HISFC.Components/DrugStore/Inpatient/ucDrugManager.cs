using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Management;

namespace Neusoft.UFC.DrugStore.Inpatient
{
    public partial class ucDrugManager : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucDrugManager()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// סԺ��ҩ������ӡ�ӿ�
        /// </summary>
        protected Neusoft.HISFC.Integrate.PharmacyInterface.IInpatientDrug IDrugManager = null;

        /// <summary>
        /// ���β����İ�ҩ֪ͨ��Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Pharmacy.DrugMessage nowDrugMessage = new Neusoft.HISFC.Object.Pharmacy.DrugMessage();

        /// <summary>
        /// ���β�������ҩ̨
        /// </summary>
        private Neusoft.HISFC.Object.Pharmacy.DrugControl drugControl = new Neusoft.HISFC.Object.Pharmacy.DrugControl();

        /// <summary>
        /// ��ǰ���ҵ�ȫ����ҩ̨
        /// </summary>
        private ArrayList drugControlGather = null;

        /// <summary>
        /// ҩ��������
        /// </summary>
        protected Neusoft.HISFC.Management.Pharmacy.DrugStore drugStoreManager = new Neusoft.HISFC.Management.Pharmacy.DrugStore();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ��ϸ
        /// </summary>
        public bool IsShowDetail
        {
            get { return this.ucDrugDetail1.Visible; }
            set
            {
                //�����Ƿ���ʾ��ϸ
                this.ucDrugDetail1.Visible = value;
                this.ucDrugMessage1.Visible = !value;
                //������ʾ�Ŀؼ������ýӿڵ�ʵ��
                if (value)
                    this.IDrugManager = this.ucDrugDetail1 as Neusoft.HISFC.Integrate.PharmacyInterface.IInpatientDrug;
                else
                    this.IDrugManager = this.ucDrugMessage1 as Neusoft.HISFC.Integrate.PharmacyInterface.IInpatientDrug;
            }
        }

        /// <summary>
        /// ��ǰ���ҵ�ȫ����ҩ̨
        /// </summary>
        public ArrayList DrugControlGather
        {
            set
            {
                this.drugControlGather = value;
            }
        }

        #endregion

        #region ��������ť��ʼ��

        protected Neusoft.NFC.Interface.Forms.ToolBarService toolBarService = new Neusoft.NFC.Interface.Forms.ToolBarService();
        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��  ��", "��ҩȷ�� ��ӡ��ҩ��", 0, true, false, null);
            toolBarService.AddToolButton("ȫ  ѡ", "ѡ��ȫ��", 1, true, false, null);
            toolBarService.AddToolButton("ȫ��ѡ", "ȡ��ѡ��", 2, true, false, null);
            toolBarService.AddToolButton("ˢ  ��", "ˢ���б�", 3, true, false, null);
            toolBarService.AddToolButton("��ҩ��", "��ҩ������ ��ҩ��׼", 4, true, false, null);
            toolBarService.AddToolButton("��  ӡ", "�����ҩ��", 3, true, false, null);
            toolBarService.AddToolButton("��ӡ��ʽ", "ѡ���Զ���ӡ ���� �ֹ���ӡ", 3, true, false, null);
            toolBarService.AddToolButton("ˢ�·�ʽ", "ѡ���Զ�ˢ�� ���� �ֹ�ˢ��", 5, true, false, null);
            toolBarService.AddToolButton("̨ѡ��", "ѡ���ҩ̨", 6, true, false, null);           

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (this.IDrugManager == null)
                return;

            switch (e.ClickedItem.Text)
            {
                case "��  ��":
                    this.IDrugManager.Save(this.nowDrugMessage);
                    break;
                case "ȫ  ѡ":
                    this.IDrugManager.CheckAll();
                    break;
                case "ȫ��ѡ":
                    this.IDrugManager.CheckNone();
                    break;
                case "ˢ  ��":
                    this.RefreshList();                    
                    break;
                case "��ҩ��":
                    break;
                case "��  ӡ":
                    this.IDrugManager.Print();
                    break;
                case "��ӡ��ʽ":
                    break;
                case "ˢ�·�ʽ":
                    break;
                case "̨ѡ��":
                    this.ChooseControl();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual void Init()
        {
 
        }

        /// <summary>
        /// ��ҩ̨ѡ��
        /// </summary>
        protected virtual int ChooseControl()
        {
            string deptCode = "6711";

            //ȡ������ȫ����ҩ̨�б�
            ArrayList al = this.drugStoreManager.QueryDrugControlList(deptCode);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ������ҩ̨�б�������") + this.drugStoreManager.Err);
                return -1;
            }
            this.DrugControlGather = al;
            return 1;
        }

        /// <summary>
        /// ������ҩ̨������ʾ
        /// </summary>
        protected void SetDrugControlProperty()
        {
            if (this.drugControl.DrugAttribute.ID.ToString() == "S" || this.drugControl.DrugAttribute.ID.ToString() == "T")		//ֻ��������ҩ̨��ʾ
            {
                this.ucDrugDetail1.IsAutoCheck = true;
            }
            else
            {
                this.ucDrugDetail1.IsAutoCheck = false;
            }
            if (this.drugControl.DrugAttribute.ID.ToString() == "R")		//��ҩ̨
            {
                this.ucDrugDetail1.IsFilterBillCode = true;
            }
            else
            {
                this.ucDrugDetail1.IsFilterBillCode = false;
            }
        }

        /// <summary>
        /// ˢ���б���ʾ
        /// </summary>
        protected virtual void RefreshList()
        {
            this.tvDrugMessage1.ShowList(this.drugControl);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            //��հ�ҩ������ϸ
            this.IDrugManager.Clear();

            //ArrayList al = new ArrayList();
            ////��ʾ��׼�����б�
            //switch (e.Node.ImageIndex)
            //{
            //    case 0:					//�����б� �����ʾ�ÿ��ҵİ�ҩ��
            //        this.DrugMessage = e.Node.Tag as neusoft.HISFC.Object.Pharmacy.DrugMessage;
            //        if (this.DrugMessage != null)
            //        {
            //            al = this.myDrugStore.GetDrugBillList(this.myDrugControl.ID, this.myDrugMessage);
            //            this.IsShowDetail = false;
            //        }
            //        break;
            //    case 1:
            //        //ȡ���ҽڵ��б���İ�ҩ֪ͨ��Ϣ
            //        this.DrugMessage = e.Node.Tag as neusoft.HISFC.Object.Pharmacy.DrugMessage;
            //        if (this.DrugMessage != null)
            //        {
            //            //�������Ұ�ҩ������ϸ����
            //            al = this.myItem.GetApplyOutList(this.myDrugMessage);
            //            this.IsShowDetail = true;
            //        }
            //        break;
            //    case 2:
            //        //ȡ���߽ڵ�ĸ����ڵ��б���İ�ҩ֪ͨ��Ϣ
            //        this.DrugMessage = e.Node.Parent.Tag as neusoft.HISFC.Object.Pharmacy.DrugMessage;
            //        //ȡ���߽ڵ��б���Ļ�����Ϣ
            //        neusoft.neuFC.Object.neuObject obj = e.Node.Tag as neusoft.neuFC.Object.neuObject;
            //        this.DrugMessage.User01 = obj.User01;  //����סԺ��ˮ��
            //        if (this.DrugMessage != null)
            //        {
            //            //�������߰�ҩ������ϸ����
            //            al = this.myItem.GetApplyOutListByPatient(this.myDrugMessage);
            //            this.IsShowDetail = true;
            //        }
            //        break;
            //    default:
            //        //��հ�ҩ������ϸ
            //        //this.IsShowDetail = true;
            //        //this.myIDrugBase.Clear();
            //        MessageBox.Show("����İ�ҩ̨");
            //        break;
            //}

            //if (al == null)
            //{
            //    MessageBox.Show(this.myItem.Err);
            //    return;
            //}
            ////��ʾ����
            //this.myIDrugBase.ShowData(al);

            return base.OnSetValue(neuObject, e);
        }
    }
}
