using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 医嘱查询控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2007-1-17]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucOrderShow : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderShow()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                txtQuery.TextChanged += new EventHandler(txtQuery_TextChanged);
            }
        }

        string orderId = "";
        FS.FrameWork.Models.NeuObject orderItem = new FS.FrameWork.Models.NeuObject();
        protected DataSet dsAllLong;
        protected DataSet dsAllShort;
        private DataSet dataSet = null;							//当前DataSet
        private DataView dvLong = null;							//当前DataView
        private DataView dvShort = null;						//当前DataView
        private string LONGSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "LongOrderQuerySetting.xml";
        private string SHORTSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "ShortOrderQuerySetting.xml";
        /// <summary>
        /// 精麻方打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST IRecipePrintST = null;
        private int sheetIndex = 0;			//当前活动Sheet页索引
        ucSubtblManager ucSubtblManager1 = null;//附材维护

        /// <summary>
        /// 医嘱单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintOrder IPrintOrderInstance = null;

        /// <summary>
        /// 是否自动保存 格式文件
        /// </summary>
        private bool isAutoSaveColumnProperty = true;

        /// <summary>
        /// 是否自动保存 格式文件
        /// </summary>
        [Description("是否自动保存 格式文件"), Category("控件设置")]
        public bool IsAutoSaveColumnProperty
        {
            get
            {
                return isAutoSaveColumnProperty;
            }
            set
            {
                isAutoSaveColumnProperty = value;
            }
        }

        /// <summary>
        /// 是否默认显示全部医嘱
        /// </summary>
        private bool isShowAll = true;

        /// <summary>
        /// 是否默认显示全部医嘱
        /// </summary>
        [Description("是否默认显示全部医嘱"), Category("控件设置")]
        public bool IsShowAll
        {
            get
            {
                return isShowAll;
            }
            set
            {
                isShowAll = value;
            }
        }


        /// <summary>
        /// 是否显示核对医嘱颜色，开启此功能，点击会有颜色显示
        /// </summary>
        private bool isCheckOrder = false;

        /// <summary>
        /// 是否显示核对医嘱颜色，开启此功能，点击会有颜色显示
        /// </summary>
        [Category("显示设置"), Description("是否显示核对医嘱颜色，开启此功能，点击会有颜色显示")]
        public bool IsCheckOrder
        {
            get
            {
                return isCheckOrder;
            }
            set
            {
                isCheckOrder = value;
            }
        }

        /// <summary>
        /// 皮试药批注
        /// </summary>
        ucTip ucTip = null;
        float[] longColumnWidth;
        float[] shortColumnWidth;
        ArrayList alQueryLong = new ArrayList();
        ArrayList alQueryShort = new ArrayList();
        //{2ED0FB9F-8CC3-4eca-81BB-DE13DAA6F9FD}
        bool value = true;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucOrderShow_Load(object sender, System.EventArgs e)
        {
            this.groupBox1.Click += new EventHandler(groupBox1_Click);

            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_Sheet_Clicked);
            this.fpSpread2.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_Sheet_Clicked);
        }

        void groupBox1_Click(object sender, EventArgs e)
        {
            this.fpSpread1.Focus();
        }


        #region 属性
        FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;
        /// <summary>
        /// 患者基本信息
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                if (this.myPatientInfo == null)
                    this.myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                return this.myPatientInfo;
            }
            set
            {
                this.myPatientInfo = value;

                this.QueryOrder();

            }
        }

        FS.HISFC.Models.Base.Employee oper = null;
        /// <summary>
        /// 操作员信息
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Base.Employee Oper
        {
            get
            {
                if (oper == null)
                    oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                return oper;
            }
            set
            {
                this.oper = value;
            }
        }

        #region 新增属性 设置是否折叠附材显示{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
        /// <summary>
        /// 是否折叠附材显示
        /// </summary>
        private bool isFoldSubtblShow = false;

        /// <summary>
        /// 是否折叠附材显示
        /// </summary>
        [Category("控件设置"), Description("是否折叠附材显示")]
        public bool IsFoldSubtblShow
        {
            get
            {
                return isFoldSubtblShow;
            }
            set
            {
                isFoldSubtblShow = value;
            }
        }
        #endregion

        /// <summary>
        /// 是否显示过滤功能
        /// </summary>
        public bool IsShowFilter
        {
            set
            {
                this.cmbOderStatus.Visible = value;
            }
        }

        /// <summary>
        /// 是否显示停止/作废医嘱
        /// </summary>
        private bool showDCOrder = true;
        /// <summary>
        /// 是否显示停止/作废医嘱
        /// </summary>
        public bool IsShowDCOrder
        {
            set
            {
                this.showDCOrder = value;
            }
        }

        /// <summary>
        /// 是否允许操作附材
        /// </summary>
        protected bool enableSubtbl = true;
        /// <summary>
        /// 是否允许操作附材 当用于护士站综合收费时查询医嘱时不允许操作附材
        /// </summary>
        public bool IsEnabledSubtbl
        {
            set
            {
                this.enableSubtbl = value;
            }
        }
        private bool autoQuitFeeApply = false;
        /// <summary>
        /// 退费申请是否自动确认
        /// </summary>
        [Category("控件设置"), Description("退费申请是否自动确认")]
        public bool AutoQuitFeeApply
        {
            set
            {
                this.autoQuitFeeApply = value;
            }
            get
            {
                return this.autoQuitFeeApply;
            }
        }

        #endregion

        #region 初始化
        private void InitFP()
        {
            if (this.IsFoldSubtblShow)
            {
                //this.fpSpread1_Sheet1.RowHeader.Visible = true;
                //this.fpSpread1_Sheet2.RowHeader.Visible = true;
            }


            for (int i = 0; i < this.fpSpread1.Sheets[0].Rows.Count; i++)
            {
                this.fpSpread1.Sheets[0].Rows[i].BackColor = System.Drawing.SystemColors.ControlLight;
                this.fpSpread1.Sheets[0].Rows[i].Height = 26;
            }

            for (int i = 0; i < this.fpSpread1.Sheets[1].Rows.Count; i++)
            {
                this.fpSpread1.Sheets[1].Rows[i].BackColor = System.Drawing.SystemColors.ControlLight;
                this.fpSpread1.Sheets[1].Rows[i].Height = 26;
            }

            SetColumnProperty();
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void SetColumnProperty()
        {
            if (System.IO.File.Exists(LONGSETTINGFILENAME))
            {
                if (this.longColumnWidth == null || this.shortColumnWidth == null)
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);
                    this.longColumnWidth = new float[this.fpSpread1.Sheets[0].Columns.Count];
                    for (int i = 0; i < this.fpSpread1.Sheets[0].Columns.Count; i++)
                    {
                        this.longColumnWidth[i] = this.fpSpread1.Sheets[0].Columns[i].Width;
                    }
                    this.shortColumnWidth = new float[this.fpSpread1.Sheets[1].Columns.Count];
                    for (int i = 0; i < this.fpSpread1.Sheets[1].Columns.Count; i++)
                    {
                        this.shortColumnWidth[i] = this.fpSpread1.Sheets[1].Columns[i].Width;
                    }
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < this.fpSpread1.Sheets[0].Columns.Count; i++)
                            this.fpSpread1.Sheets[0].Columns[i].Width = this.longColumnWidth[i];
                        for (int i = 0; i < this.fpSpread1.Sheets[1].Columns.Count; i++)
                            this.fpSpread1.Sheets[1].Columns[i].Width = this.shortColumnWidth[i];
                    }
                    catch
                    { }
                }
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);
            }

            this.fpSpread1.Sheets[0].Columns[(Int32)ColEnum.ColNurMemo].Visible = false;//！备注不显示
            this.fpSpread1.Sheets[1].Columns[(Int32)ColEnum.ColNurMemo].Visible = false;//！备注不显示

            //这是干什么？
            this.fpSpread1.Sheets[0].Columns[21].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[0].Columns[20].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[1].Columns[21].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[1].Columns[20].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

        }

        private FarPoint.Win.Spread.CellType.DateTimeCellType dateCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();

        /// <summary>
        /// 初始化界面显示
        /// </summary>
        /// <returns></returns>
        private DataSet InitDataSet()
        {
            try
            {
                dataSet = new DataSet();
                Type dtStr = System.Type.GetType("System.String");
                //Type dtDbl = typeof(System.Double);
                Type dtInt = typeof(System.Int32);
                Type dtBoolean = typeof(System.Boolean);
                Type dtDate = typeof(System.DateTime);

                DataTable table = new DataTable("Table");

                table.Columns.AddRange(new DataColumn[]
				{
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.SubComNo),dtStr),
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColNurMemo),dtStr),						//0
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSort),dtInt),					//35  by zlw 2006-4-18
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColInforming),dtBoolean),	
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderTypeID),dtStr),					//1
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderTypeName),dtStr),				//2
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderID),dtStr),				//3
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState),dtStr),				//4 新开立，审核，执行
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo),dtStr),					//5
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColMainDrug),dtStr),					//6
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderBgn),dtStr),
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColItemName),dtStr),				//8
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboFlag),dtStr),					    //9
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColMemo),dtStr),					//20
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColFirstDayQty),dtStr),  //首次量 houwb 2011-3-12
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColQty),dtStr),					//9
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColUnit),dtStr),				//10
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColDoseOnce),dtStr),				//11
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColDoseUnit),dtStr),					//12
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColHerbalQty),dtStr),					//13
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColFrequencyID),dtStr),				//14
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColFrequencyName),dtStr),				//15
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColUsageID),dtStr),				//16
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColUsageName),dtStr),				//17
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSysType),dtStr),
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.SysClassName),dtStr),				//18
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderEnd),dtStr),				//19
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColDoc),dtStr),				//21
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColExeDeptID),dtStr),			//22
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColExeDeptName),dtStr),			//22
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColNurseCellName),dtStr),   //{D642A3BA-C93C-4e03-A5AF-FF878D7D9833}				
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColEmEmergency),dtStr),					//24
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColCheckPart),dtStr),				//25
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSample),dtStr),				//26
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColStockDeptID),dtStr),			//27
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColStockDeptName),dtStr),				//28
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColUseRecID),dtStr),				//29	
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColUseRecName),dtStr),					//30
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColRecDept),dtStr),				//31
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColRecDate),dtStr),				//32
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColConfirmID),dtStr),
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColConfirmName),dtStr),
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColDCOperID),dtStr),				//33
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColDCOperName),dtStr),					//34
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColHypoTest),dtStr),				//36
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSubtbl),dtBoolean),

                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.SpellCode),dtStr),
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.WbCode),dtStr),
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ItemCode),dtStr)
					
				});


                dataSet.Tables.Add(table);

                DataColumn[] keys = new DataColumn[1];
                keys[0] = dataSet.Tables[0].Columns[FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderID)];
                this.dataSet.Tables[0].PrimaryKey = keys;

                return dataSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion

        #region 显示医嘱

        /// <summary>
        /// 添加实体toTable
        /// </summary>
        /// <param name="list"></param>
        private void AddObjectsToTable(ArrayList list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }

            this.alQueryLong = new ArrayList();
            this.alQueryShort = new ArrayList();

            Components.Order.OutPatient.Classes.LogManager.Write("开始添加到界面显示" + DateTime.Now.ToString());
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in list)
            {
                if (order == null)
                    continue;

                if (!this.showDCOrder)
                {
                    if (order.Status == 3)		//不显示作废/停止医嘱
                        continue;
                }

                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)//长期医嘱
                {
                    dsAllLong.Tables[0].Rows.Add(AddObjectToRow(order, dsAllLong.Tables[0]));
                    alQueryLong.Add(order.Item.Name);
                }
                else//临时医嘱
                {
                    dsAllShort.Tables[0].Rows.Add(AddObjectToRow(order, dsAllShort.Tables[0]));
                    alQueryShort.Add(order.Item.Name);
                }


                Components.Order.OutPatient.Classes.LogManager.Write("添加【" + order.Item.Name + "】" + DateTime.Now.ToString());
            }
            Components.Order.OutPatient.Classes.LogManager.Write("结束添加到界面显示" + DateTime.Now.ToString());

            //foreach (DataRow row in dsAllLong.Tables[0].Rows)
            //{
            //    if (row[(Int32)ColEnum.ColSubtbl].ToString() == "True")
            //    {
            //        int maxsort = -1;
            //        string combNo = row[(Int32)ColEnum.ColComboNo].ToString();

            //        DataRow[] rows = dsAllLong.Tables[0].Select(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo) + "='" + combNo + "'");

            //        foreach (DataRow combRow in rows)
            //        {
            //            if ((int)combRow[(Int32)ColEnum.ColSort] > maxsort)
            //            {
            //                maxsort = (int)combRow[(Int32)ColEnum.ColSort];
            //            }
            //        }

            //        row[(Int32)ColEnum.ColSort] = maxsort;
            //    }
            //}

            this.lbPatient.Text = myPatientInfo.PID.PatientNO//住院号
                        + "  " + this.myPatientInfo.Name //姓名
                        + "  " + this.myPatientInfo.Sex.Name //性别
                        + "  " + CacheManager.InOrderMgr.GetAge(this.myPatientInfo.Birthday)//年龄
                        + "  " + this.myPatientInfo.Pact.Name//合同单位
                        + "  住院医师：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(myPatientInfo.PVisit.AdmittingDoctor.ID)
                //+ "\r\n"
                        + "  住院日期：" + myPatientInfo.PVisit.InTime.ToString("yyyy.MM.dd") + "-" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy.MM.dd") + "/" + CacheManager.RadtIntegrate.GetInDays(myPatientInfo.ID).ToString() + "天"//住院日期
                        + "\r\n"
                        + "总费用: " + myPatientInfo.FT.TotCost.ToString()
                        + "  预交金: " + this.myPatientInfo.FT.PrepayCost.ToString()
                        + "  自费：" + myPatientInfo.FT.OwnCost.ToString()
                        + "  报销：" + myPatientInfo.FT.PubCost.ToString()
                        + "  余额: " + this.myPatientInfo.FT.LeftCost.ToString();
        }

        /// <summary>
        /// 添加到datarow
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataRow AddObjectToRow(FS.HISFC.Models.Order.Inpatient.Order order, DataTable table)
        {
            DataRow row = table.NewRow();

            string strTemp = "";

            row[(Int32)ColEnum.SubComNo] = order.SubCombNO.ToString();

            if (order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                row[(Int32)ColEnum.ColMainDrug] = System.Convert.ToInt16(order.Combo.IsMainDrug);	//6
                row[(Int32)ColEnum.ColDoseOnce] = order.DoseOnce.ToString();					//10
                row[(Int32)ColEnum.ColDoseUnit] = objItem.DoseUnit;								//0415 2307096 wang renyi
                order.DoseUnit = objItem.DoseUnit;

                //只有草药才显示付数
                if (order.Item.SysClass.ID.ToString() == "PCC")
                {
                    row[(Int32)ColEnum.ColHerbalQty] = order.HerbalQty;								//11
                }
            }
            else if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                //FS.HISFC.Models.Fee.Item objItem = order.Item as FS.HISFC.Models.Fee.Item;
                row[(Int32)ColEnum.ColDoseOnce] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                row[(Int32)ColEnum.ColDoseUnit] = order.Unit;
            }

            if (order.Note != "")
            {
                row["!"] = order.Note;
            }
            row[(Int32)ColEnum.ColOrderTypeID] = System.Convert.ToInt16(order.OrderType.Type);			//0
            row[(Int32)ColEnum.ColOrderTypeName] = order.OrderType.Name;								//2
            row[(Int32)ColEnum.ColOrderID] = order.ID;										//3
            row[(Int32)ColEnum.ColOrderState] = order.Status;										//12 新开立，审核，执行
            row[(Int32)ColEnum.ColComboNo] = order.Combo.ID;	//5

            //系统类别显示
            row[(Int32)ColEnum.SysClassName] = order.Item.SysClass.Name;

            #region 项目名称显示

            //自备、嘱托标记  用于护士打印单据和医嘱单显示区分
            string byoStr = "";

            if (!order.OrderType.IsCharge || order.Item.ID == "999")
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    byoStr = "[自备]";
                }
                else
                {
                    byoStr = "[嘱托]";
                }
            }

            if (order.Item.Specs == null || order.Item.Specs.Trim() == "")
            {
                row[(Int32)ColEnum.ColItemName] = byoStr + order.Item.Name + CacheManager.InOrderMgr.TransHypotest(order.HypoTest);
            }
            else
            {
                row[(Int32)ColEnum.ColItemName] = byoStr + (order.Item.Name + "[" + order.Item.Specs + "]" + CacheManager.InOrderMgr.TransHypotest(order.HypoTest));
            }
            #endregion

            //首日量显示 houwb 2011-3-12
            row[(Int32)ColEnum.ColFirstDayQty] = order.FirstUseNum;

            //医保用药
            if (order.IsPermission) row[(Int32)ColEnum.ColItemName] = "√" + row[(Int32)ColEnum.ColItemName];

            //长期医嘱数量显示为每次量 houwb  2011-3-12
            if (order.OrderType.IsDecompose
                && order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                row[(Int32)ColEnum.ColQty] = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                row[(Int32)ColEnum.ColUnit] = order.DoseUnit;
            }
            else
            {
                row[(Int32)ColEnum.ColQty] = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                row[(Int32)ColEnum.ColUnit] = order.Unit;
            }

            row[(Int32)ColEnum.ColFrequencyID] = order.Frequency.ID;
            row[(Int32)ColEnum.ColFrequencyName] = order.Frequency.Name;
            row[(Int32)ColEnum.ColUsageID] = order.Usage.ID;
            row[(Int32)ColEnum.ColUsageName] = order.Usage.Name;
            row[(Int32)ColEnum.ColSysType] = order.Item.SysClass.Name;
            row[(Int32)ColEnum.ColOrderBgn] = order.BeginTime;
            row[(Int32)ColEnum.ColExeDeptID] = order.ExeDept.ID;
            if (order.ExeDept.Name == "" && order.ExeDept.ID != "")
            {
                order.ExeDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ExeDept.ID);
            }
            row[(Int32)ColEnum.ColExeDeptName] = order.ExeDept.Name;

            //{D642A3BA-C93C-4e03-A5AF-FF878D7D9833}
            row[(Int32)ColEnum.ColNurseCellName] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.NurseStation.ID);

            if (order.IsEmergency)
            {
                strTemp = "是";
            }
            else
            {
                strTemp = "否";
            }
            row[(Int32)ColEnum.ColEmEmergency] = strTemp;
            row[(Int32)ColEnum.ColCheckPart] = order.CheckPartRecord;
            row[(Int32)ColEnum.ColSample] = order.Sample;
            row[(Int32)ColEnum.ColStockDeptID] = order.StockDept.ID;
            row[(Int32)ColEnum.ColStockDeptName] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.StockDept.ID);

            row[(Int32)ColEnum.ColMemo] = order.Memo;
            row[(Int32)ColEnum.ColUseRecID] = order.Oper.ID;

            row[(Int32)ColEnum.ColUseRecName] = order.Oper.Name;
            if (order.ReciptDept.Name == "" && order.ReciptDept.ID != "")
            {
                order.ReciptDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ReciptDept.ID);
            }
            row[(Int32)ColEnum.ColDoc] = order.ReciptDoctor.Name;
            row[(Int32)ColEnum.ColRecDept] = order.ReciptDept.Name;
            row[(Int32)ColEnum.ColRecDate] = order.MOTime.ToString();

            #region addby xuewj {B8EDA745-62C3-407e-9480-3A9E60647141} 未停止的医嘱 停止时间不显示
            if (order.EndTime > new DateTime(1900, 1, 1))
            {
                row[(Int32)ColEnum.ColOrderEnd] = order.EndTime;
            }
            #endregion
            row[(Int32)ColEnum.ColDCOperID] = order.DCOper.ID;
            if (string.IsNullOrEmpty(order.DCOper.Name))
            {
                row[(int)ColEnum.ColDCOperName] = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order.DCOper.ID);
            }
            else
            {
                row[(int)ColEnum.ColDCOperName] = order.DCOper.Name;
            }
            //row[(Int32)ColEnum.ColDCOperName] = order.DCOper.Name;

            row[(Int32)ColEnum.ColSort] = order.SortID;

            //皮试
            row[(Int32)ColEnum.ColHypoTest] = CacheManager.InOrderMgr.TransHypotest(order.HypoTest);

            row[(Int32)ColEnum.ColSubtbl] = order.IsSubtbl;

            if (order.Item.ID != "999")
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    row[(Int32)ColEnum.SpellCode] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).SpellCode;
                    row[(Int32)ColEnum.WbCode] = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).WBCode;
                }
                else
                {
                    row[(Int32)ColEnum.SpellCode] = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SpellCode;
                    row[(Int32)ColEnum.WbCode] = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).WBCode;
                }
            }
            else
            {
                //row[(Int32)ColEnum.SpellCode] = FS.FrameWork.Public.String.GetSpell(order.Item.Name);
                //row[(Int32)ColEnum.WbCode] = FS.FrameWork.Public.String.GetSpell(order.Item.Name);
            }

            row[(int)ColEnum.ColConfirmID] = order.Nurse.ID;
            if (string.IsNullOrEmpty(order.Nurse.Name))
            {
                row[(int)ColEnum.ColConfirmName] = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order.Nurse.ID);
            }
            else
            {
                row[(int)ColEnum.ColConfirmName] = order.Nurse.Name;
            }

            row[(int)ColEnum.ItemCode] = order.Item.ID;
            return row;
        }

        /// <summary>
        /// 显示费用信息
        /// </summary>
        private void QueryOrderFee()
        {
            string orderID = this.fpSpread1.ActiveSheet.Cells[this.fpSpread1.ActiveSheet.ActiveRowIndex, (int)ColEnum.ColOrderID].Text;

            if (orderID.Length <= 0)
            {
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order tmpOrder = CacheManager.InOrderMgr.QueryOneOrder(orderID);

            if (tmpOrder == null || tmpOrder.ID.Length <= 0)
                return;

            DataSet dsFees = new DataSet();

            try
            {
                CacheManager.InOrderMgr.QueryOrderFees(tmpOrder.Patient.ID, tmpOrder.ID, ref dsFees);

                #region 处理汇总费用

                this.fpSpread2_Sheet2.Rows.Remove(0, this.fpSpread2_Sheet2.Rows.Count);

                Hashtable hsItemsSum = new Hashtable();

                foreach (DataRow row in dsFees.Tables[0].Rows)
                {
                    if (!hsItemsSum.Contains(FS.FrameWork.Function.NConvert.ToDateTime(row[2].ToString()).ToString("yyyy-MM-dd")))
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                        obj.ID = FS.FrameWork.Function.NConvert.ToDateTime(row[2].ToString()).ToString("yyyy-MM-dd");
                        obj.Name = row[0].ToString();
                        obj.Memo = FS.FrameWork.Function.NConvert.ToDecimal(row[3].ToString()).ToString();
                        obj.User01 = FS.FrameWork.Function.NConvert.ToDecimal(row[4].ToString()).ToString();

                        hsItemsSum.Add(obj.ID, obj);
                    }
                    else
                    {

                        FS.FrameWork.Models.NeuObject obj = hsItemsSum[FS.FrameWork.Function.NConvert.ToDateTime(row[2].ToString()).ToString("yyyy-MM-dd")]
                            as FS.FrameWork.Models.NeuObject;

                        obj.Memo = (FS.FrameWork.Function.NConvert.ToDecimal(obj.Memo) +
                            FS.FrameWork.Function.NConvert.ToDecimal(row[3].ToString())).ToString();
                        obj.User01 = (FS.FrameWork.Function.NConvert.ToDecimal(obj.User01) +
                            FS.FrameWork.Function.NConvert.ToDecimal(row[4].ToString())).ToString();
                    }
                }

                foreach (System.Collections.DictionaryEntry objOrder in hsItemsSum)
                {
                    this.fpSpread2_Sheet2.Rows.Add(0, 1);

                    FS.FrameWork.Models.NeuObject itemSum = objOrder.Value as FS.FrameWork.Models.NeuObject;

                    this.fpSpread2_Sheet2.Cells[0, 0].Text = itemSum.ID;
                    this.fpSpread2_Sheet2.Cells[0, 1].Text = itemSum.Name;
                    this.fpSpread2_Sheet2.Cells[0, 2].Text = itemSum.Memo;
                    this.fpSpread2_Sheet2.Cells[0, 3].Text = itemSum.User01;

                }

                this.fpSpread2_Sheet2.SortRows(0, true, true);


                #endregion

                this.fpSpread2_Sheet1.DataSource = new DataView(dsFees.Tables[0]);

                this.fpSpread2_Sheet1.Columns.Get(7).CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

                this.fpSpread2.ActiveSheetIndex = 0;
            }
            catch
            { }

            for (int i = 0; i < this.fpSpread2.Sheets[0].Rows.Count; i++)
            {
                this.fpSpread2.Sheets[0].Rows[i].BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.fpSpread2.Sheets[0].Rows[i].Height = 26;
            }

            for (int i = 0; i < this.fpSpread2.Sheets[1].Rows.Count; i++)
            {
                this.fpSpread2.Sheets[1].Rows[i].BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.fpSpread2.Sheets[1].Rows[i].Height = 26;
            }
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void QueryOrder()
        {
            try
            {
                Components.Order.OutPatient.Classes.LogManager.Write("开始查询" + DateTime.Now.ToString());
                this.fpSpread1.Sheets[0].RowCount = 0;
                this.fpSpread1.Sheets[1].RowCount = 0;
                this.dsAllLong.Tables[0].Rows.Clear();
                this.dsAllShort.Tables[0].Rows.Clear();
                //this.panel3.Visible = false;
            }
            catch { }
            if (this.myPatientInfo == null)
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询医嘱,请稍候!");
            Application.DoEvents();

            if (this.ucSubtblManager1 != null)
            {
                this.ucSubtblManager1.PatientInfo = this.myPatientInfo;
            }

            Components.Order.OutPatient.Classes.LogManager.Write("开始查询医嘱和附材" + DateTime.Now.ToString());
            //查询所有医嘱类型
            ArrayList alAllOrder = CacheManager.InOrderMgr.QueryOrder(this.myPatientInfo.ID);
            if (alAllOrder == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            //查询所有医嘱附材
            ArrayList alSub = CacheManager.InOrderMgr.QueryOrderSubtbl(this.myPatientInfo.ID);
            if (alSub == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            Components.Order.OutPatient.Classes.LogManager.Write("结束查询医嘱和附材" + DateTime.Now.ToString());

            #region 将组合号添加附材表示，防止画组合号时画到一起
            if (this.IsFoldSubtblShow)
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order orderSubtbl in alSub)
                {
                    orderSubtbl.Combo.ID = orderSubtbl.Combo.ID + "@";
                }
            }
            #endregion
            try
            {
                dsAllLong.Tables[0].Clear();
                dsAllShort.Tables[0].Clear();
                alAllOrder.AddRange(alSub);

                ArrayList al = new ArrayList();

                //屏蔽显示重整医嘱					
                foreach (FS.HISFC.Models.Order.Order info in alAllOrder)
                {
                    //if (info.Status != 4)
                    al.Add(info);
                }

                Components.Order.OutPatient.Classes.LogManager.Write("添加到界面显示" + DateTime.Now.ToString());

                this.AddObjectsToTable(al);
                dvLong = new DataView(dsAllLong.Tables[0]);
                dvShort = new DataView(dsAllShort.Tables[0]);

                //{EACD8AED-FDF6-490a-980C-EC9A89391719} 显示前先进行排序操作
                try
                {
                    dvLong.Sort = FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSort) + " ASC , " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo) + " ASC";
                    dvShort.Sort = FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSort) + " ASC , " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo) + " ASC";

                    Components.Order.OutPatient.Classes.LogManager.Write("结束排序" + DateTime.Now.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("对显示医嘱根据" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSort) + "、" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo) + "排序发生错误" + ex.Message);
                    return;
                }

                this.fpSpread1.Sheets[0].DataSource = dvLong;
                this.fpSpread1.Sheets[1].DataSource = dvShort;


                Components.Order.OutPatient.Classes.LogManager.Write("初始化显示" + DateTime.Now.ToString());

                this.InitFP();

                this.fpSpread1.Sheets[0].Columns[0, this.fpSpread1.Sheets[0].Columns.Count - 1].Locked = true;
                this.fpSpread1.Sheets[1].Columns[0, this.fpSpread1.Sheets[0].Columns.Count - 1].Locked = true;

            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
                return;
            }

            Components.Order.OutPatient.Classes.LogManager.Write("过滤" + DateTime.Now.ToString());
            this.Filter(this.cmbOderStatus.SelectedIndex);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            Components.Order.OutPatient.Classes.LogManager.Write("结束查询" + DateTime.Now.ToString() + "\r\n\r\n");
        }


        ///<summary>
        /// 刷新组合
        /// </summary>
        public void RefreshCombo()
        {
            Classes.Function.DrawCombo(this.fpSpread1.Sheets[0], (int)ColEnum.ColComboNo, (int)ColEnum.ColComboFlag);

            Classes.Function.DrawCombo(this.fpSpread1.Sheets[1], (int)ColEnum.ColComboNo, (int)ColEnum.ColComboFlag);

            this.SetSortID();
        }

        /// <summary>
        /// 更新医嘱状态
        /// </summary>
        public void RefreshOrderState()
        {
            for (int i = 0; i < this.fpSpread1.Sheets[0].Rows.Count; i++)
            {
                this.ChangeOrderState(i, 0, false);
            }
            for (int i = 0; i < this.fpSpread1.Sheets[1].Rows.Count; i++)
            {
                this.ChangeOrderState(i, 1, false);
            }
        }


        /// <summary>
        /// 刷新医嘱状态
        /// </summary>
        /// <param name="row"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="reset"></param>
        private void ChangeOrderState(int row, int SheetIndex, bool reset)
        {
            try
            {
                int i = (int)ColEnum.ColOrderState;//"医嘱状态";
                int j = (int)ColEnum.ColSort;//顺序号所在的列

                int state = int.Parse(this.fpSpread1.Sheets[SheetIndex].Cells[row, i].Text);

                if (state != 3 && state != 4 && this.fpSpread1.Sheets[SheetIndex].Cells[row, (int)ColEnum.ColOrderEnd].Text != "")
                {
                    this.fpSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(132, 72, 168);
                }
                else
                {
                    switch (state)
                    {
                        case 0:
                            this.fpSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(128, 255, 128);
                            break;
                        case 1:
                            this.fpSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(106, 174, 242);
                            break;
                        case 2:
                            this.fpSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(243, 230, 105);
                            break;
                        case 3:
                        case 4:
                            this.fpSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.FromArgb(248, 120, 222);
                            break;
                        case 5:
                            this.fpSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.Black;
                            break;
                        default:
                            this.fpSpread1.Sheets[SheetIndex].RowHeader.Rows[row].BackColor = Color.White;
                            break;
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// 设置备注和皮试
        /// </summary>
        /// <param name="k"></param>
        private void SetTip(int k)
        {
            for (int i = 0; i < this.fpSpread1.Sheets[k].RowCount; i++)//批注
            {

                string sHypotest = this.fpSpread1.Sheets[k].Cells[i, (int)ColEnum.ColHypoTest].Text;

                switch (sHypotest)
                {
                    case "Negative":
                        fpSpread1.Sheets[k].Cells[i, (int)ColEnum.ColItemName].ForeColor = Color.Red;
                        break;
                    default:
                        fpSpread1.Sheets[k].Cells[i, (int)ColEnum.ColItemName].ForeColor = Color.Black;
                        break;
                }
            }
        }

        private void SetSortID()
        {
            this.fpSpread1.Sheets[0].RowHeaderAutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.fpSpread1.Sheets[1].RowHeaderAutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
        }

        #endregion

        #region IToolBar 成员

        public int Retrieve()
        {
            // TODO:  添加 ucOrderShow.Retrieve 实现
            this.QueryOrder();
            return 0;
        }

        public int Save()
        {
            if (ifHaveNotSameCom() == 0)
            {
                UpdateOrderSortID();

                QueryOrder();
            }
            else
            {
                MessageBox.Show("同一组合序号要相同,否则会影响组合显示!");
            }

            return 0;
        }


        #endregion

        #region 过滤

        /// <summary>
        /// 过滤医嘱显示
        /// </summary>
        /// <param name="State"></param>
        public void Filter(int State)
        {
            if (this.PatientInfo == null)
            {
                return;
            }
            if (this.PatientInfo.ID == "")
            {
                return;
            }
            if (this.dvLong == null || this.dvShort == null)
            {
                return;
            }

            string rowFilter = "(" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColItemName) + " like '%{0}%' or " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.SpellCode) + " like '%{0}%' or " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.WbCode) + " like '%{0}%')";
            string textQuery = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtQuery.Text.Trim());
            rowFilter = System.String.Format(rowFilter, textQuery);

            //查询时候才能过滤
            switch (State)
            {
                case 0://全部
                    rowFilter += "";
                    break;
                case 2://当天
                    DateTime dt = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                    DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                    DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                    //rowFilter+= " (开始时间>=" + "#" + dt1 + "#" + " and 开始时间<=" + "#" + dt2 + "#" + " ) and  附材标志 = true";
                    rowFilter += "and (" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " ='1' or " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '2')"
                        + " and (" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderBgn) + ">=" + "#" + dt1 + "#" + " and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderBgn) + "<=" + "#" + dt2 + "#)";
                    break;
                case 1://有效
                    rowFilter += "and (" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " ='1' or " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '2')";
                    break;
                case 5://无效
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '3'";
                    break;
                case 3://未审核
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '0'";
                    break;
                case 4://当天作废
                    DateTime d = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                    DateTime d1 = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                    DateTime d2 = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderEnd) + ">=" + "#" + d1 + "#" + " and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderEnd) + "<=" + "#" + d2 + "#" + " and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '3'";
                    break;
                case 6://未审核
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '4'";//已作废
                    //皮试医嘱//{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColHypoTest) + " in ('2','3','4')";
                    break;
                default:
                    rowFilter += "";
                    break;
            }

            this.dvLong.RowFilter = rowFilter;
            this.dvShort.RowFilter = rowFilter;

            this.InitFP();
            this.RefreshOrderState();
            this.RefreshCombo();
            this.RefreshSubtblDisplay(0);
            this.RefreshSubtblDisplay(1);
            #region 折叠显示附材{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
            if (this.IsFoldSubtblShow)
            {
                this.FoldSubtblDisplay(0);
                this.FoldSubtblDisplay(1);
            }
            #endregion

            this.SetTip(0);
            this.SetTip(1);
        }

        #endregion

        #region 吸附窗口
        public Crownwood.Magic.Docking.DockingManager dockingManager;
        /// <summary>
        /// 附材管理控件
        /// </summary>
        private Crownwood.Magic.Docking.Content content;

        /// <summary>
        /// 皮试药控件
        /// </summary>
        private Crownwood.Magic.Docking.Content hypoTestContent;

        private Crownwood.Magic.Docking.WindowContent wc;

        private Crownwood.Magic.Docking.WindowContent wc1;

        public void DockingManager()
        {
            this.dockingManager = new Crownwood.Magic.Docking.DockingManager(this, Crownwood.Magic.Common.VisualStyle.IDE);
            this.dockingManager.InnerControl = this.panel1;		//在InnerControl前加入的控件不受停靠窗口的影响

            content = new Crownwood.Magic.Docking.Content(this.dockingManager);
            content.Control = ucSubtblManager1;

            Size ucSize = content.Control.Size;

            content.Title = "附材管理";
            content.FullTitle = "附材管理";
            content.AutoHideSize = ucSize;
            content.DisplaySize = ucSize;

            //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
            this.hypoTestContent = new Crownwood.Magic.Docking.Content(this.dockingManager);
            this.hypoTestContent.Control = ucTip;

            Size ucTipSize = this.hypoTestContent.Control.Size;
            this.hypoTestContent.Title = "皮试药管理";
            this.hypoTestContent.FullTitle = "皮试药管理";
            this.hypoTestContent.AutoHideSize = ucTipSize;
            this.hypoTestContent.DisplaySize = ucTipSize;

            this.dockingManager.Contents.Add(this.hypoTestContent);
            this.dockingManager.Contents.Add(content);

        }
        #endregion

        #region 初始化

        /// <summary>
        /// 用于外部调用时初始化数据
        /// </summary>
        /// <returns></returns>
        public int InitForOut()
        {
            //初始化farpoint
            dsAllLong = this.InitDataSet();
            dsAllShort = this.InitDataSet();

            //sheet0 ==长期 sheet1 ==临时
            this.fpSpread1.Sheets[0].DataSource = dsAllLong.Tables[0];
            this.fpSpread1.Sheets[1].DataSource = dsAllShort.Tables[0];

            this.fpSpread1.Sheets[0].DataAutoSizeColumns = false;
            this.fpSpread1.Sheets[1].DataAutoSizeColumns = false;

            this.fpSpread1.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating;
            this.fpSpread1.SheetTabClick += new FarPoint.Win.Spread.SheetTabClickEventHandler(fpSpread1_SheetTabClick);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);

            DateTime dt = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            this.InitFP();

            //FS.FrameWork.Public.String.GetSpell();

            try
            {
                this.fpSpread1.ActiveSheetIndex = 0;

                if (isShowAll)
                {
                    this.cmbOderStatus.SelectedIndex = 0;//默认全部的医嘱
                }
                else
                {
                    this.cmbOderStatus.SelectedIndex = 1;//默认选有效的医嘱
                }
            }
            catch
            {
                return -1;
            }

            #region 附材管理窗口
            ucSubtblManager1 = new ucSubtblManager();
            //皮试药{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
            ucTip = new ucTip();
            ucTip.IsCanModifyHypotest = true;
            this.ucTip.OKEvent += new myTipEvent(ucTip_OKEvent);
            this.DockingManager();
            this.ucSubtblManager1.ShowSubtblFlag += new ucSubtblManager.ShowSubtblFlagEvent(ucSubtblManager1_ShowSubtblFlag);
            #endregion

            this.value = false;
            return 1;
        }

        #endregion

        /// <summary>
        /// 单击事件 展开附材{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                return; // 点击列标题行返回
            }
            if (!e.RowHeader)
            {
                return;//只有点击行标题才继续处理
            }
            if (!this.IsFoldSubtblShow)
            {
                return;
            }

            string subtblFlag = e.View.Sheets[e.View.ActiveSheetIndex].RowHeader.Cells[e.Row, 0].Text; //附材标识
            string comboNo = e.View.Sheets[e.View.ActiveSheetIndex].Cells[e.Row, (int)ColEnum.ColComboNo].Text;//获取组合号
            bool isContinue = true;
            int indexDown = e.Row;

            switch (subtblFlag)
            {
                case "+"://展开
                    while (isContinue)
                    {
                        #region 向下查找 如到最后一行或组合号不同则停止
                        indexDown = indexDown + 1;
                        if (indexDown >= e.View.Sheets[e.View.ActiveSheetIndex].RowCount) //如果到最后一行，不进行查找了
                            isContinue = false;
                        else
                        {
                            if (e.View.Sheets[e.View.ActiveSheetIndex].Cells[indexDown, (int)ColEnum.ColSubtbl].Text == "True")
                            {
                                //如果向下查找行是附材，则显示
                                e.View.Sheets[e.View.ActiveSheetIndex].Rows[indexDown].Visible = true;
                                continue;
                            }
                            else
                            {
                                isContinue = false;
                            }
                        }
                        #endregion
                    }
                    //e.View.Sheets[e.View.ActiveSheetIndex].RowHeader.Cells[e.Row, 0].Text = "-";
                    break;
                case "-"://折叠
                    while (isContinue)
                    {
                        #region 向下查找 如到最后一行或组合号不同则停止
                        indexDown = indexDown + 1;
                        if (indexDown >= e.View.Sheets[e.View.ActiveSheetIndex].RowCount) //如果到最后一行，不进行查找了
                            isContinue = false;
                        else
                        {
                            if (e.View.Sheets[e.View.ActiveSheetIndex].Cells[indexDown, (int)ColEnum.ColSubtbl].Text == "True")
                            {
                                //如果向下查找行是附材，则隐藏
                                e.View.Sheets[e.View.ActiveSheetIndex].Rows[indexDown].Visible = false;
                                continue;
                            }
                            else
                            {
                                isContinue = false;
                            }
                        }
                        #endregion
                    }
                    //e.View.Sheets[e.View.ActiveSheetIndex].RowHeader.Cells[e.Row, 0].Text = "+";
                    break;
                default:
                    break;
            }

        }
        #region 附材显示
        /// <summary>
        /// 更新附材显示标志
        /// </summary>
        private void RefreshSubtblFlag(string operFlag, bool isShowSubtblFlag, object sender)
        {
            if (this.fpSpread1.Sheets[this.sheetIndex].Rows.Count < 0)
                return;

            int rowIndex = this.fpSpread1.ActiveSheet.ActiveRowIndex;
            string s = this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColItemName].Text;       //医嘱名称
            string comboNo = this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColComboNo].Text;	//组合号

            #region 刷新组合显示"@"
            //用于查找同一组合医嘱
            int iUp = rowIndex;
            bool isUp = true;
            int iDown = rowIndex;
            bool isDown = true;

            if (!isShowSubtblFlag)	//不需显示"@"符号
            {
                while (isUp || isDown)
                {
                    #region 向上查找 如到最前一行或组合号不同则置标志为false
                    if (isUp)
                    {
                        iUp = iUp - 1;
                        if (iUp < 0)
                            isUp = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColComboNo].Text == comboNo)				//同一组合
                            {
                                if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text.Substring(0, 1) == "@")	//医嘱名称带有"@"符号
                                {
                                    this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text = this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text.Substring(1);
                                }
                            }
                            else		//不是同一组合 不需再查找
                            {
                                isUp = false;
                            }
                        }
                    }
                    #endregion

                    #region 向下查找 如遇最下一行或组合号不同则置标志为false
                    if (isDown)
                    {
                        iDown = iDown + 1;
                        if (iDown >= this.fpSpread1.Sheets[this.sheetIndex].Rows.Count)
                            isDown = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColComboNo].Text == comboNo)					//同一组合
                            {
                                if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColItemName].Text.Substring(0, 1) == "@")	//医嘱名称带有"@"符号
                                {
                                    this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColItemName].Text = this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColItemName].Text.Substring(1);
                                }
                            }
                            else			//不是同一组合 不需再查找
                            {
                                isDown = false;
                            }
                        }
                    }
                    #endregion
                }
                //更新本条记录医嘱标志
                if (s.Substring(0, 1) == "@")
                {
                    this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColItemName].Text = s.Substring(1);
                }
            }
            else		//需要显示"@"符号
            {
                bool isAlreadyHave = false;			//该组合是否已包含"@"医嘱符号
                while (isUp || isDown)
                {
                    #region 向上查找 如到最前一行或组合号不同则置标志为false
                    if (isUp)
                    {
                        iUp = iUp - 1;
                        if (iUp < 0)
                            isUp = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColComboNo].Text == comboNo)					//同一组合中
                            {
                                if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text.Substring(0, 1) == "@")		//已经存在"@"符号
                                {
                                    isAlreadyHave = true;
                                    break;
                                }
                            }
                            else
                            {
                                isUp = false;
                            }
                        }
                    }
                    #endregion

                    #region 向下查找 如遇最下一行或组合号不同则置标志为false
                    if (isDown)
                    {
                        iDown = iDown + 1;
                        if (iDown >= this.fpSpread1.Sheets[this.sheetIndex].Rows.Count)
                            isDown = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColComboNo].Text == comboNo)
                            {
                                if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColItemName].Text.Substring(0, 1) == "@")
                                {
                                    isAlreadyHave = true;
                                    break;
                                }
                            }
                            else
                            {
                                isDown = false;
                            }
                        }
                    }
                    #endregion
                }
                //本组合内未存在"@"符号
                if (!isAlreadyHave && s.Substring(0, 1) != "@")
                {
                    this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColItemName].Text = "@" + s;
                }
            }
            #endregion

            #region 改变界面附材的显示 添加或删除
            try
            {
                if (operFlag == "2")					//删除/停止操作
                {
                    #region 处理删除/停止操作时的附材界面显示
                    FS.HISFC.Models.Order.Inpatient.Order order = sender as FS.HISFC.Models.Order.Inpatient.Order;
                    if (order == null)
                    {
                        MessageBox.Show("准备刷新处理界面附材显示时发生错误 请退出界面重试");
                        return;
                    }
                    if (order.ID != "")					//已保存附材 
                    {
                        if (this.sheetIndex == 0)		//长嘱
                        {
                            string[] tempFind = new string[1];								//寻找需删除行的主键
                            tempFind[0] = order.ID;
                            DataRow delRow = this.dsAllLong.Tables[0].Rows.Find(tempFind);	//需由DataSet内移除的行				
                            this.dsAllLong.Tables[0].Rows.Remove(delRow);					//移除行

                            if (order.Status != 0)											//对已审核/执行的数据 仍需显示 但改变状态
                            {
                                order.Status = 3;
                                //添加改变状态的行 
                                this.dsAllLong.Tables[0].Rows.Add(this.AddObjectToRow(order, this.dsAllLong.Tables[0]));
                            }
                        }
                        else							//临嘱
                        {
                            string[] tempFind = new string[1];								//寻找需删除行的主键
                            tempFind[0] = order.ID;
                            DataRow delRow = this.dsAllShort.Tables[0].Rows.Find(tempFind);//需由DataSet内移除的行	
                            this.dsAllShort.Tables[0].Rows.Remove(delRow);					//移除行

                            if (order.Status != 0)											//对已审核/执行的数据 仍需显示 但改变状态
                            {
                                order.Status = 3;
                                //添加改变状态的行 
                                this.dsAllShort.Tables[0].Rows.Add(this.AddObjectToRow(order, this.dsAllShort.Tables[0]));
                            }
                        }
                        //处理相关信息刷新
                        this.Filter(this.cmbOderStatus.SelectedIndex);

                    }
                    #endregion
                }
                else									//保存操作
                {
                    if (this.ucSubtblManager1.AddSubInfo != null && this.ucSubtblManager1.AddSubInfo.Count > 0)
                    {
                        this.AddObjectsToTable(this.ucSubtblManager1.AddSubInfo);			//向DataSet内加入新数据
                        //处理相关信息刷新

                        this.Filter(this.cmbOderStatus.SelectedIndex);

                    }
                    if (this.ucSubtblManager1.EditSubInfo != null && this.ucSubtblManager1.EditSubInfo.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Order.Order info in this.ucSubtblManager1.EditSubInfo)
                        {
                            if (info == null) continue;
                            int row = 0, col = 0;
                            string find = this.fpSpread1.Search(this.fpSpread1.ActiveSheetIndex, info.ID, false, true, false, false,
                                0, 0, ref row, ref col);
                            if (find == info.ID)
                            {
                                this.fpSpread1.ActiveSheet.Cells[row, (int)ColEnum.ColQty].Text = info.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                                this.fpSpread1.ActiveSheet.Cells[row, (int)ColEnum.ColUnit].Text = info.Unit;
                            }
                        }
                    }
                }
            }
            catch (System.Data.ConstraintException)
            {
                MessageBox.Show("无法添加两条同样的附材！可以修改附材数量");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("处理附材显示时发生不可预知错误 请退出界面重试" + ex.Message);
                return;
            }
            #endregion

            this.RefreshSubtblDisplay(this.sheetIndex);
        }

        /// <summary>
        /// 刷新附材显示 对附材显示斜体字
        /// </summary>
        /// <param name="sheetIndex"></param>
        private void RefreshSubtblDisplay(int sheetIndex)
        {
            for (int i = 0; i < this.fpSpread1.Sheets[sheetIndex].RowCount; i++)
            {
                string temp = this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColSubtbl].Text;

                if (temp == "True")
                {
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColItemName].Font = new Font("宋体", 8.5f, System.Drawing.FontStyle.Italic);
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, 9].Locked = true;

                }
                else
                {
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColItemName].Font = new Font("宋体", 10, System.Drawing.FontStyle.Bold);
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, 9].Locked = true;
                }
            }

        }

        /// <summary>
        /// 折叠附材显示 {32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
        /// </summary>
        /// <param name="sheetIndex"></param>
        private void FoldSubtblDisplay(int sheetIndex)
        {
            for (int i = 0; i < this.fpSpread1.Sheets[sheetIndex].RowCount; i++)
            {
                string temp = this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColSubtbl].Text;

                if (temp == "True")
                {
                    //是附材操作
                    this.fpSpread1.Sheets[sheetIndex].Rows[i].Visible = false; //隐藏显示
                    string comboNo = this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColComboNo].Text;
                    bool isContinue = true;
                    int indexUp = i;
                    while (isContinue)
                    {
                        #region 向上查找 如到最前一行或组合号不同则停止
                        indexUp = indexUp - 1;
                        if (indexUp < 0) //如果到最前一行，不进行查找了
                            isContinue = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[sheetIndex].Cells[indexUp, (int)ColEnum.ColSubtbl].Text == "True")
                            {
                                //如果向上查找行是附材，则不进行处理，继续向上查找
                                continue;
                            }
                            if (this.fpSpread1.Sheets[sheetIndex].Cells[indexUp, (int)ColEnum.ColComboNo].Text + "@" == comboNo)				//同一组合
                            {
                                //this.fpSpread1.Sheets[sheetIndex].RowHeader.Cells[indexUp, 0].Text = "+"; 
                                isContinue = false;
                            }
                            else		//不是同一组合 不需再查找
                            {
                                isContinue = false;
                            }
                        }
                        #endregion
                        //暂时不考虑向下查找
                    }
                }
                else
                {
                    this.fpSpread1.Sheets[sheetIndex].Rows[i].Visible = true; //非附材医嘱进行显示
                }
            }
        }
        #endregion


        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            this.sheetIndex = e.SheetTabIndex;
            this.ucSubtblManager1.Clear();
            if (this.sheetIndex == 0)//长嘱
            {
                //长嘱默认显示有效医嘱--取消 by huangchw 2012-09-08
                //this.cmbOderStatus.SelectedIndex = 1;

                //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
                if (this.dockingManager != null)
                {
                    this.dockingManager.HideContent(this.hypoTestContent);
                }
            }

            if (this.sheetIndex == 1)//临嘱
            {
                //临嘱默认显示当天医嘱--取消 by huangchw 2012-09-08
                //this.cmbOderStatus.SelectedIndex = 2;

                //{07B60769-DFBE-4797-823D-3C07ACD737B4}
                //临时医嘱不显示附材界面
                if (this.dockingManager != null)
                {
                    this.dockingManager.HideContent(this.content);
                }
            }


            this.Filter(this.cmbOderStatus.SelectedIndex);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!this.enableSubtbl)
                return;
            //{2ED0FB9F-8CC3-4eca-81BB-DE13DAA6F9FD}
            if (!this.value)
            {
                return;
            }
            //判断当前的停靠窗口是否已显示 如未显示 则显示停靠窗口
            try
            {
                //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
                //皮试药显示
                //获取皮试药标记
                this.orderId = e.View.Sheets[e.View.ActiveSheetIndex].Cells[e.Row, (int)ColEnum.ColOrderID].Value.ToString();
                string flag = e.View.Sheets[e.View.ActiveSheetIndex].Cells[e.Row, (int)ColEnum.ColHypoTest].Value.ToString();
                if (flag == "2" || flag == "3" || flag == "4")
                {
                    this.ucTip.Hypotest = FS.FrameWork.Function.NConvert.ToInt32(flag);
                    this.ucTip.Tip = CacheManager.InOrderMgr.QueryOrderNote(orderId);

                    if (this.hypoTestContent != null && this.hypoTestContent.Visible == false)
                    {
                        if (wc1 == null && this.dockingManager != null)
                        {
                            wc1 = this.dockingManager.AddContentWithState(this.hypoTestContent, Crownwood.Magic.Docking.State.DockRight);
                            this.dockingManager.AddContentToWindowContent(this.hypoTestContent, wc1);
                        }
                        if (this.dockingManager != null)
                            this.dockingManager.ShowContent(this.hypoTestContent);
                    }
                }
                else
                {
                    if (this.dockingManager != null)
                    {
                        this.dockingManager.HideContent(this.hypoTestContent);
                    }
                }
                //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}

                //{07B60769-DFBE-4797-823D-3C07ACD737B4}
                //临时医嘱不显示附材界面
                if (e.View.ActiveSheetIndex == 0)
                {
                    if (this.content != null && this.content.Visible == false)
                    {
                        if (wc == null && this.dockingManager != null)
                        {
                            wc = this.dockingManager.AddContentWithState(content, Crownwood.Magic.Docking.State.DockBottom);
                            this.dockingManager.AddContentToWindowContent(content, wc);
                        }
                        if (this.dockingManager != null)
                            this.dockingManager.ShowContent(this.content);
                    }
                    if (this.ucSubtblManager1 != null && !e.RowHeader && !e.ColumnHeader)		//点击非列标题与行标题
                    {
                        ucSubtblManager1.OrderID = this.fpSpread1.ActiveSheet.Cells[e.Row, (int)ColEnum.ColOrderID].Text;
                        ucSubtblManager1.ComboNo = this.fpSpread1.ActiveSheet.Cells[e.Row, (int)ColEnum.ColComboNo].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ucSubtblManager1_ShowSubtblFlag(string operFlag, bool isShowSubtblFlag, object sender)
        {
            this.RefreshSubtblFlag(operFlag, isShowSubtblFlag, sender);
        }

        /// <summary>
        /// 皮试药保存
        /// {17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
        /// </summary>
        /// <param name="Tip"></param>
        /// <param name="Hypotest"></param>
        public void ucTip_OKEvent(string Tip, int Hypotest)
        {
            if (CacheManager.InOrderMgr.UpdateFeedback(this.PatientInfo.ID, this.orderId, Tip, Hypotest) == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                CacheManager.InOrderMgr.Err = "";
                return;
            }


            this.fpSpread1.ActiveSheet.Cells[this.fpSpread1.ActiveSheet.ActiveRowIndex, (int)ColEnum.ColHypoTest].Value = Hypotest;

            this.SetTip(this.fpSpread1.ActiveSheetIndex);// {E97273E4-CF5A-47bf-97C6-8025504486C4}
            FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo = new FS.HISFC.Models.Order.Medical.AllergyInfo();

            FS.HISFC.BizLogic.Order.Medical.AllergyManager allergyManager = new FS.HISFC.BizLogic.Order.Medical.AllergyManager();
            allergyInfo.PatientNO = this.PatientInfo.PID.PatientNO;
            allergyInfo.PatientType = FS.HISFC.Models.Base.ServiceTypes.I;
            allergyInfo.Allergen.ID = this.orderItem.ID;
            allergyInfo.Allergen.Name = this.orderItem.Name;
            allergyInfo.Symptom.ID = "1";
            allergyInfo.Oper.ID = allergyManager.Operator.ID;
            allergyInfo.Oper.OperTime = allergyManager.GetDateTimeFromSysDateTime();
            allergyInfo.ValidState = true;
            allergyInfo.Type = FS.HISFC.Models.Order.Medical.AllergyType.DA;
            allergyInfo.ID = this.PatientInfo.ID;
            allergyInfo.HappenNo = int.Parse(this.orderId);

            if (Hypotest == 3)// {E97273E4-CF5A-47bf-97C6-8025504486C4}
            {
               if (allergyManager.InsertAllergyInfo(allergyInfo) < 0)
                {
                    if (allergyManager.UpdateAllergyInfo(allergyInfo) < 0)
                    {
                        MessageBox.Show("皮试结果记录插入失败！" + allergyManager.Err);
                    }
                }
            }
            else if (Hypotest == 4)
            {
                allergyInfo.ValidState = false;
                if (allergyManager.UpdateAllergyInfo(allergyInfo) < 0)
                {
                    MessageBox.Show("皮试结果记录插入失败！" + allergyManager.Err);
                }
            }
        }

        #region 右键菜单
        private void fpSpread1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (!this.enableSubtbl)
            //    return;
            if (e.Button == MouseButtons.Right)
            {
                FarPoint.Win.Spread.Model.CellRange c = this.fpSpread1.GetCellFromPixel(0, 0, e.X, e.Y);
                if (c.Row >= 0)
                {
                    this.fpSpread1.ActiveSheet.ActiveRowIndex = c.Row;
                    this.fpSpread1.ActiveSheet.ClearSelection();
                    this.fpSpread1.ActiveSheet.AddSelection(c.Row, 0, 1, 1);
                }
                if (c.Row < 0) return;
                orderId = this.fpSpread1.ActiveSheet.Cells[c.Row, (int)ColEnum.ColOrderID].Text;
                orderItem.ID = this.fpSpread1.ActiveSheet.Cells[c.Row, (int)ColEnum.ItemCode].Text;
                orderItem.Name = this.fpSpread1.ActiveSheet.Cells[c.Row, (int)ColEnum.ColItemName].Text;

                if (this.contextMenuStrip1.Items.Count > 0)
                    this.contextMenuStrip1.Items.Clear();

                //ToolStripMenuItem mnuSetTime = new ToolStripMenuItem("执行时间");
                //mnuSetTime.Click += new EventHandler(mnuSetTime_Click);
                //右键显示皮试和批注houwb {D822412B-07F4-4ed8-A749-E6EC16019461}
                ToolStripMenuItem menuTip = new ToolStripMenuItem("批注/皮试");
                menuTip.Click += new EventHandler(menuTip_Click);

                //this.contextMenuStrip1.Items.Add(mnuSetTime);
                this.contextMenuStrip1.Items.Add(menuTip);

                #region 精麻处方打印 //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}

                ToolStripMenuItem mnuOrderST = new ToolStripMenuItem();//精麻处方打印 
                mnuOrderST.Click += new EventHandler(mnuOrderST_Click);
                mnuOrderST.Text = "精麻处方打印";
                this.contextMenuStrip1.Items.Add(mnuOrderST);

                #endregion
            }
        }

        private void mnuSetTime_Click(object sender, EventArgs e)
        {
            //frmSetExecTime frm = new frmSetExecTime();
            //frm.SetItem(orderId);
            //frm.ShowDialog();
        }

        /// <summary>
        /// 皮试和批注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuTip_Click(object sender, EventArgs e)
        {
            //右键显示皮试和批注houwb {D822412B-07F4-4ed8-A749-E6EC16019461}
            int iHypotest = CacheManager.InOrderMgr.QueryOrderHypotest(this.orderId);
            if (iHypotest == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }

            //非药品医嘱不显示皮试页
            FS.HISFC.Models.Order.Order o = CacheManager.InOrderMgr.QueryOneOrder(this.orderId);
            if (o.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                ucTip.Hypotest = 1;
            }
            this.ucTip.Tip = CacheManager.InOrderMgr.QueryOrderNote(this.orderId);
            ucTip.Hypotest = iHypotest;
            ucTip.OKEvent += new myTipEvent(ucTip1_OKEvent);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucTip);
        }

        /// <summary>
        /// 精麻方打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuOrderST_Click(object sender, EventArgs e)
        {
            if (this.IRecipePrintST == null)
            {
                this.IRecipePrintST = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrder),
                    typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST)) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST;
            }
            if (this.IRecipePrintST != null)
            {
                string where = " where met_ipm_order.mo_order='{0}'  and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ( 'P1','P2','S1','SY','O1'))";
                where = string.Format(where, this.orderId);
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                ArrayList alOrderTemp = ordMgr.QueryOrderBase(where);
                if (alOrderTemp.Count > 0)
                {
                    //临时这样写一下，后面看看是加接口还是怎么样吧
                    //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}
                    //FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST ucRecipePrintST = new FS.SOC.Local.Order.OrderPrint.Iboren.ucRecipePrintST();
                    //ucRecipePrintST.MakaLabel(ucRecipePrintST.ChangeOrderToOrderST(alOrderTemp));
                    //ucRecipePrintST.SetPatientInfo(this.myPatientInfo);
                    //ucRecipePrintST.PrintRecipe();
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    this.IRecipePrintST.OnInPatientPrint(this.myPatientInfo, obj, obj, alOrderTemp, alOrderTemp, false, false, "", obj);
                }
                else
                {
                    MessageBox.Show("此医嘱非精麻处方！");
                    return;
                }
            }
        }

        /// <summary>
        /// 批注事件
        /// </summary>
        private void ucTip1_OKEvent(string Tip, int Hypotest)
        {
            //int rowIndex = this.fpSpread1.Sheets[this.sheetIndex].ActiveRowIndex;
            //if (CacheManager.InOrderMgr.Updatefeedback(this.myPatientInfo.ID, this.orderId, Tip, Hypotest) == -1)
            //{
            //    MessageBox.Show(CacheManager.InOrderMgr.Err);
            //    CacheManager.InOrderMgr.Err = "";
            //    return;
            //}
            //this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColHypoTest].Text = Hypotest.ToString();
            //this.SetTip(this.sheetIndex);
            //FS.HISFC.Models.RADT.PatientInfo p = pManager.PatientQuery(this.myPatientInfo.ID);
            ////传送消息给科室
            //FS.Common.Class.Message.SendMessage(p.Name + "有的医嘱有问题<" + Tip + ">需要更改。", p.PVisit.PatientLocation.Dept.ID, "22222");
        }
        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                if (this.fpSpread1.ActiveSheetIndex == 0)
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
                else
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);
            }
            else if (keyData.GetHashCode() == Keys.F12.GetHashCode())
            {
                this.fpSpread1.ActiveSheetIndex = (this.fpSpread1.ActiveSheetIndex + 1) % 2;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtQuery_TextChanged(object sender, System.EventArgs e)
        {
            this.Filter(cmbOderStatus.SelectedIndex);
        }

        #region  医嘱
        /// <summary>
        /// 通过医嘱状态过滤医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOderStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Filter(this.cmbOderStatus.SelectedIndex);
            this.fpSpread1.Focus();
        }
        /// <summary>
        /// 更新医嘱序号
        /// </summary>
        private void UpdateOrderSortID()
        {
            int colorderid = (int)ColEnum.ColOrderID;    //医嘱流水号    
            int sortid = (int)ColEnum.ColSort;
            string OrderID = null;//医嘱编号
            string SortID = null; //顺序号
            FarPoint.Win.Spread.SheetView sv = fpSpread1.ActiveSheet;//取得当前操作的有效的SHEET
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在更新医嘱序号...");
            Application.DoEvents();
            for (int i = 0; i < sv.Rows.Count; i++) //长期医嘱
            {
                OrderID = sv.Cells[i, colorderid].Text;//医嘱编号
                SortID = sv.Cells[i, sortid].Text; //顺序编号
                int Sortid = 0;
                if (sv.Cells[i, 2].Text.ToUpper() == "TRUE")
                {

                    Sortid = Convert.ToInt32(SortID) - 10000;

                    SortID = Sortid.ToString();
                }
                #region 医嘱序号更新
                if (CacheManager.InOrderMgr.UpdateOrderSortID(OrderID, SortID) == -1)
                {
                    MessageBox.Show("更新错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;

                }

                #endregion
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }


        /// <summary>
        /// 必要时判断是不是含有相同组合号，序号不同的医嘱
        /// </summary>
        /// <returns></returns>
        private int ifHaveNotSameCom()
        {
            int m = 0;
            for (int i = 0; i < fpSpread1.ActiveSheet.RowCount; i++)
            {
                string sortNum = fpSpread1.ActiveSheet.Cells[i, 1].Text;//当前选中行的序号
                string sComNum = fpSpread1.ActiveSheet.Cells[i, 7].Text;//当前选中行的组合号
                for (int j = 0; j < fpSpread1.ActiveSheet.RowCount; j++)
                {
                    string sortNum1 = fpSpread1.ActiveSheet.Cells[j, 1].Text;//当前选中行的序号
                    string sComNum1 = fpSpread1.ActiveSheet.Cells[j, 7].Text;//当前选中行的组合号
                    if (sComNum1 == sComNum)
                    {
                        if (sortNum1 != sortNum)
                        {
                            m += 1;
                        }
                    }
                }
            }

            if (m >= 1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region 列设置

        /// <summary>
        /// 列设置
        /// </summary>
        protected enum ColEnum
        {
            /// <summary>
            /// 组号
            /// </summary>
            [FS.FrameWork.Public.Description("组号")]
            SubComNo,

            /// <summary>
            /// 护士备注
            /// </summary>
            [FS.FrameWork.Public.Description("!")]
            ColNurMemo,

            /// <summary>
            /// 顺序号
            /// </summary>
            [FS.FrameWork.Public.Description("顺序号")]
            ColSort,//  updated by zlw 2006-4-18

            /// <summary>
            /// 重整
            /// </summary>
            [FS.FrameWork.Public.Description("重整")]
            ColInforming,

            /// <summary>
            /// 医嘱类型代码 期效 0 长嘱
            /// </summary>
            [FS.FrameWork.Public.Description("期效")]
            ColOrderTypeID,

            /// <summary>
            /// 医嘱类型
            /// </summary>
            [FS.FrameWork.Public.Description("医嘱类型")]
            ColOrderTypeName,

            /// <summary>
            /// 医嘱流水号
            /// </summary>
            [FS.FrameWork.Public.Description("医嘱流水号")]
            ColOrderID,

            /// <summary>
            /// 医嘱状态
            /// </summary>
            [FS.FrameWork.Public.Description("医嘱状态")]
            ColOrderState,

            /// <summary>
            /// 组合号
            /// </summary>
            [FS.FrameWork.Public.Description("组合号")]
            ColComboNo,

            /// <summary>
            /// 主药
            /// </summary>
            [FS.FrameWork.Public.Description("主药")]
            ColMainDrug,

            /// <summary>
            /// 开始时间
            /// </summary>
            [FS.FrameWork.Public.Description("开始时间")]
            ColOrderBgn,

            /// <summary>
            /// 医嘱名称
            /// </summary>
            [FS.FrameWork.Public.Description("医嘱名称")]
            ColItemName,

            /// <summary>
            /// 组标记
            /// </summary>
            [FS.FrameWork.Public.Description("组")]
            ColComboFlag,
            /// <summary>
            /// 备注
            /// </summary>
            [FS.FrameWork.Public.Description("备注")]
            ColMemo,

            /// <summary>
            /// 首日量
            /// </summary>
            [FS.FrameWork.Public.Description("首日量")]
            ColFirstDayQty,

            /// <summary>
            /// 总量
            /// </summary>
            [FS.FrameWork.Public.Description("数量")]
            ColQty,

            /// <summary>
            /// 总量单位
            /// </summary>
            [FS.FrameWork.Public.Description("单位")]
            ColUnit,

            /// <summary>
            /// 每次量
            /// </summary>
            [FS.FrameWork.Public.Description("每次量")]
            ColDoseOnce,

            /// <summary>
            /// 单位
            /// </summary>
            [FS.FrameWork.Public.Description("每次量单位")]
            ColDoseUnit,

            /// <summary>
            /// 付数
            /// </summary>
            [FS.FrameWork.Public.Description("草药付数")]
            ColHerbalQty,

            /// <summary>
            /// 频次编码
            /// </summary>
            [FS.FrameWork.Public.Description("频次编码")]
            ColFrequencyID,

            /// <summary>
            /// 频次
            /// </summary>
            [FS.FrameWork.Public.Description("频次")]
            ColFrequencyName,

            /// <summary>
            /// 用法编码
            /// </summary>
            [FS.FrameWork.Public.Description("用法编码")]
            ColUsageID,

            /// <summary>
            /// 用法
            /// </summary>
            [FS.FrameWork.Public.Description("用法")]
            ColUsageName,

            /// <summary>
            /// 大类
            /// </summary>
            [FS.FrameWork.Public.Description("大类")]
            ColSysType,

            /// <summary>
            /// 系统类别
            /// </summary>
            [FS.FrameWork.Public.Description("系统类别")]
            SysClassName,

            /// <summary>
            /// 停止时间
            /// </summary>
            [FS.FrameWork.Public.Description("停止时间")]
            ColOrderEnd,

            /// <summary>
            /// 开立医生
            /// </summary>
            [FS.FrameWork.Public.Description("开立医生")]
            ColDoc,

            /// <summary>
            /// 执行科室编码
            /// </summary>
            [FS.FrameWork.Public.Description("执行科室编码")]
            ColExeDeptID,

            /// <summary>
            /// 执行科室
            /// </summary>
            [FS.FrameWork.Public.Description("执行科室")]
            ColExeDeptName,

            /// <summary>
            /// 病区名称
            /// //{D642A3BA-C93C-4e03-A5AF-FF878D7D9833}
            /// </summary>
            [FS.FrameWork.Public.Description("病区")]
            ColNurseCellName,

            /// <summary>
            /// 加急
            /// </summary>
            [FS.FrameWork.Public.Description("加急")]
            ColEmEmergency,

            /// <summary>
            /// 检查部位
            /// </summary>
            [FS.FrameWork.Public.Description("检查部位")]
            ColCheckPart,

            /// <summary>
            /// 样本类型
            /// </summary>
            [FS.FrameWork.Public.Description("样本类型")]
            ColSample,

            /// <summary>
            /// 扣库科室编码
            /// </summary>
            [FS.FrameWork.Public.Description("扣库科室编码")]
            ColStockDeptID,

            /// <summary>
            /// 扣库科室
            /// </summary>
            [FS.FrameWork.Public.Description("扣库科室")]
            ColStockDeptName,

            /// <summary>
            /// 录入人编码
            /// </summary>
            [FS.FrameWork.Public.Description("录入人编码")]
            ColUseRecID,

            /// <summary>
            /// 录入人
            /// </summary>
            [FS.FrameWork.Public.Description("录入人")]
            ColUseRecName,

            /// <summary>
            /// 开立科室
            /// </summary>
            [FS.FrameWork.Public.Description("开立科室")]
            ColRecDept,

            /// <summary>
            /// 开立时间
            /// </summary>
            [FS.FrameWork.Public.Description("开立时间")]
            ColRecDate,

            /// <summary>
            /// 审核人编码
            /// </summary>
            [FS.FrameWork.Public.Description("审核人编码")]
            ColConfirmID,

            /// <summary>
            /// 审核人名称
            /// </summary>
            [FS.FrameWork.Public.Description("审核人")]
            ColConfirmName,

            /// <summary>
            /// 停止人编码
            /// </summary>
            [FS.FrameWork.Public.Description("停止人编码")]
            ColDCOperID,

            /// <summary>
            /// 停止人
            /// </summary>
            [FS.FrameWork.Public.Description("停止人")]
            ColDCOperName,

            /// <summary>
            /// 皮试标志
            /// </summary>
            [FS.FrameWork.Public.Description("皮试标志")]
            ColHypoTest,

            /// <summary>
            /// 附材标志
            /// </summary>
            [FS.FrameWork.Public.Description("附材标志")]
            ColSubtbl,

            /// <summary>
            /// 拼音码
            /// </summary>
            [FS.FrameWork.Public.Description("拼音码")]
            SpellCode,

            /// <summary>
            /// 五笔码
            /// </summary>
            [FS.FrameWork.Public.Description("五笔码")]
            WbCode,
            
            /// <summary>
            /// 项目编码
            /// </summary>
            [FS.FrameWork.Public.Description("项目编码")]
            ItemCode
        }
        #endregion

        #region 重写
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //初始化farpoint
            dsAllLong = this.InitDataSet();
            dsAllShort = this.InitDataSet();

            //sheet0 ==长期 sheet1 ==临时
            this.fpSpread1.Sheets[0].DataSource = dsAllLong.Tables[0];
            this.fpSpread1.Sheets[1].DataSource = dsAllShort.Tables[0];

            this.fpSpread1.Sheets[0].DataAutoSizeColumns = false;
            this.fpSpread1.Sheets[1].DataAutoSizeColumns = false;

            this.fpSpread1.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating;
            this.fpSpread1.SheetTabClick += new FarPoint.Win.Spread.SheetTabClickEventHandler(fpSpread1_SheetTabClick);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);

            #region 定义单击事件{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellClick);
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);
            #endregion
            DateTime dt = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            this.InitFP();

            try
            {
                this.fpSpread1.ActiveSheetIndex = 0;


                if (isShowAll)
                {
                    this.cmbOderStatus.SelectedIndex = 0;//默认全部的医嘱
                }
                else
                {
                    this.cmbOderStatus.SelectedIndex = 1;//默认选有效的医嘱
                }
            }
            catch { }

            #region 附材管理窗口
            ucSubtblManager1 = new ucSubtblManager();
            //皮试药{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
            ucTip = new ucTip();
            ucTip.IsCanModifyHypotest = true;
            this.ucTip.OKEvent += new myTipEvent(ucTip_OKEvent);
            this.DockingManager();
            this.ucSubtblManager1.ShowSubtblFlag += new ucSubtblManager.ShowSubtblFlagEvent(ucSubtblManager1_ShowSubtblFlag);
            #endregion
            if (this.tv != null)
            {
                this.tv.CheckBoxes = false;
                this.tv.ExpandAll();
            }
            return base.OnInit(sender, neuObject, param);
        }

        void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.fpSpread2.Sheets[0].Rows.Remove(0, this.fpSpread2.Sheets[0].Rows.Count);
            this.fpSpread2.Sheets[1].Rows.Remove(0, this.fpSpread2.Sheets[1].Rows.Count);
            this.QueryOrderFee();

            ShowPactItem();
        }

        /// <summary>
        /// 显示项目信息
        /// </summary>
        /// <returns></returns>
        private int ShowPactItem()
        {
            try
            {
                string orderID = this.fpSpread1.ActiveSheet.Cells[this.fpSpread1.ActiveSheet.ActiveRowIndex, (int)ColEnum.ColOrderID].Text;

                if (orderID.Length <= 0)
                {
                    return -1;
                }

                FS.HISFC.Models.Order.Inpatient.Order inOrder = CacheManager.InOrderMgr.QueryOneOrder(orderID);

                if (inOrder == null || inOrder.ID.Length <= 0)
                {
                    return -1;
                }

                #region 显示项目信息

                txtItemInfo.ReadOnly = true;

                string showInfo = "";

                //项目信息
                if (inOrder.Item.ID == "999")
                {
                    showInfo += inOrder.Item.Name + " 【规格】" + inOrder.Item.Specs + " 【单价】" + inOrder.Item.Price.ToString() + "元/" + inOrder.Item.PriceUnit;
                }
                else
                {
                    if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " 【规格】" + inOrder.Item.Specs + " 【单价】" + inOrder.Item.Price.ToString() + "元/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).PackUnit;
                       
                        if (!string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual))
                        {
                            showInfo += "\r\n" + "【药品说明】" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual;
                        }
                    }
                    else
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " 【规格】" + inOrder.Item.Specs + " 【单价】" + inOrder.Item.Price.ToString() + "元/" + inOrder.Item.PriceUnit;
                    }
                }
                if (inOrder.Item.ID != "999")
                {
                    if (myPatientInfo != null && string.IsNullOrEmpty(myPatientInfo.ID))
                    {
                        FS.HISFC.Models.SIInterface.Compare compare = Classes.Function.GetPactItem(inOrder);
                        inOrder.Patient.Pact = this.myPatientInfo.Pact;
                        if (compare != null)
                        {
                            //医保对照信息
                            showInfo += "\r\n【" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(inOrder.Patient.Pact.ID).Name + "】 " + Classes.Function.GetItemGrade(compare.CenterItem.ItemGrade) + " " + (compare.CenterItem.Rate > 0 ? compare.CenterItem.Rate.ToString("p0") : "") + (compare.CenterFlag == "1" ? "【需审批】" : "");



                            //医保限制用药信息
                            if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                            {
                                showInfo += "\r\n【" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(inOrder.Patient.Pact.ID).Name + "】 " + compare.Practicablesymptomdepiction;
                            }
                        }
                    }

                    //项目内涵 暂无

                    //套餐明细
                    if (inOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID);
                        if (undrug.UnitFlag == "1")
                        {
                            showInfo += "\r\n【套餐包含】：";

                            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(inOrder.Item.ID);
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb comb in alZt)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(comb.ID);
                                showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "]") + " " + comb.Qty + combUndrug.PriceUnit + "；";
                            }
                        }
                    }


                    //附材信息
                    FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
                    ArrayList alSub = subMgr.GetSubtblInfoByItem("1", inOrder.ReciptDept.ID, inOrder.Item.ID, inOrder.Usage.ID);
                    if (alSub != null && alSub.Count > 0)
                    {
                        showInfo += "\r\n【附材带出】(供参考)：";
                        foreach (FS.HISFC.Models.Order.OrderSubtblNew sub in alSub)
                        {
                            FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(sub.Item.ID);
                            showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "] ") + "；";
                        }
                    }
                }

                txtItemInfo.Text = showInfo;

                #endregion

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (this.tv.CheckBoxes == true)
                this.tv.CheckBoxes = false;
            this.tv.ExpandAll();
            this.PatientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            return base.OnSetValue(neuObject, e);
        }
        #endregion

        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.isAutoSaveColumnProperty)
            {
                if (this.fpSpread1.ActiveSheetIndex == 0)
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
                else
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);
            }
        }

        /// <summary>
        /// 已重整医嘱查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReOrderQueryed_Click(object sender, EventArgs e)
        {
            try
            {
                this.fpSpread1.Sheets[0].RowCount = 0;
                this.fpSpread1.Sheets[1].RowCount = 0;
                this.dsAllLong.Tables[0].Rows.Clear();
                this.dsAllShort.Tables[0].Rows.Clear();
            }
            catch { }
            if (this.myPatientInfo == null)
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询已重整医嘱,请稍候!");
            Application.DoEvents();

            if (this.ucSubtblManager1 != null)
            {
                this.ucSubtblManager1.PatientInfo = this.myPatientInfo;
            }

            //查询所有医嘱类型
            ArrayList alAllOrder = CacheManager.InOrderMgr.QueryOrder(this.myPatientInfo.ID);
            if (alAllOrder == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            //查询所有医嘱附材
            ArrayList alSub = CacheManager.InOrderMgr.QueryOrderSubtbl(this.myPatientInfo.ID);
            if (alSub == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }

            try
            {
                dsAllLong.Tables[0].Clear();
                dsAllShort.Tables[0].Clear();
                alAllOrder.AddRange(alSub);

                ArrayList al = new ArrayList();

                //屏蔽显示重整医嘱					
                foreach (FS.HISFC.Models.Order.Order info in alAllOrder)
                {
                    if (info.Status == 4)
                        al.Add(info);
                }


                this.AddObjectsToTable(al);
                dvLong = new DataView(dsAllLong.Tables[0]);
                dvShort = new DataView(dsAllShort.Tables[0]);

                try
                {
                    dvLong.Sort = "顺序号 ASC , 组合号 ASC";
                    dvShort.Sort = "顺序号 ASC , 组合号 ASC";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("对显示医嘱根据顺序号、组合号排序发生错误" + ex.Message);
                    return;
                }

                this.fpSpread1.Sheets[0].DataSource = dvLong;
                this.fpSpread1.Sheets[1].DataSource = dvShort;


                this.InitFP();

                this.fpSpread1.Sheets[0].Columns[0, this.fpSpread1.Sheets[0].Columns.Count - 1].Locked = true;
                this.fpSpread1.Sheets[1].Columns[0, this.fpSpread1.Sheets[0].Columns.Count - 1].Locked = true;

            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
                return;
            }

            this.Filter(this.cmbOderStatus.SelectedIndex);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void cmbOderStatus_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            this.Filter(this.cmbOderStatus.SelectedIndex);
            this.fpSpread1.Focus();
        }

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.fpSpread1.ActiveSheetIndex == 0)
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
            else
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);

            MessageBox.Show("格式保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkFee_CheckedChanged(object sender, EventArgs e)
        {
            this.panel3.Visible = this.chkFee.Checked;
        }

        private void fpSpread2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenu contextMenu1 = new ContextMenu();
                MenuItem mnuPrint = new MenuItem();
                mnuPrint.Click += new EventHandler(mnuPrint_Click);
                mnuPrint.Text = "退费申请";

                contextMenu1.MenuItems.Add(mnuPrint);

                contextMenu1.Show(this.fpSpread2, new Point(e.X, e.Y));
            }
        }

        void mnuPrint_Click(object sender, EventArgs e)
        {
            ArrayList alExecs = new ArrayList();

            string orderID = this.fpSpread1.ActiveSheet.Cells[this.fpSpread1.ActiveSheet.ActiveRowIndex, (int)ColEnum.ColOrderID].Text;

            if (orderID.Length <= 0)
            {
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order tmpOrder = CacheManager.InOrderMgr.QueryOneOrder(orderID);

            if (tmpOrder == null || tmpOrder.ID.Length <= 0)
                return;

            string itemType = "1";

            if (tmpOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                itemType = "1";
            }
            else
            {
                itemType = "2";
            }

            for (int i = 0; i < this.fpSpread2_Sheet1.Rows.Count; i++)
            {
                if (this.fpSpread2_Sheet1.Cells[i, 7].Text == "True" &&
                    FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread2_Sheet1.Cells[i, 3].Text) > 0)
                {
                    FS.HISFC.Models.Order.ExecOrder execOrd = CacheManager.InOrderMgr.QueryExecOrderByExecOrderID(
                        this.fpSpread2_Sheet1.Cells[i, 6].Text, itemType);

                    alExecs.Add(execOrd);
                }
            }


            if (alExecs.Count <= 0)
            {
                MessageBox.Show("没有需要退费申请的项目！");
                return;
            }

            string msg = "";

            int iRet = CacheManager.FeeIntegrate.SaveApply(alExecs, this.autoQuitFeeApply, ref msg);

            if (iRet > 0)
            {
                MessageBox.Show("退费申请成功！");
            }
            else
            {
                MessageBox.Show("退费申请失败！" + CacheManager.FeeIntegrate.Err);
            }
        }

        private void fpSpread2_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpSpread2.ActiveSheetIndex == 0)
            {
                if (this.fpSpread2_Sheet2.ActiveRowIndex < 0)
                    return;

                string time = this.fpSpread2_Sheet2.Cells[this.fpSpread2_Sheet2.ActiveRowIndex, 0].Text;

                DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(time);

                DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 01);
                DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);

                (this.fpSpread2_Sheet1.DataSource as DataView).RowFilter =
                    "收费时间>=" + "#" + dt1 + "#" + " and 收费时间<=" + "#" + dt2 + "#"; ;

                this.fpSpread2_Sheet1.Columns.Get(7).CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

                for (int i = 0; i < this.fpSpread2_Sheet1.Rows.Count; i++)
                {

                    string execID = this.fpSpread2_Sheet1.Cells[i, 6].Text;

                    string orderID = this.fpSpread1.ActiveSheet.Cells[this.fpSpread1.ActiveSheet.ActiveRowIndex, (int)ColEnum.ColOrderID].Text;

                    if (orderID.Length <= 0)
                    {
                        return;
                    }

                    FS.HISFC.Models.Order.Inpatient.Order tmpOrder = CacheManager.InOrderMgr.QueryOneOrder(orderID);

                    ArrayList feeItemListTempArray = CacheManager.InPatientFeeMgr.GetItemListByExecSQN(this.PatientInfo.ID, execID, tmpOrder.Item.ItemType);

                    if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                    {
                        this.fpSpread2_Sheet1.Cells[i, 7].Locked = true;
                        this.fpSpread2_Sheet1.Rows[i].BackColor = Color.Pink;
                    }
                    else
                    {
                        this.fpSpread2_Sheet1.Cells[i, 7].Locked = false;
                        this.fpSpread2_Sheet1.Rows[i].BackColor = Color.White;
                    }

                    if (feeItemListTempArray != null)
                    {
                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList itemlist in feeItemListTempArray)
                        {
                            if (itemlist.NoBackQty <= 0 || itemlist.Item.Qty <= 0)
                            {
                                this.fpSpread2_Sheet1.Cells[i, 7].Locked = true;
                                this.fpSpread2_Sheet1.Rows[i].BackColor = Color.Pink;
                            }
                            else
                            {
                                this.fpSpread2_Sheet1.Cells[i, 7].Locked = false;
                                this.fpSpread2_Sheet1.Rows[i].BackColor = Color.White;
                            }
                        }
                    }
                }
            }
        }

        //FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        private void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread2.ActiveSheetIndex == 0)
            {
                if (this.fpSpread2_Sheet2.ActiveRowIndex < 0)
                    return;

                string time = this.fpSpread2_Sheet2.Cells[this.fpSpread2_Sheet2.ActiveRowIndex, 0].Text;

                DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(time);

                DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 01);
                DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);

                (this.fpSpread2_Sheet1.DataSource as DataView).RowFilter =
                    "收费时间>=" + "#" + dt1 + "#" + " and 收费时间<=" + "#" + dt2 + "#"; ;

                this.fpSpread2_Sheet1.Columns.Get(7).CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

                this.fpSpread2.ActiveSheetIndex = 1;

                for (int i = 0; i < this.fpSpread2_Sheet1.Rows.Count; i++)
                {

                    string execID = this.fpSpread2_Sheet1.Cells[i, 6].Text;

                    string orderID = this.fpSpread1.ActiveSheet.Cells[this.fpSpread1.ActiveSheet.ActiveRowIndex, (int)ColEnum.ColOrderID].Text;

                    if (orderID.Length <= 0)
                    {
                        return;
                    }

                    FS.HISFC.Models.Order.Inpatient.Order tmpOrder = CacheManager.InOrderMgr.QueryOneOrder(orderID);

                    ArrayList feeItemListTempArray = CacheManager.InPatientFeeMgr.GetItemListByExecSQN(this.PatientInfo.ID, execID, tmpOrder.Item.ItemType);

                    if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                    {
                        this.fpSpread2_Sheet1.Cells[i, 7].Locked = true;
                        this.fpSpread2_Sheet1.Rows[i].BackColor = Color.Pink;
                    }
                    else
                    {
                        this.fpSpread2_Sheet1.Cells[i, 7].Locked = false;
                        this.fpSpread2_Sheet1.Rows[i].BackColor = Color.White;
                    }

                    if (feeItemListTempArray != null)
                    {

                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList itemlist in feeItemListTempArray)
                        {
                            if (itemlist.NoBackQty <= 0 || itemlist.Item.Qty <= 0)
                            {
                                this.fpSpread2_Sheet1.Cells[i, 7].Locked = true;
                                this.fpSpread2_Sheet1.Rows[i].BackColor = Color.Pink;
                            }
                            else
                            {
                                this.fpSpread2_Sheet1.Cells[i, 7].Locked = false;
                                this.fpSpread2_Sheet1.Rows[i].BackColor = Color.White;
                            }
                        }
                    }
                }
            }
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {
            this.fpSpread1.Focus();
        }

        /// <summary>
        /// 点击时变换背景颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Sheet_Clicked(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //这个原来是为了核对医嘱的功能....
            if (!this.isCheckOrder)
            {
                return;
            }

            if (this.fpSpread1.ActiveSheet.Rows[e.Row].BackColor == Color.SpringGreen)
            {
                this.fpSpread1.ActiveSheet.Rows[e.Row].BackColor = Color.White;
                return;
            }
            this.fpSpread1.ActiveSheet.Rows[e.Row].BackColor = Color.SpringGreen;
        }
        // {15CDA661-3D42-4c15-A32B-F88CC1CD7907}
        private void btOrderPrint_Click(object sender, EventArgs e)
        {
            if (this.IPrintOrderInstance == null)
            {
                this.IPrintOrderInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.Controls.ucOrder), typeof(FS.HISFC.BizProcess.Interface.IPrintOrder)) as FS.HISFC.BizProcess.Interface.IPrintOrder;
            }
            if (IPrintOrderInstance == null)
            {
                MessageBox.Show("医嘱单打印接口未实现！");
                return;
            }

            try
            {
                IPrintOrderInstance.SetPatient(myPatientInfo);
                IPrintOrderInstance.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}