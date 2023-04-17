using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using System.Collections;

using FS.HISFC.Models.SIInterface;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class ucCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ��������ö��

        public enum CompareTypes
        {
            /// <summary>
            /// ��ҩ
            /// </summary>
            P = 0,
            /// <summary>
            /// �в�ҩ
            /// </summary>
            C = 1,
            /// <summary>
            /// �г�ҩ
            /// </summary>
            Z = 2,
            /// <summary>
            /// ȫ��ҩƷ
            /// </summary>
            Drug = 3,
            /// <summary>
            /// ��ҩƷ
            /// </summary>
            Undrug = 4,
        };

        #endregion

        #region ����

        /// <summary>
        /// ҩƷ�б�
        /// </summary>
        private ArrayList alDrug = new ArrayList();

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        private NeuObject pactCode = new NeuObject();

        /// <summary>
        /// Ĭ�Ϻ�ͬ��λ
        /// </summary>
        private string defaulPactCode = string.Empty;
        
        /// <summary>
        /// �Ƿ����ҩƷ
        /// </summary>
        private bool isLoadDrug = false;

        /// <summary>
        /// ��ѯ��
        /// </summary>
        private string code = "PY"; 

        /// <summary>
        /// ѭ��
        /// </summary>
        private int circle = 0;

        /// <summary>
        /// HIS��Ŀdt
        /// </summary>
        DataTable dtHisItem = new DataTable();

        /// <summary>
        /// HIS��Ŀdv
        /// </summary>
        DataView dvHisItem = new DataView();
        
        /// <summary>
        /// ������Ŀdt
        /// </summary>
        DataTable dtCenterItem = new DataTable();

        /// <summary>
        /// ������Ŀdv
        /// </summary>
        DataView dvCenterItem = new DataView();

        /// <summary>
        /// ������Ŀdt
        /// </summary>
        DataTable dtCompareItem = new DataTable();

        /// <summary>
        /// ������Ŀdv
        /// </summary>
        DataView dvCompareItem = new DataView();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private CompareTypes compareType;

        /// <summary>
        /// ��Ŀ�ȼ�������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemGradeHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        [Category("����"), Description("������Ŀ���� P:��ҩ��C:�в�ҩ��Z:�г�ҩ��ALL:ȫ��ҩƷ��Undrug:��ҩƷ")]
        public CompareTypes CompareType
        {
            get
            {
                return compareType;
            }
            set
            {
                compareType = value;
            }
        }

        /// <summary>
        /// Ĭ�϶��պ�ͬ��λ
        /// </summary>
        [Category("����"), Description("Ĭ�϶��յĺ�ͬ��λ")]
        public string DefaulPactCode
        {
            get
            {
                return defaulPactCode;
            }
            set
            {
                defaulPactCode = value;
            }
        }

        /// <summary>
        /// ��ͬ��λ��Ϣ
        /// </summary>
        public NeuObject PactCode
        {
            set
            {
                pactCode = value;
            }
            get
            {
                return pactCode;
            }
        }

        #endregion

        #region ������

        /// <summary>
        /// ҽ������ʵ��
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ConnectSI myConnectSI = null;

        /// <summary>
        /// �ӿڹ�����
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// ����������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();
        
        #endregion

        public ucCompare()
        {
            InitializeComponent();
        }

        private void ucCompare_Load(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ����Ժ�^^");
            Application.DoEvents();
            this.Init();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #region ����

        /// <summary>
        /// ��ʼ����ʾ����
        /// </summary>
        public void Init()
        {
            this.InitParaments();
            this.InitColumn();
            this.InitColumnHIS();
            this.InitColumnCenter();
            this.InitColumnCompare();
            this.InitData();
            this.InitPactinfo();
            this.InitHashTable();
        }

        #region �˵���

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);
            this.toolBarService.AddToolButton("ȡ��", "ȡ��", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("���", "���", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// �˵����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    {
                        this.Compare();
                        break;
                    }
                case "ȡ��":
                    {
                        this.Delete();
                        break;
                    }
                case "���":
                    {
                        this.Clear();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitParaments()
        {
            isLoadDrug = !(CompareType.ToString() == CompareTypes.Undrug.ToString());
        }

        /// <summary>
        /// ������ʾ����Ϣ;
        /// </summary>
        private void InitColumn()
        {
            Type str = typeof(System.String);
            Type dec = typeof(System.Decimal);
            Type date = typeof(System.DateTime);

            if (this.isLoadDrug)
            {
                //��ʼ��������Ŀ:
                DataColumn[] colHisItem = new DataColumn[]{
                    new DataColumn("ҩƷ����", str),
                    new DataColumn("ҩƷ����", str),
                    new DataColumn("ƴ����", str),
                    new DataColumn("�����", str),
                    new DataColumn("�Զ�����", str),
                    new DataColumn("ҩ��ֱ���",str),
                    new DataColumn("���", str),
                    new DataColumn("ͨ����", str),
                    new DataColumn("ͨ����ƴ��", str),
                    new DataColumn("ͨ�������", str),
                    new DataColumn("���ʱ���", str),
                    new DataColumn("���ұ���", str),
                    new DataColumn("�۸�", str),
                    new DataColumn("���ͱ���", str)};

                dtHisItem.Columns.AddRange(colHisItem);
                DataColumn[] keyHis = new DataColumn[1];
                keyHis[0] = dtHisItem.Columns[0];
                dtHisItem.CaseSensitive = true;
                dtHisItem.PrimaryKey = keyHis;
                dvHisItem = new DataView(dtHisItem);
                dvHisItem.Sort = "ҩƷ���� ASC";
                fpHisItem_Sheet1.DataSource = dvHisItem;

                DataColumn[] colCenterItem = new DataColumn[]{
                    new DataColumn("���ı���", str),
                    new DataColumn("������Ŀ����", str),
                    new DataColumn("������ĿӢ����", str),
                    new DataColumn("���", str),
                    new DataColumn("����", str),
                    new DataColumn("����ƴ����", str),
                    new DataColumn("���÷���", str),
                    new DataColumn("Ŀ¼����", str),
                    new DataColumn("Ŀ¼�ȼ�", str),
                    new DataColumn("�Ը�����", dec),
                    new DataColumn("��׼�۸�", dec),
                    new DataColumn("����ʹ��˵��", str),
                    new DataColumn("��Ŀ���", str)};

                dtCenterItem.Columns.AddRange(colCenterItem);
                DataColumn[] keyCenter = new DataColumn[1];
                keyCenter[0] = dtCenterItem.Columns[0];
                dtCenterItem.CaseSensitive = true;
                dtCenterItem.PrimaryKey = keyCenter;
                dvCenterItem = new DataView(dtCenterItem);
                dvCenterItem.Sort = "���ı��� ASC";
                fpCenterItem_Sheet1.DataSource = dvCenterItem;
            }
            else 
            {
                //��ʼ��������Ŀ:
                DataColumn[] colHisItem = new DataColumn[]{
                    new DataColumn("��ҩƷ����", str),
                    new DataColumn("��ҩƷ����", str),
                    new DataColumn("ƴ����", str),
                    new DataColumn("�����", str),
                    new DataColumn("�Զ�����", str),
                    new DataColumn("���", str),
                    new DataColumn("���ʱ���", str),
                    new DataColumn("���ұ���", str),
                    new DataColumn("�۸�", str),
                    new DataColumn("��λ", str)};

                dtHisItem.Columns.AddRange(colHisItem);
                DataColumn[] keyHis = new DataColumn[1];
                keyHis[0] = dtHisItem.Columns[0];
                dtHisItem.PrimaryKey = keyHis;
                dvHisItem = new DataView(dtHisItem);
                dvHisItem.Sort = "��ҩƷ���� ASC";
                fpHisItem_Sheet1.DataSource = dvHisItem;

                DataColumn[] colCenterItem = new DataColumn[]{
                    new DataColumn("���ı���", str),
                    new DataColumn("������Ŀ����", str),
                    new DataColumn("������ĿӢ����", str),
                    new DataColumn("���", str),
                    new DataColumn("����", str),
                    new DataColumn("����ƴ����", str),
                    new DataColumn("���÷���", str),
                    new DataColumn("Ŀ¼����", str),
                    new DataColumn("Ŀ¼�ȼ�", str),
                    new DataColumn("�Ը�����", dec),
                    new DataColumn("��׼�۸�", dec),
                    new DataColumn("����ʹ��˵��", str),
                    new DataColumn("��Ŀ���", str)};

                dtCenterItem.Columns.AddRange(colCenterItem);
                DataColumn[] keyCenter = new DataColumn[1];
                keyCenter[0] = dtCenterItem.Columns[0];
                dtCenterItem.CaseSensitive=true;
                dtCenterItem.PrimaryKey = keyCenter;
                dvCenterItem = new DataView(dtCenterItem);
                dvCenterItem.Sort = "���ı��� ASC";
                fpCenterItem_Sheet1.DataSource = dvCenterItem;
            }

            DataColumn[] colCompareItem = new DataColumn[]{
                new DataColumn("ҽԺ�Զ�����", str),
                new DataColumn("������Ŀ����", str),
                new DataColumn("���ı���", str),
                new DataColumn("��Ŀ���", str),
                new DataColumn("ҽ���շ���Ŀ��������", str),
                new DataColumn("������Ŀ����", str),
                new DataColumn("������Ŀ����", str),
                new DataColumn("ҩ��ֱ���",str),
                new DataColumn("ҽ���շ���ĿӢ������", str),
                new DataColumn("ҽ������", str),
                new DataColumn("ҽ�����",str),
                new DataColumn("ҽ��ƴ������", str),
                new DataColumn("ҽ�����÷������", str),
                new DataColumn("ҽ��Ŀ¼����", str),
                new DataColumn("ҽ��Ŀ¼�ȼ�", str),
                new DataColumn("�Ը�����", dec),
                new DataColumn("��׼�۸�", dec),
                new DataColumn("����ʹ��˵��", str),
                new DataColumn("ҽԺƴ��", str),
                new DataColumn("ҽԺ�����", str),
                new DataColumn("ҽԺ���", str),
                new DataColumn("ҽԺ�����۸�", dec),
                new DataColumn("ҽԺ����", str),
                new DataColumn("����Ա", str),
                new DataColumn("����ʱ��", date)};

            dtCompareItem.Columns.AddRange(colCompareItem);
            DataColumn[] keyCompare = new DataColumn[1];
            keyCompare[0] = dtCompareItem.Columns[1];
            dtCompareItem.CaseSensitive=true;
            dtCompareItem.PrimaryKey = keyCompare;
            dvCompareItem = new DataView(dtCompareItem);
            dvCompareItem.Sort = "����ʱ�� DESC";
            fpCompareItem_Sheet1.DataSource = dvCompareItem;
        }

        /// <summary>
        /// HIS������Ŀ������
        /// </summary>
        private void InitColumnHIS()
        {
            int width = 20;

            if (this.isLoadDrug)
            {
                this.fpHisItem_Sheet1.Columns[0].Visible = false;
                this.fpHisItem_Sheet1.Columns[1].Width = width * 8;
                this.fpHisItem_Sheet1.Columns[2].Visible = false;
                this.fpHisItem_Sheet1.Columns[3].Visible = false;
                this.fpHisItem_Sheet1.Columns[4].Visible = true;
                this.fpHisItem_Sheet1.Columns[5].Width = width * 8;
                this.fpHisItem_Sheet1.Columns[6].Width = width * 8;
                this.fpHisItem_Sheet1.Columns[7].Visible = false;
                this.fpHisItem_Sheet1.Columns[8].Visible = false;
                this.fpHisItem_Sheet1.Columns[9].Visible = false;
                this.fpHisItem_Sheet1.Columns[10].Visible = false;
                this.fpHisItem_Sheet1.Columns[11].Width = width * 3;
                this.fpHisItem_Sheet1.Columns[12].Width = width * 4;
            }
            else 
            {
                this.fpHisItem_Sheet1.Columns[0].Visible = false;
                this.fpHisItem_Sheet1.Columns[1].Width = width * 8;
                this.fpHisItem_Sheet1.Columns[2].Visible = false;
                this.fpHisItem_Sheet1.Columns[3].Visible = false;
                this.fpHisItem_Sheet1.Columns[4].Visible = true;
                this.fpHisItem_Sheet1.Columns[5].Width = width * 8;
                this.fpHisItem_Sheet1.Columns[6].Visible = false;
                this.fpHisItem_Sheet1.Columns[7].Visible = false;
                this.fpHisItem_Sheet1.Columns[8].Width = width * 3;
                this.fpHisItem_Sheet1.Columns[9].Width = width * 4;
            }
        }

        /// <summary>
        /// ��ʼ��������������Ϣ
        /// </summary>
        private void InitColumnCenter()
        {
            int width = 20;
            this.fpCenterItem_Sheet1.Columns[0].Visible = true;
            this.fpCenterItem_Sheet1.Columns[1].Width = width * 8;
            this.fpCenterItem_Sheet1.Columns[2].Width = width * 8;
            this.fpCenterItem_Sheet1.Columns[3].Width = width * 8;
            this.fpCenterItem_Sheet1.Columns[4].Width = width * 3;
            this.fpCenterItem_Sheet1.Columns[5].Visible = true;
            this.fpCenterItem_Sheet1.Columns[6].Visible = true;
            this.fpCenterItem_Sheet1.Columns[7].Visible = true;
            this.fpCenterItem_Sheet1.Columns[8].Width = width * 3;
            this.fpCenterItem_Sheet1.Columns[9].Width = width * 4;
            this.fpCenterItem_Sheet1.Columns[10].Width = width * 3;
            this.fpCenterItem_Sheet1.Columns[11].Width = width * 8;
        }

        /// <summary>
        /// ��ʼ��������������Ϣ
        /// </summary>
        private void InitColumnCompare()
        {
            int width = 20;

            FarPoint.Win.Spread.CellType.DateTimeCellType dtType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dtType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;

            fpCompareItem_Sheet1.Columns[0].Visible = true;
            fpCompareItem_Sheet1.Columns[1].Visible = true;
            fpCompareItem_Sheet1.Columns[2].Visible = true;
            fpCompareItem_Sheet1.Columns[3].Width = width * 8;
            fpCompareItem_Sheet1.Columns[4].Width = width * 8;
            fpCompareItem_Sheet1.Columns[5].Width = width * 8;
            fpCompareItem_Sheet1.Columns[6].Visible = true;
            fpCompareItem_Sheet1.Columns[7].Visible = true;
            fpCompareItem_Sheet1.Columns[7].Width = width * 4;
            fpCompareItem_Sheet1.Columns[8].Visible = false;
            fpCompareItem_Sheet1.Columns[9].Visible = false;
            fpCompareItem_Sheet1.Columns[10].Visible = false;
            fpCompareItem_Sheet1.Columns[11].Width = width * 4;
            fpCompareItem_Sheet1.Columns[12].Visible = true;
            fpCompareItem_Sheet1.Columns[13].Width = width * 4;
            fpCompareItem_Sheet1.Columns[14].Width = width * 4;
            fpCompareItem_Sheet1.Columns[15].Width = width * 4;
            fpCompareItem_Sheet1.Columns[16].Width = width * 6;
            fpCompareItem_Sheet1.Columns[17].Visible = false;
            fpCompareItem_Sheet1.Columns[18].Visible = false;
            fpCompareItem_Sheet1.Columns[19].Visible = false;
            fpCompareItem_Sheet1.Columns[20].Width = width * 8;
            fpCompareItem_Sheet1.Columns[21].Width = width * 4;
            fpCompareItem_Sheet1.Columns[22].Width = width * 4;
            fpCompareItem_Sheet1.Columns[23].Width = width * 4;
            fpCompareItem_Sheet1.Columns[24].Width = width * 6;
            fpCompareItem_Sheet1.Columns[24].CellType = dtType;


        }
        
        /// <summary>
        /// ��ʼ����ʾ����
        /// </summary>
        public void InitData()
        {
            ArrayList itemGrade = consMgr.GetAllList("ITEMGRADE");
            if (itemGrade != null && itemGrade.Count > 0)
            {
                itemGradeHelper.ArrayObject = itemGrade;
                cmbItemGrade.AddItems(itemGrade);
                this.cmbItemGrade.SelectedIndex = 0;
            }

            ArrayList alHisItem = new ArrayList();
            ArrayList alCenterItem = new ArrayList();
            ArrayList alCompareItem = new ArrayList();

            if (!string.IsNullOrEmpty(this.DefaulPactCode))
            {
                pactCode = new NeuObject();
                pactCode.ID = this.DefaulPactCode;
            }

            if (isLoadDrug)
            {
                #region ����ҩƷ

                alHisItem = this.myInterface.GetNoCompareDrugItem(pactCode.ID, compareType.ToString());
                if (alHisItem != null)
                {
                    foreach (FS.HISFC.Models.Pharmacy.Item obj in alHisItem)
                    {
                        DataRow row = dtHisItem.NewRow();
                        row["ҩƷ����"] = obj.ID;
                        row["ҩƷ����"] = obj.Name;
                        row["ƴ����"] = obj.SpellCode;
                        row["�����"] = obj.WBCode;
                        row["�Զ�����"] = obj.UserCode;
                        row["ҩ��ֱ���"] = obj.NameCollection.FormalSpell.UserCode;
                        row["���"] = obj.Specs;
                        row["���ʱ���"] = obj.NationCode;
                        row["���ұ���"] = obj.GBCode;
                        row["�۸�"] = obj.PriceCollection.RetailPrice;
                        row["���ͱ���"] = obj.DosageForm.ID;

                        dtHisItem.Rows.Add(row);
                    }
                }

                if (compareType.ToString() == "Drug")
                {
                    alCenterItem = this.myInterface.GetCenterItem(pactCode.ID);
                }
                else
                {
                    alCenterItem = this.myInterface.GetCenterItem(pactCode.ID, compareType.ToString());
                }

                if (alCenterItem != null)
                {
                    foreach (FS.HISFC.Models.SIInterface.Item obj in alCenterItem)
                    {
                        DataRow row = dtCenterItem.NewRow();
                        row["���ı���"] = obj.ID;
                        row["������Ŀ����"] = obj.Name;
                        row["������ĿӢ����"] = obj.EnglishName;
                        row["���"] = obj.Specs;
                        row["����"] = obj.DoseCode;
                        row["����ƴ����"] = obj.SpellCode;
                        row["���÷���"] = obj.FeeCode;
                        row["Ŀ¼����"] = obj.ItemType;
                        row["Ŀ¼�ȼ�"] = obj.ItemGrade;
                        row["�Ը�����"] = obj.Rate;
                        row["��׼�۸�"] = obj.Price;
                        row["����ʹ��˵��"] = obj.Memo;
                        row["��Ŀ���"] = obj.SysClass;
                        dtCenterItem.Rows.Add(row);
                    }
                }

                alCompareItem = this.myInterface.GetCompareItem(pactCode.ID, compareType.ToString());

                if (alCompareItem != null)
                {
                    foreach (FS.HISFC.Models.SIInterface.Compare obj in alCompareItem)
                    {
                        DataRow row = dtCompareItem.NewRow();

                        row["������Ŀ����"] = obj.HisCode;
                        row["���ı���"] = obj.CenterItem.ID;
                        row["��Ŀ���"] = obj.CenterItem.SysClass;
                        row["ҽ���շ���Ŀ��������"] = obj.CenterItem.Name;
                        row["ҽ���շ���ĿӢ������"] = obj.CenterItem.EnglishName;
                        row["������Ŀ����"] = obj.Name;
                        row["������Ŀ����"] = obj.RegularName;
                        row["ҩ��ֱ���"] = obj.FdaDrguCode;
                        row["ҽ������"] = obj.CenterItem.DoseCode;
                        row["ҽ��ƴ������"] = obj.CenterItem.SpellCode;
                        row["ҽ�����÷������"] = obj.CenterItem.FeeCode;
                        row["ҽ��Ŀ¼����"] = obj.CenterItem.ItemType;
                        row["ҽ��Ŀ¼�ȼ�"] = itemGradeHelper.GetObjectFromID(obj.CenterItem.ItemGrade).Name;
                        row["�Ը�����"] = obj.CenterItem.Rate;
                        row["��׼�۸�"] = obj.CenterItem.Price;
                        row["����ʹ��˵��"] = obj.CenterItem.Memo;
                        row["ҽԺƴ��"] = obj.SpellCode.SpellCode;
                        row["ҽԺ�����"] = obj.SpellCode.WBCode;
                        row["ҽԺ�Զ�����"] = obj.SpellCode.UserCode;
                        row["ҽԺ���"] = obj.Specs;
                        row["ҽԺ�����۸�"] = obj.Price;
                        row["ҽԺ����"] = obj.DoseCode;
                        row["����Ա"] = obj.CenterItem.OperCode;
                        row["����ʱ��"] = obj.CenterItem.OperDate;

                        dtCompareItem.Rows.Add(row);
                    }
                }
                #endregion
            }
            else
            {
                #region ���ط�ҩƷ
                alHisItem = myInterface.GetNoCompareUndrugItem(pactCode.ID);
                if (alHisItem != null)
                {
                    foreach (FS.HISFC.Models.Fee.Item.Undrug obj in alHisItem)
                    {
                        DataRow row = dtHisItem.NewRow();
                        row["��ҩƷ����"] = obj.ID;
                        row["��ҩƷ����"] = obj.Name;
                        row["ƴ����"] = obj.SpellCode;
                        row["�����"] = obj.WBCode;
                        row["�Զ�����"] = obj.UserCode;
                        row["���"] = obj.Specs;
                        row["���ʱ���"] = obj.NationCode;
                        row["���ұ���"] = obj.GBCode;
                        row["�۸�"] = obj.Price;
                        row["��λ"] = obj.PriceUnit;
                        dtHisItem.Rows.Add(row);
                    }
                }

                alCenterItem = this.myInterface.GetCenterItem(pactCode.ID, compareType.ToString());
                if (alCenterItem != null)
                {
                    foreach (FS.HISFC.Models.SIInterface.Item obj in alCenterItem)
                    {
                        DataRow row = dtCenterItem.NewRow();
                        row["���ı���"] = obj.ID;
                        row["������Ŀ����"] = obj.Name;
                        row["������ĿӢ����"] = obj.EnglishName;
                        row["���"] = obj.Specs;
                        row["����"] = obj.DoseCode;
                        row["����ƴ����"] = obj.SpellCode;
                        row["���÷���"] = obj.FeeCode;
                        row["Ŀ¼����"] = obj.ItemType;
                        row["Ŀ¼�ȼ�"] = obj.ItemGrade;
                        row["�Ը�����"] = obj.Rate;
                        row["��׼�۸�"] = obj.Price;
                        row["����ʹ��˵��"] = obj.Memo;
                        row["��Ŀ���"] = obj.SysClass;
                        dtCenterItem.Rows.Add(row);
                    }
                }

                alCompareItem = this.myInterface.GetCompareItem(pactCode.ID, compareType.ToString());
                if (alCompareItem != null)
                {
                    foreach (FS.HISFC.Models.SIInterface.Compare obj in alCompareItem)
                    {
                        DataRow row = dtCompareItem.NewRow();

                        row["������Ŀ����"] = obj.HisCode;
                        row["���ı���"] = obj.CenterItem.ID;
                        row["��Ŀ���"] = obj.CenterItem.SysClass;
                        row["ҽ���շ���Ŀ��������"] = obj.CenterItem.Name;
                        row["ҽ���շ���ĿӢ������"] = obj.CenterItem.EnglishName;
                        row["������Ŀ����"] = obj.Name;
                        row["������Ŀ����"] = obj.RegularName;
                        row["ҽ������"] = obj.CenterItem.DoseCode;
                        row["ҽ��ƴ������"] = obj.CenterItem.SpellCode;
                        row["ҽ�����÷������"] = obj.CenterItem.FeeCode;
                        row["ҽ��Ŀ¼����"] = obj.CenterItem.ItemType;
                        row["ҽ��Ŀ¼�ȼ�"] = obj.CenterItem.ItemGrade;
                        row["�Ը�����"] = obj.CenterItem.Rate;
                        row["��׼�۸�"] = obj.CenterItem.Price;
                        row["����ʹ��˵��"] = obj.CenterItem.Memo;
                        row["ҽԺƴ��"] = obj.SpellCode.SpellCode;
                        row["ҽԺ�����"] = obj.SpellCode.WBCode;
                        row["ҽԺ�Զ�����"] = obj.SpellCode.UserCode;
                        row["ҽԺ���"] = obj.Specs;
                        row["ҽԺ�����۸�"] = obj.Price;
                        row["ҽԺ����"] = obj.DoseCode;
                        row["����Ա"] = obj.CenterItem.OperCode;
                        row["����ʱ��"] = obj.CenterItem.OperDate;

                        dtCompareItem.Rows.Add(row);
                    }
                }
                #endregion
            }

            this.dtCenterItem.AcceptChanges();
            this.dtCompareItem.AcceptChanges();
            this.dtHisItem.AcceptChanges();
        }

        /// <summary>
        /// ��ʼ����ͬ��λ
        /// </summary>
        /// <returns></returns>
        private int InitPactinfo()
        {
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

            ArrayList pactList = pactManager.QueryPactUnitAll();
            if (pactList == null)
            {
                MessageBox.Show("��ʼ����ͬ��λ����!" + pactManager.Err);
                return -1;
            }
            this.cmbPact.AddItems(pactList);
            if (!string.IsNullOrEmpty(this.DefaulPactCode))
            {
                foreach (FS.HISFC.Models.Base.PactInfo pactInfo in pactList)
                {
                    if (pactInfo.ID == this.DefaulPactCode)
                    {
                        this.cmbPact.Tag = pactInfo.ID;
                        this.cmbPact.Text = pactInfo.Name;
                        this.cmbPact.Enabled = false;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ��tabҳ��fp��Ӧ��ϵ
        /// </summary>
        private void InitHashTable()
        {
            foreach (TabPage t in this.tabCompare.TabPages)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is FarPoint.Win.Spread.FpSpread)
                    {
                        this.hashTableFp.Add(t, c);
                    }
                }
            }
        }

        #region δ����

        /// <summary>
        /// ����ҽ��������
        /// </summary>
        /// <returns></returns>
        public int ConnectSIServer()
        {
            try
            {
                myConnectSI = new FS.HISFC.BizLogic.Fee.ConnectSI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ҽ��������ʧ��!,��������������" + ex.Message);
                return -1;
            }
            return 0;
        }

        #endregion

        /// <summary>
        /// ���ҩƷ������Ϣ
        /// </summary>
        public void GetHisDrugItem()
        {
            alDrug = myInterface.GetNoCompareDrugItem(pactCode.ID, compareType.ToString());
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="input"></param>
        private void FilterItem(string flag, string input)
        {
            string filterString = "";
            input = input.ToUpper();
            switch (flag)
            {
                case "HIS":
                    //switch (code)
                    //{
                    //    case "PY":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "ƴ����" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "ƴ����" + " like '%" + input + "%'";
                    //        }
                    //        break;
                    //    case "WB":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "�����" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "�����" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "US":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "�Զ�����" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "�Զ�����" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "ZW":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "ҩƷ����" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "ҩƷ����" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "TYPY":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "ͨ����ƴ��" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "ͨ����ƴ��" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "TYWB":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "ͨ�������" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "ͨ�������" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //}
                    if (CompareType == CompareTypes.Undrug)
                    {
                        filterString = "ƴ����" + " like '%" + input + "%'" + "or" + " �����" + " like '%" + input + "%'" + "or" + " �Զ�����" + " like '" + input + "%'" + "or" + " ��ҩƷ����" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "ƴ����" + " like '%" + input + "%'" + "or" + " �����" + " like '%" + input + "%'" + "or" + " �Զ�����" + " like '" + input + "%'" + "or" + " ҩƷ����" + " like '" + input + "%'";
                    }                    
                    this.dvHisItem.RowFilter = filterString;
                    InitColumnHIS();
                    break;
                case "CENTER":
                    if (!this.checkBox1.Checked)
                    {
                        filterString = "����ƴ����" + " like '" + input + "%'" + " or " + " ���ı���" + " like '" + input + "%'" + " or " + " ���ı���" + " like '" + input + "%'" + "or" + " ������Ŀ����" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "����ƴ����" + " like '%" + input + "%'" + " or " + " ���ı���" + " like '%" + input + "%'" + " or " + " ���ı���" + " like '%" + input + "%'" + "or" + " ������Ŀ����" + " like '" + input + "%'";
                    }
                    this.dvCenterItem.RowFilter = filterString;
                    InitColumnCenter();
                    break;
                case "COMPARE":
                    if (!this.checkBox1.Checked)
                    {
                        filterString = "ҽԺƴ��" + " like '" + input + "%'" + " or " + "ҽԺ�Զ�����" + " like '" + input + "%'" + " or " + "������Ŀ����" + " like '%" + input + "%'";
                    }
                    else
                    {
                        filterString = "ҽԺƴ��" + " like '%" + input + "%'" + " or " + "ҽԺ�Զ�����" + " like '%" + input + "%'" + " or " + "������Ŀ����" + " like '%" + input + "%'";
                    }
                    this.dvCompareItem.RowFilter = filterString;
                    break;
            }
        }

        /// <summary>
        /// ��ʾѡ��ı�����Ϣ
        /// </summary>
        /// <param name="row"></param>
        private void SetHisItemInfo(int row)
        {
            string hisCode = "";
            if (isLoadDrug)
            {
                hisCode = this.fpHisItem_Sheet1.Cells[row, 0].Text.Trim();
                this.tbHisName.Text = this.fpHisItem_Sheet1.Cells[row, 1].Text;
                this.tbHisPrice.Text = this.fpHisItem_Sheet1.Cells[row, 11].Text;

                FS.HISFC.Models.Pharmacy.Item obj = this.GetSelectHisItem(hisCode);

                if (obj == null)
                {
                    MessageBox.Show("δ�ҵ�ѡ������ҩƷ!");
                }
                else
                {
                    this.tbHisSpell.Tag = obj;
                }

            }
            else
            {
                hisCode = this.fpHisItem_Sheet1.Cells[row, 0].Text.Trim();
                this.tbHisName.Text = this.fpHisItem_Sheet1.Cells[row, 1].Text;
                this.tbHisPrice.Text = this.fpHisItem_Sheet1.Cells[row, 8].Text;

                FS.HISFC.Models.Fee.Item.Undrug obj = this.GetSelectHisUndrugItem(hisCode);

                if (obj == null)
                {
                    MessageBox.Show("δ�ҵ�ѡ�����ط�ҩƷ!");
                }
                else
                {
                    this.tbHisSpell.Tag = obj;
                }

            }

            tabCompare.SelectedTab = this.tbCenterItem;
            this.tbCenterSpell.Focus();
        }
        
        /// <summary>
        /// ��ʾѡ���������Ϣ
        /// </summary>
        /// <param name="row"></param>
        private void SetCenterItemInfo(int row)
        {
            string centerCode = "";

            centerCode = this.fpCenterItem_Sheet1.Cells[row, 0].Text.Trim();

            Item obj = this.GetSelectCenterItem(centerCode);

            if (obj == null)
            {
                MessageBox.Show("δ�ҵ�����ҩƷ");
            }
            else
            {
                tbCenterSpell.Tag = obj;
            }

            this.tbCenterName.Text = this.fpCenterItem_Sheet1.Cells[row, 1].Text;
            this.tbCenterPrice.Text = this.fpCenterItem_Sheet1.Cells[row, 10].Text;
            this.tabCompare.SelectedTab = this.tbCompare;
        }
        
        /// <summary>
        /// �����ѡ��HISҩƷ��Ϣ
        /// </summary>
        /// <param name="hisCode">ҽԺҩƷ����</param>
        /// <returns>ҩƷ��Ϣʵ��</returns>
        private FS.HISFC.Models.Pharmacy.Item GetSelectHisItem(string hisCode)
        {
            FS.HISFC.Models.Pharmacy.Item obj = new FS.HISFC.Models.Pharmacy.Item();

            DataRow row = this.dtHisItem.Rows.Find(hisCode);

            obj.ID = row["ҩƷ����"].ToString();
            obj.Name = row["ҩƷ����"].ToString();
            obj.SpellCode = row["ƴ����"].ToString();
            obj.WBCode = row["�����"].ToString();
            obj.UserCode = row["�Զ�����"].ToString();
            obj.Specs = row["���"].ToString();
            obj.NameCollection.RegularName = row["ͨ����"].ToString();
            obj.NameCollection.SpellCode = row["ͨ����ƴ��"].ToString();
            obj.NameCollection.WBCode = row["ͨ�������"].ToString();
            obj.NameCollection.InternationalCode = row["���ʱ���"].ToString();
            obj.GBCode = row["���ұ���"].ToString();
            obj.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(row["�۸�"].ToString());
            obj.DosageForm.ID = row["���ͱ���"].ToString();

            return obj;
        }
        
        /// <summary>
        /// ��ñ���His��ҩƷ��Ϣ
        /// </summary>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Item.Undrug GetSelectHisUndrugItem(string hisCode)
        {
            FS.HISFC.Models.Fee.Item.Undrug obj = new FS.HISFC.Models.Fee.Item.Undrug();

            DataRow row = this.dtHisItem.Rows.Find(hisCode);

            obj.ID = row["��ҩƷ����"].ToString();
            obj.Name = row["��ҩƷ����"].ToString();
            obj.SpellCode = row["ƴ����"].ToString();
            obj.WBCode = row["�����"].ToString();
            obj.UserCode = row["�Զ�����"].ToString();
            obj.Specs = row["���"].ToString();
            obj.NationCode = row["���ʱ���"].ToString();
            obj.GBCode = row["���ұ���"].ToString();
            obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["�۸�"].ToString());
            obj.PriceUnit = row["��λ"].ToString();

            return obj;
        }

        /// <summary>
        /// �����ѡ������Ŀ��Ϣ
        /// </summary>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        private FS.HISFC.Models.SIInterface.Item GetSelectCenterItem(string centerCode)
        {
            Item obj = new Item();

            DataRow row = this.dtCenterItem.Rows.Find(centerCode);
            if (isLoadDrug)
            {
                obj.ID = row["���ı���"].ToString();
                obj.Name = row["������Ŀ����"].ToString();
                obj.EnglishName = row["������ĿӢ����"].ToString();
            }
            else
            {
                obj.ID = row["���ı���"].ToString();
                obj.Name = row["������Ŀ����"].ToString();
                obj.EnglishName = row["������ĿӢ����"].ToString();
            }

            obj.Specs = row["���"].ToString();
            obj.DoseCode = row["����"].ToString();
            obj.SpellCode = row["����ƴ����"].ToString();
            obj.FeeCode = row["���÷���"].ToString();
            obj.ItemType = row["Ŀ¼����"].ToString();
            obj.ItemGrade = row["Ŀ¼�ȼ�"].ToString();
            obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Ը�����"].ToString());
            obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["��׼�۸�"].ToString());
            obj.Memo = row["����ʹ��˵��"].ToString();
            obj.SysClass = row["��Ŀ���"].ToString();

            return obj;
        }

        /// <summary>
        /// ���ղ���
        /// </summary>
        public void Compare()
        {
            Compare objCom = new Compare();

            if (isLoadDrug)
            {
                FS.HISFC.Models.Pharmacy.Item objHis = new FS.HISFC.Models.Pharmacy.Item();
                Item objCenter = new Item();

                if (this.tbHisSpell.Tag == null || this.tbHisSpell.Tag.ToString() == "")
                {
                    MessageBox.Show("û��ѡ�񱾵���Ŀ!");
                    return;
                }

                objHis = (FS.HISFC.Models.Pharmacy.Item)this.tbHisSpell.Tag;

                if (tbCenterSpell.Tag == null || tbCenterSpell.Tag.ToString() == "")
                {
                    MessageBox.Show("û��ѡ��������Ŀ");
                    return;
                }

                objCenter = (Item)this.tbCenterSpell.Tag;

                DataRow row = this.dtCompareItem.NewRow();

                row["������Ŀ����"] = objHis.ID;
                row["���ı���"] = objCenter.ID;
                row["��Ŀ���"] = objCenter.SysClass;
                row["ҽ���շ���Ŀ��������"] = objCenter.Name;
                row["ҽ���շ���ĿӢ������"] = objCenter.EnglishName;
                row["������Ŀ����"] = objHis.Name;
                row["������Ŀ����"] = objHis.NameCollection.RegularName;//.RegularName;
                row["ҩ��ֱ���"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(objHis.ID).NameCollection.FormalSpell.UserCode;
                row["ҽ������"] = objCenter.DoseCode;
                row["ҽ�����"] = objCenter.Specs;
                row["ҽ��ƴ������"] = objCenter.SpellCode;
                row["ҽ�����÷������"] = objCenter.FeeCode;
                row["ҽ��Ŀ¼����"] = objCenter.ItemType;
                row["ҽ��Ŀ¼�ȼ�"] = itemGradeHelper.GetObjectFromID(string.IsNullOrEmpty(this.cmbItemGrade.Tag.ToString()) ? "3" : this.cmbItemGrade.Tag.ToString()).Name;
                row["�Ը�����"] = objCenter.Rate;
                row["��׼�۸�"] = objCenter.Price;
                row["����ʹ��˵��"] = objCenter.Memo;
                row["ҽԺƴ��"] = objHis.SpellCode;
                row["ҽԺ�����"] = objHis.WBCode;// .SpellCode.WB_Code;
                row["ҽԺ�Զ�����"] = objHis.UserCode;//SpellCode.User_Code;
                row["ҽԺ���"] = objHis.Specs;
                row["ҽԺ�����۸�"] = objHis.PriceCollection.RetailPrice; //.RetailPrice;
                row["ҽԺ����"] = objHis.DosageForm.ID;
                row["����Ա"] = myInterface.Operator.ID;
                row["����ʱ��"] = System.DateTime.Now;
              
                dtCompareItem.Rows.Add(row);

                objCom.CenterItem.PactCode = pactCode.ID;
                objCom.HisCode = objHis.ID;
                objCom.CenterItem.ID = objCenter.ID;
                objCom.CenterItem.SysClass = objCenter.SysClass;
                objCom.CenterItem.Name = objCenter.Name;
                objCom.CenterItem.EnglishName = objCenter.EnglishName;
                objCom.Name = objHis.Name;
                objCom.RegularName = objHis.NameCollection.RegularName; //.RegularName;
                objCom.CenterItem.DoseCode = objCenter.DoseCode;
                objCom.CenterItem.Specs = objCenter.Specs;
                objCom.CenterItem.FeeCode = objCenter.FeeCode;
                objCom.CenterItem.ItemType = objCenter.ItemType;
                objCom.CenterItem.ItemGrade = string.IsNullOrEmpty(this.cmbItemGrade.Tag.ToString()) ? "3" : this.cmbItemGrade.Tag.ToString();//objCenter.ItemGrade;
                objCom.CenterItem.Rate = objCenter.Rate;
                objCom.CenterItem.Price = objCenter.Price;
                objCom.CenterItem.Memo = objCenter.Memo;
                objCom.SpellCode.SpellCode = objHis.SpellCode;
                objCom.SpellCode.WBCode = objHis.WBCode;//SpellCode.WB_Code;
                objCom.SpellCode.UserCode = objHis.UserCode;//SpellCode.User_Code;
                objCom.Specs = objHis.Specs;
                objCom.Price = objHis.PriceCollection.RetailPrice;//.RetailPrice;
                objCom.DoseCode = objHis.DosageForm.ID;
                objCom.CenterItem.OperCode = myInterface.Operator.ID;
                if (isLoadDrug)
                {
                    objCom.FdaDrguCode = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(objHis.ID).NameCollection.FormalSpell.UserCode;
                }

                DataRow rowFind = dtHisItem.Rows.Find(objHis.ID);
                dtHisItem.Rows.Remove(rowFind);
            }
            else
            {
                FS.HISFC.Models.Fee.Item.Undrug objHis = new FS.HISFC.Models.Fee.Item.Undrug();
                Item objCenter = new Item();

                if (this.tbHisSpell.Tag == null || this.tbHisSpell.Tag.ToString() == "")
                {
                    MessageBox.Show("û��ѡ�񱾵���Ŀ!");
                    return;
                }

                objHis = (FS.HISFC.Models.Fee.Item.Undrug)this.tbHisSpell.Tag;

                if (tbCenterSpell.Tag == null || tbCenterSpell.Tag.ToString() == "")
                {
                    MessageBox.Show("û��ѡ��������Ŀ");
                    return;
                }

                objCenter = (Item)this.tbCenterSpell.Tag;

                DataRow row = this.dtCompareItem.NewRow();

                row["������Ŀ����"] = objHis.ID;
                row["���ı���"] = objCenter.ID;
                row["��Ŀ���"] = objCenter.SysClass;
                row["ҽ���շ���Ŀ��������"] = objCenter.Name;
                row["ҽ���շ���ĿӢ������"] = objCenter.EnglishName;
                row["������Ŀ����"] = objHis.Name;
                row["������Ŀ����"] = "";
                row["ҽ������"] = objCenter.DoseCode;
                row["ҽ�����"] = objCenter.Specs;
                row["ҽ��ƴ������"] = objCenter.SpellCode;//SpellCode.Spell_Code;
                row["ҽ�����÷������"] = objCenter.FeeCode;
                row["ҽ��Ŀ¼����"] = objCenter.ItemType;
                row["ҽ��Ŀ¼�ȼ�"] = string.IsNullOrEmpty(this.cmbItemGrade.Tag.ToString()) ? "3" : this.cmbItemGrade.Tag.ToString();
                row["�Ը�����"] = objCenter.Rate;
                row["��׼�۸�"] = objCenter.Price;
                row["����ʹ��˵��"] = objCenter.Memo;
                row["ҽԺƴ��"] = objHis.SpellCode;
                row["ҽԺ�����"] = objHis.WBCode;
                row["ҽԺ�Զ�����"] = objHis.UserCode;
                row["ҽԺ���"] = objHis.Specs;
                row["ҽԺ�����۸�"] = objHis.Price;
                row["ҽԺ����"] = "";
                row["����Ա"] = myInterface.Operator.ID;
                row["����ʱ��"] = System.DateTime.Now;

                dtCompareItem.Rows.Add(row);

                objCom.CenterItem.PactCode = pactCode.ID;
                objCom.HisCode = objHis.ID;
                objCom.CenterItem.ID = objCenter.ID;
                objCom.CenterItem.SysClass = objCenter.SysClass;
                objCom.CenterItem.Name = objCenter.Name;
                objCom.CenterItem.EnglishName = objCenter.EnglishName;
                objCom.Name = objHis.Name;
                objCom.RegularName = objHis.NameCollection.RegularName;
                objCom.CenterItem.DoseCode = objCenter.DoseCode;
                objCom.CenterItem.Specs = objCenter.Specs;
                objCom.CenterItem.FeeCode = objCenter.FeeCode;
                objCom.CenterItem.ItemType = objCenter.ItemType;
                objCom.CenterItem.ItemGrade = string.IsNullOrEmpty(this.cmbItemGrade.Tag.ToString()) ? "3" : this.cmbItemGrade.Tag.ToString();//objCenter.ItemGrade;
                objCom.CenterItem.Rate = objCenter.Rate;
                objCom.CenterItem.Price = objCenter.Price;
                objCom.CenterItem.Memo = objCenter.Memo;
                objCom.SpellCode.SpellCode = objHis.SpellCode;
                objCom.SpellCode.WBCode = objHis.WBCode;
                objCom.SpellCode.UserCode = objHis.UserCode;
                objCom.Specs = objHis.Specs;
                objCom.Price = objHis.Price;
                objCom.DoseCode = "";
                objCom.CenterItem.OperCode = myInterface.Operator.ID;
              

                DataRow rowFind = dtHisItem.Rows.Find(objHis.ID);
                dtHisItem.Rows.Remove(rowFind);
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int returnValue = 0;

            returnValue = myInterface.InsertCompareItem(objCom);

            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ��!" + myInterface.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            Clear();
            this.tbHisSpell.Focus();
        }
        
        /// <summary>
        /// ɾ���Ѷ�����Ϣ
        /// </summary>
        public void Delete()
        {
            int rowAct = this.fpCompareItem_Sheet1.ActiveRowIndex;
            if (this.fpCompareItem_Sheet1.RowCount <= 0)
                return;

            string hisCode = "";
            hisCode = this.fpCompareItem_Sheet1.Cells[rowAct, 1].Text;

            if (hisCode == "" || hisCode == null)
                return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int returnValue = 0;

            returnValue = myInterface.DeleteCompareItem(pactCode.ID, hisCode);

            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ������ʧ��!" + myInterface.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            DataRow row = this.dtCompareItem.Rows.Find(hisCode);

            DataRow rowHis = dtHisItem.NewRow();
            if (isLoadDrug)
            {
                rowHis["ҩƷ����"] = row["������Ŀ����"].ToString();
                rowHis["ҩƷ����"] = row["������Ŀ����"].ToString();
                rowHis["ͨ����"] = row["������Ŀ����"].ToString();
                rowHis["���ͱ���"] = row["ҽԺ����"].ToString();
            }
            else
            {
                rowHis["��ҩƷ����"] = row["������Ŀ����"].ToString();
                rowHis["��ҩƷ����"] = row["������Ŀ����"].ToString();
            }
            rowHis["ƴ����"] = row["ҽԺƴ��"].ToString();
            rowHis["�����"] = row["ҽԺ�����"].ToString();
            rowHis["�Զ�����"] = row["ҽԺ�Զ�����"].ToString();
            rowHis["���"] = row["ҽԺ���"].ToString();
            rowHis["�۸�"] = FS.FrameWork.Function.NConvert.ToDecimal(row["ҽԺ�����۸�"].ToString());

            dtCompareItem.Rows.Remove(row);
            dtHisItem.Rows.Add(rowHis);
        }
        
        /// <summary>
        /// �����Ϣ
        /// </summary>
        public void Clear()
        {
            //this.tbCenterSpell.Text = "";
            this.tbCenterSpell.Tag = "";
            this.tbCenterName.Text = "";
            this.tbCenterPrice.Text = "";
            this.cmbItemGrade.Tag = "";
            this.cmbItemGrade.SelectedIndex = 0;


            this.tbHisSpell.Tag = "";
            this.tbHisName.Text = "";
            this.tbHisPrice.Text = "";
        }

        /// <summary>
        /// ���溯��
        /// </summary>
        public void Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int returnValue = 0;

            ArrayList alAdd = GetAddCompareItem();

            if (alAdd != null)
            {
                foreach (Compare obj in alAdd)
                {
                    returnValue = myInterface.InsertCompareItem(obj);
                    if (returnValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���������Ϣ����!" + myInterface.Err);
                        return;
                    }
                }
            }

            ArrayList alDelete = GetDeleteCompareItem();

            if (alDelete != null)
            {
                foreach (Compare obj in alDelete)
                {
                    returnValue = myInterface.DeleteCompareItem(this.pactCode.ID, obj.HisCode);
                    if (returnValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ɾ��������Ϣ����!" + myInterface.Err);
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�!");
        }

        /// <summary>
        /// ������ǰ��Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            object obj = this.hashTableFp[this.tabCompare.SelectedTab];
            FarPoint.Win.Spread.FpSpread fp = obj as FarPoint.Win.Spread.FpSpread;

            SaveFileDialog op = new SaveFileDialog();
            op.Title = "��ѡ�񱣴��·��������";
            op.CheckFileExists = false;
            op.CheckPathExists = true;
            op.DefaultExt = "*.xls";
            op.Filter = "(*.xls)|*.xls";

            DialogResult result = op.ShowDialog();
            if (result == DialogResult.Cancel || op.FileName == string.Empty)
            {
                return -1;
            }

            string filePath = op.FileName;
            bool returnValue = fp.SaveExcel(filePath, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            return base.Export(sender, neuObject);
        }
        
        /// <summary>
        /// ���������Ŀ
        /// </summary>
        /// <returns></returns>
        private ArrayList GetAddCompareItem()
        {
            DataTable dt = this.dtCompareItem.GetChanges(DataRowState.Added);
            ArrayList al = new ArrayList();
            if (dt == null)
            {
                return null;
            }
            foreach (DataRow row in dt.Rows)
            {
                Compare obj = new Compare();

                obj.CenterItem.PactCode = pactCode.ID;
                obj.HisCode = row["������Ŀ����"].ToString();
                obj.CenterItem.ID = row["���ı���"].ToString();
                obj.CenterItem.SysClass = row["��Ŀ���"].ToString();
                obj.CenterItem.Name = row["ҽ���շ���Ŀ��������"].ToString();
                obj.CenterItem.EnglishName = row["ҽ���շ���ĿӢ������"].ToString();
                obj.Name = row["������Ŀ����"].ToString();
                obj.RegularName = row["������Ŀ����"].ToString();
                obj.CenterItem.DoseCode = row["ҽ������"].ToString();
                obj.CenterItem.Specs = row["ҽ�����"].ToString();
                obj.CenterItem.SpellCode = row["ҽ��ƴ������"].ToString();
                obj.CenterItem.FeeCode = row["ҽ�����÷������"].ToString();
                obj.CenterItem.ItemType = row["ҽ��Ŀ¼����"].ToString();
                obj.CenterItem.ItemGrade = row["ҽ��Ŀ¼�ȼ�"].ToString();
                obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Ը�����"].ToString());
                obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["��׼�۸�"].ToString());
                obj.CenterItem.Memo = row["����ʹ��˵��"].ToString();
                obj.SpellCode.SpellCode = row["ҽԺƴ��"].ToString();
                obj.SpellCode.WBCode = row["ҽԺ�����"].ToString();
                obj.SpellCode.UserCode = row["ҽԺ�Զ�����"].ToString();
                obj.Specs = row["ҽԺ���"].ToString();
                obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["ҽԺ�����۸�"].ToString());
                obj.DoseCode = row["ҽԺ����"].ToString();
                obj.CenterItem.OperCode = row["����Ա"].ToString();
                //obj.CenterItem.OperDate = Convert.ToDateTime(row["����ʱ��"].ToString());
                //��ׯ�޸� {87ED5A6B-F317-4579-9BC9-660182F49333}
                obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(row["����ʱ��"].ToString());

                al.Add(obj);
            }

            return al;
        }

        /// <summary>
        /// ɾ��������Ŀ
        /// </summary>
        /// <returns></returns>
        private ArrayList GetDeleteCompareItem()
        {
            DataTable dt = this.dtCompareItem.GetChanges(DataRowState.Deleted);

            ArrayList al = new ArrayList();
            if (dt == null)
            {
                return null;
            }
            foreach (DataRow row in dt.Rows)
            {
                Compare obj = new Compare();

                obj.CenterItem.PactCode = pactCode.ID;
                obj.HisCode = row["������Ŀ����"].ToString();
                obj.CenterItem.ID = row["���ı���"].ToString();
                obj.CenterItem.SysClass = row["��Ŀ���"].ToString();
                obj.CenterItem.Name = row["ҽ���շ���Ŀ��������"].ToString();
                obj.CenterItem.EnglishName = row["ҽ���շ���ĿӢ������"].ToString();
                obj.Name = row["������Ŀ����"].ToString();
                obj.RegularName = row["������Ŀ����"].ToString();
                obj.CenterItem.DoseCode = row["ҽ������"].ToString();
                obj.CenterItem.Specs = row["ҽ�����"].ToString();
                obj.CenterItem.SpellCode = row["ҽ��ƴ������"].ToString();
                obj.CenterItem.FeeCode = row["ҽ�����÷������"].ToString();
                obj.CenterItem.ItemType = row["ҽ��Ŀ¼����"].ToString();
                obj.CenterItem.ItemGrade = row["ҽ��Ŀ¼�ȼ�"].ToString();
                obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Ը�����"].ToString());
                obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["��׼�۸�"].ToString());
                obj.CenterItem.Memo = row["����ʹ��˵��"].ToString();
                obj.SpellCode.SpellCode = row["ҽԺƴ��"].ToString();
                obj.SpellCode.WBCode = row["ҽԺ�����"].ToString();
                obj.SpellCode.UserCode = row["ҽԺ�Զ�����"].ToString();
                obj.Specs = row["ҽԺ���"].ToString();
                obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["ҽԺ�����۸�"].ToString());
                obj.DoseCode = row["ҽԺ����"].ToString();
                obj.CenterItem.OperCode = row["����Ա"].ToString();
                obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(row["����ʱ��"].ToString());

                al.Add(obj);
            }

            this.dtCompareItem.AcceptChanges();

            return al;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// HIS������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHisSpell_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("HIS", this.tbHisSpell.Text);
        }

        /// <summary>
        /// ������Ŀ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCenterSpell_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("CENTER", this.tbCenterSpell.Text);
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                circle++;

                switch (circle)
                {
                    case 0:
                        code = "PY";
                        tbSpell.Text = "ƴ����";
                        break;
                    case 1:
                        code = "WB";
                        tbSpell.Text = "�����";
                        break;
                    case 2:
                        code = "US";
                        tbSpell.Text = "�Զ�����";
                        break;
                    case 3:
                        code = "ZW";
                        tbSpell.Text = "����";
                        break;
                    case 4:
                        code = "TYPY";
                        tbSpell.Text = "ͨ��ƴ��";
                        break;
                    case 5:
                        code = "TYWB";
                        tbSpell.Text = "ͨ�����";
                        break;
                }

                if (circle == 5)
                {
                    circle = -1;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// HIS��Ŀ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHisSpell_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpHisItem_Sheet1.RowCount <= 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                this.fpHisItem.SetViewportTopRow(0, this.fpHisItem_Sheet1.ActiveRowIndex - 5);
                this.fpHisItem_Sheet1.ActiveRowIndex--;
                this.fpHisItem_Sheet1.AddSelection(this.fpHisItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpHisItem.SetViewportTopRow(0, this.fpHisItem_Sheet1.ActiveRowIndex - 4);
                this.fpHisItem_Sheet1.ActiveRowIndex++;
                this.fpHisItem_Sheet1.AddSelection(this.fpHisItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpHisItem_Sheet1.RowCount >= 0)
                {
                    SetHisItemInfo(this.fpHisItem_Sheet1.ActiveRowIndex);
                }
            }
        }

        /// <summary>
        /// HIS��Ŀ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpHisItem_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpHisItem_Sheet1.RowCount >= 0)
                SetHisItemInfo(this.fpHisItem_Sheet1.ActiveRowIndex);
        }

        /// <summary>
        /// ������Ŀ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCenterSpell_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpCenterItem_Sheet1.RowCount <= 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                this.fpCenterItem.SetViewportTopRow(0, this.fpCenterItem_Sheet1.ActiveRowIndex - 5);
                this.fpCenterItem_Sheet1.ActiveRowIndex--;
                this.fpCenterItem_Sheet1.AddSelection(this.fpCenterItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpCenterItem.SetViewportTopRow(0, this.fpCenterItem_Sheet1.ActiveRowIndex - 4);
                this.fpCenterItem_Sheet1.ActiveRowIndex++;
                this.fpCenterItem_Sheet1.AddSelection(this.fpCenterItem_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpHisItem_Sheet1.RowCount >= 0)
                {
                    SetCenterItemInfo(this.fpCenterItem_Sheet1.ActiveRowIndex);
                }
            }
        }

        /// <summary>
        /// ������Ŀ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpCenterItem_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpHisItem_Sheet1.RowCount >= 0)
            {
                SetCenterItemInfo(this.fpCenterItem_Sheet1.ActiveRowIndex);
            }
        }

        /// <summary>
        /// HIS����������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHisSpell_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 0;
        }

        /// <summary>
        /// ������Ŀ����������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCenterSpell_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 1;
        }

        /// <summary>
        /// �Ѷ�����Ŀ��ѯ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCompareQuery_TextChanged(object sender, EventArgs e)
        {
            FilterItem("COMPARE", this.tbCompareQuery.Text);
        }
        
        /// <summary>
        /// ��ͬ��λѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pactCode.ID = this.cmbPact.Tag.ToString();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ����Ժ�^^");
            Application.DoEvents();
            this.dtHisItem.Clear();
            this.dtCenterItem.Clear();
            this.dtCompareItem.Clear();
            InitData();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion 
    }
}
