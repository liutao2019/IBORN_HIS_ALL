using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// ucPrivePowerReport<br></br>
    /// [��������: ucPrivePowerReport����Ȩ�ޱ���]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2008-9-6]<br></br>
    /// <�޸ļ�¼  {85997F7C-0E19-46e8-B552-2A60009747B4}
    ///		�޸���='����' 
    ///		�޸�ʱ��='2010-05-18' 
    ///		�޸�Ŀ��='�����ճ�����5.0'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPrivePowerReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrivePowerReport()
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
        /// �������[PriveDeptָ������Ȩ�ޣ�CommonDeptָ���������AllDeptȫ������]
        /// Ĭ��0��1ָ����0��Ч��2ָ����0��1��Ч
        /// </summary>
        public string DeptType = "PriveDept";

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
        [Description("�����ַ������������ϸ��ѯ�����ǻ��ܹ���"), Category("����"),Browsable(true)]
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
        [Description("�����ַ�������ϸ����"), Category("����"), Browsable(true)]
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
        [Description("���˷�ʽ"), Category("����"), Browsable(true), DefaultValue(EnumFilterType.������)]
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
        #endregion

        #region ����Ȩ��
        /// <summary>
        /// ����Ȩ���ַ���
        /// </summary>
        private string priveClassTwos = "";

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Description("����Ȩ�ޱ��룬��ֵ�󽫼�����ԱȨ��"), Category("����"), Browsable(true)]
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
        [Description("������"), Category("��������"), Browsable(true)]
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
        [Description("������ռ�߶�"), Category("�߼�����"), Browsable(true)]
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
        [Description("�����������С"), Category("��������"), Browsable(true)]
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
        [Description("��������������"), Category("��������"), Browsable(true)]
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
        [Description("���ӱ���ռ�߶�"), Category("�߼�����"), Browsable(true)]
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
        [Description("���ӱ��������С"), Category("��������"), Browsable(true)]
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
        [Description("���ӱ�����������"), Category("��������"), Browsable(true)]
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
        [Description("���ӱ���[��]"), Category("��������"), Browsable(true)]
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
        [Description("���ӱ���[��]"), Category("��������"), Browsable(true)]
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
        [Description("���ӱ���[��]"), Category("��������"), Browsable(true)]
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
        [Description("�Ƿ���Ҫ���ӱ���"), Category("��������"), Browsable(true), DefaultValue(true)]
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
        [Description("��ʼʱ��ͽ���ʱ��������"), Category("�߼�����"), Browsable(true), DefaultValue(0)]
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
        [Description("�Ƿ���Ҫ��ʱ�����ó�����"), Category("�߼�����"), Browsable(true), DefaultValue(true)]
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
        [Description("�Ƿ���Ҫ��ʱ����Ϊ��ѯ����"), Category("����"), Browsable(true), DefaultValue(true)]
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
        [Description("�Ƿ���Ҫ��������Ϊ��ѯ����"), Category("����"), Browsable(true), DefaultValue(false)]
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
        [Description("�������Ƿ�����á�ȫ������ѯ����Ȩ�޿���"), Category("�߼�����"), Browsable(true), DefaultValue(false)]
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
        [Description("�������ָ�������������Ȩ����Ч"), Category("�߼�����"), Browsable(true)]
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
        #endregion

        #region ��ѯ

        /// <summary>
        /// ��ʼ��ʱ�Ƿ���Ĭ��������ѯ
        /// </summary>
        private bool queryDataWhenInit = true;

        /// <summary>
        /// ��ʼ��ʱ�Ƿ���Ĭ��������ѯ[ֻд]
        /// </summary>
        [Description("��ʼ��ʱ��ѯ����"), Category("�߼�����"), Browsable(true), DefaultValue(true)]
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
        [Description("����SQL��ID"), Category("����"), Browsable(true)]
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
        [Description("�Ƿ���Ҫ��ѯ��ϸ"), Category("����"), Browsable(true), DefaultValue(false)]
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
        [Description("��ϸSQL��ID"), Category("����"), Browsable(true)]
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
        [Description("��ϸ���ݲ�ѯ��ʽ"), Category("�߼�����"), Browsable(true), DefaultValue(EnumQueryType.���ȡ����)]
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
        [Description("��ϸ��ѯʱ��Ϊ������FarPint������"), Category("�߼�����"), Browsable(true)]
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

        #region �������

        /// <summary>
        /// �ϼ��е�����
        /// </summary>
        private string sumColIndexs = "";

        /// <summary>
        /// �ϼ��е�����
        /// </summary>
        [Description("�����"), Category("����"), Browsable(true)]
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
        [Description("��ϸ�����"), Category("����"), Browsable(true)]
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
        /// ������
        /// </summary>
        private string sortColIndexs = "";

        /// <summary>
        /// ������
        /// </summary>
        [Description("������"), Category("����"), Browsable(true)]
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
        [Description("��ϸ������"), Category("����"), Browsable(true)]
        public string DetailSortColIndexs
        {
            get { return detailSortColIndexs; }
            set { detailSortColIndexs = value; }
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
        [Description("ָ���ϲ����ݵ���,����Sheet1��Ч"), Category("�߼�����"), Browsable(true)]
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
        [Description("Sheet1�����Ƿ���Ҫ�ϲ�����"), Category("�߼�����"), Browsable(true), DefaultValue(false)]
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
        [Description("ָ�������������У�SQLIndexs��һ��SQL��Ϊ�������ֶ����"), Category("�߼�����"), Browsable(true)]
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
        [Description("�������ݣ�ΪTrueʱָ��CrossDataColIndexs����һ�����SQL�����ڵ�һ��"), Category("�߼�����"), Browsable(true), DefaultValue(false)]
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
        [Description("��ӡ�Ƿ���ҪԤ��"), Category("��ӡ����"), Browsable(true), DefaultValue(true)]
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
        [Description("��ӡֽ�ų��ȸ��������Զ�����"), Category("��ӡ����"), Browsable(true), DefaultValue(true)]
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
        [Description("��ӡֽ�ų��ȸ��Ӹ߶�"), Category("��ӡ����"), Browsable(true)]
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
        [Description("��ӡֽ��"), Category("��ӡ����"), Browsable(true)]
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
        [Description("ֽ�����ؿ��"), Category("��ӡ����"), Browsable(true)]
        public int PaperWith
        {
            get { return paperWith; }
            set { paperWith = value; }
        }

        /// <summary>
        /// ֽ�Ÿ߶�
        /// </summary>
        private int paperHeight = 760;

        /// <summary>
        /// ֽ�Ÿ߶�
        /// </summary>
        [Description("ֽ�����ظ߶�"), Category("��ӡ����"), Browsable(true)]
        public int PaperHeight
        {
            get { return paperHeight; }
            set { paperHeight = value; }
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
        [Description("��ϸ�����ļ�"), Category("�����ļ�"), Browsable(true)]
        public string DetailSettingFilePatch
        {
            get
            {
                if (this.detailSettingFilePatch == string.Empty)
                {
                    return Application.StartupPath + @"\Profile\" + this.FindForm().Text + "detail.xml";
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
        [Description("�����ļ�"), Category("�����ļ�"), Browsable(true)]
        public string SettingFilePatch
        {
            get 
            {
                if (this.settingFilePatch == string.Empty)
                {
                    return Application.StartupPath + @"\Profile\" + this.FindForm().Text + "tot.xml";
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
        [Description("��ͷ�ɼ���"), Category("�߼�����"), Browsable(true), DefaultValue(true)]
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
        [Description("��ͷ��"), Category("�߼�����"), Browsable(true)]
        public float ColumnHeaderHeight
        {
            get { return columnHeaderHeight; }
            set { columnHeaderHeight = value; }
        }
        
        /// <summary>
        /// ��ϸ��ͷ�ɼ���
        /// </summary>
        private bool detailRowHeaderVisible = true;

        /// <summary>
        /// ��ϸ��ͷ�ɼ���
        /// </summary>
        [Description("��ϸ��ͷ�ɼ���"), Category("�߼�����"), Browsable(true), DefaultValue(true)]
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
        [Description("��ͷ��"), Category("�߼�����"), Browsable(true)]
        public float DetailColumnHeaderHeight
        {
            get { return detailColumnHeaderHeight; }
            set { detailColumnHeaderHeight = value; }
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
            //����
            if (this.FilerType == EnumFilterType.������)
            {
                if (this.panelQueryConditions.Controls.IndexOf(this.panelFilter) >= 0)
                {
                    this.panelQueryConditions.Controls.Remove(this.panelFilter);
                }
            }
            else
            {
                if (this.panelQueryConditions.Controls.IndexOf(this.panelFilter) < 0)
                {                   
                    this.panelQueryConditions.Controls.Add(this.panelFilter);
                }
            }

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

            //ʱ��
            if (!this.IsTimeAsCondition)
            {
                this.panelQueryConditions.Controls.Remove(this.panelTime);
            }
            //else
            //{
            //    this.panelQueryConditions.Controls.Add(this.panelTime);
            //}

            //����
            if (!this.IsDeptAsCondition)
            {
                this.panelQueryConditions.Controls.Remove(this.panelDept);
            }
            //else
            //{
            //    this.panelQueryConditions.Controls.Add(this.panelDept); 
            //}

            //��ѯ
            if (!this.IsNeedDetailData)
            {
                this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet2);
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
            else if (this.DeptType == "CommonDept")
            {
                foreach (FS.HISFC.Models.Base.EnumDepartmentType deptType in this.deptTypes)
                {

                    alDept.AddRange(deptMgr.GetDeptment(deptType));
                }
            }
            else
            {
                //ÿһ����Ȩ�޶�Ӧ����
                System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> alTmpDept;

                //���ݶ���Ȩ�޻�ȡ������Ϣ
                string[] prives = priveClassTwos.Split(',',' ','|');
                foreach (string classTwoCode in prives)
                {
                    alTmpDept = new System.Collections.Generic.List<FS.FrameWork.Models.NeuObject>();
                    alTmpDept = this.priPowerMgr.QueryUserPriv(FS.FrameWork.Management.Connection.Operator.ID, classTwoCode);
                    if (alTmpDept == null || alTmpDept.Count == 0)
                    {
                        continue;
                    }
                    alDept.AddRange(alTmpDept);
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
            foreach (FS.FrameWork.Models.NeuObject dept in hsDept.Values)
            {
                alDept.Add(dept);
            }
            FS.FrameWork.Models.NeuObject deptTmp;

            //��ӡ�ȫ����
            if (alDept.Count > 0)
            {
                if (this.isAllowAllDept)
                {
                    deptTmp = new FS.FrameWork.Models.NeuObject();
                    deptTmp.ID = "All";
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
            this.cmbDept.AddItems(alDept);

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
            if (this.isNeedIntedDays)
            {
                this.dtEnd.Value = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                this.dtStart.Value = this.dtEnd.Value.AddSeconds(1);
            }
            else
            {
                this.dtEnd.Value = dt;
            }
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
            if (this.detailDataQueryType == EnumQueryType.ͬ����ͬ����ѯ)
            {
                this.queryDetailData();
            }
            if (this.LeftAdditionTitle.IndexOf("ͳ��ʱ��") != -1)
            {
                this.lbAdditionTitleLeft.Text = "ͳ��ʱ��:" + this.dtStart.Value.ToString()
                + " �� " + this.dtEnd.Value.ToString();
            }
            if (LeftAdditionTitle.IndexOf("  ͳ������") != -1)
            {
                this.lbAdditionTitleLeft.Text = "  ͳ������:" + this.dtStart.Value.ToString("yyyy��MM��dd��")
                + " �� " + this.dtEnd.Value.ToString("yyyy��MM��dd��");
            }
            if (this.cmbDept.Text != "ȫ��")
            {
                if (this.MidAdditionTitle.IndexOf("����") != -1)
                {
                    this.lbAdditionTitleMid.Text = "����:" + this.cmbDept.Text;
                }
                if (this.MidAdditionTitle.IndexOf("����") != -1)
                {
                    this.lbAdditionTitleMid.Text = "����:" + this.cmbDept.Text;
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
            string[] parm = this.getDetailParm();
            if (this.priPowerMgr.ExecQuery(this.detailSQLIndexs.Split(' ',',','|'), ref ds, parm) == -1)
            {
                this.ShowBalloonTip(1000, "��ܰ��ʾ", "��������" + this.priPowerMgr.Err);
                return -1;
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
                return this.printData(this.PaperName, this.PaperWith, this.PaperHeight);
            }
            return 0;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="paperName">ֽ������</param>
        /// <param name="paperWidth">���</param>
        /// <param name="paperHeight">�߶�</param>
        /// <returns></returns>
        private int printData(string paperName, int paperWidth, int paperHeight)
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
            PrintObject.SetPageSize(new System.Drawing.Printing.PaperSize(paperName, paperWidth, paperHeight));
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
        private System.Drawing.Printing.PaperSize getPaperSize()
        {
            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.PaperName = this.priPowerMgr.GetDateTimeFromSysDateTime().ToString();
            try
            {
                int width = 800;

                int curHeight = this.Height;

                int addHeight = (this.fpSpread1.ActiveSheet.RowCount - 1) *
                    (int)this.fpSpread1.ActiveSheet.Rows[0].Height;

                int additionAddHeight = this.PaperAddHeight;

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
        private void resetTitleLocation()
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
                with += (int)this.fpSpread1.ActiveSheet.Columns[col].Width;
            }
            if (with > this.panelTitle.Width)
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
                                break;
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
                    string[] cols = this.sumDetailColIndexs.Split(' ', ' ', '|');
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
                                break;
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
            this.txtFilter.TextChanged += new EventHandler(txtFilter_TextChanged);

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
                string[] parm = { "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.dtStart.Value.ToString();
                parm[2] = this.dtEnd.Value.ToString();

                return parm;
            }
            if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "" };
                parm[0] = this.dtStart.Value.ToString();
                parm[1] = this.dtEnd.Value.ToString();

                return parm;
            }
            if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
            {
                string[] parm = { "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
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
            if (this.dtStart.Value > this.dtEnd.Value)
            {
                MessageBox.Show( "��ѯ��ʼʱ�䲻�ܴ��ڽ�ֹʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return -1;
            }

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
            int parm = this.printData(paperName, paperWidth, paperHeight);
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
        /// ��ʾ��Ϣ
        /// </summary>
        public void ShowBalloonTip(int timeOut, string title, string tipText)
        {
            this.FindForm().ShowInTaskbar = true;
        }
        #endregion

        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
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

        void txtFilter_TextChanged(object sender, EventArgs e)
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
                        this.dataView.RowFilter = "(" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                    catch (Exception ex)
                    {
                        this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n�Ѿ�����" + field);
                        continue;
                    }
                    if (filter == "1=1")
                    {
                        filter = "(" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                    this.dataView.RowFilter = filter;

                    this.readSetting(0);
                }
                try
                {
                    this.dataView.RowFilter = filter;
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
                string[] tmpFelters = detailFilters.Split(',',' ','|');
                foreach (string field in tmpFelters)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        continue;
                    }
                    try
                    {
                        this.detailDataView.RowFilter = "(" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                    catch (Exception ex)
                    {
                        this.ShowBalloonTip(5000, "��ܰ��ʾ", ex.Message + "\n�Ѿ�����" + field);
                        continue;
                    }
                    if (filter == "1")
                    {
                        filter = "(" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                }
                try
                {
                    this.detailDataView.RowFilter = filter;
                    this.readSetting(1);
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
