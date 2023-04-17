using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR
{
	/// <summary>
	/// frmSelectTree ��ժҪ˵����
	/// </summary>
	public class frmNodePath : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmNodePath()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 8);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(280, 256);
			this.treeView1.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(144, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(64, 24);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "ȷ��";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(216, 272);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "ȡ��";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmNodePath
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(290, 298);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmNodePath";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ѡ����Ӳ����ڵ�";
			this.Load += new System.EventHandler(this.frmSelectTree_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region ����

		ArrayList arrTree;

		private void GetTreeList()
		{
			//�����ݿ������
            System.Data.DataSet ds = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetNodePath();
			arrTree = new ArrayList();

			//�����ݿ�����ArrayList����
			foreach(System.Data.DataRow dr in ds.Tables[0].Rows)
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = dr["�ڵ�·��"].ToString();
				arrTree.Add(obj);
			}
			
		}

		private void CreateTree()
		{
			this.treeView1.Nodes.Clear();
			TreeNode root = new TreeNode("���Ӳ���");
			this.treeView1.Nodes.Add(root);
			foreach(object obj in arrTree)
			{
				FS.FrameWork.Models.NeuObject neuobj = (FS.FrameWork.Models.NeuObject)obj;
				string[] fullpath = neuobj.ID.Split('\\');
				string parpath = "���Ӳ���";
				TreeNode nodeParent = this.treeView1.Nodes[0];
				foreach(string path in fullpath)
				{
					parpath += "\\" + path ;
					TreeNode node = null;
					GetNodeFromFullPath(parpath, nodeParent, ref node);
					if(node == null)
					{
						node = nodeParent.Nodes.Add(path);
					}
					nodeParent = node;
				}
			}


		}

		private  void GetNodeFromFullPath(string tag,TreeNode tv,ref TreeNode rtnNode)
		{
            if (tv.FullPath == tag)
            {
                rtnNode = tv;
                return;
            }
	
			foreach(TreeNode node in tv.Nodes)
			{
                if (node.FullPath == tag)
                {
                    rtnNode = node;
                    return;
                }
                if (tag.IndexOf(node.FullPath) > 0)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        GetNodeFromFullPath(tag, childNode, ref rtnNode);
                    }
                }
			}
		}

		private void frmSelectTree_Load(object sender, System.EventArgs e)
		{
			GetTreeList();
			CreateTree();
		}
		#endregion ����

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#region ����
		/// <summary>
		/// ���ԣ���ȥ���ڵ�Ľڵ����ȫ·��
		/// </summary>
		public string NodeFullPath
		{
			get
			{
				if(this.treeView1.SelectedNode == null || this.treeView1.SelectedNode == this.treeView1.Nodes[0])
				{
					return null;
				}
				else
				{
					string fullpath = this.treeView1.SelectedNode.FullPath;
					fullpath = fullpath.Substring(5);
					return fullpath;
				}
			}
		}
		#endregion ����
	}
}
