using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// �Һŷ�ά��
    /// </summary>
    public partial class ucRegFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRegFee()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucRegFee_Load);
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            this.treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
        }
        #region ����
        private bool isShowPubDiagFee = false;
        /// <summary>
        /// �Ƿ�Ĭ����ʾ���������
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�Ĭ����ʾ���������")]
        public bool IsShowPubDiagFee
        {
            set
            {
                this.isShowPubDiagFee = value;
            }
            get
            {
                return this.isShowPubDiagFee;
            }
        }
        #endregion

        #region ������
        /// <summary>
        /// �Һŷѹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
        private FS.HISFC.BizLogic.Registration.RegLevel regMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

        /// <summary>
        /// ����������{A53D57D8-E44D-4517-8B24-E13D686D6F1B}
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        private ArrayList al;
        private ArrayList alLevel;
        private DataTable dtRegFee = new DataTable();
        private string levelName;
        #endregion

        #region ��ʼ��
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegFee_Load(object sender, EventArgs e)
        {
            this.InitTree();
            this.InitRegLevl();
            this.InitDataTable();
        }
        /// <summary>
        /// ���ɹҺ�Ա�б�
        /// </summary>
        private void InitTree()
        {
            this.treeView1.Nodes.Clear();

            TreeNode root = new TreeNode("��ͬ��λ", 22, 22);
            this.treeView1.Nodes.Add(root);

            FS.HISFC.BizProcess.Integrate.Manager pactMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //��ú�ͬ��λ�б�
            this.al = pactMgr.QueryPactUnitOutPatient();
            if (al == null)
            {
                MessageBox.Show("��ȡ��ͬ��λ��Ϣʱ����!" + pactMgr.Err, "��ʾ");
                return;
            }

            foreach (FS.HISFC.Models.Base.PactInfo obj in al)
            {
                TreeNode node = new TreeNode(obj.Name, 11, 35);
                node.Tag = obj.ID;
                root.Nodes.Add(node);
            }
            root.Expand();
        }
        /// <summary>
        /// ��ȡ�Һż���
        /// </summary>
        private void InitRegLevl()
        {
            FS.HISFC.BizLogic.Registration.RegLevel regMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

            alLevel = regMgr.Query(true);
            if (alLevel == null)
            {
                MessageBox.Show("��ȡ�Һż���ʱ����!" + regMgr.Err, "��ʾ");
                return;
            }
        }
        /// <summary>
        /// ��ʼ�����ݴ���ʽ
        /// </summary>
        private void InitDataTable()
        {
            this.dtRegFee.Columns.AddRange(new DataColumn[]{
															   new DataColumn("�Һż���",System.Type.GetType("System.String")),
															   new DataColumn("�Һŷ�",System.Type.GetType("System.Decimal")),
															   new DataColumn("����",Type.GetType("System.Decimal")),
															   new DataColumn("�Է����Ʒ�",Type.GetType("System.Decimal")),
															   new DataColumn("�������Ʒ�",Type.GetType("System.Decimal")),
															   new DataColumn("���ӷ�",Type.GetType("System.Decimal")),
															   new DataColumn("��ˮ��",Type.GetType("System.String")),
															   new DataColumn("�������",Type.GetType("System.String"))
														   });
        }
        #endregion

        #region ����

        #region ��֤�Ƿ���Ҫ����
        private void MakeAll(string pactCode)
        {
            al = regFeeMgr.Query(pactCode,true);
            //			if(al.Count != alLevel.Count)
            //			{
            bool IsFound = false;
            foreach (FS.HISFC.Models.Registration.RegLevel level in alLevel)
            {
                IsFound = false;

                foreach (FS.HISFC.Models.Registration.RegLvlFee obj in al)
                {
                    if (level.ID == obj.RegLevel.ID)
                    {
                        IsFound = true;
                        break;
                    }
                }

                if (!IsFound)
                {
                    //����������У����Ǹú�ͬ��λû��ά����
                    FS.HISFC.Models.Registration.RegLvlFee regFee = this.Insert(pactCode, level.ID);
                    //ֱ����ӵ������У���ֹ�ٴμ������ݿ⡣
                    al.Add(regFee);
                }
            }
            //			}
        }
        #endregion

        #region ��ѯ
        /// <summary>
        /// ����ͬ��λ��ѯ�Һż���
        /// </summary>
        private void Query(string PactID)
        {
            //			al = this.regFeeMgr.Query(PactID ) ;
            if (al == null)
            {
                MessageBox.Show("��ѯ�Һŷ���Ϣʱ����!" + this.regFeeMgr.Err, "��ʾ");
                return;
            }

            this.dtRegFee.Rows.Clear();

            foreach (FS.HISFC.Models.Registration.RegLvlFee info in al)
            {
                this.getNamebyId(info.RegLevel.ID);
                if (levelName == "" || levelName == null)
                {
                    MessageBox.Show("��ȡ�������Ƴ���");
                    return;
                }
                this.dtRegFee.Rows.Add(new object[]{
													   levelName,
													   info.RegFee,
													   info.ChkFee,
													   info.OwnDigFee,
													   info.PubDigFee,
													   info.OthFee,
													   info.ID,
													   info.RegLevel.ID
												   });
            }
            this.dtRegFee.AcceptChanges();
            this.fpSpread1_Sheet1.DataSource = this.dtRegFee;
          

            this.SetFpFormat();
        }
        #endregion
        /// <summary>
        /// ��ѯ��ť�ã���ʵûɶ����
        /// </summary>
        private void Query()
        {
            int i = 0;
            string pactCode = string.Empty ;
            TreeNode root = this.treeView1.Nodes[0];
            foreach(TreeNode node in root.Nodes )
            {
                if (node.Checked)
                {
                    i++;
                    pactCode = node.Tag.ToString();
                }
            }
            if (i > 1)
            {
                MessageBox.Show("��ѡ��һ����¼���в�ѯ����");
                pactCode = string.Empty;
                return;
            }
            if (!(pactCode == "" && pactCode ==string.Empty))
            {
                this.MakeAll(pactCode);
                this.Query(pactCode);
            }

            
        }
        #region ����
        /// <summary>
        /// ����һ���µļ�¼
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="levelCode"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.RegLvlFee Insert(string pactCode, string levelCode)
        {
            FS.HISFC.Models.Registration.RegLvlFee info = new FS.HISFC.Models.Registration.RegLvlFee();

            info.ID = regFeeMgr.GetSequence("Registration.RegLevel.GetSeqNo");
            info.Pact.ID = pactCode;
            info.RegLevel.ID = levelCode;            
            info.RegFee = 0;
            info.ChkFee = 0;
            info.OwnDigFee = 0;
            info.PubDigFee = 0;
            info.OthFee = 0;
            info.Oper.ID = this.regFeeMgr.Operator.ID;
            info.Oper.OperTime = regFeeMgr.GetDateTimeFromSysDateTime();

            if (regFeeMgr.Insert(info) == -1)
            {
                MessageBox.Show("��Ӻ�ͬ��λ�Һŷѷ�����Ϣʧ�ܣ�[Registration.RegFee.Insert.1]" + regFeeMgr.Err);
            }

            return info;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private int Valid()
        {
            ///�жϽ���Ϊ��
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                {
                    if (this.fpSpread1_Sheet1.Cells[i, j].Text == string.Empty)
                    {
                        MessageBox.Show("��" + (i + 1).ToString() + "�н���Ϊ��");
                        this.fpSpread1_Sheet1.Cells[i, j].Text = "0.00";
                        return -1;
                    }
            
                }
            }

            return 1;
        }

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            this.fpSpread1.StopCellEditing();

            if (this.Valid() == -1)
            {
                return;
            }
            if (this.treeView1.SelectedNode == null || this.treeView1.SelectedNode.Parent == null)
            {
                MessageBox.Show("�޺�ͬ��λ!", "��ʾ");
                return;
            }

            string pactNames = "";
            foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    pactNames = pactNames + node.Text + "\n";
                }
            }
            if (pactNames.EndsWith("\n"))
            {
                pactNames.TrimEnd('n').TrimEnd('\\');
            }

            if (MessageBox.Show("�Ƿ񱣴����º�ͬ��λά����\n"+pactNames, "��ʾ", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }


            //��ǰ��ͬ��λ����
            string pactID = this.treeView1.SelectedNode.Tag.ToString();

            if(fpSpread1_Sheet1.RowCount>0)
                dtRegFee.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();

            //�޸�            
            DataTable dtModify = dtRegFee.GetChanges(DataRowState.Modified);
            ArrayList alModify = this.GetChanges(dtModify);

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���,���Ժ�...");
            Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regFeeMgr.con);
            //SQLCA.BeginTransaction();

            this.regFeeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                foreach (FS.HISFC.Models.Registration.RegLvlFee info in alModify)
                {
                    if (regFeeMgr.Update(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("���¹Һŷѷ��䷽������" + regFeeMgr.Err, "��ʾ");
                        return;
                    }
                }

                /// ��������ѡ�еĽڵ�
                /// 
                foreach (TreeNode pact in this.treeView1.Nodes[0].Nodes)
                {
                    if (pact.Checked && pact != this.treeView1.SelectedNode)
                    {
                        //ɾ��ԭ���ĹҺŷ�

                        if (this.regFeeMgr.DeleteByPact(pact.Tag.ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("ɾ���Һŷѳ���" + regFeeMgr.Err, "��ʾ");
                            return;
                        }

                        //Copy �Һŷ�
                        if (this.regFeeMgr.CopyByPact(pact.Tag.ToString(), pactID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("���ƹҺŷѳ���" + regFeeMgr.Err, "��ʾ");
                            return;
                        }
                    }
                    //ȡ��ѡ��
                    if (pact.Checked)
                    {
                        pact.Checked = false;
                    }
                }
                this.treeView1.Nodes[0].Checked = false;
               
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
            //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
            string errInfo = "";
            //ArrayList alInfo = new ArrayList();
            //alInfo.Add(regLevel);
            int param = Function.SendBizMessage(alModify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.RegLevel, ref errInfo);

            if (param == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                Function.ShowMessage("�Һż���ά��ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show("����ɹ���");
            dtRegFee.AcceptChanges();
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��ȡ�ı����Ϣ��ת��Ϊʵ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ArrayList GetChanges(DataTable dt)
        {
            this.al = new ArrayList();
            if (dt != null)
            {
                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        FS.HISFC.Models.Registration.RegLvlFee info = new FS.HISFC.Models.Registration.RegLvlFee();
                        info.ID = row["��ˮ��"].ToString();
                        info.RegFee = FS.FrameWork.Function.NConvert.ToDecimal(row["�Һŷ�"].ToString());
                        info.ChkFee = FS.FrameWork.Function.NConvert.ToDecimal(row["����"].ToString());
                        info.OwnDigFee = FS.FrameWork.Function.NConvert.ToDecimal(row["�Է����Ʒ�"].ToString());
                        info.PubDigFee = FS.FrameWork.Function.NConvert.ToDecimal(row["�������Ʒ�"].ToString());
                        info.OthFee = FS.FrameWork.Function.NConvert.ToDecimal(row["���ӷ�"].ToString());
                        info.RegLevel.ID = row["�������"].ToString();
                        info.RegLevel.Name = row["�Һż���"].ToString();
                        this.al.Add(info);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("����ʵ�弯��ʱ����!" + e.Message, "��ʾ");
                    return null;
                }
            }
            return al;

        }
        /// <summary>
        /// �趨��ʾ��ʽ
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.MaximumValue = 2499.99;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            this.fpSpread1_Sheet1.Columns[0].CellType = txtCellType;
            this.fpSpread1_Sheet1.Columns[0].Locked = true;
            this.fpSpread1_Sheet1.Columns[1].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[2].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[3].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[4].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[5].CellType = numbCellType;
            //{989D0388-3A0A-4664-A805-B797322BFAB6}
            this.fpSpread1_Sheet1.Columns[4].Visible = this.isShowPubDiagFee;
            this.fpSpread1_Sheet1.Columns[6].Visible = false;
            this.fpSpread1_Sheet1.Columns[7].Visible = false;

            //{A53D57D8-E44D-4517-8B24-E13D686D6F1B}
            #region �����Ѵ���
            ///����������0���յ���1��������2��������
            string rtn = this.ctlMgr.QueryControlerInfo("400027");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";

            switch (rtn)
            {
                case "0":
                    {
                        //������
                        this.fpSpread1_Sheet1.Columns[5].Label = "����";
                        break;
                    }
                case "1": //��������
                    {
                        this.fpSpread1_Sheet1.Columns[5].Label = "��������";
                        break;
                    }
                case "2": //������
                    {
                        
                        this.fpSpread1_Sheet1.Columns[5].Label = "������";
                        break;
 
                    }
                default:
                    break;
            }
            #endregion
            
       
        }

        private void getNamebyId(string levelCode)
        {
            foreach (FS.HISFC.Models.Registration.RegLevel level in alLevel)
            {
                if (levelCode == level.ID)
                {
                    levelName = level.Name.ToString();
                    break;
                }
            }
        }
        #endregion

        #endregion

        #region �¼�
        
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)//���Ǹ��ڵ�
            {
                this.MakeAll(e.Node.Tag.ToString());
                this.Query(e.Node.Tag.ToString());
            }
            else
            {
                this.al = new ArrayList();
                this.Query("NULL");
            }
        }

        /// <summary>
        /// �Ƿ��޸����ݣ�
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            this.fpSpread1.StopCellEditing();

            DataTable dt = dtRegFee.GetChanges();

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                if (MessageBox.Show("�����Ѿ��޸�,�Ƿ񱣴�䶯?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.Save();
                    return true;
                }
            }
            return true;
        }


        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            this.IsChange();
        }

        protected override int OnSave(object sender, object neuObject)
        {
            Save();

            return base.OnSave(sender, neuObject);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }
        #endregion

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool selectedFlag = e.Node.Checked;

            if (e.Node.Parent == null)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = selectedFlag;
                }
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node.Parent == null)
                node.ExpandAll();           
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ExpandAll();
        }
    }
}
