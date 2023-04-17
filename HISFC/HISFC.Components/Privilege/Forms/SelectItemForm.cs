using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.Privilege.WinForms.Forms
{
    public partial class SelectItemForm<T> : InputBaseForm
    {
        public SelectItemForm()
        {
            InitializeComponent();            
            this.InitDataSet();

            ///���������¼�
            this.txtInput.TextChanged += new EventHandler(txtInput_TextChanged);
            this.txtInput.KeyDown += new KeyEventHandler(txtInput_KeyDown);
            
            this.chbSelect.CheckedChanged += new EventHandler(chbSelect_CheckedChanged);

            this.fpSpread1.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);

        }

        void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }       

        void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void chbSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.txtInput.Focus();
            this.txtInput.SelectAll();
        }


        void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            //�ϼ�ͷѡ����һ����¼
            if (e.KeyCode == Keys.Up)
            {
                if (this.fpSpread1_Sheet1.ActiveRowIndex > 0)
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex--;
                    this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 4);
                    return;
                }
            }

            //�¼�ͷѡ����һ����¼
            if (e.KeyCode == Keys.Down)
            {
                if (this.fpSpread1_Sheet1.ActiveRowIndex < this.fpSpread1_Sheet1.RowCount - 1)
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex++;
                    this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 4);
                    return;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.RowCount == 0) return;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (e.KeyCode == Keys.Cancel)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
        }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtInput_TextChanged(object sender, EventArgs e)
        {
            string _key = this.txtInput.Text.Trim();
            _key = NFC.Public.String.TakeOffSpecialChar(_key);

            if (this.IsSimilar)
                _key = "%" + _key + "%";
            else
                _key = _key + "%";

            string _filter = "";
            //��ѯȫ��
            if (_key == "%" || _key == "%%")
            {
                _filter = "���� like '%'";
            }
            else
            {
                _filter = "(ƴ���� LIKE '" + _key + "') OR " +
                        "(������ LIKE '" + _key + "') OR " +
                        "(���� LIKE '" + _key + "') OR " +
                        "(���� LIKE '" + _key + "')";
            }

            _dvItems.RowFilter = _filter;
            this.AddItemToFp();

            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                this.fpSpread1_Sheet1.AddSelection(0, 0, 1, 4);
            }
        }

        private IList<T> _items;
        private DataSet _dsItems;
        private DataView _dvItems;
        private string _id = "ID";
        private string _secondKey;
        private string _name = "Name";
        private string _description;
        private string _input;

        /// <summary>
        /// id��
        /// </summary>
        public string ID
        {
            set { _id = value; }
        }

        /// <summary>
        /// �ڶ�����
        /// </summary>
        public string SecondKey
        {
            set { _secondKey = value; }
        }

        /// <summary>
        /// name��
        /// </summary>
        public string Value
        {
            set { _name = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Input
        {
            set { _input = value; }
        }
        /// <summary>
        /// ��ע��
        /// </summary>
        public string Description
        {
            set { _description = value; }
        }

        /// <summary>
        /// ѡ����Ŀ
        /// </summary>
        public T SelectedItem
        {
            get
            {
                if (this.fpSpread1_Sheet1.RowCount == 0) return default(T);

                string _firValue = this.fpSpread1_Sheet1.GetText(this.fpSpread1_Sheet1.ActiveRowIndex, 0);
                string _secValue = toString(this.fpSpread1_Sheet1.GetTag(this.fpSpread1_Sheet1.ActiveRowIndex, 0));

                foreach (T _item in _items)
                {
                    if (string.IsNullOrEmpty(_secondKey))
                    {
                        if (toString(typeof(T).GetProperty(_id).GetValue(_item, null)) == _firValue)
                            return _item;
                    }
                    else
                    {
                        if (toString(typeof(T).GetProperty(_id).GetValue(_item, null)) == _firValue &&
                            toString(typeof(T).GetProperty(_secondKey).GetValue(_item, null)) == _secValue)
                            return _item;
                    }
                }

                return default(T);
            }
        }
        /// <summary>
        /// �Ƿ�ģ����ѯ
        /// </summary>
        public bool IsSimilar
        {
            get { return this.chbSelect.Checked; }
            set { this.chbSelect.Checked = value; }
        }

        private void InitDataSet()
        {
            _dsItems = new DataSet();
            _dsItems.Tables.Add();

            //��������
            System.Type dtStr = System.Type.GetType("System.String");

            //��myDataTable�������
            this._dsItems.Tables[0].Columns.AddRange(new DataColumn[] {
																			new DataColumn("����",     dtStr),
																			new DataColumn("����",     dtStr),
                                                                            new DataColumn("������",   dtStr),
																			new DataColumn("����",     dtStr),                                                                            
																			new DataColumn("ƴ����",   dtStr),
																			new DataColumn("SecondKey",   dtStr)																			
																		});
            this._dvItems = new DataView(_dsItems.Tables[0]);
        }

        public void InitItem(IList<T> items)
        {
            string _idValue, _nameValue, _descValue = "", _inputValue = "", _spellValue, _secondValue = "";

            _items = items;
            foreach (T _item in _items)
            {
                _idValue = toString(typeof(T).GetProperty(_id).GetValue(_item, null));
                _nameValue = toString(typeof(T).GetProperty(_name).GetValue(_item, null));
                if (!string.IsNullOrEmpty(_input))
                {
                    _inputValue = toString(typeof(T).GetProperty(_input).GetValue(_item, null));
                }
                if (!string.IsNullOrEmpty(_description))
                {
                    _descValue = toString(typeof(T).GetProperty(_description).GetValue(_item, null));
                }

                if (!string.IsNullOrEmpty(_secondKey))
                {
                    _secondValue = toString(typeof(T).GetProperty(_secondKey).GetValue(_item, null));
                }

                _spellValue = NFC.Public.String.GetSpell(_nameValue);

                this._dsItems.Tables[0].Rows.Add(new object[] {
																		_idValue,    //����
																		_nameValue,  //����
                                                                        _inputValue, //�Զ�����	
																		_descValue,  //����                                                                        
																		_spellValue, //ƴ����		
																		_secondValue    //tag																				
																	});
            }

            this.AddItemToFp();
        }

        private void AddItemToFp()
        {
            if(this.fpSpread1_Sheet1.RowCount>0)
                this.fpSpread1_Sheet1.Rows.Remove(0,this.fpSpread1_Sheet1.RowCount);

            for (int i = 0; i < _dvItems.Count; i++)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

                this.fpSpread1_Sheet1.SetValue(i, 0, toString(_dvItems[i][0]));
                this.fpSpread1_Sheet1.SetValue(i, 1, toString(_dvItems[i][1]));
                this.fpSpread1_Sheet1.SetValue(i, 2, toString(_dvItems[i][2]));
                this.fpSpread1_Sheet1.SetValue(i, 3, toString(_dvItems[i][3]));

                this.fpSpread1_Sheet1.SetTag(i, 0, toString(_dvItems[i][5]));
            }

            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                this.fpSpread1_Sheet1.AddSelection(0, 0, 1, 4);
            }
        }

        private string toString(object obj)
        {
            if (obj == null) return "";

            return obj.ToString();
        }

        
    }    
}

