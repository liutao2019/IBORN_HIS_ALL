using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Classes;
using System.Collections;
using System.Xml;
using FarPoint.Win.Spread;
namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// [��������: ��Ŀ�б�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucItemList : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
       
        public ucItemList()
        {
            InitializeComponent();
            InitDataSet();

            fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
        }

        private string curFilterStr = string.Empty;

        public string CurFilterStr
        {
            get
            {
                return curFilterStr;
            }
            set
            {
                curFilterStr = value;
            }
        }

        string showSettingXML = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "InpatientChargeFeeItem.xml";

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(fpSpread1_Sheet1, showSettingXML);
        }

        public ucItemList(EnumShowItemType type)
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();

            // TODO: �� InitializeComponent ���ú�����κγ�ʼ��


            InitDataSet();
            //������Ŀ
            _enuShowItemType = type;
            Init();
        }

        #region ����
        //��Ŀ����
        private ArrayList _alItems = new ArrayList();
        //��Ŀdataset
        private DataSet _dsItems;
        private DataView _dvItems;
        //��ʾ��Ŀ��Χ
        protected EnumShowItemType _enuShowItemType = EnumShowItemType.All;
        /// <summary>
        /// �����ص�ҩƷ����// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        private string noAddDrugQuality = "";
        //��ѯ��ʽ0:ƴ���� 1:����� 2:�Զ����� 3:������ 4:Ӣ��
        private int _InputType = 0;
        /// <summary>
        /// �������ڿ���
        /// </summary>
        private string patientDept = string.Empty;
        /// <summary>
        /// �Ƿ���������
        /// </summary>
        private bool isUseRetailPrice2 = false;
        /// <summary>
        /// ��¼����
        /// </summary>
        FS.FrameWork.Models.NeuObject deptLogin = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept;
        //dataview�еĵ�ǰ��
        private int _CurrentRow = -1;
        public delegate int MyDelegate(Keys key);
        /// <summary>
        /// ˫�����س���Ŀ�б�ʱִ�е��¼�
        /// </summary>
        public event MyDelegate SelectItem;
        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlArguments = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        //{4AAE2DA2-80C2-4297-A0F2-7E314F8BBF6D}
        /// <summary>
        /// ������Ŀ�߳�
        /// </summary>
        System.Threading.Thread threadItem;
        //����fp״̬
        delegate void InitFpStateHander();
        /// <summary>
        /// ����{2A5608D8-26AD-47d7-82CC-81375722FF72}
        /// </summary>
        bool value = false;
        #endregion

        #region ����
        /// <summary>
        /// ��ʾ��Ŀ��Χ
        /// </summary>
        public EnumShowItemType enuShowItemType
        {
            get
            {
                return _enuShowItemType;
            }
            set
            {
                _enuShowItemType = value;
            }
        }

        /// <summary>
        /// �����ص�ҩƷ����// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        public string NoAddDrugQuality
        {
            get
            {
                return noAddDrugQuality;
            }
            set
            {
                noAddDrugQuality = value;
            }
        }
        /// <summary>
        /// ��ȡ��ѯ��ʽ0:ƴ���� 1:����� 2:�Զ����� 3:������ 4:Ӣ��
        /// </summary>
        public int InputType
        {
            get
            {
                return _InputType;
            }
        }
      
        /// <summary>
        /// ���߿��ұ���
        /// </summary>
        public string PatientDept
        {
            get { return patientDept; }
            set { patientDept = value;
            this.Init(value);
        }
        }

        /// <summary>
        /// {112B7DB5-0462-4432-AD9D-17A7912FFDBE}  ��ȡ��Ŀҽ����ǽӿ� 
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade iGetSiFlag = null;

        /// <summary>
        /// {112B7DB5-0462-4432-AD9D-17A7912FFDBE}  ��ȡ��Ŀҽ����ǽӿ� 
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade IGetSiFlag
        {
            get
            {
                if (this.iGetSiFlag == null)
                {
                    this.iGetSiFlag = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade)) as FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade;
                }

                return this.iGetSiFlag;
            }
        }

        #endregion

        #region ����

        /// <summary>
        ///��ʼ���õ���
        /// </summary>
        /// <returns></returns>
        protected virtual  int Init()
        {
            //{4AAE2DA2-80C2-4297-A0F2-7E314F8BBF6D}
            threadItem = new System.Threading.Thread(new System.Threading.ThreadStart(InitItem));
            threadItem.IsBackground = true;
            threadItem.Start();
            this.FindForm().FormClosing += new FormClosingEventHandler(ucItemList_FormClosing);
            //{2A5608D8-26AD-47d7-82CC-81375722FF72}
            value = controlArguments.GetControlParam<bool>("201026");

            isUseRetailPrice2 = controlArguments.GetControlParam<bool>("HNPHA2");

            if (System.IO.File.Exists(showSettingXML))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(fpSpread1_Sheet1, showSettingXML);
            }
            return 1;
        }
        //{4AAE2DA2-80C2-4297-A0F2-7E314F8BBF6D}
        void ucItemList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.threadItem.Abort();
        }

        //{4AAE2DA2-80C2-4297-A0F2-7E314F8BBF6D}
        /// <summary>
        /// ������Ŀ��Ϣ
        /// </summary>
        protected void InitItem()
        {
            int _intRtn;

            _intRtn = AddItemsToArrayList(patientDept);
            if (_intRtn == -1) return ;

            _intRtn = AddItemsToDataSet();
            if (_intRtn == -1) return ;
            //AddGroup(patientDept);
            InitFpStateHander OnSetFpState = new InitFpStateHander(InitFpState);
            this.Invoke(OnSetFpState);
        }

        //{4AAE2DA2-80C2-4297-A0F2-7E314F8BBF6D}
        /// <summary>
        /// ��ʼ��Fp״̬
        /// </summary>
        private void InitFpState()
        {
            if (_dvItems.Count == 0)
                _CurrentRow = -1;
            else
                _CurrentRow = 0;
            AddItemsToSheet(_CurrentRow);
            fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            fpSpread1_Sheet1.ActiveRowIndex = 0;
            DisplayCurrentRow(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientDept"></param>
        /// <returns></returns>
        public int Init(string patientDept)
        {
            //{4037E5B0-D19A-456d-A2AA-35CD9C8098EB}
            this.patientDept = patientDept;
            if (Init() == -1) return -1;
            return 1;
        }

        /// <summary>
        /// �õ������ҵ�������Ϣ
        /// </summary>
        /// <param name="deptId">���ұ���</param>
        /// <returns></returns>
        private List<FS.HISFC.Models.FeeStuff.MaterialItem> GetMateList(string deptId)
        {
            //����
            FS.HISFC.BizProcess.Integrate.Material.Material _meItems = new FS.HISFC.BizProcess.Integrate.Material.Material();
            List<FS.HISFC.Models.FeeStuff.MaterialItem> list = _meItems.QueryStockHeadItemForFee(deptId);
            if (list == null)
            {
                MessageBox.Show("����������Ŀʧ��" + _meItems.Err);
                return null;
            }
            return list;
        }

        /// <summary>
        /// ˢ��DataSet
        /// </summary>
        public int RefreshDataSet(string deptId)
        {
            //����������Ŀ
            DataRow[] vdr = _dsItems.Tables["items"].Select("isdrug = '6'");
            foreach (DataRow dr in vdr)
            {
                _dsItems.Tables["items"].Rows.Remove(dr);
            }
            for (int i = 0; i < _alItems.Count; i++)
            {
                if (_alItems[i].GetType() == typeof(FS.HISFC.Models.FeeStuff.MaterialItem))
                {
                    _alItems.RemoveAt(i);
                }
            }
            //����������Ŀ
            //List<FS.HISFC.Models.FeeStuff.MaterialItem> list = GetMateList(deptId);
            //_alItems.AddRange(list.ToArray());
            //ˢ��dataset
            //foreach (FS.HISFC.Models.FeeStuff.MaterialItem item in list)
            //{
            //    _dsItems.Tables["items"].Rows.Add(new Object[]{item.UserCode, //������
            //                                                           item.SpellCode,//ƴ����
            //                                                           item.WbCode,//�����
            //                                                            item.ID,//��Ŀ����
            //                                                            item.Name,//��Ŀ����
            //                                                            item.Specs,//���
            //                                                            item.Price,//�۸�
            //                                                            item.PriceUnit,//��λ
            //                                                            string.Empty,//��������
            //                                                            item.Memo,//��ע
            //                                                            string.Empty,//ҽ������
            //                                                            "6",//����
            //                                                            item.GbCode,//������
            //                                                            string.Empty,//Ƶ������
            //                                                            string.Empty,//�÷�����
            //                                                            item.SysClass.Name,//ϵͳ���
            //                                                            string.Empty,//Ӣ��
            //                                                            string.Empty,//���ѱ�־
            //                                                            item.Price,//��ͯ�۸�
            //                                                            item.Price//����۸�
                                                                        
            //            });
            //}
            return 1;
        }

        private void InitDataSet()
        {
            //��ʼ��
            //SetStyle(Function.CurrentPath + Function.SettingPath+"feeSetting.xml", "//Column", fpSpread1_Sheet1);
            SetStyle("", "", fpSpread1_Sheet1);
            SetAutoSize();
            _dsItems = new DataSet();
            _dsItems.Tables.Add("items");
            _dsItems.Tables["items"].Columns.AddRange(new DataColumn[]
				{
					new DataColumn("input_code",Type.GetType("System.String")),//������
					new DataColumn("spell_code",Type.GetType("System.String")),//ƴ����
					new DataColumn("wb_code",Type.GetType("System.String")),//�����
					new DataColumn("item_code",Type.GetType("System.String")),//��Ŀ����
					new DataColumn("item_name",Type.GetType("System.String")),//��Ŀ����
					new DataColumn("specs",Type.GetType("System.String")),//���
					new DataColumn("price",Type.GetType("System.String")),//�۸�
					new DataColumn("unit",Type.GetType("System.String")),//��λ
					new DataColumn("producer",Type.GetType("System.String")),//��������
					new DataColumn("memo",Type.GetType("System.String")),//��ע
					new DataColumn("grade",Type.GetType("System.String")),//ҽ������
					new DataColumn("isdrug",Type.GetType("System.String")),//1ҩ��2��ҩ
					new DataColumn("gb_code",Type.GetType("System.String")),//������
					new DataColumn("freq_name",Type.GetType("System.String")),//Ƶ������
					new DataColumn("usage_name",Type.GetType("System.String")),//�÷�����
					new DataColumn("class_code",Type.GetType("System.String")),//ϵͳ���
					new DataColumn("english_code",Type.GetType("System.String")),//Ӣ��
					new DataColumn("pub_grade",Type.GetType("System.String")),//���ѱ�־
					new DataColumn("child_price",Type.GetType("System.String")),//��ͯ�۸�
					new DataColumn("special_price",Type.GetType("System.String"))//����۸�
				});
            _dsItems.CaseSensitive = false;
            _dvItems = new DataView(_dsItems.Tables["items"]);
            GetQueryType();
        }

        enum Cols
        {
            input_code ,//������0
			spell_code ,//ƴ����1
			wb_code ,//�����2
			item_code ,//��Ŀ����3
			item_name ,//��Ŀ����4
			specs ,//���5
			price ,//�۸�6
			unit ,//��λ7
			producer ,//��������8
			memo ,//��ע9
			grade ,//ҽ������10
			isdrug ,//1ҩ��2��ҩ11
			gb_code ,//������12
			freq_name ,//Ƶ������13
			usage_name ,//�÷�����14
			class_code ,//ϵͳ���15
			english_code ,//Ӣ��16
			pub_grade ,//���ѱ�־17
			child_price ,//��ͯ�۸�18
			special_price ,//����۸�19
        }

        //{4AAE2DA2-80C2-4297-A0F2-7E314F8BBF6D}
        /// <summary>
        /// ��Ӳ���Ա���ڿ��ҵ�����
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        protected int AddGroup(string deptID)
        {
            //�������
            FS.HISFC.BizLogic.Manager.ComGroup group = new FS.HISFC.BizLogic.Manager.ComGroup();
            ArrayList al = group.GetValidGroupList(deptID);
            if (al == null) return 0;

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroup obj;
                obj = (FS.HISFC.Models.Fee.ComGroup)al[i];
                _dsItems.Tables["items"].Rows.Add(new Object[] {obj.inputCode,obj.spellCode,string.Empty,
																   obj.ID,obj.Name,obj.reMark,string.Empty,
																   "[����]",string.Empty,obj.reMark,
																   string.Empty,"3",string.Empty,
																   string.Empty,string.Empty,"����",
																   string.Empty,string.Empty,string.Empty,string.Empty });
            }

            _alItems.AddRange(al);

            //AddCompound();//��Ӹ�����

            //if (_dvItems.Count == 0)
            //    _CurrentRow = -1;
            //else
            //    _CurrentRow = 0;
            //AddItemsToSheet(_CurrentRow);
            //fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            //fpSpread1_Sheet1.ActiveRowIndex = 0;
            //DisplayCurrentRow(0);

            return 0;
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public int AddExamGroup(string deptID)
        {
            ////�������
            FS.HISFC.BizLogic.PhysicalExamination.Group group = new FS.HISFC.BizLogic.PhysicalExamination.Group();
            ArrayList al = group.QueryValidGroupList(deptID);
            if (al == null) return 0;

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.PhysicalExamination.Group obj;
                obj = (FS.HISFC.Models.PhysicalExamination.Group)al[i];
                _dsItems.Tables["items"].Rows.Add(new Object[] {obj.inputCode,obj.spellCode,obj.WBCode,
																	   obj.ID,obj.Name,string.Empty,string.Empty,
																	   "[�������]",string.Empty,string.Empty,
																	   string.Empty,"5",string.Empty,
																	   string.Empty,string.Empty,"�������",
																	   string.Empty,string.Empty,string.Empty,string.Empty });
            }

            _alItems.AddRange(al);

            //AddCompound();//��Ӹ�����

            //if (_dvItems.Count == 0)
            //    _CurrentRow = -1;
            //else
            //    _CurrentRow = 0;
            //AddItemsToSheet(_CurrentRow);
            //fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            //fpSpread1_Sheet1.ActiveRowIndex = 0;
            //DisplayCurrentRow(0);

            return 0;
        }
        #endregion

        #region ˽�к���
        /// <summary>
        /// ��Ӹ�����Ŀ
        /// </summary>
        /// <returns></returns>
        [Obsolete("����,�Ѿ��ϲ�����ҩƷ��",true)]
        private void AddCompound()
        {
            //��Ӹ�����Ŀ
            //FS.HISFC.BizLogic.Fee.UndrugComb compound = new FS.HISFC.BizLogic.Fee.UndrugComb();
            //ArrayList al = compound.QueryUndrugCombsValid();
            //if (al == null) return;
            //for (int i = 0; i < al.Count; i++)
            //{
            //    FS.HISFC.Models.Fee.Undrugztinfo obj;
            //    obj = (FS.HISFC.Models.Fee.Undrugztinfo)al[i];
            //    FS.HISFC.Models.Fee.Item.UndrugComb obj;
            //    obj = (FS.HISFC.Models.Fee.Item.UndrugComb)al[i];
            //    _dsItems.Tables["items"].Rows.Add(new Object[] {obj.UserCode, //0
            //                                                    obj.SpellCode,//1
            //                                                    obj.WBCode,//2
            //                                                    obj.Package.ID,//3
            //                                                    obj.Package.Name,//4
            //                                                    string.Empty,//5
            //                                                    string.Empty,//6
            //                                                   "[������]",//7
            //                                                    string.Empty,//8
            //                                                    string.Empty,//9
            //                                                    string.Empty,//10
            //                                                    "4",//11
            //                                                    string.Empty,//12
            //                                                    string.Empty,//13
            //                                                    string.Empty,//14
            //                                                    "������",//15
            //                                                    string.Empty,//16
            //                                                    string.Empty,//17
            //                                                    string.Empty,//18
            //                                                    string.Empty //19
            //    });
            //}
            //_alItems.AddRange(al);
        }
        /// <summary>
        /// �Զ�������Ŀ�б��С
        /// </summary>
        /// <returns></returns>
        private int SetAutoSize()
        {
            //���ÿ��
            int _intWidth = SystemInformation.Border3DSize.Width * 2;
            int i;

            _intWidth += (int)fpSpread1_Sheet1.RowHeader.Columns[0].Width;

            for (i = 0; i < fpSpread1_Sheet1.ColumnCount; i++)
            {
                if (fpSpread1_Sheet1.Columns[i].Visible)
                    _intWidth += (int)fpSpread1_Sheet1.Columns[i].Width;
            }

            //			_intWidth+=SystemInformation.VerticalScrollBarWidth;
            this.Width = _intWidth + 14;

            //���ø߶�
            //int _intHeight = SystemInformation.Border3DSize.Height * 2;
            //_intHeight += (int)fpSpread1_Sheet1.ColumnHeader.Rows[0].Height;

            //for (i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            //    _intHeight += (int)fpSpread1_Sheet1.Rows[i].Height;

            this.Height = 323;

            return 1;
        }
        /// <summary>
        /// �����Ŀ�б�_alItems
        /// </summary>
        /// <returns></returns>
        private int AddItemsToArrayList(string patientDept)
        {
            try
            {
                string[] drugQualityStr = null;
                drugQualityStr = this.noAddDrugQuality.Split(',');
                ArrayList newDrugInfoList = new ArrayList();
                //TODO: ����ҩƷ�ͷ�ҩƷ�б�
                if (_enuShowItemType == EnumShowItemType.Pharmacy)
                {
                    FS.HISFC.BizLogic.Pharmacy.Item _items = new FS.HISFC.BizLogic.Pharmacy.Item();
                    if (patientDept == string.Empty)
                    {
                        _alItems = new ArrayList(_items.QueryItemAvailableList().ToArray());
                        foreach (FS.HISFC.Models.Pharmacy.Item item in _alItems)
                        {
                            bool isAdd = true;
                            foreach (string str in drugQualityStr)
                            {
                                if (item.Quality.ID == str)
                                {
                                    isAdd = false;
                                }
                            }
                            if (isAdd)
                            {
                                newDrugInfoList.Add(item);
                            }
                        }
                        _alItems = newDrugInfoList;
                    }

                    else
                    {
                        _alItems = _items.QueryItemAvailableList(patientDept);
                        foreach (FS.HISFC.Models.Pharmacy.Item item in _alItems)
                        {
                            bool isAdd = true;
                            foreach (string str in drugQualityStr)
                            {
                                if (item.Quality.ID == str)
                                {
                                    isAdd = false;
                                }
                            }
                            if (isAdd)
                            {
                                newDrugInfoList.Add(item);
                            }
                        }
                        _alItems = newDrugInfoList;
                    }
                }
                else if (_enuShowItemType == EnumShowItemType.Undrug)
                {
                    FS.HISFC.BizLogic.Fee.Item _items = new FS.HISFC.BizLogic.Fee.Item();
                    _alItems = _items.QueryValidItems();
                    ////����
                    //List<FS.HISFC.Models.FeeStuff.MaterialItem> list = this.GetMateList((_items.Operator as FS.HISFC.Models.Base.Employee).Dept.ID);
                    //_alItems.AddRange(list.ToArray());
                }
                else if (_enuShowItemType == EnumShowItemType.DeptItem)
                {
                    FS.HISFC.BizLogic.Pharmacy.Item _items1 = new FS.HISFC.BizLogic.Pharmacy.Item();
                    ArrayList _al1 = new ArrayList();
                    _al1 = new ArrayList(_items1.QueryItemAvailableListDept(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID).ToArray());
                    FS.HISFC.BizLogic.Fee.Item _items2 = new FS.HISFC.BizLogic.Fee.Item();
                    _alItems = _al1;
                    ArrayList _al2 = new ArrayList(_items2.QueryValidItemsList(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID));
                    _alItems.AddRange(_al2);

                }

                else
                {
                    FS.HISFC.BizLogic.Pharmacy.Item _items1 = new FS.HISFC.BizLogic.Pharmacy.Item();
                    ArrayList _al1 = new ArrayList();// {4D67D981-6763-4ced-814E-430B518304E2}
                    if (patientDept == string.Empty)
                    {
                        _al1 = new ArrayList(_items1.QueryItemAvailableList().ToArray());
                        foreach (FS.HISFC.Models.Pharmacy.Item item in _al1)
                        {
                            bool isAdd = true;
                            foreach (string str in drugQualityStr)
                            {
                                if (item.Quality.ID == str)
                                {
                                    isAdd = false;
                                }
                            }
                            if (isAdd)
                            {
                                newDrugInfoList.Add(item);
                            }
                        }
                    }
                    else
                    {
                        _al1 = _items1.QueryItemAvailableList(patientDept);
                        foreach (FS.HISFC.Models.Pharmacy.Item item in _al1)
                        {
                            bool isAdd = true;
                            foreach (string str in drugQualityStr)
                            {
                                if (item.Quality.ID == str)
                                {
                                    isAdd = false;
                                }
                            }
                            if (isAdd)
                            {
                                newDrugInfoList.Add(item);
                            }
                        }
                    }
                    FS.HISFC.BizLogic.Fee.Item _items2 = new FS.HISFC.BizLogic.Fee.Item();
                    ArrayList _al2 = new ArrayList(_items2.QueryValidItemsList().ToArray());
                    _alItems = newDrugInfoList;
                    _alItems.AddRange(_al2);
                    //����
                    //List<FS.HISFC.Models.FeeStuff.MaterialItem> mateList = this.GetMateList((_items1.Operator as FS.HISFC.Models.Base.Employee).Dept.ID);
                    //_alItems.AddRange(mateList.ToArray());
                }
            }
            catch (System.Threading.ThreadAbortException ex) { }
            catch (NullReferenceException ex1) { }
            catch (Exception error)
            {
                MessageBox.Show("�����շ���Ŀ�б�ʱ����!" + error.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��_alItems����Ŀ��ӵ�_dsItems
        /// </summary>
        /// <returns></returns>
        private int AddItemsToDataSet()
        {
            _dsItems.Tables["items"].Clear();
            string  siflag = string.Empty;
            //try
            //{
                for (int i = 0; i < _alItems.Count; i++)
                {
                    if (_alItems[i] is FS.HISFC.Models.Pharmacy.Item)
                    {
                        FS.HISFC.Models.Pharmacy.Item obj;
                        obj = (FS.HISFC.Models.Pharmacy.Item)_alItems[i];

                        decimal showPrice = 0;

                        if (isUseRetailPrice2)
                        {
                            showPrice = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(obj.ID).RetailPrice2;
                        }
                        else
                        {
                            showPrice = obj.Price;
                        }
                        //siflag = this.getSIFlag(obj.ID, obj.Grade);

                        _dsItems.Tables["items"].Rows.Add(new Object[] {obj.UserCode,//0
                                                                        obj.SpellCode,//1
                                                                        obj.WBCode,//2
                                                                        obj.ID,//3
                                                                        obj.Name,//4
                                                                        obj.Specs,//5
                                                                        showPrice,//6
                                                                        obj.PackUnit,//7
                                                                        obj.Product.Name,//8
                                                                        obj.Memo,//9
                                                                        siflag,//10
                                                                        "1",//11
                                                                        string.IsNullOrEmpty(obj.GBCode)?obj.NameCollection.OtherSpell.SpellCode:obj.GBCode,//12
                                                                        obj.Frequency.Name,//13
                                                                        obj.Usage.Name,//14
                                                                        obj.SysClass.Name,//15
                                                                        obj.NameCollection.EnglishRegularName,//16
                                                                        string.Empty,//17
                                                                        obj.Price,//18
                                                                        obj.Price //19
                        });
                    }
                    else if (_alItems[i] is FS.HISFC.Models.Fee.Item.Undrug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug obj;
                        obj = (FS.HISFC.Models.Fee.Item.Undrug)_alItems[i];

                        // 2A5608D8-26AD-47d7-82CC-81375722FF72}
                        #region ���ݸ���Ŀ���������ҹ�����Ŀ

                        bool val = false;
                        string[] deptList = null;
                        if (!value ||
                            obj.DeptList == "" || obj.DeptList == "ALL" || obj.DeptList == null
                            )
                        {
                            val = true;
                        }
                        else
                        {
                            deptList = obj.DeptList.Split('|');
                            for (int j = 0; j < deptList.Length; j++)
                            {
                                if (deptList[j].ToString() == this.deptLogin.ID.ToString())
                                {
                                    val = true;
                                    break;
                                }
                            }
                        }

                        #endregion
                        //siflag = this.getSIFlag(obj.ID, obj.Grade);
                        string strSysClass = obj.SysClass.Name;
                        string strPriceUnit = obj.PriceUnit;
                        if (obj.UnitFlag == "1")
                        {
                            strPriceUnit = "[������]";
                            strSysClass = "[������]";
                        }
                        if (val)
                        {
                            _dsItems.Tables["items"].Rows.Add(new Object[] {obj.UserCode,//0
                                                                        obj.SpellCode,//1
                                                                        obj.WBCode,//2
                                                                        obj.ID,//3
                                                                        obj.Name,//4
                                                                        obj.Specs,//5
                                                                        //{EB55940A-F311-4ba3-8383-2CF15F06E2CE}
                                                                        //obj.Price*obj.FTRate.EMCRate,//6
                                                                        obj.Price,//6
                                                                        strPriceUnit,//7
                                                                        string.Empty,//8
                                                                        obj.Memo,//9
                                                                        siflag,//10
                                                                        "2",//11
                                                                        obj.GBCode,//12
                                                                        string.Empty,//13
                                                                        string.Empty,//14
                                                                        strSysClass,//15
                                                                        string.Empty,//16
                                                                        string.Empty,//17
                                                                        obj.ChildPrice*obj.FTRate.EMCRate,//18
                                                                        obj.SpecialPrice*obj.FTRate.EMCRate//19
                        });
                        }
                    }
                    else if (_alItems[i] is FS.HISFC.Models.FeeStuff.MaterialItem)
                    {
                        FS.HISFC.Models.FeeStuff.MaterialItem obj;
                        obj = _alItems[i] as FS.HISFC.Models.FeeStuff.MaterialItem;

                        _dsItems.Tables["items"].Rows.Add(new Object[]{obj.UserCode, //������
                                                                       obj.SpellCode,//ƴ����
                                                                       obj.WbCode,//�����
                                                                        obj.ID,//��Ŀ����
                                                                        obj.Name,//��Ŀ����
                                                                        obj.Specs,//���
                                                                        obj.Price,//�۸�
                                                                        obj.PriceUnit,//��λ
                                                                        string.Empty,//��������
                                                                        obj.Memo,//��ע
                                                                        string.Empty,//ҽ������
                                                                        "6",//����
                                                                        obj.GbCode,//������
                                                                        string.Empty,//Ƶ������
                                                                        string.Empty,//�÷�����
                                                                        obj.SysClass.Name,//ϵͳ���
                                                                        string.Empty,//Ӣ��
                                                                        string.Empty,//���ѱ�־
                                                                        obj.Price,//��ͯ�۸�
                                                                        obj.Price//����۸�
                                                                        
                        });
                    }

                }
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("������Ŀ�б�ʱ����!" + error.Message);
            //    return -1;
            //}


            //			_dsItems.CaseSensitive=false;
            //			_dvItems=new DataView(_dsItems.Tables["items"]);
            if (_dvItems.Count == 0)
                _CurrentRow = -1;
            else
                _CurrentRow = 0;

            return 1;
        }
       
        /// <summary>
        /// �õ�ҽ����־
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        protected virtual string getSIFlag(string itemCode, string grade)
        {
            //{112B7DB5-0462-4432-AD9D-17A7912FFDBE}   ��ȡҽ����Ŀ���
            if (this.IGetSiFlag != null)
            {
                grade = "0";

                this.IGetSiFlag.GetSiItemGrade(itemCode, ref grade);
            }

            return FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(grade);

        }
        /// <summary>
        /// ��_dsItems����Ŀ��ӵ�Sheet�У�ÿ�����10����¼
        /// </summary>
        /// <returns></returns>
        private int AddItemsToSheet(int BeginIndex)
        {
            fpSpread1_Sheet1.RowCount = _dvItems.Count;// {4D67D981-6763-4ced-814E-430B518304E2}
            for (int i = BeginIndex; i < _dvItems.Count; i++)
            {
                if (i > _dvItems.Count - 1 || _dvItems.Count == 0)
                {
                    for (int j = 0; j < fpSpread1_Sheet1.Columns.Count; j++)
                    {
                        fpSpread1_Sheet1.RowHeader.Rows[i - BeginIndex].Tag = string.Empty;
                        fpSpread1_Sheet1.SetValue(i - BeginIndex, j, string.Empty, false);
                    }
                }
                else
                {
                    DataRowView _row = _dvItems[i];
                    for (int j = 0; j < fpSpread1_Sheet1.Columns.Count; j++)
                    {
                        fpSpread1_Sheet1.RowHeader.Rows[i - BeginIndex].Tag = _row["isdrug"].ToString() + _row["item_code"].ToString();
                        if (fpSpread1_Sheet1.Columns[j].Tag == null)
                            fpSpread1_Sheet1.Columns[j].Tag = string.Empty;
                        fpSpread1_Sheet1.SetValue(i - BeginIndex, j, _row[fpSpread1_Sheet1.Columns[j].Tag.ToString()].ToString(), false);
                    }
                }
            }

            return 1;
        }
        string _Text = string.Empty;
        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public int Filter(string strText)
        {
            if (threadItem.ThreadState != System.Threading.ThreadState.Stopped)
            {
                return 1;
            }
            string _filter = strText.Trim();
            this._Text = _filter;
            if (_filter.Length > 0)
            {
                CurFilterStr = _filter;
                //"/"��ʾ���������Ƽ��������ܵ�ǰ�Ǻ����뷨
                if (_filter.Substring(0, 1) == "/")
                {
                    if (_filter.Length == 1) return 1;

                    _filter = "item_name LIKE '%" + _filter.Substring(1, _filter.Length - 1) + "%'";
                    try
                    {
                        _dvItems.RowFilter = _filter;
                    }
                    catch { }

                    if (_dvItems.Count == 0)
                        _CurrentRow = -1;
                    else
                        _CurrentRow = 0;

                    AddItemsToSheet(_CurrentRow);
                    fpSpread1_Sheet1.ActiveRowIndex = 0;
                    fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
                    DisplayCurrentRow(0);
                    return 1;
                }
            }

            //			switch(this._InputType)
            //			{
            //				case 0://ƴ����
            //					_filter="spell_code LIKE '%"+_filter+"%'";
            //					break;
            //				case 1://���
            //					_filter="wb_code LIKE '%"+_filter+"%'";
            //					break;
            //				case 2://�Զ���
            //					_filter="input_code LIKE '%"+_filter+"%'";
            //					break;
            //				case 3://����
            //					_filter="gb_code LIKE '%"+_filter+"%'";
            //					break;
            //				case 4://Ӣ��
            //					_filter="english_code LIKE '%"+_filter+"%'";
            //					break;	
            //				default:
            //					return 1;
            //			}
            string filter;
            if (this.checkBox1.Checked)
                filter = "(spell_code LIKE '%" + _filter + "%') OR" + "(input_code LIKE '%" + _filter + "%') OR" + "(english_code LIKE '%" + _filter + "%') OR" + "(wb_code LIKE '%" + _filter + "%') OR" + "(item_name LIKE '%" + _filter + "%')";
            else
                filter = "(spell_code LIKE '" + _filter + "%') OR" + "(input_code LIKE '" + _filter + "%') OR" + "(english_code LIKE '" + _filter + "%') OR" + "(wb_code LIKE '" + _filter + "%') OR" + "(item_name LIKE '" + _filter + "%') OR" + "(gb_code LIKE '" + _filter + "%')";
            //			filter = "spell_code LIKE '%"+_filter+"%'";
            try
            {
                _dvItems.RowFilter = filter;
            }
            catch { }
            if (_dvItems.Count == 0)
                _CurrentRow = -1;
            else
                _CurrentRow = 0;

            AddItemsToSheet(_CurrentRow);
            fpSpread1_Sheet1.ActiveRowIndex = 0;
            fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            DisplayCurrentRow(0);

            return 1;
        }
        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int Filter(string strText, string type)
        {
            if (threadItem.ThreadState != System.Threading.ThreadState.Stopped)
            {
                return 1;
            }
            string _filter = strText.Trim();
            this._Text = _filter;
            if (_filter.Length >= 0)
            {
                //				//"/"��ʾ���������Ƽ��������ܵ�ǰ�Ǻ����뷨
                //				if(_filter.Substring(0,1)=="/")
                //				{
                //					if(_filter.Length==1) return 1;

                _filter = "(( item_name LIKE '%" + _filter + "%' )" + " or ( spell_code LIKE '%" + _filter + "%' )" + " or ( input_code LIKE '%" + _filter + "%' )" + " or ( wb_code LIKE '%" + _filter + "%' ) OR" + "(gb_code LIKE '" + _filter + "%')" +") and  " + "unit LIKE '%" + type + "%'";
                try
                {
                    _dvItems.RowFilter = _filter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Filter" + ex.Message);
                }

                if (_dvItems.Count == 0)
                    _CurrentRow = -1;
                else
                    _CurrentRow = 0;

                AddItemsToSheet(_CurrentRow);
                fpSpread1_Sheet1.ActiveRowIndex = 0;
                fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
                DisplayCurrentRow(0);
                return 1;
                //				}
            }
            string filter;
            if (this.checkBox1.Checked)
                filter = "(spell_code LIKE '%" + _filter + "%') OR" + "(input_code LIKE '%" + _filter + "%') OR" + "(english_code LIKE '%" + _filter + "%') OR" + "(wb_code LIKE '%" + _filter + "%') OR" + "(item_name LIKE '%" + _filter + "%')";
            else
                filter = "(spell_code LIKE '" + _filter + "%') OR" + "(input_code LIKE '" + _filter + "%') OR" + "(english_code LIKE '" + _filter + "%') OR" + "(wb_code LIKE '%" + _filter + "%') OR" + "(item_name LIKE '" + _filter + "%')";
            //			filter = "spell_code LIKE '%"+_filter+"%'";
            try
            {
                _dvItems.RowFilter = filter;
            }
            catch { }
            if (_dvItems.Count == 0)
                _CurrentRow = -1;
            else
                _CurrentRow = 0;

            AddItemsToSheet(_CurrentRow);
            fpSpread1_Sheet1.ActiveRowIndex = 0;
            fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            DisplayCurrentRow(0);

            return 1;
        }
        /// <summary>
        /// ������һ��
        /// </summary>
        /// <returns></returns>
        public int NextRow()
        {
            int _Row = fpSpread1_Sheet1.ActiveRowIndex;
            if (_Row < 9)
            {
                _Row = _Row + 1;
                fpSpread1_Sheet1.ActiveRowIndex = _Row;
                fpSpread1_Sheet1.AddSelection(_Row, 0, 1, 1);
            }
            else
            {
                if (_CurrentRow >= _dvItems.Count - 10 || _dvItems.Count < 10) return 1;

                _CurrentRow++;
                AddItemsToSheet(_CurrentRow);
                fpSpread1_Sheet1.AddSelection(9, 0, 1, 1);
            }
            DisplayCurrentRow(fpSpread1_Sheet1.ActiveRowIndex);
            return 1;
        }
        /// <summary>
        /// ������һ��
        /// </summary>
        /// <returns></returns>
        public int PriorRow()
        {
            int _Row = fpSpread1_Sheet1.ActiveRowIndex;
            if (_Row > 0)
            {
                _Row = _Row - 1;
                fpSpread1_Sheet1.ActiveRowIndex = _Row;
                fpSpread1_Sheet1.AddSelection(_Row, 0, 1, 1);
            }
            else
            {
                if (_CurrentRow == 0) return 1;

                _CurrentRow--;
                AddItemsToSheet(_CurrentRow);
                fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            }
            DisplayCurrentRow(fpSpread1_Sheet1.ActiveRowIndex);
            return 1;
        }
        /// <summary>
        /// ������һҳ
        /// </summary>
        /// <returns></returns>
        public int NextPage()
        {
            if (_CurrentRow < _dvItems.Count - 10)
            {
                _CurrentRow = _CurrentRow + 10;

                AddItemsToSheet(_CurrentRow);
            }
            else if (_CurrentRow == _dvItems.Count - 10)
            {
                fpSpread1_Sheet1.ActiveRowIndex = 9;
                fpSpread1_Sheet1.AddSelection(9, 0, 1, 1);
            }
            DisplayCurrentRow(fpSpread1_Sheet1.ActiveRowIndex);
            return 1;
        }
        /// <summary>
        /// ������һҳ
        /// </summary>
        /// <returns></returns>
        public int PriorPage()
        {
            if (_CurrentRow >= 10)
            {
                _CurrentRow = _CurrentRow - 10;
                AddItemsToSheet(_CurrentRow);
            }
            else if (_CurrentRow > 0)
            {
                _CurrentRow = 0;
                AddItemsToSheet(_CurrentRow);
                fpSpread1_Sheet1.ActiveRowIndex = 0;
                fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            }
            else if (_CurrentRow == 0)
            {
                fpSpread1_Sheet1.ActiveRowIndex = 0;
                fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            }
            DisplayCurrentRow(fpSpread1_Sheet1.ActiveRowIndex);
            return 1;
        }
        /// <summary>
        /// �޸ļ�����ʽ
        /// </summary>
        /// <returns></returns>
        public int ChangeQueryType()
        {
            try
            {
                XmlDocument _doc = new XmlDocument();
                if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
                {
                    FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();
                }
                _doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode _node = _doc.SelectSingleNode("//���뷨");

                _InputType = int.Parse(_node.Attributes["currentmodel"].Value);
                _InputType++;
                if (_InputType > 4 || _InputType < 0) _InputType = 0;

                switch (_InputType)
                {
                    case 0://ƴ��
                        _dvItems.Sort = "spell_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊƴ����,F11�л����뷨";
                        break;
                    case 1://���
                        _dvItems.Sort = "wb_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊ�����,F11�л����뷨";
                        break;
                    case 2://�Զ���
                        _dvItems.Sort = "input_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊ�Զ�����,F11�л����뷨";
                        break;
                    case 3://����
                        _dvItems.Sort = "gb_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊ������,F11�л����뷨";
                        break;
                    case 4://Ӣ��
                        _dvItems.Sort = "english_code ASC";
                        //						lbInput.Text="��ǰ���뷨ΪӢ����,F11�л����뷨";
                        break;
                }
                lbInput.Text = "�Զ����롢ƴ���롢����롢Ӣ���롢��Ŀ����";

                _node.Attributes["currentmodel"].Value = _InputType.ToString();
                _doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");

                if (_dvItems.Count == 0)
                    _CurrentRow = -1;
                else
                    _CurrentRow = 0;

                AddItemsToSheet(_CurrentRow);
                fpSpread1_Sheet1.ActiveRowIndex = 0;
                fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
                DisplayCurrentRow(0);
            }
            catch (Exception error)
            {
                MessageBox.Show("�л����뷨ʱ����!" + error.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ��ȡ������ʽ
        /// </summary>
        /// <returns></returns>
        private int GetQueryType()
        {
            try
            {
                XmlDocument _doc = new XmlDocument();
                if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
                {
                    FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();
                }
                _doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode _node = _doc.SelectSingleNode("//���뷨"); 
                _InputType = int.Parse(_node.Attributes["currentmodel"].Value);
                if (_InputType > 4 || _InputType < 0) _InputType = 0;

                switch (_InputType)
                {
                    case 0://ƴ��
                        _dvItems.Sort = "spell_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊƴ����,F11�л����뷨";
                        break;
                    case 1://���
                        _dvItems.Sort = "wb_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊ�����,F11�л����뷨";
                        break;
                    case 2://�Զ���
                        _dvItems.Sort = "input_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊ�Զ�����,F11�л����뷨";
                        break;
                    case 3://����
                        _dvItems.Sort = "gb_code ASC";
                        //						lbInput.Text="��ǰ���뷨Ϊ������,F11�л����뷨";
                        break;
                    case 4://Ӣ��
                        _dvItems.Sort = "english_code ASC";
                        //						lbInput.Text="��ǰ���뷨ΪӢ����,F11�л����뷨";
                        break;
                }
                lbInput.Text = "�Զ����롢ƴ���롢����롢Ӣ���롢��Ŀ����";
            }
            catch (Exception error)
            {
                MessageBox.Show("��ȡ���뷨ʱ����!" + error.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��õ�ǰѡ�е�item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>-1:ʧ�� 0:û�� 1:�ɹ�</returns>
        public int GetSelectItem(out FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.Models.Base.Item _item = null;
            _item = new FS.HISFC.Models.Base.Item();
            if (_CurrentRow == -1)
            {
                item = _item;
                return 0;
            }
            int _Index = fpSpread1_Sheet1.ActiveRowIndex;
            string _ItemCode = fpSpread1_Sheet1.RowHeader.Rows[_Index].Tag.ToString();
            if (_ItemCode == string.Empty || _ItemCode == null)
            {
                item = _item;
                return 0;
            }
            //Isdrug: 1ҩƷ��2��ҩƷ��3���ס�4������Ŀ��6����
            string Isdrug = _ItemCode.Substring(0, 1);
            _ItemCode = _ItemCode.Substring(1);

            return GetSelectItem(_ItemCode,Isdrug,out item);
        }

        //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
        /// <summary>
        /// ��õ�ǰѡ�е�item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="_ItemCode">��Ŀ���</param>
        /// <param name="Isdrug">��Ŀ���</param>
        /// <returns>-1:ʧ�� 0:û�� 1:�ɹ�</returns>
        public int GetSelectItem(string _ItemCode,string Isdrug, out FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.Models.Base.Item _item = null;
            try
            {
                _item = new FS.HISFC.Models.Base.Item();
                //Isdrug: 1ҩƷ��2��ҩƷ��3���ס�4������Ŀ��6����

                for (int i = 0; i < _alItems.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject obj = _alItems[i] as FS.FrameWork.Models.NeuObject;
                    if (Isdrug == "1")//ҩƷ
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item) &&
                            obj.ID == _ItemCode)
                        {
                            _item = (_alItems[i] as FS.HISFC.Models.Base.Item).Clone();
                            //_item.IsPharmacy = true;
                            _item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                            item = _item;
                            return 1;
                        }
                    }
                    else if (Isdrug == "2")//��ҩƷ
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug) &&
                            obj.ID == _ItemCode)
                        {
                            FS.HISFC.Models.Fee.Item.Undrug undrugObj = (FS.HISFC.Models.Fee.Item.Undrug)_alItems[i];

                            _item = (_alItems[i] as FS.HISFC.Models.Base.Item).Clone();
                            //_item.IsPharmacy = false;
                            _item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                            item = _item;
                            if (undrugObj.UnitFlag == "1")
                            {
                                item.User01 = "[������]"; ;
                            }
                            return 1;
                        }
                    }
                    else if (Isdrug == "3")//�շ�������Ŀ
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.Fee.ComGroup) &&
                            obj.ID == _ItemCode)
                        {
                            _item.ID = obj.ID;
                            _item.Name = obj.Name;
                            _item.User01 = "[����]";
                            item = _item;
                            return 1;
                        }
                    }
                    //������Ŀ�Ѿ��鲢����ҩƷ�� ���Դ˴�ȥ��
                    //else if (Isdrug == "4")//������Ŀ
                    //{
                    //    if (obj.GetType() == typeof(FS.HISFC.Models.Fee.Item.UndrugComb) &&
                    //        obj.ID == _ItemCode)
                    //    {
                    //        _item.ID = obj.ID;
                    //        _item.Name = obj.Name;
                    //        _item.User01 = "[������]";
                    //        //_item.User02 = (obj as FS.HISFC.Models.Fee.Undrugztinfo).;
                    //        item = _item;
                    //        return 1;
                    //    }
                    //}
                    else if (Isdrug == "5")//�������
                    {
                        //???????
                        if (obj.GetType() == typeof(FS.HISFC.Models.PhysicalExamination.Group) &&
                            obj.ID == _ItemCode)
                        {
                            _item.ID = obj.ID;
                            _item.Name = obj.Name;
                            _item.User01 = "[����]";
                            // _item.User02 = (obj as FS.HISFC.Models.Fee.Item.Undrug).deptCode;
                            item = _item;
                            return 1;
                        }
                    }
                    else if (Isdrug == "6") //����
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.FeeStuff.MaterialItem))
                        {
                            if (obj.ID == _ItemCode)
                            {
                                _item = (_alItems[i] as FS.HISFC.Models.Base.Item).Clone();
                                _item.ItemType = FS.HISFC.Models.Base.EnumItemType.MatItem;
                                item = _item;
                                item.Price = (obj as FS.HISFC.Models.FeeStuff.MaterialItem).Price;
                                return 1;
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("��ȡ��Ŀ��Ϣʱ����!" + error.Message);
                item = new FS.HISFC.Models.Base.Item();
                return -1;
            }

            item = new FS.HISFC.Models.Base.Item();
            return -1;
        }


        /// <summary>
        /// ������Ŀ��ʾ���
        /// </summary>
        /// <returns></returns>
        private int SetStyle(string strFileName, string strElement, FarPoint.Win.Spread.SheetView svControl)
        {
            //XmlDocument _xmlDoc = new XmlDocument();
            //int _intCount = 0;
            //try
            //{
            //    _xmlDoc.Load(strFileName);

            //    XmlNodeList _xmlNodes = _xmlDoc.SelectNodes(strElement);
            //    foreach (XmlNode node in _xmlNodes)
            //    {
            //        svControl.AddColumns(_intCount, 1);
            //        FarPoint.Win.Spread.Column _column = svControl.Columns[_intCount];
            //        _column.Width = int.Parse(node.Attributes["width"].Value);
            //        _column.Visible = bool.Parse(node.Attributes["visible"].Value);
            //        _column.Locked = true;//bool.Parse(node.Attributes["locked"].Value);
            //        _column.Tag = node.Attributes["tag"].Value;
            //        string _displayName = node.Attributes["displayname"].Value;
            //        if (_displayName != string.Empty)
            //        {
            //            _column.Label = _displayName;
            //        }
            //        else { _column.Label = "δ��������"; }
            //        _intCount++;
            //    }
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("������Ŀ�б�������ļ�ʱ����!" + error.Message);
            //    return -1;
            //}
            //if (_intCount == 0)
            //{
            //    MessageBox.Show("������Ŀ�б�������ļ�ʱ����,�ļ�δ������!");
            //    return -1;
            //}
            //svControl.ColumnCount = _intCount;

            svControl.ColumnCount = 20;
            svControl.Columns[(int)Cols.input_code].Visible = true; //������0
            svControl.Columns[(int)Cols.spell_code].Visible = false;//ƴ����1
            svControl.Columns[(int)Cols.wb_code].Visible = false;//�����2
            svControl.Columns[(int)Cols.item_code].Visible = false;//��Ŀ����3
            svControl.Columns[(int)Cols.item_name].Visible = true;//��Ŀ����4
            svControl.Columns[(int)Cols.specs].Visible = true;//���5
            svControl.Columns[(int)Cols.price].Visible = true;//�۸�6
            svControl.Columns[(int)Cols.unit].Visible = true;//��λ7
            svControl.Columns[(int)Cols.producer].Visible = false;//��������8
            svControl.Columns[(int)Cols.memo].Visible = false;//��ע9
            svControl.Columns[(int)Cols.grade].Visible = true;//ҽ������10
            svControl.Columns[(int)Cols.isdrug].Visible = false;//1ҩ��2��ҩ11
            svControl.Columns[(int)Cols.gb_code].Visible = false;//������12
            svControl.Columns[(int)Cols.freq_name].Visible = false;//Ƶ������13
            svControl.Columns[(int)Cols.usage_name].Visible = false;//�÷�����14
            svControl.Columns[(int)Cols.class_code].Visible = false;//ϵͳ���15
            svControl.Columns[(int)Cols.english_code].Visible = false;//Ӣ��16
            svControl.Columns[(int)Cols.pub_grade].Visible = true;//���ѱ�־17
            svControl.Columns[(int)Cols.child_price].Visible = true;//��ͯ�۸�18
            svControl.Columns[(int)Cols.special_price].Visible = true;//����۸�19

            svControl.Columns[(int)Cols.input_code].Label = "������";
            svControl.Columns[(int)Cols.spell_code].Label ="ƴ����";
            svControl.Columns[(int)Cols.wb_code].Label ="�����";
            svControl.Columns[(int)Cols.item_code].Label ="��Ŀ����";
            svControl.Columns[(int)Cols.item_name].Label ="��Ŀ����";
            svControl.Columns[(int)Cols.specs].Label =" ���";
            svControl.Columns[(int)Cols.price].Label ="�۸�";
            svControl.Columns[(int)Cols.unit].Label ="��λ";
            svControl.Columns[(int)Cols.producer].Label ="����";
            svControl.Columns[(int)Cols.memo].Label ="��ע";
            svControl.Columns[(int)Cols.grade].Label ="ҽ������";
            svControl.Columns[(int)Cols.isdrug].Label ="��Ŀ";
            svControl.Columns[(int)Cols.gb_code].Label ="������";
            svControl.Columns[(int)Cols.freq_name].Label ="Ƶ��";
            svControl.Columns[(int)Cols.usage_name].Label ="�÷�";
            svControl.Columns[(int)Cols.class_code].Label ="ϵͳ���";
            svControl.Columns[(int)Cols.english_code].Label ="Ӣ��";
            svControl.Columns[(int)Cols.pub_grade].Label ="���ѱ�־";
            svControl.Columns[(int)Cols.child_price].Label ="��ͯ��";
            svControl.Columns[(int)Cols.special_price].Label ="�����";

            svControl.Columns[(int)Cols.input_code].Tag = "input_code";
            svControl.Columns[(int)Cols.spell_code].Tag = "spell_code";
            svControl.Columns[(int)Cols.wb_code].Tag = "wb_code";
            svControl.Columns[(int)Cols.item_code].Tag = "item_code";
            svControl.Columns[(int)Cols.item_name].Tag = "item_name";
            svControl.Columns[(int)Cols.specs].Tag = "specs";
            svControl.Columns[(int)Cols.price].Tag = "price";
            svControl.Columns[(int)Cols.unit].Tag = "unit";
            svControl.Columns[(int)Cols.producer].Tag = "producer";
            svControl.Columns[(int)Cols.memo].Tag = "memo";
            svControl.Columns[(int)Cols.grade].Tag = "grade";
            svControl.Columns[(int)Cols.isdrug].Tag = "isdrug";
            svControl.Columns[(int)Cols.gb_code].Tag = "gb_code";
            svControl.Columns[(int)Cols.freq_name].Tag = "freq_name";
            svControl.Columns[(int)Cols.usage_name].Tag = "usage_name";
            svControl.Columns[(int)Cols.class_code].Tag = "class_code";
            svControl.Columns[(int)Cols.english_code].Tag = "english_code";
            svControl.Columns[(int)Cols.pub_grade].Tag = "pub_grade";
            svControl.Columns[(int)Cols.child_price].Tag = "child_price";
            svControl.Columns[(int)Cols.special_price].Tag = "special_price";

            svControl.Columns[(int)Cols.input_code].Width = 60; 
            svControl.Columns[(int)Cols.item_name].Width = 200;
            svControl.Columns[(int)Cols.price].Width = 60;
            svControl.Columns[(int)Cols.unit].Width = 40;
            svControl.Columns[(int)Cols.grade].Width = 40;
            svControl.Columns[(int)Cols.pub_grade].Width = 30;
            svControl.Columns[(int)Cols.child_price].Width = 60;
            svControl.Columns[(int)Cols.special_price].Width = 60;
            svControl.Columns[(int)Cols.specs].Width = 100;
            InputMap im;

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.PageUp, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.PageDown, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            return 1;
        }

        /// <summary>
        /// ��ʾ��ǰ�к�������
        /// </summary>
        /// <returns></returns>
        private int DisplayCurrentRow(int SheetRow)
        {
            int row = _CurrentRow + SheetRow + 1;
            if (_dvItems.Count < 10)
                lbCount.Text = "��ǰ��:" + row.ToString() + "/��:" + fpSpread1_Sheet1.RowCount.ToString();
            else
                lbCount.Text = "��ǰ��:" + row.ToString() + "/��:" + _dvItems.Count.ToString();

            return 1;
        }
        /// <summary>
        /// ����Sheet��ǰ��
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public int SetCurrentRow(int Row)
        {
            if (Row < 0 || Row > 9) return -1;
            fpSpread1_Sheet1.ActiveRowIndex = Row;
            fpSpread1_Sheet1.AddSelection(Row, 0, 1, 1);
            return 1;
        }

        #endregion

        #region �¼�
        //����selectitem�¼�
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (SelectItem != null)
            {
                this.SelectItem(Keys.Enter);
            }
        }

        //��������fpspread1_Sheet1�ϣ�����enter�¼�
        private void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2
                || e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4 || e.KeyCode == Keys.F5
                || e.KeyCode == Keys.F6 || e.KeyCode == Keys.F7 || e.KeyCode == Keys.F8
                || e.KeyCode == Keys.F9 || e.KeyCode == Keys.F10)
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        fpSpread1_Sheet1.ActiveRowIndex = 0;
                        break;
                    case Keys.F2:
                        fpSpread1_Sheet1.ActiveRowIndex = 1;
                        break;
                    case Keys.F3:
                        fpSpread1_Sheet1.ActiveRowIndex = 2;
                        break;
                    case Keys.F4:
                        fpSpread1_Sheet1.ActiveRowIndex = 3;
                        break;
                    case Keys.F5:
                        fpSpread1_Sheet1.ActiveRowIndex = 4;
                        break;
                    case Keys.F6:
                        fpSpread1_Sheet1.ActiveRowIndex = 5;
                        break;
                    case Keys.F7:
                        fpSpread1_Sheet1.ActiveRowIndex = 6;
                        break;
                    case Keys.F8:
                        fpSpread1_Sheet1.ActiveRowIndex = 7;
                        break;
                    case Keys.F9:
                        fpSpread1_Sheet1.ActiveRowIndex = 8;
                        break;
                    case Keys.F10:
                        fpSpread1_Sheet1.ActiveRowIndex = 9;
                        break;
                }

                fpSpread1_Sheet1.AddSelection(fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);

                if (SelectItem != null)
                {
                    this.SelectItem(e.KeyCode);
                }
            }
        }
        //����up,down,pageup,pagedown�¼�
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (fpSpread1.ContainsFocus)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        this.PriorRow();
                        break;
                    case Keys.Down:
                        this.NextRow();
                        break;
                    case Keys.PageUp:
                        this.PriorPage();
                        break;
                    case Keys.PageDown:
                        this.NextPage();
                        break;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ������ʱ���仯��ǰ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row > 0)
                DisplayCurrentRow(e.Row);
            else if (e.Row == 0)
            {
                fpSpread1_Sheet1.ActiveRowIndex = 0;
                fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
                DisplayCurrentRow(0);
            }
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            this.Filter(this._Text);
        }
        #endregion

        #region IInterfaceContainer ��Ա
        //{112B7DB5-0462-4432-AD9D-17A7912FFDBE} ���ӽӿ�����
        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade) };
            }
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


    }
}
