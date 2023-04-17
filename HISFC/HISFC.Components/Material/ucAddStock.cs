using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material
{
    /// <summary>
    /// [��������: ���ʿ���ʼ��]
    /// [�� �� ��: wangw]
    /// [����ʱ��: 2008-3-14]
    /// </summary>
    public partial class ucAddStock : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ���췽��

        public ucAddStock()
        {
            InitializeComponent();
        }

        #endregion

        #region �ֶ�

        private DataView dvMatList;

        private DataSet dsMat = new DataSet();

        /// <summary>
        /// ���ʿ�Ŀ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper matTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��������������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper qualityHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store matManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// ���ʻ�����Ϣҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem matItemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ���ʻ�������ҵ���߼���
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset matBaseSet = new FS.HISFC.BizLogic.Material.Baseset();

        /// <summary>
        /// ���Ǵ�
        /// </summary>
        private string filter = "1=1";

        /// <summary>
        /// �Ƿ�ӵ��Ȩ��
        /// </summary>
        private bool isHavePriv = false;

        /// <summary>
        /// Ȩ�޿�����
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        #endregion

        #region ����

        /// <summary>
        /// ������Ʒ��Ϣ
        /// </summary>
        private void RetrieveData()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ���������Ϣ...���Ժ�!"));

            Application.DoEvents();

            List<FS.HISFC.Models.Material.MaterialItem> alMatItem = this.matItemManager.GetMetItemList();

            if (alMatItem == null)
            {
                MessageBox.Show(matItemManager.Err);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }

            //ȡ��Ʒ��������
            this.matTypeHelper.ArrayObject = this.matBaseSet.QueryKindAllByID("0");

            FS.HISFC.Models.Material.MaterialItem info;
            for (int i = 0; i < alMatItem.Count; i++)
            {
                info = alMatItem[i] as FS.HISFC.Models.Material.MaterialItem;
                this.dsMat.Tables[0].Rows.Add(new object[]
                    {
                        false,//�Ƿ����
                        info.Name,//��Ʒ����
                        info.Specs,//��Ʒ���
                        info.UnitPrice,//���۽��(�������ʳ������=���ۼ�)
                        info.PackUnit,//��װ��λ
                        info.PackQty,//��װ����
                        info.MinUnit,//��С��λ
                        info.ID,//��Ʒ����
                        info.SpellCode,//ƴ����
                        info.WbCode,//�����
                        info.UserCode,//�Զ�����
                        info.MaterialKind.ID//��������
                    }
                    );
                //���ø�ʽ
                this.SetFormat();
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, alMatItem.Count);
                Application.DoEvents();
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

        /// <summary>
        /// ��ʼ����ͼ
        /// </summary>
        private void InitDataView()
        {
            this.dsMat.Tables.Clear();
            this.dsMat.Tables.Add();
            this.dvMatList = new DataView(this.dsMat.Tables[0]);
            this.neuSpread1_Sheet1.DataSource = this.dvMatList;
            this.dvMatList.AllowEdit = true;

            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            this.dsMat.Tables[0].Columns.AddRange(new DataColumn[]{
                                                                       new DataColumn("���",dtBool),
                                                                       new DataColumn("��Ʒ����",dtStr),
                                                                       new DataColumn("���",dtStr),
                                                                       new DataColumn("���ۼ�",dtDec),
                                                                       new DataColumn("��װ��λ",dtStr),
                                                                       new DataColumn("��װ����",dtDec),
                                                                       new DataColumn("��С��λ",dtStr),
                                                                       new DataColumn("��Ʒ����",dtStr),
                                                                       new DataColumn("ƴ����",dtStr),
                                                                       new DataColumn("�����",dtStr),
                                                                       new DataColumn("�Զ�����",dtStr),
                                                                       new DataColumn("��������",dtStr)
                                                                       });
        }

        /// <summary>
        /// ����farpoint��ʽ
        /// </summary>
        private void SetFormat()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 38F;

            this.neuSpread1_Sheet1.Columns.Get(1).Label = "��Ʒ����";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 129F;

            this.neuSpread1_Sheet1.Columns.Get(2).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "���ۼ�";
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;

            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��װ��λ";
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;

            this.neuSpread1_Sheet1.Columns.Get(5).Label = "��װ����";
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 42F;

            this.neuSpread1_Sheet1.Columns.Get(6).Label = "��С��λ";
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 41F;

            this.neuSpread1_Sheet1.Columns.Get(7).Label = "��Ʒ����";
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;

            this.neuSpread1_Sheet1.Columns.Get(8).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(9).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get(9).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(9).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(10).Label = "�Զ�����";
            this.neuSpread1_Sheet1.Columns.Get(10).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(11).Label = "��������";
            this.neuSpread1_Sheet1.Columns.Get(11).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(11).Visible = true;
        }

        /// <summary>
        /// ѡ���¼�
        /// </summary>
        /// <param name="isSelectAll"></param>
        private void SelectMat(bool isSelectAll)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelectAll;
            }
        }

        /// <summary>
        /// �Ϸ��Լ��
        /// </summary>
        private int ValidCheck()
        {
            //�ж�����¼���Ƿ���ȷ
            if (this.txtSum.Text == string.Empty || this.txtSum.Text.Trim() == "")
            {
                MessageBox.Show(Language.Msg("��¼��Ҫ���ӵĿ����������С��λ��"), Language.Msg("��ʾ"));
                return -1;
            }

            //�ж�¼�������Ƿ������

            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtSum.Text) <= 0)
            {
                MessageBox.Show(Language.Msg("�������������"), Language.Msg("����¼�����"));
                this.txtSum.Focus();
                return -1;
            }

            //ֹͣ���ݱ༭״̬
            for (int i = 0; i < this.dvMatList.Count; i++)
            {
                this.dvMatList[i].EndEdit();
            }
            //���ù�������
            this.dvMatList.RowFilter = this.filter + " and ��� = true";
            //���ø�ʽ
            this.SetFormat();

            //�ж��Ƿ����ҩƷ����dvMatList
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(Language.Msg("��ѡ��Ҫ��ӵ�ҩƷ"), Language.Msg("��ʾ"));
                return -1;
            }

            if (MessageBox.Show(Language.Msg("ȷ��Ҫ������ѡ�еġ�") + this.neuSpread1_Sheet1.Rows.Count.ToString() + Language.Msg("������Ŀ�����"), Language.Msg("ȷ�����ӿ��"), MessageBoxButtons.YesNo) == DialogResult.No) return -1;

            return 0;
        }

        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void AddStock()
        {
            if (this.ValidCheck() < 0)
            {
                return;
            }

            List<FS.HISFC.Models.Material.MaterialStorage> alDept = this.tvDeptTree1.SelectNodes;
            if (alDept.Count == 0)
            {
                MessageBox.Show(Language.Msg("��ѡ��Ҫ��ӵĿⷿ"), Language.Msg("��ʾ"));
                return;
            }
            if (!this.isHavePriv)
            {
                bool isAllowEdit = false;
                foreach (FS.HISFC.Models.Material.MaterialStorage dept in alDept)
                {
                    if (dept.ID == this.privDept.ID)
                    {
                        isAllowEdit = true;
                    }
                }
                if (!isAllowEdit)
                {
                    MessageBox.Show(Language.Msg("����Ȩ�޲��ܶ����������ҽ��п���ʼ��! �����ҩƷ������Ϣά��Ȩ��"), Language.Msg("��ʾ"));
                    return;
                }
            }

            //�������ݿ⴦������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.matManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                string matCode = "";
                decimal quantity = FS.FrameWork.Function.NConvert.ToDecimal(this.txtSum.Text);
                bool IsUpdate = false;
                bool check = false;
                FS.HISFC.Models.Material.StoreDetail storeDetail = new FS.HISFC.Models.Material.StoreDetail();

                storeDetail.StoreBase.StockNO = this.matManager.GetNewStockNO();
                storeDetail.StoreBase.BatchNO = "1";
                storeDetail.StoreBase.ValidTime = this.matManager.GetDateTimeFromSysDateTime().AddYears(5);
                storeDetail.StoreBase.Quantity = quantity;
                storeDetail.StoreBase.StoreQty = quantity;
                storeDetail.StoreBase.PlaceNO = "0";
                storeDetail.StoreBase.ID = "0";
                storeDetail.StoreBase.SerialNO = 0;
                storeDetail.StoreBase.SystemType = "01";
                storeDetail.StoreBase.PrivType = "00";
                storeDetail.StoreBase.Class2Type = "0510";
                storeDetail.Memo = "����ʼ��";
                storeDetail.StoreBase.Item.ValidState = true;
                storeDetail.StoreBase.Operation.Oper.OperTime = this.matManager.GetDateTimeFromSysDateTime();

                foreach (FS.HISFC.Models.Material.MaterialStorage dept in alDept)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, this.neuSpread1_Sheet1.RowCount);
                        Application.DoEvents();

                        //���û��ѡ�У��򲻴����������
                        check = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);
                        if (!check)
                        {
                            continue;
                        }

                        matCode = this.neuSpread1_Sheet1.Cells[i, 7].Text;

                        storeDetail.StoreBase.StockDept.ID = dept.ID;
                        storeDetail.StoreBase.TargetDept.ID = dept.ID;
                        storeDetail.StoreBase.Item = this.matItemManager.GetMetItemByMetID(matCode);

                        if (storeDetail.StoreBase.Item == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("�޷�ת��ΪstoreDetail.StoreBase.Item����"));
                            return;
                        }

                        if (storeDetail.StoreBase.Quantity == 0)
                        {
                            continue;
                        }

                        storeDetail.StoreBase.PriceCollection.PurchasePrice = storeDetail.StoreBase.Item.UnitPrice;
                        storeDetail.StoreBase.PriceCollection.RetailPrice = storeDetail.StoreBase.PriceCollection.PurchasePrice;
                        storeDetail.StoreBase.StoreCost = quantity * storeDetail.StoreBase.PriceCollection.PurchasePrice;

                        #region д����Ч��״̬ {EBFFA2FC-9E48-4b6e-BB0B-2910C6E98501}
                        storeDetail.StoreBase.State = FS.FrameWork.Function.NConvert.ToInt32(storeDetail.StoreBase.Item.ValidState).ToString(); 
                        #endregion

                        if (this.matManager.SetStorage(storeDetail) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.matManager.Err, Language.Msg("���������ʾ"));
                            return;
                        }
                        IsUpdate = true;
                    }
                }

                if (IsUpdate)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show(Language.Msg("����ɹ���"));
                }
                else
                {
                    //���û�и��µ�����,��ع�����.
                    FS.FrameWork.Management.PublicTrans.RollBack();
                }
            }
            catch (System.Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return;
            }

            //��ʾȫ��ҩƷ
            this.dvMatList.RowFilter = "1=1";
            this.SetFormat();
            //ȡ��ѡ��
            this.SelectMat(false);
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.InitDataView();
            this.RetrieveData();
            this.tvKindTree1.Nodes[0].Expand();

            this.isHavePriv = FS.HISFC.BizProcess.Integrate.Pharmacy.ChoosePiv("0510");
            this.privDept = ((FS.HISFC.Models.Base.Employee)(matManager.Operator)).Dept;

            base.OnLoad(e);
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            if (this.dsMat.Tables[0].Rows.Count == 0) return;

            try
            {
                string queryCode = "";
                queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";

                string str = "((ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(�Զ����� LIKE '" + queryCode + "') OR " +
                    "(��Ʒ���� LIKE '" + queryCode + "') )";

                //���ù�������
                this.dvMatList.RowFilter = this.filter + " AND " + str;
                //���ø�ʽ
                this.SetFormat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ���ʿ�Ŀѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvKindTree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.filter = "1=1";
            this.txtQueryCode.Text = "";
            if (e.Node.Parent != null)
            {
                string fife = e.Node.Tag.ToString();
                this.filter = "( �������� = '" + fife + "') ";
            }
            else
            {
                this.filter = "1=1";
            }

            this.dvMatList.RowFilter = this.filter;
            this.SetFormat();
        }


        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //���ӹ�����
            this.toolBarService.AddToolButton("ȫѡ", "ѡ��ȫ��ҩƷ", 0, true, false, null);
            this.toolBarService.AddToolButton("ȫ��ѡ", "ȡ��ѡ��ȫ��ҩƷ", 1, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("������ӿ������..."));
            Application.DoEvents();
            this.AddStock();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ȫѡ":
                    this.SelectMat(true);
                    break;
                case "ȫ��ѡ":
                    this.SelectMat(false);
                    break;

            }

        }

        #endregion
    }
}
