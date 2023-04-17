using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ�����ҩƷ�б�]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-04]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class tvDrugList : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvDrugList()
        {
            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        public tvDrugList ( IContainer container )
        {
            container.Add(this);

            InitializeComponent();

            this.ImageList = this.groupImageList;
        }
       
        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation ( );
        /// <summary>
        /// ������Ʒ��Ϣ
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> prescriptionList;
        /// <summary>
        /// ҩƷ�б�
        /// </summary>
        /// <param name="planState"></param>
        public void ShowDrugList( )
        {
            this.Nodes.Clear ( );
            this.prescriptionList = this.preparationManager.QueryPrescriptionList ( FS.HISFC.Models.Base.EnumItemType.Drug);
            if ( this.prescriptionList == null )
            {
                MessageBox.Show ( Language.Msg ( "δ��ȷ��ȡ��Ʒ���ƴ�����Ϣ \n" + this.preparationManager.Err ) );
                return ;
            }
            if ( this.prescriptionList.Count == 0 )
            {
                this.Nodes.Add ( new System.Windows.Forms.TreeNode ( "û��ҩƷ" , 0 , 0 ) );
            }
            else
            {
                System.Windows.Forms.TreeNode parentNode= new System.Windows.Forms.TreeNode ( "ҩƷ�б�" , 0 , 0 ) ;
                this.Nodes.Add ( parentNode );
                System.Windows.Forms.TreeNode drugNode = new TreeNode ( );
                foreach (FS.FrameWork.Models.NeuObject info in this.prescriptionList)
                {
                    drugNode = new System.Windows.Forms.TreeNode ( info.Name+"��"+info.Memo+"��" );
                    drugNode.Tag = info;
                    drugNode.ImageIndex = 2;
                    drugNode.SelectedImageIndex = 4;
                    parentNode.Nodes.Add ( drugNode );
                }
                this.Nodes [ 0 ].ExpandAll ( );
                this.SelectedNode = this.Nodes [ 0 ];
            }
            
           
        }

       
    }
}
