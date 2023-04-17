using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.Report.Example
{
    /// <summary>
    /// ucPrivePowerReport<br></br>
    /// [��������: ucPrivePowerReport����Ȩ�ޱ���]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2008-9-6]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDayReportSummary : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayReportSummary()
        {
            InitializeComponent();
        }
        
        #region ����

        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager priPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        public FS.FrameWork.WinForms.Classes.Print PrintObject = new FS.FrameWork.WinForms.Classes.Print();

        public delegate void DelegateQueryEnd();
        public DelegateQueryEnd QueryEndHandler;

        public delegate void DelegateOperationStart(string type);
        public DelegateOperationStart OperationStartHandler;

        public delegate void DelegateOperateionEnd(string type);
        public DelegateOperateionEnd OperationEndHandler;

        public delegate void DelegateDoubleClickEnd();
        public DelegateDoubleClickEnd DetailDoubleClickEnd;

        /// <summary>
        /// uc���
        /// </summary>
        public int MaxWidth = 820;

        /// <summary>
        /// �Ƿ���Ҫ�ϼ�[Tot������ͣ�Det��ϸ��ͣ�Both��ϸ����Ҫ��ͣ�Null�������]
        /// </summary>
        private string sumType = "Null";
        
        /// <summary>
        /// �Ƿ���Ҫƽ����[Tot������ͣ�Det��ϸ��ͣ�Both��ϸ����Ҫ��ͣ�Null�������]
        /// </summary>
        private string avgType = "Null";

        private string formulaType = "Null";

        /// <summary>
        /// ���Ҳ��ɼ�ʱ�Ƿ���ҪȨ��
        /// ����Ȩ�޸�ֵ���Զ�ֵΪtrue;
        /// </summary>
        private bool isJugdePriPower = false;
        
        /// <summary>
        /// ����Ȩ�޳�ʼ������ݲ���ԱȨ���Ƿ���Բ�ѯ����
        /// </summary>
        private bool isHavePriPower = true;

        /// <summary>
        /// SQL
        /// </summary>
        public string SQL = "";

        #endregion

        #region ���Լ���ر���

        #region ����
        /// <summary>
        /// ���ܲ�ѯ������
        /// </summary>
        private System.Data.DataView dataView;

        /// <summary>
        /// ��ϸ��ѯ������
        /// </summary>
        private System.Data.DataView detailDataView;

        /// <summary>
        /// ���ܲ�ѯ������
        /// </summary>
        public System.Data.DataView DataView
        {
            set
            {
                dataView = value;
            }
            get
            {
                return this.dataView;
            }
        }

        /// <summary>
        /// ��ϸ��ѯ������
        /// </summary>
        public System.Data.DataView DetailDataView
        {
            set
            {
                detailDataView = value;
            }
            get
            {
                return detailDataView;
            }
        }

        /// <summary>
        /// ���ܹ���
        /// </summary>
        private string filters = "";

        /// <summary>
        /// ���ܹ���
        /// </summary>
        [Description("�����ַ������������ϸ��ѯ�����ǻ��ܹ���"), Category("Filter����"), Browsable(true)]
        public string Filters
        {
            get
            {
                return filters;
            }
            set
            {
                filters = value;
            }
        }

        /// <summary>
        /// ��ϸ����
        /// </summary>
        private string detailFilters = "";

        /// <summary>
        /// ��ϸ����
        /// </summary>
        [Description("�����ַ�������ϸ����"), Category("Filter����"), Browsable(true)]
        public string DetailFilters
        {
            get
            {
                return detailFilters;
            }
            set
            {
                detailFilters = value;
            }
        }

        /// <summary>
        /// ���˷�ʽ[Tot���˻��� Det������ϸ Both���߹��� Null������]
        /// </summary>
        EnumFilterType filerType = EnumFilterType.������;

        /// <summary>
        /// ���˷�ʽ[Tot���˻��� Det������ϸ Both���߹��� Null������]
        /// </summary>
        [Description("���˷�ʽ"), Category("Filter����"), Browsable(true), DefaultValue(EnumFilterType.������)]
        public EnumFilterType FilerType
        {
            get
            {
                return filerType;
            }
            set
            {
                filerType = value;               
            }
        }

        /// <summary>
        /// �Ƿ�س�����������
        /// </summary>
        bool isFilterAfterEnterKey = false;

        /// <summary>
        /// �Ƿ�س�����������
        /// </summary>
        [Description("�Ƿ�س�����������"), Category("Filter����"), Browsable(true)]
        public bool FilterAfterEnterKey
        {
            get { return isFilterAfterEnterKey; }
            set { isFilterAfterEnterKey = value; }
        }
        #endregion

        #region ����Ȩ��
        /// <summary>
        /// ����Ȩ���ַ���
        /// </summary>
        private string priveClassTwos = "";

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Description("����Ȩ�ޱ��룬��ֵ�󽫼�����ԱȨ��"), Category("Prive����Ȩ��"), Browsable(true)]
        public string PriveClassTwos
        {
            get 
            {
                return priveClassTwos;
            }
            set
            {
                priveClassTwos = value;
                if (!string.IsNullOrEmpty(value))
                {
                    isJugdePriPower = true;
                }
            }
        }

        #endregion

        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        private string mainTitle = "������";

        /// <summary>
        /// ����
        /// ���û�и��ӱ��⣬�����
        /// </summary>
        [Description("������"), Category("Title��������"), Browsable(true)]
        public string MainTitle
        {
            get
            {
                return mainTitle;
            }
            set
            {
                mainTitle = value;
            }
        }

        /// <summary>
        /// ������ռ�߶�
        /// </summary>
        private int mainTitleHeight = 47;

        /// <summary>
        /// ������ռ�߶�
        /// </summary>
        [Description("������ռ�߶�"), Category("Title��������"), Browsable(true)]
        public int MainTitleHeight
        {
            get 
            {
                return mainTitleHeight; 
            }
            set 
            { mainTitleHeight = value;
            }
        }
        /// <summary>
        /// �����������С
        /// </summary>
        private float mainTitleFontSize = 14F;

        /// <summary>
        /// �����������С
        /// </summary>
        [Description("�����������С"), Category("Title��������"), Browsable(true)]
        public float MainTitleFontSize
        {
            get
            {
                return mainTitleFontSize;
            }
            set
            {
                mainTitleFontSize = value;
            }
        }

        /// <summary>
        /// ��������������
        /// </summary>
        private FontStyle mainTitleFontStyle = FontStyle.Bold;

        /// <summary>
        /// ��������������
        /// </summary>
        [Description("��������������"), Category("Title��������"), Browsable(true)]
        public FontStyle MainTitleFontStyle
        {
            get { return mainTitleFontStyle; }
            set { mainTitleFontStyle = value; }
        }

        /// <summary>
        /// ���ӱ���ռ�߶�
        /// </summary>
        private int addtionTitleHeight = 20;

        /// <summary>
        /// ���ӱ���ռ�߶�
        /// </summary>
        [Description("���ӱ���ռ�߶�"), Category("Title��������"), Browsable(true)]
        public int AddtionTitleHeight
        {
            get
            {
                return addtionTitleHeight;
            }
            set
            {
                addtionTitleHeight = value;
            }
        }
        /// <summary>
        /// ���ӱ��������С
        /// </summary>
        private float addtionTitleFontSize = 9F;

        /// <summary>
        /// ���ӱ��������С
        /// </summary>
        [Description("���ӱ��������С"), Category("Title��������"), Browsable(true)]
        public float AddtionTitleFontSize
        {
            get
            {
                return addtionTitleFontSize;
            }
            set
            {
                addtionTitleFontSize = value;
            }
        }

        /// <summary>
        /// ��������������
        /// </summary>
        private FontStyle addtionTitleFontStyle = FontStyle.Bold;

        /// <summary>
        /// ��������������
        /// </summary>
        [Description("���ӱ�����������"), Category("Title��������"), Browsable(true)]
        public FontStyle AddtionTitleFontStyle
        {
            get { return addtionTitleFontStyle; }
            set { addtionTitleFontStyle = value; }
        }

        /// <summary>
        /// ���ӱ���[��]
        /// </summary>
        private string leftAdditionTitle = "ͳ��ʱ��";

        /// <summary>
        /// ���ӱ���[��]
        /// </summary>
        [Description("���ӱ���[��]"), Category("Title��������"), Browsable(true)]
        public string LeftAdditionTitle
        {
            get
            {
                return leftAdditionTitle;
            }
            set
            {
                leftAdditionTitle = value;
            }
        }

        /// <summary>
        /// ���ӱ���[��]
        /// </summary>
        private string midAdditionTitle = "����";

        /// <summary>
        /// ���ӱ���[��]
        /// </summary>
        [Description("���ӱ���[��]"), Category("Title��������"), Browsable(true)]
        public string MidAdditionTitle
        {
            get
            {
                return midAdditionTitle;
            }
            set
            {
                midAdditionTitle = value;
            }
        }

        /// <summary>
        /// ���ӱ���[��]
        /// </summary>
        private string rightAdditionTitle = "���ӱ���[��]";

        /// <summary>
        /// ���ӱ���[��]
        /// </summary>
        [Description("���ӱ���[��]"), Category("Title��������"), Browsable(true)]
        public string RightAdditionTitle
        {
            get
            {
                return rightAdditionTitle;
            }
            set
            {
                rightAdditionTitle = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ���ӱ���
        /// </summary>
        private bool isNeedAdditionTitle = true;

        /// <summary>
        /// �Ƿ���Ҫ���ӱ���
        /// </summary>
        [Description("�Ƿ���Ҫ���ӱ���"), Category("Title��������"), Browsable(true), DefaultValue(true)]
        public bool IsNeedAdditionTitle
        {
            get
            {
                return isNeedAdditionTitle;
            }
            set
            {
                isNeedAdditionTitle = value;               
            }
        }
        #endregion

        #region ʱ������
        /// <summary>
        /// ʱ��������
        /// </summary>
        private int daysSpan = 0;

        /// <summary>
        /// ʱ��������
        /// </summary>
        [Description("��ʼʱ��ͽ���ʱ��������"), Category("Timeʱ������"), Browsable(true), DefaultValue(0)]
        public int DaySpan
        {
            get
            {
                return daysSpan;
            }
            set
            {
                this.daysSpan = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ��ʱ������Ϊ����
        /// </summary>
        private bool isNeedIntedDays = true;

        /// <summary>
        /// �Ƿ���Ҫ��ʱ������Ϊ����
        /// </summary>
        [Description("�Ƿ���Ҫ��ʱ�����ó�����"), Category("Timeʱ������"), Browsable(true), DefaultValue(true)]
        public bool IsNeedIntedDays
        {
            get
            {
                return this.isNeedIntedDays;
            }
            set
            {
                this.isNeedIntedDays = value;
            }
        }
       
        /// <summary>
        /// �Ƿ�ʱ����Ϊ��ѯ����
        /// </summary>
        private bool isTimeAsCondition = true;
        
        /// <summary>
        /// �Ƿ�ʱ����Ϊ��ѯ����
        /// </summary>
        [Description("�Ƿ���Ҫ��ʱ����Ϊ��ѯ����"), Category("Timeʱ������"), Browsable(true), DefaultValue(true)]
        public bool IsTimeAsCondition
        {
            get
            {
                return isTimeAsCondition;
            }
            set
            {
                isTimeAsCondition = value;
            }
        }
       
        #endregion

        #region ��������
        /// <summary>
        /// �Ƿ񽫿�����Ϊ��ѯ����
        /// </summary>
        private bool isDeptAsCondition = true;

        /// <summary>
        /// �Ƿ񽫿�����Ϊ��ѯ����
        /// </summary>
        [Description("�Ƿ���Ҫ��������Ϊ��ѯ����"), Category("Dept��������"), Browsable(true), DefaultValue(false)]
        public bool IsDeptAsCondition
        {
            get
            {
                return isDeptAsCondition;
            }
            set
            {
                isDeptAsCondition = value;
            }
        }

        /// <summary>
        /// �������Ƿ�����á�ȫ������ѯ����Ȩ�޿�������
        /// </summary>
        private bool isAllowAllDept = false;

        /// <summary>
        /// �������Ƿ�����á�ȫ������ѯ����Ȩ�޿�������
        /// </summary>
        [Description("�������Ƿ�����á�ȫ������ѯ����Ȩ�޿���"), Category("Dept��������"), Browsable(true), DefaultValue(false)]
        public bool IsAllowAllDept
        {
            get 
            {
                return isAllowAllDept;
            }
            set
            {
                this.isAllowAllDept = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private FS.HISFC.Models.Base.EnumDepartmentType[] deptTypes = new FS.HISFC.Models.Base.EnumDepartmentType[12];

        /// <summary>
        /// �������
        /// ָ���������Ȩ����Ч
        /// </summary>
        [Description("�������ָ�������������Ȩ����Ч"), Category("Dept��������"), Browsable(false)]
        public FS.HISFC.Models.Base.EnumDepartmentType[] DeptTypes
        {
            get
            {
                return deptTypes;
            }
            set
            {
                this.deptTypes = value;
                if (DeptType == "PriveDept")
                {
                    this.DeptType = "CommonDept";
                }
            }

        }

        /// <summary>
        /// �������[PriveDeptָ������Ȩ�ޣ�CommonDeptָ���������AllDeptȫ������]
        /// Ĭ��0��1ָ����0��Ч��2ָ����0��1��Ч
        /// </summary>
        private string deptType = "PriveDept";

        /// <summary>
        /// �������[PriveDeptָ������Ȩ�ޣ�CommonDeptָ���������AllDeptȫ������]
        /// Ĭ��0��1ָ����0��Ч��2ָ����0��1��Ч
        /// </summary>
        [Description("�������ָ�������������Ȩ����Ч"), Category("Dept��������"), Browsable(true)]
        public string DeptType
        {
            get { return deptType; }
            set { deptType = value; }
        }

        
        #endregion

        #region ��ѯ

        /// <summary>
        /// ��ʼ��ʱ�Ƿ���Ĭ��������ѯ
        /// </summary>
        private bool queryDataWhenInit = true;

        /// <summary>
        /// ��ʼ��ʱ�Ƿ���Ĭ��������ѯ[ֻд]
        /// </summary>
        [Description("��ʼ��ʱ��ѯ����"), Category("Query��ѯ����"), Browsable(true), DefaultValue(true)]
        public bool QueryDataWhenInit
        {
            set
            {
                this.queryDataWhenInit = value;
            }
        }

        /// <summary>
        /// SQL����
        /// �������ϸ��������ǻ��ܲ�ѯsql
        /// </summary>
        private string sqlIndexs = "";

        /// <summary>
        /// SQL����
        /// �������ϸ��������ǻ��ܲ�ѯsql
        /// </summary>
        [Description("����SQL��ID"), Category("Query��ѯ����"), Browsable(true)]
        public string SQLIndexs
        {
            get
            {
                return sqlIndexs;
            }
            set
            {
                this.sqlIndexs = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ��ѯ��ϸ
        /// </summary>
        private bool isNeedDetailData = false;

        /// <summary>
        /// �Ƿ���Ҫ��ѯ��ϸ
        /// </summary>
        [Description("�Ƿ���Ҫ��ѯ��ϸ"), Category("Query��ѯ����"), Browsable(true), DefaultValue(false)]
        public bool IsNeedDetailData
        {
            get
            {
                return this.isNeedDetailData;
            }
            set
            {
                this.isNeedDetailData = value;
            }
        }

        /// <summary>
        /// SQL����
        /// ��ϸ��ѯsql
        /// </summary>
        private string detailSQLIndexs = "";

        /// <summary>
        /// SQL����
        /// ��ϸ��ѯsql
        /// </summary>
        [Description("��ϸSQL��ID"), Category("Query��ѯ����"), Browsable(true)]
        public string DetailSQLIndexs
        {
            get
            {
                return detailSQLIndexs;
            }
            set
            {
                this.detailSQLIndexs = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ��������
        /// </summary>
        private bool isNeedAdditionConditions = false;

        /// <summary>
        /// �Ƿ���Ҫ��������[ֻд]
        /// </summary>
        public bool IsNeedAdditionConditions
        {
            set
            {
                this.isNeedAdditionConditions = value;
            }
        }

        /// <summary>
        /// ��ѯ����[���ҡ���ʼʱ��][ֻ��]
        /// </summary>
        public string[] QueryConditions
        {
            get
            {
                return this.GetQueryConditions();
            }
        }

        /// <summary>
        /// ���Ӳ�ѯ����
        /// </summary>
        private string[] queryAdditionConditions;

        /// <summary>
        /// ���Ӳ�ѯ����
        /// ��Ҫ������ҡ�ʱ��
        /// </summary>
         [Browsable(false)]
        public string[] QueryAdditionConditions
        {
            set
            {
                this.queryAdditionConditions = value;
            }
        }

        /// <summary>
        /// ��ϸ���ݲ�ѯ��ʽ
        /// </summary>
        EnumQueryType detailDataQueryType;

        /// <summary>
        /// ��ϸ���ݲ�ѯ��ʽ
        /// </summary>
        [Description("��ϸ���ݲ�ѯ��ʽ"), Category("Query��ѯ����"), Browsable(true), DefaultValue(EnumQueryType.���ȡ����)]
        public EnumQueryType DetailDataQueryType
        {
            get
            {
                return this.detailDataQueryType;
            }
            set
            {
                detailDataQueryType = value;
            }
        }

        /// <summary>
        /// ��ϸ��ѯʱ��Ϊ������FarPint������
        /// </summary>
        private string queryConditionColIndexs = "";

        /// <summary>
        /// ��ϸ��ѯʱ��Ϊ������FarPint������
        /// </summary>
        [Description("��ϸ��ѯʱ��Ϊ������FarPint������"), Category("Query��ѯ����"), Browsable(true)]
        public string QueryConditionColIndexs
        {
            get
            {
                return this.queryConditionColIndexs;
            }
            set
            {
                queryConditionColIndexs = value;
            }
        }

        /// <summary>
        /// ���Ӳ�ѯ����
        /// </summary>
        private string[] detailConditions;

        /// <summary>
        /// ���Ӳ�ѯ����
        /// ��Ҫ������ҡ�ʱ��
        /// </summary>
        public string[] DetailConditions
        {
            set
            {
                this.detailConditions = value;
            }
        }

        #endregion

        #region Formula��ʽ

        /// <summary>
        /// �ϼ��е�����
        /// </summary>
        private string sumColIndexs = "";

        /// <summary>
        /// �ϼ��е�����
        /// </summary>
        [Description("�����"), Category("Formula��ʽ"), Browsable(true)]
        public string SumColIndexs
        {
            get
            {
                return this.sumColIndexs;
            }
            set
            {
                this.sumColIndexs = value;
                if (this.sumType == "Det")
                {
                    this.sumType = "Both";
                }
                else if (this.sumType == "Null")
                {
                    this.sumType = "Tot";
                }
            }
        }

        /// <summary>
        /// �ϼ��е�����
        /// </summary>
        private string sumDetailColIndexs = "";

        /// <summary>
        /// �ϼ��е�����
        /// </summary>
        [Description("��ϸ�����"), Category("Formula��ʽ"), Browsable(true)]
        public string SumDetailColIndexs
        {
            get
            {
                return sumDetailColIndexs;
            }
            set
            {
                this.sumDetailColIndexs = value;
                if (this.sumType == "Tot")
                {
                    this.sumType = "Both";
                }
                else if (this.sumType == "Null")
                {
                    this.sumType = "Det";
                }
            }
        }

        /// <summary>
        /// ��ƽ�����е�����
        /// </summary>
        private string avgColIndexs = "";

        /// <summary>
        /// ��ƽ�����е�����
        /// </summary>
        [Description("��ƽ�����е�����"), Category("Formula��ʽ"), Browsable(true)]
        public string AvgColIndexs
        {
            get
            {
                return this.avgColIndexs;
            }
            set
            {
                this.avgColIndexs = value;
                if (this.avgType == "Det")
                {
                    this.avgType = "Both";
                }
                else if (this.avgType == "Null")
                {
                    this.avgType = "Tot";
                }
            }
        }

        /// <summary>
        /// ��ƽ�����е�����
        /// </summary>
        private string avgDetailColIndexs = "";

        /// <summary>
        /// ��ƽ�����е�����
        /// </summary>
        [Description("��ϸ��ƽ�����е�����"), Category("Formula��ʽ"), Browsable(true)]
        public string AvgDetailColIndexs
        {
            get
            {
                return avgDetailColIndexs;
            }
            set
            {
                this.avgDetailColIndexs = value;
                if (this.avgType == "Tot")
                {
                    this.avgType = "Both";
                }
                else if (this.avgType == "Null")
                {
                    this.avgType = "Det";
                }
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        private string sortColIndexs = "";

        /// <summary>
        /// ������
        /// </summary>
        [Description("������"), Category("Formula��ʽ"), Browsable(true)]
        public string SortColIndexs
        {
            get { return sortColIndexs; }
            set { sortColIndexs = value; }
        }

        /// <summary>
        /// ��ϸ������
        /// </summary>
        private string detailSortColIndexs = "";

        /// <summary>
        /// ��ϸ������
        /// </summary>
        [Description("��ϸ������"), Category("Formula��ʽ"), Browsable(true)]
        public string DetailSortColIndexs
        {
            get { return detailSortColIndexs; }
            set { detailSortColIndexs = value; }
        }

        /// <summary>
        /// ����е���
        /// </summary>
        private string formulaColIndexs = "";

        /// <summary>
        /// ����е���
        /// </summary>
        [Description("����е���"), Category("Formula��ʽ"), Browsable(true)]
        public string FormulaColIndexs
        {
            get 
            { 
                return formulaColIndexs; 
            }
            set 
            { 
                formulaColIndexs = value;
                if (this.formulaType == "Det")
                {
                    this.formulaType = "Both";
                }
                else if (this.formulaType == "Null")
                {
                    this.formulaType = "Tot";
                }
            
            }
        }

        /// <summary>
        /// ��ϸ����е���
        /// </summary>
        private string detailRateColIndexs = "";

        /// <summary>
        /// ��ϸ����е���
        /// </summary>
        [Description("����е���"), Category("Formula��ʽ"), Browsable(true)]
        public string DetailRateColIndexs
        {
            get 
            { 
                return detailRateColIndexs;
            }
            set
            { 
                detailRateColIndexs = value;
                if (this.formulaType == "Tot")
                {
                    this.formulaType = "Both";
                }
                else if (this.formulaType == "Null")
                {
                    this.formulaType = "Det";
                }
            }
        }

        #endregion

        #region �ϲ�����

        /// <summary>
        /// ָ���ϲ����ݵ���
        /// </summary>
        private string mergeDataColIndexs = "";

        /// <summary>
        /// ָ���ϲ����ݵ���
        /// </summary>
        [Description("ָ���ϲ����ݵ���,����Sheet1��Ч"), Category("Merge�ϲ�����"), Browsable(true)]
        public string MergeDataColIndexs
        {
            get
            {
                return mergeDataColIndexs;
            }
            set
            {
                mergeDataColIndexs = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ�ϲ�����
        /// </summary>
        private bool isNeedMergeData = false;

        /// <summary>
        /// �Ƿ���Ҫ�ϲ�����
        /// </summary>
        [Description("Sheet1�����Ƿ���Ҫ�ϲ�����"), Category("Merge�ϲ�����"), Browsable(true), DefaultValue(false)]
        public bool IsNeedMergeData
        {
            get
            {
                return isNeedMergeData;
            }
            set
            {
                this.isNeedMergeData = value;
            }
        }
        /// <summary>
        /// ָ��������������
        /// </summary>
        private int crossDataColIndex = 0;

        /// <summary>
        /// �ϲ�����ʱ�ο�sheet�ĺϲ�����
        /// eg���ο�sheet[0]��0��[MergeDataColIndex=0]��ſ��Ҵ��룬
        /// ��ôsheet[1]��ҲӦ���п��Ҵ����У����Ҵ�����ͬ�еĺϲ���farPoint�е�ͬһ��
        /// </summary>
        [Description("ָ�������������У�SQLIndexs��һ��SQL��Ϊ�������ֶ����"), Category("Cross�ϲ�����"), Browsable(true)]
        public int CrossDataColIndex
        {
            get
            {
                return crossDataColIndex;
            }
            set
            {
                crossDataColIndex = value;
            }
        }

        /// <summary>
        /// �Ƿ񽻲�����
        /// </summary>
        private bool isNeedCrossData = false;

        /// <summary>
        /// �Ƿ񽻲�����
        /// </summary>
        [Description("�������ݣ�ΪTrueʱָ��CrossDataColIndexs����һ�����SQL�����ڵ�һ��"), Category("Cross�ϲ�����"), Browsable(true), DefaultValue(false)]
        public bool IsNeedCrossData
        {
            get
            {
                return isNeedCrossData;
            }
            set
            {
                this.isNeedCrossData = value;
            }
        }

        #endregion

        #region ��ӡ����
        /// <summary>
        /// ��ӡ�Ƿ���ҪԤ��
        /// </summary>
        private bool isNeedPreView = true;

        /// <summary>
        /// ��ӡ�Ƿ���ҪԤ��[ֻд][Ĭ����Ҫ]
        /// </summary>
        [Description("��ӡ�Ƿ���ҪԤ��"), Category("Print��ӡ����"), Browsable(true), DefaultValue(true)]
        public bool IsNeedPreView
        {
            get
            {
                return isNeedPreView;
            }
            set
            {
                isNeedPreView = value;
            }
        }

        /// <summary>
        /// ��ӡֽ�ų��ȸ��������Զ�����
        /// </summary>
        private bool isAutoPaper = true;

        /// <summary>
        /// ��ӡֽ�ų��ȸ��������Զ�����
        /// </summary>
        [Description("��ӡֽ�ų��ȸ��������Զ�����"), Category("Print��ӡ����"), Browsable(true), DefaultValue(true)]
        public bool IsAutoPaper
        {
            get { return isAutoPaper; }
            set { isAutoPaper = value; }
        }

        /// <summary>
        /// ��ӡֽ�ų��ȸ��Ӹ߶�
        /// </summary>
        private int paperAddHeight = 0;

        /// <summary>
        /// ��ӡֽ�ų��ȸ��Ӹ߶�
        /// </summary>
        [Description("��ӡֽ�ų��ȸ��Ӹ߶�"), Category("Print��ӡ����"), Browsable(true)]
        public int PaperAddHeight
        {
            get { return paperAddHeight; }
            set { paperAddHeight = value; }
        }

        /// <summary>
        /// ��ӡֽ��
        /// </summary>
        private string paperName = "Letter";

        /// <summary>
        /// ��ӡֽ��
        /// </summary>
        [Description("��ӡֽ��"), Category("Print��ӡ����"), Browsable(true)]
        public string PaperName
        {
            get { return paperName; }
            set { paperName = value; }
        }

        /// <summary>
        /// ֽ�Ÿ߶�
        /// </summary>
        private int paperWith = 800;

        /// <summary>
        /// ֽ�ſ��
        /// </summary>
        [Description("ֽ�����ؿ��"), Category("Print��ӡ����"), Browsable(true)]
        public int PaperWith
        {
            get { return paperWith; }
            set { paperWith = value; }
        }

        /// <summary>
        /// ֽ�Ÿ߶�
        /// </summary>
        private int paperHeight = 1100;

        /// <summary>
        /// ֽ�Ÿ߶�
        /// </summary>
        [Description("ֽ�����ظ߶�"), Category("Print��ӡ����"), Browsable(true)]
        public int PaperHeight
        {
            get { return paperHeight; }
            set { paperHeight = value; }
        }

        /// <summary>
        /// ��ӡ������
        /// </summary>
        private string printerName = "";

        /// <summary>
        /// ��ӡ������
        /// </summary>
        [Description("��ӡ������"), Category("Print��ӡ����"), Browsable(true)]
        public string PrinterName
        {
            get 
            {
                return printerName; 
            }
            set 
            { 
                this.printerName = value; 
            }
        }

        #endregion

        #region �����ļ�
        /// <summary>
        /// �����ļ�
        /// </summary>
        private string settingFilePatch = "";

        /// <summary>
        /// ��ϸ�����ļ�
        /// </summary>
        private string detailSettingFilePatch = "";

        /// <summary>
        /// ��ϸ�����ļ�
        /// </summary>
        [Description("��ϸ�����ļ�"), Category("Set�����ļ�"), Browsable(true)]
        public string DetailSettingFilePatch
        {
            get
            {
                if (this.detailSettingFilePatch == string.Empty)
                {
                    try
                    {
                        return FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\" + this.FindForm().Text + "detail.xml";
                    }
                    catch
                    {
                        return ""; 
                    }
                }
                return detailSettingFilePatch; 
            }
            set 
            { 
                detailSettingFilePatch = value;
            }
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        [Description("�����ļ�"), Category("Set�����ļ�"), Browsable(true)]
        public string SettingFilePatch
        {
            get 
            {
                if (this.settingFilePatch == string.Empty)
                {
                    try
                    {
                        return FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\" + this.FindForm().Text + "tot.xml";
                    }
                    catch
                    {
                        return "";
                    }
                }
                return settingFilePatch; 
            }
            set { settingFilePatch = value; }
        }
        #endregion

        #region FarPoint����

        /// <summary>
        /// ��ͷ�ɼ���
        /// </summary>
        private bool rowHeaderVisible = true;

        /// <summary>
        /// ��ͷ�ɼ���
        /// </summary>
        [Description("��ͷ�ɼ���"), Category("FarPoint����"), Browsable(true), DefaultValue(true)]
        public bool RowHeaderVisible
        {
            get
            {
                return rowHeaderVisible;
            }
            set 
            {
                rowHeaderVisible = value;
            }
        }

        /// <summary>
        /// ��ͷ��
        /// </summary>
        private float columnHeaderHeight = 20F;

        /// <summary>
        /// ��ͷ��
        /// </summary>
        [Description("��ͷ��"), Category("FarPoint����"), Browsable(true)]
        public float ColumnHeaderHeight
        {
            get { return columnHeaderHeight; }
            set { columnHeaderHeight = value; }
        }        
       
        /// <summary>
        /// ����
        /// </summary>
        [Description("����"), Category("FarPoint����"), Browsable(true)]
        public string SheetName
        {
            get
            {
                return this.fpSpread1_Sheet1.SheetName;
            }
            set 
            {
                if(string.IsNullOrEmpty(value))
                {
                    this.fpSpread1_Sheet1.SheetName = "����";
                }
                else
                {
                this.fpSpread1_Sheet1.SheetName = value;
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        [Description("��ϸ�����"), Category("FarPoint����"), Browsable(true)]
        public string DetailSheetName
        {
            get
            {
                return this.fpSpread1_Sheet2.SheetName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.fpSpread1_Sheet2.SheetName = "��ϸ";
                }
                else
                {
                    this.fpSpread1_Sheet2.SheetName = value;
                }
            }
        }

        /// <summary>
        /// ��Ĵ�ֱ����ɫ
        /// </summary>
        [Description("��Ĵ�ֱ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color VerticalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ���ˮƽ����ɫ
        /// </summary>
        [Description("���ˮƽ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color HorizontalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ��Ĵ�ֱ����ɫ
        /// </summary>
        [Description("����ͷ�Ĵ�ֱ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color CHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ���ˮƽ����ɫ
        /// </summary>
        [Description("����ͷ��ˮƽ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color CHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ��Ĵ�ֱ����ɫ
        /// </summary>
        [Description("����ͷ�Ĵ�ֱ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color RHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.RowHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ���ˮƽ����ɫ
        /// </summary>
        [Description("����ͷ��ˮƽ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color RHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.RowHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }


        /// <summary>
        /// ��Ĵ�ֱ����ɫ
        /// </summary>
        [Description("��ϸ��Ĵ�ֱ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color DetailVerticalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ���ˮƽ����ɫ
        /// </summary>
        [Description("�����ˮƽ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color DetailHorizontalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

         /// <summary>
        /// ��Ĵ�ֱ����ɫ
        /// </summary>
        [Description("��ϸ��ͷ�Ĵ�ֱ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color DetailCHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.ColumnHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ���ˮƽ����ɫ
        /// </summary>
        [Description("��ϸ��ͷ��ˮƽ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color DetailCHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.ColumnHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ��ϸ��ͷ�ɼ���
        /// </summary>
        private bool detailRowHeaderVisible = true;

        /// <summary>
        /// ��ϸ��ͷ�ɼ���
        /// </summary>
        [Description("��ϸ��ͷ�ɼ���"), Category("FarPoint����"), Browsable(true), DefaultValue(true)]
        public bool DetailRowHeaderVisible
        {
            get
            {
                return detailRowHeaderVisible;
            }
            set
            {
                detailRowHeaderVisible = value;
            }
        }
        /// <summary>
        /// ��ϸ��ͷ��
        /// </summary>
        private float detailColumnHeaderHeight = 20F;

        /// <summary>
        /// ��ϸ��ͷ��
        /// </summary>
        [Description("��ϸ��ͷ��"), Category("FarPoint����"), Browsable(true)]
        public float DetailColumnHeaderHeight
        {
            get { return detailColumnHeaderHeight; }
            set { detailColumnHeaderHeight = value; }
        }

        /// <summary>
        /// ��Ĵ�ֱ����ɫ
        /// </summary>
        [Description("��ϸ����ͷ�Ĵ�ֱ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color DetailRHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.RowHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// ���ˮƽ����ɫ
        /// </summary>
        [Description("��ϸ����ͷ��ˮƽ����ɫ"), Category("FarPoint����"), Browsable(true)]
        public Color DetailRHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.RowHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }


        #endregion

        #endregion

        #region ����

        #region ˽��

        /// <summary>
        /// �����������ÿؼ�
        /// </summary>
        /// <returns></returns>
        private int setControlWithPerporty()
        {
            this.nGroupBoxQueryCondition.Visible = this.IsDeptAsCondition | this.IsTimeAsCondition | (this.FilerType != EnumFilterType.������);
            
         

            //����
            this.lbMainTitle.Text = this.MainTitle;
            if (this.MainTitle == null || this.MainTitle == string.Empty)
            {
                //this.panelTitle.Visible = false;
            }
            this.lbAdditionTitleLeft.Text = this.LeftAdditionTitle;
            this.lbAdditionTitleMid.Text = this.MidAdditionTitle;
            this.lbAdditionTitleRight.Text = this.RightAdditionTitle;
            if (!this.IsNeedAdditionTitle)
            {
                this.panelAdditionTitle.Height = 0;
            }
            else
            {
                this.panelAdditionTitle.Height = this.AddtionTitleHeight;               
            }
          

            this.panelTitle.Height = this.MainTitleHeight;
            this.lbMainTitle.Font = new Font(this.lbMainTitle.Font.FontFamily, this.MainTitleFontSize, this.MainTitleFontStyle);
            this.lbAdditionTitleLeft.Font = new Font(this.lbAdditionTitleLeft.Font.FontFamily, this.AddtionTitleFontSize, this.AddtionTitleFontStyle);
            this.lbAdditionTitleMid.Font = new Font(this.lbAdditionTitleMid.Font.FontFamily, this.AddtionTitleFontSize, this.AddtionTitleFontStyle);
            this.lbAdditionTitleRight.Font = new Font(this.lbAdditionTitleRight.Font.FontFamily, this.AddtionTitleFontSize, this.AddtionTitleFontStyle);

            this.panelTime.Visible = this.IsTimeAsCondition;
            this.panelDept.Visible = this.IsDeptAsCondition;
            //��ѯ
            if (!this.IsNeedDetailData)
            {
                this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet2);
            }
            else
            {
                if (!this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet2))
                {
                    this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet2);
                }
            }

            //FarPiont����
            if (this.DetailSQLIndexs == null || this.DetailSQLIndexs == string.Empty)
            {
                this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet2);
            }
            this.fpSpread1_Sheet1.RowHeader.Visible = this.RowHeaderVisible;
            this.fpSpread1_Sheet2.RowHeader.Visible = this.DetailRowHeaderVisible;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].Height = this.ColumnHeaderHeight;
            this.fpSpread1_Sheet2.ColumnHeader.Rows[0].Height = this.DetailColumnHeaderHeight;
           
            return 0;
        }

        /// <summary>
        /// ���ݶ���Ȩ�޳�ʼ����Ѱ����
        /// </summary>
        /// <returns>-1ʧ�� 0�ɹ�</returns>
        private int initDeptByPriPower()
        {
            this.isHavePriPower = true;

            //���Ҳ��ɼ�ʱ��ʾ����
            if (!this.IsDeptAsCondition && !isJugdePriPower)
            {
                return 0;
            }
            if (this.priveClassTwos == null)
            {
                MessageBox.Show("����Ȩ������û�и�ֵ\n��ʹ������PriClassTwos[�ַ���]");
                return -1;
            }

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            
            //��ϣ�����ڱ�������ظ����
            System.Collections.Hashtable hsDept = new Hashtable();

            //���п��ң����ж������Ȩ��ʱ���������ظ���
            ArrayList alDept = new ArrayList();

            if (this.DeptType == "AllDept")
            {
                alDept = deptMgr.GetDeptmentAll();
            }           
            else if (this.DeptType == "PriveDept")
            {
                //ÿһ����Ȩ�޶�Ӧ����
                System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> alTmpDept;

                //���ݶ���Ȩ�޻�ȡ������Ϣ
                string[] prives = priveClassTwos.Split(',',' ','|');
                foreach (string classTwoCode in prives)
                {
                    alTmpDept = new System.Collections.Generic.List<FS.FrameWork.Models.NeuObject>();
                    string[] prive3s = classTwoCode.Split('+');
                    if (prive3s.Length == 1)
                    {
                        alTmpDept = this.priPowerMgr.QueryUserPriv(priPowerMgr.Operator.ID, classTwoCode);
                        if (alTmpDept == null || alTmpDept.Count == 0)
                        {
                            continue;
                        }
                        alDept.AddRange(alTmpDept);
                    }
                    else
                    {
                        for (int index = 0; index < prive3s.Length; index++ )
                        {
                            if (index + 1 < prive3s.Length)
                            {
                                alTmpDept = this.priPowerMgr.QueryUserPriv(priPowerMgr.Operator.ID, prive3s[index], prive3s[index + 1]);
                            }
                            if (alTmpDept == null || alTmpDept.Count == 0)
                            {
                                continue;
                            }
                            alDept.AddRange(alTmpDept);
                        }
                    }
                }
                
            }
            else
            {
                string[] deptTypes = this.deptType.Split('|', ',', ' ');
                foreach (string dType in deptTypes)
                {

                    alDept.AddRange(deptMgr.GetDeptmentByType(dType));
                }
            }
            //�Ƴ��ظ��Ŀ���
            foreach (FS.FrameWork.Models.NeuObject dept in alDept)
            {
                if (!hsDept.Contains(dept.ID))
                {
                    hsDept.Add(dept.ID,  dept);
                }
                else
                {
                    //alDept.Remove(dept);
                }
            }
            alDept.RemoveRange(0, alDept.Count);
            string id = "";
            foreach (FS.FrameWork.Models.NeuObject dept in hsDept.Values)
            {
                id += "'" + dept.ID + "',";
                alDept.Add(dept);
            }
            FS.FrameWork.Models.NeuObject deptTmp;

            //��ӡ�ȫ����
            if (alDept.Count > 0)
            {
                if (this.isAllowAllDept)
                {
                    deptTmp = new FS.FrameWork.Models.NeuObject();
                    if (this.DeptType == "PriveDept")
                    {
                        deptTmp.ID = id.TrimEnd(',', '\'');
                        deptTmp.ID = deptTmp.ID.TrimStart('\'');
                    }
                    else
                    {
                        deptTmp.ID = "All";
                    }
                    deptTmp.Name = "ȫ��";
                    alDept.Insert(0, deptTmp);
                }
            }

            //���û��Ȩ��
            else
            {
                deptTmp = new FS.FrameWork.Models.NeuObject();
                deptTmp.ID = "Null";
                deptTmp.Name = "��û�пɲ�ѯ�Ŀ���";
                alDept.Insert(0, deptTmp);
                this.isHavePriPower = false;
            }

            //�
            ArrayList alNuserCell = deptMgr.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.N.ToString());

            this.cmbNurse.AddItems(alNuserCell);
            if (alNuserCell.Count == 1)
            {
                this.cmbNurse.SelectedIndex = 0;
                this.cmbNurse.Tag = ((FS.FrameWork.Models.NeuObject)alNuserCell[0]).ID;
            }

            ArrayList alInHosDept = deptMgr.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.I.ToString());

            this.cmbDept.AddItems(alInHosDept);
            if (alInHosDept.Count == 1)
            {
                this.cmbDept.SelectedIndex = 0;
                this.cmbDept.Tag = ((FS.FrameWork.Models.NeuObject)alInHosDept[0]).ID;
            }

            return 0;
        }

        /// <summary>
        /// ��ʼ����ѯʱ��
        /// </summary>
        /// <returns>-1ʧ�� 0�ɹ�</returns>
        private int initTime()
        {
            //ʱ�䲻�ɼ���������
            if (!this.IsTimeAsCondition)
            {
                return 0;
            }
            System.DateTime dt = this.priPowerMgr.GetDateTimeFromSysDateTime();
        
            this.dtStart.Value = this.dtStart.Value.AddDays(-(this.daysSpan + 1));
            return 0;
        }    

        /// <summary>
        /// ��ѯ����
        /// �������ϸ��ѯ�������Ϊ���ܲ�ѯ
        /// ��ϸͨ��˫��fpʵ��
        /// </summary>
        /// <returns>-1ʧ�� 0�ɹ�</returns>
        private int queryData()
        {
            //add by cube 2009-10-15 ���������������е�����
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnCount = 0;
            //end add

            if (string.IsNullOrEmpty(this.SQLIndexs) && string.IsNullOrEmpty(this.SQL))
            {
                this.ShowBalloonTip(5000,"��ܰ��ʾ","��ѯ��SQL�����������ȷ\n�뽫SqlIndexs���Ը�ֵ");
                return -1;
            }
            System.Data.DataSet ds = new DataSet();
            if (!isHavePriPower && this.isJugdePriPower)
            {
                MessageBox.Show("��û��Ȩ�ޣ������Խ�����Ȩ�޴��룺" + this.priveClassTwos + "������Ϣ���Ա�Э�������");
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ժ�...");
            Application.DoEvents();
            if (this.IsNeedCrossData)
            {

                if (this.crossQuery() == -1)
                {
                    return -1;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.SQL))
                {
                    string[] sqls = this.SQLIndexs.Split(' ', ',', '|');
                    if (this.isNeedAdditionConditions)
                    {
                        if (this.priPowerMgr.ExecQuery(sqls, ref ds, this.queryAdditionConditions) == -1)
                        {
                            MessageBox.Show("ִ��sql��������>>" + this.priPowerMgr.Err);
                            return -1;
                        }
                    }
                    else if (this.priPowerMgr.ExecQuery(sqls, ref ds, this.QueryConditions) == -1)
                    {
                        MessageBox.Show("ִ��sql��������>>" + this.priPowerMgr.Err);
                        return -1;
                    }
                }
                else 
                {
                    string sql = string.Format(this.SQL, this.QueryConditions);
                    if (this.priPowerMgr.ExecQuery(sql, ref ds) == -1)
                    {
                        MessageBox.Show("ִ��sql��������>>" + this.priPowerMgr.Err);
                        return -1;
                    }
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }
            this.DataView = new DataView(ds.Tables[0]);
            this.fpSpread1_Sheet1.DataSource = this.DataView;

            this.readSetting(0);
            this.sortTot();
            this.sumTot();
            this.avgTot();
            this.formulaTot();
            this.mergeData();

            if (this.detailDataQueryType == EnumQueryType.ͬ����ͬ����ѯ)
            {
                this.queryDetailData();
            }

            if (this.LeftAdditionTitle.IndexOf("ͳ��ʱ��") != -1)
            {
                this.lbAdditionTitleLeft.Text = " ͳ��ʱ��:" + this.dtStart.Value.ToString();
            }
            else if (LeftAdditionTitle.IndexOf("ͳ������") != -1)
            {
                this.lbAdditionTitleLeft.Text = " ͳ������:" + this.dtStart.Value.ToString("yyyy��MM��dd��");
            }
            else if (this.LeftAdditionTitle.IndexOf("��ӡʱ��") != -1)
            {
                this.lbAdditionTitleLeft.Text = " ��ӡʱ��:" + this.priPowerMgr.GetDateTimeFromSysDateTime().ToString();
            }
            else if (LeftAdditionTitle.IndexOf("��ӡ����") != -1)
            {
                this.lbAdditionTitleLeft.Text = " ��ӡ����:" + this.priPowerMgr.GetDateTimeFromSysDateTime().ToString("yyyy��MM��dd��");
            }
            else if (LeftAdditionTitle.IndexOf("���") != -1)
            {
               // this.lbAdditionTitleLeft.Text = " ���:" + this.dtEnd.Value.AddSeconds(1).ToString("yyyy��");
            }
            if (this.cmbNurse.Text != "ȫ��")
            {
                if (this.MidAdditionTitle.IndexOf("����") != -1)
                {
                    this.lbAdditionTitleMid.Text = "����:" + this.cmbNurse.Text;
                }
                if (this.MidAdditionTitle.IndexOf("����") != -1)
                {
                    this.lbAdditionTitleMid.Text = "����:" + this.cmbNurse.Text;
                }
            }
            else
            {
                this.lbAdditionTitleMid.Text = "";
            }
            return 0;
        }

        /// <summary>
        /// ��ѯ������Ҫ�ϲ�������
        /// </summary>
        /// <returns></returns>
        private int crossQuery()
        {
            FarPoint.Win.Spread.SheetView sv;
            System.Data.DataSet dsTmp;
            System.Collections.Generic.List<FarPoint.Win.Spread.SheetView> sheets = new System.Collections.Generic.List<FarPoint.Win.Spread.SheetView>();
            string[] sqls = this.SQLIndexs.Split(',','|',' ');
            foreach (string sql in sqls)
            {
                dsTmp = new DataSet();
                sv = new FarPoint.Win.Spread.SheetView();
                if (this.isNeedAdditionConditions)
                {
                    if (this.priPowerMgr.ExecQuery(sql, ref dsTmp, this.queryAdditionConditions) == -1)
                    {
                        MessageBox.Show("ִ��sql��������>>" + this.priPowerMgr.Err);
                        return -1;
                    }
                }
                else if (this.priPowerMgr.ExecQuery(sql, ref dsTmp, this.QueryConditions) == -1)
                {
                    MessageBox.Show("ִ��sql��������>>" + this.priPowerMgr.Err);
                    return -1;
                }
                sv.DataSource = dsTmp;
                sheets.Add(sv);
            }
            return this.mergeCrossSheet(sheets);
        }

        /// <summary>
        /// �ϲ�����[��ɽ��汨��Ĺ���]
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        private int mergeCrossSheet(System.Collections.Generic.List<FarPoint.Win.Spread.SheetView> sheets)
        {
            if (sheets == null || sheets.Count == 0)
            {
                return -1;
            }

            //��һ��sheet��Ϊ�ο�������sheet��������Ӧ�����һsheetһ��
            System.Collections.Hashtable hsKey = new Hashtable();

            //��һ��sheet��Ϊ�ο�
            int maxRowNo = sheets[0].RowCount;
            this.fpSpread1_Sheet1.RowCount = maxRowNo;
            this.fpSpread1_Sheet1.ColumnCount = 500;
            int maxColNo = 0;

            foreach (FarPoint.Win.Spread.SheetView sheet in sheets)
            {
                //��ѭ��
                for (int colIndex = 0; colIndex < sheet.ColumnCount; colIndex++)
                {
                    //���ǵ�һ��sheet���ؼ�ֵ��0����
                    if (sheet != sheets[0] && colIndex == 0)
                    {
                        continue;
                    }
                    //��̬����һ��
                    //this.fpSpread1_Sheet1.AddColumns(this.fpSpread1_Sheet1.ColumnCount, 1);
                    //��ѭ�����������ܶ��ڲο�sheet����
                    for (int rowIndex = 0; rowIndex < sheet.RowCount && rowIndex < maxRowNo; rowIndex++)
                    {
                        //ȡ�ùؼ�ֵ
                        if (colIndex == crossDataColIndex)
                        {
                            if (sheet == sheets[0])
                            {
                                if (!hsKey.Contains(sheet.Cells[rowIndex, colIndex].Value.ToString()))
                                {
                                    //��¼�˹ؼ�ֵ��Ӧ����
                                    hsKey.Add(sheet.Cells[rowIndex, colIndex].Value.ToString(), rowIndex);
                                }
                                //��һsheet�Ĺؼ�ֵ����farPoint
                                this.fpSpread1_Sheet1.Cells[rowIndex, maxColNo].Value = sheet.Cells[rowIndex, colIndex].Value;
                            }
                            continue;
                        }
                        //���ǹؼ�ֵ�У�����farPoint
                        //�ؼ����ҵ���Ӧ���У�����ڹ�ϣ����
                        if (hsKey.Contains(sheet.Cells[rowIndex, 0].Value.ToString()))
                        {
                            int row = (int)hsKey[sheet.Cells[rowIndex, 0].Value.ToString()];
                            this.fpSpread1_Sheet1.Cells[row, maxColNo].Value = sheet.Cells[rowIndex, colIndex].Value;
                        }
                        else if (sheet == sheets[0])
                        {
                            //û���ҵ���
                            this.fpSpread1_Sheet1.Cells[rowIndex, maxColNo].Value = sheet.Cells[rowIndex, colIndex].Value;
                        }
                    }
                    maxColNo++;
                    if (this.fpSpread1_Sheet1.ColumnCount < maxColNo)
                    {
                        this.fpSpread1_Sheet1.AddColumns(this.fpSpread1_Sheet1.ColumnCount - 1, 1);
                    }
                }
            }
            this.fpSpread1_Sheet1.ColumnCount = maxColNo;
            return 0;
        }

        /// <summary>
        /// ��ȡ��ϸ��ѯ�Ĳ���
        /// </summary>
        /// <returns></returns>
        private string[] getDetailParm()
        {
            if (this.detailDataQueryType == EnumQueryType.�����������)
            {

                int activeRowIndex = this.fpSpread1_Sheet1.ActiveRow.Index;
                string[] parm;
                int totLenth = this.QueryConditions.Length;
                string[] cols = queryConditionColIndexs.Split( ' ',',','|');
                parm = new string[totLenth + cols.Length];
                int index = 0;
                foreach (string totParm in this.QueryConditions)
                {
                    parm[index] = totParm;
                    index++;
                }
                foreach (string colIndex in cols)
                {
                    string curColValue = this.fpSpread1_Sheet1.Cells[activeRowIndex, int.Parse(colIndex)].Text.Trim();
                    parm[index] = curColValue;
                    index++;
                }
                return parm;
            }
            if (this.detailDataQueryType == EnumQueryType.���ȡ����)
            {

                int activeRowIndex = this.fpSpread1_Sheet1.ActiveRow.Index;
                string[] cols = queryConditionColIndexs.Split(' ', ',', '|');
                string[] parm = new string[cols.Length];
                int index = 0;
                foreach (string colIndex in cols)
                {
                    string curColValue = this.fpSpread1_Sheet1.Cells[activeRowIndex, int.Parse(colIndex)].Text.Trim();
                    parm[index] = curColValue;
                    index++;
                }
                return parm;
            }
            if (this.DetailDataQueryType == EnumQueryType.ͬ�����첽��ѯ || this.DetailDataQueryType == EnumQueryType.ͬ����ͬ����ѯ)
            {
                return this.QueryConditions;
            }
            if (this.detailConditions == null)
            {
                return new string[] { ""};
            }
            return this.detailConditions;
        }
        /// <summary>
        /// ��ѯ����
        /// �������ϸ��ѯ�������Ϊ���ܲ�ѯ
        /// ��ϸͨ��˫��fpʵ��
        /// </summary>
        /// <returns>-1ʧ�� 0�ɹ�</returns>
        private int queryDetailData()
        {
            if (!this.isNeedDetailData)
            {
                return 0;
            }
            if (this.detailSQLIndexs == null || this.detailSQLIndexs.Length == 0)
            {
                MessageBox.Show("��ѯ��SQL�����������ȷ\n�뽫DetailSQLIndexs���Ը�ֵ");
                return -1;
            }
            System.Data.DataSet ds = new DataSet();
            if (this.isNeedAdditionConditions)
            {
                if (this.priPowerMgr.ExecQuery(this.detailSQLIndexs.Split(' ', ',', '|'), ref ds, this.queryAdditionConditions) == -1)
                {
                    this.ShowBalloonTip(1000, "��ܰ��ʾ", "��������" + this.priPowerMgr.Err);
                    return -1;
                }
            }
            else
            {
                string[] parm = this.getDetailParm();
                if (this.priPowerMgr.ExecQuery(this.detailSQLIndexs.Split(' ', ',', '|'), ref ds, parm) == -1)
                {
                    this.ShowBalloonTip(1000, "��ܰ��ʾ", "��������" + this.priPowerMgr.Err);
                    return -1;
                }
            }
            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }
            this.DetailDataView = new DataView(ds.Tables[0]);
            if (this.fpSpread1.Sheets.Count == 1)
            {
                this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet2);
            }
            this.fpSpread1_Sheet2.DataSource = this.DetailDataView;
            this.sumDetail();
            this.avgDetail();

            this.readSetting(1);
            this.sortDetail();
            
            return 0;
        }

        /// <summary>
        /// ���ݵ���
        /// </summary>
        private int exportData()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    this.ShowBalloonTip(5000, "��ܰ��ʾ", "�����ɹ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ݷ�������>>" + ex.Message);
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns></returns>
        private int printData()
        {
            this.resetTitleLocation(true);
            int width = this.Width;
            this.Dock = System.Windows.Forms.DockStyle.None;
            if (this.isAutoPaper)
            {
                PrintObject.SetPageSize(this.getPaperSize());
               
                if (this.isNeedPreView)
                {
                    PrintObject.PrintPreview(resetReportLocation(), 20, this.panelPrint);
                }
                else
                {
                    PrintObject.PrintPage(resetReportLocation(), 20, this.panelPrint);
                }

                this.Width = width;
                this.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            else
            {
                this.printData(this.PaperName, this.PaperWith, this.PaperHeight, this.PrinterName);
            }
            this.resetTitleLocation(false);
            return 0;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="paperName">ֽ������</param>
        /// <param name="paperWidth">���</param>
        /// <param name="paperHeight">�߶�</param>
        /// <returns></returns>
        private int printData(string paperName, int paperWidth, int paperHeight, string printerName)
        {
            if (paperName == string.Empty)
            {
                this.ShowBalloonTip(5000, "��ܰ��ʾ", "û������ֽ��\n�Ѿ�����");
                this.IsAutoPaper = true;
                return this.printData();
            }
            int width = this.Width;
            this.Dock = System.Windows.Forms.DockStyle.None; 
            PrintObject.IsResetPage = true;
            FS.HISFC.Models.Base.PageSize paperSize = new FS.HISFC.Models.Base.PageSize(paperName, paperWidth, paperHeight);
            if (!string.IsNullOrEmpty(this.PrinterName))
            {
                paperSize.Printer = this.PrinterName;
            }
            PrintObject.SetPageSize(paperSize);
            if (this.IsNeedPreView)
            {
                PrintObject.PrintPreview(resetReportLocation(), 20, this.panelPrint);
            }
            else
            {
                PrintObject.PrintPage(resetReportLocation(), 20, this.panelPrint);
            }
            this.Width = width;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            return 0;
        }
       
        /// <summary>
        /// ���ⵥ��ֽ�Ÿ߶�����
        /// Ĭ������������г������ݵĸ߶�
        /// </summary>
        private FS.HISFC.Models.Base.PageSize getPaperSize()
        {
            FS.HISFC.Models.Base.PageSize paperSize = new FS.HISFC.Models.Base.PageSize();
            paperSize.Name = this.priPowerMgr.GetDateTimeFromSysDateTime().ToString();
            try
            {
                int width = 800;

                int curHeight = this.Height;

                int addHeight = (this.fpSpread1.ActiveSheet.RowCount - 1) *
                    (int)this.fpSpread1.ActiveSheet.Rows[0].Height;

                int additionAddHeight = this.PaperAddHeight;
                if (!string.IsNullOrEmpty(this.PrinterName))
                {
                    paperSize.Printer = this.PrinterName;
                }
                paperSize.Width = width;
                paperSize.Height = (addHeight + curHeight + additionAddHeight);
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ֽ�ų���>>" + ex.Message);
            }
            return paperSize;
        }
        /// <summary>
        /// ���FarPoint�Ŀ��С��uc�ؼ�������ҳ�߾࣬ʹ�����
        /// ���򣬷���ҳ�߾�20������Ҫ����FarPoint
        /// </summary>
        /// <returns></returns>
        private int resetReportLocation()
        {
            this.Width = this.MaxWidth;
            float sheetWidth = 0;
            FarPoint.Win.Spread.SheetView sv = this.fpSpread1.ActiveSheet;
            for (int index = 0; index < sv.ColumnCount; index++)
            {
                sheetWidth += sv.Columns[index].Width;
            }
            if ((int)sheetWidth < this.Width)
            {
            }
            return 20;

        }

        /// <summary>
        /// �������ñ���λ��
        /// </summary>
        private void resetTitleLocation(bool isPrint)
        {
            this.lbMainTitle.Dock = DockStyle.None;
            this.panelTitle.Controls.Remove(this.lbMainTitle);
            if (this.IsNeedAdditionTitle)
            {
                this.panelAdditionTitle.Controls.Remove(this.lbAdditionTitleMid);
                this.panelAdditionTitle.Controls.Remove(this.lbAdditionTitleRight);
            }
            int with = 0;
            for (int col = 0; col < this.fpSpread1.ActiveSheet.ColumnCount; col++)
            {
                if (this.fpSpread1.ActiveSheet.Columns[col].Visible)
                {
                    with += (int)this.fpSpread1.ActiveSheet.Columns[col].Width;
                }
            }
            if (!isPrint && with > this.panelTitle.Width)
            {
                with = this.panelTitle.Width;
            }
            this.lbMainTitle.Location = new Point((with - this.lbMainTitle.Size.Width) / 2, this.lbMainTitle.Location.Y);
            this.lbAdditionTitleMid.Location = new Point((with - this.lbAdditionTitleMid.Size.Width) / 2, this.lbAdditionTitleMid.Location.Y);
            this.lbAdditionTitleRight.Location = new Point((with - this.lbAdditionTitleRight.Size.Width), this.lbAdditionTitleRight.Location.Y);

            this.panelTitle.Controls.Add(this.lbMainTitle);

            if (this.IsNeedAdditionTitle)
            {
                this.panelAdditionTitle.Controls.Add(this.lbAdditionTitleMid);
                this.panelAdditionTitle.Controls.Add(this.lbAdditionTitleRight);
            }
        }
        private void resetTitleLocation()
        {
            resetTitleLocation(false);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private int sumTot()
        {
            try
            {
                if (this.sumColIndexs == null || this.sumColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.sumType == "Tot" || this.sumType == "Both")
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = sumColIndexs.Split(' ',',','|');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                            if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1 || this.fpSpread1_Sheet1.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            string formul = "SUM(" + letter + "1:" + letter + (this.fpSpread1_Sheet1.RowCount - 1).ToString() + ")";

                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].Formula = formul;
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n���ʱ�Ѿ�������" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
                if (this.sumColIndexs[0] != '0')
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value = "�ϼƣ�";
                }
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "��ܰ��ʾ", ex.Message + "\n���ʱ�Ѿ�����");
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// ��ϸ���
        /// </summary>
        /// <returns></returns>
        private int sumDetail()
        {
            try
            {
                if (this.sumDetailColIndexs == null || this.sumDetailColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.sumType == "Det" || this.sumType == "Both")
                {
                    this.fpSpread1_Sheet2.AddRows(this.fpSpread1_Sheet2.RowCount, 1);
                    string[] cols = this.sumDetailColIndexs.Split(' ', ',', '|');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                            if (colIndex > this.fpSpread1_Sheet2.ColumnCount - 1 || this.fpSpread1_Sheet2.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();

                            this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].Formula
                            = "SUM(" + letter + "1:" + letter + (this.fpSpread1_Sheet2.RowCount - 1).ToString() + ")";
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n��ϸ���ʱ�Ѿ�������" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
                if (this.sumDetailColIndexs[0] != 0)
                {
                    this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, 0].Value = "�ϼƣ�";
                }
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "��ܰ��ʾ", ex.Message + "\n��ϸ���ʱ�Ѿ�����");
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// ������ƽ����
        /// </summary>
        /// <returns></returns>
        private int avgTot()
        {
            try
            {
                if (this.avgColIndexs == null || this.avgColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.avgType == "Tot" || this.avgType == "Both")
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = avgColIndexs.Split(' ', ',');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[0]);
                            if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1 || this.fpSpread1_Sheet1.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            int rowCount = this.fpSpread1_Sheet1.RowCount - 1;
                            if (!string.IsNullOrEmpty(this.sumColIndexs) && (this.sumType == "Both" || this.sumType == "Tot"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount <= 1)
                                {
                                    continue;
                                }
                            }

                            string formula = "AVERAGE(" + letter + "1:" + letter + rowCount.ToString() + ")";

                            int decimalPlaces = 0;
                            if (colLetter.Split('|').Length > 1)
                            {
                                decimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[1]);
                            }
                            if (colLetter.Split('|').Length > 2 && colLetter.Split('|')[2] == "%")
                            {
                                FarPoint.Win.Spread.CellType.PercentCellType p = new FarPoint.Win.Spread.CellType.PercentCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;
                            }
                            else
                            {
                                FarPoint.Win.Spread.CellType.NumberCellType p = new FarPoint.Win.Spread.CellType.NumberCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;

                            }
                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].Formula = formula;
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n��ƽ����ʱ�Ѿ�������" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
                if (this.avgColIndexs[0] != '0')
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value = "ƽ����";
                }
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "��ܰ��ʾ", ex.Message + "\n���ʱ�Ѿ�����");
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// ��ϸ��ƽ����
        /// </summary>
        /// <returns></returns>
        private int avgDetail()
        {
            try
            {
                if (this.avgDetailColIndexs == null || this.avgDetailColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.avgType == "Det" || this.avgType == "Both")
                {
                    this.fpSpread1_Sheet2.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = avgDetailColIndexs.Split(' ', ',');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[0]);
                            if (colIndex > this.fpSpread1_Sheet2.ColumnCount - 1 || this.fpSpread1_Sheet2.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            int rowCount = this.fpSpread1_Sheet2.RowCount - 1;
                            if (!string.IsNullOrEmpty(this.sumDetailColIndexs) && (this.sumType == "Both" || this.sumType == "Det"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount  <= 1)
                                {
                                    continue;
                                }
                            }
                            string formul = "AVERAGE(" + letter + "1:" + letter + rowCount.ToString() + ")";
                            
                            int decimalPlaces =0;
                            if (colLetter.Split('|').Length > 1)
                            {
                                decimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[1]);
                            }
                            if (colLetter.Split('|').Length > 2 && colLetter.Split('|')[2] == "%")
                            {
                                FarPoint.Win.Spread.CellType.PercentCellType p = new FarPoint.Win.Spread.CellType.PercentCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].CellType = p;
                            }
                            else
                            {
                                FarPoint.Win.Spread.CellType.NumberCellType p = new FarPoint.Win.Spread.CellType.NumberCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].CellType = p;
                         
                            }
                            this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].Formula = formul;
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n��ƽ����ʱ�Ѿ�������" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
                if (this.avgColIndexs[0] != '0')
                {
                    this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, 0].Value = "ƽ����";
                }
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "��ܰ��ʾ", ex.Message + "\n���ʱ�Ѿ�����");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        private int formulaTot()
        {
            try
            {
                if (this.formulaColIndexs == null || this.formulaColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.formulaType == "Tot" || this.formulaType == "Both")
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = formulaColIndexs.Split(' ', ',');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[0]);
                            if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            int rowCount = this.fpSpread1_Sheet1.RowCount - 1;
                            if (!string.IsNullOrEmpty(this.sumColIndexs) && (this.sumType == "Both" || this.sumType == "Tot"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount <= 1)
                                {
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(this.avgColIndexs) && (this.sumType == "Both" || this.sumType == "Tot"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount <= 1)
                                {
                                    continue;
                                }
                            }
                            //��ʽ����
                            //0|sum(A)��ʾ�ڵ�0������A�ĺ�
                            string formula = "";
                            if (colLetter.Split('|').Length > 1)
                            {
                                formula = colLetter.Split('|')[1];
                                formula = formula.ToUpper();
                                for (int col = 0; col < this.fpSpread1_Sheet1.ColumnCount; col++)
                                {
                                    string colstr = ((char)(col + 65)).ToString().ToUpper();
                                    if (formula.Contains("(" + colstr + ")"))
                                    {
                                        formula = formula.Replace("(" + colstr + ")", "(" + colstr + "1:" + colstr + rowCount.ToString() + ")");
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(formula))
                            {
                                int decimalPlaces = 0;
                                if (colLetter.Split('|').Length > 2)
                                {
                                    decimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[2]);
                                }
                                if (colLetter.Split('|').Length > 3 && colLetter.Split('|')[3] == "%")
                                {
                                    FarPoint.Win.Spread.CellType.PercentCellType p = new FarPoint.Win.Spread.CellType.PercentCellType();
                                    p.DecimalPlaces = decimalPlaces;
                                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;
                                }
                                else
                                {
                                    FarPoint.Win.Spread.CellType.NumberCellType p = new FarPoint.Win.Spread.CellType.NumberCellType();
                                    p.DecimalPlaces = decimalPlaces;
                                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;

                                }
                                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].Formula = formula;
                                if (colLetter.Split('|').Length > 4)
                                {
                                    try
                                    {
                                        string t = (colLetter.Split('|')[4]).ToString();
                                        if (t.Split('.').Length > 1)
                                        {
                                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, FS.FrameWork.Function.NConvert.ToInt32(t.Split('.')[0])].Value = (t.Split('|')[1]).ToString();
                                        }
                                        else
                                        {
                                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value = t;
                                        }
                                    }
                                    catch(Exception e)
                                    {
                                        this.ShowBalloonTip(5000, "��ܰ��ʾ", e.Message + "\n������˾˵������" + colLetter);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n��ʽ�����Ѿ�������" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
               
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "��ܰ��ʾ", ex.Message + "\n���ʱ�Ѿ�����");
                return -1;
            }
            return 0;
        }



        /// <summary>
        /// ����
        /// </summary>
        private void sortTot()
        {
            if (this.SortColIndexs == null || this.SortColIndexs == string.Empty)
            {
                return;
            }
            string[] cols = this.SortColIndexs.Split(' ', ',', '|');
            foreach (string colLetter in cols)
            {
                try
                {
                    if (string.IsNullOrEmpty(colLetter))
                    {
                        continue;
                    }
                    int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                    if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1)
                    {
                        continue;
                    }
                    this.fpSpread1_Sheet1.Columns[colIndex].AllowAutoSort = true;
                    this.fpSpread1_Sheet1.Columns[colIndex].ShowSortIndicator = true;
                }
                catch(Exception ex)
                {
                    this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n����ʱ�Ѿ�������" + colLetter);
                }
            }
        }

        /// <summary>
        /// ��ϸ����
        /// </summary>
        private void sortDetail()
        {
            if (this.DetailSortColIndexs == null || this.DetailSortColIndexs == string.Empty)
            {
                return;
            }
            string[] cols = this.DetailSortColIndexs.Split(' ', ',', '|');
            foreach (string colLetter in cols)
            {
                try
                {
                    if (string.IsNullOrEmpty(colLetter))
                    {
                        continue;
                    }
                    int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                    if (colIndex > this.fpSpread1_Sheet2.ColumnCount - 1)
                    {
                        continue;
                    }
                    this.fpSpread1_Sheet2.Columns[colIndex].AllowAutoSort = true;
                    this.fpSpread1_Sheet2.Columns[colIndex].ShowSortIndicator = true;
                }
                catch (Exception ex)
                {
                    this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n��ϸ����ʱ�Ѿ�������" + colLetter);
                    continue;
                }
            }
        }

        /// <summary>
        /// �ϲ�����
        /// </summary>
        private void mergeData()
        {
            if (!this.IsNeedMergeData)
            {
                return;
            }
            if (this.MergeDataColIndexs == null || this.MergeDataColIndexs == string.Empty)
            {
                this.ShowBalloonTip(5000, "��ܰ��ʾ", "û��ָ���ϲ����ݵ���");
            }
            string[] mergeCols = this.MergeDataColIndexs.Split(' ',',','|');
            foreach (string colLetter in mergeCols)
            {
                try
                {
                    if (string.IsNullOrEmpty(colLetter))
                    {
                        continue;
                    }
                    int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                    if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1)
                    {
                        continue;
                    }
                    this.fpSpread1_Sheet1.Columns[colIndex].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                }
                catch (Exception ex)
                {
                    this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n�ϲ�����ʱ�Ѿ�������" + colLetter);
                    continue;
                }
            }
        }
        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="activeSheetIndex"></param>
        private void readSetting(int activeSheetIndex)
        {
            if (activeSheetIndex == 0)
            {
                if (this.SettingFilePatch != string.Empty)
                {
                    if (System.IO.File.Exists(this.SettingFilePatch))
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.SettingFilePatch);
                    }
                }
            }
            if (activeSheetIndex == 1)
            {
                if (this.DetailSettingFilePatch != string.Empty)
                {
                    if (System.IO.File.Exists(this.DetailSettingFilePatch))
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet2, this.DetailSettingFilePatch);
                    }
                }
            }
        }

        private void Filter(string filterCode)
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("filter");
            }


            if (this.filerType == EnumFilterType.������)
            {
                return;
            }
            if (this.filerType == EnumFilterType.���ܹ��� || this.filerType == EnumFilterType.������ϸͬʱ����)
            {
                if (this.filters == null || this.filters.Length == 0)
                {
                    this.ShowBalloonTip(5000, "��ܰ��ʾ", "û�����ù����ֶ�\n�Ѿ�����");
                    return;
                }
                string filter = "1=1";
                string[] tmpFelters = filters.Split(',', ' ', '|');
                foreach (string field in tmpFelters)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        continue;
                    }
                    try
                    {
                        if (this.fpSpread1_Sheet1.DataSource != null)
                        {
                            this.dataView.RowFilter = "(" + field + " LIKE '%" + filterCode + "%') ";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n�Ѿ�����" + field);
                        continue;
                    }
                    if (filter == "1=1")
                    {
                        filter = "(" + field + " LIKE '%" + filterCode + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + filterCode + "%') ";
                    }
                    this.dataView.RowFilter = filter;

                    this.readSetting(0);
                }
                try
                {
                    if (this.fpSpread1_Sheet1.DataSource != null)
                    {
                        this.dataView.RowFilter = filter;
                    }
                }
                catch (Exception ex)
                {
                    this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n�Ѿ�����");
                }
            }
            if (this.filerType == EnumFilterType.��ϸ���� || this.filerType == EnumFilterType.������ϸͬʱ����)
            {
                if (this.detailFilters == null || this.detailFilters.Length == 0)
                {
                    this.ShowBalloonTip(5000, "��ܰ��ʾ", "û��������ϸ�����ֶ�\n�Ѿ�����");
                    return;
                }
                string filter = "1";
                string[] tmpFelters = detailFilters.Split(',', ' ', '|');
                foreach (string field in tmpFelters)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        continue;
                    }
                    try
                    {
                        if (this.fpSpread1_Sheet2.DataSource != null)
                        {
                            this.detailDataView.RowFilter = "(" + field + " LIKE '%" + filterCode + "%') ";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n�Ѿ�����" + field);
                        continue;
                    }
                    if (filter == "1")
                    {
                        filter = "(" + field + " LIKE '%" + filterCode + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + filterCode + "%') ";
                    }
                }
                try
                {
                    if (this.fpSpread1_Sheet2.DataSource != null)
                    {
                        this.detailDataView.RowFilter = filter;
                        this.readSetting(1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(filter + ex.Message);
                }
            }


            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("filter");
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.setControlWithPerporty();

            this.panelPrint.BackColor = System.Drawing.Color.White;

            //����
            if (this.initDeptByPriPower() == -1)
            {
                return -1;
            }

            //��ѯʱ��
            if (this.initTime() == -1)
            {
                return -1;
            }

            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            
            //Ĭ��������ѯ
            if (this.queryDataWhenInit)
            {
                this.queryData();
            }

            return 0;


        }

        /// <summary>
        /// ��ȡ��ѯ����
        /// �����������ⲿʵ��
        /// </summary>
        /// <returns></returns>
        public string[] GetQueryConditions()
        {
            if (this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "AAAA", "AAAA", "" };
                if (this.cmbNurse.Tag != null && this.cmbNurse.Tag.ToString() != ""&&!string.IsNullOrEmpty(this.cmbNurse.Text))
                {
                    parm[0] = this.cmbNurse.Tag.ToString();
                }
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != ""&&!string.IsNullOrEmpty(this.cmbDept.Text))
                {
                    parm[1] = this.cmbDept.Tag.ToString();
                }
                parm[2] = this.dtStart.Value.ToString();
              
                return parm;
            }
            if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "" };
                parm[0] = this.dtStart.Value.ToString();
                return parm;
            }
            if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
            {
                string[] parm = { "" };
                if (this.cmbNurse.Tag != null && this.cmbNurse.Tag.ToString() != "")
                {
                    parm[0] = this.cmbNurse.Tag.ToString();
                }

                return parm;
            }
            string[] parmNull = { "Null", "Null", "Null" };
            return parmNull;
        }

        /// <summary>
        /// Ĭ������
        /// 1��û�и��ӱ���
        /// 2������Ҫ��ϸ����
        /// 3��farPoint����
        /// 4����ѯ����panelһ�пؼ�
        /// </summary>
        public void SetDefaultSetting()
        {
            this.IsNeedAdditionTitle = false;
            this.IsNeedDetailData = false;
            this.fpSpread1_Sheet1.DefaultStyle.Locked = true;
            this.fpSpread1_Sheet2.DefaultStyle.Locked = true;
            this.FilerType = EnumFilterType.������;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet2.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet2.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;

            this.panelQueryConditions.Height = 40;
        }

        /// <summary>
        /// ���ݲ�ѯ
        /// </summary>
        /// <returns></returns>
        public int QueryData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("query");
            }
            int parm = this.queryData();

            this.resetTitleLocation();

            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("query");
            }
            return parm;
        }

        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <returns></returns>
        public int ExportData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("export");
            }
            int parm = this.exportData();
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("export");
            }
            return parm;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns></returns>
        public int PrintData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("print");
            }
            int parm = this.printData();
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("print");
            }
            return parm;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="paperName">ֽ������</param>
        /// <param name="paperWidth">���</param>
        /// <param name="paperHeight">�߶�</param>
        /// <returns></returns>
        public int PrintData(string paperName, int paperWidth, int paperHeight)
        {
            return this.PrintData(paperName, paperWidth, paperHeight,this.PrinterName);
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="paperName">ֽ������</param>
        /// <param name="paperWidth">���</param>
        /// <param name="paperHeight">�߶�</param>
        /// <param name="printerName">��ӡ������</param>
        /// <returns></returns>
        public int PrintData(string paperName, int paperWidth, int paperHeight, string printerName)
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("print");
            }
            int parm = this.printData(paperName, paperWidth, paperHeight, printerName);
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("print");
            }
            return parm;
        }




        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public int SumTot()
        {
            return this.sumTot();
        }

        /// <summary>
        /// ��ϸ���
        /// </summary>
        /// <returns></returns>
        public int SumDetail()
        {
            return this.sumDetail();
        }

        /// <summary>
        /// ������ƽ��
        /// </summary>
        /// <returns></returns>
        public int AverageTot()
        {
            return this.avgTot();
        }

        /// <summary>
        /// ��ϸ��ƽ��
        /// </summary>
        /// <returns></returns>
        public int AverageDetail()
        {
            return this.avgDetail();
        }


        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public void ShowBalloonTip(int timeOut, string title, string tipText)
        {
            this.FindForm().ShowInTaskbar = true;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(timeOut, title, tipText, ToolTipIcon.Info);
        }
        #endregion

        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.Init();
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.fpSpread1.ActiveSheetChanged += new EventHandler(fpSpread1_ActiveSheetChanged);
            base.OnLoad(e);
        }

        void fpSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            this.resetTitleLocation();
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (e.View.ActiveSheetIndex == 0)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.SettingFilePatch);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet2, this.DetailSettingFilePatch);
            }

            this.resetTitleLocation();
        }
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.detailDataQueryType != EnumQueryType.ͬ����ͬ����ѯ && e.View.ActiveSheetIndex == 0)
            {
                this.queryDetailData();
                this.fpSpread1.ActiveSheetIndex = 1;
            }
            if (this.DetailDoubleClickEnd != null)
            {
                DetailDoubleClickEnd();
            }
        }

      

        #endregion

        #region ��������Ϣ

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();
            if (QueryEndHandler != null)
            {
                QueryEndHandler();
            }
            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportData();
            return base.Export(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            this.PrintData();
            return base.Print(sender, neuObject);
        }
        #endregion

        #region ö��

        /// <summary>
        /// ��������
        /// </summary>
        public enum EnumFilterType
        {
            /// <summary>
            /// ���ܹ���
            /// </summary>
            ���ܹ���,

            /// <summary>
            /// ��ϸ����
            /// </summary>
            ��ϸ����,

            /// <summary>
            /// ������ϸͬʱ����
            /// </summary>
            ������ϸͬʱ����,

            /// <summary>
            /// ������
            /// </summary>
            ������
        };

        /// <summary>
        /// ��ѯ����
        /// </summary>
        public enum EnumQueryType
        {
            ͬ����ͬ����ѯ,
            ���ȡ����,
            ͬ�����첽��ѯ,
            �����������
        }

        ///// <summary>
        ///// ֽ��
        ///// </summary>
        //public struct Paper
        //{
        //    public Paper()
        //    {}
        //    /// <summary>
        //    /// ֽ������
        //    /// </summary>
        //    public string Name;

        //    /// <summary>
        //    /// ֽ�ſ��
        //    /// </summary>
        //    public int With;

        //    /// <summary>
        //    /// ֽ�Ÿ߶�
        //    /// </summary>
        //    public int Height;
        //}
        #endregion
    }

}
