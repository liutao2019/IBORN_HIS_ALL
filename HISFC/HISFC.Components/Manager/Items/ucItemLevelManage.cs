using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Manager.Items
{
    public partial class ucItemLevelManage : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucItemLevelManage()
        {
            InitializeComponent( );
            this.IniConstant();
        }

        #region ����

        //ҩ��������
        //private Neusoft.HISFC.BizLogic.Pharmacy.Constant pharmacyConstant = new Neusoft.HISFC.BizLogic.Pharmacy.Constant( );
        private Neusoft.HISFC.Models.Pharmacy.PhaFunction functionObject = null;
        //���������࣭ȡ�����б�
        private Neusoft.HISFC.BizLogic.Manager.Constant consManager = new Neusoft.HISFC.BizLogic.Manager.Constant();
      
        //������
        Neusoft.FrameWork.Public.ObjectHelper ehSysClass = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper ehType = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper ehQuality = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper ehMinFee = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper ehFunction1 = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper ehFunction2 = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper ehFunction3 = new Neusoft.FrameWork.Public.ObjectHelper();

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��ListView
        /// </summary>
        private void InitListView( )
        {
            //����б�
            this.lvFunctionList.Items.Clear( );
            if( this.tvFunction.SelectedNode.Nodes.Count > 0 )
            {
                foreach( TreeNode node in this.tvFunction.SelectedNode.Nodes )
                {
                    ListViewItem lvi = new ListViewItem( );
                    lvi.Text = node.Text;
                    lvi.Tag = node;
                    lvi.ImageIndex = node.ImageIndex;
                    this.lvFunctionList.Items.Add( lvi );
                }
            }
        }

        /// <summary>
        /// �ݹ�����������ڵ�
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="newnodecode"></param>
        /// <param name="newnode"></param>
        private void GetAllNode( TreeNodeCollection tnc , string newnodecode , TreeNode newnode ) //����������
        {
            foreach( TreeNode node in tnc )
            {
                if( node.Nodes.Count != 0 )
                {
                    GetAllNode( node.Nodes , newnodecode , newnode );
                }
                if( newnodecode == node.Tag.ToString( ) )
                {
                    node.Nodes.Add( newnode );
                    SettreeImage( node , false );
                }
            }
        }

        /// <summary>
     /// �������ڵ�ͼ�� true ��ʾ��Ҷ�ӽڵ�
     /// </summary>
     /// <param name="node"></param>
     /// <param name="iFtrue"></param>
        public void SettreeImage( TreeNode node , bool iFtrue )
        {
            if( iFtrue )
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }
            else
            {
                node.ImageIndex = 2;
                node.SelectedImageIndex = 1;
            }
        }

        /// <summary>
        /// ��ѯҩƷ�б� by Sunjh 2008-09-22
        /// </summary>
        /// <param name="fCode">ҩ�����ô���</param>
        /// <param name="fLevl">ҩ�����÷ּ�</param>
        private void ShowDrugList(string fCode, int fLevl)
        {
            if (fCode == "" || fCode == null)
            {
                return;
            }
            Neusoft.HISFC.BizLogic.Pharmacy.Constant cItem = new Neusoft.HISFC.BizLogic.Pharmacy.Constant();
            List<Neusoft.HISFC.Models.Pharmacy.Item> al = cItem.QueryItemListByFunctionID(fCode, 1);
            if (al != null)
            {
                this.PutList(al);
            }
            else
            {
                fpDrugList.RowCount = 0;
            }
        }

        /// <summary>
        /// ����ҩƷ�б��ؼ�
        /// </summary>
        /// <param name="al"></param>
        private void PutList(List<Neusoft.HISFC.Models.Pharmacy.Item> al)
        {
            fpDrugList.RowCount = 0;
            fpDrugList.RowCount = al.Count;
            for (int i = 0; i < al.Count; i++)
            {
                Neusoft.HISFC.Models.Pharmacy.Item pItem = new Neusoft.HISFC.Models.Pharmacy.Item();
                pItem = al[i];
                fpDrugList.Cells[i, 0].Text = pItem.ID;
                fpDrugList.Cells[i, 1].Text = pItem.Name;
                fpDrugList.Cells[i, 2].Text = pItem.NameCollection.RegularName;
                fpDrugList.Cells[i, 3].Text = pItem.PackQty.ToString();
                fpDrugList.Cells[i, 4].Text = pItem.Specs;
                //ϵͳ���
                fpDrugList.Cells[i, 5].Text = ehSysClass.GetName(pItem.SysClass.ID.ToString());
                //��Ŀ����                
                fpDrugList.Cells[i, 6].Text = ehMinFee.GetName(pItem.MinFee.ID);
                fpDrugList.Cells[i, 7].Text = pItem.PackUnit;
                fpDrugList.Cells[i, 8].Text = pItem.MinUnit;
                //ҩƷ���
                fpDrugList.Cells[i, 9].Text = ehType.GetName(pItem.Type.ID);
                //ҩƷ����
                fpDrugList.Cells[i, 10].Text = ehQuality.GetName(pItem.Quality.ID);
                fpDrugList.Cells[i, 11].Text = pItem.PriceCollection.RetailPrice.ToString();
                //��Ч��
                if (pItem.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Valid)
                {
                    fpDrugList.Cells[i, 12].Text = "��Ч";
                }
                else if (pItem.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    fpDrugList.Cells[i, 12].Text = "��Ч";
                }
                else if (pItem.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    fpDrugList.Cells[i, 12].Text = "����";
                }
                else if (pItem.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Extend)
                {
                    fpDrugList.Cells[i, 12].Text = "��չ";
                }

                fpDrugList.Cells[i, 13].Text = pItem.UserCode;
                fpDrugList.Cells[i, 14].Text = pItem.NameCollection.EnglishName;
                fpDrugList.Cells[i, 15].Text = ehFunction1.GetName(pItem.PhyFunction1.ID);
                fpDrugList.Cells[i, 16].Text = ehFunction2.GetName(pItem.PhyFunction2.ID);
                fpDrugList.Cells[i, 17].Text = ehFunction3.GetName(pItem.PhyFunction3.ID);
            }
            
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void IniConstant()
        {
            this.ehSysClass.ArrayObject = Neusoft.HISFC.Models.Base.SysClassEnumService.List();//ϵͳ���
            this.ehType.ArrayObject = consManager.GetList(Neusoft.HISFC.Models.Base.EnumConstant.ITEMTYPE);//ҩƷ���
            this.ehQuality.ArrayObject = consManager.GetList(Neusoft.HISFC.Models.Base.EnumConstant.DRUGQUALITY);//ҩƷ����
            this.ehMinFee.ArrayObject = consManager.GetList(Neusoft.HISFC.Models.Base.EnumConstant.MINFEE);//��Ŀ����
            ArrayList alLevel1Function = new ArrayList(this.pharmacyConstant.QueryPhaFunction());//(1, "NONE").ToArray());//һ��ҩ������
            this.ehFunction1.ArrayObject = new ArrayList(alLevel1Function.ToArray());
            //alLevel1Function = new ArrayList(this.pharmacyConstant.QueryPhaFunctionByLevel(2, "NONE").ToArray());//����ҩ������
            //this.ehFunction2.ArrayObject = new ArrayList(alLevel1Function.ToArray());
            //alLevel1Function = new ArrayList(this.pharmacyConstant.QueryPhaFunctionByLevel(3, "NONE").ToArray());//����ҩ������
            //this.ehFunction3.ArrayObject = new ArrayList(alLevel1Function.ToArray());
        }

        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService( );

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender , object NeuObject , object param )
        {
            this.toolBarService.AddToolButton( "�ϲ�" , "�����ϲ�Ŀ¼" , Neusoft.FrameWork.WinForms.Classes.EnumImageList.X��һ�� , true , false , null );
            return this.toolBarService;
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked( object sender , ToolStripItemClickedEventArgs e )
        {
            switch( e.ClickedItem.Text )
            {
                case "�ϲ�":
                    TreeNode node;
                    if( this.tvFunction.Nodes.Count == 0 )
                    {
                        return;
                    }
                    node = this.tvFunction.SelectedNode.Parent;
                    if( node != null )
                    {
                        this.tvFunction.SelectedNode = node;
                    }
                    break;
            }          
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ���ڵ�ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFunction_AfterSelect( object sender , TreeViewEventArgs e )
        {
            if (e.Node != null)
            {
                this.InitListView();
                this.ShowDrugList(e.Node.Tag.ToString(), e.Node.Level);
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            //ֻչ�����ĵ�һ��
            if( this.tvFunction.Nodes.Count > 0 )
            {
                this.tvFunction.Nodes[ 0 ].Expand( );
                //Ĭ��ѡ�и��ڵ�
                this.tvFunction.SelectedNode = this.tvFunction.Nodes[ 0 ];
            }

            base.OnLoad( e );
        }

        #region �˵��¼�

        /// <summary>
        /// �б���ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowList_Click( object sender , EventArgs e )
        {
            this.menuShowList.Checked = true;
            this.menuShowLarge.Checked = false;
            this.menuShowSmall.Checked = false;

            this.lvFunctionList.View = View.List;
        }

        /// <summary>
        ///  ��ͼ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowLarge_Click( object sender , EventArgs e )
        {
            this.menuShowLarge.Checked = true;
            this.menuShowSmall.Checked = false;
            this.menuShowList.Checked = false;

            this.lvFunctionList.View = View.LargeIcon;
        }

        /// <summary>
        /// Сͼ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowSmall_Click( object sender , EventArgs e )
        {
            this.menuShowLarge.Checked = false;
            this.menuShowSmall.Checked = true; 
            this.menuShowList.Checked = false;

            this.lvFunctionList.View = View.SmallIcon;
        }

        #endregion

        /// <summary>
        /// �б���ͼ�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFunctionList_Click( object sender , EventArgs e )
        {
            TreeNode node;
            if( this.lvFunctionList.SelectedItems.Count > 0 && this.lvFunctionList.Focused == true )
            {
  
                node = this.lvFunctionList.SelectedItems[ 0 ].Tag as TreeNode;
                if (node == null)
                {
                    return;
                }      
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// �б���ͼ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFunctionList_DoubleClick( object sender , EventArgs e )
        {
            if( this.lvFunctionList.SelectedItems.Count > 0 && this.lvFunctionList.Focused )
            {
                //��ǰ�ڵ��»��нڵ���򿪣��������Ӧ
                TreeNode node = this.lvFunctionList.SelectedItems[ 0 ].Tag as TreeNode;
                if (node == null)
                {
                    return;
                }
                if( node.Nodes.Count >= 1 )
                {
                    this.tvFunction.SelectedNode = node;
                    ShowDrugList(node.Tag.ToString(), node.Level);
                }
            }
        }

        #endregion

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string strCode = this.fpDrugList.Cells[e.Row, 0].Text;
            Neusoft.HISFC.BizLogic.Pharmacy.Item Pitem = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
            Neusoft.HISFC.Models.Pharmacy.Item al = new Neusoft.HISFC.Models.Pharmacy.Item();
              al = Pitem.GetItem(strCode);
            this.neutext.Text=al.Product.Manual;
           
           
        }



    }
}
