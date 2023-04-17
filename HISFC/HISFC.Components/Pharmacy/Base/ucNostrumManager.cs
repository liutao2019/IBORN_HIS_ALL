using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: Э������ά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-06]<br></br>
    /// <�޸ļ�¼>
    ///     <�޸�ʱ��>2008-07-04</�޸�ʱ��>
    ///     <�޸�����>
    ///         ���ƹ���
    ///     </�޸�����>
    ///     <�޸���>
    ///         ����   
    ///     </�޸���>
    /// </�޸ļ�¼>
    /// <���ձ��ػ����� ��ժ��>
    ///     {2229C05D-3CF4-4bcc-A3BA-6134F10E0ABA}
    ///     1����ϸ��Ϣ��ʾʱ�����������÷���Ϣ��ʾ �����÷���Ϣ Ϊ���ձ��ػ���Ϣ
    ///         �޸���Pharmacy.Nostrum.Detail ����ָ���Sql���
    ///    2.����Э�������޸�ʱ�����ֵ���Э�������ļ۸� by Sunjh 2010-1-6 {67F2F7C1-8AEC-4f57-ABD0-D823F662C439}
    /// </���ձ��ػ�����>
    /// </summary>
    public partial class ucNostrumManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucNostrumManager()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ҵ�������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ������ͼ
        /// </summary>
        DataTable dt = new DataTable();

        /// <summary>
        /// ҩƷHash����
        /// </summary>
        System.Collections.Hashtable hsDrugData = new System.Collections.Hashtable();

        /// <summary>
        /// ������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper ehUsage = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant conm = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region ��ʼ��

        /// <summary>
        /// Э���������ݼ���
        /// </summary>
        /// <returns></returns>
        private int InitNostrumList()
        {
            this.tvNostrumList.Nodes.Clear();

            List<FS.HISFC.Models.Pharmacy.Item> nostrumList = this.itemManager.QueryNostrumList();
            if (nostrumList == null)
            {
                MessageBox.Show(Language.Msg("����Э������ģ���б�������") + this.itemManager.Err);
                return -1;
            }

            this.tvNostrumList.ImageList = this.tvNostrumList.groupImageList;

            TreeNode rootNode = new TreeNode();
            rootNode.Text = "Э������";
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = rootNode.ImageIndex;

            foreach (FS.HISFC.Models.Pharmacy.Item item in nostrumList)
            {
                TreeNode nostrumNode = new TreeNode();

                nostrumNode.Text = item.Name + "[" + item.Specs + "]";
                nostrumNode.ImageIndex = 4;
                nostrumNode.SelectedImageIndex = 2;
                nostrumNode.Tag = item;

                rootNode.Nodes.Add(nostrumNode);
            }

            this.tvNostrumList.Nodes.Add(rootNode);

            this.tvNostrumList.ExpandAll();

            return 1;
        }

        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int InitDataSet()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");


            //��myDataTable�������
            dt.Columns.AddRange(new DataColumn[] {
                                                                        new DataColumn("��Ʒ����",	  dtStr),
                                                                        new DataColumn("���",        dtStr),
                                                                        new DataColumn("���ۼ�",      dtDec),
                                                                        new DataColumn("ҩƷ����",	  dtStr),
                                                                        new DataColumn("��λ",        dtStr),
                                                                        new DataColumn("ƴ����",      dtStr),
                                                                        new DataColumn("�����",      dtStr),
                                                                        new DataColumn("�Զ�����",    dtStr),
                                                                        new DataColumn("ͨ����ƴ����",dtStr),
                                                                        new DataColumn("ͨ���������",dtStr),
                                                                    });

            this.fsDrugListSheet.DataSource = this.dt.DefaultView;

            return 1;
        }

        /// <summary>
        /// ҩƷ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        private int InitDrugData()
        {
            this.InitDataSet();

            this.hsDrugData.Clear();

            List<FS.HISFC.Models.Pharmacy.Item> itemList = this.itemManager.QueryItemAvailableList(true);
            if (itemList == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ�б�������") + this.itemManager.Err);
            }

            foreach (FS.HISFC.Models.Pharmacy.Item info in itemList)
            {
                if (this.AddDrugToDataTable(info) == -1)
                {
                    return -1;
                }

                this.hsDrugData.Add(info.ID, info);
            }

            //����ҩƷ�б�
            for (int i = 0, j = fsDrugListSheet.Columns.Count; i < j; i++)
            {
                this.fsDrugListSheet.Columns[i].Locked = true;
            }

            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="item">ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDrugToDataTable(FS.HISFC.Models.Pharmacy.Item item)
        {
            try
            {
                this.dt.Rows.Add(new object[] { 
                                                item.Name,                  //��Ʒ����
                                                item.Specs,                 //���
                                                item.PriceCollection.RetailPrice,       //���ۼ�
                                                item.ID,                    //ҩƷ����
                                                item.MinUnit,               //��λ
                                                item.NameCollection.SpellCode,          //ƴ����
                                                item.NameCollection.WBCode,             //�����
                                                item.NameCollection.UserCode,           //�Զ�����
                                                item.NameCollection.RegularSpell.SpellCode,       //ͨ����
                                                item.NameCollection.RegularSpell.WBCode
                                           });
            }
            catch (System.Data.ConstraintException)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ҩƷ�Ѵ��� �����ظ����"));
                return -1;
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + e.Message));

                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + ex.Message));

                return -1;
            }

            return 1;
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ɾ��", "ɾ�������ӵ�Э��������ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelNewNostrumDetail();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveButtonHandler();
            this.tvNostrumList.Tag = null;
            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.InitNostrumList();

            return base.OnQuery( sender, neuObject );
        }

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ�ɱ༭
        /// </summary>
        /// <returns>����༭ ����True ���򷵻�False</returns>
        protected bool IsCanEdit()
        {
            if (this.fsNostrumDetailSheet.Rows.Count <= 0)      //��ǰ����ϸ���� ˵������ҩƷ
            {
                return true;
            }

            if (this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColFlug].Text == "0")        //ԭ��ά����Ϣ
            {
                MessageBox.Show( "��Э��������ȷ�ϱ����,�����ٽ�����ϸ�����޸�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop );
                return false;
            }

            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected void Clear()
        {
            this.fsNostrumDetailSheet.Rows.Count = 0;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected int AddNewNostrumDetail()
        {
            if (this.tvNostrumList.SelectedNode.Tag == null)
            {
                MessageBox.Show( "��ѡ����Ҫά������Ŀ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return -1;
            }

            if (this.IsCanEdit() == false)          //��������б༭
            {
                return -1;
            }

            FS.HISFC.Models.Pharmacy.Item selectItem = (FS.HISFC.Models.Pharmacy.Item)this.tvNostrumList.SelectedNode.Tag;
            string pcode = selectItem.ID;
            string pname = selectItem.Name + "[" + selectItem.Specs + "]";
            string itemID = this.fsDrugListSheet.GetText( this.fsDrugListSheet.ActiveRowIndex, 3 );

            //�ж��Ƿ���ڸ�ҩƷ
            if (this.IsNewLineExists( pcode, itemID ))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "������ϸ�Ѿ����ڸĴ�����Ŀ" ) );
                return -1;
            }

            FS.HISFC.Models.Pharmacy.Item detailItem = this.itemManager.GetItem( itemID );
            if (detailItem == null)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "����ҩƷ�����ȡҩƷ��ϸ��Ϣ��������  " ) + this.itemManager.Err );
                return -1;
            }

            FS.HISFC.Models.Pharmacy.Nostrum nostrum = new FS.HISFC.Models.Pharmacy.Nostrum();
            nostrum.ID = pcode;
            nostrum.Name = pname;

            nostrum.Item = detailItem;

            nostrum.Qty = NConvert.ToDecimal( 1 );

            nostrum.IsValid = true;
            nostrum.SortNO = 1;

            //��ҩƷ�б���ӵ������б���
            CreateNewLineInFpDetails( nostrum );

            this.fsNostrumDetail.Focus();
            this.fsNostrumDetailSheet.ActiveColumnIndex = (int)NostrumDetailColumn.ColQty;

            return 1;
        }

        /// <summary>
        /// ��Э�������б��м���һ��
        /// </summary>
        /// <param name="info"></param>
        private void CreateNewLineInFpDetails(FS.HISFC.Models.Pharmacy.Nostrum info)
        {
            this.fsNostrumDetailSheet.Rows.Add( 0, 1 );

            this.fsNostrumDetailSheet.SetText( 0, (int)NostrumDetailColumn.ColID, info.ID );
            this.fsNostrumDetailSheet.SetText( 0, (int)NostrumDetailColumn.ColItemID, info.Item.ID );
            this.fsNostrumDetailSheet.SetText( 0, (int)NostrumDetailColumn.ColName, info.Name );
            this.fsNostrumDetailSheet.SetText( 0, (int)NostrumDetailColumn.ColItemName, info.Item.Name );
            this.fsNostrumDetailSheet.SetText( 0, (int)NostrumDetailColumn.ColQty, info.Qty.ToString() );
            this.fsNostrumDetailSheet.SetText( 0, (int)NostrumDetailColumn.ColUnit, info.Item.MinUnit );
            this.fsNostrumDetailSheet.SetValue( 0, (int)NostrumDetailColumn.ColValid, info.IsValid );
            this.fsNostrumDetailSheet.SetText( 0, (int)NostrumDetailColumn.ColSortNO, info.SortNO.ToString() );
            this.fsNostrumDetailSheet.SetValue( 0, (int)NostrumDetailColumn.ColFlug, 1 );


            this.fsNostrumDetailSheet.SetValue( 0, (int)NostrumDetailColumn.ColPriceR, info.Item.PriceCollection.RetailPrice );
            this.fsNostrumDetailSheet.SetValue( 0, (int)NostrumDetailColumn.ColPriceP, info.Item.PriceCollection.PurchasePrice );

            this.fsNostrumDetailSheet.SetValue( 0, (int)NostrumDetailColumn.ColSumR, Math.Round( info.Item.PriceCollection.RetailPrice * info.Qty / info.Item.PackQty, 2 ) );

            this.fsNostrumDetailSheet.SetValue( 0, (int)NostrumDetailColumn.ColSumP, Math.Round( info.Item.PriceCollection.PurchasePrice * info.Qty / info.Item.PackQty, 2 ) );
        }        

        /// <summary>
        /// ɾ��Э��������ϸ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int DelNewNostrumDetail()
        {
            if (this.fsNostrumDetailSheet.Rows.Count <= 0)
            {
                return 1;
            }
            int rowIndex = this.fsNostrumDetailSheet.ActiveRowIndex;

            if (this.fsNostrumDetailSheet.Cells[rowIndex, (int)NostrumDetailColumn.ColFlug].Text == "0")     //������Ϣ
            {
                MessageBox.Show( "�ѱ������Э��������Ϣ�������������ϸ�����޸ģ������޸ģ������ϵ�ǰЭ������������ά��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop );
                return -1;
            }

            DialogResult rs = MessageBox.Show("�Ƿ�ɾ����ѡ���Э��������ϸ��Ϣ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                return 1;
            }

            this.fsNostrumDetailSheet.Rows.Remove(rowIndex, 1);

            return 1;
        }

        /// <summary>
        /// ����Э��������ϸ��Ϣ
        /// </summary>
        /// <param name="nostrumItem"></param>
        protected void ShowNostrumDetail(FS.HISFC.Models.Pharmacy.Item nostrumItem)
        {
            List<FS.HISFC.Models.Pharmacy.Nostrum> nostrumDetailCollection = this.itemManager.QueryNostrumDetail(nostrumItem.ID);
            if (nostrumDetailCollection == null)
            {
                MessageBox.Show(Language.Msg("����Э��������ϸ��Ϣ��������") + this.itemManager.Err);
                return;
            }
            //{E49F9CEA-2E6D-4b2e-919F-99145BEE3E68} ������ʾ��Ϣ��ʾ
            this.tabDetail.Text = nostrumItem.Name + "[" + nostrumItem.Specs + "]   ���� 1" + nostrumItem.PackUnit + "  �������ϸ������";

            this.ShowNostrumDetail( nostrumItem,nostrumDetailCollection );

            if (nostrumDetailCollection.Count > 0)
            {
                this.fsNostrumDetail.Enabled = false;
            }
            else
            {
                this.fsNostrumDetail.Enabled = true;
            }

            if (nostrumDetailCollection.Count > 0)
            {            
                this.fsNostrumDetailSheet.Columns[9].Locked = true;
                this.fsNostrumDetailSheet.Columns[10].Locked = true;
                this.fsNostrumDetailSheet.Columns[11].Locked = true;
                this.fsNostrumDetailSheet.Columns[12].Locked = true;
                this.fsNostrumDetailSheet.Columns[13].Locked = true;
                this.fsNostrumDetailSheet.Columns[14].Locked = true;
            }
            else
            {
                this.fsNostrumDetailSheet.Columns[9].Locked = false;
                this.fsNostrumDetailSheet.Columns[10].Locked = false;
                this.fsNostrumDetailSheet.Columns[11].Locked = false;
                this.fsNostrumDetailSheet.Columns[12].Locked = false;
                this.fsNostrumDetailSheet.Columns[13].Locked = false;
                this.fsNostrumDetailSheet.Columns[14].Locked = false;
            }
        }

        /// <summary>
        /// Э��������ϸ��Ϣ��ʾ
        /// </summary>
        /// <param name="nostrumDetailCollection">Э��������ϸ��Ϣ</param>
        /// <param name="nostrumItem">Э��������Ϣ</param>
        protected void ShowNostrumDetail(FS.HISFC.Models.Pharmacy.Item nostrumItem,List<FS.HISFC.Models.Pharmacy.Nostrum> nostrumDetailCollection)
        {
            this.fsNostrumDetailSheet.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.Nostrum info in nostrumDetailCollection)
            {
                this.fsNostrumDetailSheet.Rows.Add(0, 1);

                //Э����������
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColID].Text = info.ID;
                //ҩƷ����
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColItemID].Text = info.Item.ID;
                //Э���������
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColName].Text = nostrumItem.Name + "[" + nostrumItem.Specs + "]";
                //ҩƷ����
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColItemName].Text = info.Item.Name;
                //����
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColQty].Text = info.Qty.ToString();
                //��λ
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColUnit].Text = info.Item.MinUnit;
                //�Ƿ����
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColValid].Value = info.IsValid;
                //�����
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColSortNO].Text = info.SortNO.ToString();
                //0��־������Ϣ��1��־�²�����Ϣ
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColFlug].Value = 0;

                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColPriceR].Value = info.Item.PriceCollection.RetailPrice;
                //{E49F9CEA-2E6D-4b2e-919F-99145BEE3E68}  ������
                decimal prr = Math.Round( info.Qty * info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 2 );
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColSumR].Text = prr.ToString();

                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColPriceP].Value = info.Item.PriceCollection.PurchasePrice;
                //{E49F9CEA-2E6D-4b2e-919F-99145BEE3E68}  ������
                decimal prp = Math.Round( info.Qty * info.Item.PriceCollection.PurchasePrice / info.Item.PackQty, 2 );
                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColSumP].Text = prp.ToString();

                this.fsNostrumDetailSheet.Cells[0, (int)NostrumDetailColumn.ColUsage].Text = ehUsage.GetName(info.Item.Usage.ID);

                this.fsNostrumDetailSheet.Rows[0].Tag = info;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected int Filter()
        {
            string filterStr = Function.GetFilterStr(this.dt.DefaultView, "%" + this.txtFilter.Text + "%");

            this.dt.DefaultView.RowFilter = filterStr;

            return 1;
        }

        /// <summary>
        /// ��ʾЭ����������ϸ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvNostrum_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node.Parent != null)
            {
                if (e.Node.Tag != null)
                {
                    this.ShowNostrumDetail(e.Node.Tag as FS.HISFC.Models.Pharmacy.Item);
                }
            }
        }

        /// <summary>
        /// �ж�ҩƷ�Ƿ��Ѿ�����������
        /// </summary>
        /// <param name="packageid">���ױ���</param>
        /// <param name="itemid">ҩƷ����</param>
        /// <returns></returns>
        private bool IsNewLineExists(string packageid, string itemid)
        {
            for (int i = 0, j = this.fsNostrumDetailSheet.Rows.Count; i < j; i++)
            {
                if (this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColID).Trim().Equals(packageid) &&
                    this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColItemID).Trim().Equals(itemid))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ��֤����{AB3CDE46-C95D-4a6f-96FF-E970F8C84523}
        /// </summary>
        private bool ValidNum()
        {
            for (int i = 0; i < this.fsNostrumDetailSheet.RowCount; i++)
            {
                try
                {
                    decimal num = FS.FrameWork.Function.NConvert.ToDecimal(this.fsNostrumDetailSheet.Cells[i, 4].Value);
                    if (num > 9999.9999M)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg(this.fsNostrumDetailSheet.Cells[i, 3].Text + " ��������"));
                        return false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(" ��������ȷ��" + this.fsNostrumDetailSheet.Cells[i, 3].Text + "������"));
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// ����͸���Э��������Ϣ
        /// </summary>
        private void SaveButtonHandler()
        {
            if (this.fsNostrumDetailSheet.Rows.Count == 0)
            {
                return;
            }

            if (this.IsCanEdit() == false)          //��������б༭
            {
                return;
            }

            if (MessageBox.Show("��ȷ����¼�����Ϣ�Ƿ���ȷ������󽫲����ٽ����޸�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //����cellֵ
            this.fsNostrumDetail.StopCellEditing();

            //{AB3CDE46-C95D-4a6f-96FF-E970F8C84523}��ֹ�����������
            if (this.ValidNum() == false)
            {
                return;
            }


            //���ڲ����Э�������б�
            List<FS.HISFC.Models.Pharmacy.Nostrum> lstNostrumInsert = new List<FS.HISFC.Models.Pharmacy.Nostrum>();
            //���ڸ��µ�Э�������б�
            List<FS.HISFC.Models.Pharmacy.Nostrum> lstNostrumUpdate = new List<FS.HISFC.Models.Pharmacy.Nostrum>();
            //Э������ID
            string nostrumID = "";

            #region ��Э����������ϸ��Ϣ�б�ת��Ϊʵ��

            for (int i = 0, j = this.fsNostrumDetailSheet.Rows.Count; i < j; i++)
            {

                FS.HISFC.Models.Pharmacy.Nostrum nostrum = new FS.HISFC.Models.Pharmacy.Nostrum();
                nostrum.ID = this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColID).Trim();
                nostrum.Item.ID = this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColItemID).Trim();
                nostrum.Name = this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColName).Trim().Split('[')[0].ToString();
                nostrum.Item.Name = this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColItemName).Trim();
                nostrum.Item.Specs = this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColName).Trim().Split('[')[1].Split(']')[0].ToString();
                nostrum.Qty = NConvert.ToDecimal(this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColQty).Trim());
                nostrum.Item.MinUnit = this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColUnit).Trim();
                nostrum.IsValid = NConvert.ToBoolean(this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColValid).Trim());
                nostrum.SortNO = NConvert.ToInt32(this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColSortNO));
                nostrum.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                nostrum.Oper.OperTime = itemManager.GetDateTimeFromSysDateTime();

                //������²�����з�������б�
                if (NConvert.ToInt32(this.fsNostrumDetailSheet.GetText(i, (int)NostrumDetailColumn.ColFlug)) == 1)
                {
                    lstNostrumInsert.Add(nostrum);
                }
                //��������е��з�������б�
                else
                {
                    lstNostrumUpdate.Add(nostrum);
                }

                nostrumID = nostrum.ID;
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {                
                //�������������
                for (int i = 0, j = lstNostrumInsert.Count; i < j; i++)
                {
                    if (itemManager.InsertNostrum(lstNostrumInsert[i]) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ��") + itemManager.Err);
                        return;
                    }
                }

                //�������е�����
                for (int i = 0, j = lstNostrumUpdate.Count; i < j; i++)
                {
                    if (itemManager.UpdateNostrum(lstNostrumUpdate[i]) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ��") + itemManager.Err);
                        return;
                    }
                }
                //������ҩƷ���¼۸�
                if (this.itemManager.UpdateNostrumPrice( nostrumID ) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��������ʧ��" ) + this.itemManager.Err );
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ݳɹ�"));

                for (int i = 0, j = this.fsDrugListSheet.RowCount; i < j; i++)
                {
                    this.fsNostrumDetailSheet.SetValue(0, (int)NostrumDetailColumn.ColFlug, 0);
                }
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ��"));
            }

            this.tvNostrumList.SelectedNode = this.tvNostrumList.Nodes[0];
        }

        /// <summary>
        /// ����Э��������Ϣ��Excel
        /// </summary>
        private void ExportInfo()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel(*.xls)|*.xls";
            saveFile.Title = "��Э��������Ϣ������Excel";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFile.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        this.fsNostrumDetail.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                        MessageBox.Show("�����ɹ�!");
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("����ʧ��!��ȷ���ļ�δ����");
                    }
                    catch
                    {
                        MessageBox.Show("����ʧ��!");
                    }
                }
                else
                {
                    MessageBox.Show("�ļ�������Ϊ��!");
                    return;
                }
            }
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControls()
        {
            ehUsage.ArrayObject = conm.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //��ʼ��Э��������
                this.InitDrugData();

                //��ʼ��ҩƷ�б�
                this.InitNostrumList();
                this.InitControls();

                this.toolBarService.SetToolButtonEnabled("ɾ��", true);
            }

            base.OnLoad(e);
        }

        private void fsDrugList_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                return;
            }

            this.AddNewNostrumDetail();
        }

        /// <summary>
        /// ��֤ҩƷ�����ĺϷ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsNostrumDetail_EditModeOff(object sender, EventArgs e)
        {
            for (int i = 0, j = fsNostrumDetailSheet.Rows.Count; i < j; i++)
            {

                try
                {
                    if (NConvert.ToInt32(fsNostrumDetailSheet.GetText(i, 4)) < 0)
                    {
                        MessageBox.Show("ҩƷ����������Ϊ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.fsNostrumDetailSheet.SetText(i, 4, "1");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ҩƷ����������Ϊ�Ǹ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.fsNostrumDetailSheet.SetText(i, 4, "1");
                }
            }
        }

        /// <summary>
        /// ����Э��������Ϣ��Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.ExportInfo();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ��ť�¼�����ʵ�����¼��ƶ�ҩƷ�б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fsDrugListSheet.RowCount > 0)
            {
                //ʹ�����ϼ�
                if (e.KeyCode == Keys.Up)
                {
                    //�������һ���Ժ󣬷��ص����һ��
                    if (this.fsDrugListSheet.ActiveRowIndex == 0)
                    {
                        this.fsDrugListSheet.ActiveRowIndex = this.fsDrugListSheet.RowCount - 1;
                    }
                    else
                    {
                        this.fsDrugListSheet.ActiveRowIndex--;
                    }
                }

                //ʹ�����ϼ�
                if (e.KeyCode == Keys.Down)
                {
                    //���������һ��ʱ�����ص�һ��
                    if (this.fsDrugListSheet.ActiveRowIndex == this.fsDrugListSheet.RowCount - 1)
                    {
                        this.fsDrugListSheet.ActiveRowIndex = 0;
                    }
                    else
                    {
                        this.fsDrugListSheet.ActiveRowIndex++;
                    }
                }

                e.Handled = true;
            }
            //ʹ�ûس���
            if (e.KeyCode == Keys.Enter)
            {
                this.AddNewNostrumDetail();
            }
        }

        #endregion

        #region ��ö��

        /// <summary>
        /// Э��������Ŀö��
        /// </summary>
        private enum NostrumDetailColumn
        {
            /// <summary>
            /// Э����������
            /// </summary>
            ColID = 0,

            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColItemID = 1,

            /// <summary>
            /// ��Ŀ���ƹ��
            /// </summary>
            ColName = 2,

            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColItemName = 3,

            /// <summary>
            /// ����
            /// </summary>
            ColQty = 4,

            /// <summary>
            /// ��λ
            /// </summary>
            ColUnit = 5,

            /// <summary>
            /// ��Ч��
            /// </summary>
            ColValid = 6,

            /// <summary>
            /// ˳���
            /// </summary>
            ColSortNO = 7,

            /// <summary>
            /// ��־�Ƿ���������
            /// </summary>
            ColFlug = 8,

            /// <summary>
            /// ���ۼ�
            /// </summary>
            ColPriceR = 9,

            /// <summary>
            /// ���۽��
            /// </summary>
            ColSumR = 10,

            /// <summary>
            /// ����
            /// </summary>
            ColPriceP = 11,

            /// <summary>
            /// ������
            /// </summary>
            ColSumP = 12,

            /// <summary>
            /// ���÷���
            /// </summary>
            ColUsage = 13,

            /// <summary>
            /// �����÷�
            /// </summary>
            ColSpeUsage = 14
        }

        #endregion

    }
}
