using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using System.Collections;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Material.Base
{
    /// <summary>
    /// [��������: ���ʿ�Ŀά��]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: ]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucMatKindMain : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucMatKindMain()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���ݹ���
        /// </summary>
        private DataView dv;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// �����ļ�
        /// </summary>
        private string filePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\MatKind.xml";

        /// <summary>
        /// ���ʿ�Ŀ��
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset basesetManager = new FS.HISFC.BizLogic.Material.Baseset();

        /// <summary>
        /// ��ǰѡ������ڵ�{BAF240DB-D9B6-480b-978A-9BDC019A46E8}
        /// </summary>
        string selectedNode;


        private string kindLevel;

        /// <summary>
        /// �ϼ���Ŀ����
        /// </summary>
        private string kindPreID;

        /// <summary>
        /// �༭״̬
        /// </summary>
        private string state;

        #endregion

        #region ����

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string KindLevel
        {
            get
            {
                return this.kindLevel;
            }
            set
            {
                this.kindLevel = value;
            }
        }

        /// <summary>
        /// �ϼ���Ŀ����
        /// </summary>
        public string KindPreID
        {
            get
            {
                return this.kindPreID;
            }
            set
            {
                this.kindPreID = value;
            }
        }

        /// <summary>
        /// �༭״̬
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

        #region ��ʼ��������

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            toolBarService.AddToolButton("����", "������Ʒ����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ����ǰ��Ʒ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    if (this.ucMaterialKindTree1.neuTreeView1.SelectedNode == null)
                    {
                        return;
                    }
                    //����                        
                    if (this.ucMaterialKindTree1.neuTreeView1.SelectedNode.Tag != null)
                    {
                        this.KindPreID = this.ucMaterialKindTree1.neuTreeView1.SelectedNode.Tag.ToString();
                    }
                    this.New();
                    break;
                case "ɾ��":
                    this.DeleteData();
                    //this.ucMaterialKindTree1.InitTreeView();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object NeuObject)
        {
            //{BAF240DB-D9B6-480b-978A-9BDC019A46E8}
            if (this.Save(false) == 1)
            {
                this.ucMaterialKindTree1.InitTreeView();
                //{BAF240DB-D9B6-480b-978A-9BDC019A46E8}
                this.ucMaterialKindTree1.neuTreeView1.SelectedNode = getNodeByCode(this.selectedNode, this.ucMaterialKindTree1.neuTreeView1.Nodes[0]);                
                this.ucMaterialKindTree1.Focus();
                this.ucMaterialKindTree1.neuTreeView1.Focus();
            }

            return 1;
        }

        //{BAF240DB-D9B6-480b-978A-9BDC019A46E8}
        /// <summary>
        /// ���ݱ����ȡ���ڵ�
        /// </summary>
        /// <param name="matID"></param>
        /// <param name="tNode"></param>
        /// <returns></returns>
        private TreeNode getNodeByCode(string matID, TreeNode tNode)
        {
            foreach (TreeNode tmpNode in tNode.Nodes)
            {
                if (tmpNode.Tag.ToString() == matID)
                {
                    return tmpNode;
                }
                if (tmpNode.Nodes.Count > 0)
                {
                    TreeNode gotNode = this.getNodeByCode(matID, tmpNode);
                    if (gotNode != tmpNode)
                    {
                        return gotNode;
                    }
                }
            }
            return tNode;
        }

        #endregion

        #region ����

        /// <summary>
        /// �����������е�������ʾ��neuSpread1_Sheet1��
        /// </summary>
        public void ShowData()
        {
            this.ClearData();

            //ȡ��Ŀ��Ϣ
            ArrayList alObject = this.basesetManager.QueryKindAll();
            if (alObject == null)
            {
                MessageBox.Show(this.basesetManager.Err);
                return;
            }

            foreach (FS.HISFC.Models.Material.MaterialKind metKind in alObject)
            {
                this.dt.Rows.Add(new Object[] {																																			
																		metKind.Kgrade, //��Ŀ����
				
																		//��Ŀ����
																		metKind.ID,

																		//�ϼ�����
																		metKind.SuperKind,

																		//��Ŀ����
																		metKind.Name,

																		//ƴ����
																		metKind.SpellCode.ToString(),

																		//�����
																		metKind.WBCode,

																		//��ĩ����ʶ
																		metKind.EndGrade,

																		//��Ҫ��Ƭ
																		metKind.IsCardNeed,//.ToString(),

																		//���ι���
																		metKind.IsBatch,//.ToString(),

																		//��Ч�ڹ���
																		metKind.IsValidcon,//.ToString(),

																		//�����Ŀ����
																		metKind.AccountCode.ToString(),

																		//�����Ŀ����
																		metKind.AccountName.ToString(),

																		//����Ա
																		//metKind.Oper.ID,

																		//��������
																		//metKind.OperateDate.ToString(),

																		//Ԥ�Ʋ�ֵ��
																		metKind.LeftRate.ToString(),

																		//�Ƿ�̶��ʲ�
																		metKind.IsFixedAssets,//.ToString(),

																		//�������
																		metKind.OrderNo.ToString(),																		

																		//��Ӧ�ɱ�������Ŀ���
																		metKind.StatCode,

																		//�Ƿ�Ӽ�����
																		metKind.IsAddFlag//.ToString()
																	});
            }

            //�ύDataSet�еı仯��
            this.dt.AcceptChanges();

        }

        /// <summary>
        ///  ��ʼ��DataSet,����neuSpread1_Sheet1��
        /// </summary>
        private void InitDataTable()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {
																			new DataColumn("��Ŀ����",   dtStr),
																			new DataColumn("��Ŀ����",   dtStr),
																			new DataColumn("�ϼ�����",   dtStr),
																			new DataColumn("��Ŀ����",   dtStr),
																			new DataColumn("ƴ����",   dtStr),
																			new DataColumn("�����",     dtStr),
																			new DataColumn("ĩ����ʶ",   dtBool),
																			new DataColumn("��Ҫ��Ƭ",   dtBool),
																			new DataColumn("���ι���",   dtBool),
																			new DataColumn("Ч�ڹ���",   dtBool),
																			new DataColumn("�����Ŀ����",   dtStr),
																			new DataColumn("�����Ŀ����",   dtStr),																			
																			new DataColumn("Ԥ�Ʋ�ֵ��",    dtStr),
																			new DataColumn("�̶��ʲ�",    dtBool),
																			new DataColumn("�������",    dtStr),																			
																			new DataColumn("�������",     dtStr),
																			new DataColumn("�Ӽ�����",   dtBool)																			
														});

            this.dv = new DataView(this.dt);
            this.dv.AllowEdit = true;
            this.dv.AllowNew = true;
            this.neuSpread1.DataSource = this.dv;
            this.SetFormat();
        }

        /// <summary>
        /// ����fp��ʽ
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1_Sheet1.Columns[0].Visible = false;
            this.neuSpread1_Sheet1.Columns[2].Visible = false;
            this.neuSpread1_Sheet1.Columns[6].Visible = false;
            this.neuSpread1_Sheet1.Columns[7].Visible = false;
            this.neuSpread1_Sheet1.Columns[14].Visible = false;

            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            // ���øı�������
            this.neuSpread1_Sheet1.Columns[1].Locked = true;
        }

        /// <summary>
        /// �������
        /// </summary>
        public void ClearData()
        {
            this.dt.Rows.Clear();

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ɾ����¼
        /// </summary>
        public void DeleteData()
        {
            string kindID = "";

            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //���ѡ�����fp����Ķ����������ɾ����ϸ��Ϣ
            if (this.neuSpread1_Sheet1.ActiveRow != null)
            {
                kindID = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Value.ToString();
            }
            else
            {
                kindID = this.ucMaterialKindTree1.NodeTag;

            }
            int kindRowCount = this.basesetManager.GetKindRowCount(kindID);

            ArrayList alKind = this.basesetManager.GetMetKindByPreID(kindID);

            if (kindRowCount > 0)
            {
                MessageBox.Show("�˿�Ŀ�´�����Ʒ�ֵ���Ϣ������ɾ���ֵ���Ϣ��ִ�д˲���!", "ɾ����ʾ");
                return;
            }

            if (kindRowCount < 0)
            {
                MessageBox.Show("��ȡ�ÿ�Ŀ����Ŀ�ֵ�����������");
                return;
            }

            if (alKind.Count > 0)
            {
                MessageBox.Show("�ÿ�Ŀ�´����¼���Ŀ��Ϣ������ɾ���¼���Ŀ��Ϣ��ִ�д˲���!", "ɾ����ʾ");
                return;
            }

            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //������¼ӵģ�����ʾ��ֱ��ɾ����
            ArrayList al = this.basesetManager.GetMetKindByMetID(kindID);
            if (al == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ������Ϣ����"));
                return;
            }
            if (al.Count== 0 && this.neuSpread1_Sheet1.ActiveRow != null)
            {
                this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
            }
            else
            {

                System.Windows.Forms.DialogResult dr;
                dr = MessageBox.Show("ȷ��Ҫɾ����Ŀ��" + (this.neuSpread1_Sheet1.ActiveRow==null?this.ucMaterialKindTree1.NodeName:this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex,3].Text)+ "����?", "��ʾ!", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    return;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //t.BeginTransaction();

                basesetManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.basesetManager.DeleteMetKind(kindID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.basesetManager.Err);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                //this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
                this.ShowData();

                MessageBox.Show("ɾ���ɹ���");
                this.ucMaterialKindTree1.InitTreeView();
            }
        }

        /// <summary>
        /// ͨ������Ĳ�ѯ�룬���������б�
        /// </summary>
        //public void ChangeItem(string treeFilter)
        //{
        //    if (this.dt.Rows.Count == 0) return;

        //    try
        //    {
        //        string queryCode = "";

        //        queryCode = "%" + this.txtInputCode.Text.Trim() + "%";

        //        string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
        //            "(����� LIKE '" + queryCode + "') OR " +
        //            "(��Ŀ���� LIKE '" + queryCode + "') ";

        //        //���ù�������
        //        if (treeFilter == "")
        //        {
        //            this.dv.RowFilter = filter;
        //        }
        //        else
        //        {
        //            //this.dv.RowFilter = "((�ϼ����� = '" + treeFilter + "')or" + "(��Ŀ����='" + treeFilter + "'))and (" + filter + ")";
        //            this.dv.RowFilter = "(�ϼ����� = '" + treeFilter + "')and (" + filter + ")";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        public void ChangeItem()
        {
            if (this.dt.Rows.Count == 0)
                return;

            try
            {
                string queryCode = "";

                queryCode = "%" + this.txtInputCode.Text.Trim() + "%";

                string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(��Ŀ���� LIKE '" + queryCode + "') ";

                //���ù�������
                this.dv.RowFilter = "(�ϼ����� = '" + this.ucMaterialKindTree1.NodeTag + "')and (" + filter + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��������{BAF240DB-D9B6-480b-978A-9BDC019A46E8}
        /// </summary>
        public int Save(bool isTempSave)
        {
            this.selectedNode = this.ucMaterialKindTree1.neuTreeView1.SelectedNode.Tag.ToString();

            this.neuSpread1.StopCellEditing();
            //��Ч���ж�
            if (this.Valid())
            {
                return -1;
            };

            foreach (DataRow dr in this.dt.Rows)
            {
                dr.EndEdit();
            }
            this.SetLeafFlag();

            //�������ݿ⴦������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            basesetManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isUpdate = false; //�ж��Ƿ���»���ɾ��������

            DateTime sysTime = this.basesetManager.GetDateTimeFromSysDateTime();

            //ȡ�޸ĺ����ӵ�����
            DataTable dataChanges = this.dt.GetChanges(DataRowState.Modified | DataRowState.Added);
            if (dataChanges != null)
            {
                foreach (DataRow row in dataChanges.Rows)
                {
                    FS.HISFC.Models.Material.MaterialKind metKind = new FS.HISFC.Models.Material.MaterialKind();

                    #region ���ݱ���������Ϣ���п�Ŀ��Ϣ��ֵ

                    //��Ŀ����
                    metKind.Kgrade = row["��Ŀ����"].ToString();

                    //��Ŀ����
                    metKind.ID = row["��Ŀ����"].ToString();

                    //�ϼ�����
                    metKind.SuperKind = row["�ϼ�����"].ToString();

                    //��Ŀ����
                    metKind.Name = row["��Ŀ����"].ToString();

                    //ƴ����
                    metKind.SpellCode = row["ƴ����"].ToString();

                    //�����
                    metKind.WBCode = row["�����"].ToString();

                    //��ĩ����ʶ
                    metKind.EndGrade = FS.FrameWork.Function.NConvert.ToBoolean(row["ĩ����ʶ"].ToString());

                    //��Ҫ��Ƭ
                    metKind.IsCardNeed = FS.FrameWork.Function.NConvert.ToBoolean(row["��Ҫ��Ƭ"].ToString());

                    //���ι���
                    metKind.IsBatch = FS.FrameWork.Function.NConvert.ToBoolean(row["���ι���"].ToString());

                    //��Ч�ڹ���
                    metKind.IsValidcon = FS.FrameWork.Function.NConvert.ToBoolean(row["Ч�ڹ���"].ToString());

                    //�����Ŀ����
                    metKind.AccountCode.ID = row["�����Ŀ����"].ToString();

                    //�����Ŀ����
                    metKind.AccountName.Name = row["�����Ŀ����"].ToString();

                    //Ԥ�Ʋ�ֵ��
                    metKind.LeftRate = FS.FrameWork.Function.NConvert.ToDecimal(row["Ԥ�Ʋ�ֵ��"].ToString());

                    //�Ƿ�̶��ʲ�
                    metKind.IsFixedAssets = FS.FrameWork.Function.NConvert.ToBoolean(row["�̶��ʲ�"].ToString());

                    //�������
                    metKind.OrderNo = FS.FrameWork.Function.NConvert.ToInt32(row["�������"].ToString());

                    //��Ӧ�ɱ�������Ŀ���
                    metKind.StatCode = row["�������"].ToString();

                    //�Ƿ�Ӽ�����
                    metKind.IsAddFlag = FS.FrameWork.Function.NConvert.ToBoolean(row["�Ӽ�����"].ToString());

                    #endregion

                    metKind.Oper.ID = this.basesetManager.Operator.ID;
                    metKind.Oper.OperTime = sysTime;

                    //ִ�и��²������ȸ��£����û�гɹ������������
                    if (this.basesetManager.SetKind(metKind) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.basesetManager.Err);

                        return 0;
                    }
                }
                dataChanges.AcceptChanges();
                isUpdate = true;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            //ˢ������
            if (!isTempSave)
            {
                this.ShowData();
                MessageBox.Show("����ɹ���");  
            }

            return 1;
        }

        /// <summary>
        /// ����Ҷ�ӽڵ��־
        /// </summary>
        public void SetLeafFlag()
        {
            foreach (DataRow dr in this.dt.Rows)
            {
                DataRow[] drList = this.dt.Select("�ϼ����� = '" + dr["��Ŀ����"].ToString() + "'");
                if (drList.Length > 0)
                {
                    dr["ĩ����ʶ"] = false;
                }
                else
                {
                    dr["ĩ����ʶ"] = true;
                }
            }
        }

        /// <summary>
        /// �½�
        /// </summary>
        public void New()
        {
            try
            {
                //{BAF240DB-D9B6-480b-978A-9BDC019A46E8}���Ӷ��б����ظ�
                if (this.HasUnsavedChange() == -1)
                {
                    return;
                }

                string kindID = this.basesetManager.GetMaxKindID(this.KindPreID);

                ArrayList al = new ArrayList();

                if (this.KindPreID == "0")
                {
                    this.dt.Rows.Add(new Object[] { "1", kindID.ToString(), "0", "", "", "", 1, 1, 1, 1, "", "", "", 1, "", "", 1 });
                }
                else
                {
                    al = this.basesetManager.QueryKindAllByID(this.KindPreID);
                    if (al != null)
                    {
                        FS.HISFC.Models.Material.MaterialKind metKind;
                        metKind = al[0] as FS.HISFC.Models.Material.MaterialKind;
                        this.dt.Rows.Add(new Object[] { (Convert.ToInt32(metKind.Kgrade) + 1).ToString(), kindID.ToString(), metKind.ID.ToString(), "", "", "", 1, 1, 1, 1, "", "", "", 1, "", "", 1 });
                    }
                }

                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.RowCount - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        /// <summary>
        /// �ж��Ƿ����δ���������{BAF240DB-D9B6-480b-978A-9BDC019A46E8}
        /// </summary>
        /// <returns></returns>
        private int HasUnsavedChange()
        {
            DataTable dataChanges = this.dt.GetChanges(DataRowState.Modified | DataRowState.Added);
            //���û���޸ĵ����ݣ���ֱ�������У�������޸ĵ����ݣ��ȱ�����������
            if (dataChanges == null || dataChanges.Rows.Count <= 0)
            {
                return 1;
            }

            return this.Save(true);  
        }

        /// <summary>
        ///  ��Ч���ж� 
        /// </summary>	
        private bool Valid()
        {
            //for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            //{

            //    if (this.neuSpread1_Sheet1.Cells[i, 3].Text == "" || this.neuSpread1_Sheet1.Cells[i, 3] == null)
            //    {
            //        MessageBox.Show("��" + i.ToString() + "�п�Ŀ���Ʋ���Ϊ��");
            //        return true;
            //    }

            //}
            foreach (DataRow row in this.dt.Rows)
            {
                if (string.IsNullOrEmpty(row["��Ŀ����"].ToString()))
                {
                    MessageBox.Show("��Ŀ���Ʋ���Ϊ��");

                    return true;
                }
            }
            return false;
        }

        private bool Check()
        {
            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0501", ref testPrivDept);

            //��ʱ������Ȩ���ж�

            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show("���޴˴��ڲ���Ȩ��");
                return false;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return false;
            }
            base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Department deptObj = deptManager.GetDeptmentById(testPrivDept.ID);
            if (deptObj == null || deptObj.ID == "")
            {
                testPrivDept.Memo = deptObj.DeptType.ID.ToString();
            }
            this.ucMaterialKindTree1.storagecode = testPrivDept.ID;
            this.ucMaterialKindTree1.InitTreeView();

            return true;
        }

        #endregion

        private void ucMatKindMain_Load(object sender, EventArgs e)
        {
            //if (this.Check() == false)
            //{
            //    return;
            //}
            this.txtInputCode.TextChanged += new EventHandler(txtInputCode_TextChanged);
            this.txtInputCode.KeyUp += new KeyEventHandler(txtInputCode_KeyUp);

            this.InitDataTable();

            this.ShowData();

            this.ChangeItem();

            InputMap im;

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        private void txtInputCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }

        private void txtInputCode_TextChanged(object sender, EventArgs e)
        {
            this.ChangeItem();
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if (e.Column == 3)
            //{
            //    if (neuSpread1_Sheet1.Cells[e.Row, 3].Text.ToString() == "")
            //        return;
            //    FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();
            //    FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

            //    spCode = (FS.HISFC.Models.Base.Spell)mySpell.Get(neuSpread1_Sheet1.Cells[e.Row, 3].Text.ToString());

            //    if (spCode.SpellCode.Length > 10)
            //        spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
            //    if (spCode.WBCode.Length > 10)
            //        spCode.WBCode = spCode.WBCode.Substring(0, 10);

            //    this.neuSpread1_Sheet1.Cells[e.Row, 4].Value = spCode.SpellCode;
            //    this.neuSpread1_Sheet1.Cells[e.Row, 5].Value = spCode.WBCode;
            //}
        }

        private void neuSpread1_LeaveCell(object sender, LeaveCellEventArgs e)
        {
            if (e.Column == 3)
            {
                if (neuSpread1_Sheet1.Cells[e.Row, 3].Text.ToString() == "")
                    return;
                FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();
                FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

                spCode = (FS.HISFC.Models.Base.Spell)mySpell.Get(neuSpread1_Sheet1.Cells[e.Row, 3].Text.ToString());

                if (spCode.SpellCode.Length > 10)
                    spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
                if (spCode.WBCode.Length > 10)
                    spCode.WBCode = spCode.WBCode.Substring(0, 10);

                this.neuSpread1_Sheet1.Cells[e.Row, 4].Value = spCode.SpellCode;
                this.neuSpread1_Sheet1.Cells[e.Row, 5].Value = spCode.WBCode;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.neuSpread1_Sheet1.ActiveColumnIndex == 3)
                    {
                        FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();
                        FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

                        spCode = (FS.HISFC.Models.Base.Spell)mySpell.Get(neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 3].Text.ToString());

                        if (spCode.SpellCode.Length > 10)
                            spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
                        if (spCode.WBCode.Length > 10)
                            spCode.WBCode = spCode.WBCode.Substring(0, 10);

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 4].Value = spCode.SpellCode;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 5].Value = spCode.WBCode;
                    }

                    this.neuSpread1_Sheet1.ActiveColumnIndex++;
                }

            }
            return base.ProcessDialogKey(keyData);
        }

        private void ucMaterialKindTree1_GetLak(object sender, TreeViewEventArgs e)
        {
            //this.ChangeItem(e.Node.Tag.ToString());
            this.ChangeItem();
        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            if (this.Check() == false)
            {
                return -1;
            }

            return 1;
        }

        #endregion

    }
}
