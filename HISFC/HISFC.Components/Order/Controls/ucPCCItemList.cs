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
    /// [��������: ��ҩ��Ŀ�б�]<br></br>
    /// [�� �� ��: sunm]<br></br>
    /// [����ʱ��: 2010-09-18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPCCItemList : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
       
        public ucPCCItemList()
        {
            InitializeComponent();
            InitDataSet();
        }

        
        #region ����
        //��Ŀ����
        private ArrayList _alItems = new ArrayList();
        //��Ŀdataset
        private DataSet _dsItems;
        private DataView _dvItems;
        
        //��ѯ��ʽ0:ƴ���� 1:����� 2:�Զ����� 3:������ 4:Ӣ��
        private int _InputType = 0;
        /// <summary>
        /// �������ڿ���
        /// </summary>
        private string patientDept = string.Empty;
        //dataview�еĵ�ǰ��
        private int _CurrentRow = -1;
        public delegate int MyDelegate(Keys key);
        /// <summary>
        /// ˫�����س���Ŀ�б�ʱִ�е��¼�
        /// </summary>
        public event MyDelegate SelectItem;

        private FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo iItemCompareInfo = null;
        /// <summary>
        /// ��Ŀ��չ��Ϣ�ӿ�
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
        /// ��չ��Ϣ�ı�
        /// </summary>
        public System.Windows.Forms.TextBox txt = new TextBox();


        FS.HISFC.BizLogic.Manager.UserDefaultSetting settingManager = new FS.HISFC.BizLogic.Manager.UserDefaultSetting();

        FS.HISFC.Models.Base.UserDefaultSetting settingObj = null;

        #endregion

        #region ����
        
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

        #region ����

        /// <summary>
        ///��ʼ���õ���
        /// </summary>
        /// <returns></returns>
        protected virtual  int Init()
        {
            InitItem();

            this.SetUserDefaultSetting();

            return 1;
        }

        /// <summary>
        /// ���õ�ǰ�û�Ĭ������
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
        /// ������Ŀ��Ϣ
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
            
            this.patientDept = patientDept;
            if (Init() == -1) return -1;
            return 1;
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
            
            return 1;
        }

        private void InitDataSet()
        {
            //��ʼ��
            
            SetStyle("", "", fpSpread1_Sheet1);
            SetAutoSize();//�˷�������û����ʾ������
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
        
        #endregion

        #region ˽�к���
        
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
            int _intHeight = SystemInformation.Border3DSize.Height * 2;
            _intHeight += (int)fpSpread1_Sheet1.ColumnHeader.Rows[0].Height;

            for (i = 0; i < fpSpread1_Sheet1.RowCount; i++)
                _intHeight += (int)fpSpread1_Sheet1.Rows[i].Height;

            this.Height = _intHeight + 35+this.pnlBottom.Height;

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
                //TODO: ���ز�ҩ�б�
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
                MessageBox.Show("�����շ���Ŀ�б�ʱ����!" + ex.Message);
                return -1;
            }
            catch (NullReferenceException ex1)
            {
                MessageBox.Show("�����շ���Ŀ�б�ʱ����!" + ex1.Message);
                return -1;
            }
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
        /// ��_dsItems����Ŀ��ӵ�Sheet�У�ÿ�����6����¼
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
        /// ������Ŀ
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public int Filter(string strText)
        {
            string _filter = strText.Trim();
            this._Text = _filter;
            if (_filter.Length > 0)
            {
                //"/"��ʾ���������Ƽ��������ܵ�ǰ�Ǻ����뷨
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
                strKeLiAndYinPian = "����";
                _filter = _filter.Substring(1);
            }
            else if (_filter.StartsWith("Y"))
            {
                strKeLiAndYinPian = "��Ƭ";
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

            //����������
            if (strKeLiAndYinPian == "����")
            {
                filter += " AND (item_name LIKE '%����%')";
            }
            else if (strKeLiAndYinPian == "��Ƭ")
            {
                filter += " AND (item_name not LIKE '%����%')";
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
        /// ������Ŀ
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
                //				//"/"��ʾ���������Ƽ��������ܵ�ǰ�Ǻ����뷨
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
        /// ������һҳ
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
            svControl.Columns[(int)Cols.grade].Visible = false;//ҽ������10
            svControl.Columns[(int)Cols.isdrug].Visible = false;//1ҩ��2��ҩ11
            svControl.Columns[(int)Cols.gb_code].Visible = false;//������12
            svControl.Columns[(int)Cols.freq_name].Visible = false;//Ƶ������13
            svControl.Columns[(int)Cols.usage_name].Visible = false;//�÷�����14
            svControl.Columns[(int)Cols.class_code].Visible = false;//ϵͳ���15
            svControl.Columns[(int)Cols.english_code].Visible = false;//Ӣ��16
            svControl.Columns[(int)Cols.pub_grade].Visible = false;//���ѱ�־17
            svControl.Columns[(int)Cols.child_price].Visible = false;//��ͯ�۸�18
            svControl.Columns[(int)Cols.special_price].Visible = false;//����۸�19

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
            if (_dvItems.Count < 6)
                lbCount.Text = "��ǰ��:" + row.ToString() + "/��:" + fpSpread1_Sheet1.RowCount.ToString();
            else
                lbCount.Text = "��ǰ��:" + row.ToString() + "/��:" + _dvItems.Count.ToString();

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
        /// ����Sheet��ǰ��
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
            //[2011-9-13]zhao.zf ���ο�ݷ�ʽ
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
        /// <summary>
        /// �ӿ�����
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
        /// �����û�Ĭ������
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
                    MessageBox.Show("�����û�������Ϣ����" + settingManager.Err);
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
                    MessageBox.Show("�����û�������Ϣ����" + settingManager.Err);
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
