using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ�����ƻ��б�]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class tvPlanList : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvPlanList()
        {
            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        public tvPlanList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        /// <summary>
        /// ���ƻ������б�
        /// </summary>
        /// <param name="planState">�ƻ�״̬</param>
        public void ShowPlanList(params FS.HISFC.Models.Preparation.EnumState[] stateCollection)
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();

            List<FS.HISFC.Models.Preparation.Preparation> alList = new List<FS.HISFC.Models.Preparation.Preparation>();

            foreach (FS.HISFC.Models.Preparation.EnumState state in stateCollection)
            {
                List<FS.HISFC.Models.Preparation.Preparation> alTempList = preparationManager.QueryPreparation(state);
                if (alTempList == null)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ�Ƽ��ƻ��б�������" + preparationManager.Err));
                    return;
                }

                //{FF5101E6-3188-4928-AEAE-6C2B55A9848D}  �Լ��鲻�ϸ����Ŀ�б���й���
                List<FS.HISFC.Models.Preparation.Preparation> alFilterList = new List<FS.HISFC.Models.Preparation.Preparation>();
                foreach (FS.HISFC.Models.Preparation.Preparation info in alTempList)
                {
                    if (info.IsAssayEligible == true)
                    {
                        alFilterList.Add(info);
                    }
                }

                alList.AddRange(alFilterList);
            }

            if (alList.Count == 0)
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("û�мƻ���", 0, 0));
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("�ƻ����б�", 0, 0);
                this.Nodes.Add(parentNode);

                System.Windows.Forms.TreeNode planNode = new TreeNode();
                string privPlanNO = "";
                foreach (FS.HISFC.Models.Preparation.Preparation info in alList)
                {
                    if (privPlanNO != info.PlanNO)      //��Ӽƻ����ڵ�
                    {
                        planNode = new System.Windows.Forms.TreeNode(info.PlanNO);
                        planNode.Tag = info;
                        planNode.ImageIndex = 2;
                        planNode.SelectedImageIndex = 4;

                        parentNode.Nodes.Add(planNode);

                        privPlanNO = info.PlanNO;
                    }
                }

                this.Nodes[0].ExpandAll();

                this.SelectedNode = this.Nodes[0];
            }
        }

       
    }
}
