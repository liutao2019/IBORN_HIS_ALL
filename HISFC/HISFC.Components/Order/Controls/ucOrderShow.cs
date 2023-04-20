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
    /// [��������: ҽ����ѯ�ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2007-1-17]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
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
        private DataSet dataSet = null;							//��ǰDataSet
        private DataView dvLong = null;							//��ǰDataView
        private DataView dvShort = null;						//��ǰDataView
        private string LONGSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "LongOrderQuerySetting.xml";
        private string SHORTSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "ShortOrderQuerySetting.xml";
        /// <summary>
        /// ���鷽��ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST IRecipePrintST = null;
        private int sheetIndex = 0;			//��ǰ�Sheetҳ����
        ucSubtblManager ucSubtblManager1 = null;//����ά��

        /// <summary>
        /// ҽ������ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintOrder IPrintOrderInstance = null;

        /// <summary>
        /// �Ƿ��Զ����� ��ʽ�ļ�
        /// </summary>
        private bool isAutoSaveColumnProperty = true;

        /// <summary>
        /// �Ƿ��Զ����� ��ʽ�ļ�
        /// </summary>
        [Description("�Ƿ��Զ����� ��ʽ�ļ�"), Category("�ؼ�����")]
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
        /// �Ƿ�Ĭ����ʾȫ��ҽ��
        /// </summary>
        private bool isShowAll = true;

        /// <summary>
        /// �Ƿ�Ĭ����ʾȫ��ҽ��
        /// </summary>
        [Description("�Ƿ�Ĭ����ʾȫ��ҽ��"), Category("�ؼ�����")]
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
        /// �Ƿ���ʾ�˶�ҽ����ɫ�������˹��ܣ����������ɫ��ʾ
        /// </summary>
        private bool isCheckOrder = false;

        /// <summary>
        /// �Ƿ���ʾ�˶�ҽ����ɫ�������˹��ܣ����������ɫ��ʾ
        /// </summary>
        [Category("��ʾ����"), Description("�Ƿ���ʾ�˶�ҽ����ɫ�������˹��ܣ����������ɫ��ʾ")]
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
        /// Ƥ��ҩ��ע
        /// </summary>
        ucTip ucTip = null;
        float[] longColumnWidth;
        float[] shortColumnWidth;
        ArrayList alQueryLong = new ArrayList();
        ArrayList alQueryShort = new ArrayList();
        //{2ED0FB9F-8CC3-4eca-81BB-DE13DAA6F9FD}
        bool value = true;

        /// <summary>
        /// ��ʼ��
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


        #region ����
        FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;
        /// <summary>
        /// ���߻�����Ϣ
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
        /// ����Ա��Ϣ
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

        #region �������� �����Ƿ��۵�������ʾ{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
        /// <summary>
        /// �Ƿ��۵�������ʾ
        /// </summary>
        private bool isFoldSubtblShow = false;

        /// <summary>
        /// �Ƿ��۵�������ʾ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��۵�������ʾ")]
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
        /// �Ƿ���ʾ���˹���
        /// </summary>
        public bool IsShowFilter
        {
            set
            {
                this.cmbOderStatus.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾֹͣ/����ҽ��
        /// </summary>
        private bool showDCOrder = true;
        /// <summary>
        /// �Ƿ���ʾֹͣ/����ҽ��
        /// </summary>
        public bool IsShowDCOrder
        {
            set
            {
                this.showDCOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ������������
        /// </summary>
        protected bool enableSubtbl = true;
        /// <summary>
        /// �Ƿ������������ �����ڻ�ʿվ�ۺ��շ�ʱ��ѯҽ��ʱ�������������
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
        /// �˷������Ƿ��Զ�ȷ��
        /// </summary>
        [Category("�ؼ�����"), Description("�˷������Ƿ��Զ�ȷ��")]
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

        #region ��ʼ��
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
        /// ��ȡ�����ļ�
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

            this.fpSpread1.Sheets[0].Columns[(Int32)ColEnum.ColNurMemo].Visible = false;//����ע����ʾ
            this.fpSpread1.Sheets[1].Columns[(Int32)ColEnum.ColNurMemo].Visible = false;//����ע����ʾ

            //���Ǹ�ʲô��
            this.fpSpread1.Sheets[0].Columns[21].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[0].Columns[20].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[1].Columns[21].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[1].Columns[20].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

        }

        private FarPoint.Win.Spread.CellType.DateTimeCellType dateCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();

        /// <summary>
        /// ��ʼ��������ʾ
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
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState),dtStr),				//4 �¿�������ˣ�ִ��
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo),dtStr),					//5
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColMainDrug),dtStr),					//6
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderBgn),dtStr),
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColItemName),dtStr),				//8
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboFlag),dtStr),					    //9
					new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColMemo),dtStr),					//20
                    new DataColumn(FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColFirstDayQty),dtStr),  //�״��� houwb 2011-3-12
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

        #region ��ʾҽ��

        /// <summary>
        /// ���ʵ��toTable
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

            Components.Order.OutPatient.Classes.LogManager.Write("��ʼ��ӵ�������ʾ" + DateTime.Now.ToString());
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in list)
            {
                if (order == null)
                    continue;

                if (!this.showDCOrder)
                {
                    if (order.Status == 3)		//����ʾ����/ֹͣҽ��
                        continue;
                }

                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)//����ҽ��
                {
                    dsAllLong.Tables[0].Rows.Add(AddObjectToRow(order, dsAllLong.Tables[0]));
                    alQueryLong.Add(order.Item.Name);
                }
                else//��ʱҽ��
                {
                    dsAllShort.Tables[0].Rows.Add(AddObjectToRow(order, dsAllShort.Tables[0]));
                    alQueryShort.Add(order.Item.Name);
                }


                Components.Order.OutPatient.Classes.LogManager.Write("��ӡ�" + order.Item.Name + "��" + DateTime.Now.ToString());
            }
            Components.Order.OutPatient.Classes.LogManager.Write("������ӵ�������ʾ" + DateTime.Now.ToString());

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

            this.lbPatient.Text = myPatientInfo.PID.PatientNO//סԺ��
                        + "  " + this.myPatientInfo.Name //����
                        + "  " + this.myPatientInfo.Sex.Name //�Ա�
                        + "  " + CacheManager.InOrderMgr.GetAge(this.myPatientInfo.Birthday)//����
                        + "  " + this.myPatientInfo.Pact.Name//��ͬ��λ
                        + "  סԺҽʦ��" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(myPatientInfo.PVisit.AdmittingDoctor.ID)
                //+ "\r\n"
                        + "  סԺ���ڣ�" + myPatientInfo.PVisit.InTime.ToString("yyyy.MM.dd") + "-" + CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy.MM.dd") + "/" + CacheManager.RadtIntegrate.GetInDays(myPatientInfo.ID).ToString() + "��"//סԺ����
                        + "\r\n"
                        + "�ܷ���: " + myPatientInfo.FT.TotCost.ToString()
                        + "  Ԥ����: " + this.myPatientInfo.FT.PrepayCost.ToString()
                        + "  �Էѣ�" + myPatientInfo.FT.OwnCost.ToString()
                        + "  ������" + myPatientInfo.FT.PubCost.ToString()
                        + "  ���: " + this.myPatientInfo.FT.LeftCost.ToString();
        }

        /// <summary>
        /// ��ӵ�datarow
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

                //ֻ�в�ҩ����ʾ����
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
            row[(Int32)ColEnum.ColOrderState] = order.Status;										//12 �¿�������ˣ�ִ��
            row[(Int32)ColEnum.ColComboNo] = order.Combo.ID;	//5

            //ϵͳ�����ʾ
            row[(Int32)ColEnum.SysClassName] = order.Item.SysClass.Name;

            #region ��Ŀ������ʾ

            //�Ա������б��  ���ڻ�ʿ��ӡ���ݺ�ҽ������ʾ����
            string byoStr = "";

            if (!order.OrderType.IsCharge || order.Item.ID == "999")
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    byoStr = "[�Ա�]";
                }
                else
                {
                    byoStr = "[����]";
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

            //��������ʾ houwb 2011-3-12
            row[(Int32)ColEnum.ColFirstDayQty] = order.FirstUseNum;

            //ҽ����ҩ
            if (order.IsPermission) row[(Int32)ColEnum.ColItemName] = "��" + row[(Int32)ColEnum.ColItemName];

            //����ҽ��������ʾΪÿ���� houwb  2011-3-12
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
                strTemp = "��";
            }
            else
            {
                strTemp = "��";
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

            #region addby xuewj {B8EDA745-62C3-407e-9480-3A9E60647141} δֹͣ��ҽ�� ֹͣʱ�䲻��ʾ
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

            //Ƥ��
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
        /// ��ʾ������Ϣ
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

                #region ������ܷ���

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
        /// ��ѯҽ��
        /// </summary>
        private void QueryOrder()
        {
            try
            {
                Components.Order.OutPatient.Classes.LogManager.Write("��ʼ��ѯ" + DateTime.Now.ToString());
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
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯҽ��,���Ժ�!");
            Application.DoEvents();

            if (this.ucSubtblManager1 != null)
            {
                this.ucSubtblManager1.PatientInfo = this.myPatientInfo;
            }

            Components.Order.OutPatient.Classes.LogManager.Write("��ʼ��ѯҽ���͸���" + DateTime.Now.ToString());
            //��ѯ����ҽ������
            ArrayList alAllOrder = CacheManager.InOrderMgr.QueryOrder(this.myPatientInfo.ID);
            if (alAllOrder == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            //��ѯ����ҽ������
            ArrayList alSub = CacheManager.InOrderMgr.QueryOrderSubtbl(this.myPatientInfo.ID);
            if (alSub == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            Components.Order.OutPatient.Classes.LogManager.Write("������ѯҽ���͸���" + DateTime.Now.ToString());

            #region ����Ϻ���Ӹ��ı�ʾ����ֹ����Ϻ�ʱ����һ��
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

                //������ʾ����ҽ��					
                foreach (FS.HISFC.Models.Order.Order info in alAllOrder)
                {
                    //if (info.Status != 4)
                    al.Add(info);
                }

                Components.Order.OutPatient.Classes.LogManager.Write("��ӵ�������ʾ" + DateTime.Now.ToString());

                this.AddObjectsToTable(al);
                dvLong = new DataView(dsAllLong.Tables[0]);
                dvShort = new DataView(dsAllShort.Tables[0]);

                //{EACD8AED-FDF6-490a-980C-EC9A89391719} ��ʾǰ�Ƚ����������
                try
                {
                    dvLong.Sort = FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSort) + " ASC , " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo) + " ASC";
                    dvShort.Sort = FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSort) + " ASC , " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo) + " ASC";

                    Components.Order.OutPatient.Classes.LogManager.Write("��������" + DateTime.Now.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����ʾҽ������" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColSort) + "��" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColComboNo) + "����������" + ex.Message);
                    return;
                }

                this.fpSpread1.Sheets[0].DataSource = dvLong;
                this.fpSpread1.Sheets[1].DataSource = dvShort;


                Components.Order.OutPatient.Classes.LogManager.Write("��ʼ����ʾ" + DateTime.Now.ToString());

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

            Components.Order.OutPatient.Classes.LogManager.Write("����" + DateTime.Now.ToString());
            this.Filter(this.cmbOderStatus.SelectedIndex);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            Components.Order.OutPatient.Classes.LogManager.Write("������ѯ" + DateTime.Now.ToString() + "\r\n\r\n");
        }


        ///<summary>
        /// ˢ�����
        /// </summary>
        public void RefreshCombo()
        {
            Classes.Function.DrawCombo(this.fpSpread1.Sheets[0], (int)ColEnum.ColComboNo, (int)ColEnum.ColComboFlag);

            Classes.Function.DrawCombo(this.fpSpread1.Sheets[1], (int)ColEnum.ColComboNo, (int)ColEnum.ColComboFlag);

            this.SetSortID();
        }

        /// <summary>
        /// ����ҽ��״̬
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
        /// ˢ��ҽ��״̬
        /// </summary>
        /// <param name="row"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="reset"></param>
        private void ChangeOrderState(int row, int SheetIndex, bool reset)
        {
            try
            {
                int i = (int)ColEnum.ColOrderState;//"ҽ��״̬";
                int j = (int)ColEnum.ColSort;//˳������ڵ���

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
        /// ���ñ�ע��Ƥ��
        /// </summary>
        /// <param name="k"></param>
        private void SetTip(int k)
        {
            for (int i = 0; i < this.fpSpread1.Sheets[k].RowCount; i++)//��ע
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

        #region IToolBar ��Ա

        public int Retrieve()
        {
            // TODO:  ��� ucOrderShow.Retrieve ʵ��
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
                MessageBox.Show("ͬһ������Ҫ��ͬ,�����Ӱ�������ʾ!");
            }

            return 0;
        }


        #endregion

        #region ����

        /// <summary>
        /// ����ҽ����ʾ
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

            //��ѯʱ����ܹ���
            switch (State)
            {
                case 0://ȫ��
                    rowFilter += "";
                    break;
                case 2://����
                    DateTime dt = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                    DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                    DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                    //rowFilter+= " (��ʼʱ��>=" + "#" + dt1 + "#" + " and ��ʼʱ��<=" + "#" + dt2 + "#" + " ) and  ���ı�־ = true";
                    rowFilter += "and (" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " ='1' or " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '2')"
                        + " and (" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderBgn) + ">=" + "#" + dt1 + "#" + " and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderBgn) + "<=" + "#" + dt2 + "#)";
                    break;
                case 1://��Ч
                    rowFilter += "and (" + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " ='1' or " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '2')";
                    break;
                case 5://��Ч
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '3'";
                    break;
                case 3://δ���
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '0'";
                    break;
                case 4://��������
                    DateTime d = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                    DateTime d1 = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                    DateTime d2 = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderEnd) + ">=" + "#" + d1 + "#" + " and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderEnd) + "<=" + "#" + d2 + "#" + " and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '3'";
                    break;
                case 6://δ���
                    rowFilter += "and " + FS.FrameWork.Public.EnumHelper.Current.GetName(ColEnum.ColOrderState) + " = '4'";//������
                    //Ƥ��ҽ��//{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
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
            #region �۵���ʾ����{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
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

        #region ��������
        public Crownwood.Magic.Docking.DockingManager dockingManager;
        /// <summary>
        /// ���Ĺ���ؼ�
        /// </summary>
        private Crownwood.Magic.Docking.Content content;

        /// <summary>
        /// Ƥ��ҩ�ؼ�
        /// </summary>
        private Crownwood.Magic.Docking.Content hypoTestContent;

        private Crownwood.Magic.Docking.WindowContent wc;

        private Crownwood.Magic.Docking.WindowContent wc1;

        public void DockingManager()
        {
            this.dockingManager = new Crownwood.Magic.Docking.DockingManager(this, Crownwood.Magic.Common.VisualStyle.IDE);
            this.dockingManager.InnerControl = this.panel1;		//��InnerControlǰ����Ŀؼ�����ͣ�����ڵ�Ӱ��

            content = new Crownwood.Magic.Docking.Content(this.dockingManager);
            content.Control = ucSubtblManager1;

            Size ucSize = content.Control.Size;

            content.Title = "���Ĺ���";
            content.FullTitle = "���Ĺ���";
            content.AutoHideSize = ucSize;
            content.DisplaySize = ucSize;

            //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
            this.hypoTestContent = new Crownwood.Magic.Docking.Content(this.dockingManager);
            this.hypoTestContent.Control = ucTip;

            Size ucTipSize = this.hypoTestContent.Control.Size;
            this.hypoTestContent.Title = "Ƥ��ҩ����";
            this.hypoTestContent.FullTitle = "Ƥ��ҩ����";
            this.hypoTestContent.AutoHideSize = ucTipSize;
            this.hypoTestContent.DisplaySize = ucTipSize;

            this.dockingManager.Contents.Add(this.hypoTestContent);
            this.dockingManager.Contents.Add(content);

        }
        #endregion

        #region ��ʼ��

        /// <summary>
        /// �����ⲿ����ʱ��ʼ������
        /// </summary>
        /// <returns></returns>
        public int InitForOut()
        {
            //��ʼ��farpoint
            dsAllLong = this.InitDataSet();
            dsAllShort = this.InitDataSet();

            //sheet0 ==���� sheet1 ==��ʱ
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
                    this.cmbOderStatus.SelectedIndex = 0;//Ĭ��ȫ����ҽ��
                }
                else
                {
                    this.cmbOderStatus.SelectedIndex = 1;//Ĭ��ѡ��Ч��ҽ��
                }
            }
            catch
            {
                return -1;
            }

            #region ���Ĺ�����
            ucSubtblManager1 = new ucSubtblManager();
            //Ƥ��ҩ{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
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
        /// �����¼� չ������{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                return; // ����б����з���
            }
            if (!e.RowHeader)
            {
                return;//ֻ�е���б���ż�������
            }
            if (!this.IsFoldSubtblShow)
            {
                return;
            }

            string subtblFlag = e.View.Sheets[e.View.ActiveSheetIndex].RowHeader.Cells[e.Row, 0].Text; //���ı�ʶ
            string comboNo = e.View.Sheets[e.View.ActiveSheetIndex].Cells[e.Row, (int)ColEnum.ColComboNo].Text;//��ȡ��Ϻ�
            bool isContinue = true;
            int indexDown = e.Row;

            switch (subtblFlag)
            {
                case "+"://չ��
                    while (isContinue)
                    {
                        #region ���²��� �絽���һ�л���ϺŲ�ͬ��ֹͣ
                        indexDown = indexDown + 1;
                        if (indexDown >= e.View.Sheets[e.View.ActiveSheetIndex].RowCount) //��������һ�У������в�����
                            isContinue = false;
                        else
                        {
                            if (e.View.Sheets[e.View.ActiveSheetIndex].Cells[indexDown, (int)ColEnum.ColSubtbl].Text == "True")
                            {
                                //������²������Ǹ��ģ�����ʾ
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
                case "-"://�۵�
                    while (isContinue)
                    {
                        #region ���²��� �絽���һ�л���ϺŲ�ͬ��ֹͣ
                        indexDown = indexDown + 1;
                        if (indexDown >= e.View.Sheets[e.View.ActiveSheetIndex].RowCount) //��������һ�У������в�����
                            isContinue = false;
                        else
                        {
                            if (e.View.Sheets[e.View.ActiveSheetIndex].Cells[indexDown, (int)ColEnum.ColSubtbl].Text == "True")
                            {
                                //������²������Ǹ��ģ�������
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
        #region ������ʾ
        /// <summary>
        /// ���¸�����ʾ��־
        /// </summary>
        private void RefreshSubtblFlag(string operFlag, bool isShowSubtblFlag, object sender)
        {
            if (this.fpSpread1.Sheets[this.sheetIndex].Rows.Count < 0)
                return;

            int rowIndex = this.fpSpread1.ActiveSheet.ActiveRowIndex;
            string s = this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColItemName].Text;       //ҽ������
            string comboNo = this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColComboNo].Text;	//��Ϻ�

            #region ˢ�������ʾ"@"
            //���ڲ���ͬһ���ҽ��
            int iUp = rowIndex;
            bool isUp = true;
            int iDown = rowIndex;
            bool isDown = true;

            if (!isShowSubtblFlag)	//������ʾ"@"����
            {
                while (isUp || isDown)
                {
                    #region ���ϲ��� �絽��ǰһ�л���ϺŲ�ͬ���ñ�־Ϊfalse
                    if (isUp)
                    {
                        iUp = iUp - 1;
                        if (iUp < 0)
                            isUp = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColComboNo].Text == comboNo)				//ͬһ���
                            {
                                if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text.Substring(0, 1) == "@")	//ҽ�����ƴ���"@"����
                                {
                                    this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text = this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text.Substring(1);
                                }
                            }
                            else		//����ͬһ��� �����ٲ���
                            {
                                isUp = false;
                            }
                        }
                    }
                    #endregion

                    #region ���²��� ��������һ�л���ϺŲ�ͬ���ñ�־Ϊfalse
                    if (isDown)
                    {
                        iDown = iDown + 1;
                        if (iDown >= this.fpSpread1.Sheets[this.sheetIndex].Rows.Count)
                            isDown = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColComboNo].Text == comboNo)					//ͬһ���
                            {
                                if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColItemName].Text.Substring(0, 1) == "@")	//ҽ�����ƴ���"@"����
                                {
                                    this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColItemName].Text = this.fpSpread1.Sheets[this.sheetIndex].Cells[iDown, (int)ColEnum.ColItemName].Text.Substring(1);
                                }
                            }
                            else			//����ͬһ��� �����ٲ���
                            {
                                isDown = false;
                            }
                        }
                    }
                    #endregion
                }
                //���±�����¼ҽ����־
                if (s.Substring(0, 1) == "@")
                {
                    this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColItemName].Text = s.Substring(1);
                }
            }
            else		//��Ҫ��ʾ"@"����
            {
                bool isAlreadyHave = false;			//������Ƿ��Ѱ���"@"ҽ������
                while (isUp || isDown)
                {
                    #region ���ϲ��� �絽��ǰһ�л���ϺŲ�ͬ���ñ�־Ϊfalse
                    if (isUp)
                    {
                        iUp = iUp - 1;
                        if (iUp < 0)
                            isUp = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColComboNo].Text == comboNo)					//ͬһ�����
                            {
                                if (this.fpSpread1.Sheets[this.sheetIndex].Cells[iUp, (int)ColEnum.ColItemName].Text.Substring(0, 1) == "@")		//�Ѿ�����"@"����
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

                    #region ���²��� ��������һ�л���ϺŲ�ͬ���ñ�־Ϊfalse
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
                //�������δ����"@"����
                if (!isAlreadyHave && s.Substring(0, 1) != "@")
                {
                    this.fpSpread1.Sheets[this.sheetIndex].Cells[rowIndex, (int)ColEnum.ColItemName].Text = "@" + s;
                }
            }
            #endregion

            #region �ı���渽�ĵ���ʾ ��ӻ�ɾ��
            try
            {
                if (operFlag == "2")					//ɾ��/ֹͣ����
                {
                    #region ����ɾ��/ֹͣ����ʱ�ĸ��Ľ�����ʾ
                    FS.HISFC.Models.Order.Inpatient.Order order = sender as FS.HISFC.Models.Order.Inpatient.Order;
                    if (order == null)
                    {
                        MessageBox.Show("׼��ˢ�´�����渽����ʾʱ�������� ���˳���������");
                        return;
                    }
                    if (order.ID != "")					//�ѱ��渽�� 
                    {
                        if (this.sheetIndex == 0)		//����
                        {
                            string[] tempFind = new string[1];								//Ѱ����ɾ���е�����
                            tempFind[0] = order.ID;
                            DataRow delRow = this.dsAllLong.Tables[0].Rows.Find(tempFind);	//����DataSet���Ƴ�����				
                            this.dsAllLong.Tables[0].Rows.Remove(delRow);					//�Ƴ���

                            if (order.Status != 0)											//�������/ִ�е����� ������ʾ ���ı�״̬
                            {
                                order.Status = 3;
                                //��Ӹı�״̬���� 
                                this.dsAllLong.Tables[0].Rows.Add(this.AddObjectToRow(order, this.dsAllLong.Tables[0]));
                            }
                        }
                        else							//����
                        {
                            string[] tempFind = new string[1];								//Ѱ����ɾ���е�����
                            tempFind[0] = order.ID;
                            DataRow delRow = this.dsAllShort.Tables[0].Rows.Find(tempFind);//����DataSet���Ƴ�����	
                            this.dsAllShort.Tables[0].Rows.Remove(delRow);					//�Ƴ���

                            if (order.Status != 0)											//�������/ִ�е����� ������ʾ ���ı�״̬
                            {
                                order.Status = 3;
                                //��Ӹı�״̬���� 
                                this.dsAllShort.Tables[0].Rows.Add(this.AddObjectToRow(order, this.dsAllShort.Tables[0]));
                            }
                        }
                        //���������Ϣˢ��
                        this.Filter(this.cmbOderStatus.SelectedIndex);

                    }
                    #endregion
                }
                else									//�������
                {
                    if (this.ucSubtblManager1.AddSubInfo != null && this.ucSubtblManager1.AddSubInfo.Count > 0)
                    {
                        this.AddObjectsToTable(this.ucSubtblManager1.AddSubInfo);			//��DataSet�ڼ���������
                        //���������Ϣˢ��

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
                MessageBox.Show("�޷��������ͬ���ĸ��ģ������޸ĸ�������");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������ʾʱ��������Ԥ֪���� ���˳���������" + ex.Message);
                return;
            }
            #endregion

            this.RefreshSubtblDisplay(this.sheetIndex);
        }

        /// <summary>
        /// ˢ�¸�����ʾ �Ը�����ʾб����
        /// </summary>
        /// <param name="sheetIndex"></param>
        private void RefreshSubtblDisplay(int sheetIndex)
        {
            for (int i = 0; i < this.fpSpread1.Sheets[sheetIndex].RowCount; i++)
            {
                string temp = this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColSubtbl].Text;

                if (temp == "True")
                {
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColItemName].Font = new Font("����", 8.5f, System.Drawing.FontStyle.Italic);
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, 9].Locked = true;

                }
                else
                {
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColItemName].Font = new Font("����", 10, System.Drawing.FontStyle.Bold);
                    this.fpSpread1.Sheets[sheetIndex].Cells[i, 9].Locked = true;
                }
            }

        }

        /// <summary>
        /// �۵�������ʾ {32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
        /// </summary>
        /// <param name="sheetIndex"></param>
        private void FoldSubtblDisplay(int sheetIndex)
        {
            for (int i = 0; i < this.fpSpread1.Sheets[sheetIndex].RowCount; i++)
            {
                string temp = this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColSubtbl].Text;

                if (temp == "True")
                {
                    //�Ǹ��Ĳ���
                    this.fpSpread1.Sheets[sheetIndex].Rows[i].Visible = false; //������ʾ
                    string comboNo = this.fpSpread1.Sheets[sheetIndex].Cells[i, (int)ColEnum.ColComboNo].Text;
                    bool isContinue = true;
                    int indexUp = i;
                    while (isContinue)
                    {
                        #region ���ϲ��� �絽��ǰһ�л���ϺŲ�ͬ��ֹͣ
                        indexUp = indexUp - 1;
                        if (indexUp < 0) //�������ǰһ�У������в�����
                            isContinue = false;
                        else
                        {
                            if (this.fpSpread1.Sheets[sheetIndex].Cells[indexUp, (int)ColEnum.ColSubtbl].Text == "True")
                            {
                                //������ϲ������Ǹ��ģ��򲻽��д����������ϲ���
                                continue;
                            }
                            if (this.fpSpread1.Sheets[sheetIndex].Cells[indexUp, (int)ColEnum.ColComboNo].Text + "@" == comboNo)				//ͬһ���
                            {
                                //this.fpSpread1.Sheets[sheetIndex].RowHeader.Cells[indexUp, 0].Text = "+"; 
                                isContinue = false;
                            }
                            else		//����ͬһ��� �����ٲ���
                            {
                                isContinue = false;
                            }
                        }
                        #endregion
                        //��ʱ���������²���
                    }
                }
                else
                {
                    this.fpSpread1.Sheets[sheetIndex].Rows[i].Visible = true; //�Ǹ���ҽ��������ʾ
                }
            }
        }
        #endregion


        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            this.sheetIndex = e.SheetTabIndex;
            this.ucSubtblManager1.Clear();
            if (this.sheetIndex == 0)//����
            {
                //����Ĭ����ʾ��Чҽ��--ȡ�� by huangchw 2012-09-08
                //this.cmbOderStatus.SelectedIndex = 1;

                //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
                if (this.dockingManager != null)
                {
                    this.dockingManager.HideContent(this.hypoTestContent);
                }
            }

            if (this.sheetIndex == 1)//����
            {
                //����Ĭ����ʾ����ҽ��--ȡ�� by huangchw 2012-09-08
                //this.cmbOderStatus.SelectedIndex = 2;

                //{07B60769-DFBE-4797-823D-3C07ACD737B4}
                //��ʱҽ������ʾ���Ľ���
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
            //�жϵ�ǰ��ͣ�������Ƿ�����ʾ ��δ��ʾ ����ʾͣ������
            try
            {
                //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
                //Ƥ��ҩ��ʾ
                //��ȡƤ��ҩ���
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
                //��ʱҽ������ʾ���Ľ���
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
                    if (this.ucSubtblManager1 != null && !e.RowHeader && !e.ColumnHeader)		//������б������б���
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
        /// Ƥ��ҩ����
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
                        MessageBox.Show("Ƥ�Խ����¼����ʧ�ܣ�" + allergyManager.Err);
                    }
                }
            }
            else if (Hypotest == 4)
            {
                allergyInfo.ValidState = false;
                if (allergyManager.UpdateAllergyInfo(allergyInfo) < 0)
                {
                    MessageBox.Show("Ƥ�Խ����¼����ʧ�ܣ�" + allergyManager.Err);
                }
            }
        }

        #region �Ҽ��˵�
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

                //ToolStripMenuItem mnuSetTime = new ToolStripMenuItem("ִ��ʱ��");
                //mnuSetTime.Click += new EventHandler(mnuSetTime_Click);
                //�Ҽ���ʾƤ�Ժ���עhouwb {D822412B-07F4-4ed8-A749-E6EC16019461}
                ToolStripMenuItem menuTip = new ToolStripMenuItem("��ע/Ƥ��");
                menuTip.Click += new EventHandler(menuTip_Click);

                //this.contextMenuStrip1.Items.Add(mnuSetTime);
                this.contextMenuStrip1.Items.Add(menuTip);

                #region ���鴦����ӡ //{A5FD9B35-B074-4720-9281-5ABF4D10AD18}

                ToolStripMenuItem mnuOrderST = new ToolStripMenuItem();//���鴦����ӡ 
                mnuOrderST.Click += new EventHandler(mnuOrderST_Click);
                mnuOrderST.Text = "���鴦����ӡ";
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
        /// Ƥ�Ժ���ע
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuTip_Click(object sender, EventArgs e)
        {
            //�Ҽ���ʾƤ�Ժ���עhouwb {D822412B-07F4-4ed8-A749-E6EC16019461}
            int iHypotest = CacheManager.InOrderMgr.QueryOrderHypotest(this.orderId);
            if (iHypotest == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }

            //��ҩƷҽ������ʾƤ��ҳ
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
        /// ���鷽��ӡ
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
                    //��ʱ����дһ�£����濴���Ǽӽӿڻ�����ô����
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
                    MessageBox.Show("��ҽ���Ǿ��鴦����");
                    return;
                }
            }
        }

        /// <summary>
        /// ��ע�¼�
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
            ////������Ϣ������
            //FS.Common.Class.Message.SendMessage(p.Name + "�е�ҽ��������<" + Tip + ">��Ҫ���ġ�", p.PVisit.PatientLocation.Dept.ID, "22222");
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

        #region  ҽ��
        /// <summary>
        /// ͨ��ҽ��״̬����ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOderStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Filter(this.cmbOderStatus.SelectedIndex);
            this.fpSpread1.Focus();
        }
        /// <summary>
        /// ����ҽ�����
        /// </summary>
        private void UpdateOrderSortID()
        {
            int colorderid = (int)ColEnum.ColOrderID;    //ҽ����ˮ��    
            int sortid = (int)ColEnum.ColSort;
            string OrderID = null;//ҽ�����
            string SortID = null; //˳���
            FarPoint.Win.Spread.SheetView sv = fpSpread1.ActiveSheet;//ȡ�õ�ǰ��������Ч��SHEET
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڸ���ҽ�����...");
            Application.DoEvents();
            for (int i = 0; i < sv.Rows.Count; i++) //����ҽ��
            {
                OrderID = sv.Cells[i, colorderid].Text;//ҽ�����
                SortID = sv.Cells[i, sortid].Text; //˳����
                int Sortid = 0;
                if (sv.Cells[i, 2].Text.ToUpper() == "TRUE")
                {

                    Sortid = Convert.ToInt32(SortID) - 10000;

                    SortID = Sortid.ToString();
                }
                #region ҽ����Ÿ���
                if (CacheManager.InOrderMgr.UpdateOrderSortID(OrderID, SortID) == -1)
                {
                    MessageBox.Show("���´���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;

                }

                #endregion
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }


        /// <summary>
        /// ��Ҫʱ�ж��ǲ��Ǻ�����ͬ��Ϻţ���Ų�ͬ��ҽ��
        /// </summary>
        /// <returns></returns>
        private int ifHaveNotSameCom()
        {
            int m = 0;
            for (int i = 0; i < fpSpread1.ActiveSheet.RowCount; i++)
            {
                string sortNum = fpSpread1.ActiveSheet.Cells[i, 1].Text;//��ǰѡ���е����
                string sComNum = fpSpread1.ActiveSheet.Cells[i, 7].Text;//��ǰѡ���е���Ϻ�
                for (int j = 0; j < fpSpread1.ActiveSheet.RowCount; j++)
                {
                    string sortNum1 = fpSpread1.ActiveSheet.Cells[j, 1].Text;//��ǰѡ���е����
                    string sComNum1 = fpSpread1.ActiveSheet.Cells[j, 7].Text;//��ǰѡ���е���Ϻ�
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

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        protected enum ColEnum
        {
            /// <summary>
            /// ���
            /// </summary>
            [FS.FrameWork.Public.Description("���")]
            SubComNo,

            /// <summary>
            /// ��ʿ��ע
            /// </summary>
            [FS.FrameWork.Public.Description("!")]
            ColNurMemo,

            /// <summary>
            /// ˳���
            /// </summary>
            [FS.FrameWork.Public.Description("˳���")]
            ColSort,//  updated by zlw 2006-4-18

            /// <summary>
            /// ����
            /// </summary>
            [FS.FrameWork.Public.Description("����")]
            ColInforming,

            /// <summary>
            /// ҽ�����ʹ��� ��Ч 0 ����
            /// </summary>
            [FS.FrameWork.Public.Description("��Ч")]
            ColOrderTypeID,

            /// <summary>
            /// ҽ������
            /// </summary>
            [FS.FrameWork.Public.Description("ҽ������")]
            ColOrderTypeName,

            /// <summary>
            /// ҽ����ˮ��
            /// </summary>
            [FS.FrameWork.Public.Description("ҽ����ˮ��")]
            ColOrderID,

            /// <summary>
            /// ҽ��״̬
            /// </summary>
            [FS.FrameWork.Public.Description("ҽ��״̬")]
            ColOrderState,

            /// <summary>
            /// ��Ϻ�
            /// </summary>
            [FS.FrameWork.Public.Description("��Ϻ�")]
            ColComboNo,

            /// <summary>
            /// ��ҩ
            /// </summary>
            [FS.FrameWork.Public.Description("��ҩ")]
            ColMainDrug,

            /// <summary>
            /// ��ʼʱ��
            /// </summary>
            [FS.FrameWork.Public.Description("��ʼʱ��")]
            ColOrderBgn,

            /// <summary>
            /// ҽ������
            /// </summary>
            [FS.FrameWork.Public.Description("ҽ������")]
            ColItemName,

            /// <summary>
            /// ����
            /// </summary>
            [FS.FrameWork.Public.Description("��")]
            ColComboFlag,
            /// <summary>
            /// ��ע
            /// </summary>
            [FS.FrameWork.Public.Description("��ע")]
            ColMemo,

            /// <summary>
            /// ������
            /// </summary>
            [FS.FrameWork.Public.Description("������")]
            ColFirstDayQty,

            /// <summary>
            /// ����
            /// </summary>
            [FS.FrameWork.Public.Description("����")]
            ColQty,

            /// <summary>
            /// ������λ
            /// </summary>
            [FS.FrameWork.Public.Description("��λ")]
            ColUnit,

            /// <summary>
            /// ÿ����
            /// </summary>
            [FS.FrameWork.Public.Description("ÿ����")]
            ColDoseOnce,

            /// <summary>
            /// ��λ
            /// </summary>
            [FS.FrameWork.Public.Description("ÿ������λ")]
            ColDoseUnit,

            /// <summary>
            /// ����
            /// </summary>
            [FS.FrameWork.Public.Description("��ҩ����")]
            ColHerbalQty,

            /// <summary>
            /// Ƶ�α���
            /// </summary>
            [FS.FrameWork.Public.Description("Ƶ�α���")]
            ColFrequencyID,

            /// <summary>
            /// Ƶ��
            /// </summary>
            [FS.FrameWork.Public.Description("Ƶ��")]
            ColFrequencyName,

            /// <summary>
            /// �÷�����
            /// </summary>
            [FS.FrameWork.Public.Description("�÷�����")]
            ColUsageID,

            /// <summary>
            /// �÷�
            /// </summary>
            [FS.FrameWork.Public.Description("�÷�")]
            ColUsageName,

            /// <summary>
            /// ����
            /// </summary>
            [FS.FrameWork.Public.Description("����")]
            ColSysType,

            /// <summary>
            /// ϵͳ���
            /// </summary>
            [FS.FrameWork.Public.Description("ϵͳ���")]
            SysClassName,

            /// <summary>
            /// ֹͣʱ��
            /// </summary>
            [FS.FrameWork.Public.Description("ֹͣʱ��")]
            ColOrderEnd,

            /// <summary>
            /// ����ҽ��
            /// </summary>
            [FS.FrameWork.Public.Description("����ҽ��")]
            ColDoc,

            /// <summary>
            /// ִ�п��ұ���
            /// </summary>
            [FS.FrameWork.Public.Description("ִ�п��ұ���")]
            ColExeDeptID,

            /// <summary>
            /// ִ�п���
            /// </summary>
            [FS.FrameWork.Public.Description("ִ�п���")]
            ColExeDeptName,

            /// <summary>
            /// ��������
            /// //{D642A3BA-C93C-4e03-A5AF-FF878D7D9833}
            /// </summary>
            [FS.FrameWork.Public.Description("����")]
            ColNurseCellName,

            /// <summary>
            /// �Ӽ�
            /// </summary>
            [FS.FrameWork.Public.Description("�Ӽ�")]
            ColEmEmergency,

            /// <summary>
            /// ��鲿λ
            /// </summary>
            [FS.FrameWork.Public.Description("��鲿λ")]
            ColCheckPart,

            /// <summary>
            /// ��������
            /// </summary>
            [FS.FrameWork.Public.Description("��������")]
            ColSample,

            /// <summary>
            /// �ۿ���ұ���
            /// </summary>
            [FS.FrameWork.Public.Description("�ۿ���ұ���")]
            ColStockDeptID,

            /// <summary>
            /// �ۿ����
            /// </summary>
            [FS.FrameWork.Public.Description("�ۿ����")]
            ColStockDeptName,

            /// <summary>
            /// ¼���˱���
            /// </summary>
            [FS.FrameWork.Public.Description("¼���˱���")]
            ColUseRecID,

            /// <summary>
            /// ¼����
            /// </summary>
            [FS.FrameWork.Public.Description("¼����")]
            ColUseRecName,

            /// <summary>
            /// ��������
            /// </summary>
            [FS.FrameWork.Public.Description("��������")]
            ColRecDept,

            /// <summary>
            /// ����ʱ��
            /// </summary>
            [FS.FrameWork.Public.Description("����ʱ��")]
            ColRecDate,

            /// <summary>
            /// ����˱���
            /// </summary>
            [FS.FrameWork.Public.Description("����˱���")]
            ColConfirmID,

            /// <summary>
            /// ���������
            /// </summary>
            [FS.FrameWork.Public.Description("�����")]
            ColConfirmName,

            /// <summary>
            /// ֹͣ�˱���
            /// </summary>
            [FS.FrameWork.Public.Description("ֹͣ�˱���")]
            ColDCOperID,

            /// <summary>
            /// ֹͣ��
            /// </summary>
            [FS.FrameWork.Public.Description("ֹͣ��")]
            ColDCOperName,

            /// <summary>
            /// Ƥ�Ա�־
            /// </summary>
            [FS.FrameWork.Public.Description("Ƥ�Ա�־")]
            ColHypoTest,

            /// <summary>
            /// ���ı�־
            /// </summary>
            [FS.FrameWork.Public.Description("���ı�־")]
            ColSubtbl,

            /// <summary>
            /// ƴ����
            /// </summary>
            [FS.FrameWork.Public.Description("ƴ����")]
            SpellCode,

            /// <summary>
            /// �����
            /// </summary>
            [FS.FrameWork.Public.Description("�����")]
            WbCode,
            
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            [FS.FrameWork.Public.Description("��Ŀ����")]
            ItemCode
        }
        #endregion

        #region ��д
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //��ʼ��farpoint
            dsAllLong = this.InitDataSet();
            dsAllShort = this.InitDataSet();

            //sheet0 ==���� sheet1 ==��ʱ
            this.fpSpread1.Sheets[0].DataSource = dsAllLong.Tables[0];
            this.fpSpread1.Sheets[1].DataSource = dsAllShort.Tables[0];

            this.fpSpread1.Sheets[0].DataAutoSizeColumns = false;
            this.fpSpread1.Sheets[1].DataAutoSizeColumns = false;

            this.fpSpread1.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating;
            this.fpSpread1.SheetTabClick += new FarPoint.Win.Spread.SheetTabClickEventHandler(fpSpread1_SheetTabClick);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);

            #region ���嵥���¼�{32992D5E-FE8C-47d9-BBCB-8B46E7CF2CD7} by zhang.xs 2010-11-7
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
                    this.cmbOderStatus.SelectedIndex = 0;//Ĭ��ȫ����ҽ��
                }
                else
                {
                    this.cmbOderStatus.SelectedIndex = 1;//Ĭ��ѡ��Ч��ҽ��
                }
            }
            catch { }

            #region ���Ĺ�����
            ucSubtblManager1 = new ucSubtblManager();
            //Ƥ��ҩ{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
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
        /// ��ʾ��Ŀ��Ϣ
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

                #region ��ʾ��Ŀ��Ϣ

                txtItemInfo.ReadOnly = true;

                string showInfo = "";

                //��Ŀ��Ϣ
                if (inOrder.Item.ID == "999")
                {
                    showInfo += inOrder.Item.Name + " �����" + inOrder.Item.Specs + " �����ۡ�" + inOrder.Item.Price.ToString() + "Ԫ/" + inOrder.Item.PriceUnit;
                }
                else
                {
                    if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " �����" + inOrder.Item.Specs + " �����ۡ�" + inOrder.Item.Price.ToString() + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).PackUnit;
                       
                        if (!string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual))
                        {
                            showInfo += "\r\n" + "��ҩƷ˵����" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).Product.Manual;
                        }
                    }
                    else
                    {
                        showInfo += SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID).UserCode + " " + inOrder.Item.Name + " �����" + inOrder.Item.Specs + " �����ۡ�" + inOrder.Item.Price.ToString() + "Ԫ/" + inOrder.Item.PriceUnit;
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
                            //ҽ��������Ϣ
                            showInfo += "\r\n��" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(inOrder.Patient.Pact.ID).Name + "�� " + Classes.Function.GetItemGrade(compare.CenterItem.ItemGrade) + " " + (compare.CenterItem.Rate > 0 ? compare.CenterItem.Rate.ToString("p0") : "") + (compare.CenterFlag == "1" ? "����������" : "");



                            //ҽ��������ҩ��Ϣ
                            if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                            {
                                showInfo += "\r\n��" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(inOrder.Patient.Pact.ID).Name + "�� " + compare.Practicablesymptomdepiction;
                            }
                        }
                    }

                    //��Ŀ�ں� ����

                    //�ײ���ϸ
                    if (inOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Item.ID);
                        if (undrug.UnitFlag == "1")
                        {
                            showInfo += "\r\n���ײͰ�������";

                            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(inOrder.Item.ID);
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb comb in alZt)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(comb.ID);
                                showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "]") + " " + comb.Qty + combUndrug.PriceUnit + "��";
                            }
                        }
                    }


                    //������Ϣ
                    FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
                    ArrayList alSub = subMgr.GetSubtblInfoByItem("1", inOrder.ReciptDept.ID, inOrder.Item.ID, inOrder.Usage.ID);
                    if (alSub != null && alSub.Count > 0)
                    {
                        showInfo += "\r\n�����Ĵ�����(���ο�)��";
                        foreach (FS.HISFC.Models.Order.OrderSubtblNew sub in alSub)
                        {
                            FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(sub.Item.ID);
                            showInfo += combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "] ") + "��";
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
        /// ������ҽ����ѯ
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
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ������ҽ��,���Ժ�!");
            Application.DoEvents();

            if (this.ucSubtblManager1 != null)
            {
                this.ucSubtblManager1.PatientInfo = this.myPatientInfo;
            }

            //��ѯ����ҽ������
            ArrayList alAllOrder = CacheManager.InOrderMgr.QueryOrder(this.myPatientInfo.ID);
            if (alAllOrder == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            //��ѯ����ҽ������
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

                //������ʾ����ҽ��					
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
                    dvLong.Sort = "˳��� ASC , ��Ϻ� ASC";
                    dvShort.Sort = "˳��� ASC , ��Ϻ� ASC";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����ʾҽ������˳��š���Ϻ�����������" + ex.Message);
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

            MessageBox.Show("��ʽ����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                mnuPrint.Text = "�˷�����";

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
                MessageBox.Show("û����Ҫ�˷��������Ŀ��");
                return;
            }

            string msg = "";

            int iRet = CacheManager.FeeIntegrate.SaveApply(alExecs, this.autoQuitFeeApply, ref msg);

            if (iRet > 0)
            {
                MessageBox.Show("�˷�����ɹ���");
            }
            else
            {
                MessageBox.Show("�˷�����ʧ�ܣ�" + CacheManager.FeeIntegrate.Err);
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
                    "�շ�ʱ��>=" + "#" + dt1 + "#" + " and �շ�ʱ��<=" + "#" + dt2 + "#"; ;

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
                    "�շ�ʱ��>=" + "#" + dt1 + "#" + " and �շ�ʱ��<=" + "#" + dt2 + "#"; ;

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
        /// ���ʱ�任������ɫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Sheet_Clicked(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //���ԭ����Ϊ�˺˶�ҽ���Ĺ���....
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
                MessageBox.Show("ҽ������ӡ�ӿ�δʵ�֣�");
                return;
            }

            try
            {
                IPrintOrderInstance.SetPatient(myPatientInfo);
                IPrintOrderInstance.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}