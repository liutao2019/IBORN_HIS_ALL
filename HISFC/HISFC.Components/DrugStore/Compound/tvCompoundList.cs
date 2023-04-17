using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// <br></br>
    /// [��������: ���ù������б���ʾ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// <˵��>
    ///     1���б�������ʾʱ��ʱ����ҩ�������ز���
    /// </˵��>
    /// </summary>
    public partial class tvCompoundList : Common.Controls.baseTreeView
    {
        public tvCompoundList()
        {
            InitializeComponent();
        }

        public tvCompoundList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public delegate void SelectDataHandler(ArrayList alData);

        public event SelectDataHandler SelectDataEvent;

        #region �����

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩ��������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// ������ˮ��
        /// </summary>
        private string groupCode = "U";

        /// <summary>
        /// ������ϸ״̬
        /// </summary>
        private string state = "0";

        /// <summary>
        /// �Ƿ�ִ��
        /// </summary>
        private bool isExec = false;
        #endregion

        #region ����

        /// <summary>
        /// ������ˮ��
        /// </summary>
        public string GroupCode
        {
            get
            {
                return this.groupCode;
            }
            set
            {
                if (value == null || value == "")
                {
                    this.groupCode = "U";
                }
                else
                {
                    this.groupCode = value;
                }
            }
        }

        /// <summary>
        /// ������ϸ״̬
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
            try
            {
                this.ImageList = this.deptImageList;

                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                if (alDept == null)
                {
                    MessageBox.Show(Language.Msg("���ؿ����б�������" + deptManager.Err));
                }

                objHelper.ArrayObject = alDept;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("����֪ͨ�б��ʼ����������" + ex.Message));
            }
        }

        /// <summary>
        /// ��ȡ��ҩ���б���ʾʱ���߽ڵ�
        /// </summary>
        /// <param name="nodeBill">�����ڵ�</param>
        /// <param name="info">��ҩ��ϸ��Ϣ</param>
        /// <returns>�ɹ����ذ�ҩ����ʾʱ�Ļ��߽ڵ�</returns>
        private TreeNode GetNodePatient(FS.HISFC.Models.Pharmacy.ApplyOut info, TreeNode parentNode)
        {
            TreeNode nodePatient;
            nodePatient = new TreeNode();
            nodePatient.Text = "��" + info.User01 + "��" + info.User02;  //�����š�����
            nodePatient.ImageIndex = 2;
            nodePatient.SelectedImageIndex = 2;
            nodePatient.Tag = info;
            if (parentNode == null)
            {
                this.Nodes.Add(nodePatient);
            }
            else
            {
                parentNode.Nodes.Add(nodePatient);
            }

            return nodePatient;
        }

        /// <summary>
        /// ��ȡ��ҩ���б���ʾ���ҽڵ�
        /// </summary>
        /// <param name="info">��ҩ��ϸ��Ϣ</param>
        /// <returns>�ɹ����ذ�ҩ���б���ʾʱ�Ŀ��ҽڵ�</returns>
        private TreeNode GetNodeDept(FS.HISFC.Models.Pharmacy.ApplyOut info, TreeNode parentNode)
        {
            TreeNode nodeDept = new TreeNode();
            if (info.ApplyDept.Name == "")
            {
                info.ApplyDept.Name = objHelper.GetName(info.ApplyDept.ID);
            }

            nodeDept.Text = info.ApplyDept.Name;
            nodeDept.ImageIndex = 1;
            nodeDept.SelectedImageIndex = 1;
            nodeDept.Tag = info;
            if (parentNode == null)
            {
                this.Nodes.Add(nodeDept);
            }
            else
            {
                parentNode.Nodes.Add(nodeDept);
            }

            return nodeDept;
        }

        /// <summary>
        /// ���ݴ���İ�ҩ֪ͨ���飬��ʾ��tvCompoundList��
        /// ������������ǰ��տ��ҡ����������
        /// </summary>
        /// <param name="alDrugMessage">��ҩ֪ͨ����</param>
        public virtual void ShowList(List<FS.HISFC.Models.Pharmacy.ApplyOut> alApplyOut)
        {
            this.Nodes.Clear();

            string privDeptCode = "";				//��һ������
            TreeNode nodePatient;
            TreeNode nodeDept = new TreeNode();

            foreach (ApplyOut info in alApplyOut)
            {
                #region ÿ�ν��ڵ�����������

                this.SuspendLayout();


                if (info.ApplyDept.ID != privDeptCode)		//����µĿ��ҽڵ�
                {
                    nodeDept = this.GetNodeDept(info, null);

                    privDeptCode = info.ApplyDept.ID;
                }

                nodePatient = this.GetNodePatient(info, nodeDept);

                this.ResumeLayout();

                #endregion
            }

            if (this.Nodes.Count > 0)
            {
                this.Nodes[0].Expand();
            }
        }
        /// <summary>
        /// �������в������и�ҩƷ������{4F8AF409-2ED7-4e18-954C-DE0A61A0F061}feng.ch
        /// </summary>
        /// <param name="stockDeptId">������</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="list">������������</param>
        public virtual void ShowListByParmacy(string stockDeptId, ref ArrayList list)
        {
            this.Nodes.Clear();
            TreeNode nodeDept = new TreeNode();
            nodeDept.Tag = "allDept";
            nodeDept.Text = "���в���";
            this.Nodes.Add(nodeDept);
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            list = itemManager.QueryCompoundApplyOut(stockDeptId, "ALL", "U", null, "0", false);
        }
        /// <summary>
        /// ��ȡѡ�еĽڵ�İ�ҩ������Ϣ
        /// </summary>
        /// <param name="selectNode">��ǰѡ�нڵ�</param>
        /// <param name="al">��ҩ��ϸ��Ϣ</param>
        protected virtual void GetSelectData(TreeNode selectNode, ref ArrayList al)
        {
            #region ��ȡԭʼ������Ϣ

            al = null;

            if (selectNode == null)
            {
                return;
            }

            FS.HISFC.Models.Pharmacy.ApplyOut info = selectNode.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            if (info == null)
            {
                return;
            }

            //�жϸ��ڵ��Ƿ�Ϊ�գ��縸�ڵ�Ϊ�� ˵����ʱ�����Ϊ���ҽڵ�
            if (selectNode.Parent == null)
            {
                //���ݵ�ǰ���ҩ������ǰ������һ�ȡ����������ϸ��Ϣ
                al = this.itemManager.QueryCompoundApplyOut(info.StockDept.ID, info.ApplyDept.ID, this.GroupCode, null, this.state, this.isExec);
            }
            else
            {
                //���ݵ�ǰ���ҩ�� ��ǰ������� ��ǰ���뻼�߻�ȡ����������ϸ��Ϣ
                al = this.itemManager.QueryCompoundApplyOut(info.StockDept.ID, info.ApplyDept.ID, this.GroupCode, info.PatientNO, this.state, this.isExec);
            }

            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ������ϸ��Ϣ��������") + this.itemManager.Err);
                return;
            }

            #endregion

            #region ����ԭʼ�������κ�Ϊ��ˮ�ţ�Ų�����ֲ���Ϊ�ӿ�Ĭ��ʵ�֣�

            //string privCompoundGroup = "-1";
            //string privNewCompoundGroupNO = "";
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut compound in al)
            //{
            //    if (compound.CompoundGroup.IndexOf("C") != -1)      //��������ˮ��δ���¹�
            //    {
            //        if (privCompoundGroup == compound.CompoundGroup)
            //        {
            //            compound.CompoundGroup = privNewCompoundGroupNO;
            //            continue;
            //        }
            //        else
            //        {
            //            string newCompoundGroupNO = "";
            //            if (this.itemManager.UpdateCompoundGroupNO(compound.CompoundGroup, ref newCompoundGroupNO) == -1)
            //            {
            //                MessageBox.Show(Language.Msg("����ԭʼ�������κ�Ϊ��ˮ��ʱ��������"));
            //                return;
            //            }

            //            privCompoundGroup = compound.CompoundGroup;
            //            compound.CompoundGroup = newCompoundGroupNO;
            //            privNewCompoundGroupNO = newCompoundGroupNO;       
            //        }
            //    }
            //}

            #endregion

            #region ҩ�����Ĵ��� ��ʱ���δ˴�

            /*
            if (stockMark != null && stockMark.ID != "")
            {
                //��ȡ���ҿ��ҩƷ��Ϣ
                if (this.hsStockData == null)
                {
                    this.hsStockData = new Hashtable();

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ؿ��ҿ����Ϣ ���Ժ�..."));
                    Application.DoEvents();

                    ArrayList alStorage = this.itemManager.QueryStockinfoList(stockMark.ID);

                    foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorage)
                    {
                        if (storage.IsArkManager)
                        {
                            continue;
                        }
                        this.hsStockData.Add(storage.Item.ID, null);
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }                          
            }
            
             if (this.hsStockData == null)
            {
                al = alTotal;
            }
            else
            {
                //����ҩ������ҩƷ
                if (alTotal.Count > 0)
                {
                    if (alTotal[0] is FS.HISFC.Models.Pharmacy.ApplyOut)
                    {
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alTotal)
                        {
                            if (this.hsStockData.ContainsKey(info.Item.ID))
                            {
                                al.Add(info);
                            }
                        }
                    }
                    else
                    {
                        al = alTotal;
                    }
                }
            }
             
             *
            */

            #endregion
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ�����ϸ����.���Ժ�.."));
            Application.DoEvents();

            ArrayList al = new ArrayList();

            this.GetSelectData(e.Node, ref al);

            if (al != null)
            {
                if (this.SelectDataEvent != null)
                {
                    this.SelectDataEvent(al);
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            base.OnAfterSelect(e);
        }
        /// <summary>
        /// ˢ��������ʾ
        /// </summary>
        internal void RefreshData()
        {
            TreeViewEventArgs e = new TreeViewEventArgs(this.SelectedNode);

            this.OnAfterSelect(e);
        }
    }
}
