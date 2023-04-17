using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucBedInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.Manager.Bed bed = new FS.HISFC.BizLogic.Manager.Bed();
        private DataView dvBedInfo = new DataView();

        public ucBedInfo()
        {
            InitializeComponent();
        }

        private void ucBedInfo_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.InitTreeView() == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ʼ����ʿվ�б�ʧ��"));                
                }
                this.dvBedInfo = new DataView();
                this.bed.QueryBedInfo(ref this.dvBedInfo);
                this.neuSpread1_Sheet1.DataSource = this.dvBedInfo;
            }
            catch
            {
                //MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ʼ��ʧ��"));                
            }
        }

        /// <summary>
        /// ��ʼ����ʿվ�б�
        /// </summary>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        private int InitTreeView()
        {
            System.Collections.ArrayList alBeds = new System.Collections.ArrayList();
            alBeds = this.bed.QueryNurseStationInfo();
            if (alBeds == null)
            {
                return -1;
            }

            TreeNode root = new TreeNode("��ʿվ�б�");
            this.tvNurseStation.Nodes.Add(root);
            for (int i = 0, j = alBeds.Count; i < j; i++)
            {
                TreeNode tn = new TreeNode(((FS.HISFC.Models.Base.Bed)alBeds[i]).Name);
                tn.Tag = ((FS.HISFC.Models.Base.Bed)alBeds[i]).ID;
                root.Nodes.Add(tn);
            }
            return 1;
        }

        private void tvNurseStation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                this.dvBedInfo = new DataView();
                this.bed.QueryBedInfo(ref this.dvBedInfo);
                this.neuSpread1_Sheet1.DataSource = this.dvBedInfo;
                return;
                //��ʾ���д�λ��Ϣ
            }
            string id = (string)e.Node.Tag;

            this.dvBedInfo = new DataView();
            this.bed.QueryBedInfoByNurseStationID(id, ref this.dvBedInfo);
            this.neuSpread1_Sheet1.DataSource = this.dvBedInfo;
        }
    }
}
