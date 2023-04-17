using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Integrate;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material
{
    public partial class ucStoreManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucStoreManager()
        {
            InitializeComponent();
        }

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �����Ʒ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// �����ֵ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

        #endregion

        #region �����

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// XML�����ļ�·��
        /// </summary>
        private string xmlFilePath = @".\\" + FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\MaterialStoreManager.xml";

        /// <summary>
        /// ���ݼ�
        /// </summary>
        private DataTable dtData = null;

        /// <summary>
        /// ������ͼ
        /// </summary>
        private DataView dvData = null;

        /// <summary>
        /// ��ǰ�༭����
        /// </summary>
        private string nowEditColumn = "��λ��";

        /// <summary>
        /// ���ʿ�Ŀ
        /// </summary>
        private string nodeTag = "0";

        /// <summary>
        /// ��λ������ĳ���
        /// </summary>
        private int placeNoLength = 12;

        /// <summary>
        /// ��Ʒ��ϸ��Ϣ��ʾ�ؼ�
        /// </summary>
        private Material.Base.ucMaterialManager DetailUC = null;

        /// <summary>
        /// ��Ч�ھ�ʾ��Ϣ
        /// </summary>
        private Color validDateCautionColor = System.Drawing.Color.Moccasin;

        /// <summary>
        /// ��Ч�ھ�ʾ���� 
        /// </summary>
        private int validDateCautionDays = 90;

        /// <summary>
        /// �������� 
        /// </summary>
        private DateTime sysDate = System.DateTime.MinValue;

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucSetColumn ucColumn = null;

        /// <summary>
        /// �ɱ༭����
        /// </summary>
        private System.Collections.Hashtable hsEditColumn = new Hashtable();

        /// <summary>
        /// ��ת������
        /// </summary>
        private System.Collections.Hashtable hsJumpColumn = new Hashtable();

        /// <summary>
        /// ��������ޱ���ʱ ���徯ʾɫ
        /// </summary>
        private Color warnStoreColor = System.Drawing.Color.Blue;

        /// <summary>
        /// �Ƿ������˳������
        /// </summary>
        private bool isSetJump = false;

        /// <summary>
        /// ��Ʒ�����ϸ��Ч�ڻ�ȡ��ʽ True ʵʱ��ȡ ÿ�ε����ѯʱ���»�ȡ False �б���ʾʱ ֱ�ӻ�ȡ��Ч�Ŀ���¼����С��Ч��
        /// </summary>
        private bool validDateQueryRealTime = true;

        /// <summary>
        /// �Ƿ���п�������ޱ���
        /// </summary>
        private bool isWarnStore = false;

        /// <summary>
        /// ��������ޱ���ʱ �Ƿ񵯳�MessageBox��ʾ
        /// </summary>
        private bool isWarnMessge = false;

        /// <summary>
        /// �Ƿ���������ʾ��ϸ��Ϣ
        /// </summary>
        private bool isShowDrugDetail = true;

        /// <summary>
        /// �Ƿ�ʹ����Ч�ھ�ʾ
        /// </summary>
        private bool isValidDateFlag = false;

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperDept
        {
            set
            {
                this.operDept = value;
            }
            get
            {
                return this.operDept;
            }
        }


        /// <summary>
        /// �Ƿ���������ʾ��ϸ��Ϣ
        /// </summary>
        [Description("�Ƿ���������ʾ��ϸ��Ϣ"), Category("����"), DefaultValue(true)]
        public bool IsShowDrugDetail
        {
            get
            {
                return this.isShowDrugDetail;
            }
            set
            {
                this.isShowDrugDetail = value;
            }
        }


        /// <summary>
        /// ��λ������ĳ���
        /// </summary>
        [Description("��λ������ĳ���"), Category("����"), DefaultValue(12)]
        public int PlaceNoLength
        {
            get
            {
                return this.placeNoLength;
            }
            set
            {
                this.placeNoLength = value;
            }
        }


        /// <summary>
        /// �Ƿ�ʹ����Ч�ھ�ʾ
        /// </summary>
        [Description("�Ƿ�ʹ����Ч�ھ�ʾ"), Category("����"), DefaultValue(false)]
        public bool IsValidDateFlag
        {
            get
            {
                return isValidDateFlag;
            }
            set
            {
                isValidDateFlag = value;
            }
        }


        /// <summary>
        /// ��Ч�ھ�ʾ��Ϣ��ɫ
        /// </summary>
        [Description("��Ч�ھ�ʾ��ɫ"), Category("����")]
        public Color ValidDateCautionColor
        {
            get
            {
                return this.validDateCautionColor;
            }
            set
            {
                this.validDateCautionColor = value;
            }
        }


        /// <summary>
        /// ��Ч�ھ�ʾ���� 
        /// </summary>
        [Description("��Ч�ھ�ʾ����"), Category("����"), DefaultValue(90)]
        public int ValidDateCautionDays
        {
            get
            {
                return validDateCautionDays;
            }
            set
            {
                validDateCautionDays = value;
            }
        }


        /// <summary>
        /// ��Ʒ�����ϸ��Ч�ڻ�ȡ��ʽ 
        /// True ʵʱ��ȡ ÿ�ε����ѯʱ���»�ȡ False �б���ʾʱ ֱ�ӻ�ȡ��Ч�Ŀ���¼����С��Ч��
        /// </summary>
        [Description("��Ʒ�����ϸ��Ч�ڻ�ȡ��ʽ"), Category("����"), DefaultValue(true)]
        public bool ValidDateQueryRealTime
        {
            get
            {
                return this.validDateQueryRealTime;
            }
            set
            {
                this.validDateQueryRealTime = value;
            }
        }


        /// <summary>
        /// �Ƿ���п�������ޱ���
        /// </summary>
        [Description("�Ƿ���п�������ޱ���"), Category("����"), DefaultValue(false)]
        public bool IsWarnStore
        {
            get
            {
                return this.isWarnStore;
            }
            set
            {
                this.isWarnStore = value;
            }
        }


        /// <summary>
        /// ��������ޱ���ʱ �Ƿ񵯳�MessageBox��ʾ
        /// </summary>
        [Description("��������ޱ���ʱ �Ƿ񵯳�MessageBox��ʾ"), Category("����"), DefaultValue(false)]
        public bool IsWarnStoreMessage
        {
            get
            {
                return this.isWarnMessge;
            }
            set
            {
                this.isWarnMessge = value;
            }
        }


        /// <summary>
        /// ��������ޱ���ʱ ���徯ʾɫ
        /// </summary>
        [Description("��������ޱ���ʱ ���徯ʾɫ"), Category("����")]
        public Color WarnStoreColor
        {
            get
            {
                return this.warnStoreColor;
            }
            set
            {
                this.warnStoreColor = value;
            }
        }


        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ˢ��", "ˢ�¿����ϢҩƷ��ʾ", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            toolBarService.AddToolButton("�߼�����", "�Ƿ���ʾ�߼�����", FS.FrameWork.WinForms.Classes.EnumImageList.YԤ��, true, false, null);
            toolBarService.AddToolButton("��ת����", "���ûس���ת����˳��", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            toolBarService.AddToolButton("�鿴��ϸ", "��ѯ��ǰҩƷ�Ŀ����ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ˢ��")
            {
                this.Refresh(false);
            }
            if (e.ClickedItem.Text == "�߼�����")
            {
                this.panelFilter.Visible = !this.panelFilter.Visible;
            }
            if (e.ClickedItem.Text == "��ת����")
            {
                this.SetColumnJumpOrder();
            }
            if (e.ClickedItem.Text == "�鿴��ϸ")
            {
                this.GetData();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return 1;
        }

        public override int SetPrint(object sender, object neuObject)
        {
            this.SetColumnDisplayOrder();

            return 1;
        }

        #endregion

        #region ���ݳ�ʼ��

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        protected virtual void InitData()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

            ArrayList alDept = deptManager.GetDeptment("L");
            if (alDept == null)
            {
                MessageBox.Show("��ȡ�����б�ʧ�ܣ�", "��ʾ");
                return;
            }
            this.cmbDept.Items.Add(new ArrayList(alDept.ToArray()));

            this.ucMaterialKindTree1.InitTreeView();

            #region Fp���λس���

            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #endregion
        }

        /// <summary>
        /// ������ӵ�е�Ȩ�� ���� ��������
        /// </summary>
        private void InitDeptList()
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            string operCode = powerDetailManager.Operator.ID;
            List<FS.FrameWork.Models.NeuObject> alDept = powerDetailManager.QueryUserPriv(operCode, "0502");
            if (alDept == null)
            {
                MessageBox.Show(Language.Msg("���ݿ�����Ȩ�޻�ȡȨ�޿��ҷ�������!") + powerDetailManager.Err);
                return;
            }
            this.cmbDept.AddItems(new ArrayList(alDept.ToArray()));

            if (alDept.Count > 0)
                this.cmbDept.SelectedIndex = 0;
        }

        #endregion

        #region DataTable��������

        /// <summary>
        /// ����DataTableĬ�ϸ�ʽ
        /// </summary>
        protected virtual void InitDefaultDataTable()
        {
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            System.Type dtDtime = System.Type.GetType("System.DateTime");            

            //װ��DataTable��
            this.dtData.Columns.AddRange(new DataColumn[]{
														 new DataColumn("�ֿ����",		dtStr),		//0
														 new DataColumn("��Ʒ����",		dtStr),		//1
														 new DataColumn("��Ŀ����",		dtStr),		//2
														 new DataColumn("��Ŀ����",		dtStr),		//3
														 new DataColumn("��Ʒ����",		dtStr),		//4
														 new DataColumn("���",			dtStr),		//5
														 new DataColumn("�������",		dtDec),		//6
														 new DataColumn("������λ",		dtStr),		//7
														 new DataColumn("����",			dtDec),		//8
														 new DataColumn("�����",		dtDec),		//9
														 new DataColumn("���۵���",		dtDec),		//10
														 new DataColumn("���۽��",		dtDec),		//11
														 new DataColumn("��λ���",		dtStr),		//12
														 new DataColumn("ҽ����Ŀ����",	dtStr),		//13
														 new DataColumn("��Ч��",		dtDtime),	//14
														 new DataColumn("������˾",		dtStr),		//15
														 new DataColumn("��������",		dtStr),		//16
														 new DataColumn("��������",		dtDec),		//17
														 new DataColumn("��������",		dtDec),		//18
														 new DataColumn("�������",		dtStr),		//19
														 new DataColumn("�Ƿ���",		dtBool),	//20
														 new DataColumn("ȱ����־",		dtBool),	//21
														 new DataColumn("��Ч��־",		dtBool),	//22
														 new DataColumn("��ע",			dtStr),		//23
														 new DataColumn("����Ա",		dtStr),		//24
														 new DataColumn("��������",		dtDtime),	//25
														 new DataColumn("��չ�ֶ�",		dtStr),		//26
														 new DataColumn("ƴ����",		dtStr),		//27
														 new DataColumn("�����",		dtStr),		//28
														 new DataColumn("�Զ�����",		dtStr),		//29
														 new DataColumn("ҽ����Ŀ����",	dtStr),		//30
														 new DataColumn("��˾����",		dtStr),		//31
														 new DataColumn("���̱���",		dtStr)		//32
														 });

            this.neuSpread1_Sheet1.DataSource = this.dtData;

            //���ز�����
            try
            {
                this.neuSpread1_Sheet1.Columns[0].Visible = false;
                this.neuSpread1_Sheet1.Columns[1].Visible = false;
                this.neuSpread1_Sheet1.Columns[3].Visible = false;
                //this.neuSpread1_Sheet1.Columns[10].Visible = false;
                //this.neuSpread1_Sheet1.Columns[11].Visible = false;
                this.neuSpread1_Sheet1.Columns[26].Visible = false;
                this.neuSpread1_Sheet1.Columns[27].Visible = false;
                this.neuSpread1_Sheet1.Columns[28].Visible = false;
                this.neuSpread1_Sheet1.Columns[29].Visible = false;
                this.neuSpread1_Sheet1.Columns[30].Visible = false;
                this.neuSpread1_Sheet1.Columns[31].Visible = false;
                this.neuSpread1_Sheet1.Columns[32].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //�����ʽ
            //FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(neuSpread1_Sheet1, xmlFilePath);

        }

        /// <summary>
        /// ����DataTable
        /// </summary>
        protected virtual void SetDataTable()
        {
            this.dtData = new DataTable();

            this.InitDefaultDataTable();

            #region �ɱ༭��
            this.hsEditColumn.Clear();

            if (this.GetColumnIndex("��Ч��־") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��Ч��־"), "��Ч��־");
            if (this.GetColumnIndex("��������") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��������"), "��������");
            if (this.GetColumnIndex("��������") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��������"), "��������");
            if (this.GetColumnIndex("��Ч��") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��Ч��"), "��Ч��");
            if (this.GetColumnIndex("��ע") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��ע"), "��ע");
            if (this.GetColumnIndex("ȱ����־") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("ȱ����־"), "ȱ����־");
            if (this.GetColumnIndex("��λ��") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��λ��"), "��λ��");
            #endregion

            try
            {
                if (System.IO.File.Exists(this.xmlFilePath))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.xmlFilePath);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, this.xmlFilePath);
                }
            }
            catch
            {
                MessageBox.Show(Language.Msg("��ȡ�������ļ���Ϣ��������"));
            }
        }

        /// <summary>
        /// ����DataRow
        /// </summary>
        /// <param name="store">���ʵ��</param>
        /// <returns>DataRow��Ϣ</returns>
        private DataRow SetStorage(FS.HISFC.Models.Material.StoreHead store)
        {
            DataRow row = this.dtData.NewRow();

            try
            {
                row["�ֿ����"] = store.StoreBase.StockDept.ID;							//�ֿ����
                row["��Ʒ����"] = store.StoreBase.Item.ID;								//��Ʒ����	
                row["��Ŀ����"] = store.StoreBase.Item.MaterialKind.ID;					//��Ŀ����
                row["��Ŀ����"] = store.StoreBase.Item.MaterialKind.Name;				//��Ŀ����
                row["��Ʒ����"] = store.StoreBase.Item.Name;							//��Ʒ����
                row["���"] = store.StoreBase.Item.Specs;								//���
                row["�������"] = store.StoreBase.StoreQty.ToString();					//�������
                row["������λ"] = store.StoreBase.Item.MinUnit;							//������λ
                row["����"] = store.StoreBase.AvgPrice.ToString();						//����
                //{A9700EF7-BAED-477a-AB2E-5B4818B904A8}�������еĿ��������д���
                //row["�����"] = store.StoreBase.StoreCost.ToString();					//�����
                row["�����"] = (store.StoreBase.StoreQty * store.StoreBase.AvgPrice).ToString();

                row["���۵���"] = store.StoreBase.AvgSalePrice.ToString();				//���۵���
                //{A9700EF7-BAED-477a-AB2E-5B4818B904A8}�������еĿ��������д���
                //row["���۽��"] = store.StoreBase.RetailCost.ToString();				//���۽��
                row["���۽��"] = (store.StoreBase.StoreQty * store.StoreBase.AvgSalePrice).ToString();

                row["��λ���"] = store.StoreBase.PlaceNO;								//��λ���
                row["ҽ����Ŀ����"] = store.StoreBase.Item.UndrugInfo.Name;				//ҽ����Ŀ����
                row["ҽ����Ŀ����"] = store.StoreBase.Item.UndrugInfo.ID;				//ҽ����Ŀ����
                row["��Ч��"] = store.StoreBase.ValidTime.ToString();					//��Ч��
                row["������˾"] = store.StoreBase.Item.Company.Name;					//������˾����
                row["��������"] = store.StoreBase.Item.Factory.Name;					//������������
                row["��������"] = store.TopQty.ToString();								//��������
                row["��������"] = store.LowQty.ToString();								//��������
                row["�������"] = store.StoreBase.StockType.ToString();					//�������
                row["�Ƿ���"] = store.StoreBase.IsNurse;								//�Ƿ���
                row["ȱ����־"] = store.IsLack;											//ȱ����־
                row["��Ч��־"] = NConvert.ToBoolean(store.StoreBase.State);			//��Ч��־
                row["��ע"] = store.Memo;												//��ע
                row["����Ա"] = store.StoreBase.Operation.Oper.ID.ToString();			//����Ա
                row["��������"] = store.StoreBase.Operation.Oper.OperTime.ToString();	//��������
                row["��չ�ֶ�"] = store.StoreBase.Extend;								//��չ�ֶ�
                row["ƴ����"] = store.StoreBase.Item.SpellCode;						//ƴ����
                row["�����"] = store.StoreBase.Item.WbCode;							//�����
                row["�Զ�����"] = store.StoreBase.Item.UserCode;						//�Զ�����
                row["��˾����"] = store.StoreBase.Item.Company.ID;						//��˾����
                row["���̱���"] = store.StoreBase.Item.Factory.ID;						//���̱���
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return row;
        }

        /// <summary>
        /// ��������Ϣ ���ؿ����Ϣ
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <returns>���ʵ��</returns>
        private FS.HISFC.Models.Material.StoreHead GetStorageModifyInfo(DataRow row)
        {
            FS.HISFC.Models.Material.StoreHead storeInfo = new FS.HISFC.Models.Material.StoreHead();

            try
            {
                storeInfo.StoreBase.StockDept.ID = row["�ֿ����"].ToString();
                storeInfo.StoreBase.Item.ID = row["��Ʒ����"].ToString();
                storeInfo.StoreBase.PlaceNO = row["��λ���"].ToString();
                storeInfo.StoreBase.ValidTime = NConvert.ToDateTime(row["��Ч��"]);
                storeInfo.TopQty = NConvert.ToDecimal(row["��������"]);
                storeInfo.LowQty = NConvert.ToDecimal(row["��������"]);
                storeInfo.IsLack = NConvert.ToBoolean(row["ȱ����־"]);
                storeInfo.StoreBase.State = (NConvert.ToInt32(row["��Ч��־"])).ToString();
                storeInfo.Memo = row["��ע"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return storeInfo;
        }

        #endregion

        #region ��洦��

        /// <summary>
        /// ˢ�µ�ǰ�����ʾ
        /// </summary>
        /// <param name="isResetDataTable">�Ƿ�����DataTable</param>
        public void Refresh(bool isResetDataTable)
        {
            if (this.cmbDept.SelectedItem != null)
                this.ShowStorageData(this.cmbDept.SelectedItem.ID, isResetDataTable);
            else
                this.ClearData();
            this.SetFlag(false);
            base.Refresh();
        }


        /// <summary>
        /// ���ݿ��ұ�����������Ϣ����DataTable���������� 
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        private void ShowStorageData(string deptCode)
        {
            this.ShowStorageData(deptCode, false);
        }


        /// <summary>
        /// ���ؿ������
        /// </summary>
        /// <param name="deptCode">�����ұ���</param>
        /// <param name="isReSetDataTable">�Ƿ�����DataTable</param>
        private void ShowStorageData(string deptCode, bool isReSetDataTable)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��������Ϣ...���Ժ�");
            Application.DoEvents();

            List<FS.HISFC.Models.Material.StoreHead> alStore = this.storeManager.QueryStockHeadAll(deptCode);
            if (alStore == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(this.storeManager.Err);
                return;
            }

            if (isReSetDataTable)
            {
                this.SetDataTable();
            }
            else
            {
                this.ClearData();
            }

            foreach (FS.HISFC.Models.Material.StoreHead storeInfo in alStore)
            {
                #region ͨ���������ж��Ƿ�ʵʱ������Ʒ��Ϣ
                /*
				if (!this.ValidDateQueryRealTime)
				{
                    storeInfo.StoreBase.ValidTime = 
				}
				*/
                #endregion

                storeInfo.StoreBase.Item = this.itemManager.GetMetItemByMetID(storeInfo.StoreBase.Item.ID);
                this.dtData.Rows.Add(this.SetStorage(storeInfo));
            }

            this.dtData.AcceptChanges();

            this.dvData = this.dtData.DefaultView;
            this.dvData.AllowNew = true;
            this.neuSpread1_Sheet1.DataSource = dvData;

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, xmlFilePath);

            this.SetFp();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }


        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Save()
        {
            this.neuSpread1.StopCellEditing();

            this.dvData.RowFilter = "1=1";
            for (int i = 0; i < this.dvData.Count; i++)
            {
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                
               // this.dvData.EndInit();
                this.dvData[i].EndEdit();
            }

            DataTable dtModify = this.dtData.GetChanges(DataRowState.Modified);
            if (dtModify == null || dtModify.Rows.Count <= 0)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.storeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (DataRow dr in dtModify.Rows)
            {
                FS.HISFC.Models.Material.StoreHead storeInfo = this.GetStorageModifyInfo(dr);

                if (storeInfo.LowQty > storeInfo.TopQty)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���治�ܽ��С���" + storeInfo.StoreBase.Item.Name + "����Ϳ�������ܴ��ڿ���������", "��ʾ");
                    return;
                }

                if (this.storeManager.UpdateStockHeadStoreParam(storeInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������� ���¿��ʧ��" + this.storeManager.Err);
                    return;
                }

            }

            FS.FrameWork.Management.PublicTrans.Commit();
            //{F085BC3C-1D84-4771-A2C6-942713785DD7}
            this.dtData.AcceptChanges();
            MessageBox.Show("����ɹ���");

        }


        public void SaveAs()
        {
            this.Save();
        }


        #endregion

        #region ����

        /// <summary>
        /// ���ݵ���
        /// </summary>
        protected virtual void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        protected virtual void ClearData()
        {
            if (this.dtData == null)
            {
                this.dtData = new DataTable();
            }
            this.dtData.Clear();
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public new void Focus()
        {
            this.txtQueryCode.Select();
            this.txtQueryCode.Focus();
            this.txtQueryCode.SelectAll();
        }

        /// <summary>
        /// ��Ʒ��ϸ��Ϣ����
        /// </summary>
        protected virtual void PopDetail(FS.HISFC.Models.Material.MaterialItem item)
        {
            if (this.DetailUC == null)
            {
                this.DetailUC = new Material.Base.ucMaterialManager();
                this.DetailUC.ReadOnly = true;
            }
            this.DetailUC.InputType = "U";
            this.DetailUC.Item = item;

            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "���ʻ�����Ϣ";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.DetailUC);
        }

        /// <summary>
        /// ���������ƻ�ȡ������
        /// </summary>
        /// <param name="colName">������</param>
        /// <returns>�ɹ����������� ʧ�ܷ���-1</returns>
        private int GetColumnIndex(string colName)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Columns[i].Label == colName)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// ��ʾͣ�ñ�� ���������� ������Ч����ʾ
        /// </summary>
        ///<param name="isShowMsg">�Ƿ񵯳���ʾ��Ϣ ����ָ��������ޱ�����</param>
        protected virtual void SetFlag(bool isShowMsg)
        {
            if (this.neuSpread1_Sheet1.Rows.Count >= 1)
            {
                string warnMsg = "";

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.SetRowLabel(i, 0, "");
                    this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.SystemColors.Control;
                    if (this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ч��־")].Text.ToUpper() == "FALSE")
                    {
                        this.neuSpread1_Sheet1.SetRowLabel(i, 0, "ͣ");
                        this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.SetRowLabel(i, 0, i.ToString());
                        this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.LightGray;
                    }

                    #region ��Ч�ھ�ʾ

                    if (this.isValidDateFlag && !this.validDateQueryRealTime)
                    {
                        DateTime tempDate = NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ч��")].Text);
                        if (tempDate > this.sysDate.AddDays(this.validDateCautionDays))
                            this.neuSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.White;
                        else
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                    }

                    #endregion

                    #region ��������޾�ʾ

                    if (this.isWarnStore)
                    {
                        decimal lowQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("�������")].Text);
                        decimal topQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("�������")].Text);
                        decimal storeQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("�������")].Text);

                        if (lowQty == 0 && topQty == 0)
                        {
                            continue;
                        }

                        if (storeQty < lowQty)
                        {
                            if (this.isWarnMessge)
                            {
                                warnMsg = warnMsg + " " + this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ʒ����")].Text;
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Rows[i].ForeColor = this.warnStoreColor;
                            }
                        }
                    }

                    #endregion
                }

                if (this.isWarnStore && this.isWarnMessge)
                {
                    if (warnMsg != "" && isShowMsg)
                    {
                        MessageBox.Show("������Ʒ������������ޣ�\n" + warnMsg);
                    }
                }

                //				this.SetFp();
            }
        }

        #endregion

        #region ��Ϲ��˴���

        /// <summary>
        /// ���� ֻ����ͨ�������������
        /// </summary>
        protected virtual void CodeFilter()
        {
            if (this.dtData.Rows.Count <= 0)
                return;

            try
            {
                string queryCode = "";

                if (this.chbMisty.Checked)
                {
                    queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";
                }
                else
                {
                    queryCode = this.txtQueryCode.Text.Trim() + "%";
                }

                //���ù�������
                queryCode = "((ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(�Զ����� LIKE '" + queryCode + "') OR " +
                    "(��Ʒ���� LIKE '" + queryCode + "'))";

                string filter = "";

                filter = Function.GetFilterStr(this.dvData, queryCode);


                //				this.dvData.RowFilter = filter;
                this.dvData.RowFilter = queryCode;

                this.SetFlag(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��Ϲ���
        /// </summary>
        protected virtual void CombinedFilter()
        {
            string filterStr = "";
            if (this.chbStock.Checked)
            {
                if (this.cmbStock.Text != "")
                {
                    decimal stockNum = NConvert.ToDecimal(this.txtStockNum.Text);
                    filterStr = Function.ConnectFilterStr(filterStr, string.Format("������� {0} {1}", this.cmbStock.Text, stockNum.ToString()), "and");
                }
            }
            if (this.chbState.Checked == true)
            {
                if (this.cmbState.Text != "")
                {
                    if (this.cmbState.Text == "ͣ��")
                        filterStr = Function.ConnectFilterStr(filterStr, string.Format("��Ч��־ = {0}", false), "and");
                    else
                        filterStr = Function.ConnectFilterStr(filterStr, string.Format("��Ч��־ = {0}", true), "and");
                }
            }

            if (this.chbValidDate.Checked)
            {
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("��Ч�� {0} '{1}'", this.cmbValidDate.Text, this.dptValidDate.Value), "and");

            }

            filterStr = Function.ConnectFilterStr(filterStr, string.Format("��Ŀ���� like '{0}%'", this.nodeTag.ToString()), "and");


            if (this.dvData != null)
            {
                this.dvData.RowFilter = filterStr;

                this.SetFlag(false);
            }
        }

        #endregion

        #region Fp��˳������ Fp����ת����

        /// <summary>
        /// ������
        /// </summary>
        private void SetColumnDisplayOrder()
        {
            this.ucColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            this.ucColumn.FilePath = xmlFilePath;
            this.ucColumn.DisplayEvent += new EventHandler(ucColumn_DisplayEvent);
            this.ucColumn.DisplayEvent -= new EventHandler(ucColumn_DisplayEvent);

            this.isSetJump = false;
            this.ucColumn.SetDataTable(this.xmlFilePath, this.neuSpread1_Sheet1);
            this.ucColumn.SetColVisible(true, true, true, false);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucColumn);
        }


        /// <summary>
        /// ��������ת˳��
        /// </summary>
        private void SetColumnJumpOrder()
        {
            #region ��ת����(��ʱ����)

            if (this.ucColumn == null)
            {
                this.ucColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
                this.ucColumn.DisplayEvent -= new EventHandler(ucColumn_DisplayEvent);
                this.ucColumn.DisplayEvent += new EventHandler(ucColumn_DisplayEvent);
            }

            this.isSetJump = true;

            this.ucColumn.SetDataTable(this.xmlFilePath, this.neuSpread1_Sheet1);
            this.ucColumn.SetColVisible(false, false, false, true);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����ת˳������";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucColumn);

            #endregion
        }


        public void SetColumn()
        {
            this.SetColumnDisplayOrder();
        }


        #endregion

        #region Fp����������

        FarPoint.Win.Spread.CellType.TextCellType readonlyTextCell = new FarPoint.Win.Spread.CellType.TextCellType();

        /// <summary>
        /// ����Fpֻ������
        /// </summary>
        private void SetFp()
        {
            this.readonlyTextCell.ReadOnly = true;

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Locked = false;
                if (this.hsEditColumn.Contains(i))
                {
                    continue;
                }
                this.neuSpread1_Sheet1.Columns[i].CellType = this.readonlyTextCell;
            }
        }


        #endregion

        #region �����ϸ

        /// <summary>
        /// ��ѯ��ǰѡ����Ʒ�Ŀ����ϸ��Ϣ
        /// </summary>
        protected virtual void GetData()
        {
            this.neuSpread2_Sheet1.Rows.Count = 0;

            if (this.cmbDept.SelectedItem == null)
                return;

            string itemCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.GetColumnIndex("��Ʒ����")].Text;
            string deptCode = this.cmbDept.SelectedItem.ID;

            List<FS.HISFC.Models.Material.StoreDetail> alStorage = this.storeManager.QueryStoreListAll(deptCode, itemCode, true);
            if (alStorage == null)
            {
                MessageBox.Show("��ȡ��Ʒ�����ϸʧ��", this.storeManager.Err);
            }
            foreach (FS.HISFC.Models.Material.StoreDetail info in alStorage)
            {
                if (info.StoreBase.StoreQty <= 0)
                    continue;

                this.neuSpread2_Sheet1.Rows.Add(0, 1);
                this.neuSpread2_Sheet1.Cells[0, 0].Text = info.StoreBase.StockNO;
                this.neuSpread2_Sheet1.Cells[0, 1].Text = info.StoreBase.Item.Name;
                this.neuSpread2_Sheet1.Cells[0, 2].Text = info.StoreBase.Item.Specs;
                this.neuSpread2_Sheet1.Cells[0, 3].Text = info.StoreBase.PriceCollection.PurchasePrice.ToString("N");
                this.neuSpread2_Sheet1.Cells[0, 4].Text = info.StoreBase.StoreQty.ToString();
                this.neuSpread2_Sheet1.Cells[0, 5].Text = info.StoreBase.Item.MinUnit;
                this.neuSpread2_Sheet1.Cells[0, 6].Text = info.StoreBase.ValidTime.ToString();

                this.txtDetail.Text = "�����ϸ - " + info.StoreBase.Item.Name + "[" + info.StoreBase.Item.Specs + "]";
            }
        }

        public void GetDet()
        {
            this.GetData();
        }

        #endregion

        #region ��Ч�ڼ���

        /// <summary>
        /// ��Ч�ڼ�������
        /// </summary>
        private void ValidDateFilter()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڰ�����Ч�ڽ��в���\n\n������������Ʒ����ɫ��ʾ");

            Application.DoEvents();

            string itemCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.GetColumnIndex("��Ʒ����")].Text;
            string deptCode = this.cmbDept.SelectedItem.ID;
            DateTime minValidDate = System.DateTime.MaxValue;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.validDateQueryRealTime)
                    minValidDate = this.GetMinValidDate(deptCode, itemCode);
                else
                    minValidDate = NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ч��")].Text);
                this.neuSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.White;

                switch (this.cmbValidDate.Text)
                {
                    case "<=":
                        if (minValidDate <= this.dptValidDate.Value)
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                        break;
                    case ">=":
                        if (minValidDate >= this.dptValidDate.Value)
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                        break;
                    case "=":
                        if (minValidDate == this.dptValidDate.Value)
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                        break;
                    default:
                        if (minValidDate <= this.dptValidDate.Value)
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                        break;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }


        /// <summary>
        /// ��ȡ��Ʒ��Ч����¼����С��Ч��
        /// </summary>
        private DateTime GetMinValidDate(string deptCode, string itemCode)
        {
            List<FS.HISFC.Models.Material.StoreDetail> alStorage = this.storeManager.QueryStoreListAll(deptCode, itemCode, true);
            if (alStorage == null)
            {
                MessageBox.Show("��ȡ��Ʒ�����ϸʧ��", this.storeManager.Err);
            }
            DateTime validDate = System.DateTime.MaxValue;
            foreach (FS.HISFC.Models.Material.StoreDetail info in alStorage)
            {
                if (info.StoreBase.StoreQty <= 0)
                    continue;
                if (info.StoreBase.ValidTime < validDate)
                    validDate = info.StoreBase.ValidTime;
            }

            return validDate;
        }


        #endregion

        #region �¼�

        private void ucColumn_DisplayEvent(object sender, EventArgs e)
        {
            if (this.isSetJump)
            {
                #region ��������ת˳��

                List<string> checkCol = this.ucColumn.GetCheckCol(FS.HISFC.Components.Common.Controls.CheckCol.Set);
                this.hsJumpColumn = new Hashtable();
                bool firstColumn = true;
                foreach (string str in checkCol)
                {
                    int iIndex = this.GetColumnIndex(str);
                    this.hsJumpColumn.Add(iIndex, str);
                    if (firstColumn)
                    {
                        this.nowEditColumn = str;
                        firstColumn = false;
                    }
                }

                #endregion
            }
            else
            {
                #region ��������ʾ

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("����Ӧ��������...���Ժ�"));
                Application.DoEvents();

                try
                {
                    this.Refresh(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ӧ��������ʧ��" + ex.Message);
                }
                finally
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }

                #endregion
            }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            this.CombinedFilter();
        }

        private void ucStoreManager_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    this.xmlFilePath = Application.StartupPath + this.xmlFilePath;

                    this.InitData();

                    this.SetDataTable();

                    this.Focus();

                    this.sysDate = this.storeManager.GetDateTimeFromSysDateTime().Date;

                    this.InitDeptList();

                    this.SetFlag(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ucMaterialKindTree1_GetLak(object sender, TreeViewEventArgs e)
        {
            this.nodeTag = e.Node.Tag.ToString();

            string filter = "��Ŀ���� like '" + this.nodeTag.ToString() + "%'";

            if (cmbDept.Text == "")
            {
                MessageBox.Show("��ѡ������ң�", "��ʾ");
                return;
            }

            dvData.RowFilter = filter;
            this.SetFlag(false);
        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.CodeFilter();
        }

        private void txtQueryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex > 0)
                {
                    this.neuSpread1_Sheet1.ActiveRowIndex--;
                    return;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.RowCount)
                {
                    this.neuSpread1_Sheet1.ActiveRowIndex++;
                    return;
                }
            }
        }

        private void txtQueryCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //�س�ʱ����ת�Ƶ��༭��
            if (e.KeyChar == (char)13)
            {
                this.neuSpread1_Sheet1.ActiveColumnIndex = this.GetColumnIndex(this.nowEditColumn);
                this.neuSpread1.Focus();
            }
        }

        private void neuSpread11_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.neuSpread1.ActiveSheet.RowCount <= 0)
            {
                return;
            }
            int activeRowindex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            string sHwh = this.neuSpread1.ActiveSheet.Cells[activeRowindex, this.GetColumnIndex("��λ���")].Text;
            if (sHwh.Length >= this.placeNoLength)
            {
                MessageBox.Show("��λ��ų��Ȳ��ܴ���" + this.placeNoLength.ToString() + "λ\n ��鿴���ղ�����Ŀ�λ���");
                this.neuSpread1.ActiveSheet.SetActiveCell(e.Row, e.Column);
                return;
            }
        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.isShowDrugDetail)
            {
                //				if (this.neuSpread2_Sheet1.Rows.Count <= 0)
                //					return;
                //				FS.HISFC.Models.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.ActiveRowIndex, this.GetColumnIndex("��Ʒ����")].Text);
                //				if (item != null)
                //				{
                //					this.PopDetail(item);
                //				}
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh(false);
        }

        private void chbState_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbState.Enabled = this.chbState.Checked;
        }

        private void chbValidDate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void neuSpread2_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(neuSpread1_Sheet1, xmlFilePath);
        }

        private void txtDetail_Click(object sender, EventArgs e)
        {
            if (this.neuPanel2.Visible)
                this.lnkShowDetail.Text = "��ʾ";
            else
                this.lnkShowDetail.Text = "����";

            this.neuPanel2.Visible = !this.neuPanel2.Visible;
        }

        public void FilterShow()
        {
            this.panelFilter.Visible = !this.panelFilter.Visible;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus && keyData == Keys.Enter)
            {
                int i = this.neuSpread1_Sheet1.ActiveColumnIndex;
                if (this.hsJumpColumn.Contains(i))
                {
                    while (i < this.neuSpread1_Sheet1.Columns.Count - 1)
                    {
                        i++;
                        if (this.hsJumpColumn.Contains(i))
                        {
                            this.neuSpread1_Sheet1.ActiveColumnIndex = i;
                            return true;
                        }
                    }
                    this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
                    this.Focus();
                }
                this.neuSpread1_Sheet1.ActiveColumnIndex++;
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion
    }
}
