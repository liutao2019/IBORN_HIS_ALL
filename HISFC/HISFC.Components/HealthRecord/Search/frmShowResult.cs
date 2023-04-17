using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class frmShowResult : Form
    {
        public frmShowResult()
        {
            InitializeComponent();
        }

        #region ȫ�ֱ���
        //�洢סԺ��ˮ���б�
        private string inpatientNoList = "";
        //ҵ���
        private FS.HISFC.BizLogic.HealthRecord.SearchManager SearMan = new FS.HISFC.BizLogic.HealthRecord.SearchManager();
        //�ݴ����ݵġ�����
        private System.Data.DataSet ds = null;

        public bool boolClose = false;
        #endregion 
        #region�����ԡ�סԺ��ˮ���ַ���
        /// <summary>
        /// סԺ��ˮ���б�
        /// </summary>
        public string InpatientNoList
        {
            get
            {
                return inpatientNoList;
            }
            set
            {
                inpatientNoList = value;
            }
        }
        #endregion

        public void InitTree()
        {
            //			this.treeView1.ImageIndex = 1;
            //			this.treeView1.ImageList = this.ilTreeView;
            //			this.treeView1.Location = new System.Drawing.Point(32, 160);
            //			this.treeView1.Name = "treeView1";
            //			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            //																				  new System.Windows.Forms.TreeNode("��ʾ����", new System.Windows.Forms.TreeNode[] {
            //		new System.Windows.Forms.TreeNode("��ʾ������"),
            //		new System.Windows.Forms.TreeNode("��ʾ�����źʹ���"),
            //		new System.Windows.Forms.TreeNode("��ʾ�·�ͳ��"),
            //		new System.Windows.Forms.TreeNode("��ʾ���ͳ��"),
            //		new System.Windows.Forms.TreeNode("��ʾ���ͳ��(���ϼ�)"),
            //		new System.Windows.Forms.TreeNode("��ʾ������"),
            //		new System.Windows.Forms.TreeNode("�����ֲ��˴α�"),
            //		new System.Windows.Forms.TreeNode("ְҵ���µ����"),
            //		new System.Windows.Forms.TreeNode("��ʾ�ز�����"),
            //		new System.Windows.Forms.TreeNode("��������ͳ�Ʊ�"),
            //		new System.Windows.Forms.TreeNode("һ���ڸ���Ժͳ�Ʊ�")})});
            this.treeView1.tvList.ImageList = this.ilTreeView;
            TreeNode tnParentUr;
            //����ͷ  δ�Ǽǲ����б�
            tnParentUr = new TreeNode();
            tnParentUr.Text = "��ʾ����";
            tnParentUr.ImageIndex = 0;
            tnParentUr.SelectedImageIndex = 0;
            //���ؽڵ� 
            this.treeView1.tvList.Nodes.Add(tnParentUr);

            TreeNode tnPatient = new TreeNode();

            tnPatient = new TreeNode();
            tnPatient.Text = "��ʾ������";
            tnPatient.ImageIndex = 1;
            tnPatient.SelectedImageIndex = 2;
            tnParentUr.Nodes.Add(tnPatient);

            tnPatient = new TreeNode();
            tnPatient.Text = "��ʾ�����źʹ���";
            tnPatient.ImageIndex = 1;
            tnPatient.SelectedImageIndex = 2;
            tnParentUr.Nodes.Add(tnPatient);

            //tnPatient = new TreeNode();
            //tnPatient.Text = "��ʾ�·�ͳ��";
            //tnPatient.ImageIndex = 1;
            //tnPatient.SelectedImageIndex = 2;
            //tnParentUr.Nodes.Add(tnPatient);


            //tnPatient = new TreeNode();
            //tnPatient.Text = "��ʾ���ͳ��";
            //tnPatient.ImageIndex = 1;
            //tnPatient.SelectedImageIndex = 2;
            //tnParentUr.Nodes.Add(tnPatient);

            //tnPatient = new TreeNode();
            //tnPatient.Text = "��ʾ���ͳ��(���ϼ�)";
            //tnPatient.ImageIndex = 1;
            //tnPatient.SelectedImageIndex = 2;
            //tnParentUr.Nodes.Add(tnPatient);

            tnPatient = new TreeNode();
            tnPatient.Text = "�����ֲ��˴α�";
            tnPatient.ImageIndex = 1;
            tnPatient.SelectedImageIndex = 2;
            tnParentUr.Nodes.Add(tnPatient);

            tnPatient = new TreeNode();
            tnPatient.Text = "ְҵ���µ����";
            tnPatient.ImageIndex = 1;
            tnPatient.SelectedImageIndex = 2;
            tnParentUr.Nodes.Add(tnPatient);

            tnPatient = new TreeNode();
            tnPatient.Text = "��ʾ�ز�����";
            tnPatient.ImageIndex = 1;
            tnPatient.SelectedImageIndex = 2;
            tnParentUr.Nodes.Add(tnPatient);

            tnPatient = new TreeNode();
            tnPatient.Text = "��������ͳ�Ʊ�";
            tnPatient.ImageIndex = 1;
            tnPatient.SelectedImageIndex = 2;
            tnParentUr.Nodes.Add(tnPatient);

            tnPatient = new TreeNode();
            tnPatient.Text = "һ���ڸ���Ժͳ�Ʊ�";
            tnPatient.ImageIndex = 1;
            tnPatient.SelectedImageIndex = 2;
            tnParentUr.Nodes.Add(tnPatient);

            tnParentUr.Expand();
            this.treeView1.tvList.EndUpdate();
            //���嵥���¼� 
            this.treeView1.tvList.AfterSelect += new TreeViewEventHandler(tvList_AfterSelect);
        }

        private void frmShowResult_Load(object sender, System.EventArgs e)
        {
            InitTree();

            this.ilMenu.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ));
            this.ilMenu.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.L�б�));
            this.ilMenu.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�));
            this.toolBar1.ImageList = this.ilMenu;
            this.tbPrint.ImageIndex = 0;
            this.tbList.ImageIndex = 1;
            this.tbExist.ImageIndex = 2;
           

        }

        private void frmShowResult_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!boolClose)
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }

        private void LockFp()
        {
            try
            {
                switch (this.treeView1.tvList.SelectedNode.Text)
                {
                    case "��ʾ������": //ֻ��ʾ������ 
                        this.fpSpread1_Sheet1.Columns[0].Width = 100;
                        break;
                    case "��ʾ�����źʹ���": //��ʾ�����źʹ���
                        this.fpSpread1_Sheet1.Columns[0].Width = 100;
                        this.fpSpread1_Sheet1.Columns[1].Width = 50;
                        break;
                    case "��ʾ�·�ͳ��":
                        //if (this.fpSpread1_Sheet1.RowCount <= 0) return;
                        //fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
                        //int count = this.fpSpread1_Sheet1.Rows.Count - 1;
                        //fpSpread1_Sheet1.Cells[count, 0].Text = "�ϼ�: ";
                        //fpSpread1_Sheet1.Cells[count, 1].Formula = "sum(B1:B" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 2].Formula = "sum(C1:C" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 3].Formula = "sum(D1:D" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 4].Formula = "sum(E1:E" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 5].Formula = "sum(F1:F" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 6].Formula = "sum(G1:G" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 7].Formula = "sum(H1:H" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 8].Formula = "sum(I1:I" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 9].Formula = "sum(J1:J" + count.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count, 10].Formula = "sum(K1:K" + count.ToString() + ")";
                        //break;
                    case "��ʾ���ͳ��":
                        //if (this.fpSpread1_Sheet1.RowCount <= 0) return;
                        //fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
                        //int count1 = this.fpSpread1_Sheet1.Rows.Count - 1;
                        //fpSpread1_Sheet1.Cells[count1, 0].Text = "�ϼ�: ";
                        //fpSpread1_Sheet1.Cells[count1, 1].Formula = "sum(B1:B" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 1].Formula = "sum(B1:B" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 2].Formula = "sum(C1:C" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 3].Formula = "sum(D1:D" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 4].Formula = "sum(E1:E" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 5].Formula = "sum(F1:F" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 6].Formula = "sum(G1:G" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 7].Formula = "sum(H1:H" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 8].Formula = "sum(I1:I" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 9].Formula = "sum(J1:J" + count1.ToString() + ")";
                        //fpSpread1_Sheet1.Cells[count1, 10].Formula = "sum(K1:K" + count1.ToString() + ")";
                        //break;
                    case "��ʾ���ͳ��(���ϼ�)":
                        //						fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count,1);
                        //						int count2 = this.fpSpread1_Sheet1.Rows.Count -1 ;
                        //						fpSpread1_Sheet1.Cells[count2,0].Text = "�ϼ�: ";
                        //						fpSpread1_Sheet1.Cells[count2,1].Formula="sum(B1:B"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,1].Formula="sum(B1:B"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,2].Formula="sum(C1:C"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,3].Formula="sum(D1:D"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,4].Formula="sum(E1:E"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,5].Formula="sum(F1:F"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,6].Formula="sum(G1:G"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,7].Formula="sum(H1:H"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,8].Formula="sum(I1:I"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,9].Formula="sum(J1:J"+count2.ToString()+")";
                        //						fpSpread1_Sheet1.Cells[count2,10].Formula="sum(K1:K"+count2.ToString()+")";
                        break;
                    case "��ʾ������":
                        this.fpSpread1_Sheet1.Columns[0].Width = 80;//������
                        this.fpSpread1_Sheet1.Columns[1].Width = 30;//����
                        this.fpSpread1_Sheet1.Columns[2].Width = 30;//�Ա�
                        this.fpSpread1_Sheet1.Columns[3].Width = 50;//����
                        this.fpSpread1_Sheet1.Columns[4].Width = 60;//����
                        this.fpSpread1_Sheet1.Columns[5].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[6].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[7].Width = 65;//��Ժ�Ʊ�
                        this.fpSpread1_Sheet1.Columns[8].Width = 65;//����ҽ��
                        this.fpSpread1_Sheet1.Columns[9].Width = 100;//��һ���
                        this.fpSpread1_Sheet1.Columns[10].Width = 50;//�������,
                        this.fpSpread1_Sheet1.Columns[11].Width = 50;//ת��
                        this.fpSpread1_Sheet1.Columns[12].Width = 100;//��������
                        break;
                    case "�����ֲ��˴α�":
                        this.fpSpread1_Sheet1.Columns[0].Width = 60;//����
                        this.fpSpread1_Sheet1.Columns[1].Width = 200;//����
                        this.fpSpread1_Sheet1.Columns[2].Width = 50;//����
                        break;
                    case "ְҵ���µ����":
                        this.fpSpread1_Sheet1.Columns[0].Width = 60;//����
                        this.fpSpread1_Sheet1.Columns[1].Width = 40;//�Ա�
                        this.fpSpread1_Sheet1.Columns[2].Width = 50;//����
                        this.fpSpread1_Sheet1.Columns[3].Width = 65;//סԺ��
                        this.fpSpread1_Sheet1.Columns[4].Width = 20;//����
                        this.fpSpread1_Sheet1.Columns[5].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[6].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[7].Width = 50;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[8].Width = 50;//�����ʱ�
                        this.fpSpread1_Sheet1.Columns[9].Width = 120;//������ַ
                        this.fpSpread1_Sheet1.Columns[10].Width = 120;//��������
                        this.fpSpread1_Sheet1.Columns[11].Width = 60;//ת��
                        this.fpSpread1_Sheet1.Columns[12].Width = 60;//סԺҽʦ
                        this.fpSpread1_Sheet1.Columns[13].Width = 60;//����ҽ��
                        this.fpSpread1_Sheet1.Columns[14].Width = 60;//����ҽ��
                        break;
                    case "��ʾ�ز�����":
                        this.fpSpread1_Sheet1.Columns[0].Width = 80;
                        break;
                    case "��������ͳ�Ʊ�"://��������ͳ�Ʊ�
                        this.fpSpread1_Sheet1.Columns[0].Width = 80;//������
                        this.fpSpread1_Sheet1.Columns[1].Width = 30;//����
                        this.fpSpread1_Sheet1.Columns[2].Width = 30;//�Ա�
                        this.fpSpread1_Sheet1.Columns[3].Width = 50;//����
                        this.fpSpread1_Sheet1.Columns[4].Width = 60;//����
                        this.fpSpread1_Sheet1.Columns[5].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[6].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[7].Width = 65;//��Ժ�Ʊ�
                        this.fpSpread1_Sheet1.Columns[8].Width = 65;//����ҽ��
                        this.fpSpread1_Sheet1.Columns[9].Width = 65;//������
                        this.fpSpread1_Sheet1.Columns[10].Width = 80;//��������,
                        this.fpSpread1_Sheet1.Columns[11].Width = 20;//��������
                        this.fpSpread1_Sheet1.Columns[12].Width = 65;//��������
                        this.fpSpread1_Sheet1.Columns[13].Width = 50;//�п�
                        this.fpSpread1_Sheet1.Columns[14].Width = 50;//����
                        this.fpSpread1_Sheet1.Columns[15].Width = 20;//������ 
                        break;
                    case "һ���ڸ���Ժͳ�Ʊ�":
                        this.fpSpread1_Sheet1.Columns[0].Width = 65;//סԺ��
                        this.fpSpread1_Sheet1.Columns[1].Width = 60;//����
                        this.fpSpread1_Sheet1.Columns[2].Width = 30;//�Ա�
                        this.fpSpread1_Sheet1.Columns[3].Width = 20;//����
                        this.fpSpread1_Sheet1.Columns[4].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[5].Width = 65;//��Ժ�Ʊ�
                        this.fpSpread1_Sheet1.Columns[6].Width = 65;//��Ժ����
                        this.fpSpread1_Sheet1.Columns[7].Width = 65;//��Ժ�Ʊ�
                        this.fpSpread1_Sheet1.Columns[8].Width = 50;//����
                        this.fpSpread1_Sheet1.Columns[9].Width = 90;//ICD 
                        this.fpSpread1_Sheet1.Columns[10].Width = 200;//��һ�������
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintInfo()
        {
            try
            {

                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
                p.PrintPreview(this.neuPanel1);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            switch (this.toolBar1.Buttons.IndexOf(e.Button))
            {
                case 0:
                    PrintInfo();
                    break;
                case 1:
                    this.treeView1.Visible = !this.treeView1.Visible;
                    break;
                case 2:
                    this.Close();
                    break;
            }
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.treeView1.tvList.SelectedNode.Text == "��ʾ����")
                {
                    return;
                }
                ds = new System.Data.DataSet();
                SearMan.GetInfoBySql(treeView1.tvList.SelectedNode.Text, ref ds, inpatientNoList);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        this.fpSpread1_Sheet1.DataSource = ds.Tables[0];
                    }
                }
                LockFp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
