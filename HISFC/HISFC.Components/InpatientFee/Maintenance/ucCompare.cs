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
        #region 对照类型枚举

        public enum CompareTypes
        {
            /// <summary>
            /// 西药
            /// </summary>
            P = 0,
            /// <summary>
            /// 中草药
            /// </summary>
            C = 1,
            /// <summary>
            /// 中成药
            /// </summary>
            Z = 2,
            /// <summary>
            /// 全部药品
            /// </summary>
            Drug = 3,
            /// <summary>
            /// 非药品
            /// </summary>
            Undrug = 4,
        };

        #endregion

        #region 变量

        /// <summary>
        /// 药品列表
        /// </summary>
        private ArrayList alDrug = new ArrayList();

        /// <summary>
        /// 合同单位
        /// </summary>
        private NeuObject pactCode = new NeuObject();

        /// <summary>
        /// 默认合同单位
        /// </summary>
        private string defaulPactCode = string.Empty;
        
        /// <summary>
        /// 是否加载药品
        /// </summary>
        private bool isLoadDrug = false;

        /// <summary>
        /// 查询码
        /// </summary>
        private string code = "PY"; 

        /// <summary>
        /// 循环
        /// </summary>
        private int circle = 0;

        /// <summary>
        /// HIS项目dt
        /// </summary>
        DataTable dtHisItem = new DataTable();

        /// <summary>
        /// HIS项目dv
        /// </summary>
        DataView dvHisItem = new DataView();
        
        /// <summary>
        /// 中心项目dt
        /// </summary>
        DataTable dtCenterItem = new DataTable();

        /// <summary>
        /// 中心项目dv
        /// </summary>
        DataView dvCenterItem = new DataView();

        /// <summary>
        /// 对照项目dt
        /// </summary>
        DataTable dtCompareItem = new DataTable();

        /// <summary>
        /// 对照项目dv
        /// </summary>
        DataView dvCompareItem = new DataView();

        /// <summary>
        /// 当前对照类型
        /// </summary>
        private CompareTypes compareType;

        /// <summary>
        /// 项目等级辅助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemGradeHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region 属性

        /// <summary>
        /// 对照类型
        /// </summary>
        [Category("设置"), Description("设置项目类型 P:西药；C:中草药；Z:中成药；ALL:全部药品；Undrug:非药品")]
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
        /// 默认对照合同单位
        /// </summary>
        [Category("设置"), Description("默认对照的合同单位")]
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
        /// 合同单位信息
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

        #region 管理类

        /// <summary>
        /// 医保链接实体
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ConnectSI myConnectSI = null;

        /// <summary>
        /// 接口管理类
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// 常数管理类
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
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据，请稍后^^");
            Application.DoEvents();
            this.Init();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #region 方法

        /// <summary>
        /// 初始化显示数据
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

        #region 菜单栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("对照", "对照", FS.FrameWork.WinForms.Classes.EnumImageList.H合并, true, false, null);
            this.toolBarService.AddToolButton("取消", "取消", FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("清空", "清空", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 菜单栏事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "对照":
                    {
                        this.Compare();
                        break;
                    }
                case "取消":
                    {
                        this.Delete();
                        break;
                    }
                case "清空":
                    {
                        this.Clear();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        /// <summary>
        /// 初始化参数
        /// </summary>
        private void InitParaments()
        {
            isLoadDrug = !(CompareType.ToString() == CompareTypes.Undrug.ToString());
        }

        /// <summary>
        /// 设置显示列信息;
        /// </summary>
        private void InitColumn()
        {
            Type str = typeof(System.String);
            Type dec = typeof(System.Decimal);
            Type date = typeof(System.DateTime);

            if (this.isLoadDrug)
            {
                //初始化本地项目:
                DataColumn[] colHisItem = new DataColumn[]{
                    new DataColumn("药品编码", str),
                    new DataColumn("药品名称", str),
                    new DataColumn("拼音码", str),
                    new DataColumn("五笔码", str),
                    new DataColumn("自定义码", str),
                    new DataColumn("药监局编码",str),
                    new DataColumn("规格", str),
                    new DataColumn("通用名", str),
                    new DataColumn("通用名拼音", str),
                    new DataColumn("通用名五笔", str),
                    new DataColumn("国际编码", str),
                    new DataColumn("国家编码", str),
                    new DataColumn("价格", str),
                    new DataColumn("剂型编码", str)};

                dtHisItem.Columns.AddRange(colHisItem);
                DataColumn[] keyHis = new DataColumn[1];
                keyHis[0] = dtHisItem.Columns[0];
                dtHisItem.CaseSensitive = true;
                dtHisItem.PrimaryKey = keyHis;
                dvHisItem = new DataView(dtHisItem);
                dvHisItem.Sort = "药品编码 ASC";
                fpHisItem_Sheet1.DataSource = dvHisItem;

                DataColumn[] colCenterItem = new DataColumn[]{
                    new DataColumn("中心编码", str),
                    new DataColumn("中心项目名称", str),
                    new DataColumn("中心项目英文名", str),
                    new DataColumn("规格", str),
                    new DataColumn("剂型", str),
                    new DataColumn("中心拼音码", str),
                    new DataColumn("费用分类", str),
                    new DataColumn("目录级别", str),
                    new DataColumn("目录等级", str),
                    new DataColumn("自负比例", dec),
                    new DataColumn("基准价格", dec),
                    new DataColumn("限制使用说明", str),
                    new DataColumn("项目类别", str)};

                dtCenterItem.Columns.AddRange(colCenterItem);
                DataColumn[] keyCenter = new DataColumn[1];
                keyCenter[0] = dtCenterItem.Columns[0];
                dtCenterItem.CaseSensitive = true;
                dtCenterItem.PrimaryKey = keyCenter;
                dvCenterItem = new DataView(dtCenterItem);
                dvCenterItem.Sort = "中心编码 ASC";
                fpCenterItem_Sheet1.DataSource = dvCenterItem;
            }
            else 
            {
                //初始化本地项目:
                DataColumn[] colHisItem = new DataColumn[]{
                    new DataColumn("非药品编码", str),
                    new DataColumn("非药品名称", str),
                    new DataColumn("拼音码", str),
                    new DataColumn("五笔码", str),
                    new DataColumn("自定义码", str),
                    new DataColumn("规格", str),
                    new DataColumn("国际编码", str),
                    new DataColumn("国家编码", str),
                    new DataColumn("价格", str),
                    new DataColumn("单位", str)};

                dtHisItem.Columns.AddRange(colHisItem);
                DataColumn[] keyHis = new DataColumn[1];
                keyHis[0] = dtHisItem.Columns[0];
                dtHisItem.PrimaryKey = keyHis;
                dvHisItem = new DataView(dtHisItem);
                dvHisItem.Sort = "非药品编码 ASC";
                fpHisItem_Sheet1.DataSource = dvHisItem;

                DataColumn[] colCenterItem = new DataColumn[]{
                    new DataColumn("中心编码", str),
                    new DataColumn("中心项目名称", str),
                    new DataColumn("中心项目英文名", str),
                    new DataColumn("规格", str),
                    new DataColumn("剂型", str),
                    new DataColumn("中心拼音码", str),
                    new DataColumn("费用分类", str),
                    new DataColumn("目录级别", str),
                    new DataColumn("目录等级", str),
                    new DataColumn("自负比例", dec),
                    new DataColumn("基准价格", dec),
                    new DataColumn("限制使用说明", str),
                    new DataColumn("项目类别", str)};

                dtCenterItem.Columns.AddRange(colCenterItem);
                DataColumn[] keyCenter = new DataColumn[1];
                keyCenter[0] = dtCenterItem.Columns[0];
                dtCenterItem.CaseSensitive=true;
                dtCenterItem.PrimaryKey = keyCenter;
                dvCenterItem = new DataView(dtCenterItem);
                dvCenterItem.Sort = "中心编码 ASC";
                fpCenterItem_Sheet1.DataSource = dvCenterItem;
            }

            DataColumn[] colCompareItem = new DataColumn[]{
                new DataColumn("医院自定义码", str),
                new DataColumn("本地项目编码", str),
                new DataColumn("中心编码", str),
                new DataColumn("项目类别", str),
                new DataColumn("医保收费项目中文名称", str),
                new DataColumn("本地项目名称", str),
                new DataColumn("本地项目别名", str),
                new DataColumn("药监局编码",str),
                new DataColumn("医保收费项目英文名称", str),
                new DataColumn("医保剂型", str),
                new DataColumn("医保规格",str),
                new DataColumn("医保拼音代码", str),
                new DataColumn("医保费用分类代码", str),
                new DataColumn("医保目录级别", str),
                new DataColumn("医保目录等级", str),
                new DataColumn("自负比例", dec),
                new DataColumn("基准价格", dec),
                new DataColumn("限制使用说明", str),
                new DataColumn("医院拼音", str),
                new DataColumn("医院五笔码", str),
                new DataColumn("医院规格", str),
                new DataColumn("医院基本价格", dec),
                new DataColumn("医院剂型", str),
                new DataColumn("操作员", str),
                new DataColumn("操作时间", date)};

            dtCompareItem.Columns.AddRange(colCompareItem);
            DataColumn[] keyCompare = new DataColumn[1];
            keyCompare[0] = dtCompareItem.Columns[1];
            dtCompareItem.CaseSensitive=true;
            dtCompareItem.PrimaryKey = keyCompare;
            dvCompareItem = new DataView(dtCompareItem);
            dvCompareItem.Sort = "操作时间 DESC";
            fpCompareItem_Sheet1.DataSource = dvCompareItem;
        }

        /// <summary>
        /// HIS本地项目列属性
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
        /// 初始化中心列属性信息
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
        /// 初始化对照列属性信息
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
        /// 初始化显示数据
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
                #region 加载药品

                alHisItem = this.myInterface.GetNoCompareDrugItem(pactCode.ID, compareType.ToString());
                if (alHisItem != null)
                {
                    foreach (FS.HISFC.Models.Pharmacy.Item obj in alHisItem)
                    {
                        DataRow row = dtHisItem.NewRow();
                        row["药品编码"] = obj.ID;
                        row["药品名称"] = obj.Name;
                        row["拼音码"] = obj.SpellCode;
                        row["五笔码"] = obj.WBCode;
                        row["自定义码"] = obj.UserCode;
                        row["药监局编码"] = obj.NameCollection.FormalSpell.UserCode;
                        row["规格"] = obj.Specs;
                        row["国际编码"] = obj.NationCode;
                        row["国家编码"] = obj.GBCode;
                        row["价格"] = obj.PriceCollection.RetailPrice;
                        row["剂型编码"] = obj.DosageForm.ID;

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
                        row["中心编码"] = obj.ID;
                        row["中心项目名称"] = obj.Name;
                        row["中心项目英文名"] = obj.EnglishName;
                        row["规格"] = obj.Specs;
                        row["剂型"] = obj.DoseCode;
                        row["中心拼音码"] = obj.SpellCode;
                        row["费用分类"] = obj.FeeCode;
                        row["目录级别"] = obj.ItemType;
                        row["目录等级"] = obj.ItemGrade;
                        row["自负比例"] = obj.Rate;
                        row["基准价格"] = obj.Price;
                        row["限制使用说明"] = obj.Memo;
                        row["项目类别"] = obj.SysClass;
                        dtCenterItem.Rows.Add(row);
                    }
                }

                alCompareItem = this.myInterface.GetCompareItem(pactCode.ID, compareType.ToString());

                if (alCompareItem != null)
                {
                    foreach (FS.HISFC.Models.SIInterface.Compare obj in alCompareItem)
                    {
                        DataRow row = dtCompareItem.NewRow();

                        row["本地项目编码"] = obj.HisCode;
                        row["中心编码"] = obj.CenterItem.ID;
                        row["项目类别"] = obj.CenterItem.SysClass;
                        row["医保收费项目中文名称"] = obj.CenterItem.Name;
                        row["医保收费项目英文名称"] = obj.CenterItem.EnglishName;
                        row["本地项目名称"] = obj.Name;
                        row["本地项目别名"] = obj.RegularName;
                        row["药监局编码"] = obj.FdaDrguCode;
                        row["医保剂型"] = obj.CenterItem.DoseCode;
                        row["医保拼音代码"] = obj.CenterItem.SpellCode;
                        row["医保费用分类代码"] = obj.CenterItem.FeeCode;
                        row["医保目录级别"] = obj.CenterItem.ItemType;
                        row["医保目录等级"] = itemGradeHelper.GetObjectFromID(obj.CenterItem.ItemGrade).Name;
                        row["自负比例"] = obj.CenterItem.Rate;
                        row["基准价格"] = obj.CenterItem.Price;
                        row["限制使用说明"] = obj.CenterItem.Memo;
                        row["医院拼音"] = obj.SpellCode.SpellCode;
                        row["医院五笔码"] = obj.SpellCode.WBCode;
                        row["医院自定义码"] = obj.SpellCode.UserCode;
                        row["医院规格"] = obj.Specs;
                        row["医院基本价格"] = obj.Price;
                        row["医院剂型"] = obj.DoseCode;
                        row["操作员"] = obj.CenterItem.OperCode;
                        row["操作时间"] = obj.CenterItem.OperDate;

                        dtCompareItem.Rows.Add(row);
                    }
                }
                #endregion
            }
            else
            {
                #region 加载非药品
                alHisItem = myInterface.GetNoCompareUndrugItem(pactCode.ID);
                if (alHisItem != null)
                {
                    foreach (FS.HISFC.Models.Fee.Item.Undrug obj in alHisItem)
                    {
                        DataRow row = dtHisItem.NewRow();
                        row["非药品编码"] = obj.ID;
                        row["非药品名称"] = obj.Name;
                        row["拼音码"] = obj.SpellCode;
                        row["五笔码"] = obj.WBCode;
                        row["自定义码"] = obj.UserCode;
                        row["规格"] = obj.Specs;
                        row["国际编码"] = obj.NationCode;
                        row["国家编码"] = obj.GBCode;
                        row["价格"] = obj.Price;
                        row["单位"] = obj.PriceUnit;
                        dtHisItem.Rows.Add(row);
                    }
                }

                alCenterItem = this.myInterface.GetCenterItem(pactCode.ID, compareType.ToString());
                if (alCenterItem != null)
                {
                    foreach (FS.HISFC.Models.SIInterface.Item obj in alCenterItem)
                    {
                        DataRow row = dtCenterItem.NewRow();
                        row["中心编码"] = obj.ID;
                        row["中心项目名称"] = obj.Name;
                        row["中心项目英文名"] = obj.EnglishName;
                        row["规格"] = obj.Specs;
                        row["剂型"] = obj.DoseCode;
                        row["中心拼音码"] = obj.SpellCode;
                        row["费用分类"] = obj.FeeCode;
                        row["目录级别"] = obj.ItemType;
                        row["目录等级"] = obj.ItemGrade;
                        row["自负比例"] = obj.Rate;
                        row["基准价格"] = obj.Price;
                        row["限制使用说明"] = obj.Memo;
                        row["项目类别"] = obj.SysClass;
                        dtCenterItem.Rows.Add(row);
                    }
                }

                alCompareItem = this.myInterface.GetCompareItem(pactCode.ID, compareType.ToString());
                if (alCompareItem != null)
                {
                    foreach (FS.HISFC.Models.SIInterface.Compare obj in alCompareItem)
                    {
                        DataRow row = dtCompareItem.NewRow();

                        row["本地项目编码"] = obj.HisCode;
                        row["中心编码"] = obj.CenterItem.ID;
                        row["项目类别"] = obj.CenterItem.SysClass;
                        row["医保收费项目中文名称"] = obj.CenterItem.Name;
                        row["医保收费项目英文名称"] = obj.CenterItem.EnglishName;
                        row["本地项目名称"] = obj.Name;
                        row["本地项目别名"] = obj.RegularName;
                        row["医保剂型"] = obj.CenterItem.DoseCode;
                        row["医保拼音代码"] = obj.CenterItem.SpellCode;
                        row["医保费用分类代码"] = obj.CenterItem.FeeCode;
                        row["医保目录级别"] = obj.CenterItem.ItemType;
                        row["医保目录等级"] = obj.CenterItem.ItemGrade;
                        row["自负比例"] = obj.CenterItem.Rate;
                        row["基准价格"] = obj.CenterItem.Price;
                        row["限制使用说明"] = obj.CenterItem.Memo;
                        row["医院拼音"] = obj.SpellCode.SpellCode;
                        row["医院五笔码"] = obj.SpellCode.WBCode;
                        row["医院自定义码"] = obj.SpellCode.UserCode;
                        row["医院规格"] = obj.Specs;
                        row["医院基本价格"] = obj.Price;
                        row["医院剂型"] = obj.DoseCode;
                        row["操作员"] = obj.CenterItem.OperCode;
                        row["操作时间"] = obj.CenterItem.OperDate;

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
        /// 初始化合同单位
        /// </summary>
        /// <returns></returns>
        private int InitPactinfo()
        {
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

            ArrayList pactList = pactManager.QueryPactUnitAll();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + pactManager.Err);
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
        /// 初始化tab页与fp对应关系
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

        #region 未调用

        /// <summary>
        /// 连接医保服务器
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
                MessageBox.Show("连接医保服务器失败!,请重新配置连接" + ex.Message);
                return -1;
            }
            return 0;
        }

        #endregion

        /// <summary>
        /// 获得药品基本信息
        /// </summary>
        public void GetHisDrugItem()
        {
            alDrug = myInterface.GetNoCompareDrugItem(pactCode.ID, compareType.ToString());
        }

        /// <summary>
        /// 过滤
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
                    //            filterString = "拼音码" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "拼音码" + " like '%" + input + "%'";
                    //        }
                    //        break;
                    //    case "WB":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "五笔码" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "五笔码" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "US":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "自定义码" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "自定义码" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "ZW":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "药品名称" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "药品名称" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "TYPY":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "通用名拼音" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "通用名拼音" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //    case "TYWB":
                    //        if (!this.checkBox1.Checked)
                    //        {
                    //            filterString = "通用名五笔" + " like '" + input + "%'";
                    //        }
                    //        else
                    //        {
                    //            filterString = "通用名五笔" + " like '%" + input + "%'";
                    //        }

                    //        break;
                    //}
                    if (CompareType == CompareTypes.Undrug)
                    {
                        filterString = "拼音码" + " like '%" + input + "%'" + "or" + " 五笔码" + " like '%" + input + "%'" + "or" + " 自定义码" + " like '" + input + "%'" + "or" + " 非药品名称" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "拼音码" + " like '%" + input + "%'" + "or" + " 五笔码" + " like '%" + input + "%'" + "or" + " 自定义码" + " like '" + input + "%'" + "or" + " 药品名称" + " like '" + input + "%'";
                    }                    
                    this.dvHisItem.RowFilter = filterString;
                    InitColumnHIS();
                    break;
                case "CENTER":
                    if (!this.checkBox1.Checked)
                    {
                        filterString = "中心拼音码" + " like '" + input + "%'" + " or " + " 中心编码" + " like '" + input + "%'" + " or " + " 中心编码" + " like '" + input + "%'" + "or" + " 中心项目名称" + " like '" + input + "%'";
                    }
                    else
                    {
                        filterString = "中心拼音码" + " like '%" + input + "%'" + " or " + " 中心编码" + " like '%" + input + "%'" + " or " + " 中心编码" + " like '%" + input + "%'" + "or" + " 中心项目名称" + " like '" + input + "%'";
                    }
                    this.dvCenterItem.RowFilter = filterString;
                    InitColumnCenter();
                    break;
                case "COMPARE":
                    if (!this.checkBox1.Checked)
                    {
                        filterString = "医院拼音" + " like '" + input + "%'" + " or " + "医院自定义码" + " like '" + input + "%'" + " or " + "本地项目名称" + " like '%" + input + "%'";
                    }
                    else
                    {
                        filterString = "医院拼音" + " like '%" + input + "%'" + " or " + "医院自定义码" + " like '%" + input + "%'" + " or " + "本地项目名称" + " like '%" + input + "%'";
                    }
                    this.dvCompareItem.RowFilter = filterString;
                    break;
            }
        }

        /// <summary>
        /// 显示选择的本地信息
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
                    MessageBox.Show("未找到选定本地药品!");
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
                    MessageBox.Show("未找到选定本地非药品!");
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
        /// 显示选择的中心信息
        /// </summary>
        /// <param name="row"></param>
        private void SetCenterItemInfo(int row)
        {
            string centerCode = "";

            centerCode = this.fpCenterItem_Sheet1.Cells[row, 0].Text.Trim();

            Item obj = this.GetSelectCenterItem(centerCode);

            if (obj == null)
            {
                MessageBox.Show("未找到中心药品");
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
        /// 获得已选择HIS药品信息
        /// </summary>
        /// <param name="hisCode">医院药品代码</param>
        /// <returns>药品信息实体</returns>
        private FS.HISFC.Models.Pharmacy.Item GetSelectHisItem(string hisCode)
        {
            FS.HISFC.Models.Pharmacy.Item obj = new FS.HISFC.Models.Pharmacy.Item();

            DataRow row = this.dtHisItem.Rows.Find(hisCode);

            obj.ID = row["药品编码"].ToString();
            obj.Name = row["药品名称"].ToString();
            obj.SpellCode = row["拼音码"].ToString();
            obj.WBCode = row["五笔码"].ToString();
            obj.UserCode = row["自定义码"].ToString();
            obj.Specs = row["规格"].ToString();
            obj.NameCollection.RegularName = row["通用名"].ToString();
            obj.NameCollection.SpellCode = row["通用名拼音"].ToString();
            obj.NameCollection.WBCode = row["通用名五笔"].ToString();
            obj.NameCollection.InternationalCode = row["国际编码"].ToString();
            obj.GBCode = row["国家编码"].ToString();
            obj.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(row["价格"].ToString());
            obj.DosageForm.ID = row["剂型编码"].ToString();

            return obj;
        }
        
        /// <summary>
        /// 获得本地His非药品信息
        /// </summary>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Item.Undrug GetSelectHisUndrugItem(string hisCode)
        {
            FS.HISFC.Models.Fee.Item.Undrug obj = new FS.HISFC.Models.Fee.Item.Undrug();

            DataRow row = this.dtHisItem.Rows.Find(hisCode);

            obj.ID = row["非药品编码"].ToString();
            obj.Name = row["非药品名称"].ToString();
            obj.SpellCode = row["拼音码"].ToString();
            obj.WBCode = row["五笔码"].ToString();
            obj.UserCode = row["自定义码"].ToString();
            obj.Specs = row["规格"].ToString();
            obj.NationCode = row["国际编码"].ToString();
            obj.GBCode = row["国家编码"].ToString();
            obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["价格"].ToString());
            obj.PriceUnit = row["单位"].ToString();

            return obj;
        }

        /// <summary>
        /// 获得已选中心项目信息
        /// </summary>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        private FS.HISFC.Models.SIInterface.Item GetSelectCenterItem(string centerCode)
        {
            Item obj = new Item();

            DataRow row = this.dtCenterItem.Rows.Find(centerCode);
            if (isLoadDrug)
            {
                obj.ID = row["中心编码"].ToString();
                obj.Name = row["中心项目名称"].ToString();
                obj.EnglishName = row["中心项目英文名"].ToString();
            }
            else
            {
                obj.ID = row["中心编码"].ToString();
                obj.Name = row["中心项目名称"].ToString();
                obj.EnglishName = row["中心项目英文名"].ToString();
            }

            obj.Specs = row["规格"].ToString();
            obj.DoseCode = row["剂型"].ToString();
            obj.SpellCode = row["中心拼音码"].ToString();
            obj.FeeCode = row["费用分类"].ToString();
            obj.ItemType = row["目录级别"].ToString();
            obj.ItemGrade = row["目录等级"].ToString();
            obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(row["自负比例"].ToString());
            obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["基准价格"].ToString());
            obj.Memo = row["限制使用说明"].ToString();
            obj.SysClass = row["项目类别"].ToString();

            return obj;
        }

        /// <summary>
        /// 对照操作
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
                    MessageBox.Show("没有选择本地项目!");
                    return;
                }

                objHis = (FS.HISFC.Models.Pharmacy.Item)this.tbHisSpell.Tag;

                if (tbCenterSpell.Tag == null || tbCenterSpell.Tag.ToString() == "")
                {
                    MessageBox.Show("没有选择中心项目");
                    return;
                }

                objCenter = (Item)this.tbCenterSpell.Tag;

                DataRow row = this.dtCompareItem.NewRow();

                row["本地项目编码"] = objHis.ID;
                row["中心编码"] = objCenter.ID;
                row["项目类别"] = objCenter.SysClass;
                row["医保收费项目中文名称"] = objCenter.Name;
                row["医保收费项目英文名称"] = objCenter.EnglishName;
                row["本地项目名称"] = objHis.Name;
                row["本地项目别名"] = objHis.NameCollection.RegularName;//.RegularName;
                row["药监局编码"] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(objHis.ID).NameCollection.FormalSpell.UserCode;
                row["医保剂型"] = objCenter.DoseCode;
                row["医保规格"] = objCenter.Specs;
                row["医保拼音代码"] = objCenter.SpellCode;
                row["医保费用分类代码"] = objCenter.FeeCode;
                row["医保目录级别"] = objCenter.ItemType;
                row["医保目录等级"] = itemGradeHelper.GetObjectFromID(string.IsNullOrEmpty(this.cmbItemGrade.Tag.ToString()) ? "3" : this.cmbItemGrade.Tag.ToString()).Name;
                row["自负比例"] = objCenter.Rate;
                row["基准价格"] = objCenter.Price;
                row["限制使用说明"] = objCenter.Memo;
                row["医院拼音"] = objHis.SpellCode;
                row["医院五笔码"] = objHis.WBCode;// .SpellCode.WB_Code;
                row["医院自定义码"] = objHis.UserCode;//SpellCode.User_Code;
                row["医院规格"] = objHis.Specs;
                row["医院基本价格"] = objHis.PriceCollection.RetailPrice; //.RetailPrice;
                row["医院剂型"] = objHis.DosageForm.ID;
                row["操作员"] = myInterface.Operator.ID;
                row["操作时间"] = System.DateTime.Now;
              
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
                    MessageBox.Show("没有选择本地项目!");
                    return;
                }

                objHis = (FS.HISFC.Models.Fee.Item.Undrug)this.tbHisSpell.Tag;

                if (tbCenterSpell.Tag == null || tbCenterSpell.Tag.ToString() == "")
                {
                    MessageBox.Show("没有选择中心项目");
                    return;
                }

                objCenter = (Item)this.tbCenterSpell.Tag;

                DataRow row = this.dtCompareItem.NewRow();

                row["本地项目编码"] = objHis.ID;
                row["中心编码"] = objCenter.ID;
                row["项目类别"] = objCenter.SysClass;
                row["医保收费项目中文名称"] = objCenter.Name;
                row["医保收费项目英文名称"] = objCenter.EnglishName;
                row["本地项目名称"] = objHis.Name;
                row["本地项目别名"] = "";
                row["医保剂型"] = objCenter.DoseCode;
                row["医保规格"] = objCenter.Specs;
                row["医保拼音代码"] = objCenter.SpellCode;//SpellCode.Spell_Code;
                row["医保费用分类代码"] = objCenter.FeeCode;
                row["医保目录级别"] = objCenter.ItemType;
                row["医保目录等级"] = string.IsNullOrEmpty(this.cmbItemGrade.Tag.ToString()) ? "3" : this.cmbItemGrade.Tag.ToString();
                row["自负比例"] = objCenter.Rate;
                row["基准价格"] = objCenter.Price;
                row["限制使用说明"] = objCenter.Memo;
                row["医院拼音"] = objHis.SpellCode;
                row["医院五笔码"] = objHis.WBCode;
                row["医院自定义码"] = objHis.UserCode;
                row["医院规格"] = objHis.Specs;
                row["医院基本价格"] = objHis.Price;
                row["医院剂型"] = "";
                row["操作员"] = myInterface.Operator.ID;
                row["操作时间"] = System.DateTime.Now;

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
                MessageBox.Show("对照失败!" + myInterface.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            Clear();
            this.tbHisSpell.Focus();
        }
        
        /// <summary>
        /// 删除已对照信息
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
                MessageBox.Show("删除对照失败!" + myInterface.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            DataRow row = this.dtCompareItem.Rows.Find(hisCode);

            DataRow rowHis = dtHisItem.NewRow();
            if (isLoadDrug)
            {
                rowHis["药品编码"] = row["本地项目编码"].ToString();
                rowHis["药品名称"] = row["本地项目名称"].ToString();
                rowHis["通用名"] = row["本地项目别名"].ToString();
                rowHis["剂型编码"] = row["医院剂型"].ToString();
            }
            else
            {
                rowHis["非药品编码"] = row["本地项目编码"].ToString();
                rowHis["非药品名称"] = row["本地项目名称"].ToString();
            }
            rowHis["拼音码"] = row["医院拼音"].ToString();
            rowHis["五笔码"] = row["医院五笔码"].ToString();
            rowHis["自定义码"] = row["医院自定义码"].ToString();
            rowHis["规格"] = row["医院规格"].ToString();
            rowHis["价格"] = FS.FrameWork.Function.NConvert.ToDecimal(row["医院基本价格"].ToString());

            dtCompareItem.Rows.Remove(row);
            dtHisItem.Rows.Add(rowHis);
        }
        
        /// <summary>
        /// 清空信息
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
        /// 保存函数
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
                        MessageBox.Show("插入对照信息出错!" + myInterface.Err);
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
                        MessageBox.Show("删除对照信息出错!" + myInterface.Err);
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("保存成功!");
        }

        /// <summary>
        /// 导出当前项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            object obj = this.hashTableFp[this.tabCompare.SelectedTab];
            FarPoint.Win.Spread.FpSpread fp = obj as FarPoint.Win.Spread.FpSpread;

            SaveFileDialog op = new SaveFileDialog();
            op.Title = "请选择保存的路径和名称";
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
        /// 获得新增项目
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
                obj.HisCode = row["本地项目编码"].ToString();
                obj.CenterItem.ID = row["中心编码"].ToString();
                obj.CenterItem.SysClass = row["项目类别"].ToString();
                obj.CenterItem.Name = row["医保收费项目中文名称"].ToString();
                obj.CenterItem.EnglishName = row["医保收费项目英文名称"].ToString();
                obj.Name = row["本地项目名称"].ToString();
                obj.RegularName = row["本地项目别名"].ToString();
                obj.CenterItem.DoseCode = row["医保剂型"].ToString();
                obj.CenterItem.Specs = row["医保规格"].ToString();
                obj.CenterItem.SpellCode = row["医保拼音代码"].ToString();
                obj.CenterItem.FeeCode = row["医保费用分类代码"].ToString();
                obj.CenterItem.ItemType = row["医保目录级别"].ToString();
                obj.CenterItem.ItemGrade = row["医保目录等级"].ToString();
                obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(row["自负比例"].ToString());
                obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["基准价格"].ToString());
                obj.CenterItem.Memo = row["限制使用说明"].ToString();
                obj.SpellCode.SpellCode = row["医院拼音"].ToString();
                obj.SpellCode.WBCode = row["医院五笔码"].ToString();
                obj.SpellCode.UserCode = row["医院自定义码"].ToString();
                obj.Specs = row["医院规格"].ToString();
                obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["医院基本价格"].ToString());
                obj.DoseCode = row["医院剂型"].ToString();
                obj.CenterItem.OperCode = row["操作员"].ToString();
                //obj.CenterItem.OperDate = Convert.ToDateTime(row["操作时间"].ToString());
                //南庄修改 {87ED5A6B-F317-4579-9BC9-660182F49333}
                obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(row["操作时间"].ToString());

                al.Add(obj);
            }

            return al;
        }

        /// <summary>
        /// 删除对照项目
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
                obj.HisCode = row["本地项目编码"].ToString();
                obj.CenterItem.ID = row["中心编码"].ToString();
                obj.CenterItem.SysClass = row["项目类别"].ToString();
                obj.CenterItem.Name = row["医保收费项目中文名称"].ToString();
                obj.CenterItem.EnglishName = row["医保收费项目英文名称"].ToString();
                obj.Name = row["本地项目名称"].ToString();
                obj.RegularName = row["本地项目别名"].ToString();
                obj.CenterItem.DoseCode = row["医保剂型"].ToString();
                obj.CenterItem.Specs = row["医保规格"].ToString();
                obj.CenterItem.SpellCode = row["医保拼音代码"].ToString();
                obj.CenterItem.FeeCode = row["医保费用分类代码"].ToString();
                obj.CenterItem.ItemType = row["医保目录级别"].ToString();
                obj.CenterItem.ItemGrade = row["医保目录等级"].ToString();
                obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(row["自负比例"].ToString());
                obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["基准价格"].ToString());
                obj.CenterItem.Memo = row["限制使用说明"].ToString();
                obj.SpellCode.SpellCode = row["医院拼音"].ToString();
                obj.SpellCode.WBCode = row["医院五笔码"].ToString();
                obj.SpellCode.UserCode = row["医院自定义码"].ToString();
                obj.Specs = row["医院规格"].ToString();
                obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["医院基本价格"].ToString());
                obj.DoseCode = row["医院剂型"].ToString();
                obj.CenterItem.OperCode = row["操作员"].ToString();
                obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(row["操作时间"].ToString());

                al.Add(obj);
            }

            this.dtCompareItem.AcceptChanges();

            return al;
        }

        #endregion

        #region 事件

        /// <summary>
        /// HIS检索框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHisSpell_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("HIS", this.tbHisSpell.Text);
        }

        /// <summary>
        /// 中心项目检索框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCenterSpell_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem("CENTER", this.tbCenterSpell.Text);
        }

        /// <summary>
        /// 按键事件
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
                        tbSpell.Text = "拼音码";
                        break;
                    case 1:
                        code = "WB";
                        tbSpell.Text = "五笔码";
                        break;
                    case 2:
                        code = "US";
                        tbSpell.Text = "自定义码";
                        break;
                    case 3:
                        code = "ZW";
                        tbSpell.Text = "中文";
                        break;
                    case 4:
                        code = "TYPY";
                        tbSpell.Text = "通用拼音";
                        break;
                    case 5:
                        code = "TYWB";
                        tbSpell.Text = "通用五笔";
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
        /// HIS项目检索框
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
        /// HIS项目双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpHisItem_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpHisItem_Sheet1.RowCount >= 0)
                SetHisItemInfo(this.fpHisItem_Sheet1.ActiveRowIndex);
        }

        /// <summary>
        /// 中心项目检索框
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
        /// 中心项目双击事件
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
        /// HIS检索框进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHisSpell_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 0;
        }

        /// <summary>
        /// 中心项目检索框进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCenterSpell_Enter(object sender, EventArgs e)
        {
            this.tabCompare.SelectedIndex = 1;
        }

        /// <summary>
        /// 已对照项目查询框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCompareQuery_TextChanged(object sender, EventArgs e)
        {
            FilterItem("COMPARE", this.tbCompareQuery.Text);
        }
        
        /// <summary>
        /// 合同单位选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pactCode.ID = this.cmbPact.Tag.ToString();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据，请稍后^^");
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
