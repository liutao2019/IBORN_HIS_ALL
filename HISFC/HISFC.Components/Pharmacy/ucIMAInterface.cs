using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ��������ʹ�����������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// </summary>
    public partial class ucIMAInterface : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucIMAInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// һ��Ȩ����
        /// </summary>
        private string class1Code = "03";

        /// <summary>
        /// ����Ȩ�޹�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PowerLevelManager class3Manager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
      

        /// <summary>
        /// һ��Ȩ����
        /// </summary>
        public string Class1Code
        {
            get
            {
                return this.class1Code;
            }
            set
            {
                this.class1Code = value;
            }
        }

        /// <summary>
        ///  ��ʼ��Ȩ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected virtual int InitPrivTree()
        {
            FS.HISFC.BizLogic.Manager.PowerLevel2Manager pLevel2Manager = new FS.HISFC.BizLogic.Manager.PowerLevel2Manager();
            //ȡ����Ȩ��
            ArrayList alLevel2 = pLevel2Manager.LoadLevel2All(this.class1Code);
            if (alLevel2 == null)
            {
                MessageBox.Show(Language.Msg(pLevel2Manager.Err));
                return -1;
            }

            this.tvPrivTree.Nodes.Clear();

            foreach (FS.HISFC.Models.Admin.PowerLevelClass2 level2 in alLevel2)
            {
                TreeNode level2Node = new TreeNode(level2.Class2Name);
                level2Node.Tag = level2;

                //ȡϵͳȨ��
                ArrayList alClass3Meaning = this.class3Manager.LoadLevel3Meaning(level2.Class2Code);
                if (alClass3Meaning == null)
                {
                    MessageBox.Show(Language.Msg(this.class3Manager.Err));
                    return -1;
                }

                foreach (FS.FrameWork.Models.NeuObject level3 in alClass3Meaning)
                {
                    TreeNode level3Node = new TreeNode(level3.Name);
                    level3Node.Tag = level3;

                    level2Node.Nodes.Add(level3Node);
                }

                this.tvPrivTree.Nodes.Add(level2Node);
            }            

            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitPrivTree();
            }

            base.OnLoad(e);
        }

        private void tvPrivTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
