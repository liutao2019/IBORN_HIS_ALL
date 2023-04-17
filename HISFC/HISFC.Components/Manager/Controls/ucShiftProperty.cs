using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ������Լ�¼����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007��04]<br></br>
    /// 
    /// <˵��>
    ///     1 ά����ʵ��������Ҫ���б����¼������
    /// </˵��>
    /// </summary>
    public partial class ucShiftProperty : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucShiftProperty()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ���������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.ShiftData shiftManager = new FS.HISFC.BizLogic.Manager.ShiftData();

        /// <summary>
        /// ��ǰ����ʵ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject reflectClass = null;

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        private bool isNew = false;

        /// <summary>
        /// �Ѵ��ڵ�ʵ������Ϣ
        /// </summary>
        private System.Collections.Hashtable hsExitsClass = new Hashtable();

        /// <summary>
        /// �Ƿ����Ա ����Ա��ʾά���б�
        /// </summary>
        private bool isManager = true;

        /// <summary>
        /// ʵ��������
        /// </summary>
        private string reflectClassStr = "";
        #endregion

        #region ����

        /// <summary>
        /// �Ƿ����Ա
        /// </summary>
        [System.ComponentModel.Description("�Ƿ����Ա ����Ա��ʾά���б�"),Category("����"),DefaultValue(true)]
        public bool IsManager
        {
            get
            {
                return this.isManager;
            }
            set
            {
                this.isManager = value;

                if (!value)
                {
                    this.splitContainer1.Panel1Collapsed = true;

                    this.toolBarService.SetToolButtonEnabled("����", false);
                    this.toolBarService.SetToolButtonEnabled("ɾ��", false);
                }
            }
        }

        /// <summary>
        /// ʵ��������
        /// </summary>
        [Description("�����ڴ����ʵ������Ϣ IsManager��������ΪFalseʱ�����Բ���Ч"),Category("����")]
        public string ReflectClassStr
        {
            get
            {
                return this.reflectClassStr;
            }
            set
            {
                this.reflectClassStr = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���ӷ���", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelShiftPropertyType();
            }
            if (e.ClickedItem.Text == "����")
            {
                this.AddShiftProperty();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveShiftProperty();

            return 1;
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.tvType.ImageList = this.tvType.groupImageList;

            if (this.isManager)
            {
                this.ShowShiftPropertyList();
            }
            else
            {
                if (this.reflectClassStr != "")
                {
                    List<FS.HISFC.Models.Base.ShiftProperty> alList = this.shiftManager.QueryShiftProperty(this.reflectClassStr);
                    if (alList != null)
                    {
                        this.reflectClass = alList[0].ReflectClass;

                        this.ShowShiftPropertyDetail();
                    }
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ��Fp�ڼ�������
        /// </summary>
        /// <param name="sf">���������Ϣ</param>
        /// <param name="iRowIndex">������������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FS.HISFC.Models.Base.ShiftProperty sf,int iRowIndex)
        {
            this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);

            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColRecord].Value = sf.IsRecord;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColPropertyName].Text = sf.Property.Name;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColDescription].Text = sf.PropertyDescription;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColShiftCause].Text = sf.ShiftCause;

            this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = sf;

            return 1;
        }

        /// <summary>
        /// ���Ӵ���
        /// </summary>
        protected void AddShiftProperty()
        {
            using (ucAddShiftType ucAdd = new ucAddShiftType())
            {
                ucAdd.HsExitsClass = this.hsExitsClass;

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucAdd);
                if (ucAdd.Result == DialogResult.OK)
                {
                    this.reflectClass = ucAdd.ReflectedClass;
                    if (this.reflectClass.Name == "")
                    {
                        this.reflectClass.Name = this.reflectClass.ID;
                    }

                    TreeNode node = new TreeNode(this.reflectClass.Name);
                    node.Tag = this.reflectClass;
                    this.tvType.Nodes[0].Nodes.Add(node);

                    this.tvType.SelectedNode = node;

                    int iRowIndex = 0;
                    foreach (FS.FrameWork.Models.NeuObject property in ucAdd.Properties)
                    {
                        FS.HISFC.Models.Base.ShiftProperty sf = new FS.HISFC.Models.Base.ShiftProperty();
                        sf.ReflectClass = this.reflectClass;
                        sf.Property = property;                     //������Ϣ
                        sf.PropertyDescription = property.Memo;     //��������

                        this.AddDataToFp(sf,iRowIndex);

                        iRowIndex++;
                    }

                    this.hsExitsClass.Add(this.reflectClass.ID,null);
                   
                    this.isNew = true;
                }
            }
        }

        /// <summary>
        /// ��ȡ������б�
        /// </summary>
        protected void ShowShiftPropertyList()
        {
            List<FS.FrameWork.Models.NeuObject> shiftList = this.shiftManager.QueryShiftPropertyList();

            if (shiftList == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�б�������") + this.shiftManager.Err);
                return;
            }

            this.Clear();
            this.tvType.Nodes.Clear();

            TreeNode parentNode = new TreeNode("���ʵ���б�",0,0);
            parentNode.Tag = null;
            this.tvType.Nodes.Add(parentNode);

            foreach (FS.FrameWork.Models.NeuObject info in shiftList)
            {
                if (info.Name == "")
                {
                    info.Name = info.ID;
                }
                TreeNode node = new TreeNode(info.Name);

                node.ImageIndex = 2;
                node.SelectedImageIndex = 4;

                node.Tag = info;

                parentNode.Nodes.Add(node);
            }

            this.tvType.ExpandAll();
        }

        /// <summary>
        /// ��ʾ���������ϸ
        /// </summary>
        protected void ShowShiftPropertyDetail()
        {
            if (this.reflectClass == null || this.reflectClass.ID == "")
            {
                return;
            }

            List<FS.HISFC.Models.Base.ShiftProperty> shiftDetail = this.shiftManager.QueryShiftProperty(this.reflectClass.ID);
            if (shiftDetail == null)
            {
                MessageBox.Show(Language.Msg(Language.Msg("��ȡ���������ϸ��Ϣ��������") + this.shiftManager.Err));
                return;
            }

            int iRowIndex = 0;
            foreach (FS.HISFC.Models.Base.ShiftProperty sf in shiftDetail)
            {
                this.AddDataToFp(sf,iRowIndex);

                if (!this.hsExitsClass.ContainsKey(sf.ReflectClass.ID))
                {
                    hsExitsClass.Add(sf.ReflectClass.ID, null);
                }

                iRowIndex++;
            }
        }

        /// <summary>
        /// ɾ����ǰ��
        /// </summary>
        protected void DelShiftPropertyType()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            if (this.reflectClass == null)
            {
                return;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ���ô�����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return;
            }

            if (this.shiftManager.DelShiftProperty(this.reflectClass.ID) == -1)
            {
                MessageBox.Show(Language.Msg("ɾ��ԭ��������ʧ��") + this.shiftManager.Err);
                return;
            }

            if (this.hsExitsClass.ContainsKey(this.reflectClass.ID))
            {
                this.hsExitsClass.Remove(this.reflectClass.ID);
            }

            MessageBox.Show(Language.Msg("ɾ���ɹ�"));

            this.ShowShiftPropertyList();            
        }

        /// <summary>
        /// ����
        /// </summary>
        protected int SaveShiftProperty()
        {
            if (this.reflectClass == null)
            {
                MessageBox.Show(Language.Msg("��ѡ��������"));
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.shiftManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.shiftManager.DelShiftProperty(this.reflectClass.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(Language.Msg("ɾ��ԭ��������ʧ��") + this.shiftManager.Err);
                return -1;
            }

            DateTime sysTime = this.shiftManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Base.ShiftProperty sf = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Base.ShiftProperty;

                sf.IsRecord = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColRecord].Value);
                sf.ShiftCause = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColShiftCause].Text;
                sf.Oper.ID = this.shiftManager.Operator.ID;
                sf.Oper.OperTime = sysTime;
                if (sf.ShiftCause == "")
                {
                    sf.ShiftCause = sf.Property.Name;
                }

                if (this.shiftManager.InsertShiftProperty(sf) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("������Ա���ʧ��"));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();;
            MessageBox.Show(Language.Msg("����ɹ�"));

            this.isNew = false;

            return 1;
        }

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }

        private void tvType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node.Parent == null || e.Node.Tag == null)
            {
                this.reflectClass = null;

                return;
            }

            this.reflectClass = e.Node.Tag as FS.FrameWork.Models.NeuObject;

            this.ShowShiftPropertyDetail();
        }

        private void tvType_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (isNew)
            {
                DialogResult rs = MessageBox.Show(Language.Msg("����������δ���� �Ƿ����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    return;
                }

                this.isNew = false;
            }
        }

        #endregion

        #region ������

        private enum ColumnSet
        { 
            /// <summary>
            /// �Ƿ��¼���
            /// </summary>
            ColRecord,
            /// <summary>
            /// ��������
            /// </summary>
            ColPropertyName,
            /// <summary>
            /// ����
            /// </summary>
            ColDescription,
            /// <summary>
            /// ���ԭ��
            /// </summary>
            ColShiftCause
        }

        #endregion               
    }
}
