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
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// {C35FECE5-305E-452c-B22D-65BDEA3624AD}
    /// [功能描述: 草药项目列表]<br></br>
    /// [创 建 者: sunm]<br></br>
    /// [创建时间: 2010-09-18]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucPCCItemList : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
       
        public ucPCCItemList()
        {
            InitializeComponent();
            InitDataSet();
        }

        
        #region 变量
        //项目数组
        private ArrayList _alItems = new ArrayList();
        //项目dataset
        private DataSet _dsItems;
        private DataView _dvItems;
        
        //查询方式0:拼音码 1:五笔码 2:自定义码 3:国标码 4:英文
        private int _InputType = 0;
        /// <summary>
        /// 患者所在科室
        /// </summary>
        private string patientDept = string.Empty;
        //dataview中的当前行
        private int _CurrentRow = -1;
        public delegate int MyDelegate(Keys key);
        /// <summary>
        /// 双击、回车项目列表时执行的事件
        /// </summary>
        public event MyDelegate SelectItem;

        private FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo iItemCompareInfo = null;
        /// <summary>
        /// 项目扩展信息接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo IItemCompareInfo
        {
            get
            {
                if (this.iItemCompareInfo == null)
                {
                    this.iItemCompareInfo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo)) as FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo;
                }

                return this.iItemCompareInfo;
            }
        }

        /// <summary>
        /// 扩展信息文本
        /// </summary>
        public System.Windows.Forms.TextBox txt = new TextBox();


        FS.HISFC.BizLogic.Manager.UserDefaultSetting settingManager = new FS.HISFC.BizLogic.Manager.UserDefaultSetting();

        FS.HISFC.Models.Base.UserDefaultSetting settingObj = null;

        #endregion

        #region 属性
        
        /// <summary>
        /// 获取查询方式0:拼音码 1:五笔码 2:自定义码 3:国标码 4:英文
        /// </summary>
        public int InputType
        {
            get
            {
                return _InputType;
            }
        }
      
        /// <summary>
        /// 患者科室编码
        /// </summary>
        public string PatientDept
        {
            get
            {
                return patientDept;
            }
            set
            {
                patientDept = value;
                this.Init(value);
            }
        }

        private FS.HISFC.Models.Base.PactInfo pactInfo = new FS.HISFC.Models.Base.PactInfo();
        public FS.HISFC.Models.Base.PactInfo PactInfo
        {
            get
            {
                return this.pactInfo;
            }
            set
            {
                this.pactInfo = value;
            }
        }
        #endregion

        #region 函数

        /// <summary>
        ///初始设置调用
        /// </summary>
        /// <returns></returns>
        protected virtual  int Init()
        {
            InitItem();

            this.SetUserDefaultSetting();

            return 1;
        }

        /// <summary>
        /// 设置当前用户默认配置
        /// </summary>
        public void SetUserDefaultSetting()
        {
            settingObj = settingManager.Query(settingManager.Operator.ID);

            if (settingObj != null)
            {
                this.cbxIsReal.Checked = FS.FrameWork.Function.NConvert.ToBoolean(string.IsNullOrEmpty(settingObj.Setting3) ? "0" : settingObj.Setting3);
            }
        }

        /// <summary>
        /// 加载项目信息
        /// </summary>
        protected void InitItem()
        {
            int _intRtn;

            _intRtn = AddItemsToArrayList(patientDept);
            if (_intRtn == -1) return ;

            _intRtn = AddItemsToDataSet();
            if (_intRtn == -1) return ;

            InitFpState();
        }

        
        /// <summary>
        /// 初始化Fp状态
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
            
            this.patientDept = patientDept;
            if (Init() == -1) return -1;
            return 1;
        }

        
        /// <summary>
        /// 刷新DataSet
        /// </summary>
        public int RefreshDataSet(string deptId)
        {
            //查找物资项目
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
            
            return 1;
        }

        private void InitDataSet()
        {
            //初始化
            
            SetStyle("", "", fpSpread1_Sheet1);
            SetAutoSize();//此方法导致没有显示滚动条
            _dsItems = new DataSet();
            _dsItems.Tables.Add("items");
            _dsItems.Tables["items"].Columns.AddRange(new DataColumn[]
				{
					new DataColumn("input_code",Type.GetType("System.String")),//输入码
					new DataColumn("spell_code",Type.GetType("System.String")),//拼音码
					new DataColumn("wb_code",Type.GetType("System.String")),//五笔码
					new DataColumn("item_code",Type.GetType("System.String")),//项目代码
					new DataColumn("item_name",Type.GetType("System.String")),//项目名称
					new DataColumn("specs",Type.GetType("System.String")),//规格
					new DataColumn("price",Type.GetType("System.String")),//价格
					new DataColumn("unit",Type.GetType("System.String")),//单位
					new DataColumn("producer",Type.GetType("System.String")),//生产厂家
					new DataColumn("memo",Type.GetType("System.String")),//备注
					new DataColumn("grade",Type.GetType("System.String")),//医保级别
					new DataColumn("isdrug",Type.GetType("System.String")),//1药，2非药
					new DataColumn("gb_code",Type.GetType("System.String")),//国标码
					new DataColumn("freq_name",Type.GetType("System.String")),//频次名称
					new DataColumn("usage_name",Type.GetType("System.String")),//用法名称
					new DataColumn("class_code",Type.GetType("System.String")),//系统类别
					new DataColumn("english_code",Type.GetType("System.String")),//英文
					new DataColumn("pub_grade",Type.GetType("System.String")),//公费标志
					new DataColumn("child_price",Type.GetType("System.String")),//儿童价格
					new DataColumn("special_price",Type.GetType("System.String"))//特诊价格
				});
            _dsItems.CaseSensitive = false;
            _dvItems = new DataView(_dsItems.Tables["items"]);
            GetQueryType();
        }

        enum Cols
        {
            input_code ,//输入码0
			spell_code ,//拼音码1
			wb_code ,//五笔码2
			item_code ,//项目代码3
			item_name ,//项目名称4
			specs ,//规格5
			price ,//价格6
			unit ,//单位7
			producer ,//生产厂家8
			memo ,//备注9
			grade ,//医保级别10
			isdrug ,//1药，2非药11
			gb_code ,//国标码12
			freq_name ,//频次名称13
			usage_name ,//用法名称14
			class_code ,//系统类别15
			english_code ,//英文16
			pub_grade ,//公费标志17
			child_price ,//儿童价格18
			special_price ,//特诊价格19
        }
        
        #endregion

        #region 私有函数
        
        /// <summary>
        /// 自动调整项目列表大小
        /// </summary>
        /// <returns></returns>
        private int SetAutoSize()
        {
            //设置宽度
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

            //设置高度
            int _intHeight = SystemInformation.Border3DSize.Height * 2;
            _intHeight += (int)fpSpread1_Sheet1.ColumnHeader.Rows[0].Height;

            for (i = 0; i < fpSpread1_Sheet1.RowCount; i++)
                _intHeight += (int)fpSpread1_Sheet1.Rows[i].Height;

            this.Height = _intHeight + 35+this.pnlBottom.Height;

            return 1;
        }
        /// <summary>
        /// 添加项目列表到_alItems
        /// </summary>
        /// <returns></returns>
        private int AddItemsToArrayList(string patientDept)
        {
            try
            {
                //TODO: 加载草药列表
                if (patientDept == string.Empty)
                {
                    _alItems = new ArrayList(CacheManager.PhaIntegrate.QueryItemAvailableList((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID, "PCC").ToArray());
                    if (_alItems == null || _alItems.Count == 0)
                    {
                        _alItems = new ArrayList(CacheManager.PhaIntegrate.QueryItemAvailableList((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID, "C").ToArray());
                    }
                }
                else
                {
                    _alItems = CacheManager.PhaIntegrate.QueryItemAvailableList(patientDept, "PCC");
                    if (_alItems == null || _alItems.Count == 0)
                    {
                        _alItems = CacheManager.PhaIntegrate.QueryItemAvailableList(patientDept, "C");
                    }
                }
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                MessageBox.Show("加载收费项目列表时出错!" + ex.Message);
                return -1;
            }
            catch (NullReferenceException ex1)
            {
                MessageBox.Show("加载收费项目列表时出错!" + ex1.Message);
                return -1;
            }
            catch (Exception error)
            {
                MessageBox.Show("加载收费项目列表时出错!" + error.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 将_alItems的项目添加到_dsItems
        /// </summary>
        /// <returns></returns>
        private int AddItemsToDataSet()
        {
            _dsItems.Tables["items"].Clear();
            string siflag = string.Empty;

            for (int i = 0; i < _alItems.Count; i++)
            {
                if (_alItems[i] is FS.HISFC.Models.Pharmacy.Item)
                {
                    FS.HISFC.Models.Pharmacy.Item obj;
                    obj = (FS.HISFC.Models.Pharmacy.Item)_alItems[i];

                    _dsItems.Tables["items"].Rows.Add(new Object[] {obj.UserCode,//0
                                                                        obj.SpellCode,//1
                                                                        obj.WBCode,//2
                                                                        obj.ID,//3
                                                                        obj.Name,//4
                                                                        obj.Specs,//5
                                                                        obj.Price,//6
                                                                        obj.PackUnit,//7
                                                                        obj.Product.Name,//8
                                                                        obj.Memo,//9
                                                                        siflag,//10
                                                                        "1",//11
                                                                        obj.GBCode,//12
                                                                        obj.Frequency.Name,//13
                                                                        obj.Usage.Name,//14
                                                                        obj.SysClass.Name,//15
                                                                        obj.NameCollection.EnglishRegularName,//16
                                                                        string.Empty,//17
                                                                        obj.Price,//18
                                                                        obj.Price //19
                        });
                }

            }


            //_dsItems.CaseSensitive = false;
            //_dvItems = new DataView(_dsItems.Tables["items"]);
            if (_dvItems.Count == 0)
                _CurrentRow = -1;
            else
                _CurrentRow = 0;

            return 1;
        }
       
        
        /// <summary>
        /// 将_dsItems的项目添加到Sheet中，每次添加6条记录
        /// </summary>
        /// <returns></returns>
        private int AddItemsToSheet(int BeginIndex)
        {
            for (int i = BeginIndex; i < BeginIndex + 10; i++)
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
        /// 过滤项目
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public int Filter(string strText)
        {
            string _filter = strText.Trim();
            this._Text = _filter;
            if (_filter.Length > 0)
            {
                //"/"表示按中文名称检索，不管当前是何输入法
                //if (_filter.Substring(0, 1) == "/")
                //{
                //    if (_filter.Length == 1)
                //        return 1;

                //    _filter = "item_name LIKE '%" + _filter.Substring(1, _filter.Length - 1) + "%'";
                //    try
                //    {
                //        _dvItems.RowFilter = _filter;
                //    }
                //    catch { }

                //    if (_dvItems.Count == 0)
                //        _CurrentRow = -1;
                //    else
                //        _CurrentRow = 0;

                //    AddItemsToSheet(_CurrentRow);
                //    fpSpread1_Sheet1.ActiveRowIndex = 0;
                //    fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
                //    DisplayCurrentRow(0);
                //    return 1;
                //}
            }

            string strKeLiAndYinPian = "All";

            if (_filter.StartsWith("K"))
            {
                strKeLiAndYinPian = "颗粒";
                _filter = _filter.Substring(1);
            }
            else if (_filter.StartsWith("Y"))
            {
                strKeLiAndYinPian = "饮片";
                _filter = _filter.Substring(1);
            }
            
            string filter;
            if (this.cbxIsReal.Checked)
            {
                filter = "((spell_code LIKE '%" + _filter + "%') OR" + "(input_code LIKE '%" + _filter + "%') OR" + "(english_code LIKE '%" + _filter + "%') OR" + "(wb_code LIKE '%" + _filter + "%') OR" + "(item_name LIKE '%" + _filter + "%'))";
            }
            else
            {
                filter = "((spell_code LIKE '" + _filter + "%') OR" + "(input_code LIKE '" + _filter + "%') OR" + "(english_code LIKE '" + _filter + "%') OR" + "(wb_code LIKE '" + _filter + "%') OR" + "(item_name LIKE '" + _filter + "%'))";
            }

            //增加类别过滤
            if (strKeLiAndYinPian == "颗粒")
            {
                filter += " AND (item_name LIKE '%颗粒%')";
            }
            else if (strKeLiAndYinPian == "饮片")
            {
                filter += " AND (item_name not LIKE '%颗粒%')";
            }
            
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
        /// 过滤项目
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int Filter(string strText, string type)
        {
            string _filter = strText.Trim();
            this._Text = _filter;
            if (_filter.Length >= 0)
            {
                //				//"/"表示按中文名称检索，不管当前是何输入法
                //				if(_filter.Substring(0,1)=="/")
                //				{
                //					if(_filter.Length==1) return 1;

                _filter = "(( item_name LIKE '%" + _filter + "%' )" + " or ( spell_code LIKE '%" + _filter + "%' )" + " or ( input_code LIKE '%" + _filter + "%' )" + " or ( wb_code LIKE '%" + _filter + "%' )" + ") and  " + "unit LIKE '%" + type + "%'";
                try
                {
                    _dvItems.RowFilter = _filter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
            if (this.cbxIsReal.Checked)
                filter = "(spell_code LIKE '%" + _filter + "%') OR" + "(input_code LIKE '%" + _filter + "%') OR" + "(english_code LIKE '%" + _filter + "%') OR" + "(wb_code LIKE '%" + _filter + "%') OR" + "(item_name LIKE '%" + _filter + "%')";
            else
                filter = "(spell_code LIKE '" + _filter + "%') OR" + "(input_code LIKE '" + _filter + "%') OR" + "(english_code LIKE '" + _filter + "%') OR" + "(wb_code LIKE '" + _filter + "%') OR" + "(item_name LIKE '" + _filter + "%')";

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
        /// 跳到下一行
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
        /// 跳到上一行
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
        /// 跳到下一页
        /// </summary>
        /// <returns></returns>
        public int NextPage()
        {
            if (_CurrentRow < _dvItems.Count - 6)
            {
                _CurrentRow = _CurrentRow + 6;

                AddItemsToSheet(_CurrentRow);
            }
            else if (_CurrentRow == _dvItems.Count - 6)
            {
                fpSpread1_Sheet1.ActiveRowIndex = 5;
                fpSpread1_Sheet1.AddSelection(5, 0, 1, 1);
            }
            DisplayCurrentRow(fpSpread1_Sheet1.ActiveRowIndex);
            return 1;
        }
        /// <summary>
        /// 跳到上一页
        /// </summary>
        /// <returns></returns>
        public int PriorPage()
        {
            if (_CurrentRow >= 6)
            {
                _CurrentRow = _CurrentRow - 6;
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
        /// 修改检索方式
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
                XmlNode _node = _doc.SelectSingleNode("//输入法");

                _InputType = int.Parse(_node.Attributes["currentmodel"].Value);
                _InputType++;
                if (_InputType > 4 || _InputType < 0) _InputType = 0;

                switch (_InputType)
                {
                    case 0://拼音
                        _dvItems.Sort = "spell_code ASC";
                        //						lbInput.Text="当前输入法为拼音码,F11切换输入法";
                        break;
                    case 1://五笔
                        _dvItems.Sort = "wb_code ASC";
                        //						lbInput.Text="当前输入法为五笔码,F11切换输入法";
                        break;
                    case 2://自定义
                        _dvItems.Sort = "input_code ASC";
                        //						lbInput.Text="当前输入法为自定义码,F11切换输入法";
                        break;
                    case 3://国标
                        _dvItems.Sort = "gb_code ASC";
                        //						lbInput.Text="当前输入法为国标码,F11切换输入法";
                        break;
                    case 4://英文
                        _dvItems.Sort = "english_code ASC";
                        //						lbInput.Text="当前输入法为英文码,F11切换输入法";
                        break;
                }
                lbInput.Text = "自定义码、拼音码、五笔码、英文码、项目名称";

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
                MessageBox.Show("切换输入法时出错!" + error.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取检索方式
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
                XmlNode _node = _doc.SelectSingleNode("//输入法"); 
                _InputType = int.Parse(_node.Attributes["currentmodel"].Value);
                if (_InputType > 4 || _InputType < 0) _InputType = 0;

                switch (_InputType)
                {
                    case 0://拼音
                        _dvItems.Sort = "spell_code ASC";
                        //						lbInput.Text="当前输入法为拼音码,F11切换输入法";
                        break;
                    case 1://五笔
                        _dvItems.Sort = "wb_code ASC";
                        //						lbInput.Text="当前输入法为五笔码,F11切换输入法";
                        break;
                    case 2://自定义
                        _dvItems.Sort = "input_code ASC";
                        //						lbInput.Text="当前输入法为自定义码,F11切换输入法";
                        break;
                    case 3://国标
                        _dvItems.Sort = "gb_code ASC";
                        //						lbInput.Text="当前输入法为国标码,F11切换输入法";
                        break;
                    case 4://英文
                        _dvItems.Sort = "english_code ASC";
                        //						lbInput.Text="当前输入法为英文码,F11切换输入法";
                        break;
                }
                lbInput.Text = "自定义码、拼音码、五笔码、英文码、项目名称";
            }
            catch (Exception error)
            {
                MessageBox.Show("获取输入法时出错!" + error.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获得当前选中的item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>-1:失败 0:没有 1:成功</returns>
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
            //Isdrug: 1药品、2非药品、3组套、4复合项目、6物资
            string Isdrug = _ItemCode.Substring(0, 1);
            _ItemCode = _ItemCode.Substring(1);

            return GetSelectItem(_ItemCode,Isdrug,out item);
        }

        
        /// <summary>
        /// 获得当前选中的item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="_ItemCode">项目编号</param>
        /// <param name="Isdrug">项目类别</param>
        /// <returns>-1:失败 0:没有 1:成功</returns>
        public int GetSelectItem(string _ItemCode,string Isdrug, out FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.Models.Base.Item _item = null;
            try
            {
                _item = new FS.HISFC.Models.Base.Item();
                //Isdrug: 1药品、2非药品、3组套、4复合项目、6物资

                for (int i = 0; i < _alItems.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject obj = _alItems[i] as FS.FrameWork.Models.NeuObject;
                    if (Isdrug == "1")//药品
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
                    
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("获取项目信息时出错!" + error.Message);
                item = new FS.HISFC.Models.Base.Item();
                return -1;
            }

            item = new FS.HISFC.Models.Base.Item();
            return -1;
        }


        /// <summary>
        /// 设置项目显示风格
        /// </summary>
        /// <returns></returns>
        private int SetStyle(string strFileName, string strElement, FarPoint.Win.Spread.SheetView svControl)
        {
            
            svControl.ColumnCount = 20;
            svControl.Columns[(int)Cols.input_code].Visible = true; //输入码0
            svControl.Columns[(int)Cols.spell_code].Visible = false;//拼音码1
            svControl.Columns[(int)Cols.wb_code].Visible = false;//五笔码2
            svControl.Columns[(int)Cols.item_code].Visible = false;//项目代码3
            svControl.Columns[(int)Cols.item_name].Visible = true;//项目名称4
            svControl.Columns[(int)Cols.specs].Visible = true;//规格5
            svControl.Columns[(int)Cols.price].Visible = true;//价格6
            svControl.Columns[(int)Cols.unit].Visible = true;//单位7
            svControl.Columns[(int)Cols.producer].Visible = false;//生产厂家8
            svControl.Columns[(int)Cols.memo].Visible = false;//备注9
            svControl.Columns[(int)Cols.grade].Visible = false;//医保级别10
            svControl.Columns[(int)Cols.isdrug].Visible = false;//1药，2非药11
            svControl.Columns[(int)Cols.gb_code].Visible = false;//国标码12
            svControl.Columns[(int)Cols.freq_name].Visible = false;//频次名称13
            svControl.Columns[(int)Cols.usage_name].Visible = false;//用法名称14
            svControl.Columns[(int)Cols.class_code].Visible = false;//系统类别15
            svControl.Columns[(int)Cols.english_code].Visible = false;//英文16
            svControl.Columns[(int)Cols.pub_grade].Visible = false;//公费标志17
            svControl.Columns[(int)Cols.child_price].Visible = false;//儿童价格18
            svControl.Columns[(int)Cols.special_price].Visible = false;//特诊价格19

            svControl.Columns[(int)Cols.input_code].Label = "输入码";
            svControl.Columns[(int)Cols.spell_code].Label ="拼音码";
            svControl.Columns[(int)Cols.wb_code].Label ="五笔码";
            svControl.Columns[(int)Cols.item_code].Label ="项目代码";
            svControl.Columns[(int)Cols.item_name].Label ="项目名称";
            svControl.Columns[(int)Cols.specs].Label =" 规格";
            svControl.Columns[(int)Cols.price].Label ="价格";
            svControl.Columns[(int)Cols.unit].Label ="单位";
            svControl.Columns[(int)Cols.producer].Label ="厂家";
            svControl.Columns[(int)Cols.memo].Label ="备注";
            svControl.Columns[(int)Cols.grade].Label ="医保级别";
            svControl.Columns[(int)Cols.isdrug].Label ="项目";
            svControl.Columns[(int)Cols.gb_code].Label ="国标码";
            svControl.Columns[(int)Cols.freq_name].Label ="频次";
            svControl.Columns[(int)Cols.usage_name].Label ="用法";
            svControl.Columns[(int)Cols.class_code].Label ="系统类别";
            svControl.Columns[(int)Cols.english_code].Label ="英文";
            svControl.Columns[(int)Cols.pub_grade].Label ="公费标志";
            svControl.Columns[(int)Cols.child_price].Label ="儿童价";
            svControl.Columns[(int)Cols.special_price].Label ="特诊价";

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
        /// 显示当前行和总行数
        /// </summary>
        /// <returns></returns>
        private int DisplayCurrentRow(int SheetRow)
        {
            int row = _CurrentRow + SheetRow + 1;
            if (_dvItems.Count < 6)
                lbCount.Text = "当前行:" + row.ToString() + "/共:" + fpSpread1_Sheet1.RowCount.ToString();
            else
                lbCount.Text = "当前行:" + row.ToString() + "/共:" + _dvItems.Count.ToString();

            #region MyRegion
            string itemid = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 3].Text;
            if (string.IsNullOrEmpty(itemid))
            {
                this.pnlBottom.Visible = false;
            }
            else
            {
                if (this.IItemCompareInfo != null&&itemid!="999")
                {
                    //ArrayList alExtendInfo = new ArrayList();

                    //IItemExtendInfo.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    //if (this.pactInfo != null)
                    //{
                    //    if (string.IsNullOrEmpty(this.pactInfo.ID))
                    //    {
                    //        this.pactInfo.ID = "1";
                    //    }
                    //    IItemExtendInfo.PactInfo = this.pactInfo;
                    //}

                    FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemid);

                    FS.HISFC.Models.SIInterface.Compare compare = null;
                    string strCompareInfo = string.Empty;

                    int iRtn = IItemCompareInfo.GetCompareItemInfo(item, pactInfo, ref compare, ref strCompareInfo);
                    if (string.IsNullOrEmpty(strCompareInfo))
                    {
                        this.pnlBottom.Visible = false;
                    }
                    else
                    {
                        this.pnlBottom.Visible = true;
                        txt.Multiline = true;
                        txt.Text = strCompareInfo;
                        txt.ReadOnly = true;
                        txt.Multiline = true;
                        txt.Visible = true;
                        txt.ScrollBars = ScrollBars.Both;
                        txt.Dock = DockStyle.Fill;
                        this.pnlBottom.Controls.Add(txt);
                    }
                }
            }
            #endregion

            return 1;
        }
        /// <summary>
        /// 设置Sheet当前行
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public int SetCurrentRow(int Row)
        {
            if (Row < 0 || Row > 5) return -1;
            fpSpread1_Sheet1.ActiveRowIndex = Row;
            fpSpread1_Sheet1.AddSelection(Row, 0, 1, 1);
            return 1;
        }

        #endregion

        #region 事件
        //触发selectitem事件
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (SelectItem != null)
            {
                this.SelectItem(Keys.Enter);
            }
        }

        //当焦点在fpspread1_Sheet1上，处理enter事件
        private void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //[2011-9-13]zhao.zf 屏蔽快捷方式
            if (e.KeyCode == Keys.Enter)
            //|| e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2
            //    || e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4 || e.KeyCode == Keys.F5
            //    || e.KeyCode == Keys.F6)
            {
                //switch (e.KeyCode)
                //{
                //    case Keys.F1:
                //        fpSpread1_Sheet1.ActiveRowIndex = 0;
                //        break;
                //    case Keys.F2:
                //        fpSpread1_Sheet1.ActiveRowIndex = 1;
                //        break;
                //    case Keys.F3:
                //        fpSpread1_Sheet1.ActiveRowIndex = 2;
                //        break;
                //    case Keys.F4:
                //        fpSpread1_Sheet1.ActiveRowIndex = 3;
                //        break;
                //    case Keys.F5:
                //        fpSpread1_Sheet1.ActiveRowIndex = 4;
                //        break;
                //    case Keys.F6:
                //        fpSpread1_Sheet1.ActiveRowIndex = 5;
                //        break;
                //}

                fpSpread1_Sheet1.AddSelection(fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);

                if (SelectItem != null)
                {
                    this.SelectItem(e.KeyCode);
                }
            }
        }
        //处理up,down,pageup,pagedown事件
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
        /// 单击行时，变化当前行
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

        #region IInterfaceContainer 成员
        /// <summary>
        /// 接口容器
        /// </summary>
        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo) };
            }
        }

        #endregion

        private void fpSpread1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string itemid = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 3].Text;

            if (this.IItemCompareInfo != null)
            {
                //string itmExtendInfo = "";
                ArrayList alExtendInfo = new ArrayList();

                //IItemExtendInfo.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                //if (this.pactInfo != null)
                //{
                //    if (string.IsNullOrEmpty(this.pactInfo.ID))
                //    {
                //        this.pactInfo.ID = "1";
                //    }
                //    IItemExtendInfo.PactInfo = this.pactInfo;
                //}
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemid);

                FS.HISFC.Models.SIInterface.Compare compare = null;
                string strCompareInfo = string.Empty;

                int iRtn = IItemCompareInfo.GetCompareItemInfo(item, pactInfo, ref compare, ref strCompareInfo);
                if (string.IsNullOrEmpty(strCompareInfo))
                {
                    this.pnlBottom.Visible = false;
                }
                else
                {
                    txt.Multiline = true;
                    txt.Text = strCompareInfo;
                    txt.ReadOnly = true;
                    txt.Multiline = true;
                    txt.Visible = true;
                    txt.ScrollBars = ScrollBars.Both;
                    txt.Dock = DockStyle.Fill;
                    this.pnlBottom.Controls.Add(txt);
                }
            }
        }

        private void cbxIsReal_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveUserDefaultSetting();
        }

        /// <summary>
        /// 设置用户默认配置
        /// </summary>
        private void SaveUserDefaultSetting()
        {
            settingObj = settingManager.Query(settingManager.Operator.ID);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (settingObj == null)
            {
                settingObj = new FS.HISFC.Models.Base.UserDefaultSetting();
                settingObj.Empl.ID = settingManager.Operator.ID;

                settingObj.Setting3 = this.cbxIsReal.Checked ? "1" : "0";

                settingObj.Oper.ID = settingManager.Operator.ID;
                settingObj.Oper.OperTime = settingManager.GetDateTimeFromSysDateTime();

                if (settingManager.Insert(settingObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入用户设置信息出错" + settingManager.Err);
                    return;
                }
            }
            else
            {
                settingObj.Setting3 = this.cbxIsReal.Checked ? "1" : "0";

                settingObj.Oper.ID = settingManager.Operator.ID;
                settingObj.Oper.OperTime = settingManager.GetDateTimeFromSysDateTime();

                if (settingManager.Update(settingObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新用户设置信息出错" + settingManager.Err);
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
        }

        private void btnPriorRow_Click(object sender, EventArgs e)
        {
            PriorRow();
        }

        private void btnNextRow_Click(object sender, EventArgs e)
        {
            NextRow();
        }

        private void lklPageUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.PriorPage();
        }

        private void lklPageDown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.NextPage();
        }
    }

}
